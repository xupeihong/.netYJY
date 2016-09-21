using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Sales_RecordDetail
    {
        private string strPID = "";

        [DataFieldAttribute("PID", "varchar")]
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }

        private int IntID = 0;

        [DataFieldAttribute("ID", "int")]
        public int ID
        {
            get { return IntID; }
            set { IntID = value; }
        }

        private string strMainContent = "";

        [DataFieldAttribute("MainContent", "nvarchar")]
        public string MainContent
        {
            get { return strMainContent; }
            set { strMainContent = value; }
        }

        private string m_strDeviceName = "";

        [DataFieldAttribute("DeviceName", "nvarchar")]
        public string DeviceName
        {
            get { return m_strDeviceName; }
            set { m_strDeviceName = value; }
        }

        private string strSpecsModels = "";

        [DataFieldAttribute("SpecsModels", "nvarchar")]
        public string SpecsModels
        {
            get { return strSpecsModels; }
            set { strSpecsModels = value; }
        }

        private int IntSalesNum = 0;

        [DataFieldAttribute("SalesNum", "int")]
        public int SalesNum
        {
            get { return IntSalesNum; }
            set { IntSalesNum = value; }
        }

        private string strWorkChief = "";

        [DataFieldAttribute("WorkChief", "nvarchar")]
        public string WorkChief
        {
            get { return strWorkChief; }
            set { strWorkChief = value; }
        }

        private string strConstructor = "";

        [DataFieldAttribute("Constructor", "nvarchar")]
        public string Constructor
        {
            get { return strConstructor; }
            set { strConstructor = value; }
        }

        private string strTel = "";

        [DataFieldAttribute("Tel", "varchar")]
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }

        private string strBelongArea = "";

        [DataFieldAttribute("BelongArea", "nvarchar")]
        public string BelongArea
        {
            get { return strBelongArea; }
            set { strBelongArea = value; }
        }

        private DateTime dtOrderTime;

        [DataFieldAttribute("OrderTime", "datetime")]
        public DateTime OrderTime
        {
            get { return dtOrderTime; }
            set { dtOrderTime = value; }
        }

        private string strChannelsFrom = "";

        [DataFieldAttribute("ChannelsFrom", "nvarchar")]
        public string ChannelsFrom
        {
            get { return strChannelsFrom; }
            set { strChannelsFrom = value; }
        }
    }
}
