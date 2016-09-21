using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class FlowSystemPro
    {
        // 
        public static System.Data.DataTable GetBasicContent()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string strSql = "select distinct Type as SID,TypeDesc as Text from tk_ConfigContent where validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        public static UIDataTable getBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBasic", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 确认新增
        public static int InsertBasic(string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string XID = PreGetTaskNo(type);
            string strSql = "select SID,TypeDesc from tk_ConfigContent where Type='" + type + "' and validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            string SID = "";
            string TypeDesc = "";
            if (dt.Rows.Count > 0)
            {
                SID = dt.Rows[0]["SID"].ToString();
                SID = SID.Substring(0, SID.Length - 1);
                SID = SID + XID;
                TypeDesc = dt.Rows[0]["TypeDesc"].ToString();
            }
            string strInsertOrder = "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate) ";
            strInsertOrder += " values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v') ";
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

        private static string PreGetTaskNo(string Sel)
        {
            string strID = "";
            string xid = "";
            string strSqlID = "  select max(XID) from tk_ConfigContent where Type='" + Sel + "' and validate='v' ";
            DataTable dtID = SQLBase.FillTable(strSqlID, "FlowMeterDBCnn");
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();

                int num = Convert.ToInt32(strID);
                num = num + 1;

                xid = num.ToString();
            }
            else
            {
                xid = "1";
            }
            return xid;
        }

        // 确认修改 
        public static int ModifyBasic(string XID, string Type, string Text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertOrder = "update tk_ConfigContent ";
            strInsertOrder += " set Text = '" + Text + "' where XID = '" + XID + "' and Type = '" + Type + "' and validate='v' ";
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

        // 删除
        public static int DeleteBasic(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strUpdateOrder = "update tk_ConfigContent set Validate = 'i' ";
            strUpdateOrder += " where XID = '" + xid + "' and Type = '" + type + "' and validate='v' ";
            try
            {
                if (strUpdateOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strUpdateOrder, CommandType.Text, null);

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



        // 确认新增小组
        public static int InsertGroup(string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");
            string strSql = "select top 1 GroupID,DeptId from UM_Group  where Validate='v' order by GroupID desc ";
            DataTable dt = SQLBase.FillTable(strSql, "AccountCnn");
            int GroupID = 0;
            string DeptID = "";
            if (dt.Rows.Count > 0)
            {
                GroupID = Convert.ToInt32(dt.Rows[0]["GroupID"].ToString()) + 1;
                DeptID = dt.Rows[0]["DeptId"].ToString();
            }
            else
            {
                GroupID = 1;
                Acc_Account acc = GAccount.GetAccountInfo();
                DeptID = acc.UnitID;

            }
            string strInsertOrder = "insert into UM_Group (GroupID,GroupName,DeptId,Validate) ";
            strInsertOrder += " values ('" + GroupID + "','" + text + "','" + DeptID + "','v') ";
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

        // 删除小组
        public static int DeleteGroup(string gid, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");

            string strInsertOrder = "update UM_Group set Validate = 'i' ";
            strInsertOrder += " where GroupID = '" + gid + "' and validate='v' ";
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

        // 确认修改 
        public static int ModifyGroup(string GroupID, string Text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");

            string strInsertOrder = "update UM_Group ";
            strInsertOrder += " set GroupName = '" + Text + "' where GroupID = '" + GroupID + "' and Validate='v' ";
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

        // 确认修改 小组人员
        public static int ModifyGroupUser(string UserID, string Text, string GroupID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");

            string strInsertOrder = "update UM_Person ";
            strInsertOrder += " set UserName = '" + Text + "' where GroupID = '" + GroupID + "' and UserID='" + UserID + "' and Validate='v' ";
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

        // 删除小组人员
        public static int DeleteGroupUser(string uid, string gid, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");

            string strUpdateOrder = "update UM_Person set Validate = 'i' ";
            strUpdateOrder += " where GroupID = '" + gid + "' and UserID='" + uid + "' and validate='v' ";
            try
            {
                if (strUpdateOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strUpdateOrder, CommandType.Text, null);

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

        // 确认新增小组人员
        public static int InsertGroupUser(string text, string GroupID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("AccountCnn");
            string strSql = "select max(UserID) UserID from UM_Person where Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "AccountCnn");
            string UserID = "U";
            if (dt.Rows.Count > 0)
            {
                string strID = dt.Rows[0]["UserID"].ToString();
                strID = strID.Substring(1, strID.Length - 1);
                int IDs = Convert.ToInt32(strID) + 1;
                int length = 7 - IDs.ToString().Length;
                for (int i = 0; i < length; i++)
                {
                    UserID += "0";
                }
                UserID += IDs.ToString();
            }
            else
            {
                UserID = "U0000001";
            }
            string strInsertOrder = "insert into UM_Person (UserID,UserName,GroupID,Validate) ";
            strInsertOrder += " values ('" + UserID + "','" + text + "','" + GroupID + "','v') ";
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


        public static UIDataTable getGroupList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getGroupList1", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    dtOrder.Rows[i]["ID"] = i + 1;
                }
            }
            instData.DtData = dtOrder;
            return instData;
        }

        public static UIDataTable getPersonList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getPersonList1", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

            if (dtOrder != null && dtOrder.Rows.Count > 0) {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    dtOrder.Rows[i]["ID"] = i + 1;
                }
            }
            instData.DtData = dtOrder;
            return instData;
        }

    }
}
