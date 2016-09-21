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
    public class ProjectPro
    {
        public static string GetShowPid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select PID, PidNo from tk_PIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_PIDno (PID,PidNo,DateRecord) values('P',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["PidNo"]);
            }

            intNewID++;
            string str = "select PID, PidNo,DateRecord from tk_PIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["PID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }
        public static string GetPid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select PID, PidNo from tk_PIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_PIDno (PID,PidNo,DateRecord) values('P',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["PidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_PIDno set PidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");
           
            strPID = dtPMaxID.Rows[0]["PID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static DataTable getProID()
        {
            string str = "select ProID from tk_ProjectBas";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static DataTable getAppID()
        {
            string str = "select AppID from tk_ProjectBas";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static int UserLog(ProjectUserLog Log, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = GSqlSentence.GetInsertInfoByD<ProjectUserLog>(Log, "tk_UserLog");

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);

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

        public static DataTable GetConfigContent(string type)
        {
            string str = "select SID,Text from tk_ConfigContent where Type = '" + type + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static DataTable GetPsource()
        {
            string str = "select SID,Text from tk_ConfigContent where Type = 'Psource' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static DataTable GetEarlyType(string type)
        {
            string str = "select SID,Text from tk_ConfigEarly where Type = '"+type+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static DataTable getPrincipal()
        {
            string str = "select UserId,UserName from UM_UserNew where DeptId = '36' and roleNames like '%项目负责人%'";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            return dt;
        }

        public static DataTable getConcertPerson()
        {
            string str = "select UserId,UserName from UM_UserNew where DeptId = '36' and roleNames like '%配合负责人%'";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            return dt;
        }

        public static DataTable GetFollowPerson()
        {
            string str = "select UserId,UserName from UM_UserNew where DeptId = '36'";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            return dt;
        }

        public static tk_Project getProjectNew(string PID)
        {
            tk_Project Pro = new tk_Project();
            string str = "select * from tk_Project where PID = '" + PID + "' order by CreateTime desc";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Pro.StrJQType = dt.Rows[0]["JQType"].ToString();
                //Pro.StrContent = dt.Rows[0]["Content"].ToString();
                Pro.StrCreatePerson = dt.Rows[0]["CreatePerson"].ToString();
            }
            return Pro;
        }

        public static DataTable changeJQType(string PID, string JQ)
        {
            string str = "select * from tk_Project where PID = '" + PID + "' and JQType = '"+JQ+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            return dt;
        }

        public static int InsertProject(tk_ProjectPre Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertBas = "insert into tk_ProjectBas (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,UnitID,Validate)"
            + " values ('" + Bas.StrPID + "','" + Bas.StrProID + "','" + Bas.StrPname + "','" + Bas.StrPsource + "','" + Bas.StrCustomerName + "',"
            + "'" + Bas.StrGoal + "','" + Bas.StrMainContent + "','0','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "',"
            + "'" + Bas.StrUnitID + "','" + Bas.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Acc_Account account = GAccount.GetAccountInfo();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "新建工程项目";
            Log.strLogContent = "新建工程项目成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "新建项目";
            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        //public static int InsertProjectBas(tk_ProjectBas Bas, ref string a_strErr)
        //{
        //    int intInsertBas = 0;
        //    int intlog = 0;
        //    SQLTrans sqlTrans = new SQLTrans();
        //    sqlTrans.Open("MainProject");

        //    string strInsertBas = "insert into tk_ProjectBas (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,UnitID,Validate)"
        //    +" values ('" + Bas.StrPID + "','" + Bas.StrProID + "','" + Bas.StrPname + "','" + Bas.StrPsource + "','" + Bas.StrCustomerName + "',"
        //    +"'" + Bas.StrGoal + "','" + Bas.StrMainContent + "','" + Bas.StrState + "','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "',"
        //    + "'" + Bas.StrUnitID + "','" + Bas.StrValidate + "')";

        //    string strErr = "";
        //    ProjectUserLog Log = new ProjectUserLog();
        //    Acc_Account account = GAccount.GetAccountInfo();
        //    Log.strRelevanceID = Bas.StrPID;
        //    Log.strLogTitle = "新建工程项目";
        //    Log.strLogContent = "新建工程项目成功";
        //    Log.strLogTime = DateTime.Now;
        //    Log.strLogPerson = account.UserName;
        //    Log.strType = "新建项目";
        //    try
        //    {
        //        if (strInsertBas != "")
        //            intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
        //        intlog = UserLog(Log, ref strErr);
        //        sqlTrans.Close(true);
        //    }
        //    catch (SqlException e)
        //    {
        //        sqlTrans.Close(false);
        //        a_strErr = e.Message;
        //        return -1;
        //    }

        //    return intInsertBas;
        //}

        public static tk_ProjectPre getUpdateProjectBas(string id)
        {
            tk_ProjectPre ProjectBas = new tk_ProjectPre();
            string strSql = "select ProID,Pname,Psource,CustomerName,Goal,MainContent from tk_ProjectBas where PID = '"+id+"'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            if (dt.Rows.Count > 0)
            {
                ProjectBas.StrPID = id;
                ProjectBas.StrProID = dt.Rows[0]["ProID"].ToString();
                ProjectBas.StrPname = dt.Rows[0]["Pname"].ToString();
                ProjectBas.StrPsource = dt.Rows[0]["Psource"].ToString();
                ProjectBas.StrCustomerName = dt.Rows[0]["CustomerName"].ToString();
                ProjectBas.StrGoal = dt.Rows[0]["Goal"].ToString();
                ProjectBas.StrMainContent = dt.Rows[0]["MainContent"].ToString();
            }
            return ProjectBas;
        }

        public static int UpdateProjectBas(tk_ProjectPre Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            int insertBasHis = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertBas = "update tk_ProjectBas set ProID = '" + Bas.StrProID + "',Pname = '" + Bas.StrPname + "',"
            +"Psource = '" + Bas.StrPsource + "',CustomerName = '" + Bas.StrCustomerName + "',"
            + "MainContent = '" + Bas.StrMainContent + "' where PID = '"+Bas.StrPID+"'";

            string strInserBasHis = "insert into tk_ProjectBasHis (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,"
            + "BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,UpdateTime,UpdateUser) select PID,ProID,"
            + "Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,"
            + "Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProjectBas where PID = '" + Bas.StrPID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "修改工程项目基本信息";
            Log.strLogContent = "修改工程项目基本信息成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "新建项目";

            try
            {
                if (strInserBasHis != "")
                    insertBasHis = sqlTrans.ExecuteNonQuery(strInserBasHis, CommandType.Text, null);
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + insertBasHis;
        }

        public static int DeleteProjectBas(string PID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertBas = "update tk_ProjectBas set Validate = 'i' where PID = '" + PID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = PID;
            Log.strLogTitle = "撤销工程项目";
            Log.strLogContent = "撤销工程项目成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "新建项目";
            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
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

        public static UIDataTable getImProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getImProject", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getUserLogGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getUserLog", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectQQGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectQQ", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getPrepareGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPrepare", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static int InsertProject(tk_Project Project, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = GSqlSentence.GetInsertInfoByD<tk_Project>(Project, "tk_Project"); 
            
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Project.StrPID;
            Log.strLogTitle = "添加工程项目项目跟踪信息";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目跟踪";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static tk_Project getUpdateProject(string id)
        {
            tk_Project Pro = new tk_Project();
            string strSql = "select * from tk_Project where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Pro.StrPID = dt.Rows[0]["PID"].ToString();
                Pro.StrJQType = dt.Rows[0]["JQType"].ToString();
                Pro.StrPview = dt.Rows[0]["Pview"].ToString();
                Pro.StrJQTime = dt.Rows[0]["JQTime"].ToString();
                Pro.StrFollowPerson = dt.Rows[0]["FollowPerson"].ToString();
            }
            return Pro;
        }

        public static int UpdateProject(string EID,tk_Project Project, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsert = "update tk_Project set JQType = '" + Project.StrJQType + "',Pview = '" + Project.StrPview + "',JQTime = '"+Project.StrJQTime+"',FollowPerson = '" + Project.StrFollowPerson + "' where ID = '" + EID + "'";
            string strUpdate = "insert into tk_ProjectHis (PID,JQType,Pview,JQTime,FollowPerson,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select PID,JQType,Pview,JQTime,FollowPerson,UnitID,CreatePerson,"
            + "CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_Project where ID = '" + EID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Project.StrPID;
            Log.strLogTitle = "修改工程项目项目跟踪信息";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目跟踪";

            try
            {
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdate;
        }

        public static int DeleteProject(string EID,string PID, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsert = "update tk_Project set Validate = 'i' where ID = '" + EID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = PID;
            Log.strLogTitle = "撤销工程项目项目跟踪信息";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目跟踪";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);

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

        public static DataTable GetDetailBas(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailBas", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static DataTable GetDetailJQ(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailJQ", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static UIDataTable getAppProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAppProject", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static int InsertUseProjectBas(UseProjectBas Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsertBas = "";
            if (Bas.StrPlanSignaDate != null)
            {
                strInsertBas = "insert into tk_ProjectBas (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,UnitID,Validate,AppID,BuildUnit,LinkMan,"
                            + "Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract)"
                            + " values ('" + Bas.StrPID + "','" + Bas.StrProID + "','" + Bas.StrPname + "','" + Bas.StrPsource + "','" + Bas.StrCustomerName + "','" + Bas.StrGoal + "',"
                            + "'" + Bas.StrMainContent + "','1','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "','" + Bas.StrUnitID + "','" + Bas.StrValidate + "','" + Bas.StrAppID + "',"
                            + "'" + Bas.StrBuildUnit + "','" + Bas.StrLinkMan + "','" + Bas.StrPhone + "','" + Bas.StrPaddress + "','" + Bas.StrPrincipal + "','" + Bas.StrConcertPerson + "',"
                            + "'" + Bas.StrContractAmount + "','" + Bas.StrBudget + "','" + Bas.StrCost + "','" + Bas.StrProfit + "','" + Bas.StrSchedule + "','" + Bas.StrPlanSignaDate + "',"
                            + "'" + Bas.StrAppDate + "','" + Bas.StrAppUser + "','" + Bas.StrIsDesign + "','" + Bas.StrIsPrice + "','" + Bas.StrIsBudget + "',"
                            + "'" + Bas.StrIsCBack + "','" + Bas.StrIsContract + "')";
            }
            else
            {

                strInsertBas = "insert into tk_ProjectBas (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,UnitID,Validate,AppID,BuildUnit,LinkMan,"
                            + "Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract)"
                            + " values ('" + Bas.StrPID + "','" + Bas.StrProID + "','" + Bas.StrPname + "','" + Bas.StrPsource + "','" + Bas.StrCustomerName + "','" + Bas.StrGoal + "',"
                            + "'" + Bas.StrMainContent + "','1','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "','" + Bas.StrUnitID + "','" + Bas.StrValidate + "','" + Bas.StrAppID + "',"
                            + "'" + Bas.StrBuildUnit + "','" + Bas.StrLinkMan + "','" + Bas.StrPhone + "','" + Bas.StrPaddress + "','" + Bas.StrPrincipal + "','" + Bas.StrConcertPerson + "',"
                            + "'" + Bas.StrContractAmount + "','" + Bas.StrBudget + "','" + Bas.StrCost + "','" + Bas.StrProfit + "','" + Bas.StrSchedule + "',"
                            + "'" + Bas.StrAppDate + "','" + Bas.StrAppUser + "','" + Bas.StrIsDesign + "','" + Bas.StrIsPrice + "','" + Bas.StrIsBudget + "',"
                            + "'" + Bas.StrIsCBack + "','" + Bas.StrIsContract + "')";
            }

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "工程项目立项";
            Log.strLogContent = "立项成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";

            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        public static int AppProjectBas(tk_ProjectBas Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsertBas = "";
            if (Bas.StrPlanSignaDate != null)
            {
                strInsertBas = "update tk_ProjectBas set AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "PlanSignaDate = '" + Bas.StrPlanSignaDate + "',AppDate = '" + Bas.StrAppDate + "',AppUser = '" + Bas.StrAppUser + "',IsDesign = '" + Bas.StrIsDesign + "',IsPrice = '" + Bas.StrIsPrice + "',"
                + "IsBudget = '" + Bas.StrIsBudget + "',IsCBack = '" + Bas.StrIsCBack + "',IsContract = '"+Bas.StrIsContract+"',State = '1' where PID = '" + Bas.StrPID + "'";
            }
            else
            {
                strInsertBas = "update tk_ProjectBas set AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "AppDate = '" + Bas.StrAppDate + "',AppUser = '" + Bas.StrAppUser + "',IsDesign = '" + Bas.StrIsDesign + "',IsPrice = '" + Bas.StrIsPrice + "',"
                + "IsBudget = '" + Bas.StrIsBudget + "',IsCBack = '" + Bas.StrIsCBack + "',IsContract = '" + Bas.StrIsContract + "',State = '1' where PID = '" + Bas.StrPID + "'";
            } 
            
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "工程项目立项";
            Log.strLogContent = "立项成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";

            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        public static UseProjectBas getUseUpdateSetUp(string id)
        {
            UseProjectBas Bas = new UseProjectBas();
            string strSql = "select * from tk_ProjectBas where PID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Bas.StrPID = id;
                Bas.StrProID = dt.Rows[0]["ProID"].ToString();
                Bas.StrPname = dt.Rows[0]["Pname"].ToString();
                Bas.StrPsource = dt.Rows[0]["Psource"].ToString();
                Bas.StrCustomerName = dt.Rows[0]["CustomerName"].ToString();
                Bas.StrGoal = dt.Rows[0]["Goal"].ToString();
                Bas.StrMainContent = dt.Rows[0]["MainContent"].ToString();
                Bas.StrAppID = dt.Rows[0]["AppID"].ToString();
                Bas.StrBuildUnit = dt.Rows[0]["BuildUnit"].ToString();
                Bas.StrLinkMan = dt.Rows[0]["LinkMan"].ToString();
                Bas.StrPhone = dt.Rows[0]["Phone"].ToString();
                Bas.StrPaddress = dt.Rows[0]["Paddress"].ToString();
                Bas.StrPrincipal = dt.Rows[0]["Principal"].ToString();
                Bas.StrConcertPerson = dt.Rows[0]["ConcertPerson"].ToString();
                Bas.StrContractAmount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
                Bas.StrBudget = Convert.ToDecimal(dt.Rows[0]["Budget"]);
                Bas.StrCost = Convert.ToDecimal(dt.Rows[0]["Cost"]);
                Bas.StrProfit = Convert.ToDecimal(dt.Rows[0]["Profit"]);
                Bas.StrSchedule = dt.Rows[0]["Schedule"].ToString();
                string PlanSignaDate = dt.Rows[0]["PlanSignaDate"].ToString();
                if (PlanSignaDate != "")
                    Bas.StrPlanSignaDate = Convert.ToDateTime(dt.Rows[0]["PlanSignaDate"]).ToString("yyyy-MM-dd");
                Bas.StrAppDate = Convert.ToDateTime(dt.Rows[0]["AppDate"]).ToString("yyyy-MM-dd");
                Bas.StrIsContract = Convert.ToInt16(dt.Rows[0]["IsContract"]);
            }
            return Bas;
        }

        public static tk_ProjectBas getUpdateSetUp(string id)
        {
            tk_ProjectBas Bas = new tk_ProjectBas();
            string strSql = "select AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,IsContract from tk_ProjectBas where PID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Bas.StrPID = id;
                Bas.StrAppID = dt.Rows[0]["AppID"].ToString();
                Bas.StrBuildUnit = dt.Rows[0]["BuildUnit"].ToString();
                Bas.StrLinkMan = dt.Rows[0]["LinkMan"].ToString();
                Bas.StrPhone = dt.Rows[0]["Phone"].ToString();
                Bas.StrPaddress = dt.Rows[0]["Paddress"].ToString();
                Bas.StrPrincipal = dt.Rows[0]["Principal"].ToString();
                Bas.StrConcertPerson = dt.Rows[0]["ConcertPerson"].ToString();
                Bas.StrContractAmount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
                Bas.StrBudget = Convert.ToDecimal(dt.Rows[0]["Budget"]);
                Bas.StrCost = Convert.ToDecimal(dt.Rows[0]["Cost"]);
                Bas.StrProfit = Convert.ToDecimal(dt.Rows[0]["Profit"]);
                Bas.StrSchedule = dt.Rows[0]["Schedule"].ToString();
                string PlanSignaDate = dt.Rows[0]["PlanSignaDate"].ToString();
                if (PlanSignaDate != "")
                    Bas.StrPlanSignaDate = Convert.ToDateTime(dt.Rows[0]["PlanSignaDate"]).ToString("yyyy-MM-dd");
                Bas.StrAppDate = Convert.ToDateTime(dt.Rows[0]["AppDate"]).ToString("yyyy-MM-dd");
                Bas.StrIsContract = Convert.ToInt16(dt.Rows[0]["IsContract"]);
            }
            return Bas;
        }

        public static int UseUpNewSetUp(UseProjectBas Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            int insertBasHis = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertBas = "";
            if (Bas.StrPlanSignaDate != null)
            {
                strInsertBas = "update tk_ProjectBas set ProID ='" + Bas.StrProID + "',Pname = '" + Bas.StrPname + "',Psource = '" + Bas.StrPsource + "', CustomerName = '" + Bas.StrCustomerName + "',"
                + "Goal ='" + Bas.StrGoal + "',MainContent ='" + Bas.StrMainContent + "',AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "PlanSignaDate = '" + Bas.StrPlanSignaDate + "',AppDate = '" + Bas.StrAppDate + "',IsContract = '" + Bas.StrIsContract + "' where PID = '" + Bas.StrPID + "'";
            }
            else
            {
                strInsertBas = "update tk_ProjectBas set ProID ='" + Bas.StrProID + "',Pname = '" + Bas.StrPname + "',Psource = '" + Bas.StrPsource + "', CustomerName = '" + Bas.StrCustomerName + "',"
                + "Goal ='" + Bas.StrGoal + "',MainContent ='" + Bas.StrMainContent + "',AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "PlanSignaDate = null,AppDate = '" + Bas.StrAppDate + "',IsContract = '" + Bas.StrIsContract + "' where PID = '" + Bas.StrPID + "'";
            }

            string strInserBasHis = "insert into tk_ProjectBasHis (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,"
            + "BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,UpdateTime,UpdateUser) select PID,ProID,"
            + "Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,"
            + "Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProjectBas where PID = '" + Bas.StrPID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "修改项目立项信息";
            Log.strLogContent = "修改项目立项信息信息成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";
            try
            {
                if (strInserBasHis != "")
                    insertBasHis = sqlTrans.ExecuteNonQuery(strInserBasHis, CommandType.Text, null);
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + insertBasHis;
        }

        public static int UpdateSetUp(tk_ProjectBas Bas, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            int insertBasHis = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertBas = "";
            if (Bas.StrPlanSignaDate != null)
            {
                strInsertBas = "update tk_ProjectBas set AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "PlanSignaDate = '" + Bas.StrPlanSignaDate + "',AppDate = '" + Bas.StrAppDate + "',IsContract = '"+Bas.StrIsContract+"' where PID = '" + Bas.StrPID + "'";
            }
            else
            {
                strInsertBas = "update tk_ProjectBas set AppID = '" + Bas.StrAppID + "',BuildUnit = '" + Bas.StrBuildUnit + "',LinkMan = '" + Bas.StrLinkMan + "',Phone = '" + Bas.StrPhone + "',"
                + "Paddress = '" + Bas.StrPaddress + "',Principal = '" + Bas.StrPrincipal + "',ConcertPerson = '" + Bas.StrConcertPerson + "',ContractAmount = '" + Bas.StrContractAmount + "',"
                + "Budget = '" + Bas.StrBudget + "',Cost = '" + Bas.StrCost + "',Profit = '" + Bas.StrProfit + "',Schedule = '" + Bas.StrSchedule + "',"
                + "PlanSignaDate = null,AppDate = '" + Bas.StrAppDate + "',IsContract = '" + Bas.StrIsContract + "' where PID = '" + Bas.StrPID + "'";
            }

            string strInserBasHis = "insert into tk_ProjectBasHis (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,"
            + "BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,UpdateTime,UpdateUser) select PID,ProID,"
            + "Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,"
            + "Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProjectBas where PID = '" + Bas.StrPID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bas.StrPID;
            Log.strLogTitle = "修改项目立项信息";
            Log.strLogContent = "修改项目立项信息信息成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";
            try
            {
                if (strInserBasHis != "")
                    insertBasHis = sqlTrans.ExecuteNonQuery(strInserBasHis, CommandType.Text, null);
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + insertBasHis;
        }

        public static int deleteUseApp(string PID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            int insertBasHis = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsertBas = "update tk_ProjectBas set Validate = 'i' where PID = '" + PID + "'";

            string strInserBasHis = "insert into tk_ProjectBasHis (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,"
            + "BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,UpdateTime,UpdateUser) select PID,ProID,"
            + "Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,"
            + "Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProjectBas where PID = '" + PID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = PID;
            Log.strLogTitle = "撤销项目立项";
            Log.strLogContent = "撤销项目立项成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";
            try
            {
                if (strInserBasHis != "")
                    insertBasHis = sqlTrans.ExecuteNonQuery(strInserBasHis, CommandType.Text, null);
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + insertBasHis;
        }

        public static int deleteApp(string PID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intlog = 0;
            int insertBasHis = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsertBas = "update tk_ProjectBas set AppID = NULL,BuildUnit = NULL,LinkMan = NULL,Phone = NULL,"
                + "Paddress = NULL,Principal = NULL,ConcertPerson = NULL,ContractAmount = NULL,"
                + "Budget = NULL,Cost = NULL,Profit = NULL,Schedule = NULL,"
                + "PlanSignaDate = NULL,AppDate = NULL,AppUser = NULL,State = '0',IsDesign = NULL,IsPrice = NULL,IsBudget = NULL,IsCBack = NULL,IsContract = NULL where PID = '" + PID + "'";

            string strInserBasHis = "insert into tk_ProjectBasHis (PID,ProID,Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,"
            + "BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,UpdateTime,UpdateUser) select PID,ProID,"
            + "Pname,Psource,CustomerName,Goal,MainContent,State,CreateUser,CreateTime,FinishDate,UnitID,Validate,AppID,BuildUnit,LinkMan,Phone,Paddress,Principal,ConcertPerson,ContractAmount,Budget,"
            + "Cost,Profit,Schedule,PlanSignaDate,AppDate,AppUser,IsDesign,IsPrice,IsBudget,IsCBack,IsContract,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProjectBas where PID = '" + PID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = PID;
            Log.strLogTitle = "撤销项目立项";
            Log.strLogContent = "撤销项目立项成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目立项";
            try
            {
                if (strInserBasHis != "")
                    insertBasHis = sqlTrans.ExecuteNonQuery(strInserBasHis, CommandType.Text, null);
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + insertBasHis;
        }

        public static DataTable GetDetailApp(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailApp", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static DataTable getUmExamine(string BuType)
        {
            string str = "select distinct(Duty),Level from UM_Examine where BuType = '"+BuType+"' order by Level";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            return dt;
        }


        public static DataTable getUmExamineContent(string StrRID,string BuType)
        {
            DataTable dt = new DataTable();
            string PID = "";
            string strID = "SELECT * FROM tk_approval where PID NOT IN (SELECT PID FROM tk_approval WHERE STATE = '-1') AND relevanceid = '" + StrRID + "' and ApprovalContent = '" + BuType + "'";
            DataTable dtID = SQLBase.FillTable(strID, "MainProject");
            if (dtID.Rows.Count > 0)
            {
                PID = dtID.Rows[0]["PID"].ToString();
            }
            if (PID != "")
            {
                string str = "select a.Job,b.UserName,a.Opinion,Convert(varchar(10),a.ApprovalTime,120) as ApprovalTime,"
                        + " case when State = '0' then '未审批' else '审批通过'  end as statedesc from tk_approval a"
                        + " left join [BJOI_UM]..UM_UserNew b on a.ApprovalPersons = b.UserId"
                        + " where a.PID = '"+PID+"'";
                dt = SQLBase.FillTable(str, "MainProject");
            }
            return dt;
        }

        public static string GetShowSPid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select SPID, SPidNo from tk_SPIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_SPIDno (SPID,SPidNo,DateRecord) values('SP',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["SPidNo"]);
            }

            intNewID++;
            string str = "select SPID, SPidNo,DateRecord from tk_SPIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["SPID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetSPid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select SPID, SPidNo from tk_SPIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_SPIDno (SPID,SPidNo,DateRecord) values('P',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["SPidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_SPIDno set SPidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["SPID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static UIDataTable getAppExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAppExamineProject", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static int InsertPreDesign(tk_PreDesign Design, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdateIs = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            //Design.Strsid = getsid(Design.StrPID);
            string strInsert = "insert into tk_PreDesign (sid,PID,DesignType,Pview,UnitID,CreatePerson,CreateTime,State,Validate) values"
                + " ('" + Design.Strsid + "','" + Design.StrPID + "','" + Design.StrDesignType + "','" + Design.StrPview + "','"+Design.StrUnitID+"','" + Design.StrCreatePerson + "','" + Design.StrCreateTime + "','" + Design.StrState + "','" + Design.StrValidate + "')";

            string strUpdateIs = "update tk_ProjectBas set IsDesign = '1' where PID = '"+Design.StrPID+"'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Design.StrPID;
            Log.strLogTitle = "项目设计内容";
            Log.strLogContent = "项目设计内容保存成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目设计";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateIs != "")
                    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int InsertPrice(tk_Price Price,  ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intInsertFile = 0;
            int intUpdateIs = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            //Price.Stroid = getoid(Price.StrPID);
            string strInsert = "insert into tk_Price (oid,PID,Pview,PriceAmount,UnitID,CreatePerson,CreateTime,State,Validate) values"
                + " ('" + Price.Stroid + "','" + Price.StrPID + "','" + Price.StrPview + "','"+Price.StrPriceAmount+"','" + Price.StrUnitID + "','" + Price.StrCreatePerson + "','" + Price.StrCreateTime + "','" + Price.StrState + "','" + Price.StrValidate + "')";

           
            string strUpdateIs = "update tk_ProjectBas set IsPrice = '1' where PID = '" + Price.StrPID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Price.StrPID;
            Log.strLogTitle = "项目报价内容";
            Log.strLogContent = "项目报价内容保存成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目报价";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateIs != "")
                    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int UpdatePrice(tk_Price Price,  ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdate = 0;
            int intInsertFile = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_Price set Pview= '" + Price.StrPview + "',PriceAmount = '"+Price.StrPriceAmount+"' where oid = '" + Price.Stroid + "'";
            string strInsertHis = "insert into tk_PriceHis (oid,PID,Pview,PriceAmount,UnitID,CreatePerson,CreateTime,State,Validate,UpdateTime,UpdateUser) select oid,PID,Pview,PriceAmount,UnitID,CreatePerson,"
                + "CreateTime,State,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_Price where oid = '" + Price.Stroid + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Price.StrPID;
            Log.strLogTitle = "项目报价内容修改";
            Log.strLogContent = "项目报价内容修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目报价";

            try
            {
                if (strInsertHis != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdate;
        }

        public static string getsid(string PID)
        {
            string sid = "";
            string str = "select sid from tk_PreDesign where PID = '"+PID+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["sid"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
                Cmax++;
                string h = "";
                if (Cmax.ToString().Length == 1)
                {
                    h = "0" + Cmax;
                }
                else {
                    h = Cmax.ToString();
                }
                sid = PID + "-sj" + h;
            }
            else
            {
                sid = PID + "-sj01";
            }
            return sid;
        }

        public static string getoid(string PID)
        {
            string oid = "";
            string str = "select oid from tk_Price where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["oid"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
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
                oid = PID + "-bj" + h;
            }
            else
            {
                oid = PID + "-bj01";
            }
            return oid;
        }

        public static string getbid(string PID)
        {
            string oid = "";
            string str = "select bid from tk_Budget where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["oid"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
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
                oid = PID + "-ys" + h;
            }
            else
            {
                oid = PID + "-ys01";
            }
            return oid;
        }

        public static string getbiddingid(string PID)
        {
            string oid = "";
            string str = "select BidID from tk_Bidding where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["BidID"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
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
                oid = PID + "-tb" + h;
            }
            else
            {
                oid = PID + "-tb01";
            }
            return oid;
        }

        public static UIDataTable getDesignGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getDesign", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getPriceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPrice", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectDesignGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectDesign", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectPriceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectPrice", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_Price getUpdatePrice(string id)
        {
            tk_Price Price = new tk_Price();
            string str = "select * from tk_Price where oid = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Price.Stroid = id;
                Price.StrPID = dt.Rows[0]["PID"].ToString();
                Price.StrPview = dt.Rows[0]["Pview"].ToString();
                Price.StrPriceAmount = Convert.ToDecimal(dt.Rows[0]["PriceAmount"]);
            }
            return Price;
        }

        public static tk_PreDesign getUpdatePreDesign(string id)
        {
            tk_PreDesign Design = new tk_PreDesign();
            string str = "select * from tk_PreDesign where sid = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Design.Strsid = id;
                Design.StrPID = dt.Rows[0]["PID"].ToString();
                Design.StrDesignType = dt.Rows[0]["DesignType"].ToString();
                Design.StrPview = dt.Rows[0]["Pview"].ToString();
            }
            return Design;
        }

        public static DataTable GetCashBackDownloadOne(string id)
        {
            string strSql = "select ID,FileInfo,FileName from tk_ProjectCashBackFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetCashBackDownload(string id)
        {
            string strSql = "select ID,FileInfo,FileName from tk_ProjectCashBackFile where bid = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownloadCompletions(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_ProCompletionsFile where cid = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownload(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_PreDesignFile where sid = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetPriceDownload(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_PriceFile where oid = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetBudgetDownload(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_BudgetFile where bid = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetSubWorkDownload(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_SubWorkFile where EID = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static int DeleteCashBackFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            DataTable dt = GetCashBackDownloadOne(ID);
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
            string strInsertOrder = "update tk_ProjectCashBackFile set Validate = 'i' where ID = '" + ID + "'";
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

        public static int DeleteCompletionsFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetProCompletionsDownload(ID);
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
            string strInsertOrder = "update tk_ProCompletionsFile set Validate = 'i' where ID = '" + ID + "'";
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
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadFile(ID);
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
            string strInsertOrder = "update tk_PreDesignFile set Validate = 'i' where ID = '"+ID+"'";
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

        public static int DeletePriceFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadPriceFile(ID);
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
            string strInsertOrder = "update tk_PriceFile set Validate = 'i' where ID = '" + ID + "'";
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

        public static int DeleteBiddingFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadBiddingFile(ID);
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
            string strInsertOrder = "update tk_BiddingFile set Validate = 'i' where ID = '" + ID + "'";
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

        public static int DeleteBudgetFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadBudgetFile(ID);
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
            string strInsertOrder = "update tk_BudgetFile set Validate = 'i' where ID = '" + ID + "'";
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

        public static int DeleteSubWorkFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            DataTable dt = GetDownloadSubWorkFile(ID);
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
            string strInsertOrder = "update tk_SubWorkFile set Validate = 'i' where ID = '" + ID + "'";
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

        public static int UpdatePreDesign(tk_PreDesign Design, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intInsertHis = 0;
            int intInsertFile = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_PreDesign set DesignType ='" + Design.StrDesignType + "' ,Pview = '" + Design.StrPview + "' where sid = '"+Design.Strsid+"'";
            string strInsertHis = "insert into tk_PreDesignHis (sid,PID,DesignType,Pview,UnitID,CreatePerson,CreateTime,State,Validate,UpdateTime,UpdateUser) select sid,PID,DesignType,Pview,"
            + "UnitID,CreatePerson,CreateTime,State,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_PreDesign where sid =  '" + Design.Strsid + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Design.StrPID;
            Log.strLogTitle = "项目设计内容修改";
            Log.strLogContent = "项目设计内容修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目设计";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static DataTable GetDownloadFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_PreDesignFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownloadPriceFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_PriceFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownloadBudgetFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_BudgetFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static DataTable GetDownloadSubWorkFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_SubWorkFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static int DeleteDesign(string ID, string pid,ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdate = 0;
            //int intUpdateIs = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_PreDesign set Validate = 'i' where sid = '" + ID + "'";
            string strUpdate = "update tk_PreDesignFile set Validate = 'i' where sid = '" + ID + "'";

            //string str = "select * from tk_PreDesign where PID = '" + pid + "' and Validate = 'v'";
            //DataTable dt = SQLBase.FillTable(str, "MainProject");
            //string strUpdateIs = "";
            //if (dt.Rows.Count == 0)
            //{
            //    strUpdateIs = "update tk_ProjectBas set IsDesign = '0' where PID = '" + pid + "'";
            //}
            //if (strUpdateIs != "")
            //    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销项目设计内容";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目设计";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeletePrice(string ID, string pid, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdate = 0;
            //int intUpdateIs = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_Price set Validate = 'i' where oid = '" + ID + "'";
            string strUpdate = "update tk_PriceFile set Validate = 'i' where oid = '" + ID + "'";

            //string str = "select * from tk_PreDesign where PID = '" + pid + "' and Validate = 'v'";
            //DataTable dt = SQLBase.FillTable(str, "MainProject");
            //string strUpdateIs = "";
            //if (dt.Rows.Count == 0)
            //{
            //    strUpdateIs = "update tk_ProjectBas set IsDesign = '0' where PID = '" + pid + "'";
            //}
            //if (strUpdateIs != "")
            //    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销项目报价内容";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目报价";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getDesignExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getDesignExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getPriceExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPriceExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getBudgetExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBudgetExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getBiddingExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBiddingExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static string GetShowBid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'B'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('B',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string str = "select idName, idNo,DateRecord from tk_IDno where DateRecord='" + strYMD + "' and idName = 'B'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetBid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'B'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('B',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_IDno set idNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and idName = 'B'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int InsertBudget(tk_Budget Budget, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intInsertFile = 0;
            int intUpdateIs = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            //Budget.Strbid = getbid(Budget.StrPID);
            string strInsert = "insert into tk_Budget (bid,PID,Pview,UnitID,CreatePerson,CreateTime,State,Validate) values"
                + " ('" + Budget.Strbid + "','" + Budget.StrPID + "','" + Budget.StrPview + "','" + Budget.StrUnitID + "','" + Budget.StrCreatePerson + "','" + Budget.StrCreateTime + "','" + Budget.StrState + "','" + Budget.StrValidate + "')";

            string strUpdateIs = "update tk_ProjectBas set IsBudget = '1' where PID = '" + Budget.StrPID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Budget.StrPID;
            Log.strLogTitle = "项目预算内容";
            Log.strLogContent = "项目预算内容保存成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目预算";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateIs != "")
                    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getBudgetGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBudget", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectBudgetGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectBudget", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_Budget getUpdateBudget(string bid)
        {
            tk_Budget Budget = new tk_Budget();
            string str = "select * from tk_Budget where bid = '"+bid+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Budget.Strbid = bid;
                Budget.StrPID = dt.Rows[0]["PID"].ToString();
                Budget.StrPview = dt.Rows[0]["Pview"].ToString();
            }
            return Budget;
        }

        public static int UpdateBudget(tk_Budget Budget,  ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdate = 0;
            int intInsertFile = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_Budget set Pview= '" + Budget.StrPview + "' where bid = '" + Budget.Strbid + "'";
            string strInsertHis = "insert into tk_BudgetHis (bid,PID,Pview,UnitID,CreatePerson,CreateTime,State,Validate,UpdateTime,UpdateUser) select bid,PID,Pview,UnitID,CreatePerson,"
                + "CreateTime,State,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_Budget where bid = '" + Budget.Strbid + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Budget.StrPID;
            Log.strLogTitle = "项目预算内容修改";
            Log.strLogContent = "项目预算内容修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "项目预算";

            try
            {
                if (strInsertHis != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdate;
        }

        public static int DeleteBudget(string ID, string pid,ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_Budget set Validate = 'i' where bid = '" + ID + "'";
            string strUpdate = "update tk_BudgetFile set Validate = 'i' where bid = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目成本预算信息";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "成本预算";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static string GetShowBiddingID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'T'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('T',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string str = "select idName, idNo,DateRecord from tk_IDno where DateRecord='" + strYMD + "' and idName = 'T'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetBiddingID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'T'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('T',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_IDno set idNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and idName = 'T'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int InsertBidding(tk_Bidding Bidding, ref string a_strErr)
        {
            int intlog = 0;
            int intInsert = 0;
            int intInsertFile = 0;
            int intUpdateIs = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();

            Bidding.StrBidID = getbiddingid(Bidding.StrPID);
            string strInsert = "insert into tk_Bidding (BidID,PID,Pview,BiddingTime,UnitID,CreatePerson,CreateTime,State,Validate) values"
                + " ('" + Bidding.StrBidID + "','" + Bidding.StrPID + "','" + Bidding.StrPview + "','"+Bidding.StrBiddingTime+"','" + Bidding.StrUnitID + "','" + Bidding.StrCreatePerson + "','" + Bidding.StrCreateTime + "','" + Bidding.StrState + "','" + Bidding.StrValidate + "')";

            string strUpdateIs = "update tk_ProjectBas set State = '4' where PID = '" + Bidding.StrPID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bidding.StrPID;
            Log.strLogTitle = "添加工程项目投标信息";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "投标";
            try
            {
                sqlTrans.Open("MainProject");
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateIs != "")
                    intUpdateIs = sqlTrans.ExecuteNonQuery(strUpdateIs, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getBiddingGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBidding", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectBiddingGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectBidding", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_Bidding getUpdateBidding(string id)
        {
            tk_Bidding Bidding = new tk_Bidding();
            string str = "select * from tk_Bidding where BidID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Bidding.StrBidID = id;
                Bidding.StrPID = dt.Rows[0]["PID"].ToString();
                Bidding.StrPview = dt.Rows[0]["Pview"].ToString();
                Bidding.StrBiddingTime = dt.Rows[0]["BiddingTime"].ToString();
            }
            return Bidding;
        }

        public static DataTable GetBiddingDownload(string id)
        {
            string strSql = "select ID,[FileInfo],FileName from tk_BiddingFile where BidID = '" + id + "' and Validate = 'v'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static int UpdateBidding(tk_Bidding Bidding,  ref string a_strErr)
        {
            int intlog = 0;
            int intInsert = 0;
            int intInsertHis = 0;
            int intInsertFile = 0;

            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();

            string strInsert = "update tk_Bidding set Pview = '" + Bidding.StrPview + "',BiddingTime = '" + Bidding.StrBiddingTime + "' where BidID = '" + Bidding.StrBidID + "'";

            string strInsertHis = "insert into tk_BiddingHis (BidID,PID,Pview,BiddingTime,UnitID,CreatePerson,CreateTime,State,Validate,UpdateTime,UpdateUser) select BidID,"
            + "PID,Pview,BiddingTime,UnitID,CreatePerson,CreateTime,State,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_Bidding where BidID = '" + Bidding.StrBidID + "'";
           

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Bidding.StrPID;
            Log.strLogTitle = "修改工程项目投标信息";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "投标";
            try
            {
                sqlTrans.Open("MainProject");
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeleteBidding(string ID, string pid, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intlog = 0;
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_Bidding set Validate = 'i' where BidID = '" + ID + "'";
            string strUpdate = "update tk_BiddingFile set Validate = 'i' where BidID = '" + ID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销项目投标内容";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "投标";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static DataTable GetDownloadBiddingFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_BiddingFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static int InsertSchedule(tk_Schedule Schedule, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "insert into tk_Schedule(PID,Pview,UnitID,CreatePerson,CreateTime,Validate) values ('" + Schedule.StrPID + "','" + Schedule.StrPview + "','"+Schedule.StrUnitID+"','" + Schedule.StrCreatePerson + "','" + Schedule.StrCreateTime + "','" + Schedule.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Schedule.StrPID;
            Log.strLogTitle = "添加工程项目过程记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "过程记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getScheduleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSchedule", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectScheduleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectSchedule", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_Schedule getUpdateSchedule(string id)
        {
            tk_Schedule Schedule = new tk_Schedule();
            string str = "select * from tk_Schedule where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Schedule.StrPID = dt.Rows[0]["PID"].ToString();
                Schedule.StrPview = dt.Rows[0]["Pview"].ToString();
            }
            return Schedule;
        }

        public static int UpdateSchedule(tk_Schedule Schedule,string ID, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertHis = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_Schedule set Pview = '"+Schedule.StrPview+"' where ID = '"+ID+"'";
            string strInsertHis = "insert into tk_ScheduleHis (ID,PID,Pview,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select ID,PID,Pview,UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_Schedule where ID = '" + ID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Schedule.StrPID;
            Log.strLogTitle = "修改工程项目过程记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "过程记录";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeleteSchedule(string ID, string pid, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_Schedule set Validate = 'i' where ID = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目过程记录";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "过程记录";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static string GetShowSubID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'E'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('E',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string str = "select idName, idNo,DateRecord from tk_IDno where DateRecord='" + strYMD + "' and idName = 'E'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetSubID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'E'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('E',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_IDno set idNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and idName = 'E'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int InsertSubWork(tk_SubWork Sub, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            int intInsertFile = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            //Sub.StrEID = getEID(Sub.StrPID);
            string strInsert = "insert into tk_SubWork(EID,PID,SubUnit,Principal,SubPrice,WorkCycle,SubWorkReason,MainContent,IsSign,State,UnitID,CreatePerson,CreateTime,Validate) values"
                + " ('" + Sub.StrEID + "','" + Sub.StrPID + "','" + Sub.StrSubUnit + "','" + Sub.StrPrincipal + "','" + Sub.StrSubPrice + "','" + Sub.StrWorkCycle + "','" + Sub.StrSubWorkReason + "','" + Sub.StrMainContent + "','" + Sub.StrIsSign + "','" + Sub.StrState + "','"+Sub.StrUnitID+"','" + Sub.StrCreatePerson + "','" + Sub.StrCreateTime + "','" + Sub.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Sub.StrPID;
            Log.strLogTitle = "添加工程项目分包施工记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包施工记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static string getEID(string PID)
        {
            string oid = "";
            string str = "select EID from tk_SubWork where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["EID"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
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
                oid = PID + "-fbsg" + h;
            }
            else
            {
                oid = PID + "-fbsg01";
            }
            return oid;
        }

        public static string getSID(string PID)
        {
            string oid = "";
            string str = "select SID from tk_SubPackage where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                string strarr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["SID"].ToString();
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
                oid = PID + "-fbsj" + h;
            }
            else
            {
                oid = PID + "-fbsj01";
            }
            return oid;
        }

        public static string getPcID(string PID)
        {
            string oid = "";
            string str = "select PcID from tk_Purchase where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                string strarr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["PcID"].ToString();
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
                oid = PID + "-cg" + h;
            }
            else
            {
                oid = PID + "-cg01";
            }
            return oid;
        }

        public static string getPayID(string PID)
        {
            string oid = "";
            string str = "select PayID from tk_PayRecord where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                string strarr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["PayID"].ToString();
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
                oid = PID + "-zc" + h;
            }
            else
            {
                oid = PID + "-zc01";
            }
            return oid;
        }

        public static UIDataTable getSubWorkGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSubWork", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getPurchaseGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectPurchase", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectSubWorkGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectSubWork", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_SubWork getUpdateSubWork(string id)
        {
            tk_SubWork Sub = new tk_SubWork();
            string str = "select * from tk_SubWork where EID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Sub.StrEID = id;
                Sub.StrPID = dt.Rows[0]["PID"].ToString();
                Sub.StrSubUnit = dt.Rows[0]["SubUnit"].ToString();
                Sub.StrPrincipal = dt.Rows[0]["Principal"].ToString();
                Sub.StrSubPrice = Convert.ToDecimal(dt.Rows[0]["SubPrice"]);
                Sub.StrWorkCycle = dt.Rows[0]["WorkCycle"].ToString();
                Sub.StrSubWorkReason = dt.Rows[0]["SubWorkReason"].ToString();
                Sub.StrMainContent = dt.Rows[0]["MainContent"].ToString();
                Sub.StrIsSign = Convert.ToInt16(dt.Rows[0]["IsSign"]);
            }
            return Sub;
        }

        public static int updateSubWork(tk_SubWork Sub,  ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertHis = 0;
            int intlog = 0;
            int intInsertFile = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_SubWork set SubUnit = '" + Sub.StrSubUnit + "',Principal = '" + Sub.StrPrincipal + "',SubPrice = '" + Sub.StrSubPrice + "',WorkCycle = '" + Sub.StrWorkCycle + "',SubWorkReason = '" + Sub.StrSubWorkReason + "',"
                + "MainContent = '" + Sub.StrMainContent + "',IsSign = '" + Sub.StrIsSign + "' where EID = '"+Sub.StrEID+"'";
            string strInsertHis = "insert into tk_SubWorkHis (EID,PID,SubUnit,Principal,SubPrice,WorkCycle,SubWorkReason,MainContent,IsSign,State,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select EID,PID,SubUnit,Principal,"
                + "SubPrice,WorkCycle,SubWorkReason,MainContent,IsSign,State,UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_SubWork where EID = '" + Sub.StrEID + "'";


            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Sub.StrPID;
            Log.strLogTitle = "修改工程项目分包施工记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包施工记录";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeleteSubWork(string ID, string pid, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_SubWork set Validate = 'i' where EID = '" + ID + "'";
            string strUpdateOrder = "update tk_SubWorkFile set Validate = 'i' where EID = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目分包施工记录";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包施工记录";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strUpdateOrder != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdateOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static DataTable GetPrintSubWork(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintSubWork", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static DataTable GetPrintPurchase(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintPurchase", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static UIDataTable getPurchaseExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPurchaseExamin", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getSubWorkExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSubWorkExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static string GetShowPackageID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'S'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('S',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string str = "select idName, idNo,DateRecord from tk_IDno where DateRecord='" + strYMD + "' and idName = 'S'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetPackageID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'S'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('S',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_IDno set idNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and idName = 'S'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int InsertPurchase(tk_ProjectPurchase Purchase, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            Purchase.StrPcID = getPcID(Purchase.StrPID);
            string strInsert = "insert into tk_Purchase (PcID,PID,PcName,PartA,PartB,PcNum,Pname,PcAmount,PcType,UnitID,CreatePerson,CreateTime,State,Validate) values ('" + Purchase.StrPcID + "','" + Purchase.StrPID + "','" + Purchase.StrPcName + "','" + Purchase.StrPartA + "','" + Purchase.StrPartB + "',"
                + "'" + Purchase.StrPcNum + "','" + Purchase.StrPname + "','" + Purchase.StrPcAmount + "','" + Purchase.StrPcType + "','" + Purchase.StrUnitID + "','" + Purchase.StrCreatePerson + "','" + Purchase.StrCreateTime + "','" + Purchase.StrState + "','" + Purchase.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Purchase.StrPID;
            Log.strLogTitle = "添加工程项目采购记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "采购记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int UpdatePurchase(tk_ProjectPurchase Purchase, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_Purchase set PcName = '" + Purchase.StrPcName + "',PartA = '" + Purchase.StrPartA + "',PartB = '" + Purchase.StrPartB + "',PcNum = '" + Purchase.StrPcNum + "',PcAmount = '" + Purchase.StrPcAmount + "',"
                + "PcType = '" + Purchase.StrPcType + "' where PcID = '" + Purchase.StrPcID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Purchase.StrPID;
            Log.strLogTitle = "修改工程项目采购记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "采购记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int InsertSubPackage(tk_SubPackage Package, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            Package.StrSID = getSID(Package.StrPID);
            string strInsert = "insert into tk_SubPackage (SID,PID,DesignUnit,DesignPrincipal,DesignPrice,PredictCycle,SubReason,MainContent,State,UnitID,CreatePerson,CreateTime,Validate) values ('" + Package.StrSID + "','" + Package.StrPID + "',"
                + "'" + Package.StrDesignUnit + "','" + Package.StrDesignPrincipal + "','" + Package.StrDesignPrice + "','" + Package.StrPredictCycle + "','" + Package.StrSubReason + "','" + Package.StrMainContent + "','" + Package.StrState + "',"
                + "'" + Package.StrUnitID + "','" + Package.StrCreatePerson + "','" + Package.StrCreateTime + "','" + Package.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Package.StrPID;
            Log.strLogTitle = "添加工程项目分包设计记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包设计记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getSubPackageGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSubPackage", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectPurchaseGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectPurchaseGrid", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectSubPackageGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectSubPackage", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_SubPackage getUpdateSubPackage(string id)
        {
            tk_SubPackage Package = new tk_SubPackage();
            string str = "select * from tk_SubPackage where SID = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Package.StrSID = id;
                Package.StrPID = dt.Rows[0]["PID"].ToString();
                Package.StrDesignUnit = dt.Rows[0]["DesignUnit"].ToString();
                Package.StrDesignPrincipal = dt.Rows[0]["DesignPrincipal"].ToString();
                Package.StrDesignPrice = Convert.ToDecimal(dt.Rows[0]["DesignPrice"]);
                Package.StrPredictCycle = dt.Rows[0]["PredictCycle"].ToString();
                Package.StrSubReason = dt.Rows[0]["SubReason"].ToString();
                Package.StrMainContent = dt.Rows[0]["MainContent"].ToString();
            }
            return Package;
        }

        public static tk_ProjectPurchase getUpdatePurchase(string id)
        {
            tk_ProjectPurchase Purchase = new tk_ProjectPurchase();
            string str = "select * from tk_Purchase where PcID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Purchase.StrPcID = id;
                Purchase.StrPID = dt.Rows[0]["PID"].ToString();
                Purchase.StrPcName = dt.Rows[0]["PcName"].ToString();
                Purchase.StrPartA = dt.Rows[0]["PartA"].ToString();
                Purchase.StrPartB = dt.Rows[0]["PartB"].ToString();
                Purchase.StrPcNum = dt.Rows[0]["PcNum"].ToString();
                Purchase.StrPname = dt.Rows[0]["Pname"].ToString();
                Purchase.StrPcAmount = Convert.ToDecimal(dt.Rows[0]["PcAmount"]);
                Purchase.StrPcType = dt.Rows[0]["PcType"].ToString();
            }
            return Purchase;
        }

        public static int updateSubPackage(tk_SubPackage Package, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertHis = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_SubPackage set DesignUnit = '" + Package.StrDesignUnit + "',DesignPrincipal = '" + Package.StrDesignPrincipal + "',DesignPrice = '" + Package.StrDesignPrice + "',PredictCycle = '" + Package.StrPredictCycle + "',"
                + "SubReason = '" + Package.StrSubReason + "',MainContent = '" + Package.StrMainContent + "' where SID = '" + Package.StrSID + "'";
            string strInsertHis = "insert into tk_SubPackageHis (SID,PID,DesignUnit,DesignPrincipal,DesignPrice,PredictCycle,SubReason,MainContent,State,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select SID,PID,DesignUnit,DesignPrincipal,DesignPrice,"
                + "PredictCycle,SubReason,MainContent,State,UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_SubPackage where SID = '" + Package.StrSID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Package.StrPID;
            Log.strLogTitle = "修改工程项目分包设计记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包设计记录";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeletePurchase(string ID, string pid, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_Purchase set Validate = 'i' where PcID = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目采购记录";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "采购记录";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeleteSubPackage(string ID, string pid, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_SubPackage set Validate = 'i' where SID = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目分包设计记录";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "分包设计记录";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static DataTable GetPrintSubPackage(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintSubPackage", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static UIDataTable getSubPackageExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSubPackageExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static string GetShowPayID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'Pay'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('Pay',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string str = "select idName, idNo,DateRecord from tk_IDno where DateRecord='" + strYMD + "' and idName = 'Pay'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static string GetPayID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select idName, idNo from tk_IDno where DateRecord='" + strYMD + "' and idName = 'Pay'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainProject");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_IDno (idName,idNo,DateRecord) values('Pay',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainProject");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["idNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_IDno set idNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and idName = 'Pay'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainProject");

            strPID = dtPMaxID.Rows[0]["idName"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }

        public static int InsertPayRecord(tk_PayRecord Pay, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            Pay.StrPayID = getPayID(Pay.StrPID);
            string strInsert = "insert into tk_PayRecord (PayID,PID,PayType,PayPrice,PayDate,PayPerson,State,UnitID,CreatePerson,CreateTime,Validate) values ('" + Pay.StrPayID + "','" + Pay.StrPID + "','" + Pay.StrPayType + "',"
                + "'" + Pay.StrPayPrice + "','" + Pay.StrPayDate + "','" + Pay.StrPayPerson + "','" + Pay.StrState + "','"+Pay.StrUnitID+"','" + Pay.StrCreatePerson + "','" + Pay.StrCreateTime + "','" + Pay.StrValidate + "')";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Pay.StrPID;
            Log.strLogTitle = "添加工程项目费用支出记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "费用支出记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getPayRecordGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPayRecord", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectPayRecordGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectPayRecord", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_PayRecord getUpdatePayRecord(string id)
        {
            tk_PayRecord Pay = new tk_PayRecord();
            string str = "select * from tk_PayRecord where PayID = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Pay.StrPayID = id;
                Pay.StrPID = dt.Rows[0]["PID"].ToString();
                Pay.StrPayType = dt.Rows[0]["PayType"].ToString();
                Pay.StrPayPrice = Convert.ToDecimal(dt.Rows[0]["PayPrice"]);
                Pay.StrPayDate = dt.Rows[0]["PayDate"].ToString();
                Pay.StrPayPerson = dt.Rows[0]["PayPerson"].ToString();
            }
            return Pay;
        }

        public static int UpdatePayRecord(tk_PayRecord Pay, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertHis = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_PayRecord set PayType = '" + Pay.StrPayType + "',PayPrice = '" + Pay.StrPayPrice + "',PayDate = '" + Pay.StrPayDate + "',PayPerson = '" + Pay.StrPayPerson + "' where PayID = '"+Pay.StrPayID+"'";
            string strInsertHis = "insert into tk_PayRecordHis (PayID,PID,PayType,PayPrice,PayDate,PayPerson,State,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select PayID,PID,PayType,PayPrice,PayDate,PayPerson,"
                + "State,UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_PayRecord where PayID = '" + Pay.StrPayID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Pay.StrPID;
            Log.strLogTitle = "修改工程项目费用支出记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "费用支出记录";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int DeletePayRecord(string ID, string pid, ref string a_strErr)
        {
            int intInsert = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_PayRecord set Validate = 'i' where PayID = '" + ID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = pid;
            Log.strLogTitle = "撤销工程项目费用支出记录";
            Log.strLogContent = "撤销成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "费用支出记录";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static DataTable getPayRecordSTA(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPayRecordSTA", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static DataTable getPrintPayRecord(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintPayRecord", CommandType.StoredProcedure, sqlPar, "MainProject");
            return dt;
        }

        public static UIDataTable getPayRecoudExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPayRecoudExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static string getsgid(string PID)
        {
            string oid = "";
            string str = "select cid from tk_ProCompletions where PID = '" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["cid"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
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
                oid = PID + "-sgys" + h;
            }
            else
            {
                oid = PID + "-sgys01";
            }
            return oid;
        }

        public static int InsertProCompletions(tk_ProCompletions Com,ref string a_strErr)
        {
            int intlog = 0;
            int intInsert = 0;

            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();

            string strInsertOrder = "";
            strInsertOrder = "insert into tk_ProCompletions (cid,PID,CompleteDate,CompletePerson,CompleteRmark,UnitID,CreatePerson,CreateTime,Validate) values ('"+Com.Strcid+"','" + Com.StrPID + "','" + Com.StrCompleteDate + "',"
                    + "'" + Com.StrCompletePerson + "','"+Com.StrCompleteRmark+"','"+Com.StrUnitID+"','" + Com.StrCreatePerson + "','" + Com.StrCreateTime + "','" + Com.StrValidate + "')";
         

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Com.StrPID;
            Log.strLogTitle = "添加工程项目竣工验收信息";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "竣工验收";
            try
            {
                sqlTrans.Open("MainProject");
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getProCompletionsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProCompletions", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectCompletionsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectCompletions", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_ProCompletions getUpdateProCompletions(string id)
        {
            tk_ProCompletions Com = new tk_ProCompletions();
            string str = "select * from tk_ProCompletions where cid = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Com.Strcid = id;
                Com.StrPID = dt.Rows[0]["PID"].ToString();
                Com.StrCompleteDate = Convert.ToDateTime(dt.Rows[0]["CompleteDate"]).ToString("yyyy-MM-dd");
                Com.StrCompletePerson = dt.Rows[0]["CompletePerson"].ToString();
                Com.StrCompleteRmark = dt.Rows[0]["CompleteRmark"].ToString();
            }
            return Com;
        }

        public static DataTable GetProCompletionsDownload(string id)
        {
            string strSql = "select ID,FileName,FileInfo from tk_ProCompletionsFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            return dt;
        }

        public static int UpdateProCompletions(tk_ProCompletions Com, ref string a_strErr)
        {
            int intlog = 0;
            int intInsert = 0;
            int intInsertHis = 0;

            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();

            string strInsertOrder = "";
            strInsertOrder = "update tk_ProCompletions set CompleteDate = '" + Com.StrCompleteDate + "',CompletePerson = '" + Com.StrCompletePerson + "',CompleteRmark = '" + Com.StrCompleteRmark + "' where cid = '" + Com.Strcid + "'";
            
            string strInsertHis = "insert into tk_ProCompletionsHis (PID,CompleteDate,CompletePerson,CompleteRmark,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select PID,CompleteDate,CompletePerson,"
                + "CompleteRmark,UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProCompletions where  cid = '" + Com.Strcid + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Com.StrPID;
            Log.strLogTitle = "修改工程项目竣工验收信息";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "竣工验收";
            try
            {
                sqlTrans.Open("MainProject");
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static int InsertProFinish(tk_ProFinish Finish, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdateState = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsert = GSqlSentence.GetInsertInfoByD<tk_ProFinish>(Finish, "tk_ProFinish");
            string strUpdateState = "update tk_ProjectBas set FinishDate = '"+Finish.StrFinishDate+"',State = '8' where PID = '" + Finish.StrPID + "'";

            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Finish.StrPID;
            Log.strLogTitle = "添加工程项目结项记录";
            Log.strLogContent = "添加成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "结项记录";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdateState != "")
                    intUpdateState = sqlTrans.ExecuteNonQuery(strUpdateState, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdateState;
        }

        public static UIDataTable getProFinishGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProFinish", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getProjectFinishGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectFinish", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static tk_ProFinish getUpdateProFinish(string id)
        {
            tk_ProFinish Finish = new tk_ProFinish();
            string str = "select * from tk_ProFinish where PID = '"+id+"'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Finish.StrPID = id;
                Finish.StrFinishDate = Convert.ToDateTime(dt.Rows[0]["FinishDate"]).ToString("yyyy-MM-dd");
                Finish.StrIsDebt = Convert.ToInt16(dt.Rows[0]["IsDebt"]);
                Finish.StrDebtAmount = Convert.ToDecimal(dt.Rows[0]["DebtAmount"]);
                Finish.StrDebtReason = dt.Rows[0]["DebtReason"].ToString();
                Finish.StrRemark = dt.Rows[0]["Remark"].ToString();
            }
            return Finish;
        }

        public static int UpdateProFinish(tk_ProFinish Finish, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertHis = 0;
            int intlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            sqlTrans.Open("MainProject");

            string strInsert = "update tk_ProFinish set FinishDate = '" + Finish.StrFinishDate + "',IsDebt = '" + Finish.StrIsDebt + "',DebtAmount = '" + Finish.StrDebtAmount + "',DebtReason = '" + Finish.StrDebtReason + "',"
                + "Remark = '" + Finish.StrRemark + "' where PID = '"+Finish.StrPID+"'";
            string strInsertHis = "insert into tk_ProFinishHis (PID,FinishDate,IsDebt,DebtAmount,DebtReason,Remark,UnitID,CreatePerson,CreateTime,Validate,UpdateTime,UpdateUser) select PID,FinishDate,IsDebt,DebtAmount,DebtReason,Remark,"
            + "UnitID,CreatePerson,CreateTime,Validate,'" + DateTime.Now + "','" + account.UserID + "' from tk_ProFinish where PID = '" + Finish.StrPID + "'";
            string strErr = "";
            ProjectUserLog Log = new ProjectUserLog();
            Log.strRelevanceID = Finish.StrPID;
            Log.strLogTitle = "修改工程项目结项记录";
            Log.strLogContent = "修改成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "结项记录";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                intlog = UserLog(Log, ref strErr);
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

        public static UIDataTable getProCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProCashBack", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getCPlanTimeWarnGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCPlanTimeWarn", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getDebtGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getDebt", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static DataTable getProjectStatisticsdata(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("getProjectStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");
            
            return instData;
        }

        public static string getCountProject(string where)
        {
            string countStr = "";
            string str = "select count(*) from tk_ProjectBas b where 1=1 and b.ValiDate = 'v' " + where + "";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            string Have = "";
            if (dt.Rows.Count > 0)
                Have = dt.Rows[0][0].ToString();
            string str1 = "select count(*) from tk_ProjectBas b where 1=1 and b.ValiDate = 'v' and State = '8' " + where + "";
            DataTable dt1 = SQLBase.FillTable(str1, "MainProject");
            string Finish = "";
            if (dt1.Rows.Count > 0)
                Finish = dt1.Rows[0][0].ToString();
            string str2 = "select count(*) from tk_ProjectBas b where 1=1 and b.ValiDate = 'v' and State != '8' " + where + "";
            DataTable dt2 = SQLBase.FillTable(str2, "MainProject");
            string NoFinish = "";
            if (dt2.Rows.Count > 0)
                NoFinish = dt2.Rows[0][0].ToString();
            countStr = Have + "@" + Finish + "@" + NoFinish;
            return countStr;
        }

        public static DataTable getAmountStatisticsdata(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("getAmountStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");

            return instData;
        }

        public static string getCountAmount(string where)
        {
            string countStr = "";
            string str = "select count(*) from tk_ProjectBas a where 1=1 and a.Validate = 'v' "+where+"";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
                countStr = dt.Rows[0][0].ToString();
            //DataTable instData = new DataTable();

            //SqlParameter[] sqlPar = new SqlParameter[]
            //    {
            //        new SqlParameter("@Where",where)
            //    };

            //instData = SQLBase.FillTable("getCountAmountStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");

            //if (instData.Rows.Count > 0)
            //    countStr = instData.Rows[0][0].ToString();
            return countStr;
        }

        public static DataTable getDebtStatisticsdata(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("getDebtStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");

            return instData;
        }

        public static string getCountDebt(string where)
        {
            string countStr = "";
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("getCountDebtStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");

            if (instData.Rows.Count > 0)
                countStr = instData.Rows[0][0].ToString();
            return countStr;
        }

        public static UIDataTable getPurchaseSearchGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getPurchase", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getOrderGoodsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getOrderGoods", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable ProjectContractExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectContractExamine", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static UIDataTable getproductExamineGrid(int a_intPageSize, int a_intPageIndex, string where, string proName)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet(proName, CommandType.StoredProcedure, sqlPar, "MainProduce");
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

        public static string getProjectCBID(string CID)
        {
            string sid = "";
            string str = "select CBID from tk_ProjectCashBack where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                int Cmax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string num = dt.Rows[i]["CBID"].ToString();
                    string newStr = num.Substring(num.Length - 2, 2);
                    int s = Convert.ToInt16(newStr);
                    if (i == 0)
                        Cmax = s;
                    else
                    {
                        if (Cmax < s)
                            Cmax = s;
                    }
                }
                //strarr = strarr.TrimEnd(',');
                //string[] arr = strarr.Split(',');
                //string max = arr.Max();
                //int Cmax = Convert.ToInt16(max);
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

        public static int getCurProjectAmountNum(string CID)
        {
            int CurAmountNum = 0;
            string strSql = "select count(*) from tk_ProjectCashBack where CID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            if (dt.Rows.Count > 0)
            {
                CurAmountNum = Convert.ToInt16(dt.Rows[0][0]);
                CurAmountNum++;
            }
            return CurAmountNum;
        }

        public static bool checkMoney(string CID, string Money)
        {
            string str = "select ContractAmount from tk_ProjectBas where PID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            decimal Amount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
            string strS = "select sum(CBMoney) from tk_ProjectCashBack where  CID = '" + CID + "' and ValiDate = 'v'";
            DataTable dts = SQLBase.FillTable(strS, "MainProject");
            decimal Rmoney = 0;
            if (dts.Rows[0][0].ToString() != "")
                Rmoney = Convert.ToDecimal(dts.Rows[0][0]) + Convert.ToDecimal(Money);
            else
                Rmoney = Convert.ToDecimal(Money);
            if (Rmoney <= Amount)
                return true;
            else
                return false;
        }

        public static int InsertContratFile(ContractBas Bas, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_CFile (CID,FileName,FileInfo,CreateUser,CreateTime,Validate) values ('" + Bas.StrCID + "','" + FileName + "','" + savePaths + "','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_CFile (CID,FileName,FileInfo,CreateUser,CreateTime,Validate) values ('" + Bas.StrCID + "','" + FileName + "','" + savePaths + "','" + Bas.StrCreateUser + "','" + Bas.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertBiddingFile(tk_Bidding Bidding, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_BiddingFile (BidID,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Bidding.StrBidID + "','" + Bidding.StrPID + "','" + FileName + "','" + savePaths + "','" + Bidding.StrCreatePerson + "','" + Bidding.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_BiddingFile (BidID,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Bidding.StrBidID + "','" + Bidding.StrPID + "','" + FileName + "','" + savePaths + "','" + Bidding.StrCreatePerson + "','" + Bidding.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertProCompletionsFile(tk_ProCompletions Com, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_ProCompletionsFile (cid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Com.Strcid + "','" + Com.StrPID + "','" + FileName + "','" + savePaths + "','" + Com.StrCreatePerson + "','" + Com.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_ProCompletionsFile (cid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Com.Strcid + "','" + Com.StrPID + "','" + FileName + "','" + savePaths + "','" + Com.StrCreatePerson + "','" + Com.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertSubWorkFile(tk_SubWork Sub, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_SubWorkFile (EID,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Sub.StrEID + "','" + Sub.StrPID + "','" + FileName + "','" + savePaths + "','" + Sub.StrCreatePerson + "','" + Sub.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_SubWorkFile (EID,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Sub.StrEID + "','" + Sub.StrPID + "','" + FileName + "','" + savePaths + "','" + Sub.StrCreatePerson + "','" + Sub.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertBudgetFile(tk_Budget Budget, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_BudgetFile (bid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Budget.Strbid + "','" + Budget.StrPID + "','" + FileName + "','" + savePaths + "','" + Budget.StrCreatePerson + "','" + Budget.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_BudgetFile (bid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Budget.Strbid + "','" + Budget.StrPID + "','" + FileName + "','" + savePaths + "','" + Budget.StrCreatePerson + "','" + Budget.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertPriceFile(tk_Price Price, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_PriceFile (oid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Price.Stroid + "','" + Price.StrPID + "','" + FileName + "','" + savePaths + "','" + Price.StrCreatePerson + "','" + Price.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_PriceFile (oid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Price.Stroid + "','" + Price.StrPID + "','" + FileName + "','" + savePaths + "','" + Price.StrCreatePerson + "','" + Price.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertPreDesignFile(tk_PreDesign Design, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_PreDesignFile (sid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Design.Strsid + "','" + Design.StrPID + "','" + FileName + "','" + savePaths + "','" + Design.StrCreatePerson + "','" + Design.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_PreDesignFile (sid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Design.Strsid + "','" + Design.StrPID + "','" + FileName + "','" + savePaths + "','" + Design.StrCreatePerson + "','" + Design.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertProjectCCashBackFile(CCashBack Cash, HttpFileCollection Filedata, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");


            string FileName = "";
            string savePath = "";
            string filename = System.IO.Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            string path = System.Configuration.ConfigurationSettings.AppSettings["UPProject"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(path);
            }
            savePath = System.IO.Path.Combine(path, FileName);

            string strInsertFile = "";
            if (FileName != "")
            {

                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_ProjectCashBackFile (bid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Cash.StrCBID + "','" + Cash.StrCID + "','" + FileName + "','" + savePaths + "','" + Cash.StrCreateUser + "','" + Cash.StrCreateTime + "','v')";
                }
                else
                {
                    FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                    savePath = Path.Combine(path, FileName);
                    Filedata[0].SaveAs(savePath);
                    strInsertFile = "insert into tk_ProjectCashBackFile (bid,PID,FileName,FileInfo,CreatePerson,CreateTime,Validate) values ('" + Cash.StrCBID + "','" + Cash.StrCID + "','" + FileName + "','" + savePaths + "','" + Cash.StrCreateUser + "','" + Cash.StrCreateTime + "','v')";
                }
            }
            try
            {
                if (strInsertFile != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertFile, CommandType.Text, null);
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

        public static int InsertProjectCCashBack(CCashBack Cash, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string savePaths = "";
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsert = GSqlSentence.GetInsertInfoByD<CCashBack>(Cash, "tk_ProjectCashBack");
            string strUpdateBas = "";

            string str = "select ContractAmount from tk_ProjectBas where PID = '" + Cash.StrCID + "'";
            DataTable dt = sqlTrans.FillTable(str);
            decimal Amount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);

            string strS = "select sum(CBMoney) from tk_ProjectCashBack where  CID = '" + Cash.StrCID + "' and ValiDate = 'v'";
            DataTable dts = sqlTrans.FillTable(strS);
            decimal Rmoney = 0;
            if (dts.Rows[0][0].ToString() != "")
                Rmoney = Convert.ToDecimal(dts.Rows[0][0]) + Convert.ToDecimal(Cash.StrCBMoney);
            else
                Rmoney = Convert.ToDecimal(Cash.StrCBMoney);

            if (Rmoney == Amount)
                strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '2' where PID = '" + Cash.StrCID + "'";
            else
                strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '1' where PID = '" + Cash.StrCID + "'";


            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','项目回款操作','回款成功','" + DateTime.Now + "','" + account.UserName + "','项目回款')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
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

            return intInsert + updatebas;
        }

        public static UIDataTable getProjectCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getProjectCashBack", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static CCashBack getUpdateCashBack(string id)
        {
            CCashBack Cash = new CCashBack();
            string str = "select * from tk_ProjectCashBack where CBID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            if (dt.Rows.Count > 0)
            {
                Cash.StrCBID = id;
                Cash.StrCID = dt.Rows[0]["CID"].ToString();
                Cash.StrCurAmountNum = Convert.ToInt16(dt.Rows[0]["CurAmountNum"]);
                Cash.StrCBMethod = dt.Rows[0]["CBMethod"].ToString();
                Cash.StrCBMoney = Convert.ToDecimal(dt.Rows[0]["CBMoney"]);
                Cash.StrReceiptMoney = Convert.ToDecimal(dt.Rows[0]["ReceiptMoney"]);
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

        public static bool checkUpdateMoney(string CID, string Money,string CBID)
        {
            string str = "select ContractAmount from tk_ProjectBas where PID = '" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainProject");
            decimal Amount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);
            string strS = "select sum(CBMoney) from tk_ProjectCashBack where  CID = '" + CID + "' and CBID != '"+CBID+"' and ValiDate = 'v'";
            DataTable dts = SQLBase.FillTable(strS, "MainProject");
            decimal Rmoney = 0;
            if (dts.Rows[0][0].ToString() != "")
                Rmoney = Convert.ToDecimal(dts.Rows[0][0]) + Convert.ToDecimal(Money);
            else
                Rmoney = Convert.ToDecimal(Money);
            if (Rmoney <= Amount)
                return true;
            else
                return false;
        }

        public static int UpdateProjectCCashBack(CCashBack Cash, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            int updatebas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_ProjectCashBack set CurAmountNum = '" + Cash.StrCurAmountNum + "',CBMethod = '" + Cash.StrCBMethod + "',CBMoney = '" + Cash.StrCBMoney + "',ReceiptMoney = '"+Cash.StrReceiptMoney+"',CBBillNo = '" + Cash.StrCBBillNo + "',ReceiptNo = '" + Cash.StrReceiptNo + "',IsReturn = '" + Cash.StrIsReturn + "',"
                + "NoReturnReason = '" + Cash.StrNoReturnReason + "',PayCompany = '" + Cash.StrPayCompany + "',Remark = '" + Cash.StrRemark + "',CBDate = '" + Cash.StrCBDate + "' where CBID = '" + Cash.StrCBID + "'";
            string strUpdateBas = "";

            string str = "select ContractAmount from tk_ProjectBas where PID = '" + Cash.StrCID + "'";
            DataTable dt = sqlTrans.FillTable(str);
            decimal Amount = Convert.ToDecimal(dt.Rows[0]["ContractAmount"]);

            string strS = "select sum(CBMoney) from tk_ProjectCashBack where  CID = '" + Cash.StrCID + "' and CBID != '" + Cash.StrCBID + "' and ValiDate = 'v'";
            DataTable dts = sqlTrans.FillTable(strS);
            decimal Rmoney = 0;
            if (dts.Rows[0][0].ToString() != "")
                Rmoney = Convert.ToDecimal(dts.Rows[0][0]) + Convert.ToDecimal(Cash.StrCBMoney);
            else
                Rmoney = Convert.ToDecimal(Cash.StrCBMoney);

            if (Rmoney == Amount)
                strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '2' where PID = '" + Cash.StrCID + "'";
            else
                strUpdateBas = "update [BGOI_Project ]..tk_ProjectBas set IsCBack = '1' where PID = '" + Cash.StrCID + "'";


            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','修改项目回款操作','修改项目回款成功','" + DateTime.Now + "','" + account.UserName + "','项目回款')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
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

            return intInsert + updatebas;
        }

        public static int dellCCashBack(string cbid, string cid, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string strInsert = "update tk_ProjectCashBack set Validate = 'i' where CBID = '" + cbid + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + cid + "','撤销项目回款操作','撤销成功','" + DateTime.Now + "','" + account.UserName + "','项目回款')";
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
    }
}
