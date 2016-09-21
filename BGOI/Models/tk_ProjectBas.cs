using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProjectBas
    {
        private string PID;
        private string FinishDate;
        private string AppID;
        private string BuildUnit;
        private string LinkMan;
        private string Phone;
        private string Paddress;
        private string Principal;
        private string ConcertPerson;
        private decimal ContractAmount;
        private decimal Budget;
        private decimal Cost;
        private decimal Profit;
        private string Schedule;
        private string PlanSignaDate;
        private string AppDate;
        private string AppUser;
        private int IsDesign;
        private int IsPrice;
        private int IsBudget;
        private int IsCBack;
        private int IsContract;

        public string StrPID
        {
            get { return PID; }
            set { PID = value; }
        }

        public string StrFinishDate
        {
            get { return FinishDate; }
            set { FinishDate = value; }
        }

        [Remote("JudgeSameAppID", "ProjectManage", ErrorMessage = "该立项编号已存在,请重新填写")]
        [StringLength(20, ErrorMessage = "立项编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrAppID
        {
            get { return AppID; }
            set { AppID = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrBuildUnit
        {
            get { return BuildUnit; }
            set { BuildUnit = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrLinkMan
        {
            get { return LinkMan; }
            set { LinkMan = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPhone
        {
            get { return Phone; }
            set { Phone = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPaddress
        {
            get { return Paddress; }
            set { Paddress = value; }
        }

        [Required(ErrorMessage = "项目负责人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPrincipal
        {
            get { return Principal; }
            set { Principal = value; }
        }

        [Required(ErrorMessage = "配合负责人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrConcertPerson
        {
            get { return ConcertPerson; }
            set { ConcertPerson = value; }
        }

        [Display(Name = "项目合同额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrContractAmount
        {
            get { return ContractAmount; }
            set { ContractAmount = value; }
        }

        [Display(Name = "项目前期费用")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrBudget
        {
            get { return Budget; }
            set { Budget = value; }
        }

        [Display(Name = "项目成本")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrCost
        {
            get { return Cost; }
            set { Cost = value; }
        }

        [Display(Name = "项目利润")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public decimal StrProfit
        {
            get { return Profit; }
            set { Profit = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrSchedule
        {
            get { return Schedule; }
            set { Schedule = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrPlanSignaDate
        {
            get { return PlanSignaDate; }
            set { PlanSignaDate = value; }
        }

        [Required(ErrorMessage = "立项时间不能为空")]
        public string StrAppDate
        {
            get { return AppDate; }
            set { AppDate = value; }
        }

        public string StrAppUser
        {
            get { return AppUser; }
            set { AppUser = value; }
        }

        public int StrIsDesign
        {
            get { return IsDesign; }
            set { IsDesign = value; }
        }

        public int StrIsPrice
        {
            get { return IsPrice; }
            set { IsPrice = value; }
        }

        public int StrIsBudget
        {
            get { return IsBudget; }
            set { IsBudget = value; }
        }

        public int StrIsCBack
        {
            get { return IsCBack; }
            set { IsCBack = value; }
        }

        public int StrIsContract
        {
            get { return IsContract; }
            set { IsContract = value; }
        }
    }
}
