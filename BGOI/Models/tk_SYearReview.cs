using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
    public class tk_SYearReview
    {
        private string _YRID;
        [DataFieldAttribute("YRID", "varchar")]
        public string YRID
        {
            get { return _YRID; }
            set { _YRID = value; }
        }
        private int _Syear;
        [DataFieldAttribute("Syear", "Int")]
        public int Syear
        {
            get { return _Syear; }
            set { _Syear = value; }
        }
        private string _DeclareUnit;
        [DataFieldAttribute("DeclareUnit", "varchar")]
        public string DeclareUnit
        {
            get { return _DeclareUnit; }
            set { _DeclareUnit = value; }
        }
        private string _DeclareUser;
        [DataFieldAttribute("DeclareUser", "varchar")]
        public string DeclareUser
        {
            get { return _DeclareUser; }
            set { _DeclareUser = value; }
        }
        private DateTime? _ReviewDate;
        [DataFieldAttribute("ReviewDate", "DateTime")]
        public DateTime? ReviewDate
        {
            get { return _ReviewDate; }
            set { _ReviewDate = value; }
        }
        private string _Approval1;
        [DataFieldAttribute("Approval1", "varchar")]
        public string Approval1
        {
            get { return _Approval1; }
            set { _Approval1 = value; }
        }
        private string _Approval1User;
        [DataFieldAttribute("Approval1User", "varchar")]
        public string Approval1User
        {
            get { return _Approval1User; }
            set { _Approval1User = value; }
        }
        private string _Approval2;
        [DataFieldAttribute("Approval2", "varchar")]
        public string Approval2
        {
            get { return _Approval2; }
            set { _Approval2 = value; }
        }
        private string _Approval2User;
        [DataFieldAttribute("Approval2User", "varchar")]
        public string Approval2User
        {
            get { return _Approval2User; }
            set { _Approval2User = value; }
        }
        private string _Countersign1;
        [DataFieldAttribute("Countersign1", "varchar")]
        public string Countersign1
        {
            get { return _Countersign1; }
            set { _Countersign1 = value; }
        }
        private string _Countersign2;
        [DataFieldAttribute("Countersign2", "varchar")]
        public string Countersign2
        {
            get { return _Countersign2; }
            set { _Countersign2 = value; }
        }
        private string _Countersign3;
        [DataFieldAttribute("Countersign3", "varchar")]
        public string Countersign3
        {
            get { return _Countersign3; }
            set { _Countersign3 = value; }
        }
        private string _Countersign4;
        [DataFieldAttribute("Countersign4", "varchar")]
        public string Countersign4
        {
            get { return _Countersign4; }
            set { _Countersign4 = value; }
        }
        private string _Countersign5;
        [DataFieldAttribute("Countersign5", "varchar")]
        public string Countersign5
        {
            get { return _Countersign5; }
            set { _Countersign5 = value; }
        }
        private string _Countersign6;
        [DataFieldAttribute("Countersign6", "varchar")]
        public string Countersign6
        {
            get { return _Countersign6; }
            set { _Countersign6 = value; }
        }
        private string _Countersign;
        [DataFieldAttribute("Countersign", "varchar")]
        public string Countersign
        {
            get { return _Countersign; }
            set { _Countersign = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
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
        private string _ApprovalState;
        [DataFieldAttribute("ApprovalState", "varchar")]
        public string ApprovalState
        {
            get { return _ApprovalState; }
            set { _ApprovalState = value; }
        }
    }
}
