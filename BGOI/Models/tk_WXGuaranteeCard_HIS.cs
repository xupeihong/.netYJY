using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_WXGuaranteeCard_HIS
    {
        public string strBXKID = "";
        [DataFieldAttribute("BXKID", "varchar")]
        //维修记录编号
        public string BXKID
        {
            get { return strBXKID; }
            set { strBXKID = value; }
        }

        public string strBUnitID = "";
        [DataFieldAttribute("BUnitID", "varchar")]
        //保修卡所属单位
        public string BUnitID
        {
            get { return strBUnitID; }
            set { strBUnitID = value; }
        }

        public string strContractID = "";
        [DataFieldAttribute("ContractID", "varchar")]
        //合同编号
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }

        public string strBXKNum = "";
        [DataFieldAttribute("BXKNum", "varchar")]
        //保修卡编号
        public string BXKNum
        {
            get { return strBXKNum; }
            set { strBXKNum = value; }
        }

        public string strOrderContent = "";
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        //产品编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }


        public string strSpecsModels = "";
        [DataFieldAttribute("SpecsModels", "varchar")]
        //产品规格型号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        public DateTime strBuyDate;
        [DataFieldAttribute("BuyDate", "datetime")]
        //购买日期
        public DateTime BuyDate
        {
            get { return strBuyDate; }
            set { strBuyDate = value; }
        }

        public DateTime strBXDate;
        [DataFieldAttribute("BXDate", "datetime")]
        //保修时间
        public DateTime BXDate
        {
            get { return strBXDate; }
            set { strBXDate = value; }
        }

        public string strEndUnit = "";
        [DataFieldAttribute("EndUnit", "varchar")]
        //最终用户单位
        public string EndUnit
        {
            get { return strEndUnit; }
            set { strEndUnit = value; }
        }

        public string strContact = "";
        [DataFieldAttribute("Contact", "varchar")]
        //联系人
        public string Contact
        {
            get { return strContact; }
            set { strContact = value; }
        }

        public string strTel = "";
        [DataFieldAttribute("Tel", "varchar")]
        //联系方式
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
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
        private DateTime? _NCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return _NCreateTime; }
            set { _NCreateTime = value; }
        }
        private string _NCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return _NCreateUser; }
            set { _NCreateUser = value; }
        }
    }
}
