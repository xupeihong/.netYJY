using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_WareHouse
    {

        public string strHouseID = "";
        [DataFieldAttribute("HouseID", "varchar")]
        //仓库ID
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }
        public string strAdress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Adress", "varchar")]
        //仓库地址
        public string Adress
        {
            get { return strAdress; }
            set { strAdress = value; }
        }
        public string strHouseName = "";
        [StringLength(20, ErrorMessage = "仓库名不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       // [Remote("HouseNameExists", "InventoryManage", ErrorMessage = "该仓库名已存在")] 
        
        [DataFieldAttribute("HouseName", "varchar")]
        //仓库名称
        public string HouseName
        {
            get { return strHouseName; }
            set { strHouseName = value; }
        }
        public int strLevel;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Level", "int")]
        public int Level
        {
            get { return strLevel; }
            set { strLevel = value; }
        }
        public string strUnitID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }
        public string strDelReason = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DelReason", "varchar")]
        //删除原因
        public string DelReason
        {
            get { return strDelReason; }
            set { strDelReason = value; }
        }
        public DateTime strDelTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DelTime", "datetime")]
        //删除时间
        public DateTime DelTime
        {
            get { return strDelTime; }
            set { strDelTime = value; }
        }
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strIsHouseID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsHouseID", "varchar")]
        //1 为1级库  2为2级库
        public string IsHouseID
        {
            get { return strIsHouseID; }
            set { strIsHouseID = value; }
        }
        public string strTypeID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TypeID", "varchar")]
        //货品库类型
        public string TypeID
        {
            get { return strTypeID; }
            set { strTypeID = value; }
        }
    }
}
