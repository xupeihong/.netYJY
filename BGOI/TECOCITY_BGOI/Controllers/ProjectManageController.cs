using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    public class ProjectManageController : Controller
    {
        //
        // GET: /ProjectManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult General()
        {
            return View();
        }

        public ActionResult GeneralGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "46" || UnitID == "32")
                {
                    where = "";
                }
                else
                {
                    where = " and a.UnitID = '" + account.UnitID + "'";
                }
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult LoadSetUp()
        {
            var PID = Request["data1"];
            if (PID == null) return Json(new { success = "false" });
            string where = " and a.PID = '" + PID + "'";
            DataTable dt = ProjectMan.getNewDetailApp(where);
            if (dt.Rows[0]["Principal"].ToString() == "")
                return Json(new { success = "false" });
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\">项目名称</td><td class=\"textRight\">" + dt.Rows[0]["Pname"].ToString() + "</td><td class=\"textLeft\">立项时间</td><td class=\"textRight\">" + dt.Rows[0]["AppDate"].ToString() + "</td><td class=\"textLeft\">立项编号</td><td class=\"textRight\">" + dt.Rows[0]["AppID"].ToString() + "</td>");
            sb.Append("<td class=\"textLeft\">项目负责人</td><td class=\"textRight\">" + dt.Rows[0]["Principal"].ToString() + "</td><td class=\"textLeft\">配合负责人</td><td class=\"textRight\">" + dt.Rows[0]["ConcertPerson"].ToString() + "</td><td class=\"textLeft\">项目来源</td><td class=\"textRight\">" + dt.Rows[0]["PsourceDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目地点</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Paddress"].ToString() + "</td><td class=\"textLeft\">建设单位</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BuildUnit"].ToString() + "</td><td class=\"textLeft\">联系人</td><td class=\"textRight\">" + dt.Rows[0]["LinkMan"].ToString() + "</td><td class=\"textLeft\">电话</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Phone"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:50px;\"><td colspan=\"12\">项目简介：" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目合同额</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ContractAmount"].ToString() + "万元</td><td class=\"textLeft\" colspan=\"2\">项目前期费用：（管理费、预算）</td><td class=\"textRight\">" + dt.Rows[0]["Budget"].ToString() + "万元</td>");
            sb.Append("<td class=\"textLeft\">项目成本</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Cost"].ToString() + "万元</td><td class=\"textLeft\">项目利润</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Profit"].ToString() + "万元</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">当前进度</td><td class=\"textRight\" colspan=\"5\">" + dt.Rows[0]["Schedule"].ToString() + "</td><td class=\"textLeft\">预计合同签订日期</td><td class=\"textRight\" colspan=\"5\">" + dt.Rows[0]["PlanSignaDate"].ToString() + "</td></tr>");
            sb.Append("</table>");
            if (sb.ToString() != "")
                return Json(new { success = "true", html = sb.ToString() });
            else
                return Json(new { success = "false"});
        }

        public ActionResult CreateProject()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JudgeSameProID(string StrProID)
        {
            List<string> list = ProjectMan.GetNewProID();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == StrProID.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult JudgeSameAppID(string StrAppID)
        {
            List<string> list = ProjectMan.GetNewAppID();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == StrAppID.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewProject()
        {
            tk_ProjectPre ProjectBas = new tk_ProjectPre();
            ProjectBas.StrPID = ProjectMan.GetNewShowPid();
            return View(ProjectBas);
        }

        public ActionResult InsertProjectBas(tk_ProjectPre ProjectBas)
        {
            if (ModelState.IsValid)
            {
                //如果没错误，返回首页
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                ProjectBas.StrPID = ProjectMan.GetNewPid();
                ProjectBas.StrCreateUser = account.UserID.ToString();
                ProjectBas.StrCreateTime = DateTime.Now;
                ProjectBas.StrUnitID = account.UnitID;
                ProjectBas.StrValidate = "v";
                if (ProjectMan.InsertNewProject(ProjectBas, ref strErr) == true)
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

        public ActionResult UpdateProjectBas(string id)
        {
            if (id == null)
            {
                tk_ProjectPre ProjectBasr = new tk_ProjectPre();
                return View(ProjectBasr);
            }
            tk_ProjectPre ProjectBas = new tk_ProjectPre();
            ProjectBas = ProjectMan.getNewUpdateProjectBas(id);
            return View(ProjectBas);
        }

        public ActionResult UpProjectBas(tk_ProjectPre ProjectBas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewProjectBas(ProjectBas, ref strErr) == true)
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

        public ActionResult deleteBas()
        {
            var PID = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewProjectBas(PID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult ProjectPrepare()
        {
            return View();
        }

        public ActionResult ProjectGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "46" || UnitID == "32")
                {
                    where = " and a.State = '0'";
                }
                else
                {
                    where = " and a.State = '0' and a.UnitID = '" + account.UnitID + "'";
                }
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                UIDataTable udtTask = ProjectMan.getNewProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult CreateProjectUserLogGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            string Type = Request["Type"].ToString();
            string Type2 = "";
            if (Request["PID"] != null)
                PID = Request["PID"].ToString();
            if (Request["Type2"] != null)
                Type2 = Request["Type2"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (PID != "")
                where += " and a.RelevanceID = '" + PID + "'";
            if (Type != "")
                where += " and a.Type = '"+Type+"'";
            if (Type2 != "")
                where += " or a.Type = '" + Type2 + "'";
            UIDataTable udtTask = new UIDataTable();
            if (PID != "")
                udtTask = ProjectMan.getNewUserLogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EarlyContact(string id)
        {
            if (id == null)
            {
                tk_Project Projectr = new tk_Project();
                return View(Projectr);
            }
            tk_Project Project = new tk_Project();
            Acc_Account account = GAccount.GetAccountInfo();
            Project.StrFollowPerson = account.UserName;
            Project.StrPID = id;
            return View(Project);
        }

        public ActionResult ChangeJQ()
        {
            var PID = Request["data1"];
            var JQ = Request["data2"];
            DataTable dt = ProjectMan.changeNewJQType(PID, JQ);
            string content = "";
            string man = "";
            if (dt.Rows.Count > 0) 
            {
                content = dt.Rows[0]["Content"].ToString();
                man = dt.Rows[0]["CreatePerson"].ToString();
            }
            return Json(new { success = "true",Content = content,Man = man });
        }

        public ActionResult InsertEarlyContact(tk_Project Project)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Project.StrUnitID = account.UnitID;
                Project.StrCreateTime = DateTime.Now;
                Project.StrCreatePerson = account.UserID.ToString();
                Project.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewProject(Project, ref strErr) == true)
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

        public ActionResult UpdateEarlyContact(string id)
        {
            if (id == null)
            {
                tk_Project Projectr = new tk_Project();
                return View(Projectr);
            }
            ViewData["EID"] = id;
            tk_Project Project = new tk_Project();
            Project = ProjectMan.getNewUpdateProject(id);
            return View(Project);
        }

        public ActionResult UpEarlyContact(tk_Project Project)
        {
            if (ModelState.IsValid)
            {
            var EID = Request["EID"];
            string strErr = "";
            if (ProjectMan.UpdateNewProject(EID,Project, ref strErr) == true)
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

        public ActionResult deleteJQ()
        {
            var texts = Request["data1"];
            string[] arr = texts.Split('@');
            string strErr = "";
            if (ProjectMan.DeleteNewProject(arr[0],arr[1], ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult ProjectQQGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if(Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if(where != "")
                udtTask = ProjectMan.getNewProjectQQGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrepareGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;
                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.CreateTime >= '" + start + "' and a.CreateTime <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if(UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and a.UnitID = '" + account.UnitID + "'";
                }
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                    udtTask = ProjectMan.getNewPrepareGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult DetailProject(string id)
        {
            string where = " and a.PID = '"+id+"'";
            DataTable dt = ProjectMan.getNewDetailBas(where);
            DataTable dtJ = ProjectMan.getNewDetailJQ(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div  id=\"tabTitile\"style=\"font-size:15px;\">项目编号：" + id + "</div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\">内部编号</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ProID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Pname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目来源</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["PsourceDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">客户名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["CustomerName"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:50px;\"><td class=\"textLeft\">项目目标</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Goal"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:100px;\"><td class=\"textLeft\">项目概述</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">创建时间</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["CreateTime"].ToString() + "</td></tr>");
            if (dtJ.Rows.Count > 0)
            {
                sb.Append("<tr><td class=\"textRight\" colspan=\"3\" style=\"text-align:center;font-weight:bold;\">前期接洽信息</td></tr>");
                sb.Append("<tr><td class=\"textLeft\" style=\"width:15%\">前期接洽类型</td><td class=\"textLeft\" style=\"width:60%\">接洽内容概述</td><td class=\"textLeft\" style=\"width:25%\">跟踪人</td></tr>");
                for (int i = 0; i < dtJ.Rows.Count; i++)
                {
                    sb.Append("<tr><td class=\"textRight\">" + dtJ.Rows[i]["JQTypeDesc"].ToString() + "</td><td class=\"textRight\">" + dtJ.Rows[i]["Pview"].ToString() + "</td><td class=\"textRight\">" + dtJ.Rows[i]["FollowPerson"].ToString() + "</td></tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult AppProject()
        {
            return View();
        }

        public ActionResult AppProjectGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += " and a.State != 0 and a.State != '8'";
                    else
                        where += " and a.State != 0 and a.State != '8' and a.UnitID = '" + account.UnitID + "'";
                }
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                    udtTask = ProjectMan.getNewAppProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult UseSetUpProject()
        {
            UseProjectBas ProjectBas = new UseProjectBas();
            ProjectBas.StrPID = ProjectMan.GetNewShowPid();
            return View(ProjectBas);
        }

        public ActionResult InsertUseProjectBas(UseProjectBas ProjectBas)
        {
            if (ModelState.IsValid)
            {
                //如果没错误，返回首页
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                ProjectBas.StrPID = ProjectMan.GetNewPid();
                ProjectBas.StrAppUser = account.UserID.ToString();
                ProjectBas.StrCreateUser = account.UserID.ToString();
                ProjectBas.StrCreateTime = DateTime.Now;
                ProjectBas.StrUnitID = account.UnitID;
                ProjectBas.StrValidate = "v";
                if (ProjectMan.InsertNewUseProjectBas(ProjectBas, ref strErr) == true)
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

        public ActionResult SetUpProject(string id)
        {
            if (id == null)
            {
                tk_ProjectBas ProBasr = new tk_ProjectBas();
                return View(ProBasr);
            }
            string[] arr = id.Split('@');
            tk_ProjectBas ProBas = new tk_ProjectBas();
            ProBas.StrPID = arr[0];
            ViewData["Pname"] = arr[1];
            ViewData["PsourceDesc"] = arr[2];
            ViewData["MainContent"] = arr[3];
            ViewData["ProID"] = arr[4];
            return View(ProBas);
        }

        public ActionResult AppNewProject(tk_ProjectBas ProjectBas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                ProjectBas.StrAppUser = account.UserID.ToString();
                if (ProjectMan.AppNewProjectBas(ProjectBas, ref strErr) == true)
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

        public ActionResult UseUpdateSetUp(string id)
        {
            if (id == null)
            {
                UseProjectBas ProBasr1 = new UseProjectBas();
                return View(ProBasr1);
            }
            UseProjectBas ProBas = new UseProjectBas();
            ProBas = ProjectMan.getNewUseUpdateSetUp(id);
            return View(ProBas);
        }

        public ActionResult UpdateSetUp(string id)
        {
            if (id == null)
            {
                tk_ProjectBas ProBasr = new tk_ProjectBas();
                return View(ProBasr);
            }
            string[] arr = id.Split('@');
            tk_ProjectBas ProBas = new tk_ProjectBas();
            string Pid = arr[0];
            ViewData["Pname"] = arr[1];
            ViewData["PsourceDesc"] = arr[2];
            ViewData["MainContent"] = arr[3];
            ProBas = ProjectMan.getNewUpdateSetUp(Pid);
            return View(ProBas);
        }

        public ActionResult UpNewSetUp(tk_ProjectBas ProjectBas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewSetUp(ProjectBas, ref strErr) == true)
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

        public ActionResult UseUpNewSetUp(UseProjectBas ProjectBas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UseUpNewUpSetUp(ProjectBas, ref strErr) == true)
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

        public ActionResult deleteUseApp()
        {
            var PID = Request["data1"];
            string strErr = "";
            if (ProjectMan.deleteNewUseApp(PID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult deleteApp()
        {
            var PID = Request["data1"];
            string strErr = "";
            if (ProjectMan.deleteNewApp(PID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult DetailApp(string id)
        {
            string where = " and a.PID = '" + id + "'";
            DataTable dt = ProjectMan.getNewDetailApp(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div  id=\"tabTitile\"style=\"font-size:15px;\">项目编号：" + id + "</div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:10%\">项目名称</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["Pname"].ToString() + "</td><td class=\"textLeft\" style=\"width:10%\">立项时间</td><td class=\"textRight\"  style=\"width:20%\">" + dt.Rows[0]["AppDate"].ToString() + "</td><td class=\"textLeft\" style=\"width:10%\">立项编号</td><td class=\"textRight\"  style=\"width:25%\">" + dt.Rows[0]["AppID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">客户名称</td><td class=\"textRight\" colspan=\"5\">" + dt.Rows[0]["CustomerName"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">建设单位</td><td class=\"textRight\">" + dt.Rows[0]["BuildUnit"].ToString() + "</td><td class=\"textLeft\">联系人</td><td class=\"textRight\">" + dt.Rows[0]["LinkMan"].ToString() + "</td><td class=\"textLeft\">电话</td><td class=\"textRight\">" + dt.Rows[0]["Phone"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目地点</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Paddress"].ToString() + "</td><td class=\"textLeft\">项目来源</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["PsourceDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目负责人</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Principal"].ToString() + "</td><td class=\"textLeft\">配合负责人</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ConcertPerson"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:100px\"><td  class=\"textLeft\">项目主要内容</td><td class=\"textRight\" colspan=\"5\">" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目合同额</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ContractAmount"].ToString() + "万元</td><td class=\"textLeft\" colspan=\"2\">项目前期费用：（管理费、预算）</td><td class=\"textRight\">" + dt.Rows[0]["Budget"].ToString() + "万元</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">项目成本</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Cost"].ToString() + "万元</td><td class=\"textLeft\">项目利润</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Profit"].ToString() + "万元</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">是否设计</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["IsDesign"].ToString() + "</td><td class=\"textLeft\">是否报价</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["IsPrice"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">是否预算</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["IsBudget"].ToString() + "</td><td class=\"textLeft\">有无合同</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["IsContract"].ToString() + "</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult PrintApp(string id)
        {
            string where = " and a.PID = '" + id + "'";
            DataTable dt = ProjectMan.getNewDetailApp(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:20px;font-size:17px;font-weight:bold;\">立项申请表</div>");
            sb.Append("<table id=\"tab\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:15%\">项目名称:</td><td class=\"PRight\" style=\"width:30%\">" + dt.Rows[0]["Pname"].ToString() + "</td><td class=\"PLeft\" style=\"width:15%\">立项时间:</td><td class=\"PRight\" style=\"width:20%\">" + dt.Rows[0]["AppDate"].ToString() + "</td><td class=\"PLeft\" style=\"width:10%\">立项编号:</td><td class=\"PRight\" style=\"width:10%\">" + dt.Rows[0]["AppID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">建设单位:</td><td class=\"PRight\">" + dt.Rows[0]["BuildUnit"].ToString() + "</td><td class=\"PLeft\">联系人:</td><td class=\"PRight\">" + dt.Rows[0]["LinkMan"].ToString() + "</td><td class=\"PLeft\">电话:</td><td class=\"PRight\">" + dt.Rows[0]["Phone"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目地点:</td><td class=\"PRight\">" + dt.Rows[0]["Paddress"].ToString() + "</td><td class=\"PLeft\">项目来源:</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PsourceDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目负责人:</td><td class=\"PRight\">" + dt.Rows[0]["Principal"].ToString() + "</td><td class=\"PLeft\">配合负责人:</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["ConcertPerson"].ToString() + "</td></tr>");
            sb.Append("<tr class=\"PRight\" style=\"height:100px\"><td colspan=\"6\">项目简介：" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            string ContractAmount = "";
            string Budget = "";
            if (dt.Rows[0]["ContractAmount"].ToString() == "0.00")
                ContractAmount = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else
                ContractAmount = dt.Rows[0]["ContractAmount"].ToString();
            if (dt.Rows[0]["Budget"].ToString() == "0.00")
                Budget = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else
                Budget = dt.Rows[0]["Budget"].ToString();
            sb.Append("<tr><td class=\"PLeft\">项目合同额:</td><td class=\"PRight\">" + ContractAmount + "<span style=\"padding-left:60%;\">万元</span></td><td class=\"PLeft\" colspan=\"3\">项目前期费用：（管理费、预算）</td><td class=\"PRight\">" + Budget + "<span style=\"padding-left:20px\">万元</span></td></tr>");
            string Cost = "";
            string Profit = "";
            if (dt.Rows[0]["Cost"].ToString() == "0.00")
                Cost = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else
                Cost = dt.Rows[0]["Cost"].ToString();
            if (dt.Rows[0]["Profit"].ToString() == "0.00")
                Profit = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else
                Profit = dt.Rows[0]["Profit"].ToString();
            sb.Append("<tr><td class=\"PLeft\">项目成本:</td><td class=\"PRight\">" + Cost + "<span style=\"padding-left:60%;\">万元</span></td><td class=\"PLeft\">项目利润:</td><td class=\"PRight\" colspan=\"3\">" + Profit + "<span style=\"padding-left:60%;\">万元</span></td></tr>");
            sb.Append("<tr><td class=\"PLeft\">当前进度:</td><td class=\"PRight\">" + dt.Rows[0]["Schedule"].ToString() + "</td><td class=\"PLeft\">预计合同签订日期:</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PlanSignaDate"].ToString() + "</td></tr>");
            DataTable dtE = ProjectMan.getNewUmExamineContent(id,"工程立项");
            if (dtE.Rows.Count > 0)
            {
                sb.Append("<tr><td colspan=\"6\" class=\"PLeft\">领导审批</td></tr>");
                int height = 500 / dtE.Rows.Count;
                for (int i = 0; i < dtE.Rows.Count; i++)
                {
                    if (dtE.Rows[i]["Opinion"].ToString() != "")
                        sb.Append("<tr style=\"height:" + height + "\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见： " + dtE.Rows[i]["Opinion"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                    else
                        sb.Append("<tr style=\"height:" + height + "\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                }
            }
            else
            {
                DataTable dtF = ProjectMan.getNewUmExamine("工程立项");
                sb.Append("<tr><td colspan=\"6\" class=\"PLeft\">领导审批</td></tr>");
                int height = 500 / dtF.Rows.Count;
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    sb.Append("<tr style=\"height:" + height + "\"><td class=\"PRight\" colspan=\"3\">" + dtF.Rows[i]["Duty"].ToString() + "</td><td colspan=\"3\">日期：</td></tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }


        public ActionResult AppExamine()
        {
            ViewData["webkey"] = "工程立项";
            string fold = COM_ApprovalMan.getNewwebkey("工程立项");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult AppExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if(UnitID == "32" || UnitID == "46")
                     where += "";
                else
                    where += " and a.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewAppExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult ProjectDesign()
        {
            return View();
        }

        public ActionResult DesignProjectGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectDesignGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult PreDesign(string id)
        {
            tk_PreDesign Design = new tk_PreDesign();
            string[] arr = id.Split('@');
            Design.StrPID = arr[0];
            Design.Strsid = ProjectMan.getNewDesignID(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Design);
        }

        public ActionResult InsertPreDesignFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_PreDesign Design = new tk_PreDesign();
                Design.StrPID = Request["PID"].ToString();
                Design.Strsid = Request["sid"].ToString();
                Design.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Design.StrCreatePerson = account.UserID.ToString();
                Design.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewPreDesignFile(Design, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertPreDesign(tk_PreDesign Design, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Design.StrUnitID = account.UnitID;
                Design.StrCreatePerson = account.UserID.ToString();
                Design.StrCreateTime = DateTime.Now;
                Design.StrState = 0;
                Design.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewPreDesign(Design,ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("PreDesign", Design);
            }
        }

        public ActionResult DesignGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewDesignGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
            string PID = "";
            string ProID = "";
            if (Request["PID"] != null)
                PID = Request["PID"].ToString();
            if (Request["ProID"] != null)
                ProID = Request["ProID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (PID != "")
                where += " and a.RelevanceID = '" + PID + "'";
            if (ProID != "")
                where += " or a.RelevanceID = '" + ProID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (PID != "")
                udtTask = ProjectMan.getNewUserLogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePreDesign(string id)
        {
            string[] arr = id.Split('@');
            tk_PreDesign Design = new tk_PreDesign();
            Design = ProjectMan.getNewUpdatePreDesign(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Design);
        }

        public ActionResult GetCompletionsFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewDownloadCompletions(id);
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

        public ActionResult GetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewDownLoad(id);
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

        public ActionResult deleteCompletionsFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewCompletionsFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DellNewFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult UpPreDesign(tk_PreDesign Design, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Design.StrCreatePerson = account.UserID.ToString();
                Design.StrCreateTime = DateTime.Now;
                Design.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.UpdateNewPreDesign(Design, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdatePreDesign", Design);
            }
        }

        public ActionResult DownLoadPreDesign(string id)
        {
            ViewData["sid"] = id;
            return View();
        }

        public ActionResult DownLoadCompletions(string id)
        {
            ViewData["sid"] = id;
            return View();
        }

        public void DownLoad(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewDownloadFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                if (dtInfo.Rows[0][0].ToString() != "")
                {
                    string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                    string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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
        }

        public ActionResult dellProjectDesign()
        {
            var sid = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewDesign(sid,pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult Offer(string id)
        {
            tk_Price Price = new tk_Price();
            string[] arr = id.Split('@');
            Price.StrPID = arr[0];
            Price.Stroid = ProjectMan.getNewOfferID(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Price);
        }

        public ActionResult InsertPriceFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_Price Price = new tk_Price();
                Price.StrPID = Request["PID"].ToString();
                Price.Stroid = Request["sid"].ToString();
                Price.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Price.StrCreatePerson = account.UserID.ToString();
                Price.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewPriceFile(Price, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertPrice(tk_Price Price, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Price.StrUnitID = account.UnitID;
                Price.StrCreatePerson = account.UserID.ToString();
                Price.StrCreateTime = DateTime.Now;
                Price.StrState = 0;
                Price.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewPrice(Price, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "false", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("Offer", Price);
            }
        }

        public ActionResult PriceGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewPriceGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MakePrice()
        {
            return View();
        }

        public ActionResult MakePriceGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectPriceGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdatePrice(string id)
        {
            string[] arr = id.Split('@');
            tk_Price Price = new tk_Price();
            Price = ProjectMan.getNewUpdatePrice(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Price);
        }

        public ActionResult GetPriceFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewPriceDownload(id);
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

        public ActionResult deletePriceFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewPriceFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult upPrice(tk_Price Price, string Pname)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewPrice(Price, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdatePrice", Price);
            }
        }

        public ActionResult dellProjectPrice()
        {
            var oid = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewPrice(oid, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult DownLoadPrice(string id)
        {
            ViewData["oid"] = id;
            return View();
        }

        public void DownLoadPriceFile(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewDownloadPriceFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult AddBudget(string id)
        {
            tk_Budget Budget = new tk_Budget();
            string[] arr = id.Split('@');
            Budget.StrPID = arr[0];
            Budget.Strbid = ProjectMan.GetNewBudetid(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Budget);
        }

        public ActionResult InsertBudgetFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_Budget Budget = new tk_Budget();
                Budget.StrPID = Request["PID"].ToString();
                Budget.Strbid = Request["sid"].ToString();
                Budget.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Budget.StrCreatePerson = account.UserID.ToString();
                Budget.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNEWBudgetFile(Budget, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertBudget(tk_Budget Budget)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Budget.StrUnitID = account.UnitID;
                Budget.StrCreatePerson = account.UserID.ToString();
                Budget.StrCreateTime = DateTime.Now;
                Budget.StrState = 0;
                Budget.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewBudget(Budget, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("AddBudget", Budget);
            }
        }
        public ActionResult BudgetGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewBudgetGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectBudget()
        {
            return View();
        }

        public ActionResult ProjectBudgetGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectBudgetGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateBudget(string id)
        {
            string[] arr = id.Split('@');
            tk_Budget Budget = new tk_Budget();
            Budget = ProjectMan.getNewUpdateBudget(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Budget);
        }

        public ActionResult GetBudgetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewBudgetDownload(id);
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

        public ActionResult deleteBudgetFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewBudgetFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult upBudget(tk_Budget Budget)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewBudget(Budget, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdateBudget", Budget);
            }
        }

        public ActionResult dellProjectBudget()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewBudget(id, pid,ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult DownLoadBudget(string id)
        {
            ViewData["bid"] = id;
            return View();
        }

        public void DownLoadBudgetFile(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewDownloadBudgetFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult AddBidding(string id)
        {
            string[] arr = id.Split('@');
            tk_Bidding Bidding = new tk_Bidding();
            Bidding.StrPID = arr[0];
            Bidding.StrBidID = ProjectMan.GetNewBiddingID(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Bidding);
        }

        public ActionResult InsertBiddingFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_Bidding Bidding = new tk_Bidding();
                Bidding.StrPID = Request["PID"].ToString();
                Bidding.StrBidID = Request["sid"].ToString();
                Bidding.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Bidding.StrCreatePerson = account.UserID.ToString();
                Bidding.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewBiddingFile(Bidding, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertBidding(tk_Bidding Bidding, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Bidding.StrUnitID = account.UnitID;
                Bidding.StrCreatePerson = account.UserID.ToString();
                Bidding.StrCreateTime = DateTime.Now;
                Bidding.StrState = 0;
                Bidding.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewBidding(Bidding, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("AddBidding", Bidding);
            }
        }

        public ActionResult BiddingGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewBiddingGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectBidding()
        {
            return View();
        }

        public ActionResult BiddingProjectGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectBiddingGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateBidding(string id)
        {   
            string[] arr = id.Split('@');
            tk_Bidding Bidding = new tk_Bidding();
            Bidding = ProjectMan.getNewUpdateBidding(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Bidding);
        }

        public ActionResult GetBiddingFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewBiddingDownload(id);
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

        public ActionResult upBidding(tk_Bidding Bidding,string Pname)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewBidding(Bidding,ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdateBidding", Bidding);
            }
        }

        public ActionResult deleteBiddingFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewBiddingFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult dellProjectBidding()
        {
            var sid = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewBidding(sid, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult DownLoadBidding(string id)
        {
            ViewData["BidID"] = id;
            return View();
        }

        public void DownLoadBiddingFile(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewDownloadBiddingFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult DesignExamine()
        {
            ViewData["webkey"] = "设计审批";
            string fold = COM_ApprovalMan.getNewwebkey("设计审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult DesignExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                 string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if(UnitID == "32" || UnitID == "46")
                     where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewDesignExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult PriceExamine()
        {
            ViewData["webkey"] = "项目报价审批";
            string fold = COM_ApprovalMan.getNewwebkey("项目报价审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult PriceExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewPriceExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult BudgetExamine()
        {
            ViewData["webkey"] = "预算审批";
            string fold = COM_ApprovalMan.getNewwebkey("预算审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult BudgetExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewBudgetExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult BiddingExamine()
        {
            ViewData["webkey"] = "投标审批";
            string fold = COM_ApprovalMan.getNewwebkey("投标审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult BiddingExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewBiddingExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult ImpleGeneral()
        {
            return View();
        }

        public ActionResult ImpleGeneralGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = " and a.State >= '1'";
                else
                    where = " and a.State >= '1' and a.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewImProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult addSchedule(string id)
        {
            if (id == null)
            {
                tk_Schedule Scheduler = new tk_Schedule();
                return View(Scheduler);
            }
            string[] arr = id.Split('@');
            tk_Schedule Schedule = new tk_Schedule();
            Schedule.StrPID = arr[0];
            ViewData["Pname"] = arr[1];
            return View(Schedule);
        }

        public ActionResult InsertSchedule(tk_Schedule Schedule)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Schedule.StrUnitID = account.UnitID;
                Schedule.StrCreatePerson = account.UserID.ToString();
                Schedule.StrCreateTime = DateTime.Now;
                Schedule.StrValidate = "v";
                if (ProjectMan.InsertNewSchedule(Schedule, ref strErr) == true)
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

        public ActionResult ScheduleGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewScheduleGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectSchedule()
        {
            return View();
        }

        public ActionResult ProjectScheduleGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectScheduleGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateSchedule(string id)
        {
            if (id == null)
            {
                tk_Schedule Scheduler = new tk_Schedule();
                return View(Scheduler);
            }
            string[] arr = id.Split('@');
            tk_Schedule Schedule = new tk_Schedule();
            Schedule = ProjectMan.getNewUpdateSchedule(arr[0]);
            ViewData["ScheduleID"] = arr[0];
            ViewData["Pname"] = arr[1];
            return View(Schedule);
        }

        public ActionResult upSchedule(tk_Schedule Schedule, string ScheduleID)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewSchedule(Schedule, ScheduleID, ref strErr) == true)
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

        public ActionResult dellProjectSchedule()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewSchedule(id, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult addSubWork(string id)
        {
            string[] arr = id.Split('@');
            tk_SubWork Sub = new tk_SubWork();
            Sub.StrPID = arr[0];
            Sub.StrEID = ProjectMan.GetNewSubWorkID(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Sub);
        }

        public ActionResult InsertSubWorkFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_SubWork Sub = new tk_SubWork();
                Sub.StrPID = Request["PID"].ToString();
                Sub.StrEID = Request["sid"].ToString();
                Sub.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Sub.StrCreatePerson = account.UserID.ToString();
                Sub.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewSubWorkFile(Sub, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertSubWork(tk_SubWork Sub, string Pname)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Sub.StrUnitID = account.UnitID;
                Sub.StrState = 0;
                Sub.StrCreatePerson = account.UserID.ToString();
                Sub.StrCreateTime = DateTime.Now;
                Sub.StrValidate = "v";
                if (ProjectMan.InsertNewSubWork(Sub, ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("addSubWork", Sub);
            }
        }

        public ActionResult SubWorkGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewSubWorkGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectSubWork()
        {
            return View();
        }

        public ActionResult ProjectSubWorkGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectSubWorkGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateSubWork(string id)
        {
            string[] arr = id.Split('@');
            tk_SubWork Sub = new tk_SubWork();
            Sub = ProjectMan.getNewUpdateSubWork(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Sub);
        }

        public ActionResult GetSubWorkFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewSubWorkDownload(id);
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

        public ActionResult upSubWork(tk_SubWork Sub)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.updateNewSubWork(Sub,ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdateSubWork", Sub);
            }
        }

        public ActionResult deleteSubWorkFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewSubWorkFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult dellProjectSubWork()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewSubWork(id, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult DownLoadSubWork(string id)
        {
            ViewData["EID"] = id;
            return View();
        }

        public void DownLoadSubWorkFile(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewDownloadSubWorkFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult PrintSubWork(string id)
        {
            string where = " and a.EID = '" + id + "'";
            DataTable dt = ProjectMan.GetNewPrintSubWork(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">分包施工明细及价格</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:20%\">填表人</td><td class=\"PRight\" style=\"width:30%\">" + dt.Rows[0]["CreatePerson"].ToString() + "</td><td class=\"PLeft\" style=\"width:20%\">填写日期</td><td class=\"PRight\" style=\"width:30%\">" + Convert.ToDateTime(dt.Rows[0]["CreateTime"]).ToString("yyyy-MM-dd") + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目名称</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Pname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目负责人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Principal"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">分包单位</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["SubUnit"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">分包单位项目负责人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["subPrincipal"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">分包费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["SubPrice"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">预计分工施工周期</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["WorkCycle"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"4\">一、分包施工原因</td></tr>");
            sb.Append("<tr style=\"height:100px;\"><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["SubWorkReason"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"4\">二、分工施工主要内容</td></tr>");
            sb.Append("<tr style=\"height:150px;\"><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"4\">三、施工安全协议的签订情况</td></tr>");
            if (dt.Rows[0]["IsSign"].ToString() == "1")
                sb.Append("<tr style=\"height:50px;\"><td class=\"PRight\" colspan=\"4\">已签订施工安全协议</td></tr>");
            else
                sb.Append("<tr style=\"height:50px;\"><td class=\"PRight\" colspan=\"4\">未签订施工安全协议</td></tr>");
            DataTable dtE = ProjectMan.getNewUmExamineContent(id, "分包施工审批");
            //for (int i = 0; i < dtE.Rows.Count; i++)
            //{
            //    sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">" + dtE.Rows[i]["Job"].ToString() + "</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:left\">审批情况: " + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见: " + dtE.Rows[i]["Opinion"].ToString() + "签字:" + dtE.Rows[i]["UserName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期:" + dtE.Rows[i]["ApprovalTime"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            //}
            if (dtE.Rows.Count > 0)
            {
                for (int i = 0; i < dtE.Rows.Count; i++)
                {
                    if (dtE.Rows[i]["Opinion"].ToString() != "")
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见： " + dtE.Rows[i]["Opinion"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                    else
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                }
            }
            else
            {
                DataTable dtF = ProjectMan.getNewUmExamine("分包施工审批");
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">" + dtF.Rows[i]["Duty"].ToString() + "</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
                }
            }
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">部门经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">总经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult PurchaseExamine()
        {
            ViewData["webkey"] = "项目采购审批";
            string fold = COM_ApprovalMan.getNewwebkey("项目采购审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult PurchaseExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewPurchaseExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult SubWorkExamine()
        {
            ViewData["webkey"] = "分包施工审批";
            string fold = COM_ApprovalMan.getNewwebkey("分包施工审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult SubWorkExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if(UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewSubWorkExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult addPurchase(string id)
        {
            string[] arr = id.Split('@');
            tk_ProjectPurchase Purchase = new tk_ProjectPurchase();
            Purchase.StrPID = arr[0];
            Purchase.StrPname = arr[1];
            return View(Purchase);
        }

        public ActionResult InsertPurchase(tk_ProjectPurchase Purchase)
        {
            if (ModelState.IsValid)
            {
                //如果没错误，返回首页
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Purchase.StrCreatePerson = account.UserID.ToString();
                Purchase.StrCreateTime = DateTime.Now;
                Purchase.StrUnitID = account.UnitID;
                Purchase.StrValidate = "v";
                if (ProjectMan.InsertNewPurchase(Purchase, ref strErr) == true)
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

        public ActionResult PurchaseGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewPurchaseGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectPurchase()
        {
            return View();
        }

        public ActionResult ProjectPurchaseGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectPurchaseGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdatePurchase(string id)
        {
            if (id == null)
            {
                tk_ProjectPurchase Purchase1 = new tk_ProjectPurchase();
                return View(Purchase1);
            }
            string[] arr = id.Split('@');
            tk_ProjectPurchase Purchase = new tk_ProjectPurchase();
            Purchase = ProjectMan.getNewUpdatePurchase(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Purchase);
        }

        public ActionResult UpPurchase(tk_ProjectPurchase Purchase)
        {
            if (ModelState.IsValid)
            {
                //如果没错误，返回首页
                string strErr = "";
                if (ProjectMan.UpdateNewPurchase(Purchase, ref strErr) == true)
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

        public ActionResult dellPurchase()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewPurchase(id, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult PrintPurchase(string id)
        {
            string where = " and a.PcID = '" + id + "'";
            DataTable dt = ProjectMan.GetNewPrintPurchase(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">采购或委托合同评审表</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:20%\">合同名称</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PcName"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"2\">甲方：" + dt.Rows[0]["PartA"].ToString() + "</td><td class=\"PRight\" colspan=\"2\">乙方：" + dt.Rows[0]["PartB"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:20%\">合同编号</td><td class=\"PRight\" style=\"width:30%\">" + dt.Rows[0]["PcNum"].ToString() + "</td><td class=\"PLeft\" style=\"width:20%\">所属项目</td><td class=\"PRight\" style=\"width:30%\">" + dt.Rows[0]["Pname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">合同额</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PcAmount"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">合同类别</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PcType"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:20px;\"><td class=\"PLeft\"  colspan=\"4\">注：施工合同必须签订安全协议</td></tr>");
            DataTable dtE = ProjectMan.getNewUmExamineContent(id,"项目采购审批");
            if (dtE.Rows.Count > 0)
            {
                for (int i = 0; i < dtE.Rows.Count; i++)
                {
                    if (dtE.Rows[i]["Opinion"].ToString() != "")
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见： " + dtE.Rows[i]["Opinion"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                    else
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                }
            }
            else
            {
                DataTable dtF = ProjectMan.getNewUmExamine("项目采购审批");
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">" + dtF.Rows[i]["Duty"].ToString() + "</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
                }
            }
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">部门经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">总经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult addSubPackage(string id)
        {
            if (id == null)
            {
                tk_SubPackage Packager = new tk_SubPackage();
                return View(Packager);
            }
            string[] arr = id.Split('@');
            tk_SubPackage Package = new tk_SubPackage();
            Package.StrPID = arr[0];
            ViewData["Pname"] = arr[1];
            return View(Package);
        }

        public ActionResult InsertSubPackage(tk_SubPackage Package)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Package.StrUnitID = account.UnitID;
                Package.StrState = 0;
                Package.StrCreatePerson = account.UserID.ToString();
                Package.StrCreateTime = DateTime.Now;
                Package.StrValidate = "v";
                if (ProjectMan.InsertNewSubPackage(Package, ref strErr) == true)
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

        public ActionResult SubPackageGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewSubPackageGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectSubPackage()
        {
            return View();
        }

        public ActionResult ProjectSubPackageGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectSubPackageGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateSubPackage(string id)
        {
            if (id == null)
            {
                tk_SubPackage Packager = new tk_SubPackage();
                return View(Packager);
            }
            string[] arr = id.Split('@');
            tk_SubPackage Package = new tk_SubPackage();
            Package = ProjectMan.getNewUpdateSubPackage(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Package);
        }

        public ActionResult upSubPackage(tk_SubPackage Package)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.updateNewSubPackage(Package, ref strErr) == true)
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

        public ActionResult dellSubPackage()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewSubPackage(id, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult PrintSubPackage(string id)
        {
            string where = " and a.SID = '" + id + "'";
            DataTable dt = ProjectMan.GetNewPrintSubPackage(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">分包设计明细及价格</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:20%\">填表人</td><td class=\"PRight\" style=\"width:30%\">" + dt.Rows[0]["CreatePerson"].ToString() + "</td><td class=\"PLeft\" style=\"width:20%\">填写日期</td><td class=\"PRight\" style=\"width:30%\">" + Convert.ToDateTime(dt.Rows[0]["CreateTime"]).ToString("yyyy-MM-dd") + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目名称</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Pname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目负责人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Principal"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">设计单位</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["DesignUnit"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">设计负责人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["subPrincipal"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">设计费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["DesignPrice"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">预计设计周期</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PredictCycle"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"4\">一、分包设计原因</td></tr>");
            sb.Append("<tr style=\"height:150px;\"><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["SubReason"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PRight\" colspan=\"4\">二、设计主要内容</td></tr>");
            sb.Append("<tr style=\"height:150px;\"><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["MainContent"].ToString() + "</td></tr>");
            DataTable dtE = ProjectMan.getNewUmExamineContent(id,"分包设计审批");
            if (dtE.Rows.Count > 0)
            {
                for (int i = 0; i < dtE.Rows.Count; i++)
                {
                    if (dtE.Rows[i]["Opinion"].ToString() != "")
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见： " + dtE.Rows[i]["Opinion"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                    else
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                }
            }
            else
            {
                DataTable dtF = ProjectMan.getNewUmExamine("分包设计审批");
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">" + dtF.Rows[i]["Duty"].ToString() + "</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
                }
            }
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">部门经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">总经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult SubPackageExamine()
        {
            ViewData["webkey"] = "分包设计审批";
            string fold = COM_ApprovalMan.getNewwebkey("分包设计审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult SubPackageExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewSubPackageExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult addPayRecord(string id)
        {
            if (id == null)
            {
                tk_PayRecord Payr = new tk_PayRecord();
                return View(Payr);
            }
            string[] arr = id.Split('@');
            tk_PayRecord Pay = new tk_PayRecord();
            Pay.StrPID = arr[0];
            ViewData["Pname"] = arr[1];
            return View(Pay);
        }

        public ActionResult InsertPayRecord(tk_PayRecord Pay)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Pay.StrUnitID = account.UnitID;
                Pay.StrState = 0;
                Pay.StrCreatePerson = account.UserID.ToString();
                Pay.StrCreateTime = DateTime.Now;
                Pay.StrValidate = "v";
                if (ProjectMan.InsertNewPayRecord(Pay, ref strErr) == true)
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

        public ActionResult PayRecordGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewPayRecordGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectPayRecord()
        {
            return View();
        }

        public ActionResult ProjectPayRecordGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectPayRecordGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdatePayRecord(string id)
        {
            if (id == null)
            {
                tk_PayRecord Payr = new tk_PayRecord();
                return View(Payr);
            }
            string[] arr = id.Split('@');
            tk_PayRecord Pay = new tk_PayRecord();
            Pay = ProjectMan.getNewUpdatePayRecord(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Pay);
        }

        public ActionResult upPayRecord(tk_PayRecord Pay)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewPayRecord(Pay, ref strErr) == true)
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

        public ActionResult dellPayRecord()
        {
            var id = Request["data1"];
            var pid = Request["data2"];
            string strErr = "";
            if (ProjectMan.DeleteNewPayRecord(id, pid, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult PrintPayRecord(string id, string payid)
        {
            string where = " and a.PID = '" + id + "'";
            DataTable dt1 = ProjectMan.getNewPrintPayRecord(where);
            DataTable dt = ProjectMan.getNewPayRecordSTA(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">项目费用支出明细单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\" style=\"width:20%\">填表人</td><td class=\"PRight\" style=\"width:30%\">" + dt1.Rows[0]["CreatePerson"].ToString() + "</td><td class=\"PLeft\" style=\"width:20%\">填写日期</td><td class=\"PRight\" style=\"width:30%\">" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目名称</td><td class=\"PRight\" colspan=\"3\">" + dt1.Rows[0]["Pname"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">项目负责人</td><td class=\"PRight\" colspan=\"3\">" + dt1.Rows[0]["Principal"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">设备及配件采购费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT1"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">外协加工费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT2"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">项目分包设计费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT3"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">项目分包施工费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT4"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">项目人员差旅费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT5"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">维护客户关系费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["PT6"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">项目支出总费用</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Total"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" style=\"height:60px;\">与项目预算费用对比</td><td class=\"PRight\" colspan=\"3\"></td></tr>");
            DataTable dtE = ProjectMan.getNewUmExamineContent(payid, "费用支出审批");
            if (dtE.Rows.Count > 0)
            {
                for (int i = 0; i < dtE.Rows.Count; i++)
                {
                    if (dtE.Rows[i]["Opinion"].ToString() != "")
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</p>审批意见： " + dtE.Rows[i]["Opinion"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                    else
                        sb.Append("<tr style=\"height:70px\"><td class=\"PRight\" colspan=\"3\">" + dtE.Rows[i]["Job"].ToString() + ": " + dtE.Rows[i]["UserName"].ToString() + "</p>审批情况：" + dtE.Rows[i]["statedesc"].ToString() + "</td><td colspan=\"3\">日期：" + dtE.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
                }
            }
            else
            {
                DataTable dtF = ProjectMan.getNewUmExamine("费用支出审批");
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">" + dtF.Rows[i]["Duty"].ToString() + "</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
                }
            }
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">部门经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            //sb.Append("<tr><td class=\"PLeft\" style=\"height:70px;\">总经理审批</td><td class=\"PRight\" colspan=\"3\" style=\"text-align:right\">签字&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult PaRecoudExamine()
        {
            ViewData["webkey"] = "费用支出审批";
            string fold = COM_ApprovalMan.getNewwebkey("费用支出审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult PayRecoudExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where = "";
                else
                    where = " and c.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and c.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and c.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and c.AppDate >= '" + start + "' and c.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and c.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewPayRecoudExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult ContractGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string where = " and a.Unit = '" + unit + "' and a.BusinessType = 'BT5'";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = new UIDataTable();
            if(PID != "")
                udtTask = ContractMan.getNewContractGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinishMaintain()
        {
            return View();
        }

        public ActionResult FinishMaintainGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string UnitID = account.UnitID;
                if(UnitID == "32" || UnitID == "46")
                    where = " and a.State >= '1'";
                else
                    where = " and a.State >= '1' and a.UnitID = '" + account.UnitID + "'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                UIDataTable udtTask = ProjectMan.getNewImProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult addProCompletions(string id)
        {
            string[] arr = id.Split('@');
            tk_ProCompletions Com = new tk_ProCompletions();
            Com.StrPID = arr[0];
            Com.Strcid = ProjectMan.getsgid(arr[0]);
            ViewData["Pname"] = arr[1];
            return View(Com);
        }

        public ActionResult InsertProCompletionsFile()
        {
            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                tk_ProCompletions Com = new tk_ProCompletions();
                Com.StrPID = Request["PID"].ToString();
                Com.Strcid = Request["sid"].ToString();
                Com.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Com.StrCreatePerson = account.UserID.ToString();
                Com.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewProCompletionsFile(Com, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertProCompletions(tk_ProCompletions Com, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Com.StrUnitID = account.UnitID;
                Com.StrCreatePerson = account.UserID.ToString();
                Com.StrCreateTime = DateTime.Now;
                Com.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewProCompletions(Com,  ref strErr) == true)
                {
                    return Json(new { success = "true", Msg = "保存成功" });
                }
                else
                {
                    return Json(new { success = "true", Msg = "保存失败" });
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("addProCompletions", Com);
            }
        }

        public ActionResult ProCompletionsGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewProCompletionsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectCompletions()
        {
            return View();
        }

        public ActionResult ProjectCompletionsGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectCompletionsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateProjectCompletions(string id)
        {
            string[] arr = id.Split('@');
            ViewData["Pname"] = arr[1];
            tk_ProCompletions Com = new tk_ProCompletions();
            Com = ProjectMan.getNewUpdateProCompletions(arr[0]);
            return View(Com);
        }

        public ActionResult GetProjectCompletionsFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewProCompletionsDownload(id);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                name += dtInfo.Rows[i]["CompleteFileName"].ToString() + "@";
                file += dtInfo.Rows[i]["CompleteFile"].ToString() + "@";
            }
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }

        public ActionResult upProCompletions(tk_ProCompletions Com, string Pname)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewProCompletions(Com, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpdateProjectCompletions", Com);
            }
        }

        

        public void DownLoadProjectCompletions(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewProCompletionsDownload(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult addProFinish(string id)
        {
            if (id == null)
            {
                tk_ProFinish Finishr = new tk_ProFinish();
                return View(Finishr);
            }
            string[] arr = id.Split('@');
            tk_ProFinish Finish = new tk_ProFinish();
            ViewData["Pname"] = arr[1];
            Finish.StrPID = arr[0];
            if (arr[2].ToString() != "")
            {
                Finish.StrDebtAmount = ContractMan.getNewDebtAmountPro(arr[0]);
                if (Finish.StrDebtAmount == 0)
                    Finish.StrIsDebt = 0;
                else
                    Finish.StrIsDebt = 1;
            }
            return View(Finish);
        }

        public ActionResult InsertProFinish(tk_ProFinish Finish)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                Finish.StrUnitID = account.UnitID;
                Finish.StrCreatePerson = account.UserID.ToString();
                Finish.StrCreateTime = DateTime.Now;
                Finish.StrValidate = "v";
                if (ProjectMan.InsertNewProFinish(Finish, ref strErr) == true)
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

        public ActionResult ProFinishGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.PID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewProFinishGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectFinish()
        {
            return View();
        }

        public ActionResult ProjectFinishGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectFinishGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult UpdateProFinish(string id)
        {
            if (id == null)
            {
                tk_ProFinish Finishr = new tk_ProFinish();
                return View(Finishr);
            }
            string[] arr = id.Split('@');
            ViewData["Pname"] = arr[1];
            tk_ProFinish Finish = new tk_ProFinish();
            Finish = ProjectMan.getNewUpdateProFinish(arr[0]);
            return View(Finish);
        }

        public ActionResult upProFinish(tk_ProFinish Finish)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewProFinish(Finish, ref strErr) == true)
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

        public ActionResult ProjectCashBack()
        {
            return View();
        }

        public ActionResult ProjectCashBackGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    string UnitID = account.UnitID;
                    if (UnitID == "32" || UnitID == "46")
                        where += "";
                    else
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewProjectCashBackGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult CPlanTimeWarn()
        {
            return View();
        }

        public ActionResult CPlanTimeWarnGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = "";
                string time = ContractMan.getNewCPlanTime();
                if (unit == "32" || unit == "46")
                {
                    if (time == "")
                        where = " and a.BusinessType = 'BT5' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'"; // and a.state != '2'
                    else
                        where = " and a.BusinessType = 'BT5' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";
                }
                else
                {
                    if (time == "")
                        where = " and a.Unit = '" + unit + "' and a.BusinessType = 'BT5' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'"; // and a.state != '2'
                    else
                        where = " and a.Unit = '" + unit + "' and a.BusinessType = 'BT5' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";
                }
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                    udtTask = ProjectMan.getNewCPlanTimeWarnGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult SetWarnTime()
        {
            return View();
        }
        public ActionResult InsertConfigTime(string num)
        {
            string checkWay = "CPlanTime";
            string TimeType = "Project";
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string strErr = "";
            if (EquipMan.InsertNewCongTime(checkWay, num, unit, TimeType, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult CashBackWarn()
        {
            return View();
        }

        public ActionResult CashBackWarnGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = "";
                string time = ContractMan.getNewCashBackTime();
                if (unit == "32" || unit == "46")
                {
                    if (time == "")
                        where = " and a.BusinessType = 'BT5' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'"; // and a.state != '2'
                    else
                        where = " and a.BusinessType = 'BT5' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";
                }
                else
                {
                    if (time == "")
                        where = " and a.Unit = '" + unit + "' and a.BusinessType = 'BT5' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'"; // and a.state != '2'
                    else
                        where = " and a.Unit = '" + unit + "' and a.BusinessType = 'BT5' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";
                }
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                    udtTask = ProjectMan.getNewCPlanTimeWarnGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult SetCashBackWarnTime()
        {
            return View();
        }

        public ActionResult InsertCashBackConfigTime(string num)
        {
            string checkWay = "CashBack";
            string TimeType = "Project";
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string strErr = "";
            if (EquipMan.InsertNewCongTime(checkWay, num, unit, TimeType, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult DebtSearch()
        {
            return View();
        }

        public ActionResult DebtSearchGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = " and a.State >= '5'";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where += "";
                else
                    where += " and a.UnitID = '" + account.UnitID + "'";
                udtTask = ProjectMan.getNewDebtGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult ProjectStatistics()
        {
            return View();
        }

        public ActionResult ProjectStatisticsTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            ProjectSearch.Pname = Request["data3"];
            ProjectSearch.ProID = Request["data4"];
            ProjectSearch.Principal = Request["data5"];
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string UnitID = account.UnitID;
                string userid = account.UserID.ToString();

                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string ProID = ProjectSearch.ProID;
                string Principal = ProjectSearch.Principal;
                if (UnitID == "37" && userid != "141")
                    Principal = "";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (Principal != "" && Principal != null)
                    where += " and b.Principal = '" + Principal + "'";
                DataTable dt = new DataTable();
                if (UnitID == "32" || UnitID == "46")
                    where += "";
                else if (UnitID == "37")
                {
                    if (userid == "141")
                        where += " and b.UnitID = '" + account.UnitID + "'";
                    else
                    {
                        string userName = account.UserName;
                        where += " and b.UnitID = '" + account.UnitID + "' and b.Principal = '" + userName + "'";
                    }
                }
                else
                    where += " and b.UnitID = '" + account.UnitID + "'";
                dt = ProjectMan.getNewProjectStatisticsdata(where);

                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    string countStr = ProjectMan.getNewCountProject(where);
                    string[] arr = countStr.Split('@');
                    string sign = "";
                    if(start != "")
                        sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共有工程 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 项, 其中已结项 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 项, 正在执行中 <span style=\"color:#000099;font-weight:bold;\">" + arr[2] + "</span> 项";
                    else
                        sign = "汇总说明: 共有工程 <span style=\"color:#000099;font-weight:bold;\">" + arr[0] + "</span> 项, 其中已结项 <span style=\"color:#000099;font-weight:bold;\">" + arr[1] + "</span> 项, 正在执行中 <span style=\"color:#000099;font-weight:bold;\">" + arr[2] + "</span> 项";
                    //sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">");
                    //sb.Append("<tr  class=\"left\"><td style=\"width:20%\">工程编号</td><td style=\"width:20%\">工程名称</td><td style=\"width:10%\">客户名称</td><td style=\"width:10%\">工程状态</td><td style=\"width:10%\">项目负责人</td><td style=\"width:10%\">合同签订日期</td><td style=\"width:10%\">立项日期</td><td style=\"width:10%\">结项日期</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["PID"].ToString() + "</td><td>" + dt.Rows[i]["Pname"].ToString() + "</td><td>" + dt.Rows[i]["CustomerName"].ToString() + "</td><td>" + dt.Rows[i]["StateDesc"].ToString() + "</td><td>" + dt.Rows[i]["Principal"].ToString() + "</td><td>" + dt.Rows[i]["CStartTime"].ToString() + "</td><td>" + dt.Rows[i]["AppDate"].ToString() + "</td><td>" + dt.Rows[i]["FinishDate"].ToString() + "</td></tr>");
                    }
                    //sb.Append("</table>");
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign .ToString() });
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

        public ActionResult AmountStatistics()
        {
            return View();
        }

        public ActionResult AmountStatisticsTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            ProjectSearch.Pname = Request["data3"];
            ProjectSearch.ProID = Request["data4"];
            ProjectSearch.Principal = Request["data5"];
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";

                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string ProID = ProjectSearch.ProID;
                string Principal = ProjectSearch.Principal;

                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (Principal != "" && Principal != null)
                    where += " and a.Principal = '" + Principal + "'";
                DataTable dt = new DataTable();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where += "";
                else
                    where += " and a.UnitID = '" + account.UnitID + "'";
                dt = ProjectMan.getNewAmountStatisticsdata(where);

                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    decimal Total = 0;
                    decimal Amount = 0;
                    decimal Debt = 0;
                    decimal Receipt = 0;
                    string countStr = ProjectMan.getNewCountAmount(where);
                    //sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">");
                    //sb.Append("<tr  class=\"left\"><td style=\"width:10%\">工程编号</td><td style=\"width:15%\">工程名称</td><td style=\"width:10%\">立项时间</td><td style=\"width:10%\">项目负责人</td><td style=\"width:10%\">工程状态</td><td style=\"width:10%\">合同金额(万元)</td><td style=\"width:10%\">已付金额(万元)</td><td style=\"width:10%\">未付金额(万元)</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Amount"].ToString() == "")
                            Total += 0;
                        else
                            Total += Convert.ToDecimal(dt.Rows[i]["Amount"]);
                        if (dt.Rows[i]["money"].ToString() == "")
                            Amount += 0;
                        else
                            Amount += Convert.ToDecimal(dt.Rows[i]["money"]);
                        if (dt.Rows[i]["dept"].ToString() == "")
                            Debt += 0;
                        else
                            Debt += Convert.ToDecimal(dt.Rows[i]["dept"]);
                        if (dt.Rows[i]["Rmoney"].ToString() == "")
                            Receipt += 0;
                        else
                            Receipt += Convert.ToDecimal(dt.Rows[i]["Rmoney"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["PID"].ToString() + "</td><td>" + dt.Rows[i]["Pname"].ToString() + "</td><td>" + dt.Rows[i]["AppDate"].ToString() + "</td><td>" + dt.Rows[i]["Principal"].ToString() + "</td><td>" + dt.Rows[i]["StateDesc"].ToString() + "</td><td>" + dt.Rows[i]["Amount"].ToString() + "</td><td>" + dt.Rows[i]["money"].ToString() + "</td><td>" + dt.Rows[i]["dept"].ToString() + "</td><td>" + dt.Rows[i]["Rmoney"].ToString() + "</td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td></td><td></td><td>" + Total + "</td><td>" + Amount + "</td><td>" + Debt + "</td><td>" + Receipt + "</td></tr>");
                    //sb.Append("</table>");
                    string sign = "";
                    if(start != "")
                        sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共有工程 <span style=\"color:#000099;font-weight:bold;\">" + countStr + "</span> 项, 累计应付金额 <span style=\"color:#000099;font-weight:bold;\">" + Total + "</span> 万元, 待付款金额 <span style=\"color:#000099;font-weight:bold;\">" + Debt + "</span> 万元";
                    else
                        sign = "汇总说明: 共有工程 <span style=\"color:#000099;font-weight:bold;\">" + countStr + "</span> 项, 累计应付金额 <span style=\"color:#000099;font-weight:bold;\">" + Total + "</span> 万元, 待付款金额 <span style=\"color:#000099;font-weight:bold;\">" + Debt + "</span> 万元";
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });
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
        [HttpPost]
        [MultiButton("ReadToExcel")]
        public FileResult ToExcelFile()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["StartDate"];
            ProjectSearch.EndDate = Request["EndDate"];
            ProjectSearch.Pname = Request["Pname"];
            ProjectSearch.ProID = Request["ProID"];
            ProjectSearch.Principal = Request["Principal"];
            Acc_Account account = GAccount.GetAccountInfo();
            string where = "";

            string Pname = ProjectSearch.Pname;
            string start = ProjectSearch.StartDate;
            string end = ProjectSearch.EndDate;
            string ProID = ProjectSearch.ProID;
            string Principal = ProjectSearch.Principal;

            if (ProID != "" && ProID != null)
                where += " and a.ProID like '%" + ProID + "%'";
            if (Pname != "" && Pname != null)
                where += " and a.Pname like '%" + Pname + "%'";
            if (start != "" && start != null)
                where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
            if (Principal != "" && Principal != null)
                where += " and a.Principal = '" + Principal + "'";
            DataTable data = new DataTable();
            string UnitID = account.UnitID;
            if (UnitID == "32" || UnitID == "46")
                where += "";
            else
                where += " and a.UnitID = '" + account.UnitID + "'";
            data = ProjectMan.getNewAmountStatisticsdata(where);
            string strCols = "工程编号-3000,工程名称-3000,客户名称-2000,工程状态-10000,项目负责人-10000,合同签订日期-10000,立项日期-10000,结项日期-10000";
            string title = "抄表参数设置";
            if (data == null) return null;
            //data.Columns["id"].SetOrdinal(0);
            System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, title, strCols.Split(','));
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "Emergency.xls");
        }

        public ActionResult DeptStatistics()
        {
            return View();
        }

        public ActionResult DebtStatisticsTable()
        {
            tk_ProjectSearch ProjectSearch = new tk_ProjectSearch();
            ProjectSearch.StartDate = Request["data1"];
            ProjectSearch.EndDate = Request["data2"];
            ProjectSearch.Pname = Request["data3"];
            ProjectSearch.ProID = Request["data4"];
            ProjectSearch.Principal = Request["data5"];
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";

                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string ProID = ProjectSearch.ProID;
                string Principal = ProjectSearch.Principal;

                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (Principal != "" && Principal != null)
                    where += " and a.Principal = '" + Principal + "'";
                DataTable dt = new DataTable();
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where += "";
                else
                    where += " and a.UnitID = '" + account.UnitID + "'";
                dt = ProjectMan.getNewDebtStatisticsdata(where);

                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    decimal Debt = 0;
                    string countStr = ProjectMan.getNewCountDebt(where);
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">");
                    sb.Append("<tr  class=\"left\"><td style=\"width:20%\">工程编号</td><td style=\"width:20%\">工程名称</td><td style=\"width:10%\">立项时间</td><td style=\"width:10%\">项目负责人</td><td style=\"width:20%\">工程状态</td><td style=\"width:20%\">欠款金额(万元)</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["dept"].ToString() == "")
                            Debt += 0;
                        else
                            Debt += Convert.ToDecimal(dt.Rows[i]["dept"]);
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["PID"].ToString() + "</td><td>" + dt.Rows[i]["Pname"].ToString() + "</td><td>" + dt.Rows[i]["AppDate"].ToString() + "</td><td>" + dt.Rows[i]["Principal"].ToString() + "</td><td>" + dt.Rows[i]["StateDesc"].ToString() + "</td><td>" + dt.Rows[i]["dept"].ToString() + "</td></tr>");
                    }
                    sb.Append("<tr class=\"staleft\"  style=\"color:red\"><td>合计</td><td></td><td></td><td></td><td></td><td>" + Debt + "</td></tr>");
                    sb.Append("</table>");
                    string sign = "";
                    if(start != "")
                         sign = "汇总说明: <span style=\"color:#000099;font-weight:bold;\">" + start + "</span> 至 <span style=\"color:#000099;font-weight:bold;\">" + end + "</span> , 共有欠款工程 <span style=\"color:#000099;font-weight:bold;\">" + countStr + "</span> 项, 累计欠款今天 <span style=\"color:#000099;font-weight:bold;\">" + Debt + "</span> 万元";
                    else
                        sign = "汇总说明: 共有欠款工程 <span style=\"color:#000099;font-weight:bold;\">" + countStr + "</span> 项, 累计欠款今天 <span style=\"color:#000099;font-weight:bold;\">" + Debt + "</span> 万元";
                    return Json(new { success = "true", strSb = sb.ToString(), strSign = sign.ToString() });
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

        public ActionResult PurchaseSearch()
        {
            return View();
        }

        public ActionResult PurchaseSearchGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and b.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and b.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and b.AppDate >= '" + start + "' and b.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and b.Principal = '" + principal + "'";
                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                {
                    where += " and b.UnitID = '" + account.UnitID + "'";
                    udtTask = ProjectMan.getNewPurchaseSearchGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                }
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

        public ActionResult OrderGoodsGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string DDID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["DDID"] != null)
                DDID = Request["DDID"].ToString();
            if (DDID != "")
                where += " and a.DDID = '" + DDID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewOrderGoodsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProjectContractExamine()
        {
            ViewData["webkey"] = "工程项目合同审批";
            string fold = COM_ApprovalMan.getNewwebkey("工程项目合同审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7]; 
            return View();
        }

        public ActionResult ProjectContractExamineGrid(tk_ProjectSearch ProjectSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string where = "";
                string strCurPage;
                string strRowNum;

                string ProID = ProjectSearch.ProID;
                string Pname = ProjectSearch.Pname;
                string start = ProjectSearch.StartDate;
                string end = ProjectSearch.EndDate;
                string principal = ProjectSearch.Principal;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (ProID != "" && ProID != null)
                    where += " and a.ProID like '%" + ProID + "%'";
                if (Pname != "" && Pname != null)
                    where += " and a.Pname like '%" + Pname + "%'";
                if (start != "" && start != null)
                    where += " and a.AppDate >= '" + start + "' and a.AppDate <= '" + end + "'";
                if (principal != "" && principal != null)
                    where += " and a.Principal = '" + principal + "'";
                string UnitID = account.UnitID;
                if (UnitID == "32" || UnitID == "46")
                    where += "";
                else
                    where += " and a.UnitID = '" + account.UnitID + "'";
                UIDataTable udtTask = ProjectMan.getNewProjectContractExamineGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult PCashBack(string id)
        {
            if (id == "")
            {
                CCashBack Cashr = new CCashBack();
                return View(Cashr);
            }
            CCashBack Cash = new CCashBack();
            Cash.StrCID = id;
            Cash.StrCBID = ProjectMan.getNewProjectCBID(id);
            Cash.StrCurAmountNum = ProjectMan.getNewCurProjectAmountNum(id);
            return View(Cash);
        }

        public ActionResult CheckProjectMoney()
        {
            var CID = Request["data1"];
            var Money = Request["data2"];
            if (ProjectMan.checkNewMoney(CID, Money) == true)
                return Json(new { success = "true" });
            else
                return Json(new { success = "false" });
        }

        //public ActionResult test()
        //{
        //    获取上传的文件
        //    HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
        //     如果没有上传文件
        //    if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
        //    {
        //        return this.HttpNotFound();
        //    }
        //    return View();
        //}

        public ActionResult InsertProjectCCashBackFile()
        {
                //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }
            else
            {
                CCashBack Cash = new CCashBack();
                Cash.StrCID = Request["PID"].ToString();
                Cash.StrCBID = Request["CBID"].ToString();
                Cash.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Cash.StrCreateUser = account.UserID.ToString();
                Cash.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewProjectCCashBackFile(Cash, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertProjectCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {

                Cash.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Cash.StrCreateUser = account.UserID.ToString();
                Cash.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewProjectCCashBack(Cash,ref strErr) == true)
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

        public ActionResult DownLoadCashBack(string id)
        {
            ViewData["sid"] = id;
            return View();
        }

        public ActionResult GetCashBackFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ProjectMan.GetNewCashBackDownload(id);
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

        public void DownLoadCashBackFile(string id)
        {
            DataTable dtInfo = ProjectMan.GetNewCashBackDownloadOne(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
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

        public ActionResult deleteCashBackFile()
        {
            var id = Request["data1"];
            string strErr = "";
            if (ProjectMan.DeleteNewCashBackFile(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult ProCashBackGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string PID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["pid"] != null)
                PID = Request["pid"].ToString();
            if (PID != "")
                where += " and a.CID = '" + PID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ProjectMan.getNewProCashBackGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePCashBack(string id)
        {
            CCashBack Cash = new CCashBack();
            Cash = ProjectMan.getNewUpdateCashBack(id);
            return View(Cash);
        }

        public ActionResult CheckUpdateProjectMoney()
        {
            var CID = Request["data1"];
            var Money = Request["data2"];
            var CBID = Request["data3"];
            if (ProjectMan.checkNewUpdateMoney(CID, Money, CBID) == true)
                return Json(new { success = "true" });
            else
                return Json(new { success = "false" });
        }

        public ActionResult UpdateProjectCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ProjectMan.UpdateNewProjectCCashBack(Cash, ref strErr) == true)
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

        public ActionResult dellProjectCashBack()
        {
            var id = Request["data1"];
            string[] arr = id.Split('@');
            string strErr = "";
            if (ProjectMan.dellNewCCashBack(arr[0], arr[1], ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
    }
}
