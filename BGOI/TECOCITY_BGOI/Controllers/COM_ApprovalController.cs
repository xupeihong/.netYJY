using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI.Controllers
{
    public class COM_ApprovalController : Controller
    {
        //
        // GET: /COM_Approval/

        public ActionResult Index()
        {
            return View();
        }

        //提交审批页面
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
        //提交审批页面列表
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
            if (COM_ApprovalMan.InsertNewApproval(PID, RelevanceID, webkey, folderBack, ref strErr) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        // 审批流程控制审批按钮可用不可用
        public ActionResult JudgeAppDisable()
        {
            var webkey = Request["data1"];
            var SPID = Request["data2"];
            Acc_Account account = GAccount.GetAccountInfo();
            string logUser = account.UserID.ToString();
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            int bol = COM_ApprovalMan.JudgeNewLoginUser(logUser, webkey, folderBack, SPID);
            return Json(new { success = "true", intblo = bol });
        }

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
        public ActionResult ApprovalSup(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            string RelevanceID = arr[2];
            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            suges = SupplyManage.GetSuge(RelevanceID);
            info = SupplyManage.getProceinfo(RelevanceID);
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;

            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        public ActionResult ApprovalzhunchuSup(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[1];
            string RelevanceID = arr[2];
            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            suges = SupplyManage.GetSuge(RelevanceID);
            info = SupplyManage.getProceinfo(RelevanceID);
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;

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
            if (COM_ApprovalMan.UpdateNewApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        public ActionResult UpdateApprovalSup()
        {
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            string strErr = "";
            if (COM_ApprovalMan.UpdateNewzhunchuApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
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
                udtTask = COM_ApprovalMan.getNewConditionGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChoseGoods(string id)
        {
            ViewData["type"] = id;
            return View();
        }

        public ActionResult getProduct()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string UnitID = account.UnitID;
            DataTable dt = COM_ApprovalMan.getNewProductByUnitID(UnitID);
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            //string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //id += dt.Rows[i]["PID"].ToString() + ",";
                name += dt.Rows[i]["ProName"].ToString() + ",";
            }
            //id = id.TrimEnd(',');
            name = name.TrimEnd(',');
            return Json(new { success = "true", Strname = name });
        }

        public ActionResult SelectProduct()
        {
            var ProductName = Request["data1"];
            Acc_Account account = GAccount.GetAccountInfo();
            string UnitID = account.UnitID;
            DataTable dt = COM_ApprovalMan.getNewProductByName(ProductName, UnitID);
            string name = "";
            string spc = "";
            string pid = "";
            string units = "";

            string unitprice = "";
            string price2 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                name += dt.Rows[i]["ProName"] + "$";
                spc += dt.Rows[i]["Spec"] + "$";
                pid += dt.Rows[i]["PID"] + "$";
                units += dt.Rows[i]["Units"] + "$";

                unitprice += dt.Rows[i]["UnitPrice"] + "$";
                price2 += dt.Rows[i]["Price2"] + "$";
            }
            name = name.TrimEnd('$');
            //spc = spc.TrimEnd('$');
            spc = spc.Remove(spc.LastIndexOf("$"), 1);
            pid = pid.TrimEnd('$');
            units = units.TrimEnd('$');

            unitprice = unitprice.TrimEnd('$');
            price2 = price2.TrimEnd('$');
            return Json(new { success = "true", Strname = name, Strspc = spc, Stpid = pid, strunits = units, Strunitprice = unitprice, Strprice2 = price2 });
        }

        public ActionResult InsertTemporary()
        {
            var ProductName = Request["data1"];
            var Spc = Request["data2"];
            var Pid = Request["data3"];
            var Num = Request["data4"];
            var RelevanceID = Request["data5"];
            var dataT = Request["data6"];
            string strErr = "";
            if (COM_ApprovalMan.InsertNewTemporary(ProductName, Spc, Pid, Num, RelevanceID, dataT, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }

    }
}
