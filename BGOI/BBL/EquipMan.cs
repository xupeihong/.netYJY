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
    public class EquipMan
    {
        public static List<SelectListItem> GetNewConfigContent(string Type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = EquipPro.GetConfigCont(Type);
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
        public static List<SelectListItem> GetNewState()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = EquipPro.getState();
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
                SelListItem.Value = dtDesc.Rows[i]["StateId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["name"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigContentByUnit(string UnitID)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = EquipPro.GetConfigContByUnit(UnitID);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            //SelListItem.Value = "";
            //SelListItem.Text = "请选择";
            //ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static bool InsertNewDeviceBas(DevicsBas Bas, ref string a_strErr)
        {
            if (EquipPro.InsertDeviceBas(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewEquipGrid(int a_intPageSize, int a_intPageIndex, string where, string order, string unit)
        {
            return EquipPro.getEquipGrid(a_intPageSize, a_intPageIndex, where, order, unit);
        }

        public static UIDataTable getNewStandingBookGrid(int a_intPageSize, int a_intPageIndex, string where, string Order)
        {
            return EquipPro.getStandingBookGrid(a_intPageSize, a_intPageIndex, where, Order);
        }

        public static UIDataTable getNewTracingGrid(int a_intPageSize, int a_intPageIndex, string where, string Order)
        {
            return EquipPro.getTracingGrid(a_intPageSize, a_intPageIndex, where, Order);
        }

        public static bool InsertNewCongTime(string checkWay, string num, string unit, string TimeType, ref string a_strErr)
        {
            if (EquipPro.InsertCongTime(checkWay, num, unit, TimeType, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static string getNewCK1time()
        {
            return EquipPro.getCK1Time();
        }

        public static string getNewCK2time()
        {
            return EquipPro.getCK2Time();
        }

        public static string getNewCK3time()
        {
            return EquipPro.getCK3Time();
        }

        public static UIDataTable getNewDevicsBasGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return EquipPro.getDevicsBasGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable getNewPintEquip(string where, string whereOrder)
        {
            return EquipPro.getPrintEquip(where, whereOrder);
        }

        public static DataTable getNewPintTracing(string where, string whereOrder)
        {
            return EquipPro.getPrintTracing(where, whereOrder);
        }

        public static DataTable getNewPrintStanding(string where, string whereOrder)
        {
            return EquipPro.getPrintStanding(where, whereOrder);
        }

        public static DCheckInfo getNewUpdateDCheckInfo(string id)
        {
            return EquipPro.getUpdateDCheckInfo(id);
        }

        public static DevicsBas getNewDevicsByID(string id)
        {
            return EquipPro.getDevicsByID(id);
        }

        public static bool UpdateNewDevice(DevicsBas Bas, string ecode, ref string a_strErr)
        {
            if (EquipPro.UpdateDeviceBas(Bas, ecode, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewDeviceBas(string ecode, ref string a_strErr)
        {
            if (EquipPro.DeleteDeviceBas(ecode, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewDCheckInfo(DCheckInfo CheckInfo, ref string a_strErr)
        {
            if (EquipPro.InsertDCheckInfo(CheckInfo, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewDCheckInfo(DCheckInfo CheckInfo, string id, string num, ref string a_strErr)
        {
            if (EquipPro.UpdateDCheckInfo(CheckInfo, id, num, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewDRepairInfo(DRepairInfo Repair, ref string a_strErr)
        {
            if (EquipPro.InsertDRepairInfo(Repair, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewDRepairInfo(DRepairInfo Repair, ref string a_strErr)
        {
            if (EquipPro.UpdateDRepairInfo(Repair, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewDScrapInfo(DScrapInfo Scrap, ref string a_strErr)
        {
            if (EquipPro.InsertDScrapInfo(Scrap, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewRativeSource(RativeSource Rsource, ref string a_strErr)
        {
            if (EquipPro.InsertRativeSource(Rsource, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewRativeSourceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return EquipPro.getRativeSourceGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static RativeSource getNewUpdateRativeSource(string id)
        {
            return EquipPro.getUpdateRative(id);
        }

        public static bool UpdateNewRativeSource(RativeSource Rsource, string id, ref string a_strErr)
        {
            if (EquipPro.UpdateRativeSource(Rsource, id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewRative(string id, ref string a_strErr)
        {
            if (EquipPro.DeleteRative(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
    }
}
