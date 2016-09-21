using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF;
using System.IO;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

namespace TECOCITY_BGOI.Controllers
{
    public class EquipManController : Controller
    {
        //
        // GET: /EquipMan/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipMaintain()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.UnitID = '" + unit + "'";
            string strCurPage;
            string strRowNum;
            string TracingType = Request["TracingType"].ToString();
            string CheckCompany = Request["CheckCompany"].ToString();
            string JStarTime = Request["JStarTime"].ToString();
            string JEndTime = Request["JEndTime"].ToString();
            string StarTime = Request["starTime"].ToString();
            string EndTime = Request["endTime"].ToString();
            string State = Request["state"].ToString();
            string OrderDate = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (TracingType != "")
                where += " and a.TracingType = '" + TracingType + "'";
            if (CheckCompany != "")
                where += " and a.CheckCompany like '%" + CheckCompany + "%'";
            if (JStarTime != "")
                where += " and a.PlanDate >= '" + JStarTime + "' and a.PlanDate <= '" + JEndTime + "' ";
            if (StarTime != "")
                where += " and a.LastDate >= '" + StarTime + "' and a.LastDate <= '" + EndTime + "' ";
            if (State != "")
                where += " and a.State = '" + State + "'";
            string Order = " order by m.ControlCode";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }
            //UIDataTable udtTask = new UIDataTable();
            UIDataTable udtTask = EquipMan.getNewEquipGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order, unit);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEquip()
        {
            DevicsBas Bas = new DevicsBas();
            return View(Bas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult InsertDeviceBas(DevicsBas Bas)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            Bas.StrUnitID = account.UnitID;
            Bas.StrCreateTime = DateTime.Now;
            Bas.StrCreateUser = account.UserID.ToString();
            Bas.StrValidate = "v";
            Bas.StrIsCycle = 1;
            string strErr = "";
            if (EquipMan.InsertNewDeviceBas(Bas, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateEquip(string id)
        {
            DevicsBas Bas = EquipMan.getNewDevicsByID(id);
            ViewData["Ecode"] = id;
            return View(Bas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult UpdateNewDeviceBas(DevicsBas Bas)
        {
            var Ecode = Request["Ecode"];
            string strErr = "";
            if (EquipMan.UpdateNewDevice(Bas, Ecode, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteEquip()
        {
            var ecode = Request["data1"];
            string strErr = "";
            if (EquipMan.DeleteNewDeviceBas(ecode, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EquipCheck(string id)
        {
            string[] arr = id.Split('@');
            DCheckInfo CheckInfo = new DCheckInfo();
            CheckInfo.StrECode = arr[0];
            CheckInfo.StrCheckCompany = arr[1];
            CheckInfo.StrCheckWay = arr[2];
            return View(CheckInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CheckInfo"></param>
        /// <returns></returns>
        public ActionResult InsertDCheckInfo(DCheckInfo CheckInfo)
        {
            string strErr = "";
            if (EquipMan.InsertNewDCheckInfo(CheckInfo, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CheckRecord(string id)
        {
            DCheckInfo CheckInfo = new DCheckInfo();
            CheckInfo.StrECode = id;
            return View(CheckInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EquipService(string id)
        {
            DRepairInfo Repair = new DRepairInfo();
            Repair.StrECode = id;
            return View(Repair);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repair"></param>
        /// <returns></returns>
        public ActionResult InsertDRepairInfo(DRepairInfo Repair)
        {
            string strErr = "";
            if (EquipMan.InsertNewDRepairInfo(Repair, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RepaireRecord(string id)
        {
            DRepairInfo Repair = new DRepairInfo();
            Repair.StrECode = id;
            return View(Repair);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Repair"></param>
        /// <returns></returns>
        public ActionResult UpdateDRepairInfo(DRepairInfo Repair)
        {
            string strErr = "";
            if (EquipMan.UpdateNewDRepairInfo(Repair, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EquipScrap(string id)
        {
            DScrapInfo Scrap = new DScrapInfo();
            Scrap.StrECode = id;
            return View(Scrap);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Scrap"></param>
        /// <returns></returns>
        public ActionResult InsertDScrapInfo(DScrapInfo Scrap)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            Scrap.StrCreateUser = account.UserID.ToString();
            Scrap.StrCreateTime = DateTime.Now;
            string strErr = "";
            if (EquipMan.InsertNewDScrapInfo(Scrap, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EquipCheckHistory(string id)
        {
            ViewData["Ecode"] = id;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DCheckInfoHistoryGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string Ecode = Request["ecode"].ToString();
            string StarTime = Request["starTime"].ToString();
            string EndTime = Request["endTime"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Ecode != "")
                where += " and a.ECode = '" + Ecode + "'";
            if (StarTime != "")
                where += " and a.CheckDate >= '" + StarTime + "' and a.CheckDate <= '" + EndTime + "' ";
            UIDataTable udtTask = EquipMan.getNewDevicsBasGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpDCheckInfo(string id)
        {
            string[] arrA = id.Split('@');
            string Did = arrA[0];
            string num = arrA[1];
            ViewData["Did"] = Did;
            ViewData["num"] = num;
            DCheckInfo CheckInfo = new DCheckInfo();
            CheckInfo = EquipMan.getNewUpdateDCheckInfo(Did);
            return View(CheckInfo);
        }

        [HttpPost]
        public FileResult ToExcel()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.UnitID = '" + unit + "'";
            string strCurPage;
            string strRowNum;
            string TracingType = Request["TracingType"].ToString();
            string CheckCompany = Request["CheckCompany"].ToString();
            string JStarTime = Request["JStarTime"].ToString();
            string JEndTime = Request["JEndTime"].ToString();
            string StarTime = Request["starTime"].ToString();
            string EndTime = Request["endTime"].ToString();
            string State = Request["state"].ToString();
            string Order = Request["Order"].ToString();
            string whereOrder = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (TracingType != "")
                where += " and a.TracingType = '" + TracingType + "'";
            if (CheckCompany != "")
                where += " and a.CheckCompany like '%" + CheckCompany + "%'";
            if (JStarTime != "")
                where += " and a.PlanDate >= '" + JStarTime + "' and a.PlanDate <= '" + JEndTime + "' ";
            if (StarTime != "")
                where += " and a.LastDate >= '" + StarTime + "' and a.LastDate <= '" + EndTime + "' ";
            if (State != "")
                where += " and a.State = '" + State + "'";
            if (Order == "")
                whereOrder = " order by m.ControlCode";
            else
            {
                string[] arrLast = Order.Split('@');
                whereOrder = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }

            DataTable data = EquipMan.getNewPintEquip(where, whereOrder);
            if (data != null)
            {
                string strCols = "序号-2000,控制代号-5000,名称-5000,规格型号-7000,测量范围-7000,准确度等级/ 不确定度-5000,溯源方式-5000,有效截止日期至-5000,检定/校准单位名称-5000,备注-3000";
                data.Columns["xu"].SetOrdinal(0);
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelH(data, "-仪器设备一览表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "Info.xls");
            }
            else
                return null;
        }
        //合并单元格
        //private static void mergeCell(ISheet sheet, int firstRow, int lastRow, int firstCell, int lastCell)
        //{
        //    sheet.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCell, lastCell));//2.0使用 2.0以下为Region
        //}

        //public static HSSFCellStyle SetCellStyle(HSSFWorkbook workbook)
        //{
        //    HSSFCellStyle CellStyle = (HSSFCellStyle)workbook.CreateCellStyle();

        //    //CellStyle.FillForegroundColor = HSSFColor.LAVENDER.index;
        //    CellStyle.WrapText = true;
        //    CellStyle.Alignment = HorizontalAlignment.LEFT;
        //    CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
        //   // fCellStyle.FontIndex = HSSFFont.FONT_ARIAL.;
        //    HSSFFont ffont = (HSSFFont)workbook.CreateFont();
        //    ffont.FontHeight = 20 * 20;
        //    ffont.FontName = "宋体";
        //    ffont.Boldweight = (short)FontBoldWeight.BOLD;

        //    //ffont.Boldweight = ;
        //    //ffont.setBoldweight(HSSFFont.BOLDWEIGHT_BOLD); 
        //    //CellStyle.BorderBottom = BorderStyle.THIN;

        //    //CellStyle.BorderLeft = BorderStyle.THIN;
        //    //CellStyle.BorderRight = BorderStyle.THIN;
        //    //CellStyle.BorderTop = BorderStyle.THIN;
        //    // fCellStyle .BorderBottom
        //    // ffont.Color = HSSFColor.;
        //    CellStyle.SetFont(ffont);

        //    CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//.Center;//垂直对齐
        //    CellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
        //    return CellStyle;
        //}

        //public void ToWord(DataTable dt, Page page, string filName)    
        //{    
        //    HttpResponse response = page.Response;    
        //    response.Clear();    
        //    response.ContentType = "application/msword";    
        //    response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");    
        //    response.AddHeader("Content-Disposition","attachment:filename="+System.Web.HttpUtility.UrlEncode(filName,System.Text.Encoding.UTF8)+".doc");    
        //    StringBuilder sBuilder = new StringBuilder();    
        //    for (int i = 0; i < dt.Rows.Count; i++)    
        //    {    
        //        sBuilder.Append(dt.Rows[i][1].ToString()+"\n");    
        //    }    
        //    response.Write(sBuilder.ToString());    
        //    response.End();    
        //}  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CheckInfo"></param>
        /// <param name="Did"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult UpNewCheckInfo(DCheckInfo CheckInfo, string Did, string num)
        {
            string strErr = "";
            if (EquipMan.UpdateNewDCheckInfo(CheckInfo, Did, num, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipWarn()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipWarnGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.state != '-1' and a.UnitID = '" + unit + "'";
            string ck1 = EquipMan.getNewCK1time();
            if (ck1 != "")
                where += " and (DATEDIFF(MONTH,GETDATE(),a.PlanDate)) <= '" + ck1 + "'";
            else
                where += " and (DATEDIFF(MONTH,GETDATE(),a.PlanDate)) <= '2'";

            string strCurPage;
            string strRowNum;
            string OrderDate = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string Order = " order by m.ControlCode";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }
            UIDataTable udtTask = EquipMan.getNewEquipGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order, unit);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SetWarnTime()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult InsertConfigTime(string num)
        {
            string checkWay = "CK";
            string TimeType = "Device";
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string strErr = "";
            if (EquipMan.InsertNewCongTime(checkWay, num, unit, TimeType, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RativeSourceMaintain()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRativeSource()
        {
            RativeSource Rsource = new RativeSource();
            return View(Rsource);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rsource"></param>
        /// <returns></returns>
        public ActionResult InsertRativeSource(RativeSource Rsource)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            Rsource.StrCreateUser = account.UserID.ToString();
            Rsource.StrCreateTime = DateTime.Now;
            Rsource.StrValidate = "v";
            string strErr = "";
            if (EquipMan.InsertNewRativeSource(Rsource, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RativeSourceGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string RID = Request["rID"].ToString();
            string Manufacturer = Request["manufacturer"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (RID != "")
                where += " and a.RID like '%" + RID + "%'";
            if (Manufacturer != "")
                where += " and a.Manufacturer like '%" + Manufacturer + "%' ";
            UIDataTable udtTask = EquipMan.getNewRativeSourceGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateRativeSource(string id)
        {
            ViewData["ID"] = id;
            RativeSource Rsource = new RativeSource();
            Rsource = EquipMan.getNewUpdateRativeSource(id);
            return View(Rsource);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rsource"></param>
        /// <returns></returns>
        public ActionResult UpRativeSource(RativeSource Rsource)
        {
            var id = Request["ID"];
            string strErr = "";
            if (EquipMan.UpdateNewRativeSource(Rsource, id, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteRativeSource()
        {
            var id = Request["data1"];
            string strErr = "";
            if (EquipMan.DeleteNewRative(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TracingPlan()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TracingGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.UnitID = '" + unit + "'";
            string strCurPage;
            string strRowNum;
            string XStarTime = Request["xstarTime"].ToString();
            string XEndTime = Request["xendTime"].ToString();
            string JStarTime = Request["JStarTime"].ToString();
            string JEndTime = Request["JEndTime"].ToString();
            string OrderDate = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (XStarTime != "")
                where += " and a.LastDate >= '" + XStarTime + "' and a.LastDate <= '" + XEndTime + "' ";
            if (JStarTime != "")
                where += " and a.PlanDate >= '" + JStarTime + "' and a.PlanDate <= '" + JEndTime + "' ";
            string Order = " order by m.ControlCode";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }

            UIDataTable udtTask = EquipMan.getNewTracingGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileResult ToTracingExcel()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string UnitName = account.UnitName;
            string where = " and a.UnitID = '" + unit + "'";
            string XStarTime = Request["xstarTime"].ToString();
            string XEndTime = Request["xendTime"].ToString();
            string JStarTime = Request["JStarTime"].ToString();
            string JEndTime = Request["JEndTime"].ToString();
            string Order = Request["Order"].ToString();
            string whereOrder = "";
            string year = DateTime.Now.ToString("yyyy");
            if (XStarTime != "")
                where += " and a.LastDate >= '" + XStarTime + "' and a.LastDate <= '" + XEndTime + "' ";
            if (JStarTime != "")
                where += " and a.PlanDate >= '" + JStarTime + "' and a.PlanDate <= '" + JEndTime + "' ";
            if (Order == "")
                whereOrder = " order by m.ControlCode";
            else
            {
                string[] arrLast = Order.Split('@');
                whereOrder = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }
            DataTable data = EquipMan.getNewPintTracing(where, whereOrder);
            if (data != null)
            {
                string strCols = "序号-2000,设备名称-5000,控制编号-5000,规格型号-7000,制造商-7000,校准服务机构-10000,校准周期-5000,上次校准时间-5000,计划校准时间-5000,结果-3000,负责人-3000";
                data.Columns["xu"].SetOrdinal(0);
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, year + "年度仪器设备溯源计划表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "Info.xls");
            }
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StandingBook()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StandingBookGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.UnitID = '" + unit + "'";
            string strCurPage;
            string strRowNum;
            string TracingType = Request["TracingType"].ToString();
            string CheckCompany = Request["CheckCompany"].ToString();
            string StarTime = Request["starTime"].ToString();
            string EndTime = Request["endTime"].ToString();
            string OrderDate = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (TracingType != "")
                where += " and b.CheckWay = '" + TracingType + "'";
            if (CheckCompany != "")
                where += " and a.CheckCompany like '%" + CheckCompany + "%'";
            if (StarTime != "")
                where += " and b.CheckDate >= '" + StarTime + "' and b.CheckDate <= '" + EndTime + "' ";
            string Order = " order by m.ControlCode";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }
            UIDataTable udtTask = EquipMan.getNewStandingBookGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ToExcelStanding()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string UnitName = account.UnitName;
            string where = " and a.UnitID = '" + unit + "'";
            string TracingType = Request["TracingType"].ToString();
            string CheckCompany = Request["CheckCompany"].ToString();
            string StarTime = Request["starTime"].ToString();
            string EndTime = Request["endTime"].ToString();
            string Order = Request["Order"].ToString();
            string whereOrder = "";
            if (TracingType != "")
                where += " and b.CheckWay = '" + TracingType + "'";
            if (CheckCompany != "")
                where += " and a.CheckCompany like '%" + CheckCompany + "%'";
            if (StarTime != "")
                where += " and b.CheckDate >= '" + StarTime + "' and b.CheckDate <= '" + EndTime + "' ";
            if (Order == "")
                whereOrder = " order by m.ControlCode";
            else
            {
                string[] arrLast = Order.Split('@');
                whereOrder = " order by m." + arrLast[0] + " " + arrLast[1] + "";
            }
            DataTable data = EquipMan.getNewPrintStanding(where, whereOrder);
            if (data != null)
            {
                string strCols = "序号-2000,控制编号-5000,名称-5000,生产厂家-7000,检定单位-7000,出厂编号-7000,规格型号-5000,精度-5000,以检日期-5000,周期-3000,方式-3000,费用-3000";
                data.Columns["xu"].SetOrdinal(0);
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, UnitName + "-仪器设备一览表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "Info.xls");
            }
            else
                return null;
        }
    }
}
