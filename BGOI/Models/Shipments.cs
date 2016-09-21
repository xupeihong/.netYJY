using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
    public class Shipments
    {

        [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string strShipGoodsID = "";
        [DataFieldAttribute("ShipGoodsID", "varchar")]
        public string ShipGoodsID
        {
            get { return strShipGoodsID; }
            set { strShipGoodsID = value; }
        }
        private string strOrderID;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }


        private string strContractID;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("ContractID", "varchar")]
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }
        public DateTime strShipmentDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ShipmentDate", "datetime")]
        //发货日期
        public DateTime ShipmentDate
        {
            get { return strShipmentDate; }
            set { strShipmentDate = value; }
        }

        public string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public string strOrderer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Orderer", "varchar")]
        //订货人
        public string Orderer
        {
            get { return strOrderer; }
            set { strOrderer = value; }
        }
        public string strShippers = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Shippers", "varchar")]
        //发货人
        public string Shippers
        {
            get { return strShippers; }
            set { strShippers = value; }
        }

        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        //创建人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建日期
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        //  Validate, , 
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        //0为没删除  1为删除
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
