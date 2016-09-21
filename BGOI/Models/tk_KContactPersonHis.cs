using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_KContactPersonHis
    {
        private string _KID;
        [DataFieldAttribute("KID", "varchar")]
        public string KID
        {
            get { return _KID; }
            set { _KID = value; }
        }
        private string _CName;
        [DataFieldAttribute("CName", "varchar")]
        public string CName
        {
            get { return _CName; }
            set { _CName = value; }
        }
        private string _Sex;
        [DataFieldAttribute("Sex", "varchar")]
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        private string _Job;
        [DataFieldAttribute("Job", "varchar")]
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        private DateTime? _Birthday;
        [DataFieldAttribute("Birthday", "varchar")]
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }
        private int _Age;
        [DataFieldAttribute("Age", "varchar")]
        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        private int _Mobile;
        [DataFieldAttribute("Mobile", "varchar")]
        public int Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        private string _FAX;
        [DataFieldAttribute("FAX", "varchar")]
        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        private string _Email;
        [DataFieldAttribute("Email", "varchar")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private int _QQ;
        [DataFieldAttribute("QQ", "varchar")]
        public int QQ
        {
            get { return _QQ; }
            set { _QQ = value; }
        }
        private string _WeiXin;
        [DataFieldAttribute("WeiXin", "varchar")]
        public string WeiXin
        {
            get { return _WeiXin; }
            set { _WeiXin = value; }
        }
        private string _Remark;
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
        [DataFieldAttribute("CreateTime", "varchar")]
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
