using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SApproval
    {
        private string _PID;
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return _PID; }
            set { _PID = value; }
        }
        private string _SID;
        [DataFieldAttribute("SID", "varchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }
        private string _ApprovalType;
        [DataFieldAttribute("ApprovalType", "varchar")]
        public string ApprovalType
        {
            get { return _ApprovalType; }
            set { _ApprovalType = value; }
        }
        private string _ApprovalLever;
        [DataFieldAttribute("ApprovalLever", "varchar")]
        public string ApprovalLever
        {
            get { return _ApprovalLever; }
            set { _ApprovalLever = value; }
        }
        private string _ApprovalContent;
        [DataFieldAttribute("ApprovalContent", "varchar")]
        public string ApprovalContent
        {
            get { return _ApprovalContent; }
            set { _ApprovalContent = value; }
        }
        private string _ApprovalPersons;
        [DataFieldAttribute("ApprovalPersons", "varchar")]
        public string ApprovalPersons
        {
            get { return _ApprovalPersons; }
            set { _ApprovalPersons = value; }
        }
        private string _Job;
        [DataFieldAttribute("Job", "varchar")]
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        private DateTime? _ApprovalTime;
        [DataFieldAttribute("ApprovalTime", "varchar")]
        public DateTime? ApprovalTime
        {
            get { return _ApprovalTime; }
            set { _ApprovalTime = value; }
        }
        private string _IsPass;
        [DataFieldAttribute("IsPass", "varchar")]
        public string IsPass
        {
            get { return _IsPass; }
            set { _IsPass = value; }
        }
        private string _NoPassReason;
        [DataFieldAttribute("NoPassReason", "varchar")]
        public string NoPassReason
        {
            get { return _NoPassReason; }
            set { _NoPassReason = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("NoPassReason", "varchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _CreateTime;
        [DataFieldAttribute("CreateTime", "varchar")]
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
        private string _State;
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _ApprovalMan;
        [DataFieldAttribute("ApprovalMan", "varchar")]
        public string ApprovalMan
        {
            get { return _ApprovalMan; }
            set { _ApprovalMan = value; }
        }
        private string _Remark;
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _Opinion;
        [DataFieldAttribute("Opinion", "varchar")]
        public string Opinion
        {
            get { return _Opinion; }
            set { _Opinion = value; }
        }
    }
}
