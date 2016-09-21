using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_PriceUp
    {

        private string _ID;
        [DataFieldAttribute("ID", "varchar")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _SID;
        [DataFieldAttribute("SID", "varchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _PriceName;
        [DataFieldAttribute("PriceName", "varchar")]
        public string PriceName
        {
            get { return _PriceName; }
            set { _PriceName = value; }
        }
        private string _PriceInfo;
        [DataFieldAttribute("PriceInfo", "varchar")]
        public string PriceInfo
        {
            get { return _PriceInfo; }
            set { _PriceInfo = value; }
        }
        private DateTime? _PriceTime;
        [DataFieldAttribute("PriceTime", "time")]
        public DateTime? PriceTime
        {
            get { return _PriceTime; }
            set { _PriceTime = value; }
        }
        private string _Createuser;
        [DataFieldAttribute("Createuser", "time")]
        public string Createuser
        {
            get { return _Createuser; }
            set { _Createuser = value; }
        }
        private string _Validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }
    }
}
