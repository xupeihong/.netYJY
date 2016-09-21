using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class OrdersInfoNew
    {
        #region Old
        private string strPID;
        [DataField("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        private string strUnitID;
        [DataField("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }
        private string strOrderID;
        [DataField("OrderID", "")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
        private string strContractID;
        [DataField("ContractID", "varchar")]
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }
        private string strSalesType;
        [DataField("SalesType", "varchar")]
        public string SalesType
        {
            get { return strSalesType; }
            set { strSalesType = value; }
        }
        private DateTime strContractDate;
        [DataField("ContractDate", "date")]
        public DateTime ContractDate
        {
            get { return strContractDate; }
            set { strContractDate = value; }
        }
        private string strOrderUnit;
        [DataField("OrderUnit", "nvarchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }
        private string strOrderContactor;
        [DataField("OrderContactor", "nvarchar")]
        public string OrderContactor
        {
            get { return strOrderContactor; }
            set { strOrderContactor = value; }
        }
        private string strOrderTel;
        [DataField("OrderTel", "varchar")]
        public string OrderTel
        {
            get { return strOrderTel; }
            set { strOrderTel = value; }
        }
        private string strOrderAddress;
        [DataField("OrderAddress", "nvarchar")]
        public string OrderAddress
        {
            get { return strOrderAddress; }
            set { strOrderAddress = value; }
        }
        private string strUseUnit;
        [DataField("UseUnit", "nvarchar")]
        public string UseUnit
        {
            get { return strUseUnit; }
            set { strUseUnit = value; }
        }
        private string strUseContactor;
        [DataField("UseContactor", "varchar")]
        public string UseContactor
        {
            get { return strUseContactor; }
            set { strUseContactor = value; }
        }
        private string strOstate;
        [DataField("Ostate", "varchar")]
        public string Ostate
        {
            get { return strOstate; }
            set { strOstate = value; }
        }


        private string strUseTel;
        [DataField("UseTel", "varchar")]
        public string UseTel
        {
            get { return strUseTel; }
            set { strUseTel = value; }
        }
        private string strUseAddress;
        [DataField("UseAddress", "varchar")]
        public string UseAddress
        {
            get { return strUseAddress; }
            set { strUseAddress = value; }
        }
        private decimal strTotal;
        [DataField("Total", "decimal")]
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        private string strPayWay;
        [DataField("PayWay", "varchar")]
        public string PayWay
        {
            get { return strPayWay; }
            set { strPayWay = value; }
        }
        private string strGuarantee;
        [DataField("Guarantee", "varchar")]
        public string Guarantee
        {
            get { return strGuarantee; }
            set { strGuarantee = value; }
        }
        private string strProvider;
        [DataField("Provider", "varchar")]
        public string Provider
        {
            get { return strProvider; }
            set { strProvider = value; }
        }
        private string strProvidManager;
        [DataField("ProvidManager", "nvarchar")]
        public string ProvidManager
        {
            get { return strProvidManager; }
            set { strProvidManager = value; }
        }
        private string strDemand;
        [DataField("Demand", "nvarchar")]
        public string Demand
        {
            get { return strDemand; }
            set { strDemand = value; }
        }
        private string strDemandManager;
        [DataField("DemandManager", "nvarchar")]
        public string DemandManager
        {
            get { return strDemandManager; }
            set { strDemandManager = value; }
        }
        private string strRemark;
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private string strIsBranch;
        [DataField("IsBranch", "varchar")]
        public string IsBranch
        {
            get { return strIsBranch; }
            set { strIsBranch = value; }
        }
        private string strIsPriceRules;
        [DataField("IsPriceRules", "varchar")]
        public string IsPriceRules
        {
            get { return strIsPriceRules; }
            set { strIsPriceRules = value; }
        }
        private string strIsManager;
        [DataField("IsManager", "varchar")]
        public string IsManager
        {
            get { return strIsManager; }
            set { strIsManager = value; }
        }
        private string strCreateTime;
        [DataField("CreateTime", "datetime")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }


        private DateTime strSupplyTime;
        [DataField("SupplyTime", "datetime")]
        public DateTime SupplyTime
        {
            get { return strSupplyTime; }
            set { strSupplyTime = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private string strState;
        [DataField("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        } 
        #endregion

        private string strExpectedReturnDate;
        [DataField("ExpectedReturnDate", "varchar")]
        public string ExpectedReturnDate
        {
            get { return strExpectedReturnDate; }
            set { strExpectedReturnDate = value; }
        }
    }
}
