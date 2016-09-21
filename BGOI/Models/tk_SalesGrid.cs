using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class tk_SalesGrid
    {
        #region 家用产品--零售销售
        private string m_strSalesProduct = ""; //销售产品

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SalesProduct
        {
            get { return m_strSalesProduct; }
            set { m_strSalesProduct = value; }
        }

        private string m_strSpecsModels = ""; //规格型号

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SpecsModels
        {
            get { return m_strSpecsModels; }
            set { m_strSpecsModels = value; }
        }

        private string m_strSalesMan = "";  //销售人

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SalesMan
        {
            get { return m_strSalesMan; }
            set { m_strSalesMan = value; }
        }

        private string strStartDate = "";    //开始时间

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StartDate
        {
            get { return strStartDate; }
            set { strStartDate = value; }
        }

        private string m_strEndDate = "";   //结束时间

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string EndDate
        {
            get { return m_strEndDate; }
            set { m_strEndDate = value; }
        }

        private string m_strIsHK = "";   //是否回款

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string IsHK
        {
            get { return m_strIsHK; }
            set { m_strIsHK = value; }
        }

        private string m_strState = "";   //任务状态
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strApplyMan = "";  //申请人--内购管理

        public string ApplyMan
        {
            get { return m_strApplyMan; }
            set { m_strApplyMan = value; }
        }

        private string m_strIOID = "";    //内购申请编号--内购审批
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string IOID
        {
            get { return m_strIOID; }
            set { m_strIOID = value; }
        }

        private string m_strMalls = "";    //专柜处理--所属代理商
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Malls
        {
            get { return m_strMalls; }
            set { m_strMalls = value; }
        }

        private string m_strApplyReason = "";  //专柜处理--申请事由
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ApplyReason
        {
            get { return m_strApplyReason; }
            set { m_strApplyReason = value; }
        }

        private string m_strMakeType = "";    //专柜处理--制作类型
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string MakeType
        {
            get { return m_strMakeType; }
            set { m_strMakeType = value; }
        }

        private string m_strApplyer = "";   //专柜处理--申请人
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Applyer
        {
            get { return m_strApplyer; }
            set { m_strApplyer = value; }
        }

        private string m_strCustomer = "";    //专柜处理--商场名称
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Customer
        {
            get { return m_strCustomer; }
            set { m_strCustomer = value; }
        }

        private string m_strSIID = "";  //专柜审批--申请编号
        [StringLength(20, ErrorMessage = "专柜申请编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string SIID
        {
            get { return m_strSIID; }
            set { m_strSIID = value; }
        }

        private string m_strPAID = "";  //样机审批--申请编号
        [StringLength(20, ErrorMessage = "样机申请编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string PAID
        {
            get { return m_strPAID; }
            set { m_strPAID = value; }
        }

        private string m_strActionTitle = "";     //促销活动--活动主题
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ActionTitle", "nvarchar")]
        public string ActionTitle
        {
            get { return m_strActionTitle; }
            set { m_strActionTitle = value; }
        }

        private string m_strApplyType = "";     //市场销售--申请类型
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyType", "nvarchar")]
        public string ApplyType
        {
            get { return m_strApplyType; }
            set { m_strApplyType = value; }
        }

        private string m_strApplyTitle = "";      //市场销售--申请名称
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyTitle", "nvarchar")]
        public string ApplyTitle
        {
            get { return m_strApplyTitle; }
            set { m_strApplyTitle = value; }
        }

        private string m_strUnitId = "";     //部门
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string UnitId
        {
            get { return m_strUnitId; }
            set { m_strUnitId = value; }
        }

        private string strISCollection;
           [DataField("ISCollection", "nvarchar")]
        public string ISCollection
        {
            get { return strISCollection; }
            set { strISCollection = value; }
        }
        //赠送备注
           private string strSendRemark;
          [DataField("SendRemark", "nvarchar")]
           public string SendRemark
           {
               get { return strSendRemark; }
               set { strSendRemark = value; }
           }
        #endregion

        #region 项目销售
        private string m_PlanName = "";//项目名称
        private string m_PlanID = "";//工程编号
        private string m_MainContent = "";//内容
        private string m_Manager = "";//负责人
        private string m_OfferTitle = "";//报价标题
        private string m_ContractID = "";//合同编号
        private string m_OrderUnit = "";//订货单位
        private string m_OrderID = "";//订货编号


        private string m_UseUnit = "";//使用单位
        private string m_Shippers = "";//发货人
        private string m_Brokerage = "";//经手人
        private string m_OrderContactor = "";//零售销售联系人
        private string m_BelongArea = "";
        public string OrderContactor
        {
            get { return m_OrderContactor; }
            set { m_OrderContactor = value; }
        }
        private string m_OrderTel = "";//零售销售联系电话

        public string OrderTel
        {
            get { return m_OrderTel; }
            set { m_OrderTel = value; }
        }



        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string PlanName
        {
            get { return m_PlanName; }
            set { m_PlanName = value; }
        }
        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string PlanID
        {
            get { return m_PlanID; }
            set { m_PlanID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string MainContent
        {
            get { return m_MainContent; }
            set { m_MainContent = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Manager
        {
            get { return m_Manager; }
            set { m_Manager = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string OfferTitle
        {
            get { return m_OfferTitle; }
            set { m_OfferTitle = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string ContractID
        {
            get { return m_ContractID; }
            set { m_ContractID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string OrderUnit
        {
            get { return m_OrderUnit; }
            set { m_OrderUnit = value; }
        }
        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string OrderID
        {
            get { return m_OrderID; }
            set { m_OrderID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string UseUnit
        {
            get { return m_UseUnit; }
            set { m_UseUnit = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Shippers
        {
            get { return m_Shippers; }
            set { m_Shippers = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string Brokerage
        {
            get { return m_Brokerage; }
            set { m_Brokerage = value; }
        }

        public string BelongArea
        {
            get { return m_BelongArea; }
            set { m_BelongArea = value; }
        }
        #endregion
    }
}
