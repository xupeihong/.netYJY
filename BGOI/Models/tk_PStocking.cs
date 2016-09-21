using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_PStocking
    {
        private string strRKID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RKID", "nvarchar")]

        public string RKID
        {
            get { return strRKID; }
            set { strRKID = value; }
        }

        private string strRWID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("RWID", "nvarchar")]

        public string RWID
        {
            get { return strRWID; }
            set { strRWID = value; }
        }

        private DateTime strStockInTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockInTime", "DateTime")]

        public DateTime StockInTime
        {
            get { return strStockInTime; }
            set { strStockInTime = value; }
        }

        private string strHouseID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("HouseID", "nvarchar")]

        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }

        private string strStockRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockRemark", "nvarchar")]

        public string StockRemark
        {
            get { return strStockRemark; }
            set { strStockRemark = value; }
        }

        private string strStockInUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StockInUser", "nvarchar")]

        public string StockInUser
        {
            get { return strStockInUser; }
            set { strStockInUser = value; }
        }

        private string strType = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Type", "nvarchar")]

        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }

        private string strUnitID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "nvarchar")]

        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }

        private string strState = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("State", "nvarchar")]

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private DateTime strCreateTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateTime", "DateTime")]

        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }

        private string strCreateUser = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("CreateUser", "nvarchar")]

        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }

        private string strValidate = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Validate", "nvarchar")]

        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }

        private DateTime strFinishTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("FinishTime", "DateTime")]

        public DateTime FinishTime
        {
            get { return strFinishTime; }
            set { strFinishTime = value; }
        }

        private string strMaterState;
        [DataFieldAttribute("MaterState", "string")]

        public string MaterState
        {
            get { return strMaterState; }
            set { strMaterState = value; }
        }

        private string strBatch;
        [DataFieldAttribute("Batch", "string")]

        public string Batch
        {
            get { return strBatch; }
            set { strBatch = value; }
        }

        private string strStorekeeper;
        [DataFieldAttribute("Storekeeper", "nvarchar")]

        public string Storekeeper
        {
            get { return strStorekeeper; }
            set { strStorekeeper = value; }
        }
    }
}
