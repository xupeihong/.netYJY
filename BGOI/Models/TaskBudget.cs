using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class TaskBudget
    {
        private string BudgetID;
        private string TaskID;
        private string PostTime;
        private decimal? ContractPrice;
        private decimal? AdvancePrice;
        private string AppTime;
        private decimal? ProPrice;
        private string ProTime;
        private string Comments;

        [DataFieldAttribute("BudgetID", "varchar")]
        public string StrBudgetID
        {
            get { return BudgetID; }
            set { BudgetID = value; }
        }

        [DataFieldAttribute("TaskID", "varchar")]
        public string StrTaskID
        {
            get { return TaskID; }
            set { TaskID = value; }
        }

        [DataFieldAttribute("PostTime", "nvarchar")]
        public string StrPostTime
        {
            get { return PostTime; }
            set { PostTime = value; }
        }

        [DataFieldAttribute("ContractPrice ", "decimal")]
        public decimal? StrContractPrice
        {
            get { return ContractPrice; }
            set { ContractPrice = value; }
        }

        [DataFieldAttribute("AdvancePrice ", "decimal")]
        public decimal? StrAdvancePrice
        {
            get { return AdvancePrice; }
            set { AdvancePrice = value; }
        }

        [DataFieldAttribute("AppTime", "nvarchar")]
        public string StrAppTime
        {
            get { return AppTime; }
            set { AppTime = value; }
        }

        [DataFieldAttribute("ProPrice", "decimal")]
        public decimal? StrProPrice
        {
            get { return ProPrice; }
            set { ProPrice = value; }
        }

        [DataFieldAttribute("Comments", "text")]
        public string StrComments
        {
            get { return Comments; }
            set { Comments = value; }
        }

        [DataFieldAttribute("ProTime", "varchar")]
        public string StrProTime
        {
            get { return ProTime; }
            set { ProTime = value; }
        }
    }
}
