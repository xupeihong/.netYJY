using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
namespace TECOCITY_BGOI
{
    public class DecetePro
    {
        public static string GetShowTaskID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select TID, TidNo from tk_TIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_TIDno (TID,TidNo,DateRecord) values('J',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["TidNo"]);
            }

            intNewID++;
            string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strPID = dtPMaxID.Rows[0]["TID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 3);
            return strPID;
        }

        public static string GetTaskID()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select TID, TidNo from tk_TIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_TIDno (TID,TidNo,DateRecord) values('J',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["TidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_TIDno set TidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainKAT");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strPID = dtPMaxID.Rows[0]["TID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 3);
            return strPID;
        }

        public static DataTable GetConfigCont(string Type)
        {
            string strSql = "select SID,Text from tk_ConfigContent where validate='v' and Type = '" + Type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetConfigPipe()
        {
            string strSql = "select PipeID,Text from tk_ConfigRTPipe where validate='v' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetProductSpec()
        {
            string strSql = "select PipeID,Text from tk_ConfigRTPipe where validate='v' and ID >='15' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetConfigRTSize()
        {
            string strSql = "select SizeID,Text from tk_ConfigRTSize where validate='v' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetConfigTZPly()
        {
            string strSql = "select PlyID,Text from tk_ConfigRTPly where validate='v' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetConfigContRative()
        {
            string strSql = "select ID,RID from tk_RativeSource";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static DataTable GetConfigUser()
        {
            string strSql = "select UserId,UserName from UM_UserNew where DeptId = '29' and roleNames like '%检测员%'";
            DataTable dt = SQLBase.FillTable(strSql, "AccountCnn");
            return dt;
        }

        public static DataTable GetConfigUnit(string Type)
        {
            string strSql = "select UnitID,Text from tk_ConfigUnit where validate='v' and Type = '" + Type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetDeceteUnit()
        {
            string strSql = "select UnitCode,UnitName from UM_ConfigUnit";
            DataTable dt = SQLBase.FillTable(strSql, "AccountCnn");
            return dt;
        }

        public static DataTable GetUser(string unitid)
        {
            string strSql = "select ID,Name from tk_ConfigEntrustUser where UnitID = '" + unitid + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetPhone(string uid)
        {
            string strSql = "select Phone from tk_ConfigEntrustUser where ID = '" + uid + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static int InsertEntrustTask(EntrustTask Task, string Press, string PipSize, string length, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string[] arrPress = Press.Split(',');
            string[] arrPip = PipSize.Split(',');
            string[] arrLength = length.Split(',');
            string strInsertCont = "";
            string strInsert = GSqlSentence.GetInsertInfoByD<EntrustTask>(Task, "tk_EntrustTask");
            for (int i = 0; i < arrPress.Length; i++)
            {
                strInsertCont += "insert into tk_EntrustTaskCont (TaskID,Press,PipeSize,Length) values ('" + Task.StrTaskID + "','" + arrPress[i] + "','" + arrPip[i] + "','" + arrLength[i] + "')";
            }

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
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

        public static int InsertFinishEntrustTask(EntrustTask Task, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_EntrustTask set FinishTime = '" + Task.StrFinishTime + "',FinishUser = '" + Task.StrFinishUser + "',State = '2' where TaskID = '" + Task.StrTaskID + "'";

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

        public static EntrustTask getUpdateEntrustTask(string id)
        {
            EntrustTask Task = new EntrustTask();
            string strSql = "select * from tk_EntrustTask where TaskID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                Task.StrTaskID = id;
                Task.StrProjectNum = dt.Rows[0]["ProjectNum"].ToString();
                Task.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                Task.StrTaskPlace = dt.Rows[0]["TaskPlace"].ToString();
                Task.StrDrawingNum = dt.Rows[0]["DrawingNum"].ToString();
                Task.StrStartAEnd = dt.Rows[0]["StartAEnd"].ToString();
                Task.StrTaskDesc = dt.Rows[0]["TaskDesc"].ToString();
                Task.StrTellsbTime = Convert.ToDateTime(dt.Rows[0]["TellsbTime"]).ToString("yyyy-MM-dd HH:ss");
                Task.StrTellsbPlace = dt.Rows[0]["TellsbPlace"].ToString();
                Task.StrJoinBuidUnit = dt.Rows[0]["JoinBuidUnit"].ToString();
                Task.StrJoinAboutUnit = dt.Rows[0]["JoinAboutUnit"].ToString();
                Task.StrBuildUnit = dt.Rows[0]["BuildUnit"].ToString();
                Task.StrBuildPerson = dt.Rows[0]["BuildPerson"].ToString();
                Task.StrBuildPhone = dt.Rows[0]["BuildPhone"].ToString();
                Task.StrDsignUnit = dt.Rows[0]["DsignUnit"].ToString();
                Task.StrDsignPerson = dt.Rows[0]["DsignPerson"].ToString();
                Task.StrDsignPhone = dt.Rows[0]["DsignPhone"].ToString();
                Task.StrWorkUnit = dt.Rows[0]["WorkUnit"].ToString();
                Task.StrWorkPerson = dt.Rows[0]["WorkPerson"].ToString();
                Task.StrWorkPhone = dt.Rows[0]["WorkPhone"].ToString();
                Task.StrVisorUnit = dt.Rows[0]["VisorUnit"].ToString();
                Task.StrVisorPerson = dt.Rows[0]["VisorPerson"].ToString();
                Task.StrVisorPhone = dt.Rows[0]["VisorPhone"].ToString();
                Task.StrWatchUnit = dt.Rows[0]["WatchUnit"].ToString();
                Task.StrWatchPerson = dt.Rows[0]["WatchPerson"].ToString();
                Task.StrWatchPhone = dt.Rows[0]["WatchPhone"].ToString();
                Task.StrProtectUnit = dt.Rows[0]["ProtectUnit"].ToString();
                Task.StrProtectPerson = dt.Rows[0]["ProtectPerson"].ToString();
                Task.StrProtectPhone = dt.Rows[0]["ProtectPhone"].ToString();
                Task.StrEleVisitUnit = dt.Rows[0]["EleVisitUnit"].ToString();
                Task.StrEleVisitPerson = dt.Rows[0]["EleVisitPerson"].ToString();
                Task.StrEleVisitPhone = dt.Rows[0]["EleVisitPhone"].ToString();
                Task.StrMeasureUnit = dt.Rows[0]["MeasureUnit"].ToString();
                Task.StrMeasurePerson = dt.Rows[0]["MeasurePerson"].ToString();
                Task.StrMeasurePhone = dt.Rows[0]["MeasurePhone"].ToString();
                Task.StrDescT = dt.Rows[0]["DescT"].ToString();
                Task.StrPlanUser = dt.Rows[0]["PlanUser"].ToString();
                Task.StrPlanUserTel = dt.Rows[0]["PlanUserTel"].ToString();
                Task.StrMinster = dt.Rows[0]["Minster"].ToString();
                Task.StrCreateUnit = dt.Rows[0]["CreateUnit"].ToString();
            }
            return Task;
        }

        public static int UpdateEntrustTask(EntrustTask Task, string Press, string PipSize, string length, ref string a_strErr)
        {
            int intdelete = 0;
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string[] arrPress = Press.Split(',');
            string[] arrPip = PipSize.Split(',');
            string[] arrLength = length.Split(',');
            string strDelete = "delete from tk_EntrustTaskCont where TaskID = '" + Task.StrTaskID + "'";
            string strInsertCont = "";
            string strInsert = "update tk_EntrustTask set ProjectNum = '" + Task.StrProjectNum + "',TaskName = '" + Task.StrTaskName + "',TaskPlace = '" + Task.StrTaskPlace + "',DrawingNum = '" + Task.StrDrawingNum + "',StartAEnd = '" + Task.StrStartAEnd + "',"
                + "TaskDesc = '" + Task.StrTaskDesc + "',TellsbTime = '" + Task.StrTellsbTime + "',TellsbPlace = '" + Task.StrTellsbPlace + "',JoinBuidUnit = '" + Task.StrJoinBuidUnit + "',JoinAboutUnit = '" + Task.StrJoinAboutUnit + "',BuildUnit = '" + Task.StrBuildUnit + "',"
                + "BuildPerson = '" + Task.StrBuildPerson + "',BuildPhone = '" + Task.StrBuildPhone + "',DsignUnit = '" + Task.StrDsignUnit + "',DsignPerson = '" + Task.StrDsignPerson + "',DsignPhone = '" + Task.StrDsignPhone + "',WorkUnit = '" + Task.StrWorkUnit + "',"
                + "WorkPerson = '" + Task.StrWorkPerson + "',WorkPhone = '" + Task.StrWorkPhone + "',VisorUnit = '" + Task.StrVisorUnit + "',VisorPerson = '" + Task.StrVisorPerson + "',VisorPhone = '" + Task.StrVisorPhone + "',WatchUnit = '" + Task.StrWatchUnit + "',"
                + "WatchPerson = '" + Task.StrWatchPerson + "',WatchPhone = '" + Task.StrWatchPhone + "',ProtectUnit = '" + Task.StrProtectUnit + "',ProtectPerson = '" + Task.StrProtectPerson + "',ProtectPhone = '" + Task.StrProtectPhone + "',EleVisitUnit = '" + Task.StrEleVisitUnit + "',"
                + "EleVisitPerson = '" + Task.StrEleVisitPerson + "',EleVisitPhone = '" + Task.StrEleVisitPhone + "',MeasureUnit = '" + Task.StrMeasureUnit + "',MeasurePerson = '" + Task.StrMeasurePerson + "',MeasurePhone = '" + Task.StrMeasurePhone + "',DescT = '" + Task.StrDescT + "',"
                + "PlanUser = '" + Task.StrPlanUser + "',PlanUserTel = '" + Task.StrPlanUserTel + "',Minster = '" + Task.StrMinster + "',CreateUnit = '" + Task.StrCreateUnit + "' where TaskID = '" + Task.StrTaskID + "'";
            for (int i = 0; i < arrPress.Length; i++)
            {
                strInsertCont += "insert into tk_EntrustTaskCont (TaskID,Press,PipeSize,Length) values ('" + Task.StrTaskID + "','" + arrPress[i] + "','" + arrPip[i] + "','" + arrLength[i] + "')";
            }

            try
            {
                if (strDelete != "")
                    intdelete = sqlTrans.ExecuteNonQuery(strDelete, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intdelete;
        }

        public static DataTable getJudgeDecete(string TaskID)
        {
            string strSql = "select * from tk_TaskDecete where TaskID = '" + TaskID + "' and State >= '0'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable getJudgeFinishDecete(string TaskID)
        {
            string strSql = "select * from tk_TaskDecete where TaskID = '" + TaskID + "' and (State = '0' or State = '1')";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable getJudgeTask(string Decete)
        {
            string strSql = "select * from tk_Task where DetectID = '" + Decete + "' and State >= '0'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable getJudgeFinishTask(string Decete)
        {
            string strSql = "select * from tk_Task where DetectID = '" + Decete + "' and State = '0'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static DataTable GetDetailDecete(string DeceteID)
        {
            string where = " and a.DetectID = '" + DeceteID + "'";
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailDecete", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static DataTable getDetailTask(string TaskNumber)
        {
            string where = " and a.TaskNumber = '" + TaskNumber + "'";
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getDetailTask", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static int deleteEntrustTask(string TaskID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_EntrustTask set State = '-1' where TaskID = '" + TaskID + "'";
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

        public static int deleteDecete(string DeceteID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_TaskDecete set State = '-1' where DetectID = '" + DeceteID + "'";
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

        public static DataTable getEntrustContent(string id)
        {
            string strSql = "select * from tk_EntrustTaskCont where TaskID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static UIDataTable getEntrustGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getEntrust", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static DataTable GetPrintEntrustByTaskID(string TaskID)
        {
            string where = " and a.TaskID = '" + TaskID + "'";
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintEntrust", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static DataTable GetPrinEntrustContent(string TaskID)
        {
            string str = "select a.Press,b.Text as PipeSize,a.Length from tk_EntrustTaskCont a left join tk_ConfigContent b on a.PipeSize = b.SID and b.Type = 'PipeSize' where TaskID = '" + TaskID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainKAT");
            return dt;
        }

        public static string GetShowDeceteID(string type)
        {
            string strDeceteID = "";
            string strYMD = DateTime.Now.ToString("yyyy");

            string strSelPID = "select * from tk_Deceteno where Year='" + strYMD + "' and Type = '" + type + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strDeceteID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Deceteno (Year,Type,Number) values('" + strYMD + "','" + type + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string str = "select * from tk_Deceteno where Year='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strDeceteID = dtPMaxID.Rows[0]["Year"].ToString() + dtPMaxID.Rows[0]["Type"].ToString() + GFun.GetNum(intNewID, 3);
            return strDeceteID;
        }

        public static string GetDeceteID(string type)
        {
            string strDeceteID = "";
            string strYMD = DateTime.Now.ToString("yyyy");

            string strSelPID = "select * from tk_Deceteno where Year='" + strYMD + "' and Type = '" + type + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strDeceteID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Deceteno (Year,Type,Number) values('" + strYMD + "','" + type + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string strUpdateID = "update tk_Deceteno set Number='" + intNewID + "' where Year ='" + strYMD + "' and Type = '" + type + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainKAT");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            strDeceteID = dtPMaxID.Rows[0]["Year"].ToString() + dtPMaxID.Rows[0]["Type"].ToString() + GFun.GetNum(intNewID, 3);
            return strDeceteID;
        }

        public static int InsertDecete(Decete Dec, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<Decete>(Dec, "tk_TaskDecete");
            string strInsertCont = "update tk_EntrustTask set State = '1' where TaskID = '" + Dec.StrTaskID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int InsertFinishDecete(Decete Dec, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_TaskDecete set FinishTime = '" + Dec.StrFinishTime + "',FinishUser = '" + Dec.StrFinishUser + "',State = '2' where DetectID = '" + Dec.StrDetectID + "'";

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

        public static UIDataTable getDeceteGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getDecete", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static Decete getUpdateDecete(string DeceteID)
        {
            Decete Dec = new Decete();
            string str = "select * from tk_TaskDecete where DetectID = '" + DeceteID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                Dec.StrTaskID = dt.Rows[0]["TaskID"].ToString();
                Dec.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                Dec.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                Dec.StrDetectPlace = dt.Rows[0]["DetectPlace"].ToString();
                Dec.StrEntrustUnit = dt.Rows[0]["EntrustUnit"].ToString();
                Dec.StrLivePerson = dt.Rows[0]["LivePerson"].ToString();
                Dec.StrLivePhone = dt.Rows[0]["LivePhone"].ToString();
                Dec.StrPieceCont = dt.Rows[0]["PieceCont"].ToString();
                Dec.StrPieceTexture = dt.Rows[0]["PieceTexture"].ToString();
                Dec.StrExecutive = dt.Rows[0]["Executive"].ToString();
                Dec.StrTechnicalgrade = dt.Rows[0]["Technicalgrade"].ToString();
                Dec.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                Dec.StrDetectScale = dt.Rows[0]["DetectScale"].ToString();
                Dec.StrDetectPart = dt.Rows[0]["DetectPart"].ToString();
                Dec.StrWorkAmount = dt.Rows[0]["WorkAmount"].ToString();
                Dec.StrDeceteType = dt.Rows[0]["DeceteType"].ToString();
            }
            return Dec;
        }

        public static int UpdateDecete(Decete Dec, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_TaskDecete set EntrustUnit = '" + Dec.StrEntrustUnit + "',LivePerson = '" + Dec.StrLivePerson + "',LivePhone = '" + Dec.StrLivePhone + "',"
                + "PieceCont = '" + Dec.StrPieceCont + "',PieceTexture = '" + Dec.StrPieceTexture + "',Executive = '" + Dec.StrExecutive + "',Technicalgrade = '" + Dec.StrTechnicalgrade + "',HegeLevel = '" + Dec.StrHegeLevel + "',"
                + "DetectScale = '" + Dec.StrDetectScale + "',DetectPart = '" + Dec.StrDetectPart + "',WorkAmount = '" + Dec.StrWorkAmount + "' where DetectID = '" + Dec.StrDetectID + "'";

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

        public static string GetShowTaskNumber(string DeceteID)
        {
            string TaskNumber = "";

            string strSelPID = "select * from tk_Taskno where DeceteID='" + DeceteID + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return TaskNumber;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Taskno (DeceteID,Number) values('" + DeceteID + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string str = "select * from tk_Taskno where DeceteID='" + DeceteID + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            TaskNumber = dtPMaxID.Rows[0]["DeceteID"].ToString() + "-" + GFun.GetNum(intNewID, 2);
            return TaskNumber;
        }

        public static string GetTaskNumber(string DeceteID)
        {
            string TaskNumber = "";

            string strSelPID = "select * from tk_Taskno where DeceteID='" + DeceteID + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return TaskNumber;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Taskno (DeceteID,Number) values('" + DeceteID + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string strUpdateID = "update tk_Taskno set Number='" + intNewID + "' where DeceteID='" + DeceteID + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainKAT");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            TaskNumber = dtPMaxID.Rows[0]["DeceteID"].ToString() + "-" + GFun.GetNum(intNewID, 2); ;
            return TaskNumber;
        }

        public static int InsertTask(Task task, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<Task>(task, "tk_Task");
            string strUpdate = "update tk_TaskDecete set State = '1' where DetectID = '" + task.StrDetectID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
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

        public static int FinishTask(Task task, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_Task set FinishTime = '" + task.StrFinishTime + "',FinishUser = '" + task.StrFinishUser + "',state = '1' where TaskNumber = '" + task.StrTaskNumber + "'";

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

        public static int UpdateTask(Task task, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_Task set WorkAmount = '" + task.StrWorkAmount + "',DeceteTime = '" + task.StrDeceteTime + "' where TaskNumber = '" + task.StrTaskNumber + "'";

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

        public static int deleteTask(string TaskNumber, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_Task set State = '-1' where TaskNumber = '" + TaskNumber + "'";

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

        public static Task getUpdateTask(string id)
        {
            Task task = new Task();
            string strSql = "select * from tk_Task where TaskNumber = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                task.StrTaskNumber = id;
                task.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                task.StrDeceteTime = Convert.ToDateTime(dt.Rows[0]["DeceteTime"]).ToString("yyyy-MM-dd");
                task.StrWorkAmount = dt.Rows[0]["WorkAmount"].ToString();
            }
            return task;
        }

        public static UIDataTable getTaskGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getTask", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static CardMT getHaveMT()
        {
            CardMT CMT = new CardMT();
            string strSql = "select * from tk_techlCardMTmodel";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CMT.StrDetectTime = dt.Rows[0]["DetectTime"].ToString();
                CMT.StrCiType = dt.Rows[0]["CiType"].ToString();
                CMT.StrCiConsert = dt.Rows[0]["CiConsert"].ToString();
                CMT.StrTestPiece = dt.Rows[0]["TestPiece"].ToString();
                CMT.StrBlackModel = dt.Rows[0]["BlackModel"].ToString();
                CMT.StrDeceteStand = dt.Rows[0]["DeceteStand"].ToString();
                CMT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CMT.StrDeceteMethod = dt.Rows[0]["DeceteMethod"].ToString();
                CMT.StrCiTakeType = dt.Rows[0]["CiTakeType"].ToString();
                CMT.StrCiMethod = dt.Rows[0]["CiMethod"].ToString();
                CMT.StrPowerType = dt.Rows[0]["PowerType"].ToString();
                CMT.StrSunFace = dt.Rows[0]["SunFace"].ToString();
                CMT.StrNoBad = dt.Rows[0]["NoBad"].ToString();
                CMT.StrWaskstr = dt.Rows[0]["Waskstr"].ToString();
                CMT.StrEquipaskstr = dt.Rows[0]["Equipaskstr"].ToString();
                CMT.StrCIaskstr = dt.Rows[0]["CIaskstr"].ToString();
                CMT.StrCITimeaskstr = dt.Rows[0]["CITimeaskstr"].ToString();
                CMT.StrTestTimeaskstr = dt.Rows[0]["TestTimeaskstr"].ToString();
                CMT.StrConcertaskstr = dt.Rows[0]["Concertaskstr"].ToString();
                CMT.StrMakeaskstr = dt.Rows[0]["Makeaskstr"].ToString();
                CMT.StrWatchaskstrr = dt.Rows[0]["Watchaskstr"].ToString();
                CMT.StrSetingaskstr = dt.Rows[0]["Setingaskstr"].ToString();
                CMT.StrFwatchaskstr = dt.Rows[0]["Fwatchaskstr"].ToString();
                CMT.StrReaskstr = dt.Rows[0]["Reaskstr"].ToString();
                CMT.StrPassaskstr = dt.Rows[0]["Passaskstr"].ToString();
                CMT.StrRtypeaskstr = dt.Rows[0]["Rtypeaskstr"].ToString();
                CMT.StrRcontaskstr = dt.Rows[0]["Rcontaskstr"].ToString();
                CMT.StrDelCI = dt.Rows[0]["DelCI"].ToString();
                CMT.StrBackHand = dt.Rows[0]["BackHand"].ToString();
                CMT.StrReport = dt.Rows[0]["Report"].ToString();
            }
            return CMT;
        }

        public static string GetShowCardNumber(string DeceteID)
        {
            string TaskNumber = "";

            string strSelPID = "select * from tk_Cardno where DeceteID='" + DeceteID + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return TaskNumber;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Cardno (DeceteID,Number) values('" + DeceteID + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string str = "select * from tk_Cardno where DeceteID='" + DeceteID + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            TaskNumber = dtPMaxID.Rows[0]["DeceteID"].ToString() + "-" + GFun.GetNum(intNewID, 2);
            return TaskNumber;
        }

        public static string GetCardNumber(string DeceteID)
        {
            string TaskNumber = "";

            string strSelPID = "select * from tk_Cardno where DeceteID='" + DeceteID + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return TaskNumber;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_Cardno (DeceteID,Number) values('" + DeceteID + "',0)";
                SQLBase.ExecuteNonQuery(strInsertID, "MainKAT");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["Number"]);
            }

            intNewID++;
            string strUpdateID = "update tk_Cardno set Number='" + intNewID + "' where DeceteID='" + DeceteID + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "MainKAT");

            //string str = "select TID, TidNo,DateRecord from tk_TIDno where DateRecord='" + strYMD + "'";
            //dtPMaxID = SQLBase.FillTable(strSelPID, "MainKAT");
            TaskNumber = dtPMaxID.Rows[0]["DeceteID"].ToString() + "-" + GFun.GetNum(intNewID, 2); ;
            return TaskNumber;
        }

        public static CardPT getHaveCardPT()
        {
            CardPT CPT = new CardPT();
            string strSql = "select * from tk_techlCardPTmodel";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CPT.StrDetectTime = dt.Rows[0]["DetectTime"].ToString();
                CPT.StrPieceFace = dt.Rows[0]["PieceFace"].ToString();
                CPT.StrPttype = dt.Rows[0]["Pttype"].ToString();
                CPT.StrWashJtype = dt.Rows[0]["WashJtype"].ToString();
                CPT.StrFilmType = dt.Rows[0]["FilmType"].ToString();
                CPT.StrTestPiece = dt.Rows[0]["TestPiece"].ToString();
                CPT.StrQuick = dt.Rows[0]["Quick"].ToString();
                CPT.StrDeceteMethod = dt.Rows[0]["DeceteMethod"].ToString();
                CPT.StrPtMethod = dt.Rows[0]["PtMethod"].ToString();
                CPT.StrDelMethod = dt.Rows[0]["DelMethod"].ToString();
                CPT.StrHotWD = dt.Rows[0]["HotWD"].ToString();
                CPT.StrHotTime = dt.Rows[0]["HotTime"].ToString();
                CPT.StrFilmtMethod = dt.Rows[0]["FilmtMethod"].ToString();
                CPT.StrPtTime = dt.Rows[0]["PtTime"].ToString();
                CPT.StrFilmTime = dt.Rows[0]["FilmTime"].ToString();
                CPT.StrWatchMethod = dt.Rows[0]["WatchMethod"].ToString();
                CPT.StrFaceAsk = dt.Rows[0]["FaceAsk"].ToString();
                CPT.StrDeceteStand = dt.Rows[0]["DeceteStand"].ToString();
                CPT.StrDeceteBL = dt.Rows[0]["DeceteBL"].ToString();
                CPT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CPT.StrDetectJask = dt.Rows[0]["DetectJask"].ToString();
                CPT.StrSureStand = dt.Rows[0]["SureStand"].ToString();
                CPT.StrPersonSafe = dt.Rows[0]["PersonSafe"].ToString();
                CPT.StrEquipSafe = dt.Rows[0]["EquipSafe"].ToString();
                CPT.StrFaskstr = dt.Rows[0]["Faskstr"].ToString();
                CPT.StrFdesc = dt.Rows[0]["Fdesc"].ToString();
                CPT.StrWaskstr = dt.Rows[0]["Waskstr"].ToString();
                CPT.StrWdesc = dt.Rows[0]["Wdesc"].ToString();
                CPT.StrPTaskstr = dt.Rows[0]["PTaskstr"].ToString();
                CPT.StrPTdesc = dt.Rows[0]["PTdesc"].ToString();
                CPT.StrDelaskstr = dt.Rows[0]["Delaskstr"].ToString();
                CPT.StrDeldesc = dt.Rows[0]["Deldesc"].ToString();
                CPT.StrFilaskstr = dt.Rows[0]["Filaskstr"].ToString();
                CPT.StrFildesc = dt.Rows[0]["Fildesc"].ToString();
                CPT.StrWatchaskstr = dt.Rows[0]["Watchaskstr"].ToString();
                CPT.StrWatchdesc = dt.Rows[0]["Watchdesc"].ToString();
                CPT.StrReaskstr = dt.Rows[0]["Reaskstr"].ToString();
                CPT.StrRedesc = dt.Rows[0]["Redesc"].ToString();
                CPT.StrBackaskstr = dt.Rows[0]["Backaskstr"].ToString();
                CPT.StrBackdesc = dt.Rows[0]["Backdesc"].ToString();
                CPT.StrWaitaskstr = dt.Rows[0]["Waitaskstr"].ToString();
                CPT.StrWaitdesc = dt.Rows[0]["Waitdesc"].ToString();
            }
            return CPT;
        }

        public static DataTable getPipeSize(string PipeID)
        {
            string strSql = "select SizeID,Text from tk_ConfigRTSize where PipeID = '" + PipeID + "' and validate='v' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }
        public static DataTable getPipeSize()
        {
            string strSql = "select SizeID as SID,Text from tk_ConfigRTSize where validate='v' order by SID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }
        public static DataTable getPipeSize2()
        {
            string strSql = "select PipeID as SID,Text from tk_ConfigRTPipe where validate='v' order by SID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }
        public static DataTable GetTZPly(string PipeID)
        {
            string strSql = "select PlyID,Text from tk_ConfigRTPly where PipeID like '%," + PipeID + ",%' order by ID";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            return dt;
        }

        public static string getCKmodelUT(string ProductSpec, string MumTxt)
        {
            string model = "";
            string strSql = "select * from tk_techlCardUTmodel where ProductSpec = '" + ProductSpec + "' and MumTxt = '" + MumTxt + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                model = dt.Rows[0]["EquipModel"].ToString() + "@" + dt.Rows[0]["MainTxture"].ToString() + "@" + dt.Rows[0]["TxtureNum"].ToString() + "@" + dt.Rows[0]["HjType"].ToString() + "@" + dt.Rows[0]["PokoType"].ToString()
                    + "@" + dt.Rows[0]["PieceState"].ToString() + "@" + dt.Rows[0]["PieceFace"].ToString() + "@" + dt.Rows[0]["YiqiModel"].ToString() + "@" + dt.Rows[0]["YiqiNumber"].ToString() + "@" + dt.Rows[0]["TestPart"].ToString()
                     + "@" + dt.Rows[0]["FashePart"].ToString() + "@" + dt.Rows[0]["TantType"].ToString() + "@" + dt.Rows[0]["TantModel"].ToString() + "@" + dt.Rows[0]["Jpsize"].ToString() + "@" + dt.Rows[0]["Kval"].ToString()
                      + "@" + dt.Rows[0]["TantHead"].ToString() + "@" + dt.Rows[0]["OuhType"].ToString() + "@" + dt.Rows[0]["DeceteStand"].ToString() + "@" + dt.Rows[0]["TechlLevel"].ToString() + "@" + dt.Rows[0]["HegeLevel"].ToString()
                       + "@" + dt.Rows[0]["DecetePart"].ToString() + "@" + dt.Rows[0]["DeceteLength"].ToString() + "@" + dt.Rows[0]["DeceteBL"].ToString() + "@" + dt.Rows[0]["DeceteType"].ToString() + "@" + dt.Rows[0]["DeceteFace"].ToString()
                        + "@" + dt.Rows[0]["SaoPart"].ToString() + "@" + dt.Rows[0]["SaoType"].ToString() + "@" + dt.Rows[0]["SaoSpeed"].ToString() + "@" + dt.Rows[0]["SaoConver"].ToString() + "@" + dt.Rows[0]["OuType"].ToString()
                        + "@" + dt.Rows[0]["TantRange"].ToString() + "@" + dt.Rows[0]["FaceBC"].ToString() + "@" + dt.Rows[0]["Scan"].ToString() + "@" + dt.Rows[0]["ScanLM"].ToString() + "@" + dt.Rows[0]["Lmass"].ToString()
                         + "@" + dt.Rows[0]["EL"].ToString() + "@" + dt.Rows[0]["SL"].ToString() + "@" + dt.Rows[0]["RL"].ToString() + "@" + dt.Rows[0]["TechlAsk"].ToString() + "@" + dt.Rows[0]["CardDesc"].ToString();
            }
            return model;
        }

        public static string getCKmodelRT(string StrPipeStand, string StrStandSize, string StrRttype, string StrTZType, string StrTZPly)
        {
            string model = "";
            string strSql = "select * from tk_techlCardRTmodel where PipeStand = '" + StrPipeStand + "' and Rttype = '" + StrRttype + "' and TZType = '" + StrTZType + "' and TZPly = '" + StrTZPly + "' and StandSize = '" + StrStandSize + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                model = dt.Rows[0]["PressType"].ToString() + "@" + dt.Rows[0]["HotState"].ToString() + "@" + dt.Rows[0]["PieceState"].ToString() + "@" + dt.Rows[0]["MainTxture"].ToString() + "@" + dt.Rows[0]["TxtureNum"].ToString()
                    + "@" + dt.Rows[0]["PokoType"].ToString() + "@" + dt.Rows[0]["HjType"].ToString() + "@" + dt.Rows[0]["YiqiModel"].ToString() + "@" + dt.Rows[0]["FocusSize"].ToString() + "@" + dt.Rows[0]["XZJModel"].ToString()
                     + "@" + dt.Rows[0]["Pb"].ToString() + "@" + dt.Rows[0]["BPB"].ToString() + "@" + dt.Rows[0]["FilmType"].ToString() + "@" + dt.Rows[0]["FilmSpec"].ToString() + "@" + dt.Rows[0]["FilmPF"].ToString()
                      + "@" + dt.Rows[0]["WashType"].ToString() + "@" + dt.Rows[0]["WashEquipModel"].ToString() + "@" + dt.Rows[0]["ExecuteStand"].ToString() + "@" + dt.Rows[0]["TechlLevelRT"].ToString() + "@" + dt.Rows[0]["HegeLevel"].ToString()
                       + "@" + dt.Rows[0]["DeceteBL"].ToString() + "@" + dt.Rows[0]["Hflength"].ToString() + "@" + dt.Rows[0]["DetectTime"].ToString() + "@" + dt.Rows[0]["DecetePart"].ToString() + "@" + dt.Rows[0]["BlackRange"].ToString()
                        + "@" + dt.Rows[0]["SiNum"].ToString() + "@" + dt.Rows[0]["Kv"].ToString() + "@" + dt.Rows[0]["Ci"].ToString() + "@" + dt.Rows[0]["mA"].ToString() + "@" + dt.Rows[0]["CiTime"].ToString()
                        + "@" + dt.Rows[0]["Cimin"].ToString() + "@" + dt.Rows[0]["FocusLength"].ToString() + "@" + dt.Rows[0]["OneTimelength"].ToString() + "@" + dt.Rows[0]["Delength"].ToString() + "@" + dt.Rows[0]["TechlAsk"].ToString()
                         + "@" + dt.Rows[0]["CardDesc"].ToString();
            }
            return model;
        }

        public static int InsertTechlCardRTmodel(CardRT CRT, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            string strInsert = "";
            sqlTrans.Open("MainKAT");
            string str = "select * from tk_techlCardRTmodel where PipeStand = '" + CRT.StrPipeStand + "' and Rttype = '" + CRT.StrRttype + "' and TZType = '" + CRT.StrTZType + "' and TZPly = '" + CRT.StrTZPly + "' and StandSize = '" + CRT.StrStandSize + "'";
            DataTable dt = SQLBase.FillTable(str, "MainKAT");
            if (dt.Rows.Count == 0)
            {
                strInsert = "insert into tk_techlCardRTmodel (PipeStand,Rttype,TZType,TZPly,StandSize,PressType,HotState,PieceState,MainTxture,TxtureNum,PokoType,"
                + "HjType,YiqiModel,FocusSize,XZJModel,Pb,BPB,FilmType,FilmSpec,FilmPF,WashType,WashEquipModel,ExecuteStand,TechlLevelRT,HegeLevel,DeceteBL,Hflength,DetectTime,"
                + "DecetePart,BlackRange,SiNum,Kv,Ci,mA,CiTime,Cimin,FocusLength,OneTimelength,Delength,TechlAsk,CardDesc"
                + ") values ('" + CRT.StrPipeStand + "','" + CRT.StrRttype + "','" + CRT.StrTZType + "','" + CRT.StrTZPly + "','" + CRT.StrStandSize + "',"
                + "'" + CRT.StrPressType + "','" + CRT.StrHotState + "','" + CRT.StrPieceState + "','" + CRT.StrMainTxture + "','" + CRT.StrTxtureNum + "','" + CRT.StrPokoType + "',"
                + "'" + CRT.StrHjType + "','" + CRT.StrYiqiModel + "','" + CRT.StrFocusSize + "','" + CRT.StrXZJModel + "','" + CRT.StrPb + "','" + CRT.StrBPB + "',"
                + "'" + CRT.StrFilmType + "','" + CRT.StrFilmSpec + "','" + CRT.StrFilmPF + "','" + CRT.StrWashType + "','" + CRT.StrWashEquipModel + "','" + CRT.StrExecuteStand + "',"
                + "'" + CRT.StrTechlLevelRT + "','" + CRT.StrHegeLevel + "','" + CRT.StrDeceteBL + "','" + CRT.StrHflength + "','" + CRT.StrDetectTime + "','" + CRT.StrDecetePart + "',"
                + "'" + CRT.StrBlackRange + "','" + CRT.StrSiNum + "','" + CRT.StrKv + "','" + CRT.StrCi + "','" + CRT.StrmA + "','" + CRT.StrCiTime + "',"
                + "'" + CRT.StrCimin + "','" + CRT.StrFocusLength + "','" + CRT.StrOneTimelength + "','" + CRT.StrDelength + "','" + CRT.StTechlAsk + "','" + CRT.StCardDesc + "')";
            }
            else
            {
                strInsert = "update tk_techlCardRTmodel set PressType = '" + CRT.StrPressType + "',HotState = '" + CRT.StrHotState + "',PieceState = '" + CRT.StrPieceState + "',MainTxture = '" + CRT.StrMainTxture + "'"
                + ",TxtureNum = '" + CRT.StrTxtureNum + "',PokoType = '" + CRT.StrPokoType + "',HjType = '" + CRT.StrHjType + "',YiqiModel = '" + CRT.StrYiqiModel + "',FocusSize = '" + CRT.StrFocusSize + "'"
                + ",XZJModel = '" + CRT.StrXZJModel + "',Pb = '" + CRT.StrPb + "',BPB = '" + CRT.StrBPB + "',FilmType = '" + CRT.StrFilmType + "',FilmSpec = '" + CRT.StrFilmSpec + "',FilmPF = '" + CRT.StrFilmPF + "'"
                + ",WashType = '" + CRT.StrWashType + "',WashEquipModel = '" + CRT.StrWashEquipModel + "',ExecuteStand = '" + CRT.StrExecuteStand + "',TechlLevelRT = '" + CRT.StrTechlLevelRT + "',HegeLevel = '" + CRT.StrHegeLevel + "'"
                + ",DeceteBL = '" + CRT.StrDeceteBL + "',Hflength = '" + CRT.StrHflength + "',DetectTime = '" + CRT.StrDetectTime + "',DecetePart = '" + CRT.StrDecetePart + "',BlackRange = '" + CRT.StrBlackRange + "'"
                + ",SiNum = '" + CRT.StrSiNum + "',Kv = '" + CRT.StrKv + "',Ci = '" + CRT.StrCi + "',mA = '" + CRT.StrmA + "',CiTime = '" + CRT.StrCiTime + "',Cimin = '" + CRT.StrCimin + "',FocusLength = '" + CRT.StrFocusLength + "'"
                + ",OneTimelength = '" + CRT.StrOneTimelength + "',Delength = '" + CRT.StrDelength + "',TechlAsk = '" + CRT.StTechlAsk + "',CardDesc = '" + CRT.StCardDesc + "'"
                + " where PipeStand = '" + CRT.StrPipeStand + "' and Rttype = '" + CRT.StrRttype + "' and TZType = '" + CRT.StrTZType + "' and TZPly = '" + CRT.StrTZPly + "' and StandSize = '" + CRT.StrStandSize + "'";
            }
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

        public static int InsertTechlCardRT(CardRT CardRT, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<CardRT>(CardRT, "tk_techlCardRT");
            string strInsertCont = "update tk_TaskDecete set IsCard = '1' where DetectID = '" + CardRT.StrDetectID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int UpdateTechlCardRT(CardRT CRT, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardRT set PipeStand = '" + CRT.StrPipeStand + "',Rttype = '" + CRT.StrRttype + "',TZType = '" + CRT.StrTZType + "',TZPly = '" + CRT.StrTZPly + "',StandSize = '" + CRT.StrStandSize + "',"
            + "PieceName = '" + CRT.StrPieceName + "',ProductNum = '" + CRT.StrProductNum + "',PressType = '" + CRT.StrPressType + "',HotState = '" + CRT.StrHotState + "',PieceState = '" + CRT.StrPieceState + "',MainTxture = '" + CRT.StrMainTxture + "',"
            + "TxtureNum = '" + CRT.StrTxtureNum + "',PokoType = '" + CRT.StrPokoType + "',HjType = '" + CRT.StrHjType + "',YiqiModel = '" + CRT.StrYiqiModel + "',FocusSize = '" + CRT.StrFocusSize + "',XZJModel = '" + CRT.StrXZJModel + "',"
            + "Pb = '" + CRT.StrPb + "',BPB = '" + CRT.StrBPB + "',FilmType = '" + CRT.StrFilmType + "',FilmSpec = '" + CRT.StrFilmSpec + "',FilmPF = '" + CRT.StrFilmPF + "',WashType = '" + CRT.StrWashType + "',WashEquipModel = '" + CRT.StrWashEquipModel + "',"
            + "ExecuteStand = '" + CRT.StrExecuteStand + "',TechlLevelRT = '" + CRT.StrTechlLevelRT + "',HegeLevel = '" + CRT.StrHegeLevel + "',DeceteBL = '" + CRT.StrDeceteBL + "',Hflength ='" + CRT.StrHflength + "',DetectTime = '" + CRT.StrDetectTime + "',"
            + "DecetePart = '" + CRT.StrDecetePart + "',BlackRange = '" + CRT.StrBlackRange + "',SiNum = '" + CRT.StrSiNum + "',Kv = '" + CRT.StrKv + "',Ci = '" + CRT.StrCi + "',mA = '" + CRT.StrmA + "',"
            + "CiTime = '" + CRT.StrCiTime + "',Cimin = '" + CRT.StrCimin + "',FocusLength = '" + CRT.StrFocusLength + "',OneTimelength = '" + CRT.StrOneTimelength + "',Delength = '" + CRT.StrDelength + "',TechlAsk = '" + CRT.StTechlAsk + "',CardDesc = '" + CRT.StCardDesc + "' where CardID = '" + CRT.StrCardID + "'";

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

        public static int DeleteTechlCardRT(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardRT set Validate = 'i' where CardID = '" + id + "'";

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

        public static int DeleteTechlCardUT(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardUT set Validate = 'i' where CardID = '" + id + "'";

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

        public static int DeleteTechlCardPT(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardPT set Validate = 'i' where CardID = '" + id + "'";

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

        public static CardPT getUpdateCardPT(string id)
        {
            CardPT CPT = new CardPT();
            string strSql = "select * from tk_techlCardPT where CardID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CPT.StrCardID = id;
                CPT.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                CPT.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                CPT.StrPieceName = dt.Rows[0]["PieceName"].ToString();
                CPT.StrTxtNum = dt.Rows[0]["TxtNum"].ToString();
                CPT.StrPieceSpec = dt.Rows[0]["PieceSpec"].ToString();
                CPT.StrHjType = dt.Rows[0]["HjType"].ToString();
                CPT.StrPokoType = dt.Rows[0]["PokoType"].ToString();
                CPT.StrDetectTime = dt.Rows[0]["DetectTime"].ToString();
                CPT.StrPieceFace = dt.Rows[0]["PieceFace"].ToString();
                CPT.StrDecetePart = dt.Rows[0]["DecetePart"].ToString();
                CPT.StrDeceteWD = dt.Rows[0]["DeceteWD"].ToString();
                CPT.StrPttype = dt.Rows[0]["Pttype"].ToString();
                CPT.StrWashJtype = dt.Rows[0]["WashJtype"].ToString();
                CPT.StrFilmType = dt.Rows[0]["FilmType"].ToString();
                CPT.StrTestPiece = dt.Rows[0]["TestPiece"].ToString();
                CPT.StrQuick = dt.Rows[0]["Quick"].ToString();
                CPT.StrDeceteMethod = dt.Rows[0]["DeceteMethod"].ToString();
                CPT.StrPtMethod = dt.Rows[0]["PtMethod"].ToString();
                CPT.StrDelMethod = dt.Rows[0]["DelMethod"].ToString();
                CPT.StrHotWD = dt.Rows[0]["HotWD"].ToString();
                CPT.StrHotTime = dt.Rows[0]["HotTime"].ToString();
                CPT.StrFilmtMethod = dt.Rows[0]["FilmtMethod"].ToString();
                CPT.StrPtTime = dt.Rows[0]["PtTime"].ToString();
                CPT.StrFilmTime = dt.Rows[0]["FilmTime"].ToString();
                CPT.StrWatchMethod = dt.Rows[0]["WatchMethod"].ToString();
                CPT.StrFaceAsk = dt.Rows[0]["FaceAsk"].ToString();
                CPT.StrDeceteStand = dt.Rows[0]["DeceteStand"].ToString();
                CPT.StrDeceteBL = dt.Rows[0]["DeceteBL"].ToString();
                CPT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CPT.StrDetectJask = dt.Rows[0]["DetectJask"].ToString();
                CPT.StrSureStand = dt.Rows[0]["SureStand"].ToString();
                CPT.StrPersonSafe = dt.Rows[0]["PersonSafe"].ToString();
                CPT.StrEquipSafe = dt.Rows[0]["EquipSafe"].ToString();
                CPT.StrFaskstr = dt.Rows[0]["Faskstr"].ToString();
                CPT.StrFdesc = dt.Rows[0]["Fdesc"].ToString();
                CPT.StrWaskstr = dt.Rows[0]["Waskstr"].ToString();
                CPT.StrWdesc = dt.Rows[0]["Wdesc"].ToString();
                CPT.StrPTaskstr = dt.Rows[0]["PTaskstr"].ToString();
                CPT.StrPTdesc = dt.Rows[0]["PTdesc"].ToString();
                CPT.StrDelaskstr = dt.Rows[0]["Delaskstr"].ToString();
                CPT.StrDeldesc = dt.Rows[0]["Deldesc"].ToString();
                CPT.StrFilaskstr = dt.Rows[0]["Filaskstr"].ToString();
                CPT.StrFildesc = dt.Rows[0]["Fildesc"].ToString();
                CPT.StrWatchaskstr = dt.Rows[0]["Watchaskstr"].ToString();
                CPT.StrWatchdesc = dt.Rows[0]["Watchdesc"].ToString();
                CPT.StrReaskstr = dt.Rows[0]["Reaskstr"].ToString();
                CPT.StrRedesc = dt.Rows[0]["Redesc"].ToString();
                CPT.StrBackaskstr = dt.Rows[0]["Backaskstr"].ToString();
                CPT.StrBackdesc = dt.Rows[0]["Backdesc"].ToString();
                CPT.StrWaitaskstr = dt.Rows[0]["Waitaskstr"].ToString();
                CPT.StrWaitdesc = dt.Rows[0]["Waitdesc"].ToString();
            }
            return CPT;
        }

        public static CardUT getUpdateCardUT(string id)
        {
            CardUT CUT = new CardUT();
            string strSql = "select * from tk_techlCardUT where CardID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CUT.StrCardID = id;
                CUT.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                CUT.StrProductSpec = dt.Rows[0]["ProductSpec"].ToString();
                CUT.StrMumTxt = dt.Rows[0]["MumTxt"].ToString();
                CUT.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                CUT.StrPieceName = dt.Rows[0]["PieceName"].ToString();
                CUT.StrProductNum = dt.Rows[0]["ProductNum"].ToString();
                CUT.StrEquipModel = dt.Rows[0]["EquipModel"].ToString();
                CUT.StrMainTxture = dt.Rows[0]["MainTxture"].ToString();
                CUT.StrTxtureNum = dt.Rows[0]["TxtureNum"].ToString();
                CUT.StrHjType = dt.Rows[0]["HjType"].ToString();
                CUT.StrPokoType = dt.Rows[0]["PokoType"].ToString();
                CUT.StrPieceState = dt.Rows[0]["PieceState"].ToString();
                CUT.StrPieceFace = dt.Rows[0]["PieceFace"].ToString();
                CUT.StrYiqiModel = dt.Rows[0]["YiqiModel"].ToString();
                CUT.StrYiqiNumber = dt.Rows[0]["YiqiNumber"].ToString();
                CUT.StrTestPart = dt.Rows[0]["TestPart"].ToString();
                CUT.StrFashePart = dt.Rows[0]["FashePart"].ToString();
                CUT.StrTantType = dt.Rows[0]["TantType"].ToString();
                CUT.StrTantModel = dt.Rows[0]["TantModel"].ToString();
                CUT.StrJpsize = dt.Rows[0]["Jpsize"].ToString();
                CUT.StrKval = dt.Rows[0]["Kval"].ToString();
                CUT.StrTantHead = dt.Rows[0]["TantHead"].ToString();
                CUT.StrOuhType = dt.Rows[0]["OuhType"].ToString();
                CUT.StrDeceteStand = dt.Rows[0]["DeceteStand"].ToString();
                CUT.StrTechlLevel = dt.Rows[0]["TechlLevel"].ToString();
                CUT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CUT.StrDecetePart = dt.Rows[0]["DecetePart"].ToString();
                CUT.StrDeceteLength = dt.Rows[0]["DeceteLength"].ToString();
                CUT.StrDeceteBL = dt.Rows[0]["DeceteBL"].ToString();
                CUT.StrDeceteType = dt.Rows[0]["DeceteType"].ToString();
                CUT.StrDeceteFace = dt.Rows[0]["DeceteFace"].ToString();
                CUT.StrSaoPart = dt.Rows[0]["SaoPart"].ToString();
                CUT.StrSaoType = dt.Rows[0]["SaoType"].ToString();
                CUT.StrSaoSpeed = dt.Rows[0]["SaoSpeed"].ToString();
                CUT.StrSaoConver = dt.Rows[0]["SaoConver"].ToString();
                CUT.StrOuType = dt.Rows[0]["OuType"].ToString();
                CUT.StrTantRange = dt.Rows[0]["TantRange"].ToString();
                CUT.StrFaceBC = dt.Rows[0]["FaceBC"].ToString();
                CUT.StrScan = dt.Rows[0]["Scan"].ToString();
                CUT.StrScanLM = dt.Rows[0]["ScanLM"].ToString();
                CUT.StrLmass = dt.Rows[0]["Lmass"].ToString();
                CUT.StrEL = dt.Rows[0]["EL"].ToString();
                CUT.StrSL = dt.Rows[0]["SL"].ToString();
                CUT.StrRL = dt.Rows[0]["RL"].ToString();
                CUT.StrTechlAsk = dt.Rows[0]["TechlAsk"].ToString();
                CUT.StCardDesc = dt.Rows[0]["CardDesc"].ToString();
            }
            return CUT;
        }

        public static CardMT getUpdateCardMT(string id)
        {
            CardMT CMT = new CardMT();
            string strSql = "select * from tk_techlCardMT where CardID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CMT.StrCardID = id;
                CMT.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                CMT.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                CMT.StrPieceName = dt.Rows[0]["PieceName"].ToString();
                CMT.StrTxtNum = dt.Rows[0]["TxtNum"].ToString();
                CMT.StrPieceSpec = dt.Rows[0]["PieceSpec"].ToString();
                CMT.StrHjType = dt.Rows[0]["HjType"].ToString();
                CMT.StrPokoType = dt.Rows[0]["PokoType"].ToString();
                CMT.StrDetectTime = dt.Rows[0]["DetectTime"].ToString();
                CMT.StrPieceFace = dt.Rows[0]["PieceFace"].ToString();
                CMT.StrDecetePart = dt.Rows[0]["DecetePart"].ToString();
                CMT.StrDeceteEquip = dt.Rows[0]["DeceteEquip"].ToString();
                CMT.StrEquipNumber = dt.Rows[0]["EquipNumber"].ToString();
                CMT.StrCiType = dt.Rows[0]["CiType"].ToString();
                CMT.StrCiConsert = dt.Rows[0]["CiConsert"].ToString();
                CMT.StrTestPiece = dt.Rows[0]["TestPiece"].ToString();
                CMT.StrBlackModel = dt.Rows[0]["BlackModel"].ToString();
                CMT.StrDeceteStand = dt.Rows[0]["DeceteStand"].ToString();
                CMT.StrDeceteBL = dt.Rows[0]["DeceteBL"].ToString();
                CMT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CMT.StrDeceteMethod = dt.Rows[0]["DeceteMethod"].ToString();
                CMT.StrCiTakeType = dt.Rows[0]["CiTakeType"].ToString();
                CMT.StrCiMethod = dt.Rows[0]["CiMethod"].ToString();
                CMT.StrPowerType = dt.Rows[0]["PowerType"].ToString();
                CMT.StrSunFace = dt.Rows[0]["SunFace"].ToString();
                CMT.StrNoBad = dt.Rows[0]["NoBad"].ToString();
                CMT.StrWaskstr = dt.Rows[0]["Waskstr"].ToString();
                CMT.StrEquipaskstr = dt.Rows[0]["Equipaskstr"].ToString();
                CMT.StrCIaskstr = dt.Rows[0]["CIaskstr"].ToString();
                CMT.StrCITimeaskstr = dt.Rows[0]["CITimeaskstr"].ToString();
                CMT.StrTestTimeaskstr = dt.Rows[0]["TestTimeaskstr"].ToString();
                CMT.StrConcertaskstr = dt.Rows[0]["Concertaskstr"].ToString();
                CMT.StrMakeaskstr = dt.Rows[0]["Makeaskstr"].ToString();
                CMT.StrWatchaskstrr = dt.Rows[0]["Watchaskstr"].ToString();
                CMT.StrSetingaskstr = dt.Rows[0]["Setingaskstr"].ToString();
                CMT.StrFwatchaskstr = dt.Rows[0]["Fwatchaskstr"].ToString();
                CMT.StrReaskstr = dt.Rows[0]["Reaskstr"].ToString();
                CMT.StrPassaskstr = dt.Rows[0]["Passaskstr"].ToString();
                CMT.StrRtypeaskstr = dt.Rows[0]["Rtypeaskstr"].ToString();
                CMT.StrRcontaskstr = dt.Rows[0]["Rcontaskstr"].ToString();
                CMT.StrDelCI = dt.Rows[0]["DelCI"].ToString();
                CMT.StrBackHand = dt.Rows[0]["BackHand"].ToString();
                CMT.StrReport = dt.Rows[0]["Report"].ToString();
            }
            return CMT;
        }

        public static int DeleteTechlCardMT(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardMT set Validate = 'i' where CardID = '" + id + "'";

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

        public static int InsertTechlCardUTModel(CardUT CUT, ref string a_strErr)
        {
            int intInsert = 0;
            string strInsert = "";
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string str = "select * from tk_techlCardUTmodel where ProductSpec = '" + CUT.StrProductSpec + "' and MumTxt = '" + CUT.StrMumTxt + "'";
            DataTable dt = SQLBase.FillTable(str, "MainKAT");
            if (dt.Rows.Count == 0)
            {
                strInsert = "insert into tk_techlCardUTmodel (ProductSpec,MumTxt,EquipModel,MainTxture,TxtureNum,HjType,PokoType,PieceState,PieceFace,YiqiModel,YiqiNumber,"
                + "TestPart,FashePart,TantType,TantModel,Jpsize,Kval,TantHead,OuhType,DeceteStand,TechlLevel,HegeLevel,DecetePart,DeceteLength,DeceteBL,DeceteType,DeceteFace,"
                + "SaoPart,SaoType,SaoSpeed,SaoConver,OuType,TantRange,FaceBC,Scan,ScanLM,Lmass,EL,SL,RL,TechlAsk,CardDesc) values ("
                + "'" + CUT.StrProductSpec + "','" + CUT.StrMumTxt + "','" + CUT.StrEquipModel + "','" + CUT.StrMainTxture + "','" + CUT.StrTxtureNum + "','" + CUT.StrHjType + "',"
                + "'" + CUT.StrPokoType + "','" + CUT.StrPieceState + "','" + CUT.StrPieceFace + "','" + CUT.StrYiqiModel + "','" + CUT.StrYiqiNumber + "','" + CUT.StrTestPart + "',"
                + "'" + CUT.StrFashePart + "','" + CUT.StrTantType + "','" + CUT.StrTantModel + "','" + CUT.StrJpsize + "','" + CUT.StrKval + "','" + CUT.StrTantHead + "',"
                + "'" + CUT.StrOuhType + "','" + CUT.StrDeceteStand + "','" + CUT.StrTechlLevel + "','" + CUT.StrHegeLevel + "','" + CUT.StrDecetePart + "','" + CUT.StrDeceteLength + "',"
                + "'" + CUT.StrDeceteBL + "','" + CUT.StrDeceteType + "','" + CUT.StrDeceteFace + "','" + CUT.StrSaoPart + "','" + CUT.StrSaoType + "','" + CUT.StrSaoSpeed + "',"
                + "'" + CUT.StrSaoConver + "','" + CUT.StrOuType + "','" + CUT.StrTantRange + "','" + CUT.StrFaceBC + "','" + CUT.StrScan + "','" + CUT.StrScanLM + "',"
                + "'" + CUT.StrLmass + "','" + CUT.StrEL + "','" + CUT.StrSL + "','" + CUT.StrRL + "','" + CUT.StrTechlAsk + "','" + CUT.StCardDesc + "')";
            }
            else
            {
                strInsert = "update tk_techlCardUTmodel set EquipModel = '" + CUT.StrEquipModel + "',MainTxture = '" + CUT.StrMainTxture + "',TxtureNum = '" + CUT.StrTxtureNum + "',HjType = '" + CUT.StrHjType + "'"
                + ",PokoType = '" + CUT.StrPokoType + "',PieceState = '" + CUT.StrPieceState + "',PieceFace = '" + CUT.StrPieceFace + "',YiqiModel = '" + CUT.StrYiqiModel + "',YiqiNumber = '" + CUT.StrYiqiNumber + "'"
                + ",TestPart = '" + CUT.StrTestPart + "',FashePart = '" + CUT.StrFashePart + "',TantType = '" + CUT.StrTantType + "',TantModel = '" + CUT.StrTantModel + "',Jpsize = '" + CUT.StrJpsize + "'"
                + ",Kval = '" + CUT.StrKval + "',TantHead = '" + CUT.StrTantHead + "',OuhType = '" + CUT.StrOuhType + "',DeceteStand = '" + CUT.StrDeceteStand + "',TechlLevel = '" + CUT.StrTechlLevel + "'"
                + ",HegeLevel = '" + CUT.StrHegeLevel + "',DecetePart = '" + CUT.StrDecetePart + "',DeceteLength = '" + CUT.StrDeceteLength + "',DeceteBL = '" + CUT.StrDeceteBL + "'"
                + ",DeceteType = '" + CUT.StrDeceteType + "',DeceteFace = '" + CUT.StrDeceteFace + "',SaoPart = '" + CUT.StrSaoPart + "',SaoType = '" + CUT.StrSaoType + "',SaoSpeed = '" + CUT.StrSaoSpeed + "'"
                + ",SaoConver = '" + CUT.StrSaoConver + "',OuType = '" + CUT.StrOuType + "',TantRange = '" + CUT.StrTantRange + "',FaceBC = '" + CUT.StrFaceBC + "',Scan = '" + CUT.StrScan + "'"
                + ",ScanLM = '" + CUT.StrScanLM + "',Lmass = '" + CUT.StrLmass + "',EL = '" + CUT.StrEL + "',SL = '" + CUT.StrSL + "',RL = '" + CUT.StrRL + "',TechlAsk = '" + CUT.StrTechlAsk + "',CardDesc = '" + CUT.StCardDesc + "'"
                + " where ProductSpec = '" + CUT.StrProductSpec + "' and MumTxt = '" + CUT.StrMumTxt + "'";
            }
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

        public static int updateTechlCardUT(CardUT CUT, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardUT set ProductSpec = '" + CUT.StrProductSpec + "',MumTxt = '" + CUT.StrMumTxt + "',PieceName = '" + CUT.StrPieceName + "',ProductNum = '" + CUT.StrProductNum + "',EquipModel = '" + CUT.StrEquipModel + "',"
            + "MainTxture = '" + CUT.StrMainTxture + "',TxtureNum = '" + CUT.StrTxtureNum + "',HjType = '" + CUT.StrHjType + "',PokoType = '" + CUT.StrPokoType + "',PieceState = '" + CUT.StrPieceState + "',PieceFace = '" + CUT.StrPieceFace + "',"
            + "YiqiModel = '" + CUT.StrYiqiModel + "',YiqiNumber = '" + CUT.StrYiqiNumber + "',TestPart = '" + CUT.StrTestPart + "',FashePart = '" + CUT.StrFashePart + "',TantType = '" + CUT.StrTantType + "',TantModel = '" + CUT.StrTantModel + "',"
            + "Jpsize = '" + CUT.StrJpsize + "',Kval = '" + CUT.StrKval + "',TantHead = '" + CUT.StrTantHead + "',OuhType = '" + CUT.StrOuhType + "',DeceteStand = '" + CUT.StrDeceteStand + "',TechlLevel = '" + CUT.StrTechlLevel + "',"
            + "HegeLevel = '" + CUT.StrHegeLevel + "',DecetePart = '" + CUT.StrDecetePart + "',DeceteLength = '" + CUT.StrDeceteLength + "',DeceteBL = '" + CUT.StrDeceteBL + "',DeceteType = '" + CUT.StrDeceteType + "',DeceteFace = '" + CUT.StrDeceteFace + "',"
            + "SaoPart = '" + CUT.StrSaoPart + "',SaoType = '" + CUT.StrSaoType + "',SaoSpeed = '" + CUT.StrSaoSpeed + "',SaoConver = '" + CUT.StrSaoConver + "',OuType = '" + CUT.StrOuType + "',TantRange = '" + CUT.StrTantRange + "',"
            + "FaceBC = '" + CUT.StrFaceBC + "',Scan = '" + CUT.StrScan + "',ScanLM = '" + CUT.StrScanLM + "',Lmass = '" + CUT.StrLmass + "',EL = '" + CUT.StrEL + "',SL = '" + CUT.StrSL + "',"
            + "RL = '" + CUT.StrRL + "',TechlAsk = '" + CUT.StrTechlAsk + "',CardDesc = '" + CUT.StCardDesc + "' where CardID = '" + CUT.StrCardID + "'";

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

        public static int InsertTechlCardUT(CardUT CardUT, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<CardUT>(CardUT, "tk_techlCardUT");
            string strInsertCont = "update tk_TaskDecete set IsCard = '1' where DetectID = '" + CardUT.StrDetectID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int InsertTechlCardMT(CardMT CardMT, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<CardMT>(CardMT, "tk_techlCardMT");
            string strInsertCont = "update tk_TaskDecete set IsCard = '1' where DetectID = '" + CardMT.StrDetectID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int UpdateTechlCardMT(CardMT CMT, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardMT set PieceName = '" + CMT.StrPieceName + "',TxtNum = '" + CMT.StrTxtNum + "',PieceSpec = '" + CMT.StrPieceSpec + "',HjType = '" + CMT.StrHjType + "',PokoType = '" + CMT.StrPokoType + "',"
            + "DetectTime = '" + CMT.StrDetectTime + "',PieceFace = '" + CMT.StrPieceFace + "',DecetePart = '" + CMT.StrDecetePart + "',DeceteEquip = '" + CMT.StrDeceteEquip + "',EquipNumber = '" + CMT.StrEquipNumber + "',"
            + "CiType = '" + CMT.StrCiType + "',CiConsert = '" + CMT.StrCiConsert + "',TestPiece = '" + CMT.StrTestPiece + "',BlackModel = '" + CMT.StrBlackModel + "',DeceteStand = '" + CMT.StrDeceteStand + "',"
            + "DeceteBL = '" + CMT.StrDeceteBL + "',HegeLevel = '" + CMT.StrHegeLevel + "',DeceteMethod = '" + CMT.StrDeceteMethod + "',CiTakeType = '" + CMT.StrCiTakeType + "',CiMethod = '" + CMT.StrCiMethod + "',"
            + "PowerType = '" + CMT.StrPowerType + "',SunFace = '" + CMT.StrSunFace + "',NoBad = '" + CMT.StrNoBad + "',Waskstr = '" + CMT.StrWaskstr + "',Equipaskstr = '" + CMT.StrEquipaskstr + "',"
            + "CIaskstr = '" + CMT.StrCIaskstr + "',CITimeaskstr = '" + CMT.StrCITimeaskstr + "',TestTimeaskstr = '" + CMT.StrTestTimeaskstr + "',Concertaskstr = '" + CMT.StrConcertaskstr + "',Makeaskstr = '" + CMT.StrMakeaskstr + "',"
            + "Watchaskstr = '" + CMT.StrWatchaskstrr + "',Setingaskstr = '" + CMT.StrSetingaskstr + "',Fwatchaskstr = '" + CMT.StrFwatchaskstr + "',Reaskstr = '" + CMT.StrReaskstr + "',Passaskstr = '" + CMT.StrPassaskstr + "',"
            + "Rtypeaskstr = '" + CMT.StrRtypeaskstr + "',Rcontaskstr = '" + CMT.StrRcontaskstr + "',DelCI = '" + CMT.StrDelCI + "',BackHand = '" + CMT.StrBackHand + "',Report = '" + CMT.StrReport + "' where CardID = '" + CMT.StrCardID + "'";

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

        public static int InsertTechlCardPT(CardPT CardPT, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<CardPT>(CardPT, "tk_techlCardPT");
            string strInsertCont = "update tk_TaskDecete set IsCard = '1' where DetectID = '" + CardPT.StrDetectID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int UpdateTechlCardPT(CardPT CPT, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = "update tk_techlCardPT set PieceName = '" + CPT.StrPieceName + "',TxtNum = '" + CPT.StrTxtNum + "',PieceSpec = '" + CPT.StrPieceSpec + "',HjType = '" + CPT.StrHjType + "',PokoType = '" + CPT.StrPokoType + "',"
            + "DetectTime = '" + CPT.StrDetectTime + "',PieceFace = '" + CPT.StrPieceFace + "',DecetePart = '" + CPT.StrDecetePart + "',DeceteWD = '" + CPT.StrDeceteWD + "',Pttype = '" + CPT.StrPttype + "',WashJtype = '" + CPT.StrWashJtype + "',"
            + "FilmType = '" + CPT.StrFilmType + "',TestPiece = '" + CPT.StrTestPiece + "',Quick = '" + CPT.StrQuick + "',DeceteMethod = '" + CPT.StrDeceteMethod + "',PtMethod = '" + CPT.StrPtMethod + "',DelMethod = '" + CPT.StrDelMethod + "',"
            + "HotWD = '" + CPT.StrHotWD + "',HotTime = '" + CPT.StrHotTime + "',FilmtMethod = '" + CPT.StrFilmtMethod + "',PtTime = '" + CPT.StrPtTime + "',FilmTime = '" + CPT.StrFilmTime + "',WatchMethod = '" + CPT.StrWatchMethod + "',"
            + "FaceAsk = '" + CPT.StrFaceAsk + "',DeceteStand = '" + CPT.StrDeceteStand + "',DeceteBL = '" + CPT.StrDeceteBL + "',HegeLevel = '" + CPT.StrHegeLevel + "',DetectJask = '" + CPT.StrDetectJask + "',SureStand = '" + CPT.StrSureStand + "',"
            + "PersonSafe = '" + CPT.StrPersonSafe + "',EquipSafe = '" + CPT.StrEquipSafe + "',Faskstr = '" + CPT.StrFaskstr + "',Fdesc = '" + CPT.StrFdesc + "',Waskstr = '" + CPT.StrWaskstr + "',Wdesc = '" + CPT.StrWdesc + "',"
            + "PTaskstr = '" + CPT.StrPTaskstr + "',PTdesc = '" + CPT.StrPTdesc + "',Delaskstr = '" + CPT.StrDelaskstr + "',Deldesc = '" + CPT.StrDeldesc + "',Filaskstr = '" + CPT.StrFilaskstr + "',Fildesc = '" + CPT.StrFildesc + "',"
            + "Watchaskstr = '" + CPT.StrWatchaskstr + "',Watchdesc = '" + CPT.StrWatchdesc + "',Reaskstr = '" + CPT.StrReaskstr + "',Redesc = '" + CPT.StrRedesc + "',Backaskstr = '" + CPT.StrBackaskstr + "',Backdesc = '" + CPT.StrBackdesc + "',"
            + "Waitaskstr = '" + CPT.StrWaitaskstr + "',Waitdesc = '" + CPT.StrWaitdesc + "' where CardID = '" + CPT.StrCardID + "'";

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

        public static UIDataTable getCardRTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCardRT", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static CardRT getUpdateCarRT(string id)
        {
            CardRT CRT = new CardRT();
            string strSql = "select * from tk_techlCardRT where CardID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            if (dt.Rows.Count > 0)
            {
                CRT.StrCardID = id;
                CRT.StrDetectID = dt.Rows[0]["DetectID"].ToString();
                CRT.StrPipeStand = dt.Rows[0]["PipeStand"].ToString();
                CRT.StrRttype = dt.Rows[0]["Rttype"].ToString();
                CRT.StrTZType = dt.Rows[0]["TZType"].ToString();
                CRT.StrTZPly = dt.Rows[0]["TZPly"].ToString();
                CRT.StrStandSize = dt.Rows[0]["StandSize"].ToString();
                CRT.StrTaskName = dt.Rows[0]["TaskName"].ToString();
                CRT.StrPieceName = dt.Rows[0]["PieceName"].ToString();
                CRT.StrProductNum = dt.Rows[0]["ProductNum"].ToString();
                CRT.StrPressType = dt.Rows[0]["PressType"].ToString();
                CRT.StrHotState = dt.Rows[0]["HotState"].ToString();
                CRT.StrPieceState = dt.Rows[0]["PieceState"].ToString();
                CRT.StrMainTxture = dt.Rows[0]["MainTxture"].ToString();
                CRT.StrTxtureNum = dt.Rows[0]["TxtureNum"].ToString();
                CRT.StrPokoType = dt.Rows[0]["PokoType"].ToString();
                CRT.StrHjType = dt.Rows[0]["HjType"].ToString();
                CRT.StrYiqiModel = dt.Rows[0]["YiqiModel"].ToString();
                CRT.StrFocusSize = dt.Rows[0]["FocusSize"].ToString();
                CRT.StrXZJModel = dt.Rows[0]["XZJModel"].ToString();
                CRT.StrPb = dt.Rows[0]["Pb"].ToString();
                CRT.StrBPB = dt.Rows[0]["BPB"].ToString();
                CRT.StrFilmType = dt.Rows[0]["FilmType"].ToString();
                CRT.StrFilmSpec = dt.Rows[0]["FilmSpec"].ToString();
                CRT.StrFilmPF = dt.Rows[0]["FilmPF"].ToString();
                CRT.StrWashType = dt.Rows[0]["WashType"].ToString();
                CRT.StrWashEquipModel = dt.Rows[0]["WashEquipModel"].ToString();
                CRT.StrExecuteStand = dt.Rows[0]["ExecuteStand"].ToString();
                CRT.StrTechlLevelRT = dt.Rows[0]["TechlLevelRT"].ToString();
                CRT.StrHegeLevel = dt.Rows[0]["HegeLevel"].ToString();
                CRT.StrDeceteBL = dt.Rows[0]["DeceteBL"].ToString();
                CRT.StrHflength = dt.Rows[0]["Hflength"].ToString();
                CRT.StrDetectTime = dt.Rows[0]["DetectTime"].ToString();
                CRT.StrDecetePart = dt.Rows[0]["DecetePart"].ToString();
                CRT.StrBlackRange = dt.Rows[0]["BlackRange"].ToString();
                CRT.StrSiNum = dt.Rows[0]["SiNum"].ToString();
                CRT.StrKv = dt.Rows[0]["Kv"].ToString();
                CRT.StrCi = dt.Rows[0]["Ci"].ToString();
                CRT.StrmA = dt.Rows[0]["mA"].ToString();
                CRT.StrCiTime = dt.Rows[0]["CiTime"].ToString();
                CRT.StrCimin = dt.Rows[0]["Cimin"].ToString();
                CRT.StrFocusLength = dt.Rows[0]["FocusLength"].ToString();
                CRT.StrOneTimelength = dt.Rows[0]["OneTimelength"].ToString();
                CRT.StrDelength = dt.Rows[0]["Delength"].ToString();
                CRT.StTechlAsk = dt.Rows[0]["TechlAsk"].ToString();
                CRT.StCardDesc = dt.Rows[0]["CardDesc"].ToString();
            }
            return CRT;
        }

        public static DataTable GetPrintCardRT(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintCardRT", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static UIDataTable getCardUTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCardUT", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static DataTable GetPrintCardUT(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintCardUT", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static UIDataTable getCardMTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCardMT", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static DataTable GetPrintCardMT(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintCardMT", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static UIDataTable getCardPTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCardPT", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static DataTable GetPrintCardPT(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintCardPT", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }

        public static UIDataTable getCarGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCar", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static int InsertIssuedCar(string taskNumber, string car, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string[] arrCar = car.Split(',');
            string strInsert = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string user = account.UserID.ToString();
            strInsert = " delete from tk_IssuedTaskCar where TaskNumber='" + taskNumber + "'";
            for (int i = 0; i < arrCar.Length; i++)
            {
                strInsert += "insert into tk_IssuedTaskCar (TaskNumber,CarID,CreateUser,CreateTime,Validate) values ('" + taskNumber + "','" + arrCar[i] + "','" + user + "','" + DateTime.Now + "','v')";
            }
            string strInsertCont = "update tk_Task set State = '1' where TaskNumber = '" + taskNumber + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static UIDataTable getUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getUser", CommandType.StoredProcedure, sqlPar, "MainKAT");
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

        public static int InsertIssuedOther(IssuedOtherTask OtherTask, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<IssuedOtherTask>(OtherTask, "tk_IssuedOtherTask");
            string strInsertCont = "update tk_Task set State = '1' where TaskNumber = '" + OtherTask.StrTaskNumber + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static int InsertIssuedRTTask(IssuedTaskRT TaskRT, ref string a_strErr)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            string strInsert = GSqlSentence.GetInsertInfoByD<IssuedTaskRT>(TaskRT, "tk_IssuedTaskRT");
            string strInsertCont = "update tk_Task set State = '1' where TaskNumber = '" + TaskRT.StrTaskNumber + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertCont != "")
                    intInsertCont = sqlTrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intInsertCont;
        }

        public static DataTable GetPrintTaskRT(string where)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };

            DataTable dt = SQLBase.FillTable("getPrintTaskRT", CommandType.StoredProcedure, sqlPar, "MainKAT");
            return dt;
        }







        // 20150721 ly 获取委托单详细 
        public static string getDrawNum(string TaskID, string TaskName)
        {
            string strSql = " select isnull(DrawingNum,'') DrawingNum,isnull(ProjectNum,'') ProjectNum,b.Text as BuildUnit,c.Text as VisorUnit ";
            strSql += " from tk_EntrustTask a ";
            strSql += " left join (select * from tk_ConfigUnit where Type='BuildUnit') b on a.BuildUnit=b.UnitID ";
            strSql += " left join (select * from tk_ConfigUnit where Type='VisorUnit') c on a.VisorUnit=c.UnitID ";
            strSql += " where ";
            if (TaskID != "")
                strSql += " TaskID='" + TaskID + "' and";
            if (TaskName != "")
                strSql += " TaskName='" + TaskName + "' and";

            strSql = strSql.Substring(0, strSql.Length - 3);
            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            string strDNum = dt.Rows[0]["DrawingNum"].ToString() + "@" + dt.Rows[0]["ProjectNum"].ToString() + "@" +
                dt.Rows[0]["BuildUnit"].ToString() + "@" + dt.Rows[0]["VisorUnit"].ToString();

            return strDNum;

        }
        // 获取管径规格、管径长度 
        public static string getNewPipe(string TaskID)
        {
            // 管径规格
            string strSql = " select distinct isnull(b.Text,'') Type from tk_EntrustTaskCont a, tk_ConfigContent b ";
            strSql += " where a.PipeSize=b.SID and b.Type='PipeSize' and a.TaskID='" + TaskID + "'";

            DataTable dt = SQLBase.FillTable(strSql, "MainKAT");
            string strPipe = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strPipe += dt.Rows[i]["Type"].ToString() + " ";
            }
            // 管径总长度 
            strSql = " select SUM(length) length from tk_EntrustTaskCont where TaskID='" + TaskID + "'";
            DataTable dt2 = SQLBase.FillTable(strSql, "MainKAT");
            strPipe += "@" + dt2.Rows[0][0].ToString();

            // 检测方法 
            strPipe += "@" + "射线检测";
            return strPipe;

        }


        public static DataTable getDeceteListNewOld(string taskID, string length, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                // 获取 全线、长度、检测比例、管径规格、检测焊口数、拍片数[每道口]、任务编号 
                string strSql1 = " select '全线' as name,'" + length + "米' as length,c.DetectScale,e.DNType as Caliber,d.Num as WeldCount,";
                strSql1 += " f.PicNum ,a.TaskNumber,' ' as Comments ";
                strSql1 += " from tk_DayLogRT a ";
                strSql1 += " left join (select * from tk_Task) b on a.TaskNumber=b.TaskNumber ";
                strSql1 += " left join (select * from tk_TaskDecete where DeceteType='RT') c on c.DetectID=b.DetectID ";
                strSql1 += " left join (select TaskNumber,Caliber,count(WeldSymbols) as Num from tk_MulRecordRT group by TaskNumber,Caliber) d on a.TaskNumber=d.TaskNumber";
                strSql1 += " left join (select * from tk_ConfigRTSize) e on d.Caliber=e.SizeID ";
                strSql1 += " left join (select * from tk_ConfigDnPic) f on e.DNType=f.DNType ";
                strSql1 += " where a.TaskID='" + taskID + "'";
                DataTable dtAll = SQLBase.FillTable(strSql1, "MainKAT");
                int AllCount = 0;
                int PicCount = 0;
                if (dtAll != null && dtAll.Rows.Count > 0)
                {
                    for (int row1 = 0; row1 < dtAll.Rows.Count; row1++)
                    {
                        string DN = dtAll.Rows[row1]["Caliber"].ToString();
                        if (row1 > 0)
                        {
                            string DN1 = dtAll.Rows[row1 - 1]["Caliber"].ToString();
                            int WeldCount1 = Convert.ToInt32(dtAll.Rows[row1 - 1]["WeldCount"]);// 焊口数
                            if (DN1 == DN)// 上下相等 
                            {
                                int preW = Convert.ToInt32(dtAll.Rows[row1]["WeldCount"]);
                                dtAll.Rows[row1 - 1]["WeldCount"] = WeldCount1 + preW;
                                dtAll.Rows.RemoveAt(row1);
                            }
                        }
                    }
                }
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    string taskNum = dtAll.Rows[i]["TaskNumber"].ToString();
                    string caliber = dtAll.Rows[i]["Caliber"].ToString();
                    string strComments = dtAll.Rows[i]["Comments"].ToString();// 备注 
                    int Weld = Convert.ToInt32(dtAll.Rows[i]["WeldCount"]);// 焊口数
                    int Pic = Convert.ToInt32(dtAll.Rows[i]["PicNum"]);// 每个焊口对应的拍片数
                    dtAll.Rows[i]["PicNum"] = Weld * Pic;
                    AllCount += Weld;
                    PicCount += Weld * Pic;

                    //--射线类型、管径规格、焊口数、拍片数 
                    string strSql2 = " select a.TaskNumber,b.Text as RayType,e.DNType as Caliber,COUNT(a.WeldSymbols) as WeldCount,d.PicNum ";
                    strSql2 += " from tk_MulRecordRT a ";
                    strSql2 += " left join (select * from tk_ConfigContent where Type='Rttype') b on a.RayType=b.SID ";
                    strSql2 += " left join (select * from tk_DayLogRT) c on a.TaskNumber=c.TaskNumber ";
                    strSql2 += " left join (select * from tk_ConfigRTSize) e on a.Caliber=e.SizeID ";
                    strSql2 += " left join (select * from tk_ConfigDnPic) d on e.DNType=d.DNType ";
                    strSql2 += " where a.TaskNumber='" + taskNum + "' and e.DNType='" + caliber + "' ";
                    strSql2 += " group by a.TaskNumber,b.Text,e.DNType,a.WeldSymbols,d.PicNum ";
                    strSql2 += " order by RayType asc ";

                    DataTable dtDetail = SQLBase.FillTable(strSql2, "MainKAT");
                    if (dtDetail != null && dtDetail.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtDetail.Rows.Count; j++)
                        {
                            string strType = dtDetail.Rows[j]["RayType"].ToString();
                            string strCailber = dtDetail.Rows[j]["Caliber"].ToString();
                            int intWeldCount = Convert.ToInt32(dtDetail.Rows[j]["WeldCount"]);
                            int intPic = Convert.ToInt32(dtDetail.Rows[j]["PicNum"]);
                            int intNum = intWeldCount * intPic;

                            strComments += strType + "管径规格" + strCailber + "，焊口数" + intWeldCount + "，拍片数" + intNum + "；";

                        }

                        dtAll.Rows[i]["Comments"] = strComments.Substring(0, strComments.Length - 1);

                    }

                }
                DataRow dr = dtAll.NewRow();
                dr[dtAll.Columns.Count - 3] = PicCount;
                dr[dtAll.Columns.Count - 4] = AllCount;
                dtAll.Rows.Add(dr);

                return dtAll;

            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }

        // 获取检测工作量列表 
        public static DataTable getDeceteListNew(string taskID, string length, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                // 获取 全线、长度、检测比例、检测焊口数、拍片数[每道口]、任务编号 
                string strSel2 = " select '全线' as name,'" + length + "米' as length,";
                strSel2 += " d.DetectScale,g.DNType as Caliber,a.WeldSymbols,f.PicNum,a.TaskNumber,'' as Comments ";
                strSel2 += " from tk_MulRecordRT a ";
                strSel2 += " left join (select * from tk_ConfigRTPipe where validate='v') b on a.Caliber=b.Text ";
                strSel2 += " left join (select * from tk_ConfigRTSize where Validate='v') g on b.PipeID=g.PipeID ";
                strSel2 += " left join (select * from tk_Task where Validate='v' and TaskType='RT') c on a.TaskNumber=c.TaskNumber ";
                strSel2 += " left join (select * from tk_TaskDecete where Validate='v' and DeceteType='RT') d on c.DetectID=d.DetectID ";
                strSel2 += " left join (select * from tk_EntrustTask where Validate='v') e on e.TaskID=d.TaskID ";
                strSel2 += " left join (select * from tk_ConfigDnPic) f on g.DNType=f.DNType ";
                strSel2 += " where e.TaskID='" + taskID + "'";

                string strSql = " select l.name,l.length,l.DetectScale,l.Caliber, COUNT(l.WeldSymbols) as WeldCount,l.PicNum,l.TaskNumber,l.Comments from ";
                strSql += "(" + strSel2 + ")l";
                strSql += " group by l.Caliber,l.PicNum,l.name,l.length,l.DetectScale,l.TaskNumber,l.Comments ";

                DataTable dtAll = SQLBase.FillTable(strSql, "MainKAT");
                int AllCount = 0;
                int PicCount = 0;
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    string taskNum = dtAll.Rows[i]["TaskNumber"].ToString();
                    string caliber = dtAll.Rows[i]["Caliber"].ToString();
                    string strComments = dtAll.Rows[i]["Comments"].ToString();// 备注 
                    int Weld = Convert.ToInt32(dtAll.Rows[i]["WeldCount"]);// 焊口数
                    int Pic = Convert.ToInt32(dtAll.Rows[i]["PicNum"]);// 每个焊口对应的拍片数
                    dtAll.Rows[i]["PicNum"] = Weld * Pic;
                    AllCount += Weld;
                    PicCount += Weld * Pic;

                    // 射线类型、管径规格、焊口数、拍片数 
                    string strSql2 = " select a.TaskNumber,a.RayType,b.Text as RayTypeDesc,e.DNType as Caliber,COUNT(a.WeldSymbols) as WeldCount,d.PicNum ";
                    strSql2 += " from tk_MulRecordRT a ";
                    strSql2 += " left join (select * from tk_ConfigContent where Type='Rttype') b on a.RayType=b.SID ";
                    strSql2 += " left join (select * from tk_DayLogRT) c on a.TaskNumber=c.TaskNumber ";
                    strSql2 += " left join ( select * from tk_ConfigRTPipe where Validate='v')f on f.Text=a.Caliber ";
                    strSql2 += " left join (select * from tk_ConfigRTSize where validate='v') e on f.PipeID=e.PipeID ";
                    strSql2 += " left join (select * from tk_ConfigDnPic) d on e.DNType=d.DNType ";
                    strSql2 += " where a.TaskNumber='" + taskNum + "' and e.DNType='" + caliber + "' ";
                    strSql2 += " group by a.TaskNumber,b.Text,e.DNType,a.WeldSymbols,d.PicNum,a.RayType ";
                    strSql2 += " order by RayType asc ";

                    DataTable dtDetail = SQLBase.FillTable(strSql2, "MainKAT");
                    if (dtDetail != null && dtDetail.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtDetail.Rows.Count; j++)
                        {
                            string strType = dtDetail.Rows[j]["RayType"].ToString();
                            string strType2 = dtDetail.Rows[j]["RayTypeDesc"].ToString();
                            if (strType2 != null && strType2 != "")
                            {
                                strType = strType2;
                                dtDetail.Rows[j]["RayType"] = strType2;
                            }
                            string strCailber = dtDetail.Rows[j]["Caliber"].ToString();
                            int intWeldCount = Convert.ToInt32(dtDetail.Rows[j]["WeldCount"]);
                            int intPic = Convert.ToInt32(dtDetail.Rows[j]["PicNum"]);
                            int intNum = intWeldCount * intPic;

                            strComments += strType + "管径规格" + strCailber + "，焊口数" + intWeldCount + "，拍片数" + intNum + "；";

                        }

                        dtAll.Rows[i]["Comments"] = strComments.Substring(0, strComments.Length - 1);

                    }

                }
                DataRow dr = dtAll.NewRow();
                dr[dtAll.Columns.Count - 3] = PicCount;
                dr[dtAll.Columns.Count - 4] = AllCount;
                dtAll.Rows.Add(dr);

                return dtAll;

            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }

        // 获取 结算信息
        public static TaskAccounts getTaskA(string taskID)
        {
            TaskAccounts taskacc = new TaskAccounts();

            string strSql = " select a.* from tk_TaskAccounts a ";
            strSql += " where a.TaskID='" + taskID + "'";
            DataTable dtDetail = SQLBase.FillTable(strSql, "MainKAT");

            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                taskacc.StrAccountsID = dtDetail.Rows[0]["AccountsID"].ToString();
                taskacc.StrTaskID = taskID;
                taskacc.StrAccAmount = dtDetail.Rows[0]["AccAmount"].ToString();
                taskacc.StrPostTime = dtDetail.Rows[0]["PostTime"].ToString();

                if (dtDetail.Rows[0]["ContractPrice"] != null && dtDetail.Rows[0]["ContractPrice"].ToString() != "")
                    taskacc.StrContractPrice = Convert.ToDecimal(dtDetail.Rows[0]["ContractPrice"]);
                else
                    taskacc.StrContractPrice = null;

                taskacc.StrSignTime = dtDetail.Rows[0]["SignTime"].ToString();
                if (dtDetail.Rows[0]["ActualPrice"] != null && dtDetail.Rows[0]["ActualPrice"].ToString() != "")
                    taskacc.StrActualPrice = Convert.ToDecimal(dtDetail.Rows[0]["ActualPrice"]);
                else
                    taskacc.StrActualPrice = null;
                taskacc.StrActualTime = dtDetail.Rows[0]["ActualTime"].ToString();
                taskacc.StrKnotStyle = dtDetail.Rows[0]["KnotStyle"].ToString();
                taskacc.StrComments = dtDetail.Rows[0]["Comments"].ToString();
                taskacc.StrIsSign = dtDetail.Rows[0]["IsSign"].ToString();

                taskacc.StrIsBill = dtDetail.Rows[0]["IsBill"].ToString();

                if (dtDetail.Rows[0]["RePairPrice"] != null && dtDetail.Rows[0]["RePairPrice"].ToString() != "")
                    taskacc.StrRePairPrice = Convert.ToDecimal(dtDetail.Rows[0]["RePairPrice"]);
                else
                    taskacc.StrRePairPrice = null;

            }
            else
            {
                taskacc.StrAccountsID = "";
                taskacc.StrTaskID = taskID;
                taskacc.StrAccAmount = "";
                taskacc.StrPostTime = "";
                taskacc.StrContractPrice = null;
                taskacc.StrSignTime = "";
                taskacc.StrActualPrice = null;
                taskacc.StrActualTime = "";
                taskacc.StrKnotStyle = "";
                taskacc.StrComments = "";
                taskacc.StrIsSign = "";
                taskacc.StrRePairPrice = null;
                taskacc.StrIsBill = "";
            }

            return taskacc;

        }

        // 提交结算信息 
        public static int SaveNewContract(string AccountsID, string TaskID, string AccAmount, string PostTime, string ContractPrice,
            string SignTime, string ActualPrice, string ActualTime, string KnotStyle, string IsSign, string Comments, string RepairPrice,
            string IsBill, ref string a_strErr)
        {
            string strSql = "";
            int intCount = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            try
            {
                string strSel = " select count(*) from tk_TaskAccounts where TaskID='" + TaskID + "'";
                intCount = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
                if (intCount > 0)
                {
                    strSql = " update tk_TaskAccounts set AccountsID='" + AccountsID + "',AccAmount='" + AccAmount + "',PostTime='" + PostTime + "',ContractPrice='" + ContractPrice +
                        "',SignTime='" + SignTime + "',ActualPrice='" + ActualPrice + "',ActualTime='" + ActualTime + "',KnotStyle='" + KnotStyle + "',IsSign='" + IsSign +
                        "',Comments='" + Comments + "', RePairPrice='" + RepairPrice + "',IsBill='" + IsBill + "'";

                    strSql += " where TaskID='" + TaskID + "'";

                }
                else
                {
                    strSql = " insert into tk_TaskAccounts values('" + AccountsID + "','" + TaskID + "','" + AccAmount + "','" + PostTime + "','" + ContractPrice + "','" + SignTime +
                        "','" + ActualPrice + "','" + ActualTime + "','" + KnotStyle + "','" + IsSign + "','" + Comments + "','" + RepairPrice + "','" + IsBill + "')";

                }

                intCount = sqlTrans.ExecuteNonQuery(strSql, CommandType.Text, null);
                if (intCount > 0)
                {
                    sqlTrans.Close(true);
                    return intCount;
                }
                else
                {
                    sqlTrans.Close(true);
                    return -1;
                }

            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
        }

        // 获取 预算信息 
        public static TaskBudget getBudget(string taskID)
        {
            TaskBudget taskacc = new TaskBudget();

            string strSql = " select * from tk_TaskBudget where TaskID='" + taskID + "'";
            DataTable dtDetail = SQLBase.FillTable(strSql, "MainKAT");

            if (dtDetail != null && dtDetail.Rows.Count > 0)
            {
                taskacc.StrBudgetID = dtDetail.Rows[0]["BudgetID"].ToString();
                taskacc.StrTaskID = taskID;
                taskacc.StrPostTime = dtDetail.Rows[0]["PostTime"].ToString();

                if (dtDetail.Rows[0]["ContractPrice"] != null && dtDetail.Rows[0]["ContractPrice"].ToString() != "")
                    taskacc.StrContractPrice = Convert.ToDecimal(dtDetail.Rows[0]["ContractPrice"]);
                else
                    taskacc.StrContractPrice = null;

                if (dtDetail.Rows[0]["AdvancePrice"] != null && dtDetail.Rows[0]["AdvancePrice"].ToString() != "")
                    taskacc.StrAdvancePrice = Convert.ToDecimal(dtDetail.Rows[0]["AdvancePrice"]);
                else
                    taskacc.StrAdvancePrice = null;
                taskacc.StrAppTime = dtDetail.Rows[0]["AppTime"].ToString();

                if (dtDetail.Rows[0]["ProPrice"] != null && dtDetail.Rows[0]["ProPrice"].ToString() != "")
                    taskacc.StrProPrice = Convert.ToDecimal(dtDetail.Rows[0]["ProPrice"]);
                else
                    taskacc.StrProPrice = null;

                taskacc.StrProTime = dtDetail.Rows[0]["ProTime"].ToString();
                taskacc.StrComments = dtDetail.Rows[0]["Comments"].ToString();

            }
            else
            {
                taskacc.StrBudgetID = "";
                taskacc.StrTaskID = taskID;
                taskacc.StrPostTime = "";
                taskacc.StrContractPrice = null;
                taskacc.StrAdvancePrice = null;
                taskacc.StrAppTime = "";
                taskacc.StrProPrice = null;
                taskacc.StrProTime = "";
                taskacc.StrComments = "";

            }

            return taskacc;

        }

        // 提交预算信息
        public static int SaveNewBudget(string BudgetID, string TaskID, string PostTime, string ContractPrice, string AdvancePrice, string AppTime,
            string ProPrice, string ProTime, string Comments, ref string a_strErr)
        {
            string strSql = "";
            int intCount = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainKAT");
            try
            {
                string strSel = " select count(*) from tk_TaskBudget where TaskID='" + TaskID + "'";
                intCount = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
                if (intCount > 0)
                {
                    strSql = " update tk_TaskBudget set BudgetID='" + BudgetID + "',PostTime='" + PostTime + "',ContractPrice='" + ContractPrice +
                        "',AdvancePrice='" + AdvancePrice + "',AppTime='" + AppTime + "',ProPrice='" + ProPrice + "',ProTime='" + ProTime +
                        "',Comments='" + Comments + "'";

                    strSql += " where TaskID='" + TaskID + "'";

                }
                else
                {
                    strSql = " insert into tk_TaskBudget values('" + BudgetID + "','" + TaskID + "','" + PostTime + "','" + ContractPrice + "','" +
                        AdvancePrice + "','" + AppTime + "','" + ProPrice + "','" + ProTime + "','" + Comments + "')";

                }

                intCount = sqlTrans.ExecuteNonQuery(strSql, CommandType.Text, null);
                if (intCount > 0)
                {
                    sqlTrans.Close(true);
                    return intCount;
                }
                else
                {
                    sqlTrans.Close(true);
                    return -1;
                }

            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
        }
    }
}
