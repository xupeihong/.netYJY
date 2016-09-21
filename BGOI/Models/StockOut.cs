using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StockOut
    {
        private string strListOutID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListOutID", "varchar")]
        public string ListOutID
        {
            get { return strListOutID; }
            set { strListOutID = value; }
        }

        private string strSubjectID = "";
        [DataFieldAttribute("SubjectID", "varchar")]
        public string SubjectID
        {
            get { return strSubjectID; }
            set { strSubjectID = value; }
        }

        private DateTime strProOutTime;
        [DataFieldAttribute("ProOutTime", "datetime")]
        public DateTime ProOutTime
        {
            get { return strProOutTime; }
            set { strProOutTime = value; }
        }

        private string strProOutUser = "";
        [DataFieldAttribute("ProOutUser", "nvarchar")]
        public string ProOutUser
        {
            get { return strProOutUser; }
            set { strProOutUser = value; }
        }

        private string strHouseID = "";
        [DataFieldAttribute("HouseID", "varchar")]
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }


        private int strState;
        [DataFieldAttribute("State", "varchar")]
        public int State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strValiDate = "";
        [DataFieldAttribute("ValiDate", "varchar")]
        public string ValiDate
        {
            get { return strValiDate; }
            set { strValiDate = value; }
        }

        private decimal strAmountM;
        [DataFieldAttribute("Amount", "varchar")]
        public decimal AmountM
        {
            get { return strAmountM; }
            set { strAmountM = value; }
        }

        private string strUse = "";
        [DataFieldAttribute("[Use]", "varchar")]
        public string Use
        {
            get { return strUse; }
            set { strUse = value; }
        }

        private string strType = "";
        [DataFieldAttribute("Type", "varchar")]
        public string Type
        {
            get { return strType; }
            set { strType = value; }
        }
        public string strRemark = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strPurchase = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Purchase", "nvarchar")]
        public string Purchase
        {
            get { return strPurchase; }
            set { strPurchase = value; }
        }


        private string strUnitID = "";
        // [Remote("CheckUserAccountExists", "SuppliesManage", ErrorMessage = "该供应商编号已存在")] 添加用
        //[StringLength(20, ErrorMessage = "回访编号不能超过20个字符")]
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("UnitID", "varchar")]
        public string UnitID
        {
            get { return strUnitID; }
            set { strUnitID = value; }
        }
    }
}
