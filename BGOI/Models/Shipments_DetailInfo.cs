using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Shipments_DetailInfo
    {

        private string strProductID;
        [DataField("ProductID", "varchar")]
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }

        public string strShipGoodsID = "";
        [DataFieldAttribute("ShipGoodsID", "varchar")]
        //发货单编号
        public string ShipGoodsID
        {
            get { return strShipGoodsID; }
            set { strShipGoodsID = value; }
        }

        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //关联订单
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }


        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        public string strSpecifications = "";
        [DataFieldAttribute("Specifications", "varchar")]
        //规格型号
        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }

        public string strSupplier = "";
        [DataFieldAttribute("Supplier", "Supplier")]
        public string Supplier
        {
            get { return strSupplier; }
            set { strSupplier = value; }
        }
      

        private decimal strPrice;
        [DataField("Price", "decimal")]
        public decimal Price
        {
            get { return strPrice; }
            set { strPrice = value; }
        }
        //public decimal strSubtotal = 0;
        //[DataFieldAttribute("Subtotal", "varchar")]
        //public decimal Subtotal
        //{
        //    get { return strSubtotal; }
        //    set { strSubtotal = value; }
        //}
        public int strAmount = 0;
        [DataFieldAttribute("Amount", "varchar")]
        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }
        private string strUnit;
        [DataField("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private DateTime strCreateTime;
        [DataField("CreateTime", "DateTime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strCreateUser;
        [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataField("Validate","varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
