using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI
{
    public class MainMan
    {
        public static string getNewJudgeWarnString( string where2,string where4)
        {
            return MainPro.getJudgeWarnString( where2,where4);
        }

        public static bool UpdatePwd(string oldPwd, string newPwd, int userId, ref string err)
        {
            return MainPro.UpdatePwd(oldPwd, newPwd, userId, ref err);
        }

        public static DataTable getNewUserNamebyLoginName(string LoginName)
        {
            return MainPro.getUserNamebyLoginName(LoginName);
        }

        public static bool RestNewPwd(string userId, ref string err)
        {
            return MainPro.RestPwd(userId, ref err);
        }

        public static string judgeNewValidate(string userid)
        {
            return MainPro.judgeValidate(userid);
        }
    }
}
