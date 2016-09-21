using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_WXGuaranteeCard
    {

        public string strBXKID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXKID", "varchar")]
        //维修记录编号
        public string BXKID
        {
            get { return strBXKID; }
            set { strBXKID = value; }
        }

        public string strBUnitID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BUnitID", "varchar")]
        //保修卡所属单位
        public string BUnitID
        {
            get { return strBUnitID; }
            set { strBUnitID = value; }
        }

        public string strContractID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ContractID", "varchar")]
        //合同编号
        public string ContractID
        {
            get { return strContractID; }
            set { strContractID = value; }
        }

        public string strBXKNum = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXKNum", "varchar")]
        //保修卡编号
        public string BXKNum
        {
            get { return strBXKNum; }
            set { strBXKNum = value; }
        }

        public string strOrderContent = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContent", "varchar")]
        //产品名称
        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        //产品编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }


        public string strSpecsModels = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SpecsModels", "varchar")]
        //产品规格型号
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        public DateTime strBuyDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BuyDate", "datetime")]
        //购买日期
        public DateTime BuyDate
        {
            get { return strBuyDate; }
            set { strBuyDate = value; }
        }

        public DateTime strBXDate;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BXDate", "datetime")]
        //保修时间
        public DateTime BXDate
        {
            get { return strBXDate; }
            set { strBXDate = value; }
        }

        public string strEndUnit = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("EndUnit", "varchar")]
        //最终用户单位
        public string EndUnit
        {
            get { return strEndUnit; }
            set { strEndUnit = value; }
        }

        public string strContact = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Contact", "varchar")]
        //联系人
        public string Contact
        {
            get { return strContact; }
            set { strContact = value; }
        }

        public string strTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Tel", "varchar")]
        //联系方式
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        public string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public DateTime strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        public string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strUserAdd = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UserAdd", "varchar")]
        //客户地址
        public string UserAdd
        {
            get { return strUserAdd; }
            set { strUserAdd = value; }
        }
    }
}
