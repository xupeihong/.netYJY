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
    public class MandateInfoMan
    {
        public static List<SelectListItem> GetTestType()
        {

            DataTable dt = MandateInfoPro.GetTestType();
            return GetList(dt);
        }

        public static List<SelectListItem> GetTestType(string FatherTestType)
        {

            DataTable dt = MandateInfoPro.GetTestType(FatherTestType);
            return GetList(dt);
        }

        public static List<SelectListItem> GetList(DataTable dt)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            list.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["TID"].ToString();
                SelListItem.Text = dt.Rows[i]["ChildTestType"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }

        public static List<SelectListItem> GetTestTypeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetConfigInfo("", "testType");
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            list.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i][1].ToString();
                SelListItem.Text = dt.Rows[i][1].ToString();
                list.Add(SelListItem);
            }
            return list;
        }

        public static List<SelectListItem> GetFatherTestType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetFatherTestType();
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            list.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["FatherTestType"].ToString();
                SelListItem.Text = dt.Rows[i]["FatherTestType"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }

        public static List<SelectListItem> GetTestItems(string TID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetTestItems(TID);
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            list.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ItemID"].ToString();
                SelListItem.Text = dt.Rows[i]["ItemContent"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }

        public static List<SelectListItem> GetPayId(string PayCompany)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetPayId(PayCompany);
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            list.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i][0].ToString();
                SelListItem.Text = dt.Rows[i][0].ToString();
                list.Add(SelListItem);
            }
            return list;
        }

        public static List<SelectListItem> GetPayType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetState("PayType");
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                SelListItem.Value = dt.Rows[i]["Id"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }
        public static List<SelectListItem> GetPaymentMethod()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetState("PaymentMethod");
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                SelListItem.Value = dt.Rows[i]["Id"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }
        public static List<SelectListItem> GetPayCompany(string PayCompany)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dt = MandateInfoPro.GetPayCompany();
            if (dt == null)
                return list;
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Text = dt.Rows[i][0].ToString();
                SelListItem.Value = dt.Rows[i][0].ToString();
                if (dt.Rows[i][0].ToString() == PayCompany)
                    SelListItem.Selected = true;
                list.Add(SelListItem);
            }
            return list;
        }

        public static double GetPayMoney(string ClienName)
        {
            return MandateInfoPro.GetPayMoney(ClienName);
        }

        public static double GetMandateCharge(string YYCode)
        {
            return MandateInfoPro.GetMandateCharge(YYCode);
        }
        public static DataTable GetMandateChargeInfo(string where)
        {
            return MandateInfoPro.GetMandateChargeInfo(where);
        }
        public static DataTable GetConfigInfo(string condition, string TaskType)
        {
            return MandateInfoPro.GetConfigInfo(condition, TaskType);
        }
        public static DataTable GetState(string type)
        {
            return MandateInfoPro.GetState(type);
        }
        public static string GetPayId()
        {
            return MandateInfoPro.GetPayId();
        }
        public static string GetYYCode()
        {
            return MandateInfoPro.GetYYCode();
        }
        public static string GetMCode(string type)
        {
            return MandateInfoPro.GetMCode(type);
        }
        public static PayInfo GetPayInfo(string PayId)
        {
            return MandateInfoPro.GetPayInfo(PayId);
        }
        public static DataTable GetBasisAndItem(string SampleName)
        {
            return MandateInfoPro.GetBasisAndItem(SampleName);
        }

        public static DataTable GetClienInfo(string ClienName)
        {
            return MandateInfoPro.GetClienInfo(ClienName);
        }

        public static bool InsertMandate(MandateInfo mandate, List<SampleInfo> sampleList, ref string err)
        {
            return MandateInfoPro.InsertMandate(mandate, sampleList, ref err);
        }
        public static bool UpdateMandate(MandateInfo mandate, List<SampleInfo> sampleList, ref string err)
        {
            return MandateInfoPro.UpdateMandate(mandate, sampleList, ref err);
        }
        public static UIDataTable getMandateGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return MandateInfoPro.getMandateGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static MandateInfo GetMandateInfo(string YYCode)
        {
            return MandateInfoPro.GetMandateInfo(YYCode);
        }

        public static DataTable GetSampleInfo(string YYCode)
        {
            return MandateInfoPro.GetSampleInfo(YYCode);
        }

        public static string GetTestItemIDs(string YYCode)
        {
            return MandateInfoPro.GetTestItemIDs(YYCode);
        }
        public static bool SaveAccept(string YYCode, string MCode, string AcceptPeople, string type, ref string err)
        {
            return MandateInfoPro.SaveAccept(YYCode, MCode, AcceptPeople, type, ref err);
        }
        public static DataTable GetMandateInfoByYYCode(string YYCode)
        {
            return MandateInfoPro.GetMandateInfoByYYCode(YYCode);
        }
        public static DataTable GetSampleInfoByYYCode(string YYCode)
        {
            return MandateInfoPro.GetSampleInfoByYYCode(YYCode);
        }

        public static bool IsPayState(string YYCode)
        {
            return MandateInfoPro.IsPayState(YYCode);
        }
        public static bool SavePayInfo(PayInfo payInfo, List<ConsumptionInfo> list, double yk, ref string err)
        {
            return MandateInfoPro.SavePayInfo(payInfo, list, yk, ref err);
        }
        public static bool UpdatePayInfo(PayInfo payInfo, string YYCode, ref string err)
        {
            return MandateInfoPro.UpdatePayInfo(payInfo, YYCode, ref err);
        }
        public static UIDataTable GetPayGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return MandateInfoPro.GetPayGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetLedger(string where)
        {
            return MandateInfoPro.GetLedger(where);
        }
        public static string GetLedgerMoneyAnaNumber(string where)
        {
            return MandateInfoPro.GetLedgerMoneyAnaNumber(where);
        }
        public static bool SaveRepeal(string YYCode, string RepealReason, string MCode)
        {
            return MandateInfoPro.SaveRepeal(YYCode, RepealReason, MCode);
        }

        public static DataSet GetJYFX(int month)
        {
            return MandateInfoPro.GetJYFX(month);
        }

        public static DataTable GetOperationTask(int month, ref string istask)
        {
            return MandateInfoPro.GetOperationTask(month, ref istask);
        }
        public static OperationAnalysis getOperationAnalysis(int month)
        {
            return MandateInfoPro.getOperationAnalysis(month);
        }
        public static string GetOId()
        {
            return MandateInfoPro.GetOId();
        }
        public static bool SaveOperationAnalysis(OperationAnalysis oa, List<OperationTask> list, ref string err)
        {
            return MandateInfoPro.SaveOperationAnalysis(oa, list, ref err);
        }

        public static UIDataTable GetOperationAnalysisGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return MandateInfoPro.GetOperationAnalysisGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static string SKTZ(string MCode)
        {
            return MandateInfoPro.SKTZ(MCode);
        }
    }
}
