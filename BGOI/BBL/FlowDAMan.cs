using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class FlowDAMan
    {
        // 重复性和示值报告-查询
        public static DataTable GetRepeatValue(string where)
        {
            return FlowDAPro.GetRepeatValue(where);
        }

        // 检测对比图-加载检测表列表
        public static UIDataTable LoadDetecList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowDAPro.LoadDetecList(a_intPageSize, a_intPageIndex, where);
        }

        // 获取表格数据
        public static DataSet getMeterImg(string arr)
        {
            return FlowDAPro.getMeterImg(arr);
        }
        //按年分
        public static UIDataTable LoadYearAnaly(string where)
        {
            return FlowDAPro.LoadYearAnaly(where);
        }

        public static UIDataTable LoadCalibreAnaly(string where)
        {
            return FlowDAPro.LoadCalibreAnaly(where);
        }
        public static UIDataTable LoadRepalceAnaly(string where)
        {
            return FlowDAPro.LoadRepalceAnaly(where);
        }

        // 大客户数据分析-获取列表数据
        public static string LoadCustomerAnaly(ref string a_strErr)
        {
            DataTable dtCustomer = new DataTable();
            dtCustomer = FlowDAPro.LoadCustomerAnaly(ref a_strErr);

            if (dtCustomer == null) return "";
            if (dtCustomer.Rows.Count == 0) return "";

            string strCustomer = GFun.Dt2Json("Customer", dtCustomer);
            return strCustomer;
        }

        // 大客户数据分析-汇总
        public static string LoadCustomerTotal(ref string a_strErr)
        {
            DataTable dtCustomer = new DataTable();
            dtCustomer = FlowDAPro.LoadCustomerTotal(ref a_strErr);

            if (dtCustomer == null) return "";
            if (dtCustomer.Rows.Count == 0) return "";

            string strCustomer = GFun.Dt2Json("CustomerTotal", dtCustomer);
            return strCustomer;
        }

    }
}
