using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public  class tk_SHReturnVisit
    {
        public string strHFID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HFID", "varchar")]
        //回访编号
        public string HFID
        {
            get { return strHFID; }
            set { strHFID = value; }
        }
        public string strUntiID = "";
       
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UntiID", "varchar")]
        //回访处理单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strRecordID = "";
        //[Remote("RecordIDExists", "CustomerService", ErrorMessage = "该编号已存在")]
        //[StringLength(20, ErrorMessage = "编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RecordID", "varchar")]
        //编号，自填
        public string RecordID
        {
            get { return strRecordID; }
            set { strRecordID = value; }
        }
        public DateTime strRVDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RVDate", "datetime")]
        //回访日期
        public DateTime RVDate
        {
            get { return strRVDate; }
            set { strRVDate = value; }
        }
     
        public string strProductID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductID", "varchar")]
        //产品
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        public string strContactPerson = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContactPerson", "varchar")]
        //联系人
        public string ContactPerson
        {
            get { return strContactPerson; }
            set { strContactPerson = value; }
        }
        public string strUserInformation = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserInformation", "varchar")]
        //用户情况简述
        public string UserInformation
        {
            get { return strUserInformation; }
            set { strUserInformation = value; }
        }
      
        public DateTime strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strSatisfiedDegree = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SatisfiedDegree", "varchar")]
        //对此次服务满意度
        public string SatisfiedDegree
        {
            get { return strSatisfiedDegree; }
            set { strSatisfiedDegree = value; }
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
        public string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //创建人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strReturnVisit = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReturnVisit", "varchar")]
        //回访人
             public string ReturnVisit
        {
            get { return strReturnVisit; }
            set { strReturnVisit = value; }
        }
    }
}
