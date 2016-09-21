using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class CSettlement
    {
        private string CID;
        private string FinishDate;
        private int IsDebt;
        private decimal DebtAmount;
        private string DebtReason;
        private string Remark;
        private DateTime? CreateTime;
        private string CreateUser;

        [DataFieldAttribute("CID", "varchar")]
        public string StrCID
        {
            get { return CID; }
            set { CID = value; }
        }

        [Required(ErrorMessage = "结项日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishDate", "date")]
        public string StrFinishDate
        {
            get { return FinishDate; }
            set { FinishDate = value; }
        }

        [DataFieldAttribute("IsDebt", "int")]
        public int StrIsDebt
        {
            get { return IsDebt; }
            set { IsDebt = value; }
        }

        [DataFieldAttribute("DebtAmount", "decimal")]
        public decimal StrDebtAmount
        {
            get { return DebtAmount; }
            set { DebtAmount = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebtReason", "nvarchar")]
        public string StrDebtReason
        {
            get { return DebtReason; }
            set { DebtReason = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
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
    }
}
