using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class RativeSource
    {
        private string RID;
        private string EquipID;
        private string ProModel;
        private string Source;
        private string Manufacturer;
        private string ChangeTime;
        private string Nominal;
        private string SourceNumber;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        [DataFieldAttribute("RID", "varchar")]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [DataFieldAttribute("EquipID", "varchar")]
        public string StrEquipID
        {
            get { return EquipID; }
            set { EquipID = value; }
        }

        [DataFieldAttribute("ProModel", "varchar")]
        public string StrProModel
        {
            get { return ProModel; }
            set { ProModel = value; }
        }

        [DataFieldAttribute("Source", "varchar")]
        public string StrSource
        {
            get { return Source; }
            set { Source = value; }
        }

        [DataFieldAttribute("Manufacturer", "nvarchar")]
        public string StrManufacturer
        {
            get { return Manufacturer; }
            set { Manufacturer = value; }
        }

        [DataFieldAttribute("ChangeTime", "date")]
        public string StrChangeTime
        {
            get { return ChangeTime; }
            set { ChangeTime = value; }
        }

        [DataFieldAttribute("Nominal", "nvarchar")]
        public string StrNominal
        {
            get { return Nominal; }
            set { Nominal = value; }
        }

        [DataFieldAttribute("SourceNumber", "nvarchar")]
        public string StrSourceNumber
        {
            get { return SourceNumber; }
            set { SourceNumber = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
