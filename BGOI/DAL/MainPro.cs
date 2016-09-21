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
    public class MainPro
    {
        public static string getJudgeWarnString(string where2, string where4)
        {
            string warn = "";
            string sid = "";
            string fid = "";
            string sid2 = "";
            #region MyRegion
            //string str1 = "select * from tk_DevicsBas a where 1=1 " + where + "";
            //DataTable dt = SQLBase.FillTable(str1, "SupplyCnn");
            //if (dt.Rows.Count > 0)
            //    warn += "a";
            //string str2 = "select * from tk_ContractBas a where 1=1 " + where1 + "";
            //DataTable dt2 = SQLBase.FillTable(str2, "SupplyCnn");
            //if (dt2.Rows.Count > 0)
            //    warn += "b"; 
            #endregion
            string str3 = "select  top 1 * from tk_SFileInfo a INNER JOIN tk_SupplierBas b on a.sid=b.sid where 1=1 AND a.Validate='v'  " + where2 + " order by a.createtime desc";
            DataTable dt3 = SQLBase.FillTable(str3, "SupplyCnn");
            if (dt3.Rows.Count > 0)
            {
                sid = dt3.Rows[0]["SID"].ToString();
                fid = dt3.Rows[0]["FID"].ToString();
                warn += "c";
                warn += sid;
            }
            string str5 = "select top 1 * from tk_SCertificate a INNER JOIN tk_SupplierBas b on a.sid=b.sid where 1=1 AND a.Validate='v' " + where4 + " order by a.CreateTime desc";
            DataTable dt5 = SQLBase.FillTable(str5, "SupplyCnn");
            if (dt5.Rows.Count > 0)
            {
                sid2 = dt5.Rows[0]["SID"].ToString();
                warn += "e";
                warn += sid2;
                warn += fid;
            }
            #region MyRegion
            //string str4 =
            //    "select distinct a.YYCode from taskOrder b left join MandateInfo a on a.MCode = b.MId left join tk_ConfigState c on b.state = c.Id left join SampleInfo d on a.YYCode = d.YYCode where DATEDIFF ( day ,getdate(),b.RFinishDate)<=2 and b.State<12 and b.State<>-1 " +
            //    where3;
            //DataTable dt4 = SQLBase.FillTable(str4, "MainDBCnn");
            //if (dt4.Rows.Count > 0)
            //    warn += "d"; 
            #endregion
            warn = warn.TrimEnd(',');
            return warn;
        }

        private const String AccountCnn = "AccountCnn";
        public static bool UpdatePwd(string oldPwd, string newPwd, int userId, ref string err)
        {
            string sql = "select count(*) from UM_UserNew where UserId=" + userId + " and UserPwd='" + oldPwd + "'";
            int count = Convert.ToInt32(SQLBase.ExecuteScalar(sql, AccountCnn));
            if (count <= 0)
            {
                err = "原密码错误！";
                return false;
            }
            sql = "update UM_UserSystem set LoginPwd = '" + newPwd + "',Validate = NULL where UserId = " + userId;
            if (SQLBase.ExecuteNonQuery(sql, AccountCnn) > 0)
            {
                sql = "update UM_UserNew set UserPwd = '" + newPwd + "' where UserId = " + userId;
                SQLBase.ExecuteNonQuery(sql, AccountCnn);
                err = "保存成功！";
                return true;
            }
            else
            {
                err = "保存失败！";
                return false;
            }

        }

        public static DataTable getUserNamebyLoginName(string LoginName)
        {
            string str = "select UserId,UserName from UM_UserNew where UserLogin = '" + LoginName + "'";
            DataTable dt = SQLBase.FillTable(str, AccountCnn);
            return dt;
        }

        public static bool RestPwd(string userId, ref string err)
        {
            string sql = "";
            sql = "update UM_UserSystem set LoginPwd = '1',Validate = '"+DateTime.Now.AddDays(1)+"' where UserId = " + userId;
            if (SQLBase.ExecuteNonQuery(sql, AccountCnn) > 0)
            {
                sql = "update UM_UserNew set UserPwd = '1' where UserId = " + userId;
                SQLBase.ExecuteNonQuery(sql, AccountCnn);
                err = "保存成功！";
                return true;
            }
            else
            {
                err = "保存失败！";
                return false;
            }
        }

        public static string judgeValidate(string userid)
        {
            string validate = "";
            string str = "select Validate from UM_UserSystem where UserID = '"+userid+"'";
            DataTable dt = SQLBase.FillTable(str, AccountCnn);
            if (dt.Rows.Count > 0)
                validate = dt.Rows[0][0].ToString();
            return validate;
        }
    }
}
