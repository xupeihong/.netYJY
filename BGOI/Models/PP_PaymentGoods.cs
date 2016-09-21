using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_PaymentGoods
    {
        private string strPayId;
        [DataFieldAttribute("PayId", "varchar")]
        public string PayId
        {
            get { return strPayId; }
            set { strPayId = value; }
        }

        private string strPayXid;
        [DataFieldAttribute("PayXid", "varchar")]
        public string PayXid
        {
            get { return strPayXid; }
            set { strPayXid = value; }
        }


        private string strLJCPID;
        [DataFieldAttribute("LJCPID", "varchar")]
        public string LJCPID
        {
            get { return strLJCPID; }
            set { strLJCPID = value; }
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
        /// <summary>
        /// 单价（不含税）
        /// </summary>
        private decimal dclUnitPriceNoTax;
        [DataFieldAttribute("UnitPriceNoTax", "varchar")]
        public decimal UnitPriceNoTax
        {
            get { return dclUnitPriceNoTax; }
            set { dclUnitPriceNoTax = value; }
        }
        /// <summary>
        ///金额(不含税)
        /// </summary>
        private decimal dclTotalNoTax;
        [DataFieldAttribute("TotalNoTax", "varchar")]
        public decimal TotalNoTax
        {
            get { return dclTotalNoTax; }
            set { dclTotalNoTax = value; }
        }
        /// <summary>
        /// 单价(含税)
        /// </summary>
        private double dbeUnitPrice;
        [DataFieldAttribute("UnitPrice", "varchar")]
        public double UnitPrice
        {
            get { return dbeUnitPrice; }
            set { dbeUnitPrice = value; }
        }
        /// <summary>
        /// 金额(含税)
        /// </summary>
        private double dbeTotal;
        [DataFieldAttribute("Total", "varchar")]
        public double Total
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

        private string strINID;
        [DataFieldAttribute("INID", "varchar")]
        public string INID
        {
            get { return strINID; }
            set { strINID = value; }
        }


        private string strDID;
        [DataFieldAttribute("DID", "varchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        //private string strShuLiang;
        //[DataFieldAttribute("ShuLiang", "varchar")]
        //public string ShuLiang
        //{
        //    get { return strShuLiang; }
        //    set { strShuLiang = value; }
        //}
    }
}
