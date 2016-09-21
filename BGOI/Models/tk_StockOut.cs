using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_StockOut
    {
        private string OutID;// 系统生成 按照规则
        private string RID;
        private string ReceiveUser;
        private int? OutCount;
        private string StockOutTime;
        private string StockUnit;
        private string OperateUser;
        private string Validate;
        private DateTime? CreateTime;
        private string CreateUser;
       

        public tk_StockOut()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OutID", "nvarchar")]
        public string strOutID
        {
            get { return OutID; }
            set { OutID = value; }
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RID", "nvarchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }
        
        [Required(ErrorMessage = "领用人/单位不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiveUser", "nvarchar")]
        public string strReceiveUser
        {
            get { return ReceiveUser; }
            set { ReceiveUser = value; }
        }

        [Required(ErrorMessage = "出库数量不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("OutCount", "int")]
        public int? strOutCount
        {
            get { return OutCount; }
            set { OutCount = value; }
        }

        [Required(ErrorMessage = "出库时间不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockOutTime", "nvarchar")]
        public string strStockOutTime
        {
            get { return StockOutTime; }
            set { StockOutTime = value; }
        }

        [DataFieldAttribute("StockUnit", "nvarchar")]
        public string strStockUnit
        {
            get { return StockUnit; }
            set { StockUnit = value; }
        }

        [DataFieldAttribute("OperateUser", "nvarchar")]
        public string strOperateUser
        {
            get { return OperateUser; }
            set { OperateUser = value; }
        }
        
        [DataFieldAttribute("Validate", "nvarchar")]
        public string strValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }

        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime? strCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string strCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        private string strUse = "";
        [DataFieldAttribute("[Use]", "varchar")]
        public string Use
        {
            get { return strUse; }
            set { strUse = value; }
        }
    }
}