using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
 public    class tk_CFile
    {
        private string str_CID;
          [DataFieldAttribute("CID", "nvarchar")]
        public string CID
        {
            get { return str_CID; }
            set { str_CID = value; }
        }

        private string str_FileType;
         [DataFieldAttribute("FileType", "nvarchar")]
        public string FileType
        {
            get { return str_FileType; }
            set { str_FileType = value; }
        }
        private string str_FileName;
         [DataFieldAttribute("FileName", "nvarchar")]
        public string FileName
        {
            get { return str_FileName; }
            set { str_FileName = value; }
        }
        private string str_FileInfo;
         [DataFieldAttribute("FileInfo", "nvarchar")]
        public string FileInfo
        {
            get { return str_FileInfo; }
            set { str_FileInfo = value; }
        }
   
        private string str_CreateUser;
      [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser
        {
            get { return str_CreateUser; }
            set { str_CreateUser = value; }
        }
        private string str_CreateTime;
      [DataFieldAttribute("CreateTime", "datetime")]
        public string CreateTime
        {
            get { return str_CreateTime; }
            set { str_CreateTime = value; }
        }
        private string str_Validate;
      [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return str_Validate; }
            set { str_Validate = value; }
        }

   //  FileInfo ,FileName ,CreateTime ,CreateUser,FileType ,Validate
    
    }
}
