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
    public class COM_ApprovalPro
    {
        public static string getwebkey(string webkey)
        {
            string key = "";
            string str = "select * from [BGOI_BasMan]..tk_ConfigWebkey where ApprovalType = '" + webkey + "'";
            DataTable dt = SQLBase.FillTable(str);
            if (dt.Rows.Count > 0)
                key = dt.Rows[0]["webkey"].ToString();
            return key;
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
                strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";

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
        public static int judgeLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            // 返回bol区别  -2审批未通过  -1- 不是审批人员 0-可以审批 1-已经审批 2-还没有到该人员审批
            int bol = 0;
            int Level = 0;
            int State = 0;
            string AppType = "";
            int haveCount = 0;
            int count = 0;
            int countl = 0;
            string[] arr = folderBack.Split('/');
            string strSql = "select ApprovalLevel,State,SUBSTRING(AppType,1,1) as AppType,SUBSTRING(AppType,3,1) as num from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalPersons = '" + userid + "'";
            DataTable dt = SQLBase.FillTable(strSql);
            if (dt.Rows.Count == 0 || dt == null)
            {
                bol = -1;
                return bol;
            }
            Level = Convert.ToInt16(dt.Rows[0]["ApprovalLevel"]);
            State = Convert.ToInt16(dt.Rows[0]["State"]);
            int lastLevel = Level - 1;
            if (Level == 0)
            {
                if (State == 0)
                {
                    bol = 0;
                    return bol;
                }
                else
                {
                    bol = 1;
                    return bol;
                }
            }
            else
            {
                if (State == 1)
                {
                    bol = 1;
                    return bol;
                }
                if (State == -1)
                {
                    bol = -2;
                    return bol;
                }
                string strl = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and AppType = '2'  and ApprovalLevel < '" + Level + "' and state = '0'";
                DataTable dtl = SQLBase.FillTable(strl);
                if (dtl.Rows.Count > 0)
                    countl = Convert.ToInt16(dtl.Rows[0][0]);
                if (countl > 0)
                {
                    bol = 2;
                    return bol;
                }
                string str = "select SUBSTRING(AppType,1,1) as AppType from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt1 = SQLBase.FillTable(str);
                string str2 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt3 = SQLBase.FillTable(str2);
                string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "' and state = '1'";
                DataTable dt2 = SQLBase.FillTable(str1);
                if (dt1.Rows.Count > 0)
                    AppType = dt1.Rows[0]["AppType"].ToString();
                if (dt3.Rows.Count > 0)
                    haveCount = Convert.ToInt16(dt3.Rows[0][0]);
                if (dt2.Rows.Count > 0)
                    count = Convert.ToInt16(dt2.Rows[0][0]);
                if (AppType == "1")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                if (AppType == "2")
                {
                    if (count != haveCount)
                    {
                        bol = 2;
                    }
                    else
                    {
                        bol = 0;
                    }
                }
                if (AppType == "3")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                return bol;
            }
        }
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
                    string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '1' and AppType = '3-"+num+"'";
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
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else
            {
               strAllBas = "";
            }
            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, CommandType.Text, null);
            return intInsertBas + intUpdateBas;
        }
	
        public static int UpdatezhunchuApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            //string strstate = "";
            //int intstate = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');
            int state = 0;
            if (IsPass == "是")
                state = 1;
            else
                state = -1;
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/51/55/tk_PID/54/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货
            string strInsertBas = "update [" + arr[0] + "].." + arr[1] + " set ApprovalMan = '" + UserId + "',ApprovalTime = '" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',"
                    + "Remark = '" + Remark + "',State = '" + state + "' where PID = '" + PID + "' and ApprovalPersons = '" + UserId + "'";
            string strUpdateBas = "";
            if (state == -1)
            {
                if (arr[2].IndexOf("..") > 0)
                {
                    strUpdateBas = "update " + arr[2] + " set State = '" + arr[7] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else if (arr[2].IndexOf("..") > 0 && arr[7] == "29")
                {
                    strUpdateBas = "update " + arr[2] + " set nState = '" + arr[7] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else if (arr[7] == "29")
                {
                    strUpdateBas = "update " + arr[2] + " set nState = '" + arr[7] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else
                {
                    strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[7] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
            }
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            if (strUpdateBas != "")
                intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");

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
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog 准出停止
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog 准出暂停
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog 准出淘汰
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货
            string strAllBas = "";
            if (bol == 0 && state != -1)
            {
                if (arr[2].IndexOf("..") > 0 && arr[4] == "23")
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "',wstate='3' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[2].IndexOf("..") > 0 && arr[4] == "26")
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "',wstate='2' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[2].IndexOf("..") > 0 && arr[4] == "29")
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "',wstate='4' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[2].IndexOf("..") > 0 && arr[4] == "10")
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[2].IndexOf("..") > 0 && arr[4] == "4")
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "',wstate='0' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[2].IndexOf("..") > 0 && arr[4] == "62")
                    strAllBas = "update " + arr[2] + " set nState = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "23")
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "',wstate='3' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "26")
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "',wstate='2' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "29")
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "',wstate='4' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "10")
                {
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else if (arr[4] == "4")
                {
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "',wstate='0' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else
                {
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set nState = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
            }
            else
            {
                strAllBas = "";
            }
            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, "SupplyCnn");
            return intInsertBas + intUpdateBas;
        }
        public static UIDataTable getConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

           SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Data",arr[0]),
                    new SqlParameter("@Table",arr[1]),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCondition", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static DataTable getProductByUnitID(string UnitID)
        {
            //string str = "select distinct ProName from BGOI_Inventory.dbo.tk_ProductInfo where PID in ("
            //        +"select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in ("
            //        +"select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='"+UnitID+"')) and ProTypeID='2'";

            //string str = "select distinct ProName from [BGOI_Inventory]..tk_ProductionOfFinishedGoods a left join [BGOI_Inventory]..tk_ProductInfo b on a.PID = b.PID where UnitID = '" + UnitID + "'";
            string str = "select distinct ProName from BGOI_Inventory.dbo.tk_ProductInfo where PID in(select distinct ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine)";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
        }

        public static DataTable getProductByName(string ProductName, string UnitID)
        {
            //string str = "select ProName,Spec,Units,PID,UnitPrice,Price2 from BGOI_Inventory.dbo.tk_ProductInfo where PID in ("
            //        + "select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in ("
            //        + "select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "')) and ProTypeID='2' and ProName like '%" + ProductName + "%'";
            //string str = "select ProName,a.Spec,Units,PID,UnitPrice,Price2 from (select distinct ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine) b"
            //            +" left join BGOI_Inventory.dbo.tk_ProductInfo a on a.PID=b.ProductID" 
            //            +" where PID in (select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID"
            //            + " in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "'))"
            //            + " and ProTypeID='2' and ProName like '%" + ProductName + "%'";

            string str = " select ProName,replace(a.Spec,' ','') as Spec,Units,PID,UnitPrice,Price2 from  " +
                " (select distinct ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine) b " +
                " left join BGOI_Inventory.dbo.tk_ProductInfo a on a.PID=b.ProductID  " +
                " where ProTypeID='2' and ProName like '%" + ProductName + "%'";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
        }

        public static int InsertTemporary(string ProductName, string Spc, string Pid, string Num, string RelevanceID, string dataT, ref string a_strErr)
        {
            int intInsertBas = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arrName = ProductName.Split(',');
            string[] arrSpc = Spc.Split(',');
            string[] arrPid = Pid.Split(',');
            string[] arrNum = Num.Split(',');

            string strInsertBas = "";
            for (int i = 0; i < arrName.Length; i++)
            {
                string DID = "";
                if (i <= 9)
                    DID = RelevanceID + "-0" + (i + 1);
                else
                    DID = RelevanceID + "-" + (i + 1);
                strInsertBas += "insert into " + dataT + " (RWID,DID,OrderContent,SpecsModels,OrderUnit,OrderNum,DeliveryTime,State,PID) values ('" + RelevanceID + "','" + DID + "','" + arrName[i] + "',"
                            + "'" + arrSpc[i] + "','个','" + arrNum[i] + "','" + DateTime.Now + "','0','" + arrPid[i] + "')";
            }

            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
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
    }
}
