using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace TestTextToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullFilePath = @"C:\Users\Henry\Documents\Visual Studio 2012\Projects\AWayData\TestTextToImage\MyTestFile.Jpg";
            Image imageText = ConvertTextToImage("Hello World", "Bookman Old Style", 10, Color.Yellow, Color.Red, 40, 40);
            imageText.Save(fullFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static Image ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Image image = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(image))
            {

                Font font = new Font(fontname, fontsize);
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, image.Width, image.Height);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();


            }
            return image;
        }

    }
}
