using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Mvc;
using System.Web;

namespace TECOCITY_BGOI
{
    public class SalesManage
    {
        #region [备案管理]

        public static string GetTopPID()
        {
            return SalesPro.GetTopPID();
        }
        public static string GetNewPid()
        {
            return SalesPro.GetNewPid();
        }
        //检查是否存在相同的工程编号和工程名称
        public static int CheckPlanIDandPlanName(ProjectBasInfo Project) 
        {
            return SalesPro.CheckPlanIDandPlanName(Project);
        }
        public static DataTable GetConfigInfo(string taskType)
        {
            return SalesPro.GetConfigInfo(taskType);
        }
        public static UIDataTable GetProjectDetail(int PageSize, int PageIndex, string PID)
        {
            return SalesPro.GetProjectDetail(PageSize, PageIndex, PID);
        }
        public static UIDataTable GetSalesGridInfo(int a_intPageSize, int a_intPageIndex, string strWhere,string ordercontent,string specification)
        {
            return SalesPro.GetSalesGridInfo(a_intPageSize, a_intPageIndex, strWhere, ordercontent, specification);
        }

        public static UIDataTable GetOrderInfoGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.GetOrderInfoGrid(a_intPageSize, a_intPageIndex, strWhere);
        }


        public static DataTable ProjectBasInfoRelBill(string PID, ref string strErr)
        {
            return SalesPro.ProjectBasInfoRelBill(PID, ref strErr);
        }

        public static UIDataTable GetRecordOfferInfo(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            return SalesPro.GetRecordOfferInfo(a_intPageSize, a_intPageIndex, BJID);
        }
        public static UIDataTable GetRecordOffer(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            return SalesPro.GetRecordOffer(a_intPageSize, a_intPageIndex, BJID);
        }
        public static UIDataTable GetOrderInfoDetailGrid(int a_intPageSize, int a_intPageIndex, string DHID)
        {
            return SalesPro.GetOrderInfoDetailGrid(a_intPageSize, a_intPageIndex, DHID);
        }

        public static UIDataTable GetCheckDetail(int PageSize, int PageIndex)
        {
            return SalesPro.GetCheckDetail(PageSize, PageIndex);
        }
        public static UIDataTable GetOrdersDetails(int PageSize, int PageIndex, string OID)
        {
            return SalesPro.GetOrdersDetails(PageSize, PageIndex, OID);
        }
        public static bool AddRecordInfo(string PID, Sales_RecordInfo record, List<Sales_RecordDetail> delist, string CreateUser, ref string strErr)
        {
            return SalesPro.AddRecordInfo(PID, record, delist, CreateUser, ref strErr);
        }
        public static List<SelectListItem> ListItem()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetListItem();

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;

        }

        public static List<SelectListItem> GetBelongArea()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetBelongArea();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static List<SelectListItem> GetChannelsFrom()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetChannelsFrom();
            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static ProjectSelect_Config GetSelectPro(string ID)
        {

            return SalesPro.GetSelectPro(ID);

        }

        public static bool AddProjectBaseInfo(ProjectBasInfo Project, List<ProjectDetail> list, ref string strErr)
        {
            return SalesPro.AddProjectBaseInfo(Project, list, ref strErr);
        }

        public static DataTable GetRecordToExcel(string strWhere, string ordercontent, string specification)
        {
            return SalesPro.GetRecordToExcel(strWhere,ordercontent, specification);
        }


        public static ProjectBasInfo getProjectBaseInfo(string PID) 
        {
            return SalesPro.getProjectBaseInfo(PID);
        }

        public static DataTable GetPrintProjectDetail(string PID)
        {
            return SalesPro.GetPrintProjectDetail(PID);
        }

        public static bool UPdateProjectState(string PID)
        {
            return SalesPro.UPdateProjectState(PID);
        }


        public static UIDataTable  GetLogGrid(string ID,int a_intPageSize,int a_intPageIndex) 
        {
            return SalesPro.GetLogGrid(ID,a_intPageSize,a_intPageIndex );
        }


        #endregion
        #region [物品管理]
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            return SalesPro.GetPtype(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype,string UnitID,string PName,string PID,string SPec)
        {
            return SalesPro.ChangePtypeList(a_intPageSize, a_intPageIndex, ptype,UnitID,PName ,PID ,SPec);
        }

        public static DataTable GetChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return SalesPro.GetChangePtypeList(a_intPageSize, a_intPageIndex, ptype);
        }
        public static DataTable GetBasicDetail(string PID)
        {
            return SalesPro.GetBasicDetail(PID);
        }
        #endregion
        #region [报价管理]
        public static string GetNewBJID()
        {
            return SalesPro.GetNewBJID();
        }
        public static UIDataTable GetOfferGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.GetOfferGrid(a_intPageSize, a_intPageIndex, strWhere);
        }
        public static UIDataTable GetSearchOfferGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.GetSearchOfferGrid(a_intPageSize, a_intPageIndex, strWhere);
        }
        public static Offer getOfferByBJID(string BJID)
        {
            return SalesPro.getOfferByBJID(BJID);
        }

        public static DataTable getProjectDetailGrid(string PID)
        {
            return SalesPro.getProjectDetailGrid(PID);
        }

        public static bool SaveOffer(Offer offer, List<OfferInfo> list, ref string strErr)
        {
            return SalesPro.SaveOffer(offer, list, ref strErr);
        }

        public static DataTable GetOfferInfoGrid(string BJID)
        {
            return SalesPro.GetOfferInfoGrid(BJID);
        }

        public static UIDataTable GetOfferDetailGrid(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            return SalesPro.GetOfferDetailGrid(a_intPageSize, a_intPageIndex, BJID);
        }
        public static bool SaveUpdateOffer(Offer offer, List<OfferInfo> list, ref string strErr)
        {
            return SalesPro.SaveUpdateOffer(offer, list, ref strErr);
        }
        public static bool CancelOffer(string ID, ref string strErr)
        {
            return SalesPro.CancelOffer(ID, ref strErr);
        }

        public static UIDataTable GetBJOrders(int PageSize, int PageIndex, string PID)
        {
            return SalesPro.GetBJOrders(PageSize, PageIndex, PID);
        }

        public static DataTable GetOfferToExcel(string where, ref string strErr)
        {
            return SalesPro.GetOfferToExcel(where, ref strErr);
        }

        public static bool InsertBiddingNew(tk_CFile fileUp, HttpFileCollection Filedata, ref string strErr)
        {
            if (SalesPro.InsertBiddingNew(fileUp, Filedata, ref strErr) > 0)
                return true;
            else
                return false;
        }

        public static UIDataTable GetUploadFileGrid(int PageSize, int PageIndex, string CID)
        {
            return SalesPro.GetUploadFileGrid(PageSize, PageIndex, CID);
        }
        #endregion
        #region [供应商]
        public static DataTable GetSupplier(string SID)
        {
            return SalesPro.GetSupplier(SID);
        }
        public static UIDataTable GetSupType(int a_intPageSize, int a_intPageIndex)
        {
            return SalesPro.GetSupType(a_intPageSize, a_intPageIndex);
        }
        public static UIDataTable GetCheckSupList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            return SalesPro.GetCheckSupList(a_intPageSize, a_intPageIndex, ptype);
        }

        #endregion
        #region [订单管理]
        //获取订单ID
        public static string GetNewOrderID()
        {
            return SalesPro.GetNewOrderID();
        }
        public static List<SelectListItem> GetUM_USER(string DeptID)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetUM_USER(DeptID);

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["UserName"].ToString();//UserId
                SelListItem.Text = dt.Rows[i]["UserName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;

        }
        public static DataTable GetProject(string ID)
        {
            return SalesPro.GetProject(ID);
        }
        public static bool SaveOrderInfo(OrdersInfo orderinfo, List<Orders_DetailInfo> list, ref string strErr)
        {
            return SalesPro.SaveOrderInfo(orderinfo, list, ref strErr);
        }

        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.GetOrderInfo(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static DataTable GetOrdersDetail(string OrderID)
        {
            return SalesPro.GetOrdersDetail(OrderID);
        }

        public static DataTable GetOredersShipmentDetail(string OrderID)
        {
            return SalesPro.GetOredersShipmentDetail(OrderID);
        }
        public static DataTable GetOrdersInfo(string OrderID)
        {
            return SalesPro.GetOrdersDetail(OrderID);
        }
        public static DataTable GetOrdersDetailBYDID(string DID)
        {
            return SalesPro.GetOrdersDetailBYDID(DID);
        }


        public static OrdersInfo GetOrdersByOrderID(string OrderID)
        {
            return SalesPro.GetOrdersByOrderID(OrderID);
        }


        public static bool SaveUpdateOrderInfo(OrdersInfo orderinfo, List<Orders_DetailInfo> list, ref string strErr)
        {
            return SalesPro.UpdateOrderInfo(orderinfo, list, ref strErr);
        }

        public static bool CanCelOrdersInfo(string OrderID, ref string strErr)
        {
            return SalesPro.CanCelOrdersInfo(OrderID, ref strErr);
        }


        public static UIDataTable LoadOrderDetail(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesPro.LoadOrderDetail(a_intPageSize, a_intPageIndex, OrderID);
        }

        public static UIDataTable LoadOrderBill(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesPro.LoadOrderBill(a_intPageSize, a_intPageIndex, OrderID);
        }
        public static DataTable GetOrderInfoToExcel(string where, ref string strErr)
        {
            return SalesPro.GetOrderInfoToExcel(where, ref strErr);
        }

        public static string getOrderContractID(string OrderID)
        {
            return SalesPro.getOrderContractID(OrderID);
        }

        public static string GetMaxContractID() 
        {
            return SalesPro.GetMaxContractID();
        }

        public static UIDataTable GetOrderHTXXGrid(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesPro.GetOrderHTXXGrid(a_intPageSize,a_intPageIndex,OrderID);
        }
        public static UIDataTable GetOrderFJXXGrid(int a_intPageSize, int a_intPageIndex, string OrderID) 
        {
            return SalesPro.GetOrderFJXXGrid(a_intPageSize, a_intPageIndex, OrderID);
        }


        public static string GetOrdersDetailDID(string OrderID) 
        {
            return SalesPro.GetOrdersDetailDID(OrderID );
        }
        #endregion

        #region [发货管理]
        public static string GetNewShipGoodsID()
        {
            return SalesPro.GetNewShipGoodsID();
        }
        public static bool SaveOrderShip(Shipments shipments, List<Shipments_DetailInfo> list,List<Orders_DetailInfo>listDetail, ref string strErr)
        {
            return SalesPro.SaveOrderShip(shipments, list,listDetail, ref strErr);
        }

        public static UIDataTable LoadOrderShipment(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            return SalesPro.LoadOrderShipment(a_intPageSize, a_intPageIndex, ShipGoodsID);
        }

        public static UIDataTable LoadOrderShipmentDetail(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            return SalesPro.LoadOrderShipmentDetail(a_intPageSize, a_intPageIndex, ShipGoodsID);
        }

        public static UIDataTable LoadShipmentsGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.LoadShipmentsGrid(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static DataTable GetShipmentDetail(string ShipGoodsID)
        {
            return SalesPro.GetShipmentDetail(ShipGoodsID);
        }
        public static DataTable GetShipments(string ShipGoodsID, string Orderid)
        {
            return SalesPro.GetShipments(ShipGoodsID, Orderid);
        }

        public static Shipments getShipmentsBySID(string ShipGoodsID)
        {
            return SalesPro.getShipmentsBySID(ShipGoodsID);
        }

        public static bool SaveUpdateShipment(Shipments shipments, List<Shipments_DetailInfo> list, ref string strErr)
        {
            return SalesPro.SaveUpdateShipment(shipments, list, ref strErr);
        }

        public static bool CanCelShipments(string ShipGoodsID, ref string strErr)
        {
            return SalesPro.CanCelShipments(ShipGoodsID, ref strErr);
        }

        public static DataTable ShipmentsToExcel(string where, ref string strErr)
        {
            return SalesPro.ShipmentsToExcel(where, ref strErr);
        }

        #endregion



        #region [退货管理]
        public static string getNewGoodsID()
        {
            return SalesPro.getNewGoodsID();
        }

        public static string getNewExCheckID()
        {
            return SalesPro.getNewExCheckID();
        }
        //退换货类型
        public static List<SelectListItem> GetEXCType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetEXCType();

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> GetTypeSelect(string type)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetTypeSelect(type);

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static DataTable GetShipmentsDetail(string OrderID)
        {
            return SalesPro.GetShipmentsDetail(OrderID);
        }

        public static bool SaveExchangeGoods(ExchangeGoods excgoods, List<ExReturn_Detail> RList,  ref string strErr)
        {
            return SalesPro.SaveExchangeGoods(excgoods, RList, ref strErr);
        }


        public static bool SaveExcGoodsAndDetail(ExchangeGoods exchangegoods,List<Exchange_Detail>EList,ref string strErr)
        {
            return SalesPro.SaveExchangeGoods(exchangegoods, EList, ref strErr);
        }
        public static bool SaveUpdateExcGoods(ExchangeGoods excgoods,  List<ExReturn_Detail> RList, ref string strErr)
        {
            return SalesPro.SaveUpdateExcGoods(excgoods,  RList, ref strErr);
        }

        public static bool SaveUpdateReturnGoods(ExchangeGoods excgoods, List<Exchange_Detail> RList, ref string strErr)
        {
            return SalesPro.SaveUpdateReturnGoods(excgoods, RList, ref strErr);
        }
        public static UIDataTable LoadExchangeGoodsGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.LoadExchangeGoodsGrid(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadExchangeDetail(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.LoadExchangeDetail(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static UIDataTable LoadReturnDetail(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.LoadReturnDetail(a_intPageSize, a_intPageIndex, strWhere);
        }

        public static DataTable LoadExchangeBill(string OrderID, ref string strErr)
        {
            return SalesPro.LoadExchangeBill(OrderID, ref strErr);
        }
        public static DataTable GetExchangeGoodsDetail(string EID, ref string strErr)
        {
            return SalesPro.GetExchangeGoodsDetail(EID, ref strErr);
        }
        public static DataTable GetExchangeDetailByDID(string DID, ref string strErr)
        {
            return SalesPro.GetExchangeGoodsDetailByDID(DID, ref strErr);
        }

        public static DataTable GetReturnDetailByDID(string DID, ref string strErr)
        {
            return SalesPro.GetReturnDetailByDID(DID, ref strErr);
        }

        public static DataTable GetShipmentOrdersDetail(string OrderID) 
        {
            return SalesPro.GetShipmentOrdersDetail(OrderID);
        }
        public static DataTable GetReturnDetailByEID(string EID, ref string strErr)
        {
            return SalesPro.GetReturnDetailByEID(EID, ref strErr);
        }
        public static ExchangeGoods GetExchangeGoodsBYEID(string EID)
        {
            return SalesPro.GetExchangeGoodsByEID(EID);
        }
        public static ExchangeGoods GetExchangeGoods(string EID)
        {
            return SalesPro.GetExchangeGoods(EID);
        }


     

        public static DataTable GetExchangeGoodsCheckDetail(string EID, ref string strErr)
        {
            return SalesPro.GetExchangeGoodsCheckDetail(EID, ref strErr);
        }
        public static ExchangeGoods GetExchangeGoodsBYOrderID(string ID)
        {
            return SalesPro.GetExchangeGoodsBYOrderID(ID);
        }

        //获取退货和换货的物品数据
        public static DataTable GetExcAndReturnDetailByEID(string EID, ref string strErr)
        {
            return SalesPro.GetExcAndReturnDetailByEID(EID, ref strErr);
        }
        //生成检验表
        public static bool SaveExchangeCheck(Exchange_Check check, List<ExchangeGoods_DetailInfo> listDetailInfo, ref string strErr)
        {
            return SalesPro.SaveExchangeCheck(check, listDetailInfo, ref strErr);
        }


        public static bool CanCelExchangGoods(string EID, ref string strErr)
        {
            return SalesPro.CanCelExchangGoods(EID, ref strErr);
        }
        public static bool SaveExchangeGoodsFinish(ExchangeGoods exGoods, ref string strErr)
        {
            return SalesPro.SaveExchangeGoodsFinish(exGoods, ref strErr);
        }
        public static DataTable ExchangeGoodsManageToExcel(string where, ref string strErr)
        {
            return SalesPro.ExchangeGoodsManageToExcel(where, ref strErr);
        }

        public static UIDataTable GetExChangeGoodsByOID(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesPro.GetExChangeGoodsByOID(a_intPageSize, a_intPageIndex, OrderID);
        }

        #endregion

        #region [回款管理]
        public static string GetHKNO(string UnitID)
        {
            return SalesPro.GetHKNO(UnitID);
        }
        //获取汇款方式
        public static List<SelectListItem> Methods()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.Methods();

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
                SelListItem.Value = dt.Rows[i]["ID"].ToString();
                SelListItem.Text = dt.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;

        }
        //获取未回款的订单
        public static List<SelectListItem> GetOrderID()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dt = SalesPro.GetOrderID();

            if (dt == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dt.Rows[i]["OrderID"].ToString();
                SelListItem.Text = dt.Rows[i]["OrderID"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        public static bool SaveReceivePayment(ReceivePayment rpayment, ref string strErr)
        {
            return SalesPro.SaveReceivePayment(rpayment, ref strErr);
        }

        public static UIDataTable LoadReceivePayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            return SalesPro.LoadReceivePayment(a_intPageSize, a_intPageIndex, strWhere);
        }
        public static DataTable ReceivePaymentToExcel(string where, ref string strErr)
        {
            return SalesPro.ReceivePaymentToExcel(where, ref strErr);
        }

        public static DataTable LoadReceiveBill(string OrderID, ref string strErr)
        {
            return SalesPro.LoadReceiveBill(OrderID, ref strErr);
        }
        public static UIDataTable LoadReceivePaymentBill(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            return SalesPro.LoadReceivePaymentBill(a_intPageSize, a_intPageIndex, OrderID);
        }

        public static ReceivePayment getReceivePayment(string RID)
        {
            return SalesPro.getReceivePayment(RID);
        }

        public static bool SaveUpdateReceivePayment(ReceivePayment receivepayment, ref string strErr)
        {
            return SalesPro.SaveUpdateReceivePayment(receivepayment, ref strErr);
        }

        public static bool CancelReceivePayment(string RID, ref string strErr)
        {
            return SalesPro.CancelReceivePayment(RID, ref strErr);
        }

        public static DataTable GetReceivePaymentByOrderID(string OrderID)
        {
          return  SalesPro.GetReceivePaymentByOrderID(OrderID);
        }
        //获取订单的合同额，和已回款
        public static DataTable  getOrdersInfoTotal(string OrderID)
        {
            return SalesPro.getOrdersInfoTotal(OrderID);
        }

        //回款提醒
        public static UIDataTable GetShowReceivePayment(int a_intPageSize, int a_intPageIndex, string strWhere) 
        {
            return SalesPro.GetShowReceivePayment(a_intPageSize,a_intPageIndex,strWhere);
        }

        public static DataTable GetTopShowReceivePayment() 
        {
            return SalesPro.GetTopShowReceivePayment();
        }
        #endregion

        #region 审批
        public static UIDataTable GetProjectBasApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.GetProjectBasApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable GetOfferApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.GetOfferApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable GetOrderApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.GetOrderApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }


        public static int JudgeNewLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            return SalesPro.judgeLoginUser(userid, webkey, folderBack, SPID);
        }

        public static int ExJudgeNewLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            return SalesPro.ExjudgeLoginUser(userid, webkey, folderBack, SPID);
        }
        public static bool InsertNewApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            if (SalesPro.InsertApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetContractSPID(string CID)
        {
            return SalesPro.GetContractSPID(CID);
        }

        public static bool UpdateNewApproval(string IsPass, string Opinion, string Remark,string PID,string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            if (SalesPro.UpdateApproval(IsPass, Opinion, Remark,PID, webkey, folderBack, RelevanceID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable getNewConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            return SalesPro.getConditionGrid11(a_intPageSize, a_intPageIndex, where, folderBack);
        }
        public static UIDataTable GetUserLogGrid(int a_intPageSize, int a_intPageIndex, string ReleVanceID)
        {

            return SalesPro.GetUserLogGrid( a_intPageSize, a_intPageIndex, ReleVanceID);
        }


        #endregion



        #region [退货审批]
           public static UIDataTable EXGetProjectBasApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.EXGetProjectBasApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static UIDataTable EXGetOfferApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.EXGetOfferApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable GetEXOrderApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SalesPro.GetEXOrderApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }

        public static bool EXInsertNewApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            if (SalesPro.EXInsertApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable EXGetContractSPID(string CID)
        {
            return SalesPro.EXGetContractSPID(CID);
        }

        public static bool EXUpdateNewApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            if (SalesPro.EXUpdateApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static UIDataTable EXgetNewConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            return SalesPro.EXgetConditionGrid(a_intPageSize, a_intPageIndex, where, folderBack);
        }

        //public static int JudgeNewLoginUser(string userid, string webkey, string folderBack, string SPID)
        //{
        //    return SalesPro.judgeLoginUser(userid, webkey, folderBack, SPID);
        //}
        #endregion
        #region [结算]
        public static string getNewFinishID() 
        {
            return SalesPro.getNewFinishID();
        }
        public static bool InsertProFinish(ProFinish profinish, ref string strErr)
        {
            return SalesPro.InsertProFinish(profinish, ref strErr);
        }

        public static decimal getNewDebtAmount(string OrderID)
        {
            return SalesPro.getDebtAmount(OrderID);
        }

        public static string GetContractID(string OrderID) 
        {
            return SalesPro.GetContractID(OrderID);
        }
        #endregion

        #region [统计分析]
        public static DataTable GetStatisticsManageTable(string where)
        {
            return SalesPro.GetStatisticsManageTable(where);
        }
        public static string getNewCountDebt(string where)
        {
            return SalesPro.getCountDebt(where);
        }

        public static UIDataTable GetOrdersInfoStatisticalGrid(int a_intPageSize,int a_intPageIndex,string where)
        {
            return SalesPro.GetOrdersInfoStatisticalGrid(a_intPageSize, a_intPageIndex,where);
        }
        public static DataTable GetOrderStaticalGrid(string where) 
        {
            return SalesPro.GetOrderStaticalGrid(where);
        }
        public static string GetOrderStatistical(string where) 
        {
            return SalesPro.GetOrderStaticstical(where);
        }

        //人员统计分析
        public static DataTable SalesSummaryTable(string where) 
        {
            return SalesPro.SalesSummaryTable(where);
        }

        public static string  GetSalesSummary(string where)
        {
            return SalesPro.GetSalesSummary(where);
        }
        //本月累计汇总
        public static DataTable GetMonthsSalesSummaryTable(string where)
        {
            return SalesPro.GetMonthsSalesSummaryTable(where);
        }
        //设备统计
        public static DataTable GetEquipmentSalesSummaryTable(string StartDate,string EndDate) 
        {
            return SalesPro.GetEquipmentSalesSummaryTable(StartDate ,EndDate );
        }
        //调压箱统计
        public static DataTable GetPressureRegulatingBoxTable(string StartDate, string EndDate) 
        {
            return SalesPro.GetPressureRegulatingBoxTable(StartDate, EndDate);
        }
        //高压箱统计
        public static DataTable GetHighVoltageCompartmentTable(string StartDate, string EndDate)
        {
            return SalesPro.GetHighVoltageCompartmentTable(StartDate, EndDate);
        }


        //切断阀
        public static DataTable GetCutOffSalesSummaryTable(string StartDate, string EndDate) 
        {
            return SalesPro.GetCutOffSalesSummaryTable(StartDate, EndDate);
        }

        //调压器

        public static DataTable GetRegulatorSummaryTable(string StartDate, string EndDate) 
        {
            return SalesPro.GetHighVoltageCompartmentTable(StartDate, EndDate);
        }
        //其他设备统计
        public static DataTable GetOtherEquipmentTable(string StartDate, string EndDate) 
        {
            return SalesPro.GetOtherEquipmentTable(StartDate, EndDate);
        }
        #endregion

        #region [经验分析]

        public static  DataTable GetContractStatisticalAnalysisTable(string DateTime,string  LastMonthTime,string LastYearTime)
        {
            return SalesPro.GetContractStatisticalAnalysisTable(DateTime, LastMonthTime, LastYearTime);
        }



        public static DataTable GetContractNowStatisticalAnalysisTable(string StartTime,string EndTime,string LastStartTime,string LastEndTime) 
        {
            return SalesPro.GetContractNowStatisticalAnalysisTable(StartTime, EndTime, LastStartTime, LastEndTime);
        
        }


        public static DataTable GetReceivableAccountTable(string StartTime, string EndTime, string LastStartTime, string LastEndTime) 
        {
            return SalesPro.GetReceivableAccountTable(StartTime, EndTime, LastStartTime, LastEndTime);
       
        }


        public static DataTable GetReceivableAccountTable(string DateTime,string LastDateTime) 
        {
            return SalesPro.GetReceivableAccountTable(DateTime,LastDateTime);

        }


        public static DataTable GetDeceiveReceivableAccountTable(string DateTime) 
        {
            return SalesPro.GetDeceiveReceivableAccountTable(DateTime);
        }

        public static DataTable getLJYSKReceivableTable(string DateTime) 
        {
            return SalesPro.getLJYSKReceivableTable(DateTime );
        }

        public static DataTable GetAccountsPayableStatisticalAnalysis2Table(string DateTime) 
        {
            return SalesPro.GetAccountsPayableStatisticalAnalysis2Table(DateTime);
        }


        public static DataTable GetAccountsPayableNotMonthTable(string DateTime) 
        {
            return SalesPro.GetAccountsPayableNotMonthTable(DateTime );
        }

        public static DataTable getAccountsPayableYearsTable()
        {
            return SalesPro.getAccountsPayableYearsTable();
        }


        /// <summary>
        /// 自有产品销售量
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetMonthOwnProductSalesAmount(string Datetime,string LastMonthDatetime,string LastYearDatetime) 
        {
            return SalesPro.GetMonthOwnProductSalesAmount(Datetime ,LastMonthDatetime,LastYearDatetime);
        }

        /// <summary>
        /// 自有产品销售额，合同额
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>

        public static DataTable GetMonthOwnProductSalesTotal(string Datetime, string LastMonthDateTime, string LastYearDatetime)
        {
            return SalesPro.GetMonthOwnProductSalesTotal(Datetime, LastMonthDateTime, LastYearDatetime);
        }

        /// <summary>
        /// 自有产品累计销售量
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetMonthlyAountOwnProducts(string StartDatetime, string EndDatetime, string LastStartDatetime, string LastEndDatetime)
        {
            return SalesPro.GetMonthlyAountOwnProducts(StartDatetime, EndDatetime, LastStartDatetime, LastEndDatetime);
        }
        /// <summary>
        /// 
        /// 自有产品累计销售额
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetMonthlyTotalOwnProducts(string StartDatetime, string EndDatetime, string LastStartDatetime, string LastEndDatetime)
        {
            return SalesPro.GetMonthlyTotalOwnProducts(StartDatetime, EndDatetime, LastStartDatetime, LastEndDatetime);
        }


        public static DataTable GetMonthOwnProductChannelsFrom(string Datetime,string StartDateTime,string EndDateTime)
        {
            return SalesPro.GetMonthOwnProductChannelsFrom(Datetime ,StartDateTime ,EndDateTime);
        }

        public static DataTable GetMonthOwnProductModelAountTop10(string Datetime) 
        {
            return SalesPro.GetMonthOwnProductModelAountTop10(Datetime);
        }

        public static DataTable GetMonthOwnProductModelTotalTop10(string Datetime)
        {
            return SalesPro.GetMonthOwnProductModelTotalTop10(Datetime);
        }

        public static DataTable GetMonthOwnProductFromToAountTop10(string Datetime,string EndTime) 
        {
            return SalesPro.GetMonthOwnProductModelFromToAountTop10(Datetime, EndTime);
        }

        public static DataTable GetMonthOwnProductFromToTotalTop10(string Datetime, string EndTime)
        {
            return SalesPro.GetMonthOwnProductModelFromToTotalTop10(Datetime, EndTime);
        }

        #endregion


        #region [库存查询]

        public static UIDataTable GetInventoryGrid(int IntPageSize,int IntPageIndex,string Ptype,string where) 
        {
            return SalesPro.GetInventoryGrid(IntPageSize,IntPageIndex,Ptype,where);
        }


        #endregion


        #region [获取物品单价]
        public static DataTable GetProductPrice(string ProID, string SupID)
        {
            return SalesPro.GetProductPrice(ProID, SupID);
        }
        #endregion

        #region [获取登录人的首字母]
        public static string GetNamePY(string LoginName) 
        {
            return SalesPro.GetNamePY(LoginName);
        }
        #endregion

        #region [上传合同文件]
        public static bool InsertNewFile(string id, byte[] fileByte, string FileName, ref string a_strErr)
        {
            if (SalesPro.InsertFile(id, fileByte, FileName, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static DataTable GetNewDownloadFile(string id)
        {
            return SalesPro.GetDownloadFile(id);
        }
        public static DataTable GetDownload(string ID) 
        {
            return SalesPro.GetDownload(ID);
        }
        //GetUploadFile
        public static DataTable GetUploadFile(string ID)
        {
            return SalesPro.GetUploadFile(ID);
        }


        public static bool DellNewFile(string ID, ref string a_strErr)
        {
            if (SalesPro.DeleteFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }


        public static bool UpdateNewCCashBack(CCashBack Cash, ref string a_strErr)
        {
            if (SalesPro.UpdateCCashBack(Cash, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        public static bool dellNewCCashBack(string id,string cid, ref string a_strErr)
        {
            if (SalesPro.dellCCashBack(id,cid, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        #endregion


        #region [订单跟踪]
        public static DataTable GetOrderPstate(string OrderID) 
        {
            return SalesPro.GetOrderPstate(OrderID);
        }

        public static DataTable GetOrderPstateConfig(string Pstate) 
        {
            return SalesPro.GetOrderPstateConfig(Pstate);
        }
        #endregion

    }
}
