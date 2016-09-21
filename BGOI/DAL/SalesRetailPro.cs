using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class SalesRetailPro
    {
        #region 零售销售
        public static string GetDHNO(string UnitID, string Sid, string TaskType)
        {
            string strDHNo = "";
            int MaxNum = 0;
            string sqlMaxNum = "select MaxNum from SIDRecord where SID='" + Sid + "' and DataRecord='" + DateTime.Now.ToString("yyMMdd") + "' and TaskType='" + TaskType + "' ";
            DataTable dtMaxNum = SQLBase.FillTable(sqlMaxNum, "SalesDBCnn");
            if (dtMaxNum != null && dtMaxNum.Rows.Count > 0)
            {
                MaxNum = int.Parse(dtMaxNum.Rows[0]["MaxNum"].ToString()) + 1; ;
            }
            else
                MaxNum = 1;

            strDHNo = "" + Sid + "" + DateTime.Now.ToString("yyMMdd") + "00" + UnitID + GFun.GetNum(MaxNum, 3);

            return strDHNo;
        }

        public static bool SaveSalesRecord(OrdersInfo order, Alarm alarm, List<Orders_DetailInfo> orderlist, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertList = "";
                string strDel = "delete from OrdersInfo where OrderID='" + order.OrderID + "' ";
                trans.ExecuteNonQuery(strDel, CommandType.Text, null);
                string strInsert = GSqlSentence.GetInsertInfoByD<OrdersInfo>(order, "OrdersInfo");
                string strAlarm = GSqlSentence.GetInsertInfoByD<Alarm>(alarm, "Alarm");
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                    if (strAlarm != "")
                        trans.ExecuteNonQuery(strAlarm, CommandType.Text, null);
                }
                string strDelList = "delete from Orders_DetailInfo where OrderID='" + order.OrderID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (orderlist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(orderlist, "Orders_DetailInfo");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
                UpdateMaxNum(order.OrderID, "DH", "Retail");
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool UpdateSalesRecord(OrdersInfo order, List<Orders_DetailInfo> orderlist, string LoginUser, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertHis = "insert into OrdersInfo_HIS(OrderID,UnitID,SalesType,ContractDate,ProjectName,SupplyTime,OrderUnit,OrderContactor,"
                    + "UseTel,UseAddress,ProvidManager,IsHK,HKRemark,Remark,CreateUser,CreateTime,Validate,State,NCreateTime,NCreateUser) "
                    + "select OrderID,UnitID,SalesType,ContractDate,ProjectName,SupplyTime,OrderUnit,OrderContactor,UseTel,UseAddress,ProvidManager,IsHK,HKRemark,Remark,CreateUser,CreateTime,Validate,State,"
                    + "'" + DateTime.Now.ToString() + "','" + LoginUser + "' from OrdersInfo where OrderID='" + order.OrderID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into Orders_DetailInfo_HIS(OrderID,DID,ProductID,Channel,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
                    + "OrderNum,TaxUnitPrice,UnitPrice,DUnitPrice,DTotalPrice,Price,Subtotal,DeliveryTime,State,Remark,BelongCom,CreateTime,CreateUser,Validate,NCreateUser,NCreateTime) "
                    + "select OrderID,DID,ProductID,Channel,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,TaxUnitPrice,UnitPrice,DUnitPrice,DTotalPrice,Price,Subtotal,"
                    + "DeliveryTime,State,Remark,BelongCom,CreateTime,CreateUser,Validate,'" + LoginUser + "','" + DateTime.Now.ToString() + "' from Orders_DetailInfo where OrderID='" + order.OrderID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                string strInsertList = "";
                string strUpdate = GSqlSentence.GetUpdateInfoByD<OrdersInfo>(order, "OrderID", "OrdersInfo");
                if (strUpdate != "")
                {
                    trans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                }
                string strDelList = "delete from Orders_DetailInfo where OrderID='" + order.OrderID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (orderlist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(orderlist, "Orders_DetailInfo");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool DeleteRecord(string OrderID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update OrdersInfo set Validate='i' where OrderID='" + OrderID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);
                string sqlUpdateDetail = "update Orders_DetailInfo set Validate='i' where OrderID='" + OrderID + "' ";
                trans.ExecuteNonQuery(sqlUpdateDetail, CommandType.Text, null);

                string strInsertHis = "insert into OrdersInfo_HIS(OrderID,SalesType,ContractDate,OrderUnit,"
                    + "ProvidManager,Remark,CreateUser,CreateTime,Validate,State,NCreateTime,NCreateUser) "
                    + "select OrderID,SalesType,ContractDate,OrderUnit,ProvidManager,Remark,CreateUser,CreateTime,Validate,State,"
                    + "'" + DateTime.Now.ToString() + "','" + LoginUser + "' from OrdersInfo where OrderID='" + OrderID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into Orders_DetailInfo_HIS(OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
                    + "OrderNum,TaxUnitPrice,UnitPrice,DUnitPrice,DTotalPrice,Price,Subtotal,DeliveryTime,State,Remark,BelongCom,Channel,CreateTime,CreateUser,Validate,NCreateUser,NCreateTime) "
                    + "select OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,TaxUnitPrice,UnitPrice,DUnitPrice,DTotalPrice,Price,Subtotal,"
                    + "DeliveryTime,State,Remark,BelongCom,Channel,CreateTime,CreateUser,Validate,'" + LoginUser + "','" + DateTime.Now.ToString() + "' from Orders_DetailInfo where OrderID='" + OrderID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetRetailApprovalGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Where2",where2)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetRetailApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }

        public static OrdersInfo GetOrderInfo(string OrderID)
        {
            OrdersInfo info = new OrdersInfo();
            // string sql = "select convert(varchar(10),ContractDate,23)as ContractDate,ProjectName,convert(varchar(10),SupplyTime,23)as  SupplyTime,OrderUnit,UseTel,OrderContactor,UseAddress,Remark,"
            //  + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=OrdersInfo.ProvidManager),State,IsHK,ISFinish,HKRemark from OrdersInfo where OrderID='" + OrderID + "' and Validate='v' and SalesType='Sa03' ";
            string sql = "select ContractDate,ProjectName,SupplyTime,OrderUnit,UseTel,OrderContactor,UseAddress,Remark,"
            + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=OrdersInfo.ProvidManager),State,IsHK,ISFinish,HKRemark,OrderTotal from OrdersInfo where OrderID='" + OrderID + "' and Validate='v' and SalesType='Sa03' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                info.OrderID = OrderID;
                info.ContractDate = Convert.ToDateTime(dt.Rows[0]["ContractDate"]);
                info.ProjectName = dt.Rows[0]["ProjectName"].ToString();
                info.SupplyTime = Convert.ToDateTime(dt.Rows[0]["SupplyTime"]);
                info.OrderUnit = dt.Rows[0]["OrderUnit"].ToString();
                info.OrderContactor = dt.Rows[0]["OrderContactor"].ToString();
                info.UseTel = dt.Rows[0]["UseTel"].ToString();
                info.UseAddress = dt.Rows[0]["UseAddress"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.ProvidManager = dt.Rows[0]["ProvidManager"].ToString();
                info.State = dt.Rows[0]["State"].ToString();
                info.IsHK = dt.Rows[0]["IsHK"].ToString();
                info.ISFinish = Convert.ToInt32(dt.Rows[0]["ISFinish"]);
                info.HKRemark = dt.Rows[0]["HKRemark"].ToString();
                if (dt.Rows[0]["OrderTotal"] != "")
                {
                    info.OrderTotal = Convert.ToDecimal(dt.Rows[0]["OrderTotal"]);
                }
            }

            return info;
        }

        public static DataTable GetOrderDetailInfo(string OrderID)
        {
            //  string sql = "select OrderContent,SpecsModels,OrderNum,UnitPrice,"
            //+ "DTotalPrice,BelongCom=(select Text from ProjectSelect_Config where ID=Orders_DetailInfo.BelongCom and Type='BelongCom'),"
            //+ "Channels=(select Text from ProjectSelect_Config where ID=Orders_DetailInfo.Channel and Type='ChannelsFrom'),Remark as txtRemark from Orders_DetailInfo where OrderID='" + OrderID + "' and Validate='v' ";
            string sql = "select ProductID,OrderContent,SpecsModels,OrderNum,UnitPrice,DTotalPrice,BelongCom=(select ID from tk_ConfigFiveMalls where ID=Orders_DetailInfo.BelongCom),Channels=(select ID from tk_ConfigFiveMalls where ID=Orders_DetailInfo.Channel ),Remark as txtRemark,TaxRate from Orders_DetailInfo where Validate='v' and OrderID ='" + OrderID + "'";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable GetUserListitem()
        {
            //string sql = "select UserId as ID,UserName as Text from UM_UserNew where roleNames like '%销售人员%' and DeptId='37' ";
            string sql = "select UserId as ID,UserName as Text from UM_UserNew where DeptId='37' ";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");

            return dt;
        }

        public static DataTable GetMallsListitem()
        {
            string sql = "select ID,UnitName as Text from tk_ConfigFiveMalls where ChildGrade='1' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static UIDataTable GetSalesRetailList(int a_intPageSize, int a_intPageIndex, string UnitId, string strWhere, string filed, string strWhere2)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            #region MyRegion
            //if (UnitId != "19")
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY OrderID desc) AS RowNumber,* from "
            //               + "(select distinct a.OrderID,"
            //               + "OrderContent = (stuff((select ',' + OrderContent from Orders_DetailInfo b where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=a.ProvidManager),CONVERT(varchar(100), a.ContractDate, 23) as ContractDate,"
            //               + "DTotalPrice = (stuff((select ',' + CONVERT(varchar(50),DTotalPrice) from Orders_DetailInfo b where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProductID = (stuff((select ',' + ProductID from Orders_DetailInfo where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "a.Remark,(case when a.IsHK='y' then '是' when a.IsHK='n' then '否' else '' end) as IsHK,State=(select StateDesc from ProjectState_Config p where p.StateId=a.State and StateType='Retail') from OrdersInfo a "
            //               + "where a.SalesType='Sa03' " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //               + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.OrderID) from OrdersInfo a where a.SalesType='Sa03' " + strWhere + " ";
            //}
            //else
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY OrderID desc) AS RowNumber,* from "
            //               + "(select distinct a.OrderID,"
            //               + "OrderContent = (stuff((select ',' + OrderContent from Orders_DetailInfo where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=a.ProvidManager),CONVERT(varchar(100), a.ContractDate, 23) as ContractDate,"
            //               + "DTotalPrice = (stuff((select ',' + CONVERT(varchar(50),DTotalPrice) from Orders_DetailInfo b where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProductID = (stuff((select ',' + ProductID from Orders_DetailInfo b where OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "a.Remark,(case when a.IsHK='y' then '是' when a.IsHK='n' then '否' else '' end) as IsHK,State=(select StateDesc from ProjectState_Config p where p.StateId=a.State and StateType='Retail') from OrdersInfo a,CopyApproval c "
            //               + "where a.SalesType='Sa03' and c.PID=a.OrderID " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //               + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.OrderID) from OrdersInfo a,CopyApproval c where a.SalesType='Sa03' and c.PID=a.OrderID " + strWhere + " ";
            //} 
            #endregion
            DataSet ds = SQLBase.FillDataSet("GetSalesRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                    if (instData.DtData != null)
                    {
                        for (int r = 0; r < instData.DtData.Rows.Count; r++)
                        {
                            instData.DtData.Rows[r]["ProvidManager"] = GetUserProvidManager(instData.DtData.Rows[r]["ProvidManager"].ToString());
                        }
                    }
                }
            }

            return instData;
        }


        public static string GetUserProvidManager(string UserID)
        {
            string sql = "select UserId as ID,UserName as Text from UM_UserNew where  DeptId='37' and UserId='" + UserID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");
            var s = "";
            if (dt == null) return null;
            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    s = dt.Rows[r]["Text"].ToString(); ;
                }
            }
            return s;
            //return dt;
        }
        public static DataTable GetRetailToPrint(string UnitId, string strWhere, string filed, string strWhere2)
        {
            #region MyRegion
            //string sql = "";
            //if (UnitId != "19")
            //{
            //    sql = "select distinct a.OrderID,"
            //               + "OrderContent = (stuff((select ',' + OrderContent from Orders_DetailInfo b where b.OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=a.ProvidManager),CONVERT(varchar(100), a.ContractDate, 23) as ContractDate,"
            //               + "DTotalPrice = (stuff((select ',' + CONVERT(varchar(50),DTotalPrice) from Orders_DetailInfo b where b.OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "a.Remark,(case when a.IsHK='y' then '是' when a.IsHK='n' then '否' else '' end) as IsHK,State=(select StateDesc from ProjectState_Config p where p.StateId=a.State and StateType='Retail') "
            //               + " from OrdersInfo a "
            //               + "where a.SalesType='Sa03' " + strWhere + " and 1=1 ";
            //}
            //else
            //{
            //    sql = "select distinct a.OrderID,"
            //               + "OrderContent = (stuff((select ',' + OrderContent from Orders_DetailInfo b where b.OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "ProvidManager=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=a.ProvidManager),CONVERT(varchar(100), a.ContractDate, 23) as ContractDate,"
            //               + "DTotalPrice = (stuff((select ',' + CONVERT(varchar(50),DTotalPrice) from Orders_DetailInfo b where b.OrderID = a.OrderID for xml path('')),1,1,'')),"
            //               + "a.Remark,(case when a.IsHK='y' then '是' when a.IsHK='n' then '否' else '' end) as IsHK,State=(select StateDesc from ProjectState_Config p where p.StateId=a.State and StateType='Retail') "
            //               + " from OrdersInfo a,CopyApproval c "
            //               + "where c.PID=a.OrderID and a.SalesType='Sa03' " + strWhere + " and 1=1 ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            DataTable dt = SQLBase.FillTable("GetSalesRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static UIDataTable GetDetailList(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select  Top " + a_intPageSize + " * "
                      + "from (Select distinct ROW_NUMBER() OVER (ORDER BY OrderID desc) AS RowNumber,"
                      + "OrderID,DID,OrderContent,SpecsModels,OrderNum,UnitPrice,DTotalPrice,Remark,BelongCom,Channel From Orders_DetailInfo where OrderID='" + OrderID + "' and 1=1 ) AS TEMPTABLE"
                      + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
                      + "select count(OrderID) FROM Orders_DetailInfo Where OrderID='" + OrderID + "' and 1=1 ";

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
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    // 11111
                    instData.DtData.Rows[r]["BelongCom"] = GetConfigChanel(instData.DtData.Rows[r]["BelongCom"].ToString());
                    instData.DtData.Rows[r]["Channel"] = GetConfigChanel(instData.DtData.Rows[r]["Channel"].ToString());
                    //GetConfigRetail

                }
            }
            return instData;
        }
        #endregion

        #region 内购管理
        public static bool SaveInternalRecord(tk_InternalOrder interOrder, List<tk_InternalDetail> detaillist, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertList = "";
                string strDel = "delete from InternalOrder where IOID='" + interOrder.IOID + "' ";
                trans.ExecuteNonQuery(strDel, CommandType.Text, null);
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_InternalOrder>(interOrder, "InternalOrder");
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                string strDelList = "delete from Internal_Detail where IOID='" + interOrder.IOID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (detaillist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(detaillist, "Internal_Detail");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
                if (interOrder.Type == "0")
                    UpdateMaxNum(interOrder.IOID, "NG", "Internal");
                else if (interOrder.Type == "1")
                    UpdateMaxNum(interOrder.IOID, "ZS", "Internal");
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool UpdateInternalRecord(tk_InternalOrder interOrder, List<tk_InternalDetail> detaillist, string LoginUser, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");

                string strInsertHis = "insert into InternalOrder_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from InternalOrder where IOID='" + interOrder.IOID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into Internal_Detail_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from Internal_Detail where IOID='" + interOrder.IOID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);
                string strInsertList = "";
                string strInsert = GSqlSentence.GetUpdateInfoByD<tk_InternalOrder>(interOrder, "IOID", "InternalOrder");
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                string strDelList = "delete from Internal_Detail where IOID='" + interOrder.IOID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (detaillist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(detaillist, "Internal_Detail");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetInternalList(int a_intPageSize, int a_intPageIndex, string UnitID, string strWhere, string filed, string strWhere2)
        {
            UIDataTable instData = new UIDataTable();
            #region MyRegion
            //string sql = "";
            //if (UnitID != "19")
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY IOID desc) AS RowNumber,* from "
            //               + "(select distinct a.IOID,"
            //               + "OrderContent = (stuff((select ',' + OrderContent from Internal_Detail b where a.IOID=b.IOID for xml path('')),1,1,'')),"
            //               + "a.OrderDate,Applyer=(select UserName from BJOI_UM..UM_UserNew u where u.UserId=a.Applyer),Warehouse=(select Text from ProjectSelect_Config p where p.ID=a.Warehouse and Type='Warehouse'),Steering,GoodsUser,"
            //               + "State=(select StateDesc from ProjectState_Config g where g.StateId=a.State and StateType='Retail') from InternalOrder a "
            //               + "where a.Validate='v' " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //               + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.IOID) from InternalOrder a where a.Validate='v' " + strWhere + " ";
            //}
            //else
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY IOID desc) AS RowNumber,* from "
            //             + "(select distinct a.IOID,"
            //             + "a.OrderDate,OrderContent = (stuff((select ',' + OrderContent from Internal_Detail b where a.IOID=b.IOID for xml path('')),1,1,'')),"
            //             + "Applyer=(select UserName from BJOI_UM..UM_UserNew u where u.UserId=a.Applyer),Warehouse=(select Text from ProjectSelect_Config p where p.ID=a.Warehouse and Type='Warehouse'),Steering,GoodsUser,"
            //             + "State=(select StateDesc from ProjectState_Config g where g.StateId=a.State and StateType='Retail') from InternalOrder a "
            //             + "where a.Validate='v' " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //             + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.IOID) from InternalOrder a,CopyApproval c where a.Validate='v' and c.PID=a.IOID " + strWhere + " ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitID),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            DataSet ds = SQLBase.FillDataSet("GetInternalRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }

            return instData;
        }

        public static DataTable GetInternalToPrint(string UnitId, string strWhere, string filed, string strWhere2)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            DataTable dt = SQLBase.FillTable("GetInternalRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static bool DeleteInternalApply(string IOID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update InternalOrder set Validate='i' where IOID='" + IOID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);
                string sqlUpdateDetail = "update Internal_Detail set Validate='i' where IOID='" + IOID + "' ";
                trans.ExecuteNonQuery(sqlUpdateDetail, CommandType.Text, null);

                string strInsertHis = "insert into InternalOrder_HIS(IOID,Warehouse,OrderDate,Amount,Applyer,ApplyTel,GoodsUser,"
                    + "UserTel,Address,Recipiments,Executives,Steering,Type,State,CreateUser,CreateTime,Validate,NCreateTime,NCreateUser) "
                    + "select IOID,Warehouse,OrderDate,Amount,Applyer,ApplyTel,GoodsUser,"
                    + "UserTel,Address,Recipiments,Executives,Steering,Type,State,CreateUser,CreateTime,Validate,"
                    + "'" + DateTime.Now.ToString() + "','" + LoginUser + "' from InternalOrder where IOID='" + IOID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into Internal_Detail_HIS(DID,IOID,ProductID,OrderContent,GoodsType,Specifications,Supplier,"
                    + "Unit,Amount,UnitPrice,Discounts,Total,Remark,State,CreateTime,CreateUser,Validate,NCreateUser,NCreateTime) "
                    + "select DID,IOID,ProductID,OrderContent,GoodsType,Specifications,Supplier,"
                    + "Unit,Amount,UnitPrice,Discounts,Total,Remark,State,CreateTime,CreateUser,Validate,'" + LoginUser + "','" + DateTime.Now.ToString() + "' from Internal_Detail_HIS where IOID='" + IOID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static tk_InternalOrder GetInternalOrder(string IOID, string op)
        {
            tk_InternalOrder order = new tk_InternalOrder();
            // string sql = "select CONVERT(varchar(100), OrderDate, 23) as OrderDate,Applyer=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=InternalOrder.Applyer),ApplyTel,"
            // + "GoodsUser,SendReason,UserTel,[Type],GoodsTotal,Address,Recipiments,Warehouse=(select Text from ProjectSelect_Config p where p.ID=InternalOrder.Warehouse and p.Type='WareHouse'),Steering,State from InternalOrder where IOID='" + IOID + "' and [Type]='" + op + "' and Validate='v'";
            string sql = "select  OrderDate,Applyer=(select UserName from BJOI_UM.dbo.UM_UserNew where UserId=InternalOrder.Applyer),ApplyTel,"
            + "GoodsUser,SendReason,UserTel,[Type],GoodsTotal,Address,Recipiments,Warehouse=(select Text from ProjectSelect_Config p where p.ID=InternalOrder.Warehouse and p.Type='WareHouse'),Steering,State from InternalOrder where IOID='" + IOID + "' and [Type]='" + op + "' and Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                order.IOID = IOID;
                order.OrderDate = Convert.ToDateTime(dt.Rows[0]["OrderDate"]);
                order.Applyer = dt.Rows[0]["Applyer"].ToString();
                order.ApplyTel = dt.Rows[0]["ApplyTel"].ToString();
                order.GoodsUser = dt.Rows[0]["GoodsUser"].ToString();
                order.SendReason = dt.Rows[0]["SendReason"].ToString();
                order.UserTel = dt.Rows[0]["UserTel"].ToString();
                order.Type = dt.Rows[0]["Type"].ToString();
                order.GoodsTotal = Convert.ToDecimal(dt.Rows[0]["GoodsTotal"]);
                order.Address = dt.Rows[0]["Address"].ToString();
                order.Recipiments = dt.Rows[0]["Recipiments"].ToString();
                order.Warehouse = dt.Rows[0]["Warehouse"].ToString();
                order.Steering = dt.Rows[0]["Steering"].ToString();
                order.State = dt.Rows[0]["State"].ToString();
            }

            return order;
        }

        public static DataTable GetInternalDetail(string IOID, string op)
        {
            string sql = "";
            if (op == "0")//"0"
            {
                sql = "select ProductID,OrderContent,GoodsType,Specifications,Amount,UnitPrice,Discounts,Total,PayWay=(select Text from ProjectSelect_Config a where a.Type='PayWay' and a.ID=Internal_Detail.PayWay ),"
                    + "Remark from Internal_Detail where IOID='" + IOID + "' and Validate='v' ";
            }
            else if (op == "1")//"1"
            {
                sql = "select ProductID,OrderContent,GoodsType,Specifications,Amount,UnitPrice,Discounts,Total,Remark from Internal_Detail where IOID='" + IOID + "' and Validate='v' ";
            }

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }


        public static DataTable GetPrintInternalDetail(string IOID, string op)
        {
            string sql = "";
            if (op == "NG")//"0"
            {
                sql = "select ProductID,OrderContent,GoodsType,Specifications,Amount,UnitPrice,Discounts,Total,PayWay=(select Text from ProjectSelect_Config a where a.Type='PayWay' and a.ID=Internal_Detail.PayWay ),"
                    + "Remark from Internal_Detail where IOID='" + IOID + "' and Validate='v' ";
            }
            else if (op == "ZS")//"1"
            {
                sql = "select ProductID,OrderContent,GoodsType,Specifications,Amount,UnitPrice,Discounts,Total,Remark from Internal_Detail where IOID='" + IOID + "' and Validate='v' ";
            }

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }
        public static UIDataTable GetInterDetailList(int a_intPageSize, int a_intPageIndex, string IOID)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select  Top " + a_intPageSize + " * "
                      + "from (Select distinct ROW_NUMBER() OVER (ORDER BY IOID desc) AS RowNumber,"
                      + "IOID,DID,OrderContent,GoodsType,Specifications,Amount,UnitPrice,Discounts,Total,PayWay=(select Text from ProjectSelect_Config a where Type='PayWay' and a.ID=Internal_Detail.PayWay),Remark From Internal_Detail where IOID='" + IOID + "' and 1=1 ) AS TEMPTABLE"
                      + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
                      + "select count(IOID) FROM Internal_Detail Where IOID='" + IOID + "' and 1=1 ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static UIDataTable GetInternalApprovalGrid(int a_intPageSize, int a_intPageIndex, string where, string where2)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@where2",where2)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetInternalApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }
        #endregion

        #region 共用方法
        public static void SaveLog(Sales_SalesLog salesLog)
        {
            string strInsert = GSqlSentence.GetInsertInfoByD<Sales_SalesLog>(salesLog, "SalesLog");

            SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
        }

        public static DataTable GetSelectUnit()
        {
            string sql = "select DeptId as ID,DeptName as Text from UM_UnitNew where SuperId='31' ";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");

            return dt;
        }

        /// <summary>
        /// 获取某一字段的值
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="TabName"></param>
        /// <param name="ReturnFiled"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public static string GetTaskFiled(string filed, string filedName, string TabName, string ReturnFiled, string strType, string TypeName)
        {
            string returnVal = "";
            string sql = "";
            if (strType != "" && TypeName != "")
            {
                sql = "select " + ReturnFiled + " from " + TabName + " where " + filedName + "='" + filed + "' and " + TypeName + "='" + strType + "' ";
            }
            else
                sql = "select " + ReturnFiled + " from " + TabName + " where " + filedName + "='" + filed + "' ";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
                returnVal = dt.Rows[0][0].ToString();

            return returnVal;
        }

        /// <summary>
        /// 金额转换大写
        /// </summary>
        /// <param name="strAmount"></param>
        /// <returns></returns>
        public static string MoneyToUpper(string strAmount)
        {
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (strAmount.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                strAmount = strAmount.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            strAmount = Math.Round(double.Parse(strAmount), 2).ToString();
            if (strAmount.IndexOf(".") > 0)
            {
                if (strAmount.IndexOf(".") == strAmount.Length - 2)
                {
                    strAmount = strAmount + "0";
                }
            }
            else
            {
                strAmount = strAmount + ".00";
            }
            strLower = strAmount;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }

        public static void UpdateMaxNum(string orderID, string Sid, string TaskType)
        {
            string sql = "";
            string UpNum = "";
            string strMaxNum = orderID.Substring(12);

            if (strMaxNum.Substring(0, 2) == "00")
            {
                UpNum = strMaxNum.Substring(2, 1);
            }
            else if (strMaxNum.Substring(0, 1) == "0" && strMaxNum.Substring(1, 1) != "0")
            {
                UpNum = strMaxNum.Substring(1, 2);
            }
            else if (strMaxNum.Substring(0, 1) != "0")
            {
                UpNum = strMaxNum;
            }

            if (UpNum == "1")
            {
                sql = "insert into SIDRecord(SID,MaxNum,DataRecord,TaskType) values('" + Sid + "','" + UpNum + "','" + DateTime.Now.ToString("yyMMdd") + "','" + TaskType + "') ";
            }
            else
            {
                sql = "update SIDRecord set MaxNum='" + UpNum + "' where SID='" + Sid + "' and DataRecord='" + DateTime.Now.ToString("yyMMdd") + "' and TaskType='" + TaskType + "' ";
            }

            int count = SQLBase.ExecuteNonQuery(sql, "SalesDBCnn");
        }

        public static DataTable GetCopyPerson(string unitId)
        {
            string sql = "select UserId,DeptId,UserName from UM_UserNew where DeptId='" + unitId + "' ";

            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");
            return dt;
        }

        public static string GetCopyNum(string UserID, string UnitId, string TaskType)
        {
            string sql = "select count(*) from CopyApproval where UserID='" + UserID + "' and TaskType='" + TaskType + "' ";
            int count = Convert.ToInt32(SQLBase.ExecuteScalar(sql, "SalesDBCnn"));

            string sqlU = "select count(*) from InternalOrder where UnitID='" + UnitId + "' and Validate='v' ";
            int count0 = Convert.ToInt32(SQLBase.ExecuteScalar(sqlU, "SalesDBCnn"));

            if (count > 0 && count0 <= 0)
                return "0";
            else
                return "-1";
        }

        public static DataTable GetCopyUnit()
        {
            string sql = "select DeptId as ID,DeptName as Text from UM_UnitNew where SuperId='31' and DeptName in ('综合管理部','公司领导') ";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");

            return dt;
        }

        public static bool SaveCopyPerson(string condition)
        {
            int count = SQLBase.ExecuteNonQuery(condition, "SalesDBCnn");

            if (count > 0)
                return true;
            else
                return false;
        }

        public static DataTable GetSelectListitem(string TaskType)
        {
            string sql = "select ID,Text from ProjectSelect_Config where [Type]='" + TaskType + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }

        public static DataTable GetStateDesc(string TaskType)
        {
            string sql = "select StateId as ID,StateDesc as Text from ProjectState_Config where StateType='" + TaskType + "' ";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable GetCompany()
        {
            string sql = "select ID,UnitName as Text from tk_ConfigFiveMalls where ChildGrade='0'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }

        public static DataTable GetSPInfo(string PID)
        {
            string sql = "select Job,ApprovalMan=(select UserName from BJOI_UM..UM_UserNew u where u.UserId=a.ApprovalMan),CONVERT(varchar(100), ApprovalTime, 23)ApprovalTime,Opinion,Remark,"
                  + " State=(select name from [BGOI_Project ]..tk_ConfigState b where a.State = b.StateId and b.Type = 'PSstate') from tk_Approval a where a.RelevanceID='" + PID + "' ";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }
        #endregion

        #region 专柜制作
        public static bool SaveShopeInfo(tk_ShoppeInfo shope, List<tk_ShoppeInfoDetail> detaillist, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertList = "";
                string strDel = "delete from ShoppeInfo where SIID='" + shope.SIID + "' ";
                trans.ExecuteNonQuery(strDel, CommandType.Text, null);
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_ShoppeInfo>(shope, "ShoppeInfo");
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                string strDelList = "delete from ShoppeInfoDetail where SIID='" + shope.SIID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (detaillist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(detaillist, "ShoppeInfoDetail");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
                UpdateMaxNum(shope.SIID, "ZG", "Shope");
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetShoppeList(int a_intPageSize, int a_intPageIndex, string UnitID, string strWhere, string filed)
        {
            //string sql = "";
            UIDataTable instData = new UIDataTable();
            #region MyRegion
            //if (UnitID != "19")
            //{
            //    sql = "Select  Top " + a_intPageSize + " * "
            //              + "from (Select distinct ROW_NUMBER() OVER (ORDER BY SIID desc) AS RowNumber,"
            //              + "SIID,Customer,Malls,ApplyReason=(select Text from ProjectSelect_Config p where p.ID=ShoppeInfo.ApplyReason and p.Type='ApplyReason'),"
            //                + "MakeType=(select Text from ProjectSelect_Config p where p.ID=ShoppeInfo.MakeType and p.Type='MakeType'),"
            //                + "UseYear,Budget,Applyer=(select UserName from BJOI_UM.dbo.UM_UserNew b where ShoppeInfo.Applyer=b.UserId),"
            //                + "CONVERT(varchar(100), ApplyTime, 23) as ApplyTime,"
            //              + "State=(select StateDesc from ProjectState_Config g where g.StateId=ShoppeInfo.State and StateType='Retail') "
            //              + " From ShoppeInfo where 1=1 and Validate='v' " + strWhere + " ) AS TEMPTABLE"
            //              + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //              + "select count(SIID) FROM ShoppeInfo Where 1=1 and Validate='v' " + strWhere + " ";
            //}
            //else
            //{
            //    sql = "Select  Top " + a_intPageSize + " * "
            //              + "from (Select distinct ROW_NUMBER() OVER (ORDER BY SIID desc) AS RowNumber,"
            //              + "SIID,Customer,Malls,ApplyReason,MakeType,UseYear,Budget,Applyer=(select UserName from BJOI_UM.dbo.UM_UserNew b where ShoppeInfo.Applyer=b.UserId),"
            //                + "CONVERT(varchar(100), ApplyTime, 23) as ApplyTime,"
            //              + "(case when State='0' then '新建' when State='1' then '审批中' when State='2' then '审批完成' else '' end) as State "
            //              + " From ShoppeInfo,CopyApproval c where 1=1 and c.PID=ShoppeInfo.SIID and Validate='v' " + strWhere + " ) AS TEMPTABLE"
            //              + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //              + "select count(SIID) FROM ShoppeInfo,CopyApproval c Where 1=1 and c.PID=ShoppeInfo.SIID and Validate='v' " + strWhere + " ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitID),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed)
            };

            DataSet ds = SQLBase.FillDataSet("GetShoppeRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static DataTable GetShoppeToPrint(string UnitId, string strWhere, string filed)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed)
            };

            DataTable dt = SQLBase.FillTable("GetShoppeRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static bool DeleteShoppeInfo(string SIID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update ShoppeInfo set Validate='i' where SIID='" + SIID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);
                string sqlUpdateDetail = "update ShoppeInfoDetail set Validate='i' where SIID='" + SIID + "' ";
                trans.ExecuteNonQuery(sqlUpdateDetail, CommandType.Text, null);

                string strInsertHis = "insert into ShoppeInfo_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from ShoppeInfo where SIID='" + SIID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into ShoppeInfoDetail_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from ShoppeInfoDetail where SIID='" + SIID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static DataTable GetMallInfoList(string SIID)
        {
            string sql = "select SIID,Malls,Customer,MallType,Phone,Address,ProductsOneName,SampleOneNum,ShoppeSize,"
                + "ProductsTwoName,ShoppeTwoSize,SampleNum,ShoppePosition,MonthSalesNum,SalesAmount from ShoppeInfo where SIID='" + SIID + "'";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static tk_ShoppeInfo GetShoppeInfo(string SIID)
        {
            tk_ShoppeInfo shoppe = new tk_ShoppeInfo();
            string sql = "select * from ShoppeInfo where SIID='" + SIID + "'";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            //if (dt != null)
            //{

            //    for (int r = 0; r < dt.Rows.Count; r++)
            //    {
            //        dt.Rows[r]["Applyer"] = GetUdserName(dt.Rows[r]["Applyer"].ToString(), dt.Rows[r]["UnitID"].ToString());
            //    }
            //}

            if (dt != null && dt.Rows.Count > 0)
            {
                shoppe = GSqlSentence.SetTValueD<tk_ShoppeInfo>(shoppe, dt.Rows[0]);
            }

            return shoppe;
        }

        public static tk_ShoppeInfo GetPrintShoppeInfo(string SIID)
        {
            tk_ShoppeInfo shoppe = new tk_ShoppeInfo();
            string sql = "select * from ShoppeInfo where SIID='" + SIID + "'";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null)
            {

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    dt.Rows[r]["Applyer"] = GetUdserName(dt.Rows[r]["Applyer"].ToString(), dt.Rows[r]["UnitID"].ToString());
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                shoppe = GSqlSentence.SetTValueD<tk_ShoppeInfo>(shoppe, dt.Rows[0]);
            }

            return shoppe;
        }

        public static string GetUdserName(string str, string unitid)
        {
            string sql = "select UserName from BJOI_UM.dbo.UM_UserNew b where b.UserId='" + str + "' and DeptId='" + unitid + "'";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");
            string s = "";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    s = dt.Rows[0]["UserName"].ToString();
                }
            }
            return s;
        }
        public static DataTable GetShoppeDetailInfo(string SIID)
        {
            string sql = "select ProductID,OrderContent,Specifications as SpecsModels,GoodsType,Amount,"
                + "Price,Discount,Total,Remark from ShoppeInfoDetail where SIID='" + SIID + "' and Validate='v' ";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static bool UpdateShoppeInfo(tk_ShoppeInfo shoppe, List<tk_ShoppeInfoDetail> shoppelist, string LoginUser, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertHis = "insert into ShoppeInfo_His "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from ShoppeInfo where SIID='" + shoppe.SIID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into ShoppeInfoDetail_His "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from ShoppeInfoDetail where SIID='" + shoppe.SIID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                string strInsertList = "";
                string strUpdate = GSqlSentence.GetUpdateInfoByD<tk_ShoppeInfo>(shoppe, "SIID", "ShoppeInfo");
                if (strUpdate != "")
                {
                    trans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                }
                string strDelList = "delete from ShoppeInfoDetail where SIID='" + shoppe.SIID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (shoppelist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(shoppelist, "ShoppeInfoDetail");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetShoppeApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetShoppeApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }
        #endregion

        #region 样机管理
        public static UIDataTable GetPrototypeList(int a_intPageSize, int a_intPageIndex, string strWhere, string UnitID, string filed, string strWhere2)
        {
            UIDataTable instData = new UIDataTable();
            #region MyRegion
            //string sql = "";
            //if (UnitID == "19")
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY PAID desc) AS RowNumber,* from "
            //               + "(select distinct a.PAID,"
            //               + "Sample = (stuff((select ',' + OrderContent from PrototypeDetail b where b.PAID = a.PAID and OperateType='0' for xml path('')),1,1,'')),"
            //               + "RevokeInfo = (stuff((select ',' + OrderContent from PrototypeDetail b where b.PAID = a.PAID and OperateType='1' for xml path('')),1,1,'')),a.Applyer,"
            //               + "CONVERT(varchar(100), a.ApplyDate, 23) as ApplyDate,"
            //               + "ProductID = (stuff((select ',' + ProductID from PrototypeDetail b where b.PAID = a.PAID for xml path('')),1,1,'')),Malls=(select UnitName from tk_ConfigFiveMalls where ID=a.Malls),"
            //               + "a.Explain,State=(select StateDesc from ProjectState_Config g where g.StateId=a.State and StateType='Retail') from PrototypeApply a,CopyApproval c "
            //               + "where a.Validate='v' and c.PID=a.PAID " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //               + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.PAID) from PrototypeApply a,CopyApproval c where a.Validate='v' and c.PID=a.PAID " + strWhere + " ";
            //}
            //else
            //{
            //    sql = "Select  Top " + a_intPageSize + " * from (Select ROW_NUMBER() OVER (ORDER BY PAID desc) AS RowNumber,* from "
            //               + "(select distinct a.PAID,"
            //               + "Sample = (stuff((select ',' + OrderContent from PrototypeDetail b where b.PAID = a.PAID and OperateType='0' for xml path('')),1,1,'')),"
            //               + "RevokeInfo = (stuff((select ',' + OrderContent from PrototypeDetail b where b.PAID = a.PAID and OperateType='1' for xml path('')),1,1,'')),a.Applyer,"
            //               + "CONVERT(varchar(100), a.ApplyDate, 23) as ApplyDate,"
            //               + "ProductID = (stuff((select ',' + ProductID from PrototypeDetail b where b.PAID = a.PAID for xml path('')),1,1,'')),Malls=(select UnitName from tk_ConfigFiveMalls where ID=a.Malls),"
            //               + "a.Explain,State=(select StateDesc from ProjectState_Config g where g.StateId=a.State and StateType='Retail') from PrototypeApply a "
            //               + "where a.Validate='v' " + strWhere + " and 1=1) as c ) AS TEMPTABLE "
            //               + "Where RowNumber>" + a_intPageSize * a_intPageIndex + "  ";
            //    sql += "select count(distinct a.PAID) from PrototypeApply a where a.Validate='v' " + strWhere + " ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitID),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strWhere),
                 new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            DataSet ds = SQLBase.FillDataSet("GetPrototypeRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }

            return instData;
        }


        public static DataTable GetPrototypeToPrint(string UnitId, string strWhere, string filed, string strWhere2)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed),
                new SqlParameter("@where2",strWhere2)
            };

            DataTable dt = SQLBase.FillTable("GetPrototypeRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static tk_Property GetPrototypeInfo(string PAID)
        {
            string sql = "select PAID,Applyer,ApplyDate,Malls=(select UnitName from tk_ConfigFiveMalls where ID=PrototypeApply.Malls),ExPlain,SampleNum,SampleAmount,RevokeNum,RevokeAmount,State from PrototypeApply where PAID='" + PAID + "' and Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            tk_Property tkPro = new tk_Property();
            if (dt != null && dt.Rows.Count > 0)
            {
                tkPro.PAID = PAID;
                tkPro.Applyer = dt.Rows[0]["Applyer"].ToString();
                tkPro.ApplyDate = dt.Rows[0]["ApplyDate"].ToString();
                //tkPro.IsFive = dt.Rows[0]["IsFive"].ToString();
                tkPro.Malls = dt.Rows[0]["Malls"].ToString();
                tkPro.ExPlain = dt.Rows[0]["ExPlain"].ToString();
                tkPro.SampleNum = Convert.ToDecimal(dt.Rows[0]["SampleNum"]);
                tkPro.SampleAmount = Convert.ToDecimal(dt.Rows[0]["SampleAmount"]);
                tkPro.RevokeNum = Convert.ToDecimal(dt.Rows[0]["RevokeNum"]);
                tkPro.RevokeAmount = Convert.ToDecimal(dt.Rows[0]["RevokeAmount"]);
                tkPro.State = dt.Rows[0]["State"].ToString();
            }

            return tkPro;
        }

        public static DataTable GetProtoDetail(string PAID, string OperateType)
        {
            string sql = "select ProductID,OrderContent,Ptype,Specifications as SpecsModels,Amount,UnitPrice,"
                + "Total,BusinessType,Remark from PrototypeDetail where PAID='" + PAID + "' and Validate='v' and OperateType='" + OperateType + "' ";

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static bool DeleteProtoInfo(string PAID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update PrototypeApply set Validate='i' where PAID='" + PAID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);
                string sqlUpdateDetail = "update PrototypeDetail set Validate='i' where PAID='" + PAID + "' ";
                trans.ExecuteNonQuery(sqlUpdateDetail, CommandType.Text, null);

                string strInsertHis = "insert into PrototypeApply_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from PrototypeApply where PAID='" + PAID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into PrototypeDetail_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from PrototypeDetail where PAID='" + PAID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool SavePrototype(tk_Property tkProto, List<tk_PropertyDetail> tkProlist, List<tk_PropertyDetail> tkProlist1, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertList = "";
                string strDel = "delete from PrototypeApply where PAID='" + tkProto.PAID + "' ";
                trans.ExecuteNonQuery(strDel, CommandType.Text, null);
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_Property>(tkProto, "PrototypeApply");
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                string strDelList = "delete from PrototypeDetail where PAID='" + tkProto.PAID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (tkProlist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(tkProlist, "PrototypeDetail");
                }

                if (strInsertList != "")
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                string strInsertList1 = "";
                if (tkProlist1.Count > 0)
                {
                    strInsertList1 = GSqlSentence.GetInsertByList(tkProlist1, "PrototypeDetail");
                }

                if (strInsertList1 != "")
                    trans.ExecuteNonQuery(strInsertList1, CommandType.Text, null);

                trans.Close(true);
                UpdateMaxNum(tkProto.PAID, "YJ", "ProtoType");
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool UpdateProtoType(tk_Property tkPro, List<tk_PropertyDetail> tkProlist, List<tk_PropertyDetail> tkProlist1, string LoginUser, ref string StrErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsertHis = "insert into PrototypeApply_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from PrototypeApply where PAID='" + tkPro.PAID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string strInsertDetailHis = "insert into PrototypeDetail_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from PrototypeDetail where PAID='" + tkPro.PAID + "' ";
                trans.ExecuteNonQuery(strInsertDetailHis, CommandType.Text, null);

                string strInsertList = "";
                string strUpdate = GSqlSentence.GetUpdateInfoByD<tk_Property>(tkPro, "PAID", "PrototypeApply");
                int i = 0, j = 0, k = 0;
                if (strUpdate != "")
                {
                    i = trans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                }
                string strDelList = "delete from PrototypeDetail where PAID='" + tkPro.PAID + "'";
                trans.ExecuteNonQuery(strDelList, CommandType.Text, null);
                if (tkProlist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(tkProlist, "PrototypeDetail");
                }

                if (strInsertList != "")
                    j = trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);

                string strInsertList1 = "";
                if (tkProlist1.Count > 0)
                {
                    strInsertList1 = GSqlSentence.GetInsertByList(tkProlist1, "PrototypeDetail");
                }

                if (strInsertList1 != "")
                    k = trans.ExecuteNonQuery(strInsertList1, CommandType.Text, null);
                if (i + j + k >= 3)
                {
                    trans.Close(true);
                }
                else
                {
                    trans.Close(false);
                }
            }
            catch (Exception ex)
            {
                trans.Close(false);
                StrErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetProtoDetailList(int a_intPageSize, int a_intPageIndex, string PAID, string Op)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select  Top " + a_intPageSize + " * "
                      + "from (Select distinct ROW_NUMBER() OVER (ORDER BY PAID desc) AS RowNumber,"
                      + "PAID,DID,OrderContent,Specifications as SpecsModels,Ptype,Supplier,Amount,"
                      + "UnitPrice,Total,BusinessType From PrototypeDetail where PAID='" + PAID + "' and Validate='v' and OperateType='" + Op + "' and 1=1 ) AS TEMPTABLE"
                      + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
                      + "select count(PAID) FROM PrototypeDetail Where PAID='" + PAID + "' and Validate='v' and OperateType='" + Op + "' and 1=1 ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static UIDataTable GetProtoApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetProtoApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }

        public static string GetSubBelongCom(ref string strErr)
        {
            string strJsonTree = "";
            GetSubComTree("Com00", null, ref strJsonTree);
            return "{\"Datas\":[" + strJsonTree + "]}";

        }

        public static DataTable GetConfigRetail(string ConfigType, string ChidGrade)
        {
            string sql = "select ID,UnitName as Text, ChildGrade from tk_ConfigFiveMalls where ChildGrade='" + ChidGrade + "' and HigherUnitID='" + ConfigType + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable GetConfigBelongCom(string ConfigType, string ChildGrade)
        {
            string sql = "select ID,UnitName as Text, ChildGrade from tk_ConfigFiveMalls where ChildGrade='" + ChildGrade + "' and ID='" + ConfigType + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static string GetConfigChanel(string ID)
        {
            string sql = "select UnitName from tk_ConfigFiveMalls where ID ='" + ID + "'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            string s = "";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    s = dt.Rows[0][0].ToString();
                }
            }
            return s;
        }


        public static UIDataTable GetBelongComSalesRetailList(int a_intPageSize, int a_intPageIndex, string strWhere, string MallID, string Grade)
        {
            DataTable dtSub = new DataTable();
            string sqlSub = "";
            string subMall = "";
            if (Grade == "1")
                subMall = "'" + MallID + "'";
            else if (Grade == "0")
            {
                //sqlSub = "select * from tk_ConfigSalesRetail where HigherUnitID='" + MallID + "' and ChildGrade='1' ";
                sqlSub = "select * from tk_ConfigFiveMalls where HigherUnitID='" + MallID + "' and ChildGrade='1' ";
            }
            else if (MallID == "Com00" && Grade == "")
            {
                // sqlSub = "select * from tk_ConfigSalesRetail where ChildGrade='1' ";
                //tk_ConfigFiveMalls
                sqlSub = "select * from tk_ConfigFiveMalls where ChildGrade='1' ";
            }
            dtSub = SQLBase.FillTable(sqlSub, "SalesDBCnn");
            if (dtSub != null && dtSub.Rows.Count > 0)
            {
                for (int i = 0; i < dtSub.Rows.Count; i++)
                {
                    subMall += "'" + dtSub.Rows[i]["ID"].ToString() + "'";
                    if (i < dtSub.Rows.Count - 1)
                        subMall += ",";
                }
            }
            string strChanel = "";
            if (subMall == "")
            {
                strChanel = "";
            }
            else
            {
                strChanel = " and a.Channel in(" + subMall + ")";
            }
            UIDataTable instData = new UIDataTable();


            string sql = "Select  Top " + a_intPageSize + " * from (Select distinct ROW_NUMBER() OVER (ORDER BY did desc) AS RowNumber," + "ProductID,OrderContent,a.SpecsModels,a.OrderNum,b.ProvidManager,a.BelongCom,a.Channel,UnitPrice,Total,UnitName " + " as Malls,a.Remark From Orders_DetailInfo a inner join OrdersInfo  b on a.OrderID =b.OrderID " +
              "inner join tk_ConfigFiveMalls c on c.ID=a.Channel where a.Validate='v'" + strChanel + " " + strWhere + " and 1=1 ) AS TEMPTABLE Where RowNumber>" + a_intPageIndex * a_intPageSize + "";
            sql += "select count(ProductID) From Orders_DetailInfo a inner join OrdersInfo b on a.OrderID=b.OrderID inner join tk_ConfigFiveMalls c on c.ID=a.Channel where a.Validate='v' " + strChanel + " " + strWhere + " and 1=1 ";
            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }

                if (instData.DtData != null)
                {
                    for (int r = 0; r < instData.DtData.Rows.Count; r++)
                    {
                        instData.DtData.Rows[r]["ProvidManager"] = GetUserProvidManager(instData.DtData.Rows[r]["ProvidManager"].ToString());
                    }
                }
            }

            return instData;
        }

        public static void GetSubComTree(string type, string parent, ref string a_strJsonTree)
        {
            string sql = "select ID,UnitName,ChildGrade from tk_ConfigSalesRetail where HigherUnitID='" + type + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strLevel = "";
                string strIsLeaf = "";
                string strChildGrade = "";
                if (dt.Rows[i]["ChildGrade"].ToString() == "0")
                {
                    strLevel = "0";
                    strIsLeaf = "false";
                    //strChildGrade = "0";
                }
                else if (dt.Rows[i]["ChildGrade"].ToString() == "1")//20160314k
                {
                    strLevel = "1";
                    strIsLeaf = "true";
                    //  strChildGrade = "1";
                }
                if (a_strJsonTree != "") a_strJsonTree += ",";
                a_strJsonTree += "{\"id\":\"" + dt.Rows[i]["UnitName"].ToString() + "\"," +
                   "\"UnitCode\":\"" + dt.Rows[i]["ID"].ToString() + "\"," +
                   "\"Grade\":\"" + dt.Rows[i]["ChildGrade"].ToString() + "\"," +
                   "\"level\":" + strLevel + "," +
                   "\"parent\":\"" + parent + "\"," +
                   "\"isLeaf\":" + strIsLeaf + "," +
                   "\"expanded\":false," +
                   "\"loaded\":true" +
                   "}";

                //string sqlChild = "select ID,Text,Type from ProjectSelect_Config where Type in ('ChannelsFrom')";
                //DataTable dtChild = SQLBase.FillTable(sqlChild, "SalesDBCnn");
                //if (dtChild.Rows.Count > 0)
                //{
                //    for (int k = 0; k < dtChild.Rows.Count; k++)
                //    {
                //        strLevel = "1";
                //        strIsLeaf = "true";
                //        strChildGrade = "1";
                //        parent = dt.Rows[i]["Text"].ToString();
                //        if (a_strJsonTree != "") a_strJsonTree += ",";
                //        a_strJsonTree += "{\"id\":\"" + dtChild.Rows[k]["Text"].ToString() + "\"," +
                //           "\"UnitCode\":\"" + dtChild.Rows[k]["ID"].ToString() + "\"," +
                //           "\"Grade\":\"" + strChildGrade + "\"," +
                //           "\"level\":" + strLevel + "," +
                //           "\"parent\":\"" + parent + "\"," +
                //           "\"isLeaf\":" + strIsLeaf + "," +
                //           "\"expanded\":false," +
                //           "\"loaded\":true" +
                //           "}";
                //    }
                //    parent = "";
                //}


                GetSubComTree(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["UnitName"].ToString(), ref a_strJsonTree);
            }
        }

        public static string GetSubMalls(ref string strErr)
        {
            string strJsonTree = "";
            GetSubTree("Com00", null, ref strJsonTree);
            return "{\"Datas\":[" + strJsonTree + "]}";
        }

        public static void GetSubTree(string unitId, string parent, ref string a_strJsonTree)
        {
            string sql = "select ID,UnitName,ChildGrade from tk_ConfigFiveMalls where HigherUnitID='" + unitId + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strLevel = "";
                string strIsLeaf = "";

                if (dt.Rows[i]["ChildGrade"].ToString() == "0")
                {
                    strLevel = "0";
                    strIsLeaf = "false";
                }
                else if (dt.Rows[i]["ChildGrade"].ToString() == "1")
                {
                    strLevel = "1";
                    strIsLeaf = "true";
                }

                if (a_strJsonTree != "") a_strJsonTree += ",";
                a_strJsonTree += "{\"id\":\"" + dt.Rows[i]["UnitName"].ToString() + "\"," +
                    //"\"UnitName\":\"" + dtnew.Rows[i]["UnitName"].ToString() + "\"," +
                   "\"UnitCode\":\"" + dt.Rows[i]["ID"].ToString() + "\"," +
                   "\"Grade\":\"" + dt.Rows[i]["ChildGrade"].ToString() + "\"," +
                   "\"level\":" + strLevel + "," +
                   "\"parent\":\"" + parent + "\"," +
                   "\"isLeaf\":" + strIsLeaf + "," +
                   "\"expanded\":false," +
                   "\"loaded\":true" +
                   "}";

                GetSubTree(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["UnitName"].ToString(), ref a_strJsonTree);
            }
        }

        public static UIDataTable GetFiveMallList(int a_intPageSize, int a_intPageIndex, string strWhere, string MallID, string Grade)
        {
            DataTable dtSub = new DataTable();
            string sqlSub = "";
            string subMall = "";
            if (Grade == "1")
                subMall = "'" + MallID + "'";
            else if (Grade == "0")
            {
                sqlSub = "select * from tk_ConfigFiveMalls where HigherUnitID='" + MallID + "' and ChildGrade='1' ";
            }
            else if (MallID == "Com00" && Grade == "")
            {
                sqlSub = "select * from tk_ConfigFiveMalls where ChildGrade='1' ";
            }
            dtSub = SQLBase.FillTable(sqlSub, "SalesDBCnn");
            if (dtSub != null && dtSub.Rows.Count > 0)
            {
                for (int i = 0; i < dtSub.Rows.Count; i++)
                {
                    subMall += "'" + dtSub.Rows[i]["ID"].ToString() + "'";
                    if (i < dtSub.Rows.Count - 1)
                        subMall += ",";
                }
            }

            UIDataTable instData = new UIDataTable();
            string sql = "Select  Top " + a_intPageSize + " * from (Select distinct ROW_NUMBER() OVER (ORDER BY ProductID desc) AS RowNumber,"
                       + "ProductID,OrderContent,Ptype,Specifications,Amount,BusinessType,"
                       + "UnitPrice,Total,UnitName as Malls,Remark From PrototypeDetail a "
                       + "inner join PrototypeApply b on a.PAID=b.PAID "
                       + "inner join tk_ConfigFiveMalls c on c.ID=b.Malls "
                       + "where a.Validate='v' and b.Malls in(" + subMall + ") " + strWhere + " and 1=1 ) AS TEMPTABLE Where RowNumber>" + a_intPageIndex * a_intPageSize + "";
            sql += "select count(ProductID) From PrototypeDetail a inner join PrototypeApply b on a.PAID=b.PAID "
                + "inner join tk_ConfigFiveMalls c on c.ID=b.Malls where a.Validate='v' and b.Malls in(" + subMall + ") " + strWhere + " and 1=1 ";
            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }

            return instData;
        }
        #endregion

        #region 促销活动
        public static UIDataTable GetPromotionList(int a_intPageSize, int a_intPageIndex, string UnitID, string strwhere, string filed)
        {
            //string sql = "";
            UIDataTable instData = new UIDataTable();
            #region MyRegion
            //if (UnitID != "19")
            //{
            //    sql = "Select Top " + a_intPageSize + " * "
            //              + "from (Select distinct ROW_NUMBER() OVER (ORDER BY PID desc) AS RowNumber,"
            //              + "PID,ActionTitle,ActionProject,Position,ActionStyle,PurPose,Applyer,Manager,Remark From PromotionInfo where Validate='v' " + strwhere + " and 1=1 ) AS TEMPTABLE"
            //              + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //              + "select count(PID) FROM PromotionInfo Where Validate='v' " + strwhere + " and 1=1 ";
            //}
            //else
            //{
            //    sql = "Select Top " + a_intPageSize + " * "
            //               + "from (Select distinct ROW_NUMBER() OVER (ORDER BY PID desc) AS RowNumber,"
            //               + "PID,ActionTitle,ActionProject,Position,ActionStyle,PurPose,Applyer,Manager,Remark From PromotionInfo,CopyApproval c where Validate='v' and c.PID=PromotionInfo.PID " + strwhere + " and 1=1 ) AS TEMPTABLE"
            //               + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //               + "select count(PID) FROM PromotionInfo,CopyApproval c Where Validate='v' and c.PID=PromotionInfo.PID " + strwhere + " and 1=1 ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitID),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strwhere),
                new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed)
            };

            DataSet ds = SQLBase.FillDataSet("GetPromotionRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }

            return instData;
        }

        public static DataTable GetPromotionToPrint(string UnitId, string strWhere, string filed)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed)
            };

            DataTable dt = SQLBase.FillTable("GetPromotionRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static UIDataTable GetPromotionApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetPromotionApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }

        public static bool AddPromotionInfo(tk_Promotion promotion, HttpFileCollection files, ref string a_strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_Promotion>(promotion, "PromotionInfo");
                trans.ExecuteNonQuery(strInsert);

                //string strInsertFile = "";
                //for (int i = 0; i < files.Count; i++)
                //{
                //    string FileName = "";
                //    byte[] fileByte = new byte[0];
                //    FileName = files[i].FileName.Substring(files[i].FileName.LastIndexOf('\\') + 1);
                //    int fileLength = files[i].ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        files[i].InputStream.Read(fileByte, 0, fileLength);
                //    }

                //    SqlParameter[] parms = new SqlParameter[]
                //    {
                //        new SqlParameter("@FileByte",fileByte)
                //    };
                //    strInsertFile = "insert into tk_RetailFile(PID,FileName,FileInfo,SalesType,CreateUser,CreateTime,Validate) "
                //        + "values('" + promotion.PID + "','" + FileName + "',@FileByte,'Promotion','" + promotion.CreateUser + "','" + promotion.CreateTime + "','" + promotion.Validate + "') ";

                //   // trans.ExecuteNonQuery(strInsertFile, parms);
                //    UpdateMaxNum(promotion.PID, "CX", "Promotion");
                //}
                UpdateMaxNum(promotion.PID, "CX", "Promotion");

                trans.Close(true);
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                trans.Close(false);
                throw;
            }
            return true;
        }

        public static bool UpdatePromotionInfo(tk_Promotion promotion, HttpFileCollection files, string LoginUser, ref string a_strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string InsertHIS = "insert into PromotionInfo_HIS select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from PromotionInfo where PID='" + promotion.PID + "' ";
                trans.ExecuteNonQuery(InsertHIS);

                string sql = GSqlSentence.GetUpdateInfoByD<tk_Promotion>(promotion, "PID", "PromotionInfo");
                trans.ExecuteNonQuery(sql);
                string InsertFileHIS = "insert into tk_RetailFile_HIS "
                    + "select *,'" + DateTime.Now + "','" + LoginUser + "' from tk_RetailFile where PID='" + promotion.PID + "' and SalesType='Promotion' ";
                trans.ExecuteNonQuery(InsertFileHIS);

                //if (files.Count > 0)
                //{
                //    string strDelFile = "delete from tk_RetailFile where PID='" + promotion.PID + "' and SalesType='Promotion' ";
                //    trans.ExecuteNonQuery(strDelFile);
                //}

                //string InsertFile = "";
                //for (int i = 0; i < files.Count; i++)
                //{
                //    string FileName = "";
                //    byte[] fileByte = new byte[0];
                //    FileName = files[i].FileName.Substring(files[i].FileName.LastIndexOf('\\') + 1);
                //    int fileLength = files[i].ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        files[i].InputStream.Read(fileByte, 0, fileLength);
                //    }

                //    SqlParameter[] param = new SqlParameter[]
                //    {
                //        new SqlParameter("@FileByte",fileByte)
                //    };
                //    if (fileByte.Length != 0)
                //    {
                //        // InsertFile = "insert into tk_RetailFile(PID,FileName,FileInfo,SalesType,CreateUser,CreateTime,Validate) "
                //        //  + "values('" + promotion.PID + "','" + FileName + "',@FileByte,'Promotion','" + promotion.CreateUser + "','" + promotion.CreateTime + "','" + promotion.Validate + "') ";

                //        InsertFile = " update tk_RetailFile set FileName ='" + FileName + "',FileInfo = convert(varbinary(max),@FileByte)  where PID='" + promotion.PID + "'"; //"update tk_RetailFile  set FileName='"+FileName+"',FileInfo=,SalesType,CreateUser,CreateTime,Validate) "
                //        //+ "values('" + promotion.PID + "','" + FileName + "',@FileByte,'Promotion','" + promotion.CreateUser + "','" + promotion.CreateTime + "','" + promotion.Validate + "') ";

                //        trans.ExecuteNonQuery(InsertFile, param);
                //    }
                //}

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                a_strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool DeletePromotionInfo(string PID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update PromotionInfo set Validate='i' where PID='" + PID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);

                string strInsertHis = "insert into PromotionInfo_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from PromotionInfo where PID='" + PID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string sqlUpFile = "Update tk_RetailFile set Validate='i' where PID='" + PID + "' and SalesType='Promotion' ";
                trans.ExecuteNonQuery(sqlUpdate);

                string InsertFile = "insert into tk_RetailFile_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from tk_RetailFile where PID='" + PID + "' ";
                trans.ExecuteNonQuery(InsertFile);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static DataTable GetPromotionFile(string PID, string FileName, string SalesType)
        {
            string sql = "";
            if (FileName == "")
            {
                sql = "select * from tk_RetailFile where PID='" + PID + "' and SalesType='" + SalesType + "' and Validate='v' ";
            }
            else
            {
                sql = "select * from tk_RetailFile where PID='" + PID + "' and FileName='" + FileName + "' and SalesType='" + SalesType + "' and Validate='v' ";
            }

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static tk_Promotion GetPromotionInfo(string PID)
        {
            tk_Promotion promotion = new tk_Promotion();
            string sql = "select * from PromotionInfo where PID='" + PID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                promotion = GSqlSentence.SetTValueD<tk_Promotion>(promotion, dt.Rows[0]);
            }

            return promotion;
        }
        #endregion

        #region 市场销售
        public static bool AddMarketInfo(tk_MarketSales market, HttpFileCollection files, ref string a_strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_MarketSales>(market, "MarketSales");
                trans.ExecuteNonQuery(strInsert);

                //string strInsertFile = "";
                //for (int i = 0; i < files.Count; i++)
                //{
                //    string FileName = "";
                //    byte[] fileByte = new byte[0];
                //    FileName = files[i].FileName.Substring(files[i].FileName.LastIndexOf('\\') + 1);
                //    int fileLength = files[i].ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        files[i].InputStream.Read(fileByte, 0, fileLength);
                //    }

                //    SqlParameter[] parms = new SqlParameter[]
                //    {
                //        new SqlParameter("@FileByte",fileByte)
                //    };
                //    strInsertFile = "insert into tk_RetailFile(PID,FileName,FileInfo,SalesType,CreateUser,CreateTime,Validate) "
                //        + "values('" + market.PID + "','" + FileName + "',@FileByte,'Market','" + market.CreateUser + "','" + market.CreateTime + "','" + market.Validate + "') ";

                //    trans.ExecuteNonQuery(strInsertFile, parms);
                //    UpdateMaxNum(market.PID, "SA", "Market");
                //}
                UpdateMaxNum(market.PID, "SA", "Market");

                trans.Close(true);
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                trans.Close(false);
                throw;
            }
            return true;
        }

        public static UIDataTable GetMarketList(int a_intPageSize, int a_intPageIndex, string UnitID, string strwhere, string filed)
        {
            //string sql = "";
            UIDataTable instData = new UIDataTable();
            #region MyRegion
            //if (UnitID != "19")
            //{
            //    sql = "Select Top " + a_intPageSize + " * "
            //              + "from (Select distinct ROW_NUMBER() OVER (ORDER BY PID desc) AS RowNumber,"
            //              + "PID,ApplyType=(select Text from ProjectSelect_Config p where p.ID=MarketSales.ApplyType),ApplyTitle,SELECT CONVERT(varchar(100), ApplyTime, 23) as ApplyTime,"
            //              + "Manager=(select * from BJOI_UM.dbo.UM_UserNew u where u.UserId=MarketSales.Manager),Remark,(case when State='0' then '新建' when State='1' then '审批中' when State='2' then'审批完成' else '' end) as State"
            //              + " From MarketSales where Validate='v' " + strwhere + " and 1=1 ) AS TEMPTABLE"
            //              + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //              + "select count(PID) FROM MarketSales Where Validate='v' " + strwhere + " and 1=1 ";
            //}
            //else
            //{
            //    sql = "Select Top " + a_intPageSize + " * "
            //               + "from (Select distinct ROW_NUMBER() OVER (ORDER BY PID desc) AS RowNumber,"
            //               + "PID,ApplyType=(select Text from ProjectSelect_Config p where p.ID=MarketSales.ApplyType),ApplyTitle,SELECT CONVERT(varchar(100), ApplyTime, 23) as ApplyTime,"
            //               + "Manager=(select * from BJOI_UM.dbo.UM_UserNew u where u.UserId=MarketSales.Manager),Remark,(case when State='0' then '新建' when State='1' then '审批中' when State='2' then'审批完成' else '' end) as State"
            //               + " From MarketSales,CopyApproval c where Validate='v' and c.PID=MarketSales.PID " + strwhere + " and 1=1 ) AS TEMPTABLE"
            //               + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " "
            //               + "select count(PID) FROM MarketSales,CopyApproval c Where Validate='v' and c.PID=MarketSales.PID " + strwhere + " and 1=1 ";
            //} 
            #endregion
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitID),
                new SqlParameter("@pageIndex",a_intPageIndex),
                new SqlParameter("@pageSize",a_intPageSize),
                new SqlParameter("@where",strwhere),
                new SqlParameter("@OutType","List"),
                new SqlParameter("@filed",filed)
            };

            DataSet ds = SQLBase.FillDataSet("GetMarketRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }

            return instData;
        }

        public static DataTable GetMarketToPrint(string UnitId, string strWhere, string filed)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UnitID",UnitId),
                new SqlParameter("@pageIndex","0"),
                new SqlParameter("@pageSize","0"),
                new SqlParameter("@where",strWhere),
                new SqlParameter("@OutType","Print"),
                new SqlParameter("@filed",filed)
            };

            DataTable dt = SQLBase.FillTable("GetMarketRetailInfo", CommandType.StoredProcedure, param, "SalesDBCnn");

            return dt;
        }

        public static tk_MarketSales GetMarketInfo(string PID)
        {
            tk_MarketSales market = new tk_MarketSales();
            string sql = "select * from MarketSales where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                market = GSqlSentence.SetTValueD<tk_MarketSales>(market, dt.Rows[0]);
            }

            return market;
        }

        public static bool UpdateMarketSalesInfo(tk_MarketSales market, HttpFileCollection files, string LoginUser, ref string a_strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string InsertHIS = "insert into MarketSales_HIS select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from MarketSales where PID='" + market.PID + "' ";
                trans.ExecuteNonQuery(InsertHIS);

                string sql = GSqlSentence.GetUpdateInfoByD<tk_MarketSales>(market, "PID", "MarketSales");
                trans.ExecuteNonQuery(sql);
                string InsertFileHIS = "insert into tk_RetailFile_HIS "
                    + "select *,'" + DateTime.Now + "','" + LoginUser + "' from tk_RetailFile where PID='" + market.PID + "' and SalesType='Market' ";
                trans.ExecuteNonQuery(InsertFileHIS);

                //if (files.Count > 0)
                //{
                //    string strDelFile = "delete from tk_RetailFile where PID='" + market.PID + "' and SalesType='Market' ";
                //    trans.ExecuteNonQuery(strDelFile);
                //}

                //string strInsertFile = "";
                //for (int i = 0; i < files.Count; i++)
                //{
                //    string FileName = "";
                //    byte[] fileByte = new byte[0];
                //    FileName = files[i].FileName.Substring(files[i].FileName.LastIndexOf('\\') + 1);
                //    int fileLength = files[i].ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        files[i].InputStream.Read(fileByte, 0, fileLength);
                //    }

                //    SqlParameter[] parms = new SqlParameter[]
                //    {
                //        new SqlParameter("@FileByte",fileByte)
                //    };
                //    // strInsertFile = "insert into tk_RetailFile(PID,FileName,FileInfo,SalesType,CreateUser,CreateTime,Validate) "
                //    //    + "values('" + market.PID + "','" + FileName + "',@FileByte,'Market','" + market.CreateUser + "','" + market.CreateTime + "','" + market.Validate + "') ";
                //    if (fileByte.Length != 0)
                //    {
                //        strInsertFile = " update tk_RetailFile set FileName ='" + FileName + "',FileInfo = convert(varbinary(max),@FileByte)  where PID='" + market.PID + "'";

                //        trans.ExecuteNonQuery(strInsertFile, parms);
                //    }
                //}
                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                a_strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static bool DeleteMarket(string PID, string LoginUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                trans.Open("SalesDBCnn");
                string sqlUpdate = "update MarketSales set Validate='i' where PID='" + PID + "'";
                trans.ExecuteNonQuery(sqlUpdate, CommandType.Text, null);

                string strInsertHis = "insert into MarketSales_HIS "
                    + "select *, '" + DateTime.Now.ToString() + "','" + LoginUser + "' from MarketSales where PID='" + PID + "' ";
                trans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);

                string sqlUpFile = "Update tk_RetailFile set Validate='i' where PID='" + PID + "' and SalesType='Market' ";
                trans.ExecuteNonQuery(sqlUpdate);

                string InsertFile = "insert into tk_RetailFile_HIS "
                    + "select *,'" + DateTime.Now.ToString() + "','" + LoginUser + "' from tk_RetailFile where PID='" + PID + "' ";
                trans.ExecuteNonQuery(InsertFile);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(false);
                strErr = ex.Message;
                throw;
            }
            return true;
        }

        public static UIDataTable GetMarketApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetMarketApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }

            return instData;
        }
        #endregion

        #region 销售提醒
        public static UIDataTable GetRemindList(int a_intPageSize, int a_intPageIndex, string UnitName, string SalesType)
        {
            string sql = "";
            UIDataTable instData = new UIDataTable();
            sql = "Select  Top " + a_intPageSize + " * "
                      + "from (Select distinct ROW_NUMBER() OVER (ORDER BY a.PID desc) AS RowNumber,"
                      + "a.PID,b.ContractID,CONVERT(varchar(100),SignTime, 23) as SignTime,LogContent,ProductType,Actor,Unit "
                      + " From SalesLog  a left join OrdersInfo b on a.PID=b.OrderID Where 1=1 and Unit='" + UnitName + "' and a.SalesType='" + SalesType + "' and ProductType ='订单新增' and datediff(day,GETDATE(),SignTime)<7  ) AS TEMPTABLE"
                      + " Where RowNumber>" + a_intPageIndex * a_intPageSize + " order by ProductType desc "
                      + "select count(PID) FROM SalesLog Where 1=1 and Unit='" + UnitName + "' and SalesType='" + SalesType + "' and ProductType ='订单新增' and datediff(day,GETDATE(),SignTime)<7 ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static DataTable GetNowRemindInfo(string UnitName, string SalesType, string UserId)
        {
            string sql = "";
            if (SalesType == "Retail")
            {
                sql = "select Top 2 PID,TaskType from CopyApproval where UserID='" + UserId + "' order by CreateTime desc ";
            }
            else if (SalesType == "Project")
            {
                sql = "select Top 3 (Unit+' ： '+CONVERT(varchar(100),SignTime, 23)) as SignContent from SalesLog where SalesType='" + SalesType + "' and Unit='" + UnitName + "' and ProductType ='订单新增'  and datediff(day,GETDATE(),SignTime)<7 order by LogTime desc ";
            }

            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        //获取库存状态
        public static DataTable GetTopRetailLibraryTubeManage(string UserId)
        {
            string sql = "     select OrderID, OperationContent, Operator,OperationTime from BGOI_Inventory.dbo.InAlarm   a,(SELECT Orderid  as dh, MAX(OperationTime) as date FROM   BGOI_Inventory.dbo.InAlarm  GROUP BY OrderID )  b where a.OrderID = b.dh and a.OperationTime=b.date";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }
        //获取售后状态、
        public static DataTable GetTopRetailAfterSaleManage(string UserId)
        {
            string sql = "select OrderID, OperationContent, Operator,OperationTime from BGOI_CustomerService.dbo.CSAlarm  a,(SELECT Orderid  as dh, MAX(OperationTime) as date FROM   BGOI_CustomerService.dbo.CSAlarm GROUP BY OrderID )  b where a.OrderID = b.dh and a.OperationTime=b.date";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }
        #endregion


        #region 库存和售后关联
        public static UIDataTable GetSalesRetailLibraryTubeGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetSalesRetailLibraryTubeGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["ProvidManager"] = GetUserProvidManager(instData.DtData.Rows[r]["ProvidManager"].ToString());
                }
            }

            return instData;
        }


        public static UIDataTable GetSalesRetailAfterSaleGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetSalesRetailAfterSaleGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order != null)
            {
                if (DO_Order.Tables.Count > 0)
                {
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
                    instData.DtData = DO_Order.Tables[0];
                }
            }
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["ProvidManager"] = GetUserProvidManager(instData.DtData.Rows[r]["ProvidManager"].ToString());
                }
            }
            return instData;
        }
        #endregion

        #region 系统设置
        public static UIDataTable GetBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select Top " + a_intPageSize + " * "
                        + "from (Select distinct ROW_NUMBER() OVER (ORDER BY XID asc) AS RowNumber,XID, Text,"
                        + "Type "
                        + "from ProjectSelect_Config "
                        + "where 1=1 " + where + " ) AS TEMPTABLE Where RowNumber>" + a_intPageSize * a_intPageIndex + " "
                      + "Select count (*) from ProjectSelect_Config where 1=1 " + where + "  ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static bool AddBasicInfo(string typeId, string textDesc)
        {
            int XID = 0;
            string ID = "";
            string SID = "";
            string sql = "select Top 1 * from ProjectSelect_Config where Type='" + typeId + "' order by XID desc ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                XID = Convert.ToInt32(dt.Rows[0]["XID"]) + 1;
                SID = dt.Rows[0]["ID"].ToString();
                ID = SID.Substring(0, SID.Length - 1) + XID;
            }

            string strInsert = "insert into ProjectSelect_Config(XID,ID,Text,Type) values(" + XID + ",'" + ID + "','" + textDesc + "','" + typeId + "') ";
            int count = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;

        }

        public static bool AlterBasicInfo(string XID, string Type, string Text)
        {
            string sql = "Update ProjectSelect_Config set Text='" + Text + "' where XID=" + XID + " and Type='" + Type + "' ";
            int count = SQLBase.ExecuteNonQuery(sql, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;

        }

        public static bool DeleteBasicInfo(string XID, string Type)
        {
            string sql = "delete from ProjectSelect_Config where Type='" + Type + "' and XID=" + XID + " ";
            int count = SQLBase.ExecuteNonQuery(sql, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取5S店
        /// </summary>
        /// <param name="a_intPageSize"></param>
        /// <param name="a_intPageIndex"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static UIDataTable GetMallsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select Top " + a_intPageSize + " * "
                        + "from (Select distinct ROW_NUMBER() OVER (ORDER BY ID asc) AS RowNumber,ID,"
                        + "Malls=(select UnitName from tk_ConfigFiveMalls b where b.ID=a.ID and b.ChildGrade='1'),"
                        + "UnitName=(select UnitName from tk_ConfigFiveMalls b where b.ID=a.HigherUnitID and b.ChildGrade='0'),ChildGrade,HigherUnitID "
                        + "from tk_ConfigFiveMalls a where a.HigherUnitID!='Com00' " + where + " "
                        + " " + where + " ) AS TEMPTABLE Where RowNumber>" + a_intPageSize * a_intPageIndex + " "
                      + "Select count (*) from tk_ConfigFiveMalls a where a.HigherUnitID!='Com00' " + where + "  ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
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
                    instData.DtData = ds.Tables[0];
                }
            }
            return instData;
        }

        public static bool InsertMallsInfo(string HigherUnitID, string Malls)
        {
            string sql = "select top 1 * from tk_ConfigFiveMalls where ChildGrade='1' order by ID desc";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            int id = 0;
            string mallsID = "";
            string sid = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                mallsID = dt.Rows[0]["ID"].ToString();
                mallsID = mallsID.Substring(2, 2);
                id = Convert.ToInt32(mallsID);
                int newId = id + 1;
                if (id < 9)
                    sid = "Ma0" + newId;
                else if (id > 9)
                    sid = "Ma" + newId;
            }
            string strInsert = "insert tk_ConfigFiveMalls(ID,UnitName,ChildGrade,HigherUnitID) "
                + "values('" + sid + "','" + Malls + "','1','" + HigherUnitID + "')";

            int count = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;

        }

        public static bool UpdateMallsInfo(string ID, string HigherUnitID, string Malls)
        {
            string sql = "update tk_ConfigFiveMalls set UnitName='" + Malls + "' where ID='" + ID + "' and HigherUnitID='" + HigherUnitID + "'";
            int count = SQLBase.ExecuteNonQuery(sql, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;
        }

        public static bool DeleteMalls(string ID, string HigherUnitID)
        {
            string sql = "delete from tk_ConfigFiveMalls where ID='" + ID + "' and HigherUnitID='" + HigherUnitID + "' ";
            int count = SQLBase.ExecuteNonQuery(sql, "SalesDBCnn");
            if (count > 0)
                return true;
            else
                return false;

        }
        #endregion

        #region 统计分析
        public static DataSet GetWeekStatics(string strWhere)
        {
            string sql = "select ProvidManager=(select UserName from BJOI_UM..UM_UserNew c Where c.UserId=b.ProvidManager),"
                       + "UnitName=(select DeptName from BJOI_UM..UM_UnitNew u where u.DeptId=b.UnitID),a.UnitPrice,a.DUnitPrice,a.DTotalPrice from Orders_DetailInfo a,OrdersInfo b "
                       + "where a.OrderID=b.OrderID and b.SalesType='Sa03' and b.Validate='v' " + strWhere + " ";
            sql += "select SUM(a.UnitPrice) as UnitPrice,SUM(a.DUnitPrice) as DUnitPrice,SUM(a.DTotalPrice)as DTotalPrice from Orders_DetailInfo a,OrdersInfo b "
                       + "where a.OrderID=b.OrderID and b.SalesType='Sa03' and b.Validate='v' " + strWhere + " ";

            DataSet ds = SQLBase.FillDataSet(sql, CommandType.Text, null, "SalesDBCnn");

            return ds;
        }

        /// <summary>
        /// 内购产品统计
        /// </summary>
        /// <param name="a_strWhere"></param>
        /// <returns></returns>
        public static UIDataTable GetDetailGrid(string a_strWhere)
        {
            UIDataTable instData = new UIDataTable();
            string sql = "Select a.IOID,a.DID,a.OrderContent,a.GoodsType,a.Specifications,a.Amount,a.UnitPrice,a.Discounts,a.Total,"
                + " PayWay=(select Text from ProjectSelect_Config c where c.Type='PayWay' and c.ID=a.PayWay),a.Remark,"
                + "a.IsHK From Internal_Detail a,InternalOrder b where a.IOID=b.IOID " + a_strWhere + "";

            DataTable dt = SQLBase.FillTable(sql, CommandType.Text, null, "SalesDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                instData.IntRecords = dt.Rows.Count;
                instData.IntTotalPages = 1;
                instData.DtData = dt;
            }
            return instData;
        }

        public static bool AlterInternalDetail(string IOID, string DID, string UserName, ref string a_strErr)
        {
            SQLTrans trans = new SQLTrans();

            try
            {
                trans.Open("SalesDBCnn");
                string strInsertHIS = "insert into Internal_Detail_HIS select * ,'" + DateTime.Now.ToString() + "','" + UserName + "' "
                    + "FROM Internal_Detail where IOID='" + IOID + "' and DID='" + DID + "' ";
                trans.ExecuteNonQuery(strInsertHIS);

                string sqlAlter = "update Internal_Detail set IsHK='y' where IOID='" + IOID + "' and DID='" + DID + "' ";
                trans.ExecuteNonQuery(sqlAlter);

                trans.Close(true);
            }
            catch (Exception ex)
            {
                trans.Close(true);
                a_strErr = ex.Message;
                throw;
            }

            return true;
        }
        #endregion
    }
}
