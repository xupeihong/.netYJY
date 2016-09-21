using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class OrdersInfo
    {
        #region Old
        //private string strPID;
        //[DataField("PID", "varchar")]
        //public string PID
        //{
        //    get { return strPID; }
        //    set { strPID = value; }
        //}
        //private string strUnitID;
        //[DataField("UnitID", "varchar")]
        //public string UnitID
        //{
        //    get { return strUnitID; }
        //    set { strUnitID = value; }
        //}
        //private string strOrderID;
        //[DataField("OrderID", "")]
        //public string OrderID
        //{
        //    get { return strOrderID; }
        //    set { strOrderID = value; }
        //}
        //private string strContractID;
        //[DataField("ContractID", "varchar")]
        //public string ContractID
        //{
        //    get { return strContractID; }
        //    set { strContractID = value; }
        //}
        //private string strSalesType;
        //[DataField("SalesType", "varchar")]
        //public string SalesType
        //{
        //    get { return strSalesType; }
        //    set { strSalesType = value; }
        //}
        //private DateTime strContractDate;
        //[DataField("ContractDate", "date")]
        //public DateTime ContractDate
        //{
        //    get { return strContractDate; }
        //    set { strContractDate = value; }
        //}
        //private string strOrderUnit;
        //[DataField("OrderUnit", "nvarchar")]
        //public string OrderUnit
        //{
        //    get { return strOrderUnit; }
        //    set { strOrderUnit = value; }
        //}
        //private string strOrderContactor;
        //[DataField("OrderContactor", "nvarchar")]
        //public string OrderContactor
        //{
        //    get { return strOrderContactor; }
        //    set { strOrderContactor = value; }
        //}
        //private string strOrderTel;
        //[DataField("OrderTel", "varchar")]
        //public string OrderTel
        //{
        //    get { return strOrderTel; }
        //    set { strOrderTel = value; }
        //}
        //private string strOrderAddress;
        //[DataField("OrderAddress", "nvarchar")]
        //public string OrderAddress
        //{
        //    get { return strOrderAddress; }
        //    set { strOrderAddress = value; }
        //}
        //private string strUseUnit;
        //[DataField("UseUnit", "nvarchar")]
        //public string UseUnit
        //{
        //    get { return strUseUnit; }
        //    set { strUseUnit = value; }
        //}
        //private string strUseContactor;
        //[DataField("UseContactor", "varchar")]
        //public string UseContactor
        //{
        //    get { return strUseContactor; }
        //    set { strUseContactor = value; }
        //}
        //private string strUseTel;
        //[DataField("UseTel", "varchar")]
        //public string UseTel
        //{
        //    get { return strUseTel; }
        //    set { strUseTel = value; }
        //}
        //private string strUseAddress;
        //[DataField("UseAddress", "varchar")]
        //public string UseAddress
        //{
        //    get { return strUseAddress; }
        //    set { strUseAddress = value; }
        //}
        //private decimal strTotal;
        //[DataField("Total", "decimal")]
        //public decimal Total
        //{
        //    get { return strTotal; }
        //    set { strTotal = value; }
        //}
        //private string strPayWay;
        //[DataField("PayWay", "varchar")]
        //public string PayWay
        //{
        //    get { return strPayWay; }
        //    set { strPayWay = value; }
        //}
        //private string strGuarantee;
        //[DataField("Guarantee", "varchar")]
        //public string Guarantee
        //{
        //    get { return strGuarantee; }
        //    set { strGuarantee = value; }
        //}
        //private string strProvider;
        //[DataField("Provider", "varchar")]
        //public string Provider
        //{
        //    get { return strProvider; }
        //    set { strProvider = value; }
        //}
        //private string strProvidManager;
        //[DataField("ProvidManager", "nvarchar")]
        //public string ProvidManager
        //{
        //    get { return strProvidManager; }
        //    set { strProvidManager = value; }
        //}
        //private string strDemand;
        //[DataField("Demand", "nvarchar")]
        //public string Demand
        //{
        //    get { return strDemand; }
        //    set { strDemand = value; }
        //}
        //private string strDemandManager;
        //[DataField("DemandManager", "nvarchar")]
        //public string DemandManager
        //{
        //    get { return strDemandManager; }
        //    set { strDemandManager = value; }
        //}
        //private string strRemark;
        //[DataField("Remark", "nvarchar")]
        //public string Remark
        //{
        //    get { return strRemark; }
        //    set { strRemark = value; }
        //}
        //private string strIsBranch;
        //[DataField("IsBranch", "varchar")]
        //public string IsBranch
        //{
        //    get { return strIsBranch; }
        //    set { strIsBranch = value; }
        //}
        //private string strIsPriceRules;
        //[DataField("IsPriceRules", "varchar")]
        //public string IsPriceRules
        //{
        //    get { return strIsPriceRules; }
        //    set { strIsPriceRules = value; }
        //}
        //private string strIsManager;
        //[DataField("IsManager", "varchar")]
        //public string IsManager
        //{
        //    get { return strIsManager; }
        //    set { strIsManager = value; }
        //}
        //private string strCreateTime;
        //[DataField("CreateTime", "datetime")]
        //public string CreateTime
        //{
        //    get { return strCreateTime; }
        //    set { strCreateTime = value; }
        //}
        //private string strCreateUser;
        //[DataField("CreateUser", "nvarchar")]
        //public string CreateUser
        //{
        //    get { return strCreateUser; }
        //    set { strCreateUser = value; }
        //}
        //private string strValidate;
        //[DataField("Validate", "varchar")]
        //public string Validate
        //{
        //    get { return strValidate; }
        //    set { strValidate = value; }
        //}
        //private string strState;
        //[DataField("State", "varchar")]
        //public string State
        //{
        //    get { return strState; }
        //    set { strState = value; }
        //} 
        #endregion

        private string strPID;
        //[StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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
        //[Required(ErrorMessage = "订货单号不能为空")]
        //[StringLength(20, ErrorMessage = "订货单号长度不能超过20个字符")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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

        private string m_strProjectName = ""; //新增--项目名称
        //   [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProjectName", "nvarchar")]
        public string ProjectName
        {
            get { return m_strProjectName; }
            set { m_strProjectName = value; }
        }

        private DateTime strSupplyTime;
        [DataField("SupplyTime", "datetime")]    //新增--计划供货时间
        public DateTime SupplyTime
        {
            get { return strSupplyTime; }
            set { strSupplyTime = value; }
        }

        private string strOrderUnit;
        // [Required(ErrorMessage = "订货单位不能为空")]
        //  [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderUnit", "nvarchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }

        private string strOrderContactor;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderContactor", "nvarchar")]
        public string OrderContactor
        {
            get { return strOrderContactor; }
            set { strOrderContactor = value; }
        }

        private string strOrderTel;
        //[RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderTel", "varchar")]
        public string OrderTel
        {
            get { return strOrderTel; }
            set { strOrderTel = value; }
        }

        private string strOrderAddress;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderAddress", "nvarchar")]
        public string OrderAddress
        {
            get { return strOrderAddress; }
            set { strOrderAddress = value; }
        }

        private string strUseUnit;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UseUnit", "nvarchar")]
        public string UseUnit
        {
            get { return strUseUnit; }
            set { strUseUnit = value; }
        }

        private string strUseContactor;
        //  [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UseContactor", "varchar")]
        public string UseContactor
        {
            get { return strUseContactor; }
            set { strUseContactor = value; }
        }

        private string strUseTel;
        //  [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UseTel", "varchar")]
        public string UseTel
        {
            get { return strUseTel; }
            set { strUseTel = value; }
        }

        private string strUseAddress;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UseAddress", "varchar")]
        public string UseAddress
        {
            get { return strUseAddress; }
            set { strUseAddress = value; }
        }

        private decimal strTotal;
        // [Range(1, 100)]
        [DataType(DataType.Currency)]
        //  [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Total", "decimal")]
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }

        private string strPayWay;
        //   [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PayWay", "varchar")]
        public string PayWay
        {
            get { return strPayWay; }
            set { strPayWay = value; }
        }

        private string strGuarantee;
        //   [Required(ErrorMessage = "产品保修期不能为空")]
        //   [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Guarantee", "varchar")]
        public string Guarantee
        {
            get { return strGuarantee; }
            set { strGuarantee = value; }
        }
        private string strChannelsFrom;
        [DataField("ChannelsFrom", "varchar")]
        public string ChannelsFrom
        {
            get { return strChannelsFrom; }
            set { strChannelsFrom = value; }
        }



        private string strProvider;
        //     [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Provider", "varchar")]
        public string Provider
        {
            get { return strProvider; }
            set { strProvider = value; }
        }

        private string strProvidManager;
        //    [Required(ErrorMessage = "负责人不能为空")]
        //   [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProvidManager", "nvarchar")]
        public string ProvidManager
        {
            get { return strProvidManager; }
            set { strProvidManager = value; }
        }

        private string strDemand;
        //  [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Demand", "nvarchar")]
        public string Demand
        {
            get { return strDemand; }
            set { strDemand = value; }
        }

        private string strDemandManager;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("DemandManager", "nvarchar")]
        public string DemandManager
        {
            get { return strDemandManager; }
            set { strDemandManager = value; }
        }

        private string strRemark;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strIsBranch;
        //  [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IsBranch", "varchar")]
        public string IsBranch
        {
            get { return strIsBranch; }
            set { strIsBranch = value; }
        }

        private string strIsPriceRules;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("IsPriceRules", "varchar")]
        public string IsPriceRules
        {
            get { return strIsPriceRules; }
            set { strIsPriceRules = value; }
        }

        private string strIsHK = "";          //是否回款--新增
        [DataField("IsHK", "varchar")]
        public string IsHK
        {
            get { return strIsHK; }
            set { strIsHK = value; }
        }

        private string strHKRemark = "";    //回款备注--新增
        //   [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("HKRemark", "nvarchar")]
        public string HKRemark
        {
            get { return strHKRemark; }
            set { strHKRemark = value; }
        }

        private string strIsManager;
        // [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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


        private string strDeliveryTime;

        [DataField("DeliveryTime", "varchar")]
        public string DeliveryTime
        {

            get { return strDeliveryTime; }
            set { strDeliveryTime = value; }
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
        private string strOstate;
        [DataField("Ostate", "varchar")]
        public string Ostate
        {
            get { return strOstate; }
            set { strOstate = value; }
        }

        private string strExpectedReturnDate;
        [DataField("ExpectedReturnDate", "datetime")]
        public string ExpectedReturnDate
        {
            get { return strExpectedReturnDate; }
            set { strExpectedReturnDate = value; }
        }

        private string strISF;
        [DataField("ISF", "varchar")]
        public string ISF
        {
            get { return strISF; }
            set { strISF = value; }
        }

        private string strISHT;
        [DataField("ISHT", "varchar")]
        public string ISHT
        {
            get { return strISHT; }
            set { strISHT = value; }
        }

        private int strPstate;
        [DataField("Pstate", "varchar")]
        public int Pstate
        {
            get { return strPstate; }
            set { strPstate = value; }
        }
        //添加零售反馈是否完成
        private int strISFinish;
        [DataField("ISFinish", "varchar")]
        public int ISFinish
        {
            get { return strISFinish; }
            set { strISFinish = value; }
        }

        private int strOrderAmount;
        //订单数据
        [DataField("OrderAmount", "int")]
        public int OrderAmount
        {
            get { return strOrderAmount; }
            set { strOrderAmount = value; }
        }
        private int strOrderActualAmount;
        //订单实际数量
        [DataField("OrderActualAmount", "int")]
        public int OrderActualAmount
        {
            get { return strOrderActualAmount; }
            set { strOrderActualAmount = value; }
        }


        private decimal strOrderTotal;
        //订单金额
        [DataField("OrderTotal", "decimal")]
        public decimal OrderTotal
        {
            get { return strOrderTotal; }
            set { strOrderTotal = value; }
        }

        private decimal strOrderActualTotal;
        //订单实际金额
        [DataField("OrderActualTotal", "decimal")]
        public decimal OrderActualTotal
        {
            get { return strOrderActualTotal; }
            set { strOrderActualTotal = value; }
        }
        private string strEXState;
        [DataField("EXState", "varchar")]
        public string EXState
        {
            get { return strEXState; }
            set { strEXState = value; }
        }

        private string strISCollection;
        [DataField("ISCollection", "varchar")]
        public string ISCollection
        {
            get { return strISCollection; }
            set { strISCollection = value; }
        }

        private string strLibraryTubeState;
        [DataField("LibraryTubeState", "varchar")]
        public string LibraryTubeState
        {
            get { return strLibraryTubeState; }
            set { strLibraryTubeState = value; }
        }

        private string strAfterSaleState;
        [DataField("AfterSaleState", "varchar")]
        public string AfterSaleState
        {
            get { return strAfterSaleState; }
            set { strAfterSaleState = value; }
        }

        private string strLibraryTubeTime;
        [DataField("LibraryTubeTime", "datetime")]
        public string LibraryTubeTime
        {
            get { return strLibraryTubeTime; }
            set { strLibraryTubeTime = value; }
        }
    }
}
