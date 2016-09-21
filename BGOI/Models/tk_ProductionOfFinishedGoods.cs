using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductionOfFinishedGoods
    {
        //PID, , , CreateTime, Validate
        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        //货品唯一编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strUnitID = "";
        [DataFieldAttribute("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
