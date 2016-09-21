using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TECOCITY_BGOI
{
    public class tk_CheckData2
    {
        private string RID;
        private string RepairID;
        private string RepairType;
        public string RepairMethod;
        private string MeterID;
        private string CheckUser;
        private DateTime? CheckDate;
        private string A1path;
        private string A2path;
        private string A3path;
        private string A4path;
        private string A5path;
        private string A6path;
        private string ATemperature;//温度
        private string APressure;//压力
        private string AAverage;//平均
        private string AAheory;//理论
        private string A1Error;
        //private string A2Error;
        //private string A3Error;
        //private string A4Error;
        //private string A5Error;
        //private string A6Error;
        //private string AAverageError;
        //private string AAheoryError;
        private string B1path;
        private string B2path;
        private string B3path;
        private string B4path;
        private string B5path;
        private string B6path;
        private string BTemperature;
        private string BPressure;
        private string BAverage;
        private string BAheory;


        private string B1Error;
        private string B2Error;
        private string B3Error;
        private string B4Error;
        private string B5Error;
        private string B6Error;
        private string BAverageError;
        private string BAheoryError;
        private string CreateUser;
        private DateTime? CreateTime;
        private string validate;
     
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

        public string StrA1path
        {
            get { return A1path; }
            set { A1path = value; }
        }
        public string StrA2path
        {
            get { return A2path; }
            set { A2path = value; }
        }
        public string StrA3path
        {
            get { return A3path; }
            set { A3path = value; }
        }
        public string StrA4path
        {
            get { return A4path; }
            set { A4path = value; }
        }
        public string StrA5path
        {
            get { return A5path; }
            set { A5path = value; }
        }
        public string StrA6path
        {
            get { return A6path; }
            set { A6path = value; }
        }
        public string StrATemperature
        {
            get { return ATemperature; }
            set { ATemperature = value; }
        }
        public string StrAPressure
        {
            get { return APressure; }
            set { APressure = value; }
        }
        public string StrAAverage
        {
            get { return AAverage; }
            set { AAverage = value; }
        }
        public string StrAAheory
        {
            get { return AAheory; }
            set { AAheory = value; }
        }
        //public string StrA1Error
        //{
        //    get { return A1Error; }
        //    set { A1Error = value; }
        //}

        //public string StrA2Error
        //{
        //    get { return A2Error; }
        //    set { A2Error = value; }
        //}
        //public string StrA3Error
        //{
        //    get { return A3Error; }
        //    set { A3Error = value; }
        //}
        //public string StrA4Error
        //{
        //    get { return A4Error; }
        //    set { A4Error = value; }
        //}
        //public string StrA5Error
        //{
        //    get { return A5Error; }
        //    set { A5Error = value; }
        //}
        //public string StrA6Error
        //{
        //    get { return A6Error; }
        //    set { A6Error = value; }
        //}
        //public string StrAAverageError
        //{
        //    get { return AAverageError; }
        //    set { AAverageError = value; }
        //}
        //public string StrAAheoryError
        //{
        //    get { return AAheoryError; }
        //    set { AAheoryError = value; }
        //}
  

        public string StrB1path
        {
            get { return B1path; }
            set { B1path = value; }
        }
        public string StrB2path
        {
            get { return B2path; }
            set { B2path = value; }
        }
        public string StrB3path
        {
            get { return B3path; }
            set { B3path = value; }
        }
        public string StrB4path
        {
            get { return B4path; }
            set { B4path = value; }
        }
        public string StrB5path
        {
            get { return B5path; }
            set { B5path = value; }
        }
        public string StrB6path
        {
            get { return B6path; }
            set { B6path = value; }
        }
        public string StrBTemperature
        {
            get { return BTemperature; }
            set { BTemperature = value; }
        }
        public string StrBPressure
        {
            get { return BPressure; }
            set { BPressure = value; }
        }
        public string StrBAverage
        {
            get { return BAverage; }
            set { BAverage = value; }
        }
        public string StrBAheory
        {
            get { return BAheory; }
            set { BAheory = value; }
        }
        public string StrB1Error
        {
            get { return B1Error; }
            set { B1Error = value; }
        }

        public string StrB2Error
        {
            get { return B2Error; }
            set { B2Error = value; }
        }
        public string StrB3Error
        {
            get { return B3Error; }
            set { B3Error = value; }
        }
        public string StrB4Error
        {
            get { return B4Error; }
            set { B4Error = value; }
        }
        public string StrB5Error
        {
            get { return B5Error; }
            set { B5Error = value; }
        }
        public string StrB6Error
        {
            get { return B6Error; }
            set { B6Error = value; }
        }
        public string StrBAverageError
        {
            get { return BAverageError; }
            set { BAverageError = value; }
        }
        public string StrBAheoryError
        {
            get { return BAheoryError; }
            set { BAheoryError = value; }
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
        public string Strvalidate
        {
            get { return validate; }
            set { validate = value; }
        }
    }

}
