using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class DevicsBas
    {
        private string ControlCode;
        private string Ename;
        private string Manufacturer;
        private string FactoryNumber;
        private string DevicsType;
        private string FactoryDate;
        private string Specification;
        private string TracingType;
        private string Precision;
        private string Clrange;
        private int IsCycle;
        private string CycleType;
        private string Cycle;
        private string LastDate;
        private string PlanDate;
        private string CheckCompany;
        private string Remark;
        private int State;
        private string UnitID;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        [DataFieldAttribute("ControlCode", "varchar")]
        public string StrControlCode
        {
            get { return ControlCode; }
            set { ControlCode = value; }
        }

        [DataFieldAttribute("Ename", "nvarchar")]
        public string StrEname
        {
            get { return Ename; }
            set { Ename = value; }
        }

        [DataFieldAttribute("Manufacturer", "nvarchar")]
        public string StrManufacturer
        {
            get { return Manufacturer; }
            set { Manufacturer = value; }
        }

        [DataFieldAttribute("FactoryNumber", "nvarchar")]
        public string StrFactoryNumber
        {
            get { return FactoryNumber; }
            set { FactoryNumber = value; }
        }

        [DataFieldAttribute("Specification", "varchar")]
        public string StrSpecification
        {
            get { return Specification; }
            set { Specification = value; }
        }

        [DataFieldAttribute("DevicsType", "varchar")]
        public string StrDevicsType
        {
            get { return DevicsType; }
            set { DevicsType = value; }
        }

        [DataFieldAttribute("FactoryDate", "date")]
        public string StrFactoryDate
        {
            get { return FactoryDate; }
            set { FactoryDate = value; }
        }

        [DataFieldAttribute("Precision", "varchar")]
        public string StrPrecision
        {
            get { return Precision; }
            set { Precision = value; }
        }

        [DataFieldAttribute("TracingType", "varchar")]
        public string StrTracingType
        {
            get { return TracingType; }
            set { TracingType = value; }
        }

        [DataFieldAttribute("Clrange", "varchar")]
        public string StrClrange
        {
            get { return Clrange; }
            set { Clrange = value; }
        }

        [DataFieldAttribute("IsCycle", "int")]
        public int StrIsCycle
        {
            get { return IsCycle; }
            set { IsCycle = value; }
        }

        [DataFieldAttribute("CycleType", "nvarchar")]
        public string StrCycleType
        {
            get { return CycleType; }
            set { CycleType = value; }
        }

        [DataFieldAttribute("Cycle", "int")]
        public string StrCycle
        {
            get { return Cycle; }
            set { Cycle = value; }
        }

        [DataFieldAttribute("LastDate", "date")]
        public string StrLastDate
        {
            get { return LastDate; }
            set { LastDate = value; }
        }

        [DataFieldAttribute("PlanDate", "date")]
        public string StrPlanDate
        {
            get { return PlanDate; }
            set { PlanDate = value; }
        }

        [DataFieldAttribute("CheckCompany", "nvarchar")]
        public string StrCheckCompany
        {
            get { return CheckCompany; }
            set { CheckCompany = value; }
        }

        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
        }

        [DataFieldAttribute("UnitID", "varchar")]
        public string StrUnitID
        {
            get { return UnitID; }
            set { UnitID = value; }
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
