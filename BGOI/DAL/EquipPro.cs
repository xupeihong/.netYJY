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
    public class EquipPro
    {
        public static DataTable GetConfigCont(string Type)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string strSql = "select SID,Text from tk_ConfigContent where Type = '" + Type + "' and Validate = 'v' and UnitID = '" + account.UnitID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static DataTable getState()
        {
            string strSql = "select StateId,name from tk_ConfigState where Type = 'Equip'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static DataTable GetConfigContByUnit(string UnitID)
        {
            string strSql = "select SID,Text from tk_ConfigBussinessType where Unit like '%," + UnitID + ",%'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static int InsertDeviceBas(DevicsBas Bas, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<DevicsBas>(Bas, "tk_DevicsBas");

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

        public static UIDataTable getEquipGrid(int a_intPageSize, int a_intPageIndex, string where, string order, string unit)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",order),
                    new SqlParameter("@UnitID",unit)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getEquipNew", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            DataTable dtOrder = DO_Order.Tables[0];
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            dtOrder.Columns.Add(c);
            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                dtOrder.Rows[i]["xu"] = (i + 1);
            }
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

        public static UIDataTable getStandingBookGrid(int a_intPageSize, int a_intPageIndex, string where, string Order)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",Order)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getStandingBook", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            DataTable dtOrder = DO_Order.Tables[0];
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            dtOrder.Columns.Add(c);
            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                dtOrder.Rows[i]["xu"] = (i + 1);
            }
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

        public static UIDataTable getTracingGrid(int a_intPageSize, int a_intPageIndex, string where, string Order)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",Order)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getTracingNew", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            DataTable dtOrder = DO_Order.Tables[0];
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            dtOrder.Columns.Add(c);
            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                dtOrder.Rows[i]["xu"] = (i + 1);
            }
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

        public static UIDataTable getDevicsBasGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getDCheckInfo", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            DataTable dtOrder = DO_Order.Tables[0];
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            dtOrder.Columns.Add(c);
            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                dtOrder.Rows[i]["xu"] = (i + 1);
            }
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

        public static DCheckInfo getUpdateDCheckInfo(string id)
        {
            DCheckInfo Check = new DCheckInfo();
            string strSql = "select * from tk_DCheckInfo where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Check.StrECode = dt.Rows[0]["ECode"].ToString();
                Check.StrCheckDate = dt.Rows[0]["CheckDate"].ToString();
                Check.StrCheckWay = dt.Rows[0]["CheckWay"].ToString();
                Check.StrCheckCompany = dt.Rows[0]["CheckCompany"].ToString();
                Check.StrCharge = GFun.SafeToFloat(dt.Rows[0]["Charge"].ToString());
                Check.StrPrecision = dt.Rows[0]["Precision"].ToString();
                Check.StrCensorshipRemark = dt.Rows[0]["CensorshipRemark"].ToString();
                Check.StrPrincipal = dt.Rows[0]["Principal"].ToString();
                Check.StrCalibrationResults = dt.Rows[0]["CalibrationResults"].ToString();
            }
            return Check;
        }

        public static DevicsBas getDevicsByID(string id)
        {
            DevicsBas Bas = new DevicsBas();
            string str = "select * from tk_DevicsBas where ECode = '" + id + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Bas.StrControlCode = dt.Rows[0]["ControlCode"].ToString();
                Bas.StrEname = dt.Rows[0]["Ename"].ToString();
                Bas.StrManufacturer = dt.Rows[0]["Manufacturer"].ToString();
                Bas.StrFactoryNumber = dt.Rows[0]["FactoryNumber"].ToString();
                Bas.StrSpecification = dt.Rows[0]["Specification"].ToString();
                Bas.StrDevicsType = dt.Rows[0]["DevicsType"].ToString();
                if (dt.Rows[0]["FactoryDate"].ToString() != "")
                    Bas.StrFactoryDate = Convert.ToDateTime(dt.Rows[0]["FactoryDate"]).ToString("yyyy-MM-dd");
                Bas.StrPrecision = dt.Rows[0]["Precision"].ToString();
                Bas.StrTracingType = dt.Rows[0]["TracingType"].ToString();
                Bas.StrClrange = dt.Rows[0]["Clrange"].ToString();
                Bas.StrCycleType = dt.Rows[0]["CycleType"].ToString();
                Bas.StrCycle = dt.Rows[0]["Cycle"].ToString();
                Bas.StrCheckCompany = dt.Rows[0]["CheckCompany"].ToString();
                Bas.StrRemark = dt.Rows[0]["Remark"].ToString();
            }
            return Bas;
        }

        public static int UpdateDeviceBas(DevicsBas Bas, string ecode, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_DevicsBas set ControlCode = '" + Bas.StrControlCode + "',Ename = '" + Bas.StrEname + "',Manufacturer = '" + Bas.StrManufacturer + "',"
                              + "FactoryNumber = '" + Bas.StrFactoryNumber + "',Specification = '" + Bas.StrSpecification + "',DevicsType = '" + Bas.StrDevicsType + "',"
                              + "FactoryDate = '" + Bas.StrFactoryDate + "',TracingType = '" + Bas.StrTracingType + "',Precision = '" + Bas.StrPrecision + "',"
                              + "Clrange = '" + Bas.StrClrange + "',CycleType = '" + Bas.StrCycleType + "',Cycle = '" + Bas.StrCycle + "',"
                              + "CheckCompany = '" + Bas.StrCheckCompany + "',Remark = '" + Bas.StrRemark + "'"
                              + " where ECode = '" + ecode + "'";

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

        public static int DeleteDeviceBas(string ecode, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_DevicsBas set Validate = 'i' where ECode = '" + ecode + "'";

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

        public static int InsertDCheckInfo(DCheckInfo CheckInfo, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<DCheckInfo>(CheckInfo, "tk_DCheckInfo");
            string strUpdate = "";
            DateTime CheckDate = Convert.ToDateTime(CheckInfo.StrCheckDate);
            DateTime PlanDate = new DateTime();
            string strSql = "";
            strSql = "select CycleType,Cycle from tk_DevicsBas where ECode = '" + CheckInfo.StrECode + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            string CycelType = dt.Rows[0]["CycleType"].ToString();
            int Cycel = Convert.ToInt16(dt.Rows[0]["Cycle"]);
            if (CycelType == "Cy1")
            {
                PlanDate = CheckDate.AddYears(Cycel);
            }
            if (CycelType == "Cy2")
            {
                PlanDate = CheckDate.AddMonths(Cycel);
            }
            if (CycelType == "Cy3")
            {
                PlanDate = CheckDate.AddDays(Cycel);
            }
            if (CheckInfo.StrPrecision != null)
                strUpdate = "update tk_DevicsBas set LastDate = '" + CheckInfo.StrCheckDate + "',PlanDate = '" + PlanDate.ToString("yyyy-MM-dd") + "',Precision = '" + CheckInfo.StrPrecision + "' where ECode = '" + CheckInfo.StrECode + "'";
            else
                strUpdate = "update tk_DevicsBas set LastDate = '" + CheckInfo.StrCheckDate + "',PlanDate = '" + PlanDate.ToString("yyyy-MM-dd") + "' where ECode = '" + CheckInfo.StrECode + "'";



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

        public static int UpdateDCheckInfo(DCheckInfo CheckInfo, string id, string num, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_DCheckInfo set CheckDate = '" + CheckInfo.StrCheckDate + "',CheckWay = '" + CheckInfo.StrCheckWay + "',CheckCompany = '" + CheckInfo.StrCheckCompany + "',Charge = '" + CheckInfo.StrCharge + "',"
            + "Precision = '" + CheckInfo.StrPrecision + "',CensorshipRemark = '" + CheckInfo.StrCensorshipRemark + "',Principal = '" + CheckInfo.StrPrincipal + "',CalibrationResults = '" + CheckInfo.StrCalibrationResults + "' where ID = '" + id + "'";
            string strUpdate = "";
            if (num == "1")
            {
                DateTime CheckDate = Convert.ToDateTime(CheckInfo.StrCheckDate); DateTime PlanDate = new DateTime();
                string strSql = "";
                if (CheckInfo.StrCheckWay == "CK1")
                {
                    strSql = "select CalibrationCycleType,CalibrationCycle from tk_DevicsBas where ECode = '" + CheckInfo.StrECode + "'";
                    DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
                    string CycelType = dt.Rows[0]["CalibrationCycleType"].ToString();
                    int Cycel = Convert.ToInt16(dt.Rows[0]["CalibrationCycle"]);
                    if (CycelType == "Cy1")
                    {
                        PlanDate = CheckDate.AddYears(Cycel);
                    }
                    if (CycelType == "Cy2")
                    {
                        PlanDate = CheckDate.AddMonths(Cycel);
                    }
                    if (CycelType == "Cy3")
                    {
                        PlanDate = CheckDate.AddDays(Cycel);
                    }
                    if (CheckInfo.StrPrecision != null)
                        strUpdate = "update tk_DevicsBas set LastCalibrationDate = '" + CheckInfo.StrCheckDate + "',PlanCalibrationDate = '" + PlanDate.ToString("yyyy-MM-dd") + "',Precision = '" + CheckInfo.StrPrecision + "' where ECode = '" + CheckInfo.StrECode + "'";
                    else
                        strUpdate = "update tk_DevicsBas set LastCalibrationDate = '" + CheckInfo.StrCheckDate + "',PlanCalibrationDate = '" + PlanDate.ToString("yyyy-MM-dd") + "' where ECode = '" + CheckInfo.StrECode + "'";
                }
                if (CheckInfo.StrCheckWay == "CK2")
                {
                    strSql = "select CheckCycleType,CheckCycle from tk_DevicsBas where ECode = '" + CheckInfo.StrECode + "'";
                    DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
                    string CycelType = dt.Rows[0]["CheckCycleType"].ToString();
                    int Cycel = Convert.ToInt16(dt.Rows[0]["CheckCycle"]);
                    if (CycelType == "Cy1")
                    {
                        PlanDate = CheckDate.AddYears(Cycel);
                    }
                    if (CycelType == "Cy2")
                    {
                        PlanDate = CheckDate.AddMonths(Cycel);
                    }
                    if (CycelType == "Cy3")
                    {
                        PlanDate = CheckDate.AddDays(Cycel);
                    }
                    if (CheckInfo.StrPrecision != null)
                        strUpdate = "update tk_DevicsBas set LastCheckDate = '" + CheckInfo.StrCheckDate + "',PlanCheckDate = '" + PlanDate.ToString("yyyy-MM-dd") + "',Precision = '" + CheckInfo.StrPrecision + "' where ECode = '" + CheckInfo.StrECode + "'";
                    else
                        strUpdate = "update tk_DevicsBas set LastCheckDate = '" + CheckInfo.StrCheckDate + "',PlanCheckDate = '" + PlanDate.ToString("yyyy-MM-dd") + "' where ECode = '" + CheckInfo.StrECode + "'";
                }
                if (CheckInfo.StrCheckWay == "CK3")
                {
                    strSql = "select TestCycleType,TestCycle from tk_DevicsBas where ECode = '" + CheckInfo.StrECode + "'";
                    DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
                    string CycelType = dt.Rows[0]["TestCycleType"].ToString();
                    int Cycel = Convert.ToInt16(dt.Rows[0]["TestCycle"]);
                    if (CycelType == "Cy1")
                    {
                        PlanDate = CheckDate.AddYears(Cycel);
                    }
                    if (CycelType == "Cy2")
                    {
                        PlanDate = CheckDate.AddMonths(Cycel);
                    }
                    if (CycelType == "Cy3")
                    {
                        PlanDate = CheckDate.AddDays(Cycel);
                    }
                    if (CheckInfo.StrPrecision != null)
                        strUpdate = "update tk_DevicsBas set LastTestDate = '" + CheckInfo.StrCheckDate + "',PlanTestDate = '" + PlanDate.ToString("yyyy-MM-dd") + "',Precision = '" + CheckInfo.StrPrecision + "' where ECode = '" + CheckInfo.StrECode + "'";
                    else
                        strUpdate = "update tk_DevicsBas set LastTestDate = '" + CheckInfo.StrCheckDate + "',PlanTestDate = '" + PlanDate.ToString("yyyy-MM-dd") + "' where ECode = '" + CheckInfo.StrECode + "'";
                }
            }

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

            return intInsert;
        }

        public static DataTable getPrintEquip(string where, string whereOrder)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",whereOrder)
                };

            DataTable DO_Order = SQLBase.FillTable("getPrintEquipNew", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            DO_Order.Columns.Add(c);
            for (int i = 0; i < DO_Order.Rows.Count; i++)
            {
                DO_Order.Rows[i]["xu"] = (i + 1);
            }
            return DO_Order;
        }

        public static DataTable getPrintTracing(string where, string whereOrder)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",whereOrder)
                };

            DataTable DO_Order = SQLBase.FillTable("getPrintTracingNew", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            DO_Order.Columns.Add(c);
            for (int i = 0; i < DO_Order.Rows.Count; i++)
            {
                DO_Order.Rows[i]["xu"] = (i + 1);
            }
            return DO_Order;
        }

        public static DataTable getPrintStanding(string where, string whereOrder)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",whereOrder)
                };

            DataTable DO_Order = SQLBase.FillTable("getPrintStanding", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            DataColumn c = new DataColumn();
            c.ColumnName = "xu";
            DO_Order.Columns.Add(c);
            for (int i = 0; i < DO_Order.Rows.Count; i++)
            {
                DO_Order.Rows[i]["xu"] = (i + 1);
            }
            return DO_Order;
        }

        public static int InsertCongTime(string checkWay, string num, string unit, string TimeType, ref string a_strErr)
        {
            int intdelete = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strSql = "select * from tk_ConfigWarnTime where Unit = '" + unit + "' and Type = '" + checkWay + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            string strdelete = "";
            if (dt.Rows.Count > 0)
            {
                strdelete = "delete from tk_ConfigWarnTime where Unit = '" + unit + "' and Type = '" + checkWay + "'";
            }
            string strInsert = "Insert into tk_ConfigWarnTime (num,Type,TimeType,Unit) values ('" + num + "','" + checkWay + "','" + TimeType + "','" + unit + "')";

            try
            {
                if (strdelete != "")
                    intdelete = sqlTrans.ExecuteNonQuery(strdelete, CommandType.Text, null);
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

        public static string getCK1Time()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CK' and TimeType = 'Device'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");

            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }

        public static string getCK2Time()
        {
            string CK2time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CK2' and TimeType = 'Device'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK2time = dt.Rows[0]["num"].ToString();
            return CK2time;
        }

        public static string getCK3Time()
        {
            string CK3time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CK3' and TimeType = 'Device'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK3time = dt.Rows[0]["num"].ToString();
            return CK3time;
        }

        public static int InsertDRepairInfo(DRepairInfo Repair, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<DRepairInfo>(Repair, "tk_DRepairInfo");
            string strUpdate = "update tk_DevicsBas set state = '2' where ECode = '" + Repair.StrECode + "'";

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

        public static int UpdateDRepairInfo(DRepairInfo Repair, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_DRepairInfo set ServiceRecord = '" + Repair.StrServiceRecord + "',ServiceResults = '" + Repair.StrServiceResults + "',ReturnTime = '" + Repair.StrReturnTime + "',Remark = '" + Repair.StrRemark + "' where ECode = '" + Repair.StrECode + "'";
            string strUpdate = "update tk_DevicsBas set state = '0' where ECode = '" + Repair.StrECode + "'";

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

        public static int InsertDScrapInfo(DScrapInfo Scrap, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<DScrapInfo>(Scrap, "tk_DScrapInfo");
            string strUpdate = "update tk_DevicsBas set state = '-1' where ECode = '" + Scrap.StrECode + "'";

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

        public static int InsertRativeSource(RativeSource Rsource, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<RativeSource>(Rsource, "tk_RativeSource");

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

        public static UIDataTable getRativeSourceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getRativeSource", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static RativeSource getUpdateRative(string id)
        {
            RativeSource Rsource = new RativeSource();
            string strSql = "select * from tk_RativeSource where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Rsource.StrRID = dt.Rows[0]["RID"].ToString();
                Rsource.StrEquipID = dt.Rows[0]["EquipID"].ToString();
                Rsource.StrProModel = dt.Rows[0]["ProModel"].ToString();
                Rsource.StrSource = dt.Rows[0]["Source"].ToString();
                Rsource.StrManufacturer = dt.Rows[0]["Manufacturer"].ToString();
                Rsource.StrNominal = dt.Rows[0]["Nominal"].ToString();
                Rsource.StrSourceNumber = dt.Rows[0]["SourceNumber"].ToString();
            }
            return Rsource;
        }

        public static int UpdateRativeSource(RativeSource Rsource, string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_RativeSource set RID = '" + Rsource.StrRID + "',EquipID = '" + Rsource.StrEquipID + "',ProModel = '" + Rsource.StrProModel + "',"
                + "Source = '" + Rsource.StrSource + "',Manufacturer = '" + Rsource.StrManufacturer + "',Nominal = '" + Rsource.StrNominal + "',SourceNumber = '" + Rsource.StrSourceNumber + "' where ID = '" + id + "'";

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

        public static int DeleteRative(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_RativeSource set Validate = 'i' where ID = '" + id + "'";

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
    }
}
