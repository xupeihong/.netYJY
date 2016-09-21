using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Mvc;
using System.Web;

namespace TECOCITY_BGOI
{
    public class SalesRetailMan
    {
        #region 零售销售
        public static string GetDHNO(string UnitID, string Sid, string TaskType)
        {
            return SalesRetailPro.GetDHNO(UnitID, Sid, TaskType);
        }

        public static bool SaveSalesRecord(OrdersInfo order, Alarm alarm, List<Orders_DetailInfo> orderlist, ref string StrErr)
        {
            return SalesRetailPro.SaveSalesRecord(order,alarm, orderlist, ref StrErr);
        }

        public static bool UpdateSalesRecord(OrdersInfo order, List<Orders_DetailInfo> orderlist, string LoginUser, ref string StrErr)
        {
            return SalesRetailPro.UpdateSalesRecord(order, orderlist, LoginUser, ref StrErr);
        }

        public static bool DeleteRecord(string OrderID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeleteRecord(OrderID, LoginUser, ref strErr);
        }

        public static UIDataTable GetRetailApprovalGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            return SalesRetailPro.GetRetailApprovalGrid(a_intPageSize, a_intPageIndex, where, where2);
        }

        public static OrdersInfo GetOrderInfo(string OrderID)
        {
            return SalesRetailPro.GetOrderInfo(OrderID);
        }

        public static DataTable GetOrderDetailInfo(string OrderID)
        {
            return SalesRetailPro.GetOrderDetailInfo(OrderID);
        }

        public static string MoneyToUpper(string capValue)
        {
            return SalesRetailPro.MoneyToUpper(capValue);
        }

        public static string GetCopyNum(string UserID, string UnitId, string TaskType)
        {
            return SalesRetailPro.GetCopyNum(UserID, UnitId, TaskType);
        }

        public static DataTable GetSelectInfo(string TaskType)
        {
            return SalesRetailPro.GetSelectListitem(TaskType);
        }

        public static DataTable GetSPInfo(string PID)
        {
            return SalesRetailPro.GetSPInfo(PID);
        }

        public static List<SelectListItem> GetStateListitem(string TaskType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetStateDesc(TaskType);
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetUnitListitem()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetSelectUnit();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetUserListitem()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetUserListitem();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetCopyUnitListitem()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetCopyUnit();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetCompanyList()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetCompany();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetSelectListitem(string TaskType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetSelectListitem(TaskType);
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetCopyPerson(string unitId)
        {
            return SalesRetailPro.GetCopyPerson(unitId);
        }

        public static string GetTaskFiled(string filed, string filedName, string TabName, string ReturnFiled, string strType, string TypeName)
        {
            return SalesRetailPro.GetTaskFiled(filed, filedName, TabName, ReturnFiled, strType, TypeName);
        }

        public static bool SaveCopyPerson(string condition)
        {
            return SalesRetailPro.SaveCopyPerson(condition);
        }

        public static List<SelectListItem> GetMallsListitem()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesRetailPro.GetMallsListitem();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable GetSalesRetailList(int a_intPageSize, int a_intPageIndex, string unitId, string strWhere, string filed, string strWhere2)
        {
            return SalesRetailPro.GetSalesRetailList(a_intPageSize, a_intPageIndex, unitId, strWhere, filed, strWhere2);
        }

        public static DataTable GetRetailToPrint(string unitId, string strWhere, string filed, string strWhere2)
        {
            return SalesRetailPro.GetRetailToPrint(unitId, strWhere, filed, strWhere2);
        }

        public static UIDataTable GetDetailList(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesRetailPro.GetDetailList(a_intPageSize, a_intPageIndex, OrderID);
        }
        #endregion

        public static bool SaveInternalRecord(tk_InternalOrder interOrder, List<tk_InternalDetail> detaillist, ref string StrErr)
        {
            return SalesRetailPro.SaveInternalRecord(interOrder, detaillist, ref StrErr);
        }

        public static UIDataTable GetInternalList(int a_intPageSize, int a_intPageIndex, string unitId, string strWhere, string filed, string strWhere2)
        {
            return SalesRetailPro.GetInternalList(a_intPageSize, a_intPageIndex, unitId, strWhere, filed, strWhere2);
        }

        public static DataTable GetInternalToPrint(string UnitId, string strWhere, string filed, string strWhere2)
        {
            return SalesRetailPro.GetInternalToPrint(UnitId, strWhere, filed, strWhere2);
        }

        public static bool DeleteInternalApply(string IOID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeleteInternalApply(IOID, LoginUser, ref strErr);
        }

        public static UIDataTable GetInterDetailList(int a_intPageSize, int a_intPageIndex, string IOID)
        {
            return SalesRetailPro.GetInterDetailList(a_intPageSize, a_intPageIndex, IOID);
        }

        public static tk_InternalOrder GetInternalOrder(string IOID, string op)
        {
            return SalesRetailPro.GetInternalOrder(IOID, op);
        }

        public static bool UpdateInternalRecord(tk_InternalOrder interOrder, List<tk_InternalDetail> detaillist, string LoginUser, ref string StrErr)
        {
            return SalesRetailPro.UpdateInternalRecord(interOrder, detaillist, LoginUser, ref StrErr);
        }

        public static DataTable GetInternalDetail(string IOID, string op)
        {
            return SalesRetailPro.GetInternalDetail(IOID, op);
        }

        public static DataTable GetPrintInternalDetail(string IOID, string op)
        {
            return SalesRetailPro.GetPrintInternalDetail(IOID, op);
        }

        public static UIDataTable GetInternalApprovalGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            return SalesRetailPro.GetInternalApprovalGrid(a_intPageSize, a_intPageIndex, where, where2);
        }

        public static bool SaveShopeInfo(tk_ShoppeInfo shope, List<tk_ShoppeInfoDetail> detaillist, ref string StrErr)
        {
            return SalesRetailPro.SaveShopeInfo(shope, detaillist, ref StrErr);
        }

        public static UIDataTable GetShoppeList(int a_intPageSize, int a_intPageIndex, string unitId, string strWhere, string filed)
        {
            return SalesRetailPro.GetShoppeList(a_intPageSize, a_intPageIndex, unitId, strWhere, filed);
        }

        public static DataTable GetShoppeToPrint(string UnitId, string strWhere, string filed)
        {
            return SalesRetailPro.GetShoppeToPrint(UnitId, strWhere, filed);
        }

        public static bool DeleteShoppeInfo(string SIID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeleteShoppeInfo(SIID, LoginUser, ref strErr);
        }

        public static DataTable GetMallInfoList(string SIID)
        {
            return SalesRetailPro.GetMallInfoList(SIID);
        }

        public static tk_ShoppeInfo GetShoppeInfo(string SIID)
        {
            return SalesRetailPro.GetShoppeInfo(SIID);
        }
        public static tk_ShoppeInfo GetPrintShoppeInfo(string SIID)
        {
            return SalesRetailPro.GetPrintShoppeInfo(SIID);
        }


        public static DataTable GetShoppeDetailInfo(string SIID)
        {
            return SalesRetailPro.GetShoppeDetailInfo(SIID);
        }

        public static bool UpdateShoppeInfo(tk_ShoppeInfo shoppe, List<tk_ShoppeInfoDetail> shoppelist, string LoginUser, ref string StrErr)
        {
            return SalesRetailPro.UpdateShoppeInfo(shoppe, shoppelist, LoginUser, ref StrErr);
        }

        public static UIDataTable GetShoppeApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetShoppeApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static void SaveLog(Sales_SalesLog salesLog)
        {
            SalesRetailPro.SaveLog(salesLog);
        }

        public static UIDataTable GetPrototypeList(int a_intPageSize, int a_intPageIndex, string strWhere, string UnitID, string filed, string strWhere2)
        {
            return SalesRetailPro.GetPrototypeList(a_intPageSize, a_intPageIndex, strWhere, UnitID, filed, strWhere2);
        }

        public static DataTable GetPrototypeToPrint(string UnitId, string strWhere, string filed, string strWhere2)
        {
            return SalesRetailPro.GetPrototypeToPrint(UnitId, strWhere, filed, strWhere2);
        }

        public static bool DeleteProtoInfo(string PAID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeleteProtoInfo(PAID, LoginUser, ref strErr);
        }

        public static bool SavePrototype(tk_Property tkProto, List<tk_PropertyDetail> tkProlist, List<tk_PropertyDetail> tkProlist1, ref string StrErr)
        {
            return SalesRetailPro.SavePrototype(tkProto, tkProlist, tkProlist1, ref StrErr);
        }

        public static bool UpdateProtoType(tk_Property tkPro, List<tk_PropertyDetail> tkProlist, List<tk_PropertyDetail> tkProlist1, string LoginUser, ref string StrErr)
        {
            return SalesRetailPro.UpdateProtoType(tkPro, tkProlist, tkProlist1, LoginUser, ref StrErr);
        }

        public static tk_Property GetPrototypeInfo(string PAID)
        {
            return SalesRetailPro.GetPrototypeInfo(PAID);
        }

        public static DataTable GetProtoDetail(string PAID, string OperateType)
        {
            return SalesRetailPro.GetProtoDetail(PAID, OperateType);
        }

        public static UIDataTable GetProtoDetailList(int a_intPageSize, int a_intPageIndex, string PAID, string Op)
        {
            return SalesRetailPro.GetProtoDetailList(a_intPageSize, a_intPageIndex, PAID, Op);
        }

        public static UIDataTable GetProtoApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetProtoApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static string GetSubMalls(ref string strErr)
        {
            return SalesRetailPro.GetSubMalls(ref strErr);
        }

        public static string GetSubCom(ref string strErr)
        {
            return SalesRetailPro.GetSubBelongCom(ref strErr);
        }

        public static DataTable GetConfigBelongCom(string ConfigType, string ChidGrade)
        {
            return SalesRetailPro.GetConfigBelongCom(ConfigType, ChidGrade);
        }
        public static DataTable GetConfigRetail(string ConfigType, string ChidGrade) 
        {
            return SalesRetailPro.GetConfigRetail(ConfigType, ChidGrade);
        }

        public static UIDataTable GetBelongComSalesRetailList(int a_intPageSize, int a_intPageIndex, string strWhere, string MallID, string Grade) 
        {
            return SalesRetailPro.GetBelongComSalesRetailList(a_intPageSize, a_intPageIndex, strWhere, MallID, Grade);
        }

        public static UIDataTable GetFiveMallList(int a_intPageSize, int a_intPageIndex, string strWhere, string MallID, string Grade)
        {
            return SalesRetailPro.GetFiveMallList(a_intPageSize, a_intPageIndex, strWhere, MallID, Grade);
        }

        public static UIDataTable GetPromotionList(int a_intPageSize, int a_intPageIndex, string UnitID, string strwhere, string filed)
        {
            return SalesRetailPro.GetPromotionList(a_intPageSize, a_intPageIndex, UnitID, strwhere, filed);
        }

        public static DataTable GetPromotionToPrint(string UnitId, string strWhere, string filed)
        {
            return SalesRetailPro.GetPromotionToPrint(UnitId, strWhere, filed);
        }

        public static bool DeletePromotionInfo(string PID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeletePromotionInfo(PID, LoginUser, ref strErr);
        }

        public static bool AddPromotionInfo(tk_Promotion promotion, HttpFileCollection files, ref string a_strErr)
        {
            return SalesRetailPro.AddPromotionInfo(promotion, files, ref a_strErr);
        }

        public static bool UpdatePromotionInfo(tk_Promotion promotion, HttpFileCollection files, string LoginUser, ref string a_strErr)
        {
            return SalesRetailPro.UpdatePromotionInfo(promotion, files, LoginUser, ref a_strErr);
        }

        public static UIDataTable GetPromotionApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetPromotionApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetRetailFile(string PID, string FileName, string SalesType)
        {
            return SalesRetailPro.GetPromotionFile(PID, FileName, SalesType);
        }

        public static tk_Promotion GetPromotionInfo(string PID)
        {
            return SalesRetailPro.GetPromotionInfo(PID);
        }

        public static bool AddMarketInfo(tk_MarketSales market, HttpFileCollection files, ref string a_strErr)
        {
            return SalesRetailPro.AddMarketInfo(market, files, ref a_strErr);
        }

        public static UIDataTable GetMarketList(int a_intPageSize, int a_intPageIndex, string UnitID, string strwhere, string filed)
        {
            return SalesRetailPro.GetMarketList(a_intPageSize, a_intPageIndex, UnitID, strwhere, filed);
        }

        public static DataTable GetMarketToPrint(string UnitId, string strWhere, string filed)
        {
            return SalesRetailPro.GetMarketToPrint(UnitId, strWhere, filed);
        }

        public static tk_MarketSales GetMarketInfo(string PID)
        {
            return SalesRetailPro.GetMarketInfo(PID);
        }

        public static bool UpdateMarketSalesInfo(tk_MarketSales market, HttpFileCollection files, string LoginUser, ref string a_strErr)
        {
            return SalesRetailPro.UpdateMarketSalesInfo(market, files, LoginUser, ref a_strErr);
        }

        public static bool DeleteMarket(string PID, string LoginUser, ref string strErr)
        {
            return SalesRetailPro.DeleteMarket(PID, LoginUser, ref strErr);
        }

        public static UIDataTable GetMarketApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetMarketApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable GetRemindList(int a_intPageSize, int a_intPageIndex, string UnitName, string SalesType)
        {
            return SalesRetailPro.GetRemindList(a_intPageSize, a_intPageIndex, UnitName, SalesType);
        }

        public static DataTable GetNowRemindInfo(string UnitName, string SalesType, string UserId)
        {
            return SalesRetailPro.GetNowRemindInfo(UnitName, SalesType, UserId);
        }

        public static DataTable GetTopRetailLibraryTubeManage(string UserId)
        {
            return SalesRetailPro.GetTopRetailLibraryTubeManage(UserId);
        }

        //
        public static DataTable GetTopRetailAfterSaleManage(string UserId)
        {
            return SalesRetailPro.GetTopRetailAfterSaleManage(UserId);
        }

        public static UIDataTable GetBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetBasicGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool AddBasicInfo(string typeId, string textDesc)
        {
            return SalesRetailPro.AddBasicInfo(typeId, textDesc);
        }

        public static bool AlterBasicInfo(string XID, string Type, string Text)
        {
            return SalesRetailPro.AlterBasicInfo(XID, Type, Text);
        }

        public static bool DeleteBasicInfo(string XID, string Type)
        {
            return SalesRetailPro.DeleteBasicInfo(XID, Type);
        }

        public static UIDataTable GetMallsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetMallsGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertMallsInfo(string HigherUnitID, string Malls)
        {
            return SalesRetailPro.InsertMallsInfo(HigherUnitID, Malls);
        }

        public static bool UpdateMallsInfo(string ID, string HigherUnitID, string Malls)
        {
            return SalesRetailPro.UpdateMallsInfo(ID, HigherUnitID, Malls);
        }

        public static bool DeleteMalls(string ID, string HigherUnitID)
        {
            return SalesRetailPro.DeleteMalls(ID, HigherUnitID);
        }

        public static DataSet GetWeekStatics(string strWhere)
        {
            return SalesRetailPro.GetWeekStatics(strWhere);
        }

        public static UIDataTable GetDetailGrid(string a_strWhere)
        {
            return SalesRetailPro.GetDetailGrid(a_strWhere);
        }

        public static bool AlterInternalDetail(string IOID, string DID, string UserName, ref string a_strErr)
        {
            return SalesRetailPro.AlterInternalDetail(IOID, DID, UserName, ref a_strErr);
        }


        #region 售后库存关联的存储过程
        public static UIDataTable GetSalesRetailLibraryTubeGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetSalesRetailLibraryTubeGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable GetSalesRetailAfterSaleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesRetailPro.GetSalesRetailAfterSaleGrid(a_intPageSize, a_intPageIndex, where);
        }
        #endregion
    }
}
