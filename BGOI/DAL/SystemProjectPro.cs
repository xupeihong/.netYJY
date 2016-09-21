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
    public class SystemProjectPro
    {
        public static DataTable GetConfigContent()
        {
            string strSql = "select distinct Type,TypeDesc as ss from tk_ConfigContent where Type='Psource' or Type='Design' or Type='PayType' or Type = 'JQType'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
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

            DataSet DO_Order = SQLBase.FillDataSet("getBasic", CommandType.StoredProcedure, sqlPar, "MainProject");
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

        public static int InsertContent(string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");
            string XID = PreGetTaskNo(type);
            string strSql = "select SID,TypeDesc from tk_ConfigContent where Type='" + type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainProject");
            string SID = "";
            string TypeDesc = "";
            if (dt.Rows.Count > 0)
            {
                SID = dt.Rows[0]["SID"].ToString();
                SID = SID.Substring(0, SID.Length - 1);
                SID = SID + XID;
                TypeDesc = dt.Rows[0]["TypeDesc"].ToString();
            }
            string strInsertOrder = "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v')";
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

        public static string PreGetTaskNo(string Sel)
        {
            string strID = "";
            string xid = "";
            string strSqlID = "select max(XID) from tk_ConfigContent where Type='" + Sel + "'";
            DataTable dtID = SQLBase.FillTable(strSqlID, "MainProject");
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

        public static int UpdateContent(string xid, string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_ConfigContent set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
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

        public static int DeleteContent(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainProject");

            string strInsertOrder = "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
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
    }
}
