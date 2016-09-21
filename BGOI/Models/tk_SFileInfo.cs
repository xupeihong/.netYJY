using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SFileInfo
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _Fid;
        [DataFieldAttribute("Fid", "varchar")]
        public string Fid
        {
            get { return _Fid; }
            set { _Fid = value; }
        }
        private string _ftype;

        [DataFieldAttribute("Ftype", "nvarchar")]
        public string Ftype
        {
            get { return _ftype; }
            set { _ftype = value; }
        }
        private string _typeo;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "其他类说明不能超过20个字符")]
        [DataFieldAttribute("Typeo", "nvarchar")]
        public string Typeo
        {
            get { return _typeo; }
            set { _typeo = value; }
        }
        private string _item;
        [DataFieldAttribute("Item", "nvarchar")]
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }
        private string _itemo;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "其他项说明不能超过20个字符")]
        [DataFieldAttribute("Itemo", "nvarchar")]
        public string Itemo
        {
            get { return _itemo; }
            set { _itemo = value; }
        }
        private string _ffilename;
        [DataFieldAttribute("Ffilename", "nvarchar")]
        public string Ffilename
        {
            get { return _ffilename; }
            set { _ffilename = value; }
        }
        private string _MfFilename;
        [DataFieldAttribute("MfFilename", "nvarchar")]
        public string MfFilename
        {
            get { return _MfFilename; }
            set { _MfFilename = value; }
        }
        private string _filetype;
        [DataFieldAttribute("Filetype", "varchar")]
        public string Filetype
        {
            get { return _filetype; }
            set { _filetype = value; }
        }
        //private string _MFileType;
        //[DataFieldAttribute("MFileType", "varchar")]
        //public string MFileType
        //{
        //    get { return _MFileType; }
        //    set { _MFileType = value; }
        //}
        private string _fileinfo;
        [DataFieldAttribute("Fileinfo", "varbinary")]
        public string Fileinfo
        {
            get { return _fileinfo; }
            set { _fileinfo = value; }
        }
        private string _MFileInfo;
        [DataFieldAttribute("MFileInfo", "varbinary")]
        public string MFileInfo
        {
            get { return _MFileInfo; }
            set { _MFileInfo = value; }
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
        //private DateTime? _TimeOut;
        //[DataFieldAttribute("TimeOut", "DateTime")]
        //public DateTime? TimeOut
        //{
        //    get { return _TimeOut; }
        //    set { _TimeOut = value; }
        //}
        private string _validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }
        private string _FTimeOut;
        [DataFieldAttribute("FTimeOut", "datetime")]
        public string FTimeOut
        {
            get { return _FTimeOut; }
            set { _FTimeOut = value; }
        }
    }
}
