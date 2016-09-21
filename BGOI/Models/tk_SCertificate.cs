using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_SCertificate
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _isplan;
        [DataFieldAttribute("Isplan", "char")]
        public string Isplan
        {
            get { return _isplan; }
            set { _isplan = value; }
        }
        private string _ctype;
        [DataFieldAttribute("Ctype", "varchar")]
        public string Ctype
        {
            get { return _ctype; }
            set { _ctype = value; }
        }
        private string _cname;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "证书名称不能超过20个字符")]
        [DataFieldAttribute("Cname", "nvarchar")]
        public string Cname
        {
            get { return _cname; }
            set { _cname = value; }
        }
        private string _ccode;
        [Remote("CheckCodeExists", "SuppliesManage", ErrorMessage = "该证书编号已存在")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "证书编号不能超过20个字符")]
        [DataFieldAttribute("Ccode", "varchar")]
        public string Ccode
        {
            get { return _ccode; }
            set { _ccode = value; }
        }
        private string _corganization;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "认证机构名称不能超过20个字符")]
        [DataFieldAttribute("Corganization", "nvarchar")]
        public string Corganization
        {
            get { return _corganization; }
            set { _corganization = value; }
        }
        private string _cdate;
        [DataFieldAttribute("Cdate", "DateTime")]
        public string Cdate
        {
            get { return _cdate; }
            set { _cdate = value; }
        }
        private string _TimeOut;
        [DataFieldAttribute("TimeOut", "DateTime")]
        public string TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }
        private string _cfilename;
        [DataFieldAttribute("Cfilename", "nvarchar")]
        public string Cfilename
        {
            get { return _cfilename; }
            set { _cfilename = value; }
        }
        private string _fileinfo;
        [DataFieldAttribute("Fileinfo", "varbinary")]
        public string Fileinfo
        {
            get { return _fileinfo; }
            set { _fileinfo = value; }
        }
        private string _createuser;
        [DataFieldAttribute("Createuser", "nvarchar")]
        public string Createuser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        private DateTime? _createtime;
        [DataFieldAttribute("Createtime", "DateTime")]
        public DateTime? Createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        private string _validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }
        private string _FileType;
        [DataFieldAttribute("FileType", "varchar")]
        public string FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }
    }
}
