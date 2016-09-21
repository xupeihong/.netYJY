using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductTask
    {
        private string strRWID = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWID", "varchar")]
        
        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
        }

        private string strUnitID = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }

        private string strOrderID = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderID", "varchar")]
        public string OrderID
        {
            get { return strOrderID; }
            set { strOrderID = value; }
        }

        private DateTime dateContractDate;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContractDate", "DateTime")]
        public DateTime ContractDate
        {
            get { return dateContractDate; }
            set { dateContractDate = value; }
        }

        private string strClientcode = "";
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Clientcode", "varchar")]
        public string Clientcode
        {
            get { return strClientcode; }
            set { strClientcode = value; }
        }

        private string strOrderUnit = "";
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderUnit", "varchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }

        private string strOrderContactor = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContactor", "varchar")]
        public string OrderContactor
        {
            get { return strOrderContactor; }
            set { strOrderContactor = value; }
        }

        private string strOrderTel = "";
        [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderTel", "varchar")]
        public string OrderTel
        {
            get { return strOrderTel; }
            set { strOrderTel = value; }
        }

        private string strOrderAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderAddress", "varchar")]
        public string OrderAddress
        {
            get { return strOrderAddress; }
            set { strOrderAddress = value; }
        }

        private string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strPreparation = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Preparation", "varchar")]
        public string Preparation
        {
            get { return strPreparation; }
            set { strPreparation = value; }
        }

        private string strState="";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strCreateUser = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private DateTime strCreateTime;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strValidate = "";
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private DateTime strMaterialcompletiontime;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Materialcompletiontime", "DateTime")]
        public DateTime Materialcompletiontime
        {
            get { return strMaterialcompletiontime; }
            set { strMaterialcompletiontime = value; }
        }

        private DateTime strStarttime ;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Starttime", "DateTime")]
        public DateTime Starttime
        {
            get { return strStarttime; }
            set { strStarttime = value; }
        }

        private DateTime strProductioncompletiontime;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Productioncompletiontime", "DateTime")]
        public DateTime Productioncompletiontime
        {
            get { return strProductioncompletiontime; }
            set { strProductioncompletiontime = value; }
        }

        private DateTime strStoragetime;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Storagetime", "DateTime")]
        public DateTime Storagetime
        {
            get { return strStoragetime; }
            set { strStoragetime = value; }
        }

        private DateTime strCancelTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelTime", "DateTime")]
        public DateTime CancelTime
        {
            get { return strCancelTime; }
            set { strCancelTime = value; }
        }

        private string strCancelReason = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelReason", "nvarchar")]
        public string CancelReason
        {
            get { return strCancelReason;}
            set { strCancelReason = value;}
        }

        //一次修改 主表添加技术要求字段
        private string strTechnology = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Technology", "nvarchar")]
        public string Technology
        {
            get { return strTechnology; }
            set { strTechnology = value; }
        }

        //一次修改 主表添加备注字段
        private string strNote = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Note", "nvarchar")]
        public string Note
        {
            get { return strNote; }
            set { strNote = value; }
        }

        //一次修改 主表添加编号字段
        private string strID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ID", "nvarchar")]
        public string ID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }
        //库房
        private string strHouseID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HouseID", "nvarchar")]
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }
    }
}
