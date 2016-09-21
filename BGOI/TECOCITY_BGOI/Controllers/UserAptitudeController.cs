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
    public class UserAptitudeController : Controller
    {
        //
        // GET: /UserAptitude/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AptitudeMaintain()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAptitude()
        {
            UserAptitude Uaptitude = new UserAptitude();
            return View(Uaptitude);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AptitudeUserGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = " and a.Unit = '" + account.UnitID + "'";
            string strCurPage;
            string strRowNum;
            string UserName = Request["userName"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (UserName != "")
                where += " and b.UserName like '%" + UserName + "%'";
            UIDataTable udtTask = UserAptitudeMan.getNewAptitudeUserGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AptitudeGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = "";
            string strCurPage;
            string strRowNum;
            string UserID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["userid"] != null)
                UserID = Request["userid"].ToString();
            if (UserID != "")
                where += " and a.UserID = '" + UserID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = UserAptitudeMan.getNewAptitudeGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, account.UnitID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LookPicture(string id)
        {
            ViewData["ID"] = id;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Uaptitude"></param>
        /// <returns></returns>
        public ActionResult InsertUserAptitude(UserAptitude Uaptitude)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            Uaptitude.StrCreateUser = account.UserID.ToString();
            Uaptitude.StrCreateTime = DateTime.Now;
            Uaptitude.StrValidate = "v";
            Uaptitude.StrUnit = account.UnitID;
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                Uaptitude.StrFileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (UserAptitudeMan.InsertNewUserAptitude(Uaptitude, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("AddAptitude", Uaptitude);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("AddAptitude", Uaptitude);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseUser()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UserGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = " and a.DeptId = '" + account.UnitID + "'";
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
                where += " and a.UserName like '%" + UserName + "%'";
            UIDataTable udtTask = UserAptitudeMan.getNewUserGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateAptitude(string id)
        {
            UserAptitude Uaptitude = new UserAptitude();
            Uaptitude = UserAptitudeMan.getNewUpdateUserAptitude(id);
            ViewData["ID"] = id;
            return View(Uaptitude);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFile()
        {
            var id = Request["data1"];
            DataTable dt = UserAptitudeMan.getNewFile(id);
            string ID = "";
            string name = "";
            string file = "";
            if (dt.Rows.Count > 0)
            {
                ID = dt.Rows[0]["ID"].ToString();
                name = dt.Rows[0]["FileName"].ToString();
                file = dt.Rows[0]["FileInfo"].ToString();
            }
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DownLoad(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = UserAptitudeMan.getNewFile(informNo);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (UserAptitudeMan.deleteNewFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Uaptitude"></param>
        /// <returns></returns>
        public ActionResult UpDataUserAptitude(UserAptitude Uaptitude)
        {
            var ID = Request["ID"];
            Acc_Account account = GAccount.GetAccountInfo();
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                Uaptitude.StrFileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (UserAptitudeMan.UpdateNewUserAptitude(ID, Uaptitude, fileByte, ref strErr) == true)
            {
                ViewData["ID"] = ID;
                ViewData["msg"] = "保存成功";
                return View("UpdateAptitude", Uaptitude);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UpdateAptitude", Uaptitude);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LendAptitude(string id)
        {
            string[] arr = id.Split('@');
            UCertificatLend Lend = new UCertificatLend();
            Lend.StrID = Convert.ToInt16(arr[0]);
            Lend.StrUserID = arr[1];
            Lend.StrUserName = arr[2];
            Lend.StrCertificatCode = arr[3];
            return View(Lend);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lend"></param>
        /// <returns></returns>
        public ActionResult InsertLendAptitude(UCertificatLend Lend)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            Lend.StrCreateUser = account.UserID.ToString();
            Lend.StrCreateTime = DateTime.Now;
            Lend.StrValidate = "v";
            string strErr = "";
            if (UserAptitudeMan.InsertNewLendAptitude(Lend, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BackAptitude(string id)
        {
            string[] arr = id.Split('@');
            UCertificatLend Lend = new UCertificatLend();
            Lend.StrID = Convert.ToInt16(arr[0]);
            Lend.StrUserID = arr[1];
            Lend.StrUserName = arr[2];
            Lend.StrCertificatCode = arr[3];
            return View(Lend);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lend"></param>
        /// <returns></returns>
        public ActionResult ReturnLendAptitude(UCertificatLend Lend)
        {
            string strErr = "";
            if (UserAptitudeMan.UpdateNewLendAptitude(Lend, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AptitudeWarn()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SetWarnTime()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult InsertConfigTime(string num)
        {
            string checkWay = "AptitudeWarn";
            string TimeType = "Aptitude";
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string strErr = "";
            if (EquipMan.InsertNewCongTime(checkWay, num, unit, TimeType, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AptitudeWarnGrid()
        {
            string where = "";
            Acc_Account account = GAccount.GetAccountInfo();
            where = " and a.Unit = '" + account.UnitID + "'";
            string num = UserAptitudeMan.getNewAptitudeTime();
            if (num == "")
                where += " and DATEDIFF(MONTH,GETDATE(),a.CertificatDate) <= '2'";
            else
                where += "  and DATEDIFF(MONTH,GETDATE(),a.CertificatDate) <= '" + num + "'";
            string strCurPage;
            string strRowNum;
            string UserName = Request["userName"].ToString();
            string BusinessType = Request["businessType"].ToString();
            string TecoName = Request["tecoName"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (UserName != "")
                where += " and a.UserName like '%" + UserName + "%'";
            if (BusinessType != "")
                where += " and a.BusinessType = '" + BusinessType + "'";
            if (TecoName != "")
                where += " and a.TecoName like '%" + TecoName + "%'";
            UIDataTable udtTask = UserAptitudeMan.getNewAptitudeGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, account.UnitID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }
}
