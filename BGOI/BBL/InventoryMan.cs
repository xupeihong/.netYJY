using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
namespace TECOCITY_BGOI
{
    public class InventoryMan
    {
        //打印
        public static DataTable PrintList(string strWhere, string tableName, ref string strErr)
        {
            return InventoryPro.PrintList(strWhere, tableName, ref strErr);
        }
        //导出
        public static DataTable ToExcel(string strWhere, string tableName, string FieldName, string OrderBy, ref string strErr)
        {
            return InventoryPro.ToExcel(strWhere, tableName, FieldName, OrderBy, ref strErr);
        }
        public static DataTable getSpecOptionalAdd(string UnitID, string Spec)
        {
            return InventoryPro.getSpecOptionalAdd(UnitID, Spec);
        }


        public static bool AddInventLog(tk_Inventorylog logobj)
        {
            return InventoryPro.AddInventLog(logobj);
        }



        public static UIDataTable StockRemainList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.StockRemainList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable StockRemainListnew(int a_intPageSize, int a_intPageIndex, string where, string PID, string UnitID)
        {
            return InventoryPro.StockRemainListnew(a_intPageSize, a_intPageIndex, where, PID, UnitID);
        }
        #region [下拉框]
        public static List<SelectListItem> GetHouseID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseID();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetHouseIDRU()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseIDRU();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetHouseSYID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseSYID();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetHouseID2()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseID();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetSubjectID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetSubjectID();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                if (SelListItem.Value == "S00005")
                {
                    SelListItem.Selected = true;
                }
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetPType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetPType();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //厂家
        public static List<SelectListItem> GetManufacturer()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetManufacturer();
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
                SelListItem.Text = dtDesc.Rows[i]["COMNameC"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetProType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetProType();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                if (SelListItem.Value == "2")
                {
                    SelListItem.Selected = true;
                }
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetSpec()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetSpec();
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
                SelListItem.Value = dtDesc.Rows[i]["Text"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetStockOutUse()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetStockOutUse();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        #endregion
        #region [一二级仓库下拉框]
        //根据库房类型加载一级仓库
        public static DataTable GetHouseIDone(string TypeID)
        {
            return InventoryPro.GetHouseIDone(TypeID);
        }
        public static DataTable GetHouseIDoneNew(string TypeID)
        {
            return InventoryPro.GetHouseIDoneNew(TypeID);
        }
        //根据库房类型加载二级仓库
        public static DataTable GetHouseIDtwo(string TypeID)
        {
            return InventoryPro.GetHouseIDtwo(TypeID);
        }
        public static DataTable GetHouseIDtwoNew(string TypeID)
        {
            return InventoryPro.GetHouseIDtwoNew(TypeID);
        }
        public static DataTable GetHouseIDtwoNewnew(string HouseID, string ProType)
        {
            return InventoryPro.GetHouseIDtwoNewnew(HouseID, ProType);
        }
        public static DataTable GetHouseIDoneNewnew(string HouseID, string ProType)
        {
            return InventoryPro.GetHouseIDoneNewnew(HouseID, ProType);
        }
        #endregion
        public static string GetTopListInID()
        {
            return InventoryPro.GetTopListInID();
        }
        //批次
        public static string GetTopHandwrittenAccount()
        {
            return InventoryPro.GetTopHandwrittenAccount();
        }

        public static string ProStockInDetialNum(string CID)
        {
            return InventoryPro.ProStockInDetialNum(CID);
        }
        public static string GetTopListOutID()
        {
            return InventoryPro.GetTopListOutID();
        }
        public static bool SaveProcureStockIn(StockIn record, string SHID, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveProcureStockIn(record, SHID, delist, ref strErr);
        }
        public static bool AddRecordInfo(StockIn record, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.AddRecordInfo(record, delist, ref strErr);
        }
        public static bool UpDateState(string CID)
        {
            return InventoryPro.UpDateState(CID);
        }
        public static bool UpProtoDateState(string CID)
        {
            return InventoryPro.UpProtoDateState(CID);
        }
        public static bool UpProductionDateState(string CID)
        {
            return InventoryPro.UpProductionDateState(CID);
        }
        public static bool AddTestStockIn(StockIn record, string TID, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.AddTestStockIn(record, TID, delist, ref strErr);
        }
        public static bool SaveProtoStockIn(StockIn record, string DID, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveProtoStockIn(record, DID, delist, ref strErr);
        }
        public static bool SaveProductionStockIn(StockIn record, string RKID, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveProductionStockIn(record, RKID, delist, ref strErr);
        }
        public static bool SaveBasicStockIn(StockIn record, List<StockInDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveBasicStockIn(record, delist, ref strErr);
        }
        public static bool SaveOrderSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveOrderSalesOut(record, DID, Count, delist, ref strErr);
        }
        public static bool UpOutDateState(string ListOutID)
        {
            return InventoryPro.UpOutDateState(ListOutID);
        }
        public static bool SaveOrderProSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveOrderProSalesOut(record, DID, Count, delist, ref strErr);
        }
        public static bool SaveProtoUpOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveProtoUpOut(record, DID, Count, delist, ref strErr);
        }
        public static bool SaveBuyGiveOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveBuyGiveOut(record, DID, Count, delist, ref strErr);
        }
        public static DataTable GetProtoDetail(string EID)
        {
            return InventoryPro.GetProtoDetail(EID);
        }
        public static DataTable GetOutByWhere(string a_strWhere, ref string a_strErr)
        {
            DataTable dtInfo = InventoryPro.GetOutByWhere(a_strWhere, ref a_strErr);
            if (dtInfo == null) return null;
            if (dtInfo.Rows.Count == 0) return null;

            return dtInfo;
        }
        public static DataTable GetConfigInfo(string taskType)
        {
            return InventoryPro.GetConfigInfo(taskType);
        }
        public static DataTable GetProcureDetail(string SHID, string RKID)
        {
            return InventoryPro.GetProcureDetail(SHID, RKID);
        }
        public static DataTable GetSalesReturnTask(string EID)
        {
            return InventoryPro.GetSalesReturnTask(EID);
        }
        public static DataTable GetProductionDetail(string EID)
        {
            return InventoryPro.GetProductionDetail(EID);
        }
        public static DataTable GetBasicDetail(string PID)
        {
            return InventoryPro.GetBasicDetail(PID);
        }
        public static DataTable GetBasicOUTDetail(string PID)
        {
            return InventoryPro.GetBasicOUTDetail(PID);
        }
        public static DataTable GetBasicDetailSpec(string PID)
        {
            return InventoryPro.GetBasicDetailSpec(PID);
        }
        public static DataTable GetOrderSalesDetail(string EID)
        {
            return InventoryPro.GetOrderSalesDetail(EID);
        }
        public static DataTable GetProtoUpDetail(string DID)
        {
            return InventoryPro.GetProtoUpDetail(DID);
        }
        public static DataTable GetBuyGiveDetail(string DID)
        {
            return InventoryPro.GetBuyGiveDetail(DID);
        }
        public static UIDataTable StockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.StockInList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable ChangeProcureList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ChangeProcureList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable StockInDetialList(int a_intPageSize, int a_intPageIndex, string CID)
        {
            return InventoryPro.StockInDetialList(a_intPageSize, a_intPageIndex, CID);
        }
        public static UIDataTable TestStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.TestStockInList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable ChangeTestList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ChangeTestList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ProtoStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ProtoStockInList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable ProtoStockInListShengChan(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ProtoStockInListShengChan(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable ChangeProtoList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ChangeProtoList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ChangeProductionList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ChangeProductionList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable BasicStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.BasicStockInList(a_intPageSize, a_intPageIndex, where);
        }
        //成本
        public static UIDataTable WarehousingCostList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.WarehousingCostList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.GetPtype(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype, string PID)
        {
            return InventoryPro.ChangePtypeList(a_intPageSize, a_intPageIndex, ptype, PID);
        }
        public static UIDataTable ChangePtypeListLinJian(int a_intPageSize, int a_intPageIndex, string ptype, string PID)
        {
            return InventoryPro.ChangePtypeListLinJian(a_intPageSize, a_intPageIndex, ptype, PID);
        }
        public static UIDataTable ChangePtypeListnew(int a_intPageSize, int a_intPageIndex, string PID, string type, string Spec)
        {
            return InventoryPro.ChangePtypeListnew(a_intPageSize, a_intPageIndex, PID, type, Spec);
        }
        public static UIDataTable RetailSalesOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.RetailSalesOutList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable OrderInfoSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.OrderInfoSalesList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable StockOutDetialList(int a_intPageSize, int a_intPageIndex, string ListOutID)
        {
            return InventoryPro.StockOutDetialList(a_intPageSize, a_intPageIndex, ListOutID);
        }
        public static UIDataTable ProjectSalesOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ProjectSalesOutList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable OrderProSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.OrderProSalesList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ProtoUpDetailList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ProtoUpDetailList(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ProtoUpOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ProtoUpOutList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable BuyGiveOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.BuyGiveOutList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable InternalDetailList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.InternalDetailList(a_intPageSize, a_intPageIndex);
        }
        #region [二级库出库]
        public static UIDataTable SecondOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.SecondOutList(a_intPageSize, a_intPageIndex, where);
        }
        public static bool SaveSecondOut(StockOut record, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveSecondOut(record, Count, delist, ref strErr);
        }

        #endregion
        #region [专柜出库]
        public static DataTable GetCounterSalesDetail(string EID)
        {
            return InventoryPro.GetCounterSalesDetail(EID);
        }

        public static UIDataTable CounterOutSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.CounterOutSalesList(a_intPageSize, a_intPageIndex);
        }
        public static bool SaveCounterSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveCounterSalesOut(record, DID, Count, delist, ref strErr);
        }

        public static UIDataTable BasicOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.BasicOutList(a_intPageSize, a_intPageIndex, where);
        }

        public static bool SaveBacicOut(StockOut record, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveBacicOut(record, Count, delist, ref strErr);
        }


        #endregion
        #region [生产领料单出库]
        public static bool SaveProductionMaterials(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveProductionMaterials(record, DID, Count, delist, ref strErr);
        }
        public static UIDataTable ProductionMaterialsOutSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ProductionMaterialsOutSalesList(a_intPageSize, a_intPageIndex);
        }
        public static DataTable GetProductionMaterialsSalesDetail(string EID)
        {
            return InventoryPro.GetProductionMaterialsSalesDetail(EID);
        }


        #endregion
        #region [家用产品销售]
        public static UIDataTable ChangeHomeProductSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.ChangeHomeProductSalesList(a_intPageSize, a_intPageIndex);
        }
        public static DataTable GetHomeProductSales(string DID)
        {
            return InventoryPro.GetHomeProductSales(DID);
        }
        public static DataTable GetOrderID(string ListOutID)
        {
            return InventoryPro.GetOrderID(ListOutID);
        }
        public static bool SaveHomeProductSales(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            return InventoryPro.SaveHomeProductSales(record, DID, Count, delist, ref strErr);
        }
        public static UIDataTable HomeProductSalesList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.HomeProductSalesList(a_intPageSize, a_intPageIndex, where);
        }

        public static bool AddInAlarm(InAlarm inal)
        {
            return InventoryPro.AddInAlarm(inal);
        }

        public static bool UpHomeProductSalesState(string ListOutID, string orderid)
        {
            return InventoryPro.UpHomeProductSalesState(ListOutID, orderid);
        }

        //提示报警
        public static DataTable GetOrderidNew()
        {
            return InventoryPro.GetOrderidNew();
        }
        #endregion
        #region [入库单管理]
        //入库单管理列表页
        public static UIDataTable StorageManagementtList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.StorageManagementtList(a_intPageSize, a_intPageIndex, where);
        }


        #endregion
        #region [出库单管理]
        public static UIDataTable StorageManagementOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.StorageManagementOutList(a_intPageSize, a_intPageIndex, where);
        }
        //public static bool UpRefundDateState(string ListOutID)
        //{
        //    return InventoryPro.UpRefundDateState(ListOutID);
        //}
        #endregion
        #region [报废单管理]
        public static UIDataTable ScrapManagementList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ScrapManagementList(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetTopListScrapID()
        {
            return InventoryPro.GetTopListScrapID();
        }

        public static bool SaveScrapManagement(Scrap record, List<Scrap> delist, string Count, ref string strErr)
        {
            return InventoryPro.SaveScrapManagement(record, delist, Count, ref strErr);
        }
        //报废完成
        public static bool UpScrapDateState(string ListScrapID, ref string strErr)
        {
            return InventoryPro.UpScrapDateState(ListScrapID, ref strErr);
        }
        #endregion
        #region [销售发货单管理]
        //销售发货单列表
        public static UIDataTable SalesInvoiceManagementList(int a_intPageSize, int a_intPageIndex, string where, string whereone)
        {
            return InventoryPro.SalesInvoiceManagementList(a_intPageSize, a_intPageIndex, where, whereone);
        }
        //得到发货单编号和创建人
        public static string GetTopListSalesInvoiceID()
        {
            return InventoryPro.GetTopListSalesInvoiceID();
        }
        //制作销售发货单
        //public static bool SaveSalesInvoiceManagement(Shipments record, List<Shipments_DetailInfo> delist, ref string strErr)
        //{
        //    return InventoryPro.SaveSalesInvoiceManagement(record, delist, ref strErr);
        //}
        //销售发货单完成
        public static bool UpSalesInvoDateState(string ShipGoodsID, ref string strErr)
        {
            return InventoryPro.UpSalesInvoDateState(ShipGoodsID, ref strErr);
        }
        //订单
        public static UIDataTable OrderInfoInvoSalesList(int a_intPageSize, int a_intPageIndex)
        {
            return InventoryPro.OrderInfoInvoSalesList(a_intPageSize, a_intPageIndex);
        }
        public static DataTable GetOrderSalesInvoDetail(string EID)
        {
            return InventoryPro.GetOrderSalesInvoDetail(EID);
        }
        public static UIDataTable SalesInvoiceManagementDetialList(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            return InventoryPro.SalesInvoiceManagementDetialList(a_intPageSize, a_intPageIndex, ShipGoodsID);
        }
        #endregion
        #region [新增货品]
        public static DataTable UpInventoryList(string PID)
        {
            return InventoryPro.UpInventoryList(PID);
        }
        public static UIDataTable InventoryAddProList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.InventoryAddProList(a_intPageSize, a_intPageIndex, where);
        }
        public static bool SaveInventoryAddPro(tk_ProductInfo record, ref string strErr)
        {
            return InventoryPro.SaveInventoryAddPro(record, ref strErr);
        }
        public static bool SaveInventoryAddProNew(tk_ProductInfo record, ref string strErr)
        {
            return InventoryPro.SaveInventoryAddProNew(record, ref strErr);
        }
        //供应商
        public static DataTable GetMan()
        {
            return InventoryPro.GetMan();
        }

        // 检查数据库中数据是否重复 
        public static int checkDataList(string year, string month)
        {
            return InventoryPro.checkDataList(year, month);

        }
        // 保存数据 
        // 上传信息：物料编码，物料长描述，供应商，计量单位，需要数量，计划单位，需要日期，备注，计划年，计划月，是否有效，创建时间
        public static bool SavePlanData(string strData, ref string strErr)
        {
            return InventoryPro.SavePlanData(strData, ref strErr);
        }
        #region [新增规格型号]
        public static bool SaveAddSpec(tk_ProductSpec record, ref string strErr)
        {
            return InventoryPro.SaveAddSpec(record, ref strErr);
        }
        public static string GetTopGGID()
        {
            return InventoryPro.GetTopGGID();
        }
        #endregion
        #endregion
        #region [创建库房]

        public static string HouserID()
        {
            return InventoryPro.HouserID();
        }
        public static bool SaveInventoryAddFirstPage(tk_WareHouse rem, ref string strErr)
        {
            return InventoryPro.SaveInventoryAddFirstPage(rem, ref strErr);
        }
        public static List<string> GetCode()
        {
            List<string> list = new List<string>();
            DataTable dt = InventoryPro.GetSupCode();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["HouseName"].ToString());
            }
            return list;
        }
        #endregion
        #region [调拨单]
        public static string GetTopID()
        {
            return InventoryPro.GetTopID();
        }
        public static string GetTopDID()
        {
            return InventoryPro.GetTopID();
        }
        public static UIDataTable AllocationSheetList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.AllocationSheetList(a_intPageSize, a_intPageIndex, where);
        }
        public static bool SaveAllocationSheet(tk_AllocationSheet record, List<tk_AllocationSheetDetailed> delist, string Count, ref string strErr)
        {
            return InventoryPro.SaveAllocationSheet(record, delist, Count, ref strErr);
        }


        public static UIDataTable AllocationSheetDetialList(int a_intPageSize, int a_intPageIndex, string ID)
        {
            return InventoryPro.AllocationSheetDetialList(a_intPageSize, a_intPageIndex, ID);
        }
        //根据用户id加载部门(上级ID)
        public static List<SelectListItem> GetDepName()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDepName();
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
                SelListItem.Value = dtDesc.Rows[i]["DeptId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["DeptName"].ToString();
                if (SelListItem.Value == GAccount.GetAccountInfo().UnitID)
                {
                    SelListItem.Selected = true;
                }
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        //加载仓库类型
        public static List<SelectListItem> GetHouseType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseType();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //根据用户id加载部门（当前部门与旗下部门）
        public static List<SelectListItem> GetDepNameDQ()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDepNameDQ();
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
                SelListItem.Value = dtDesc.Rows[i]["DeptId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["DeptName"].ToString();
                if (SelListItem.Value == GAccount.GetAccountInfo().UnitID)
                {
                    SelListItem.Selected = true;
                }
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetOneHouse(string oneHouserID)
        {
            return InventoryPro.GetOneHouse(oneHouserID);
        }
        //加载一级仓库
        //public static List<SelectListItem> GetOneHouse(string oneHouserID)
        //{

        //    List<SelectListItem> ListItem = new List<SelectListItem>();
        //    DataTable dtDesc = InventoryPro.GetOneHouse(oneHouserID);
        //    if (dtDesc == null)
        //    {
        //        return ListItem;
        //    }
        //    SelectListItem SelListItem = new SelectListItem();
        //    SelListItem.Value = "";
        //    SelListItem.Text = "请选择";
        //    ListItem.Add(SelListItem);
        //    for (int i = 0; i < dtDesc.Rows.Count; i++)
        //    {
        //        SelListItem = new SelectListItem();
        //        SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
        //        SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
        //        ListItem.Add(SelListItem);
        //    }
        //    return ListItem;
        //}
        //加载二级仓库
        public static List<SelectListItem> GetTwoHouse(string twoHouserID)
        {

            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetTwoHouse(twoHouserID);
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        //根据选择id加载仓库
        public static DataTable GetUserName(string DeptId)
        {
            return InventoryPro.GetUserName(DeptId);
        }
        #endregion
        #region [成品定义]
        // 保存数据 
        //货品唯一编号	组装该成品的零件PID	需零件数量	规格型号
        public static bool SaveDefinitionOfProduct(string strData, ref string strErr)
        {
            return InventoryPro.SaveDefinitionOfProduct(strData, ref strErr);
        }
        public static UIDataTable DefinitionOfProductList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.DefinitionOfProductList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable DefinitionOfList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.DefinitionOfList(a_intPageSize, a_intPageIndex, where);
        }
        public static List<SelectListItem> GetDefinitionOfProduct()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDefinitionOfProduct();
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
                SelListItem.Value = dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static bool SaveAddDefinitionOfProduct(List<tk_ProFinishDefine> delist, string type, string ProductID, ref string strErr)
        {
            return InventoryPro.SaveAddDefinitionOfProduct(delist, type, ProductID, ref strErr);
        }
        //撤销
        public static bool DeDefinitionOfProduct(string PID, ref string strErr)
        {
            return InventoryPro.DeDefinitionOfProduct(PID, ref strErr);
        }
        //添加可生产
        public static bool AddProFin(string PID, ref string strErr)
        {
            return InventoryPro.AddProFin(PID, ref strErr);
        }
        //修改加载数据(下拉框)
        public static DataTable GetUpDefinitionOfProduct(string PID)
        {
            return InventoryPro.GetUpDefinitionOfProduct(PID);
        }
        //修改加载数据(零件)
        public static DataTable GetUpXian(string PID)
        {
            return InventoryPro.GetUpXian(PID);
        }

        #region [添加下拉框]
        //加载名称
        public static List<SelectListItem> GetDefinitionOfProductName()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDefinitionOfProductName();
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
                SelListItem.Value = "";// dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //加载编号
        public static List<SelectListItem> GetDefinitionOfProductPID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDefinitionOfProductPID();
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
                SelListItem.Value = "";// dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["PID"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //加载编号
        public static List<SelectListItem> GetDefinitionOfProductSpec()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetDefinitionOfProductSpec();
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
                SelListItem.Value = "";// dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Spec"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        #endregion

        #region [根据产品名称加载规格和编号]
        public static DataTable GetProNameToSpec(string ProName)
        {
            return InventoryPro.GetProNameToSpec(ProName);
        }

        public static DataTable GetPIDToSpec(string ProductID)
        {
            return InventoryPro.GetPIDToSpec(ProductID);
        }
        public static DataTable GetProNameToPID(string ProName)
        {
            return InventoryPro.GetProNameToPID(ProName);
        }
        #endregion
        #endregion
        #region [统计总汇]

        #region [物料总汇]
        public static DataTable MaterialSummaryTableList(string HouseID, string start, string end, string where)
        {
            return InventoryPro.MaterialSummaryTableList(HouseID, start, end, where);
        }

        #endregion
        #region [库存汇总表]
        public static DataTable InventorySummaryTableList(string start, string end, string where)
        {
            return InventoryPro.InventorySummaryTableList(start, end, where);
        }
        public static DataTable AdditionalList(string start, string end, string where)
        {
            return InventoryPro.AdditionalList(start, end, where);
        }

        //成本
        public static DataTable KuCunZongHui()
        {
            return InventoryPro.KuCunZongHui();
        }

        //计算金额，总成本
        public static DataTable GetJinE()
        {
            return InventoryPro.GetJinE();
        }
        #endregion

        #region [物料出入库明细表]
        public static DataTable MaterialOutOfTheWarehouseList(string start, string end, string where)
        {
            return InventoryPro.MaterialOutOfTheWarehouseList(start, end, where);
        }
        public static DataTable AddWarehouseList(string start, string end, string where)
        {
            return InventoryPro.AddWarehouseList(start, end, where);
        }
        #endregion

        #region [库存统计表]
        public static DataTable InventoryStatisticsList()
        {
            return InventoryPro.InventoryStatisticsList();
        }
        #endregion

        #endregion
        #region [库存报警]


        //最低库存报警--物料信息列表
        public static UIDataTable MaterialBasicData(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.MaterialBasicData(a_intPageSize, a_intPageIndex, where);
        }
        //最低库存报警--查询上限数量
        public static DataTable MaterialBasicNum()
        {
            return InventoryPro.MaterialBasicNum();
        }
        //设备入库时，设备ID回显
        public static string getAlarm(string strFirsTypeText, string strCount)
        {
            return InventoryPro.getAlarm(strFirsTypeText, strCount);
        }
        // 获取报警库存列表
        public static string getWarnLow(ref string a_strErr)
        {
            DataTable dtWarnLow = new DataTable();
            dtWarnLow = InventoryPro.getWarnLow(ref a_strErr);

            if (dtWarnLow == null)
                return "";
            if (dtWarnLow.Rows.Count == 0)
                return "";

            string strWarnLow = GFun.Dt2Json("WarnLow", dtWarnLow);
            return strWarnLow;
        }
        //查询物料编号
        public static List<SelectListItem> GetNum()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetNum();
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
                SelListItem.Value = dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable UpLowAlarm(string PID)
        {
            return InventoryPro.UpLowAlarm(PID);
        }
        public static DataTable GetPidXiang(string PID)
        {
            return InventoryPro.GetPidXiang(PID);
        }
        public static bool SaveLowAlarm(tk_HouseEarlyWarningNum record, ref string strErr)
        {
            return InventoryPro.SaveLowAlarm(record, ref strErr);
        }
        public static bool LowAlarmZSC(string PID, ref string strErr)
        {
            return InventoryPro.LowAlarmZSC(PID, ref strErr);
        }
        public static bool LowAlarmZT(string PID, ref string strErr)
        {
            return InventoryPro.LowAlarmZT(PID, ref strErr);
        }
        #endregion
        #region [库房]
        public static UIDataTable StorageRoomList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.StorageRoomList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeStorageRoom(string HouseID, ref string strErr)
        {
            return InventoryPro.DeStorageRoom(HouseID, ref strErr);
        }

        public static DataTable UpStorageRoom(string HFID)
        {
            return InventoryPro.UpStorageRoom(HFID);
        }

        public static DataTable UpUpStorageRoom(string HouseID)
        {
            return InventoryPro.UpUpStorageRoom(HouseID);
        }
        public static bool SaveUpStorageRoom(tk_WareHouse rem, ref string strErr)
        {
            return InventoryPro.SaveUpStorageRoom(rem, ref strErr);
        }
        #endregion
        #region [配置信息]
        public static UIDataTable ConfigurationInformationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ConfigurationInformationList(a_intPageSize, a_intPageIndex, where);
        }
        //添加
        public static bool InsertNewContentnew(string type, string text, ref string a_strErr)
        {
            if (InventoryPro.InsertNewContentnew(type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        //删除
        public static bool DeleteNewContentnew(string xid, string type, ref string a_strErr)
        {
            if (InventoryPro.DeleteContentnew(xid, type, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        //修改
        public static bool UpdateNewContentnew(string xid, string type, string text, ref string a_strErr)
        {
            if (InventoryPro.UpdateContentnew(xid, type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        #endregion
        #region 【发展】
        public static List<SelectListItem> GetHouseFZ()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetHouseFZ();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        //查询物料编号
        public static List<SelectListItem> GetLJNum()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetLJNum();
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
                SelListItem.Value = dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        #region 【基本入库】
        //导出
        public static DataTable GetBasicStockInFZToExcel(string strWhere, string ListInID, ref string strErr)
        {
            return InventoryPro.GetBasicStockInFZToExcel(strWhere, ListInID, ref strErr);
        }

        #endregion
        #region 【基本出库】
        //导出
        public static DataTable BasicStockOutToExcelFZ(string strWhere, string ListOutID, ref string strErr)
        {
            return InventoryPro.BasicStockOutToExcelFZ(strWhere, ListOutID, ref strErr);
        }

        #endregion
        #endregion
        #region [提醒]
        //入库
        public static DataTable GetNumTiXinRu()
        {
            return InventoryPro.GetNumTiXinRu();
        }
        public static DataTable GetNumTiXinRuNew()
        {
            return InventoryPro.GetNumTiXinRuNew();
        }
        #endregion
        #region [库存账单]
        public static DataTable InventoryBillList(string start, string end, string where, string PID, string Spec, string ProName, string SingleLibrary, string ListID)
        {
            return InventoryPro.InventoryBillList(start, end, where, PID, Spec, ProName, SingleLibrary, ListID);
        }
        #endregion
        #region [产品类型设置]
        public static List<SelectListItem> GetConfigPType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = InventoryPro.GetConfigPType();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable ProductTypeSettingList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.ProductTypeSettingList(a_intPageSize, a_intPageIndex, where);
        }
        public static int OID()
        {
            return InventoryPro.OID();
        }

        public static string GetID()
        {
            return InventoryPro.GetID();
        }

        public static bool SaveAddProductTypeSetting(tk_ConfigPType rem, ref string strErr, string type)
        {
            return InventoryPro.SaveAddProductTypeSetting(rem, ref strErr, type);
        }

        //撤销
        public static bool DeProductTypeSetting(string ID, ref string strErr)
        {
            return InventoryPro.DeProductTypeSetting(ID, ref strErr);
        }

        public static DataTable UpProductTypeSetting(string ID)
        {
            return InventoryPro.UpProductTypeSetting(ID);
        }
        #endregion

        #region [上传]
        public static bool InsertAwardInOut(tk_AwardInOut bas, HttpFileCollection fileByte, ref string Err)
        {
            return InventoryPro.InsertAwardInOut(bas, fileByte, ref Err);
        }
        public static UIDataTable InOutUploadList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return InventoryPro.InOutUploadList(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetTopSID()
        {
            return InventoryPro.GetTopSID();
        }

        //撤销
        public static bool DeInOutUpload(string SID, ref string strErr)
        {
            return InventoryPro.DeInOutUpload(SID, ref strErr);
        }
        public static DataTable GetNewDownloadAward(string id)
        {
            return InventoryPro.GetDownloadAward(id);
        }

        public static DataTable GetFilesNew(string OId)
        {
            return InventoryPro.GetFilesNew(OId);
        }
        #endregion
    }
}
