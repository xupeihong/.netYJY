using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ProjectUserLog
    {

        private string RelevanceID;
        private string LogTitle;
        private string LogContent;
        private DateTime? LogTime;
        private string LogPerson;
        private string Type;


        public ProjectUserLog()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        [DataFieldAttribute("RelevanceID", "varchar")]
        public string strRelevanceID
        {
            get { return RelevanceID; }
            set { RelevanceID = value; }
        }

        [DataFieldAttribute("LogTitle", "nvarchar")]
        public string strLogTitle
        {
            get { return LogTitle; }
            set { LogTitle = value; }
        }

        [DataFieldAttribute("LogContent", "nvarchar")]
        public string strLogContent
        {
            get { return LogContent; }
            set { LogContent = value; }
        }

        [DataFieldAttribute("LogTime", "datetime")]
        public DateTime? strLogTime
        {
            get { return LogTime; }
            set { LogTime = value; }
        }

        [DataFieldAttribute("LogPerson", "varchar")]
        public string strLogPerson
        {
            get { return LogPerson; }
            set { LogPerson = value; }
        }

        [DataFieldAttribute("Type", "varchar")]
        public string strType
        {
            get { return Type; }
            set { Type = value; }
        }
    }
}
