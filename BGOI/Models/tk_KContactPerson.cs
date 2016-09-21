using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_KContactPerson
    {
        private string _KID;
        [DataFieldAttribute("KID", "varchar")]
        public string KID
        {
            get { return _KID; }
            set { _KID = value; }
        }
        private string _CName;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CName", "varchar")]
        public string CName
        {
            get { return _CName; }
            set { _CName = value; }
        }
        private string _Sex;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [RegularExpression(@"^(男|女)$", ErrorMessage = "请输入正确的性别")]
        [DataFieldAttribute("Sex", "varchar")]
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        private string _Job;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Job", "varchar")]
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        private DateTime? _Birthday;
        //[RegularExpression(@"^(19|20)(\d){2}.([1-9]|1[0-2]).([1-9]|[12][0-9]|3[0-1])$", ErrorMessage = "请输入正确的生日")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Birthday", "varchar")]
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }
        private int _Age;
        [RegularExpression(@"^(?:[1-9][0-9]?|1[01][0-9]|120)$", ErrorMessage = "请输入正确的年龄")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Age", "varchar")]
        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        private string _Mobile;
       // [RegularExpression(@"^\s*(15\d{9}|13[0-9]\d{8})\s*$", ErrorMessage = "请输入正确的手机号")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Mobile", "varchar")]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        private string _FAX;
        [RegularExpression(@"^(\d{3,4}-)?\d{7,8}$", ErrorMessage = "请输入正确的传真号")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FAX", "varchar")]
        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        private string _Email;
        [RegularExpression(@"^[a-z]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?$", ErrorMessage = "请输入正确的Email地址")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Email", "varchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _QQ;
        [RegularExpression(@"^\d{5,10}$", ErrorMessage = "请输入正确的QQ")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("QQ", "varchar")]
        public string QQ
        {
            get { return _QQ; }
            set { _QQ = value; }
        }
        private string _WeiXin;
        [StringLength(10, ErrorMessage = "微信不能超过10个字符")]
        [RegularExpression(@"^[a-z_\d]+$", ErrorMessage = "请输入正确的微信")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("WeiXin", "varchar")]
        public string WeiXin
        {
            get { return _WeiXin; }
            set { _WeiXin = value; }
        }
        private string _Remark;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _CreateTime;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
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
