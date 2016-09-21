using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHReturnVisit_HIS
    {
        public string strHFID = "";
        [DataFieldAttribute("HFID", "varchar")]
        //回访编号
        public string HFID
        {
            get { return strHFID; }
            set { strHFID = value; }
        }
        public string strUntiID = "";
        [DataFieldAttribute("UntiID", "varchar")]
        //回访处理单位
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }
        public string strRecordID = "";
        [DataFieldAttribute("RecordID", "varchar")]
        //编号，自填
        public string RecordID
        {
            get { return strRecordID; }
            set { strRecordID = value; }
        }
        public DateTime strRVDate;
        [DataFieldAttribute("RVDate", "datetime")]
        //回访日期
        public DateTime RVDate
        {
            get { return strRVDate; }
            set { strRVDate = value; }
        }

        public string strProductID = "";
        [DataFieldAttribute("ProductID", "varchar")]
        //产品
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        public string strContactPerson = "";
        [DataFieldAttribute("ContactPerson", "varchar")]
        //联系人
        public string ContactPerson
        {
            get { return strContactPerson; }
            set { strContactPerson = value; }
        }
        public string strUserInformation = "";
        [DataFieldAttribute("UserInformation", "varchar")]
        //用户情况简述
        public string UserInformation
        {
            get { return strUserInformation; }
            set { strUserInformation = value; }
        }

        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strTel = "";
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strSatisfiedDegree = "";
        [DataFieldAttribute("SatisfiedDegree", "varchar")]
        //对此次服务满意度
        public string SatisfiedDegree
        {
            get { return strSatisfiedDegree; }
            set { strSatisfiedDegree = value; }
        }

        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        //创建人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strReturnVisit = "";
        [DataFieldAttribute("ReturnVisit", "varchar")]
        //回访人
        public string ReturnVisit
        {
            get { return strReturnVisit; }
            set { strReturnVisit = value; }
        }
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }
    }
}

