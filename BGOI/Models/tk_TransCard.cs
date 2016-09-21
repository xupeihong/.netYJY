using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_TransCard
    {
        private string TID;
        private string RID;
        private string FirstCheck;
        private string SendRepair;
        private string LastCheck;
        private string OneRepair;
        private string TwoCheck;
        private string TwoRepair;
        private string ThreeCheck;
        private string ThreeRepair;
        private string Comments;
        private string Validate;


        public tk_TransCard()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [Required(ErrorMessage = "内部编号不能为空")]
        [StringLength(20, ErrorMessage = "内部编号长度不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TID", "nvarchar")]
        public string strTID
        {
            get { return TID; }
            set { TID = value; }
        }

        [DataFieldAttribute("RID", "nvarchar")]
        public string strRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [DataFieldAttribute("FirstCheck", "nvarchar")]
        public string strFirstCheck
        {
            get { return FirstCheck; }
            set { FirstCheck = value; }
        }

        [DataFieldAttribute("SendRepair", "nvarchar")]
        public string strSendRepair
        {
            get { return SendRepair; }
            set { SendRepair = value; }
        }

        [DataFieldAttribute("LastCheck", "nvarchar")]
        public string strLastCheck
        {
            get { return LastCheck; }
            set { LastCheck = value; }
        }

        [DataFieldAttribute("OneRepair", "nvarchar")]
        public string strOneRepair
        {
            get { return OneRepair; }
            set { OneRepair = value; }
        }

        [DataFieldAttribute("TwoCheck", "nvarchar")]
        public string strTwoCheck
        {
            get { return TwoCheck; }
            set { TwoCheck = value; }
        }

        [DataFieldAttribute("TwoRepair", "nvarchar")]
        public string strTwoRepair
        {
            get { return TwoRepair; }
            set { TwoRepair = value; }
        }

        [DataFieldAttribute("ThreeCheck", "nvarchar")]
        public string strThreeCheck
        {
            get { return ThreeCheck; }
            set { ThreeCheck = value; }
        }

        [DataFieldAttribute("ThreeRepair", "nvarchar")]
        public string strThreeRepair
        {
            get { return ThreeRepair; }
            set { ThreeRepair = value; }
        }

        [DataFieldAttribute("Comments", "nvarchar")]
        public string strComments
        {
            get { return Comments; }
            set { Comments = value; }
        }

        [DataFieldAttribute("Validate", "nvarchar")]
        public string strValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }



    }
}