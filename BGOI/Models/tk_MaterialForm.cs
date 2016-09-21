using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public  class tk_MaterialForm
    {
        private string strLLID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("LLID", "nvarchar")]

        public string LLID
        {
            get { return strLLID; }
            set { strLLID = value; }
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

        private string strMaterialDepartment = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaterialDepartment", "nvarchar")]

        public string MaterialDepartment
        {
            get { return strMaterialDepartment; }
            set { strMaterialDepartment = value; }
        }

        private string strRWIDDID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWIDDID", "nvarchar")]

        public string RWIDDID
        {
            get { return strRWIDDID; }
            set { strRWIDDID = value; }
        }

        private int strAmount ;
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Amount", "int")]

        public int Amount
        {
            get { return strAmount; }
            set { strAmount = value; }
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

        private DateTime strMaterialTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("MaterialTime", "datetime")]

        public DateTime MaterialTime
        {
            get { return strMaterialTime; }
            set { strMaterialTime = value; }
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
        [DataFieldAttribute("CreateTime", "datetime")]

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

        private DateTime strCancelTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CancelTime", "datetime")]

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

        private DateTime strFinishTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishTime", "datetime")]

        public DateTime FinishTime
        {
            get { return strFinishTime; }
            set { strFinishTime = value; }
        }

        private string  strmaterState;
        [DataFieldAttribute("materState", "nvarchar")]

        public string  materState
        {
            get { return strmaterState; }
            set { strmaterState = value; }
        }

        private string strHouseID;
        [DataFieldAttribute("HouseID", "nvarchar")]

        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }
    }
}
