using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI.Controllers
{
    [Authorization]
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectModule()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            ViewData["Role"] = account.UserRole;
            ViewData["UserID"] = account.UserID.ToString();
            return View();
        }

        public ActionResult SelectModuleYS()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            ViewData["Role"] = account.UserRole;
            ViewData["UserID"] = account.UserID.ToString();
            ViewData["UserName"] = account.UserName;
            ViewData["Unit"] = account.UnitName;
            return View();
        }

        public ActionResult SelectModuleFZ()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            ViewData["Role"] = account.UserRole;
            ViewData["UserID"] = account.UserID.ToString();
            ViewData["UserName"] = account.UserName;
            ViewData["Unit"] = account.UnitName;
            return View();
        }

        //这里的方法需要验证登录状态，以下雷同
        public ActionResult Main(string id)
        {
            //var Url = Request["Url"];
            //ViewData["Url"] = Url; 
            ViewData["Role"] = id;
            Acc_Account account = GAccount.GetAccountInfo();
            //ViewData["Role"] = account.UserRole;
            ViewData["UserName"] = account.UserName;
            ViewData["Unit"] = account.UnitName;
            //领导审批提示
            ViewData["UserId"] = account.UserID;
            ViewData["ExJob"] = account.Exjob;
            ViewData["RoleNames"] = account.RoleNames;
            return View();
        }
        public ActionResult SignJudge()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unitid = account.UnitID;
            #region MyRegion
            //string where = " and a.state != '-1' and a.UnitID = '" + unitid + "'";
            //string ck1 = EquipMan.getNewCK1time();
            //if (ck1 != "")
            //    where += " and (DATEDIFF(MONTH,GETDATE(),a.PlanDate)) <= '" + ck1 + "'";
            //else
            //    where += " and (DATEDIFF(MONTH,GETDATE(),a.PlanDate)) <= '2'";

            //string where1 = "";
            //string time = ContractMan.getNewReturnTime();
            //if (time == "")
            //    where1 = " and a.Unit = '" + unitid + "' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'";
            //else
            //    where1 = " and a.Unit = '" + unitid + "' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";

            #endregion
            //资质到期
            string where2 = " and b.state!='-1' and b.DeclareUnitID = '" + unitid + "' ";
            //string num = UserAptitudeMan.getNewAptitudeTime();//1,1,2
            string num = SupplyManage.getZZTimeOut();
            if (num == "")
                where2 += " and DATEDIFF(MONTH,GETDATE(),a.FTimeOut) <= '2'";
            else
                where2 += "  and DATEDIFF(MONTH,GETDATE(),a.FTimeOut) <= '" + num + "'";
            //证书到期
            string where4 = " and b.state!='-1' and b.DeclareUnitID = '" + unitid + "'";
            string ck = SupplyManage.getZSTimeOut();
            if (ck == "")
                where4 += " and DATEDIFF(MONTH,GETDATE(),a.TimeOut) <= '2'";
            else
                where4 += "  and DATEDIFF(MONTH,GETDATE(),a.TimeOut) <= '" + num + "'";
            #region 过期代码
            //string where3 = "";
            //if (unitid == "27")
            //    where3 += " and Recipient ='" + account.UserName + "'";
            //else
            //    where3 += " and 1<>1"; 
            #endregion

            string warn = MainMan.getNewJudgeWarnString(where2, where4);
            if (warn != "")
                return Json(new { success = "true", Msg = warn });
            else
                return Json(new { success = "false" });
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult Left_ProjectManage()
        {
            return View();
        }

        public ActionResult Left_SalesManage()
        {
            return View();
        }

        public ActionResult Left_FlowMeterManage()
        {
            return View();
        }

        public ActionResult Left_PPManage()
        {
            return View();
        }

        public ActionResult Left_SuppliesManage()
        {
            return View();
        }

        public ActionResult Left_InventoryManage()
        {
            return View();
        }
        public ActionResult Left_ProduceManage()
        {
            return View();
        }
        public ActionResult Left_CustomerService()
        {
            return View();
        }

        public ActionResult UpdatePwd()
        {
            return View();
        }

        public ActionResult SavePwd()
        {
            string OldPwd = Request["OldPwd"].ToString();
            string NewPwd = Request["NewPwd"].ToString();
            Acc_Account account = GAccount.GetAccountInfo();
            int UserId = account.UserID;
            string err = "";
            if (MainMan.UpdatePwd(OldPwd, NewPwd, UserId, ref err))
            {
                return Json(new { success = "true", Msg = err });
            }
            else
                return Json(new { success = "false", Msg = err });
        }

        public ActionResult ResetPwd()
        {
            return View();
        }

        public ActionResult LoadUserName()
        {
            string loginName = Request["data1"];
            DataTable dt = MainMan.getNewUserNamebyLoginName(loginName);
            if (dt.Rows.Count > 0)
            {
                string UserName = dt.Rows[0]["UserName"].ToString();
                string UserID = dt.Rows[0]["UserId"].ToString();
                return Json(new { success = "true", userId = UserID, userName = UserName });
            }
            else
                return Json(new { success = "false" });
        }

        public ActionResult SaveRestPwd()
        {
            string UserID = Request["UserID"].ToString();
            string err = "";
            if (MainMan.RestNewPwd(UserID, ref err))
            {
                return Json(new { success = "true", Msg = err });
            }
            else
                return Json(new { success = "false", Msg = err });
        }

        public ActionResult ValidateJudge()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string validate = MainMan.judgeNewValidate(UserId);
            if (validate != "")
            {
                return Json(new { success = "true", Msg = validate });
            }
            else
            {
                return Json(new { success = "false"});
            }

        }
    }
}
