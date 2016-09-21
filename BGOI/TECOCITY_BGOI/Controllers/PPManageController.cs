using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    public class PPManageController : Controller
    {
        //
        // GET: /SalesManage/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult PPRight()
        {
            return View();
        }

        //采购申请主页

        public ActionResult GetPipeSize()
        {
            string where = Request["where"].ToString();
            DataTable dt = PPManage.GetNewConfigCont(where);
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["ID"].ToString() + ",";
                name += dt.Rows[i]["Text"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }

        public ActionResult GetNewConfigContKF()
        {

            DataTable dt = PPManage.GetNewConfigContKF();
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["HouseID"].ToString() + ",";
                name += dt.Rows[i]["HouseName"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }
        public ActionResult PurchaseApplication()
        {
            return View();
        }
        #region[上传]

     
        public ActionResult AddFile()
        {
            if (Request["PID"] == null)
            {
                ViewData["PIDs"] = "";
                return View();
            }
            string Info = Request["PID"].ToString();
            ViewData["PIDs"] = Info;
            return View();
        }
        public ActionResult InsertForm(PP_File pp)
        {
            pp.PID = Request["Hidden"].ToString();
            #region MyRegion
            //HttpPostedFileBase file = Request.Files[0];
            //byte[] fileByte = new byte[0];
            //string FileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);

            ////ViewData["FileName"] = FileName;


            //int fileLength = file.ContentLength;
            //if (fileLength != 0)
            //{
            //    fileByte = new byte[fileLength];
            //    file.InputStream.Read(fileByte, 0, fileLength);
            //} 
            #endregion

            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                //pp.FileName = a.Substring(0, a.IndexOf('.'));
                pp.FileName = a;
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (PPManage.InsertNewFile(pp, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "上传成功";
                return View("AddFile");
            }
            else
            {
                ViewData["msg"] = "上传失败";
                return View("AddFile");
            }



        }

        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (PPManage.deleteFile(arr[0]) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        public ActionResult GetFile()
        {
            var id = Request["data1"];
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = PPManage.GetNewDownLoad(id);
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
        #endregion

        #region[设置提醒时间]
        public ActionResult SetWarnTime()
        {
            return View();
        }

        public ActionResult InsertConfigTime(string num)
        {
            string checkWay = "CG";
            string TimeType = "TimeCG";
            Acc_Account account = GAccount.GetAccountInfo();
            string unit = account.UnitID.ToString();
            string strErr = "";
            if (EquipMan.InsertNewCongTime(checkWay, num, unit, TimeType, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }
        #endregion


        #region[公共方法]
        public ActionResult GetDataTime()
        {
            string table = Request["table"];
            string lie = Request["lie"];
            string type = Request["type"];
            DataTable dt = PPManage.GetDataTime(table, lie, type);

            if (dt != null)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }

        public ActionResult GetSuppliers()
        {
            DataTable dt = PPManage.GetSuppliers();

            if (dt != null)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }

        public ActionResult SelectRZ()
        {
            string type = Request["Type"];
            string where = " type='" + type + "' ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = PPManage.SelectRZ(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);


        }
        #endregion







        #region[选择业务类型]
        public ActionResult OrderInfoManage()
        {
            return View();
        }
        public ActionResult GetOrderInfo()
        {

            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = PPManage.GetOrderInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrderInfoGoods()
        {

            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";
            string OrderID = Request["OrderID"];
            if (OrderID == "" || OrderID == null)
            {
                return View();
            }
            string where = "OrderID='" + OrderID + "'";
            UIDataTable udtTask = PPManage.GetOrderInfoGoods(where, GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SalesRetailManage()
        {
            return View();
        }
        public ActionResult GetSalesGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string strWhere = "";

                if (salesProduct != "" && salesProduct != null)
                    strWhere += " and b.OrderContent='" + salesProduct + "' ";
                if (specsModels != "" && specsModels != null)
                    strWhere += " and b.SpecsModels='" + specsModels + "' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and a.ProvidManager='" + salesMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and a.ContractDate between '" + startDate + "' and '" + endDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = PPManage.GetSalesRetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
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

        public ActionResult GetSearchOrderInfo(tk_SalesGrid tk_salesgrid)
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
                string State = Request["State"].ToString();
                string HState = Request["HState"];

                string s = "";
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
                    s += " OrderContent like '%" + OrderContent + "%' and";
                }
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    s += " ContractDate between  '" + StartDate + "' and '" + EndDate + "' and";
                }
                if (!string.IsNullOrEmpty(State))
                {
                    if (State == "0")
                    {
                        //  s += " (a.State =1 or a.State =2 or a.State =3 ) and";
                    }
                    else
                    {
                        s += " a.State =" + State + " and";
                    }
                }
                if (!string.IsNullOrEmpty(HState))
                {
                    s += " IsPay =" + HState + " ";
                }
                if (!string.IsNullOrEmpty(s) && string.IsNullOrEmpty(HState))
                {
                    s = s.Substring(0, s.Length - 3);
                }
                if (!string.IsNullOrEmpty(s)) { where = " where " + s; }
                UIDataTable udtTask = SalesManage.GetOrderInfo(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        #endregion


        public ActionResult CreateNewProject()
        {
            tk_ProjectBas ProjectBas = new tk_ProjectBas();
            ProjectBas.StrPID = ProjectMan.GetNewShowPid();
            return View(ProjectBas);
        }
        public ActionResult CreatePurchase()
        {
            PP_PurchaseRequisition pp = new PP_PurchaseRequisition();
            pp.CID = PPManage.GetPurchaseRequisitionQGID();
            return View(pp);
        }


        #region [询价]
        /// <summary>
        /// 获取登陆人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddInquiry()
        {
            PP_Inquirys inq = new TECOCITY_BGOI.PP_Inquirys();
            inq.XJID = PPManage.GetTopListXJID();
            inq.OrderUnit = GAccount.GetAccountInfo().UnitID;
            return View(inq);
        }
        public ActionResult insertinquiry()
        {
            PP_Inquirys inq = new PP_Inquirys();
            inq.XJID = Request["XJID"].ToString();
            inq.CID = Request["CID"].ToString();
            inq.OrderNumber = Request["OrderNumber"].ToString();
            //inq.OrderUnit = Request["OrderUnit"].ToString();
            inq.OrderUnit = GAccount.GetAccountInfo().UnitID;
            inq.InquiryTitle = Request["InquiryTitle"].ToString();
            inq.OrderContacts = Request["OrderContacts"].ToString();
            inq.Approver1 = Request["Approver1"].ToString();
            inq.Approver2 = Request["Approver2"].ToString();
            inq.State = Request["State"].ToString();
            inq.BusinessTypes = Request["BusinessTypes"].ToString();
            inq.InquiryExplain = Request["InquiryExplain"].ToString();
            inq.InquiryDate = Convert.ToDateTime(Request["InquiryDate"]);
            inq.DeliveryLimit = Request["DeliveryLimit"].ToString();
            inq.DeliveryMethod = Request["DeliveryMethod"].ToString();
            inq.IsInvoice = Request["IsInvoice"].ToString();
            inq.PaymentMethod = Request["PaymentMethod"].ToString();
            inq.PaymentAgreement = Request["PaymentAgreement"].ToString();
            inq.TotalTax = Convert.ToDecimal(Request["TotalTax"]);
            inq.TotalNoTax = Convert.ToDecimal(Request["TotalNoTax"]);
            bool b = PPManage.insertinquiry(inq);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

        }
        public ActionResult IndexXJXQ()
        {
            return View();
        }
        public ActionResult SelectInquiry()
        {

            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = " b.Type='请购状态'  and a.Validate='1' and ";
            //}
            //else
            //{
            where = " b.Type='请购状态' and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'  and a.Validate='1' and ";
            //}

            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string XJID = Request["XJID"].ToString();
            //string XJState = Request["State"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";

            if (Begin != "" && End != "")
                where += " a.inquirydate between '" + Begin + "' and '" + End + "' and ";
            //if (XJState != "")
            //where += " a.XJState= '" + XJState + "' and";
            if (XJID != "")
                where += " a.XJID like'%" + XJID + "%' and";
            if (XJID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = PPManage.SelectInquiry(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PrintXJ(string id)
        {
            DataTable dt = PPManage.SelectGoodsXJID(id);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">请购单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">询价人</td><td class=\"PRight\" colspan=\"5\">" + dt.Rows[0]["UserName"].ToString() + "</td><td class=\"PLeft\">询价日期</td><td class=\"PRight\"  colspan=\"4\">" + Convert.ToDateTime(dt.Rows[0]["InquiryDate"]).ToString("yyyy-MM-dd") + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\">询价标题</td><td class=\"PRight\" colspan=\"5\">" + dt.Rows[0]["InquiryTitle"].ToString() + "</td><td class=\"PLeft\">合同条件</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["ContractCondition"].ToString() + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\" >询价说明</td><td  colspan=\"10\">" + dt.Rows[0]["InquiryExplain"].ToString() + "</td></tr>");

            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td><td  style=\"width:10%\" >税前总价</td><td  style=\"width:10%\" >税前单价</td><td  style=\"width:10%\" >备注</td></tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["XXID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["Supplier"].ToString() + "</td><td  >" + dt.Rows[i]["NegotiatedPricingNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNegotiationNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["GoodsUse"].ToString() + "</td><td  >" + dt.Rows[i]["DrawingNum"].ToString() + "</td><td  >" + dt.Rows[i]["Remark"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();

        }
        public ActionResult SelectXJXQ()
        {
            string XJID = Request["XJID"].ToString();
            DataTable dt = PPManage.SelectXJXQ(XJID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult SelectGoodsXJID()
        {
            string XJID = Request["XJID"];
            DataTable dt = PPManage.SelectGoodsXJID(XJID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult InsertXJ()
        {
            string strErr = "";
            PP_Inquirys inq = new PP_Inquirys();
            inq.XJID = Request["XJID"];
            inq.CID = Request["CID"];
            inq.OrderUnit = GAccount.GetAccountInfo().UnitID;
            inq.InquiryTitle = Request["InquiryTitle"];
            inq.OrderContacts = Request["OrderContacts"];
            inq.XJState = "0";
            inq.InquiryExplain = Request["InquiryExplain"];
            inq.InquiryDate = Convert.ToDateTime(Request["InquiryDate"]);
            inq.CreateTime = DateTime.Now;
            inq.CreateUser = GAccount.GetAccountInfo().UserName;
            inq.Validate = "1";


            string[] RowNumber = Request["RowNumber"].Split(',');
            string[] XXID = Request["xxid"].Split(',');
            string[] OrderContent = Request["proname"].Split(',');
            string[] Specifications = Request["spec"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] Unit = Request["units"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] NegotiatedPricingNoTax = Request["total"].Split(',');
            string[] totalNegotiationNoTax = Request["totalNegotiationNoTax"].Split(',');

            string[] DrawingNum = Request["drawingNum"].Split(',');
            string[] GoodsUse = Request["goodsuse"].Split(',');
            PP_InquiryCondition con = new PP_InquiryCondition();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                con.XJID = Request["XJID"];
                con.XID = Request["XJID"] + "-" + RowNumber[i];
                con.Supplier = Request["Suppliers"];
                con.ContractCondition = Request["ContractCondition"];
                con.CreateTime = DateTime.Now;
                con.CreateUser = GAccount.GetAccountInfo().UserName;
                con.Validate = "1";
            }

            List<PP_InquiryGoods> list = new List<PP_InquiryGoods>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_InquiryGoods pp = new PP_InquiryGoods();
                pp.XJID = Request["XJID"];
                pp.XID = Request["XJID"] + "-" + RowNumber[i];
                pp.XXID = XXID[i];
                pp.OrderContent = OrderContent[i];
                pp.Specifications = Specifications[i];
                pp.Supplier = Supplier[i];
                pp.Unit = Unit[i];
                pp.Amount = Convert.ToInt32(Amount[i]);
                pp.NegotiatedPricingNoTax = Convert.ToDecimal(NegotiatedPricingNoTax[i]);
                pp.GoodsUse = GoodsUse[i];
                //pp.TotalNegotiationNoTax = Convert.ToInt32(Amount[i]) * Convert.ToDecimal(NegotiatedPricingNoTax[i]);
                pp.TotalNegotiationNoTax = Convert.ToDecimal(totalNegotiationNoTax[i]);

                pp.DrawingNum = DrawingNum[i];
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.Validate = "1";
                list.Add(pp);
            }
            bool ok = PPManage.InsertXJ(list, con, inq, ref strErr);
            if (ok)
            {
                PPManage.AddRZ(inq.XJID, "增加询价", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(inq.XJID, "增加询价", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult IndexInquiry()
        {
            return View();
        }
        public ActionResult SelectXJ()
        {
            string where = " ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string XJID = Request["XJID"];
            where += "a.XJID='" + XJID + "'";
            UIDataTable udtTask = PPManage.SelectXJ(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateXJValidate()
        {
            string XJID = Request["XJID"];
            int a = PPManage.UpdateXJValidate(XJID);
            if (a != 1)
            {
                PPManage.AddRZ(XJID, "撤销询价", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = false });
            }

            else
            {
                PPManage.AddRZ(XJID, "撤销询价", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = true });
            }

        }
        public ActionResult SelectXJCID()
        {
            string CID = Request["CID"];
            string where = "CID='" + CID + "'";
            DataTable dt = PPManage.SelectXJCID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }
        public ActionResult UpdateXJ()
        {

            string strErr = "";
            PP_Inquirys inq = new PP_Inquirys();
            inq.XJID = Request["XJID"];
            inq.OrderUnit = GAccount.GetAccountInfo().UnitID;
            inq.InquiryTitle = Request["InquiryTitle"];
            inq.OrderContacts = Request["OrderContacts"];
            inq.InquiryExplain = Request["InquiryExplain"];
            inq.InquiryDate = Convert.ToDateTime(Request["InquiryDate"]);

            string[] RowNumber = Request["RowNumber"].Split(',');
            string[] XXID = Request["xxid"].Split(',');
            string[] OrderContent = Request["proname"].Split(',');
            string[] Specifications = Request["spec"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] Unit = Request["units"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] NegotiatedPricingNoTax = Request["total"].Split(',');
            string[] totalNegotiationNoTax = Request["totalNegotiationNoTax"].Split(',');

            string[] DrawingNum = Request["drawingNum"].Split(',');
            string[] GoodsUse = Request["goodsuse"].Split(',');
            PP_InquiryCondition con = new PP_InquiryCondition();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                con.XJID = Request["XJID"];
                con.XID = Request["XJID"] + "-" + RowNumber[i];
                con.Supplier = Request["Suppliers"];
                con.ContractCondition = Request["ContractCondition"];

            }

            List<PP_InquiryGoods> list = new List<PP_InquiryGoods>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_InquiryGoods pp = new PP_InquiryGoods();
                pp.XJID = Request["XJID"];
                pp.XID = Request["XJID"] + "-" + RowNumber[i];
                pp.XXID = XXID[i];
                pp.OrderContent = OrderContent[i];
                pp.Specifications = Specifications[i];
                pp.Supplier = Supplier[i];
                pp.Unit = Unit[i];
                pp.Amount = Convert.ToInt32(Amount[i]);
                pp.GoodsUse = GoodsUse[i];
                pp.DrawingNum = DrawingNum[i];
                pp.NegotiatedPricingNoTax = Convert.ToDecimal(NegotiatedPricingNoTax[i]);
                pp.TotalNegotiationNoTax = Convert.ToDecimal(totalNegotiationNoTax[i]);

                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.Validate = "1";
                list.Add(pp);
            }
            bool ok = PPManage.UpdateXJ(inq, con, list, ref strErr);
            if (ok)
            {
                PPManage.AddRZ(inq.XJID, "修改询价单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(inq.XJID, "修改询价单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult Inquiry()
        {
            PP_Inquirys inq = new TECOCITY_BGOI.PP_Inquirys();
            inq.XJID = PPManage.GetTopListXJID();
            return View(inq);
        }
        public ActionResult DetailsXJ()
        {
            return View();
        }
        public ActionResult UpdateXJXX()
        {
            return View();
        }
        #endregion
        #region [采购订单]

        public ActionResult LJSplits()
        {
            return View();
        }

        public ActionResult SelectSplitLJ()
        {
            string DDID = Request["DDID"];
            string where = "";
            if (DDID != "")
            {
                where = " a.DDID='" + DDID + "' ";
            }
            DataTable dt = PPManage.SelectSplitLJ(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult SelectSplitLJxq()
        {
            string Name = Request["Name"];
            string cpid = Request["CPID"];
  
            DataTable dt = PPManage.SelectSplitLJxq(Name, cpid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        public ActionResult AddOrder()
        {
            PP_PurchaseOrder order = new TECOCITY_BGOI.PP_PurchaseOrder();

            order.DDID = PPManage.GetTopListDDID();
            return View(order);
        }
        public ActionResult InsertPurchaseOrder()
        {
            PP_PurchaseOrder order = new PP_PurchaseOrder();
            order.DDID = Request["DDID"].ToString();
            order.CID = Request["CID"].ToString();
            order.OrderNumber = Request["OrderNumber"].ToString();
            //order.OrderUnit = Request["OrderUnit"].ToString();
            order.OrderUnit = GAccount.GetAccountInfo().UnitID;
            order.OrderContacts = Request["OrderContacts"].ToString();
            order.Approver1 = Request["Approver1"].ToString();

            order.Approver2 = Request["Approver2"].ToString();
            order.ArrivalStatus = Request["ArrivalStatus"].ToString();
            order.PayStatus = Request["PayStatus"].ToString();
            order.State = Request["State"].ToString();
            order.BusinessTypes = Request["BusinessTypes"].ToString();
            order.PleaseExplain = Request["PleaseExplain"].ToString();

            order.OrderDate = Convert.ToDateTime(Request["OrderDate"]); ;
            order.DeliveryLimit = Request["DeliveryLimit"];
            order.DeliveryMethod = Request["DeliveryMethod"].ToString();
            order.IsInvoice = Request["IsInvoice"].ToString();
            order.PaymentMethod = Request["PaymentMethod"].ToString();
            order.PaymentAgreement = Request["PaymentAgreement"].ToString();

            order.ContractNO = Request["ContractNO"].ToString();
            order.TheProject = Request["TheProject"].ToString();
            order.TotalTax = Convert.ToDecimal(Request["TotalTax"]);
            order.TotalNoTax = Convert.ToDecimal(Request["TotalNoTax"]);
            bool b = PPManage.InsertPurchseOrder(order);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        //public ActionResult PrintDD(string id)
        //{
        //    string str = "a.DDID='" + id + "'";
        //    DataTable dt = PPManage.SelectGoodsDDID(str);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">订购单</div>");
        //    sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
        //    sb.Append("<tr><td class=\"PLeft\">订购人</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["UserName"].ToString() + "</td><td class=\"PLeft\">订购日期</td><td class=\"PRight\"  colspan=\"4\">" + Convert.ToDateTime(dt.Rows[0]["OrderDate"]).ToString("yyyy-MM-dd") + "</td></tr>");

        //    sb.Append("<tr><td class=\"PLeft\">业务类型</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["BusinessTypes"].ToString() + "</td><td class=\"PLeft\">任务单号</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["PID"].ToString() + "</td></tr>");

        //    sb.Append("<tr><td class=\"PLeft\" >交货期限</td><td  colspan=\"2\">" + dt.Rows[0]["DeliveryLimit"].ToString() + "</td><td class=\"PLeft\" >交货方式</td><td  colspan=\"2\">" + dt.Rows[0]["JHFS"].ToString() + "</td><td class=\"PLeft\" >是否开发票</td><td  colspan=\"3\">" + dt.Rows[0]["FP"].ToString() + "</td></tr>");

        //    sb.Append("<tr><td class=\"PLeft\" >支付方式</td><td  colspan=\"2\">" + dt.Rows[0]["ZFFS"].ToString() + "</td><td class=\"PLeft\" >付款约定</td><td  colspan=\"2\">" + dt.Rows[0]["FKYD"].ToString() + "</td><td class=\"PLeft\" >合同编号</td><td  colspan=\"3\">" + dt.Rows[0]["ContractNO"].ToString() + "</td></tr>");

        //    sb.Append("<tr><td class=\"PLeft\" >所属项目</td><td  colspan=\"9\">" + dt.Rows[0]["TheProject"].ToString() + "</td></tr>");

        //    sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td><td  style=\"width:10%\" >用途</td><td  style=\"width:10%\" >图纸号</td></tr>");

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sb.Append("<tr><td >" + dt.Rows[i]["MaterialNO"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["Supplier"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["GoodsUse"].ToString() + "</td><td  >" + dt.Rows[i]["DrawingNum"].ToString() + "</td></tr>");
        //    }
        //    sb.Append("</table>");
        //    Response.Write(sb.ToString());
        //    return View();


        //}
        public ActionResult PrintSupplier()
        {
            return View();
        }
        public ActionResult GetSupplierID()
        {
            string where = Request["where"].ToString();
            DataTable dt = PPManage.GetSupplierID(where);
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["Supplier"].ToString() + ",";
                name += dt.Rows[i]["COMNameC"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }
        public ActionResult PrintDD(string id, string Supplier)
        {
            string str = "a.DDID='" + id + "' and b.Supplier='" + Supplier + "'";
            DataTable dt = PPManage.SelectGoodsDDID(str);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">采购单</div>");
            sb.Append("<div align=\"center\" style=\"margin-top:15px;margin-left:430px;font-size:20px;\">YSGLJL-SC-F02</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">订购日期</td><td class=\"PRight\"  colspan=\"9\">" + Convert.ToDateTime(dt.Rows[0]["OrderDate"]).ToString("yyyy-MM-dd") + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" >订单编号</td><td  colspan=\"4\">" + dt.Rows[0]["DDID"].ToString() + "</td><td class=\"PLeft\" >交货方式</td><td  colspan=\"4\">" + dt.Rows[0]["JHFS"].ToString() + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\" >支付方式</td><td  colspan=\"4\">" + dt.Rows[0]["ZFFS"].ToString() + "</td><td class=\"PLeft\" >付款约定</td><td  colspan=\"4\">" + dt.Rows[0]["FKYD"].ToString() + "</td></tr>");
            sb.Append("<tr><td style=\"width:10%\" >序号</td><td style=\"width:10%\" >名称</td><td style=\"width:10%\"  >供货商</td><td  style=\"width:10%\" >单位</td><td  style=\"width:10%\" >计划采购量</td><td style=\"width:10%\" >单价(元)</td><td style=\"width:10%\" >金额(元)</td><td style=\"width:10%\" >税前单价</td><td  style=\"width:10%\" >税前总价</td><td  style=\"width:10%\" >备注</td></tr>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int a = i;
                a++;
                sb.Append("<tr><td >" + a + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["COMNameC"].ToString() + "</td><td >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPrice"].ToString() + "</td><td  >" + dt.Rows[i]["Total"].ToString() + "</td><td  >" + dt.Rows[i]["Remark"].ToString() + "</td></tr>");
            }
            string strs = "a.RelevanceID='" + id + "' ";
            DataTable dts = PPManage.selectjob(strs);
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                sb.Append("<tr><td class=\"PLeft\" >" + dts.Rows[i]["job"].ToString() + "</td><td  colspan=\"3\">" + dts.Rows[i]["UserName"].ToString() + "</td><td class=\"PLeft\" >日期</td><td  colspan=\"5\">" + dts.Rows[i]["ApprovalTime"].ToString() + "</td></tr>");
            }
            sb.Append("<tr><td class=\"PLeft\" >经办人</td><td  colspan=\"3\">" + dt.Rows[0]["OrderContacts"].ToString() + "</td><td class=\"PLeft\" >日期</td><td  colspan=\"5\">" + Convert.ToDateTime(dt.Rows[0]["OrderDate"]).ToString("yyyy-MM-dd") + "</td></tr>");

            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();

        }

        public ActionResult ShipmentsToExcel()
        {
            string DDID = Request["DDID"];
            string where = " 1=1 and ";
            //if (DDID == "")
            //{
            //    return Content(@"<script>alert('请输入订单单号');   window.location.href='../PPManage/IndexOrder';</script>");
            //}

            //string str = " ";
            //if (DDID != "")
            //{
            //    str += "a.DDID='" + DDID + "'";
            //}
            //if (DDID == "")
            //    str = "1=1";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string DeliveryLimit = Request["DeliveryLimit"].ToString();
            if (DeliveryLimit != "")
                DeliveryLimit += " 00:00:00";
            string DeliveryLimit1 = Request["DeliveryLimit1"].ToString();
            if (DeliveryLimit1 != "")
                DeliveryLimit1 += " 23:59:59";
            if (DeliveryLimit != "" && DeliveryLimit1 != "")

                where += " a.DeliveryLimit between '" + DeliveryLimit + "' and '" + DeliveryLimit1 + "' and ";

            if (Begin != "" && End != "")
                where += " a.OrderDate between '" + Begin + "' and '" + End + "' and ";

            if (DDID != "")
                where += " a.DDID like'%" + DDID + "%' and";

            if (DDID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            DataTable dt = PPManage.SelectGoodsDC(where); ;//LoadShipmentsGrid(where,ref string strErr);
            DataTable dts = PPManage.SelectGoodsDCs(where);
            if (dt != null)
            {
                string strCols = "名称-3000,图号-3000,单位-1800,数量-1800,单价-2500,金额-2500,税前单价-2500,税前金额-1800,备注-5000";
                //System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "零件采购单", strCols.Split(','));
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelHs(dt, dts, "零件采购单", strCols.Split(','));

                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "零件采购单表.xls");
            }
            else
                return null;
        }


        public ActionResult DDXQ()
        {
            string DDID = Request["DDID"];
            DataTable dt = PPManage.SelectDDXQ(DDID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult IndexDDXQ()
        {
            return View();
        }
        public ActionResult SelectDD()
        {
            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = "  b.Type='采购订单状态'  and a.Validate='1' and ";
            //}
            //else
            //{
            where = "  a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'  and a.Validate='1' and ";
            //}

            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string DDID = Request["DDID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string DeliveryLimit = Request["DeliveryLimit"].ToString();
            if (DeliveryLimit != "")
                DeliveryLimit += " 00:00:00";
            string DeliveryLimit1 = Request["DeliveryLimit1"].ToString();
            if (DeliveryLimit1 != "")
                DeliveryLimit1 += " 23:59:59";
            if (DeliveryLimit != "" && DeliveryLimit1 != "")

                where += " a.DeliveryLimit between '" + DeliveryLimit + "' and '" + DeliveryLimit1 + "' and ";

            if (Begin != "" && End != "")
                where += " a.OrderDate between '" + Begin + "' and '" + End + "' and ";

            if (DDID != "")
                where += " a.DDID like'%" + DDID + "%' and";

            if (DDID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = PPManage.SelectDD(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectDDGoods()
        {
            string where = "  1=1 and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string DDID = Request["DDID"];
            if (DDID != "")
            {
                where += " a.DDID ='" + DDID + "'";
            }

            if (DDID == "")
                where = where.Substring(0, where.Length - 4);
            UIDataTable udtTask = PPManage.SelectDDGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult IndexOrder()
        {
            return View();
        }
        public ActionResult Order()
        {

            PP_PurchaseOrder order = new TECOCITY_BGOI.PP_PurchaseOrder();

            order.DDID = PPManage.GetTopListDDID();
            return View(order);
        }

        public ActionResult SelectDDCID()
        {
            string CID = Request["CID"];
            string where = "CID='" + CID + "'";
            DataTable dt = PPManage.SelectDDCID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }

        public ActionResult SelectGoodsDDID()
        {
            string str = " 1=1 and ";
            string DDID = Request["DDID"];
            if (DDID != "")
            {
                str += "a.DDID='" + DDID + "'";
            }
            if (DDID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectGoodsDDID(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SelectCP()
        {

            string str = " 1=1 and ";
            string DDID = Request["DDID"];
            if (DDID != "")
            {
                str += "DDID='" + DDID + "'";
            }
            if (DDID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectCP(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult SelectGoodsDDID1()
        {
            string str = " 1=1 and ";
            string Supplier = Request["Supplier"];
            if (Supplier != "")
            {
                str += " j.COMNameC='" + Supplier + "'";
            }
            if (Supplier == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectGoodsDDID1(str);

            if (dt == null)
                return Json(new { success = false });
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }

        public ActionResult SelectCountDD()
        {
            DataTable dt = PPManage.SelectCountDD();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult InsertOrder()
        {


            PP_PurchaseOrder good = new PP_PurchaseOrder();
            good.OrderDate = Convert.ToDateTime(Request["orderdate"]); ;

            good.DDID = Request["ddid"].ToString();
            good.DeliveryLimit = Request["begin"];
            good.DeliveryMethod = Request["deliverymethod"];
            good.IsInvoice = Request["isinvoice"];
            good.PaymentMethod = Request["paymentmethod"];
            good.PaymentAgreement = Request["paymentagreement"];
            good.ContractNO = Request["contractno"];
            good.TheProject = Request["theproject"];
            good.OrderContacts = Request["ordercontacts"];
            good.OrderUnit = GAccount.GetAccountInfo().UnitID;
            good.DDState = "C";
            good.PayStatus = "1";
            good.PID = Request["tasknum"];
            good.BusinessTypes = Request["businesstypes"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] ljrownumber = Request["ljrownumber"].Split(',');
            string[] ljcpid = Request["ljcpid"].Split(',');
            string[] ljpid = Request["ljpid"].Split(',');
            string[] ljnames = Request["ljnames"].Split(',');
            string[] ljspes = Request["ljspes"].Split(',');
            string[] ljNums = Request["ljNums"].Split(',');
            string[] ljmanufacturer = Request["ljmanufacturer"].Split(',');

            string[] ljunits = Request["ljunits"].Split(',');
            string[] ljunitprice = Request["ljunitprice"].Split(',');
            string[] ljzj = Request["ljzj"].Split(',');
            string[] ljprice2 = Request["ljprice2"].Split(',');
            string[] ljzj2 = Request["ljzj2"].Split(',');

            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < ljrownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + ljrownumber[i];
                pp.DDID = Request["ddid"];
                pp.LJCPID = ljcpid[i];
                pp.MaterialNO = ljpid[i];
                pp.OrderContent = ljnames[i];
                pp.Specifications = ljspes[i];
                pp.Amount = ljNums[i];
                pp.ActualAmount = 0;
                pp.Supplier = ljmanufacturer[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = ljunits[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(ljprice2[i]);
                pp.TotalNoTax = Convert.ToDecimal(ljzj2[i]);
                pp.UnitPrice = Convert.ToDecimal(ljunitprice[i]);
                pp.Total = Convert.ToDecimal(ljzj[i]);
                list.Add(pp);
            }



            string[] cppid = Request["cppid"].Split(',');
            string[] cpname = Request["cpname"].Split(',');
            string[] cpspec = Request["cpspec"].Split(',');
            string[] cpnums = Request["cpnums"].Split(',');
            string[] cpunits = Request["cpunits"].Split(',');

            string[] cpunitprice = Request["cpunitprice"].Split(',');
            string[] cpunitprices = Request["cpunitprices"].Split(',');
            string[] cpprice2 = Request["cpprice2"].Split(',');
            string[] cpprice2s = Request["cpprice2s"].Split(',');
            string strErr = "";


            List<PP_ChoseGoods> cplist = new List<PP_ChoseGoods>();
            int a = 0;
            for (int i = 0; i < cpname.Length; i++)
            {

                a++;
                PP_ChoseGoods cp = new PP_ChoseGoods();
                cp.DDID = Request["ddid"];
                cp.PID = cppid[i];
                cp.Name = cpname[i];
                cp.Spc = cpspec[i];
                cp.Num = cpnums[i];
                cp.Units = cpunits[i];
                cp.SHnum = "0";
                cp.FKnum = "0";

                cp.ID = Request["ddid"] + "-" + a;
                cp.UnitPrice = cpunitprice[i];
                cp.UnitPrices = cpunitprices[i];
                cp.Price2 = cpprice2[i];
                cp.Price2s = cpprice2s[i];
                cplist.Add(cp);
            }
            bool b = PPManage.InsertOrder(good, list, cplist, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.DDID, "增加订购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.DDID, "增加订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult LJSplitsInsert()
        {
            string[] Rownumber = Request["Rownumber"].Split(',');
            string[] Ljcpid = Request["Ljcpid"].Split(',');
            string[] Ljpid = Request["Ljpid"].Split(',');
            string[] Ljnames = Request["Ljnames"].Split(',');
            string[] Ljspes = Request["Ljspes"].Split(',');
            string[] Supplier = Request["Supplier"].Split(',');
            string[] Ljnums = Request["Ljnums"].Split(',');

            string[] Ljunits = Request["Ljunits"].Split(',');
            string[] Ljunitprice = Request["Ljunitprice"].Split(',');
            string[] Ljzj = Request["Ljzj"].Split(',');
            string[] Ljprice2 = Request["Ljprice2"].Split(',');
            string[] Ljzj2 = Request["Ljzj2"].Split(',');

            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < Rownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["DDID"] + "$" + Rownumber[i];
                pp.DDID = Request["DDID"];
                pp.LJCPID = Ljcpid[i];
                pp.MaterialNO = Ljpid[i];
                pp.OrderContent = Ljnames[i];
                pp.Specifications = Ljspes[i];
                pp.Amount = Ljnums[i];
                pp.Supplier = Supplier[i];
                pp.SJFK = "0"; 
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = Ljunits[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(Ljprice2[i]);
                pp.TotalNoTax = Convert.ToDecimal(Ljzj2[i]);
                pp.UnitPrice = Convert.ToDecimal(Ljunitprice[i]);
                pp.Total = Convert.ToDecimal(Ljzj[i]);
                list.Add(pp);
            }
            bool b = PPManage.LJSplitsInsert(  list );
            if (b)
            {
                PPManage.AddRZ(Request["DDID"], "拆分零件", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(Request["DDID"], "拆分零件", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false });
            }
        }
        public ActionResult SavePlanData()
        {
            string strErr = "";

            PP_PurchaseOrder good = new PP_PurchaseOrder();
            string strData = Request["strData"].ToString();
            good.OrderDate = Convert.ToDateTime(Request["OrderDate"]);
            good.DDID = Request["ddid"].ToString();
            good.DeliveryLimit = Request["begin"];
            good.DeliveryMethod = Request["deliverymethod"];
            good.IsInvoice = Request["isinvoice"];
            good.PaymentMethod = Request["paymentmethod"];
            good.PaymentAgreement = Request["paymentagreement"];
            good.ContractNO = Request["contractno"];
            good.TheProject = Request["theproject"];
            good.OrderContacts = Request["ordercontacts"];
            good.OrderUnit = GAccount.GetAccountInfo().UnitID;
            good.DDState = "0";
            good.PayStatus = "1";
            good.PID = Request["tasknum"];
            good.BusinessTypes = Request["businesstypes"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            bool bo = PPManage.SavePlanData(strData, good, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (!bo)
                    return Json(new { success = "false", Msg = "添加失败" });
                else
                    return Json(new { success = "true", SavePlanData = bo });
            }

        }

        public ActionResult UpdateDD()
        {

            PP_PurchaseOrder good = new PP_PurchaseOrder();
            good.OrderDate = Convert.ToDateTime(Request["orderdate"]); ;

            good.DDID = Request["ddid"].ToString();
            good.DeliveryLimit = Request["begin"];
            good.DeliveryMethod = Request["deliverymethod"];
            good.IsInvoice = Request["isinvoice"];
            good.PaymentMethod = Request["paymentmethod"];
            good.PaymentAgreement = Request["paymentagreement"];
            good.ContractNO = Request["contractno"];
            good.TheProject = Request["theproject"];
            good.OrderContacts = Request["ordercontacts"];
            good.OrderUnit = GAccount.GetAccountInfo().UnitID;
            good.DDState = "C";
            good.PayStatus = "1";
            good.PID = Request["tasknum"];
            good.BusinessTypes = Request["businesstypes"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] ljrownumber = Request["ljrownumber"].Split(',');
            string[] ljcpid = Request["ljcpid"].Split(',');
            string[] ljpid = Request["ljpid"].Split(',');
            string[] ljnames = Request["ljnames"].Split(',');
            string[] ljspes = Request["ljspes"].Split(',');
            string[] ljNums = Request["ljNums"].Split(',');
            string[] ljmanufacturer = Request["ljmanufacturer"].Split(',');

            string[] ljunits = Request["ljunits"].Split(',');
            string[] ljunitprice = Request["ljunitprice"].Split(',');
            string[] ljzj = Request["ljzj"].Split(',');
            string[] ljprice2 = Request["ljprice2"].Split(',');
            string[] ljzj2 = Request["ljzj2"].Split(',');

            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < ljrownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + ljrownumber[i];
                pp.DDID = Request["ddid"];
                pp.MaterialNO = ljpid[i];
                pp.LJCPID = ljcpid[i];
                pp.OrderContent = ljnames[i];
                pp.Specifications = ljspes[i];
                pp.Amount = ljNums[i];
                pp.ActualAmount = 0;
                pp.Supplier = ljmanufacturer[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = ljunits[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(ljprice2[i]);
                pp.TotalNoTax = Convert.ToDecimal(ljzj2[i]);
                pp.UnitPrice = Convert.ToDecimal(ljunitprice[i]);
                pp.Total = Convert.ToDecimal(ljzj[i]);
                list.Add(pp);
            }



            string[] cppid = Request["cppid"].Split(',');
            string[] cpname = Request["cpname"].Split(',');
            string[] cpspec = Request["cpspec"].Split(',');
            string[] cpnums = Request["cpnums"].Split(',');
            string[] cpunits = Request["cpunits"].Split(',');


            string[] cpunitprice = Request["cpunitprice"].Split(',');
            string[] cpunitprices = Request["cpunitprices"].Split(',');
            string[] cpprice2 = Request["cpprice2"].Split(',');
            string[] cpprice2s = Request["cpprice2s"].Split(',');
            string strErr = "";


            List<PP_ChoseGoods> cplist = new List<PP_ChoseGoods>();
            int a = 0;
            for (int i = 0; i < cpname.Length; i++)
            {
                a++;
                PP_ChoseGoods cp = new PP_ChoseGoods();
                cp.ID = Request["ddid"] + '-' + a;
                cp.DDID = Request["ddid"];
                cp.PID = cppid[i];
                cp.Name = cpname[i];
                cp.Spc = cpspec[i];
                cp.Num = cpnums[i];
                cp.Units = cpunits[i];
                cp.SHnum = "0";
                cp.FKnum = "0";

                cp.UnitPrice = cpunitprice[i];
                cp.UnitPrices = cpunitprices[i];
                cp.Price2 = cpprice2[i];
                cp.Price2s = cpprice2s[i];
                cplist.Add(cp);
            }
            bool b = PPManage.UpdateDD(good, list, cplist, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.DDID, "修改定单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订单");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.DDID, "修改定单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订单");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult GetOrder()
        {
            return View();
        }
        public ActionResult GetByOrderID()
        {
            string where = " 1=1 ";
            if (Request["DID"] != null)
                where = "b.DID ='" + Request["DID"].ToString() + "' ";
            if (Request["DDID"] != null)
            {
                where = "b.DDID ='" + Request["DDID"].ToString() + "' ";
            }
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = PPManage.GetByOrderID(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";

            if (Request["DID"] != null || Request["DDID"] != null)
            {
                if (udtTask == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", udtTask.DtData) });
            }
            else
            {
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }



        }


        public ActionResult UpdateDDValidate()
        {
            string DDID = Request["DDID"];
            int a = PPManage.UpdateDDValidate(DDID);
            if (a > 0)
            {
                PPManage.AddRZ(DDID, "撤销订单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }

            else
            {
                PPManage.AddRZ(DDID, "撤销订单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false });
            }

        }

        public ActionResult DetailsDD()
        {
            return View();
        }

        public ActionResult UpdateDDXX()
        {
            return View();
        }

        public ActionResult SelectKC()
        {
            DataTable dt = new DataTable();
            dt = PPManage.SelectKC();
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult Selectkooujing()
        {
            string where = Request["chengpin"];
            DataTable dt = new DataTable();
            dt = PPManage.Selectkoujing(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult ChengPin()
        {
            return View();
        }

        public ActionResult SelectLingJ()
        {
            string where = Request["PID"];
            DataTable dt = new DataTable();
            dt = PPManage.SelectLingJ(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }
        public ActionResult SelectLingJXQ()
        {
            string where = "";
            //string pid = Request["PID"];
            string text = Request["text"];
            string cppid = Request["cppid"];
            if (text == null)
            {
                where = "c.ProductID='" + cppid + "'";
            }
            else
            {
                where = " b.COMNameC='" + text + "' and c.ProductID='" + cppid + "'";
            }
            DataTable dt = new DataTable();
            dt = PPManage.SelectLingJXQ(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }


        public ActionResult SelectCPXX()
        {
            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            string ddid = Request["DDID"];
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (ddid != "")
            {
                where = "a.DDID='" + ddid + "'";
            }
            UIDataTable udtTask = PPManage.SelectCPXX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectDDSupplier()
        {
            string where = "";
            string ddid = Request["ddid"];
            string text = Request["text"];
            string cpid = Request["cpid"];
            if (text == null)
            {
                where = "a.PID='" + cpid + "'  and b.ddid='" + ddid + "'";
            }
            else
            {
                where = " d.COMNameC='" + text + "' and a.PID='" + cpid + "'  and b.ddid='" + ddid + "' ";
            }
            DataTable dt = new DataTable();
            dt = PPManage.SelectDDSupplier(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SelectDDCP()
        {
            string str = " 1=1 and ";
            string DDID = Request["ddid"];
            if (DDID != "")
            {
                str += "DDID='" + DDID + "'";
            }
            if (DDID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectDDCP(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region [请购]
        public ActionResult SelectQG()
        {
            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = " type='请购状态'  and a.Validate='1' and ";
            //}
            //else
            //{
            where = " type='请购状态' and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'  and a.Validate='1' and ";
            //}

            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string CID = Request["CID"].ToString();
            string State = Request["State"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string Begin1 = Request["Begin1"].ToString();
            if (Begin1 != "")
                Begin1 += " 00:00:00";
            string End1 = Request["End1"].ToString();
            if (End1 != "")
                End1 += " 23:59:59";
            if (CID != "")
                where += " a.CID like'%" + CID + "%' and";
            if (Begin != "" && End != "")
                where += " a.pleasedate between '" + Begin + "' and '" + End + "' and ";
            if (Begin1 != "" && End1 != "")
                where += " a.deliverydate between '" + Begin1 + "' and '" + End1 + "' and ";
            if (State != "")
                where += " a.State= '" + State + "' and";
            if (State == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = PPManage.SelectQG(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectPurchaseGoodsList()
        {
            string where = " type='请购状态' and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string CID = Request["CID"].ToString();
            string OrderContent = Request["OrderContent"].ToString();
            string State = Request["State"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            string Begin1 = Request["Begin1"].ToString();
            if (Begin1 != "")
                Begin1 += " 00:00:00";
            string End1 = Request["End1"].ToString();
            if (End1 != "")
                End1 += " 23:59:59";


            if (CID != "")
                where += " a.CID like'%" + CID + "%' and";
            if (OrderContent != "")
                where += " OrderContent like '%" + OrderContent + "%' and";
            if (Begin != "" && End != "")
                where += " b.pleasedate between '" + Begin + "' and '" + End + "' and ";
            if (Begin1 != "" && End1 != "")
                where += " a.purchaseDate between '" + Begin1 + "' and '" + End1 + "' and ";
            if (State != "")
                where += " b.State= '" + State + "' and";
            if (State == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = PPManage.SelectPurchaseGoodsList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PurchaseGoodsList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string CID = Request["CID"];


            UIDataTable udtTask = PPManage.PurchaseGoodsList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, CID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SelectGoodsQGID()
        {
            string CID = Request["CID"];
            DataTable dt = PPManage.SelectGoodsQGID(CID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpdatePurchaseGoods()
        {
            return View();
        }
        public ActionResult UpdatePurchaseGoods1()
        {
            string CID = Request["CID"].ToString();
            DataTable dt = PPManage.SelectQGid(CID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult IndexQGXQ()
        {
            return View();
        }
        public ActionResult UpdateQG()
        {
            PP_PurchaseRequisition delist = new PP_PurchaseRequisition();

            string strErr = "";

            delist.CID = Request["CID"];
            delist.State = "0";
            delist.OrderUnit = GAccount.GetAccountInfo().UnitID;
            //delist.PleaseDate = Convert.ToDateTime(Request["PleaseDate"]);
            delist.PleaseDate = Convert.ToDateTime(Request["PleaseDate"]).ToString("yyyy-MM-dd HH:mm:ss");
            delist.PleaseExplain = Request["PleaseExplain"];
            delist.DeliveryDate = Convert.ToDateTime(Request["DeliveryDate"]).ToString("yyyy-MM-dd HH:mm:ss");
            delist.OrderContacts = Request["OrderContacts"];
            delist.CreateTime = DateTime.Now;
            delist.CreateUser = GAccount.GetAccountInfo().UserName;
            delist.Validate = "1";


            string[] RowNumber = Request["RowCount"].Split(',');
            string[] OrderContent = Request["ProName"].Split(',');
            string[] INID = Request["pid"].Split(',');
            string[] Specifications = Request["Spec"].Split(',');
            string[] Unit = Request["Units"].Split(',');
            string[] Amount = Request["Amount"].Split(',');
            string[] UnitpriceNoTax = Request["UnitPrice"].Split(',');
            string[] TotalNoTax = Request["Total"].Split(',');
            string[] GoodsUse = Request["goodsuse"].Split(',');
            string[] supplier = Request["supplier"].Split(',');
            string[] DrawingNum = Request["drawingNum"].Split(',');

            List<PP_PurchaseGoods> list = new List<PP_PurchaseGoods>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_PurchaseGoods record = new PP_PurchaseGoods();
                record.DID = Request["CID"] + "-" + RowNumber[i];
                record.CID = Request["CID"];
                record.INID = INID[i];
                record.OrderContent = OrderContent[i];
                record.Specifications = Specifications[i];
                record.Unit = Unit[i];
                record.Amount = Convert.ToInt32(Amount[i]);
                record.UnitpriceNoTax = Convert.ToDecimal(UnitpriceNoTax[i]);
                record.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                record.GoodsUse = GoodsUse[i];
                record.Supplier = supplier[i];
                record.DrawingNum = DrawingNum[i];
                record.PurchaseDate = Convert.ToDateTime(Request["DeliveryDate"]);
                record.CreateTime = DateTime.Now;
                record.CreateUser = GAccount.GetAccountInfo().UserName;
                record.Validate = "1";
                list.Add(record);
            }

            bool ok = PPManage.UpdateQG(delist, list, ref strErr);
            if (ok)
            {
                PPManage.AddRZ(delist.CID, "修改请购单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(delist.CID, "修改请购单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult InsertQG()
        {

            PP_PurchaseRequisition delist = new PP_PurchaseRequisition();

            string strErr = "";

            delist.CID = Request["CID"];
            delist.State = "0";
            delist.OrderUnit = GAccount.GetAccountInfo().UnitID;
            delist.PleaseDate = Convert.ToDateTime(Request["PleaseDate"]).ToString("yyyy-MM-dd HH:mm:ss");
            delist.PleaseExplain = Request["PleaseExplain"];
            delist.DeliveryDate = Convert.ToDateTime(Request["DeliveryDate"]).ToString("yyyy-MM-dd HH:mm:ss");
            delist.OrderContacts = Request["OrderContacts"];
            delist.OrderNumber = Request["OrderNumber"];

            delist.CreateTime = DateTime.Now;
            delist.CreateUser = GAccount.GetAccountInfo().UserName;
            delist.Validate = "1";


            string[] RowNumber = Request["RowCount"].Split(',');
            string[] OrderContent = Request["ProName"].Split(',');
            string[] INID = Request["pid"].Split(',');
            string[] Specifications = Request["Spec"].Split(',');
            string[] Unit = Request["Units"].Split(',');
            string[] Amount = Request["Amount"].Split(',');
            string[] UnitpriceNoTax = Request["UnitPrice"].Split(',');
            string[] TotalNoTax = Request["Total"].Split(',');
            string[] GoodsUse = Request["goodsuse"].Split(',');
            string[] DrawingNum = Request["drawingNum"].Split(',');
            string[] supplier = Request["supplier"].Split(',');

            List<PP_PurchaseGoods> list = new List<PP_PurchaseGoods>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_PurchaseGoods record = new PP_PurchaseGoods();
                record.DID = Request["CID"] + "-" + RowNumber[i];
                record.CID = Request["CID"];
                record.INID = INID[i];
                record.OrderContent = OrderContent[i];
                record.Specifications = Specifications[i];
                record.Unit = Unit[i];
                record.Amount = Convert.ToInt32(Amount[i]);
                record.UnitpriceNoTax = Convert.ToDecimal(UnitpriceNoTax[i]);
                record.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                record.GoodsUse = GoodsUse[i];
                record.DrawingNum = DrawingNum[i];
                record.Supplier = supplier[i];
                record.PurchaseDate = Convert.ToDateTime(Request["DeliveryDate"]);
                record.CreateTime = DateTime.Now;
                record.CreateUser = GAccount.GetAccountInfo().UserName;
                record.Validate = "1";
                list.Add(record);

                delist.ExpectedTotal += Convert.ToDecimal(TotalNoTax[i]);
            }

            bool ok = PPManage.InsertQG(list, delist, ref strErr);
            if (ok)
            {
                PPManage.AddRZ(delist.CID, "增加请购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(delist.CID, "增加请购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult PrintQG(string id)
        {
            DataTable dt = PPManage.SelectGoodsQGID(id);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">请购单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">请购人</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["UserName"].ToString() + "</td><td class=\"PLeft\">请购日期</td><td class=\"PRight\"  colspan=\"4\">" + Convert.ToDateTime(dt.Rows[0]["PleaseDate"]).ToString("yyyy-MM-dd") + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\">期望交货日期</td><td class=\"PRight\" colspan=\"9\">" + dt.Rows[0]["DeliveryDate"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"PLeft\" >请购说明</td><td  colspan=\"9\">" + dt.Rows[0]["PleaseExplain"].ToString() + "</td></tr>");
            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td><td  style=\"width:10%\" >税前单价</td><td  style=\"width:10%\" >税前总价</td><td  style=\"width:10%\" >备注</td></tr>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["INID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["UnitpriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["GoodsUse"].ToString() + "</td><td  >" + dt.Rows[i]["DrawingNum"].ToString() + "</td><td  >" + dt.Rows[i]["Remark"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();




        }
        public ActionResult QGXQ()
        {

            string CID = Request["CID"].ToString();
            DataTable dt = PPManage.SelectQGXQ(CID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpdateQGValidate()
        {
            string CID = Request["CID"];
            int a = PPManage.UpdateQGValidate(CID);
            if (a != 1)
            {
                PPManage.AddRZ(CID, "删除请购单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = false });
            }
            else
            {
                PPManage.AddRZ(CID, "删除请购单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                return Json(new { success = true });
            }

        }
        public ActionResult CGRemind()
        {
            return View();
        }

        public ActionResult DetailsQG()
        {
            return View();
        }
        public ActionResult UpdateQGXX()
        {
            return View();
        }
        #endregion






        public ActionResult ChangeBasic()
        {
            return View();
        }
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
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string PName = Request["PName"].ToString();
            string PID = Request["PId"].ToString();
            string SPec = Request["Spec"].ToString();
            UIDataTable udtTask = SalesManage.ChangePtypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, Ptype, UnitID, PName, PID, SPec);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

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

            UIDataTable udtTask = SalesManage.GetPtype(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBasicDetail()
        {

            string PID = Request["PID"].ToString();
            string where = "";
            string[] str = PID.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length - 1)
                {
                    where += " a.PID=" + str[i] + " ";
                }
                else
                {
                    where += " a.PID=" + str[i] + " or ";
                }
            }
            DataTable dt = PPManage.GetBasicDetail(where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });


        }

        /// <summary>
        /// 获取供货商 
        /// </summary>
        /// <returns></returns>



        #region [供货商]

        public ActionResult GetSupplier()
        {
            string SID = Request["SID"].ToString();
            DataTable dt = PPManage.GetSupplier(SID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult GetProductPrice()
        {
            string ProID = Request["ProID"].ToString();
            string SupID = Request["SupID"].ToString();
            DataTable dt = PPManage.GetProductPrice(ProID, SupID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        public ActionResult Supplier()
        {
            return View();
        }

        public ActionResult GetSupType()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "9";

            UIDataTable udtTask = SalesManage.GetSupType(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCheckSupList()
        {
            //int a_intPageSize, int a_intPageIndex, string ptype
            string where = Request["ptype"].ToString();
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "2";

            UIDataTable udtTask = SalesManage.GetCheckSupList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }




        #region[收货]

        public ActionResult GetSHID()
        {
            return View();
        }
        public ActionResult PrintSH(string id)
        {
            string where = "a.SHID='" + id + "'";
            DataTable dt = PPManage.SelectSHXX(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">收货单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">收货人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["CreateUser"].ToString() + "</td><td class=\"PLeft\">到货日期</td><td class=\"PRight\"  colspan=\"3\">" + Convert.ToDateTime(dt.Rows[0]["ArrivalDate"]).ToString("yyyy-MM-dd") + "</td></tr>");



            sb.Append("<tr><td class=\"PLeft\" >收货说明</td><td  colspan=\"7\">" + dt.Rows[0]["ArrivalDescription"].ToString() + "</td></tr>");

            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td></tr>  ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["INID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["COMNameC"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }
        public ActionResult ReceiptGoods()
        {
            PP_ReceivingInformation pp = new PP_ReceivingInformation();
            pp.SHID = PPManage.GetTopListSHID();
            return View(pp);
        }
        public ActionResult GetList()
        {
            string CID = Request["CID"].ToString();
            DataTable dt = PPManage.GetList(CID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult SelectGoods()
        {
            string DID = Request["DID"].ToString();
            string where = "Bak='" + DID + "'";
            DataTable dt = PPManage.SelectGoods(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult InsertSH()
        {
            PP_ReceivingInformation pp = new PP_ReceivingInformation();

            string strErr = "";
            string[] DDID = Request["ddid"].Split(',');
            pp.DDID = DDID[0];
            pp.SHID = Request["shid"];
            pp.XXID = "L";
            pp.ArrivalProcess = Request["ArrivalProcess"];
            pp.ArrivalDescription = Request["ArrivalDescription"];
            pp.ArrivalDate = Request["ArrivalDate"];
            pp.OrderUnit = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";


            string[] RowNumber = Request["rownumber"].Split(',');
            string[] OrderContent = Request["proname"].Split(',');
            string[] Specifications = Request["spec"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] Unit = Request["units"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] MaterialNO = Request["materialno"].Split(',');
            string[] SJAmount = Request["sjamount"].Split(',');
            string[] SHAmount = Request["shamount"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] UnitPriceNoTax = Request["unitPriceNoTax"].Split(',');
            string[] TotalNoTax = Request["totalNoTax"].Split(',');
            List<PP_StorageDetailed> list = new List<PP_StorageDetailed>();
            List<string> str = new List<string>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_StorageDetailed stor = new PP_StorageDetailed();
                stor.SHID = Request["shid"];
                stor.DID = Request["shid"] + '-' + RowNumber[i];
                stor.OrderContent = OrderContent[i];
                stor.Specifications = Specifications[i];
                stor.Supplier = Supplier[i];
                stor.Unit = Unit[i];
                stor.Amount = Convert.ToInt32(Amount[i]);
                stor.INID = MaterialNO[i];

                stor.ActualAmount = Convert.ToInt32(SHAmount[i]);
                stor.UnitPriceNoTax = Convert.ToDecimal(UnitPriceNoTax[i]);
                stor.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                stor.CreateTime = DateTime.Now;
                stor.CreateUser = GAccount.GetAccountInfo().UserName;
                stor.Validate = "1";
                stor.SHState = "0";
                stor.THAmount = "0";
                stor.Bak = DID[i];
                list.Add(stor);
                int num = Convert.ToInt32(SJAmount[i]) + Convert.ToInt32(SHAmount[i]);
                str.Add(num.ToString());
            }
            bool b = true;
            if (list.Count != 0)
            {
                b = PPManage.InsertSH(pp, "", list, ref strErr, str);

            }
            if (b)
            {
                PPManage.AddRZ(pp.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult InsertCPSHXX()
        {
            PP_ReceivingInformation pp = new PP_ReceivingInformation();

            string strErr = "";
            pp.DDID = Request["ddid"];
            pp.SHID = Request["shid"];
            pp.XXID = "C";
            pp.ArrivalProcess = Request["ArrivalProcess"];
            pp.ArrivalDescription = Request["ArrivalDescription"];
            pp.ArrivalDate = Request["ArrivalDate"];
            pp.OrderUnit = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";


            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] OrderContent = Request["ljname"].Split(',');
            string[] Specifications = Request["ljspec"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] Amount = Request["ljbcnums"].Split(',');
            string[] MaterialNO = Request["ljid"].Split(',');
            string[] SHAmount = Request["ljbcshnums"].Split(',');
            string[] UnitPriceNoTax = Request["ljprice2"].Split(',');
            string[] TotalNoTax = Request["ljzj2"].Split(',');
            string[] Bak = Request["dddid"].Split(',');
            string[] ljcpid = Request["ljcpid"].Split(',');
            List<PP_StorageDetailed> list = new List<PP_StorageDetailed>();
            for (int i = 0; i < RowNumber.Length; i++)
            {
                PP_StorageDetailed stor = new PP_StorageDetailed();
                stor.SHID = Request["shid"];
                stor.DID = Request["shid"] + '-' + RowNumber[i];
                stor.OrderContent = OrderContent[i];
                stor.Specifications = Specifications[i];
                stor.Supplier = Supplier[i];
                stor.Unit = Unit[i];
                stor.Amount = Convert.ToInt32(Amount[i]);
                stor.INID = MaterialNO[i];
                stor.ActualAmount = Convert.ToInt32(SHAmount[i]);
                stor.UnitPriceNoTax = Convert.ToDecimal(UnitPriceNoTax[i]);
                stor.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                stor.CreateTime = DateTime.Now;
                stor.CreateUser = GAccount.GetAccountInfo().UserName;
                stor.Validate = "1";
                stor.SHState = "0";
                stor.THAmount = "0";
                stor.Bak = Bak[i];
                stor.LJCPID = ljcpid[i];
                list.Add(stor);

            }
            decimal UnitPrices = 0;
            decimal Price2s = 0;
            string[] CPID = Request["cpid"].Split(',');
            string[] PID = Request["cppid"].Split(',');
            string[] SHnum = Request["cpshnums"].Split(',');
            string[] SHnums = Request["sjshnum"].Split(',');
            string[] cprownumberss = Request["cprownumberss"].Split(',');
            string[] cpnamesss = Request["cpname"].Split(',');
            string[] cpspesss = Request["cpspe"].Split(',');
            string[] cpunitsss = Request["cpunits"].Split(',');
            string[] cpnumsss = Request["cpnum"].Split(',');
            string[] cpunitpricess = Request["cpunitprice"].Split(',');
            string[] cpprice2ss = Request["cpprice2"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < cprownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.ID = Request["shid"] + "-" + cprownumberss[i];
                ChoseGoods.DDID = Request["shid"];
                ChoseGoods.PID = PID[i];
                ChoseGoods.Name = cpnamesss[i];
                ChoseGoods.SHnum = SHnum[i];
                ChoseGoods.Num = cpnumsss[i];
                ChoseGoods.Spc = cpspesss[i];
                ChoseGoods.Units = cpunitsss[i];
                ChoseGoods.UnitPrice = cpunitpricess[i];
                ChoseGoods.Price2 = cpprice2ss[i];
                ChoseGoods.FKnum = CPID[i];
                ChoseGoods.Validate = SHnums[i];
                UnitPrices = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpunitpricess[i]);
                Price2s = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpprice2ss[i]);
                ChoseGoods.UnitPrices = UnitPrices.ToString();
                ChoseGoods.Price2s = Price2s.ToString();
                ChoseGoods.CreateTime = DateTime.Now.ToString();
                ChoseGoods.CreateUser = GAccount.GetAccountInfo().UserName;
                ChoseGood.Add(ChoseGoods);
            }



            bool b = true;
            if (list.Count != 0)
            {
                b = PPManage.InsertCPSHXX(pp, "", list, ref strErr, ChoseGood);

            }
            if (b)
            {
                PPManage.AddRZ(pp.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult SelectSHSupplier()
        {
            string where = "";
            string shid = Request["shid"];
            string text = Request["text"];
            string cpid = Request["cpid"];
            if (text == null)
            {
                where = "a.PID='" + cpid + "' and   b.shid='" + shid + "'";
            }
            else
            {
                where = " d.COMNameC='" + text + "' and a.PID='" + cpid + "' and  b.shid='" + shid + "' ";
            }
            DataTable dt = new DataTable();
            dt = PPManage.SelectSHSupplier(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        public ActionResult UpdateCPSHXX()
        {
            PP_ReceivingInformation pp = new PP_ReceivingInformation();

            string strErr = "";
            pp.DDID = Request["ddid"];
            pp.SHID = Request["shid"];
            pp.XXID = "C";
            pp.ArrivalProcess = Request["ArrivalProcess"];
            pp.ArrivalDescription = Request["ArrivalDescription"];
            pp.ArrivalDate = Request["ArrivalDate"];
            pp.OrderUnit = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";


            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] OrderContent = Request["ljname"].Split(',');
            string[] Specifications = Request["ljspec"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] Amount = Request["ljbcnums"].Split(',');
            string[] MaterialNO = Request["ljid"].Split(',');
            string[] SHAmount = Request["ljbcshnums"].Split(',');
            //string[] UnitPriceNoTax = Request["ljprice2"].Split(',');
            //string[] TotalNoTax = Request["ljzj2"].Split(',');
            string[] Bak = Request["dddid"].Split(',');
            string[] ljcpid = Request["ljcpid"].Split(',');
            List<PP_StorageDetailed> list = new List<PP_StorageDetailed>();
            for (int i = 0; i < RowNumber.Length; i++)
            {
                PP_StorageDetailed stor = new PP_StorageDetailed();
                stor.SHID = Request["shid"];
                stor.DID = Request["shid"] + '-' + RowNumber[i];
                stor.OrderContent = OrderContent[i];
                stor.Specifications = Specifications[i];
                stor.Supplier = Supplier[i];
                stor.Unit = Unit[i];
                stor.Amount = Convert.ToInt32(Amount[i]);
                stor.INID = MaterialNO[i];
                stor.ActualAmount = Convert.ToInt32(SHAmount[i]);

                stor.CreateTime = DateTime.Now;
                stor.CreateUser = GAccount.GetAccountInfo().UserName;
                stor.Validate = "1";
                stor.SHState = "0";
                stor.THAmount = "0";
                stor.Bak = Bak[i];
                stor.LJCPID = ljcpid[i];
                list.Add(stor);

            }
            decimal UnitPrices = 0;
            decimal Price2s = 0;
            string[] CPID = Request["cpid"].Split(',');
            string[] PID = Request["cppid"].Split(',');
            string[] SHnum = Request["cpshnums"].Split(',');
            string[] SHnums = Request["sjshnum"].Split(',');
            string[] cprownumberss = Request["cprownumberss"].Split(',');
            string[] cpnamesss = Request["cpname"].Split(',');
            string[] cpspesss = Request["cpspe"].Split(',');
            string[] cpunitsss = Request["cpunits"].Split(',');
            string[] cpnumsss = Request["cpnum"].Split(',');
            string[] cpunitpricess = Request["cpunitprice"].Split(',');
            string[] cpprice2ss = Request["cpprice2"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < cprownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.ID = Request["shid"] + "-" + cprownumberss[i];
                ChoseGoods.DDID = Request["shid"];
                ChoseGoods.PID = PID[i];
                ChoseGoods.Name = cpnamesss[i];
                ChoseGoods.SHnum = SHnum[i];
                ChoseGoods.Num = cpnumsss[i];
                ChoseGoods.Spc = cpspesss[i];
                ChoseGoods.Units = cpunitsss[i];
                ChoseGoods.UnitPrice = cpunitpricess[i];
                ChoseGoods.Price2 = cpprice2ss[i];
                ChoseGoods.FKnum = CPID[i];
                ChoseGoods.Validate = SHnums[i];
                UnitPrices = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpunitpricess[i]);
                Price2s = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpprice2ss[i]);
                ChoseGoods.UnitPrices = UnitPrices.ToString();
                ChoseGoods.Price2s = Price2s.ToString();
                ChoseGoods.CreateTime = DateTime.Now.ToString();
                ChoseGoods.CreateUser = GAccount.GetAccountInfo().UserName;
                ChoseGood.Add(ChoseGoods);
            }



            bool b = true;
            if (list.Count != 0)
            {
                b = PPManage.UpdateCPSHXX(pp, "", list, ref strErr, ChoseGood);

            }
            if (b)
            {
                PPManage.AddRZ(pp.SHID, "修改收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.SHID, "修改收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult IndexSH()
        {
            return View();
        }
        public ActionResult SelectSH()
        {
            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = " Validate = '1' and ";
            //}
            //else
            //{
            where = " Validate = '1' and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "' and ";
            //}


            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string SHID = Request["SHID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";




            if (Begin != "" && End != "")
                where += "  a.ArrivalDate between '" + Begin + "' and '" + End + "' and";
            if (SHID != "")
                where += "  a.SHID like'%" + SHID + "%' and";
            if (SHID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = PPManage.SelectSH(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SelectSHGoods()
        {
            string where = " a.Validate= '1'  and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string SHID = Request["SHID"];

            if (SHID != "" && SHID != null)
            {
                where += " a.SHID like'" + SHID + "' ";
            }
            else
            {
                where = where.Substring(0, where.Length - 4);
                return View();
            }


            UIDataTable udtTask = PPManage.SelectSHGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            if (Request["DID"] != null)
            {
                if (udtTask == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", udtTask.DtData) });
            }
            else
            {
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SelectSHGoodsDID()
        {
            string where = " 1=1 and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string DID = Request["DID"];

            if (DID != null)
                where += " a.DID ='" + DID + "' and";

            if (DID != "")
                where = where.Substring(0, where.Length - 3);
            else
                where = where.Substring(0, where.Length - 4);

            UIDataTable udtTask = PPManage.SelectSHGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            if (Request["DID"] != null)
            {
                if (udtTask == null)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, datas = GFun.Dt2Json("", udtTask.DtData) });
            }
            else
            {
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SelectSHDDID()
        {
            string DDID = Request["DDID"];
            string where = "DDID='" + DDID + "'";
            DataTable dt = PPManage.SelectSHDDID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datass = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }
        public ActionResult SelectSHXX()
        {
            string SHID = Request["SHID"].ToString();
            string where = "1=1";
            DataTable dt = new DataTable();
            if (SHID != "")
            {
                where = "a.SHID='" + SHID + "'";
                dt = PPManage.SelectSHXX(where);
            }
            else
            {
                dt = PPManage.SelectSHXX(where);
            }
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateSH()
        {
            PP_ReceivingInformation pp = new PP_ReceivingInformation();
            List<PP_StorageDetailed> list = new List<PP_StorageDetailed>();
            string strErr = "";
            pp.SHID = Request["shid"];
            pp.ArrivalProcess = Request["arrivalProcess"];
            pp.ArrivalDescription = Request["arrivalDescription"];
            pp.ArrivalDate = Request["arrivalDate"];

            string[] ShuLiang = Request["shuliang"].Split(',');
            string[] ActualAmount = Request["actualamount"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] Bak = Request["bak"].Split(',');
            for (int i = 0; i < ShuLiang.Length; i++)
            {
                PP_StorageDetailed good = new PP_StorageDetailed();
                good.ActualAmount = Convert.ToInt32(ActualAmount[i]);
                good.DID = DID[i];
                good.Bak = Bak[i];
                good.ShuLiang = ShuLiang[i];
                list.Add(good);
            }

            bool b = PPManage.UpdateSH(pp, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(pp.SHID, "修改收货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.SHID, "修改收货单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = false, Msg = strErr });
            }

        }
        public ActionResult UpdateSHValidate()
        {
            string SHID = Request["SHID"];
            string XXID = Request["xxid"];
            int a = PPManage.UpdateSHValidate(SHID, XXID);
            if (a == 0)
            {
                PPManage.AddRZ(SHID, "撤销收货单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = false });
            }

            else
            {
                PPManage.AddRZ(SHID, "撤销收货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                return Json(new { success = true });
            }

        }
        public ActionResult DetailsSH()
        {
            return View();
        }
        public ActionResult UpdateSHXX()
        {
            return View();
        }

        public ActionResult InsertCPSH()
        {

            PP_ReceivingInformation pp = new PP_ReceivingInformation();
            pp.SHID = PPManage.GetTopListSHID();
            return View(pp);
        }

        public ActionResult SelectSHCPXX()
        {
            string where = "";
            string shid = Request["shid"];
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (shid != "")
            {
                where = "a.shid='" + shid + "'";
            }
            UIDataTable udtTask = PPManage.SelectSHCPXX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateSHXXer()
        {
            return View();
        }
        public ActionResult SelectSHCP()
        {

            string str = " 1=1 and ";
            string SHID = Request["shid"];
            if (SHID != "")
            {
                str += "SHID='" + SHID + "'";
            }
            if (SHID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectSHCP(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region[退货]
        public ActionResult ReturnGoods()
        {

            PP_ReturnGoods th = new PP_ReturnGoods();
            th.THID = PPManage.GetTopListTHID();
            return View(th);

        }
        public ActionResult InsertTH()
        {

            PP_ReturnGoods th = new PP_ReturnGoods();
            th.SHID = Request["SHID"];
            th.THID = Request["THID"];
            th.ReturnHandler = Request["returnhandler"];
            th.ReturnDate = Request["returndate"];
            th.ReturnType = Request["returntype"];
            th.ReturnMode = Request["returnmode"];
            th.ReturnAgreement = Request["returnagreement"];
            th.TheProject = Request["theproject"];
            th.ReturnDescription = Request["returndescription"];
            th.OrderUnit = GAccount.GetAccountInfo().UnitID;
            th.CreateTime = DateTime.Now;
            th.CreateUser = GAccount.GetAccountInfo().UserName;
            th.Validate = "1";

            string strErr = "";
            string[] RowNumBer = Request["rownumber"].Split(',');
            string[] OrderContent = Request["proname"].Split(',');
            string[] Specifications = Request["spec"].Split(',');
            string[] XXID = Request["xxid"].Split(',');
            string[] Unit = Request["units"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] UnitPriceNoTax = Request["unitPricenotax"].Split(',');
            string[] TotalNoTax = Request["totalnotax"].Split(',');
            string[] THAmount = Request["thamount"].Split(',');
            string[] DID = Request["did"].Split(',');
            List<PP_ReturngoodsDetails> list = new List<PP_ReturngoodsDetails>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_ReturngoodsDetails good = new PP_ReturngoodsDetails();
                good.EID = Request["THID"];
                good.INID = XXID[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Amount[i];
                good.Supplier = Supplier[i];
                good.UnitPriceNoTax = UnitPriceNoTax[i];
                good.TotalNoTax = TotalNoTax[i];
                good.DID = Request["THID"] + "-" + RowNumBer[i];
                int a = 0;
                if (THAmount[i] == "")
                {
                    a = Convert.ToInt32(Amount[i]);
                }
                else
                {
                    a = Convert.ToInt32(THAmount[i]) + Convert.ToInt32(Amount[i]);
                }

                good.THAmount = a.ToString();
                good.Bak = DID[i];
                list.Add(good);
            }
            bool b = PPManage.InsertTH(th, "", list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(th.THID, "增加退货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(th.THID, "增加退货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = false, Msg = strErr });
            }

        }

        public ActionResult PrintTH(string id)
        {
            string where = "a.THID='" + id + "'";
            DataTable dt = PPManage.SelectTHXQ(where);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">退货单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">退货人</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["ReturnHandler"].ToString() + "</td><td class=\"PLeft\">退货日期</td><td class=\"PRight\"  colspan=\"3\">" + Convert.ToDateTime(dt.Rows[0]["ReturnDate"]).ToString("yyyy-MM-dd") + "</td></tr>");



            sb.Append("<tr><td class=\"PLeft\" >退货类型</td><td  colspan=\"2\">" + dt.Rows[0]["THLX"].ToString() + "</td><td class=\"PLeft\" >退货方式</td><td  colspan=\"2\">" + dt.Rows[0]["THFS"].ToString() + "</td><td class=\"PLeft\" >退货约定</td><td  colspan=\"2\">" + dt.Rows[0]["ReturnAgreement"].ToString() + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\" >所属项目</td><td  colspan=\"2\">" + dt.Rows[0]["TheProject"].ToString() + "</td><td class=\"PLeft\" >退货说明</td><td  colspan=\"5\">" + dt.Rows[0]["ReturnDescription"].ToString() + "</td></tr>");

            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td><td  style=\"width:10%\" >备注</td></tr>  ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["INID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["Supplier"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["GoodsUse"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }
        public ActionResult IndexTH()
        {
            return View();
        }
        public ActionResult SelectTHDDID()
        {
            string SHID = Request["SHID"];
            string where = "SHID='" + SHID + "'";
            DataTable dt = PPManage.SelectTHDDID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }

        public ActionResult SelectTH()
        {

            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = "  Validate='1' and ";
            //}
            //else
            //{
            where = " Validate = '1' and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "' and ";
            //}



            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Begin != "" && End != "")
                where += "  a.returndate between '" + Begin + "' and '" + End + "' and ";
            string THID = Request["THID"];

            if (THID != "")
                where += " a.THID like'%" + THID + "%' and";

            if (THID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = PPManage.SelectTH(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectTHGoods()
        {
            string where = "";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string EID = Request["EID"];
            where += " a.EID like'" + EID + "'";
            UIDataTable udtTask = PPManage.SelectTHGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectTHXQ()
        {
            string THID = Request["THID"];
            string where = "1=1";
            DataTable dt = new DataTable();
            if (THID != "")
            {
                where = "a.THID='" + THID + "'";
                dt = PPManage.SelectTHXQ(where);
            }
            else
            {
                dt = PPManage.SelectTHXQ(where);
            }
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateTH()
        {
            string strErr = "";
            List<PP_ReturngoodsDetails> list = new List<PP_ReturngoodsDetails>();
            PP_ReturnGoods th = new PP_ReturnGoods();
            th.THID = Request["THID"];
            th.ReturnHandler = Request["returnhandler"];
            th.ReturnDate = Request["returndate"];
            th.ReturnType = Request["returntype"];
            th.ReturnMode = Request["returnmode"];
            th.ReturnAgreement = Request["returnagreement"];
            th.TheProject = Request["theproject"];
            th.ReturnDescription = Request["returndescription"];

            string[] Amount = Request["amount"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] SL = Request["shuliang"].Split(',');
            string[] Bak = Request["bak"].Split(',');
            for (int i = 0; i < Amount.Length; i++)
            {
                PP_ReturngoodsDetails good = new PP_ReturngoodsDetails();

                good.Bak = Bak[i];
                good.shuliang = SL[i];
                good.Amount = Amount[i];
                good.DID = DID[i];
                list.Add(good);
            }

            bool b = PPManage.UpdateTH(th, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(th.THID, "修改退货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(th.THID, "修改退货单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = false, Msg = strErr });
            }

        }

        public ActionResult UpdateTHValidate()
        {
            string THID = Request["THID"];

            int a = PPManage.UpdateTHValidate(THID);
            if (a == 0)
            {
                PPManage.AddRZ(THID, "撤销退货单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = false });
            }

            else
            {
                PPManage.AddRZ(THID, "撤销退货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                return Json(new { success = true });
            }

        }

        public ActionResult DetailsTH()
        {
            return View();
        }

        public ActionResult UpdateTHXX()
        {
            return View();
        }
        #endregion

        #region[入库单]
        public ActionResult AddStorage()
        {
            PP_PurchaseInventorys PP = new PP_PurchaseInventorys();
            PP.RKID = PPManage.GetTopListRKID();
            return View(PP);
        }

        public ActionResult InsertRK()
        {
            PP_PurchaseInventorys pp = new PP_PurchaseInventorys();

            string strErr = "";
            pp.SHID = Request["SHID"];
            pp.RKID = Request["RKID"];
            pp.Handler = Request["handler"];
            pp.Rkdate = Convert.ToDateTime(Request["rkdate"]);
            pp.RKInstructions = Request["rkinstructions"];
            pp.CKID = Request["CKID"];
            pp.State = "0";
            pp.UnitID = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";

            string[] RowNumber = Request["rownumber"].Split(',');
            string[] OrderContent = Request["proname"].Split(',');
            string[] Specifications = Request["spec"].Split(',');
            string[] Unit = Request["units"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] MaterialNO = Request["materialno"].Split(',');
            string[] UnitPriceNoTax = Request["unitPricenotax"].Split(',');
            string[] TotalNoTax = Request["totalnotax"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] SJAmount = Request["sjamount"].Split(',');
            string[] RKState = Request["rkstate"].Split(',');
            List<PP_GoodsreceiptDetailed> list = new List<PP_GoodsreceiptDetailed>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_GoodsreceiptDetailed good = new PP_GoodsreceiptDetailed();
                good.RKID = Request["RKID"];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Convert.ToInt32(Amount[i]);
                good.Supplier = Supplier[i];
                good.DID = Request["RKID"] + "-" + RowNumber[i];
                good.UnitPriceNoTax = UnitPriceNoTax[i];
                good.TotalNoTax = TotalNoTax[i];
                good.Bak = DID[i];
                good.INID = MaterialNO[i];
                int a = Convert.ToInt32(RKState[i]) + Convert.ToInt32(SJAmount[i]);
                good.ShuLiang = a.ToString();
                good.SJAmount = SJAmount[i];
                list.Add(good);
            }


            bool b = PPManage.InsertRK(pp, "", list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = false, Msg = strErr });
            }
        }


        public ActionResult InsertCPRK()
        {
            PP_PurchaseInventorys pp = new PP_PurchaseInventorys();

            string strErr = "";
            pp.SHID = Request["shid"];
            pp.RKID = Request["rkid"];
            pp.Handler = Request["ordercontacts"];
            pp.Rkdate = Convert.ToDateTime(Request["begin"]);
            pp.RKInstructions = Request["rkinstructions"];
            pp.CKID = Request["ckid"];
            pp.RKType = "C";
            pp.UnitID = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";

            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] SHDID = Request["shdid"].Split(',');
            string[] LJCPID = Request["ljcpid"].Split(',');
            string[] MaterialNO = Request["ljid"].Split(',');
            string[] OrderContent = Request["ljname"].Split(',');
            string[] Specifications = Request["ljspec"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] Amount = Request["ljnums"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] UnitPriceNoTax = Request["ljprice2"].Split(',');
            string[] TotalNoTax = Request["ljzj2"].Split(',');
            string[] SJAmount = Request["ljbcnums"].Split(',');
            string[] Bak = Request["ljbcrknums"].Split(',');
            List<PP_GoodsreceiptDetailed> list = new List<PP_GoodsreceiptDetailed>();
            for (int i = 0; i < RowNumber.Length; i++)
            {
                PP_GoodsreceiptDetailed good = new PP_GoodsreceiptDetailed();
                good.RKID = Request["rkid"];
                good.DID = Request["rkid"] + "-" + RowNumber[i];
                good.LJCPID = LJCPID[i];
                good.INID = MaterialNO[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Convert.ToInt32(Amount[i]);
                good.Supplier = Supplier[i];
                good.UnitPriceNoTax = UnitPriceNoTax[i];
                good.TotalNoTax = TotalNoTax[i];
                good.SJAmount = SJAmount[i];
                good.Bak = Bak[i];
                good.SHDID = SHDID[i];
                list.Add(good);
            }

            decimal UnitPrices = 0;
            decimal Price2s = 0;
            string[] cprownumberss = Request["cprownumberss"].Split(',');
            string[] CPID = Request["cpid"].Split(',');
            string[] CPPID = Request["id"].Split(',');
            string[] RKnums = Request["sjrknum"].Split(',');

            string[] cpnamesss = Request["cpname"].Split(',');
            string[] cpspesss = Request["cpspe"].Split(',');
            string[] cpunitsss = Request["cpunits"].Split(',');
            string[] cpnumsss = Request["cpnum"].Split(',');
            string[] cpunitpricess = Request["cpunitprice"].Split(',');
            string[] cpprice2ss = Request["cpprice2"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < cprownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.ID = Request["rkid"] + "-" + cprownumberss[i];
                ChoseGoods.DDID = Request["rkid"];
                ChoseGoods.PID = CPID[i];
                ChoseGoods.Name = cpnamesss[i];
                ChoseGoods.Num = cpnumsss[i];
                ChoseGoods.Spc = cpspesss[i];
                ChoseGoods.Units = cpunitsss[i];
                ChoseGoods.UnitPrice = cpunitpricess[i];
                ChoseGoods.Price2 = cpprice2ss[i];
                ChoseGoods.FKnum = RKnums[i];
                ChoseGoods.Validate = CPPID[i];
                UnitPrices = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpunitpricess[i]);
                Price2s = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpprice2ss[i]);
                ChoseGoods.UnitPrices = UnitPrices.ToString();
                ChoseGoods.Price2s = Price2s.ToString();
                ChoseGoods.CreateTime = DateTime.Now.ToString();
                ChoseGoods.CreateUser = GAccount.GetAccountInfo().UserName;

                ChoseGood.Add(ChoseGoods);
            }

            bool b = PPManage.InsertCPRK(pp, "", list, ref strErr, ChoseGood);
            if (b)
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult SelectRKGoods()
        {
            string where = " ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string RKID = Request["RKID"].ToString();
            where += " a.RKID like'" + RKID + "' ";

            UIDataTable udtTask = PPManage.SelectRKGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectRK()
        {
            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = " Validate='1' and ";
            //}
            //else
            //{
            where = "  a.UnitID='" + GAccount.GetAccountInfo().UnitID + "'  and a.Validate='1' and ";
            //}



            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string RKID = Request["RKID"].ToString();
            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Begin != "" && End != "")
                where += " a.Rkdate between '" + Begin + "' and '" + End + "' and ";
            if (RKID != "")
                where += " a.RKID like'%" + RKID + "%' and";
            if (RKID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = PPManage.SelectRK(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult IndexRK()
        {
            return View();
        }

        public ActionResult PrintRK(string id)
        {
            DataTable dt = PPManage.SelectRKXQ(id);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">入库单-YSGLJL-SC-F14</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">入库人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["Handler"].ToString() + "</td><td class=\"PLeft\">入库日期</td><td class=\"PRight\"  colspan=\"3\">" + Convert.ToDateTime(dt.Rows[0]["Rkdate"]).ToString("yyyy-MM-dd") + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\">入库库房</td><td class=\"PRight\" colspan=\"1\">" + dt.Rows[0]["HouseName"].ToString() + "</td><td class=\"PLeft\">入库说明</td><td class=\"PRight\" colspan=\"5\">" + dt.Rows[0]["RKInstructions"].ToString() + "</td></tr>");


            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td></tr>  ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["INID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Amount"].ToString() + "</td><td  >" + dt.Rows[i]["COMNameC"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());

            return View();
        }

        public ActionResult IndexRKXQ()
        {
            return View();
        }
        public ActionResult RKXQ()
        {
            string RKID = Request["RKID"];
            DataTable dt = PPManage.SelectRKXQ(RKID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });

        }

        public ActionResult SelectRKDDID()
        {
            string SHID = Request["SHID"];
            string where = "SHID='" + SHID + "'";
            DataTable dt = PPManage.SelectRKDDID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }
        public ActionResult SelectGoodsreceiptDetailed()
        {
            string DDID = Request["DID"];
            string where = "Bak='" + DDID + "'";
            DataTable dt = PPManage.SelectGoodsreceiptDetailed(where);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult UpdateRK()
        {
            PP_PurchaseInventorys pp = new PP_PurchaseInventorys();

            string strErr = "";
            pp.SHID = Request["SHID"];
            pp.RKID = Request["RKID"];
            pp.Handler = Request["handler"];
            pp.Rkdate = Convert.ToDateTime(Request["rkdate"]);
            pp.RKInstructions = Request["rkinstructions"];
            pp.CKID = Request["CKID"];

            List<PP_GoodsreceiptDetailed> list = new List<PP_GoodsreceiptDetailed>();
            string[] DID = Request["DID"].Split(',');
            string[] ShuLiang = Request["shuliang"].Split(',');
            string[] Bak = Request["Bak"].Split(',');
            string[] SJAmount = Request["sjamount"].Split(',');
            for (int i = 0; i < DID.Length; i++)
            {
                PP_GoodsreceiptDetailed good = new PP_GoodsreceiptDetailed();
                good.DID = DID[i];
                good.Bak = Bak[i];
                good.SJAmount = SJAmount[i];
                good.ShuLiang = ShuLiang[i];
                list.Add(good);
            }

            bool b = PPManage.UpdateRK(pp, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(pp.RKID, "修改入库单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.RKID, "修改入库单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult DetailsRK()
        {
            return View();
        }

        public ActionResult UpdateRKXX()
        {
            return View();
        }


        public ActionResult UpdateRKValidate()
        {
            string RKID = Request["RKID"];
            string rktype = Request["rktype"];
            int a = PPManage.UpdateRKValidate(RKID, rktype);
            if (a == 0)
            {
                PPManage.AddRZ(RKID, "撤销入库单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = false });
            }

            else
            {
                PPManage.AddRZ(RKID, "撤销入库单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = true });
            }

        }

        public ActionResult ADDStorageer()
        {
            PP_PurchaseInventorys PP = new PP_PurchaseInventorys();
            PP.RKID = PPManage.GetTopListRKID();
            return View(PP);
        }


        public ActionResult SelectRKSupplier()
        {
            string where = "";
            string rkid = Request["rkid"];
            string text = Request["text"];
            string cpid = Request["cpid"];
            if (text == null)
            {
                where = "a.PID='" + cpid + "' and c.ProductID='" + cpid + "' and b.rkid='" + rkid + "'";
            }
            else
            {
                where = " d.COMNameC='" + text + "' and a.PID='" + cpid + "' and c.ProductID='" + cpid + "' and b.rkid='" + rkid + "'";
            }
            DataTable dt = new DataTable();
            dt = PPManage.SelectRKSupplier(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }


        public ActionResult SelectRKCPXX()
        {
            string where = "";
            string rkid = Request["rkid"];
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (rkid != "")
            {
                where = "a.RKID='" + rkid + "' ";
            }
            UIDataTable udtTask = PPManage.SelectRKCPXX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRKXXer()
        {
            return View();
        }

        public ActionResult SelectRKCP()
        {
            string where = "";
            string rkid = Request["rkid"];
            where = "  RKID='" + rkid + "'";

            DataTable dt = new DataTable();
            dt = PPManage.SelectRKCP(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateCPRK()
        {
            PP_PurchaseInventorys pp = new PP_PurchaseInventorys();

            string strErr = "";
            pp.SHID = Request["shid"];
            pp.RKID = Request["rkid"];
            pp.Handler = Request["ordercontacts"];
            pp.Rkdate = Convert.ToDateTime(Request["begin"]);
            pp.RKInstructions = Request["rkinstructions"];
            pp.CKID = Request["ckid"];
            pp.RKType = "C";
            pp.UnitID = GAccount.GetAccountInfo().UnitID;
            pp.CreateTime = DateTime.Now;
            pp.CreateUser = GAccount.GetAccountInfo().UserName;
            pp.Validate = "1";

            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] MaterialNO = Request["ljid"].Split(',');
            string[] OrderContent = Request["ljname"].Split(',');
            string[] Specifications = Request["ljspec"].Split(',');
            string[] Amount = Request["ljnums"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] UnitPriceNoTax = Request["ljprice2"].Split(',');
            string[] TotalNoTax = Request["ljzj2"].Split(',');

            string[] LJCPID = Request["ljcpid"].Split(',');
            string[] BCRKnum = Request["ljbcnums"].Split(',');
            string[] ljzqnums = Request["ljzqnums"].Split(',');
            string[] LJyrnums = Request["LJyrnums"].Split(',');
            string[] SHDID = Request["shdid"].Split(',');
            List<PP_GoodsreceiptDetailed> list = new List<PP_GoodsreceiptDetailed>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_GoodsreceiptDetailed good = new PP_GoodsreceiptDetailed();
                good.RKID = Request["rkid"];
                good.SHDID = SHDID[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Convert.ToInt32(Amount[i]);
                good.Supplier = Supplier[i];
                good.DID = Request["rkid"] + "-" + RowNumber[i];
                good.UnitPriceNoTax = UnitPriceNoTax[i];
                good.TotalNoTax = TotalNoTax[i];
                good.INID = MaterialNO[i];
                good.SJAmount = BCRKnum[i];
                good.LJCPID = LJCPID[i];
                good.Bak = (Convert.ToInt32(LJyrnums[i]) + (Convert.ToInt32(BCRKnum[i]) - Convert.ToInt32(ljzqnums[i]))).ToString();
                list.Add(good);
            }

            decimal UnitPrices = 0;
            decimal Price2s = 0;
            string[] CPID = Request["cpid"].Split(',');
            string[] PID = Request["cppid"].Split(',');
            string[] cprownumberss = Request["cprownumberss"].Split(',');
            string[] cpnamesss = Request["cpname"].Split(',');
            string[] cpspesss = Request["cpspe"].Split(',');
            string[] cpunitsss = Request["cpunits"].Split(',');
            string[] cpnumsss = Request["cpnum"].Split(',');
            string[] cpunitpricess = Request["cpunitprice"].Split(',');
            string[] cpprice2ss = Request["cpprice2"].Split(',');
            string[] cppids = Request["cppids"].Split(',');
            string[] cprknums = Request["cprknums"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < cprownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.ID = Request["rkid"] + "-" + cprownumberss[i];
                ChoseGoods.DDID = Request["rkid"];
                ChoseGoods.PID = PID[i];
                ChoseGoods.Name = cpnamesss[i];
                ChoseGoods.Num = cpnumsss[i];
                ChoseGoods.Spc = cpspesss[i];
                ChoseGoods.Units = cpunitsss[i];
                ChoseGoods.UnitPrice = cpunitpricess[i];
                ChoseGoods.Price2 = cpprice2ss[i];
                ChoseGoods.Validate = cppids[i];
                ChoseGoods.SHnum = cprknums[i];
                UnitPrices = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpunitpricess[i]);
                Price2s = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpprice2ss[i]);
                ChoseGoods.UnitPrices = UnitPrices.ToString();
                ChoseGoods.Price2s = Price2s.ToString();
                ChoseGoods.CreateTime = DateTime.Now.ToString();
                ChoseGoods.CreateUser = GAccount.GetAccountInfo().UserName;

                ChoseGood.Add(ChoseGoods);
            }

            bool b = PPManage.UpdateCPRK(pp, "", list, ref strErr, ChoseGood);
            if (b)
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.RKID, "添加入库单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                return Json(new { success = false, Msg = strErr });
            }
        }
        #endregion

        #region[付款]


        public ActionResult SelectFKSupplier()
        {
            string where = "";
            string fkid = Request["fkid"];
            string text = Request["text"];
            string cpid = Request["cpid"];
            if (text == null)
            {
                where = "a.PID='" + cpid + "' and   b.payid='" + fkid + "'";
            }
            else
            {
                where = " d.COMNameC='" + text + "' and a.PID='" + cpid + "'   and b.payid='" + fkid + "' ";
            }
            DataTable dt = new DataTable();
            dt = PPManage.SelectFKSupplier(where);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult Payment()
        {
            PP_Payment pay = new PP_Payment();
            pay.PayId = PPManage.GetTopListFKID();
            return View(pay);
        }
        public ActionResult InsertFK()
        {
            PP_Payment pay = new PP_Payment();

            pay.DDID = Request["ddid"];
            pay.PayId = Request["payid"];
            pay.PaymentMenthod = Request["paymentmenthod"];
            pay.PayCompany = GAccount.GetAccountInfo().UnitID;
            pay.PayTime = Convert.ToDateTime(Request["paytime"]);
            pay.State = Request["state"];
            pay.OrderContacts = Request["ordercontacts"];
            pay.CreateTime = DateTime.Now;
            pay.CreateUser = GAccount.GetAccountInfo().UserName;
            pay.Validate = "1";
            pay.Remark = "C";

            string strErr = "";
            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] DID = Request["dddid"].Split(',');
            string[] LJCPID = Request["ljcpid"].Split(',');
            string[] INID = Request["ljid"].Split(',');
            string[] OrderContent = Request["ljnames"].Split(',');
            string[] Specifications = Request["ljspes"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] Amount = Request["ljnums"].Split(',');
            string[] Rate = Request["ljbcnums"].Split(',');
            string[] LJBCFKnums = Request["ljbcfknums"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] UnitPriceNoTax = Request["ljprice2"].Split(',');
            string[] TotalNoTax = Request["ljzj2"].Split(',');
            string[] UnitPrice = Request["ljunitprice"].Split(',');
            string[] Total = Request["ljzj"].Split(',');

            List<PP_PaymentGoods> list = new List<PP_PaymentGoods>();
            List<string> str = new List<string>();
            for (int i = 0; i < RowNumber.Length; i++)
            {

                PP_PaymentGoods good = new PP_PaymentGoods();
                good.PayId = Request["PayId"];
                good.PayXid = Request["PayId"] + "-" + RowNumber[i];
                good.LJCPID = LJCPID[i];
                good.DID = DID[i];
                good.INID = INID[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Amount[i];
                good.Supplier = Supplier[i];
                good.UnitPriceNoTax = Convert.ToDecimal(UnitPriceNoTax[i]);
                good.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                good.UnitPrice = Convert.ToDouble(UnitPrice[i]);
                good.Total = Convert.ToDouble(Total[i]);
                good.Rate = Rate[i];
                good.GoodsUse = LJBCFKnums[i];
                list.Add(good);
            }
            decimal UnitPrices = 0;
            decimal Price2s = 0;
            string[] CPPID = Request["cpid"].Split(',');
            string[] PID = Request["cppid"].Split(',');
            string[] cprownumberss = Request["cprownumberss"].Split(',');
            string[] cpnamesss = Request["cpname"].Split(',');
            string[] cpspesss = Request["cpspe"].Split(',');
            string[] cpunitsss = Request["cpunits"].Split(',');
            string[] cpnumsss = Request["cpnum"].Split(',');
            string[] cpunitpricess = Request["cpunitprice"].Split(',');
            string[] cpprice2ss = Request["cpprice2"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < cprownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.ID = Request["payid"] + "-" + cprownumberss[i];
                ChoseGoods.DDID = Request["payid"];
                ChoseGoods.PID = PID[i];
                ChoseGoods.FKnum = CPPID[i];
                ChoseGoods.Name = cpnamesss[i];
                ChoseGoods.Num = cpnumsss[i];
                ChoseGoods.Spc = cpspesss[i];
                ChoseGoods.Units = cpunitsss[i];
                ChoseGoods.UnitPrice = cpunitpricess[i];
                ChoseGoods.Price2 = cpprice2ss[i];
                UnitPrices = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpunitpricess[i]);
                Price2s = Convert.ToInt32(cpnumsss[i]) * Convert.ToDecimal(cpprice2ss[i]);
                ChoseGoods.UnitPrices = UnitPrices.ToString();
                ChoseGoods.Price2s = Price2s.ToString();
                ChoseGood.Add(ChoseGoods);
            }
            bool b = PPManage.InsertFK(pay, "", list, ref strErr, ChoseGood);
            if (b)
            {
                PPManage.AddRZ(pay.PayId, "订单付款", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pay.PayId, "订单付款", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult InsertLJFK()
        {
            PP_Payment pay = new PP_Payment();

            pay.DDID = Request["ddid"];
            pay.PayId = Request["PayId"];
            pay.PaymentMenthod = Request["PaymentMenthod"];
            pay.PayCompany = GAccount.GetAccountInfo().UnitID;
            pay.PayTime = Convert.ToDateTime(Request["PayTime"]);
            pay.State = Request["State"];
            pay.Remark = "L";
            pay.OrderContacts = Request["OrderContacts"];
            pay.CreateTime = DateTime.Now;
            pay.CreateUser = GAccount.GetAccountInfo().UserName;
            pay.Validate = "1";

            string strErr = "";
            string[] RowNumber = Request["RowNumber"].Split(',');
            string[] OrderContent = Request["ProName"].Split(',');
            string[] Specifications = Request["Spec"].Split(',');
            string[] Unit = Request["Units"].Split(',');
            string[] Amount = Request["Amount"].Split(',');
            string[] Supplier = Request["Supplier"].Split(',');
            string[] UnitPriceNoTax = Request["UnitPriceNoTax"].Split(',');
            string[] TotalNoTax = Request["totalnotax"].Split(',');

            string[] UnitPrice = Request["unitprice"].Split(',');
            string[] Total = Request["total"].Split(',');

            string[] INID = Request["inid"].Split(',');
            string[] Remark = Request["remark"].Split(',');
            string[] Rate = Request["rate"].Split(',');
            string[] Rates = Request["rates"].Split(',');
            string[] DID = Request["did"].Split(',');
            List<PP_PaymentGoods> list = new List<PP_PaymentGoods>();
            List<string> str = new List<string>();
            for (int i = 0; i < OrderContent.Length; i++)
            {

                PP_PaymentGoods good = new PP_PaymentGoods();
                good.PayId = Request["PayId"];
                good.PayXid = Request["PayId"] + "-" + RowNumber[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Amount[i];
                good.Supplier = Supplier[i];
                good.UnitPriceNoTax = Convert.ToDecimal(UnitPriceNoTax[i]);
                good.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);

                good.UnitPrice = Convert.ToDouble(UnitPrice[i]);
                good.Total = Convert.ToDouble(Total[i]);


                good.INID = INID[i];
                good.Remark = Remark[i];
                good.Rate = Rates[i];
                good.DID = DID[i];
                list.Add(good);
                double zongjjine = double.Parse(Rates[i]) + double.Parse(Rate[i]);
                str.Add(zongjjine.ToString());
            }
            bool b = PPManage.InsertLJFK(pay, "", list, ref strErr, str);
            if (b)
            {
                PPManage.AddRZ(pay.PayId, "订单付款", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pay.PayId, "订单付款", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult IndexFK()
        {
            return View();
        }
        public ActionResult SelectFKDDID()
        {
            string DDID = Request["DDID"];
            string where = "DDID='" + DDID + "'";
            DataTable dt = PPManage.SelectFKDDID(where);

            if (dt.Rows.Count > 0)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }


        public ActionResult SelectFK()
        {


            string danwei = GAccount.GetAccountInfo().UnitID;
            string where = "";
            //if (danwei == "32" || danwei == "46")
            //{
            //    where = "  b.Type='付款状态' and a.Validate='1' and ";
            //}
            //else
            //{

            where = "  b.Type='付款状态' and a.PayCompany='" + GAccount.GetAccountInfo().UnitID + "' and a.Validate='1' and ";
            //}


            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string Begin = Request["Begin"].ToString();
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Begin != "" && End != "")
                where += " a.PayTime between '" + Begin + "' and '" + End + "' and ";
            string State = Request["State"];
            if (State != "")
                where += " a.State like'%" + State + "%' and ";
            string PayId = Request["PayId"];
            if (PayId != "")
                where += " a.PayId like'%" + PayId + "%' and";
            if (PayId == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = PPManage.SelectFK(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectFKGoods()
        {
            string where = " 1=1 ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string PayId = Request["PayId"];
            if (PayId != "")
            {
                where = " a.PayId like'" + PayId + "' and Rate > '0'";
            }

            UIDataTable udtTask = PPManage.SelectFKGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailsFK()
        {
            return View();
        }

        public ActionResult SelectFKXQ()
        {
            string PayID = Request["PayID"];
            string str = "1=1";
            if (PayID != "")
            {
                str = "a.PayId='" + PayID + "'";
            }

            DataTable dt = PPManage.SelectFKXQ(str);

            if (dt != null)
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            else
                return Json(new { success = false });
        }

        public ActionResult PrintFK(string id)
        {

            string str = "a.PayId='" + id + "' and b.Rate > '0'";
            DataTable dt = PPManage.SelectFKXQ(str);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">付款单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");
            sb.Append("<tr><td class=\"PLeft\">付款人</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["OrderContacts"].ToString() + "</td><td class=\"PLeft\">付款日期</td><td class=\"PRight\"  colspan=\"4\">" + Convert.ToDateTime(dt.Rows[0]["PayTime"]).ToString("yyyy-MM-dd") + "</td></tr>");

            sb.Append("<tr><td class=\"PLeft\">付款方式</td><td class=\"PRight\" colspan=\"3\">" + dt.Rows[0]["ZF"].ToString() + "</td><td class=\"PLeft\">付费状态</td><td class=\"PRight\" colspan=\"4\">" + dt.Rows[0]["FK"].ToString() + "</td></tr>");


            sb.Append("<tr><td style=\"width:10%\" >编码</td><td style=\"width:10%\"  >名称</td><td  style=\"width:10%\" >型号</td><td  style=\"width:10%\" >单位</td><td style=\"width:10%\" >数量</td><td style=\"width:10%\" >供货商</td><td style=\"width:10%\" >单价</td><td  style=\"width:10%\" >金额</td></tr>  ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["INID"].ToString() + "</td><td >" + dt.Rows[i]["OrderContent"].ToString() + "</td><td >" + dt.Rows[i]["Specifications"].ToString() + "</td><td  >" + dt.Rows[i]["Unit"].ToString() + "</td><td  >" + dt.Rows[i]["Rate"].ToString() + "</td><td  >" + dt.Rows[i]["COMNameC"].ToString() + "</td><td  >" + dt.Rows[i]["UnitPriceNoTax"].ToString() + "</td><td  >" + Convert.ToInt32(dt.Rows[i]["Rate"]) * Convert.ToDouble(dt.Rows[i]["UnitPriceNoTax"]) + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }

        public ActionResult UpdateFKXX()
        {
            return View();
        }

        public ActionResult UpdateFK()
        {

            PP_Payment pp = new PP_Payment();
            List<PP_PaymentGoods> list = new List<PP_PaymentGoods>();
            string strErr = "";
            pp.PayId = Request["PayId"];
            pp.PaymentMenthod = Request["PaymentMenthod"];
            pp.State = Request["State"];
            pp.PayTime = Convert.ToDateTime(Request["PayTime"]);
            pp.OrderContacts = Request["OrderContacts"];

            string[] ShuLiang = Request["shuliang"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] PayXid = Request["payxid"].Split(',');
            string[] Rate = Request["rate"].Split(',');
            for (int i = 0; i < ShuLiang.Length; i++)
            {
                PP_PaymentGoods good = new PP_PaymentGoods();
                good.DID = DID[i];
                good.Rate = Rate[i];
                good.PayXid = PayXid[i];
                good.GoodsUse = ShuLiang[i];
                list.Add(good);

            }
            bool b = PPManage.UpdateFK(pp, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(pp.PayId, "修改付款单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pp.PayId, "修改付款单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult UpdateFKXXer()
        {
            return View();
        }
        public ActionResult PaymentCP()
        {
            PP_Payment pay = new PP_Payment();
            pay.PayId = PPManage.GetTopListFKID();
            return View(pay);
        }

        public ActionResult DelectFK()
        {
            string PayId = Request["PayId"];
            //string where = "PayId='" + PayId + "'";
            bool dt = PPManage.DeleteFK(PayId);
            if (dt != true)
            {
                PPManage.AddRZ(PayId, "撤销付款单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = false });
            }

            else
            {
                PPManage.AddRZ(PayId, "撤销付款单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = true });
            }

        }

        public ActionResult SelectFKCP()
        {

            string str = " 1=1 and ";
            string PayID = Request["PayID"];
            if (PayID != "")
            {
                str += " PAYID='" + PayID + "'";
            }
            if (PayID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectFKCP(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SelectFKCPXX()
        {
            string where = "";
            string payid = Request["payid"];
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (payid != "")
            {
                where = "a.PAYID='" + payid + "'  ";
            }
            UIDataTable udtTask = PPManage.SelectFKCPXX(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFKCP()
        {
            PP_Payment pay = new PP_Payment();
            pay.PayId = Request["payid"];
            pay.PaymentMenthod = Request["paymentmenthod"];
            pay.PayCompany = GAccount.GetAccountInfo().UnitID;
            pay.PayTime = Convert.ToDateTime(Request["paytime"]);
            pay.State = Request["state"];
            pay.OrderContacts = Request["ordercontacts"];
            string strErr = "";
            string[] RowNumber = Request["ljrownumber"].Split(',');
            string[] INID = Request["ljid"].Split(',');
            string[] DID = Request["dddid"].Split(',');
            string[] LJCPID = Request["ljcpid"].Split(',');
            string[] OrderContent = Request["ljname"].Split(',');
            string[] Specifications = Request["ljspes"].Split(',');
            string[] Amount = Request["ljnums"].Split(',');
            string[] Supplier = Request["ljmanufacturer"].Split(',');
            string[] SJFK = Request["ljsjfknum"].Split(',');
            string[] BCFK = Request["ljbcnums"].Split(',');
            string[] Unit = Request["ljunits"].Split(',');
            string[] UnitPriceNoTax = Request["ljunitprice"].Split(',');
            string[] TotalNoTax = Request["ljzj"].Split(',');
            string[] UnitPrice = Request["ljprice2"].Split(',');
            string[] Total = Request["ljzj2"].Split(',');

            List<PP_PaymentGoods> list = new List<PP_PaymentGoods>();
            List<string> str = new List<string>();
            for (int i = 0; i < OrderContent.Length; i++)
            {
                PP_PaymentGoods good = new PP_PaymentGoods();
                good.PayId = Request["PayId"];
                good.PayXid = Request["PayId"] + "-" + RowNumber[i];
                good.INID = INID[i];
                good.DID = DID[i];
                good.LJCPID = LJCPID[i];
                good.OrderContent = OrderContent[i];
                good.Specifications = Specifications[i];
                good.Unit = Unit[i];
                good.Amount = Amount[i];
                good.Supplier = Supplier[i];
                good.UnitPriceNoTax = Convert.ToDecimal(UnitPriceNoTax[i]);
                good.TotalNoTax = Convert.ToDecimal(TotalNoTax[i]);
                good.UnitPrice = Convert.ToDouble(UnitPrice[i]);
                good.Total = Convert.ToDouble(Total[i]);
                good.Rate = BCFK[i];
                good.Remark = SJFK[i];
                list.Add(good);
            }

            string[] rownumberss = Request["rownumberss"].Split(',');
            string[] cppid = Request["cppid"].Split(',');
            string[] cpid = Request["cpid"].Split(',');
            string[] cpppids = Request["cpppids"].Split(',');
            string[] namesss = Request["namesss"].Split(',');
            string[] spesss = Request["spesss"].Split(',');
            string[] numsss = Request["numsss"].Split(',');
            string[] unitsss = Request["unitsss"].Split(',');
            string[] unitpricess = Request["unitpricess"].Split(',');
            string[] price2ss = Request["price2ss"].Split(',');
            string[] fknums = Request["fknums"].Split(',');
            List<PP_ChoseGoods> ChoseGood = new List<PP_ChoseGoods>();
            for (int i = 0; i < rownumberss.Length; i++)
            {
                PP_ChoseGoods ChoseGoods = new PP_ChoseGoods();
                ChoseGoods.DDID = Request["payid"];
                ChoseGoods.ID = Request["payid"] + '-' + rownumberss[i];
                ChoseGoods.PID = cppid[i];
                ChoseGoods.SHnum = cpppids[i];
                ChoseGoods.Name = namesss[i];
                ChoseGoods.Spc = spesss[i];
                ChoseGoods.Num = numsss[i];
                ChoseGoods.Units = unitsss[i];
                ChoseGoods.UnitPrice = unitpricess[i];
                ChoseGoods.Price2 = price2ss[i];
                ChoseGoods.UnitPrices = (Convert.ToDouble(unitpricess[i]) * Convert.ToInt32(numsss[i])).ToString();
                ChoseGoods.Price2s = (Convert.ToDouble(price2ss[i]) * Convert.ToInt32(numsss[i])).ToString();
                ChoseGoods.FKnum = fknums[i];

                ChoseGood.Add(ChoseGoods);
            }
            bool b = PPManage.UpdateFKCP(pay, list, ref strErr, ChoseGood);
            if (b)
            {
                PPManage.AddRZ(pay.PayId, "付款修改", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(pay.PayId, "付款修改", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "付款");
                return Json(new { success = false, Msg = strErr });
            }
        }

        #endregion
        public ActionResult InventoryFirstPage()
        {
            return View();
        }

        public ActionResult StockRemainList()
        {
            string where = "and";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string ProType = Request["ProType"].ToString();
            string PID = Request["PID"].ToString();
            string ProName = Request["ProName"].ToString();
            string Spec = Request["Spec"].ToString();
            string HouseID = Request["HouseID"].ToString();
            where += " d.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and";
            if (ProType != "")
                where += " c.Text='" + ProType + "' and";
            if (PID != "")
                where += " PID like '%" + PID + "%' and";
            if (ProName != "")
                where += " b.ProName like '%" + ProName + "%' and";
            if (Spec != "")
                where += " b.Spec='" + Spec + "' and";
            if (HouseID != "")
                where += " d.HouseName='" + HouseID + "' and";
            where = where.Substring(0, where.Length - 3);

            UIDataTable udtTask = InventoryMan.StockRemainList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        #region[统计]
        public ActionResult DGMX()
        {
            return View();
        }
        public ActionResult CGKXHZ()
        {
            return View();
        }

        public ActionResult STH()
        {
            return View();
        }

        public ActionResult PrintSupplierTJ(string id)
        {




            string str = " 1=1 and ";

            if (id != "")
            {
                str += "j.COMNameC='" + id + "'";
            }
            if (id == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectGoodsDDID1(str);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">订购单</div>");
            sb.Append("<table id=\"tab\" style=\"margin-top:10px;\" class = \"tabInfoP\">");


            sb.Append("<tr><td style=\"width:10%\" >供货商</td><td style=\"width:10%\"  >计划数额</td><td  style=\"width:10%\" >已发生数额</td><td  style=\"width:10%\" >已付款(元)</td><td style=\"width:10%\" >未付款(元)</td></tr>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td >" + dt.Rows[i]["COMNameC"].ToString() + "</td><td >" + dt.Rows[i]["TotalNoTax"].ToString() + "</td><td >" + dt.Rows[i]["RKjine"].ToString() + "</td><td  >" + dt.Rows[i]["SJFK"].ToString() + "</td><td  >" + dt.Rows[i]["NoFK"].ToString() + "</td></tr>");
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }
        #endregion

        #region [审批]
        public ActionResult Approval()
        {

            PP_Approval app = new PP_Approval();
            app.PID = PPManage.GetApprovalSPID();
            return View(app);

        }
        public ActionResult SelectApproval()
        {
            string CID = Request["CID"].ToString();
            DataTable dt = PPManage.SelectApproval(CID);
            string Approvaler = dt.Rows[0]["Approvaler"].ToString();
            string ApprovalType = dt.Rows[0]["ApprovalType"].ToString();
            DataTable dts = PPManage.SelectApprovalUser(Approvaler, ApprovalType);
            if (dts == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dts) });
        }
        public ActionResult InsertApproval()
        {
            PP_Approval app = new PP_Approval();

            app.PID = Request["PID"].ToString();
            app.ApprovalContent = Request["ApprovalContent"].ToString();
            app.PIDS = Request["PIDS"].ToString();
            app.ApprovalType = Request["ApprovalType"].ToString();
            //app.Approvaler = Request["Approvaler"].ToString();
            app.Approvaler = GAccount.GetAccountInfo().UserID.ToString();
            app.Job = Request["Job"].ToString();
            app.ApprovalTime = Convert.ToDateTime(Request["ApprovalTime"]);
            app.IsPass = Request["IsPass"].ToString();
            app.NoPassReason = Request["NoPassReason"].ToString();
            app.ApprovalExplain = Request["ApprovalExplain"].ToString();
            app.ApprovalLevel = Request["ApprovalLevel"].ToString();
            bool ok = PPManage.InsertApproval(app);
            if (ok)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult SPxj()
        {
            ViewData["webkey"] = "询价审批";
            string fold = COM_ApprovalMan.getNewwebkey("询价审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }
        public ActionResult SPdd()
        {
            ViewData["webkey"] = "订购审批";
            string fold = COM_ApprovalMan.getNewwebkey("订购审批");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            return View();
        }

        public ActionResult SelectSP()
        {
            string where = "  d.Type='审批状态' and b.ApprovalContent='询价审批' and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string XJID = Request["XJID"];
            string PID = Request["PID"];

            string Begin = Request["Begin"];
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Begin != "" && End != "")
                where += " a.InquiryDate between '" + Begin + "' and '" + End + "' and ";

            if (XJID != "")
                where += " a.XJID like'%" + XJID + "%' and ";
            if (PID != "")
                where += " b.PID like'%" + PID + "%' and";

            if (PID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);


            UIDataTable udtTask = PPManage.SelectSP(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectDDSP()
        {
            string where = "  d.Type='审批状态' and b.ApprovalContent='订购审批' and ";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string DDID = Request["DDID"];
            string PID = Request["PID"];

            string Begin = Request["Begin"];
            if (Begin != "")
                Begin += " 00:00:00";
            string End = Request["End"].ToString();
            if (End != "")
                End += " 23:59:59";
            if (Begin != "" && End != "")
                where += " a.OrderDate between '" + Begin + "' and '" + End + "' and ";

            if (DDID != "")
                where += " a.DDID like'%" + DDID + "%' and ";
            if (PID != "")
                where += " b.PID like'%" + PID + "%' and";

            if (PID == "")
                where = where.Substring(0, where.Length - 4);
            else
                where = where.Substring(0, where.Length - 3);
            UIDataTable udtTask = PPManage.SelectDDSP(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[系统设置]
        public ActionResult XTSZ()
        {
            return View();
        }

        public ActionResult SelectConfig()
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
                where += "  a.Type ='" + type + "' and Validate='v' ";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = PPManage.SelectConfig(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddConfig(string id)
        {
            ViewData["type"] = id;
            return View();
        }

        public ActionResult InsertContent()
        {
            var type = Request["Type"];
            var text = Request["Text"];
            string strErr = "";
            if (PPManage.InsertNewContent(type, text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult UpdateConfig(string id)
        {
            string[] arr = id.Split('@');
            ViewData["IDs"] = arr[0];
            ViewData["Type"] = arr[1];
            ViewData["Text"] = arr[2];
            return View();
        }

        public ActionResult upNewContent()
        {
            var id = Request["IDs"];
            var Type = Request["Type"];
            var Text = Request["Text"];
            string strErr = "";
            if (PPManage.UpdateNewContent(id, Type, Text, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        public ActionResult DeleteContent()
        {
            var xid = Request["data1"];
            var type = Request["data2"];
            string strErr = "";
            if (PPManage.DeleteNewContent(xid, type, ref strErr) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
        }

        #endregion

        #region[统计]
        public ActionResult TJghmx()
        {
            return View();
        }

        #endregion

        #region [采购提醒]
        public ActionResult CPlanTimeWarnGrid()
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string unit = account.UnitID.ToString();
                string where = "  1=1  and a.Validate='1' and ";
                string time = PPManage.getCPlanTime();
                if (time == "")
                    where += "   a.OrderUnit = '" + unit + "' and  DATEDIFF(day,GETDATE(),a.DeliveryLimit) > '7' and "; // and a.state != '2'
                else
                    where += "   a.OrderUnit = '" + unit + "' and DATEDIFF(day,GETDATE(),a.DeliveryLimit) >= '" + time + "' and ";
                string strCurPage;
                string strRowNum;



                //string DeliveryDate = Request["DeliveryDate"];

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";




                //if (DeliveryDate != "" && DeliveryDate != null)
                //    where += " and a.DeliveryDate >= '" + DeliveryDate + "' and a.DeliveryDate <= '" + DeliveryDate + "'";
                //string Begin = Request["Begin"].ToString();
                //if (Begin != "")
                //    Begin += " 00:00:00";
                //string End = Request["End"].ToString();
                //if (End != "")
                //    End += " 23:59:59";
                //if (Begin != "" && End != "")
                //    where += " a.OrderDate between '" + Begin + "' and '" + End + "' and ";

                string DDID = Request["DDID"];
                if (DDID != "")
                    where += " a.DDID like'%" + DDID + "%' and";


                if (DDID == "")
                    where = where.Substring(0, where.Length - 4);
                else
                    where = where.Substring(0, where.Length - 3);

                UIDataTable udtTask = new UIDataTable();
                if (where != "")
                    udtTask = PPManage.SelectDDTJ(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
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
        #endregion

        #region[修改添加商品/供货商]
        public ActionResult UpdateGoods()
        {
            return View();
        }
        public ActionResult UpdateSupplier()
        {
            return View();
        }
        #endregion

        #region[二期采购]
        #region[采购单]
        public ActionResult ErOrder()
        {
            PP_PurchaseOrder order = new TECOCITY_BGOI.PP_PurchaseOrder();

            order.DDID = PPManage.GetTopListDDID();
            return View(order);
        }
        public ActionResult ErUpdateDD()
        {
            return View();
        }
        public ActionResult ErDetailsDD()
        {
            return View();
        }
        public ActionResult ErInsertOrder()
        {
            PP_ErPurchaseOrder order = new PP_ErPurchaseOrder();
            order.DDID = Request["ddid"];
            order.CID = Request["cid"];
            order.GoodsType = Request["Goodstype"];
            order.OrderDate = Convert.ToDateTime(Request["orderdate"]);
            order.DeliveryLimit = Request["begin"];
            order.TheProject = Request["theproject"];
            order.StockSituation = Request["stocksituation"];
            order.ProjectPeople = Request["projectpeople"];
            order.Contract = Request["contract"];
            order.Tsix = Request["tsix"];
            order.ContractNoReason = Request["contractnoreason"];
            order.SaleUnitPrice = Request["saleunitprice"];
            order.ContractTotal = Request["contracttotal"];
            order.FKexplain = Request["fkexplain"];
            order.ProjectHK = Request["Projecthk"];
            order.DDState = "L";
            order.PayStatus = "1";
            order.OrderUnit = GAccount.GetAccountInfo().UnitID;
            order.CreateTime = DateTime.Now;
            order.CreateUser = GAccount.GetAccountInfo().UserName;
            order.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] pids = Request["pidss"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] units = Request["units"].Split(',');
            string[] amount = Request["amount"].Split(',');
            string[] supplier = Request["supplier"].Split(',');
            string[] total = Request["total"].Split(',');
            string[] totalnotax = Request["totalnotax"].Split(',');
            string[] price2 = Request["price2"].Split(',');
            string[] totaltax = Request["totaltax"].Split(',');
            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + rownumber[i];
                pp.DDID = Request["ddid"];
                pp.MaterialNO = pids[i];
                pp.OrderContent = proname[i];
                pp.Specifications = spec[i];
                pp.Amount = amount[i];
                pp.ActualAmount = 0;
                pp.Supplier = supplier[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = units[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(total[i]);
                pp.TotalNoTax = Convert.ToDecimal(totalnotax[i]);
                pp.UnitPrice = Convert.ToDecimal(price2[i]);
                pp.Total = Convert.ToDecimal(totaltax[i]);
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.ErInsertOrder(order, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(order.DDID, "增加订购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(order.DDID, "增加订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult ErDDUpdate()
        {
            PP_ErPurchaseOrder order = new PP_ErPurchaseOrder();
            order.DDID = Request["ddid"];
            order.CID = Request["cid"];
            order.GoodsType = Request["Goodstype"];
            order.OrderDate = Convert.ToDateTime(Request["orderdate"]);
            order.DeliveryLimit = Request["begin"];
            order.TheProject = Request["theproject"];
            order.StockSituation = Request["stocksituation"];
            order.ProjectPeople = Request["projectpeople"];
            order.Contract = Request["contract"];
            order.Tsix = Request["tsix"];
            order.ContractNoReason = Request["contractnoreason"];
            order.SaleUnitPrice = Request["saleunitprice"];
            order.ContractTotal = Request["contracttotal"];
            order.FKexplain = Request["fkexplain"];
            order.ProjectHK = Request["Projecthk"];
            order.DDState = "0";
            order.PayStatus = "1";
            order.OrderUnit = GAccount.GetAccountInfo().UnitID;
            order.CreateTime = DateTime.Now;
            order.CreateUser = GAccount.GetAccountInfo().UserName;
            order.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] units = Request["units"].Split(',');
            string[] amount = Request["amount"].Split(',');
            string[] supplier = Request["supplier"].Split(',');
            string[] total = Request["total"].Split(',');
            string[] totalnotax = Request["totalnotax"].Split(',');
            string[] price2 = Request["price2"].Split(',');
            string[] totaltax = Request["totaltax"].Split(',');
            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + rownumber[i];
                pp.DDID = Request["ddid"];
                pp.OrderContent = proname[i];
                pp.Specifications = spec[i];
                pp.Amount = amount[i];
                pp.ActualAmount = 0;
                pp.Supplier = supplier[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = units[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(total[i]);
                pp.TotalNoTax = Convert.ToDecimal(totalnotax[i]);
                pp.UnitPrice = Convert.ToDecimal(price2[i]);
                pp.Total = Convert.ToDecimal(totaltax[i]);
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.ErDDUpdate(order, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(order.DDID, "修改订购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(order.DDID, "修改订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }
        }
        public ActionResult ErSelectGoodsDDID()
        {
            string str = " 1=1 and ";
            string DDID = Request["DDID"];
            if (DDID != "")
            {
                str += "a.DDID='" + DDID + "'";
            }
            if (DDID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.ErSelectGoodsDDID(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        #endregion
        #region[物流]
        public ActionResult ErAddLogistics()
        {
            PP_Logistics good = new TECOCITY_BGOI.PP_Logistics();

            good.ID = PPManage.GetWLID();
            return View(good);
        }
        public ActionResult InsertWL()
        {
            PP_Logistics good = new PP_Logistics();
            good.ID = Request["ID"];
            good.SQCompany = Request["sqcompany"];
            good.THCompany = Request["thcompany"];
            good.SHaddress = Request["shaddress"];
            good.SHContacts = Request["shcontacts"];
            good.SHTel = Request["shtel"];
            good.FHConsignor = Request["fhconsignor"];
            good.FHTel = Request["fhtel"];
            good.FHFax = Request["fhfax"];
            good.LogisticsS = Request["logisticss"];
            good.LogisticsSTel = Request["logisticsstel"];
            good.LogisticsSFax = Request["logisticssfax"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] amount = Request["amount"].Split(',');
            List<PP_LogisticsGoods> list = new List<PP_LogisticsGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_LogisticsGoods pp = new PP_LogisticsGoods();

                pp.ID = Request["ID"];
                pp.ProName = proname[i];
                pp.Spec = spec[i];
                pp.Amount = amount[i];
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.Validate = "1";
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.InsertWL(good, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.ID, "增加物流", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "物流");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.ID, "增加物流", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "物流");
                return Json(new { success = false, Msg = strErr });
            }

        }
        public ActionResult ErLogistics()
        {
            return View();
        }
        public ActionResult SelectWL()
        {
            string where = "1=1 and a.Validate='1'";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = PPManage.SelectWL(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SelectWLGoods()
        {
            string where = "1=1";
            string strCurPage;
            string strRowNum;
            string ID = Request["ID"];

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (ID != "")
            {
                where = "a.ID='" + ID + "'";
            }
            UIDataTable udtTask = PPManage.SelectWLGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PrintWL(string id)
        {

            string str = "a.ID='" + id + "'";
            DataTable dt = PPManage.SelectWLGoodsXX(str);
            string goods = "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                goods += dt.Rows[i]["ProName"].ToString();
                goods += dt.Rows[i]["Spec"].ToString() + ":";
                goods += dt.Rows[i]["Amount"].ToString() + "台;";
            }
            sb.Append("<div align=\"center\" style=\"margin-top:10px;font-size:20px;\">物流运输授权书</div>");

            sb.Append("<p><div  style=\"margin-top:10px;font-size:20px;\">我公司(北京燕山工业燃气设备有限公司)现授权" + dt.Rows[0]["SQCompany"].ToString() + "到" + dt.Rows[0]["THCompany"].ToString() + "提货，特此证明</div></p>");

            sb.Append("<p><div  style=\"margin-top:10px;font-size:20px;\">此批货物为" + goods + "发往" + dt.Rows[0]["SHaddress"].ToString() + ",联系人：" + dt.Rows[0]["SHContacts"].ToString() + ",联系电话：" + dt.Rows[0]["SHTel"].ToString() + ".</div> </p>");

            sb.Append("<p><div  style=\"margin-top:10px;font-size:20px;\">提货公司联系人" + dt.Rows[0]["FHConsignor"].ToString() + ",联系人电话：" + dt.Rows[0]["FHTel"].ToString() + ",联系传真：" + dt.Rows[0]["FHFax"].ToString() + ".</div> </p>");

            sb.Append("<p><div  style=\"margin-top:10px;font-size:20px;\">物流联系人" + dt.Rows[0]["LogisticsS"].ToString() + ",联系人电话：" + dt.Rows[0]["LogisticsSTel"].ToString() + ",联系传真：" + dt.Rows[0]["LogisticsSFax"].ToString() + ".</div> </p>");

            sb.Append("<div  align=\"right\" style=\"margin-top:60px;font-size:20px;\">北京燕山工业燃气设备有限公司</div>");
            System.DateTime currentTime = new System.DateTime();
            sb.Append("<div  align=\"right\" style=\"margin-top:10px;font-size:20px;\">" + DateTime.Now.ToShortDateString().ToString() + "</div>");
            Response.Write(sb.ToString());
            return View();


        }
        public ActionResult ErUpdateWL()
        {
            return View();
        }

        public ActionResult SelectWLGoodsXX()
        {
            string str = " 1=1 and ";
            string ID = Request["ID"];
            if (ID != "")
            {
                str += " a.ID='" + ID + "'";
            }
            if (ID == "")
                str = str.Substring(0, str.Length - 4);
            DataTable dt = PPManage.SelectWLGoodsXX(str);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdateWL()
        {
            PP_Logistics good = new PP_Logistics();
            good.ID = Request["ID"];
            good.SQCompany = Request["sqcompany"];
            good.THCompany = Request["thcompany"];
            good.SHaddress = Request["shaddress"];
            good.SHContacts = Request["shcontacts"];
            good.SHTel = Request["shtel"];
            good.FHConsignor = Request["fhconsignor"];
            good.FHTel = Request["fhtel"];
            good.FHFax = Request["fhfax"];
            good.LogisticsS = Request["logisticss"];
            good.LogisticsSTel = Request["logisticsstel"];
            good.LogisticsSFax = Request["logisticssfax"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] amount = Request["amount"].Split(',');
            List<PP_LogisticsGoods> list = new List<PP_LogisticsGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_LogisticsGoods pp = new PP_LogisticsGoods();

                pp.ID = Request["ID"];
                pp.ProName = proname[i];
                pp.Spec = spec[i];
                pp.Amount = amount[i];
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.Validate = "1";
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.UpdateWL(good, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.ID, "修改物流", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "物流");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.ID, "修改物流", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "物流");
                return Json(new { success = false, Msg = strErr });
            }
        }

        public ActionResult DeleteWL()
        {
            string str = " 1=1 and ";
            string ID = Request["ID"];
            if (ID != "")
            {
                str += "  ID='" + ID + "'";
            }
            if (ID == "")
                str = str.Substring(0, str.Length - 4);
            bool dt = PPManage.DeleteWL(str);

            if (dt == false)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = "ture" });
        }
        #endregion

        #endregion

        #region[采购Ⅲ]
        #region[订购]
        public ActionResult SanOrder()
        {
            PP_PurchaseOrder order = new TECOCITY_BGOI.PP_PurchaseOrder();

            order.DDID = PPManage.GetTopListDDID();
            return View(order);
        }

        public ActionResult InsertOrderSan()
        {

            PP_PurchaseOrder good = new PP_PurchaseOrder();
            good.OrderDate = Convert.ToDateTime(Request["orderdate"]); ;

            good.DDID = Request["ddid"].ToString();
            good.DeliveryLimit = Request["begin"];
            good.DeliveryMethod = Request["deliverymethod"];
            good.IsInvoice = Request["isinvoice"];
            good.PaymentMethod = Request["paymentmethod"];
            good.PaymentAgreement = Request["paymentagreement"];
            good.ContractNO = Request["contractno"];
            good.TheProject = Request["theproject"];
            good.OrderContacts = Request["ordercontacts"];
            good.OrderUnit = GAccount.GetAccountInfo().UnitID;
            good.DDState = "L";
            good.PayStatus = "1";
            good.PID = Request["tasknum"];
            good.BusinessTypes = Request["businesstypes"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] units = Request["units"].Split(',');
            string[] amount = Request["amount"].Split(',');
            string[] supplier = Request["supplier"].Split(',');

            string[] unitpricenotax = Request["unitpricenotax"].Split(',');
            string[] totalnotax = Request["totalnotax"].Split(',');
            string[] price = Request["price"].Split(',');
            string[] totaltax = Request["totaltax"].Split(',');
            string[] pids = Request["pids"].Split(',');

            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + rownumber[i];
                pp.DDID = Request["ddid"];
                pp.MaterialNO = pids[i];
                pp.OrderContent = proname[i];
                pp.Specifications = spec[i];
                pp.Amount = amount[i];
                pp.ActualAmount = 0;
                pp.Supplier = supplier[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = units[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(unitpricenotax[i]);
                pp.TotalNoTax = Convert.ToDecimal(totalnotax[i]);
                pp.UnitPrice = Convert.ToDecimal(price[i]);
                pp.Total = Convert.ToDecimal(totaltax[i]);
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.InsertOrderSan(good, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.DDID, "增加订购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.DDID, "增加订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }

        }

        public ActionResult DetailsDDSan()
        {
            return View();
        }

        public ActionResult SanUpdateDD()
        {

            return View();
        }

        public ActionResult SanUpdateDDS()
        {

            PP_PurchaseOrder good = new PP_PurchaseOrder();
            good.OrderDate = Convert.ToDateTime(Request["orderdate"]); ;

            good.DDID = Request["ddid"].ToString();
            good.DeliveryLimit = Request["begin"];
            good.DeliveryMethod = Request["deliverymethod"];
            good.IsInvoice = Request["isinvoice"];
            good.PaymentMethod = Request["paymentmethod"];
            good.PaymentAgreement = Request["paymentagreement"];
            good.ContractNO = Request["contractno"];
            good.TheProject = Request["theproject"];
            good.OrderContacts = Request["ordercontacts"];
            good.OrderUnit = GAccount.GetAccountInfo().UnitID;
            good.DDState = "L";
            good.PayStatus = "1";
            good.PID = Request["tasknum"];
            good.BusinessTypes = Request["businesstypes"];
            good.CreateTime = DateTime.Now;
            good.CreateUser = GAccount.GetAccountInfo().UserName;
            good.Validate = "1";

            string[] rownumber = Request["rownumber"].Split(',');
            string[] proname = Request["proname"].Split(',');
            string[] spec = Request["spec"].Split(',');
            string[] units = Request["units"].Split(',');
            string[] amount = Request["amount"].Split(',');
            string[] supplier = Request["supplier"].Split(',');

            string[] unitpricenotax = Request["unitpricenotax"].Split(',');
            string[] totalnotax = Request["totalnotax"].Split(',');
            string[] price = Request["price"].Split(',');
            string[] totaltax = Request["totaltax"].Split(',');


            List<PP_OrderGoods> list = new List<PP_OrderGoods>();
            for (int i = 0; i < rownumber.Length; i++)
            {
                PP_OrderGoods pp = new PP_OrderGoods();
                pp.DID = Request["ddid"] + "-" + rownumber[i];
                pp.DDID = Request["ddid"];

                pp.OrderContent = proname[i];
                pp.Specifications = spec[i];
                pp.Amount = amount[i];
                pp.ActualAmount = 0;
                pp.Supplier = supplier[i];
                pp.SJFK = "0";
                pp.CreateTime = DateTime.Now;
                pp.CreateUser = GAccount.GetAccountInfo().UserName;
                pp.RKState = "0";
                pp.Validate = "1";
                pp.Unit = units[i];
                pp.UnitPriceNoTax = Convert.ToDecimal(unitpricenotax[i]);
                pp.TotalNoTax = Convert.ToDecimal(totalnotax[i]);
                pp.UnitPrice = Convert.ToDecimal(price[i]);
                pp.Total = Convert.ToDecimal(totaltax[i]);
                list.Add(pp);
            }
            string strErr = "";
            bool b = PPManage.SanUpdateDDS(good, list, ref strErr);
            if (b)
            {
                PPManage.AddRZ(good.DDID, "增加订购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = true });
            }
            else
            {
                PPManage.AddRZ(good.DDID, "增加订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                return Json(new { success = false, Msg = strErr });
            }

        }
        #endregion

        #region[付款]
        public ActionResult Payments()
        {
            PP_Payment pay = new PP_Payment();
            pay.PayId = PPManage.GetTopListFKID();
            return View(pay);
        }
        #endregion

        #endregion


        #region[检验单]
        public ActionResult Transfer()
        {
            PP_TransferS pp = new PP_TransferS();
            pp.TransferNum = PPManage.GetTopListJJID();
            return View(pp);
        }

        public ActionResult InsertJJD()
        {
            PP_TransferS tran = new PP_TransferS();

            tran.TransferNum = Request["transfernum"];
            tran.SJPeople = Request["sjpeople"];
            tran.Inspectiondate = Convert.ToDateTime(Request["inspectiondate"]);
            tran.Gooddate = Convert.ToDateTime(Request["goodsdate"]);


            string[] SHID = Request["shid"].Split(',');
            string[] DDID = Request["ddid"].Split(',');
            string[] DID = Request["did"].Split(',');
            string[] MaterialNO = Request["materialno"].Split(',');
            string[] Supplier = Request["supplier"].Split(',');
            string[] ProName = Request["proname"].Split(',');
            string[] Spec = Request["spec"].Split(',');
            string[] Amount = Request["amount"].Split(',');
            string[] Units = Request["units"].Split(',');
            List<PP_TransferGoods> goods = new List<PP_TransferGoods>();
            int a = 0;
            for (int i = 0; i < SHID.Length; i++)
            {
                tran.SHID = SHID[i];
                a++;
                PP_TransferGoods good = new PP_TransferGoods();
                good.ID = tran.TransferNum + '-' + a;
                good.PID = tran.TransferNum;
                good.GoodsNum = MaterialNO[i];
                good.Supplier = Supplier[i];
                good.GoodsName = ProName[i];
                good.GoodsSpe = Spec[i];
                good.Amount = Amount[i];
                good.Unit = Units[i];
                good.Remark = DDID[i];
                good.Bak = DID[i];
                goods.Add(good);
            }
            bool b = PPManage.InsertJJD(tran, goods);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult IndexTransfer()
        {
            return View();
        }
        public ActionResult SelectJJD()
        {

            string where = " 1=1";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = PPManage.SelectJJD(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectJJDGoods()
        {
            string PID = Request["TransferNum"];
            if (PID == null || PID == "")
            {
                return View();
            }

            string where = " a.pid='" + PID + "'";
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            UIDataTable udtTask = PPManage.SelectJJDGoods(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertGoods()
        {
            return View();
        }

        public ActionResult SelectJJDXX()
        {
            string TransferNum = Request["TransferNum"].ToString();
            string where = "1=1";
            DataTable dt = new DataTable();
            if (TransferNum != "")
            {
                where = "a.TransferNum='" + TransferNum + "'";
                dt = PPManage.SelectJJDXX(where);
            }

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult SelectshJJD()
        {
            string shid = Request["shid"];
            string where = "1=1";
            DataTable dt = new DataTable();
            if (shid != "")
            {
                where = "a.SHID='" + shid + "'";
                dt = PPManage.SelectJJDXX(where);
            }

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult InsertJJDXX()
        {
            PP_TransferS tran = new PP_TransferS();

            tran.TransferNum = Request["transfernum"];
            tran.Bak = Request["jyr"];
            tran.Summary = Request["summary"];
            tran.testPeople = Request["testpeople"];



            string[] ID = Request["id"].Split(',');
            string[] YesAmount = Request["yesamount"].Split(',');
            string[] NoAmount = Request["noamount"].Split(',');
            string[] Remark = Request["remark"].Split(',');
            string[] Baks = Request["bak"].Split(',');
            List<PP_TransferGoods> goods = new List<PP_TransferGoods>();
            int a = 0;
            for (int i = 0; i < ID.Length; i++)
            {

                a++;
                PP_TransferGoods good = new PP_TransferGoods();
                good.ID = ID[i];
                good.YesAmount = YesAmount[i];
                good.NoAmount = NoAmount[i];
                good.Remark = Remark[i];
                good.Bak = Baks[i];
                goods.Add(good);
            }
            bool b = PPManage.InsertJJDXX(tran, goods);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult Production()
        {
            return View();
        }


        public ActionResult UpdatePeopleSC()
        {
            PP_TransferS tran = new PP_TransferS();

            tran.TransferNum = Request["transfernum"];
            tran.productionPeople = Request["people"];
            bool b = PPManage.UpdatePeopleSC(tran);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult UpdatePeopleJH()
        {
            PP_TransferS tran = new PP_TransferS();

            tran.TransferNum = Request["transfernum"];
            tran.planPeople = Request["people"];
            bool b = PPManage.UpdatePeopleJH(tran);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult Plan()
        {
            return View();
        }

        public ActionResult Warehouse()
        {
            return View();
        }
        public ActionResult UpdateWarehouse()
        {
            PP_TransferS tran = new PP_TransferS();

            tran.TransferNum = Request["transfernum"];
            tran.Warehouse = Request["warehouse"];
            bool b = PPManage.UpdateWarehouse(tran);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion


        #region[上传]
        // 上传附件 
        public ActionResult InsertBiddingNew()
        {
            return View();

        }
        public ActionResult InsertBiddingNewS()
        {
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();

            //获取上传的文件
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }



            // 将信息存入 上传文件数据表中
            tk_FileUpload fileUp = new tk_FileUpload();
            fileUp.StrRID = Request["RID"].ToString();
 
            fileUp.StrCreatePerson = acc.UserName.ToString();
            fileUp.StrCreateTime = DateTime.Now;
            fileUp.StrValidate = "v";

            PPManage.InsertBiddingNewS(fileUp, Filedata, ref strErr);
            return this.Json(new { });
        }

        //public void DownLoad(string id)
        //{
        //    string[] arrStr = id.Split('/');
        //    string informNo = arrStr[0];
        //    DataTable dtInfo = PPManage.getFile(informNo);
        //    if (dtInfo.Rows[0][0].ToString() != "")
        //    {
        //        byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
        //        Response.Clear();
        //        Response.Charset = "GB2312";
        //        Response.ContentEncoding = System.Text.Encoding.UTF8;
        //        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FileName"].ToString()));
        //        //Response.BinaryWrite(bContent);
        //        //Response.Flush();
        //        //Response.End();
        //        // 添加头信息，指定文件大小，让浏览器能够显示下载进度

        //        Response.AddHeader("Content-Length", bContent.Length.ToString());
        //        // 指定返回的是一个不能被客户端读取的流，必须被下载
        //        Response.ContentType = "application/msword";
        //        // 把文件流发送到客户端

        //        Response.BinaryWrite((byte[])dtInfo.Rows[0]["FileInfo"]);
        //        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //}


        public void DownLoad(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = PPManage.getFile(informNo);
          
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                string fileName = dtInfo.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["CG"] + "\\"
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

        #endregion
    }
}
