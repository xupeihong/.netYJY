using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_RepairInfo
    {
        private string RID;
        private string RepairID;
        private string RepairUser;
        private DateTime? RepairSDate;
        private DateTime? RepairEDate;
        private string MeterID;
        private string Breakdown;
        private string RepairContent;
        private string RepairNum;
        private string AdjustPre;
        private string AdjustNow;

        private string RepairResult;
        private string Remark;
        private string validate;
        private DateTime? CreateTime;
        private string CreateUser;
        private string IsPhoto;


        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
        public string StrRepairID
        {
            get { return RepairID; }
            set { RepairID = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRepairUser
        {
            get { return RepairUser; }
            set { RepairUser = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrRepairSDate
        {
            get { return RepairSDate; }
            set { RepairSDate = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrRepairEDate
        {
            get { return RepairEDate; }
            set { RepairEDate = value; }
        }
        public string StrMeterID
        {
            get { return MeterID; }
            set { MeterID = value; }
        }
        public string StrBreakdown
        {
            get { return Breakdown; }
            set { Breakdown = value; }
        }
        public string StrRepairContent
        {
            get { return RepairContent; }
            set { RepairContent = value; }
        }
        public string StrRepairNum
        {
            get { return RepairNum; }
            set { RepairNum = value; }
        }

        public string StrAdjustPre
        {
            get { return AdjustPre; }
            set { AdjustPre = value; }
        }
        public string StrAdjustNow
        {
            get { return AdjustNow; }
            set { AdjustNow = value; }
        }
        public string StrRepairResult
        {
            get { return RepairResult; }
            set { RepairResult = value; }
        }
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



    public class tk_RepairInfoSearch
    {
        private string RID;

        private string RepairUser;
        private DateTime? RepairSDate;
        private DateTime? RepairEDate;

        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRepairUser
        {
            get { return RepairUser; }
            set { RepairUser = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrRepairSDate
        {
            get { return RepairSDate; }
            set { RepairSDate = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrRepairEDate
        {
            get { return RepairEDate; }
            set { RepairEDate = value; }
        }

    }
}
