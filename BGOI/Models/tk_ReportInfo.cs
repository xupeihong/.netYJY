using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ReportInfo
    {
        private string strBGID="";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BGID", "nvarchar")]
        public string BGID
        {
            get { return strBGID; }
            set { strBGID = value; }
        }

        private string strRWID = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWID", "nvarchar")]
        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
        }

        private string strDID = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DID", "nvarchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private DateTime struploadtime;
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("uploadtime", "DateTime")]
        public DateTime uploadtime
        {
            get { return struploadtime; }
            set { struploadtime = value; }
        }

        private string strRemarks = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remarks", "nvarchar")]
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        private string strCreatePerson = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreatePerson", "nvarchar")]
        public string CreatePerson
        {
            get { return strCreatePerson; }
            set { strCreatePerson = value; }
        }

        private DateTime strCreateTime;
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strValidate = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        //private DateTime strCancelTime;
        //[DataFieldAttribute("CancelTime", "DateTime")]
        //public DateTime CancelTime
        //{
        //    get { return strCancelTime; }
        //    set { strCancelTime = value; }
        //}

        private string strCancelReason = "";
          [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelReason", "nvarchar")]
        public string CancelReason
        {
            get { return strCancelReason; }
            set { strCancelReason = value; }
        }
    }
}
