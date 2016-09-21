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
    public class ProjectMan
    {
        public static string GetNewShowPid()
        {
            return ProjectPro.GetShowPid();
        }
        public static string GetNewPid()
        {
            return ProjectPro.GetPid();
        }

        public static List<string> GetNewProID()
        {
            List<string> list = new List<string>();
            DataTable dt = ProjectPro.getProID();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["ProID"].ToString());
            }
            return list;
        }

        public static List<string> GetNewAppID()
        {
            List<string> list = new List<string>();
            DataTable dt = ProjectPro.getAppID();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["AppID"].ToString());
            }
            return list;
        }

        public static List<SelectListItem> GetNewFollowPerson()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.GetFollowPerson();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["UserId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["UserName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewPrincipal()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.getPrincipal();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["UserId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["UserName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> getNewConcertPerson()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.getConcertPerson();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["UserId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["UserName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigContent(string type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.GetConfigContent(type);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewPsource()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.GetPsource();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewEarlyType(string type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProjectPro.GetConfigContent(type);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static bool InsertNewProject(tk_ProjectPre Bas, ref string a_strErr)
        {
            if (ProjectPro.InsertProject(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        //public static bool InsertNewProjectBas(tk_ProjectBas Bas, ref string a_strErr)
        //{
        //    if (ProjectPro.InsertProjectBas(Bas,ref a_strErr) >= 1)
        //        return true;
        //    else
        //        return false;
        //}

        public static tk_ProjectPre getNewUpdateProjectBas(string id)
        {
            return ProjectPro.getUpdateProjectBas(id);
        }

        public static bool UpdateNewProjectBas(tk_ProjectPre Bas, ref string a_strErr)
        {
            if (ProjectPro.UpdateProjectBas(Bas, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewProjectBas(string PID, ref string a_strErr)
        {
            if (ProjectPro.DeleteProjectBas(PID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewImProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getImProjectGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewUserLogGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getUserLogGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectQQGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectQQGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewPrepareGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPrepareGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_Project getNewProjectNew(string PID)
        {
            return ProjectPro.getProjectNew(PID);
        }

        public static DataTable changeNewJQType(string PID, string JQ) 
        {
            return ProjectPro.changeJQType(PID, JQ);
        }

        public static bool InsertNewProject(tk_Project Project, ref string a_strErr)
        {
            if (ProjectPro.InsertProject(Project, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static tk_Project getNewUpdateProject(string id)
        {
            return ProjectPro.getUpdateProject(id);
        }

        public static bool UpdateNewProject(string EID, tk_Project Project, ref string a_strErr)
        {
            if (ProjectPro.UpdateProject(EID,Project, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewProject(string EID,string PID, ref string a_strErr)
        {
            if (ProjectPro.DeleteProject(EID,PID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable getNewDetailBas(string where)
        {
            return ProjectPro.GetDetailBas(where);
        }

        public static DataTable getNewDetailJQ(string where)
        {
            return ProjectPro.GetDetailJQ(where);
        }

        public static UIDataTable getNewAppProjectGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getAppProjectGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewUseProjectBas(UseProjectBas Bas, ref string a_strErr)
        {
            if (ProjectPro.InsertUseProjectBas(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool AppNewProjectBas(tk_ProjectBas Bas, ref string a_strErr)
        {
            if (ProjectPro.AppProjectBas(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UseProjectBas getNewUseUpdateSetUp(string id)
        {
            return ProjectPro.getUseUpdateSetUp(id);
        }

        public static tk_ProjectBas getNewUpdateSetUp(string id)
        {
            return ProjectPro.getUpdateSetUp(id);
        }

        public static bool UseUpNewUpSetUp(UseProjectBas Bas, ref string a_strErr)
        {
            if (ProjectPro.UseUpNewSetUp(Bas, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewSetUp(tk_ProjectBas Bas, ref string a_strErr)
        {
            if (ProjectPro.UpdateSetUp(Bas, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool deleteNewUseApp(string PID, ref string a_strErr)
        {
            if (ProjectPro.deleteUseApp(PID, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool deleteNewApp(string PID, ref string a_strErr)
        {
            if (ProjectPro.deleteApp(PID, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static DataTable getNewDetailApp(string where)
        {
            return ProjectPro.GetDetailApp(where);
        }

        public static DataTable getNewUmExamine(string BuType)
        {
            return ProjectPro.getUmExamine(BuType);
        }

        public static DataTable getNewUmExamineContent(string StrRID, string BuType)
        {
            return ProjectPro.getUmExamineContent(StrRID,BuType);
        }

        public static string GetNewShowSPid()
        {
            return ProjectPro.GetShowSPid();
        }

        public static string GetNewSPid()
        {
            return ProjectPro.GetSPid();
        }

        public static UIDataTable getNewAppExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getAppExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static string getNewDesignID(string pid)
        {
            return ProjectPro.getsid(pid);
        }

        public static bool InsertNewPreDesign(tk_PreDesign Design, ref string a_strErr)
        {
            if (ProjectPro.InsertPreDesign(Design,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static string getNewOfferID(string pid)
        {
            return ProjectPro.getoid(pid);
        }

        public static bool InsertNewPrice(tk_Price Price, ref string a_strErr)
        {
            if (ProjectPro.InsertPrice(Price, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewPrice(tk_Price Price, ref string a_strErr)
        {
            if (ProjectPro.UpdatePrice(Price, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewDesignGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getDesignGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewPriceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPriceGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectDesignGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectDesignGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectPriceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectPriceGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_Price getNewUpdatePrice(string id)
        {
            return ProjectPro.getUpdatePrice(id);
        }

        public static tk_PreDesign getNewUpdatePreDesign(string id)
        {
            return ProjectPro.getUpdatePreDesign(id);
        }

        public static DataTable GetNewCashBackDownloadOne(string id)
        {
            return ProjectPro.GetCashBackDownloadOne(id);
        }

        public static DataTable GetNewCashBackDownload(string id)
        {
            return ProjectPro.GetCashBackDownload(id);
        }

        public static DataTable GetNewDownloadCompletions(string id)
        {
            return ProjectPro.GetDownloadCompletions(id);
        }

        public static DataTable GetNewDownLoad(string id)
        {
            return ProjectPro.GetDownload(id);
        }

        public static DataTable GetNewPriceDownload(string id)
        {
            return ProjectPro.GetPriceDownload(id);
        }

        public static DataTable GetNewBudgetDownload(string id)
        {
            return ProjectPro.GetBudgetDownload(id);
        }

        public static DataTable GetNewSubWorkDownload(string id)
        {
            return ProjectPro.GetSubWorkDownload(id);
        }

        public static bool DeleteNewCashBackFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteCashBackFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewCompletionsFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteCompletionsFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DellNewFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewPriceFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeletePriceFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewBiddingFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteBiddingFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewBudgetFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteBudgetFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewSubWorkFile(string ID, ref string a_strErr)
        {
            if (ProjectPro.DeleteSubWorkFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewPreDesign(tk_PreDesign Design,ref string a_strErr)
        {
            if (ProjectPro.UpdatePreDesign(Design, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static DataTable GetNewDownloadFile(string id)
        {
            return ProjectPro.GetDownloadFile(id);
        }

        public static DataTable GetNewDownloadPriceFile(string id)
        {
            return ProjectPro.GetDownloadPriceFile(id);
        }

        public static DataTable GetNewDownloadBudgetFile(string id)
        {
            return ProjectPro.GetDownloadBudgetFile(id);
        }

        public static DataTable GetNewDownloadSubWorkFile(string id)
        {
            return ProjectPro.GetDownloadSubWorkFile(id);
        }

        public static bool DeleteNewDesign(string ID,string pid, ref string a_strErr)
        {
            if (ProjectPro.DeleteDesign(ID,pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewPrice(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeletePrice(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewDesignExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getDesignExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewPriceExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPriceExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewBudgetExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getBudgetExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewBiddingExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getBiddingExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static string GetNewShowBid()
        {
            return ProjectPro.GetShowBid();
        }
        public static string GetNewBid()
        {
            return ProjectPro.GetBid();
        }

        public static string GetNewBudetid(string PID)
        {
            return ProjectPro.getbid(PID);
        }

        public static bool InsertNewBudget(tk_Budget Budget, ref string a_strErr)
        {
            if (ProjectPro.InsertBudget(Budget,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewBudgetGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getBudgetGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectBudgetGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectBudgetGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_Budget getNewUpdateBudget(string bid)
        {
            return ProjectPro.getUpdateBudget(bid);
        }

        public static bool UpdateNewBudget(tk_Budget Budget, ref string a_strErr)
        {
            if (ProjectPro.UpdateBudget(Budget, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewBudget(string ID,string pid ,ref string a_strErr)
        {
            if (ProjectPro.DeleteBudget(ID,pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static string GetNewShowBiddingID()
        {
            return ProjectPro.GetShowBiddingID();
        }
        public static string GetNewBiddingID(string PID)
        {
            return ProjectPro.getbiddingid(PID);
        }

        public static bool InsertNewBidding(tk_Bidding Bidding, ref string a_strErr)
        {
            if (ProjectPro.InsertBidding(Bidding,  ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewBiddingGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getBiddingGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectBiddingGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectBiddingGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_Bidding getNewUpdateBidding(string id)
        {
            return ProjectPro.getUpdateBidding(id);
        }

        public static DataTable GetNewBiddingDownload(string id)
        {
            return ProjectPro.GetBiddingDownload(id);
        }

        public static bool UpdateNewBidding(tk_Bidding Bidding,  ref string a_strErr)
        {
            if (ProjectPro.UpdateBidding(Bidding,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewBidding(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeleteBidding(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewDownloadBiddingFile(string id)
        {
            return ProjectPro.GetDownloadBiddingFile(id);
        }

        public static bool InsertNewSchedule(tk_Schedule Schedule, ref string a_strErr)
        {
            if (ProjectPro.InsertSchedule(Schedule, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewScheduleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getScheduleGrid(a_intPageSize, a_intPageIndex, where);
        }

        //public static UIDataTable getNewScheduleGrid(int a_intPageSize, int a_intPageIndex, string where)
        //{
        //    return ProjectPro.getScheduleGrid(a_intPageSize, a_intPageIndex, where);
        //}

        public static UIDataTable getNewProjectScheduleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectScheduleGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_Schedule getNewUpdateSchedule(string id)
        {
            return ProjectPro.getUpdateSchedule(id);
        }

        public static bool UpdateNewSchedule(tk_Schedule Schedule,string ID, ref string a_strErr)
        {
            if (ProjectPro.UpdateSchedule(Schedule,ID, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewSchedule(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeleteSchedule(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static string GetNewShowSubID()
        {
            return ProjectPro.GetShowSubID();
        }
        public static string GetNewSubID()
        {
            return ProjectPro.GetSubID();
        }
        public static string GetNewSubWorkID(string PID)
        {
            return ProjectPro.getEID(PID);
        }

        public static bool InsertNewSubWork(tk_SubWork Sub,  ref string a_strErr)
        {
            if (ProjectPro.InsertSubWork(Sub,ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewSubWorkGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getSubWorkGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewPurchaseGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPurchaseGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectSubWorkGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectSubWorkGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_SubWork getNewUpdateSubWork(string id)
        {
            return ProjectPro.getUpdateSubWork(id);
        }

        public static bool updateNewSubWork(tk_SubWork Sub,  ref string a_strErr)
        {
            if (ProjectPro.updateSubWork(Sub,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewSubWork(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeleteSubWork(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewPrintSubWork(string where)
        {
            return ProjectPro.GetPrintSubWork(where);
        }

        public static DataTable GetNewPrintPurchase(string where)
        {
            return ProjectPro.GetPrintPurchase(where);
        }

        public static UIDataTable getNewPurchaseExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPurchaseExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewSubWorkExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getSubWorkExamineGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetNewShowPackageID()
        {
            return ProjectPro.GetShowPackageID();
        }
        public static string GetNewPackageID()
        {
            return ProjectPro.GetPackageID();
        }

        public static bool InsertNewPurchase(tk_ProjectPurchase Purchase, ref string a_strErr)
        {
            if (ProjectPro.InsertPurchase(Purchase, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewPurchase(tk_ProjectPurchase Purchase, ref string a_strErr)
        {
            if (ProjectPro.UpdatePurchase(Purchase, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewSubPackage(tk_SubPackage Package, ref string a_strErr)
        {
            if (ProjectPro.InsertSubPackage(Package, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewSubPackageGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getSubPackageGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectPurchaseGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectPurchaseGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectSubPackageGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectSubPackageGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_SubPackage getNewUpdateSubPackage(string id)
        {
            return ProjectPro.getUpdateSubPackage(id);
        }

        public static tk_ProjectPurchase getNewUpdatePurchase(string id)
        {
            return ProjectPro.getUpdatePurchase(id);
        }

        public static bool updateNewSubPackage(tk_SubPackage Package, ref string a_strErr)
        {
            if (ProjectPro.updateSubPackage(Package, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewPurchase(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeletePurchase(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewSubPackage(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeleteSubPackage(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewPrintSubPackage(string where)
        {
            return ProjectPro.GetPrintSubPackage(where);
        }

        public static UIDataTable getNewSubPackageExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getSubPackageExamineGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetNewShowPayID()
        {
            return ProjectPro.GetShowPayID();
        }
        public static string GetNewPayID()
        {
            return ProjectPro.GetPayID();
        }

        public static bool InsertNewPayRecord(tk_PayRecord Pay, ref string a_strErr)
        {
            if (ProjectPro.InsertPayRecord(Pay, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewPayRecordGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPayRecordGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectPayRecordGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectPayRecordGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_PayRecord getNewUpdatePayRecord(string id)
        {
            return ProjectPro.getUpdatePayRecord(id);
        }

        public static bool UpdateNewPayRecord(tk_PayRecord Pay, ref string a_strErr)
        {
            if (ProjectPro.UpdatePayRecord(Pay, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool DeleteNewPayRecord(string ID, string pid, ref string a_strErr)
        {
            if (ProjectPro.DeletePayRecord(ID, pid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable getNewPayRecordSTA(string where)
        {
            return ProjectPro.getPayRecordSTA(where);
        }

        public static DataTable getNewPrintPayRecord(string where)
        {
            return ProjectPro.getPrintPayRecord(where);
        }

        public static UIDataTable getNewPayRecoudExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPayRecoudExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static string getsgid(string pid)
        {
            return ProjectPro.getsgid(pid);
        }

        public static bool InsertNewProCompletions(tk_ProCompletions Com, ref string a_strErr)
        {
            if (ProjectPro.InsertProCompletions(Com,  ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewProCompletionsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProCompletionsGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectCompletionsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectCompletionsGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_ProCompletions getNewUpdateProCompletions(string id)
        {
            return ProjectPro.getUpdateProCompletions(id);
        }

        public static DataTable GetNewProCompletionsDownload(string id)
        {
            return ProjectPro.GetProCompletionsDownload(id);
        }

        public static bool UpdateNewProCompletions(tk_ProCompletions Com, ref string a_strErr)
        {
            if (ProjectPro.UpdateProCompletions(Com, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewProFinish(tk_ProFinish Finish, ref string a_strErr)
        {
            if (ProjectPro.InsertProFinish(Finish, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewProFinishGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProFinishGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectFinishGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectFinishGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static tk_ProFinish getNewUpdateProFinish(string id)
        {
            return ProjectPro.getUpdateProFinish(id);
        }

        public static bool UpdateNewProFinish(tk_ProFinish Finish, ref string a_strErr)
        {
            if (ProjectPro.UpdateProFinish(Finish, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewProjectCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProjectCashBackGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewCPlanTimeWarnGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getCPlanTimeWarnGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewDebtGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getDebtGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable getNewProjectStatisticsdata(string where)
        {
            return ProjectPro.getProjectStatisticsdata(where);
        }

        public static string getNewCountProject(string where)
        {
            return ProjectPro.getCountProject(where);
        }

        public static DataTable getNewAmountStatisticsdata(string where)
        {
            return ProjectPro.getAmountStatisticsdata(where);
        }

        public static string getNewCountAmount(string where)
        {
            return ProjectPro.getCountAmount(where);
        }

        public static DataTable getNewDebtStatisticsdata(string where)
        {
            return ProjectPro.getDebtStatisticsdata(where);
        }

        public static string getNewCountDebt(string where)
        {
            return ProjectPro.getCountDebt(where);
        }

        public static UIDataTable getNewPurchaseSearchGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPurchaseSearchGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewOrderGoodsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getOrderGoodsGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getNewProjectContractExamineGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.ProjectContractExamineGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable getnproductExamineGrid(int a_intPageSize, int a_intPageIndex, string where,string proName)
        {
            return ProjectPro.getproductExamineGrid(a_intPageSize, a_intPageIndex, where,proName);
        }
        public static UIDataTable getPlanList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getPlanList(a_intPageSize, a_intPageIndex, where);
        }

        public static string getNewProjectCBID(string CID)
        {
            return ProjectPro.getProjectCBID(CID);
        }

        public static int getNewCurProjectAmountNum(string CID)
        {
            return ProjectPro.getCurProjectAmountNum(CID);
        }

        public static bool checkNewMoney(string CID, string Money)
        {
            if (ProjectPro.checkMoney(CID, Money) == true)
                return true;
            else
                return false;
        }

        public static bool InsertNewContratFile(ContractBas Bas, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertContratFile(Bas, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewBiddingFile(tk_Bidding Bidding, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertBiddingFile(Bidding, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewProCompletionsFile(tk_ProCompletions Com, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertProCompletionsFile(Com, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewSubWorkFile(tk_SubWork Sub, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertSubWorkFile(Sub, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNEWBudgetFile(tk_Budget Budget, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertBudgetFile(Budget, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewPriceFile(tk_Price Price, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertPriceFile(Price, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewPreDesignFile(tk_PreDesign Design, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertPreDesignFile(Design, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewProjectCCashBackFile(CCashBack Cash, HttpFileCollection Filedata, ref string a_strErr)
        {
            if (ProjectPro.InsertProjectCCashBackFile(Cash, Filedata, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewProjectCCashBack(CCashBack Cash, ref string a_strErr)
        {
            if (ProjectPro.InsertProjectCCashBack(Cash,ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewProCashBackGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProjectPro.getProCashBackGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static CCashBack getNewUpdateCashBack(string id)
        {
            return ProjectPro.getUpdateCashBack(id);
        }

        public static bool checkNewUpdateMoney(string CID, string Money, string CBID)
        {
            if (ProjectPro.checkUpdateMoney(CID, Money, CBID) == true)
                return true;
            else
                return false;
        }

        public static bool UpdateNewProjectCCashBack(CCashBack Cash, ref string a_strErr)
        {
            if (ProjectPro.UpdateProjectCCashBack(Cash, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool dellNewCCashBack(string cbid, string cid, ref string a_strErr)
        {
            if (ProjectPro.dellCCashBack(cbid, cid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
    }
}
