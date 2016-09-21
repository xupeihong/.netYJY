using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Principal;

namespace TECOCITY_BGOI
{
    public class  GAccount
    {
        public static string KEY_CACHEUSER = System.Configuration.ConfigurationManager.AppSettings["AuthSaveKey"];
        //private static string _SystemID = ",1,";
        private static string _SystemID = "6";
        public static string SystemID
        {
            get { return _SystemID; }
            set { _SystemID = value; }
        }

        public static String UserInfo
        {
            get
            {
                String str = "";
                Acc_Account oAccountInfo = GetAccountInfo();
                if (oAccountInfo != null)
                {
                    str = str + oAccountInfo.UnitName + "  ";
                    str = str + oAccountInfo.UserName;
                }

                return str;
            }
        }

        public static Acc_Account GetAccountInfo()
        {
            return GetAccountInfo(new HttpContextWrapper(System.Web.HttpContext.Current));
        }

        public static Acc_Account GetAccountInfo(HttpContextBase httpContext)
        {
            String strLoginName;
            Acc_Account oGAccountInfo = null;
            IPrincipal user = httpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            try
            {
                oGAccountInfo = (httpContext.Session[KEY_CACHEUSER]) as Acc_Account;
            }
            catch
            {
            }

            if (oGAccountInfo != null) return oGAccountInfo;

            strLoginName = user.Identity.Name.Split('^')[0];
            oGAccountInfo = new Acc_Account();
            oGAccountInfo.LoginName = strLoginName;

            string strError = "";
            DataRow oRow = null;

            DataTable dt = GetAccount(strLoginName, SystemID.ToString(), ref strError); //SQLBase.FillTable(strSql, AccountCnn);
            if (dt != null && dt.Rows.Count > 0)
            {
                oRow = dt.Rows[0];
                //oGAccountInfo.UserID = GFun.SafeToInt32(oRow[0]);
                //oGAccountInfo.UserName = GFun.SafeToString(oRow[1]);
                //oGAccountInfo.UnitID = GFun.SafeToString(oRow[2]);
                //oGAccountInfo.Rights = GFun.SafeToString(oRow[3]);
                //oGAccountInfo.UnitCode = GFun.SafeToString(oRow[4]);
                //oGAccountInfo.UnitName = GFun.SafeToString(oRow[5]);
                //oGAccountInfo.UnitBrief = GFun.SafeToString(oRow[6]);
                //oGAccountInfo.HigherUnitID = GFun.SafeToString(oRow[7]);
                //oGAccountInfo.Functions = GFun.SafeToString(oRow[8]);
                //oGAccountInfo.UserRole = GFun.SafeToString(oRow[9]);

                oGAccountInfo.UserID = GFun.SafeToInt32(oRow[0]);
                oGAccountInfo.UserName = GFun.SafeToString(oRow[1]);
                oGAccountInfo.UnitID = GFun.SafeToString(oRow[2]);
                oGAccountInfo.Rights = GFun.SafeToString(oRow[3]);
                oGAccountInfo.UnitName = GFun.SafeToString(oRow[4]);
                oGAccountInfo.FunctionsCode = GFun.SafeToString(oRow[5]);
                oGAccountInfo.UserRole = GFun.SafeToString(oRow[6]);
                oGAccountInfo.UserMobile = GFun.SafeToString(oRow[7]);
                oGAccountInfo.RoleNames = GFun.SafeToString(oRow[8]);
                oGAccountInfo.Path = GFun.SafeToString(oRow[9]);
                oGAccountInfo.Exjob = GFun.SafeToString(oRow[10]);
            }

            //设置Session
            httpContext.Session[KEY_CACHEUSER] = oGAccountInfo;

            return oGAccountInfo;
        }

        private static DataTable GetAccount(string a_strLoginName, string a_strSysID, ref string a_strErr)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AccountCnn"].ConnectionString);
            try
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@LoginName",a_strLoginName),
                    new SqlParameter("@SystemID",a_strSysID)
                };
                //string strGetAccount = "SELECT a.UID,a.EmpNo,b.LoginName,a.UserName,a.UnitID,c.UnitName,a.UserPhone,c.HigherUnitID,b.RoleID,b.RightID,c.UnitCode ,c.UnitName,c.UnitBrief,c.HigherUnitID,a.UserPhone,d.UnRoleFuncCodes,e.UnRightFuncCodes ";
                //strGetAccount += "FROM UM_User as a INNER JOIN UM_UserSysLogon as b ON a.UID = b.UID inner join UM_Unit as c on a.UnitID=c.UnitCode inner join UM_RoleFunctions d on d.RoleID=b.RoleID inner join UM_RightFunctions e on e.RightID=b.RightID ";
                //strGetAccount += "where b.LoginName=@LoginName and b.SystemID =@SystemID";

                string strGetAccount = "SELECT a.UserId,a.UserName,a.DeptId,b.RightCodes,c.DeptName,";
                strGetAccount += "b.UserFunc,b.UserRole,a.MobilePhone,a.roleNames,c.Path,a.ExJob";
                strGetAccount += " FROM UM_UserNew as a";
                strGetAccount += " INNER JOIN UM_UserSystem as b ON a.UserId = b.UserId";
                strGetAccount += " inner join UM_UnitNew as c on a.DeptId=c.DeptId";
                strGetAccount += " where a.UserLogin=@LoginName and b.SystemID like '%' + @SystemID + '%'";


                SqlCommand comm = new SqlCommand(strGetAccount, conn);
                comm.Parameters.Add(sqlPar[0]);
                comm.Parameters.Add(sqlPar[1]);

                SqlDataAdapter da = new SqlDataAdapter(comm);
                conn.Open();

                DataTable dtAccount = new DataTable();
                da.Fill(dtAccount);

                return dtAccount;
            }
            catch (SqlException ex)
            {
                conn.Close();
                conn.Dispose();
                a_strErr = ex.Message;
                return null;
            }
        }
    }

    public class Acc_Account
    {
        private string _functions = String.Empty;
        //private string[] _functionsSplit = new string[0];
        private string _rights = String.Empty;
        private string[] _rightsSplit = new string[0];

        private string _HigherUnit = String.Empty;
        private string[] _HigherUnitSplit = new string[0];
        private string _SubUnit = String.Empty;
        private string[] _SubUnitSplit = new string[0];

        public Acc_Account()
        {
        }

        //User
        public int UserID { get; set; }
        public String LoginName { get; set; }
        public String UserName { get; set; }
        public String Department { get; set; }
        //Unit
        public string UnitID { get; set; }
        public String UnitName { get; set; }
        public String UnitBrief { get; set; }
        public String UserMobile { get; set; }

        public String RoleNames { get; set; }
        //SystemFunctions
        public int UnitLevel { get; set; }
        public int UnitCodeLen { get; set; }
        public String UnitCode { get; set; }
        public String DespatchName { get; set; }
        public String HigherUnitID { get; set; }
        public String UserRole { get; set; }
        public String UnitOneWord { get; set; }
        public String FunctionsCode { get; set; }
        public String BranchID { get; set; }
        public String BranchName { get; set; }
        public String BranchPCD { get; set; }
        public String Path { get; set; }
        public String Exjob { get; set; }



        public String HigherUnit
        {
            get { return _HigherUnit; }
            set
            {
                _HigherUnit = value;
                _HigherUnitSplit = SplitString(_HigherUnit);
            }
        }

        public String SubUnit
        {
            get { return _SubUnit; }
            set
            {
                _SubUnit = value;
                _SubUnitSplit = SplitString(_SubUnit);
            }
        }

        public String[] HigherUnits { get { return _HigherUnitSplit; } }
        public String[] SubUnits { get { return _SubUnitSplit; } }

        //
        public string Functions
        {
            get { return _functions ?? String.Empty; }
            set
            {
                _functions = value;
                // _functionsSplit = SplitString(value);
            }
        }

        public string Rights
        {
            get { return _rights ?? String.Empty; }
            set
            {
                _rights = value;
                _rightsSplit = SplitString(value);
            }
        }

        public bool IsInRight(string right)
        {
            if (_rightsSplit == null ||
                _rightsSplit.Length == 0 || _rightsSplit.Contains(right, StringComparer.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public bool IsFunctions(string functions)
        {
            if (_functions == null ||
                _functions.Length == 0 || String.Equals(_functions, functions, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}
