using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_FileInfo
    {
        private string strBGID = "";
        [DataFieldAttribute("BGID", "nvarchar")]
        public string BGID
        {
            get { return strBGID; }
            set { strBGID = value; }
        }

        private string strDID = "";
        [DataFieldAttribute("DID", "nvarchar")]
        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strType = "";
        [DataFieldAttribute("Type", "nvarchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }

        private string strFileName = "";
        [DataFieldAttribute("FileName", "nvarchar")]
        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        private string strFileType = "";
        [DataFieldAttribute("FileType", "nvarchar")]
        public string FileType
        {
            get { return strFileType; }
            set { strFileType = value; }
        }

        private byte[] strFileInfo;
        [DataFieldAttribute("FileInfo", "varbinary")]
        public byte[] FileInfo
        {
            get { return strFileInfo; }
            set { strFileInfo = value; }
        }

        private string strCreatePerson = "";
        [DataFieldAttribute("CreatePerson", "nvarchar")]
        public string CreatePerson
        {
            get { return strCreatePerson; }
            set { strCreatePerson = value; }
        }

        private DateTime strCreateTime ;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strValidate = "";
        [DataFieldAttribute("Validate", "nvarchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private DateTime strCancelTime;
        [DataFieldAttribute("CancelTime", "DateTime")]
        public DateTime CancelTime
        {
            get { return strCancelTime; }
            set { strCancelTime = value; }
        }

        private string strCancelReason = "";
        [DataFieldAttribute("CancelReason", "nvarchar")]
        public string CancelReason
        {
            get { return strCancelReason; }
            set { strCancelReason = value; }
        }
    }
}
