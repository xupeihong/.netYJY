using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class RepairCardUT
    { 
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

        public RepairCardUT()
        {

            //TODO: 在此处添加构造函数逻辑

        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RIDUT", "varchar")]
        public string strRIDUT
        {
            get { return RIDUT; }
            set { RIDUT = value; }
        }

        [Required(ErrorMessage = "维修编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RepairIDUT", "varchar")]
        public string strRepairIDUT
        {
            get { return RepairIDUT; }
            set { RepairIDUT = value; }
        }

        [Required(ErrorMessage = "客户名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerNameUT", "varchar")]
        public string strCustomerNameUT
        {
            get { return CustomerNameUT; }
            set { CustomerNameUT = value; }
        }

        [Required(ErrorMessage = "客户地址不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CustomerAddrUT", "nvarchar")]
        public string strCustomerAddrUT
        {
            get { return CustomerAddrUT; }
            set { CustomerAddrUT = value; }
        }

        [Required(ErrorMessage = "送表人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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

        [Required(ErrorMessage = "送表日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("S_DateUT", "nvarchar")]
        public string strS_DateUT
        {
            get { return S_DateUT; }
            set { S_DateUT = value; }
        }

        [Required(ErrorMessage = "仪表编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MeterIDUT", "nvarchar")]
        public string strMeterIDUT
        {
            get { return MeterIDUT; }
            set { MeterIDUT = value; }
        }

        [Required(ErrorMessage = "证书编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CertifIDUT", "nvarchar")]
        public string strCertifIDUT
        {
            get { return CertifIDUT; }
            set { CertifIDUT = value; }
        }

        [Required(ErrorMessage = "仪表名称不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
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

        [Required(ErrorMessage = "仪表型号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ModelUT", "nvarchar")]
        public string strModelUT
        {
            get { return ModelUT; }
            set { ModelUT = value; }
        }

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


    }
}
