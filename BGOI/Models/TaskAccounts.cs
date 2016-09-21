using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class TaskAccounts
    {
        private string AccountsID;
        private string TaskID;
        private string AccAmount;
        private string PostTime;
        private decimal? ContractPrice;
        private string SignTime;
        private decimal? ActualPrice;
        private string ActualTime;
        private string KnotStyle;
        private string Comments;
        private string IsSign;
        private decimal? RePairPrice;
        private string IsBill;

        [DataFieldAttribute("AccountsID", "varchar")]
        public string StrAccountsID
        {
            get { return AccountsID; }
            set { AccountsID = value; }
        }

        [DataFieldAttribute("TaskID", "varchar")]
        public string StrTaskID
        {
            get { return TaskID; }
            set { TaskID = value; }
        }

        [DataFieldAttribute("AccAmount", "varchar")]
        public string StrAccAmount
        {
            get { return AccAmount; }
            set { AccAmount = value; }
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

        [DataFieldAttribute("SignTime", "nvarchar")]
        public string StrSignTime
        {
            get { return SignTime; }
            set { SignTime = value; }
        }

        [DataFieldAttribute("ActualPrice", "decimal")]
        public decimal? StrActualPrice
        {
            get { return ActualPrice; }
            set { ActualPrice = value; }
        }

        [DataFieldAttribute("ActualTime", "nvarchar")]
        public string StrActualTime
        {
            get { return ActualTime; }
            set { ActualTime = value; }
        }

        [DataFieldAttribute("KnotStyle", "varchar")]
        public string StrKnotStyle
        {
            get { return KnotStyle; }
            set { KnotStyle = value; }
        }

        [DataFieldAttribute("Comments", "text")]
        public string StrComments
        {
            get { return Comments; }
            set { Comments = value; }
        }

        [DataFieldAttribute("IsSign", "varchar")]
        public string StrIsSign
        {
            get { return IsSign; }
            set { IsSign = value; }
        }

        [DataFieldAttribute("RePairPrice", "decimal")]
        public decimal? StrRePairPrice
        {
            get { return RePairPrice; }
            set { RePairPrice = value; }
        }

        [DataFieldAttribute("IsBill", "varchar")]
        public string StrIsBill
        {
            get { return IsBill; }
            set { IsBill = value; }
        }
    }
}
