using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_WorkCardUT
    {
        private string WID;
        private string RID;
        private string RepairUser;
        private string IsCheck;
        private string CheckResult;
        private string CirNum;
        private string NewCirNum;
        private string ChangePlace;
        private string CirVersion;
        private string NewCirVersion;
        private string PreVersion;
        private string NewVersion;
        private string PreData;
        private string NewData;
        private string ProbePlace1;
        private string ProbeID1;
        private string NewProbePlace1;
        private string NewProbeID1;
        private string Check1;
        private string ProbePlace2;
        private string ProbeID2;
        private string NewProbePlace2;
        private string NewProbeID2;
        private string Check2;
        private string ProbePlace3;
        private string ProbeID3;
        private string NewProbePlace3;
        private string NewProbeID3;
        private string Check3;
        private string ProbePlace4;
        private string ProbeID4;
        private string NewProbePlace4;
        private string NewProbeID4;
        private string Check4;
        private string ProbePlace5;
        private string ProbeID5;
        private string NewProbePlace5;
        private string NewProbeID5;
        private string Check5;
        private string ProbePlace6;
        private string ProbeID6;
        private string NewProbePlace6;
        private string NewProbeID6;
        private string Check6;
        private string ProbePlace7;
        private string ProbeID7;
        private string NewProbePlace7;
        private string NewProbeID7;
        private string Check7;
        private string ProbePlace8;
        private string ProbeID8;
        private string NewProbePlace8;
        private string NewProbeID8;
        private string Check8;
        private string ProbeOut;
        private string RepairContent1;
        private string ProbeUnder;
        private string RepairContent2;
        private string ProbeUnit;
        private string RepairContent3;
        private string Suppress;
        private string SpecialTool;
        private string Coordinate;
        private string AcceptUser;
        private string AcceptDate;
        private string Validate;
        
        public tk_WorkCardUT()
        {

            //TODO: 在此处添加构造函数逻辑

        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("WID", "varchar")]
        public string strWID
        {
            get { return WID; }
            set { WID = value; }
        }

        [DataFieldAttribute("RID", "varchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [DataFieldAttribute("RepairUser", "varchar")]
        public string strRepairUser
        {
            get { return RepairUser; }
            set { RepairUser = value; }
        }

        [DataFieldAttribute("IsCheck", "nvarchar")]
        public string strIsCheck
        {
            get { return IsCheck; }
            set { IsCheck = value; }
        }

        [DataFieldAttribute("CheckResult", "nvarchar")]
        public string strCheckResult
        {
            get { return CheckResult; }
            set { CheckResult = value; }
        }

        [DataFieldAttribute("CirNum", "nvarchar")]
        public string strCirNum
        {
            get { return CirNum; }
            set { CirNum = value; }
        }

        [DataFieldAttribute("NewCirNum", "nvarchar")]
        public string strNewCirNum
        {
            get { return NewCirNum; }
            set { NewCirNum = value; }
        }

        [DataFieldAttribute("ChangePlace", "nvarchar")]
        public string strChangePlace
        {
            get { return ChangePlace; }
            set { ChangePlace = value; }
        }

        [DataFieldAttribute("CirVersion", "nvarchar")]
        public string strCirVersion
        {
            get { return CirVersion; }
            set { CirVersion = value; }
        }

        [DataFieldAttribute("NewCirVersion", "nvarchar")]
        public string strNewCirVersion
        {
            get { return NewCirVersion; }
            set { NewCirVersion = value; }
        }

        [DataFieldAttribute("PreVersion", "nvarchar")]
        public string strPreVersion
        {
            get { return PreVersion; }
            set { PreVersion = value; }
        }

        [DataFieldAttribute("NewVersion", "nvarchar")]
        public string strNewVersion
        {
            get { return NewVersion; }
            set { NewVersion = value; }
        }

        [DataFieldAttribute("PreData", "nvarchar")]
        public string strPreData
        {
            get { return PreData; }
            set { PreData = value; }
        }

        [DataFieldAttribute("NewData", "nvarchar")]
        public string strNewData
        {
            get { return NewData; }
            set { NewData = value; }
        }

        [DataFieldAttribute("ProbePlace1", "nvarchar")]
        public string strProbePlace1
        {
            get { return ProbePlace1; }
            set { ProbePlace1 = value; }
        }

        [DataFieldAttribute("ProbeID1", "nvarchar")]
        public string strProbeID1
        {
            get { return ProbeID1; }
            set { ProbeID1 = value; }
        }

        [DataFieldAttribute("NewProbePlace1", "nvarchar")]
        public string strNewProbePlace1
        {
            get { return NewProbePlace1; }
            set { NewProbePlace1 = value; }
        }

        [DataFieldAttribute("NewProbeID1", "nvarchar")]
        public string strNewProbeID1
        {
            get { return NewProbeID1; }
            set { NewProbeID1 = value; }
        }

        [DataFieldAttribute("Check1", "nvarchar")]
        public string strCheck1
        {
            get { return Check1; }
            set { Check1 = value; }
        }

        [DataFieldAttribute("ProbePlace2", "nvarchar")]
        public string strProbePlace2
        {
            get { return ProbePlace2; }
            set { ProbePlace2 = value; }
        }

        [DataFieldAttribute("ProbeID2", "nvarchar")]
        public string strProbeID2
        {
            get { return ProbeID2; }
            set { ProbeID2 = value; }
        }

        [DataFieldAttribute("NewProbePlace2", "nvarchar")]
        public string strNewProbePlace2
        {
            get { return NewProbePlace2; }
            set { NewProbePlace2 = value; }
        }

        [DataFieldAttribute("NewProbeID2", "nvarchar")]
        public string strNewProbeID2
        {
            get { return NewProbeID2; }
            set { NewProbeID2 = value; }
        }

        [DataFieldAttribute("Check2", "nvarchar")]
        public string strCheck2
        {
            get { return Check2; }
            set { Check2 = value; }
        }

        [DataFieldAttribute("ProbePlace3", "nvarchar")]
        public string strProbePlace3
        {
            get { return ProbePlace3; }
            set { ProbePlace3 = value; }
        }

        [DataFieldAttribute("ProbeID3", "nvarchar")]
        public string strProbeID3
        {
            get { return ProbeID3; }
            set { ProbeID3 = value; }
        }

        [DataFieldAttribute("NewProbePlace3", "nvarchar")]
        public string strNewProbePlace3
        {
            get { return NewProbePlace3; }
            set { NewProbePlace3 = value; }
        }

        [DataFieldAttribute("NewProbeID3", "nvarchar")]
        public string strNewProbeID3
        {
            get { return NewProbeID3; }
            set { NewProbeID3 = value; }
        }

        [DataFieldAttribute("Check3", "nvarchar")]
        public string strCheck3
        {
            get { return Check3; }
            set { Check3 = value; }
        }

        [DataFieldAttribute("ProbePlace4", "nvarchar")]
        public string strProbePlace4
        {
            get { return ProbePlace4; }
            set { ProbePlace4 = value; }
        }

        [DataFieldAttribute("ProbeID4", "nvarchar")]
        public string strProbeID4
        {
            get { return ProbeID4; }
            set { ProbeID4 = value; }
        }

        [DataFieldAttribute("NewProbePlace4", "nvarchar")]
        public string strNewProbePlace4
        {
            get { return NewProbePlace4; }
            set { NewProbePlace4 = value; }
        }

        [DataFieldAttribute("NewProbeID4", "nvarchar")]
        public string strNewProbeID4
        {
            get { return NewProbeID4; }
            set { NewProbeID4 = value; }
        }

        [DataFieldAttribute("Check4", "nvarchar")]
        public string strCheck4
        {
            get { return Check4; }
            set { Check4 = value; }
        }

        [DataFieldAttribute("ProbePlace5", "nvarchar")]
        public string strProbePlace5
        {
            get { return ProbePlace5; }
            set { ProbePlace5 = value; }
        }

        [DataFieldAttribute("ProbeID5", "nvarchar")]
        public string strProbeID5
        {
            get { return ProbeID5; }
            set { ProbeID5 = value; }
        }

        [DataFieldAttribute("NewProbePlace5", "nvarchar")]
        public string strNewProbePlace5
        {
            get { return NewProbePlace5; }
            set { NewProbePlace5 = value; }
        }

        [DataFieldAttribute("NewProbeID5", "nvarchar")]
        public string strNewProbeID5
        {
            get { return NewProbeID5; }
            set { NewProbeID5 = value; }
        }

        [DataFieldAttribute("Check5", "nvarchar")]
        public string strCheck5
        {
            get { return Check5; }
            set { Check5 = value; }
        }

        [DataFieldAttribute("ProbePlace6", "nvarchar")]
        public string strProbePlace6
        {
            get { return ProbePlace6; }
            set { ProbePlace6 = value; }
        }

        [DataFieldAttribute("ProbeID6", "nvarchar")]
        public string strProbeID6
        {
            get { return ProbeID6; }
            set { ProbeID6 = value; }
        }

        [DataFieldAttribute("NewProbePlace6", "nvarchar")]
        public string strNewProbePlace6
        {
            get { return NewProbePlace6; }
            set { NewProbePlace6 = value; }
        }

        [DataFieldAttribute("NewProbeID6", "nvarchar")]
        public string strNewProbeID6
        {
            get { return NewProbeID6; }
            set { NewProbeID6 = value; }
        }

        [DataFieldAttribute("Check6", "nvarchar")]
        public string strCheck6
        {
            get { return Check6; }
            set { Check6 = value; }
        }

        [DataFieldAttribute("ProbePlace7", "nvarchar")]
        public string strProbePlace7
        {
            get { return ProbePlace7; }
            set { ProbePlace7 = value; }
        }

        [DataFieldAttribute("ProbeID7", "nvarchar")]
        public string strProbeID7
        {
            get { return ProbeID7; }
            set { ProbeID7 = value; }
        }

        [DataFieldAttribute("NewProbePlace7", "nvarchar")]
        public string strNewProbePlace7
        {
            get { return NewProbePlace7; }
            set { NewProbePlace7 = value; }
        }

        [DataFieldAttribute("NewProbeID7", "nvarchar")]
        public string strNewProbeID7
        {
            get { return NewProbeID7; }
            set { NewProbeID7 = value; }
        }

        [DataFieldAttribute("Check7", "nvarchar")]
        public string strCheck7
        {
            get { return Check7; }
            set { Check7 = value; }
        }

        [DataFieldAttribute("ProbePlace8", "nvarchar")]
        public string strProbePlace8
        {
            get { return ProbePlace8; }
            set { ProbePlace8 = value; }
        }

        [DataFieldAttribute("ProbeID8", "nvarchar")]
        public string strProbeID8
        {
            get { return ProbeID8; }
            set { ProbeID8 = value; }
        }

        [DataFieldAttribute("NewProbePlace8", "nvarchar")]
        public string strNewProbePlace8
        {
            get { return NewProbePlace8; }
            set { NewProbePlace8 = value; }
        }

        [DataFieldAttribute("NewProbeID8", "nvarchar")]
        public string strNewProbeID8
        {
            get { return NewProbeID8; }
            set { NewProbeID8 = value; }
        }

        [DataFieldAttribute("Check8", "nvarchar")]
        public string strCheck8
        {
            get { return Check8; }
            set { Check8 = value; }
        }

        [DataFieldAttribute("ProbeOut", "nvarchar")]
        public string strProbeOut
        {
            get { return ProbeOut; }
            set { ProbeOut = value; }
        }

        [DataFieldAttribute("RepairContent1", "nvarchar")]
        public string strRepairContent1
        {
            get { return RepairContent1; }
            set { RepairContent1 = value; }
        }

        [DataFieldAttribute("ProbeUnder", "nvarchar")]
        public string strProbeUnder
        {
            get { return ProbeUnder; }
            set { ProbeUnder = value; }
        }

        [DataFieldAttribute("RepairContent2", "nvarchar")]
        public string strRepairContent2
        {
            get { return RepairContent2; }
            set { RepairContent2 = value; }
        }

        [DataFieldAttribute("ProbeUnit", "nvarchar")]
        public string strProbeUnit
        {
            get { return ProbeUnit; }
            set { ProbeUnit = value; }
        }

        [DataFieldAttribute("RepairContent3", "nvarchar")]
        public string strRepairContent3
        {
            get { return RepairContent3; }
            set { RepairContent3 = value; }
        }

        [DataFieldAttribute("Suppress", "nvarchar")]
        public string strSuppress
        {
            get { return Suppress; }
            set { Suppress = value; }
        }

        [DataFieldAttribute("SpecialTool", "nvarchar")]
        public string strSpecialTool
        {
            get { return SpecialTool; }
            set { SpecialTool = value; }
        }

        [DataFieldAttribute("Coordinate", "nvarchar")]
        public string strCoordinate
        {
            get { return Coordinate; }
            set { Coordinate = value; }
        }

        [DataFieldAttribute("AcceptUser", "nvarchar")]
        public string strAcceptUser
        {
            get { return AcceptUser; }
            set { AcceptUser = value; }
        }

        [DataFieldAttribute("AcceptDate", "nvarchar")]
        public string strAcceptDate
        {
            get { return AcceptDate; }
            set { AcceptDate = value; }
        }

        [DataFieldAttribute("Validate", "nvarchar")]
        public string strValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }


    }
}
