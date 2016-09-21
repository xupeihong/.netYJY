using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class PP_OrderGoods
    {
        private string strDDID;
        [DataFieldAttribute("DDID", "varchar")]
        public string DDID
        {
            get { return strDDID; }
            set { strDDID = value; }
        }


        private string strSJFK;
        [DataFieldAttribute("SJFK", "varchar")]
        public string SJFK
        {
            get { return strSJFK; }
            set { strSJFK = value; }
        }

        private string strRKState;
        [DataFieldAttribute("RKState", "varchar")]
        public string RKState
        {
            get { return strRKState; }
            set { strRKState = value; }
        }

        private string strDrawingNum;
        [DataFieldAttribute("DrawingNum", "nvarchar")]
        public string DrawingNum
        {
            get { return strDrawingNum; }
            set { strDrawingNum = value; }
        }

        private string strDID;
       [DataFieldAttribute("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }


       private string strLJCPID;
       [DataFieldAttribute("LJCPID", "varchar")]
       public string LJCPID
       {
           get { return strLJCPID; }
           set { strLJCPID = value; }
       }


       /// <summary>
       /// 物品物料编码
       /// </summary>
       private string strMaterialNO;
       [DataFieldAttribute("MaterialNO", "varchar")]
       public string MaterialNO
       {
           get { return strMaterialNO; }
           set { strMaterialNO = value; }
       }
       /// <summary>
       /// 产品名称
       /// </summary>
       private string strOrderContent;
       [DataFieldAttribute("OrderContent", "varchar")]
       public string OrderContent
       {
           get { return strOrderContent; }
           set { strOrderContent = value; }
       }
       /// <summary>
       /// 规格型号
       /// </summary>
       private string strSpecifications;
       [DataFieldAttribute("Specifications", "varchar")]
       public string Specifications
       {
           get { return strSpecifications; }
           set { strSpecifications = value; }
       }
       /// <summary>
       /// 生产厂家
       /// </summary>
       private string strSupplier;
       [DataFieldAttribute("Supplier", "varchar")]
       public string Supplier
       {
           get { return strSupplier; }
           set { strSupplier = value; }
       }

       private string strUnit;
       [DataFieldAttribute("Unit", "varchar")]
       public string Unit
       {
           get { return strUnit; }
           set { strUnit = value; }
       }
       /// <summary>
       /// 进货数量
       /// </summary>
       private string strAmount;
       [DataFieldAttribute("Amount", "varchar")]
       public string Amount
       {
           get { return strAmount; }
           set { strAmount = value; }
       }


       private int intActualAmount;
       [DataFieldAttribute("ActualAmount", "int")]
       public int ActualAmount
       {
           get { return intActualAmount; }
           set { intActualAmount = value; }
       }
       /// <summary>
       /// 单价（不含税）
       /// </summary>
       private decimal dclUnitPriceNoTax;
       [DataFieldAttribute("UnitPriceNoTax", "decimal")]
       public decimal UnitPriceNoTax
       {
           get { return dclUnitPriceNoTax; }
           set { dclUnitPriceNoTax = value; }
       }
       /// <summary>
       ///金额(不含税)
       /// </summary>
       private decimal dclTotalNoTax;
       [DataFieldAttribute("TotalNoTax", "decimal")]
       public decimal TotalNoTax
       {
           get { return dclTotalNoTax; }
           set { dclTotalNoTax = value; }
       }
       /// <summary>
       /// 单价(含税)
       /// </summary>
       private decimal dbeUnitPrice;
       [DataFieldAttribute("UnitPrice", "decimal")]
       public decimal UnitPrice
       {
           get { return dbeUnitPrice; }
           set { dbeUnitPrice = value; }
       }
       /// <summary>
       /// 金额(含税)
       /// </summary>
       private decimal dbeTotal;
       [DataFieldAttribute("Total", "decimal")]
       public decimal Total
       {
           get { return dbeTotal; }
           set { dbeTotal = value; }
       }

       private string strRate;
       [DataFieldAttribute("Rate", "varchar")]
       public string Rate
       {
           get { return strRate; }
           set { strRate = value; }
       }
       /// <summary>
       /// 用途
       /// </summary>
       private string strUse;
       [DataFieldAttribute("GoodsUse", "varchar")]
       public string GoodsUse
       {
           get { return strUse; }
           set { strUse = value; }
       }
       /// <summary>
       /// 备注
       /// </summary>
       private string strRemark;
       [DataFieldAttribute("Remark", "varchar")]
       public string Remark
       {
           get { return strRemark; }
           set { strRemark = value; }
       }
       /// <summary>
       /// 计划到货日期
       /// </summary>
       private DateTime dtPurchaseDate;
       [DataFieldAttribute("PurchaseDate", "varchar")]
       public DateTime PurchaseDate
       {
           get { return dtPurchaseDate; }
           set { dtPurchaseDate = value; }
       }


       private DateTime dtCreateTime;
       [DataFieldAttribute("CreateTime", "datetime")]
       public DateTime CreateTime
       {
           get { return dtCreateTime; }
           set { dtCreateTime = value; }
       }

       private string strCreateUser;
       [DataFieldAttribute("CreateUser", "nvarchar")]
       public string CreateUser
       {
           get { return strCreateUser; }
           set { strCreateUser = value; }
       }

       private string strValidate;
       [DataFieldAttribute("Validate", "nvarchar")]
       public string Validate
       {
           get { return strValidate; }
           set { strValidate = value; }
       }


       private string strGoodsName;
       [DataFieldAttribute("GoodsName", "nvarchar")]
       public string GoodsName
       {
           get { return strGoodsName; }
           set { strGoodsName = value; }
       }

       private string strGoodsNum;
       [DataFieldAttribute("GoodsNum", "nvarchar")]
       public string GoodsNum
       {
           get { return strGoodsNum; }
           set { strGoodsNum = value; }
       }

       private string strGoodsyiju;
       [DataFieldAttribute("Goodsyiju", "nvarchar")]
       public string Goodsyiju
       {
           get { return strGoodsyiju; }
           set { strGoodsyiju = value; }
       }
    }
}
