using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;

namespace TECOCITY_BGOI
{
    public class ContractMan
    {
        public static string GetNewShowCID()
        {
            return ContractPro.GetShowCID();
        }
        public static string GetNewCID()
        {
            return ContractPro.GetCID();
        }

        public static DataTable getNewMoneyFromProjectBas(string ProID)
        {
            return ContractPro.getMoneyFromProjectBas(ProID);
        }

        public static List<string> GetNewContractID()
        {
            List<string> list = new List<string>();
            DataTable dt = ContractPro.getContractID();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["ContractID"].ToString());
            }
            return list;
        }
        public static List<SelectListItem> GetNewState()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ContractPro.getContractState();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            //ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["StateId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["name"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable getNewContractGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ContractPro.getContractGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ContractPro.getCashBackGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewUserlogGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ContractPro.getUserlogGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewStandingBookGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ContractPro.getStandingBookGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable getNewPrintStandingBook(string where)
        {
            return ContractPro.getPrintStandingBook(where);
        }

        public static string getNewReturnTime()
        {
            return ContractPro.getReturnTime();
        }

        public static string getNewCPlanTime()
        {
            return ContractPro.getCPlanTime();
        }

        public static string getNewCashBackTime()
        {
            return ContractPro.getCashBackTime();
        }

        public static UIDataTable getNewProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ContractPro.getProjectGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool judgeNewCIDinContractBas(string CID)
        {
            return ContractPro.judgeCIDinContractBas(CID);
        }

        public static bool InsertNewContractBas(ContractBas Bas, ref string a_strErr)
        {
            if (ContractPro.InsertContractBas(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewProjectContract(ContractBas Bas, ref string a_strErr)
        {
            if (ContractPro.InsertProjectContract(Bas,ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewProjectContract(ContractBas Bas,ref string a_strErr)
        {
            if (ContractPro.UpdateProjectContract(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewFile(string id, byte[] fileByte, string FileName, ref string a_strErr)
        {
            if (ContractPro.InsertFile(id, fileByte, FileName, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewDownLoadProject(string id)
        {
            return ContractPro.GetDownloadProject(id);
        }

        public static DataTable GetNewDownloadFileProject(string id)
        {
            return ContractPro.GetDownloadFileProject(id);
        }

        public static DataTable GetNewDownLoad(string id)
        {
            return ContractPro.GetDownload(id);
        }

        public static DataTable GetNewDownloadFile(string id)
        {
            return ContractPro.GetDownloadFile(id);
        }


        public static bool DeleteNewProjectFile(string ID, ref string a_strErr)
        {
            if (ContractPro.DeleteProjectFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static bool DellNewFile(string ID, ref string a_strErr)
        {
            if (ContractPro.DeleteFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static ContractBas getNewChangeContract(string id)
        {
            return ContractPro.getChangeContractBas(id);
        }

        public static ContractBas getNewChangeProContractBas(string id)
        {
            return ContractPro.getChangeProContractBas(id);
        }

        public static bool UpdateNewContractBas(ContractBas Bas, ref string a_strErr)
        {
            if (ContractPro.UpdateContract(Bas,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }
        public static string GetNewShowCBID()
        {
            return ContractPro.GetShowCBID();
        }
        public static string GetNewCBID()
        {
            return ContractPro.GetCBID();
        }

        public static int GetNewCurAmountNum(string CID)
        {
            return ContractPro.getCurAmountNum(CID);
        }

        public static int getNewCurProjectAmountNum(string CID)
        {
            return ContractPro.getCurProjectAmountNum(CID);
        }

        public static bool InsertNewCCashBack(CCashBack Cash, ref string a_strErr)
        {
            if (ContractPro.InsertCCashBack(Cash, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool checkNewMoney(string CID, string Money)
        {
            if (ContractPro.checkMoney(CID, Money) == true)
                return true;
            else
                return false;
        }

        public static bool InsertNewProCCashBack(CCashBack Cash,string PID, ref string a_strErr)
        {
            if (ContractPro.InsertProCCashBack(Cash,PID, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewCCashBack(CCashBack Cash, ref string a_strErr)
        {
            if (ContractPro.UpdateCCashBack(Cash, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool dellNewCCashBack(string cbid, string cid, ref string a_strErr)
        {
            if (ContractPro.dellCCashBack(cbid,cid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static CCashBack getNewUpdateCashBack(string id)
        {
            return ContractPro.getUpdateCashBack(id);
        }

        public static decimal getNewDebtAmount(string CID)
        {
            return ContractPro.getDebtAmount(CID);
        }

        public static decimal getNewDebtAmountPro(string CID)
        {
            return ContractPro.getDebtAmountPro(CID);
        }

        public static bool InsertNewSettlement(CSettlement CST, ref string a_strErr)
        {
            if (ContractPro.InsertSettlement(CST, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewSettlementPro(CSettlement CST, string PID,ref string a_strErr)
        {
            if (ContractPro.InsertSettlementPro(CST, PID,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static DataTable getNewDetailContract(string where)
        {
            return ContractPro.getDetailContract(where);
        }
        public static string GetNewshowCBID(string CID)
        {
            return ContractPro.getCBID(CID);
        }
    }
}
