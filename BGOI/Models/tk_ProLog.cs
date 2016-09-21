using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public  class tk_ProLog
    {
        //private string StrID;
        //[DataFieldAttribute("ID", "nvarchar")]
        //public string ID
        //{
        //    get { return StrID; }
        //    set { StrID = value; }
        //}

        private DateTime StrLogTime;
        [DataFieldAttribute("LogTime", "DateTime")]
        public DateTime LogTime
        {
            get { return StrLogTime; }
            set { StrLogTime = value; }
        }

        private string StrYYCode;
        [DataFieldAttribute("YYCode", "nvarchar")]
        public string YYCode
        {
            get { return StrYYCode; }
            set { StrYYCode = value; }
        }

        private string StrYYType;
        [DataFieldAttribute("YYType", "nvarchar")]
        public string YYType
        {
            get { return StrYYType; }
            set { StrYYType = value; }
        }

        private string StrContent;
        [DataFieldAttribute("Content", "nvarchar")]
        public string Content
        {
            get { return StrContent; }
            set { StrContent = value; }
        }

        private string StrTarget;
        [DataFieldAttribute("Target", "nvarchar")]
        public string Target
        {
            get { return StrTarget; }
            set { StrTarget = value; }
        }

        private string StrActor;
        [DataFieldAttribute("Actor", "nvarchar")]
        public string Actor
        {
            get { return StrActor; }
            set { StrActor = value; }
        }

        private string StrUnit;
        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return StrUnit; }
            set { StrUnit = value; }
        }

    }
}
