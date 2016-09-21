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
    public class SystemManageDAL
    {
        public static UIDataTable getBasMangeGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getBasManage", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static DataTable GetConfigContent()
        {
            string strSql = "select distinct b.Type, b.TypeDesc as ss from tk_ConfigContent b";
            strSql += "  left join	 tk_ConfigReation a on	 a.type=b.type ";
            strSql += "  left join	  tk_ConfigAgentClass c on	 c.type=b.type";
            strSql += " left join	  tk_ConfigQualityStandard d on	 d.type=b.type";
            strSql += "  left join	  tk_ConfigScalType f on   f.type=b.type";
            strSql += " left join	  tk_ConfigBillWay g on   g.type=b.type ";
            strSql += " left join	  tk_ConfigSupUnit h on   h.type=b.type  ";
            strSql += " where  b.Type='Ptype'  or b.Type='SupplierType'or b.Type='BusinessDistribute'";
            strSql += " or g.Type='BillingWay'or b.Type='FDepartment'or b.Type='Department'or b.Type='Job'or b.Type='FileType'or b.Type='FFileName'or b.Type='COMCtry'";
            strSql += " or b.Type='COMArea'or b.Type='EnterpriseType'or b.Type='HasImportMaterial' or b.Type='TypeO'";
            strSql += " or b.Type='CapitalUnit'or b.Type='Opinions'or b.Type='csCType'or b.Type='ChargeUser'or b.Type='TypeO'";
            strSql += "  or b.Type='Result'";
            strSql += "  or a.Type='Relation'or c.Type='AgentClass'or d.Type='CPZLZXBZ' or f.Type='ScaleType'";
            strSql += "  or	b.type='BankName'or b.type='csState' or h.Type='UnReviewUnit' ";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static int InsertContent(string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            int intContent = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string XID = PreGetTaskNo(type);
            string strSql = "select SID,TypeDesc from tk_ConfigContent where Type='" + type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            string SID = "";
            string TypeDesc = "";
            Acc_Account acc = GAccount.GetAccountInfo();

            if (dt.Rows.Count > 0)
            {
                SID = dt.Rows[0]["SID"].ToString();
                SID = SID.Substring(0, SID.Length - 1);
                SID = SID + (Convert.ToInt32(XID) - 1).ToString();
                TypeDesc = dt.Rows[0]["TypeDesc"].ToString();
            }
            string strInsertOrder = "";
            string strConte = "";
            //在此处判断
            if (type == "BusinessDistribute")
            {
                strInsertOrder += "insert into tk_BusinessDistribute (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
                strConte += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            }
            else if (type == "BillingWay")
            {
                strInsertOrder += "insert into tk_ConfigBillWay (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
                strConte += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            }
            else if (type == "CPZLZXBZ")
            {
                strInsertOrder += "insert into tk_ConfigQualityStandard (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
                strConte += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            }
            else if (type == "Relation")
            {
                strInsertOrder += "insert into tk_ConfigReation (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
                strConte += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            }
            else if (type == "ScaleType")
            {
                strInsertOrder += "insert into tk_ConfigScalType (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
                strConte += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            }
            else
                strInsertOrder += "insert into tk_ConfigContent (XID,SID,Text,Type,TypeDesc,Validate,UnitID) values ('" + XID + "','" + SID + "','" + text + "','" + type + "','" + TypeDesc + "','v','" + acc.UnitID + "')";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strConte != "")
                    intContent = sqlTrans.ExecuteNonQuery(strConte, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return (intInsert + intContent);
        }
        public static string PreGetTaskNo(string sel)
        {
            string strID = "";
            string xid = "";
            string strSqlID = "select max(XID) from tk_ConfigContent where Type='" + sel + "'";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SupplyCnn");
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
        public static int DeleteContent(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            int intup = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strup = "";
            string strInsertOrder = "";
            if (type == "BusinessDistribute")
            {
                strup += "update tk_BusinessDistribute set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "BillingWay")
            {
                strup += "update tk_ConfigBillWay set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "CPZLZXBZ")
            {
                strup += "update tk_ConfigQualityStandard set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "Relation")
            {
                strup += "update tk_ConfigReation set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "ScaleType")
            {
                strup += "update tk_ConfigScalType set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else
                strInsertOrder += "update tk_ConfigContent set Validate = 'i' where XID = '" + xid + "' and Type = '" + type + "'";

            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strup != "")
                    intup = sqlTrans.ExecuteNonQuery(strup, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return (intInsert + intup);
        }
        public static int UpdateContent(string xid, string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            int intUP = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertOrder = "";
            string strup = "";
            //判断类型
            if (type == "BusinessDistribute")
            {
                strInsertOrder += "update  tk_BusinessDistribute  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
                strup += "update  tk_ConfigContent  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "BillingWay")
            {
                strInsertOrder += "update  tk_ConfigBillWay  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
                strup += "update  tk_ConfigContent  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "CPZLZXBZ")
            {
                strInsertOrder += "update  tk_ConfigQualityStandard  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
                strup += "update  tk_ConfigContent  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "Relation")
            {
                strInsertOrder += "update  tk_ConfigReation  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
                strup += "update  tk_ConfigContent  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else if (type == "ScaleType")
            {
                strInsertOrder += "update  tk_ConfigScalType  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
                strup += "update  tk_ConfigContent  set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            }
            else
                strInsertOrder += "update tk_ConfigContent set Text = '" + text + "' where XID = '" + xid + "' and Type = '" + type + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                if (strup != "")
                    intUP = sqlTrans.ExecuteNonQuery(strup, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return (intInsert + intUP);
        }

    }
}
