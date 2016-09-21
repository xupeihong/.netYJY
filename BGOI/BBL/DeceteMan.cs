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
    public class DeceteMan
    {
        public static string GetNewShowTaskID()
        {
            return DecetePro.GetShowTaskID();
        }

        public static string GetNewTaskID()
        {
            return DecetePro.GetTaskID();
        }

        public static DataTable GetNewConfigCont(string Type)
        {
            return DecetePro.GetConfigCont(Type);
        }

        public static List<SelectListItem> GetNewConfigUnit(string Type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigUnit(Type);
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
                SelListItem.Value = dtDesc.Rows[i]["UnitID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigSelectUser(string Type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetUser(Type);
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
                SelListItem.Value = dtDesc.Rows[i]["UnitID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigContent(string Type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigCont(Type);
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

        public static List<SelectListItem> GetNewConfigPipe()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigPipe();
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
                SelListItem.Value = dtDesc.Rows[i]["PipeID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewProductSpec()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetProductSpec();
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
                SelListItem.Value = dtDesc.Rows[i]["PipeID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigStandSize()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigRTSize();
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
                SelListItem.Value = dtDesc.Rows[i]["SizeID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigTZPly()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigTZPly();
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
                SelListItem.Value = dtDesc.Rows[i]["PlyID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigRative()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigContRative();
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
                SelListItem.Value = dtDesc.Rows[i]["ID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["RID"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetNewConfigUser()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetConfigUser();
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

        public static List<SelectListItem> GetNewDeceteUnit()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.GetDeceteUnit();
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
                SelListItem.Value = dtDesc.Rows[i]["UnitCode"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["UnitName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetNewUser(string unitid)
        {
            return DecetePro.GetUser(unitid);
        }

        public static DataTable GetNewPhone(string uid)
        {
            return DecetePro.GetPhone(uid);
        }

        public static bool InsertNewEntrust(EntrustTask Task, string Press, string PipSize, string length, ref string a_strErr)
        {
            if (DecetePro.InsertEntrustTask(Task, Press, PipSize, length, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewFinishEntrustTask(EntrustTask Task, ref string a_strErr)
        {
            if (DecetePro.InsertFinishEntrustTask(Task, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewEntrust(EntrustTask Task, string Press, string PipSize, string length, ref string a_strErr)
        {
            if (DecetePro.UpdateEntrustTask(Task, Press, PipSize, length, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static DataTable getNewJudgeDecete(string TaskID)
        {
            return DecetePro.getJudgeDecete(TaskID);
        }

        public static DataTable getNewJudgeFinishDecete(string TaskID)
        {
            return DecetePro.getJudgeFinishDecete(TaskID);
        }

        public static DataTable getNewJudgeTask(string Decete)
        {
            return DecetePro.getJudgeTask(Decete);
        }

        public static DataTable getNewFinishTask(string Decete)
        {
            return DecetePro.getJudgeFinishTask(Decete);
        }

        public static DataTable getNewDetailDecete(string TaskID)
        {
            return DecetePro.GetDetailDecete(TaskID);
        }

        public static DataTable getNewDetailTask(string TaskID)
        {
            return DecetePro.getDetailTask(TaskID);
        }

        public static bool deleteNewEntrustTask(string TaskID, ref string a_strErr)
        {
            if (DecetePro.deleteEntrustTask(TaskID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool deleteNewDecete(string Decete, ref string a_strErr)
        {
            if (DecetePro.deleteDecete(Decete, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewEntrustContent(string uid)
        {
            return DecetePro.getEntrustContent(uid);
        }

        public static EntrustTask getNewUpdateEntrustTask(string id)
        {
            return DecetePro.getUpdateEntrustTask(id);
        }

        public static UIDataTable getNewEntrustGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getEntrustGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetNewPrintEntrustByTaskID(string TaskID)
        {
            return DecetePro.GetPrintEntrustByTaskID(TaskID);
        }

        public static DataTable GetNewPrinEntrustContent(string TaskID)
        {
            return DecetePro.GetPrinEntrustContent(TaskID);
        }

        public static string GetNewShowDeceteID(string type)
        {
            return DecetePro.GetShowDeceteID(type);
        }

        public static string GetNewDeceteID(string type)
        {
            return DecetePro.GetDeceteID(type);
        }

        public static bool InsertNewDecete(Decete Dec, ref string a_strErr)
        {
            if (DecetePro.InsertDecete(Dec, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewFinishDecete(Decete Dec, ref string a_strErr)
        {
            if (DecetePro.InsertFinishDecete(Dec, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewDeceteGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getDeceteGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static Decete getNewUpdateDecete(string DeceteID)
        {
            return DecetePro.getUpdateDecete(DeceteID);
        }

        public static bool UpdateNewDecete(Decete Dec, ref string a_strErr)
        {
            if (DecetePro.UpdateDecete(Dec, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static string getNewShowTaskNumber(string DeceteID)
        {
            return DecetePro.GetShowTaskNumber(DeceteID);
        }

        public static string getNewTaskNumber(string DeceteID)
        {
            return DecetePro.GetTaskNumber(DeceteID);
        }

        public static Task getNewUpdateTask(string id)
        {
            return DecetePro.getUpdateTask(id);
        }

        public static bool InsertNewTask(Task task, ref string a_strErr)
        {
            if (DecetePro.InsertTask(task, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool FinishNewTask(Task task, ref string a_strErr)
        {
            if (DecetePro.FinishTask(task, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool UpdateNewTask(Task task, ref string a_strErr)
        {
            if (DecetePro.UpdateTask(task, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool deleteNewTask(string TaskNumber, ref string a_strErr)
        {
            if (DecetePro.deleteTask(TaskNumber, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewTaskGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getTaskGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static CardMT getNewHaveMT()
        {
            return DecetePro.getHaveMT();
        }

        public static string getNewShowCardNumber(string DeceteID)
        {
            return DecetePro.GetShowCardNumber(DeceteID);
        }

        public static CardPT getNewHaveCardPT()
        {
            return DecetePro.getHaveCardPT();
        }

        public static DataTable getNewPipeSize(string PipeID)
        {
            return DecetePro.getPipeSize(PipeID);
        }
        public static List<SelectListItem> getNewPipeSize()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.getPipeSize();
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

        public static List<SelectListItem> getNewPipeSize2()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = DecetePro.getPipeSize2();
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

        public static DataTable getNewTZPly(string PipeID)
        {
            return DecetePro.GetTZPly(PipeID);
        }

        public static string getNewCardNumber(string DeceteID)
        {
            return DecetePro.GetCardNumber(DeceteID);
        }

        public static string getNewCKmodelUT(string ProductSpec, string MumTxt)
        {
            return DecetePro.getCKmodelUT(ProductSpec, MumTxt);
        }

        public static string getNewCKmodelRT(string StrPipeStand, string StrStandSize, string StrRttype, string StrTZType, string StrTZPly)
        {
            return DecetePro.getCKmodelRT(StrPipeStand, StrStandSize, StrRttype, StrTZType, StrTZPly);
        }

        public static bool InsertNewTechlCardRTmodel(CardRT CardRT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardRTmodel(CardRT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewTechlCardRT(CardRT CardRT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardRT(CardRT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewTechlCardRT(CardRT CardRT, ref string a_strErr)
        {
            if (DecetePro.UpdateTechlCardRT(CardRT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewTechlCardRT(string id, ref string a_strErr)
        {
            if (DecetePro.DeleteTechlCardRT(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewTechlCardUT(string id, ref string a_strErr)
        {
            if (DecetePro.DeleteTechlCardUT(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewTechlCardPT(string id, ref string a_strErr)
        {
            if (DecetePro.DeleteTechlCardPT(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static CardPT getNewUpdateCardPT(string id)
        {
            return DecetePro.getUpdateCardPT(id);
        }

        public static CardUT getNewUpdateCardUT(string id)
        {
            return DecetePro.getUpdateCardUT(id);
        }

        public static CardMT getNewUpdateCardMT(string id)
        {
            return DecetePro.getUpdateCardMT(id);
        }

        public static bool DeleteNewTechlCardMT(string id, ref string a_strErr)
        {
            if (DecetePro.DeleteTechlCardMT(id, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewTechlCardUTModel(CardUT CardUT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardUTModel(CardUT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool updateNewTechlCardUT(CardUT CardUT, ref string a_strErr)
        {
            if (DecetePro.updateTechlCardUT(CardUT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewTechlCardUT(CardUT CardUT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardUT(CardUT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewTechlCardMT(CardMT CardMT, ref string a_strErr)
        {
            if (DecetePro.UpdateTechlCardMT(CardMT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool InsertNewTechlCardMT(CardMT CardMT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardMT(CardMT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewTechlCardPT(CardPT CardPT, ref string a_strErr)
        {
            if (DecetePro.InsertTechlCardPT(CardPT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool UpdateNewTechlCardPT(CardPT CardPT, ref string a_strErr)
        {
            if (DecetePro.UpdateTechlCardPT(CardPT, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewCardRTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getCardRTGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static CardRT getNewUpdateCardRT(string id)
        {
            return DecetePro.getUpdateCarRT(id);
        }

        public static DataTable GetNewPrintCardRT(string where)
        {
            return DecetePro.GetPrintCardRT(where);
        }

        public static UIDataTable getNewCardUTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getCardUTGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetNewPrintCardUT(string where)
        {
            return DecetePro.GetPrintCardUT(where);
        }

        public static UIDataTable getNewCardMTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getCardMTGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetNewPrintCardMT(string where)
        {
            return DecetePro.GetPrintCardMT(where);
        }

        public static UIDataTable getNewCardPTGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getCardPTGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetNewPrintCardPT(string where)
        {
            return DecetePro.GetPrintCardPT(where);
        }

        public static UIDataTable getNewCarGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getCarGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewIssuedCar(string taskNumber, string car, ref string a_strErr)
        {
            if (DecetePro.InsertIssuedCar(taskNumber, car, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return DecetePro.getUserGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertNewIssuedOther(IssuedOtherTask OtherTask, ref string a_strErr)
        {
            if (DecetePro.InsertIssuedOther(OtherTask, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static bool InsertNewIssuedRTTask(IssuedTaskRT TaskRT, ref string a_strErr)
        {
            if (DecetePro.InsertIssuedRTTask(TaskRT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        public static DataTable GetNewPrintTaskRT(string where)
        {
            return DecetePro.GetPrintTaskRT(where);
        }





        // 20150721 ly 获取委托单详细 
        public static string getDrawNum(string TaskID, string TaskName)
        {
            return DecetePro.getDrawNum(TaskID, TaskName);
        }
        // 获取工程编号 
        public static string getProjectNum(string p1, string p2)
        {
            throw new NotImplementedException();
        }
        // 获取管径规格、管径长度 
        public static string getNewPipe(string TaskID)
        {
            return DecetePro.getNewPipe(TaskID);
        }
        // 获取检测工作量列表 
        public static string getDeceteListNew(string taskID, string length, ref string a_strErr)
        {
            DataTable dtDeceteList = new DataTable();
            dtDeceteList = DecetePro.getDeceteListNew(taskID, length, ref a_strErr);

            if (dtDeceteList == null)
                return "";
            if (dtDeceteList.Rows.Count == 0)
                return "";

            string strDeceteList = GFun.Dt2Json("DeceteList", dtDeceteList);
            return strDeceteList;
        }

        // 获取 结算信息
        public static TaskAccounts getTaskA(string taskID)
        {
            return DecetePro.getTaskA(taskID);
        }
        // 提交结算信息 
        public static bool SaveNewContract(string AccountsID, string TaskID, string AccAmount, string PostTime, string ContractPrice, string SignTime,
            string ActualPrice, string ActualTime, string KnotStyle, string IsSign, string Comments, string RepairPrice, string IsBill, ref string a_strErr)
        {
            if (DecetePro.SaveNewContract(AccountsID, TaskID, AccAmount, PostTime, ContractPrice, SignTime, ActualPrice, ActualTime, KnotStyle, IsSign,
                Comments, RepairPrice, IsBill, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 获取 预算信息 
        public static TaskBudget getBudget(string taskID)
        {
            return DecetePro.getBudget(taskID);
        }

        // 提交预算信息
        public static bool SaveNewBudget(string BudgetID, string TaskID, string PostTime, string ContractPrice, string AdvancePrice, string AppTime,
            string ProPrice, string ProTime, string Comments, ref string a_strErr)
        {
            if (DecetePro.SaveNewBudget(BudgetID, TaskID, PostTime, ContractPrice, AdvancePrice, AppTime, ProPrice, ProTime, Comments, ref a_strErr) >= 1)
                return true;
            else
                return false;

        }
    }
}
