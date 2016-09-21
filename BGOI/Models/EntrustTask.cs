using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class EntrustTask
    {
        private string TaskID;
        private string ProjectNum;
        private string TaskName;
        private string TaskPlace;
        private string DrawingNum;
        private string StartAEnd;
        private string TaskDesc;
        private string TellsbTime;
        private string TellsbPlace;
        private string JoinBuidUnit;
        private string JoinAboutUnit;
        private string BuildUnit;
        private string BuildPerson;
        private string BuildPhone;
        private string DsignUnit;
        private string DsignPerson;
        private string DsignPhone;
        private string WorkUnit;
        private string WorkPerson;
        private string WorkPhone;
        private string VisorUnit;
        private string VisorPerson;
        private string VisorPhone;
        private string WatchUnit;
        private string WatchPerson;
        private string WatchPhone;
        private string ProtectUnit;
        private string ProtectPerson;
        private string ProtectPhone;
        private string EleVisitUnit;
        private string EleVisitPerson;
        private string EleVisitPhone;
        private string MeasureUnit;
        private string MeasurePerson;
        private string MeasurePhone;
        private string DescT;
        private string PlanUser;
        private string PlanUserTel;
        private string Minster;
        private string CreateUnit;
        private DateTime? CreateTime;
        private string FinishTime;
        private string FinishUser;
        private int State;
        private string Validate;

        [DataFieldAttribute("TaskID", "varchar")]
        public string StrTaskID
        {
            get { return TaskID; }
            set { TaskID = value; }
        }

        [DataFieldAttribute("ProjectNum", "varchar")]
        public string StrProjectNum
        {
            get { return ProjectNum; }
            set { ProjectNum = value; }
        }

        [DataFieldAttribute("TaskName", "nvarchar")]
        public string StrTaskName
        {
            get { return TaskName; }
            set { TaskName = value; }
        }

        [DataFieldAttribute("TaskPlace", "nvarchar")]
        public string StrTaskPlace
        {
            get { return TaskPlace; }
            set { TaskPlace = value; }
        }

        [DataFieldAttribute("DrawingNum", "varchar")]
        public string StrDrawingNum
        {
            get { return DrawingNum; }
            set { DrawingNum = value; }
        }

        [DataFieldAttribute("StartAEnd", "nvarchar")]
        public string StrStartAEnd
        {
            get { return StartAEnd; }
            set { StartAEnd = value; }
        }

        [DataFieldAttribute("TaskDesc", "nvarchar")]
        public string StrTaskDesc
        {
            get { return TaskDesc; }
            set { TaskDesc = value; }
        }

        [DataFieldAttribute("TellsbTime", "DateTime")]
        public string StrTellsbTime
        {
            get { return TellsbTime; }
            set { TellsbTime = value; }
        }

        [DataFieldAttribute("TellsbPlace", "varchar")]
        public string StrTellsbPlace
        {
            get { return TellsbPlace; }
            set { TellsbPlace = value; }
        }

        [DataFieldAttribute("JoinBuidUnit", "nvarchar")]
        public string StrJoinBuidUnit
        {
            get { return JoinBuidUnit; }
            set { JoinBuidUnit = value; }
        }

        [DataFieldAttribute("JoinAboutUnit", "nvarchar")]
        public string StrJoinAboutUnit
        {
            get { return JoinAboutUnit; }
            set { JoinAboutUnit = value; }
        }

        [DataFieldAttribute("BuildUnit", "varchar")]
        public string StrBuildUnit
        {
            get { return BuildUnit; }
            set { BuildUnit = value; }
        }

        [DataFieldAttribute("BuildPerson", "varchar")]
        public string StrBuildPerson
        {
            get { return BuildPerson; }
            set { BuildPerson = value; }
        }

        [DataFieldAttribute("BuildPhone", "varchar")]
        public string StrBuildPhone
        {
            get { return BuildPhone; }
            set { BuildPhone = value; }
        }

        [DataFieldAttribute("DsignUnit", "varchar")]
        public string StrDsignUnit
        {
            get { return DsignUnit; }
            set { DsignUnit = value; }
        }

        [DataFieldAttribute("DsignPerson", "varchar")]
        public string StrDsignPerson
        {
            get { return DsignPerson; }
            set { DsignPerson = value; }
        }

        [DataFieldAttribute("DsignPhone", "varchar")]
        public string StrDsignPhone
        {
            get { return DsignPhone; }
            set { DsignPhone = value; }
        }

        [DataFieldAttribute("WorkUnit", "varchar")]
        public string StrWorkUnit
        {
            get { return WorkUnit; }
            set { WorkUnit = value; }
        }

        [DataFieldAttribute("WorkPerson", "varchar")]
        public string StrWorkPerson
        {
            get { return WorkPerson; }
            set { WorkPerson = value; }
        }

        [DataFieldAttribute("WorkPhone", "varchar")]
        public string StrWorkPhone
        {
            get { return WorkPhone; }
            set { WorkPhone = value; }
        }

        [DataFieldAttribute("VisorUnit", "varchar")]
        public string StrVisorUnit
        {
            get { return VisorUnit; }
            set { VisorUnit = value; }
        }

        [DataFieldAttribute("VisorPerson", "varchar")]
        public string StrVisorPerson
        {
            get { return VisorPerson; }
            set { VisorPerson = value; }
        }

        [DataFieldAttribute("VisorPhone", "varchar")]
        public string StrVisorPhone
        {
            get { return VisorPhone; }
            set { VisorPhone = value; }
        }

        [DataFieldAttribute("WatchUnit", "varchar")]
        public string StrWatchUnit
        {
            get { return WatchUnit; }
            set { WatchUnit = value; }
        }

        [DataFieldAttribute("WatchPerson", "varchar")]
        public string StrWatchPerson
        {
            get { return WatchPerson; }
            set { WatchPerson = value; }
        }

        [DataFieldAttribute("WatchPhone", "varchar")]
        public string StrWatchPhone
        {
            get { return WatchPhone; }
            set { WatchPhone = value; }
        }

        [DataFieldAttribute("ProtectUnit", "varchar")]
        public string StrProtectUnit
        {
            get { return ProtectUnit; }
            set { ProtectUnit = value; }
        }

        [DataFieldAttribute("ProtectPerson", "varchar")]
        public string StrProtectPerson
        {
            get { return ProtectPerson; }
            set { ProtectPerson = value; }
        }

        [DataFieldAttribute("ProtectPhone", "varchar")]
        public string StrProtectPhone
        {
            get { return ProtectPhone; }
            set { ProtectPhone = value; }
        }

        [DataFieldAttribute("EleVisitUnit", "varchar")]
        public string StrEleVisitUnit
        {
            get { return EleVisitUnit; }
            set { EleVisitUnit = value; }
        }

        [DataFieldAttribute("EleVisitPerson", "varchar")]
        public string StrEleVisitPerson
        {
            get { return EleVisitPerson; }
            set { EleVisitPerson = value; }
        }

        [DataFieldAttribute("EleVisitPhone", "varchar")]
        public string StrEleVisitPhone
        {
            get { return EleVisitPhone; }
            set { EleVisitPhone = value; }
        }

        [DataFieldAttribute("MeasureUnit", "varchar")]
        public string StrMeasureUnit
        {
            get { return MeasureUnit; }
            set { MeasureUnit = value; }
        }

        [DataFieldAttribute("MeasurePerson", "varchar")]
        public string StrMeasurePerson
        {
            get { return MeasurePerson; }
            set { MeasurePerson = value; }
        }

        [DataFieldAttribute("MeasurePhone", "varchar")]
        public string StrMeasurePhone
        {
            get { return MeasurePhone; }
            set { MeasurePhone = value; }
        }

        [DataFieldAttribute("DescT", "nvarchar")]
        public string StrDescT
        {
            get { return DescT; }
            set { DescT = value; }
        }

        [DataFieldAttribute("PlanUser", "varchar")]
        public string StrPlanUser
        {
            get { return PlanUser; }
            set { PlanUser = value; }
        }

        [DataFieldAttribute("PlanUserTel", "varchar")]
        public string StrPlanUserTel
        {
            get { return PlanUserTel; }
            set { PlanUserTel = value; }
        }

        [DataFieldAttribute("Minster", "varchar")]
        public string StrMinster
        {
            get { return Minster; }
            set { Minster = value; }
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

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
