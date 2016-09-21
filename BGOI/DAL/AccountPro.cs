using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI
{
    public class AccountPro
    {
        private const String AccountCnn = "AccountCnn";

        public static bool ValidateUserPWD(String strLoginName, String strPwd)
        {
            //if (strPwd != "1")
            //{
            //    strPwd = Encrypt(strPwd);
            //}

            // and (vvv = '' or vvv > getdate())
            //String strSql = "SELECT a.[LoginPwd] FROM UM_User as a INNER JOIN UM_UserSystem as b ON a.LoginName = b.UserID";
            //strSql = strSql + String.Format(" Where a.LoginName='{0}' And b.SystemID like '%" + GAccount.SystemID + "%'", strLoginName, GAccount.SystemID);
            String strSql = "SELECT a.UserPwd FROM UM_UserNew as a INNER JOIN UM_UserSystem as b ON a.UserId = b.UserId";
            strSql = strSql + String.Format(" Where a.UserLogin='{0}' And b.SystemID like '%" + GAccount.SystemID + "%' and (b.Validate = '' or b.Validate is NULL or b.Validate > '"+DateTime.Now+"')", strLoginName, GAccount.SystemID);
            
            String pwd = GFun.SafeToString(SQLBase.ExecuteScalar(strSql, AccountCnn));
            if (pwd == strPwd)
            {
                //GLog.LogInfo("用户名密码正确登录成功！");

                return true;
            }

            return false;
        }

        public static Acc_Account ValidateUser(string a_strUserName, string a_strPwd)
        {
            string strError = "";
            DataRow oRow = null;
            Acc_Account instAccount = new Acc_Account();
            DataTable dt = GetAccount(a_strUserName, GAccount.SystemID, ref strError);
            if (dt != null && dt.Rows.Count > 0)
            {
                oRow = dt.Rows[0];
                //instAccount.UserID = GFun.SafeToInt32(oRow[0]);
                //instAccount.UserName = GFun.SafeToString(oRow[1]);
                //instAccount.UnitID = GFun.SafeToString(oRow[2]);
                //instAccount.Rights = GFun.SafeToString(oRow[3]);
                //instAccount.UnitCode = GFun.SafeToString(oRow[4]);
                //instAccount.UnitName = GFun.SafeToString(oRow[5]);
                //instAccount.UnitBrief = GFun.SafeToString(oRow[6]);
                //instAccount.HigherUnitID = GFun.SafeToString(oRow[7]);
                //instAccount.Functions = GFun.SafeToString(oRow[8]);
                //instAccount.UserRole = GFun.SafeToString(oRow[9]);
                //instAccount.FunctionsCode = GFun.SafeToString(oRow[12]);
                //instAccount.BranchID = GFun.SafeToString(oRow[13]);
                //instAccount.BranchPCD = GFun.SafeToString(oRow[14]);
                //instAccount.BranchName = GFun.SafeToString(oRow[15]);

                instAccount.UserID = GFun.SafeToInt32(oRow[0]);
                instAccount.UserName = GFun.SafeToString(oRow[1]);
                instAccount.UnitID = GFun.SafeToString(oRow[2]);
                instAccount.Rights = GFun.SafeToString(oRow[3]);
                instAccount.UnitName = GFun.SafeToString(oRow[4]);
                instAccount.FunctionsCode = GFun.SafeToString(oRow[5]);
                instAccount.UserRole = GFun.SafeToString(oRow[6]);
                instAccount.UserMobile = GFun.SafeToString(oRow[7]);
                instAccount.RoleNames = GFun.SafeToString(oRow[8]);
                instAccount.Path = GFun.SafeToString(oRow[9]);
                instAccount.Exjob = GFun.SafeToString(oRow[10]);
            }
            return instAccount;
        }

        private static DataTable GetAccount(string a_strLoginName, string a_strSysID, ref string a_strErr)
        {
            try
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@LoginName",a_strLoginName),
                    new SqlParameter("@SystemID",a_strSysID)
                };
                //string strGetAccount = "SELECT a.ID,a.UserName,a.UnitID,b.RightCodes,c.UnitCode ,c.UnitName,c.UnitBrief,c.HigherUnitID,UserFunc,b.UserRole,c.UnitOneWord,a.UserPhone,d.FunctionsCode,a.Branch,e.Provice + '/' +e.City + '/'+e.District as BranchPCD, ";
                //strGetAccount += "f.Name + g.Name + h.Name + e.Street + e.BranchName as BranchName ";
                //strGetAccount += "FROM UM_User as a INNER JOIN UM_UserSystem as b ON a.LoginName = b.UserID inner join UM_Unit as c on a.UnitID=c.UnitCode inner join UM_UnitFunctions as d on a.UnitID = d.UnitID ";
                //strGetAccount += "inner join UM_Branch e on a.Branch = e.BranchID ";
                //strGetAccount += "inner join [CGMS]..tk_ConfigAddr_Provice f on e.Provice = f.ProvinceID ";
                //strGetAccount += "inner join [CGMS]..tk_ConfigAddr_City g on e.City = g.CityID ";
                //strGetAccount += "inner join [CGMS]..tk_ConfigAddr_District h on e.District = h.DistrictID ";
                //strGetAccount += "where a.LoginName=@LoginName and b.SystemID like '%' + @SystemID + '%'";

                string strGetAccount = "SELECT a.UserId,a.UserName,a.DeptId,b.RightCodes,c.DeptName,";
                strGetAccount += "b.UserFunc,b.UserRole,a.MobilePhone,a.roleNames,c.Path,a.ExJob";
                strGetAccount += " FROM UM_UserNew as a";
                strGetAccount += " INNER JOIN UM_UserSystem as b ON a.UserId = b.UserId";
                strGetAccount += " inner join UM_UnitNew as c on a.DeptId=c.DeptId";
                strGetAccount += " where a.UserLogin=@LoginName and b.SystemID like '%' + @SystemID + '%'";
                DataTable dtAccount = SQLBase.FillTable(strGetAccount, CommandType.Text, sqlPar, AccountCnn);
                return dtAccount;
            }
            catch (SqlException ex)
            {
                a_strErr = ex.Message;
                return null;
            }
        }
    }
}
