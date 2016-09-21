﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TECOCITY_BGOI
{
    public class SQLTrans
    {
        private SqlConnection m_cnn = null;
        private SqlTransaction m_oTransaction = null;

        public SQLTrans()
        {
        }

        public void Open()
        {
            if (m_oTransaction != null)
            {
                m_oTransaction.Rollback();
                m_oTransaction.Dispose();
                m_oTransaction = null;
            }

            if (m_cnn != null)
            {
                m_cnn.Close();
                m_cnn.Dispose();
                m_cnn = null;
            }

            m_cnn = SQLBase.CreateConnecion();
            m_cnn.Open();

            m_oTransaction = m_cnn.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCnnConfig">ConnectionStrings索引</param>
        public void Open(String strCnnConfig)
        {
            if (m_oTransaction != null)
            {
                m_oTransaction.Rollback();
                m_oTransaction.Dispose();
                m_oTransaction = null;
            }

            if (m_cnn != null)
            {
                m_cnn.Close();
                m_cnn.Dispose();
                m_cnn = null;
            }

            m_cnn = SQLBase.CreateConnecion(strCnnConfig);
            m_cnn.Open();

            m_oTransaction = m_cnn.BeginTransaction();
        }

        public void Close(bool bCommit)
        {
            try
            {
                if (m_oTransaction != null)
                {
                    if (bCommit)
                    {
                        m_oTransaction.Commit();
                    }
                    else
                    {
                        m_oTransaction.Rollback();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (m_oTransaction != null)
                {
                    m_oTransaction.Dispose();
                    m_oTransaction = null;
                }
                if (m_cnn != null)
                {
                    m_cnn.Close();
                    m_cnn = null;
                }
            }
        }

        #region 数据库相关操作

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="table">需填充的DataTable</param>
        /// <returns>返回DataTable中数据行数； 返回-1时查询命令出错</returns>
        public int FillTable(String strSql, DataTable table)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                da.SelectCommand = new SqlCommand(strSql, m_cnn, m_oTransaction);
                nRow = da.Fill(table);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                da.Dispose();
            }

            return nRow;
        }

        /// <summary>
        /// 执行查询命令 根据Select SQL语句填充DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回填充的DataTable；返回null时查询命令出错</returns>
        public DataTable FillTable(String strSql)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, dt) == -1) return null;
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
        public int FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataTable table)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                da.SelectCommand = SQLBase.PrepareCommand(m_cnn, m_oTransaction, cmdType, strSql, cmdParms);

                nRow = da.Fill(table);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                da.Dispose();
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
        public DataTable FillTable(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            if (FillTable(strSql, cmdType, cmdParms, dt) == -1) return null;
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
        public int FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms, DataSet dataset)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            int nRow = -1;
            try
            {
                da.SelectCommand = SQLBase.PrepareCommand(m_cnn, m_oTransaction, cmdType, strSql, cmdParms);
                nRow = da.Fill(dataset);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                da.Dispose();
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
        public DataSet FillDataSet(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            DataSet dt = new DataSet();
            if (FillDataSet(strSql, cmdType, cmdParms, dt) == -1) return null;
            return dt;
        }

        /// <summary>
        ///  执行查询命令，尽量不要使用此函数
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回SqlDataReader</returns>
        public SqlDataReader ExecuteReader(String strSql)
        {
            SqlCommand cmd = new SqlCommand(strSql, m_cnn, m_oTransaction);
            SqlDataReader oReader = null;
            try
            {
                oReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
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
        public object ExecuteScalar(String strSql)
        {
            SqlCommand cmd = new SqlCommand(strSql, m_cnn, m_oTransaction);
            object oReturn = null;
            try
            {
                oReturn = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
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
        public object ExecuteScalar(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = SQLBase.PrepareCommand(m_cnn, m_oTransaction, cmdType, strSql, cmdParms);
            object oReturn = null;
            try
            {
                oReturn = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
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
        public int ExecuteNonQuery(String strSql, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlCommand cmd = SQLBase.PrepareCommand(m_cnn, m_oTransaction, cmdType, strSql, cmdParms);
            int nReturn = -1;
            try
            {
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）

        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public int ExecuteNonQuery(String strSql)
        {
            SqlCommand cmd = new SqlCommand(strSql, m_cnn, m_oTransaction);
            int nReturn = -1;
            try
            {
                ;
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）

        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="Parameters">参数数组</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public int ExecuteNonQuery(String strSql, SqlParameter[] Parameters)
        {
            SqlCommand cmd = new SqlCommand(strSql, m_cnn, m_oTransaction);
            cmd.Parameters.AddRange(Parameters);

            int nReturn = -1;
            try
            {
                nReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }

            return nReturn;
        }

        /// <summary>
        /// 执行 非查询 sql语句（插入，删除，修改等）

        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>返回命令影响行数 返回-1时命令出错</returns>
        public int ExecuteNonQuery(String strSql, SqlParameter param)
        {
            SqlCommand cmd = new SqlCommand(strSql, m_cnn, m_oTransaction);
            cmd.Parameters.Add(param);

            int nReturn = -1;
            try
            {
                nReturn = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }

            return nReturn;
        }
        #endregion
    }
}
