using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TECOCITY_BGOI
{
    public class PpPro
    {

        #region [例子]
        #endregion
        #region[公共方法]

        public static DataTable GetTypeID(string where)
        {
            //string str = "select id,Text From BGOI_PP.dbo.tk_ConfigState where type='" + where + "'";
            string str = "select id,Text From BGOI_PP.dbo.tk_ConfigState where 1=1 and Validate='v' " + where + " ";
            DataTable dt = SQLBase.FillTable(str, "MainPP");
            return dt;
        }
        public static UIDataTable SelectRZ(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.pp_Log where " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "id ";
            String strTable = " BGOI_PP.dbo.pp_Log a ";

            String strField = "a.*";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="LogTitle">操作</param>
        /// <param name="LogConTent">受否成功 成功or失败</param>
        /// <param name="LogTime">操作时间</param>
        /// <param name="LogPerson">操作人</param>
        /// <param name="Type">操作类型</param>
        public static int AddRZ(string id, string LogTitle, string LogConTent, string LogTime, string LogPerson, string Type)
        {
            PP_pp_Log log = new PP_pp_Log();
            log.RelevanceID = id;
            log.LogTitle = LogTitle;
            log.LogContent = LogConTent;
            log.LogTime = LogTime;
            log.LogPerson = LogPerson;
            log.Type = Type;
            string strRZ = GSqlSentence.GetInsertInfoByD<PP_pp_Log>(log, "[BGOI_PP].[dbo].pp_Log");
            int a = SQLBase.ExecuteNonQuery(strRZ);
            return a;
        }


        public static DataTable GetBasicDetail(string PID)
        {
            string sql = " select a.PID,a.ProName,a.Spec,a.MaterialNum,a.Manufacturer,a.UnitPrice,a.Price2,a.Units,a.Remark,b.COMNameC From BGOI_Inventory.dbo.tk_ProductInfo as  a  ";
            sql += "   left join BGOI_BasMan .dbo.tk_supplierbas as b  on a.Manufacturer=b.SID  ";
            sql += "where " + PID + "";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }


        public static string getCPlanTime()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CG' and TimeType = 'TimeCG'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }

        public static DataTable GetDataTime(string table, string lie, string type)
        {
            string sql = "select Convert(varchar(10), " + lie + ",23 ) as " + lie + "  from " + table + " where " + lie + " = (select " + type + "(" + lie + ") from " + table + ")";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable GetSuppliers()
        {
            string sql = "   select  Supplier  from BGOI_PP.dbo.PaymentGoods group by Supplier";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        #endregion


        public static UIDataTable GetSalesRetailList(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY OrderID desc) AS RowNumber,* from "
                       + "(select distinct a.OrderID,"
                       + "OrderContent = (stuff((select ',' + OrderContent from Orders_DetailInfo where OrderID = a.OrderID for xml path('')),1,1,'')),"
                       + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=a.ProvidManager),CONVERT(varchar(100), a.ContractDate, 23) as ContractDate,"
                       + "DTotalPrice = (stuff((select ',' + CONVERT(varchar(50),DTotalPrice) from Orders_DetailInfo where OrderID = a.OrderID for xml path('')),1,1,'')),"
                       + "ProductID = (stuff((select ',' + ProductID from Orders_DetailInfo where OrderID = a.OrderID for xml path('')),1,1,'')),"
                       + "a.Remark from OrdersInfo a,Orders_DetailInfo b "
                       + "where a.SalesType='Sa03' and a.Validate='v' and a.OrderID=b.OrderID " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
                       + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            sql += "select count(distinct a.OrderID) from OrdersInfo a,Orders_DetailInfo b where a.SalesType='Sa03' and a.Validate='v' and a.OrderID=b.OrderID " + strWhere + " ";
            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                //return instData;
            }
            DataTable dt = ds.Tables[0];
            instData.IntRecords = GFun.SafeToInt32(ds.Tables[1].Rows[0][0]);
            if (instData.IntRecords > 0)
            {
                if (instData.IntRecords % a_intPageSize == 0)
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize;
                else
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            instData.DtData = dt;
            return instData;
        }

        public static UIDataTable SelectPurchaseGoodsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.PurchaseGoods a " +
                                     " left join BGOI_PP.dbo.PurchaseRequisition b on a.CID=b.CID  " +
                                     "left join BGOI_PP.dbo.tk_ConfigState c on c.id=b.[State] where " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.purchaseDate ";
            String strTable = " BGOI_PP.dbo.PurchaseGoods a " +
                                      "  left join BGOI_PP.dbo.PurchaseRequisition b on a.CID=b.CID " +
                                      "left join BGOI_PP.dbo.tk_ConfigState c on c.id=b.[State] ";
            String strField = "a.CID,a.ordercontent,Convert(varchar(12),a.purchaseDate,111) as purchaseDate,Convert(varchar(12),b.pleasedate,111) as pleasedate,b.ExpectedTotal,c.Text as State,b.orderunit";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static DataTable GetStateID()
        {
            string str = "select state From BGOI_PP.dbo.PurchaseRequisition group by state";
            DataTable dt = SQLBase.FillTable(str, "MainPP");
            return dt;
        }




        public static DataTable GetUserId(string where)
        {
            string str = "select UserName,UserId  from BJOI_UM.dbo.UM_UserNew where 1=1 " + where + "";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            return dt;
        }

        public static DataTable SelectQGid(string CID)
        {
            string str = "Select a.ordercontent,a.specifications,a.unit,a.amount,a.unitpricenotax,a.totalnotax,a.[GoodsUse],a.remark,a.purchaseDate,b.pleasedate,b.pleaseexplain,b.ordercontacts,b.CID from dbo.PurchaseRequisition as b left join PurchaseGoods as a on a.CID=b.CID where 1=1 and a. CID='" + CID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainPP");
            return dt;
        }

        #region[选择任务单号]
        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            //string sql = "  select a.ContractID,a.OrderID,a.OrderContactor ,a.OrderTel,a.OrderAddress,b.StateDesc from BGOI_Sales.dbo.OrdersInfo  as a ";
            //sql += "  left join BGOI_Sales.dbo.ProjectState_Config  as b ";
            //sql += "  on a.OState=b.StateId ";
            //sql += "  where StateType='OrderState' and b.StateId !='3' ";
            //DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");





            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Sales.dbo.OrdersInfo  as a " +
                                     " left join BGOI_Sales.dbo.ProjectState_Config  as b   on a.OState=b.StateId  " +
                                     " where StateType='OrderState' and b.StateId !='3' and a.UnitID='47' ";

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "";
            string strOrderBy = "a.OrderID ";
            String strTable = " BGOI_Sales.dbo.OrdersInfo  as a " +
                                      "  left join BGOI_Sales.dbo.ProjectState_Config  as b   on a.OState=b.StateId " +
                                      " where StateType='OrderState' and b.StateId !='3' and a.UnitID='47' ";
            String strField = "a.ContractID,a.OrderID,a.OrderContactor ,a.OrderTel,a.OrderAddress,b.StateDesc ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }


        public static UIDataTable GetOrderInfoGoods(string where, int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            //string sql = "  select a.ContractID,a.OrderID,a.OrderContactor ,a.OrderTel,a.OrderAddress,b.StateDesc from BGOI_Sales.dbo.OrdersInfo  as a ";
            //sql += "  left join BGOI_Sales.dbo.ProjectState_Config  as b ";
            //sql += "  on a.OState=b.StateId ";
            //sql += "  where StateType='OrderState' and b.StateId !='3' ";
            //DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");





            string strSelCount = "select COUNT(*) " +
                                   " from  BGOI_Sales.dbo.Orders_DetailInfo " +

                                     "where  " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " deliverytime ";
            String strTable = "BGOI_Sales.dbo.Orders_DetailInfo";

            String strField = " OrderID,Productid ,ordercontent ,SpecsModels ,orderunit ,manufacturer,ActualAmount,unitprice ,technology ,deliverytime,actualsubtotal ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        #endregion


        #region [订购]
        /// <summary>
        /// 自动生成DDID号
        /// </summary>
        /// <returns></returns>
        public static string GetTopListDDID()
        {
            string strID = "";
            string strD = "DD-" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select MAX(DDID) from BGOI_PP.dbo.PurchaseOrder where OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'";
            string str = GAccount.GetAccountInfo().UnitID;
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }
                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }
                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }
                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }
        /// <summary>
        /// 添加采购订单
        /// </summary>
        /// <param name="inq"></param>
        /// <returns></returns>
        public static bool InsertPurchaseOrder(PP_PurchaseOrder order)
        {
            bool ok = false;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            string str = "insert into BGOI_PP.dbo.PurchaseOrder(DDID,CID,OrderNumber,OrderUnit,OrderContacts,Approver1,Approver2,ArrivalStatus,PayStatus,State,BusinessTypes,PleaseExplain,OrderDate,DeliveryLimit,DeliveryMethod,IsInvoice,PaymentMethod,PaymentAgreement,ContractNO,TheProject,TotalTax,TotalNoTax) values('" + order.DDID + "','" + order.CID + "','" + order.OrderNumber + "','" + order.OrderUnit + "','" + order.OrderContacts + "','" + order.Approver1 + "','" + order.Approver2 + "','" + order.ArrivalStatus + "','" + order.PayStatus + "','" + order.State + "','" + order.BusinessTypes + "','" + order.PleaseExplain + "','" + order.OrderDate + "','" + order.DeliveryLimit + "','" + order.DeliveryMethod + "','" + order.IsInvoice + "','" + order.PaymentMethod + "','" + order.PaymentAgreement + "','" + order.ContractNO + "','" + order.TheProject + "','" + order.TotalTax + "','" + order.TotalNoTax + "')";
            int num = SQLBase.ExecuteNonQuery(str);
            if (num > 0)
                ok = true;
            return ok;

        }


        public static bool LJSplitsInsert(List<PP_OrderGoods> order)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");
            string Insertsql = "";
            string Updatesql = "";
            foreach (var item in order)
            {
                string sqls = " select * from  OrderGoods where DDID='" + item.DDID + "' and LJCPID='" + item.LJCPID + "' and MaterialNO='" + item.MaterialNO + "' and Specifications='" + item.Specifications + "'";
                DataTable dtsqls = SQLBase.FillTable(sqls, "MainPP");
                if (dtsqls.Rows.Count > 0)
                {
                    Updatesql += " Update OrderGoods set Amount='" + item.Amount + "'where DDID='" + item.DDID + "' and LJCPID='" + item.LJCPID + "' and MaterialNO='" + item.MaterialNO + "' and Specifications='" + item.Specifications + "'  ";
                }
                else
                {
                    Insertsql += " insert into OrderGoods ( DID, DDID, LJCPID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, CreateTime, CreateUser, Validate,SJFK,RKState)  values('" + item.DID + "','" + item.DDID + "','" + item.LJCPID + "','" + item.MaterialNO + "','" + item.OrderContent + "','" + item.Specifications + "','" + item.Supplier + "','" + item.Unit + "','" + item.Amount + "','0','" + item.UnitPriceNoTax + "','" + item.TotalNoTax + "','" + item.UnitPrice + "','" + item.Total + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','1','0','0') ";
                }

            }
            try
            {
                if (Updatesql != "")
                    sqlTrans.ExecuteNonQuery(Updatesql, CommandType.Text, null);

                if (Insertsql != "")
                    sqlTrans.ExecuteNonQuery(Insertsql, CommandType.Text, null);
                sqlTrans.Close(true);
                return true;
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                return false;
            }

        }


        public static DataTable GetSupplierID(string where)
        {
            //string str = "select id,Text From BGOI_PP.dbo.tk_ConfigState where type='" + where + "'";
            string str = " select Supplier,COMNameC from BGOI_PP.dbo.OrderGoods a " +
                "  left join BGOI_BasMan .dbo.tk_supplierbas  b on a.Supplier=b.SID  " +
                " where  " + where + " group by a.Supplier , b.COMNameC ";
            DataTable dt = SQLBase.FillTable(str, "MainPP");
            return dt;
        }
        public static DataTable SelectDDXQ(string where)
        {
            string sql = "select a.*,b.* from BGOI_PP.dbo.PurchaseOrder as a , BGOI_PP.dbo.OrderGoods as b where a.DDID=b.DDID  and a.DDID='" + where + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectDD(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.PurchaseOrder a  " +
                                   "   left join BJOI_UM.dbo.UM_UnitNew Q on a.OrderUnit=Q.DeptId " +
                                   "   left join (select SUM(ActualAmount) as ActualAmount ,SUM(Amount) as Amount ,SUM(SJFK) as SJFK,DDID from BGOI_PP.dbo.OrderGoods  group by DDID ) as b on a.DDID=b.DDID  " +
                                  " left join (select * from BGOI_PP.dbo.tk_ConfigState) as z on a.State = z.id and z.Type = '提交审批状态' where " + where;



            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.OrderDate desc ";
            String strTable = "  BGOI_PP. dbo.PurchaseOrder a  " +
                    "   left join BJOI_UM.dbo.UM_UnitNew Q on a.OrderUnit=Q.DeptId " +
                    "  left join (select SUM(ActualAmount) as ActualAmount ,SUM(Amount) as Amount ,SUM(SJFK) as SJFK,DDID from BGOI_PP.dbo.OrderGoods  group by DDID ) as b on a.DDID=b.DDID  " +
" left join (select * from BGOI_PP.dbo.tk_ConfigState) as z on a.State = z.id and z.Type = '审批状态' ";
            String strField = " z.Text as sp,a.DDID,a.CID,a.PID,a.DDState,a.OrderNumber ,a.OrderUnit ,a.OrderContacts ,a.Approver1 ,a.Approver2 ,a.ArrivalStatus ,a.PayStatus,a.State,a.BusinessTypes ,a.PleaseExplain ,Convert(varchar(10),a.OrderDate,23) as OrderDate ,Convert(varchar(10),a.DeliveryLimit,23) as DeliveryLimit  ,a.DeliveryMethod ,a.IsInvoice ,a.PaymentMethod ,a.PaymentAgreement ,a.ContractNO ,a.TheProject ,a.TotalTax ,a.TotalNoTax ,a.CreateTime ,a.CreateUser ,a.Validate ,q.DeptName,case b.ActualAmount-b.Amount when '0' then '收货完成' else '收获未完成' end as SH,case b.SJFK-b.Amount when '0' then '付款完成' else '付款未完成' end as FK ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectDDGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*)  from BGOI_PP. dbo.OrderGoods a  where " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.DDID ";
            String strTable = "  BGOI_PP. dbo.OrderGoods a  ";

            String strField = "a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static UIDataTable GetByOrderID(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("Order", CommandType.StoredProcedure, sqlPar, "MainPP");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            DataTable dtOrder = DO_Order.Tables[0];
            instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[1].Rows[0][0]);
            if (instData.IntRecords > 0)
            {
                if (instData.IntRecords % a_intPageSize == 0)
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize;
                else
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            instData.DtData = dtOrder;
            return instData;
        }
        //public static bool InsertOrder(PP_PurchaseOrder record, string DID, List<PP_OrderGoods> delist, ref string strErr)
        //{
        //    //int resultCount = 0;
        //    int count = 0;
        //    SQLTrans trans = new SQLTrans();
        //    trans.Open("MainPP");
        //    try
        //    {

        //        string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
        //        if (strInsert != "")
        //        {
        //            count = SQLBase.ExecuteNonQuery(strInsert);
        //        }
        //        trans.Close(true);

        //        string strInsertList = "";
        //        if (delist.Count > 0)
        //        {
        //            foreach (PP_OrderGoods SID in delist)
        //            {
        //                strInsertList = "Insert into [BGOI_PP].[dbo].OrderGoods(DDID,DID,MaterialNO,OrderContent,Specifications,Unit,Amount,ActualAmount,Supplier,UnitPriceNoTax,TotalNoTax,[GoodsUse],CreateTime,CreateUser,Validate,DrawingNum,RKState,SJFK) " +
        //                    "values ('" + SID.DDID + "','" + SID.DID + "','" + SID.MaterialNO + "','" + SID.OrderContent + "','" + SID.Specifications + "','" + SID.Unit + "'," + SID.Amount + ",'" + SID.ActualAmount + "','" + SID.Supplier + "'," + SID.UnitPriceNoTax + ",'" + SID.TotalNoTax + "','" + SID.GoodsUse + "','" + SID.CreateTime + "','" + SID.CreateUser + "','" + SID.Validate + "','" + SID.DrawingNum + "','" + SID.RKState + "','" + SID.SJFK + "')";

        //                if (strInsertList != "")
        //                {
        //                    SQLBase.ExecuteNonQuery(strInsertList);
        //                }
        //            }
        //        }


        //        if (count > 0)
        //        {
        //            string sql = "";
        //            if (record.CID != "无")
        //            {
        //                string[] list = record.CID.Split('-');
        //                if (list[0] == "QG")
        //                {
        //                    sql = "update [BGOI_PP].[dbo].PurchaseRequisition set state='2' where CID='" + record.CID + "'";
        //                    int a = SQLBase.ExecuteNonQuery(sql);

        //                }
        //                if (list[0] == "XJ")
        //                {
        //                    sql = "update BGOI_PP.dbo.Inquirys set XJState='2' where XJID='" + record.CID + "'";
        //                    int a = SQLBase.ExecuteNonQuery(sql);


        //                }
        //            }

        //            return true;

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        //AddRZ(record.DDID, "增加订购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");


        //    }
        //    catch (Exception ex)
        //    {

        //        strErr = ex.Message;
        //        trans.Close(true);
        //        return false;
        //    }
        //}

        public static bool InsertOrder(PP_PurchaseOrder record, List<PP_OrderGoods> delist, List<PP_ChoseGoods> cplist, ref string strErr)
        {


            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }

                string strInsertListcp = GSqlSentence.GetInsertByList(cplist, "[BGOI_PP].[dbo].ChoseGoods");
                if (strInsertListcp != "")
                {
                    trans.ExecuteNonQuery(strInsertListcp, CommandType.Text, null);
                }

                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool SavePlanData(string strData, PP_PurchaseOrder good, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            strErr = "";



            int resultcount = 0;
            try
            {

                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(good, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                int n = 0;
                int count = 0;
                string strSql = "";
                string[] strList = strData.Split('!');// 完整的数据
                if (resultcount < 100000)
                {
                    for (int i = 0; i < strList.Length; i++)
                    {

                        n++;
                        //DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, CreateTime, CreateUser, Validate
                        string[] strList1 = strList[i].Split(',');
                        string ddid = good.DDID + "-" + n;
                        string[] Supplier = strList1[10].Split(':');
                        string[] GoodsNum = strList1[11].Split(':');
                        string[] Goodsyiju = strList1[12].Split(':');
                        string[] GoodsName = strList1[13].Split(':');
                        string[] GoodsSpec = strList1[14].Split(':');
                        strSql = " insert into BGOI_PP.dbo.OrderGoods" +
                          "(DID, DDID, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, RKState, Remark, SJFK, DrawingNum,GoodsNum,Goodsyiju,GoodsName, CreateTime, CreateUser, Validate)" +
                          " values('" + ddid + "','" + good.DDID + "','" + strList1[1] + "','" + GoodsSpec[1] + "','" + Supplier[1] + "','" + strList1[3] + "'," + strList1[4] + ",0," + strList1[5] + "," + strList1[6] + "," + strList1[7] + "," + strList1[8] + ",0,'" + strList1[9] + "',0,'" + strList1[2] + "','" + GoodsNum[1] + "','" + Goodsyiju[1] + "','" + GoodsName[1] + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "',1)";
                        if (strSql != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strSql);
                        }
                    }

                    if (count > 0)
                        return true;
                    else
                    {
                        strErr = "数据保存失败";
                        return false;
                    }
                }
                else
                {
                    strErr = "货品数据，请重新上传";
                    return false;
                }

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                throw;
            }
        }
        public static DataTable SelectGoodsDDID(string where)
        {
            string sql = " select a.OrderContacts,a.DDID, Convert(varchar(10), a.OrderDate,23) as OrderDate,a.PID,a.OrderContacts,a.BusinessTypes,a.DeliveryLimit,a.DeliveryMethod,";
            sql += " a.IsInvoice,a.PaymentMethod,a.PaymentAgreement,a.ContractNO,a.TheProject,b.* ,a.State  ,e.Text as JHFS,f.Text as FP,g.Text as ZFFS,h.Text as FKYD,i.Text as BusinessTypess,j.COMNameC ";
            sql += " from  BGOI_PP.dbo.PurchaseOrder as a ";
            sql += " left join BGOI_PP.dbo.OrderGoods as b on a.DDID=b.DDID ";
            //sql += " left join  BJOI_UM.dbo.UM_UserNew as d on a.OrderContacts=d.UserId ";
            sql += " left join BGOI_PP.dbo.tk_ConfigState as e on a.DeliveryMethod = e.id and e.Type = '交货方式' ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as f on a.IsInvoice = f.id and f.Type = '是否开发票' ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as g on a.PaymentMethod = g.id and g.Type = '支付方式' ";
            sql += "   left join (select * from BGOI_PP.dbo.tk_ConfigState) as i on a.BusinessTypes=i.id and i.Type='业务类型' ";
            sql += "  left join BGOI_BasMan .dbo.tk_supplierbas as j on b.Supplier=j.SID ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as h on a.PaymentAgreement = h.id and h.Type = '付款约定' where  a.Validate='1' and " + where;

            sql += " order by b.Supplier desc ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable selectjob(string where)
        {
            string sql = " select a.Job,a.RelevanceID ,b.UserName,a.ApprovalTime from BGOI_PP..Approvel a left join BJOI_UM..UM_UserNew b on a.ApprovalPersons = b.UserId where " + where + "";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }


        public static DataTable SelectGoodsDC(string where)
        {
            string sql = " select b.OrderContent,b.DrawingNum,b.Unit,b.Amount,b.UnitPriceNoTax,b.TotalNoTax,b.UnitPrice,b.Total,g.Text,b.Remark";
            sql += " from  BGOI_PP.dbo.PurchaseOrder as a ";
            sql += " left join BGOI_PP.dbo.OrderGoods as b on a.DDID=b.DDID ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as g on a.PaymentMethod = g.id and g.Type = '支付方式' where ";

            sql += where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectGoodsDCs(string where)
        {
            string sql = "   select top(1) b.Supplier,b.GoodsNum,b.Goodsyiju,b.GoodsName, b.Specifications   from  BGOI_PP.dbo.PurchaseOrder as a";

            sql += " left join BGOI_PP.dbo.OrderGoods as b on a.DDID=b.DDID where  ";

            sql += where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectGoodsDDID1(string where)
        {

            string sql = " select  j.COMNameC,SUM(b.TotalNoTax) AS TotalNoTax ,sum( b.UnitPriceNoTax * a.ActualAmount) - SUM(b.SJFK * b.UnitPriceNoTax ) as NoFK,SUM(b.SJFK * b.UnitPriceNoTax) as SJFK  ,sum ( b.UnitPriceNoTax * a.ActualAmount) as RKjine from BGOI_PP.dbo.PurchaseOrder as c   left join BGOI_PP.dbo.OrderGoods as b  on b.DDID= c.DDID  left join  BGOI_PP.dbo.StorageDetailed  as a on b.DID = a.Bak    left join BGOI_BasMan .dbo.tk_supplierbas as j on b.Supplier=j.SID	where   " + where + " and c.Validate='1' and b.Validate='1' and a.Validate='1' or   " + where + " and c.Validate='1' and b.Validate='1' group by j.COMNameC ";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectCountDD()
        {
            string sql = " select Count(*) as Count from BGOI_PP.dbo.PurchaseOrder where Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectDDCID(string where)
        {
            string sql = "select * from BGOI_PP.dbo.PurchaseOrder where " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateDD(PP_PurchaseOrder record, List<PP_OrderGoods> delist, List<PP_ChoseGoods> cplist, ref string strErr)
        {
            string InserPurchaseOrder_HIS = "insert into BGOI_PP.dbo.PurchaseOrder_HIS (DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, CreateTime, CreateUser, Validate, NCreateUser, NCreateTime)" +
              "select DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, CreateTime, CreateUser, Validate, '" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.PurchaseOrder where DDID ='" + record.DDID + "'";
            string InserOrderGoods_HIS = "insert into BGOI_PP.dbo.OrderGoods_HIS (DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate, NCreateUser,NCreateTime )" +
             "select DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.OrderGoods where DDID ='" + record.DDID + "'";

            string InserChoseGoods_HIS = "insert into BGOI_PP.dbo.ChoseGoods_HIS (DDID, PID, Name, Spc, Num, Units, CreateTime, CreateUser, Validate,NCreateUser  ,NCreateTime)" +
            "select DDID, PID, Name, Spc, Num, Units, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.ChoseGoods where DDID ='" + record.DDID + "'";
            SQLBase.ExecuteNonQuery(InserPurchaseOrder_HIS);
            SQLBase.ExecuteNonQuery(InserOrderGoods_HIS);
            SQLBase.ExecuteNonQuery(InserChoseGoods_HIS);
            string deletecpsql = "delete  from BGOI_PP.dbo.ChoseGoods where DDID='" + record.DDID + "' ";
            string deleteljsql = "delete  from BGOI_PP.dbo.OrderGoods where DDID='" + record.DDID + "' ";
            string deleteddsql = "delete  from BGOI_PP.dbo.PurchaseOrder where DDID='" + record.DDID + "' ";
            SQLBase.ExecuteNonQuery(deletecpsql);
            SQLBase.ExecuteNonQuery(deleteljsql);
            SQLBase.ExecuteNonQuery(deleteddsql);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }

                string strInsertListcp = GSqlSentence.GetInsertByList(cplist, "[BGOI_PP].[dbo].ChoseGoods");
                if (strInsertListcp != "")
                {
                    trans.ExecuteNonQuery(strInsertListcp, CommandType.Text, null);
                }

                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static int UpdateDDValidate(string where)
        {
            string sql = "update BGOI_PP.dbo.PurchaseOrder set Validate='-1' where DDID='" + where + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                //AddRZ(where, "撤销订单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
            }
            else
            {
                //AddRZ(where, "撤销订单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
            }
            return count;
        }

        public static int UpdateSHState(string where)
        {
            string sql = "update BGOI_PP.dbo.OrderGoods set SHState='1' where DID='" + where + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            return count;
        }

        public static DataTable SelectKC()
        {
            string sql = "select distinct ProName from  BGOI_Inventory.dbo.tk_ProductInfo where PID in (" +
                " select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in ( " +
                " select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "')) and ProTypeID='2' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable Selectkoujing(string where)
        {
            string sql = "select * from  BGOI_Inventory.dbo.tk_ProductInfo where PID in (" +
           " select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in ( " +
           " select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "')) and ProTypeID='2' and ProName='" + where + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectLingJ(string where)
        {
            string sql = " select * from BGOI_Inventory.dbo.tk_ProFinishDefine where ProductID='" + where + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectLingJXQ(string where)
        {
            string sql = "  select distinct  a.PID,a.ProName,a.Spec,a.UnitPrice,a.Price2,a.Units,a.Manufacturer,b.COMNameC,c.Number,c.ProductID from BGOI_Inventory.dbo.tk_ProductInfo as a  " +
                " left join BGOI_BasMan .dbo.tk_supplierbas as b  on a.Manufacturer=b.SID  " +
                    " left join BGOI_Inventory.dbo.tk_ProFinishDefine as c on a.PID=c.PartPID  " +
                        "where " + where + "";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectCP(string where)
        {
            string sql = "  select * from BGOI_PP.dbo.ChoseGoods  " +
                        "where " + where + "";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.ChoseGoods a where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "DDID desc ";
            string strTable = "  BGOI_PP. dbo.ChoseGoods a  ";

            string strField = " a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectDDSupplier(string where)
        {
            string sql = "    select distinct b.*,a.Name,isnull(c.Number,0) as Number,d.COMNameC from  BGOI_PP.dbo.OrderGoods b     ";
            sql += "      left join BGOI_PP.dbo.ChoseGoods a on a.PID=b.LJCPID   ";
            sql += "      left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.LJCPID=c.ProductID and b.MaterialNO=c.PartPID ";
            sql += "   left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID   where " + where + "  order by Number asc";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectDDCP(string where)
        {
            string sql = "  select  * from  BGOI_PP.dbo.ChoseGoods as a  " +
                        "where " + where + "";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectDDTJ(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_PP.dbo.PurchaseOrder as a  " +
                " left join (select SUM(ActualAmount) as ActualAmount ,SUM(Amount) as Amount ,DDID from BGOI_PP.dbo.OrderGoods  group by DDID ) as b on a.DDID=b.DDID where " +
                 where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.DDID ";
            String strTable = "  BGOI_PP. dbo.PurchaseOrder as a  " +
                " left join (select SUM(ActualAmount) as ActualAmount ,SUM(Amount) as Amount ,DDID from BGOI_PP.dbo.OrderGoods  group by DDID ) as b on a.DDID=b.DDID ";

            String strField = "  a.DDState,a.OrderNumber ,a.OrderUnit ,a.OrderContacts ,a.PayStatus,a.State,a.BusinessTypes ,Convert(varchar(10),a.OrderDate,23) as OrderDate ,Convert(varchar(10),a.DeliveryLimit,23) as DeliveryLimit  ,a.ContractNO ,a.TheProject ,a.TotalTax ,a.TotalNoTax ,a.Validate, b.*, DATEDIFF(day,a.OrderDate,GETDATE())as num  ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectSplitLJ(string where)
        {
            string sql = " select l.OrderContent,l.LJCPID,sum2,l.Name,l.spec from (" +
                "  select distinct a.OrderContent,a.LJCPID,sum(a.Amount) as sum2,c.Name, b.spec from BGOI_PP.dbo.OrderGoods a  " +
                "  left join (select distinct proName,COUNT(Spec) spec from BGOI_Inventory.dbo.tk_ProductInfo group by ProName ) b on a.OrderContent=b.ProName " +
                "  left join (select distinct Name,PID from BGOI_PP.dbo.ChoseGoods) as c on a.LJCPID =c.PID  " +
                "  where " + where + " group by a.OrderContent,a.LJCPID,b.spec,c.Name " +
                "   ) l where l.spec>1  ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectSplitLJxq(string name, string cpid)
        {
            string sqls = " select * from  OrderGoods  where LJCPID='" + cpid + "' and OrderContent='" + name + "' ";
            DataTable dts = SQLBase.FillTable(sqls, "MainPP");

            string sql = "  select * from BGOI_Inventory.dbo.tk_ProductInfo  a   " +
                " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  and b.UnitID='47' " +
                        "where a.ProTypeID='1' and  a.ProName='" + name + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
            //and PID !='" + dts.Rows[0]["MaterialNO"] + "'
        }
        #endregion

        #region [询价]
        public static bool insertinquiry(PP_Inquirys inq)
        {
            bool ok = false;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            string str = "insert into BGOI_PP.dbo.Inquirys(XJID,CID,OrderNumber,OrderUnit,InquiryTitle,OrderContacts,Approver1,Approver2,State,BusinessTypes,InquiryExplain,InquiryDate,DeliveryLimit,DeliveryMethod,IsInvoice,PaymentMethod,PaymentAgreement,TotalTax,TotalNoTax) values('" + inq.XJID + "','" + inq.CID + "','" + inq.OrderNumber + "','" + inq.OrderUnit + "','" + inq.InquiryTitle + "','" + inq.OrderContacts + "','" + inq.Approver1 + "','" + inq.Approver2 + "','" + inq.State + "','" + inq.BusinessTypes + "','" + inq.InquiryExplain + "','" + inq.InquiryDate + "','" + inq.DeliveryLimit + "','" + inq.DeliveryMethod + "','" + inq.IsInvoice + "','" + inq.PaymentMethod + "','" + inq.PaymentAgreement + "','" + inq.TotalTax + "','" + inq.TotalNoTax + "')";
            int num = SQLBase.ExecuteNonQuery(str);



            if (num > 0)
                ok = true;
            return ok;

        }

        public static UIDataTable SelectXJ(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.Inquirys a " +
                                     " left join BGOI_PP.dbo.InquiryGoods b on a.XJID=b.XJID  " +
                                     "left join BGOI_PP.dbo.InquiryCondition c on c.XJID=b.XJID where " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.XJID ";
            String strTable = " BGOI_PP.dbo.Inquirys a " +
                                      "  left join BGOI_PP.dbo.InquiryGoods b on a.XJID=b.XJID " +
                                      "left join BGOI_PP.dbo.InquiryCondition c on c.XJID=b.XJID  ";
            String strField = "a.XJState,a.InquiryTitle,a.InquiryDate,a.OrderContacts, b.*  ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectInquiry(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.Inquirys a " +
                                   " left join BGOI_PP.dbo.tk_ConfigState b on b.id=a.XJState " +
                                      " left join BJOI_UM.dbo.UM_UnitNew q on a.OrderUnit=q.DeptId " +
                                   " left join (select * from BGOI_PP.dbo.tk_ConfigState) as z on a.State = z.id and z.Type = '提交审批状态' " +
                                     "left join BGOI_PP.dbo.InquiryCondition c on c.XJID=a.XJID where " + where + "and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'or " + where + " and  a.OrderUnit='32' or " + where + " and  a.OrderUnit='46'";

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where + "and a.OrderUnit='" + GAccount.GetAccountInfo().UnitID + "'or " + where + " and  a.OrderUnit='32' or " + where + " and  a.OrderUnit='46'";
            string strOrderBy = "a.InquiryDate desc ";
            String strTable = " BGOI_PP.dbo.Inquirys a " +
                                      "left join BGOI_PP.dbo.InquiryCondition c on c.XJID=a.XJID  " +
                                         " left join BJOI_UM.dbo.UM_UnitNew q on a.OrderUnit=q.DeptId " +
             " left join BGOI_PP.dbo.tk_ConfigState b on b.id=a.XJState " +
            " left join (select * from BGOI_PP.dbo.tk_ConfigState) as z on a.State = z.id and z.Type = '提交审批状态' ";

            String strField = "z.Text as sp,a.XJID,a.InquiryTitle,Convert(varchar(10),a.InquiryDate,23) as InquiryDate ,a.OrderContacts ,a.State,b.text,q.DeptName";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static bool InsertXJ(List<PP_InquiryGoods> record, PP_InquiryCondition con, PP_Inquirys inq, ref string strErr)
        {
            //int resultCount = 0;
            PP_pp_Log log = new PP_pp_Log();
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                foreach (var item in record)
                {
                    string str = "Insert into [BGOI_PP].[dbo].InquiryGoods(XJID,DrawingNum,XID,[GoodsUse],XXID,OrderContent,Specifications,Supplier,Unit,Amount,NegotiatedPricingNoTax,TotalNegotiationNoTax,UnitPriceTax,TotalTax,Remark,CreateTime,CreateUser,Validate) select '" + item.XJID + "','" + item.DrawingNum + " ','" + item.XID + " ','" + item.GoodsUse + "','" + item.XXID + "','" + item.OrderContent + "','" + item.Specifications + " ','" + item.Supplier + " ','" + item.Unit + "'," + item.Amount + "," + item.NegotiatedPricingNoTax + "," + item.TotalNegotiationNoTax + "," + item.UnitPriceTax + ",'" + item.TotalTax + "','" + item.Remark + "','" + item.CreateTime + " ','" + item.CreateUser + "','" + item.Validate + "'";
                    SQLBase.ExecuteNonQuery(str);
                }


                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_Inquirys>(inq, "[BGOI_PP].[dbo].Inquirys");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }

                string sql = GSqlSentence.GetInsertInfoByD<PP_InquiryCondition>(con, "[BGOI_PP].[dbo].InquiryCondition");
                if (sql != "")
                {
                    count += SQLBase.ExecuteNonQuery(sql);
                }
                if (count > 0)
                {
                    if (inq.CID != "无")
                    {
                        string sqls = "update [BGOI_PP].[dbo].PurchaseRequisition set state='1' where CID='" + inq.CID + "'";
                        int a = SQLBase.ExecuteNonQuery(sqls);
                        if (a > 0)
                        {
                            //AddRZ(inq.CID, "增加询价", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                        }

                    }
                    //AddRZ(inq.XJID, "增加询价", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                    return true;
                }
                else
                {
                    return false;
                }
                //    AddRZ(inq.XJID, "增加询价", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                //AddRZ(inq.CID, "增加询价", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static DataTable SelectGoodsXJID(string where)
        {
            string sql = " select d.UserName, a.XJID,a.ContractCondition,b.XXID,b.DrawingNum,b.OrderContent,b.Specifications,b.Supplier,b.Unit,b.Amount, " +
                " b.NegotiatedPricingNoTax,b.TotalNegotiationNoTax,b.Remark,b.GoodsUse,c.CID,c.OrderNumber,c.OrderUnit,c.InquiryTitle,c.OrderContacts,c.[State],CONVERT(varchar(10), c.InquiryDate , 23) as InquiryDate,c.InquiryExplain ,z.COMNameC,z.SID" +
                "   from BGOI_PP.dbo.InquiryCondition as a  " +

                " left join BGOI_PP.dbo.InquiryGoods as b on a.XJID=b.XJID " +
                "  left join BGOI_PP.dbo.Inquirys  as c on a.XJID=c.XJID " +
                " left join  BJOI_UM.dbo.UM_UserNew as d on c.OrderContacts=d.UserId  " +
                "   left join BGOI_BasMan .dbo.tk_supplierbas as z  on b.Supplier=z.SID   " +
                "  where c.Validate='1' and c.XJID='" + where + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectXJXQ(string where)
        {
            string sql = "select a.*,b.*, c.*  from BGOI_PP.dbo.InquiryCondition as a, BGOI_PP.dbo.InquiryGoods as b, BGOI_PP.dbo.Inquirys as c  where a.XJID=b.XJID AND b.XJID=c.XJID AND c.XJID='" + where + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;

        }
        public static string GetTopListXJID()
        {
            string strID = "";
            string strD = "XJ-" + DateTime.Now.ToString("yyMMdd");
            string str = GAccount.GetAccountInfo().UnitID;
            string strSqlID = "select MAX(XJID) from BGOI_PP.dbo.Inquirys where OrderUnit='" + str + "'";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }

        public static int UpdateXJValidate(string where)
        {
            string sql = "update BGOI_PP.dbo.Inquirys set Validate='-1' where XJID='" + where + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                //AddRZ(where, "撤销询价", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
            }
            else
            {
                //AddRZ(where, "撤销询价", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
            }
            return count;
        }

        public static DataTable SelectXJCID(string where)
        {
            string sql = "select * from BGOI_PP.dbo.Inquirys where " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateXJ(PP_Inquirys inq, PP_InquiryCondition tion, List<PP_InquiryGoods> list, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strUpdate = "update Inquirys set OrderContacts=@OrderContacts, InquiryTitle=@InquiryTitle,InquiryExplain=@InquiryExplain,InquiryDate=@InquiryDate  where XJID=@XJID";
                SqlParameter[] param ={
                                       new SqlParameter ("@InquiryTitle",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@InquiryExplain",SqlDbType .VarChar ),
                                       new SqlParameter ("@InquiryDate",SqlDbType .DateTime),
                                       new SqlParameter ("@XJID",SqlDbType .VarChar ),
                                        new SqlParameter ("@OrderContacts",SqlDbType .VarChar )
                                     };
                param[0].Value = inq.InquiryTitle;
                param[1].Value = inq.InquiryExplain;
                param[2].Value = inq.InquiryDate;
                param[3].Value = inq.XJID;
                param[4].Value = inq.OrderContacts;
                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.Inquirys_HIS (XJID, CID, OrderNumber, OrderUnit, InquiryTitle, OrderContacts, Approver1, Approver2, XJState, State, BusinessTypes, InquiryExplain, InquiryDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, TotalTax, TotalNoTax, CreateTime, CreateUser, Validate,NCreateTime,NCreateUser)" +
                "select a.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.Inquirys as a where XJID ='" + inq.XJID + "'";

                string UpdateInquiryCondition = "update InquiryCondition set ContractCondition= '" + tion.ContractCondition
                    + "' where XJID='" + inq.XJID + "'";
                string InserInquiryConditionHIS = "insert into BGOI_PP.dbo.InquiryCondition_HIS (XJID,XID,Supplier,ContractCondition,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)" +
               "select XJID,XID,Supplier,ContractCondition,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.InquiryCondition where XJID ='" + inq.XJID + "'";

                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.InquiryGoods_HIS (XJID, XID, XXID, OrderContent, Specifications, Supplier, Unit, Amount, NegotiatedPricingNoTax, TotalNegotiationNoTax, UnitPriceTax, TotalTax, Rate, GoodsUse, Remark, DrawingNum, CreateTime, CreateUser, Validate,NCreateTime,NCreateUser)" +
                          "select b.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                          " from  BGOI_PP.dbo.InquiryGoods as b where XJID='" + inq.XJID + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");



                    string sql = "delete from BGOI_PP.dbo.InquiryGoods";
                    SQLBase.ExecuteNonQuery(sql, "MainPP");

                    //strUpdateList = "update BGOI_PP.dbo.InquiryGoods set Amount='" + item.Amount + "',Supplier='" + item.Supplier + "',NegotiatedPricingNoTax='" + item.NegotiatedPricingNoTax + "', TotalNegotiationNoTax='" + item.TotalNegotiationNoTax + "',DrawingNum='" + item.DrawingNum + "',[GoodsUse]='" + item.GoodsUse + "',Remark='" + item.Remark + "' where XID='" + item.XID + "'";

                    //if (strUpdateList != "")
                    //{
                    //    SQLBase.ExecuteNonQuery(strUpdateList, "MainPP");
                    //    AddRZ(item.XJID, "修改询价单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                    //}

                    string strInsertList = GSqlSentence.GetInsertByList(list, "[BGOI_PP].[dbo].InquiryGoods");
                    if (strInsertList != "")
                    {
                        int a = trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                        trans.Close(true);
                        if (a > 0)
                        {
                            foreach (PP_InquiryGoods item in list)
                            {
                                //AddRZ(item.XJID, "修改询价单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "询价");
                            }

                        }

                    }

                }
                if (strUpdate != "")
                {

                    SQLBase.ExecuteNonQuery(InserInquiryConditionHIS, "MainPP");//
                    SQLBase.ExecuteNonQuery(UpdateInquiryCondition, "MainPP");

                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }

        #endregion

        #region [请购单]
        public static string GetPurchaseRequisitionQGID()
        {
            string strID = "";
            string strD = "QG-" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select MAX(CID) from BGOI_PP.dbo.PurchaseRequisition";
            string str = GAccount.GetAccountInfo().UnitID;
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }

        public static UIDataTable PurchaseGoodsList(int a_intPageSize, int a_intPageIndex, string CID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_PP].[dbo].[PurchaseGoods] where CID='" + CID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "CID='" + CID + "'";
            string strOrderBy = " CID ";
            String strTable = "[BGOI_PP].[dbo].[PurchaseGoods] ";
            String strField = "CID,OrderContent,Specifications,Unit,Amount,UnitPriceNoTax,TotalNoTax,GoodsUse,Remark ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            return instData;





        }
        public static UIDataTable SelectQG(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.PurchaseRequisition a " +
                                   "left join BGOI_PP.dbo.tk_ConfigState b on a.[State]=b.id  " +
                                       " left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.PleaseDate desc, a.State desc";
            String strTable = " BGOI_PP.dbo.PurchaseRequisition a  " +
                 "  left join BGOI_PP.dbo.tk_ConfigState b on a.[State]=b.id  " +
                   " left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId ";
            String strField = " a.State,a.CID,CONVERT(varchar(10), a.PleaseDate, 23) as PleaseDate,a.PleaseExplain,CONVERT(varchar(10), a.DeliveryDate, 23) as DeliveryDate,b.Text ,d.DeptName ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectGoodsQGID(string where)
        {
            //string sql = "select * from BGOI_PP.dbo.PurchaseGoods where CID='" + where + "'";
            string sql = "select  a.OrderUnit,a.OrderContacts,a.BusinessTypes,a.PleaseExplain,z.SID,z.COMNameC,Convert(varchar(10), a.PleaseDate,23) as PleaseDate,Convert(varchar(10), a.DeliveryDate ,23 ) as DeliveryDate,a.ExpectedTotal,b.*,c.[Text] from  BGOI_PP.dbo.PurchaseRequisition a";
            sql += "  left join  BGOI_PP.dbo.PurchaseGoods b on a.CID=b.CID left join BGOI_PP.dbo.tk_ConfigState c on c.id=a.[State]";
            sql += " left join BGOI_BasMan .dbo.tk_supplierbas as z  on b.Supplier=z.SID  ";
            sql += "    where c.[Type]='请购状态' and a.Validate='1' and b.CID  ='" + where + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectQGXQ(string where)
        {
            string sql = "select a.OrderUnit,a.OrderContacts,a.BusinessTypes,a.PleaseExplain,a.PleaseDate,a.DeliveryDate,a.ExpectedTotal,b.*,c.[Text] from  BGOI_PP.dbo.PurchaseRequisition a left join  BGOI_PP.dbo.PurchaseGoods b on a.CID=b.CID left join BGOI_PP.dbo.tk_ConfigState c on c.id=a.[State] where c.[Type]='请购状态' and a.Validate='1' and b.CID  ='" + where + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool InsertQG(List<PP_PurchaseGoods> record, PP_PurchaseRequisition delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(record, "[BGOI_PP].[dbo].PurchaseGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseRequisition>(delist, "[BGOI_PP].[dbo].PurchaseRequisition");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {
                    //AddRZ(delist.CID, "增加请购", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(delist.CID, "增加请购", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool UpdateQG(PP_PurchaseRequisition pp, List<PP_PurchaseGoods> list, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strUpdate = "update PurchaseRequisition set DeliveryDate=@DeliveryDate,PleaseExplain=@PleaseExplain,OrderContacts=@OrderContacts,PleaseDate=@PleaseDate  where CID=@CID";
                SqlParameter[] param ={new SqlParameter ("@DeliveryDate",SqlDbType .DateTime),
                                       new SqlParameter ("@PleaseExplain",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@OrderContacts",SqlDbType .VarChar ),
                                       new SqlParameter ("@PleaseDate",SqlDbType .DateTime ),
                                       new SqlParameter ("@CID",SqlDbType .VarChar )
                                     };
                param[0].Value = pp.DeliveryDate;
                param[1].Value = pp.PleaseExplain;
                param[2].Value = pp.OrderContacts;
                param[3].Value = pp.PleaseDate;
                param[4].Value = pp.CID;
                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.PurchaseRequisition_HIS (CID,OrderNumber,OrderUnit,OrderContacts,Approver1,Approver2,State,BusinessTypes,PleaseExplain,PleaseDate,DeliveryDate,ExpectedTotal,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)" +
                "select CID,OrderNumber,OrderUnit,OrderContacts,Approver1,Approver2,State,BusinessTypes,PleaseExplain,PleaseDate,DeliveryDate,ExpectedTotal,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.PurchaseRequisition where CID ='" + pp.CID + "'";
                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.PurchaseGoods_HIS (CID,DID,INID,OrderContent,Specifications,Unit,Supplier,Amount,UnitpriceNoTax,TotalNoTax,Rate,[GoodsUse],Remark,DrawingNum,PurchaseDate,CreateTime,CreateUser,Validate,NCreateUser,NCreateTime)" +
                          "select b.*,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                          " from  BGOI_PP.dbo.PurchaseGoods as b where CID='" + pp.CID + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");

                    string sql = "delete from BGOI_PP.dbo.PurchaseGoods ";
                    SQLBase.ExecuteNonQuery(sql, "MainPP");

                    string strInsertList = GSqlSentence.GetInsertByList(list, "[BGOI_PP].[dbo].PurchaseGoods");
                    if (strInsertList != "")
                    {
                        int a = trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                        trans.Close(true);
                        if (a > 0)
                        {
                            //AddRZ(list[0].CID, "修改请购单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                        }

                    }
                }
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }



        public static int UpdateQGValidate(string where)
        {
            string sql = "update BGOI_PP.dbo.PurchaseRequisition set Validate='-1' where CID='" + where + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                //AddRZ(where, "删除请购单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
            }
            else
            {
                //AddRZ(where, "删除请购单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
            }
            return count;
        }
        #endregion

        #region[收货]
        public static DataTable GetList(string where)
        {
            string sql = "select a.DDID,a.CID,a.DeliveryLimit,a.DeliveryMethod,a.IsInvoice,a.PaymentMethod,a.PaymentAgreement,a.ContractNO,a.TheProject,a.OrderContacts,b.OrderContent,b.Specifications,b.Unit,b.Amount,b.Supplier,b.UnitPriceNoTax,b.TotalNoTax,c.ArrivalDescription from BGOI_PP.dbo.PurchaseOrder as a left join BGOI_PP.dbo.OrderGoods as b on a.DDID=b.DDID left join BGOI_PP.dbo.ReceivingInformation as c on a.DDID=c.DDID  where a.DDID='" + where + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static string GetTopListSHID()
        {
            string strID = "";
            string strD = "SH-" + DateTime.Now.ToString("yyMMdd");
            string str = GAccount.GetAccountInfo().UnitID;
            string strSqlID = "select MAX(SHID) from BGOI_PP.dbo.ReceivingInformation where OrderUnit='" + str + "'";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }

        public static UIDataTable SelectSH(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.ReceivingInformation as a " +
                         " left join (select  SUM(ActualAmount) as ActualAmount , SUM(Amount) as Amount ,SHID from BGOI_PP.dbo.StorageDetailed group by SHID )  b on a.SHID=b.SHID " +
                                   "       left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId where" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.ArrivalDate desc ";
            String strTable = " BGOI_PP. dbo.ReceivingInformation a " +
                " left join (select  SUM(SHStates) as ActualAmount , SUM(Amount) as Amount ,SHID from BGOI_PP.dbo.StorageDetailed group by SHID )  b on a.SHID=b.SHID " +
                        "       left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId ";
            String strField = "a.DDID,a.XXID,a.SHID,a.ArrivalProcess,a.ArrivalDescription,Convert(varchar(10),a.ArrivalDate,23) as ArrivalDate  ,a.CreateUser,d.DeptName,case b.ActualAmount-b.Amount when '0' then '入库完成' else '入库未完成' end as RK";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }




        public static UIDataTable SelectSHGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.StorageDetailed as a" +
            "  left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier  where " + where;
            //" left join BGOI_PP.dbo.tk_ConfigState as b on a.SHStates=b.id where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_PP. dbo.StorageDetailed a " +
                "   left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier ";
            //" left join BGOI_PP.dbo.tk_ConfigState as b on a.SHStates=b.id ";
            String strField = " a.* ,d.COMNameC";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static bool InsertSH(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<string> str)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ReceivingInformation>(record, "[BGOI_PP].[dbo].ReceivingInformation");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                foreach (PP_StorageDetailed sh in delist)
                {
                    strInsertList = "insert into BGOI_PP.dbo.StorageDetailed (SHID,DID,INID,OrderContent,Specifications,Supplier,Unit,Amount,ActualAmount,Bak,UnitPriceNoTax,TotalNoTax,CreateTime,CreateUser,Validate,SHStates,THAmount) values('" + sh.SHID + "','" + sh.DID + "','" + sh.INID + "','" + sh.OrderContent + "','" + sh.Specifications + "','" + sh.Supplier + "','" + sh.Unit + "','" + sh.Amount + "','" + sh.ActualAmount + "','" + sh.Bak + "','" + sh.UnitPriceNoTax + "','" + sh.TotalNoTax + "','" + sh.CreateTime + "','" + sh.CreateUser + "','" + sh.Validate + "','" + sh.SHState + "','" + sh.THAmount + "')";
                    SQLBase.ExecuteNonQuery(strInsertList);

                }
                for (int i = 0; i < str.Count; i++)
                {
                    string sql = "update BGOI_PP.dbo.OrderGoods set ActualAmount ='" + str[i] + "' where DID ='" + delist[i].Bak + "'";
                    SQLBase.ExecuteNonQuery(sql);
                }

                if (count > 0)
                {
                    //AddRZ(record.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(record.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static UIDataTable SelectSHCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.SH_ChoseGoods a where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "SHID desc ";
            string strTable = "  BGOI_PP. dbo.SH_ChoseGoods a  ";

            string strField = " a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static bool InsertCPSHXX(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ReceivingInformation>(record, "[BGOI_PP].[dbo].ReceivingInformation");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                foreach (PP_StorageDetailed sh in delist)
                {
                    strInsertList = "insert into BGOI_PP.dbo.StorageDetailed (SHID,DID,INID,OrderContent,Specifications,Supplier,Unit,Amount,UnitPriceNoTax,TotalNoTax,CreateTime,CreateUser,Validate,SHStates,THAmount,Bak,LJCPID,ActualAmount) values('" + sh.SHID + "','" + sh.DID + "','" + sh.INID + "','" + sh.OrderContent + "','" + sh.Specifications + "','" + sh.Supplier + "','" + sh.Unit + "','" + sh.Amount + "','" + sh.UnitPriceNoTax + "','" + sh.TotalNoTax + "','" + sh.CreateTime + "','" + sh.CreateUser + "','" + sh.Validate + "','" + sh.SHState + "','" + sh.THAmount + "','" + sh.Bak + "','" + sh.LJCPID + "','" + sh.Amount + "')";
                    SQLBase.ExecuteNonQuery(strInsertList);
                    string update = " update BGOI_PP.dbo.OrderGoods  set ActualAmount='" + sh.ActualAmount + "' where DID='" + sh.Bak + "'";
                    SQLBase.ExecuteNonQuery(update);


                }

                foreach (PP_ChoseGoods sh in str)
                {
                    string strInsertSH = "insert into BGOI_PP.dbo.SH_ChoseGoods (ID, SHID, CPPID, PID, Name, Spc, Num, Units,  UnitPrice, UnitPrices, Price2, Price2s,CreateTime,CreateUser,RKnum,SHnum) values('" + sh.ID + "','" + sh.DDID + "','" + sh.FKnum + "','" + sh.PID + "','" + sh.Name + "','" + sh.Spc + "','" + sh.Num + "','" + sh.Units + "' ,'" + sh.UnitPrice + "','" + sh.UnitPrices + "','" + sh.Price2 + "','" + sh.Price2s + "','" + sh.CreateTime + "','" + sh.CreateUser + "','0','" + sh.SHnum + "')";
                    SQLBase.ExecuteNonQuery(strInsertSH);
                }


                if (count > 0)
                {
                    //AddRZ(record.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(record.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static DataTable SelectSHDDID(string where)
        {
            string sql = "select  * from BGOI_PP.dbo.ReceivingInformation   where " +
               " " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectSHXX(string where)
        {
            string sql = "select a.*,b.DDID,b.ArrivalProcess, Convert(varchar(10), b.ArrivalDate,23) as ArrivalDate ,b.ArrivalDescription, d.COMNameC  ,c.ActualAmount as DDAmount from BGOI_PP.dbo.StorageDetailed as a  " +
                " left join BGOI_PP.dbo.ReceivingInformation  as b on a.SHID=b.SHID " +

                "   left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier " +
                " left join BGOI_PP.dbo.OrderGoods as c on a.Bak=c.DID  " +
                " where  a.Validate='1' and " + where + "";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateSH(PP_ReceivingInformation pp, List<PP_StorageDetailed> list, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strUpdate = "update ReceivingInformation set ArrivalDate=@ArrivalDate,ArrivalDescription=@ArrivalDescription,ArrivalProcess=@ArrivalProcess  where SHID=@SHID";
                SqlParameter[] param ={new SqlParameter ("@ArrivalDate",SqlDbType .DateTime),
                                       new SqlParameter ("@ArrivalDescription",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@ArrivalProcess",SqlDbType .NVarChar ),
                                 
                                       new SqlParameter ("@SHID",SqlDbType .NVarChar )
                                     };
                param[0].Value = pp.ArrivalDate;
                param[1].Value = pp.ArrivalDescription;
                param[2].Value = pp.ArrivalProcess;
                param[3].Value = pp.SHID;

                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.ReceivingInformation_HIS (SHID, DDID, XXID, ArrivalProcess, ArrivalDescription, ArrivalDate, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
                "select SHID, DDID, XXID, ArrivalProcess, ArrivalDescription, ArrivalDate, CreateTime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.ReceivingInformation where SHID ='" + pp.SHID + "'";


                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.StorageDetailed_HIS (SHID, DID, INID, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, Bak, UnitPriceNoTax, TotalNoTax, CreateTime, CreateUser, Validate, SHStates, THAmount, NCreateUser, NCreateTime)" +
                          "select b.*,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                          " from  BGOI_PP.dbo.StorageDetailed as b where SHID='" + pp.SHID + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");
                    foreach (PP_StorageDetailed item in list)
                    {
                        string sql = " update   BGOI_PP.dbo.StorageDetailed set ActualAmount ='" + item.ActualAmount + "' where DID='" + item.DID + "'";
                        SQLBase.ExecuteNonQuery(sql, "MainPP");

                        int a = Convert.ToInt32(item.ShuLiang) - Convert.ToInt32(item.ActualAmount);

                        string sqls = " update BGOI_PP.dbo.OrderGoods set ActualAmount= (select ActualAmount from  BGOI_PP.dbo.OrderGoods where DID='" + item.Bak + "') - " + a + "  where DID='" + item.Bak + "'";
                        SQLBase.ExecuteNonQuery(sqls, "MainPP");
                    }


                    //if (a > 0)
                    //{
                    //    AddRZ(list[0].CID, "修改请购单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "请购");
                    //}


                }

                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }


        public static DataTable SelectSHSupplier(string where)
        {
            //string sql = "    select distinct a.Name,b.*,c.Number,d.COMNameC,e.Amount as DDAmount ,e.ActualAmount as DDActualAmount from BGOI_PP.dbo.SH_ChoseGoods a    ";
            //sql += "  left join  BGOI_PP.dbo.StorageDetailed b on a.PID=b.LJCPID  ";
            //sql += "   left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.INID=c.PartPID ";
            //sql += "  left join BGOI_PP.dbo.OrderGoods e on b.Bak = e.DID   ";
            //sql += "  left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID   where " + where;
            string sql = "    select distinct b.*,a.Name,isnull(c.Number,0) as Number,d.COMNameC,e.Amount as DDAmount ,e.ActualAmount as DDActualAmount from  BGOI_PP.dbo.StorageDetailed b     ";
            sql += "      left join BGOI_PP.dbo.SH_ChoseGoods a on a.PID=b.LJCPID   ";
            sql += "      left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.LJCPID=c.ProductID and b.INID=c.PartPID ";
            sql += "  left join BGOI_PP.dbo.OrderGoods e on b.Bak = e.DID   ";
            sql += "   left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID   where " + where + "  order by Number asc";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static int UpdateSHValidate(string where, string xxid)
        {
            string sql = "update BGOI_PP.dbo.ReceivingInformation set Validate='-1' where SHID='" + where + "'";
            string str = " select b.ActualAmount,b.OrderContent,b.Bak,b.Amount from BGOI_PP.dbo.ReceivingInformation  as a " +
            " left join BGOI_PP.dbo.StorageDetailed  as b on a.SHID=b.SHID  " +
            " where a.SHID='" + where + "' ";
            DataTable dt = SQLBase.FillTable(str, "MainPP");
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            string sqls = "update BGOI_PP.dbo.StorageDetailed set Validate='-1' where SHID='" + where + "'";
            SQLBase.ExecuteNonQuery(sqls, "MainPP");
            if (count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (xxid == "L")
                    {
                        string strs = " update BGOI_PP.dbo.OrderGoods set ActualAmount=((select ActualAmount from  BGOI_PP.dbo.OrderGoods where DID='" + dt.Rows[i]["Bak"] + "') - " + dt.Rows[i]["ActualAmount"] + ") where DID='" + dt.Rows[i]["Bak"] + "'";
                        SQLBase.ExecuteNonQuery(strs, "MainPP");
                    }
                    else
                    {
                        string strs = " update BGOI_PP.dbo.OrderGoods set ActualAmount=((select ActualAmount from  BGOI_PP.dbo.OrderGoods where DID='" + dt.Rows[i]["Bak"] + "') - " + dt.Rows[i]["Amount"] + ") where DID='" + dt.Rows[i]["Bak"] + "'";
                        SQLBase.ExecuteNonQuery(strs, "MainPP");
                    }
                }
            }
            return count;
        }

        public static DataTable SelectGoods(string where)
        {
            string sql = "select * from BGOI_PP.dbo.StorageDetailed where Bak='" + where + "' and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static int UpdateGoods(string Amount, string where)
        {
            string sql = "update BGOI_PP.dbo.StorageDetailed set ActualAmount='" + Amount + "' where Bak='" + where + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            string sqls = "update BGOI_PP.dbo.OrderGoods set ActualAmount ='" + Amount + "' where DID ='" + where + "'";
            SQLBase.ExecuteNonQuery(sqls);
            return count;
        }
        public static bool deleteFile(string id)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");
            string strInsert = "update BGOI_PP.dbo.pp_File set FileName = NULL,FileInfo = NULL where ID = '" + id + "'";
            if (strInsert != "")
                intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
            sqlTrans.Close(true);
            if (intInsert > 0)
                return true;
            else
            {
                return false;
            }
        }
        public static DataTable SelectSHCP(string where)
        {
            string sql = "  select * from BGOI_PP.dbo.SH_ChoseGoods " +
                        "where " + where + "";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateCPSHXX(PP_ReceivingInformation record, string DID, List<PP_StorageDetailed> delist, ref string strErr, List<PP_ChoseGoods> str)
        {

            string InserNewOrdersHIS = "insert into BGOI_PP.dbo.ReceivingInformation_HIS (SHID, DDID, XXID, ArrivalProcess, ArrivalDescription, ArrivalDate, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
               "select SHID, DDID, XXID, ArrivalProcess, ArrivalDescription, ArrivalDate, CreateTime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.ReceivingInformation where SHID ='" + record.SHID + "'";
            SQLBase.ExecuteNonQuery(InserNewOrdersHIS);


            string strInsertDetailHIS = "insert into BGOI_PP.dbo.StorageDetailed_HIS (SHID, DID, LJCPID,INID, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, Bak, UnitPriceNoTax, TotalNoTax, CreateTime, CreateUser, Validate, SHStates, THAmount, NCreateUser, NCreateTime)" +
                           "select b.*,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                           " from  BGOI_PP.dbo.StorageDetailed as b where SHID='" + record.SHID + "'";
            SQLBase.ExecuteNonQuery(strInsertDetailHIS);

            string date = "select * from BGOI_PP.dbo.ReceivingInformation where SHID='" + record.SHID + "' ";
            DataTable table = SQLBase.FillTable(date, "MainPP");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                SQLBase.ExecuteNonQuery("update BGOI_PP.dbo.OrderGoods set ActualAmount='0' where DDID='" + table.Rows[i]["DDID"] + "' ");
            }



            string deleteStorageDetailed = " delete from BGOI_PP.dbo.StorageDetailed where SHID='" + record.SHID + "'";
            string deleteReceivingInformation = " delete from BGOI_PP.dbo.ReceivingInformation where SHID='" + record.SHID + "'";
            string deleteSH_ChoseGoods = " delete from BGOI_PP.dbo.SH_ChoseGoods where SHID='" + record.SHID + "'";
            SQLBase.ExecuteNonQuery(deleteStorageDetailed);
            SQLBase.ExecuteNonQuery(deleteReceivingInformation);
            SQLBase.ExecuteNonQuery(deleteSH_ChoseGoods);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ReceivingInformation>(record, "[BGOI_PP].[dbo].ReceivingInformation");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                foreach (PP_StorageDetailed sh in delist)
                {
                    strInsertList = "insert into BGOI_PP.dbo.StorageDetailed (SHID,DID,INID,OrderContent,Specifications,Supplier,Unit,Amount,UnitPriceNoTax,TotalNoTax,CreateTime,CreateUser,Validate,SHStates,THAmount,Bak,LJCPID,ActualAmount) values('" + sh.SHID + "','" + sh.DID + "','" + sh.INID + "','" + sh.OrderContent + "','" + sh.Specifications + "','" + sh.Supplier + "','" + sh.Unit + "','" + sh.Amount + "','" + sh.UnitPriceNoTax + "','" + sh.TotalNoTax + "','" + sh.CreateTime + "','" + sh.CreateUser + "','" + sh.Validate + "','" + sh.SHState + "','" + sh.THAmount + "','" + sh.Bak + "','" + sh.LJCPID + "','" + sh.Amount + "')";
                    SQLBase.ExecuteNonQuery(strInsertList);
                    string update = " update BGOI_PP.dbo.OrderGoods  set ActualAmount='" + sh.ActualAmount + "' where DID='" + sh.Bak + "'";
                    SQLBase.ExecuteNonQuery(update);
                }

                foreach (PP_ChoseGoods sh in str)
                {
                    string strInsertSH = "insert into BGOI_PP.dbo.SH_ChoseGoods (ID, SHID, CPPID, PID, Name, Spc, Num, Units,  UnitPrice, UnitPrices, Price2, Price2s,CreateTime,CreateUser,RKnum) values('" + sh.ID + "','" + sh.DDID + "','" + sh.FKnum + "','" + sh.PID + "','" + sh.Name + "','" + sh.Spc + "','" + sh.Num + "','" + sh.Units + "' ,'" + sh.UnitPrice + "','" + sh.UnitPrices + "','" + sh.Price2 + "','" + sh.Price2s + "','" + sh.CreateTime + "','" + sh.CreateUser + "','0')";
                    SQLBase.ExecuteNonQuery(strInsertSH);
                }


                if (count > 0)
                {
                    //AddRZ(record.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(record.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        #endregion

        #region[退货]

        public static string GetTopListTHID()
        {
            string strID = "";
            string strD = "TH-" + DateTime.Now.ToString("yyMMdd");
            string str = GAccount.GetAccountInfo().UnitID;
            string strSqlID = "select MAX(THID) from BGOI_PP.dbo.ReturnGoods where OrderUnit='" + str + "' ";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }
        public static bool InsertTH(PP_ReturnGoods record, string DID, List<PP_ReturngoodsDetails> delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ReturnGoods>(record, "[BGOI_PP].[dbo].ReturnGoods");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                foreach (PP_ReturngoodsDetails list in delist)
                {
                    string sql = "insert into [BGOI_PP].[dbo].ReturngoodsDetails  (EID,DID,INID,OrderContent,Specifications,Supplier,Unit,Amount,UnitPriceNoTax,TotalNoTax,[GoodsUse],Bak) values ('" + list.EID + "','" + list.DID + "','" + list.INID + "','" + list.OrderContent + "','" + list.Specifications + "','" + list.Supplier + "','" + list.Unit + "','" + list.Amount + "','" + list.UnitPriceNoTax + "','" + list.TotalNoTax + "','" + list.GoodsUse + "','" + list.Bak + "')";
                    count += SQLBase.ExecuteNonQuery(sql);
                }
                if (count > 0)
                {
                    //AddRZ(record.THID, "增加退货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                    foreach (PP_ReturngoodsDetails list in delist)
                    {
                        //AddRZ(record.SHID, "增加退货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                        string sql = "update BGOI_PP.dbo.StorageDetailed set THAmount ='" + list.THAmount + "' where DID='" + list.Bak + "'";
                        SQLBase.ExecuteNonQuery(sql);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static DataTable SelectTHDDID(string where)
        {
            string sql = "select * from BGOI_PP.dbo.ReturnGoods where " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectTH(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.ReturnGoods a " +
                                   "  left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId " +
                                   " left join BJOI_UM.dbo.UM_UserNew as c on a.ReturnHandler=c.UserId where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.ReturnDate desc ";
            String strTable = " BGOI_PP.dbo.ReturnGoods a  " +
                "  left join BJOI_UM.dbo.UM_UnitNew d on a.OrderUnit=d.DeptId " +
                 "  left join BJOI_UM.dbo.UM_UserNew as c on a.ReturnHandler=c.UserId ";
            String strField = " a.THID,CONVERT(varchar(10), a.ReturnDate, 23) as ReturnDate,a.SHID,a.ReturnType,a.ReturnMode,a.TheProject,a.ReturnAgreement,a.ReturnHandler,a.ReturnDescription,c.UserName ,d.DeptName ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }

        public static UIDataTable SelectTHGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.ReturngoodsDetails a" +
                                   "    left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.EID ";
            String strTable = " BGOI_PP.dbo.ReturngoodsDetails a  " +
                "  left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier ";
            String strField = " a.EID,a.INID,a.OrderContent,a.Specifications,a.Supplier,a.Unit,a.Amount,a.UnitPriceNoTax,a.TotalNoTax ,d.COMNameC ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }
        public static DataTable SelectTHXQ(string where)
        {
            string sql = " select a.THID,a.ReturnType,a.TheProject,a.ReturnMode,a.ReturnAgreement,a.ReturnHandler,a.ReturnDescription,Convert(varchar(10),a.ReturnDate,23) as ReturnDate ," +
                "   a.Validate,b.INID,b.DID,b.OrderContent,b.Specifications,b.Supplier,b.Unit,b.Amount,b.UnitPriceNoTax,b.TotalNoTax,b.GoodsUse,b.Bak,c.UserName ,d.Text as THLX,e.Text as THFS " +
                "   from BGOI_PP.dbo.ReturnGoods a " +
                " left join BJOI_UM.dbo.UM_UserNew as c on a.ReturnHandler=c.UserId  " +
                "  left join BGOI_PP.dbo. ReturngoodsDetails as b on a.THID =b.EID " +
                "  left join BGOI_PP.dbo.tk_ConfigState as d on a.ReturnType=d.id and d.Type='退货类型' " +
                "  left join (select * from BGOI_PP.dbo.tk_ConfigState) as e on e.id=a.ReturnMode and e.Type='退货方式' where  a.Validate='1' and " + where;

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static bool UpdateTH(PP_ReturnGoods pp, List<PP_ReturngoodsDetails> list, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strUpdate = "update ReturnGoods set ReturnDate=@ReturnDate,ReturnType=@ReturnType,ReturnMode=@ReturnMode ,ReturnAgreement=@ReturnAgreement,TheProject=@TheProject,ReturnDescription=@ReturnDescription,ReturnHandler=@ReturnHandler where THID=@THID";
                SqlParameter[] param ={new SqlParameter ("@ReturnDate",SqlDbType .DateTime),
                                       new SqlParameter ("@ReturnType",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@ReturnMode",SqlDbType .NVarChar ),
                                       new SqlParameter ("@ReturnAgreement",SqlDbType .NVarChar),
                                       new SqlParameter ("@TheProject",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@ReturnDescription",SqlDbType .NVarChar ),
                                       new SqlParameter ("@ReturnHandler",SqlDbType .NVarChar ),
                                       new SqlParameter ("@THID",SqlDbType .NVarChar )
                                     };
                param[0].Value = pp.ReturnDate;
                param[1].Value = pp.ReturnType;
                param[2].Value = pp.ReturnMode;
                param[3].Value = pp.ReturnAgreement;
                param[4].Value = pp.TheProject;
                param[5].Value = pp.ReturnDescription;
                param[6].Value = pp.ReturnHandler;
                param[7].Value = pp.THID;

                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.ReturnGoods_HIS (THID, SHID, XXID, ReturnType, ReturnMode, TheProject, ReturnAgreement, ReturnHandler, ReturnDescription, ReturnDate, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
                "select THID, SHID, XXID, ReturnType, ReturnMode, TheProject, ReturnAgreement, ReturnHandler, ReturnDescription, ReturnDate, CreateTime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.ReturnGoods where THID ='" + pp.THID + "'";

                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.ReturngoodsDetails_HIS (EID, DID, INID, OrderContent, Specifications, Supplier, Unit, Amount, GoodsUse, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Bak, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
                          "select b.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                          " from  BGOI_PP.dbo.ReturngoodsDetails as b where EID='" + pp.THID + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");

                    for (int i = 0; i < list.Count; i++)
                    {
                        string sql = "  update [BGOI_PP].[dbo].ReturngoodsDetails set Amount ='" + list[i].Amount + "' where DID='" + list[i].DID + "'";
                        SQLBase.ExecuteNonQuery(sql, "MainPP");
                        int a = Convert.ToInt32(list[i].shuliang) - Convert.ToInt32(list[i].Amount);
                        if (a < 0)
                        {
                            string sqls = " update BGOI_PP.dbo.StorageDetailed set THAmount= (select THAmount from  BGOI_PP.dbo.StorageDetailed where DID='" + list[i].Bak + "') - " + a + "  where DID='" + list[i].Bak + "'";
                            SQLBase.ExecuteNonQuery(sqls, "MainPP");
                        }
                        else
                        {
                            string sqls = " update BGOI_PP.dbo.StorageDetailed set THAmount= (select THAmount from  BGOI_PP.dbo.StorageDetailed where DID='" + list[i].Bak + "') - " + a + "  where DID='" + list[i].Bak + "'";
                            SQLBase.ExecuteNonQuery(sqls, "MainPP");
                        }
                        //AddRZ(pp.THID, "修改退货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
                    }

                }


                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }

        public static int UpdateTHValidate(string where)
        {
            string sql = "update BGOI_PP.dbo.ReturnGoods set Validate='-1' where THID='" + where + "'";
            string sqls = "  select b.Bak,b.Amount,b.OrderContent from BGOI_PP.dbo.ReturnGoods as a left join BGOI_PP.dbo.ReturngoodsDetails as b on a.THID=b.EID where a.THID='" + where + "'";
            DataTable dt = SQLBase.FillTable(sqls, "MainPP");
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strs = " update BGOI_PP.dbo.StorageDetailed set THAmount=((select THAmount from  BGOI_PP.dbo.StorageDetailed where DID='" + dt.Rows[i]["Bak"] + "') - " + dt.Rows[i]["Amount"] + ") where DID='" + dt.Rows[i]["Bak"] + "'";
                    SQLBase.ExecuteNonQuery(strs, "MainPP");
                }
                //AddRZ(where, "撤销退货单", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
            }
            else
            {
                //AddRZ(where, "撤销退货单", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "退货");
            }
            return count;
        }


        #endregion

        #region[入库]
        public static string GetTopListRKID()
        {
            string strID = "";
            string strD = "RK-" + DateTime.Now.ToString("yyMMdd");
            string str = GAccount.GetAccountInfo().UnitID;
            string strSqlID = "select MAX(RKID) from BGOI_PP.dbo.PurchaseInventorys where UnitID='" + str + "' ";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }

        public static UIDataTable SelectRK(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.PurchaseInventorys a  " +
                                   " left join BGOI_Inventory.dbo.tk_WareHouse as b on a.CKID=b.HouseID " +
                                   "  left join BJOI_UM.dbo.UM_UnitNew d on a.UnitID=d.DeptId  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.Rkdate desc ";
            String strTable = "  BGOI_PP. dbo.PurchaseInventorys a  " +
                  " left join BGOI_Inventory.dbo.tk_WareHouse as b on a.CKID=b.HouseID " +
                "  left join BJOI_UM.dbo.UM_UnitNew d on a.UnitID=d.DeptId ";
            String strField = "a.RKID, a.SHID,Convert(varchar(10), a.Rkdate,23 ) as Rkdate, a.CKID, a.XXID, a.RKInstructions, a.Handler, a.RKType, a.UnitID,Convert(varchar(10),a.CreateTime,23) as CreateTime, a.CreateUser, a.Validate, a.State ,d.DeptName ,b.HouseName";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        public static bool InsertRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseInventorys>(record, "[BGOI_PP].[dbo].PurchaseInventorys");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                foreach (PP_GoodsreceiptDetailed rk in delist)
                {
                    string sql = "insert into [BGOI_PP].[dbo].GoodsreceiptDetailed (RKID,DID,INID,OrderContent,Specifications,Supplier,Unit,Amount,Bak,UnitPriceNoTax,TotalNoTax,SJAmount) values('" + rk.RKID + "','" + rk.DID + "','" + rk.INID + "','" + rk.OrderContent + "','" + rk.Specifications + "','" + rk.Supplier + "','" + rk.Unit + "','" + rk.Amount + "','" + rk.Bak + "','" + rk.UnitPriceNoTax + "','" + rk.TotalNoTax + "','" + rk.SJAmount + "')";

                    count += SQLBase.ExecuteNonQuery(sql);
                }

                if (count > 0)
                {

                    //AddRZ(record.RKID, "增加入库", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");
                    string[] str = record.SHID.Split('-');

                    foreach (PP_GoodsreceiptDetailed rk in delist)
                    {
                        if (str[0] == "DD")
                        {
                            string sql = "update BGOI_PP.dbo.OrderGoods set RKState ='" + rk.ShuLiang + "' where DID='" + rk.Bak + "'";
                            SQLBase.ExecuteNonQuery(sql);
                            //AddRZ(record.SHID, "增加入库", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                        }
                        else
                        {
                            string sql = "update BGOI_PP.dbo.StorageDetailed set SHStates ='" + rk.ShuLiang + "' where DID='" + rk.Bak + "'";
                            SQLBase.ExecuteNonQuery(sql);
                            //AddRZ(record.SHID, "增加入库", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");
                        }

                    }
                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(record.RKID, "增加入库", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "入库");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool InsertCPRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr, List<PP_ChoseGoods> cp)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseInventorys>(record, "[BGOI_PP].[dbo].PurchaseInventorys");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                foreach (PP_GoodsreceiptDetailed rk in delist)
                {
                    string insertsql = "insert into [BGOI_PP].[dbo].GoodsreceiptDetailed (RKID, DID, INID, SHDID, LJCPID, OrderContent, Specifications, Supplier, Unit, Amount, SJAmount,  UnitPriceNoTax, TotalNoTax) values('" + rk.RKID + "','" + rk.DID + "','" + rk.INID + "','" + rk.SHDID + "','" + rk.LJCPID + "','" + rk.OrderContent + "','" + rk.Specifications + "','" + rk.Supplier + "','" + rk.Unit + "','" + rk.Amount + "','" + rk.SJAmount + "','" + rk.UnitPriceNoTax + "','" + rk.TotalNoTax + "')";
                    count += SQLBase.ExecuteNonQuery(insertsql);
                    string update = " update  BGOI_PP.dbo.StorageDetailed  set ActualAmount='" + rk.Bak + "' where DID='" + rk.SHDID + "' ";
                    SQLBase.ExecuteNonQuery(update);
                }

                foreach (var item in cp)
                {
                    string insert = "insert into BGOI_PP.dbo.RK_ChoseGoods (ID, RKID, CPPID, PID, Name, Spc, Num, Units,RKnum, UnitPrice, UnitPrices, Price2, Price2s,  CreateTime, CreateUser, Validate) values ('" + item.ID + "','" + item.DDID + "','" + item.Validate + "','" + item.PID + "','" + item.Name + "','" + item.Spc + "','" + item.Num + "','" + item.Units + "','" + item.FKnum + "','" + item.UnitPrice + "','" + item.UnitPrices + "','" + item.Price2 + "','" + item.Price2s + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','1')";
                    SQLBase.ExecuteNonQuery(insert);
                }
                return true;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static DataTable SelectRKXQ(string where)
        {
            string sql = " select a.RKID, a.SHID,Convert(varchar(10), a.Rkdate,23 ) as Rkdate, a.CKID,  a.RKInstructions, a.Handler, a.RKType, a.UnitID, a.CreateTime, a.CreateUser, a.Validate, a.State,  b.DID,b.SJAmount, b.INID, b.OrderContent, b.Specifications, b.Supplier, b.Unit, b.Amount,  b.Bak,b.UnitPriceNoTax,b.TotalNoTax ,c.HouseName  ,d.COMNameC from BGOI_PP.dbo.PurchaseInventorys as a    " +
                 "  left join   BGOI_PP.dbo.GoodsreceiptDetailed as b on a.RKID=b.RKID " +
                 "  left join BGOI_Inventory.dbo.tk_WareHouse as c on a.CKID=c.HouseID  " +
                "  left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=b.Supplier  where a.Validate = '1' and a.RKID='" + where + "'";


            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectRKGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.GoodsreceiptDetailed a  " +
                                   "   left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier where" +
                                  where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.RKID ";
            String strTable = "  BGOI_PP. dbo.GoodsreceiptDetailed a  " +
                "   left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier ";

            String strField = "a.RKID, a.DID, a.INID,a.OrderContent, a.Specifications, a.Supplier, a.Unit, a.Amount, a.Bak,a.SJAmount,d.COMNameC";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectRKDDID(string where)
        {
            string sql = "select * from BGOI_PP.dbo.PurchaseInventorys where " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static DataTable SelectGoodsreceiptDetailed(string where)
        {
            string sql = "select * from BGOI_PP.dbo.GoodsreceiptDetailed where " + where + " ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static bool UpdateRK(PP_PurchaseInventorys pp, List<PP_GoodsreceiptDetailed> list, ref string strErr)
        {
            try
            {
                string strUpdate = "update PurchaseInventorys  set Rkdate=@Rkdate,CKID=@CKID,RKInstructions=@RKInstructions ,Handler=@Handler where RKID=@RKID";
                SqlParameter[] param ={new SqlParameter ("@Rkdate",SqlDbType .DateTime),
                                       new SqlParameter ("@CKID",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@RKInstructions",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Handler",SqlDbType .NVarChar ),
                                       new SqlParameter ("@RKID",SqlDbType .NVarChar )
                                     };
                param[0].Value = pp.Rkdate;
                param[1].Value = pp.CKID;
                param[2].Value = pp.RKInstructions;
                param[3].Value = pp.Handler;
                param[4].Value = pp.RKID;

                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.PurchaseInventorys_HIS (RKID, SHID, Rkdate, CKID, XXID, RKInstructions, Handler, RKType, UnitID, CreateTime, CreateUser, Validate, State, NCreateTime, NCreateUser)" +
                "select RKID, SHID, Rkdate, CKID, XXID, RKInstructions, Handler, RKType, UnitID, CreateTime, CreateUser, Validate, State,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.PurchaseInventorys where RKID ='" + pp.RKID + "'";

                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.GoodsreceiptDetailed_HIS (RKID, DID, INID, OrderContent, Specifications, Supplier, Unit, Amount, SJAmount, Bak, UnitPriceNoTax, TotalNoTax, NCreateTime, NCreateUser)" +
                          "select b.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                          " from  BGOI_PP.dbo.GoodsreceiptDetailed as b where RKID='" + pp.RKID + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");
                    foreach (PP_GoodsreceiptDetailed item in list)
                    {
                        string sql = " update   BGOI_PP.dbo.GoodsreceiptDetailed set SJAmount ='" + item.SJAmount + "' where DID='" + item.DID + "'";
                        SQLBase.ExecuteNonQuery(sql, "MainPP");

                        int a = Convert.ToInt32(item.ShuLiang) - Convert.ToInt32(item.SJAmount);

                        string[] str = item.Bak.Split('-');
                        if (str[0] == "DD")
                        {
                            string sqls = " update BGOI_PP.dbo.OrderGoods set RKState= (select RKState from  BGOI_PP.dbo.OrderGoods where DID='" + item.Bak + "') - " + a + "  where DID='" + item.Bak + "'";
                            SQLBase.ExecuteNonQuery(sqls, "MainPP");
                        }
                        if (str[0] == "SH")
                        {
                            string sqls = " update BGOI_PP.dbo.StorageDetailed set SHStates= (select SHStates from  BGOI_PP.dbo.StorageDetailed where DID='" + item.Bak + "') - " + a + "  where DID='" + item.Bak + "'";
                            SQLBase.ExecuteNonQuery(sqls, "MainPP");
                        }

                    }
                }

                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }
        public static int UpdateRKValidate(string where, string rktype)
        {
            string sql = "update BGOI_PP.dbo.PurchaseInventorys set Validate='-1' where RKID='" + where + "'";
            string sqls = "  select b.SHDID, b.Bak,b.SJAmount,b.OrderContent from BGOI_PP.dbo.PurchaseInventorys as a left join BGOI_PP.dbo.GoodsreceiptDetailed as b on a.RKID=b.RKID where a.RKID='" + where + "'";
            DataTable dt = SQLBase.FillTable(sqls, "MainPP");
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (rktype == "C")
                    {
                        string strs = " update BGOI_PP.dbo.StorageDetailed set ActualAmount=((select ActualAmount from  BGOI_PP.dbo.StorageDetailed where DID='" + dt.Rows[i]["SHDID"] + "') - " + dt.Rows[i]["SJAmount"] + ") where DID='" + dt.Rows[i]["SHDID"] + "'";
                        SQLBase.ExecuteNonQuery(strs, "MainPP");

                    }
                    else
                    {
                        string strs = " update BGOI_PP.dbo.StorageDetailed set SHStates=((select SHStates from  BGOI_PP.dbo.StorageDetailed where DID='" + dt.Rows[i]["Bak"] + "') - " + dt.Rows[i]["SJAmount"] + ") where DID='" + dt.Rows[i]["Bak"] + "'";
                        SQLBase.ExecuteNonQuery(strs, "MainPP");
                    }
                }

            }
            return count;
        }

        public static DataTable SelectRKSupplier(string where)
        {
            string sql = "   select distinct a.Name,b.*,c.Number,d.COMNameC,e.Amount as SHAmount ,e.ActualAmount as SHActualAmount from BGOI_PP.dbo.RK_ChoseGoods a     ";
            sql += "  left join  BGOI_PP.dbo.GoodsreceiptDetailed b on a.PID=b.LJCPID  ";
            sql += "   left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.INID=c.PartPID  ";
            sql += "  left join BGOI_PP.dbo.StorageDetailed e on b.SHDID = e.DID    ";
            sql += "  left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID   where " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }


        public static UIDataTable SelectRKCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.RK_ChoseGoods a where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "RKID desc ";
            string strTable = "  BGOI_PP. dbo.RK_ChoseGoods a  ";

            string strField = " a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectRKCP(string where)
        {
            string sql = "  select * from BGOI_PP.dbo.RK_ChoseGoods " +
                      "where " + where + "";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateCPRK(PP_PurchaseInventorys record, string DID, List<PP_GoodsreceiptDetailed> delist, ref string strErr, List<PP_ChoseGoods> cp)
        {
            string InserNewOrdersHIS = "insert into BGOI_PP.dbo.PurchaseInventorys_HIS (RKID, SHID, Rkdate, CKID, XXID, RKInstructions, Handler, RKType, UnitID, CreateTime, CreateUser, Validate, State, NCreateTime, NCreateUser)" +
              "select RKID, SHID, Rkdate, CKID, XXID, RKInstructions, Handler, RKType, UnitID, CreateTime, CreateUser, Validate, State,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.PurchaseInventorys where RKID ='" + record.RKID + "'";
            SQLBase.ExecuteNonQuery(InserNewOrdersHIS);


            string strInsertDetailHIS = "insert into BGOI_PP.dbo.GoodsreceiptDetailed_HIS (RKID, DID, INID, SHDID, LJCPID, OrderContent, Specifications, Supplier, Unit, Amount, SJAmount, Bak, UnitPriceNoTax, TotalNoTax, NCreateUser, NCreateTime)" +
                           "select b.*,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                           " from  BGOI_PP.dbo.GoodsreceiptDetailed as b where RKID='" + record.RKID + "'";
            SQLBase.ExecuteNonQuery(strInsertDetailHIS);

            string deletePurchaseInventorys = " delete from BGOI_PP.dbo.PurchaseInventorys where RKID='" + record.RKID + "'";
            string deleteGoodsreceiptDetailed = " delete from BGOI_PP.dbo.GoodsreceiptDetailed where RKID='" + record.RKID + "'";
            string deleteRK_ChoseGoods = " delete from BGOI_PP.dbo.RK_ChoseGoods where RKID='" + record.RKID + "'";
            SQLBase.ExecuteNonQuery(deletePurchaseInventorys);
            SQLBase.ExecuteNonQuery(deleteGoodsreceiptDetailed);
            SQLBase.ExecuteNonQuery(deleteRK_ChoseGoods);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseInventorys>(record, "[BGOI_PP].[dbo].PurchaseInventorys");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                foreach (PP_GoodsreceiptDetailed sh in delist)
                {
                    strInsertList = "insert into BGOI_PP.dbo.GoodsreceiptDetailed (RKID, DID, INID, SHDID, LJCPID, OrderContent, Specifications, Supplier, Unit, Amount, SJAmount,   UnitPriceNoTax, TotalNoTax) values('" + sh.RKID + "','" + sh.DID + "','" + sh.INID + "','" + sh.SHDID + "','" + sh.LJCPID + "','" + sh.OrderContent + "','" + sh.Specifications + "','" + sh.Supplier + "','" + sh.Unit + "','" + sh.Amount + "','" + sh.SJAmount + "','" + sh.UnitPriceNoTax + "','" + sh.TotalNoTax + "')";
                    SQLBase.ExecuteNonQuery(strInsertList);
                    string update = " update BGOI_PP.dbo.StorageDetailed  set ActualAmount='" + sh.Bak + "' where DID='" + sh.SHDID + "'";
                    SQLBase.ExecuteNonQuery(update);
                }

                foreach (PP_ChoseGoods sh in cp)
                {
                    string strInsertSH = "insert into BGOI_PP.dbo.RK_ChoseGoods (ID, RKID, CPPID, PID, Name, Spc, Num, Units,  UnitPrice, UnitPrices, Price2, Price2s,CreateTime,CreateUser,RKnum,Validate) values('" + sh.ID + "','" + sh.DDID + "','" + sh.Validate + "','" + sh.PID + "','" + sh.Name + "','" + sh.Spc + "','" + sh.Num + "','" + sh.Units + "' ,'" + sh.UnitPrice + "','" + sh.UnitPrices + "','" + sh.Price2 + "','" + sh.Price2s + "','" + sh.CreateTime + "','" + sh.CreateUser + "','" + sh.SHnum + "','1')";
                    SQLBase.ExecuteNonQuery(strInsertSH);
                }


                if (count > 0)
                {
                    //AddRZ(record.SHID, "增加收货", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

                    return true;
                }
                else
                {
                    return false;
                }
                //AddRZ(record.SHID, "增加收货", "失败", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "收货");

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion

        #region[付款]

        public static string GetTopListFKID()
        {
            string strID = "";
            string strD = "PAY-" + DateTime.Now.ToString("yyMMdd");
            string str = GAccount.GetAccountInfo().UnitID;
            string strSqlID = "select MAX(PayId) from BGOI_PP.dbo.Payment where PayCompany='" + str + "'";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(4, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }


                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;

        }
        public static bool InsertFK(PP_Payment record, string DID, List<PP_PaymentGoods> list, ref string strErr, List<PP_ChoseGoods> str)
        {
            int count = 0;
            int a = 0;
            int b = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_Payment>(record, "[BGOI_PP].[dbo].Payment");

                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);


                    if (count > 0)
                    {
                        foreach (var item in list)
                        {
                            string strInsertList = " insert into BGOI_PP.dbo.PaymentGoods (PayId, PayXid, LJCPID, DID, OrderContent, Specifications, INID, Supplier, Unit, Amount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, Remark) values ('" + item.PayId + "','" + item.PayXid + "','" + item.LJCPID + "','" + item.DID + "','" + item.OrderContent + "','" + item.Specifications + "','" + item.INID + "','" + item.Supplier + "','" + item.Unit + "','" + item.Amount + "','" + item.UnitPriceNoTax + "','" + item.TotalNoTax + "','" + item.UnitPrice + "','" + item.Total + "','" + item.Rate + "','" + item.GoodsUse + "','" + item.Remark + "')";
                            SQLBase.ExecuteNonQuery(strInsertList);

                            string sql = "update [BGOI_PP].[dbo].OrderGoods set SJFK='" + item.GoodsUse + "' where DID='" + item.DID + "'";
                            a = SQLBase.ExecuteNonQuery(sql);
                        }
                        if (a > 0)
                        {
                            for (int i = 0; i < str.Count; i++)
                            {
                                string insert = "insert into BGOI_PP.dbo.FK_ChoseGoods (ID, PAYID,CPPID, PID, Name, Spc, Num, Units , UnitPrice, UnitPrices, Price2, Price2s, CreateTime, CreateUser, Validate) values ('" + str[i].ID + "','" + str[i].DDID + "','" + str[i].FKnum + "','" + str[i].PID + "','" + str[i].Name + "','" + str[i].Spc + "','" + str[i].Num + "','" + str[i].Units + "', '" + str[i].UnitPrice + "','" + str[i].UnitPrices + "','" + str[i].Price2 + "','" + str[i].Price2s + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','1')";
                                b = SQLBase.ExecuteNonQuery(insert);

                            }
                        }

                        trans.Close(true);
                    }
                    if (b > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool InsertLJFK(PP_Payment record, string DID, List<PP_PaymentGoods> list, ref string strErr, List<string> str)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_Payment>(record, "[BGOI_PP].[dbo].Payment");

                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                foreach (var delist in list)
                {
                    string sql = "insert into [BGOI_PP].[dbo].PaymentGoods (PayId,PayXid,OrderContent,Specifications,Unit,Amount,Supplier,UnitPriceNoTax,TotalNoTax,UnitPrice,Total,GoodsUse,INID,Remark,Rate,DID) values ('" + delist.PayId + "','" + delist.PayXid + "','" + delist.OrderContent + "','" + delist.Specifications + "','" + delist.Unit + "','" + delist.Amount + "','" + delist.Supplier + "','" + delist.UnitPriceNoTax + "','" + delist.TotalNoTax + "','" + delist.UnitPrice + "','" + delist.Total + "','" + delist.GoodsUse + "','" + delist.INID + "','" + delist.Remark + "','" + delist.Rate + "','" + delist.DID + "')";
                    count += SQLBase.ExecuteNonQuery(sql);
                }
                if (count > 0)
                {

                    for (int i = 0; i < str.Count; i++)
                    {
                        string sql = "update [BGOI_PP].[dbo].OrderGoods set SJFK='" + str[i] + "' where DID='" + list[i].DID + "'";
                        int a = SQLBase.ExecuteNonQuery(sql);
                        if (a > 0)
                        {
                            //AddRZ(record.DDID, "订单付款", "成功", DateTime.Now.ToString(), GAccount.GetAccountInfo().UserName, "订购");
                        }
                    }


                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static DataTable SelectFKDDID(string where)
        {
            string sql = "select * from BGOI_PP.dbo.Payment where " + where + " and Validate='1'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectFK(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.Payment a  " +
                                   "     left join BJOI_UM.dbo.UM_UnitNew d on a.PayCompany=d.DeptId " +
              "  left join BGOI_PP.dbo.tk_ConfigState as b on b.id=a.State where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.PayTime desc,a.State desc ";
            String strTable = "  BGOI_PP. dbo.Payment a  " +
                "     left join BJOI_UM.dbo.UM_UnitNew d on a.PayCompany=d.DeptId " +

                    "  left join BGOI_PP.dbo.tk_ConfigState as b on b.id=a.State ";
            String strField = "a.PayId, a.DDID, a.PaymentMenthod, a.Paymoney, a.PayCompany, a.Remark,Convert(varchar(10), a.PayTime,23) as PayTime, a.State, a.OrderContacts, a.Createtime, a.CreateUser, a.Validate,b.Text,d.DeptName";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectFKGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.PaymentGoods a  " +
                                   "   left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier where " +
                                  where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.PayId ";
            String strTable = "  BGOI_PP. dbo.PaymentGoods a  " +
                "  left join  BGOI_BasMan .dbo.tk_supplierbas as d on d.SID=a.Supplier ";

            String strField = "a.PayId, a.PayXid, a.OrderContent, a.Specifications, a.INID, a.Supplier, a.Unit, a.Amount, a.UnitPriceNoTax, a.TotalNoTax,a.[GoodsUse],a.Rate,     d.COMNameC";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectFKXQ(string where)
        {
            string sql = "select a.PayId, a.DDID, a.PaymentMenthod, a.Paymoney, a.PayCompany, a.Remark,Convert(varchar(10), a.PayTime,23) as PayTime, a.State," +
                " a.OrderContacts, a.Createtime, a.CreateUser, a.Validate,  b.PayXid,b.DID, b.OrderContent, b.Specifications, b.INID, b.Supplier," +
                " b.Unit, b.Amount, b.UnitPriceNoTax,b.Rate,  b.TotalNoTax,b.[GoodsUse],e.Text as FK,d.Text as ZF ,z.COMNameC" +
                " from BGOI_PP.dbo.Payment as a  " +
                "  left join BGOI_PP.dbo.PaymentGoods as b on a.PayId=b.PayId " +
                "  left join  BGOI_BasMan .dbo.tk_supplierbas as z on z.SID=b.Supplier " +
                " left join BGOI_PP.dbo.tk_ConfigState as d on a.PaymentMenthod=d.id and d.Type='支付方式' " +
                "  left join (select * from BGOI_PP.dbo.tk_ConfigState) as e on a.State=e.id and e.Type='付款状态' where  a.Validate='1' and " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectFKDC()
        {
            string sql = "select  b.OrderContent, b.Specifications, b.Unit, b.Amount, b.UnitPriceNoTax,b.TotalNoTax ,c.Text " +
                "  from BGOI_PP.dbo.Payment as a " +
                "  left join BGOI_PP.dbo.PaymentGoods as b on a.PayId=b.PayId  " +
                " left join BGOI_PP.dbo.tk_ConfigState  as c on a.PaymentMenthod=c.id " +
                "  where c.Type='支付方式' ";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        public static bool UpdateFK(PP_Payment pp, List<PP_PaymentGoods> list, ref string strErr)
        {
            try
            {
                string strUpdate = "update Payment  set PaymentMenthod=@PaymentMenthod,PayTime=@PayTime,State=@State ,OrderContacts=@OrderContacts where PayId=@PayId";
                SqlParameter[] param ={
                                          new SqlParameter ("@PaymentMenthod",SqlDbType .NVarChar  ),
                                          new SqlParameter ("@PayTime",SqlDbType .DateTime),
                                       new SqlParameter ("@State",SqlDbType .NVarChar ),
                                       new SqlParameter ("@OrderContacts",SqlDbType .NVarChar ),
                                       new SqlParameter ("@PayId",SqlDbType .NVarChar )
                                     };
                param[0].Value = pp.PaymentMenthod;
                param[1].Value = pp.PayTime;
                param[2].Value = pp.State;
                param[3].Value = pp.OrderContacts;
                param[4].Value = pp.PayId;

                string InserNewOrdersHIS = "insert into BGOI_PP.dbo.Payment_HIS (PayId, DDID, PaymentMenthod, Paymoney, PayCompany, Remark, PayTime, State, OrderContacts, Createtime, CreateUser, Validate, NCreateTime, NCreateUser)" +
                "select PayId, DDID, PaymentMenthod, Paymoney, PayCompany, Remark, PayTime, State, OrderContacts, Createtime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.Payment where PayId ='" + pp.PayId + "'";

                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {

                    strInsertDetailHIS = "insert into BGOI_PP.dbo.PaymentGoods_HIS (PayId, PayXid, DID, OrderContent, Specifications, INID, Supplier, Unit, Amount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, Remark, NCreateTime, NCreateUser)" +
                          "select b.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                          " from  BGOI_PP.dbo.PaymentGoods as b where PayId='" + pp.PayId + "'";
                    SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainPP");
                    foreach (PP_PaymentGoods item in list)
                    {
                        string sql = " update   BGOI_PP.dbo.PaymentGoods set Rate ='" + item.Rate + "' where PayXid='" + item.PayXid + "'";
                        SQLBase.ExecuteNonQuery(sql, "MainPP");

                        int a = Convert.ToInt32(item.GoodsUse) - Convert.ToInt32(item.Rate);

                        string sqls = " update BGOI_PP.dbo.OrderGoods set SJFK= (select SJFK from  BGOI_PP.dbo.OrderGoods where DID='" + item.DID + "') - " + a + "  where DID='" + item.DID + "'";
                        SQLBase.ExecuteNonQuery(sqls, "MainPP");
                    }
                }
                if (strUpdate != "")
                {
                    SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainPP");
                    SQLBase.ExecuteNonQuery(strUpdate, param, "MainPP");
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }



        }
        public static bool DeleteFK(string where)
        {
            string sql = "Update BGOI_PP.dbo.Payment set Validate ='-1' where PayId ='" + where + "'";
            string sqls = "select b.DID,b.Rate,b.OrderContent from BGOI_PP.dbo.Payment as a left join BGOI_PP.dbo.PaymentGoods as b on a.PayId=b.PayId where a.Payid='" + where + "'";
            DataTable dt = SQLBase.FillTable(sqls, "MainPP");

            int a = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (a > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strs = " update BGOI_PP.dbo.OrderGoods set SJFK=((select SJFK from  BGOI_PP.dbo.OrderGoods where DID='" + dt.Rows[i]["DID"] + "') - " + dt.Rows[i]["Rate"] + ") where DID='" + dt.Rows[i]["DID"] + "'";
                    SQLBase.ExecuteNonQuery(strs, "MainPP");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable SelectFKCP(string where)
        {
            string sql = "  select * from BGOI_PP.dbo.FK_ChoseGoods  " +
                        "where " + where + "";

            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectFKCPXX(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP. dbo.FK_ChoseGoods a where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "PAYID desc ";
            string strTable = "  BGOI_PP. dbo.FK_ChoseGoods a  ";

            string strField = " a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }


        public static bool UpdateFKCP(PP_Payment record, List<PP_PaymentGoods> delist, ref string strErr, List<PP_ChoseGoods> str)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            string InserNewOrdersHIS = "insert into BGOI_PP.dbo.Payment_HIS (PayId, DDID, PaymentMenthod, Paymoney, PayCompany, Remark, PayTime, State, OrderContacts, Createtime, CreateUser, Validate, NCreateTime, NCreateUser)" +
                       "select PayId, DDID, PaymentMenthod, Paymoney, PayCompany, Remark, PayTime, State, OrderContacts, Createtime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_PP.dbo.Payment where PayId ='" + record.PayId + "'";
            SQLBase.ExecuteNonQuery(InserNewOrdersHIS);
            string updatePayment = " update BGOI_PP.dbo.Payment set PaymentMenthod='" + record.PaymentMenthod + "',PayTime='" + record.PayTime
                + "',State='" + record.State + "' where Payid='" + record.PayId + "'";
            SQLBase.ExecuteNonQuery(updatePayment);



            string strInsertDetailHIS = "";
            strInsertDetailHIS = "insert into BGOI_PP.dbo.PaymentGoods_HIS (PayId, PayXid, LJCPID, DID, OrderContent, Specifications, INID, Supplier, Unit, Amount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, Remark, NCreateTime, NCreateUser)" +
                         "select b.*,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                         " from  BGOI_PP.dbo.PaymentGoods as b where PayId='" + record.PayId + "'";
            int a = SQLBase.ExecuteNonQuery(strInsertDetailHIS);


            string delectPaymentGoods = "delete from [BGOI_PP].[dbo].PaymentGoods where Payid='" + record.PayId + "' ";
            SQLBase.ExecuteNonQuery(delectPaymentGoods);

            string delectFK_ChoseGoods = "delete from [BGOI_PP].[dbo].FK_ChoseGoods  where Payid='" + record.PayId + "' ";
            SQLBase.ExecuteNonQuery(delectFK_ChoseGoods);

            string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].PaymentGoods");
            if (strInsertList != "")
            {
                trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
            }

            for (int i = 0; i < str.Count; i++)
            {
                string insertCP = " insert into BGOI_PP.dbo.FK_ChoseGoods  (ID, PAYID, CPPID, PID, Name, Spc, Num, Units, FKnum, UnitPrice, UnitPrices, Price2, Price2s, CreateTime, CreateUser, Validate) values ('" + str[i].ID + "','" + str[i].DDID + "','" + str[i].SHnum + "','" + str[i].PID + "','" + str[i].Name + "','" + str[i].Spc + "','" + str[i].Num + "','" + str[i].Units + "','" + str[i].FKnum + "','" + str[i].UnitPrice + "','" + str[i].UnitPrices + "','" + str[i].Price2 + "','" + str[i].Price2s + "','" + str[i].CreateTime + "','" + str[i].CreateTime + "','1')";
                SQLBase.ExecuteNonQuery(insertCP);
            }

            for (int i = 0; i < delist.Count; i++)
            {
                string updateDD = " update BGOI_PP.dbo.OrderGoods set SJFK ='" + delist[i].Remark + "' where DID='" + delist[i].DID + "'";
                SQLBase.ExecuteNonQuery(updateDD);
            }
            trans.Close(true);
            return true;
        }

        public static DataTable SelectFKSupplier(string where)
        {
            //string sql = "   select distinct a.Name,b.*,c.Number,d.COMNameC,e.Amount as DDAmount ,e.SJFK as SJFK from BGOI_PP.dbo.FK_ChoseGoods  a    ";
            //sql += "  left join  BGOI_PP.dbo.PaymentGoods b on a.PID=b.LJCPID  ";
            //sql += "  left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.INID=c.PartPID  ";
            //sql += "  left join BGOI_PP.dbo.OrderGoods e on b.DID = e.DID  ";
            //sql += "  left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID  where " + where;


            string sql = "    select distinct b.*,a.Name,isnull(c.Number,0) as Number,d.COMNameC,e.Amount as DDAmount ,e.SJFK as SJFK  from  BGOI_PP.dbo.PaymentGoods b     ";
            sql += "      left join BGOI_PP.dbo.FK_ChoseGoods a on a.PID=b.LJCPID   ";
            sql += "      left join BGOI_Inventory.dbo.tk_ProFinishDefine c on b.LJCPID=c.ProductID and b.INID=c.PartPID ";
            sql += "  left join BGOI_PP.dbo.OrderGoods e on b.DID = e.DID  ";
            sql += "   left join BGOI_BasMan .dbo.tk_supplierbas d on b.Supplier=d.SID   where " + where + "  order by Number asc";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        #endregion

        #region[上传]
        public static int InsertFile(PP_File pp, byte[] fileByte, ref string a_strErr)
        {
            int intInsert = 0;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };

            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            string strInsertOrder = "insert into pp_File (FileInfo,FileName,CreateTime,CreateUser,Validate,PID) values (@fileByte,'" + pp.FileName + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','1','" + pp.PID + "')";

            try
            {
                sqlTrans.Open("MainPP");
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, para);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }

        public static DataTable GetDownload(string id)
        {
            string strSql = "select ID, PID,[FileInfo],FileName from pp_File where FileName != '' and FileInfo != '' and PID='" + id + "' ";
            DataTable dt = SQLBase.FillTable(strSql, "MainPP");
            return dt;
        }


        public static DataTable getFile(string id)
        {
            string strSql = "select ID,FileName,FileInfo from pp_File where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainPP");
            return dt;
        }
        #endregion

        #region[供货商]
        public static DataTable GetSupplier(string SID)
        {
            string str = "select a.SID,SupplierType,COMNameC ,SupplierCode ,b.Price from BGOI_BasMan .dbo.tk_SupplierBas a inner join tk_SProducts b on a.SID =b.SID where a.SID='" + SID + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetProductPrice(string ProID, string SupID)
        {
            string Str = "select b.price from tk_SupplierBas a inner join tk_SProducts b on a.SID =b.SID where b.ProductID ='" + ProID + "' and a.COMNameC ='" + SupID + "'";
            DataTable dt = SQLBase.FillTable(Str, "SupplyCnn");
            if (dt == null) return null;
            return dt;
        }

        #endregion

        #region [审批]
        public static bool InsertApproval(PP_Approval app)
        {
            bool ok = false;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            string sql = "insert into BGOI_PP. dbo.Approve (PID,PIDS,ApprovalContent,ApprovalType,ApprovalLevel,Approvaler,Job,ApprovalTime,IsPass,NoPassReason,approvalExplain) values('" + app.PID + "','" + app.PIDS + "','" + app.ApprovalContent + "','" + app.ApprovalType + "','" + app.ApprovalLevel + "','" + app.Approvaler + "','" + app.Job + "','" + app.ApprovalTime + "','" + app.IsPass + "','" + app.NoPassReason + "','" + app.ApprovalExplain + "')";
            int num = SQLBase.ExecuteNonQuery(sql);
            if (num > 0)
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
            return ok;

        }

        public static string GetApprovalSPID()
        {
            string strID = "";
            string strD = "SP-" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select MAX(PID) from BGOI_PP.dbo.Approve";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "000" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "00" + (num + 1);

                        else
                            strD = strD + (num + 1);
                    }
                    else
                    {
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "0001";
            }
            return strD;

        }

        public static DataTable SelectApproval(string CID)
        {
            string sql = "select * from BGOI_PP.dbo.Approve where PIDS='" + CID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static DataTable SelectApprovalUser(string approvaler, string approvaltype)
        {
            string sql = "select a.approvaler,a.ApprovalType,b.ID,b.[Text],c.UserName from  Approve as a left join tk_ConfigState as b on a.ApprovalType=b.id  left join BJOI_UM.dbo.UM_UserNew  as c on a.approvaler=c.UserId  where b.[Type]='审批类型' and a.approvaler='" + approvaler + "' and a.ApprovalType='" + approvaltype + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectSP(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select Count(*) from BGOI_PP.dbo.Inquirys  where state='1' ";

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.InquiryDate desc ";
            String strTable = "   (select distinct(PID),RelevanceID,ApprovalContent from BGOI_PP. dbo.Approvel) as  b   " +
                " left join BGOI_PP.dbo.Inquirys as a  on a.XJID=b.RelevanceID " +
                " left join BJOI_UM.dbo.UM_UserNew as c on a.OrderContacts=c.UserId  " +
                " left join BGOI_PP.dbo.tk_ConfigState as d on a.State=d.id ";
            String strField = "a.XJID,a.State,a.InquiryTitle,c.UserName, CONVERT( varchar(10) ,a.InquiryDate,23) as InquiryDate,b.PID,d.Text ";




            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectDDSP(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select Count(*) from BGOI_PP.dbo.PurchaseOrder  where state='1' ";

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.OrderDate desc ";
            String strTable = "  (select distinct(PID),RelevanceID,ApprovalContent from BGOI_PP. dbo.Approvel) as  b   " +
                " left join BGOI_PP.dbo.PurchaseOrder as a on a.DDID=b.RelevanceID " +
                //" left join BJOI_UM.dbo.UM_UserNew as c on a.OrderContacts=c.UserId  " +
                " left join BGOI_PP.dbo.tk_ConfigState as d on a.State=d.id ";
            String strField = "a.DDID, a.State,CONVERT( varchar(10) ,a.OrderDate,23) as OrderDate,b.PID,d.Text ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }
        #endregion
        #region[系统设置]
        public static DataTable GetConfigContent()
        {
            //string strSql = "select distinct Type from tk_ConfigState where Type='请购状态' or Type='询价单状态' or Type='交货方式' or Type='是否开发票' or Type='支付方式' or  Type='付款约定' or Type='订单付款状态' or Type='到货状态' or  Type='采购订单状态' or Type='付款状态'";
            string strSql = "select distinct Type from tk_ConfigState where    Type='交货方式' or Type='是否开发票' or Type='支付方式' or  Type='付款约定' or  Type='退货类型' or Type='退货方式'";
            DataTable dt = SQLBase.FillTable(strSql, "MainPP");
            return dt;
        }

        public static UIDataTable SelectConfig(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select Count(*) from BGOI_PP.dbo.tk_ConfigState as a where  " + where;

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.id asc ";
            String strTable = "  BGOI_PP.dbo.tk_ConfigState as a  ";
            String strField = "a.* ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static int InsertContent(string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");
            string XID = PreGetTaskNo(type);
            string strSql = "select id,Type from tk_ConfigState where Type='" + type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainPP");
            string TypeDesc = "";
            if (dt.Rows.Count > 0)
            {

                TypeDesc = dt.Rows[0]["Type"].ToString();
            }
            string strInsertOrder = "insert into tk_ConfigState (id,Text,Type,Validate) values ('" + XID + "','" + text + "','" + type + "','v')";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }


        public static string PreGetTaskNo(string Sel)
        {
            string strID = "";
            string xid = "";
            string strSqlID = "select max(id) from tk_ConfigState where Type='" + Sel + "'";
            DataTable dtID = SQLBase.FillTable(strSqlID, "MainPP");
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();

                int num = Convert.ToInt32(strID);
                num = num + 1;

                xid = num.ToString();

            }
            else
            {
                xid = "1";
            }
            return xid;
        }


        public static int UpdateContent(string xid, string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");

            string strInsertOrder = "update BGOI_PP.dbo.tk_ConfigState set Text = '" + text + "' where id = '" + xid + "' and Type = '" + type + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intInsert;
        }
        public static int DeleteContent(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");
            string strInsertOrder = "update BGOI_PP.dbo.tk_ConfigState set Validate = '-1' where id = '" + xid + "' and Type = '" + type + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert;
        }
        #endregion


        #region[二期采购]
        #region[物流]
        public static string GetWLID()
        {


            string strID = "";
            string strD = "WL-" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select MAX(ID) from BGOI_PP.dbo.Logistics";
            string str = GAccount.GetAccountInfo().UnitID;
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00" + str;
                    strD = strD + "0001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 4, 4));

                    string stryyMMdd = strID.Substring(3, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "000" + (num + 1);
                        }
                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "00" + str;
                            strD = strD + "00" + (num + 1);
                        }
                        else
                        {
                            strD = strD + "00" + str;
                            strD = strD + (num + 1);
                        }
                    }
                    else
                    {
                        strD = strD + "00" + str;
                        strD = strD + "0001";
                    }
                }
            }
            else
            {
                strD = strD + "00" + str;
                strD = strD + "0001";
            }
            return strD;






        }
        public static bool InsertWL(PP_Logistics record, List<PP_LogisticsGoods> delist, ref string strErr)
        {


            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].LogisticsGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_Logistics>(record, "[BGOI_PP].[dbo].Logistics");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static UIDataTable SelectWL(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.Logistics a  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime desc ";
            String strTable = "  BGOI_PP.dbo.Logistics a  ";
            String strField = " a.*";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectWLGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.LogisticsGoods a  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime desc ";
            String strTable = "  BGOI_PP.dbo.LogisticsGoods a  ";
            String strField = " a.*";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectWLGoodsXX(string where)
        {
            string sql = " select a.*,b.* from BGOI_PP.dbo.Logistics as a  ";
            sql += " left join BGOI_PP.dbo.LogisticsGoods as b on a.ID=b.ID where " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool UpdateWL(PP_Logistics record, List<PP_LogisticsGoods> delist, ref string strErr)
        {
            string Logistics_HIS = "insert into BGOI_PP.dbo.Logistics_HIS (ID, SQCompany, THCompany, SHaddress, SHContacts, SHTel, FHConsignor, FHTel, FHFax, LogisticsS, LogisticsSTel, LogisticsSFax, CreateTime, CreateUser, Validate, NCreateUser, NCreateTime)" +
        "select ID, SQCompany, THCompany, SHaddress, SHContacts, SHTel, FHConsignor, FHTel, FHFax, LogisticsS, LogisticsSTel, LogisticsSFax, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.Logistics where ID ='" + record.ID + "'";
            SQLBase.ExecuteNonQuery(Logistics_HIS);

            string LogisticsGoods_HIS = "insert into BGOI_PP.dbo.LogisticsGoods_HIS (ID, ProName, Spec, Amount, CreateTime, CreateUser, Validate, NCreateUser, NCreateTime)" +
     "select ID, ProName, Spec, Amount, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.LogisticsGoods where ID ='" + record.ID + "'";
            SQLBase.ExecuteNonQuery(LogisticsGoods_HIS);

            string sql = " delete from BGOI_PP.dbo.Logistics where id='" + record.ID + "'";
            string sqls = " delete from BGOI_PP.dbo.LogisticsGoods where id='" + record.ID + "'";
            SQLBase.ExecuteNonQuery(sql);
            SQLBase.ExecuteNonQuery(sqls);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].LogisticsGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_Logistics>(record, "[BGOI_PP].[dbo].Logistics");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool DeleteWL(string where)
        {//BGOI_PP.dbo.Logistics
            string sql = " update BGOI_PP.dbo.Logistics set Validate='-1' where " + where + " ";
            int a = SQLBase.ExecuteNonQuery(sql);
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region[订购]
        public static bool ErInsertOrder(PP_ErPurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {


            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ErPurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool ErDDUpdate(PP_ErPurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {
            string InserPurchaseOrder_HIS = "insert into BGOI_PP.dbo.PurchaseOrder_HIS (DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, GoodsType, GoodsUnits, StockSituation, ProjectPeople, Contract, Tsix, ContractNoReason, SaleUnitPrice, ContractTotal, FKexplain, ProjectHK, CreateTime, CreateUser, Validate, NCreateUser, NCreateTime)" +
             "select DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, GoodsType, GoodsUnits, StockSituation, ProjectPeople, Contract, Tsix, ContractNoReason, SaleUnitPrice, ContractTotal, FKexplain, ProjectHK, CreateTime, CreateUser, Validate, '" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.PurchaseOrder where DDID ='" + record.DDID + "'";

            string InserOrderGoods_HIS = "insert into BGOI_PP.dbo.OrderGoods_HIS (DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate, NCreateUser,NCreateTime )" +
           "select DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.OrderGoods where DDID ='" + record.DDID + "'";
            SQLBase.ExecuteNonQuery(InserPurchaseOrder_HIS);
            SQLBase.ExecuteNonQuery(InserOrderGoods_HIS);
            string sql = "delete from BGOI_PP.dbo.PurchaseOrder where ddid='" + record.DDID + "'";
            string sqls = "delete from BGOI_PP.dbo.OrderGoods where ddid='" + record.DDID + "'";
            SQLBase.ExecuteNonQuery(sql);
            SQLBase.ExecuteNonQuery(sqls);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_ErPurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static DataTable ErSelectGoodsDDID(string where)
        {
            string sql = " select a.*,b.*,j.COMNameC ,c.text as GoodsTypes,d.text as Tsixs,e.text as Contracts ,j.SID from BGOI_PP.dbo.PurchaseOrder as a ";

            sql += " left join BGOI_PP.dbo.OrderGoods as b on a.DDID=b.DDID ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as c on a.GoodsType = c.id and c.Type = '采购申请类型' ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as d on a.Tsix = d.id and d.Type = 'T6系统' ";
            sql += " left join (select * from BGOI_PP.dbo.tk_ConfigState) as e on a.Contract = e.id and e.Type = '合同' ";
            sql += "  left join BGOI_BasMan .dbo.tk_supplierbas as j on b.Supplier=j.SID  where " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }
        #endregion
        #endregion
        #region[三期采购]
        #region[订购]
        public static bool InsertOrderSan(PP_PurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {


            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        public static bool SanUpdateDDS(PP_PurchaseOrder record, List<PP_OrderGoods> delist, ref string strErr)
        {


            string InserPurchaseOrder_HIS = "insert into BGOI_PP.dbo.PurchaseOrder_HIS (DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, GoodsType, GoodsUnits, StockSituation, ProjectPeople, Contract, Tsix, ContractNoReason, SaleUnitPrice, ContractTotal, FKexplain, ProjectHK, CreateTime, CreateUser, Validate, NCreateUser, NCreateTime)" +
           "select DDID, CID, PID, OrderNumber, OrderUnit, OrderContacts, Approver1, Approver2, ArrivalStatus, PayStatus, DDState, State, BusinessTypes, PleaseExplain, OrderDate, DeliveryLimit, DeliveryMethod, IsInvoice, PaymentMethod, PaymentAgreement, ContractNO, TheProject, TotalTax, TotalNoTax, GoodsType, GoodsUnits, StockSituation, ProjectPeople, Contract, Tsix, ContractNoReason, SaleUnitPrice, ContractTotal, FKexplain, ProjectHK, CreateTime, CreateUser, Validate, '" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.PurchaseOrder where DDID ='" + record.DDID + "'";

            string InserOrderGoods_HIS = "insert into BGOI_PP.dbo.OrderGoods_HIS (DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate, NCreateUser,NCreateTime )" +
           "select DID, DDID, MaterialNO, OrderContent, Specifications, Supplier, Unit, Amount, ActualAmount, UnitPriceNoTax, TotalNoTax, UnitPrice, Total, Rate, GoodsUse, RKState, Remark, SJFK, DrawingNum, PurchaseDate, GoodsName, Goodsyiju, GoodsNum, CreateTime, CreateUser, Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "' from BGOI_PP.dbo.OrderGoods where DDID ='" + record.DDID + "'";
            SQLBase.ExecuteNonQuery(InserPurchaseOrder_HIS);
            SQLBase.ExecuteNonQuery(InserOrderGoods_HIS);
            string sql = "delete from BGOI_PP.dbo.PurchaseOrder where ddid='" + record.DDID + "'";
            string sqls = "delete from BGOI_PP.dbo.OrderGoods where ddid='" + record.DDID + "'";
            SQLBase.ExecuteNonQuery(sql);
            SQLBase.ExecuteNonQuery(sqls);
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainPP");
            try
            {
                string strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_PP].[dbo].OrderGoods");
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);
                string strInsert = GSqlSentence.GetInsertInfoByD<PP_PurchaseOrder>(record, "[BGOI_PP].[dbo].PurchaseOrder");
                if (strInsert != "")
                {
                    count += SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion
        #endregion
        #region[交接单]
        public static string GetTopListJJID()
        {
            string strID = "";
            string strD = DateTime.Now.ToString("yyyy");
            string strSqlID = "select MAX(TransferNum) from BGOI_PP.dbo.TransferS";

            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {

                    strD = strD + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(5, 2));

                    string stryyMMdd = strID.Substring(0, 4);
                    if (DateTime.Now.ToString("yyyy") == stryyMMdd)
                    {
                        if (num < 9)
                        {
                            strD = strD + "00" + (num + 1);
                        }

                        else if (num < 99 && num >= 9)
                        {
                            strD = strD + "0" + (num + 1);
                        }


                        else
                        {
                            strD = strD + (num + 1);
                        }

                    }
                    else
                    {
                        strD = strD + "001";
                    }
                }
            }
            else
            {

                strD = strD + "001";
            }
            return strD;

        }
        public static bool InsertJJD(PP_TransferS tran, List<PP_TransferGoods> goods)
        {
            int a = 0;
            string sqltran = "insert into BGOI_PP.dbo.TransferS (SHID,TransferNum,SJPeople,Inspectiondate,GoodDate ) values ('" + tran.SHID + "','" + tran.TransferNum + "','" + tran.SJPeople + "','" + tran.Inspectiondate + "','" + tran.Gooddate + "' )";
            int count = SQLBase.ExecuteNonQuery(sqltran);
            if (count > 0)
            {
                foreach (var item in goods)
                {
                    string sqlgoods = " insert into BGOI_PP.dbo.TransferGoods (ID, PID,GoodsNum,GoodsName,GoodsSpe,Supplier,Amount,Unit,Remark,Bak) values ('" + item.ID + "','" + item.PID + "','" + item.GoodsNum + "','" + item.GoodsName + "','" + item.GoodsSpe + "','" + item.Supplier + "','" + item.Amount + "','" + item.Unit + "' ,'" + item.Remark + "','" + item.Bak + "')";
                    a += SQLBase.ExecuteNonQuery(sqlgoods);
                }
                if (a > 0)
                {
                    return true;
                }
                return true;
            }
            else
            {
                return false;
            }


        }

        public static UIDataTable SelectJJD(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_PP.dbo.TransferS  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " TransferNum desc  ";
            String strTable = " BGOI_PP.dbo.TransferS  ";
            String strField = "SHID, TransferNum, SJPeople,   Convert(varchar(10),Inspectiondate,111) as Inspectiondate, Convert(varchar(10),Gooddate,111) as Gooddate,  Convert(varchar(10),LJReturnDate,111) as LJReturnDate, Summary, testPeople, qualified, Noqualified, productionPeople, planPeople, Warehouse, Bak, Remark";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static UIDataTable SelectJJDGoods(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " BGOI_PP.dbo.TransferGoods  as  a " +
                                  " left join  BGOI_BasMan .dbo.tk_supplierbas as b on b.SID=a.Supplier where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.ID ";
            String strTable = " BGOI_PP.dbo.TransferGoods as a " +
                " left join  BGOI_BasMan .dbo.tk_supplierbas as b on b.SID=a.Supplier ";
            String strField = " a.*,b.COMNameC ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }


            return instData;
        }

        public static DataTable SelectJJDXX(string where)
        {
            string sql = " select SHID, TransferNum, SJPeople,   Convert(varchar(10),Inspectiondate,111) as Inspectiondate, Convert(varchar(10),Gooddate,111) as Gooddate,  Convert(varchar(10),LJReturnDate,111) as LJReturnDate, Summary, testPeople, qualified, Noqualified, productionPeople, planPeople, Warehouse, a.Bak as Baka, a.Remark as Remarka,b.*, c.COMNameC from BGOI_PP.dbo.TransferS as a ";
            sql += "   left join  BGOI_PP.dbo.TransferGoods as b on a.TransferNum =b.PID ";
            sql += "   left join  BGOI_BasMan .dbo.tk_supplierbas as c on b.supplier=c.SID where " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainPP");
            return dt;
        }

        public static bool InsertJJDXX(PP_TransferS tran, List<PP_TransferGoods> goods)
        {

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");
            string updatejj = " update BGOI_PP.dbo.TransferS set Summary='" + tran.Summary + "',testPeople='" + tran.testPeople + "',Bak='" + tran.Bak + "',LJReturnDate='" + DateTime.Now + "' where TransferNum='" + tran.TransferNum + "'";

            try
            {
                if (updatejj != "")
                    sqlTrans.ExecuteNonQuery(updatejj, CommandType.Text, null);
                foreach (var item in goods)
                {
                    string updategoods = " update BGOI_PP.dbo.TransferGoods set YesAmount='" + item.YesAmount + "',NoAmount='" + item.NoAmount + "' where id='" + item.ID + "'";

                    string updatesh = " update BGOI_PP.dbo.StorageDetailed  set ActualAmount = (select ActualAmount from BGOI_PP.dbo.StorageDetailed   where DID='" + item.Bak + "')-" + item.NoAmount + " where DID='" + item.Bak + "'";
                    string updatedd = " update BGOI_PP.dbo.OrderGoods set  ActualAmount=(select ActualAmount from BGOI_PP.dbo.OrderGoods where DID='" + item.Remark + "')-" + item.NoAmount + " where DID='" + item.Remark + "'";

                    sqlTrans.ExecuteNonQuery(updategoods, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatesh, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatedd, CommandType.Text, null);
                }
                sqlTrans.Close(true);
                return true;
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                return false;
            }

        }

        public static bool UpdatePeopleSC(PP_TransferS tran)
        {
            string sql = " update BGOI_PP.dbo.TransferS set productionPeople='" + tran.productionPeople + "'where TransferNum='" + tran.TransferNum + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public static bool UpdatePeopleJH(PP_TransferS tran)
        {
            string sql = " update BGOI_PP.dbo.TransferS set planPeople='" + tran.planPeople + "'where TransferNum='" + tran.TransferNum + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public static bool UpdateWarehouse(PP_TransferS tran)
        {
            string sql = " update BGOI_PP.dbo.TransferS set Warehouse='" + tran.Warehouse + "'where TransferNum='" + tran.TransferNum + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "MainPP");
            if (count > 0)
            {
                return true;
            }
            else
            { return false; }
        }
        #endregion
        #region [上传]
        public static int InsertBiddingNewS(tk_FileUpload fileUp, HttpFileCollection Filedata, ref string a_strErr)
        {
            a_strErr = "";
            string savePaths = "";
            int intInsertFile = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainPP");

            //获取上传文件的文件名
            string FileName = "";
            string savePath = "";
            string filename = Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;

            // 文件保存路径
            string path = System.Configuration.ConfigurationSettings.AppSettings["CG"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos + "\\" + fileUp.StrRID;
            path = path + savePaths;

            //如果不存在就创建file文件夹 
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            savePath = Path.Combine(path, FileName);

            //
            string strInsertFile = "";

            if (FileName != "")
            {
                if (File.Exists(savePath) == false)// 没有同名文件 
                {
                    Filedata[0].SaveAs(savePath);

                    strInsertFile = "insert into pp_File (PID,FileInfo,FileName,CreateTime,Validate,CreateUser) ";
                    strInsertFile += " values ('" + fileUp.StrRID + "','" + savePaths + "','" + FileName + "','" + fileUp.StrCreateTime + "','" + fileUp.StrValidate + "','" + fileUp.StrCreatePerson + "')";
                    intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                }
                else// 有同名文件 
                {
                    string strUpdate = "";
                    string strSel = " select count(*) from pp_File where PID='" + fileUp.StrRID + "' and FileName='" + FileName
                    + "' and CreateUser='" + account.UserName + "' and FileInfo='" + savePaths + "'  and Validate='v' ";
                    int count = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
                    if (count > 0)// 存在同一阶段同名的文件 则覆盖
                    {
                        savePath = Path.Combine(path, FileName);
                        Filedata[0].SaveAs(savePath);

                        strUpdate = " update pp_File set Validate='i' where PID='" + fileUp.StrRID + "' and FileInfo='" + savePaths + "' and FileName='" + FileName
                            + "' and Validate='v' and CreateUser='" + fileUp.StrCreatePerson + "' ";
                        sqlTrans.ExecuteNonQuery(strUpdate);
                        //
                        strInsertFile = "insert into pp_File (PID,FileName, FileInfo, CreateTime,Validate,CreateUser) ";
                        strInsertFile += " values ('" + fileUp.StrRID + " ,'" + FileName + "','" + savePaths + "','" + fileUp.StrCreateTime + "','" + fileUp.StrValidate + "','" + fileUp.StrCreatePerson + "')";
                        intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                    }
                    else // 存在同名文件 但是不同阶段 则更名上传
                    {
                        FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                        savePath = Path.Combine(path, FileName);
                        Filedata[0].SaveAs(savePath);
                        //
                        strInsertFile = "insert into pp_File (PID,FileName,FileInfo,CreateTime,Validate,CreateUser) ";
                        strInsertFile += " values ('" + fileUp.StrRID + "','" + FileName + "','" + savePaths + "','" + fileUp.StrCreateTime + "','" + fileUp.StrValidate + "','" + fileUp.StrCreatePerson + "')";
                        intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                    }
                }
            }

            try
            {
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                return -1;
            }

            return intInsertFile;
        }
        #endregion
    }
}
