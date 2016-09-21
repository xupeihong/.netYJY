using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_Quotation
    {
        private string RID;
        private int QID;
        private string Type;
        private string RepairContent;
        private decimal UnitPrice;
        private decimal ConcesioPrice;
       

         public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }

         public int StrQID
         {
             get { return QID; }
             set { QID = value; }
         }
         [Required(ErrorMessage = "不能为空")]
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public string StrType
         {
             get { return Type; }
             set { Type = value; }
         }
        
         public string StrRepairContent
         {
             get { return RepairContent; }
             set { RepairContent = value; }
         }
         [Required(ErrorMessage = "不能为空")]
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public decimal StrUnitPrice
         {
             get { return UnitPrice; }
             set { UnitPrice = value; }
         }
         [Required(ErrorMessage = "不能为空")]
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public decimal StrConcesioPrice
         {
             get { return ConcesioPrice; }
             set { ConcesioPrice = value; }
         }
    }


    public class tk_QuotationSearch
    {
        private string RID;
        private int QID;
        private string Type;
        private string RepairContent;
        private decimal UnitPrice;
        private decimal ConcesioPrice;

         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }

        public int StrQID
        {
            get { return QID; }
            set { QID = value; }
        }
       
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrType
        {
            get { return Type; }
            set { Type = value; }
        }

        public string StrRepairContent
        {
            get { return RepairContent; }
            set { RepairContent = value; }
        }
      
        public decimal StrUnitPrice
        {
            get { return UnitPrice; }
            set { UnitPrice = value; }
        }
        
        public decimal StrConcesioPrice
        {
            get { return ConcesioPrice; }
            set { ConcesioPrice = value; }
        }
    }
}
