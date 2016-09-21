using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class IssuedTaskRT
    {
        private string TaskNumber;
        private string EscortPerson;
        private string MainPerson;
        private string AssistPerson;
        private string OtherPerson;
        private string LeadPerson;
        private string ShootingPerson;
        private string EquipType;
        private string EquipModel;
        private string CreateUser;
        private DateTime? CreateTime;
        private string Validate;

        [DataFieldAttribute("TaskNumber", "varchar")]
        public string StrTaskNumber
        {
            get { return TaskNumber; }
            set { TaskNumber = value; }
        }

        [DataFieldAttribute("EscortPerson", "varchar")]
        public string StrEscortPerson
        {
            get { return EscortPerson; }
            set { EscortPerson = value; }
        }

        [DataFieldAttribute("MainPerson", "varchar")]
        public string StrMainPerson
        {
            get { return MainPerson; }
            set { MainPerson = value; }
        }

        [DataFieldAttribute("AssistPerson", "varchar")]
        public string StrAssistPerson
        {
            get { return AssistPerson; }
            set { AssistPerson = value; }
        }

        [DataFieldAttribute("OtherPerson", "nvarchar")]
        public string StrOtherPerson
        {
            get { return OtherPerson; }
            set { OtherPerson = value; }
        }

        [DataFieldAttribute("LeadPerson", "varchar")]
        public string StrLeadPerson
        {
            get { return LeadPerson; }
            set { LeadPerson = value; }
        }

        [DataFieldAttribute("ShootingPerson", "nvarchar")]
        public string StrShootingPerson
        {
            get { return ShootingPerson; }
            set { ShootingPerson = value; }
        }

        [DataFieldAttribute("EquipType", "varchar")]
        public string StrEquipType
        {
            get { return EquipType; }
            set { EquipType = value; }
        }

        [DataFieldAttribute("EquipModel", "varchar")]
        public string StrEquipModel
        {
            get { return EquipModel; }
            set { EquipModel = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
