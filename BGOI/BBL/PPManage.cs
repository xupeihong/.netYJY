using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
namespace TECOCITY_BGOI
{
    public class PPManage
    {

        #region[公共方法]



        public static UIDataTable SelectRZ(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectRZ(a_intPageSize, a_intPageIndex, where);
        }

        public static string getCPlanTime()
        {
            return PpPro.getCPlanTime();
        }

        public static DataTable GetDataTime(string table, string lie, string type)
        {
            return PpPro.GetDataTime(table, lie, type);
        }

        public static DataTable GetSuppliers()
        {
            return PpPro.GetSuppliers();
        }

        public static int AddRZ(string id, string LogTitle, string LogConTent, string LogTime, string LogPerson, string Type)
        {
            return PpPro.AddRZ(id, LogTitle, LogConTent, LogTime, LogPerson, Type);
        }
        #endregion

        public static UIDataTable GetSalesRetailList(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return PpPro.GetSalesRetailList(a_intPageSize, a_intPageIndex, strWhere);
        }


        public static UIDataTable SelectPurchaseGoodsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectPurchaseGoodsList(a_intPageSize, a_intPageIndex, where);
        }


        public static DataTable SelectQGid(string CID)
        {
            return PpPro.SelectQGid(CID);
        }

        public static List<SelectListItem> GetTypeID(string where)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = PpPro.GetTypeID(where);
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
                SelListItem.Value = dtDesc.Rows[i]["id"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetNewConfigCont(string where)
        {
            return PpPro.GetTypeID(where);
        }

        public static DataTable GetNewConfigContKF()
        {
            return InventoryPro.GetHouseID();
        }

        public static List<SelectListItem> GetUserId()
        {
            //
            List<SelectListItem> ListItem = new List<SelectListItem>();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string rolenames = GAccount.GetAccountInfo().RoleNames;
            DataTable dtDesc = PpPro.GetUserId(" and Deptid='" + unitid + "' and RoleNames='" + rolenames + "'");
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


        public static DataTable GetBasicDetail(string PID)
        {
            return PpPro.GetBasicDetail(PID);
        }


        #region[选择任务单号]
        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex)
        {

            return PpPro.GetOrderInfo(a_intPageSize, a_intPageIndex);
        }

        public static UIDataTable GetOrderInfoGoods(string where, int a_intPageSize, int a_intPageIndex)
        {

            return PpPro.GetOrderInfoGoods(where, a_intPageSize, a_intPageIndex);
        }
        #endregion

        #region [订购]

        public static List<SelectListItem> GetDDyewu()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            SelectListItem SelListItem = new SelectListItem();



            return ListItem;
        }
        public static string GetTopListDDID()
        {
            return PpPro.GetTopListDDID();
        }

        public static bool LJSplitsInsert(List<PP_OrderGoods> order)
        {
            return PpPro.LJSplitsInsert(order);
        }
        public static DataTable GetSupplierID(string where)
        {

            return PpPro.GetSupplierID(where);
        }

        public static bool InsertPurchseOrder(PP_PurchaseOrder order)
        {
            return PpPro.InsertPurchaseOrder(order);
        }

        public static bool InsertOrder(PP_PurchaseOrder record, List<PP_OrderGoods> delist, List<PP_ChoseGoods> cplist, ref string strErr)
        {
            return PpPro.InsertOrder(record, delist, cplist, ref strErr);
        }


        public static bool SavePlanData(string strData, PP_PurchaseOrder good, ref string strErr)
        {
            return PpPro.SavePlanData(strData, good, ref strErr);
        }
        public static DataTable SelectDDXQ(string where)
        {
            return PpPro.SelectDDXQ(where);
        }
        public static UIDataTable SelectDD(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectDD(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable SelectDDTJ(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectDDTJ(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable SelectGoodsDDID(string where)
        {
            return PpPro.SelectGoodsDDID(where);
        }
        public static DataTable selectjob(string where)
        {
            return PpPro.selectjob(where);
        }
        public static DataTable SelectGoodsDC(string where)
        {
            return PpPro.SelectGoodsDC(where);
        }

        public static DataTable SelectGoodsDCs(string where)
        {
            return PpPro.SelectGoodsDCs(where);
        }
        public static DataTable SelectGoodsDDID1(string where)
        {
            return PpPro.SelectGoodsDDID1(where);
        }

        public static DataTable SelectCountDD()
        {
            return PpPro.SelectCountDD();
        }
        public static UIDataTable SelectDDGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectDDGoods(a_intPageSize, a_intPageIndex, where);

        }
        public static DataTable SelectDDCID(string where)
        {
            return PpPro.SelectDDCID(where);
        }

        public static bool UpdateDD(PP_PurchaseOrder record, List<PP_OrderGoods> delist, List<PP_ChoseGoods> cplist, ref string strErr)
        {
            return PpPro.UpdateDD(record, delist, cplist, ref strErr);
        }

        public static int UpdateDDValidate(string where)
        {
            return PpPro.UpdateDDValidate(where);
        }

        public static int UpdateSHState(string where)
        {
            return PpPro.UpdateSHState(where);
        }

        public static DataTable SelectKC()
        {
            return PpPro.SelectKC();
        }

        public static List<SelectListItem> SelectKCs()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = PpPro.SelectKC();
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

                SelListItem.Text = dtDesc.Rows[i]["ProName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable Selectkoujing(string where)
        {
            return PpPro.Selectkoujing(where);
        }

        public static DataTable SelectLingJ(string where)
        {
            return PpPro.SelectLingJ(where);
        }
        public static DataTable SelectLingJXQ(string where)
        {
            return PpPro.SelectLingJXQ(where);
        }

        public static DataTable SelectCP(string where)
        {
            return PpPro.SelectCP(where);
        }
        public static UIDataTable SelectCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectCPXX(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable SelectDDSupplier(string where)
        {
            return PpPro.SelectDDSupplier(where);
        }

        public static DataTable SelectDDCP(string where)
        {
            return PpPro.SelectDDCP(where);
        }


        public static DataTable SelectSplitLJ(string where)
        {
            return PpPro.SelectSplitLJ(where);
        }

        public static DataTable SelectSplitLJxq(string name, string cpid)
        {
            return PpPro.SelectSplitLJxq(name, cpid);
        }
        #endregion

        #region [询价]
        public static bool insertinquiry(PP_Inquirys inq)
        {
            return PpPro.insertinquiry(inq);
        }

        public static UIDataTable SelectInquiry(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectInquiry(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable SelectGoodsXJID(string where)
        {
            return PpPro.SelectGoodsXJID(where);
        }
        public static bool InsertXJ(List<PP_InquiryGoods> record, PP_InquiryCondition con, PP_Inquirys inq, ref string strErr)
        {
            return PpPro.InsertXJ(record, con, inq, ref strErr);
        }
        public static string GetTopListXJID()
        {
            return PpPro.GetTopListXJID();
        }

        public static DataTable SelectXJXQ(string where)
        {
            return PpPro.SelectXJXQ(where);
        }


        public static int UpdateXJValidate(string where)
        {
            return PpPro.UpdateXJValidate(where);
        }
        public static UIDataTable SelectXJ(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectXJ(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectXJCID(string where)
        {
            return PpPro.SelectXJCID(where);
        }

        public static bool UpdateXJ(PP_Inquirys inq, PP_InquiryCondition tion, List<PP_InquiryGoods> list, ref string strErr)
        {
            return PpPro.UpdateXJ(inq, tion, list, ref strErr);
        }
        #endregion

        #region [请购单]


        public static DataTable SelectGoodsQGID(string where)
        {
            return PpPro.SelectGoodsQGID(where);
        }
        public static UIDataTable SelectQG(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectQG(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable PurchaseGoodsList(int a_intPageSize, int a_intPageIndex, string CID)
        {
            return PpPro.PurchaseGoodsList(a_intPageSize, a_intPageIndex, CID);
        }
        public static string GetPurchaseRequisitionQGID()
        {
            return PpPro.GetPurchaseRequisitionQGID();
        }
        public static DataTable SelectQGXQ(string where)
        {
            return PpPro.SelectQGXQ(where);
        }
        public static UIDataTable GetByOrderID(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.GetByOrderID(a_intPageSize, a_intPageIndex, where);
        }

        public static bool InsertQG(List<PP_PurchaseGoods> record, PP_PurchaseRequisition delist, ref string strErr)
        {
            return PpPro.InsertQG(record, delist, ref strErr);
        }

        public static bool UpdateQG(PP_PurchaseRequisition pp, List<PP_PurchaseGoods> list, ref string strErr)
        {
            return PpPro.UpdateQG(pp, list, ref strErr);
        }


        public static int UpdateQGValidate(string where)
        {
            return PpPro.UpdateQGValidate(where);
        }
        #endregion

        #region[收货单]
        public static DataTable GetList(string where)
        {
            return PpPro.GetList(where);
        }

        public static string GetTopListSHID()
        {
            return PpPro.GetTopListSHID();
        }

        public static UIDataTable SelectSH(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectSH(a_intPageSize, a_intPageIndex, where);
        }


        public static UIDataTable SelectSHCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectSHCPXX(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable SelectSHGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectSHGoods(a_intPageSize, a_intPageIndex, where);
        }
        public static bool InsertSH(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<string> str)
        {
            return PpPro.InsertSH(record, DID, delist, ref strErr, str);
        }

        public static bool InsertCPSHXX(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            return PpPro.InsertCPSHXX(record, DID, delist, ref strErr, str);
        }
        public static DataTable SelectSHDDID(string where)
        {
            return PpPro.SelectSHDDID(where);
        }

        public static DataTable SelectGoods(string where)
        {
            return PpPro.SelectGoods(where);
        }

        public static DataTable SelectSHXX(string where)
        {
            return PpPro.SelectSHXX(where);
        }
        public static int UpdateSHValidate(string where, string xxid)
        {
            return PpPro.UpdateSHValidate(where, xxid);
        }

        public static int UpdateGoods(string Amount, string where)
        {
            return PpPro.UpdateGoods(Amount, where);
        }
        public static bool UpdateSH(PP_ReceivingInformation pp, List<PP_StorageDetailed> list, ref string strErr)
        {
            return PpPro.UpdateSH(pp, list, ref strErr);
        }


        public static bool deleteFile(string id)
        {
            return PpPro.deleteFile(id);
        }

        public static DataTable SelectSHCP(string where)
        {
            return PpPro.SelectSHCP(where);
        }
        public static bool UpdateCPSHXX(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            return PpPro.UpdateCPSHXX(record, DID, delist, ref  strErr, str);
        }

        public static DataTable SelectSHSupplier(string where)
        {
            return PpPro.SelectSHSupplier(where);
        }
        #endregion
        #region[退货]
        public static bool InsertTH(PP_ReturnGoods record, string DID, List<PP_ReturngoodsDetails> delist, ref string strErr)
        {
            return PpPro.InsertTH(record, DID, delist, ref strErr);
        }

        public static string GetTopListTHID()
        {
            return PpPro.GetTopListTHID();
        }

        public static DataTable SelectTHDDID(string where)
        {
            return PpPro.SelectTHDDID(where);
        }

        public static UIDataTable SelectTHGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectTHGoods(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable SelectTH(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectTH(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectTHXQ(string where)
        {
            return PpPro.SelectTHXQ(where);
        }

        public static bool UpdateTH(PP_ReturnGoods pp, List<PP_ReturngoodsDetails> list, ref string strErr)
        {
            return PpPro.UpdateTH(pp, list, ref strErr);
        }

        public static int UpdateTHValidate(string where)
        {
            return PpPro.UpdateTHValidate(where);
        }
        #endregion


        #region[入库]
        public static string GetTopListRKID()
        {
            return PpPro.GetTopListRKID();
        }

        public static bool InsertRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr)
        {
            return PpPro.InsertRK(record, DID, delist, ref strErr);
        }
        public static bool InsertCPRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr, List<PP_ChoseGoods> cp)
        {
            return PpPro.InsertCPRK(record, DID, delist, ref  strErr, cp);
        }
        public static UIDataTable SelectRK(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectRK(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectRKXQ(string where)
        {
            return PpPro.SelectRKXQ(where);
        }

        public static DataTable SelectRKDDID(string where)
        {
            return PpPro.SelectRKDDID(where);
        }
        public static DataTable SelectGoodsreceiptDetailed(string where)
        {
            return PpPro.SelectGoodsreceiptDetailed(where);
        }
        public static UIDataTable SelectRKGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectRKGoods(a_intPageSize, a_intPageIndex, where);
        }

        public static bool UpdateRK(PP_PurchaseInventorys pp, List<PP_GoodsreceiptDetailed> list, ref string strErr)
        {
            return PpPro.UpdateRK(pp, list, ref strErr);
        }

        public static int UpdateRKValidate(string where, string rktype)
        {
            return PpPro.UpdateRKValidate(where, rktype);
        }

        public static DataTable SelectRKSupplier(string where)
        {
            return PpPro.SelectRKSupplier(where);
        }

        public static UIDataTable SelectRKCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectRKCPXX(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectRKCP(string where)
        {
            return PpPro.SelectRKCP(where);
        }

        public static bool UpdateCPRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr, List<PP_ChoseGoods> cp)
        {
            return PpPro.UpdateCPRK(record, DID, delist, ref   strErr, cp);
        }
        #endregion

        #region[付款]

        public static string GetTopListFKID()
        {
            return PpPro.GetTopListFKID();
        }
        public static bool InsertFK(PP_Payment record, string DID, List<PP_PaymentGoods> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            return PpPro.InsertFK(record, DID, delist, ref strErr, str);
        }

        public static bool InsertLJFK(PP_Payment record, string DID, List<PP_PaymentGoods> delist, ref string strErr, List<string> str)
        {
            return PpPro.InsertLJFK(record, DID, delist, ref strErr, str);
        }
        public static DataTable SelectFKDDID(string where)
        {
            return PpPro.SelectFKDDID(where);
        }

        public static UIDataTable SelectFK(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectFK(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable SelectFKGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectFKGoods(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectFKXQ(string where)
        {
            return PpPro.SelectFKXQ(where);
        }

        public static bool UpdateFK(PP_Payment pp, List<PP_PaymentGoods> list, ref string strErr)
        {
            return PpPro.UpdateFK(pp, list, ref strErr);
        }

        public static DataTable SelectFKDC()
        {
            return PpPro.SelectFKDC();
        }

        public static DataTable SelectFKCP(string where)
        {
            return PpPro.SelectFKCP(where);
        }
        public static UIDataTable SelectFKCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectFKCPXX(a_intPageSize, a_intPageIndex, where);
        }

        public static bool UpdateFKCP(PP_Payment record, List<PP_PaymentGoods> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            return PpPro.UpdateFKCP(record, delist, ref strErr, str);
        }

        public static DataTable SelectFKSupplier(string where)
        {
            return PpPro.SelectFKSupplier(where);
        }
        #endregion

        #region[上传]
        public static bool InsertNewFile(PP_File pp, byte[] fileByte, ref string a_strErr)
        {
            if (PpPro.InsertFile(pp, fileByte, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewDownLoad(string id)
        {
            return PpPro.GetDownload(id);
        }


        public static DataTable getFile(string id)
        {
            return PpPro.getFile(id);
        }
        #endregion
        #region[供货商]
        public static DataTable GetSupplier(string SID)
        {
            return PpPro.GetSupplier(SID);
        }

        public static DataTable GetProductPrice(string ProID, string SupID)
        {
            return PpPro.GetProductPrice(ProID, SupID);
        }
        #endregion


        #region [审批]
        public static bool InsertApproval(PP_Approval app)
        {
            return PpPro.InsertApproval(app);
        }
        public static DataTable SelectApproval(string CID)
        {
            return PpPro.SelectApproval(CID);
        }
        public static string GetApprovalSPID()
        {
            return PpPro.GetApprovalSPID();
        }

        public static DataTable SelectApprovalUser(string approvaler, string approvaltype)
        {
            return PpPro.SelectApprovalUser(approvaler, approvaltype);
        }

        public static UIDataTable SelectSP(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectSP(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable SelectDDSP(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectDDSP(a_intPageSize, a_intPageIndex, where);
        }
        #endregion

        #region[系统设置]
        public static UIDataTable SelectConfig(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectConfig(a_intPageSize, a_intPageIndex, where);
        }
        public static List<SelectListItem> GetNewConfig()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = PpPro.GetConfigContent();
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
                SelListItem.Value = dtDesc.Rows[i]["Type"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Type"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static bool InsertNewContent(string type, string text, ref string a_strErr)
        {
            if (PpPro.InsertContent(type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static bool UpdateNewContent(string xid, string type, string text, ref string a_strErr)
        {
            if (PpPro.UpdateContent(xid, type, text, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteNewContent(string xid, string type, ref string a_strErr)
        {
            if (PpPro.DeleteContent(xid, type, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool DeleteFK(string where)
        {
            return PpPro.DeleteFK(where);
        }
        #endregion

        #region[二期采购]
        #region[物流]
        public static string GetWLID()
        {
            return PpPro.GetWLID();
        }
        public static bool InsertWL(PP_Logistics record, List<PP_LogisticsGoods> delist, ref string strErr)
        {
            return PpPro.InsertWL(record, delist, ref strErr);
        }

        public static UIDataTable SelectWL(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectWL(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable SelectWLGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectWLGoods(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectWLGoodsXX(string where)
        {
            return PpPro.SelectWLGoodsXX(where);
        }
        public static bool UpdateWL(PP_Logistics record, List<PP_LogisticsGoods> delist, ref string strErr)
        {
            return PpPro.UpdateWL(record, delist, ref strErr);
        }

        public static bool DeleteWL(string where)
        {
            return PpPro.DeleteWL(where);
        }
        #endregion
        #region[订购]
        public static bool ErInsertOrder(PP_ErPurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {
            return PpPro.ErInsertOrder(record, delist, ref   strErr);
        }
        public static bool ErDDUpdate(PP_ErPurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {
            return PpPro.ErDDUpdate(record, delist, ref   strErr);
        }
        public static DataTable ErSelectGoodsDDID(string where)
        {
            return PpPro.ErSelectGoodsDDID(where);
        }
        #endregion

        #endregion
        #region[三期采购]
        #region[订购]
        public static bool InsertOrderSan(PP_PurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {
            return PpPro.InsertOrderSan(record, delist, ref strErr);
        }
        public static bool SanUpdateDDS(PP_PurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {
            return PpPro.SanUpdateDDS(record, delist, ref strErr);
        }
        #endregion
        #endregion

        #region[交接单]

        public static string GetTopListJJID()
        {
            return PpPro.GetTopListJJID();
        }
        public static bool InsertJJD(PP_TransferS tran, List<PP_TransferGoods> goods)
        {
            return PpPro.InsertJJD(tran, goods);
        }

        public static UIDataTable SelectJJD(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectJJD(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable SelectJJDGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            return PpPro.SelectJJDGoods(a_intPageSize, a_intPageIndex, where);
        }

        public static DataTable SelectJJDXX(string where)
        {
            return PpPro.SelectJJDXX(where);
        }

        public static bool InsertJJDXX(PP_TransferS tran, List<PP_TransferGoods> goods)
        {
            return PpPro.InsertJJDXX(tran, goods);
        }

        public static bool UpdatePeopleSC(PP_TransferS tran)
        {
            return PpPro.UpdatePeopleSC(tran);
        }

        public static bool UpdatePeopleJH(PP_TransferS tran)
        {
            return PpPro.UpdatePeopleJH(tran);
        }
        public static bool UpdateWarehouse(PP_TransferS tran)
        {
            return PpPro.UpdateWarehouse(tran);
        }
        #endregion

        #region [上传]
        public static bool InsertBiddingNewS(tk_FileUpload fileUp,HttpFileCollection Filedata, ref string strErr)
        {
            if (PpPro.InsertBiddingNewS(fileUp,  Filedata, ref strErr) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
