using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SProcessInfo
    {
        private string _SID;
        [DataFieldAttribute("SID", "varchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _Reason;
        [DataFieldAttribute("Reason", "varchar")]
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        private string _DeclareUser;
        [DataFieldAttribute("DeclareUser", "varchar")]
        public string DeclareUser
        {
            get { return _DeclareUser; }
            set { _DeclareUser = value; }
        }
        private string _Opinions;
        [DataFieldAttribute("Opinions", "varchar")]
        public string Opinions
        {
            get { return _Opinions; }
            set { _Opinions = value; }
        }
        private string _OpinionsD;
        [DataFieldAttribute("OpinionsD", "varchar")]
        public string OpinionsD
        {
            get { return _OpinionsD; }
            set { _OpinionsD = value; }
        }

        private string _isCompany;
        [DataFieldAttribute("OpinionsD", "varchar")]
        public string isCompany
        {
            get { return _isCompany; }
            set { _isCompany = value; }
        }
        private string _DeclareUnit;
        [DataFieldAttribute("DeclareUnit", "varchar")]
        public string DeclareUnit
        {
            get { return _DeclareUnit; }
            set { _DeclareUnit = value; }
        }
        private string _ReviewDate;
        [DataFieldAttribute("ReviewDate", "DateTime")]
        public string ReviewDate
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
        private DateTime? _ApprovalTime1;
        [DataFieldAttribute("ApprovalTime1", "DateTime")]
        public DateTime? ApprovalTime1
        {
            get { return _ApprovalTime1; }
            set { _ApprovalTime1 = value; }
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
        private DateTime? _ApprovalTime2;
        [DataFieldAttribute("ApprovalTime2", "DateTime")]
        public DateTime? ApprovalTime2
        {
            get { return _ApprovalTime2; }
            set { _ApprovalTime2 = value; }
        }
        private string _Approval3;
        [DataFieldAttribute("Approval3", "varchar")]
        public string Approval3
        {
            get { return _Approval3; }
            set { _Approval3 = value; }
        }
        private string _Approval3User;
        [DataFieldAttribute("Approval3User", "varchar")]
        public string Approval3User
        {
            get { return _Approval3User; }
            set { _Approval3User = value; }
        }
        private DateTime? _ApprovalTime3;
        [DataFieldAttribute("ApprovalTime3", "DateTime")]
        public DateTime? ApprovalTime3
        {
            get { return _ApprovalTime3; }
            set { _ApprovalTime3 = value; }
        }
        private string _Approval4;
        [DataFieldAttribute("Approval4", "varchar")]
        public string Approval4
        {
            get { return _Approval4; }
            set { _Approval4 = value; }
        }
        private string _Approval4User;
        [DataFieldAttribute("Approval4User", "varchar")]
        public string Approval4User
        {
            get { return _Approval4User; }
            set { _Approval4User = value; }
        }
        private DateTime? _ApprovalTime4;
        [DataFieldAttribute("ApprovalTime4", "DateTime")]
        public DateTime? ApprovalTime4
        {
            get { return _ApprovalTime4; }
            set { _ApprovalTime4 = value; }
        }
        private string _Approval5;
        [DataFieldAttribute("Approval5", "varchar")]
        public string Approval5
        {
            get { return _Approval5; }
            set { _Approval5 = value; }
        }
        private string _Approval5User;
        [DataFieldAttribute("Approval5User", "varchar")]
        public string Approval5User
        {
            get { return _Approval5User; }
            set { _Approval5User = value; }
        }
        private DateTime? _ApprovalTime5;
        [DataFieldAttribute("ApprovalTime5", "DateTime")]
        public DateTime? ApprovalTime5
        {
            get { return _ApprovalTime5; }
            set { _ApprovalTime5 = value; }
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
        private string _SPState;
        [DataFieldAttribute("SPState", "varchar")]
        public string SPState
        {
            get { return _SPState; }
            set { _SPState = value; }
        }
        private DateTime? _Time1;
        [DataFieldAttribute("Time1", "DateTime")]
        public DateTime? Time1
        {
            get { return _Time1; }
            set { _Time1 = value; }
        }
        private string _ApprovalState;
        [DataFieldAttribute("ApprovalState", "varchar")]
        public string ApprovalState
        {
            get { return _ApprovalState; }
            set { _ApprovalState = value; }
        }
        private string _ISAgree;
        [DataFieldAttribute("ISAgree", "varchar")]
        public string ISAgree
        {
            get { return _ISAgree; }
            set { _ISAgree = value; }
        }
        private string _OpinOut;
        [DataFieldAttribute("OpinOut", "varchar")]
        public string OpinOut
        {
            get { return _OpinOut; }
            set { _OpinOut = value; }
        }
        private string _RecoverReson;
        [DataFieldAttribute("RecoverReson", "varchar")]
        public string RecoverReson
        {
            get { return _RecoverReson; }
            set { _RecoverReson = value; }
        }
        private string _Recovername;
        [DataFieldAttribute("Recovername", "varchar")]
        public string Recovername
        {
            get { return _Recovername; }
            set { _Recovername = value; }
        }
    }
}
