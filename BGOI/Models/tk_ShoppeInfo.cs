using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ShoppeInfo
    {
        private string m_strSIID = "";
        [StringLength(20, ErrorMessage = "产品编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SIID", "varchar")]
        public string SIID
        {
            get { return m_strSIID; }
            set { m_strSIID = value; }
        }

        private string m_strUnitID = "";
        [DataField("UnitID", "varchar")]
        public string UnitID
        {
            get { return m_strUnitID; }
            set { m_strUnitID = value; }
        }

        private DateTime dtApplyTime;
        [DataField("ApplyTime", "datetime")]
        public DateTime ApplyTime
        {
            get { return dtApplyTime; }
            set { dtApplyTime = value; }
        }

        private string m_strApplyer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Applyer", "nvarchar")]
        public string Applyer
        {
            get { return m_strApplyer; }
            set { m_strApplyer = value; }
        }

        private string m_strCustomer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Customer", "nvarchar")]
        public string Customer
        {
            get { return m_strCustomer; }
            set { m_strCustomer = value; }
        }

        private string m_strMalls = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Malls", "nvarchar")]
        public string Malls
        {
            get { return m_strMalls; }
            set { m_strMalls = value; }
        }

        private string m_strMallType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("MallType", "nvarchar")]
        public string MallType
        {
            get { return m_strMallType; }
            set { m_strMallType = value; }
        }

        private string m_strPhone = "";
        [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的申请人联系电话格式")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Phone", "nvarchar")]
        public string Phone
        {
            get { return m_strPhone; }
            set { m_strPhone = value; }
        }

        private string m_strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Address", "nvarchar")]
        public string Address
        {
            get { return m_strAddress; }
            set { m_strAddress = value; }
        }

        private string m_strProductsOneName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProductsOneName", "nvarchar")]
        public string ProductsOneName
        {
            get { return m_strProductsOneName; }
            set { m_strProductsOneName = value; }
        }

        private int IntSampleOneNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SampleOneNum", "int")]
        public int SampleOneNum
        {
            get { return IntSampleOneNum; }
            set { IntSampleOneNum = value; }
        }

        private decimal dcShoppeSize;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ShoppeSize", "decimal")]
        public decimal ShoppeSize
        {
            get { return dcShoppeSize; }
            set { dcShoppeSize = value; }
        }

        private string m_strProductsTwoName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProductsTwoName", "nvarchar")]
        public string ProductsTwoName
        {
            get { return m_strProductsTwoName; }
            set { m_strProductsTwoName = value; }
        }

        private decimal dcShoppeTwoSize;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ShoppeTwoSize", "decimal")]
        public decimal ShoppeTwoSize
        {
            get { return dcShoppeTwoSize; }
            set { dcShoppeTwoSize = value; }
        }

        private int IntSampleNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SampleNum", "int")]
        public int SampleNum
        {
            get { return IntSampleNum; }
            set { IntSampleNum = value; }
        }

        private string m_strShoppePosition = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ShoppePosition", "nvarchar")]
        public string ShoppePosition
        {
            get { return m_strShoppePosition; }
            set { m_strShoppePosition = value; }
        }

        private decimal dcMonthSalesNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("MonthSalesNum", "decimal")]
        public decimal MonthSalesNum
        {
            get { return dcMonthSalesNum; }
            set { dcMonthSalesNum = value; }
        }

        private decimal dcSalesAmount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SalesAmount", "decimal")]
        public decimal SalesAmount
        {
            get { return dcSalesAmount; }
            set { dcSalesAmount = value; }
        }

        private decimal dcAmount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Amount", "decimal")]
        public decimal Amount
        {
            get { return dcAmount; }
            set { dcAmount = value; }
        }

        private string m_strCookers = "";          //灶具
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Cookers", "nvarchar")]
        public string Cookers
        {
            get { return m_strCookers; }
            set { m_strCookers = value; }
        }

        private string m_strTurbine = "";      //烟机
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Turbine", "nvarchar")]
        public string Turbine
        {
            get { return m_strTurbine; }
            set { m_strTurbine = value; }
        }

        private string m_strGasHeater = "";    //热水器
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("GasHeater", "nvarchar")]
        public string GasHeater
        {
            get { return m_strGasHeater; }
            set { m_strGasHeater = value; }
        }

        private string m_strGasBoiler = "";    //壁挂炉
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("GasBoiler", "nvarchar")]
        public string GasBoiler
        {
            get { return m_strGasBoiler; }
            set { m_strGasBoiler = value; }
        }

        private string m_strApplyReason = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ApplyReason", "nvarchar")]
        public string ApplyReason
        {
            get { return m_strApplyReason; }
            set { m_strApplyReason = value; }
        }

        private string m_strMakeType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("MakeType", "nvarchar")]
        public string MakeType
        {
            get { return m_strMakeType; }
            set { m_strMakeType = value; }
        }

        private string m_strUseYear = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UseYear", "nvarchar")]
        public string UseYear
        {
            get { return m_strUseYear; }
            set { m_strUseYear = value; }
        }

        private string m_strEndYear = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("EndYear", "nvarchar")]
        public string EndYear
        {
            get { return m_strEndYear; }
            set { m_strEndYear = value; }
        }

        private decimal dcBudget;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Budget", "decimal")]
        public decimal Budget
        {
            get { return dcBudget; }
            set { dcBudget = value; }
        }

        private string m_strExplain = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Explain", "nvarchar")]
        public string Explain
        {
            get { return m_strExplain; }
            set { m_strExplain = value; }
        }

        private string m_strState = "";
        [DataField("State", "varchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return m_strCreateUser; }
            set { m_strCreateUser = value; }
        }

        private DateTime strCreateTime;
        [DataField("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string m_strValidate = "";
        [DataField("Validate", "varchar")]
        public string Validate
        {
            get { return m_strValidate; }
            set { m_strValidate = value; }
        }
    }
}
