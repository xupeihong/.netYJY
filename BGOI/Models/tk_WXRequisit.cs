using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_WXRequisit
    {
        public string strBXID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXID", "varchar")]
        //报装编号
        public string BXID
        {
            get { return strBXID; }
            set { strBXID = value; }
        }
        public string strUntiID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UntiID", "varchar")]
        //报修建立单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strRepairID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairID", "varchar")]
        //报修编号
        public string RepairID
        {
            get { return strRepairID; }
            set { strRepairID = value; }
        }
        public string strCustomer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Customer", "varchar")]
        //用户名称
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }
        public string strContactName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContactName", "varchar")]
        //联系人
        public string ContactName
        {
            get { return strContactName; }
            set { strContactName = value; }
        }
        public string strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Address", "varchar")]
        //地址
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strDeviceName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeviceName", "varchar")]
        //设备名称
        public string DeviceName
        {
            get { return strDeviceName; }
            set { strDeviceName = value; }
        }
        public string strDeviceType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeviceType", "varchar")]
        //设备型号
        public string DeviceType
        {
            get { return strDeviceType; }
            set { strDeviceType = value; }
        }
        public DateTime strEnableDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("EnableDate", "datetime")]
        //启用日期
        public DateTime EnableDate
        {
            get { return strEnableDate; }
            set { strEnableDate = value; }
        }
        public string strGuaranteePeriod = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("GuaranteePeriod", "varchar")]
        //保修期
        public string GuaranteePeriod
        {
            get { return strGuaranteePeriod; }
            set { strGuaranteePeriod = value; }
        }
        public string strBXKNum = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXKNum", "varchar")]
        //保修卡编号
        public string BXKNum
        {
            get { return strBXKNum; }
            set { strBXKNum = value; }
        }
        public string strRepairTheUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairTheUser", "varchar")]
        //用户报修简述
        public string RepairTheUser
        {
            get { return strRepairTheUser; }
            set { strRepairTheUser = value; }
        }
        public DateTime strRepairDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairDate", "datetime")]
        //报修日期
        public DateTime RepairDate
        {
            get { return strRepairDate; }
            set { strRepairDate = value; }
        }
        public string strSate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Sate", "varchar")]
        //未维修，已维修
        public string Sate
        {
            get { return strSate; }
            set { strSate = value; }
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
        public string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
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
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        //登记人
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strDeviceID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeviceID", "varchar")]
        //设备编号
        public string DeviceID
        {
            get { return strDeviceID; }
            set { strDeviceID = value; }
        }
        
        //public DateTime strCollectionTime;
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        //[DataFieldAttribute("CollectionTime", "datetime")]
        ////收款时间
        //public DateTime CollectionTime
        //{
        //    get { return strCollectionTime; }
        //    set { strCollectionTime = value; }
        //}
      
    }
}
