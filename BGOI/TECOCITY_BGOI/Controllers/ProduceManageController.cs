using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.IO;
using System.Web.Services.Protocols;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    public class ProduceManageController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }
        #region 生产任务
        #region  任务单管理

        #region  新增生产任务
        public ActionResult getRZ()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string RWID = Request["RWID"].ToString();
            UIDataTable udtTask = ProduceMan.getRZ(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RWID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getspec()
        {
            string OrderContent = Request["OrderContent"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.getspec(OrderContent);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult Productiontasklist()
        {
            ViewData["webkey"] = "生产任务审批";
            string fold = COM_ApprovalMan.getNewwebkey("生产任务审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        //任务单列表
        public ActionResult Productionlist(ProduceList ProduceList)
        {
            if (ModelState.IsValid)
            {
                string where = "  and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "6";

                string UnitID = ProduceList.UnitID;
                string OrderContent = Request["OrderContent"].ToString();
                string SpecsModels = Request["SpecsModels"].ToString();


                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";

                string Ends = Request["Ends"].ToString();
                if (Ends != "")
                    Ends += " 00:00:00";

                string Ende = Request["Ende"].ToString();
                if (Ende != "")
                    Ende += " 23:59:59";

                string Statea = Request["Statea"].ToString();
                string Stateb = Request["Stateb"].ToString();

                if (Request["UnitID"] != "")
                    where += " a.OrderUnit like '%" + Request["UnitID"] + "%' and";
                if (OrderContent != "")
                    where += " b.OrderContent = '" + OrderContent + "' and";
                if (SpecsModels != "")
                    where += " b.SpecsModels = '" + SpecsModels + "' and";
                if (Starts != "" && Starte != "")
                    where += " a.CreateTime between '" + Starts + "' and '" + Starte + "' and";
                if (Ends != "" && Ende != "")
                    where += " a.Productioncompletiontime  between '" + Ends + "' and '" + Ende + "' and";
                if (Statea == "0")
                {
                    where += "";
                }
                else
                {
                    where += " (a.State= '" + Statea + "') or";
                }


                if (Stateb == "0")
                {
                    where += "";
                }
                else
                {
                    where += " (a.State= '" + Stateb + "') and";
                }
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProduceMan.ProduceRemainList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        //相关单据
        public ActionResult LoadTask()
        {
            string RWID = Request["RWID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.LoadTask(RWID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }



        //点击后详情页面
        public ActionResult LoadTasks()
        {
            string strErr = "";
            string a = Request["ID"].ToString();
            string ID = a.Substring(0, 2);
            if (ID == "RW")
            {
                DataTable data = ProduceMan.GetRW(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["Clientcode"] = dt["Clientcode"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderContactor"] = dt["OrderContactor"].ToString();
                    ViewData["OrderAddress"] = dt["OrderAddress"].ToString();
                    ViewData["OrderTel"] = dt["OrderTel"].ToString();
                    ViewData["Remark"] = dt["Remark"].ToString();
                    ViewData["ContractDate"] = dt["a"].ToString();
                    ViewData["Technology"] = dt["Technology"].ToString();
                    ViewData["Note"] = dt["Note"].ToString();
                    ViewData["m"] = "任务单编号：" + a + "";
                }
            }

            if (ID == "LL")
            {
                DataTable data = ProduceMan.GetLL(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["ID"] = dt["ID"].ToString();
                    ViewData["MaterialDepartment"] = dt["MaterialDepartment"].ToString();
                    ViewData["CreateTime"] = dt["CreateTime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["Amount"] = dt["Amount"].ToString();
                    ViewData["Remark"] = dt["Remark"].ToString();
                    ViewData["z"] = dt["m"].ToString();
                    ViewData["n"] = dt["n"].ToString();
                    ViewData["MaterialTime"] = dt["MaterialTime"].ToString();
                    ViewData["m"] = "领料单编号：" + a + "";
                }
            }
            if (ID == "SG")
            {
                DataTable data = ProduceMan.GetSG(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["ID"] = dt["ID"].ToString();
                    ViewData["billing"] = dt["billing"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["Remark"] = dt["Remark"].ToString();
                    ViewData["CreateUser"] = dt["CreateUser"].ToString();
                    ViewData["m"] = "随工单编号：" + a + "";
                }
            }
            if (ID == "BG")
            {
                DataTable data = ProduceMan.GetBG(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["uploadtime"] = dt["uploadtime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["Remarks"] = dt["Remarks"].ToString();
                    ViewData["CreatePerson"] = dt["CreatePerson"].ToString();
                    ViewData["m"] = "报告单编号：" + a + "";
                }
            }
            if (ID == "RK")
            {
                DataTable data = ProduceMan.GetRK(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["FinishTime"] = dt["FinishTime"].ToString();
                    ViewData["StockInTime"] = dt["StockInTime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["HouseID"] = dt["HouseID"].ToString();
                    ViewData["Batch"] = dt["Batch"].ToString();
                    ViewData["StockRemark"] = dt["StockRemark"].ToString();
                    ViewData["Storekeeper"] = dt["Storekeeper"].ToString();
                    ViewData["StockInUser"] = dt["StockInUser"].ToString();
                    ViewData["m"] = "入库单编号：" + a + "";
                }
            }
            return View();
        }
        //任务单相关单据
        public ActionResult LoadRWPayment()
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
            string RWID = Request["RWID"];
            if (!string.IsNullOrEmpty(RWID)) { where = " where RWID='" + RWID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadRWPayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //任务单详细相关单据
        public ActionResult LoadRWPaymentDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string RWID = Request["RWID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadRWPaymentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RWID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //领料单相关单据
        public ActionResult LoadLLPayment()
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
            string LLID = Request["LLID"];
            if (!string.IsNullOrEmpty(LLID)) { where = " where a.RWIDDID=b.DID and a.LLID='" + LLID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadLLPayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //领料单详细相关单据
        public ActionResult LoadLLPaymentDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string LLID = Request["LLID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadLLPaymentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, LLID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //随工单相关单据
        public ActionResult LoadSGPayment()
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
            string SGID = Request["SGID"];
            if (!string.IsNullOrEmpty(SGID)) { where = " where SGID='" + SGID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadSGPayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //随工单详细相关单据
        public ActionResult LoadSGPaymentDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string SGID = Request["SGID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadSGPaymentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, SGID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //报告单相关单据
        public ActionResult LoadBGPayment()
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
            string BGID = Request["BGID"];
            if (!string.IsNullOrEmpty(BGID)) { where = " where a.RWID=b.RWID and  BGID='" + BGID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadBGPayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //报告单详细相关单据
        public ActionResult LoadBGPaymentDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string BGID = Request["BGID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadBGPaymentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, BGID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //入库单相关单据
        public ActionResult LoadRKPayment()
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
            string RKID = Request["RKID"];
            if (!string.IsNullOrEmpty(RKID)) { where = " where RKID='" + RKID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadRKPayment(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //入库单详细相关单据
        public ActionResult LoadRKPaymentDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string RKID = Request["RKID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadRKPaymentDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RKID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //RWID编码
        public ActionResult AddTask()
        {
            tk_ProductTask si = new TECOCITY_BGOI.tk_ProductTask();
            si.RWID = ProduceMan.GetTopRWID();
            si.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(si);
        }

        public ActionResult ProduceInDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "4";

            string RWID = Request["RWID"].ToString();


            UIDataTable udtTask = ProduceMan.ProduceInDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RWID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ChangeTask()
        {
            return View();
        }

        public ActionResult ChangeTasks()
        {
            return View();
        }

        public ActionResult ChangeSpecsModelsList()
        {
            string where = "and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string OrderID = Request["OrderID"].ToString();
            string SpecsModels = Request["SpecsModels"].ToString();

            if (OrderID != "")
                where += " b.OrderID = '" + OrderID + "' and";
            if (SpecsModels != "")
                where += " b.SpecsModels = '" + SpecsModels + "' and";
            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = ProduceMan.ChangeSpecsModelsList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

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
                strRowNum = "100";

            UIDataTable udtTask = ProduceMan.GetPtype(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
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

            UIDataTable udtTask = ProduceMan.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Changematerial()
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

            UIDataTable udtTask = ProduceMan.Changematerial(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaskDetail()
        {
            string OrderID = Request["OrderID"].ToString();
            DataTable dt = ProduceMan.GetTaskDetail(OrderID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult getKCnum()
        {
            string PID = Request["PID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            DataTable dt = ProduceMan.getKCnum(PID, HouseID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetTaskDetails()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetTaskDetails(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult IndexAllcustom()
        {
            string OrderID = Request["OrderID"].ToString();
            DataTable dt = ProduceMan.IndexAllcustom(OrderID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult AddProcureStockIn()
        {
            tk_ProductTask si = new TECOCITY_BGOI.tk_ProductTask();
            si.RWID = ProduceMan.GetTopRWID();
            si.CreateUser = GAccount.GetAccountInfo().UserName;

            return View(si);
        }

        public string ProTaskDetialNum(string RWID)
        {
            return ProduceMan.ProTaskDetialNum(RWID);
        }

        //保存
        public ActionResult SaveTaskIn()
        {
            tk_ProductTask task = new tk_ProductTask();
            task.RWID = Request["RWID"].ToString();
            task.Clientcode = Request["Clientcode"].ToString();
            task.OrderUnit = Request["OrderUnit"].ToString();
            task.OrderAddress = Request["OrderAddress"].ToString();
            task.OrderContactor = Request["OrderContactor"].ToString();
            task.OrderTel = Request["OrderTel"].ToString();
            task.Remark = Request["Remark"].ToString();
            task.CreateUser = Request["CreateUser"].ToString();
            task.ContractDate = Convert.ToDateTime(Request["ContractDate"]);
            task.CreateTime = DateTime.Now;
            task.State = "0";
            task.Validate = "v";
            task.OrderID = Request["OrderID"].ToString();
            task.Technology = Request["Technology"].ToString();//一次修改添加技术要求字段
            task.Note = Request["Note"].ToString();
            task.ID = Request["ID"].ToString();
            //task.HouseID = Request["HouseID"].ToString();

            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrOrderContent = Request["OrderContent"].Split(',');
            string[] arrSpecsModels = Request["SpecsModels"].Split(',');
            string[] arrOrderUnit = Request["OrderUnits"].Split(',');
            string[] arrOrderNum = Request["OrderNum"].Split(',');
            //string[] arrTechnology = Request["Technology"].Split(','); //一次修改注释技术要求和备注
            string[] arrDeliveryTime = Request["DeliveryTime"].Split(',');
            //string[] arrRemark = Request["Remarks"].Split(',');

            string strErr = "";
            tk_ProductTDatail deInfo = new tk_ProductTDatail();
            List<tk_ProductTDatail> detailList = new List<tk_ProductTDatail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_ProductTDatail();
                deInfo.RWID = Request["RWID"].ToString();
                deInfo.DID = ProTaskDetialNum(Request["RWID"].ToString());
                deInfo.PID = arrPID[i].ToString();
                deInfo.OrderContent = arrOrderContent[i].ToString();
                deInfo.SpecsModels = arrSpecsModels[i].ToString();
                deInfo.OrderUnit = arrOrderUnit[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                //deInfo.Technology = arrTechnology[i].ToString();
                deInfo.DeliveryTime = arrDeliveryTime[i].ToString();
                //deInfo.Remark = arrRemark[i].ToString();
                deInfo.State = "0";
                deInfo.Lstate = "0";
                deInfo.Sstate = "0";
                detailList.Add(deInfo);
            }
            var OrderID = Request["OrderID"].ToString();
            //string HouseID = Request["HouseID"].ToString();
            bool b = ProduceMan.SaveTask(task, detailList, ref strErr, OrderID);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["RWID"].ToString();
                    log.YYType = "添加成功 ";
                    log.Content = "新增任务单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["RWID"].ToString();
                    log.YYType = "添加失败 ";
                    log.Content = "新增任务单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
        }
        //审批信息
        public ActionResult SPXX()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string RWID = Request["RWID"].ToString();
            UIDataTable udtTask = ProduceMan.SPXX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RWID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPD()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getPD(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult getPDSP()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getPDSP(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region   生成领料单
        #region 判断是否提交审批
        public ActionResult selectPDTJ()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.selectPDTJ(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  生成领料单时的是否审批判断
        public ActionResult getSP()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getSP(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  生成领料单时的是否领料完成判断
        public ActionResult getLLSL()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getLLSL(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  生成领料单时的是否入库，生产随工单判断
        public ActionResult gettll()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.gettll(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        public ActionResult AddMaterial()
        {
            return View();
        }

        public ActionResult GetMaterial()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetMaterial(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult Getcount()
        {
            string PID = Request["PID"].ToString();
            string a = Request["a"].ToString();
            DataTable dt = ProduceMan.Getcount(PID, a);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetZZ()
        {
            string IdentitySharing = Request["IdentitySharing"].ToString();
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetZZ(IdentitySharing, PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetLLXQNum()
        {
            string IdentitySharing = Request["IdentitySharing"].ToString();
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetLLXQNum(IdentitySharing, PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetZSL()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetZSL(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetNULL()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetNULL(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult GetMT()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = ProduceMan.GetMT(PID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public string MaterialFDetailNum(string LLID)
        {
            return ProduceMan.MaterialFDetailNum(LLID);
        }

        public ActionResult AddLL()
        {
            tk_MaterialForm si = new TECOCITY_BGOI.tk_MaterialForm();
            si.LLID = ProduceMan.GetTopLLID();
            si.MaterialDepartment = GAccount.GetAccountInfo().UnitName;
            si.CreateUser = GAccount.GetAccountInfo().UserName;
            si.RWID = Request["RWID"].ToString();
            return View(si);
        }
        //任务单传的值
        public ActionResult GetMaterialForm()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetMaterialForm(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult getTnum()
        {
            string DID = Request["DID"].ToString();
            DataTable dt = ProduceMan.getTnum(DID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        //保存领料单
        public ActionResult SaveMaterialFDetailIn()
        {
            tk_MaterialForm Material = new tk_MaterialForm();
            Material.LLID = Request["LLID"].ToString();
            Material.RWID = Request["RWID"].ToString();
            Material.ID = Request["ID"].ToString();
            Material.MaterialDepartment = Request["MaterialDepartment"].ToString();
            Material.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            Material.OrderContent = Request["OrderContents"].ToString();
            Material.SpecsModels = Request["SpecsModelss"].ToString();
            Material.MaterialTime = Convert.ToDateTime(Request["MaterialTime"]);
            Material.CreateUser = Request["CreateUser"].ToString();
            Material.RWIDDID = Request["DID"].ToString();
            Material.State = "0";
            Material.Validate = "v";
            Material.Amount = Convert.ToInt32(Request["OrderNums"]);
            Material.materState = "0";
            Material.HouseID = Request["HouseID"].ToString();

            string[] arrMain = Request["MainContent"].Split('&');
            string[] arrPID = Request["PID"].Split('&');
            string[] arrOrderContent = Request["OrderContent"].Split('&');
            string[] arrSpecsModels = Request["SpecsModels"].Split('&');
            string[] arrManufacturer = Request["Manufacturer"].Split('&');
            string[] arrOrderUnit = Request["OrderUnit"].Split('&');
            string[] arrOrderNum = Request["OrderNum"].Split('&');
            string[] arrTechnology = Request["Technology"].Split('&');
            string[] arrRemark = Request["Remark"].Split('&');
            string[] arrIdentitySharing = Request["IdentitySharing"].Split('&');

            string strErr = "";
            tk_MaterialFDetail deInfo = new tk_MaterialFDetail();
            List<tk_MaterialFDetail> detailList = new List<tk_MaterialFDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_MaterialFDetail();
                deInfo.LLID = Request["LLID"].ToString();
                deInfo.DID = MaterialFDetailNum(Request["LLID"].ToString());
                deInfo.PID = arrPID[i].ToString();
                deInfo.OrderContent = arrOrderContent[i].ToString();
                deInfo.SpecsModels = arrSpecsModels[i].ToString();
                deInfo.Manufacturer = arrManufacturer[i].ToString();
                deInfo.OrderUnit = arrOrderUnit[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                deInfo.Technology = arrTechnology[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.Validate = "v";
                deInfo.IdentitySharing = arrIdentitySharing[i].ToString();
                detailList.Add(deInfo);
            }
            var LLID = Request["LLID"].ToString();
            var RWID = Request["RWID"].ToString();
            var RWIDDID = Request["DID"].ToString();
            var NCreateTime = DateTime.Now;
            var NCreateUser = GAccount.GetAccountInfo().UserName;
            var HouseID = Request["HouseID"].ToString();
            bool b = ProduceMan.SaveMaterialFDetailIn(Material, detailList, ref strErr, LLID, NCreateTime, NCreateUser, RWID, RWIDDID, HouseID);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["LLID"].ToString();
                    log.YYType = "添加成功 ";
                    log.Content = "新增领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["LLID"].ToString();
                    log.YYType = "添加失败 ";
                    log.Content = "新增领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
        }
        #endregion

        #region 生成随工单

        #region  生成随工单时的是否生成随工单完成判断
        public ActionResult getSGSL()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getSGSL(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  生成随工单时，领料单是否有此数据判断
        public ActionResult selectLL()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.selectLL(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  生成随工单时，判断是否需添加随工记录
        public ActionResult selectSGSL()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.selectSGSL(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region 生成随工单时判断是否已经入库
        public ActionResult gettsg()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.gettsg(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        public string ProductRProductNum(string SGID)
        {
            return ProduceMan.ProductRProductNum(SGID);
        }

        public ActionResult AddSG()
        {
            tk_ProductRecord si = new TECOCITY_BGOI.tk_ProductRecord();
            si.SGID = ProduceMan.GetTopSGID();
            si.CreateUser = GAccount.GetAccountInfo().UserName;
            si.RWID = Request["RWID"].ToString();
            ViewData["RWID"] = si.RWID;
            return View(si);
        }
        //提取任务单信息
        public ActionResult GetProductRecord()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetProductRecord(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult GetProductRecordDID()
        {
            string[] arrMain = Request["MainContent"].Split(',');
            string[] DID = Request["DID"].Split(',');
            tk_ProductRProduct deInfo = new tk_ProductRProduct();
            List<tk_ProductRProduct> detailList = new List<tk_ProductRProduct>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo.DID = DID[i].ToString();
                detailList.Add(deInfo);
            }
            DataTable dt = ProduceMan.GetProductRecordDID(deInfo.DID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

            }
            return null;
        }
        public ActionResult SaveProductRecordIn()
        {
            tk_ProductRecord ProductRecord = new tk_ProductRecord();
            ProductRecord.SGID = Request["SGID"].ToString();
            ProductRecord.RWID = Request["RWID"].ToString();
            ProductRecord.ID = Request["ID"].ToString();
            ProductRecord.billing = Convert.ToDateTime(Request["billing"]);
            ProductRecord.Remark = Request["Remark"].ToString();
            ProductRecord.State = "0";
            ProductRecord.Validate = "v";
            ProductRecord.CreateTime = Convert.ToDateTime(DateTime.Now);
            ProductRecord.CreateUser = Request["CreateUser"].ToString();

            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrOrderNums = Request["OrderNums"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrDID = Request["DID"].Split(',');

            string strErr = "";
            tk_ProductRProduct deInfo = new tk_ProductRProduct();
            List<tk_ProductRProduct> detailList = new List<tk_ProductRProduct>();
            for (int i = 0; i < arrMain.Length-1; i++)
            {
                deInfo = new tk_ProductRProduct();
                deInfo.SGID = Request["SGID"].ToString();
                deInfo.SGDID = ProductRProductNum(Request["SGID"].ToString());
                deInfo.PID = arrPID[i].ToString();
                deInfo.DID = arrDID[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNums[i]);
                detailList.Add(deInfo);
            }
            var RWID = Request["RWID"].ToString();
            var SGID = Request["SGID"].ToString();
            string HouseID = Request["HouseID"].ToString();
            bool b = ProduceMan.SaveProductRecordIn(ProductRecord, detailList, ref strErr, RWID, SGID, HouseID);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "生成随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "生成随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }
        #endregion

        #region 检验报告上传
        public ActionResult AddBG(HttpPostedFileBase sd)
        {
            tk_ReportInfo si = new TECOCITY_BGOI.tk_ReportInfo();
            si.BGID = ProduceMan.GetTopBGID();
            si.RWID = Request["RWID"].ToString();
            si.CreatePerson = GAccount.GetAccountInfo().UserName;
            string RWID = Request["RWID"].ToString();
            return View(si);
        }


        public ActionResult GetBGType()
        {
            DataTable dt = ProduceMan.GetBGType();
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult GetReportInfoselect()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetReportInfoselect(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult GetReportInfos()
        {
            string DID = Request["DID"].ToString();
            DataTable dt = ProduceMan.GetReportInfos(DID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public string ProFileInfoNum(string BGID)
        {
            return ProduceMan.ProFileInfoNum(BGID);
        }

        public ActionResult SaveFileInfoIn()
        {
            tk_ReportInfo task = new tk_ReportInfo();
            task.BGID = Request["BGID"].ToString();
            task.RWID = Request["RWID"].ToString();
            task.DID = Request["DID"].ToString();
            task.uploadtime = Convert.ToDateTime(Request["uploadtime"]);
            task.Remarks = Request["Remarks"].ToString();
            task.Validate = "v";
            task.CreatePerson = GAccount.GetAccountInfo().UserName;
            task.CreateTime = Convert.ToDateTime(DateTime.Now);





            string strErr = "";
            HttpFileCollectionBase files = Request.Files;
            tk_FileInfo deInfo = new tk_FileInfo();
            List<tk_FileInfo> detailList = new List<tk_FileInfo>();
            for (int i = 0; i < files.Count; i++)
            {
                deInfo = new tk_FileInfo();
                //上传文件

                HttpPostedFileBase file = files[i];
                byte[] fileByte = new byte[0];
                if (file.FileName != "")
                {
                    deInfo.FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    int fileLength = file.ContentLength;
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        file.InputStream.Read(fileByte, 0, fileLength);

                    }
                }
                deInfo.FileInfo = fileByte;
                deInfo.BGID = Request["BGID"].ToString();
                deInfo.DID = ProFileInfoNum(Request["BGID"].ToString());
                deInfo.CreatePerson = GAccount.GetAccountInfo().UserName;
                deInfo.Type = Request["Type" + i].ToString();
                deInfo.CreateTime = DateTime.Now;
                deInfo.Validate = "v";
                detailList.Add(deInfo);
            }
            var CreatePerson = GAccount.GetAccountInfo().UserName;
            var CreateTime = DateTime.Now;
            var BGID = Request["BGID"].ToString();
            bool b = ProduceMan.SaveFileInfoIn(task, detailList, ref strErr, BGID, CreatePerson, CreateTime);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "检验报告上传";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                ViewData["msg"] = "保存成功";
                return View("AddBG", task);
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "检验报告上传";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                ViewData["msg"] = "保存失败";
                return View("AddBG", task);
            }
        }
        #endregion

        #region 完成入库

        #region  完成入库单时，判断随工单是否生产完成
        public ActionResult selectSG()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.selectSG(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region  产品入库时，判断此产品是否已经完成入库
        public ActionResult selectRK()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.selectRK(RWID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        public ActionResult AddRK()
        {
            tk_PStocking si = new TECOCITY_BGOI.tk_PStocking();
            si.RKID = ProduceMan.GetTopRKID();
            si.StockInUser = GAccount.GetAccountInfo().UserName;
            si.RWID = Request["RWID"].ToString();
            return View(si);
        }

        public string PStockingDetailNum(string RKID)
        {
            return ProduceMan.PStockingDetailNum(RKID);
        }

        public ActionResult SavePStockingDetailIn()
        {
            tk_PStocking task = new tk_PStocking();
            task.RKID = Request["RKID"].ToString();
            task.RWID = Request["RWID"].ToString();
            task.StockInTime = Convert.ToDateTime(Request["StockInTime"]);
            task.FinishTime = Convert.ToDateTime(Request["FinishTime"]);
            task.HouseID = Request["HouseID"].ToString();
            task.Batch = Request["Batch"].ToString();
            task.StockRemark = Request["StockRemark"].ToString();
            task.StockInUser = Request["StockInUser"].ToString();
            task.State = "0";
            task.MaterState = "0";
            task.Type = "生产组装入库";
            task.Validate = "v";
            task.UnitID = GAccount.GetAccountInfo().UnitID;
            task.CreateUser = GAccount.GetAccountInfo().UserName;
            task.CreateTime = Convert.ToDateTime(DateTime.Now);
            task.Storekeeper = Request["Storekeeper"].ToString();


            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrOrderContent = Request["OrderContent"].Split(',');
            string[] arrSpecsModels = Request["SpecsModels"].Split(',');
            string[] arrOrderUnit = Request["OrderUnit"].Split(',');
            string[] arrOrderNum = Request["OrderNum"].Split(',');
            string[] arrRemarks = Request["Remarks"].Split(',');
            string[] arrDID = Request["DID"].Split(',');

            var a = "";
            string strErr = "";
            tk_PStockingDetail deInfo = new tk_PStockingDetail();
            List<tk_PStockingDetail> detailList = new List<tk_PStockingDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_PStockingDetail();
                deInfo.RKID = Request["RKID"].ToString();
                deInfo.DID = PStockingDetailNum(Request["RKID"].ToString());
                deInfo.PID = arrPID[i].ToString();
                deInfo.OrderContent = arrOrderContent[i].ToString();
                deInfo.Specifications = arrSpecsModels[i].ToString();
                deInfo.Unit = arrOrderUnit[i].ToString();
                deInfo.Amount = Convert.ToInt32(arrOrderNum[i]);
                deInfo.Remark = arrRemarks[i].ToString();
                deInfo.RWIDID = arrDID[i].ToString();
                detailList.Add(deInfo);
            }
            var RWID = Request["RWID"].ToString();

            var FinishTime = Convert.ToDateTime(Request["FinishTime"]);
            bool b = ProduceMan.SavePStockingDetailIn(task, detailList, ref strErr, RWID, FinishTime);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "完成入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "完成入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult GetReportInfo()
        {
            string RWID = Request["RWID"].ToString();
            string RKID = Request["RKID"].ToString();
            DataTable dt = ProduceMan.GetReportInfo(RWID, RKID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion

        #region 修改任务单
        public ActionResult addupdatetask()
        {
            return View();
        }

        public ActionResult ChangeT()
        {
            return View();
        }
        public ActionResult getupdate()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.getupdate(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SCTask()
        {
            string strErr = "";
            string DID = Request["DID"].ToString();
            if (ProduceMan.SCTask(DID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除成功 ";
                log.Content = "删除任务单产品";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除失败 ";
                log.Content = "删除任务单产品";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        public ActionResult UpdateTask()
        {
            string RWID = Request["RWID"].ToString();
            tk_ProductTask a = new tk_ProductTask();
            a.RWID = RWID;
            DataTable task = ProduceMan.IndexAllTask(RWID);
            tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
            foreach (DataRow dt in task.Rows)
            {
                so.RWID = dt["RWID"].ToString();
                so.Clientcode = dt["Clientcode"].ToString();
                so.OrderUnit = dt["OrderUnit"].ToString();
                so.OrderAddress = dt["OrderAddress"].ToString();
                so.OrderContactor = dt["OrderContactor"].ToString();
                so.OrderTel = dt["OrderTel"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.ContractDate = Convert.ToDateTime(dt["ContractDate"]);
                ViewData["ContractDate"] = so.ContractDate.ToShortDateString();
                so.Technology = dt["Technology"].ToString();
                so.Note = dt["Note"].ToString();
                so.HouseID = dt["HouseID"].ToString();
            }
            return View(so);
        }

        public ActionResult GetTaskDetailss()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetTaskDetailss(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveUpdateTask()
        {
            tk_ProductTask Material = new tk_ProductTask();
            Material.RWID = Request["RWID"].ToString();
            Material.Clientcode = Request["Clientcode"].ToString();
            Material.OrderUnit = Request["OrderUnit"].ToString();
            Material.OrderAddress = Request["OrderAddress"].ToString();
            Material.OrderContactor = Request["OrderContactor"].ToString();
            Material.OrderTel = Request["OrderTel"].ToString();
            Material.Remark = Request["Remark"].ToString();
            Material.ContractDate = Convert.ToDateTime(Request["ContractDate"]);
            Material.Technology = Request["Technology"].ToString();
            Material.Note = Request["Note"].ToString();
            //Material.HouseID = Request["HouseID"].ToString();


            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrOrderContent = Request["OrderContent"].Split(',');
            string[] arrSpecsModels = Request["SpecsModels"].Split(',');
            string[] arrOrderUnits = Request["OrderUnits"].Split(',');
            string[] arrOrderNum = Request["OrderNum"].Split(',');
            //string[] arrTechnology = Request["Technology"].Split(',');
            string[] arrDeliveryTime = Request["DeliveryTime"].Split(',');
            //string[] arrRemarks = Request["Remarks"].Split(',');


            string strErr = "";
            tk_ProductTDatail deInfo = new tk_ProductTDatail();
            List<tk_ProductTDatail> detailList = new List<tk_ProductTDatail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_ProductTDatail();
                deInfo.PID = arrPID[i].ToString();
                deInfo.OrderContent = arrOrderContent[i].ToString();
                deInfo.SpecsModels = arrSpecsModels[i].ToString();
                deInfo.OrderUnit = arrOrderUnits[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                //deInfo.Technology = arrTechnology[i].ToString();
                deInfo.DeliveryTime = arrDeliveryTime[i].ToString();
                //deInfo.Remark = arrRemarks[i].ToString();
                deInfo.RWID = Request["RWID"].ToString();
                deInfo.DID = ProTaskDetialNum(Request["RWID"].ToString()); ;
                deInfo.State = "0";
                deInfo.Lstate = "0";
                detailList.Add(deInfo);
            }
            var RWID = Request["RWID"].ToString();
            bool b = ProduceMan.SaveUpdateTask(Material, detailList, ref strErr, RWID);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["RWID"].ToString();
                    log.YYType = "修改成功 ";
                    log.Content = "修改领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["RWID"].ToString();
                    log.YYType = "修改失败 ";
                    log.Content = "修改领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
        }
        #endregion

        #region 撤销
        public ActionResult CheXiaoTask()
        {
            string strErr = "";
            string RWID = Request["RWID"].ToString();

            if (ProduceMan.CheXiaoTask(RWID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RWID"].ToString();
                log.YYType = "撤销成功 ";
                log.Content = "撤销任务单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RWID"].ToString();
                log.YYType = "撤销失败 ";
                log.Content = "撤销任务单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 打印

        public ActionResult PrintTask()
        {

            string RWID = Request["Info"];
            string PID = Request["PID"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(RWID))
            {
                s += " RWID like '%" + RWID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_Produce.dbo.tk_ProductTask ";
            DataTable data = ProduceMan.PrintTasks(where, tableName, ref strErr);
            tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
            foreach (DataRow dt in data.Rows)
            {
                so.RWID = dt["RWID"].ToString();
                so.ContractDate = Convert.ToDateTime(dt["ContractDate"]);
                ViewData["ContractDate"] = so.ContractDate.ToShortDateString();
                so.Clientcode = dt["Clientcode"].ToString();
                so.OrderUnit = dt["OrderUnit"].ToString();
                so.OrderAddress = dt["OrderAddress"].ToString();
                so.OrderContactor = dt["OrderContactor"].ToString();
                so.OrderTel = dt["OrderTel"].ToString();
                so.OrderContactor = dt["OrderContactor"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = dt["CreateUser"].ToString();
                so.ID = dt["ID"].ToString();
                so.Technology = dt["Technology"].ToString();
                so.Note = dt["Note"].ToString();
            }
            DataTable a = ProduceMan.PrintTask(RWID);
            DataTable z = ProduceMan.GetApprovalTime(PID);
            string aa;
            string bb;
            string cc;
            string dd;
            if (z.Rows.Count == 0)
            {
                aa = "";
                bb = "";
                cc = "";
                dd = "";
            }
            else
            {
                aa = z.Rows[0]["UserName"].ToString();
                bb = z.Rows[1]["UserName"].ToString();
                cc = z.Rows[0]["ApprovalTime"].ToString();
                dd = z.Rows[1]["ApprovalTime"].ToString();
            }
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>" +
                "<div><div style='font-size: 18px;margin-left:15%;float: left'>北京市燕山工业燃气设备有限公司</div>" +
                "<div style='font-size: 22px; font-weight: bold; float: right; margin-right: 20%;'>生产任务单</div></div>" +
            "<div><div style='margin-left: 5%; float: left;'>编号：" + so.ID + "</div><div style='margin-left: 2%; float: left;'>开单日期：" + ViewData["ContractDate"] + "</div> <div style='margin-left:5%;float:left;' >YSGLJ—SC—F13</div><div style='margin-left:5%;float:left;' >单号：" + so.RWID + "</div></div>" +
           " <table id='search' class='tabInfo2' style='text-align: left;'><tr><td colspan='2' width:'20%'>客户代码:</td><td colspan='5' width:'40%'>订货单位:" + so.OrderUnit + "</td><td colspan='5' width:'40%'>电话:" + so.OrderTel + "</td></tr><tr><td  colspan='2' width:'20%' ></td><td colspan='5' width:'40%'>地址:" + so.OrderAddress + "</td><td colspan='5' width:'40%'>联系人:" + so.OrderContactor + "</td> </tr>");

            sb2.Append(" <tr ><td  colspan='4' width:'33'>编制:" + GAccount.GetAccountInfo().UserName + "</td><td  colspan='4' width:'33%'>审核:" + aa + "</td><td  colspan='4' width:'33%'>实施:" + bb + "</td></tr><tr ><td  colspan='4' width:'33%'>日期:</td><td  colspan='4' width:'33%'>日期:" + cc + "</td><td  colspan='4' width:'33%'>日期:" + dd + "</td> </tr> </table>");
            if (a.Rows.Count <= 6)
            {
                var d = 0;
                sb1.Append(" <tr align='center' valign='middle'> <td style='width: 10%;' class='th'>产品编码 </td> <td colspan='2' style='width: 15%; text-align: center;' class='th'>产品名称</td><td  colspan='2'style='width: 15%; text-align: center;' class='th'>规格型号</td><td  style='width: 5%;' class='th'>数量</td><td style='width: 10%;' class='th'>单位</td><td colspan='3' style='width: 20%;' class='th'>技术要求及参数</td> <td  style='width: 10%;' class='th'>完成日期 </td><td colspan='2' style='width: 15%;' class='th'>备注</td></tr> <tbody id='DetailInfo' class='tabInfoP'></tbody>");
                for (int i = 0; i < a.Rows.Count + 5; i++)
                {
                    var m = Convert.ToInt32(a.Rows.Count) + 5;

                    if (i == 0)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labPID" + i + " ' id='PID" + i + "'>" + a.Rows[i]["PID"] + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'>" + a.Rows[i]["OrderContent"] + "</lable> </td> <td colspan='2' ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'>" + a.Rows[i]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'>" + a.Rows[i]["OrderNum"] + "</lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'>" + a.Rows[i]["OrderUnit"] + "</lable></td> <td colspan='3' rowspan='" + m + "'><lable class='labTechnology" + i + "  id='Technology" + i + "'>" + so.Technology + "</lable></td> <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["a"] + "</lable></td> <td colspan='2' rowspan='" + m + "'><lable class='labRemark" + i + "  id='Remark" + i + "'>" + so.Note + "</lable></td></tr>");
                        d = d + Convert.ToInt32(a.Rows[i]["OrderNum"]);
                    }
                    if (i < a.Rows.Count && i > 0)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labPID" + i + " ' id='PID" + i + "'>" + a.Rows[i]["PID"] + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'>" + a.Rows[i]["OrderContent"] + "</lable> </td> <td colspan='2' ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'>" + a.Rows[i]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'>" + a.Rows[i]["OrderNum"] + "</lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'>" + a.Rows[i]["OrderUnit"] + "</lable></td>  <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["a"] + "</lable></td> </tr>");
                        d = d + Convert.ToInt32(a.Rows[i]["OrderNum"]);
                    }
                    if (i < a.Rows.Count + 4 && i > a.Rows.Count - 1)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labPID" + i + " ' id='PID" + i + "'></lable> </td> <td colspan='2' ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'></lable> </td> <td colspan='2' ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'></lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'></lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'></lable></td>  <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td> </tr>");
                    }
                    if (i < a.Rows.Count + 5 && i > a.Rows.Count + 3)
                    {
                        var n = "合计";
                        sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labPID" + i + " ' id='PID" + i + "'>" + n + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'></lable> </td> <td colspan='2' ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'></lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'>" + d + "</lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'></lable></td>  <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td> </tr>");
                    }
                }

                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                var d = 0;
                int count = a.Rows.Count % 6;
                if (count > 0)
                    count = a.Rows.Count / 6 + 1;
                else
                    count = a.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 6 * i + a.Rows.Count % 6;
                    sb1.Append("  <tr align='center' valign='middle'> <td style='width: 10%;' class='th'>产品编码 </td> <td colspan='2' style='width: 15%; text-align: center;' class='th'>产品名称</td><td  colspan='2'style='width: 15%; text-align: center;' class='th'>规格型号</td><td  style='width: 5%;' class='th'>数量</td><td style='width: 10%;' class='th'>单位</td><td colspan='3' style='width: 20%;' class='th'>技术要求及参数</td> <td  style='width: 10%;' class='th'>完成日期 </td><td colspan='2' style='width: 15%;' class='th'>备注</td></tr> <tbody id='DetailInfo' class='tabInfoP'></tbody>");
                    for (int j = b; j < length + 5; j++)
                    {
                        var m = Convert.ToInt32(length) + 5;

                        if (j == 0)
                        {
                            sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labPID" + j + " ' id='PID" + j + "'>" + a.Rows[j]["PID"] + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'>" + a.Rows[j]["OrderContent"] + "</lable> </td> <td colspan='2' ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'>" + a.Rows[j]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + j + "  id='OrderNum" + j + "'>" + a.Rows[j]["OrderNum"] + "</lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'>" + a.Rows[j]["OrderUnit"] + "</lable></td> <td colspan='3' rowspan='" + m + "'><lable class='labTechnology" + j + "  id='Technology" + j + "'>" + so.Technology + "</lable></td> <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["a"] + "</lable></td> <td colspan='2' rowspan='" + m + "'><lable class='labRemark" + j + "  id='Remark" + j + "'>" + so.Note + "</lable></td></tr>");
                            d = d + Convert.ToInt32(a.Rows[j]["OrderNum"]);
                        }
                        if (j < length && j > 0)
                        {
                            sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labPID" + j + " ' id='PID" + j + "'>" + a.Rows[j]["PID"] + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'>" + a.Rows[j]["OrderContent"] + "</lable> </td> <td colspan='2' ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'>" + a.Rows[j]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + j + "  id='OrderNum" + j + "'>" + a.Rows[j]["OrderNum"] + "</lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'>" + a.Rows[j]["OrderUnit"] + "</lable></td>  <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["a"] + "</lable></td> </tr>");
                            d = d + Convert.ToInt32(a.Rows[j]["OrderNum"]);
                        }
                        if (j < length + 4 && j > length - 1)
                        {
                            sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labPID" + j + " ' id='PID" + j + "'></lable> </td> <td colspan='2' ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'></lable> </td> <td colspan='2' ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'></lable></td> <td ><lable class='labOrderNum" + j + "  id='OrderNum" + j + "'></lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'></lable></td>  <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'></lable></td> </tr>");
                        }
                        if (j < length + 5 && j > length + 3)
                        {
                            var n = "合计";
                            sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labPID" + j + " ' id='PID" + j + "'>" + n + "</lable> </td> <td colspan='2' ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'></lable> </td> <td colspan='2' ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'></lable></td> <td ><lable class='labOrderNum" + j + "  id='OrderNum" + j + "'>" + d + "</lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'></lable></td>  <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'></lable></td> </tr>");
                        }
                    }
                    if ((length - b) < 6)
                    {
                    }
                    //sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }

            Response.Write(html);
            return View();
        }
        #endregion

        #region 任务单详情
        public ActionResult TaskDetail()
        {
            string RWID = Request["RWID"].ToString();
            tk_ProductTask a = new tk_ProductTask();
            a.RWID = RWID;
            tk_ProductTask task = ProduceMan.ProTaskDetails(RWID);
            ViewData["ContractDate"] = task.ContractDate.ToShortDateString();
            return View(task);

        }

        public ActionResult ProTaskDetail()
        {
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetTaskDetailss(RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion

      
        #endregion

        #region 领料单管理
        public ActionResult Materialrequisition()
        {
            ViewData["webkey"] = "领料单审批";
            string fold = COM_ApprovalMan.getNewwebkey("领料单审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        #region 领料单列表显示

        //领料单列表查询
        public ActionResult Materialrequisitionlist(ProduceList LL)
        {
            if (ModelState.IsValid)
            {
                string where = " where c.Validate='v' and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string OrderUnit = LL.OrderUnit;
                string OrderContent = LL.OrderContent;
                string SpecsModels = LL.SpecsModels;

                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";

                if (Request["OrderUnit"] != "")
                    where += " d.OrderUnit like '%" + OrderUnit + "%' and";
                if (Request["OrderContent"] != "")
                    where += "  c.OrderContent like '%" + OrderContent + "%' and";
                if (Request["SpecsModels"] != "")
                    where += "  c.SpecsModels like '%" + SpecsModels + "%' and";
                if (Starts != "" && Starte != "")
                    where += " c.MaterialTime between '" + Starts + "' and '" + Starte + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProduceMan.Materialrequisitionlist(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        //领料单祥表
        public ActionResult ProMaterialFDetail()
        {
            string LLID = Request["LLID"].ToString();
            DataTable dt = ProduceMan.ProMaterialFDetail(LLID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SCLLDetail()
        {
            string strErr = "";
            string DID = Request["DID"].ToString();
            if (ProduceMan.SCLLDetail(DID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除成功 ";
                log.Content = "删除领料单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除失败 ";
                log.Content = "删除领料单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region  领料单页面  新增领料单
        public ActionResult Addnewll()
        {
            tk_MaterialForm si = new TECOCITY_BGOI.tk_MaterialForm();
            si.LLID = ProduceMan.GetTopLLID();
            si.ID = ProduceMan.GetTopLID();
            si.MaterialDepartment = GAccount.GetAccountInfo().UnitName;
            si.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(si);
        }

        public ActionResult savesomematerial()
        {
            tk_MaterialForm Material = new tk_MaterialForm();
            Material.LLID = ProduceMan.GetTopLLID();
            Material.ID = Request["ID"].ToString();
            Material.MaterialDepartment = Request["MaterialDepartment"].ToString();
            Material.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            Material.OrderContent = Request["OrderContents"].ToString();
            Material.SpecsModels = Request["SpecsModelss"].ToString();
            Material.MaterialTime = Convert.ToDateTime(Request["MaterialTime"]);
            Material.CreateUser = Request["CreateUser"].ToString();
            Material.Remark = Request["remarks"].ToString();
            Material.Amount = Convert.ToInt32(Request["orderNums"]);
            Material.State = "0";
            Material.Validate = "v";
            var llID = Material.LLID;
            Material.RWIDDID = ProduceMan.GetTopID(llID);





            var PID = Request["pIDs"].ToString();
            var Main = Request["mainContent"].ToString();
            var LLID = ProduceMan.GetTopLLID();
            var amount = Request["orderNums"].ToString();
            var v = "v";
            var OrderContent = Request["orderContent"].ToString();
            var SpecsModels = Request["specsModels"].ToString();
            var Remark = Request["remarks"].ToString();
            var orderUnit = Request["orderUnit"].ToString();
            var DID = ProduceMan.GetTopID(LLID);

            string strErr = "";

            bool b = ProduceMan.savesomematerial(Material, PID, Main, LLID, amount, v, OrderContent, SpecsModels, Remark, orderUnit, DID);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "添加领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "添加领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult gettask()
        {
            return View();
        }

        public ActionResult gettaskll()
        {
            string where = "  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";


            string OrderContent = Request["OrderContent"].ToString();

            if (OrderContent != "")
                where += "  ProName like '%" + OrderContent + "%' and";

            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = ProduceMan.gettaskll(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改领料单信息
        public ActionResult GetTaskNum()
        {
            string LLID = Request["LLID"].ToString();
            string RWIDDID = Request["RWIDDID"].ToString();
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.GetTaskNum(LLID, RWIDDID, RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult GetMaterialFormTaskdetail()
        {
            string LLID = Request["LLID"].ToString();
            DataTable dt = ProduceMan.GetMaterialFormTaskdetail(LLID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        public ActionResult UpdateMaterialForm()
        {
            string LLID = Request["LLID"].ToString();
            tk_MaterialForm a = new tk_MaterialForm();
            a.LLID = LLID;
            DataTable task = ProduceMan.IndexAllMaterialForm(LLID);
            foreach (DataRow dt in task.Rows)
            {
                a.RWID = dt["RWID"].ToString();
                a.LLID = dt["LLID"].ToString();
                a.ID = dt["ID"].ToString();
                a.MaterialDepartment = dt["MaterialDepartment"].ToString();
                a.CreateTime = Convert.ToDateTime(dt["CreateTime"].ToString());
                ViewData["CreateTime"] = a.CreateTime.ToShortDateString();
                a.OrderContent = dt["OrderContent"].ToString();
                a.SpecsModels = dt["SpecsModels"].ToString();
                a.MaterialTime = Convert.ToDateTime(dt["MaterialTime"]);
                ViewData["MaterialTime"] = a.MaterialTime.ToShortDateString();
                a.Amount = Convert.ToInt32(dt["Amount"].ToString());
                a.RWIDDID = dt["RWIDDID"].ToString();
                a.HouseID = dt["HouseID"].ToString();
            }
            return View(a);
        }

        public ActionResult GetMaterialFormDetail()
        {
            string LLID = Request["LLID"].ToString();
            DataTable dt = ProduceMan.GetMaterialFormDetail(LLID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveUpdateMaterialTask()
        {
            tk_MaterialForm Material = new tk_MaterialForm();
            Material.LLID = Request["LLID"].ToString();
            Material.ID = Request["ID"].ToString();
            Material.MaterialDepartment = Request["MaterialDepartment"].ToString();
            Material.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            Material.OrderContent = Request["OrderContent"].ToString();
            Material.SpecsModels = Request["SpecsModels"].ToString();
            Material.MaterialTime = Convert.ToDateTime(Request["MaterialTime"]);
            Material.Amount = Convert.ToInt32(Request["Amount"]);


            var RWIDDID = Request["RWIDDID"].ToString();
            var OrderNum = Request["Amount"].ToString();


            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrDID = Request["DID"].Split(',');
            string[] arrOrderContents = Request["OrderContents"].Split(',');
            string[] arrSpecsModelss = Request["SpecsModelss"].Split(',');
            //string[] arrSpecifications = Request["Specifications"].Split(',');
            string[] arrManufacturer = Request["Manufacturer"].Split(',');
            string[] arrOrderUnit = Request["OrderUnit"].Split(',');
            string[] arrOrderNum = Request["OrderNum"].Split(',');
            string[] arrTechnology = Request["Technology"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');

            string strErr = "";
            tk_MaterialFDetail deInfo = new tk_MaterialFDetail();
            List<tk_MaterialFDetail> detailList = new List<tk_MaterialFDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_MaterialFDetail();
                deInfo.DID = arrDID[i].ToString();
                deInfo.OrderContent = arrOrderContents[i].ToString();
                deInfo.SpecsModels = arrSpecsModelss[i].ToString();
                //deInfo.Specifications = arrSpecifications[i].ToString();
                deInfo.Manufacturer = arrManufacturer[i].ToString();
                deInfo.OrderUnit = arrOrderUnit[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                deInfo.Technology = arrTechnology[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.Validate = "v";
                detailList.Add(deInfo);
            }
            var LLID = Request["LLID"].ToString();
            bool b = ProduceMan.SaveUpdateMaterialTask(Material, detailList, ref strErr, LLID, RWIDDID, OrderNum);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "修改成功 ";
                log.Content = "修改领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "修改失败 ";
                log.Content = "修改领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult SaveUpdateMaterialForm()
        {
            tk_MaterialForm Material = new tk_MaterialForm();
            Material.LLID = Request["LLID"].ToString();
            Material.RWID = Request["RWID"].ToString();
            Material.ID = Request["ID"].ToString();
            Material.MaterialDepartment = Request["MaterialDepartment"].ToString();
            Material.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
            Material.OrderContent = Request["OrderContent"].ToString();
            Material.SpecsModels = Request["SpecsModels"].ToString();
            Material.MaterialTime = Convert.ToDateTime(Request["MaterialTime"]);
            Material.Amount = Convert.ToInt32(Request["Amount"]);
            Material.HouseID = Request["HouseID"].ToString();

            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrPID = Request["PID"].Split(',');
            string[] arrOrderContents = Request["OrderContents"].Split(',');
            string[] arrSpecsModelss = Request["SpecsModelss"].Split(',');
            //string[] arrSpecifications = Request["Specifications"].Split(',');
            string[] arrManufacturer = Request["Manufacturer"].Split(',');
            string[] arrOrderUnit = Request["OrderUnit"].Split(',');
            string[] arrOrderNum = Request["OrderNum"].Split(',');
            string[] arrTechnology = Request["Technology"].Split(',');
            string[] arrRemark = Request["Remark"].Split(',');
            string[] arrIdentitySharing = Request["IdentitySharing"].Split(',');

            string strErr = "";
            tk_MaterialFDetail deInfo = new tk_MaterialFDetail();
            List<tk_MaterialFDetail> detailList = new List<tk_MaterialFDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_MaterialFDetail();
                deInfo.PID = arrPID[i].ToString();
                deInfo.OrderContent = arrOrderContents[i].ToString();
                deInfo.SpecsModels = arrSpecsModelss[i].ToString();
                //deInfo.Specifications = arrSpecifications[i].ToString();
                deInfo.Manufacturer = arrManufacturer[i].ToString();
                deInfo.OrderUnit = arrOrderUnit[i].ToString();
                deInfo.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                deInfo.Technology = arrTechnology[i].ToString();
                deInfo.Remark = arrRemark[i].ToString();
                deInfo.IdentitySharing = arrIdentitySharing[i].ToString();
                detailList.Add(deInfo);
            }
            var LLID = Request["LLID"].ToString();
            var RWID = Request["RWID"].ToString();
            bool b = ProduceMan.SaveUpdateMaterialForm(Material, detailList, ref strErr, LLID, RWID);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (b)
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["LLID"].ToString();
                    log.YYType = "修改成功 ";
                    log.Content = "修改领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["LLID"].ToString();
                    log.YYType = "修改失败 ";
                    log.Content = "修改领料单";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
        }
        #endregion

        #region 领料单详情
        public ActionResult MaterialFormDetail()
        {
            string LLID = Request["LLID"].ToString();
            tk_MaterialForm a = new tk_MaterialForm();
            a.LLID = LLID;
            DataTable task = ProduceMan.IndexAllMaterialForms(LLID);
            foreach (DataRow dt in task.Rows)
            {
                a.RWID = dt["RWID"].ToString();
                a.LLID = dt["LLID"].ToString();
                a.ID = dt["ID"].ToString();
                a.MaterialDepartment = dt["MaterialDepartment"].ToString();
                a.CreateTime = Convert.ToDateTime(dt["CreateTime"].ToString());
                ViewData["CreateTime"] = a.CreateTime.ToShortDateString();
                a.OrderContent = dt["OrderContent"].ToString();
                a.SpecsModels = dt["SpecsModels"].ToString();
                a.MaterialTime = Convert.ToDateTime(dt["MaterialTime"]);
                ViewData["MaterialTime"] = a.MaterialTime.ToShortDateString();
                a.Amount = Convert.ToInt32(dt["Amount"].ToString());
                a.RWIDDID = dt["RWIDDID"].ToString();
            }
            return View(a);
        }
        public ActionResult GetMaterialFormDetails()
        {
            string LLID = Request["LLID"].ToString();
            DataTable dt = ProduceMan.GetMaterialFormDetails(LLID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion

        #region 撤销
        public ActionResult CXLL()
        {
            string strErr = "";
            string LLID = Request["LLID"].ToString();
            string RWID = Request["RWID"].ToString();

            if (ProduceMan.CXLL(LLID, ref strErr, RWID))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "撤销成功 ";
                log.Content = "撤销领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["LLID"].ToString();
                log.YYType = "撤销失败 ";
                log.Content = "撤销领料单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 相关单据
        public ActionResult LoadLL()
        {
            string strErr = "";
            string a = Request["ID"].ToString();
            string ID = a.Substring(0, 2);
            if (ID == "LL")
            {
                DataTable data = ProduceMan.GetLL(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["ID"] = dt["ID"].ToString();
                    ViewData["MaterialDepartment"] = dt["MaterialDepartment"].ToString();
                    ViewData["CreateTime"] = dt["CreateTime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["Amount"] = dt["Amount"].ToString();
                    ViewData["Remark"] = dt["Remark"].ToString();
                    ViewData["z"] = dt["m"].ToString();
                    ViewData["n"] = dt["n"].ToString();
                    ViewData["MaterialTime"] = dt["MaterialTime"].ToString();
                    ViewData["m"] = "领料单编号：" + a + "";
                }
            }
            return View();
        }

        public ActionResult LoadLLs()
        {
            string LLID = Request["LLID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.LoadLLs(LLID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //点击详情后弹出页面
        public ActionResult LoadLLXG()
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
            string LLID = Request["LLID"];
            if (!string.IsNullOrEmpty(LLID)) { where = " where LLID='" + LLID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadLLXG(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadLLXGDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string LLID = Request["LLID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadLLXGDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, LLID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提交审批
        public ActionResult getTJLL()
        {
            string LLID = Request["LLID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.getTJLL(LLID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region 审批判断
        public ActionResult getPDLLSP()
        {
            string LLID = Request["LLID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.getPDLLSP(LLID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion

        #region 上传
        public ActionResult SCMaterialForm(string OID, string ID)
        {
            ViewData["OID"] = OID;
            ViewData["ID"] = ID;
            ViewData["msg"] = "";
            return View();
        }


        public ActionResult InsertFile()
        {
            string ID = Request["ID"].ToString();
            string OId = Request["OID"].ToString();
            Acc_Account account = GAccount.GetAccountInfo();
            try
            {
                HttpFileCollectionBase files = Request.Files;
                List<ProduceFile> list = new List<ProduceFile>();
                ProduceFile operationFile = null;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    operationFile = new ProduceFile()
                    {
                        FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1),
                        OID = Request["OId"].ToString(),
                        CreateUser = account.UserName.ToString(),
                        CreateTime = DateTime.Now.ToString(),
                        Validate = "v"
                    };

                    if (file.FileName != "")
                    {
                        var PathName = Path.Combine(Request.MapPath("~/Upload/Operation/"));
                        string FileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + operationFile.FileName.Substring(operationFile.FileName.LastIndexOf('.') + 1);
                        if (!Directory.Exists(PathName))
                        {
                            Directory.CreateDirectory(PathName);
                            PathName += FileName;
                        }
                        else
                        {
                            PathName += FileName;
                        }
                        file.SaveAs(PathName);
                        operationFile.FilePath = "Upload/Operation/" + FileName;

                    }

                    list.Add(operationFile);
                }
                if (ProduceMan.InsertFile(list))
                {
                    tk_ProLog log = new tk_ProLog();
                    log.LogTime = DateTime.Now;
                    log.YYCode = Request["OID"].ToString();
                    log.YYType = "添加成功 ";
                    log.Content = "添加文件";
                    log.Actor = GAccount.GetAccountInfo().UserName;
                    log.Unit = GAccount.GetAccountInfo().UnitName;
                    ProduceMan.AddProduceLog(log);
                    ViewData["msg"] = "保存成功！";

                    ViewData["OID"] = OId;
                    ViewData["ID"] = ID;
                    return View("SCMaterialForm");

                }
                else
                {
                    ViewData["msg"] = "保存失败！";
                    ViewData["OID"] = OId;
                    ViewData["ID"] = ID;
                    return View("SCMaterialForm");
                }

            }
            catch (Exception e)
            {
                ViewData["msg"] = "保存失败！" + e.Message;
                ViewData["OID"] = OId;
                ViewData["ID"] = ID;
                return View("SCMaterialForm");

            }

        }
        #endregion


        #region 查看
        public ActionResult CKMaterialForm(string OId)
        {
            ViewData["OId"] = OId;
            return View();
        }

        public ActionResult GetFiles(string OId)
        {
            string json = GFun.Dt2Json("", ProduceMan.GetFiles(OId));
            return Json(json);
        }
        #endregion

        #region 打印
        public ActionResult PrintLL()
        {

            string LLID = Request["Info"];
            DataTable data = ProduceMan.PrintLL(LLID);
            tk_MaterialForm so = new TECOCITY_BGOI.tk_MaterialForm();
            foreach (DataRow dt in data.Rows)
            {
                so.LLID = dt["LLID"].ToString();
                so.MaterialDepartment = dt["MaterialDepartment"].ToString();
                so.ID = dt["ID"].ToString();
                so.Amount = Convert.ToInt32(dt["Amount"]);
                so.OrderContent = dt["OrderContent"].ToString();
                so.SpecsModels = dt["SpecsModels"].ToString();
                so.MaterialTime = Convert.ToDateTime(dt["MaterialTime"]);
                ViewData["OrderContent"] = dt["m"].ToString();
                ViewData["SpecsModels"] = dt["n"].ToString();
            }


            //if (OrdersInfo.ContractID == "")
            //{
            //    string Str = SalesManage.GetNamePY(GAccount.GetAccountInfo().UserName);
            //    string Dime = DateTime.Now.Year.ToString();// ("YYYY");
            //    Dime = Dime.Substring(2, 2);
            //    string MaxContractID = SalesManage.GetMaxContractID();
            //    MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);
            //    MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
            //    OrdersInfo.ContractID = Str + "-" + Dime + "-" + MaxContractID;
            //}
            DataTable a = ProduceMan.PrintLLs(LLID);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'><div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司</div><div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>领料单</div><br /><div><div style='font-weight: bold; float:right; margin-right:5%' >YSGLJL-SC-F16</div></div> <br/><div><div style='font-weight: bold; float:left;display:inline;margin-left:3%;'>领料部门:" + so.MaterialDepartment + "</div> <div style='font-weight: bold; float:left;display:inline;padding-left:5%;'>编号:" + so.ID + "</div><div style='font-weight: bold; float:left;display:inline;padding-left:5%;' >产品名称:" + ViewData["OrderContent"] + "</div><div style='font-weight: bold; float:left;display:inline;padding-left:6%;' >规格型号:" + ViewData["SpecsModels"] + "</div> <div style='font-weight: bold; float:right;display:inline;padding-right:2%' >共:" + so.Amount + "套</div></div>");

            sb2.Append("<tr ><td colspan='3'  width:'33%'>领料人:</td><td colspan='4' width:'33%'>库管员:</td><td colspan='4'  width:'33%'>日期:" + so.MaterialTime.ToString("yyyy年MM月dd日") + "</td></tr></table></div>");
            if (a.Rows.Count <= 26)
            {
                sb1.Append("<table id='search' class='tabInfo2' style='text-align: left;'><tr align='center' valign='middle'><td style='width: 5%;' class='th'>序号</td><td style='width: 15%;' class='th'>编号 </th><th  style='width: 15%; text-align: center;' class='th'>零件名称 </th><th style='width: 15%; text-align: center;' class='th'>图号或规格</td><td style='width: 5%;' class='th'>单台数量 </td> <td style='width: 5%;' class='th'>单位</td><td  style='width: 5%;' class='th'>领出数量</td><td  style='width: 5%;' class='td'>更换数量</td> <td  style='width: 10%;' class='th'>更换日期</td><td  style='width: 10%;' class='th'>签字</td><td  style='width: 20%;' class='th'>备注</td></tr><tbody id='DetailInfo' class='tabInfoP'></tbody>");
                //var IdentitySharing = "";
                //int shuliang = 0;
                //int kong = 0;
                //int m = 0;
                //int quzhi = 0;
                //int kuahang = 0;
                //sb1.Append("<table id='search' class='tabInfo2' style='text-align: left;'><tr align='center' valign='middle'><td style='width: 5%;' class='th'>序号</td><td style='width: 15%;' class='th'>编号 </th><th  style='width: 15%; text-align: center;' class='th'>零件名称 </th><th style='width: 15%; text-align: center;' class='th'>图号或规格</td><td style='width: 5%;' class='th'>单台数量 </td> <td style='width: 5%;' class='th'>单位</td><td  style='width: 5%;' class='th'>领出数量</td><td  style='width: 5%;' class='td'>更换数量</td> <td  style='width: 10%;' class='th'>更换日期</td><td  style='width: 10%;' class='th'>签字</td><td  style='width: 20%;' class='th'>备注</td></tr><tbody id='DetailInfo' class='tabInfoP'></tbody>");
                //DataTable BiaoShi = ProduceMan.GetZSL(LLID);

                //    for (quzhi = 0; quzhi < BiaoShi.Rows.Count; quzhi++)
                //    {
                //        IdentitySharing = BiaoShi.Rows[quzhi][0].ToString();
                //        if (IdentitySharing == "" || IdentitySharing == null)
                //        {
                //            DataTable DeengYuKong = ProduceMan.GetNULL(LLID);
                //            for (kong = 0; kong < DeengYuKong.Rows.Count; kong++)
                //            {
                //                sb1.Append("<tr id ='DetailInfo" + kong + "'><td ><lable class='labRowNumber" + kong + " ' id='RowNumber" + kong + "'>" + (kong + 1) + "</lable> </td><td ><lable class='labPID" + kong + " ' id='PID" + kong + "'>" + DeengYuKong.Rows[kong]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + kong + " ' id='OrderContent" + kong + "'>" + DeengYuKong.Rows[kong]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + kong + "  id='SpecsModels" + kong + "'>" + DeengYuKong.Rows[kong]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + kong + "  id='OrderNum" + kong + "'>" + DeengYuKong.Rows[kong]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + kong + "  id='OrderUnit" + kong + "'>" + DeengYuKong.Rows[kong]["OrderUnit"] + "</lable></td> <td  ><lable class='labTechnology" + kong + "  id='Technology" + kong + "'>" + a.Rows[kong]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + kong + "  id='Remark" + kong + "'>" + DeengYuKong.Rows[kong]["Remark"] + "</lable></td> </tr>");
                //            }
                //        }
                //        else
                //        {
                //            DataTable jitiao = ProduceMan.GetLLXQNum(LLID, IdentitySharing);
                //            foreach (DataRow dt2 in jitiao.Rows)
                //            {
                //                if (quzhi == 1)
                //                {
                //                    shuliang = 0;
                //                }
                //                else
                //                {
                //                    //合并的数量
                //                    shuliang = Convert.ToInt32(dt2["a"]);
                //                }
                //                kuahang = Convert.ToInt32(dt2["a"]);
                //            }
                //            //循环共用的数量
                //            DataTable ZHI = ProduceMan.GetZZ(LLID, IdentitySharing);

                //            for (int you = 0; you < ZHI.Rows.Count; you++)
                //            {

                //                m = Convert.ToInt32(kong) + you + shuliang;
                //                if (you == 0)
                //                {
                //                    sb1.Append("<tr id ='DetailInfo" + you + "'><td ><lable class='labRowNumber" + you + " ' id='RowNumber" + you + "'>" + (m + 1) + "</lable> </td><td ><lable class='labPID" + you + " ' id='PID" + you + "'>" + ZHI.Rows[you]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + you + " ' id='OrderContent" + you + "'>" + ZHI.Rows[you]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + you + "  id='SpecsModels" + you + "'>" + ZHI.Rows[you]["SpecsModels"] + "</lable></td> <td rowspan=" + kuahang + "><lable class='labOrderNum" + you + "  id='OrderNum" + you + "'>" + ZHI.Rows[you]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + you + "  id='OrderUnit" + you + "'>" + ZHI.Rows[you]["OrderUnit"] + "</lable></td> <td rowspan=" + kuahang + " ><lable class='labTechnology" + you + "  id='Technology" + you + "'>" + ZHI.Rows[you]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + you + "  id='Remark" + you + "'>" + ZHI.Rows[you]["Remark"] + "</lable></td> </tr>");
                //                }
                //                else
                //                {
                //                    sb1.Append("<tr id ='DetailInfo" + you + "'><td ><lable class='labRowNumber" + you + " ' id='RowNumber" + you + "'>" + (m + 1) + "</lable> </td><td ><lable class='labPID" + you + " ' id='PID" + you + "'>" + ZHI.Rows[you]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + you + " ' id='OrderContent" + you + "'>" + ZHI.Rows[you]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + you + "  id='SpecsModels" + you + "'>" + ZHI.Rows[you]["SpecsModels"] + "</lable></td>  <td><lable class='labOrderUnit" + you + "  id='OrderUnit" + you + "'>" + ZHI.Rows[you]["OrderUnit"] + "</lable></td>  <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + you + "  id='Remark" + you + "'>" + ZHI.Rows[you]["Remark"] + "</lable></td> </tr>");
                //                }
                //            }
                //        }

                //}





                for (int i = 0; i < a.Rows.Count; i++)
                {
                    sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labRowNumber" + i + " ' id='RowNumber" + i + "'>" + (i + 1) + "</lable> </td><td ><lable class='labPID" + i + " ' id='PID" + i + "'>" + a.Rows[i]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'>" + a.Rows[i]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'>" + a.Rows[i]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'>" + a.Rows[i]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'>" + a.Rows[i]["OrderUnit"] + "</lable></td> <td  ><lable class='labTechnology" + i + "  id='Technology" + i + "'>" + a.Rows[i]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + i + "  id='Remark" + i + "'>" + a.Rows[i]["Remark"] + "</lable></td> </tr>");
                }
                //sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 26;
                if (count > 0)
                    count = a.Rows.Count / 26 + 1;
                else
                    count = a.Rows.Count / 26;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 26 * i;
                    int length = 26 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 26 * i + a.Rows.Count % 26;
                    sb1.Append(" <table id='search' class='tabInfo2' style='text-align: left;'><tr align='center' valign='middle'><td style='width: 5%;' class='th'>序号</td><td style='width: 15%;' class='th'>编号 </td><td  style='width: 15%; text-align: center;' class='th'>零件名称 </td><td style='width: 15%; text-align: center;' class='th'>图号或规格</td><td style='width: 5%;' class='th'>单台数量 </td> <td style='width: 5%;' class='th'>单位</td><td  style='width: 5%;' class='th'>领出数量</td><td  style='width: 5%;' class='th'>更换数量</td> <td  style='width: 10%;' class='th'>更换日期</td><td  style='width: 10%;' class='th'>签字</td><td  style='width: 20%;' class='th'>备注</td></tr><tbody id='DetailInfo' class='tabInfoP'></tbody>");
                    //for (int j = b; j < length; j++)
                    //{
                    //    var IdentitySharing = "";
                    //    int shuliang = 0;
                    //    int kong = 0;
                    //    int m = 0;
                    //    int quzhi = 0;
                    //    int kuahang = 0;
                    //    DataTable BiaoShi = ProduceMan.GetZSL(LLID);
                    //    for (quzhi = j; quzhi < BiaoShi.Rows.Count; quzhi++)
                    //    {
                    //        IdentitySharing = BiaoShi.Rows[quzhi][0].ToString();
                    //        if (IdentitySharing == "" || IdentitySharing == null)
                    //        {
                    //            DataTable DeengYuKong = ProduceMan.GetNULL(LLID);
                    //            for (kong = 0; kong < DeengYuKong.Rows.Count; kong++)
                    //            {
                    //                sb1.Append("<tr id ='DetailInfo" + kong + "'><td ><lable class='labRowNumber" + kong + " ' id='RowNumber" + kong + "'>" + (kong + 1) + "</lable> </td><td ><lable class='labPID" + kong + " ' id='PID" + kong + "'>" + DeengYuKong.Rows[kong]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + kong + " ' id='OrderContent" + kong + "'>" + DeengYuKong.Rows[kong]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + kong + "  id='SpecsModels" + kong + "'>" + DeengYuKong.Rows[kong]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + kong + "  id='OrderNum" + kong + "'>" + DeengYuKong.Rows[kong]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + kong + "  id='OrderUnit" + kong + "'>" + DeengYuKong.Rows[kong]["OrderUnit"] + "</lable></td> <td  ><lable class='labTechnology" + kong + "  id='Technology" + kong + "'>" + a.Rows[kong]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + kong + "  id='DeliveryTime" + kong + "'>" + DeengYuKong.Rows[kong]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + kong + "  id='Remark" + kong + "'>" + DeengYuKong.Rows[kong]["Remark"] + "</lable></td> </tr>");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            DataTable jitiao = ProduceMan.GetLLXQNum(LLID, IdentitySharing);
                    //            foreach (DataRow dt2 in jitiao.Rows)
                    //            {
                    //                if (quzhi == 1)
                    //                {
                    //                    shuliang = 0;
                    //                }
                    //                else
                    //                {
                    //                    //合并的数量
                    //                    shuliang = Convert.ToInt32(dt2["a"]);

                    //                }
                    //                shuliang += shuliang;
                    //                kuahang = Convert.ToInt32(dt2["a"]);
                    //            }
                    //            //循环共用的数量
                    //            DataTable ZHI = ProduceMan.GetZZ(LLID, IdentitySharing);

                    //            for (int you = 0; you < ZHI.Rows.Count; you++)
                    //            {

                    //                m = Convert.ToInt32(kong) + you + shuliang+1;
                    //                if (you == 0)
                    //                {
                    //                    sb1.Append("<tr id ='DetailInfo" + you + "'><td ><lable class='labRowNumber" + you + " ' id='RowNumber" + you + "'>" + (m) + "</lable> </td><td ><lable class='labPID" + you + " ' id='PID" + you + "'>" + ZHI.Rows[you]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + you + " ' id='OrderContent" + you + "'>" + ZHI.Rows[you]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + you + "  id='SpecsModels" + you + "'>" + ZHI.Rows[you]["SpecsModels"] + "</lable></td> <td rowspan=" + kuahang + "><lable class='labOrderNum" + you + "  id='OrderNum" + you + "'>" + ZHI.Rows[you]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + you + "  id='OrderUnit" + you + "'>" + ZHI.Rows[you]["OrderUnit"] + "</lable></td> <td rowspan=" + kuahang + " ><lable class='labTechnology" + you + "  id='Technology" + you + "'>" + ZHI.Rows[you]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + you + "  id='Remark" + you + "'>" + ZHI.Rows[you]["Remark"] + "</lable></td> </tr>");
                    //                }
                    //                else
                    //                {
                    //                    sb1.Append("<tr id ='DetailInfo" + you + "'><td ><lable class='labRowNumber" + you + " ' id='RowNumber" + you + "'>" + (m) + "</lable> </td><td ><lable class='labPID" + you + " ' id='PID" + you + "'>" + ZHI.Rows[you]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + you + " ' id='OrderContent" + you + "'>" + ZHI.Rows[you]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + you + "  id='SpecsModels" + you + "'>" + ZHI.Rows[you]["SpecsModels"] + "</lable></td>  <td><lable class='labOrderUnit" + you + "  id='OrderUnit" + you + "'>" + ZHI.Rows[you]["OrderUnit"] + "</lable></td>  <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + you + "  id='DeliveryTime" + you + "'>" + ZHI.Rows[you]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + you + "  id='Remark" + you + "'>" + ZHI.Rows[you]["Remark"] + "</lable></td> </tr>");
                    //                }
                    //            }
                    //        }

                    //    }

                    //}
                    for (int j = b; j < length; j++)
                    {
                        sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labRowNumber" + j + " ' id='RowNumber" + j + "'>" + (j + 1) + "</lable> </td><td ><lable class='labPID" + j + " ' id='PID" + j + "'>" + a.Rows[j]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'>" + a.Rows[j]["OrderContent"] + "</lable> </td> <td  ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'>" + a.Rows[j]["SpecsModels"] + "</lable></td> <td ><lable class='labOrderNum" + j + "  id='OrderNum" + j + "'>" + a.Rows[j]["Manufacturer"] + "</lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'>" + a.Rows[j]["OrderUnit"] + "</lable></td> <td  ><lable class='labTechnology" + j + "  id='Technology" + j + "'>" + a.Rows[j]["OrderNum"] + "</lable></td> <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["a"] + "</lable></td> <td><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["DeliveryTime"] + "</lable></td> <td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["DeliveryTime"] + "</lable></td> <td  ><lable class='labRemark" + j + "  id='Remark" + j + "'>" + a.Rows[j]["Remark"] + "</lable></td> </tr>");
                    }
                    if ((length - b) < 26)
                    {
                    }
                    //sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }

            Response.Write(html);
            return View();
        }


        #endregion
        #endregion

        #region 随工单管理
        public ActionResult withthejob()
        {
            return View();
        }
        #region  随工单列表显示
        public ActionResult withthejobList(ProduceList SG)
        {
            if (ModelState.IsValid)
            {
                string where = "   and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string RWID = SG.RWID;
                string OrderContent = SG.OrderContent;
                string SpecsModels = SG.SpecsModels;

                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";

                if (Request["RWID"] != "")
                    where += " a.RWID like '%" + Request["RWID"] + "%' and";
                if (Request["OrderContent"] != "")
                    where += " b.OrderContent like '%" + Request["OrderContent"] + "%' and";
                if (Request["SpecsModels"] != "")
                    where += " b.SpecsModels = '" + Request["SpecsModels"] + "' and";
                if (Starts != "" && Starte != "")
                    where += " a.billing between '" + Starts + "' and '" + Starte + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProduceMan.withthejobList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult ProduceInDetials()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string SGID = Request["SGID"].ToString();
            string RWID = Request["RWID"].ToString();


            UIDataTable udtTask = ProduceMan.ProduceInDetials(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, SGID, RWID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ProProductRDatail()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.ProProductRDatail(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }


        #endregion

        #region  记录单
        public ActionResult SGJL()
        {
            var SGID = Request["SGID"].ToString();
            tk_ProductRecord a = ProduceMan.RDetail(SGID);
            tk_ProductRecord b = ProduceMan.getsumnum(SGID);
            ViewData["m"] = b.m.ToString();
            a.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["billing"] = a.billing.ToShortDateString();
            return View(a);
        }

        public ActionResult GetProductdetail()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.GetProductdetail(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public string ProductRDatailNum(string SGID)
        {
            return ProduceMan.ProductRDatailNum(SGID);
        }

        public ActionResult GetSGJLType()
        {
            DataTable dt = ProduceMan.GetSGJLType();
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveProductRDatail()
        {
            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrProcess = Request["Process"].Split(',');
            string[] arrteam = Request["team"].Split(',');
            string[] arrEstimatetime = Request["Estimatetime"].Split(',');
            string[] arrperson = Request["person"].Split(',');
            string[] arrplannumber = Request["plannumber"].Split(',');
            string[] arrQualified = Request["Qualified"].Split(',');
            string[] arrnumber = Request["number"].Split(',');
            string[] arrnumbers = Request["numbers"].Split(',');
            string[] arrFnubers = Request["Fnubers"].Split(',');
            string[] arrfinishtime = Request["finishtime"].Split(',');
            string[] arrpeople = Request["people"].Split(',');
            string[] arrreason = Request["reason"].Split(',');
            string[] arrTechnical = Request["Technical"].Split(',');

            string[] arrA = Request["A"].Split(',');
            string[] arrDID = Request["DID"].Split(',');

            string m = "";
            tk_ProductTDatail task = new tk_ProductTDatail();
            List<tk_ProductTDatail> ta = new List<tk_ProductTDatail>();
            for (int i = 0; i < arrA.Length; i++)
            {
                task = new tk_ProductTDatail();
                task.DID = arrDID[i].ToString();
                ta.Add(task);
            }

            string strErr = "";
            tk_ProductRDatail deInfo = new tk_ProductRDatail();
            List<tk_ProductRDatail> detailList = new List<tk_ProductRDatail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_ProductRDatail();
                deInfo.SGID = Request["SGID"].ToString();
                deInfo.DID = ProductRDatailNum(Request["SGID"].ToString());
                deInfo.Process = arrProcess[i].ToString();
                deInfo.team = arrteam[i].ToString();
                deInfo.Estimatetime = arrEstimatetime[i].ToString();
                deInfo.person = arrperson[i].ToString();
                deInfo.plannumber = Convert.ToInt32(arrplannumber[i]);
                deInfo.Qualified = Convert.ToInt32(arrQualified[i]);
                deInfo.number = Convert.ToInt32(arrnumber[i]);
                deInfo.numbers = Convert.ToInt32(arrnumbers[i]);
                deInfo.Fnubers = Convert.ToInt32(arrFnubers[i]);
                deInfo.finishtime = Convert.ToDateTime(arrfinishtime[i]);
                deInfo.people = arrpeople[i].ToString();
                deInfo.reason = arrreason[i].ToString();
                deInfo.Validate = "v";
                deInfo.Technical = arrTechnical[i].ToString();
                detailList.Add(deInfo);
            }
            var RWID = Request["RWID"].ToString();
            var SGID = Request["SGID"].ToString();
            bool b = ProduceMan.SaveProductRDatail(detailList, ta, ref strErr, RWID, SGID);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "新增随工记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "新增随工记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }


        #endregion

        #region 撤销
        public ActionResult CXSG()
        {
            string strErr = "";
            string SGID = Request["SGID"].ToString();
            string RWID = Request["RWID"].ToString();
            if (ProduceMan.CXSG(SGID, ref strErr, RWID))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "撤销成功 ";
                log.Content = "撤销随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "撤销失败 ";
                log.Content = "撤销随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 删除祥表信息
        public ActionResult SCSG()
        {
            string strErr = "";
            string DID = Request["DID"].ToString();

            if (ProduceMan.SCSG(DID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除成功 ";
                log.Content = "删除报告单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除失败 ";
                log.Content = "删除报告单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 修改随工单
        public ActionResult updateSG()
        {
            string SGID = Request["SGID"].ToString();
            tk_ProductRecord Material = ProduceMan.IndexAllupdateSG(SGID);
            ViewData["SGID"] = Request["SGID"].ToString();
            ViewData["billing"] = Material.billing.ToShortDateString();
            return View(Material);
        }
        public ActionResult LoadRDatail()
        {
            string SGID = Request["SGID"].ToString();
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.LoadRDatail(SGID, RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult getISSGdetail()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.getISSGdetail(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult GetSGnum()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.GetSGnum(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult LoadRDatails()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.LoadRDatails(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveUpdateRDatail()
        {
            tk_ProductRecord Material = new tk_ProductRecord();
            Material.SGID = Request["SGID"].ToString();
            Material.RWID = Request["RWID"].ToString();
            Material.ID = Request["ID"].ToString();
            Material.SpecsModels = Request["SpecsModels"].ToString();
            Material.billing = Convert.ToDateTime(Request["billing"]);
            //Material.OrderContent = Request["OrderContent"].ToString();
            //Material.Remark = Request["Remark"].ToString();
            Material.CreateUser = Request["CreateUser"].ToString();


            //string[] arrMain = Request["MainContent"].Split(',');
            //string[] arrProcess = Request["Process"].Split(',');
            //string[] arrTeam = Request["Team"].Split(',');
            //string[] arrEstimatetime = Request["Estimatetime"].Split(',');
            //string[] arrPerson = Request["Person"].Split(',');
            //string[] arrPlannumber = Request["Plannumber"].Split(',');
            //string[] arrQualified = Request["Qualified"].Split(',');
            //string[] arrNumber = Request["Number"].Split(',');
            //string[] arrNumbers = Request["Numbers"].Split(',');
            //string[] arrFnubers = Request["Fnubers"].Split(',');
            //string[] arrFinishtime = Request["Finishtime"].Split(',');
            //string[] arrPeople = Request["People"].Split(',');
            //string[] arrReason = Request["Reason"].Split(',');
            //string[] arrDID = Request["DID"].Split(',');

            string strErr = "";
            //tk_ProductRDatail deInfo = new tk_ProductRDatail();
            //List<tk_ProductRDatail> detailList = new List<tk_ProductRDatail>();
            //for (int i = 0; i < arrMain.Length; i++)
            //{
            //    deInfo = new tk_ProductRDatail();
            //    deInfo.Process = arrProcess[i].ToString();
            //    deInfo.team = arrTeam[i].ToString();
            //    deInfo.Estimatetime = arrEstimatetime[i].ToString();
            //    deInfo.person = arrPerson[i].ToString();
            //    deInfo.plannumber = Convert.ToInt32(arrPlannumber[i]);
            //    deInfo.Qualified = Convert.ToInt32(arrQualified[i]);
            //    deInfo.number = Convert.ToInt32(arrNumber[i]);
            //    deInfo.numbers = Convert.ToInt32(arrNumbers[i]);
            //    deInfo.Fnubers = Convert.ToInt32(arrFnubers[i]);
            //    deInfo.finishtime = Convert.ToDateTime(arrFinishtime[i]);
            //    deInfo.people = arrPeople[i].ToString();
            //    deInfo.reason = arrReason[i].ToString();
            //    deInfo.DID = arrDID[i].ToString();
            //    detailList.Add(deInfo);
            //}

            string[] arrMains = Request["MainContents"].Split(',');
            string[] arrOrderNum = Request["OrderNums"].Split(',');
            string[] arrDIDS = Request["DIDS"].Split(',');

            string strErrs = "";
            tk_ProductRProduct RProduct = new tk_ProductRProduct();
            List<tk_ProductRProduct> RProducts = new List<tk_ProductRProduct>();
            for (int i = 0; i < arrMains.Length; i++)
            {
                RProduct = new tk_ProductRProduct();
                RProduct.OrderNum = Convert.ToInt32(arrOrderNum[i]);
                RProduct.SGDID = arrDIDS[i].ToString();
                RProducts.Add(RProduct);
            }
            var SGID = Request["SGID"].ToString();
            var RWID = Request["RWID"].ToString();
            bool b = ProduceMan.SaveUpdateRDatail(Material, RProducts, ref strErr, ref strErrs, SGID, RWID);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "修改成功 ";
                log.Content = "修改随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["SGID"].ToString();
                log.YYType = "修改失败 ";
                log.Content = "修改随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }
        #endregion

        #region 随工单详情
        public ActionResult SGDtail()
        {
            string SGID = Request["SGID"].ToString();
            tk_ProductRecord Material = ProduceMan.IndexAllSGDtail(SGID);
            ViewData["SGID"] = Request["SGID"].ToString();
            ViewData["billing"] = Material.billing.ToShortDateString();
            return View(Material);
        }

        public ActionResult LoadSGDetail()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.LoadSGDetail(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        public ActionResult LoadSGDetails()
        {
            string SGID = Request["SGID"].ToString();
            DataTable dt = ProduceMan.LoadSGDetails(SGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion

        #region 相关单据
        public ActionResult LoadSG()
        {
            string strErr = "";
            string a = Request["ID"].ToString();
            string ID = a.Substring(0, 2);
            if (ID == "SG")
            {
                DataTable data = ProduceMan.GetSG(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["ID"] = dt["ID"].ToString();
                    ViewData["billing"] = dt["billing"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["Remark"] = dt["Remark"].ToString();
                    ViewData["CreateUser"] = dt["CreateUser"].ToString();
                    ViewData["m"] = "随工单编号：" + a + "";
                }
            }
            return View();
        }

        public ActionResult LoadSGs()
        {
            string SGID = Request["SGID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.LoadSGs(SGID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult LoadSGXG()
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
            string SGID = Request["SGID"];
            if (!string.IsNullOrEmpty(SGID)) { where = " where SGID='" + SGID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadSGXG(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadSGXGs()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string SGID = Request["SGID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadSGXGs(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, SGID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadSGXGDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string SGID = Request["SGID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadSGXGDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, SGID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 打印
        public ActionResult getXl()
        {
            DataTable dt = ProduceMan.PrintSGss();
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult PrintSG()
        {
            ViewData["b"] = "";
            string SGID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(SGID))
            {
                s += " a.SGID like '%" + SGID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and c.DID=b.DID  and a.SGID=c.SGID and c.PID=b.PID and " + s; }
            string strErr = "";

            string tableName = " BGOI_Produce.dbo.tk_ProductRProduct c,BGOI_Produce.dbo.tk_ProductTDatail b,BGOI_Produce.dbo.tk_ProductRecord a ";
            DataTable data = ProduceMan.PrintSGs(where, tableName, ref strErr);
            tk_ProductRecord so = new TECOCITY_BGOI.tk_ProductRecord();
            foreach (DataRow dt in data.Rows)
            {
                so.SGID = dt["SGID"].ToString();
                so.ID = dt["ID"].ToString();
                so.SpecsModels = dt["SpecsModels"].ToString();
                so.billing = Convert.ToDateTime(dt["billing"]);
                ViewData["billing"] = so.billing.ToShortDateString();
                so.OrderContent = dt["OrderContent"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = dt["CreateUser"].ToString();
                ViewData["OrderContent"] = dt["m"].ToString();
                ViewData["SpecsModels"] = dt["n"].ToString();
                ViewData["PID"] = dt["x"].ToString();
                ViewData["photo"] = dt["y"].ToString();
            }
            DataTable a = ProduceMan.PrintSGss();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            string html = "";
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'><div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司</div><div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>随工单</div><br /><div><div style='font-weight: bold;  margin-left:75%' >YSGLJL-SC-F01</div><div style='font-weight: bold; float:right; margin-right:8%' >" + so.SGID + "</div></div><br/><div><div style='font-weight: bold; float:left;display:inline;margin-left:5%;' >编号:" + so.ID + "</div><div style='font-weight: bold; float:left ;display:inline;padding-left:25%;'  >负责人:" + so.CreateUser + "</div><div style='font-weight: bold; float:right;display:inline;padding-right:10%;' >发单日期:" + ViewData["billing"] + "</div></div><table id='search' class='tabInfo2' style='text-align: left;'><tr><td colspan='2' >产品名称:" + ViewData["OrderContent"] + "</td><td  >规格型号:</td><td colspan='2' >" + ViewData["SpecsModels"] + "</td><td colspan='2'>图纸号或批次号:</td><td colspan='2' >" + ViewData["photo"] + "</td><td colspan='2' >产品编码:</td><td colspan='2' ></td></tr>");

            sb2.Append("<tr style='width:99%;height:100px'> <td colspan='13' style=' text-align: left; vertical-align: middle'>技术要求:按作业指导书的要求进行组装、调试</td></tr></table>");

            sb1.Append("<tr align='center' valign='middle'><td style='width: 5%;' class='th' rowspan='2'>序号</td><td style='width: 15%;' class='th' rowspan='2'>工序</td><td style='width: 10%;' class='th' rowspan='2'>班组</td><td style='width: 10%;' class='th' rowspan='2'>预计完成日期</td><td style='width: 5%;' class='th' rowspan='2'>责任人</td><td style='width: 5%;' class='th' rowspan='2'>计划数量</td><td style='width: 5%;' class='th' colspan='4'>完成数量</td><td style='width: 10%;' class='th' rowspan='2'>实际完工日期</td><td style='width: 10%;' class='th'rowspan='2'>检验员</td><td style='width: 10%;' class='th'rowspan='2'>原因分析或说明</td></tr><tr> <td style='width: 5%;' class='th'>合格</td><td style='width: 5%;' class='th'>返修 </td><td style='width: 5%;' class='th'>变更</td><td style='width: 5%;' class='th'>废品</td></tr><tbody id='DetailInfos' class='tabInfoP'></tbody>");
            for (int i = 0; i < 10; i++)
            {
                sb1.Append("<tr id ='DetailInfos" + i + "'><td ><lable class='labRowNumber" + i + " ' id='RowNumber" + i + "'>" + (i + 1) + "</lable> </td><td  style='text-align:center'><label  id='d" + i + "'> <select id='PID" + i + "'>");
                for (var m = 0; m < a.Rows.Count + 1; m++)
                {
                    if (m == 0)
                    {
                        sb1.Append("<option value=''></option>");
                    }
                    else
                    {
                        sb1.Append("<option value='" + a.Rows[m - 1]["Text"] + "'>" + a.Rows[m - 1]["Text"] + "</option>");
                    }
                }

                sb1.Append("</select></td></label> <td  ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'></lable> </td><td  ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'></lable></td> <td ><lable class='labOrderNum" + i + "  id='OrderNum" + i + "'></lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'></lable></td> <td  ><lable class='labTechnology" + i + "  id='Technology" + i + "'></lable></td> <td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td><td><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td><td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td><td  ><lable class='labRemark" + i + "  id='Remark" + i + "'></lable></td><td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'></lable></td><td  ><lable class='labRemark" + i + "  id='Remark" + i + "'></lable></td></tr>");
            }
            //sb1.Append("</tbody></table></div>");
            html = sb.ToString() + sb1.ToString() + sb2.ToString();
            Response.Write(html);

            return View();
        }




        #endregion


        #endregion

        #region 检验报告管理
        public ActionResult InspectionReport()
        {
            return View();
        }
        #region  列表显示
        public ActionResult ReportInfo(ProduceList BG)
        {
            if (ModelState.IsValid)
            {
                string where = "  and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string RWID = BG.RWID;
                string OrderContent = BG.OrderContent;
                string SpecsModels = BG.SpecsModels;
                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";



                if (Request["RWID"] != "")
                    where += " a.RWID like '%" + Request["RWID"] + "%' and";
                if (Request["OrderContent"] != "")
                    where += " b.OrderContent like '%" + Request["OrderContent"] + "%' and";
                if (Request["SpecsModels"] != "")
                    where += " b.SpecsModels like '%" + Request["SpecsModels"] + "%' and";
                if (Starts != "" && Starte != "")
                    where += " a.uploadtime between '" + Starts + "' and '" + Starte + "' and";

                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProduceMan.ReportInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult FileInfo()
        {
            string BGID = Request["BGID"].ToString();
            DataTable dt = ProduceMan.FileInfo(BGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        #endregion

        #region 修改检验报告
        public ActionResult UpdateBG(HttpPostedFileBase sd)
        {
            string BGID = Request["BGID"].ToString();
            tk_ReportInfo a = new tk_ReportInfo();
            a.BGID = BGID;
            tk_ReportInfo Material = ProduceMan.IndexAllReportInfo(BGID);
            BGID = Request["BGID"].ToString();
            return View(Material);
        }

        public ActionResult getFileInfo()
        {
            string BGID = Request["BGID"].ToString();
            DataTable dt = ProduceMan.getFileInfo(BGID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        //修改报告时删除撤销文件
        public ActionResult SCBG()
        {
            string strErr = "";
            string DID = Request["DID"].ToString();
            if (ProduceMan.SCBG(DID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除成功 ";
                log.Content = "删除随工单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["DID"].ToString();
                log.YYType = "删除失败 ";
                log.Content = "删除随工单记录";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        //保存修改后的文件
        public ActionResult SaveUpdateFileInfoIn()
        {
            int bb;
            tk_ReportInfo task = new tk_ReportInfo();
            task.BGID = Request["BGID"].ToString();
            task.RWID = Request["RWID"].ToString();
            task.DID = Request["DID"].ToString();
            task.uploadtime = Convert.ToDateTime(Request["uploadtime"]);
            task.Remarks = Request["Remarks"].ToString();
            var a = Convert.ToInt32(Request["q"].ToString());
            if (a == 0)
            {
                bb = Convert.ToInt32(Request["bbb"]) - 1;
            }
            else
            {
                bb = Convert.ToInt32(Request["bbb"]);
            }

            string strErr = "";
            HttpFileCollectionBase files = Request.Files;
            tk_FileInfo deInfo = new tk_FileInfo();
            List<tk_FileInfo> detailList = new List<tk_FileInfo>();
            for (int i = 0; i < files.Count; i++)
            {
                deInfo = new tk_FileInfo();
                //上传文件
                //tk_FileInfo deInfo = new tk_FileInfo();
                HttpPostedFileBase file = files[i];
                byte[] fileByte = new byte[0];
                if (file.FileName != "")
                {
                    deInfo.FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    int fileLength = file.ContentLength;
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        file.InputStream.Read(fileByte, 0, fileLength);

                    }
                }
                deInfo.FileInfo = fileByte;
                deInfo.BGID = Request["BGID"].ToString();
                deInfo.DID = ProFileInfoNum(Request["BGID"].ToString());
                deInfo.CreatePerson = GAccount.GetAccountInfo().UserName;
                deInfo.Type = Request["Type" + bb].ToString();
                deInfo.Validate = "v";
                deInfo.CreateTime = DateTime.Now;
                detailList.Add(deInfo);
            }
            var CreatePerson = GAccount.GetAccountInfo().UserName;
            var CreateTime = DateTime.Now;
            var BGID = Request["BGID"].ToString();
            bool b = ProduceMan.SaveUpdateFileInfoIn(task, detailList, ref strErr, BGID, CreatePerson, CreateTime);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "修改成功 ";
                log.Content = "修改检验报告";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                ViewData["msg"] = "修改成功";
                return View("updateBG", task);
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "修改失败 ";
                log.Content = "修改检验报告";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                ViewData["msg"] = "修改失败";
                return View("updateBG", task);
            }
        }
        #endregion

        #region 撤销
        public ActionResult CXBG()
        {
            string strErr = "";
            string BGID = Request["BGID"].ToString();

            if (ProduceMan.CXBG(BGID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "撤销成功 ";
                log.Content = "撤销随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["BGID"].ToString();
                log.YYType = "撤销失败 ";
                log.Content = "撤销随工单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 相关单据
        public ActionResult LoadBG()
        {
            string strErr = "";
            string a = Request["ID"].ToString();
            string ID = a.Substring(0, 2);
            if (ID == "BG")
            {
                DataTable data = ProduceMan.GetBG(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["uploadtime"] = dt["uploadtime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["Remarks"] = dt["Remarks"].ToString();
                    ViewData["CreatePerson"] = dt["CreatePerson"].ToString();
                    ViewData["m"] = "报告单编号：" + a + "";
                }
            }
            return View();
        }

        public ActionResult LoadBGs()
        {
            string BGID = Request["BGID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.LoadBGs(BGID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult LoadBGXG()
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
            string BGID = Request["BGID"];
            if (!string.IsNullOrEmpty(BGID)) { where = " where BGID='" + BGID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadBGXG(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadBGXGDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string BGID = Request["BGID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadBGXGDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, BGID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 文件下载
        public ActionResult DownLoadFile(string id)
        {
            ViewData["StrCID"] = id;
            return View();
        }

        public ActionResult GetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProduceMan.GetNewDownLoad(id);
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
            DataTable dtInfo = ProduceMan.GetNewDownloadFile(informNo);
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

                Response.BinaryWrite((byte[])dtInfo.Rows[0]["FileInfo"]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        #endregion
        #endregion

        #region 产品入库
        #region 列表显示
        public ActionResult Productstorage()
        {
            return View();
        }

        public ActionResult PStocking(ProduceList RK)
        {
            if (ModelState.IsValid)
            {
                string where = "   and";
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string RKID = RK.RKID;
                string OrderContent = RK.OrderContent;


                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";



                if (Request["RKID"] != "")
                    where += " a.RKID like '%" + Request["RKID"] + "%' and";
                if (Request["OrderContent"] != "")
                    where += " b.OrderContent = '" + Request["OrderContent"] + "' and";

                if (Starts != "" && Starte != "")
                    where += " a.StockInTime between '" + Starts + "' and '" + Starte + "' and";

                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProduceMan.PStocking(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult PStockingDetail()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string RKID = Request["RKID"].ToString();


            UIDataTable udtTask = ProduceMan.PStockingDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RKID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改入库单信息
        public ActionResult UpdateRK()
        {
            string RKID = Request["RKID"].ToString();
            tk_PStocking Material = ProduceMan.IndexAllupdateRK(RKID);
            ViewData["RKID"] = Request["RKID"].ToString();
            ViewData["StockInTime"] = Material.StockInTime.ToShortDateString();
            ViewData["FinishTime"] = Material.FinishTime.ToShortDateString();
            return View(Material);
        }

        public ActionResult LoadRposDatail()
        {
            string RKID = Request["RKID"].ToString();
            string RWID = Request["RWID"].ToString();
            DataTable dt = ProduceMan.LoadRposDatail(RKID, RWID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveUpdateposDetail()
        {
            tk_PStocking Material = new tk_PStocking();
            Material.RKID = Request["RKID"].ToString();
            Material.RWID = Request["RWID"].ToString();
            Material.StockInTime = Convert.ToDateTime(Request["StockInTime"]);
            Material.FinishTime = Convert.ToDateTime(Request["FinishTime"]);
            Material.HouseID = Request["HouseID"].ToString();
            Material.Batch = Request["Batch"].ToString();
            Material.StockRemark = Request["StockRemark"].ToString();

            string[] arrMain = Request["MainContent"].Split(',');
            //string[] arrPID = Request["PID"].Split(',');
            //string[] arrOrderContent = Request["OrderContent"].Split(',');
            //string[] arrSpecifications = Request["Specifications"].Split(',');
            //string[] arrSupplier = Request["Supplier"].Split(',');
            //string[] arrUnit = Request["Unit"].Split(',');
            string[] arrAmount = Request["Amount"].Split(',');
            //string[] arrRemark = Request["Remark"].Split(',');
            string[] arrDID = Request["DID"].Split(',');


            string strErr = "";
            tk_PStockingDetail deInfo = new tk_PStockingDetail();
            List<tk_PStockingDetail> detailList = new List<tk_PStockingDetail>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_PStockingDetail();
                //deInfo.PID = arrPID[i].ToString();
                //deInfo.OrderContent = arrOrderContent[i].ToString();
                //deInfo.Specifications = arrSpecifications[i].ToString();
                //deInfo.Supplier = arrSupplier[i].ToString();
                //deInfo.Unit = arrUnit[i].ToString();
                //deInfo.Amount = '1';
                deInfo.Amount = Convert.ToInt32(arrAmount[i]);
                //deInfo.Remark = arrRemark[i].ToString();
                deInfo.DID = arrDID[i].ToString();

                detailList.Add(deInfo);
            }
            var RKID = Request["RKID"].ToString();
            var RWID = Request["RWID"].ToString();
            var RWIDDID = Request["M"].ToString();
            bool b = ProduceMan.SaveUpdateposDetail(Material, detailList, ref strErr, RKID, RWID, RWIDDID);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "修改成功 ";
                log.Content = "修改产品入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "修改失败 ";
                log.Content = "修改产品入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }
        #endregion

        #region 入库单详情
        public ActionResult RKDetail()
        {
            string RKID = Request["RKID"].ToString();
            tk_PStocking Material = ProduceMan.IndexAllRKDetail(RKID);
            ViewData["RKID"] = Request["RKID"].ToString();
            ViewData["RWID"] = Material.RWID.ToString();
            ViewData["StockInTime"] = Material.StockInTime.ToShortDateString();
            ViewData["FinishTime"] = Material.FinishTime.ToShortDateString();
            return View(Material);
        }

        public ActionResult LoadRKDatail()
        {
            string RKID = Request["RKID"].ToString();
            DataTable dt = ProduceMan.LoadRKDatail(RKID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion

        #region 撤销入库
        public ActionResult CXRK()
        {
            string strErr = "";
            string RKID = Request["RKID"].ToString();
            string RWID = Request["RWID"].ToString();

            if (ProduceMan.CXRK(RKID, ref strErr, RWID))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "撤销成功 ";
                log.Content = "撤销入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["RKID"].ToString();
                log.YYType = "撤销失败 ";
                log.Content = "撤销入库";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region 相关单据
        public ActionResult LoadRK()
        {
            string strErr = "";
            string a = Request["ID"].ToString();
            string ID = a.Substring(0, 2);
            if (ID == "RK")
            {
                DataTable data = ProduceMan.GetRK(a, ref strErr);
                tk_ProductTask so = new TECOCITY_BGOI.tk_ProductTask();
                foreach (DataRow dt in data.Rows)
                {
                    ViewData["FinishTime"] = dt["FinishTime"].ToString();
                    ViewData["StockInTime"] = dt["StockInTime"].ToString();
                    ViewData["PID"] = dt["PID"].ToString();
                    ViewData["OrderContent"] = dt["OrderContent"].ToString();
                    ViewData["SpecsModels"] = dt["SpecsModels"].ToString();
                    ViewData["OrderUnit"] = dt["OrderUnit"].ToString();
                    ViewData["OrderNum"] = dt["OrderNum"].ToString();
                    ViewData["HouseID"] = dt["HouseID"].ToString();
                    ViewData["Batch"] = dt["Batch"].ToString();
                    ViewData["StockRemark"] = dt["StockRemark"].ToString();
                    ViewData["Storekeeper"] = dt["Storekeeper"].ToString();
                    ViewData["StockInUser"] = dt["StockInUser"].ToString();
                    ViewData["m"] = "入库单编号：" + a + "";
                }
            }
            return View();
        }

        public ActionResult LoadRKs()
        {
            string RKID = Request["RKID"].ToString();
            string strErr = "";
            DataTable dt = ProduceMan.LoadRKs(RKID, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult LoadRKXG()
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
            string RKID = Request["RKID"];
            if (!string.IsNullOrEmpty(RKID)) { where = " where RKID='" + RKID + "'"; }


            UIDataTable udtTask = ProduceMan.LoadRKXG(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadRKXGDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";
            string RKID = Request["RKID"].ToString();
            UIDataTable udtTask = ProduceMan.LoadRKXGDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RKID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 打印
        public ActionResult PrintRK()
        {

            string RKID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(RKID))
            {
                s += " RKID like '%" + RKID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_Produce.dbo.tk_PStocking ";
            DataTable data = ProduceMan.PrintRKs(where, tableName, ref strErr);
            tk_PStocking so = new TECOCITY_BGOI.tk_PStocking();
            foreach (DataRow dt in data.Rows)
            {
                so.RWID = dt["RWID"].ToString();
                so.RKID = dt["RKID"].ToString();
                so.StockInTime = Convert.ToDateTime(dt["StockInTime"]);
                ViewData["StockInTime"] = so.StockInTime.ToShortDateString();
                so.FinishTime = Convert.ToDateTime(dt["FinishTime"]);
                ViewData["FinishTime"] = so.FinishTime.ToShortDateString();
                so.HouseID = dt["HouseID"].ToString();
                so.StockInUser = dt["StockInUser"].ToString();
                so.StockRemark = dt["StockRemark"].ToString();
            }
            DataTable a = ProduceMan.PrintRKdetail(RKID);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append(" <div id='ReportContent' style='margin-top: 10px; page-break-after: always;'><div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司</div><div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>入库单</div><br /><div><div style='font-weight: bold;margin-left:80%' >YSGLJL-SC-F14</div><div style='font-weight: bold; margin-left:78%' >" + so.RKID + "</div></div><div><div style='font-weight: bold; float:left;display:inline;margin-left:20%;' >入库日期:" + ViewData["StockInTime"] + "</div><div style='font-weight: bold;float:left;display:inline;padding-left:30%;' >完成日期:" + ViewData["FinishTime"] + "</div></div>");

            sb2.Append(" <tr ><td colspan='3'  width:'33%'>入库库房:" + so.HouseID + "</td><td colspan='3' width:'33%'>入库批次:</td><td colspan='4'  width:'33%'>入库人:" + so.StockInUser + "</td></tr><tr ><td colspan='8'  width:'33%'>入库说明:" + so.StockRemark + "</td></tr></table>");
            if (a.Rows.Count <= 26)
            {
                sb1.Append("<table id='search' class='tabInfo2' style='text-align: left;'><tr align='center' valign='middle'><th style='width: 5%;' class='th'>序号</th><th style='width: 15%;' class='th'>产品编号</th> <th  style='width: 15%; text-align: center;' class='th'>产品名称</th><th style='width: 15%; text-align: center;' class='th'>规格型号</th> <th style='width: 10%;' class='th'>单位</th><th  style='width: 10%;' class='th'>收货数量</th><th  style='width: 20%;' class='th'>备注</th> </tr> <tbody id='DetailInfo' class='tabInfoP'></tbody>");
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    sb1.Append("<tr id ='DetailInfo" + i + "'><td ><lable class='labRowNumber" + i + " ' id='RowNumber" + i + "'>" + (i + 1) + "</lable> </td><td ><lable class='labPID" + i + " ' id='PID" + i + "'>" + a.Rows[i]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + i + " ' id='OrderContent" + i + "'>" + a.Rows[i]["OrderContent"] + "</lable> </td><td  ><lable class='labSpecsModels" + i + "  id='SpecsModels" + i + "'>" + a.Rows[i]["Specifications"] + "</lable></td> <td><lable class='labOrderUnit" + i + "  id='OrderUnit" + i + "'>" + a.Rows[i]["Unit"] + "</lable></td> <td  ><lable class='labTechnology" + i + "'  id='Technology" + i + "'>" + a.Rows[i]["Amount"] + "</lable></td><td ><lable class='labDeliveryTime" + i + "  id='DeliveryTime" + i + "'>" + a.Rows[i]["Remark"] + "</lable></td></tr>");
                }
                //sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 26;
                if (count > 0)
                    count = a.Rows.Count / 26 + 1;
                else
                    count = a.Rows.Count / 26;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 26 * i;
                    int length = 26 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 26 * i + a.Rows.Count % 26;
                    sb1.Append(" <table id='search' class='tabInfo2' style='text-align: left;'><tr align='center' valign='middle'><th style='width: 5%;' class='th'>序号</th><th style='width: 15%;' class='th'>编号</th> <th  style='width: 15%; text-align: center;' class='th'>产品名称</th><th style='width: 15%; text-align: center;' class='th'>规格型号</th> <th style='width: 10%;' class='th'>单位</th><th  style='width: 10%;' class='th'>收货数量</th><th  style='width: 20%;' class='th'>备注</th> </tr> <tbody id='DetailInfo' class='tabInfoP'></tbody>");
                    for (int j = b; j < length; j++)
                    {
                        sb1.Append("<tr id ='DetailInfo" + j + "'><td ><lable class='labRowNumber" + j + " ' id='RowNumber" + j + "'>" + (j + 1) + "</lable> </td><td ><lable class='labPID" + j + " ' id='PID" + j + "'>" + a.Rows[j]["PID"] + "</lable> </td> <td  ><lable class='labOrderContent" + j + " ' id='OrderContent" + j + "'>" + a.Rows[j]["OrderContent"] + "</lable> </td><td  ><lable class='labSpecsModels" + j + "  id='SpecsModels" + j + "'>" + a.Rows[j]["Specifications"] + "</lable></td> <td><lable class='labOrderUnit" + j + "  id='OrderUnit" + j + "'>" + a.Rows[j]["Unit"] + "</lable></td> <td  ><lable class='labTechnology" + j + "'  id='Technology" + j + "'>" + a.Rows[j]["Amount"] + "</lable></td><td ><lable class='labDeliveryTime" + j + "  id='DeliveryTime" + j + "'>" + a.Rows[j]["Remark"] + "</lable></td></tr>");
                    }
                    if ((length - b) < 26)
                    {
                    }
                    //sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }

            Response.Write(html);
            return View();
        }


        #endregion
        #endregion

        #endregion

        #region 生产计划
        public ActionResult projectmanagement()
        {
            return View();
        }
        #endregion

        #region 退换货
        public ActionResult ExchangeGoodsManage()
        {
            ViewData["webkey"] = "退货检验单审批";
            string fold = COM_ApprovalMan.getNewwebkey("退货检验单审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
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

        public ActionResult ExchangeCheckGoods()
        {
            Exchange_Check ExCheck = new Exchange_Check();
            ExCheck.TID = SalesManage.getNewExCheckID();
            ExCheck.RememberPeople = GAccount.GetAccountInfo().UserName;
            return View(ExCheck);
        }

        public ActionResult btnCheck()
        {
            return View();
        }

        public ActionResult getCHECK()
        {
            string where = "  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "6";

            string EID = Request["EID"].ToString();

            if (EID != "")
                where += " a.EID like '%" +EID + "%' and";

            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = ProduceMan.getCHECK(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getExchangeCheck()
        {
            string where = "  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "6";

            string TID = Request["TID"].ToString();
            string StartDate = Request["StartDate"].ToString();
            if (StartDate != "")
                StartDate += " 00:00:00";

            string EndDate = Request["EndDate"].ToString();
            if (EndDate != "")
                EndDate += " 23:59:59";

            string State = Request["State"].ToString();

            if (TID != "")
                where += " a.TID like '%" + TID + "%' and";
            if (StartDate != "" && EndDate != "")
                where += " a.ChangeDate between '" + StartDate + "' and '" + EndDate + "' and";
           
            if (State == "-1")
            {
                where += "";
            }
            else
            {
                where += " a.ProductionState= '" + State + "' and";
            }

            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = ProduceMan.getExchangeCheck(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getExchangeCheckDetail()
        {
            string where = "  and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "6";

            string TID = Request["TID"].ToString();

            where = TID;

            UIDataTable udtTask = ProduceMan.getExchangeCheckDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSPPD()
        {
            string TID = Request["TID"].ToString();
            DataTable dt = ProduceMan.getSPPD(TID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult getPDSPCK()
        {
            string TID = Request["TID"].ToString();
            DataTable dt = ProduceMan.getPDSPCK(TID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SubmitApproval(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }

        //列表
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
            if (ProduceMan.InsertNewApproval(PID, RelevanceID, webkey, folderBack, ref strErr) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }

        //审批
        public ActionResult Approval(string id)
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

        public ActionResult UpdateApproval()
        {
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            string strErr = "";
            if (ProduceMan.UpdateNewApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        #endregion

        #region 上传报告文档类型设置
        public ActionResult BGLX()
        {
            return View();
        }

        public ActionResult SZBGLX()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string SID = Request["SID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (SID != "")
                where += " SID = '" + SID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProduceMan.SZBGLX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getText()
        {
            string SID = Request["SID"].ToString();
            string text = Request["text"].ToString();
            DataTable dt = ProduceMan.getText(SID, text);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult THBGLX()
        {
            return View();
        }
        public ActionResult THBGLXs()
        {
            string SID = Request["SID"].ToString();
            DataTable dt = ProduceMan.THBGLXs(SID);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SCBGLX()
        {
            string strErr = "";
            string Text = Request["Text"].ToString();

            if (ProduceMan.SCBGLX(Text, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["Text"].ToString();
                log.YYType = "删除成功 ";
                log.Content = "删除文档类型";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["Text"].ToString();
                log.YYType = "删除失败 ";
                log.Content = "删除文档类型";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult SaveBGLX()
        {
            string SID = Request["SID"].ToString();

            string[] arrMain = Request["MainContent"].Split(',');
            string[] arrText = Request["Text"].Split(',');


            string strErr = "";
            tk_ConfigContent deInfo = new tk_ConfigContent();
            List<tk_ConfigContent> detailList = new List<tk_ConfigContent>();
            for (int i = 0; i < arrMain.Length; i++)
            {
                deInfo = new tk_ConfigContent();
                if (SID == "上传的文件类型")
                {
                    deInfo.XID = 1;
                    deInfo.SID = "上传的文件类型";
                    deInfo.Text = arrText[i].ToString();
                }
                if (SID == "随工单班组类型")
                {
                    deInfo.XID = 2;
                    deInfo.SID = "随工单班组类型";
                    deInfo.Text = arrText[i].ToString();
                }
                if (SID == "随工单工序类型")
                {
                    deInfo.XID = 3;
                    deInfo.SID = "随工单工序类型";
                    deInfo.Text = arrText[i].ToString();
                }
                detailList.Add(deInfo);
            }
            bool b = ProduceMan.SaveBGLX(detailList, ref strErr);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = "";
                log.YYType = "添加成功 ";
                log.Content = "添加上传文件的类型";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = true });
            }
            else
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = "";
                log.YYType = "添加失败 ";
                log.Content = "添加上传文件的类型";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }
        #endregion
    }
}
