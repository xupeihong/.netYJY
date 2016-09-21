using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_EquipmentDebugging_HIS
    {
        public string strTRID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TRID", "varchar")]
        //调试任务编号
        public string TRID
        {
            get { return strTRID; }
            set { strTRID = value; }
        }
        public string strUserName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserName", "varchar")]
        //用户名称
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        public string strAddress = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Address", "varchar")]
        //用户地址
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        public string strContactPerson = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContactPerson", "varchar")]
        //联系人
        public string ContactPerson
        {
            get { return strContactPerson; }
            set { strContactPerson = value; }
        }
        public string strConstructionUnit = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ConstructionUnit", "varchar")]
        //施工单位
        public string ConstructionUnit
        {
            get { return strConstructionUnit; }
            set { strConstructionUnit = value; }
        }
        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //联系电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        public string strCUnitPer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CUnitPer", "varchar")]
        //施工单位联系人
        public string CUnitPer
        {
            get { return strCUnitPer; }
            set { strCUnitPer = value; }
        }
        public string strCUnitTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CUnitTel", "varchar")]
        //施工单位联系电话
        public string CUnitTel
        {
            get { return strCUnitTel; }
            set { strCUnitTel = value; }
        }
        public string strEquManType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("EquManType", "varchar")]
        //设备管理方式   自管   厂家代管   输配公司代管   燃气施工方式代管    其他公司代管
        public string EquManType
        {
            get { return strEquManType; }
            set { strEquManType = value; }
        }
        public string strUnitName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitName", "varchar")]
        //单位名称
        public string UnitName
        {
            get { return strUnitName; }
            set { strUnitName = value; }
        }
        public string strUnitTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitTel", "varchar")]
        //单位电话
        public string UnitTel
        {
            get { return strUnitTel; }
            set { strUnitTel = value; }
        }
        public string strUnitPer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitPer", "varchar")]
        //单位联系人
        public string UnitPer
        {
            get { return strUnitPer; }
            set { strUnitPer = value; }
        }
        public string strDebPerson = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebPerson", "varchar")]
        //调试人员
        public string DebPerson
        {
            get { return strDebPerson; }
            set { strDebPerson = value; }
        }
        public DateTime strDebTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DebTime", "datetime")]
        //调试日期
        public DateTime DebTime
        {
            get { return strDebTime; }
            set { strDebTime = value; }
        }
        public string strUserType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserType", "varchar")]
        //用户类型   锅炉  直燃机   公福   居民户  其它
        public string UserType
        {
            get { return strUserType; }
            set { strUserType = value; }
        }
        public string strGas = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Gas", "varchar")]
        //气种   天然气  人工煤气  液化石油气 混气  沼气  压缩天然气   其它
        public string Gas
        {
            get { return strGas; }
            set { strGas = value; }
        }
        public string strFieldFailure = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FieldFailure", "varchar")]
        //现场故障情况
        public string FieldFailure
        {
            get { return strFieldFailure; }
            set { strFieldFailure = value; }
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
        //创建人
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
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }
    }
}

