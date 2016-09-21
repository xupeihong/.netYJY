using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;
namespace TECOCITY_BGOI.Controllers
{
    public class ContractController : Controller
    {
        //
        // GET: /Contract/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractMaintain()
        {
            return View();
        }

        public ActionResult ProContractMaintain()
        {
            return View();
        }
        /// <summary>
        /// 可以
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractGrid(tk_ContractSearch ContractSearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                if (unit == "32" || unit == "46")
                    where += "";
                else
                     where += " and a.Unit = '" + unit + "'";
                if (account.UserRole == "3")
                    where += " and a.BusinessType = 'BT5'";
                string strCurPage;
                string strRowNum;
                string Cname = ContractSearch.Cname;
                string ContractID = ContractSearch.ContractID;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Cname != "" && Cname != null)
                    where += " and a.Cname like '%" + Cname + "%'";
                if (ContractID != "" && ContractID != null)
                    where += " and a.ContractID like '%" + ContractID + "%'";
                UIDataTable udtTask = ContractMan.getNewContractGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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

        public ActionResult CashBackGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string CID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Cid"] != null)
                CID = Request["Cid"].ToString();
            if (CID != "")
                where += " and a.CID = '" + CID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ContractMan.getNewCashBackGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
            string CID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Cid"] != null)
                CID = Request["Cid"].ToString();
            if (CID != "")
                where += " and a.UserId = '" + CID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = ContractMan.getNewUserlogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult JudgeSameContractID(string StrContractID)
        {
            List<string> list = ContractMan.GetNewContractID();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == StrContractID.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddContract(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas.StrCID = ContractMan.GetNewShowCID();
            if (id != null)
                Bas.StrPID = id;
            return View(Bas);
        }

        public ActionResult AddProjectContract(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas.StrCID = ContractMan.GetNewShowCID();
            DataTable dt = ContractMan.getNewMoneyFromProjectBas(id);
            if (dt.Rows.Count > 0)
            {
                Bas.StrPContractAmount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
                Bas.StrPBudget = Convert.ToDecimal(dt.Rows[0]["Budget"]);
                Bas.StrPCost = Convert.ToDecimal(dt.Rows[0]["Cost"]);
                Bas.StrPProfit = Convert.ToDecimal(dt.Rows[0]["Profit"]);
            }
            if (id != null)
                Bas.StrPID = id;
            return View(Bas);
        }

        public ActionResult InsertContractFile()
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

                ContractBas Bas = new ContractBas();
                Bas.StrCID = Request["PID"].ToString();
                Bas.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Bas.StrCreateUser = account.UserID.ToString();
                Bas.StrValidate = "v";
                string strErr = "";
                if (ProjectMan.InsertNewContratFile(Bas, Filedata, ref strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
        }

        public ActionResult InsertProjectContract(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Bas.StrBusinessType = "BT5";
                Bas.StrUnit = account.UnitID.ToString();
                Bas.StrCID = ContractMan.GetNewCID();
                Bas.StrCreateTime = DateTime.Now;
                Bas.StrCreateUser = account.UserID.ToString();
                Bas.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewProjectContract(Bas,ref strErr) == true)
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
                return View("AddProjectContract", Bas);
            }
        }

        public ActionResult DownloadFileProject(string id)
        {
            ViewData["StrCID"] = id;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFileProject()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ContractMan.GetNewDownLoadProject(id);
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

        public void DownLoadProject(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = ContractMan.GetNewDownloadFileProject(informNo);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult InsertContractBas(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Bas.StrUnit = account.UnitID.ToString();
                Bas.StrCID = ContractMan.GetNewCID();
                Bas.StrCreateTime = DateTime.Now;
                Bas.StrCreateUser = account.UserID.ToString();
                Bas.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewContractBas(Bas, ref strErr) == true)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddFile(string id)
        {
            ViewData["StrCID"] = id;
            return View();
        }

        public ActionResult DownloadFile(string id)
        {
            ViewData["StrCID"] = id;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = ContractMan.GetNewDownLoad(id);
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
            DataTable dtInfo = ContractMan.GetNewDownloadFile(informNo);
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

        public ActionResult deleteProjectFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (ContractMan.DeleteNewProjectFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (ContractMan.DellNewFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertForm()
        {
            string CID = Request["StrCID"];
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            string FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
            ViewData["FileName"] = FileName;


            int fileLength = file.ContentLength;
            if (fileLength != 0)
            {
                fileByte = new byte[fileLength];
                file.InputStream.Read(fileByte, 0, fileLength);
            }
            string strErr = "";
            if (ContractMan.InsertNewFile(CID, fileByte, FileName, ref strErr) == true)
            {
                ViewData["msg"] = "上传成功";
                ViewData["StrCID"] = CID;
                return View("AddFile");
            }
            else
            {
                ViewData["msg"] = "上传失败";
                return View("AddFile");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BGOI_KAT()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EntrustGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string TaskName = Request["taskname"].ToString();
            string ProjectNum = Request["projectNum"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (TaskName != "")
                where += " and a.TaskName like '%" + TaskName + "%'";
            if (ProjectNum != "")
                where += " and a.ProjectNum like '%" + ProjectNum + "%'";
            UIDataTable udtTask = DeceteMan.getNewEntrustGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        public ActionResult BGOI_SJS()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult MandateGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string MCode = Request["mcode"].ToString();
            string ProName = Request["ProName"].ToString();


            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (MCode != "")
                where += " and a.MCode like '%" + MCode + "%'";
            if (ProName != "")
                where += " and a.ProName like '%" + ProName + "'";

            UIDataTable udtMandate = MandateInfoMan.getMandateGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtMandate.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{\"page\":" + GFun.SafeToInt32(Request["curpage"]) + ",\"total\":" + udtMandate.IntTotalPages + ",\"records\":" + udtMandate.IntRecords + ",\"rows\":";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BGOI_Project()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectGrid(tk_ProjectSearch ProjectSearch)
        { 
            if (ModelState.IsValid)
            {
                string where = "";
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
                UIDataTable udtTask = ContractMan.getNewProjectGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeContract(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas = ContractMan.getNewChangeContract(id);
            return View(Bas);
        }

        public ActionResult ChangeProContract(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas = ContractMan.getNewChangeProContractBas(id);
            return View(Bas);
        }

        public ActionResult upProjectContract(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ContractMan.UpdateNewProjectContract(Bas, ref strErr) == true)
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
                return Json(new { success = "false", Msg = "保存出错" });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult UpdateContract(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ContractMan.UpdateNewContractBas(Bas, ref strErr) == true)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CashBack(string id, string type)
        {
            CCashBack Cash = new CCashBack();
            Cash.StrCID = id;
            Cash.StrCBID = ContractMan.GetNewshowCBID(id);
            Cash.StrCurAmountNum = ContractMan.GetNewCurAmountNum(id);
            ViewData["type"] = type;
            return View(Cash);
        }

        public ActionResult ProCashBack(string id, string type)
        {
            if (id == "")
            {
                CCashBack Cashr = new CCashBack();
                return View(Cashr);
            }
            string[] arr = id.Split('@');
            CCashBack Cash = new CCashBack();
            Cash.StrCID = arr[0];
            Cash.StrCBID = ContractMan.GetNewshowCBID(arr[0]);
            Cash.StrCurAmountNum = ContractMan.GetNewCurAmountNum(arr[0]);
            ViewData["PID"] = arr[1];
            ViewData["type"] = type;
            return View(Cash);
        }

        public ActionResult CheckMoney()
        {
            var CID = Request["data1"];
            var Money = Request["data2"];
            if (ContractMan.checkNewMoney(CID, Money) == true)
                return Json(new { success = "true" });
            else
                return Json(new { success = "false"});
        }

        public ActionResult InsertProCCashBack(CCashBack Cash, string PID)
        {
            if (ModelState.IsValid)
            {
                Cash.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Cash.StrCreateUser = account.UserID.ToString();
                Cash.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewProCCashBack(Cash,PID, ref strErr) == true)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        public ActionResult InsertCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {
                Cash.StrCreateTime = DateTime.Now;
                Acc_Account account = GAccount.GetAccountInfo();
                Cash.StrCreateUser = account.UserID.ToString();
                Cash.StrValidate = "v";
                string strErr = "";
                if (ContractMan.InsertNewCCashBack(Cash, ref strErr) == true)
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

        public ActionResult UpdateCashBack(string id)
        {
            CCashBack Cash = new CCashBack();
            Cash = ContractMan.getNewUpdateCashBack(id);
            return View(Cash);
        }

        public ActionResult upCCashBack(CCashBack Cash)
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                if (ContractMan.UpdateNewCCashBack(Cash, ref strErr) == true)
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

        public ActionResult dellCashBack()
        {
            var id = Request["data1"];
            string[] arr = id.Split('@');
            string strErr = "";
            if (ContractMan.dellNewCCashBack(arr[0],arr[1], ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Settlement(string id)
        {
            CSettlement CST = new CSettlement();
            CST.StrCID = id;
            CST.StrDebtAmount = ContractMan.getNewDebtAmount(id);
            if (CST.StrDebtAmount == 0)
                CST.StrIsDebt = 0;
            else
                CST.StrIsDebt = 1;
            return View(CST);
        }

        public ActionResult SettlementPro(string id)
        {
            string[] arr = id.Split('@');
            CSettlement CST = new CSettlement();
            ViewData["PID"] = arr[1];
            CST.StrCID = arr[0];
            CST.StrDebtAmount = ContractMan.getNewDebtAmountPro(arr[0]);
            if (CST.StrDebtAmount == 0)
                CST.StrIsDebt = 0;
            else
                CST.StrIsDebt = 1;
            return View(CST);
        }

        public ActionResult InsertSettlementPro(CSettlement CST,string PID)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                CST.StrCreateUser = account.UserID.ToString();
                CST.StrCreateTime = DateTime.Now;
                string strErr = "";
                if (ContractMan.InsertNewSettlementPro(CST,PID, ref strErr) == true)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CST"></param>
        /// <returns></returns>
        public ActionResult InsertSettlement(CSettlement CST)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                CST.StrCreateUser = account.UserID.ToString();
                CST.StrCreateTime = DateTime.Now;
                string strErr = "";
                if (ContractMan.InsertNewSettlement(CST, ref strErr) == true)
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StayReturnCash()
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
            string checkWay = "ReturnCash";
            string TimeType = "Contract";
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
        public ActionResult StayReturnCashGrid(tk_ContractSearch ContractSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = "";
                string time = ContractMan.getNewReturnTime();
                if (time == "")
                    where = " and a.Unit = '" + unit + "' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '2'";
                else
                    where = " and a.Unit = '" + unit + "' and a.state != '2' and DATEDIFF(MONTH,GETDATE(),a.CPlanEndTime) <= '" + time + "'";
                string strCurPage;
                string strRowNum;
                //string PayOrIncome = Request["payOrIncome"].ToString();
                string Cname = ContractSearch.Cname;
                string ContractID = ContractSearch.ContractID;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                //if (PayOrIncome != "")
                //    where += " and a.PayOrIncome = '" + PayOrIncome + "'";
                if (Cname != "" && Cname != null)
                    where += " and a.Cname like '%" + Cname + "%'";
                if (ContractID != "" && ContractID != null)
                    where += " and a.ContractID = '" + ContractID + "'";
                UIDataTable udtTask = ContractMan.getNewContractGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StaySettlement()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult StaySettlementGrid()
        {
            string where = " and DATEDIFF(DAY,GETDATE(),a.CPlanEndTime) <= 7";
            string strCurPage;
            string strRowNum;
            string BusinessType = Request["businessType"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (BusinessType != "")
                where += " and a.BusinessType = '" + BusinessType + "'";
            UIDataTable udtTask = ContractMan.getNewContractGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        public ActionResult DetailContract(string id)
        {
            string where = " and a.CID = '" + id + "'";
            DataTable dt = ContractMan.getNewDetailContract(where);
            StringBuilder sb = new StringBuilder();
            sb.Append(" <div id=\"tabTitile\"><span style=\"margin-left:10px;\">合同ID：" + dt.Rows[0]["CID"].ToString() + "</span></div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">合同编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ContractID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">业务类型</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BusinessTypeDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">对应项目编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Cname"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:100px;\"><td class=\"textLeft\">合同内容</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ContractContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">合同开始时间</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["CStartTime"].ToString() + "</td><td class=\"textLeft\" style=\"width:25%\">合同工期</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["TimeScale"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">预计完工时间</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["CPlanEndTime"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同初始金额</td><td class=\"textRight\">" + dt.Rows[0]["CBeginAmount"].ToString() + "</td><td class=\"textLeft\">履约保证金</td><td class=\"textRight\">" + dt.Rows[0]["Margin"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同签订回款次数</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["AmountNum"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同签订日期</td><td class=\"textRight\">" + dt.Rows[0]["Ctime"].ToString() + "</td><td class=\"textLeft\">负责人</td><td class=\"textRight\">" + dt.Rows[0]["Principal"].ToString() + "</td></tr>");
            if (dt.Rows[0]["CEndAmount"].ToString() != "0.00")
                sb.Append("<tr><td class=\"textLeft\">变更后金额</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["CEndAmount"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同款向</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PayOrIncome"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">甲方</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PartyA"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">乙方</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PartyB"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">备注</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Rmark"].ToString() + "</td></tr>");
            sb.Append("");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult DetailProjectContract(string id)
        {
            string where = " and a.CID = '" + id + "'";
            DataTable dt = ContractMan.getNewDetailContract(where);
            StringBuilder sb = new StringBuilder();
            sb.Append(" <div id=\"tabTitile\"><span style=\"margin-left:10px;\">合同ID：" + dt.Rows[0]["CID"].ToString() + "</span></div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">合同编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ContractID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">业务类型</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BusinessTypeDesc"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">对应项目编号</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["PID"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">合同名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Cname"].ToString() + "</td></tr>");
            sb.Append("<tr style=\"height:100px;\"><td class=\"textLeft\">合同内容</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ContractContent"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">项目合同额</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["PContractAmount"].ToString() + "</td><td class=\"textLeft\" style=\"width:25%\">项目前期费用（管理费、预算）</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["PBudget"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">项目成本</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["PCost"].ToString() + "</td><td class=\"textLeft\" style=\"width:25%\">项目利润</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["PProfit"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\" style=\"width:25%\">合同开始时间</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["CStartTime"].ToString() + "</td><td class=\"textLeft\" style=\"width:25%\">预计完工时间</td><td class=\"textRight\" style=\"width:25%\">" + dt.Rows[0]["CPlanEndTime"].ToString() + "</td></tr>");
            sb.Append("<tr></tr>");
            //sb.Append("<tr><td class=\"textLeft\">合同签订回款次数</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["AmountNum"].ToString() + "</td></tr>");
            if (dt.Rows[0]["CEndAmount"].ToString() != "0.00")
                sb.Append("<tr><td class=\"textLeft\">变更后金额</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["CEndAmount"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">备注</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Rmark"].ToString() + "</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractStandingBook()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractStandingBookGrid(tk_ContractSearch ContractSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = " and a.Unit = '" + unit + "'";
                string strCurPage;
                string strRowNum;
                string Cname = ContractSearch.Cname;
                string ContractID = ContractSearch.ContractID;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Cname != "" && Cname != null)
                    where += " and a.Cname like '%" + Cname + "%'";
                if (ContractID != "" && ContractID != null)
                    where += " and a.ContractID like '%" + ContractID + "%'";
                UIDataTable udtTask = ContractMan.getNewStandingBookGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ToExcel()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string unitName = account.UnitName;
            string where = " and a.Unit = '" + unit + "'";
            string strCurPage;
            string strRowNum;
            string Cname = Request["cname"].ToString();
            string ContractID = Request["contractID"].ToString();
            string year = DateTime.Now.ToString("yyyy");
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Cname != "")
                where += " and a.Cname like '%" + Cname + "%'";
            if (ContractID != "")
                where += " and a.ContractID like '%" + ContractID + "%'";
            DataTable data = ContractMan.getNewPrintStandingBook(where);
            if (data != null)
            {
                string strCols = "序号-2000,合同编号-5000,合同名称-5000,内容-5000,签署日期-5000,甲方单位-7000,乙方单位-7000,合同金额-5000,页数-5000,经办人-5000,备注-7000";
                data.Columns["xu"].SetOrdinal(0);
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, year + "年" + unitName + "合同管理台账", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "Info.xls");
            }
            else
                return null;
        }
    }
}
