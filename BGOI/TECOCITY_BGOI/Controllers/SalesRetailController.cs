using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    [Authorization]
    public class SalesRetailController : Controller
    {
        //
        // GET: /SalesRetail/

        public ActionResult Index()
        {
            return View();
        }

        #region 提交审批&&抄送
        public ActionResult ApprovalCommon(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string applyDesc = "关于编号" + RelevanceID + "的" + arr[2] + "审批申请";
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["RelevanceID"] = RelevanceID;
            ViewData["applyDesc"] = applyDesc;
            ViewData["PID"] = PID;
            ViewData["id"] = id;

            return View();
        }

        public ActionResult GetPersonInfo(string unitId)
        {
            DataTable dt = SalesRetailMan.GetCopyPerson(unitId);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetCopyInfo()
        //{
        //    DataTable dt = SalesRetailMan.GetCopyPerson();
        //    string strJson = GFun.Dt2Json("", dt);
        //    strJson = strJson.Substring(1);
        //    strJson = strJson.Substring(0, strJson.Length - 1);

        //    return Json(strJson, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SaveApprovalInfo()
        {
            string TaskType = "";
            var webkey = Request["data1"];
            if (webkey == "内购审批")
                TaskType = "Internal";
            else if (webkey == "赠送审批")
                TaskType = "Send";
            else if (webkey == "样机审批")
                TaskType = "Prototype";
            else if (webkey == "专柜审批")
                TaskType = "Shoppe";
            else if (webkey == "促销审批")
                TaskType = "Promotion";
            else if (webkey == "市场销售审批")
                TaskType = "Market";
            else if (webkey == "零售销售审批")
                TaskType = "Retail";

            var folderBack = Request["data2"];
            var RelevanceID = Request["data3"];
            string cbVal = Request["dataCbVal"].ToString();
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            bool b = COM_ApprovalMan.InsertNewApproval(PID, RelevanceID, webkey, folderBack, ref strErr);
            if (cbVal != "")
            {
                string InsertCC = "insert into CopyApproval(UserID,PID,TaskType,UnitID,CreateTime)";
                string[] arrCb = cbVal.Split(',');
                for (int i = 0; i < arrCb.Length - 1; i++)
                {
                    string[] arrUser = arrCb[i].Split('-');
                    InsertCC += "select '" + arrUser[0] + "','" + RelevanceID + "','" + TaskType + "','" + arrUser[1] + "','" + DateTime.Now.ToString() + "' ";
                    if (i < arrCb.Length - 2)
                        InsertCC += " union ";
                }
                bool b1 = SalesRetailMan.SaveCopyPerson(InsertCC);
            }


            if (b == true)//&& b1 == true
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }

        /// <summary>
        /// 审批意见打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintSPInfo(string PID, string TaskType)
        {
            string html = "";
            string html1 = "";
            DataTable dtSpInfo = SalesRetailMan.GetSPInfo(PID);
            if (TaskType == "Prototype")
            {
                tk_Property proto = SalesRetailMan.GetPrototypeInfo(PID);
                html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
                html += "<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>样机申请审批情况</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请日期：</td><td colspan='2' style='text-align:center;'>" + proto.ApplyDate + "</td><td style='width:16%; text-align:center;'>商场名称：</td><td colspan='2' style='text-align:center;border-top:1px solid black;'>" + proto.Malls + "</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>活动说明：</td><td style='width:16%;text-align:center;' colspan='5'>" + proto.ExPlain + "</td></tr>";

                if (dtSpInfo != null && dtSpInfo.Rows.Count > 0)
                {
                    html1 = "<tr><td style='width:16%; text-align:center;height:18px;'>审批人</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>职务</td><td style='width:16%; text-align:center;height:18px;'>审批情况</td><td style='width:16%; text-align:center;height:18px;'>审批意见</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>审批时间</td><td style='width:17%; text-align:center;height:18px;'>备注</td></tr>";

                    for (int i = 0; i < dtSpInfo.Rows.Count; i++)
                    {
                        html1 += "<tr><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalMan"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Job"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["State"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Opinion"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalTime"].ToString() + "</td><td style='width:17%; text-align:center;'>" + dtSpInfo.Rows[i]["Remark"].ToString() + "</td>";
                        html1 += "</tr>";
                    }
                }
                html += html1;
                html += "</table></div>";
            }
            else if (TaskType == "Internal" || TaskType == "Send")
            {
                string type = "";
                if (TaskType == "Internal")
                    type = "0";
                else if (TaskType == "Send")
                    type = "1";

                tk_InternalOrder order = new tk_InternalOrder();
                order = SalesRetailMan.GetInternalOrder(PID, type);

                html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
                html += "<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>内购（赠送）申请审批情况</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请日期：</td><td colspan='2' style='width:16%; text-align:center;'>" + order.OrderDate + "</td><td style='width:16%; text-align:center;'>申请人：</td><td style='width:16%; text-align:center;border-top:1px solid black;' colspan='2'>" + order.Applyer + "</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>内购公司商品使用人：</td><td style='width:16%;text-align:center;' colspan='2'>" + order.GoodsUser + "</td><td style='width:16%; text-align:center;'>联系电话：</td><td style='width:16%;text-align:center;' colspan='2'>" + order.UserTel + "</td></tr>";

                if (dtSpInfo != null && dtSpInfo.Rows.Count > 0)
                {
                    html1 = "<tr><td style='width:16%; text-align:center;height:18px;'>审批人</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>职务</td><td style='width:16%; text-align:center;height:18px;'>审批情况</td><td style='width:16%; text-align:center;height:18px;'>审批意见</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>审批时间</td><td style='width:17%; text-align:center;height:18px;'>备注</td></tr>";

                    for (int i = 0; i < dtSpInfo.Rows.Count; i++)
                    {
                        html1 += "<tr><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalMan"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Job"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["State"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Opinion"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalTime"].ToString() + "</td><td style='width:17%; text-align:center;'>" + dtSpInfo.Rows[i]["Remark"].ToString() + "</td>";
                        html1 += "</tr>";
                    }
                }
                html += html1;
                html += "</table></div>";
            }
            else if (TaskType == "Market")
            {
                tk_MarketSales market = new tk_MarketSales();
                market = SalesRetailMan.GetMarketInfo(PID);
                string applyType = SalesRetailMan.GetTaskFiled(market.ApplyType, "ID", "ProjectSelect_Config", "Text", "Market", "Type");

                html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
                html += "<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>市场销售申请审批情况</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请日期：</td><td colspan='2' style='width:16%; text-align:center;'>" + market.ApplyTime + "</td><td style='width:16%; text-align:center;'>申请类型：</td><td style='width:16%; text-align:center;border-top:1px solid black;' colspan='2'>" + applyType + "</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请名称：</td><td style='width:16%;text-align:center;' colspan='5'>" + market.ApplyTitle + "</td></tr>";

                if (dtSpInfo != null && dtSpInfo.Rows.Count > 0)
                {
                    html1 = "<tr><td style='width:16%; text-align:center;height:18px;'>审批人</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>职务</td><td style='width:16%; text-align:center;height:18px;'>审批情况</td><td style='width:16%; text-align:center;height:18px;'>审批意见</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>审批时间</td><td style='width:17%; text-align:center;height:18px;'>备注</td></tr>";

                    for (int i = 0; i < dtSpInfo.Rows.Count; i++)
                    {
                        html1 += "<tr><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalMan"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Job"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["State"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Opinion"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalTime"].ToString() + "</td><td style='width:17%; text-align:center;'>" + dtSpInfo.Rows[i]["Remark"].ToString() + "</td>";
                        html1 += "</tr>";
                    }
                }
                html += html1;
                html += "</table></div>";
            }
            else if (TaskType == "Shoppe")
            {
                tk_ShoppeInfo shoppe = SalesRetailMan.GetShoppeInfo(PID);

                html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
                html += "<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>专柜申请审批情况</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>所属代理商：</td><td colspan='2' style='width:16%; text-align:center;'>" + shoppe.Malls + "</td><td style='width:16%; text-align:center;'>商场名称：</td><td style='width:16%; text-align:center;border-top:1px solid black;' colspan='2'>" + shoppe.Customer + "</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请日期：</td><td style='width:16%;text-align:center;' colspan='5'>" + shoppe.ApplyTime + "</td></tr>";

                if (dtSpInfo != null && dtSpInfo.Rows.Count > 0)
                {
                    html1 = "<tr><td style='width:16%; text-align:center;height:18px;'>审批人</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>职务</td><td style='width:16%; text-align:center;height:18px;'>审批情况</td><td style='width:16%; text-align:center;height:18px;'>审批意见</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>审批时间</td><td style='width:17%; text-align:center;height:18px;'>备注</td></tr>";

                    for (int i = 0; i < dtSpInfo.Rows.Count; i++)
                    {
                        html1 += "<tr><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalMan"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Job"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["State"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Opinion"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalTime"].ToString() + "</td><td style='width:17%; text-align:center;'>" + dtSpInfo.Rows[i]["Remark"].ToString() + "</td>";
                        html1 += "</tr>";
                    }
                }
                html += html1;
                html += "</table></div>";
            }
            else if (TaskType == "Promotion")
            {
                tk_Promotion promotion = new tk_Promotion();
                promotion = SalesRetailMan.GetPromotionInfo(PID);

                html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
                html += "<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>促销申请审批情况</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>申请日期：</td><td colspan='2' style='width:16%; text-align:center;'>" + promotion.ApplyTime + "</td><td style='width:16%; text-align:center;'>申请人：</td><td style='width:16%; text-align:center;border-top:1px solid black;' colspan='2'>" + promotion.Applyer + "</td></tr>";
                html += "<tr><td style='width:16%; text-align:center;'>活动主题：</td><td style='width:16%;text-align:center;' colspan='5'>" + promotion.ActionTitle + "</td></tr>";

                if (dtSpInfo != null && dtSpInfo.Rows.Count > 0)
                {
                    html1 = "<tr><td style='width:16%; text-align:center;height:18px;'>审批人</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>职务</td><td style='width:16%; text-align:center;height:18px;'>审批情况</td><td style='width:16%; text-align:center;height:18px;'>审批意见</td>"
                          + "<td style='width:16%; text-align:center;height:18px;'>审批时间</td><td style='width:17%; text-align:center;height:18px;'>备注</td></tr>";

                    for (int i = 0; i < dtSpInfo.Rows.Count; i++)
                    {
                        html1 += "<tr><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalMan"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Job"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["State"].ToString() + "</td><td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["Opinion"].ToString() + "</td>"
                            + "<td style='width:16%; text-align:center;'>" + dtSpInfo.Rows[i]["ApprovalTime"].ToString() + "</td><td style='width:17%; text-align:center;'>" + dtSpInfo.Rows[i]["Remark"].ToString() + "</td>";
                        html1 += "</tr>";
                    }
                }
                html += html1;
                html += "</table></div>";
            }

            Response.Write(html);
            return View();
        }
        #endregion

        #region 系统设置
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
                where += " and Type ='" + type + "' ";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SalesRetailMan.GetBasicGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBasicGrid(string id)
        {
            ViewData["type"] = id;
            return View();
        }

        public ActionResult InsertBasicGrid()
        {
            string TypeId = Request["TypeId"].ToString();
            string TextDesc = Request["TextDesc"].ToString();
            bool b = SalesRetailMan.AddBasicInfo(TypeId, TextDesc);

            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult UpdateBasicGrid(string id, string type, string text)
        {
            ViewData["XID"] = id;
            ViewData["Type"] = type;
            ViewData["Text"] = text;

            return View();
        }

        public ActionResult AlterBasicGrid()
        {
            string XID = Request["XID"].ToString();
            string TypeId = Request["TypeId"].ToString();
            string TextDesc = Request["TextDesc"].ToString();

            bool b = SalesRetailMan.AlterBasicInfo(XID, TypeId, TextDesc);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult DeleteBasicGrid()
        {
            string XID = Request["XID"].ToString();
            string TypeId = Request["TypeId"].ToString();
            bool b = SalesRetailMan.DeleteBasicInfo(XID, TypeId);
            if (b)
            {
                return Json(new { success = true, msg = "删除成功" });
            }
            else
            {
                return Json(new { success = false, msg = "删除失败" });
            }

        }

        /// <summary>
        /// 5S店设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SetFiveMalls()
        {
            return View();
        }

        public ActionResult GetFivMallsGrid()
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
            string HigherUnitID = Request["sel"].ToString();
            if (HigherUnitID != "")
                where += " and a.HigherUnitID ='" + HigherUnitID + "' ";
            UIDataTable udtTask = new UIDataTable();

            udtTask = SalesRetailMan.GetMallsGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddFiveMalls(string id)
        {
            ViewData["HigherUnitID"] = id;
            return View();
        }

        public ActionResult InsertMalls()
        {
            string HigherUnitID = Request["HigherUnitID"].ToString();
            string Malls = Request["Malls"].ToString();

            bool b = SalesRetailMan.InsertMallsInfo(HigherUnitID, Malls);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult UpdateFiveMalls(string id, string higherUnitID, string malls)
        {
            ViewData["ID"] = id;
            ViewData["HigherUnitID"] = higherUnitID;
            ViewData["Malls"] = malls;
            return View();
        }

        public ActionResult AlterMallsInfo()
        {
            string ID = Request["ID"].ToString();
            string HigherUnitID = Request["HigherUnitID"].ToString();
            string Text = Request["Text"].ToString();
            bool b = SalesRetailMan.UpdateMallsInfo(ID, HigherUnitID, Text);
            if (b)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult DeleteMalls()
        {
            string ID = Request["ID"].ToString();
            string HigherUnitID = Request["HigherUnitID"].ToString();
            bool b = SalesRetailMan.DeleteMalls(ID, HigherUnitID);
            if (b)
            {
                return Json(new { success = true, msg = "删除成功" });
            }
            else
            {
                return Json(new { success = false, msg = "删除失败" });
            }

        }
        #endregion

        #region 销售提醒
        public ActionResult SalesRemind(string SalesType)
        {
            ViewData["SalesType"] = SalesType;
            return View();
        }

        public ActionResult GetSalesRemindList()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string SalesType = Request["SalesType"].ToString();

            UIDataTable udtTask = SalesRetailMan.GetRemindList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, GAccount.GetAccountInfo().UnitName, SalesType);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNowRemind(string SalesType)
        {
            DataTable dt = SalesRetailMan.GetNowRemindInfo(GAccount.GetAccountInfo().UnitName, SalesType, GAccount.GetAccountInfo().UserID.ToString());
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopRetailLibraryTubeManage()
        {
            DataTable dt = SalesRetailMan.GetTopRetailLibraryTubeManage(GAccount.GetAccountInfo().UnitID.ToString());
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopRetailAfterSaleManage()
        {
            DataTable dt = SalesRetailMan.GetTopRetailAfterSaleManage(GAccount.GetAccountInfo().UnitID.ToString());
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 零售销售
        public ActionResult ApprovalRetail()
        {
            string text = "零售销售审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApprovalRetail(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string isHK = salesGrid.IsHK;
                string strWhere = "";
                string strWhere2 = "";
                strWhere += " and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";

                if (salesProduct != "" && salesProduct != null)
                    strWhere2 += " and c.OrderContent='" + salesProduct + "' ";
                if (specsModels != "" && specsModels != null)
                    strWhere2 += " and c.SpecsModels='" + specsModels + "' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and a.ProvidManager='" + salesMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and a.ContractDate between '" + startDate + "' and '" + endDate + "' ";
                if (!string.IsNullOrEmpty(isHK))
                    strWhere += " and a.IsHK='" + isHK + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetRetailApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, strWhere2);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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

        public ActionResult GetConfigInfo(string TaskType)
        {
            DataTable dt = SalesRetailMan.GetSelectInfo(TaskType);

            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalesRetailManage()
        {
            //string text = "零售销售审批";

            //ViewData["webkey"] = text;
            //string fold = COM_ApprovalMan.getNewwebkey(text);
            //ViewData["folderBack"] = fold;
            //string[] arr = fold.Split('/');
            //ViewData["Nostate"] = arr[7];
            return View();
        }


        public ActionResult GetConfigBelongCom(string ConfigType, string ChidGrade)
        {
            DataTable dt = SalesRetailMan.GetConfigBelongCom(ConfigType, ChidGrade);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetConfigRetail(string ConfigType, string ChidGrade)
        {
            DataTable dt = SalesRetailMan.GetConfigRetail(ConfigType, ChidGrade);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示销售产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesProductInfo()
        {
            return View();
        }

        public ActionResult GetBelongCom()
        {
            string strErr = "";
            string strJsonSubUnits = SalesRetailMan.GetSubCom(ref strErr);

            return Json(strJsonSubUnits, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所在区域的零售物品数据
        /// </summary>
        /// <param name="SalesProduct"></param>
        /// <param name="SpecsModels"></param>
        /// <param name="SalesMan"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="IsHK"></param>
        /// <returns></returns>
        public ActionResult GetBelongComSalesRetailList(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                //  string isHK = salesGrid.IsHK;
                //string state = salesGrid.State;
                string strWhere = "";
                string strWhere2 = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                //string Validate = "v";
                //string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Retail");

                //strWhere += " and a.Validate='" + Validate + "' ";

                //if (filed == "0")
                //{
                //    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Retail' ";
                //}


                if (salesProduct != "" && salesProduct != null)
                    strWhere += " and a.OrderContent='" + salesProduct + "' ";
                if (specsModels != "" && specsModels != null)
                    strWhere += " and a.SpecsModels='" + specsModels + "' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and b.ProvidManager='" + salesMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and b.ContractDate between '" + startDate + "' and '" + endDate + "' ";


                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string MallID = Request["MallID"].ToString();
                string strGrade = Request["strGrade"].ToString();

                UIDataTable udtTask = SalesRetailMan.GetBelongComSalesRetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, MallID, strGrade);
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




        public ActionResult PrintRetail(string SalesProduct, string SpecsModels, string SalesMan, string StartDate, string EndDate, string IsHK)
        {
            string strWhere = "";
            string strWhere2 = "";
            string Validate = "v";

            strWhere += " and a.Validate='" + Validate + "' ";

            if (SalesProduct != "" && SalesProduct != null)
                strWhere2 += " and b.OrderContent='" + SalesProduct + "' ";
            if (SpecsModels != "" && SpecsModels != null)
                strWhere2 += " and b.SpecsModels='" + SpecsModels + "' ";
            if (SalesMan != "" && SalesMan != null)
                strWhere += " and a.ProvidManager='" + SalesMan + "' ";
            if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                strWhere += " and a.ContractDate between '" + StartDate + "' and '" + EndDate + "' ";
            if (!string.IsNullOrEmpty(IsHK))
                strWhere += " and a.IsHK='" + IsHK + "' ";

            DataTable dt = SalesRetailMan.GetRetailToPrint(GAccount.GetAccountInfo().UnitID, strWhere, "-1", strWhere2);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();

            string html = "";
            string html1 = "";

            sb.Append("<div class='PrintArea' style='page-break-after: always;height :1000px;'><div  style='text-align:center;font-size:18px; margin-top:20px;'>业务人员日常项目登记表</div>");
            //if (dt != null && dt.Rows.Count > 0)
            if (dt != null)
            {
                if (dt.Rows.Count <= 5)
                {
                    StringBuilder sb2 = new StringBuilder();
                    // sb1.Append("<tr><td style='width:5%; text-align:center;height:18px;'>序号</td><td style='width:6%; text-align:center;height:18px;'>立项时间</td><td style='width:8%; text-align:center;height:18px;'>项目名称简述</td><td style='width:6%; text-align:center;height:18px;'>产品名称</td><td style='width:6%; text-align:center;height:18px;'>规格材质</td><td style='width:6%; text-align:center;height:18px;'>数量</td><td style='width:6%; text-align:center;height:6px;'>成本单价</td><td style='width:6%; text-align:center;height:18px;'>金额小计</td><td style='width:6%; text-align:center;height:18px;'>款项情况</td><td style='width:6%; text-align:center;height:18px;'>计划供货时间</td><td style='width:9%; text-align:center;height:18px;'>最终安装地址</td><td style='width:6%; text-align:center;height:18px;'>联系人</td><td style='width:6%; text-align:center;height:18px;'>联系电话</td><td style='width:6%; text-align:center;height:18px;'>施工单位</td><td style='width:12%; text-align:center;height:18px;'>备注</td></tr>");

                    sb1.Append("<table class='tabInfo'><tr align='center' valign='middle'><th style='width:5%; text-align:center;height:18px;'>序号</th><th style='width:6%; text-align:center;height:18px;'>立项时间</th><th style='width:8%; text-align:center;height:18px;'>项目名称</th><th style='width:6%; text-align:center;height:18px;'>产品名称</th><th style='width:6%; text-align:center;height:18px;'>规格材质</th><th style='width:6%; text-align:center;height:18px;'>数量</th><th style='width:6%; text-align:center;height:6px;'>单价</th><th style='width:6%; text-align:center;height:18px;'>金额小计</th><th style='width:6%; text-align:center;height:18px;'>款项情况</th><th style='width:6%; text-align:center;height:18px;'>销售时间</th><th style='width:9%; text-align:center;height:18px;'>最终安装地址</th><th style='width:6%; text-align:center;height:18px;'>客户联系人</th><th style='width:6%; text-align:center;height:18px;'>客户联系电话</th><th style='width:6%; text-align:center;height:18px;'>施工单位</th><th style='width:12%; text-align:center;height:18px;'>备注</th><tbody id='DetailInfo'></tr>");


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb2.Append("<tr style='font-size:18px;'><td style='width:5%; text-align:center;'>" + (i + 1) + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["ContractDate"].ToString() + "</td><td style='width:8%; text-align:center;'>" + dt.Rows[i]["ProjectName"].ToString() + "</td>"
                        + "<td style='width:6%; text-align:center;'>" + dt.Rows[i]["OrderContent"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["OrderNum"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["SpecsModels"].ToString() + "</td>"
                        + "<td style='width:6%; text-align:center;'>" + dt.Rows[i]["UnitPrice"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["DTotalPrice"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["ProductRemark"].ToString() + "</td>"
                        + "<td style='width:6%; text-align:center;'>" + dt.Rows[i]["SupplyTime"].ToString() + "</td><td style='width:9%; text-align:center;'>" + dt.Rows[i]["UseAddress"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["OrderContactor"].ToString() + "</td>"
                        + "<td style='width:6%; text-align:center;'>" + dt.Rows[i]["UseTel"].ToString() + "</td><td style='width:6%; text-align:center;'>" + dt.Rows[i]["OrderUnit"].ToString() + "</td><td style='width:12%; text-align:center;'>" + dt.Rows[i]["Remark"].ToString() + "</td></tr>");
                    }
                    sb2.Append("</tbody></table></div>");
                    html = sb.ToString() + sb1.ToString() + sb2.ToString();
                }
                else
                {
                    int count = dt.Rows.Count % 5;
                    if (count > 0)
                        count = dt.Rows.Count / 5 + 1;
                    else
                        count = dt.Rows.Count / 5;
                    for (int i = 0; i < count; i++)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb1 = new StringBuilder();
                        int a = 5 * i;
                        int length = 5 * (i + 1);
                        if (length > dt.Rows.Count)
                            length = 5 * i + dt.Rows.Count % 5;
                        // sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><div  style='text-align:center;font-size:18px; margin-top:20px;'>业务人员日常项目登记表</div>");
                        // sb.Append("<table><tr style='height:60px;'><td colspan='15' style='text-align:center; font-size:25px; font-weight:bold;'>业务人员日常项目登记表</td></tr>");
                        sb1.Append("<table class='tabInfo'><tr align='center' valign='middle'><th>序号</th><th>立项时间</th>"
                              + "<th>项目名称</th><th>产品名称</th><th>规格材质</th>"
                              + "<th>数量</th><th>单价</th><th>金额小计</th>"
                              + "<th>款项情况</th><th>销售日期</th><th>最终安装地址</th>"
                              + "<th>客户联系人</th><th>客户联系电话</th><th>安装单位</th>"
                              + "<th>备注</th></tr><tbody>");
                        for (int j = a; j < length; j++)
                        {

                            sb2.Append("<tr><td style='width:5%; text-align:center;'>" + (j + 1) + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["ContractDate"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["ProjectName"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["OrderContent"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["SpecsModels"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["OrderNum"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["UnitPrice"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["DTotalPrice"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["ProductRemark"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["SupplyTime"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["UseAddress"].ToString() + "</td><td style='width:40px text-align:center;'>" + dt.Rows[j]["OrderContactor"].ToString() + "</td><td style='width:40px; text-align:center;'>" + dt.Rows[j]["UseTel"].ToString() + "</td><td style='width:40px; text-align:center;'>" + dt.Rows[j]["OrderUnit"].ToString() + "</td><td style='width:40px; text-align:center;'>" + dt.Rows[j]["Remark"].ToString() + "</td></tr>");
                        }
                        if ((length - a) < 10)
                        {
                        }

                        sb2.Append("</tbody></table></div>");
                        html += sb.ToString() + sb1.ToString() + sb2.ToString();
                    }
                }
                // html += html1;
                // html += "</table></div>";

            }
            Response.Write(html);

            return View();
        }

        public FileResult RetailToExcel(tk_SalesGrid sales)
        {
            string salesProduct = sales.SalesProduct;
            string specsModels = sales.SpecsModels;
            string salesMan = sales.SalesMan;
            string startDate = sales.StartDate;
            string endDate = sales.EndDate;
            string isHK = sales.IsHK;
            //string state = sales.State;
            string UnitId = GAccount.GetAccountInfo().UnitID;

            string strWhere = "";
            string strWhere2 = "";
            string Validate = "v";

            strWhere += " and a.Validate='" + Validate + "' ";

            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Retail");

            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Retail' ";
            }
            //else
            //{
            //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            //}

            if (salesProduct != "" && salesProduct != null)
                strWhere2 += " and b.OrderContent='" + salesProduct + "' ";
            if (specsModels != "" && specsModels != null)
                strWhere2 += " and b.SpecsModels='" + specsModels + "' ";
            if (salesMan != "" && salesMan != null)
                strWhere += " and a.ProvidManager='" + salesMan + "' ";
            if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                strWhere += " and a.ContractDate between '" + startDate + "' and '" + endDate + "' ";
            if (!string.IsNullOrEmpty(isHK))
                strWhere += " and a.IsHK='" + isHK + "' ";


            DataTable dt = SalesRetailMan.GetRetailToPrint(UnitId, strWhere, filed, strWhere2);
            if (dt != null)
            {
                string strCols = "立项时间-6000,项目名称简述-8000,产品名称-6000,规格材质-8000,数量-4000,单价-6000,金额小计-6000,"
                    + "款项情况-10000,计划供货时间-6000,最终安装地址-8000,联系人-6000,联系电话-6000,施工单位-6000,备注-12000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "业务人员日常项目登记表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "业务人员日常项目登记表.xls");
            }
            else
                return null;
        }

        public ActionResult SalesRecord()
        {
            OrdersInfo order = new OrdersInfo();
            string unitID = GAccount.GetAccountInfo().UnitID;
            order.OrderID = SalesRetailMan.GetDHNO(unitID, "DH", "Retail");
            return View(order);
        }

        public ActionResult SaveSalesRecord(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                OrdersInfo order = new OrdersInfo();
                string TaskLength = fc["TaskLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrOrderNum = (HttpContext.ApplicationInstance.Context.Request["OrderNum"]).Split(',');
                string[] TaxRate = (HttpContext.ApplicationInstance.Context.Request["TaxRate"].Split(','));
                string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                string[] arrDTotalPrice = (HttpContext.ApplicationInstance.Context.Request["DTotalPrice"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                string[] arrChannel = (HttpContext.ApplicationInstance.Context.Request["Channels"]).Split(',');
                string[] arrBelongCom = (HttpContext.ApplicationInstance.Context.Request["BelongCom"]).Split(',');
                GSqlSentence.SetTValue<OrdersInfo>(order, HttpContext.ApplicationInstance.Context.Request);
                // OrdersInfo order = new OrdersInfo();
                // order.Provider = Request["ProvidManager"].ToString();
                //order.IsHK = Request["IsHK"].ToString();
                if (HttpContext.ApplicationInstance.Context.Request["ISFinish"] != "")
                {
                    order.ISFinish = Convert.ToInt32(Request["ISFinish"].ToString());
                }
                if (Request["SupplyTime"] != "")
                {
                    order.SupplyTime = Convert.ToDateTime(Request["SupplyTime"]);
                }
                order.OrderUnit = Request["OrderUnit"].ToString();
                order.OrderContactor = Request["OrderContactor"].ToString();
                order.OrderTel = Request["OrderTel"].ToString();
                order.UseAddress = Request["UseAddress"].ToString();
                Alarm alarm = new Alarm();
                alarm.OrderID = order.OrderID;
                alarm.Operator = GAccount.GetAccountInfo().UserName.ToString();
                alarm.OperationTime = DateTime.Now.ToString();
                alarm.OperationContent = "添加销售记录";

                // order.Remark = Request["Remark"].ToString();
                // order.ISFinish = Request["ISFinish"].ToString();
                if (Request["ContractDate"] != "")
                {
                    order.ContractDate = Convert.ToDateTime(Request["ContractDate"].ToString());
                }
                order.ProvidManager = HttpContext.ApplicationInstance.Context.Request["ProvidManager"].ToString();
                order.IsHK = Request["IsHK"].ToString();
                order.Remark = HttpContext.ApplicationInstance.Context.Request["Remark"].ToString();
                order.HKRemark = Request["HKRemark"].ToString();

                order.LibraryTubeState = "0";
                order.AfterSaleState = "0";
                order.LibraryTubeTime = DateTime.Now.ToString();
                order.UnitID = GAccount.GetAccountInfo().UnitID;
                order.SalesType = "Sa03";
                order.CreateUser = GAccount.GetAccountInfo().UserName;
                order.CreateTime = DateTime.Now.ToString();
                order.Validate = "v";
                order.State = "0";

                Orders_DetailInfo orderDetail = new Orders_DetailInfo();
                List<Orders_DetailInfo> orderlist = new List<Orders_DetailInfo>();
                for (int i = 0; i < arrOrderContent.Length; i++)
                {
                    orderDetail = new Orders_DetailInfo();
                    orderDetail.ProductID = arrProductID[i].ToString();
                    orderDetail.OrderContent = arrOrderContent[i].ToString();
                    orderDetail.OrderID = order.OrderID;
                    orderDetail.DID = order.OrderID + GFun.GetNum((i + 1), 3);
                    orderDetail.SpecsModels = arrSpecsModels[i];
                    if (arrOrderNum[i] != "")
                    {
                        orderDetail.OrderNum = int.Parse(arrOrderNum[i]);
                    }
                    orderDetail.TaxRate = TaxRate[i].ToString();
                    orderDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                    orderDetail.DTotalPrice = Convert.ToDecimal(arrDTotalPrice[i]);
                    orderDetail.DeliveryTime = DateTime.Now;
                    orderDetail.State = "0";
                    orderDetail.Remark = arrRemark[i];
                    orderDetail.Channel = arrChannel[i];
                    orderDetail.BelongCom = arrBelongCom[i];
                    orderDetail.CreateTime = DateTime.Now.ToString();
                    orderDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    orderDetail.Validate = "v";

                    orderlist.Add(orderDetail);
                }
                string strErr = "";
                bool b = SalesRetailMan.SaveSalesRecord(order, alarm, orderlist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = order.OrderID;
                    salesLog.LogContent = "添加销售记录";
                    salesLog.ProductType = "零售销售";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }


        public ActionResult GetLogGrid()
        {
            string ID = Request["ID"];
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //string FirstCode = Xid.Substring(0, 2);
            UIDataTable udtTask = SalesManage.GetLogGrid(ID, GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateRecord(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                OrdersInfo order = new OrdersInfo();
                string TaskLength = fc["TaskLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = Request["OrderContent"].Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrOrderNum = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                string[] arrTaxRate = (HttpContext.ApplicationInstance.Context.Request["TaxRate"]).Split(',');
                string[] arrDTotalPrice = (HttpContext.ApplicationInstance.Context.Request["DTotalPrice"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                string[] arrBelongCom = (HttpContext.ApplicationInstance.Context.Request["BelongCom"]).Split(',');
                string[] arrChannels = (HttpContext.ApplicationInstance.Context.Request["Channels"]).Split(',');
                GSqlSentence.SetTValue<OrdersInfo>(order, HttpContext.ApplicationInstance.Context.Request);
                order.ProvidManager = HttpContext.ApplicationInstance.Context.Request["ProvidManager"].ToString();
                order.IsHK = HttpContext.ApplicationInstance.Context.Request["IsHK"].ToString();
                order.State = HttpContext.ApplicationInstance.Context.Request["State"].ToString();
                order.Remark = HttpContext.ApplicationInstance.Context.Request["Remark"].ToString();
                order.UnitID = GAccount.GetAccountInfo().UnitID;
                order.SalesType = "Sa03";
                order.CreateUser = GAccount.GetAccountInfo().UserName;
                order.CreateTime = DateTime.Now.ToString();
                order.Validate = "v";
                // order.ISFinish =Convert.ToInt32( Request["ISFinish"].ToString());
                //order.IsHK = Request["ISHK"].ToString();
                Orders_DetailInfo orderDetail = new Orders_DetailInfo();
                List<Orders_DetailInfo> orderlist = new List<Orders_DetailInfo>();
                for (int i = 0; i < arrOrderContent.Length; i++)
                {
                    orderDetail = new Orders_DetailInfo();
                    orderDetail.ProductID = arrProductID[i].ToString();
                    orderDetail.OrderContent = arrOrderContent[i].ToString();
                    orderDetail.OrderID = order.OrderID;
                    orderDetail.DID = order.OrderID + GFun.GetNum((i + 1), 3);
                    orderDetail.SpecsModels = arrSpecsModels[i];
                    orderDetail.OrderNum = int.Parse(arrOrderNum[i]);
                    orderDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                    orderDetail.TaxRate = arrTaxRate[i].ToString();
                    orderDetail.DTotalPrice = Convert.ToDecimal(arrDTotalPrice[i]);
                    orderDetail.DeliveryTime = DateTime.Now;
                    orderDetail.State = "0";
                    orderDetail.Remark = arrRemark[i];
                    orderDetail.BelongCom = arrBelongCom[i];
                    orderDetail.Channel = arrChannels[i];
                    orderDetail.CreateTime = DateTime.Now.ToString();
                    orderDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    orderDetail.Validate = "v";

                    orderlist.Add(orderDetail);
                }
                string strErr = "";
                bool b = SalesRetailMan.UpdateSalesRecord(order, orderlist, GAccount.GetAccountInfo().UserName, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = order.OrderID;
                    salesLog.LogContent = "修改销售记录";
                    salesLog.ProductType = "零售销售";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }


        public ActionResult DeleteRecord()
        {
            string OrderID = Request["OrderID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeleteRecord(OrderID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = OrderID;
                salesLog.LogContent = "撤销销售记录";
                salesLog.ProductType = "零售销售";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "撤销成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }

        }

        public ActionResult UpdateSalesRecord(string OrderID)
        {
            OrdersInfo info = new OrdersInfo();
            info = SalesRetailMan.GetOrderInfo(OrderID);

            return View(info);
        }

        public ActionResult GetOrderDetail(string orderID)
        {
            DataTable dt = SalesRetailMan.GetOrderDetailInfo(orderID);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        #region 添加物品
        public ActionResult GetProduct()
        {
            return View();
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

        public ActionResult GetSupplier()
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
        #endregion

        public ActionResult GetSalesGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string isHK = salesGrid.IsHK;
                string state = salesGrid.State;
                string OrderContactor = salesGrid.OrderContactor;
                string OrderTel = salesGrid.OrderTel;
                string ISCollection = salesGrid.ISCollection;
                string strWhere = "";
                string strWhere2 = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                string Validate = "v";
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Retail");

                if (state != "" && state != "-2" && state != null)
                {
                    strWhere += " and a.State='" + state + "' ";
                }
                else if (state == "-2")
                    Validate = "i";

                strWhere += " and a.Validate='" + Validate + "' ";

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Retail' ";
                }
                //else
                //{
                //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}

                if (salesProduct != "" && salesProduct != null)
                    strWhere2 += " and b.OrderContent like '%" + salesProduct + "%' ";
                if (specsModels != "" && specsModels != null)
                    strWhere2 += " and b.SpecsModels like '%" + specsModels + "%' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and a.ProvidManager like '%" + salesMan + "%' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and a.ContractDate between '" + startDate + "' and '" + endDate + "' ";
                if (!string.IsNullOrEmpty(isHK))
                    strWhere += " and a.IsHK='" + isHK + "' ";
                if (OrderContactor != "" && OrderContactor != null)
                {
                    strWhere += " and a.OrderContactor like '%" + OrderContactor + "%' ";
                }
                if (OrderTel != "" && OrderTel != null)
                {
                    strWhere += " and a.OrderTel like'%" + OrderTel + "%'";
                }
                if (ISCollection != "" && ISCollection != null)
                {

                    strWhere += " and a.ISCollection =" + ISCollection;
                }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetSalesRetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, UnitId, strWhere, filed, strWhere2);
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

        /// <summary>
        /// 物品详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailGrid()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string OrderID = Request["OrderID"].ToString();

            UIDataTable udtTask = SalesRetailMan.GetDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, OrderID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 内购管理
        public ActionResult InternalManage(string op)
        {
            string text = "";
            ViewData["op"] = op;
            if (op == "NG")
                text = "内购审批";
            else if (op == "ZS")
                text = "赠送审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];

            return View();
        }

        public ActionResult ApplyInternal(string op)
        {
            tk_InternalOrder interOrder = new tk_InternalOrder();
            string unitID = GAccount.GetAccountInfo().UnitID;

            if (op == "NG")
            {
                interOrder.Type = "0";
                interOrder.IOID = SalesRetailMan.GetDHNO(unitID, "NG", "Internal");
            }
            else if (op == "ZS")
            {
                interOrder.Type = "1";
                interOrder.IOID = SalesRetailMan.GetDHNO(unitID, "ZS", "Internal");
            }

            return View(interOrder);
        }

        public ActionResult SaveInternalOrder(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_InternalOrder interOrder = new tk_InternalOrder();
                GSqlSentence.SetTValue<tk_InternalOrder>(interOrder, HttpContext.ApplicationInstance.Context.Request);
                List<tk_InternalDetail> interlist = new List<tk_InternalDetail>();
                if (interOrder.Type == "0")
                {
                    //    string TaskLength = fc["TaskLength"].ToString();
                    string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                    string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                    string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                    string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                    string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                    string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                    string[] arrDiscounts = (HttpContext.ApplicationInstance.Context.Request["Discounts"]).Split(',');
                    string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                    string[] arrPayWay = (HttpContext.ApplicationInstance.Context.Request["PayWay"]).Split(',');
                    string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                    interOrder.CreateUser = GAccount.GetAccountInfo().UserName;
                    interOrder.CreateTime = DateTime.Now;
                    interOrder.Validate = "v";
                    interOrder.UnitID = GAccount.GetAccountInfo().UnitID;
                    interOrder.State = "0";

                    tk_InternalDetail interDetail = new tk_InternalDetail();
                    for (int i = 0; i < arrOrderContent.Length; i++)
                    {
                        interDetail = new tk_InternalDetail();
                        interDetail.OrderContent = arrOrderContent[i].ToString();
                        interDetail.IOID = interOrder.IOID;
                        interDetail.DID = interOrder.IOID + "-" + GFun.GetNum((i + 1), 3);
                        interDetail.ProductID = arrProductID[i];
                        interDetail.Specifications = arrSpecsModels[i];
                        interDetail.GoodsType = arrGoodsType[i];
                        interDetail.Amount = Convert.ToInt32(arrAmount[i]);
                        interDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                        if (arrDiscounts[i] != "")
                        { interDetail.Discounts = Convert.ToDecimal(arrDiscounts[i]); }

                        interDetail.Total = Convert.ToDecimal(arrTotal[i]);
                        interDetail.State = "0";
                        interDetail.IState = "0";
                        interDetail.PayWay = arrPayWay[i];
                        interDetail.Remark = arrRemark[i];
                        interDetail.CreateTime = DateTime.Now;
                        interDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        interDetail.Validate = "v";

                        interlist.Add(interDetail);
                    }
                }
                else if (interOrder.Type == "1")
                {
                    //string TaskLength = fc["TaskLength"].ToString();
                    string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                    string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                    string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                    string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                    string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                    string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                    string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                    interOrder.CreateUser = GAccount.GetAccountInfo().UserName;
                    interOrder.CreateTime = DateTime.Now;
                    interOrder.Validate = "v";
                    interOrder.UnitID = GAccount.GetAccountInfo().UnitID;
                    interOrder.State = "0";
                    //  interOrder .SendDepartment =(HttpContext.ApplicationInstance.Context .Request ["SendDepartment"])
                    if (Request["IAmount"] != "")
                    {
                        interOrder.Amount = Convert.ToDecimal(Request["IAmount"]);
                    }
                    tk_InternalDetail interDetail = new tk_InternalDetail();
                    for (int i = 0; i < arrOrderContent.Length; i++)
                    {
                        interDetail = new tk_InternalDetail();
                        interDetail.OrderContent = arrOrderContent[i].ToString();
                        interDetail.IOID = interOrder.IOID;
                        interDetail.DID = interOrder.IOID + "-" + GFun.GetNum((i + 1), 3);
                        interDetail.ProductID = arrProductID[i];
                        interDetail.Specifications = arrSpecsModels[i];
                        interDetail.GoodsType = arrGoodsType[i];
                        if (arrAmount[i] != "")
                        {
                            interDetail.Amount = Convert.ToInt32(arrAmount[i]);
                        }
                        if (arrUnitPrice[i] != "")
                        {
                            interDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                        }
                        interDetail.State = "0";
                        interDetail.Remark = arrRemark[i];
                        interDetail.CreateTime = DateTime.Now;
                        interDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        interDetail.Validate = "v";

                        interlist.Add(interDetail);
                    }
                }
                string strErr = "";
                bool b = SalesRetailMan.SaveInternalRecord(interOrder, interlist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = interOrder.IOID;
                    salesLog.LogContent = "创建内购申请";
                    salesLog.ProductType = "内购管理";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }

        public ActionResult UpdateInternalOrder(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_InternalOrder interOrder = new tk_InternalOrder();
                GSqlSentence.SetTValue<tk_InternalOrder>(interOrder, HttpContext.ApplicationInstance.Context.Request);
                List<tk_InternalDetail> interlist = new List<tk_InternalDetail>();
                if (interOrder.Type == "0")
                {
                    string TaskLength = fc["TaskLength"].ToString();
                    string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                    string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                    string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                    string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                    string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                    string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                    string[] arrDiscounts = (HttpContext.ApplicationInstance.Context.Request["Discounts"]).Split(',');
                    string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                    string[] arrPayWay = (HttpContext.ApplicationInstance.Context.Request["PayWay"]).Split(',');
                    string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                    interOrder.CreateUser = GAccount.GetAccountInfo().UserName;
                    interOrder.CreateTime = DateTime.Now;
                    interOrder.Validate = "v";
                    interOrder.UnitID = GAccount.GetAccountInfo().UnitID;

                    tk_InternalDetail interDetail = new tk_InternalDetail();
                    for (int i = 0; i < arrOrderContent.Length; i++)
                    {
                        interDetail = new tk_InternalDetail();
                        interDetail.OrderContent = arrOrderContent[i].ToString();
                        interDetail.IOID = interOrder.IOID;
                        interDetail.DID = interOrder.IOID + "-" + GFun.GetNum((i + 1), 3);
                        interDetail.ProductID = arrProductID[i];
                        interDetail.Specifications = arrSpecsModels[i];
                        interDetail.GoodsType = arrGoodsType[i];
                        interDetail.Amount = Convert.ToInt32(arrAmount[i]);
                        interDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                        interDetail.Discounts = Convert.ToDecimal(arrDiscounts[i]);
                        interDetail.Total = Convert.ToDecimal(arrTotal[i]);
                        interDetail.PayWay = arrPayWay[i];
                        interDetail.Remark = arrRemark[i];
                        interDetail.CreateTime = DateTime.Now;
                        interDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        interDetail.Validate = "v";

                        interlist.Add(interDetail);
                    }
                }
                else if (interOrder.Type == "1")
                {
                    string TaskLength = fc["TaskLength"].ToString();
                    string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                    string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                    string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                    string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                    string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                    string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                    string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                    interOrder.CreateUser = GAccount.GetAccountInfo().UserName;
                    interOrder.CreateTime = DateTime.Now;
                    interOrder.Validate = "v";
                    interOrder.UnitID = GAccount.GetAccountInfo().UnitID;

                    tk_InternalDetail interDetail = new tk_InternalDetail();
                    for (int i = 0; i < arrOrderContent.Length; i++)
                    {
                        interDetail = new tk_InternalDetail();
                        interDetail.OrderContent = arrOrderContent[i].ToString();
                        interDetail.IOID = interOrder.IOID;
                        interDetail.DID = interOrder.IOID + "-" + GFun.GetNum((i + 1), 3);
                        interDetail.ProductID = arrProductID[i];
                        interDetail.Specifications = arrSpecsModels[i];
                        interDetail.GoodsType = arrGoodsType[i];
                        interDetail.Amount = Convert.ToInt32(arrAmount[i]);
                        interDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                        interDetail.Remark = arrRemark[i];
                        interDetail.CreateTime = DateTime.Now;
                        interDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                        interDetail.Validate = "v";

                        interlist.Add(interDetail);
                    }
                }

                string strErr = "";
                bool b = SalesRetailMan.UpdateInternalRecord(interOrder, interlist, GAccount.GetAccountInfo().UserName, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = interOrder.IOID;
                    salesLog.LogContent = "修改内购申请";
                    salesLog.ProductType = "内购管理";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }

        public FileResult InternalToExcel(tk_SalesGrid salesGrid)
        {
            string salesProduct = salesGrid.SalesProduct;
            string specsModels = salesGrid.SpecsModels;
            string applyMan = salesGrid.ApplyMan;
            string startDate = salesGrid.StartDate;
            string endDate = salesGrid.EndDate;
            string hdOp = Request["hdOp"].ToString();
            string UnitId = GAccount.GetAccountInfo().UnitID;
            string op = "";

            string TaskType = "";
            if (hdOp == "NG")
            {
                TaskType = "Internal";
                op = "0";
            }
            else if (hdOp == "ZS")
            {
                TaskType = "Send";
                op = "1";
            }

            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, TaskType);

            string strWhere = "";
            string strWhere2 = "";
            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='" + TaskType + "' ";
            }
            //else
            //{
            //    strWhere += " and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            //}
            if (salesProduct != "" && salesProduct != null)
                strWhere2 += " and b.OrderContent='" + salesProduct + "' ";
            if (specsModels != "" && specsModels != null)
                strWhere2 += " and b.Specifications='" + specsModels + "' ";
            if (applyMan != "" && applyMan != null)
                strWhere += " and a.Applyer='" + applyMan + "' ";
            if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                strWhere += " and a.ContractDate between '" + startDate + "' and '" + endDate + "' ";
            if (op != "" && op != null)
                strWhere += " and a.Type='" + op + "' ";


            DataTable dt = SalesRetailMan.GetInternalToPrint(UnitId, strWhere, filed, strWhere2);
            if (dt != null)
            {
                string Title = "";
                string strCols = "";
                if (op == "0")
                {
                    strCols = "申请编号-6000,内购产品-12000,申请日期-8000,申请人-6000,出库仓库-6000,主管负责人-10000,内购公司商品使用人-5000,状态-6000";
                    Title = "内购产品统计表";
                }
                else if (op == "1")
                {
                    strCols = "申请编号-6000,赠送产品-12000,申请人-6000,申请日期-8000,出库仓库-6000,主管负责人-10000,内购公司商品使用人-5000,状态-6000";
                    Title = "赠送产品统计表";
                }
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, Title, strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "" + Title + ".xls");
            }
            else
                return null;
        }

        public ActionResult GetInternalGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string applyMan = salesGrid.ApplyMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string op = Request["OP"].ToString();
                string orderunit = salesGrid.OrderUnit;
                string sendremark = salesGrid.SendRemark;
                string UnitId = GAccount.GetAccountInfo().UnitID;

                string TaskType = "";
                if (op == "0")
                    TaskType = "Internal";
                else if (op == "1")
                    TaskType = "Send";

                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, TaskType);

                string strWhere = "";
                string strWhere2 = "";
                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and c.TaskType='" + TaskType + "' ";
                }
                //else
                //{
                //    strWhere += " and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}
                if (salesProduct != "" && salesProduct != null)
                    strWhere2 += " and b.OrderContent='" + salesProduct + "' ";
                if (specsModels != "" && specsModels != null)
                    strWhere2 += " and b.Specifications='" + specsModels + "' ";
                if (applyMan != "" && applyMan != null)
                    strWhere += " and a.Applyer='" + applyMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and a.OrderDate between '" + startDate + "' and '" + endDate + "' ";
                if (op != "" && op != null)
                    strWhere += " and a.Type='" + op + "' ";
                if (orderunit != "" && orderunit != null)
                {
                    strWhere += " and a.SendDepartment like'%" + orderunit + "%'";
                }
                if (sendremark != "" && sendremark != null)
                {
                    strWhere += " and a.SendRemark like'%" + sendremark + "%'";
                }



                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetInternalList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, UnitId, strWhere, filed, strWhere2);
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

        public ActionResult DeleteInternal()
        {
            string IOID = Request["IOID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeleteInternalApply(IOID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = IOID;
                salesLog.LogContent = "撤销销售记录";
                salesLog.ProductType = "零售销售";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }
        }

        public ActionResult UpdateInternal(string IOID, string op)
        {
            string type = "";
            if (op == "NG")
                type = "0";
            else if (op == "ZS")
                type = "1";

            tk_InternalOrder order = new tk_InternalOrder();
            order = SalesRetailMan.GetInternalOrder(IOID, type);

            return View(order);
        }

        public ActionResult GetInternalDetailInfo(string IOID, string op)
        {
            DataTable dt = SalesRetailMan.GetInternalDetail(IOID, op);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInternalDetail()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string IOID = Request["IOID"].ToString();

            UIDataTable udtTask = SalesRetailMan.GetInterDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, IOID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApprovalManage(string op)
        {
            string text = "";
            if (op == "NG")
                text = "内购审批";
            else if (op == "ZS")
                text = "赠送审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApproval(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string IOID = sales.IOID;
                string SalesProduct = sales.SalesProduct;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;

                string strWhere = "";
                string strWhere2 = "";

                if (IOID != "" && IOID != null)
                    strWhere += " and a.IOID like'%" + IOID + "%'";
                if (SalesProduct != "" && SalesProduct != null)
                    strWhere2 += " and b.OrderContent like'%" + SalesProduct + "%' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and a.OrderDate between '" + StartDate + "' and '" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string op = Request["Op"].ToString();
                if (op == "内购审批")
                    strWhere += " and a.Type='0' ";
                else if (op == "赠送审批")
                    strWhere += " and a.Type='1' ";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetInternalApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, strWhere2);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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
        #endregion

        #region 专柜制作
        public ActionResult ShoppeManage()
        {
            string text = "专柜审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult ApplyShoppe()
        {
            tk_ShoppeInfo shoppe = new tk_ShoppeInfo();
            string unitID = GAccount.GetAccountInfo().UnitID;
            shoppe.SIID = SalesRetailMan.GetDHNO(unitID, "ZG", "Shope");

            return View(shoppe);
        }

        public ActionResult GetRightsFiled(string PID, string TaskType)
        {
            string RightsFiled = "";
            string CopyNum = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), GAccount.GetAccountInfo().UnitID.ToString(), TaskType);
            string TaskFiled = "";
            string ApprovalFiled = SalesRetailMan.GetTaskFiled(PID, "PID", "tk_Approval", "RelevanceID", "", "");
            if (TaskType == "Market")
                TaskFiled = SalesRetailMan.GetTaskFiled(GAccount.GetAccountInfo().UserName, "CreateUser", "MarketSales", "CreateUser", PID, "PID");
            else if (TaskType == "Promotion")
                TaskFiled = SalesRetailMan.GetTaskFiled(GAccount.GetAccountInfo().UserName, "CreateUser", "PromotionInfo", "CreateUser", PID, "PID");

            if (CopyNum == "0" || TaskFiled != "" || ApprovalFiled != "")
            {
                RightsFiled = "1";
            }
            else if (CopyNum != "0" && TaskFiled == "" && ApprovalFiled == "")
            {
                RightsFiled = "0";
            }

            return Json(RightsFiled, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveApplyShoppe(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_ShoppeInfo shope = new tk_ShoppeInfo();
                string TaskLength = fc["TaskLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["DAmount"]).Split(',');
                string[] arrPrice = (HttpContext.ApplicationInstance.Context.Request["Price"]).Split(',');
                string[] arrDiscount = (HttpContext.ApplicationInstance.Context.Request["Discount"]).Split(',');
                string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                GSqlSentence.SetTValue<tk_ShoppeInfo>(shope, HttpContext.ApplicationInstance.Context.Request);
                shope.UnitID = GAccount.GetAccountInfo().UnitID;
                shope.CreateUser = GAccount.GetAccountInfo().UserName;
                shope.CreateTime = DateTime.Now;
                shope.Validate = "v";
                shope.State = "0";

                tk_ShoppeInfoDetail shopeDetail = new tk_ShoppeInfoDetail();
                List<tk_ShoppeInfoDetail> shopelist = new List<tk_ShoppeInfoDetail>();
                for (int i = 0; i < arrOrderContent.Length; i++)
                {
                    shopeDetail = new tk_ShoppeInfoDetail();
                    shopeDetail.ProductID = arrProductID[i];
                    shopeDetail.OrderContent = arrOrderContent[i].ToString();
                    shopeDetail.SIID = shope.SIID;
                    shopeDetail.DID = shope.SIID + GFun.GetNum((i + 1), 3);
                    shopeDetail.Specifications = arrSpecsModels[i];
                    shopeDetail.GoodsType = arrGoodsType[i];
                    if (arrAmount[i] != "")
                    {
                        shopeDetail.Amount = int.Parse(arrAmount[i]);
                    }
                    if (arrPrice[i] != "")
                    {
                        shopeDetail.Price = Convert.ToDecimal(arrPrice[i]);
                    }
                    if (arrDiscount[i] != "")
                    {
                        shopeDetail.Discount = Convert.ToDecimal(arrDiscount[i]);
                    }
                    shopeDetail.Total = Convert.ToDecimal(arrTotal[i]); ;
                    shopeDetail.State = "0";
                    shopeDetail.Remark = arrRemark[i];
                    shopeDetail.CreateTime = DateTime.Now;
                    shopeDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    shopeDetail.Validate = "v";

                    shopelist.Add(shopeDetail);
                }
                string strErr = "";
                bool b = SalesRetailMan.SaveShopeInfo(shope, shopelist, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = shope.SIID;
                    salesLog.LogContent = "添加专柜申请";
                    salesLog.ProductType = "专柜管理";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult GetShoppeGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string malls = salesGrid.Malls;
                string applyReason = salesGrid.ApplyReason;
                string makeType = salesGrid.MakeType;
                string applyer = salesGrid.Applyer;
                string customer = salesGrid.Customer;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string strWhere = "";
                string unitId = GAccount.GetAccountInfo().UnitID;
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), unitId, "Shoppe");

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Shoppe' ";
                }

                if (malls != "" && malls != null)
                    strWhere += " and Malls='" + malls + "' ";
                if (applyReason != "" && applyReason != null)
                    strWhere += " and ApplyReason='" + applyReason + "' ";
                if (makeType != "" && makeType != null)
                    strWhere += " and MakeType='" + makeType + "' ";
                if (applyer != "" && applyer != null)
                    strWhere += " and Applyer='" + applyer + "' ";
                if (customer != "" && customer != null)
                    strWhere += " and Customer='" + customer + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and ApplyTime between '" + startDate + "' and '" + endDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetShoppeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, unitId, strWhere, filed);
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

        public FileResult ShoppeToExcel(tk_SalesGrid salesGrid)
        {
            string malls = salesGrid.Malls;
            string applyReason = salesGrid.ApplyReason;
            string makeType = salesGrid.MakeType;
            string applyer = salesGrid.Applyer;
            string customer = salesGrid.Customer;
            string startDate = salesGrid.StartDate;
            string endDate = salesGrid.EndDate;
            string strWhere = "";
            string unitId = GAccount.GetAccountInfo().UnitID;
            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), unitId, "Shoppe");

            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Shoppe' ";
            }

            if (malls != "" && malls != null)
                strWhere += " and Malls='" + malls + "' ";
            if (applyReason != "" && applyReason != null)
                strWhere += " and ApplyReason='" + applyReason + "' ";
            if (makeType != "" && makeType != null)
                strWhere += " and MakeType='" + makeType + "' ";
            if (applyer != "" && applyer != null)
                strWhere += " and Applyer='" + applyer + "' ";
            if (customer != "" && customer != null)
                strWhere += " and Customer='" + customer + "' ";
            if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                strWhere += " and ApplyTime between '" + startDate + "' and '" + endDate + "' ";


            DataTable dt = SalesRetailMan.GetShoppeToPrint(unitId, strWhere, filed);
            if (dt != null)
            {
                string strCols = "申请编号-6000,商场名称-12000,所属代理商-6000,申请事由-8000,制作类型-6000,使用年限-6000,费用预算-10000,申请人-6000,申请日期-6000,状态-6000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "专柜管理表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "专柜管理表.xls");
            }
            else
                return null;
        }

        public ActionResult DeleteShoppe()
        {
            string SIID = Request["SIID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeleteShoppeInfo(SIID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = SIID;
                salesLog.LogContent = "撤销专柜申请记录";
                salesLog.ProductType = "专柜管理";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }

        }

        public ActionResult GetMallInfo(string SIID)
        {
            DataTable dt = SalesRetailMan.GetMallInfoList(SIID);
            string jsonData = "";
            jsonData = GFun.Dt2Json("", dt);
            jsonData = jsonData.Substring(1);
            jsonData = jsonData.Substring(0, jsonData.Length - 1);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateShoppe(string SIID)
        {
            tk_ShoppeInfo shoppe = SalesRetailMan.GetShoppeInfo(SIID);
            return View(shoppe);
        }

        public ActionResult GetShoppeDetail(string SIID)
        {
            DataTable dt = SalesRetailMan.GetShoppeDetailInfo(SIID);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);

            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateApplyShoppe(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_ShoppeInfo shope = new tk_ShoppeInfo();
                string TaskLength = fc["TaskLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrGoodsType = (HttpContext.ApplicationInstance.Context.Request["GoodsType"]).Split(',');
                string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                string[] arrPrice = (HttpContext.ApplicationInstance.Context.Request["Price"]).Split(',');
                string[] arrDiscount = (HttpContext.ApplicationInstance.Context.Request["Discount"]).Split(',');
                string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');
                GSqlSentence.SetTValue<tk_ShoppeInfo>(shope, HttpContext.ApplicationInstance.Context.Request);
                shope.UnitID = GAccount.GetAccountInfo().UnitID;
                shope.CreateUser = GAccount.GetAccountInfo().UserName;
                shope.CreateTime = DateTime.Now;
                shope.Validate = "v";

                tk_ShoppeInfoDetail shopeDetail = new tk_ShoppeInfoDetail();
                List<tk_ShoppeInfoDetail> shopelist = new List<tk_ShoppeInfoDetail>();
                for (int i = 0; i < arrOrderContent.Length; i++)
                {
                    shopeDetail = new tk_ShoppeInfoDetail();
                    shopeDetail.ProductID = arrProductID[i];
                    shopeDetail.OrderContent = arrOrderContent[i].ToString();
                    shopeDetail.SIID = shope.SIID;
                    shopeDetail.DID = shope.SIID + GFun.GetNum((i + 1), 3);
                    shopeDetail.Specifications = arrSpecsModels[i];
                    shopeDetail.GoodsType = arrGoodsType[i];
                    shopeDetail.Amount = int.Parse(arrAmount[i]);
                    shopeDetail.Price = Convert.ToDecimal(arrPrice[i]);
                    shopeDetail.Discount = Convert.ToDecimal(arrDiscount[i]);
                    shopeDetail.Total = Convert.ToDecimal(arrTotal[i]); ;
                    shopeDetail.State = "0";
                    shopeDetail.Remark = arrRemark[i];
                    shopeDetail.CreateTime = DateTime.Now;
                    shopeDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    shopeDetail.Validate = "v";

                    shopelist.Add(shopeDetail);
                }
                string strErr = "";
                bool b = SalesRetailMan.UpdateShoppeInfo(shope, shopelist, GAccount.GetAccountInfo().UserName, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = shope.SIID;
                    salesLog.LogContent = "修改专柜申请";
                    salesLog.ProductType = "专柜管理";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult ApprovalShoppe()
        {
            string text = "专柜审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApprovalShoppe(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string SIID = sales.SIID;
                string Customer = sales.Customer;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;

                string strWhere = "";

                if (SIID != "" && SIID != null)
                    strWhere += " and SIID='" + SIID + "' ";
                if (Customer != "" && Customer != null)
                    strWhere += " and a.Customer='" + Customer + "' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and a.ApplyTime between '" + StartDate + "' and '" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetShoppeApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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
        #endregion

        #region 样机管理
        public ActionResult PropertyManage()
        {
            string text = "样机审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult ConditionGrid()
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
            {
                where += " and a.RelevanceID = '" + PID + "'";
            }
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = COM_ApprovalMan.getNewConditionGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApplyProperty()
        {
            tk_Property tkProto = new tk_Property();
            string unitID = GAccount.GetAccountInfo().UnitID;
            tkProto.PAID = SalesRetailMan.GetDHNO(unitID, "YJ", "ProtoType");

            return View(tkProto);
        }

        public ActionResult UpdateProperty(string PAID)
        {
            tk_Property tkPro = SalesRetailMan.GetPrototypeInfo(PAID);
            return View(tkPro);
        }

        public ActionResult GetProtoTypeDetail(string PAID, string OperateType)
        {
            DataTable dt = SalesRetailMan.GetProtoDetail(PAID, OperateType);
            //string strJson = GFun.Dt2Json("", dt);
            //strJson = strJson.Substring(1);
            //strJson = strJson.Substring(0, strJson.Length - 1);

            //return Json(strJson, JsonRequestBehavior.AllowGet);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        public ActionResult UpdatePrototypeInfo(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_Property tkProto = new tk_Property();
                string TaskLength = fc["TaskLength"].ToString();
                string RevokeLength = fc["RevokeLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrPtype = (HttpContext.ApplicationInstance.Context.Request["Ptype"]).Split(',');
                string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                string[] arrUnitPrice = (HttpContext.ApplicationInstance.Context.Request["UnitPrice"]).Split(',');
                string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                string[] arrBusinessType = (HttpContext.ApplicationInstance.Context.Request["BusinessType"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["Remark"]).Split(',');
                GSqlSentence.SetTValue<tk_Property>(tkProto, HttpContext.ApplicationInstance.Context.Request);

                string[] arrtxtProductID = (HttpContext.ApplicationInstance.Context.Request["txtProductID"]).Split(',');
                string[] arrtxtOrderContent = (HttpContext.ApplicationInstance.Context.Request["txtOrderContent"]).Split(',');
                string[] arrtxtSpecsModels = (HttpContext.ApplicationInstance.Context.Request["txtSpecsModels"]).Split(',');
                string[] arrtxtPtype = (HttpContext.ApplicationInstance.Context.Request["txtPtype"]).Split(',');
                string[] arrtxtAmount = (HttpContext.ApplicationInstance.Context.Request["txtAmount"]).Split(',');
                string[] arrtxtUnitPrice = (HttpContext.ApplicationInstance.Context.Request["txtUnitPrice"]).Split(',');
                string[] arrtxtTotal = (HttpContext.ApplicationInstance.Context.Request["txtTotal"]).Split(',');
                string[] arrtxtBusinessType = (HttpContext.ApplicationInstance.Context.Request["txtBusinessType"]).Split(',');
                string[] arrtxtRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');

                tkProto.UnitID = GAccount.GetAccountInfo().UnitID;
                tkProto.CreateUser = GAccount.GetAccountInfo().UserName;
                tkProto.CreateTime = DateTime.Now;
                tkProto.Validate = "v";

                tk_PropertyDetail ProtoDetail = new tk_PropertyDetail();
                List<tk_PropertyDetail> Protolist = new List<tk_PropertyDetail>();
                for (int i = 0; i < arrOrderContent.Length; i++)
                {
                    ProtoDetail = new tk_PropertyDetail();
                    ProtoDetail.OrderContent = arrOrderContent[i].ToString();
                    ProtoDetail.PAID = tkProto.PAID;
                    ProtoDetail.DID = tkProto.PAID + GFun.GetNum((i + 1), 3);
                    ProtoDetail.ProductID = arrProductID[i];
                    ProtoDetail.Specifications = arrSpecsModels[i];
                    ProtoDetail.Ptype = arrPtype[i];
                    ProtoDetail.Amount = Convert.ToInt32(arrAmount[i]);
                    if (arrUnitPrice[i] != "")
                    {
                        ProtoDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                    }
                    ProtoDetail.Total = Convert.ToDecimal(arrTotal[i]);
                    ProtoDetail.BusinessType = arrBusinessType[i];
                    ProtoDetail.State = "0";
                    ProtoDetail.OperateType = "0";
                    ProtoDetail.Remark = arrRemark[i];
                    ProtoDetail.CreateTime = DateTime.Now;
                    ProtoDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    ProtoDetail.Validate = "v";

                    Protolist.Add(ProtoDetail);
                }

                tk_PropertyDetail ProtoDetail1 = new tk_PropertyDetail();
                List<tk_PropertyDetail> Protolist1 = new List<tk_PropertyDetail>();
                for (int i = 0; i < arrtxtProductID.Length; i++)
                {
                    ProtoDetail1 = new tk_PropertyDetail();
                    ProtoDetail1.OrderContent = arrtxtOrderContent[i].ToString();
                    ProtoDetail1.PAID = tkProto.PAID;
                    ProtoDetail1.DID = tkProto.PAID + GFun.GetNum((i + 1), 3);
                    ProtoDetail1.ProductID = arrtxtProductID[i];
                    ProtoDetail1.Specifications = arrtxtSpecsModels[i];
                    ProtoDetail1.Ptype = arrtxtPtype[i];
                    ProtoDetail1.Amount = Convert.ToInt32(arrtxtAmount[i]);
                    ProtoDetail1.Total = Convert.ToDecimal(arrtxtTotal[i]);
                    if (arrtxtUnitPrice[i] != "")
                    {
                        ProtoDetail1.UnitPrice = Convert.ToDecimal(arrtxtUnitPrice[i]);
                    }
                    ProtoDetail1.BusinessType = arrtxtBusinessType[i];
                    ProtoDetail1.State = "0";
                    ProtoDetail1.OperateType = "1";
                    ProtoDetail1.Remark = arrtxtRemark[i];
                    ProtoDetail1.CreateTime = DateTime.Now;
                    ProtoDetail1.CreateUser = GAccount.GetAccountInfo().UserName;
                    ProtoDetail1.Validate = "v";

                    Protolist1.Add(ProtoDetail1);
                }

                string strErr = "";
                bool b = SalesRetailMan.UpdateProtoType(tkProto, Protolist, Protolist1, GAccount.GetAccountInfo().UserName, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = ProtoDetail.PAID;
                    salesLog.LogContent = "修改样机申请";
                    salesLog.ProductType = "样机管理";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        public ActionResult ApprovalProperty()
        {
            string text = "样机审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult GetPrototypeGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string applyMan = salesGrid.ApplyMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string strWhere = "";
                string strWhere2 = "";
                string unitId = GAccount.GetAccountInfo().UnitID;
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), unitId, "Prototype");

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Prototype' ";
                }
                //else
                //{
                //    strWhere += " and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}

                if (salesProduct != "" && salesProduct != null)
                    strWhere2 += " and b.OrderContent like'%" + salesProduct + "%' ";
                if (specsModels != "" && specsModels != null)
                    strWhere2 += " and b.Specifications like'%" + specsModels + "%' ";
                if (applyMan != "" && applyMan != null)
                    strWhere += " and a.Applyer='" + applyMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and a.ApplyDate between '" + startDate + "' and '" + endDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetPrototypeList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, unitId, filed, strWhere2);
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

        public FileResult PrototypeToExcel(tk_SalesGrid salesGrid)
        {
            string salesProduct = salesGrid.SalesProduct;
            string specsModels = salesGrid.SpecsModels;
            string applyMan = salesGrid.ApplyMan;
            string startDate = salesGrid.StartDate;
            string endDate = salesGrid.EndDate;
            string strWhere = "";
            string strWhere2 = "";
            string unitId = GAccount.GetAccountInfo().UnitID;
            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), unitId, "Prototype");

            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Prototype' ";
            }
            //else
            //{
            //    strWhere += " and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            //}

            if (salesProduct != "" && salesProduct != null)
                strWhere2 += " and b.OrderContent like'%" + salesProduct + "%' ";
            if (specsModels != "" && specsModels != null)
                strWhere2 += " and b.Specifications like'%" + specsModels + "%' ";
            if (applyMan != "" && applyMan != null)
                strWhere += " and a.Applyer='" + applyMan + "' ";
            if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                strWhere += " and a.ApplyDate between '" + startDate + "' and '" + endDate + "' ";


            DataTable dt = SalesRetailMan.GetPrototypeToPrint(unitId, strWhere, filed, strWhere2);
            if (dt != null)
            {
                string strCols = "申请编号-6000,上样产品-12000,撤样产品-6000,申请人-8000,申请日期-6000,商场名称-6000,活动说明-10000,状态-6000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "样机管理表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "样机管理表.xls");
            }
            else
                return null;
        }

        public ActionResult SavePrototype(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                tk_Property tkProto = new tk_Property();
                string TaskLength = fc["TaskLength"].ToString();
                string RevokeLength = fc["RevokeLength"].ToString();
                string[] arrProductID = (HttpContext.ApplicationInstance.Context.Request["ProductID"]).Split(',');
                string[] arrOrderContent = (HttpContext.ApplicationInstance.Context.Request["OrderContent"]).Split(',');
                string[] arrSpecsModels = (HttpContext.ApplicationInstance.Context.Request["SpecsModels"]).Split(',');
                string[] arrPtype = (HttpContext.ApplicationInstance.Context.Request["Ptype"]).Split(',');
                string[] arrAmount = (HttpContext.ApplicationInstance.Context.Request["Amount"]).Split(',');
                string[] arrUnitPrice = Request["UnitPrice"].Split(',');
                string[] arrTotal = (HttpContext.ApplicationInstance.Context.Request["Total"]).Split(',');
                string[] arrBusinessType = (HttpContext.ApplicationInstance.Context.Request["BusinessType"]).Split(',');
                string[] arrRemark = (HttpContext.ApplicationInstance.Context.Request["Remark"]).Split(',');
                GSqlSentence.SetTValue<tk_Property>(tkProto, HttpContext.ApplicationInstance.Context.Request);

                string[] arrtxtProductID = (HttpContext.ApplicationInstance.Context.Request["txtProductID"]).Split(',');
                string[] arrtxtOrderContent = (HttpContext.ApplicationInstance.Context.Request["txtOrderContent"]).Split(',');
                string[] arrtxtSpecsModels = (HttpContext.ApplicationInstance.Context.Request["txtSpecsModels"]).Split(',');
                string[] arrtxtPtype = (HttpContext.ApplicationInstance.Context.Request["txtPtype"]).Split(',');
                string[] arrtxtAmount = (HttpContext.ApplicationInstance.Context.Request["txtAmount"]).Split(',');
                string[] arrtxtUnitPrice = Request["txtUnitPrice"].Split(',');
                string[] arrtxtTotal = (HttpContext.ApplicationInstance.Context.Request["txtTotal"]).Split(',');
                string[] arrtxtBusinessType = (HttpContext.ApplicationInstance.Context.Request["txtBusinessType"]).Split(',');
                string[] arrtxtRemark = (HttpContext.ApplicationInstance.Context.Request["txtRemark"]).Split(',');

                tkProto.UnitID = GAccount.GetAccountInfo().UnitID;
                tkProto.CreateUser = GAccount.GetAccountInfo().UserName;
                tkProto.CreateTime = DateTime.Now;
                tkProto.Validate = "v";
                tkProto.State = "0";

                tk_PropertyDetail ProtoDetail = new tk_PropertyDetail();
                List<tk_PropertyDetail> Protolist = new List<tk_PropertyDetail>();
                for (int i = 0; i < arrProductID.Length; i++)
                {
                    ProtoDetail = new tk_PropertyDetail();
                    ProtoDetail.PAID = tkProto.PAID;
                    ProtoDetail.DID = tkProto.PAID + GFun.GetNum((i + 1), 3);
                    ProtoDetail.ProductID = arrProductID[i];
                    ProtoDetail.OrderContent = arrOrderContent[i];
                    ProtoDetail.Specifications = arrSpecsModels[i];
                    ProtoDetail.Ptype = arrPtype[i];
                    if (arrAmount[i] != "")
                    {
                        ProtoDetail.Amount = Convert.ToInt32(arrAmount[i]);
                    }
                    if (arrUnitPrice[i] != "")
                    {
                        ProtoDetail.UnitPrice = Convert.ToDecimal(arrUnitPrice[i]);
                    }
                    if (arrTotal[i] != "")
                    {
                        ProtoDetail.Total = Convert.ToDecimal(arrTotal[i]);
                    }
                    ProtoDetail.BusinessType = arrBusinessType[i];
                    ProtoDetail.State = "0";
                    ProtoDetail.IState = "0";
                    ProtoDetail.OperateType = "0";
                    ProtoDetail.Remark = arrRemark[i];
                    ProtoDetail.CreateTime = DateTime.Now;
                    ProtoDetail.CreateUser = GAccount.GetAccountInfo().UserName;
                    ProtoDetail.Validate = "v";

                    Protolist.Add(ProtoDetail);
                }

                tk_PropertyDetail ProtoDetail1 = new tk_PropertyDetail();
                List<tk_PropertyDetail> Protolist1 = new List<tk_PropertyDetail>();

                for (int i = 0; i < arrtxtProductID.Length; i++)
                {
                    ProtoDetail1 = new tk_PropertyDetail();
                    ProtoDetail1.PAID = tkProto.PAID;
                    ProtoDetail1.DID = tkProto.PAID + GFun.GetNum((i + 1), 3);
                    ProtoDetail1.ProductID = arrtxtProductID[i];
                    ProtoDetail1.OrderContent = arrtxtOrderContent[i];
                    ProtoDetail1.Specifications = arrtxtSpecsModels[i];
                    ProtoDetail1.Ptype = arrtxtPtype[i];
                    if (arrtxtAmount[i] != "")
                    {
                        ProtoDetail1.Amount = Convert.ToInt32(arrtxtAmount[i]);
                    }
                    if (arrtxtUnitPrice[i] != "")
                    {
                        ProtoDetail1.UnitPrice = Convert.ToDecimal(arrtxtUnitPrice[i]);
                    }
                    if (arrtxtTotal[i] != "")
                    {
                        ProtoDetail1.Total = Convert.ToDecimal(arrtxtTotal[i]);
                    }
                    ProtoDetail1.BusinessType = arrtxtBusinessType[i];
                    ProtoDetail1.State = "0";
                    ProtoDetail1.IState = "0";
                    ProtoDetail1.OperateType = "1";
                    ProtoDetail1.Remark = arrtxtRemark[i];
                    ProtoDetail1.CreateTime = DateTime.Now;
                    ProtoDetail1.CreateUser = GAccount.GetAccountInfo().UserName;
                    ProtoDetail1.Validate = "v";

                    Protolist1.Add(ProtoDetail1);
                }

                string strErr = "";
                bool b = SalesRetailMan.SavePrototype(tkProto, Protolist, Protolist1, ref strErr);
                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = ProtoDetail.PAID;
                    salesLog.LogContent = "添加样机申请";
                    salesLog.ProductType = "样机管理";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true, Msg = strErr });
                }
            }
            else
            {
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }

        public ActionResult DeletePrototype()
        {
            string PAID = Request["PAID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeleteProtoInfo(PAID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = PAID;
                salesLog.LogContent = "撤销样机申请";
                salesLog.ProductType = "样机管理";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }
        }

        public ActionResult GetProtoDetailGrid()
        {
            string strCurPage;
            string strRowNum;

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            string PAID = Request["PAID"].ToString();
            string Op = Request["Op"].ToString();

            UIDataTable udtTask = SalesRetailMan.GetProtoDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, PAID, Op);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchApprovalPrototype(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string SIID = sales.PAID;
                string Customer = sales.Customer;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;

                string strWhere = "";

                if (SIID != "" && SIID != null)
                    strWhere += " and PAID='" + SIID + "' ";
                if (Customer != "" && Customer != null)
                    strWhere += " and a.Malls='" + Customer + "' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and a.ApplyDate between '" + StartDate + "' and '" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetProtoApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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

        public ActionResult PrototypeInfomation()
        {
            return View();
        }

        public ActionResult GetFiveMall()
        {
            string strErr = "";
            string strJsonSubUnits = SalesRetailMan.GetSubMalls(ref strErr);

            return Json(strJsonSubUnits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFiveMallsGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string operateType = Request["OperateType"].ToString();
                string specsModels = salesGrid.SpecsModels;
                string applyMan = salesGrid.ApplyMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string strWhere = "";

                if (operateType != "" && operateType != null)
                    strWhere += " and a.OperateType='" + operateType + "' ";
                if (specsModels != "" && specsModels != null)
                    strWhere += " and a.Specifications like'%" + specsModels + "%' ";
                if (applyMan != "" && applyMan != null)
                    strWhere += " and b.Applyer='" + applyMan + "' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and b.ApplyDate between '" + startDate + "' and '" + endDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string MallID = Request["MallID"].ToString();
                string strGrade = Request["strGrade"].ToString();

                UIDataTable udtTask = SalesRetailMan.GetFiveMallList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, MallID, strGrade);
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

        #endregion

        #region 促销管理
        public ActionResult PromotionManage()
        {
            string text = "促销审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult ApprovalPromotion()
        {
            string text = "促销审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApprovalPromotion(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ActionTitle = sales.ActionTitle;
                string Applyer = sales.Applyer;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string strWhere = "";

                if (ActionTitle != "" && ActionTitle != null)
                    strWhere += " and a.ActionTitle like'%" + ActionTitle + "%' ";
                if (Applyer != "" && Applyer != null)
                    strWhere += " and a.Applyer='" + Applyer + "' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and a.StartTime='" + StartDate + "' and a.EndTime='" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetPromotionApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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

        public ActionResult ApplyPromotion()
        {
            string unitID = GAccount.GetAccountInfo().UnitID;
            string PID = SalesRetailMan.GetDHNO(unitID, "CX", "Promotion");
            tk_Promotion promotion = new tk_Promotion();
            promotion.PID = PID;

            return View(promotion);
        }

        public ActionResult UpdatePromotion(string PID)
        {
            tk_Promotion promotion = new tk_Promotion();
            promotion = SalesRetailMan.GetPromotionInfo(PID);

            return View(promotion);
        }

        public ActionResult GetPromotionGrid(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ActionTitle = sales.ActionTitle;
                string Applyer = sales.Applyer;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string strWhere = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Promotion");

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Promotion' ";
                }
                //else
                //{
                //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}
                if (ActionTitle != "" && ActionTitle != null)
                    strWhere += " and ActionTitle like'%" + ActionTitle + "%' ";
                if (Applyer != "" && Applyer != null)
                    strWhere += " and Applyer='" + Applyer + "' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and StartTime='" + StartDate + "' and EndTime='" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetPromotionList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, UnitId, strWhere, filed);
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

        public FileResult PromotionToExcel(tk_SalesGrid sales)
        {
            string ActionTitle = sales.ActionTitle;
            string Applyer = sales.Applyer;
            string StartDate = sales.StartDate;
            string EndDate = sales.EndDate;
            string strWhere = "";
            string UnitId = GAccount.GetAccountInfo().UnitID;
            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Promotion");

            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Promotion' ";
            }
            //else
            //{
            //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            //}
            if (ActionTitle != "" && ActionTitle != null)
                strWhere += " and ActionTitle like'%" + ActionTitle + "%' ";
            if (Applyer != "" && Applyer != null)
                strWhere += " and Applyer='" + Applyer + "' ";
            if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                strWhere += " and StartTime='" + StartDate + "' and EndTime='" + EndDate + "' ";


            DataTable dt = SalesRetailMan.GetPromotionToPrint(UnitId, strWhere, filed);
            if (dt != null)
            {
                string strCols = "申请编号-6000,活动主题-12000,活动对象-6000,促销活动位置-8000,宣传方式-6000,活动目的-6000,申请人-10000,活动负责人-6000,备注-6000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "促销管理表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "促销管理表.xls");
            }
            else
                return null;
        }

        public ActionResult DeletePromotion()
        {
            string PID = Request["PID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeletePromotionInfo(PID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = PID;
                salesLog.LogContent = "撤销促销活动申请";
                salesLog.ProductType = "促销管理";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }
        }

        public ActionResult SavePromotionApply(tk_Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                string a_strErr = "";
                promotion.UnitID = GAccount.GetAccountInfo().UnitID;
                promotion.State = "0";
                promotion.CreateTime = DateTime.Now;
                promotion.CreateUser = GAccount.GetAccountInfo().UserName;
                promotion.Validate = "v";
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                bool b = SalesRetailMan.AddPromotionInfo(promotion, files, ref a_strErr);

                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = promotion.PID;
                    salesLog.LogContent = "创建促销活动申请";
                    salesLog.ProductType = "促销管理";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    ViewData["msg"] = "保存成功";
                    return View("ApplyPromotion", promotion);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("ApplyPromotion", promotion);
                }
            }
            else
            {
                ViewData["msg"] = "数据验证不通过...";
                return View("ApplyPromotion", promotion);
            }
        }

        public ActionResult UpdatePromotionApply(tk_Promotion promotion)
        {
            string a_strErr = "";
            promotion.UnitID = GAccount.GetAccountInfo().UnitID;
            promotion.CreateTime = DateTime.Now;
            promotion.CreateUser = GAccount.GetAccountInfo().UserName;
            promotion.Validate = "v";

            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            bool b = SalesRetailMan.UpdatePromotionInfo(promotion, files, GAccount.GetAccountInfo().UserName, ref a_strErr);

            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = promotion.PID;
                salesLog.LogContent = "修改促销活动申请";
                salesLog.ProductType = "促销管理";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                ViewData["msg"] = "保存成功";
                return View("UpdatePromotion", promotion);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UpdatePromotion", promotion);
            }
        }

        public ActionResult GetPromotionFile()
        {
            string PID = Request["PID"].ToString();
            string SalesType = Request["SalesType"].ToString();
            DataTable dt = SalesRetailMan.GetRetailFile(PID, "", SalesType);
            string FileName = "";
            string FileInfo = "";
            if (dt.Rows.Count > 0 && dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FileName += dt.Rows[i]["FileName"].ToString() + ",";
                    FileInfo += dt.Rows[i]["FileInfo"].ToString() + ",";
                }
            }

            return Json(new { success = "true", FileName = FileName, FileInfo = FileInfo });
        }

        public void DownLoadFile(string PID, string FileName, string FileType)
        {
            byte[] bContent = null;
            DataTable dtInfo = SalesRetailMan.GetRetailFile(PID, FileName, FileType);
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                bContent = (byte[])dtInfo.Rows[0]["FileInfo"];

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0]["FileInfo"]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }



        #endregion

        #region 市场销售
        public ActionResult MarketSalesManage()
        {
            string text = "市场销售审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult GetMarketGrid(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ApplyType = sales.ApplyType;
                string ApplyTitle = sales.ApplyTitle;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string strWhere = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Market");

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Market' ";
                }
                //else
                //{
                //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}
                if (ApplyType != "" && ApplyType != null)
                    strWhere += " and ApplyType like'%" + ApplyType + "%' ";
                if (ApplyTitle != "" && ApplyTitle != null)
                    strWhere += " and ApplyTitle like'%" + ApplyTitle + "%' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and ApplyTime between '" + StartDate + "' and '" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetMarketList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, UnitId, strWhere, filed);
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

        public ActionResult MarketToExcel(tk_SalesGrid sales)
        {
            string ApplyType = sales.ApplyType;
            string ApplyTitle = sales.ApplyTitle;
            string StartDate = sales.StartDate;
            string EndDate = sales.EndDate;
            string strWhere = "";
            string UnitId = GAccount.GetAccountInfo().UnitID;
            string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Market");

            if (filed == "0")
            {
                strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Market' ";
            }
            //else
            //{
            //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            //}
            if (ApplyType != "" && ApplyType != null)
                strWhere += " and ApplyType='" + ApplyType + "' ";
            if (ApplyTitle != "" && ApplyTitle != null)
                strWhere += " and ApplyTitle='" + ApplyTitle + "' ";
            if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                strWhere += " and ApplyTime between '" + StartDate + "' and '" + EndDate + "' ";


            DataTable dt = SalesRetailMan.GetMarketToPrint(UnitId, strWhere, filed);
            if (dt != null)
            {
                string strCols = "申请编号-6000,申请类型-12000,申请名称-6000,申请时间-8000,销售负责人-6000,申请内容-6000,任务状态-10000";

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "市场销售管理表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "市场销售管理表.xls");
            }
            else
                return null;
        }

        public ActionResult ApplyMarket()
        {
            string unitID = GAccount.GetAccountInfo().UnitID;
            string PID = SalesRetailMan.GetDHNO(unitID, "SA", "Market");
            tk_MarketSales market = new tk_MarketSales();
            market.PID = PID;

            return View(market);
        }

        public ActionResult SaveMarketApply(tk_MarketSales market)
        {
            if (ModelState.IsValid)
            {
                string a_strErr = "";
                market.UnitID = GAccount.GetAccountInfo().UnitID;
                market.State = "0";
                market.CreateTime = DateTime.Now;
                market.CreateUser = GAccount.GetAccountInfo().UserName;
                market.Validate = "v";
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                bool b = SalesRetailMan.AddMarketInfo(market, files, ref a_strErr);

                if (b)
                {
                    Sales_SalesLog salesLog = new Sales_SalesLog();
                    salesLog.LogTime = DateTime.Now.ToString();
                    salesLog.PID = market.PID;
                    salesLog.LogContent = "创建促销活动申请";
                    salesLog.ProductType = "市场销售";
                    salesLog.SalesType = "Retail";
                    salesLog.Actor = GAccount.GetAccountInfo().UserName;
                    salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                    SalesRetailMan.SaveLog(salesLog);

                    ViewData["msg"] = "保存成功";
                    return View("ApplyMarket", market);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("ApplyMarket", market);
                }
            }
            else
            {
                ViewData["msg"] = "数据验证不通过...";
                return View("ApplyMarket", market);
            }
        }

        public ActionResult UpdateMarketInfo(tk_MarketSales market)
        {
            string a_strErr = "";
            market.UnitID = GAccount.GetAccountInfo().UnitID;
            market.CreateTime = DateTime.Now;
            market.CreateUser = GAccount.GetAccountInfo().UserName;
            market.Validate = "v";

            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            bool b = SalesRetailMan.UpdateMarketSalesInfo(market, files, GAccount.GetAccountInfo().UserName, ref a_strErr);

            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = market.PID;
                salesLog.LogContent = "修改市场销售申请";
                salesLog.ProductType = "市场销售";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                ViewData["msg"] = "保存成功";
                return View("UpdateMarketApply", market);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UpdateMarketApply", market);
            }
        }

        public ActionResult DeleteMarketInfo()
        {
            string PID = Request["PID"].ToString();
            string strErr = "";
            string loginUser = GAccount.GetAccountInfo().UserName;
            bool b = SalesRetailMan.DeleteMarket(PID, loginUser, ref strErr);
            if (b)
            {
                Sales_SalesLog salesLog = new Sales_SalesLog();
                salesLog.LogTime = DateTime.Now.ToString();
                salesLog.PID = PID;
                salesLog.LogContent = "撤销促销活动申请";
                salesLog.ProductType = "市场销售";
                salesLog.SalesType = "Retail";
                salesLog.Actor = GAccount.GetAccountInfo().UserName;
                salesLog.Unit = GAccount.GetAccountInfo().UnitName;
                SalesRetailMan.SaveLog(salesLog);

                return Json(new { success = "true", Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出错-" + strErr });
            }
        }

        public ActionResult UpdateMarketApply(string PID)
        {
            tk_MarketSales market = new tk_MarketSales();
            market = SalesRetailMan.GetMarketInfo(PID);

            return View(market);
        }

        public ActionResult ApprovalMarket()
        {
            string text = "市场销售审批";

            ViewData["webkey"] = text;
            string fold = COM_ApprovalMan.getNewwebkey(text);
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];
            return View();
        }

        public ActionResult SearchApprovalMarket(tk_SalesGrid sales)
        {
            if (ModelState.IsValid)
            {
                string ApplyType = sales.ApplyType;
                string ApplyTitle = sales.ApplyTitle;
                string StartDate = sales.StartDate;
                string EndDate = sales.EndDate;
                string strWhere = "";

                if (ApplyType != "" && ApplyType != null)
                    strWhere += " and a.ApplyType like'%" + ApplyType + "%' ";
                if (ApplyTitle != "" && ApplyTitle != null)
                    strWhere += " and a.ApplyTitle like'%" + ApplyTitle + "%' ";
                if ((StartDate != "" && StartDate != null) && (EndDate != "" && EndDate != null))
                    strWhere += " and a.ApplyTime between '" + StartDate + "' and '" + EndDate + "' ";

                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                string strjson = "";
                UIDataTable udtTask = SalesRetailMan.GetMarketApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                strjson = GFun.Dt2Json("", udtTask.DtData);
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
        #endregion

        #region 和售后库存关联
        //库存
        public ActionResult RetailLibraryTubeManage()
        {
            return View();
        }
        public ActionResult GetSalesRetailLibraryTubeGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string OrderID = salesGrid.OrderID;
                // string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                //string isHK = salesGrid.IsHK;
                string state = salesGrid.State;
                string OrderContactor = salesGrid.OrderContactor;
                string OrderTel = salesGrid.OrderTel;
                string ISCollection = salesGrid.ISCollection;
                string strWhere = "";
                string strWhere2 = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                string Validate = "v";
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Retail");

                if (state != "" && state != "-2" && state != null)
                {
                    strWhere += " and a.State='" + state + "' ";
                }
                else if (state == "-2")
                    Validate = "i";

                // strWhere += " and a.Validate='" + Validate + "' ";

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Retail' ";
                }
                //else
                //{
                //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}

                if (OrderID != "" && OrderID != null)
                    strWhere += " and t.OrderID like '%" + OrderID + "%'";
                //  if (specsModels != "" && specsModels != null)
                //   strWhere2 += " and b.SpecsModels like '%" + specsModels + "%' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and t.ProvidManager like '%" + salesMan + "%' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and t.ContractDate between '" + startDate + "' and '" + endDate + "' ";
                //if (!string.IsNullOrEmpty(isHK))
                //    strWhere += " and a.IsHK='" + isHK + "' ";
                //if (OrderContactor != "" && OrderContactor != null)
                //{
                //    strWhere += " and a.OrderContactor like '%" + OrderContactor + "%' ";
                //}
                if (OrderTel != "" && OrderTel != null)
                {
                    strWhere += " and t.OrderTel like'%" + OrderTel + "%'";
                }
                if (ISCollection != "" && ISCollection != null)
                {

                    strWhere += " and t.ISCollection =" + ISCollection;
                }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetSalesRetailLibraryTubeGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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





        //售后
        public ActionResult RetailAfterSaleManage()
        {
            return View();
        }

        public ActionResult GetSalesRetailAfterSaleGrid(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string OrderID = salesGrid.OrderID;
                // string specsModels = salesGrid.SpecsModels;
                string salesMan = salesGrid.SalesMan;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                //string isHK = salesGrid.IsHK;
                string state = salesGrid.State;
                string OrderContactor = salesGrid.OrderContactor;
                string OrderTel = salesGrid.OrderTel;
                string ISCollection = salesGrid.ISCollection;
                string strWhere = "";
                string strWhere2 = "";
                string UnitId = GAccount.GetAccountInfo().UnitID;
                string Validate = "v";
                string filed = SalesRetailMan.GetCopyNum(GAccount.GetAccountInfo().UserID.ToString(), UnitId, "Retail");

                if (state != "" && state != "-2" && state != null)
                {
                    strWhere += " and a.State='" + state + "' ";
                }
                else if (state == "-2")
                    Validate = "i";

                // strWhere += " and a.Validate='" + Validate + "' ";

                if (filed == "0")
                {
                    strWhere += " and c.UserID='" + GAccount.GetAccountInfo().UserID + "' and TaskType='Retail' ";
                }
                //else
                //{
                //    strWhere += " and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                //}

                if (OrderID != "" && OrderID != null)
                    strWhere += " and t.OrderID like '%" + OrderID + "%'";
                //  if (specsModels != "" && specsModels != null)
                //   strWhere2 += " and b.SpecsModels like '%" + specsModels + "%' ";
                if (salesMan != "" && salesMan != null)
                    strWhere += " and t.ProvidManager like '%" + salesMan + "%' ";
                if ((startDate != "" && startDate != null) && (endDate != "" && endDate != null))
                    strWhere += " and t.ContractDate between '" + startDate + "' and '" + endDate + "' ";
                //if (!string.IsNullOrEmpty(isHK))
                //    strWhere += " and a.IsHK='" + isHK + "' ";
                //if (OrderContactor != "" && OrderContactor != null)
                //{
                //    strWhere += " and a.OrderContactor like '%" + OrderContactor + "%' ";
                //}
                if (OrderTel != "" && OrderTel != null)
                {
                    strWhere += " and t.OrderTel like'%" + OrderTel + "%'";
                }
                if (ISCollection != "" && ISCollection != null)
                {

                    strWhere += " and t.ISCollection =" + ISCollection;
                }
                string strCurPage;
                string strRowNum;

                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";

                UIDataTable udtTask = SalesRetailMan.GetSalesRetailAfterSaleGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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
        #endregion

        #region 统计分析
        public ActionResult SalesWeekStatics()
        {
            return View();
        }

        public ActionResult SearchStatics(tk_SalesGrid salesGrid)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                string salesMan = salesGrid.SalesMan;
                string unitId = salesGrid.UnitId;
                if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    where += " and b.ContractDate between '" + startDate + "' and '" + endDate + "' ";
                if (!String.IsNullOrEmpty(salesMan))
                    where += " and b.ProvidManager='" + salesMan + "' ";
                if (!String.IsNullOrEmpty(unitId))
                    where += " and b.UnitID='" + unitId + "' ";

                DataSet ds = SalesRetailMan.GetWeekStatics(where);
                StringBuilder sb = new StringBuilder();
                string html1 = "";
                string html2 = "";
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //html1 += "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;height:200px;overflow-y:auto;margin-top:5px;line-height:25px;\">";
                    //html1 += "<tr><td style=\"width:10%;\">序号</td><td style=\"width:19%\">销售人员</td><td style=\"width:19%\">所属部门</td><td style=\"width:19%\">定价单价（元）</td><td style=\"width:19%\">成交价单价（元）</td><td style=\"width:19%\">成交总价（元）</td></tr>";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        html1 += "<tr style=\"height:25px;\"><td style=\"width:10%;background-color:white\">" + (i + 1) + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["ProvidManager"].ToString() + "</td>"
                            + "<td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["UnitName"].ToString() + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["UnitPrice"].ToString() + "</td>"
                            + "<td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["DUnitPrice"].ToString() + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["DTotalPrice"].ToString() + "</td></tr>";
                    }
                    html2 = "汇总说明:开始日期：" + startDate + "结束日期：" + endDate + "，定价单价：" + ds.Tables[1].Rows[0]["UnitPrice"] + "元，"
                        + "成交价单价：" + ds.Tables[1].Rows[0]["DUnitPrice"] + "元，成交总价：" + ds.Tables[1].Rows[0]["DTotalPrice"] + "元";

                    //html1 += "</table>";
                }

                sb.Append(html1);

                return Json(new { success = "true", strSb = sb.ToString(), strSign = html2 });

            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        public ActionResult PrintWeekStatics(string StartDate, string EndDate, string SalesMan, string UnitId)
        {
            string where = "";
            if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                where += " and b.ContractDate between '" + StartDate + "' and '" + EndDate + "' ";
            if (!String.IsNullOrEmpty(SalesMan))
                where += " and b.ProvidManager='" + SalesMan + "' ";
            if (!String.IsNullOrEmpty(UnitId))
                where += " and b.UnitID='" + UnitId + "' ";

            DataSet ds = SalesRetailMan.GetWeekStatics(where);
            StringBuilder sb = new StringBuilder();
            string html1 = "";
            string html2 = "";
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                html2 = "<div style='text-align:center; font-size:35px; font-weight:bold;'>家用产品周销售统计</div>";
                html2 += "<div id='Sign' style='width: 97%; margin-left: 10px; margin-top: 10px;'>汇总说明:开始日期：" + StartDate + "结束日期：" + EndDate + "，定价单价：" + ds.Tables[1].Rows[0]["UnitPrice"] + "元，"
                   + "成交价单价：" + ds.Tables[1].Rows[0]["DUnitPrice"] + "元，成交总价：" + ds.Tables[1].Rows[0]["DTotalPrice"] + "元</div>";
                html1 += "<div><table id=\"T\" class=\"tabInfo\" style=\"margin-left:10px;height:200px;overflow-y:auto;margin-top:5px;line-height:25px;\">";
                html1 += "<tr><th style=\"width:10%;\" class=\"th\">序号</th><th style=\"width:19%\" class=\"th\">销售人员</th><th style=\"width:19%\" class=\"th\">所属部门</th><th style=\"width:19%\" class=\"th\">定价单价（元）</th><th style=\"width:19%\" class=\"th\">成交价单价（元）</th><th style=\"width:19%\" class=\"th\">成交总价（元）</th></tr>";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    html1 += "<tr style=\"height:25px;\"><td style=\"width:10%;background-color:white\">" + (i + 1) + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["ProvidManager"].ToString() + "</td>"
                        + "<td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["UnitName"].ToString() + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["UnitPrice"].ToString() + "</td>"
                        + "<td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["DUnitPrice"].ToString() + "</td><td style=\"width:19%;background-color:white\">" + ds.Tables[0].Rows[i]["DTotalPrice"].ToString() + "</td></tr>";
                }
                html1 += "</table></div>";
            }

            sb.Append(html2 + html1);
            Response.Write(sb.ToString());
            return View();
        }

        /// <summary>
        /// 内购产品统计
        /// </summary>
        /// <returns></returns>
        public ActionResult StaticsInternal()
        {
            return View();
        }

        public ActionResult GetInternalDetailGrid(tk_SalesGrid salesGrid)
        {

            if (ModelState.IsValid)
            {
                string strWhere = " and b.Type='0' and b.Validate='v' ";
                string salesProduct = salesGrid.SalesProduct;
                string specsModels = salesGrid.SpecsModels;
                string startDate = salesGrid.StartDate;
                string endDate = salesGrid.EndDate;
                if (!String.IsNullOrEmpty(salesProduct))
                    strWhere += " and a.OrderContent like'%" + salesProduct + "%' ";
                if (!String.IsNullOrEmpty(specsModels))
                    strWhere += " and a.Specifications like'%" + specsModels + "%' ";
                if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    strWhere += " and b.OrderDate between '" + startDate + "' and '" + endDate + "' ";


                UIDataTable udtTask = SalesRetailMan.GetDetailGrid(strWhere);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + 0 + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        /// <summary>
        /// 修改回款状态
        /// </summary>
        /// <returns></returns>
        public ActionResult AlterInternalHK()
        {
            string IOID = Request["IOID"].ToString();
            string DID = Request["DID"].ToString();
            string a_strErr = "";

            bool b = SalesRetailMan.AlterInternalDetail(IOID, DID, GAccount.GetAccountInfo().UserName, ref a_strErr);
            if (b)
            {
                return Json(new { success = true, Msg = "保存成功" });
            }
            else
            {
                return Json(new { success = false, Msg = "保存出错-" + a_strErr });
            }
        }

        public ActionResult StaticsExcelInternal(tk_SalesGrid salesGrid)
        {
            string strWhere = " and b.Type='0' and b.Validate='v' ";
            string salesProduct = salesGrid.SalesProduct;
            string specsModels = salesGrid.SpecsModels;
            string startDate = salesGrid.StartDate;
            string endDate = salesGrid.EndDate;
            if (!String.IsNullOrEmpty(salesProduct))
                strWhere += " and a.OrderContent like'%" + salesProduct + "%' ";
            if (!String.IsNullOrEmpty(specsModels))
                strWhere += " and a.Specifications like'%" + specsModels + "%' ";
            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                strWhere += " and b.OrderDate between '" + startDate + "' and '" + endDate + "' ";

            UIDataTable udt = SalesRetailMan.GetDetailGrid(strWhere);
            if (udt.DtData != null && udt.DtData.Rows.Count > 0)
            {
                string Title = "";
                string strCols = "";
                strCols = "产品名称-6000,产品类型-6000,规格型号-6000,数量-3000,零售价-6000,折扣-6000,总价-5000,付款方式-6000,备注-10000,是否回款-6000";
                Title = "内购产品统计表";

                udt.DtData.Columns.Remove("IOID");
                udt.DtData.Columns.Remove("DID");
                for (int i = 0; i < udt.DtData.Rows.Count; i++)
                {
                    string IsHK = "";
                    string PayWay = udt.DtData.Rows[i]["PayWay"].ToString();
                    if (PayWay == "安装后付款")
                    {
                        if (udt.DtData.Rows[i]["IsHK"].ToString() == "y")
                            IsHK = "已回款";
                        else
                            IsHK = "未回款";
                    }
                    else
                        IsHK = "不涉及";

                    udt.DtData.Rows[i]["IsHK"] = IsHK;


                }

                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(udt.DtData, Title, strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "" + Title + ".xls");
            }
            else
                return null;

        }

        public ActionResult PrintInterDetail(string SalesProduct, string SpecsModels, string StartDate, string EndDate)
        {
            string strWhere = "  and b.Type='0' and b.Validate='v' ";
            if (!String.IsNullOrEmpty(SalesProduct))
                strWhere += " and a.OrderContent like'%" + SalesProduct + "%' ";
            if (!String.IsNullOrEmpty(SpecsModels))
                strWhere += " and a.Specifications like'%" + SpecsModels + "%' ";
            if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                strWhere += " and b.OrderDate between '" + StartDate + "' and '" + EndDate + "' ";

            UIDataTable udt = SalesRetailMan.GetDetailGrid(strWhere);
            string html = "";
            string html1 = "";

            html = "<div><table class='tabInfo' style='margin-top: 10px; height: 500px; overflow-y: auto;'>";
            html += "<tr style='height:60px;'><td colspan='11' style='text-align:center; font-size:25px; font-weight:bold;'>内购产品统计</td></tr>";

            if (udt.DtData != null && udt.DtData.Rows.Count > 0)
            {
                html1 = "<tr><td style='width:5%; text-align:center;height:18px;'>序号</td><td style='width:10%; text-align:center;height:18px;'>产品名称</td>"
                      + "<td style='width:10%; text-align:center;height:18px;'>产品类型</td><td style='width:10%; text-align:center;height:18px;'>规格型号</td><td style='width:5%; text-align:center;height:18px;'>数量</td>"
                      + "<td style='width:10%; text-align:center;height:18px;'>零售价</td><td style='width:10%; text-align:center;height:18px;'>折扣</td><td style='width:10%; text-align:center;height:18px;'>总价</td>"
                      + "<td style='width:10%; text-align:center;height:18px;'>付款方式</td><td style='width:10%; text-align:center;height:18px;'>是否回款</td><td style='width:10%; text-align:center;height:18px;'>备注</td></tr>";

                for (int i = 0; i < udt.DtData.Rows.Count; i++)
                {
                    string IsHK = "";
                    string PayWay = udt.DtData.Rows[i]["PayWay"].ToString();
                    if (PayWay == "安装后付款")
                    {
                        if (udt.DtData.Rows[i]["IsHK"].ToString() == "y")
                            IsHK = "已回款";
                        else
                            IsHK = "未回款";
                    }
                    else
                        IsHK = "不涉及";

                    html1 += "<tr><td style='width:5%; text-align:center;'>" + (i + 1) + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["OrderContent"].ToString() + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["GoodsType"].ToString() + "</td>"
                        + "<td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["Specifications"].ToString() + "</td><td style='width:5%; text-align:center;'>" + udt.DtData.Rows[i]["Amount"].ToString() + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["UnitPrice"].ToString() + "</td>"
                        + "<td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["Discounts"].ToString() + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["Total"].ToString() + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["PayWay"].ToString() + "</td>"
                        + "<td style='width:10%; text-align:center;'>" + IsHK + "</td><td style='width:10%; text-align:center;'>" + udt.DtData.Rows[i]["Remark"].ToString() + "</td>";
                    html1 += "</tr>";
                }
            }
            html += html1;
            html += "</table></div>";

            Response.Write(html);
            return View();
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印样机出撤申请
        /// </summary>
        /// <param name="PAID"></param>
        /// <returns></returns>
        public ActionResult PrintPrototype(string PAID)
        {
            StringBuilder sbu = new StringBuilder();
            tk_Property proto = SalesRetailMan.GetPrototypeInfo(PAID);
            DataTable dtSample = SalesRetailMan.GetProtoDetail(PAID, "0");
            DataTable dtRevoke = SalesRetailMan.GetProtoDetail(PAID, "1");
            string html = "";
            string htmlSam = "";
            string htmlRev = "";
            string applyDate = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
           // StringBuilder sb5 = new StringBuilder();

            if (proto.ApplyDate.Contains("1900") == false && proto.ApplyDate != "")
            {
                applyDate = proto.ApplyDate;
            }

            sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><table class='tabInfo' style='margin-top: 10px;overflow-y: auto;'>");
            sb.Append("<tr style='height:80px;text-align:center;'><td colspan='4' style='text-align:center; font-size:35px; font-weight:bold;'>产品出（撤）样机申请表</td></tr>");
            sb.Append( "<tr><td style='width:25%;'>申请人：</td><td style='width:25%;'>" + proto.Applyer + "</td><td style='width:25%;'>申请日期：</td><td style='width:25%;'>" + applyDate + "</td></tr>");
           sb.Append("<tr><td>商场名称：</td><td colspan='3'>" + proto.Malls + "</td></tr><tr><td>活动说明：</td><td colspan='3'>" + proto.ExPlain + "</td></tr></table>");
           sb2.Append("<table class='tabInfo'><tr><td>部门经理审批：</td><td colspan='3'></td></tr><tr><td>总（副总）经理审批</td><td colspan='3'></td></tr></table></div>");


            if (dtSample != null && dtSample.Rows.Count > 0)
            {
                if (dtSample.Rows.Count <= 6)
                {
                    sb1.Append("<table class='tabInfo'><tr align='center'><th style='width: 5%;' class='th'>序号</th><th style='width: 13%;' class='th'>产品编码</th>"
                         + "<th style='width: 13%;' class='th'>产品大类</th><th style='width: 13%;' class='th'>上样型号</th>"
                         + "<th style='width: 13%;' class='th'>数量</th><th style='15%' class='th'>金额</th><th style='width: 13%;' class='th'>业务类型</th><th style='width: 17%;' class='th'>备注</th></tr>"
                         + "<tbody id='DetailInfo'>");
                    for (int i = 0; i < dtSample.Rows.Count; i++)
                    {
                        sb1.Append("<tr><td>" + (i + 1) + "</td><td>" + dtSample.Rows[i]["ProductID"].ToString() + "</td><td>" + dtSample.Rows[i]["Ptype"].ToString() + "</td><td>" + dtSample.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dtSample.Rows[i]["Amount"].ToString() + "</td><td>" + dtSample.Rows[i]["Total"].ToString() + "</td><td>" + dtSample.Rows[i]["BusinessType"].ToString() + "</td><td>" + dtSample.Rows[i]["Remark"].ToString() + "</td></tr>");
                    }
                    sb1.Append("<tr><td colspan='2'>上样合计：</td><td colspan='6'>" + proto.SampleAmount + "</td></tr></tbody></table>");
                    htmlSam += sb.ToString() + sb1.ToString() + sb2.ToString();
                }
                else {


                    int count = dtSample.Rows.Count % 6;
                    if (count > 0)
                        count = dtSample.Rows.Count / 6 + 1;
                    else
                        count = dtSample.Rows.Count / 6;
                    for (int i = 0; i < count; i++)
                    {
                        sb1 = new StringBuilder();
                        int a = 6 * i;
                        int length = 6 * (i + 1);
                        //if (length > dt.Rows.Count)

                        if (length > dtSample.Rows.Count)
                            length = 6 * i + dtSample.Rows.Count % 6;
                        sb1.Append("<table class='tabInfo'><tr align='center'><th style='width: 5%;' class='th'>序号</th><th style='width: 13%;' class='th'>产品编码</th>"
                           + "<th style='width: 13%;' class='th'>产品大类</th><th style='width: 13%;' class='th'>上样型号</th>"
                           + "<th style='width: 13%;' class='th'>数量</th><th style='15%' class='th'>金额</th><th style='width: 13%;' class='th'>业务类型</th><th style='width: 17%;' class='th'>备注</th></tr>"
                           + "<tbody id='DetailInfo'>");
                        for (int j = a; j <length; j++)
                        {
                            sb1.Append("<tr><td>" + (i + 1) + "</td><td>" + dtSample.Rows[i]["ProductID"].ToString() + "</td><td>" + dtSample.Rows[i]["Ptype"].ToString() + "</td><td>" + dtSample.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dtSample.Rows[i]["Amount"].ToString() + "</td><td>" + dtSample.Rows[i]["Total"].ToString() + "</td><td>" + dtSample.Rows[i]["BusinessType"].ToString() + "</td><td>" + dtSample.Rows[i]["Remark"].ToString() + "</td></tr>");
                        }
                        sb1.Append("<tr><td colspan='2'>上样合计：</td><td colspan='6'>" + proto.SampleAmount + "</td></tr></tbody></table>");
                        htmlSam += sb.ToString() + sb1.ToString() + sb2.ToString();
                    }
                       
                }
            }
          //  html += htmlSam;

            if (dtRevoke != null && dtRevoke.Rows.Count > 0)
            {
                if (dtRevoke.Rows.Count <= 6)
                {
                    sb3.Append("<table class='tabInfo'><tr align='center'><th style='width: 5%;' class='th'>序号</th><th style='width: 13%;' class='th'>产品编码</th>"
                        + "<th style='width: 13%;' class='th'>产品大类</th><th style='width: 13%;' class='th'>撤样型号</th>"
                        + "<th style='width: 13%;' class='th'>数量</th><th style='15%' class='th'>金额</th><th style='width: 13%;' class='th'>业务类型</th><th style='width: 17%;' class='th'>备注</th></tr>"
                        + "<tbody id='RevokeInfo'>");
                    for (int i = 0; i < dtRevoke.Rows.Count; i++)
                    {
                        sb3.Append("<tr><td>" + (i + 1) + "</td><td>" + dtRevoke.Rows[i]["ProductID"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Ptype"].ToString() + "</td><td>" + dtRevoke.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Amount"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Total"].ToString() + "</td><td>" + dtRevoke.Rows[i]["BusinessType"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Remark"].ToString() + "</td></tr>");
                    }
                    sb3.Append("<tr><td colspan='2'>撤样合计：</td><td colspan='6'>" + proto.RevokeAmount + "</td></tr></tbody></table>");
                    htmlRev += sb.ToString() + sb3.ToString() + sb2.ToString();
                }
                else {

                    int count = dtRevoke.Rows.Count % 6;
                    if (count > 0)
                        count = dtRevoke.Rows.Count / 6 + 1;
                    else
                        count = dtRevoke.Rows.Count / 6;
                    for (int i = 0; i < count; i++)
                    {
                        sb3 = new StringBuilder();
                        int a = 6 * i;
                        int length = 6 * (i + 1);
                        //if (length > dt.Rows.Count)

                        if (length > dtRevoke.Rows.Count)
                            length = 6 * i + dtRevoke.Rows.Count % 6;
                        sb3.Append("<table class='tabInfo'><tr align='center'><th style='width: 5%;' class='th'>序号</th><th style='width: 13%;' class='th'>产品编码</th>"
                       + "<th style='width: 13%;' class='th'>产品大类</th><th style='width: 13%;' class='th'>撤样型号</th>"
                       + "<th style='width: 13%;' class='th'>数量</th><th style='15%' class='th'>金额</th><th style='width: 13%;' class='th'>业务类型</th><th style='width: 17%;' class='th'>备注</th></tr>"
                       + "<tbody id='RevokeInfo'>");
                        for (int j =a; j < length; j++)
                        {
                            sb3.Append("<tr><td>" + (i + 1) + "</td><td>" + dtRevoke.Rows[i]["ProductID"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Ptype"].ToString() + "</td><td>" + dtRevoke.Rows[i]["SpecsModels"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Amount"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Total"].ToString() + "</td><td>" + dtRevoke.Rows[i]["BusinessType"].ToString() + "</td><td>" + dtRevoke.Rows[i]["Remark"].ToString() + "</td></tr>");
                        }
                        sb3.Append("<tr><td colspan='2'>撤样合计：</td><td colspan='6'>" + proto.RevokeAmount + "</td></tr></tbody></table>");
                        htmlRev += sb.ToString() + sb3.ToString() + sb2.ToString();
                    }
                
                }
            }
       html=htmlSam + htmlRev;
         
           // sbu.Append(html);

            Response.Write(html);
            return View();
        }

        /// <summary>
        /// 打印员工内购（赠送）单
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintInternal(string IOID, string OP)
        {
            string type = "";
            if (OP == "NG")
                type = "0";
            else if (OP == "ZS")
                type = "1";

            StringBuilder sbu = new StringBuilder();
            DataTable dtDetail = SalesRetailMan.GetPrintInternalDetail(IOID, OP);
            string html = "";
            string html1 = "";

            tk_InternalOrder order = new tk_InternalOrder();
            order = SalesRetailMan.GetInternalOrder(IOID, type);
            string orderDate = order.OrderDate.ToString("yyyy/MM/dd");
            if (orderDate != "")
            {
                string[] a = orderDate.Split('/');
                //orderDate = orderDate.Substring(0, 4) + "年" + orderDate.Substring(5, 2) + "月" + orderDate.Substring(8, 2) + "日";
                orderDate = a[0].ToString() + "年" + a[1].ToString() + "月" + a[2].ToString() + "日";
            }

            string wareHouse = "□公司出货 □博达二级库";
            if (order.Warehouse == "公司出货")
                wareHouse = "☑公司出货 □博达二级库";
            else if (order.Warehouse == "博达二级库")
                wareHouse = "□公司出货 ☑博达二级库";

            string UpperMoney = "";
            if (order.GoodsTotal.ToString() != "")
                UpperMoney = SalesRetailMan.MoneyToUpper(order.GoodsTotal.ToString());

            if (OP == "NG")            //内购
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><table class='tabInfo' style='margin-top: 10px; overflow-y: auto;'><tr style='height:60px;text-align:center;'><td colspan='4' style='text-align:center; font-size:25px; font-weight:bold;'>内购单</td></tr><tr><td style='width:25%;'>出库仓库：</td><td style='width:25%;'>" + wareHouse + "</td><td style='width:25%;'>日期：</td><td style='width:25%;'>" + orderDate + "</td></tr></table>");
                sb2.Append("<table class='tabInfo'><tr><td style='width:25%;'>申请人：</td><td style='width:25%;'>" + order.Applyer + "</td><td style='width:25%;'>联系电话：</td><td style='width:25%;'>" + order.ApplyTel + "</td></tr><tr><td style='width:25%;'>内购公司商品使用人：</td><td style='width:25%;'>" + order.GoodsUser + "</td><td style='width:25%;'>联系电话：</td><td style='width:25%;'>" + order.UserTel + "</td></tr><tr><td style='width:25%;'>主管负责人：</td><td style='width:25%;'>" + order.Steering + "</td><td style='width:25%;'>总经理：</td><td style='width:25%;'></td></tr></table></div>");
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {

                    if (dtDetail.Rows.Count <= 6)
                    {
                       sb1.Append("<table class='tabInfo'><tr align='center'><th style='width:15%;' class='th'>名称</th>"
                              + "<th style='width: 15%;' class='th'>类别</th><th style='width: 14%;' class='th'>型号</th><th style='width: 14%;' class='th'>数量</th>"
                              + "<th style='14%' class='th'>零售价</th><th style='width: 14%;' class='th'>折扣</th><th style='width: 14%;' class='th'>收货金额</th></tr>"
                              + "<tbody id='DetailInfo'>");

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                         sb1.Append("<tr><td>" + dtDetail.Rows[i]["OrderContent"].ToString() + "</td><td>" + dtDetail.Rows[i]["GoodsType"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["Specifications"].ToString() + "</td><td>" + dtDetail.Rows[i]["Amount"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["UnitPrice"].ToString() + "</td><td>" + dtDetail.Rows[i]["Discounts"].ToString() + "</td><td>" + dtDetail.Rows[i]["Total"].ToString() + "</td></tr>");
                        }
                        sb1.Append("<tr><td>合计：</td><td colspan='6'>" + order.GoodsTotal + "</td></tr><tr><td>合计（大写）：</td><td colspan='6'>" + UpperMoney + "</td></tr></tbody></table>");
                        html = sb.ToString() + sb1.ToString() + sb2.ToString();
                    }
                    else
                    {

                        int count = dtDetail.Rows.Count % 6;
                        if (count > 0)
                            count = dtDetail.Rows.Count / 6 + 1;
                        else
                            count = dtDetail.Rows.Count / 6;
                        for (int i = 0; i < count; i++)
                        {
                            sb1 = new StringBuilder();
                            int a = 6 * i;
                            int length = 6 * (i + 1);
                            //if (length > dt.Rows.Count)

                            if (length > dtDetail.Rows.Count)
                                length = 6 * i + dtDetail.Rows.Count % 6;
                           sb1.Append("<table class='tabInfo'><tr align='center'><th style='width:15%;' class='th'>名称</th>"
                              + "<th style='width: 15%;' class='th'>类别</th><th style='width: 14%;' class='th'>型号</th><th style='width: 14%;' class='th'>数量</th>"
                              + "<th style='14%' class='th'>零售价</th><th style='width: 14%;' class='th'>折扣</th><th style='width: 14%;' class='th'>收货金额</th></tr>"
                              + "<tbody id='DetailInfo'>");//<th style='width:20%;border: 1px solid #000;' class='th'>备注</th><th style='width: 10%;border: 1px solid #000;' class='th'>供应商</th>

                            for (int j = a; j < length; j++)
                            {
                               sb1.Append("<tr><td>" + dtDetail.Rows[i]["OrderContent"].ToString() + "</td><td>" + dtDetail.Rows[i]["GoodsType"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["Specifications"].ToString() + "</td><td>" + dtDetail.Rows[i]["Amount"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["UnitPrice"].ToString() + "</td><td>" + dtDetail.Rows[i]["Discounts"].ToString() + "</td><td>" + dtDetail.Rows[i]["Total"].ToString() + "</td></tr>");

                            }
                          sb1.Append("<tr><td>合计：</td><td colspan='6'>" + order.GoodsTotal + "</td></tr><tr><td>合计（大写）：</td><td colspan='6'>" + UpperMoney + "</td></tr></tbody></table>");
                          html += sb.ToString() + sb1.ToString() + sb2.ToString();
                        }

                    }

                }
             //   html += html1;
             

               // html += "</table></div>";
            }
            else if (OP == "ZS")       //赠送
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><table class='tabInfo' style='margin-top: 10px; text-align:center;'>");
                sb.Append("<tr style='height:60px;'><td colspan='6' style='text-align:center; font-size:25px; font-weight:bold;'>赠送单</td></tr>");
                sb.Append("<tr><td style='width:16%;'>出库仓库：</td><td colspan='2'>" + wareHouse + "</td><td style='width:16%;'>日期：</td><td colspan='2' style='text-align:center;border-top:1px solid black;'>" + orderDate + "</td></tr></table>");
                sb2.Append("<table class='tabInfo'><tr><td>申请人：</td><td colspan='2'>" + order.Applyer + "</td><td>联系电话：</td><td colspan='2'>" + order.ApplyTel + "</td></tr><tr><td>赠送公司商品使用人：</td><td colspan='2'>" + order.GoodsUser + "</td><td>联系电话：</td><td colspan='2'>" + order.UserTel + "</td></tr><tr><td>客户地址：</td><td colspan='5'>" + order.Address + "</td></tr><tr><td style='width:16%;'>提货收款人：</td><td style='width:17%;'>" + order.Recipiments + "</td><td style='width:16%;'>主管负责人：</td><td style='width:17%;'>" + order.Steering + "</td><td style='width:16%;'>总经理：</td><td style='width:18%;'></td></tr></table></div>");
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {

                    if (dtDetail.Rows.Count <= 6)
                    {
                        sb1.Append("<table class='tabInfo'><tr align='center'><th style='width:15%;' class='th'>名称</th>"
                              + "<th style='width: 15%;' class='th'>类别</th><th style='width: 14%;' class='th'>型号</th><th style='width: 14%;' class='th'>数量</th>"
                              + "<th style='14%' class='th'>零售价</th><th style='width: 14%;' class='th'>折扣</th><th style='width: 14%;' class='th'>收货金额</th></tr>"
                              + "<tbody id='DetailInfo'>");

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            sb1.Append("<tr><td>" + dtDetail.Rows[i]["OrderContent"].ToString() + "</td><td>" + dtDetail.Rows[i]["GoodsType"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["Specifications"].ToString() + "</td><td>" + dtDetail.Rows[i]["Amount"].ToString() + "</td>"
                                + "<td>" + dtDetail.Rows[i]["UnitPrice"].ToString() + "</td><td>" + dtDetail.Rows[i]["Discounts"].ToString() + "</td><td>" + dtDetail.Rows[i]["Total"].ToString() + "</td></tr>");
                        }
                        sb1.Append("<tr><td>合计：</td><td colspan='6'>" + order.GoodsTotal + "</td></tr><tr><td>合计（大写）：</td><td colspan='6'>" + UpperMoney + "</td></tr></tbody></table>");

                        html = sb.ToString() + sb1.ToString() + sb2.ToString();
                    }
                    else
                    {

                        int count = dtDetail.Rows.Count % 6;
                        if (count > 0)
                            count = dtDetail.Rows.Count / 6 + 1;
                        else
                            count = dtDetail.Rows.Count / 6;
                        for (int i = 0; i < count; i++)
                        {
                            sb1 = new StringBuilder();
                            int a = 6 * i;
                            int length = 6 * (i + 1);
                            //if (length > dt.Rows.Count)

                            if (length > dtDetail.Rows.Count)
                                length = 6 * i + dtDetail.Rows.Count % 6;
                            sb1.Append("<table class='tabInfo'><tr align='center'><th style='width:15%;' class='th'>名称</th>"
                               + "<th style='width: 15%;' class='th'>类别</th><th style='width: 14%;' class='th'>型号</th><th style='width: 14%;' class='th'>数量</th>"
                               + "<th style='14%' class='th'>零售价</th><th style='width: 14%;' class='th'>折扣</th><th style='width: 14%;' class='th'>收货金额</th></tr>"
                               + "<tbody id='DetailInfo'>");//<th style='width:20%;border: 1px solid #000;' class='th'>备注</th><th style='width: 10%;border: 1px solid #000;' class='th'>供应商</th>

                            for (int j = a; j < length; j++)
                            {
                                sb1.Append("<tr><td>" + dtDetail.Rows[i]["OrderContent"].ToString() + "</td><td>" + dtDetail.Rows[i]["GoodsType"].ToString() + "</td>"
                              + "<td>" + dtDetail.Rows[i]["Specifications"].ToString() + "</td><td>" + dtDetail.Rows[i]["Amount"].ToString() + "</td>"
                              + "<td>" + dtDetail.Rows[i]["UnitPrice"].ToString() + "</td><td>" + dtDetail.Rows[i]["Discounts"].ToString() + "</td><td>" + dtDetail.Rows[i]["Total"].ToString() + "</td></tr>");

                            }
                            sb1.Append("<tr><td>合计：</td><td colspan='6'>" + order.GoodsTotal + "</td></tr><tr><td>合计（大写）：</td><td colspan='6'>" + UpperMoney + "</td></tr></tbody></table>");
                            html += sb.ToString() + sb1.ToString() + sb2.ToString();
                        }
                       
                        //html += html1;
                        //html += "<tr><td>申请人：</td><td colspan='2'>" + order.Applyer + "</td><td>联系电话：</td><td colspan='2'>" + order.ApplyTel + "</td></tr>";
                        //html += "<tr><td>赠送公司商品使用人：</td><td colspan='2'>" + order.GoodsUser + "</td><td>联系电话：</td><td colspan='2'>" + order.UserTel + "</td></tr>";
                        //html += "<tr><td>客户地址：</td><td colspan='5'>" + order.Address + "</td></tr>";
                        //html += "<tr><td style='width:16%;'>提货收款人：</td><td style='width:17%;'>" + order.Recipiments + "</td><td style='width:16%;'>主管负责人：</td><td style='width:17%;'>" + order.Steering + "</td><td style='width:16%;'>总经理：</td><td style='width:18%;'></td></tr>";

                        //html += "</table></div>";
                    }
                }
                //html += html1;
                //html += "<tr><td>申请人：</td><td colspan='2'>" + order.Applyer + "</td><td>联系电话：</td><td colspan='2'>" + order.ApplyTel + "</td></tr>";
                //html += "<tr><td>赠送公司商品使用人：</td><td colspan='2'>" + order.GoodsUser + "</td><td>联系电话：</td><td colspan='2'>" + order.UserTel + "</td></tr>";
                //html += "<tr><td>客户地址：</td><td colspan='5'>" + order.Address + "</td></tr>";
                //html += "<tr><td style='width:16%;'>提货收款人：</td><td style='width:17%;'>" + order.Recipiments + "</td><td style='width:16%;'>主管负责人：</td><td style='width:17%;'>" + order.Steering + "</td><td style='width:16%;'>总经理：</td><td style='width:18%;'></td></tr>";

                //html += "</table></div>";
            }
            //  sbu.Append(html);

            Response.Write(html);
            return View();
        }

        /// <summary>
        /// 打印专柜制作申请
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintShoppe(string SIID)
        {
            tk_ShoppeInfo shoppe = SalesRetailMan.GetPrintShoppeInfo(SIID);
            List<SelectListItem> list = SalesRetailMan.GetSelectListitem("ApplyReason");
            string applyInfo = "申请事由：";
            //+ "制作类型：新进[   ]     重装[   ]";
            for (int i = 1; i < list.Count; i++)
            {
                if (shoppe.ApplyReason == list[i].Value)
                    applyInfo += list[i].Text + "[ √ ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                else
                    applyInfo += list[i].Text + "[    ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            List<SelectListItem> list1 = SalesRetailMan.GetSelectListitem("MakeType");
            applyInfo += "</br>制作类型：";
            for (int j = 1; j < list1.Count; j++)
            {
                if (shoppe.MakeType == list1[j].Value)
                    applyInfo += list1[j].Text + "[ √ ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                else
                    applyInfo += list1[j].Text + "[    ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }



            string html = "";
            html = "<div><table class='tabInfo' style='margin-top: 10px; height: 400px; overflow-y: auto;'>";
            html += "<tr style='height:50px;'><td colspan='6' style='text-align:center; width:16%; border-bottom-style:none;font-size:35px; font-weight:bold;'>北京市燕山工业燃气设备有限公司</td></tr>";
            html += "<tr style='height:30px;'><td colspan='6' style='text-align:center; width:16%; border-top-style:none; font-size:25px; font-weight:bold;'>专柜制作申请表</td></tr>";
            html += "<tr><td style='width:16%;'>申请日期：</td><td colspan='3'>" + shoppe.ApplyTime + "</td><td>申请人：</td><td>" + shoppe.Applyer + "</td></tr>";

            html += "<tr><td style='width:16%; text-align:center;' rowspan='9'>所</br>在</br>商</br>场</br>基</br>本</br>信</br>息</td><td>所属代理商：</td><td colspan='4'>" + shoppe.Malls + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='3'>商场联系人:" + shoppe.MallType + "</td><td>联系电话：</td><td>" + shoppe.Phone + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='5'>商场地址：" + shoppe.Address + "</td></tr>"
                + "<tr><td style='width:16%;' rowspan='2' colspan='2'>竞品一名称：" + shoppe.ProductsOneName + "</td><td style='width:16%;'>专柜尺寸：</td><td colspan='2'>" + shoppe.ShoppeSize + "</td></tr>"
                + "<tr><td style='width:16%;'>出样数量：</td><td colspan='2'>" + shoppe.SampleOneNum + "</td></tr>"
                + "<tr><td style='width:16%;' rowspan='2' colspan='2'>竞品二名称：" + shoppe.ProductsTwoName + "</td><td style='width:16%;'>专柜尺寸：</td><td colspan='2'>" + shoppe.ShoppeTwoSize + "</td></tr>"
                + "<tr><td style='width:16%;'>出样数量：</td><td colspan='2'>" + shoppe.SampleNum + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='5'>我司专柜位置（简述）：" + shoppe.ShoppePosition + "</td></tr>"
                + "<tr><td style='width:16%' colspan='5'>进驻后预计月均销量及金额（台\\万元）：" + shoppe.MonthSalesNum + "\\" + shoppe.SalesAmount + "</td></tr>";

            html += "<tr style='height:40px;'><td style='width:16%;' colspan='6'>" + applyInfo + "</td></tr>";
            html += "<tr><td style='width:16%;' colspan='6'>灶具出样型号：" + shoppe.Cookers + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='6'>烟机出样型号：" + shoppe.Turbine + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='6'>燃气热水器出样型号：" + shoppe.GasHeater + "</td></tr>"
                + "<tr><td style='width:16%;' colspan='6'>壁挂炉出样型号：" + shoppe.GasBoiler + "</td></tr>";

            string useYear = shoppe.UseYear;
            string endYear = shoppe.EndYear;
            if (useYear != "")
                useYear = useYear.Substring(0, 4) + "年" + useYear.Substring(5, 2) + "月" + useYear.Substring(8, 2) + "日";
            if (endYear != "")
                endYear = endYear.Substring(0, 4) + "年" + endYear.Substring(5, 2) + "月" + endYear.Substring(8, 2) + "日";
            html += "<tr><td style='width:16%;' colspan='6'>专柜使用年限：" + useYear + "——" + endYear + "</td></tr>";
            html += "<tr><td style='width:16%;' colspan='6'>费用预算（万元）：" + shoppe.Budget + "</td></tr>";
            html += "<tr style='height:40px;'><td style='width:16%;' colspan='6'>" + shoppe.Explain + "</td></tr>";
            html += "<tr><td style='width:16%;' colspan='3'>申请人签名：</td><td colspan='3'>代理商（盖章）：</td></tr>";
            html += "<tr><td style='width:16%;' colspan='3'>市场策划审核：</td><td colspan='3'>总经理审批：</td></tr>";
            html += "</table></div>";

            html += "<div>注：*此表适用于各类专柜、灯箱、店招的制作申请；</br>"
                + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*必须按公司C1标准执行；</br>"
                + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*所有推广资源使用，必须先提交申请，审批通过后方可执行，否则视为违规操作，费用由经销商（广告公司）自行承担</div>";

            Response.Write(html);
            return View();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintPromotion()
        {

            tk_Promotion Promotion = SalesRetailMan.GetPromotionInfo(Request["PID"].ToString());
            StringBuilder sb = new StringBuilder();
            string html = "";
            sb.Append("<div id='PrintArea' style='page-break-after: always;height :1000px;'><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>北京市燕山工业燃气设备有限公司</div><div id='' style='text-align:center;font-size:18px; margin-top:20px;'>打印促销活动申请</div><table class='tabInfo' cellpadding='0' ><tr ><td>申请日期:</td><td>" + Promotion.ApplyTime + "</td><td >申请人:</td><td>" + Promotion.Applyer + "</td></tr><tr><td colspan='2'>活动主题:</td><td colspan='2'>" + Promotion.ActionTitle + "</td></tr><tr><td colspan='2'>活动执行时间</td><td colspan='2'>" + Promotion.StartTime + "至" + Promotion.EndTime + "</td></tr><tr><td colspan='2'>活动对象</td><td colspan='2'>" + Promotion.PurPose + "</td></tr><tr><td colspan='2'>促销活动位置：</td><td colspan='2'>" + Promotion.Position + "</td></tr><tr><td colspan='2'>活动宣传方式</td><td colspan='2'>" + Promotion.ActionStyle + "</td></tr><tr><td colspan='2'>活动目的:</td><td colspan='2'>" + Promotion.Remark + "</td></tr><tr><td colspan='2'>部门经理审批</td><td colspan='2'></td></tr><tr><td colspan='2'>总经理审批</td><td colspan='2'></td></tr><tr><td colspan='2'>活动方案上传</td><td colspan='2'>" + Promotion.ActionProject + "</td></tr></table>");
            html = sb.ToString();

            Response.Write(html);
            return View();
        }

        #endregion
    }
}
