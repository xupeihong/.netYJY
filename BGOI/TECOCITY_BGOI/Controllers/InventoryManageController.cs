using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Text;

namespace TECOCITY_BGOI.Controllers
{
    [Authorization]
    [AuthorizationAttribute]
    public class InventoryManageController : Controller
    {
        //
        // GET: /InventoryManage/  

        public ActionResult Index()
        {
            return View();
        }
        //可选可添加
        public ActionResult getSpecOptionalAdd()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string UnitID = account.UnitID;
            string Spec = Request["Spec"].ToString();
            DataTable dt = InventoryMan.getSpecOptionalAdd(UnitID, Spec);
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string spec = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spec += dt.Rows[i]["Spec"].ToString() + ",";
            }
            spec = spec.TrimEnd(',');
            return Json(new { success = "true", Strname = spec });
        }






        #region 【仓库类型】
        //零件库二级库
        public ActionResult GetHouseIDtwo()
        {
            string TypeID = Request["va"].ToString();
            DataTable dt = InventoryMan.GetHouseIDtwo(TypeID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //零件库一级库
        public ActionResult GetHouseIDone()
        {
            string TypeID = Request["va"].ToString();
            DataTable dt = InventoryMan.GetHouseIDone(TypeID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //全部仓库类型二级库(根据产品库)
        public ActionResult GetHouseIDtwoNew()
        {
            string TypeID = Request["va"].ToString();
            DataTable dt = InventoryMan.GetHouseIDtwoNew(TypeID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //全部仓库类型一级库(根据产品库)
        public ActionResult GetHouseIDoneNew()
        {
            string TypeID = Request["va"].ToString();
            DataTable dt = InventoryMan.GetHouseIDoneNew(TypeID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //全部仓库类型一级库(根据部门类型,产品库类型)
        public ActionResult GetHouseIDoneNewnew()
        {
            string HouseID = Request["HouseID"].ToString();
            string ProType = Request["ProType"].ToString();
            DataTable dt = InventoryMan.GetHouseIDoneNewnew(HouseID, ProType);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //全部仓库类型二级库(根据部门类型,产品库类型)
        public ActionResult GetHouseIDtwoNewnew()
        {
            string HouseID = Request["HouseID"].ToString();
            string ProType = Request["ProType"].ToString();
            DataTable dt = InventoryMan.GetHouseIDtwoNewnew(HouseID, ProType);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region [发货单]

        public ActionResult AllocationSheet()
        {
            return View();
        }
        //加载列表
        public ActionResult AllocationSheetList(AllocationSheetQuery sheet)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string HouseID = Request["HouseID"].ToString().Trim(); //仓库名称
                string ID = sheet.ID;// Request["ID"].ToString().Trim();
                string OrderContent = sheet.OrderContent;// Request["OrderContent"].ToString().Trim();
                string SpecsModels = Request["SpecsModels"].ToString().Trim();
                string Begin = Request["Begin"].ToString().Trim();
                string End = Request["End"].ToString().Trim();
                if (Begin != "" && End != "")
                    where += " a.Inspector between '" + Begin + "' and '" + End + "' and";
                if (HouseID != "")
                    where += " b.HouseName='" + HouseID + "' and";
                if (Request["ID"] != "")
                    where += " a.ID like '%" + Request["ID"] + "%' and";
                if (Request["OrderContent"] != "")
                    where += " ID in (select DBID from BGOI_Inventory.dbo.tk_AllocationSheetDetailed  where OrderContent like '%" + Request["OrderContent"] + "%') and";
                if (SpecsModels != "")
                    where += " ID in (select DBID from BGOI_Inventory.dbo.tk_AllocationSheetDetailed  where SpecsModels like '%" + Request["SpecsModels"] + "%') and";

                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.AllocationSheetList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //发货单导出
        public FileResult AllocationSheetToExcel()
        {
            string where = " a.Validate='v' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string HouseID = Request["HouseID"].ToString().Trim(); //仓库名称
            string ID = Request["ID"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string SpecsModels = Request["SpecsModels"].ToString().Trim();
            string Begin = Request["Begin"].ToString().Trim();
            string End = Request["End"].ToString().Trim();
            if (Begin != "" && End != "")
                where += " a.Inspector between '" + Begin + "' and '" + End + "' and";
            if (HouseID != "")
                where += " b.HouseName='" + HouseID + "' and";
            if (ID != "")
                where += " a.ID like '%" + ID + "%' and";
            if (OrderContent != "")
                where += " ID in (select DBID from BGOI_Inventory.dbo.tk_AllocationSheetDetailed  where OrderContent like '%" + OrderContent + "%') and";
            if (SpecsModels != "")
                where += " ID in (select DBID from BGOI_Inventory.dbo.tk_AllocationSheetDetailed  where SpecsModels like '%" + SpecsModels + "%') and";
            var IDN = Request["IDN"].ToString();
            if (IDN != "")
            {
                var str = IDN.Remove(IDN.Length - 1, 1);
                where += "  a.ID in (" + str + ")  and";
            }
            string unitid = GAccount.GetAccountInfo().UnitID;
            //if (unitid == "46" || unitid == "32")
            //{
            //    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            //}
            //else
            //{
            //    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            //}
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_AllocationSheet a " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse b on a.UserID=b.HouseID " +
                                "  left join BGOI_Inventory.dbo.tk_WareHouse e on  a.Handlers=e.HouseID ";
            FieldName = " a.ID, a.CreateUnitID, a.Inspector,e.HouseName AS  'CK',b.HouseName as 'RK',a.Remark,a.ReasonRemark, a.CreateUser ";//, a.Handlers, a.UserID,   a.CreateTime, a.Validate 
            string OrderBy = " a.ID ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "发货编号-3000,创建单位-5000,单据日期-6000,出库仓库-5000,入库仓库-3000,";
                strCols += "备注-3000,原因描述-6000,创建人-3000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "发货单导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "发货单表.xls");
            }
            else
                return null;

        }
        //加载产品
        public ActionResult AllocationSheetDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ID = Request["ID"].ToString();


            UIDataTable udtTask = InventoryMan.AllocationSheetDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddAllocationSheet()
        {
            tk_AllocationSheet so = new tk_AllocationSheet();
            so.ID = InventoryMan.GetTopID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult SaveAllocationSheet()
        {
            if (ModelState.IsValid)
            {
                tk_AllocationSheet stockout = new tk_AllocationSheet();
                stockout.ID = Request["ID"].ToString();
                stockout.CreateUnitID = Request["CreateUnitID"].ToString();
                stockout.Inspector = Request["Inspector"].ToString();

                if (Request["IsHouseIDonechu"] != "0")//出库一级
                {
                    stockout.Handlers = Request["IsHouseIDonechu"].ToString();
                }
                if (Request["IsHouseIDtwochu"] != "0")//出库二级
                {
                    stockout.Handlers = Request["IsHouseIDtwochu"].ToString();
                }
                if (Request["IsHouseIDoneru"] != "0")//入库一级
                {
                    stockout.UserID = Request["IsHouseIDoneru"].ToString();
                }
                if (Request["IsHouseIDtworu"] != "0")//入库二级
                {
                    stockout.UserID = Request["IsHouseIDtworu"].ToString();
                }

                stockout.ReasonRemark = Request["ReasonRemark"].ToString();

                stockout.Remark = Request["Remark"].ToString();
                stockout.CreateTime = DateTime.Now;
                stockout.CreateUser = Request["CreateUser"].ToString();
                stockout.Validate = "v";
                string Count = Request["Count"].ToString();
                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrPrice2 = Request["Price2"].Split(',');
                string[] arrNOPrice = Request["NOPrice"].Split(',');
                string strErr = "";
                tk_AllocationSheetDetailed deInfo = new tk_AllocationSheetDetailed();
                List<tk_AllocationSheetDetailed> detailList = new List<tk_AllocationSheetDetailed>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new tk_AllocationSheetDetailed();
                    deInfo.DBID = Request["ID"].ToString();
                    deInfo.DID = InventoryMan.GetTopDID();
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.OrderContent = arrProName[i].ToString();
                    deInfo.SpecsModels = arrSpec[i].ToString();

                    deInfo.OrderUnit = arrUnitName[i].ToString();
                    deInfo.OrderNum = Convert.ToInt32(arrCount[i]);

                    if (arrPrice2[i] != "")
                    {
                        deInfo.NoTaxuUnit = Convert.ToDecimal(arrPrice2[i]);
                    }
                    if (arrPrice[i] != "")
                    {
                        deInfo.TaxUnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrNOPrice[i] != "")
                    {
                        deInfo.NOPrice = Convert.ToDecimal(arrNOPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.Price = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.State = "0";
                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveAllocationSheet(stockout, detailList, Count, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加调拨单";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_AllocationSheet";
                        log.Typeid = Request["ID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加调拨单";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_AllocationSheet";
                        log.Typeid = Request["ID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult GetUserName()
        {
            string DeptId = Request["DeptId"].ToString();
            DataTable dt = InventoryMan.GetUserName(DeptId);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //加载打印数据
        public ActionResult PrintAllocationSheet()
        {
            string ID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ID))
            {
                where += " where  ID = '" + ID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_AllocationSheet ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_AllocationSheet so = new tk_AllocationSheet();
            foreach (DataRow dt in data.Rows)
            {
                so.ID = ID;
                so.ReasonRemark = dt["ReasonRemark"].ToString();
                // ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "调拨未完成" : "调拨已完成";
            }
            //物料信息
            string wheretype = "";
            if (!string.IsNullOrEmpty(ID))
            {
                wheretype = " where [DBID]='" + ID + "'";
            }
            string protype = " BGOI_Inventory.dbo.tk_AllocationSheetDetailed ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["OrderContent"] = dtpt["OrderContent"].ToString();
                ViewData["SpecsModels"] = dtpt["SpecsModels"].ToString();
                ViewData["OrderUnit"] = dtpt["OrderUnit"].ToString();
                ViewData["OrderNum"] = dtpt["OrderNum"].ToString();
                ViewData["NoTaxuUnit"] = dtpt["NoTaxuUnit"].ToString();

            }

            //入库仓库
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ID))
            {
                wherehouse = "where HouseID in(select UserID from BGOI_Inventory.dbo.tk_AllocationSheet where ID='" + ID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["RuHouseName"] = dtpt["HouseName"].ToString();
            }
            //出库仓库
            string wherechouse = "";
            if (!string.IsNullOrEmpty(ID))
            {
                wherechouse = "where HouseID in(select UserID from BGOI_Inventory.dbo.tk_AllocationSheet where ID='" + ID + "')";
            }
            string prochouse = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprochouse = InventoryMan.PrintList(wherechouse, prochouse, ref strErr);
            foreach (DataRow dtptc in dtprochouse.Rows)
            {
                ViewData["CuHouseName"] = dtptc["HouseName"].ToString();
            }
            return View(so);
        }

        //加载物料信息
        public ActionResult PrintAllocationSheetJS()
        {
            string DBID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(DBID))
            {
                s += " DBID like '%" + DBID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_Inventory.dbo.tk_AllocationSheetDetailed  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region [库房]
        public ActionResult StorageRoom()
        {
            return View();
        }
        public ActionResult StorageRoomList(InventoryFirstPageQuery firstquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string UnitID = Request["HouseID"].ToString().Trim();//所属单位
                if (UnitID == "")
                {
                    string unitid = GAccount.GetAccountInfo().UnitID;
                    if (unitid == "46" || unitid == "32")
                    {
                        where = "";
                    }
                    else
                    {
                        where += " a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                    }
                }
                else
                {
                    where += " a.UnitID='" + UnitID + "' and";
                }
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string ProType = Request["ProType"].ToString().Trim();
                if (ProType != "请选择")
                    where += " a.typeid='" + ProType + "' and";
                if (Request["IsHouseIDone"] != "0")
                    where += " a.HouseID='" + Request["IsHouseIDone"] + "' and";
                if (Request["IsHouseIDtwo"] != "0")
                    where += " a.HouseID='" + Request["IsHouseIDtwo"] + "' and";
                //if (HouseID != "")
                //    where += " a.HouseName='" + HouseID + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.StorageRoomList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }

        //库房管理导出
        public FileResult StorageRoomListToExcel()
        {
            string where = " a.Validate='v' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            string UnitID = Request["HouseID"].ToString().Trim();//所属单位
            if (UnitID == "")
            {
                if (unitid == "46" || unitid == "32")
                {
                    where = "";
                }
                else
                {
                    where += " a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                }
            }
            else
            {
                where += " a.UnitID='" + UnitID + "' and";
            }
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string ProType = Request["ProType"].ToString().Trim();
            if (ProType != "请选择")
                where += " a.typeid='" + ProType + "' and";

            if (Request["IsHouseIDoneto"] != "0")
                where += " a.HouseID='" + Request["IsHouseIDoneto"] + "' and";
            if (Request["IsHouseIDtwoto"] != "0")
                where += " a.HouseID='" + Request["IsHouseIDtwoto"] + "' and";

            var HouseNameN = Request["HouseNameN"].ToString();
            if (HouseNameN != "")
            {
                var str = HouseNameN.Remove(HouseNameN.Length - 1, 1);
                where += "  HouseName in (" + str + ")  and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_WareHouse a " +
                              " left join BGOI_Inventory.dbo.tk_ConfigProType b on a.TypeID=b.OID " +
                              " left join BJOI_UM.dbo.UM_UnitNew c on a.UnitID=c.DeptId ";
            FieldName = "   HouseName,Adress,b.Text,IsHouseID,c.DeptName  ";
            string OrderBy = " a.HouseID ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "仓库名称-3000,仓库地址-5000,仓库类型-6000,仓库级别-5000,所属单位-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "库房管理导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "库房管理存表.xls");
            }
            else
                return null;

        }
        //撤销
        public ActionResult DeStorageRoom()
        {
            string strErr = "";
            string HouseID = Request["HouseID"].ToString();

            if (InventoryMan.DeStorageRoom(HouseID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销仓库";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WareHouse";
                log.Typeid = Request["HouseID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销仓库";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WareHouse";
                log.Typeid = Request["HouseID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        public ActionResult UpStorageRoom()
        {
            tk_WareHouse so = new TECOCITY_BGOI.tk_WareHouse();
            string HouseID = Request["HouseID"];
            DataTable dt = InventoryMan.UpStorageRoom(HouseID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                so.HouseID = dt.Rows[0][0].ToString();
                so.Adress = dt.Rows[0][1].ToString();
                so.HouseName = dt.Rows[0][2].ToString();
                so.UnitID = dt.Rows[0][4].ToString();
                so.IsHouseID = dt.Rows[0][8].ToString();
                so.TypeID = dt.Rows[0][9].ToString();
            }
            return View(so);
        }
        public ActionResult UpUpStorageRoom()
        {
            string HouseID = Request["HouseID"].ToString();
            DataTable dt = InventoryMan.UpUpStorageRoom(HouseID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加库房
        public ActionResult SaveUpStorageRoom()
        {
            if (ModelState.IsValid)
            {
                tk_WareHouse rem = new tk_WareHouse();
                if (Request["UnitID"] == "")
                {
                    rem.UnitID = GAccount.GetAccountInfo().UnitID;
                }
                else
                {
                    rem.UnitID = Request["UnitID"].ToString();
                }
                rem.HouseID = Request["HouseID"].ToString();
                rem.IsHouseID = Request["IsHouseID"].ToString();
                rem.Adress = Request["Adress"].ToString();
                rem.HouseName = Request["HouseName"].ToString();
                rem.TypeID = Request["TypeID"].ToString();
                rem.DelTime = DateTime.Now;
                rem.Validate = "v";
                string strErr = "";
                bool b = InventoryMan.SaveUpStorageRoom(rem, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "修改仓库";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WareHouse";
                        log.Typeid = Request["HouseID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "修改仓库";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WareHouse";
                        log.Typeid = Request["HouseID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        #region [新增库房]
        //新增库房
        public ActionResult AddInventoryFirstPage()
        {
            tk_WareHouse so = new tk_WareHouse();
            so.HouseID = InventoryMan.HouserID();
            return View(so);
        }
        //添加库房
        public ActionResult SaveInventoryAddFirstPage()
        {
            if (ModelState.IsValid)
            {
                tk_WareHouse rem = new tk_WareHouse();
                if (Request["UnitID"] == "")
                {
                    rem.UnitID = GAccount.GetAccountInfo().UnitID;
                }
                else
                {
                    rem.UnitID = Request["UnitID"].ToString();
                }
                rem.HouseID = Request["HouseID"].ToString();
                rem.IsHouseID = Request["IsHouseID"].ToString();
                rem.Adress = Request["Adress"].ToString();
                rem.HouseName = Request["HouseName"].ToString();
                rem.TypeID = Request["TypeID"].ToString();
                rem.DelTime = DateTime.Now;
                rem.Validate = "v";
                string strErr = "";
                bool b = InventoryMan.SaveInventoryAddFirstPage(rem, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加仓库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WareHouse";
                        log.Typeid = Request["HouseID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加仓库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WareHouse";
                        log.Typeid = Request["HouseID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        /// <summary>
        /// 验证编号是否重名方法
        /// </summary>
        /// <param name="SupplierCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HouseNameExists(string HouseName)
        {
            List<string> list = InventoryMan.GetCode();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == HouseName.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #endregion
        #region [新增货品]
        public ActionResult UPInventoryUPPro()
        {
            string PID = Request["PID"].ToString();
            string where = "";
            if (!string.IsNullOrEmpty(PID))
            {
                where += " where PID like '%" + PID + "%' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_ProductInfo ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_ProductInfo so = new TECOCITY_BGOI.tk_ProductInfo();
            foreach (DataRow dt in data.Rows)
            {
                so.PID = dt["PID"].ToString();//货品编号
                so.ProName = dt["ProName"].ToString();//货品名称
                so.MaterialNum = dt["MaterialNum"].ToString();
                so.Manufacturer = dt["Manufacturer"].ToString();
                so.Units = dt["Units"].ToString();
                so.Spec = dt["Spec"].ToString();
                string UnitPrice = dt["UnitPrice"].ToString();
                string Price2 = dt["Price2"].ToString();

                if (dt["UnitPrice"] != null || dt["UnitPrice"].ToString() != "")
                {
                    so.UnitPrice = Convert.ToDecimal(dt["UnitPrice"]);//单价（含税）
                }
                else
                {
                    so.UnitPrice = 0;//单价（含税）
                }
                if (dt["Price2"] != null || dt["Price2"].ToString() != "")
                {
                    so.Price2 = Convert.ToDecimal(dt["Price2"]);//不含税单价
                }
                else
                {
                    so.Price2 = 0;//不含税单价
                }

                so.Remark = dt["Remark"].ToString();
                so.Detail = dt["Detail"].ToString();
            }
            return View(so);
        }
        public ActionResult UpInventoryList()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.UpInventoryList(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //新增货品
        public ActionResult InventoryAddPro()
        {
            return View();
        }
        //加载货品列表
        public ActionResult InventoryAddProList(InventoryAddProQuery addpro)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string ProTypeID = Request["ProTypeID"].ToString().Trim(); ;
                string PID = addpro.PID;//Request["PID"].ToString().Trim();
                string ProName = addpro.ProName;// Request["ProName"].ToString().Trim();
                // string Spec = Request["Spec"].ToString().Trim();
                // where += " d.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                if (ProTypeID != "")
                    where += " b.ProTypeID='" + ProTypeID + "' and";
                if (Request["PID"] != "")
                    where += " b.PID like '%" + Request["PID"].ToString().Trim() + "%' and";
                if (Request["ProName"] != "")
                    where += " b.ProName like '%" + Request["ProName"].ToString().Trim() + "%' and";
                //if (Spec != "")
                //    where += " b.Spec like '" + Spec + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.InventoryAddProList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }



        }

        //货品导出
        public FileResult InventoryAddProListToExcel()
        {
            string where = " b.Ptype=e.ID and b.ProTypeID=c.ID and b.Manufacturer=f.SID and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string ProTypeID = Request["ProTypeID"].ToString().Trim(); ;
            string PID = Request["PID"].ToString().Trim();
            string ProName = Request["ProName"].ToString().Trim();

            if (ProTypeID != "")
                where += " b.ProTypeID='" + ProTypeID + "' and";
            if (Request["PID"] != "")
                where += " b.PID like '%" + Request["PID"].ToString().Trim() + "%' and";
            if (Request["ProName"] != "")
                where += " b.ProName like '%" + Request["ProName"].ToString().Trim() + "%' and";



            var PIDN = Request["PIDN"].ToString();
            if (PIDN != "")
            {
                var str = PIDN.Remove(PIDN.Length - 1, 1);
                where += "  b.PID in (" + str + ")  and";
            }
            string unitid = GAccount.GetAccountInfo().UnitID;
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,(select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') e ,BGOI_BasMan.dbo.tk_SupplierBas  f ";
            FieldName = "  PID,b.ProName,b.Spec, MaterialNum,UnitPrice,Units,Price2,Detail,e.[Text] as Ptext,f.COMNameC,Remark ";//c.Text, ProTypeID,   
            string OrderBy = " b.PID ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "产品编号-3000,产品名称-5000,规格型号-6000,物料号-5000,单价（含税）-3000,";
                strCols += "单位-3000,不含税单价-6000,详细说明-3000,产品类型-3000,供应商-3000,备注-3000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "产品导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "产品表.xls");
            }
            else
                return null;

        }
        public ActionResult AddInventoryAddPro()
        {
            tk_ProductInfo so = new TECOCITY_BGOI.tk_ProductInfo();
            //so.PID = InventoryMan.GetTopListOutID();
            //so.ProOutUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //添加货品
        public ActionResult SaveInventoryAddPro()
        {
            if (ModelState.IsValid)
            {
                tk_ProductInfo stockout = new tk_ProductInfo();
                stockout.PID = Request["PID"].ToString();
                stockout.MaterialNum = Request["MaterialNum"].ToString();
                stockout.Spec = Request["Spec"].ToString();
                stockout.ProName = Request["ProName"].ToString();
                stockout.ProTypeID = Request["ProTypeID"].ToString();
                stockout.UnitPrice = Convert.ToDecimal(Request["UnitPrice"].ToString());
                stockout.Units = Request["Units"].ToString();
                stockout.Ptype = Request["Ptype"].ToString();
                stockout.Manufacturer = Request["Manufacturer"].ToString();
                stockout.Remark = Request["Remark"].ToString();
                stockout.Detail = Request["Detail"].ToString();
                stockout.Price2 = Convert.ToDecimal(Request["Price2"].ToString());
                string strErr = "";
                bool b = InventoryMan.SaveInventoryAddPro(stockout, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, Msg = "操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }


        //添加货品(单个)
        public ActionResult SaveInventoryAddProNew()
        {
            if (ModelState.IsValid)
            {
                tk_ProductInfo stockout = new tk_ProductInfo();
                stockout.PID = Request["PID"].ToString();
                stockout.MaterialNum = Request["MaterialNum"].ToString();
                stockout.Spec = Request["Spec"].ToString();
                stockout.ProName = Request["ProName"].ToString();
                stockout.ProTypeID = Request["ProTypeID"].ToString();
                stockout.UnitPrice = Convert.ToDecimal(Request["UnitPrice"].ToString());
                stockout.Units = Request["Units"].ToString();
                stockout.Ptype = Request["Ptype"].ToString();
                //供应商转换
                DataTable dt = InventoryMan.GetMan();
                stockout.Manufacturer = Request["Manufacturer"].ToString();
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (stockout.Manufacturer == dt.Rows[j]["COMNameC"].ToString())
                        {
                            stockout.Manufacturer = "" + dt.Rows[j]["SID"].ToString() + "";
                        }
                    }
                }


                stockout.Remark = Request["Remark"].ToString();
                stockout.Detail = Request["Detail"].ToString();
                stockout.Price2 = Convert.ToDecimal(Request["Price2"].ToString());
                string strErr = "";
                bool b = InventoryMan.SaveInventoryAddProNew(stockout, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, Msg = "操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        #region [上传]
        // 上传文件至服务器项目文件夹下
        [HttpPost]
        public ActionResult InventoryAddPro(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["txtPath"];//获取上传的文件
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Pro\\ProName\\";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }
            ViewBag.error = "上传成功";
            ViewData["Path"] = savePath;

            // 年份
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            this.ViewBag.strDate = strDate;

            List<SelectListItem> itemsY = new List<SelectListItem>();
            int intNowYear = DateTime.Now.Year;
            itemsY = new List<SelectListItem>();
            itemsY.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            for (int i = 5; i >= 0; i--)
            {
                itemsY.Add(new SelectListItem { Text = (intNowYear - i).ToString(), Value = (intNowYear - i).ToString() });
            }
            this.ViewBag.Years = itemsY;

            List<SelectListItem> itemsM = new List<SelectListItem>();
            int intNowMonth = 12;
            itemsM = new List<SelectListItem>();
            itemsM.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            for (int i = 11; i >= 0; i--)
            {
                itemsM.Add(new SelectListItem { Text = (intNowMonth - i).ToString(), Value = (intNowMonth - i).ToString() });
            }
            this.ViewBag.Months = itemsM;

            return View();

        }

        // 检查数据库中数据是否重复 
        public ActionResult checkDataList()
        {
            string year = "";
            if (Request["year"] != null && Request["year"].ToString() != "")
                year = Request["year"].ToString();
            else
                year = DateTime.Now.Year.ToString();

            string month = "";
            if (Request["month"] != null && Request["month"].ToString() != "")
                month = Request["month"].ToString();
            else
                month = DateTime.Now.Month.ToString();

            int strJson = InventoryMan.checkDataList(year, month);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        // 保存上传数据 
        public ActionResult SavePlanData()
        {
            string strErr = "";
            // 上传信息：物料编码，物料长描述，供应商，计量单位，需要数量，计划单位，需要日期，备注，计划年，计划月，是否有效，创建时间
            //            序号,编号,零件名称,图号或规格,单台,数量,单 位,领出数量,更换数量,更换日期,签字,备注
            string strData = Request["strData"].ToString();

            bool bo = InventoryMan.SavePlanData(strData, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (!bo)
                    return Json(new { success = "false", Msg = "上传产品表单数据失败" });
                else
                    return Json(new { success = "true", SavePlanData = bo });
            }

        }

        #endregion
        //加载打印数据
        public ActionResult PrintInventoryAddPro()
        {
            string PID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(PID))
            {
                where += " where  PID = '" + PID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_ProductInfo ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_ProductInfo so = new tk_ProductInfo();
            foreach (DataRow dt in data.Rows)
            {
                so.PID = PID;
                so.ProName = dt["ProName"].ToString();
                so.MaterialNum = dt["MaterialNum"].ToString();
                so.Spec = dt["Spec"].ToString();
                so.UnitPrice = Convert.ToDecimal(dt["UnitPrice"]);
                so.Units = dt["Units"].ToString();
                so.Remark = dt["Units"].ToString();
                so.Detail = dt["Detail"].ToString();
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(PID))
            {
                wheretype = " where ID in(select Ptype from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_ConfigPType ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["Text"] = dtpt["Text"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(PID))
            {
                wherehouse = "where ID in(select ProTypeID from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigProType ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text1"] = dtpt["Text"].ToString();
            }

            string wherehouse1 = "";
            if (!string.IsNullOrEmpty(PID))
            {
                wherehouse1 = " where ProductID='" + PID + "'";
            }
            string prohouse1 = " BGOI_Inventory.dbo.tk_StockRemain ";
            DataTable dtprohouse1 = InventoryMan.PrintList(wherehouse1, prohouse1, ref strErr);
            foreach (DataRow dtpt in dtprohouse1.Rows)
            {
                ViewData["FinishCount"] = dtpt["FinishCount"].ToString();
            }

            string wherehouse2 = "";
            if (!string.IsNullOrEmpty(PID))
            {
                wherehouse2 = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockRemain where ProductID='" + PID + "' )and TypeID in (select ProTypeID from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "') ";
            }
            string prohouse2 = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprohouse2 = InventoryMan.PrintList(wherehouse2, prohouse2, ref strErr);
            foreach (DataRow dtpt in dtprohouse2.Rows)
            {
                ViewData["HouserName"] = dtpt["HouseName"].ToString();
            }
            return View(so);
        }
        #region [新增规格型号]
        public ActionResult AddSpec()
        {
            tk_ProductSpec so = new tk_ProductSpec();
            so.GGID = InventoryMan.GetTopGGID();
            return View(so);
        }
        //添加货品
        public ActionResult SaveAddSpec()
        {
            if (ModelState.IsValid)
            {
                tk_ProductSpec stockout = new tk_ProductSpec();
                stockout.Spec = Request["Spec"].ToString().Trim();
                stockout.GGID = Request["GGID"].ToString().Trim();
                string strErr = "";
                bool b = InventoryMan.SaveAddSpec(stockout, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, Msg = "操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        #endregion
        #endregion
        #region [成品定义]
        public ActionResult DefinitionOfProduct()
        {
            return View();
        }
        // 保存上传数据 
        public ActionResult SaveDefinitionOfProduct()
        {
            string strErr = "";
            //货品唯一编号	组装该成品的零件PID	需零件数量	规格型号
            string strData = Request["strData"].ToString();

            bool bo = InventoryMan.SaveDefinitionOfProduct(strData, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (!bo)
                    return Json(new { success = "false", Msg = "上传成品表单数据失败" });
                else
                    return Json(new { success = "true", SavePlanData = bo });
            }

        }
        //加载货品列表
        public ActionResult DefinitionOfProductList(InventoryAddProQuery addpro)
        {
            if (ModelState.IsValid)
            {
                string where = " a.ValiDate='v'and b.ProTypeID='2' and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                //string ProTypeID = Request["ProTypeID"].ToString().Trim(); ;
                string PID = addpro.PID;//Request["PID"].ToString().Trim();
                string ProName = addpro.ProName;// Request["ProName"].ToString().Trim();
                // string Spec = Request["Spec"].ToString().Trim();
                //where += " d.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";

                //if (ProTypeID != "")
                //    where += " b.ProTypeID='" + ProTypeID + "' and";
                if (Request["PID"] != "")
                    where += " a.ProductID like '%" + Request["PID"] + "%' and";
                if (Request["ProName"] != "")
                    where += " b.ProName like '%" + Request["ProName"] + "%' and";
                //if (Spec != "")
                //    where += " b.Spec like '" + Spec + "' and";

                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " ";
                }
                else
                {
                    where += " c.UnitID='" + unitid + "'  and";
                }
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.DefinitionOfProductList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }


        //发货单导出
        public FileResult DefinitionOfProductListToExcel()
        {
            string where = " a.ValiDate='v'and b.ProTypeID='2' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var PIDN = Request["PIDN"].ToString();
            if (PIDN != "")
            {
                var str = PIDN.Remove(PIDN.Length - 1, 1);
                where += "  a.ProductID in (" + str + ")  and";
            }
            string PID = Request["PID"].ToString().Trim();
            string ProName = Request["ProName"].ToString().Trim();
            if (PID != "")
                where += " a.ProductID like '%" + PID + "%' and";
            if (ProName != "")
                where += " b.ProName like '%" + ProName + "%' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " ";
            }
            else
            {
                where += " c.UnitID='" + unitid + "'  ";
            }
            where += " group by ProductID,b.ProName,b.Spec,b.MaterialNum,b.Remark,a.State   ";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_ProFinishDefine a " +
                              " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID  " +
                              " left join BGOI_Inventory.dbo.tk_ConfigPType c on b.Ptype=c.ID ";
            FieldName = " ProductID,b.ProName,b.Spec,b.MaterialNum,b.Remark,(case when  a.State ='0'then '未添加可生产' else '已添加可生产' end) as State  ";
            string OrderBy = " a.ProductID ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "货品编号-3000,货品名称-5000,规格型号-6000,物料号-5000,备注-3000,";
                strCols += "状态-3000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "成品定义导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "成品定义表.xls");
            }
            else
                return null;

        }
        //加载打印数据
        public ActionResult PrintDefinitionOfProduct()
        {
            string PID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(PID))
            {
                where += " where  PID = '" + PID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_ProductInfo ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_ProductInfo so = new tk_ProductInfo();
            foreach (DataRow dt in data.Rows)
            {
                so.PID = PID;
                so.ProName = dt["ProName"].ToString();
                so.UnitPrice = Convert.ToDecimal(dt["UnitPrice"]);
                so.Spec = dt["Spec"].ToString();
                so.Units = dt["Units"].ToString();
                //ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
            }
            return View(so);
        }
        //加载零件物料信息
        public ActionResult PrictDefinitionOf()
        {
            string PID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(PID))
            {
                s += " b.ProductID like '%" + PID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";
            //ProductID,ProName,Spec,Number,Units,UnitPrice
            string tableName = " BGOI_Inventory.dbo.tk_ProductInfo a " +
                               " left join BGOI_Inventory.dbo.tk_ProFinishDefine b on a.PID=b.PartPID ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        /// <summary>
        /// 加载零件信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DefinitionOfList()
        {
            string where = " ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PID = Request["PID"].ToString();
            //if (PID != "")
            //{
            where += "  and b.ProductID='" + PID + "' ";
            //}
          //  where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = InventoryMan.DefinitionOfList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddDefinitionOfProduct()
        {
            return View();
        }
        public ActionResult SaveAddDefinitionOfProduct()
        {
            if (ModelState.IsValid)
            {
                string type = Request["type"].ToString();
                string ProductID = Request["ProductID"].ToString();
                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrCount = Request["StockInCount"].Split(',');
                string[] arrstrBiaoShi = Request["strBiaoShi"].Split(',');
                string strErr = "";

                tk_ProFinishDefine stockin = new tk_ProFinishDefine();
                List<tk_ProFinishDefine> detailList = new List<tk_ProFinishDefine>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    stockin = new tk_ProFinishDefine();
                    stockin.ValiDate = "v";
                    stockin.State = "0";
                    stockin.PartPID = arrPID[i].ToString();
                    stockin.Number = Convert.ToInt32(arrCount[i]);
                    stockin.strIdentitySharing = arrstrBiaoShi[i].ToString();
                    string str = arrstrBiaoShi[i].ToString();
                    if (str != "")
                    {
                        for (int j = 0; j < arrstrBiaoShi.Length; j++)
                        {
                            if (arrstrBiaoShi[j].ToString() != "")
                            {
                                if (str == arrstrBiaoShi[j].ToString())
                                {
                                    stockin.IdentifierStr += (arrPID[j] + ",").ToString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    detailList.Add(stockin);
                }
                if (type == "1")
                {
                    bool b = InventoryMan.SaveAddDefinitionOfProduct(detailList, type, ProductID, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加成品";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_ProFinishDefine";
                        log.Typeid = Request["ProductID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加成品";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_ProFinishDefine";
                        log.Typeid = Request["ProductID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else
                {
                    bool b = InventoryMan.SaveAddDefinitionOfProduct(detailList, type, ProductID, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "修改成品";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_ProFinishDefine";
                        log.Typeid = Request["ProductID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "修改成品";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_ProFinishDefine";
                        log.Typeid = Request["ProductID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeDefinitionOfProduct()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();

            if (InventoryMan.DeDefinitionOfProduct(PID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销成品";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ProFinishDefine";
                log.Typeid = Request["PID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销成品";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ProFinishDefine";
                log.Typeid = Request["PID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //添加可生产
        public ActionResult AddProFin()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();

            if (InventoryMan.AddProFin(PID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加可生产";
                log.LogContent = "添加成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ProductionOfFinishedGoods";
                log.Typeid = Request["PID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "添加成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加可生产";
                log.LogContent = "添加失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ProductionOfFinishedGoods";
                log.Typeid = Request["PID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //修改
        public ActionResult UpDefinitionOfProduct()
        {
            return View();
        }
        //修改加载数据(下拉框)
        public ActionResult GetUpDefinitionOfProduct()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.GetUpDefinitionOfProduct(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //修改加载数据（零件）
        public ActionResult GetUpXian()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.GetUpXian(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        #region [根据产品名称加载规格和编号下拉框内容]
        //根据产品名称加载规格
        public ActionResult GetProNameToSpec()
        {
            string ProName = Request["ProName"].ToString();
            DataTable dt = InventoryMan.GetProNameToSpec(ProName);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据产品名称加载规格
        public ActionResult GetPIDToSpec()
        {
            string ProductID = Request["ProductID"].ToString();
            DataTable dt = InventoryMan.GetPIDToSpec(ProductID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据产品名称加载编号
        public ActionResult GetProNameToPID()
        {
            string ProName = Request["ProName"].ToString();
            DataTable dt = InventoryMan.GetProNameToPID(ProName);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #endregion
        #region [库存管理]
        #region [库存账单]
        public ActionResult InventoryBill()
        {
            return View();
        }
        public ActionResult InventoryBillList(InventoryBillQuery query)
        {
            string where = " and f.Validate='v'and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string Spec = Request["Spec"].ToString();
            string PID = Request["PID"].ToString().Trim();
            string ProName = Request["ProName"].ToString().Trim();

            string PID1 = query.PID;// Request["PID"].ToString().Trim();
            string ProName1 = query.ProName;// Request["ProName"].ToString().Trim();
            string ListInID = query.ListInID;// Request["ListInID"].ToString().Trim();


            string ListID = Request["ListID"].ToString().Trim();
            // string ListOutID =  Request["ListOutID"].ToString().Trim();
            string SingleLibrary = Request["SingleLibrary"].ToString().Trim();

            //if (Spec != "")
            //{
            //    where += " g.Spec  like '%" + Spec + "%' and";
            //}
            //if (Request["PID"].ToString().Trim() != "")
            //{
            //    where += " a.ProductID like '%" + Request["PID"].ToString().Trim() + "%' and";
            //}
            //if (Request["ProName"].ToString().Trim() != "")
            //{
            //    where += " g.ProName  like '%" + Request["ProName"].ToString().Trim() + "%' and";
            //}
            if (where != "")
            {
                where = where.Substring(0, where.Length - 3);
            }
            DataTable dt = InventoryMan.InventoryBillList(start, end, where, PID, Spec, ProName, SingleLibrary, ListID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //加载打印数据
        public ActionResult PrintInventoryBill()
        {
            return View();
        }
        #endregion
        #region //[货品库存]

        //加载打印数据
        public ActionResult PrintInventoryFirstPage()
        {
            string PID = Request["Info"];
            string Text = Request["Text"];
            ViewData["Text"] = Text;
            string HouseName = Request["HouseName"];
            ViewData["HouseName"] = HouseName;
            string unitid = GAccount.GetAccountInfo().UnitID;
            string where = "";
            if (!string.IsNullOrEmpty(PID))
            {
                where += " where  ProductID = '" + PID + "' ";
                if (unitid == "46" || unitid == "32")
                {
                    where += " and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse where TypeID in(select ID from BGOI_Inventory.dbo.tk_ConfigProType where Text='" + Text + "' ) and HouseName='" + HouseName + "'  and  HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "')) ";
                }
                else
                {
                    where += " and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and TypeID in(select ID from BGOI_Inventory.dbo.tk_ConfigProType where Text='" + Text + "' ) and HouseName='" + HouseName + "'  and  HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "')) ";
                }

            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockRemain ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockRemain so = new TECOCITY_BGOI.tk_StockRemain();
            foreach (DataRow dt in data.Rows)
            {
                if (dt["OnlineCount"].ToString() == "")
                {
                    so.OnlineCount = 0;
                }
                else
                {
                    so.OnlineCount = Convert.ToInt32(dt["OnlineCount"]);
                }
                so.Location = dt["Location"].ToString();
                so.UsableStock = dt["UsableStock"].ToString();
                so.Costing = dt["Costing"].ToString();
                so.FinishCount = Convert.ToInt32(dt["FinishCount"]);
            }
            ViewData["PID"] = PID;
            string pro = " BGOI_Inventory.dbo.tk_ProductInfo ";
            DataTable dtpro = InventoryMan.PrintList("where  PID = '" + PID + "' ", pro, ref strErr);
            tk_ProductInfo proc = new TECOCITY_BGOI.tk_ProductInfo();
            foreach (DataRow dtp in dtpro.Rows)
            {
                ViewData["ProName"] = dtp["ProName"].ToString();
                ViewData["Spec"] = dtp["Spec"].ToString();
            }
            //string wheretype = "";
            //if (!string.IsNullOrEmpty(PID))
            //{
            //    if (unitid == "46" || unitid == "32")
            //    {
            //        wheretype = "where ID IN (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where  HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "'))";
            //    }
            //    else
            //    {
            //        wheretype = "where ID IN (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "'))";
            //    }

            //}
            //string protype = " BGOI_Inventory.dbo.tk_ConfigProType ";
            //DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            //foreach (DataRow dtpt in dtprotype.Rows)
            //{
            //    ViewData["Text"] = dtpt["Text"].ToString();
            //}
            //string wherehouse = "";
            //if (!string.IsNullOrEmpty(PID))
            //{
            //    if (unitid == "46" || unitid == "32")
            //    {
            //        wherehouse = "where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "')";
            //    }
            //    else
            //    {
            //        wherehouse = "where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and HouseID in (select HouseID from BGOI_Inventory.dbo.tk_StockRemain  where ProductID='" + PID + "')";
            //    }

            //}
            //string prohouse = " BGOI_Inventory.dbo.tk_WareHouse ";
            //DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            //foreach (DataRow dtpt in dtprohouse.Rows)
            //{
            //    ViewData["HouseName"] = dtpt["HouseName"].ToString();
            //}
            return View(so);
        }

        public ActionResult InventoryFirstPage()
        {
            return View();
        }
        public ActionResult InventoryFirstPagetwo()
        {
            string ProType = Request["ProType"].ToString();
            DataTable dt = InventoryMan.GetConfigInfo(ProType);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = Dt2String(dt, "") });
        }
        public ActionResult StockRemainList(InventoryFirstPageQuery firstquery)
        {
            if (ModelState.IsValid)
            {
                string where = " d.Validate='v'  and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ProType = Request["ProType"].ToString();
                string PID = firstquery.PID;
                string ProName = firstquery.ProName;
                string Spec = Request["Spec"].ToString();
                string UnitID = Request["UnitID"].ToString();
                if (UnitID == "")
                {
                    UnitID = GAccount.GetAccountInfo().UnitID;
                }
                if (ProType != "")
                    where += " c.Text='" + ProType + "' and";
                if (Request["PID"] != "")
                    where += " PID like '%" + Request["PID"] + "%' and";
                if (Request["ProName"] != "")
                    where += " b.ProName like '%" + Request["ProName"] + "%' and";
                if (Spec != "")
                    where += " b.Spec like '%" + Spec + "%' and";
                if (Request["IsHouseIDone"] != "0")
                    where += " d.HouseID='" + Request["IsHouseIDone"] + "' and";
                if (Request["IsHouseIDtwo"] != "0")
                    where += " d.HouseID='" + Request["IsHouseIDtwo"] + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.StockRemainListnew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, PID, UnitID);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //货品导出
        public FileResult InventoryToExcel()
        {
            string where = " d.Validate='v'  and d.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ProType = Request["ProType"].ToString();

            var PIDN = Request["PIDN"].ToString();
            if (PIDN != "")
            {
                var str = PIDN.Remove(PIDN.Length - 1, 1);
                where += "  PID in (" + str + ")  and";
            }
            string PID = Request["PIDCX"].ToString();
            string ProName = Request["ProName"].ToString();
            string Spec = Request["Spec"].ToString();
            string UnitID = Request["HouseID"].ToString();
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (UnitID == "")
            {
                UnitID = unitid;
            }
            if (ProType != "")
                where += " c.OID='" + ProType + "' and";
            if (PID != "")
                where += " PID like '%" + PID + "%' and";

            if (ProName != "")
                where += " b.ProName like '%" + ProName + "%' and";
            if (Spec != "")
                where += " b.Spec like '%" + Spec + "%' and";
            if (Request["IsHouseIDoneto"] != "0")
                where += " d.HouseID='" + Request["IsHouseIDoneto"] + "' and";
            if (Request["IsHouseIDtwoto"] != "0")
                where += " d.HouseID='" + Request["IsHouseIDtwoto"] + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockRemain a" +
                                  " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID" +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID" +
                                  " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in" +
                                  " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where HouseID in" +
                                  " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c on d.TypeID=c.ID";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockRemain a" +
                                    " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID" +
                                    " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID " +
                                    " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in" +
                                    " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "' and HouseID in" +
                                    " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c on d.TypeID=c.ID ";// where ProductID='" + PID + "'
            }
            FieldName = "  PID,b.ProName,b.Spec,a.FinishCount,a.OnlineCount,a.Location,d.HouseName   ";
            string OrderBy = " a.ID ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "货品编号-3000,货品名称-5000,规格型号-6000,库存数量-5000,在线库存-3000,";
                strCols += "存放位置-3000,所属仓库-6000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "产品库存导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "产品库存表.xls");
            }
            else
                return null;

        }
        #endregion
        #endregion
        #region [货品入库]

        #region//[采购入库]
        public ActionResult ProcureStockIn()
        {
            return View();
        }
        public ActionResult StockInList(ProcureStockInQuery proin)
        {

            if (ModelState.IsValid)
            {
                string where = " a.Type='采购' and  a.ValiDate='v'  and";
                if (GAccount.GetAccountInfo().UnitID == "46" || GAccount.GetAccountInfo().UnitID == "32")
                {
                    where += "";
                }
                else
                {
                    where += " e.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string BatchID = proin.BatchID;// Request["BatchID"].ToString();
                string ListInID = proin.ListInID;// Request["ListInID"].ToString();
                //string HouseID = Request["HouseID"].ToString();
                string HouseID = "";
                string ProType = Request["ProType"].ToString();
                string Spec = Request["Spec"].ToString().Replace(" ", "");
                if (Request["IsHouseIDone"] != "0")
                {
                    HouseID = Request["IsHouseIDone"].ToString();
                }
                if (Request["IsHouseIDtwo"] != "0")
                {
                    HouseID = Request["IsHouseIDtwo"].ToString();
                }
                if (Spec != "")
                    where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo where Spec like '%" + Spec + "%')) and";
                if (ProType != "")
                    where += " f.OID='" + ProType + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["BatchID"] != "")
                    where += " a.BatchID like'%" + Request["BatchID"] + "%' and";
                if (Request["ListInID"] != "")
                    where += " a.ListInID like '%" + Request["ListInID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.StockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public FileResult ProcureStockInToExcelFZ()
        {
            string where = " a.Type='采购' and  a.ValiDate='v'  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "  a.ListInID in (" + str + ")  and";
            }
            string IsHouseIDoneto = Request["IsHouseIDoneto"].ToString();
            string IsHouseIDtwoto = Request["IsHouseIDtwoto"].ToString();

            string BatchID = Request["BatchID"].ToString();
            string ListInID = Request["ListInID"].ToString();
            string HouseID = "";
            string ProType = Request["ProType"].ToString();
            string Spec = Request["Spec"].ToString().Replace(" ", "");
            if (IsHouseIDoneto != "0")
            {
                HouseID = IsHouseIDoneto;
            }
            if (IsHouseIDtwoto != "0")
            {
                HouseID = IsHouseIDtwoto;
            }
            if (Spec != "")
                where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
            if (ProType != "")
                where += " f.OID='" + ProType + "' and";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (BatchID != "")
                where += " a.BatchID like'%" + BatchID + "%' and";
            if (ListInID != "")
                where += " a.ListInID like '%" + ListInID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            if (unitid == "47")
            {
                FieldName = "  a.ListInID,a.BatchID,a.HandwrittenAccount,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            else
            {
                FieldName = "  a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-5000,科目-5000,入库时间-6000,入库操作员-6000,总金额-5000,";
                strCols += "产品库类型-5000,所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "采购入库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "采购入库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddProcureStockIn()
        {
            StockIn si = new TECOCITY_BGOI.StockIn();
            si.ListInID = InventoryMan.GetTopListInID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            ViewData["StockInTime"] = DateTime.Now;
            si.BatchID = InventoryMan.GetTopHandwrittenAccount();
            return View(si);
        }

        public ActionResult GetConfigInfo()
        {
            string taskType = Request["TaskType"].ToString();
            DataTable dt = InventoryMan.GetConfigInfo(taskType);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = Dt2String(dt, "Text") });
        }

        public static string Dt2String(DataTable a_dtValue, string a_strColumnName)
        {
            if (a_dtValue == null) return "";

            string strInfo = "";
            for (int i = 0; i < a_dtValue.Rows.Count; i++)
            {
                if (strInfo != "") strInfo += ",";
                strInfo += a_dtValue.Rows[i][a_strColumnName].ToString();
            }
            return strInfo;
        }

        public ActionResult SaveStockIn()
        {
            StockIn stockin = new StockIn();
            stockin.ListInID = Request["ListInID"].ToString();
            stockin.SubjectID = Request["SubjectID"].ToString();
            stockin.BatchID = Request["BatchID"].ToString();
            stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
            stockin.HouseID = Request["HouseID"].ToString();
            stockin.ProPlace = Request["ProPlace"].ToString();
            stockin.StockInUser = Request["LoginName"].ToString();
            stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
            stockin.State = 0;
            stockin.ValiDate = "v";
            stockin.Type = "采购";
            stockin.ApplyPer = "";
            stockin.ApplyTime = DateTime.Now;

            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrProName = Request["ProName"].Split(',');
            string[] arrSpec = Request["SpecsModels"].Split(',');
            string[] arrUnitName = Request["UnitName"].Split(',');
            string[] arrCount = Request["StockInCount"].Split(',');
            string[] arrPrice = Request["UnitPrice"].Split(',');
            string[] arrAmount = Request["TotalAmount"].Split(',');
            string[] arrMan = Request["Manufacturer"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');

            string strErr = "";
            StockInDetail deInfo = new StockInDetail();
            List<StockInDetail> detailList = new List<StockInDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new StockInDetail();
                deInfo.ListInID = Request["ListInID"].ToString();
                deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                deInfo.MainContent = arrMain[i].ToString();
                deInfo.ProductID = arrPID[i].ToString();
                deInfo.ProName = arrProName[i].ToString();
                deInfo.Spec = arrSpec[i].ToString();
                deInfo.Units = arrUnitName[i].ToString();
                deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                deInfo.Manufacturer = arrMan[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();

                detailList.Add(deInfo);
            }

            bool b = InventoryMan.AddRecordInfo(stockin, detailList, ref strErr);
            if (b)
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加采购入库";
                log.LogContent = "添加成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockIn";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加采购入库";
                log.LogContent = "添加失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockIn";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }

        public string ProStockInDetialNum(string ListInID)
        {
            return InventoryMan.ProStockInDetialNum(ListInID);
        }

        public ActionResult StockInDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ListInID = Request["ListInID"].ToString();


            UIDataTable udtTask = InventoryMan.StockInDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ListInID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpDateState()
        {
            string ListInID = Request["ListInID"].ToString();

            if (InventoryMan.UpDateState(ListInID))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "入库成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = "入库失败" });
            }
        }

        public ActionResult ChangeProcure()
        {
            return View();

        }

        public ActionResult ChangeProcureList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ChangeProcureList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProcureDetail()
        {
            string SHID = Request["SHID"].ToString();
            string RKID = Request["RKID"].ToString();
            DataTable dt = InventoryMan.GetProcureDetail(SHID, RKID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveProcureStockIn()
        {
            if (ModelState.IsValid)
            {
                StockIn stockin = new StockIn();
                stockin.ListInID = Request["ListInID"].ToString();
                if (GAccount.GetAccountInfo().UnitID == "47")
                {
                    stockin.HandwrittenAccount = Request["HandwrittenAccount"].ToString();
                    stockin.SubjectID = "S00001";

                }
                else
                {
                    stockin.HandwrittenAccount = "";
                    stockin.SubjectID = Request["SubjectID"].ToString();
                }
                stockin.BatchID = Request["BatchID"].ToString();
                stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
                stockin.HouseID = Request["HouseID"].ToString();
                stockin.ProPlace = Request["ProPlace"].ToString();
                stockin.StockInUser = Request["LoginName"].ToString();
                stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockin.State = 0;
                stockin.ValiDate = "v";
                stockin.Type = "采购";
                string SHID = Request["SHID"].ToString();
                stockin.DrawID = Request["DrawID"].ToString();
                stockin.Remark = Request["Remark1"].ToString();
                stockin.ApplyPer = "";
                stockin.ApplyTime = DateTime.Now;



                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockInCount"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');
                string[] arrSHID = Request["SHID"].Split(',');
                string strErr = "";

                StockInDetail deInfo = new StockInDetail();
                List<StockInDetail> detailList = new List<StockInDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockInDetail();
                    deInfo.ListInID = Request["ListInID"].ToString();
                    //deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                    deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();
                    deInfo.BatchNumber = Request["BatchID"].ToString();
                    deInfo.UpState = "0";
                    deInfo.Numupdate = 0;
                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveProcureStockIn(stockin, SHID, detailList, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加采购详细入库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockInDetail";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加采购详细入库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockInDetail";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }



        }
        //加载打印数据
        public ActionResult PrintProcureStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                where += " where  ListInID = '" + ListInID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockIn so = new tk_StockIn();
            foreach (DataRow dt in data.Rows)
            {
                so.strStockID = ListInID;
                so.strStockInTime = dt["StockInTime"].ToString();

                ViewData["year"] = Convert.ToDateTime(dt["StockInTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["StockInTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["StockInTime"]).Day.ToString();//日

                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["StockInUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName;// dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion

        #region//[退货检验入库]

        public ActionResult TestStockIn()
        {
            return View();
        }

        public ActionResult TestStockInList(TestStockInQuery testin)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Type='退货检验' and a.ValiDate='v' and";
                if (GAccount.GetAccountInfo().UnitID == "46" || GAccount.GetAccountInfo().UnitID == "32")
                {
                    where += "";
                }
                else
                {
                    where += " e.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                }

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string BatchID = testin.BatchID;// Request["BatchID"].ToString();
                string ListInID = testin.ListInID;// Request["ListInID"].ToString();
                //string HouseID = Request["HouseID"].ToString();
                string HouseID = "";
                string ProType = Request["ProType"].ToString();
                string Spec = Request["Spec"].ToString().Replace(" ", "");
                if (Request["IsHouseIDone"] != "0")
                {
                    HouseID = Request["IsHouseIDone"].ToString();
                }
                if (Request["IsHouseIDtwo"] != "0")
                {
                    HouseID = Request["IsHouseIDtwo"].ToString();
                }
                if (Spec != "")
                    where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo where Spec like '%" + Spec + "%')) and";
                if (ProType != "")
                    where += " f.OID='" + ProType + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["BatchID"] != "")
                    where += " a.BatchID like'%" + Request["BatchID"] + "%' and";
                if (Request["ListInID"] != "")
                    where += " a.ListInID like '%" + Request["ListInID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.TestStockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public FileResult TestStockInListToExcelFZ()
        {
            string where = " a.Type='退货检验' and  a.ValiDate='v'  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "  a.ListInID in (" + str + ")  and";
            }

            string IsHouseIDoneto = Request["IsHouseIDoneto"].ToString();
            string IsHouseIDtwoto = Request["IsHouseIDtwoto"].ToString();

            string BatchID = Request["BatchID"].ToString();
            string ListInID = Request["ListInID"].ToString();
            string HouseID = "";
            string ProType = Request["ProType"].ToString();
            string Spec = Request["Spec"].ToString().Replace(" ", "");
            if (IsHouseIDoneto != "0")
            {
                HouseID = IsHouseIDoneto;
            }
            if (IsHouseIDtwoto != "0")
            {
                HouseID = IsHouseIDtwoto;
            }
            if (Spec != "")
                where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
            if (ProType != "")
                where += " f.OID='" + ProType + "' and";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (BatchID != "")
                where += " a.BatchID like'%" + BatchID + "%' and";
            if (ListInID != "")
                where += " a.ListInID like '%" + ListInID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            if (unitid == "47")
            {
                FieldName = "  a.ListInID,a.BatchID,a.HandwrittenAccount,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            else
            {
                FieldName = "  a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-5000,科目-5000,入库时间-6000,入库操作员-6000,总金额-5000,";
                strCols += "产品库类型-5000,所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "退货检验入库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "退货检验入库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddTestStockIn()
        {
            StockIn si = new TECOCITY_BGOI.StockIn();
            si.ListInID = InventoryMan.GetTopListInID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            si.BatchID = InventoryMan.GetTopHandwrittenAccount();
            return View(si);
        }

        public ActionResult ChangeTest()
        {
            return View();
        }

        public ActionResult ChangeTestList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ChangeTestList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetSalesReturnTask()
        {
            string EID = Request["eid"].ToString();
            DataTable dt = InventoryMan.GetSalesReturnTask(EID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveTestStockIn()
        {
            if (ModelState.IsValid)
            {
                StockIn stockin = new StockIn();
                stockin.ListInID = Request["ListInID"].ToString();

                if (GAccount.GetAccountInfo().UnitID == "47")
                {
                    stockin.HandwrittenAccount = Request["HandwrittenAccount"].ToString();
                    stockin.SubjectID = "S00001";
                }
                else
                {
                    stockin.HandwrittenAccount = "";
                    stockin.SubjectID = Request["SubjectID"].ToString();
                }
                stockin.BatchID = Request["BatchID"].ToString();
                stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
                stockin.HouseID = Request["HouseID"].ToString();
                stockin.ProPlace = Request["ProPlace"].ToString();
                stockin.StockInUser = Request["LoginName"].ToString();
                stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockin.State = 0;
                stockin.ValiDate = "v";
                stockin.Type = "退货检验";
                string TID = Request["TID"].ToString();
                stockin.DrawID = Request["DrawID"].ToString();
                stockin.Remark = Request["Remark1"].ToString();
                stockin.ApplyPer = "";
                stockin.ApplyTime = DateTime.Now;
                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockInCount"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["TID"].Split(',');

                string strErr = "";
                StockInDetail deInfo = new StockInDetail();
                List<StockInDetail> detailList = new List<StockInDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockInDetail();
                    deInfo.ListInID = Request["ListInID"].ToString();
                    deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                    deInfo.MainContent = arrMain[i].ToString();
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();
                    deInfo.BatchNumber = Request["BatchID"].ToString();
                    deInfo.UpState = "0";
                    deInfo.Numupdate = 0;
                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.AddTestStockIn(stockin, TID, detailList, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加退货检验入库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加退货检验入库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }




        }

        //加载打印数据
        public ActionResult PrintTestStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                where += " where  ListInID = '" + ListInID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockIn so = new tk_StockIn();
            foreach (DataRow dt in data.Rows)
            {
                so.strStockID = ListInID;
                ViewData["strStockInTime"] = Convert.ToDateTime(dt["StockInTime"]).ToString("yyy/mm/dd");

                ViewData["year"] = Convert.ToDateTime(dt["StockInTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["StockInTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["StockInTime"]).Day.ToString();//日

                //so.strStockInTime = dt["StockInTime"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["StockInUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName;// dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion

        #region //[撤样机入库]

        public ActionResult ProtoStockIn()
        {
            return View();
        }

        public ActionResult ProtoStockInList(ProtoStockInQuery prostoin)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Type='撤样机' and  a.ValiDate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string ApplyPer = prostoin.ApplyPer;// Request["BatchID"].ToString();
                string ListInID = prostoin.ListInID;// Request["ListInID"].ToString();
                //string HouseID = Request["HouseID"].ToString();
                string HouseID = "";
                string ProType = Request["ProType"].ToString();
                string Spec = Request["Spec"].ToString().Replace(" ", "");
                if (Request["IsHouseIDone"] != "0")
                {
                    HouseID = Request["IsHouseIDone"].ToString();
                }
                if (Request["IsHouseIDtwo"] != "0")
                {
                    HouseID = Request["IsHouseIDtwo"].ToString();
                }
                if (Spec != "")
                    where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo where Spec like '%" + Spec + "%')) and";
                if (ProType != "")
                    where += " f.OID='" + ProType + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ApplyPer"] != "")
                    where += " a.ApplyPer like'%" + Request["ApplyPer"] + "%' and";
                if (Request["ListInID"] != "")
                    where += " a.ListInID like '%" + Request["ListInID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.ProtoStockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }




        }
        public FileResult ProtoStockInListToExcelFZ()
        {
            string where = " a.Type='撤样机' and  a.ValiDate='v'  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "  a.ListInID in (" + str + ")  and";
            }
            string IsHouseIDoneto = Request["IsHouseIDoneto"].ToString();
            string IsHouseIDtwoto = Request["IsHouseIDtwoto"].ToString();

            string ApplyPer = Request["ApplyPer"].ToString();
            string ListInID = Request["ListInID"].ToString();
            string HouseID = "";
            string ProType = Request["ProType"].ToString();
            string Spec = Request["Spec"].ToString().Replace(" ", "");
            if (IsHouseIDoneto != "0")
            {
                HouseID = IsHouseIDoneto;
            }
            if (IsHouseIDtwoto != "0")
            {
                HouseID = IsHouseIDtwoto;
            }
            if (Spec != "")
                where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
            if (ProType != "")
                where += " f.OID='" + ProType + "' and";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (Request["ApplyPer"] != "")
                where += " a.ApplyPer like'%" + Request["ApplyPer"] + "%' and";
            if (ListInID != "")
                where += " a.ListInID like '%" + ListInID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            if (unitid == "47")
            {
                FieldName = "  a.ListInID,a.BatchID,a.HandwrittenAccount,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            else
            {
                FieldName = "  a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-5000,科目-5000,入库时间-6000,入库操作员-6000,总金额-5000,";
                strCols += "产品库类型-5000,所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "撤样机入库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "撤样机入库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddProtoStockIn()
        {
            StockIn si = new TECOCITY_BGOI.StockIn();
            si.ListInID = InventoryMan.GetTopListInID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            ViewData["ApplyTime"] = DateTime.Now;
            ViewData["StockInTime"] = DateTime.Now;
            si.BatchID = InventoryMan.GetTopHandwrittenAccount();
            return View(si);
        }

        public ActionResult ChangeProto()
        {
            return View();
        }

        public ActionResult ChangeProtoList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ChangeProtoList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProtoDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetProtoDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveProtoStockIn()
        {
            if (ModelState.IsValid)
            {
                StockIn stockin = new StockIn();
                stockin.ListInID = Request["ListInID"].ToString().Trim();
                stockin.SubjectID = Request["SubjectID"].ToString().Trim();
                stockin.BatchID = Request["BatchID"].ToString().Trim();
                stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
                stockin.HouseID = Request["HouseID"].ToString().Trim();
                stockin.ProPlace = Request["ProPlace"].ToString().Trim();
                stockin.StockInUser = Request["LoginName"].ToString().Trim();
                stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockin.State = 0;
                stockin.ValiDate = "v";
                stockin.Type = "撤样机";
                string DID = Request["DID"].ToString().Trim();
                stockin.DrawID = Request["DrawID"].ToString().Trim();
                stockin.Remark = Request["Remark1"].ToString().Trim();
                stockin.ApplyPer = Request["ApplyPer"].ToString().Trim();
                stockin.ApplyTime = Convert.ToDateTime(Request["ApplyTime"].ToString());
                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                // string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockInCount"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');
                string[] arrDID = Request["DID"].Split(',');
                string strErr = "";
                StockInDetail deInfo = new StockInDetail();
                List<StockInDetail> detailList = new List<StockInDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockInDetail();
                    deInfo.ListInID = Request["ListInID"].ToString();
                    deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    //  deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrDID[i].ToString();
                    deInfo.BatchNumber = Request["BatchID"].ToString();
                    deInfo.UpState = "0";
                    deInfo.Numupdate = 0;
                    detailList.Add(deInfo);
                }
                bool b = InventoryMan.SaveProtoStockIn(stockin, DID, detailList, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加撤样机入库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加撤样机入库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }

        public ActionResult UpProtoDateState()
        {
            string ListInID = Request["ListInID"].ToString();

            if (InventoryMan.UpProtoDateState(ListInID))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "入库成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = "入库失败" });
            }
        }

        //加载打印数据
        public ActionResult PrintProtoStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                where += " where  ListInID = '" + ListInID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockIn so = new tk_StockIn();
            foreach (DataRow dt in data.Rows)
            {
                so.strStockID = ListInID;
                so.strStockInTime = Convert.ToDateTime(dt["StockInTime"]).ToString("yyyy/MM/dd"); //dt["StockInTime"].ToString();//"yyyy/MM/dd"
                ViewData["year"] = Convert.ToDateTime(dt["StockInTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["StockInTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["StockInTime"]).Day.ToString();//日

                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["StockInUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
                ViewData["ApplyPer"] = dt["ApplyPer"].ToString();
                ViewData["ApplyTime"] = Convert.ToDateTime(dt["ApplyTime"]).ToString("yyyy/MM/dd"); //dt["ApplyTime"].ToString();//

            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            //string wherehouse = "";
            //if (!string.IsNullOrEmpty(ListInID))
            //{
            //    wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            //}
            //string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            //DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            //foreach (DataRow dtpt in dtprohouse.Rows)
            //{
            //    ViewData["Text"] = dtpt["Text"].ToString();
            //}
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion

        #region //基本入库

        public ActionResult BasicStockIn()
        {
            return View();
        }
        //成长详细
        public ActionResult WarehousingCost()
        {
            return View();
        }
        public ActionResult WarehousingCostList(BasicStockInQuery stockkin)
        {
            if (ModelState.IsValid)
            {
                string where = "   ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.WarehousingCostList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult BasicStockInList(BasicStockInQuery stockkin)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Type='基本' and  a.ValiDate='v'  and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string BatchID = stockkin.BatchID;// Request["BatchID"].ToString();
                string ListInID = stockkin.ListInID;// Request["ListInID"].ToString();
                string HouseID = "";

                string ProType = Request["ProType"].ToString();
                string Spec = Request["Spec"].ToString().Replace(" ", "");
                if (Request["IsHouseIDone"] != "0")
                {
                    HouseID = Request["IsHouseIDone"].ToString();
                }
                if (Request["IsHouseIDtwo"] != "0")
                {
                    HouseID = Request["IsHouseIDtwo"].ToString();
                }
                if (Spec != "")
                    where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
                // where += " replace(c.Spec,' ','')='" + Spec + "' and";
                if (ProType != "")
                    where += " f.OID='" + ProType + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["BatchID"].ToString().Trim() != "")
                    where += " a.BatchID like'%" + Request["BatchID"].ToString().Trim() + "%' and";
                if (Request["ListInID"].ToString().Trim() != "")
                    where += " a.ListInID like '%" + Request["ListInID"].ToString().Trim() + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.BasicStockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        public ActionResult AddBasicStockIn()
        {
            StockIn si = new TECOCITY_BGOI.StockIn();
            si.ListInID = InventoryMan.GetTopListInID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            ViewData["StockInTime"] = DateTime.Now;
            si.BatchID = InventoryMan.GetTopHandwrittenAccount();
            return View(si);
        }

        public ActionResult ChangeBasic()
        {
            return View();
        }

        public ActionResult GetPtype()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "20";

            UIDataTable udtTask = InventoryMan.GetPtype(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePtypeList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Ptype = Request["ptype"].ToString();
            string PID = Request["PID"].ToString();
            UIDataTable udtTask = InventoryMan.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, PID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //零件
        public ActionResult ChangePtypeListLinJian()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Ptype = Request["ptype"].ToString();
            string PID = Request["PID"].ToString();
            UIDataTable udtTask = InventoryMan.ChangePtypeListLinJian(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, PID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ChangePtypeListnew()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string PID = Request["PID"].ToString();
            string type = Request["Info"].ToString();
            string Spec = Request["Spec"].ToString();
            UIDataTable udtTask = InventoryMan.ChangePtypeListnew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, PID, type, Spec);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetBasicDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.GetBasicDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetBasicOUTDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.GetBasicOUTDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //根据成品加载规格
        public ActionResult GetBasicDetailSpec()
        {
            string PID = Request["pid"].ToString();
            DataTable dt = InventoryMan.GetBasicDetailSpec(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult SaveBasicStockIn()
        {
            if (ModelState.IsValid)
            {
                StockIn stockin = new StockIn();
                stockin.ListInID = Request["ListInID"].ToString();
                if (GAccount.GetAccountInfo().UnitID == "47")
                {
                    stockin.SubjectID = "S00001";
                }
                else
                {
                    stockin.SubjectID = Request["SubjectID"].ToString();
                }

                stockin.BatchID = Request["BatchID"].ToString();
                stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
                stockin.HouseID = Request["HouseID"].ToString();
                stockin.ProPlace = Request["ProPlace"].ToString();
                stockin.StockInUser = Request["LoginName"].ToString();
                stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockin.State = 0;
                stockin.ValiDate = "v";
                stockin.Type = "基本";
                if (Request["DrawID"] != null && Request["DrawID"].ToString() != "")
                {
                    stockin.DrawID = Request["DrawID"].ToString();
                }
                stockin.Remark = Request["Remarkzhu"].ToString();
                stockin.ApplyPer = "";
                stockin.ApplyTime = DateTime.Now;
                if (GAccount.GetAccountInfo().UnitID == "47")
                {
                    stockin.HandwrittenAccount = Request["HandwrittenAccount"].ToString();
                }
                else
                {
                    stockin.HandwrittenAccount = "";
                }


                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockInCount"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');

                string strErr = "";
                StockInDetail deInfo = new StockInDetail();
                List<StockInDetail> detailList = new List<StockInDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockInDetail();
                    deInfo.ListInID = Request["ListInID"].ToString();
                    deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.BatchNumber = Request["BatchID"].ToString();
                    deInfo.UpState = "0";
                    deInfo.Numupdate = 0;
                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveBasicStockIn(stockin, detailList, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加基本入库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加基本入库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockIn";
                    log.Typeid = Request["ListInID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //加载打印数据
        public ActionResult PrintBasicStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                where += " where  ListInID = '" + ListInID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockIn so = new tk_StockIn();
            foreach (DataRow dt in data.Rows)
            {
                so.strStockID = ListInID;

                so.strStockInTime = dt["StockInTime"].ToString();

                ViewData["year"] = Convert.ToDateTime(dt["StockInTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["StockInTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["StockInTime"]).Day.ToString();//日
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["StockInUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName;// dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        //加载物料信息
        public ActionResult PrictStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                s += " ListInID like '%" + ListInID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_Inventory.dbo.tk_StockInDetail  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region [生产组装入库]
        public ActionResult ProductionStockIn()
        {
            return View();
        }

        //入库单添加页面
        public ActionResult AddProductionStockIn()
        {
            StockIn si = new TECOCITY_BGOI.StockIn();
            si.ListInID = InventoryMan.GetTopListInID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            si.BatchID = InventoryMan.GetTopHandwrittenAccount();
            return View(si);
        }
        public ActionResult ProductionStockInList(ProductionStockInQuery ducin)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Type='生产组装入库' and a.ValiDate='v' and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string BatchID = ducin.BatchID;// Request["BatchID"].ToString().Trim();
                string ListInID = ducin.ListInID;// Request["ListInID"].ToString().Trim();
                string HouseID = "";
                string ProType = Request["ProType"].ToString();
                string Spec = Request["Spec"].ToString().Replace(" ", "");
                if (Request["IsHouseIDone"] != "0")
                {
                    HouseID = Request["IsHouseIDone"].ToString();
                }
                if (Request["IsHouseIDtwo"] != "0")
                {
                    HouseID = Request["IsHouseIDtwo"].ToString();
                }
                if (Spec != "")
                    where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo where Spec like '%" + Spec + "%')) and";
                if (ProType != "")
                    where += " f.OID='" + ProType + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["BatchID"] != "")
                    where += " a.BatchID like'%" + Request["BatchID"] + "%' and";
                if (Request["ListInID"] != "")
                    where += " a.ListInID like '%" + Request["ListInID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.ProtoStockInListShengChan(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public FileResult ProductionStockInListToExcelFZ()
        {
            string where = " a.Type='生产组装入库' and  a.ValiDate='v'  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "   a.ListInID in (" + str + ")  and";
            }
            string IsHouseIDoneto = Request["IsHouseIDoneto"].ToString();
            string IsHouseIDtwoto = Request["IsHouseIDtwoto"].ToString();

            string BatchID = Request["BatchID"].ToString();
            string ListInID = Request["ListInID"].ToString();
            string HouseID = "";
            string ProType = Request["ProType"].ToString();
            string Spec = Request["Spec"].ToString().Replace(" ", "");
            if (IsHouseIDoneto != "0")
            {
                HouseID = IsHouseIDoneto;
            }
            if (IsHouseIDtwoto != "0")
            {
                HouseID = IsHouseIDtwoto;
            }
            if (Spec != "")
                where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
            if (ProType != "")
                where += " f.OID='" + ProType + "' and";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (BatchID != "")
                where += " a.BatchID like'%" + BatchID + "%' and";
            if (ListInID != "")
                where += " a.ListInID like '%" + ListInID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            if (unitid == "47")
            {
                FieldName = "  a.ListInID,a.BatchID,a.HandwrittenAccount,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            else
            {
                FieldName = "  a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-5000,科目-5000,入库时间-6000,入库操作员-6000,总金额-5000,";
                strCols += "产品库类型-5000,所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "生产组装入库入库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "生产组装入库入库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult ChangeProduction()
        {
            return View();
        }
        //加载生产表数据
        public ActionResult ChangeProductionList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ChangeProductionList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //添加到入库表中
        public ActionResult SaveProductionStockIn()
        {
            StockIn stockin = new StockIn();
            stockin.ListInID = Request["ListInID"].ToString();
            if (GAccount.GetAccountInfo().UnitID == "47")
            {
                stockin.HandwrittenAccount = Request["HandwrittenAccount"].ToString();
                stockin.SubjectID = "S00001";
            }
            else
            {
                stockin.HandwrittenAccount = "";
                stockin.SubjectID = Request["SubjectID"].ToString();
            }
            stockin.BatchID = Request["BatchID"].ToString();
            stockin.StockInTime = Convert.ToDateTime(Request["StockInTime"].ToString());
            stockin.HouseID = Request["HouseID"].ToString();
            stockin.ProPlace = Request["ProPlace"].ToString();
            stockin.StockInUser = Request["LoginName"].ToString();
            //stockin.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
            stockin.State = 0;
            stockin.ValiDate = "v";
            stockin.Type = "生产组装入库";
            string DID = Request["DID"].ToString();
            stockin.DrawID = Request["DrawID"].ToString();
            string RKID = Request["RKID"].ToString();

            stockin.Remark = Request["Remark1"].ToString();
            stockin.ApplyPer = "";
            stockin.ApplyTime = DateTime.Now;
            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrProName = Request["ProName"].Split(',');
            string[] arrSpec = Request["SpecsModels"].Split(',');
            string[] arrUnitName = Request["UnitName"].Split(',');
            string[] arrCount = Request["StockInCount"].Split(',');
            string[] arrPrice = Request["UnitPrice"].Split(',');
            string[] arrAmount = Request["TotalAmount"].Split(',');
            string[] arrMan = Request["Manufacturer"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');
            string[] arrOrderID = Request["OrderID"].Split(',');


            string strErr = "";
            StockInDetail deInfo = new StockInDetail();
            List<StockInDetail> detailList = new List<StockInDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new StockInDetail();
                deInfo.ListInID = Request["ListInID"].ToString();
                deInfo.DIID = ProStockInDetialNum(Request["ListInID"].ToString());
                deInfo.ProductID = arrPID[i].ToString();
                deInfo.ProName = arrProName[i].ToString();
                deInfo.Spec = arrSpec[i].ToString();
                deInfo.Units = arrUnitName[i].ToString();
                deInfo.StockInCount = Convert.ToInt32(arrCount[i]);
                stockin.AmountM = Convert.ToInt32(arrCount[i]);
                deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                deInfo.Manufacturer = arrMan[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.OrderID = arrOrderID[i].ToString();
                deInfo.BatchNumber = Request["BatchID"].ToString();
                deInfo.UpState = "0";
                deInfo.Numupdate = 0;
                detailList.Add(deInfo);
            }

            bool b = InventoryMan.SaveProductionStockIn(stockin, RKID, detailList, ref strErr);
            if (b)
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加生产组装入库";
                log.LogContent = "添加成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockIn";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "添加生产组装入库";
                log.LogContent = "添加失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockIn";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult GetProductionDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetProductionDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpProductionDateState()
        {
            string ListInID = Request["ListInID"].ToString();

            if (InventoryMan.UpProductionDateState(ListInID))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "入库成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改入库库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListInID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = "入库失败" });
            }
        }
        //加载打印数据
        public ActionResult PrintProductionStockIn()
        {
            string ListInID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                where += " where  ListInID = '" + ListInID + "' ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockIn so = new tk_StockIn();
            foreach (DataRow dt in data.Rows)
            {
                so.strStockID = ListInID;
                so.strStockInTime = dt["StockInTime"].ToString();

                ViewData["year"] = Convert.ToDateTime(dt["StockInTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["StockInTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["StockInTime"]).Day.ToString();//日

                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["StockInUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已入库" : "未入库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListInID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockIn  where  ListInID='" + ListInID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #endregion
        #region [货品出库]
        #region [销售订单出库]

        public ActionResult RetailSalesOut()
        {
            return View();
        }

        public ActionResult RetailSalesOutList(RetailSalesOutQuery retout)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='零售销售' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string OrderContactor = retout.OrderContactor;//Request["OrderContactor"].ToString();
                string ListOutID = retout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();
                if (Request["OrderContactor"] != "")
                    where += " a.Purchase like '%" + Request["OrderContactor"] + "%' and";
                if (Request["ListOutID"] != "")
                    where += " a.ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.RetailSalesOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public FileResult RetailSalesOutListToExcel()
        {
            string where = "Type='零售销售' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }

            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "销售订单出库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "销售订单出库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddRetailSalesOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }

        public ActionResult OrderInfoSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.OrderInfoSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ChangeRetailSales()
        {
            return View();
        }

        public ActionResult GetOrderSalesDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetOrderSalesDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveOrderSalesOut()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "37")
                {
                    stockout.SubjectID = Request["SubjectID"].ToString();
                    stockout.UnitID = unitid;
                }
                else
                {
                    stockout.SubjectID = "";
                    stockout.UnitID = Request["SubjectID"].ToString();
                }
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "零售销售";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string DID = Request["DID"].ToString();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();


                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');
                string[] arrPurchase = Request["OrderContactor"].Split(',');
                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();
                   // stockout.Purchase = arrPurchase[i].ToString();

                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveOrderSalesOut(stockout, DID, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加零售销售出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {

                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加零售销售出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        public ActionResult StockOutDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ListOutID = Request["ListOutID"].ToString();


            UIDataTable udtTask = InventoryMan.StockOutDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ListOutID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpOutDateState()
        {
            string ListOutID = Request["ListOutID"].ToString();

            if (InventoryMan.UpOutDateState(ListOutID))
            {

                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改出库库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListOutID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "出库成功" });
            }
            else
            {

                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改出库库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListOutID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = "出库失败" });
            }
        }

        public ActionResult PrintRetailSalesOut()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where Type='零售销售' and  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                ViewData["strStockOutTime"] = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyy/mm/dd");

                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                //so.strStockOutTime = dt["ProOutTime"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                ViewData["Use"] = dt["Text"].ToString();
                so.strCreateUser = dt["Purchase"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [项目销售出库]

        public ActionResult ProjectSalesOut()
        {
            return View();
        }

        public ActionResult ProjectSalesOutList(ProjectSalesOutQuery proout)
        {

            if (ModelState.IsValid)
            {
                string where = "Type='项目销售' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = proout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.ProjectSalesOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public FileResult ProjectSalesOutListToExcel()
        {
            string where = "Type='项目销售' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "项目销售出库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "项目销售出库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddProjectSalesOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }

        public ActionResult OrderProSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.OrderProSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ChangeProjectSales()
        {
            return View();
        }

        public ActionResult SaveOrderProSalesOut()
        {
            StockOut stockout = new StockOut();
            stockout.ListOutID = Request["ListOutID"].ToString();
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "37")
            {
                stockout.SubjectID = Request["SubjectID"].ToString();
                stockout.UnitID = unitid;
            }
            else
            {
                stockout.SubjectID = "";
                stockout.UnitID = Request["SubjectID"].ToString();
            }
            stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
            stockout.HouseID = Request["HouseID"].ToString();
            stockout.ProOutUser = Request["ProOutUser"].ToString();
            stockout.Use = Request["Use"].ToString();
            stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
            stockout.State = 0;
            stockout.ValiDate = "v";
            stockout.Type = "项目销售";
            stockout.Purchase = Request["Purchase"].ToString().Trim();
            string DID = Request["DID"].ToString();
            string Count = Request["Count"].ToString();


            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrProName = Request["ProName"].Split(',');
            string[] arrSpec = Request["SpecsModels"].Split(',');
            string[] arrUnitName = Request["UnitName"].Split(',');
            string[] arrCount = Request["StockOutCount"].Split(',');
            string[] arrActual = Request["StockOutCountActual"].Split(',');
            string[] arrPrice = Request["UnitPrice"].Split(',');
            string[] arrAmount = Request["TotalAmount"].Split(',');
            string[] arrMan = Request["Manufacturer"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');
            string[] arrOrderID = Request["OrderID"].Split(',');

            string strErr = "";
            StockOutDetail deInfo = new StockOutDetail();
            List<StockOutDetail> detailList = new List<StockOutDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new StockOutDetail();
                deInfo.ListOutID = Request["ListOutID"].ToString();
                deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                deInfo.ProductID = arrPID[i].ToString();
                deInfo.ProName = arrProName[i].ToString();
                deInfo.Spec = arrSpec[i].ToString();
                deInfo.Units = arrUnitName[i].ToString();
                deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                if (arrPrice[i] != "")
                {
                    deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                }
                if (arrAmount[i] != "")
                {
                    deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                }
                deInfo.Manufacturer = arrMan[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.OrderID = arrOrderID[i].ToString();
                detailList.Add(deInfo);
            }

            bool b = InventoryMan.SaveOrderProSalesOut(stockout, DID, Count, detailList, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {

                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加项目销售出库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockOutDetail";
                    log.Typeid = Request["ListOutID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {

                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加项目销售出库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockOutDetail";
                    log.Typeid = Request["ListOutID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = "出库操作失败" });
                }
            }
        }

        //加载打印数据
        public ActionResult PrintProjectSalesOut()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                so.strStockOutTime = dt["ProOutTime"].ToString();

                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["Use"] = dt["Text"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName;// dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [二级库出库]
        public ActionResult SecondOut()
        {
            return View();
        }

        public ActionResult SecondOutList(SecondOutQuery secout)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='二级库' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = secout.ListOutID;// Request["ListOutID"].ToString().Trim();
                string HouseID = Request["HouseID"].ToString().Trim();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.SecondOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }



        }
        public FileResult SecondOutListToExcel()
        {
            string where = "Type='二级库' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,科目-5000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "二级库出库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "二级库出库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddSecondOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;
            ViewData["ProOutTime"] = DateTime.Now;
            return View(so);
        }

        public ActionResult SaveSecondOut()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                stockout.SubjectID = Request["SubjectID"].ToString();
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "二级库";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');

                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();


                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveSecondOut(stockout, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加二级库出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加二级库出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }

        //加载打印数据
        public ActionResult PrintSecondOut()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b   ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                ViewData["strStockOutTime"] = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyy/mm/dd");

                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName;// dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [上样机出库]

        public ActionResult ProtoUpOut()
        {
            return View();
        }

        public ActionResult ProtoUpOutList(ProtoUpOutQuery upout)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='上样机' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = upout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.ProtoUpOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public FileResult ProtoUpOutListToExcel()
        {
            string where = "Type='上样机' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,科目-5000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "上样机导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "上样机信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddProtoUpOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;
            ViewData["ProOutTime"] = DateTime.Now;
            return View(so);
        }

        public ActionResult ProtoUpDetailList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = new UIDataTable();
            udtTask = InventoryMan.ProtoUpDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ChangeProtoUp()
        {
            return View();
        }

        public ActionResult GetProtoUpDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetProtoUpDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveProtoUpOut()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                stockout.SubjectID = Request["SubjectID"].ToString();
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "上样机";
                stockout.Purchase = Request["Purchase"].ToString().Trim();//申请人
                string DID = Request["DID"].ToString();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                // string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');

                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    // deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();


                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveProtoUpOut(stockout, DID, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加上样机出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加上样机出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //加载打印数据
        public ActionResult PrintProtoUpOut()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b   ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                so.strStockOutTime = dt["ProOutTime"].ToString();
                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [内购/赠送出库]

        public ActionResult BuyGiveOut()
        {
            return View();
        }

        public ActionResult BuyGiveOutList(BuyGiveOutQuery buyout)
        {

            if (ModelState.IsValid)
            {
                string where = "Type='内购/赠送' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = buyout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.BuyGiveOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        public FileResult BuyGiveOutListToExcel()
        {
            string where = "Type='内购/赠送' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,科目-5000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "内购导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "内购信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddBuyGiveOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;
            ViewData["ProOutTime"] = DateTime.Now;
            return View(so);
        }

        public ActionResult ChangeBuyGive()
        {
            return View();
        }

        public ActionResult InternalDetailList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.InternalDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetBuyGiveDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetBuyGiveDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveBuyGiveOut()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                stockout.SubjectID = Request["SubjectID"].ToString();
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "内购/赠送";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string DID = Request["DID"].ToString();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');

                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();


                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveBuyGiveOut(stockout, DID, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加内购/赠送出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加内购/赠送出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }

        //加载打印数据
        public ActionResult PrintBuyGiveOut()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut  a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b  ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                ViewData["strStockOutTime"] = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyy/mm/dd");

                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [专柜制作]

        public ActionResult CountersOut()
        {
            return View();
        }
        public ActionResult CountersOutList()
        {
            string where = "Type='专柜制作' and ";
            string strCurPage;
            string strRowNum;


            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ListOutID = Request["ListOutID"].ToString().Trim();
            string HouseID = Request["HouseID"].ToString();

            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = InventoryMan.ProjectSalesOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddCountersOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        public ActionResult ChangeCountersSales()
        {
            return View();
        }
        public ActionResult GetCounterSalesDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetCounterSalesDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult CounterOutSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.CounterOutSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveCounterSalesOut()
        {
            StockOut stockout = new StockOut();
            stockout.ListOutID = Request["ListOutID"].ToString();
            stockout.SubjectID = Request["SubjectID"].ToString();
            stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
            stockout.HouseID = Request["HouseID"].ToString();
            stockout.ProOutUser = Request["ProOutUser"].ToString();
            stockout.Use = Request["Use"].ToString();
            stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
            stockout.State = 0;
            stockout.ValiDate = "v";
            stockout.Type = "专柜制作";
            string DID = Request["DID"].ToString();
            string Count = Request["Count"].ToString();


            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrProName = Request["ProName"].Split(',');
            string[] arrSpec = Request["SpecsModels"].Split(',');
            string[] arrUnitName = Request["UnitName"].Split(',');
            string[] arrCount = Request["StockOutCount"].Split(',');
            string[] arrPrice = Request["UnitPrice"].Split(',');
            string[] arrAmount = Request["TotalAmount"].Split(',');
            // string[] arrMan = Request["Manufacturer"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');
            string[] arrSIID = Request["SIID"].Split(',');

            string strErr = "";
            StockOutDetail deInfo = new StockOutDetail();
            List<StockOutDetail> detailList = new List<StockOutDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new StockOutDetail();
                deInfo.ListOutID = Request["ListOutID"].ToString();
                deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                deInfo.ProductID = arrPID[i].ToString();
                deInfo.ProName = arrProName[i].ToString();
                deInfo.Spec = arrSpec[i].ToString();
                deInfo.Units = arrUnitName[i].ToString();
                deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                if (arrPrice[i] != "")
                {
                    deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                }
                if (arrAmount[i] != "")
                {
                    deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                }
                // deInfo.Manufacturer = arrMan[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.OrderID = arrSIID[i].ToString();


                detailList.Add(deInfo);
            }

            bool b = InventoryMan.SaveCounterSalesOut(stockout, DID, Count, detailList, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加专柜出库";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockOutDetail";
                    log.Typeid = Request["ListOutID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加专柜出库";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_StockOutDetail";
                    log.Typeid = Request["ListOutID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = "出库操作失败" });
                }
            }
        }
        #endregion
        #region [基本出库]

        public ActionResult BasicStockOut()
        {
            return View();
        }

        public ActionResult AddBasicStockOut()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;
            ViewData["ProOutTime"] = DateTime.Now;
            return View(so);
        }

        public ActionResult ChangeBasicOut()
        {
            return View();
        }

        public ActionResult BasicOutList(BasicStockOutQuery basiout)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='基本' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = basiout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.BasicOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        public ActionResult SaveBacicOut()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "37")
                {
                    stockout.SubjectID = Request["SubjectID"].ToString();
                    stockout.UnitID = unitid;
                }
                else
                {
                    stockout.SubjectID = "";
                    stockout.UnitID = Request["SubjectID"].ToString();
                }
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "基本";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');

                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveBacicOut(stockout, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加基础出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加基础出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //加载打印数据
        public ActionResult PrintBasicOutList()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                so.strStockOutTime = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyyy/MM/dd");
                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }


        //加载打印数据
        public ActionResult PrintBasicOutInvoiceList()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                so.strStockOutTime = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyyy/MM/dd");
                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                ViewData["Time"] = DateTime.Now.ToString(); ;
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        //加载物料信息
        public ActionResult PrictBasicOutPro()
        {
            string ListOutID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                s += " ListOutID like '%" + ListOutID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_Inventory.dbo.tk_StockOutDetail  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region [生产领料单出库]
        public ActionResult ProductionMaterials()
        {
            return View();
        }

        public ActionResult ProductionMaterialsList(ProductionMaterialsQuery matquery)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='生产领料单出库' and ";
                string strCurPage;
                string strRowNum;


                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = matquery.ListOutID;// Request["ListOutID"].ToString().Trim();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["ListOutID"] != "")
                    where += " ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.ProjectSalesOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public FileResult ProductionMaterialsListToExcel()
        {
            string where = "Type='生产领料单出库' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "生产领料单出库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "生产领料单出库信息表.xls");
            }
            else
                return null;
        }
        public ActionResult AddProductionMaterials()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }

        public ActionResult SaveProductionMaterials()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "37")
                {
                    stockout.SubjectID = Request["SubjectID"].ToString();
                    stockout.UnitID = unitid;
                }
                else
                {
                    stockout.SubjectID = "";
                    stockout.UnitID = Request["SubjectID"].ToString();
                }
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                // stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "生产领料单出库";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string DID = Request["DID"].ToString();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                // string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrLLID = Request["LLID"].Split(',');

                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    // deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrLLID[i].ToString();
                    detailList.Add(deInfo);
                }
                bool b = InventoryMan.SaveProductionMaterials(stockout, DID, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加生产领料单";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加生产领料单";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        public ActionResult ChangeProductionMaterials()
        {
            return View();
        }

        public ActionResult ProductionMaterialsOutSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ProductionMaterialsOutSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductionMaterialsSalesDetail()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetProductionMaterialsSalesDetail(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //加载打印数据
        public ActionResult PrintProductionMaterials()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID  ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b   ";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                ViewData["strStockOutTime"] = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyy/mm/dd");


                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                // so.strStockOutTime = dt["ProOutTime"].ToString();
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["Purchase"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; // dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        #endregion
        #region [家用产品销售]
        public ActionResult HomeProductSales()
        {
            return View();
        }
        public ActionResult HomeProductSalesList(RetailSalesOutQuery retout)
        {
            if (ModelState.IsValid)
            {
                string where = "Type='家用产品销售' and ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string OrderContactor = retout.OrderContactor;//Request["OrderContactor"].ToString();
                string ListOutID = retout.ListOutID;// Request["ListOutID"].ToString();
                string HouseID = Request["HouseID"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();
                if (Request["OrderContactor"] != "")
                    where += " a.Purchase like '%" + Request["OrderContactor"] + "%' and";
                if (Request["ListOutID"] != "")
                    where += " a.ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
                }
                else
                {
                    where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
                }
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.HomeProductSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult AddHomeProductSales()
        {
            StockOut so = new TECOCITY_BGOI.StockOut();
            so.ListOutID = InventoryMan.GetTopListOutID();
            so.ProOutUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        public ActionResult ChangeHomeProductSales()
        {
            return View();
        }
        public ActionResult ChangeHomeProductSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.ChangeHomeProductSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetHomeProductSales()
        {
            string DID = Request["did"].ToString();
            DataTable dt = InventoryMan.GetHomeProductSales(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult SaveHomeProductSales()
        {
            if (ModelState.IsValid)
            {
                StockOut stockout = new StockOut();
                stockout.ListOutID = Request["ListOutID"].ToString();
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "37")
                {
                    stockout.SubjectID = Request["SubjectID"].ToString();
                    stockout.UnitID = unitid;
                }
                else
                {
                    stockout.SubjectID = "";
                    stockout.UnitID = Request["SubjectID"].ToString();
                }
                stockout.ProOutTime = Convert.ToDateTime(Request["ProOutTime"].ToString());
                stockout.HouseID = Request["HouseID"].ToString();
                stockout.ProOutUser = Request["ProOutUser"].ToString();
                stockout.Use = Request["Use"].ToString();
                stockout.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                stockout.State = 0;
                stockout.ValiDate = "v";
                stockout.Type = "家用产品销售";
                stockout.Purchase = Request["Purchase"].ToString().Trim();
                string DID = Request["DID"].ToString();
                string Count = Request["Count"].ToString();
                stockout.Remark = Request["Remark1"].ToString();
                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrProName = Request["ProName"].Split(',');
                string[] arrSpec = Request["SpecsModels"].Split(',');
                string[] arrUnitName = Request["UnitName"].Split(',');
                string[] arrCount = Request["StockOutCount"].Split(',');
                string[] arrActual = Request["StockOutCountActual"].Split(',');
                string[] arrPrice = Request["UnitPrice"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');
                string[] arrRemark = Request["Remark"].Split(',');
                string[] arrOrderID = Request["OrderID"].Split(',');
                string[] arrPurchase = Request["OrderContactor"].Split(',');
                string strErr = "";
                StockOutDetail deInfo = new StockOutDetail();
                List<StockOutDetail> detailList = new List<StockOutDetail>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    deInfo = new StockOutDetail();
                    deInfo.ListOutID = Request["ListOutID"].ToString();
                    deInfo.DOID = ProStockInDetialNum(Request["ListOutID"].ToString());
                    deInfo.ProductID = arrPID[i].ToString();
                    deInfo.ProName = arrProName[i].ToString();
                    deInfo.Spec = arrSpec[i].ToString();
                    deInfo.Units = arrUnitName[i].ToString();
                    deInfo.StockOutCount = Convert.ToInt32(arrCount[i]);
                    deInfo.StockOutCountActual = Convert.ToInt32(arrActual[i]);
                    if (arrPrice[i] != "")
                    {
                        deInfo.UnitPrice = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrAmount[i] != "")
                    {
                        deInfo.TotalAmount = Convert.ToDecimal(arrAmount[i]);
                    }
                    deInfo.Manufacturer = arrMan[i].ToString();
                    deInfo.Remark = arrRemark[i].ToString();
                    deInfo.OrderID = arrOrderID[i].ToString();
                  //  stockout.Purchase = arrPurchase[i].ToString();

                    detailList.Add(deInfo);
                }

                bool b = InventoryMan.SaveHomeProductSales(stockout, DID, Count, detailList, ref strErr);
                if (strErr != "")
                {
                    return Json(new { success = "false", Msg = strErr });
                }
                else
                {
                    if (b)
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加家用产品销售出库";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        #region [添加报警表]
                        InAlarm inal = new InAlarm();
                        inal.OrderID = arrOrderID[0].ToString();
                        inal.OperationTime = DateTime.Now.ToString();
                        inal.Operator = Request["ProOutUser"].ToString();
                        inal.OperationContent = "签收订单";
                        InventoryMan.AddInAlarm(inal);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_Inventorylog log = new tk_Inventorylog();
                        log.LogTitle = "添加家用产品销售出库";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_StockOutDetail";
                        log.Typeid = Request["ListOutID"].ToString();
                        InventoryMan.AddInventLog(log);
                        #endregion
                        return Json(new { success = false, Msg = "出库操作失败" });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult UpHomeProductSalesState()
        {
            string ListOutID = Request["ListOutID"].ToString();
            string orderid = "";
            DataTable dt = InventoryMan.GetOrderID(ListOutID);
            foreach (DataRow dr in dt.Rows)
            {
                orderid = dr["OrderID"].ToString();
            }
            if (InventoryMan.UpHomeProductSalesState(ListOutID, orderid))
            {

                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改出库库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListOutID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                #region [添加报警表]

                InAlarm inal = new InAlarm();
                inal.OrderID = orderid;
                inal.OperationTime = DateTime.Now.ToString();
                inal.Operator = Request["ProOutUser"].ToString();
                inal.OperationContent = "已出库订单";
                InventoryMan.AddInAlarm(inal);
                #endregion
                return Json(new { success = "true", Msg = "出库成功" });
            }
            else
            {

                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改出库库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockRemain";
                log.Typeid = Request["ListOutID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = "出库失败" });
            }
        }
        public FileResult HomeProductSalesToExcel()
        {
            string where = "Type='家用产品销售' and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string OrderContactor = Request["OrderContactor"].ToString();
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString();

            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();
            if (Request["OrderContactor"] != "")
                where += " a.Purchase like '%" + Request["OrderContactor"] + "%' and";
            if (Request["ListOutID"] != "")
                where += " a.ListOutID like '%" + Request["ListOutID"] + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse ) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                                      " left join  BGOI_Inventory.dbo.InfooUT c on b.LibraryTubeState=c.id " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            FieldName = "  a.ListOutID,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Purchase,a.Amount,e.HouseName,a.Remark, (case when  a.State ='0'then '未入库' else '已入库' end) as State,c.Text,b.OrderID  ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,出库时间-5000,库管-6000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000,订单状态-6000,订单编号-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "家用销售出库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "家用销售出库信息表.xls");
            }
            else
                return null;
        }

        //加载打印数据
        public ActionResult PrintHomeProductSales()
        {
            string ListOutID = Request["Info"];
            string where = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                where += " where  ListOutID = '" + ListOutID + "' and a.[Use]=b.ID ";
            }
            string strErr = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a ,(select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses') b";
            DataTable data = InventoryMan.PrintList(where, tableName, ref strErr);
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                so.strOutID = ListOutID;
                so.strStockOutTime = Convert.ToDateTime(dt["ProOutTime"]).ToString("yyyy/MM/dd");
                ViewData["year"] = Convert.ToDateTime(dt["ProOutTime"]).Year.ToString();//年
                ViewData["Month"] = Convert.ToDateTime(dt["ProOutTime"]).Month.ToString();//月
                ViewData["Day"] = Convert.ToDateTime(dt["ProOutTime"]).Day.ToString();//日
                ViewData["Use"] = dt["Text"].ToString();
                ViewData["Time"] = DateTime.Now.ToString(); ;
                //  so.strCreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.strCreateUser = dt["ProOutUser"].ToString();
                ViewData["State"] = Convert.ToInt32(dt["State"]) > 0 ? "已出库" : "未出库";
            }

            string wheretype = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wheretype = " where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string protype = " BGOI_Inventory.dbo.tk_WareHouse ";
            DataTable dtprotype = InventoryMan.PrintList(wheretype, protype, ref strErr);
            foreach (DataRow dtpt in dtprotype.Rows)
            {
                ViewData["HouseName"] = dtpt["HouseName"].ToString();
            }
            string wherehouse = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                wherehouse = "where ID in(select SubjectID from BGOI_Inventory.dbo.tk_StockOut  where  ListOutID='" + ListOutID + "')";
            }
            string prohouse = " BGOI_Inventory.dbo.tk_ConfigSubject ";
            DataTable dtprohouse = InventoryMan.PrintList(wherehouse, prohouse, ref strErr);
            foreach (DataRow dtpt in dtprohouse.Rows)
            {
                ViewData["Text"] = GAccount.GetAccountInfo().UnitName; //dtpt["Text"].ToString();
            }
            ViewData["Text"] = GAccount.GetAccountInfo().UnitName;
            return View(so);
        }
        //加载物料信息
        public ActionResult PrictHomeProductSalesPro()
        {
            string ListOutID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(ListOutID))
            {
                s += " ListOutID like '%" + ListOutID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_Inventory.dbo.tk_StockOutDetail  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }



        //提示报警
        public ActionResult GetOrderidNew()
        {
            DataTable dt = InventoryMan.GetOrderidNew();

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #endregion
        #region [订单管理]

        #region [入库单管理]
        public ActionResult StorageManagement()
        {
            return View();
        }
        //入库单管理列表页
        public ActionResult StorageManagementtList(StorageManagementQuery manaout)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;


                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ProType = Request["ProType"].ToString();
                string BatchID = manaout.BatchID;// Request["BatchID"].ToString().Trim();
                string ListInID = manaout.ListInID;// Request["ListInID"].ToString().Trim();
                string HouseID = Request["HouseID"].ToString();
                // string StockInUser = Request["StockInUser"].ToString();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();
                if (ProType != "")
                    where += " f.ID  ='" + ProType + "' and";
                if (Request["BatchID"].ToString().Trim() != "")
                    where += " a.BatchID ='" + Request["BatchID"].ToString().Trim() + "' and";
                if (Request["ListInID"].ToString().Trim() != "")
                    where += " a.ListInID like '%" + Request["ListInID"].ToString().Trim() + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and a.Validate='v' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.StorageManagementtList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                // UIDataTable udtTask = InventoryMan.StockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);

                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }

        //导出用户回访
        public FileResult StorageManagementToExcel()
        {
            string ListInID = Request["ListInID"];
            string where = " ";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            string ProType = Request["ProType"].ToString();
            string HouseID = Request["HouseID"].ToString();

            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "  a.ListInID in (" + str + ")  and";
            }
            if (ProType != "")
                where += " f.ID  ='" + ProType + "' and";
            if (Request["BatchID"].ToString().Trim() != "")
                where += " a.BatchID ='" + Request["BatchID"].ToString().Trim() + "' and";
            if (Request["ListInID"].ToString().Trim() != "")
                where += " a.ListInID like '%" + Request["ListInID"].ToString().Trim() + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and a.Validate='v' and";
            where = where.Substring(0, where.Length - 3);

            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')and Validate='v') e on a.HouseID=e.HouseID " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
            if (GAccount.GetAccountInfo().UnitID == "47")
            {
                FieldName = " a.ListInID as ListInID,a.HandwrittenAccount AS BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,StockInUser,Amount,e.HouseName as HouseName,f.Text,(case when State ='0'then '未入库' else '已入库' end) as State  ";
            }
            else
            {
                FieldName = " a.ListInID as ListInID,a.BatchID AS BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,StockInUser,Amount,e.HouseName as HouseName,f.Text,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-6000,科目-6000,入库时间-5000,入库人-6000,总金额-5000,所属仓库-3000,货品库类型-3000,状态-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "入库单管理信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "入库单管理信息表.xls");
            }
            else
                return null;

        }
        #endregion

        #region [出库单管理]

        public ActionResult StorageManagementOut()
        {
            return View();
        }
        public ActionResult StorageManagementOutList(StorageManagementOutQuery stoout)
        {
            if (ModelState.IsValid)
            {
                string where = " ";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListOutID = stoout.ListOutID;// Request["ListOutID"].ToString().Trim();
                string HouseID = Request["HouseID"].ToString().Trim();
                string ProType = Request["ProType"].ToString();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();
                if (ProType != "")
                    where += " f.ID='" + ProType + "' and";
                if (Request["ListOutID"] != "")
                    where += " a.ListOutID like '%" + Request["ListOutID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseName='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " a.State= '" + State + "' and a.Validate='v' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.StorageManagementOutList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //public ActionResult UpRefundDaGetBasicDetailteState()
        //{
        //    string ListOutID = Request["ListOutID"].ToString();

        //    if (InventoryMan.UpRefundDateState(ListOutID))
        //    {

        //        return Json(new { success = "true", Msg = "退库成功" });
        //    }
        //    else
        //    {
        //        return Json(new { success = "false", Msg = "退库失败" });
        //    }
        //}


        //导出用户回访
        public FileResult StorageManagementOutToExcel()
        {
            string where = "";
            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "   a.ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutID"].ToString();
            string HouseID = Request["HouseID"].ToString().Trim();
            string ProType = Request["ProType"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();
            if (ProType != "")
                where += " f.ID='" + ProType + "' and";
            if (ListOutID != "")
                where += " a.ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseName='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and a.Validate='v' and";
            where = where.Substring(0, where.Length - 3);

            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')and Validate='v') e on a.HouseID=e.HouseID " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
            //if (GAccount.GetAccountInfo().UnitID == "47")
            //{
            FieldName = " a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,f.Text,(case when State ='0'then '未出库' else '已出库' end) as State  ";
            //}
            //else
            //{
            //    FieldName = " a.ListOutID as ListOutID,a.BatchID AS BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,StockInUser,Amount,e.HouseName as HouseName,f.Text,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            //}
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,科目-6000,出库时间-5000,经办人-6000,总金额-5000,所属仓库-3000,货品库类型-3000,状态-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "出库单管理信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "出库单管理信息表.xls");
            }
            else
                return null;

        }
        #endregion

        #region [报废单管理]

        public ActionResult ScrapManagement()
        {
            return View();
        }

        public ActionResult ScrapManagementOut()
        {
            Scrap so = new TECOCITY_BGOI.Scrap();
            so.ListScrapID = InventoryMan.GetTopListScrapID();
            so.Handlers = GAccount.GetAccountInfo().UserName;
            ViewData["ScrapTime"] = DateTime.Now;
            return View(so);
        }
        public ActionResult ScrapManagementList(ScrapManagementQuery scrapquery)
        {
            if (ModelState.IsValid)
            {
                string where = " ";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string ListScrapID = scrapquery.ListScrapID;// Request["ListScrapID"].ToString().Trim();
                string HouseID = Request["HouseID"].ToString().Trim();
                string Handlers = scrapquery.Handlers;// Request["Handlers"].ToString().Trim();
                string PID = scrapquery.PID;// Request["PID"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                string State = Request["State"].ToString();

                if (Request["Handlers"] != "")
                    where += " Handlers like '%" + Request["Handlers"] + "%' and";
                if (Request["ListScrapID"] != "")
                    where += " ListScrapID like '%" + Request["ListScrapID"] + "%' and";
                if (Request["PID"] != "")
                    where += " PID like '%" + Request["PID"] + "%' and";
                if (HouseID != "")
                    where += " e.HouseID='" + HouseID + "' and";
                if (Begin != "" && End != "")
                    where += " ScrapTime between '" + Begin + "' and '" + End + "' and ";
                if (State != "")
                    where += " State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.ScrapManagementList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //制作报废单
        public ActionResult SaveScrapManagement()
        {
            if (ModelState.IsValid)
            {
                Scrap scrap = new Scrap();
                scrap.ListScrapID = Request["ListScrapID"].ToString();
                scrap.SubjectID = Request["SubjectID"].ToString();
                scrap.ReasonRemark = Request["ReasonRemark"].ToString();
                scrap.ScrapTime = Convert.ToDateTime(Request["ScrapTime"].ToString());
                scrap.HouseID = Request["HouseID"].ToString();
                scrap.Handling = Request["Handling"].ToString();
                scrap.Handlers = Request["Handlers"].ToString();
                scrap.AmountM = Convert.ToDecimal(Request["Amount"].ToString());
                scrap.State = 0;
                string Count = Request["Count"].ToString();

                string[] arrMain = Request["MainContent"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrCount = Request["scrapCount"].Split(',');
                string[] arrAmount = Request["TotalAmount"].Split(',');
                string[] arrMan = Request["Manufacturer"].Split(',');


                string strErr = "";
                List<Scrap> detailList = new List<Scrap>();
                for (int i = 0; i < arrMain.Length; i++)
                {
                    scrap = new Scrap();
                    scrap.ListScrapID = Request["ListScrapID"].ToString();
                    scrap.PID = arrPID[i].ToString();
                    scrap.ScrapCount = Convert.ToInt32(arrCount[i]);
                    scrap.AmountM = Convert.ToDecimal(arrAmount[i]);
                    scrap.FactoryNum = arrMan[i].ToString();


                    scrap.SubjectID = Request["SubjectID"].ToString();
                    scrap.ReasonRemark = Request["ReasonRemark"].ToString();
                    scrap.ScrapTime = Convert.ToDateTime(Request["ScrapTime"].ToString());
                    scrap.HouseID = Request["HouseID"].ToString();
                    scrap.Handling = Request["Handling"].ToString();
                    scrap.Handlers = Request["Handlers"].ToString();
                    scrap.State = 0;
                    detailList.Add(scrap);
                }

                bool b = InventoryMan.SaveScrapManagement(scrap, detailList, Count, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "新增报废表";
                    log.LogContent = "添加成功";
                    log.Person = Request["Handlers"].ToString();
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_Scrap";
                    log.Typeid = Request["ListScrapID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion

                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "新增报废表";
                    log.LogContent = "添加失败";
                    log.Person = Request["Handlers"].ToString();
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_Scrap";
                    log.Typeid = Request["ListScrapID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion

                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //报废完成
        public ActionResult UpScrapDateState()
        {
            string strErr = "";
            string ListScrapID = Request["ListScrapID"].ToString();

            if (InventoryMan.UpScrapDateState(ListScrapID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改报废单报废库存数量";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockOutDetail";
                log.Typeid = Request["ListScrapID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "报废成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "修改报废单报废库存数量";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_StockOutDetail";
                log.Typeid = Request["ListScrapID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        public FileResult ScrapManagementToExcel()
        {
            string where = "";
            string ListScrapID = Request["ListScrapID"].ToString().Trim();
            string HouseID = Request["HouseID"].ToString().Trim();
            string Handlers = Request["Handlers"].ToString().Trim();
            string PID = Request["PID"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            var ListScrapIDN = Request["ListScrapIDN"].ToString();
            if (ListScrapIDN != "")
            {
                var str = ListScrapIDN.Remove(ListScrapIDN.Length - 1, 1);
                where += "  ListScrapID in (" + str + ")  and";
            }
            if (Handlers != "")
                where += " Handlers like '%" + Handlers + "%' and";
            if (ListScrapID != "")
                where += " ListScrapID like '%" + ListScrapID + "%' and";
            if (PID != "")
                where += " PID like '%" + PID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " ScrapTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " State= '" + State + "' and";
            where = where.Substring(0, where.Length - 3);

            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_Inventory.dbo.tk_Scrap a " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')and Validate='v') e on a.HouseID=e.HouseID " +
                                        "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
            FieldName = " a.ListScrapID,PID,Convert(varchar(12),a.ScrapTime,111) as ScrapTime,a.ScrapCount,a.AmountM,Handlers,e.HouseName,f.Text,a.ReasonRemark,(case when State ='0'then '未报废' else '已报废' end) as State  ";
            string OrderBy = " a.ScrapTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "报废单单编号-6000,货物编号-6000,报废时间-5000,数量-6000,金额-5000,经办人-3000,仓库-3000,库房类型-5000,报废原因-6000,状态-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "报废单管理信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "报废单管理信息表.xls");
            }
            else
                return null;

        }
        #endregion

        #region [销售发货单管理]

        //销售发货单的页面
        public ActionResult SalesInvoiceManagement()
        {
            return View();
        }
        //销售发货单列表
        public ActionResult SalesInvoiceManagementList(SalesInvoiceanagementQuery salesquery)
        {
            if (ModelState.IsValid)
            {
                string whereone = "";
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;


                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string ContractID = salesquery.ContractID;// Request["ContractID"].ToString().Trim();
                string OrderID = salesquery.OrderID;// Request["OrderID"].ToString().Trim();
                // string HouseID = Request["HouseID"].ToString().Trim();
                string ShipGoodeID = salesquery.ShipGoodeID;// Request["ShipGoodeID"].ToString().Trim();
                string PID = salesquery.ProID;// Request["PID"].ToString().Trim();
                if (Request["ContractID"] != "")
                    where += " a.ContractID='" + Request["ContractID"] + "' and";
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["ShipGoodeID"] != "")
                    where += " a.ShipGoodsID like '%" + Request["ShipGoodeID"] + "%' and";
                if (Request["OrderID"] != "")
                    where += " a.OrderID like '%" + Request["OrderID"] + "%' and ";
                //if (HouseID != "")
                //    where += " b.HouseID  like '" + HouseID + "' and ";
                if (Request["PID"] != "")
                    where += " a.ShipGoodsID in (select ShipGoodsID from BGOI_Sales.dbo.Shipments_DetailInfo  where ProductID  like '" + Request["PID"] + "') and";
                if (Begin != "" && End != "")
                    where += " a.ShipmentDate between '" + Begin + "' and '" + End + "' and";
                //if (State != "")
                //    where += " a.State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = InventoryMan.SalesInvoiceManagementList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, whereone);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }


        //产品信息
        public ActionResult SalesInvoiceManagementDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ShipGoodsID = Request["ShipGoodsID"].ToString();


            UIDataTable udtTask = InventoryMan.SalesInvoiceManagementDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ShipGoodsID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //得到发货单编号和创建人
        public ActionResult SalesInvoiceManagementOut()
        {
            Shipments so = new TECOCITY_BGOI.Shipments();
            so.ShipGoodsID = InventoryMan.GetTopListSalesInvoiceID();//发货单编号
            so.CreateUser = GAccount.GetAccountInfo().UserName;//创建人

            return View(so);
        }
        //制作销售发货单
        //public ActionResult SaveSalesInvoiceManagement()
        //{
        //    Shipments scrap = new Shipments();
        //    scrap.ShipGoodsID = Request["ShipGoodsID"].ToString();
        //    scrap.SubjectID = Request["SubjectID"].ToString();
        //    scrap.Remark = Request["Remark"].ToString();
        //    scrap.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
        //    scrap.HouseID = Request["HouseID"].ToString();
        //    scrap.CreateUser = Request["CreateUser"].ToString();
        //    scrap.AmountM = Convert.ToDecimal(Request["AmountM"].ToString());
        //    scrap.OrderID = Request["OrderID"].ToString();
        //    scrap.State = "0";

        //    string[] arrMain = Request["MainContent"].Split(',');
        //    string[] arrPID = Request["PID"].Split(',');
        //    string[] arrCount = Request["scrapCount"].Split(',');
        //    string[] arrAmount = Request["TotalAmount"].Split(',');
        //    string[] arrMan = Request["Manufacturer"].Split(',');
        //    string[] arrOrderContent = Request["ProName"].Split(',');
        //    string[] arrdid = Request["DID"].Split(','); 
        //    string strErr = "";
        //    Shipments_DetailInfo deta = new Shipments_DetailInfo();
        //    List<Shipments_DetailInfo> detailList = new List<Shipments_DetailInfo>();
        //    for (int i = 0; i < arrMain.Length; i++)
        //    {
        //        deta = new Shipments_DetailInfo();
        //        deta.ShipGoodsID = Request["ShipGoodsID"].ToString();
        //        deta.HouseID = Request["HouseID"].ToString();
        //        deta.PID = arrPID[i].ToString();
        //        scrap.PID = arrPID[i].ToString();
        //        deta.Amount = Convert.ToInt32(arrCount[i]);
        //        deta.Subtotal = Convert.ToDecimal(arrAmount[i]);
        //        deta.OrderContent = arrOrderContent[i].ToString();
        //        deta.DID = arrdid[i].ToString();
        //        deta.strSupplier = arrMan[i].ToString();
        //        detailList.Add(deta);
        //    }

        //    bool b = InventoryMan.SaveSalesInvoiceManagement(scrap, detailList, ref strErr);
        //    if (b)
        //    {
        //        return Json(new { success = true });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Msg = strErr });
        //    }
        //}
        //发货单完成
        public ActionResult UpSalesInvoDateState()
        {
            string strErr = "";
            string ShipGoodsID = Request["ShipGoodsID"].ToString();

            if (InventoryMan.UpSalesInvoDateState(ShipGoodsID, ref strErr))
            {

                return Json(new { success = "true", Msg = " 发货成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //订单选择页面
        public ActionResult SalasInvoiSales()
        {
            return View();
        }
        //订单
        public ActionResult OrderInfoInvoSalesList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = InventoryMan.OrderInfoInvoSalesList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetOrderSalesInvoDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = InventoryMan.GetOrderSalesInvoDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #endregion
        #region [库存报警]

        public ActionResult LowAlarm()
        {
            return View();
        }
        //最低库存报警--物料信息列表
        public ActionResult MaterialBasicData(LowAlarmQuery quer)
        {

            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                string where = "";
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string ProName = quer.ProName;// Request["ProName"].ToString();
                if (Request["ProName"].ToString().Trim() != "")
                {
                    where += " and b.ProName like  '%" + Request["ProName"].ToString().Trim() + "%' ";
                }
                UIDataTable udtTask = InventoryMan.MaterialBasicData(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //最低库存报警--查询上限数量
        public ActionResult MaterialBasicNum()
        {
            DataTable dt = InventoryMan.MaterialBasicNum();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //最低库存报警--获取报警提示信息
        public ActionResult getAlarm()
        {
            string strFirsTypeText = Request["strFirsTypeText"].ToString();
            string strCount = Request["strCount"].ToString();
            string strAlarm = InventoryMan.getAlarm(strFirsTypeText, strCount);
            //return Json(strAlarm);
            if (strAlarm == "")
                return Json(new { success = "false", Msg = "数据加载失败" });
            else
                return Json(new { success = "true", Alarm = strAlarm });

        }
        // 进入系统判断库存量 
        public ActionResult LowWarn()
        {
            return View();
        }
        // 获取报警库存列表
        public ActionResult getWarnLow()
        {
            string strErr = "";
            //
            string strDetail = InventoryMan.getWarnLow(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDetail == "")
                    return Json(new { success = "false", Msg = "报警库存数据获取失败" });
                else
                    return Json(new { success = "true", WarnLow = strDetail });
            }
        }
        //添加报警数量上限
        public ActionResult SaveLowAlarm()
        {
            if (ModelState.IsValid)
            {
                tk_HouseEarlyWarningNum stockin = new tk_HouseEarlyWarningNum();
                stockin.PID = Request["PID"].ToString();
                stockin.Num = Request["Num"].ToString();
                string strErr = "";
                bool b = InventoryMan.SaveLowAlarm(stockin, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加报警数量上限";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加报警数量上限";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public ActionResult UpLowAlarm()
        {
            string PID = Request["pid"].ToString().Trim();
            DataTable dt = InventoryMan.UpLowAlarm(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetPidXiang()
        {
            string PID = Request["pid"].ToString().Trim();
            DataTable dt = InventoryMan.GetPidXiang(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult Setlowerlimit()
        {
            return View();
        }
        //设置提醒在途中
        public ActionResult LowAlarmZT()
        {
            if (ModelState.IsValid)
            {
                string PID = Request["PID"].ToString();
                string strErr = "";
                bool b = InventoryMan.LowAlarmZT(PID, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加状态";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加状态";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        //设置提醒在生产
        public ActionResult LowAlarmZSC()
        {
            if (ModelState.IsValid)
            {
                string PID = Request["PID"].ToString();
                string strErr = "";
                bool b = InventoryMan.LowAlarmZSC(PID, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加状态";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_Inventorylog log = new tk_Inventorylog();
                    log.LogTitle = "添加状态";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_HouseEarlyWarningNum";
                    log.Typeid = Request["PID"].ToString();
                    InventoryMan.AddInventLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        #endregion
        #region [统计总汇]

        #region [物料总汇]
        public ActionResult MaterialSummaryTable()
        {
            return View();
        }
        public ActionResult MaterialSummaryTableList()
        {
            //h.ValiDate='v' and g.ValiDate='v' 
            string where = "   ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string spec = Request["spec"].ToString();
            if (spec != "")
                where += " Spec like '%" + spec + "%' and";
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.MaterialSummaryTableList(HouseID, start, end, where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        //加载打印数据
        public ActionResult PrintMaterialSummaryTable()
        {
            string where = "   ";
            string start = Request["start"].ToString().Replace("'", "");
            string end = Request["end"].ToString().Replace("'", "");
            string HouseID = Request["HouseID"].ToString().Replace("'", "");
            string spec = Request["spec"].ToString().Replace("'", "");
            if (spec != "")
                where += " Spec like '%" + spec + "%' and";
            where = where.Substring(0, where.Length - 3);
            DataTable a = InventoryMan.MaterialSummaryTableList(HouseID, start, end, where);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>物料总汇");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='16'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 10)
            {
                #region [表头]
                //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                //sb1.Append("<tr>");
                //sb1.Append("<td>");
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 10%;'rowspan='2'>物料类型</td>");
                sb1.Append("<td style='width: 10%;'rowspan='2'>物料编码</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>物料名称</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>规格型号</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>所属仓库</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>单位</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>盘点日单价</td>");
                sb1.Append("<td style='width: 10%;'colspan='2'>盘点日帐面记录</td>");
                sb1.Append("<td style='width: 10%;'>盘点记录数量</td>");
                sb1.Append("<td style='width: 10%;'colspan='2'>盘点日至基准日入帐数</td>");
                sb1.Append("<td style='width: 10%;'colspan='2'>基准日应结存</td>");
                sb1.Append("<td style='width: 10%;'colspan='2'>差异</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 5%;'>数量</td>");
                sb1.Append("<td style='width: 5%;'>金额</td>");
                sb1.Append("<td style='width: 5%;'>数量</td>");
                sb1.Append("<td style='width: 5%;'>入库数量</td>");
                sb1.Append("<td style='width: 5%;'>出库数量</td>");
                sb1.Append("<td style='width: 5%;'>数量</td>");
                sb1.Append("<td style='width: 5%;'>金额</td>");
                sb1.Append("<td style='width: 5%;'>数量</td>");
                sb1.Append("<td style='width: 5%;'>金额</td>");
                sb1.Append("</tr>");
                //sb1.Append("<tbody id='DetailInfo' class='tabInfo2'>");
                //sb1.Append("</tbody>");
                //sb1.Append("</table>");
                //sb1.Append("</td>");
                //sb.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容]
                    sb1.Append("<tr id ='DetailInfo" + i + "'>");
                    sb1.Append("<td><lable class='labText" + i + "' id='Text" + i + "'>" + a.Rows[i]["Text"] + "</lable></td>");
                    sb1.Append("<td><lable class='labProductID" + i + "' id='ProductID" + i + "'>" + a.Rows[i]["PID"] + "</lable></td>");
                    sb1.Append("<td><lable class='labProName" + i + "' id='ProName" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labSpec" + i + "' id='Spec" + i + "'>" + a.Rows[i]["Spec"] + "</lable></td>");
                    sb1.Append("<td><lable class='labHouseNAME" + i + "' id='HouseNAME" + i + "'>" + a.Rows[i]["HouseNAME"] + "</lable></td>");
                    sb1.Append("<td><lable class='labUnits" + i + "' id='Units" + i + "'>" + a.Rows[i]["Units"] + "</lable></td>");
                    sb1.Append("<td><lable class='labUnitPrice" + i + "' id='UnitPrice" + i + "'>" + a.Rows[i]["UnitPrice"] + "</lable></td>");
                    sb1.Append("<td><lable class='labFinishCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["FinishCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labtotal" + i + "' id='total" + i + "'>" + a.Rows[i]["total"] + "</lable></td>");
                    sb1.Append("<td><lable class='labFinishCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["FinishCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labInCount" + i + "' id='InCount" + i + "'>" + a.Rows[i]["InCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labOutCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["OutCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labtotalCount" + i + " ' id='totalCount" + i + "'>" + a.Rows[i]["totalCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labtotalPrice" + i + "' id='totalPrice" + i + "'>" + a.Rows[i]["totalPrice"] + "</lable></td>");
                    sb1.Append("<td><lable class='labFinishCount1" + i + "' id='FinishCount1" + i + "'>" + a.Rows[i]["Cynum"] + "</lable></td>");
                    sb1.Append("<td><lable class='labtotal1" + i + "' id='total1" + i + "'>" + a.Rows[i]["CynumPrice"] + "</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                }
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 10;
                if (count > 0)
                    count = a.Rows.Count / 10 + 1;
                else
                    count = a.Rows.Count / 10;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 10 * i;
                    int length = 10 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 10 * i + a.Rows.Count % 10;
                    #region [表头]
                    //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                    //sb1.Append("<tr>");
                    //sb1.Append("<td>");
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 10%;'rowspan='2'>物料类型</td>");
                    sb1.Append("<td style='width: 10%;'rowspan='2'>物料编码</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>物料名称</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>规格型号</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>所属仓库</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>单位</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>盘点日单价</td>");
                    sb1.Append("<td style='width: 10%;'colspan='2'>盘点日帐面记录</td>");
                    sb1.Append("<td style='width: 10%;'>盘点记录数量</td>");
                    sb1.Append("<td style='width: 10%;'colspan='2'>盘点日至基准日入帐数</td>");
                    sb1.Append("<td style='width: 10%;'colspan='2'>基准日应结存</td>");
                    sb1.Append("<td style='width: 10%;'colspan='2'>差异</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 5%;'>数量</td>");
                    sb1.Append("<td style='width: 5%;'>金额</td>");
                    sb1.Append("<td style='width: 5%;'>数量</td>");
                    sb1.Append("<td style='width: 5%;'>入库数量</td>");
                    sb1.Append("<td style='width: 5%;'>出库数量</td>");
                    sb1.Append("<td style='width: 5%;'>数量</td>");
                    sb1.Append("<td style='width: 5%;'>金额</td>");
                    sb1.Append("<td style='width: 5%;'>数量</td>");
                    sb1.Append("<td style='width: 5%;'>金额</td>");
                    sb1.Append("</tr>");
                    //sb1.Append("<tbody id='DetailInfo' class='tabInfo2'>");
                    //sb1.Append("</tbody>");
                    //sb1.Append("</table>");
                    //sb1.Append("</td>");
                    //sb.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容]
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labText" + i + "' id='Text" + i + "'>" + a.Rows[i]["Text"] + "</lable></td>");
                        sb1.Append("<td><lable class='labProductID" + i + "' id='ProductID" + i + "'>" + a.Rows[i]["PID"] + "</lable></td>");
                        sb1.Append("<td><lable class='labProName" + i + "' id='ProName" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labSpec" + i + "' id='Spec" + i + "'>" + a.Rows[i]["Spec"] + "</lable></td>");
                        sb1.Append("<td><lable class='labHouseNAME" + i + "' id='HouseNAME" + i + "'>" + a.Rows[i]["HouseNAME"] + "</lable></td>");
                        sb1.Append("<td><lable class='labUnits" + i + "' id='Units" + i + "'>" + a.Rows[i]["Units"] + "</lable></td>");
                        sb1.Append("<td><lable class='labUnitPrice" + i + "' id='UnitPrice" + i + "'>" + a.Rows[i]["UnitPrice"] + "</lable></td>");
                        sb1.Append("<td><lable class='labFinishCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["FinishCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labtotal" + i + "' id='total" + i + "'>" + a.Rows[i]["total"] + "</lable></td>");
                        sb1.Append("<td><lable class='labFinishCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["FinishCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labInCount" + i + "' id='InCount" + i + "'>" + a.Rows[i]["InCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labOutCount" + i + "' id='FinishCount" + i + "'>" + a.Rows[i]["OutCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labtotalCount" + i + " ' id='totalCount" + i + "'>" + a.Rows[i]["totalCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labtotalPrice" + i + "' id='totalPrice" + i + "'>" + a.Rows[i]["totalPrice"] + "</lable></td>");
                        sb1.Append("<td><lable class='labFinishCount1" + i + "' id='FinishCount1" + i + "'>" + a.Rows[i]["Cynum"] + "</lable></td>");
                        sb1.Append("<td><lable class='labtotal1" + i + "' id='total1" + i + "'>" + a.Rows[i]["CynumPrice"] + "</lable></td>");
                        sb1.Append("</tr>");
                        #endregion
                    }
                    if ((length - b) < 10)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }
        public ActionResult PrintMaterial()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString().Replace("'", "");
            string end = Request["end"].ToString().Replace("'", "");
            string HouseID = Request["HouseID"].ToString().Replace("'", "");

            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.MaterialSummaryTableList(HouseID, start, end, where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region [库存汇总表]
        public ActionResult InventorySummaryTable()
        {
            DataTable data = InventoryMan.KuCunZongHui();
            tk_StockOut so = new tk_StockOut();
            foreach (DataRow dt in data.Rows)
            {
                ViewData["zongjie"] = dt["zongjie"].ToString();
            }
            DataTable jine = InventoryMan.GetJinE();
            foreach (DataRow dr in jine.Rows)
            {
                ViewData["fzzje"] = dr["zcbje"].ToString();
                ViewData["fzhsje"] = dr["hsje"].ToString();
            }
            return View();
        }
        //加载打印数据
        public ActionResult PrintInventorySummaryTable()
        {
            return View();
        }
        //加载打印数据
        public ActionResult PrintInventoryList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string ProName = Request["ProName"].ToString().Trim();
            string Spec = Request["Spec"].ToString();
            if (ProName != "")
                where += " ProName   like '%" + ProName + "%' and";
            if (Spec != "")
                where += " Spec  like '%" + Spec + "%' and";

            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.InventorySummaryTableList(start, end, where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult InventorySummaryTableList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string ProName = Request["ProName"].ToString().Trim();
            string Spec = Request["Spec"].ToString();
            if (ProName != "")
                where += " ProName   like '%" + ProName + "%' and";
            if (Spec != "")
                where += " Spec  like '%" + Spec + "%' and";
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.InventorySummaryTableList(start, end, where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult AdditionalList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string ProName = Request["ProName"].ToString().Trim();
            string Spec = Request["Spec"].ToString();
            if (ProName != "")
                where += " ProName   like '%" + ProName + "%' and";
            if (Spec != "")
                where += " Spec  like '%" + Spec + "%' and";
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.AdditionalList(start, end, where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region [物料出入库明细表]
        public ActionResult MaterialOutOfTheWarehouse()
        {
            return View();
        }
        //加载打印数据
        public ActionResult PrintMaterialOutOfTheWarehouse()
        {
            return View();
        }
        public ActionResult PrintMaterialOutOfTheList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.MaterialOutOfTheWarehouseList(start, end, where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult MaterialOutOfTheWarehouseList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string Spec = Request["Spec"].ToString();
            if (Spec != "")
            {
                where += " Spec  like '%" + Spec + "%' and";
            }
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.MaterialOutOfTheWarehouseList(start, end, where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult AddWarehouseList()
        {
            string where = " and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            string Spec = Request["Spec"].ToString();
            if (Spec != "")
            {
                where += " Spec  like '%" + Spec + "%' and";
            }
            where = where.Substring(0, where.Length - 3);
            DataTable dt = InventoryMan.AddWarehouseList(start, end, where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        #endregion

        #region [入库汇总]
        public ActionResult InventoryStatistics()
        {
            return View();
        }
        //加载打印数据
        public ActionResult PrintInventoryStatistics()
        {
            DataTable a = InventoryMan.InventoryStatisticsList();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;width: 100%'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>入库汇总表");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='16'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 20)
            {
                #region [表头]
                //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                //sb1.Append("<tr>");
                //sb1.Append("<td>");
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 20%;'>物料编码</td>");
                sb1.Append("<td style='width: 20%;'>物料名称</td>");
                sb1.Append("<td style='width: 20%;'>规格型号</td>");
                sb1.Append("<td style='width: 15%;'>计量单位</td>");
                sb1.Append("<td style='width: 15%;'>入库数量</td>");
                sb1.Append("<td style='width: 15%;'>在线生产数量</td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容]
                    sb1.Append("<tr id ='DetailInfo" + i + "'>");
                    sb1.Append("<td><lable class='labProductID" + i + "' id='ProductID" + i + "'>" + a.Rows[i]["ProductID"] + "</lable></td>");
                    sb1.Append("<td><lable class='labProName" + i + "' id='ProName" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labSpec" + i + "' id='Spec" + i + "'>" + a.Rows[i]["Spec"] + "</lable></td>");
                    sb1.Append("<td><lable class='labUnits" + i + "' id='Units" + i + "'>" + a.Rows[i]["Units"] + "</lable></td>");
                    sb1.Append("<td><lable class='labStockInCount" + i + "' id='StockInCount" + i + "'>" + a.Rows[i]["StockInCount"] + "</lable></td>");
                    sb1.Append("<td><lable class='labOnlineCount" + i + "' id='OnlineCount" + i + "'>" + a.Rows[i]["OnlineCount"] + "</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                }
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 20;
                if (count > 0)
                    count = a.Rows.Count / 20 + 1;
                else
                    count = a.Rows.Count / 20;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 20 * i;
                    int length = 20 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 20 * i + a.Rows.Count % 20;
                    #region [表头]
                    //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                    //sb1.Append("<tr>");
                    //sb1.Append("<td>");
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 20%;'>物料编码</td>");
                    sb1.Append("<td style='width: 20%;'>物料名称</td>");
                    sb1.Append("<td style='width: 20%;'>规格型号</td>");
                    sb1.Append("<td style='width: 15%;'>计量单位</td>");
                    sb1.Append("<td style='width: 15%;'>入库数量</td>");
                    sb1.Append("<td style='width: 15%;'>在线生产数量</td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容]
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labProductID" + i + "' id='ProductID" + i + "'>" + a.Rows[i]["ProductID"] + "</lable></td>");
                        sb1.Append("<td><lable class='labProName" + i + "' id='ProName" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labSpec" + i + "' id='Spec" + i + "'>" + a.Rows[i]["Spec"] + "</lable></td>");
                        sb1.Append("<td><lable class='labUnits" + i + "' id='Units" + i + "'>" + a.Rows[i]["Units"] + "</lable></td>");
                        sb1.Append("<td><lable class='labStockInCount" + i + "' id='StockInCount" + i + "'>" + a.Rows[i]["StockInCount"] + "</lable></td>");
                        sb1.Append("<td><lable class='labOnlineCount" + i + "' id='OnlineCount" + i + "'>" + a.Rows[i]["OnlineCount"] + "</lable></td>");
                        sb1.Append("</tr>");
                        #endregion
                    }
                    if ((length - b) < 20)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }
        public ActionResult InventoryStatisticsList()
        {
            DataTable dt = InventoryMan.InventoryStatisticsList();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #endregion
        #region [公共信息维护]
        public ActionResult ConfigurationInformation()
        {
            return View();
        }
        public ActionResult BasicSet()
        {
            return View();
        }
        public ActionResult ConfigurationInformationList()
        {
            string where = " Validate = 'v' and";
            string strCurPage;
            string strRowNum;
            UIDataTable udtTask = new UIDataTable();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sel = Request["sel"].ToString();
            if (sel == "1")
            {
                where += " [Type] = 'SpecsModels' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = InventoryMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            }
            else if (sel == "2")
            {
                where += " [Type] = 'OutUses' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = InventoryMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            }
            else
            {
                where += " [Type] = '' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = InventoryMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);

            }
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddConfigurationInformation(string id)
        {
            ViewData["type"] = id;
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertContentnew()
        {
            var type = Request["Type"];
            var text = Request["Text"].ToString().Trim();
            string strErr = "";
            if (InventoryMan.InsertNewContentnew(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteContentnew()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (InventoryMan.DeleteNewContentnew(xid, type, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateContentnew(string id)
        {
            string[] arr = id.Split('@');
            ViewData["XID"] = arr[0];
            ViewData["Type"] = arr[1];
            ViewData["Text"] = arr[2];
            return View();
        }

        public ActionResult upNewContentnew()
        {
            var ID = Request["XID"];
            var Type = Request["Type"];
            var Text = Request["Text"];
            string strErr = "";
            if (InventoryMan.UpdateNewContentnew(ID, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "修改成功" });
            else
                return Json(new { success = "false", Msg = "修改出错" + "/" + strErr });
        }

        #endregion
        #region 【发展】
        #region 【基本入库】

        public FileResult BasicStockInToExcelFZ()
        {
            string where = " a.Type='基本' and  a.ValiDate='v'  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListInIDN = Request["ListInIDN"].ToString();
            if (ListInIDN != "")
            {
                var str = ListInIDN.Remove(ListInIDN.Length - 1, 1);
                where += "  a.ListInID in (" + str + ")  and";
            }

            string IsHouseIDoneto = Request["IsHouseIDoneto"].ToString();
            string IsHouseIDtwoto = Request["IsHouseIDtwoto"].ToString();

            string BatchID = Request["BatchID"].ToString();
            string ListInID = Request["ListInIDC"].ToString();
            string HouseID = "";
            string ProType = Request["ProType"].ToString();
            string Spec = Request["Spec"].ToString().Replace(" ", "");
            if (IsHouseIDoneto != "0")
            {
                HouseID = IsHouseIDoneto;
            }
            if (IsHouseIDtwoto != "0")
            {
                HouseID = IsHouseIDtwoto;
            }
            if (Spec != "")
                where += " a.ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockInDetail where replace(Spec,' ','')  like'%" + Spec + "%')  and";
            if (ProType != "")
                where += " f.OID='" + ProType + "' and";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (BatchID != "")
                where += " a.BatchID like'%" + BatchID + "%' and";
            if (ListInID != "")
                where += " a.ListInID like '%" + ListInID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.StockInTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            if (unitid == "47")
            {
                FieldName = "  a.ListInID,a.BatchID,a.HandwrittenAccount,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            else
            {
                FieldName = "  a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,f.Text as T,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            }
            string OrderBy = " a.StockInTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "入库单编号-6000,入库批号-5000,科目-5000,入库时间-6000,入库操作员-6000,总金额-5000,";
                strCols += "产品库类型-5000,所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "基本入库导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "基本入库信息表.xls");
            }
            else
                return null;
        }
        #endregion
        #region 【基本出库】
        public FileResult BasicStockOutToExcelFZ()
        {
            string where = "Type='基本' and ";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            var ListOutIDN = Request["ListOutIDN"].ToString();
            if (ListOutIDN != "")
            {
                var str = ListOutIDN.Remove(ListOutIDN.Length - 1, 1);
                where += "  ListOutID in (" + str + ")  and";
            }
            string ListOutID = Request["ListOutIDold"].ToString();
            string HouseID = Request["HouseID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string State = Request["State"].ToString();

            if (ListOutID != "")
                where += " ListOutID like '%" + ListOutID + "%' and";
            if (HouseID != "")
                where += " e.HouseID='" + HouseID + "' and";
            if (Begin != "" && End != "")
                where += " a.ProOutTime between '" + Begin + "' and '" + End + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and";
            }
            else
            {
                where += " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = "";
            if (unitid == "46" || unitid == "32")
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                        "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                        "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            }
            else
            {
                tableName = " BGOI_Inventory.dbo.tk_StockOut a " +
                                          "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse " +
                                          " where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
            }
            FieldName = " a.ListOutID,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,a.Remark,(case when  State ='0'then '未入库' else '已入库' end) as State   ";
            string OrderBy = " a.ProOutTime ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "出库单编号-6000,出库时间-5000,经手人-6000,总金额-5000,";
                strCols += "所属仓库-5000,备注-6000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "出库单导出", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "出库单信息表.xls");
            }
            else
                return null;
        }

        #endregion
        #endregion
        #region [提示]
        //入库//出库
        public ActionResult GetNumTiXinRu()
        {
            DataTable dt = InventoryMan.GetNumTiXinRu();

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        public ActionResult GetNumTiXinRuNew()
        {
            DataTable dt = InventoryMan.GetNumTiXinRuNew();

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region [产品类型设置]
        public ActionResult ProductTypeSetting()
        {
            return View();
        }
        public ActionResult ProductTypeSettingList()
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += "";
                }
                else
                {
                    where += " UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
                }
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.ProductTypeSettingList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult AddProductTypeSetting()
        {
            tk_ConfigPType so = new tk_ConfigPType();
            so.ID = InventoryMan.GetID();
            so.OID = InventoryMan.OID();
            return View(so);
        }
        //添加
        public ActionResult SaveAddProductTypeSetting()
        {
            if (ModelState.IsValid)
            {
                tk_ConfigPType rem = new tk_ConfigPType();
                rem.ID = Request["ID"].ToString();
                rem.OID = Convert.ToInt32(Request["OID"].ToString());
                rem.Text = Request["Text"].ToString();
                rem.UnitID = GAccount.GetAccountInfo().UnitID;
                rem.Validate = "v";
                string strErr = "";
                string type = Request["type"].ToString();
                if (type == "1")//添加
                {
                    bool b = InventoryMan.SaveAddProductTypeSetting(rem, ref strErr, type);
                    if (strErr != "")
                    {
                        return Json(new { success = "false", Msg = strErr });
                    }
                    else
                    {
                        if (b)
                        {
                            #region [添加日志]
                            tk_Inventorylog log = new tk_Inventorylog();
                            log.LogTitle = "添加产品类型";
                            log.LogContent = "添加成功";
                            log.Person = GAccount.GetAccountInfo().UserName;
                            log.Tiem = DateTime.Now;
                            log.Type = "tk_ConfigPType";
                            log.Typeid = Request["ID"].ToString();
                            InventoryMan.AddInventLog(log);
                            #endregion
                            return Json(new { success = true });
                        }
                        else
                        {
                            #region [添加日志]
                            tk_Inventorylog log = new tk_Inventorylog();
                            log.LogTitle = "添加产品类型";
                            log.LogContent = "添加失败";
                            log.Person = GAccount.GetAccountInfo().UserName;
                            log.Tiem = DateTime.Now;
                            log.Type = "tk_ConfigPType";
                            log.Typeid = Request["ID"].ToString();
                            InventoryMan.AddInventLog(log);
                            #endregion
                            return Json(new { success = false, Msg = "操作失败" });
                        }
                    }
                }
                else//修改
                {
                    bool b = InventoryMan.SaveAddProductTypeSetting(rem, ref strErr, type);
                    if (strErr != "")
                    {
                        return Json(new { success = "false", Msg = strErr });
                    }
                    else
                    {
                        if (b)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false, Msg = "操作失败" });
                        }
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeProductTypeSetting()
        {
            string strErr = "";
            string ID = Request["ID"].ToString();

            if (InventoryMan.DeProductTypeSetting(ID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销产品类型";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ConfigPType";
                log.Typeid = Request["ID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销产品类型";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_ConfigPType";
                log.Typeid = Request["ID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        public ActionResult UpProductTypeSetting()
        {
            tk_ConfigPType so = new TECOCITY_BGOI.tk_ConfigPType();
            string ID = Request["ID"];
            DataTable dt = InventoryMan.UpProductTypeSetting(ID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                //ID, OID, Text, UnitID, Validate
                so.ID = dt.Rows[0][0].ToString();
                so.OID = Convert.ToInt32(dt.Rows[0][1].ToString());
                so.Text = dt.Rows[0][2].ToString();
                so.UnitID = dt.Rows[0][3].ToString();
                so.Validate = dt.Rows[0][4].ToString();
            }
            return View(so);
        }
        #endregion



        #region [上传]
        public ActionResult InOutUpload()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            ViewData["Department"] = account.UnitName;
            return View();
        }
        //加载列表
        public ActionResult InOutUploadList(tk_AwardInOut sheet)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string Award = Request["Award"].ToString();
                string unitid = GAccount.GetAccountInfo().UnitID;
                if (unitid == "46" || unitid == "32")
                {
                    where += "";
                }
                else
                {
                    where += " CreatUser='" + GAccount.GetAccountInfo().UserName + "' and";
                }
                if (Award != "")
                    where += "  Award   like '%" + Award + "%' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = InventoryMan.InOutUploadList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult AddInOutUpload()
        {
            tk_AwardInOut so = new TECOCITY_BGOI.tk_AwardInOut();
            so.SID = InventoryMan.GetTopSID();
            return View(so);
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertAwardInOut(tk_AwardInOut bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string strErr = "";
                HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;//获取上传的文件
                if (InventoryMan.InsertAwardInOut(bas, file, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddInOutUpload", bas);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddInOutUpload", bas);
                }
            }
            else
            {
                ViewData["msg"] = "上传文件不成功";
                return View("AddInOutUpload", bas);
            }
        }
        public void DownLoadAward(string sid)
        {

            DataTable dtInfo = InventoryMan.GetNewDownloadAward(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                if (dtInfo.Rows[0][0].ToString() != "")
                {
                    string fileName = dtInfo.Rows[0]["Award"].ToString();//客户端保存的文件名 
                    string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPStoin"] + "\\"
                         + fileName;//路径
                    //以字符流的形式下载文件 
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }

            }
        }
        //撤销
        public ActionResult DeInOutUpload()
        {
            string strErr = "";
            string SID = Request["SID"].ToString();

            if (InventoryMan.DeInOutUpload(SID, ref strErr))
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销上传";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_AwardInOut";
                log.Typeid = Request["SID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_Inventorylog log = new tk_Inventorylog();
                log.LogTitle = "撤销上传";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_AwardInOut";
                log.Typeid = Request["SID"].ToString();
                InventoryMan.AddInventLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }


        public ActionResult ViewPicture(string OId)
        {
            ViewData["OId"] = OId;
            return View();
        }
        public ActionResult GetFilesNew(string OId)
        {
            //string Pathdt = "";
            //DataTable dt = InventoryMan.GetFilesNew(OId);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Pathdt = dr["AwardInfo"].ToString();
            //}

            //return Json(new { success = "true", Msg = Pathdt });

            string json = GFun.Dt2Json("", InventoryMan.GetFilesNew(OId));
            return Json(json);
        }
        #endregion
    }
}
