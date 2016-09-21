using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class ProduceMan
    {
        #region 提交审批
        public static UIDataTable getNewUMwebkeyGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.getUMwebkeyGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static bool InsertNewApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            if (ProducePro.InsertApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static string GetNewShowSPid(string folderBack)
        {
            return ProducePro.GetShowSPid(folderBack);
        }
        public static string GetNewSPid(string folderBack)
        {
            return ProducePro.GetSPid(folderBack);
        }
        #endregion
        #region 审批
        public static UIDataTable CreateProjectLogGrid(int PageSize, int PageIndex, string where)
        {
            return ProducePro.CreateProjectLogGrid(PageSize, PageIndex, where);
        }
        public static string getNewwebkey(string webkey)
        {
            return ProducePro.getwebkey(webkey);
        }
      
        #endregion
        public static bool AddProduceLog(tk_ProLog logobj)
        {
            return ProducePro.AddProduceLog(logobj);
        }
        #region 任务单管理
        #region  新增生产任务
        public static DataTable IndexAllcustom(string OrderID)
        {
            return ProducePro.IndexAllcustom(OrderID);
        }

        public static UIDataTable getRZ(int PageSize, int PageIndex, string RWID)
        {
            return ProducePro.getRZ(PageSize, PageIndex, RWID);
        }

        public static DataTable getspec(string OrderContent)
        {
            return ProducePro.getspec(OrderContent);
        }

        public static UIDataTable ProduceRemainList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.ProduceRemainList(a_intPageSize, a_intPageIndex, where);
        }

        //获取生产产品名称
        public static List<SelectListItem> GetProName()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetProName();
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
                SelListItem.Value = dtDesc.Rows[i]["OrderContent"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["OrderContent"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }


        //相关单据
        public static DataTable GetRW(string a, ref string strErr)
        {
            return ProducePro.GetRW(a, ref strErr);
        }
        public static DataTable GetLL(string a, ref string strErr)
        {
            return ProducePro.GetLL(a, ref strErr);
        }
        public static DataTable GetSG(string a, ref string strErr)
        {
            return ProducePro.GetSG(a, ref strErr);
        }
        public static DataTable GetBG(string a, ref string strErr)
        {
            return ProducePro.GetBG(a, ref strErr);
        }
        public static DataTable GetRK(string a, ref string strErr)
        {
            return ProducePro.GetRK(a, ref strErr);
        }

        public static DataTable LoadTask(string RWID, ref string strErr)
        {
            return ProducePro.LoadTask(RWID, ref strErr);
        }
        //任务单相关单据
        public static UIDataTable LoadRWPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadRWPayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        //任务单详细相关单据
        public static UIDataTable LoadRWPaymentDetail(int PageSize, int PageIndex, string RWID)
        {
            return ProducePro.LoadRWPaymentDetail(PageSize, PageIndex, RWID);
        }
        //领料单相关单据
        public static UIDataTable LoadLLPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadLLPayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        //领料单详细相关单据
        public static UIDataTable LoadLLPaymentDetail(int PageSize, int PageIndex, string LLID)
        {
            return ProducePro.LoadLLPaymentDetail(PageSize, PageIndex, LLID);
        }
        //随工相关单据
        public static UIDataTable LoadSGPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadSGPayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        //随工单详细相关单据
        public static UIDataTable LoadSGPaymentDetail(int PageSize, int PageIndex, string SGID)
        {
            return ProducePro.LoadSGPaymentDetail(PageSize, PageIndex, SGID);
        }
        //报告单相关单据
        public static UIDataTable LoadBGPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadBGPayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        //报告单详细相关单据
        public static UIDataTable LoadBGPaymentDetail(int PageSize, int PageIndex, string BGID)
        {
            return ProducePro.LoadBGPaymentDetail(PageSize, PageIndex, BGID);
        }
        //入库单相关单据
        public static UIDataTable LoadRKPayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadRKPayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        //入库单详细相关单据
        public static UIDataTable LoadRKPaymentDetail(int PageSize, int PageIndex, string RKID)
        {
            return ProducePro.LoadRKPaymentDetail(PageSize, PageIndex, RKID);
        }


        public static string GetTopRWID()
        {
            return ProducePro.GetTopRWID();
        }

        //获取规格型号
        public static List<SelectListItem> GetProType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetProType();
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
                SelListItem.Value = dtDesc.Rows[i]["SpecsModels"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["SpecsModels"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }


        public static UIDataTable ProduceInDetialList(int a_intPageSize, int a_intPageIndex, string RWID)
        {
            return ProducePro.ProduceInDetialList(a_intPageSize, a_intPageIndex, RWID);
        }


        public static UIDataTable ChangeSpecsModelsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.ChangeSpecsModelsList(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            return ProducePro.GetPtype(a_intPageSize, a_intPageIndex);
        }

        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return ProducePro.ChangePtypeList(a_intPageSize, a_intPageIndex, ptype);
        }

        public static UIDataTable Changematerial(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return ProducePro.Changematerial(a_intPageSize, a_intPageIndex, ptype);
        }
        public static List<SelectListItem> GetProSpecsModels()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetProSpecsModels();
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
                SelListItem.Value = dtDesc.Rows[i]["SpecsModels"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["SpecsModels"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetTaskDetail(string OrderID)
        {
            return ProducePro.GetTaskDetail(OrderID);
        }

        public static DataTable getKCnum(string PID, string HouseID)
        {
            return ProducePro.getKCnum(PID,HouseID);
        }

        public static DataTable GetTaskDetails(string PID)
        {
            return ProducePro.GetTaskDetails(PID);
        }


        public static string ProTaskDetialNum(string RWID)
        {
            return ProducePro.ProTaskDetialNum(RWID);
        }

        //插入数据

        public static bool SaveTask(tk_ProductTask record, List<tk_ProductTDatail> delist, ref string strErr, string OrderID)
        {
            return ProducePro.SaveTask(record, delist, ref strErr, OrderID);
        }

        //public static bool SaveTask(tk_ProductTask record, List<tk_ProductTDatail> delist, ref string strErr)
        //{
        //    return ProducePro.SaveTask(record, delist, ref strErr);
        //}

        //审批信息
        //任务单详细相关单据
        public static UIDataTable SPXX(int PageSize, int PageIndex, string RWID)
        {
            return ProducePro.SPXX(PageSize, PageIndex, RWID);
        }
        #endregion

        #region 修改任务单
        public static DataTable getupdate(string RWID)
        {
            return ProducePro.getupdate(RWID);
        }

        public static bool SCTask(string DID, ref string strErr)
        {
            return ProducePro.SCTask(DID, ref strErr);
        }

        public static DataTable GetTaskDetailss(string RWID)
        {
            return ProducePro.GetTaskDetailss(RWID);
        }

        public static DataTable IndexAllTask(string RWID)
        {
            return ProducePro.IndexAllTask(RWID);
        }

        public static bool SaveUpdateTask(tk_ProductTask record, List<tk_ProductTDatail> delist, ref string strErr, string RWID)
        {
            return ProducePro.SaveUpdateTask(record, delist, ref strErr, RWID);
        }

        #endregion

        #region 撤销任务单
        public static bool CheXiaoTask(string RWID, ref string strErr)
        {
            return ProducePro.CheXiaoTask(RWID, ref strErr);
        }
        #endregion

        #region 打印
        public static DataTable PrintTasks(string strWhere, string tableName, ref string strErr)
        {
            return ProducePro.PrintTasks(strWhere, tableName, ref strErr);
        }
        public static DataTable PrintTask(string RWID)
        {
            return ProducePro.PrintTask(RWID);
        }
        public static DataTable GetApprovalTime(string PID)
        {
            return ProducePro.GetApprovalTime(PID);
        }
        #endregion

        #region  任务单详情
        public static DataTable ProTaskDetail(string RWID)
        {
            return ProducePro.ProTaskDetail(RWID);
        }

        public static tk_ProductTask ProTaskDetails(string RWID)
        {
            return ProducePro.ProTaskDetails(RWID);
        }
        #endregion

        public static DataTable getPD(string RWID)
        {
            return ProducePro.getPD(RWID);
        }

        public static DataTable getPDSP(string RWID)
        {
            return ProducePro.getPDSP(RWID);
        }

        #region   生成领料单

        public static string GetTopLLID()
        {
            return ProducePro.GetTopLLID();
        }

        public static string GetTopLID()
        {
            return ProducePro.GetTopLID();
        }

        public static string GetTopID(string LLID)
        {
            return ProducePro.GetTopID(LLID);
        }
        public static string MaterialFDetailNum(string LLID)
        {
            return ProducePro.MaterialFDetailNum(LLID);
        }
        public static DataTable GetMaterialForm(string RWID)
        {
            return ProducePro.GetMaterialForm(RWID);
        }

        public static DataTable getTnum(string DID)
        {
            return ProducePro.getTnum(DID);
        }

        public static DataTable GetMT(string PID)
        {
            return ProducePro.GetMT(PID);
        }


        public static DataTable GetMaterial(string PID)
        {
            return ProducePro.GetMaterial(PID);
        }

        public static DataTable Getcount(string PID,string a )
        {
            return ProducePro.Getcount(PID,a);
        }

        public static DataTable GetZZ(string LLID, string IdentitySharing)
        {
            return ProducePro.GetZZ(LLID, IdentitySharing);
        }

        public static DataTable GetLLXQNum(string LLID, string IdentitySharing)
        {
            return ProducePro.GetLLXQNum(LLID, IdentitySharing);
        }

        public static DataTable GetZSL(string LLID)
        {
            return ProducePro.GetZSL(LLID);
        }

        public static DataTable GetNULL(string LLID)
        {
            return ProducePro.GetNULL(LLID);
        }

        public static bool SaveMaterialFDetailIn(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, DateTime NCreateTime, string NCreateUser, string RWID, string RWIDDID, string HouseID)
        {
            return ProducePro.SaveMaterialFDetailIn(record, delist, ref strErr, LLID, NCreateTime, NCreateUser, RWID, RWIDDID, HouseID);
        }
        #endregion

        #region 生成随工单
        public static string GetTopSGID()
        {
            return ProducePro.GetTopSGID();
        }
        public static string ProductRProductNum(string SGID)
        {
            return ProducePro.ProductRProductNum(SGID);
        }

        public static DataTable GetProductRecord(string RWID)
        {
            return ProducePro.GetProductRecord(RWID);
        }

        public static DataTable GetProductRecordDID(string DID)
        {
            return ProducePro.GetProductRecordDID(DID);
        }

        public static bool SaveProductRecordIn(tk_ProductRecord record, List<tk_ProductRProduct> delist, ref string strErr, string RWID, string SGID, string HouseID)
        {
            return ProducePro.SaveProductRecordIn(record, delist, ref strErr, RWID, SGID, HouseID);
        }
        #endregion

        # region 检验报告上传
        public static string GetTopBGID()
        {
            return ProducePro.GetTopBGID();
        }

        public static string ProFileInfoNum(string BGID)
        {
            return ProducePro.ProFileInfoNum(BGID);
        }

        public static DataTable GetBGType()
        {
            return ProducePro.GetBGType();
        }

        public static DataTable GetReportInfoselect(string RWID)
        {
            return ProducePro.GetReportInfoselect(RWID);
        }

        public static DataTable GetReportInfos(string DID)
        {
            return ProducePro.GetReportInfos(DID);
        }

        public static bool SaveFileInfo(tk_FileInfo info)
        {
            return ProducePro.SaveFileInfo(info);
        }

        public static bool SaveFileInfoIn(tk_ReportInfo record, List<tk_FileInfo> delist, ref string strErr, string BGID, string CreatePerson, DateTime CreateTime)
        {
            return ProducePro.SaveFileInfoIn(record, delist, ref strErr, BGID, CreatePerson, CreateTime);
        }
        #endregion

        #region 完成入库
        public static string GetTopRKID()
        {
            return ProducePro.GetTopRKID();
        }

        public static DataTable GetReportInfo(string RWID, string RKID)
        {
            return ProducePro.GetReportInfo(RWID, RKID);
        }

        public static List<SelectListItem> GetHouseID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetHouseID();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseName"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetHouseIDs()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetHouseIDs();
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
                SelListItem.Value = dtDesc.Rows[i]["HouseID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["HouseName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static string PStockingDetailNum(string RKID)
        {
            return ProducePro.PStockingDetailNum(RKID);
        }

        public static bool SavePStockingDetailIn(tk_PStocking record, List<tk_PStockingDetail> delist, ref string strErr, string RWID, DateTime FinishTime)
        {
            return ProducePro.SavePStockingDetailIn(record, delist, ref strErr, RWID, FinishTime);
        }
        #endregion
        #endregion

        #region 领料单
        #region 领料单列表
        public static UIDataTable Materialrequisitionlist(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.Materialrequisitionlist(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable ProMaterialFDetail(string LLID)
        {
            return ProducePro.ProMaterialFDetail(LLID);
        }

        public static List<SelectListItem> GetNAME()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetNAME();
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
                SelListItem.Value = dtDesc.Rows[i]["OrderContent"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["OrderContent"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetTYPE()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetTYPE();
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
                SelListItem.Value = dtDesc.Rows[i]["SpecsModels"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["SpecsModels"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static bool SCLLDetail(string DID, ref string strErr)
        {
            return ProducePro.SCLLDetail(DID, ref strErr);
        }
        #endregion

        #region  领料单页面   新增领料单
        public static List<SelectListItem> getOrderContent()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.getOrderContent();
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
                SelListItem.Value = dtDesc.Rows[i]["OrderContent"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["OrderContent"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable gettaskll(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.gettaskll(a_intPageSize, a_intPageIndex, where);
        }

        public static bool savesomematerial(tk_MaterialForm record, string PID, string Main, string LLID, string amount, string v, string OrderContent, string SpecsModels, string Remark, string orderUnit, string DID)
        {
            return ProducePro.savesomematerial(record, PID, Main, LLID, amount, v, OrderContent, SpecsModels, Remark, orderUnit, DID);
        }
        #endregion

        #region 修改领料单
        public static DataTable GetTaskNum(string LLID, string RWIDDID, string RWID)
        {
            return ProducePro.GetTaskNum(LLID, RWIDDID, RWID);
        }

        public static DataTable GetMaterialFormTaskdetail(string LLID)
        {
            return ProducePro.GetMaterialFormTaskdetail(LLID);
        }

        public static DataTable GetMaterialFormDetail(string LLID)
        {
            return ProducePro.GetMaterialFormDetail(LLID);
        }

        public static DataTable IndexAllMaterialForm(string LLID)
        {
            return ProducePro.IndexAllMaterialForm(LLID);
        }

        public static bool SaveUpdateMaterialTask(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, string RWIDDID, string OrderNum)
        {
            return ProducePro.SaveUpdateMaterialTask(record, delist, ref strErr, LLID, RWIDDID, OrderNum);
        }

        public static bool SaveUpdateMaterialForm(tk_MaterialForm record, List<tk_MaterialFDetail> delist, ref string strErr, string LLID, string RWID)
        {
            return ProducePro.SaveUpdateMaterialForm(record, delist, ref strErr, LLID, RWID);
        }
        #endregion

        #region 领料单详情
        public static DataTable GetMaterialFormDetails(string LLID)
        {
            return ProducePro.GetMaterialFormDetails(LLID);
        }
        public static DataTable IndexAllMaterialForms(string LLID)
        {
            return ProducePro.IndexAllMaterialForms(LLID);
        }
        #endregion

        #region 撤销领料单
        public static bool CXLL(string LLID, ref string strErr, string RWID)
        {
            return ProducePro.CXLL(LLID, ref strErr, RWID);
        }
        #endregion

        #region 提交审批
        public static DataTable getTJLL(string LLID, ref string strErr)
        {
            return ProducePro.getTJLL(LLID, ref strErr);
        }
        #endregion

        #region 审批
        public static DataTable getPDLLSP(string LLID, ref string strErr)
        {
            return ProducePro.getPDLLSP(LLID, ref strErr);
        }
        #endregion
        #region 相关单据
        public static DataTable LoadLLs(string LLID, ref string strErr)
        {
            return ProducePro.LoadLLs(LLID, ref strErr);
        }

        public static UIDataTable LoadLLXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadLLXG(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadLLXGDetail(int PageSize, int PageIndex, string LLID)
        {
            return ProducePro.LoadLLXGDetail(PageSize, PageIndex, LLID);
        }
        #endregion

        #region 打印
        public static DataTable PrintLLs(string LLID)
        {
            return ProducePro.PrintLLs(LLID);
        }
        public static DataTable PrintLL(string LLID)
        {
            return ProducePro.PrintLL(LLID);
        }
        #endregion

        #region 上传
        public static bool InsertFile(List<ProduceFile> list)
        {
            return ProducePro.InsertFile(list);
        }
        #endregion

        #region 查看
        public static DataTable GetFiles(string OId)
        {
            return ProducePro.GetFiles(OId);
        }
        #endregion
        #endregion

        #region 随工单
        #region 随工单列表
        public static UIDataTable withthejobList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.withthejobList(a_intPageSize, a_intPageIndex, where);
        }
        public static List<SelectListItem> getname()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.getname();
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
                SelListItem.Value = dtDesc.Rows[i]["OrderContent"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["OrderContent"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> getspc()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.getspc();
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
                SelListItem.Value = dtDesc.Rows[i]["SpecsModels"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["SpecsModels"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable ProduceInDetials(int a_intPageSize, int a_intPageIndex, string SGID, string RWID)
        {
            return ProducePro.ProduceInDetials(a_intPageSize, a_intPageIndex, SGID, RWID);
        }

        public static DataTable ProProductRDatail(string SGID)
        {
            return ProducePro.ProProductRDatail(SGID);
        }
        #endregion

        #region 记录单
        public static tk_ProductRecord getsumnum(string SGID)
        {
            return ProducePro.getsumnum(SGID);
        }

        public static tk_ProductRecord RDetail(string SGID)
        {
            return ProducePro.RDetail(SGID);
        }
        public static DataTable GetProductdetail(string SGID)
        {
            return ProducePro.GetProductdetail(SGID);
        }

        public static string ProductRDatailNum(string SGID)
        {
            return ProducePro.ProductRDatailNum(SGID);
        }

        public static DataTable GetSGJLType()
        {
            return ProducePro.GetSGJLType();
        }

        public static bool SaveProductRDatail(List<tk_ProductRDatail> delist, List<tk_ProductTDatail> ta, ref string strErr, string RWID, string SGID)
        {
            return ProducePro.SaveProductRDatail(delist, ta, ref strErr, RWID, SGID);
        }
        #endregion

        #region 撤销随工单
        public static bool CXSG(string SGID, ref string strErr, string RWID)
        {
            return ProducePro.CXSG(SGID, ref strErr, RWID);
        }
        #endregion

        #region 删除随工单祥表
        public static bool SCSG(string DID, ref string strErr)
        {
            return ProducePro.SCSG(DID, ref strErr);
        }
        #endregion

        #region 修改随工单
        public static tk_ProductRecord IndexAllupdateSG(string SGID)
        {
            return ProducePro.IndexAllupdateSG(SGID);
        }

        public static DataTable GetSGnum(string SGID)
        {
            return ProducePro.GetSGnum(SGID);
        }


        public static DataTable LoadRDatail(string SGID, string RWID)
        {
            return ProducePro.LoadRDatail(SGID, RWID);
        }

        public static DataTable getISSGdetail(string SGID)
        {
            return ProducePro.getISSGdetail(SGID);
        }
        public static DataTable LoadRDatails(string SGID)
        {
            return ProducePro.LoadRDatails(SGID);
        }

        public static bool SaveUpdateRDatail(tk_ProductRecord record, List<tk_ProductRProduct> delist, ref string strErr, ref string strErrs, string SGID, string RWID)
        {
            return ProducePro.SaveUpdateRDatail(record, delist, ref strErr, ref strErrs, SGID, RWID);
        }
        #endregion

        #region 随工单详情
        public static tk_ProductRecord IndexAllSGDtail(string SGID)
        {
            return ProducePro.IndexAllSGDtail(SGID);
        }

        public static DataTable LoadSGDetail(string SGID)
        {
            return ProducePro.LoadSGDetail(SGID);
        }

        public static DataTable LoadSGDetails(string SGID)
        {
            return ProducePro.LoadSGDetails(SGID);
        }
        #endregion

        #region 相关单据
        public static DataTable LoadSGs(string SGID, ref string strErr)
        {
            return ProducePro.LoadSGs(SGID, ref strErr);
        }

        public static UIDataTable LoadSGXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadSGXG(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadSGXGs(int PageSize, int PageIndex, string SGID)
        {
            return ProducePro.LoadSGXGs(PageSize, PageIndex, SGID);
        }

        public static UIDataTable LoadSGXGDetail(int PageSize, int PageIndex, string SGID)
        {
            return ProducePro.LoadSGXGDetail(PageSize, PageIndex, SGID);
        }
        #endregion

        #region 打印
        public static DataTable PrintSGs(string strWhere, string tableName, ref string strErr)
        {
            return ProducePro.PrintSGs(strWhere, tableName, ref strErr);
        }



        public static DataTable PrintSGss()
        {
            return ProducePro.PrintSGss();
        }
        #endregion
        #endregion

        #region 报告上传

        #region 修改检验报告
        public static tk_ReportInfo IndexAllReportInfo(string BGID)
        {
            return ProducePro.IndexAllReportInfo(BGID);
        }

        public static DataTable getFileInfo(string BGID)
        {
            return ProducePro.getFileInfo(BGID);
        }

        public static bool SCBG(string DID, ref string strErr)
        {
            return ProducePro.SCBG(DID, ref strErr);
        }
        public static bool SaveUpdateFileInfoIn(tk_ReportInfo record, List<tk_FileInfo> delist, ref string strErr, string BGID, string CreatePerson, DateTime CreateTime)
        {
            return ProducePro.SaveUpdateFileInfoIn(record, delist, ref strErr, BGID, CreatePerson, CreateTime);
        }
        #endregion

        #region 显示列表
        public static UIDataTable ReportInfo(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.ReportInfo(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable FileInfo(string BGID)
        {
            return ProducePro.FileInfo(BGID);
        }
        #endregion

        #region 撤销报告单
        public static bool CXBG(string BGID, ref string strErr)
        {
            return ProducePro.CXBG(BGID, ref strErr);
        }


        #endregion

        #region 相关单据
        public static DataTable LoadBGs(string BGID, ref string strErr)
        {
            return ProducePro.LoadBGs(BGID, ref strErr);
        }

        public static UIDataTable LoadBGXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadBGXG(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadBGXGDetail(int PageSize, int PageIndex, string BGID)
        {
            return ProducePro.LoadBGXGDetail(PageSize, PageIndex, BGID);
        }

        #endregion

        #region 文件下载
        public static DataTable GetNewDownLoad(string id)
        {
            return ProducePro.GetDownload(id);
        }

        public static DataTable GetNewDownloadFile(string id)
        {
            return ProducePro.GetDownloadFile(id);
        }
        #endregion
        #endregion

        #region 产品入库

        #region 列表显示
        public static List<SelectListItem> getsupplier()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.getsupplier();
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
                SelListItem.Value = dtDesc.Rows[i]["COMNameC"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["COMNameC"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable PStocking(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.PStocking(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable PStockingDetail(int a_intPageSize, int a_intPageIndex, string RKID)
        {
            return ProducePro.PStockingDetail(a_intPageSize, a_intPageIndex, RKID);
        }
        #endregion

        #region 修改入库单
        public static tk_PStocking IndexAllupdateRK(string RKID)
        {
            return ProducePro.IndexAllupdateRK(RKID);
        }

        public static DataTable LoadRposDatail(string RKID, string RWID)
        {
            return ProducePro.LoadRposDatail(RKID, RWID);
        }
        public static bool SaveUpdateposDetail(tk_PStocking record, List<tk_PStockingDetail> delist, ref string strErr, string RKID, string RWID, string RWIDDID)
        {
            return ProducePro.SaveUpdateposDetail(record, delist, ref strErr, RKID, RWID, RWIDDID);
        }

        #endregion

        #region 入库单详情
        public static tk_PStocking IndexAllRKDetail(string RKID)
        {
            return ProducePro.IndexAllRKDetail(RKID);
        }

        public static DataTable LoadRKDatail(string RKID)
        {
            return ProducePro.LoadRKDatail(RKID);
        }
        #endregion

        #region 撤销入库
        public static bool CXRK(string RKID, ref string strErr, string RWID)
        {
            return ProducePro.CXRK(RKID, ref strErr, RWID);
        }
        #endregion

        #region 相关单据
        public static DataTable LoadRKs(string RKID, ref string strErr)
        {
            return ProducePro.LoadRKs(RKID, ref strErr);
        }

        public static UIDataTable LoadRKXG(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return ProducePro.LoadRKXG(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadRKXGDetail(int PageSize, int PageIndex, string RKID)
        {
            return ProducePro.LoadRKXGDetail(PageSize, PageIndex, RKID);
        }
        #endregion

        #region 打印
        public static DataTable PrintRKs(string strWhere, string tableName, ref string strErr)
        {
            return ProducePro.PrintRKs(strWhere, tableName, ref strErr);
        }

        public static DataTable PrintRKdetail(string RKID)
        {
            return ProducePro.PrintRKdetail(RKID);
        }
        #endregion
        #endregion

        #region 计划管理
        #region 制定计划

        public static UIDataTable GetProductPlan(int a_intPageSize, int a_intPageIndex, string JHID)
        {
            return ProducePro.GetProductPlan(a_intPageSize, a_intPageIndex, JHID);
        }
        public static List<SelectListItem> GetSpecifications()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetSpecifications();
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
                SelListItem.Value = dtDesc.Rows[i]["Specifications"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Specifications"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static string GetTopJHID()
        {
            return ProducePro.GetTopJHID();
        }

        public static string ProJHNum(string JHID)
        {
            return ProducePro.ProJHNum(JHID);
        }

        public static DataTable GetZD()
        {
            return ProducePro.GetZD();
        }

        public static bool SaveZD(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr)
        {
            return ProducePro.SaveZD(record, delist, ref strErr);
        }
        #endregion

        #region 修改计划单
        public static DataTable LoadPlanDatail(string JHID)
        {
            return ProducePro.LoadPlanDatail(JHID);
        }

        public static tk_Product_Plan IndexAllupdatePlan(string JHID)
        {
            return ProducePro.IndexAllupdatePlan(JHID);
        }

        public static bool SaveUpdatePlan(tk_Product_Plan record, List<tk_Product_PlanDetail> delist, ref string strErr, string JHID)
        {
            return ProducePro.SaveUpdatePlan(record, delist, ref strErr, JHID);
        }
        #endregion

        #region 撤销计划
        #region 撤销任务单
        public static bool CXJH(string JHID, ref string strErr)
        {
            return ProducePro.CXJH(JHID, ref strErr);
        }
        #endregion
        #endregion

        #region 打印
        public static DataTable PrintJHs(string strWhere, string tableName, ref string strErr)
        {
            return ProducePro.PrintJHs(strWhere, tableName, ref strErr);
        }
        #endregion

        #region 判断是否提交审批
        public static DataTable selectPDTJ(string RWID)
        {
            return ProducePro.selectPDTJ(RWID);
        }
        #endregion

        #region 生成领料单时的是否审批判断
        public static DataTable getSP(string RWID)
        {
            return ProducePro.getSP(RWID);
        }
        #endregion

        #region 生成领料单时的是否领料完成判断
        public static DataTable getLLSL(string RWID)
        {
            return ProducePro.getLLSL(RWID);
        }
        #endregion

        #region 生成领料单时是否生成随工单，入库单判断
        public static DataTable gettll(string RWID)
        {
            return ProducePro.gettll(RWID);
        }
        #endregion

        #region 生成随工单时的是否生成随工单完成判断
        public static DataTable getSGSL(string RWID)
        {
            return ProducePro.getSGSL(RWID);
        }
        #endregion

        #region 生成随工单时领料单是否有此数据判断
        public static DataTable selectLL(string RWID)
        {
            return ProducePro.selectLL(RWID);
        }
        #endregion

        #region  完成入库单时，判断随工单是否生产完成
        public static DataTable selectSG(string RWID)
        {
            return ProducePro.selectSG(RWID);
        }
        #endregion

        #region  产品入库时，判断此产品是否已经完成入库
        public static DataTable selectRK(string RWID)
        {
            return ProducePro.selectRK(RWID);
        }
        #endregion

        #region  生成随工单时，判断是否需添加随工记录
        public static DataTable selectSGSL(string RWID)
        {
            return ProducePro.selectSGSL(RWID);
        }
        #endregion
        #region 生成随工单时判断是否入库
        public static DataTable gettsg(string RWID)
        {
            return ProducePro.gettsg(RWID);
        }
        #endregion
        #endregion

        #region 上传文件类型设置
        public static List<SelectListItem> GetBGLX()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = ProducePro.GetBGLX();
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
                SelListItem.Text = dtDesc.Rows[i]["SID"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static UIDataTable SZBGLX(int a_intPageSize, int a_intPageIndex, string where)
        {
            return ProducePro.SZBGLX(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable THBGLXs(string SID)
        {
            return ProducePro.THBGLXs(SID);
        }
        public static DataTable getText(string SID, string text)
        {
            return ProducePro.getText(SID, text);
        }
        public static bool SCBGLX(string Text, ref string strErr)
        {
            return ProducePro.SCBGLX(Text, ref strErr);
        }
        public static bool SaveBGLX(List<tk_ConfigContent> detailList, ref string strErr)
        {
            return ProducePro.SaveBGLX(detailList, ref strErr);
        }
        #endregion

        #region 退换货检验
        public static UIDataTable getCHECK(int PageSize, int PageIndex, string where)
        {
            return ProducePro.getCHECK(PageSize, PageIndex, where);
        }

        public static UIDataTable getExchangeCheck(int PageSize, int PageIndex, string where)
        {
            return ProducePro.getExchangeCheck(PageSize, PageIndex, where);
        }

        public static UIDataTable getExchangeCheckDetail(int PageSize, int PageIndex, string where)
        {
            return ProducePro.getExchangeCheckDetail(PageSize, PageIndex, where);
        }

        public static DataTable getSPPD(string TID)
        {
            return ProducePro.getSPPD(TID);
        }

        public static DataTable getPDSPCK(string TID)
        {
            return ProducePro.getPDSPCK(TID);
        }

        public static bool UpdateNewApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            if (ProducePro.UpdateApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        #endregion
    }
}
