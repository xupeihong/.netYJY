using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
namespace TECOCITY_BGOI
{
    public class UserAptitudeMan
    {
        public static UIDataTable getNewUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return UserAptitudePro.getUserGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewUserAptitude(UserAptitude Uaptitude, byte[] fileByte, ref string a_strErr)
        {
            if (UserAptitudePro.InsertUserAptitude(Uaptitude, fileByte, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewAptitudeUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return UserAptitudePro.getAptitudeUserGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewAptitudeGrid(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            return UserAptitudePro.getAptitudeGrid(a_intPageSize, a_intPageIndex, where, unitid);
        }

        public static UserAptitude getNewUpdateUserAptitude(string id)
        {
            return UserAptitudePro.getUpdateUserAptitude(id);
        }

        public static DataTable getNewFile(string id)
        {
            return UserAptitudePro.getFile(id);
        }

        public static bool deleteNewFile(string id, ref string a_strErr)
        {
            if (UserAptitudePro.deleteFile(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewUserAptitude(string ID, UserAptitude Uap, byte[] fileByte, ref string a_strErr)
        {
            if (UserAptitudePro.UpdateUserAptitude(ID, Uap, fileByte, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewLendAptitude(UCertificatLend Lend, ref string a_strErr)
        {
            if (UserAptitudePro.InsertLendAptitude(Lend, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewLendAptitude(UCertificatLend Lend, ref string a_strErr)
        {
            if (UserAptitudePro.UpdateLendAptitude(Lend, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static string getNewAptitudeTime()
        {
            return UserAptitudePro.getAptitudeTime();
        }
       
    }
}
