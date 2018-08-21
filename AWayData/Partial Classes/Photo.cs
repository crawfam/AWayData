using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWayData
{
    public partial class Photo
    {

        private int _id = -1;
        private string _caption = string.Empty;
        private int _pnum = -1;
        private string _PhotoText = string.Empty;
        public int PhotoNum { get { return _pnum; } }

        #region PhotoDateTimeString
        /// <summary>
        /// PhotoDate - Public read-only
        /// string representing the date as
        /// paresed from the caption.
        /// </summary>
        public string PhotoDateTimeString
        {
            get
            {
                string[] sa = this.Caption.Split(". ".ToCharArray());
                string strDataTime = sa[0] + "/" + sa[1] + "/" + sa[2] + " " + sa[3] + ":" + sa[4] + ":" + sa[5] + " " + sa[7];
                System.DateTime dtDateTime = System.DateTime.Parse(strDataTime);
                return dtDateTime.ToLongDateString() + " " + dtDateTime.ToShortTimeString();
            }
        }
        #endregion

        #region PhotoDateTime
        /// <summary>
        /// PhotoDateTime
        /// </summary>
        public System.DateTime PhotoDateTime
        {
            get
            {
                string[] sa = this.Caption.Split(". ".ToCharArray());
                string strDataTime = sa[0] + "/" + sa[1] + "/" + sa[2] + " " + sa[3] + ":" + sa[4] + ":" + sa[5] + " " + sa[7];
                System.DateTime dtDateTime = System.DateTime.Parse(strDataTime);
                return dtDateTime;
            }
        }
        #endregion

        #region PhotoDateShort
        /// <summary>
        /// PhotoDateShort - Public read only
        /// string representing a short date
        /// as parsed from the PhotoDate.
        /// </summary>
        public string PhotoDateShort
        {
            get
            {
                System.DateTime dtDateTime = System.DateTime.Parse(PhotoDateTimeString);
                return dtDateTime.ToShortDateString();
            }
        }
        #endregion

        #region PhotoTimeShort
        /// <summary>
        /// PhotoTimeShort - Public read only
        /// string representing a short date
        /// as parsed from the PhotoDate.
        /// </summary>
        public string PhotoTimeShort
        {
            get
            {
                System.DateTime dtDateTime = System.DateTime.Parse(PhotoDateTimeString);
                return dtDateTime.ToShortTimeString();
            }
        }
        #endregion

        #region PhotoText
        public string PhotoText
        {
            get
            {
                return _PhotoText;
            }

            set
            {
                _PhotoText = value;
            }
        }
        #endregion

        #region HTMLPhotoText
        public string HTMLPhotoText
        {
            get
            {
                string s = System.Web.HttpContext.Current.Server.HtmlEncode(_PhotoText);
                return s.Replace("\r\n", "<br/>");
            }
        }
        #endregion

        #region Photo Constructor #1
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Photo() { }
        #endregion

        #region Photo Constructor #2
        /// <summary>
        /// Constructor #3 Takes only the PhotoID,
        /// and Caption.
        /// </summary>
        /// <param name="caption">Photo Caption</param>
        public Photo(int PhotoID, string caption, System.DateTime PhotoDate)
        {
            _id = PhotoID;
            _caption = caption;
            // PhotoDate does nothing now
            // added to differentiate with
            // constructor #1
        }
        #endregion

        #region Photo Constructor #3
        /// <summary>
        /// Constructor #4 Takes only the 
        /// the Caption - to be used to be able to pull
        /// out the PhotoDate and PhotoDate members to the 
        /// application.
        /// </summary>
        /// <param name="caption"></param>
        public Photo(string caption)
        {
            _caption = caption;
        }
        #endregion
    }

}
