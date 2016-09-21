using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ProducePro
    {
        #region 提交审批
        public static UIDataTable getUMwebkeyGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getExamine", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static string GetShowSPid(string folderBack)
        {
            string[] arr = folderBack.Split('/');
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select SPID, SPidNo from [" + arr[0] + "].." + arr[6] + " where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID);
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into [" + arr[0] + "].." + arr[6] + " (SPID,SPidNo,DateRecord) values('SP',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID);
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["SPidNo"]);
            }

            intNewID++;
            string str = "select SPID, SPidNo,DateRecord from [" + arr[0] + "].." + arr[6] + " where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID);
            strPID = dtPMaxID.Rows[0]["SPID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetSPid(string folderBack)
        {
            string[] arr = folderBack.Split('/');
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select SPID, SPidNo from [" + arr[0] + "].." + arr[6] + " where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID);
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into [" + arr[0] + "].." + arr[6] + " (SPID,SPidNo,DateRecord) values('P',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID);
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["SPidNo"]);
            }

            intNewID++;
            string strUpdateID = "update [" + arr[0] + "].." + arr[6] + " set SPidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID);

            strPID = dtPMaxID.Rows[0]["SPID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }
        #endregion
        #region 审批
        public static UIDataTable CreateProjectLogGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_Log " +
" where YYCode='" + where + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "YYCode='" + where + "'";
            string strOrderBy = "LogTime ";
            String strTable = "BGOI_Produce.dbo.tk_Log";
            String strField = " YYCode,Content,YYType,LogTime,Actor";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }


      
        public static string getwebkey(string webkey)
        {
            string key = "";
            string str = "select * from [BGOI_BasMan]..tk_ConfigWebkey where ApprovalType = '" + webkey + "'";
            DataTable dt = SQLBase.FillTable(str);
            if (dt.Rows.Count > 0)
                key = dt.Rows[0]["webkey"].ToString();
            return key;
        }
        #endregion
        //添加日志GetTaskDetail
        public static bool AddProduceLog(tk_ProLog logobj)
        {
            int count = 0;
            string strInsert = GSqlSentence.GetInsertInfoByD(logobj, "BGOI_Produce .dbo.tk_Log");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region 任务单管理

        #region 新增生产任务
        //添加任务单编号
        public static string GetTopRWID()
        {
            string strID = "";
            string strD = "RW" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(RWID) from [BGOI_Produce].[dbo].[tk_ProductTask]";
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
        //相关单据
        public static DataTable GetRW(string a, ref string strErr)
        {
            string sql = "select *, convert(varchar(10),ContractDate,120) a from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + a + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        public static DataTable GetLL(string a, ref string strErr)
        {
            string sql = "select  a.ID,a.MaterialDepartment,convert(varchar(10),a.CreateTime,120) CreateTime,a.OrderContent m,a.SpecsModels n,convert(varchar(10),a.MaterialTime,120) MaterialTime,b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,a.Amount,b.Remark from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo. tk_ProductTDatail b where a.RWIDDID=b.DID and LLID='" + a + " '";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        public static DataTable GetSG(string a, ref string strErr)
        {
            string sql = "select a.ID,convert(varchar(10),a.billing,120) billing,c.PID,c.OrderContent,c.SpecsModels,c.OrderUnit,b.OrderNum,c.Remark,a.CreateUser from BGOI_Produce.dbo.tk_ProductRecord a,BGOI_Produce.dbo.tk_ProductRProduct b,BGOI_Produce.dbo.tk_ProductTDatail c where  a.SGID=b.SGID and b.DID=c.DID and a.SGID='" + a + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        public static DataTable GetBG(string a, ref string strErr)
        {
            string sql = "select  convert(varchar(10),a.uploadtime,120) uploadtime,b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,b.OrderNum,a.Remarks,a.CreatePerson from BGOI_Produce.dbo.tk_ReportInfo a,BGOI_Produce.dbo.tk_ProductTDatail b where a.DID=b.DID and BGID='" + a + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        public static DataTable GetRK(string a, ref string strErr)
        {
            string sql = "select convert(varchar(10),a.FinishTime,120) FinishTime,convert(varchar(10),a.StockInTime,120) StockInTime,c.PID,c.OrderContent,c.SpecsModels,c.OrderUnit,c.OrderNum,HouseID,Batch,StockRemark,Storekeeper,StockInUser from BGOI_Produce.dbo.tk_PStocking a,BGOI_Produce.dbo.tk_PStockingDetail b,BGOI_Produce.dbo.tk_ProductTDatail c where a.RKID=b.RKID and b.RWIDDID=c.DID and a.RKID='" + a + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable LoadTask(string RWID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",RWID)
                };
            DataTable dt = SQLBase.FillTable("getTask", CommandType.StoredProcedure, sqlPar, "MainProduce");
            if (dt == null) return null;
            return dt;
        }
        //任务单相关单据
        public static UIDataTable LoadRWPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_ProductTask " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "ContractDate desc ";
            String strTable = " BGOI_Produce.dbo.tk_ProductTask ";

            String strField = "RWID,OrderUnit,OrderContactor,OrderTel,CreateTime,Remark,State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["state"] = GetSelectTask(instData.DtData.Rows[r]["state"].ToString()).name;
                    }
                }
            }
            return instData;
        }
        //任务单详细相关单据
        public static UIDataTable LoadRWPaymentDetail(int a_intPageSize, int a_intPageIndex, string RWID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RWID='" + RWID + "'";
            string strOrderBy = " RWID ";
            String strTable = "BGOI_Produce.dbo.tk_ProductTDatail ";
            String strField = "OrderContent,SpecsModels,OrderUnit,OrderNum ,Technology ,DeliveryTime ,Remark ,State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["state"] = GetSelectTaskdetail(instData.DtData.Rows[r]["state"].ToString()).name;
                    }
                }
            }
            return instData;
        }
        //领料单相关单据
        public static UIDataTable LoadLLPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b" + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "MaterialTime desc ";
            String strTable = " BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b ";

            String strField = " a.LLID,a.MaterialDepartment,a.MaterialTime,b.PID,b.OrderContent,b.SpecsModels,a.Amount,a.State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["state"] = GetSelectLL(instData.DtData.Rows[r]["state"].ToString()).name;
                    }
                }
            }
            return instData;
        }
        //领料单详细相关单据
        public static UIDataTable LoadLLPaymentDetail(int a_intPageSize, int a_intPageIndex, string LLID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "' and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "LLID='" + LLID + "' and Validate='v'";
            string strOrderBy = " LLID ";
            String strTable = "BGOI_Produce.dbo.tk_MaterialFDetail ";
            String strField = "OrderContent,Specifications,Manufacturer,OrderUnit ,OrderNum ,Technology ,DeliveryTime ,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            return instData;
        }
        //随工单相关单据
        public static UIDataTable LoadSGPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();

            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_ProductRecord " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "SGID desc ";
            String strTable = " BGOI_Produce.dbo.tk_ProductRecord ";

            String strField = "SGID,billing,OrderContent,SpecsModels,State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["state"] = GetSelectSG(instData.DtData.Rows[r]["state"].ToString()).name;
                    }
                }
            }
            return instData;
        }
        //随工单详细相关单据
        public static UIDataTable LoadSGPaymentDetail(int a_intPageSize, int a_intPageIndex, string SGID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_ProductRDatail where SGID ='" + SGID + "' and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "SGID ='" + SGID + "'";
            string strOrderBy = " SGID ";
            String strTable = "BGOI_Produce.dbo.tk_ProductRDatail ";
            String strField = "Process,team,Estimatetime,person ,plannumber ,Qualified ,number ,numbers,Fnubers ,finishtime,people,reason";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        //报告单相关单据
        public static UIDataTable LoadBGPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_ReportInfo a, BGOI_Produce.dbo.tk_ProductTDatail b  " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "uploadtime desc ";
            String strTable = " BGOI_Produce.dbo.tk_ReportInfo a, BGOI_Produce.dbo.tk_ProductTDatail b  ";

            String strField = "a.BGID,a.CreateTime,a.DID,b.OrderContent,b.SpecsModels,b.OrderNum,b.Remark,a.CreatePerson";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        //报告单详细相关单据
        public static UIDataTable LoadBGPaymentDetail(int a_intPageSize, int a_intPageIndex, string BGID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_FileInfo where BGID='" + BGID + "' and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "BGID='" + BGID + "' and Validate='v'";
            string strOrderBy = " BGID ";
            String strTable = "BGOI_Produce.dbo.tk_FileInfo ";
            String strField = "BGID,Type,FileName,CreatePerson";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        //入库单相关单据
        public static UIDataTable LoadRKPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_PStocking " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "StockInTime desc ";
            String strTable = " BGOI_Produce.dbo.tk_PStocking ";

            String strField = "RKID,Validate,StockInUser,StockInTime,StockRemark";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        //入库单详细相关单据
        public static UIDataTable LoadRKPaymentDetail(int a_intPageSize, int a_intPageIndex, string RKID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_PStockingDetail where RKID='" + RKID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RKID='" + RKID + "'";
            string strOrderBy = " RKID ";
            String strTable = "BGOI_Produce.dbo.tk_PStockingDetail ";
            String strField = "PID,OrderContent,Specifications,Unit ,Amount ,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }


        public static DataTable getspec(string OrderContent)
        {
            string str = "select distinct SpecsModels from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductTask b where OrderContent='" + OrderContent + "' and a.RWID=b.RWID and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        //任务单列表
        public static UIDataTable ProduceRemainList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProductTask", CommandType.StoredProcedure, sqlPar, "MainProduce");
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
            //if (instData.DtData != null)
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        instData.DtData.Rows[r]["state"] = GetSelectTask(instData.DtData.Rows[r]["state"].ToString()).name;
            //    }
            //}
            return instData;

        }


        public static UIDataTable getRZ(int a_intPageSize, int a_intPageIndex, string RWID)
        {
            //UIDataTable instData = new UIDataTable();
            //string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_UserLog where RWID='" + RWID + "'";
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            //string strFilter = "RelevanceID='" + RWID + "'";
            //string strOrderBy = " RelevanceID ";
            //String strTable = "BGOI_Produce.dbo.tk_UserLog ";
            //String strField = " LogTime,RelevanceID YYCode,LogContent YYType,Type Content,LogPerson Actor";
            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",RWID)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getRZ", CommandType.StoredProcedure, sqlPar, "MainProduce");
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
        #region 获取状态信息
        //任务单主表
        public static tk_ConfigState GetSelectTask(string state)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ConfigState where Type='Tstate' and StateId='" + state + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ConfigState Task = new tk_ConfigState();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProTask(Task, item);
            }
            return Task;
        }
        private static void DatarowToProTask(tk_ConfigState pros, DataRow item)
        {
            pros.name = item["name"].ToString();
        }

        //任务单相表
        public static tk_ConfigState GetSelectTaskdetail(string state)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ConfigState where Type='TDstate' and StateId='" + state + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ConfigState Task = new tk_ConfigState();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProTaskdetail(Task, item);
            }
            return Task;
        }
        private static void DatarowToProTaskdetail(tk_ConfigState pros, DataRow item)
        {
            pros.name = item["name"].ToString();
        }

        //领料单主表
        public static tk_ConfigState GetSelectLL(string state)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ConfigState where Type='LLState' and StateId='" + state + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ConfigState LL = new tk_ConfigState();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProLL(LL, item);
            }
            return LL;
        }
        private static void DatarowToProLL(tk_ConfigState pros, DataRow item)
        {
            pros.name = item["name"].ToString();
        }

        //随工单主表
        public static tk_ConfigState GetSelectSG(string state)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ConfigState where Type='SGstate' and StateId='" + state + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ConfigState SG = new tk_ConfigState();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProSG(SG, item);
            }
            return SG;
        }
        private static void DatarowToProSG(tk_ConfigState pros, DataRow item)
        {
            pros.name = item["name"].ToString();
        }

        //入库单主表
        public static tk_ConfigState GetSelectRK(string state)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ConfigState where Type='RKstate' and StateId='" + state + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ConfigState RK = new tk_ConfigState();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProRK(RK, item);
            }
            return RK;
        }
        private static void DatarowToProRK(tk_ConfigState pros, DataRow item)
        {
            pros.name = item["name"].ToString();
        }
        #endregion


        public static DataTable GetProName()
        {
            string str = "select distinct OrderContent from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductTask b where a.RWID=b.RWID and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static DataTable GetProType()
        {
            string str = "select distinct SpecsModels from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductTask b" +
"where a.RWID=b.RWID and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static UIDataTable ProduceInDetialList(int a_intPageSize, int a_intPageIndex, string RWID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Produce].[dbo].[tk_ProductTDatail] where RWID='" + RWID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RWID='" + RWID + "'";
            string strOrderBy = "DeliveryTime ";
            String strTable = "[BGOI_Produce].[dbo].[tk_ProductTDatail]";
            String strField = "OrderContent,SpecsModels,OrderUnit,OrderNum,Technology,DeliveryTime,Remark,State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["state"] = GetSelectTaskdetail(instData.DtData.Rows[r]["state"].ToString()).name;
                    }
                }
            }
            return instData;
        }


        public static UIDataTable ChangeSpecsModelsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) From BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b where a.OrderID=b.OrderID  and UnitID ='" + GAccount.GetAccountInfo().UnitID + "' and (a.State='-1' or a.State='0'or a.State='1'or a.State='2'or a.State='3'or a.State='4'or a.State='5') and a.ISF='0'" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.OrderID=b.OrderID and UnitID ='" + GAccount.GetAccountInfo().UnitID + "' and (a.State='-1' or a.State='0'or a.State='1'or a.State='2'or a.State='3'or a.State='4'or a.State='5') and a.ISF='0'" + where;
            string strOrderBy = "a.PID ";
            String strTable = "BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b ";
            String strField = " b.ProductID,b.OrderContent,b.SpecsModels,b.OrderUnit,a.Remark,a.PID,b.OrderID,ContractID,b.OrderNum";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }

        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            string strSelCount = "";
            UIDataTable instData = new UIDataTable();

            strSelCount = "select count(*)  From BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            string strOrderBy = " OID  ";
            String strTable = "BGOI_Inventory.dbo.tk_ConfigPType";
            String strField = "ID,Text";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }

        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            string where = " and  b.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            if (ptype != "")
            {
                where += " and Ptype='" + ptype + "'  ";
            }
            //else
            //{
            //    where = " ";
            //}
            //UIDataTable instData = new UIDataTable();
            //string strSelCount = " select  COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b where a.Ptype=b.ID and a.ProTypeID='2' and  UnitID='" + GAccount.GetAccountInfo().UnitID + "'" + where;
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            //string strFilter = " a.Ptype=b.ID and a.ProTypeID='2' and  UnitID='" + GAccount.GetAccountInfo().UnitID + "'" + where;
            //string strOrderBy = " PID ";
            //String strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b  ";
            //String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text ";

            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getKSC", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

        public static UIDataTable Changematerial(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            string where = "";
            if (ptype != "")
            {
                where = " and Ptype='" + ptype + "'";
            }
            else
            {
                where = "";
            }
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select  COUNT(*) From BGOI_Inventory.dbo.tk_ConfigPType b,BGOI_Inventory.dbo.tk_ProductInfo a left join BGOI_BasMan.dbo.tk_IsNotSupplierBas c on a.Manufacturer=c.SID and c.Validate='v' where a.Ptype=b.ID and ProTypeID ='1' and  b.UnitID='" + GAccount.GetAccountInfo().UnitID + "'" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.Ptype=b.ID and ProTypeID ='1' and  b.UnitID='" + GAccount.GetAccountInfo().UnitID + "'" + where;
            string strOrderBy = " PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ConfigPType b,BGOI_Inventory.dbo.tk_ProductInfo a left join BGOI_BasMan.dbo.tk_IsNotSupplierBas c on a.Manufacturer=c.SID and c.Validate='v'  ";
            String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,c.COMNameC Manufacturer,b.Text ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }
        //从销售订单中提取型号下拉
        public static DataTable GetProSpecsModels()
        {
            string str = "select distinct b.SpecsModels from BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b where a.OrderID=b.OrderID and UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            return dt;
        }
        //根据条件changeTask里面的PId拿到值
        public static DataTable GetTaskDetail(string OrderID)
        {
            string sql = "select OrderID,ProductID,OrderContent,SpecsModels,OrderUnit,Technology,OrderNum,DID,OrderNum OrderNums From BGOI_Sales.dbo.Orders_DetailInfo where OrderID='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable getKCnum(string PID, string HouseID)
        {
            string sql = "select FinishCount from  BGOI_Inventory.dbo.tk_StockRemain where ProductID='"+PID+"' and HouseID='"+HouseID+"'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable IndexAllcustom(string OrderID)
        {
            string str = "select a.Technology,b.CustomerID,b.OrderUnit,b.OrderAddress,b.OrderContactor,b.OrderTel from BGOI_Sales.dbo.Orders_DetailInfo a,BGOI_Sales.dbo.OrdersInfo b where a.OrderID=b.OrderID and" +
" a.OrderID='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            return dt;
        }

        public static DataTable GetTaskDetails(string PID)
        {
            string sql = "select PID,ProName,Spec ,Units from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        //取内容编号
        public static string ProTaskDetialNum(string RWID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_ProductTDatail]";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = RWID + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != RWID)
                {
                    Num = RWID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = RWID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = RWID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }
        public static string ProTaskDetialNums(string RWID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_ProductTDatail] where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Num = RWID + "-" + "01";
                return Num;
            }
            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != RWID)
                {
                    Num = RWID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = RWID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = RWID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }



        public static bool SaveTask(tk_ProductTask record, List<tk_ProductTDatail> delist, ref string strErr, string OrderID)
        {
            //strErr = "";
            //foreach (tk_ProductTDatail SID in delist)
            //{
            //    //修改库存表中的剩余量
            //    DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "' and HouseID='" + HouseID + "'");
            //    if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
            //    {
            //        strErr = "产品"+SID.PID+"在仓库中没有剩余量";
            //        return false;
            //    }
            //    else
            //    {

            //        if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(SID.OrderNum))
            //        {
            //            strErr = "产品"+SID.PID+"数量大于库存数量,库存数量为"+dt.Rows[0][0]+"";
            //            return false;
            //        }
            //        //else
            //        //{
            //        //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
            //        //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
            //        //}
            //    }

            //}

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_ProductTask>(record, "[BGOI_Produce].[dbo].tk_ProductTask");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }

                trans.Close(true);
                string strInsertList = "";
                if (delist.Count > 0)
                {

                    //strInsertList=  GSqlSentence.GetInsertByList<tk_ProductTDatail>(delist, "tk_ProductTDatail");
                    foreach (tk_ProductTDatail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Produce].[dbo].[tk_ProductTDatail](RWID,DID,PID,OrderContent,SpecsModels,OrderUnit,OrderNum," +
                            //Technology,
                        "DeliveryTime," +
                            //Remark,
                       " State,Lstate,Sstate) " +
                            "values ('" + SID.RWID + "','" + ProTaskDetialNum(SID.RWID) + "','" + SID.PID + "','" + SID.OrderContent + "','" + SID.SpecsModels + "','" + SID.OrderUnit + "','" + SID.OrderNum + "'," +
                            //'" + SID.Technology + "',
                        "'" + SID.DeliveryTime + "'," +
                            //'" + SID.Remark + "',//一次修改  注释技术要求和备注
                        "'" + SID.State + "','" + SID.Lstate + "','" + SID.Sstate + "');update BGOI_Sales.dbo.OrdersInfo set Pstate='1' where OrderID='" + OrderID + "'";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
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
                trans.Close(true);
                return false;
            }
        }

        //审批信息
        public static UIDataTable SPXX(int a_intPageSize, int a_intPageIndex, string RWID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_Approval " +
" where RelevanceID='" + RWID + "'))";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RelevanceID='" + RWID + "'";
            string strOrderBy = "ApprovalTime ";
            String strTable = "BGOI_Produce.dbo.tk_Approval";
            String strField = "AppType,ApprovalMan,CreateTime,IsPass";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["AppType"] = GetSelectSPXXType(instData.DtData.Rows[r]["AppType"].ToString()).Text;
                    }
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["ApprovalMan"] = GetSelectSPXXUser(instData.DtData.Rows[r]["ApprovalMan"].ToString()).UserName;
                    }
                }
            }
            return instData;
        }

        public static tk_ConfigApp GetSelectSPXXType(string AppType)
        {
            string str = "select * from 	[BGOI_BasMan]..tk_ConfigApp where Type='apptype' and SID='" + AppType + "'";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            if (dt == null) return null;
            tk_ConfigApp Task = new tk_ConfigApp();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToConfigApp(Task, item);
            }
            return Task;
        }
        private static void DatarowToConfigApp(tk_ConfigApp pros, DataRow item)
        {
            pros.Text = item["Text"].ToString();
        }

        public static UM_UserNew GetSelectSPXXUser(string CreateUser)
        {
            string str = "	select * from 	BJOI_UM.dbo.UM_UserNew where  UserID='" + CreateUser + "'";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            if (dt == null) return null;
            UM_UserNew Task = new UM_UserNew();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToUserNew(Task, item);
            }
            return Task;
        }
        private static void DatarowToUserNew(UM_UserNew pros, DataRow item)
        {
            pros.UserName = item["UserName"].ToString();
        }
        #endregion

        #region 修改任务单
        public static DataTable getupdate(string RWID)
        {
            string str = "select State from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        public static bool SCTask(string DID, ref string strErr)
        {
            int count = 0;
            int a = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "insert into BGOI_Produce.dbo.tk_ProductTDatail_HIS (RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,NCreateTime,NCreateUser) select RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_ProductTDatail where DID ='" + DID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                string strInserts = "delete from BGOI_Produce.dbo.tk_ProductTDatail where DID ='" + DID + "'";
                if (strInserts != "")
                {
                    a = SQLBase.ExecuteNonQuery(strInserts);
                }
                trans.Close(true);
                if (count > 0 && a > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "删除失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }

        public static DataTable GetTaskDetailss(string RWID)
        {
            string sql = "select OrderID,DID,PID,OrderContent,SpecsModels,a.OrderUnit,OrderNum,a.Technology,convert(varchar(10),DeliveryTime,120) DeliveryTime,a.Remark from BGOI_Produce.dbo.tk_ProductTDatail a left join BGOI_Produce.dbo.tk_ProductTask b on a.RWID=b.RWID  where a.RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable IndexAllTask(string RWID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static bool SaveUpdateTask(tk_ProductTask record, List<tk_ProductTDatail> delist, ref string strErr, string RWID)
        {
            //strErr = "";
            //foreach (tk_ProductTDatail SID in delist)
            //{
            //    //修改库存表中的剩余量
            //    DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "' and HouseID='" + record.HouseID + "'");
            //    if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
            //    {
            //        strErr = "产品" + SID.PID + "在仓库中没有剩余量";
            //        return false;
            //    }
            //    else
            //    {

            //        if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(SID.OrderNum))
            //        {
            //            strErr = "产品" + SID.PID + "数量大于库存数量,库存数量为" + dt.Rows[0][0] + "";
            //            return false;
            //        }
            //        //else
            //        //{
            //        //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
            //        //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
            //        //}
            //    }

            //}


            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_ProductTask set Clientcode=@Clientcode,OrderUnit=@OrderUnit," + "OrderAddress=@OrderAddress,OrderContactor=@OrderContactor,OrderTel=@OrderTel,Remark=@Remark,ContractDate=@ContractDate,Technology=@Technology,Note=@Note where RWID=@RWID";
                SqlParameter[] param ={
                                       new SqlParameter ("@Clientcode",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@OrderUnit",SqlDbType.NVarChar ),
                                       new SqlParameter ("@OrderAddress",SqlDbType .NVarChar ),
                                       new SqlParameter ("@OrderContactor",SqlDbType .NVarChar ),
                                       new SqlParameter ("@OrderTel",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Remark",SqlDbType .NVarChar ),
                                       new SqlParameter ("@ContractDate",SqlDbType .DateTime ),
                                       new SqlParameter ("@Technology",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Note",SqlDbType.NVarChar ),
                                       //new SqlParameter ("@HouseID",SqlDbType.NVarChar ),
                                       new SqlParameter ("@RWID",SqlDbType .NVarChar )
                                     };
                param[0].Value = record.Clientcode;
                param[1].Value = record.OrderUnit;
                param[2].Value = record.OrderAddress;
                param[3].Value = record.OrderContactor;
                param[4].Value = record.OrderTel;
                param[5].Value = record.Remark;
                param[6].Value = record.ContractDate;
                param[7].Value = record.Technology;
                param[8].Value = record.Note;
                param[9].Value = record.RWID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_ProductTask_HIS (RWID,UnitID,OrderID,ContractDate,Clientcode,OrderUnit,OrderContactor,OrderTel,OrderAddress,Remark,Preparation,State,CreateUser,CreateTime,Validate,Materialcompletiontime,Starttime,Productioncompletiontime,Storagetime,CancelTime,CancelReason,NCreateTime,NCreateUser,Technology,Note)" +
                "select RWID,UnitID,OrderID,ContractDate,Clientcode,OrderUnit,OrderContactor,OrderTel,OrderAddress,Remark,Preparation,State,CreateUser,CreateTime,Validate,Materialcompletiontime,Starttime,Productioncompletiontime,Storagetime,CancelTime,CancelReason,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "',Technology,Note from BGOI_Produce.dbo.tk_ProductTask where RWID ='" + record.RWID + "'";
                string strUpdateList = "";
                string strInsertDetailHIS = "";
                string strinsertdetail = "";
                strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_ProductTDatail_HIS(RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,photo,DeliveryTime,State,Lstate,Sstate,NCreateUser,NCreateTime)" +
                      "select RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,photo,DeliveryTime,State,Lstate,Sstate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                      " from BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";
                strUpdateList = "delete from BGOI_Produce.dbo.tk_ProductTDatail  where RWID='" + RWID + "'";
                if (strInsertDetailHIS != "" && strUpdateList != "")
                {
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");
                }
                if (delist.Count > 0)
                {
                    foreach (tk_ProductTDatail SID in delist)
                    {
                        strinsertdetail = "Insert into [BGOI_Produce].[dbo].[tk_ProductTDatail](RWID,DID,PID,OrderContent,SpecsModels,OrderUnit,OrderNum,DeliveryTime,State,Lstate,Sstate) " +
                            "values ('" + SID.RWID + "','" + ProTaskDetialNums(RWID) + "','" + SID.PID + "','" + SID.OrderContent + "','" + SID.SpecsModels + "','" + SID.OrderUnit + "','" + SID.OrderNum + "','" + SID.DeliveryTime + "','" + SID.State + "','" + SID.Lstate + "','" + SID.Sstate + "')";
                        if (strUpdateList != "")
                        {
                            SQLBase.ExecuteNonQuery(strinsertdetail);
                        }
                    }
                }

                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        #region 任务单详情
        public static DataTable ProTaskDetail(string RWID)
        {
            string sql = "select distinct PID,OrderContent,SpecsModels,OrderUnit,OrderNum,Technology,DeliveryTime,Remark from                             BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static tk_ProductTask ProTaskDetails(string RWID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ProductTask Task = new tk_ProductTask();

            foreach (DataRow item in dt.Rows)
            {
                DataRowTask(item, Task);
            }
            return Task;
        }

        public static void DataRowTask(DataRow item, tk_ProductTask task)
        {
            task.RWID = item["RWID"].ToString();
            task.Clientcode = item["Clientcode"].ToString();
            task.OrderUnit = item["OrderUnit"].ToString();
            task.OrderAddress = item["OrderAddress"].ToString();
            task.OrderContactor = item["OrderContactor"].ToString();
            task.OrderTel = item["OrderTel"].ToString();
            task.Remark = item["Remark"].ToString();
            task.ContractDate = Convert.ToDateTime(item["ContractDate"]);
            task.Note = item["Note"].ToString();
            task.Technology = item["Technology"].ToString();
            task.ID = item["ID"].ToString();
            task.CreateUser = item["CreateUser"].ToString();
        }
        #endregion

        public static DataTable getPD(string RWID)
        {
            string str = "select name from BGOI_Produce.dbo.tk_ProductTask,BGOI_Produce.dbo.tk_ConfigState where stateid=State and Type='Tstate' and RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }


        public static DataTable getPDSP(string RWID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_Approval where RelevanceID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #region 生成领料单
        public static DataTable GetMT(string PID)
        {
            string str = " select PID,ProName,Spec ,Units,Remark from BGOI_Inventory.dbo.tk_ProductInfo where PID in  (" + PID + ")";
            //select PID,ProName,Spec ,Units from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "'
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static string GetTopLLID()
        {
            string strID = "";
            string strD = "LL" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(LLID) from [BGOI_Produce].[dbo].[tk_MaterialFDetail]";
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

        public static string GetTopLID()
        {
            string strID = "";
            string strD = DateTime.Now.ToString("yyyyMMdd");
            //string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(ID) from [BGOI_Produce].[dbo].[tk_MaterialForm] where RWID=''";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "-" + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(0, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "-" + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "-" + "0" + (num + 1);

                        else
                            strD = strD + "-" + (num + 1);
                    }
                    else
                    {
                        strD = strD + "-" + "001";
                    }
                }
            }
            else
            {
                strD = strD + "-" + "001";
            }
            return strD;

        }
        public static string MaterialFDetailNum(string LLID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_MaterialFDetail]";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = LLID + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != LLID)
                {
                    Num = LLID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = LLID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = LLID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static string MaterialFDetailNums(string LLID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_MaterialFDetail] where LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = LLID + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != LLID)
                {
                    Num = LLID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = LLID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = LLID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static DataTable GetMaterialForm(string RWID)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");

            string strInsert = "select count(Amount) from BGOI_Produce.dbo.tk_MaterialForm where Validate='v' and RWID='" + RWID + "'";
            if (strInsert != "")
            {
                count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
            }
            trans.Close(true);
            if (count > 0)
            {
                string sql = "select b.DID, b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,(OrderNum-isnull(c.Amount,0)) OrderNum,b.Remark from BGOI_Produce.dbo.tk_ProductTDatail b left join  (select RWID,RWIDDID,SUM(Amount)Amount,Validate from BGOI_Produce.dbo.tk_MaterialForm where  Validate='v' group by RWID,RWIDDID,Validate) c on b.RWID = c.RWID and b.DID=c.RWIDDID where b.RWID='"+RWID+"' and (OrderNum-isnull(c.Amount,0))>0 ";

                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
            else
            {
                string sql = "select distinct DID,PID,OrderContent,SpecsModels,OrderUnit,OrderNum,Remark from BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";

                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }

        }

        public static DataTable getTnum(string DID)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");

            string strInsert = "select count(Amount) from BGOI_Produce.dbo.tk_MaterialForm where RWIDDID='" + DID + "' and Validate='v'";
            if (strInsert != "")
            {
                count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
            }
            trans.Close(true);
            if (count > 0)
            {
                string sql = "select (OrderNum-sum (Amount)) OrderNum" +
 " from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_MaterialForm b" +
 " where a.DID=b.RWIDDID and a.RWID=b.RWID and a.DID='" + DID + "'" +
 " group by OrderNum";
                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
            else
            {
                string sql = "select OrderNum from BGOI_Produce.dbo.tk_ProductTDatail where DID='" + DID + "'";
                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
        }

        public static DataTable getLLstate(string RWID)
        {
            int a;
            string sql = "select(OrderNum-sum(Amount)) OrderNum,RWIDDID from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b where a.RWID='" + RWID + "' and a.RWID=b.RWID and a.RWIDDID=b.DID group by OrderNum,RWIDDID";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var b = dr["RWIDDID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  Lstate='1'where DID='" + b + "';";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static DataTable getLLdetail(string LLID, string RWID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where Lstate<>'1' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update [BGOI_Produce].[dbo].[tk_ProductTask] set LLstate='1',Materialcompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "';  update BGOI_Sales.dbo.OrdersInfo set Pstate='2' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'); Insert into BGOI_Produce .dbo.tk_Log(LogTime,YYCode,YYType,Content,Target,Actor,Unit)Values('" + DateTime.Now + "','" + RWID + "','添加成功 ','新增领料单',null,'" + GAccount.GetAccountInfo().UserName + "','" + GAccount.GetAccountInfo().UnitName + "')";
                        //update [BGOI_Produce].[dbo].[tk_MaterialForm] set state='1' where LLID='" + LLID + "';
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static bool SaveMaterialFDetailIn(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, DateTime NCreateTime, string NCreateUser, string RWID, string RWIDDID, string HouseID)
        {
            strErr = "";
            foreach (tk_MaterialFDetail SID in delist)
            {
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "' and HouseID='" + HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "零件" + SID.PID + "在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(SID.OrderNum))
                    {
                        strErr = "零件" + SID.PID + "数量大于库存数量,库存数量为"+dt.Rows[0][0]+"";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            int count = 0;
            int m = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string a = GSqlSentence.GetInsertInfoByD<tk_MaterialForm>(record, "[BGOI_Produce].[dbo].tk_MaterialForm");
                if (a != "")
                {
                    m = SQLBase.ExecuteNonQuery(a);
                }
                string strInsert = "select count(*) from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                if (strInsert != "")
                {
                    count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
                }
                trans.Close(true);
                if (count > 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_MaterialFDetail SID in delist)
                        {
                            strInsertList = "Insert into BGOI_Produce.dbo.tk_MaterialFDetail_HIS(LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Technology,Remark,Validate,NCreateTime,NCreateUser) select LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Technology,Remark ,Validate,'" + NCreateTime + "','" + NCreateUser + "',IdentitySharing from BGOI_Produce.dbo.tk_MaterialFDetail where DID='" + SID.DID + "'; update [BGOI_Produce].[dbo].[tk_MaterialFDetail] set LLID='" + SID.LLID + "', DID='" + MaterialFDetailNum(SID.LLID) + "',PID='" + SID.PID + "',OrderContent='" + SID.OrderContent + "',SpecsModels='" + SID.SpecsModels + "', Manufacturer='" + SID.Manufacturer + "',OrderUnit='" + SID.OrderUnit + "',OrderNum='" + SID.OrderNum + "',Technology='" + SID.Technology + "',Remark='" + SID.Remark + "',Validate='" + SID.Validate + "',IdentitySharing='" + SID.IdentitySharing + "' where LLID='" + LLID + "';";
                            //update BGOI_Produce.dbo.tk_ProductTDatail set State='5' where  DID='" + RWIDDID + "'
                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList);
                            }
                            getLLstate(RWID);
                            getLLdetail(SID.LLID, RWID);
                        }


                    }
                }

                if (count == 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_MaterialFDetail SID in delist)
                        {
                            strInsertList = "Insert into [BGOI_Produce].[dbo].[tk_MaterialFDetail](LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Technology,Remark,Validate,IdentitySharing) " +
                                "values ('" + SID.LLID + "','" + MaterialFDetailNum(SID.LLID) + "','" + SID.PID + "','" + SID.OrderContent + "','" + SID.SpecsModels + "','" + SID.Manufacturer + "','" + SID.OrderUnit + "','" + SID.OrderNum + "','" + SID.Technology + "','" + SID.Remark + "','" + SID.Validate + "','" + SID.IdentitySharing + "');update BGOI_Produce.dbo.tk_ProductTask set  LLstate='0'where RWID='" + RWID + "';";
                            //update BGOI_Produce.dbo.tk_ProductTDatail set State='5' where  DID='" + RWIDDID + "'

                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList);
                            }
                            getLLstate(RWID);
                            getLLdetail(SID.LLID, RWID);
                        }
                    }

                }
                return true;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool SaveUpdateMaterialTask(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, string RWIDDID, string OrderNum)
        {
            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_MaterialForm set ID=@ID," + "MaterialDepartment=@MaterialDepartment,CreateTime=@CreateTime,OrderContent=@OrderContent,SpecsModels=@SpecsModels,MaterialTime=@MaterialTime,Amount=@Amount where LLID=@LLID";
                SqlParameter[] param ={
                                       new SqlParameter ("@ID",SqlDbType.NVarChar ),
                                       new SqlParameter ("@MaterialDepartment",SqlDbType .NVarChar ),
                                       new SqlParameter ("@CreateTime",SqlDbType .DateTime ),
                                       new SqlParameter ("@OrderContent",SqlDbType .NVarChar ),
                                       new SqlParameter ("@SpecsModels",SqlDbType .NVarChar ),
                                       new SqlParameter ("@MaterialTime",SqlDbType .DateTime ),
                                       new SqlParameter ("@Amount",SqlDbType .Int ),
                                       new SqlParameter ("@LLID",SqlDbType .NVarChar )
                                     };

                param[0].Value = record.ID;
                param[1].Value = record.MaterialDepartment;
                param[2].Value = record.CreateTime;
                param[3].Value = record.OrderContent;
                param[4].Value = record.SpecsModels;
                param[5].Value = record.MaterialTime;
                param[6].Value = record.Amount;
                param[7].Value = record.LLID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_MaterialForm_HIS (LLID,RWID,ID,MaterialDepartment,RWIDDID,Amount,OrderContent,SpecsModels,MaterialTime,Remark,State,CreateUser,CreateTime,Validate,CancelTime,CancelReason,FinishTime,NCreateTime,NCreateUser)" +
                "select LLID,RWID,ID,MaterialDepartment,RWIDDID,Amount,OrderContent,SpecsModels,MaterialTime,Remark,State,CreateUser,CreateTime,Validate,CancelTime,CancelReason,FinishTime,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_MaterialForm where LLID ='" + record.LLID + "'";

                string a = "insert into BGOI_Produce.dbo.tk_ProductTDatail_HIS (RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,NCreateTime,NCreateUser) select RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_ProductTDatail where DID ='" + RWIDDID + "'";
                string b = "update BGOI_Produce.dbo.tk_ProductTDatail set OrderNum='" + OrderNum + "' where DID='" + RWIDDID + "'";
                if (a != "")
                {
                    SQLBase.ExecuteNonQuery(a, "MainProduce");
                    SQLBase.ExecuteNonQuery(b, "MainProduce");
                }

                string insertHis = "";
                string deleteall = "";
                string insertdetail = "";

                insertHis = "insert into BGOI_Produce.dbo.tk_MaterialFDetail_HIS(LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,NCreateUser,NCreateTime)" +
                                  "select LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                                  " from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                deleteall = "delete from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                if (insertHis != "")
                {
                    SQLBase.ExecuteNonQuery(insertHis, "MainProduce");
                    SQLBase.ExecuteNonQuery(deleteall, "MainProduce");
                }
                if (delist.Count > 0)
                {
                    foreach (tk_MaterialFDetail item in delist)
                    {
                        insertdetail = "insert into BGOI_Produce.dbo.tk_MaterialFDetail(LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Technology,Remark,Validate) values('" + LLID + "','" + MaterialFDetailNums(LLID) + "','" + item.PID + "','" + item.OrderContent + "','" + item.SpecsModels + "','" + item.Manufacturer + "','" + item.OrderUnit + "','" + item.OrderNum + "','" + item.Technology + "','" + item.Remark + "','v')";

                        if (insertdetail != "")
                        {
                            SQLBase.ExecuteNonQuery(insertdetail, "MainProduce");
                        }
                    }
                }

                //string strUpdateList = "";
                //string strInsertDetailHIS = "";
                //if (delist.Count > 0)
                //{
                //    foreach (tk_MaterialFDetail item in delist)
                //    {
                //        strUpdateList = "update BGOI_Produce.dbo.tk_MaterialFDetail set DID='" + item.DID + "', OrderContent='" + item.OrderContent + "'," +
                //            "SpecsModels='" + item.SpecsModels + "',Specifications='" + item.Specifications + "',Manufacturer='" + item.Manufacturer + "'," +
                //            "OrderUnit='" + item.OrderUnit + "',OrderNum='" + item.OrderNum + "',Technology='" + item.Technology + "',Remark='" + item.Remark + "' where DID='" + item.DID + "'";
                //        strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_MaterialFDetail_HIS(LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,NCreateUser,NCreateTime)" +
                //              "select LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                //              " from BGOI_Produce.dbo.tk_MaterialFDetail where DID='" + item.DID + "'";
                //        if (strUpdateList != "")
                //        {
                //            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                //            SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");

                //        }
                //    }
                //}
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        public static DataTable getLLstates(string RWID)
        {
            int a;
            string sql = "select(OrderNum-sum(Amount)) OrderNum,RWIDDID from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b where a.RWID='" + RWID + "' and a.RWID=b.RWID and a.RWIDDID=b.DID and a.Validate='v' group by OrderNum,RWIDDID";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var b = dr["RWIDDID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='1'where DID='" + b + "';";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='5'where DID='" + b + "';";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static DataTable getLLdetails(string LLID, string RWID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where State<>'1' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update [BGOI_Produce].[dbo].[tk_ProductTask] set LLstate='1',Materialcompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='2' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";
                        //update [BGOI_Produce].[dbo].[tk_MaterialForm] set state='1' where LLID='" + LLID + "';
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update [BGOI_Produce].[dbo].[tk_ProductTask] set LLstate='0',Materialcompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='1' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";
                        //update [BGOI_Produce].[dbo].[tk_MaterialForm] set state='1' where LLID='" + LLID + "';
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }


        public static bool SaveUpdateMaterialForm(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, string RWID)
        {

            strErr = "";
            foreach (tk_MaterialFDetail SID in delist)
            {
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "零件" + SID.PID + "在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(SID.OrderNum))
                    {
                        strErr = "零件" + SID.PID + "数量大于库存数量,库存数量为" + dt.Rows[0][0] + "";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            
            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_MaterialForm set RWID=@RWID,ID=@ID," + "MaterialDepartment=@MaterialDepartment,CreateTime=@CreateTime,OrderContent=@OrderContent,SpecsModels=@SpecsModels,MaterialTime=@MaterialTime,Amount=@Amount,HouseID=@HouseID where LLID=@LLID";
                SqlParameter[] param ={
                                       new SqlParameter ("@RWID",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@ID",SqlDbType.NVarChar ),
                                       new SqlParameter ("@MaterialDepartment",SqlDbType .NVarChar ),
                                       new SqlParameter ("@CreateTime",SqlDbType .DateTime ),
                                       new SqlParameter ("@OrderContent",SqlDbType .NVarChar ),
                                       new SqlParameter ("@SpecsModels",SqlDbType .NVarChar ),
                                       new SqlParameter ("@MaterialTime",SqlDbType .DateTime ),
                                       new SqlParameter ("@Amount",SqlDbType .Int ),
                                       new SqlParameter ("@HouseID",SqlDbType .NVarChar ),
                                       new SqlParameter ("@LLID",SqlDbType .NVarChar )
                                     };
                param[0].Value = record.RWID;
                param[1].Value = record.ID;
                param[2].Value = record.MaterialDepartment;
                param[3].Value = record.CreateTime;
                param[4].Value = record.OrderContent;
                param[5].Value = record.SpecsModels;
                param[6].Value = record.MaterialTime;
                param[7].Value = record.Amount;
                param[8].Value = record.LLID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_MaterialForm_HIS (LLID,RWID,ID,MaterialDepartment,RWIDDID,Amount,OrderContent,SpecsModels,MaterialTime,Remark,State,CreateUser,CreateTime,Validate,CancelTime,CancelReason,FinishTime,NCreateTime,NCreateUser)" +
                "select LLID,RWID,ID,MaterialDepartment,RWIDDID,Amount,OrderContent,SpecsModels,MaterialTime,Remark,State,CreateUser,CreateTime,Validate,CancelTime,CancelReason,FinishTime,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_MaterialForm where LLID ='" + record.LLID + "'";

                string insertHis = "";
                string deleteall = "";
                string insertdetail = "";

                insertHis = "insert into BGOI_Produce.dbo.tk_MaterialFDetail_HIS(LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,NCreateUser,NCreateTime,IdentitySharing)" +
                                  "select LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "',IdentitySharing" +
                                  " from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                deleteall = "delete from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                if (insertHis != "")
                {
                    SQLBase.ExecuteNonQuery(insertHis, "MainProduce");
                    SQLBase.ExecuteNonQuery(deleteall, "MainProduce");
                }


                if (delist.Count > 0)
                {
                    foreach (tk_MaterialFDetail item in delist)
                    {
                        insertdetail = "insert into BGOI_Produce.dbo.tk_MaterialFDetail(LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Technology,Remark,Validate,IdentitySharing) values('" + LLID + "','" + MaterialFDetailNums(LLID) + "','" + item.PID + "','" + item.OrderContent + "','" + item.SpecsModels + "','" + item.Manufacturer + "','" + item.OrderUnit + "','" + item.OrderNum + "','" + item.Technology + "','" + item.Remark + "','v','"+item.IdentitySharing+"')";

                        if (insertdetail != "")
                        {
                            SQLBase.ExecuteNonQuery(insertdetail, "MainProduce");
                        }
                        //getLLstates(RWID);
                        //getLLdetails(LLID, RWID);
                        getLLstate(RWID);
                        getLLdetail(item.LLID, RWID);
                    }
                }
                //string strUpdateList = "";
                //string strInsertDetailHIS = "";
                //if (delist.Count > 0)
                //{
                //    foreach (tk_MaterialFDetail item in delist)
                //    {
                //        strUpdateList = "update BGOI_Produce.dbo.tk_MaterialFDetail set DID='" + item.DID + "', OrderContent='" + item.OrderContent + "'," +
                //            "SpecsModels='" + item.SpecsModels + "',"+
                //        //Specifications='" + item.Specifications + "',
                //       " Manufacturer='" + item.Manufacturer + "'," +
                //            "OrderUnit='" + item.OrderUnit + "',OrderNum='" + item.OrderNum + "',Technology='" + item.Technology + "',Remark='" + item.Remark + "' where DID='" + item.DID + "'";
                //        strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_MaterialFDetail_HIS(LLID,DID,PID,OrderContent,SpecsModels,"+
                //        //Specifications,
                //        "Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,NCreateUser,NCreateTime)" +
                //              "select LLID,DID,PID,OrderContent,SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,BatchID,DeliveryTime,Remark,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                //              " from BGOI_Produce.dbo.tk_MaterialFDetail where DID='" + item.DID + "'";
                //        if (strUpdateList != "")
                //        {
                //            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                //            SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");

                //        }
                //    }
                //}
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        public static DataTable GetMaterial(string PID)
        {
            string sql = "select a.PID,a.ProName,a.Spec,a.Units,b.Number,a.Remark,IdentitySharing from BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_Inventory.dbo.  tk_ProFinishDefine b where ProTypeID ='1' and PID = PartPID and ProductID  = '" + PID + "' order by IdentitySharing";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static DataTable Getcount(string PID, string a)
        {
            string sql = "select distinct  IdentitySharing from BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_Inventory.dbo.  tk_ProFinishDefine b where ProTypeID ='1' and PID = PartPID and ProductID ='" + PID + "' and IdentitySharing='" + a + "' order by IdentitySharing";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }


        public static DataTable GetZZ(string LLID, string IdentitySharing)
        {
            string sql = "select convert(varchar(10),DeliveryTime,120) a,* from BGOI_Produce.dbo.tk_MaterialFDetail where LLID like '%" + LLID + "%' and Validate='v' and IdentitySharing='" + IdentitySharing + "' order by IdentitySharing";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static DataTable GetLLXQNum(string LLID, string IdentitySharing)
        {
            string sql = "select  COUNT(*) a from BGOI_Produce.dbo.tk_MaterialFDetail  where LLID like '%" + LLID + "%' and Validate='v' and IdentitySharing='" + IdentitySharing + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static DataTable GetZSL(string LLID)
        {
            string sql = "select distinct  IdentitySharing from BGOI_Produce.dbo.tk_MaterialFDetail where  LLID like '%" + LLID + "%' and Validate='v' order by IdentitySharing ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static DataTable GetNULL(string LLID)
        {
            string sql = "select convert(varchar(10),DeliveryTime,120) a,* from BGOI_Produce.dbo.tk_MaterialFDetail where LLID like '%" + LLID + "%' and Validate='v' and IdentitySharing='' order by IdentitySharing  ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion

        #region 撤销任务单
        public static bool CheXiaoTask(string RWID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set Validate='i',State='-2',CancelTime='" + DateTime.Now + "' where RWID='" + RWID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 打印
        public static DataTable PrintTasks(string strWhere, string tableName, ref string strErr)
        {
            String strField = "select  * from " + tableName + " " + strWhere + "";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }
        public static DataTable PrintTask(string RWID)
        {
            String strField = "select convert(varchar(10),DeliveryTime,120) a, * from BGOI_Produce.dbo.tk_ProductTDatail where  RWID like '%" + RWID + "%'";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }

        public static DataTable GetApprovalTime(string PID)
        {
            String strField = "select MIN(ApprovalLevel),UserName,convert(varchar(100),ApprovalTime,23) ApprovalTime from  BGOI_Produce.dbo.tk_Approval left join BJOI_UM.dbo.UM_UserNew on UserId=ApprovalMan where PID='" + PID + "' group by UserName,ApprovalTime";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成随工单
        public static string GetTopSGID()
        {
            string strID = "";
            string strD = "SG" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(SGID) from [BGOI_Produce].[dbo].[tk_ProductRecord]";
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

        public static string ProductRProductNum(string SGID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(SGDID) From [BGOI_Produce].[dbo].[tk_ProductRProduct]";
            DataTable dt = SQLBase.FillTable(sqlstr);

            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = SGID + "-P" + "01";
                return Num;
            }
            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != SGID)
                {
                    Num = SGID + "-P" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = SGID + "-P" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = SGID + "-P" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }


        public static DataTable GetProductRecord(string RWID)
        {

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");

            string strInsert = "select COUNT(OrderNum) from BGOI_Produce.dbo.tk_ProductRProduct where SGID in (select SGID from   BGOI_Produce.dbo.tk_ProductRecord where RWID in (select RWID from BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'))";
            if (strInsert != "")
            {
                count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
            }
            trans.Close(true);
            if (count > 0)
            {
                string sql = "select b.DID, b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,(b.OrderNum-isnull(c.Amount,0)) OrderNum,b.photo,b.Remark,(b.OrderNum-isnull(c.Amount,0)) Amount from BGOI_Produce.dbo.tk_ProductTDatail b left join  (select DID,PID,SUM(OrderNum)Amount from BGOI_Produce.dbo.tk_ProductRProduct a,BGOI_Produce.dbo.tk_ProductRecord b where a.SGID=b.SGID and b.Validate='v'  group by DID,PID) c on b.DID = c.DID and b.PID=c.PID where b.RWID='" + RWID + "'and (b.OrderNum-isnull(c.Amount,0))>0";

                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
            else
            {
                string sql = "select PID,DID,OrderContent,SpecsModels,OrderUnit,OrderNum,photo,Remark,OrderNum Amount from    BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";
                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
        }

        public static DataTable GetProductRecordDID(string DID)
        {
            string sql = "select PID,DID,OrderContent,SpecsModels,OrderUnit,OrderNum,photo,Remark from BGOI_Produce.dbo.tk_ProductTDatail where DID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable getSGtask(string RWID)
        {
            int a;
            string sql = "select (a.OrderNum-SUM(b.OrderNum)) m,a.DID,RWID from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b where a.DID=b.DID and RWID='" + RWID + "' group by a.OrderNum,a.DID ,RWID ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                var b = dr["DID"].ToString();
                if (a == 0)
                {
                    string c = "select state from BGOI_Produce.dbo.tk_ProductTDatail where  DID='" + b + "'";
                    DataTable dt1 = SQLBase.FillTable(c, "MainProduce");
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        int d = Convert.ToInt32(dr1["state"]);

                        if (d == 4)
                        {
                            int count = 0;
                            SQLTrans trans = new SQLTrans();
                            trans.Open("MainProduce");
                            try
                            {
                                string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  Sstate='1'where DID='" + b + "'";
                                if (strInsert != "")
                                {
                                    count = SQLBase.ExecuteNonQuery(strInsert);
                                }
                                trans.Close(true);
                            }
                            finally
                            {

                            }
                        }
                        else
                        {
                            int count = 0;
                            SQLTrans trans = new SQLTrans();
                            trans.Open("MainProduce");
                            try
                            {
                                string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='2'where DID='" + b + "';update BGOI_Produce.dbo.tk_ProductTDatail set  Sstate='1'where DID='" + b + "'";
                                if (strInsert != "")
                                {
                                    count = SQLBase.ExecuteNonQuery(strInsert);
                                }
                                trans.Close(true);
                            }
                            finally
                            {

                            }
                        }
                    }
                }
            }
            return dt;
        }

        public static DataTable getsgtaskdetail(string RWID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where Sstate<>'1' and RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update [BGOI_Produce].[dbo].[tk_ProductTask] set State='4' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='3' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";

                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
            }
            return dt;
        }

        public static bool SaveProductRecordIn(tk_ProductRecord record, List<tk_ProductRProduct> delist, ref string strErr, string RWID, string SGID, string HouseID)
        {

            int count = 0;
            int b = 0;
            var count1 = "";
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_ProductRecord>(record, "[BGOI_Produce].[dbo].tk_ProductRecord");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                string sql = "select count(*) from BGOI_Produce.dbo.tk_ProductRProduct where SGID='" + SGID + "'";
                if (sql != "")
                {
                    b = Convert.ToInt32(SQLBase.ExecuteScalar(sql));
                }

                trans.Close(true);

                if (b == 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_ProductRProduct SID in delist)
                        {
                            strInsertList = "Insert into [BGOI_Produce].[dbo].[tk_ProductRProduct](SGID,SGDID,DID,PID,OrderNum) " +
                                "values ('" + SID.SGID + "','" + ProductRProductNum(SID.SGID) + "','" + SID.DID + "','" + SID.PID + "','" + SID.OrderNum + "');";
                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList);
                            }
                            //取在线生产数量
                            string sql1 = "select OnlineCount from BGOI_Inventory.dbo.tk_StockRemain where HouseID  ='" + HouseID + "' and ProductID='" + SID.PID + " ' ";
                            DataTable dt = SQLBase.FillTable(sql1, "MainProduce");
                            foreach (DataRow dr in dt.Rows)
                            {
                                count1 = dr["OnlineCount"].ToString();
                            }

                            //判断是否为空
                            if (dt.Rows.Count==0)
                            {
                                string a = "insert into BGOI_Inventory.dbo.tk_StockRemain(ProductID,OnlineCount,HouseID,FinishCount) values ('" + SID.PID + "'," + SID.OrderNum + ",'" + HouseID + "','0')";
                                if (a != "")
                                {
                                    SQLBase.ExecuteNonQuery(a);
                                }
                            }
                            else
                            {
                                int OrderNum = SID.OrderNum;
                                int OnlineCount = Convert.ToInt32(count1) + OrderNum;
                                string insertProductLog = "Insert into BGOI_Produce .dbo.tk_Log(LogTime,YYCode,YYType,Content,Target,Actor,Unit)Values('" + DateTime.Now + "','" + SID.PID + "','修改在线生产数量成功 ','修改在线生产数量',null,'" + GAccount.GetAccountInfo().UserName + "','" + GAccount.GetAccountInfo().UnitName + "')";
                                if (insertProductLog != "")
                                {
                                    SQLBase.ExecuteNonQuery(insertProductLog);
                                }
                                string update = "update [BGOI_Inventory].[dbo].[tk_StockRemain] set OnlineCount='" + OnlineCount + "' where ProductID='" + SID.PID + "' and HouseID='" + HouseID + "'";
                                if (update != "")
                                {
                                    SQLBase.ExecuteNonQuery(update);
                                }
                            }
                            getSGtask(RWID);
                            getsgtaskdetail(RWID);
                        }
                    }
                    return true;
                }

                if (b > 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_ProductRProduct SID in delist)
                        {
                            strInsertList = "Insert into BGOI_Produce.dbo.tk_ProductRProduct_HIS(SGID,SGDID,DID,PID,OrderNum,NCreateTime,NCreateUser) select SGID,SGDID,DID,PID,OrderNum,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'from BGOI_Produce.dbo.tk_ProductRProduct where SGDID='" + SID.SGDID + "'; update [BGOI_Produce].[dbo].[tk_ProductRProduct] set SGID='" + SID.SGID + "', SGDID='" + ProductRProductNum(SID.SGID) + "',DID='" + SID.DID + "',PID='" + SID.PID + "',OrderNum='" + SID.OrderNum + "' where SGDID='" + SID.SGDID + "'";
                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList);
                            }
                            getSGtask(RWID);
                            getsgtaskdetail(RWID);

                        }
                    }
                    return true;
                }

                else
                    return false;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 检验报告上传
        public static string GetTopBGID()
        {
            string strID = "";
            string strD = "BG" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(BGID) from [BGOI_Produce].[dbo].[tk_ReportInfo]";
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

        public static string ProFileInfoNum(string BGID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_FileInfo]";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = BGID + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != BGID)
                {
                    Num = BGID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = BGID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = BGID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }
        public static string ProFileInfoNums(string BGID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_FileInfo] where BGID='" + BGID + "'";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Num = BGID + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != BGID)
                {
                    Num = BGID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = BGID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = BGID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static DataTable GetReportInfo(string RWID, string RKID)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");

            string strInsert = "select COUNT(Amount) from BGOI_Produce.dbo.tk_PStockingDetail where RKID in (select RKID from   BGOI_Produce.dbo.tk_PStocking where RWID in (select RWID from BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'))";
            if (strInsert != "")
            {
                count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
            }
            trans.Close(true);
            if (count > 0)
            {
                string sql = "select distinct a.DID,a.PID,a.OrderContent,a.SpecsModels,a.OrderUnit,a.photo,a.Remark,(m.OrderNum-isnull(Amount,0)) ordernum,(m.OrderNum-isnull(Amount,0)) ordernums from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRecord d,BGOI_Produce.dbo.tk_ProductRProduct e left join  (select SUM(OrderNum) OrderNum,DID from BGOI_Produce.dbo.tk_ProductRecord m,BGOI_Produce.dbo.tk_ProductRProduct n where RWID='" + RWID + "' and m.SGID=n.SGID group by did) m on e.DID=m.DID left join (select sum(Amount) Amount,RWIDDID from BGOI_Produce.dbo.tk_PStockingDetail b,BGOI_Produce.dbo.tk_PStocking c where b.RKID=c.RKID and c.Validate='v'  and RWID='" + RWID + "'  group by RWIDDID) n on m.DID=n.RWIDDID where d.SGID=e.SGID and a.DID=e.DID  and a.RWID ='" + RWID + "'  and (m.OrderNum-isnull(Amount,0))>0";
                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }
            else
            {
                string sql = "select c.PID,c.DID,c.OrderContent,c.SpecsModels,c.OrderUnit,sum(b.OrderNum) ordernum,c.photo,c.Remark,sum(b.OrderNum) ordernums from BGOI_Produce.dbo.tk_ProductRecord a,BGOI_Produce.dbo.tk_ProductRProduct b,BGOI_Produce.dbo.tk_ProductTDatail c where a.SGID=b.SGID and c.RWID='" + RWID + "' and c.DID=b.DID and a.Validate='v' group by c.PID,c.DID,c.OrderContent,c.SpecsModels,c.OrderUnit,c.photo,c.Remark";
                DataTable dt = SQLBase.FillTable(sql, "MainProduce");
                return dt;
            }

        }

        public static DataTable GetReportInfoselect(string RWID)
        {
            string sql = "select PID,DID,OrderContent,SpecsModels,OrderUnit,OrderNum,photo,Remark,OrderNum Amount from    BGOI_Produce.dbo.tk_ProductTDatail where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable GetReportInfos(string DID)
        {
            string sql = "select PID,DID,OrderContent,SpecsModels,OrderUnit,OrderNum,photo,Remark,OrderNum Amount from    BGOI_Produce.dbo.tk_ProductTDatail where DID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable GetBGType()
        {
            string str = "select Text from BGOI_Produce.dbo.tk_ConfigContent where SID='上传的文件类型'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static bool SaveFileInfo(tk_FileInfo info)
        {

            SqlParameter[] sqlPar = new SqlParameter[]
            {
                new SqlParameter("@FileInfo",info.FileInfo)
            };

            string sql = "Insert into tk_FileInfo(BGID,DID,Type,FileName,FileType,FileInfo,CreatePerson,CreateTime,Validate)Values('" + info.BGID + "','" + info.DID + "','" + info.Type + "','" + info.FileName + "','" + info.FileType + "',@FileInfo,'" + info.CreatePerson + "','" + info.CreateTime + "','" + info.Validate + "')";
            string cnn = "MainProduce";
            return SQLBase.ExecuteNonQuery(sql, sqlPar, cnn) > 0;
        }

        public static bool SaveFileInfoIn(tk_ReportInfo record, List<tk_FileInfo> delist, ref string strErr, string BGID, string CreatePerson, DateTime CreateTime)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_ReportInfo>(record, "[BGOI_Produce].[dbo].tk_ReportInfo");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }

                trans.Close(true);

                if (count > 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_FileInfo SID in delist)
                        {
                            SqlParameter[] para = new SqlParameter[]{
                                new SqlParameter("@FileInfo",SID.FileInfo)
                            };
                            strInsertList = "insert into [BGOI_Produce].[dbo].[tk_FileInfo](BGID,DID,Type,FileName,FileInfo,CreatePerson,CreateTime,Validate)Values('" + SID.BGID + "','" + ProFileInfoNum(SID.BGID) + "','" + SID.Type + "','" + SID.FileName + "',@FileInfo,'" + SID.CreatePerson + "','" + SID.CreateTime + "','" + SID.Validate + "')";

                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList, para);
                            }
                        }
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
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 完成入库
        public static string GetTopRKID()
        {
            string strID = "";
            string strD = "RK" + "-" + DateTime.Now.ToString("yyyyMMdd");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            string strSqlID = "select max(RKID) from [BGOI_Produce].[dbo].[tk_PStocking]";
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

        public static DataTable GetHouseID()
        {
            string str = "select HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static DataTable GetHouseIDs()
        {
            string str = "select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static string PStockingDetailNum(string RKID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_PStockingDetail]";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = RKID + "-" + "01";
                return Num;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != RKID)
                {
                    Num = RKID + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = RKID + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = RKID + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static DataTable getRKstate(string RWID)
        {
            int a;
            string sql = "select (a.OrderNum-isnull(c.num,0)) OrderNum,DID from BGOI_Produce.dbo.tk_ProductTDatail a left join (select SUM(Amount) as num,RWIDDID from BGOI_Produce.dbo.tk_PStockingDetail m,BGOI_Produce.dbo.tk_PStocking n where m.RKID=n.RKID and n.Validate='v' and   RWID ='" + RWID + "' group by RWIDDID) c on a.DID=c.rwiddid where  RWID ='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var DID = dr["DID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='4'where DID='" + DID + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static DataTable getrkTaskstate(string RWID, string RKID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where State<>'4' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='6' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='4' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "') ";


                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static bool SavePStockingDetailIn(tk_PStocking record, List<tk_PStockingDetail> delist, ref string strErr, string RWID, DateTime FinishTime)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_PStocking>(record, "[BGOI_Produce].[dbo].tk_PStocking");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }

                trans.Close(true);

                if (count > 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_PStockingDetail SID in delist)
                        {
                            strInsertList = "insert into BGOI_Produce.dbo.tk_PStockingDetail (RKID,DID,PID,OrderContent,Specifications,Unit,Amount,Remark,RWIDDID) values('" + SID.RKID + "','" + PStockingDetailNum(SID.RKID) + "','" + SID.PID + "','" + SID.OrderContent + "','" + SID.Specifications + "','" + SID.Unit + "','" + SID.Amount + "','" + SID.Remark + "','" + SID.RWIDID + "');";
                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList);
                            }

                            getRKstate(RWID);
                            getrkTaskstate(RWID, SID.RKID);

                        }
                    }
                    return true;
                }



                else
                    return false;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion
        #endregion

        #region 领料单管理

        #region 领料单列表
        public static UIDataTable Materialrequisitionlist(int a_intPageSize, int a_intPageIndex, string where)
        {

            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProductMaterial", CommandType.StoredProcedure, sqlPar, "MainProduce");
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
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["state"] = GetSelectLL(instData.DtData.Rows[r]["state"].ToString()).name;
                }
            }

            return instData;

        }

        public static DataTable ProMaterialFDetail(string LLID)
        {
            string sql = "select distinct b.PID,DID,b.OrderContent,b.SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,b.Remark from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_MaterialFDetail b where a.LLID=b.LLID and a.LLID='" + LLID + "'and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable GetNAME()
        {
            string str = "select distinct OrderContent from BGOI_Produce.dbo.tk_MaterialFDetail";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static DataTable GetTYPE()
        {
            string str = "select distinct SpecsModels from BGOI_Produce.dbo.tk_MaterialFDetail";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static bool SCLLDetail(string DID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_MaterialFDetail set Validate='i' where DID='" + DID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "删除失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region  领料单页面 新增领料单
        public static DataTable getOrderContent()
        {
            string str = "select distinct ProName OrderContent from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static UIDataTable gettaskll(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select Count(*)  from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " ProTypeID='2' " + where;
            string strOrderBy = " ProName ";
            String strTable = "  BGOI_Inventory.dbo.tk_ProductInfo  ";
            String strField = " ProName,Spec ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }

        public static string GetTopID(string LLID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(RWIDDID) From [BGOI_Produce].[dbo].[tk_MaterialForm]";
            DataTable dt = SQLBase.FillTable(sqlstr);
            if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null)
            {
                Num = LLID + "-" + "W" + "-" + "01";
                return Num;
            }

            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != LLID)
                {
                    Num = LLID + "-" + "W" + "-" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = LLID + "-" + "W" + "-" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = LLID + "-" + "W" + "-" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static bool savesomematerial(tk_MaterialForm record, string PID, string Main, string LLID, string amount, string v, string OrderContent, string SpecsModels, string Remark, string orderUnit, string DID)
        {
            int count = 0;
            int m = 0;
            int count1 = 0;
            int count2 = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string a = GSqlSentence.GetInsertInfoByD<tk_MaterialForm>(record, "[BGOI_Produce].[dbo].tk_MaterialForm");
                if (a != "")
                {
                    m = SQLBase.ExecuteNonQuery(a);
                }
                string strInsert = "select count(*) from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "'";
                if (strInsert != "")
                {
                    count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
                }
                string d = "insert into BGOI_Produce.dbo.tk_ProductTDatail(DID,OrderContent,SpecsModels,OrderNum,Remark,OrderUnit,PID) values('" + DID + "','" + OrderContent + "','" + SpecsModels + "','" + amount + "','" + Remark + "','" + orderUnit + "','" + PID + "')";
                if (d != "")
                {
                    SQLBase.ExecuteNonQuery(d);
                }
                trans.Close(true);
                if (count == 0)
                {
                    string strInsertList = "";
                    //需要循环的长度
                    string strInsert1 = "select count(*) from BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_Inventory.dbo.  tk_ProFinishDefine b where ProTypeID ='1' and PID = PartPID and ProductID ='" + PID + "'";
                    if (strInsert1 != "")
                    {
                        count1 = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert1));
                    }
                    string strInsert2 = "select a.PID,a.ProName,a.Spec,b.Number,a.Units,a.Remark from BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_Inventory.dbo.  tk_ProFinishDefine b where ProTypeID ='1' and PID = PartPID and ProductID ='" + PID + "'";
                    DataTable dt = SQLBase.FillTable(strInsert2);
                    for (int i = 0; i < count1; i++)
                    {
                        var z = Convert.ToInt32(amount) * Convert.ToInt32(dt.Rows[i][3].ToString());
                        strInsertList = "insert into BGOI_Produce.dbo.tk_MaterialFDetail(LLID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Validate,Remark)values('" + LLID + "','" + MaterialFDetailNum(LLID) + "','" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + z + "' ,'" + v + "','" + dt.Rows[i][5].ToString() + "')";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }

                }
                return true;
            }

            catch (Exception ex)
            {

                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 修改领料单
        public static DataTable GetTaskNum(string LLID, string RWIDDID, string RWID)
        {
            string sql = "select * from (select Amount from BGOI_Produce.dbo.tk_MaterialForm where LLID='" + LLID + "') a,(select (OrderNum-isnull(c.Amount,0)) OrderNum from BGOI_Produce.dbo.tk_ProductTDatail b left join  (select RWID,RWIDDID,SUM(Amount)Amount from BGOI_Produce.dbo.tk_MaterialForm group by RWID,RWIDDID) c on b.RWID = c.RWID and b.DID=c.RWIDDID where b.RWID='" + RWID + "' and c.RWIDDID='" + RWIDDID + "')b";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable GetMaterialFormTaskdetail(string LLID)
        {
            string str = "select b.DID,b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,b.Remark,a.Amount from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b where a.RWIDDID=b.DID and  LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static DataTable GetMaterialFormDetail(string LLID)
        {
            string sql = "select PID,DID,b.OrderContent,b.SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,b.Remark,b.IdentitySharing from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_MaterialFDetail b where a.LLID=b.LLID and a.LLID='" + LLID + "' and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable IndexAllMaterialForm(string LLID)
        {
            string str = "select convert(varchar(10),MaterialTime,120) a,convert(varchar(10),CreateTime,120) b,* from BGOI_Produce.dbo.tk_MaterialForm where LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }


        #endregion

        #region 领料单详情
        public static DataTable GetMaterialFormDetails(string LLID)
        {
            string sql = "select distinct PID,DID,b.OrderContent,b.SpecsModels,Specifications,Manufacturer,OrderUnit,OrderNum,Technology,b.Remark from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_MaterialFDetail b where a.LLID=b.LLID and a.LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        public static DataTable IndexAllMaterialForms(string LLID)
        {
            string str = "select convert(varchar(10),MaterialTime,120) a,convert(varchar(10),CreateTime,120) b,* from BGOI_Produce.dbo.tk_MaterialForm where LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }


        #endregion

        #region 撤销领料单
        public static bool CXLL(string LLID, ref string strErr, string RWID)
        {
            int a;
            int b;
            string c = "";
            int d = 0;
            //string sql = "select(OrderNum-sum(Amount)) OrderNum,RWIDDID from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ProductTDatail b where a.LLID='"+LLID+"' and a.RWID=b.RWID and a.RWIDDID=b.DID group by OrderNum,RWIDDID";
            //DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    a =Convert.ToInt32(dr["OrderNum"]);
            //    b = Convert.ToInt32(dr["amount"]);
            //    c = dr["DID"].ToString();
            //    d = a + b;
            //}
            //string m = "insert into BGOI_Produce.dbo.tk_ProductTDatail_HIS(RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,NCreateUser,NCreateTime) select RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_ProductTDatail where DID ='" + c + "'";
            //if (m != "")
            //{
            //    SQLBase.ExecuteNonQuery(m);
            //}

            //string n = "update BGOI_Produce.dbo.tk_MaterialForm set Validate='i',State='-1',CancelTime='" + DateTime.Now + "' where LLID='" + LLID + "'";
            //if (n != "")
            //{
            //    SQLBase.ExecuteNonQuery(n);
            //}
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string n = "update BGOI_Produce.dbo.tk_MaterialForm set Validate='i',State='-1',CancelTime='" + DateTime.Now + "' where LLID='" + LLID + "'";
                if (n != "")
                {
                    count = SQLBase.ExecuteNonQuery(n);
                }
                //string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set OrderNum='"+d+"' where DID='" + c + "'";
                //if (strInsert != "")
                //{
                //    count = SQLBase.ExecuteNonQuery(strInsert);
                //}
                trans.Close(true);
                if (count > 0)
                {
                    getLLstates(RWID);
                    getLLdetails(LLID, RWID);
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 提交审批
        public static DataTable getTJLL(string LLID, ref string strErr)
        {
            string Sql = "select name from BGOI_Produce.dbo.tk_MaterialForm a,BGOI_Produce.dbo.tk_ConfigState where StateId=State and Type='LLstate' and  LLID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(Sql, "MainProduce");
            return dt;
        }
        #endregion

        #region 审批、
        public static DataTable getPDLLSP(string LLID, ref string strErr)
        {
            string str = "select * from BGOI_Produce.dbo.tk_Approval where RelevanceID='" + LLID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion


        #region 相关单据
        public static DataTable LoadLLs(string LLID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",LLID)
                };
            DataTable dt = SQLBase.FillTable("getLL", CommandType.StoredProcedure, sqlPar, "MainProduce");
            if (dt == null) return null;
            return dt;
        }

        public static UIDataTable LoadLLXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_MaterialForm " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "MaterialTime desc ";
            String strTable = " BGOI_Produce.dbo.tk_MaterialForm ";

            String strField = "LLID,MaterialDepartment,Amount,OrderContent,MaterialTime,Remark,State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "新建";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "-1")
                    {
                        instData.DtData.Rows[r]["State"] = "撤销";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "领料完成";
                    }
                }
            }
            return instData;
        }

        //领料单详细相关单据
        public static UIDataTable LoadLLXGDetail(int a_intPageSize, int a_intPageIndex, string LLID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from BGOI_Produce.dbo.tk_MaterialFDetail where LLID='" + LLID + "' and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "LLID='" + LLID + "' and Validate='v'";
            string strOrderBy = " LLID ";
            String strTable = "BGOI_Produce.dbo.tk_MaterialFDetail ";
            String strField = "OrderContent,SpecsModels,OrderUnit,OrderNum ,Technology ,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        #endregion

        #region 打印
        public static DataTable PrintLLs(string LLID)
        {
            String strField = "select convert(varchar(10),DeliveryTime,120) a,* from BGOI_Produce.dbo.tk_MaterialFDetail where LLID like '%" + LLID + "%' and Validate='v' order by IdentitySharing";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }

        public static DataTable PrintLL(string LLID)
        {
            string str = "select a.OrderContent m,a.SpecsModels n,* from BGOI_Produce.dbo.tk_MaterialForm b,BGOI_Produce.dbo.tk_ProductTDatail a where Validate='v' and a.DID=b.RWIDDID and LLID like '%" + LLID + "%'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 上传
        public static bool InsertFile(List<ProduceFile> list)
        {
            string sql = GSqlSentence.GetInsertByList<ProduceFile>(list, "ProduceFile");

            return SQLBase.ExecuteNonQuery(sql, "MainProduce") > 0;

        }
        #endregion

        #region 查看
        public static DataTable GetFiles(string OId)
        {
            string sql = "select FilePath from ProduceFile where Validate='V' and OID='" + OId + "' order by CreateTime";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        #endregion
        #endregion

        #region 随工单管理

        #region 撤销随工单
        public static bool CXSG(string SGID, ref string strErr, string RWID)
        {
            //int a;
            //int b;
            //string c = "";
            //int d = 0;
            //string sql = "select a.OrderNum,b.OrderNum amount ,a.DID from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b where  a.DID=b.DID and SGID='" + SGID + "'";
            //DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    a = Convert.ToInt32(dr["OrderNum"]);
            //    b = Convert.ToInt32(dr["amount"]);
            //    c = dr["DID"].ToString();
            //    d = a + b;
            //}
            //string m = "insert into BGOI_Produce.dbo.tk_ProductTDatail_HIS(RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,NCreateUser,NCreateTime) select RWID,DID,PID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Hasthematerialquantity,number,Technology,photo,DeliveryTime,State,Remark,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_ProductTDatail where DID ='" + c + "'";
            //if (m != "")
            //{
            //    SQLBase.ExecuteNonQuery(m);
            //}

            //string n = "update BGOI_Produce.dbo.tk_ProductRecord,a.DID set Validate='i',CancelTime='" + DateTime.Now + "' where SGID='" + SGID + "'";
            //if (n != "")
            //{
            //    SQLBase.ExecuteNonQuery(n);
            //}
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string n = "update BGOI_Produce.dbo.tk_ProductRecord  set Validate='i',CancelTime='" + DateTime.Now + "' where SGID='" + SGID + "'";
                if (n != "")
                {
                    count = SQLBase.ExecuteNonQuery(n);
                }
                //string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set OrderNum='" + d + "' where DID='" + c + "'";
                //if (strInsert != "")
                //{
                //    count = SQLBase.ExecuteNonQuery(strInsert);
                //}
                trans.Close(true);
                if (count > 0)
                {
                    getSGtask(RWID);
                    getsgtaskdetail(RWID);
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 删除随工单祥表
        public static bool SCSG(string DID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_ProductRDatail set Validate='i' where DID='" + DID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "删除失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 随工单列表
        public static UIDataTable withthejobList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProductMaterialForm", CommandType.StoredProcedure, sqlPar, "MainProduce");
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
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["state"] = GetSelectSG(instData.DtData.Rows[r]["state"].ToString()).name;
                }
            }
            return instData;

        }

        public static DataTable getname()
        {
            string str = "select distinct OrderContent from BGOI_Produce.dbo.tk_ProductTDatail";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        public static DataTable getspc()
        {
            string str = "select distinct SpecsModels from BGOI_Produce.dbo.tk_ProductTDatail";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static UIDataTable ProduceInDetials(int a_intPageSize, int a_intPageIndex, string SGID, string RWID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*)  from  BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b where a.DID=b.DID and b.SGID='" + SGID + "' and RWID='" + RWID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "a.DID=b.DID and b.SGID='" + SGID + "'and RWID='" + RWID + "'";
            string strOrderBy = "SGID ";
            String strTable = "BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b ";
            String strField = "a.PID,a.DID,a.OrderContent,a.SpecsModels,a.OrderUnit,b.OrderNum,a.photo,a.Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }

        public static DataTable ProProductRDatail(string SGID)
        {
            string sql = "select " + " DID,Process,team,convert(varchar(10),Estimatetime,120)  Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,convert(varchar(10),finishtime,120) finishtime,people,reason " +
 "from BGOI_Produce.dbo.tk_ProductRDatail where Validate='v' and SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        #endregion

        #region 记录单
        public static tk_ProductRecord getsumnum(string SGID)
        {
            string str = "select sum(Qualified) m from BGOI_Produce.dbo.tk_ProductRDatail where SGID ='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ProductRecord Material = new tk_ProductRecord();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToRecordss(item, Material);
            }
            return Material;
        }

        public static void DataRowToRecordss(DataRow item, tk_ProductRecord task)
        {
            task.m = item["m"].ToString();

        }


        public static tk_ProductRecord RDetail(string SGID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ProductRecord where SGID ='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ProductRecord Material = new tk_ProductRecord();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToRecord(item, Material);
            }
            return Material;
        }

        public static void DataRowToRecord(DataRow item, tk_ProductRecord task)
        {
            task.RWID = item["RWID"].ToString();
            task.ID = item["ID"].ToString();
            task.CreateUser = item["CreateUser"].ToString();
            task.SpecsModels = item["SpecsModels"].ToString();
            task.billing = Convert.ToDateTime(item["billing"]);
        }

        public static DataTable GetProductdetail(string SGID)
        {
            string sql = "select distinct a.DID,a.PID,a.OrderContent,a.SpecsModels,a.OrderUnit,b.OrderNum,a.photo,a.Remark" +
 " from  BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b where a.DID=b.DID and b.SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static string ProductRDatailNum(string SGID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DID) From [BGOI_Produce].[dbo].[tk_ProductRDatail] where SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sqlstr);

            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Num = SGID + "-R" + "01";
                return Num;
            }
            else
            {
                strid = dt.Rows[0][0].ToString();
                string strN = strid.Substring(0, 20);
                if (strid == "" || strid == null || strN != SGID)
                {
                    Num = SGID + "-R" + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = SGID + "-R" + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = SGID + "-R" + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }

        public static DataTable GetSGJLType()
        {
            string str = "select Text from BGOI_Produce.dbo.tk_ConfigContent where SID='随工单班组类型'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        //更新任务单从表
        public static DataTable getSGstate(string RWID, string DID)
        {
            int a;
            string sql = "select (OrderNum-isnull(b.Amount,0))  OrderNum,a.DID from  BGOI_Produce.dbo.tk_ProductTDatail a left join (select DID,SUM(OrderNum) amount from BGOI_Produce.dbo.tk_ProductRProduct where DID='" + DID + "' group by  DID,OrderNum) b on a.DID=b.DID where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var b = dr["DID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='3'where DID='" + b + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        //更新主表
        public static DataTable getTaskstate(string RWID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where State<>'3' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='5',Productioncompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static bool SaveProductRDatail(List<tk_ProductRDatail> delist, List<tk_ProductTDatail> ta, ref string strErr, string RWID, string SGID)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            try
            {
                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (tk_ProductRDatail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Produce].[dbo].[tk_ProductRDatail](SGID,DID,Process,team,Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,finishtime,people,reason,Validate,Technical)" +
                            "values ('" + SID.SGID + "','" + ProductRDatailNum(SID.SGID) + "','" + SID.Process + "','" + SID.team + "','" + SID.Estimatetime + "','" + SID.people + "','" + SID.plannumber + "','" + SID.Qualified + "','" + SID.number + "','" + SID.numbers + "','" + SID.Fnubers + "','" + SID.finishtime + "','" + SID.people + "','" + SID.reason + "','" + SID.Validate + "','" + SID.Technical + "');update [BGOI_Produce].[dbo].[tk_ProductRecord] set" + " State='1' where SGID ='" + SID.SGID + "'";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }

                    }
                    if (ta.Count > 0)
                    {
                        foreach (tk_ProductTDatail N in ta)
                        {
                            getSGstates(RWID, SGID);
                        }
                    }
                    getTaskstates(RWID);
                }
                return true;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 修改随工单

        public static tk_ProductRecord IndexAllupdateSG(string SGID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ProductRecord where SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ProductRecord Material = new tk_ProductRecord();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToupdateSG(item, Material);
            }
            return Material;
        }

        public static void DataRowToupdateSG(DataRow item, tk_ProductRecord task)
        {
            task.RWID = item["RWID"].ToString();
            task.ID = item["ID"].ToString();
            task.SpecsModels = item["SpecsModels"].ToString();
            task.billing = Convert.ToDateTime(item["billing"]);
            task.OrderContent = item["OrderContent"].ToString();
            task.Remark = item["Remark"].ToString();
            task.CreateUser = item["CreateUser"].ToString();
        }

        public static DataTable LoadRDatail(string SGID, string RWID)
        {
            string sql = "select distinct m.PID,m.OrderContent,m.SpecsModels,m.OrderUnit,m.OrderNum,m.OrderNum d,m.photo,m.Remark,m.SGDID,n.OrderNumss from (select a.PID,a.OrderContent,a.SpecsModels,a.OrderUnit,b.OrderNum,a.photo,a.Remark,(a.OrderNum-isnull(b.Amount,0)) OrderNumss,b.SGDID,b.SGID from BGOI_Produce.dbo.tk_ProductTDatail a,(select SGID,OrderNum,SGDID,DID,PID,SUM(OrderNum)Amount from BGOI_Produce.dbo.tk_ProductRProduct group by DID,PID,SGDID,OrderNum,SGID) b  where a.DID=b.DID and a.RWID='" + RWID + "' and b.SGID='" + SGID + "') m left join (select c.PID,(b.OrderNum-isnull(c.Amount,0)) OrderNumss from BGOI_Produce.dbo.tk_ProductTDatail b left join  (select a.DID,a.PID,(a.OrderNum)Amount from BGOI_Produce.dbo.tk_ProductRProduct a,BGOI_Produce.dbo.tk_ProductTDatail b,BGOI_Produce.dbo.tk_ProductRecord c where  b.RWID=c.RWID and a.SGID=c.SGID and c.Validate='v'and b.RWID='"+RWID+"' and a.SGID='"+SGID+"'  group by a.DID,a.PID,a.OrderNum) c on b.DID = c.DID and b.PID=c.PID where b.RWID='" + RWID + "')n  on m.PID=n.PID";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }


        public static DataTable getISSGdetail(String SGID)
        {
            string sql = "select COUNT(*) a from BGOI_Produce.dbo.tk_ProductRDatail where SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable LoadRDatails(string SGID)
        {
            string sql = "select DID,Process,team,convert(varchar(10),Estimatetime,120) Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,convert(varchar(10),finishtime,120) finishtime,people,reason from BGOI_Produce.dbo.tk_ProductRDatail where SGID='" + SGID + "'and Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;

        }

        public static DataTable GetSGnum(string SGID)
        {
            string sql = "select  OrderNum" +
  " from BGOI_Produce.dbo.tk_ProductRProduct where SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;

        }

        public static DataTable getSGstates(string RWID, string SGID)
        {
            int a;
            string sql = "select b.DID, b.PID,b.OrderContent,b.SpecsModels,b.OrderUnit,(b.OrderNum-isnull(c.Amount,0)) OrderNum,b.photo,b.Remark,(b.OrderNum-isnull(c.Amount,0)) Amount from BGOI_Produce.dbo.tk_ProductTDatail b left join  (select DID,PID,SUM(OrderNum)Amount from BGOI_Produce.dbo.tk_ProductRProduct a,BGOI_Produce.dbo.tk_ProductRecord b where a.SGID=b.SGID and b.Validate='v'  group by DID,PID) c on b.DID = c.DID and b.PID=c.PID where b.RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var DID = dr["DID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='3'where DID='" + DID + "';update BGOI_Produce.dbo.tk_ProductRecord set  State='1'where SGID='" + SGID + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='0'where DID='" + DID + "';update BGOI_Produce.dbo.tk_ProductRecord set  state='0'where SGID='" + SGID + "';";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static DataTable getTaskstates(string RWID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where State<>'3' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='5',Productioncompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='3' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='4',Productioncompletiontime='" + DateTime.Now + "' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='2' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static bool SaveUpdateRDatail(tk_ProductRecord record, List<tk_ProductRProduct> delist, ref string strErr, ref string strErrs, string SGID, string RWID)
        {
            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_ProductRecord set RWID=@RWID," + "ID=@ID,SpecsModels=@SpecsModels,billing=@billing,CreateUser=@CreateUser  where SGID=@SGID";
                SqlParameter[] param ={
                                       new SqlParameter ("@RWID",SqlDbType .NVarChar),
                                       new SqlParameter ("@ID",SqlDbType.NVarChar ),
                                       new SqlParameter ("@SpecsModels",SqlDbType .NVarChar),
                                       new SqlParameter ("@billing",SqlDbType .DateTime),
                                       //new SqlParameter ("@OrderContent",SqlDbType .NVarChar),
                                       //new SqlParameter ("@Remark",SqlDbType .NVarChar),
                                       new SqlParameter ("@CreateUser",SqlDbType .NVarChar),
                                       new SqlParameter ("@SGID",SqlDbType .NVarChar)
                                     };
                param[0].Value = record.RWID;
                param[1].Value = record.ID;
                param[2].Value = record.SpecsModels;
                param[3].Value = Convert.ToDateTime(record.billing);
                //param[4].Value = record.OrderContent;
                //param[5].Value = record.Remark;
                param[4].Value = record.CreateUser;
                param[5].Value = record.SGID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_ProductRecord_HIS (SGID,RWID,ID,billing,OrderContent,SpecsModels,Remark,State,CreateUser,CreateTime,Validate,FinishTime,CancelTime,CancelReason,NCreateTime,NCreateUser)" +
                "select SGID,RWID,ID,billing,OrderContent,SpecsModels,Remark,State,CreateUser,CreateTime,Validate,FinishTime,CancelTime,CancelReason'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_ProductRecord where SGID ='" + record.SGID + "'";
                string strUpdateList = "";
                string strInsertDetailHIS = "";
                //if (delist1.Count > 0)
                //{
                //    foreach (tk_ProductRDatail item in delist1)
                //    {
                //        strUpdateList = "update BGOI_Produce.dbo.tk_ProductRDatail set Process='" + item.Process + "', team='" + item.team + "'," +
                //            "Estimatetime='" + item.Estimatetime + "',person='" + item.person + "',plannumber='" + item.plannumber + "'," +
                //            "Qualified='" + item.Qualified + "',number='" + item.number + "',numbers='" + item.numbers + "',Fnubers='" + item.Fnubers + "',finishtime='" + item.finishtime + "',people='" + item.people + "',reason='" + item.reason + "' where DID='" + item.DID + "'";
                //        strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_ProductRDatail_HIS(SGID,DID,Process,team,Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,finishtime,people,reason,CreateTime,CreateUser,Validate,NCreateUser,NCreateTime)" +
                //              "select SGID,DID,Process,team,Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,finishtime,people,reason,CreateTime,CreateUser,Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                //              " from BGOI_Produce.dbo.tk_ProductRDatail where DID='" + item.DID + "'";
                //        if (strUpdateList != "")
                //        {
                //            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                //            SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");
                //        }
              
                //    }
                //}
                string strUpdateLists = "";
                string strInsertDetailHISs = "";
                if (delist.Count > 0)
                {
                    foreach (tk_ProductRProduct item in delist)
                    {
                        strUpdateLists = "update BGOI_Produce.dbo.tk_ProductRProduct set OrderNum='" + item.OrderNum + "'where SGDID='" + item.SGDID + "'";
                        strInsertDetailHISs = "insert into BGOI_Produce.dbo.tk_ProductRProduct_HIS(SGID,SGDID,DID,PID,OrderNum,NCreateUser,NCreateTime)" +
                              "select SGID,SGDID,DID,PID,OrderNum,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                              " from BGOI_Produce.dbo.tk_ProductRProduct where SGDID='" + item.SGDID + "'";
                        if (strUpdateLists != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertDetailHISs, "MainProduce");
                            SQLBase.ExecuteNonQuery(strUpdateLists, "MainProduce");
                        }
                        //getSGstates(RWID, SGID);
                        //getTaskstates(RWID);

                        getSGtask(RWID);
                        getsgtaskdetail(RWID);
                    }
                }
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        #region 随工单详情
        public static tk_ProductRecord IndexAllSGDtail(string SGID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ProductRecord where SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ProductRecord Material = new tk_ProductRecord();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToSGDtail(item, Material);
            }
            return Material;
        }

        public static void DataRowToSGDtail(DataRow item, tk_ProductRecord task)
        {
            task.RWID = item["RWID"].ToString();
            task.ID = item["ID"].ToString();
            task.SpecsModels = item["SpecsModels"].ToString();
            task.billing = Convert.ToDateTime(item["billing"]);
            task.OrderContent = item["OrderContent"].ToString();
            task.Remark = item["Remark"].ToString();
            task.CreateUser = item["CreateUser"].ToString();
        }

        public static DataTable LoadSGDetail(string SGID)
        {
            string sql = "select a.PID,a.OrderContent,a.SpecsModels,a.OrderUnit,b.OrderNum,a.photo,a.Remark" +
          " from BGOI_Produce.dbo.tk_ProductTDatail a,BGOI_Produce.dbo.tk_ProductRProduct b " +
          " where a.DID=b.DID and b.SGID='" + SGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable LoadSGDetails(string SGID)
        {
            string sql = "select DID,Process,team,convert(varchar(10),Estimatetime,120) Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,convert(varchar(10),finishtime,120) finishtime,people,reason from BGOI_Produce.dbo.tk_ProductRDatail where SGID='" + SGID + "'and Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        #endregion

        #region 相关单据
        public static DataTable LoadSGs(string SGID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",SGID)
                };
            DataTable dt = SQLBase.FillTable("getsg", CommandType.StoredProcedure, sqlPar, "MainProduce");
            if (dt == null) return null;
            return dt;
        }

        public static UIDataTable LoadSGXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_ProductRecord " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "billing desc ";
            String strTable = " BGOI_Produce.dbo.tk_ProductRecord ";

            String strField = "SGID,billing,OrderContent,SpecsModels,State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "在线生产";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "生产完成";
                    }
                }
            }
            return instData;
        }

        public static UIDataTable LoadSGXGs(int a_intPageSize, int a_intPageIndex, string SGID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_ProductRProduct a,BGOI_Produce.dbo.tk_ProductTDatail b where a.DID=b.DID and a.SGID='" + SGID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "SGID='" + SGID + "' and a.DID=b.DID";
            string strOrderBy = " SGID ";
            String strTable = "BGOI_Produce.dbo.tk_ProductRProduct a,BGOI_Produce.dbo.tk_ProductTDatail b ";
            String strField = "a.OrderNum,a.DID,b.OrderContent,b.SpecsModels,b.OrderUnit,b.Remark,b.photo ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }

        public static UIDataTable LoadSGXGDetail(int a_intPageSize, int a_intPageIndex, string SGID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_ProductRDatail where SGID='" + SGID + "' and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "SGID='" + SGID + "' and Validate='v'";
            string strOrderBy = " SGID ";
            String strTable = "BGOI_Produce.dbo.tk_ProductRDatail";
            String strField = "Process,team,Estimatetime,person,plannumber,Qualified,number,numbers,Fnubers,finishtime,people";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        #endregion

        #region 打印
        public static DataTable PrintSGs(string strWhere, string tableName, ref string strErr)
        {
            String strField = "select a.SGID,a.ID,a.SpecsModels,a.billing,a.OrderContent,a.Remark,a.CreateUser,b.OrderContent m,b.SpecsModels n,b.PID x,b.photo y from " + tableName + " " + strWhere + "";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }

        public static DataTable PrintSGss()
        {
            String strField = "select Text from BGOI_Produce.dbo.tk_ConfigContent where SID='随工单工序类型'";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }
        #endregion
        #endregion

        #region 检验报告
        #region 报告列表显示
        public static UIDataTable ReportInfo(int a_intPageSize, int a_intPageIndex, string where)
        {
            //           
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getReportInfo", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

        public static DataTable FileInfo(string BGID)
        {
            string sql = "select DID,BGID,Type,FileName from BGOI_Produce.dbo.tk_FileInfo where Validate='v' and BGID='" + BGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }


        #endregion

        #region 撤销报告单
        public static bool CXBG(string BGID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_ReportInfo set Validate='i',CancelTime='" + DateTime.Now + "' where BGID='" + BGID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 相关单据
        public static DataTable LoadBGs(string BGID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",BGID)
                };
            DataTable dt = SQLBase.FillTable("getbg", CommandType.StoredProcedure, sqlPar, "MainProduce");
            if (dt == null) return null;
            return dt;
        }

        public static UIDataTable LoadBGXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_ReportInfo " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "uploadtime desc ";
            String strTable = " BGOI_Produce.dbo.tk_ReportInfo ";

            String strField = "BGID,uploadtime,CreatePerson,Remarks";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }

        public static UIDataTable LoadBGXGDetail(int a_intPageSize, int a_intPageIndex, string BGID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_FileInfo where BGID='" + BGID + "'and Validate='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "BGID='" + BGID + "'and Validate='v'";
            string strOrderBy = " BGID ";
            String strTable = "BGOI_Produce.dbo.tk_FileInfo ";
            String strField = "DID,Type,FileName,CreatePerson";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        #endregion

        #region 修改报告单
        public static tk_ReportInfo IndexAllReportInfo(string BGID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_ReportInfo where BGID='" + BGID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_ReportInfo Material = new tk_ReportInfo();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToReportInfo(item, Material);
            }
            return Material;
        }

        public static void DataRowToReportInfo(DataRow item, tk_ReportInfo task)
        {
            task.BGID = item["BGID"].ToString();
            task.RWID = item["RWID"].ToString();
            task.uploadtime = Convert.ToDateTime(item["uploadtime"].ToString());
            task.Remarks = item["Remarks"].ToString();
            task.CreatePerson = item["CreatePerson"].ToString();
        }

        public static DataTable getFileInfo(string BGID)
        {
            string sql = "select DID,Type,FileName from BGOI_Produce.dbo.tk_FileInfo where Validate='v' and  BGID='" + BGID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static bool SCBG(string DID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_FileInfo set Validate='i',CancelTime='" + DateTime.Now + "' where DID='" + DID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "删除失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }


        public static bool SaveUpdateFileInfoIn(tk_ReportInfo record, List<tk_FileInfo> delist, ref string strErr, string BGID, string CreatePerson, DateTime CreateTime)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "select COUNT(*) from BGOI_Produce.dbo.tk_ReportInfo where BGID='" + BGID + "'";
                if (strInsert != "")
                {
                    count = Convert.ToInt32(SQLBase.ExecuteScalar(strInsert));
                }

                trans.Close(true);

                if (count > 0)
                {
                    string strInsertList = "";
                    if (delist.Count > 0)
                    {
                        foreach (tk_FileInfo SID in delist)
                        {
                            SqlParameter[] para = new SqlParameter[]{
                                new SqlParameter("@FileInfo",SID.FileInfo)
                            };
                            strInsertList = "insert into [BGOI_Produce].[dbo].[tk_FileInfo](BGID,DID,Type,FileName,FileInfo,CreatePerson,CreateTime,Validate)Values('" + SID.BGID + "','" + ProFileInfoNums(SID.BGID) + "','" + SID.Type + "','" + SID.FileName + "',@FileInfo,'" + SID.CreatePerson + "','" + SID.CreateTime + "','" + SID.Validate + "')";

                            if (strInsertList != "")
                            {
                                SQLBase.ExecuteNonQuery(strInsertList, para);
                            }
                        }
                    }
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 文件下载
        public static DataTable GetDownload(string id)
        {
            string strSql = "select BGID,DID ID,FileName,FileInfo from BGOI_Produce.dbo.tk_FileInfo where DID = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProduce");
            return dt;
        }

        public static DataTable GetDownloadFile(string id)
        {
            string strSql = "select BGID,DID,FileName,FileInfo from BGOI_Produce.dbo.tk_FileInfo where DID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProduce");
            return dt;
        }
        #endregion
        #endregion

        #region 产品入库
        #region 列表信息
        public static DataTable getsupplier()
        {
            string str = "select distinct COMNameC from  BGOI_BasMan.dbo.tk_SupplierBas";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            return dt;
        }

        public static UIDataTable PStocking(int a_intPageSize, int a_intPageIndex, string where)
        {
            //           
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPStocking", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

        public static UIDataTable PStockingDetail(int a_intPageSize, int a_intPageIndex, string RKID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select  COUNT(*)" +
"from  BGOI_Produce.dbo.tk_PStockingDetail where RKID='" + RKID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RKID='" + RKID + "'";
            string strOrderBy = "RKID ";
            String strTable = "BGOI_Produce.dbo.tk_PStockingDetail";
            String strField = "PID,OrderContent,Specifications,Supplier,Unit,Amount,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }
        #endregion

        #region 修改入库单信息
        public static tk_PStocking IndexAllupdateRK(string RKID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_PStocking where RKID='" + RKID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_PStocking Material = new tk_PStocking();
            foreach (DataRow item in dt.Rows)
            {
                DataRowToupdateRK(item, Material);
            }
            return Material;
        }

        public static void DataRowToupdateRK(DataRow item, tk_PStocking task)
        {
            task.RWID = item["RWID"].ToString();
            task.StockInTime = Convert.ToDateTime(item["StockInTime"]);
            task.FinishTime = Convert.ToDateTime(item["FinishTime"]);
            task.HouseID = item["HouseID"].ToString();
            task.Batch = item["Batch"].ToString();
            task.StockRemark = item["StockRemark"].ToString();
            task.StockInUser = item["StockInUser"].ToString();
        }

        public static DataTable LoadRposDatail(string RKID, string RWID)
        {
            string sql = "select c.DID,c.PID,OrderContent,Specifications,Unit,Amount,amount a ,Remark,OrderNum-num ordernum,a.DID m from BGOI_Produce.dbo.tk_ProductTDatail a left join (select SUM(Amount) as num,RWIDDID,DID,PID,Specifications,Unit,Amount,m.RKID from BGOI_Produce.dbo.tk_PStockingDetail m,BGOI_Produce.dbo.tk_PStocking n where m.RKID=n.RKID and n.Validate='v' group by Amount,RWIDDID,DID,PID,Specifications,Unit,Amount,m.RKID) c on a.DID=c.rwiddid where  RWID ='" + RWID + "' and c.RKID='" + RKID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }

        public static DataTable getRKstates(string RWID)
        {
            int a;
            string sql = "select (a.OrderNum-isnull(c.num,0)) OrderNum,DID from BGOI_Produce.dbo.tk_ProductTDatail a left join (select SUM(Amount) as num,RWIDDID from BGOI_Produce.dbo.tk_PStockingDetail m,BGOI_Produce.dbo.tk_PStocking n where m.RKID=n.RKID and n.Validate='v' group by Amount,RWIDDID) c on a.DID=c.rwiddid where  RWID ='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["OrderNum"]);
                var m = dr["DID"].ToString();
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='4'where DID='" + m + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTDatail set  state='3'where DID='" + m + "'";
                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
            }
            return dt;
        }

        public static DataTable getrkTaskstates(string RWID, string RKID)
        {
            int a;
            string sql = "select COUNT(*) m from BGOI_Produce.dbo.tk_ProductTDatail  where State<>'5' and RWID='" + RWID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["m"]);
                if (a == 0)
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='6' where RWID='" + RWID + "'; update BGOI_Sales.dbo.OrdersInfo set Pstate='4' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "') ";
                        //update BGOI_Produce.dbo.tk_PStocking set state='1' where RKID='" + RKID + "'

                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    int count = 0;
                    SQLTrans trans = new SQLTrans();
                    trans.Open("MainProduce");
                    try
                    {
                        string strInsert = "update BGOI_Produce.dbo.tk_ProductTask set  state='5' where RWID='" + RWID + "' ; update BGOI_Sales.dbo.OrdersInfo set Pstate='3' where OrderID in(select OrderID from  BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "')";
                        //update BGOI_Produce.dbo.tk_PStocking set state='1' where RKID='" + RKID + "'

                        if (strInsert != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsert);
                        }
                        trans.Close(true);
                    }
                    finally
                    {

                    }
                }

            }
            return dt;
        }

        public static bool SaveUpdateposDetail(tk_PStocking record, List<tk_PStockingDetail> delist, ref string strErr, string RKID, string RWID, string RWIDDID)
        {
            try
            {
                string strUpdate = "update BGOI_Produce.dbo.tk_PStocking set RWID=@RWID," + "StockInTime=@StockInTime,FinishTime=@FinishTime,HouseID=@HouseID,Batch=@Batch,StockRemark=@StockRemark where RKID=@RKID";
                SqlParameter[] param ={
                                       new SqlParameter ("@RWID",SqlDbType .NVarChar),
                                       new SqlParameter ("@StockInTime",SqlDbType.DateTime ),
                                       new SqlParameter ("@FinishTime",SqlDbType .DateTime),
                                       new SqlParameter ("@HouseID",SqlDbType .NVarChar),
                                       new SqlParameter ("@Batch",SqlDbType .NVarChar),
                                       new SqlParameter ("@StockRemark",SqlDbType .NVarChar),
                                       new SqlParameter ("@RKID",SqlDbType .NVarChar)
                                     };
                param[0].Value = record.RWID;
                param[1].Value = Convert.ToDateTime(record.StockInTime);
                param[2].Value = Convert.ToDateTime(record.FinishTime);
                param[3].Value = record.HouseID;
                param[4].Value = record.Batch;
                param[5].Value = record.StockRemark;
                param[6].Value = record.RKID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_PStocking_HIS (RKID,RWID,StockInTime,HouseID,StockRemark,StockInUser,Type,UnitID,State,CreateTime,CreateUser,Batch,FinishTime,NCreateTime,NCreateUser)" +
                "select RKID,RWID,StockInTime,HouseID,StockRemark,StockInUser,Type,UnitID,State,CreateTime,CreateUser,Batch,FinishTime'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_PStocking where RKID ='" + record.RKID + "'";
                string strUpdateList = "";
                string strInsertDetailHIS = "";
                if (delist.Count > 0)
                {
                    foreach (tk_PStockingDetail item in delist)
                    {
                        strUpdateList = "update BGOI_Produce.dbo.tk_PStockingDetail set" +

                            " Amount='" + item.Amount + "'" +

                       " where DID='" + item.DID + "'";
                        strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_PStockingDetail_HIS(RKID,DID,PID,OrderContent,Specifications,Unit,Amount,Remark,NCreateUser,NCreateTime)" +
                              "select RKID,DID,PID,OrderContent,Specifications,Unit,Amount,Remark,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                              " from BGOI_Produce.dbo.tk_PStockingDetail where DID='" + item.DID + "'";
                        if (strUpdateList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                            SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");
                        }
                        getRKstates(RWID);
                        getrkTaskstates(RWID, RKID);
                    }
                }
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        #region 入库单详情
        public static tk_PStocking IndexAllRKDetail(string RKID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_PStocking where RKID='" + RKID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_PStocking Material = new tk_PStocking();
            foreach (DataRow item in dt.Rows)
            {
                DataRowToRKDetail(item, Material);
            }
            return Material;
        }

        public static void DataRowToRKDetail(DataRow item, tk_PStocking task)
        {
            task.RWID = item["RWID"].ToString();
            task.StockInTime = Convert.ToDateTime(item["StockInTime"]);
            task.FinishTime = Convert.ToDateTime(item["FinishTime"]);
            task.HouseID = item["HouseID"].ToString();
            task.Batch = item["Batch"].ToString();
            task.StockRemark = item["StockRemark"].ToString();
            task.StockInUser = item["StockInUser"].ToString();
        }

        public static DataTable LoadRKDatail(string RKID)
        {
            string sql = "select PID,OrderContent,Specifications,Supplier,Unit,Amount,Remark  from BGOI_Produce.dbo.tk_PStockingDetail" +
 " where RKID='" + RKID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        #endregion

        #region 撤销入库单
        public static bool CXRK(string RKID, ref string strErr, string RWID)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_PStocking set Validate='i' where RKID='" + RKID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    getRKstates(RWID);
                    getrkTaskstates(RWID, RKID);
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 相关单据
        public static DataTable LoadRKs(string RKID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",RKID)
                };
            DataTable dt = SQLBase.FillTable("getrk", CommandType.StoredProcedure, sqlPar, "MainProduce");
            if (dt == null) return null;
            return dt;
        }

        public static UIDataTable LoadRKXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from BGOI_Produce.dbo.tk_PStocking " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }

            string strFilter = strWhere;
            string strOrderBy = "StockInTime desc ";
            String strTable = " BGOI_Produce.dbo.tk_PStocking ";

            String strField = "RKID,StockInTime,HouseID,StockRemark,StockInUser,Type,UnitID,State";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }
                }
            }
            return instData;
        }

        public static UIDataTable LoadRKXGDetail(int a_intPageSize, int a_intPageIndex, string RKID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_Produce.dbo.tk_PStockingDetail where RKID='" + RKID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainProduce"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "RKID='" + RKID + "'";
            string strOrderBy = " RKID ";
            String strTable = "BGOI_Produce.dbo.tk_PStockingDetail";
            String strField = "PID,OrderContent,Specifications,Supplier,Unit,Amount,Remark";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainProduce");

            return instData;
        }
        #endregion

        #region 打印
        public static DataTable PrintRKs(string strWhere, string tableName, ref string strErr)
        {
            String strField = "select  * from " + tableName + " " + strWhere + "";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }

        public static DataTable PrintRKdetail(string RKID)
        {
            String strField = "select  * from BGOI_Produce.dbo.tk_PStockingDetail where RKID like '%" + RKID + "%'";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }
        #endregion
        #endregion

        #region 计划单管理
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
            String strField = "JHID,DID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks";
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
            string str = "select a.PID, b.ProName,b.Spec,d.FinishCount,d.OnlineCount,(c.Number* d.FinishCount) Spareparts" +
" from BGOI_Inventory.dbo.tk_ProductionOfFinishedGoods a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ProFinishDefine " + "  c,BGOI_Inventory.dbo.tk_StockRemain d" +
" where a.PID=b.PID and c.PartPID=b.PID and d.ProductID= b.PID";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static bool SaveZD(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_Product_Plan>(record, "[BGOI_Produce].[dbo].tk_Product_Plan");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (tk_Product_PlanDetail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Produce].[dbo].[tk_Product_PlanDetail](JHID,DID,PID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,Validate,CreateUser,CreateTime) " +
                            "values ('" + SID.JHID + "','" + ProJHNum(SID.JHID) + "','" + SID.PID + "','" + SID.Name + "','" + SID.Specifications + "','" + SID.Finishedproduct + "','" + SID.finishingproduct + "','" + SID.Spareparts + "','" + SID.notavailable + "','" + SID.Total + "','" + SID.plannumber + "','" + SID.demandnumber + "','" + SID.Remarks + "','" + SID.Validate + "','" + SID.CreateUser + "','" + SID.CreateTime + "')";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
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
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region 修改计划单
        public static DataTable LoadPlanDatail(string JHID)
        {
            string str = "select " + "Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,DID " +
"from  [BGOI_Produce].dbo.[tk_Product_PlanDetail] where JHID='" + JHID + "' ";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static tk_Product_Plan IndexAllupdatePlan(string JHID)
        {
            string str = "select * from BGOI_Produce.dbo.tk_Product_Plan where JHID='" + JHID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            if (dt == null) return null;
            tk_Product_Plan Task = new tk_Product_Plan();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToPlan(item, Task);
            }
            return Task;
        }

        public static void DataRowToPlan(DataRow item, tk_Product_Plan task)
        {
            task.Specifieddate = Convert.ToDateTime(item["Specifieddate"]);
            task.Plannedmonth = item["Plannedmonth"].ToString();
            task.Remarks = item["Remarks"].ToString();
            task.Formulation = item["Formulation"].ToString();
        }

        public static bool SaveUpdatePlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr, string JHID)
        {
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
                param[2].Value = record.Remarks;
                param[3].Value = record.Formulation;
                param[4].Value = record.JHID;


                string InserNewOrdersHIS = "insert into BGOI_Produce.dbo.tk_Product_Plan_HIS (JHID,UnitID,Plannedyear,Plannedmonth,Specifieddate,Formulation,Remarks,State,Approvalstatus,CreateUser,CreateTime,Validate,NCreateTime,NCreateUser)" +
                "select JHID,UnitID,Plannedyear,Plannedmonth,Specifieddate,Formulation,Remarks,State,Approvalstatus,CreateUser,CreateTime,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Produce.dbo.tk_Product_Plan where JHID ='" + record.JHID + "'";
                string strUpdateList = "";
                string strInsertDetailHIS = "";
                if (delist.Count > 0)
                {
                    foreach (tk_Product_PlanDetail item in delist)
                    {
                        strUpdateList = "update BGOI_Produce.dbo.tk_Product_PlanDetail set Name='" + item.Name + "', Specifications='" + item.Specifications + "'," +
                            "Finishedproduct='" + item.Finishedproduct + "',finishingproduct='" + item.finishingproduct + "',Spareparts='" + item.Spareparts + "'," +
                            "notavailable='" + item.notavailable + "',Total='" + item.Total + "',plannumber='" + item.plannumber + "',demandnumber='" + item.demandnumber + "',Remarks='" + item.Remarks + "' where DID='" + item.DID + "'";
                        strInsertDetailHIS = "insert into BGOI_Produce.dbo.tk_Product_PlanDetail_HIS(JHID,DID,PID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,CreateUser,CreateTime,Validate,NCreateUser,NCreateTime)" +
                              "select JHID,DID,PID,Name,Specifications,Finishedproduct,finishingproduct,Spareparts,notavailable,Total,plannumber,demandnumber,Remarks,CreateUser,CreateTime,Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                              " from BGOI_Produce.dbo.tk_Product_PlanDetail where DID='" + item.DID + "'";
                        if (strUpdateList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainProduce");
                            SQLBase.ExecuteNonQuery(strUpdateList, "MainProduce");

                        }
                    }
                }
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainProduce");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainProduce");
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

        #region 撤销计划
        public static bool CXJH(string JHID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = "update BGOI_Produce.dbo.tk_Product_Plan set Validate='i' where JHID='" + JHID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #region 打印
        public static DataTable PrintJHs(string strWhere, string tableName, ref string strErr)
        {
            String strField = "select  * from " + tableName + " " + strWhere + "";
            DataTable dt = SQLBase.FillTable(strField, "MainProduce");
            return dt;
        }
        #endregion

        #region 判断是否提交
        public static DataTable selectPDTJ(string RWID)
        {
            string sql = "select count(*) IsPass from BGOI_Produce.dbo.tk_Approval where  RelevanceID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成领料单时的是否审批判断
        public static DataTable getSP(string RWID)
        {
            string str = "select count(*) IsPass from BGOI_Produce.dbo.tk_Approval where  RelevanceID='" + RWID + "' and State=0";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成领料单时的是否领料完成判断
        public static DataTable getLLSL(string RWID)
        {
            string str = "select LLstate from BGOI_Produce.dbo.tk_ProductTask where  RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成领料单时是否生成随工单，入库判断
        public static DataTable gettll(string RWID)
        {
            string str = "select state from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成随工单时的是否生成随工单完成判断
        public static DataTable getSGSL(string RWID)
        {
            string str = "select State from BGOI_Produce.dbo.tk_ProductTask where  RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成随工单时领料单是否有此条数据判断
        public static DataTable selectLL(string RWID)
        {
            string str = "select LLstate from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region  完成入库单时，判断随工单是否生产完成
        public static DataTable selectSG(string RWID)
        {
            string str = "select COUNT(*) a from BGOI_Produce.dbo.tk_ProductRecord a,BGOI_Produce.dbo.tk_ProductRProduct b where RWID='" + RWID + "' and a.SGID=b.SGID";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region  产品入库时，判断此产品是否已经完成入库
        public static DataTable selectRK(string RWID)
        {
            string str = "select State from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region  生成随工单时，判断是否需添加随工记录
        public static DataTable selectSGSL(string RWID)
        {
            string str = "select state from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #region 生成随工单时判断是否入库
        public static DataTable gettsg(string RWID)
        {
            string str = "select state from BGOI_Produce.dbo.tk_ProductTask where RWID='" + RWID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }
        #endregion

        #endregion

        #region 上传文件类型设置
        public static DataTable GetBGLX()
        {
            string str = "select distinct SID from BGOI_Produce.dbo.tk_ConfigContent ";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
        }

        public static UIDataTable SZBGLX(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBGLX", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

        public static DataTable THBGLXs(string SID)
        {
            string str = " select distinct Text from BGOI_Produce.dbo.tk_ConfigContent  where SID='" + SID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static DataTable getText(string SID, string text)
        {
            string str = "select distinct Text from BGOI_Produce.dbo.tk_ConfigContent  where SID='" + SID + "' and Text='" + text + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static bool SCBGLX(string Text, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainProduce");
            try
            {
                string strInsert = " delete from BGOI_Produce.dbo.tk_ConfigContent  where  Text='" + Text + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "删除失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }

        public static bool SaveBGLX(List<tk_ConfigContent> detailList, ref string strErr)
        {
            string strInsertList = "";
            if (detailList.Count > 0)
            {
                foreach (tk_ConfigContent SID in detailList)
                {
                    strInsertList = "insert into [BGOI_Produce].[dbo].[tk_ConfigContent](XID,SID,Text)Values('" + SID.XID + "','" + SID.SID + "','" + SID.Text + "')";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }
            return true;
        }
        #endregion

        #region 退换货检验
        public static UIDataTable getCHECK(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select a.EID,a.ContractID,a.OrderID,a.ChangeDate,a.ReturnWay,a.State ,a.ISF,a.ISEXR From     BGOI_Sales.dbo.ExchangeGoods a where a.validate='v' and State!='3'" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.validate='v' and State!='3'" + where;
            string strOrderBy = "a.CreateTime desc ";
            String strTable = "BGOI_Sales.dbo.ExchangeGoods a ";
            String strField = " a.EID,a.ContractID,a.OrderID,a.ChangeDate,a.ReturnWay,a.State ,a.ISF,a.ISEXR";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["ReturnWay"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["ReturnWay"] = GetSelectPro(instData.DtData.Rows[r]["ReturnWay"].ToString()).Text;
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["State"] = GetStatePro(instData.DtData.Rows[r]["State"].ToString(), "ExChangeState").StateDesc;
                    }
                }

            }
            return instData;
        }


        public static ProjectSelect_Config GetSelectPro(string ID)
        {
            string str = "select * from ProjectSelect_Config where ID='" + ID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            ProjectSelect_Config proselect = new ProjectSelect_Config();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProSele(proselect, item);
            }
            return proselect;
        }
        private static void DatarowToProSele(ProjectSelect_Config pros, DataRow item)
        {
            pros.ID = item["ID"].ToString();
            pros.Text = item["Text"].ToString();
            pros.Type = item["Type"].ToString();
        }

        public static ProjectState_Config GetStatePro(string ID, string Type)
        {
            string str = "select * from ProjectState_Config where StateId='" + ID + "' and StateType='" + Type + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            ProjectState_Config proselect = new ProjectState_Config();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToPState(proselect, item);
            }
            return proselect;
        }
        public static void DatarowToPState(ProjectState_Config Pstate, DataRow item)
        {
            Pstate.StateId = item["StateId"].ToString();
            Pstate.StateDesc = item["StateDesc"].ToString();
            Pstate.StateType = item["StateType"].ToString();
        }


        public static UIDataTable getExchangeCheck(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getExchangeCheck", CommandType.StoredProcedure, sqlPar, "MainProduce");
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

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["Brokerage"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["Brokerage"] = GetSelectPro(instData.DtData.Rows[r]["Brokerage"].ToString()).Text;
                    }
                   
                }

            }
            return instData;
        }

        public static UIDataTable getExchangeCheckDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select TID,TDID,SpecsModels,ProductID,PackWreck,FeatureWreck,Componments,Quality,CreateTime from BGOI_Sales.dbo.ExchangeGoods_DetailInfo where  Validate='v' and TID='"+where+"'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " Validate='v' and TID='"+where+"'";
            string strOrderBy = "CreateTime desc ";
            String strTable = "BGOI_Sales.dbo.ExchangeGoods_DetailInfo  ";
            String strField = " TID,TDID,SpecsModels,ProductID,PackWreck,FeatureWreck,Componments,Quality,CreateTime";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }

        public static DataTable getSPPD(string TID)
        {
            string str = "select b.StateDesc name from BGOI_Sales.dbo.Exchange_Check a,BGOI_Sales.dbo.ProjectState_Config b where a.ProductionState=StateId and StateType='ProductionState' and TID='"+TID+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        public static DataTable getPDSPCK(string TID)
        {
            string str = "select * from BGOI_Sales.dbo.tk_Approval where RelevanceID='" + TID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProduce");
            return dt;
        }

        //提交审批
        public static int InsertApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');
            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/52/tk_PID/54/tk_UserLog
            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";

            string strUpdateBas = "";
            if (arr[2].IndexOf("..") > 0)
                strUpdateBas = "update " + arr[2] + " set ProductionState = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set ProductionState = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";

            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','提交" + webkey + "审批操作','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "')";
            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                if (strUpdateBas != "")
                    intUpdateBas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intUpdateBas;
        }

        //审批
        public static int UpdateApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');
            int state = 0;
            if (IsPass == "是")
                state = 1;
            else
                state = -1;
            string strInsertBas = "update [" + arr[0] + "].." + arr[1] + " set ApprovalMan = '" + UserId + "',ApprovalTime = '" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',"
                    + "Remark = '" + Remark + "',State = '" + state + "' where PID = '" + PID + "' and ApprovalPersons = '" + UserId + "'";
            string strUpdateBas = "";
            if (state == -1)
            {
                if (arr[2].IndexOf("..") > 0)
                    strUpdateBas = "update " + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            if (strUpdateBas != "")
                intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
            string str = "select ApprovalLevel,SUBSTRING(AppType,1,1) as Type,SUBSTRING(AppType,3,1) as num  from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '0'";
            DataTable dt = SQLBase.FillTable(str);
            string Type = "";
            int num = 0;
            int bol = 0;
            int Hcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Type = dt.Rows[i]["Type"].ToString();
                if (Type == "1")
                {
                    bol = 1;
                    break;
                }
                if (Type == "2")
                {
                    bol = 1;
                    break;
                }
                if (Type == "3")
                {
                    num = Convert.ToInt16(dt.Rows[i]["num"]);
                    string level = dt.Rows[i]["ApprovalLevel"].ToString();
                    string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '1' and AppType = '3-" + num + "'";
                    DataTable dt2 = SQLBase.FillTable(str1);
                    if (dt2.Rows.Count > 0)
                        Hcount = Convert.ToInt16(dt2.Rows[0][0]);
                    if (Hcount < num)
                    {
                        bol = 1;
                        break;
                    }
                }
            }
            string strAllBas = "";
            if (bol == 0 && state != -1)
            {
                if (arr[2].IndexOf("..") > 0)
                    strAllBas = "update " + arr[2] + " set ProductionState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set ProductionState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else
            {
                strAllBas = "";
            }
            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, CommandType.Text, null);
            return intInsertBas + intUpdateBas;
        }
        #endregion
    }

}
