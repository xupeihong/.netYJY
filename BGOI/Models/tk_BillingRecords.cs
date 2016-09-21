using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_BillingRecords
    {
        public string strBRDID = "";
        //序号
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BRDID", "varchar")]
        public string BRDID
        {
            get { return strBRDID; }
            set { strBRDID = value; }
        }
        public DateTime strBRDTime;
        //时间
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BRDTime", "datetime")]
        public DateTime BRDTime
        {
            get { return strBRDTime; }
            set { strBRDTime = value; }
        }
        public string strDwName = "";
        //单位名称
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DwName", "varchar")]
        public string DwName
        {
            get { return strDwName; }
            set { strDwName = value; }
        }
        public string strProject = "";
        //项目
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Project", "varchar")]
        public string Project
        {
            get { return strProject; }
            set { strProject = value; }
        }
        public int strAmount;
        //金额
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Amount", "int")]
        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
        }						
        public string strPersonCharge = "";
        //负责人
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PersonCharge", "varchar")]
        public string PersonCharge
        {
            get { return strPersonCharge; }
            set { strPersonCharge = value; }
        }
        public DateTime strReceivablesTime;
        //收款日期
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceivablesTime", "datetime")]
        public DateTime ReceivablesTime
        {
            get { return strReceivablesTime; }
            set { strReceivablesTime = value; }
        }
        public string strPaymentMethod = "";
        //支付方式
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PaymentMethod", "varchar")]
        public string PaymentMethod
        {
            get { return strPaymentMethod; }
            set { strPaymentMethod = value; }
        }
        public string strRemark = "";
        //备注
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
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
    }
}
