using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHComplain_User
    {
        //, , , , 
        public string strTSID = "";
        [DataFieldAttribute("TSID", "varchar")]
        //投诉编号
        public string TSID
        {
            get { return strTSID; }
            set { strTSID = value; }
        }
        public string strDID = "";
        [DataFieldAttribute("DID", "varchar")]
        //内容编号
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }
        public string strUserID = "";
        [DataFieldAttribute("UserID", "varchar")]
        //人员ID
        public string UserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }
        public string strUserUnitID = "";
        [DataFieldAttribute("UserUnitID", "varchar")]
        //人员所属单位
        public string UserUnitID
        {
            get { return strUserUnitID; }
            set { strUserUnitID = value; }
        }
        public string strUserName = "";
        [DataFieldAttribute("UserName", "varchar")]
        //人员姓名
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }


    }
}
