using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SHComplain
    {
        public string strTSID = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TSID", "varchar")]
        //投诉编号
        public string TSID
        {
            get { return strTSID; }
            set { strTSID = value; }
        }
        public string strUntiID = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UntiID", "varchar")]
        //投诉处理单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strCustomerID = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerID", "varchar")]
        //客户编号
        public string CustomerID
        {
            get { return strCustomerID; }
            set { strCustomerID = value; }
        }
        public string strCustomer = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Customer", "varchar")]
        //客户名称
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }
        public DateTime strRecordDate;
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RecordDate", "datetime")]
        //投诉日期
        public DateTime RecordDate
        {
            get { return strRecordDate; }
            set { strRecordDate = value; }
        }
        public string strComplaintDate = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ComplaintDate", "varchar")]
        //投诉时间
        public string ComplaintDate
        {
            get { return strComplaintDate; }
            set { strComplaintDate = value; }
        }
        public string strEmergencyDegree = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("EmergencyDegree", "varchar")]
        //紧急程度
        public string EmergencyDegree
        {
            get { return strEmergencyDegree; }
            set { strEmergencyDegree = value; }
        }
        public string strComplaintTheme = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ComplaintTheme", "varchar")]
        //投诉主题
        public string ComplaintTheme
        {
            get { return strComplaintTheme; }
            set { strComplaintTheme = value; }
        }
        public string strComplaintCategory = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ComplaintCategory", "varchar")]
        //投诉类别
        public string ComplaintCategory
        {
            get { return strComplaintCategory; }
            set { strComplaintCategory = value; }
        }
        public string strFirstDealUser = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FirstDealUser", "varchar")]
        //首次处理人
        public string FirstDealUser
        {
            get { return strFirstDealUser; }
            set { strFirstDealUser = value; }
        }
        public string strComplainContent = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ComplainContent", "varchar")]
        //投诉内容
        public string ComplainContent
        {
            get { return strComplainContent; }
            set { strComplainContent = value; }
        }
        public string strState = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        //0-未处理完成，1-已处理完成
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        public string strRemark = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public DateTime strCreateTime;
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
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
