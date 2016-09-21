using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class Orders_DetailInfo
    {
        #region old
        //private string strPID;
        //[DataField("PID", "varchar")]
        //public string PID
        //{
        //    get { return strPID; }
        //    set { strPID = value; }
        //}

        //private string strOrderID;
        //[DataField("OrderID", "varchar")]
        //public string OrderID
        //{
        //    get { return strOrderID; }
        //    set { strOrderID = value; }
        //}
        //private string strDID;
        //[DataField("DID", "varchar")]
        //public string DID
        //{
        //    get { return strDID; }
        //    set { strDID = value; }
        //}
        //private string strProductID;
        //[DataField("ProductID", "varchar")]
        //public string ProductID
        //{
        //    get { return strProductID; }
        //    set { strProductID = value; }
        //}
        //private string strOrderContent;
        //[DataField("OrderContent", "nvarchar")]
        //public string OrderContent
        //{
        //    get { return strOrderContent; }
        //    set { strOrderContent = value; }
        //}
        //private string strSpecsModels;
        //[DataField("SpecsModels", "nvarchar")]
        //public string SpecsModels
        //{
        //    get { return strSpecsModels; }
        //    set { strSpecsModels = value; }
        //}
        //private string strManufacturer;
        //[DataField("Manufacturer", "nvarchar")]
        //public string Manufacturer
        //{
        //    get { return strManufacturer; }
        //    set { strManufacturer = value; }
        //}
        //private string strOrderUnit;
        //[DataField("OrderUnit", "nvarchar")]
        //public string OrderUnit
        //{
        //    get { return strOrderUnit; }
        //    set { strOrderUnit = value; }
        //}
        //private int strOrderNum;
        //[DataField("OrderNum", "int")]
        //public int OrderNum
        //{
        //    get { return strOrderNum; }
        //    set { strOrderNum = value; }
        //}

        //private decimal strTaxUnitPrice;
        //[DataField("TaxUnitPrice", "decimal")]
        //public decimal TaxUnitPrice
        //{
        //    get { return strTaxUnitPrice; }
        //    set { strTaxUnitPrice = value; }
        //}

        //private decimal strUnitPrice;
        //[DataField("UnitPrice", "decimal")]
        //public decimal UnitPrice
        //{
        //    get { return strUnitPrice; }
        //    set { strUnitPrice = value; }
        //}

        //private decimal strDUnitPrice;
        //[DataField("DUnitPrice", "decimal")]
        //public decimal DUnitPrice
        //{
        //    get { return strDUnitPrice; }
        //    set { strDUnitPrice = value; }
        //}

        //private decimal strDTotalPrice;
        //[DataField("DTotalPrice", "decimal")]
        //public decimal DTotalPrice
        //{
        //    get { return strDTotalPrice; }
        //    set { strDTotalPrice = value; }
        //}

        //private decimal strPrice;
        //[DataField("Price", "decimal")]
        //public decimal Price
        //{
        //    get { return strPrice; }
        //    set { strPrice = value; }
        //}
        //private decimal strSubtotal;
        //[DataField("Subtotal", "decimal")]
        //public decimal Subtotal
        //{
        //    get { return strSubtotal; }
        //    set { strSubtotal = value; }
        //}
        //private string strTechnology;
        //[DataField("Technology", "nvarchar")]
        //public string Technology
        //{
        //    get { return strTechnology; }
        //    set { strTechnology = value; }
        //}
        //private DateTime strDeliveryTime;
        //[DataField("DeliveryTime", "datetime")]
        //public DateTime DeliveryTime
        //{
        //    get { return strDeliveryTime; }
        //    set { strDeliveryTime = value; }
        //}
        //private string strState;
        //[DataField("State", "varchar")]
        //public string State
        //{
        //    get { return strState; }
        //    set { strState = value; }
        //}

        //private string strRemark = "";
        //[DataField("Remark", "nvarchar")]
        //public string Remark
        //{
        //    get { return strRemark; }
        //    set { strRemark = value; }
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
        #endregion

        private string strPID;
        [DataField("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        [Required(ErrorMessage = "订货单号不能为空")]
        [StringLength(20, ErrorMessage = "订货单号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        private string strOrderID;
        [DataField("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }
        private string strDID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strChannel = "";        //渠道--新增
        [DataField("Channel", "nvarchar")]
        public string Channel
        {
            get { return strChannel; }
            set { strChannel = value; }
        }

        private string strProductID;
        [Required(ErrorMessage = "物料编码不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        private string strOrderContent;
        [Required(ErrorMessage = "订货内容不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        private string strSpecsModels;
        [Required(ErrorMessage = "规格型号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("SpecsModels", "nvarchar")]
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }
        private string strManufacturer;
        [Required(ErrorMessage = "生产厂家不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Manufacturer", "nvarchar")]
        public string Manufacturer
        {
            get { return strManufacturer; }
            set { strManufacturer = value; }
        }
        private string strOrderUnit;
        [Required(ErrorMessage = "订货单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderUnit", "nvarchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }

        private int strOrderNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderNum", "int")]
        public int OrderNum
        {
            get { return strOrderNum; }
            set { strOrderNum = value; }
        }
        //实际数量
        private int strActualAmount;
         [DataField("ActualAmount", "int")]
        public int ActualAmount
        {
            get { return strActualAmount; }
            set { strActualAmount = value; }
        }

        private int strShipmentNum;
          [DataField("ShipmentNum", "int")]
        public int ShipmentNum
        {
            get { return strShipmentNum; }
            set { strShipmentNum = value; }
        }

        private decimal strTaxUnitPrice;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("TaxUnitPrice", "decimal")]
        public decimal TaxUnitPrice
        {
            get { return strTaxUnitPrice; }
            set { strTaxUnitPrice = value; }
        }

        private decimal strUnitPrice;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UnitPrice", "decimal")]
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }
        private string strTaxRate;
         [DataField("TaxRate", "varchar")]
        public string TaxRate
        {
            get { return strTaxRate; }
            set { strTaxRate = value; }
        }
        private decimal strDUnitPrice;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("DUnitPrice", "decimal")]
        public decimal DUnitPrice
        {
            get { return strDUnitPrice; }
            set { strDUnitPrice = value; }
        }

        private decimal strDTotalPrice;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("DTotalPrice", "decimal")]
        public decimal DTotalPrice
        {
            get { return strDTotalPrice; }
            set { strDTotalPrice = value; }
        }

        private decimal strPrice;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Price", "decimal")]
        public decimal Price
        {
            get { return strPrice; }
            set { strPrice = value; }
        }
        private decimal strSubtotal;
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Subtotal", "decimal")]
        public decimal Subtotal
        {
            get { return strSubtotal; }
            set { strSubtotal = value; }
        }

        //实际金额
        private decimal strActualSubTotal;
         [DataField("ActualSubTotal", "decimal")]
        public decimal ActualSubTotal
        {
            get { return strActualSubTotal; }
            set { strActualSubTotal = value; }
        }
        private decimal strUnitCost;
        [DataField("UnitCost","decimal")]
        public decimal UnitCost {
            get { return strUnitCost; }
            set { strUnitCost = value; }
        }

        private decimal strTotalCost;
        [DataField("TotalCost", "decimal")]
        public decimal TotalCost 
        {
            get { return strTotalCost; }
            set { strTotalCost = value; }
        }

        private string strSaleNo;
        [DataField("SaleNo", "varchar")]
        public string SaleNo {

            get { return strSaleNo; }
            set { strSaleNo = value; }
        }

        private string strProjectNo;
         [DataField("ProjectNo", "varchar")]
        public string ProjectNo 
        {

            get { return strProjectNo; }
            set { strProjectNo = value; }
        
        }

         private string strJNAME;
        [DataField("JNAME", "varchar")]
         public string JNAME {
             get { return strJNAME; }
             set { strJNAME = value; }
         
         }




        private string strTechnology;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("Technology", "nvarchar")]
        public string Technology
        {
            get { return strTechnology; }
            set { strTechnology = value; }
        }
        private DateTime strDeliveryTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("DeliveryTime", "datetime")]
        public DateTime DeliveryTime
        {
            get { return strDeliveryTime; }
            set { strDeliveryTime = value; }
        }
        private string strState;
        [DataField("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strIState;
        [DataField("IState", "varchar")]
        public string IState
        {
            get { return strIState; }
            set { strIState = value; }
        }


        private string strRemark = "";
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strBelongCom = "";              //2016-2-24新增，所属分公司
        [DataField("BelongCom", "nvarchar")]
        public string BelongCom
        {
            get { return strBelongCom; }
            set { strBelongCom = value; }
        }

        private string strCreateTime;
        [DataField("CreateTime", "datetime")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
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
    }
}
