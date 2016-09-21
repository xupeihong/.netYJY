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
    public class SystemPro
    {
        public static DataTable GetAppContent(string data, string table)
        {
            string str = "select SID,Text from ["+data+"].."+table+"";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
        }

        public static DataTable GetConfigCont(string Type)
        {
            string strSql = "select SID,Text from [BGOI_BasMan]..tk_ConfigApp where Type = '" + Type + "'";
            DataTable dt = SQLBase.FillTable(strSql);
            return dt;
        }

        public static DataTable getAppType(string Type)
        {
            string strSql = "select SID,Text from [BGOI_BasMan]..tk_ConfigApp where Type = '" + Type + "' and XID < '3'";
            DataTable dt = SQLBase.FillTable(strSql);
            return dt;
        }

        public static DataTable GetUser()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string unitid = account.UnitID;
            string strSql = "select UserId,UserName,ExJob from [BJOI_UM]..UM_UserNew where DeptId = '" + unitid + "' and ExJob != ''";
            DataTable dt = SQLBase.FillTable(strSql);
            return dt;
        }

        public static UIDataTable getUserGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Where2",where2)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAppUser", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static int InsertExamine(string Butype, string allcontent, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intDelete = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = allcontent.Split('@');
            string strDelete = "delete from [BJOI_UM]..UM_Examine where BuType = '"+Butype+"'";
            string strInsertBas = "";
            for (int i = 0; i < arr.Length; i++)
            {
                string[] brr = arr[i].Split('/');
                strInsertBas += "insert into [BJOI_UM]..UM_Examine (Duty,UserId,BuType,[Level],AppType) values ('"+brr[1]+"','"+brr[0]+"','"+Butype+"','"+brr[2]+"','"+brr[3]+"')";
            }
            try
            {
                if (strDelete != "")
                    intDelete = sqlTrans.ExecuteNonQuery(strDelete, CommandType.Text, null);
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

        public static UIDataTable getExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
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

        public static string getHaveExaminContent(string BuType)
        {   
            string Content = "";
            string strSql = "select a.Duty,a.UserId,b.UserName,a.[Level],SUBSTRING(a.AppType,1,1) as AppTypeS,SUBSTRING(a.AppType,3,1) as Num from [BJOI_UM]..UM_Examine a left join [BJOI_UM]..UM_UserNew b on a.UserId = b.UserId where a.BuType = '" + BuType + "' order by a.level";
            DataTable dt = SQLBase.FillTable(strSql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    string level = "";
                    string levelLast = "";
                    string userName = "";
                    string userNameLast = "";
                    string userid = "";
                    string useridLast = "";
                    string job = "";
                    string jobLast = "";
                    level = dt.Rows[i]["Level"].ToString();
                    levelLast = dt.Rows[i - 1]["Level"].ToString();
                    userName = dt.Rows[i]["UserName"].ToString();
                    userNameLast = dt.Rows[i - 1]["UserName"].ToString();
                    userid = dt.Rows[i]["UserId"].ToString();
                    useridLast = dt.Rows[i - 1]["UserId"].ToString();
                    job = dt.Rows[i]["Duty"].ToString();
                    jobLast = dt.Rows[i - 1]["Duty"].ToString();
                    if (level == levelLast) {
                        string[] strList = levelLast.Split(',');
                        int Bo = Array.IndexOf(strList, level);
                        if (Bo != -1)// 存在
                        {
                            useridLast += "," + userid;
                            jobLast += "," + job;
                            userNameLast += "," + userName;
                        }
                        dt.Rows[i - 1]["UserId"] = useridLast;
                        dt.Rows[i - 1]["Duty"] = jobLast;
                        dt.Rows[i - 1]["UserName"] = userNameLast;
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
                DataTable dt2 = dt;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Content += dt.Rows[i]["Level"].ToString() + "/" + dt.Rows[i]["UserName"].ToString() + "/" + dt.Rows[i]["AppTypeS"].ToString() + "/" + dt.Rows[i]["Num"].ToString() + "/" + dt.Rows[i]["UserId"].ToString() + "/" + dt.Rows[i]["Duty"].ToString() + "@"; 
                }
                Content = Content.TrimEnd('@');
            }
            return Content;
        }
    }
}
