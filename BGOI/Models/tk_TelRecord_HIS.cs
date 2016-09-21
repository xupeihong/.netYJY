using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_TelRecord_HIS
    {

        public string strDHID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DHID", "varchar")]
        //电话记录编号
        public string DHID
        {
            get { return strDHID; }
            set { strDHID = value; }
        }
        public DateTime strAnswerDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AnswerDate", "datetime")]
        //接听日期
        public DateTime AnswerDate
        {
            get { return strAnswerDate; }
            set { strAnswerDate = value; }
        }
        public string strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Address", "varchar")]
        //地址内容
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strUserName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserName", "varchar")]
        //联系人
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //联系电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        //, , , CreateTime, CreateUser, Validate
        public string strSchoolWork = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SchoolWork", "varchar")]
        //派工单号
        public string SchoolWork
        {
            get { return strSchoolWork; }
            set { strSchoolWork = value; }
        }
        public string strProcessingResults = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProcessingResults", "varchar")]
        //处理结果
        public string ProcessingResults
        {
            get { return strProcessingResults; }
            set { strProcessingResults = value; }
        }
        public string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "nvarchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private DateTime? _NCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return _NCreateTime; }
            set { _NCreateTime = value; }
        }
        private string _NCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return _NCreateUser; }
            set { _NCreateUser = value; }
        }
    }
}
