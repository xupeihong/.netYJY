using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    [Authorization]
    [AuthorizationAttribute]
    public class CustomerServiceController : Controller
    {
        //
        // GET: /CustomerService/

        public ActionResult Index()
        {
            return View();
        }
        //根据PID加载产品规格
        public ActionResult GetProSpec()
        {
            string KID = Request["DDL"].ToString();
            DataTable dt = CustomerServiceMan.GetProSpec(KID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #region [加载订单]
        public ActionResult OrderList()
        {
            return View();
        }
        public ActionResult ChangeOrderList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Ptype = Request["OrderContactor"].ToString();

            UIDataTable udtTask = CustomerServiceMan.ChangeOrderList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region [客户信息]
        public ActionResult GetKClientBas()
        {
            string KID = Request["DDL"].ToString();
            DataTable dt = CustomerServiceMan.GetKClientBas(KID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region [加载产品]
        public ActionResult ChangeProduct()
        {
            return View();
        }
        //加载产品类型
        public ActionResult GetPtype()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "8";

            UIDataTable udtTask = CustomerServiceMan.GetPtype(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //加载产品信息
        public ActionResult ChangePtypeList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Ptype = Request["ptype"].ToString();

            UIDataTable udtTask = CustomerServiceMan.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region [用户服务]

        #region [售后服务需求]

        public ActionResult ServiceDemand()
        {
            return View();
        }

        #endregion
        #region [用户回访]
        /// <summary>
        /// 验证编号是否重名方法
        /// </summary>
        /// <param name="SupplierCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RecordIDExists(string RecordID)
        {
            List<string> list = CustomerServiceMan.GetCode();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == RecordID.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserVisit()
        {
            return View();
        }
        public ActionResult AddUserVisit()
        {
            tk_SHReturnVisit so = new TECOCITY_BGOI.tk_SHReturnVisit();
            so.HFID = CustomerServiceMan.GetTopHFID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //添加
        public ActionResult SaveUserVisit()
        {
            if (ModelState.IsValid)
            {
                string HFIDold = "";
                string type = Request["Type"].ToString();
                if (type == "1")
                {
                    HFIDold = Request["HFIDold"].ToString();
                }
                else
                {
                    HFIDold = "";
                }
                tk_SHReturnVisit allr = new tk_SHReturnVisit();
                allr.HFID = Request["HFID"].ToString();
                allr.RecordID = Request["RecordID"].ToString();
                allr.ContactPerson = Request["ContactPerson"].ToString();
                allr.UserInformation = Request["UserInformation"].ToString();
                allr.SatisfiedDegree = Request["SatisfiedDegree"].ToString();
                allr.Tel = Request["Tel"].ToString();
                allr.ReturnVisit = Request["ReturnVisit"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.RVDate = Convert.ToDateTime(Request["CreateTime"]);
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.ReturnVisit = Request["ReturnVisit"].ToString();
                allr.UntiID = GAccount.GetAccountInfo().UnitID;
                allr.Validate = "v";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                List<tk_SHRV_Product> list = new List<tk_SHRV_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHRV_Product allpro = new tk_SHRV_Product();
                    allpro.HFID = allr.HFID;

                    allpro.DID = CustomerServiceMan.GetTopHFIDDID();
                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    if (Total[i] != "")
                    {
                        allpro.Total = Convert.ToDecimal(Total[i]);
                    }
                    if (UnitPrice[i] != "")
                    {
                        allpro.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    }
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    allpro.Validate = "v";
                    list.Add(allpro);
                }
                string strErr = "";

                if (type == "1")//修改
                {
                    bool b = CustomerServiceMan.SaveUserVisit(allr, list, type, HFIDold, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改用户回访";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHReturnVisit";
                        log.Typeid = Request["HFID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改用户回访";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHReturnVisit";
                        log.Typeid = Request["HFID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//添加
                {
                    bool b = CustomerServiceMan.SaveUserVisit(allr, list, type, HFIDold, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加用户回访";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHReturnVisit";
                        log.Typeid = Request["HFID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加用户回访";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHReturnVisit";
                        log.Typeid = Request["HFID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //用户回访列表页面
        public ActionResult UserVisitList(UserVisitQuery uvquery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v'  and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string HFID = uvquery.HFID;
                string ReturnVisit = uvquery.ReturnVisit;
                string Tel = uvquery.Tel;
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["HFID"].ToString().Trim() != "")
                    where += " HFID like '%" + Request["HFID"].ToString().Trim() + "%' and";
                if (Request["ReturnVisit"].ToString().Trim() != "")
                    where += " ReturnVisit like '%" + Request["ReturnVisit"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " RVDate between '" + Begin + "' and '" + End + "' and";
                if (Request["Tel"].ToString().Trim() != "")
                    where += " Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.UserVisitList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "查询条件验证不通过！" });
            }

        }
        //导出用户回访
        public FileResult UserVisitListToExcel()
        {
            string where = " a.Validate='v' and b.Validate='v' and a.HFID=b.HFIDID  and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string HFID = Request["HFID"].ToString().Trim();
            string ReturnVisit = Request["ReturnVisit"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            //if (HFID != "")
            //    where += " a.HFID like '%" + HFID + "%' and";
            if (ReturnVisit != "")
                where += " a.ReturnVisit like '%" + ReturnVisit + "%' and";
            if (Begin != "" && End != "")
                where += " a.RVDate between '" + Begin + "' and '" + End + "' and";
            if (Request["Tel"].ToString().Trim() != "")
                where += " a.Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
            if (!string.IsNullOrEmpty(HFID))
            {
                where += "  a.HFID like '%" + HFID + "%' and b.HFIDID like '%" + HFID + "%' and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHReturnVisit a,BGOI_CustomerService.dbo.tk_SHSurvey b";
            //string FieldName = " a.HFID, a.UntiID,  a.RecordID,  a.RVDate,  a.ProductID,  a.UserInformation,  a.ContactPerson,  a.Tel,  a.SatisfiedDegree,  a.Remark,  a.CreateTime,  a.CreateUser,   a.ReturnVisit ,b.SurveyDate, b.CustomerID, b.Customer, b.ProductQuality, b.ProductQrice, b.ProductDelivery, b.ProductSurvey, b.CustomerServiceSurvey, b.SupplySurvey, b.LeakSurvey, b.ServiceSurvey, b.AgencySales, b.AgencyConsultation, b.AgencySpareParts, b.AgencySurvey, b.Remark, b.UserSign, b.CreateTime, b.CreateUser  ";
            string FieldName = " a.HFID,a.RecordID,a.RVDate,a.ProductID,a.UserInformation,a.ContactPerson, " +
                       " a.Tel,a.SatisfiedDegree,a.Remark,a.CreateTime,a.CreateUser,a.ReturnVisit,b.SurveyDate,b.CustomerID,b.Customer, " +
                       " (case when b.ProductQuality=0 then '非常满意'when b.ProductQuality=1 then '满意'when b.ProductQuality=2 then '一般'when b.ProductQuality=3 then '不满意' end) as  ProductQuality, " +
                       " (case when b.ProductQrice=0 then '非常满意'when b.ProductQrice=1 then '满意'when b.ProductQrice=2 then '一般'when b.ProductQrice=3 then '不满意' end) as  ProductQrice, " +
                       " (case when b.ProductDelivery=0 then '非常满意'when b.ProductDelivery=1 then '满意'when b.ProductDelivery=2 then '一般'when b.ProductDelivery=3 then '不满意' end) as  ProductDelivery, " +
                       " b.ProductSurvey,(case when b.CustomerServiceSurvey=0 then '非常满意'when b.CustomerServiceSurvey=1 then '满意'when b.CustomerServiceSurvey=2 then '一般'when b.CustomerServiceSurvey=3 then '不满意' end) as  CustomerServiceSurvey, " +
                       " (case when b.SupplySurvey=0 then '非常满意'when b.SupplySurvey=1 then '满意'when b.SupplySurvey=2 then '一般'when b.SupplySurvey=3 then '不满意' end) as  SupplySurvey, " +
                       " (case when b.LeakSurvey=0 then '非常满意' when b.LeakSurvey=1 then '满意'when b.LeakSurvey=2 then '一般' when b.LeakSurvey=3 then '不满意' end) as  LeakSurvey, " +
                       " b.ServiceSurvey,(case when b.AgencySales=0 then '非常满意'when b.AgencySales=1 then '满意'when b.AgencySales=2 then '一般'when b.AgencySales=3 then '不满意' end) as  AgencySales, " +
                       " (case when b.AgencyConsultation=0 then '非常满意' when b.AgencyConsultation=1 then '满意'when b.AgencyConsultation=2 then '一般'when b.AgencyConsultation=3 then '不满意' end) as  AgencyConsultation, " +
                       " (case when b.AgencySpareParts=0 then '非常满意' when b.AgencySpareParts=1 then '满意' when b.AgencySpareParts=2 then '一般'  when b.AgencySpareParts=3 then '不满意' end) as  AgencySpareParts, " +
                       " b.AgencySurvey,b.Remark,b.UserSign,b.CreateTime,b.CreateUser  ";
            string OrderBy = " a.CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                //回访处理单位-6000,a.UntiID,
                string strCols = "回访编号-6000,编号-6000,回访日期-5000,产品-6000,用户情况简述-5000,联系人-3000,";
                strCols += "电话-3000,对此次服务满意度-6000,备注-5000,创建时间-5000,创建人-5000,回访人-5000,调查日期-6000,客户编号-5000, " +
                            " 客户名称-6000,产品质量-5000,产品价格-5000,产品交货期-3000,产品调查说明原因-6000," +
                            " 服务售后维修 保养服务调查结果-5000,服务备品 备件供应调查结果-5000,有无漏气现象调查结果-5000,服务调查说明原因-5000," +
                            " 代理售后维修 保养服务调查结果-3000,代理咨询 维护培训调查结果-6000,代理备品 备件供应调查结果-5000, " +
                            " 代理调查说明原因-5000,备注-5000,用户签字-5000,创建时间-5000,登记人-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "用户回访信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "用户回访信息表.xls");
            }
            else
                return null;

        }


        //详细页面
        public ActionResult UserVisitDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string HFID = Request["HFID"].ToString();


            UIDataTable udtTask = CustomerServiceMan.UserVisitDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, HFID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //撤销
        public ActionResult DeUserVisit()
        {
            string strErr = "";
            string HFID = Request["HFID"].ToString();

            if (CustomerServiceMan.DeUserVisit(HFID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销用户回访";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHReturnVisit";
                log.Typeid = Request["HFID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销用户回访";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHReturnVisit";
                log.Typeid = Request["HFID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpUserVisitList()
        {
            tk_SHReturnVisit so = new TECOCITY_BGOI.tk_SHReturnVisit();
            string HFID = Request["HFID"];
            DataTable dt = CustomerServiceMan.UpUserVisitPIDList(HFID);
            //so.HFID = CustomerServiceMan.GetTopHFID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            if (dt.Rows.Count > 0 && dt != null)
            {
                so.RecordID = dt.Rows[0][2].ToString();
            }
            if (HFID != "")
            {
                ViewData["HFID"] = HFID.ToString();
            }
            return View(so);
        }
        //加载修改产品的信息
        public ActionResult UpUserVisitPIDList()
        {
            string HFID = Request["HFID"].ToString();
            DataTable dt = CustomerServiceMan.UpUserVisitPIDList(HFID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //加载打印数据
        public ActionResult PrintUserVisit()
        {
            string HFID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(HFID))
            {
                s += " HFID like '%" + HFID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHReturnVisit ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_SHReturnVisit so = new TECOCITY_BGOI.tk_SHReturnVisit();
            foreach (DataRow dt in data.Rows)
            {
                so.HFID = dt["HFID"].ToString();
                so.RecordID = dt["RecordID"].ToString();
                so.RVDate = Convert.ToDateTime(dt["RVDate"]);
                so.ProductID = dt["ProductID"].ToString();
                ViewData["ProdectName"] = "";
                so.UserInformation = dt["UserInformation"].ToString();
                so.ContactPerson = dt["ContactPerson"].ToString();
                so.Tel = dt["Tel"].ToString();
                if (dt["SatisfiedDegree"].ToString() == "0")
                {
                    so.SatisfiedDegree = "非常满意  ";
                }
                else if (dt["SatisfiedDegree"].ToString() == "1")
                {
                    so.SatisfiedDegree = "满意";
                }
                else if (dt["SatisfiedDegree"].ToString() == "2")
                {
                    so.SatisfiedDegree = "一般";
                }
                else
                {
                    so.SatisfiedDegree = "不满意";
                }
                so.Remark = dt["Remark"].ToString();
                so.ReturnVisit = dt["ReturnVisit"].ToString();
            }
            return View(so);
        }
        //打印加载物料信息
        public ActionResult GetUserVisit()
        {
            string HFID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(HFID))
            {
                s += " HFID like '%" + HFID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHRV_Product  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        //顾客满意度
        public ActionResult GetCustomerSatisfactionSurveyUserVisit()
        {
            string HFID = Request["HFID"].ToString();
            DataTable dt = CustomerServiceMan.GetCustomerSatisfactionSurveyUserVisit(HFID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //顾客满意度调查表列表页面
        public ActionResult CustomerSatisfactionSurveyList()
        {
            string where = " Validate='v'  and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //if (Request["HFID"] != "")
            where += " HFIDID = '" + Request["HFID"] + "' and";
            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.CustomerSatisfactionSurveyList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //判断有无满意度调查
        public ActionResult IfGetDiaoCha()
        {
            string HFID = Request["HFID"].ToString();
            DataTable dt = CustomerServiceMan.IfGetDiaoCha(HFID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        // 修改删除产品
        public ActionResult DeUserPro()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();
            string HFID = Request["HFID"].ToString();

            if (CustomerServiceMan.DeUserPro(HFID, PID))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = " 删除产品";
                log.LogContent = "删除成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHRV_Product";
                log.Typeid = Request["HFID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "删除产品";
                log.LogContent = "删除失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHRV_Product";
                log.Typeid = Request["HFID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        #region [公共信息维护]
        public ActionResult BasCus()
        {
            return View();
        }
        public ActionResult ConfigurationInformationList()
        {
            string where = " Validate = 'v' and";
            string strCurPage;
            string strRowNum;
            UIDataTable udtTask = new UIDataTable();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sel = Request["sel"].ToString();
            if (sel == "1")
            {
                where += " [Type] = 'gongshi' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = CustomerServiceMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            }
            else if (sel == "2")
            {

                where += " [Type] = 'CBMethod' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = CustomerServiceMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            }
            else
            {
                where += " [Type] = '' and";
                where = where.Substring(0, where.Length - 3);
                udtTask = CustomerServiceMan.ConfigurationInformationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);

            }
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCon(string id)
        {
            ViewData["type"] = id;
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertContentnew()
        {
            var type = Request["Type"];
            var text = Request["Text"].ToString().Trim();
            string strErr = "";
            if (CustomerServiceMan.InsertNewContentnew(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteContentnew()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (CustomerServiceMan.DeleteNewContentnew(xid, type, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateCon(string id)
        {
            string[] arr = id.Split('@');
            ViewData["XID"] = arr[0];
            ViewData["Type"] = arr[1];
            ViewData["Text"] = arr[2];
            return View();
        }
        public ActionResult upNewContentnew()
        {
            var ID = Request["XID"];
            var Type = Request["Type"];
            var Text = Request["Text"];
            string strErr = "";
            if (CustomerServiceMan.UpdateNewContentnew(ID, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "修改成功" });
            else
                return Json(new { success = "false", Msg = "修改出错" + "/" + strErr });
        }
        public ActionResult GetBasCus()
        {
            string TSID = Request["TSID"].ToString();
            DataTable dt = CustomerServiceMan.GetBasCus(TSID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #endregion
        #region [顾客满意度调查]

        public ActionResult CustomerSatisfactionSurvey()
        {
            return View();
        }
        public ActionResult AddCustomerSatisfactionSurvey()
        {
            tk_SHSurvey so = new TECOCITY_BGOI.tk_SHSurvey();
            so.DCID = CustomerServiceMan.GetTopDCID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["HFID"] = Request["HFID"].ToString(); ;
            return View(so);
        }
        public ActionResult GetCustomerSatisfactionSurvey()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = CustomerServiceMan.GetCustomerSatisfactionSurvey(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加2       //修改1
        public ActionResult SaveCustomerSatisfactionSurvey()
        {
            if (ModelState.IsValid)
            {
                string type = Request["Type"].ToString();
                tk_SHSurvey allr = new tk_SHSurvey();
                allr.DCID = Request["DCID"].ToString();

                allr.Customer = Request["CustomerID"].ToString();
                allr.CustomerID = Request["CustomerID"].ToString();
                allr.Address = Request["Address"].ToString();
                allr.Tel = Request["Tel"].ToString();
                allr.UserPerson = Request["ContactPerson"].ToString();


                allr.ProductQrice = Request["ProductQrice"].ToString();
                allr.ProductQuality = Request["ProductQuality"].ToString();
                allr.ProductDelivery = Request["ProductDelivery"].ToString();
                allr.ProductSurvey = Request["ProductSurvey"].ToString();
                allr.CustomerServiceSurvey = Request["CustomerServiceSurvey"].ToString();
                allr.SupplySurvey = Request["SupplySurvey"].ToString();
                allr.LeakSurvey = Request["LeakSurvey"].ToString();
                allr.ServiceSurvey = Request["ServiceSurvey"].ToString();
                allr.AgencySales = Request["AgencySales"].ToString();
                allr.AgencyConsultation = Request["AgencyConsultation"].ToString();
                allr.AgencySpareParts = Request["AgencySpareParts"].ToString();
                allr.AgencySurvey = Request["AgencySurvey"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.SurveyDate = Convert.ToDateTime(Request["Begin"]);
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.Other = Request["Other"].ToString();
                allr.HFIDID = Request["HFIDID"].ToString();
                allr.Validate = "v";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] OrderNum = Request["OrderNum"].Split(',');
                string[] OrderForm = Request["OrderForm"].Split(',');
                string[] OrderDate = Request["OrderDate"].Split(',');
                List<tk_SHSurvey_Product> list = new List<tk_SHSurvey_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHSurvey_Product allpro = new tk_SHSurvey_Product();
                    allpro.DCID = allr.DCID;
                    allpro.DID = CustomerServiceMan.GetTopDCIDDID();
                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.OrderForm = OrderForm[i];
                    allpro.SpecsModels = SpecsModels[i];
                    if (string.IsNullOrEmpty(OrderNum[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(OrderNum[i]);
                    }
                    allpro.OrderDate = Convert.ToDateTime(OrderDate[i]);
                    list.Add(allpro);
                }
                string strErr = "";

                if (type == "1")//修改
                {
                    bool b = CustomerServiceMan.SaveCustomerSatisfactionSurvey(allr, list, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改顾客满意度调查";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHSurvey";
                        log.Typeid = Request["DCID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改顾客满意度调查";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHSurvey";
                        log.Typeid = Request["DCID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//添加
                {
                    bool b = CustomerServiceMan.SaveCustomerSatisfactionSurvey(allr, list, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加顾客满意度调查";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHSurvey";
                        log.Typeid = Request["DCID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加顾客满意度调查";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHSurvey";
                        log.Typeid = Request["DCID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeCustomerSatisfactionSurvey()
        {
            string strErr = "";
            string DCID = Request["DCID"].ToString();

            if (CustomerServiceMan.DeCustomerSatisfactionSurvey(DCID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销顾客满意度调查";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHSurvey";
                log.Typeid = Request["DCID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销顾客满意度调查";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHSurvey";
                log.Typeid = Request["DCID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //导出
        public FileResult CustomerSatisfactionSurveyToExcel()
        {
            //string DCID = Request["DCID"];
            string where = "  a.Validate='v'  and";
            string s = "";
            //if (!string.IsNullOrEmpty(DCID))
            //{
            //    s += " DCID like '%" + DCID + "%' and";
            //}
            //if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHReturnVisit a,BGOI_CustomerService.dbo.tk_SHSurvey b ";
            //string FieldName = " HFIDID,DCID, UntiID, SurveyDate, CustomerID, Customer, ProductQuality, ProductQrice, ProductDelivery, ProductSurvey, CustomerServiceSurvey, SupplySurvey, LeakSurvey, ServiceSurvey, AgencySales, AgencyConsultation, AgencySpareParts, AgencySurvey, Remark, UserSign, CreateTime, CreateUser ";

            string FieldName = " a.HFID,b.HFIDID,a.UntiID,a.RecordID,a.RVDate,a.ProductID,a.UserInformation,a.ContactPerson, " +
                        " a.Tel,a.SatisfiedDegree,a.Remark,a.CreateTime,a.CreateUser,a.ReturnVisit,b.SurveyDate,b.CustomerID,b.Customer, " +
                        " (case when b.ProductQuality=0 then '非常满意'when b.ProductQuality=1 then '满意'when b.ProductQuality=2 then '一般'when b.ProductQuality=3 then '不满意' end) as  ProductQuality, " +
                        " (case when b.ProductQrice=0 then '非常满意'when b.ProductQrice=1 then '满意'when b.ProductQrice=2 then '一般'when b.ProductQrice=3 then '不满意' end) as  ProductQrice, " +
                        " (case when b.ProductDelivery=0 then '非常满意'when b.ProductDelivery=1 then '满意'when b.ProductDelivery=2 then '一般'when b.ProductDelivery=3 then '不满意' end) as  ProductDelivery, " +
                        " b.ProductSurvey,(case when b.CustomerServiceSurvey=0 then '非常满意'when b.CustomerServiceSurvey=1 then '满意'when b.CustomerServiceSurvey=2 then '一般'when b.CustomerServiceSurvey=3 then '不满意' end) as  CustomerServiceSurvey, " +
                        " (case when b.SupplySurvey=0 then '非常满意'when b.SupplySurvey=1 then '满意'when b.SupplySurvey=2 then '一般'when b.SupplySurvey=3 then '不满意' end) as  SupplySurvey, " +
                        " (case when b.LeakSurvey=0 then '非常满意' when b.LeakSurvey=1 then '满意'when b.LeakSurvey=2 then '一般' when b.LeakSurvey=3 then '不满意' end) as  LeakSurvey, " +
                        " b.ServiceSurvey,(case when b.AgencySales=0 then '非常满意'when b.AgencySales=1 then '满意'when b.AgencySales=2 then '一般'when b.AgencySales=3 then '不满意' end) as  AgencySales, " +
                        " (case when b.AgencyConsultation=0 then '非常满意' when b.AgencyConsultation=1 then '满意'when b.AgencyConsultation=2 then '一般'when b.AgencyConsultation=3 then '不满意' end) as  AgencyConsultation, " +
                        " (case when b.AgencySpareParts=0 then '非常满意' when b.AgencySpareParts=1 then '满意' when b.AgencySpareParts=2 then '一般'  when b.AgencySpareParts=3 then '不满意' end) as  AgencySpareParts, " +
                        " b.AgencySurvey,b.Remark,b.UserSign,b.CreateTime,b.CreateUser";
            string OrderBy = " a.CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "回访编号-6000,满意度编号-6000,调查单位-6000,调查日期-6000,客户编号-5000,客户名称-6000,产品质量调查结果-5000,产品价格调查结果-3000,";
                strCols += "产品交货期调查结果-3000,产品调查说明原因-6000,服务售后维修 保养服务调查结果-5000,服务备品 备件供应调查结果-5000,有无漏气现象调查结果-5000,服务调查说明原因-5000,";
                strCols += "代理售后维修 保养服务调查结果-3000,代理咨询 维护培训调查结果-6000,代理备品 备件供应调查结果-5000,代理调查说明原因-5000,备注-5000,用户签字-5000,创建时间-5000,登记人-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "顾客满意度调查信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "顾客满意度调查信息表.xls");
            }
            else
                return null;

        }
        //加载打印数据
        public ActionResult PrintCustomerSatisfactionSurvey()
        {
            string HFIDID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(HFIDID))
            {
                s += " HFIDID like '%" + HFIDID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHSurvey ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_SHSurvey so = new TECOCITY_BGOI.tk_SHSurvey();
            foreach (DataRow dt in data.Rows)
            {
                so.DCID = dt["DCID"].ToString();
                so.Customer = dt["Customer"].ToString();//一会处理
                so.SurveyDate = Convert.ToDateTime(dt["SurveyDate"]);

                //ViewData["SurveyDate"] = Convert.ToDateTime(dt["SurveyDate"]).ToString("yyy/mm/dd");

                so.UserSign = dt["UserSign"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.ProductSurvey = dt["ProductSurvey"].ToString();
                so.ServiceSurvey = dt["ServiceSurvey"].ToString();
                so.AgencySurvey = dt["AgencySurvey"].ToString();

                so.Address = dt["Address"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.UserPerson = dt["UserPerson"].ToString();
            }
            return View(so);
        }
        //打印加载物料信息
        public ActionResult GetCustomerSatisfactionSurveyList()
        {
            string DCID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(DCID))
            {
                s += " DCID in(select DCID from BGOI_CustomerService.dbo.tk_SHSurvey where  HFIDID='" + DCID + "') ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHSurvey_Product  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpCustomerSatisfactionSurveyList()
        {
            tk_SHSurvey so = new TECOCITY_BGOI.tk_SHSurvey();
            //so.DCID = CustomerServiceMan.GetTopDCID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["DCID"] = Request["DCID"].ToString();
            return View(so);
        }
        //打印顾客满意度
        public ActionResult UpSurveyList()
        {
            string DCID = Request["DCID"].ToString();
            DataTable dt = CustomerServiceMan.UpSurveyList(DCID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //详细页面
        public ActionResult UserCustomerSatisfactionSurveyList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string DCID = Request["DCID"].ToString();


            UIDataTable udtTask = CustomerServiceMan.UserCustomerSatisfactionSurveyList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, DCID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region [用户投诉]
        public ActionResult UserComplaints()
        {
            ViewData["webkey"] = "售后处理记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后处理记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }
        //用户投诉列表页面
        public ActionResult UserComplaintsList(UserComplaintsQuery comquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PID = comquery.PID;// Request["TSID"].ToString().Trim();
                string OrderContent = comquery.OrderContent;// Request["OrderContent"].ToString().Trim();
                string UserName = comquery.UserName;// Request["UserName"].ToString().Trim();
                string FirstDealUser = comquery.FirstDealUser;// Request["HandleUser"].ToString().Trim();
                string Tel = comquery.Tel;// Request["Tel"].ToString().Trim();
                string Adderss = comquery.Adderss;// Request["Adderss"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["Tel"] != "")//电话
                    where += " a.Tel like '%" + Request["Tel"] + "%' and";
                if (Request["Adderss"] != "")//地址
                    where += " a.Adderss like '%" + Request["Adderss"] + "%' and";
                if (Request["PID"].ToString().Trim() != "")//产品编号
                    where += " a.TSID in (select TSID from BGOI_CustomerService.dbo.tk_SHComplain_Product where PID like '%" + Request["PID"].ToString().Trim() + " %') and";
                if (Request["OrderContent"].ToString().Trim() != "")//产品名称
                    where += " a.TSID in (select TSID from BGOI_CustomerService.dbo.tk_SHComplain_Product where OrderContent like '%" + Request["OrderContent"].ToString().Trim() + " %') and";
                if (Request["FirstDealUser"] != "")//首次处理人
                    where += " a.FirstDealUser  like '%" + Request["FirstDealUser"] + "%' and";
                if (Request["UserName"] != "")//投诉人员
                    where += " c.UserName  like '%" + Request["UserName"] + "%' and";
                if (Begin != "" && End != "")
                    where += " a.RecordDate between '" + Begin + "' and '" + End + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.UserComplaintsList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult AddUserComplaints()
        {
            tk_SHComplain so = new TECOCITY_BGOI.tk_SHComplain();
            so.TSID = CustomerServiceMan.GetTopTSID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            DateTime date = DateTime.Now;
            so.ComplaintDate = date.Hour.ToString() + ":" + date.Minute.ToString() + ":" + date.Second.ToString();
            return View(so);
        }
        public ActionResult GetUserName()
        {
            string DeptId = Request["DeptId"].ToString();
            DataTable dt = CustomerServiceMan.GetUserName(DeptId);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加2       //修改1
        public ActionResult SaveUserComplaints()
        {
            if (ModelState.IsValid)
            {
                string type = Request["Type"].ToString();
                #region [用户投诉]
                tk_SHComplain allr = new tk_SHComplain();
                allr.TSID = Request["TSID"].ToString();
                allr.UntiID = Request["UntiID"].ToString();
                allr.CustomerID = Request["CustomerID"].ToString();
                allr.Customer = Request["Customer"].ToString();
                allr.RecordDate = Convert.ToDateTime(Request["RecordDate"]);
                allr.ComplaintDate = Request["ComplaintDate"].ToString();
                allr.EmergencyDegree = Request["EmergencyDegree"].ToString();
                allr.ComplaintTheme = Request["ComplaintTheme"].ToString();
                allr.ComplaintCategory = Request["ComplaintCategory"].ToString();
                allr.FirstDealUser = Request["FirstDealUser"].ToString();
                allr.ComplainContent = Request["ComplainContent"].ToString();

                allr.Tel = Request["Tel"].ToString().Trim();
                allr.Adderss = Request["Adderss"].ToString().Trim();

                allr.State = "0";
                allr.Remark = Request["Remark"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.Validate = "v";
                #endregion
                #region [用户投诉人员]
                tk_SHComplain_User allruser = new tk_SHComplain_User();
                allruser.TSID = Request["TSID"].ToString();
                allruser.DID = CustomerServiceMan.GetTopTSIDDID();
                allruser.UserID = Request["CustomerID"].ToString();
                allruser.UserUnitID = Request["UntiID"].ToString();
                allruser.UserName = Request["UserName"].ToString();
                #endregion
                #region [用户处理]
                tk_SHComplain_Process allpro = new tk_SHComplain_Process();
                allpro.TSID = Request["TSID"].ToString(); ;
                allpro.CLID = CustomerServiceMan.GetTopCLIDDID();
                allpro.HandleProcess = Request["HandleProcess"].ToString();
                allpro.HandleState = Request["HandleState"].ToString();
                allpro.HandleDate = Convert.ToDateTime(Request["HandleDate"]);
                // allpro.HandleUser = Request["HandleUser"].ToString();
                allpro.CustomerFeedback = Request["CustomerFeedback"].ToString();
                allpro.HandleUser = Request["HandleUser"].ToString();
                allpro.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allpro.Validate = "v";
                allpro.CreateUser = Request["CreateUser"].ToString();
                allpro.State = "0";
                #endregion
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] OrderID = Request["OrderID"].Split(',');
                List<tk_SHComplain_Product> list = new List<tk_SHComplain_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHComplain_Product allproduct = new tk_SHComplain_Product();
                    allproduct.TSID = allr.TSID;
                    allproduct.DID = CustomerServiceMan.GetTopProductDID();
                    allproduct.PID = ProductID[i];
                    allproduct.OrderContent = MainContent[i];
                    allproduct.Unit = Unit[i];
                    allproduct.SpecsModels = SpecsModels[i];
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allproduct.Num = 0;
                    }
                    else
                    {
                        allproduct.Num = Convert.ToInt32(Amount[i]);
                    }
                    allproduct.Validate = "v";
                    allproduct.OrderID = OrderID[i];
                    list.Add(allproduct);
                }
                string strErr = "";

                if (type == "1")//修改
                {
                    bool b = CustomerServiceMan.SaveUserComplaints(allr, allruser, allpro, list, type, ref strErr);
                    if (b)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//添加
                {
                    bool b = CustomerServiceMan.SaveUserComplaints(allr, allruser, allpro, list, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加用户投诉";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHComplain";
                        log.Typeid = Request["TSID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加用户投诉";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHComplain";
                        log.Typeid = Request["TSID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //导出
        public FileResult UserComplaintsToExcel()
        {
            string where = " a.Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PID = Request["PID"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string UserName = Request["UserName"].ToString().Trim();
            string FirstDealUser = Request["FirstDealUser"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Adderss = Request["Adderss"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Tel != "")//电话
                where += " a.Tel like '%" + Tel + "%' and";
            if (Adderss != "")//地址
                where += " a.Adderss like '%" + Adderss + "%' and";
            if (PID != "")//产品编号
                where += " a.TSID in (select TSID from BGOI_CustomerService.dbo.tk_SHComplain_Product where PID like '%" + PID + " %') and";
            if (OrderContent != "")//产品名称
                where += " a.TSID in (select TSID from BGOI_CustomerService.dbo.tk_SHComplain_Product where OrderContent like '%" + OrderContent + " %') and";
            if (FirstDealUser != "")//首次处理人
                where += " a.FirstDealUser  like '%" + FirstDealUser + "%' and";
            if (UserName != "")//投诉人员
                where += " c.UserName  like '%" + UserName + "%' and";
            if (Begin != "" && End != "")
                where += " a.RecordDate between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHComplain a " +
                              " left join BGOI_CustomerService.dbo.tk_SHComplain_User c on a.TSID=c.TSID " +
                              " left join BGOI_CustomerService.dbo.tk_SHComplain_Process d on a.TSID=d.TSID ";
            string FieldName = " a.TSID, a.UntiID, a.CustomerID, a.Customer,a.Tel,a.Adderss, a.RecordDate, a.ComplaintDate, a.EmergencyDegree, a.ComplaintTheme, a.ComplaintCategory, a.FirstDealUser, a.ComplainContent, (case when a.State=0 then '未处理' else '已处理' end)  as State, a.Remark, a.CreateTime,a.CreateUser ";
            string OrderBy = " a.CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "投诉编号-6000,投诉处理单位-6000,客户编号-6000,客户名称-5000,电话-6000,地址-6000,投诉日期-6000,投诉时间-5000,紧急程度-3000,";
                strCols += "投诉主题-3000,投诉类别-6000,首次处理人-5000,投诉内容-5000,状态-5000,备注-5000,创建时间-3000,登记人-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "用户投诉信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "用户投诉信息表.xls");
            }
            else
                return null;

        }
        //详细页面 处理记录
        public ActionResult UserComplaintsDetialList()
        {
            string where = " a.Validate='v' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string TSID = Request["TSID"].ToString();
            where += " a.TSID='" + TSID + "' and";
            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserComplaintsDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //详细页面 投诉信息
        public ActionResult UserProComDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string TSID = Request["TSID"].ToString();


            UIDataTable udtTask = CustomerServiceMan.UserProComDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, TSID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //撤销
        public ActionResult DeUserComplaints()
        {
            string strErr = "";
            string TSID = Request["TSID"].ToString();

            if (CustomerServiceMan.DeUserComplaints(TSID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销用户投诉";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHComplain";
                log.Typeid = Request["TSID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销用户投诉";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHComplain";
                log.Typeid = Request["TSID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //加载打印数据
        public ActionResult PrintUserComplaints()
        {
            string TSID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(TSID))
            {
                s += " TSID like '%" + TSID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHComplain ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_SHComplain so = new TECOCITY_BGOI.tk_SHComplain();
            foreach (DataRow dt in data.Rows)
            {
                so.TSID = dt["TSID"].ToString();
                so.Customer = dt["Customer"].ToString();//一会处理
                ViewData["RecordDate"] = Convert.ToDateTime(dt["RecordDate"]).ToString("yyyy/MM/dd");
                so.Customer = dt["Customer"].ToString();
                so.FirstDealUser = dt["FirstDealUser"].ToString();
                so.ComplainContent = dt["ComplainContent"].ToString();
            }


            string tableName1 = " BGOI_CustomerService.dbo.tk_SHComplain_User  ";
            DataTable dt1 = CustomerServiceMan.PrintList(" where TSID like '%" + TSID + "%' ", tableName1, ref strErr);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                ViewData["UserUnitID"] = dt1.Rows[0][3].ToString();
                ViewData["UserName"] = dt1.Rows[0][4].ToString();
            }

            string tableName2 = " BGOI_CustomerService.dbo.tk_SHComplain_Process  ";
            DataTable dt2 = CustomerServiceMan.PrintList(where, tableName2, ref strErr);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                ViewData["HandleState"] = dt2.Rows[0][3].ToString();
            }

            return View(so);
        }
        //打印加载产品信息
        public ActionResult GetUserComplaintsList()
        {
            string TSID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(TSID))
            {
                s += " TSID like '%" + TSID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHComplain_Product  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //打印加载投诉人员信息
        public ActionResult GetUserComplaintsNameList()
        {
            string TSID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(TSID))
            {
                s += " TSID like '%" + TSID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHComplain_User  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //打印加载投诉状态
        public ActionResult GetUserComplaintsStateList()
        {
            string TSID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(TSID))
            {
                s += " TSID like '%" + TSID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHComplain_Process  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult AddProcessingRecord()
        {
            tk_SHComplain_Process so = new TECOCITY_BGOI.tk_SHComplain_Process();
            //  so.TSID = CustomerServiceMan.GetTopTSID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["TSID"] = Request["TSID"].ToString();
            return View(so);
        }
        //添加2
        public ActionResult SaveProcessingRecord()
        {
            if (ModelState.IsValid)
            {
                #region [用户处理]
                tk_SHComplain_Process allpro = new tk_SHComplain_Process();
                allpro.TSID = Request["TSID"].ToString(); ;
                allpro.CLID = CustomerServiceMan.GetTopCLIDDID();
                allpro.HandleProcess = Request["HandleProcess"].ToString();
                allpro.HandleState = Request["HandleState"].ToString();
                allpro.HandleDate = Convert.ToDateTime(Request["HandleDate"]);
                //allpro.CostDate = Request["CostDate"].ToString();
                allpro.CustomerFeedback = Request["CustomerFeedback"].ToString();
                allpro.HandleUser = Request["HandleUser"].ToString();
                allpro.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allpro.Validate = "v";
                allpro.CreateUser = Request["CreateUser"].ToString();
                allpro.State = "0";
                #endregion
                string strErr = "";
                bool b = CustomerServiceMan.SaveProcessingRecord(allpro, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加用户投诉处理";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHComplain_Process";
                    log.Typeid = Request["TSID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加用户投诉处理";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHComplain_Process";
                    log.Typeid = Request["TSID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //添加处理记录
        public ActionResult UpAddProcessingRecord()
        {
            string TSID = Request["TSID"].ToString();
            DataTable dt = CustomerServiceMan.UpAddProcessingRecord(TSID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult ModifyUserComplaints()
        {
            tk_SHComplain so = new TECOCITY_BGOI.tk_SHComplain();
            so.TSID = Request["TSID"].ToString();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["TSID"] = Request["TSID"].ToString();
            return View(so);
        }
        //加载修改用户投诉表
        public ActionResult UpModifyUserComplaintsList()
        {
            string TSID = Request["TSID"].ToString();
            DataTable dt = CustomerServiceMan.UpModifyUserComplaintsList(TSID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //加载修改用户投诉表(产品)
        public ActionResult UpModifyUserComplaintsListPro()
        {
            string TSID = Request["TSID"].ToString();
            DataTable dt = CustomerServiceMan.UpModifyUserComplaintsListPro(TSID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据客户加载报修
        public ActionResult GetBX()
        {


            string Customer = Request["Customer"].ToString();
            DataTable dt = CustomerServiceMan.GetBX(Customer);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据客户加载报装
        public ActionResult GetBZ()
        {
            string Customer = Request["Customer"].ToString();
            DataTable dt = CustomerServiceMan.GetBZ(Customer);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        // 修改删除产品
        public ActionResult DeUserCom()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();
            string TSID = Request["TSID"].ToString();

            if (CustomerServiceMan.DeUserCom(TSID, PID))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = " 删除产品";
                log.LogContent = "删除成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHComplain_Product";
                log.Typeid = Request["TSID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "删除产品";
                log.LogContent = "删除失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHComplain_Product";
                log.Typeid = Request["TSID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion
        #region [电话记录]
        public ActionResult TelephoneAnswering()
        {
            return View();
        }
        //加载电话记录列表
        public ActionResult TelephoneAnsweringList(TelephoneAnsweringQuery waquery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string UserName = waquery.UserName;// Request["UserName"].ToString().Trim();
                string Address = waquery.Address;// Request["Address"].ToString().Trim();
                string Tel = waquery.Tel;// Request["Tel"].ToString().Trim();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                //string State = Request["State"].ToString();
                if (Request["UserName"].ToString().Trim() != "")
                    where += " UserName like '%" + Request["UserName"].ToString().Trim() + "%' and";
                if (Request["Address"].ToString().Trim() != "")
                    where += " Address like '%" + Request["Address"].ToString().Trim() + "%' and";
                if (Request["Tel"].ToString().Trim() != "")
                    where += " Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " AnswerDate between '" + Begin + "' and '" + End + "' and";
                //if (State != "")
                //    where += " State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = CustomerServiceMan.TelephoneAnsweringList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //添加电话记录
        public ActionResult AddTelephoneAnswering()
        {
            tk_TelRecord so = new TECOCITY_BGOI.tk_TelRecord();
            so.DHID = CustomerServiceMan.GetTopDHID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //保存电话记录
        public ActionResult SaveTelephoneAnswering()
        {
            if (ModelState.IsValid)
            {
                string type = Request["type"].ToString();
                string strErr = "";
                tk_TelRecord card = new tk_TelRecord();
                card.DHID = Request["DHID"].ToString();
                card.SchoolWork = Request["SchoolWork"].ToString();
                card.AnswerDate = Convert.ToDateTime(Request["AnswerDate"].ToString());
                card.UserName = Request["UserName"].ToString();
                card.Tel = Request["Tel"].ToString();
                card.Address = Request["Address"].ToString();
                card.ProcessingResults = Request["ProcessingResults"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SaveTelephoneAnswering(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加电话记录";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_TelRecord";
                        log.Typeid = Request["DHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加电话记录";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_TelRecord";
                        log.Typeid = Request["DHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SaveTelephoneAnswering(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改电话记录";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_TelRecord";
                        log.Typeid = Request["DHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改电话记录";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_TelRecord";
                        log.Typeid = Request["DHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //导出
        public FileResult TelephoneAnsweringToExcelFZ()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string UserName = Request["UserName"].ToString().Trim();
            string Address = Request["Address"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (UserName != "")
                where += " UserName like '%" + UserName + "%' and";
            if (Address != "")
                where += " Address like '%" + Address + "%' and";
            if (Tel != "")
                where += " Tel like '%" + Tel + "%' and";
            if (Begin != "" && End != "")
                where += " AnswerDate between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_TelRecord ";
            string FieldName = " DHID, Convert(varchar(100),AnswerDate,23) as AnswerDate, Address, UserName, Tel, SchoolWork, ProcessingResults, Remark,CreateUser ";
            string OrderBy = " CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                //DHID, AnswerDate, Address, UserName, Tel, SchoolWork, ProcessingResults, Remark,
                string strCols = "电话编号-6000,接听日期-6000,地址内容-6000,联系人-5000,联系电话-6000,派工单号-5000,处理结果-5000,备注-5000,记录人-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "电话接听任务记录单", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "电话接听任务记录信息表.xls");
            }
            else
                return null;
        }
        //修改电话记录页面
        public ActionResult UpTelephoneAnswering()
        {
            tk_TelRecord so = new TECOCITY_BGOI.tk_TelRecord();
            so.DHID = Request["DHID"].ToString();//CustomerServiceMan.GetTopBXKID();
            DataTable dt = CustomerServiceMan.UpTelephoneAnswering(so.DHID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    so.AnswerDate = Convert.ToDateTime(dr["AnswerDate"]);
                    so.Address = dr["Address"].ToString();
                    so.UserName = dr["UserName"].ToString();
                    so.Tel = dr["Tel"].ToString();
                    so.SchoolWork = dr["SchoolWork"].ToString();
                    so.ProcessingResults = dr["ProcessingResults"].ToString();
                    so.Remark = dr["Remark"].ToString();
                }
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //加载修改电话记录的接听时间
        public ActionResult UpTelDate()
        {
            string DHID = Request["DHID"].ToString();
            DataTable dt = CustomerServiceMan.UpTelephoneAnswering(DHID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //撤销
        public ActionResult DeTelephoneAnswering()
        {
            string strErr = "";
            string DHID = Request["DHID"].ToString();

            if (CustomerServiceMan.DeTelephoneAnswering(DHID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销电话记录";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_TelRecord";
                log.Typeid = Request["DHID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销电话记录";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_TelRecord";
                log.Typeid = Request["DHID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion
        #endregion
        #region [产品报装]
        #region [产品报装]
        public ActionResult ProductReport()
        {
            return View();
        }
        public ActionResult AddProductReport()
        {
            tk_SHInstallR so = new TECOCITY_BGOI.tk_SHInstallR();
            so.BZID = CustomerServiceMan.GetTopBZID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            ViewData["InstallTime"] = DateTime.Now;
            return View(so);
        }
        public ActionResult GetBasicDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = CustomerServiceMan.GetBasicDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据产品加载保修卡
        public ActionResult GetPIDBasicDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = CustomerServiceMan.GetPIDBasicDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加
        public ActionResult SaveProductReport()
        {
            if (ModelState.IsValid)
            {
                string type = Request["Type"].ToString();
                tk_SHInstallR allr = new tk_SHInstallR();
                allr.BZID = Request["BZID"].ToString();
                allr.WarehouseTwo = Request["WarehouseTwo"].ToString();
                allr.CustomerName = Request["CustomerName"].ToString();
                allr.IsWhether = Request["IsWhether"].ToString();
                allr.Tel = Request["Tel"].ToString();
                allr.Tel = Request["Tel"].ToString();
                allr.Address = Request["Address"].ToString();
                allr.InstallTime = Convert.ToDateTime(Request["InstallTime"]);
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.RelationID = Request["TRID"].ToString().Trim();
                if (Request["IsWhether"].ToString() == "0")
                {
                    allr.WarehouseOne = Request["WarehouseOne"].ToString();
                }
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.UntiID = Request["UnitID"].ToString();
                allr.Sate = "0";
                allr.Validate = "v";
                allr.BZCompany = Request["BZCompany"].ToString();
                allr.DiPer = Request["DiPer"].ToString();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] SalesChannel = Request["SalesChannel"].Split(',');
                string[] IsPendingCollection = Request["IsPendingCollection"].Split(',');
                List<tk_SHInstallR_Product> list = new List<tk_SHInstallR_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHInstallR_Product allpro = new tk_SHInstallR_Product();
                    allpro.BZID = allr.BZID;
                    allpro.DID = CustomerServiceMan.GetTopDID();
                    allpro.State = "0";
                    allpro.Validate = "v";
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    allpro.Price = Total[i];
                    allpro.UnitPrice = UnitPrice[i];
                    allpro.IsPendingCollection = IsPendingCollection[i];
                    allpro.SalesChannel = SalesChannel[i];
                    list.Add(allpro);
                }
                string strErr = "";
                if (type == "1")//修改
                {
                    bool b = CustomerServiceMan.AddProductReport(allr, list, type, ref strErr);
                    if (b)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//添加
                {
                    bool b = CustomerServiceMan.AddProductReport(allr, list, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加产品报装";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHInstallR";
                        log.Typeid = Request["BZID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加产品报装";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_SHInstallR";
                        log.Typeid = Request["BZID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //产品报装列表页面
        public ActionResult ProductReportList(ProductReportQuery requery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v'  and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PID = requery.PID;// Request["BZID"].ToString().Trim();
                string SpecsModels = requery.SpecsModels;// Request["SpecsModels"].ToString().Trim();
                string OrderContent = requery.OrderContent;// Request["OrderContent"].ToString().Trim();
                string CustomerName = requery.CustomerName;// Request["CustomerName"].ToString().Trim();
                string Tel = requery.Tel;// Request["Tel"].ToString().Trim();
                string Address = requery.Address;// Request["Address"].ToString().Trim();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";

                if (Request["CustomerName"] != "")//
                    where += " a.CustomerName like '%" + Request["CustomerName"] + "%' and";
                if (Request["Tel"] != "")//
                    where += " a.Tel like  '%" + Request["Tel"] + "%' and";
                if (Request["Address"] != "")//
                    where += " a.Address like  '%" + Request["Address"] + "%' and";
                //string State = Request["State"].ToString();
                if (Request["PID"] != "")//产品编号
                    where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where PID like '%" + Request["PID"] + "%') and";
                if (Request["SpecsModels"] != "")//产品规格型号
                    where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where SpecsModels like  '%" + Request["SpecsModels"] + "%') and";
                if (Request["OrderContent"] != "")//产品名称
                    where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where OrderContent like  '%" + Request["OrderContent"] + "%') and";
                if (Begin != "" && End != "")
                    where += " a.InstallTime between '" + Begin + "' and '" + End + "' and";
                //if (State != "")
                //    where += " a.State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.ProductReportList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //产品报装详细
        public ActionResult ProductReportDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string BZID = Request["BZID"].ToString();
            UIDataTable udtTask = CustomerServiceMan.ProductReportDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, BZID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //导出
        public FileResult ProductReportToExcel()
        {
            string where = " a.Validate='v'  and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PID = Request["PID"].ToString().Trim();
            string SpecsModels = Request["SpecsModels"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string CustomerName = Request["CustomerName"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Address = Request["Address"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";

            if (Request["CustomerName"] != "")//
                where += " a.CustomerName like '%" + Request["CustomerName"] + "%' and";
            if (Request["Tel"] != "")//
                where += " a.Tel like  '%" + Request["Tel"] + "%' and";
            if (Request["Address"] != "")//
                where += " a.Address like  '%" + Request["Address"] + "%' and";
            if (Request["PID"] != "")//产品编号
                where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where PID like '%" + Request["PID"] + "%') and";
            if (Request["SpecsModels"] != "")//产品规格型号
                where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where SpecsModels like  '%" + Request["SpecsModels"] + "%') and";
            if (Request["OrderContent"] != "")//产品名称
                where += " a.BZID in(select BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where OrderContent like  '%" + Request["OrderContent"] + "%') and";
            if (Begin != "" && End != "")
                where += " a.InstallTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHInstallR a " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse c on a.WarehouseTwo=c.HouseID  " +
                               " left join BGOI_CustomerService.dbo.tk_Customerser_Config d on a.BZCompany=d.ID " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse e on  a.WarehouseOne=e.HouseID  ";
            string FieldName = " a.BZID,Convert(varchar(100),a.InstallTime,23)as InstallTime,a.CustomerName as CustomerName,a.Tel," +
                " a.Address,c.HouseName as 'ckejk',(case when e.HouseName !='' then e.HouseName else'否' end) as 'yjdbk'," +
                " d.Text,a.Remark,a.CreateUser  ";
            string OrderBy = " a.BZID desc ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "报装编号-6000,报装时间-5000,客户姓名-6000,联系电话-5000,地址-6000,出库二级库房-3000,调拨一级库房-6000,分公司-6000,备注-3000,";
                strCols += "登记人-5000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "产品报装信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "产品报装信息表.xls");
            }
            else
                return null;

        }
        //撤销
        public ActionResult DeProductReport()
        {
            string strErr = "";
            string BZID = Request["BZID"].ToString();

            if (CustomerServiceMan.DeProductReport(BZID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销产品报装";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstallR";
                log.Typeid = Request["BZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销产品报装";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstallR";
                log.Typeid = Request["BZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpProductReport()
        {
            tk_SHInstallR so = new TECOCITY_BGOI.tk_SHInstallR();
            //so.BZID = CustomerServiceMan.GetTopBZID();
            ViewData["BZID"] = Request["BZID"].ToString();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult UpProductReportList()
        {
            if (ModelState.IsValid)
            {
                string BZID = Request["BZID"].ToString();
                DataTable dt = CustomerServiceMan.UPProductReportList(BZID);

                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        public ActionResult UPProductReportListpro()
        {
            if (ModelState.IsValid)
            {
                string BZID = Request["BZID"].ToString();
                DataTable dt = CustomerServiceMan.UPProductReportListpro(BZID);

                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //安装记录
        public ActionResult InstallRecord()
        {
            tk_SHInstall so = new TECOCITY_BGOI.tk_SHInstall();
            so.AZID = CustomerServiceMan.GetTopAZID();
            ViewData["BZID"] = Request["BZID"].ToString();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //添加安装记录
        public ActionResult SaveInstallRecord()
        {
            if (ModelState.IsValid)
            {
                tk_SHInstall allr = new tk_SHInstall();
                allr.BZID = Request["BAIDold"].ToString();
                allr.AZID = Request["AZID"].ToString();
                allr.InstallName = Request["InstallName"].ToString();
                allr.IsCharge = Request["IsCharge"].ToString();
                allr.IsInvoice = Request["IsInvoice"].ToString();
                allr.ReceiptType = Request["ReceiptType"].ToString();
                allr.SureSatisfied = Request["SureSatisfied"].ToString();
                allr.IsProContent = Request["IsProContent"].ToString();
                allr.IsWearClothes = Request["IsWearClothes"].ToString();
                allr.IsTeaching = Request["IsTeaching"].ToString();
                allr.IsGifts = Request["IsGifts"].ToString();
                allr.IsClean = Request["IsClean"].ToString();
                allr.IsUserSign = Request["IsUserSign"].ToString();
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.Validate = "v";
                allr.InstallTime = Convert.ToDateTime(Request["InstallTime"]);
                allr.AZCompany = Request["AZCompany"].ToString();
                allr.OrderID = "";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                //string[] PRemark = Request["PRemark"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                List<tk_SHInstall_Product> list = new List<tk_SHInstall_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHInstall_Product allpro = new tk_SHInstall_Product();
                    allpro.AZID = allr.AZID;
                    allpro.BZDID = CustomerServiceMan.GetProductReportDID(allr.BZID);
                    allpro.DID = CustomerServiceMan.GetTopAZIDDID();

                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    allpro.Total = Convert.ToDecimal(Total[i]);
                    allpro.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    allpro.Validate = "v";
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    list.Add(allpro);
                }
                string strErr = "";
                bool b = CustomerServiceMan.AddSHInstall(allr, list, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加产品安装记录";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstall";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加产品安装记录";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstallR";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //加载打印数据
        public ActionResult PrintProductReport()
        {
            string BZID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(BZID))
            {
                s += " BZID like '%" + BZID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHInstallR ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_SHInstallR so = new TECOCITY_BGOI.tk_SHInstallR();
            foreach (DataRow dt in data.Rows)
            {
                so.CustomerName = dt["CustomerName"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.Address = dt["Address"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = GAccount.GetAccountInfo().UserName;
                so.BZID = dt["BZID"].ToString();
                if (dt["IsWhether"].ToString() == "0")
                {
                    so.IsWhether = "是";
                }
                else
                {
                    so.IsWhether = "否";
                }
                string whereone = " where  HouseID ='" + dt["WarehouseOne"].ToString() + "'";
                string tableName1 = " BGOI_Inventory.dbo.tk_WareHouse ";
                DataTable oneHouse = CustomerServiceMan.PrintList(whereone, tableName1, ref strErr);
                if (oneHouse != null && oneHouse.Rows.Count > 0)
                {
                    foreach (DataRow dr in oneHouse.Rows)
                    {
                        so.WarehouseOne = dr["HouseName"].ToString();
                    }
                }
                string wheretwo = " where HouseID ='" + dt["WarehouseTwo"].ToString() + "'";
                string tableName2 = " BGOI_Inventory.dbo.tk_WareHouse ";
                DataTable twoHouse = CustomerServiceMan.PrintList(wheretwo, tableName2, ref strErr);
                if (twoHouse != null && twoHouse.Rows.Count > 0)
                {
                    foreach (DataRow dr in twoHouse.Rows)
                    {
                        so.WarehouseTwo = dr["HouseName"].ToString();
                    }
                }
                //so.InstallTime = Convert.ToDateTime(dt["InstallTime"]);
                ViewData["InstallTime"] = Convert.ToDateTime(dt["InstallTime"]).ToString("yyy/MM/dd");
            }
            return View(so);
        }
        //打印加载物料信息
        public ActionResult GetProductReport()
        {
            string BZID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(BZID))
            {
                s += " BZID like '%" + BZID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHInstallR_Product ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        // 修改删除产品
        public ActionResult DeProduct()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();
            string BZID = Request["BZID"].ToString();

            if (CustomerServiceMan.DeProduct(BZID, PID))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = " 删除产品";
                log.LogContent = "删除成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstallR_Product";
                log.Typeid = Request["BZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "删除产品";
                log.LogContent = "删除失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstallR_Product";
                log.Typeid = Request["BZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion
        #region [产品安装]
        public ActionResult ProductInstallation()
        {
            return View();
        }
        //产品安装在记录列表页面
        public ActionResult ProductInstallationList(ProductInstallationQuery inquery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v'  and   BZID!=''  and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PID = inquery.PID;// Request["AZID"].ToString().Trim();
                //  string SpecsModels = Request["SpecsModels"].ToString().Trim();
                string OrderContent = inquery.OrderContent;// Request["OrderContent"].ToString().Trim();
                string InstallName = inquery.InstallName;//Request["InstallName"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                //string State = Request["State"].ToString();
                if (Request["InstallName"] != "")
                    where += " InstallName like '%" + Request["InstallName"] + "%' and";
                if (Request["PID"] != "")
                    //where += " b.PID like '%" + Request["PID"] + "%' and";
                    where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where PID like '%" + Request["PID"] + "%' ) and";
                //if (SpecsModels != "")
                //    where += " b.SpecsModels='" + SpecsModels + "' and";
                if (Request["OrderContent"] != "")
                    //where += " b.OrderContent  like '%" + Request["OrderContent"] + "%' and";
                    where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where OrderContent like '%" + Request["OrderContent"] + "%' ) and";
                if (Begin != "" && End != "")
                    where += " InstallTime between '" + Begin + "' and '" + End + "' and";
                //if (State != "")
                //    where += " a.State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.ProductInstallationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeProductInstallation()
        {
            string strErr = "";
            string AZID = Request["AZID"].ToString();

            if (CustomerServiceMan.DeProductInstallation(AZID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销产品安装";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstall";
                log.Typeid = Request["AZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销产品安装";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstall";
                log.Typeid = Request["AZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpProductInstallationList()
        {
            tk_SHInstall so = new TECOCITY_BGOI.tk_SHInstall();
            //so.AZID = CustomerServiceMan.GetTopAZID();
            so.AZID = Request["AZID"].ToString();
            DataTable dt = CustomerServiceMan.UpProductInstallation(so.AZID);
            if (dt != null && dt.Rows.Count > 0)
            {
                so.BZID = dt.Rows[0][10].ToString();
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //修改
        public ActionResult UpProductInstallation()
        {
            string AZIDold = Request["AZID"].ToString();
            DataTable dt = CustomerServiceMan.UpProductInstallation(AZIDold);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //修改
        public ActionResult UpProductInstallationpro()
        {
            string AZIDold = Request["AZID"].ToString();
            DataTable dt = CustomerServiceMan.UpProductInstallationpro(AZIDold);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加安装记录
        public ActionResult SaveProductInstallation()
        {
            if (ModelState.IsValid)
            {
                string AZIDold = Request["AZIDold"].ToString();
                tk_SHInstall allr = new tk_SHInstall();
                allr.BZID = Request["BAIDold"].ToString();
                allr.AZID = Request["AZID"].ToString();
                allr.InstallName = Request["InstallName"].ToString();
                allr.IsCharge = Request["IsCharge"].ToString();
                allr.IsInvoice = Request["IsInvoice"].ToString();
                allr.ReceiptType = Request["ReceiptType"].ToString();
                allr.SureSatisfied = Request["SureSatisfied"].ToString();
                allr.IsProContent = Request["IsProContent"].ToString();
                allr.IsWearClothes = Request["IsWearClothes"].ToString();
                allr.IsTeaching = Request["IsTeaching"].ToString();
                allr.IsGifts = Request["IsGifts"].ToString();
                allr.IsClean = Request["IsClean"].ToString();
                allr.IsUserSign = Request["IsUserSign"].ToString();
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.Validate = "v";
                allr.InstallTime = Convert.ToDateTime(Request["InstallTime"]);
                allr.AZCompany = Request["AZCompany"].ToString();
                allr.OrderID = "";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                //string[] PRemark = Request["PRemark"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                List<tk_SHInstall_Product> list = new List<tk_SHInstall_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHInstall_Product allpro = new tk_SHInstall_Product();
                    allpro.AZID = allr.AZID;
                    allpro.BZDID = CustomerServiceMan.GetProductReportDID(allr.BZID);
                    allpro.DID = CustomerServiceMan.GetTopAZIDDID();

                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    if (Total[i] != "")
                    {
                        allpro.Total = Convert.ToDecimal(Total[i]);

                    }
                    if (UnitPrice[i] != "")
                    {
                        allpro.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    }
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    allpro.Validate = "v";
                    list.Add(allpro);
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveProductInstallation(allr, list, AZIDold, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改产品安装记录";
                    log.LogContent = "修改成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstall";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改产品安装记录";
                    log.LogContent = "修改失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstallR";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //加载打印数据
        public ActionResult PrintProductInstallation()
        {
            string AZID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(AZID))
            {
                s += " AZID like '%" + AZID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHInstall ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_SHInstall so = new TECOCITY_BGOI.tk_SHInstall();
            foreach (DataRow dt in data.Rows)
            {
                so.AZID = dt["AZID"].ToString();
                so.BZID = dt["BZID"].ToString();
                ViewData["InstallTime"] = Convert.ToDateTime(dt["InstallTime"]).ToString("yyy/MM/dd");
                //  so.InstallTime = Convert.ToDateTime(dt["InstallTime"]);
                so.InstallName = dt["InstallName"].ToString();
                so.OrderID = dt["OrderID"].ToString();
                if (dt["IsCharge"].ToString() == "0")
                {
                    so.IsCharge = "是";
                }
                else
                {
                    so.IsCharge = "否";
                }
                if (dt["IsInvoice"].ToString() == "0")
                {
                    so.IsInvoice = "是";
                }
                else
                {
                    so.IsInvoice = "否";
                }
                if (dt["ReceiptType"].ToString() == "0")
                {
                    so.ReceiptType = "发票";
                }
                else
                {
                    so.ReceiptType = "收据";
                }
                if (dt["SureSatisfied"].ToString() == "0")
                {
                    so.SureSatisfied = "非常满意  ";
                }
                else if (dt["SureSatisfied"].ToString() == "1")
                {
                    so.SureSatisfied = "满意";
                }
                else if (dt["SureSatisfied"].ToString() == "2")
                {
                    so.SureSatisfied = "一般";
                }
                else
                {
                    so.SureSatisfied = "不满意";
                }
                if (dt["IsProContent"].ToString() == "0")
                {
                    so.IsProContent = "是";
                }
                else
                {
                    so.IsProContent = "否";
                }
                if (dt["IsWearClothes"].ToString() == "0")
                {
                    so.IsWearClothes = "是";
                }
                else
                {
                    so.IsWearClothes = "否";
                }
                if (dt["IsTeaching"].ToString() == "0")
                {
                    so.IsTeaching = "是";
                }
                else
                {
                    so.IsTeaching = "否";
                }
                if (dt["IsGifts"].ToString() == "0")
                {
                    so.IsGifts = "是";
                }
                else
                {
                    so.IsGifts = "否";
                }
                if (dt["IsClean"].ToString() == "0")
                {
                    so.IsClean = "是";
                }
                else
                {
                    so.IsClean = "否";
                }
                if (dt["IsUserSign"].ToString() == "0")
                {
                    so.IsUserSign = "是";
                }
                else
                {
                    so.IsUserSign = "否";
                }
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = GAccount.GetAccountInfo().UserName;
            }
            return View(so);
        }
        //打印加载物料信息
        public ActionResult GetProductInstallation()
        {
            string AZID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(AZID))
            {
                s += " AZID like '%" + AZID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where  " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_SHInstall_Product  ";
            DataTable dt = CustomerServiceMan.PrintList(where, tableName, ref strErr);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        //产品安装详细
        public ActionResult ProductInstallationDetialList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string AZID = Request["AZID"].ToString();
            UIDataTable udtTask = CustomerServiceMan.ProductInstallationDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, AZID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //导出
        public FileResult ProductInstallationToExcel()
        {
            string where = " Validate='v'  and   BZID!=''  and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PID = Request["PID"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string InstallName = Request["InstallName"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (InstallName != "")
                where += " InstallName like '%" + InstallName + "%' and";
            if (PID != "")
                where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where PID like '%" + PID + "%' ) and";
            if (OrderContent != "")
                where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where OrderContent like '%" + OrderContent + "%' ) and";
            if (Begin != "" && End != "")
                where += " InstallTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHInstall ";
            string FieldName = " AZID, BZID, UntiID, InstallTime, InstallName," +
                                " (case when IsCharge=0 then '是' else '否' end) as IsCharge," +
                                "(case when IsInvoice=0  then '是' else '否' end) as IsInvoice," +
                                "(case when ReceiptType=0 then '发票' else '收据' end) as ReceiptType,Remark, " +
                                "(case when SureSatisfied=0 then '非常满意' when SureSatisfied=1 then '满意' when SureSatisfied=2 then '一般' else '不满意' end) as SureSatisfied," +
                                "(case when IsProContent=0 then '是' else '否' end) as IsProContent, " +
                                "(case when IsWearClothes=0 then '是' else '否' end) as IsWearClothes, " +
                                "(case when IsTeaching=0 then '是' else '否' end) as IsTeaching," +
                                "(case when IsGifts=0 then '是' else '否' end) as IsGifts," +
                                "(case when IsClean=0 then '是' else '否' end) as IsClean," +
                                "(case when IsUserSign=0 then '是' else '否' end) as IsUserSign ";
            string OrderBy = " CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "安装编号-5000,报装编号-5000,安装建立单位-5000,安装时间-5000,安装人员-3000,是否收费-3000,是否开发票收据-3000,";
                strCols += "收据类型-3000,备注-3000,确认客户满意度-3000,是否向用户说明包装内所含物品-3000,";
                strCols += "是否穿工作服-3000,是否指导用户使用及指导事项-3000,是否接收用户赠与的物品-3000,工作完成后是否做好清洁工作-3000,";
                strCols += "客户是否阅读安装单并签字-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "产品安装信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "产品安装信息表.xls");
            }
            else
                return null;

        }

        // 修改删除产品
        public ActionResult DeUserInst()
        {
            string strErr = "";
            string PID = Request["PID"].ToString();
            string AZID = Request["AZID"].ToString();
            if (CustomerServiceMan.DeUserInst(AZID, PID))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = " 删除产品";
                log.LogContent = "删除成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstall_Product";
                log.Typeid = Request["AZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "删除产品";
                log.LogContent = "删除失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_SHInstall_Product";
                log.Typeid = Request["AZID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        #endregion

        #region [家用销售服务]
        public ActionResult HomeSalesInstallation()
        {
            return View();
        }
        //安装记录
        public ActionResult AddHomeSalesInstallation()
        {
            tk_SHInstall so = new TECOCITY_BGOI.tk_SHInstall();
            so.AZID = CustomerServiceMan.GetTopAZID();
            //ViewData["BZID"] = Request["BZID"].ToString();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //添加安装记录
        public ActionResult SaveHomeSalesInstallation()
        {
            if (ModelState.IsValid)
            {
                tk_SHInstall allr = new tk_SHInstall();
                allr.BZID = "";
                allr.AZID = Request["AZID"].ToString();
                allr.InstallName = Request["InstallName"].ToString();
                allr.IsCharge = Request["IsCharge"].ToString();
                allr.IsInvoice = Request["IsInvoice"].ToString();
                allr.ReceiptType = Request["ReceiptType"].ToString();
                allr.SureSatisfied = Request["SureSatisfied"].ToString();
                allr.IsProContent = Request["IsProContent"].ToString();
                allr.IsWearClothes = Request["IsWearClothes"].ToString();
                allr.IsTeaching = Request["IsTeaching"].ToString();
                allr.IsGifts = Request["IsGifts"].ToString();
                allr.IsClean = Request["IsClean"].ToString();
                allr.IsUserSign = Request["IsUserSign"].ToString();
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.Validate = "v";
                allr.InstallTime = Convert.ToDateTime(Request["InstallTime"]);
                allr.AZCompany = Request["AZCompany"].ToString();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                //string[] PRemark = Request["PRemark"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] OrderID = Request["OrderID"].Split(',');
                allr.OrderID = OrderID[0].ToString();
                List<tk_SHInstall_Product> list = new List<tk_SHInstall_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHInstall_Product allpro = new tk_SHInstall_Product();
                    allpro.AZID = allr.AZID;
                    allpro.BZDID = CustomerServiceMan.GetProductReportDID(allr.BZID);
                    allpro.DID = CustomerServiceMan.GetTopAZIDDID();

                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    allpro.Total = Convert.ToDecimal(Total[i]);
                    allpro.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    allpro.Validate = "v";
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    list.Add(allpro);
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveHomeSalesInstallation(allr, list, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加产品安装记录";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstall";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    #region [添加报警表]
                    CSAlarm csal = new CSAlarm();
                    csal.OrderID = allr.OrderID;
                    csal.OperationTime = DateTime.Now.ToString();
                    csal.Operator = Request["CreateUser"].ToString();
                    csal.OperationContent = "签收订单";
                    CustomerServiceMan.AddCSAlarm(csal);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加产品安装记录";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstallR";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult ChangeHomeSalesInstallation()
        {
            return View();
        }
        public ActionResult ChangeHomeSalesInstallationList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            UIDataTable udtTask = CustomerServiceMan.ChangeHomeSalesInstallationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetHomeSalesInstallation()
        {
            string DID = Request["did"].ToString();
            DataTable dt = CustomerServiceMan.GetHomeSalesInstallation(DID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //产品安装在记录列表页面
        public ActionResult HomeSalesInstallationList(ProductInstallationQuery inquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v'  and a.OrderID=b.OrderID and b.AfterSaleState=c.id and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PID = inquery.PID;// Request["AZID"].ToString().Trim();
                string OrderContent = inquery.OrderContent;// Request["OrderContent"].ToString().Trim();
                string InstallName = inquery.InstallName;
                string AfterSaleState = Request["AfterSaleState"].ToString();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["InstallName"] != "")
                    where += " a.InstallName like '%" + Request["InstallName"] + "%' and";
                if (Request["PID"] != "")
                    where += " a.AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where PID like '%" + Request["PID"] + "%' ) and";
                if (Request["OrderContent"] != "")
                    where += " a.AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where OrderContent like '%" + Request["OrderContent"] + "%' ) and";
                if (Begin != "" && End != "")
                    where += " InstallTime between '" + Begin + "' and '" + End + "' and";
                if (AfterSaleState == "2" || AfterSaleState == "3")
                {
                    where += " b.AfterSaleState='" + AfterSaleState + "' and b.LibraryTubeState='2' and";
                }
                else if (AfterSaleState == "4")
                {
                    where += " b.AfterSaleState='" + AfterSaleState + "' and b.LibraryTubeState='4' and";
                }
                else
                {
                    where += " b.AfterSaleState='" + AfterSaleState + "' and";
                }
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.HomeSalesInstallationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //安排安装
        public ActionResult ButDE()
        {
            string strErr = "";
            string OrderID = Request["OrderID"].ToString();

            if (CustomerServiceMan.ButDE(OrderID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销产品安装";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                #region [添加报警表]
                CSAlarm csal = new CSAlarm();
                csal.OrderID = Request["OrderID"].ToString();
                csal.OperationTime = DateTime.Now.ToString();
                csal.Operator = Request["CreateUser"].ToString();
                csal.OperationContent = "撤销安装";
                CustomerServiceMan.AddCSAlarm(csal);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销安装";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //安排安装
        public ActionResult ButAPAZ()
        {
            string strErr = "";
            string OrderID = Request["OrderID"].ToString();

            if (CustomerServiceMan.ButAPAZ(OrderID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "安排产品安装";
                log.LogContent = "安排成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                #region [添加报警表]
                CSAlarm csal = new CSAlarm();
                csal.OrderID = Request["OrderID"].ToString();
                csal.OperationTime = DateTime.Now.ToString();
                csal.Operator = Request["CreateUser"].ToString();
                csal.OperationContent = "已安排安装";
                CustomerServiceMan.AddCSAlarm(csal);
                #endregion
                return Json(new { success = "true", Msg = "安排成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "安排产品安装";
                log.LogContent = "安排失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //回款
        public ActionResult ButHK()
        {
            string strErr = "";
            string OrderID = Request["OrderID"].ToString();

            if (CustomerServiceMan.ButHK(OrderID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "回款";
                log.LogContent = "回款成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                #region [添加报警表]
                CSAlarm csal = new CSAlarm();
                csal.OrderID = Request["OrderID"].ToString();
                csal.OperationTime = DateTime.Now.ToString();
                csal.Operator = Request["CreateUser"].ToString();
                csal.OperationContent = "回款";
                CustomerServiceMan.AddCSAlarm(csal);
                #endregion
                return Json(new { success = "true", Msg = "回款成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "回款";
                log.LogContent = "回款失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //完成
        public ActionResult ButWCAZ()
        {
            string strErr = "";
            string OrderID = Request["OrderID"].ToString();

            if (CustomerServiceMan.ButWCAZ(OrderID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "完成";
                log.LogContent = "完成成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                #region [添加报警表]
                CSAlarm csal = new CSAlarm();
                csal.OrderID = Request["OrderID"].ToString();
                csal.OperationTime = DateTime.Now.ToString();
                csal.Operator = Request["CreateUser"].ToString();
                csal.OperationContent = "完成";
                CustomerServiceMan.AddCSAlarm(csal);
                #endregion
                return Json(new { success = "true", Msg = "完成成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "完成";
                log.LogContent = "完成失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "OrdersInfo";
                log.Typeid = Request["OrderID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpHomeSalesInstallationList()
        {
            tk_SHInstall so = new TECOCITY_BGOI.tk_SHInstall();
            //so.AZID = CustomerServiceMan.GetTopAZID();
            so.AZID = Request["AZID"].ToString();
            DataTable dt = CustomerServiceMan.UpProductInstallation(so.AZID);
            if (dt != null && dt.Rows.Count > 0)
            {
                so.BZID = dt.Rows[0][10].ToString();
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //修改加载产品信息
        public ActionResult UpHomeSalesInstallation()
        {
            string AZIDold = Request["AZID"].ToString();
            DataTable dt = CustomerServiceMan.UpHomeSalesInstallation(AZIDold);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //保存
        public ActionResult SaveUpHomeSalesInstallation()
        {
            if (ModelState.IsValid)
            {
                string AZIDold = Request["AZID"].ToString();
                tk_SHInstall allr = new tk_SHInstall();
                allr.BZID = "";
                allr.AZID = Request["AZID"].ToString();
                allr.InstallName = Request["InstallName"].ToString();
                allr.IsCharge = Request["IsCharge"].ToString();
                allr.IsInvoice = Request["IsInvoice"].ToString();
                allr.ReceiptType = Request["ReceiptType"].ToString();
                allr.SureSatisfied = Request["SureSatisfied"].ToString();
                allr.IsProContent = Request["IsProContent"].ToString();
                allr.IsWearClothes = Request["IsWearClothes"].ToString();
                allr.IsTeaching = Request["IsTeaching"].ToString();
                allr.IsGifts = Request["IsGifts"].ToString();
                allr.IsClean = Request["IsClean"].ToString();
                allr.IsUserSign = Request["IsUserSign"].ToString();
                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.Validate = "v";
                allr.InstallTime = Convert.ToDateTime(Request["InstallTime"]);
                allr.AZCompany = Request["AZCompany"].ToString();
                string[] OrderID = Request["OrderID"].Split(',');
                allr.OrderID = OrderID[0].ToString();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                //string[] PRemark = Request["PRemark"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                List<tk_SHInstall_Product> list = new List<tk_SHInstall_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_SHInstall_Product allpro = new tk_SHInstall_Product();
                    allpro.AZID = allr.AZID;
                    allpro.BZDID = CustomerServiceMan.GetProductReportDID(allr.BZID);
                    allpro.DID = CustomerServiceMan.GetTopAZIDDID();

                    allpro.PID = ProductID[i];
                    allpro.OrderContent = MainContent[i];
                    allpro.Unit = Unit[i];
                    allpro.SpecsModels = SpecsModels[i];
                    if (Total[i] != "")
                    {
                        allpro.Total = Convert.ToDecimal(Total[i]);

                    }
                    if (UnitPrice[i] != "")
                    {
                        allpro.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    }
                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allpro.Num = 0;
                    }
                    else
                    {
                        allpro.Num = Convert.ToInt32(Amount[i]);
                    }
                    allpro.Validate = "v";
                    list.Add(allpro);
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveUpHomeSalesInstallation(allr, list, AZIDold, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改产品安装记录";
                    log.LogContent = "修改成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstall";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改产品安装记录";
                    log.LogContent = "修改失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_SHInstallR";
                    log.Typeid = Request["AZID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //提示报警
        public ActionResult GetOrderidNew()
        {
            DataTable dt = CustomerServiceMan.GetOrderidNew();

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        //导出
        public FileResult HomeSalesInstallationToExcel()
        {
            string where = " a.Validate='v'  and a.OrderID=b.OrderID and b.AfterSaleState=c.id and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PID = Request["PID"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string AfterSaleState = Request["AfterSaleState"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Request["PID"] != "")
                where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where PID like '%" + Request["PID"] + "%' ) and";
            if (Request["OrderContent"] != "")
                where += " AZID in (select AZID from BGOI_CustomerService.dbo.tk_SHInstall_Product where OrderContent like '%" + Request["OrderContent"] + "%' ) and";
            if (Begin != "" && End != "")
                where += " InstallTime between '" + Begin + "' and '" + End + "' and";
            if (AfterSaleState == "2" || AfterSaleState == "3")
            {
                where += " b.AfterSaleState='" + AfterSaleState + "' and b.LibraryTubeState='2' and";
            }
            else if (AfterSaleState == "4")
            {
                where += " b.AfterSaleState='" + AfterSaleState + "' and b.LibraryTubeState='4' and";
            }
            else
            {
                where += " b.AfterSaleState='" + AfterSaleState + "' and";
            }
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string FieldName = "";
            string tableName = " BGOI_CustomerService.dbo.tk_SHInstall a,[BGOI_Sales].[dbo].OrdersInfo b ,BGOI_CustomerService.dbo.Infotall c  ";
            FieldName = "  a.OrderID,AZID,InstallTime,InstallName, " +
                                " (case when IsCharge=0 then '是' else '否' end) as IsCharge," +
                                "(case when IsInvoice=0  then '是' else '否' end) as IsInvoice," +
                                "(case when ReceiptType=0 then '发票' else '收据' end) as ReceiptType, " +
                                "(case when SureSatisfied=0 then '非常满意' when SureSatisfied=1 then '满意' when SureSatisfied=2 then '一般' else '不满意' end) as SureSatisfied," +
                                "(case when IsProContent=0 then '是' else '否' end) as IsProContent, " +
                                "(case when IsWearClothes=0 then '是' else '否' end) as IsWearClothes, " +
                                "(case when IsTeaching=0 then '是' else '否' end) as IsTeaching," +
                                "(case when IsGifts=0 then '是' else '否' end) as IsGifts," +
                                "(case when IsClean=0 then '是' else '否' end) as IsClean," +
                                "(case when IsUserSign=0 then '是' else '否' end) as IsUserSign,a.Remark,a.CreateUser,c.Text as AfterSaleState ";
            string OrderBy = " a.CreateTime desc  ";
            DataTable data = InventoryMan.ToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "订单编号-6000,安装编号-6000,安装时间-6000,安装人员-5000,是否收费-6000,是否开票-5000,开票类型-3000,";
                strCols += "客户满意度-3000,是否向用户说明包装内所含物品-6000,是否穿工作服-5000,是否指导用户使用及指导事项-5000,";
                strCols += "是否接收用户赠与的物品-3000,工作完成后是否做好清洁工作-6000,客户是否阅读安装单并签字-5000,备注-5000,";
                strCols += "记录人-3000,状态-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "产品安装信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "产品安装信息表.xls");
            }
            else
                return null;

        }
        #endregion
        #endregion
        #region [维修任务]
        #region [维修任务]
        public ActionResult MaintenanceTask()
        {
            return View();
        }
        //维修任务列表页面
        public ActionResult MaintenanceTaskList(MaintenanceTaskQuery taskquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string DeviceID = taskquery.DeviceID;// Request["BXID"].ToString().Trim();
                string OrderContent = Request["OrderContent"].ToString().Trim();
                string UserName = Request["UserName"].ToString().Trim();
                string Sate = Request["Sate"].ToString().Trim();

                string RepairDateBegin = Request["RepairDateBegin"].ToString();
                if (RepairDateBegin != "")
                    RepairDateBegin += " 00:00:00";
                string RepairDateEnd = Request["RepairDateEnd"].ToString();
                if (RepairDateEnd != "")
                    RepairDateEnd += " 23:59:59";

                string MaintenanceTimeBegin = Request["MaintenanceTimeBegin"].ToString();
                if (MaintenanceTimeBegin != "")
                    MaintenanceTimeBegin += " 00:00:00";
                string MaintenanceTimeEnd = Request["MaintenanceTimeEnd"].ToString();
                if (MaintenanceTimeEnd != "")
                    MaintenanceTimeEnd += " 23:59:59";

                if (RepairDateBegin != "" && RepairDateEnd != "")
                    where += " a.RepairDate between '" + RepairDateBegin + "' and '" + RepairDateEnd + "' and";
                if (MaintenanceTimeBegin != "" && MaintenanceTimeEnd != "")
                    where += " a.WXID in( select WXID from BGOI_CustomerService.dbo.tk_WXRecord where MaintenanceTime between '" + MaintenanceTimeBegin + "' and '" + MaintenanceTimeEnd + "' )and";

                //if (Request["DeviceID"] != "")
                //    where += " a.DeviceID like '%" + Request["DeviceID"] + "%' and";
                if (OrderContent != "")
                    where += " a.DeviceName like '%" + OrderContent + "%' and";
                if (Sate != "")
                    where += " a.Sate='" + Sate + "' and";
                //if (UserName != "")
                //    where += " DeviceType  like '%" + UserName + "%' and";

                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.MaintenanceTaskList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public ActionResult AddRepairRegistration()
        {
            tk_WXRequisit so = new TECOCITY_BGOI.tk_WXRequisit();
            so.BXID = CustomerServiceMan.GetTopBXID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //添加2 //修改1
        public ActionResult SaveMaintenanceTask()
        {
            if (ModelState.IsValid)
            {
                string type = Request["Type"].ToString();
                #region [维修报修]
                tk_WXRequisit allr = new tk_WXRequisit();
                allr.BXID = Request["BXID"].ToString();
                allr.UntiID = GAccount.GetAccountInfo().UnitID;//Request["UntiID"].ToString();//加载报装编号对应的用户编号
                allr.RepairID = Request["RepairID"].ToString();
                allr.Customer = Request["Customer"].ToString();
                allr.EnableDate = Convert.ToDateTime(Request["EnableDate"]);
                allr.ContactName = Request["ContactName"].ToString();
                allr.Address = Request["Address"].ToString();
                allr.Tel = Request["Tel"].ToString();

                allr.DeviceName = Request["DeviceName"].ToString();
                allr.DeviceType = Request["DeviceType"].ToString();
                allr.DeviceID = Request["DeviceID"].ToString();
                // allr.CollectionTime = Convert.ToDateTime(Request["CollectionTime"]);

                allr.GuaranteePeriod = Request["GuaranteePeriod"].ToString();
                allr.BXKNum = Request["BXKNum"].ToString();
                allr.RepairTheUser = Request["RepairTheUser"].ToString();

                allr.RepairDate = Convert.ToDateTime(Request["RepairDate"]);
                allr.Sate = "0";
                allr.Remark = Request["Remark"].ToString();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.CreateUser = Request["CreateUser"].ToString();
                allr.Validate = "v";
                #endregion
                string strErr = "";

                if (type == "1")//修改
                {
                    bool b = CustomerServiceMan.SaveMaintenanceTask(allr, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改维修报修";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WXRequisit";
                        log.Typeid = Request["BXID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改维修报修";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WXRequisit";
                        log.Typeid = Request["BXID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//添加
                {
                    bool b = CustomerServiceMan.SaveMaintenanceTask(allr, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加维修报修任务";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WXRequisit";
                        log.Typeid = Request["BXID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加维修报修任务";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_WXRequisit";
                        log.Typeid = Request["BXID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        //详细页面 报修信息
        public ActionResult UserMaintenanceTaskList()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string BXID = Request["BXID"].ToString();
            where += "  BXID='" + BXID + "' and";
            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserMaintenanceTaskList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //撤销
        public ActionResult DeMaintenanceTask()
        {
            string strErr = "";
            string BXID = Request["BXID"].ToString();
            if (CustomerServiceMan.DeMaintenanceTask(BXID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销维修任务";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXRequisit";
                log.Typeid = Request["BXID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销维修任务";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXRequisit";
                log.Typeid = Request["BXID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult WXMaintenanceTask()
        {
            string strErr = "";
            string BXID = Request["BXID"].ToString();
            if (CustomerServiceMan.WXMaintenanceTask(BXID, ref strErr))
            {
                return Json(new { success = "true", Msg = "成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //完成维修
        public ActionResult CompleteMaintenanceTask()
        {
            string strErr = "";
            string BXID = Request["BXID"].ToString();
            if (CustomerServiceMan.CompleteMaintenanceTask(BXID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "完成维修";
                log.LogContent = "完成成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXRequisit";
                log.Typeid = Request["BXID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "完成维修成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "完成维修";
                log.LogContent = "完成失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXRequisit";
                log.Typeid = Request["BXID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //维修记录
        public ActionResult UpMaintenanceTask()
        {
            tk_WXRecord so = new TECOCITY_BGOI.tk_WXRecord();
            so.WXID = CustomerServiceMan.GetTopWXID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult UpWXRecordList()
        {
            string BXID = Request["BXID"].ToString();
            DataTable dt = CustomerServiceMan.UpWXRecordList(BXID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //添加
        public ActionResult SaveUpMainten()
        {
            if (ModelState.IsValid)
            {
                tk_WXRecord allr = new tk_WXRecord();
                allr.CreateTime = Convert.ToDateTime(Request["CreateTime"]);
                allr.WXID = Request["WXID"].ToString();
                allr.BXID = Request["BXID"].ToString();
                allr.SignatureName = Request["SignatureName"].ToString();

                allr.CollectionTime = Convert.ToDateTime(Request["CollectionTime"]);

                allr.MaintenanceTime = Convert.ToDateTime(Request["MaintenanceTime"]);
                allr.MaintenanceVehicle = Request["MaintenanceVehicle"];
                allr.MaintenanceName = Request["MaintenanceName"];
                allr.MaintenanceRecord = Request["MaintenanceRecord"];
                allr.FinalResults = Request["FinalResults"];

                allr.ArtificialCost = Convert.ToDecimal(Request["ArtificialCost"]);
                allr.MaterialCost = Convert.ToDecimal(Request["MaterialCost"]);
                allr.OtherCost = Convert.ToDecimal(Request["OtherCost"]);
                allr.Total = Convert.ToDecimal(Request["TotalP"]);
                allr.PaymentMethod = Request["PaymentMethod"];
                allr.Payee = Request["Payee"];

                allr.Sate = Request["Sate"].ToString();
                allr.SignatureName = Request["SignatureName"].ToString();

                allr.Remark = Request["Remark"].ToString();
                allr.CreateUser = Request["CreateUser"].ToString();

                allr.Validate = "v";
                string[] ProductID = Request["ProductID"].Split(',');
                string[] MainContent = Request["MainContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] Total = Request["Total"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                List<tk_WXRecord_Product> list = new List<tk_WXRecord_Product>();
                for (int i = 0; i < MainContent.Length; i++)
                {
                    tk_WXRecord_Product allproduct = new tk_WXRecord_Product();
                    allproduct.WXID = allr.WXID;
                    allproduct.DID = CustomerServiceMan.GetTopWXIDDID();
                    allproduct.Lname = ProductID[i];
                    allproduct.UnitPrice = Convert.ToDecimal(UnitPrice[i]);
                    allproduct.Total = Convert.ToDecimal(Total[i]);

                    if (string.IsNullOrEmpty(Amount[i]))
                    {
                        allproduct.Amount = 0;
                    }
                    else
                    {
                        allproduct.Amount = Convert.ToInt32(Amount[i]);
                    }
                    list.Add(allproduct);
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveUpMainten(allr, list, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加维修记录";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXRecord";
                    log.Typeid = Request["WXID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加维修记录";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXRecord";
                    log.Typeid = Request["WXID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //修改
        public ActionResult UpDateModifyTask()
        {
            return View();
        }
        public ActionResult UpDateModifyTaskComplaintsa()
        {
            string BXID = Request["BXID"].ToString();
            DataTable dt = CustomerServiceMan.UpDateModifyTaskComplaintsa(BXID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //详细页面 维修信息
        public ActionResult UserMaintenanceTaskTwoList()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string BXID = Request["BXID"].ToString();
            where += "  BXID='" + BXID + "' and";
            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserMaintenanceTaskTwoList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //详细页面 更换零件信息
        public ActionResult UserMaintenanceTaskThreeList()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string BXID = Request["BXID"].ToString();
            where += "  BXID='" + BXID + "' and";
            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserMaintenanceTaskThreeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, BXID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //加载打印数据
        public ActionResult PrintMaintenanceTaskList()
        {
            string BXID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(BXID))
            {
                s += " BXID like '%" + BXID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_WXRequisit ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_WXRequisit so = new TECOCITY_BGOI.tk_WXRequisit();
            foreach (DataRow dt in data.Rows)
            {
                so.BXID = dt["BXID"].ToString();
                so.Customer = dt["Customer"].ToString();

                ViewData["RepairDate"] = Convert.ToDateTime(dt["RepairDate"]).ToShortDateString().ToString();
                //so.RepairDate = Convert.ToDateTime(dt["RepairDate"]);

                // so.RepairDate = DateTime.Parse(dt["RepairDate"].ToString());
                so.ContactName = dt["ContactName"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.Address = dt["Address"].ToString();
                so.DeviceType = dt["DeviceType"].ToString();
                so.CreateUser = dt["CreateUser"].ToString();
                so.RepairTheUser = dt["RepairTheUser"].ToString();
                so.Remark = dt["Remark"].ToString();
                ViewData["EnableDate"] = Convert.ToDateTime(dt["EnableDate"]).ToShortDateString().ToString();
                //so.EnableDate = Convert.ToDateTime(dt["EnableDate"]);


                if (dt["GuaranteePeriod"].ToString() == "0")
                {
                    so.GuaranteePeriod = "保修期内";// dt["GuaranteePeriod"].ToString();
                }
                else
                {
                    so.GuaranteePeriod = "保修期外";// dt["GuaranteePeriod"].ToString();
                }

                so.BXKNum = dt["BXKNum"].ToString();


                if (dt["Sate"].ToString() == "0")
                {
                    so.Sate = "未报修";
                }
                else
                {
                    so.Sate = "已报修";
                }
            }

            string WXRecord = " BGOI_CustomerService.dbo.tk_WXRecord ";
            DataTable daWXRecord = CustomerServiceMan.PrintList(where, WXRecord, ref strErr);
            foreach (DataRow dtrd in daWXRecord.Rows)
            {
                ViewData["MaintenanceTime"] = Convert.ToDateTime(dtrd["MaintenanceTime"]).ToShortDateString().ToString();
                // ViewData["MaintenanceTime"] = dtrd["MaintenanceTime"].ToString();
                ViewData["MaintenanceVehicle"] = dtrd["MaintenanceVehicle"].ToString();
                ViewData["MaintenanceName"] = dtrd["MaintenanceName"].ToString();
                ViewData["MaintenanceRecord"] = dtrd["MaintenanceRecord"].ToString();
            }
            return View(so);
        }
        //根据产品id加载产品规格
        public ActionResult GetPronewSpec()
        {
            string PID = Request["pid"].ToString();
            DataTable dt = CustomerServiceMan.GetPronewSpec(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据用户加载报装编号
        public ActionResult GetUserBAO()
        {

            string Tel = Request["Tel"].ToString();
            DataTable dt = CustomerServiceMan.GetUserBAO(Tel);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //导出
        public FileResult MaintenanceTaskToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            // string DeviceID = Request["DeviceID"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            // string UserName = Request["UserName"].ToString().Trim();
            string Sate = Request["Sate"].ToString().Trim();

            string RepairDateBegin = Request["RepairDateBegin"].ToString();
            if (RepairDateBegin != "")
                RepairDateBegin += " 00:00:00";
            string RepairDateEnd = Request["RepairDateEnd"].ToString();
            if (RepairDateEnd != "")
                RepairDateEnd += " 23:59:59";

            string MaintenanceTimeBegin = Request["MaintenanceTimeBegin"].ToString();
            if (MaintenanceTimeBegin != "")
                MaintenanceTimeBegin += " 00:00:00";
            string MaintenanceTimeEnd = Request["MaintenanceTimeEnd"].ToString();
            if (MaintenanceTimeEnd != "")
                MaintenanceTimeEnd += " 23:59:59";

            if (RepairDateBegin != "" && RepairDateEnd != "")
                where += " RepairDate between '" + RepairDateBegin + "' and '" + RepairDateEnd + "' and";
            if (MaintenanceTimeBegin != "" && MaintenanceTimeEnd != "")
                where += " WXID in( select WXID from BGOI_CustomerService.dbo.tk_WXRecord where MaintenanceTime between '" + MaintenanceTimeBegin + "' and '" + MaintenanceTimeEnd + "' )and";

            //if (Request["DeviceID"] != "")
            //    where += " a.DeviceID like '%" + Request["DeviceID"] + "%' and";
            if (OrderContent != "")
                where += " DeviceName like '%" + OrderContent + "%' and";
            if (Sate != "")
                where += " Sate='" + Sate + "' and";
            //if (UserName != "")
            //    where += " DeviceType  like '%" + UserName + "%' and";

            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_WXRequisit ";
            string FieldName = " BXID, UntiID, RepairID, Customer, ContactName, Address, Tel, DeviceName, DeviceType, EnableDate, GuaranteePeriod, BXKNum, RepairTheUser, RepairDate, " +
                               " (case when Sate=0 then '未维修' else '已维修' end) as Sate,Remark, CreateTime, CreateUser, DeviceID, CollectionTime ";
            string OrderBy = " CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "报装编码-6000,报修建立单位-6000,报修编号-6000,用户名称-5000,联系人-6000,地址-5000,电话-3000,";
                strCols += "设备名称-3000,设备型号-6000,启用日期-5000,保修期-5000,保修卡编号-5000,用户报修简述-5000,报修日期-3000,状态-6000,";
                strCols += "备注-3000,创建时间-6000,登记人-5000,设备编号-5000,收款时间-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "维修任务信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "维修任务信息表.xls");
            }
            else
                return null;

        }
        #endregion
        #region [保修卡]
        public ActionResult WarrantyCard()
        {
            return View();
        }
        public ActionResult WarrantyCardList(WarrantyCardQuery waquery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string Contact = waquery.Contact;// Request["Contact"].ToString().Trim();
                string SpecsModels = waquery.SpecsModels;// Request["SpecsModels"].ToString().Trim();
                string OrderContent = waquery.OrderContent;// Request["OrderContent"].ToString().Trim();
                string UserAdd = waquery.UserAdd;//Request["UserAdd"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                //string State = Request["State"].ToString();
                if (Request["UserAdd"].ToString().Trim() != "")
                    where += " UserAdd like '%" + Request["UserAdd"].ToString().Trim() + "%' and";
                if (Request["OrderContent"].ToString().Trim() != "")
                    where += " OrderContent like '%" + Request["OrderContent"].ToString().Trim() + "%' and PID like '%" + Request["OrderContent"].ToString().Trim() + "%' and";
                if (Request["Contact"].ToString().Trim() != "")
                    where += " Contact like '%" + Request["Contact"].ToString().Trim() + "%' and";
                if (Request["SpecsModels"].ToString().Trim() != "")
                    where += " SpecsModels like '%" + Request["SpecsModels"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " BuyDate between '" + Begin + "' and '" + End + "' and";
                //if (State != "")
                //    where += " State= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = CustomerServiceMan.WarrantyCardList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult AddWarrantyCard()
        {
            tk_WXGuaranteeCard so = new TECOCITY_BGOI.tk_WXGuaranteeCard();
            so.BXKID = CustomerServiceMan.GetTopBXKID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        //保存保修卡
        public ActionResult SaveWarrantyCard()
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                tk_WXGuaranteeCard card = new tk_WXGuaranteeCard();
                card.BXKID = Request["BXKID"].ToString();
                card.ContractID = Request["ContractID"].ToString();
                card.OrderContent = Request["OrderContent"].ToString();
                card.BuyDate = Convert.ToDateTime(Request["BuyDate"].ToString());
                if (Request["BXDate"] != "")
                {
                    card.BXDate = Convert.ToDateTime(Request["BXDate"].ToString());
                }
                card.SpecsModels = Request["SpecsModels"].ToString();
                card.PID = Request["PID"].ToString();
                card.EndUnit = Request["EndUnit"].ToString();
                card.Contact = Request["Contact"].ToString();
                card.Tel = Request["Tel"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.BXKNum = Request["BXKNum"].ToString();
                card.UserAdd = Request["UserAdd"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                bool b = CustomerServiceMan.SaveWarrantyCard(card, ref strErr);
                if (b)
                {

                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加保修卡";
                    log.LogContent = "添加成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXGuaranteeCard";
                    log.Typeid = Request["BXKID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {

                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "添加保修卡";
                    log.LogContent = "添加失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXGuaranteeCard";
                    log.Typeid = Request["BXKID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        //撤销
        public ActionResult DeWarrantyCard()
        {
            string strErr = "";
            string BXKID = Request["BXKID"].ToString();

            if (CustomerServiceMan.DeWarrantyCard(BXKID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销保修卡";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXGuaranteeCard";
                log.Typeid = Request["BXKID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销保修卡";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_WXGuaranteeCard";
                log.Typeid = Request["BXKID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpWarrantyCard()
        {
            tk_WXGuaranteeCard so = new TECOCITY_BGOI.tk_WXGuaranteeCard();
            so.BXKID = Request["BXKID"].ToString();//CustomerServiceMan.GetTopBXKID();
            DataTable dt = CustomerServiceMan.UPdateWarrantyCardList(so.BXKID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewData["BXKNum"] = dt.Rows[0][2].ToString();
                ViewData["ContractID"] = dt.Rows[0][2].ToString();
            }

            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //修改
        public ActionResult UPdateWarrantyCard()
        {
            if (ModelState.IsValid)
            {
                string BXKIDold = Request["BXKIDold"].ToString();
                string strErr = "";
                tk_WXGuaranteeCard card = new tk_WXGuaranteeCard();
                card.BXKID = Request["BXKID"].ToString();
                card.ContractID = Request["ContractID"].ToString();
                card.OrderContent = Request["OrderContent"].ToString();
                card.BuyDate = Convert.ToDateTime(Request["BuyDate"].ToString());
                card.BXDate = Convert.ToDateTime(Request["BXDate"].ToString());
                card.SpecsModels = Request["SpecsModels"].ToString();
                card.PID = Request["PID"].ToString();
                card.EndUnit = Request["EndUnit"].ToString();
                card.Contact = Request["Contact"].ToString();
                card.Tel = Request["Tel"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.BXKNum = Request["BXKNum"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                bool b = CustomerServiceMan.UPdateWarrantyCard(card, BXKIDold, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改保修卡";
                    log.LogContent = "修改成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXGuaranteeCard";
                    log.Typeid = Request["BXKID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改保修卡";
                    log.LogContent = "修改失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "tk_WXGuaranteeCard";
                    log.Typeid = Request["BXKID"].ToString();
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }


        }
        public ActionResult UpWarrantyCardList()
        {
            string BXKID = Request["BXKID"].ToString();
            DataTable dt = CustomerServiceMan.UPdateWarrantyCardList(BXKID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //导出
        public FileResult WarrantyCardToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Contact = Request["Contact"].ToString().Trim();
            string SpecsModels = Request["SpecsModels"].ToString().Trim();
            string OrderContent = Request["OrderContent"].ToString().Trim();
            string UserAdd = Request["UserAdd"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            //string State = Request["State"].ToString();
            if (Request["UserAdd"].ToString().Trim() != "")
                where += " UserAdd like '%" + Request["UserAdd"].ToString().Trim() + "%' and";
            if (Request["OrderContent"].ToString().Trim() != "")
                where += " OrderContent like '%" + Request["OrderContent"].ToString().Trim() + "%' and PID like '%" + Request["OrderContent"].ToString().Trim() + "%' and";
            if (Request["Contact"].ToString().Trim() != "")
                where += " Contact like '%" + Request["Contact"].ToString().Trim() + "%' and";
            if (Request["SpecsModels"].ToString().Trim() != "")
                where += " SpecsModels like '%" + Request["SpecsModels"].ToString().Trim() + "%' and";
            if (Begin != "" && End != "")
                where += " BuyDate between '" + Begin + "' and '" + End + "' and";
            //if (State != "")
            //    where += " State= '" + State + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_WXGuaranteeCard  ";
            string FieldName = " BXKID,Convert(varchar(12),BuyDate,111) as BuyDate,Convert(varchar(12),BXDate,111) as BXDate,Contact," +
                " Tel,OrderContent,SpecsModels,Remark,CreateUser ";
            string OrderBy = " CreateTime desc ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "保修编码-6000,购买日期-6000,保修时间-5000,联系人-6000,联系方式-5000,产品名称-3000,";
                strCols += "产品型号-3000,备注-5000,登记人-6000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "保修卡信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "保修卡信息表.xls");
            }
            else
                return null;

        }
        //加载打印数据
        public ActionResult PrintWarrCard()
        {
            string BXKID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(BXKID))
            {
                s += " BXKID like '%" + BXKID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_WXGuaranteeCard ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_WXGuaranteeCard so = new TECOCITY_BGOI.tk_WXGuaranteeCard();
            foreach (DataRow dt in data.Rows)
            {
                so.BXKID = dt["BXKID"].ToString();
                so.BUnitID = dt["BUnitID"].ToString();
                so.Contact = dt["Contact"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.PID = dt["PID"].ToString();
                so.EndUnit = dt["EndUnit"].ToString();
                ViewData["BXDate"] = Convert.ToDateTime(dt["BXDate"]).ToString("yyy/MM/dd");
                ViewData["BuyDate"] = Convert.ToDateTime(dt["BuyDate"]).ToString("yyy/MM/dd");
                so.OrderContent = dt["OrderContent"].ToString();
                so.SpecsModels = dt["SpecsModels"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = GAccount.GetAccountInfo().UserName;
                so.ContractID = dt["ContractID"].ToString();
                so.Tel = dt["Tel"].ToString();
            }
            return View(so);
        }


        // 保存上传数据 
        public ActionResult SaveWarrantyCardData()
        {
            string strErr = "";
            //维修记录编号	保修卡所属单位	合同编号	保修卡编号	产品名称	产品编号	产品规格型号	购买日期  （2015-10-19 00:00:00.000）	
            //保修时间	最终用户单位	联系人	联系方式	备注	创建时间   （2015-10-19 00:00:00.000）	
            //登记人	初始状态0	客户地址  
            string strData = Request["strData"].ToString();

            bool bo = CustomerServiceMan.SaveWarrantyCardData(strData, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (!bo)
                    return Json(new { success = "false", Msg = "上传产品表单数据失败" });
                else
                    return Json(new { success = "true", SavePlanData = bo });
            }

        }
        #endregion
        #region [调压巡检]
        public ActionResult PressureAdjustingInspection()
        {
            return View();
        }
        public ActionResult AddPressureAdjustingInspection()
        {
            tk_PressureAdjustingInspection so = new TECOCITY_BGOI.tk_PressureAdjustingInspection();
            so.TYID = CustomerServiceMan.GetTopTYID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        //保存调压巡检
        public ActionResult SavePressureAdjustingInspection()
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                tk_PressureAdjustingInspection card = new tk_PressureAdjustingInspection();
                card.TYID = Request["TYID"].ToString();
                card.UserName = Request["UserName"].ToString();
                card.UserAdd = Request["UserAdd"].ToString();
                card.Users = Request["Users"].ToString();
                if (Request["OperationTime"].ToString() != "")
                {
                    card.OperationTime = Convert.ToDateTime(Request["OperationTime"].ToString());
                }
                //if (Request["AfternoonTime"].ToString() != "")
                //{
                //    card.AfternoonTime = Convert.ToDateTime(Request["AfternoonTime"].ToString());
                //}
                card.Tel = Request["Tel"].ToString();
                card.KeyStorageUnitJia = Request["KeyStorageUnitJia"].ToString();
                card.KeyStorageUnitYi = Request["KeyStorageUnitYi"].ToString();
                card.Uses = Request["Uses"].ToString();
                card.Boiler = "";
                card.CreateUser = Request["CreateUser"].ToString();
                card.KungFu = "";
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                card.Civil = "";
                card.UserSignature = Request["UserSignature"].ToString();
                card.UsePressureShang = "";
                card.UsePressureXia = "";
                card.InspectionPersonnel = Request["InspectionPersonnel"].ToString();
                card.Remarks = Request["Remarks"].ToString();
                string type = Request["type"].ToString();

                string[] arrUsePressureShangP1 = Request["UsePressureShangP1"].Split(',');
                string[] arrUsePressureShangP2 = Request["UsePressureShangP2"].Split(',');
                string[] arrUsePressureShangPb = Request["UsePressureShangPb"].Split(',');
                string[] arrUsePressureShangPf = Request["UsePressureShangPf"].Split(',');
                string[] arrUsePressureXiaP1 = Request["UsePressureXiaP1"].Split(',');
                string[] arrUsePressureXiaP2 = Request["UsePressureXiaP2"].Split(',');
                string[] arrUsePressureXiaPb = Request["UsePressureXiaPb"].Split(',');

                tk_PressureAdjustingInspectionDetail deInfo = new tk_PressureAdjustingInspectionDetail();
                List<tk_PressureAdjustingInspectionDetail> detailList = new List<tk_PressureAdjustingInspectionDetail>();
                for (int i = 0; i < arrUsePressureShangP1.Length; i++)
                {
                    deInfo = new tk_PressureAdjustingInspectionDetail();
                    deInfo.TXID = CustomerServiceMan.GetTopTXID();
                    deInfo.TYID = Request["TYID"].ToString();
                    deInfo.UsePressureShangP1 = arrUsePressureShangP1[i].ToString();
                    deInfo.UsePressureShangP2 = arrUsePressureShangP2[i].ToString();
                    deInfo.UsePressureShangPb = arrUsePressureShangPb[i].ToString();
                    deInfo.UsePressureShangPf = arrUsePressureShangPf[i].ToString();
                    deInfo.UsePressureXiaP1 = arrUsePressureXiaP1[i].ToString();
                    deInfo.UsePressureXiaP2 = arrUsePressureXiaP2[i].ToString();
                    deInfo.UsePressureXiaPb = arrUsePressureXiaPb[i].ToString();
                    deInfo.Validate = "v";
                    detailList.Add(deInfo);
                }
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SavePressureAdjustingInspection(card, detailList, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加调压巡检";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_PressureAdjustingInspection";
                        log.Typeid = Request["TYID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加调压巡检";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_PressureAdjustingInspection";
                        log.Typeid = Request["TYID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SavePressureAdjustingInspection(card, detailList, type, ref strErr);
                    if (b)
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改调压巡检";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_PressureAdjustingInspection";
                        log.Typeid = Request["TYID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改调压巡检";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_PressureAdjustingInspection";
                        log.Typeid = Request["TYID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult PressureAdjustingInspectionList(PressureAdjustingInspectionQuery PreQuery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string UserName = PreQuery.UserName;// Request["UserName"].ToString().Trim();
                string InspectionPersonnel = PreQuery.InspectionPersonnel;// Request["InspectionPersonnel"].ToString().Trim();
                string Tel = PreQuery.Tel;// Request["Tel"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["UserName"].ToString().Trim() != "")
                    where += " UserName like '%" + Request["UserName"].ToString().Trim() + "%' and";
                if (Request["Tel"].ToString().Trim() != "")
                    where += " Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
                if (Request["InspectionPersonnel"].ToString().Trim() != "")
                    where += " InspectionPersonnel like '%" + Request["InspectionPersonnel"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " OperationTime between '" + Begin + "' and '" + End + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.PressureAdjustingInspectionList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DePressureAdjustingInspection()
        {
            string strErr = "";
            string TYID = Request["TYID"].ToString();

            if (CustomerServiceMan.DePressureAdjustingInspection(TYID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销调压巡检";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_PressureAdjustingInspection";
                log.Typeid = Request["TYID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销调压巡检";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_PressureAdjustingInspection";
                log.Typeid = Request["TYID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        //加载打印数据
        public ActionResult PrintPressureAdjustingInspection()
        {
            string TYID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(TYID))
            {
                s += " TYID like '%" + TYID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_PressureAdjustingInspection ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_PressureAdjustingInspection so = new TECOCITY_BGOI.tk_PressureAdjustingInspection();
            foreach (DataRow dt in data.Rows)
            {
                so.TYID = dt["TYID"].ToString();
                so.UserAdd = dt["UserAdd"].ToString();
                so.Users = dt["Users"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.KeyStorageUnitJia = dt["KeyStorageUnitJia"].ToString();
                so.Uses = dt["Uses"].ToString();
                so.Boiler = dt["Boiler"].ToString();
                so.KungFu = dt["KungFu"].ToString();
                so.CreateUser = dt["CreateUser"].ToString();
                so.Civil = dt["Civil"].ToString();
                so.Other = dt["Other"].ToString();
                so.UsePressureShang = dt["UsePressureShang"].ToString();
                so.Tel = dt["Tel"].ToString();
                so.KeyStorageUnitJia = dt["KeyStorageUnitJia"].ToString();
                ViewData["OperationTime"] = Convert.ToDateTime(dt["OperationTime"]).ToString("yyy/MM/dd");


                so.InspectionPersonnel = dt["InspectionPersonnel"].ToString();
                so.UserSignature = dt["UserSignature"].ToString();
                so.Remarks = dt["Remarks"].ToString();
                so.KeyStorageUnitYi = dt["KeyStorageUnitYi"].ToString();
                so.UsePressureXia = dt["UsePressureXia"].ToString();
                so.CreateTime = Convert.ToDateTime(dt["CreateTime"]);
                so.Validate = dt["Validate"].ToString();
                so.UserName = dt["UserName"].ToString();
                //so.AfternoonTime = Convert.ToDateTime(dt["AfternoonTime"]);
            }
            return View(so);
        }
        public ActionResult UpPrintPressureAdjustingInspection()
        {
            string TYID = Request["Info"].ToString();
            DataTable dt = CustomerServiceMan.UpPrintPressureAdjustingInspection(TYID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpPressureAdjustingInspection()
        {
            tk_PressureAdjustingInspection so = new TECOCITY_BGOI.tk_PressureAdjustingInspection();
            so.TYID = Request["TYID"].ToString();
            DataTable data = CustomerServiceMan.UpPressureAdjustingInspection(so.TYID);
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow dt in data.Rows)
                {
                    so.TYID = dt["TYID"].ToString();
                    so.UserAdd = dt["UserAdd"].ToString();
                    so.Users = dt["Users"].ToString();
                    so.Tel = dt["Tel"].ToString();
                    so.KeyStorageUnitJia = dt["KeyStorageUnitJia"].ToString();
                    //so.Uses = dt["Uses"].ToString();
                    //so.Boiler = dt["Boiler"].ToString();
                    //so.KungFu = dt["KungFu"].ToString();
                    //so.Civil = dt["Civil"].ToString();
                    //so.Other = dt["Other"].ToString();
                    so.UsePressureShang = dt["UsePressureShang"].ToString();
                    so.Tel = dt["Tel"].ToString();
                    so.KeyStorageUnitJia = dt["KeyStorageUnitJia"].ToString();
                    so.OperationTime = Convert.ToDateTime(dt["OperationTime"]);
                    so.InspectionPersonnel = dt["InspectionPersonnel"].ToString();
                    so.UserSignature = dt["UserSignature"].ToString();
                    so.Remarks = dt["Remarks"].ToString();
                    so.KeyStorageUnitYi = dt["KeyStorageUnitYi"].ToString();
                    so.UsePressureXia = dt["UsePressureXia"].ToString();
                    so.CreateTime = Convert.ToDateTime(dt["CreateTime"]);
                    so.Validate = dt["Validate"].ToString();
                    so.UserName = dt["UserName"].ToString();
                    //so.AfternoonTime = Convert.ToDateTime(dt["AfternoonTime"]);
                }
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult PressureAdjustingInspectionDetailList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string TYID = Request["TYID"].ToString().Trim();// and Validate='v' and UsePressureShangP1!='' and";
            UIDataTable udtTask = CustomerServiceMan.PressureAdjustingInspectionDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, TYID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpTime()
        {
            string TYID = Request["Info"].ToString();
            DataTable dt = CustomerServiceMan.UpTime(TYID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpUser()
        {
            string TYID = Request["Info"].ToString();
            DataTable dt = CustomerServiceMan.UpTime(TYID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //导出
        public FileResult PressureAdjustingInspectionToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string UserName = Request["UserName"].ToString().Trim();
            string InspectionPersonnel = Request["InspectionPersonnel"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (UserName != "")
                where += " UserName like '%" + UserName + "%' and";
            if (Tel != "")
                where += " Tel like '%" + Tel + "%' and";
            if (InspectionPersonnel != "")
                where += " InspectionPersonnel like '%" + InspectionPersonnel + "%' and";
            if (Begin != "" && End != "")
                where += " OperationTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_PressureAdjustingInspection ";
            string FieldName = " TYID, UserAdd, Users, Tel, (case when Uses='0' then '锅炉' when Uses='1' then '公福' when Uses='2' then '民用' else '其它' end ) as Uses,Convert(varchar(100),OperationTime,23) as OperationTime, InspectionPersonnel, UserSignature, Remarks,  CreateTime, CreateUser, UserName,Convert(varchar(100),AfternoonTime,23) as  AfternoonTime ";
            string OrderBy = " CreateTime desc";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "调压记录编号-6000,用户地址-6000,联系人-6000,电话-5000,用途-6000,";
                strCols += "运维时间-5000,巡检人员-5000,用户签字-5000,备注-5000,创建时间-5000,登记人-6000,";
                strCols += "用户名称-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "调压巡检记录表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "调压巡检记录表.xls");
            }
            else
                return null;

        }
        #endregion
        #region [设备调试任务单]
        //设备调试任务单页面
        public ActionResult EquipmentCommissioning()
        {
            return View();
        }
        public ActionResult AddEquipmentCommissioning()
        {
            tk_EquipmentDebugging so = new TECOCITY_BGOI.tk_EquipmentDebugging();
            so.TRID = CustomerServiceMan.GetTopTRID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            so.DebTime = DateTime.Now;
            return View(so);
        }
        //保存设备调试任务单
        public ActionResult SaveEquipmentCommissioning()
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                tk_EquipmentDebugging card = new tk_EquipmentDebugging();
                card.TRID = Request["TRID"].ToString();
                card.UserName = Request["UserName"].ToString();
                card.Address = Request["Address"].ToString();
                card.ContactPerson = Request["ContactPerson"].ToString();
                card.DebTime = Convert.ToDateTime(Request["DebTime"].ToString());
                card.Tel = Request["Tel"].ToString();
                card.ConstructionUnit = Request["ConstructionUnit"].ToString();
                card.CUnitPer = Request["CUnitPer"].ToString();
                card.CUnitTel = Request["CUnitTel"].ToString();
                card.EquManType = Request["EquManType"].ToString();
                card.UnitName = Request["UnitName"].ToString();
                card.UnitTel = Request["UnitTel"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                card.UnitPer = Request["UnitPer"].ToString();
                card.DebPerson = Request["DebPerson"].ToString();
                card.UserType = Request["UserType"].ToString();
                card.Gas = Request["Gas"].ToString();
                card.FieldFailure = Request["FieldFailure"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                string type = Request["type"].ToString();

                string[] arrProName = Request["ProName"].Split(',');
                string[] arrPID = Request["PID"].Split(',');
                string[] arrSpecsModels = Request["SpecsModels"].Split(',');

                tk_DebuggingSituation deInfo = new tk_DebuggingSituation();
                List<tk_DebuggingSituation> detailList = new List<tk_DebuggingSituation>();
                for (int i = 0; i < arrProName.Length; i++)
                {
                    deInfo = new tk_DebuggingSituation();
                    deInfo.PID = arrPID[i].ToString();
                    deInfo.Spec = arrSpecsModels[i].ToString();

                    deInfo.ProName = arrProName[i].ToString();

                    deInfo.QKID = CustomerServiceMan.GetTopQKID();
                    deInfo.TRID = Request["TRID"].ToString();
                    deInfo.ProductForm = Request["ProductForm"].ToString();
                    deInfo.PowerNumber = Request["PowerNumber"].ToString();
                    deInfo.PowerTime = Convert.ToDateTime(Request["PowerTime"]);
                    deInfo.PowerInitialP = Request["PowerInitialP"].ToString();
                    deInfo.StepDownNumber = Request["StepDownNumber"].ToString();
                    deInfo.StepDownTime = Convert.ToDateTime(Request["StepDownTime"]);
                    deInfo.StepDownInitialP = Request["StepDownInitialP"].ToString();
                    deInfo.InletPreP1 = Request["InletPreP1"].ToString();
                    deInfo.BleedingpreP1 = Request["BleedingpreP1"].ToString();
                    deInfo.PowerExportPreP2 = Request["PowerExportPreP2"].ToString();
                    deInfo.PowerOffPrePb = Request["PowerOffPrePb"].ToString();
                    deInfo.PowerCutOffPrePq = Request["PowerCutOffPrePq"].ToString();
                    deInfo.SDExportPreP2 = Request["SDExportPreP2"].ToString();
                    deInfo.SDPowerOffPrePb = Request["SDPowerOffPrePb"].ToString();
                    deInfo.SDCutOffPrePq = Request["SDCutOffPrePq"].ToString();
                    detailList.Add(deInfo);
                }
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SaveEquipmentCommissioning(card, detailList, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加设备调试任务单";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_EquipmentDebugging";
                        log.Typeid = Request["TRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加设备调试任务单";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_EquipmentDebugging";
                        log.Typeid = Request["TRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SaveEquipmentCommissioning(card, detailList, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改设备调试任务单";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_EquipmentDebugging";
                        log.Typeid = Request["TRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改设备调试任务单";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_EquipmentDebugging";
                        log.Typeid = Request["TRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //
        public ActionResult EquipmentCommissioningList(EquipmentCommissioningQuery PreQuery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string UserName = PreQuery.UserName;// Request["UserName"].ToString().Trim();
                string Tel = PreQuery.Tel;// Request["Tel"].ToString().Trim();
                string Spec = PreQuery.Spec;// Request["Spec"].ToString().Trim();
                string PID = PreQuery.PID;// Request["PID"].ToString().Trim();

                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["UserName"] != "")
                    where += " UserName like '%" + Request["UserName"] + "%' and";
                if (Request["Tel"] != "")
                    where += " Tel like '%" + Request["Tel"] + "%' and";
                if (Request["Spec"] != "")
                    //where += " b.Spec like '%" + Request["Spec"] + "%' and";
                    where += " TRID in (select TRID from BGOI_CustomerService.dbo.tk_DebuggingSituation where Spec like '%" + Request["Spec"] + "%') and";
                if (Request["PID"] != "")
                    //where += " b.PID like '%" + Request["PID"] + "%' and";
                    where += " TRID in (select TRID from BGOI_CustomerService.dbo.tk_DebuggingSituation where PID like '%" + Request["PID"] + "%') and";
                if (Begin != "" && End != "")
                    where += " DebTime between '" + Begin + "' and '" + End + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.EquipmentCommissioningList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeEquipmentCommissioning()
        {
            string strErr = "";
            string TRID = Request["TRID"].ToString();

            if (CustomerServiceMan.DeEquipmentCommissioning(TRID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销设备调试任务单";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_EquipmentDebugging";
                log.Typeid = Request["TRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销设备调试任务单";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_EquipmentDebugging";
                log.Typeid = Request["TRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpEquipmentCommissioning()
        {
            tk_EquipmentDebugging so = new TECOCITY_BGOI.tk_EquipmentDebugging();
            so.TRID = Request["TRID"].ToString();
            DataTable data = CustomerServiceMan.UpEquipmentCommissioning(so.TRID);
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow dt in data.Rows)
                {
                    so.TRID = dt["TRID"].ToString();
                    so.UserName = dt["UserName"].ToString();
                    so.Address = dt["Address"].ToString();
                    so.Tel = dt["Tel"].ToString();
                    so.ContactPerson = dt["ContactPerson"].ToString();
                    so.ConstructionUnit = dt["ConstructionUnit"].ToString();
                    so.CUnitPer = dt["CUnitPer"].ToString();
                    so.CUnitTel = dt["CUnitTel"].ToString();
                    so.UnitName = dt["UnitName"].ToString();
                    so.UnitTel = dt["UnitTel"].ToString();
                    so.UnitPer = dt["UnitPer"].ToString();
                    so.DebPerson = dt["DebPerson"].ToString();
                    so.DebTime = Convert.ToDateTime(dt["DebTime"]);
                    so.FieldFailure = dt["FieldFailure"].ToString();
                    so.Remark = dt["Remark"].ToString();
                    so.CreateUser = dt["CreateUser"].ToString();
                }
            }
            return View(so);
        }
        //根据编号加载主表信息
        public ActionResult GetEquipmentDebugging()
        {
            string TRID = Request["TRID"].ToString();
            DataTable dt = CustomerServiceMan.GetEquipmentDebugging(TRID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //根据编号加载副表信息
        public ActionResult UpDebuggingSituation()
        {
            string TRID = Request["TRID"].ToString();
            DataTable dtsi = CustomerServiceMan.UpDebuggingSituation(TRID);
            if (dtsi == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dtsi) });
        }
        //加载详细
        public ActionResult EquipmentCommissioningDetialList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string TRID = Request["TRID"].ToString().Trim();
            UIDataTable udtTask = CustomerServiceMan.EquipmentCommissioningDetialList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, TRID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //加载打印数据
        public ActionResult PrintEquipmentCommissioning()
        {
            tk_EquipmentDebugging so = new TECOCITY_BGOI.tk_EquipmentDebugging();
            so.TRID = Request["Info"].ToString();
            DataTable data = CustomerServiceMan.UpEquipmentCommissioning(so.TRID);
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow dt in data.Rows)
                {
                    so.TRID = dt["TRID"].ToString();
                    so.UserName = dt["UserName"].ToString();
                    so.Address = dt["Address"].ToString();
                    so.Tel = dt["Tel"].ToString();
                    so.ContactPerson = dt["ContactPerson"].ToString();
                    so.ConstructionUnit = dt["ConstructionUnit"].ToString();
                    so.CUnitPer = dt["CUnitPer"].ToString();
                    so.CUnitTel = dt["CUnitTel"].ToString();
                    so.UnitName = dt["UnitName"].ToString();
                    so.UnitTel = dt["UnitTel"].ToString();
                    so.UnitPer = dt["UnitPer"].ToString();
                    so.DebPerson = dt["DebPerson"].ToString();
                    so.DebTime = Convert.ToDateTime(dt["DebTime"]);
                    so.FieldFailure = dt["FieldFailure"].ToString();
                    so.Remark = dt["Remark"].ToString();
                    so.CreateUser = dt["CreateUser"].ToString();
                }
            }
            DataTable dtfb = CustomerServiceMan.UpDebuggingSituation(so.TRID);
            if (dtfb != null && dtfb.Rows.Count > 0)
            {
                foreach (DataRow dr in dtfb.Rows)
                {
                    ViewData["PowerNumber"] = dr["PowerNumber"].ToString();
                    ViewData["PowerInitialP"] = dr["PowerInitialP"].ToString();
                    ViewData["StepDownNumber"] = dr["StepDownNumber"].ToString();
                    ViewData["StepDownInitialP"] = dr["StepDownInitialP"].ToString();
                    ViewData["InletPreP1"] = dr["InletPreP1"].ToString();
                    ViewData["BleedingpreP1"] = dr["BleedingpreP1"].ToString();
                    ViewData["PowerExportPreP2"] = dr["PowerExportPreP2"].ToString();
                    ViewData["PowerOffPrePb"] = dr["PowerOffPrePb"].ToString();
                    ViewData["PowerCutOffPrePq"] = dr["PowerCutOffPrePq"].ToString();
                    ViewData["SDExportPreP2"] = dr["SDExportPreP2"].ToString();
                    ViewData["SDPowerOffPrePb"] = dr["SDPowerOffPrePb"].ToString();
                    ViewData["SDCutOffPrePq"] = dr["SDCutOffPrePq"].ToString();
                }
            }

            return View(so);
        }
        //加载派工单
        public ActionResult EqProductReportList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string RelationID = Request["TRID"].ToString();
            UIDataTable udtTask = CustomerServiceMan.EqProductReportList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, RelationID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //根据产品加载报修
        public ActionResult GetPIDBaoDetail()
        {
            string PID = Request["PID"].ToString();
            DataTable dt = CustomerServiceMan.GetPIDBaoDetail(PID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //导出
        public FileResult EquipmentCommissioningToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string UserName = Request["UserName"].ToString().Trim();
            string Tel = Request["Tel"].ToString().Trim();
            string Spec = Request["Spec"].ToString().Trim();
            string PID = Request["PID"].ToString().Trim();

            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (UserName != "")
                where += " UserName like '%" + UserName + "%' and";
            if (Tel != "")
                where += " Tel like '%" + Tel + "%' and";
            if (Spec != "")
                where += " TRID in (select TRID from BGOI_CustomerService.dbo.tk_DebuggingSituation where Spec like '%" + Spec + "%') and";
            if (PID != "")
                where += " TRID in (select TRID from BGOI_CustomerService.dbo.tk_DebuggingSituation where PID like '%" + PID + "%') and";
            if (Begin != "" && End != "")
                where += " DebTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_EquipmentDebugging ";
            string FieldName = " TRID, UserName, Address, ContactPerson, Tel, ConstructionUnit, CUnitPer, CUnitTel," +
                                " (case when EquManType=0 then '自管' when EquManType=1 then '厂家代管' when EquManType=2 then '输配公司代管' when EquManType=3 then '燃气施工方式代管 ' else '其他公司代管' end) as EquManType, " +
                                " UnitName, UnitTel, UnitPer, DebPerson, DebTime, " +
                                " (case when UserType=0 then '锅炉' when UserType=1 then '直燃机 ' when UserType=2 then ' 公福' when UserType=3 then '居民户 ' else '其它' end) as UserType, " +
                                " (case when Gas=0 then '天然气' when Gas=1 then '人工煤气 ' when Gas=2 then ' 液化石油气' when Gas=3 then ' 混气 ' when Gas=4 then ' 沼气 ' when Gas=5 then ' 压缩天然气 ' else '其它' end) as Gas, " +
                                " FieldFailure, Remark, CreateTime, CreateUser ";
            string OrderBy = " CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "调试任务编号-6000,用户名称-6000,用户地址-6000,联系人-5000,联系电话-6000,施工单位-5000,施工单位联系人-3000,";
                strCols += "施工单位联系电话-3000,设备管理方式 -6000,单位名称-5000,单位电话-5000,单位联系人-5000,调试人员-5000,调试日期-5000,用户类型 -6000,";
                strCols += "气种-3000,现场故障情况-6000,备注-3000,创建时间-6000,创建人-6000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "设备调试任务单", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "设备调试任务单.xls");
            }
            else
                return null;

        }


        //判断是否报装
        public ActionResult PanDuanIfPro()
        {
            string TRID = Request["TRID"].ToString();
            DataTable dt = CustomerServiceMan.PanDuanIfPro(TRID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #endregion
        #region [销售记录]
        #region [收款记录]
        public ActionResult CollectionRecord()
        {
            ViewData["webkey"] = "售后收款记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后收款记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        public ActionResult AddCollectionRecord()
        {
            tk_CollectionRecord so = new TECOCITY_BGOI.tk_CollectionRecord();
            so.CRID = CustomerServiceMan.GetTopCRID();
            ViewData["BRDID"] = Request["BRDID"].ToString();
            so.CreateUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        //保存收款记录
        public ActionResult SaveCollectionRecord()
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                string type = Request["type"].ToString();
                tk_CollectionRecord card = new tk_CollectionRecord();
                card.CRID = Request["CRID"].ToString();
                card.PaymentUnit = Request["PaymentUnit"].ToString();
                card.CollectionAmount = Request["CollectionAmount"].ToString();
                card.PaymentContent = Request["PaymentContent"].ToString();
                card.PaymentMethod = Request["PaymentMethod"].ToString();
                card.PaymentPeo = Request["PaymentPeo"].ToString();
                card.CRTime = Convert.ToDateTime(Request["CRTime"].ToString());
                card.CorporateFinance = Request["CorporateFinance"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                card.State = "0";
                card.StateNew = "0";
                card.BRDID = Request["BRDID"].ToString();
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SaveCollectionRecord(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加收款记录";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CollectionRecord";
                        log.Typeid = Request["CRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加收款记录";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CollectionRecord";
                        log.Typeid = Request["CRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SaveCollectionRecord(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改收款记录";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CollectionRecord";
                        log.Typeid = Request["CRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改收款记录";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CollectionRecord";
                        log.Typeid = Request["CRID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        public ActionResult CollectionRecordList(tk_CollectionRecord waquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PaymentPeo = waquery.PaymentPeo;// Request["PaymentPeo"].ToString().Trim();
                string PaymentUnit = waquery.PaymentUnit;// Request["PaymentUnit"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["PaymentPeo"].ToString().Trim() != "")
                    where += " PaymentPeo like '%" + Request["PaymentPeo"].ToString().Trim() + "%' and";
                if (Request["PaymentUnit"].ToString().Trim() != "")
                    where += " PaymentUnit like '%" + Request["PaymentUnit"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " CRTime between '" + Begin + "' and '" + End + "' and";
                string State = Request["State"].ToString();
                if (State != "")
                    where += " StateNew= '" + State + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.CollectionRecordList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeCollectionRecord()
        {
            string strErr = "";
            string CRID = Request["CRID"].ToString();

            if (CustomerServiceMan.DeCollectionRecord(CRID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销收款记录";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_CollectionRecord";
                log.Typeid = Request["CRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销收款记录";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_CollectionRecord";
                log.Typeid = Request["CRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }

        //改收款状态
        public ActionResult GetState()
        {
            string strErr = "";
            string CRID = Request["CRID"].ToString();

            if (CustomerServiceMan.GetState(CRID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "修改收款状态";
                log.LogContent = "修改成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_CollectionRecord";
                log.Typeid = Request["CRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "收款成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "修改收款状态";
                log.LogContent = "修改失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_CollectionRecord";
                log.Typeid = Request["CRID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult GetSKSP()
        {
            string CRID = Request["CRID"].ToString();
            if (CustomerServiceMan.GetSKSP(CRID).Rows[0][13].ToString() == "2")
            {
                return Json(new { success = "true", Msg = "已审批" });
            }
            else if (CustomerServiceMan.GetSKSP(CRID).Rows[0][13].ToString() == "1")
            {
                return Json(new { success = "false", Msg = "待审批" });
            }
            else
            {
                return Json(new { success = "false", Msg = "未提交审批" });
            }
        }
        public ActionResult UpCollectionRecord()
        {
            tk_CollectionRecord so = new TECOCITY_BGOI.tk_CollectionRecord();
            so.CRID = Request["CRID"].ToString();
            DataTable dt = CustomerServiceMan.UpCollectionRecord(so.CRID);
            if (dt != null && dt.Rows.Count > 0)
            {
                so.CRTime = Convert.ToDateTime(dt.Rows[0][1].ToString());
                so.PaymentUnit = dt.Rows[0][2].ToString();
                so.CollectionAmount = dt.Rows[0][3].ToString();
                so.PaymentContent = dt.Rows[0][4].ToString();
                // so.PaymentMethod = dt.Rows[0][5].ToString();
                so.PaymentPeo = dt.Rows[0][6].ToString();
                so.CorporateFinance = dt.Rows[0][7].ToString();
                so.Remark = dt.Rows[0][8].ToString();
                so.BRDID = dt.Rows[0][13].ToString();
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult UpUpCollectionRecordList()
        {
            string CRID = Request["CRID"].ToString();
            DataTable dt = CustomerServiceMan.UpUpCollectionRecordList(CRID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //加载打印数据
        public ActionResult PrintCollectionRecord()
        {
            string CRID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(CRID))
            {
                s += " CRID like '%" + CRID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_CollectionRecord ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_CollectionRecord so = new TECOCITY_BGOI.tk_CollectionRecord();
            foreach (DataRow dt in data.Rows)
            {
                so.CRID = dt["CRID"].ToString();
                // so.CRTime = Convert.ToDateTime(dt["CRTime"]);
                ViewData["CRTime"] = Convert.ToDateTime(dt["CRTime"]).ToString("yyy/MM/dd");
                so.PaymentUnit = dt["PaymentUnit"].ToString();
                so.CollectionAmount = dt["CollectionAmount"].ToString();
                so.PaymentContent = dt["PaymentContent"].ToString();
                if (dt["PaymentMethod"].ToString() == "0")
                {
                    so.PaymentMethod = "汇款";
                }
                else if (dt["PaymentMethod"].ToString() == "1")
                {
                    so.PaymentMethod = "支票";
                }
                else
                {
                    so.PaymentMethod = "现金";
                }
                so.PaymentPeo = dt["PaymentPeo"].ToString();
                so.CorporateFinance = dt["CorporateFinance"].ToString();
                so.Remark = dt["Remark"].ToString();
                so.CreateUser = GAccount.GetAccountInfo().UserName;
            }
            return View(so);
        }
        //导出
        public FileResult CollectionRecordToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PaymentPeo = Request["PaymentPeo"].ToString().Trim();
            string PaymentUnit = Request["PaymentUnit"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (PaymentPeo != "")
                where += " PaymentPeo like '%" + PaymentPeo + "%' and";
            if (PaymentUnit != "")
                where += " PaymentUnit like '%" + PaymentUnit + "%' and";
            if (Begin != "" && End != "")
                where += " CRTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_CollectionRecord ";
            string FieldName = " Convert(varchar(100),CRTime,23) as CRTime, PaymentUnit, CollectionAmount, PaymentContent, PaymentMethod, PaymentPeo, CorporateFinance, Remark ";
            string OrderBy = " CreateTime  desc";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "日期-6000,付款单位-6000,收款额-6000,付款内容-5000,付款方式-6000,收款人-5000,公司财务-3000,备注-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "收款记录信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "收款记录信息表.xls");
            }
            else
                return null;


        }


        public ActionResult GetBasCusCollectionRecord()
        {
            string CRID = Request["CRID"].ToString();
            DataTable dt = CustomerServiceMan.GetBasCusCollectionRecord(CRID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult BillingRecordsProcessing()
        {
            ViewData["webkey"] = "售后收款记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后收款记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult UserBillingRecordsProcessing()
        {
            string where = " 1=1 and a.ValiDate = 'v' and a.state != -1 and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string HandleUser = Request["HandleUser"].ToString().Trim();
            if (HandleUser != "")
            {
                where += " a.HandleUser like '%" + HandleUser + "%' and";
            }

            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserBillingRecordsProcessing(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region [订货单]
        public ActionResult CustimerOrder()
        {
            return View();
        }
        public ActionResult GetOrderInfo()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = CustomerServiceMan.GetOrderInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCustimerOrder()
        {
            OrdersInfo orderinfo = new OrdersInfo();
            orderinfo.PID = Request["ID"];
            orderinfo.OrderID = CustomerServiceMan.GetNewOrderID();
            if (orderinfo.ContractID == "" || orderinfo.ContractID == null)
            {
                orderinfo.ContractID = CustomerServiceMan.GetMaxContractID();
            }
            return View(orderinfo);
        }
        public ActionResult SaveOrderInfo(OrdersInfoNew orderinfo)
        {
            if (ModelState.IsValid)
            {
                orderinfo.PID = Request["ID"];
                orderinfo.State = "0";
                orderinfo.Ostate = "0";
                orderinfo.ContractDate = DateTime.Now;
                orderinfo.CreateTime = DateTime.Now.ToString();
                orderinfo.CreateUser = GAccount.GetAccountInfo().UserName;
                orderinfo.Validate = "v";
                orderinfo.SalesType = "Sa01";
                orderinfo.SupplyTime = DateTime.Now;
                orderinfo.UnitID = GAccount.GetAccountInfo().UnitID;//1225k

                orderinfo.ExpectedReturnDate = Request["ExpectedReturnDate"].ToString();

                ////订单详细表
                List<Orders_DetailInfoNew> list = new List<Orders_DetailInfoNew>();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] OrderContent = Request["OrderContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] Subtotal = Request["Subtotal"].Split(',');
                string[] Technology = Request["Technology"].Split(',');
                string[] DeliveryTime = Request["DeliveryTime"].Split(',');
                //string[] YPrice = Request["YPrice"].Split(',');
                string[] TaxRate = Request["TaxRate"].Split(',');
                for (int i = 0; i < OrderContent.Length; i++)
                {
                    if (ProductID[i] != "")
                    {
                        Orders_DetailInfoNew ordersDetail = new Orders_DetailInfoNew();
                        ordersDetail.PID = orderinfo.PID;
                        ordersDetail.ProductID = ProductID[i].ToString();
                        ordersDetail.OrderID = orderinfo.OrderID;
                        ordersDetail.DID = ordersDetail.OrderID + string.Format("{0:D3}", i + 1);
                        ordersDetail.OrderContent = OrderContent[i].ToString();
                        ordersDetail.SpecsModels = SpecsModels[i].ToString();
                        ordersDetail.Manufacturer = Supplier[i].ToString();
                        ordersDetail.OrderUnit = Unit[i].ToString();
                        if (!string.IsNullOrEmpty(UnitPrice[i].ToString()))
                        { ordersDetail.Price = Convert.ToDecimal(UnitPrice[i].ToString()); }//最终成交单价
                        //if (!string.IsNullOrEmpty(YPrice[i].ToString()) && YPrice[i] != "undefined")
                        //{
                        //    ordersDetail.UnitPrice = Convert.ToDecimal(YPrice[i].ToString()); //不含税单价
                        //}
                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            ordersDetail.OrderNum = 0;
                        }
                        else { ordersDetail.OrderNum = Convert.ToInt32(Amount[i]); }
                        ordersDetail.TaxRate = TaxRate[i].ToString();
                        if (string.IsNullOrEmpty(Subtotal[i]))
                        {
                            ordersDetail.Subtotal = 0.00M;
                        }
                        else { ordersDetail.Subtotal = Convert.ToDecimal(Subtotal[i]); }

                        ordersDetail.Technology = Technology[i].ToString();
                        if (string.IsNullOrEmpty(DeliveryTime[i]))
                        {
                            ordersDetail.DeliveryTime = DateTime.Now;
                        }
                        else
                        {
                            ordersDetail.DeliveryTime = Convert.ToDateTime(DeliveryTime[i]);
                        }
                        ordersDetail.CreateTime = DateTime.Now.ToString();
                        ordersDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        ordersDetail.Validate = "v";
                        ordersDetail.State = "0";
                        list.Add(ordersDetail);
                    }
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveOrderInfo(orderinfo, list, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "新增订单";
                    log.LogContent = "新增成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "OrdersInfo";
                    log.Typeid = orderinfo.OrderID;
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });

                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "新增订单";
                    log.LogContent = "新增失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "OrdersInfo";
                    log.Typeid = orderinfo.OrderID;
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult LoadOrderDetailnew()
        {
            string OrderID = Request["OrderID"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = CustomerServiceMan.LoadOrderDetailnew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CanCelOrdersInfonew()
        {
            string OrderID = Request["ID"].ToString();
            string strErr = "";
            bool b = CustomerServiceMan.CanCelOrdersInfonew(OrderID, ref strErr);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult UpdateOrdersInfonew()
        {
            string OrderID = Request.QueryString["OrderID"].ToString();
            OrdersInfo OrderInfo = CustomerServiceMan.GetOrdersByOrderIDnew(OrderID);
            ViewData["PID"] = OrderInfo.PID;
            return View(OrderInfo);
        }



        public ActionResult GetOrdersDetailnew()
        {
            string OrderID = Request["OrderID"].ToString();

            DataTable dt = CustomerServiceMan.GetOrdersDetailnew(OrderID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        public ActionResult SaveUpdateOrderInfonew(OrdersInfoNew orderinfo)
        {
            if (ModelState.IsValid)
            {
                //订单详细表
                List<Orders_DetailInfoNew> list = new List<Orders_DetailInfoNew>();
                string[] ProductID = Request["ProductID"].Split(',');
                string[] OrderContent = Request["OrderContent"].Split(',');
                string[] SpecsModels = Request["SpecsModels"].Split(',');
                string[] Supplier = Request["Supplier"].Split(',');
                string[] Unit = Request["Unit"].Split(',');
                string[] Amount = Request["Amount"].Split(',');
                string[] UnitPrice = Request["UnitPrice"].Split(',');
                string[] Subtotal = Request["Subtotal"].Split(',');
                string[] Technology = Request["Technology"].Split(',');
                string[] DeliveryTime = Request["DeliveryTime"].Split(',');
                string[] TaxRatestr = Request["TaxRate"].Split(',');
                // string[] DID = Request["DID"].Split(',');
                for (int i = 0; i < ProductID.Length; i++)
                {
                    if (ProductID[i] != "")
                    {
                        Orders_DetailInfoNew ordersDetail = new Orders_DetailInfoNew();
                        // ordersDetail.PID = orderinfo.PID;
                        ordersDetail.ProductID = ProductID[i].ToString();
                        ordersDetail.OrderID = orderinfo.OrderID;
                        ordersDetail.DID = "";// DID[i].ToString();
                        ordersDetail.OrderContent = OrderContent[i].ToString();
                        ordersDetail.SpecsModels = SpecsModels[i].ToString();
                        ordersDetail.Manufacturer = Supplier[i].ToString();
                        ordersDetail.OrderUnit = Unit[i].ToString();
                        if (!string.IsNullOrEmpty(UnitPrice[i].ToString())) { ordersDetail.Price = Convert.ToDecimal(UnitPrice[i]); }

                        if (string.IsNullOrEmpty(Amount[i]))
                        {
                            ordersDetail.OrderNum = 0;
                        }
                        else { ordersDetail.OrderNum = Convert.ToInt32(Amount[i]); }
                        if (!string.IsNullOrEmpty(Subtotal[i]))
                        {
                            ordersDetail.Subtotal = Convert.ToDecimal(Subtotal[i]);
                        }
                        ordersDetail.TaxRate = TaxRatestr[i].ToString();
                        ordersDetail.Technology = Technology[i].ToString();
                        if (DeliveryTime[i] != "")
                            ordersDetail.DeliveryTime = Convert.ToDateTime(DeliveryTime[i]);

                        ordersDetail.Validate = "v";
                        list.Add(ordersDetail);
                    }
                }
                string strErr = "";
                bool b = CustomerServiceMan.SaveUpdateOrderInfonew(orderinfo, list, ref strErr);
                if (b)
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改订单";
                    log.LogContent = "修改成功";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "OrdersInfo";
                    log.Typeid = orderinfo.OrderID;
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = true });
                }
                else
                {
                    #region [添加日志]
                    tk_CustomerServicelog log = new tk_CustomerServicelog();
                    log.LogTitle = "修改订单";
                    log.LogContent = "修改失败";
                    log.Person = GAccount.GetAccountInfo().UserName;
                    log.Tiem = DateTime.Now;
                    log.Type = "OrdersInfo";
                    log.Typeid = orderinfo.OrderID;
                    CustomerServiceMan.AddCustomerServiceLog(log);
                    #endregion
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { Sucess = "false", Msg = "验证不通过" });
            }
        }
        public ActionResult GetSearchOrderInfonew(tk_SalesGrid tk_salesgrid)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "9";
                string ContractID = tk_salesgrid.ContractID;
                string OrderUnit = tk_salesgrid.OrderUnit;
                string UseUnit = tk_salesgrid.UseUnit;
                string OrderContent = tk_salesgrid.MainContent;
                string StartDate = tk_salesgrid.StartDate;
                string EndDate = tk_salesgrid.EndDate;
                //string State = Request["State"].ToString();
                //string HState = Request["HState"];

                string s = "";
                if (!string.IsNullOrEmpty(ContractID))
                {
                    s += " and ContractID like '%" + ContractID + "%'";
                }
                if (!string.IsNullOrEmpty(OrderUnit))
                {
                    s += " and OrderUnit like '%" + OrderUnit + "%'";
                }
                if (!string.IsNullOrEmpty(UseUnit))
                {
                    s += " and UseUnit like '%" + UseUnit + "%'";
                }
                if (!string.IsNullOrEmpty(OrderContent))
                {
                    s += " and a.OrderID =(select OrderID from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderContent like '%" + OrderContent + "%')";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " and ContractDate between  '" + StartDate + "' and '" + EndDate + "'";
                }
                //if (!string.IsNullOrEmpty(State))
                //{
                //    if (State == "y")
                //    {
                //        //  s += " (a.State =1 or a.State =2 or a.State =3 ) and";
                //    }
                //    else
                //    {
                //        s += " and a.OState ='" + State + "'";
                //    }
                //}
                //if (!string.IsNullOrEmpty(HState))
                //{
                //    s += " and  IsHK ='" + HState + "' ";
                //}
                //if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
                //{
                //    //  s = s.Substring(0, s.Length - 3);
                //}
                if (!string.IsNullOrEmpty(s)) { where = " " + s; }
                UIDataTable udtTask = CustomerServiceMan.GetOrderInfonew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }
        public ActionResult PrintOrderInfonew()
        {
            OrdersInfo OrdersInfo = CustomerServiceMan.GetOrdersByOrderIDnew(Request["OrderID"].ToString());
            if (OrdersInfo.ContractID == "")
            {
                string Str = CustomerServiceMan.GetNamePYnew(GAccount.GetAccountInfo().UserName);
                string Dime = DateTime.Now.Year.ToString();// ("YYYY");
                Dime = Dime.Substring(2, 2);
                //string MaxContractID = SalesManage.GetMaxContractID();
                //MaxContractID = MaxContractID.Substring(MaxContractID.Length - 3);
                //MaxContractID = string.Format("{0:D3}", Convert.ToInt32(MaxContractID) + 1);
                OrdersInfo.ContractID = CustomerServiceMan.GetMaxContractID();
            }
            // return View(OrdersInfo);

            DataTable dt = CustomerServiceMan.GetOrdersDetailnew(OrdersInfo.OrderID);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>北京市燕山工业燃气设备有限公司</div><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>打印订货备忘录</div><div id='' style='margin-left:10px;font-size:16px;'>合同编号:" + OrdersInfo.ContractID + " &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;签订日期:" + OrdersInfo.ContractDate + "  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;YZJL-XS-D02</div><table class='tabInfo' cellpadding='0' ><tr><td>订单单位:</td><td>" + OrdersInfo.OrderUnit + " </td><td>联系人:</td><td>" + OrdersInfo.OrderContactor + "</td><td>联系电话:</td><td>" + OrdersInfo.OrderTel + "</td><td>地址:</td><td>" + OrdersInfo.OrderAddress + "</td></tr><tr><td>使用单位:</td><td>" + OrdersInfo.UseUnit + " </td><td>联系人:</td><td> " + OrdersInfo.UseContactor + " </td><td>联系电话:</td><td> " + OrdersInfo.UseTel + "</td><td> 地址:</td><td>" + OrdersInfo.UseAddress + "</td></tr></table>");

            sb2.Append("<div><table id='Botom' class=' tabInfo'><tr> <td>合计:人民币(大写)</td> <td colspan='6'>" + OrdersInfo.Total + "</td></tr><tr><td>付款方式</td><td colspan='6'>" + OrdersInfo.PayWay + "</td></tr><tr><td>产品保修期</td><td colspan='6'>" + OrdersInfo.Guarantee + "</td></tr><tr><td rowspan='2'>供方</td><td>单位</td><td>" + OrdersInfo.Provider + "</td><td rowspan='2'> 需方 </td><td>单位</td><td>" + OrdersInfo.Demand + "</td></tr><tr><td>负责人：</td><td> " + OrdersInfo.ProvidManager + "</td><td>负责人：</td><td>" + OrdersInfo.DemandManager + "</td></tr><tr><td>备注：</td><td colspan='6'>" + OrdersInfo.Remark + "</td></tr><tr><td>业务渠道：</td><td colspan='6'>" + OrdersInfo.ChannelsFrom + "</td></tr>  <tr style='height: 30px;'><td style='text-align:left;' colspan='7'>联系电话: </td></tr><tr><td style='text-align:left;' colspan='7'>签字: </td></tr><tr><td style='text-align:left;' colspan='7'>日期: </td></tr></table></div></div>");
            if (dt.Rows.Count <= 6)
            {
                sb1.Append("<div><table id='myTable' cellpadding=' 0' cellspacing='0' class='tabInfo2' ><tr style='background-color: #88c9e9;' align='center' valign='middle'><td style='width: 200px;'>序号</td><td style='width: 200px;'>物品编号</td><td style='width: 200px;'>物品名称</td><td style='width: 200px;'>规格型号</td><td style='width: 200px;'>单位</td><td style='width: 200px;'>数量</td><td style='width: 200px;'>供应商</td><td style='width:200px;'>单价</td><td style='width: 200px;'>小计</td><td style='width: 200px;'>技术要求或参数</td><td style='width: 200px;'>交货时间</td></tr><tbody id='DetailInfo'>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb1.Append("<tr  id ='DetailInfo" + i + "' onclick='selRow(this)'><td ><lable class='labRowNumber" + i + " ' id='RowNumber" + i + "'>" + (i + 1) + "</lable> </td><td ><lable class='labProductID" + i + " ' id='ProductID" + i + "'>" + dt.Rows[i]["ProductID"] + " </lable> </td><td ><lable class='labProName" + i + " ' id='ProName" + i + "'>" + dt.Rows[i]["OrderContent"] + "</lable> </td><td ><lable class='labSpec" + i + " ' id='Spec" + i + "'>" + dt.Rows[i]["SpecsModels"] + "</lable> </td><td ><lable class='labUnits" + i + " ' id='Units" + i + "'>" + dt.Rows[i]["OrderUnit"] + "</lable> </td><td >" + dt.Rows[i]["OrderNum"] + "</td><td >" + dt.Rows[i]["Manufacturer"] + "</td><td >" + dt.Rows[i]["Price"] + " </td><td >" + dt.Rows[i]["Subtotal"] + "</td><td >" + dt.Rows[i]["Technology"] + "</td><td >" + dt.Rows[i]["DeliveryTime"] + "</td><td style='display:none;'><lable class='labPID" + i + " ' id='DID" + i + "'>" + dt.Rows[i]["DID"] + "</lable> </td></tr>");
                }
                sb1.Append("</tbody></table></div>");
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = dt.Rows.Count % 6;
                if (count > 0)
                    count = dt.Rows.Count / 6 + 1;
                else
                    count = dt.Rows.Count / 6;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int a = 6 * i;
                    int length = 6 * (i + 1);
                    if (length > dt.Rows.Count)
                        length = 6 * i + dt.Rows.Count % 6;
                    sb1.Append("<div><table id='myTable' cellpadding=' 0' cellspacing='0' class='tabInfo' ><tr style='background-color: #88c9e9;' align='center' valign='middle'><th style='width: 200px;' class='th'>    序号</th><th style='width: 200px;' class='th'>    物品编号</th><th style='width: 200px;' class='th'>    物品名称</th><th style='width: 200px;' class='th'>    规格型号</th><th style='width: 200px;' class='th'>    单位</th><th style='width: 200px;' class='th'>    数量</th><th style='width: 200px;' class='th'>供应商</th><th style='width:200px;' class='th'>    单价</th><th style='width: 200px;' class='th'>    小计</th><th style='width: 200px;' class='th'>    技术要求或参数</th><th style='width: 200px;' class='th'>    交货时间</th></tr><tbody id='DetailInfo'>");
                    for (int j = a; j < length; j++)
                    {
                        sb1.Append("<tr  id ='DetailInfo" + j + "' onclick='selRow(this)'><td ><lable class='labRowNumber" + j + " ' id='RowNumber" + j + "'>" + (j + 1) + "</lable> </td><td ><lable class='labProductID" + j + " ' id='ProductID" + j + "'>" + dt.Rows[j]["ProductID"] + " </lable> </td><td ><lable class='labProName" + j + " ' id='ProName" + j + "'>" + dt.Rows[j]["OrderContent"] + "</lable> </td><td ><lable class='labSpec" + j + " ' id='Spec" + j + "'>" + dt.Rows[j]["SpecsModels"] + "</lable> </td><td ><lable class='labUnits" + j + " ' id='Units" + j + "'>" + dt.Rows[j]["OrderUnit"] + "</lable> </td><td >" + dt.Rows[j]["OrderNum"] + "</td><td >" + dt.Rows[j]["Manufacturer"] + "</td><td >" + dt.Rows[j]["Price"] + "</td><td >" + dt.Rows[j]["Subtotal"] + "</td><td >" + dt.Rows[j]["Technology"] + "</td><td >" + dt.Rows[j]["DeliveryTime"] + " </td><td style='display:none;'><lable class='labPID" + j + " ' id='DID" + j + "'>" + dt.Rows[j]["DID"] + "</lable> </td></tr>");
                    }
                    if ((length - a) < 6)
                    {
                    }
                    sb1.Append("</tbody></table></div>");

                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }

            Response.Write(html);
            return View();
        }
        public FileResult OrderInfoToExcelnew()
        {
            string ContractID = Request["ContractID"];
            string OrderUnit = Request["OrderUnit"];
            string UseUnit = Request["UseUnit"];
            string OrderContent = Request["OrderContent"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            //string State = Request["State"];
            //string HState = Request["HState"];
            string where = " ";
            string s = " a.Validate ='v' and";
            if (!string.IsNullOrEmpty(ContractID))
            {
                s += " ContractID like '%" + ContractID + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderUnit))
            {
                s += " OrderUnit like '%" + OrderUnit + "%'  and";
            }
            if (!string.IsNullOrEmpty(UseUnit))
            {
                s += " UseUnit like '%" + UseUnit + "%' and";
            }
            if (!string.IsNullOrEmpty(OrderContent))
            {
                //s += " OrderContent like '%" + OrderContent + "%' and";
                s += " and a.OrderID =(select OrderID from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderContent like '%" + OrderContent + "%')";
            }
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                s += " OfferTime between  '" + StartDate + "' and '" + EndDate + "' and";
            }
            //if (!string.IsNullOrEmpty(State))
            //{
            //    if (State == "y")
            //    {
            //        //s += " (a.State=0 or a.State =1 or a.State =2 or a.State =3 ) and";
            //    }
            //    else
            //    {
            //        s += " a.State =" + State + " and";
            //    }
            //}
            //if (!string.IsNullOrEmpty(HState))
            //{
            //    s += " IsPay =" + HState + " ";
            //}
            if (!string.IsNullOrEmpty(s))//&& string.IsNullOrEmpty(HState)
            {
                s = s.Substring(0, s.Length - 3);
            }
            if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
            string strErr = "";
            DataTable data = CustomerServiceMan.GetOrderInfoToExcelnew(where, ref strErr);
            if (data != null)
            {
                string strCols = "项目编号-5000,订单编号-5000,合同编号-5000,订货单位-5000,订货人-5000,使用单位-5000,使用人-5000,使用联系电话-5000,使用地址-5000,是否付款-3000," +
               " 状态-5000,保修期-5000,供方单位-5000,供方负责人-5000,需方单位-5000,需方负责人-5000,备注-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "订单信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "订单信息表.xls");
            }
            else
                return null;
        }
        #region [供应商]
        public ActionResult GetSupplier()
        {
            return View();
        }
        public ActionResult GetSupplierCot()
        {
            string SID = Request["SID"].ToString();
            string ProduID = Request["ProduID"];
            DataTable dt = CustomerServiceMan.GetSupplierCot(SID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetCheckSupListOld()
        {

            string where = Request["ptype"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";

            UIDataTable udtTask = CustomerServiceMan.GetCheckSupListOld(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupTypeOld()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = CustomerServiceMan.GetSupTypeOld(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region [开票记录]
        public ActionResult BillingRecords()
        {
            ViewData["webkey"] = "售后开票记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后开票记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        public ActionResult AddBillingRecords()
        {
            tk_BillingRecords so = new TECOCITY_BGOI.tk_BillingRecords();
            so.BRDID = CustomerServiceMan.GetTopBRDID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        //保存开票记录
        public ActionResult SaveBillingRecords()
        {
            if (ModelState.IsValid)
            {
                string strErr = "";
                string type = Request["type"].ToString();
                tk_BillingRecords card = new tk_BillingRecords();
                card.BRDID = Request["BRDID"].ToString();
                card.DwName = Request["DwName"].ToString();
                card.Amount = Convert.ToInt32(Request["Amount"]);
                card.Project = Request["Project"].ToString();
                card.PaymentMethod = Request["PaymentMethod"].ToString();
                card.PersonCharge = Request["PersonCharge"].ToString();
                card.BRDTime = Convert.ToDateTime(Request["BRDTime"].ToString());
                card.ReceivablesTime = Convert.ToDateTime(Request["ReceivablesTime"].ToString());
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                card.State = "0";
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SaveBillingRecords(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加开票记录";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_BillingRecords";
                        log.Typeid = Request["BRDID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加开票记录";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_BillingRecords";
                        log.Typeid = Request["BRDID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SaveBillingRecords(card, type, ref strErr);
                    if (b)
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改开票记录";
                        log.LogContent = "修改成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_BillingRecords";
                        log.Typeid = Request["BRDID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {
                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改开票记录";
                        log.LogContent = "修改失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_BillingRecords";
                        log.Typeid = Request["BRDID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }

            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }

        }
        public ActionResult BillingRecordsList(tk_BillingRecords waquery)
        {
            if (ModelState.IsValid)
            {
                string where = " a.Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string PersonCharge = waquery.PersonCharge;// Request["PersonCharge"].ToString().Trim();
                string DwName = waquery.DwName;// Request["DwName"].ToString().Trim();
                string Begin = Request["Begin"].ToString();
                if (Begin != "")
                    Begin += " 00:00:00";
                string End = Request["End"].ToString();
                if (End != "")
                    End += " 23:59:59";
                if (Request["PersonCharge"].ToString().Trim() != "")
                    where += " a.PersonCharge like '%" + Request["PersonCharge"].ToString().Trim() + "%' and";
                if (Request["DwName"].ToString().Trim() != "")
                    where += " a.DwName like '%" + Request["DwName"].ToString().Trim() + "%' and";
                if (Begin != "" && End != "")
                    where += " a.ReceivablesTime between '" + Begin + "' and '" + End + "' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.BillingRecordsList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //撤销
        public ActionResult DeBillingRecords()
        {
            string strErr = "";
            string BRDID = Request["BRDID"].ToString();

            if (CustomerServiceMan.DeBillingRecords(BRDID, ref strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销开票记录";
                log.LogContent = "撤销成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_BillingRecords";
                log.Typeid = Request["BRDID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "撤销开票记录";
                log.LogContent = "撤销失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_BillingRecords";
                log.Typeid = Request["BRDID"].ToString();
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult UpBillingRecords()
        {
            tk_BillingRecords so = new TECOCITY_BGOI.tk_BillingRecords();
            so.BRDID = Request["BRDID"].ToString();
            DataTable dt = CustomerServiceMan.UpBillingRecords(so.BRDID);
            if (dt != null && dt.Rows.Count > 0)
            {
                so.BRDTime = Convert.ToDateTime(dt.Rows[0][1].ToString());
                so.DwName = dt.Rows[0][2].ToString();
                so.Project = dt.Rows[0][3].ToString();
                so.Amount = Convert.ToInt32(dt.Rows[0][4]);
                so.PersonCharge = dt.Rows[0][5].ToString();
                so.ReceivablesTime = Convert.ToDateTime(dt.Rows[0][6].ToString());
                so.PaymentMethod = dt.Rows[0][7].ToString();
                so.Remark = dt.Rows[0][8].ToString();
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }
        public ActionResult UpUpUpBillingRecords()
        {
            string BRDID = Request["BRDID"].ToString();
            DataTable dt = CustomerServiceMan.UpUpUpBillingRecords(BRDID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        //加载打印数据
        public ActionResult PrintBillingRecords()
        {
            string BRDID = Request["Info"].Replace("'", "").ToString().Trim();
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(BRDID))
            {
                s += " BRDID like '%" + BRDID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_CustomerService.dbo.tk_BillingRecords ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            tk_BillingRecords so = new TECOCITY_BGOI.tk_BillingRecords();
            foreach (DataRow dt in data.Rows)
            {
                so.BRDID = dt["BRDID"].ToString();
                // so.BRDTime = Convert.ToDateTime(dt["BRDTime"]);
                ViewData["BRDTime"] = Convert.ToDateTime(dt["BRDTime"]).ToString("yyy/MM/dd");
                so.DwName = dt["DwName"].ToString();
                so.Project = dt["Project"].ToString();
                so.Amount = Convert.ToInt32(dt["Amount"]);
                so.PaymentMethod = dt["PaymentMethod"].ToString();
                so.PersonCharge = dt["PersonCharge"].ToString();
                // so.ReceivablesTime = Convert.ToDateTime(dt["ReceivablesTime"]);
                ViewData["ReceivablesTime"] = Convert.ToDateTime(dt["ReceivablesTime"]).ToString("yyy/MM/dd");

                so.Remark = dt["Remark"].ToString();
                so.CreateUser = GAccount.GetAccountInfo().UserName;
            }
            return View(so);
        }
        //导出
        public FileResult BillingRecordsToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string PersonCharge = Request["PersonCharge"].ToString().Trim();
            string DwName = Request["DwName"].ToString().Trim();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (PersonCharge != "")
                where += " PersonCharge like '%" + PersonCharge + "%' and";
            if (DwName != "")
                where += " DwName like '%" + DwName + "%' and";
            if (Begin != "" && End != "")
                where += " ReceivablesTime between '" + Begin + "' and '" + End + "' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_BillingRecords ";
            string FieldName = " Convert(varchar(100),BRDTime,23) as BRDTime, DwName, Project, Amount, PersonCharge,Convert(varchar(100),ReceivablesTime,23) as ReceivablesTime, PaymentMethod, Remark  ";
            string OrderBy = " CreateTime  desc";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "时间-6000,单位名称-6000,项目-6000,金额-5000,负责人-6000,收款日期-5000,支付方式-3000,备注-3000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "开票记录信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "开票记录信息表.xls");
            }
            else
                return null;


        }
        public ActionResult GetKPJL()
        {
            string BRDID = Request["BRDID"].ToString();
            if (CustomerServiceMan.GetKPJL(BRDID).Rows.Count > 0)
            {
                return Json(new { success = "true", Msg = "已开票" });
            }
            else
            {
                return Json(new { success = "false", Msg = "未开票" });
            }
        }
        public ActionResult GetPDSP()
        {
            string BRDID = Request["BRDID"].ToString();
            if (CustomerServiceMan.GetPDSP(BRDID).Rows[0][12].ToString() == "2")
            {
                return Json(new { success = "true", Msg = "已审批" });
            }
            else if (CustomerServiceMan.GetPDSP(BRDID).Rows[0][12].ToString() == "1")
            {
                return Json(new { success = "false", Msg = "待审批" });
            }
            else
            {
                return Json(new { success = "false", Msg = "未提交审批" });
            }
        }
        public ActionResult GetBasBillingRecords()
        {
            string BRDID = Request["BRDID"].ToString();
            DataTable dt = CustomerServiceMan.GetBasBillingRecords(BRDID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult KPProcessing()
        {
            ViewData["webkey"] = "售后开票记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后开票记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }
        public ActionResult UserKPProcessing()
        {
            string where = " 1=1 and a.ValiDate = 'v' and a.state != -1 and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string HandleUser = Request["HandleUser"].ToString().Trim();
            if (HandleUser != "")
            {
                where += " a.HandleUser like '%" + HandleUser + "%' and";
            }

            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserKPProcessing(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion
        #region [统计详情]

        #region [用户服务统计]
        public ActionResult UserServiceStatistics()
        {
            return View();
        }
        #endregion
        #region [报装统计]
        public ActionResult InstallStatistics()
        {
            return View();
        }
        public ActionResult InstallStatisticsList(InstallStatisticsQuery que)
        {
            if (ModelState.IsValid)
            {
                //h.ValiDate='v' and g.ValiDate='v' 
                string where = " and a.ValiDate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string start = Request["start"].ToString();
                string end = Request["end"].ToString();
                string Tel = que.Tel;// Request["Tel"].ToString().Trim();
                string CustomerName = que.CustomerName;// Request["CustomerName"].ToString().Trim();
                if (Request["CustomerName"].ToString().Trim() != "")
                    where += " a.CustomerName like '%" + Request["CustomerName"].ToString().Trim() + "%' and";
                if (Request["Tel"].ToString().Trim() != "")
                    where += " a.Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
                if (start != "" && end != "")
                    where += " (e.InstallTime between '" + start + "' and  '" + end + "') and";
                where = where.Substring(0, where.Length - 3);
                DataTable dt = CustomerServiceMan.InstallStatisticsList(where);

                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //加载打印数据
        public ActionResult PrintInstallStatistics()
        {
            string where = " and a.ValiDate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            if (Request["CustomerName"].ToString().Trim() != "")
                where += " a.CustomerName like '%" + Request["CustomerName"].ToString().Trim() + "%' and";
            if (Request["Tel"].ToString().Trim() != "")
                where += " a.Tel like '%" + Request["Tel"].ToString().Trim() + "%' and";
            //if (start != "" && end != "")
            where += " (e.InstallTime between '" + start + "' and  '" + end + "') and";
            where = where.Substring(0, where.Length - 3);
            DataTable a = CustomerServiceMan.InstallStatisticsList(where);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>报装统计");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='23'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='23'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='23'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 10)
            {
                #region [表头]
                //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                //sb1.Append("<tr>");
                //sb1.Append("<td>");
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 3%;'>月份</td>");
                sb1.Append("<td style='width: 3%;'>类别</td>");
                sb1.Append("<td style='width: 3%;'>姓名</td>");
                sb1.Append("<td style='width: 3%;'>型号</td>");
                sb1.Append("<td style='width: 3%;'>数量</td>");
                sb1.Append("<td style='width: 3%;'>价格</td>");
                sb1.Append("<td style='width: 5%;'>报装时间</td>");
                sb1.Append("<td style='width: 5%;'>安装时间</td>");
                sb1.Append("<td style='width: 5%;'>安装人员</td>");
                sb1.Append("<td style='width: 5%;'>联系方式</td>");
                sb1.Append("<td style='width: 3%;'>地址</td>");
                sb1.Append("<td style='width: 5%;'>发票/收据</td>");
                sb1.Append("<td style='width: 3%;'>销售渠道</td>");

                sb1.Append("<td style='width: 5%;'>分公司</td>");
                sb1.Append("<td style='width: 3%;'>备注</td>");
                sb1.Append("<td style='width: 5%;'>确认客户满意度</td>");
                sb1.Append("<td style='width: 5%;'>是否向用户说明包装内所含物品</td>");
                sb1.Append("<td style='width: 5%;'>工作服</td>");
                sb1.Append("<td style='width: 5%;'>是否指导用户使用及指导事项</td>");
                sb1.Append("<td style='width: 5%;'>是否接收用户赠与的物品</td>");
                sb1.Append("<td style='width: 5%;'>是否收费</td>");
                sb1.Append("<td style='width: 5%;'>工作完成后是否做好清洁工作</td>");
                sb1.Append("<td style='width: 10%;'>客户是否阅读安装单并签字</td>");
                sb1.Append("</tr>");
                //sb1.Append("<tbody id='DetailInfo' class='tabInfo2'>");
                //sb1.Append("</tbody>");
                //sb1.Append("</table>");
                //sb1.Append("</td>");
                //sb.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容]
                    sb1.Append("<tr id ='DetailInfo" + i + "'>");
                    sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[i]["y"] + "</lable></td>");
                    sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["lb"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["khxm"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["xh"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["sl"] + "</lable></td>");
                    sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["dj"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["bzsj"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["azsj"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["azry"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["khdh"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["khdz"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[i]["fpsj"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[i]["xxsqd"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[i]["fgs"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[i]["bz"] + "</lable></td>");
                    sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[i]["qrkhmyd"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfxyhsmbnshwp" + i + "' id='sfxyhsmbnshwp" + i + "'>" + a.Rows[i]["sfxyhsmbnshwp"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfcgzf" + i + "' id='sfcgzf" + i + "'>" + a.Rows[i]["sfcgzf"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfzdyhsyjzdsx" + i + "' id='sfzdyhsyjzdsx" + i + "'>" + a.Rows[i]["sfzdyhsyjzdsx"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfjsyhzydwp" + i + "' id='sfjsyhzydwp" + i + "'>" + a.Rows[i]["sfjsyhzydwp"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfsf" + i + "' id='sfsf" + i + "'>" + a.Rows[i]["sfsf"] + "</lable></td>");
                    sb1.Append("<td><lable class='labgzwchsfzhqjgz" + i + "' id='gzwchsfzhqjgz" + i + "'>" + a.Rows[i]["gzwchsfzhqjgz"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhsfydazdbqz" + i + "' id='khsfydazdbqz" + i + "'>" + a.Rows[i]["khsfydazdbqz"] + "</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                }
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 10;
                if (count > 0)
                    count = a.Rows.Count / 10 + 1;
                else
                    count = a.Rows.Count / 10;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 10 * i;
                    int length = 10 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 10 * i + a.Rows.Count % 10;
                    #region [表头]
                    //sb1.Append("<table id='list' class='tabInfo2' style='width: 100%;'>");
                    //sb1.Append("<tr>");
                    //sb1.Append("<td>");
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 3%;'>月份</td>");
                    sb1.Append("<td style='width: 3%;'>类别</td>");
                    sb1.Append("<td style='width: 3%;'>姓名</td>");
                    sb1.Append("<td style='width: 3%;'>型号</td>");
                    sb1.Append("<td style='width: 3%;'>数量</td>");
                    sb1.Append("<td style='width: 3%;'>价格</td>");
                    sb1.Append("<td style='width: 5%;'>报装时间</td>");
                    sb1.Append("<td style='width: 5%;'>安装时间</td>");
                    sb1.Append("<td style='width: 5%;'>安装人员</td>");
                    sb1.Append("<td style='width: 5%;'>联系方式</td>");
                    sb1.Append("<td style='width: 3%;'>地址</td>");
                    sb1.Append("<td style='width: 5%;'>发票/收据</td>");
                    sb1.Append("<td style='width: 3%;'>销售渠道</td>");

                    sb1.Append("<td style='width: 5%;'>分公司</td>");
                    sb1.Append("<td style='width: 3%;'>备注</td>");
                    sb1.Append("<td style='width: 5%;'>确认客户满意度</td>");
                    sb1.Append("<td style='width: 5%;'>是否向用户说明包装内所含物品</td>");
                    sb1.Append("<td style='width: 5%;'>工作服</td>");
                    sb1.Append("<td style='width: 5%;'>是否指导用户使用及指导事项</td>");
                    sb1.Append("<td style='width: 5%;'>是否接收用户赠与的物品</td>");
                    sb1.Append("<td style='width: 5%;'>是否收费</td>");
                    sb1.Append("<td style='width: 5%;'>工作完成后是否做好清洁工作</td>");
                    sb1.Append("<td style='width: 10%;'>客户是否阅读安装单并签字</td>");
                    sb1.Append("</tr>");
                    //sb1.Append("<tbody id='DetailInfo' class='tabInfo2'>");
                    //sb1.Append("</tbody>");
                    //sb1.Append("</table>");
                    //sb1.Append("</td>");
                    //sb.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容]
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[j]["y"] + "</lable></td>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["lb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["khxm"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["xh"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["sl"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["dj"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["bzsj"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["azsj"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["azry"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["khdh"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["khdz"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[j]["fpsj"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[j]["xxsqd"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[j]["fgs"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[j]["bz"] + "</lable></td>");
                        sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[j]["qrkhmyd"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfxyhsmbnshwp" + i + "' id='sfxyhsmbnshwp" + i + "'>" + a.Rows[j]["sfxyhsmbnshwp"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfcgzf" + i + "' id='sfcgzf" + i + "'>" + a.Rows[j]["sfcgzf"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfzdyhsyjzdsx" + i + "' id='sfzdyhsyjzdsx" + i + "'>" + a.Rows[j]["sfzdyhsyjzdsx"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfjsyhzydwp" + i + "' id='sfjsyhzydwp" + i + "'>" + a.Rows[j]["sfjsyhzydwp"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfsf" + i + "' id='sfsf" + i + "'>" + a.Rows[j]["sfsf"] + "</lable></td>");
                        sb1.Append("<td><lable class='labgzwchsfzhqjgz" + i + "' id='gzwchsfzhqjgz" + i + "'>" + a.Rows[j]["gzwchsfzhqjgz"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhsfydazdbqz" + i + "' id='khsfydazdbqz" + i + "'>" + a.Rows[j]["khsfydazdbqz"] + "</lable></td>");
                        sb1.Append("</tr>");
                        #endregion
                    }
                    if ((length - b) < 10)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }
        #endregion
        #region [客户满意度]
        public ActionResult SatisfactionStatistics()
        {
            return View();
        }
        public ActionResult SatisfactionStatisticsList(InstallStatisticsQuery que)
        {
            if (ModelState.IsValid)
            {
                string where = " where ValiDate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string start = Request["start"].ToString();
                string end = Request["end"].ToString();
                if (start != "" && end != "")
                    where += "   SurveyDate between '" + start + "' and  '" + end + "' and";
                where = where.Substring(0, where.Length - 3);
                DataTable dt = CustomerServiceMan.SatisfactionStatisticsList(where);
                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //加载打印数据
        public ActionResult PrintSatisfactionStatistics()
        {
            string where = " where ValiDate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            //if (start != "" && end != "")
            where += "   SurveyDate between '" + start + "' and  '" + end + "' and";
            where = where.Substring(0, where.Length - 3);
            DataTable a = CustomerServiceMan.SatisfactionStatisticsList(where);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>用客满意度调查统计表");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='6'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='6'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='6'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 10)
            {
                #region [表头]
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 20%;'></td>");
                sb1.Append("<td style='width: 20%;'></td>");
                sb1.Append("<td style='width: 15%;'>非常满意</td>");
                sb1.Append("<td style='width: 15%;'>满意</td>");
                sb1.Append("<td style='width: 15%;'>一般</td>");
                sb1.Append("<td style='width: 15%;'>不满意</td>");
                sb1.Append("</tr>");
                #endregion
                #region [内容]
                #region [产品]
                sb1.Append("<tr id ='DetailInfo'>");
                sb1.Append("<td rowspan='4'><lable class='cp' id='cp'>产品</lable></td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容0-2]
                    if (i == 0)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 1)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 2)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    #endregion
                }
                #region [服务]
                sb1.Append("<tr id ='DetailInfo'>");
                sb1.Append("<td rowspan='4'><lable class='fw' id='fw'>服务</lable></td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容3-5]
                    if (i == 3)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 4)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 5)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    #endregion
                }
                #region [代理]
                sb1.Append("<tr id ='DetailInfo'>");
                sb1.Append("<td rowspan='4'><lable class='dl' id='dl'>代理</lable></td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容6-8]
                    if (i == 6)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 7)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    if (i == 8)
                    {
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["mc"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["cpzlfcmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["cpzlmy"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["cpzlyb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["cpzlbmy"] + "</lable></td>");
                        sb1.Append("</tr>");
                    }
                    #endregion
                }
                #endregion
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 10;
                if (count > 0)
                    count = a.Rows.Count / 10 + 1;
                else
                    count = a.Rows.Count / 10;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 10 * i;
                    int length = 10 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 10 * i + a.Rows.Count % 10;
                    #region [表头]
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 20%;'></td>");
                    sb1.Append("<td style='width: 20%;'></td>");
                    sb1.Append("<td style='width: 15%;'>非常满意</td>");
                    sb1.Append("<td style='width: 15%;'>满意</td>");
                    sb1.Append("<td style='width: 15%;'>一般</td>");
                    sb1.Append("<td style='width: 15%;'>不满意</td>");
                    sb1.Append("</tr>");
                    #endregion
                    #region [产品]
                    sb1.Append("<tr id ='DetailInfo'>");
                    sb1.Append("<td rowspan='4'><lable class='cp' id='cp'>产品</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容0-2]
                        if (i == 0)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 1)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 2)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        #endregion
                    }
                    #region [服务]
                    sb1.Append("<tr id ='DetailInfo'>");
                    sb1.Append("<td rowspan='4'><lable class='fw' id='fw'>服务</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容3-5]
                        if (i == 3)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 4)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 5)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        #endregion
                    }
                    #region [代理]
                    sb1.Append("<tr id ='DetailInfo'>");
                    sb1.Append("<td rowspan='4'><lable class='dl' id='dl'>代理</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容6-8]
                        if (i == 6)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 7)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        if (i == 8)
                        {
                            sb1.Append("<tr id ='DetailInfo" + i + "'>");
                            sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["mc"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["cpzlfcmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["cpzlmy"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["cpzlyb"] + "</lable></td>");
                            sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["cpzlbmy"] + "</lable></td>");
                            sb1.Append("</tr>");
                        }
                        #endregion
                    }
                    if ((length - b) < 10)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }


        #endregion
        #region [维修任务统计]
        public ActionResult MaintenanceTaskStatistics()
        {
            return View();
        }
        public ActionResult MaintenanceTaskStatisticsList(InstallStatisticsQuery que)
        {
            if (ModelState.IsValid)
            {
                string where = " where a.ValiDate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string start = Request["start"].ToString();
                string end = Request["end"].ToString();
                if (start != "" && end != "")
                    where += " (a.CreateTime between '" + start + "' and  '" + end + "') and";
                where = where.Substring(0, where.Length - 3);
                DataTable dt = CustomerServiceMan.MaintenanceTaskStatisticsList(where);
                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }

        //加载打印数据
        public ActionResult PrintMaintenanceTaskStatistics()
        {
            string where = " where a.ValiDate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            //if (start != "" && end != "")
            where += " (a.CreateTime between '" + start + "' and  '" + end + "') and";
            where = where.Substring(0, where.Length - 3);
            DataTable a = CustomerServiceMan.MaintenanceTaskStatisticsList(where);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>维修任务统计");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='18'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='18'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='18'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 10)
            {
                #region [表头]
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 3%;'>日期</td>");
                sb1.Append("<td style='width: 3%;'>用户名称</td>");
                sb1.Append("<td style='width: 3%;'>联系人</td>");
                sb1.Append("<td style='width: 3%;'>用户地址</td>");
                sb1.Append("<td style='width: 3%;'>电话</td>");
                sb1.Append("<td style='width: 3%;'>设备型号</td>");
                sb1.Append("<td style='width: 5%;'>设备编号</td>");
                sb1.Append("<td style='width: 5%;'>启用日期</td>");
                sb1.Append("<td style='width: 5%;'>保修期</td>");
                sb1.Append("<td style='width: 5%;'>保修日期</td>");
                sb1.Append("<td style='width: 3%;'>维修日期</td>");
                sb1.Append("<td style='width: 5%;'>保修卡编号</td>");
                sb1.Append("<td style='width: 3%;'>维修车辆及人员</td>");
                sb1.Append("<td style='width: 5%;'>用户保修简述</td>");
                sb1.Append("<td style='width: 3%;'>维修情况记录</td>");
                sb1.Append("<td style='width: 5%;'>更换零件</td>");
                sb1.Append("<td style='width: 5%;'>维修成本</td>");
                sb1.Append("<td style='width: 5%;'>备注</td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容]
                    sb1.Append("<tr id ='DetailInfo" + i + "'>");
                    sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[i]["CreateTime"] + "</lable></td>");
                    sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["Customer"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["ContactName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["Address"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["Tel"] + "</lable></td>");
                    sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["DeviceType"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["DeviceID"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["EnableDate"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["GuaranteePeriod"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["RepairDate"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["MaintenanceTime"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[i]["BXKNum"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[i]["MaintenanceName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[i]["RepairTheUser"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[i]["MaintenanceRecord"] + "</lable></td>");
                    sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfxyhsmbnshwp" + i + "' id='sfxyhsmbnshwp" + i + "'>" + a.Rows[i]["Total"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsfcgzf" + i + "' id='sfcgzf" + i + "'>" + a.Rows[i]["Remark"] + "</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                }
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 10;
                if (count > 0)
                    count = a.Rows.Count / 10 + 1;
                else
                    count = a.Rows.Count / 10;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 10 * i;
                    int length = 10 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 10 * i + a.Rows.Count % 10;
                    #region [表头]
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 3%;'>日期</td>");
                    sb1.Append("<td style='width: 3%;'>用户名称</td>");
                    sb1.Append("<td style='width: 3%;'>联系人</td>");
                    sb1.Append("<td style='width: 3%;'>用户地址</td>");
                    sb1.Append("<td style='width: 3%;'>电话</td>");
                    sb1.Append("<td style='width: 3%;'>设备型号</td>");
                    sb1.Append("<td style='width: 5%;'>设备编号</td>");
                    sb1.Append("<td style='width: 5%;'>启用日期</td>");
                    sb1.Append("<td style='width: 5%;'>保修期</td>");
                    sb1.Append("<td style='width: 5%;'>保修日期</td>");
                    sb1.Append("<td style='width: 3%;'>维修日期</td>");
                    sb1.Append("<td style='width: 5%;'>保修卡编号</td>");
                    sb1.Append("<td style='width: 3%;'>维修车辆及人员</td>");
                    sb1.Append("<td style='width: 5%;'>用户保修简述</td>");
                    sb1.Append("<td style='width: 3%;'>维修情况记录</td>");
                    sb1.Append("<td style='width: 5%;'>更换零件</td>");
                    sb1.Append("<td style='width: 5%;'>维修成本</td>");
                    sb1.Append("<td style='width: 5%;'>备注</td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容]
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[j]["CreateTime"] + "</lable></td>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["Customer"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["ContactName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["Address"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["Tel"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["DeviceType"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["DeviceID"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["EnableDate"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["GuaranteePeriod"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["RepairDate"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["MaintenanceTime"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[j]["BXKNum"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[j]["MaintenanceName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[j]["RepairTheUser"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[j]["MaintenanceRecord"] + "</lable></td>");
                        sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[j]["ProName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfxyhsmbnshwp" + i + "' id='sfxyhsmbnshwp" + i + "'>" + a.Rows[j]["Total"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsfcgzf" + i + "' id='sfcgzf" + i + "'>" + a.Rows[j]["Remark"] + "</lable></td>");
                        sb1.Append("</tr>");
                        #endregion
                    }
                    if ((length - b) < 10)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }
        #endregion
        #region [设备调试统计表]
        public ActionResult TestingOfEquipmentStatistics()
        {
            return View();
        }
        public ActionResult TestingOfEquipmentStatisticsList(InstallStatisticsQuery que)
        {
            if (ModelState.IsValid)
            {
                string where = " where a.ValiDate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string start = Request["start"].ToString();
                string end = Request["end"].ToString();
                if (start != "" && end != "")
                    where += " (a.DebTime between '" + start + "' and  '" + end + "') and";
                where = where.Substring(0, where.Length - 3);
                DataTable dt = CustomerServiceMan.TestingOfEquipmentStatisticsList(where);
                if (dt == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        //计算总数
        public ActionResult GetTestingOfEquipmentStatistics()
        {
            string where = " where a.Validate='v' and";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            // if (start != "" && end != "")
            where += " (a.DebTime between '" + start + "' and  '" + end + "') and";
            where = where.Substring(0, where.Length - 3);
            DataTable dt = CustomerServiceMan.GetTestingOfEquipmentStatistics(where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult PrintTestingOfEquipmentStatistics()
        {
            string where = " where a.ValiDate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string start = Request["start"].ToString();
            string end = Request["end"].ToString();
            //if (start != "" && end != "")
            where += " (a.DebTime between '" + start + "' and  '" + end + "') and";
            where = where.Substring(0, where.Length - 3);
            DataTable a = CustomerServiceMan.TestingOfEquipmentStatisticsList(where);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string html = "";
            #region [开头]
            sb.Append("<div id='ReportContent' style='margin-top: 10px; page-break-after: always;'>");
            sb.Append("<div style='width: 100%; text-align: center; font-size: 18px;'>北京市燕山工业燃气设备有限公司");
            sb.Append("</div>");
            sb.Append("<div style='font-size: 22px; font-weight: bold; float: left; margin-left: 40%;'>设备调试统计表");
            sb.Append("</div>");
            #endregion
            #region [表尾]
            sb2.Append("<tr style='height: 30px;'>");
            sb2.Append("<td style='text-align:left;' colspan='16'>联系电话: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>签字: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("<tr>");
            sb2.Append("<td style='text-align:left;' colspan='16'>日期: ");
            sb2.Append("</td>");
            sb2.Append("</tr>");
            sb2.Append("</table>");
            sb2.Append("</div>");
            #endregion
            if (a.Rows.Count <= 10)
            {
                #region [表头]
                sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 5%;' rowspan='2'>用户名称</td>");
                sb1.Append("<td style='width: 5%;' rowspan='2'>联系电话</td>");
                sb1.Append("<td style='width: 10%;'rowspan='2'>用户地址</td>");
                sb1.Append("<td style='width: 5%;' rowspan='2'>设备管理方式</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>设备名称</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>规格型号</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>用户类别</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>调试人员</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>进口压力</td>");
                sb1.Append("<td style='width: 5%;'rowspan='2'>放散压力</td>");
                sb1.Append("<td style='width: 10%;'colspan='3'>上台</td>");
                sb1.Append("<td style='width: 10%;'colspan='3'>下台</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr align='center' valign='middle'>");
                sb1.Append("<td style='width: 5%;'>出口压力P2</td>");
                sb1.Append("<td style='width: 5%;'>关闭压力Pb</td>");
                sb1.Append("<td style='width: 5%;'>切断压力Pq</td>");
                sb1.Append("<td style='width: 5%;'>出口压力P2</td>");
                sb1.Append("<td style='width: 5%;'>关闭压力Pb</td>");
                sb1.Append("<td style='width: 5%;'>切断压力Pq</td>");
                sb1.Append("</tr>");
                #endregion
                for (int i = 0; i < a.Rows.Count; i++)
                {
                    #region [内容]
                    sb1.Append("<tr id ='DetailInfo" + i + "'>");
                    sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[i]["UserName"] + "</lable></td>");
                    sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[i]["Tel"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[i]["Address"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[i]["EquManType"] + "</lable></td>");
                    sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[i]["ProName"] + "</lable></td>");
                    sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[i]["Spec"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[i]["UserType"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[i]["DebPerson"] + "</lable></td>");
                    sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[i]["InletPreP1"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[i]["BleedingpreP1"] + "</lable></td>");
                    sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[i]["PowerExportPreP2"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[i]["PowerOffPrePb"] + "</lable></td>");
                    sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[i]["PowerCutOffPrePq"] + "</lable></td>");
                    sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[i]["SDExportPreP2"] + "</lable></td>");
                    sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[i]["SDPowerOffPrePb"] + "</lable></td>");
                    sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[i]["SDCutOffPrePq"] + "</lable></td>");
                    sb1.Append("</tr>");
                    #endregion
                }
                html = sb.ToString() + sb1.ToString() + sb2.ToString();
            }
            else
            {
                int count = a.Rows.Count % 10;
                if (count > 0)
                    count = a.Rows.Count / 10 + 1;
                else
                    count = a.Rows.Count / 10;
                for (int i = 0; i < count; i++)
                {
                    sb1 = new StringBuilder();
                    int b = 10 * i;
                    int length = 10 * (i + 1);
                    if (length > a.Rows.Count)
                        length = 10 * i + a.Rows.Count % 10;
                    #region [表头]
                    sb1.Append("<table id='myTable' cellpadding='0' cellspacing='0' class='tabInfo2'>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 5%;' rowspan='2'>用户名称</td>");
                    sb1.Append("<td style='width: 5%;' rowspan='2'>联系电话</td>");
                    sb1.Append("<td style='width: 10%;'rowspan='2'>用户地址</td>");
                    sb1.Append("<td style='width: 5%;' rowspan='2'>设备管理方式</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>设备名称</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>规格型号</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>用户类别</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>调试人员</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>进口压力</td>");
                    sb1.Append("<td style='width: 5%;'rowspan='2'>放散压力</td>");
                    sb1.Append("<td style='width: 10%;'colspan='3'>上台</td>");
                    sb1.Append("<td style='width: 10%;'colspan='3'>下台</td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr align='center' valign='middle'>");
                    sb1.Append("<td style='width: 5%;'>出口压力P2</td>");
                    sb1.Append("<td style='width: 5%;'>关闭压力Pb</td>");
                    sb1.Append("<td style='width: 5%;'>切断压力Pq</td>");
                    sb1.Append("<td style='width: 5%;'>出口压力P2</td>");
                    sb1.Append("<td style='width: 5%;'>关闭压力Pb</td>");
                    sb1.Append("<td style='width: 5%;'>切断压力Pq</td>");
                    sb1.Append("</tr>");
                    #endregion
                    for (int j = b; j < length; j++)
                    {
                        #region [内容]
                        sb1.Append("<tr id ='DetailInfo" + i + "'>");
                        sb1.Append("<td><lable class='laby" + i + "' id='y" + i + "'>" + a.Rows[j]["UserName"] + "</lable></td>");
                        sb1.Append("<td><lable class='lablb" + i + "' id='lb" + i + "'>" + a.Rows[j]["Tel"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhxm" + i + "' id='khxm" + i + "'>" + a.Rows[j]["Address"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxh" + i + "' id='xh" + i + "'>" + a.Rows[j]["EquManType"] + "</lable></td>");
                        sb1.Append("<td><lable class='labsl" + i + "' id='sl" + i + "'>" + a.Rows[j]["ProName"] + "</lable></td>");
                        sb1.Append("<td><lable class='labdj" + i + "' id='dj" + i + "'>" + a.Rows[j]["Spec"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbzsj" + i + "' id='bzsj" + i + "'>" + a.Rows[j]["UserType"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazsj" + i + "' id='azsj" + i + "'>" + a.Rows[j]["DebPerson"] + "</lable></td>");
                        sb1.Append("<td><lable class='labazry" + i + "' id='azry" + i + "'>" + a.Rows[j]["InletPreP1"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdh" + i + "' id='khdh" + i + "'>" + a.Rows[j]["BleedingpreP1"] + "</lable></td>");
                        sb1.Append("<td><lable class='labkhdz" + i + "' id='khdz" + i + "'>" + a.Rows[j]["PowerExportPreP2"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfpsj" + i + "' id='fpsj" + i + "'>" + a.Rows[j]["PowerOffPrePb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labxxsqd" + i + "' id='xxsqd" + i + "'>" + a.Rows[j]["PowerCutOffPrePq"] + "</lable></td>");
                        sb1.Append("<td><lable class='labfgs" + i + "' id='fgs" + i + "'>" + a.Rows[j]["SDExportPreP2"] + "</lable></td>");
                        sb1.Append("<td><lable class='labbz" + i + "' id='bz" + i + "'>" + a.Rows[j]["SDPowerOffPrePb"] + "</lable></td>");
                        sb1.Append("<td><lable class='labqrkhmyd" + i + "' id='qrkhmyd" + i + "'>" + a.Rows[j]["SDCutOffPrePq"] + "</lable></td>");
                        sb1.Append("</tr>");
                        #endregion
                    }
                    if ((length - b) < 10)
                    {
                    }
                    html += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
            }
            Response.Write(html);
            return View();
        }

        #endregion

        #endregion
        #region [处理审批]

        #region [处理记录审批]
        public ActionResult AppProcessing()
        {
            ViewData["webkey"] = "售后处理记录审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后处理记录审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }
        public ActionResult UserAppProcessing()
        {
            string where = " 1=1 and a.ValiDate = 'v' and a.state != -1 and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string HandleUser = Request["HandleUser"].ToString().Trim();
            if (HandleUser != "")
            {
                where += " a.HandleUser like '%" + HandleUser + "%' and";
            }

            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserAppProcessing(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region [合同审批]
        public ActionResult ContractProcessing()
        {
            ViewData["webkey"] = "售后部门合同审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后部门合同审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult UserContractProcessing()
        {
            string where = " 1=1 and a.ValiDate = 'v' and a.state=1 and  b.ApprovalPersons='" + GAccount.GetAccountInfo().UserID + "' and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string StrCID = Request["StrCID"].ToString().Trim();
            if (StrCID != "")
            {
                where += " a.StrCID like '%" + StrCID + "%' and";
            }

            where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = CustomerServiceMan.UserContractProcessing(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #endregion
        #region [客户服务]
        #region [客户服务]
        public ActionResult CusService()
        {
            return View();
        }
        public ActionResult AddCusService()
        {
            tk_CustomerInformation so = new TECOCITY_BGOI.tk_CustomerInformation();
            so.KHID = CustomerServiceMan.GetTopKHID();
            so.CreateUser = GAccount.GetAccountInfo().UserName;

            return View(so);
        }
        //保存保修卡
        public ActionResult SaveCusService()
        {
            if (ModelState.IsValid)
            {
                string type = Request["type"].ToString();
                string strErr = "";
                tk_CustomerInformation card = new tk_CustomerInformation();
                card.KHID = Request["KHID"].ToString();
                card.CusName = Request["CusName"].ToString();
                card.CusTel = Request["CusTel"].ToString();
                card.CusAdd = Request["CusAdd"].ToString();
                card.CusUnit = Request["CusUnit"].ToString();
                card.CusEmail = Request["CusEmail"].ToString();
                card.Remark = Request["Remark"].ToString();
                card.CreateUser = Request["CreateUser"].ToString();
                card.CreateTime = Convert.ToDateTime(Request["CreateTime"].ToString());
                card.Validate = "v";
                if (type == "1")//添加
                {
                    bool b = CustomerServiceMan.SaveCusService(card, type, ref strErr);
                    if (b)
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加服务信息";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CustomerInformation";
                        log.Typeid = Request["KHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else//修改
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "添加服务信息";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CustomerInformation";
                        log.Typeid = Request["KHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
                else//修改
                {
                    bool b = CustomerServiceMan.SaveCusService(card, type, ref strErr);
                    if (b)
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改服务信息";
                        log.LogContent = "添加成功";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CustomerInformation";
                        log.Typeid = Request["KHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = true });
                    }
                    else
                    {

                        #region [添加日志]
                        tk_CustomerServicelog log = new tk_CustomerServicelog();
                        log.LogTitle = "修改服务信息";
                        log.LogContent = "添加失败";
                        log.Person = GAccount.GetAccountInfo().UserName;
                        log.Tiem = DateTime.Now;
                        log.Type = "tk_CustomerInformation";
                        log.Typeid = Request["KHID"].ToString();
                        CustomerServiceMan.AddCustomerServiceLog(log);
                        #endregion
                        return Json(new { success = false, Msg = strErr });
                    }
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult CusServiceList(tk_CustomerInformation waquery)
        {
            if (ModelState.IsValid)
            {
                string where = " Validate='v' and";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string CusName = waquery.CusName;// Request["CusName"].ToString().Trim();
                string CusAdd = waquery.CusAdd;// Request["CusAdd"].ToString().Trim();
                string CusTel = waquery.CusTel;// Request["CusTel"].ToString().Trim();
                if (Request["CusName"].ToString().Trim() != "")
                    where += " CusName like '%" + Request["CusName"].ToString().Trim() + "%' and";
                if (Request["CusAdd"].ToString().Trim() != "")
                    where += " CusAdd like '%" + Request["CusAdd"].ToString().Trim() + "%' and";
                if (Request["CusTel"].ToString().Trim() != "")
                    where += " CusTel like '%" + Request["CusTel"].ToString().Trim() + "%' and";
                where = where.Substring(0, where.Length - 3);
                UIDataTable udtTask = CustomerServiceMan.CusServiceList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = "false", Msg = "操作失败！" });
            }
        }
        public ActionResult UpSaveCusService()
        {
            tk_CustomerInformation so = new TECOCITY_BGOI.tk_CustomerInformation();
            so.KHID = Request["KHID"].ToString();//CustomerServiceMan.GetTopBXKID();
            DataTable dt = CustomerServiceMan.UpSaveCusService(so.KHID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    so.CusName = dr["CusName"].ToString();
                    so.CusTel = dr["CusTel"].ToString();
                    so.CusAdd = dr["CusAdd"].ToString();
                    so.CusUnit = dr["CusUnit"].ToString();
                    so.CusEmail = dr["CusEmail"].ToString();
                    //  so.CreateUser = dr["CreateUser"].ToString();
                    so.Remark = dr["Remark"].ToString();
                }
            }
            so.CreateUser = GAccount.GetAccountInfo().UserName;
            return View(so);
        }

        //导出
        public FileResult CusServiceToExcel()
        {
            string where = " Validate='v' and";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string CusName = Request["CusName"].ToString().Trim();
            string CusAdd = Request["CusAdd"].ToString().Trim();
            string CusTel = Request["CusTel"].ToString().Trim();
            if (CusName != "")
                where += " CusName like '%" + CusName + "%' and";
            if (CusAdd != "")
                where += " CusAdd like '%" + CusAdd + "%' and";
            if (CusTel != "")
                where += " CusTel like '%" + CusTel + "%' and";
            where = where.Substring(0, where.Length - 3);
            string strErr = "";
            string tableName = " BGOI_CustomerService.dbo.tk_CustomerInformation ";
            string FieldName = " KHID, CusName, CusTel, CusAdd, CusUnit, CusEmail, CreateUser, CreateTime, Remark ";
            string OrderBy = " CreateTime ";
            DataTable data = CustomerServiceMan.NewGetWarrantyCardToExcel(where, tableName, FieldName, OrderBy, ref strErr);
            if (data != null)
            {
                string strCols = "客户编号-6000,客户名称-6000,客户电话-6000,客户地址-5000,客户单位-6000,客户邮箱-5000,登陆人-3000,";
                strCols += "创建时间-3000,备注 -6000 ";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(data, "客户信息", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "客户信息.xls");
            }
            else
                return null;

        }
        #endregion
        #region [合同]
        public ActionResult Contract()
        {
            ViewData["webkey"] = "售后部门合同审批";
            string fold = COM_ApprovalMan.getNewwebkey("售后部门合同审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }
        public ActionResult AddContractnew(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas.StrCID = CustomerServiceMan.GetNewShowCIDnew();
            if (id != null)
                Bas.StrPID = id;
            return View(Bas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult InsertContractBasnew(ContractBas Bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                Bas.StrUnit = account.UnitID.ToString();
                Bas.StrCID = ContractMan.GetNewCID();
                Bas.StrCreateTime = DateTime.Now;
                Bas.StrCreateUser = account.UserID.ToString();
                Bas.StrValidate = "v";
                Bas.StrState = 0;
                string strErr = "";
                if (CustomerServiceMan.InsertNewContractBasnew(Bas, ref strErr) == true)
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
        /// 可以
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractGridnew(tk_ContractSearch ContractSearch)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = " and a.Unit = '" + unit + "'";
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
                UIDataTable udtTask = CustomerServiceMan.getNewContractGridnew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        /// 回款记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CashBackNew(string id, string type)
        {
            CCashBack Cash = new CCashBack();
            Cash.StrCID = id;
            Cash.StrCBID = ContractMan.GetNewshowCBID(id);
            Cash.StrCurAmountNum = ContractMan.GetNewCurAmountNum(id);
            ViewData["type"] = type;
            return View(Cash);
        }
        /// <summary>
        /// 保存回款记录
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
        //回款记录详细
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
        //巡检次数
        public ActionResult ViewInspection()
        {
            string CId = Request["data1"].ToString();
            DataTable dt = CustomerServiceMan.ViewInspection(CId);
            string a = dt.Rows[0][0].ToString();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, Msg = "本月巡检" + dt.Rows[0][0].ToString() + "次" });
        }
        //售后调压箱代管业务
        public ActionResult BGOI_TPRBE()
        {
            return View();
        }
        //变更合同
        public ActionResult ChangeContracNew(string id)
        {
            ContractBas Bas = new ContractBas();
            Bas = ContractMan.getNewChangeContract(id);
            return View(Bas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bas"></param>
        /// <returns></returns>
        public ActionResult UpdateContractNew(ContractBas Bas)
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
        //提交合同
        public ActionResult TJContract()
        {
            string strErr = "";
            string id = Request["texts"].ToString();
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
          // string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string PID = CustomerServiceMan.GetSPid(folderBack);
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            if (CustomerServiceMan.TJContract(PID, RelevanceID, webkey, createUser, ref  strErr))
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "提交审批";
                log.LogContent = "提交成功";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_Approval";
                log.Typeid = PID;
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "true", Msg = "提交成功" });
            }
            else
            {
                #region [添加日志]
                tk_CustomerServicelog log = new tk_CustomerServicelog();
                log.LogTitle = "提交审批";
                log.LogContent = "提交失败";
                log.Person = GAccount.GetAccountInfo().UserName;
                log.Tiem = DateTime.Now;
                log.Type = "tk_Approval";
                log.Typeid = PID;
                CustomerServiceMan.AddCustomerServiceLog(log);
                #endregion
                return Json(new { success = "false", Msg = strErr });
            }
        }
        public ActionResult HTApproval(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[2];
            string RelevanceID = arr[0];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        //10W以下及10W审批
        public ActionResult HTApprovalSW(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = arr[2];
            string RelevanceID = arr[0];
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        public ActionResult UpdateTJContract()
        {

            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];// COM_ApprovalMan.GetNewShowSPid(folderBack);
            var RelevanceID = Request["RelevanceID"];
            string strErr = "";
            if (CustomerServiceMan.UpdateTJContract(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        //10W以下及10W审批
        public ActionResult UpdateTJContractSW()
        {

            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            string strErr = "";
            if (CustomerServiceMan.UpdateTJContractSW(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        //加载打印数据
        public ActionResult PrintXSContract()
        {
            string CID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(CID))
            {
                s += " CID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where Validate='v' and " + s; }
            string strErr = "";

            string tableName = " BGOI_BasMan.dbo.tk_ContractBas  ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            foreach (DataRow dt in data.Rows)
            {
                ViewData["CID"] = dt["CID"].ToString();
                ViewData["Cname"] = dt["Cname"].ToString();
            }

            string wherecon = "";
            string scon = "";
            if (!string.IsNullOrEmpty(CID))
            {
                scon += " RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(scon)) { wherecon = " where a.Validate='v' and a.Job='总经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselect = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condata = CustomerServiceMan.PrintList(wherecon, conselect, ref strErr);
            foreach (DataRow dr in condata.Rows)
            {
                ViewData["name"] = dr["UserName"].ToString();
                ViewData["ApprovalTime"] = dr["ApprovalTime"].ToString();
                ViewData["Remark"] = dr["Remark"].ToString();
            }


            string whereconf = "";
            string sconf = "";
            if (!string.IsNullOrEmpty(CID))
            {
                sconf += " RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(sconf)) { whereconf = " where a.Validate='v' and a.Job='副总经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselectf = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condataf = CustomerServiceMan.PrintList(whereconf, conselectf, ref strErr);
            foreach (DataRow drf in condataf.Rows)
            {
                ViewData["namef"] = drf["UserName"].ToString();
                ViewData["ApprovalTimef"] = drf["ApprovalTime"].ToString();
                ViewData["Remarkf"] = drf["Remark"].ToString();
            }


            string whereconfj = "";
            string sconfj = "";
            if (!string.IsNullOrEmpty(CID))
            {
                sconfj += " RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(sconfj)) { whereconfj = " where a.Validate='v' and a.Job='经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselectfj = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condatafj = CustomerServiceMan.PrintList(whereconfj, conselectfj, ref strErr);
            foreach (DataRow drfj in condatafj.Rows)
            {
                ViewData["namefj"] = drfj["UserName"].ToString();
                ViewData["ApprovalTimefj"] = drfj["ApprovalTime"].ToString();
                ViewData["Remarkfj"] = drfj["Remark"].ToString();
            }
            return View();
        }

        //加载打印数据
        public ActionResult PrintXSContractCG()
        {
            string CID = Request["Info"];
            string where = "";
            string s = "";
            if (!string.IsNullOrEmpty(CID))
            {
                s += " a.CID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(s)) { where = " where a.Validate='v' and a.BusinessType = b.SID and b.Type = 'BusinessType'  and " + s; }
            string strErr = "";

            string tableName = " BGOI_BasMan.dbo.tk_ContractBas a ,BGOI_BasMan.dbo.tk_ConfigBussinessType b   ";
            DataTable data = CustomerServiceMan.PrintList(where, tableName, ref strErr);
            foreach (DataRow dt in data.Rows)
            {
                ViewData["CID"] = dt["CID"].ToString();
                ViewData["Cname"] = dt["Cname"].ToString();
                ViewData["Text"] = dt["Text"].ToString();
            }

            string wherecon = "";
            string scon = "";
            if (!string.IsNullOrEmpty(CID))
            {
                scon += " a.RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(scon)) { wherecon = " where a.Validate='v' and a.Job='总经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselect = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condata = CustomerServiceMan.PrintList(wherecon, conselect, ref strErr);
            foreach (DataRow dr in condata.Rows)
            {
                ViewData["name"] = dr["UserName"].ToString();
                ViewData["ApprovalTime"] = dr["ApprovalTime"].ToString();
                ViewData["Remark"] = dr["Remark"].ToString();
            }


            string whereconf = "";
            string sconf = "";
            if (!string.IsNullOrEmpty(CID))
            {
                sconf += " a.RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(sconf)) { whereconf = " where a.Validate='v' and a.Job='副总经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselectf = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condataf = CustomerServiceMan.PrintList(whereconf, conselectf, ref strErr);
            foreach (DataRow drf in condataf.Rows)
            {
                ViewData["namef"] = drf["UserName"].ToString();
                ViewData["ApprovalTimef"] = drf["ApprovalTime"].ToString();
                ViewData["Remarkf"] = drf["Remark"].ToString();
            }


            string whereconfj = "";
            string sconfj = "";
            if (!string.IsNullOrEmpty(CID))
            {
                sconfj += " a.RelevanceID like '%" + CID + "%' ";
            }
            if (!string.IsNullOrEmpty(sconfj)) { whereconfj = " where a.Validate='v' and a.Job='经理' and a.ApprovalPersons = b.UserId and " + scon; }
            string conselectfj = " BGOI_CustomerService.dbo.tk_Approval a ,BJOI_UM..UM_UserNew b ";
            DataTable condatafj = CustomerServiceMan.PrintList(whereconfj, conselectfj, ref strErr);
            foreach (DataRow drfj in condatafj.Rows)
            {
                ViewData["namefj"] = drfj["UserName"].ToString();
                ViewData["ApprovalTimefj"] = drfj["ApprovalTime"].ToString();
                ViewData["Remarkfj"] = drfj["Remark"].ToString();
            }
            return View();
        }
        public ActionResult ConditionGridNew()
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
                udtTask = CustomerServiceMan.ConditionGridNew(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region [系统设置]
        public ActionResult AftermarketConfiguration()
        {
            return View();
        }
        public ActionResult getBasicGrid()
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
                where += "  a.Type ='" + type + "' ";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = CustomerServiceMan.getBasicGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region [售后待审批提示]
        public ActionResult ConSP()
        {
            DataTable dt = CustomerServiceMan.ConSP();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
    }
}
