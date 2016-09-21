using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    public class SystemManageController : Controller
    {
        //
        // GET: /SystemManage/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BasInformation()
        {
            return View();
        }
        public ActionResult BasMangeGrid()
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
            if (type != "")//在此处做判断
                where += " and a.Type ='" + type + "' ";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SystemManage.getNewBasMangeGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertContent()
        {
            var type = Request["Type"];
            var text = Request["Text"];
            string strErr = "";
            if (SystemManage.InsertNewContent(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteContent()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (SystemManage.DeleteNewContent(xid, type, ref strErr) == true)
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
            if (SystemManage.UpdateNewContent(XID, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

    }
}
