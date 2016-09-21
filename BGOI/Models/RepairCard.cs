using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class RepairCard
    {
        private string RID;
        private string RepairID;
        private string CustomerName;
        private string CustomerAddr;
        private string S_Name;
        private string S_Tel;
        private string S_Date;

        private string MeterID;
        private string CertifID;
        private string MeterName;
        private string Manufacturer;
        private string Precision;
        private string Model;
        private string ModelType;
        private string FactoryDate;
        private decimal? RecordNum;
        private string FlowRange;
        private string Pressure;
        private string Caliber;
        private string PreUnit;
        private string NewUnit;

        private string X_ID;
        private string X_CertifID;
        private string X_Model;
        private string X_Manufacturer;
        private string X_Standard;
        private string X_Operating;
        private string X_FactoryDate;
        private string X_Pressure;
        private string X_Temperature;
        private string X_Data;
        private string X_PreUnit;
        private string X_NewUnit;

        private string FaceOther;
        private string RepairContent;
        private string CheckUser;
        private string IsRepair;
        private string ConfirmUser;
        private string ConfirmDate;
        private string Text;

        private string GetTypeModel;
        private string G_Name;
        private string G_Tel;
        private string G_Date;
        private int State;
        private string FinishDate;
        private string IsOut;
        private string TakeID;
        private string DeliverID;
        private string RecieveID;
        private string ReceiveUser;
        private string Logistic;

        private DateTime? CreateTime;
        private string CreateUser;
        private string validate;

        private string OutUnit;
        private string SubUnit;
        private string ModelProperty;

        public RepairCard()
        {

            //TODO: 在此处添加构造函数逻辑

        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RID", "varchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [Required(ErrorMessage = "维修编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairID", "varchar")]
        public string strRepairID
        {
            get { return RepairID; }
            set { RepairID = value; }
        }

        [Required(ErrorMessage = "客户名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerName", "varchar")]
        public string strCustomerName
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        [Required(ErrorMessage = "客户地址不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerAddr", "nvarchar")]
        public string strCustomerAddr
        {
            get { return CustomerAddr; }
            set { CustomerAddr = value; }
        }

        [Required(ErrorMessage = "送表人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("S_Name", "nvarchar")]
        public string strS_Name
        {
            get { return S_Name; }
            set { S_Name = value; }
        }

        [DataFieldAttribute("S_Tel", "nvarchar")]
        public string strS_Tel
        {
            get { return S_Tel; }
            set { S_Tel = value; }
        }

        [Required(ErrorMessage = "送表日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("S_Date", "nvarchar")]
        public string strS_Date
        {
            get { return S_Date; }
            set { S_Date = value; }
        }

        [Required(ErrorMessage = "仪表编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MeterID", "nvarchar")]
        public string strMeterID
        {
            get { return MeterID; }
            set { MeterID = value; }
        }

        [Required(ErrorMessage = "证书编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CertifID", "nvarchar")]
        public string strCertifID
        {
            get { return CertifID; }
            set { CertifID = value; }
        }

        [Required(ErrorMessage = "仪表名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MeterName", "nvarchar")]
        public string strMeterName
        {
            get { return MeterName; }
            set { MeterName = value; }
        }

        [DataFieldAttribute("Manufacturer", "nvarchar")]
        public string strManufacturer
        {
            get { return Manufacturer; }
            set { Manufacturer = value; }
        }

        [DataFieldAttribute("Precision", "nvarchar")]
        public string strPrecision
        {
            get { return Precision; }
            set { Precision = value; }
        }

        [Required(ErrorMessage = "仪表型号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Model", "nvarchar")]
        public string strModel
        {
            get { return Model; }
            set { Model = value; }
        }

        [DataFieldAttribute("ModelType", "nvarchar")]
        public string strModelType
        {
            get { return ModelType; }
            set { ModelType = value; }
        }

        [DataFieldAttribute("FactoryDate", "nvarchar")]
        public string strFactoryDate
        {
            get { return FactoryDate; }
            set { FactoryDate = value; }
        }

        [DataFieldAttribute("RecordNum", "decimal")]
        public decimal? strRecordNum
        {
            get { return RecordNum; }
            set { RecordNum = value; }
        }

        [DataFieldAttribute("FlowRange", "nvarchar")]
        public string strFlowRange
        {
            get { return FlowRange; }
            set { FlowRange = value; }
        }

        [DataFieldAttribute("Pressure", "nvarchar")]
        public string strPressure
        {
            get { return Pressure; }
            set { Pressure = value; }
        }

        [DataFieldAttribute("Caliber", "nvarchar")]
        public string strCaliber
        {
            get { return Caliber; }
            set { Caliber = value; }
        }

        [DataFieldAttribute("PreUnit", "nvarchar")]
        public string strPreUnit
        {
            get { return PreUnit; }
            set { PreUnit = value; }
        }

        [DataFieldAttribute("NewUnit", "nvarchar")]
        public string strNewUnit
        {
            get { return NewUnit; }
            set { NewUnit = value; }
        }

        [Required(ErrorMessage = "修正仪编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("X_ID", "nvarchar")]
        public string strX_ID
        {
            get { return X_ID; }
            set { X_ID = value; }
        }

        [Required(ErrorMessage = "修正仪证书编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("X_CertifID", "nvarchar")]
        public string strX_CertifID
        {
            get { return X_CertifID; }
            set { X_CertifID = value; }
        }

        [Required(ErrorMessage = "修正仪型号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("X_Model", "nvarchar")]
        public string strX_Model
        {
            get { return X_Model; }
            set { X_Model = value; }
        }

        [DataFieldAttribute("X_Manufacturer", "nvarchar")]
        public string strX_Manufacturer
        {
            get { return X_Manufacturer; }
            set { X_Manufacturer = value; }
        }

        [DataFieldAttribute("X_Standard", "nvarchar")]
        public string strX_Standard
        {
            get { return X_Standard; }
            set { X_Standard = value; }
        }

        [DataFieldAttribute("X_Operating", "nvarchar")]
        public string strX_Operating
        {
            get { return X_Operating; }
            set { X_Operating = value; }
        }

        [DataFieldAttribute("X_FactoryDate", "nvarchar")]
        public string strX_FactoryDate
        {
            get { return X_FactoryDate; }
            set { X_FactoryDate = value; }
        }

        [DataFieldAttribute("X_Pressure", "nvarchar")]
        public string strX_Pressure
        {
            get { return X_Pressure; }
            set { X_Pressure = value; }
        }

        [DataFieldAttribute("X_Temperature", "nvarchar")]
        public string strX_Temperature
        {
            get { return X_Temperature; }
            set { X_Temperature = value; }
        }

        [DataFieldAttribute("X_Data", "nvarchar")]
        public string strX_Data
        {
            get { return X_Data; }
            set { X_Data = value; }
        }

        [DataFieldAttribute("X_PreUnit", "nvarchar")]
        public string strX_PreUnit
        {
            get { return X_PreUnit; }
            set { X_PreUnit = value; }
        }

        [DataFieldAttribute("X_NewUnit", "nvarchar")]
        public string strX_NewUnit
        {
            get { return X_NewUnit; }
            set { X_NewUnit = value; }
        }

        [DataFieldAttribute("FaceOther", "nvarchar")]
        public string strFaceOther
        {
            get { return FaceOther; }
            set { FaceOther = value; }
        }

        [DataFieldAttribute("RepairContent", "nvarchar")]
        public string strRepairContent
        {
            get { return RepairContent; }
            set { RepairContent = value; }
        }

        [DataFieldAttribute("CheckUser", "nvarchar")]
        public string strCheckUser
        {
            get { return CheckUser; }
            set { CheckUser = value; }
        }

        [DataFieldAttribute("IsRepair", "nvarchar")]
        public string strIsRepair
        {
            get { return IsRepair; }
            set { IsRepair = value; }
        }

        [DataFieldAttribute("ConfirmUser", "nvarchar")]
        public string strConfirmUser
        {
            get { return ConfirmUser; }
            set { ConfirmUser = value; }
        }

        [DataFieldAttribute("ConfirmDate", "nvarchar")]
        public string strConfirmDate
        {
            get { return ConfirmDate; }
            set { ConfirmDate = value; }
        }

        [DataFieldAttribute("Text", "nvarchar")]
        public string strText
        {
            get { return Text; }
            set { Text = value; }
        }

        [DataFieldAttribute("GetTypeModel", "nvarchar")]
        public string strGetTypeModel
        {
            get { return GetTypeModel; }
            set { GetTypeModel = value; }
        }

        [DataFieldAttribute("G_Name", "nvarchar")]
        public string strG_Name
        {
            get { return G_Name; }
            set { G_Name = value; }
        }

        [DataFieldAttribute("G_Tel", "nvarchar")]
        public string strG_Tel
        {
            get { return G_Tel; }
            set { G_Tel = value; }
        }

        [DataFieldAttribute("G_Date", "nvarchar")]
        public string strG_Date
        {
            get { return G_Date; }
            set { G_Date = value; }
        }

        [DataFieldAttribute("State", "int")]
        public int strState
        {
            get { return State; }
            set { State = value; }
        }

        [DataFieldAttribute("FinishDate", "nvarchar")]
        public string strFinishDate
        {
            get { return FinishDate; }
            set { FinishDate = value; }
        }

        [DataFieldAttribute("IsOut", "nvarchar")]
        public string strIsOut
        {
            get { return IsOut; }
            set { IsOut = value; }
        }

        [DataFieldAttribute("TakeID", "nvarchar")]
        public string strTakeID
        {
            get { return TakeID; }
            set { TakeID = value; }
        }

        [DataFieldAttribute("DeliverID", "nvarchar")]
        public string strDeliverID
        {
            get { return DeliverID; }
            set { DeliverID = value; }
        }

        [DataFieldAttribute("RecieveID", "nvarchar")]
        public string strRecieveID
        {
            get { return RecieveID; }
            set { RecieveID = value; }
        }

        [DataFieldAttribute("ReceiveUser", "nvarchar")]
        public string strReceiveUser
        {
            get { return ReceiveUser; }
            set { ReceiveUser = value; }
        }

        [DataFieldAttribute("Logistic", "text")]
        public string strLogistic
        {
            get { return Logistic; }
            set { Logistic = value; }
        }

        [DataFieldAttribute("validate", "nvarchar")]
        public string strvalidate
        {
            get { return validate; }
            set { validate = value; }
        }

        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? strCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string strCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("OutUnit", "nvarchar")]
        public string strOutUnit
        {
            get { return OutUnit; }
            set { OutUnit = value; }
        }

        [DataFieldAttribute("SubUnit", "nvarchar")]
        public string strSubUnit
        {
            get { return SubUnit; }
            set { SubUnit = value; }
        }

        [DataFieldAttribute("ModelProperty", "nvarchar")]
        public string strModelProperty
        {
            get { return ModelProperty; }
            set { ModelProperty = value; }
        }

    }
}
