using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ExchangeGoods
    {
        private string strEID;
        [Required(ErrorMessage = "编号不能为空")]
        [StringLength(20, ErrorMessage = "编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("EID", "varchar")]
        public string EID
        {
            get { return strEID; }
            set { strEID = value; }
        }
        private string strOrderID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }

        private string strContractID;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ContractID", "varchar")]
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }
        private string strUnit;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Unit", "varchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        private string strBrokerage;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Brokerage", "nvarchar")]
        public string Brokerage
        {
            get { return strBrokerage; }
            set { strBrokerage = value; }
        }
        private string strChangeDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ChangeDate", "date")]
        public string ChangeDate
        {
            get { return strChangeDate; }
            set { strChangeDate = value; }
        }
        private string strReason;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Reason", "varchar")]
        public string Reason
        {
            get { return strReason; }
            set { strReason = value; }
        }
        private string strBrokerageRequire;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("BrokerageRequire", "varchar")]
        public string BrokerageRequire
        {
            get { return strBrokerageRequire; }
            set { strBrokerageRequire = value; }
        }
        private string strIsApproval1;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IsApproval1", "varchar")]
        public string IsApproval1
        {
            get { return strIsApproval1; }
            set { strIsApproval1 = value; }
        }
        private string strHandle;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Handle", "nvarchar")]
        public string Handle
        {
            get { return strHandle; }
            set { strHandle = value; }
        }
        private string strState;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        private DateTime? strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("CreateTime", "datetime")]
        public DateTime? CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strCreateUser;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private string strReturnReason;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ReturnReason", "varchar")]
        public string ReturnReason
        {
            get { return strReturnReason; }
            set { strReturnReason = value; }
        }
        private string strReturnType;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ReturnType", "varchar")]
        public string ReturnType
        {
            get { return strReturnType; }
            set { strReturnType = value; }
        }
        private string strReturnWay;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ReturnWay", "varchar")]
        public string ReturnWay
        {
            get { return strReturnWay; }
            set { strReturnWay = value; }
        }
        private string strReturnContract;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ReturnContract", "varchar")]
        public string ReturnContract
        {
            get { return strReturnContract; }
            set { strReturnContract = value; }
        }
        private string strReturnTotal;
        [DataField("ReturnTotal", "varchar")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ReturnTotal
        {
            get { return strReturnTotal; }
            set { strReturnTotal = value; }
        }
        private string strPayWay;
        [DataField("PayWay", "varchar")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string PayWay
        {
            get { return strPayWay; }
            set { strPayWay = value; }
        }
        private string strReturnInstructions;
        [DataField("ReturnInstructions", "varchar")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ReturnInstructions
        {
            get { return strReturnInstructions; }
            set { strReturnInstructions = value; }
        }
        private string strMethods;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Methods", "varchar")]
        public string Methods
        {
            get { return strMethods; }
            set { strMethods = value; }
        }
        private string strExFinishDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ExFinishDate", "datetime")]
        public string ExFinishDate
        {
            get { return strExFinishDate; }
            set { strExFinishDate = value; }
        }
        private string strExFinishDealPoe;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ExFinishDealPeo", "varchar")]
        public string ExFinishDealPeo
        {
            get { return strExFinishDealPoe; }
            set { strExFinishDealPoe = value; }
        }
        private string strExFinishDescription;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ExFinishDescription", "varchar")]
        public string ExFinishDescription
        {
            get { return strExFinishDescription; }
            set { strExFinishDescription = value; }
        }

//是否是非常规订单
        private string strISF;
        [DataField("ISF", "varchar")]
        public string ISF
        {
            get { return strISF; }
            set { strISF = value; }
        }
        private string strISEXR;
        [DataField("ISEXR", "varchar")]
        public string ISEXR
        {
            get { return strISEXR; }
            set { strISEXR = value; }
        }
    }
}
