using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class Tk_SupplierBas //:IValidatableObject
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private int _Num;
        [DataFieldAttribute("Num", "varchar")]
        public int Num
        {
            get { return _Num; }
            set { _Num = value; }
        }
        private string _DeclareUnitID;
        [DataFieldAttribute("DeclareUnitID", "varchar")]
        public string DeclareUnitID
        {
            get { return _DeclareUnitID; }
            set { _DeclareUnitID = value; }
        }
        private string _DeclareDate;
        [DataFieldAttribute("DeclareDate", "varchar")]
        public string DeclareDate
        {
            get { return _DeclareDate; }
            set { _DeclareDate = value; }
        }

        private string _SupplierCode;

        [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")]
        [StringLength(20, ErrorMessage = "供应商编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SupplierCode", "varchar")]
        public string SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        private string _SupplierType;
        [DataFieldAttribute("SupplierType", "varchar")]
        public string SupplierType
        {
            get { return _SupplierType; }
            set { _SupplierType = value; }
        }
        private string _SupplierClass;
        [DataFieldAttribute("SupplierClass", "varchar")]
        public string SupplierClass
        {
            get { return _SupplierClass; }
            set { _SupplierClass = value; }
        }
        private string _COMNameC;
        [Remote("CheckSupplyName", "SuppliesManage", ErrorMessage = "该供应商名称已存在")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMNameC", "nvarchar")]
        public string COMNameC
        {
            get { return _COMNameC; }
            set { _COMNameC = value; }
        }
        private string _COMShortName;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMShortName", "nvarchar")]
        public string COMShortName
        {
            get { return _COMShortName; }
            set { _COMShortName = value; }
        }
        private string _COMNameE;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMNameE", "varchar")]
        public string COMNameE
        {
            get { return _COMNameE; }
            set { _COMNameE = value; }
        }
        private string _COMWebsite;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMWebsite", "varchar")]
        public string COMWebsite
        {
            get { return _COMWebsite; }
            set { _COMWebsite = value; }
        }
        private string _COMRAddress;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("COMRAddress", "nvarchar")]
        public string COMRAddress
        {
            get { return _COMRAddress; }
            set { _COMRAddress = value; }
        }
        private string _COMCountry;
        [DataFieldAttribute("COMCountry", "nvarchar")]
        public string COMCountry
        {
            get { return _COMCountry; }
            set { _COMCountry = value; }
        }
        private string _ComAddress;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ComAddress", "nvarchar")]
        public string ComAddress
        {
            get { return _ComAddress; }
            set { _ComAddress = value; }
        }
        private string _COMArea;
        [DataFieldAttribute("COMArea", "nvarchar")]
        public string COMArea
        {
            get { return _COMArea; }
            set { _COMArea = value; }
        }
        private string _COMFactoryAddress;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(50, ErrorMessage = "公司出厂/出货地址不能超过50个字符")]
        [DataFieldAttribute("COMFactoryAddress", "nvarchar")]
        public string COMFactoryAddress
        {
            get { return _COMFactoryAddress; }
            set { _COMFactoryAddress = value; }
        }
        private string _COMFactoryArea;
        [RegularExpression(@"^[0-9]+(.[0-9]{1,2})?$", ErrorMessage = "请输入数字")]
        [DataFieldAttribute("COMFactoryArea", "decimal")]
        public string COMFactoryArea
        {
            get { return _COMFactoryArea; }
            set { _COMFactoryArea = value; }
        }
        private string _COMCreateDate;
        [DataFieldAttribute("COMCreateDate", "DatTime")]
        public string COMCreateDate
        {
            get { return _COMCreateDate; }
            set { _COMCreateDate = value; }
        }
        private string _COMLegalPerson;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(10, ErrorMessage = "法人代表不能超过10个字符")]
        [DataFieldAttribute("COMLegalPerson", "nvarchar")]
        public string COMLegalPerson
        {
            get { return _COMLegalPerson; }
            set { _COMLegalPerson = value; }
        }
        private string _TaxRegistrationNo;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "税务登记号不能超过20个字符")]
        [DataFieldAttribute("TaxRegistrationNo", "varchar")]
        public string TaxRegistrationNo
        {
            get { return _TaxRegistrationNo; }
            set { _TaxRegistrationNo = value; }
        }
        private string _IsCooperate;
        [DataFieldAttribute("IsCooperate", "char")]
        public string IsCooperate
        {
            get { return _IsCooperate; }
            set { _IsCooperate = value; }
        }
        private string _COMGroup;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "供应商集团名称不能超过20个字符")]
        [DataFieldAttribute("COMGroup", "nvarchar")]
        public string COMGroup
        {
            get { return _COMGroup; }
            set { _COMGroup = value; }
        }
        private string _BusinessLicenseNo;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "营业执照号码不能超过20个字符")]
        [DataFieldAttribute("BusinessLicenseNo", "varchar")]
        public string BusinessLicenseNo
        {
            get { return _BusinessLicenseNo; }
            set { _BusinessLicenseNo = value; }
        }
        private string _StaffNum;
        [DataFieldAttribute("StaffNum", "string")]
        public string StaffNum
        {
            get { return _StaffNum; }
            set { _StaffNum = value; }
        }
        private string _RegisteredCapital;
        [RegularExpression(@"^[0-9]+(.[0-9]{1,2})?$", ErrorMessage = "请输入数字")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        //[StringLength(20, ErrorMessage = "法人代表不能超过20个字符")]
        [DataFieldAttribute("RegisteredCapital", "decimal")]
        public string RegisteredCapital
        {
            get { return _RegisteredCapital; }
            set { _RegisteredCapital = value; }
        }
        private string _CapitalUnit;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "注册单位不能超过20个字符")]
        [DataFieldAttribute("CapitalUnit", "nvarchar")]
        public string CapitalUnit
        {
            get { return _CapitalUnit; }
            set { _CapitalUnit = value; }
        }
        private string _OrganizationCode;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "组织机构与代码不能超过20个字符")]
        [DataFieldAttribute("OrganizationCode", "nvarchar")]
        public string OrganizationCode
        {
            get { return _OrganizationCode; }
            set { _OrganizationCode = value; }
        }
        private string _ThreeCertity;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ThreeCertity", "nvarchar")]
        public string ThreeCertity
        {
            get { return _ThreeCertity; }
            set { _ThreeCertity = value; }
        }
        private string _BankName;
        //[RegularExpression(@"/^[\u4e00-\u9fa5]*$/", ErrorMessage = "请输入正确的开户行名称")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "开户行名称不能超过20个字符")]
        [DataFieldAttribute("BankName", "nvarchar")]
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        private string _BankAccount;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "银行基本账号不能超过20个字符")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "请输入正确的银行账号")]
        [DataFieldAttribute("BankAccount", "string")]
        public string BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }
        private string _EnterpriseType;
        [DataFieldAttribute("EnterpriseType", "nvarchar")]
        public string EnterpriseType
        {
            get { return _EnterpriseType; }
            set { _EnterpriseType = value; }
        }
        private string _BusinessDistribute;
        [DataFieldAttribute("BusinessDistribute", "nvarchar")]
        public string BusinessDistribute
        {
            get { return _BusinessDistribute; }
            set { _BusinessDistribute = value; }
        }
        private string _BillingWay;
        [DataFieldAttribute("BillingWay", "nvarchar")]
        public string BillingWay
        {
            get { return _BillingWay; }
            set { _BillingWay = value; }
        }
        private string _DevelopStaffs;
        [DataFieldAttribute("DevelopStaffs", "string")]
        public string DevelopStaffs
        {
            get { return _DevelopStaffs; }
            set { _DevelopStaffs = value; }
        }
        private string _QAStaffs;
        [DataFieldAttribute("QAStaffs", "string")]
        public string QAStaffs
        {
            get { return _QAStaffs; }
            set { _QAStaffs = value; }
        }
        private string _ProduceStaffs;
        [DataFieldAttribute("ProduceStaffs", "string")]
        public string ProduceStaffs
        {
            get { return _ProduceStaffs; }
            set { _ProduceStaffs = value; }
        }
        private string _HasRegulation;
        [DataFieldAttribute("HasRegulation", "char")]
        public string HasRegulation
        {
            get { return _HasRegulation; }
            set { _HasRegulation = value; }
        }
        private string _ProductLineNum;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "拟购生产线数量不能超过20个字符")]
        [DataFieldAttribute("ProductLineNum", "string")]
        public string ProductLineNum
        {
            get { return _ProductLineNum; }
            set { _ProductLineNum = value; }
        }
        private string _WorkTime_Start;
        [DataFieldAttribute("WorkTime_Start", "varchar")]
        public string WorkTime_Start
        {
            get { return _WorkTime_Start; }
            set { _WorkTime_Start = value; }
        }
        private string _WorkTime_End;
        [DataFieldAttribute("WorkTime_End", "varchar")]
        public string WorkTime_End
        {
            get { return _WorkTime_End; }
            set { _WorkTime_End = value; }
        }
        private string _WorkDay_Start;
        [DataFieldAttribute("WorkDay_Start", "nvarchar")]
        public string WorkDay_Start
        {
            get { return _WorkDay_Start; }
            set { _WorkDay_Start = value; }
        }
        private string _WorkDay_End;
        [DataFieldAttribute("WorkDay_End", "nvarchar")]
        public string WorkDay_End
        {
            get { return _WorkDay_End; }
            set { _WorkDay_End = value; }
        }
        private string _BusinessScope;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(100, ErrorMessage = "经营范围不能超过100个字符")]
        [DataFieldAttribute("BusinessScope", "nvarchar")]
        public string BusinessScope
        {
            get { return _BusinessScope; }
            set { _BusinessScope = value; }
        }
        private string _IsrankingIn5;
        [DataFieldAttribute("IsrankingIn5", "char")]
        public string IsrankingIn5
        {
            get { return _IsrankingIn5; }
            set { _IsrankingIn5 = value; }
        }
        private string _RankingType;
        [DataFieldAttribute("RankingType", "string")]
        public string RankingType
        {
            get { return _RankingType; }
            set { _RankingType = value; }
        }
        private string _Ranking;
        [DataFieldAttribute("Ranking", "string")]
        public string Ranking
        {
            get { return _Ranking; }
            set { _Ranking = value; }
        }
        private string _ScaleType;
        [DataFieldAttribute("ScaleType", "nvarchar")]
        public string ScaleType
        {
            get { return _ScaleType; }
            set { _ScaleType = value; }
        }
        private string _QualityStandard;
        [DataFieldAttribute("QualityStandard", "nvarchar")]
        public string QualityStandard
        {
            get { return _QualityStandard; }
            set { _QualityStandard = value; }
        }
        private string _AnnualOutput;
        [DataFieldAttribute("AnnualOutput", "nvarchar")]
        public string AnnualOutput
        {
            get { return _AnnualOutput; }
            set { _AnnualOutput = value; }
        }
        private string _AnnualOutputValue;
        [DataFieldAttribute("AnnualOutputValue", "nvarchar")]
        public string AnnualOutputValue
        {
            get { return _AnnualOutputValue; }
            set { _AnnualOutputValue = value; }
        }
        private string _MainClient;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(100, ErrorMessage = "近三年客户名称不能超过100个字符")]
        [DataFieldAttribute("MainClient", "nvarchar")]
        public string MainClient
        {
            get { return _MainClient; }
            set { _MainClient = value; }
        }
        private string _Achievement;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(120, ErrorMessage = "近三年业绩不能超过120个字符")]
        [DataFieldAttribute("Achievement", "nvarchar")]
        public string Achievement
        {
            get { return _Achievement; }
            set { _Achievement = value; }
        }
        private string _HasAgent;
        [DataFieldAttribute("HasAgent", "char")]
        public string HasAgent
        {
            get { return _HasAgent; }
            set { _HasAgent = value; }
        }
        private string _HasAuthorization;
        [DataFieldAttribute("HasAuthorization", "char")]
        public string HasAuthorization
        {
            get { return _HasAuthorization; }
            set { _HasAuthorization = value; }
        }
        private string _HasDrawing;
        [DataFieldAttribute("HasDrawing", "char")]
        public string HasDrawing
        {
            get { return _HasDrawing; }
            set { _HasDrawing = value; }
        }
        private string _AgentClass;
        [DataFieldAttribute("AgentClass", "nvarchar")]
        public string AgentClass
        {
            get { return _AgentClass; }
            set { _AgentClass = value; }
        }
        private string _HasImportMaterial;
        [DataFieldAttribute("HasImportMaterial", "char")]
        public string HasImportMaterial
        {
            get { return _HasImportMaterial; }
            set { _HasImportMaterial = value; }
        }
        private string _HasCertificate;
        [DataFieldAttribute("HasCertificate", "char")]
        public string HasCertificate
        {
            get { return _HasCertificate; }
            set { _HasCertificate = value; }
        }
        private string _Award;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(120, ErrorMessage = "曾获奖项不能超过120个字符")]
        [DataFieldAttribute("Award", "nvarchar")]
        public string Award
        {
            get { return _Award; }
            set { _Award = value; }
        }
        private string _FAX;
        [DataFieldAttribute("FAX", "nvarchar")]
        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        private string _Relation;
        [DataFieldAttribute("Relation", "varchar")]
        public string Relation
        {
            get { return _Relation; }
            set { _Relation = value; }
        }
        private string _IsUnReview;
        [DataFieldAttribute("IsUnReview", "char")]
        public string IsUnReview
        {
            get { return _IsUnReview; }
            set { _IsUnReview = value; }
        }
        private string _IsURInnerUnit;
        [DataFieldAttribute("IsURInnerUnit", "char")]
        public string IsURInnerUnit
        {
            get { return _IsURInnerUnit; }
            set { _IsURInnerUnit = value; }
        }
        private string _UnReviewUnit;
        [DataFieldAttribute("UnReviewUnit", "nvarchar")]
        public string UnReviewUnit
        {
            get { return _UnReviewUnit; }
            set { _UnReviewUnit = value; }
        }
        private string _UnReviewDesc;
        [DataFieldAttribute("UnReviewDesc", "nvarchar")]
        public string UnReviewDesc
        {
            get { return _UnReviewDesc; }
            set { _UnReviewDesc = value; }
        }
        private string _URConfirmUser;
        [DataFieldAttribute("URConfirmUser", "nvarchar")]
        public string URConfirmUser
        {
            get { return _URConfirmUser; }
            set { _URConfirmUser = value; }
        }
        private string _Evaluation1;
        [DataFieldAttribute("Evaluation1", "nvarchar")]
        public string Evaluation1
        {
            get { return _Evaluation1; }
            set { _Evaluation1 = value; }
        }
        private string _Evaluation2;
        [DataFieldAttribute("Evaluation2", "nvarchar")]
        public string Evaluation2
        {
            get { return _Evaluation2; }
            set { _Evaluation2 = value; }
        }
        private string _Evaluation3;
        [DataFieldAttribute("Evaluation3", "nvarchar")]
        public string Evaluation3
        {
            get { return _Evaluation3; }
            set { _Evaluation3 = value; }
        }
        private string _Evaluation4;
        [DataFieldAttribute("Evaluation4", "nvarchar")]
        public string Evaluation4
        {
            get { return _Evaluation4; }
            set { _Evaluation4 = value; }
        }
        private string _Evaluation5;
        [DataFieldAttribute("Evaluation5", "char")]
        public string Evaluation5
        {
            get { return _Evaluation5; }
            set { _Evaluation5 = value; }
        }
        private string _Evaluation6;
        [DataFieldAttribute("Evaluation6", "nvarchar")]
        public string Evaluation6
        {
            get { return _Evaluation6; }
            set { _Evaluation6 = value; }
        }
        private string _Evaluation7;
        [DataFieldAttribute("Evaluation7", "nvarchar")]
        public string Evaluation7
        {
            get { return _Evaluation7; }
            set { _Evaluation7 = value; }
        }
        private string _DeclareUser;
        [DataFieldAttribute("DeclareUser", "nvarchar")]
        public string DeclareUser
        {
            get { return _DeclareUser; }
            set { _DeclareUser = value; }
        }
        private string _BussinessUser;
        [DataFieldAttribute("BussinessUser", "nvarchar")]
        public string BussinessUser
        {
            get { return _BussinessUser; }
            set { _BussinessUser = value; }
        }
        private string _TecolUser;
        [DataFieldAttribute("TecolUser", "nvarchar")]
        public string TecolUser
        {
            get { return _TecolUser; }
            set { _TecolUser = value; }
        }
        private string _BuyUser;
        [DataFieldAttribute("BuyUser", "nvarchar")]
        public string BuyUser
        {
            get { return _BuyUser; }
            set { _BuyUser = value; }
        }
        private string _SaleUser;
        [DataFieldAttribute("SaleUser", "nvarchar")]
        public string SaleUser
        {
            get { return _SaleUser; }
            set { _SaleUser = value; }
        }
        private string _ChargeUser;
        [DataFieldAttribute("ChargeUser", "nvarchar")]
        public string ChargeUser
        {
            get { return _ChargeUser; }
            set { _ChargeUser = value; }
        }
        private string _Approval1;
        [DataFieldAttribute("Approval1", "char")]
        public string Approval1
        {
            get { return _Approval1; }
            set { _Approval1 = value; }
        }
        private string _Approval1User;
        [DataFieldAttribute("Approval1User", "nvarchar")]
        public string Approval1User
        {
            get { return _Approval1User; }
            set { _Approval1User = value; }
        }
        private string _Approval2;
        [DataFieldAttribute("Approval2", "char")]
        public string Approval2
        {
            get { return _Approval2; }
            set { _Approval2 = value; }
        }
        private string _Approval2User;
        [DataFieldAttribute("Approval2User", "nvarchar")]
        public string Approval2User
        {
            get { return _Approval2User; }
            set { _Approval2User = value; }
        }
        private string _Approval3;
        [DataFieldAttribute("Approval3", "char")]
        public string Approval3
        {
            get { return _Approval3; }
            set { _Approval3 = value; }
        }
        private string _Approval3User;
        [DataFieldAttribute("Approval3User", "nvarchar")]
        public string Approval3User
        {
            get { return _Approval3User; }
            set { _Approval3User = value; }
        }
        private string _Approval4;
        [DataFieldAttribute("Approval4", "char")]
        public string Approval4
        {
            get { return _Approval4; }
            set { _Approval4 = value; }
        }
        private string _Approval4User;
        [DataFieldAttribute("Approval4User", "nvarchar")]
        public string Approval4User
        {
            get { return _Approval4User; }
            set { _Approval4User = value; }
        }
        private int _State;
        [DataFieldAttribute("State", "Int")]
        public int State
        {
            get { return _State; }
            set { _State = value; }
        }
        private int _WState;
        [DataFieldAttribute("WState", "Int")]
        public int WState
        {
            get { return _WState; }
            set { _WState = value; }
        }
        private int _NState;
        [DataFieldAttribute("NState", "Int")]
        public int NState
        {
            get { return _NState; }
            set { _NState = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _CreateTime;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _Validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }
        public string _Turnover;
        [DataFieldAttribute("Turnover", "varchar")]
        public string Turnover
        {
            get { return _Turnover; }
            set { _Turnover = value; }
        }
        private string _OtherType;
        [DataFieldAttribute("OtherType", "varchar")]
        public string OtherType
        {
            get { return _OtherType; }
            set { _OtherType = value; }
        }
        private string _IsUnreviewUser;
        [DataFieldAttribute("IsUnreviewUser", "varchar")]
        public string IsUnreviewUser
        {
            get { return _IsUnreviewUser; }
            set { _IsUnreviewUser = value; }
        }
        private string _ApprovalAdvice1;
        [DataFieldAttribute("ApprovalAdvice1", "varchar")]
        public string ApprovalAdvice1
        {
            get { return _ApprovalAdvice1; }
            set { _ApprovalAdvice1 = value; }
        }
        private string _ApprovalAdvice2;
        [DataFieldAttribute("ApprovalAdvice2", "varchar")]
        public string ApprovalAdvice2
        {
            get { return _ApprovalAdvice2; }
            set { _ApprovalAdvice2 = value; }
        }
        private string _ApprovalAdvice3;
        [DataFieldAttribute("ApprovalAdvice3", "varchar")]
        public string ApprovalAdvice3
        {
            get { return _ApprovalAdvice3; }
            set { _ApprovalAdvice3 = value; }
        }
        private string _ApprovalAdvice4;
        [DataFieldAttribute("ApprovalAdvice4", "varchar")]
        public string ApprovalAdvice4
        {
            get { return _ApprovalAdvice4; }
            set { _ApprovalAdvice4 = value; }
        }
        private string _ApprovalAdvice5;
        [DataFieldAttribute("ApprovalAdvice5", "varchar")]
        public string ApprovalAdvice5
        {
            get { return _ApprovalAdvice5; }
            set { _ApprovalAdvice5 = value; }
        }
        private string _ApprovalRes;
        [DataFieldAttribute("ApprovalRes", "varchar")]
        public string ApprovalRes
        {
            get { return _ApprovalRes; }
            set { _ApprovalRes = value; }
        }
        private string _ApprovalState;
        [DataFieldAttribute("ApprovalState", "varchar")]
        public string ApprovalState
        {
            get { return _ApprovalState; }
            set { _ApprovalState = value; }
        }
        private DateTime? _AppTime;
        [DataFieldAttribute("AppTime", "DateTime")]
        public DateTime? AppTime
        {
            get { return _AppTime; }
            set { _AppTime = value; }
        }
        private int _ResState;
        [DataFieldAttribute("ResState", "Int")]
        public int ResState
        {
            get { return _ResState; }
            set { _ResState = value; }
        }
      
       
    }
}
