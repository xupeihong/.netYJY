using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace TECOCITY_BGOI
{
    public static class SqlHelper
    {
        private static string connStr =
           System.Configuration.ConfigurationManager.ConnectionStrings["SupplyCnn"].ConnectionString;
        public static int ExcuteNoQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand com = new SqlCommand(sql, conn))
                {
                    com.Parameters.AddRange(ps);
                    conn.Open();
                    return com.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand com = new SqlCommand(sql, conn))
                {
                    com.Parameters.AddRange(ps);
                    conn.Open();
                    return com.ExecuteScalar();
                }
            }
        }
        public static DataTable ExecuteReader(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.SelectCommand.Parameters.AddRange(ps);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
        public static DataTable ExecuteReader(string sql, CommandType ct, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.SelectCommand.Parameters.AddRange(ps);
                sda.SelectCommand.CommandType = ct;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }
}
