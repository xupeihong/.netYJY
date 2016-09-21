using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SFileInfoHis
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _ftype;
        [DataFieldAttribute("Ftype", "nvarchar")]
        public string Ftype
        {
            get { return _ftype; }
            set { _ftype = value; }
        }
        private string _typeo;
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
        private string _filetype;
        [DataFieldAttribute("Filetype", "varchar")]
        public string Filetype
        {
            get { return _filetype; }
            set { _filetype = value; }
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
        private string _NCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return _NCreateUser; }
            set { _NCreateUser = value; }
        }
        private DateTime? _NCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return _NCreateTime; }
            set { _NCreateTime = value; }
        }
    }
}
