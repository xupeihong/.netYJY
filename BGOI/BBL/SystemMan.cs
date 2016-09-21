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
    public class SystemMan
    {
        public static List<SelectListItem> GetNewAppContent(string data,string table)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = SystemPro.GetAppContent(data, table);
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
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetNewConfigCont(string Type)
        {
            return SystemPro.GetConfigCont(Type);
        }

        public static DataTable getNewAppType(string Type)
        {
            return SystemPro.getAppType(Type);
        }

        public static DataTable GetNewUser()
        {
            return SystemPro.GetUser();
        }

        public static UIDataTable getNewUserGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            return SystemPro.getUserGrid(a_intPageSize, a_intPageIndex, where, where2);
        }

        public static bool InsertNewExamine(string Butype, string allcontent, ref string a_strErr)
        {
            if (SystemPro.InsertExamine(Butype,allcontent, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SystemPro.getExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static string getNewHaveExaminContent(string BuType)
        {
            return SystemPro.getHaveExaminContent(BuType);
        }
    }
}
