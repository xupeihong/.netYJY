using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_pp_Log
    {
 


        private string strRelevanceID;
        [DataFieldAttribute("RelevanceID", "varchar")]
        public string RelevanceID
        {
            get { return strRelevanceID; }
            set { strRelevanceID = value; }
        }


        private string strLogTitle;
        [DataFieldAttribute("LogTitle", "varchar")]
        public string LogTitle
        {
            get { return strLogTitle; }
            set { strLogTitle = value; }
        }



        private string strLogContent;
        [DataFieldAttribute("LogContent", "varchar")]
        public string LogContent
        {
            get { return strLogContent; }
            set { strLogContent = value; }
        }



        private string strLogTime;
        [DataFieldAttribute("LogTime", "varchar")]
        public string LogTime
        {
            get { return strLogTime; }
            set { strLogTime = value; }
        }



        private string strLogPerson;
        [DataFieldAttribute("LogPerson", "varchar")]
        public string LogPerson
        {
            get { return strLogPerson; }
            set { strLogPerson = value; }
        }


        private string strType;
        [DataFieldAttribute("Type", "varchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }
    }
}
