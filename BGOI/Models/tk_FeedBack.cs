using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_FeedBack
    {
        private string _SID;
        [DataFieldAttribute("SID", "nvarchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _FName;
        [DataFieldAttribute("FName", "nvarchar")]
        public string FName
        {
            get { return _FName; }
            set { _FName = value; }
        }
        private string _FContent;
        [DataFieldAttribute("FContent", "nvarchar")]
        public string FContent
        {
            get { return _FContent; }
            set { _FContent = value; }
        }
        private string _FReason;
        [DataFieldAttribute("FReason", "nvarchar")]
        public string FReason
        {
            get { return _FReason; }
            set { _FReason = value; }
        }
        private DateTime? _FTime;
        [DataFieldAttribute("FTime", "nvarchar")]
        public DateTime? FTime
        {
            get { return _FTime; }
            set { _FTime = value; }
        }
    }
}
