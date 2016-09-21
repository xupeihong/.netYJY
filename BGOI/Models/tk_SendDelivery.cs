using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_SendDelivery
    {
        private string DeliverID;// 系统生成 按照规则
        private string RID;// 外键
        private string UnitName;
        private string ReceiveName;
        private string ReceiveTel;
        private string ReceiveDate;
        private string ReceiveAddr;
        private string SendName;
        private string SendTel;
        private string SendDate;
        private string Validate;
        private DateTime? CreateTime;
        private string CreateUser;

        public tk_SendDelivery()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [Required(ErrorMessage = "发货单号不能为空")]
        [StringLength(20, ErrorMessage = "发货单号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("DeliverID", "nvarchar")]
        public string strDeliverID
        {
            get { return DeliverID; }
            set { DeliverID = value; }
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

        [Required(ErrorMessage = "收货地址不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ReceiveAddr", "nvarchar")]
        public string strReceiveAddr
        {
            get { return ReceiveAddr; }
            set { ReceiveAddr = value; }
        }

        [DataFieldAttribute("SendName", "nvarchar")]
        public string strSendName
        {
            get { return SendName; }
            set { SendName = value; }
        }

        [DataFieldAttribute("SendTel", "nvarchar")]
        public string strSendTel
        {
            get { return SendTel; }
            set { SendTel = value; }
        }

        [DataFieldAttribute("SendDate", "nvarchar")]
        public string strSendDate
        {
            get { return SendDate; }
            set { SendDate = value; }
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