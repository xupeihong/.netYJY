using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Tk_SContactPerson
    {
        public Tk_SContactPerson()
        {
        }
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
    
        private string _fdepartment;
        [DataFieldAttribute("Fdepartment", "nvarchar")]
        public string Fdepartment
        {
            get { return _fdepartment; }
            set { _fdepartment = value; }
        }
        private string _pname;
        [RegularExpression(@"/^\s*[\u4e00-\u9fa5]{1,15}\s*$/", ErrorMessage = "请输入正确的姓名格式")]
        [DataFieldAttribute("Pname", "nvarchar")]
        public string Pname
        {
            get { return _pname; }
            set { _pname = value; }
        }
        private string _department;
        [DataFieldAttribute("Department", "nvarchar")]
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        private string _job;
        [DataFieldAttribute("Job", "nvarchar")]
        public string Job
        {
            get { return _job; }
            set { _job = value; }
        }
        private string _phone;
        [RegularExpression(@"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$", ErrorMessage = "请输入正确的座机号")]
        [DataFieldAttribute("Phone", "varchar")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _mobile;
        [RegularExpression(@"/^\s*(15\d{9}|13[0-9]\d{8})\s*$/", ErrorMessage = "请输入正确的手机号")]
        [DataFieldAttribute("Mobile", "varchar")]
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        //private string _FAX;
        //[DataFieldAttribute("FAX", "varchar")]
        //public string FAX
        //{
        //    get { return _FAX; }
        //    set { _FAX = value; }
        //}
        private string _Email;
        [DataFieldAttribute("Email", "varchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _createUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return _createUser; }
            set { _createUser = value; }
        }
        private DateTime? _createTime;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private string _validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }
    }
}
