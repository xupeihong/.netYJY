using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_CustomerServicelog
    {
        public string strLogTitle = "";
        [DataFieldAttribute("LogTitle", "varchar")]
        //日志标题
        public string LogTitle
        {
            get { return strLogTitle; }
            set { strLogTitle = value; }
        }

        public string strLogContent = "";
        [DataFieldAttribute("LogContent", "varchar")]
        //日志内容
        public string LogContent
        {
            get { return strLogContent; }
            set { strLogContent = value; }
        }

        public DateTime strTime;
        [DataFieldAttribute("Time", "varchar")]
        //创建时间
        public DateTime Tiem
        {
            get { return strTime; }
            set { strTime = value; }
        }

        public string strPerson = "";
        [DataFieldAttribute("Person", "varchar")]
        //创建人
        public string Person
        {
            get { return strPerson; }
            set { strPerson = value; }
        }

        public string strType = "";
        [DataFieldAttribute("Type", "varchar")]
        //操作的表
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }

        public string strTypeid = "";
        [DataFieldAttribute("Typeid", "varchar")]
        //操作的表的唯一id
        public string Typeid
        {
            get { return strTypeid; }
            set { strTypeid = value; }
        }
    }
}
