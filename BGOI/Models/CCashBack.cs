using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class CCashBack
    {
        private string CID;
        private string CBID;
        private int CurAmountNum;
        private string CBMethod;
        private decimal CBMoney;
        private decimal ReceiptMoney;
        private string CBBillNo;
        private string ReceiptNo;
        private int IsReturn;
        private string NoReturnReason;
        private string PayCompany;
        private string Remark;
        private string CBDate;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        [DataFieldAttribute("CID", "varchar")]
        public string StrCID
        {
            get { return CID; }
            set { CID = value; }
        }

        [DataFieldAttribute("CBID", "varchar")]
        public string StrCBID
        {
            get { return CBID; }
            set { CBID = value; }
        }

        [DataFieldAttribute("CurAmountNum", "int")]
        public int StrCurAmountNum
        {
            get { return CurAmountNum; }
            set { CurAmountNum = value; }
        }

        [Required(ErrorMessage = "回款方式不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CBMethod", "varchar")]
        public string StrCBMethod
        {
            get { return CBMethod; }
            set { CBMethod = value; }
        }

        [Display(Name = "回款金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CBMoney", "decimal")]
        public decimal StrCBMoney
        {
            get { return CBMoney; }
            set { CBMoney = value; }
        }

        [Display(Name = "发票金额")]
        [RegularExpression("^[0-9]+(.[0-9]{2})?$", ErrorMessage = "必须是数字或两位小数")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiptMoney", "decimal")]
        public decimal StrReceiptMoney
        {
            get { return ReceiptMoney; }
            set { ReceiptMoney = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CBBillNo", "varchar")]
        public string StrCBBillNo
        {
            get { return CBBillNo; }
            set { CBBillNo = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiptNo", "varchar")]
        public string StrReceiptNo
        {
            get { return ReceiptNo; }
            set { ReceiptNo = value; }
        }

        [DataFieldAttribute("IsReturn", "int")]
        public int StrIsReturn
        {
            get { return IsReturn; }
            set { IsReturn = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("NoReturnReason", "nvarchar")]
        public string StrNoReturnReason
        {
            get { return NoReturnReason; }
            set { NoReturnReason = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PayCompany", "nvarchar")]
        public string StrPayCompany
        {
            get { return PayCompany; }
            set { PayCompany = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }

        [Required(ErrorMessage = "回款日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CBDate", "date")]
        public string StrCBDate
        {
            get { return CBDate; }
            set { CBDate = value; }
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
