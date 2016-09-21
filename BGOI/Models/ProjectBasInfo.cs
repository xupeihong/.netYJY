using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
    public class ProjectBasInfo
    {
        private string strPID = "";

        [DataFieldAttribute("PID", "nvarchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        private string strRecordID;
        //[Required(ErrorMessage = "编号不能为空")]
        //[StringLength(20, ErrorMessage = "编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("RecordID", "nvarchar")]
        public string RecordID
        {
            get { return strRecordID; }
            set { strRecordID = value; }
        }

        private string strUnitID;
       // [Required(ErrorMessage = "所属单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("UnitID", "nvarchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }


        private DateTime strRecordDate;
        [Required(ErrorMessage = "日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RecordDate", "DateTime")]
        public DateTime RecordDate
        {
            get { return strRecordDate; }
            set { strRecordDate = value; }
        }

        private string strExpectedTime;
        public string ExpectedTime
        {
            get { return strExpectedTime; }
            set { strExpectedTime = value; }
        }


        private string strPlanID = "";
        [Required(ErrorMessage = "工程编号不能为空")]
        [StringLength(20, ErrorMessage = "工程编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PlanID", "nvarchar")]
        public string PlanID
        {
            get { return strPlanID; }
            set { strPlanID = value; }
        }

        private string strPlanName = "";
        [Required(ErrorMessage = "项目名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataField("PlanName", "nvarchar")]
        public string PlanName
        {
            get { return strPlanName; }
            set { strPlanName = value; }
        }


        private string strMainContent;
       // [Required(ErrorMessage = "内容不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MainContent", "nvarchar")]
        public string MainContent
        {
            get { return strMainContent; }
            set { strMainContent = value; }
        }

        //业务负责人
        private string strWorkChief = "";
        //[Required(ErrorMessage = "业务负责人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("WorkChief", "nvarchar")]
        public string WorkChief
        {
            get { return strWorkChief; }
            set { strWorkChief = value; }
        }

        //施工方

        private string strConstructor = "";
       // [Required(ErrorMessage = "施工方不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Constructor", "nvarchar")]
        public string Constructor
        {
            get { return strConstructor; }
            set { strConstructor = value; }
        }

        //电话

        private string strTel = "";
       // [RegularExpression(@"^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$", ErrorMessage = "请输入正确的联系电话格式")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "nvarchar")]
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        //所属区域

        private string strBelongArea = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BelongArea", "nvarchar")]
        public string BelongArea
        {
            get { return strBelongArea; }
            set { strBelongArea = value; }
        }


        private string strChannelsFrom = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ChannelsFrom", "nvarchar")]
        //渠道来源
        public string ChannelsFrom
        {
            get { return strChannelsFrom; }
            set { strChannelsFrom = value; }
        }

        private string strRemark;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        //备案人
        
        private string strManager = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Manager", "nvarchar")]
        public string Manager
        {
            get { return strManager; }
            set { strManager = value; }
        }
       
        private string strIsFilings;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsFilings", "nvarchar")]
        public string IsFilings
        {
            get { return strIsFilings; }
            set { strIsFilings = value; }
        }
        
        private string strIsOffer;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsOffer", "nvarchar")]
        public string IsOffer
        {
            get { return strIsOffer; }
            set { strIsOffer = value; }
        }
       
        private string strIsOrder;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsOrder", "nvarchar")]
        public string IsOrder
        {
            get { return strIsOrder; }
            set { strIsOrder = value; }
        }

        private string strSpecsModels;
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SpecsModels", "nvarchar")]
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }
        
        private int strShipmentsState;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ShipmentsState", "int")]
        public int ShipmentsState
        {
            get { return strShipmentsState; }
            set { strShipmentsState = value; }
        }

        
        private string strIsQDCompact;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsQDCompact", "nvarchar")]
        public string IsQDCompact
        {
            get { return strIsQDCompact; }
            set { strIsQDCompact = value; }
        }
        
        private int strBackGoods;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BackGoods", "int")]
        public int BackGoods
        {
            get { return strBackGoods; }
            set { strBackGoods = value; }
        }
        
        private int strChangeGoods;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ChangeGoods", "int")]
        public int ChangeGoods
        {
            get { return strChangeGoods; }
            set { strChangeGoods = value; }
        }
       
        private string strIsPay;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("IsPay", "nvarchar")]
        public string IsPay
        {
            get { return strIsPay; }
            set { strIsPay = value; }
        }

        private DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate;
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private string strState;
        [DataFieldAttribute("State", "nvarchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strPstate;
         [DataFieldAttribute("Pstate", "nvarchar")]
        public string Pstate
        {
            get { return strPstate; }
            set { strPstate = value; }
        }

         private string strISF;
          [DataFieldAttribute("ISF", "nvarchar")]
         public string ISF
         {
             get { return strISF; }
             set { strISF = value; }
         }

    }
}
