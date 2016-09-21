using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    public class FlowSystemController : Controller
    {
        //
        // GET: /FlowSystem/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BasicSet()
        {
            return View();
        }
        //
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
                udtTask = FlowSystemMan.getBasicGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 新增
        public ActionResult AddBasic(string id)
        {
            ViewData["type"] = id;
            return View();
        }

        // 确认新增
        public ActionResult InsertBasic()
        {
            var type = Request["Type"];
            var text = Request["Text"];
            string strErr = "";
            if (FlowSystemMan.InsertBasic(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 修改
        public ActionResult UpdateBasic(string Info)
        {
            string[] arr = Info.Split('@');
            ViewData["XID"] = arr[0];
            ViewData["Type"] = arr[1];
            ViewData["Text"] = arr[2];
            return View();
        }

        // 确认修改 
        public ActionResult ModifyBasic()
        {
            var XID = Request["XID"];
            var Type = Request["Type"];
            var Text = Request["Text"];
            string strErr = "";
            if (FlowSystemMan.ModifyBasic(XID, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 删除
        public ActionResult DeleteBasic()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (FlowSystemMan.DeleteBasic(xid, type, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }



        // 加载小组
        public ActionResult getGroupList()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowSystemMan.getGroupList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //
        public ActionResult getPersonList()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string GroupID = Request["GroupID"].ToString();
            where = " and b.GroupID='" + GroupID + "' ";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowSystemMan.getPersonList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 小组人员配置 
        public ActionResult GroupSet()
        {
            return View();
        }

        // 新增小组
        public ActionResult AddGroup()
        {
            return View();
        }

        // 确认新增小组
        public ActionResult InsertGroup()
        {
            var text = Request["Text"];
            string strErr = "";
            if (FlowSystemMan.InsertGroup(text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 修改小组
        public ActionResult UpdateGroup(string Info)
        {
            string[] arr = Info.Split('@');
            ViewData["GroupID"] = arr[0];
            ViewData["Text"] = arr[1];
            return View();
        }

        // 确认修改 
        public ActionResult ModifyGroup()
        {
            var GroupID = Request["GroupID"];
            var Text = Request["Text"];
            string strErr = "";
            if (FlowSystemMan.ModifyGroup(GroupID, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 删除小组
        public ActionResult DeleteGroup()
        {
            var gid = Request["data1"];
            string strErr = "";
            if (FlowSystemMan.DeleteGroup(gid, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }



        // 新增小组人员
        public ActionResult AddGroupUser(string Info)
        {
            ViewData["GroupID"] = Info;
            return View();
        }

        // 确认新增小组人员
        public ActionResult InsertGroupUser()
        {
            var text = Request["Text"];
            var GroupID = Request["GroupID"];
            string strErr = "";
            if (FlowSystemMan.InsertGroupUser(text, GroupID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 修改小组人员
        public ActionResult UpdateGroupUser(string Info)
        {
            string[] arr = Info.Split('@');
            ViewData["UserID"] = arr[0];
            ViewData["Text"] = arr[1];
            ViewData["GroupID"] = arr[2];
            return View();
        }

        // 确认修改 小组人员
        public ActionResult ModifyGroupUser()
        {
            var UserID = Request["UserID"];
            var Text = Request["Text"];
            var GroupID = Request["GroupID"];
            string strErr = "";
            if (FlowSystemMan.ModifyGroupUser(UserID, Text, GroupID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        // 删除小组人员
        public ActionResult DeleteGroupUser()
        {
            var uid = Request["data1"];
            var gid = Request["data2"];
            string strErr = "";
            if (FlowSystemMan.DeleteGroupUser(uid, gid, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }



    }
}
