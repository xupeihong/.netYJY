using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class tk_FileUpload
    {
        private string RID;
        private string Type;
        private string FileName;
        private string Comments;
        private string CreatePerson;
        private DateTime? CreateTime;
        private string Validate;

        public string StrRID
        {
            get { return RID; }
            set { RID = value; }
        }

        [Required(ErrorMessage = "上传环节不能为空")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        public string StrType
        {
            get { return Type; }
            set { Type = value; }
        }

        public string StrComments
        {
            get { return Comments; }
            set { Comments = value; }
        }
        
        public string StrFileName
        {
            get { return FileName; }
            set { FileName = value; }
        }
        
        public string StrCreatePerson
        {
            get { return CreatePerson; }
            set { CreatePerson = value; }
        }

        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }

    }
}
