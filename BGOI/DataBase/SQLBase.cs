using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TECOCITY_BGOI
{
    public abstract class SQLBase
    {
        private static readonly String m_strCnn = System.Configuration.ConfigurationManager.ConnectionStrings["MainDBCnn"].ConnectionString;

        private SQLBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static String ConnectionString
        {
            get { return m_strCnn; }
        }

        public static SqlConnection CreateConnecion()
        {
            return new SqlConnection(m_strCnn);
        }

        public static SqlConnection CreateConnecion(String strCnnConfig)
        {
            String strCnn = System.Configuration.ConfigurationManager.ConnectionStrings[strCnnConfig].ConnectionString;
            return new SqlConnection(strCnn);
        }

        public static SqlCommand PrepareCommand(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdType;

            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
                //cmd.Parameters.Clear();
                cmd.Parameters.AddRange(cmdParms);

            return cmd;
        }

        #region 数据库相关操作


        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="table">需填充的DataTable</param>
        /// <returns>返回DataTable中数据行数； 返回-1时查询命令出错</returns>
        public static int FillTable(String strSql, DataTable table, SqlConnection cnn)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                cnn.Open();
                da.SelectCommand = new SqlCommand(strSql, cnn);

                nRow = da.Fill(table);
            }
            catch (SqlException e)
            {
                GLog.LogError("FillTable", e);
            }
            catch (Exception e)
            {
                GLog.LogError("FillTable", e);
            }
            finally
            {
                da.Dispose();

                cnn.Close();
                cnn.Dispose();
            }

            return nRow;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回填充的DataTable；返回null时查询命令出错</returns>
        public static DataTable FillTable(String strSql, SqlConnection cnn)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, dt, cnn) == -1) return null;
            return dt;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <param name="table">需填充的DataTable</param>
        /// <returns>返回DataTable中数据行数； 返回-1时查询命令出错</returns>
        public static int FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataTable table, SqlConnection cnn)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                cnn.Open();
                da.SelectCommand = PrepareCommand(cnn, null, cmdType, strSql, cmdParms);

                nRow = da.Fill(table);
            }
            catch (SqlException e)
            {
                GLog.LogError("FillTable", e);
            }
            finally
            {
                da.Dispose();

                cnn.Close();
                cnn.Dispose();
            }

            return nRow;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <returns>返回填充的DataTable；返回null时查询命令出错</returns>
        public static DataTable FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, SqlConnection cnn)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, cmdType, cmdParms, dt, cnn) == -1) return null;
            return dt;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataSet
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <param name="table">需填充的DataSet</param>
        /// <returns>返回DataSet中数据行数； 返回-1时查询命令出错</returns>
        public static int FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataSet dataset, SqlConnection cnn)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                cnn.Open();
                da.SelectCommand = PrepareCommand(cnn, null, cmdType, strSql, cmdParms);

                nRow = da.Fill(dataset);
            }
            catch (SqlException e)
            {
                GLog.LogError("FillDataSet", e);
            }
            finally
            {
                da.Dispose();

                cnn.Close();
                cnn.Dispose();
            }

            return nRow;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataSet
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <returns>返回填充的DataSet；返回null时查询命令出错</returns>
        public static DataSet FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, SqlConnection cnn)
        {
            DataSet dt = new DataSet();
            if (FillDataSet(strSql, cmdType, cmdParms, dt, cnn) == -1) return null;
            return dt;
        }

        //适用于 SQL Server 2005
        //SELECT TOP 页大小 * 
        //FROM 
        //       (
        //      SELECT ROW_NUMBER() OVER (ORDER BY id) AS RowNumber,* FROM table1
        //     ) A
        //WHERE RowNumber > 页大小*(页数-1)

        //支持分页和排序用,第一个字段是RowNumber
        public static DataTable FillTable(String strField, String strTable, String strOrderBy, String strFilter, int nPageSize, int nPageIndex, SqlConnection cnn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select Top ");
            strSql.Append(nPageSize);
            strSql.Append(" * From (Select ROW_NUMBER() OVER (ORDER BY ");
            strSql.Append(strOrderBy);
            strSql.Append(") AS RowNumber,");
            strSql.Append(strField);

            strSql.Append(" From ");
            strSql.Append(strTable);
            //strSql.Append(" where ValiDate='v'");


            if (strFilter.Length > 0)
            {
                strSql.Append(" Where ");
                strSql.Append(strFilter);
            }
            strSql.Append(") AS TEMPTABLE Where RowNumber>");
            strSql.Append(nPageSize * nPageIndex);
            //strSql.Append(" and ValiDate='v'");

            //strFilter可以放在里面的Where后面,也可以放在外面的Where后面(现在是在里面的Where后面,也不知道哪一个效率更高)

            return FillTable(strSql.ToString(), cnn);
        }

        /// <summary>
        ///  执行查询命令，尽量不要使用此函数
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(String strSql, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cnn);
            SqlDataReader oReader = null;
            try
            {
                cnn.Open();
                oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SequentialAccess);
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteReader", e);
            }
            catch (Exception e)
            {
                GLog.LogError("ExecuteReader", e);
            }
            finally
            {
            }

            return oReader;
        }

        /// <summary>
        /// 执行返回查询第一行第一列内容命令


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回查询结果中的第一行第一列</returns>
        public static object ExecuteScalar(String strSql, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cnn);
            object oReturn = null;
            try
            {
                cnn.Open();
                oReturn = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteScalar", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return oReturn;
        }

        /// <summary>
        /// 执行返回查询第一行第一列内容命令


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <returns>返回查询结果中的第一行第一列</returns>
        public static object ExecuteScalar(String strSql, CommandType cmdType, SqlParameter[] cmdParms, SqlConnection cnn)
        {
            SqlCommand cmd = PrepareCommand(cnn, null, cmdType, strSql, cmdParms);
            object oReturn = null;
            try
            {
                cnn.Open();
                oReturn = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteScalar", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return oReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="cmdType">命令字符串类型，如text,StoredProcedure</param>
        /// <param name="cmdParms">命令执行参数</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public static int ExecuteNonQuery(String strSql, CommandType cmdType, SqlParameter[] cmdParms, SqlConnection cnn)
        {
            SqlCommand cmd = PrepareCommand(cnn, null, cmdType, strSql, cmdParms);
            int nReturn = -1;
            try
            {
                cnn.Open();
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteNonQuery", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public static int ExecuteNonQuery(String strSql, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cnn);
            int nReturn = -1;
            try
            {
                cnn.Open();
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteNonQuery", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="Parameters">参数数组</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public static int ExecuteNonQuery(String strSql, SqlParameter[] Parameters, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cnn);
            cmd.Parameters.AddRange(Parameters);
            cmd.CommandTimeout = 300;
            int nReturn = -1;
            try
            {

                cnn.Open();

                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteNonQuery", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）


        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public static int ExecuteNonQuery(String strSql, SqlParameter param, SqlConnection cnn)
        {
            SqlCommand cmd = new SqlCommand(strSql, cnn);
            cmd.Parameters.Add(param);


            int nReturn = -1;
            try
            {
                cnn.Open();
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                GLog.LogError("ExecuteNonQuery", e);
            }
            finally
            {
                cnn.Close();
                cnn = null;
            }

            return nReturn;
        }
        #endregion

        #region 主数据库相关操作 ConnectString = ConnectionStrings["MainDBCnn"]
        /// <summary>
        /// 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int FillTable(String strSql, DataTable table)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return FillTable(strSql, table, cnn);
        }
        public static DataTable FillTable(String strSql)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, dt) == -1) return null;
            return dt;
        }

        public static int FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataTable table)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return FillTable(strSql, cmdType, cmdParms, table, cnn);
        }

        public static DataTable FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, cmdType, cmdParms, dt) == -1) return null;
            return dt;
        }

        public static int FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataSet dataset)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return FillDataSet(strSql, cmdType, cmdParms, dataset, cnn);
        }
        public static DataSet FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            DataSet dt = new DataSet();
            if (FillDataSet(strSql, cmdType, cmdParms, dt) == -1) return null;
            return dt;
        }

        public static DataTable FillTable(String strField, String strTable, String strOrderBy, String strFilter, int nPageSize, int nPageIndex)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return null;

            return FillTable(strField, strTable, strOrderBy, strFilter, nPageSize, nPageIndex, cnn);
        }

        public static SqlDataReader ExecuteReader(String strSql)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return null;

            return ExecuteReader(strSql, cnn);
        }

        public static object ExecuteScalar(String strSql)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return null;

            return ExecuteScalar(strSql, cnn);
        }

        public static object ExecuteScalar(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return null;

            return ExecuteScalar(strSql, cmdType, cmdParms, cnn);
        }

        public static int ExecuteNonQuery(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, cmdType, cmdParms, cnn);
        }

        public static int ExecuteNonQuery(String strSql)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, cnn);
        }

        public static int ExecuteNonQuery(String strSql, SqlParameter[] Parameters)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, Parameters, cnn);
        }

        public static int ExecuteNonQuery(String strSql, SqlParameter param)
        {
            SqlConnection cnn = CreateConnecion();
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, param, cnn);
        }
        #endregion

        #region CnnConfig数据库相关操作 ConnectString = ConnectionStrings[strCnnConfig]
        /// <summary>
        /// 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int FillTable(String strSql, DataTable table, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return FillTable(strSql, table, cnn);
        }
        public static DataTable FillTable(String strSql, String strCnnConfig)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, dt, strCnnConfig) == -1) return null;
            return dt;
        }

        public static int FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataTable table, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return FillTable(strSql, cmdType, cmdParms, table, cnn);
        }

        public static DataTable FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, String strCnnConfig)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, cmdType, cmdParms, dt, strCnnConfig) == -1) return null;
            return dt;
        }

        public static int FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataSet dataset, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return FillDataSet(strSql, cmdType, cmdParms, dataset, cnn);
        }
        public static DataSet FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, String strCnnConfig)
        {
            DataSet dt = new DataSet();
            if (FillDataSet(strSql, cmdType, cmdParms, dt, strCnnConfig) == -1) return null;
            return dt;
        }

        public static DataTable FillTable(String strField, String strTable, String strOrderBy, String strFilter, int nPageSize, int nPageIndex, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return null;

            return FillTable(strField, strTable, strOrderBy, strFilter, nPageSize, nPageIndex, cnn);
        }

        public static SqlDataReader ExecuteReader(String strSql, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return null;

            return ExecuteReader(strSql, cnn);
        }

        public static object ExecuteScalar(String strSql, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return null;

            return ExecuteScalar(strSql, cnn);
        }

        public static object ExecuteScalar(String strSql, CommandType cmdType, SqlParameter[] cmdParms, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return null;

            return ExecuteScalar(strSql, cmdType, cmdParms, cnn);
        }

        public static int ExecuteNonQuery(String strSql, CommandType cmdType, SqlParameter[] cmdParms, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, cmdType, cmdParms, cnn);
        }

        public static int ExecuteNonQuery(String strSql, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, cnn);
        }

        public static int ExecuteNonQuery(String strSql, SqlParameter[] Parameters, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, Parameters, cnn);
        }

        public static int ExecuteNonQuery(String strSql, SqlParameter param, String strCnnConfig)
        {
            SqlConnection cnn = CreateConnecion(strCnnConfig);
            if (cnn == null) return -1;

            return ExecuteNonQuery(strSql, param, cnn);
        }
        #endregion

        #region 读取Reader字段
        public static Int32 GetInt32(SqlDataReader oReader, int nIndex)
        {
            if (oReader.IsDBNull(nIndex)) return 0;
            return oReader.GetInt32(nIndex);
        }

        public static String GetString(SqlDataReader oReader, int nIndex)
        {
            if (oReader.IsDBNull(nIndex)) return "";
            String str = oReader.GetString(nIndex);
            str = str.Trim();
            return str;
        }

        public static float GetFloat(SqlDataReader oReader, int nIndex)
        {
            if (oReader.IsDBNull(nIndex)) return 0.0f;
            return oReader.GetFloat(nIndex);
        }

        public static double GetDouble(SqlDataReader oReader, int nIndex)
        {
            if (oReader.IsDBNull(nIndex)) return 0;
            return oReader.GetDouble(nIndex);
        }

        public static DateTime GetDateTime(SqlDataReader oReader, int nIndex)
        {
            if (oReader.IsDBNull(nIndex)) return DateTime.MinValue;
            return oReader.GetDateTime(nIndex);
        }

        public static int GetDBMax(String strField, String strTable)
        {
            String strSql = "Select max(" + strField + ") From " + strTable;
            return GFun.SafeToInt32(ExecuteScalar(strSql));
        }

        public static int GetDBCount(String strSql)
        {
            return GFun.SafeToInt32(ExecuteScalar(strSql));
        }

        #endregion
    }
}
