using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductRecord
    {
        private string strSGID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))] 
        [DataFieldAttribute("SGID", "nvarchar")]

        public string SGID
        {
            get { return strSGID; }
            set { strSGID = value; }
        }

        private string strRWID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWID", "nvarchar")]

        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
        }

        private string strID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ID", "nvarchar")]

        public string ID
        {
            get { return strID; }
            set { strID = value; }
        }

        private DateTime strbilling ;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("billing", "datetime")]

        public DateTime billing
        {
            get { return strbilling; }
            set { strbilling = value; }
        }

        private string strOrderContent = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OrderContent", "nvarchar")]

        public string OrderContent
        {
            get { return strOrderContent; }
            set { strOrderContent = value; }
        }

        private string strSpecsModels = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SpecsModels", "nvarchar")]

        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        private string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]

        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        private string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "nvarchar")]

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private DateTime strCreateTime ;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "DateTime")]

        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "nvarchar")]

        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private DateTime strFinishTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishTime", "DateTime")]

        public DateTime FinishTime
        {
            get { return strFinishTime; }
            set { strFinishTime = value; }
        }

        private DateTime strCancelTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelTime", "DateTime")]

        public DateTime CancelTime
        {
            get { return strCancelTime; }
            set { strCancelTime = value; }
        }

        private string strCancelReason = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelReason", "nvarchar")]

        public string CancelReason
        {
            get { return strCancelReason; }
            set { strCancelReason = value; }
        }


        private string strm = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("m", "nvarchar")]

        public string m
        {
            get { return strm; }
            set { strm = value; }
        }
    }
}
