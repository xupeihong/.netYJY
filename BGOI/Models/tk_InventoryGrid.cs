using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TECOCITY_BGOI
{
   public  class tk_InventoryGrid
    {
        private string m_FinishAount = "";//库存数量

        public string FinishAount
        {
            get { return m_FinishAount; }
            set { m_FinishAount = value; }
        }
        private string m_ProductID = "";//物料编码

        public string ProductID
        {
            get { return m_ProductID; }
            set { m_ProductID = value; }
        }
        private string m_Spec = "";//规则型号

        public string Spec
        {
            get { return m_Spec; }
            set { m_Spec = value; }
        }
        private string m_HouserName = "";

        public string HouserName
        {
            get { return m_HouserName; }
            set { m_HouserName = value; }
        }
       //private string m_

    }
}
