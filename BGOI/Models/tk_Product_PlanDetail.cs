using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_Product_PlanDetail
    {
        private string strJHID = "";
        [DataFieldAttribute("JHID", "nvarchar")]

        public string JHID
        {
            get { return strJHID; }
            set { strJHID = value; }
        }

        private string strDID = "";
        [DataFieldAttribute("DID", "nvarchar")]

        public string DID
        {
            get { return strDID; }
            set { strDID = value; }
        }

        private string strPID = "";
        [DataFieldAttribute("PID", "nvarchar")]

        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private string strName = "";
        [DataFieldAttribute("Name", "nvarchar")]

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strSpecifications = "";
        [DataFieldAttribute("Specifications", "nvarchar")]

        public string Specifications
        {
            get { return strSpecifications; }
            set { strSpecifications = value; }
        }

        private int strFinishedproduct;
        [DataFieldAttribute("Finishedproduct", "int")]

        public int Finishedproduct
        {
            get { return strFinishedproduct; }
            set { strFinishedproduct = value; }
        }

        private int strfinishingproduct;
        [DataFieldAttribute("finishingproduct", "int")]

        public int finishingproduct
        {
            get { return strfinishingproduct; }
            set { strfinishingproduct = value; }
        }

        private int strSpareparts;
        [DataFieldAttribute("Spareparts", "int")]

        public int Spareparts
        {
            get { return strSpareparts; }
            set { strSpareparts = value; }
        }

        private int intOnlineCount;
        [DataFieldAttribute("OnlineCount","int")]
        public int OnlineCount
        {
            get { return intOnlineCount; }
            set { intOnlineCount = value; }
        }

        private int strnotavailable;
        [DataFieldAttribute("notavailable", "int")]

        public int notavailable
        {
            get { return strnotavailable; }
            set { strnotavailable = value; }
        }

        private int strTotal;
        [DataFieldAttribute("Total", "int")]

        public int Total
        {
            get { return strTotal; }
            set { strTotal = value; }
        }

        private int strplannumber ;
        [DataFieldAttribute("plannumber", "int")]

        public int plannumber
        {
            get { return strplannumber; }
            set { strplannumber = value; }
        }

        private int strdemandnumber ;
        [DataFieldAttribute("demandnumber", "int")]

        public int demandnumber
        {
            get { return strdemandnumber; }
            set { strdemandnumber = value; }
        }

        private string strRemarks = "";
        [DataFieldAttribute("Remarks", "nvarchar")]

        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
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
    }
}
