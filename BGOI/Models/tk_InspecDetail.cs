using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_InspecDetail
    {

        private string strSID;
        [DataFieldAttribute("SID", "nvarchar")]
        public string SID
        {
            get { return strSID; }
            set { strSID = value; }
        }
        private string strRID;
        [DataFieldAttribute("RID", "nvarchar")]
        public string RID
        {
            get { return strRID; }
            set { strRID = value; }
        }

        private string strMeterID;
        [DataFieldAttribute("MeterID", "nvarchar")]
        public string MeterID
        {
            get { return strMeterID; }
            set { strMeterID = value; }
        }

        private string strAccuracy;
        [DataFieldAttribute("Accuracy", "nvarchar")]
        public string Accuracy
        {
            get { return strAccuracy; }
            set { strAccuracy = value; }
        }

        private int strMcount;
        [DataFieldAttribute("Mcount", "int")]
        public int Mcount
        {
            get { return strMcount; }
            set { strMcount = value; }
        }

        private DateTime? strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate;
        [DataFieldAttribute("Validate", "char")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }


    }
}
