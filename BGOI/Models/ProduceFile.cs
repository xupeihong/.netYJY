using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ProduceFile
    {
        private string strOID;
        [DataFieldAttribute("OID", "varchar")]
        public string OID
        {
            get { return strOID; }
            set { strOID = value; }
        }
        private string strFileName;
        [DataFieldAttribute("FileName", "varchar")]
        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        private string strFilePath;
        [DataFieldAttribute("FilePath", "varchar")]
        public string FilePath
        {
            get { return strFilePath; }
            set { strFilePath = value; }
        }

        private string strCreateTime;

        [DataFieldAttribute("CreateTime", "datetime")]
        public string CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        private string strCreateUser;

        [DataFieldAttribute("CreateUser", "nvarchar")]
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
