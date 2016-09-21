using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class DScrapInfo
    {
        private string ECode;
        private string ScrapTime;
        private string ScrapResults;
        private string Remark;
        private DateTime? CreateTime;
        private string CreateUser;

        [DataFieldAttribute("ECode", "varchar")]
        public string StrECode
        {
            get { return ECode; }
            set { ECode = value; }
        }

        [DataFieldAttribute("ScrapTime", "date")]
        public string StrScrapTime
        {
            get { return ScrapTime; }
            set { ScrapTime = value; }
        }

        [DataFieldAttribute("ScrapResults", "nvarchar")]
        public string StrScrapResults
        {
            get { return ScrapResults; }
            set { ScrapResults = value; }
        }

        [DataFieldAttribute("Remark", "nvarchar")]
        public string StrRemark
        {
            get { return Remark; }
            set { Remark = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string StrCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }
    }
}
