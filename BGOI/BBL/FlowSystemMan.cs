using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class FlowSystemMan
    {
        // 
        public static List<SelectListItem> GetBasicContent()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowSystemPro.GetBasicContent();
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

        public static UIDataTable getBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowSystemPro.getBasicGrid(a_intPageSize, a_intPageIndex, where);
        }

        // 确认新增
        public static bool InsertBasic(string type, string text, ref string a_strErr)
        {
            if (FlowSystemPro.InsertBasic(type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认修改 
        public static bool ModifyBasic(string XID, string Type, string Text, ref string a_strErr)
        {
            if (FlowSystemPro.ModifyBasic(XID, Type, Text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 删除
        public static bool DeleteBasic(string xid, string type, ref string a_strErr)
        {
            if (FlowSystemPro.DeleteBasic(xid, type, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }



        // 确认新增小组
        public static bool InsertGroup(string text, ref string a_strErr)
        {
            if (FlowSystemPro.InsertGroup(text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        // 删除小组
        public static bool DeleteGroup(string gid, ref string a_strErr)
        {
            if (FlowSystemPro.DeleteGroup(gid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认修改 
        public static bool ModifyGroup(string GroupID, string Text, ref string a_strErr)
        {
            if (FlowSystemPro.ModifyGroup(GroupID, Text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认修改 小组人员
        public static bool ModifyGroupUser(string UserID, string Text, string GroupID, ref string a_strErr)
        {
            if (FlowSystemPro.ModifyGroupUser(UserID, Text, GroupID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 删除小组人员
        public static bool DeleteGroupUser(string uid, string gid, ref string a_strErr)
        {
            if (FlowSystemPro.DeleteGroupUser(uid, gid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认新增小组人员
        public static bool InsertGroupUser(string text, string GroupID, ref string a_strErr)
        {
            if (FlowSystemPro.InsertGroupUser(text, GroupID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }



        public static UIDataTable getGroupList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowSystemPro.getGroupList(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getPersonList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowSystemPro.getPersonList(a_intPageSize, a_intPageIndex, where);
        }
    }
}
