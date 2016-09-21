using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECOCITY_BGOI;

namespace TECOCITY.Controllers
{
    [AuthorizationAttribute]
    public class FlowMeterManageController : Controller
    {
        //
        // GET: /FlowMeterManage/

        public ActionResult Index()
        {
            return View();
        }

        #region // 登记卡管理 完成

        #region ----[主页 完成]

        public ActionResult CardManage()
        {
            return View();
        }

        // 主页-查询加载登记卡列表 
        public ActionResult LoadCardList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";
            string ModelType = "";
            string RepairID = Request["RepairID"].ToString();
            string CustomerName = Request["CustomerName"].ToString();
            string CustomerAddr = Request["CustomerAddr"].ToString();
            string MeterID = Request["MeterID"].ToString();
            string MeterName = Request["MeterName"].ToString();
            string Model = Request["Model"].ToString();
            string SS_Date = Request["SS_Date"].ToString();
            string ES_Date = Request["ES_Date"].ToString();
            string State = Request["State"].ToString();
            if (Request["CardType"] != null)
                ModelType = Request["CardType"].ToString();

            string strWhere = "";
            if (RepairID != "")
                strWhere += " and a.RepairID like '%" + RepairID + "%'";
            if (CustomerName != "")
                strWhere += " and a.CustomerName like '%" + CustomerName + "%'";
            if (CustomerAddr != "")
                strWhere += " and a.CustomerAddr like '%" + CustomerAddr + "%'";
            if (MeterID != "")
                strWhere += " and a.MeterID like '%" + MeterID + "%'";
            if (MeterName != "")
                strWhere += " and a.MeterName like '%" + MeterName + "%'";
            if (Model != "")
                strWhere += " and a.Model ='" + Model + "'";
            if (SS_Date != "")
                strWhere += " and a.S_Date >='" + SS_Date + "'";
            if (ES_Date != "")
                strWhere += " and a.S_Date <='" + ES_Date + "'";
            if (State != "")
                strWhere += " and a.State ='" + State + "'";

            string Order = "";
            string OrderDate = "";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadCardList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, Order, ModelType);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 主页-获取附件列表 
        public ActionResult LoadFileList()
        {
            string strCurPage;
            string strRowNum;
            string strWhere = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RID = Request["RID"].ToString();
            strWhere += " and a.RID ='" + RID + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadFileList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 主页-下载附件 
        public void DownLoad(string id)
        {
            DataTable dtInfo = FlowMeterMan.GetNewDownloadFile(id);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
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

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }


        #endregion

        #region ----[新增登记卡 完成]

        // 已删除：新增-新增登记卡
        public ActionResult AddCard()
        {
            RepairCard repairCard = new RepairCard();
            repairCard.strRID = FlowMeterMan.GetNewRID();
            return View(repairCard);
        }

        // 已删除：新增-加载需要检查的仪表项目
        public ActionResult GetCheckItems()
        {
            string strErr = "";
            string strItems = FlowMeterMan.GetCheckItems(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strItems == "")
                    return Json(new { success = "false", Msg = "检查项目加载失败" });
                else
                    return Json(new { success = "true", CheckItems = strItems });
            }
        }

        // 已删除：新增-确认新增登记卡
        public ActionResult AddNewCard(RepairCard repairCard)
        {
            if (ModelState.IsValid)
            {
                var IsRepair = Request["StrIsRepair"];
                var Title = Request["HTitle"];
                var Checked = Request["HChecked"];
                repairCard.strRID = FlowMeterMan.GetRID();

                repairCard.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                repairCard.strCreateUser = acc.UserName;

                repairCard.strState = 0;
                repairCard.strIsRepair = IsRepair;
                repairCard.strvalidate = "v";

                string strErr = "";
                if (FlowMeterMan.AddNewCard(repairCard, Title, Checked, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }


        //----【151113 修改后新增登记卡】
        // 新增-新增登记卡
        public ActionResult AddCardNew()
        {
            RepairCardNew repairCard = new RepairCardNew();
            repairCard.strRID = FlowMeterMan.GetNewRID();
            return View(repairCard);
        }

        // 新增-加载超声波需要检查的仪表项目
        public ActionResult GetCheckItemsUT()
        {
            string strErr = "";
            string strItems = FlowMeterMan.GetCheckItemsUT(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strItems == "")
                    return Json(new { success = "false", Msg = "检查项目加载失败" });
                else
                    return Json(new { success = "true", CheckItems = strItems });
            }
        }

        // 新增-确认新增登记卡
        public ActionResult AddNewCard2(RepairCardNew repairCard)
        {
            if ((repairCard.strCustomerName != "" && repairCard.strCustomerName != null) ||
                ((repairCard.strCustomerName == "" || repairCard.strCustomerName == null) &&
                (repairCard.strCustomerNameUT == "" || repairCard.strCustomerNameUT == null)))
            {
                if (ModelState.IsValid)
                {
                    var IsRepair = Request["StrIsRepair"];
                    var IsOut = Request["StrIsOut"];
                    var IsOutUT = Request["StrIsOutUT"];
                    //
                    var FirstCheckUT = Request["StrFirstCheckUT"];
                    var SecondCheckUT = Request["StrSecondCheckUT"];
                    var ThirdCheckUT = Request["StrThirdCheckUT"];
                    //
                    var Title = Request["HTitle"];
                    var Checked = Request["HChecked"];
                    var TitleUT = Request["HTitleUT"];
                    var CheckedUT = Request["HCheckedUT"];
                    repairCard.strRID = FlowMeterMan.GetRID();

                    repairCard.strCreateTime = DateTime.Now;
                    Acc_Account acc = GAccount.GetAccountInfo();
                    repairCard.strCreateUser = acc.UserName;
                    if (repairCard.strRecordNum == null)
                        repairCard.strRecordNum = 0;

                    repairCard.strState = 0;
                    repairCard.strIsRepair = IsRepair;
                    repairCard.strIsOut = IsOut;
                    repairCard.strIsOutUT = IsOutUT;
                    repairCard.strFirstCheckUT = FirstCheckUT;
                    repairCard.strSecondCheckUT = SecondCheckUT;
                    repairCard.strThirdCheckUT = ThirdCheckUT;
                    repairCard.strvalidate = "v";

                    string strErr = "";
                    if (FlowMeterMan.AddNewCard2(repairCard, Title, Checked, TitleUT, CheckedUT, ref strErr) == true)
                        return Json(new { success = "true", Msg = "新增成功" });
                    else
                        return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
                }
                else
                {
                    //如果有错误，继续输入信息
                    return Json(new { success = "false", Msg = "数据验证不通过" });
                }
            }
            else
            {
                var IsRepair = Request["StrIsRepair"];
                var IsOut = Request["StrIsOut"];
                var IsOutUT = Request["StrIsOutUT"];
                var Title = Request["HTitle"];
                var Checked = Request["HChecked"];
                var TitleUT = Request["HTitleUT"];
                var CheckedUT = Request["HCheckedUT"];
                repairCard.strRID = FlowMeterMan.GetRID();

                repairCard.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                repairCard.strCreateUser = acc.UserName;

                repairCard.strState = 0;
                repairCard.strIsRepair = IsRepair;
                repairCard.strvalidate = "v";

                string strErr = "";
                if (FlowMeterMan.AddNewCard2(repairCard, Title, Checked, TitleUT, CheckedUT, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
            }

        }

        // 新增-根据仪表编号判断是否存在当前信息
        public ActionResult CheckMeterInfo()
        {
            string MeterID = Request["MeterID"].ToString();
            string ModelType = Request["ModelType"].ToString();
            DataTable dt = FlowMeterMan.CheckMeterInfo(MeterID, ModelType);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        // 新增-根据仪表类型获取仪表名称 一般是一对一的关系
        public ActionResult GetMeterName()
        {
            string strErr = "";
            string ModelType = Request["ModelType"].ToString();
            string strMeterName = FlowMeterMan.GetMeterName(ModelType, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strMeterName == "")
                    return Json(new { success = "false", Msg = "" });
                else
                    return Json(new { success = "true", MeterName = strMeterName });
            }
        }

        // 新增-加载流量范围下拉框
        public ActionResult GetFlowRange()
        {
            string strModel = "";
            if (Request["Model"] != null)
                strModel = Request["Model"].ToString();

            string strFlowRange = FlowMeterMan.GetFlowRange(strModel);
            if (strFlowRange == "")
                return Json(new { success = "false", Msg = "列表信息加载失败" });
            else
                return Json(new { success = "true", strDetail = strFlowRange });
        }

        // 新增-加载承压等级下拉框
        public ActionResult GetPressure()
        {
            string strModel = "";
            if (Request["Model"] != null)
                strModel = Request["Model"].ToString();

            string strPressure = FlowMeterMan.GetPressure(strModel);
            if (strPressure == "")
                return Json(new { success = "false", Msg = "列表信息加载失败" });
            else
                return Json(new { success = "true", strDetail = strPressure });
        }

        // 新增-超声波加载流量范围下拉框
        public ActionResult GetFlowRangeUT()
        {
            string strModel = "";
            if (Request["Model"] != null)
                strModel = Request["Model"].ToString();

            string strFlowRangeUT = FlowMeterMan.GetFlowRangeUT(strModel);
            if (strFlowRangeUT == "")
                return Json(new { success = "false", Msg = "列表信息加载失败" });
            else
                return Json(new { success = "true", strDetail = strFlowRangeUT });
        }

        // 新增-超声波加载承压等级下拉框
        public ActionResult GetPressureUT()
        {
            string strModel = "";
            if (Request["Model"] != null)
                strModel = Request["Model"].ToString();

            string strPressureUT = FlowMeterMan.GetPressureUT(strModel);
            if (strPressureUT == "")
                return Json(new { success = "false", Msg = "列表信息加载失败" });
            else
                return Json(new { success = "true", strDetail = strPressureUT });
        }



        #endregion

        #region ----[修改登记卡 完成]

        // 修改维修登记卡 
        public ActionResult EditRepairCard()
        {
            if (Request["Info"] == null)
            {
                RepairCard repairCard1 = new RepairCard();
                ViewData["RID"] = "";
                ViewData["State"] = "";

                return View(repairCard1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);

            ViewData["RID"] = repairCard.strRID;
            ViewData["State"] = repairCard.strState;
            return View(repairCard);
        }

        // 修改-获取选中的选项内容
        public ActionResult GetCheckeds()
        {
            var strRID = Request["RID"];// 维修单号            
            DataTable dt = FlowMeterMan.GetCheckeds(strRID);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });
            else
            {
                string strItems = "";
                string strChecked = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strItems += dt.Rows[i]["CheckItem"].ToString() + "?";
                    strChecked += dt.Rows[i]["CheckContent"].ToString() + "?";
                }
                strItems = strItems.TrimEnd('?');
                strChecked = strChecked.TrimEnd('?');
                return Json(new
                {
                    success = "true",
                    CheckItem = strItems,
                    CheckContent = strChecked

                });

            }

        }

        // 修改-确认修改登记卡 
        public ActionResult UpdateCard(RepairCard repairCard)
        {
            if (ModelState.IsValid)
            {
                var IsRepair = Request["StrIsRepair"];
                var Title = Request["HTitle"];
                var Checked = Request["HChecked"];
                repairCard.strRID = Request["RID"];
                repairCard.strState = Convert.ToInt32(Request["State"]);

                repairCard.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                repairCard.strCreateUser = acc.UserName;
                repairCard.strIsRepair = IsRepair;
                repairCard.strvalidate = "v";

                string strErr = "";
                if (FlowMeterMan.UpdateCard(repairCard, Title, Checked, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }



        // 151118ly 修改超声波登记卡信息
        public ActionResult EditRepairCardUT()
        {
            if (Request["Info"] == null)
            {
                RepairCardUT repairCard1 = new RepairCardUT();
                ViewData["RIDUT"] = "";
                ViewData["StateUT"] = "";

                return View(repairCard1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCardUT repairCard = new RepairCardUT();
            repairCard = FlowMeterMan.getNewRepairCardUT(Info[0]);

            ViewData["RIDUT"] = repairCard.strRIDUT;
            ViewData["StateUT"] = repairCard.strStateUT;
            return View(repairCard);
        }

        // 修改-获取超声波选中的选项内容 
        public ActionResult GetCheckedsUT()
        {
            var strRID = Request["RID"];// 维修单号            
            DataTable dt = FlowMeterMan.GetCheckedsUT(strRID);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });
            else
            {
                string strItems = "";
                string strChecked = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strItems += dt.Rows[i]["CheckItem"].ToString() + "?";
                    strChecked += dt.Rows[i]["CheckContent"].ToString() + "?";
                }
                strItems = strItems.TrimEnd('?');
                strChecked = strChecked.TrimEnd('?');
                return Json(new
                {
                    success = "true",
                    CheckItem = strItems,
                    CheckContent = strChecked

                });

            }

        }

        // 修改-确认修改超声波登记卡
        public ActionResult UpdateCardUT(RepairCardUT repairCard)
        {
            if (ModelState.IsValid)
            {
                var IsOutUT = Request["StrIsOutUT"];
                var Title = Request["HTitleUT"];
                var Checked = Request["HCheckedUT"];
                var FirstCheckUT = Request["StrFirstCheckUT"];
                var SecondCheckUT = Request["StrSecondCheckUT"];
                var ThirdCheckUT = Request["StrThirdCheckUT"];

                repairCard.strRIDUT = Request["RIDUT"];
                repairCard.strStateUT = Convert.ToInt32(Request["State"]);

                repairCard.strCreateTimeUT = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                repairCard.strCreateUserUT = acc.UserName;
                repairCard.strIsOutUT = IsOutUT;
                repairCard.strFirstCheckUT = FirstCheckUT;
                repairCard.strSecondCheckUT = SecondCheckUT;
                repairCard.strThirdCheckUT = ThirdCheckUT;
                repairCard.strvalidateUT = "v";

                string strErr = "";
                if (FlowMeterMan.UpdateCardUT(repairCard, Title, Checked, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }




        #endregion

        #region ----[查看登记卡详细 完成]

        // 查看登记卡详细信息 
        public ActionResult DetailCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);
            string strType = FlowMeterMan.getType(repairCard.strModelType);

            ViewData["RID"] = repairCard.strRID;
            ViewData["State"] = repairCard.strState;
            ViewData["Type"] = strType;
            //
            if (repairCard.strIsRepair == "0")
                ViewData["IsRepair"] = "维修";
            else
                ViewData["IsRepair"] = "不维修";
            //
            if (repairCard.strIsOut == "0")
                ViewData["IsOut"] = "是";
            else
                ViewData["IsOut"] = "否";
            //
            if (repairCard.strGetTypeModel == "GetType1")
                ViewData["GetTypeModel"] = "物流取表";
            else if (repairCard.strGetTypeModel == "GetType2")
                ViewData["GetTypeModel"] = "取表人";

            string strModel = FlowMeterMan.getModels(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();
            ViewData["ModelProperty"] = strModel.Split(',')[2].ToString();

            return View(repairCard);

        }

        // 查看-获取操作日志记录列表 
        public ActionResult GetoperateLog()
        {
            string strRID = Request["RID"].ToString();
            string strErr = "";
            string strList = FlowMeterMan.GetoperateLog(strRID, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strList == "")
                    return Json(new { success = "false", Msg = "操作历史记录加载失败" });
                else
                    return Json(new { success = "true", OperateList = strList });
            }
        }


        // 查看超声波登记卡详细
        public ActionResult DetailCardUT()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCardUT repairCard = new RepairCardUT();
            repairCard = FlowMeterMan.getNewRepairCardUT(Info[0]);

            ViewData["RID"] = repairCard.strRIDUT;
            ViewData["State"] = repairCard.strStateUT;
            if (repairCard.strIsOutUT == "0")
                ViewData["IsOutUT"] = "是";
            else
                ViewData["IsOutUT"] = "否";
            //
            if (repairCard.strFirstCheckUT == "0")
                ViewData["FirstCheck"] = "合格";
            else
                ViewData["FirstCheck"] = "不合格";
            if (repairCard.strSecondCheckUT == "0")
                ViewData["SecondCheck"] = "合格";
            else
                ViewData["SecondCheck"] = "不合格";
            if (repairCard.strThirdCheckUT == "0")
                ViewData["ThirdCheck"] = "合格";
            else
                ViewData["ThirdCheck"] = "不合格";
            //
            if (repairCard.strGetTypeModelUT == "GetType1")
                ViewData["GetTypeModelUT"] = "物流取表";
            else if (repairCard.strGetTypeModelUT == "GetType2")
                ViewData["GetTypeModelUT"] = "取表人";

            string strModel = FlowMeterMan.getModelsUT(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["ModelProperty"] = strModel.Split(',')[1].ToString();

            return View(repairCard);

        }


        #endregion

        #region ----[打印登记卡 完成]

        public ActionResult PrintCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);
            //
            if (repairCard.strGetTypeModel == "GetType1")
                ViewData["GetTypeModel"] = "物流取表";
            else if (repairCard.strGetTypeModel == "GetType2")
                ViewData["GetTypeModel"] = "取表人";

            string strModel = FlowMeterMan.getModels(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();
            ViewData["ModelProperty"] = strModel.Split(',')[2].ToString();
            ViewData["RID"] = repairCard.strRID;
            ViewData["State"] = repairCard.strState;
            return View(repairCard);

        }

        // 打印超声波登记卡
        public ActionResult PrintCardUT()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCardUT repairCard = new RepairCardUT();
            repairCard = FlowMeterMan.getNewRepairCardUT(Info[0]);
            //
            if (repairCard.strGetTypeModelUT == "GetType1")
                ViewData["GetTypeModelUT"] = "物流取表";
            else if (repairCard.strGetTypeModelUT == "GetType2")
                ViewData["GetTypeModelUT"] = "取表人";

            string strModel = FlowMeterMan.getModelsUT(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["ModelProperty"] = strModel.Split(',')[1].ToString();
            ViewData["RID"] = repairCard.strRIDUT;
            ViewData["State"] = repairCard.strStateUT;
            return View(repairCard);

        }


        #endregion

        #region ----[打印随工单 完成]

        public ActionResult PrintWorkCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCard workCard = new tk_WorkCard();
            workCard = FlowMeterMan.getNewWorkCard(Info[0]);

            string strRepairID = FlowMeterMan.getRepairID(Info[0]);
            ViewData["RepairID"] = strRepairID.ToString();

            return View(workCard);

        }

        // 打印随工单-加载需要检查的仪表项目
        public ActionResult GetOutCheck()
        {
            string strErr = "";
            string strItems = FlowMeterMan.GetOutCheck(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strItems == "")
                    return Json(new { success = "false", Msg = "检查项目加载失败" });
                else
                    return Json(new { success = "true", CheckItems = strItems });
            }
        }

        // 打印随工单-加载选中的检测项目
        public ActionResult GetOutCheckeds()
        {
            var strRID = Request["RID"];// 维修内部单号            
            DataTable dt = FlowMeterMan.GetOutCheckeds(strRID);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });
            else
            {
                string strItems = "";
                string strChecked = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strItems += dt.Rows[i]["CheckItem"].ToString() + "?";
                    strChecked += dt.Rows[i]["CheckContent"].ToString() + "?";
                }
                strItems = strItems.TrimEnd('?');
                strChecked = strChecked.TrimEnd('?');
                return Json(new
                {
                    success = "true",
                    CheckItem = strItems,
                    CheckContent = strChecked

                });

            }

        }

        // 打印随工单-加载更换部件列表 
        public ActionResult getChangeBakList()
        {
            string strRID = Request["RID"].ToString();
            string strErr = "";
            string strList = FlowMeterMan.GetChangeBakList(strRID, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strList == "")
                    return Json(new { success = "false", Msg = "更换部件列表加载失败" });
                else
                    return Json(new { success = "true", ChangeBakList = strList });
            }
        }


        // 超声波随工单 
        public ActionResult PrintWorkCardUT()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCardUT workCard = new tk_WorkCardUT();
            workCard = FlowMeterMan.getNewWorkCardUT(Info[0]);

            // 维修编号 流量计编号 型号 
            string strList = FlowMeterMan.getMeterInfo(Info[0]);
            ViewData["RepairID"] = strList.Split(',')[0].ToString();
            ViewData["MeterID"] = strList.Split(',')[1].ToString();
            ViewData["Model"] = strList.Split(',')[2].ToString();

            return View(workCard);

        }


        #endregion

        #region ----[打印流转单 完成]

        public ActionResult PrintTransCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);

            string strModel = FlowMeterMan.getModels(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();
            ViewData["RID"] = repairCard.strRID;

            tk_TransCard transCard = new tk_TransCard();
            transCard = FlowMeterMan.getNewTransCard(Info[0]);
            ViewData["FirstCheck"] = transCard.strFirstCheck;
            ViewData["SendRepair"] = transCard.strSendRepair;
            ViewData["LastCheck"] = transCard.strLastCheck;
            ViewData["OneRepair"] = transCard.strOneRepair;
            ViewData["TwoCheck"] = transCard.strTwoCheck;
            ViewData["TwoRepair"] = transCard.strTwoRepair;
            ViewData["ThreeCheck"] = transCard.strThreeCheck;
            ViewData["ThreeRepair"] = transCard.strThreeRepair;
            ViewData["Comments"] = transCard.strComments;

            return View(repairCard);

        }



        #endregion

        #region ----[下发任务 完成]

        public ActionResult SendTaskCard()
        {
            if (Request["Info"] == null)
            {
                ViewData["RID"] = "";
                ViewData["RepairID"] = "";
                ViewData["State"] = "";

                return View();
            }
            else
            {
                string[] Info = Request["Info"].ToString().Split('@');
                if (Info[1] == "超声波")// 超声波 
                {
                    RepairCardUT repairCard = new RepairCardUT();
                    repairCard = FlowMeterMan.getNewRepairCardUT(Info[0]);
                    ViewData["RID"] = repairCard.strRIDUT;
                    ViewData["RepairID"] = repairCard.strRepairIDUT;
                    ViewData["State"] = repairCard.strStateUT;
                    return View();
                }
                else
                {
                    RepairCard repairCard = new RepairCard();
                    repairCard = FlowMeterMan.getNewRepairCard(Info[0]);
                    ViewData["RID"] = repairCard.strRID;
                    ViewData["RepairID"] = repairCard.strRepairID;
                    ViewData["State"] = repairCard.strState;
                    return View();
                }

            }
        }

        // 获取接收任务的小组列表 
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
            udtTask = FlowMeterMan.getGroupList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 获取人员列表 
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
            udtTask = FlowMeterMan.getPersonList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 下发任务 1.在下发任务表中添加小组和人员信息 2.在维修登记卡中填入完成时间，是否送外部检测
        public ActionResult SendTask()
        {
            var FinishDate = Request["FinishDate"];// 完成时间
            var Checked = Request["SaveUser"];
            var RID = Request["RID"];// 内部维修编号

            string strErr = "";
            if (FlowMeterMan.SendTask(RID, FinishDate, Checked, ref strErr) == true)
                return Json(new { success = "true", Msg = "下发任务成功" });
            else
                return Json(new { success = "false", Msg = "下发任务失败" + "/" + strErr });

        }


        #endregion

        #region ----[确认收货 完成]

        public ActionResult TakeDelivery()
        {
            return View();
        }

        #region ----[确认收货 完成]

        // 加载状态为收货确认之前的登记卡列表 
        public ActionResult LoadTaskList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string State = "0";// 已登记状态

            string strWhere = "";
            if (State != "")
                strWhere += " and a.State ='" + State + "'";

            string Order = "";
            string OrderDate = "";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadCardListAll(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 确认收货-确定 
        public ActionResult CheckReceive()
        {
            string strErr = "";
            var RIDs = Request["CheckIDs"];
            var TakeID = Request["TakeID"];
            var UnitName = Request["UnitName"];
            var DeliverName = Request["DeliverName"];
            var DeliverTel = Request["DeliverTel"];
            var DeliverDate = Request["DeliverDate"];
            var ReceiveName = Request["ReceiveName"];
            var ReceiveTel = Request["ReceiveTel"];
            var ReceiveDate = Request["ReceiveDate"];

            if (FlowMeterMan.CheckReceive(RIDs, TakeID, UnitName, DeliverName, DeliverTel, DeliverDate, ReceiveName, ReceiveTel, ReceiveDate, ref strErr) == true)
                return Json(new { success = "true", Msg = "确认收货成功" });
            else
                return Json(new { success = "false", Msg = "确认收货失败" + "/" + strErr });
        }

        #endregion

        #region  ----[入库操作 完成]

        // 确认收货-判断选中的数据是否已经进行过入库操作
        public ActionResult CheckStockInfo()
        {
            var IDs = Request["CheckIDs"];
            string strErr = "";
            if (FlowMeterMan.CheckStockInfo(IDs, ref strErr) == true)
                return Json(new { success = "true", Msg = "" });
            else
                return Json(new { success = "false", Msg = "所选数据已经进行过入库操作，请勿重复操作，重复编号为：" + "/" + strErr });
        }

        // 确认收货-入库
        public ActionResult InStockDetail()
        {
            if (Request["Info"] == null)
            {
                tk_StockIn stockIn1 = new tk_StockIn();
                return View(stockIn1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_StockIn stockIn = new tk_StockIn();
            stockIn.strRID = Info[0];
            stockIn.strStockID = FlowMeterMan.GetNewStockID();
            Acc_Account acc = GAccount.GetAccountInfo();
            stockIn.strStockUnit = acc.UnitName;// 当前登陆单位
            return View(stockIn);
        }

        // 确认收货-确定入库
        public ActionResult AddStockInfo(tk_StockIn stockIn)
        {
            if (ModelState.IsValid)
            {
                var RIDs = stockIn.strRID;
                stockIn.strStockID = FlowMeterMan.GetStockID();
                stockIn.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                stockIn.strStockUnit = acc.UnitName;// 入库单位
                stockIn.strCreateUser = acc.UserName;
                stockIn.strValidate = "v";

                string strErr = "";
                if (FlowMeterMan.AddStockInfo(stockIn, RIDs, ref strErr) == true)
                    return Json(new { success = "true", Msg = "入库信息提交成功" });
                else
                    return Json(new { success = "false", Msg = "入库信息提交出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }

        #endregion

        #endregion

        #region ----[上传附件 完成]

        public ActionResult UpLoadManage()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_FileUpload FileUp = new tk_FileUpload();
            FileUp.StrRID = Info[0].ToString();
            ViewData["RID"] = Info[0].ToString();
            return View(FileUp);
        }

        // 上传附件
        public ActionResult InsertBidding(tk_FileUpload fileUp, string Pname)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                fileUp.StrRID = Request["Hidden"].ToString();
                fileUp.StrCreatePerson = account.UserName.ToString();
                fileUp.StrCreateTime = DateTime.Now;
                fileUp.StrValidate = "v";
                HttpPostedFileBase file = Request.Files[0];
                byte[] fileByte = new byte[0];
                fileUp.StrFileName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                ViewData["FileName"] = fileUp.StrFileName;

                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
                string strErr = "";
                if (FlowMeterMan.InsertNewFile(fileUp, fileByte, ref strErr) == true)
                {
                    ViewData["msg"] = "上传已完成";
                    return View("UpLoadManage", fileUp);
                }
                else
                {
                    ViewData["msg"] = "上传失败";
                    return View("UpLoadManage", fileUp);
                }
            }
            else
            {
                //如果有错误，继续输入信息
                return View("UpLoadManage", fileUp);
            }
        }


        #endregion


        #endregion

        #region // 随工单管理 完成

        public ActionResult WorkCardManage()
        {
            return View();
        }

        #region ----[查询随工单 完成]

        // 随工单管理-查询加载随工单列表 
        public ActionResult LoadWorkCardList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RepairID = Request["RepairID"].ToString();
            string SRepairDate = Request["SRepairDate"].ToString();
            string ERepairDate = Request["ERepairDate"].ToString();
            string RepairUser = Request["RepairUser"].ToString();
            string ModelType = "";
            if (Request["CardType"] != null)
                ModelType = Request["CardType"].ToString();

            string strWhere = "";
            if (RepairID != "")
                strWhere += " and b.RepairID ='" + RepairID + "'";
            if (SRepairDate != "")
                strWhere += " and a.RepairDate >= '" + SRepairDate + "'";
            if (ERepairDate != "")
                strWhere += " and a.RepairDate <= '" + ERepairDate + "'";
            if (RepairUser != "")
                strWhere += " and a.RepairUser like '%" + RepairUser + "%'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadWorkCardList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, ModelType);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 随工单管理-查询超声波随工单列表
        public ActionResult LoadWorkCardListUT()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RepairID = Request["RepairID"].ToString();
            string SRepairDate = Request["SRepairDate"].ToString();
            string ERepairDate = Request["ERepairDate"].ToString();
            string RepairUser = Request["RepairUser"].ToString();
            string ModelType = Request["ModelType"].ToString();

            string strWhere = "";
            if (RepairID != "")
                strWhere += " and b.RepairID ='" + RepairID + "'";
            if (SRepairDate != "")
                strWhere += " and a.RepairDate >= '" + SRepairDate + "'";
            if (ERepairDate != "")
                strWhere += " and a.RepairDate <= '" + ERepairDate + "'";
            if (RepairUser != "")
                strWhere += " and a.RepairUser like '%" + RepairUser + "%'";
            if (ModelType != "")
                strWhere += " and d.Text ='" + ModelType + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadWorkCardListUT(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region ----[修改随工单 完成]

        // 随工单管理-修改随工单 
        public ActionResult EditWorkCard()
        {
            if (Request["Info"] == null)
            {
                tk_WorkCard workCard1 = new tk_WorkCard();
                ViewData["RepairID"] = "";
                ViewData["RID"] = "";

                return View(workCard1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCard workCard = new tk_WorkCard();
            workCard = FlowMeterMan.getNewWorkCard(Info[0]);

            string strRepairID = FlowMeterMan.getRepairID(Info[0]);
            ViewData["RepairID"] = strRepairID.ToString();
            ViewData["RID"] = workCard.strRID.ToString();

            return View(workCard);
        }

        // 随工单管理-确认修改随工单 
        public ActionResult UpdateWorkCard(tk_WorkCard workCard)
        {
            if (ModelState.IsValid)
            {
                // radio 选项
                var StrIsRepairY = Request["StrIsIsRepairY"];
                var StrIsYChangeBak = Request["StrIsYChangeBak"];
                var StrIsYUnChangeBak = Request["StrIsYUnChangeBak"];
                var StrIsRepairN = Request["StrIsIsRepairN"];
                var StrIsNChangeBak = Request["StrIsNChangeBak"];
                var StrIsNUnChangeBak = Request["StrIsNUnChangeBak"];
                var Title = Request["HTitle"];
                var Checked = Request["HChecked"];

                workCard.strValidate = "v";
                workCard.strIsRepairY = Request["IsRepairY"];
                workCard.strYChangeBak = Request["IsYChangeBak"];
                workCard.strYUnChangeBak = Request["IsYUnChangeBak"];
                workCard.strIsRepairN = Request["IsRepairN"];
                workCard.strNChangeBak = Request["IsNChangeBak"];
                workCard.strNUnChangeBak = Request["IsNUnChangeBak"];

                // 更换部件信息
                var RID = Request["RID"];// 维修单内部编号 
                var BakName = Request["BakName"];// 部件名称
                var BakType = Request["BakType"];// 规格型号
                var BakNum = Request["BakNum"];// 数量
                var Comments = Request["Comments"];// 备注

                string strErr = "";
                if (FlowMeterMan.UpdateWorkCard(workCard, Title, Checked, RID, BakName, BakType, BakNum, Comments, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });

            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }

        // 随工单管理-加载更换部件记录列表
        public ActionResult getChangeBakList2()
        {
            string a_strErr = "";
            var strRID = Request["strRID"];// 维修内部编号            
            DataTable dt = FlowMeterMan.getChangeBakList2(strRID, ref a_strErr);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });

            string BakName = "";// 名称 
            string BakType = "";// 管径规格
            string BakNum = "";// 数量
            string Comments = "";// 备注

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BakName += dt.Rows[i]["BakName"].ToString() + ",";
                BakType += dt.Rows[i]["BakType"].ToString() + ",";
                BakNum += dt.Rows[i]["BakNum"].ToString() + ",";
                Comments += dt.Rows[i]["Comments"].ToString() + "@";
            }
            BakName = BakName.TrimEnd(',');
            BakType = BakType.TrimEnd(',');
            BakNum = BakNum.TrimEnd(',');
            Comments = Comments.Substring(0, Comments.Length - 1);

            return Json(new
            {
                success = "true",
                BakName = BakName,
                BakType = BakType,
                BakNum = BakNum,
                Comments = Comments
            });
        }


        // 超声波随工单修改
        public ActionResult EditWorkCardUT()
        {
            if (Request["Info"] == null)
            {
                tk_WorkCardUT workCard1 = new tk_WorkCardUT();
                ViewData["RepairID"] = "";
                ViewData["MeterID"] = "";
                ViewData["Model"] = "";

                return View(workCard1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCardUT workCard = new tk_WorkCardUT();
            workCard = FlowMeterMan.getNewWorkCardUT(Info[0]);

            // 维修编号 流量计编号 型号 
            string strList = FlowMeterMan.getMeterInfo(Info[0]);
            ViewData["RepairID"] = strList.Split(',')[0].ToString();
            ViewData["MeterID"] = strList.Split(',')[1].ToString();
            ViewData["Model"] = strList.Split(',')[2].ToString();

            return View(workCard);
        }

        // 超声波随工单管理-确认修改随工单 
        public ActionResult UpdateWorkCardUT(tk_WorkCardUT workCard)
        {
            if (ModelState.IsValid)
            {
                // radio 选项
                var StrChangePlace = Request["StrChangePlace"];
                var StrCheck1 = Request["StrCheck1"];
                var StrCheck2 = Request["StrCheck2"];
                var StrCheck3 = Request["StrCheck3"];
                var StrCheck4 = Request["StrCheck4"];
                var StrCheck5 = Request["StrCheck5"];
                var StrCheck6 = Request["StrCheck6"];
                var StrCheck7 = Request["StrCheck7"];
                var StrCheck8 = Request["StrCheck8"];
                var StrRepairContent1 = Request["StrRepairContent1"];
                var StrRepairContent2 = Request["StrRepairContent2"];
                var StrRepairContent3 = Request["StrRepairContent3"];

                workCard.strValidate = "v";
                workCard.strChangePlace = Request["ChangePlace"];
                workCard.strCheck1 = Request["Check1"];
                workCard.strCheck2 = Request["Check2"];
                workCard.strCheck3 = Request["Check3"];
                workCard.strCheck4 = Request["Check4"];
                workCard.strCheck5 = Request["Check5"];
                workCard.strCheck6 = Request["Check6"];
                workCard.strCheck7 = Request["Check7"];
                workCard.strCheck8 = Request["Check8"];
                workCard.strRepairContent1 = Request["RepairContent1"];
                workCard.strRepairContent2 = Request["RepairContent2"];
                workCard.strRepairContent3 = Request["RepairContent3"];

                string strErr = "";
                if (FlowMeterMan.UpdateWorkCardUT(workCard, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });

            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[查看随工单详细 完成]

        // 随工单管理-查看随工单详细
        public ActionResult DetailWorkCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCard workCard = new tk_WorkCard();
            workCard = FlowMeterMan.getNewWorkCard(Info[0]);

            string strRepairID = FlowMeterMan.getRepairID(Info[0]);
            ViewData["RepairID"] = strRepairID.ToString();

            return View(workCard);

        }


        // 随工单管理-查看超声波详细
        public ActionResult DetailWorkCardUT()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_WorkCardUT workCard = new tk_WorkCardUT();
            workCard = FlowMeterMan.getNewWorkCardUT(Info[0]);

            // 维修编号 流量计编号 型号 
            string strList = FlowMeterMan.getMeterInfo(Info[0]);
            ViewData["RepairID"] = strList.Split(',')[0].ToString();
            ViewData["MeterID"] = strList.Split(',')[1].ToString();
            ViewData["Model"] = strList.Split(',')[2].ToString();

            return View(workCard);

        }


        #endregion


        #endregion

        #region // 流转卡管理 完成

        public ActionResult TransCardManage()
        {
            return View();
        }

        #region ----[查询流转卡 完成]

        // 流转卡管理-查询加载流转卡列表 
        public ActionResult LoadTransCardList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RepairID = Request["RepairID"].ToString();
            string CustomerName = Request["CustomerName"].ToString();
            string MeterID = Request["MeterID"].ToString();

            string strWhere = "";
            if (RepairID != "")
                strWhere += " and b.RepairID ='" + RepairID + "'";
            if (CustomerName != "")
                strWhere += " and b.CustomerName like '%" + CustomerName + "%'";
            if (MeterID != "")
                strWhere += " and b.MeterID like '%" + MeterID + "%'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadTransCardList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 流转卡管理-查询加载超声波流转卡列表 
        public ActionResult LoadTransCardListUT()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RepairID = Request["RepairID"].ToString();
            string CustomerName = Request["CustomerName"].ToString();
            string MeterID = Request["MeterID"].ToString();

            string strWhere = "";
            if (RepairID != "")
                strWhere += " and b.RepairID ='" + RepairID + "'";
            if (CustomerName != "")
                strWhere += " and b.CustomerNamelike '%" + CustomerName + "%'";
            if (MeterID != "")
                strWhere += " and b.MeterID like '%" + MeterID + "%'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadTransCardListUT(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region ----[修改流转卡 完成]

        // 流转卡管理-修改流转卡
        public ActionResult EditTransCard()
        {
            if (Request["Info"] == null)
            {
                RepairCard repairCard1 = new RepairCard();
                ViewData["FirstCheck"] = "";
                ViewData["SendRepair"] = "";
                ViewData["LastCheck"] = "";
                ViewData["OneRepair"] = "";
                ViewData["TwoCheck"] = "";
                ViewData["TwoRepair"] = "";
                ViewData["ThreeCheck"] = "";
                ViewData["ThreeRepair"] = "";
                ViewData["Comments"] = "";
                ViewData["TID"] = "";
                return View(repairCard1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);
            string strModel = FlowMeterMan.getModels(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();

            tk_TransCard transCard = new tk_TransCard();
            transCard = FlowMeterMan.getNewTransCard(Info[0]);
            ViewData["FirstCheck"] = transCard.strFirstCheck;
            ViewData["SendRepair"] = transCard.strSendRepair;
            ViewData["LastCheck"] = transCard.strLastCheck;
            ViewData["OneRepair"] = transCard.strOneRepair;
            ViewData["TwoCheck"] = transCard.strTwoCheck;
            ViewData["TwoRepair"] = transCard.strTwoRepair;
            ViewData["ThreeCheck"] = transCard.strThreeCheck;
            ViewData["ThreeRepair"] = transCard.strThreeRepair;
            ViewData["Comments"] = transCard.strComments;
            ViewData["TID"] = transCard.strTID;

            return View(repairCard);
        }

        // 流转卡管理-确认修改流转卡
        public ActionResult UpdateTransCard()
        {
            if (ModelState.IsValid)
            {
                var TID = Request["TID"];
                var RID = Request["strRID"];
                var FirstCheck = Request["strFirstCheck"];
                var SendRepair = Request["strSendRepair"];
                var LastCheck = Request["strLastCheck"];
                var OneRepair = Request["strOneRepair"];
                var TwoCheck = Request["strTwoCheck"];
                var TwoRepair = Request["strTwoRepair"];
                var ThreeCheck = Request["strThreeCheck"];
                var ThreeRepair = Request["strThreeRepair"];
                var Comments = Request["strComments"];

                string strErr = "";
                if (FlowMeterMan.UpdateTransCard(TID, RID, FirstCheck, SendRepair, LastCheck, OneRepair, TwoCheck, TwoRepair, ThreeCheck,
                    ThreeRepair, Comments, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });

            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[查看流转卡详细 完成]

        // 流转卡管理-查看流转卡详细
        public ActionResult DetailTransCard()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Info[0]);
            string strModel = FlowMeterMan.getModels(Info[0]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();

            tk_TransCard transCard = new tk_TransCard();
            transCard = FlowMeterMan.getNewTransCard(Info[0]);
            ViewData["FirstCheck"] = transCard.strFirstCheck;
            ViewData["SendRepair"] = transCard.strSendRepair;
            ViewData["LastCheck"] = transCard.strLastCheck;
            ViewData["OneRepair"] = transCard.strOneRepair;
            ViewData["TwoCheck"] = transCard.strTwoCheck;
            ViewData["TwoRepair"] = transCard.strTwoRepair;
            ViewData["ThreeCheck"] = transCard.strThreeCheck;
            ViewData["ThreeRepair"] = transCard.strThreeRepair;
            ViewData["Comments"] = transCard.strComments;

            return View(repairCard);

        }


        #endregion


        #endregion

        #region // 收货管理 完成

        #region ----[收货单查询 完成]

        public ActionResult DeliveryManage()
        {
            return View();
        }

        // 收货管理-查询
        public ActionResult LoadDeliveryList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string TakeID = Request["TakeID"].ToString();
            string UnitName = Request["UnitName"].ToString();
            string SDeliverDate = Request["SDeliverDate"].ToString();
            string EDeliverDate = Request["EDeliverDate"].ToString();
            string ReceiveName = Request["ReceiveName"].ToString();
            string SReceiveDate = Request["SReceiveDate"].ToString();
            string EReceiveDate = Request["EReceiveDate"].ToString();

            string strWhere = "";
            if (TakeID != "")
                strWhere += " and a.TakeID like '%" + TakeID + "%'";
            if (UnitName != "")
                strWhere += " and a.UnitName like '%" + UnitName + "%'";
            if (SDeliverDate != "")
                strWhere += " and a.DeliverDate >='" + SDeliverDate + "'";
            if (EDeliverDate != "")
                strWhere += " and a.DeliverDate <='" + EDeliverDate + "'";
            if (ReceiveName != "")
                strWhere += " and a.ReceiveName like '%" + ReceiveName + "%'";
            if (SReceiveDate != "")
                strWhere += " and a.ReceiveDate >='" + SReceiveDate + "'";
            if (EReceiveDate != "")
                strWhere += " and a.ReceiveDate <='" + EReceiveDate + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadDeliveryList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 收货管理-加载收货单详细列表
        public ActionResult LoadDetailList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string TakeID = Request["TakeID"].ToString();
            string strWhere = "";
            if (TakeID != "")
                strWhere += " and b.TakeID ='" + TakeID + "'";
            //else
            //    strWhere += " and a.TakeID='' ";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadDetailList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region ----[收货单修改 完成]

        // 收货管理-修改
        public ActionResult EditDelivery()
        {
            if (Request["Info"] == null)
            {
                tk_TakeDelivery takeDelivery1 = new tk_TakeDelivery();
                return View(takeDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_TakeDelivery takeDelivery = new tk_TakeDelivery();
            takeDelivery = FlowMeterMan.getNewDelivery(Info[0]);

            return View(takeDelivery);
        }

        // 收货管理-确认修改 
        public ActionResult UpdateDelivery(tk_TakeDelivery takeDelivery)
        {
            if (ModelState.IsValid)
            {
                takeDelivery.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                takeDelivery.strCreateUser = acc.UserName;
                takeDelivery.strValidate = "v";

                string strErr = "";
                if (FlowMeterMan.UpdateDelivery(takeDelivery, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[收货单撤销 完成]

        // 收货管理-撤销界面
        public ActionResult DelDelivery()
        {
            if (Request["Info"] == null)
            {
                tk_TakeDelivery takeDelivery1 = new tk_TakeDelivery();
                ViewData["TaskID"] = "";
                return View(takeDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_TakeDelivery takeDelivery = new tk_TakeDelivery();
            takeDelivery = FlowMeterMan.getNewDelivery(Info[0]);
            ViewData["TaskID"] = takeDelivery.strTakeID;

            return View(takeDelivery);
        }

        // 收货管理-撤销收货单
        public ActionResult ReDelivery()
        {
            var strRIDs = Request["RID"];
            string a_strErr = "";
            if (FlowMeterMan.ReDelivery(strRIDs, ref a_strErr) == true)
                return Json(new { success = "true", Msg = "撤销成功" });
            else
                return Json(new { success = "false", Msg = "撤销出错" + "/" + a_strErr });
        }


        #endregion

        #region ----[收货确认单查看并打印 完成]

        // 界面
        public ActionResult DeliveryDtail()
        {
            if (Request["Info"] == null)
            {
                tk_TakeDelivery takeDelivery1 = new tk_TakeDelivery();
                return View(takeDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_TakeDelivery takeDelivery = new tk_TakeDelivery();
            takeDelivery = FlowMeterMan.getNewDelivery2(Info[0]);

            return View(takeDelivery);
        }

        // 收货确认单查看-加载列表 
        public ActionResult GetDeliveryInfo()
        {
            string strTakeID = Request["TakeID"].ToString();
            string strErr = "";
            string strList = FlowMeterMan.GetDeliveryInfo(strTakeID, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strList == "")
                    return Json(new { success = "false", Msg = "收货确认单明细加载失败" });
                else
                    return Json(new { success = "true", DeliveryInfo = strList });
            }
        }


        #endregion


        #endregion

        #region // 送检管理 完成

        #region ----[送检单管理首页 完成]

        public ActionResult InspectionManage()
        {
            return View();
        }

        // 送检单管理-加载送检表列表 
        public ActionResult LoadInspecList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string SID = Request["SID"].ToString();
            string BathID = Request["BathID"].ToString();
            string SInspecDate = Request["SInspecDate"].ToString();
            string EInspecDate = Request["EInspecDate"].ToString();

            string strWhere = "";
            if (SID != "")
                strWhere += " and a.SID like '%" + SID + "%'";
            if (BathID != "")
                strWhere += " and a.BathID ='" + BathID + "'";
            if (SInspecDate != "")
                strWhere += " and a.InspecDate >='" + SInspecDate + "'";
            if (EInspecDate != "")
                strWhere += " and a.InspecDate <='" + EInspecDate + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadInspecList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region ----[新建送检单 完成]

        // 生成（新建）送检单 
        public ActionResult AddInspection()
        {
            tk_InspecMain inspecMain = new tk_InspecMain();
            inspecMain.strSID = FlowMeterMan.GetNewSID();

            return View(inspecMain);
        }

        // 新建送检单-选择仪表 
        public ActionResult SelectMeter()
        {
            return View();
        }

        // 新建送检单-加载仪表列表 [登记卡中是否送检IsOut=1&&状态为7-维修完成]
        public ActionResult LoadMeterList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string strWhere = "";
            strWhere += " and a.IsOut='1' ";
            strWhere += " and a.State='7' ";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadMeterList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 新建送检单-根据RID获取仪表信息 
        public ActionResult GetMeterDetail()
        {
            string RID = Request["RID"].ToString();
            DataTable dt = FlowMeterMan.GetMeterDetail(RID);

            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        // 新建送检单-确定提交
        public ActionResult InsertInspec(tk_InspecMain InspecMain)
        {
            if (ModelState.IsValid)
            {
                string a_strErr = "";
                InspecMain.strSID = FlowMeterMan.GetSID();
                InspecMain.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                InspecMain.strCreateUser = acc.UserName;
                InspecMain.strValidate = "v";

                string[] SID = Request["SID"].Split(',');
                string[] RID = Request["RID"].Split(',');
                string[] MeterID = Request["MeterID"].Split(',');
                string[] Accuracy = Request["Accuracy"].Split(',');
                string[] Mcount = Request["Mcount"].Split(',');
                string[] OutUnit = Request["OutUnit"].Split(',');
                List<tk_InspecDetail> list = new List<tk_InspecDetail>();
                for (int i = 0; i < SID.Length; i++)
                {
                    tk_InspecDetail meterdetail = new tk_InspecDetail();
                    meterdetail.SID = SID[i];
                    meterdetail.RID = RID[i];
                    meterdetail.MeterID = MeterID[i];
                    meterdetail.Accuracy = Accuracy[i];
                    if (string.IsNullOrEmpty(Mcount[i]))
                        meterdetail.Mcount = 0;
                    else
                        meterdetail.Mcount = Convert.ToInt32(Mcount[i]);

                    meterdetail.CreateTime = DateTime.Now;
                    meterdetail.CreateUser = acc.UserName;
                    meterdetail.Validate = "v";

                    list.Add(meterdetail);
                }
                if (FlowMeterMan.InsertInspec(InspecMain, list, OutUnit, ref a_strErr) == true)
                    return Json(new { success = "true", Msg = "新建送检单成功" });
                else
                    return Json(new { success = "false", Msg = "新建送检单出错" + "/" + a_strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[修改送检单 完成]

        // 修改送检单-通过SID获取送检单信息  
        public ActionResult EditInspection()
        {
            if (Request["Info"] == null)
            {
                tk_InspecMain inspecMain1 = new tk_InspecMain();
                return View(inspecMain1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_InspecMain inspecMain = FlowMeterMan.getInspectBySID(Info[0]);
            return View(inspecMain);
        }

        // 修改送检单-获取多条详细信息列表 
        public ActionResult GetInspecInfo()
        {
            string SID = Request["SID"].ToString();
            DataTable dt = FlowMeterMan.GetInspecInfo(SID);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }

        // 修改送检单-确认修改
        public ActionResult UpdateInspec(tk_InspecMain inspecMain)
        {
            if (ModelState.IsValid)
            {
                string a_strErr = "";
                inspecMain.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                inspecMain.strCreateUser = acc.UserName;
                inspecMain.strValidate = "v";
                var strRIDs = Request["Hidden"];

                string[] SID = Request["SID"].Split(',');
                string[] RID = Request["RID"].Split(',');
                string[] MeterID = Request["MeterID"].Split(',');
                string[] Accuracy = Request["Accuracy"].Split(',');
                string[] Mcount = Request["Mcount"].Split(',');
                string[] OutUnit = Request["OutUnit"].Split(',');
                List<tk_InspecDetail> list = new List<tk_InspecDetail>();
                for (int i = 0; i < SID.Length; i++)
                {
                    tk_InspecDetail meterdetail = new tk_InspecDetail();
                    meterdetail.SID = SID[i];
                    meterdetail.RID = RID[i];
                    meterdetail.MeterID = MeterID[i];
                    meterdetail.Accuracy = Accuracy[i];
                    if (string.IsNullOrEmpty(Mcount[i]))
                        meterdetail.Mcount = 0;
                    else
                        meterdetail.Mcount = Convert.ToInt32(Mcount[i]);

                    meterdetail.CreateTime = DateTime.Now;
                    meterdetail.CreateUser = acc.UserName;
                    meterdetail.Validate = "v";

                    list.Add(meterdetail);
                }
                if (FlowMeterMan.UpdateInspec(inspecMain, list, strRIDs, OutUnit, ref a_strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + a_strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[查看详细并导出 完成]

        // 查看详细-主页
        public ActionResult InspectionDetail()
        {
            return View();
        }

        // 查看详细-获取详细列表
        public ActionResult LoadInspecDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string strOutUnit = Request["OutUnit"].ToString();
            string strBathID = Request["BathID"].ToString();
            string strWhere = "";
            if (strOutUnit != "")
                strWhere += " and c.OutUnit like '%" + strOutUnit + "%'";
            if (strBathID != "")
                strWhere += " and b.BathID ='" + strBathID + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadInspecDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 查看详细-导出到excel 
        [HttpPost]
        public ActionResult InspecToExcel()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";
            string strOutUnit = Request["OutUnit"].ToString();
            string strBathID = Request["BathID"].ToString();
            string strWhere = "";
            if (strOutUnit != "")
                strWhere += " and c.OutUnit like '%" + strOutUnit + "%'";
            if (strBathID != "")
                strWhere += " and b.BathID ='" + strBathID + "'";
            //
            DataTable dt = FlowMeterMan.GetDetailInfo(strWhere);
            if (dt != null)
            {
                string strCols = "序号-3000,器具名称-4000,生产厂家-6000,规格型号-4000,编号-5000,管径（DN）-5000,准确度-4000,数量（台）-5000,使用地点-6000,送检批次-4000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "外送检定明细表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "InspecDetail.xls");
            }
            else
            {
                return null;
            }
        }


        #endregion



        #endregion

        #region // 发货管理 完成

        #region ----[发货单查询 完成]

        public ActionResult SendManage()
        {
            return View();
        }

        // 发货管理-查询
        public ActionResult LoadSendList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string DeliverID = Request["DeliverID"].ToString();
            string UnitName = Request["UnitName"].ToString();
            string SReceiveDate = Request["SReceiveDate"].ToString();
            string EReceiveDate = Request["EReceiveDate"].ToString();
            string SSendDate = Request["SSendDate"].ToString();
            string ESendDate = Request["ESendDate"].ToString();

            string strWhere = "";
            if (DeliverID != "")
                strWhere += " and a.DeliverID like '%" + DeliverID + "%'";
            if (UnitName != "")
                strWhere += " and a.UnitName like '%" + UnitName + "%'";
            if (SReceiveDate != "")
                strWhere += " and a.ReceiveDate >='" + SReceiveDate + "'";
            if (EReceiveDate != "")
                strWhere += " and a.ReceiveDate <='" + EReceiveDate + "'";
            if (SSendDate != "")
                strWhere += " and a.SendDate >='" + SSendDate + "'";
            if (ESendDate != "")
                strWhere += " and a.SendDate <='" + ESendDate + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadSendList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 发货管理-加载发货单详细列表
        public ActionResult LoadSendDetail()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string DeliverID = Request["DeliverID"].ToString();
            string strWhere = "";
            if (DeliverID != "")
                strWhere += " and b.DeliverID ='" + DeliverID + "'";
            //else
            //    strWhere += " and b.DeliverID='' ";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadSendDetail(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region ----[发货单修改 完成]

        // 发货管理-修改
        public ActionResult EditSendDelivery()
        {
            if (Request["Info"] == null)
            {
                tk_SendDelivery sendDelivery1 = new tk_SendDelivery();
                return View(sendDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_SendDelivery sendDelivery = new tk_SendDelivery();
            sendDelivery = FlowMeterMan.getNewSendDelivery(Info[0]);

            return View(sendDelivery);
        }

        // 发货管理-确认修改 
        public ActionResult UpdateSendDelivery(tk_SendDelivery sendDelivery)
        {
            if (ModelState.IsValid)
            {
                sendDelivery.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                sendDelivery.strCreateUser = acc.UserName;
                sendDelivery.strValidate = "v";

                string strErr = "";
                if (FlowMeterMan.UpdateSendDelivery(sendDelivery, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[发货单撤销 完成]

        // 发货管理-撤销界面
        public ActionResult DelSendDelivery()
        {
            if (Request["Info"] == null)
            {
                tk_SendDelivery sendDelivery1 = new tk_SendDelivery();
                ViewData["DeliverID"] = "";
                return View(sendDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_SendDelivery sendDelivery = new tk_SendDelivery();
            sendDelivery = FlowMeterMan.getNewSendDelivery(Info[0]);
            ViewData["DeliverID"] = sendDelivery.strDeliverID;

            return View(sendDelivery);
        }

        // 发货管理-撤销发货单
        public ActionResult ReSendDelivery()
        {
            var strRIDs = Request["RID"];
            string a_strErr = "";
            if (FlowMeterMan.ReSendDelivery(strRIDs, ref a_strErr) == true)
                return Json(new { success = "true", Msg = "撤销成功" });
            else
                return Json(new { success = "false", Msg = "撤销出错" + "/" + a_strErr });
        }



        #endregion

        #region ----[发货确认单查看并打印 完成]

        // 界面
        public ActionResult SendDeliveryDtail()
        {
            if (Request["Info"] == null)
            {
                tk_SendDelivery sendDelivery1 = new tk_SendDelivery();
                return View(sendDelivery1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_SendDelivery sendDelivery = new tk_SendDelivery();
            sendDelivery = FlowMeterMan.getNewSendDelivery2(Info[0]);

            return View(sendDelivery);
        }

        // 发货确认单查看-加载列表 
        public ActionResult GetSendDeliveryInfo()
        {
            string strDeliverID = Request["DeliverID"].ToString();
            string strErr = "";
            string strList = FlowMeterMan.GetSendDeliveryInfo(strDeliverID, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strList == "")
                    return Json(new { success = "false", Msg = "发货确认单明细加载失败" });
                else
                    return Json(new { success = "true", SendDeliveryInfo = strList });
            }
        }

        #endregion

        #region ----[任务管理-发货 完成]

        public ActionResult SendDelivery()
        {
            return View();
        }

        #region ----[确认发货 完成]

        // 加载打压完成状态的登记卡列表 
        public ActionResult LoadSendTaskList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string State = "10";// 打压完成状态

            string strWhere = "";
            if (State != "")
                strWhere += " and a.State ='" + State + "'";

            string Order = "";
            string OrderDate = "";
            if (Request["OrderDate"] != null)
                OrderDate = Request["OrderDate"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadCardListAll(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 确认发货-确定 
        public ActionResult CheckSendInfo()
        {
            string strErr = "";
            var RIDs = Request["CheckIDs"];
            var DeliverID = Request["DeliverID"];
            var UnitName = Request["UnitName"];
            var ReceiveName = Request["ReceiveName"];
            var ReceiveTel = Request["ReceiveTel"];
            var ReceiveDate = Request["ReceiveDate"];
            var ReceiveAddr = Request["ReceiveAddr"];
            var SendName = Request["SendName"];
            var SendTel = Request["SendTel"];
            var SendDate = Request["SendDate"];

            if (FlowMeterMan.CheckSendInfo(RIDs, DeliverID, UnitName, ReceiveName, ReceiveTel, ReceiveDate, ReceiveAddr,
                SendName, SendTel, SendDate, ref strErr) == true)
                return Json(new { success = "true", Msg = "确认发货成功" });
            else
                return Json(new { success = "false", Msg = "确认发货失败" + "/" + strErr });
        }

        #endregion

        #region  ----[出库操作 完成]

        // 确认发货-判断选中的数据是否已经进行过出库记录操作
        public ActionResult CheckStockOutInfo()
        {
            var IDs = Request["CheckIDs"];
            string strErr = "";
            if (FlowMeterMan.CheckStockOutInfo(IDs, ref strErr) == true)
                return Json(new { success = "true", Msg = "" });
            else
                return Json(new { success = "false", Msg = "所选数据已经进行过出库操作，请勿重复操作，重复编号为：" + "/" + strErr });
        }

        // 确认发货-出库 
        public ActionResult OutStockDetail()
        {
            if (Request["Info"] == null)
            {
                tk_StockOut stockOut1 = new tk_StockOut();
                return View(stockOut1);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_StockOut stockOut = new tk_StockOut();
            stockOut.strRID = Info[0];
            stockOut.strOutID = FlowMeterMan.GetNewOutID();
            Acc_Account acc = GAccount.GetAccountInfo();
            stockOut.strStockUnit = acc.UnitName;// 当前登陆单位
            stockOut.strOperateUser = acc.UserName;// 当前登陆用户 
            return View(stockOut);
        }

        // 确认发货-确定出库
        public ActionResult AddStockOutInfo(tk_StockOut stockOut)
        {
            if (ModelState.IsValid)
            {
                var RIDs = stockOut.strRID;
                stockOut.strOutID = FlowMeterMan.GetOutID();
                stockOut.strCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                stockOut.strStockUnit = acc.UnitName;// 出库单位
                stockOut.strCreateUser = acc.UserName;
                stockOut.strValidate = "v";

                string strErr = "";
                if (FlowMeterMan.AddStockOutInfo(stockOut, RIDs, ref strErr) == true)
                    return Json(new { success = "true", Msg = "出库信息提交成功" });
                else
                    return Json(new { success = "false", Msg = "出库信息提交出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }





        #endregion

        #endregion


        #endregion

        #region // 缴费管理 完成

        #region ----[缴费单查询 完成]

        public ActionResult PayMentManage()
        {
            return View();
        }

        // 缴费单查询-界面加载缴费记录列表
        public ActionResult LoadPayList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string PayUnit = Request["PayUnit"].ToString();
            string strWhere = "";
            if (PayUnit != "")
                strWhere += " and a.PayUnit='" + PayUnit + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadPayList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 缴费单查询-获取设备信息
        public ActionResult LoadModelList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string strWhere = "";
            string PayID = Request["PayID"].ToString();
            strWhere += " and PayID ='" + PayID + "'";
            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadModelList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            if (strjson != "")
            {
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
            }
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 缴费单查询-获取相关联的报价单
        public ActionResult LoadBJDList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string PayID = Request["PayID"].ToString();

            string strWhere = "";
            strWhere += " and PayID ='" + PayID + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadBJDList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            if (strjson != "")
            {
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
            }
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region ----[新增缴费记录 完成]

        // 可以一条缴费对应多个报价单 
        public ActionResult AddPayment()
        {
            tk_Payment payment = new tk_Payment();
            payment.StrPayID = FlowMeterMan.GetNewPayID();
            return View(payment);
        }

        // 新增缴费记录-加载为缴费完成状态的报价单列表 
        public ActionResult LoadBJDList1()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string strWhere = "";
            strWhere += " and a.State!='2'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadBJDList1(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            if (strjson != "")
            {
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
            }
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 新增缴费记录-确定新增缴费记录 
        public ActionResult AddNewPay(tk_Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.StrPayID = FlowMeterMan.GetPayID();// 缴费单号
                payment.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                payment.StrCreateUser = acc.UserName;
                payment.StrValidate = "v";

                string strErr = "";
                string Checks = Request["HCheck"].ToString();
                payment.StrQID = Checks;
                decimal LowPrice = Convert.ToDecimal(Request["Lowprice"]);// 欠费金额
                if (FlowMeterMan.AddNewPay(payment, Checks, LowPrice, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增缴费记录成功" });
                else
                    return Json(new { success = "false", Msg = "新增缴费记录出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }

        }


        #endregion

        #region ----[查看缴费单详细 完成]

        // 缴费管理-查看详细
        public ActionResult PaymentDetail()
        {
            string[] Info = Request["Info"].ToString().Split('@');
            tk_Payment payMent = new tk_Payment();
            payMent = FlowMeterMan.getNewPayMent(Info[0]);
            return View(payMent);
        }


        #endregion

        #region ----[修改缴费单 完成]

        // 修改缴费单 
        public ActionResult EditPayment()
        {
            if (Request["Info"] == null)
            {
                tk_Payment payMent1 = new tk_Payment();
                ViewData["TotalPriceC"] = "";
                ViewData["LowPrice"] = "";
                ViewData["QIDs"] = "";

                return View(payMent1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_Payment payMent = new tk_Payment();
            payMent = FlowMeterMan.getNewPayMent(Info[0]);
            string strList = FlowMeterMan.getBJInfo(Info[0]);
            string[] strInfos = strList.Split('#');
            ViewData["TotalPriceC"] = strInfos[0];
            if (strInfos.Length == 1)
                ViewData["LowPrice"] = "";
            else
                ViewData["LowPrice"] = strInfos[1];
            ViewData["QIDs"] = payMent.StrQID;

            return View(payMent);
        }

        // 修改缴费单-加载相关报价单列表 
        public ActionResult LoadBJDList2()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string strQIDs = Request["QIDs"].ToString();
            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadBJDList2(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strQIDs);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            if (strjson != "")
            {
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
            }
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        // 修改缴费单-确认修改
        public ActionResult UpdatePayment(tk_Payment payment)
        {
            if (ModelState.IsValid)
            {
                decimal LowPrice = Convert.ToDecimal(Request["Lowprice"]);// 欠费金额
                payment.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                payment.StrCreateUser = acc.UserName;
                payment.StrValidate = "v";

                string strErr = "";
                if (FlowMeterMan.UpdatePayment(payment, LowPrice, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改缴费记录成功" });
                else
                    return Json(new { success = "false", Msg = "修改缴费记录失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = "false", Msg = "数据验证不通过" });
            }
        }


        #endregion

        #region ----[撤销缴费记录单 完成]

        // 缴费管理-撤销界面
        public ActionResult DelPayment()
        {
            if (Request["Info"] == null)
            {
                tk_Payment payment1 = new tk_Payment();
                ViewData["QIDs"] = "";
                return View(payment1);
            }

            string[] Info = Request["Info"].ToString().Split('@');
            tk_Payment payment = new tk_Payment();
            payment = FlowMeterMan.getNewPayMent(Info[0]);
            ViewData["QIDs"] = payment.StrQID;

            return View(payment);
        }

        // 缴费管理-确定撤销 
        public ActionResult RePayment()
        {
            var strPayID = Request["PayID"];
            var strQIDs = Request["QIDs"];
            string a_strErr = "";
            if (FlowMeterMan.RePayment(strPayID, strQIDs, ref a_strErr) == true)
                return Json(new { success = "true", Msg = "撤销成功" });
            else
                return Json(new { success = "false", Msg = "撤销出错" + "/" + a_strErr });
        }

        #endregion


        #endregion



        #region   //  检测情况记录
        public ActionResult TestManager()
        {
            return View();
        }

        public ActionResult CheckData()
        {
            return View();
        }

        public ActionResult CheckDataIsThere()
        {
            var s = FlowMeterMan.CheckData(Request["RID"]);
            return Json(new { success = s });
        }
        public ActionResult CkeckDataDetail()
        {
            tk_CheckData data = new tk_CheckData();
            data = FlowMeterMan.getNewCheckData(Request["RepairID"]);

            return View(data);
        }
        //页面-添加检测表 
        public ActionResult AddCheckData()
        {

            return View();
        }

        //查询检测列表
        public ActionResult LoadCheckDataList()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";


                string strWhere = "";
                string RID = Request["RID"].ToString();
                string RepairType = Request["RepairType"].ToString();
                string RepairMethod = Request["RepairMethod"].ToString();
                string CheckDate = Request["CheckDate"].ToString();
                string CheckUser = Request["CheckUser"].ToString();
                string type = Request["type"].ToString();
                if (RID != "")
                    strWhere += " and RID=" + "'" + RID + "'";
                if (RepairType != "")
                    strWhere += " and RepairType=" + "'" + RepairType + "'";
                if (RepairMethod != "")
                    strWhere += " and RepairMethod=" + "'" + RepairMethod + "'";
                if (CheckUser != "")
                    strWhere += " and CheckUser=" + "'" + CheckUser + "'";
                if (CheckDate != "")
                    strWhere += " and CheckDate=" + "'" + CheckDate + "'";

                UIDataTable udtTask = new UIDataTable();
                if (type == "CardType2")
                {
                    udtTask = FlowMeterMan.LoadCheckDataList2(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                }
                else
                {
                    strWhere += " and RID in(select RID from tk_RepairCard where ModelType='" + type + "')";
                    udtTask = FlowMeterMan.LoadCheckDataList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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

        //页面-修改检测表 
        public ActionResult UpdateIncomingInspection()
        {
            if (Request["Info"] == null)
            {
                tk_CheckData dt = new tk_CheckData();
              
                return View(dt);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_CheckData data = new tk_CheckData();
            data = FlowMeterMan.getNewCheckData(Info[0]);

            ViewData["RID"] = data.StrRID;

            return View(data);
        }
        // 确认-新增检测
        public ActionResult InsertCheckData(tk_CheckData data)
        {
            if (ModelState.IsValid)
            {
                data.StrRepairID = "JC" + FlowMeterMan.GetKeyId("CheckData");
                data.StrCreateTime = DateTime.Now;
                data.StrRepairType = Request["StrRepairType"];
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;


                string strErr = "";
                if (FlowMeterMan.InsertCheckData(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "数据不通过" });
            }
        }
        //确认-修改检测表 
        public ActionResult UpdateIncomingInspectionSure(tk_CheckData data)
        {


            if (ModelState.IsValid)
            {
                data.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;

                string strErr = "";
                if (FlowMeterMan.UpdateCheckData(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "数据不通过" });
            }
        }
        //确认-删除检测表
        public ActionResult DeleteIncomingInspection()
        {
            var id = Request["id"];
            string strErr = "";
            if (FlowMeterMan.DeleteCheckData(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除成功" + "/" + strErr });
        }
        #endregion
        #region 检测情况记录（超声）

        //页面-添加检测表 
        public ActionResult AddCheckData2()
        {

            return View();
        }

        //页面-修改检测表 
        public ActionResult UpdateCheckData2()
        {
            if (Request["Info"] == null)
            {
                tk_CheckData2 dt = new tk_CheckData2();

                return View(dt);
            }
            tk_CheckData2 data = new tk_CheckData2();
            data = FlowMeterMan.getNewCheckData2(Request["Info"]);

            ViewData["RID"] = data.StrRID;

            return View(data);
        }
        //页面-检测表详情 
        public ActionResult CkeckData2Detail()
        {
            tk_CheckData2 data = new tk_CheckData2();
            data = FlowMeterMan.getNewCheckData2(Request["RepairID"]);

            return View(data);
        }
        //查询检测列表
        public ActionResult LoadCheckDataList2()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";


                string strWhere = "";
                string RID = Request["RID"].ToString();
                string RepairType = Request["RepairType"].ToString();
                string RepairMethod = Request["RepairMethod"].ToString();
                string CheckDate = Request["CheckDate"].ToString();
                string CheckUser = Request["CheckUser"].ToString();
                if (RID != "")
                    strWhere += " and RID=" + "'" + RID + "'";
                if (RepairType != "")
                    strWhere += " and RepairType=" + "'" + RepairType + "'";
                if (RepairMethod != "")
                    strWhere += " and RepairMethod=" + "'" + RepairMethod + "'";
                if (CheckUser != "")
                    strWhere += " and CheckUser=" + "'" + CheckUser + "'";
                if (CheckDate != "")
                    strWhere += " and CheckDate=" + "'" + CheckDate + "'";


                UIDataTable udtTask = new UIDataTable();
                udtTask = FlowMeterMan.LoadCheckDataList2(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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
        // 确认-新增检测
        public ActionResult InsertCheckData2(tk_CheckData2 data)
        {
            if (ModelState.IsValid)
            {
                data.StrRepairID = "JC" + FlowMeterMan.GetKeyId("CheckData");
                data.StrCreateTime = DateTime.Now;
                data.StrRepairType = Request["StrRepairType"];
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;


                string strErr = "";
                if (FlowMeterMan.InsertCheckData2(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "数据不通过" });
            }
        }
        //确认-修改检测表 
        public ActionResult UpdateCheckData2Sure(tk_CheckData2 data)
        {


            if (ModelState.IsValid)
            {
                data.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;

                string strErr = "";
                if (FlowMeterMan.UpdateCheckData2(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                //如果有错误，继续输入信息
                return Json(new { success = false, Msg = "数据不通过" });
            }
        }
        public ActionResult DeleteCheckData2()
        {
            var id = Request["id"];
            string strErr = "";
            if (FlowMeterMan.DeleteCheckData2(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除成功" + "/" + strErr });
        }
        #endregion
        #region  //清洗记录
        //页面-清洗记录首页
        public ActionResult CleanRepair()
        {
            return View();
        }
        //页面-添加清洗记录
        public ActionResult AddCleanRepair()
        {
            tk_CleanRepair data = new tk_CleanRepair();

            var pr = FlowMeterMan.getRepairInfo(Request["RID"], "开始清洗");
            data.StrCleanSDate = pr.StrOpTime;
            data.StrCleanUser = pr.StrOpUser;
            return View(data);
        }
        //页面-修改清洗记录
        public ActionResult UpdateCleanRepair()
        {
            if (Request["Info"] == null)
            {
                tk_CleanRepair dt = new tk_CleanRepair();

                return View(dt);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_CleanRepair data = new tk_CleanRepair();
            data = FlowMeterMan.getCleanRepair(Info[0]);
            return View(data);
        }
        //页面-清洗记录详情
        public ActionResult CleanRepairDetail()
        {
            tk_CleanRepair data = new tk_CleanRepair();
            data = FlowMeterMan.getCleanRepair(Request["CleanID"]);
            return View(data);
        }
        //页面-开始清洗
        public ActionResult StartCleanRepair()
        {

            return View();

        }
        //查询清洗记录
        public ActionResult LoadCleanRepairList()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";

                string RID = Request["RID"].ToString();
                string CleanUser = Request["CleanUser"].ToString();
                string CleanSDate = Request["CleanSDate"].ToString();
                string CleanEDate = Request["CleanEDate"].ToString();
                string type = Request["type"].ToString();
                string strWhere = "";
                if (RID != "")
                    strWhere += " and RID=" + "'" + RID + "'";
                if (CleanUser != "")
                    strWhere += " and CleanUser=" + "'" + CleanUser + "'";
                if (CleanSDate != "")
                    strWhere += " and CleanSDate>=" + "'" + CleanSDate + "'";
                if (CleanEDate != "")
                    strWhere += " and CleanEDate<=" + "'" + CleanEDate + "'";
                if (type == "CardType2")
                {
                    strWhere += " and RID in(select RID from tk_UTRepairCard where ModelType='" + type + "')";
                }
                else
                {
                    strWhere += " and RID in(select RID from tk_RepairCard where ModelType='" + type + "')";
                }

                UIDataTable udtTask = new UIDataTable();
                udtTask = FlowMeterMan.LoadCleanRepairList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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
        //加载清洗更换备件
        public ActionResult RepairChange()
        {

            var strRID = Request["CleanID"];// 清洗编号        
            string where = "CleanID='" + strRID + "'";
            DataTable dt = FlowMeterMan.RepairChange(where);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });

            string BakName = "";// 名称 
            string BakType = "";// 规格
            string BakNum = "";// 数量
            string Measure = "";// 单位

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BakName += dt.Rows[i]["DeviceName"].ToString() + ",";
                BakType += dt.Rows[i]["DeviceType"].ToString() + ",";
                BakNum += dt.Rows[i]["Num"].ToString() + ",";
                Measure += dt.Rows[i]["Measure"].ToString() + ",";
            }
            BakName = BakName.TrimEnd(',');
            BakType = BakType.TrimEnd(',');
            BakNum = BakNum.TrimEnd(',');
            Measure = Measure.TrimEnd(',');

            return Json(new
            {
                success = "true",
                BakName = BakName,
                BakType = BakType,
                BakNum = BakNum,
                Measure = Measure
            });

        }
        //确认-清洗开始
        public ActionResult StartCleanRepairSure(tk_ProRecord pro)
        {
            if (ModelState.IsValid)
            {
                Acc_Account acc = GAccount.GetAccountInfo();
                pro.StrRID = Request["StrRID"];
                pro.StrOpType = "开始清洗";
                pro.StrCreateTime = DateTime.Now;
                pro.StrCreateUser = acc.UserName;

                if (FlowMeterMan.StartCleanRepair(pro) == true)
                    return Json(new { success = "true", Msg = "开始清洗" });
                else
                    return Json(new { success = "false", Msg = "开始出错" });
            }
            else
            {
                return Json(new { success = false, Msg = "数据不通过" });
            }

        }

        //确认-新增清洗记录
        public ActionResult InsertCleanRepair(tk_CleanRepair data)
        {
            if (ModelState.IsValid)
            {
                data.StrCleanID = "QX" + FlowMeterMan.GetKeyId("ClRepair");

                data.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;
                data.Strvalidate = "v";

                string strErr = "";

                if (FlowMeterMan.InsertCleanRepair(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });

            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }

        }
        //确认-修改清洗记录
        public ActionResult UpdateCleanRepairSure(tk_CleanRepair data)
        {
            if (ModelState.IsValid)
            {
                data.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                data.StrCreateUser = acc.UserName;
                data.Strvalidate = "v";
                string strErr = "";

                if (FlowMeterMan.UpdateCleanRepair(data, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }

        //确认-删除清洗记录
        public ActionResult DeleteCleanRepair()
        {
            var id = Request["id"];
            string strErr = "";
            if (FlowMeterMan.DeleteCleanRepair(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除成功" + "/" + strErr });
        }
        #endregion

        #region 维修记录
        //页面-维修记录
        public ActionResult RepairInfo()
        {
            return View();
        }
        //页面-开始维修
        public ActionResult StartRepair()
        {
            return View();
        }
        public ActionResult RepairInfoIsThere()
        {
            tk_RepairInfo data = new tk_RepairInfo();

            var pr = FlowMeterMan.getRepairInfo2(Request["RID"]);
            if (pr.StrRepairID != null && pr.StrRepairID != "")
            {
                return Json(new
                {
                    success = "true",
                    id = pr.StrRepairID,

                });
            }
            return Json(new { success = "false" });
        }
        public ActionResult AddRepairInfo()
        {
            tk_RepairInfo data = new tk_RepairInfo();

            var pr = FlowMeterMan.getRepairInfo(Request["RID"], "开始维修");
            data.StrRepairSDate = pr.StrOpTime;
            data.StrRepairUser = pr.StrOpUser;
            return View(data); ;
        }

        //页面-修改维修记录
        public ActionResult UpdateRepairInfo()
        {
            if (Request["Info"] == null)
            {
                tk_RepairInfo dt = new tk_RepairInfo();

                return View(dt);
            }
            string[] Info = Request["Info"].ToString().Split('@');
            tk_RepairInfo data = new tk_RepairInfo();
            data = FlowMeterMan.getRepairInfo(Info[0]);
            return View(data);
        }

        //页面-维修记录详情
        public ActionResult RepairInfoDetail()
        {

            tk_RepairInfo data = new tk_RepairInfo();
            data = FlowMeterMan.getRepairInfo(Request["RepairID"]);
            return View(data);
        }
        //查询维修记录
        public ActionResult LoadRepairInfoList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            else strCurPage = "1";
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";


            string strWhere = "";

            string RID = Request["RID"].ToString();
            string RepairUser = Request["RepairUser"].ToString();
            string RepairSDate = Request["RepairSDate"].ToString();
            string RepairEDate = Request["RepairEDate"].ToString();
            string type = Request["type"].ToString();
            if (RID != "")
                strWhere += " and RID=" + "'" + RID + "'";
            if (RepairUser != "")
                strWhere += " and RepairUser=" + "'" + RepairUser + "'";
            if (RepairSDate != "")
                strWhere += " and RepairSDate>=" + "'" + RepairSDate + "'";
            if (RepairEDate != "")
                strWhere += " and RepairEDate<=" + "'" + RepairEDate + "'";
            if (type == "CardType2")
            {
                strWhere += " and RID in(select RID from tk_UTRepairCard where ModelType='" + type + "')";
            }
            else
            {
                strWhere += " and RID in(select RID from tk_RepairCard where ModelType='" + type + "')";
            }
            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadRepairInfoList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        //加载维修更换备件
        public ActionResult RepairDevice()
        {

            var strRID = Request["RID"];// 清洗编号       
            string where = "  RID='" + strRID + "'";
            DataTable dt = FlowMeterMan.RepairDevice(where);
            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });

            string BakName = "";// 名称 
            string BakType = "";// 规格
            string BakNum = "";// 数量
            string Measure = "";// 单位

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BakName += dt.Rows[i]["DeviceName"].ToString() + ",";
                BakType += dt.Rows[i]["DeviceType"].ToString() + ",";
                BakNum += dt.Rows[i]["Num"].ToString() + ",";
                Measure += dt.Rows[i]["Measure"].ToString() + ",";
            }
            BakName = BakName.Substring(0, BakName.Length - 1);
            BakType = BakType.Substring(0, BakType.Length - 1);
            BakNum = BakNum.Substring(0, BakNum.Length - 1);
            Measure = Measure.Substring(0, Measure.Length - 1);

            return Json(new
            {
                success = "true",
                BakName = BakName,
                BakType = BakType,
                BakNum = BakNum,
                Measure = Measure
            });

        }
        //确认-开始维修
        public ActionResult StartRepairSure(tk_ProRecord pro)
        {
            if (ModelState.IsValid)
            {
                Acc_Account acc = GAccount.GetAccountInfo();
                pro.StrRID = Request["StrRID"];
                pro.StrOpType = "开始维修";
                pro.StrCreateTime = DateTime.Now;
                pro.StrCreateUser = acc.UserName;

                if (FlowMeterMan.StartRepair(pro) == true)
                    return Json(new { success = "true", Msg = "已开始维修" });
                else
                    return Json(new { success = "false", Msg = "开始出错" });
            }
            else
            {
                return Json(new { success = false, Msg = "数据验证不通过" });
            }

        }
        // 确认-新增维修记录
        public ActionResult InsertRepairInfo(tk_RepairInfo rf)
        {
            if (ModelState.IsValid)
            {
                rf.StrRepairID = "WX" + FlowMeterMan.GetKeyId("RepairInfo");
                rf.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                rf.StrCreateUser = acc.UserName;
                rf.Strvalidate = "v";

                string strErr = "";

                string BakName = Request["Name"];
                string BakType = Request["Type"];
                string Measure = Request["M"];
                string BakNum = Request["Num"];
                if (FlowMeterMan.InsertRepairInfo(rf, BakName, BakType, Measure, BakNum, ref strErr) == true)
                    return Json(new { success = "true", Msg = "新增成功" });
                else
                    return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });
            }
            else
            {
                return Json(new { success = false, Msg = "数据验证不通过" });
            }

        }
        //确认-修改维修记录
        public ActionResult UpdateRepairInfoSure(tk_RepairInfo rf)
        {
            if (ModelState.IsValid)
            {
                rf.StrCreateTime = DateTime.Now;
                Acc_Account acc = GAccount.GetAccountInfo();
                rf.StrCreateUser = acc.UserName;
                rf.Strvalidate = "v";
                rf.StrRepairNum = (Convert.ToInt32(rf.StrRepairNum) + 1).ToString();
                string strErr = "";

                string BakName = Request["Name"];
                string BakType = Request["Type"];
                string Measure = Request["M"];
                string BakNum = Request["Num"];
                if (FlowMeterMan.UpdateRepairInfo(rf, BakName, BakType, Measure, BakNum, ref strErr) == true)
                    return Json(new { success = "true", Msg = "修改成功" });
                else
                    return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });
            }
            else
            {
                return Json(new { success = false, Msg = "数据验证不通过" });
            }
        }
        //确认-删除清洗记录
        public ActionResult DeleteRepairInfo()
        {
            var id = Request["id"];
            string strErr = "";
            if (FlowMeterMan.DeleteRepairInfo(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除成功" + "/" + strErr });
        }
        #endregion

        #region 任务管理
        //页面-任务管理首页
        public ActionResult TaskManagement()
        {
            return View();
        }

        //加载清洗维修更换备件
        public ActionResult ServicingAccessory()
        {
            var strRID = Request["RID"];
            string where = " RID='" + strRID + "'";

            DataTable dt = FlowMeterMan.RepairDevice(where);//维修

            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });

            string BakName = "";// 名称 
            string BakType = "";// 规格
            string BakNum = "";// 数量
            string BakMeasure = "";
            string UnitPrice = "";// 单价
            string TotalPrice = "";// 优惠价
            string Comments = "";// 备注
            string type = "";
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BakName += dt.Rows[i]["DeviceName"].ToString() + ",";
                    BakType += dt.Rows[i]["DeviceType"].ToString() + ",";
                    BakNum += dt.Rows[i]["Num"].ToString() + ",";
                    BakMeasure += dt.Rows[i]["Measure"].ToString() + ",";
                    UnitPrice += dt.Rows[i]["UnitPrice"].ToString() + ",";
                    TotalPrice += dt.Rows[i]["TotalPrice"].ToString() + ",";
                    Comments += dt.Rows[i]["Comments"].ToString() + "@";
                    type += "维修,";
                }
            }

            BakName = BakName.Substring(0,BakName.Length-1);
            BakType = BakType.Substring(0, BakType.Length - 1);
            BakNum = BakNum.Substring(0, BakNum.Length - 1);
            BakMeasure = BakMeasure.Substring(0, BakMeasure.Length - 1);

            UnitPrice = UnitPrice.Substring(0, UnitPrice.Length - 1);
            TotalPrice = TotalPrice.Substring(0, TotalPrice.Length - 1);
            Comments = Comments.Substring(0, Comments.Length - 1);
            type = type.Substring(0, type.Length - 1);
            return Json(new
            {
                success = "true",
                BakName = BakName,
                BakType = BakType,
                BakNum = BakNum,
                BakMeasure = BakMeasure,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice,
                Comments = Comments,
                type = type
            });

        }

        // 根究状态判断是否可以进行当前操作 
        public ActionResult Operate()
        {
            string strErr = Request["str"];
            string type = Request["type"];
            string rid = Request["RID"];
            int p = Convert.ToInt32(Request["P"].ToString());
            if (FlowMeterMan.Operate(p, rid, type, ref  strErr) == true)
                return Json(new { success = "true", Msg = "" });
            else
                return Json(new { success = "false", Msg = strErr });

        }
        //加载过程记录
        public ActionResult LoadProcedureList()
        {

            string RID = Request["RID"].ToString();
            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowMeterMan.LoadProcedureList(RID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region 报价

        //页面-维修确认单
        public ActionResult Servicing()
        {


            return View();

        }
        //页面-修改维修确认单
        public ActionResult UpdateServicing()
        {


            return View();

        }
        //页面-添加维修确认单
        public ActionResult AddServicing()
        {
            return View();
        }
        public ActionResult ServicingDetail()
        {

            RepairCard repairCard = new RepairCard();
            repairCard = FlowMeterMan.getNewRepairCard(Request["RID"]);

            string strModel = FlowMeterMan.getModels(Request["RID"]);
            ViewData["Model"] = strModel.Split(',')[0].ToString();
            ViewData["X_Model"] = strModel.Split(',')[1].ToString();
            ViewData["RID"] = repairCard.strRID;
            ViewData["State"] = repairCard.strState;
            return View(repairCard);
        }
        //页面-报价单首页
        public ActionResult Quotation()
        {
            return View();
        }
        //页面-添加报价单
        public ActionResult AddQuotation()
        {
            return View();
        }
        //页面-修改报价单
        public ActionResult UpdateQuotation()
        {

            tk_Quotation data = new tk_Quotation();
            data = FlowMeterMan.getQuotation(Request["QID"]);
            return View(data);
        }
        //页面-报价单详情
        public ActionResult QuotationDetail()
        {
            //string QID = Request["QID"].ToString();
            //tk_Quotation data = new tk_Quotation();
            //data = FlowMeterMan.getQuotation(QID);
            //ViewData["Hide"] = QID;
            return View();
        }
        //页面-报价汇总
        public ActionResult QuotationSummary()
        {

            return View();
        }
        //页面-确认单汇总
        public ActionResult ServicingSummary()
        {

            return View();
        }
        //查询报价单详情
        public ActionResult LoadQuotationList()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";

                string RID = Request["RID"].ToString();


                string strWhere = "";
                if (RID != "")
                    strWhere += " and RID=" + "'" + RID + "'";

                UIDataTable udtTask = new UIDataTable();
                udtTask = FlowMeterMan.LoadQuotationList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
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


        //查询报价单
        public ActionResult LoadQuotationList2()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";

                string RID = Request["RID"].ToString();


                string ModelType = "";
                string RepairID = Request["RepairID"].ToString();
                string CustomerName = Request["CustomerName"].ToString();
                string CustomerAddr = Request["CustomerAddr"].ToString();
                string MeterID = Request["MeterID"].ToString();
                string MeterName = Request["MeterName"].ToString();
                string Model = Request["Model"].ToString();
                string SS_Date = Request["SS_Date"].ToString();
                string ES_Date = Request["ES_Date"].ToString();
                string State = Request["State"].ToString();

                string SubUnit = Request["SubUnit"].ToString();
                string strWhere = "";
                if (RepairID != "")
                    strWhere += " and a.RepairID like '%" + RepairID + "%'";
                if (CustomerName != "")
                    strWhere += " and a.CustomerName like '%" + CustomerName + "%'";
                if (CustomerAddr != "")
                    strWhere += " and a.CustomerAddr like '%" + CustomerAddr + "%'";
                if (MeterID != "")
                    strWhere += " and a.MeterID like '%" + MeterID + "%'";
                if (MeterName != "")
                    strWhere += " and a.MeterName like '%" + MeterName + "%'";
                if (Model != "")
                    strWhere += " and a.Model ='" + Model + "'";
                if (SS_Date != "")
                    strWhere += " and a.S_Date >='" + SS_Date + "'";
                if (ES_Date != "")
                    strWhere += " and a.S_Date <='" + ES_Date + "'";
                if (State != "")
                    strWhere += " and a.State ='" + State + "'";

                if (SubUnit != "")
                    strWhere += " and a.SubUnit like'%" + SubUnit + "%'";


                if (Request["CardType"] != null)
                    ModelType = Request["CardType"].ToString();



                if (RID != "")
                {
                    if (RID.Contains(','))
                    {
                        RID = RID.Substring(0, RID.Length - 1);
                        var RIDlist = RID.Split(',');
                        var str = "";
                        for (int i = 0; i < RIDlist.Length; i++)
                        {
                            if (i == RIDlist.Length - 1)
                                str += "'" + RIDlist[i] + "'";
                            else
                                str += "'" + RIDlist[i] + "',";
                        }
                        strWhere += "  and a.RID in (" + str + ")";
                    }
                    else
                    {
                        strWhere += "  and a.RID=" + "'" + RID + "'";
                    }
                }
                UIDataTable udtTask = new UIDataTable();
                if (ModelType == "CardType2")
                {
                    udtTask = FlowMeterMan.GetGenList2(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                    for (int i = 0; i < udtTask.DtData.Rows.Count; i++)
                    {

                        string strModel = FlowMeterMan.getModelsUT(udtTask.DtData.Rows[i]["RID"].ToString());
                        udtTask.DtData.Rows[i]["Model"] = strModel.Split(',')[0].ToString();


                    }
                }
                else
                {
                    strWhere += " and a.ModelType='" + ModelType + "'";
                    udtTask = FlowMeterMan.GetGenList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
                    for (int i = 0; i < udtTask.DtData.Rows.Count; i++)
                    {

                        string strModel = FlowMeterMan.getModels(udtTask.DtData.Rows[i]["RID"].ToString());
                        udtTask.DtData.Rows[i]["Model"] = strModel.Split(',')[0].ToString();


                    }
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

        public ActionResult GetGenQtnList()
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                else strCurPage = "1";
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "15";

                string RID = Request["RID"].ToString();
                string Type = Request["Type"].ToString();
                string CardType = Request["CardType"].ToString();
                string strWhere = "";
                if (RID != "")
                    strWhere += "  and a.RID=" + "'" + RID + "'";

                if (Type != "")
                    strWhere += "  and a.State=" + "'" + Type + "'";
                if (CardType == "CardType2")
                {
                    strWhere += " and a.RID in (select RID from tk_UTRepairCard)";
                }
                else if (CardType == "")
                    strWhere += " and a.RID in (select RID from tk_RepairCard) ";
                else
                {
                    strWhere += " and a.RID in (select RID from tk_RepairCard where ModelType='" + CardType + "')";
                }
                UIDataTable udtTask = new UIDataTable();
                udtTask = FlowMeterMan.GetGenQtnList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);

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
        //确认-新增报价单
        public ActionResult InsertQuotation()
        {




            string strErr = "";
            string RID = Request["rid"];
            string Type = Request["Type"];
            string UnitPrice = Request["p"];

            string DeviceName = Request["strDeviceName"];
            string DeviceType = Request["strDeviceType"];
            string Measure = Request["strMeasure"];
            string Num = Request["strNum"];
            string UnitPrice2 = Request["strUnitPrice"];
            string TotalPrice = Request["strTotalPrice"];
            string Comments = Request["strComments"];
            if (FlowMeterMan.InsertQuotation(RID, Type, UnitPrice, DeviceName, DeviceType, Num, UnitPrice2, TotalPrice, Comments, Measure, ref strErr) == true)
                return Json(new { success = "true", Msg = "新增成功" });
            else
                return Json(new { success = "false", Msg = "新增出错" + "/" + strErr });



        }
        //确认-修改报价单
        public ActionResult UpdateQuotationSure(tk_Quotation qt)
        {
            string strErr = "";
            string RID = Request["rid"];
            string Type = Request["Type"];
            string UnitPrice = Request["p"];

            string DeviceName = Request["strDeviceName"];
            string DeviceType = Request["strDeviceType"];
            string Measure = Request["strMeasure"];
            string Num = Request["strNum"];
            string UnitPrice2 = Request["strUnitPrice"];
            string TotalPrice = Request["strTotalPrice"];
            string Comments = Request["Comments"];


            if (FlowMeterMan.UpdateQuotation(RID, Type, UnitPrice, DeviceName, DeviceType, Num, UnitPrice2, TotalPrice, Comments, Measure, ref strErr) == true)
                return Json(new { success = "true", Msg = "修改成功" });
            else
                return Json(new { success = "false", Msg = "修改失败" + "/" + strErr });


        }
        public ActionResult DeleteQuotation(tk_Quotation qt)
        {
            var id = Request["id"];
            string strErr = "";
            if (FlowMeterMan.DeleteQuotation(id, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除成功" + "/" + strErr });
        }

        public ActionResult GetComponent()
        {
            string where = "";
            var name = Request["name"];
            if (name != "" && name != null) {
                where = name;
            }
            DataTable dt = FlowMeterMan.GetComponent(where);

            if (dt == null || dt.Rows.Count == 0)
                return Json(new { success = "false", Msg = "无数据" });
            else
            {
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"rows\": ";
                jsonData += strjson + "}";
                return Json(new
                {
                    success = "true",
                    data = jsonData


                });
            }




        }

        public ActionResult Component()
        {
            return View();
        }
        #endregion

        #region 统计分析
        //页面-
        public ActionResult SearchService()
        {
            return View();
        }
        //页面-清洗维修记录明细查询
        public ActionResult SearchSchedule()
        {
            return View();

        }

        //页面-进度明细汇总
        public ActionResult ScheduleDetail()
        {
            return View();
        }

        //页面-检测表单
        public ActionResult CheckDataSummary()
        {
            return View();
        }
        //页面-检测表单（超声检测）
        public ActionResult CheckData2Summary()
        {
            return View();
        }
        //页面-维修记录表单
        public ActionResult RepairInfoSummary()
        {
            return View();
        }
        public ActionResult Accessory()
        {
            return View();
        }
        //加载进度明细汇总
        public ActionResult LoadScheduleDetail()
        {
            string type = Request["type"].ToString();


            string strWhere = "";
            if (type != "")
                strWhere = " and ModelType='" + type + "'";
            DataTable dt = FlowMeterMan.ScheduleDetail(strWhere, type);
            if (dt.Rows.Count > 0)
            {
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"success\":true,  \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = "false", Msg = "无数据" });
        }


        //总表
        public ActionResult LoadScheduleSummary()
        {
            string ModelType = Request["CardType"].ToString(); ;
            string RepairID = Request["RepairID"].ToString();
            string CustomerName = Request["CustomerName"].ToString();
            string CustomerAddr = Request["CustomerAddr"].ToString();
            string MeterID = Request["MeterID"].ToString();
            string MeterName = Request["MeterName"].ToString();
            string Model = Request["Model"].ToString();
            string SS_Date = Request["SS_Date"].ToString();
            string ES_Date = Request["ES_Date"].ToString();
            string State = Request["State"].ToString();


            string strWhere = "";
            if (ModelType != "")
                strWhere += " and a.ModelType like '%" + ModelType + "%'";
            if (RepairID != "")
                strWhere += " and a.RepairID like '%" + RepairID + "%'";
            if (CustomerName != "")
                strWhere += " and a.CustomerName like '%" + CustomerName + "%'";
            if (CustomerAddr != "")
                strWhere += " and a.CustomerAddr like '%" + CustomerAddr + "%'";
            if (MeterID != "")
                strWhere += " and a.MeterID like '%" + MeterID + "%'";
            if (MeterName != "")
                strWhere += " and a.MeterName like '%" + MeterName + "%'";
            if (Model != "")
                strWhere += " and a.Model ='" + Model + "'";
            if (SS_Date != "")
                strWhere += " and a.S_Date >='" + SS_Date + "'";
            if (ES_Date != "")
                strWhere += " and a.S_Date <='" + ES_Date + "'";
            if (State != "")
                strWhere += " and a.State ='" + State + "'";
            DataTable dt = FlowMeterMan.LoadScheduleSummary(strWhere, ModelType);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strModel = FlowMeterMan.getModels2(dt.Rows[i]["RID"].ToString(), ModelType);
                    dt.Rows[i]["Model"] = strModel.Split(',')[0].ToString();
                    if (ModelType != "CardType2")
                        dt.Rows[i]["X_Model"] = strModel.Split(',')[1].ToString();
                    dt.Rows[i]["ModelType"] = strModel.Split(',')[2].ToString();
                }
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"success\":true,  \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = "false", Msg = "无数据" });
        }
        //方法-加载检测表单
        public ActionResult LoadCheckDataSummary()
        {
            var rid = Request["RID"];
            var where = "";
            if (rid != "")
                where = "a.RID='" + rid + "'";
            DataTable dt = FlowMeterMan.LoadCheckDataSummary(where);
            if (dt != null)
            {
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"success\":true,  \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = "false", Msg = "无数据" });
        }
        //方法-加载检测表单（超声检测）
        public ActionResult LoadCheckData2Summary()
        {
            var rid = Request["RID"];
            var where = "";
            if (rid != "")
                where = "a.RID='" + rid + "'";
            DataTable dt = FlowMeterMan.LoadCheckData2Summary(where);
            if (dt != null)
            {
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"success\":true,  \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = "false", Msg = "无数据" });
        }
        //方法-加载维修记录
        public ActionResult LoadRepairInfoSummary()
        {
            var rid = Request["RID"];
            var where = "";
            if (rid != "")
                where = "a.RID='" + rid + "'";
            var type = Request["type"];
            DataTable dt = FlowMeterMan.LoadRepairInfoSummary(where, type);

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strModel = FlowMeterMan.getModels2(dt.Rows[i]["RID"].ToString(), type);
                    dt.Rows[i]["Model"] = strModel.Split(',')[0].ToString();
                    dt.Rows[i]["ModelType"] = strModel.Split(',')[2].ToString();
                }
                string strjson = GFun.Dt2Json("", dt);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"success\":true,  \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { success = "false", Msg = "无数据" });

        }


        public ActionResult ExportDataTableToExcelServicing(tk_CardSearch c)
        {




            string strWhere = "";



            var RID = c.RID.Substring(0, c.RID.Length - 1);
            var RIDlist = RID.Split(',');
            var str = "";
            for (int i = 0; i < RIDlist.Length; i++)
            {
                if (i == RIDlist.Length - 1)
                    str += "'" + RIDlist[i] + "'";
                else
                    str += "'" + RIDlist[i] + "',";
            }
            strWhere += "  and a.RID in (" + str + ")";



            DataTable data = new DataTable();
            if (c.ModelType == "CardType2")
            {
                data = FlowMeterMan.GetGenList2(15, 0, strWhere).DtData; ;
                for (int i = 0; i < data.Rows.Count; i++)
                {

                    string strModel = FlowMeterMan.getModelsUT(data.Rows[i]["RID"].ToString());
                    data.Rows[i]["Model"] = strModel.Split(',')[0].ToString();


                }
            }
            else
            {
                strWhere += " and a.ModelType='" + c.ModelType + "'";
                data = FlowMeterMan.GetGenList(15, 0, strWhere).DtData;
                for (int i = 0; i < data.Rows.Count; i++)
                {

                    string strModel = FlowMeterMan.getModels(data.Rows[i]["RID"].ToString());
                    data.Rows[i]["Model"] = strModel.Split(',')[0].ToString();


                }
            }
            if (data != null)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {

                    string strModel = FlowMeterMan.getModels(data.Rows[i]["RID"].ToString());
                    data.Rows[i]["Model"] = strModel.Split(',')[0].ToString();


                }
                string strCols = "RowNumber,Model,Caliber,PreUnit,DeviceName,UnitPrice,Num,Measure,TotalPrice2,qx,ccbd,jc,TotalPrice";


                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelServicing(data, "零备件费用", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "零备件费用.xls");
            }
            else
                return null;
        }

        public ActionResult ExportDataTableToExcelSchedule(tk_CardSearch c)
        {




            string strWhere = "";


            if (c.ModelType != "" && c.ModelType != null)
                strWhere = " and ModelType='" + c.ModelType + "'";
            DataTable dt = FlowMeterMan.ScheduleDetail(strWhere, c.ModelType);

            if (dt != null)
            {
                var tit = "";
                switch (c.ModelType)
                {
                    case "CardType1":

                        tit = "涡轮";
                        break;
                    case "CardType2":

                        tit = "涡轮";
                        break;
                    case "超声波":

                        tit = "腰轮";
                        break;
                    default:


                        tit = "总表";

                        break;
                }
                string strCols = "口径,清洗完成,维修完成,完成总数,待初测,待清洗,清洗中,待维修,维修中,待检测,待打压,正在进行总数,周转表,无法维修,总数,待出厂,已出厂";


                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelSchedule(dt, tit, strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", tit + ".xls");
            }
            else
                return null;
        }



        public ActionResult ExportDataTableToExcelCheckData(tk_CheckData c)
        {




            string strWhere = "";


            if (c.StrRID != "")
                strWhere = "a.RID='" + c.StrRID + "'";
            DataTable dt = FlowMeterMan.LoadCheckDataSummary(strWhere);
            if (dt != null)
            {

                string strCols = "MeterID,M,CertifID,Remark,Qmin,0.1Qmax,0.2Qmax,0.25Qmax,0.4Qmax,0.7Qmax,Qmax,Avg_Qmin,Avg_0.1Qmax,Avg_0.2Qmax,Avg_0.25Qmax,Avg_0.4Qmax,Avg_0.7Qmax,";

                strCols += "Avg_Qmax,Repeat_Qmin,Repeat_0.1Qmax,Repeat_0.2Qmax,Repeat_0.25Qmax,Repeat_0.4Qmax,Repeat_0.7Qmax,Repeat_Qmax,Ratio,q1,q2,PDeviation,Oratio";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelCheckData(dt, "检测", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "检测.xls");
            }
            else
                return null;
        }
        public ActionResult ExportDataTableToExcelCheckData2(tk_CheckData2 c)
        {




            string strWhere = "";


            if (c.StrRID != "")
                strWhere = "a.RID='" + c.StrRID + "'";
            DataTable dt = FlowMeterMan.LoadCheckData2Summary(strWhere);
            if (dt != null)
            {

                string strCols = "MeterID,M,CertifID,Remark,测试条件A,A1path,A2path,A3path,A4path,A5path,A6path,AAverage,AAheory,";
                strCols += "误差A,A1ER,A2ER,A3ER,A4ER,A5ER,A6ER,AVER,AHVER@";
                strCols += "MeterID,M,CertifID,Remark,测试条件B,B1path,B2path,B3path,B4path,B5path,B6path,BAverage,BAheory,";
                strCols += "误差B,B1ER,B2ER,B3ER,B4ER,B5ER,B6ER,BVER,BHVER";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelCheckData2(dt, "检测", strCols.Split('@'));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "检测.xls");
            }
            else
                return null;
        }

        public ActionResult ExportDataTableToExcelRepairInfo(RepairCard c)
        {




            string strWhere = "";


            if (c.strRID != "")
                strWhere = "a.RID='" + c.strRID + "'";
            DataTable dt = FlowMeterMan.LoadRepairInfoSummary(strWhere, c.strModelType);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strModel = FlowMeterMan.getModels2(dt.Rows[i]["RID"].ToString(), c.strModelType);
                    dt.Rows[i]["Model"] = strModel.Split(',')[0].ToString();
                    dt.Rows[i]["ModelType"] = strModel.Split(',')[2].ToString();
                }
                string strCols = "MeterID,RepairSDate,RepairEDate,M,ModelType,Manufacturer,Model,Breakdown,RepairContent,DeviceName,DeviceType,Measure,";

                strCols += "Num,RepairNum,AdjustPre,AdjustNow,RepairUser,RepairResult,Remark";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelRepairInfo(dt, "维修记录", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "维修记录.xls");
            }
            else
                return null;
        }


        public ActionResult ExportDataTableToExcelSummary(tk_CardSearch c)
        {




            string strWhere = "";


            var RID = c.RID.Substring(0, c.RID.Length - 1);
            var RIDlist = RID.Split(',');
            var str = "";
            for (int i = 0; i < RIDlist.Length; i++)
            {
                if (i == RIDlist.Length - 1)
                    str += "'" + RIDlist[i] + "'";
                else
                    str += "'" + RIDlist[i] + "',";
            }
            strWhere += "  and a.RID in (" + str + ")";

            DataTable dt = FlowMeterMan.LoadScheduleSummary(strWhere, c.ModelType);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strModel = FlowMeterMan.getModels2(dt.Rows[i]["RID"].ToString(), c.ModelType);
                    dt.Rows[i]["Model"] = strModel.Split(',')[0].ToString();
                    if (c.ModelType != "CardType2")
                        dt.Rows[i]["X_Model"] = strModel.Split(',')[1].ToString();
                    dt.Rows[i]["ModelType"] = strModel.Split(',')[2].ToString();
                }

                string strCols = "RepairID,RowNumber,RepairID,y,m,d,ModelType,Manufacturer,Model,CertifID,FactoryDate,Caliber,Pressure,FlowRange,Precision,PreUnit,NewUnit,";

                strCols += "归属,附件,X_Manufacturer,X_Model,X_ID,X_FactoryDate,X_PreUnit,X_NewUnit,RecordNum,X_Standard,X_Pressure,X_Temperature,检测结果,初测状态,次数,";
                strCols += "维修记录,清洗维修状态,出厂前检测状态,打压状态,铅封号,完成情况,零件,说明,CustomerName,S_Name,S_Tel,ReceiveUser,G_Name,G_Tel,G_Date,Text,地点,情况";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelSummary(dt, "检测", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "检测.xls");
            }
            else
                return null;
        }

    }
}
        #endregion


