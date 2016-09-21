using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class TestItem
    {
        private string strYYCode;
        private int intItemID;

        [DataFieldAttribute("YYCode", "varchar")]
        public string YYCode
        {
            get { return strYYCode; }
            set { strYYCode = value; }
        }
        [DataFieldAttribute("ItemID", "int")]
        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }
    }
}
