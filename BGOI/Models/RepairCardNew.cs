using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class RepairCardNew
    {
        public RepairCardNew()
        {

            //TODO: 在此处添加构造函数逻辑

        }

        #region // ----[维修登记卡]

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
        private string FactoryDate;
        private decimal? RecordNum;
        private string FlowRange;
        private string Pressure;
        private string Caliber;
        private string PreUnit;
        private string NewUnit;
        private string ModelType;

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

        //[Required(ErrorMessage = "联系电话不能为空")]
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

        [Required(ErrorMessage = "精度等级不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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

        [Required(ErrorMessage = "仪表类型不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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

        #endregion 

        #region // ----[超声波维修登记卡]

        private string RIDUT;
        private string RepairIDUT;
        private string CustomerNameUT;
        private string CustomerAddrUT;
        private string S_NameUT;
        private string S_TelUT;
        private string S_DateUT;

        private string MeterIDUT;
        private string CertifIDUT;
        private string MeterNameUT;
        private string ManufacturerUT;
        private string ModelUT;
        private string ModelTypeUT;
        private string CirNumUT;
        private string CirVersionUT;
        private string FactoryDateUT;
        private string FlowRangeUT;
        private string PressureUT;
        private string CaliberUT;

        private string TrackA1UT;
        private string TrackA2UT;
        private string TrackA3UT;
        private string TrackA4UT;
        private string TrackA5UT;
        private string TrackA6UT;
        private string TrackB1UT;
        private string TrackB2UT;
        private string TrackB3UT;
        private string TrackB4UT;
        private string TrackB5UT;
        private string TrackB6UT;
        private string Comments1UT;

        private string FaceOtherUT;
        private string RepairContentUT;
        private string CheckUserUT;
        private string FirstCheckUT;
        private string FirstDateUT;
        private string SecondCheckUT;
        private string SecondDateUT;
        private string ThirdCheckUT;
        private string ThirdDateUT;

        private string TextUT;
        private string GetTypeModelUT;
        private string G_NameUT;
        private string G_TelUT;
        private string G_DateUT;
        private int StateUT;
        private string FinishDateUT;
        private string IsOutUT;
        private string TakeIDUT;
        private string DeliverIDUT;
        private string RecieveIDUT;
        private string ReceiveUserUT;
        private string LogisticUT;

        private DateTime? CreateTimeUT;
        private string CreateUserUT;
        private string validateUT;
        private string OutUnitUT;
        private string SubUnitUT;
        private string ModelPropertyUT;


        //[Required(ErrorMessage = "内部编号不能为空")]
        //[StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RIDUT", "varchar")]
        public string strRIDUT
        {
            get { return RIDUT; }
            set { RIDUT = value; }
        }

        //[Required(ErrorMessage = "维修编号不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairIDUT", "varchar")]
        public string strRepairIDUT
        {
            get { return RepairIDUT; }
            set { RepairIDUT = value; }
        }

        //[Required(ErrorMessage = "客户名称不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerNameUT", "varchar")]
        public string strCustomerNameUT
        {
            get { return CustomerNameUT; }
            set { CustomerNameUT = value; }
        }

        //[Required(ErrorMessage = "客户地址不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerAddrUT", "nvarchar")]
        public string strCustomerAddrUT
        {
            get { return CustomerAddrUT; }
            set { CustomerAddrUT = value; }
        }

        //[Required(ErrorMessage = "送表人不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("S_NameUT", "nvarchar")]
        public string strS_NameUT
        {
            get { return S_NameUT; }
            set { S_NameUT = value; }
        }

        [DataFieldAttribute("S_TelUT", "nvarchar")]
        public string strS_TelUT
        {
            get { return S_TelUT; }
            set { S_TelUT = value; }
        }

        //[Required(ErrorMessage = "送表日期不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("S_DateUT", "nvarchar")]
        public string strS_DateUT
        {
            get { return S_DateUT; }
            set { S_DateUT = value; }
        }

        //[Required(ErrorMessage = "仪表编号不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MeterIDUT", "nvarchar")]
        public string strMeterIDUT
        {
            get { return MeterIDUT; }
            set { MeterIDUT = value; }
        }

        //[Required(ErrorMessage = "证书编号不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CertifIDUT", "nvarchar")]
        public string strCertifIDUT
        {
            get { return CertifIDUT; }
            set { CertifIDUT = value; }
        }

        //[Required(ErrorMessage = "仪表名称不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MeterNameUT", "nvarchar")]
        public string strMeterNameUT
        {
            get { return MeterNameUT; }
            set { MeterNameUT = value; }
        }

        [DataFieldAttribute("ManufacturerUT", "nvarchar")]
        public string strManufacturerUT
        {
            get { return ManufacturerUT; }
            set { ManufacturerUT = value; }
        }
        
        //[Required(ErrorMessage = "仪表型号不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ModelUT", "nvarchar")]
        public string strModelUT
        {
            get { return ModelUT; }
            set { ModelUT = value; }
        }

        //[Required(ErrorMessage = "仪表类型不能为空")]
        //[ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ModelTypeUT", "nvarchar")]
        public string strModelTypeUT
        {
            get { return ModelTypeUT; }
            set { ModelTypeUT = value; }
        }

        [DataFieldAttribute("CirNumUT", "nvarchar")]
        public string strCirNumUT
        {
            get { return CirNumUT; }
            set { CirNumUT = value; }
        }

        [DataFieldAttribute("CirVersionUT", "nvarchar")]
        public string strCirVersionUT
        {
            get { return CirVersionUT; }
            set { CirVersionUT = value; }
        }

        [DataFieldAttribute("FactoryDateUT", "nvarchar")]
        public string strFactoryDateUT
        {
            get { return FactoryDateUT; }
            set { FactoryDateUT = value; }
        }

        [DataFieldAttribute("FlowRangeUT", "nvarchar")]
        public string strFlowRangeUT
        {
            get { return FlowRangeUT; }
            set { FlowRangeUT = value; }
        }

        [DataFieldAttribute("PressureUT", "nvarchar")]
        public string strPressureUT
        {
            get { return PressureUT; }
            set { PressureUT = value; }
        }

        [DataFieldAttribute("CaliberUT", "nvarchar")]
        public string strCaliberUT
        {
            get { return CaliberUT; }
            set { CaliberUT = value; }
        }

        [DataFieldAttribute("TrackA1UT", "nvarchar")]
        public string strTrackA1UT
        {
            get { return TrackA1UT; }
            set { TrackA1UT = value; }
        }

        [DataFieldAttribute("TrackA2UT", "nvarchar")]
        public string strTrackA2UT
        {
            get { return TrackA2UT; }
            set { TrackA2UT = value; }
        }

        [DataFieldAttribute("TrackA3UT", "nvarchar")]
        public string strTrackA3UT
        {
            get { return TrackA3UT; }
            set { TrackA3UT = value; }
        }

        [DataFieldAttribute("TrackA4UT", "nvarchar")]
        public string strTrackA4UT
        {
            get { return TrackA4UT; }
            set { TrackA4UT = value; }
        }

        [DataFieldAttribute("TrackA5UT", "nvarchar")]
        public string strTrackA5UT
        {
            get { return TrackA5UT; }
            set { TrackA5UT = value; }
        }

        [DataFieldAttribute("TrackA6UT", "nvarchar")]
        public string strTrackA6UT
        {
            get { return TrackA6UT; }
            set { TrackA6UT = value; }
        }

        [DataFieldAttribute("TrackB1UT", "nvarchar")]
        public string strTrackB1UT
        {
            get { return TrackB1UT; }
            set { TrackB1UT = value; }
        }

        [DataFieldAttribute("TrackB2UT", "nvarchar")]
        public string strTrackB2UT
        {
            get { return TrackB2UT; }
            set { TrackB2UT = value; }
        }

        [DataFieldAttribute("TrackB3UT", "nvarchar")]
        public string strTrackB3UT
        {
            get { return TrackB3UT; }
            set { TrackB3UT = value; }
        }

        [DataFieldAttribute("TrackB4UT", "nvarchar")]
        public string strTrackB4UT
        {
            get { return TrackB4UT; }
            set { TrackB4UT = value; }
        }

        [DataFieldAttribute("TrackB5UT", "nvarchar")]
        public string strTrackB5UT
        {
            get { return TrackB5UT; }
            set { TrackB5UT = value; }
        }

        [DataFieldAttribute("TrackB6UT", "nvarchar")]
        public string strTrackB6UT
        {
            get { return TrackB6UT; }
            set { TrackB6UT = value; }
        }

        [DataFieldAttribute("Comments1UT", "text")]
        public string strComments1UT
        {
            get { return Comments1UT; }
            set { Comments1UT = value; }
        }

        [DataFieldAttribute("FaceOtherUT", "nvarchar")]
        public string strFaceOtherUT
        {
            get { return FaceOtherUT; }
            set { FaceOtherUT = value; }
        }

        [DataFieldAttribute("RepairContentUT", "nvarchar")]
        public string strRepairContentUT
        {
            get { return RepairContentUT; }
            set { RepairContentUT = value; }
        }

        [DataFieldAttribute("CheckUserUT", "nvarchar")]
        public string strCheckUserUT
        {
            get { return CheckUserUT; }
            set { CheckUserUT = value; }
        }

        [DataFieldAttribute("FirstCheckUT", "nvarchar")]
        public string strFirstCheckUT
        {
            get { return FirstCheckUT; }
            set { FirstCheckUT = value; }
        }

        [DataFieldAttribute("FirstDateUT", "nvarchar")]
        public string strFirstDateUT
        {
            get { return FirstDateUT; }
            set { FirstDateUT = value; }
        }

        [DataFieldAttribute("SecondCheckUT", "nvarchar")]
        public string strSecondCheckUT
        {
            get { return SecondCheckUT; }
            set { SecondCheckUT = value; }
        }

        [DataFieldAttribute("SecondDateUT", "nvarchar")]
        public string strSecondDateUT
        {
            get { return SecondDateUT; }
            set { SecondDateUT = value; }
        }

        [DataFieldAttribute("ThirdCheckUT", "nvarchar")]
        public string strThirdCheckUT
        {
            get { return ThirdCheckUT; }
            set { ThirdCheckUT = value; }
        }

        [DataFieldAttribute("ThirdDateUT", "nvarchar")]
        public string strThirdDateUT
        {
            get { return ThirdDateUT; }
            set { ThirdDateUT = value; }
        }

        [DataFieldAttribute("TextUT", "text")]
        public string strTextUT
        {
            get { return TextUT; }
            set { TextUT = value; }
        }

        [DataFieldAttribute("GetTypeModelUT", "nvarchar")]
        public string strGetTypeModelUT
        {
            get { return GetTypeModelUT; }
            set { GetTypeModelUT = value; }
        }

        [DataFieldAttribute("G_NameUT", "nvarchar")]
        public string strG_NameUT
        {
            get { return G_NameUT; }
            set { G_NameUT = value; }
        }

        [DataFieldAttribute("G_TelUT", "nvarchar")]
        public string strG_TelUT
        {
            get { return G_TelUT; }
            set { G_TelUT = value; }
        }

        [DataFieldAttribute("G_DateUT", "nvarchar")]
        public string strG_DateUT
        {
            get { return G_DateUT; }
            set { G_DateUT = value; }
        }

        [DataFieldAttribute("StateUT", "int")]
        public int strStateUT
        {
            get { return StateUT; }
            set { StateUT = value; }
        }

        [DataFieldAttribute("FinishDateUT", "nvarchar")]
        public string strFinishDateUT
        {
            get { return FinishDateUT; }
            set { FinishDateUT = value; }
        }

        [DataFieldAttribute("IsOutUT", "nvarchar")]
        public string strIsOutUT
        {
            get { return IsOutUT; }
            set { IsOutUT = value; }
        }

        [DataFieldAttribute("TakeIDUT", "nvarchar")]
        public string strTakeIDUT
        {
            get { return TakeIDUT; }
            set { TakeIDUT = value; }
        }

        [DataFieldAttribute("DeliverIDUT", "nvarchar")]
        public string strDeliverIDUT
        {
            get { return DeliverIDUT; }
            set { DeliverIDUT = value; }
        }

        [DataFieldAttribute("RecieveIDUT", "nvarchar")]
        public string strRecieveIDUT
        {
            get { return RecieveIDUT; }
            set { RecieveIDUT = value; }
        }

        [DataFieldAttribute("ReceiveUserUT", "nvarchar")]
        public string strReceiveUserUT
        {
            get { return ReceiveUserUT; }
            set { ReceiveUserUT = value; }
        }

        [DataFieldAttribute("LogisticUT", "text")]
        public string strLogisticUT
        {
            get { return LogisticUT; }
            set { LogisticUT = value; }
        }

        [DataFieldAttribute("validateUT", "nvarchar")]
        public string strvalidateUT
        {
            get { return validateUT; }
            set { validateUT = value; }
        }

        [DataFieldAttribute("CreateTimeUT", "datetime")]
        public DateTime? strCreateTimeUT
        {
            get { return CreateTimeUT; }
            set { CreateTimeUT = value; }
        }

        [DataFieldAttribute("CreateUserUT", "nvarchar")]
        public string strCreateUserUT
        {
            get { return CreateUserUT; }
            set { CreateUserUT = value; }
        }

        [DataFieldAttribute("OutUnitUT", "nvarchar")]
        public string strOutUnitUT
        {
            get { return OutUnitUT; }
            set { OutUnitUT = value; }
        }

        [DataFieldAttribute("SubUnitUT", "nvarchar")]
        public string strSubUnitUT
        {
            get { return SubUnitUT; }
            set { SubUnitUT = value; }
        }

        [DataFieldAttribute("ModelPropertyUT", "nvarchar")]
        public string strModelPropertyUT
        {
            get { return ModelPropertyUT; }
            set { ModelPropertyUT = value; }
        }


        
        #endregion


    }
}