using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SServicePrice
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private long _serviceid;
        [DataFieldAttribute("Serviceid", "bigint")]
        public long Serviceid
        {
            get { return _serviceid; }
            set { _serviceid = value; }
        }
        private decimal _price;
        [DataFieldAttribute("Price", "decimal")]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private int _years;
        [DataFieldAttribute("Years", "int")]
        public int Years
        {
            get { return _years; }
            set { _years = value; }
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
