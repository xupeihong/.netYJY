using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
using System.IO;
namespace TECOCITY_BGOI
{
    public class ContractPro
    {
        public static string GetShowCID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select CID, CidNo from tk_CIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_CIDno (CID,CidNo,DateRecord) values('C',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SupplyCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["CidNo"]);
            }

            intNewID++;
            string str = "select CID, CidNo,DateRecord from tk_CIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            strPID = dtPMaxID.Rows[0]["CID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetCID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select CID, CidNo from tk_CIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_CIDno (CID,CidNo,DateRecord) values('C',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SupplyCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["CidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_CIDno set CidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "SupplyCnn");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strPID = dtPMaxID.Rows[0]["CID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static DataTable getContractID()
        {
            string str = "select ContractID from tk_ContractBas";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            return dt;
        }

        public static DataTable getMoneyFromProjectBas(string ProID)
        {
            string str = "select ContractAmount,Budget,Cost,Profit from tk_ProjectBas where ProID = '"+ProID+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static DataTable getContractState()
        {
            //string strSql = "select StateId,name from tk_ConfigState where Type = 'Contract' order by StateId";
            string strSql = "select StateId,name from tk_ConfigState where Type = 'Renew' order by StateId";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static string getReturnTime()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'ReturnCash' and TimeType = 'Contract'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }

        public static string getCPlanTime()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CPlanTime' and TimeType = 'Project'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }

        public static string getCashBackTime()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CashBack' and TimeType = 'Project'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }

        public static UIDataTable getContractGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getContract", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static UIDataTable getCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCashBack", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static UIDataTable getUserlogGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getUserlog", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static UIDataTable getStandingBookGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getContractStandingBook", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static DataTable getPrintStandingBook(string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            DataTable DO_Order = SQLBase.FillTable("getPrintContractStandingBook", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            DO_Order.Columns.Add(c);
            for (int i = 0; i < DO_Order.Rows.Count; i++)
            {
                DO_Order.Rows[i]["xu"] = (i + 1);
            }
            return DO_Order;
        }

        public static UIDataTable getProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProject", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static bool judgeCIDinContractBas(string CID)
        {
            string str = "select CID from tk_ContractBas where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static int InsertProjectContract(ContractBas Bas,  ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intInsertFile = 0;
            int intUpdateIs = 0;
            int intInsertLog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "insert into tk_ContractBas (CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,CPlanEndTime,CEndAmount,CEndTime,Rmark,State,CreateTime,CreateUser,Validate,PContractAmount,PBudget,PCost,PProfit) values ("
                + "'" + Bas.StrCID + "','" + Bas.StrContractID + "','" + Bas.StrPID + "','" + Bas.StrUnit + "','" + Bas.StrBusinessType + "','" + Bas.StrCname + "','" + Bas.StrContractContent + "','" + Bas.StrCStartTime + "','" + Bas.StrCPlanEndTime + "',"
                + "'" + Bas.StrCEndAmount + "','" + Bas.StrCEndTime + "','" + Bas.StrRmark + "','" + Bas.StrState + "',"
                + "'" + Bas.StrCreateTime + "','" + Bas.StrCreateUser + "','" + Bas.StrValidate + "','" + Bas.StrPContractAmount + "','" + Bas.StrPBudget + "','" + Bas.StrPCost + "','" + Bas.StrPProfit + "')";
            string strInsertLog = "insert into tk_UserLog values ('" + Bas.StrCID + "','添加合同操作','添加成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            string strUpdateIs = "update [BGOI_Project ]..tk_ProjectBas set State = '5' where ProID = '" + Bas.StrPID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateIs != "")
                    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdateIs;
        }

        public static int UpdateProjectContract(ContractBas Bas,  ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intInsertFile = 0;
            int intInsertHis = 0;
            int intInsertLog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_ContractBas set ContractID = '" + Bas.StrContractID + "',Cname = '" + Bas.StrCname + "',ContractContent = '" + Bas.StrContractContent + "',PContractAmount = '" + Bas.StrPContractAmount + "',PBudget = '" + Bas.StrPBudget + "',"
                + "PCost = '" + Bas.StrPCost + "',PProfit = '" + Bas.StrPProfit + "',CStartTime = '" + Bas.StrCStartTime + "',CPlanEndTime = '" + Bas.StrCPlanEndTime + "',Rmark = '" + Bas.StrRmark + "' where CID = '"+Bas.StrCID+"'";
            string strInsertHis = "insert into tk_ContractBasHis (CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,"
               + "PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate,PContractAmount,PBudget,PCost,PProfit,UpdateTime,UpdateUser) select CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,"
               + "PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate,PContractAmount,PBudget,PCost,PProfit,'" + DateTime.Now + "','" + account.UserID + "' from tk_ContractBas  where CID = '" + Bas.StrCID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + Bas.StrCID + "','变更合同操作','变更成功','" + DateTime.Now + "','" + account.UserName + "','合同')";


            try
            {

                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertHis;
        }

        public static int InsertContractBas(ContractBas Bas, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intInsertLog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "insert into tk_ContractBas (CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate) values ("
                + "'" + Bas.StrCID + "','" + Bas.StrContractID + "','" + Bas.StrPID + "','" + Bas.StrUnit + "','" + Bas.StrBusinessType + "','" + Bas.StrCname + "','" + Bas.StrContractContent + "','" + Bas.StrCStartTime + "','" + Bas.StrTimeScale + "','" + Bas.StrCPlanEndTime + "','" + Bas.StrCBeginAmount + "','" + Bas.StrMargin + "',"
                + "'" + Bas.StrCEndAmount + "','" + Bas.StrCEndTime + "','" + Bas.StrCtime + "','" + Bas.StrAmountNum + "','" + Bas.StrCurAmountNum + "','" + Bas.StrPrincipal + "','" + Bas.StrPartyA + "','" + Bas.StrPartyB + "','" + Bas.StrPayOrIncome + "','" + Bas.StrPageCount + "','" + Bas.StrRmark + "','" + Bas.StrState + "',"
                + "'" + Bas.StrCreateTime + "','" + Bas.StrCreateUser + "','" + Bas.StrValidate + "')";
            string strInsertLog = "insert into tk_UserLog values ('" + Bas.StrCID + "','添加合同操作','添加成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static int InsertFile(string id, byte[] fileByte, string FileName, ref string a_strErr)
        {
            int intInsert = 0;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };

            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            string strInsertOrder = "insert into tk_CFile (CID,FileInfo,FileName,CreateTime,CreateUser,Validate) values ('" + id + "', @fileByte,'" + FileName + "','" + DateTime.Now + "','" + account.UserID.ToString() + "','v')";

            try
            {
                sqlTrans.Open("SupplyCnn");
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, para);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static DataTable GetDownloadProject(string id)
        {
            string strSql = "select ID,CID,[FileInfo],FileName from tk_CFile where CID = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownloadFileProject(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_CFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownload(string id)
        {
            string strSql = "select ID,CID,[FileInfo],FileName from tk_CFile where CID = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static DataTable GetDownloadFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_CFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static int DeleteProjectFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadFileProject(ID);
            if (dt.Rows[0][0].ToString() != "")
            {
                string fileName = dt.Rows[0]["FileName"].ToString();//客户端保存的文件名 
                string filePath = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\"
                    + dt.Rows[0]["FileInfo"] + "\\" + fileName;//路径
                if (File.Exists(filePath))
                {

                    FileInfo fi = new FileInfo(filePath);

                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)

                        fi.Attributes = FileAttributes.Normal;

                    File.Delete(filePath);
                }

            }
            string strInsertOrder = "delete from tk_CFile where ID = '" + ID + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static int DeleteFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsertOrder = "delete from tk_CFile where ID = '" + ID + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static ContractBas getChangeContractBas(string id)
        {
            ContractBas Bas = new ContractBas();
            string strSql = "select * from tk_ContractBas where CID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Bas.StrCID = id;
                Bas.StrContractID = dt.Rows[0]["ContractID"].ToString();
                Bas.StrPID = dt.Rows[0]["PID"].ToString();
                Bas.StrBusinessType = dt.Rows[0]["BusinessType"].ToString();
                Bas.StrCname = dt.Rows[0]["Cname"].ToString();
                Bas.StrContractContent = dt.Rows[0]["ContractContent"].ToString();
                Bas.StrCStartTime = dt.Rows[0]["CStartTime"].ToString();
                Bas.StrTimeScale = Convert.ToInt16(dt.Rows[0]["TimeScale"]);
                Bas.StrCPlanEndTime = dt.Rows[0]["CPlanEndTime"].ToString();
                Bas.StrCBeginAmount = Convert.ToDecimal(dt.Rows[0]["CBeginAmount"]);
                Bas.StrMargin = Convert.ToDecimal(dt.Rows[0]["Margin"]);
                Bas.StrCEndAmount = Convert.ToDecimal(dt.Rows[0]["CEndAmount"]);
                Bas.StrCtime = dt.Rows[0]["Ctime"].ToString();
                Bas.StrAmountNum = Convert.ToInt16(dt.Rows[0]["AmountNum"]);
                Bas.StrPrincipal = dt.Rows[0]["Principal"].ToString();
                Bas.StrPayOrIncome = dt.Rows[0]["PayOrIncome"].ToString();
                Bas.StrPartyA = dt.Rows[0]["PartyA"].ToString();
                Bas.StrPartyB = dt.Rows[0]["PartyB"].ToString();
                Bas.StrPageCount = Convert.ToInt16(dt.Rows[0]["PageCount"]);
                Bas.StrRmark = dt.Rows[0]["Rmark"].ToString();
                Bas.StrState = Convert.ToInt16(dt.Rows[0]["State"]);
            }
            return Bas;
        }

        public static ContractBas getChangeProContractBas(string id)
        {
            ContractBas Bas = new ContractBas();
            string strSql = "select * from tk_ContractBas where CID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Bas.StrCID = id;
                Bas.StrContractID = dt.Rows[0]["ContractID"].ToString();
                Bas.StrPID = dt.Rows[0]["PID"].ToString();
                Bas.StrCname = dt.Rows[0]["Cname"].ToString();
                Bas.StrContractContent = dt.Rows[0]["ContractContent"].ToString();
                Bas.StrPContractAmount = Convert.ToDecimal(dt.Rows[0]["PContractAmount"]);
                Bas.StrPBudget = Convert.ToDecimal(dt.Rows[0]["PBudget"]);
                Bas.StrPCost = Convert.ToDecimal(dt.Rows[0]["PCost"]);
                Bas.StrPProfit = Convert.ToDecimal(dt.Rows[0]["PProfit"]);
                Bas.StrCStartTime = dt.Rows[0]["CStartTime"].ToString();
                Bas.StrCPlanEndTime = dt.Rows[0]["CPlanEndTime"].ToString();
                //Bas.StrAmountNum = Convert.ToInt16(dt.Rows[0]["AmountNum"]);
                Bas.StrRmark = dt.Rows[0]["Rmark"].ToString();
            }
            return Bas;
        }

        public static int UpdateContract(ContractBas Bas, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intInsertHis = 0;
            int intInsertLog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_ContractBas set ContractID = '" + Bas.StrContractID + "',PID = '" + Bas.StrPID + "',BusinessType = '" + Bas.StrBusinessType + "',"
                + "Cname = '" + Bas.StrCname + "',ContractContent = '" + Bas.StrContractContent + "',CStartTime = '" + Bas.StrCStartTime + "',TimeScale = '" + Bas.StrTimeScale + "',CPlanEndTime = '" + Bas.StrCPlanEndTime + "',"
                + "CBeginAmount = '" + Bas.StrCBeginAmount + "',Margin = '" + Bas.StrMargin + "',CEndAmount = '" + Bas.StrCEndAmount + "',Ctime = '" + Bas.StrCtime + "',"
                + "AmountNum = '" + Bas.StrAmountNum + "',Principal = '" + Bas.StrPrincipal + "',PartyA = '" + Bas.StrPartyA + "',PartyB = '" + Bas.StrPartyB + "',PayOrIncome = '" + Bas.StrPayOrIncome + "',PageCount = '" + Bas.StrPageCount + "',"
                + "Rmark = '" + Bas.StrRmark + "' where CID = '" + Bas.StrCID + "'";
            string strInsertHis = "insert into tk_ContractBasHis (CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,"
                + "PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate,PContractAmount,PBudget,PCost,PProfit,UpdateTime,UpdateUser) select CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,"
                + "PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate,PContractAmount,PBudget,PCost,PProfit,'" + DateTime.Now + "','" + account.UserID + "' from tk_ContractBas  where CID = '" + Bas.StrCID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + Bas.StrCID + "','变更合同操作','变更成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertHis;
        }
        public static string GetShowCBID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select CBID, CBIDno from tk_CBIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_CBIDno (CBID,CBIDno,DateRecord) values('CB',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SupplyCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["CBIDno"]);
            }

            intNewID++;
            string str = "select CBID, CBIDno,DateRecord from tk_CBIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            strPID = dtPMaxID.Rows[0]["CBID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetCBID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select CBID, CBIDno from tk_CBIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_CBIDno (CBID,CBIDno,DateRecord) values('CB',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SupplyCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["CBIDno"]);
            }

            intNewID++;
            string strUpdateID = "update tk_CBIDno set CBIDno='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "SupplyCnn");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strPID = dtPMaxID.Rows[0]["CBID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int getCurAmountNum(string CID)
        {
            int CurAmountNum = 0;
            string strSql = "select CurAmountNum from tk_ContractBas where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                CurAmountNum = Convert.ToInt16(dt.Rows[0]["CurAmountNum"]);
                CurAmountNum++;
            }
            return CurAmountNum;
        }

        public static int getCurProjectAmountNum(string CID)
        {
            int CurAmountNum = 0;
            string strSql = "select count(*) from tk_CCashBack where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                CurAmountNum = Convert.ToInt16(dt.Rows[0][0]);
                CurAmountNum++;
            }
            return CurAmountNum;
        }

        public static int InsertCCashBack(CCashBack Cash, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            int update = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<CCashBack>(Cash, "tk_CCashBack");
            string strUpdate = "update tk_ContractBas set CurAmountNum = '" + Cash.StrCurAmountNum + "',State = '3' where CID = '" + Cash.StrCID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','合同回款操作','回款成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    update = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + update;
        }

        public static bool checkMoney(string CID, string Money)
        {
            string str = "select PContractAmount from tk_ContractBas where CID = '"+CID+"'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            decimal Amount = Convert.ToDecimal(dt.Rows[0]["PContractAmount"]);
            string strS = "select sum(CBMoney) from tk_CCashBack where  CID = '" + CID + "'";
            DataTable dts = SQLBase.FillTable(strS, "SupplyCnn");
            decimal Rmoney = 0;
            if(dts.Rows[0][0].ToString() != "")
                 Rmoney = Convert.ToDecimal(dts.Rows[0][0]) + Convert.ToDecimal(Money);
            else
                Rmoney = Convert.ToDecimal(Money);
            if (Rmoney <= Amount)
                return true;
            else
                return false;
        }

        public static int InsertProCCashBack(CCashBack Cash, string PID,ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            int update = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<CCashBack>(Cash, "tk_CCashBack");
            string strUpdate = "update tk_ContractBas set CurAmountNum = '" + Cash.StrCurAmountNum + "',State = '3' where CID = '" + Cash.StrCID + "'";
            string strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '1' where ProID = '" + PID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','合同回款操作','回款成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    update = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                if (strUpdateBas != "")
                    updatebas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + update;
        }

        public static int UpdateCCashBack(CCashBack Cash, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_CCashBack set CurAmountNum = '" + Cash.StrCurAmountNum + "',CBMethod = '" + Cash.StrCBMethod + "',CBMoney = '" + Cash.StrCBMoney + "',CBBillNo = '" + Cash.StrCBBillNo + "',ReceiptNo = '" + Cash.StrReceiptNo + "',IsReturn = '" + Cash.StrIsReturn + "',"
                + "NoReturnReason = '" + Cash.StrNoReturnReason + "',PayCompany = '" + Cash.StrPayCompany + "',Remark = '" + Cash.StrRemark + "',CBDate = '" + Cash.StrCBDate + "' where CBID = '"+Cash.StrCBID+"'";
            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','修改合同回款操作','修改回款成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static int dellCCashBack(string cbid,string cid, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_CCashBack set Validate = 'i' where CBID = '"+cbid+"'";
            string strInsertLog = "insert into tk_UserLog values ('" + cid + "','撤销合同回款操作','撤销成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static CCashBack getUpdateCashBack(string id)
        {
            CCashBack Cash = new CCashBack();
            string str = "select * from tk_CCashBack where CBID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Cash.StrCBID = id;
                Cash.StrCID = dt.Rows[0]["CID"].ToString();
                Cash.StrCurAmountNum = Convert.ToInt16(dt.Rows[0]["CurAmountNum"]);
                Cash.StrCBMethod = dt.Rows[0]["CBMethod"].ToString();
                Cash.StrCBMoney = Convert.ToDecimal(dt.Rows[0]["CBMoney"]);
                Cash.StrCBBillNo = dt.Rows[0]["CBBillNo"].ToString();
                Cash.StrReceiptNo = dt.Rows[0]["ReceiptNo"].ToString();
                Cash.StrIsReturn = Convert.ToInt16(dt.Rows[0]["IsReturn"]);
                Cash.StrNoReturnReason = dt.Rows[0]["NoReturnReason"].ToString();
                Cash.StrPayCompany = dt.Rows[0]["PayCompany"].ToString();
                Cash.StrRemark = dt.Rows[0]["Remark"].ToString();
                Cash.StrCBDate = Convert.ToDateTime(dt.Rows[0]["CBDate"]).ToString("yyy-MM-dd");
            }
            return Cash;
        }

        public static decimal getDebtAmount(string CID)
        {
            decimal Debt = 0;
            string strSql = "select CBeginAmount,CEndAmount from tk_ContractBas where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            decimal TotalAmout = 0;
            decimal CBeginAmount = 0;
            decimal CEndAmount = 0;
            if (dt.Rows.Count > 0)
            {
                CBeginAmount = Convert.ToDecimal(dt.Rows[0]["CBeginAmount"]);
                CEndAmount = Convert.ToDecimal(dt.Rows[0]["CEndAmount"]);
            }
            if (CEndAmount != 0)
                TotalAmout = CEndAmount;
            else
                TotalAmout = CBeginAmount;
            decimal ReturnAmout = 0;
            string str = "select CBMoney from tk_CCashBack where CID = '" + CID + "'";
            DataTable dt2 = SQLBase.FillTable(str, "SupplyCnn");
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ReturnAmout += Convert.ToDecimal(dt2.Rows[i]["CBMoney"]);
                }
            }
            Debt = TotalAmout - ReturnAmout;
            return Debt;
        }

        public static decimal getDebtAmountPro(string CID)
        {
            decimal Debt = 0;
            string strSql = "select ContractAmount from tk_ProjectBas where PID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            decimal TotalAmout = 0;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ContractAmount"].ToString() != "")
                    TotalAmout = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
            }
            decimal ReturnAmout = 0;
            string str = "select CBMoney from tk_ProjectCashBack where CID = '" + CID + "'";
            DataTable dt2 = SQLBase.FillTable(str, "MainProject");
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["CBMoney"].ToString() != "")
                        ReturnAmout += Convert.ToDecimal(dt2.Rows[i]["CBMoney"]);
                }
            }
            Debt = TotalAmout - ReturnAmout;
            return Debt;
        }

        public static int InsertSettlement(CSettlement CST, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            int update = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<CSettlement>(CST, "tk_CSettlement");
            string strUpdate = "update tk_ContractBas set State = '4' where CID = '" + CST.StrCID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + CST.StrCID + "','合同结算操作','结算成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    update = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + update;
        }

        public static int InsertSettlementPro(CSettlement CST, string PID,ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            int update = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<CSettlement>(CST, "tk_CSettlement");
            string strUpdate = "update tk_ContractBas set State = '4' where CID = '" + CST.StrCID + "'";
            string strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '2' where ProID = '" + PID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + CST.StrCID + "','合同结算操作','结算成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    update = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                if (strUpdateBas != "")
                    updatebas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + update;
        }

        public static DataTable getDetailContract(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailContract", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            return dt;
        }

        public static string getCBID(string CID)
        {
            string sid = "";
            string str = "select CBID from tk_CCashBack where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                string strarr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["CBID"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    strarr += s + ",";
                }
                strarr = strarr.TrimEnd(',');
                string[] arr = strarr.Split(',');
                string max = arr.Max();
                int Cmax = Convert.ToInt16(max);
                Cmax++;
                string h = "";
                if (Cmax.ToString().Length == 1)
                {
                    h = "0" + Cmax;
                }
                else
                {
                    h = Cmax.ToString();
                }
                sid = CID + "-" + h;
            }
            else
            {
                sid = CID + "-01";
            }
            return sid;
        }
    }
}
