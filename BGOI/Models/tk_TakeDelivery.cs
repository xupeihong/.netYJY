using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_TakeDelivery
    {
        private string TakeID;// 系统生成 按照规则
        private string RID;// 外键
        private string UnitName;
        private string DeliverName;
        private string DeliverTel;
        private string DeliverDate;
        private string ReceiveName;
        private string ReceiveTel;
        private string ReceiveDate;
        private string Validate;
        private DateTime? CreateTime;
        private string CreateUser;

        public tk_TakeDelivery()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [Required(ErrorMessage = "收货单号不能为空")]
        [StringLength(20, ErrorMessage = "收货单号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TakeID", "nvarchar")]
        public string strTakeID
        {
            get { return TakeID; }
            set { TakeID = value; }
        }

        [Required(ErrorMessage = "登记号不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RID", "nvarchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [DataFieldAttribute("UnitName", "nvarchar")]
        public string strUnitName
        {
            get { return UnitName; }
            set { UnitName = value; }
        }

        [DataFieldAttribute("DeliverName", "nvarchar")]
        public string strDeliverName
        {
            get { return DeliverName; }
            set { DeliverName = value; }
        }

        [DataFieldAttribute("DeliverTel", "nvarchar")]
        public string strDeliverTel
        {
            get { return DeliverTel; }
            set { DeliverTel = value; }
        }

        [DataFieldAttribute("DeliverDate", "nvarchar")]
        public string strDeliverDate
        {
            get { return DeliverDate; }
            set { DeliverDate = value; }
        }

        [Required(ErrorMessage = "收货人姓名不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiveName", "nvarchar")]
        public string strReceiveName
        {
            get { return ReceiveName; }
            set { ReceiveName = value; }
        }

        [DataFieldAttribute("ReceiveTel", "nvarchar")]
        public string strReceiveTel
        {
            get { return ReceiveTel; }
            set { ReceiveTel = value; }
        }

        [Required(ErrorMessage = "收货日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiveDate", "nvarchar")]
        public string strReceiveDate
        {
            get { return ReceiveDate; }
            set { ReceiveDate = value; }
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