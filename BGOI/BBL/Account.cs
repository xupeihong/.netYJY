using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Account
    {
        public static Acc_Account ValidateUser(string a_strUserName, string a_strPwd)
        {
            return AccountPro.ValidateUser(a_strUserName, a_strPwd);
        }

        public static bool ValidateUserPWD(String strLoginName, String strPwd)
        {
            return AccountPro.ValidateUserPWD(strLoginName, strPwd);
        }

    }
}
