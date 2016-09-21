using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_WorkCard
    {
        private string WID;
        private string RID;
        private string RepairUser;
        private string CheckUser;
        private string RepairDate;
        private string I_Qmin;
        private string I_2Qmax;
        private string I_4Qmax;
        private string I_Qmax;
        private string I_Repeat1;
        private string I_Repeat2;
        private string I_Repeat3;
        private string I_Repeat4;

        private string DescripComments;
        private string IsRepairY;
        private string YChangeBak;
        private string YUnChangeBak;
        private string TotalCheck;
        private string RepairGroup;
        private string IsRepairN;
        private string NChangeBak;
        private string NUnChangeBak;
        private string TotalCheck2;
        private string RepairGroup2;
        private string O_Qmin;
        private string O_2Qmax;
        private string O_4Qmax;
        private string O_Qmax;
        private string O_Repeat1;
        private string O_Repeat2;
        private string O_Repeat3;
        private string O_Repeat4;

        private string RepairPerson;
        private string StockOut;
        private string OutCheck;
        private string PressHege;
        private string MainCheck;
        private string O_Date;

        private string Validate;

        public tk_WorkCard()
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

        [DataFieldAttribute("CheckUser", "nvarchar")]
        public string strCheckUser
        {
            get { return CheckUser; }
            set { CheckUser = value; }
        }

        [DataFieldAttribute("RepairDate", "nvarchar")]
        public string strRepairDate
        {
            get { return RepairDate; }
            set { RepairDate = value; }
        }

        [DataFieldAttribute("I_Qmin", "nvarchar")]
        public string strI_Qmin
        {
            get { return I_Qmin; }
            set { I_Qmin = value; }
        }

        [DataFieldAttribute("I_2Qmax", "nvarchar")]
        public string strI_2Qmax
        {
            get { return I_2Qmax; }
            set { I_2Qmax = value; }
        }

        [DataFieldAttribute("I_4Qmax", "nvarchar")]
        public string strI_4Qmax
        {
            get { return I_4Qmax; }
            set { I_4Qmax = value; }
        }

        [DataFieldAttribute("I_Qmax", "nvarchar")]
        public string strI_Qmax
        {
            get { return I_Qmax; }
            set { I_Qmax = value; }
        }

        [DataFieldAttribute("I_Repeat1", "nvarchar")]
        public string strI_Repeat1
        {
            get { return I_Repeat1; }
            set { I_Repeat1 = value; }
        }

        [DataFieldAttribute("I_Repeat2", "nvarchar")]
        public string strI_Repeat2
        {
            get { return I_Repeat2; }
            set { I_Repeat2 = value; }
        }

        [DataFieldAttribute("I_Repeat3", "nvarchar")]
        public string strI_Repeat3
        {
            get { return I_Repeat3; }
            set { I_Repeat3 = value; }
        }

        [DataFieldAttribute("I_Repeat4", "nvarchar")]
        public string strI_Repeat4
        {
            get { return I_Repeat4; }
            set { I_Repeat4 = value; }
        }

        [DataFieldAttribute("DescripComments", "nvarchar")]
        public string strDescripComments
        {
            get { return DescripComments; }
            set { DescripComments = value; }
        }

        [DataFieldAttribute("IsRepairY", "nvarchar")]
        public string strIsRepairY
        {
            get { return IsRepairY; }
            set { IsRepairY = value; }
        }

        [DataFieldAttribute("YChangeBak", "nvarchar")]
        public string strYChangeBak
        {
            get { return YChangeBak; }
            set { YChangeBak = value; }
        }

        [DataFieldAttribute("YUnChangeBak", "nvarchar")]
        public string strYUnChangeBak
        {
            get { return YUnChangeBak; }
            set { YUnChangeBak = value; }
        }

        [DataFieldAttribute("IsRepairN", "nvarchar")]
        public string strIsRepairN
        {
            get { return IsRepairN; }
            set { IsRepairN = value; }
        }

        [DataFieldAttribute("NChangeBak", "nvarchar")]
        public string strNChangeBak
        {
            get { return NChangeBak; }
            set { NChangeBak = value; }
        }

        [DataFieldAttribute("NUnChangeBak", "nvarchar")]
        public string strNUnChangeBak
        {
            get { return NUnChangeBak; }
            set { NUnChangeBak = value; }
        }

        [DataFieldAttribute("TotalCheck", "nvarchar")]
        public string strTotalCheck
        {
            get { return TotalCheck; }
            set { TotalCheck = value; }
        }

        [DataFieldAttribute("RepairGroup", "nvarchar")]
        public string strRepairGroup
        {
            get { return RepairGroup; }
            set { RepairGroup = value; }
        }

        [DataFieldAttribute("TotalCheck2", "nvarchar")]
        public string strTotalCheck2
        {
            get { return TotalCheck2; }
            set { TotalCheck2 = value; }
        }

        [DataFieldAttribute("RepairGroup2", "nvarchar")]
        public string strRepairGroup2
        {
            get { return RepairGroup2; }
            set { RepairGroup2 = value; }
        }

        [DataFieldAttribute("O_Qmin", "nvarchar")]
        public string strO_Qmin
        {
            get { return O_Qmin; }
            set { O_Qmin = value; }
        }

        [DataFieldAttribute("O_2Qmax", "nvarchar")]
        public string strO_2Qmax
        {
            get { return O_2Qmax; }
            set { O_2Qmax = value; }
        }

        [DataFieldAttribute("O_4Qmax", "nvarchar")]
        public string strO_4Qmax
        {
            get { return O_4Qmax; }
            set { O_4Qmax = value; }
        }

        [DataFieldAttribute("O_Qmax", "nvarchar")]
        public string strO_Qmax
        {
            get { return O_Qmax; }
            set { O_Qmax = value; }
        }

        [DataFieldAttribute("O_Repeat1", "nvarchar")]
        public string strO_Repeat1
        {
            get { return O_Repeat1; }
            set { O_Repeat1 = value; }
        }

        [DataFieldAttribute("O_Repeat2", "nvarchar")]
        public string strO_Repeat2
        {
            get { return O_Repeat2; }
            set { O_Repeat2 = value; }
        }

        [DataFieldAttribute("O_Repeat3", "nvarchar")]
        public string strO_Repeat3
        {
            get { return O_Repeat3; }
            set { O_Repeat3 = value; }
        }

        [DataFieldAttribute("O_Repeat4", "nvarchar")]
        public string strO_Repeat4
        {
            get { return O_Repeat4; }
            set { O_Repeat4 = value; }
        }

        [DataFieldAttribute("RepairPerson", "nvarchar")]
        public string strRepairPerson
        {
            get { return RepairPerson; }
            set { RepairPerson = value; }
        }

        [DataFieldAttribute("StockOut", "nvarchar")]
        public string strStockOut
        {
            get { return StockOut; }
            set { StockOut = value; }
        }

        [DataFieldAttribute("OutCheck", "nvarchar")]
        public string strOutCheck
        {
            get { return OutCheck; }
            set { OutCheck = value; }
        }

        [DataFieldAttribute("PressHege", "nvarchar")]
        public string strPressHege
        {
            get { return PressHege; }
            set { PressHege = value; }
        }

        [DataFieldAttribute("MainCheck", "nvarchar")]
        public string strMainCheck
        {
            get { return MainCheck; }
            set { MainCheck = value; }
        }

        [DataFieldAttribute("O_Date", "nvarchar")]
        public string strO_Date
        {
            get { return O_Date; }
            set { O_Date = value; }
        }

        [DataFieldAttribute("Validate", "nvarchar")]
        public string strValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }

    }
}
