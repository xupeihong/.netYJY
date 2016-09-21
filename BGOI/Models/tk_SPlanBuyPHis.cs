using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SPlanBuyPHis
    {
        private string _SID;
        [DataFieldAttribute("SID", "varchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _ProductID;
        [DataFieldAttribute("ProductID", "varchar")]
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        private decimal _ContractPrice;
        [DataFieldAttribute("ContractPrice", "decimal")]
        public decimal ContractPrice
        {
            get { return _ContractPrice; }
            set { _ContractPrice = value; }
        }
        private DateTime? _ContractValidity;
        [DataFieldAttribute("ContractValidity", "DateTime")]
        public DateTime? ContractValidity
        {
            get { return _ContractValidity; }
            set { _ContractValidity = value; }
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
        private string _ProcessUser;
        [DataFieldAttribute("ProcessUser", "varchar")]
        public string ProcessUser
        {
            get { return _ProcessUser; }
            set { _ProcessUser = value; }
        }
        private DateTime? _ProcessTime;
        [DataFieldAttribute("ProcessTime", "DateTime")]
        public DateTime? ProcessTime
        {
            get { return _ProcessTime; }
            set { _ProcessTime = value; }
        }
    }
}
