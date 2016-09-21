using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.IO;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    [Authorization]
    public class SalesManageController : Controller
    {
        //
        // GET: /SalesManage/
        #region [备案查询]

        public ActionResult Index()
        {
            return View();
        }

        public FileResult RecordToExcel()
        {
            #region MyRegion
            //string ProjectName = Request["ProjectName"];
            //string PlanID = Request["PlanID"];
            //string RecordContent = Request["RecordContent"];
            //string SpecsModels = Request["SpecsModels"];
            //string BelongArea = Request["BelongArea"];
            //string StartDate = Request["StartDate"];
            //string EndDate = Request["EndDate"];
            //string WorkChief = Request["WorkChief"];
            //string State = Request["State"];
            //string HState = Request["HState"];
            //string where = "";

            //string s = "";
            //if (!string.IsNullOrEmpty(ProjectName))
            //{
            //    s += " PlanName like '%" + ProjectName + "%' and";
            //}
            //if (!string.IsNullOrEmpty(PlanID))
            //{
            //    s += " PlanID like '%" + PlanID + "%'  and";
            //}
            //if (!string.IsNullOrEmpty(RecordContent))
            //{
            //    s += " MainContent like '%" + RecordContent + "%' and";
            //}
            //if (!string.IsNullOrEmpty(SpecsModels))
            //{
            //    s += " SpecsModels like '%" + SpecsModels + "%' and";
            //}
            //if (!string.IsNullOrEmpty(BelongArea))
            //{
            //    s += " BelongArea like '%" + BelongArea + "%' and";
            //}
            //if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            //{
            //    s += " RecordDate between  '" + StartDate + "' and '" + EndDate + "' and";
            //}
            //if (!string.IsNullOrEmpty(WorkChief))
            //{
            //    s += " Manager like '%" + WorkChief + "%' and";
            //}
            //if (!string.IsNullOrEmpty(State))
            //{
            //    if (State == "0")
            //    {
            //        s += " (State =1 or State =2 or State =3 or State =4 ) and";
            //    }
            //    else
            //    {
            //        s += " State =" + State + " and";
            //    }
            //}
            //if (!string.IsNullOrEmpty(HState))
            //{
            //    s += " IsPay =" + HState + " ";
            //}
            //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
            //{
            //    s = s.Substring(0, s.Length - 3);
            //}
            //if (!string.IsNullOrEmpty(s)) { where = " where " + s; }

            //string strErr = "";
            //DataTable data = SalesManage.GetRecordToExcel(where, ref strErr);

            #endregion
            //

            string ProjectName = Request["ProjectName"];
            string PlanID = Request["PlanID"];
            string RecordContent = Request["RecordContent"];
            string SpecsModels = Request["SpecsModels"];
            string BelongArea = Request["BelongArea"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string WorkChief = Request["WorkChief"];
            string State = Request["State"];
            string HState = Request["HState"];
            string where = "";
            string ordercontent = "";
            string specification = "";
            string s = "";
            if (!string.IsNullOrEmpty(ProjectName))
            {
                s += " and PlanName like '%" + ProjectName + "%'";
            }
            if (!string.IsNullOrEmpty(PlanID))
            {
                s += " and  PlanID like '%" + PlanID + "%' ";
            }
            if (!string.IsNullOrEmpty(RecordContent))
            {
                ordercontent = " and  OrderContent like '%" + RecordContent + "%' ";
            }
            if (!string.IsNullOrEmpty(SpecsModels))
            {
                specification = " and  Specifications like '%" + SpecsModels + "%' ";
            }
            if (!string.IsNullOrEmpty(BelongArea))
            {
                s += " and  BelongArea like '%" + BelongArea + "%' ";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " and  RecordDate between  '" + StartDate + "' and '" + EndDate + "' ";
            }
            if (!string.IsNullOrEmpty(WorkChief))
            {
                s += " and  WorkChief like '%" + WorkChief + "%' ";
            }
            if (!string.IsNullOrEmpty(State))
            {
                if (State == "0")
                {
                    // s += " and  (State =1 or State =2 or State =3 or State =4 )";
                }
                else
                {
                    s += " and  PState =" + State + "";
                }
            }
            if (!string.IsNullOrEmpty(HState))
            {
                s += " and  IsPay =" + HState + " ";
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
            {
                //  s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " " + s; }


            DataTable dt = SalesManage.GetRecordToExcel(where, ordercontent, specification);

            if (dt != null)
            {
                string strCols = "编号-6000,备案编号-6000,产品名称-6000,备案日期-6000,项目名称-6000,工程编号-5000,规格型号-5000,业务负责人-3000,";
                strCols += "所属区域-3000,渠道来源-6000,进度-5000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "备案信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "备案信息表.xls");
            }
            else
                return null;

        }
        public ActionResult CheckDetail()
        {
            return View();
        }
        public ActionResult RecordInfo()
        {
            ProjectBasInfo Project = new ProjectBasInfo();
            // Project.PID = SalesManage.GetNewPid();
            Project.Manager = GAccount.GetAccountInfo().UserName;
            Project.PID = SalesManage.GetTopPID();
            return View(Project);
        }


        public ActionResult RecordInfoF()
        {
            ProjectBasInfo Project = new ProjectBasInfo();
            // Project.PID = SalesManage.GetNewPid();
            Project.Manager = GAccount.GetAccountInfo().UserName;
            Project.PID = SalesManage.GetTopPID();
            Project.ISF = "1";
            return View(Project);
        }
       
        public ActionResult CheckPlanIDandPlanName()
        {
            ProjectBasInfo Project = new ProjectBasInfo();
            Project.PlanID = Request["PlanID"].ToString();
            Project.PlanName = Request["PlanName"].ToString();
            Project.BelongArea = Request["BelongArea"].ToString();
            int  i = SalesManage.CheckPlanIDandPlanName(Project);
            if (i<=0)
                return Json(new { success = false });
            else
                return Json(new { success = true });
        }

        public ActionResult GetCheckDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";
            UIDataTable udtTask = SalesManage.GetCheckDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveRecordInfo()
        {

            Sales_RecordInfo recordInfo = new Sales_RecordInfo();
            recordInfo.PID = Request["PID"].ToString();
            recordInfo.RecordDate = Convert.ToDateTime(Request["RecordDate"]);
            recordInfo.PlanID = Request["PlanID"].ToString();
            recordInfo.PlanName = Request["PlanName"].ToString();
            recordInfo.Remark = Request["Remark"].ToString();
            string[] arrID = Request["WID"].Split(',');
            string[] arrMain = Request["OrderContent"].Split(',');
            string[] arrDevice = Request["Specifications"].Split(',');
            string[] arrSpecs = Request["Unit"].Split(',');
            string[] arrNum = Request["SalesNum"].Split(',');
            string[] arrChief = Request["Amount"].Split(',');
            string[] arrCon = Request["PurchaseDate"].Split(',');
            string[] arrTel = Request["Tel"].Split(',');
            string[] arrArea = Request["BelongArea"].Split(',');
            string[] arrOrder = Request["OrderTime"].Split(',');
            string[] arrChannels = Request["ChannelsFrom"].Split(',');

            string strErr = "";
            Sales_RecordDetail deInfo = new Sales_RecordDetail();
            List<Sales_RecordDetail> detailList = new List<Sales_RecordDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new Sales_RecordDetail();
                deInfo.PID = recordInfo.PID;
                deInfo.ID = Convert.ToInt32(arrID[i]);
                deInfo.MainContent = arrMain[i].ToString();
                deInfo.DeviceName = arrDevice[i].ToString();
                deInfo.SpecsModels = arrSpecs[i].ToString();
                deInfo.SalesNum = Convert.ToInt32(arrNum[i]);
                deInfo.WorkChief = arrChief[i].ToString();
                deInfo.Constructor = arrCon[i].ToString();
                deInfo.Tel = arrTel[i].ToString();
                deInfo.BelongArea = arrArea[i].ToString();
                deInfo.OrderTime = Convert.ToDateTime(arrOrder[i]);
                deInfo.ChannelsFrom = arrChannels[i].ToString();

                detailList.Add(deInfo);
            }

            bool b = SalesManage.AddRecordInfo(recordInfo.PID, recordInfo, detailList, GAccount.GetAccountInfo().UserName, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult GetSalesGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetSalesGridInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, "", "");
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecordInfoByPID()
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
            string PID = Request["PID"];
            if (!string.IsNullOrEmpty(PID)) { where = " and PID='" + PID + "'"; }


            UIDataTable udtTask = SalesManage.GetSalesGridInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, "", "");
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDetailGrid()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string PID = Request["ID"].ToString();
            UIDataTable udtTask = SalesManage.GetProjectDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, PID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetConfigInfo()
        {
            string taskType = Request["TaskType"].ToString();
            DataTable dt = SalesManage.GetConfigInfo(taskType);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = Dt2String(dt, "Text") });
        }
        public ActionResult GetSearchData(tk_SalesGrid salesgrid)
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                #region MyRegion
                //string ProjectName = Request["ProjectName"];
                //string PlanID = Request["PlanID"];
                //string RecordContent = Request["RecordContent"];
                //string SpecsModels = Request["SpecsModels"];
                //string BelongArea = Request["BelongArea"];
                //string StartDate = Request["StartDate"];
                //string EndDate = Request["EndDate"];
                //string WorkChief = Request["WorkChief"];
                //string State = Request["State"];
                //string HState = Request["HState"]; 
                #endregion
                string ProjectName = salesgrid.PlanName;
                string PlanID = salesgrid.PlanID;
                string RecordContent = salesgrid.MainContent;
                string SpecsModels = salesgrid.SpecsModels;
                string BelongArea = Request["BelongArea"];
                string StartDate = salesgrid.StartDate;
                string EndDate = salesgrid.EndDate;
                string WorkChief = salesgrid.Manager;
                string State = Request["State"];
                string HState = Request["HState"];
                string where = "";
                string ordercontent = "";
                string specification = "";
                string s = "";
                if (!string.IsNullOrEmpty(ProjectName))
                {
                    s += " and PlanName like '%" + ProjectName + "%'";
                }
                if (!string.IsNullOrEmpty(PlanID))
                {
                    s += " and  PlanID like '%" + PlanID + "%' ";
                }
                if (!string.IsNullOrEmpty(RecordContent))
                {
                    ordercontent = " and  OrderContent like '%" + RecordContent + "%' ";
                }
                if (!string.IsNullOrEmpty(SpecsModels))
                {
                    specification = " and  Specifications like '%" + SpecsModels + "%' ";
                }
                if (!string.IsNullOrEmpty(BelongArea))
                {
                    s += " and  BelongArea like '%" + BelongArea + "%' ";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and  RecordDate between  '" + StartDate + "' and '" + EndDate + "' ";
                }
                if (!string.IsNullOrEmpty(WorkChief))
                {
                    s += " and  WorkChief like '%" + WorkChief + "%' ";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "0")
                    {
                        // s += " and  (State =1 or State =2 or State =3 or State =4 )";
                    }
                    else
                    {
                        s += " and  PState =" + State + "";
                    }
                }
                if (!string.IsNullOrEmpty(HState))
                {
                    s += " and  IsPay =" + HState + " ";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
                {
                    //  s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = " " + s; }


                UIDataTable udtTask = SalesManage.GetSalesGridInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, ordercontent, specification);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
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
        public ActionResult RecordManage()
        {
            return View();
        }
        public ActionResult GetOrdersDetails()
        {
            string OID = Request["oID"];
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";
            UIDataTable udtTask = SalesManage.GetOrdersDetails(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //报价详细视图
        public ActionResult RecordOffer()
        {
            return View();
        }
        public ActionResult GetRecordOffer()
        {
            string Xid = Request["Xid"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "12";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetRecordOffer(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"].ToString()) - 1, Xid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRecordOfferInfo()
        {
            string Xid = Request["Xid"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "12";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetRecordOfferInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"].ToString()) - 1, Xid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //销售详细视图
        public ActionResult OrdersInfo()
        {
            return View();
        }
        public ActionResult GetOrderInfoGrid()
        {
            string Xid = Request["Xid"];
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "12";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetOrderInfoGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Xid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOrderInfoDetailGrid()
        {
            string Xid = Request["Xid"];
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "12";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetOrderInfoDetailGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Xid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectBasInfoRelBill()
        {
            string PID = Request["ID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.ProjectBasInfoRelBill(PID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        public ActionResult PrintProjectBasInfo()
        {
            ProjectBasInfo ProjectB = SalesManage.getProjectBaseInfo(Request["PID"].ToString());

            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            DataTable dt = SalesManage.GetPrintProjectDetail(ProjectB.PID);
            sb.Append(" <div id='PrintArea' style='page-break-after: always; height :1000px;'>");
            sb.Append("<div id='' style='text-align:center;font-size:18px; margin-top:20px;'> 北京市燕山工业燃气设备有限公司<br />备案项目<table class='tabInfo' style='width: 98%; margin-left: 8px; margin-top: 10px;border: 1px solid #000;cellspacing:0px; cellpadding:0px;'> <tr style='background-color: #88c9e9; text-align: left; '> <td colspan='4' style='border: 1px solid #000;'>   产品信息</td> </tr> <tr><td style='border: 1px solid #000;'  colspan='4' align='left'> 备案日期:" + ProjectB.RecordDate + "</td> </tr></table></div>");
            sb2.Append("<div><table class='tabInfo' style='width: 98%; height :400px; margin-left: 8px;border: 1px solid #000;cellspacing:0px; cellpadding:0px;'><tr style='background-color: #88c9e9; text-align: left; '><td colspan='4' style='border: 1px solid #000;' > 项目信息</td></tr><tr><td style='border: 1px solid #000;'>工程编号：</td><td style='border: 1px solid #000;' class='LeftTd'>" + ProjectB.PlanID + "</td><td style='border: 1px solid #000;'>项目名称：</td><td style='border: 1px solid #000;' class='LeftTd'>" + ProjectB.PlanName + "</td></tr><tr><td style='border: 1px solid #000;'>业务负责人：</td><td class='LeftTd' style='border: 1px solid #000;'>" + ProjectB.WorkChief + "</td><td style='border: 1px solid #000;'>施工方：</td><td style='border: 1px solid #000;' class='LeftTd'>" + ProjectB.Constructor + "</td></tr><tr><td style='border: 1px solid #000;'>电话：</td><td class='LeftTd' style='border: 1px solid #000;'>" + ProjectB.Tel + "</td><td style='border: 1px solid #000;'>所属区域：</td><td class='LeftTd' style='border: 1px solid #000;'>" + ProjectB.BelongArea + "</td></tr><tr><td style='border: 1px solid #000;'>预计订购时间：</td><td class='LeftTd' style='border: 1px solid #000;'>" + ProjectB.RecordDate + "</td> <td style='border: 1px solid #000;'>渠道来源：</td> <td style='border: 1px solid #000;' class='LeftTd'>" + ProjectB.ChannelsFrom + "</td></tr><tr><td style='border: 1px solid #000;'>备案人：</td><td style='border:1px solid #000;' colspan=' 4' style=' text-align :left '>" + ProjectB.Manager + "</td></tr><tr><td  style='border: 1px solid #000;'>备注：</td><td colspan='4'  style='border: 1px solid #000;' style=' text-align :left '>" + ProjectB.Remark + "</td></tr></table></div></div>");
            string html = "";
            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<div style='overflow-y:auto;'><table id='myTable' style='width: 98%; margin-left: 8px;border: 1px solid #000;cellspacing:0px; cellpadding:0px; ' cellpadding=' 0' cellspacing='0' class='tabInfo'><tr align='center' valign='middle'><th  style='width: 10%;border: 1px solid #000;' class='th'>序号</th><th style='width: 10%;border: 1px solid #000;' class='th'>货品编号</th><th style='width: 20%;border: 1px solid #000;' class='th'>货品名称</th><th style='width: 20%;border: 1px solid #000;' class='th'>规格型号</th><th style='width: 10%;border: 1px solid #000;' class='th'>单位</th><th style='width: 10%;border: 1px solid #000;' class='th'>数量</th><th style='width: 20%;border: 1px solid #000;' class='th' >备注</th></tr><tbody id='DetailInfo'>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr  id ='DetailInfo' + rowCount + ''><td style='border: 1px solid #000;'><lable class='labRowNumber'" + i + "' id='RowNumber'" + i + "'>" + (i + 1) + "</lable> </td><td style='display:none;border: 1px solid #000;'><lable class='labAmount" + i + "  id='ProductID" + i + "'>" + dt.Rows[i]["ProductID"] + "</lable></td><td style='border: 1px solid #000;' ><lable class='labProName" + i + "' id='ProName" + i + "'>" + dt.Rows[i]["OrderContent"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labSpec" + i + "' id='Spec" + i + "'>" + dt.Rows[i]["Specifications"] + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labAmount" + i + "'  id='Unit" + i + "'>" + dt.Rows[i]["Unit"] + "</lable></td><td style='border: 1px solid #000;'><lable class='labAmount" + i + "'  id='Amount" + i + "'>" + dt.Rows[i]["Amount"] + "</lable></td><td colspan='3' style='border: 1px solid #000;'><lable class='labAmount" + i + "'  id='Remark" + i + "'>" + dt.Rows[i]["Remark"] + "</lable></td></tr>");
                    //<td style='display:none;border: 1px solid #000;'><lable class='labPID" + i + " ' id='PID" + i + "'>" + dt.Rows[i]["PID"] + "</lable> </td>

                }
                sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > dt.Rows.Count)
                        length = 6 * i + dt.Rows.Count % 6;
                    sb1.Append("<div style='overflow-y:auto;'><table id='myTable' style='width: 98%; margin-left: 8px;border: 1px solid #000;cellspacing:0px; cellpadding:0px;' cellpadding=' 0' cellspacing='0' class='tabInfo'><tr align='center' valign='middle'><th style='width: 10%; border: 1px solid #000;' class='th'>序号</th><th style='width: 10%;border: 1px solid #000;' class='th'>货品编号</th><th style='width: 20%;border: 1px solid #000;' class='th'>货品名称</th><th style='width: 20%;border: 1px solid #000;' class='th'>规格型号</th><th style='width: 10%;border: 1px solid #000;' class='th'>单位</th><th style='width: 10%;border: 1px solid #000;' class='th'>数量</th><th style='width: 10%;border: 1px solid #000;' class='th'>备注</th></tr><tbody id='DetailInfo'>");

                    for (int j = a; j < length; j++)
                    {
                        sb1.Append("<tr  id ='DetailInfo' + rowCount + ''><td style='border: 1px solid #000;'><lable class='labRowNumber'" + j + "' id='RowNumber'" + j + "'>" + (j + 1) + "</lable> </td><td style='display:none;border: 1px solid #000;' ><lable class='labAmount" + j + "  id='ProductID" + j + "'>'" + dt.Rows[j]["ProductID"] + "</lable></td><td style='border: 1px solid #000;'><lable class='labProName" + j + "' id='ProName" + j + "'>" + dt.Rows[j]["OrderContent"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labSpec" + j + "' id='Spec" + j + "'>" + dt.Rows[j]["Specifications"] + "</lable> </td><td style='border:1px solid #000;'><lable class='labAmount" + j + "'  id='Unit" + j + "'>" + dt.Rows[j]["Unit"] + "</lable></td><td style='border: 1px solid #000;'><lable class='labAmount" + j + "'  id='Amount" + j + "'>" + dt.Rows[j]["Amount"] + "</lable></td><td style='border: 1px solid #000;'><lable class='labAmount" + j + "'  id='Remark" + j + "'>" + dt.Rows[j]["Remark"] + "</lable></td><td style='display:none;border: 1px solid #000;'><lable class='labPID" + j + " ' id='PID" + j + "'>" + dt.Rows[j]["PID"] + "</lable> </td></tr>");
                    }
                    sb1.Append("</tbody></table></div>");
                    if ((length - a) < 6)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }

        public ActionResult GetPrintProjectDetail()
        {
            string PID = Request["ID"];
            DataTable dt = SalesManage.GetPrintProjectDetail(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult GetLogGrid()
        {
            string ID = Request["ID"];
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetLogGrid(ID, GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectBill()
        {
            string PID = Request["ID"];
            ProjectBasInfo project = SalesManage.getProjectBaseInfo(PID);
            return View(project);

        }
        #endregion

        #region [新增物品]
        public ActionResult ChangeProduct()
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
                strRowNum = "8";

            UIDataTable udtTask = SalesManage.GetPtype(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
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

            string UnitID = GAccount.GetAccountInfo().UnitID;
            string PName = Request["PName"].ToString();
            string PID = Request["PId"].ToString();
            string SPec = Request["Spec"].ToString();


            UIDataTable udtTask = SalesManage.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, UnitID, PName, PID, SPec);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\":" + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetChangePtypeList()
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
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string PName = Request["PName"].ToString();
            string PID = Request["PId"].ToString();
            string SPec = Request["Spec"].ToString();
            UIDataTable udtTask = SalesManage.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, UnitID, PName, PID, SPec);


            if (udtTask.DtData == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", udtTask.DtData) });
        }
        public ActionResult GetBasicDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = SalesManage.GetBasicDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveProject(ProjectBasInfo Project)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Project.CreateUser = account.UserName;
                Project.CreateTime = DateTime.Now;
                Project.UnitID = account.UnitID;
                Project.State = "0";
                Project.Pstate = "1";
                Project.IsPay = "0";
                Project.Validate = "v";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["OrderContent"].Split(',');
                string[] SpecsModels = Request["Specifications"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] PRemark = Request["PRemark"].Split(',');
                List<ProjectDetail> list = new List<ProjectDetail>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    if (MainContent[i] != "")
                    {
                        ProjectDetail projectdetail = new ProjectDetail();
                        projectdetail.PID = Project.PID;
                        projectdetail.ProductID = ProductID[i];
                        projectdetail.OrderContent = MainContent[i];
                        projectdetail.Unit = Unit[i];
                        projectdetail.Specifications = SpecsModels[i];
                        projectdetail.Remark = PRemark[i];
                        projectdetail.XID = Project.PID + string.Format("{0:D3}", i + 1);
                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            projectdetail.Amount = 0;
                        }
                        else
                        {
                            projectdetail.Amount = Convert.ToInt32(Amount[i]);
                        }
                        projectdetail.Remark = PRemark[i];
                        projectdetail.PurchaseDate = DateTime.Now;
                        list.Add(projectdetail);
                    }
                }

                bool b = SalesManage.AddProjectBaseInfo(Project, list, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = Project.PID;
                    salesLog.LogContent = "新增备案项目";
                    salesLog.ProductType = "新增备案项目";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true, Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
            //ProjectBasInfo Project = new ProjectBasInfo();
            //Project.PID = Request["PID"].ToString();
            //Project.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            //Project.PlanID = Request["PlanID"].ToString();
            //Project.PlanName = Request["PlanName"].ToString();
            //Project.WorkChief = Request["WorkChief"].ToString();
            //Project.Tel = Request["Tel"].ToString();
            //Project.Constructor = Request["Constructor"].ToString();
            //Project.BelongArea = Request["BelongArea"].ToString();
            //Project.RecordDate = Convert.ToDateTime(Request["RecordDate"]);
            //Project.ChannelsFrom = Request["ChannelsFrom"].ToString();
            //Project.Manager = Request["Manager"].ToString();
            //Project.Remark = Request["Remark"].ToString();



        }
        #endregion

        #region [报价管理]
        public ActionResult OfferManage()
        {
            string text = "报价审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        public ActionResult GetOfferGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetOfferGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearchOfferGrid(tk_SalesGrid tk_SalesGrid)
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "9";
                string ProjectName = tk_SalesGrid.PlanName;
                string PlanID = tk_SalesGrid.PlanID;
                string OfferTitle = tk_SalesGrid.OfferTitle;
                string BelongArea = Request["BelongArea"];
                string StartDate = tk_SalesGrid.StartDate;
                string EndDate = tk_SalesGrid.EndDate;
                string OfferContacts = tk_SalesGrid.Manager;
                string State = Request["State"];
                string HState = Request["HState"];
                string where = "";

                string s = "";
                if (!string.IsNullOrEmpty(ProjectName))
                {
                    s += "  and b.PlanName like '%" + ProjectName + "%' ";
                }
                if (!string.IsNullOrEmpty(PlanID))
                {
                    s += "  and b.PLanID like '%" + PlanID + "%'  ";
                }
                if (!string.IsNullOrEmpty(OfferTitle))
                {
                    s += " and a.OfferTitle like '%" + OfferTitle + "%' ";
                }
                if (!string.IsNullOrEmpty(BelongArea))
                {
                    s += "  and b.BelongArea like '%" + BelongArea + "%' ";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += "  and a.OfferTime between  '" + StartDate + "' and '" + EndDate + "' ";
                }
                if (!string.IsNullOrEmpty(OfferContacts))
                {
                    s += "  and a.OfferContacts like '%" + OfferContacts + "%' ";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "-3")
                    {
                        // s += " (State =1 or State =2 or State =3 or State =4 ) and";
                    }
                    else
                    {
                        s += "  and a.OState =" + State + " ";
                    }
                }
                if (!string.IsNullOrEmpty(HState))
                {
                    s += " and  b.IsPay =" + HState + " ";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
                {
                    // s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }

                UIDataTable udtTask = SalesManage.GetSearchOfferGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult UpdateOffer()
        {
            string BJID = Request.QueryString["BJID"].ToString();
            Offer offer = SalesManage.getOfferByBJID(BJID);
            return View(offer);
        }

        public ActionResult UpdateOfferF()
        {
            string BJID = Request.QueryString["BJID"].ToString();
            Offer offer = SalesManage.getOfferByBJID(BJID);
            return View(offer);
        }

        public ActionResult SHowAppOfferXX()
        {
            string BJID = Request.QueryString["id"].ToString();
            Offer offer = SalesManage.getOfferByBJID(BJID);
            return View(offer);
        }
        public ActionResult AddOffer()
        {
            Offer offer = new Offer();
            offer.BJID = SalesManage.GetNewBJID();
            if (Request.QueryString["BJID"] != null)
            {
                offer.PID = Request.QueryString["BJID"].ToString();
            }
            offer.OfferContacts = GAccount.GetAccountInfo().UserName;
             //offer.OfferTime = DateTime.Now.ToString();
            offer.ISF = "0";
            return View(offer);
        }


        public ActionResult AddOfferF()
        {
            Offer offer = new Offer();
            offer.BJID = SalesManage.GetNewBJID();
            if (Request.QueryString["BJID"] != null)
            {
                offer.PID = Request.QueryString["BJID"].ToString();
            }
            offer.OfferContacts = GAccount.GetAccountInfo().UserName;
            offer.OfferTime = DateTime.Now.ToString(); ;
            offer.ISF = "1";
            return View(offer);
        }
        public ActionResult SaveOffer(Offer offer)
        {
            #region OLD

            //Offer offer = new Offer();
            //offer.PID = Request["PID"].ToString();
            //offer.BJID = Request["BJID"].ToString();
            //offer.OfferTitle = Request["OfferTitle"].ToString();
            //offer.OfferTime = Convert.ToDateTime(Request["OfferTime"]);
            //offer.FKYD = Request["FKYD"].ToString();
            //offer.Description = Request["Description"].ToString();
            //offer.OfferContacts = Request["OfferContacts"].ToString(); 
            #endregion
            if (ModelState.IsValid)
            {

                //HttpPostedFileBase file = Request.Files[0];
                //byte[] fileByte = new byte[0];
                //string FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                ////SalesManage.InsertNewFile(CID, fileByte, FileName, ref strErr);
                Acc_Account account = GAccount.GetAccountInfo();
                offer.CreateUser = account.UserName;
                offer.OfferUnit = account.UnitName;
                offer.Validate = "v";
                offer.State = "0";
                offer.Ostate = "0";
                offer.CreateTime = DateTime.Now;
                // offer.Total = Convert.ToDecimal(0);
                string[] ProductID = Request["ProductID"].Split(',');
                string[] ProName = Request["OrderContent"].Split(',');
                string[] Spec = Request["SpecsModels"].Split(',');
                string[] Units = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] Total = Request["txtTotal"].Split(',');
                string[] Remark = Request["Remark"].Split(',');
                List<OfferInfo> list = new List<OfferInfo>();
                for (int i = 0; i < ProName.Length; i++)
                {
                    if (ProName[i] != "")
                    {
                        OfferInfo offerinfo = new OfferInfo();
                        offerinfo.BJID = offer.BJID;
                        offerinfo.XID = offer.BJID + string.Format("{0:D3}", i + 1);
                        offerinfo.ProductID = ProductID[i].ToString();
                        offerinfo.OrderContent = ProName[i].ToString();
                        offerinfo.Specifications = Spec[i].ToString();
                        offerinfo.Supplier = Supplier[i].ToString();
                        offerinfo.Unit = Units[i].ToString();
                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            offerinfo.Amount = 0;
                        }
                        else
                        {
                            offerinfo.Amount = Convert.ToInt32(Amount[i]);
                        }
                        if (!string.IsNullOrEmpty(UnitPrice[i]))
                            offerinfo.UintPrice = Convert.ToDecimal(UnitPrice[i]);
                        offerinfo.Total = Total[i].ToString();
                        offerinfo.Remark = Remark[i].ToString();
                        offerinfo.CreateTime = DateTime.Now.ToString();
                        offerinfo.CreateUser = GAccount.GetAccountInfo().UserName;
                        offerinfo.Validate = "v";
                        list.Add(offerinfo);
                    }
                }
                string strErr = "";
                bool b = SalesManage.SaveOffer(offer, list,ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = offer.BJID;
                    salesLog.LogContent = "新增报价单";
                    salesLog.ProductType = "报价单新增";
                    salesLog.SalesType = "Project";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });

                    //return Json(new { success = true, Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = false, Msg = "保存出错" + "/" + strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult getProjectDetailGrid()
        {
            string PID = Request["PID"];

            DataTable dt = SalesManage.getProjectDetailGrid(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetOfferInfoGrid()
        {
            string BJID = Request["BJID"].ToString();

            DataTable dt = SalesManage.GetOfferInfoGrid(BJID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetOfferInfoByBJID()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string BJID = Request["BJID"];
            if (!string.IsNullOrEmpty(BJID))
            {
                where += " and  BJID='" + BJID + "'";
            }
            UIDataTable udtTask = SalesManage.GetOfferGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //相关物品
        public ActionResult GetOfferDetailGrid()
        {
            string BJID = Request["BJID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetOfferDetailGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, BJID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //详细-报价订货单
        public ActionResult GetBJOrders()
        {
            string PID = Request["PID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";
            UIDataTable udtTask = SalesManage.GetBJOrders(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, PID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUpdateOffer(Offer offer)
        {
            if (ModelState.IsValid)
            {
                #region old 1011k
                //Offer offer = new Offer();
                //offer.PID = Request["PID"].ToString();
                //offer.BJID = Request["BJID"].ToString();
                //offer.OfferTitle = Request["OfferTitle"].ToString();
                //offer.OfferTime = Convert.ToDateTime(Request["OfferTime"]);
                //offer.FKYD = Request["FKYD"].ToString();
                //offer.Description = Request["Description"].ToString();
                //offer.OfferContacts = Request["OfferContacts"].ToString(); 
                #endregion
                string[] XID = Request["XID"].Split(',');
                string[] ProductID = Request["ProductID"].Split(',');
                string[] ProName = Request["OrderContent"].Split(',');
                string[] Spec = Request["SpecsModels"].Split(',');
                string[] Units = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] Total = Request["txtTotal"].Split(',');
                string[] Remark = Request["Remark"].Split(',');
                List<OfferInfo> list = new List<OfferInfo>();
                for (int i = 0; i < ProName.Length; i++)
                {
                    if (ProName[i] != "")
                    {
                        OfferInfo offerinfo = new OfferInfo();
                        offerinfo.BJID = offer.BJID;
                        offerinfo.XID = offer.BJID + string.Format("{0:D3}", i + 1);
                        offerinfo.ProductID = ProductID[i].ToString();
                        offerinfo.OrderContent = ProName[i].ToString();
                        offerinfo.Specifications = Spec[i].ToString();
                        offerinfo.Supplier = Supplier[i].ToString();
                        offerinfo.Unit = Units[i].ToString();
                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            offerinfo.Amount = 0;
                        }
                        else
                        {
                            offerinfo.Amount = Convert.ToInt32(Amount[i]);
                        }
                        if (!string.IsNullOrEmpty(UnitPrice[i]))
                            offerinfo.UintPrice = Convert.ToDecimal(UnitPrice[i]);
                        offerinfo.Total = Total[i].ToString();
                        offerinfo.Remark = Remark[i].ToString();
                        list.Add(offerinfo);
                    }
                }
                string strErr = "";
                bool b = SalesManage.SaveUpdateOffer(offer, list, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = offer.BJID;
                    salesLog.LogContent = "修改报价单";
                    salesLog.ProductType = "报价单修改";
                    salesLog.SalesType = "Project";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Success = "false", Msg = "验证不通过" });
            }
        }
        //撤销报价单把Vali
        public ActionResult CancelOffer()
        {
            string ID = Request["ID"].ToString();
            string strErr = "";
            bool b = SalesManage.CancelOffer(ID, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }


        public FileResult OfferToExcel()
        {
            string ProjectName = Request["ProjectName"];
            string PlanID = Request["PlanID"];
            string OfferTitle = Request["OfferTitle"];
            string BelongArea = Request["BelongArea"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string OfferContacts = Request["OfferContacts"];
            string State = Request["State"];
            string HState = Request["HState"];
            string where = "";

            string s = "";
            if (!string.IsNullOrEmpty(ProjectName))
            {
                s += " PlanName like '%" + ProjectName + "%' and";
            }
            if (!string.IsNullOrEmpty(PlanID))
            {
                s += " PLanID like '%" + PlanID + "%'  and";
            }
            if (!string.IsNullOrEmpty(OfferTitle))
            {
                s += " OfferTitle like '%" + OfferTitle + "%' and";
            }
            if (!string.IsNullOrEmpty(BelongArea))
            {
                s += " BelongArea like '%" + BelongArea + "%' and";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " OfferTime between  '" + StartDate + "' and '" + EndDate + "' and";
            }
            if (!string.IsNullOrEmpty(OfferContacts))
            {
                s += " Manager like '%" + OfferContacts + "%' and";
            }
            if (!string.IsNullOrEmpty(State))
            {
                if (State == "0")
                {
                    s += " (State =1 or State =2 or State =3 or State =4 ) and";
                }
                else
                {
                    s += " State =" + State + " and";
                }
            }
            if (!string.IsNullOrEmpty(HState))
            {
                s += " IsPay =" + HState + " ";
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
            {
                s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
            //
            string strErr = "";
            DataTable data = SalesManage.GetOfferToExcel(where, ref strErr);
            if (data != null)
            {
                string strCols = "报价单号-6000,项目编号-6000,报价标题-6000,报价说明-5000,报价人-6000,报价单位-5000," +
                    "总金额-5000,报价日期-6000,报价状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "报价信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "报价信息表.xls");
            }
            else
                return null;

        }

        public ActionResult PrintOffer()
        {
            Offer offer = SalesManage.getOfferByBJID(Request["BJID"].ToString());
            DataTable dt = SalesManage.GetOfferInfoGrid(offer.BJID);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("  <div id='PrintArea' style='page-break-after: always;height :1000px;'>");
            sb.Append("<div><table class='tabInfo' style='width: 99%; margin-left: 8px; margin-top: 10px;'><tr style='text-align:center ;font :bold ;margin-top:20px;font-size:24px;'><td colspan='6' > 报价单</td></tr><tr style='text-align:right'><td colspan='6' style='text-align:right'>安外外馆东后街35号（100011）</br>电话：64257827    64263855 </br>传真：64263855</td></tr><tr><td colspan='6' style='text-align:center'>北京市燕山工业燃气设备有限公司</td></tr><tr><td style='border: 1px solid #000;' colspan='2'>   备案编号：" + offer.PID + "</td><td style='border: 1px solid #000;' colspan='2'>   报价标题:" + offer.OfferTitle + " </td><td style='border: 1px solid #000;' colspan='2'>报价日期" + offer.OfferTime + "</td></tr><tr style='border: 1px solid #000; '><td width='200'>客户</td><td colspan='2' width='200' >" + offer.Customer + "</td><td>客户电话</td><td colspan='2'>" + offer.CustomerTel + "</td></tr><tr><td colspan='3'>承蒙贵处垂询，现提供下列报价参考：</td><td colspan='3'>单位:万元（人民币）</td></tr></table></div>");//background-color: #88c9e9;<tr><td colspan='6' style='border: 1px solid #000;'>   报价物品</td></tr>
            sb2.Append("<div><table class='tabInfo' style='width: 99%; margin-left: 8px; margin-top: 2px;'><tr style=' text-align: left; '>  <td colspan='4' style='border: 1px solid #000;' >报价说明</td></tr><tr>  <td style='border: 1px solid #000;'>合同条件</td><td colspan='3'> " + offer.FKYD + "</td></tr><tr>  <td style='border: 1px solid #000;'>报价金额</td><td colspan='3' style='border: 1px solid #000;'>" + offer.Total/10000 + "</td></tr><tr>  <td style='border: 1px solid #000;'>报价说明</td><td colspan='3' style='border: 1px solid #000;'>" + offer.Description + "</td></tr><tr style='text-align: left; '>  <td colspan='4' style='border: 1px solid #000;'>报价人信息</td></tr><tr>  <td style='border: 1px solid #000;'>报价人</td><td colspan='3' style='border: 1px solid #000;'>" + offer.OfferContacts + "</td></tr></table></div></div>");

            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<div><table id='myTable' style='width:99%; margin-left: 8px; margin-top: 2px;' cellpadding='0' cellspacing='0' class='tabInfo'><tr  align='center' valign='middle'><th style='width: 5%;border: 1px solid #000;' class='th'>序号</th><th style='width: 15%;border: 1px solid #000;' class='th'>物品编号</th><th style='width: 15%;border: 1px solid #000;' class='th'>物品名称 </th><th style='width: 20%;border: 1px solid #000;' class='th'> 规格型号 </th><th style='width:10%;border: 1px solid #000;' class='th'>单位</th><th style='width: 10%;border: 1px solid #000;' class='th'>数量</th><th style='width: 10%;border: 1px solid #000;' class='th'>单价</th><th style='width: 10%;border: 1px solid #000;' class='th'>金额</th></tr><tbody id='DetailInfo'>");//<th style='width: 20%;border: 1px solid #000;' class='th'>备注</th><th style='width: 10%;border: 1px solid #000;' class='th'>供应商</th>

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr  id ='DetailInfo" + i + "'><td  style='border: 1px solid #000;'><lable class='labi" + i + " ' id='i" + i + "'>" + (i + 1) + "</lable> </td><td  style='border: 1px solid #000;'><lable class='labProductID" + i + " ' id='ProductID" + i + "'>" + dt.Rows[i]["ProductID"] + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labProName" + i + " ' id='OrderContent" + i + "'>" + dt.Rows[i]["OrderContent"] + "</lable> </td><td  style='border: 1px solid #000;'><lable class='labSpec" + i + " ' id='Spec" + i + "'>" + dt.Rows[i]["Specifications"] + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labUnits" + i + " ' id='Units" + i + "'>" + dt.Rows[i]["Unit"] + "</lable> </td><td style='border: 1px solid #000;' ><lable>" + dt.Rows[i]["Amount"] + "</lable></td><td  style='border: 1px solid #000;'><lable class='labUnitPrice" + i + " ' id='UnitPrice" + i + "'>" + Convert.ToDecimal(dt.Rows[i]["UintPrice"]) / 10000 + "</lable> </td><td  style='border: 1px solid #000;'><lable>" + Convert.ToDecimal(dt.Rows[i]["Total"]) / 10000 + "</lable></td></tr>");//<td  style='border: 1px solid #000;'><lable class='labRemark" + i + " ' id='Remark" + i + "'>" + dt.Rows[i]["Remark"] + "</lable> </td><td  style='border: 1px solid #000;'>" + dt.Rows[i]["Supplier"] + " </td>

                }
                sb1.Append("</tbody></table></div>");
                //  sb.Append("</div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    //if (length > dt.Rows.Count)

                    if (length > dt.Rows.Count)
                        length = 6 * i + dt.Rows.Count % 6;
                    sb1.Append("<div><table id='myTable' style='width:99%; margin-left: 8px; margin-top: 2px;' cellpadding='0' cellspacing='0' class='tabInfo'><tr align='center' valign='middle'><th style='width: 5%;border: 1px solid #000;' class='th'>序号</th><th style='width: 15%;border: 1px solid #000;' class='th'>物品编号</th><th style='width: 15%;border: 1px solid #000;' class='th'>物品名称 </th><th style='width: 20%;border: 1px solid #000;' class='th'> 规格型号 </th><th style='width: 5%;border: 1px solid #000;' class='th'>单位</th><th style='width: 5%;border: 1px solid #000;' class='th'>数量</th><th style='width: 10%;border: 1px solid #000;' class='th'>单价</th><th style='width: 10%;border: 1px solid #000;' class='th'>金额</th></tr><tbody id='DetailInfo'>");//<th style='width:20%;border: 1px solid #000;' class='th'>备注</th><th style='width: 10%;border: 1px solid #000;' class='th'>供应商</th>

                    for (int j = a; j < length; j++)
                    {
                        sb1.Append("<tr  id ='DetailInfo" + j + "'><td  style='border: 1px solid #000;'><lable class='labi" + j + " ' id='i" + i + "'>" + (j + 1) + "</lable> </td><td  style='border: 1px solid #000;' ><lable class='labProductID" + j + " ' id='ProductID" + j + "'>" + dt.Rows[j]["ProductID"] + "</lable> </td><td   style='border: 1px solid #000;'><lable class='labProName" + i + " ' id='ProName" + j + "'>" + dt.Rows[j]["OrderContent"] + "</lable> </td><td   style='border: 1px solid #000;'><lable class='labSpec" + j + " ' id='Spec" + i + "'>" + dt.Rows[j]["Specifications"] + "</lable> </td><td   style='border: 1px solid #000;'><lable class='labUnits" + j + " ' id='Units" + j + "'>" + dt.Rows[j]["Unit"] + "</lable> </td><td   style='border: 1px solid #000;'><lable type='text' id='mount" + i + "' style='width:30px;'></lable></td><td  style='border: 1px solid #000;' ><lable class='labUnitPrice" + i + " ' id='UnitPrice" + j + "'>" + Convert.ToDecimal(dt.Rows[j]["UintPrice"]) / 10000 + "</lable> </td><td   style='border: 1px solid #000;'><lable  id='txtTotal" + i + "' style='width:60px;'>" + Convert.ToDecimal(dt.Rows[j]["Total"]) / 10000 + "</lable></td></tr>");//<td style='border: 1px solid #000;'><lable class='labRemark" + j + " ' id='Remark" + j + "'>" + dt.Rows[j]["Remark"] + "</lable> </td><td style='display:none;border: 1px solid #000;'><lable class='labPID" + i + " ' id='PID" + j + "'>" + dt.Rows[j]["XID"] + "</lable> </td><td  style='border: 1px solid #000;'><lable  id='Supplier" + j + "'>" + dt.Rows[j]["Supplier"] + "</lable> </td>

                    }
                    sb1.Append("</tbody></table></div>");
                    //sb.Append("</div>");
                    if ((length - a) < 6)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }

            }


            Response.Write(html);
            return View();

        }

        //报价详细
        public ActionResult OfferBill()
        {
            string BJID = Request["ID"];
            Offer offer = SalesManage.getOfferByBJID(BJID);
            return View(offer);
        }


        //报价上传文件
        // 上传附件 
        public ActionResult InsertBiddingNew()
        {
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;//获取上传的文件
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }

            tk_CFile fileUp = new tk_CFile();
            fileUp.CID = Request["RID"].ToString();
            //fileUp.FileType = Request["Types"].ToString();
            fileUp.CreateUser = acc.UserName.ToString();
            fileUp.CreateTime = DateTime.Now.ToString();
            fileUp.Validate = "v";
            SalesManage.InsertBiddingNew(fileUp, Filedata, ref strErr);
            return this.Json(new { });
            // return View("UpLoadManageNew", fileUp);
        }

        //获取报价的文件
        public ActionResult GetUploadFileGrid() 
        {
            string CID = Request["CID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = SalesManage.GetUploadFileGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, CID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);



        }
        //备案详细
        public ActionResult ShowProject() 
        {
            ProjectBasInfo project = new ProjectBasInfo();
           // project.PID = Request["PID"];
            project = SalesManage.getProjectBaseInfo(Request["PID"].ToString());
            return View(project);
        }
        #endregion

        #region [订单管理]

        public ActionResult AddOrder()
        {
            OrdersInfo orderinfo = new OrdersInfo();
            orderinfo.PID = Request["ID"];
            orderinfo.OrderID = SalesManage.GetNewOrderID();
            if (orderinfo.ContractID == "" || orderinfo.ContractID == null)
            {
                //string Str = SalesManage.GetNamePY(GAccount.GetAccountInfo().UserName);
                //string Dime = DateTime.Now.Year.ToString();// ("YYYY");
                //Dime = Dime.Substring(2, 2);
                //string MaxContractID = SalesManage.GetMaxContractID();
                //if (MaxContractID == "" || MaxContractID == null) 
                //{
                //    MaxContractID = "000";
                //}
                //else
                //{
                //    MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);
                //}

                //MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
                orderinfo.ContractID = SalesManage.GetMaxContractID();

            }
            orderinfo.ISF = "0";
            return View(orderinfo);
        }

        //添加非常规产品订单
        public ActionResult AddOrderF()
        {
            OrdersInfo orderinfo = new OrdersInfo();
            orderinfo.PID = Request["ID"];
            orderinfo.ISF = "1";
            orderinfo.ISHT = "0";
            orderinfo.OrderID = SalesManage.GetNewOrderID();
            if (orderinfo.ContractID == "" || orderinfo.ContractID == null)
            {
                //string Str = SalesManage.GetNamePY(GAccount.GetAccountInfo().UserName);
                //string Dime = DateTime.Now.Year.ToString();// ("YYYY");
                //Dime = Dime.Substring(2, 2);
                //string MaxContractID = SalesManage.GetMaxContractID();
                //if (MaxContractID == "" || MaxContractID == null) 
                //{
                //    MaxContractID = "000";
                //}
                //else
                //{
                //MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);}
                //MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
                orderinfo.ContractID = SalesManage.GetMaxContractID();

            }

            return View(orderinfo);
        }
        public ActionResult GetProject()
        {
            string ID = Request["ID"].ToString();
            DataTable dt = SalesManage.GetProject(ID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult AddContractOrder()
        {
            OrdersInfo orderinfo = new OrdersInfo();
            orderinfo.OrderID = SalesManage.GetNewOrderID();
            if (Request["id"] != "")
            {
                orderinfo.ContractID = Request["id"].ToString();
            }
            else
            {
                //string Str = SalesManage.GetNamePY(GAccount.GetAccountInfo().UserName);
                //string Dime = DateTime.Now.Year.ToString();// ("YYYY");
                //Dime = Dime.Substring(2, 2);
                // string MaxContractID = SalesManage.GetMaxContractID();
                //MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);
                //MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
                orderinfo.ContractID = SalesManage.GetMaxContractID();

            }

            return View(orderinfo);
        }
        public ActionResult SaveOrderInfo(OrdersInfo orderinfo)
        {
            // var errors = ModelState.Values.SelectMany(v => v.Errors); 
            //if (ModelState.IsValid)
            // {

            orderinfo.PID = Request["ID"];
            orderinfo.State = "0";
            orderinfo.Ostate = "0";
            orderinfo.ContractDate = DateTime.Now;
            orderinfo.CreateTime = DateTime.Now.ToString();
            orderinfo.CreateUser = GAccount.GetAccountInfo().UserName;
            orderinfo.Validate = "v";
            orderinfo.SalesType = "Sa01";
            orderinfo.Pstate = 0;
            orderinfo.SupplyTime = DateTime.Now;
            orderinfo.UnitID = GAccount.GetAccountInfo().UnitID;//1225k
            //if (Request["OrderAmount"] != null)
            //{
            //    orderinfo.OrderAmount = Convert.ToInt32(Request["OrderAmount"]);
            //    orderinfo.OrderActualAmount = Convert.ToInt32(Request["OrderAmount"]);
            //}
            //if (Request["OrderTotal"] != null)
            //{
            //    orderinfo.OrderTotal = Convert.ToDecimal(Request["OrderTotal"]);
            //    orderinfo.OrderActualTotal = Convert.ToDecimal(Request["OrderTotal"]);
            //}
            //  orderinfo.OrderActualAmount = 0;
            // orderinfo.OrderActualTotal = 0.0M;
            ////订单详细表
            List<Orders_DetailInfo> list = new List<Orders_DetailInfo>();
            string[] ProductID = Request["ProductID"].Split(',');
            string[] OrderContent = Request["OrderContent"].Split(',');
            string[] SpecsModels = Request["SpecsModels"].Split(',');
            string[] Supplier = Request["Supplier"].Split(',');
            string[] Unit = Request["Unit"].Split(',');
            string[] Amount = Request["Amount"].Split(',');
            string[] UnitPrice = Request["UnitPrice"].Split(',');
            string[] Subtotal = Request["Subtotal"].Split(',');
            string[] UnitCost = Request["UnitCost"].Split(',');
            string[] TotalCost = Request["TotalCost"].Split(',');
            string[] SaleNo = Request["SaleNo"].Split(',');
            string[] ProjectNo = Request["ProjectNo"].Split(',');
            string[] JNAME = Request["JNAME"].Split(',');
            string[] Technology = Request["Technology"].Split(',');
            //string[] DeliveryTime = Request["DeliveryTime"].Split(',');
            //string[] YPrice = Request["YPrice"].Split(',');
            //string[] TaxRate = Request["TaxRate"].Split(',');
            for (int i = 0; i < OrderContent.Length; i++)
            {

                if (OrderContent[i] != "")
                {
                    Orders_DetailInfo ordersDetail = new Orders_DetailInfo();
                    ordersDetail.PID = orderinfo.PID;
                    ordersDetail.ProductID = ProductID[i].ToString();
                    ordersDetail.OrderID = orderinfo.OrderID;
                    ordersDetail.DID = ordersDetail.OrderID + string.Format("{0:D3}", i + 1);
                    ordersDetail.OrderContent = OrderContent[i].ToString();
                    ordersDetail.SpecsModels = SpecsModels[i].ToString();
                    ordersDetail.Manufacturer = Supplier[i].ToString();
                    ordersDetail.OrderUnit = Unit[i].ToString();
                    ordersDetail.DeliveryTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(UnitPrice[i].ToString()))
                    { ordersDetail.Price = Convert.ToDecimal(UnitPrice[i].ToString()); }
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        ordersDetail.OrderNum = 0;
                        ordersDetail.ActualAmount = 0;
                    }
                    else
                    {
                        ordersDetail.OrderNum = Convert.ToInt32(Amount[i]);
                        ordersDetail.ActualAmount = Convert.ToInt32(Amount[i]);
                        orderinfo.OrderAmount += Convert.ToInt32(Amount[i]);
                        orderinfo.OrderActualAmount += Convert.ToInt32(Amount[i]);
                    }
                    //  ordersDetail.TaxRate = TaxRate[i].ToString();
                    if (string.IsNullOrEmpty(Subtotal[i]))
                    {
                        ordersDetail.Subtotal = 0.00M;
                        ordersDetail.ActualSubTotal = 0.00M;
                    }
                    else
                    {
                        ordersDetail.Subtotal = Convert.ToDecimal(Subtotal[i]);
                        ordersDetail.ActualSubTotal = Convert.ToDecimal(Subtotal[i]);
                        orderinfo.OrderTotal += Convert.ToDecimal(Subtotal[i]);
                        orderinfo.OrderActualTotal += Convert.ToDecimal(Subtotal[i]);
                    }

                    if (string.IsNullOrEmpty(UnitCost[i]))
                    {
                        ordersDetail.UnitCost = 0.0M;
                    }
                    else
                    {
                        ordersDetail.UnitCost = Convert.ToDecimal(UnitCost[i]);
                    }
                    if (string.IsNullOrEmpty(TotalCost[i]))
                    {
                        ordersDetail.TotalCost = 0.0M;
                    }
                    else { ordersDetail.TotalCost = Convert.ToDecimal(TotalCost[i]); }
                    ordersDetail.SaleNo = SaleNo[i].ToString();
                    ordersDetail.ProjectNo = ProjectNo[i].ToString();
                    ordersDetail.JNAME = JNAME[i].ToString();
                    ordersDetail.Technology = Technology[i].ToString();
                    //if (string.IsNullOrEmpty(DeliveryTime[i]))
                    //{
                    //    ordersDetail.DeliveryTime = DateTime.Now;
                    //}
                    //else
                    //{
                    //    ordersDetail.DeliveryTime = Convert.ToDateTime(DeliveryTime[i]);
                    //}
                    ordersDetail.CreateTime = DateTime.Now.ToString();
                    ordersDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    ordersDetail.Validate = "v";
                    ordersDetail.State = "0";
                    ordersDetail.IState = "0";
                    list.Add(ordersDetail);
                }
            }
            string strErr = "";
            bool b = SalesManage.SaveOrderInfo(orderinfo, list, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = orderinfo.OrderID;
                salesLog.LogContent = "新增订单";
                salesLog.ProductType = "订单新增";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                salesLog.SignTime = orderinfo.ExpectedReturnDate;
                salesLog.SalesType = "Project";
                SalesRetailMan.SaveLog(salesLog);
                return Json(new { success = true });

            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
            // }
            //else
            //{
            //    return Json(new { success = "false", Msg = "数据验证不通过" });
            //}
        }


        public ActionResult OrderInfoManage()
        {
            string text = "报价审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult GetOrderInfo()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetOrderInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearchOrderInfo(tk_SalesGrid tk_salesgrid)
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
                    strRowNum = "9";
                string ContractID = tk_salesgrid.ContractID;
                string OrderUnit = tk_salesgrid.OrderUnit;
                string UseUnit = tk_salesgrid.UseUnit;
                string OrderContent = tk_salesgrid.MainContent;
                string StartDate = tk_salesgrid.StartDate;
                string EndDate = tk_salesgrid.EndDate;
                string State = Request["State"].ToString();
                string HState = Request["HState"];

                string s = "";
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += " and ContractID like '%" + ContractID + "%'";
                }
                if (!string.IsNullOrEmpty(OrderUnit))
                {
                    s += " and OrderUnit like '%" + OrderUnit + "%'";
                }
                if (!string.IsNullOrEmpty(UseUnit))
                {
                    s += " and UseUnit like '%" + UseUnit + "%'";
                }
                if (!string.IsNullOrEmpty(OrderContent))
                {
                    s += " and OrderContent like '%" + OrderContent + "%'";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and DeliveryTime between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "y")
                    {
                        //  s += " (a.State =1 or a.State =2 or a.State =3 ) and";
                    }
                    else
                    {
                        s += " and a.OState ='" + State + "'";
                    }
                }
                if (!string.IsNullOrEmpty(HState))
                {
                    s += " and  IsHK ='" + HState + "' ";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
                {
                    //  s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = " " + s; }
                UIDataTable udtTask = SalesManage.GetOrderInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult OrderShipments()
        {
            Shipments shipments = new Shipments();
            shipments.OrderID = Request["OrderID"].ToString();
            shipments.ShipGoodsID = SalesManage.GetNewShipGoodsID();
            return View(shipments);
        }
        public ActionResult SaveOrderShip()
        {
            Shipments shipments = new Shipments();
            shipments.OrderID = Request["OrderID"].ToString();
            shipments.ShipGoodsID = Request["ShipGoodeID"].ToString();
            shipments.ShipmentDate = DateTime.Now;
            shipments.Remark = Request["Remark"].ToString();
            shipments.Orderer = Request["Orderer"].ToString();
            shipments.Shippers = Request["Shippers"].ToString();
            shipments.Validate = "v";
            shipments.CreateTime = DateTime.Now;
            shipments.ContractID = Request["ContractID"].ToString();
            List<Shipments_DetailInfo> List = new List<Shipments_DetailInfo>();
            string[] ProductID = Request["ProductID"].Split(',');
            string[] OrderContent = Request["OrderContent"].Split(',');
            string[] SpecsModels = Request["SpecsModels"].Split(',');
            string[] Supplier = Request["Supplier"].Split(',');
            string[] Unit = Request["Unit"].Split(',');
            string[] Amount = Request["Amount"].Split(',');
            string[] UnitPrice = Request["UnitPrice"].Split(',');
            // string[] Subtotal = Request["Subtotal"].Split(',');
            string[] oDID = Request["DID"].Split(',');
            for (int i = 0; i < OrderContent.Length - 1; i++)
            {
                Shipments_DetailInfo shipdetail = new Shipments_DetailInfo();
                shipdetail.ShipGoodsID = shipments.ShipGoodsID;
                shipdetail.DID = shipdetail.ShipGoodsID + string.Format("{0:D3}", i + 1);
                shipdetail.ProductID = ProductID[i].ToString();
                shipdetail.OrderContent = OrderContent[i].ToString();
                shipdetail.Specifications = SpecsModels[i].ToString();
                shipdetail.Supplier = Supplier[i].ToString();
                shipdetail.Unit = Unit[i].ToString();
                if (string.IsNullOrEmpty(Amount[i]))
                {
                    shipdetail.Amount = 0;
                }
                else { shipdetail.Amount = Convert.ToInt32(Amount[i]); }
                if (string.IsNullOrEmpty(UnitPrice[i])) { shipdetail.Price = 0.00M; }

                else
                {
                    shipdetail.Price = Convert.ToDecimal(UnitPrice[i]);
                }
                //if (string.IsNullOrEmpty(Subtotal[i]))
                //{
                //    shipdetail.Subtotal = 0.00M;
                //}
                //else { shipdetail.Subtotal = Convert.ToDecimal(Subtotal[i]); }

                shipdetail.CreateTime = DateTime.Now;
                List.Add(shipdetail);
            }
            List<Orders_DetailInfo> listDetail = new List<Orders_DetailInfo>();

            for (int i = 0; i < oDID.Length - 1; i++)
            {
                //修改订单的发货数量
                //Or  orderinfo = new OrdersInfo();
                Orders_DetailInfo detail = new Orders_DetailInfo();
                if (!string.IsNullOrEmpty(oDID[i]))
                {
                    detail.DID = oDID[i].ToString();
                }

                if (!string.IsNullOrEmpty(Amount[i]))
                {
                    detail.ShipmentNum = Convert.ToInt32(Amount[i]);
                }
                listDetail.Add(detail);
            }
            string strErr = "";
            bool b = SalesManage.SaveOrderShip(shipments, List, listDetail, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = shipments.ShipGoodsID;
                salesLog.LogContent = "新增发货单";
                salesLog.ProductType = "新增发货单";
                salesLog.SalesType = "Project";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }


        public ActionResult GetOrdersDetail()
        {
            string OrderID = Request["OrderID"].ToString();

            DataTable dt = SalesManage.GetOrdersDetail(OrderID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult GetOredersShipmentDetail()
        {
            string OrderID = Request["OrderID"].ToString();

            DataTable dt = SalesManage.GetOredersShipmentDetail(OrderID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetOrdersDetailBYDID()
        {
            string DID = Request["DID"].ToString();
            if (DID != "" || DID != null)
            {
                string[] s = DID.Split(',');
                if (s.Length > 1) { DID = DID.Substring(0, DID.Length - 1); }
            }

            DataTable dt = SalesManage.GetOrdersDetailBYDID(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateOrdersInfo()
        {
            string OrderID = Request.QueryString["OrderID"].ToString();
            OrdersInfo OrderInfo = SalesManage.GetOrdersByOrderID(OrderID);
            ViewData["PID"] = OrderInfo.PID;
            return View(OrderInfo);
        }

        //非常规订单修改
        public ActionResult UpdateOrdersInfoF()
        {
            string OrderID = Request.QueryString["OrderID"].ToString();
            OrdersInfo OrderInfo = SalesManage.GetOrdersByOrderID(OrderID);
            ViewData["PID"] = OrderInfo.PID;
            return View(OrderInfo);
        }


        public ActionResult SHowAppOrderXX()
        {

            string OrderID = Request.QueryString["id"].ToString();
            OrdersInfo OrderInfo = SalesManage.GetOrdersByOrderID(OrderID);
            return View(OrderInfo);
        }


        public ActionResult SaveUpdateOrderInfo(OrdersInfo orderinfo)
        {
            if (ModelState.IsValid)
            {
                #region Old 1011
                //OrdersInfo orderinfo = new OrdersInfo();
                ////orderinfo.PID = Request["Pid"].ToString();
                //orderinfo.OrderID = Request["OrderID"].ToString();
                //orderinfo.OrderUnit = Request["OrderUnit"].ToString();
                //orderinfo.OrderContactor = Request["OrderContactor"].ToString();
                //orderinfo.OrderTel = Request["OrderTel"].ToString();
                //orderinfo.OrderAddress = Request["OrderAddress"].ToString();
                //orderinfo.UseUnit = Request["UseUnit"].ToString();
                //orderinfo.UseContactor = Request["UseContactor"].ToString();
                //orderinfo.UseTel = Request["UseTel"].ToString();
                //orderinfo.UseAddress = Request["UseAddress"].ToString();
                //if (!string.IsNullOrEmpty(Request["Total"].ToString()))
                //{
                //    orderinfo.Total = Convert.ToDecimal(Request["Total"]);
                //}
                //orderinfo.PayWay = Request["PayWay"].ToString();
                //orderinfo.Guarantee = Request["Guarantee"];
                //orderinfo.Provider = Request["Provider"].ToString();
                //orderinfo.ProvidManager = Request["ProvidManager"].ToString();
                //orderinfo.Demand = Request["Demand"].ToString();
                //orderinfo.DemandManager = Request["DemandManager"].ToString();
                //orderinfo.Remark = Request["Remark"].ToString(); 
                #endregion
                ////订单详细表
                List<Orders_DetailInfo> list = new List<Orders_DetailInfo>();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] OrderContent = Request["OrderContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] Subtotal = Request["Subtotal"].Split(',');
                string[] Technology = Request["Technology"].Split(',');
                //  string[] DeliveryTime = Request["DeliveryTime"].Split(',');
                string[] UnitCost = Request["UnitCost"].Split(',');
                string[] TotalCost = Request["TotalCost"].Split(',');
                string[] SaleNo = Request["SaleNo"].Split(',');
                string[] ProjectNo = Request["ProjectNo"].Split(',');
                string[] JNAME = Request["JNAME"].Split(',');
                // string[] DID = Request["DID"].Split(',');
                for (int i = 0; i < OrderContent.Length; i++)
                {
                    if (OrderContent[i] != "")
                    {
                        Orders_DetailInfo ordersDetail = new Orders_DetailInfo();
                        // ordersDetail.PID = orderinfo.PID;
                        ordersDetail.ProductID = ProductID[i].ToString();
                        ordersDetail.OrderID = orderinfo.OrderID;
                        ordersDetail.DID = ordersDetail.OrderID + string.Format("{0:D3}", i + 1);
                        ordersDetail.State = "0";
                        ordersDetail.Validate = "v";
                        ordersDetail.OrderContent = OrderContent[i].ToString();
                        ordersDetail.SpecsModels = SpecsModels[i].ToString();
                        ordersDetail.Manufacturer = Supplier[i].ToString();
                        ordersDetail.OrderUnit = Unit[i].ToString();
                        ordersDetail.DeliveryTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(UnitPrice[i].ToString())) { ordersDetail.Price = Convert.ToDecimal(UnitPrice[i]); }

                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            ordersDetail.OrderNum = 0;
                        }
                        else
                        {
                            ordersDetail.OrderNum = Convert.ToInt32(Amount[i]);
                            ordersDetail.ActualAmount = Convert.ToInt32(Amount[i]);
                        }
                        if (!string.IsNullOrEmpty(Subtotal[i]))
                        {
                            ordersDetail.Subtotal = Convert.ToDecimal(Subtotal[i]);
                            ordersDetail.ActualSubTotal = Convert.ToDecimal(Subtotal[i]);
                        }

                        ordersDetail.Technology = Technology[i].ToString();
                        //if (DeliveryTime[i] != "")
                        //    ordersDetail.DeliveryTime = Convert.ToDateTime(DeliveryTime[i]);
                        if (UnitCost[i] != "")
                        {
                            ordersDetail.UnitCost = Convert.ToDecimal(UnitCost[i]);
                        }
                        if (TotalCost[i] != "")
                        { ordersDetail.TotalCost = Convert.ToDecimal(TotalCost[i]); }

                        ordersDetail.SaleNo = SaleNo[i].ToString();
                        ordersDetail.ProjectNo = ProjectNo[i].ToString();
                        ordersDetail.CreateTime = DateTime.Now.ToString();
                        ordersDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        list.Add(ordersDetail);
                    }
                }
                string strErr = "";
                bool b = SalesManage.SaveUpdateOrderInfo(orderinfo, list, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = orderinfo.OrderID;
                    salesLog.LogContent = "修改订单";
                    salesLog.ProductType = "订单修改";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "false", Msg = "验证不通过" });
            }
        }

        public ActionResult CanCelOrdersInfo()
        {
            string OrderID = Request["ID"].ToString();
            string strErr = "";
            bool b = SalesManage.CanCelOrdersInfo(OrderID, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult LoadOrderDetail()
        {
            string OrderID = Request["OrderID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.LoadOrderDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadOrderBill()
        {
            string OrderID = Request["OrderID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.LoadOrderBill(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadOrdersShipments()
        {
            return View();
        }
        public FileResult OrderInfoToExcel()
        {
            string ContractID = Request["ContractID"];
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string State = Request["State"];
            string HState = Request["HState"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(ContractID))
            {
                s += " and ContractID like '%" + ContractID + "%'";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " and OrderUnit like '%" + OrderUnit + "%'";
            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += " and UseUnit like '%" + UseUnit + "%'";
            }
            if (!string.IsNullOrEmpty(OrderContent))
            {
                s += " and OrderContent like '%" + OrderContent + "%'";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " and DeliveryTime between  '" + StartDate + "' and '" + EndDate + "'";
            }
            if (!string.IsNullOrEmpty(State))
            {
                if (State == "y")
                {
                    //  s += " (a.State =1 or a.State =2 or a.State =3 ) and";
                }
                else
                {
                    s += " and a.OState ='" + State + "'";
                }
            }
            if (!string.IsNullOrEmpty(HState))
            {
                s += " and  IsHK ='" + HState + "' ";
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
            {
                //  s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " " + s; }
            string strErr = "";
            DataTable data = SalesManage.GetOrderInfoToExcel(where, ref strErr);
            if (data != null)
            {
                string strCols = "编号-5000,订单编号-5000,合同编号-5000, 订单单位-5000,订货单位联系人-5000, 订货单位联系方式-5000,订货地址-5000, 使用单位-5000,创建人-5000,合同总额-5000, 回款总额-5000, 欠款金额-5000, 使用单位联系人-5000, 使用单位联系人方式-5000, 交货日期-5000,  状态-5000";
                //string strCols = "项目编号-5000,订单编号-5000,合同编号-5000,订货单位-5000,订货人-5000,使用单位-5000,使用人-5000,使用联系电话-5000,使用地址-5000,是否付款-3000," +
                //  " 状态-5000,保修期-5000,供方单位-5000,供方负责人-5000,需方单位-5000,需方负责人-5000,备注-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "订单信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "订单信息表.xls");
            }
            else
                return null;
        }

        public ActionResult PrintOrderInfo()
        {
            OrdersInfo OrdersInfo = SalesManage.GetOrdersByOrderID(Request["OrderID"].ToString());
            if (OrdersInfo.ContractID == "")
            {
                string Str = SalesManage.GetNamePY(GAccount.GetAccountInfo().UserName);
                string Dime = DateTime.Now.Year.ToString();// ("YYYY");
                Dime = Dime.Substring(2, 2);
                //string MaxContractID = SalesManage.GetMaxContractID();
                //MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);
                //MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
                OrdersInfo.ContractID = SalesManage.GetMaxContractID();
            }
            // return View(OrdersInfo);

            DataTable dt = SalesManage.GetOrdersDetail(OrdersInfo.OrderID);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>北京市燕山工业燃气设备有限公司</div><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>打印订货备忘录</div><div id='' style='margin-left:10px;font-size:16px;'>合同编号:" + OrdersInfo.ContractID + " &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;签订日期:" + OrdersInfo.ContractDate + "  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;YSGLJL-XS-F02</div><table class='tabInfo' cellpadding='0' ><tr><td style='border: 1px solid #000;'>订单单位:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderUnit + " </td><td style='border: 1px solid #000;'>联系人:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderContactor + "</td><td style='border: 1px solid #000;'>联系电话:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderTel + "</td><td style='border: 1px solid #000;'>地址:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderAddress + "</td></tr><tr><td>使用单位:</td><td>" + OrdersInfo.UseUnit + " </td><td style='border: 1px solid #000;'>联系人:</td><td style='border: 1px solid #000;'> " + OrdersInfo.UseContactor + " </td><td style='border: 1px solid #000;'>联系电话:</td><td style='border: 1px solid #000;'> " + OrdersInfo.UseTel + "</td><td style='border: 1px solid #000;'> 地址:</td><td style='border: 1px solid #000;'>" + OrdersInfo.UseAddress + "</td></tr><tr><td style='border: 1px solid #000;'>交货时间:</td><td style='border: 1px solid #000;' colspan='8'>" + OrdersInfo.DeliveryTime + " </td></tr></table>");
            //sb1.Append("<div><table id='myTable' cellpadding=' 0' cellspacing='0' class='tabInfo' ><tr style='background-color: #88c9e9;' align='center' valign='middle'><th style='width: 200px;' class='th'>    序号</th><th style='width: 200px;' class='th'>    物品编号</th><th style='width: 200px;' class='th'>    物品名称</th><th style='width: 200px;' class='th'>    规格型号</th><th style='width: 200px;' class='th'>    单位</th><th style='width: 200px;' class='th'>    数量</th><th style='width: 200px;' class='th'>供应商</th><th style='width:200px;' class='th'>    单价</th><th style='width: 200px;' class='th'>    小计</th><th style='width: 200px;' class='th'>    技术要求或参数</th><th style='width: 200px;' class='th'>    交货时间</th></tr><tbody id='DetailInfo'>");

            sb2.Append("<div><table id='Botom' class=' tabInfo'><tr> <td style='border: 1px solid #000;'>合计:人民币(大写)</td> <td style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.Total + "</td></tr><tr><td style='border: 1px solid #000;'>付款方式</td><td  style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.PayWay + "</td></tr><tr><td style='border: 1px solid #000;'>产品保修期</td><td style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.Guarantee + "</td></tr><tr><td style='border: 1px solid #000;' rowspan='2'>供方</td><td style='border: 1px solid #000;'>单位</td><td style='border: 1px solid #000;'>" + OrdersInfo.Provider + "</td><td  style='border: 1px solid #000;' rowspan='2'> 需方 </td><td  style='border: 1px solid #000;'>单位</td><td style='border: 1px solid #000;'>" + OrdersInfo.Demand + "</td></tr><tr><td style='border: 1px solid #000;'>负责人：</td><td style='border: 1px solid #000;'> " + OrdersInfo.ProvidManager + "</td><td style='border: 1px solid #000;'>负责人：</td><td>" + OrdersInfo.DemandManager + "</td></tr><tr><td style='border: 1px solid #000;'>备注：</td><td style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.Remark + "</td></tr><tr><td style='border: 1px solid #000;'>业务渠道：</td><td style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.ChannelsFrom + "</td></tr></table></div></div>");
            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<div><table id='myTable' cellpadding=' 0' cellspacing='0' class='tabInfo' ><tr style='background-color: #88c9e9;' align='center' valign='middle'><th style='width: 200px;' class='th'>    序号</th><th style='width: 200px;' class='th'>    物品编号</th><th style='width: 200px;' class='th'>    物品名称</th><th style='width: 200px;' class='th'>    规格型号</th><th style='width: 200px;' class='th'>    单位</th><th style='width: 200px;' class='th'>    数量</th><th style='width: 200px;' class='th'>供应商</th><th style='width:200px;' class='th'>    单价</th><th style='width: 200px;' class='th'>    小计</th><th style='width: 200px;' class='th'>    技术要求或参数</th></tr><tbody id='DetailInfo'>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr  id ='DetailInfo" + i + "'><td style='border: 1px solid #000;' ><lable class='labRowNumber" + i + " ' id='RowNumber" + i + "'>" + (i + 1) + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labProductID" + i + " ' id='ProductID" + i + "'>" + dt.Rows[i]["ProductID"] + " </lable> </td><td  style='border: 1px solid #000;'><lable class='labProName" + i + " ' id='ProName" + i + "'>" + dt.Rows[i]["OrderContent"] + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labSpec" + i + " ' id='Spec" + i + "'>" + dt.Rows[i]["SpecsModels"] + "</lable> </td><td  style='border: 1px solid #000;'><lable class='labUnits" + i + " ' id='Units" + i + "'>" + dt.Rows[i]["OrderUnit"] + "</lable> </td><td  style='border: 1px solid #000;'>" + dt.Rows[i]["OrderNum"] + "</td><td >" + dt.Rows[i]["Manufacturer"] + "</td><td  style='border: 1px solid #000;'>" + dt.Rows[i]["Price"] + " </td><td  style='border: 1px solid #000;'>" + dt.Rows[i]["Subtotal"] + "</td><td style='border: 1px solid #000;' >" + dt.Rows[i]["Technology"] + "</td></tr>");
                }
                sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > dt.Rows.Count)
                        length = 6 * i + dt.Rows.Count % 6;
                    sb1.Append("<div><table id='myTable' cellpadding=' 0' cellspacing='0' class='tabInfo' ><tr style='background-color: #88c9e9;' align='center' valign='middle'><th style='width: 200px;' class='th'>    序号</th><th style='width: 200px;' class='th'>    物品编号</th><th style='width: 200px;' class='th'>    物品名称</th><th style='width: 200px;' class='th'>    规格型号</th><th style='width: 200px;' class='th'>    单位</th><th style='width: 200px;' class='th'>    数量</th><th style='width: 200px;' class='th'>供应商</th><th style='width:200px;' class='th'>    单价</th><th style='width: 200px;' class='th'>    小计</th><th style='width: 200px;' class='th'>    技术要求或参数</th></tr><tbody id='DetailInfo'>");
                    for (int j = a; j < length; j++)
                    {
                        sb1.Append("<tr  id ='DetailInfo" + j + "' ><td style='border: 1px solid #000;' ><lable class='labRowNumber" + j + " ' id='RowNumber" + j + "'>" + (j + 1) + "</lable> </td><td style='border: 1px solid #000;'><lable class='labProductID" + j + " ' id='ProductID" + j + "'>" + dt.Rows[j]["ProductID"] + " </lable> </td><td ><lable class='labProName" + j + " ' id='ProName" + j + "'>" + dt.Rows[j]["OrderContent"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labSpec" + j + " ' id='Spec" + j + "'>" + dt.Rows[j]["SpecsModels"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labUnits" + j + " ' id='Units" + j + "'>" + dt.Rows[j]["OrderUnit"] + "</lable> </td><td style='border: 1px solid #000;'>" + dt.Rows[j]["OrderNum"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Manufacturer"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Price"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Subtotal"] + "</td><td >" + dt.Rows[j]["Technology"] + "</td></tr>");
                    }
                    if ((length - a) < 6)
                    {
                    }
                    sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }

            Response.Write(html);
            return View();
        }
        public ActionResult GetOrdersInfo()
        {

            string OrderID = Request["OrderID"].ToString();
            DataTable dt = SalesManage.GetOrdersInfo(OrderID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetOrderHTXXGrid()
        {
            string OrderID = Request["OrderID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = SalesManage.GetOrderHTXXGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderFJXXGrid()
        {
            string OrderID = Request["OrderID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = SalesManage.GetOrderFJXXGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadFile(string id)
        {
            ViewData["StrCID"] = id;
            return View();
        }

        //订单的相关单据
        public ActionResult OrdersInfoBill()
        {
            string OrderID = Request["ID"];
            OrdersInfo order = SalesManage.GetOrdersByOrderID(OrderID);
            return View(order);
        }

        #endregion

        #region [发货管理]

        public ActionResult ShipmentsManage()
        {
            return View();
        }
        public ActionResult LoadOrderShipment()
        {
            string ShipGoodsID = Request["ShipGoodsID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.LoadOrderShipment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ShipGoodsID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadOrderShipmentDetail()
        {
            string ShipGoodsID = Request["ShipGoodsID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.LoadOrderShipmentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, ShipGoodsID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadShipmentsGrid()
        {
            string strCurPage;
            string strRowNum;
            string where = "";
            string s = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string ContractID = Request["ContractID"];
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderID = Request["OrderID"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            if (!string.IsNullOrEmpty(ContractID))
            {
                s += " and ContractID like '%" + ContractID + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderContent))
            {
                s += " and OrderContent like '%" + OrderContent + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += " and OrderID like '%" + OrderID + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " and OrderUnit like'%" + OrderUnit + "%' ";

            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += " and OrderUnit like'%" + OrderUnit + "%' ";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " and ShipmentDate between  '" + StartDate + "' and '" + EndDate + "'";
            }
            //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            //{
            //    s = s.Substring(0, s.Length - 3);
            //}
            if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
            UIDataTable udtTask = SalesManage.LoadShipmentsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearchShipmentsGrid(tk_SalesGrid tk_SalesGrid)
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                string where = "";
                string s = "";
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "9";
                string ContractID = Request["ContractID"];
                //string OrderUnit = Request["OrderUnit"];
                //string UseUnit = Request["UseUnit"];
                //string OrderID = Request["OrderID"];
                string OrderContent = Request["OrderContent"];
                string StartDate = Request["StartDate"];
                string EndDate = Request["EndDate"];
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += " and ContractID like '%" + ContractID + "%' ";
                }
                if (!string.IsNullOrEmpty(OrderContent))
                {
                    s += " and b.OrderContent like '%" + OrderContent + "%' ";
                }
                //if (!string.IsNullOrEmpty(OrderID))
                //{
                //    s += " and b.OrderID like '%" + OrderID + "%' ";
                //}
                //if (!string.IsNullOrEmpty(OrderUnit))
                //{
                //    s += " and b.OrderUnit like'%" + OrderUnit + "%' ";

                //}
                //if (!string.IsNullOrEmpty(UseUnit))
                //{
                //    s += " and b.OrderUnit like'%" + OrderUnit + "%' ";
                //}
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and ShipmentDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
                {
                    //  s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                UIDataTable udtTask = SalesManage.LoadShipmentsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Sucess = "false", Msg = "验证不通过" });
            }

        }

        public ActionResult PrintShipments()
        {
            Shipments shipment = new Shipments();
            shipment.ShipGoodsID = Request["ShipGoodsID"].ToString();
            shipment.OrderID = Request["OrderID"].ToString();
            ViewData["ContractID"] = SalesManage.getOrderContractID(shipment.OrderID);
            return View(shipment);
        }

        public ActionResult GetShipmentDetail()
        {
            string ShipGoodsID = Request["ShipGoodsID"].ToString();
            DataTable dt = SalesManage.GetShipmentDetail(ShipGoodsID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult GetShipments()
        {
            string ShipGoodsID = Request["ShipGoodsID"].ToString();
            string Orderid = Request["OrderID"].ToString();
            DataTable dt = SalesManage.GetShipments(ShipGoodsID, Orderid);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateShipments()
        {
            string ShipGoodsID = Request.QueryString["ShipGoodsID"];
            Shipments shipments = SalesManage.getShipmentsBySID(ShipGoodsID);
            return View(shipments);
        }

        public ActionResult SaveUpdateShipment(Shipments shipments)
        {
            if (ModelState.IsValid)
            {
                #region Old1011
                //Shipments shipments = new Shipments();
                //shipments.OrderID = Request["OrderID"].ToString();
                //shipments.ShipGoodsID = Request["ShipGoodeID"].ToString();
                //shipments.ShipmentDate = DateTime.Now;
                //shipments.Remark = Request["Remark"].ToString();
                //shipments.Orderer = Request["Orderer"].ToString();
                //shipments.Shippers = Request["Shippers"].ToString(); 
                #endregion
                List<Shipments_DetailInfo> List = new List<Shipments_DetailInfo>();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] OrderContent = Request["OrderContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] DID = Request["Did"].Split(',');
                for (int i = 0; i < OrderContent.Length; i++)
                {
                    Shipments_DetailInfo shipdetail = new Shipments_DetailInfo();
                    shipdetail.ShipGoodsID = shipments.ShipGoodsID;
                    shipdetail.DID = DID[i].ToString();
                    shipdetail.ProductID = ProductID[i].ToString();
                    shipdetail.OrderContent = OrderContent[i].ToString();
                    shipdetail.Specifications = SpecsModels[i].ToString();
                    shipdetail.Supplier = Supplier[i].ToString();
                    shipdetail.Unit = Unit[i].ToString();
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        shipdetail.Amount = 0;
                    }
                    else { shipdetail.Amount = Convert.ToInt32(Amount[i]); }
                    if (string.IsNullOrEmpty(UnitPrice[i])) { shipdetail.Price = 0.00M; }

                    else
                    {
                        shipdetail.Price = Convert.ToDecimal(UnitPrice[i]);
                    }
                    shipdetail.CreateTime = DateTime.Now;
                    List.Add(shipdetail);
                }
                string strErr = "";
                bool b = SalesManage.SaveUpdateShipment(shipments, List, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = shipments.ShipGoodsID;
                    salesLog.LogContent = "修改发货单";
                    salesLog.ProductType = "发货单修改";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "", Msg = "验证不通过" });
            }
        }


        public ActionResult CanCelShipments()
        {
            string ShipGoodsID = Request["ShipGoodsID"].ToString();
            string strErr = "";
            bool b = SalesManage.CanCelShipments(ShipGoodsID, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = ShipGoodsID;
                salesLog.LogContent = "撤销发货单";
                salesLog.ProductType = "发货单撤销";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }

        public FileResult ShipmentsToExcel()
        {
            string ContractID = Request["ContractID"];
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderID = Request["OrderID"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string s = "";
            string where = "";
            if (!string.IsNullOrEmpty(ContractID))
            {
                s += " ContractID like '%" + ContractID + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderContent))
            {
                s += " OrderContent like '%" + OrderContent + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += " OrderID like '%" + OrderID + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " OrderUnit like'%" + OrderUnit + "%' and";

            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += " OrderUnit like'%" + OrderUnit + "%' and";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " ShipmentDate between  '" + StartDate + "' and '" + EndDate + "'";
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            {
                s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
            string strErr = "";
            DataTable dt = SalesManage.ShipmentsToExcel(where, ref strErr);//LoadShipmentsGrid(where,ref string strErr);
            if (dt != null)
            {
                string strCols = "订单号-5000,发货单号-5000,发货日期-5000,发货人-5000,订货人-5000,备注-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "发货信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "发货信息表.xls");
            }
            else
                return null;
        }

        public ActionResult ShipmentBill()
        {
            string ShipmentID = Request["ID"];
            Shipments shipment = SalesManage.getShipmentsBySID(ShipmentID);
            return View(shipment);
        }
        #endregion

        #region [回款管理]
        public ActionResult AddReceivePayment()
        {
            ReceivePayment rPayment = new ReceivePayment();
            string unitID = GAccount.GetAccountInfo().UnitID;
            rPayment.OrderID = Request.QueryString["OrderID"].ToString();
            ViewData["ContractID"] = SalesManage.GetContractID(rPayment.OrderID);
            rPayment.RID = SalesManage.GetHKNO(unitID);
            DataTable dt = SalesManage.getOrdersInfoTotal(rPayment.OrderID);
            ViewData["Total"] = dt.Rows[0]["Total"].ToString();
            ViewData["DebtAmount"] = dt.Rows[0]["DebtAmount"].ToString();
            ViewData["HKAmount"] = dt.Rows[0]["HKAmount"].ToString();
            return View(rPayment);
        }

        public ActionResult GetReceivePaymentTotal()
        {
            string PID = Request["OrderID"].ToString();
            DataTable dt = SalesManage.getOrdersInfoTotal(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }




        public ActionResult SaveReceivePayment(ReceivePayment rpayment)
        {
            if (ModelState.IsValid)
            {
                #region MyRegion
                //ReceivePayment rpayment = new ReceivePayment();
                rpayment.RID = Request["RID"].ToString();
                rpayment.OrderID = Request["OrderID"].ToString();
                rpayment.Mothods = Request["Methods"].ToString();
                if (!string.IsNullOrEmpty(Request["Amount"].ToString()))
                {
                    rpayment.Amount = Convert.ToDecimal(Request["Amount"]);
                }
                rpayment.PaymentUnit = Request["PaymentUnit"].ToString();
                rpayment.PayTime = DateTime.Now.ToString();
                rpayment.Remark = Request["Remark"].ToString();
                rpayment.ChequeID = Request["ChequeID"].ToString();
                #endregion
                // rpayment.PayTime = DateTime.Now.ToString();
                rpayment.Validate = "v";
                rpayment.CreateTime = DateTime.Now.ToString();
                rpayment.CreateUser = GAccount.GetAccountInfo().UserName;

                string strErr = "";
                bool b = SalesManage.SaveReceivePayment(rpayment, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = rpayment.OrderID;
                    salesLog.LogContent = "新增回款";
                    salesLog.ProductType = "新增回款";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "fales", Msg = "验证不通过" });
            }
        }

        public ActionResult ReceivePaymentManage()
        {
            return View();
        }
        public ActionResult LoadReceivePayment()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string s = "";
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderID = Request["OrderID"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];

            if (!string.IsNullOrEmpty(OrderContent))
            {
                s += "and b.OrderContent like '%" + OrderContent + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += "and b.OrderID like '%" + OrderID + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += "and c.OrderUnit like'%" + OrderUnit + "%' ";

            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += "and c.UseUnit like'%" + UseUnit + "%' ";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += "and a.PayTime between  '" + StartDate + "' and '" + EndDate + "'";
            }
            //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            //{
            //    s = s.Substring(0, s.Length - 3);
            //}
            //if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
            UIDataTable udtTask = SalesManage.LoadReceivePayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetSearchRecevie(tk_SalesGrid tk_SalesGrid)
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
                string s = "";
                string OrderUnit = tk_SalesGrid.OrderUnit;
                string UseUnit = tk_SalesGrid.UseUnit;
                string OrderID = tk_SalesGrid.OrderID;
                string OrderContent = tk_SalesGrid.MainContent;
                string StartDate = tk_SalesGrid.StartDate;
                string EndDate = tk_SalesGrid.EndDate;

                if (!string.IsNullOrEmpty(OrderContent))
                {
                    s += " b.OrderContent like '%" + OrderContent + "%' and";
                }
                if (!string.IsNullOrEmpty(OrderID))
                {
                    s += " b.OrderID like '%" + OrderID + "%' and";
                }
                if (!string.IsNullOrEmpty(OrderUnit))
                {
                    s += " c.OrderUnit like'%" + OrderUnit + "%' and";

                }
                if (!string.IsNullOrEmpty(UseUnit))
                {
                    s += " c.UseUnit like'%" + UseUnit + "%' and";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " a.PayTime between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
                {
                    s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
                UIDataTable udtTask = SalesManage.LoadReceivePayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult LoadReceiveBill()
        {
            string OrderID = Request["OID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.LoadReceiveBill(OrderID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult LoadReceivePaymentBill()
        {
            string OrderID = Request["OID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.LoadReceivePaymentBill(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public FileResult ReceivePaymentToExcel()
        {
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderID = Request["OrderID"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string s = "";
            string where = "";
            if (!string.IsNullOrEmpty(OrderContent))
            {
                s += " b.OrderContent like '%" + OrderContent + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += " b.OrderID like '%" + OrderID + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " c.OrderUnit like'%" + OrderUnit + "%' and";

            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += " c.UseUnit like'%" + UseUnit + "%' and";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " a.PayTime between  '" + StartDate + "' and '" + EndDate + "'";
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            {
                s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
            string strErr = "";
            DataTable dt = SalesManage.ReceivePaymentToExcel(where, ref strErr);

            if (dt != null)
            {
                string strCols = "回款单号-5000,关联订单-5000,关联订单内容-5000,回款金额-5000,回款方式-5000,回款日期-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "回款信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "回款信息表.xls");
            }
            else
                return null;
        }

        //相关单据的详细页
        public ActionResult LoadReceivePaymentXX()
        {
            return View();
        }

        public ActionResult UpdateReceivePayment()
        {
            ReceivePayment receivepayment = SalesManage.getReceivePayment(Request.QueryString["RID"].ToString());
            return View(receivepayment);
        }

        public ActionResult SaveUpdateReceivePayment(ReceivePayment receivepayment)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                bool b = SalesManage.SaveUpdateReceivePayment(receivepayment, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = receivepayment.RID;
                    salesLog.LogContent = "修改回款单";
                    salesLog.ProductType = "回款单修改";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "false", Msg = "验证数据不通过" });
            }
        }

        public ActionResult CancelReceivePayment()
        {
            string RID = Request["ID"].ToString();
            string strErr = "";
            bool b = SalesManage.CancelReceivePayment(RID, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult PrintReceivePayment()
        {
            // ReceivePayment receivepayment = SalesManage.getReceivePayment(Request ["RID"].ToString ());
            OrdersInfo OrdersInfo = SalesManage.GetOrdersByOrderID(Request["OrderID"].ToString());

            DataTable dt = SalesManage.GetReceivePaymentByOrderID(OrdersInfo.OrderID);


            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("<div id='PrintArea' style='page-break-after: always;'><div><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>订单回款记录单</div><div id='' style='margin-left:10px;font-size:16px;'>合同编号:" + OrdersInfo.ContractID + " &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;签订日期:" + OrdersInfo.ContractDate + ";  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;YZJL-XS-D02</div><table class='tabInfo' cellpadding='0' ><tr><td style='border: 1px solid #000;'>订单单位:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderUnit + " </td><td style='border: 1px solid #000;'>联系人:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderContactor + "</td><td style='border: 1px solid #000;'>联系电话:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderTel + "</td><td style='border: 1px solid #000;'>地址:</td><td style='border: 1px solid #000;'>" + OrdersInfo.OrderAddress + "</td></tr><tr><td style='border: 1px solid #000;'>使用单位:</td><td style='border: 1px solid #000;'>" + OrdersInfo.UseUnit + " </td><td style='border: 1px solid #000;'>联系人:</td><td style='border: 1px solid #000;'> " + OrdersInfo.UseContactor + " </td><td style='border: 1px solid #000;'>联系电话:</td><td style='border: 1px solid #000;'> " + OrdersInfo.UseTel + "</td><td style='border: 1px solid #000;'> 地址:</td><td style='border: 1px solid #000;'>" + OrdersInfo.UseAddress + "</td></tr></table></div>");

            sb2.Append("<div><table id='Botom' class=' tabInfo'><tr> <td style='border: 1px solid #000;'>合计:人民币</td> <td colspan='6' style='border: 1px solid #000;'>" + OrdersInfo.Total + "</td></tr><tr><td style='border: 1px solid #000;'>付款方式</td><td colspan='6' style='border: 1px solid #000;'>" + OrdersInfo.PayWay + "</td></tr><tr><td style='border: 1px solid #000;'>产品保修期</td><td colspan='6' style='border: 1px solid #000;'>" + OrdersInfo.Guarantee + "</td></tr><tr><td rowspan='2'>供方</td><td style='border: 1px solid #000;'>单位</td><td style='border: 1px solid #000;'>" + OrdersInfo.Provider + "</td><td  style='border: 1px solid #000;' rowspan='2'> 需方 </td><td style='border: 1px solid #000;'>单位</td><td style='border: 1px solid #000;'>" + OrdersInfo.Demand + "</td></tr><tr><td style='border: 1px solid #000;'>负责人：</td><td style='border: 1px solid #000;'> " + OrdersInfo.ProvidManager + "</td><td style='border: 1px solid #000;'>负责人：</td><td style='border: 1px solid #000;'>" + OrdersInfo.DemandManager + "</td></tr><tr><td style='border: 1px solid #000;'>备注：</td><td style='border: 1px solid #000;' colspan='6'>" + OrdersInfo.Remark + "</td></tr></table></div></div>");
            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<div><table id='Botom' class=' tabInfo'><tr> <td style='border: 1px solid #000;'>回款金额</td><td style='border: 1px solid #000;'>回款方式</td><td style='border: 1px solid #000;'>缴费单位</td><td>回款时间</td><td>是否开发票</td><td style='border: 1px solid #000;'>发票号</td><td style='border: 1px solid #000;'>支票号</td><td style='border: 1px solid #000;'>备注</td><td style='border: 1px solid #000;'>负责人</td></tr><tbody>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr> <td style='border: 1px solid #000;'>" + dt.Rows[i]["Amount"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Mothods"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["PaymentUnit"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["PayTime"] + "</td><td>" + dt.Rows[i]["IsInvoice"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["InvoiceNum"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["ChequeID"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Remark"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Manager"] + "</td></tr>");
                }
                sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > dt.Rows.Count)

                        for (int j = a; j < length; j++)
                        {
                            sb1.Append("<tr> <td style='border: 1px solid #000;'>" + dt.Rows[j]["Amount"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Methods"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Methods"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["PayTime"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["IsInvoice"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["InvoiceNum"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["ChequeID"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Remark"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Manager"] + "</td></tr>");
                        }
                    sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);

            return View();

        }

        //回款时时提醒
        public ActionResult SHowReceivePayment() 
        {
            return View();
        }
        public ActionResult GetShowReceivePayment() 
        {
            //DataTable dt = SalesManage.GetShowReceivePayment();
            //string strJson = GFun.Dt2Json("", dt);
            //strJson = strJson.Substring(1);
            //strJson = strJson.Substring(0, strJson.Length - 1);

            //return Json(strJson, JsonRequestBehavior.AllowGet);
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string s = "";
            string ContractID = Request["ContractID"];
            //string UseUnit = Request["UseUnit"];
            string OrderID = Request["OrderID"];
            //string OrderContent = Request["OrderContent"];
            //string StartDate = Request["StartDate"];
            //string EndDate = Request["EndDate"];

            //if (!string.IsNullOrEmpty(OrderContent))
            //{
            //    s += " b.OrderContent like '%" + OrderContent + "%' and";
            //}
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += "and a.OrderID like '%" + OrderID + "%' ";
            }
            //if (!string.IsNullOrEmpty(OrderUnit))
            //{
            //    s += " c.OrderUnit like'%" + OrderUnit + "%' and";

            //}
            if (!string.IsNullOrEmpty(ContractID))
            {
                s += "and a.ContractID like'%" + ContractID + "%' ";
            }
            //if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            //{
            //    s += " a.PayTime between  '" + StartDate + "' and '" + EndDate + "'";
            //}
            //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            //{
            //    s = s.Substring(0, s.Length - 3);
            //}
            if (!string.IsNullOrEmpty(s)) { where = " " + s; }
            UIDataTable udtTask = SalesManage.GetShowReceivePayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetTopShowReceivePayment() 
        {
            DataTable dt = SalesManage.GetTopShowReceivePayment();
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region [退货管理]

        public ActionResult ExchangeGoodsManage()
        {
            return View();
        }
        public ActionResult LoadExchangeGoodsGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string OrderID = Request["OrderID"];

            string OrderUnit = Request["OrderUnit"];
            string Brokerage = Request["Brokerage"];
            // string ChangeDate = Request["ChangeDate"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string State = Request["State"];
            string s = "";

            if (!string.IsNullOrEmpty(OrderID))
            {
                s += " and b.OrderID like '%" + OrderID + "%' ";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " and b.OrderUnit like'%" + OrderUnit + "%' ";

            }
            if (!string.IsNullOrEmpty(Brokerage))
            {
                s += " and a.Brokerage like'%" + Brokerage + "%' ";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " and a.ChangeDate between  '" + StartDate + "' and '" + EndDate + "'";
            }
            if (!string.IsNullOrEmpty(State))
            {
                if (State == "0")
                {
                    //  s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) ";
                }
                else
                {
                    s += " and  a.State =" + State + " ";
                }
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(State))
            {
                //  s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
            UIDataTable udtTask = SalesManage.LoadExchangeGoodsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearchExchangeGoodsGrid(tk_SalesGrid tk_SalesGrid)
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
                string OrderID = tk_SalesGrid.OrderID;

                string ContractID = tk_SalesGrid.ContractID;
                //  string Brokerage = tk_SalesGrid.Manager;
                // string ChangeDate = Request["ChangeDate"];
                string StartDate = tk_SalesGrid.StartDate;
                string EndDate = tk_SalesGrid.EndDate;
                string State = Request["State"];
                string s = "";

                if (!string.IsNullOrEmpty(OrderID))
                {
                    s += " and a.OrderID like '%" + OrderID + "%' ";
                }
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += " and a.ContractID like'%" + ContractID + "%' ";

                }
                //if (!string.IsNullOrEmpty(Brokerage))
                //{
                //    s += " and a.Brokerage like'%" + Brokerage + "%' ";
                //}
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and a.ChangeDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "-1")
                    {
                        // s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) ";
                    }
                    else
                    {
                        s += " and a.State =" + State + " ";
                    }
                }
                //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(State))
                //{
                //    s = s.Substring(0, s.Length - 3);
                //}
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                UIDataTable udtTask = SalesManage.LoadExchangeGoodsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Sucess = "", Msg = "验证不通过" });
            }
        }
        public ActionResult AddReturnGoods()
        {
            ExchangeGoods exgoods = new ExchangeGoods();
            exgoods.ISEXR = "1";//换货
            exgoods.EID = SalesManage.getNewGoodsID();
            exgoods.OrderID = Request.QueryString["OrderID"].ToString();
            exgoods.Brokerage = GAccount.GetAccountInfo().UserName;
            return View(exgoods);
        }


        public ActionResult AddExchangeGoods()
        {
            ExchangeGoods exgoods = new ExchangeGoods();
            exgoods.ISF = "0";
            exgoods.ISEXR = "2";//换货
            exgoods.EID = SalesManage.getNewGoodsID();
            exgoods.OrderID = Request.QueryString["OrderID"].ToString();
            exgoods.Brokerage = GAccount.GetAccountInfo().UserName;
            return View(exgoods);

        }

        public ActionResult AddExchangeGoodsF()
        {
            ExchangeGoods exgoods = new ExchangeGoods();
            exgoods.ISF = "1";
            exgoods.ISEXR = "2";//换货
            exgoods.EID = SalesManage.getNewGoodsID();
            exgoods.OrderID = Request.QueryString["OrderID"].ToString();
            exgoods.Brokerage = GAccount.GetAccountInfo().UserName;
            return View(exgoods);
        }
        public ActionResult GetShipmentsDetail()
        {
            string OrderID = Request["orderID"].ToString();
            DataTable dt = SalesManage.GetShipmentsDetail(OrderID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetShipmentOrdersDetail()
        {
            string OrderID = Request["OrderID"].ToString();

            DataTable dt = SalesManage.GetShipmentOrdersDetail(OrderID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        public ActionResult SaveExcGoods(ExchangeGoods excgoods)
        {
            if (ModelState.IsValid)
            {
                #region MyRegion
                //ExchangeGoods excgoods = new ExchangeGoods();
                //excgoods.EID = Request["EID"].ToString();
                //excgoods.OrderID = Request["OID"].ToString();
                //excgoods.ReturnType = Request["EXCType"].ToString();
                //excgoods.ReturnWay = Request["EXCWay"].ToString();
                //excgoods.ReturnReason = Request["EXCWhy"].ToString();
                //excgoods.ReturnContract = Request["EXCYd"].ToString(); 
                #endregion

                excgoods.Validate = "v";
                excgoods.CreateTime = DateTime.Now;
                excgoods.CreateUser = GAccount.GetAccountInfo().UserName;
                excgoods.State = "0";
                excgoods.ChangeDate = DateTime.Now.ToString();
                List<ExReturn_Detail> Rlist = new List<ExReturn_Detail>();
                string[] DID = Request["DID"].Split(',');
                //string[] RDID = Request["RDID"].Split(',');
                string[] RProductID = Request["RProductID"].Split(',');
                string[] ROrderContent = Request["ROrderContent"].Split(',');
                string[] RSpecsModels = Request["RSpecsModels"].Split(',');
                string[] RSupplier = Request["RSupplier"].Split(',');
                string[] RUnit = Request["RUnit"].Split(',');
                string[] RAmount = Request["RAmount"].Split(',');
                string[] RUnitPrice = Request["RUnitPrice"].Split(',');
                string[] RSubtotal = Request["RSubtotal"].Split(',');
                string[] RUnitCost = Request["RunitCost"].Split(',');
                string[] RTotalCost = Request["RtotalCost"].Split(',');
                string[] RTechnology = Request["Rtechnology"].Split(',');
                string[] RSaleNo = Request["Rsaleno"].Split(',');
                string[] RProjectNo = Request["Rprojectno"].Split(',');
                string[] RJNAME = Request["Rjname"].Split(',');
                for (int i = 0; i < ROrderContent.Length-1; i++)
                {

                    if (ROrderContent[i] != "")
                    {
                        ExReturn_Detail Rdetail = new ExReturn_Detail();
                        Rdetail.EID = excgoods.EID;
                        Rdetail.EDID=DID[i].ToString ();
                        Rdetail.OrderContent = ROrderContent[i].ToString();
                        Rdetail.ProductID = RProductID[i].ToString();
                        Rdetail.Specifications = RSpecsModels[i].ToString();
                        Rdetail.DID = excgoods.EID + string.Format("{0:D3}", i + 1);
                        Rdetail.Supplier = RSupplier[i].ToString();
                        Rdetail.Unit = RUnit[i].ToString();
                        if(RUnitPrice[i]!=""){ Rdetail.Price=Convert.ToDecimal(RUnitPrice[i]);}
                       Rdetail .SaleNo=RSaleNo[i].ToString();
                        if(RSubtotal[i]!=""){ Rdetail .Subtotal=Convert .ToDecimal (RSubtotal[i]);}
                       if(RUnitCost[i]!=""){
                        Rdetail .UnitCost=Convert.ToDecimal( RUnitCost[i]);}
                       if (!string.IsNullOrEmpty(RTotalCost[i]))
                       {
                           Rdetail.TotalCost = Convert.ToDecimal(RTotalCost[i]);
                       }
                     
                        Rdetail .Technology=RTechnology[i].ToString ();
                        Rdetail .ProjectNo=RProjectNo [i].ToString ();
                        Rdetail.JNAME =RJNAME [i].ToString ();
                        if (!string.IsNullOrEmpty(RAmount[i]))
                        {
                            Rdetail.Amount = Convert.ToInt32(RAmount[i]);
                        }
                        if (!string.IsNullOrEmpty(RUnitPrice[i]))
                        {
                            Rdetail.ExUnit = Convert.ToDecimal(RUnitPrice[i]);
                            //ExTotal
                        }
                        if (!string.IsNullOrEmpty(RSubtotal[i]))
                        {
                            Rdetail.ExTotal = Convert.ToDecimal(RSubtotal[i]);
                        }
                        Rdetail.CreateTime = DateTime.Now;
                        Rdetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        Rdetail.Validate = "0";
                        Rlist.Add(Rdetail);
                        #region MyRegion
                        //添加到订单详细
                        
                        #endregion
                    }
                }
                string strErr = "";
                bool b = SalesManage.SaveExchangeGoods(excgoods, Rlist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = excgoods.EID;
                    salesLog.LogContent = "新增换货单";
                    salesLog.ProductType = "换货单添加";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "fales", Msg = "验证不通过" });
            }
        }

        public ActionResult AddReturnDetail()
        {
            ExReturn_Detail rdetail = new ExReturn_Detail();
            rdetail.EID = Request.QueryString["Eid"].ToString();
            return View(rdetail);
        }


        //退货单和详细
        public ActionResult SaveExcGoodsAndDetail(ExchangeGoods excgoods)
        {
            if (ModelState.IsValid)
            {
                excgoods.Validate = "v";
                excgoods.CreateTime = DateTime.Now;
                excgoods.CreateUser = GAccount.GetAccountInfo().UserName;
                excgoods.State = "0";
                excgoods.ChangeDate = DateTime.Now.ToString();
                List<Exchange_Detail> EList = new List<Exchange_Detail>();
                string[] RDID = Request["RDID"].Split(',');
                string[] RProductID = Request["RProductID"].Split(',');
                string[] ROrderContent = Request["ROrderContent"].Split(',');
                string[] RSpecsModels = Request["RSpecsModels"].Split(',');
                string[] RSupplier = Request["RSupplier"].Split(',');
                string[] RUnit = Request["RUnit"].Split(',');
                string[] RAmount = Request["RAmount"].Split(',');
                string[] RUnitPrice = Request["RUnitPrice"].Split(',');
                string[] RSubtotal = Request["RSubtotal"].Split(',');
                for (int i = 0; i < ROrderContent.Length-1; i++)
                {

                    if (ROrderContent[i] != "")
                    {
                        Exchange_Detail Rdetail = new Exchange_Detail();
                        Rdetail.EID = excgoods.EID;
                        Rdetail.EDID = RDID[i].ToString();//关联订单详细编码
                        Rdetail.OrderContent = ROrderContent[i].ToString();
                        Rdetail.ProductID = RProductID[i].ToString();
                        Rdetail.Specifications = RSpecsModels[i].ToString();
                        Rdetail.DID = excgoods.EID + string.Format("{0:D3}", i + 1);
                        Rdetail.Supplier = RSupplier[i].ToString();
                        Rdetail.Unit = RUnit[i].ToString();
                        if (!string.IsNullOrEmpty(RAmount[i]))
                        {
                            Rdetail.Amount = Convert.ToInt32(RAmount[i]);
                        }
                        if (!string.IsNullOrEmpty(RUnitPrice[i]))
                        {
                            Rdetail.ExUnit = Convert.ToDecimal(RUnitPrice[i]);
                            //ExTotal
                        }
                        if (!string.IsNullOrEmpty(RSubtotal[i]))
                        {
                            Rdetail.ExTotal = Convert.ToDecimal(RSubtotal[i]);
                        }
                        Rdetail.CreateTime = DateTime.Now;
                        Rdetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        Rdetail.Validate = "0";
                        EList.Add(Rdetail);
                    }
                }
                string strErr = "";
                bool b = SalesManage.SaveExcGoodsAndDetail(excgoods, EList, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = excgoods.EID;
                    salesLog.LogContent = "新增退货单";
                    salesLog.ProductType = "退货单添加";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "fales", Msg = "验证不通过" });

            }
        }
        public ActionResult LoadExchangeDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string where = "";
            string EID = Request["Eid"];
            if (!string.IsNullOrEmpty(EID))
            {
                where += " where EID='" + EID + "'";
            }
            UIDataTable udtTask = SalesManage.LoadExchangeDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadReturnDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string where = "";
            string EID = Request["Eid"];
            if (!string.IsNullOrEmpty(EID))
            {
                where += " where EID='" + EID + "'";
            }
            UIDataTable udtTask = SalesManage.LoadReturnDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadExchangeBill()
        {
            string OrderID = Request["OID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.LoadExchangeBill(OrderID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //修改常规换货
        public ActionResult UpdateExchangeGoods()
        {
            // ExchangeGoods exgoods = new ExchangeGoods();
            // exgoods.EID =Request .QueryString ["EID"].ToString ();
            //exgoods.OrderID = Request.QueryString["OrderID"].ToString();

            string EID = Request.QueryString["EID"].ToString();
            ExchangeGoods exgoods = SalesManage.GetExchangeGoodsBYEID(EID);
            return View(exgoods);
        }


        //修改换货非常规
        public ActionResult UpdateExchangeGoodsF()
        {
            // ExchangeGoods exgoods = new ExchangeGoods();
            // exgoods.EID =Request .QueryString ["EID"].ToString ();
            //exgoods.OrderID = Request.QueryString["OrderID"].ToString();

            string EID = Request.QueryString["EID"].ToString();
            ExchangeGoods exgoods = SalesManage.GetExchangeGoodsBYEID(EID);
            return View(exgoods);
        }


        //获取退货的数据
        public ActionResult GetReturnGoodsDetail()
        {
             string EID = Request["EID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.GetReturnDetailByEID(EID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //修改退货
        public ActionResult UpdateReturnGoods()
        {
            string EID = Request.QueryString["EID"].ToString();
            ExchangeGoods exgoods = SalesManage.GetExchangeGoodsBYEID(EID);
            return View(exgoods);
        }



        public ActionResult SHowEXOrderXX()
        {
            string EID = Request.QueryString["id"].ToString();
            ExchangeGoods exgoods = SalesManage.GetExchangeGoodsBYOrderID(EID);
            return View(exgoods);
        }

        public ActionResult SaveUpdateExcGoods(ExchangeGoods excgoods)
        {
            if (ModelState.IsValid)
            {
                //List<Exchange_Detail> List = new List<Exchange_Detail>();
                //List<Orders_DetailInfo> OrderList = new List<Orders_DetailInfo>();
                List<ExReturn_Detail> Rlist = new List<ExReturn_Detail>();

                //string[] DID = Request["DID"].Split(',');
                //string[] ProductID = Request["ProductID"].Split(',');
                //string[] OrderContent = Request["OrderContent"].Split(',');
                //string[] SpecsModels = Request["SpecsModels"].Split(',');
                //string[] Supplier = Request["Supplier"].Split(',');
                //string[] Unit = Request["Unit"].Split(',');
                //string[] Amount = Request["Amount"].Split(',');
                //string[] UnitPrice = Request["UnitPrice"].Split(',');
                //string[] Subtotal = Request["Subtotal"].Split(',');
                //for (int i = 0; i < OrderContent.Length - 1; i++)
                //{
                //    //退货修改原订单数据
                //    Orders_DetailInfo ordetail = new Orders_DetailInfo();
                //    ordetail.DID = DID[i].ToString();
                //    if (!string.IsNullOrEmpty(UnitPrice[i]))
                //    {
                //        ordetail.Price = Convert.ToDecimal(UnitPrice[i]);
                //    }
                //    if (!string.IsNullOrEmpty(Amount[i]))
                //    {
                //        ordetail.OrderNum = Convert.ToInt32(Amount[i]);
                //    }
                //    if (!string.IsNullOrEmpty(Subtotal[i]))
                //    {
                //        ordetail.Subtotal = Convert.ToDecimal(Subtotal[i]);
                //    }
                //    Exchange_Detail Excdetail = new Exchange_Detail();
                //    Excdetail.DID = excgoods.EID + string.Format("{0:D3}", i + 1);
                //    Excdetail.EID = excgoods.EID;
                //    Excdetail.ProductID = ProductID[i].ToString();
                //    Excdetail.OrderContent = OrderContent[i].ToString();
                //    Excdetail.Specifications = SpecsModels[i].ToString();
                //    Excdetail.Unit = Unit[i].ToString();
                //    Excdetail.Amount = Convert.ToInt32(Amount[i]);
                //    if (!string.IsNullOrEmpty(UnitPrice[i]))
                //    {
                //        Excdetail.ExUnit = Convert.ToDecimal(UnitPrice[i]);
                //    }
                //    if (!string.IsNullOrEmpty(Subtotal[i]))
                //    {
                //        Excdetail.ExTotal = Convert.ToDecimal(Subtotal[i]);
                //    }
                //    Excdetail.CreateTime = DateTime.Now;
                //    Excdetail.CreateUser = GAccount.GetAccountInfo().UserName;
                //    Excdetail.Validate = "0";
                //    List.Add(Excdetail);
                //    OrderList.Add(ordetail);
                //}
                string[] RDID = Request["DID"].Split(',');
                //string[] RDID = Request["RDID"].Split(',');
                string[] RProductID = Request["RProductID"].Split(',');
                string[] ROrderContent = Request["ROrderContent"].Split(',');
                string[] RSpecsModels = Request["RSpecsModels"].Split(',');
                string[] RSupplier = Request["RSupplier"].Split(',');
                string[] RUnit = Request["RUnit"].Split(',');
                string[] RAmount = Request["RAmount"].Split(',');
                string[] RUnitPrice = Request["RUnitPrice"].Split(',');
                string[] RSubtotal = Request["RSubtotal"].Split(',');
                string[] Runitcost = Request["Runitcost"].Split(',');
                string[] Rtotalcost = Request["Rtotalcost"].Split(',');
                string[] Rprojectno = Request["Rprojectno"].Split(',');
                string[] Rsaleno = Request["Rsaleno"].Split(',');
                 string[] Rjname = Request["Rjname"].Split(',');
                 string[] EID = Request["EID"].Split(',');
                 string[] EDID = Request["EDID"].Split(',');
                
                 string[] Rtechnology = Request["Rtechnology"].Split(',');
                 for (int i = 0; i < ROrderContent.Length - 1; i++)
                {
                    ExReturn_Detail Rdetail = new ExReturn_Detail();
                    Rdetail.EID = excgoods.EID;
                    Rdetail.OrderContent = ROrderContent[i].ToString();
                    Rdetail.ProductID = RProductID[i].ToString();
                    Rdetail.Specifications = RSpecsModels[i].ToString();
                    Rdetail.DID = excgoods.EID + string.Format("{0:D3}", i + 1);
                    Rdetail.EDID = EDID[i].ToString();
                    Rdetail.Supplier = RSupplier[i].ToString();
                    Rdetail.Unit = RUnit[i].ToString();
                    if (!string.IsNullOrEmpty(RAmount[i]))
                    {
                        Rdetail.Amount = Convert.ToInt32(RAmount[i]);
                    }
                    if (!string.IsNullOrEmpty(RUnitPrice[i]))
                    {
                        Rdetail.ExUnit = Convert.ToDecimal(RUnitPrice[i]);
                        Rdetail.Price = Convert.ToDecimal(RUnitPrice [i]);
                        //ExTotal
                    }
                    if (!string.IsNullOrEmpty(RSubtotal[i]))
                    {
                        Rdetail.ExTotal = Convert.ToDecimal(RSubtotal[i]);
                        Rdetail.Subtotal = Convert.ToDecimal(RSubtotal[i]);
                    }
                    if (!string.IsNullOrEmpty(Runitcost[i]))
                    {
                        Rdetail.UnitCost = Convert.ToDecimal(Runitcost[i]);
                    }
                    if (!string.IsNullOrEmpty(Rtotalcost[i]))
                    {
                        Rdetail.TotalCost = Convert.ToDecimal(Rtotalcost[i]);
                    }

                    Rdetail.ProjectNo= Rprojectno[i].ToString();
                    Rdetail.SaleNo = Rsaleno[i].ToString();
                    Rdetail.Technology = Rtechnology[i].ToString();
                    Rdetail.JNAME = Rjname[i].ToString();
                    Rdetail.CreateTime = DateTime.Now;
                    Rdetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    Rdetail.Validate = "0";
                    Rlist.Add(Rdetail);
                }

                string strErr = "";
                bool b = SalesManage.SaveUpdateExcGoods(excgoods, Rlist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = excgoods.EID;
                    salesLog.LogContent = "修改换货单";
                    salesLog.ProductType = "换货单修改";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "", Msg = "验证不通过" });
            }
        }

        public ActionResult SaveUpdateReturnGoods(ExchangeGoods excgoods)
        {
            if (ModelState.IsValid)
            {
          
                List<Exchange_Detail> Rlist = new List<Exchange_Detail>();
                string[] RDID = Request["DID"].Split(',');
                //string[] RDID = Request["RDID"].Split(',');
                string[] RProductID = Request["RProductID"].Split(',');
                string[] ROrderContent = Request["ROrderContent"].Split(',');
                string[] RSpecsModels = Request["RSpecsModels"].Split(',');
                string[] RSupplier = Request["RSupplier"].Split(',');
                string[] RUnit = Request["RUnit"].Split(',');
                string[] RAmount = Request["RAmount"].Split(',');
                string[] RUnitPrice = Request["RUnitPrice"].Split(',');
                string[] RSubtotal = Request["RSubtotal"].Split(',');
                //string[] Runitcost = Request["Runitcost"].Split(',');
                //string[] Rtotalcost = Request["Rtotalcost"].Split(',');
                //string[] Rprojectno = Request["Rprojectno"].Split(',');
                //string[] Rsaleno = Request["Rsaleno"].Split(',');
                //string[] Rjname = Request["Rjname"].Split(',');


                string[] EID = Request["EID"].Split(',');
                string[] EDID = Request["EDID"].Split(',');

               // string[] Rtechnology = Request["Rtechnology"].Split(',');
                for (int i = 0; i < ROrderContent.Length - 1; i++)
                {
                    Exchange_Detail Rdetail = new Exchange_Detail();
                    Rdetail.EID = excgoods.EID;
                    Rdetail.OrderContent = ROrderContent[i].ToString();
                    Rdetail.ProductID = RProductID[i].ToString();
                    Rdetail.Specifications = RSpecsModels[i].ToString();
                    Rdetail.DID = excgoods.EID + string.Format("{0:D3}", i + 1);
                    Rdetail.EDID = EDID[i].ToString();
                    Rdetail.Supplier = RSupplier[i].ToString();
                    Rdetail.Unit = RUnit[i].ToString();
                    if (!string.IsNullOrEmpty(RAmount[i]))
                    {
                        Rdetail.Amount = Convert.ToInt32(RAmount[i]);
                    }
                    if (!string.IsNullOrEmpty(RUnitPrice[i]))
                    {
                        Rdetail.ExUnit = Convert.ToDecimal(RUnitPrice[i]);
                        //Rdetail.Price = Convert.ToDecimal(RUnitPrice[i]);
                        //ExTotal
                    }
                    if (!string.IsNullOrEmpty(RSubtotal[i]))
                    {
                        Rdetail.ExTotal = Convert.ToDecimal(RSubtotal[i]);
                      //  Rdetail.Subtotal = Convert.ToDecimal(RSubtotal[i]);
                    }
                   
                    Rdetail.CreateTime = DateTime.Now;
                    Rdetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    Rdetail.Validate = "0";
                    Rlist.Add(Rdetail);
                }

                string strErr = "";
                bool b = SalesManage.SaveUpdateReturnGoods(excgoods, Rlist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = excgoods.EID;
                    salesLog.LogContent = "修改退货单";
                    salesLog.ProductType = "退货单修改";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "", Msg = "验证不通过" });
            }
        }
        public ActionResult GetExchangeGoodsDetail()
        {
            string EID = Request["EID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.GetExchangeGoodsDetail(EID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetExchangeDetailByDID()
        {
            string DID = Request["DID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.GetExchangeDetailByDID(DID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateReturnDetail()
        {
            ExReturn_Detail returnDeatil = new ExReturn_Detail();
            returnDeatil.EID = Request.QueryString["EID"].ToString();
            return View(returnDeatil);
        }
        public ActionResult GetReturnDetailByDID()
        {
            string DID = Request["ID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.GetReturnDetailByDID(DID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult ExchangeCheckGoods()
        {
            Exchange_Check ExCheck = new Exchange_Check();
            ExCheck.TID = SalesManage.getNewExCheckID();
            ExCheck.EID = Request.QueryString["EID"].ToString();
            ExCheck.RememberPeople = GAccount.GetAccountInfo().UserName;
            return View(ExCheck);
        }

        public ActionResult GetExcAndReturnDetailByEID()
        {
            string EID = Request["EID"].ToString();
            string strErr = "";
            DataTable dt = SalesManage.GetExcAndReturnDetailByEID(EID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SaveExChangeCheck(Exchange_Check check)
        {
            if (ModelState.IsValid)
            {
                check.State = "1";
                check.Validate = "v";
                check.CreateTime = DateTime.Now.ToString();
                check.CreateUser = GAccount.GetAccountInfo().UserName;
                check.IState = "0";
                check.ProductionState = "0";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] SpecsModels = Request["Specifications"].Split(',');
                string[] PackWreck = Request["PackWreck"].Split(',');
                string[] FeatureWreck = Request["FeatureWreck"].Split(',');
                string[] Componments = Request["Componments"].Split(',');
                string[] Quality = Request["Quality"].Split(',');
                string[] Remark = Request["Remark"].Split(',');
                string[] DID = Request["DID"].Split(',');
                List<ExchangeGoods_DetailInfo> listDetailInfo = new List<ExchangeGoods_DetailInfo>();

                for (int i = 0; i < SpecsModels.Length - 1; i++)
                {
                    ExchangeGoods_DetailInfo exchangedetialinfo = new ExchangeGoods_DetailInfo();
                    exchangedetialinfo.TID = check.TID;
                    exchangedetialinfo.TDID = check.TID + string.Format("{0:D3}", i + 1);
                    exchangedetialinfo.DID = DID[i].ToString();
                    exchangedetialinfo.ProductID = ProductID[i].ToString();
                    exchangedetialinfo.SpecsModels = SpecsModels[i].ToString();
                    exchangedetialinfo.PackWreck = PackWreck[i].ToString();
                    exchangedetialinfo.FeatureWreck = FeatureWreck[i].ToString();
                    exchangedetialinfo.Componments = Componments[i].ToString();
                    exchangedetialinfo.Quality = Quality[i].ToString();
                    exchangedetialinfo.Remark = Remark[i].ToString();
                    exchangedetialinfo.CreateTime = DateTime.Now.ToString();
                    exchangedetialinfo.Validate = "v";
                    exchangedetialinfo.CreateUser = GAccount.GetAccountInfo().UserName;
                    listDetailInfo.Add(exchangedetialinfo);
                }
                string strErr = "";
                bool b = SalesManage.SaveExchangeCheck(check, listDetailInfo, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = check.TID;
                    salesLog.LogContent = "新增检验单";
                    salesLog.ProductType = "新增检验单";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "", Msg = "" });
            }
        }

        public ActionResult CanCelExchangGoods()
        {
            string EID = Request["ID"].ToString();
            string strErr = "";
            bool b = SalesManage.CanCelExchangGoods(EID, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult ExchangeGoodsFinish()
        {
            ExchangeGoods exchangeGoods = new ExchangeGoods();
            exchangeGoods.EID = Request.QueryString["EID"].ToString();
            exchangeGoods.OrderID = Request.QueryString["OrderID"].ToString();
            exchangeGoods.ExFinishDealPeo = GAccount.GetAccountInfo().UserName;
            return View(exchangeGoods);
        }

        public ActionResult SaveExchangeGoodsFinish(ExchangeGoods exGoods)
        {
            if (ModelState.IsValid)
            {
                exGoods.State = "3";
                string strErr = "";
                bool b = SalesManage.SaveExchangeGoodsFinish(exGoods, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = exGoods.EID;
                    salesLog.LogContent = "退换货完成";
                    salesLog.ProductType = "退换货完成";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "false", Msg = "验证不通过" });
            }
        }

        public FileResult ExchangeGoodsManageToExcel()
        {
            string OrderID = Request["OrderID"];

            string OrderUnit = Request["OrderUnit"];
            string Brokerage = Request["Brokerage"];
            // string ChangeDate = Request["ChangeDate"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            string State = Request["State"];
            string s = "";
            string where = "";
            if (!string.IsNullOrEmpty(OrderID))
            {
                s += "  and  b.OrderID like '%" + OrderID + "%'";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += "  and b.OrderUnit like'%" + OrderUnit + "%'";

            }
            if (!string.IsNullOrEmpty(Brokerage))
            {
                s += "  and a.Brokerage like'%" + Brokerage + "%'";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += "  and a.ChangeDate between  '" + StartDate + "' and '" + EndDate + "'";
            }
            if (!string.IsNullOrEmpty(State))
            {
                if (State == "0")
                {
                    //  s += "  (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) ";
                }
                else
                {
                    s += "  and a.State =" + State + " ";
                }
            }
            if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(State))
            {
                //  s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " " + s; }
            string strErr = "";
            DataTable dt = SalesManage.ExchangeGoodsManageToExcel(where, ref strErr);
            if (dt != null)
            {
                string strCols = "退货单号-5000,订货编号-5000,退货日期-5000,退货方式-5000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "退换货信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "退换货信息表.xls");
            }
            else
                return null;
        }
        public ActionResult GetExChangeGoodsByOID()
        {
            // string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string OrderID = Request["OrderID"];

            UIDataTable udtTask = SalesManage.GetExChangeGoodsByOID(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintExChangeGoods()
        {
            ExchangeGoods exchangegoods = SalesManage.GetExchangeGoods(Request["EID"].ToString());

           // Exchange_Check exchangecheck = SalesManage.GetExchangeCheck();
            // OrdersInfo Ordersinfo = new OrdersInfo();
            OrdersInfo Ordersinfo = SalesManage.GetOrdersByOrderID(exchangegoods.OrderID);
            string strErr = "";
            DataTable dt = SalesManage.GetReturnDetailByEID(exchangegoods.EID, ref strErr);
            DataTable dt2 = SalesManage.GetExchangeGoodsDetail(exchangegoods.EID, ref strErr);
            DataTable dt3 = SalesManage.GetExchangeGoodsCheckDetail(exchangegoods.EID, ref strErr);//获取检验表里面的数据
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();//鉴定结果处理意见
            StringBuilder sb5 = new StringBuilder();//获取检验数据
            int r = dt.Rows.Count + 3;
            sb.Append("<div id='PrintArea' style='page-break-after: always; height :1000px;'>");
            sb.Append("<div id='' style='text-align:center;font-size:18px; margin-top:10px;'>北京市燕山工业燃气设备有限公司产品退换货单</div>");
            sb.Append("<div id=''><table><tr><td colspan='3'>部门流水编号：</td><td  colspan='7'>YSGLJL-XS-F04</td></tr></table></div>");
            //<td   style='width:10px;border: 1px solid #000;' rowspan='" + r + "'>退换产品部门填写</td>
            sb.Append("<table id='Botom' class='tabInfo' style='width :98% ;'><tr><td style='border: 1px solid #000;width:30px'>退换产品部门</td><td style='border: 1px solid #000;width:80px'><input type='checkbox' checked='checked' id='Amount' value='A' style='width:30px;'/ colspan='2'>销售分部</td><td  style='border: 1px solid #000;width:80px'><input type='checkbox' id='Amount' value='A'style='width:30px;'/>用户服务部</td><td  style='border: 1px solid #000;width:30px'>经办人</td><td  style='border: 1px solid #000;width:80px'>" + exchangegoods.Brokerage + "</td><td  style='border: 1px solid #000;width:30px'>原合同编号</td><td  style='border: 1px solid #000;width:80px' colspan='4'>" + Ordersinfo.ContractID + "</td></tr><tr><td  style='border: 1px solid #000;' style='border: 1px solid #000;'>使用单位</td><td  style='border: 1px solid #000;' colspan='3'>" + Ordersinfo.UseUnit + "</td><td style='border: 1px solid #000;'>联系人</td><td  style='border: 1px solid #000;' colspan='2'>" + Ordersinfo.UseContactor + "</td><td style='border: 1px solid #000;'>联系电话</td><td  style='border: 1px solid #000;width:10px' colspan='3'>" + Ordersinfo.UseTel + "</td></tr>");
            //<tr><td style='border: 1px solid #000;' colspan='2'>A、推介产品错<input type='checkbox' id='Amount' value='A'style='width:30px;'/></td><td  style='border: 1px solid #000;'colspan='2'> B、客户自订产品错<input type='checkbox' id='Amount' value='A'style='width:30px;'/> </td><td style='border: 1px solid #000;' colspan='2'>C、无理由退货<input type='checkbox' id='Amount' value='A'style='width:30px;'/></td><td style='border: 1px solid #000;' colspan='5'> D产品质量问题<input type='checkbox' id='Amount' value='A'style='width:30px;'/></td></tr>
            sb2.Append("<tr><td  style='border: 1px solid #000;'>客户退换产品原因（理由）：</td><td colspan='9' style='border: 1px solid #000;'>" + exchangegoods.ReturnReason + "</td></tr><tr style='background-color: #88c9e9; text-align: left; '><td style='border: 1px solid #000;' colspan='10'>  退货信息</td></tr><tr><td style='border: 1px solid #000;' colspan='3'>退货类型</td><td colspan='2' style='border: 1px solid #000;'>" + exchangegoods.ReturnType + "</td><td style='border: 1px solid #000;' colspan='3'>退货方式</td><td colspan='2' style='border: 1px solid #000;' >" + exchangegoods.ReturnWay + "</td></tr><tr><td style='border: 1px solid #000;' colspan='3'>退货约定</td><td colspan='10' style='border: 1px solid #000;'>" + exchangegoods.ReturnContract + "</td><tr><td style='border: 1px solid #000;' colspan='3'>退货说明</td><td style='border: 1px solid #000;' colspan='10'>" + exchangegoods.ReturnInstructions + "</td></tr><tr><td  style='border: 1px solid #000;' colspan='2'>退换经办人要求：</td><td style='border: 1px solid #000;'  colspan='9'>" + exchangegoods.ISEXR + "</td></tr><tr><td style='width:10%;border: 1px solid #000;'>退换经办人签字</td><td style='width:10%;border: 1px solid #000;'></td><td  style='width:10%;border: 1px solid #000;'>日期</td><td style='width:10%;border: 1px solid #000;'>" + exchangegoods.ChangeDate + "</td><td  style='width:10%;border: 1px solid #000;'>部门负责人签字</td><td style='border: 1px solid #000;' colspan='2' ></td><td  style='width:10%;border: 1px solid #000;'>日期</td><td colspan='2' style='border: 1px solid #000;' colspan='3'>" + exchangegoods.ChangeDate + "</td></tr>");
            //"  <tr style='background-color: #88c9e9; text-align: left; '><td style='border: 1px solid #000;'>鉴定结果</td>"+

            //sb5.Append("<div><table  id='Bottom2' class='tabInfo' style='width :98% ;'><tr><td>规格型号</td><td>产品编码</td><td>包装折损情况</td><td>外观折损情况</td><td>零配件少损情况</td><td>质量情况</td></tr>");
            //<td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A' style='width:30px;border: 1px solid #000;'/>产品质量问题</td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A' style='width:30px;'/>产品材质问题</td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A'style='width:30px;'/>客户使用操作问题 </td><td style='border: 1px solid #000;'><input type='checkbox' id='Amount' value='A' style='width:30px;'/>非本公司责任 </td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A'style='width:30px;' />其他 </td>\//<td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A' style='width:30px;border: 1px solid #000;'/>产品质量问题</td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A' style='width:30px;'/>产品材质问题</td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A'style='width:30px;'/>客户使用操作问题 </td><td style='border: 1px solid #000;'><input type='checkbox' id='Amount' value='A' style='width:30px;'/>非本公司责任 </td><td style='border: 1px solid #000;' colspan='2'><input type='checkbox' id='Amount' value='A'style='width:30px;' />其他 </td>

            sb4.Append("<tr style='background-color: #88c9e9; text-align: left; '><td style='border: 1px solid #000;'>鉴定结果</td><td style='border: 1px solid #000;' colspan='9'>"+exchangegoods.ReturnType+"</td></tr><tr><td colspan='2' style='border: 1px solid #000;'>鉴定人签字</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;' colspan='2'>部门负责人签字</td><td style='border: 1px solid #000;' colspan='3'></td></tr><tr><td style='border: 1px solid #000;' colspan='2'>签字日期</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;' colspan='2'>签字日期</td><td style='border: 1px solid #000;' colspan='4'></td></tr><tr><td style='border: 1px solid #000;'>处理意见</td><td style='border: 1px solid #000;' colspan='11'>1、按退换经办人要求退换产品。</br>	2、退换原因中A、B、C均为退换部门责任。</br>	3、退换原因中D为生产部门责任。</td>	</tr><tr><td style='border: 1px solid #000;' rowspan='2'>会签</td><td style='border: 1px solid #000;'>退换部门负责人</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;'>生产部门负责人</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;'>仓库部门负责人</td><td style='border: 1px solid #000;' colspan='2'></td></tr><tr><td style='border: 1px solid #000;' style='border: 1px solid #000;'>签字日期</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;'>签字日期</td><td style='border: 1px solid #000;' colspan='2'></td><td style='border: 1px solid #000;'>签字日期</td><td style='border: 1px solid #000;' colspan='2'></td></tr></table></div>");
            string html = "";
            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<tr style='background-color: #88c9e9; text-align: left; '> <td style='border: 1px solid #000;' colspan='11'> 退货货品</td> </tr> <tr align='center' valign='middle'> <th style='border: 1px solid #000;' class='th'> 序号 </th> <th style='border: 1px solid #000;' class='th'> 物品编号 </th> <th style='border: 1px solid #000;' class='th'> 物品名称 </th> <th style='border: 1px solid #000;' class='th'> 规格型号 </th> <th style='border: 1px solid #000;' class='th'> 单位 </th> <th style='border: 1px solid #000;' class='th'> 供应商 </th> <th style='border: 1px solid #000;' class='th'> 数量 </th> <th style='border: 1px solid #000;' class='th' colspan='2'> 单价 </th> <th style='border: 1px solid #000;' class='th' colspan='2'> 成交价 </th> </tr> ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr  id ='" + i + "' ><td style='border: 1px solid #000;'>" + (i + 1) + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["ProductID"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["OrderContent"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Specifications"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Unit"] + "</td><td style='border: 1px solid #000;' >" + dt.Rows[i]["Supplier"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[i]["Amount"] + "</td><td style='border: 1px solid #000;' colspan='2'>" + dt.Rows[i]["ExUnit"] + "</td><td style='border: 1px solid #000;' colspan='2'>" + dt.Rows[i]["ExTotal"] + "</td></tr>");
                }
                //sb1.Append("</tbody></table></div>");
                //  html += sb.ToString() + sb1.ToString() + sb2.ToString();
                if (dt2.Rows.Count <= 6)
                {
                    sb3.Append("<tr style='background-color: #88c9e9; text-align: left; '> <td style='border: 1px solid #000;' colspan='11'> 换货</td> </tr> <tr align='center' valign='middle'> <th style='width:10%;border: 1px solid #000;' class='th'> 序号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 物品编号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 物品名称 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 规格型号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 单位 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 供应商 </th> <th style='width:10%;border: 1px solid #000;' class='th'> 数量 </th> <th style='width: 15%;border: 1px solid #000;' class='th' colspan='1'> 单价 </th> <th style='width: 15%;border: 1px solid #000;' class='th' colspan='2'> 成交价 </th>  </tr>");
                    for (int m = 0; m < dt2.Rows.Count; m++)
                    {
                        sb3.Append("<tr  id ='DetailInfo" + m + "' '><td style='border: 1px solid #000;'><lable class='labRowNumber" + m + " ' id='RowNumber" + m + "'>" + (m + 1) + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labProductID" + m + " ' id='ProductID" + m + "'>" + dt2.Rows[m]["ProductID"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labOrderContent" + m + " ' id='OrderContent" + m + "'>" + dt2.Rows[m]["OrderContent"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labSpecifications" + m + " ' id='Spec" + m + "'>" + dt2.Rows[m]["Specifications"] + "</lable> </td><td style='border: 1px solid #000;'><lable class='labUnit" + m + " ' id='Units" + m + "'>" + dt2.Rows[m]["Unit"] + "</lable> </td><td style='border: 1px solid #000;'><lable  class='labSupplier" + m + "'  id='Supplier" + m + "'>" + dt2.Rows[m]["Supplier"] + "</lable></td><td style='border: 1px solid #000;'>" + dt2.Rows[m]["Amount"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[m]["ExUnit"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[m]["ExTotal"] + "</td><td style='display:none;'><lable class='labOrderID" + m + " ' colspan='2' id='OrderID" + m + "'>" + dt2.Rows[m]["EID"] + "</lable> </td><td style='border: 1px solid #000;' ><lable class='labDID" + m + " ' id='DID" + m + "'>" + dt2.Rows[m]["DID"] + "</lable> </td></tr>");

                    }
                    // sb2.Append("</tbody></table></div>");
                    //  html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }


                if (dt3.Rows.Count <= 6)
                {
                    int s = dt3.Rows.Count + 5;
                    sb5.Append("<div><table  id='Bottom2' class='tabInfo' cellpadding='0' cellspacing='0'  style='width :98%;' ><tr><td style='border: 1px solid #000;' rowspan='" + s + "'>生产检验部门填写</td><td style='border: 1px solid #000;'>规格型号</td><td style='border: 1px solid #000;'>产品编码</td><td style='border: 1px solid #000;'> 包装折损情况</td><td style='border: 1px solid #000;'>外观折损情况</td><td style='border: 1px solid #000;'  colspan='2'>零配件少损情况</td><td style='border: 1px solid #000;'  colspan='3'>质量情况</td></tr>");

                    for (int t = 0; t < dt3.Rows.Count; t++)
                    {
                        sb5.Append("<tr><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["SpecsModels"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["ProductID"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["PackWreck"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["FeatureWreck"] + "</td><td style='width: 20%;border: 1px solid #000;' colspan='2'>" + dt3.Rows[t]["Componments"] + "</td><td colspan='3' style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["Quality"] + "</td></tr>");
                    }
                    // sb5.Append("</table></div>");
                }

                html = sb.ToString() + sb1.ToString() + sb3.ToString() + sb2.ToString() + sb5.ToString() + sb4.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                //
                int count2 = dt2.Rows.Count % 6;
                if (count2 > 0)
                    count2 = dt2.Rows.Count / 6 + 1;
                else
                    count2 = dt2.Rows.Count / 6;
                //
                int count3 = dt3.Rows.Count % 6;
                if (count3 > 0)
                    count3 = dt3.Rows.Count / 6 + 1;
                else
                    count3 = dt3.Rows.Count / 6;

                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > dt.Rows.Count)
                        length = 6 * i + dt.Rows.Count % 6;
                    //<div><table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo' style=' width :98% ;'>
                    sb1.Append(" <tr style='background-color: #88c9e9; text-align: left; '> <td style='border: 1px solid #000;' colspan='10'> 退货货品</td> </tr> <tr align='center' valign='middle'> <th style='width:10%;border: 1px solid #000;' class='th'> 序号 </th> <th style='width: 20%;border: 1px solid #000;' class='th'> 物品编号 </th> <th style='width:20%;border: 1px solid #000;' class='th'> 物品名称 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 规格型号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 单位 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 供应商 </th> <th style='width:10%;border: 1px solid #000;' class='th'> 数量 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 单价 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 成交价 </th></tr> ");

                    for (int j = a; j < length; j++)
                    {
                        sb1.Append("<tr  id ='DetailInfo" + j + "'><td style='border: 1px solid #000;'>" + (j + 1) + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["ProductID"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["OrderContent"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Specifications"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Unit"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Supplier"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["Amount"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["ExUnit"] + "</td><td style='border: 1px solid #000;'>" + dt.Rows[j]["ExTotal"] + "</td></tr>");
                    }
                    // sb1.Append("</tbody></table></div>");
                    if ((length - a) < 6)
                    {
                    }
                    // sb2.Append("</tbody></table></div></div>");
                    //   html += sb.ToString() + sb1.ToString();
                    html += sb.ToString() + sb1.ToString() + sb2.ToString() + sb4.ToString();
                }

                for (int i = 0; i < count2; i++)
                {
                    sb3 = new StringBuilder();
                    int a = 6 * i;
                    int length2 = 6 * (i + 1);
                    if (length2 > dt2.Rows.Count)
                        length2 = 6 * i + dt2.Rows.Count % 6;
                    //<div><table id='ReturnTable' cellpadding='0' cellspacing='0' class='tabInfo' style=' width :98% ;'>
                    sb3.Append(" <tr style='background-color: #88c9e9; text-align: left; '> <td style='border: 1px solid #000;' colspan='10'> 换货</td> </tr> <tr align='center' valign='middle'> <th style='width:10%;border: 1px solid #000;' class='th'> 序号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 物品编号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 物品名称 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 规格型号 </th> <th style='width: 10%;border: 1px solid #000;' class='th'> 单位 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 供应商 </th> <th style='width:10%;border: 1px solid #000;' class='th'> 数量 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 单价 </th> <th style='width: 15%;border: 1px solid #000;' class='th'> 成交价 </th> </tr>");

                    for (int j = a; j < length2; j++)
                    {
                        sb3.Append("<tr  id ='DetailInfo" + j + "'><td style='border: 1px solid #000;'>" + (j + 1) + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["ProductID"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["OrderContent"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["Specifications"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["Unit"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["Supplier"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["Amount"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["ExUnit"] + "</td><td style='border: 1px solid #000;'>" + dt2.Rows[j]["ExTotal"] + "</td></tr>");
                    }
                    //  sb1.Append("</tbody></table></div>");
                    if ((length2 - a) < 6)
                    {
                    }
                    // sb2.Append("</tbody></table></div></div>");
                    // html += sb.ToString() + sb1.ToString();
                    html += sb.ToString() + sb3.ToString() + sb2.ToString() + sb4.ToString();

                }

                for (int i = 0; i < count3; i++)
                {
                    sb5 = new StringBuilder();
                    int a = 6 * i;
                    int length3 = 6 * (i + 1);
                    if (length3 > dt3.Rows.Count)
                        length3 = 6 * i + dt3.Rows.Count % 6;
                    int s = count + 5;
                    sb5.Append("<div><table  id='Bottom2' class='tabInfo' cellpadding='0' cellspacing='0'  style='width :98%;' ><tr><td style='border: 1px solid #000;' rowspan='" + s + "'>生产检验部门填写</td><td style='border: 1px solid #000;'>规格型号</td><td style='border: 1px solid #000;'>产品编码</td><td style='border: 1px solid #000;'> 包装折损情况</td><td style='border: 1px solid #000;'>外观折损情况</td><td style='border: 1px solid #000;'  colspan='2'>零配件少损情况</td><td style='border: 1px solid #000;'  colspan='3'>质量情况</td></tr>");

                    for (int t = a; t < length3; t++)
                    {
                        sb5.Append("<tr><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["SpecsModels"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["ProductID"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["PackWreck"] + "</td><td style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["FeatureWreck"] + "</td><td style='width: 20%;border: 1px solid #000;' colspan='2'>" + dt3.Rows[t]["Componments"] + "</td><td colspan='3' style='width: 20%;border: 1px solid #000;'>" + dt3.Rows[t]["Quality"] + "</td></tr>");
                    }
                    if ((length3 - a) < 6)
                    {
                    }
                    // sb5.Append("</table></div>");
                    //  html += sb.ToString() + sb1.ToString() + sb2.ToString() + sb5.ToString() + sb4.ToString();
                    html += sb.ToString() + sb2.ToString() + sb5.ToString() + sb4.ToString();
                }


                //  sb2.Append("</tbody></table></div></div>");
                //sb1.Append("</tbody></table></div>");
                //    html += sb.ToString() + sb1.ToString() +sb3.ToString()+ sb2.ToString() +sb5.ToString()+ sb4.ToString();
            }
            Response.Write(html);
            return View();
        }

        public ActionResult ExchangeBill()
        {
            string ExcID = Request["ID"];
            ExchangeGoods exchangegoods = SalesManage.GetExchangeGoods(ExcID);
            return View(exchangegoods);
        }
        #endregion

        #region [获取供应商]
        public ActionResult GetSupplier()
        {
            string SID = Request["SID"].ToString();
            string ProduID = Request["ProduID"];
            DataTable dt = SalesManage.GetSupplier(SID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetSupType()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetSupType(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCheckSupList()
        {
            //int a_intPageSize, int a_intPageIndex, string ptype
            string where = Request["ptype"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";

            UIDataTable udtTask = SalesManage.GetCheckSupList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Supplier()
        {
            return View();
        }

        #endregion

        #region 零售销售
        public ActionResult RetailManage()
        {
            return View();
        }
        #endregion

        #region 审批
        public ActionResult InternalManage(string op)
        {
            ViewData["op"] = op;
            return View();
        }
        //备案审批
        public ActionResult ApprovalManage(string op)
        {
            string text = "";
            if (op == "BA")
                text = "备案审批";
            else if (op == "BJ")
                text = "报价审批";
            else if (op == "DD")
                text = "订单审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApproval(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ProjectName = sales.PlanName;
                string PlanID = sales.PlanID;
                string RecordContent = sales.MainContent;
                string SpecsModels = sales.SpecsModels;
                string BelongArea = Request["BelongArea"];
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string WorkChief = sales.Manager;
                string State = Request["State"];
                // string HState = Request["HState"];
                string where = "";

                string s = "";
                if (!string.IsNullOrEmpty(ProjectName))
                {
                    s += "and  a.PlanName like '%" + ProjectName + "%' ";
                }
                if (!string.IsNullOrEmpty(PlanID))
                {
                    s += " and a.PlanID like '%" + PlanID + "%' ";
                }
                if (!string.IsNullOrEmpty(RecordContent))
                {
                    s += " and a.MainContent like '%" + RecordContent + "%' ";
                }
                if (!string.IsNullOrEmpty(SpecsModels))
                {
                    s += "  and a.SpecsModels like '%" + SpecsModels + "%'";
                }
                if (!string.IsNullOrEmpty(BelongArea))
                {
                    s += " and a.BelongArea like '%" + BelongArea + "%' ";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and a.RecordDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(WorkChief))
                {
                    s += " and a.Manager like '%" + WorkChief + "%'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "0")
                    {
                        // s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) and";
                    }
                    else
                    {
                        s += " and a.PState =" + State + "";
                    }
                }
                //if (!string.IsNullOrEmpty(HState))
                //{
                //    s += " a.IsPay =" + HState + " ";
                //}
                if (string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(State))
                {
                    //s = s.Substring(3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                //string op = Request["Op"].ToString();
                //if (op == "备案审批")
                //    strWhere += " and a.Type='0' ";
                //else if (op == "报价审批")
                //    strWhere += " and a.Type='1' ";

                string strjson = "";
                UIDataTable udtTask = SalesManage.GetProjectBasApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        //报价审批
        public ActionResult OfferApprovalManage(string op)
        {
            string text = "";
            if (op == "BJ")
                text = "报价审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();

        }

        public ActionResult SearchOfferApproval(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ProjectName = sales.PlanName;
                string PlanID = sales.PlanID;
                string OfferTitle = sales.OfferTitle;
                string BelongArea = Request["BelongArea"];
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string WorkChief = sales.Manager;
                string State = Request["State"];
                string where = "";

                string s = "";
                if (!string.IsNullOrEmpty(ProjectName))
                {
                    s += "and  a.PlanName like '%" + ProjectName + "%' ";
                }
                if (!string.IsNullOrEmpty(PlanID))
                {
                    s += " and a.PlanID like '%" + PlanID + "%' ";
                }

                //if (!string.IsNullOrEmpty(SpecsModels))
                //{
                //    s += "  and a.SpecsModels like '%" + SpecsModels + "%'";
                //}
                if (!string.IsNullOrEmpty(OfferTitle))
                {
                    s += " and b.OfferTitle like '%" + OfferTitle + "%'";
                }
                if (!string.IsNullOrEmpty(BelongArea))
                {
                    s += " and a.BelongArea like '%" + BelongArea + "%' ";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and b.OfferTime between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(WorkChief))
                {
                    s += " and b.OfferContacts like '%" + WorkChief + "%'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "-1")
                    {
                        // s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) and";
                    }
                    else
                    {
                        s += " and b.State =" + State + "";
                    }
                }
                //if (!string.IsNullOrEmpty(HState))
                //{
                //    s += " a.IsPay =" + HState + " ";
                //}
                if (string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(State))
                {
                    //s = s.Substring(3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                //string op = Request["Op"].ToString();
                //if (op == "备案审批")
                //    strWhere += " and a.Type='0' ";
                //else if (op == "报价审批")
                //    strWhere += " and a.Type='1' ";

                string strjson = "";
                UIDataTable udtTask = SalesManage.GetOfferApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        //订单审批
        public ActionResult OrderApprovalManage(string op)
        {
            string text = "";
            if (op == "DD")
                text = "订单审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchOrderApproval(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ContractID = sales.ContractID;
                string OrderUnit = sales.OrderUnit;
                string UseUnit = sales.UseUnit;
                string MainContent = sales.MainContent;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string WorkChief = sales.Manager;
                string State = Request["State"];
                string where = "";

                string s = "";
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += "and  b.ContractID like '%" + ContractID + "%' ";
                }
                if (!string.IsNullOrEmpty(OrderUnit))
                {
                    s += " and b.OrderUnit like '%" + OrderUnit + "%' ";
                }
                if (!string.IsNullOrEmpty(UseUnit))
                {
                    s += " and b.UseUnit like '%" + UseUnit + "%' ";
                }
                if (!string.IsNullOrEmpty(MainContent))
                {
                    s += "  and a.MainContent like '%" + MainContent + "%'";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and b.ContractDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(WorkChief))
                {
                    s += " and a.Manager like '%" + WorkChief + "%'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "-1")
                    {
                        // s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) and";
                    }
                    else
                    {
                        s += " and b.State =" + State + "";
                    }
                }
                if (string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(State))
                {
                    //s = s.Substring(3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesManage.GetOrderApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        //退换货审批
        public ActionResult EXOrderApprovalManage(string op)
        {
            string text = "";
            if (op == "EX")
                text = "退换货审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        public ActionResult SearchEXOrderApproval(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ContractID = sales.ContractID;
                string OrderUnit = sales.OrderUnit;
                string UseUnit = sales.UseUnit;
                string MainContent = sales.MainContent;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string WorkChief = sales.Manager;
                string State = Request["State"];
                string where = "";

                string s = "";
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += "and  b.ContractID like '%" + ContractID + "%' ";
                }
                if (!string.IsNullOrEmpty(OrderUnit))
                {
                    s += " and b.OrderUnit like '%" + OrderUnit + "%' ";
                }
                if (!string.IsNullOrEmpty(UseUnit))
                {
                    s += " and b.UseUnit like '%" + UseUnit + "%' ";
                }
                if (!string.IsNullOrEmpty(MainContent))
                {
                    s += "  and a.MainContent like '%" + MainContent + "%'";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and b.ContractDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                if (!string.IsNullOrEmpty(WorkChief))
                {
                    s += " and a.Manager like '%" + WorkChief + "%'";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "-1")
                    {
                        // s += " (a.State =1 or a.State =2 or a.State =3 or a.State =4 ) and";
                    }
                    else
                    {
                        s += " and b.EXState =" + State + "";
                    }
                }
                if (string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(State))
                {
                    //s = s.Substring(3);
                }
                if (!string.IsNullOrEmpty(s)) { where = "  " + s; }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesManage.GetEXOrderApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        //合同审批

        public ActionResult SalesContractManage(string op)
        {
            string text = "";
            if (op == "HT")
                text = "销售合同审批";
            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        //加载合同数据
        public ActionResult ContractGrid(tk_ContractSearch ContractSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = " and a.Unit = '" + unit + "'";
                if (account.UserRole == "3")
                    where += " and a.BusinessType = 'BT5'";
                string strCurPage;
                string strRowNum;
                string Cname = ContractSearch.Cname;
                string ContractID = ContractSearch.ContractID;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Cname != "" && Cname != null)
                    where += " and a.Cname like '%" + Cname + "%'";
                if (ContractID != "" && ContractID != null)
                    where += " and a.ContractID like '%" + ContractID + "%'";
                UIDataTable udtTask = ContractMan.getNewContractGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public ActionResult CashBackGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string CID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Cid"] != null)
                CID = Request["Cid"].ToString();
            if (CID != "")
                where += " and a.CID = '" + CID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ContractMan.getNewCashBackGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserLogGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string CID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Cid"] != null)
                CID = Request["Cid"].ToString();
            if (CID != "")
                where += " and a.UserId = '" + CID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ContractMan.getNewUserlogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //提交审批页面
        public ActionResult SubmitApproval(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            // ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        public ActionResult UMwebkeyGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string webkey = Request["webkey"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (webkey != "")
                where += " and a.BuType = '" + webkey + "'";
            UIDataTable udtTask = COM_ApprovalMan.getNewUMwebkeyGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertApproval()
        {
            var webkey = Request["data1"];
            var folderBack = Request["data2"];
            var RelevanceID = Request["data3"];
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            if (SalesManage.InsertNewApproval(PID, RelevanceID, webkey, folderBack, ref strErr) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }

        public ActionResult GetContractSPID()
        {
            string CID = Request["cid"].ToString();
            DataTable dt = SalesManage.GetContractSPID(CID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //审批界面
        public ActionResult Approval(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            string RelevanceID = arr[2];
            //string AID=arr[3];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            //  ViewData["AID"] = AID;
            return View();
        }
        public ActionResult UpdateApproval()
        {

            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var reg = "\r";
            Opinion = Opinion.Replace(reg, "");
            Opinion = Opinion.Replace("\n", "");
            var Remark = Request["Remark"];
            Remark = Remark.Replace(reg, "");
            Remark = Remark.Replace("\n", "");
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            // var ID = Request["AID"];
            string strErr = "";
            if (SalesManage.UpdateNewApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }

        public ActionResult ApprovalCondition(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            return View();
        }

        public ActionResult ConditionGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string webkey = Request["webkey"].ToString();
            string PID = "";
            string folderBack = Request["folderBack"].ToString();
            if (Request["PID"] != null)
                PID = Request["PID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SalesManage.getNewConditionGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        //订单修改审批界面

        public ActionResult SubmitUpdateApproval(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }

        //审批日志记录
        public ActionResult GetUserLogGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            //   string webkey = Request["webkey"].ToString();
            string PID = "";
            //string folderBack = Request["folderBack"].ToString();
            if (Request["PID"] != null)
                PID = Request["PID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            // if (PID != "")
            //    where += " where a.RelevanceID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            // if (where != "")
            udtTask = SalesManage.GetUserLogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, PID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region [退货审批]
        //退货审批
        public ActionResult EXSubmitApproval(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        public ActionResult EXUMwebkeyGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string webkey = Request["webkey"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (webkey != "")
                where += " and a.BuType = '" + webkey + "'";
            UIDataTable udtTask = COM_ApprovalMan.getNewUMwebkeyGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EXInsertApproval()
        {
            var webkey = Request["data1"];
            var folderBack = Request["data2"];
            var RelevanceID = Request["data3"];
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            if (SalesManage.EXInsertNewApproval(PID, RelevanceID, webkey, folderBack, ref strErr) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }

        public ActionResult EXGetContractSPID()
        {
            string CID = Request["cid"].ToString();
            DataTable dt = SalesManage.EXGetContractSPID(CID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult EXApproval(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            string RelevanceID = arr[2];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        public ActionResult EXUpdateApproval()
        {

            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            string strErr = "";
            if (SalesManage.EXUpdateNewApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }

        public ActionResult EXApprovalCondition(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            return View();
        }

        public ActionResult EXConditionGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string webkey = Request["webkey"].ToString();
            string PID = "";
            string folderBack = Request["folderBack"].ToString();
            if (Request["PID"] != null)
                PID = Request["PID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SalesManage.EXgetNewConditionGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JudgeAppDisable()
        {
            var webkey = Request["data1"];
            var SPID = Request["data2"];
            Acc_Account account = GAccount.GetAccountInfo();
            string logUser = account.UserID.ToString();
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            int bol = SalesManage.JudgeNewLoginUser(logUser, webkey, folderBack, SPID);
            return Json(new { success = "true", intblo = bol });
        }

        public ActionResult ExJudgeAppDisable()
        {
            var webkey = Request["data1"];
            var SPID = Request["data2"];
            Acc_Account account = GAccount.GetAccountInfo();
            string logUser = account.UserID.ToString();
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            int bol = SalesManage.ExJudgeNewLoginUser(logUser, webkey, folderBack, SPID);
            return Json(new { success = "true", intblo = bol });
        }
        // 
        #endregion
        #endregion

        #region [结算]
        public ActionResult OrdersSettlement()
        {
            ProFinish proFinish = new ProFinish();
            proFinish.PID = SalesManage.getNewFinishID();// Request["PID"].ToString();
            proFinish.OrderID = Request["id"].ToString();
            ViewData["PID"] = proFinish.PID;
            ViewData["ContractID"] = SalesManage.GetContractID(proFinish.OrderID);
            proFinish.DebtAmount = SalesManage.getNewDebtAmount(proFinish.OrderID);
            if (proFinish.DebtAmount == 0)
                proFinish.IsDebt = "0";
            else
                proFinish.IsDebt = "1";
            return View(proFinish);
        }

        public ActionResult InsertProFinish(ProFinish profinish)
        {

            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                profinish.CreateUser = account.UserID.ToString();
                profinish.CreateTime = DateTime.Now.ToString();
                //  profinish.
                string strErr = "";
                if (SalesManage.InsertProFinish(profinish, ref strErr) == true)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = profinish.PID;
                    salesLog.LogContent = "订单结算";
                    salesLog.ProductType = "订单结算";

                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }


        }
        #endregion

        #region [统计分析]
        public ActionResult StatisticalManage()
        {
            return View();

        }


        public ActionResult GetOrdersInfoStatisticalGrid()
        {

            tk_SalesGrid SalesSearch = new tk_SalesGrid();
            SalesSearch.StartDate = Request["StartDate"];
            SalesSearch.EndDate = Request["EndDate"];
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string StartDate = Request["StartDate"];
                string EndDate = Request["EndDate"];
                ViewData["StartDate"] = StartDate;
                ViewData["EndDate"] = EndDate;
                string where = "";
                if (StartDate != "" && StartDate != null && EndDate != "" && EndDate != null)
                    where += " and a.ContractDate  between '" + StartDate + "' and '" + EndDate + "'";
                DataTable dt = new DataTable();
                //if (where != "")
                //{
                //    dt = SalesManage.GetOrderStaticalGrid(where);
                //}
                StringBuilder sb = new StringBuilder();
                string countStr = SalesManage.GetOrderStatistical(where);
                string[] arr = countStr.Split('@');
                if (arr.Length > 0)
                {
                    string sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + StartDate + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + EndDate + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">");
                    //sb.Append("<tr  class=\"left\"><td style=\"width:20%\">规则型号</td><td style=\"width:20%\">物资编号</td><td style=\"width:20%\">合同编号</td><td style=\"width:10%\">订货单位</td><td style=\"width:10%\">订货内容</td><td style=\"width:10%\">单位</td><td style=\"width:10%\">数量</td><td style=\"width:10%\">订单下发日期</td><td style=\"width:10%\">交货日期</td><td style=\"width:10%\">发货下发通知日期</td><td style=\"width:10%\">实际送货数量</td><td style=\"width:10%\">未发数量</td><td style=\"width:10%\">产品编号</td><td style=\"width:10%\">送货地址</td><td style=\"width:10%\">接货人电话</td><td style=\"width:10%\">备注</td></tr>");
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["ContractID"].ToString() + "</td><td>" + dt.Rows[i]["OrderID"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["OrderContactor"].ToString() + "</td><td>" + dt.Rows[i]["OrderNum"].ToString() + "</td><td>" + dt.Rows[i]["ShipmentNum"].ToString() + "</td><td>" + dt.Rows[i]["WFSL"].ToString() + "</td><td>" + dt.Rows[i]["ContractDate"].ToString() + "</td><td>" + dt.Rows[i]["OrderTel"].ToString() + "</td><td>" + dt.Rows[i]["OrderAddress"].ToString() + "</td><td>" + dt.Rows[i]["UseUnit"].ToString() + "</td><td>" + dt.Rows[i]["Total"].ToString() + "</td><td>" + dt.Rows[i]["UseContactor"].ToString() + "</td><td>" + dt.Rows[i]["UseContactor"].ToString() + "</td><td>" + dt.Rows[i]["UseContactor"].ToString() + "</td><td>" + dt.Rows[i]["UseContactor"].ToString() + "</td><td>" + dt.Rows[i]["UseContactor"].ToString() + "</td></tr>");
                    //}
                    // sb.Append("</table>");
                    return Json(new { success = "true", strSign = sign.ToString() });
                }
                else
                {
                    return Json(new { success = "false" });
                }
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }

        }
        public ActionResult GetOrderSGrid()
        {
            tk_ProjectSearch SalesGrid = new tk_ProjectSearch();
            SalesGrid.StartDate = Request["StartDate"];
            SalesGrid.EndDate = Request["EndDate"];
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "20";
                string where = "";

                string StartDate = SalesGrid.StartDate;
                string EndDate = SalesGrid.EndDate;
                if (StartDate != "" && StartDate != null && EndDate != "" && EndDate != null)
                    where += " and a.ContractDate  between '" + StartDate + "' and '" + EndDate + "'";
                UIDataTable udtTask = SalesManage.GetOrdersInfoStatisticalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                //  strSb = sb.ToString(), strSign = sign.ToString()
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";


                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult StatisticsManageTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "20";
                string where = "";
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.CreateTime >= '" + start + "' and b.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetStatisticsManageTable(where);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    int ShipmentNum = 0;
                    int Amount = 0;
                    int Debt = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:5%\">规格型号</td><td style=\"width:5%\">物料编号</td><td style=\"width:10%\">合同编号</td><td style=\"width:5%\">订货单位</td><td style=\"width:5%\">订货内容</td><td style=\"width:5%\">单位</td>" +
        "<td style=\"width:5%\">数量</td><td style=\"width:10%\">交（提)货日期</td>" +
        "<td style=\"width:3%\">实际送货数量</td><td style=\"width:3%\">未发数量</td>" +
        "<td style=\"width:10%\">接货人电话</td><td style=\"width:5%\">备注</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["OrderNum"].ToString() == "")
                            Amount += 0;
                        else
                            Amount += Convert.ToInt32(dt.Rows[i]["OrderNum"]);
                        if (dt.Rows[i]["ShipmentNum"].ToString() == "")
                            ShipmentNum += 0;
                        else
                            ShipmentNum += Convert.ToInt32(dt.Rows[i]["ShipmentNum"]);
                        if (dt.Rows[i]["SFSL"].ToString() == "")
                            Debt += 0;
                        else
                            Debt += Convert.ToInt32(dt.Rows[i]["SFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["ProductID"].ToString() + "</td><td>" + dt.Rows[i]["ContractID"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["OrderContent"].ToString() + "</td><td>" + dt.Rows[i]["DW"].ToString() + "</td><td>" + dt.Rows[i]["OrderNum"].ToString() + "</td><td>" + dt.Rows[i]["DeliveryTime"].ToString() + "</td><td>" + dt.Rows[i]["ShipmentNum"].ToString() + "</td><td>" + dt.Rows[i]["SFSL"].ToString() + "</td><td>" + dt.Rows[i]["OrderTel"].ToString() + "</td><td>" + dt.Rows[i]["Remark"].ToString() + "</td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td></td><td></td><td></td><td>" + Amount + "</td><td></td><td>" + ShipmentNum + "</td><td>" + Debt + "</td><td></td><td></td></tr>");
                    string countStr = SalesManage.GetOrderStatistical(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }



        //年度累计销售汇总

        public ActionResult SalesSummary()
        {
            return View();
        }
        public ActionResult SalesSummaryTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            if (ModelState.IsValid)
            {
                //string strCurPage;
                //string strRowNum;
                //if (Request["curpage"] != null)
                //    strCurPage = Request["curpage"].ToString();
                //if (Request["rownum"] != null)
                //    strRowNum = Request["rownum"].ToString();
                //else
                //    strRowNum = "20";
                string where = "";
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.SalesSummaryTable(where);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    decimal QK = 0;
                    decimal SubTotal = 0;
                    decimal HK = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\">序号</td><td style=\"width:20%\">姓名</td><td style=\"width:20%\">总额</td><td style=\"width:20%\">已收款</td><td style=\"width:20%\">欠款</td><td style=\"width:20%\">回款率</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SubTotal"].ToString() == "")
                            SubTotal += 0;
                        else
                            SubTotal += Convert.ToInt64(dt.Rows[i]["SubTotal"]);
                        if (dt.Rows[i]["HK"].ToString() == "")
                            HK += 0;
                        else
                            HK += Convert.ToInt32(dt.Rows[i]["HK"]);
                        if (dt.Rows[i]["QK"].ToString() == "")
                            QK += 0;
                        else
                            QK += Convert.ToInt64(dt.Rows[i]["QK"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["CreateUser"].ToString() + "</td><td>" + dt.Rows[i]["SubTotal"].ToString() + "</td><td>" + dt.Rows[i]["HK"].ToString() + "</td><td>" + dt.Rows[i]["QK"].ToString() + "</td><td>" + dt.Rows[i]["HKL"] + "</td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td>" + SubTotal + "</td><td>" + HK + "</td><td>" + QK + "</td><td></td></tr>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult SalesSummaryToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.SalesSummaryTable(where);
            if (dt != null)
            {
                decimal QK = 0;
                decimal SubTotal = 0;
                decimal HK = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SubTotal"].ToString() == "")
                        SubTotal += 0;
                    else
                        SubTotal += Convert.ToInt64(dt.Rows[i]["SubTotal"]);
                    if (dt.Rows[i]["HK"].ToString() == "")
                        HK += 0;
                    else
                        HK += Convert.ToInt32(dt.Rows[i]["HK"]);
                    if (dt.Rows[i]["QK"].ToString() == "")
                        QK += 0;
                    else
                        QK += Convert.ToInt64(dt.Rows[i]["QK"]);
                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                row["CreateUser"] = "合计";
                row["SubTotal"] = SubTotal;
                row["HK"] = HK;
                row["QK"] = QK;
                row["HKL"] = "";
                dt.Rows.Add(row);
                string strCols = "序号-5000,姓名-5000,合同额-5000,已收款-5000,欠款-5000,回款率-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelBYSales(dt, "年度累计销售汇总", strCols.Split(','), ProjectSearch.StartDate ,ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "年度累计销售汇总.xls");
            }
            else
                return null;

        }
        //本月销售汇总
        public ActionResult DepartmentSalesSummary()
        {
            return View();
        }
        public ActionResult DepartmentSalesSummaryTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetMonthsSalesSummaryTable(where);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    double QK = 0;
                    double SubTotal = 0;
                    double HK = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\">序号</td><td style=\"width:20%\">姓名</td><td style=\"width:20%\">总额</td><td style=\"width:20%\">已收款</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SubTotal"].ToString() == "")
                            SubTotal += 0;
                        else
                            SubTotal += Convert.ToDouble(dt.Rows[i]["SubTotal"]);
                        if (dt.Rows[i]["HK"].ToString() == "" || dt.Rows[i]["HK"] == null)
                            HK += 0;
                        else
                            HK += Convert.ToDouble(dt.Rows[i]["HK"]);

                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["CreateUser"].ToString() + "</td><td>" + dt.Rows[i]["SubTotal"].ToString() + "</td><td>" + dt.Rows[i]["HK"].ToString() + "</td></tr>");
                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td>" + SubTotal + "</td><td>" + HK + "</td></tr>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        // sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult DepartmentSalesSummaryToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetMonthsSalesSummaryTable(where);
            if (dt != null)
            {
                decimal QK = 0;
                decimal SubTotal = 0;
                decimal HK = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SubTotal"].ToString() == "")
                        SubTotal += 0;
                    else
                        SubTotal += Convert.ToInt64(dt.Rows[i]["SubTotal"]);
                    if (dt.Rows[i]["HK"].ToString() == "" || dt.Rows[i]["HK"] == null)
                        HK += 0;
                    else
                        HK += Convert.ToInt32(dt.Rows[i]["HK"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                row["CreateUser"] = "合计";
                row["SubTotal"] = SubTotal;
                row["HK"] = HK;

                dt.Rows.Add(row);
                string strCols = "序号-5000,姓名-5000,合同额-5000,已收款-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelBYSales(dt, "本月销售汇总", strCols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "本月销售汇总.xls");
            }
            else
                return null;
        }
        //燕山输配产品部回款/欠款统计
        public ActionResult PaymentStatistics()
        {
            return View();

        }
        //合同应收款明细汇总表
        public ActionResult ContractSummary()
        {
            return View();
        }
        //销售合同电子台账
        public ActionResult ContractElectronicLedger()
        {
            return View();
        }
        //设备销售汇总
        public ActionResult EquipmentSalesSummary()
        {
            return View();
        }
        public ActionResult GetEquipmentSalesSummaryTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetEquipmentSalesSummaryTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    double YCB = 0;
                    double YMLR = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td style=\"width:20%\" rowspan='2'>自有产品分类</td><td style=\"width:20%\" colspan='2' colspan='2'>月份</td><td style=\"width:20%\" colspan='4' colspan='4'>年度累计</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>合同额</td><td>成本</td><td>毛利润</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["YAount"].ToString() == "" || dt.Rows[i]["YAount"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["YAount"]);
                        if (dt.Rows[i]["YSubTotal"].ToString() == "" || dt.Rows[i]["YSubTotal"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["YSubTotal"]);
                        if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                            YCB += 0;
                        else
                            YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["Text"] + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["YAount"].ToString() + "</td><td>" + dt.Rows[i]["YSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["YCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td></tr>");
                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + YSubTotal + "</td><td>" + YCB + "</td><td>" + YMLR + "</td></tr>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        // sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult EquipmentSalesSummaryToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetEquipmentSalesSummaryTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double YCB = 0;
                double YMLR = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["YAount"].ToString() == "" || dt.Rows[i]["YAount"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["YAount"]);
                    if (dt.Rows[i]["YSubTotal"].ToString() == "" || dt.Rows[i]["YSubTotal"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["YSubTotal"]);
                    if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                        YCB += 0;
                    else
                        YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                row["Text"] = "合计";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["YAount"] = YAount;
                row["YSubTotal"] = YSubTotal;
                row["YCB"] = YCB;
                row["YMLR"] = YMLR;

                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                string strTols = "数量@5000,合同额@5000,数量(台)@5000,合同额@5000,成本@5000,毛利润@5000";//
                System.IO.MemoryStream stream = ExcelHelper.EquipmentSalesSummaryTableToExcel(dt, "设备销售情况汇总表", start, strTols.Split(','), end);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "设备销售情况汇总表.xls");
            }
            else
                return null;
        }

        //调压箱销售情况汇总表
        public ActionResult PressureRegulatingBox()
        {
            return View();
        }
        public ActionResult PressureRegulatingBoxTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetPressureRegulatingBoxTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    double YCB = 0;
                    double YMLR = 0;
                    double SJJ = 0;
                    double DWCB = 0;
                    double LJCB = 0;
                    double ZJXB = 0;
                    double YFHSL = 0;
                    double DFHSL = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td style=\"width:20%\" rowspan='2'>常规/技改</td><td  rowspan='2'>规格型号</td><td  rowspan='2'>单位</td><td colspan='2'>月份</td><td colspan='7'>年度累计</td><td colspan='3'>状态</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>销售均价</td><td>合同额</td><td>单位成本</td><td>累计直接成本</td><td>毛利润</td><td>直接成本占销售价比例</td><td>已发货数量</td><td>待发货数量</td></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                        if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                            SJJ += 0;
                        else
                            SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                        if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                        if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                            DWCB += 0;
                        else
                            DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                        //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                        //    YCB += 0;
                        //else
                        //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                        if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                            LJCB += 0;
                        else
                            LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                            ZJXB += 0;
                        else
                            ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                        //YFHSL
                        if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                            YFHSL += 0;
                        else
                            YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                        if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                            DFHSL += 0;
                        else
                            DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["ISF"] + "</td><td>" + dt.Rows[i]["YSpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["SL"].ToString() + "</td><td>" + dt.Rows[i]["SJJ"].ToString() + "</td><td>" + dt.Rows[i]["HTE"].ToString() + "</td><td>" + dt.Rows[i]["DWCB"].ToString() + "</td><td>" + dt.Rows[i]["LJCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td><td>" + dt.Rows[i]["ZJXB"].ToString() + "</td><td>" + dt.Rows[i]["YFHSL"].ToString() + "</td><td>" + dt.Rows[i]["DFSL"].ToString() + "</td></tr>");

                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td></td><td></td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + SJJ + "</td><td>" + YSubTotal + "</td><td>" + DWCB + "</td><td>" + LJCB + "</td><td>" + YMLR + "</td><td>" + ZJXB + "</td><td>" + YFHSL + "</td><td>" + DFHSL + "</td></tr></table>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult PressureRegulatingBoxToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetPressureRegulatingBoxTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double YCB = 0;
                double YMLR = 0;
                double SJJ = 0;
                double DWCB = 0;
                double LJCB = 0;
                double ZJXB = 0;
                double YFHSL = 0;
                double DFHSL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                    if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                        SJJ += 0;
                    else
                        SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                    if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                    if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                        DWCB += 0;
                    else
                        DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                    //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                    //    YCB += 0;
                    //else
                    //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                    if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                        LJCB += 0;
                    else
                        LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                    if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                        ZJXB += 0;
                    else
                        ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                    //YFHSL
                    if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                        YFHSL += 0;
                    else
                        YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                    if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                        DFHSL += 0;
                    else
                        DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                row["ISF"] = 0;
                row["YSpecsModels"] = "合计";
                row["OrderUnit"] = "";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["SL"] = YAount;
                row["SJJ"] = SJJ;
                row["HTE"] = YSubTotal;
                row["DWCB"] = DWCB;
                row["LJCB"] = LJCB;
                row["YMLR"] = YMLR;
                row["ZJXB"] = ZJXB;
                row["YFHSL"] = YFHSL;
                row["DFSL"] = DFHSL;
                //  row[]
                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                //销售数量	合同额	数量	销售均价	合同额	单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量

                string strTols = "销售数量@5000,合同额@5000,数量(台)@5000,销售均价@5000,合同额@5000,单位成本@5000,累计直接成本,毛利润@5000,直接成本占销售价比例@5000,已发货数量@5000,代发货数量@5000,生产中数量@5000";//
                System.IO.MemoryStream stream = ExcelHelper.PressureRegulatingBoxToExcel(dt, "调压箱销售情况汇总表", start, strTols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "调压箱销售情况汇总表.xls");
            }
            else
                return null;
        }

        //高压箱
        public ActionResult HighVoltageCompartment()
        {
            return View();
        }
        public ActionResult GetHighVoltageCompartmentTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetHighVoltageCompartmentTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    // double YCB = 0;
                    double YMLR = 0;
                    double SJJ = 0;
                    double DWCB = 0;
                    double LJCB = 0;
                    double ZJXB = 0;
                    double YFHSL = 0;
                    double DFHSL = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td style=\"width:20%\" rowspan='2'>名称</td><td  rowspan='2'>规格型号</td><td  rowspan='2'>单位</td><td colspan='2'>月份</td><td colspan='7'>年度累计</td><td colspan='3'>状态</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>销售均价</td><td>合同额</td><td>单位成本</td><td>累计直接成本</td><td>毛利润</td><td>直接成本占销售价比例</td><td>已发货数量</td><td>待发货数量</td></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                        if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                            SJJ += 0;
                        else
                            SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                        if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                        if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                            DWCB += 0;
                        else
                            DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                        //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                        //    YCB += 0;
                        //else
                        //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                        if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                            LJCB += 0;
                        else
                            LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                            ZJXB += 0;
                        else
                            ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                        //YFHSL
                        if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                            YFHSL += 0;
                        else
                            YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                        if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                            DFHSL += 0;
                        else
                            DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["OrderContent"] + "</td><td>" + dt.Rows[i]["YSpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["SL"].ToString() + "</td><td>" + dt.Rows[i]["SJJ"].ToString() + "</td><td>" + dt.Rows[i]["HTE"].ToString() + "</td><td>" + dt.Rows[i]["DWCB"].ToString() + "</td><td>" + dt.Rows[i]["LJCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td><td>" + dt.Rows[i]["ZJXB"].ToString() + "</td><td>" + dt.Rows[i]["YFHSL"].ToString() + "</td><td>" + dt.Rows[i]["DFSL"].ToString() + "</td></tr>");

                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td></td><td></td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + SJJ + "</td><td>" + YSubTotal + "</td><td>" + DWCB + "</td><td>" + LJCB + "</td><td>" + YMLR + "</td><td>" + ZJXB + "</td><td>" + YFHSL + "</td><td>" + DFHSL + "</td></tr></table>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }


        public FileResult HighVoltageCompartmentToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetHighVoltageCompartmentTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double YCB = 0;
                double YMLR = 0;
                double SJJ = 0;
                double DWCB = 0;
                double LJCB = 0;
                double ZJXB = 0;
                double YFHSL = 0;
                double DFHSL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                    if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                        SJJ += 0;
                    else
                        SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                    if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                    if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                        DWCB += 0;
                    else
                        DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                    //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                    //    YCB += 0;
                    //else
                    //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                    if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                        LJCB += 0;
                    else
                        LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                    if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                        ZJXB += 0;
                    else
                        ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                    //YFHSL
                    if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                        YFHSL += 0;
                    else
                        YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                    if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                        DFHSL += 0;
                    else
                        DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                //  row["ISF"] = 0;
                row["YSpecsModels"] = "合计";
                row["OrderUnit"] = "";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["SL"] = YAount;
                row["SJJ"] = SJJ;
                row["HTE"] = YSubTotal;
                row["DWCB"] = DWCB;
                row["LJCB"] = LJCB;
                row["YMLR"] = YMLR;
                row["ZJXB"] = ZJXB;
                row["YFHSL"] = YFHSL;
                row["DFSL"] = DFHSL;
                //  row[]
                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                //销售数量	合同额	数量	销售均价	合同额	单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量

                string strTols = "销售数量@5000,合同额@5000,数量(台)@5000,销售均价@5000,合同额@5000,单位成本@5000,累计直接成本,毛利润@5000,直接成本占销售价比例@5000,已发货数量@5000,代发货数量@5000,生产中数量@5000";//
                System.IO.MemoryStream stream = ExcelHelper.HighVoltageCompartmentToExcel(dt, "高压箱销售情况汇总表", start, strTols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "高压箱销售情况汇总表.xls");
            }
            else
                return null;

        }

        //切断阀销售情况汇总表
        public ActionResult CutOffSalesSummary()
        {
            return View();
        }

        public ActionResult GetCutOffSalesSummaryTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetCutOffSalesSummaryTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    // double YCB = 0;
                    double YMLR = 0;
                    double SJJ = 0;
                    double CBSubTotal = 0;
                    double CCSubTotal = 0;
                    double LJCB = 0;
                    double ZJXB = 0;
                    double YFHSL = 0;
                    double DFHSL = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td  rowspan='2'>规格型号</td><td  rowspan='2'>单位</td><td colspan='2'>月份</td><td colspan='7'>年度累计</td><td colspan='3'>状态</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>销售均价</td><td>合同额</td><td>更换线圈后直接成本</td><td>C-C型直接成本</td><td>累计直接成本</td><td>毛利润</td><td>直接成本占销售价比例</td><td>已发货数量</td><td>待发货数量</td></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                        if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                            SJJ += 0;
                        else
                            SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                        if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                        if (dt.Rows[i]["CBSubTotal"].ToString() == "" || dt.Rows[i]["CBSubTotal"] == null)
                            CBSubTotal += 0;
                        else
                            CBSubTotal += Convert.ToDouble(dt.Rows[i]["CBSubTotal"]);
                        if (dt.Rows[i]["CSubTotal"].ToString() == "" || dt.Rows[i]["CSubTotal"] == null)
                            CCSubTotal += 0;
                        else
                            CCSubTotal += Convert.ToDouble(dt.Rows[i]["CSubTotal"]);
                        if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                            LJCB += 0;
                        else
                            LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                            ZJXB += 0;
                        else
                            ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                        //YFHSL
                        if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                            YFHSL += 0;
                        else
                            YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                        if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                            DFHSL += 0;
                        else
                            DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["YSpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["SL"].ToString() + "</td><td>" + dt.Rows[i]["SJJ"].ToString() + "</td><td>" + dt.Rows[i]["HTE"].ToString() + "</td><td>" + dt.Rows[i]["CBSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["CSubTotal"] + "</td><td>" + dt.Rows[i]["LJCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td><td>" + dt.Rows[i]["ZJXB"].ToString() + "</td><td>" + dt.Rows[i]["YFHSL"].ToString() + "</td><td>" + dt.Rows[i]["DFSL"].ToString() + "</td></tr>");

                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td></td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + SJJ + "</td><td>" + YSubTotal + "</td><td>" + CBSubTotal + "</td><td>" + CCSubTotal + "</td><td>" + LJCB + "</td><td>" + YMLR + "</td><td>" + ZJXB + "</td><td>" + YFHSL + "</td><td>" + DFHSL + "</td></tr></table>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult CutOffSalesSummaryToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetCutOffSalesSummaryTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double CCSubTotal = 0;
                double YMLR = 0;
                double SJJ = 0;
                double CBSubTotal = 0;
                double LJCB = 0;
                double ZJXB = 0;
                double YFHSL = 0;
                double DFHSL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                    if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                        SJJ += 0;
                    else
                        SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                    if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                    if (dt.Rows[i]["CBSubTotal"].ToString() == "" || dt.Rows[i]["CBSubTotal"] == null)
                        CBSubTotal += 0;
                    else
                        CBSubTotal += Convert.ToDouble(dt.Rows[i]["CBSubTotal"]);
                    if (dt.Rows[i]["CSubTotal"].ToString() == "" || dt.Rows[i]["CSubTotal"] == null)
                        CCSubTotal += 0;
                    else
                        CCSubTotal += Convert.ToDouble(dt.Rows[i]["CSubTotal"]);
                    if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                        LJCB += 0;
                    else
                        LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                    if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                        ZJXB += 0;
                    else
                        ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                    //YFHSL
                    if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                        YFHSL += 0;
                    else
                        YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                    if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                        DFHSL += 0;
                    else
                        DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                //row["ISF"] = 0;
                row["YSpecsModels"] = "合计";
                row["OrderUnit"] = "";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["SL"] = YAount;
                row["SJJ"] = SJJ;
                row["HTE"] = YSubTotal;
                row["CBSubTotal"] = CBSubTotal;
                row["CSubTotal"] = CCSubTotal;
                row["LJCB"] = LJCB;
                row["YMLR"] = YMLR;
                row["ZJXB"] = ZJXB;
                row["YFHSL"] = YFHSL;
                row["DFSL"] = DFHSL;
                //  row[]
                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                //销售数量	合同额	数量	销售均价	合同额	单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量

                string strTols = "销售数量@5000,合同额@5000,数量(台)@5000,销售均价@5000,合同额@5000,单位成本@5000,累计直接成本,毛利润@5000,直接成本占销售价比例@5000,已发货数量@5000,代发货数量@5000,生产中数量@5000";//
                System.IO.MemoryStream stream = ExcelHelper.CutOffSalesSummaryToExcel(dt, "切断阀销售情况汇总表", start, strTols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "切断阀销售情况汇总表.xls");
            }
            else
                return null;
        }
        //调压器销售情况汇总表
        public ActionResult RegulatorSummary()
        {
            return View();
        }

        public ActionResult RegulatorSummaryTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetRegulatorSummaryTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    // double YCB = 0;
                    double YMLR = 0;
                    double SJJ = 0;
                    double DWCB = 0;
                    double LJCB = 0;
                    double ZJXB = 0;
                    double YFHSL = 0;
                    double DFHSL = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td  rowspan='2'>规格型号</td><td  rowspan='2'>单位</td><td colspan='2'>月份</td><td colspan='7'>年度累计</td><td colspan='3'>状态</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>销售均价</td><td>合同额</td><td>单位成本</td><td>累计直接成本</td><td>毛利润</td><td>直接成本占销售价比例</td><td>已发货数量</td><td>待发货数量</td></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                        if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                            SJJ += 0;
                        else
                            SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                        if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                        if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                            DWCB += 0;
                        else
                            DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                        //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                        //    YCB += 0;
                        //else
                        //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                        if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                            LJCB += 0;
                        else
                            LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                            ZJXB += 0;
                        else
                            ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                        //YFHSL
                        if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                            YFHSL += 0;
                        else
                            YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                        if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                            DFHSL += 0;
                        else
                            DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["YSpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["SL"].ToString() + "</td><td>" + dt.Rows[i]["SJJ"].ToString() + "</td><td>" + dt.Rows[i]["HTE"].ToString() + "</td><td>" + dt.Rows[i]["DWCB"].ToString() + "</td><td>" + dt.Rows[i]["LJCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td><td>" + dt.Rows[i]["ZJXB"].ToString() + "</td><td>" + dt.Rows[i]["YFHSL"].ToString() + "</td><td>" + dt.Rows[i]["DFSL"].ToString() + "</td></tr>");

                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td></td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + SJJ + "</td><td>" + YSubTotal + "</td><td>" + DWCB + "</td><td>" + LJCB + "</td><td>" + YMLR + "</td><td>" + ZJXB + "</td><td>" + YFHSL + "</td><td>" + DFHSL + "</td></tr></table>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult RegulatorSummaryTableToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetRegulatorSummaryTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double YCB = 0;
                double YMLR = 0;
                double SJJ = 0;
                double DWCB = 0;
                double LJCB = 0;
                double ZJXB = 0;
                double YFHSL = 0;
                double DFHSL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                    if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                        SJJ += 0;
                    else
                        SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                    if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                    if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                        DWCB += 0;
                    else
                        DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                    //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                    //    YCB += 0;
                    //else
                    //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                    if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                        LJCB += 0;
                    else
                        LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                    if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                        ZJXB += 0;
                    else
                        ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                    //YFHSL
                    if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                        YFHSL += 0;
                    else
                        YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                    if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                        DFHSL += 0;
                    else
                        DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                //row["ISF"] = 0;
                row["YSpecsModels"] = "合计";
                row["OrderUnit"] = "";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["SL"] = YAount;
                row["SJJ"] = SJJ;
                row["HTE"] = YSubTotal;
                row["DWCB"] = DWCB;
                row["LJCB"] = LJCB;
                row["YMLR"] = YMLR;
                row["ZJXB"] = ZJXB;
                row["YFHSL"] = YFHSL;
                row["DFSL"] = DFHSL;
                //  row[]
                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                //销售数量	合同额	数量	销售均价	合同额	单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量

                string strTols = "销售数量@5000,合同额@5000,数量(台)@5000,销售均价@5000,合同额@5000,单位成本@5000,累计直接成本,毛利润@5000,直接成本占销售价比例@5000,已发货数量@5000,代发货数量@5000,生产中数量@5000";//
                System.IO.MemoryStream stream = ExcelHelper.PressureRegulatingBoxToExcel(dt, "调压器销售情况汇总表", start, strTols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "调压器销售情况汇总表.xls");
            }
            else
                return null;

        }
        //其他设备销售情况汇总表
        public ActionResult OtherEquipment()
        {
            return View();
        }
        public ActionResult OtherEquipmentTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            if (ModelState.IsValid)
            {
                string where = "";

                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                if (start != "" && start != null)
                    where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                if (where != "")
                    dt = SalesManage.GetOtherEquipmentTable(start, end);
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {

                    double MSubTotal = 0;
                    double MAount = 0;
                    double YAount = 0;
                    double YSubTotal = 0;
                    // double YCB = 0;
                    double YMLR = 0;
                    double SJJ = 0;
                    double DWCB = 0;
                    double LJCB = 0;
                    double ZJXB = 0;
                    double YFHSL = 0;
                    double DFHSL = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:10%\" rowspan='2'>序号</td><td  rowspan='2'>规格型号</td><td  rowspan='2'>单位</td><td colspan='2'>月份</td><td colspan='7'>年度累计</td><td colspan='3'>状态</td></tr>");
                    sb.Append("<tr><td>数量</td><td>合同额</td><td>数量(台)</td><td>销售均价</td><td>合同额</td><td>单位成本</td><td>累计直接成本</td><td>毛利润</td><td>直接成本占销售价比例</td><td>已发货数量</td><td>待发货数量</td></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["MSubTotal"].ToString() == "")
                            MSubTotal += 0;
                        else
                            MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                        if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                            MAount += 0;
                        else
                            MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                        if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                            YAount += 0;
                        else
                            YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                        if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                            SJJ += 0;
                        else
                            SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                        if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                            YSubTotal += 0;
                        else
                            YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                        if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                            DWCB += 0;
                        else
                            DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                        //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                        //    YCB += 0;
                        //else
                        //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                        if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                            LJCB += 0;
                        else
                            LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                        if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                            YMLR += 0;
                        else
                            YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                        if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                            ZJXB += 0;
                        else
                            ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                        //YFHSL
                        if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                            YFHSL += 0;
                        else
                            YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                        if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                            DFHSL += 0;
                        else
                            DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["RowNumber"] + "</td><td>" + dt.Rows[i]["YSpecsModels"].ToString() + "</td><td>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td>" + dt.Rows[i]["MAount"].ToString() + "</td><td>" + dt.Rows[i]["MSubTotal"].ToString() + "</td><td>" + dt.Rows[i]["SL"].ToString() + "</td><td>" + dt.Rows[i]["SJJ"].ToString() + "</td><td>" + dt.Rows[i]["HTE"].ToString() + "</td><td>" + dt.Rows[i]["DWCB"].ToString() + "</td><td>" + dt.Rows[i]["LJCB"].ToString() + "</td><td>" + dt.Rows[i]["YMLR"].ToString() + "</td><td>" + dt.Rows[i]["ZJXB"].ToString() + "</td><td>" + dt.Rows[i]["YFHSL"].ToString() + "</td><td>" + dt.Rows[i]["DFSL"].ToString() + "</td></tr>");

                    }
                    //  SubTotal = SubTotal / 10000;
                    // HK = HK / 10000;
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>" + (dt.Rows.Count + 1) + "</td><td>合计</td><td></td><td>" + MAount + "</td><td>" + MSubTotal + "</td><td>" + YAount + "</td><td>" + SJJ + "</td><td>" + YSubTotal + "</td><td>" + DWCB + "</td><td>" + LJCB + "</td><td>" + YMLR + "</td><td>" + ZJXB + "</td><td>" + YFHSL + "</td><td>" + DFHSL + "</td></tr></table>");
                    string countStr = SalesManage.GetSalesSummary(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if (arr.Length > 0)
                    {
                        //   sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共发生采购订单 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 起, 累计订购金额 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 元";
                    }
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
                // return Json(new { success = "false" });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult OtherEquipmentTableToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            string where = "";
            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            if (Pname != "" && Pname != null)
                where += " and b.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " where a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
            DataTable dt = new DataTable();
            if (where != "")
                dt = SalesManage.GetOtherEquipmentTable(start, end);
            if (dt != null)
            {
                double MSubTotal = 0;
                double MAount = 0;
                double YAount = 0;
                double YSubTotal = 0;
                double YCB = 0;
                double YMLR = 0;
                double SJJ = 0;
                double DWCB = 0;
                double LJCB = 0;
                double ZJXB = 0;
                double YFHSL = 0;
                double DFHSL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MSubTotal"].ToString() == "")
                        MSubTotal += 0;
                    else
                        MSubTotal += Convert.ToDouble(dt.Rows[i]["MSubTotal"]);
                    if (dt.Rows[i]["MAount"].ToString() == "" || dt.Rows[i]["MAount"] == null)
                        MAount += 0;
                    else
                        MAount += Convert.ToDouble(dt.Rows[i]["MAount"]);
                    if (dt.Rows[i]["SL"].ToString() == "" || dt.Rows[i]["SL"] == null)
                        YAount += 0;
                    else
                        YAount += Convert.ToDouble(dt.Rows[i]["SL"]);
                    if (dt.Rows[i]["SJJ"].ToString() == "" || dt.Rows[i]["SJJ"] == null)
                        SJJ += 0;
                    else
                        SJJ += Convert.ToDouble(dt.Rows[i]["SJJ"]);
                    if (dt.Rows[i]["HTE"].ToString() == "" || dt.Rows[i]["HTE"] == null)
                        YSubTotal += 0;
                    else
                        YSubTotal += Convert.ToDouble(dt.Rows[i]["HTE"]);
                    if (dt.Rows[i]["DWCB"].ToString() == "" || dt.Rows[i]["DWCB"] == null)
                        DWCB += 0;
                    else
                        DWCB += Convert.ToDouble(dt.Rows[i]["DWCB"]);
                    //if (dt.Rows[i]["YCB"].ToString() == "" || dt.Rows[i]["YCB"] == null)
                    //    YCB += 0;
                    //else
                    //    YCB += Convert.ToDouble(dt.Rows[i]["YCB"]);
                    if (dt.Rows[i]["LJCB"].ToString() == "" || dt.Rows[i]["LJCB"] == null)
                        LJCB += 0;
                    else
                        LJCB += Convert.ToDouble(dt.Rows[i]["LJCB"]);
                    if (dt.Rows[i]["YMLR"].ToString() == "" || dt.Rows[i]["YMLR"] == null)
                        YMLR += 0;
                    else
                        YMLR += Convert.ToDouble(dt.Rows[i]["YMLR"]);
                    if (dt.Rows[i]["ZJXB"].ToString() == "" || dt.Rows[i]["ZJXB"] == null)
                        ZJXB += 0;
                    else
                        ZJXB += Convert.ToDouble(dt.Rows[i]["ZJXB"]);
                    //YFHSL
                    if (dt.Rows[i]["YFHSL"].ToString() == "" || dt.Rows[i]["YFHSL"] == null)
                        YFHSL += 0;
                    else
                        YFHSL += Convert.ToDouble(dt.Rows[i]["YFHSL"]);
                    if (dt.Rows[i]["DFSL"].ToString() == "" || dt.Rows[i]["DFSL"] == null)
                        DFHSL += 0;
                    else
                        DFHSL += Convert.ToDouble(dt.Rows[i]["DFSL"]);

                }
                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                int s = dt.Rows.Count + 1;
                row["RowNumber"] = s.ToString();
                //  row["ISF"] = 0;
                row["YSpecsModels"] = "合计";
                row["OrderUnit"] = "";
                row["MAount"] = MAount;
                row["MSubTotal"] = MSubTotal;
                row["SL"] = YAount;
                row["SJJ"] = SJJ;
                row["HTE"] = YSubTotal;
                row["DWCB"] = DWCB;
                row["LJCB"] = LJCB;
                row["YMLR"] = YMLR;
                row["ZJXB"] = ZJXB;
                row["YFHSL"] = YFHSL;
                row["DFSL"] = DFHSL;
                //  row[]
                dt.Rows.Add(row);
                //string strCols = "序号@5000,自有产品分类@6000," + start + "至" + end + "月@5000,年度累计@5000";//,数量-5000,合同额-5000,数量(台)-5000,合同额-5000,成本-5000,毛利润-5000
                //销售数量	合同额	数量	销售均价	合同额	单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量

                string strTols = "销售数量@5000,合同额@5000,数量(台)@5000,销售均价@5000,合同额@5000,单位成本@5000,累计直接成本,毛利润@5000,直接成本占销售价比例@5000,已发货数量@5000,代发货数量@5000,生产中数量@5000";//
                System.IO.MemoryStream stream = ExcelHelper.OtherEquipmentTableToExcel(dt, "其他设备销售情况汇总表", start, strTols.Split(','), ProjectSearch.StartDate, ProjectSearch.EndDate);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "其他设备销售情况汇总表.xls");
            }
            else
                return null;
        }
        #endregion

        #region [经营分析]
        //合同总经营分析
        public ActionResult ContractStatisticalAnalysis()
        {
            return View();

        }
        public ActionResult GetContractStatisticalAnalysisTable()
        {

            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];

            string datetime = DateTime.Now.ToString();
            string lastmonthdatetime = DateTime.Now.AddMonths(-1).ToString();
            string lastyeardatetime = DateTime.Now.AddYears(-1).ToString();

            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();
            #region MyRegion
            //string laststarttime = "";
            //string lastendtime = "";
            //string  lastyearstarttime = "";
            //string lastyearendtime = "";
            //if (ProjectSearch.StartDate != null && ProjectSearch.StartDate != "") 
            //{
            //    DateTime LastStartTime = Convert.ToDateTime(ProjectSearch.StartDate);
            //    DateTime LastYearStartTime = Convert.ToDateTime(ProjectSearch.StartDate);
            //    LastStartTime = LastStartTime.AddMonths(-1);
            //    LastYearStartTime = LastYearStartTime.AddYears(-1);
            //    laststarttime = LastStartTime.ToString();
            //    lastyearstarttime = LastYearStartTime.ToString();
            //}
            //if (ProjectSearch.EndDate != null && ProjectSearch.EndDate != "")
            //{
            //    DateTime LastEndTime = Convert.ToDateTime(ProjectSearch.EndDate);
            //    DateTime LastYearEndTime = Convert.ToDateTime(ProjectSearch.EndDate);
            //    LastEndTime = LastEndTime.AddMonths(-1);
            //    LastYearEndTime = LastYearEndTime.AddYears(-1);
            //    lastendtime = LastEndTime.ToString();
            //    lastyearendtime = LastYearEndTime.ToString();
            //} 
            #endregion
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "20";
                string where = "";
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;

                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.CreateTime >= '" + start + "' and b.CreateTime <= '" + end + "'";
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                // if (where != "")
                dt = SalesManage.GetContractStatisticalAnalysisTable(datetime, lastmonthdatetime, lastyeardatetime);

                dt2 = SalesManage.GetContractNowStatisticalAnalysisTable(startdatetime, enddatetime, laststartdatetime, lastenddatetime);
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        double Etotal = 0;
                        double Ftotal = 0;
                        double BYHBTotal = 0;
                        double GTotal = 0;
                        double YQNTQTotal = 0;

                        sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                        sb.Append("<tr><td colspan='10'>本月合同总额统计表（单位：万元）</td></tr>");
                        sb.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">当月</td><td rowspan='2' style=\"width:10%\">上月</td><td colspan='2' style=\"width:5%\">环比</td><td rowspan='2' style=\"width:5%\">去年同期</td><td colspan='2' style=\"width:5%\">同比</td>" +
            "<td rowspan='2' style=\"width:5%\">占全年累计合同额%</td><td rowspan='2'  style=\"width:10%\">备注</td></tr>");
                        sb.Append("<tr><td>变动</td><td>变动%</td><td>变动</td><td>变动%</td></tr>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Etotal"].ToString() == "")
                                Etotal += 0;
                            else
                                Etotal += Convert.ToDouble(dt.Rows[i]["Etotal"]);
                            if (dt.Rows[i]["Ftotal"].ToString() == "")
                                Ftotal += 0;
                            else
                                Ftotal += Convert.ToDouble(dt.Rows[i]["Ftotal"]);
                            if (dt.Rows[i]["BYHBTotal"].ToString() == "")
                                BYHBTotal += 0;
                            else
                                BYHBTotal += Convert.ToDouble(dt.Rows[i]["BYHBTotal"]);
                            if (dt.Rows[i]["GTotal"].ToString() == "")
                                GTotal += 0;
                            else
                                GTotal += Convert.ToDouble(dt.Rows[i]["GTotal"]);

                            if (dt.Rows[i]["YQNTQTotal"].ToString() == "")
                                YQNTQTotal += 0;
                            else
                                YQNTQTotal += Convert.ToDouble(dt.Rows[i]["YQNTQTotal"]);
                            sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["EDeptName"].ToString() + "</td><td>" + dt.Rows[i]["Etotal"].ToString() + "</td><td>" + dt.Rows[i]["Ftotal"].ToString() + "</td><td>" + dt.Rows[i]["BYHBTotal"].ToString() + "</td><td>" + dt.Rows[i]["BYHB"].ToString() + "%</td><td>" + dt.Rows[i]["GTotal"].ToString() + "</td><td>" + dt.Rows[i]["YQNTQTotal"].ToString() + "</td><td>" + dt.Rows[i]["YQNTQ"].ToString() + "</td><td>" + dt.Rows[i]["HTEB"].ToString() + "%</td><td></td></tr>");
                        }
                        sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td>" + Etotal + "</td><td>" + Ftotal + "</td><td>" + BYHBTotal + "</td><td></td><td>" + GTotal + "</td><td>" + YQNTQTotal + "</td><td></td><td></td><td></td></tr>");
                        string countStr = SalesManage.GetOrderStatistical(where);
                        string[] arr = countStr.Split('@');
                        string sign = "";
                        if (arr.Length > 0)
                        {
                        }
                    }


                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            double Etotal2 = 0;
                            double Ftotal2 = 0;
                            double BYHBTotal2 = 0;
                            sb2.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                            sb2.Append("<tr><td colspan='10'>" + DateTime.Now.AddMonths(-1).Year.ToString() + "/1" + "-" + DateTime.Now.AddMonths(-1).Month.ToString() + "月累计合同总额统计表（单位：万元）</td></tr>");
                            sb2.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">" + DateTime.Now.AddMonths(-1).Year.ToString() + "/1" + "-" + DateTime.Now.AddMonths(-1).Month.ToString() + "月</td><td rowspan='2' style=\"width:5%\">去年同期</td><td colspan='2'  style=\"width:10%\">同比</td><td rowspan='2'  style=\"width:10%\">备注</td></tr>");
                            sb2.Append("<tr><td>变动</td><td>变动%</td></tr>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["Etotal"].ToString() == "")
                                    Etotal2 += 0;
                                else
                                    Etotal2 += Convert.ToDouble(dt.Rows[i]["Etotal"]);
                                if (dt.Rows[i]["Ftotal"].ToString() == "")
                                    Ftotal2 += 0;
                                else
                                    Ftotal2 += Convert.ToDouble(dt.Rows[i]["Ftotal"]);
                                if (dt.Rows[i]["BYHBTotal"].ToString() == "")
                                    BYHBTotal2 += 0;
                                else
                                    BYHBTotal2 += Convert.ToDouble(dt.Rows[i]["BYHBTotal"]);
                                //if (dt.Rows[i]["GTotal"].ToString() == "")
                                //    GTotal += 0;
                                //else
                                //    GTotal += Convert.ToInt32(dt.Rows[i]["GTotal"]);

                                //if (dt.Rows[i]["YQNTQTotal"].ToString() == "")
                                //    YQNTQTotal += 0;
                                //else
                                //    YQNTQTotal += Convert.ToInt32(dt.Rows[i]["YQNTQTotal"]);
                                sb2.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["EDeptName"].ToString() + "</td><td>" + dt.Rows[i]["Etotal"].ToString() + "</td><td>" + dt.Rows[i]["Ftotal"].ToString() + "</td><td>" + dt.Rows[i]["BYHBTotal"].ToString() + "</td><td>" + dt.Rows[i]["BYHB"].ToString() + "%</td><td></td></tr>");
                            }
                            sb2.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td>" + Etotal2 + "</td><td>" + Ftotal2 + "</td><td>" + BYHBTotal2 + "</td><td></td><td></td></tr>");
                        }

                    }

                    return Json(new { success = "true", strSb = sb.ToString(), BotomTab = sb2.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public FileResult ContractStatisticalAnalysisToExcel()
        {

            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();

            string datetime = DateTime.Now.ToString();
            string lastmonthdatetime = DateTime.Now.AddMonths(-1).ToString();
            string lastyeardatetime = DateTime.Now.AddYears(-1).ToString();

            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();


            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = SalesManage.GetContractStatisticalAnalysisTable(datetime, lastmonthdatetime, lastyeardatetime);
            dt2 = SalesManage.GetContractNowStatisticalAnalysisTable(startdatetime, enddatetime, laststartdatetime, lastenddatetime);
            if (dt != null)
            {
                double Etotal = 0;
                double Ftotal = 0;
                double BYHBTotal = 0;
                double GTotal = 0;
                double YQNTQTotal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Etotal"].ToString() == "")
                        Etotal += 0;
                    else
                        Etotal += Convert.ToInt32(dt.Rows[i]["Etotal"]);
                    if (dt.Rows[i]["Ftotal"].ToString() == "")
                        Ftotal += 0;
                    else
                        Ftotal += Convert.ToInt32(dt.Rows[i]["Ftotal"]);
                    if (dt.Rows[i]["BYHBTotal"].ToString() == "")
                        BYHBTotal += 0;
                    else
                        BYHBTotal += Convert.ToInt32(dt.Rows[i]["BYHBTotal"]);
                    if (dt.Rows[i]["GTotal"].ToString() == "")
                        GTotal += 0;
                    else
                        GTotal += Convert.ToInt32(dt.Rows[i]["GTotal"]);

                    if (dt.Rows[i]["YQNTQTotal"].ToString() == "")
                        YQNTQTotal += 0;
                    else
                        YQNTQTotal += Convert.ToInt32(dt.Rows[i]["YQNTQTotal"]);
                }
                if (dt2 != null)
                {
                    double Etotal2 = 0;
                    double Ftotal2 = 0;
                    double BYHBTotal2 = 0;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["Etotal"].ToString() == "")
                            Etotal2 += 0;
                        else
                            Etotal2 += Convert.ToInt32(dt2.Rows[i]["Etotal"]);
                        if (dt2.Rows[i]["Ftotal"].ToString() == "")
                            Ftotal2 += 0;
                        else
                            Ftotal2 += Convert.ToInt32(dt2.Rows[i]["Ftotal"]);
                        if (dt2.Rows[i]["BYHBTotal"].ToString() == "")
                            BYHBTotal2 += 0;
                        else
                            BYHBTotal2 += Convert.ToInt32(dt2.Rows[i]["BYHBTotal"]);
                        //if (dt.Rows[i]["GTotal"].ToString() == "")
                        //    GTotal2 += 0;
                        //else
                        //    GTotal2 += Convert.ToInt32(dt.Rows[i]["GTotal"]);

                        //if (dt.Rows[i]["YQNTQTotal"].ToString() == "")
                        //    YQNTQTotal2 += 0;
                        //else
                        //    YQNTQTotal += Convert.ToInt32(dt.Rows[i]["YQNTQTotal"]);
                    }
                    DataRow row2 = dt2.NewRow();
                    int s = dt.Rows.Count + 1;
                    // row["RowNumber"] = s.ToString();
                    // row["EUnitID"] = "合计";
                    row2["EDeptName"] = "合计";
                    row2["Etotal"] = Etotal2;
                    row2["Ftotal"] = Ftotal2;
                    row2["BYHBTotal"] = BYHBTotal2;
                    row2["BYHB"] = 0.0;
                    dt2.Rows.Add(row2);

                }

                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                DataRow row = dt.NewRow();
                //int s = dt.Rows.Count + 1;
                // row["RowNumber"] = s.ToString();
                // row["EUnitID"] = "合计";
                row["EDeptName"] = "合计";
                row["Etotal"] = Etotal;
                row["Ftotal"] = Ftotal;
                row["BYHBTotal"] = BYHBTotal;
                row["BYHB"] = 0.0;

                row["GTotal"] = GTotal;
                row["YQNTQTotal"] = YQNTQTotal;
                row["YQNTQ"] = 0.0;
                row["HTEB"] = 0;
                dt.Rows.Add(row);
                string strCols = "序号-5000,姓名-5000,合同额-5000,已收款-5000,欠款-5000,回款率-5000";
                System.IO.MemoryStream stream = ExcelHelper.ContractStatisticalAnalysisToExcel(dt, dt2, "合同额经营分析", datetime);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "合同额经营分析.xls");
            }
            else
                return null;


        }

        /// <summary>
        /// 应收款经营分析
        /// </summary>
        /// <returns></returns>

        public ActionResult ReceivableAccount()
        {
            return View();

        }

        public ActionResult GetReceivableAccountTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];

            //  string datetime = DateTime.Now.ToString();
            // string lastmonthdatetime = DateTime.Now.AddMonths(-1).ToString();
            //  string lastyeardatetime = DateTime.Now.AddYears(-1).ToString();

            // string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                // if (where != "")

                enddatetime = "2016-1-1";
                lastenddatetime = "2015-1-1";
                dt = SalesManage.GetReceivableAccountTable(enddatetime, lastenddatetime);

                dt2 = SalesManage.GetDeceiveReceivableAccountTable(enddatetime);

                dt3 = SalesManage.getLJYSKReceivableTable(enddatetime);
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        double Etotal = 0;
                        double Rtotal = 0;
                        double YSFK = 0;
                        double GTotal = 0;
                        double YQNTQTotal = 0;

                        sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                        sb.Append("<tr><td colspan='10'>本月应收账款情况（单位：万元）（单位：万元）</td></tr>");
                        sb.Append("<tr  class=\"left\"><td style=\"width:5%\">部门</td><td  style=\"width:5%\">合同额</td><td  style=\"width:10%\">已收款（本月）</td><td  style=\"width:5%\">总回款率</td><td  style=\"width:5%\">本月应收帐款额（无填0）</td><td  style=\"width:5%\">同比增减%</td>" +
            "<td style=\"width:5%\">占本月公司应收账款总额%</td><td  style=\"width:10%\">备注</td></tr>");
                        // sb.Append("<tr><td>变动</td><td>变动%</td><td>变动</td><td>变动%</td></tr>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Etotal"].ToString() == "")
                                Etotal += 0;
                            else
                                Etotal += Convert.ToDouble(dt.Rows[i]["Etotal"]);
                            if (dt.Rows[i]["Rtotal"].ToString() == "")
                                Rtotal += 0;
                            else
                                Rtotal += Convert.ToDouble(dt.Rows[i]["Rtotal"]);
                            if (dt.Rows[i]["YSFK"].ToString() == "")
                                YSFK += 0;
                            else
                                YSFK += Convert.ToDouble(dt.Rows[i]["YSFK"]);

                            sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["EDeptName"].ToString() + "</td><td>" + dt.Rows[i]["Etotal"].ToString() + "</td><td>" + dt.Rows[i]["Rtotal"].ToString() + "</td><td>" + dt.Rows[i]["HKL"].ToString() + "</td><td>" + dt.Rows[i]["YSFK"].ToString() + "%</td><td>" + dt.Rows[i]["BYZEB"].ToString() + "</td><td>" + dt.Rows[i]["LastTotalTB"].ToString() + "</td><td></td></tr>");
                        }
                        sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td>" + Etotal + "</td><td>" + Rtotal + "</td><td>" + YSFK + "</td><td></td><td></td><td></td><td></td></tr>");

                    }


                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            double QDFTotal = 0;
                            // double Ftotal2 = 0;
                            // double BYHBTotal2 = 0;
                            sb2.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                            sb2.Append("<tr><td colspan='10'>本月应收账款结构（单位：万元）</td></tr>");
                            sb2.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">项目产品</td><td rowspan='2' style=\"width:5%\">客户名称</td><td rowspan='2' style=\"width:10%\">应收款额</td><td rowspan='2'  style=\"width:10%\">形成原因</td><td style=\"width:10%\">收回风险</td><td rowspan='2'>占当月应收账款总额%</td><td rowspan='2' >其他需要说明的内容</td></tr>");
                            // sb2.Append("<tr><td rowspan='2' >占当月应收账款总额%</td><td>其他需要说明的内容</td></tr>");
                            sb2.Append("<tr><td>何时收回</td></tr>");
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                if (dt2.Rows[i]["qdftotal"].ToString() == "")
                                    QDFTotal += 0;
                                else
                                    QDFTotal += Convert.ToDouble(dt2.Rows[i]["qdftotal"]);
                                sb2.Append("<tr class=\"staleft\"><td>" + dt2.Rows[i]["deptname"].ToString() + "</td><td>" + dt2.Rows[i]["text"] + "</td><td>" + dt2.Rows[i]["OrderUnit"] + "</td><td>" + dt2.Rows[i]["qdftotal"].ToString() + "</td><td></td><td>" + enddatetime + "</td><td>" + dt2.Rows[i]["qdfamount"] + "</td><td></td></tr>");
                            }
                            sb2.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + QDFTotal + "</td><td></td><td></td><td></td><td></td></tr>");
                        }

                    }

                    if (dt3 != null)
                    {
                        if (dt3.Rows.Count > 0)
                        {
                            double QDFTotal = 0;
                            // double Ftotal2 = 0;
                            // double BYHBTotal2 = 0;
                            sb3.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                            sb3.Append("<tr><td colspan='10'>累计应收账款结构（单位：万元）</td></tr>");
                            sb3.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">项目产品</td><td rowspan='2' style=\"width:5%\">客户名称</td><td rowspan='2' style=\"width:10%\">应收款额</td><td colspan='5'>账龄</td><td rowspan='2' >其他需要说明的内容</td></tr>");
                            //<td rowspan='2'  style=\"width:10%\">形成原因</td><td style=\"width:10%\">收回风险</td><td rowspan='2'>占当月应收账款总额%</td> sb2.Append("<tr><td rowspan='2' >占当月应收账款总额%</td><td>其他需要说明的内容</td></tr>");
                            sb3.Append("<tr><td><3月</td><td>≥3月 <6月</td><td>≥6月 <1年</td><td>≥1年 <2年</td><td>≥2年</td></tr>");
                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {
                                if (dt3.Rows[i]["qdftotal"].ToString() == "")
                                    QDFTotal += 0;
                                else
                                    QDFTotal += Convert.ToDouble(dt3.Rows[i]["qdftotal"]);
                                sb3.Append("<tr class=\"staleft\"><td>" + dt3.Rows[i]["deptname"].ToString() + "</td><td>" + dt2.Rows[i]["Text"] + "</td><td>" + dt3.Rows[i]["OrderUnit"] + "</td><td>" + dt3.Rows[i]["qdftotal"].ToString() + "</td><td>" + dt3.Rows[i]["Less3"] + "</td><td>" + dt3.Rows[i]["Greater3Less6"] + "</td><td>" + dt3.Rows[i]["Greater6Less1"] + "</td><td>" + dt3.Rows[i]["Greater1Less2"] + "</td><td>" + dt3.Rows[i]["Greater2"] + "</td><td></td></tr>");
                            }
                            sb3.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + QDFTotal + "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                        }

                    }

                    return Json(new { success = "true", strSb = sb.ToString(), BotomTab = sb2.ToString(), LJTab = sb3.ToString() });

                }
                else
                {
                    return Json(new { success = "false" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public ActionResult GetDeceiveAccountTableToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();

            string datetime = DateTime.Now.ToString();
            string lastmonthdatetime = DateTime.Now.AddMonths(-1).ToString();
            string lastyeardatetime = DateTime.Now.AddYears(-1).ToString();

            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();


            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt = SalesManage.GetReceivableAccountTable(datetime, lastenddatetime);

            dt2 = SalesManage.GetDeceiveReceivableAccountTable(datetime);

            dt3 = SalesManage.getLJYSKReceivableTable(datetime);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    double Etotal = 0;
                    double Rtotal = 0;
                    double YSFK = 0;
                    double GTotal = 0;
                    double YQNTQTotal = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Etotal"].ToString() == "")
                            Etotal += 0;
                        else
                            Etotal += Convert.ToDouble(dt.Rows[i]["Etotal"]);
                        if (dt.Rows[i]["Rtotal"].ToString() == "")
                            Rtotal += 0;
                        else
                            Rtotal += Convert.ToDouble(dt.Rows[i]["Rtotal"]);
                        if (dt.Rows[i]["YSFK"].ToString() == "")
                            YSFK += 0;
                        else
                            YSFK += Convert.ToDouble(dt.Rows[i]["YSFK"]);
                    }
                    DataRow row = dt.NewRow();
                    //row["EUnitID"] = "";
                    row["EDeptName"] = "合计";
                    row["Etotal"] = Etotal;
                    row["Rtotal"] = Rtotal;
                    row["HKL"] = 0.0;
                    row["YSFK"] = YSFK;
                    row["BYZEB"] = 0.0;
                    row["LastTotalTB"] = GTotal;

                    dt.Rows.Add(row);
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double QDFTotal = 0;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["qdftotal"].ToString() == "")
                                QDFTotal += 0;
                            else
                                QDFTotal += Convert.ToDouble(dt2.Rows[i]["qdftotal"]);
                        }
                        DataRow row2 = dt2.NewRow();
                        //   int s = dt2.Rows.Count + 1;
                        // row["RowNumber"] = s.ToString();
                        // row2["Funitid"] = "合计";
                        row2["deptname"] = "合计";
                        // row2["typedesc"] = "";
                        row2["qdftotal"] = QDFTotal;
                        row2["qdfamount"] = 0;
                        // row2["BYHB"] = 0.0;
                        dt2.Rows.Add(row2);
                    }
                }
                if (dt3 != null) {
                if (dt3.Rows.Count > 0)
                {
                    double QDFTotal = 0;
                    // double Ftotal2 = 0;
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (dt3.Rows[i]["qdftotal"].ToString() == "")
                            QDFTotal += 0;
                        else
                            QDFTotal += Convert.ToDouble(dt3.Rows[i]["qdftotal"]);

                    }

                    DataRow row3 = dt3.NewRow();
                    row3["DeptName"] = "合计";
                    //row3["ptype"] = "";
                    row3["qdftotal"] = QDFTotal;
                    row3["typedesc"] = "";
                    row3["Less3"] = "";
                    row3["Greater3Less6"] = "";
                    row3["Greater6Less1"] = "";
                    row3["Greater1Less2"] = "";
                    row3["Greater2"] = "";
                    dt3.Rows.Add(row3);

                }
                }

                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                //DataRow row = dt.NewRow();
                //int s = dt.Rows.Count + 1;
                // row["RowNumber"] = s.ToString();
                // row["EUnitID"] = "合计";

                string strCols = "序号-5000,姓名-5000,合同额-5000,已收款-5000,欠款-5000,回款率-5000";
                System.IO.MemoryStream stream = ExcelHelper.DeceiveAccountTableToExcel(dt, dt2, dt3, "应收款累计经营分析", datetime);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "应收款累计经营分析.xls");
            }
            else
                return null;

        }


        public ActionResult OtherBusinessAnalysis()
        {
            return View();
        }


        /// <summary>
        /// 应收款经营分析二
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountsPayableStatisticalAnalysis2()
        {
            return View();
        }


        public ActionResult GetAccountsPayableStatisticalAnalysis2Table()
        {
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();
            DataTable dt = SalesManage.GetAccountsPayableStatisticalAnalysis2Table(enddatetime);
            //  DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            //dt = SalesManage.GetReceivableAccountTable(enddatetime, lastenddatetime);

            dt2 = SalesManage.GetAccountsPayableNotMonthTable(enddatetime);

            dt3 = SalesManage.getAccountsPayableYearsTable();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    double SubTotal = 0;
                    double YFK = 0;
                    double QK = 0;
                    double GTotal = 0;
                    double YQNTQTotal = 0;

                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr><td colspan='7'>本月应收账款情况（单位：万元）</td></tr>");
                    sb.Append("<tr  class=\"left\"><td style=\"width:5%\">部门</td><td  style=\"width:5%\">负责人</td><td  style=\"width:10%\">合同额</td><td  style=\"width:5%\">已付款</td><td  style=\"width:5%\">未付款（无填0）</td><td  style=\"width:5%\">占本月公司应付账款总额%</td>" +
        "<td  style=\"width:10%\">备注</td></tr>");
                    // sb.Append("<tr><td>变动</td><td>变动%</td><td>变动</td><td>变动%</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SubTotal"].ToString() == "")
                            SubTotal += 0;
                        else
                            SubTotal += Convert.ToDouble(dt.Rows[i]["SubTotal"]);
                        if (dt.Rows[i]["YFK"].ToString() == "")
                            YFK += 0;
                        else
                            YFK += Convert.ToDouble(dt.Rows[i]["YFK"]);
                        if (dt.Rows[i]["QK"].ToString() == "")
                            QK += 0;
                        else
                            QK += Convert.ToDouble(dt.Rows[i]["QK"]);

                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["FDeptName"].ToString() + "</td><td>" + dt.Rows[i]["CreateUser"] + "</td><td>" + dt.Rows[i]["SubTotal"].ToString() + "</td><td>" + dt.Rows[i]["YFK"].ToString() + "</td><td></td><td>" + dt.Rows[i]["BYZE"].ToString() + "</td><td></td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + SubTotal + "</td><td>" + YFK + "</td><td>" + QK + "</td><td></td><td></td></tr>");

                }


                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double SubTotal = 0;
                        // double Ftotal2 = 0;
                        // double BYHBTotal2 = 0;
                        sb2.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                        sb2.Append("<tr><td colspan='7'>本月应收账款结构（单位：万元）</td></tr>");
                        sb2.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">项目产品</td><td rowspan='2' style=\"width:5%\">负责人</td><td rowspan='2' style=\"width:10%\">应收款额</td><td rowspan='2'  style=\"width:10%\">账龄</td><td style=\"width:10%\">实际支付款额</td><td rowspan='2' >其他需要说明的内容</td></tr>");
                        //  sb2.Append("<tr><td rowspan='2' >%</td><td>其他需要说明的内容</td></tr>");
                        sb2.Append("<tr><td>哪年</td></tr>");
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["SubTotal"].ToString() == "")
                                SubTotal += 0;
                            else
                                SubTotal += Convert.ToDouble(dt2.Rows[i]["SubTotal"]);
                            sb2.Append("<tr class=\"staleft\"><td>" + dt2.Rows[i]["deptname"].ToString() + "</td><td>" + dt2.Rows[i]["text"] + "</td><td>" + dt2.Rows[i]["CreateUser"] + "</td><td>" + dt2.Rows[i]["SubTotal"].ToString() + "</td><td>" + dt2.Rows[i]["ContractDate"] + "</td><td>" + dt2.Rows[i]["RTotal"] + "</td><td></td></tr>");
                        }
                        sb2.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + SubTotal + "</td><td></td><td></td><td></td></tr>");
                    }

                }

                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        double YFTotal = 0;
                        // double Ftotal2 = 0;
                        // double BYHBTotal2 = 0;
                        sb3.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                        sb3.Append("<tr><td colspan='5'>累计应收账款结构（单位：万元）</td></tr>");
                        sb3.Append("<tr  class=\"left\"><td  style=\"width:5%\">部门</td><td  style=\"width:5%\">年费</td><td style=\"width:5%\">应付款额</td><td  style=\"width:10%\">占历年累计应付款总额%</td><td >备注</td></tr>");
                        //<td rowspan='2'  style=\"width:10%\">形成原因</td><td style=\"width:10%\">收回风险</td><td rowspan='2'>占当月应收账款总额%</td> sb2.Append("<tr><td rowspan='2' >占当月应收账款总额%</td><td>其他需要说明的内容</td></tr>");
                        // sb3.Append("<tr><td><3月</td><td>≥3月 <6月</td><td>≥6月 <1年</td><td>≥1年 <2年</td><td>≥2年</td></tr>");
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt3.Rows[i]["YFTotal"].ToString() == "")
                                YFTotal += 0;
                            else
                                YFTotal += Convert.ToDouble(dt3.Rows[i]["YFTotal"]);
                            sb3.Append("<tr class=\"staleft\"><td>" + dt3.Rows[i]["deptname"].ToString() + "</td><td>" + dt3.Rows[i]["DTime"] + "</td><td>" + dt3.Rows[i]["YFTotal"] + "</td><td>" + dt3.Rows[i]["Total"].ToString() + "</td><td></td></tr>");
                        }
                        sb3.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + YFTotal + "</td><td></td><td></td></tr>");
                    }

                }

                return Json(new { success = "true", strSb = sb.ToString(), BotomTab = sb2.ToString(), LJTab = sb3.ToString() });

            }
            else
            {
                return Json(new { success = "false" });
            }
        }


        public FileResult GetAccountsPayableStatisticalAnalysis2ToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();

            string datetime = DateTime.Now.ToString();
            string Enddatetime = DateTime.Now.AddMonths(-1).ToString();
            //string lastyeardatetime = DateTime.Now.AddYears(-1).ToString();

            //string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            //string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            //string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();


            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt = SalesManage.GetAccountsPayableStatisticalAnalysis2Table(Enddatetime);

            dt2 = SalesManage.GetAccountsPayableNotMonthTable(Enddatetime);

            dt3 = SalesManage.getAccountsPayableYearsTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    double SubTotal = 0;
                    double YFK = 0;
                    double QK = 0;
                    double GTotal = 0;
                    double YQNTQTotal = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SubTotal"].ToString() == "")
                            SubTotal += 0;
                        else
                            SubTotal += Convert.ToDouble(dt.Rows[i]["SubTotal"]);
                        if (dt.Rows[i]["YFK"].ToString() == "")
                            YFK += 0;
                        else
                            YFK += Convert.ToDouble(dt.Rows[i]["YFK"]);
                        if (dt.Rows[i]["QK"].ToString() == "")
                            QK += 0;
                        else
                            QK += Convert.ToDouble(dt.Rows[i]["QK"]);
                    }
                    DataRow row = dt.NewRow();
                    //row["EUnitID"] = "";
                    row["FDeptName"] = "合计";
                    row["CreateUser"] = "";
                    row["SubTotal"] = SubTotal;
                    row["YFK"] = YFK;
                    row["QK"] = QK;
                    row["BYZE"] = 0.0;

                    //  row["LastTotalTB"] = GTotal;

                    dt.Rows.Add(row);
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double SubTotal = 0;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["SubTotal"].ToString() == "")
                                SubTotal += 0;
                            else
                                SubTotal += Convert.ToDouble(dt2.Rows[i]["SubTotal"]);
                        }
                        DataRow row2 = dt2.NewRow();
                        //   int s = dt2.Rows.Count + 1;
                        // row["RowNumber"] = s.ToString();
                        // row2["Funitid"] = "合计";
                        row2["deptname"] = "合计";
                        row2["SubTotal"] = SubTotal;
                        row2["RTotal"] = 0;
                        row2["CreateUser"] = "";
                        row2["text"] = "";
                        row2["contractdate"] = "";

                        // row2["typedesc"] = "";


                        // row2["BYHB"] = 0.0;
                        dt2.Rows.Add(row2);
                    }
                }
                if (dt3.Rows.Count > 0)
                {
                    double YFTotal = 0;
                    // double Ftotal2 = 0;
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (dt3.Rows[i]["YFTotal"].ToString() == "")
                            YFTotal += 0;
                        else
                            YFTotal += Convert.ToDouble(dt3.Rows[i]["YFTotal"]);

                    }

                    DataRow row3 = dt3.NewRow();
                    row3["DeptName"] = "合计";
                    //row3["ptype"] = "";
                    row3["YFTotal"] = YFTotal;
                    row3["DTime"] = "";
                    row3["Total"] = 0;
                    //row3["Greater3Less6"] = "";
                    //row3["Greater6Less1"] = "";
                    //row3["Greater1Less2"] = "";
                    //row3["Greater2"] = "";
                    dt3.Rows.Add(row3);

                }

                //dt.Columns.Add("合计", typeof(System.String));
                // dt.Rows.Add();
                //DataRow row = dt.NewRow();
                //int s = dt.Rows.Count + 1;
                // row["RowNumber"] = s.ToString();
                // row["EUnitID"] = "合计";

                string strCols = "序号-5000,姓名-5000,合同额-5000,已收款-5000,欠款-5000,回款率-5000";
                System.IO.MemoryStream stream = ExcelHelper.AccountsPayableStatisticalAnalysis2ToExcel(dt, dt2, dt3, "应收款累计经营分析二", datetime);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "应收款累计经营分析二.xls");
            }
            else
                return null;
        }


        /// <summary>
        /// 本月自有产品销售数量,销售额
        /// </summary>
        /// <returns></returns>

        public ActionResult MonthOwnProductSales()
        {

            return View();
        }

        public ActionResult GetMonthOwnProductSalesTable()
        {

            string Datetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string LastMonthDatetime = DateTime.Now.AddMonths(-2).ToString();

            string LastYearDatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();




            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            DataTable dt = SalesManage.GetMonthOwnProductSalesAmount(Datetime, LastMonthDatetime, LastYearDatetime);
            //  DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            //dt = SalesManage.GetReceivableAccountTable(enddatetime, lastenddatetime);

            dt2 = SalesManage.GetMonthOwnProductSalesTotal(Datetime, LastMonthDatetime, LastYearDatetime);

            dt3 = SalesManage.GetMonthlyAountOwnProducts(startdatetime, enddatetime, laststartdatetime, lastenddatetime);
            DataTable dt4 = SalesManage.GetMonthlyTotalOwnProducts(startdatetime, enddatetime, laststartdatetime, lastenddatetime);

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    double Aount = 0;
                    double LastAount = 0;
                    double ChangeAount = 0;
                    double LastYearAount = 0;
                    //   double YQNTQTotal = 0;

                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr><td colspan='7'>本月应收账款情况（单位：万元）</td></tr>");
                    sb.Append("<tr  class=\"left\"><td style=\"width:5%\">部门</td><td  style=\"width:5%\">产品分类</td><td  style=\"width:10%\">本月</td><td  style=\"width:5%\">上月</td><td  style=\"width:5%\">环比</td><td  style=\"width:5%\">去年同期</td>" +
        "<td  style=\"width:10%\">同比</td><td  style=\"width:10%\">占全年累计销售量%</td><td>备注</td></tr>");
                    // sb.Append("<tr><td>变动</td><td>变动%</td><td>变动</td><td>变动%</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToInt32(dt.Rows[i]["Aount"]);
                        if (dt.Rows[i]["LastAount"].ToString() == "")
                            LastAount += 0;
                        else
                            LastAount += Convert.ToInt32(dt.Rows[i]["LastAount"]);
                        if (dt.Rows[i]["ChangeAount"].ToString() == "")
                            ChangeAount += 0;
                        else
                            ChangeAount += Convert.ToInt32(dt.Rows[i]["ChangeAount"]);
                        if (dt.Rows[i]["LastYearAount"].ToString() == "")
                        {
                            LastYearAount = 0;
                        }
                        else { LastYearAount += Convert.ToInt32(dt.Rows[i]["LastYearAount"]); }



                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["Ddeptname"].ToString() + "</td><td>" + dt.Rows[i]["Text"] + "</td><td>" + dt.Rows[i]["Aount"].ToString() + "</td><td>" + dt.Rows[i]["LastAount"].ToString() + "</td><td>" + dt.Rows[i]["ChangeAount"] + "</td><td>" + dt.Rows[i]["ChangeAountPercent"].ToString() + "</td><td>" + dt.Rows[i]["LastYearAount"] + "</td><td>" + dt.Rows[i]["ChangeLastYearAount"] + "</td><td>" + dt.Rows[i]["ChangeLastYearAountPercent"] + "</td><td></td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + Aount + "</td><td>" + LastAount + "</td><td>" + ChangeAount + "</td><td></td>" + LastYearAount + "<td></td></tr>");

                }


                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double Total = 0;
                        double LastTotal = 0;

                        // double Ftotal2 = 0;
                        // double BYHBTotal2 = 0;
                        sb2.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                        sb2.Append("<tr><td colspan='7'>本月应收账款结构（单位：万元）</td></tr>");
                        sb2.Append("<tr  class=\"left\"><td rowspan='2' style=\"width:5%\">部门</td><td rowspan='2' style=\"width:5%\">产品分类</td><td rowspan='2' style=\"width:5%\">当月</td><td rowspan='2' style=\"width:10%\">上月</td><td colspan='2'  style=\"width:10%\">环比</td><td style=\"width:10%\">去年同期</td><td colspan='2' >环比</td><td rowspan='2'>占全年累计%</td></tr>");
                        //  sb2.Append("<tr><td rowspan='2' >%</td><td>其他需要说明的内容</td></tr>");
                        sb2.Append("<tr><td>变动</td><td>变动%</td><td>变动</td><td>变动%</td></tr>");
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt2.Rows[i]["Total"]);
                            if (dt2.Rows[i]["LastTotal"].ToString() == "")
                                LastTotal = 0;
                            else
                                LastTotal += Convert.ToDouble(dt2.Rows[i]["LastTotal"]);


                            sb2.Append("<tr class=\"staleft\"><td>" + dt2.Rows[i]["deptname"].ToString() + "</td><td>" + dt2.Rows[i]["text"] + "</td><td>" + dt2.Rows[i]["CreateUser"] + "</td><td>" + dt2.Rows[i]["SubTotal"].ToString() + "</td><td>" + dt2.Rows[i]["ContractDate"] + "</td><td>" + dt2.Rows[i]["RTotal"] + "</td><td></td></tr>");
                        }
                        sb2.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + Total + "</td><td></td><td></td><td></td></tr>");
                    }

                }

                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        double Aount = 0;
                        double LastAount = 0;
                        double ChangeAmount = 0;
                        // double Ftotal2 = 0;
                        // double BYHBTotal2 = 0;
                        sb3.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                        sb3.Append("<tr><td colspan='8'>累计产品自有销售量（单位：台）</td></tr>");
                        sb3.Append("<tr  class=\"left\"><td rowspan='2'  style=\"width:5%\">部门</td><td   rowspan='2' style=\"width:5%\">产品分类</td><td  rowspan='2' style=\"width:5%\">" + startdatetime + "-" + enddatetime + "月</td><td  rowspan='2' style=\"width:10%\">去年同期</td><td colspan='2' >同比</td><td  rowspan='2' >占全年累计销售量%</td><td  rowspan='2' >备注</td></tr>");
                        sb3.Append("<tr><td>变动</td><td>变动%</td></tr>");

                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt3.Rows[i]["Aount"].ToString() == "")
                                Aount += 0;
                            else
                                Aount += Convert.ToDouble(dt3.Rows[i]["Aount"]);
                            if (dt3.Rows[i]["LastAount"].ToString() == "")
                                LastAount = 0;
                            else
                                LastAount += Convert.ToDouble(dt3.Rows[i]["LastAount"]);
                            if (dt3.Rows[i]["ChangeAmount"].ToString() == "")
                                ChangeAmount = 0;
                            else
                                ChangeAmount += Convert.ToDouble(dt3.Rows[i]["ChangeAmount"]);
                            sb3.Append("<tr class=\"staleft\"><td>" + dt3.Rows[i]["deptname"].ToString() + "</td><td>" + dt3.Rows[i]["Text"] + "</td><td>" + dt3.Rows[i]["Aount"] + "</td><td>" + dt3.Rows[i]["LastAount"] + "</td><td>" + dt3.Rows[i]["ChangeAmount"].ToString() + "</td><td>" + dt3.Rows[i]["ChangeAmountPercent"] + "</td><td>" + dt3.Rows[i]["YearAount"] + "</td><td></td></tr>");
                        }
                        sb3.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + Aount + "</td><td>" + LastAount + "</td><td>" + ChangeAmount + "</td><td></td><td></td><td></td></tr>");
                    }

                }

                if (dt4 != null)
                {
                    if (dt4.Rows.Count > 0)
                    {
                        double Total = 0;
                        double LastTotal = 0;
                        double ChangeTotal = 0;
                        double UnitPriceTotal = 0;
                        double ML = 0;
                        sb4.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:20px;line-height:15px;\">");
                        sb4.Append("<tr><td colspan='11'>累计产品自有销额（单位：万元）</td></tr>");
                        sb4.Append("<tr  class=\"left\"><td rowspan='2'  style=\"width:5%\">部门</td><td   rowspan='2' style=\"width:5%\">产品分类</td><td  rowspan='2' style=\"width:5%\">" + startdatetime + "-" + enddatetime + "月</td><td  rowspan='2' style=\"width:10%\">去年同期</td><td colspan='2'>同比</td><td rowspan='2'>直接成本</td><td rowspan='2' >毛利</td><td rowspan='2' >毛利率</td><td  rowspan='2' >占全年累计销售量%</td><td  rowspan='2' >备注</td></tr>");
                        sb4.Append("<tr><td>变动</td><td>变动%</td></tr>");
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            if (dt4.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt4.Rows[i]["Total"]);

                            if (dt4.Rows[i]["LastTotal"].ToString() == "")
                                LastTotal = 0;
                            else
                                LastTotal += Convert.ToDouble(dt4.Rows[i]["LastTotal"]);
                            if (dt4.Rows[i]["LastTotal"].ToString() == "")
                                ChangeTotal = 0;
                            else
                                ChangeTotal += Convert.ToDouble(dt4.Rows[i]["LastTotal"]);

                            if (dt4.Rows[i]["UnitPriceTotal"].ToString() == "")
                                UnitPriceTotal = 0;
                            else
                                UnitPriceTotal += Convert.ToDouble(dt4.Rows[i]["UnitPriceTotal"]);
                            if (dt4.Rows[i]["ML"].ToString() == "")
                                ML = 0;
                            else
                                ML += Convert.ToDouble(dt4.Rows[i]["ML"]);

                            sb4.Append("<tr class=\"staleft\"><td>" + dt4.Rows[i]["deptname"].ToString() + "</td><td>" + dt4.Rows[i]["Text"] + "</td><td>" + dt4.Rows[i]["Total"] + "</td><td>" + dt4.Rows[i]["LastTotal"] + "</td><td>" + dt4.Rows[i]["ChangeTotal"].ToString() + "</td><td>" + dt4.Rows[i]["ChangeTotaltPercent"] + "</td><td>" + dt4.Rows[i]["UnitPriceTotal"] + "</td><td>" + dt4.Rows[i]["ML"] + "</td><td>" + dt4.Rows[i]["MLL"] + "</td><td>" + dt4.Rows[i]["YearTotalPercent"] + "</td><td></td></tr>");
                        }
                        sb4.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + Total + "</td><td>" + LastTotal + "</td><td>" + ChangeTotal + "</td><td></td><td>" + UnitPriceTotal + "</td><td>" + ML + "</td><td></td><td></td><td></td></tr>");
                    }
                }

                return Json(new { success = "true", strSb = sb.ToString(), BotomTab = sb2.ToString(), LJTab = sb3.ToString(), BotomTab4 = sb4.ToString() });

            }
            else
            {
                return Json(new { success = "false" });
            }
        }

        /// <summary>
        /// 自有产品销售量、销售额导出
        /// </summary>
        /// <returns></returns>
        /// 

        public FileResult GetMonthOwnProductSalesToExcel()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();

            string Datetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string LastMonthDatetime = DateTime.Now.AddMonths(-2).ToString();

            string LastYearDatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();
            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            DataTable dt = SalesManage.GetMonthOwnProductSalesAmount(Datetime, LastMonthDatetime, LastYearDatetime);
            //  DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt2 = SalesManage.GetMonthOwnProductSalesTotal(Datetime, LastMonthDatetime, LastYearDatetime);
            dt3 = SalesManage.GetMonthlyAountOwnProducts(startdatetime, enddatetime, laststartdatetime, lastenddatetime);
            DataTable dt4 = SalesManage.GetMonthlyTotalOwnProducts(startdatetime, enddatetime, laststartdatetime, lastenddatetime);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    double Aount = 0;
                    double LastAount = 0;
                    double ChangeAount = 0;
                    double LastYearAount = 0;
                    double ChangeLastYearAount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToDouble(dt.Rows[i]["Aount"]);
                        if (dt.Rows[i]["LastAount"].ToString() == "")
                            LastAount += 0;
                        else
                            LastAount += Convert.ToDouble(dt.Rows[i]["LastAount"]);
                        if (dt.Rows[i]["ChangeAmount"].ToString() == "")
                            ChangeAount += 0;
                        else
                            ChangeAount += Convert.ToDouble(dt.Rows[i]["ChangeAmount"]);
                        if (dt.Rows[i]["LastYearAount"].ToString() == "")
                            LastYearAount = 0;
                        else
                            LastYearAount += Convert.ToDouble(dt.Rows[i]["LastYearAount"]);
                        if (dt.Rows[i]["changeLastYearAmount"].ToString() == "")
                            ChangeLastYearAount = 0;
                        else
                            ChangeLastYearAount += Convert.ToDouble(dt.Rows[i]["changeLastYearAount"]);

                    }
                    DataRow row = dt.NewRow();
                    //row["EUnitID"] = "";
                    row["DeptName"] = "合计";
                    row["Text"] = "";
                    row["Aount"] = Aount;
                    row["LastAount"] = LastAount;
                    row["ChangeAmount"] = ChangeAount;
                    row["ChangeAmountPercent"] = 0;
                    row["LastYearAount"] = LastYearAount;
                    row["ChangeLastYearAmount"] = ChangeLastYearAount;
                    row["ChangeLastYearAmountPercent"] = 0;
                    row["YearAountPercent"] = 0;
                    //row["BYZE"] = 0.0;

                    //  row["LastTotalTB"] = GTotal;

                    dt.Rows.Add(row);
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double Total = 0;
                        double LastTotal = 0;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt2.Rows[i]["Total"]);
                            if (dt2.Rows[i]["LastTotal"].ToString() == "")
                                LastTotal = 0;
                            else
                                LastTotal += Convert.ToDouble(dt2.Rows[i]["LastTotal"]);
                        }
                        DataRow row2 = dt2.NewRow();
                        row2["deptname"] = "合计";
                        row2["SubTotal"] = Total;
                        row2["RTotal"] = 0;
                        row2["CreateUser"] = "";
                        row2["text"] = "";
                        row2["contractdate"] = "";
                        dt2.Rows.Add(row2);
                    }
                }
                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        double Aount = 0;
                        double LastAount = 0;
                        // double Ftotal2 = 0;
                        double ChangeAmount = 0;

                        double YearAount = 0;
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt3.Rows[i]["Aount"].ToString() == "")
                                Aount += 0;
                            else
                                Aount += Convert.ToDouble(dt3.Rows[i]["Aount"]);
                            if (dt3.Rows[i]["LastAount"].ToString() == "")
                                LastAount = 0;
                            else
                                LastAount += Convert.ToDouble(dt3.Rows[i]["LastAount"]);
                            if (dt3.Rows[i]["ChangeAmount"].ToString() == "")
                                ChangeAmount = 0;
                            else
                                ChangeAmount += Convert.ToDouble(dt3.Rows[i]["ChangeAmount"]);

                            if (dt3.Rows[i]["YearAount"].ToString() == "")
                                YearAount = 0;
                            else
                                YearAount += Convert.ToDouble(dt3.Rows[i]["YearAount"]);
                        }

                        DataRow row3 = dt3.NewRow();
                        row3["DeptName"] = "合计";
                        //row3["ptype"] = "";
                        row3["Aount"] = Aount;
                        row3["LastAount"] = LastAount;
                        row3["ChangeAmount"] = ChangeAmount;
                        row3["ChangeAmountPercent"] = 0;
                        row3["YearAount"] = YearAount;
                        // row3["Total"] = 0;
                        dt3.Rows.Add(row3);

                    }
                }
                if (dt4 != null)
                {
                    if (dt4.Rows.Count > 0)
                    {
                        double Total = 0;
                        double LastTotal = 0;
                        // double Ftotal2 = 0;
                        double ChangeTotal = 0;
                        double UnitPriceTotal = 0;

                        double ML = 0;
                        //  double YearAount = 0;
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt4.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt4.Rows[i]["Total"]);
                            if (dt4.Rows[i]["LastTotal"].ToString() == "")
                                LastTotal = 0;
                            else
                                LastTotal += Convert.ToDouble(dt3.Rows[i]["LastTotal"]);
                            if (dt4.Rows[i]["ChangeTotal"].ToString() == "")
                                ChangeTotal = 0;
                            else
                                ChangeTotal += Convert.ToDouble(dt4.Rows[i]["ChangeTotal"]);

                            //if (dt4.Rows[i]["YearAount"].ToString() == "")
                            //    YearAount = 0;
                            //else
                            //    YearAount += Convert.ToDouble(dt3.Rows[i]["YearAount"]);
                            if (dt4.Rows[i]["UnitPriceTotal"].ToString() == "")
                                UnitPriceTotal = 0;
                            else
                                UnitPriceTotal += Convert.ToDouble(dt4.Rows[i]["UnitPriceTotal"]);

                            if (dt4.Rows[i]["ML"].ToString() == "")
                                ML = 0;
                            else
                                ML += Convert.ToDouble(dt4.Rows[i]["ML"]);



                        }

                        DataRow row4 = dt4.NewRow();
                        row4["DeptName"] = "合计";
                        row4["Text"] = "";
                        row4["Total"] = Total;
                        row4["LastTotal"] = LastTotal;
                        row4["ChangeTotal"] = ChangeTotal;
                        row4["ChangeTotalPercent"] = 0;
                        row4["UnitPriceTotal"] = UnitPriceTotal;
                        row4["ML"] = ML;
                        row4["MLL"] = 0;
                        row4["YearTotalPercent"] = 0;
                        dt3.Rows.Add(row4);

                    }
                }



                System.IO.MemoryStream stream = ExcelHelper.MonthOwnProductSalesToExcel(dt, dt2, dt3, dt4, "自有产品销售额/销售量分析", Datetime);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "自有产品销售额/销售量分析.xls");
            }
            else
                return null;
        }



        /// <summary>
        /// 自有产品销售渠道、销售类型
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthOwnProductChannelsFrom()
        {
            return View();
        }

        public ActionResult GetMonthOwnProductChannelsFrom()
        {

            string Datetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string LastMonthDatetime = DateTime.Now.AddMonths(-2).ToString();

            string LastYearDatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            DataTable dt = SalesManage.GetMonthOwnProductChannelsFrom(Datetime, startdatetime, enddatetime);
            DataTable dt2 = SalesManage.GetMonthOwnProductModelAountTop10(Datetime);
            DataTable dt3 = SalesManage.GetMonthOwnProductModelTotalTop10(Datetime);
            DataTable dt4 = SalesManage.GetMonthOwnProductFromToAountTop10(Datetime, enddatetime);
            DataTable dt5 = SalesManage.GetMonthOwnProductFromToTotalTop10(Datetime, enddatetime);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    double Aount = 0;
                    double Total = 0;
                    double MAount = 0;
                    double MTotal = 0;
                    double SalesAount = 0;
                    // double SalesAount = 0;
                    double SalesTotal = 0;
                    double CSalesAount = 0;
                    double CSalesTotal = 0;
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb.Append("<tr><td colspan='9'>自有产品销售渠道分析（单位：万元）</td></tr>");
                    sb.Append("<tr  class=\"left\"><td style=\"width:5%\">部门</td><td  style=\"width:5%\">渠道类型</td><td  style=\"width:10%\">本月销售量</td><td  style=\"width:5%\">占本月总销售量%</td><td  style=\"width:5%\">本月销售额</td><td  style=\"width:5%\">1-？月销售量</td><td  style=\"width:5%\">占累计自有产品销售总量% </td><td  style=\"width:5%\">1-？月销售额</td><td  style=\"width:5%\">占累计自有产品销售总额%</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToDouble(dt.Rows[i]["Aount"]);
                        if (dt.Rows[i]["Total"].ToString() == "")
                            Total += 0;
                        else
                            Total += Convert.ToDouble(dt.Rows[i]["Total"]);
                        if (dt.Rows[i]["SalesAount"].ToString() == "")
                            SalesAount += 0;
                        else
                            SalesAount += Convert.ToDouble(dt.Rows[i]["SalesAount"]);

                        if (dt.Rows[i]["SalesTotal"].ToString() == "")
                            SalesTotal += 0;
                        else
                            SalesTotal += Convert.ToDouble(dt.Rows[i]["SalesTotal"]);

                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["DeptName"].ToString() + "</td><td>" + dt.Rows[i]["ChannelsFrom"] + "</td><td>" + dt.Rows[i]["Aount"].ToString() + "</td><td>" + dt.Rows[i]["AountPercent"] + "</td><td>" + dt.Rows[i]["Total"].ToString() + "</td><td>" + dt.Rows[i]["SalesAount"].ToString() + "</td><td>" + dt.Rows[i]["CSalesAountPercent"].ToString() + "</td><td>" + dt.Rows[i]["SalesTotal"].ToString() + "</td><td>" + dt.Rows[i]["CSalesTotalPercent"].ToString() + "</td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td>" + Aount + "</td><td></td><td>" + Total + "</td><td>" + SalesAount + "</td><td></td><td>" + SalesTotal + "</td><td></td></tr>");

                }


                if (dt2.Rows.Count > 0)
                {
                    double Aount = 0;

                    sb1.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb1.Append("<tr><td colspan='9'>自有产品销售型号前10名（按销售量）</td></tr>");
                    sb1.Append("<tr  class=\"left\"><td style=\"width:5%\">序号</td><td  style=\"width:5%\">产品类别</td><td  style=\"width:10%\">产品型号</td><td  style=\"width:5%\">本月销售量</td><td  style=\"width:5%\">占本月计自有产品销售总量% </td></tr>");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToDouble(dt2.Rows[i]["Aount"]);

                        sb1.Append("<tr class=\"staleft\"><td>" + (i + 1) + "</td><td>" + dt2.Rows[i]["Text"] + "</td><td>" + dt2.Rows[i]["Spec"].ToString() + "</td><td>" + dt2.Rows[i]["Aount"].ToString() + "</td><td>" + dt2.Rows[i]["AountPercent"] + "</td></tr>");
                    }
                    sb1.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + Aount + "</td><td></td></tr>");

                }


                if (dt3.Rows.Count > 0)
                {
                    double Total = 0;

                    sb2.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb2.Append("<tr><td colspan='9'>自有产品销售型号前10名（按销售额）</td></tr>");
                    sb2.Append("<tr  class=\"left\"><td style=\"width:5%\">序号</td><td  style=\"width:5%\">产品类别</td><td  style=\"width:10%\">产品型号</td><td  style=\"width:5%\">本月销售量</td><td  style=\"width:5%\">占本月计自有产品销售总量% </td></tr>");
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (dt3.Rows[i]["Total"].ToString() == "")
                            Total += 0;
                        else
                            Total += Convert.ToDouble(dt3.Rows[i]["Total"]);

                        sb2.Append("<tr class=\"staleft\"><td>" + (i + 1) + "</td><td>" + dt3.Rows[i]["Text"] + "</td><td>" + dt3.Rows[i]["Spec"].ToString() + "</td><td>" + dt3.Rows[i]["Total"].ToString() + "</td><td>" + dt3.Rows[i]["TotalPercent"] + "</td></tr>");
                    }
                    sb2.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + Total + "</td><td></td></tr>");

                }
                if (dt4.Rows.Count > 0)
                {
                    double Aount = 0;

                    sb3.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb3.Append("<tr><td colspan='9'>自有产品销售型号前10名（按销售量）</td></tr>");
                    sb3.Append("<tr  class=\"left\"><td style=\"width:5%\">序号</td><td  style=\"width:5%\">产品类别</td><td  style=\"width:10%\">产品型号</td><td  style=\"width:5%\">本月销售量</td><td  style=\"width:5%\">占本月计自有产品销售总量% </td></tr>");
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (dt4.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToDouble(dt4.Rows[i]["Aount"]);

                        sb3.Append("<tr class=\"staleft\"><td>" + (i + 1) + "</td><td>" + dt4.Rows[i]["Text"] + "</td><td>" + dt4.Rows[i]["Spec"].ToString() + "</td><td>" + dt4.Rows[i]["Aount"].ToString() + "</td><td>" + dt4.Rows[i]["AountPercent"] + "</td></tr>");
                    }
                    sb3.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + Aount + "</td><td></td></tr>");

                }

                if (dt5.Rows.Count > 0)
                {
                    double Total = 0;

                    sb4.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">");
                    sb4.Append("<tr><td colspan='9'>自有产品销售型号前10名（按销售额）</td></tr>");
                    sb4.Append("<tr  class=\"left\"><td style=\"width:5%\">序号</td><td  style=\"width:5%\">产品类别</td><td  style=\"width:10%\">产品型号</td><td  style=\"width:5%\">本月销售量</td><td  style=\"width:5%\">占本月计自有产品销售总量% </td></tr>");
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (dt5.Rows[i]["Total"].ToString() == "")
                            Total += 0;
                        else
                            Total += Convert.ToDouble(dt5.Rows[i]["Total"]);

                        sb4.Append("<tr class=\"staleft\"><td>" + (i + 1) + "</td><td>" + dt5.Rows[i]["Text"] + "</td><td>" + dt5.Rows[i]["Spec"].ToString() + "</td><td>" + dt5.Rows[i]["Total"].ToString() + "</td><td>" + dt5.Rows[i]["TotalPercent"] + "</td></tr>");
                    }
                    sb4.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td>" + Total + "</td><td></td></tr>");

                }


                return Json(new { success = "true", strSb = sb.ToString(), BotomTab = sb1.ToString(), LJTab = sb2.ToString(), BotomTab4 = sb3.ToString(), BotomTab5 = sb4.ToString() });
            }
            else
            {
                return Json(new { success = "false" });
            }


        }



        public FileResult GetMonthOwnProductChannelsFromToExcel()
        {
            string Datetime = DateTime.Now.AddMonths(-1).ToString();
            //string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string LastMonthDatetime = DateTime.Now.AddMonths(-2).ToString();

            string LastYearDatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            string startdatetime = DateTime.Now.AddMonths(-1).Year.ToString() + "/01/01";
            string enddatetime = DateTime.Now.AddMonths(-1).ToString();
            string laststartdatetime = DateTime.Now.AddMonths(-1).AddYears(-1).Year.ToString() + "/01/01";
            string lastenddatetime = DateTime.Now.AddYears(-1).AddMonths(-1).ToString();

            DataTable dt = SalesManage.GetMonthOwnProductChannelsFrom(Datetime, startdatetime, enddatetime);
            DataTable dt2 = SalesManage.GetMonthOwnProductModelAountTop10(Datetime);
            DataTable dt3 = SalesManage.GetMonthOwnProductModelTotalTop10(Datetime);
            DataTable dt4 = SalesManage.GetMonthOwnProductFromToAountTop10(Datetime, enddatetime);
            DataTable dt5 = SalesManage.GetMonthOwnProductFromToTotalTop10(Datetime, enddatetime);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    double Aount = 0;
                    double Total = 0;
                    double MAount = 0;
                    double MTotal = 0;
                    double SalesAount = 0;
                    // double SalesAount = 0;
                    double SalesTotal = 0;
                    double CSalesAount = 0;
                    double CSalesTotal = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Aount"].ToString() == "")
                            Aount += 0;
                        else
                            Aount += Convert.ToDouble(dt.Rows[i]["Aount"]);
                        if (dt.Rows[i]["Total"].ToString() == "")
                            Total += 0;
                        else
                            Total += Convert.ToDouble(dt.Rows[i]["Total"]);
                        if (dt.Rows[i]["SalesAount"].ToString() == "")
                            SalesAount += 0;
                        else
                            SalesAount += Convert.ToDouble(dt.Rows[i]["SalesAount"]);

                        if (dt.Rows[i]["SalesTotal"].ToString() == "")
                            SalesTotal += 0;
                        else
                            SalesTotal += Convert.ToDouble(dt.Rows[i]["SalesTotal"]);

                    }
                    DataRow row = dt.NewRow();
                    //row["EUnitID"] = "";
                    row["DeptName"] = "合计";
                    row["ChannelsFrom"] = "";
                    row["Aount"] = Aount;
                    row["AountPercent"] = 0;
                    row["Total"] = Total;
                    row["SalesAount"] = SalesAount;
                    row["CSalesAountPercent"] = 0;
                    row["SalesTotal"] = SalesTotal;
                    row["CSalesTotalPercent"] = 0;
                    dt.Rows.Add(row);
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        double Aount = 0;
                        double LastTotal = 0;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i]["Aount"].ToString() == "")
                                Aount += 0;
                            else
                                Aount += Convert.ToDouble(dt2.Rows[i]["Aount"]);

                        }
                        DataRow row2 = dt2.NewRow();
                        row2["Text"] = "合计";
                        row2["Spec"] = "";
                        row2["Aount"] = Aount;
                        row2["AountPercent"] = 0;
                        dt2.Rows.Add(row2);
                    }
                }
                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        double Total = 0;

                        double YearAount = 0;
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt3.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt3.Rows[i]["Total"]);
                        }

                        DataRow row3 = dt3.NewRow();
                        row3["Text"] = "合计";
                        row3["Spec"] = "";
                        row3["Total"] = Total;
                        row3["TotalPercent"] = 0;
                        dt3.Rows.Add(row3);

                    }
                }
                if (dt4 != null)
                {
                    if (dt4.Rows.Count > 0)
                    {
                        double Aount = 0;
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            if (dt4.Rows[i]["Aount"].ToString() == "")
                                Aount += 0;
                            else
                                Aount += Convert.ToDouble(dt4.Rows[i]["Aount"]);
                        }
                        DataRow row4 = dt4.NewRow();
                        row4["Text"] = "合计";
                        row4["Spec"] = "";
                        row4["Aount"] = Aount;
                        row4["AountPercent"] = 0;
                        dt4.Rows.Add(row4);

                    }
                }

                if (dt5 != null)
                {
                    if (dt5.Rows.Count > 0)
                    {
                        double Total = 0;
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            if (dt5.Rows[i]["Total"].ToString() == "")
                                Total += 0;
                            else
                                Total += Convert.ToDouble(dt5.Rows[i]["Total"]);
                        }
                        DataRow row5 = dt5.NewRow();
                        row5["Text"] = "合计";
                        row5["Spec"] = "";
                        row5["Total"] = Total;
                        row5["TotalPercent"] = 0;
                        dt5.Rows.Add(row5);

                    }
                }

                System.IO.MemoryStream stream = ExcelHelper.GetMonthOwnProductChannelsFromToExcel(dt, dt2, dt3, dt4, dt5, "自有产品渠道/型号分析", Datetime);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "自有产品渠道/型号分析.xls");
            }
            else
                return null;
        }

        #endregion

        #region [获取物品的单价信息]
        public ActionResult GetProductPrice()
        {
            string ProID = Request["ProID"].ToString();
            string SupID = Request["SupID"].ToString();
            DataTable dt = SalesManage.GetProductPrice(ProID, SupID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region [合同管理]
        public ActionResult SalesContractMaintain()
        {
            return View();
        }
        public ActionResult AddContract()
        {

            ContractBas Bas = new ContractBas();
            Bas.StrCID = ContractMan.GetNewShowCID();
            Bas.StrPID = Request["id"];
            ViewData["StrPID"] = Bas.StrPID;
            return View(Bas);
        }
        public ActionResult InsertContractBas(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Bas.StrUnit = account.UnitID.ToString();
                Bas.StrCID = ContractMan.GetNewCID();
                Bas.StrCreateTime = DateTime.Now;
                Bas.StrCreateUser = account.UserID.ToString();
                Bas.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewContractBas(Bas, ref strErr) == true)
                {//修改备案的状态为已签订合同
                    SalesManage.UPdateProjectState(Bas.StrPID);
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult AddFile(string id)
        {
            ViewData["StrCID"] = id;
            ViewData["ContractID"] = SalesManage.GetOrdersByOrderID(id).ContractID;
            return View();
        }

        public ActionResult DownloadFile(string id)
        {
            ViewData["StrCID"] = id;
            ViewData["ContractID"] = SalesManage.GetOrdersByOrderID(id).ContractID;
            return View();
        }

        public void DownLoad2(string id)
        {
            DataTable dtInfo = SalesManage.GetNewDownloadFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["SalesOfferupload"] + "\\"
                    + dtInfo.Rows[0]["FileInfo"] + "\\" + fileName;//路径

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = SalesManage.GetDownload(id);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["ID"].ToString() + "@";
                name += dtInfo.Rows[i]["FileName"].ToString() + "@";
                file += dtInfo.Rows[i]["FileInfo"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }


        public ActionResult GetUploadFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = SalesManage.GetUploadFile(id);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["ID"].ToString() + "@";
                name += dtInfo.Rows[i]["FileName"].ToString() + "@";
                file += dtInfo.Rows[i]["FileInfo"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }
        public void DownLoad(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SalesManage.GetNewDownloadFile(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FileName"].ToString()));
                //Response.BinaryWrite(bContent);
                //Response.Flush();
                //Response.End();
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SalesManage.DellNewFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertForm()
        {
            string CID = Request["StrCID"];
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            string FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
            ViewData["FileName"] = FileName;
            ViewData["ContractID"] = SalesManage.GetOrdersByOrderID(CID).ContractID;

            int fileLength = file.ContentLength;
            if (fileLength != 0)
            {
                fileByte = new byte[fileLength];
                file.InputStream.Read(fileByte, 0, fileLength);
            }
            string strErr = "";
            if (SalesManage.InsertNewFile(CID, fileByte, FileName, ref strErr) == true)
            {//上传工业品买卖合同后修改订单的状态为


                ViewData["msg"] = "上传成功";
                ViewData["StrCID"] = CID;

                return View("AddFile");
            }
            else
            {
                ViewData["msg"] = "上传失败";
                return View("AddFile");
            }
        }
        public ActionResult ChangeContract(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas = ContractMan.getNewChangeContract(id);
            return View(Bas);
        }

        public ActionResult upProjectContract(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                string strErr = "";
                //if (SalesManage.UpdateNewProjectContract(Bas, files, ref strErr) == true)
                //{
                //    ViewData["msg"] = "保存成功";
                //    return View("ChangeProContract", Bas);
                //}
                //else
                //{
                //    ViewData["msg"] = "保存失败";
                return View("ChangeProContract", Bas);
                //}
            }
            else
            {
                //如果有错误，继续输入信息
                return View("ChangeProContract", Bas);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult UpdateContract(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ContractMan.UpdateNewContractBas(Bas, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        //合同回款
        public ActionResult CashBack(string id, string type)
        {
            CCashBack Cash = new CCashBack();
            Cash.StrCID = id;
            Cash.StrCBID = ContractMan.GetNewshowCBID(id);
            Cash.StrCurAmountNum = ContractMan.GetNewCurAmountNum(id);
            ViewData["type"] = type;
            return View(Cash);
        }
        //合同结算
        public ActionResult Settlement(string id)
        {

            CSettlement CST = new CSettlement();
            CST.StrCID = id;
            CST.StrDebtAmount = ContractMan.getNewDebtAmount(id);
            if (CST.StrDebtAmount == 0)
                CST.StrIsDebt = 0;
            else
                CST.StrIsDebt = 1;
            return View(CST);
        }
        public ActionResult SettlementPro(string id)
        {
            string[] arr = id.Split('@');
            CSettlement CST = new CSettlement();
            ViewData["PID"] = arr[1];
            CST.StrCID = arr[0];
            CST.StrDebtAmount = ContractMan.getNewDebtAmountPro(arr[0]);
            if (CST.StrDebtAmount == 0)
                CST.StrIsDebt = 0;
            else
                CST.StrIsDebt = 1;
            return View(CST);
        }

        public ActionResult InsertSettlementPro(CSettlement CST, string PID)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                CST.StrCreateUser = account.UserID.ToString();
                CST.StrCreateTime = DateTime.Now;
                string strErr = "";
                if (ContractMan.InsertNewSettlementPro(CST, PID, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult InsertSettlement(CSettlement CST)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                CST.StrCreateUser = account.UserID.ToString();
                CST.StrCreateTime = DateTime.Now;
                string strErr = "";
                if (ContractMan.InsertNewSettlement(CST, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult DetailContract(string id)
        {
            string where = " and a.CID = '" + id + "'";
            DataTable dt = ContractMan.getNewDetailContract(where);
            StringBuilder sb = new StringBuilder();
            sb.Append(" <div id=\"tabTitile\"><span style=\"margin-left:10px;\">合同ID：" + dt.Rows[0]["CID"].ToString() + "</span></div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">合同编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ContractID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">业务类型</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BusinessTypeDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">对应项目编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Cname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同开始时间</td><td class=\"textRight\">" + dt.Rows[0]["CStartTime"].ToString() + "</td><td class=\"textLeft\">合同工期</td><td class=\"textRight\">" + dt.Rows[0]["TimeScale"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">预计完工时间</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["CPlanEndTime"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同初始金额</td><td class=\"textRight\">" + dt.Rows[0]["CBeginAmount"].ToString() + "</td><td class=\"textLeft\">履约保证金</td><td class=\"textRight\">" + dt.Rows[0]["Margin"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同签订回款次数</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["AmountNum"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同签订日期</td><td class=\"textRight\">" + dt.Rows[0]["Ctime"].ToString() + "</td><td class=\"textLeft\">负责人</td><td class=\"textRight\">" + dt.Rows[0]["Principal"].ToString() + "</td></tr>");
            if (dt.Rows[0]["CEndAmount"].ToString() != "0.00")
                sb.Append("<tr><td class=\"textLeft\">变更后金额</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["CEndAmount"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同款向</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PayOrIncome"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">甲方</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PartyA"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">乙方</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PartyB"].ToString() + "</td></tr>");
            sb.Append("");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult InsertCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {
                Cash.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Cash.StrCreateUser = account.UserID.ToString();
                Cash.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewCCashBack(Cash, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult UpdateCashBack(string id)
        {
            CCashBack Cash = new CCashBack();
            Cash = ContractMan.getNewUpdateCashBack(id);
            return View(Cash);
        }

        public ActionResult upCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (SalesManage.UpdateNewCCashBack(Cash, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult dellCashBack()
        {
            var id = Request["data1"];
            string[] arr = id.Split('@');
            //  string cid = arr[1].ToString();
            string strErr = "";
            if (SalesManage.dellNewCCashBack(arr[0], arr[1], ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        #endregion



        #region [库存查询]
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryQuery()
        {
            return View();
        }

        public ActionResult GetInventoryGrid(tk_InventoryGrid Inventorygrid)
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string HouseName = Inventorygrid.HouserName;
                string FinishAount = Inventorygrid.FinishAount;
                string Spec = Inventorygrid.Spec;
                string ProductID = Inventorygrid.ProductID;

                string s = "";
                string where = "";
                if (!string.IsNullOrEmpty(HouseName))
                {
                    s += " and HouseName like '%" + HouseName + "%'";
                }
                if (!string.IsNullOrEmpty(ProductID))
                {
                    s += " and  ProductID like '%" + ProductID + "%' ";
                }
                if (!string.IsNullOrEmpty(FinishAount))
                {
                    s += " and  FinishCount =" + FinishAount + "";
                }
                if (!string.IsNullOrEmpty(Spec))
                {
                    s += " and  Spec like '%" + Spec + "%' ";
                }
                if (!string.IsNullOrEmpty(s))
                {
                    s = s.Substring(4, s.Length - 4);
                }


                if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
                string Ptype = Request["ptype"].ToString();

                UIDataTable udtTask = SalesManage.GetInventoryGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        #endregion



        #region [订单跟踪]

        public ActionResult OrderTrack()
        {
            return View();
        }
        public ActionResult GetOrderPstate()
        {
            string Orderid = Request["Orderid"].ToString();

            DataTable dt = SalesManage.GetOrderPstate(Orderid);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        public ActionResult GetOrderPstateConfig()
        {
            string Pstate = Request["pstate"].ToString();

            DataTable dt = SalesManage.GetOrderPstateConfig(Pstate);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
    }
}

