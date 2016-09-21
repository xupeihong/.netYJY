using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SService
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _ServiceID;
        [DataFieldAttribute("ServiceID", "varchar")]
        public string ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }
        private string _ServiceName;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "服务名称不能超过20个字符")]
        [DataFieldAttribute("ServiceName", "nvarchar")]
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
        private string _ServiceDesc;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "服务描述不能超过20个字符")]
        [DataFieldAttribute("ServiceDesc", "nvarchar")]
        public string ServiceDesc
        {
            get { return _ServiceDesc; }
            set { _ServiceDesc = value; }
        }
        private string _Purpose;
        [DataFieldAttribute("Purpose", "nvarchar")]
        public string Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }
        private string _FFileName;
        [DataFieldAttribute("FFileName", "nvarchar")]
        public string FFileName
        {
            get { return _FFileName; }
            set { _FFileName = value; }
        }
        private string _FileInfo;
        [DataFieldAttribute("FileInfo", "nvarchar")]
        public string FileInfo
        {
            get { return _FileInfo; }
            set { _FileInfo = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
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
