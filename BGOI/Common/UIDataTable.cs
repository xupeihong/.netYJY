using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class UIDataTable
    {
        private int m_intTotalPages = 0;//总页数
        private int m_intRecords = 0;//查询出的记录数

        private System.Data.DataTable m_dtData = new System.Data.DataTable();

        public int IntTotalPages
        {
            get { return m_intTotalPages; }
            set { m_intTotalPages = value; }
        }

        public int IntRecords
        {
            get { return m_intRecords; }
            set { m_intRecords = value; }
        }

        public System.Data.DataTable DtData
        {
            get { return m_dtData; }
            set { m_dtData = value; }
        }
    }
}
