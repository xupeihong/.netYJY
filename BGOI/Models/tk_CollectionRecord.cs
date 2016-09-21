using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_CollectionRecord
    {
        public string strCRID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CRID", "varchar")]
        public string CRID
        {
            get { return strCRID; }
            set { strCRID = value; }
        }

        public DateTime strCRTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CRTime", "datetime")]
        //日期
        public DateTime CRTime
        {
            get { return strCRTime; }
            set { strCRTime = value; }
        }
        public string strPaymentUnit = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentUnit", "varchar")]
        //付款单位
        public string PaymentUnit
        {
            get { return strPaymentUnit; }
            set { strPaymentUnit = value; }
        }
        public string strCollectionAmount = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CollectionAmount", "varchar")]
        //收款额
        public string CollectionAmount
        {
            get { return strCollectionAmount; }
            set { strCollectionAmount = value; }
        }
        public string strPaymentContent = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentContent", "varchar")]
        //付款内容
        public string PaymentContent
        {
            get { return strPaymentContent; }
            set { strPaymentContent = value; }
        }
        public string strPaymentMethod = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentMethod", "varchar")]
        //付款方式
        public string PaymentMethod
        {
            get { return strPaymentMethod; }
            set { strPaymentMethod = value; }
        }
        public string strPaymentPeo = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentPeo", "varchar")]
        //收款人
        public string PaymentPeo
        {
            get { return strPaymentPeo; }
            set { strPaymentPeo = value; }
        }
        public string strCorporateFinance = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CorporateFinance", "varchar")]
        //公司财务
        public string CorporateFinance
        {
            get { return strCorporateFinance; }
            set { strCorporateFinance = value; }
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
        public string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "varchar")]
        public string State
        {
            get { return strState; }
            set { strState = value; }
        }
        public string strBRDID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BRDID", "varchar")]
        public string BRDID
        {
            get { return strBRDID; }
            set { strBRDID = value; }
        }

        public string strStateNew = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StateNew", "varchar")]
        public string StateNew
        {
            get { return strStateNew; }
            set { strStateNew = value; }
        }
    }
}
