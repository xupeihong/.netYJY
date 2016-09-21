using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_ProRecord
    {

       
        private string RID;
        private string OpType;
        private DateTime? OpTime;
        private string OpUser;
        
        private DateTime? CreateTime;
        private string CreateUser;
        private string Validate;


     
        public string StrID
        {
            get { return RID; }
            set { RID = value; }
        }

        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }
       
        public string StrOpType
        {
            get { return OpType; }
            set { OpType = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public DateTime? StrOpTime 
        {
            get { return OpTime; }
            set { OpTime = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public string StrOpUser
        {
            get { return OpUser; }
            set { OpUser = value; }
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
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
