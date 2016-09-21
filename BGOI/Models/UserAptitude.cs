using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class UserAptitude
    {
        private string UserID;
        private string UserName;
        private string BusinessType;
        private string TecoName;
        private string TecoClass;
        private string GetTime;
        private string CertificatCode;
        private string CertificateName;
        private string CertificateUnit;
        private string LastCertificatDate;
        private string CertificatDate;
        private string FileName;
        //private byte[] FileInfo;
        private string Remark;
        private string Unit;
        private int State;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

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

        [DataFieldAttribute("BusinessType", "varchar")]
        public string StrBusinessType
        {
            get { return BusinessType; }
            set { BusinessType = value; }
        }

        [DataFieldAttribute("TecoName", "nvarchar")]
        public string StrTecoName
        {
            get { return TecoName; }
            set { TecoName = value; }
        }

        [DataFieldAttribute("TecoClass", "nvarchar")]
        public string StrTecoClass
        {
            get { return TecoClass; }
            set { TecoClass = value; }
        }

        [DataFieldAttribute("GetTime", "date")]
        public string StrGetTime
        {
            get { return GetTime; }
            set { GetTime = value; }
        }

        [DataFieldAttribute("CertificatCode", "varchar")]
        public string StrCertificatCode
        {
            get { return CertificatCode; }
            set { CertificatCode = value; }
        }

        [DataFieldAttribute("CertificateName", "nvarchar")]
        public string StrCertificateName
        {
            get { return CertificateName; }
            set { CertificateName = value; }
        }

        [DataFieldAttribute("CertificateUnit", "nvarchar")]
        public string StrCertificateUnit
        {
            get { return CertificateUnit; }
            set { CertificateUnit = value; }
        }

        [DataFieldAttribute("LastCertificatDate", "date")]
        public string StrLastCertificatDate
        {
            get { return LastCertificatDate; }
            set { LastCertificatDate = value; }
        }

        [DataFieldAttribute("CertificatDate", "date")]
        public string StrCertificatDate
        {
            get { return CertificatDate; }
            set { CertificatDate = value; }
        }

        [DataFieldAttribute("FileName", "varchar")]
        public string StrFileName
        {
            get { return FileName; }
            set { FileName = value; }
        }

        //[DataFieldAttribute("FileInfo", "varbinary")]
        //public byte[] StrFileInfo
        //{
        //    get { return FileInfo; }
        //    set { FileInfo = value; }
        //}

        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }

        [DataFieldAttribute("Unit", "nvarchar")]
        public string StrUnit
        {
            get { return Unit; }
            set { Unit = value; }
        }

        [DataFieldAttribute("State", "int")]
        public int StrState
        {
            get { return State; }
            set { State = value; }
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
