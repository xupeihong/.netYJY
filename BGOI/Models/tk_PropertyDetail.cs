using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_PropertyDetail
    {
        private string m_strPAID = "";
        [DataField("PAID", "varchar")]
        public string PAID
        {
            get { return m_strPAID; }
            set { m_strPAID = value; }
        }

        private string m_strDID = "";
        [DataField("DID", "varchar")]
        public string DID
        {
            get { return m_strDID; }
            set { m_strDID = value; }
        }

        private string m_strProductID = "";
        [DataField("ProductID", "nvarchar")]
        public string ProductID
        {
            get { return m_strProductID; }
            set { m_strProductID = value; }
        }

        private string m_strFactoryNum = "";
        [DataField("FactoryNum", "nvarchar")]
        public string FactoryNum
        {
            get { return m_strFactoryNum; }
            set { m_strFactoryNum = value; }
        }

        private string m_strOrderContent = "";
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return m_strOrderContent; }
            set { m_strOrderContent = value; }
        }

        private string m_strPtype = "";
        [DataField("Ptype", "nvarchar")]
        public string Ptype
        {
            get { return m_strPtype; }
            set { m_strPtype = value; }
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

        private int intAmount = 0;
        [DataField("Amount", "int")]
        public int Amount
        {
            get { return intAmount; }
            set { intAmount = value; }
        }

        private decimal dcUnitPrice;
        [DataField("UnitPrice", "decimal")]
        public decimal UnitPrice
        {
            get { return dcUnitPrice; }
            set { dcUnitPrice = value; }
        }

        private decimal dcTotal;
        [DataField("Total", "decimal")]
        public decimal Total
        {
            get { return dcTotal; }
            set { dcTotal = value; }
        }

        private string m_strBusinessType = "";
        [DataField("BusinessType", "nvarchar")]
        public string BusinessType
        {
            get { return m_strBusinessType; }
            set { m_strBusinessType = value; }
        }

        private string m_strOperateType = "";
        [DataField("OperateType", "nvarchar")]
        public string OperateType
        {
            get { return m_strOperateType; }
            set { m_strOperateType = value; }
        }

        private string m_strState = "";
        [DataField("State", "nvarchar")]
        public string State
        {
            get { return m_strState; }
            set { m_strState = value; }
        }

        private string m_strIState = "";
        [DataField("IState", "nvarchar")]
        public string IState
        {
            get { return m_strIState; }
            set { m_strIState = value; }
        }
        private string m_strRemark = "";
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }

        private DateTime strCreateTime;
        [DataField("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string m_strCreateUser = "";
        [DataField("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return m_strCreateUser; }
            set { m_strCreateUser = value; }
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
