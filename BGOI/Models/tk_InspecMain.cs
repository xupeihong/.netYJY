using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_InspecMain
    {
        private string SID;
        private string LinkPerson;
        private string LinkTel;
        private string InspecDate;
        private string BathID;

        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;

        public tk_InspecMain()
        {
            //TODO: 在此处添加构造函数逻辑
        }

        [Required(ErrorMessage = "送检单编号不能为空")]
        [StringLength(20, ErrorMessage = "送检单编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SID", "varchar")]
        public string strSID
        {
            get { return SID; }
            set { SID = value; }
        }
        
        [Required(ErrorMessage = "联系人不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("LinkPerson", "varchar")]
        public string strLinkPerson
        {
            get { return LinkPerson; }
            set { LinkPerson = value; }
        }

        [Required(ErrorMessage = "联系电话不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("LinkTel", "nvarchar")]
        public string strLinkTel
        {
            get { return LinkTel; }
            set { LinkTel = value; }
        }

        [Required(ErrorMessage = "送检日期不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("InspecDate", "nvarchar")]
        public string strInspecDate
        {
            get { return InspecDate; }
            set { InspecDate = value; }
        }

        [Required(ErrorMessage = "送检批次不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BathID", "nvarchar")]
        public string strBathID
        {
            get { return BathID; }
            set { BathID = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
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
