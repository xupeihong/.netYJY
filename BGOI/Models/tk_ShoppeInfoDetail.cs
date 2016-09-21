using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ShoppeInfoDetail
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

        private string m_strDID = "";
        [DataField("DID", "nvarchar")]
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

        private string m_strOrderContent = "";
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return m_strOrderContent; }
            set { m_strOrderContent = value; }
        }

        private string m_strSpecifications = "";
        [DataField("Specifications", "nvarchar")]
        public string Specifications
        {
            get { return m_strSpecifications; }
            set { m_strSpecifications = value; }
        }

        private string m_strGoodsType = "";
        [DataField("GoodsType", "nvarchar")]
        public string GoodsType
        {
            get { return m_strGoodsType; }
            set { m_strGoodsType = value; }
        }

        private int IntAmount = 0;
        [DataField("Amount", "int")]
        public int Amount
        {
            get { return IntAmount; }
            set { IntAmount = value; }
        }

        private decimal dcPrice;
        [DataField("Price", "decimal")]
        public decimal Price
        {
            get { return dcPrice; }
            set { dcPrice = value; }
        }

        private decimal dcDiscount;
        [DataField("Discount", "decimal")]
        public decimal Discount
        {
            get { return dcDiscount; }
            set { dcDiscount = value; }
        }

        private decimal dcTotal;
        [DataField("Total", "decimal")]
        public decimal Total
        {
            get { return dcTotal; }
            set { dcTotal = value; }
        }

        private string m_strRemark = "";
        [DataField("Remark", "nvarchar")]
        public string Remark
        {
            get { return m_strRemark; }
            set { m_strRemark = value; }
        }

        private string m_strState = "";
        [DataField("State", "nvarchar")]
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
