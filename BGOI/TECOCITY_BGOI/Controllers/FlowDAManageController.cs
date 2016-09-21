using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI.Controllers
{
    public class FlowDAManageController : Controller
    {
        //
        // GET: /FlowDAManage/ 数据分析 /

        public ActionResult Index()
        {
            return View();
        }

        #region //检测分析

        // 检测对比曲线图
        public ActionResult DetectionCurve()
        {
            return View();
        }

        // 检测对比图-加载检测表列表
        public ActionResult LoadDetecList()
        {
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";

            string RepairMethod = Request["RepairMethod"].ToString();
            string Caliber = Request["Caliber"].ToString();
            string CustomerName = Request["CustomerName"].ToString();
            string Model = Request["Model"].ToString();
            string strWhere = "";
            if (RepairMethod != "")
                strWhere += " and b.RepairMethod ='" + RepairMethod + "'";
            if (Caliber != "")
                strWhere += " and b.Caliber ='" + Caliber + "'";
            if (CustomerName != "")
                strWhere += " and b.CustomerName like '%" + CustomerName + "%'";
            if (Model != "")
                strWhere += " and b.Model ='" + Model + "'";

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadDetecList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // 曲线图界面
        public ActionResult ShowImgs()
        {
            string Info = Request["Info"].ToString();
            ViewData["Meters"] = Info;

            return View();
        }

        // 获取表格数据
        public ActionResult GetMeterImg()
        {
            string Infos = Request["Meters"].ToString();
            DataSet ds = FlowDAMan.getMeterImg(Infos);
            StringBuilder sb = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                string arrFlow = "";
                string arrpreD = "";
                string arraftD = "";
                string arrType = "";
                sb.Append("<table><tr>");

                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                {
                    string flow = ds.Tables[0].Rows[row][0].ToString();
                    string preData = ds.Tables[0].Rows[row][1].ToString();
                    string aftData = ds.Tables[0].Rows[row][2].ToString();
                    string types = ds.Tables[0].Rows[row][3].ToString();

                    #region // --表格

                    if (row == 0)
                        sb.Append("<td><table class='tabInfo' id='T0' style='width:80%; margin-top:5px; text-align:\"center\"' align='center'>");

                    if (row == 0 && (types.Split('-')[1] == "高频"))
                    {
                        arrType += "仪表系数";
                        sb.Append("<tr><td style='width:10%;'></td><td style='width:30%;'>维修前仪表系数</td><td style='width:30%;'>维修后仪表系数</td></tr>");
                    }
                    else if (row == 0 && (types.Split('-')[1] == "低频"))
                    {
                        arrType += "示值误差";
                        sb.Append("<tr><td style='width:10%;'>表</td><td style='width:30%;'>维修前示值误差</td><td style='width:30%;'>维修后示值误差</td></tr>");
                    }
                    //
                    sb.Append("<tr><td style='width:30%;'>" + flow + "</td><td style='width:30%;'>" + preData + "</td><td style='width:30%;'>" + aftData + "</td></tr>");
                    if (row == (ds.Tables[0].Rows.Count - 1))
                        sb.Append("</table>");

                    #endregion

                    #region // --曲线图所需数据

                    arrFlow += flow + ",";
                    arrpreD += preData + ",";
                    arraftD += aftData + ",";
                    arrType = types;

                    #endregion

                }
                if (arrFlow == "")
                    arrFlow = "0" + "@";
                else
                    arrFlow = arrFlow.Substring(0, arrFlow.Length - 1) + "@";
                if (arrpreD == "")
                    arrpreD = "0" + "@";
                else
                    arrpreD = arrpreD.Substring(0, arrpreD.Length - 1) + "@";
                if (arraftD == "")
                    arraftD = "0" + "@";
                else
                    arraftD = arraftD.Substring(0, arraftD.Length - 1) + "@";
                if (arrType == "")
                    arrType = "0" + "@";
                else
                    arrType = arrType + "@";
                sb.Append("</td>");
                sb.Append("</tr></table>");

                arrFlow = arrFlow.Substring(0, arrFlow.Length - 1);
                arrpreD = arrpreD.Substring(0, arrpreD.Length - 1);
                arraftD = arraftD.Substring(0, arraftD.Length - 1);
                arrType = arrType.Substring(0, arrType.Length - 1);
                string strInfo = arrFlow + "&" + arrpreD + "&" + arraftD + "&" + arrType;
                return Json(new { success = "true", strSb = sb.ToString(), strInfo = strInfo });
            }
            else
            {
                return Json(new { success = "false" });
            }

        }


        #region // 重复性和示值报告 完成

        public ActionResult RepeatValueReport()
        {
            return View();
        }

        // 重复性和示值报告-查询 完成
        public ActionResult GetRepeatValue()
        {
            tk_RepeatReportSearch RepeatReport = new tk_RepeatReportSearch();
            RepeatReport.strMeterID = Request["MeterID"];

            if (ModelState.IsValid)
            {
                string where = "";
                // 表规格
                string Model = Request["Model"].ToString();
                // 表号
                string MeterID = Request["MeterID"].ToString();

                if (MeterID != "" && MeterID != null)
                    where += " and b.MeterID ='" + MeterID + "'";
                if (Model != "" && Model != null)
                    where += " and b.Model ='" + Model + "'";

                DataTable dt = new DataTable();
                if (where != "")
                {
                    dt = FlowDAMan.GetRepeatValue(where);
                }
                //
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    sb.Append("<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">");
                    sb.Append(" <tr class=\"left\" style=\"height:25px\"><td style=\"width:8%\" rowspan=\"2\">规格</td><td style=\"width:8%\" rowspan=\"2\">表号</td><td style=\"width:21%\" colspan=\"3\">初检</td>");
                    sb.Append(" <td style=\"width:21%\" colspan=\"3\">清洗后</td><td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">清洗前后误差降低</td>");
                    sb.Append(" <td style=\"width:14%\" colspan=\"2\">清洗后偏正/偏负</td></tr>");
                    sb.Append(" <tr class=\"left\" style=\"height:25px\"><td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">示值</td>");
                    sb.Append(" <td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">示值</td><td></td>");
                    sb.Append(" <td style=\"width:7%\">大段</td><td style=\"width:7%\">小段</td>");
                    sb.Append(" <td style=\"width:7%\">大段</td><td style=\"width:7%\">小段</td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr class=\"staleft\"><td>" + dt.Rows[i]["Model"].ToString() + "</td><td>" + dt.Rows[i]["MeterID"].ToString() + "</td><td>"
                            + dt.Rows[i]["rep1"].ToString() + "</td><td>" + dt.Rows[i]["q1"].ToString() + "</td><td>" + dt.Rows[i]["q2"].ToString() + "</td><td>"
                            + dt.Rows[i]["rep2"].ToString() + "</td><td>" + dt.Rows[i]["q3"].ToString() + "</td><td>" + dt.Rows[i]["q4"].ToString() + "</td><td>"
                            + dt.Rows[i]["avgrepeat"].ToString() + "</td><td>" + dt.Rows[i]["MaxQ1"].ToString() + "</td><td>" + dt.Rows[i]["MinQ1"].ToString() + "</td><td>"
                            + dt.Rows[i]["MaxQ2"].ToString() + "</td><td>" + dt.Rows[i]["MinQ2"].ToString() + "</td></tr>");
                    }
                    sb.Append("</table>");
                    // 总结描述 
                    int rowCount = dt.Rows.Count;
                    string sign = "<span style=\"color:#000099;font-weight:bold;\">";
                    sign += dt.Rows[rowCount - 1]["rep1"].ToString() + "台重复性提高" + dt.Rows[rowCount - 2]["avgrepeat"].ToString() + ";";
                    sign += dt.Rows[rowCount - 1]["rep1"].ToString() + "台误差降低" + dt.Rows[rowCount - 2]["MaxQ1"].ToString() + ";";
                    sign += dt.Rows[rowCount - 1]["rep1"].ToString() + "台误差降低" + dt.Rows[rowCount - 2]["MinQ1"].ToString() + ";";
                    sign += dt.Rows[rowCount - 1]["rep1"].ToString() + "台";
                    if (Convert.ToDecimal(dt.Rows[rowCount - 2]["MaxQ2"]) < 0)
                        sign += "误差偏负" + dt.Rows[rowCount - 2]["MaxQ2"].ToString() + ";";
                    else
                        sign += "误差偏正" + dt.Rows[rowCount - 2]["MaxQ2"].ToString() + ";";
                    sign += dt.Rows[rowCount - 1]["rep1"].ToString() + "台";
                    if (Convert.ToDecimal(dt.Rows[rowCount - 2]["MaxQ2"]) < 0)
                        sign += "误差偏负" + dt.Rows[rowCount - 2]["MinQ2"].ToString() + ";";
                    else
                        sign += "误差偏正" + dt.Rows[rowCount - 2]["MinQ2"].ToString() + ";";
                    sign += "</span>\n";

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


        #endregion


        #endregion

        #region //故障点分析

        // 大客户数据分析
        public ActionResult CustomerAnaly()
        {
            return View();
        }

        // 大客户数据分析-获取列表数据
        public ActionResult LoadCustomerAnaly()
        {
            string strErr = "";
            string strCustomer = FlowDAMan.LoadCustomerAnaly(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strCustomer == "")
                    return Json(new { success = "false", Msg = "统计数据加载失败" });
                else
                    return Json(new { success = "true", Customer = strCustomer });
            }
        }

        // 大客户数据分析-汇总
        public ActionResult LoadCustomerTotal()
        {
            string strErr = "";
            string strCustomer = FlowDAMan.LoadCustomerTotal(ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strCustomer == "")
                    return Json(new { success = "false", Msg = "统计数据加载失败" });
                else
                    return Json(new { success = "true", CustomerTotal = strCustomer });
            }
        }

        // 问题数量分析
        public ActionResult UserAnaly()
        {
            return View();
        }

        // 按年份分析
        public ActionResult YearAnaly()
        {
            return View();
        }

        // 按口径分析
        public ActionResult CalibreAnaly()
        {
            return View();
        }

        public ActionResult LoadYearAnaly()
        {
            string name = Request["name"];
            string sdate = Request["sdate"];
            string edate = Request["edate"];

            string strWhere = "";
            if (name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != "" && edate != "")
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadYearAnaly(strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }




        public ActionResult LoadCalibreAnaly()
        {
            string name = Request["name"];
            string sdate = Request["sdate"];
            string edate = Request["edate"];

            string strWhere = "";
            if (name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != "" && edate != "")
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadCalibreAnaly(strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }



        public ActionResult LoadRepalceAnaly()
        {
            string name = Request["name"];
            string sdate = Request["sdate"];
            string edate = Request["edate"];

            string strWhere = "";
            if (name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != "" && edate != "")
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }

            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadRepalceAnaly(strWhere);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportDataTableToExcelYearAnaly(tk_CardSearch c)
        {

            string name = c.CustomerName;
            string sdate = c.SS_Date;
            string edate = c.ES_Date;
            string strWhere = "";
            if (name != null && name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != null && sdate != "" && edate != "" && edate != null)
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != null && sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != null && edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }



            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadYearAnaly(strWhere);

            DataTable dt = udtTask.DtData;
            string strCols = "";
            if (name == "" || name == null)
            {
                strCols = "仪表发生问题同年对比,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
            }
            else
            {
                if (name.Contains("贸易"))
                {
                    strCols = "贸易,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
                }
                else
                {
                    strCols = "区域计量,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
                }
            }


            System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelYearAnaly(dt, "按年分", strCols.Split(','));
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "按年分.xls");


        }

        public ActionResult ExportDataTableToExcelCalibreAnaly(tk_CardSearch c)
        {

            string name = c.CustomerName;
            string sdate = c.SS_Date;
            string edate = c.ES_Date;
            string strWhere = "";
            if (name != null && name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != null && sdate != "" && edate != "" && edate != null)
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != null && sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != null && edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }



            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadCalibreAnaly(strWhere);

            DataTable dt = udtTask.DtData;
            string strCols = "";
            if (name == "" || name == null)
            {
                strCols = "仪表发生问题同年对比,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
            }
            else
            {
                if (name.Contains("贸易"))
                {
                    strCols = "贸易,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
                }
                else
                {
                    strCols = "区域计量,总表数,总问题表数,总故障率,大问题表数,大问题故障率,影响计量问题表数,影响计量故障率";
                }
            }


            System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelCalibreAnaly(dt, "按口径", strCols.Split(','));
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "按口径.xls");


        }


        public ActionResult ExportDataTableToExcelUserAnaly(tk_CardSearch c)
        {

            string name = c.CustomerName;
            string sdate = c.SS_Date;
            string edate = c.ES_Date;
            string strWhere = "";
            if (name != null && name != "")
            {
                strWhere += " and CustomerName like '%" + name + "%'";
            }
            if (sdate != null && sdate != "" && edate != "" && edate != null)
            {
                strWhere += " and  DATEPART(year,S_Date)>=" + sdate + " and  DATEPART(year,S_Date)<=" + edate;
            }
            else
            {
                if (sdate != null && sdate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + sdate;
                }
                if (edate != null && edate != "")
                {
                    strWhere += "and  DATEPART(year,S_Date)=" + edate;
                }
            }



            UIDataTable udtTask = new UIDataTable();
            udtTask = FlowDAMan.LoadRepalceAnaly(strWhere);

            DataTable dt = udtTask.DtData;
            string strCols = "";
            if (name == "" || name == null)
            {
                strCols = "仪表发生问题同年对比,问题数量,所占比例";
            }
            else
            {
                if (name.Contains("贸易"))
                {
                    strCols = "贸易,问题数量,所占比例";
                }
                else
                {
                    strCols = "区域计量,问题数量,所占比例";
                }
            }


            System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcelUserAnaly(dt, "问题数量", strCols.Split(','));
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "问题数量.xls");


        }
        #endregion









    }
}
