using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
  public   class PP_File
    {
      private string strID;
        [DataFieldAttribute("ID", "varchar")]
      public string ID
        {
            get { return strID; }
            set { strID = value; }
        }

        private string strPID;
        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strFileInfo;
        [DataFieldAttribute("FileInfo", "varchar")]
        public string FileInfo
        {
            get { return strFileInfo; }
            set { strFileInfo = value; }
        }

        private string strFileName;
        [DataFieldAttribute("FileName", "varchar")]
        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }
        private string strCreateTime;
        [DataFieldAttribute("CreateTime", "varchar")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
       
    }
}
