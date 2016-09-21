using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TECOCITY_BGOI
{
    public class ProFinish
    {
        private string m_PID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }
        private string m_OrderID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderID", "varchar")]
        public string OrderID
        {
            get { return m_OrderID; }
            set { m_OrderID = value; }
        }
        private string m_FinishDate;
        [Required(ErrorMessage = "结项日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishDate", "date")]
        public string FinishDate
        {
            get { return m_FinishDate; }
            set { m_FinishDate = value; }
        }
        private decimal m_Amount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Amount", "decimal")]
        public decimal Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        private string m_IsDebt;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsDebt", "varchar")]
        public string IsDebt
        {
            get { return m_IsDebt; }
            set { m_IsDebt = value; }
        }
        private decimal m_DebtAmount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebtAmount", "decimal")]
        public decimal DebtAmount
        {
            get { return m_DebtAmount; }
            set { m_DebtAmount = value; }
        }
        private string m_DebtReason;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebtReason", "varchar")]
        public string DebtReason
        {
            get { return m_DebtReason; }
            set { m_DebtReason = value; }
        }
        private string m_HasExchange;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HasExchange", "varchar")]
        public string HasExchange
        {
            get { return m_HasExchange; }
            set { m_HasExchange = value; }
        }
        private decimal m_AlterAmount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("AlterAmount", "decimal")]
        public decimal AlterAmount
        {
            get { return m_AlterAmount; }
            set { m_AlterAmount = value; }
        }
        private string m_CreateUser;

        public string CreateUser
        {
            get { return m_CreateUser; }
            set { m_CreateUser = value; }
        }
        private string m_CreateTime;

        public string CreateTime
        {
            get { return m_CreateTime; }
            set { m_CreateTime = value; }
        }
        private string m_Validate;

        public string Validate
        {
            get { return m_Validate; }
            set { m_Validate = value; }
        }

    }
}
