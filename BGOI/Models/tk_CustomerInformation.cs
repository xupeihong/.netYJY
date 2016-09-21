using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_CustomerInformation
    {
        public string strKHID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("KHID", "varchar")]
        //客户编号
        public string KHID
        {
            get { return strKHID; }
            set { strKHID = value; }
        }
        public string strCusName = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CusName", "varchar")]
        //客户名称
        public string CusName
        {
            get { return strCusName; }
            set { strCusName = value; }
        }
        public string strCusTel = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CusTel", "varchar")]
        //客户电话
        public string CusTel
        {
            get { return strCusTel; }
            set { strCusTel = value; }
        }
        public string strCusAdd = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CusAdd", "varchar")]
        //客户地址
        public string CusAdd
        {
            get { return strCusAdd; }
            set { strCusAdd = value; }
        }
        public string strCusUnit = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CusUnit", "varchar")]
        //客户单位
        public string CusUnit
        {
            get { return strCusUnit; }
            set { strCusUnit = value; }
        }
        public string strCusEmail = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CusEmail", "varchar")]
        //客户邮箱
        public string CusEmail
        {
            get { return strCusEmail; }
            set { strCusEmail = value; }
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
        public string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "varchar")]
        //客户信息
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
