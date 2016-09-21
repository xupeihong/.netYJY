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
    public class ProducePlanMan
    {
        public static DataTable GetZD()
        {
            return ProducePlanPro.GetZD();
        }

        public static List<SelectListItem> getstate(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePlanPro.getstate(strType);
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

        public static bool SaveProductPlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {
            return ProducePlanPro.SaveProductPlan(record, delist, ref strErr);
        }
        public static UIDataTable getPlanList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePlanPro.getPlanList(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable GetProductPlan(int a_intPageSize, int a_intPageIndex, string JHID)
        {
            return ProducePlanPro.GetProductPlan(a_intPageSize, a_intPageIndex, JHID);
        }
        public static tk_Product_Plan IndexAllupdatePlan(string JHID)
        {
            return ProducePlanPro.IndexAllupdatePlan(JHID);
        }
        public static DataTable LoadPlanDatail(string JHID)
        {
            return ProducePlanPro.LoadPlanDatail(JHID);
        }
        public static bool SaveUpdatePlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {
            return ProducePlanPro.SaveUpdatePlan(record, delist, ref strErr);
        }
        public static List<SelectListItem> GetPlanYear()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            DataTable dt = ProducePlanPro.GetPlanYear();
            if (dt == null)
                return listItem;
            SelectListItem selListItem = new SelectListItem();
            selListItem.Value = "";
            selListItem.Text = "";
            listItem.Add(selListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selListItem = new SelectListItem();
                selListItem.Value = dt.Rows[i][0].ToString();
                selListItem.Text = dt.Rows[i][0].ToString();
                listItem.Add(selListItem);
            }
            return listItem;
        }
    }
}
