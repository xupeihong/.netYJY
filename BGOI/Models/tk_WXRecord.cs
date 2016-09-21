using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_WXRecord
    {
        public string strWXID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("WXID", "varchar")]
        //维修记录编号
        public string WXID
        {
            get { return strWXID; }
            set { strWXID = value; }
        }
        public string strBXID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXID", "varchar")]
        //关联报装编号
        public string BXID
        {
            get { return strBXID; }
            set { strBXID = value; }
        }
        public DateTime strMaintenanceTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaintenanceTime", "datetime")]
        //维修日期
        public DateTime MaintenanceTime
        {
            get { return strMaintenanceTime; }
            set { strMaintenanceTime = value; }
        }
        public string strMaintenanceVehicle = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaintenanceVehicle", "varchar")]
        //维修车辆
        public string MaintenanceVehicle
        {
            get { return strMaintenanceVehicle; }
            set { strMaintenanceVehicle = value; }
        }
        public string strMaintenanceName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaintenanceName", "varchar")]
        //维修人员
        public string MaintenanceName
        {
            get { return strMaintenanceName; }
            set { strMaintenanceName = value; }
        }
        public string strMaintenanceRecord = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaintenanceRecord", "varchar")]
        //维修情况记录
        public string MaintenanceRecord
        {
            get { return strMaintenanceRecord; }
            set { strMaintenanceRecord = value; }
        }
        public string strFinalResults = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinalResults", "varchar")]
        //最终处理结果
        public string FinalResults
        {
            get { return strFinalResults; }
            set { strFinalResults = value; }
        }
        public decimal strArtificialCost;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ArtificialCost", "decimal")]
        //人工费
        public decimal ArtificialCost
        {
            get { return strArtificialCost; }
            set { strArtificialCost = value; }
        }
        public decimal strMaterialCost;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaterialCost", "decimal")]
        //材料费
        public decimal MaterialCost
        {
            get { return strMaterialCost; }
            set { strMaterialCost = value; }
        }
        public decimal strOtherCost;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OtherCost", "decimal")]
        //其他费用
        public decimal OtherCost
        {
            get { return strOtherCost; }
            set { strOtherCost = value; }
        }
        public decimal strTotal;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Total", "decimal")]
        //总计金额
        public decimal Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }
        public string strPaymentMethod = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentMethod", "varchar")]
        //支付方式
        public string PaymentMethod
        {
            get { return strPaymentMethod; }
            set { strPaymentMethod = value; }
        }
        public string strPayee = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Payee", "varchar")]
        //收款人
        public string Payee
        {
            get { return strPayee; }
            set { strPayee = value; }
        }
        public string strSate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Sate", "varchar")]
        //用户意见
        public string Sate
        {
            get { return strSate; }
            set { strSate = value; }
        }
        public string strSignatureName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SignatureName", "varchar")]
        //用户签名姓名
        public string SignatureName
        {
            get { return strSignatureName; }
            set { strSignatureName = value; }
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
        public DateTime strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public DateTime strCollectionTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CollectionTime", "datetime")]
        //收款时间
        public DateTime CollectionTime
        {
            get { return strCollectionTime; }
            set { strCollectionTime = value; }
        }
    }
}
