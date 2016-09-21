using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_IsNotSupplierBas
    {
        private string _SID;
        [DataFieldAttribute("SID", "nvarchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _COMNameC;
        [Remote("CheckisnotSupplyName", "SuppliesManage", ErrorMessage = "该供应商名称已存在")]
        [DataFieldAttribute("COMNameC", "nvarchar")]
        public string COMNameC
        {
            get { return _COMNameC; }
            set { _COMNameC = value; }
        }
        private string _SupplyContent;
        [DataFieldAttribute("SupplyContent", "nvarchar")]
        public string SupplyContent
        {
            get { return _SupplyContent; }
            set { _SupplyContent = value; }
        }
        private string _Contacts;
        [DataFieldAttribute("Contacts", "nvarchar")]
        public string Contacts
        {
            get { return _Contacts; }
            set { _Contacts = value; }
        }
        private string _TelFax;
        [DataFieldAttribute("TelFax", "nvarchar")]
        public string TelFax
        {
            get { return _TelFax; }
            set { _TelFax = value; }
        }
        private string _Phone;
        [DataFieldAttribute("Phone", "nvarchar")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Mailbox;
        [DataFieldAttribute("Mailbox", "nvarchar")]
        public string Mailbox
        {
            get { return _Mailbox; }
            set { _Mailbox = value; }
        }
        private string _Remarks;
        [DataFieldAttribute("Remarks", "nvarchar")]
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _UnitID;
        [DataFieldAttribute("UnitID", "nvarchar")]
        public string UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }
        private string _Validate;
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }
        private string _CreateTime;
        [DataFieldAttribute("CreateTime", "nvarchar")]
        public string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private string _State;
        [DataFieldAttribute("State", "nvarchar")]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
    }
}
