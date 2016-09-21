using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class SystemProjectMan
    {
        public static List<SelectListItem> GetNewConfig()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = SystemProjectPro.GetConfigContent();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["Type"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ss"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable getNewBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SystemProjectPro.getBasicGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewContent(string type, string text, ref string a_strErr)
        {
            if (SystemProjectPro.InsertContent(type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewContent(string xid, string type, string text, ref string a_strErr)
        {
            if (SystemProjectPro.UpdateContent(xid, type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewContent(string xid, string type, ref string a_strErr)
        {
            if (SystemProjectPro.DeleteContent(xid, type, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
    }
}
