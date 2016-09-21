using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_Log
    {
        private decimal intID;
        private DateTime strLogTime;
        private string strYYCode;
        private string strContent;
        private string strTarget;
        private string strActor;
        private string strUnit;



        [DataFieldAttribute("ID", "decimal")]
        public decimal ID
        {
            get { return intID; }
            set { intID = value; }
        }

        /// <summary>
        /// 日志时间
        /// </summary>
        [DataFieldAttribute("LogTime", "datetime")]
        public DateTime LogTime
        {
            get { return strLogTime; }
            set { strLogTime = value; }
        }
        /// <summary>
        /// 预约号
        /// </summary>
        [DataFieldAttribute("YYCode", "nvarchar")]
        public string YYCode
        {
            get { return strYYCode; }
            set { strYYCode = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        [DataFieldAttribute("Content", "nvarchar")]
        public string Content
        {
            get { return strContent; }
            set { strContent = value; }
        }

        [DataFieldAttribute("Target", "nvarchar")]
        public string Target
        {
            get { return strTarget; }
            set { strTarget = value; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        [DataFieldAttribute("Actor", "nvarchar")]
        public string Actor
        {
            get { return strActor; }
            set { strActor = value; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        [DataFieldAttribute("Unit", "nvarchar")]
        public string Unit
        {
            get { return strUnit; }
            set { strUnit = value; }
        }
    }
}
