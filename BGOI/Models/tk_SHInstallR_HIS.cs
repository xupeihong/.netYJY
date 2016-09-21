using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHInstallR_HIS
    {
        public string strBZID = "";
        [DataFieldAttribute("BZID", "varchar")]
        //报装编号
        public string BZID
        {
            get { return strBZID; }
            set { strBZID = value; }
        }

        //报装建立单位
        public string strUntiID = "";
        [DataFieldAttribute("UntiID", "varchar")]
        public string UntiID
        {
            get { return strUntiID; }
            set { strUntiID = value; }
        }

        //客户姓名
        public string strCustomerName = "";
        [DataFieldAttribute("CustomerName", "varchar")]
        public string CustomerName
        {
            get { return strCustomerName; }
            set { strCustomerName = value; }
        }

        public DateTime strInstallTime;
        [DataFieldAttribute("InstallTime", "datetime")]
        //报装时间
        public DateTime InstallTime
        {
            get { return strInstallTime; }
            set { strInstallTime = value; }
        }

        //地址
        public string strAddress = "";
        [DataFieldAttribute("Address", "varchar")]
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        //联系方式
        public string strTel = "";
        [DataFieldAttribute("Tel", "varchar")]
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        //备注
        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        //出库二级库房
        public string strWarehouseTwo = "";
        [DataFieldAttribute("WarehouseTwo", "varchar")]
        public string WarehouseTwo
        {
            get { return strWarehouseTwo; }
            set { strWarehouseTwo = value; }
        }
        //是否调拨
        public string strWIsWhether = "";
        [DataFieldAttribute("IsWhether", "varchar")]
        public string IsWhether
        {
            get { return strWIsWhether; }
            set { strWIsWhether = value; }
        }
        //调拨一级库房
        public string strWarehouseOne = "";
        [DataFieldAttribute("WarehouseOne", "varchar")]
        public string WarehouseOne
        {
            get { return strWarehouseOne; }
            set { strWarehouseOne = value; }
        }
        //登记人
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        //状态
        public string strSate = "";
        [DataFieldAttribute("Sate", "varchar")]
        public string Sate
        {
            get { return strSate; }
            set { strSate = value; }
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

        //关联ID
        public string strRelationID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RelationID", "varchar")]
        public string RelationID
        {
            get { return strRelationID; }
            set { strRelationID = value; }
        }
        //分公司
        public string strBZCompany = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BZCompany", "varchar")]
        public string BZCompany
        {
            get { return strBZCompany; }
            set { strBZCompany = value; }
        }

        //派工人员
        public string strDiPer = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DiPer", "varchar")]
        public string DiPer
        {
            get { return strDiPer; }
            set { strDiPer = value; }
        }
    }
}

