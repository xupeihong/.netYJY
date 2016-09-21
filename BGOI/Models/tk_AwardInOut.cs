using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public class tk_AwardInOut
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
        private string _Award;
        [DataFieldAttribute("Award", "varchar")]
        public string Award
        {
            get { return _Award; }
            set { _Award = value; }
        }
        private string _AwardInfo;
        [DataFieldAttribute("AwardInfo", "varchar")]
        public string AwardInfo
        {
            get { return _AwardInfo; }
            set { _AwardInfo = value; }
        }
        private DateTime? _AwardTime;
        [DataFieldAttribute("AwardTime", "varchar")]
        public DateTime? AwardTime
        {
            get { return _AwardTime; }
            set { _AwardTime = value; }
        }
        private string _CreatUser;
        [DataFieldAttribute("CreatUser", "varchar")]
        public string CreatUser
        {
            get { return _CreatUser; }
            set { _CreatUser = value; }
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
