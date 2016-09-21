using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TECOCITY_BGOI
{
    public class COM_ApprovalMan
    {
        public static string getNewwebkey(string webkey)
        {
            return COM_ApprovalPro.getwebkey(webkey);
        }

        public static string GetNewShowSPid(string folderBack)
        {
            return COM_ApprovalPro.GetShowSPid(folderBack);
        }
        public static string GetNewSPid(string folderBack)
        {
            return COM_ApprovalPro.GetSPid(folderBack);
        }

        public static UIDataTable getNewUMwebkeyGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return COM_ApprovalPro.getUMwebkeyGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            if (COM_ApprovalPro.InsertApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static int JudgeNewLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            return COM_ApprovalPro.judgeLoginUser(userid, webkey, folderBack, SPID);
        }
        public static bool UpdateNewApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            if (COM_ApprovalPro.UpdateApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static bool UpdateNewzhunchuApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            if (COM_ApprovalPro.UpdatezhunchuApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static UIDataTable getNewConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            return COM_ApprovalPro.getConditionGrid(a_intPageSize, a_intPageIndex, where, folderBack);
        }

        public static DataTable getNewProductByUnitID(string UnitID)
        {
            return COM_ApprovalPro.getProductByUnitID(UnitID);
        }

        public static DataTable getNewProductByName(string ProductName, string UnitID)
        {
            return COM_ApprovalPro.getProductByName(ProductName, UnitID);
        }

        public static bool InsertNewTemporary(string ProductName, string Spc, string Pid, string Num, string RelevanceID, string dataT, ref string a_strErr)
        {
            if (COM_ApprovalPro.InsertTemporary(ProductName, Spc, Pid, Num, RelevanceID,dataT,ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
    }
}
