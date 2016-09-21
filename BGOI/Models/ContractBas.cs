using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class ContractBas
    {
        private string CID;
        private string ContractID;
        private string PID;
        private string Unit;
        private string BusinessType;
        private string Cname;
        private string ContractContent;
        private string CStartTime;
        private int TimeScale;
        private string CPlanEndTime;
        private decimal CBeginAmount;
        private decimal Margin;
        private decimal CEndAmount;
        private string CEndTime;
        private string Ctime;
        private int AmountNum;
        private int CurAmountNum;
        private string Principal;
        private string PartyA;
        private string PartyB;
        private string PayOrIncome;
        private int PageCount;
        private string Rmark;
        private int State;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;
        private decimal PContractAmount;
        private decimal PBudget;
        private decimal PCost;
        private decimal PProfit;

        [DataFieldAttribute("CID", "varchar")]
        public string StrCID
        {
            get { return CID; }
            set { CID = value; }
        }


        [Remote("JudgeSameContractID", " Contract", ErrorMessage = "该合同编号已存在,请重新填写")]
        [Required(ErrorMessage = "合同编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContractID", "varchar")]
        public string StrContractID
        {
            get { return ContractID; }
            set { ContractID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        [DataFieldAttribute("Unit", "varchar")]
        public string StrUnit
        {
            get { return Unit; }
            set { Unit = value; }
        }

        [DataFieldAttribute("BusinessType", "varchar")]
        public string StrBusinessType
        {
            get { return BusinessType; }
            set { BusinessType = value; }
        }

        [Required(ErrorMessage = "合同名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Cname", "nvarchar")]
        public string StrCname
        {
            get { return Cname; }
            set { Cname = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContractContent", "nvarchar")]
        public string StrContractContent
        {
            get { return ContractContent; }
            set { ContractContent = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CStartTime", "varchar")]
        public string StrCStartTime
        {
            get { return CStartTime; }
            set { CStartTime = value; }
        }

        [Display(Name = "合同工期")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "必须是数字")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TimeScale", "int")]
        public int StrTimeScale
        {
            get { return TimeScale; }
            set { TimeScale = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CPlanEndTime", "varchar")]
        public string StrCPlanEndTime
        {
            get { return CPlanEndTime; }
            set { CPlanEndTime = value; }
        }

        [Display(Name = "合同初始金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CBeginAmount", "decimal")]
        public decimal StrCBeginAmount
        {
            get { return CBeginAmount; }
            set { CBeginAmount = value; }
        }

        [Display(Name = "履约保证金")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Margin", "decimal")]
        public decimal StrMargin
        {
            get { return Margin; }
            set { Margin = value; }
        }

        [Display(Name = "变更金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CEndAmount", "decimal")]
        public decimal StrCEndAmount
        {
            get { return CEndAmount; }
            set { CEndAmount = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CEndTime", "varchar")]
        public string StrCEndTime
        {
            get { return CEndTime; }
            set { CEndTime = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Ctime", "varchar")]
        public string StrCtime
        {
            get { return Ctime; }
            set { Ctime = value; }
        }

        [Display(Name = "合同签订回款次数")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "必须是数字")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AmountNum", "int")]
        public int StrAmountNum
        {
            get { return AmountNum; }
            set { AmountNum = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CurAmountNum", "int")]
        public int StrCurAmountNum
        {
            get { return CurAmountNum; }
            set { CurAmountNum = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Principal", "nvarchar")]
        public string StrPrincipal
        {
            get { return Principal; }
            set { Principal = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PartyA", "varchar")]
        public string StrPartyA
        {
            get { return PartyA; }
            set { PartyA = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PartyB", "varchar")]
        public string StrPartyB
        {
            get { return PartyB; }
            set { PartyB = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayOrIncome", "varchar")]
        public string StrPayOrIncome
        {
            get { return PayOrIncome; }
            set { PayOrIncome = value; }
        }

        [Display(Name = "页数")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "必须是数字")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PageCount", "int")]
        public int StrPageCount
        {
            get { return PageCount; }
            set { PageCount = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Rmark", "nvarchar")]
        public string StrRmark
        {
            get { return Rmark; }
            set { Rmark = value; }
        }

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
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

        [Display(Name = "项目合同额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PContractAmount", "decimal")]
        public decimal StrPContractAmount
        {
            get { return PContractAmount; }
            set { PContractAmount = value; }
        }

        [Display(Name = "项目前期费用：（管理费、预算）")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PBudget", "decimal")]
        public decimal StrPBudget
        {
            get { return PBudget; }
            set { PBudget = value; }
        }

        [Display(Name = "项目成本")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PCost", "decimal")]
        public decimal StrPCost
        {
            get { return PCost; }
            set { PCost = value; }
        }

        [Display(Name = "项目利润")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PProfit", "decimal")]
        public decimal StrPProfit
        {
            get { return PProfit; }
            set { PProfit = value; }
        }
    }
}
