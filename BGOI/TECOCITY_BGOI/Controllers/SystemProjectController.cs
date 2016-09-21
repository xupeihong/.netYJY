using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    public class SystemProjectController : Controller
    {
        //
        // GET: /SystemProject/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BasicSet()
        {
            return View();
        }

        public ActionResult BasicGrid()
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
            string type = Request["sel"].ToString();
            if (type != "")
                where += " and a.Type ='" + type + "' ";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SystemProjectMan.getNewBasicGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContent(string id)
        {
            ViewData["type"] = id;
            return View();
        }

        public ActionResult InsertContent()
        {
            var type = Request["Type"];
            var text = Request["Text"];
            string strErr = "";
            if (SystemProjectMan.InsertNewContent(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult UpdateContent(string id)
        {
            string[] arr = id.Split('@');
            ViewData["XID"] = arr[0];
            ViewData["Type"] = arr[1];
            ViewData["Text"] = arr[2];
            return View();
        }

        public ActionResult upNewContent()
        {
            var XID = Request["XID"];
            var Type = Request["Type"];
            var Text = Request["Text"];
            string strErr = "";
            if (SystemProjectMan.UpdateNewContent(XID, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult DeleteContent()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (SystemProjectMan.DeleteNewContent(xid, type, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

    }
}
