using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_KClientBas
    {
        private string _KID;
        [DataFieldAttribute("KID", "nvarchar")]
        public string KID
        {
            get { return _KID; }
            set { _KID = value; }
        }
        private string _Kcode;
        [DataFieldAttribute("Kcode", "nvarchar")]
        public string Kcode
        {
            get { return _Kcode; }
            set { _Kcode = value; }
        }
        private string _GainDate;
        [DataFieldAttribute("GainDate", "DateTime")]
        public string GainDate
        {
            get { return _GainDate; }
            set { _GainDate = value; }
        }
        private string _DeclareUnit;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeclareUnit", "nvarchar")]
        public string DeclareUnit
        {
            get { return _DeclareUnit; }
            set { _DeclareUnit = value; }
        }
        private string _DeclareUser;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeclareUser", "nvarchar")]
        public string DeclareUser
        {
            get { return _DeclareUser; }
            set { _DeclareUser = value; }
        }
        private string _ChargeUser;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ChargeUser", "nvarchar")]
        public string ChargeUser
        {
            get { return _ChargeUser; }
            set { _ChargeUser = value; }
        }
        private string _IsShare;
        [DataFieldAttribute("IsShare", "nvarchar")]
        public string IsShare
        {
            get { return _IsShare; }
            set { _IsShare = value; }
        }
        private string _ShareUnits;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ShareUnits", "nvarchar")]
        public string ShareUnits
        {
            get { return _ShareUnits; }
            set { _ShareUnits = value; }
        }
        private string _CName;
      //  [Remote("CheckCNameExists", "SuppliesManage", ErrorMessage = "该客户名称已存在")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CName", "nvarchar")]
        public string CName
        {
            get { return _CName; }
            set { _CName = value; }
        }
        private string _CShortName;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CShortName", "nvarchar")]
        public string CShortName
        {
            get { return _CShortName; }
            set { _CShortName = value; }
        }
        private string _Industry;
        [DataFieldAttribute("Industry", "nvarchar")]
        public string Industry
        {
            get { return _Industry; }
            set { _Industry = value; }
        }
        private string _StaffSize;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StaffSize", "nvarchar")]
        public string StaffSize
        {
            get { return _StaffSize; }
            set { _StaffSize = value; }
        }
        private string _Products;
        [DataFieldAttribute("Products", "nvarchar")]
        public string Products
        {
            get { return _Products; }
            set { _Products = value; }
        }
        private string _Phone;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [RegularExpression(@"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$", ErrorMessage = "请输入正确的座机号")]

        [DataFieldAttribute("Phone", "nvarchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _FAX;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [RegularExpression(@"^(\d{3,4}-)?\d{7,8}$", ErrorMessage = "请输入正确的传真号")]

        [DataFieldAttribute("FAX", "nvarchar")]
        public string FAX
        {
            get { return _FAX; }
            set { _FAX = value; }
        }
        private int _ZipCode;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "请输入正确的邮编")]

        [DataFieldAttribute("ZipCode", "nvarchar")]
        public int ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        private string _COMWebsite;
        [StringLength(20, ErrorMessage = "公司网址不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]

        [RegularExpression(@"([\w-]+\.)+[\w-]+.([^a-z])(/[\w-: ./?%&=]*)?|[a-zA-Z\-\.][\w-]+.([^a-z])(/[\w-: ./?%&=]*)?", ErrorMessage = "请输入正确的网址")]

        [DataFieldAttribute("COMWebsite", "nvarchar")]
        public string COMWebsite
        {
            get { return _COMWebsite; }
            set { _COMWebsite = value; }
        }
        private string _ComAddress;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [StringLength(20, ErrorMessage = "公司地址不能超过20个字符")]
        [DataFieldAttribute("ComAddress", "nvarchar")]
        public string ComAddress
        {
            get { return _ComAddress; }
            set { _ComAddress = value; }
        }
        private string _Province;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Province", "nvarchar")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        private string _City;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("City", "nvarchar")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _ClientDesc;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ClientDesc", "nvarchar")]
        public string ClientDesc
        {
            get { return _ClientDesc; }
            set { _ClientDesc = value; }
        }
        private string _Remark;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _CType;
        [DataFieldAttribute("CType", "nvarchar")]
        public string CType
        {
            get { return _CType; }
            set { _CType = value; }
        }
        private string _CClass;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CClass", "nvarchar")]
        public string CClass
        {
            get { return _CClass; }
            set { _CClass = value; }
        }
        private string _CSource;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CSource", "nvarchar")]
        public string CSource
        {
            get { return _CSource; }
            set { _CSource = value; }
        }
        private string _CRelation;
        [DataFieldAttribute("CRelation", "nvarchar")]
        public string CRelation
        {
            get { return _CRelation; }
            set { _CRelation = value; }
        }
        private string _Maturity;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Maturity", "nvarchar")]
        public string Maturity
        {
            get { return _Maturity; }
            set { _Maturity = value; }
        }
        private string _State;
        [DataFieldAttribute("State", "nvarchar")]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private string _CreateTime;
        [DataFieldAttribute("CreateTime", "nvarchar")]
        public string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _Validate;
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }
    }
}
