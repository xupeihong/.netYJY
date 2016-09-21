using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_WXRequisit_HIS
    {
        public string strBXID = "";
        [DataFieldAttribute("BXID", "varchar")]
        //报装编号
        public string BXID
        {
            get { return strBXID; }
            set { strBXID = value; }
        }
        public string strUntiID = "";
        [DataFieldAttribute("UntiID", "varchar")]
        //报修建立单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strRepairID = "";
        [DataFieldAttribute("RepairID", "varchar")]
        //报修编号
        public string RepairID
        {
            get { return strRepairID; }
            set { strRepairID = value; }
        }
        public string strCustomer = "";
        [DataFieldAttribute("Customer", "varchar")]
        //用户名称
        public string Customer
        {
            get { return strCustomer; }
            set { strCustomer = value; }
        }
        public string strContactName = "";
        [DataFieldAttribute("ContactName", "varchar")]
        //联系人
        public string ContactName
        {
            get { return strContactName; }
            set { strContactName = value; }
        }
        public string strAddress = "";
        [DataFieldAttribute("Address", "varchar")]
        //地址
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strTel = "";
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strDeviceName = "";
        [DataFieldAttribute("DeviceName", "varchar")]
        //设备名称
        public string DeviceName
        {
            get { return strDeviceName; }
            set { strDeviceName = value; }
        }
        public string strDeviceType = "";
        [DataFieldAttribute("DeviceType", "varchar")]
        //设备型号
        public string DeviceType
        {
            get { return strDeviceType; }
            set { strDeviceType = value; }
        }
        public DateTime strEnableDate;
        [DataFieldAttribute("EnableDate", "datetime")]
        //启用日期
        public DateTime EnableDate
        {
            get { return strEnableDate; }
            set { strEnableDate = value; }
        }
        public string strGuaranteePeriod = "";
        [DataFieldAttribute("GuaranteePeriod", "varchar")]
        //保修期
        public string GuaranteePeriod
        {
            get { return strGuaranteePeriod; }
            set { strGuaranteePeriod = value; }
        }
        //, , , , Remark, CreateTime, CreateUser, Validate
        public string strBXKNum = "";
        [DataFieldAttribute("BXKNum", "varchar")]
        //保修卡编号
        public string BXKNum
        {
            get { return strBXKNum; }
            set { strBXKNum = value; }
        }
        public string strRepairTheUser = "";
        [DataFieldAttribute("RepairTheUser", "varchar")]
        //用户报修简述
        public string RepairTheUser
        {
            get { return strRepairTheUser; }
            set { strRepairTheUser = value; }
        }
        public DateTime strRepairDate;
        [DataFieldAttribute("RepairDate", "datetime")]
        //报修日期
        public DateTime RepairDate
        {
            get { return strRepairDate; }
            set { strRepairDate = value; }
        }
        public string strSate = "";
        [DataFieldAttribute("Sate", "varchar")]
        //未维修，已维修
        public string Sate
        {
            get { return strSate; }
            set { strSate = value; }
        }
        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        //登记人
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        private DateTime? _NCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return _NCreateTime; }
            set { _NCreateTime = value; }
        }
        private string _NCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return _NCreateUser; }
            set { _NCreateUser = value; }
        }
        private string _DeviceID;
        [DataFieldAttribute("DeviceID", "varchar")]
        public string DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
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