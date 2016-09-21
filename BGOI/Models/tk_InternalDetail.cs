using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_InternalDetail
    {
        private string m_strDID = "";
        [DataField("DID", "varchar")]
        public string DID
        {
            get { return m_strDID; }
            set { m_strDID = value; }
        }

        private string m_strIOID = "";
        [DataField("IOID", "varchar")]
        public string IOID
        {
            get { return m_strIOID; }
            set { m_strIOID = value; }
        }

        private string m_strProductID = "";
        [DataField("ProductID", "nvarchar")]
        public string ProductID
        {
            get { return m_strProductID; }
            set { m_strProductID = value; }
        }

        private string m_strOrderContent = "";
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return m_strOrderContent; }
            set { m_strOrderContent = value; }
        }

        private string m_strGoodsType = "";
        [DataField("GoodsType", "nvarchar")]
        public string GoodsType
        {
            get { return m_strGoodsType; }
            set { m_strGoodsType = value; }
        }

        private string m_strSpecifications = "";
        [DataField("Specifications", "nvarchar")]
        public string Specifications
        {
            get { return m_strSpecifications; }
            set { m_strSpecifications = value; }
        }

        private string m_strSupplier = "";
        [DataField("Supplier", "nvarchar")]
        public string Supplier
        {
            get { return m_strSupplier; }
            set { m_strSupplier = value; }
        }

        private string m_strUnit = "";
        [DataField("Unit", "nvarchar")]
        public string Unit
        {
            get { return m_strUnit; }
            set { m_strUnit = value; }
        }

        private int IntAmount = 0;
        [DataField("Amount", "int")]
        public int Amount
        {
            get { return IntAmount; }
            set { IntAmount = value; }
        }

        private decimal strUnitPrice;
        [DataField("UnitPrice", "decimal")]
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }

        private decimal strDiscounts;
        [DataField("Discounts", "decimal")]
        public decimal Discounts
        {
            get { return strDiscounts; }
            set { strDiscounts = value; }
        }

        private decimal strTotal;
        [DataField("Total", "decimal")]
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }

        private string m_strPayWay = "";
        [DataField("PayWay", "nvarchar")]
        public string PayWay
        {
            get { return m_strPayWay; }
            set { m_strPayWay = value; }
        }

        private string m_strRemark = "";
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }

        private string m_strState = "";
        [DataField("State", "varchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strIState = "";
        [DataField("IState", "varchar")]
        public string IState
        {
            get { return m_strIState; }
            set { m_strIState = value; }
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
