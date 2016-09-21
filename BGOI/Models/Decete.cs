using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Decete
    {
        private string TaskID;
        private string DetectID;
        private string TaskName;
        private string WorkUnit;
        private string EntrustUnit;
        private string DetectPlace;
        private string LivePerson;
        private string LivePhone;
        private string PieceCont;
        private string PieceTexture;
        private string Executive;
        private string Technicalgrade;
        private string HegeLevel;
        private string DetectScale;
        private string DetectPart;
        private string WorkAmount;
        private string DeceteType;
        private string CreateUser;
        private string CreateUnit;
        private DateTime? CreateTime;
        private string FinishTime;
        private string FinishUser;
        private string Validate;
        private int State;
        private int IsCard;

        [DataFieldAttribute("TaskID", "varchar")]
        public string StrTaskID
        {
            get { return TaskID; }
            set { TaskID = value; }
        }

        [DataFieldAttribute("DetectID", "varchar")]
        public string StrDetectID
        {
            get { return DetectID; }
            set { DetectID = value; }
        }

        [DataFieldAttribute("TaskName", "nvarchar")]
        public string StrTaskName
        {
            get { return TaskName; }
            set { TaskName = value; }
        }

        [DataFieldAttribute("WorkUnit", "varchar")]
        public string StrWorkUnit
        {
            get { return WorkUnit; }
            set { WorkUnit = value; }
        }

        [DataFieldAttribute("EntrustUnit", "varchar")]
        public string StrEntrustUnit
        {
            get { return EntrustUnit; }
            set { EntrustUnit = value; }
        }

        [DataFieldAttribute("DetectPlace", "nvarchar")]
        public string StrDetectPlace
        {
            get { return DetectPlace; }
            set { DetectPlace = value; }
        }

        [DataFieldAttribute("LivePerson", "varchar")]
        public string StrLivePerson
        {
            get { return LivePerson; }
            set { LivePerson = value; }
        }

        [DataFieldAttribute("LivePhone", "varchar")]
        public string StrLivePhone
        {
            get { return LivePhone; }
            set { LivePhone = value; }
        }

        [DataFieldAttribute("PieceCont", "varchar")]
        public string StrPieceCont
        {
            get { return PieceCont; }
            set { PieceCont = value; }
        }

        [DataFieldAttribute("PieceTexture", "varchar")]
        public string StrPieceTexture
        {
            get { return PieceTexture; }
            set { PieceTexture = value; }
        }

        [DataFieldAttribute("Executive", "varchar")]
        public string StrExecutive
        {
            get { return Executive; }
            set { Executive = value; }
        }

        [DataFieldAttribute("Technicalgrade", "varchar")]
        public string StrTechnicalgrade
        {
            get { return Technicalgrade; }
            set { Technicalgrade = value; }
        }

        [DataFieldAttribute("HegeLevel", "varchar")]
        public string StrHegeLevel
        {
            get { return HegeLevel; }
            set { HegeLevel = value; }
        }

        [DataFieldAttribute("DetectScale", "varchar")]
        public string StrDetectScale
        {
            get { return DetectScale; }
            set { DetectScale = value; }
        }

        [DataFieldAttribute("DetectPart", "varchar")]
        public string StrDetectPart
        {
            get { return DetectPart; }
            set { DetectPart = value; }
        }

        [DataFieldAttribute("WorkAmount", "varchar")]
        public string StrWorkAmount
        {
            get { return WorkAmount; }
            set { WorkAmount = value; }
        }

        [DataFieldAttribute("DeceteType", "varchar")]
        public string StrDeceteType
        {
            get { return DeceteType; }
            set { DeceteType = value; }
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

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
        }

        [DataFieldAttribute("IsCard", "int")]
        public int StrIsCard
        {
            get { return IsCard; }
            set { IsCard = value; }
        }
    }
}
