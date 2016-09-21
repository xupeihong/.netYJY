using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace TECOCITY_BGOI.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /System/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppSet(string id)
        {
            string[] arr = id.Split('/');
            ViewData["data"] = arr[0];
            ViewData["table"] = arr[1];
            return View();
        }

        public ActionResult CUExamine(string id)
        {
            ViewData["Content"] = id;
            return View();
        }

        public ActionResult GetLevel()
        {
            DataTable dt = SystemMan.GetNewConfigCont("AppLevel");
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["SID"].ToString() + ",";
                name += dt.Rows[i]["Text"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }

        public ActionResult GetUser()
        {
            DataTable dt = SystemMan.GetNewUser();
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["UserId"].ToString() + "/" + dt.Rows[i]["ExJob"].ToString() + ",";
                name += dt.Rows[i]["UserName"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }

        public ActionResult GetAppType()
        {
            DataTable dt = SystemMan.getNewAppType("AppType");
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["SID"].ToString() + ",";
                name += dt.Rows[i]["Text"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }

        public ActionResult ChooseUser(string id, string userIDs)
        {
            ViewData["parentID"] = id;
            ViewData["userIDs"] = userIDs;
            return View();
        }

        public ActionResult UserGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = " and a.DeptId = '" + account.UnitID + "'";
            string where2 = "";
            string strCurPage;
            string strRowNum;
            string UserName = Request["uaskname"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (UserName != "")
                where2 += " and m.UserName like '%" + UserName + "%'";
            UIDataTable udtTask = SystemMan.getNewUserGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, where2);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertExamine()
        {
            var Butype = Request["Content"];
            var allcontent = Request["allcontent"];
            string strErr = "";
            if (SystemMan.InsertNewExamine(Butype,allcontent, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult ExamineGrid()
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
            UIDataTable udtTask = new UIDataTable();
            if(where != "")
                udtTask = SystemMan.getNewExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHaveContent()
        {
            var butype = Request["data1"];
            string content = SystemMan.getNewHaveExaminContent(butype);
            if(content != "")
                return Json(new { success = "true", Msg = content });
            else
                return Json(new { success = "false", Msg = content });
        }

    }
}
