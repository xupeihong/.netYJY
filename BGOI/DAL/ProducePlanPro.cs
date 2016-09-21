using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI
{
    public class ProducePlanPro
    {
        public static System.Data.DataTable getstate(string strType)
        {
            string strSql = "select StateId as SID,Name as Text from tk_ConfigState ";
            strSql += " where Type = '" + strType + "' order by SID ";
            DataTable dt = SQLBase.FillTable(strSql, "MainProduce");
            return dt;
        }

        public static DataTable GetPlanYear()
        {
            string sql = "select distinct Plannedyear from dbo.tk_Product_Plan order by Plannedyear desc";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        #region 制定任务单
        public static UIDataTable GetProductPlan(int a_intPageSize, int a_intPageIndex, string JHID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Produce].[dbo].[tk_Product_PlanDetail] where JHID='" + JHID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "JHID='" + JHID + "'";
            string strOrderBy = "JHID ";
            String strTable = "[BGOI_Produce].[dbo].[tk_Product_PlanDetail]";
            String strField = "JHID,DID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,OnlineCount";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;

        }

        public static DataTable GetSpecifications()
        {
            string str = "select distinct Specifications from BGOI_Produce.dbo.[tk_Product_PlanDetail]";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static string GetTopJHID()
        {
            string strID = "";
            string strD = "JH" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(JHID) from [BGOI_Produce].[dbo].[tk_Product_Plan]";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "-" + UnitID + "-" + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(3, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "-" + UnitID + "-" + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "-" + UnitID + "-" + "0" + (num + 1);

                        else
                            strD = strD + "-" + UnitID + "-" + (num + 1);
                    }
                    else
                    {
                        strD = strD + "-" + UnitID + "-" + "001";
                    }
                }
            }
            else
            {
                strD = strD + "-" + UnitID + "-" + "001";
            }
            return strD;

        }

        public static string ProJHNum(string JHID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_Product_PlanDetail]";
            DataTable dt = SQLBase.FillTable(sqlstr);

            if (dt != null && dt.Rows.Count > 0)
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != JHID)
                {
                    Num = JHID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = JHID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = JHID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static DataTable GetZD()
        {
            string str = "select distinct a.ProductID,B.ProName,b.Spec,isnull(c.finishCount,0)finishCount,isnull(c.HalfCount,0)HalfCount,isnull(c.OnlineCount,0)OnlineCount,isnull(d.num,0) xdnum,isnull(e.lj,0) lj from tk_ProductionOfFinishedGoods aa left join tk_ProFinishDefine a on aa.PID = A.ProductID left join tk_ProductInfo b on a.ProductID = b.PID left join tk_StockRemain c on a.ProductID = c.ProductID LEFT JOIN (select ProductID ,sum(OrderNum-ShipmentNum)num from BGOI_Sales.dbo.Orders_DetailInfo group by ProductID) d on  a.ProductID = d.ProductID left join (select KSC.ProductID,min(FinishCount/Number)lj from tk_ProFinishDefine KSC LEFT JOIN tk_StockRemain KC ON KSC.PartPID = KC.ProductID  where Number>0  group by KSC.ProductID) e on a.ProductID = e.ProductID where a.ValiDate='v' AND aa.ValiDate ='v' and UnitID ='"+GAccount.GetAccountInfo().UnitID+"'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static bool SaveProductPlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {

            int count = 0;
            
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_Product_Plan>(record, "[BGOI_Produce].[dbo].tk_Product_Plan");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert, "MainProduce");
                }
                if(count<=0)
                {
                    strErr = "计划单保存失败！";
                    return false;
                }

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    
                    strInsertList = GSqlSentence.GetInsertByList<tk_Product_PlanDetail>(delist, "tk_Product_PlanDetail");
                    if (strInsertList != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsertList, "MainProduce");
                    }
                }

                if (count > 0)
                {
                    return true;
                }

                else
                    return false;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
               
                return false;
            }
        }
        #endregion

        #region 修改计划单
        public static DataTable LoadPlanDatail(string JHID)
        {
            string str = "select " + "PID,Name,Specifications,Finishedproduct,finishingproduct,OnlineCount,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,DID,CreateUser,CreateTime " +
"from  [BGOI_Produce].dbo.[tk_Product_PlanDetail] where JHID='" + JHID + "' ";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static tk_Product_Plan IndexAllupdatePlan(string JHID)
        {
            string str = "select  JHID, UnitID, Plannedyear, Plannedmonth, convert(varchar(100),Specifieddate,23) Specifieddate, Formulation, Remarks, State, Approvalstatus, CreateUser, CreateTime, Validate from BGOI_Produce.dbo.tk_Product_Plan where JHID='" + JHID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_Product_Plan Task = new tk_Product_Plan();

            GSqlSentence.SetTValueD<tk_Product_Plan>(Task, dt.Rows[0]);
            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRowToPlan(item, Task);
            //}
            return Task;
        }

        public static void DataRowToPlan(DataRow item, tk_Product_Plan task)
        {
            task.Specifieddate = Convert.ToDateTime(item["Specifieddate"]);
            task.Plannedmonth = item["Plannedmonth"].ToString();
            task.Remarks = item["Remarks"].ToString();
            task.Formulation = item["Formulation"].ToString();
        }

        public static bool SaveUpdatePlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {
            //SqlTransaction sqltra = con.BeginTransaction();//开始事务
            
            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_Product_Plan set Specifieddate=@Specifieddate,Plannedmonth=@Plannedmonth," + "Remarks=@Remarks,Formulation=@Formulation where JHID=@JHID";
                SqlParameter[] param ={
                                       new SqlParameter ("@Specifieddate",SqlDbType.DateTime ),
                                       new SqlParameter ("@Plannedmonth",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Remarks",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Formulation",SqlDbType .NVarChar ),
                                       new SqlParameter ("@JHID",SqlDbType .NVarChar )
                                     };
                param[0].Value = Convert.ToDateTime(record.Specifieddate);
                param[1].Value = record.Plannedmonth;
                param[2].Value = record.Remarks == null ? "" : record.Remarks;
                param[3].Value = record.Formulation;
                param[4].Value = record.JHID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_Product_Plan_HIS (JHID,UnitID,Plannedyear,Plannedmonth,Specifieddate,Formulation,Remarks,State,Approvalstatus,CreateUser,CreateTime,Validate,NCreateTime,NCreateUser)" +
                "select JHID,UnitID,Plannedyear,Plannedmonth,Specifieddate,Formulation,Remarks,State,Approvalstatus,CreateUser,CreateTime,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_Product_Plan where JHID ='" + record.JHID + "'";

                int count = 0;

                count = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                if (count <= 0)
                {
                    strErr = "历史记录更新失败";
                    return false;
                }
                count = SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
                if (count <= 0)
                {
                    strErr = "计划信息更新失败";
                    return false;
                }



                string strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_Product_PlanDetail_HIS(JHID,DID,PID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,CreateUser,CreateTime,Validate,NCreateUser,NCreateTime) select JHID,DID,PID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,CreateUser,CreateTime,Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                              " from BGOI_Produce.dbo.tk_Product_PlanDetail where JHID='" + record.JHID + "'";
                count = SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                if (count < 0)
                {
                    strErr = "历史记录更新失败";
                    return false;
                }
                string strDeleteDetail = "delete tk_Product_PlanDetail where JHID='" + record.JHID + "'";
                count = SQLBase.ExecuteNonQuery(strDeleteDetail, "MainProduce");
                if (count < 0)
                {
                    return false;
                }

                if (delist.Count > 0)
                {
                    string strUpdateList = GSqlSentence.GetInsertByList<tk_Product_PlanDetail>(delist, "tk_Product_PlanDetail");
                    count = SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");
                    if (count <= 0)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }
        #endregion



        public static UIDataTable getPlanList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProductPlangrid", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

            instData.DtData = dtOrder;
            return instData;
        }


    }
}
