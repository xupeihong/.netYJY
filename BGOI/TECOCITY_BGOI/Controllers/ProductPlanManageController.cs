using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    public class ProductPlanManageController : Controller
    {
        //
        // GET: /ProductPlanManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductPlanGrid()
        {
            ViewData["webkey"] = "生产计划审批";
            string fold = COM_ApprovalMan.getNewwebkey("生产计划审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

       

        public ActionResult Approval()
        {
            ViewData["webkey"] = "生产计划审批";
            string fold = COM_ApprovalMan.getNewwebkey("生产计划审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult AppExamineGrid()
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
            UIDataTable udtTask = ProjectMan.getnproductExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, "getAppExamineProject");
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RWApproval()
        {
            ViewData["webkey"] = "生产任务审批";
            string fold = COM_ApprovalMan.getNewwebkey("生产任务审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }



        public ActionResult RWAppExamineGrid()
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
            UIDataTable udtTask = ProjectMan.getnproductExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, "getrwAppExamineProject");
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductPlanGrid()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage1"] != null)
                strCurPage = Request["curpage1"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string JHID = Request["JHID"].ToString();


            UIDataTable udtTask = ProducePlanMan.GetProductPlan(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage1"]) - 1, JHID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage1"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductPlan(ProduceList JH)
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

                string Name = JH.Name;
                string Specifications = Request["Specifications"].ToString();

                string Starts = Request["Starts"].ToString();
                if (Starts != "")
                    Starts += " 00:00:00";

                string Starte = Request["Starte"].ToString();
                if (Starte != "")
                    Starte += " 23:59:59";

                string Type = Request["Type"].ToString();
                string Type1 = Request["Type1"].ToString();
                string Type2 = Request["Type2"].ToString();
                string Type3 = Request["Type3"].ToString();
                string State = Request["State"].ToString();

                if (Request["Name"] != "")
                    where += " and b.Name like '%" + Request["Name"] + "%'";
                if (Specifications != "")
                    where += " and b.Specifications = '" + Specifications + "'";
                if (Starts != "" && Starte != "")
                    where += " and a.Specifieddate between '" + Starts + "' and '" + Starte + "'";
                if (Type != "" && Type2 != "")
                    where += " and a.Plannedyear between '" + Type + "' and '" + Type2 + "'";
                if (Type1 != "" && Type3 != "")
                    where += " and a.Plannedmonth between '" + Type1 + "' and '" + Type3 + "'";
                if (State != "")
                    where += " and a.State = '" + State + "'";


                //where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = ProducePlanMan.getPlanList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
                //return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        #region 制定任务单

        public string ProJHNum(string JHID)
        {
            return ProduceMan.ProJHNum(JHID);
        }
        public ActionResult ProductPlanAdd()
        {
            tk_Product_Plan si = new TECOCITY_BGOI.tk_Product_Plan();
            si.JHID = ProduceMan.GetTopJHID();
            si.Formulation = GAccount.GetAccountInfo().UserName;
            return View(si);
        }

        public ActionResult GetZD()
        {
            DataTable dt = ProducePlanMan.GetZD();
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SaveProductPlan(tk_Product_Plan plan)
        {
            plan.CreateTime = DateTime.Now;
            plan.State = "0";
            plan.Validate = "v";
            plan.CreateUser = GAccount.GetAccountInfo().UserName;
            int count = Convert.ToInt32(Request["tbadyrows"]);

            tk_Product_PlanDetail deInfo = null;
            List<tk_Product_PlanDetail> detailList = new List<tk_Product_PlanDetail>();
            for (int i = 0; i < count; i++)
            {
                string did = "";
                if (i > 8)
                    did = plan.JHID + "-" + (i + 1);
                else
                    did = plan.JHID + "-0" + (i + 1);
                deInfo = new tk_Product_PlanDetail()
                {
                    PID = Request["PID" + i].ToString(),
                    Name = Request["ProName" + i].ToString(),
                    Specifications = Request["Specifications" + i].ToString(),
                    Finishedproduct = Request["Finishedproduct" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["Finishedproduct" + i]),
                    finishingproduct = Request["finishingproduct" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["finishingproduct" + i]),
                    OnlineCount = Request["OnlineCount" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["OnlineCount" + i]),
                    Spareparts = Request["lj" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["lj" + i]),
                    notavailable = Request["notavailable" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["notavailable" + i]),
                    Total = Request["Total" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["Total" + i]),
                    plannumber = Request["plannumber" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["plannumber" + i]),
                    demandnumber = Request["demandnumber" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["demandnumber" + i]),
                    Remarks = Request["Remarks" + i].ToString(),
                    JHID = plan.JHID,
                    DID = did,
                    CreateTime = DateTime.Now,
                    CreateUser = GAccount.GetAccountInfo().UserName,
                    Validate = "v"
                };
                detailList.Add(deInfo);
            }
            string strErr = "";
            bool b = ProducePlanMan.SaveProductPlan(plan, detailList, ref strErr);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["JHID"].ToString();
                log.YYType = "添加成功 ";
                log.Content = "制定计划单";
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
                log.YYCode = Request["JHID"].ToString();
                log.YYType = "添加失败 ";
                log.Content = "制定计划单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }
        }

        #endregion

        #region  修改计划单
        public ActionResult LoadPlanDatail()
        {
            string JHID = Request["JHID"].ToString();
            DataTable dt = ProducePlanMan.LoadPlanDatail(JHID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult UpdatePlan()
        {
            string JHID = Request["JHID"].ToString();
            tk_Product_Plan Material = ProducePlanMan.IndexAllupdatePlan(JHID);
            ViewData["JHID"] = Request["JHID"].ToString();
            ViewData["Specifieddate"] = Material.Specifieddate.ToString("yyyy-MM-dd");
            return View(Material);
        }

        public ActionResult SaveUpdatePlan(tk_Product_Plan plan)
        {
            int count = Convert.ToInt32(Request["tbadyrows"]);

            tk_Product_PlanDetail deInfo = null;
            List<tk_Product_PlanDetail> detailList = new List<tk_Product_PlanDetail>();
            for (int i = 0; i < count; i++)
            {
                string did = "";
                if (i > 8)
                    did = plan.JHID + "-" + (i + 1);
                else
                    did = plan.JHID + "-0" + (i + 1);
                deInfo = new tk_Product_PlanDetail()
                {
                    PID = Request["PID" + i].ToString(),
                    Name = Request["Name" + i].ToString(),
                    Specifications = Request["Specifications" + i].ToString(),
                    Finishedproduct = Request["Finishedproduct" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["Finishedproduct" + i]),
                    finishingproduct = Request["finishingproduct" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["finishingproduct" + i]),
                    OnlineCount = Request["OnlineCount" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["OnlineCount" + i]),
                    Spareparts = Request["Spareparts" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["Spareparts" + i]),
                    notavailable = Request["notavailable" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["notavailable" + i]),
                    Total = Request["Total" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["Total" + i]),
                    plannumber = Request["plannumber" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["plannumber" + i]),
                    demandnumber = Request["demandnumber" + i].ToString() == "" ? 0 : Convert.ToInt32(Request["demandnumber" + i]),
                    Remarks = Request["Remarks" + i].ToString(),
                    JHID = plan.JHID,
                    DID = did,
                    CreateTime = Convert.ToDateTime( Request["CreateTime" + i]),
                    CreateUser = Request["CreateUser" + i],
                    Validate = "v"
                };
                detailList.Add(deInfo);
            }
            string strErr = "";
            bool b = ProducePlanMan.SaveUpdatePlan(plan, detailList, ref strErr);
            if (b)
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = Request["JHID"].ToString();
                log.YYType = "修改成功 ";
                log.Content = "修改计划单";
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
                log.YYCode = Request["JHID"].ToString();
                log.YYType = "修改失败 ";
                log.Content = "修改计划单";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = false, Msg = strErr });
            }

        }




        #region 废除修改保存
        //public ActionResult SaveUpdatePlan()
        //{
        //    tk_Product_Plan task = new tk_Product_Plan();
        //    task.JHID = Request["JHID"].ToString();
        //    task.Specifieddate = Convert.ToDateTime(Request["Specifieddate"]);
        //    task.Plannedmonth = Request["Plannedmonth"].ToString();
        //    task.Remarks = Request["Remarks"].ToString();
        //    task.Formulation = Request["Formulation"].ToString();


        //    string[] arrMain = Request["MainContent"].Split(',');
        //    string[] arrDID = Request["DID"].Split(',');
        //    string[] arrName = Request["Name"].Split(',');
        //    string[] arrSpecifications = Request["Specifications"].Split(',');
        //    string[] arrFinishedproduct = Request["Finishedproduct"].Split(',');
        //    string[] arrfinishingproduct = Request["finishingproduct"].Split(',');
        //    string[] arrfinishingproducts = Request["finishingproducts"].Split(',');
        //    string[] arrnotavailable = Request["notavailable"].Split(',');
        //    string[] arrTotal = Request["Total"].Split(',');
        //    string[] arrplannumber = Request["plannumber"].Split(',');
        //    string[] arrdemandnumber = Request["demandnumber"].Split(',');
        //    string[] arrRemark = Request["Remark"].Split(',');

        //    string strErr = "";
        //    tk_Product_PlanDetail deInfo = new tk_Product_PlanDetail();
        //    List<tk_Product_PlanDetail> detailList = new List<tk_Product_PlanDetail>();
        //    for (int i = 0; i < arrMain.Length; i++)
        //    {
        //        deInfo = new tk_Product_PlanDetail();
        //        deInfo.JHID = Request["JHID"].ToString();
        //        deInfo.DID = arrDID[i].ToString();
        //        deInfo.Name = arrName[i].ToString();
        //        deInfo.Specifications = arrSpecifications[i].ToString();
        //        deInfo.Finishedproduct = Convert.ToInt32(arrFinishedproduct[i]);
        //        deInfo.finishingproduct = Convert.ToInt32(arrfinishingproduct[i]);
        //        deInfo.Spareparts = Convert.ToInt32(arrfinishingproducts[i]);
        //        deInfo.notavailable = Convert.ToInt32(arrnotavailable[i]);
        //        deInfo.Total = Convert.ToInt32(arrTotal[i]);
        //        deInfo.plannumber = Convert.ToInt32(arrplannumber[i]);
        //        deInfo.demandnumber = Convert.ToInt32(arrdemandnumber[i]);
        //        deInfo.Remarks = arrRemark[i].ToString();

        //        detailList.Add(deInfo);
        //    }
        //    var JHID = Request["JHID"].ToString();
        //    bool b = ProduceMan.SaveUpdatePlan(task, detailList, ref strErr, JHID);
        //    if (b)
        //    {
        //        #region [添加日志]
        //        tk_ProLog log = new tk_ProLog();
        //        log.LogTime = DateTime.Now;
        //        log.YYCode = Request["JHID"].ToString();
        //        log.YYType = "修改成功 ";
        //        log.Content = "修改计划单";
        //        log.Actor = GAccount.GetAccountInfo().UserName;
        //        log.Unit = GAccount.GetAccountInfo().UnitName;
        //        ProduceMan.AddProduceLog(log);
        //        #endregion
        //        return Json(new { success = true });
        //    }
        //    else
        //    {
        //        #region [添加日志]
        //        tk_ProLog log = new tk_ProLog();
        //        log.LogTime = DateTime.Now;
        //        log.YYCode = Request["JHID"].ToString();
        //        log.YYType = "修改失败 ";
        //        log.Content = "修改计划单";
        //        log.Actor = GAccount.GetAccountInfo().UserName;
        //        log.Unit = GAccount.GetAccountInfo().UnitName;
        //        ProduceMan.AddProduceLog(log);
        //        #endregion
        //        return Json(new { success = false, Msg = strErr });
        //    }
        //}
        #endregion
        #endregion

        #region 撤销计划单
        #region 撤销
        public ActionResult CXJH()
        {
            string strErr = "";
            string JHID = Request["JHID"].ToString();

            if (ProduceMan.CXJH(JHID, ref strErr))
            {
                #region [添加日志]
                tk_ProLog log = new tk_ProLog();
                log.LogTime = DateTime.Now;
                log.YYCode = JHID;
                log.YYType = "撤销成功 ";
                log.Content = "撤销计划";
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
                log.YYCode = JHID;
                log.YYType = "撤销失败 ";
                log.Content = "撤销计划";
                log.Actor = GAccount.GetAccountInfo().UserName;
                log.Unit = GAccount.GetAccountInfo().UnitName;
                ProduceMan.AddProduceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion
        #endregion

        #region  打印
        public ActionResult PrintJH()
        {
            string JHID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(JHID))
            {
                s += " JHID like '%" + JHID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_Produce.dbo.tk_Product_Plan ";
            DataTable data = ProduceMan.PrintJHs(where, tableName, ref strErr);
            tk_Product_Plan so = new TECOCITY_BGOI.tk_Product_Plan();
            foreach (DataRow dt in data.Rows)
            {
                so.JHID = dt["JHID"].ToString();
                so.Specifieddate = Convert.ToDateTime(dt["Specifieddate"]);
                ViewData["Specifieddate"] = so.Specifieddate.ToString("yyyy年MM月dd日");
            }
            return View(so);
        }

        public ActionResult PrintJHs()
        {
            string JHID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(JHID))
            {
                s += " JHID like '%" + JHID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_Produce.dbo.tk_Product_PlanDetail  ";
            DataTable dt = ProduceMan.PrintJHs(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        #endregion

        public ActionResult CreateProjectLogGrid()
        {
           
            string PID="";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "7";

            string where = Request["PID"].ToString();
            UIDataTable udtTask = ProduceMan.CreateProjectLogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
