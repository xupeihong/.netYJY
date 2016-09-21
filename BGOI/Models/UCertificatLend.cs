using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class UCertificatLend
    {
        private int ID;
        private string UserID;
        private string UserName;
        private string CertificatCode;
        private string LendDate;
        private string LendUnit;
        private string LendReason;
        private string ReturnDate;
        private string Remark;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        [DataFieldAttribute("ID", "int")]
        public int StrID
        {
            get { return ID; }
            set { ID = value; }
        }

        [DataFieldAttribute("UserID", "varchar")]
        public string StrUserID
        {
            get { return UserID; }
            set { UserID = value; }
        }

        [DataFieldAttribute("UserName", "varchar")]
        public string StrUserName
        {
            get { return UserName; }
            set { UserName = value; }
        }

        [DataFieldAttribute("CertificatCode", "varchar")]
        public string StrCertificatCode
        {
            get { return CertificatCode; }
            set { CertificatCode = value; }
        }

        [DataFieldAttribute("LendDate", "date")]
        public string StrLendDate
        {
            get { return LendDate; }
            set { LendDate = value; }
        }

        [DataFieldAttribute("LendUnit", "nvarchar")]
        public string StrLendUnit
        {
            get { return LendUnit; }
            set { LendUnit = value; }
        }

        [DataFieldAttribute("LendReason", "nvarchar")]
        public string StrLendReason
        {
            get { return LendReason; }
            set { LendReason = value; }
        }

        [DataFieldAttribute("ReturnDate", "date")]
        public string StrReturnDate
        {
            get { return ReturnDate; }
            set { ReturnDate = value; }
        }

        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
