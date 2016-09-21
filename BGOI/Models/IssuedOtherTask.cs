using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class IssuedOtherTask
    {
        private string TaskNumber;
        private string JoinPerson;
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

        [DataFieldAttribute("JoinPerson", "nvarchar")]
        public string StrJoinPerson
        {
            get { return JoinPerson; }
            set { JoinPerson = value; }
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
