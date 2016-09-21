using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public  class tk_ProductRDatail
    {
        private string strSGID = "";
        [DataFieldAttribute("SGID", "nvarchar")]

        public string SGID
        {
            get { return strSGID; }
            set { strSGID = value; }
        }

        private string strDID = "";
        [DataFieldAttribute("DID", "nvarchar")]

        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strProcess = "";
        [DataFieldAttribute("Process", "nvarchar")]

        public string Process
        {
            get { return strProcess; }
            set { strProcess = value; }
        }

        private string strteam = "";
        [DataFieldAttribute("team", "nvarchar")]

        public string team
        {
            get { return strteam; }
            set { strteam = value; }
        }

        private string strEstimatetime = "";
        [DataFieldAttribute("Estimatetime", "nvarchar")]

        public string Estimatetime
        {
            get { return strEstimatetime; }
            set { strEstimatetime = value; }
        }

        private string strperson = "";
        [DataFieldAttribute("person", "nvarchar")]

        public string person
        {
            get { return strperson; }
            set { strperson = value; }
        }

        private int strplannumber;
        [DataFieldAttribute("plannumber", "int")]

        public int plannumber
        {
            get { return strplannumber; }
            set { strplannumber = value; }
        }

        private int strQualified;
        [DataFieldAttribute("Qualified", "int")]

        public int Qualified
        {
            get { return strQualified; }
            set { strQualified = value; }
        }

        private int strnumber;
        [DataFieldAttribute("number", "int")]

        public int number
        {
            get { return strnumber; }
            set { strnumber = value; }
        }

        private int strnumbers ;
        [DataFieldAttribute("numbers", "int")]

        public int numbers
        {
            get { return strnumbers; }
            set { strnumbers = value; }
        }

        private int strFnubers;
        [DataFieldAttribute("Fnubers", "int")]

        public int Fnubers
        {
            get { return strFnubers; }
            set { strFnubers = value; }
        }

        private DateTime strfinishtime;
        [DataFieldAttribute("finishtime", "DateTime")]

        public DateTime finishtime
        {
            get { return strfinishtime; }
            set { strfinishtime = value; }
        }

        private string strpeople = "";
        [DataFieldAttribute("people", "nvarchar")]

        public string people
        {
            get { return strpeople; }
            set { strpeople = value; }
        }

        private string strreason = "";
        [DataFieldAttribute("reason", "nvarchar")]

        public string reason
        {
            get { return strreason; }
            set { strreason = value; }
        }

        private string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "nvarchar")]

        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
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

        private string strTechnical = "";
        [DataFieldAttribute("Technical", "nvarchar")]

        public string Technical
        {
            get { return strTechnical; }
            set { strTechnical = value; }
        }
    }
}
