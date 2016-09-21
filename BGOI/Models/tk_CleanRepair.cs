using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_CleanRepair
    {
        private string RID;
        private string CleanID;
        private string CleanUser;
        private DateTime? CleanSDate;
        private DateTime? CleanEDate;
        private string MeterID;
        private string Breakdown;
        private string Remark;
        private string validate;
        private DateTime? CreateTime;
        private string CreateUser;
        private string IsPhoto;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
        public string StrCleanID
        {
            get { return CleanID; }
            set { CleanID = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCleanUser
        {
            get { return CleanUser; }
            set { CleanUser = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrCleanSDate
        {
            get { return CleanSDate; }
            set { CleanSDate = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrCleanEDate
        {
            get { return CleanEDate; }
            set { CleanEDate = value; }
        }
        public string StrMeterID
        {
            get { return MeterID; }
            set { MeterID = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public string StrBreakdown
        {
            get { return Breakdown; }
            set { Breakdown = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }
        public string Strvalidate
        {
            get { return validate; }
            set { validate = value; }
        }
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }
        public string StrIsPhoto
        {
            get { return IsPhoto; }
            set { IsPhoto = value; }
        }
    }

    public class tk_CleanRepairSearch
    {
        private string RID;

        private string CleanUser;
        private DateTime? CleanSDate;
        private DateTime? CleanEDate;


        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }


        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCleanUser
        {
            get { return CleanUser; }
            set { CleanUser = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrCleanSDate
        {
            get { return CleanSDate; }
            set { CleanSDate = value; }
        }

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrCleanEDate
        {
            get { return CleanEDate; }
            set { CleanEDate = value; }
        }

    }
}
