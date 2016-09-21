using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace TECOCITY_BGOI
{
    public class FlowDAPro
    {
        // 重复性和示值报告-查询
        public static DataTable GetRepeatValue(string where)
        {
            DataTable instData = new DataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };
            instData = SQLBase.FillTable("GetRepeatValue", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            if (instData != null && instData.Rows.Count > 0)
            {
                int rowCount = instData.Rows.Count;// 行数
                int colCount = instData.Columns.Count;// 列数
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 2; col < colCount; col++)
                    {
                        string strInfo = instData.Rows[row][col].ToString().Trim();
                        if (instData.Rows[row][col] == null || strInfo == "")
                            instData.Rows[row][col] = 0;
                    }
                }
                // 1、重复性=清洗重复性-初检重复性
                // 2、误差降低大=|清洗示值大|-|初检示值大|
                // 3、误差降低小=|清洗示值小|-|初检示值小|
                // 4、清洗后偏正大=清洗示值大-初检示值大
                // 5、清洗后偏正小=清洗示值小-初检示值小
                for (int i = 0; i < rowCount; i++)
                {
                    decimal rep1 = Convert.ToDecimal(instData.Rows[i]["rep1"].ToString());//初检重复性                    
                    decimal q1 = Convert.ToDecimal(instData.Rows[i]["q1"].ToString());//初检示值大                    
                    decimal q2 = Convert.ToDecimal(instData.Rows[i]["q2"].ToString());//初检示值小                    
                    decimal rep2 = Convert.ToDecimal(instData.Rows[i]["rep2"].ToString());//清洗重复性                    
                    decimal q3 = Convert.ToDecimal(instData.Rows[i]["q3"].ToString());//清洗示值大                    
                    decimal q4 = Convert.ToDecimal(instData.Rows[i]["q4"].ToString());//清洗示值小 
                    decimal avgrepeat = 0;//重复性 
                    decimal MaxQ1 = 0; //误差降低大 
                    decimal MinQ1 = 0;//误差降低小  
                    decimal MaxQ2 = 0;//清洗后偏正大 
                    decimal MinQ2 = 0; //清洗后偏正小
                    //
                    avgrepeat = rep2 - rep1;
                    instData.Rows[i]["avgrepeat"] = avgrepeat;
                    MaxQ1 = Math.Abs(q3) - Math.Abs(q1);
                    instData.Rows[i]["MaxQ1"] = MaxQ1;
                    MinQ1 = Math.Abs(q4) - Math.Abs(q2);
                    instData.Rows[i]["MinQ1"] = MinQ1;
                    MaxQ2 = q3 - q1;
                    instData.Rows[i]["MaxQ2"] = MaxQ2;
                    MinQ2 = q4 - q2;
                    instData.Rows[i]["MinQ2"] = MinQ2;
                }
                DataRow newRow1 = instData.NewRow();// 求和
                newRow1[0] = "合计";
                newRow1[1] = "--";
                DataRow newRow2 = instData.NewRow();//平均值
                newRow2[0] = "平均值";
                newRow2[1] = "--";
                DataRow newRow3 = instData.NewRow();// 表数量
                newRow3[0] = "表数量";
                newRow3[1] = "--";

                // 计算新增的三行数据
                for (int col = 2; col < instData.Columns.Count; col++)
                {
                    decimal Sum = 0;
                    decimal Avg = 0;
                    decimal Count = 0;
                    for (int row = 0; row < rowCount; row++)
                    {
                        Sum += Convert.ToDecimal(instData.Rows[row][col]);
                        Count++;
                    }
                    Avg = Math.Round(Sum / Count, 2);
                    newRow1[col] = Sum;
                    newRow2[col] = Avg;
                    newRow3[col] = Count;

                }
                instData.Rows.Add(newRow1);
                instData.Rows.Add(newRow2);
                instData.Rows.Add(newRow3);

            }
            return instData;
        }

        // 检测对比图-加载检测表列表
        public static UIDataTable LoadDetecList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("GetDetecList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            //
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            DataTable dtOrder = DO_Order.Tables[0];
            instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[1].Rows[0][0]);
            if (instData.IntRecords > 0)
            {
                if (instData.IntRecords % a_intPageSize == 0)
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize;
                else
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            //
            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    dtOrder.Rows[i]["RowNumber"] = i + 1;
                }
            }

            instData.DtData = dtOrder;
            return instData;
        }

        // 获取表格数据???? 流量点 还有示值误差对应的字段名核对
        public static DataSet getMeterImg(string arr)
        {
            string strSql = "";
            string[] Info = arr.ToString().Split('@');
            string RID = Info[0].ToString();
            // string MeterID = Info[0].ToString();
            string Types = Info[2].ToString();

            //-- 流量点 维修前示值误差 维修后示值误差 检测方式
            strSql += " select ISNULL(a1,0) Flow,ISNULL(a2,0) PreData,isnull(a3,0) AftData,'" + Types + "' as Types from (";
            strSql += " select a.Qmin as a1,a.Avg_Qmin as a2,b.Avg_Qmin as a3 from tk_CheckData a ";
            strSql += " left join (select * from tk_CheckData where RepairType='维修检测' and validate='是') b on a.RID=b.RID ";
            strSql += " where a.RepairType='进厂检测' and a.RID='" + RID + "' ";
            strSql += " union all(select a.[0.1Qmax],a.[Avg_0.1Qmax],b.[Avg_0.1Qmax] from tk_CheckData a ";
            strSql += " left join (select * from tk_CheckData where RepairType='维修检测' and validate='是') b on a.RID=b.RID ";
            strSql += " where a.RepairType='进厂检测' and a.RID='" + RID + "') ";
            strSql += " union all(select a.[0.2Qmax],a.[Avg_0.2Qmax],b.[Avg_0.2Qmax] from tk_CheckData a ";
            strSql += " left join (select * from tk_CheckData where RepairType='维修检测' and validate='是') b on a.RID=b.RID ";
            strSql += " where a.RepairType='进厂检测' and a.RID='" + RID + "') ";
            strSql += " union all(select a.[0.25Qmax],a.[Avg_0.25Qmax],b.[Avg_0.25Qmax] from tk_CheckData a ";
            strSql += " left join (select * from tk_CheckData where RepairType='维修检测' and validate='是') b on a.RID=b.RID ";
            strSql += " where a.RepairType='进厂检测' and a.RID='" + RID + "' )) k ";

            DataSet ds = SQLBase.FillDataSet(strSql, CommandType.Text, null, "FlowMeterDBCnn");
            return ds;

        }


        // 大客户数据分析-获取列表数据
        public static DataTable LoadCustomerAnaly(ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                int endYear = DateTime.Now.Year;
                string sql = "";
                //
                for (int i = 5; i <= 15; i += 5)
                {
                    int yearMid = endYear - i;
                    string rid = "";
                    if (i == 5)// 5年以内
                    {
                        rid = " and  RID in (select RID from tk_RepairCard where DATEPART(year,FactoryDate)<=" + endYear + " and DATEPART(year,FactoryDate)>" + yearMid + ")";
                        sql += " select '" + i + "年以上' as t0,";
                    }
                    if (i == 10)// 5-10年以内
                    {
                        rid = " and  RID in (select RID from tk_RepairCard where DATEPART(year,FactoryDate)<=" + (endYear - 5) + " and DATEPART(year,FactoryDate)>" + yearMid + ")";
                        sql += " union all( select '5-10年以内' as t0,";
                    }
                    if (i == 15)// 10年以上
                    {
                        rid = " and  RID in (select RID from tk_RepairCard where DATEPART(year,FactoryDate)<=" + (endYear - 10) + ")";
                        sql += " union all( select '10年以上' as t0,";
                    }
                    sql += " (select COUNT(*) from tk_GenQtn where TotalPriceU>'2000' " + rid + ") as t1, ";
                    sql += " (select COUNT(*) from  (select COUNT(*) C from tk_RepairDevice where 1=1 " + rid + " group by RID ) B ) as t2, ";
                    sql += " (select COUNT(*) from tk_RepairInfo  where 1=1 " + rid + ") as t3, ";
                    sql += " (select COUNT(*) from tk_RepairCard  where 1=1 " + rid + ") as t4 ";
                    if (i > 5)
                        sql += ") ";
                }
                string strSql = "select t0, Convert(nvarchar(30),t1,30) t1,Convert(nvarchar(30),t2,30) t2,Convert(nvarchar(30),t3,30) t3,Convert(nvarchar(30),t4,30) t4 from (";
                strSql += sql;
                strSql += ")l ";
                //
                DataTable DO_Order = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                DataRow newRow1 = DO_Order.NewRow();
                DataRow newRow2 = DO_Order.NewRow();
                DataRow newRow3 = DO_Order.NewRow();
                newRow1["t0"] = DO_Order.Rows[0]["t0"];
                newRow2["t0"] = DO_Order.Rows[1]["t0"];
                newRow3["t0"] = DO_Order.Rows[2]["t0"];

                string AllCount = (Convert.ToInt32(DO_Order.Rows[0]["t4"]) + Convert.ToInt32(DO_Order.Rows[1]["t4"]) + Convert.ToInt32(DO_Order.Rows[2]["t4"])).ToString();

                double s = 0.00;

                #region // 5年以内

                if (DO_Order.Rows[0]["t4"].ToString() != "0")
                {
                    s = Convert.ToDouble(DO_Order.Rows[0]["t1"]) / Convert.ToDouble(DO_Order.Rows[0]["t4"]) * 100;
                    newRow1["t1"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[0]["t2"]) / Convert.ToDouble(DO_Order.Rows[0]["t4"]) * 100;
                    newRow1["t2"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[0]["t3"]) / Convert.ToDouble(DO_Order.Rows[0]["t4"]) * 100;
                    newRow1["t3"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[0]["t4"]) / Convert.ToDouble(AllCount) * 100;
                    newRow1["t4"] = Math.Round(s, 2).ToString() + "%";
                }
                else
                {
                    newRow1["t1"] = "0.00%";
                    newRow1["t2"] = "0.00%";
                    newRow1["t3"] = "0.00%";
                    newRow1["t4"] = "0.00%";
                }

                #endregion

                #region // 5-10年以内

                if (DO_Order.Rows[1]["t4"].ToString() != "0")
                {
                    s = Convert.ToDouble(DO_Order.Rows[1]["t1"]) / Convert.ToDouble(DO_Order.Rows[1]["t4"]) * 100;
                    newRow2["t1"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[1]["t2"]) / Convert.ToDouble(DO_Order.Rows[1]["t4"]) * 100;
                    newRow2["t2"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[1]["t3"]) / Convert.ToDouble(DO_Order.Rows[1]["t4"]) * 100;
                    newRow2["t3"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[1]["t4"]) / Convert.ToDouble(AllCount) * 100;
                    newRow2["t4"] = Math.Round(s, 2).ToString() + "%";
                }
                else
                {
                    newRow2["t1"] = "0.00%";
                    newRow2["t2"] = "0.00%";
                    newRow2["t3"] = "0.00%";
                    newRow2["t4"] = "0.00%";
                }

                #endregion

                #region // 5-10年以内

                if (DO_Order.Rows[2]["t4"].ToString() != "0")
                {
                    s = Convert.ToDouble(DO_Order.Rows[2]["t1"]) / Convert.ToDouble(DO_Order.Rows[2]["t4"]) * 100;
                    newRow3["t1"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[2]["t2"]) / Convert.ToDouble(DO_Order.Rows[2]["t4"]) * 100;
                    newRow3["t2"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[2]["t3"]) / Convert.ToDouble(DO_Order.Rows[2]["t4"]) * 100;
                    newRow3["t3"] = Math.Round(s, 2).ToString() + "%";
                    //
                    s = Convert.ToDouble(DO_Order.Rows[2]["t4"]) / Convert.ToDouble(AllCount) * 100;
                    newRow3["t4"] = Math.Round(s, 2).ToString() + "%";
                }
                else
                {
                    newRow3["t1"] = "0.00%";
                    newRow3["t2"] = "0.00%";
                    newRow3["t3"] = "0.00%";
                    newRow3["t4"] = "0.00%";
                }

                #endregion

                DO_Order.Rows.InsertAt(newRow1, 1);
                DO_Order.Rows.InsertAt(newRow2, 3);
                DO_Order.Rows.InsertAt(newRow3, 5);

                return DO_Order;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                return null;
            }
        }

        // 大客户数据分析-汇总
        public static DataTable LoadCustomerTotal(ref string a_strErr)
        {
            DataTable dtDetail = LoadCustomerAnaly(ref a_strErr);
            double Count1 = 0;
            double Count2 = 0;
            double Count3 = 0;
            double Count4 = 0;
            for (int row = 0; row < dtDetail.Rows.Count; row += 2)
            {
                Count1 += Convert.ToDouble(dtDetail.Rows[row]["t1"]);
                Count2 += Convert.ToDouble(dtDetail.Rows[row]["t2"]);
                Count3 += Convert.ToDouble(dtDetail.Rows[row]["t3"]);
                Count4 += Convert.ToDouble(dtDetail.Rows[row]["t4"]);
            }
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("项目", typeof(System.String));
            dtNew.Columns.Add("台数", typeof(System.String));
            dtNew.Columns.Add("百分比", typeof(System.String));

            DataRow drNew1 = dtNew.NewRow();
            drNew1[0] = "维修费高于2千元的故障";
            drNew1[1] = Count1;
            double s = 0.00;
            s = (Count1 / Count4) * 100;
            drNew1[2] = Math.Round(s, 2).ToString() + "%";
            //
            DataRow drNew2 = dtNew.NewRow();
            drNew2[0] = "直接影响计量的故障";
            drNew2[1] = Count2;
            s = (Count2 / Count4) * 100;
            drNew2[2] = Math.Round(s, 2).ToString() + "%";
            //
            DataRow drNew3 = dtNew.NewRow();
            drNew3[0] = "故障合计";
            drNew3[1] = Count3;
            s = (Count3 / Count4) * 100;
            drNew3[2] = Math.Round(s, 2).ToString() + "%";
            //
            DataRow drNew4 = dtNew.NewRow();
            drNew4[0] = "总数";
            drNew4[1] = Count4;
            drNew4[2] = "100.00%";

            dtNew.Rows.Add(drNew1);
            dtNew.Rows.Add(drNew2);
            dtNew.Rows.Add(drNew3);
            dtNew.Rows.Add(drNew4);

            return dtNew;

        }

        public static UIDataTable LoadYearAnaly(string where)
        {
            var y = DateTime.Now.Year;
            ArrayList year = new ArrayList();
            for (int i = 1998; i <= y; i++)
            {
                year.Add(i);
            }


            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            UIDataTable instData = new UIDataTable();
            DataTable dt = new DataTable();

            dt.Columns.Add("年份", typeof(System.String));
            dt.Columns.Add("总表数", typeof(System.String));
            dt.Columns.Add("总问题表数", typeof(System.String));
            dt.Columns.Add("总故障率", typeof(System.String));
            dt.Columns.Add("大问题表数", typeof(System.String));
            dt.Columns.Add("大问题故障率", typeof(System.String));
            dt.Columns.Add("影响计量问题表数", typeof(System.String));
            dt.Columns.Add("影响计量故障率", typeof(System.String));

            for (int i = 0; i < year.Count; i++)
            {

                string rid = "and  RID in (select RID from tk_RepairCard where DATEPART(year,FactoryDate)=" + year[i] + ")";

                if (where != "")
                {
                    rid = "and  RID in (select RID from tk_RepairCard where DATEPART(year,FactoryDate)=" + year[i] + where + ")";
                }
                string sql = "select " + year[i] + " as '年份',";
                sql += "(select COUNT(*)    from tk_RepairCard  where 1=1 " + rid + " ) '总数', ";
                sql += "(select COUNT(*)    from tk_RepairInfo  where 1=1 " + rid + ") '总问题', ";
                sql += "(select COUNT(*)  from tk_GenQtn where TotalPriceU>'2000' " + rid + " ) '大问题',";
                sql += "(select COUNT(*) from  (select  COUNT(*) C from tk_RepairDevice where 1=1 " + rid + " group by  RID ) B ) '影响计量'";
                DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                for (int j = 0; j < DO_Order.Rows.Count; j++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["年份"] = DO_Order.Rows[0]["年份"];
                    newRow["总表数"] = DO_Order.Rows[0]["总数"];
                    newRow["总问题表数"] = DO_Order.Rows[0]["总问题"];
                    double s = 0.00;
                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["总问题"].ToString() == "0")
                        {
                            newRow["总故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["总问题"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["总故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["总故障率"] = "0.00%";
                    }

                    newRow["大问题表数"] = DO_Order.Rows[0]["大问题"];
                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["大问题"].ToString() == "0")
                        {
                            newRow["大问题故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["大问题"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["大问题故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["大问题故障率"] = "0.00%";
                    }


                    newRow["影响计量问题表数"] = DO_Order.Rows[0]["影响计量"];

                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["影响计量"].ToString() == "0")
                        {
                            newRow["影响计量故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["影响计量"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["影响计量故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["影响计量故障率"] = "0.00%";
                    }


                    dt.Rows.Add(newRow);
                }

            }







            instData.DtData = dt;
            return instData;
        }


        public static UIDataTable LoadCalibreAnaly(string where)
        {
            ArrayList Calibre = new ArrayList();
            Calibre.Add(50); Calibre.Add(80); Calibre.Add(100);
            Calibre.Add(150); Calibre.Add(200); Calibre.Add(250);
            Calibre.Add(300); Calibre.Add(400); Calibre.Add(500);
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            UIDataTable instData = new UIDataTable();
            DataTable dt = new DataTable();

            dt.Columns.Add("口径", typeof(System.String));
            dt.Columns.Add("总表数", typeof(System.String));
            dt.Columns.Add("总问题表数", typeof(System.String));
            dt.Columns.Add("总故障率", typeof(System.String));
            dt.Columns.Add("大问题表数", typeof(System.String));
            dt.Columns.Add("大问题故障率", typeof(System.String));
            dt.Columns.Add("影响计量问题表数", typeof(System.String));
            dt.Columns.Add("影响计量故障率", typeof(System.String));

            for (int i = 0; i < Calibre.Count; i++)
            {

                string rid = "and  RID in (select RID from tk_RepairCard where Caliber='" + Calibre[i] + "')";

                if (where != "")
                {
                    rid = "and  RID in (select RID from tk_RepairCard where Caliber='" + Calibre[i] + "'" + where + ")";
                }
                string sql = "select " + Calibre[i] + " as '口径',";
                sql += "(select COUNT(*)    from tk_RepairCard  where 1=1 " + rid + " ) '总数', ";
                sql += "(select COUNT(*)    from tk_RepairInfo  where 1=1 " + rid + ") '总问题', ";
                sql += "(select COUNT(*)  from tk_GenQtn where TotalPriceU>'2000' " + rid + " ) '大问题',";
                sql += "(select COUNT(*) from  (select  COUNT(*) C from tk_RepairDevice where 1=1 " + rid + " group by  RID ) B ) '影响计量'";
                DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                for (int j = 0; j < DO_Order.Rows.Count; j++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["口径"] = "DN" + DO_Order.Rows[0]["口径"];
                    newRow["总表数"] = DO_Order.Rows[0]["总数"];
                    newRow["总问题表数"] = DO_Order.Rows[0]["总问题"];
                    double s = 0.00;
                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["总问题"].ToString() == "0")
                        {
                            newRow["总故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["总问题"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["总故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["总故障率"] = "0.00%";
                    }

                    newRow["大问题表数"] = DO_Order.Rows[0]["大问题"];
                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["大问题"].ToString() == "0")
                        {
                            newRow["大问题故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["大问题"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["大问题故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["大问题故障率"] = "0.00%";
                    }


                    newRow["影响计量问题表数"] = DO_Order.Rows[0]["影响计量"];

                    if (DO_Order.Rows[0]["总数"].ToString() != "0")
                    {
                        if (DO_Order.Rows[0]["影响计量"].ToString() == "0")
                        {
                            newRow["影响计量故障率"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["影响计量"]) / Convert.ToDouble(DO_Order.Rows[0]["总数"]) * 100;
                            newRow["影响计量故障率"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["影响计量故障率"] = "0.00%";
                    }


                    dt.Rows.Add(newRow);
                }

            }







            instData.DtData = dt;
            return instData;
        }


        public static UIDataTable LoadRepalceAnaly(string where)
        {
            ArrayList Repalce = new ArrayList();
            Repalce.Add("机芯总成"); Repalce.Add("磁耦合"); Repalce.Add("主轴轴承");
            Repalce.Add("叶轮"); Repalce.Add("油泵"); Repalce.Add("机械表头");
            Repalce.Add("前导流"); Repalce.Add("高频头");
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            UIDataTable instData = new UIDataTable();
            DataTable dt = new DataTable();

            dt.Columns.Add("零件", typeof(System.String));
            dt.Columns.Add("问题数量", typeof(System.String));
            dt.Columns.Add("所占比例", typeof(System.String));

            string sqlnum = "select Count(*) from tk_RepairDevice where DeviceName in ('机芯总成','磁耦合','主轴轴承','叶轮','油泵','机械表头','前导流','高频头')";
            var num = SQLBase.FillTable(sqlnum, "FlowMeterDBCnn").Rows[0][0];

            for (int i = 0; i < Repalce.Count; i++)
            {

                string rid = "";

                if (where != "")
                {
                    rid = "and  RID in (select RID from tk_RepairCard where 1=1" + where + ")";
                }
                string sql = "select Count(*) '数量' from tk_RepairDevice where DeviceName='" + Repalce[i] + "'" + rid;
                DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                for (int j = 0; j < DO_Order.Rows.Count; j++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["零件"] = Repalce[i];
                    newRow["问题数量"] = DO_Order.Rows[0]["数量"];
                    if (num.ToString() != "0")
                    {
                        var s = 0.00;
                        if (DO_Order.Rows[0]["数量"].ToString() == "0")
                        {
                            newRow["所占比例"] = "0.00%";
                        }
                        else
                        {
                            s = Convert.ToDouble(DO_Order.Rows[0]["数量"]) / Convert.ToDouble(num.ToString()) * 100;
                            newRow["所占比例"] = Math.Round(s, 2).ToString() + "%";
                        }
                    }
                    else
                    {
                        newRow["所占比例"] = "0.00%";
                    }
                    dt.Rows.Add(newRow);
                }
            }

            instData.DtData = dt;
            return instData;
        }
    }
}
