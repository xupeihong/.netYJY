using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TECOCITY_BGOI
{
    public class tk_CheckData
    {
        private string RID;
        private string RepairID;
        private string RepairType;
        public string RepairMethod;
        private string MeterID;
        private string CheckUser;
        private DateTime? CheckDate;
        private string Qmin;
        private string Qmax1;
        private string Qmax2;
        private string Qmax25;
        private string Qmax4;
        private string Qmax7;
        private string Qmax;
        private string Avg_Qmin;
        private string Avg_Qmax1;
        private string Avg_Qmax2;
        private string Avg_Qmax25;
        private string Avg_Qmax4;
        private string Avg_Qmax7;
        private string Avg_Qmax;
        private string Repeat_Qmin;
        private string Repeat_Qmax1;
        private string Repeat_Qmax2;
        private string Repeat_Qmax25;
        private string Repeat_Qmax4;
        private string Repeat_Qmax7;
        private string Repeat_Qmax;
        private string Ratio;
        private string q1;
        private string q2;
        private string PDeviation;
        private string Oratio;
        private string Remark;
        private string validate;
        private string CreateUser;
        private DateTime? CreateTime;
        private string AvgRepeat;


        public string StrAvgRepeat
        {
            get { return AvgRepeat; }
            set { AvgRepeat = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRepairID
        {
            get { return RepairID; }
            set { RepairID = value; }
        }
        public string StrRepairType
        {
            get { return RepairType; }
            set { RepairType = value; }
        }
         [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRepairMethod
        {
            get { return RepairMethod; }
            set { RepairMethod = value; }
        }
        public string StrMeterID
        {
            get { return MeterID; }
            set { MeterID = value; }
        }
        [Required(ErrorMessage = "不能为空")]
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCheckUser
        {
            get { return CheckUser; }
            set { CheckUser = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public DateTime? StrCheckDate
        {
            get { return CheckDate; }
            set { CheckDate = value; }
        }

        public string StrQmin
        {
            get { return Qmin; }
            set { Qmin = value; }
        }
        public string StrQmax1
        {
            get { return Qmax1; }
            set { Qmax1 = value; }
        }
        public string StrQmax2
        {
            get { return Qmax2; }
            set { Qmax2 = value; }
        }
        public string StrQmax25
        {
            get { return Qmax25; }
            set { Qmax25 = value; }
        }
        public string StrQmax4
        {
            get { return Qmax4; }
            set { Qmax4 = value; }
        }
        public string StrQmax7
        {
            get { return Qmax7; }
            set { Qmax7 = value; }
        }
        public string StrQmax
        {
            get { return Qmax; }
            set { Qmax = value; }
        }
        public string StrAvg_Qmin
        {
            get { return Avg_Qmin; }
            set { Avg_Qmin = value; }
        }
        public string StrAvg_Qmax1
        {
            get { return Avg_Qmax1; }
            set { Avg_Qmax1 = value; }
        }
        public string StrAvg_Qmax2
        {
            get { return Avg_Qmax2; }
            set { Avg_Qmax2 = value; }
        }
        public string StrAvg_Qmax25
        {
            get { return Avg_Qmax25; }
            set { Avg_Qmax25 = value; }
        }

        public string StrAvg_Qmax4
        {
            get { return Avg_Qmax4; }
            set { Avg_Qmax4 = value; }
        }
        public string StrAvg_Qmax7
        {
            get { return Avg_Qmax7; }
            set { Avg_Qmax7 = value; }
        }
        public string StrAvg_Qmax
        {
            get { return Avg_Qmax; }
            set { Avg_Qmax = value; }
        }
        public string StrRepeat_Qmin
        {
            get { return Repeat_Qmin; }
            set { Repeat_Qmin = value; }
        }
        public string StrRepeat_Qmax1
        {
            get { return Repeat_Qmax1; }
            set { Repeat_Qmax1 = value; }
        }
        public string StrRepeat_Qmax2
        {
            get { return Repeat_Qmax2; }
            set { Repeat_Qmax2 = value; }
        }
        public string StrRepeat_Qmax25
        {
            get { return Repeat_Qmax25; }
            set { Repeat_Qmax25 = value; }
        }
        public string StrRepeat_Qmax4
        {
            get { return Repeat_Qmax4; }
            set { Repeat_Qmax4 = value; }
        }
        public string StrRepeat_Qmax7
        {
            get { return Repeat_Qmax7; }
            set { Repeat_Qmax7 = value; }
        }
        public string StrRepeat_Qmax
        {
            get { return Repeat_Qmax; }
            set { Repeat_Qmax = value; }
        }

        public string StrRatio
        {
            get { return Ratio; }
            set { Ratio = value; }
        }
        public string Strq1
        {
            get { return q1; }
            set { q1 = value; }
        }
        public string Strq2
        {
            get { return q2; }
            set { q2 = value; }
        }
        public string StrPDeviation
        {
            get { return PDeviation; }
            set { PDeviation = value; }
        }
        public string StrOratio
        {
            get { return Oratio; }
            set { Oratio = value; }
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
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

    }

    public class tk_CheckDataSearch
    {
        private string RID;
        
        public string RepairMethod;
        
        private string CheckUser;
        private DateTime? CheckDate;
       
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRepairMethod
        {
            get { return RepairMethod; }
            set { RepairMethod = value; }
        }
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrCheckUser
        {
            get { return CheckUser; }
            set { CheckUser = value; }
        }
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public DateTime? StrCheckDate
         {
             get { return CheckDate; }
             set { CheckDate = value; }
         }

    }
}
