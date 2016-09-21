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
    public class UserAptitudePro
    {
        public static UIDataTable getUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getUser", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static int InsertUserAptitude(UserAptitude Uap, byte[] fileByte, ref string a_strErr)
        {
            int intInsert = 0;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            //string strInsert = GSqlSentence.GetInsertInfoByD<UserAptitude>(Uaptitude, "tk_UserAptitude");
            string strInsert = "insert into tk_UserAptitude (UserID,UserName,BusinessType,TecoName,TecoClass,GetTime,CertificatCode,CertificateName,"
            + "CertificateUnit,LastCertificatDate,CertificatDate,FileName,FileInfo,Remark,Unit,State,CreateTime,CreateUser,Validate) values ("
            + "'" + Uap.StrUserID + "','" + Uap.StrUserName + "','" + Uap.StrBusinessType + "','" + Uap.StrTecoName + "','" + Uap.StrTecoClass + "','" + Uap.StrGetTime + "',"
            + "'" + Uap.StrCertificatCode + "','" + Uap.StrCertificateName + "','" + Uap.StrCertificateUnit + "','" + Uap.StrLastCertificatDate + "','" + Uap.StrCertificatDate + "',"
            + "'" + Uap.StrFileName + "',@fileByte,'" + Uap.StrRemark + "','" + Uap.StrUnit + "','" + Uap.StrState + "','" + Uap.StrCreateTime + "','" + Uap.StrCreateUser + "','" + Uap.StrValidate + "')";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, para);
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

        public static UIDataTable getAptitudeGrid(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@UnitID",unitid)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAptitude", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static UIDataTable getAptitudeUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAptitudeUser", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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

        public static UserAptitude getUpdateUserAptitude(string id)
        {
            UserAptitude Uap = new UserAptitude();
            string strSql = "select * from tk_UserAptitude where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                Uap.StrUserID = dt.Rows[0]["UserID"].ToString();
                Uap.StrUserName = dt.Rows[0]["UserName"].ToString();
                Uap.StrBusinessType = dt.Rows[0]["BusinessType"].ToString();
                Uap.StrTecoName = dt.Rows[0]["TecoName"].ToString();
                Uap.StrTecoClass = dt.Rows[0]["TecoClass"].ToString();
                Uap.StrGetTime = Convert.ToDateTime(dt.Rows[0]["GetTime"]).ToString("yyyy-MM-dd");
                Uap.StrCertificatCode = dt.Rows[0]["CertificatCode"].ToString();
                Uap.StrCertificateName = dt.Rows[0]["CertificateName"].ToString();
                Uap.StrCertificateUnit = dt.Rows[0]["CertificateUnit"].ToString();
                Uap.StrLastCertificatDate = Convert.ToDateTime(dt.Rows[0]["LastCertificatDate"]).ToString("yyyy-MM-dd");
                Uap.StrCertificatDate = Convert.ToDateTime(dt.Rows[0]["CertificatDate"]).ToString("yyyy-MM-dd");
                Uap.StrFileName = dt.Rows[0]["FileName"].ToString();
                Uap.StrRemark = dt.Rows[0]["Remark"].ToString();
            }
            return Uap;
        }

        public static DataTable getFile(string id)
        {
            string strSql = "select ID,FileName,FileInfo from tk_UserAptitude where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static int deleteFile(string id, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "update tk_UserAptitude set FileName = NULL,FileInfo = NULL where ID = '" + id + "'";

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

        public static int UpdateUserAptitude(string ID, UserAptitude Uap, byte[] fileByte, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intInsertHis = 0;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertHis = "insert into tk_UserAptitudeHis (UserID,UserName,BusinessType,TecoName,TecoClass,GetTime,CertificatCode,CertificateName,CertificateUnit,"
            + "LastCertificatDate,CertificatDate,FileName,FileInfo,Remark,Unit,State,CreateTime,CreateUser,Validate"
            + ") select UserID,UserName,BusinessType,TecoName,TecoClass,GetTime,CertificatCode,CertificateName,CertificateUnit,LastCertificatDate,"
            + "CertificatDate,FileName,FileInfo,Remark,Unit,State,CreateTime,CreateUser,Validate from tk_UserAptitude where ID = '" + ID + "'";

            string strInsert = "update tk_UserAptitude set BusinessType = '" + Uap.StrBusinessType + "',TecoName = '" + Uap.StrTecoName + "',TecoClass = '" + Uap.StrTecoClass + "',"
                + "GetTime = '" + Uap.StrGetTime + "',CertificatCode = '" + Uap.StrCertificatCode + "',CertificateName = '" + Uap.StrCertificateName + "',CertificateUnit = '" + Uap.StrCertificateUnit + "',"
                + "LastCertificatDate = '" + Uap.StrLastCertificatDate + "',CertificatDate = '" + Uap.StrCertificatDate + "',Remark = '" + Uap.StrRemark + "' where ID = '" + ID + "'";

            string strUpdate = "";
            if (Uap.StrFileName != null)
                strUpdate = "update tk_UserAptitude set FileName = '" + Uap.StrFileName + "',FileInfo = @fileByte where ID = '" + ID + "'";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, para);
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

        public static int InsertLendAptitude(UCertificatLend Lend, ref string a_strErr)
        {
            int intInsert = 0;
            int intupdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsert = GSqlSentence.GetInsertInfoByD<UCertificatLend>(Lend, "tk_UCertificatLend");
            string strUpdate = "update tk_UserAptitude set State = '1' where ID = '" + Lend.StrID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    intupdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intupdate;
        }

        public static int UpdateLendAptitude(UCertificatLend Lend, ref string a_strErr)
        {
            int intInsert = 0;
            int intupdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsert = "update tk_UCertificatLend set ReturnDate = '" + Lend.StrReturnDate + "' where ID = '" + Lend.StrID + "'";
            string strUpdate = "update tk_UserAptitude set State = '0' where ID = '" + Lend.StrID + "'";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    intupdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intupdate;
        }

        public static string getAptitudeTime()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'AptitudeWarn' and TimeType = 'Aptitude'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }
        
    }
}
