using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SProducts
    {
        private string _ID;
        [DataFieldAttribute("ID", "varchar")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _ptype;
        [DataFieldAttribute("Ptype", "nvarchar")]
        public string Ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        private string _productid;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "产品编号不能超过20个字符")]
        [DataFieldAttribute("Productid", "varchar")]
        public string Productid
        {
            get { return _productid; }
            set { _productid = value; }
        }
        private string _productname;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "产品名称不能超过20个字符")]
        [DataFieldAttribute("Productname", "nvarchar")]
        public string Productname
        {
            get { return _productname; }
            set { _productname = value; }
        }
        private string _standard;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "规格类型不能超过20个字符")]
        [DataFieldAttribute("Standard", "nvarchar")]
        public string Standard
        {
            get { return _standard; }
            set { _standard = value; }
        }
        private string _measureunit;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "单位不能超过20个字符")]
        [DataFieldAttribute("Measureunit", "nvarchar")]
        public string Measureunit
        {
            get { return _measureunit; }
            set { _measureunit = value; }
        }
        private string _detaildesc;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "详细说明不能超过20个字符")]
        [DataFieldAttribute("Detaildesc", "nvarchar")]
        public string Detaildesc
        {
            get { return _detaildesc; }
            set { _detaildesc = value; }
        }
        private string _price;
        [DataFieldAttribute("Price", "decimal")]
        [RegularExpression(@"^[0-9]+(.[0-9]{2})?$", ErrorMessage = "请输入数字")] //^[0-9]*$
        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private string _originplace;
        [DataFieldAttribute("Originplace", "nvarchar")]
        public string Originplace
        {
            get { return _originplace; }
            set { _originplace = value; }
        }
        private string _BYTtime;
        [DataFieldAttribute("BYTtime", "DateTime")]
        public string BYTtime
        {
            get { return _BYTtime; }
            set { _BYTtime = value; }
        }
        private string _FFileName;
        [DataFieldAttribute("FFileName", "nvarchar")]
        public string FFileName
        {
            get { return _FFileName; }
            set { _FFileName = value; }
        }
       
        private string _FileInfo;
        [DataFieldAttribute("FileInfo", "varbinary")]
        public string FileInfo
        {
            get { return _FileInfo; }
            set { _FileInfo = value; }
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

    }
}
