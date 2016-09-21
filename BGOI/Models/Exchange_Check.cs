using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
    public class Exchange_Check
    {
        private string m_TID;
        [Required(ErrorMessage = "编号不能为空")]
        [StringLength(20, ErrorMessage = "编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("TID", "varchar")]
        public string TID
        {
            get { return m_TID; }
            set { m_TID = value; }
        }
        private string m_EID;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField ("EID","varchar")]
        public string EID
        {
            get { return m_EID; }
            set { m_EID = value; }
        }
        private string m_Brokerage;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Brokerage", "varchar")]
        public string Brokerage
        {
            get { return m_Brokerage; }
            set { m_Brokerage = value; }
        }
        private string m_ChangeDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ChangeDate", "date")]
        public string ChangeDate
        {
            get { return m_ChangeDate; }
            set { m_ChangeDate = value; }
        }
        private string m_IsApproval1;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string IsApproval1
        {
            get { return m_IsApproval1; }
            set { m_IsApproval1 = value; }
        }
        private string m_CheckDescription;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("CheckDescription", "varchar")]
        public string CheckDescription
        {
            get { return m_CheckDescription; }
            set { m_CheckDescription = value; }
        }
        private string m_RememberPeople;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("RememberPeople", "varchar")]
        public string RememberPeople
        {
            get { return m_RememberPeople; }
            set { m_RememberPeople = value; }
        }
        private string m_State;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("State", "varchar")]
        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        private string m_IState;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IState", "varchar")]
        public string IState
        {
            get { return m_IState; }
            set { m_IState = value; }
        }

        private string m_ProductionState;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProductionState", "varchar")]
        public string ProductionState
        {
            get { return m_ProductionState; }
            set { m_ProductionState = value; }
        }

        private string m_CreateTime;

        public string CreateTime
        {
            get { return m_CreateTime; }
            set { m_CreateTime = value; }
        }
        private string m_CreateUser;

        public string CreateUser
        {
            get { return m_CreateUser; }
            set { m_CreateUser = value; }
        }
        private string m_Validate;

        public string Validate
        {
            get { return m_Validate; }
            set { m_Validate = value; }
        }

       
    }
}
