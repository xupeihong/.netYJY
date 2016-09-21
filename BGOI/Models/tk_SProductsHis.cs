using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SProductsHis
    {
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
        [DataFieldAttribute("Productid", "varchar")]
        public string Productid
        {
            get { return _productid; }
            set { _productid = value; }
        }
        private string _productname;
        [DataFieldAttribute("Productname", "nvarchar")]
        public string Productname
        {
            get { return _productname; }
            set { _productname = value; }
        }
        private string _standard;
        [DataFieldAttribute("Standard", "nvarchar")]
        public string Standard
        {
            get { return _standard; }
            set { _standard = value; }
        }
        private string _measureunit;
        [DataFieldAttribute("Measureunit", "nvarchar")]
        public string Measureunit
        {
            get { return _measureunit; }
            set { _measureunit = value; }
        }
        private string _detaildesc;
        [DataFieldAttribute("Detaildesc", "nvarchar")]
        public string Detaildesc
        {
            get { return _detaildesc; }
            set { _detaildesc = value; }
        }
        private decimal _price;
        [DataFieldAttribute("Price", "decimal")]
        public decimal Price
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
        //private string _purpose;
        //[DataFieldAttribute("Purpose", "nvarchar")]
        //public string Purpose
        //{
        //    get { return _purpose; }
        //    set { _purpose = value; }
        //}
        
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
        private string _ProcessUser;
        [DataFieldAttribute("ProcessUser", "varchar")]
        public string ProcessUser
        {
            get { return _ProcessUser; }
            set { _ProcessUser = value; }
        }
        private DateTime? _ProcessTime;
        [DataFieldAttribute("ProcessTime", "varchar")]
        public DateTime? ProcessTime
        {
            get { return _ProcessTime; }
            set { _ProcessTime = value; }
        }
    }
}
