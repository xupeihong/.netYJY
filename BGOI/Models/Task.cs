using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Task
    {
        private string DetectID;
        private string TaskNumber;
        private string WorkAmount;
        private string DeceteTime;
        private string TaskType;
        private int State;
        private string CreateUser;
        private string CreateUnit;
        private DateTime? CreateTime;
        private string FinishTime;
        private string FinishUser;
        private string Validate;

        [DataFieldAttribute("DetectID", "varchar")]
        public string StrDetectID
        {
            get { return DetectID; }
            set { DetectID = value; }
        }

        [DataFieldAttribute("TaskNumber", "varchar")]
        public string StrTaskNumber
        {
            get { return TaskNumber; }
            set { TaskNumber = value; }
        }

        [DataFieldAttribute("WorkAmount", "varchar")]
        public string StrWorkAmount
        {
            get { return WorkAmount; }
            set { WorkAmount = value; }
        }

        [DataFieldAttribute("DeceteTime", "date")]
        public string StrDeceteTime
        {
            get { return DeceteTime; }
            set { DeceteTime = value; }
        }

        [DataFieldAttribute("TaskType", "varchar")]
        public string StrTaskType
        {
            get { return TaskType; }
            set { TaskType = value; }
        }

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("CreateUnit", "varchar")]
        public string StrCreateUnit
        {
            get { return CreateUnit; }
            set { CreateUnit = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("FinishTime", "date")]
        public string StrFinishTime
        {
            get { return FinishTime; }
            set { FinishTime = value; }
        }

        [DataFieldAttribute("FinishUser", "varchar")]
        public string StrFinishUser
        {
            get { return FinishUser; }
            set { FinishUser = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
