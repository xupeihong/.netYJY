using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class CustomerServiceMan
    {

        //打印
        public static DataTable PrintList(string strWhere, string tableName, ref string strErr)
        {
            return CustomerServicePro.PrintList(strWhere, tableName, ref strErr);
        }

        //日志
        public static bool AddCustomerServiceLog(tk_CustomerServicelog logobj)
        {
            return CustomerServicePro.AddCustomerServiceLog(logobj);
        }



        //加载保修卡
        public static List<SelectListItem> GetWXGCard()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetWXGCard();
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
                SelListItem.Value = dtDesc.Rows[i]["BXKID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["BXKNum"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //产品名称
        public static List<SelectListItem> GetOrderContent()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetOrderContent();
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
                SelListItem.Value = dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //得到产品规格
        public static List<SelectListItem> GetSpecsModels()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetSpecsModels();
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
                SelListItem.Value = dtDesc.Rows[i]["PID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Spec"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //得到一级库
        public static List<SelectListItem> GetOneWareHouse()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetOneWareHouse();
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
        //得到二级库
        public static List<SelectListItem> GetTwoWareHouse()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetTwoWareHouse();
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
        //导出
        public static DataTable NewGetWarrantyCardToExcel(string strWhere, string tableName, string FieldName, string OrderBy, ref string strErr)
        {
            return CustomerServicePro.NewGetWarrantyCardToExcel(strWhere, tableName, FieldName, OrderBy, ref strErr);
        }
        //根据BZID得到DID
        public static string GetProductReportDID(string BZID)
        {
            return CustomerServicePro.GetProductReportDID(BZID);
        }
        //根据用户id加载部门
        public static List<SelectListItem> GetDepName()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetDepName();
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
                SelListItem.Value = dtDesc.Rows[i]["DeptId"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["DeptName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //加载客户
        public static List<SelectListItem> GetKClientBas()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetKClientBas();
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
                SelListItem.Value = dtDesc.Rows[i]["KID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["DeclareUser"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //加载分公司
        public static List<SelectListItem> GetCompany()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetCompany();
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
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        //根据用户id加载客户
        public static List<SelectListItem> GetKClientBasID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetKClientBasID();
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
        //加载客户dt
        public static DataTable GetKClientBas(string KID)
        {
            return CustomerServicePro.GetKClientBas(KID);
        }
        //根据NAME加载产品规格
        public static DataTable GetProSpec(string KID)
        {
            return CustomerServicePro.GetProSpec(KID);
        }
        //根据选择id加载客户
        public static DataTable GetUserName(string DeptId)
        {
            return CustomerServicePro.GetUserName(DeptId);
        }
        #region [加载订单]
        public static UIDataTable ChangeOrderList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return CustomerServicePro.ChangeOrderList(a_intPageSize, a_intPageIndex, ptype);
        }
        #endregion
        #region [加载产品]
        //加载产品信息
        public static DataTable GetBasicDetail(string PID)
        {
            return CustomerServicePro.GetBasicDetail(PID);
        }
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            return CustomerServicePro.GetPtype(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return CustomerServicePro.ChangePtypeList(a_intPageSize, a_intPageIndex, ptype);
        }
        #endregion
        #region [用户服务]

        #region [用户回访]
        public static string GetTopHFID()
        {
            return CustomerServicePro.GetTopHFID();
        }
        public static string GetTopHFIDDID()
        {
            return CustomerServicePro.GetTopHFIDDID();
        }
        //添加修改
        public static bool SaveUserVisit(tk_SHReturnVisit Project, List<tk_SHRV_Product> list, string type, string HFIDold, ref string strErr)
        {
            return CustomerServicePro.SaveUserVisit(Project, list, type, HFIDold, ref strErr);
        }
        //用户回访列表 
        public static UIDataTable UserVisitList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserVisitList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable UserVisitDetialList(int a_intPageSize, int a_intPageIndex, string HFID)
        {
            return CustomerServicePro.UserVisitDetialList(a_intPageSize, a_intPageIndex, HFID);
        }
        //撤销
        public static bool DeUserVisit(string HFID, ref string strErr)
        {
            return CustomerServicePro.DeUserVisit(HFID, ref strErr);
        }
        public static DataTable UpUserVisitPIDList(string HFID)
        {
            return CustomerServicePro.UpUserVisitPIDList(HFID);
        }

        public static List<string> GetCode()
        {
            List<string> list = new List<string>();
            DataTable dt = CustomerServicePro.GetSupCode();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["RecordID"].ToString());
            }
            return list;
        }

        //顾客满意度调查
        public static DataTable GetCustomerSatisfactionSurveyUserVisit(string HFID)
        {
            return CustomerServicePro.GetCustomerSatisfactionSurveyUserVisit(HFID);
        }
        //判断有无满意度调查
        public static DataTable IfGetDiaoCha(string HFID)
        {
            return CustomerServicePro.IfGetDiaoCha(HFID);
        }

        public static bool DeUserPro(string HFID, string PID)
        {
            return CustomerServicePro.DeUserPro(HFID, PID);
        }
        #endregion

        #region [顾客满意度调查]
        public static string GetTopDCID()
        {
            return CustomerServicePro.GetTopDCID();
        }
        public static string GetTopDCIDDID()
        {
            return CustomerServicePro.GetTopDCIDDID();
        }
        //顾客满意度调查列表 
        public static UIDataTable CustomerSatisfactionSurveyList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.CustomerSatisfactionSurveyList(a_intPageSize, a_intPageIndex, where);
        }
        //加载订单详细消息
        public static DataTable GetCustomerSatisfactionSurvey(string PID)
        {
            return CustomerServicePro.GetCustomerSatisfactionSurvey(PID);
        }
        //添加修改
        public static bool SaveCustomerSatisfactionSurvey(tk_SHSurvey Project, List<tk_SHSurvey_Product> list, string type, ref string strErr)
        {
            return CustomerServicePro.SaveCustomerSatisfactionSurvey(Project, list, type, ref strErr);
        }
        //撤销
        public static bool DeCustomerSatisfactionSurvey(string DCID, ref string strErr)
        {
            return CustomerServicePro.DeCustomerSatisfactionSurvey(DCID, ref strErr);
        }
        public static DataTable UpSurveyList(string DCID)
        {
            return CustomerServicePro.UpSurveyList(DCID);
        }
        public static UIDataTable UserCustomerSatisfactionSurveyList(int a_intPageSize, int a_intPageIndex, string DCID)
        {
            return CustomerServicePro.UserCustomerSatisfactionSurveyList(a_intPageSize, a_intPageIndex, DCID);
        }
        #endregion

        #region [用户投诉]
        //用户投诉列表 
        public static UIDataTable UserComplaintsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserComplaintsList(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetTopTSID()
        {
            return CustomerServicePro.GetTopTSID();
        }
        public static string GetTopTSIDDID()
        {
            return CustomerServicePro.GetTopTSIDDID();
        }
        public static string GetTopCLIDDID()
        {
            return CustomerServicePro.GetTopCLIDDID();
        }
        public static string GetTopProductDID()
        {
            return CustomerServicePro.GetTopProductDID();
        }
        //添加修改
        public static bool SaveUserComplaints(tk_SHComplain Project, tk_SHComplain_User allruser, tk_SHComplain_Process allpro, List<tk_SHComplain_Product> list, string type, ref string strErr)
        {
            return CustomerServicePro.SaveUserComplaints(Project, allruser, allpro, list, type, ref strErr);
        }
        public static UIDataTable UserComplaintsDetialList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserComplaintsDetialList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable UserProComDetialList(int a_intPageSize, int a_intPageIndex, string TSID)
        {
            return CustomerServicePro.UserProComDetialList(a_intPageSize, a_intPageIndex, TSID);
        }
        //撤销
        public static bool DeUserComplaints(string TSID, ref string strErr)
        {
            return CustomerServicePro.DeUserComplaints(TSID, ref strErr);
        }

        //添加
        public static bool SaveProcessingRecord(tk_SHComplain_Process allpro, ref string strErr)
        {
            return CustomerServicePro.SaveProcessingRecord(allpro, ref strErr);
        }
        public static DataTable UpAddProcessingRecord(string TSID)
        {
            return CustomerServicePro.UpAddProcessingRecord(TSID);
        }
        public static DataTable UpModifyUserComplaintsList(string TSID)
        {
            return CustomerServicePro.UpModifyUserComplaintsList(TSID);
        }
        public static DataTable UpModifyUserComplaintsListPro(string TSID)
        {
            return CustomerServicePro.UpModifyUserComplaintsListPro(TSID);
        }
        //根据客户加载报修
        public static DataTable GetBX(string Customer)
        {
            return CustomerServicePro.GetBX(Customer);
        }


        //根据客户加载报装
        public static DataTable GetBZ(string Customer)
        {
            return CustomerServicePro.GetBZ(Customer);
        }

        public static bool DeUserCom(string TSID, string PID)
        {
            return CustomerServicePro.DeUserCom(TSID, PID);
        }
        #endregion

        #region [电话记录]
        public static string GetTopDHID()
        {
            return CustomerServicePro.GetTopDHID();
        }
        //加载电话记录
        public static UIDataTable TelephoneAnsweringList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.TelephoneAnsweringList(a_intPageSize, a_intPageIndex, where);
        }
        //保存电话记录
        public static bool SaveTelephoneAnswering(tk_TelRecord record, string type, ref string strErr)
        {
            return CustomerServicePro.SaveTelephoneAnswering(record, type, ref strErr);
        }
        //导出
        public static DataTable TelephoneAnsweringToExcelFZ(string strWhere, ref string strErr)
        {
            return CustomerServicePro.TelephoneAnsweringToExcelFZ(strWhere, ref strErr);
        }
        //加载修改数据
        public static DataTable UpTelephoneAnswering(string DHID)
        {
            return CustomerServicePro.UpTelephoneAnswering(DHID);
        }
        //撤销
        public static bool DeTelephoneAnswering(string DHID, ref string strErr)
        {
            return CustomerServicePro.DeTelephoneAnswering(DHID, ref strErr);
        }
        #endregion
        #endregion
        #region [产品安装]

        #region [产品报装]
        public static string GetTopBZID()
        {
            return CustomerServicePro.GetTopBZID();
        }
        public static string GetTopDID()
        {
            return CustomerServicePro.GetTopDID();
        }
        public static bool AddProductReport(tk_SHInstallR Project, List<tk_SHInstallR_Product> list, string type, ref string strErr)
        {
            return CustomerServicePro.AddProductReport(Project, list, type, ref strErr);
        }

        public static UIDataTable ProductReportList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.ProductReportList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeProductReport(string BZID, ref string strErr)
        {
            return CustomerServicePro.DeProductReport(BZID, ref strErr);
        }
        public static DataTable UPProductReportList(string BZID)
        {
            return CustomerServicePro.UPProductReportList(BZID);
        }
        public static DataTable UPProductReportListpro(string BZID)
        {
            return CustomerServicePro.UPProductReportListpro(BZID);
        }
        public static string GetTopAZID()
        {
            return CustomerServicePro.GetTopAZID();
        }
        public static string GetTopAZIDDID()
        {
            return CustomerServicePro.GetTopAZIDDID();
        }
        //产品报装详细
        public static UIDataTable ProductReportDetialList(int a_intPageSize, int a_intPageIndex, string BZID)
        {
            return CustomerServicePro.ProductReportDetialList(a_intPageSize, a_intPageIndex, BZID);
        }
        //添加产品安装记录
        public static bool AddSHInstall(tk_SHInstall Project, List<tk_SHInstall_Product> list, ref string strErr)
        {
            return CustomerServicePro.AddSHInstall(Project, list, ref strErr);
        }

        //根据产品加载保修卡
        public static DataTable GetPIDBasicDetail(string PID)
        {
            return CustomerServicePro.GetPIDBasicDetail(PID);
        }

        public static bool DeProduct(string BZID, string PID)
        {
            return CustomerServicePro.DeProduct(BZID, PID);
        }
        #endregion
        #region [产品安装]
        public static UIDataTable ProductInstallationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.ProductInstallationList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeProductInstallation(string AZID, ref string strErr)
        {
            return CustomerServicePro.DeProductInstallation(AZID, ref strErr);
        }
        //修改
        public static DataTable UpProductInstallation(string AZIDold)
        {
            return CustomerServicePro.UpProductInstallation(AZIDold);
        }
        //修改
        public static DataTable UpProductInstallationpro(string AZIDold)
        {
            return CustomerServicePro.UpProductInstallationpro(AZIDold);
        }
        //修改产品安装记录
        public static bool SaveProductInstallation(tk_SHInstall Project, List<tk_SHInstall_Product> list, string AZIDold, ref string strErr)
        {
            return CustomerServicePro.SaveProductInstallation(Project, list, AZIDold, ref strErr);
        }
        //产品安装详细
        public static UIDataTable ProductInstallationDetialList(int a_intPageSize, int a_intPageIndex, string AZID)
        {
            return CustomerServicePro.ProductInstallationDetialList(a_intPageSize, a_intPageIndex, AZID);
        }

        public static bool DeUserInst(string AZID, string PID)
        {
            return CustomerServicePro.DeUserInst(AZID, PID);
        }
        #endregion

        #region [家用产品售后]
        public static bool AddCSAlarm(CSAlarm csal)
        {
            return CustomerServicePro.AddCSAlarm(csal);
        }
        //添加家用产品售后安装记录
        public static bool SaveHomeSalesInstallation(tk_SHInstall Project, List<tk_SHInstall_Product> list, ref string strErr)
        {
            return CustomerServicePro.SaveHomeSalesInstallation(Project, list, ref strErr);
        }
        public static UIDataTable ChangeHomeSalesInstallationList(int a_intPageSize, int a_intPageIndex)
        {
            return CustomerServicePro.ChangeHomeSalesInstallationList(a_intPageSize, a_intPageIndex);
        }
        public static DataTable GetHomeSalesInstallation(string DID)
        {
            return CustomerServicePro.GetHomeSalesInstallation(DID);
        }
        public static UIDataTable HomeSalesInstallationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.HomeSalesInstallationList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool ButDE(string OrderID, ref string strErr)
        {
            return CustomerServicePro.ButDE(OrderID, ref strErr);
        }
        //安排安装
        public static bool ButAPAZ(string OrderID, ref string strErr)
        {
            return CustomerServicePro.ButAPAZ(OrderID, ref strErr);
        }
        //回款
        public static bool ButHK(string OrderID, ref string strErr)
        {
            return CustomerServicePro.ButHK(OrderID, ref strErr);
        }
        //完成
        public static bool ButWCAZ(string OrderID, ref string strErr)
        {
            return CustomerServicePro.ButWCAZ(OrderID, ref strErr);
        }
        //提示报警
        public static DataTable GetOrderidNew()
        {
            return CustomerServicePro.GetOrderidNew();
        }
        //修改
        public static DataTable UpHomeSalesInstallation(string AZIDold)
        {
            return CustomerServicePro.UpHomeSalesInstallation(AZIDold);
        }

        //修改产品安装记录
        public static bool SaveUpHomeSalesInstallation(tk_SHInstall Project, List<tk_SHInstall_Product> list, string AZIDold, ref string strErr)
        {
            return CustomerServicePro.SaveUpHomeSalesInstallation(Project, list, AZIDold, ref strErr);
        }
        #endregion
        #endregion
        #region [审批]
        public static UIDataTable UserAppProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserAppProcessing(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable GetBasCus(string TSID)
        {
            return CustomerServicePro.GetBasCus(TSID);
        }

        public static UIDataTable UserContractProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserContractProcessing(a_intPageSize, a_intPageIndex, where);
        }
        #endregion
        #region [维修任务]

        #region [维修任务]
        //用户投诉列表 
        public static UIDataTable MaintenanceTaskList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.MaintenanceTaskList(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetTopBXID()
        {
            return CustomerServicePro.GetTopBXID();
        }

        //添加修改
        public static bool SaveMaintenanceTask(tk_WXRequisit Project, string type, ref string strErr)
        {
            return CustomerServicePro.SaveMaintenanceTask(Project, type, ref strErr);
        }
        public static List<SelectListItem> GetSpec()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetSpec();
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
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static UIDataTable UserMaintenanceTaskList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserMaintenanceTaskList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeMaintenanceTask(string BXID, ref string strErr)
        {
            return CustomerServicePro.DeMaintenanceTask(BXID, ref strErr);
        }

        public static bool WXMaintenanceTask(string BXID, ref string strErr)
        {
            return CustomerServicePro.WXMaintenanceTask(BXID, ref strErr);
        }
        //完成维修
        public static bool CompleteMaintenanceTask(string BXID, ref string strErr)
        {
            return CustomerServicePro.CompleteMaintenanceTask(BXID, ref strErr);
        }
        public static string GetTopWXID()
        {
            return CustomerServicePro.GetTopWXID();
        }
        public static DataTable UpWXRecordList(string BXID)
        {
            return CustomerServicePro.UpWXRecordList(BXID);
        }
        public static bool SaveUpMainten(tk_WXRecord Project, List<tk_WXRecord_Product> list, ref string strErr)
        {
            return CustomerServicePro.SaveUpMainten(Project, list, ref strErr);
        }
        public static string GetTopWXIDDID()
        {
            return CustomerServicePro.GetTopWXIDDID();
        }
        //修改
        public static DataTable UpDateModifyTaskComplaintsa(string BXID)
        {
            return CustomerServicePro.UpDateModifyTaskComplaintsa(BXID);
        }
        public static UIDataTable UserMaintenanceTaskTwoList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserMaintenanceTaskTwoList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable UserMaintenanceTaskThreeList(int a_intPageSize, int a_intPageIndex, string where, string BXID)
        {
            return CustomerServicePro.UserMaintenanceTaskThreeList(a_intPageSize, a_intPageIndex, where, BXID);
        }
        //根据产品id加载产品信息
        public static DataTable GetPronewSpec(string PID)
        {
            return CustomerServicePro.GetPronewSpec(PID);
        }
        //根据用户加载报装编号
        public static DataTable GetUserBAO(string Tel)
        {
            return CustomerServicePro.GetUserBAO(Tel);
        }
        #endregion

        #region [保修卡]

        public static UIDataTable WarrantyCardList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.WarrantyCardList(a_intPageSize, a_intPageIndex, where);
        }
        public static string GetTopBXKID()
        {
            return CustomerServicePro.GetTopBXKID();
        }
        public static bool SaveWarrantyCard(tk_WXGuaranteeCard record, ref string strErr)
        {
            return CustomerServicePro.SaveWarrantyCard(record, ref strErr);
        }
        //撤销
        public static bool DeWarrantyCard(string BXKID, ref string strErr)
        {
            return CustomerServicePro.DeWarrantyCard(BXKID, ref strErr);
        }
        public static bool UPdateWarrantyCard(tk_WXGuaranteeCard card, string BXKIDold, ref string strErr)
        {
            return CustomerServicePro.UPdateWarrantyCard(card, BXKIDold, ref strErr);
        }
        public static DataTable UPdateWarrantyCardList(string BXKID)
        {
            return CustomerServicePro.UPdateWarrantyCardList(BXKID);
        }
        //导出
        public static DataTable GetWarrantyCardToExcel(string strWhere, ref string strErr)
        {
            return CustomerServicePro.GetWarrantyCardToExcel(strWhere, ref strErr);
        }
        // 保存数据 
        //维修记录编号	保修卡所属单位	合同编号	保修卡编号	产品名称	产品编号	产品规格型号	购买日期  （2015-10-19 00:00:00.000）	
        //保修时间	最终用户单位	联系人	联系方式	备注	创建时间   （2015-10-19 00:00:00.000）	
        //登记人	初始状态0	客户地址  
        public static bool SaveWarrantyCardData(string strData, ref string strErr)
        {
            return CustomerServicePro.SaveWarrantyCardData(strData, ref strErr);
        }
        #endregion
        #region [调压巡检]
        public static bool SavePressureAdjustingInspection(tk_PressureAdjustingInspection record, List<tk_PressureAdjustingInspectionDetail> detailList, string type, ref string strErr)
        {
            return CustomerServicePro.SavePressureAdjustingInspection(record, detailList, type, ref strErr);
        }
        public static UIDataTable PressureAdjustingInspectionList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.PressureAdjustingInspectionList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DePressureAdjustingInspection(string TYID, ref string strErr)
        {
            return CustomerServicePro.DePressureAdjustingInspection(TYID, ref strErr);
        }
        public static string GetTopTYID()
        {
            return CustomerServicePro.GetTopTYID();
        }
        public static DataTable UpPressureAdjustingInspection(string TYID)
        {
            return CustomerServicePro.UpPressureAdjustingInspection(TYID);
        }
        public static string GetTopTXID()
        {
            return CustomerServicePro.GetTopTXID();
        }

        public static UIDataTable PressureAdjustingInspectionDetailList(int a_intPageSize, int a_intPageIndex, string TYID)
        {
            return CustomerServicePro.PressureAdjustingInspectionDetailList(a_intPageSize, a_intPageIndex, TYID);
        }
        public static DataTable UpPrintPressureAdjustingInspection(string TYID)
        {
            return CustomerServicePro.UpPrintPressureAdjustingInspection(TYID);
        }
        public static DataTable UpTime(string TYID)
        {
            return CustomerServicePro.UpTime(TYID);
        }
        #endregion
        #region [设备调试任务单]
        public static string GetTopTRID()
        {
            return CustomerServicePro.GetTopTRID();
        }
        public static bool SaveEquipmentCommissioning(tk_EquipmentDebugging record, List<tk_DebuggingSituation> delist, string type, ref string strErr)
        {
            return CustomerServicePro.SaveEquipmentCommissioning(record, delist, type, ref strErr);
        }
        public static string GetTopQKID()
        {
            return CustomerServicePro.GetTopQKID();
        }
        public static UIDataTable EquipmentCommissioningList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.EquipmentCommissioningList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeEquipmentCommissioning(string TRID, ref string strErr)
        {
            return CustomerServicePro.DeEquipmentCommissioning(TRID, ref strErr);
        }
        public static DataTable UpEquipmentCommissioning(string TRID)
        {
            return CustomerServicePro.UpEquipmentCommissioning(TRID);
        }
        //根据编号加载主表信息
        public static DataTable GetEquipmentDebugging(string TRID)
        {
            return CustomerServicePro.UpEquipmentCommissioning(TRID);
        }
        //根据编号加载副表信息
        public static DataTable UpDebuggingSituation(string TRID)
        {
            return CustomerServicePro.UpDebuggingSituation(TRID);
        }
        public static UIDataTable EquipmentCommissioningDetialList(int a_intPageSize, int a_intPageIndex, string TRID)
        {
            return CustomerServicePro.EquipmentCommissioningDetialList(a_intPageSize, a_intPageIndex, TRID);
        }

        //根据产品加载报修
        public static DataTable GetPIDBaoDetail(string PID)
        {
            return CustomerServicePro.GetPIDBaoDetail(PID);
        }

        public static UIDataTable EqProductReportList(int a_intPageSize, int a_intPageIndex, string RelationID)
        {
            return CustomerServicePro.EqProductReportList(a_intPageSize, a_intPageIndex, RelationID);
        }


        //判断是否报装
        public static DataTable PanDuanIfPro(string TRID)
        {
            return CustomerServicePro.PanDuanIfPro(TRID);
        }
        #endregion
        #endregion
        #region [销售记录]
        #region [收款记录]
        public static string GetTopCRID()
        {
            return CustomerServicePro.GetTopCRID();
        }
        public static bool SaveCollectionRecord(tk_CollectionRecord record, string type, ref string strErr)
        {
            return CustomerServicePro.SaveCollectionRecord(record, type, ref strErr);
        }
        public static UIDataTable CollectionRecordList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.CollectionRecordList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeCollectionRecord(string CRID, ref string strErr)
        {
            return CustomerServicePro.DeCollectionRecord(CRID, ref strErr);
        }

        public static bool GetState(string CRID, ref string strErr)
        {
            return CustomerServicePro.GetState(CRID, ref strErr);
        }
        public static DataTable UpCollectionRecord(string CRID)
        {
            return CustomerServicePro.UpCollectionRecord(CRID);
        }
        public static DataTable UpUpCollectionRecordList(string CRID)
        {
            return CustomerServicePro.UpUpCollectionRecordList(CRID);
        }

        public static DataTable GetBasCusCollectionRecord(string CRID)
        {
            return CustomerServicePro.GetBasCusCollectionRecord(CRID);
        }

        public static DataTable GetSKSP(string CRID)
        {
            return CustomerServicePro.GetSKSP(CRID);
        }
        public static UIDataTable UserBillingRecordsProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserBillingRecordsProcessing(a_intPageSize, a_intPageIndex, where);
        }
        #endregion
        #region [订货单]
        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return CustomerServicePro.GetOrderInfo(a_intPageSize, a_intPageIndex, strWhere);
        }
        //获取订单ID
        public static string GetNewOrderID()
        {
            return CustomerServicePro.GetNewOrderID();
        }
        public static string GetMaxContractID()
        {
            return CustomerServicePro.GetMaxContractID();
        }
        public static bool SaveOrderInfo(OrdersInfoNew orderinfo, List<Orders_DetailInfoNew> list, ref string strErr)
        {
            return CustomerServicePro.SaveOrderInfo(orderinfo, list, ref strErr);
        }
        public static UIDataTable LoadOrderDetailnew(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return CustomerServicePro.LoadOrderDetailnew(a_intPageSize, a_intPageIndex, OrderID);
        }
        public static bool CanCelOrdersInfonew(string OrderID, ref string strErr)
        {
            return CustomerServicePro.CanCelOrdersInfo(OrderID, ref strErr);
        }
        public static OrdersInfo GetOrdersByOrderIDnew(string OrderID)
        {
            return CustomerServicePro.GetOrdersByOrderIDnew(OrderID);
        }
        public static DataTable GetOrdersDetailnew(string OrderID)
        {
            return CustomerServicePro.GetOrdersDetailnew(OrderID);
        }
        public static bool SaveUpdateOrderInfonew(OrdersInfoNew orderinfo, List<Orders_DetailInfoNew> list, ref string strErr)
        {
            return CustomerServicePro.SaveUpdateOrderInfonew(orderinfo, list, ref strErr);
        }
        public static UIDataTable GetOrderInfonew(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return CustomerServicePro.GetOrderInfonew(a_intPageSize, a_intPageIndex, strWhere);
        }
        public static DataTable GetOrderInfoToExcelnew(string where, ref string strErr)
        {
            return CustomerServicePro.GetOrderInfoToExcelnew(where, ref strErr);
        }
        #region [获取登录人的首字母]
        public static string GetNamePYnew(string LoginName)
        {
            return CustomerServicePro.GetNamePYnew(LoginName);
        }
        #endregion
        #region [供应商]
        public static UIDataTable GetCheckSupListOld(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return CustomerServicePro.GetCheckSupListOld(a_intPageSize, a_intPageIndex, ptype);
        }
        public static DataTable GetSupplierCot(string SID)
        {
            return CustomerServicePro.GetSupplierCot(SID);
        }
        public static UIDataTable GetSupTypeOld(int a_intPageSize, int a_intPageIndex)
        {
            return CustomerServicePro.GetSupTypeOld(a_intPageSize, a_intPageIndex);
        }
        #endregion
        public static List<SelectListItem> GetUM_USERNEW(string DeptID)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = CustomerServicePro.GetUM_USERNEW(DeptID);

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            //SelListItem.Value = "";
            //SelListItem.Text = "";
            //ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["UserName"].ToString();//UserId
                SelListItem.Text = dt.Rows[i]["UserName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;

        }
        #endregion
        #region [开票记录]
        public static string GetTopBRDID()
        {
            return CustomerServicePro.GetTopBRDID();
        }
        public static bool SaveBillingRecords(tk_BillingRecords record, string type, ref string strErr)
        {
            return CustomerServicePro.SaveBillingRecords(record, type, ref strErr);
        }
        public static UIDataTable BillingRecordsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.BillingRecordsList(a_intPageSize, a_intPageIndex, where);
        }
        //撤销
        public static bool DeBillingRecords(string BRDID, ref string strErr)
        {
            return CustomerServicePro.DeBillingRecords(BRDID, ref strErr);
        }
        public static DataTable UpBillingRecords(string BRDID)
        {
            return CustomerServicePro.UpBillingRecords(BRDID);
        }
        public static DataTable UpUpUpBillingRecords(string BRDID)
        {
            return CustomerServicePro.UpUpUpBillingRecords(BRDID);
        }
        public static DataTable GetKPJL(string BRDID)
        {
            return CustomerServicePro.GetKPJL(BRDID);
        }

        public static DataTable GetPDSP(string BRDID)
        {
            return CustomerServicePro.GetPDSP(BRDID);
        }
        public static DataTable GetBasBillingRecords(string BRDID)
        {
            return CustomerServicePro.GetBasBillingRecords(BRDID);
        }

        public static UIDataTable UserKPProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.UserKPProcessing(a_intPageSize, a_intPageIndex, where);
        }
        #endregion
        #endregion
        #region [统计]

        #region [报装统计表]
        public static DataTable InstallStatisticsList(string where)
        {
            return CustomerServicePro.InstallStatisticsList(where);
        }
        #endregion

        #region [客户满意度统计]
        public static DataTable SatisfactionStatisticsList(string where)
        {
            return CustomerServicePro.SatisfactionStatisticsList(where);
        }
        #endregion

        #region [维修任务统计]
        public static DataTable MaintenanceTaskStatisticsList(string where)
        {
            return CustomerServicePro.MaintenanceTaskStatisticsList(where);
        }
        #endregion
        #region [设备调试统计表]
        public static DataTable TestingOfEquipmentStatisticsList(string where)
        {
            return CustomerServicePro.TestingOfEquipmentStatisticsList(where);
        }
        //计算总数
        public static DataTable GetTestingOfEquipmentStatistics(string where)
        {
            return CustomerServicePro.GetTestingOfEquipmentStatistics(where);
        }
        #endregion
        #endregion
        #region [客户服务]
        #region [客户服务]
        public static string GetTopKHID()
        {
            return CustomerServicePro.GetTopKHID();
        }

        public static bool SaveCusService(tk_CustomerInformation record, string type, ref string strErr)
        {
            return CustomerServicePro.SaveCusService(record, type, ref strErr);
        }

        public static UIDataTable CusServiceList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.CusServiceList(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable UpSaveCusService(string KHID)
        {
            return CustomerServicePro.UpSaveCusService(KHID);
        }
        #endregion
        #region [合同]
        public static string GetNewShowCIDnew()
        {
            return CustomerServicePro.GetNewShowCIDnew();
        }
        public static bool InsertNewContractBasnew(ContractBas Bas, ref string a_strErr)
        {
            if (CustomerServicePro.InsertNewContractBasnew(Bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static UIDataTable getNewContractGridnew(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.getNewContractGridnew(a_intPageSize, a_intPageIndex, where);
        }
        public static List<SelectListItem> GetNewConfigContentNew(string Type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetConfigContNew(Type);
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
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //巡检次数
        public static DataTable ViewInspection(string CId)
        {
            return CustomerServicePro.ViewInspection(CId);
        }


        //提交合同
        public static bool TJContract(string PID, string RelevanceID, string webkey, string createUser, ref string strErr)
        {
            return CustomerServicePro.TJContract( PID,  RelevanceID,  webkey,  createUser,ref  strErr);
        }

        //提交合同IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr
        public static bool UpdateTJContract(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string strErr)
        {
            return CustomerServicePro.UpdateTJContract(IsPass, Opinion, Remark, PID, webkey, folderBack,RelevanceID, ref  strErr);
        }

        //10W以下及10W审批
        public static bool UpdateTJContractSW(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string strErr)
        {
            return CustomerServicePro.UpdateTJContractSW(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref  strErr);
        }


        public static UIDataTable ConditionGridNew(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            return CustomerServicePro.ConditionGridNew(a_intPageSize, a_intPageIndex, where, folderBack);
        }
        #endregion
        #endregion

        #region [系统设置]
        public static UIDataTable getBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            //CustomerServicePro
            return CustomerServicePro.getBasicGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static List<SelectListItem> GetBasicContent()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = CustomerServicePro.GetBasicContent();
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
        #endregion

        #region [公共信息]
        public static UIDataTable ConfigurationInformationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return CustomerServicePro.ConfigurationInformationList(a_intPageSize, a_intPageIndex, where);
        }
        //添加
        public static bool InsertNewContentnew(string type, string text, ref string a_strErr)
        {
            if (CustomerServicePro.InsertNewContentnew(type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        //删除
        public static bool DeleteNewContentnew(string xid, string type, ref string a_strErr)
        {
            if (CustomerServicePro.DeleteContentnew(xid, type, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        //修改
        public static bool UpdateNewContentnew(string xid, string type, string text, ref string a_strErr)
        {
            if (CustomerServicePro.UpdateContentnew(xid, type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        #endregion

        #region [待审批提示]
        public static DataTable ConSP()
        {
            return CustomerServicePro.ConSP();
        }

        public static string GetSPid(string folderBack)
        {
            return CustomerServicePro.GetSPid(folderBack);
        }
        #endregion
    }
}
