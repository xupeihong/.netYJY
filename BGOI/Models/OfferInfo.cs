using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class OfferInfo
    {
        private string strBJID;
        [DataField("BJID", "varchar")]
        public string BJID
        {
            get { return strBJID; }
            set { strBJID = value; }
        }
        private string strXID;
        [DataField("XID", "varchar")]
        public string XID
        {
            get { return strXID; }
            set { strXID = value; }
        }
        private string strProductID;
        [DataField("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        private string strOrderContent;
        [DataField("OrderContent", "nvarchar")]
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }
        private string strSpecifications;
        [DataField("Specifications", "nvarchar")]
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }
        private string strSupplier;
        [DataField("Supplier", "nvarchar")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }
        private string strUnit;
        [DataField("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        private int strAmount;
        [DataField("Amount", "int")]
        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }
        private decimal strUnitPriec;
        [DataField("UintPrice", "decimal")]
        public decimal UintPrice
        {
            get { return strUnitPriec; }
            set { strUnitPriec = value; }
        }
        private string strRemark;
        [DataField("Remark", "")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strTotal;
        [DataField("Total", "decimal")]
        public string Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strCreateTime;
        [DataField("CreateTime", "date")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
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
