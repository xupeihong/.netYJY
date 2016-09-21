using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHComplain_HIS
    {
        public string strTSID = "";
        [DataFieldAttribute("TSID", "varchar")]
        //投诉编号
        public string TSID
        {
            get { return strTSID; }
            set { strTSID = value; }
        }
        public string strUntiID = "";
        [DataFieldAttribute("UntiID", "varchar")]
        //投诉处理单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strCustomerID = "";
        [DataFieldAttribute("CustomerID", "varchar")]
        //客户编号
        public string CustomerID
        {
            get { return strCustomerID; }
            set { strCustomerID = value; }
        }
        public string strCustomer = "";
        [DataFieldAttribute("Customer", "varchar")]
        //客户名称
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }
        public DateTime strRecordDate;
        [DataFieldAttribute("RecordDate", "datetime")]
        //投诉日期
        public DateTime RecordDate
        {
            get { return strRecordDate; }
            set { strRecordDate = value; }
        }
        public string strComplaintDate = "";
        [DataFieldAttribute("ComplaintDate", "varchar")]
        //投诉时间
        public string ComplaintDate
        {
            get { return strComplaintDate; }
            set { strComplaintDate = value; }
        }
        public string strEmergencyDegree = "";
        [DataFieldAttribute("EmergencyDegree", "varchar")]
        //紧急程度
        public string EmergencyDegree
        {
            get { return strEmergencyDegree; }
            set { strEmergencyDegree = value; }
        }
        public string strComplaintTheme = "";
        [DataFieldAttribute("ComplaintTheme", "varchar")]
        //投诉主题
        public string ComplaintTheme
        {
            get { return strComplaintTheme; }
            set { strComplaintTheme = value; }
        }
        public string strComplaintCategory = "";
        [DataFieldAttribute("ComplaintCategory", "varchar")]
        //投诉类别
        public string ComplaintCategory
        {
            get { return strComplaintCategory; }
            set { strComplaintCategory = value; }
        }
        public string strFirstDealUser = "";
        [DataFieldAttribute("FirstDealUser", "varchar")]
        //首次处理人
        public string FirstDealUser
        {
            get { return strFirstDealUser; }
            set { strFirstDealUser = value; }
        }
        public string strComplainContent = "";
        [DataFieldAttribute("ComplainContent", "varchar")]
        //投诉内容
        public string ComplainContent
        {
            get { return strComplainContent; }
            set { strComplainContent = value; }
        }
        public string strState = "";
        [DataFieldAttribute("State", "varchar")]
        //0-未处理完成，1-已处理完成
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        public string strRemark = "";
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
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }
        public string strTel = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //客户电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strAdderss = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Adderss", "varchar")]
        //客户地址
        public string Adderss
        {
            get { return strAdderss; }
            set { strAdderss = value; }
        }
    }
}

