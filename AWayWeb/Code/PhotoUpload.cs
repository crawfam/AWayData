using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using AWayWeb.DataClasses;
using AWayData;

namespace AWayWeb.Code
{
    public class PhotoUpload
    {
        public static List<Photo> getUploadFiles()
        {
            DateTime dtTemp;
            TimeSpan tsTimeSpan = new TimeSpan(0, 0, 0, 50); // a 50 millisecond time span

            // return list 
            List<Photo> pList = new List<Photo>();

            DirectoryInfo d = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Upload"));
            FileInfo[] aryFileInfo = d.GetFiles("*.jpg");

            // put into sorted list for sorting by date
            SortedList<DateTime, FileInfo> lstFiles = new SortedList<DateTime, FileInfo>();

            foreach (FileInfo f in aryFileInfo)
            {
                string[] sa = f.Name.Split(". ".ToCharArray());
                string strDateTime = sa[0] + "/" + sa[1] + "/" + sa[2] + " " + sa[3] + ":" + sa[4] + ":" + sa[5] + " " + sa[7];
                dtTemp = System.DateTime.Parse(strDateTime);

                if (lstFiles.ContainsKey(dtTemp))
                {
                    dtTemp += tsTimeSpan; //    add 5 seconds
                    if (!lstFiles.ContainsKey(dtTemp))
                        lstFiles.Add(dtTemp, f);
                }
                else
                    lstFiles.Add(dtTemp, f);
            }

            foreach (DateTime key in lstFiles.Keys)
            {
                FileInfo f = lstFiles[key];

                if ((f.Extension.ToLower() == ".jpg") || (f.Extension.ToLower() == ".gif") || (f.Extension.ToLower() == ".bmp") || (f.Extension.ToLower() == ".png"))
                {
                    byte[] buffer = new byte[f.OpenRead().Length];
                    f.OpenRead().Read(buffer, 0, (int)f.OpenRead().Length);

                    // note: lob off the extension for caption
                    // add photo to the photo list
                    pList.Add(createPhoto(key, f.Name.Remove(f.Name.Length - 4, 4), buffer));
                }
            }

            return pList;
        }

        public static Photo createPhoto(DateTime dt, string Caption, byte[] BytesOriginal)
        {
            Photo p = new Photo(Caption);
            p.Caption = Caption;
            p.PhotoDate = dt;
            p.PhotoText = Caption;
            p.BytesFull = ResizeImageFile(BytesOriginal, 600);
            p.BytesPoster = ResizeImageFile(BytesOriginal, 198);
            p.BytesThumb = ResizeImageFile(BytesOriginal, 100);

            return p;                                   
        }

        private static byte[] ResizeImageFile(byte[] imageFile, int targetSize)
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return m.GetBuffer();
                    }
                }
            }
        }

        private static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }

        public static ICollection ListUploadDirectory()
        {
            DirectoryInfo d = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Upload"));
            //DirectoryInfo d = new DirectoryInfo("c:\\Upload");
            return d.GetFileSystemInfos("*.*");
        }

    }
}