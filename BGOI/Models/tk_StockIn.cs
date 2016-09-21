using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_StockIn
    {
        private string StockID;// 系统生成 按照规则
        private string RID;
        private string BathID;
        private string ReceiveUser;
        private int? InCount;
        private string StockInTime;
        private string StockUnit;
       // private string Validate;
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        public tk_StockIn()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockID", "nvarchar")]
        public string strStockID
        {
            get { return StockID; }
            set { StockID = value; }
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RID", "nvarchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [DataFieldAttribute("BathID", "nvarchar")]
        public string strBathID
        {
            get { return BathID; }
            set { BathID = value; }
        }

        [Required(ErrorMessage = "承接人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiveUser", "nvarchar")]
        public string strReceiveUser
        {
            get { return ReceiveUser; }
            set { ReceiveUser = value; }
        }

        [Required(ErrorMessage = "入库数量不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("InCount", "int")]
        public int? strInCount
        {
            get { return InCount; }
            set { InCount = value; }
        }

        [Required(ErrorMessage = "入库时间不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockInTime", "nvarchar")]
        public string strStockInTime
        {
            get { return StockInTime; }
            set { StockInTime = value; }
        }

        [DataFieldAttribute("StockUnit", "nvarchar")]
        public string strStockUnit
        {
            get { return StockUnit; }
            set { StockUnit = value; }
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


    }
}