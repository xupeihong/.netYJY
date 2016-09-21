using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
using System.IO;
namespace TECOCITY_BGOI
{
    public class InventoryPro
    {
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="tableName">表名</param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable PrintList(string strWhere, string tableName, ref string strErr)
        {
            String strField = "select  * from " + tableName + " " + strWhere + "";
            DataTable dt = SQLBase.FillTable(strField, "SalesDBCnn");
            return dt;
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="tableName">表名</param>
        /// <param name="FieldName">字段名</param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static DataTable ToExcel(string strWhere, string tableName, string FieldName, string OrderBy, ref string strErr)
        {
            String strField = "select  " + FieldName + ""
                + "from " + tableName + " where  " + strWhere + " order by " + OrderBy + "";
            DataTable dt = SQLBase.FillTable(strField, "MainInventory");
            return dt;

        }
        //可选可添规格下拉框
        public static DataTable getSpecOptionalAdd(string UnitID, string Spec)
        {
            string str = "select distinct Spec from BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b where a.Spec like '%" + Spec + "%' and  a.Ptype=b.ID and Spec!=''and b.UnitID='47'  ";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
        }

        //添加日志
        public static bool AddInventLog(tk_Inventorylog logobj)
        {
            int count = 0;
            string strInsert = GSqlSentence.GetInsertInfoByD(logobj, " [BGOI_Inventory].[dbo].[tk_Inventorylog]");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
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
        #region [创建库房]
        public static bool SaveInventoryAddFirstPage(tk_WareHouse rem, ref string strErr)
        {
            string sql = " select * from BGOI_Inventory.dbo.tk_WareHouse where HouseName='" + rem.HouseName + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            if (dt == null || dt.Rows.Count < 1)
            {
                strErr = "";
                int count = 0;
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_WareHouse>(rem, "[BGOI_Inventory].[dbo].tk_WareHouse");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "添加失败";
                    return false;
                }
            }
            else
            {
                strErr = "该仓库名已存在";
                return false;
            }


        }
        //验证仓库名称
        public static DataTable GetSupCode()
        {
            string sql = "select HouseName from dbo.tk_WareHouse ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #region [调拨单]
        public static UIDataTable AllocationSheetList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_AllocationSheet a " +
                                 " left join BGOI_Inventory.dbo.tk_WareHouse b on a.Handlers=b.UnitID   " +
                                 "  left join BGOI_Inventory.dbo.tk_WareHouse e on  a.Handlers=e.HouseID  where  " + where;
            //" left join BGOI_Inventory.dbo.tk_AllocationSheetDetailed c on a.ID=c.DBID " +
            //" left join BGOI_Inventory.dbo.tk_ProductInfo d on c.ProductID=d.PID and " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " " + where;
            string strOrderBy = "a.ID ";
            String strTable = " BGOI_Inventory.dbo.tk_AllocationSheet a " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse b on a.UserID=b.HouseID " +
                                "  left join BGOI_Inventory.dbo.tk_WareHouse e on  a.Handlers=e.HouseID ";
            //" left join BGOI_Inventory.dbo.tk_AllocationSheetDetailed c on a.ID=c.DBID " +
            //" left join BGOI_Inventory.dbo.tk_ProductInfo d on c.ProductID=d.PID  ";
            String strField = " a.ID, a.CreateUnitID, a.Inspector, a.Handlers, a.UserID, a.Remark, a.ReasonRemark, a.CreateUser, a.CreateTime, a.Validate," +
                              "  b.HouseName as 'RK',e.HouseName AS  'CK' ";
            //" c.DBID, c.DID, c.ProductID, c.OrderContent, c.SpecsModels, d.Units, c.OrderNum, c.NoTaxuUnit, c.TaxUnitPrice, c.NOPrice, c.Price, c.State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                //for (int r = 0; r < instData.DtData.Rows.Count; r++)
                //{
                //    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                //    {
                //        instData.DtData.Rows[r]["State"] = "新建";
                //    }
                //    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                //    {
                //        instData.DtData.Rows[r]["State"] = "调拨完成";
                //    }
                //}
            }
            return instData;
        }
        public static bool SaveAllocationSheet(tk_AllocationSheet record, List<tk_AllocationSheetDetailed> delist, string Count, ref string strErr)
        {
            strErr = "";
            int count = 0;
            foreach (tk_AllocationSheetDetailed SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.Handlers + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    else
                    {
                        int Cnum = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(Count);
                        string strUpOut = "";
                        strUpOut = "update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + Cnum + "' where HouseID='" + record.Handlers + "' and ProductID='" + SID.ProductID + "'";
                        //出库仓库减数量
                        if (SQLBase.ExecuteNonQuery(strUpOut) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改出库仓库数量','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + SID.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改出库仓库数量','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + SID.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                }
            }
            string strInsert = GSqlSentence.GetInsertInfoByD<tk_AllocationSheet>(record, "[BGOI_Inventory].[dbo].tk_AllocationSheet");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }
            string strInsertList = "";
            if (delist.Count > 0)
            {
                strInsertList = GSqlSentence.GetInsertByList(delist, "BGOI_Inventory.dbo.tk_AllocationSheetDetailed");

                #region [修改入库]
                DataTable Upstr = SQLBase.FillTable("select FinishCount FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where HouseID='" + record.UserID + "'");
                int strgn = 0;
                int strgnum = 0;
                foreach (DataRow dedrt in Upstr.Rows)
                {
                    strgnum = Convert.ToInt32(dedrt["FinishCount"]);
                }
                strgn = Convert.ToInt32(strgnum + Count);
                foreach (tk_AllocationSheetDetailed dedr in delist)
                {
                    if (Upstr.Rows.Count > 0)
                    {
                        //修改
                        string strUp = "update  [BGOI_Inventory].[dbo].[tk_StockRemain] set  FinishCount='" + strgn + "' where HouseID='" + record.Handlers + "'";
                        if (SQLBase.ExecuteNonQuery(strUp) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改入库仓库数量','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + dedr.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改入库仓库数量','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + dedr.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    else
                    {
                        //添加
                        string strIn = "Insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID, FinishCount, OnlineCount, HouseID, UsableStock, Costing, Location, ProtoCount, DefectCount, CompleteCount, HalfCount)" +
                                       "values('" + dedr.ProductID + "','" + strgn + "','','" + record.UserID + "','','','','','','','')";
                        if (SQLBase.ExecuteNonQuery(strIn) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加入库仓库数量','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + dedr.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加入库仓库数量','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockRemain','" + dedr.ProductID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                }
                #endregion

                if (strInsertList != "")
                {
                    if (SQLBase.ExecuteNonQuery(strInsertList) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加调拨单详细','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_AllocationSheetDetailed','" + record.ID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加调拨单详细','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_AllocationSheetDetailed','" + record.ID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
                return true;
            }
            else
                return false;
        }
        //根据用户id加载上级下部门（后期在看）
        public static DataTable GetDepName()
        {
            string SuperId = "";
            string DeptId = GAccount.GetAccountInfo().UnitID;
            string strdrt = "select SuperId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + DeptId + "'";
            DataTable drt = SQLBase.FillTable(strdrt, "MainInventory");
            foreach (DataRow dr in drt.Rows)
            {
                SuperId = dr["SuperId"].ToString();
            }
            string str = "select DeptId,DeptName from BJOI_UM.dbo.UM_UnitNew where SuperId='" + SuperId + "' or  SuperId='" + DeptId + "'";//根据用户id加载上级下部门和旗下部门
            // string str = "select DeptId,DeptName from BJOI_UM.dbo.UM_UnitNew where SuperId='" + SuperId + "'";//根据用户id加载上级下部
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //加载仓库类型
        public static DataTable GetHouseType()
        {
            string str = "select ID,Text from BGOI_Inventory.dbo.tk_ConfigProType";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //加载仓库ID
        public static string HouserID()
        {
            string strID = "";
            string strD = "H";
            string strSqlID = "select max(HouseID) from BGOI_Inventory.dbo.tk_WareHouse";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "00001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));
                    if (num < 9)
                        strD = strD + "0000" + (num + 1);
                    else if (num < 99 && num >= 9)
                        strD = strD + "000" + (num + 1);
                    else
                        strD = strD + (num + 1);
                }
            }
            else
            {
                strD = strD + "001";
            }
            return strD;


        }
        //根据用户id加载部门（当前部门与旗下部门）
        public static DataTable GetDepNameDQ()
        {
            string DeptId = GAccount.GetAccountInfo().UnitID;
            string str = "select DeptId,DeptName from BJOI_UM.dbo.UM_UnitNew where DeptId='" + DeptId + "' or SuperId='" + DeptId + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //根加载一级仓库
        public static DataTable GetOneHouse(string oneHouserID)
        {
            string str = " select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and  TypeID='" + oneHouserID + "' ";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //加载二级仓库
        public static DataTable GetTwoHouse(string twoHouserID)
        {
            string str = " select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='" + twoHouserID + "' ";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //根据选择id加载仓库
        public static DataTable GetUserName(string DeptId)
        {
            string sql = "select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID=" + DeptId;
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static string GetTopID()
        {
            string strID = "";
            string strD = "DB-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(ID) from BGOI_Inventory.dbo.tk_AllocationSheet";
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + UnitID + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(3, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + UnitID + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + UnitID + "0" + (num + 1);

                        else
                            strD = strD + UnitID + (num + 1);
                    }
                    else
                    {
                        strD = strD + UnitID + "001";
                    }
                }
            }
            else
            {
                strD = strD + UnitID + "001";
            }
            return strD;

        }
        public static string GetTopDID()
        {
            string strID = "";
            string strD = "DBID-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_Inventory.dbo.tk_AllocationSheetDetailed";
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + UnitID + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(5, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + UnitID + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + UnitID + "0" + (num + 1);

                        else
                            strD = strD + UnitID + (num + 1);
                    }
                    else
                    {
                        strD = strD + UnitID + "001";
                    }
                }
            }
            else
            {
                strD = strD + UnitID + "001";
            }
            return strD;

        }

        public static UIDataTable AllocationSheetDetialList(int a_intPageSize, int a_intPageIndex, string ID)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Inventory].[dbo].[tk_AllocationSheetDetailed]where DBID='" + ID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " DBID='" + ID + "'";
            string strOrderBy = " DBID ";
            String strTable = "[BGOI_Inventory].[dbo].[tk_AllocationSheetDetailed] ";
            String strField = " DBID, DID, ProductID, OrderContent, SpecsModels, OrderUnit, OrderNum, NoTaxuUnit, TaxUnitPrice, NOPrice, Price, State, Remark ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        #endregion
        #region [库房]
        public static UIDataTable StorageRoomList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_WareHouse a " +
                                 " left join BGOI_Inventory.dbo.tk_ConfigProType b on a.TypeID=b.OID " +
                                 " left join BJOI_UM.dbo.UM_UnitNew c on a.UnitID=c.DeptId " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.HouseID ";
            String strTable = " BGOI_Inventory.dbo.tk_WareHouse a " +
                              " left join BGOI_Inventory.dbo.tk_ConfigProType b on a.TypeID=b.OID " +
                              " left join BJOI_UM.dbo.UM_UnitNew c on a.UnitID=c.DeptId ";
            String strField = " b.Text,c.DeptName,Adress, HouseName,IsHouseID,HouseID ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["IsHouseID"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsHouseID"] = "一级库房";
                    }
                    if (instData.DtData.Rows[r]["IsHouseID"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["IsHouseID"] = "二级库房";
                    }
                }
            }

            return instData;
        }

        //撤销
        public static bool DeStorageRoom(string HouseID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = "update BGOI_Inventory.dbo.tk_WareHouse set Validate='i' where HouseID='" + HouseID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }

        //修改加载产品的信息
        public static DataTable UpStorageRoom(string HouseID)
        {
            string sql = "select * from BGOI_Inventory.dbo.tk_WareHouse  where Validate='v' and HouseID='" + HouseID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable UpUpStorageRoom(string HouseID)
        {
            string sql = "select * from BGOI_Inventory.dbo.tk_WareHouse where HouseID='" + HouseID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static bool SaveUpStorageRoom(tk_WareHouse rem, ref string strErr)
        {
            #region [修改]
            int i = 0, m = 0, n = 0;
            string InserNewOrdersHIS = "insert into BGOI_Inventory.dbo.tk_WareHouse_HIS (HouseID, Adress, HouseName, Level, UnitID, DelTime, DelReason, Validate, IsHouseID, TypeID, NCreateTime, NCreateUser)" +
  "select HouseID, Adress, HouseName, Level, UnitID, DelTime, DelReason, Validate, IsHouseID, TypeID,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_Inventory.dbo.tk_WareHouse where HouseID ='" + rem.HouseID + "'";
            m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainCustomer");

            string strDelete = "delete from BGOI_Inventory.dbo.tk_WareHouse where HouseID='" + rem.HouseID + "'";
            i = SQLBase.ExecuteNonQuery(strDelete, "MainInventory");

            string strInsertnew = GSqlSentence.GetInsertInfoByD<tk_WareHouse>(rem, "BGOI_Inventory.dbo.tk_WareHouse");

            n = SQLBase.ExecuteNonQuery(strInsertnew);
            if (i + m + n >= 3)
            {
                return true;
            }
            else
            {
                strErr = "修改失败";
                return false;
            }
            #endregion

        }
        #endregion
        #region [新增货品]
        public static UIDataTable InventoryAddProList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "";
            //strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d  ,(select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "')  e where a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID and  b.Ptype=e.ID  and d.UnitID='" + GAccount.GetAccountInfo().UnitID + "' " + where;
            if (where != "")
            {
                strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_ProductInfo b  " +
                             " left join BGOI_Inventory.dbo.tk_ConfigProType c on b.ProTypeID=c.ID  " +
                             " left join (select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='"+GAccount.GetAccountInfo().UnitID+"') e on b.Ptype=e.ID " +
                             " left join BGOI_BasMan.dbo.tk_SupplierBas  f  on b.Manufacturer=f.SID  where " + where;
            }
            else
            {
                strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_ProductInfo b  " +
                                 " left join BGOI_Inventory.dbo.tk_ConfigProType c on b.ProTypeID=c.ID  " +
                                 " left join (select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') e on b.Ptype=e.ID " +
                                 " left join BGOI_BasMan.dbo.tk_SupplierBas  f  on b.Manufacturer=f.SID  ";
            }
           
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //string strFilter = " a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID  and  b.Ptype=e.ID  and d.UnitID='" + GAccount.GetAccountInfo().UnitID + "'" + where;//and d.TypeID=c.ID
            //string strOrderBy = "a.ID ";
            //String strTable = " BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d ,(select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='"+GAccount.GetAccountInfo().UnitID+"') e  ";
            //String strField = " c.Text,PID, ProTypeID, b.ProName, MaterialNum,d.HouseName,a.FinishCount, b.Spec, UnitPrice, Price2, Units, Manufacturer, Remark, Detail, e.[Text] as Ptext ";


            //string strFilter = " b.Ptype=e.ID and b.ProTypeID=c.ID and b.Manufacturer=f.SID " + where;//and d.TypeID=c.ID
            //string strOrderBy = "b.PID ";
            //String strTable = " BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,(select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') e ,BGOI_BasMan.dbo.tk_SupplierBas  f ";
            //String strField = " c.Text,PID, ProTypeID, b.ProName, MaterialNum, b.Spec, UnitPrice,Price2, Units, Manufacturer, Remark, Detail, e.[Text] as Ptext ,f.COMNameC ";

            string strFilter = where;//and d.TypeID=c.ID
            string strOrderBy = " b.PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductInfo b  "+
                              " left join BGOI_Inventory.dbo.tk_ConfigProType c on b.ProTypeID=c.ID  "+
                              " left join (select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') e on b.Ptype=e.ID " +
                              " left join BGOI_BasMan.dbo.tk_SupplierBas  f  on b.Manufacturer=f.SID ";
            String strField = " c.Text,PID, ProTypeID, b.ProName, MaterialNum, b.Spec, UnitPrice,Price2, Units, Manufacturer, Remark, Detail, e.[Text] as Ptext ,f.COMNameC ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static bool SaveInventoryAddPro(tk_ProductInfo record, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            #region [插入历史表]
            string prohis = "select * from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + record.PID + "'";
            DataTable prodt = SQLBase.FillTable(prohis, "MainInventory");
            tk_ProductInfo_HIS proinfo = new tk_ProductInfo_HIS();
            foreach (DataRow dr in prodt.Rows)
            {
                proinfo.PID = dr["PID"].ToString();
                proinfo.MaterialNum = dr["MaterialNum"].ToString();
                proinfo.Spec = dr["Spec"].ToString();
                proinfo.ProName = dr["ProName"].ToString();
                proinfo.ProTypeID = dr["ProTypeID"].ToString();
                proinfo.UnitPrice = Convert.ToDecimal(dr["UnitPrice"].ToString());
                proinfo.Units = dr["Units"].ToString();
                proinfo.Ptype = dr["Ptype"].ToString();
                proinfo.Manufacturer = dr["Manufacturer"].ToString();
                proinfo.Remark = dr["Remark"].ToString();
                proinfo.Detail = dr["Detail"].ToString();
                proinfo.Price2 = Convert.ToDecimal(dr["Price2"].ToString());
                proinfo.NCreateUser = GAccount.GetAccountInfo().UserName;
                proinfo.NCreateTime = DateTime.Now;
            }
            string strProductInfohis = GSqlSentence.GetInsertInfoByD<tk_ProductInfo_HIS>(proinfo, "BGOI_Inventory.dbo.tk_ProductInfo_HIS");
            if (trans.ExecuteNonQuery(strProductInfohis, CommandType.Text, null) > 0)
            {
                #region [日志]
                string strlog = "Insert into [BGOI_Inventory].[dbo].[dbo.tk_Inventorylog](LogTitle, LogContent, Time, Person, Type, Typeid) " +
                    "values ('留存产品信息','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductInfo_HIS','" + proinfo.PID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            else
            {
                #region [日志]
                string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('留存产品信息','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductInfo_HIS','" + proinfo.PID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            #endregion
            #region [修改tk_ProductInfo]
            string strUpdateList = " update BGOI_Inventory.dbo.tk_ProductInfo set PID='" + record.PID + "', ProTypeID='" + record.ProTypeID + "'," +
                                    " ProName='" + record.ProName + "', MaterialNum='" + record.MaterialNum + "', Spec='" + record.Spec + "', UnitPrice='" + record.UnitPrice + "', Price2='" + record.Price2 + "', Units='" + record.Units + "', " +
                                    " Manufacturer='" + record.Manufacturer + "', Remark='" + record.Remark + "', Detail='" + record.Detail + "', Ptype='" + record.Ptype + "'" +
                                    " where PID='" + record.PID + " ' ";
            if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
            {
                #region [日志]
                string strlog1 = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                     "values ('修改数据','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductInfo','" + record.PID + "')";
                SQLBase.ExecuteNonQuery(strlog1);
                #endregion
                return true;
            }
            else
            {
                #region [日志]
                string strlog1 = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                     "values ('修改数据','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductInfo','" + record.PID + "')";
                SQLBase.ExecuteNonQuery(strlog1);
                #endregion
                strErr = "修改失败";
                return false;
            }
            #endregion

            #region [添加]
            //DataTable strdt = SQLBase.FillTable(" select * from BGOI_Inventory.dbo.tk_ProductInfo where ProName='" + record.ProName + "'");
            //if (strdt.Rows.Count > 0)
            //{
            //    strErr = "产品已存在！";
            //    return false;
            //}
            //else
            //{
            //    strErr = "";
            //    int count = 0;
            //    string strInsert = GSqlSentence.GetInsertInfoByD<tk_ProductInfo>(record, "[BGOI_Inventory].[dbo].tk_ProductInfo");
            //    if (strInsert != "")
            //    {
            //        count = SQLBase.ExecuteNonQuery(strInsert);
            //    }
            //    if (count > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        strErr = "添加失败";
            //        return false;
            //    }
            // }
            #endregion
        }
        public static bool SaveInventoryAddProNew(tk_ProductInfo record, ref string strErr)
        {

            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                #region [添加]
                DataTable strdt = SQLBase.FillTable(" select * from BGOI_Inventory.dbo.tk_ProductInfo where ProName='" + record.ProName + "'");
                if (strdt.Rows.Count > 0)
                {
                    strErr = "产品已存在！";
                    return false;
                }
                else
                {
                    strErr = "";
                    int count = 0;
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_ProductInfo>(record, "[BGOI_Inventory].[dbo].tk_ProductInfo");
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "添加失败";
                        return false;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                throw;
            }
        }

        public static DataTable GetManufacturer()
        {
            string str = "select SID,COMNameC from BGOI_BasMan.dbo.tk_SupplierBas where Validate='v'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }

        public static DataTable GetPType()
        {
            string str = "select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where  UnitID ='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static int checkDataList(string year, string month)
        {
            string strsql = " select count(*) from tk_PlanMaterial where validate='v' and PlanYear='" + year + "' and PlanMonth='" + month + "'";
            int count = Convert.ToInt32(SQLBase.ExecuteScalar(strsql));// 返回查询到的数据条数 
            if (count < 0)
                return -1;
            else
                return count;

        }

        public static DataTable GetMan()
        {
            //供应商
            string strBas = " select SID,COMNameC from BGOI_BasMan.dbo.tk_SupplierBas where Validate='v' ";
            DataTable Basdt = SQLBase.FillTable(strBas, "SupplyCnn");
            return Basdt;
        }
        // 保存计划表单数据 
        // 上传信息：物料编码，物料长描述，供应商，计量单位，需要数量，计划单位，需要日期，备注，计划年，计划月，是否有效，创建时间
        //序号	编号	零件名称	图号或规格	单台  数量	单 位	领出数量	更换数量	更换日期	签字	备注
        public static bool SavePlanData(string strData, ref string strErr)
        {
            strErr = "";
            int resultcount = 0;
            try
            {
                int count = 0;
                string strSql = "";
                string[] strList = strData.Split('!');// 完整的数据
                if (resultcount < 100000)
                {
                    for (int i = 0; i < strList.Length; i++)
                    {
                        string[] strList1 = strList[i].Split(',');
                        string strsql = " select * from BGOI_Inventory.dbo.tk_ProductInfo where ltrim(PID)=" + strList1[0].ToString().Trim() + "";
                        #region [处理类型]
                        string strProType = " select OID,[Text] from BGOI_Inventory.dbo.tk_ConfigProType";
                        DataTable pdt = SQLBase.FillTable(strProType, "MainInventory");

                        string strPType = " select ID,[Text] from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
                        DataTable ptdt = SQLBase.FillTable(strPType, "MainInventory");

                        //供应商
                        string strBas = " select SID,COMNameC from BGOI_BasMan.dbo.tk_SupplierBas where Validate='v' ";
                        DataTable Basdt = SQLBase.FillTable(strBas, "SupplyCnn");
                        #endregion
                        DataTable dt = SQLBase.FillTable(strsql, "MainInventory");
                        if (dt.Rows.Count == 0)
                        {
                            #region [供应商]
                            if (Basdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < Basdt.Rows.Count; j++)
                                {
                                    if (strList1[8].Replace("'", "").ToString() == Basdt.Rows[j]["COMNameC"].ToString())
                                    {
                                        strList1[8] = "'" + Basdt.Rows[j]["SID"].ToString() + "'";
                                    }
                                }
                            }
                            #endregion

                            #region [转换仓库类型]
                            if (pdt.Rows.Count > 0)
                            {
                                foreach (DataRow pdr in pdt.Rows)
                                {
                                    string v = pdr["Text"].ToString();
                                    if (strList1[1].Replace("'", "").ToString() == pdr["Text"].ToString())
                                    {
                                        strList1[1] = pdr["OID"].ToString();
                                    }
                                }
                            }
                            #endregion
                            #region [转换货品类型]
                            if (ptdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < ptdt.Rows.Count; j++)
                                {
                                    if (strList1[11].Replace("'", "").ToString() == ptdt.Rows[j]["Text"].ToString())
                                    {
                                        strList1[11] = "'" + ptdt.Rows[j]["ID"].ToString() + "'";
                                    }
                                }
                            }
                            #endregion
                            strSql += " insert into BGOI_Inventory.dbo.tk_ProductInfo" +
                           "(PID, ProTypeID, ProName, MaterialNum, Spec, UnitPrice, Price2, Units, Manufacturer, Remark, Detail, Ptype)" +
                           " values(" + strList1[0] + "," + strList1[1] + "," + strList1[2] + "," + strList1[3] + "," + strList1[4] + "," + strList1[5] + "" +
                           " ," + strList1[6] + "," + strList1[7] + "," + strList1[8] + "," + strList1[9] + "," + strList1[10] + "," + strList1[11] + ")   ";

                        }
                        else
                        {
                            #region [供应商]
                            if (Basdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < Basdt.Rows.Count; j++)
                                {
                                    if (strList1[8].Replace("'", "").ToString() == Basdt.Rows[j]["COMNameC"].ToString())
                                    {
                                        strList1[8] = "'" + Basdt.Rows[j]["SID"].ToString() + "'";
                                    }
                                }
                            }
                            #endregion
                            #region [转换仓库类型]
                            if (pdt.Rows.Count > 0)
                            {
                                foreach (DataRow pdr in pdt.Rows)
                                {
                                    string v = pdr["Text"].ToString();
                                    if (strList1[1].Replace("'", "").ToString() == pdr["Text"].ToString())
                                    {
                                        strList1[1] = pdr["OID"].ToString();
                                    }
                                }
                            }
                            #endregion
                            #region [转换货品类型]
                            if (ptdt.Rows.Count > 0)
                            {
                                for (int j = 0; j < ptdt.Rows.Count; j++)
                                {
                                    if (strList1[11].Replace("'", "").ToString() == ptdt.Rows[j]["Text"].ToString())
                                    {
                                        strList1[11] = "'" + ptdt.Rows[j]["ID"].ToString() + "'";
                                    }
                                }
                            }
                            #endregion
                            //continue;//过滤重复的
                            strSql += " update BGOI_Inventory.dbo.tk_ProductInfo set PID=" + strList1[0] + ", ProTypeID=" + strList1[1] + ", ProName=" + strList1[2] + ", MaterialNum=" + strList1[3] + ", Spec=" + strList1[4] + ", UnitPrice=" + strList1[5] + ", Price2=" + strList1[6] + ", Units=" + strList1[7] + ", Manufacturer=" + strList1[8] + ", Remark=" + strList1[9] + ", Detail=" + strList1[10] + ", Ptype=" + strList1[11] + " where PID=" + strList1[0] + "  ";
                        }
                    }
                    if (strSql != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strSql);
                        if (count > 0)
                            return true;
                        else
                        {
                            strErr = "货品数据保存失败";
                            return false;
                        }
                    }
                    else
                    {
                        strErr = "上传已保存";
                        return false;
                    }

                }
                else
                {
                    strErr = "货品数据上传失败，请重新上传";
                    return false;
                }

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                throw;
            }
        }
        public static DataTable UpInventoryList(string PID)
        {
            string sql = "Select c.Text,PID,ProTypeID,b.ProName,MaterialNum,b.Spec,UnitPrice,Price2,Units,Manufacturer,Remark,Detail,e.[Text] as Ptext From " +
                        "BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c, " +
                        "(select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') e Where b.ProTypeID=c.ID  and b.Ptype=e.ID  and b.PID='" + PID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #region [新增规格型号]
        public static bool SaveAddSpec(tk_ProductSpec record, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            string strProductInfohis = GSqlSentence.GetInsertInfoByD<tk_ProductSpec>(record, "BGOI_Inventory.dbo.tk_ProductSpec");
            if (trans.ExecuteNonQuery(strProductInfohis) > 0)
            {
                #region [日志]
                string strlog = "Insert into [BGOI_Inventory].[dbo].[dbo.tk_Inventorylog](LogTitle, LogContent, Time, Person, Type, Typeid) " +
                    "values ('新增货品规格','新增成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductSpec','" + record.GGID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
                return true;
            }
            else
            {
                #region [日志]
                string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('新增货品规格','新增失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProductSpec','" + record.GGID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
                strErr = record.Spec + "货品规格型号已存在";
                return false;
            }
        }
        public static string GetTopGGID()
        {
            string strID = "";
            string strD = "GG-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(GGID) from BGOI_CustomerService.dbo.tk_ProductSpec";
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + UnitID + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(3, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + UnitID + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + UnitID + "0" + (num + 1);

                        else
                            strD = strD + UnitID + (num + 1);
                    }
                    else
                    {
                        strD = strD + UnitID + "001";
                    }
                }
            }
            else
            {
                strD = strD + UnitID + "001";
            }
            return strD;

        }
        #endregion
        #endregion
        #region [成品定义]
        // 保存计划表单数据 
        //货品唯一编号	组装该成品的零件PID	需零件数量	规格型号
        public static bool SaveDefinitionOfProduct(string strData, ref string strErr)
        {
            strErr = "";
            int resultcount = 0;
            try
            {
                int count = 0;
                string strSql = "";
                string[] strList = strData.Split('!');// 完整的数据
                if (resultcount < 100000)
                {
                    for (int i = 0; i < strList.Length; i++)
                    {
                        string[] strList1 = strList[i].Split(',');
                        string strsql = " select * from BGOI_Inventory.dbo.tk_ProFinishDefine where  ProductID=" + strList1[0] + "";
                        DataTable dt = SQLBase.FillTable(strsql, "MainInventory");
                        if (dt.Rows.Count == 0)
                        {
                            strSql += " insert into BGOI_Inventory.dbo.tk_ProFinishDefine(ProductID, PartPID, Number,Spec) values(" + strList[i] + ") ";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (strSql != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strSql);
                    }
                    if (count > 0)
                        return true;
                    else
                    {
                        strErr = "货品数据保存失败";
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
        public static UIDataTable DefinitionOfProductList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            #region [冗余]
            // string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d  ,BGOI_Inventory.dbo.tk_ConfigPType e,BGOI_Inventory.dbo.tk_ProFinishDefine f  where a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID and f.ProductID=a.ProductID and   b.Ptype=e.ID  and" + where;
            //string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a " +
            //                    " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
            //                    " left join BGOI_Inventory.dbo.tk_ConfigProType c on b.ProTypeID=c.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID  and d.TypeID=c.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_ConfigPType e on b.Ptype=e.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_ProFinishDefine f on f.ProductID=b.PID and f.ProductID=a.ProductID " +
            //                    " where  " + where; 
            #endregion
            string strSelCount = " select COUNT(*) from (select ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine a " +
                                " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigPType c on b.Ptype=c.ID " +
                                " where " + where +
                                " group by ProductID) a ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            // string strFilter = " a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID  and d.TypeID=c.ID and  f.ProductID=a.ProductID and  b.Ptype=e.ID and " + where;
            string strFilter = where + " group by ProductID,b.ProName,b.Spec,b.Units,b.MaterialNum,b.Remark,a.State ";
            #region [冗余]
            //string strOrderBy = "a.ID ";
            ////String strTable = " BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d ,BGOI_Inventory.dbo.tk_ConfigPType e,BGOI_Inventory.dbo.tk_ProFinishDefine f  ";
            //String strTable = " BGOI_Inventory.dbo.tk_StockRemain a" +
            //                     " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
            //                    " left join BGOI_Inventory.dbo.tk_ConfigProType c on b.ProTypeID=c.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID  and d.TypeID=c.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_ConfigPType e on b.Ptype=e.ID " +
            //                    " left join BGOI_Inventory.dbo.tk_ProFinishDefine f on f.ProductID=b.PID and f.ProductID=a.ProductID ";
            //String strField = " c.Text,PID, ProTypeID, b.ProName, MaterialNum,d.HouseName,a.FinishCount, b.Spec, UnitPrice, Price2, Units, Manufacturer, Remark, Detail, e.[Text] as Ptext ";
            #endregion
            string strOrderBy = " ProductID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProFinishDefine a " +
                              " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID  " +
                              " left join BGOI_Inventory.dbo.tk_ConfigPType c on b.Ptype=c.ID ";
            String strField = " ProductID,b.ProName,b.Spec,b.Units,b.MaterialNum,b.Remark,a.State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未添加可生产";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已添加可生产";
                    }
                }
            }
            return instData;
        }
        public static UIDataTable DefinitionOfList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_Inventory.dbo.tk_ProductInfo a " +
                                 " left join BGOI_Inventory.dbo.tk_ProFinishDefine b on a.PID=b.PartPID " +
                                 " left join  BGOI_BasMan.dbo.tk_SupplierBas c on c.SID=a.Manufacturer " +
                                 " where b.ValiDate='v' and a.ProTypeID='1' " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "   b.ValiDate='v' and a.ProTypeID='1'  " + where;
            string strOrderBy = " a.PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductInfo a " +
                              " left join BGOI_Inventory.dbo.tk_ProFinishDefine b on a.PID=b.PartPID " +
                             " left join  BGOI_BasMan.dbo.tk_SupplierBas c on c.SID=a.Manufacturer ";
            String strField = " b.Number,PID,ProName,a.Spec,b.IdentitySharing, UnitPrice, Price2, Units,c.COMNameC , Remark";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }

            return instData;
        }
        public static DataTable GetDefinitionOfProduct()
        {
            //  string str = "select PID,ProName from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'";
            string str = " select * From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and ProTypeID='2' ";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static bool SaveAddDefinitionOfProduct(List<tk_ProFinishDefine> delist, string type, string ProductID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                if (type == "1")//添加
                {
                    string jianchestr = " select * from BGOI_Inventory.dbo.tk_ProFinishDefine  where ValiDate='v' and ProductID='" + ProductID + "'";
                    DataTable dt = SQLBase.FillTable(jianchestr, "MainInventory");

                    if (dt.Rows.Count <= 0 || dt.Rows == null)
                    {
                        string strInsertList = "";
                        if (delist.Count > 0)
                        {
                            foreach (tk_ProFinishDefine SID in delist)
                            {
                                strInsertList += " Insert into [BGOI_Inventory].[dbo].tk_ProFinishDefine( ProductID, PartPID, Number,Spec, ValiDate,State,IdentitySharing,IdentifierStr) " +
                                                " values ('" + ProductID + "','" + SID.PartPID + "','" + SID.Number + "','" + SID.Spec + "','" + SID.ValiDate + "','" + SID.State + "','" + SID.IdentitySharing + "','" + SID.IdentifierStr + "') ";
                            }
                        }
                        if (strInsertList != "")
                        {
                            count = SQLBase.ExecuteNonQuery(strInsertList);
                        }
                        if (count > 0)
                        {
                            trans.Close(true);
                            return true;
                        }
                        else
                        {
                            trans.Close(true);
                            return false;
                        }
                    }
                    else
                    {
                        strErr = "此成品已定义!";
                        trans.Close(true);
                        return false;
                    }

                }
                else//修改
                {
                    #region [插入历史表]
                    string define = "select * from [BGOI_Inventory].[dbo].tk_ProFinishDefine where  ProductID='" + ProductID + "'";
                    DataTable dt = SQLBase.FillTable(define, "MainInventory");
                    tk_ProFinishDefine_HIS definehis = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        definehis = new tk_ProFinishDefine_HIS();
                        definehis.ProductID = dr["ProductID"].ToString();
                        definehis.PartPID = dr["PartPID"].ToString();
                        definehis.Number = Convert.ToInt32(dr["Number"]);
                        definehis.Spec = dr["Spec"].ToString();
                        definehis.ValiDate = dr["ValiDate"].ToString();
                        definehis.NCreateUser = GAccount.GetAccountInfo().UserName;
                        definehis.NCreateTime = DateTime.Now;
                    }
                    string prodefinehis = GSqlSentence.GetInsertInfoByD<tk_ProFinishDefine_HIS>(definehis, "BGOI_Inventory.dbo.tk_ProFinishDefine_HIS");
                    if (trans.ExecuteNonQuery(prodefinehis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_Inventory].[dbo].tk_Inventorylog( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存成品','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProFinishDefine_HIS','" + ProductID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_Inventory].[dbo].tk_Inventorylog( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存成品','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ProFinishDefine_HIS','" + ProductID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    #endregion
                    #region [修改]
                    string strUpdateList = "";

                    string del = "delete  from BGOI_Inventory.dbo.tk_ProFinishDefine  where ProductID='" + ProductID + "'";
                    SQLBase.ExecuteNonQuery(del);
                    string delSC = "delete  from BGOI_Inventory.dbo.tk_ProductionOfFinishedGoods  where PID='" + ProductID + "'";
                    SQLBase.ExecuteNonQuery(delSC);
                    if (delist.Count > 0)
                    {
                        foreach (tk_ProFinishDefine SID in delist)
                        {
                            strUpdateList += "  Insert into [BGOI_Inventory].[dbo].tk_ProFinishDefine (ProductID, PartPID, Number, Spec, ValiDate, State,IdentitySharing,IdentifierStr ) values ('" + ProductID + "','" + SID.PartPID + "','" + SID.Number + "','" + SID.Spec + "','" + SID.ValiDate + "','" + SID.State + "','" + SID.IdentitySharing + "','" + SID.IdentifierStr + "') ";
                        }
                    }
                    if (strUpdateList != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strUpdateList);
                    }
                    if (count > 0)
                    {
                        trans.Close(true);
                        return true;
                    }
                    else
                    {
                        trans.Close(true);
                        return false;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        //撤销
        public static bool DeDefinitionOfProduct(string PID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = "update BGOI_Inventory.dbo.tk_ProFinishDefine set Validate='i' where ProductID='" + PID + "'";

                string strInsertnew = "update BGOI_Inventory.dbo.tk_ProductionOfFinishedGoods set Validate='i' where PID='" + PID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert + strInsertnew);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        //添加可生产
        public static bool AddProFin(string PID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string updatestr = " update [BGOI_Inventory].[dbo].tk_ProFinishDefine set State='1' where ProductID='" + PID + "'";

                string strInsert = "";
                string sqlchongfu = " select * from BGOI_Inventory.dbo.tk_ProductionOfFinishedGoods where Validate='v' and  PID='" + PID + "'";
                DataTable dt = SQLBase.FillTable(sqlchongfu, "MainInventory");
                if (dt.Rows.Count <= 0 || dt.Rows == null)
                {
                    tk_ProductionOfFinishedGoods profin = new tk_ProductionOfFinishedGoods();
                    profin.PID = PID;
                    profin.UnitID = GAccount.GetAccountInfo().UnitID;
                    profin.CreateTime = DateTime.Now;
                    profin.CreateUser = GAccount.GetAccountInfo().UserName;
                    profin.Validate = "v";
                    strInsert = GSqlSentence.GetInsertInfoByD<tk_ProductionOfFinishedGoods>(profin, "BGOI_Inventory.dbo.tk_ProductionOfFinishedGoods");
                }
                else
                {
                    strErr = "已添加该产品！";
                    return false;
                }

                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    SQLBase.ExecuteNonQuery(updatestr);
                    return true;
                }
                else
                {
                    strErr = "添加失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        //修改加载零件
        public static DataTable GetUpXian(string PID)
        {
            // string sql = "select * From BGOI_Inventory.dbo.tk_ProFinishDefine where ProductID='" + PID + "'";
            string sql = " select a.PartPID,b.ProName,b.Spec,b.Units,a.Number,a.IdentitySharing   from BGOI_Inventory.dbo.tk_ProFinishDefine a " +
                        " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.PartPID=b.PID " +
                        " where b.ProTypeID='1' and a.ProductID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        //修改加载数据(下拉框)
        public static DataTable GetUpDefinitionOfProduct(string PID)
        {
            //string sql = "select * From BGOI_Inventory.dbo.tk_ProFinishDefine where ProductID='" + PID + "'";
            //string sql = " select distinct b.ProName From BGOI_Inventory.dbo.tk_ProFinishDefine a " +
            //            " left join  BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
            //            " where b.ProTypeID='2' and ProductID='" + PID + "' ";

            string sql = "select PID,ProName,Spec  From BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "' and  ProTypeID='2'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        #region [添加下拉框]
        public static DataTable GetDefinitionOfProductName()
        {
            //  string str = "select PID,ProName from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'";
            string str = " select distinct(ProName) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and ProTypeID='2' and a.ProName!=''";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetDefinitionOfProductPID()
        {
            //  string str = "select PID,ProName from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'";
            string str = " select distinct(PID) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and ProTypeID='2' and a.PID!=''";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetDefinitionOfProductSpec()
        {
            //  string str = "select PID,ProName from BGOI_Inventory.dbo.tk_ProductInfo where ProTypeID='2'";
            string str = " select distinct(Spec) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and ProTypeID='2' and a.Spec!=''";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        #endregion

        #region [根据产品名称添加编号和规格]
        public static DataTable GetProNameToSpec(string ProName)
        {
            string sql = "select distinct(Spec) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and a.ProTypeID='2'  and a.Spec != '' and a.ProName like '%" + ProName + "%'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static DataTable GetPIDToSpec(string ProductID)
        {
            string sql = "select distinct(Spec) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and a.ProTypeID='2'  and a.Spec != '' and a.PID= '" + ProductID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static DataTable GetProNameToPID(string ProName)
        {
            string sql = "select distinct(PID) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID and a.ProTypeID='2'  and a.PID != '' and a.ProName like '%" + ProName + "%'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        #endregion
        #endregion
        #region [库存管理]
        public static UIDataTable StockRemainListnew(int a_intPageSize, int a_intPageIndex, string where, string PID, string UnitID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "";
            if (UnitID == "46" || UnitID == "32")
            {
                strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a " +
                                    " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID " +
                                    " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
                                    " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in " +
                                    " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where HouseID in " +
                                    " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c " +
                                    " on d.TypeID=c.ID " +
                                    "  where" + where;
            }
            else
            {
                //  string strSelCount = " select COUNT(*) from tk_StockRemain a,tk_ProductInfo b,tk_ConfigProType c,tk_WareHouse d where a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID " + where;
                strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a " +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID  " +
                                     " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
                                     " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in " +
                                     " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "' and HouseID in " +
                                     " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c " +// where ProductID='" + PID + "'
                                     " on d.TypeID=c.ID " +
                                     "  where d.UnitID='" + UnitID + "' and " + where;

            }

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //string strFilter = " a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID " + where;
            //string strOrderBy = "a.ID ";
            //String strTable = " BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d ";
            //String strField = " c.Text,PID,b.ProName,b.Spec,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName ";

            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (UnitID == "46" || UnitID == "32")
            {
                strFilter = where;
                strOrderBy = "a.ID ";
                strTable = " BGOI_Inventory.dbo.tk_StockRemain a" +
                                  " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID" +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID" +
                                  " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in" +
                                  " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where HouseID in" +
                                  " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c on d.TypeID=c.ID";
                strField = " c.Text,PID,b.ProName,b.Spec,a.FinishCount,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName ";
            }
            else
            {
                strFilter = " d.UnitID='" + UnitID + "' and " + where;
                strOrderBy = "a.ID ";
                strTable = " BGOI_Inventory.dbo.tk_StockRemain a" +
                                   " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID" +
                                   " left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID " +
                                   " left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID in" +
                                   " (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "' and HouseID in" +

                                   " (select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c on d.TypeID=c.ID ";// where ProductID='" + PID + "'

                strField = " c.Text,PID,b.ProName,b.Spec,a.FinishCount,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName ";
            }


            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static UIDataTable StockRemainList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d where a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID " + where;
            string strOrderBy = "a.ID ";
            String strTable = " BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d ";
            String strField = " c.Text,PID,b.ProName,b.Spec,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetProType()
        {
            string str = "select ID,Text from BGOI_Inventory.dbo.tk_ConfigProType";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetSpec()
        {
            string str = "select ID,Text  FROM [BGOI_Inventory].[dbo].[tk_ProductSelect_Config]  where [Type]='SpecsModels'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetHouseIDoneNewnew(string HouseID, string ProType)
        {
            string str = "";
            if (HouseID == "46" || HouseID == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='" + ProType + "' and Validate='v'";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1'and TypeID='" + ProType + "' and Validate='v'  and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + HouseID + "') or UnitID='" + HouseID + "')";
            }
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetHouseIDtwoNewnew(string HouseID, string ProType)
        {
            string str = "";
            if (HouseID == "46" || HouseID == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2'and TypeID='" + ProType + "' and Validate='v' ";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='" + ProType + "' and Validate='v'  and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + HouseID + "') or UnitID='" + HouseID + "')";
            }
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //零件
        public static DataTable GetHouseIDone(string TypeID)
        {
            string str = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='1' ";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='1' and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + GAccount.GetAccountInfo().UnitID + "') or UnitID='" + GAccount.GetAccountInfo().UnitID + "')";
            }

            //string str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='" + TypeID + "' and UnitID in(select Deptid from BJOI_UM.dbo.UM_UnitNew where SuperId='" + GAccount.GetAccountInfo().UnitID + "')";
            //string str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='" + TypeID + "' and UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //零件
        public static DataTable GetHouseIDtwo(string TypeID)
        {
            string str = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='1'";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='1' and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + GAccount.GetAccountInfo().UnitID + "') or UnitID='" + GAccount.GetAccountInfo().UnitID + "')";
            }

            //string str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='" + TypeID + "' and UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }


        public static DataTable GetHouseIDoneNew(string TypeID)
        {
            string str = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1'and TypeID='" + TypeID + "' and Validate='v'";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1' and TypeID='" + TypeID + "' and Validate='v'  and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + unitid + "') or UnitID='" + unitid + "')";
            }
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetHouseIDtwoNew(string TypeID)
        {
            string str = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='" + TypeID + "' and Validate='v' ";
            }
            else
            {
                str = "select * From BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2' and TypeID='" + TypeID + "' and Validate='v'  and ( UnitID in(select SuperId from BJOI_UM.dbo.UM_UnitNew where Deptid='" + unitid + "') or UnitID='" + unitid + "')";
            }
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetHouseID()
        {
            string str = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v'";
            }
            else
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v' and UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            }

            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //根据当前用户ID加载旗下仓库
        public static DataTable GetHouseIDRU()
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string str = "";
            if (UnitID == "46" || UnitID == "32")
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v'";
            }
            else
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v' and UnitID=" + UnitID;
            }

            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        /// <summary>
        ///根据ID找的上级ID加载旗下所有仓库
        /// </summary>
        /// <returns></returns>
        public static DataTable GetHouseSYID()
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string str = "";
            if (UnitID == "46" || UnitID == "32")
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v'";
            }
            else
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where Validate='v' and UnitID='" + UnitID + "' or UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId in (select DeptId from BJOI_UM.dbo.UM_UnitNew where SuperId='" + UnitID + "')) ";
            }

            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        #endregion
        #region [货品入库]
        public static DataTable GetSubjectID()
        {
            string str = "SELECT  ID,Text FROM [BGOI_Inventory].[dbo].[tk_ConfigSubject] order by OID desc";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static DataTable GetOutByWhere(string a_strWhere, ref string a_strErr)
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;// 当前单位ID
            string strSql = "";
            try
            {
                if (UnitID == "46" || UnitID == "32")
                {
                    strSql = "select c.Text,PID,b.ProName,b.Spec,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName " +
                        " from BGOI_Inventory.dbo.tk_StockRemain a   " +
                        "left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID " +
                        "left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID " +
                        "in  (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse)) c  on b.ProTypeID=c.ID " +
                        "left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID   " +
                        "where " + a_strWhere;
                }
                else
                {
                    strSql = "select c.Text,PID,b.ProName,b.Spec,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName " +
                            " from BGOI_Inventory.dbo.tk_StockRemain a   " +
                            "left join BGOI_Inventory.dbo.tk_ProductInfo b on a.ProductID=b.PID   " +
                            "left join (select * from BGOI_Inventory.dbo.tk_ConfigProType c where ID  " +
                            "in  (select TypeID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and HouseID in   " +
                            "(select HouseID from BGOI_Inventory.dbo.tk_StockRemain ))) c  on b.ProTypeID=c.ID   " +
                            "left join BGOI_Inventory.dbo.tk_WareHouse d on a.HouseID=d.HouseID   " +
                            "where  d.UnitID='" + GAccount.GetAccountInfo().UnitID + "'  " + a_strWhere;
                }

                //strSql = "Select  c.Text,PID,b.ProName," +
                //    "b.Spec,a.OnlineCount,a.UsableStock,a.Costing,a.Location,d.HouseName  From  BGOI_Inventory.dbo.tk_StockRemain a," +
                //    "BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_Inventory.dbo.tk_ConfigProType c,BGOI_Inventory.dbo.tk_WareHouse d  " +
                //    "Where  a.ProductID=b.PID and b.ProTypeID=c.ID and a.HouseID=d.HouseID " + a_strWhere + " ";


                DataTable dtInfo = SQLBase.FillTable(strSql);
                return dtInfo;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                return null;
            }
        }
        public static UIDataTable StockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                " from BGOI_Inventory.dbo.tk_StockIn a " +
                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')" +
                //" )b on a.ListInID=b.ListInID " +
                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')) " +
                //" )c on c.PID=b.ProductID  " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  " +
                                " where " + where;

            //string strSelCount = "select COUNT(*) " +
            //                     " from BGOI_Inventory.dbo.tk_StockIn a " +
            //                     " left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
            //                     " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')" +
            //                     " )b on a.ListInID=b.ListInID " +
            //                     " left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
            //                     " (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
            //                     " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')) " +
            //                     " )c on c.PID=b.ProductID  " +
            //                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
            //                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
            //                     " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  " +
            //                     " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')" +
                //" )b on a.ListInID=b.ListInID " +
                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')) " +
                //" )c on c.PID=b.ProductID  " +
                             " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                             " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                             " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  ";
            //String strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
            //                  " left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')" +
            //                  " )b on a.ListInID=b.ListInID " +
            //                  " left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
            //                  " (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='采购' and  ValiDate='v'  and State= '0')) " +
            //                  " )c on c.PID=b.ProductID  " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
            //                  " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  ";
            // String strField = " f.Text as T,c.Spec,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            String strField = " HandwrittenAccount ,f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }

                }
            }

            return instData;
        }
        public static string GetTopListInID()
        {
            string strID = "";
            string strD = "A" + DateTime.Now.ToString("yyyyMMdd");
            string strSqlID = "select max(ListInID) from BGOI_Inventory.dbo.tk_StockIn";
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
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(1, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "0" + (num + 1);

                        else
                            strD = strD + (num + 1);
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
        public static string GetTopHandwrittenAccount()
        {
            string strID = "";
            string strD = "" + DateTime.Now.ToString("yyyyMMdd");
            string strSqlID = "select max(BatchID) from BGOI_Inventory.dbo.tk_StockIn";
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
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(0, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "0" + (num + 1);

                        else
                            strD = strD + (num + 1);
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
        public static DataTable GetConfigInfo(string taskType)
        {
            string sql = "select *  FROM [BGOI_Inventory].[dbo].[tk_ProductSelect_Config]  where [Type]='" + taskType + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static UIDataTable ChangeProcureList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "  select COUNT(*) FROM [BGOI_PP].[dbo].[PurchaseInventorys] a left join BGOI_Inventory.dbo.tk_WareHouse b on a.CKID=b.HouseID where a.Validate='1' and a.State='0' and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "'   ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.Validate='1' and a.State='0' and a.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            string strOrderBy = " RKID ";
            String strTable = " [BGOI_PP].[dbo].[PurchaseInventorys] a left join BGOI_Inventory.dbo.tk_WareHouse b on a.CKID=b.HouseID ";
            String strField = " RKID,SHID,Convert(varchar(100),Rkdate,23) as  Rkdate,b.HouseName,RKInstructions,Handler ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetProcureDetail(string SHID, string RKID)
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            //if (UnitID == "47")
            //{
            //    string sqlold = "select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID='" + RKID + "' ";
            //    DataTable dtold = SQLBase.FillTable(sqlold, "MainInventory");
            //    if (dtold.Rows.Count > 0 && dtold != null)
            //    {
            //        sql = " select e.Price2,c.RKID,c.RKID as SHID ,b.MaterialNO,b.OrderContent,b.Specifications,b.Supplier,COMNameC,b.Unit, " +
            //              " ((case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end)*Price2)as TotalNoTax,  " +
            //                 " (case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end) as Amount, " +
            //                 " b.UnitPriceNoTax,b.Remark From BGOI_PP.dbo.ReceivingInformation a " +//b.TotalNoTax,b.TotalNoTax,
            //                 " left join BGOI_PP.dbo.OrderGoods b  on a.DDID=b.DDID " +
            //                 " left join  BGOI_PP.dbo.PurchaseInventorys c on c.SHID=a.SHID " +
            //                 " left join  BGOI_BasMan.dbo.tk_SupplierBas d on b.Supplier=d.SID " +
            //                 " left join  BGOI_Inventory.dbo.tk_ProductInfo e on  b.MaterialNO=e.PID " +
            //                 " left join [BGOI_Inventory].[dbo].tk_StockInDetail f on c.RKID=f.OrderID  and  b.MaterialNO=f.ProductID " +
            //                  " where a.SHID='" + SHID + "' and c.RKID='" + RKID + "'  " +
            //                  " and f.OrderID='" + RKID + "' and b.Amount-f.StockInCount>0  group by  e.Price2,c.RKID,a.SHID,b.MaterialNO, " +
            //                  " b.OrderContent,b.Specifications,b.Supplier,COMNameC,b.Unit, " +
            //                 "  b.UnitPriceNoTax,b.Remark,b.Amount,f.StockInCount  having  b.Amount-sum(f.StockInCount)>0";
            //    }
            //    else
            //    {
            //        sql = "select e.Price2,c.RKID,c.RKID as SHID,b.MaterialNO,b.OrderContent,b.Specifications,b.Supplier,COMNameC,b.Unit,b.Amount,b.UnitPriceNoTax,b.TotalNoTax,b.Remark From BGOI_PP.dbo.ReceivingInformation a ,BGOI_PP.dbo.OrderGoods b,BGOI_PP.dbo.PurchaseInventorys c ,BGOI_BasMan.dbo.tk_SupplierBas d ,BGOI_Inventory.dbo.tk_ProductInfo e where b.MaterialNO=e.PID and b.Supplier=d.SID and a.DDID=b.DDID and c.SHID=a.SHID and a.SHID='" + SHID + "' and c.RKID='" + RKID + "' ";
            //    }
            //}
            //else
            //{

                string sqlold = "select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID='" + RKID + "' ";
                DataTable dtold = SQLBase.FillTable(sqlold, "MainInventory");
                if (dtold.Rows.Count > 0 && dtold != null)
                {
                    sql = "select c.Remark,COMNameC,a.RKID as RKID,DID,a.INID as MaterialNO,a.RKID as SHID,OrderContent,Specifications,Supplier,c.Units  as Unit,c.Remark,c.UnitPrice,c.Price2, " +
                        " ((case when a.Amount-sum(f.StockInCount)>0 then a.Amount-sum(f.StockInCount) else 0 end)*Price2)as TotalNoTax,  " +
                        " (case when a.Amount-sum(f.StockInCount)>0 then a.Amount-sum(f.StockInCount) else 0 end) as Amount       " +
                         " From BGOI_PP.dbo.GoodsreceiptDetailed a       " +
                         " left join  BGOI_PP.dbo.PurchaseInventorys b on b.RKID=a.RKID " +
                         " left join  BGOI_Inventory.dbo.tk_ProductInfo c on  c.PID=a.INID        " +
                         " left join   BGOI_BasMan.dbo.tk_SupplierBas d on d.SID=c.Manufacturer        " +
                         " left join [BGOI_Inventory].[dbo].tk_StockInDetail f on a.RKID=f.OrderID and  a.INID=f.ProductID  " +
                         " where a.RKID='" + RKID + "'and b.RKID='" + RKID + "' and f.OrderID='" + RKID + "'  " +
                         " group by COMNameC, a.RKID,DID,c.PID,OrderContent,Specifications,Supplier,c.Units,Amount,c.Remark,c.UnitPrice,c.Price2, " +
                         " f.StockInCount,a.INID   " +
                         " having  a.Amount-sum(f.StockInCount)>0  ";
                }
                else
                {
                    sql = " select c.Remark,c.Price2,a.RKID,a.INID as MaterialNO,a.RKID as SHID ,a.OrderContent,a.Specifications,COMNameC, " +
                        " a.Unit,a.Amount,a.UnitPriceNoTax,a.TotalNoTax from BGOI_PP.dbo.GoodsreceiptDetailed a," +
                        "  BGOI_BasMan.dbo.tk_SupplierBas b  " +
                        " ,BGOI_Inventory.dbo.tk_ProductInfo c ,BGOI_PP.dbo.PurchaseInventorys d where " +
                        " a.Supplier=b.SID and a.INID=c.PID  and d.RKID=a.RKID and d.RKID='" + RKID + "' and  a.RKID='" + RKID + "' ";
                }
           // }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static bool SaveProcureStockIn(StockIn record, string SHID, List<StockInDetail> delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                int CGNum = 0;
                int InCount = 0;
                string[] strSHID = SHID.Split(',');
                #region [查询采购单货品实际数量]
                if (delist.Count > 0)
                {
                    string sqlcg1 = "";
                    //if (GAccount.GetAccountInfo().UnitID == "47")
                    //{
                    //    sqlcg1 = " select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID in " +
                    //       " (select RKID from BGOI_PP.dbo.PurchaseInventorys where SHID='" + strSHID[0] + "')";
                    //}
                    //else
                    //{
                    sqlcg1 = " select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID ='" + strSHID[0] + "'";
                    // }
                    DataTable dtcg1 = SQLBase.FillTable(sqlcg1, "MainInventory");
                    foreach (DataRow dr1 in dtcg1.Rows)
                    {
                        InCount += Convert.ToInt32(dr1["StockInCount"]);
                    }
                    foreach (StockInDetail SID in delist)
                    {
                        InCount += Convert.ToInt32(SID.StockInCount);
                    }
                    string sqlcg = "";
                    //根据产品编号活动采购实际数量
                    if (GAccount.GetAccountInfo().UnitID == "47")
                    {
                        sqlcg = " select * from BGOI_PP.dbo.OrderGoods where DDID in (select DDID from BGOI_PP.dbo.ReceivingInformation where SHID in (select SHID from BGOI_PP.dbo.PurchaseInventorys  where RKID  ='" + strSHID[0] + "'))";
                    }
                    else
                    {
                        sqlcg = " select * from BGOI_PP.dbo.GoodsreceiptDetailed where RKID in(select RKID from BGOI_PP.dbo.PurchaseInventorys where RKID  ='" + strSHID[0] + "')";

                    }
                    DataTable dtcg = SQLBase.FillTable(sqlcg, "MainInventory");
                    foreach (DataRow dr in dtcg.Rows)
                    {
                        CGNum += Convert.ToInt32(dr["Amount"]);
                    }
                }
                if (CGNum == InCount)//实际的大于操作的，没入完
                {
                    string UpPrototnew = "";
                    //if (GAccount.GetAccountInfo().UnitID == "47")
                    //{
                    UpPrototnew = "update  BGOI_PP.dbo.PurchaseInventorys set State='1' where State='0' and RKID ='" + strSHID[0] + "'";
                    //  }
                    //  else
                    //  {
                    //      UpPrototnew = "update  BGOI_PP.dbo.PurchaseInventorys set State='1' where State='0' and RKID ='" + strSHID[0] + "'";

                    //}
                    if (UpPrototnew != "")
                    {
                        SQLBase.ExecuteNonQuery(UpPrototnew);
                    }
                }
                #endregion
                trans.Close(true);

                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockInDetail(ProductID,ProName,Spec,Units,StockInCount,UnitPrice,TotalAmount,Manufacturer,Remark,MainContent,ListInID,DIID,OrderID,BatchNumber,UpState,Numupdate) " +
                            "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockInCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.MainContent + "','" + SID.ListInID + "','" + ProStockInDetialNum(SID.ListInID) + "','" + SID.OrderID + "','" + SID.BatchNumber + "','" + SID.UpState + "','" + SID.Numupdate + "' )";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }
                }
                if (count > 0)
                {
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
        public static bool AddRecordInfo(StockIn record, List<StockInDetail> delist, ref string strErr)
        {
            int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strPID = "";
                foreach (StockInDetail SID in delist)
                {
                    strPID = SID.ProductID;
                    string strupdate = "update [BGOI_PP].[dbo].[PurchaseInventorys] set state='1' where SHID like '%" + strPID + "%'";
                    if (strupdate != "")
                    {
                        trans.ExecuteNonQuery(strupdate, CommandType.Text, null);
                    }
                }


                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                string strInsertList = "";
                if (delist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_Inventory].[dbo].tk_StockInDetail");
                }
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                    //trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }
                trans.Close(true);


                if (count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {
                        //修改库存表中的剩余量
                        DataTable dt = SQLBase.FillTable("  select FinishCount FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "'");
                        if (dt.Rows.Count == 0)
                        {
                            resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location) values('" + SID.ProductID + "','" + SID.StockInCount + "','" + record.HouseID + "','" + record.ProPlace + "')");
                        }
                        else
                        {
                            int newCount = Convert.ToInt32(SID.StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                            resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                        }



                    }
                    if (resultCount > 0)
                    {
                        return true;
                    }
                    else return false;
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
        public static string ProStockInDetialNum(string ListInID)
        {
            string strid = "";
            string Num = ListInID;
            string strN = "";
            string sqlstr = "Select MAX(DIID) From  [BGOI_Inventory].[dbo].[tk_StockInDetail]";
            DataTable dt = SQLBase.FillTable(sqlstr);

            if (dt != null && dt.Rows.Count > 0)
            {
                strid = dt.Rows[0][0].ToString();
                if (strid != "")
                {
                    strN = strid.Substring(0, 12);
                }
                else
                {
                    strN = "";
                }

                if (strid == "" || strid == null || strN != ListInID)
                {
                    Num = ListInID + "01";
                }
                else
                {
                    int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                    if (IntNum < 9)
                    {
                        Num = ListInID + "0" + (IntNum + 1).ToString();
                    }
                    else
                    {
                        Num = ListInID + (IntNum + 1).ToString();
                    }
                }
            }
            return Num;
        }
        public static UIDataTable StockInDetialList(int a_intPageSize, int a_intPageIndex, string ListInID)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Inventory].[dbo].[tk_StockInDetail]where ListInID='" + ListInID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " ListInID='" + ListInID + "'";
            string strOrderBy = " ListInID ";
            String strTable = "[BGOI_Inventory].[dbo].[tk_StockInDetail] ";
            String strField = "ProductID,ProName,Spec,Units,StockInCount,UnitPrice,Manufacturer,Remark,OrderID ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static bool UpDateState(string ListInID)
        {
            int resultCount = 0;
            string HouseID = "";
            string ProPlace = "";
            int StockInCount = 0;

            try
            {
                //return true;
                string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "'";
                DataTable dts = SQLBase.FillTable(sqlstr);

                string sqlstrIn = "   select HouseID,ProPlace FROM [BGOI_Inventory].[dbo].[tk_StockIn] where ListInID='" + ListInID + "' ";
                DataTable dtIn = SQLBase.FillTable(sqlstrIn);
                if (dtIn != null && dtIn.Rows.Count > 0)
                {
                    HouseID = dtIn.Rows[0][0].ToString();
                    ProPlace = dtIn.Rows[0][1].ToString();


                    if (dts != null && dts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dts.Rows)
                        {
                            for (int i = 0; i < dts.Columns.Count; i++)
                            {
                                string ProductID = dr[i].ToString();

                                string strCount = "select SUM(StockInCount) FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "' and ProductID='" + ProductID + "'";
                                DataTable dtcount = SQLBase.FillTable(strCount);
                                if (dtcount != null && dtcount.Rows.Count > 0)
                                {
                                    StockInCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
                                }
                                //修改库存表中的剩余量
                                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                if (dt.Rows.Count == 0)
                                {
                                    resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location,OnlineCount) values('" + ProductID + "','" + StockInCount + "','" + HouseID + "','" + ProPlace + "','0')");
                                }
                                else
                                {
                                    int newCount = Convert.ToInt32(StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                                    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                }

                            }
                        }
                    }
                    else return false;

                    string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockIn] set State='1' where ListInID='" + ListInID + "'";
                    if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                    {

                        return true;
                    }
                    else return false;
                }
                else return false;

            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        public static UIDataTable TestStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                 " from BGOI_Inventory.dbo.tk_StockIn a" +
                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='退货检验' and  ValiDate='v'  and State= '0')" +
                //" )b on a.ListInID=b.ListInID " +
                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='退货检验' and  ValiDate='v'  and State= '0')) " +
                //" )c on c.PID=b.ProductID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  " +
                                " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='退货检验' and  ValiDate='v'  and State= '0')" +
                //" )b on a.ListInID=b.ListInID " +
                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='退货检验' and  ValiDate='v'  and State= '0')) " +
                //" )c on c.PID=b.ProductID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  ";
            String strField = " a.HandwrittenAccount, f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }

                }
            }

            return instData;
        }
        public static UIDataTable ChangeTestList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Sales.dbo.Exchange_Check where IState='0' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " IState='0'";
            string strOrderBy = " TID ";
            String strTable = " BGOI_Sales.dbo.Exchange_Check ";
            String strField = " TID,EID,Brokerage,Convert(varchar(100),ChangeDate,23) as ChangeDate,AppraisalResult ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetSalesReturnTask(string EID)
        {
            //string sql = "select ROW_NUMBER() OVER (ORDER BY b.CreateTime ) AS RowNumber,b.PID,b.OrderContent,b.Specifications,b.Supplier,b.Unit,b.Amount,b.ExUnitNo,b.ExTotalNo,b.Remark from BGOI_Sales.dbo.Exchange_Check a,BGOI_Sales.dbo.SalesReturn_DetailInfo b where a.EID=b.EID and a.State='0' and a.EID='" + EID + "' ";
            // string sql = "select ROW_NUMBER() OVER (ORDER BY b.CreateTime ) AS RowNumber,c.COMNameC,d.Price2,b.ProductID,b.OrderContent,b.Specifications,b.Supplier,b.Unit,b.Amount,b.ExUnitNo,b.ExTotalNo,b.Remark from BGOI_Sales.dbo.Exchange_Check a,BGOI_Sales.dbo.Exchange_Detail b,BGOI_BasMan.dbo.tk_SupplierBas c, BGOI_Inventory.dbo.tk_ProductInfo d where b.ProductID=d.PID and c.SID=d.Manufacturer and a.EID=b.EID and a.IState='0' and a.EID='" + EID + "' ";

            string sql = "";
            string sqlold = "select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID  in (select TID from BGOI_Sales.dbo.Exchange_Check where EID='" + EID + "') ";
            DataTable dtold = SQLBase.FillTable(sqlold, "MainInventory");
            if (dtold.Rows.Count > 0 && dtold != null)
            {
                sql = " select d.COMNameC,c.Price2,b.ProductID,b.OrderContent,b.Specifications,b.Supplier, " +
                        " b.Unit,b.ExUnitNo,b.Remark,   " +//b.ExTotalNo,
                        " ((case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end)*c.Price2)as ExTotalNo, " +
                        " (case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end) as Amount   " +
                        " From BGOI_Sales.dbo.Exchange_Check a  " +
                        " left join BGOI_Sales.dbo.Exchange_Detail b  on a.EID=b.EID   " +
                        " left join  BGOI_Inventory.dbo.tk_ProductInfo c on  b.ProductID=c.PID   " +
                        " left join  BGOI_BasMan.dbo.tk_SupplierBas d on  d.SID=c.Manufacturer    " +
                        " left join [BGOI_Inventory].[dbo].tk_StockInDetail f on a.TID=f.OrderID   and  b.ProductID=f.ProductID   " +
                        " where a.EID='" + EID + "'   " +
                        " and f.OrderID in (select TID from BGOI_Sales.dbo.Exchange_Check where EID='" + EID + "') " +
                        " group by d.COMNameC,c.Price2,b.ProductID,b.OrderContent,b.Specifications,b.Supplier, " +
                        " b.Unit,b.Amount,b.ExUnitNo,b.Remark,f.StockInCount " +
                        "  having  b.Amount-sum(f.StockInCount)>0";
            }
            else
            {
                sql = "select ROW_NUMBER() OVER (ORDER BY b.CreateTime ) AS RowNumber,c.COMNameC,d.Price2,b.ProductID,b.OrderContent,b.Specifications,b.Supplier,b.Unit,b.Amount,b.ExUnitNo,b.ExTotalNo,b.Remark from BGOI_Sales.dbo.Exchange_Check a,BGOI_Sales.dbo.Exchange_Detail b,BGOI_BasMan.dbo.tk_SupplierBas c, BGOI_Inventory.dbo.tk_ProductInfo d where b.ProductID=d.PID and c.SID=d.Manufacturer and a.EID=b.EID and a.IState='0' and a.EID='" + EID + "' ";
            }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static bool AddTestStockIn(StockIn record, string TID, List<StockInDetail> delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                int CGNum = 0;
                int InCount = 0;
                string[] strTID = TID.Split(',');
                #region [查询退货检验单货品实际数量]
                if (delist.Count > 0)
                {
                    string sqlcg1 = " select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID in " +
                        " (select TID from [BGOI_Sales].[dbo].[Exchange_Check] where TID='" + strTID[0] + "')";
                    DataTable dtcg1 = SQLBase.FillTable(sqlcg1, "MainInventory");
                    foreach (DataRow dr1 in dtcg1.Rows)
                    {
                        InCount += Convert.ToInt32(dr1["StockInCount"]);
                    }
                    foreach (StockInDetail SID in delist)
                    {
                        InCount += Convert.ToInt32(SID.StockInCount);
                    }
                    //根据产品编号活动退货检验单数量
                    string sqlcg = " select * from BGOI_Sales.dbo.Exchange_Detail where EID in (select EID from [BGOI_Sales].[dbo].[Exchange_Check] where TID  ='" + strTID[0] + "')";
                    DataTable dtcg = SQLBase.FillTable(sqlcg, "MainInventory");
                    foreach (DataRow dr in dtcg.Rows)
                    {
                        CGNum += Convert.ToInt32(dr["Amount"]);
                    }
                }
                if (CGNum == InCount)//实际的大于操作的，没入完
                {
                    string UpPrototnew = "update [BGOI_Sales].[dbo].[Exchange_Check] set IState='1' where IState='0' and TID = '" + strTID[0] + "'";
                    if (UpPrototnew != "")
                    {
                        SQLBase.ExecuteNonQuery(UpPrototnew);
                    }
                }
                #endregion

                //string strupdate = "";
                //string[] strTID = TID.Split(',');
                //foreach (var i in strTID)
                //{
                //    strupdate = "update [BGOI_Sales].[dbo].[Exchange_Check] set IState='1' where IState='0' and TID = '" + i + "'";
                //    if (strupdate != "")
                //    {
                //        SQLBase.ExecuteNonQuery(strupdate);
                //    }
                //}


                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }

                trans.Close(true);

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {

                        strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockInDetail(ProductID,ProName,Spec,Units,StockInCount,UnitPrice,TotalAmount,Manufacturer,Remark,MainContent,ListInID,DIID,OrderID,BatchNumber,UpState,Numupdate) " +
                            "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockInCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.MainContent + "','" + SID.ListInID + "','" + ProStockInDetialNum(SID.ListInID) + "','" + SID.OrderID + "','" + SID.BatchNumber + "','" + SID.UpState + "','" + SID.Numupdate + "' )";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }
                }



                if (count > 0)
                {
                    return true;
                    //foreach (StockInDetail SID in delist)
                    //{
                    //    //修改库存表中的剩余量
                    //    DataTable dt = SQLBase.FillTable("  select FinishCount FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "'");
                    //    if (dt.Rows.Count == 0)
                    //    {
                    //        resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location) values('" + SID.ProductID + "','" + SID.StockInCount + "','" + record.HouseID + "','" + record.ProPlace + "')");
                    //    }
                    //    else
                    //    {
                    //        int newCount = Convert.ToInt32(SID.StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                    //        resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //    }



                    //}
                    //if (resultCount > 0)
                    //{
                    //    return true;
                    //}
                    //else return false;
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
        public static UIDataTable ProtoStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                " from BGOI_Inventory.dbo.tk_StockIn a" +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  " +
                                " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockIn a" +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  ";
            String strField = " a.ApplyPer,  Convert(varchar(12),a.ApplyTime,111)as ApplyTime,f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }
                }
            }
            return instData;
        }
        public static UIDataTable ChangeProtoList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Sales.dbo.PrototypeDetail a,BGOI_Sales.dbo.PrototypeApply b,BGOI_Sales.dbo.tk_ConfigFiveMalls c where b.Malls=c.ID and a.OperateType='1' and a.IState='0' and a.PAID in (select PAID from  BGOI_Sales.dbo.PrototypeApply where UnitID='" + GAccount.GetAccountInfo().UnitID + "') ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //string strFilter = " b.Malls=c.ID and a.OperateType='1' and a.IState='0' and a.PAID in (select PAID from  BGOI_Sales.dbo.PrototypeApply where UnitID='" + GAccount.GetAccountInfo().UnitID + "') ";
            //string strOrderBy = " a.DID ";
            //String strTable = " BGOI_Sales.dbo.PrototypeDetail a,BGOI_Sales.dbo.PrototypeApply b,BGOI_Sales.dbo.tk_ConfigFiveMalls c";
            //String strField = " a.DID,a.FactoryNum,a.OrderContent,a.Ptype,a.Specifications,a.Supplier,a.Amount,a.UnitPrice,a.Total,a.BusinessType,a.OperateType,c.UnitName ";

            string strFilter = "  a.OperateType='1' and a.IState='0' and a.PAID in (select PAID from  BGOI_Sales.dbo.PrototypeApply where UnitID='" + GAccount.GetAccountInfo().UnitID + "') ";
            string strOrderBy = " a.DID ";
            String strTable = "BGOI_Sales.dbo.PrototypeDetail a " +
                             " left join BGOI_Sales.dbo.PrototypeApply b  on a.PAID=b.PAID " +
                             " left join BGOI_Sales.dbo.tk_ConfigFiveMalls c  on  b.Malls=c.ID";
            String strField = " a.ProductID ,a.DID,a.FactoryNum,a.OrderContent,a.Ptype,a.Specifications,a.Supplier,a.Amount,a.UnitPrice,a.Total,a.BusinessType,a.OperateType,c.UnitName ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetProtoDetail(string DID)
        {

            string sql = "";
            string sqlold = "select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID='" + DID + "' ";
            DataTable dtold = SQLBase.FillTable(sqlold, "MainInventory");
            if (dtold.Rows.Count > 0 && dtold != null)
            {
                sql = " select COMNameC,PAID,DID,a.ProductID,OrderContent,Specifications,a.UnitPrice,a.Remark ," +
                        " ((case when a.Amount-sum(f.StockInCount)>0 then a.Amount-sum(f.StockInCount) else 0 end)*a.UnitPrice)as Total, " +
                        " (case when a.Amount-sum(f.StockInCount)>0 then a.Amount-sum(f.StockInCount) else 0 end) as Amount" +
                        " From BGOI_Sales.dbo.PrototypeDetail a        " +
                        " left join  BGOI_Inventory.dbo.tk_ProductInfo c on  c.PID=a.ProductID     " +
                        " left join  BGOI_BasMan.dbo.tk_SupplierBas d on d.SID=c.Manufacturer   " +
                        " left join [BGOI_Inventory].[dbo].tk_StockInDetail f on a.DID=f.OrderID and a.ProductID=f.ProductID   " +
                        " where a.DID='" + DID + "' and f.OrderID='" + DID + "'  " +
                        " group by COMNameC, a.PAID,DID,c.PID,OrderContent,Specifications,Supplier,c.Units,a.Amount,a.Remark, " +
                        " a.UnitPrice,c.Price2,f.StockInCount,a.ProductID having  a.Amount-sum(f.StockInCount)>0  ";
            }
            else
            {
                sql = "select c.COMNameC,a.PAID,a.DID,a.ProductID,a.OrderContent,a.Specifications,a.Amount,a.UnitPrice,a.Total,a.Remark " +
                      " from BGOI_Sales.dbo.PrototypeDetail a,BGOI_Inventory.dbo.tk_ProductInfo b,BGOI_BasMan.dbo.tk_SupplierBas c  " +
                      " where c.SID=b.Manufacturer and b.PID=a.ProductID and IState='0' and DID='" + DID + "'";
                //sql = "select PAID,DID,ProductID,OrderContent,Specifications,Amount,UnitPrice,Total,Remark from BGOI_Sales.dbo.PrototypeDetail  where IState='0' and DID='" + DID + "'";
            }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static bool SaveProtoStockIn(StockIn record, string DID, List<StockInDetail> delist, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                int CGNum = 0;
                int InCount = 0;
                string[] strDID = DID.Split(',');
                #region [查询实际数量]
                if (delist.Count > 0)
                {
                    string sqlcg1 = " select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID ='" + strDID[0] + "'";
                    DataTable dtcg1 = SQLBase.FillTable(sqlcg1, "MainInventory");
                    foreach (DataRow dr1 in dtcg1.Rows)
                    {
                        InCount += Convert.ToInt32(dr1["StockInCount"]);
                    }
                    foreach (StockInDetail SID in delist)
                    {
                        InCount += Convert.ToInt32(SID.StockInCount);
                    }
                    //根据产品编号活动实际数量
                    string sqlcg = " select * from BGOI_Sales.dbo.PrototypeDetail where DID  ='" + strDID[0] + "'";
                    DataTable dtcg = SQLBase.FillTable(sqlcg, "MainInventory");
                    foreach (DataRow dr in dtcg.Rows)
                    {
                        CGNum += Convert.ToInt32(dr["Amount"]);
                    }
                }
                if (CGNum == InCount)//实际的大于操作的，没入完
                {
                    string UpPrototnew = "update  BGOI_Sales.dbo.PrototypeDetail set IState='1' where IState='0' and DID='" + strDID[0] + "'";
                    if (UpPrototnew != "")
                    {
                        SQLBase.ExecuteNonQuery(UpPrototnew);
                    }
                }
                //else
                //{
                //    string UpPrototnew = "update  BGOI_PP.dbo.PurchaseInventorys set State='1 where State='0' and SHID='" + SHID + "'";
                //    if (UpPrototnew != "")
                //    {
                //        SQLBase.ExecuteNonQuery(UpPrototnew);
                //    }
                //}
                #endregion


                //string UpProtot = "";
                //string[] strDID = DID.Split(',');
                //foreach (var i in strDID)
                //{
                //    UpProtot = "update  BGOI_Sales.dbo.PrototypeDetail set IState='1' where IState='0' and DID='" + i + "'";
                //    if (UpProtot != "")
                //    {
                //        SQLBase.ExecuteNonQuery(UpProtot);
                //    }
                //}

                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockInDetail(ProductID,ProName,Spec,Units,StockInCount,UnitPrice,TotalAmount,Manufacturer,Remark,MainContent,ListInID,DIID,OrderID,BatchNumber,UpState,Numupdate) " +
                            "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockInCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.MainContent + "','" + SID.ListInID + "','" + ProStockInDetialNum(SID.ListInID) + "','" + SID.OrderID + "' ,'" + SID.BatchNumber + "','" + SID.UpState + "','" + SID.Numupdate + "')";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }
                }


                if (count > 0)
                {
                    return true;
                    //foreach (StockInDetail SID in delist)
                    //{
                    //    //修改库存表中的剩余量
                    //    DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(ProtoCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "'");
                    //    if (dt.Rows.Count == 0)
                    //    {
                    //        resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location,ProtoCount) values('" + SID.ProductID + "','" + SID.StockInCount + "','" + record.HouseID + "','" + record.ProPlace + "','" + SID.StockInCount + "')");
                    //    }
                    //    else
                    //    {
                    //        int newCount = Convert.ToInt32(SID.StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                    //        int newProto = Convert.ToInt32(SID.StockInCount) + Convert.ToInt32(dt.Rows[0][1]);
                    //        resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "',ProtoCount='" + newProto + "' where ProductID='" + SID.ProductID + "'");
                    //    }



                    //}
                    //if (resultCount > 0)
                    //{
                    //    return true;
                    //}
                    //else return false;
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
        public static UIDataTable BasicStockInList(int a_intPageSize, int a_intPageIndex, string where)
        {
            #region [新的]
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "";
            if (unitid == "46" || unitid == "32")
            {
                strSelCount = "select COUNT(*) " +
                                " from BGOI_Inventory.dbo.tk_StockIn a" +
                                " left join  BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID where " + where;
            }
            else
            {
                strSelCount = "select COUNT(*) " +
                                    " from BGOI_Inventory.dbo.tk_StockIn a" +
                                    " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                    " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                    " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                    " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID " +
                                    " where  a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " + where;
            }
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;


            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strFilter = where;
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID ";
                strField = " a.HandwrittenAccount,f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,g.HouseName,convert(varchar(20),a.State,120) State ";
            }
            else
            {
                strFilter = " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " + where;
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "' )) g on a.HouseID=g.HouseID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=g.TypeID " +
                                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                  " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
                strField = " a.HandwrittenAccount,f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            }

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }
                }
            }
            return instData;
            #endregion

            #region [冗余]
            //UIDataTable instData = new UIDataTable();
            //string strSelCount = "select COUNT(*) " +
            //                  " from BGOI_Inventory.dbo.tk_StockIn a" +
            //                  " left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where" +
            //                  " HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " +
            //                  " Type='基本' and  ValiDate='v'  and State= '0')" +
            //                  " )b on a.ListInID=b.ListInID " +
            //                  " left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
            //                  " (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where " +
            //                  " HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " +
            //                  " Type='基本' and  ValiDate='v'  and State= '0')) " +
            //                  " )c on c.PID=b.ProductID " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
            //                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) e on a.HouseID=e.HouseID " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID " +
            //                  " where " + where;
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            //string strFilter = where;
            //string strOrderBy = "a.CreateTime ";
            //String strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
            //                  " left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where" +
            //                  " HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " +
            //                  " Type='基本' and  ValiDate='v'  and State= '0')" +
            //                  " )b on a.ListInID=b.ListInID " +
            //                  " left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
            //                  " (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
            //                  " (select ListInID from BGOI_Inventory.dbo.tk_StockIn where " +
            //                  " HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " +
            //                  " Type='基本' and  ValiDate='v'  and State= '0')) " +
            //                  " )c on c.PID=b.ProductID " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
            //                  " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) e on a.HouseID=e.HouseID " +
            //                  " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID ";
            //String strField = " f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            //if (instData == null)
            //{
            //    instData.DtData = null;
            //    instData.IntRecords = 0;
            //    instData.IntTotalPages = 0;
            //}
            //else
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        if (instData.DtData.Rows[r]["State"].ToString() == "0")
            //        {
            //            instData.DtData.Rows[r]["State"] = "未入库";
            //        }
            //        if (instData.DtData.Rows[r]["State"].ToString() == "1")
            //        {
            //            instData.DtData.Rows[r]["State"] = "已入库";
            //        }

            //    }
            //}

            // return instData;
            #endregion
        }
        //成本
        public static UIDataTable WarehousingCostList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "select count(*) from ( select b.ProductID,sum(b.StockInCount) as 's',c.HouseName,b.ProName,b.Spec,b.UnitPrice " +
                                " from BGOI_Inventory.dbo.tk_StockIn a " +
                                " left join BGOI_Inventory.dbo.tk_StockInDetail b on a.ListInID=b.ListInID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse c on a.HouseID=c.HouseID and UnitID='" + GAccount.GetAccountInfo().UnitID + "' " +
                                " where a.ValiDate='v' and c.HouseName!='' and  b.ProductID!='' and b.ProName!='' " +
                                " group by  b.ProductID,c.HouseName,b.ProName,b.Spec,b.UnitPrice " +
                                " ) F ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " F.ProductID ";
            String strTable = " ( select b.ProductID,sum(b.StockInCount) as 's',c.HouseName,b.ProName,b.Spec,b.UnitPrice " +
                                " from BGOI_Inventory.dbo.tk_StockIn a " +
                                " left join BGOI_Inventory.dbo.tk_StockInDetail b on a.ListInID=b.ListInID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse c on a.HouseID=c.HouseID and UnitID='" + GAccount.GetAccountInfo().UnitID + "' " +
                                " where a.ValiDate='v' and c.HouseName!='' and  b.ProductID!='' and b.ProName!='' " +
                                " group by  b.ProductID,c.HouseName,b.ProName,b.Spec,b.UnitPrice " +
                                " ) F ";
            String strField = " F.ProductID,F.s,F.UnitPrice,(F.s*F.UnitPrice) as 'CBJG',F.HouseName,F.ProName,F.Spec ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            string strSelCount = "";
            UIDataTable instData = new UIDataTable();
            if (GAccount.GetAccountInfo().UnitID == "36")
            {

                strSelCount = "select count(*)  From BGOI_Inventory.dbo.tk_ConfigPType where UnitID !='55'";
            }
            else
            {
                strSelCount = "select count(*)  From BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            }
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;


            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (GAccount.GetAccountInfo().UnitID == "36")
            {
                strFilter = " UnitID !='55' ";
                strOrderBy = " ID  ";
                strTable = "BGOI_Inventory.dbo.tk_ConfigPType";
                strField = "ID,Text";
            }
            else
            {

                strFilter = " UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
                strOrderBy = " ID  ";
                strTable = "BGOI_Inventory.dbo.tk_ConfigPType";
                strField = "ID,Text";
            }
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype, string PID)
        {
            string where = "";
            if (PID != "")
            {
                where = " and a.PID like '%" + PID + "%'";
            }
            if (ptype != "")
            {
                where = " and Ptype='" + ptype + "'";
            }
            //else
            //{
            //    where = "";
            //}
            string strSelCount = "";
            UIDataTable instData = new UIDataTable();
            if (GAccount.GetAccountInfo().UnitID == "36")
            {
                strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID !='55') b where a.Ptype=b.ID  and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID !='55') " + where;
            }
            else
            {

                strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.Ptype=b.ID  and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') " + where;
            }
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (GAccount.GetAccountInfo().UnitID == "36")
            {
                strFilter = " a.Manufacturer=c.SID and a.Ptype=b.ID and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID !='55') " + where;
                strOrderBy = " PID ";
                strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID !='55') b,BGOI_BasMan.dbo.tk_SupplierBas c  ";
                strField = " PID,ProName,MaterialNum,Spec,UnitPrice,c.COMNameC,b.Text,a.Price2 ";
            }
            else
            {
                strFilter = " a.Manufacturer=c.SID and a.Ptype=b.ID and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') " + where;
                strOrderBy = " PID ";
                strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b,BGOI_BasMan.dbo.tk_SupplierBas c  ";
                strField = " PID,ProName,MaterialNum,Spec,UnitPrice,c.COMNameC,b.Text,a.Price2 ";
            }
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static UIDataTable ChangePtypeListLinJian(int a_intPageSize, int a_intPageIndex, string ptype, string PID)
        {
            string where = "";
            if (PID != "")
            {
                where = " and a.PID like '%" + PID + "%'";
            }
            if (ptype != "")
            {
                where = " and Ptype='" + ptype + "'";
            }
            //else
            //{
            //    where = "";
            //}

            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.ProTypeID='1' and a.Ptype=b.ID  and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "  a.ProTypeID='1' and a.Ptype=b.ID and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') " + where;
            string strOrderBy = " PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b  ";
            String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text,a.Price2 ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }

        public static UIDataTable ChangePtypeListnew(int a_intPageSize, int a_intPageIndex, string PID, string type, string Spec)
        {
            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            string strSelCount = "";
            string where = "";
            if (PID != "")
            {
                where += " and PID like '%" + PID + "%'";
            }
            else
            {
                where += "";
            }
            if (Spec != "")
            {
                where += " and Spec like '%" + Spec + "%'";
            }
            else
            {
                where += "";
            }
            UIDataTable instData = new UIDataTable();
            if (type != "1")
            {
                //strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b where a.Ptype=b.ID " + where;
                strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where  a.Ptype=b.ID  " + where;
                // and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "')
            }
            else
            {
                strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b where a.ProTypeID=1 and a.Ptype=b.ID  " + where;
                //and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') 
            }
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (type != "1")
            {
                strFilter = "  a.Ptype=b.ID " + where;
                //and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') 
                strOrderBy = " PID ";
                strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b  ";
                strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text,a.Price2 ";


                //strFilter = " a.Ptype=b.ID " + where;
                //strOrderBy = " PID ";
                //strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b  ";
                //strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text ";
            }
            else//成品定义
            {

                strFilter = " a.ProTypeID=1 and a.Ptype=b.ID" + where;
                // and a.Ptype in (select ID from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') 
                strOrderBy = " PID ";
                strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "') b  ";
                strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text,a.Price2 ";
            }
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetBasicDetail(string PID)
        {
            string sql = "select PID,ProName,Spec,Manufacturer,COMNameC,UnitPrice,Price2,Units,Remark From BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_BasMan.dbo.tk_SupplierBas b  where a.Manufacturer=b.SID   AND PID in (" + PID + ")";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        //批号
        public static DataTable GetBasicOUTDetail(string PID)
        {
            string sql = "select PID,ProName,Spec,Manufacturer,COMNameC,UnitPrice,Price2,Units,Remark,c.x,c.ProductID From BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_BasMan.dbo.tk_SupplierBas b,(select MIN(BatchNumber) as x,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ProductID in (" + PID + ") group by ProductID) c where a.Manufacturer=b.SID and a.PID=c.ProductID AND PID in (" + PID + ")";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static DataTable GetBasicDetailSpec(string PID)
        {
            string sql = "select PID,ProName,Spec,Manufacturer,COMNameC,UnitPrice,Price2,Units,Remark From BGOI_Inventory.dbo.tk_ProductInfo a ,BGOI_BasMan.dbo.tk_SupplierBas b  where a.Manufacturer=b.SID AND PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static bool SaveBasicStockIn(StockIn record, List<StockInDetail> delist, ref string strErr)
        {

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockInDetail(ProductID,ProName,Spec,Units,StockInCount,UnitPrice,TotalAmount,Manufacturer,Remark,MainContent,ListInID,DIID,BatchNumber,UpState,Numupdate) " +
                            "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockInCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.MainContent + "','" + SID.ListInID + "','" + ProStockInDetialNum(SID.ListInID) + "','" + SID.BatchNumber + "','" + SID.UpState + "','" + SID.Numupdate + "' )";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }
                }

                if (count > 0)
                {
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
        public static bool UpProtoDateState(string ListInID)
        {
            int resultCount = 0;
            string HouseID = "";
            string ProPlace = "";
            int StockInCount = 0;

            try
            {
                string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "'";
                DataTable dts = SQLBase.FillTable(sqlstr);

                string sqlstrIn = "   select HouseID,ProPlace FROM [BGOI_Inventory].[dbo].[tk_StockIn] where ListInID='" + ListInID + "' ";
                DataTable dtIn = SQLBase.FillTable(sqlstrIn);
                if (dtIn != null && dtIn.Rows.Count > 0)
                {
                    HouseID = dtIn.Rows[0][0].ToString();
                    ProPlace = dtIn.Rows[0][1].ToString();


                    if (dts != null && dts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dts.Rows)
                        {
                            for (int i = 0; i < dts.Columns.Count; i++)
                            {
                                string ProductID = dr[i].ToString();

                                string strCount = "select SUM(StockInCount) FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "' and ProductID='" + ProductID + "'";
                                DataTable dtcount = SQLBase.FillTable(strCount);
                                if (dtcount != null && dtcount.Rows.Count > 0)
                                {
                                    StockInCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
                                }
                                //修改库存表中的剩余量
                                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(ProtoCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                if (dt.Rows.Count == 0)
                                {
                                    resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location,ProtoCount,OnlineCount) values('" + ProductID + "','" + StockInCount + "','" + HouseID + "','" + ProPlace + "','" + StockInCount + "','0')");
                                }
                                else
                                {
                                    int newCount = Convert.ToInt32(StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                                    int newProto = Convert.ToInt32(StockInCount) + Convert.ToInt32(dt.Rows[0][1]);
                                    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "',ProtoCount='" + newProto + "' where ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");

                                }

                            }
                        }
                    }
                    else return false;

                    string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockIn] set State='1' where ListInID='" + ListInID + "'";
                    if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                    {

                        return true;
                    }
                    else return false;
                }

                else return false;

            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #region [生产组装入库]
        //加载生产数据
        public static UIDataTable ChangeProductionList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            //string strSelCount = " select COUNT(*) from BGOI_Produce.dbo.tk_PStocking a,BGOI_Produce.dbo.tk_PStockingDetail b where Type='生产组装入库' and State='0' and a.RKID=b.RKID ";
            string strSelCount = " select COUNT(*) from BGOI_Produce.dbo.tk_PStocking  where Type='生产组装入库' and State='0'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " Type='生产组装入库' and State='0'";
            string strOrderBy = " RKID ";
            String strTable = " BGOI_Produce.dbo.tk_PStocking  ";
            String strField = " RKID, RWID, StockInTime, HouseID, StockRemark, StockInUser, Type, UnitID, State, CreateTime, CreateUser, Validate, FinishTime, MaterState, Batch, Storekeeper ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        //加入到入库表中
        public static bool SaveProductionStockIn(StockIn record, string RKID, List<StockInDetail> delist, ref string strErr)
        {

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {

                int CGNum = 0;
                int InCount = 0;
                string[] strRKID = RKID.Split(',');
                #region [查询采购单货品实际数量]
                if (delist.Count > 0)
                {
                    string sqlcg1 = " select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID ='" + strRKID[0] + "'";
                    DataTable dtcg1 = SQLBase.FillTable(sqlcg1, "MainInventory");
                    foreach (DataRow dr1 in dtcg1.Rows)
                    {
                        InCount += Convert.ToInt32(dr1["StockInCount"]);
                    }
                    foreach (StockInDetail SID in delist)
                    {
                        InCount += Convert.ToInt32(SID.StockInCount);
                    }
                    //根据产品编号活动采购实际数量
                    string sqlcg = " select * from [BGOI_Produce].[dbo].tk_PStockingDetail where   RKID ='" + strRKID[0] + "'";
                    DataTable dtcg = SQLBase.FillTable(sqlcg, "MainInventory");
                    foreach (DataRow dr in dtcg.Rows)
                    {
                        CGNum += Convert.ToInt32(dr["Amount"]);
                    }
                }
                if (CGNum == InCount)//实际的大于操作的，没入完
                {
                    string UpPrototnew = "update   BGOI_Produce.dbo.tk_PStocking set State='1' where State='0' and RKID ='" + strRKID[0] + "'";
                    if (UpPrototnew != "")
                    {
                        SQLBase.ExecuteNonQuery(UpPrototnew);
                    }
                }
                #endregion



                //string UpProtot = "";
                //string[] strRKID = RKID.Split(',');
                //foreach (var i in strRKID)
                //{
                //    UpProtot = "update  BGOI_Produce.dbo.tk_PStocking set State='1' where State='0' and RKID='" + i + "'";
                //    if (UpProtot != "")
                //    {
                //        SQLBase.ExecuteNonQuery(UpProtot);
                //    }
                //}

                string strInsert = GSqlSentence.GetInsertInfoByD<StockIn>(record, "[BGOI_Inventory].[dbo].tk_StockIn");
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);

                string strInsertList = "";
                if (delist.Count > 0)
                {
                    foreach (StockInDetail SID in delist)
                    {
                        strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockInDetail(ProductID,ProName,Spec,Units,StockInCount,UnitPrice,TotalAmount,Manufacturer,Remark,MainContent,ListInID,DIID,OrderID,BatchNumber,UpState,Numupdate) " +
                            "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockInCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.MainContent + "','" + SID.ListInID + "','" + ProStockInDetialNum(SID.ListInID) + "','" + SID.OrderID + "','" + SID.BatchNumber + "','" + SID.UpState + "','" + SID.Numupdate + "' )";

                        if (strInsertList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertList);
                        }
                    }
                }


                if (count > 0)
                {
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
        public static DataTable GetProductionDetail(string DID)
        {
            string sql = "";
            string sqlold = "select * from [BGOI_Inventory].[dbo].tk_StockInDetail where OrderID='" + DID + "' ";
            DataTable dtold = SQLBase.FillTable(sqlold, "MainInventory");
            if (dtold.Rows.Count > 0 && dtold != null)
            {
                sql = "  select COMNameC, b.RKID as RKID,DID,B.PID,OrderContent, " +
                      "  Specifications,Supplier,c.Units,c.Remark,c.UnitPrice,c.Price2,g.HouseName,g.HouseID,    " +
                      " ((case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end)*c.Price2)as TotalNoTax," +
                      "  (case when b.Amount-sum(f.StockInCount)>0 then b.Amount-sum(f.StockInCount) else 0 end) as Amount     " +
                      "  From BGOI_Produce.dbo.tk_PStocking a    " +
                      "  left join BGOI_Produce.dbo.tk_PStockingDetail b  on a.RKID=b.RKID   " +
                      "  left join  BGOI_Inventory.dbo.tk_ProductInfo c on  c.PID=b.PID     " +
                      "  left join   BGOI_BasMan.dbo.tk_SupplierBas d on d.SID=c.Manufacturer     " +
                      "  left join [BGOI_Inventory].[dbo].tk_StockInDetail f on a.RKID=f.OrderID   " +
                      "  and  b.PID=f.ProductID "+
                      "  left join [BGOI_Inventory].dbo.tk_WareHouse g on a.HouseID=g.HouseName   " +
                      "  where a.RKID='" + DID + "'  " +
                      "  and f.OrderID='" + DID + "'  " +
                      "  group by COMNameC, b.RKID,DID,B.PID,OrderContent, " +
                      "  Specifications,Supplier,c.Units,Amount,c.Remark,c.UnitPrice,c.Price2,g.HouseName,g.HouseID,f.StockInCount    " +
                      "  having  b.Amount-sum(f.StockInCount)>0 ";
            }
            else
            {
                sql = " select COMNameC, b.RKID as RKID,DID,B.PID,OrderContent,Specifications,Supplier,c.Units,Amount,c.Remark,c.UnitPrice,c.Price2,g.HouseName,g.HouseID " +
                      " from BGOI_Produce.dbo.tk_PStocking a left join BGOI_Produce.dbo.tk_PStockingDetail b on a.RKID=b.RKID " +
                      " left join BGOI_Inventory.dbo.tk_ProductInfo c  on c.PID=b.PID " +
                      " left join BGOI_BasMan.dbo.tk_SupplierBas d  on c.Manufacturer=d.SID " +
                      " left join [BGOI_Inventory].dbo.tk_WareHouse g on a.HouseID=g.HouseName   " +
                      " where  a.State='0' and a.RKID='" + DID + "'";
            }
            //string sql = "select COMNameC, b.RKID as RKID,DID,B.PID,OrderContent,Specifications,Supplier,c.Units,Amount,c.Remark,c.UnitPrice,c.Price2 " +
            //             "from BGOI_Produce.dbo.tk_PStocking a left join BGOI_Produce.dbo.tk_PStockingDetail b on a.RKID=b.RKID " +
            //             " left join BGOI_Inventory.dbo.tk_ProductInfo c  on c.PID=b.PID " +
            //              " left join BGOI_BasMan.dbo.tk_SupplierBas d  on c.Manufacturer=d.SID " +
            //             "where  a.State='0' and a.RKID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        //入库完成
        public static bool UpProductionDateState(string ListInID)
        {
            int resultCount = 0;
            string HouseID = "";
            string ProPlace = "";
            int StockInCount = 0;

            try
            {
                string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "'";
                DataTable dts = SQLBase.FillTable(sqlstr);

                string sqlstrIn = "   select HouseID,ProPlace FROM [BGOI_Inventory].[dbo].[tk_StockIn] where ListInID='" + ListInID + "' ";
                DataTable dtIn = SQLBase.FillTable(sqlstrIn);
                if (dtIn != null && dtIn.Rows.Count > 0)
                {
                    HouseID = dtIn.Rows[0][0].ToString();
                    ProPlace = dtIn.Rows[0][1].ToString();


                    if (dts != null && dts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dts.Rows)
                        {
                            for (int i = 0; i < dts.Columns.Count; i++)
                            {
                                string ProductID = dr[i].ToString();

                                string strCount = "select SUM(StockInCount) FROM [BGOI_Inventory].[dbo].[tk_StockInDetail] where ListInID='" + ListInID + "' and ProductID='" + ProductID + "'";
                                DataTable dtcount = SQLBase.FillTable(strCount);
                                if (dtcount != null && dtcount.Rows.Count > 0)
                                {
                                    StockInCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
                                }
                                //修改库存表中的剩余量
                                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(OnlineCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                if (dt.Rows.Count == 0)
                                {
                                    resultCount = SQLBase.ExecuteNonQuery(" insert into [BGOI_Inventory].[dbo].[tk_StockRemain](ProductID,FinishCount,HouseID,Location,OnlineCount) values('" + ProductID + "','" + StockInCount + "','" + HouseID + "','" + ProPlace + "',0')");
                                }
                                else
                                {
                                    int newCount = Convert.ToInt32(StockInCount) + Convert.ToInt32(dt.Rows[0][0]);
                                    int newOnline = Convert.ToInt32(dt.Rows[0][1]) - Convert.ToInt32(StockInCount);
                                    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "',OnlineCount='" + newOnline + "' where ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                }
                            }
                        }
                    }
                    else return false;

                    string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockIn] set State='1' where ListInID='" + ListInID + "'";
                    if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                    {

                        return true;
                    }
                    else return false;
                }

                else return false;

            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        public static UIDataTable ProtoStockInListShengChan(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockIn a" +
                                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='生产组装入库' and  ValiDate='v'  and State= '0')" +
                                //" )b on a.ListInID=b.ListInID " +
                                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='生产组装入库' and  ValiDate='v'  and State= '0')) " +
                                //" )c on c.PID=b.ProductID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  " +
                                " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockIn a" +
                                //" left join (select distinct ListInID,ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in " +
                                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='生产组装入库' and  ValiDate='v'  and State= '0')" +
                                //" )b on a.ListInID=b.ListInID " +
                                //" left join (select * from BGOI_Inventory.dbo.tk_ProductInfo where PID in " +
                                //" (select distinct ProductID from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in  " +
                                //" (select ListInID from BGOI_Inventory.dbo.tk_StockIn where Type='生产组装入库' and  ValiDate='v'  and State= '0')) " +
                                //" )c on c.PID=b.ProductID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                " left join BGOI_Inventory.dbo.tk_ConfigProType f on f.OID=e.TypeID  ";
            String strField = " f.Text as T,a.Remark,a.ListInID,a.BatchID,d.Text as SubjectName,Convert(varchar(12),a.StockInTime,111) as StockInTime,a.StockInUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }

                }
            }

            return instData;
        }
        #endregion
        #endregion
        #region [货品出库]
        #region [零售销售出库]
        public static UIDataTable RetailSalesOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "a.Purchase,a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }
                }
            }
            return instData;
        }
        public static DataTable GetStockOutUse()
        {
            // string str = "select ID,Text From [BGOI_Inventory].[dbo].[tk_StockOutUse]";
            string str = "select ID,Text From [BGOI_Inventory].[dbo].tk_ProductSelect_Config where Type='OutUses'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static UIDataTable OrderInfoSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa03'  and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa03' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            string strOrderBy = " a.OrderID ";
            String strTable = " [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b ";
            String strField = " a.DID,a.OrderID,a.ProductID,OrderContent,SpecsModels,Manufacturer,b.OrderContactor,b.Remark,a.OrderUnit,OrderNum,Price,Subtotal,b.DeliveryTime,b.SalesType  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetOrderSalesDetail(string DID)
        {
            //string sql = "select OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Price,Subtotal,DeliveryTime,Remark from [BGOI_Sales].[dbo].[Orders_DetailInfo]  where State='0' and DID='" + DID + "'";
            string sql = "select a.OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,b.OrderContactor," +
                         " a.OrderUnit,OrderNum,Price,Subtotal,a.DeliveryTime,b.Remark " +
                         " from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b " +
                         " where a.IState='0' and a.OrderID=b.OrderID and DID='" + DID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static string GetTopListOutID()
        {
            string strID = "";
            string strD = "B" + DateTime.Now.ToString("yyyyMMdd");
            string strSqlID = "select max(ListOutID) from BGOI_Inventory.dbo.tk_StockOut where ValiDate='v'";
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
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));
                    string stryyyyMMdd = strID.Substring(1, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "0" + (num + 1);

                        else
                            strD = strD + (num + 1);
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
        public static bool SaveOrderSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "'and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    return true;
                    //    //int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    //resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }
            }
            string strInsertList = "";

            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual,BatchID) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "','" + SID.BatchID + "' )";
                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }
            else return false;
            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  BGOI_Sales.dbo.Orders_DetailInfo set IState='1' where IState='0' and DID='" + i + "'";
                if (UpProtot != "")
                {
                    int ss = SQLBase.ExecuteNonQuery(UpProtot);
                }
            }
            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
                return true;
            }
            else return false;
        }
        public static string ProStockOutDetialNum(string ListOutID)
        {
            string strid = "";
            string Num = "";
            string sqlstr = "Select MAX(DOID) From  [BGOI_Inventory].[dbo].[tk_StockOutDetail]";
            DataTable dt = SQLBase.FillTable(sqlstr);

            if (dt != null && dt.Rows.Count > 0)
            {
                strid = dt.Rows[0][0].ToString();

                if (strid == "" || strid == null)
                {
                    Num = ListOutID + "01";
                }
                //string strN = strid.Substring(0, 12);
                //if (strid == "" || strid == null || strN != ListOutID)
                //{
                //    Num = ListOutID + "01";
                //}

                else
                {
                    string strN = strid.Substring(0, 12);
                    if (strN != ListOutID)
                    {
                        Num = ListOutID + "01";
                    }
                    else
                    {
                        int IntNum = Convert.ToInt32(strid.Substring(strid.Length - 2, 2));

                        if (IntNum < 9)
                        {
                            Num = ListOutID + "0" + (IntNum + 1).ToString();
                        }
                        else
                        {
                            Num = ListOutID + (IntNum + 1).ToString();
                        }
                    }
                }
            }
            else
            {
                Num = ListOutID + "01";
            }
            return Num;
        }
        public static UIDataTable StockOutDetialList(int a_intPageSize, int a_intPageIndex, string ListOutID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "ListOutID='" + ListOutID + "'";
            string strOrderBy = " ListOutID ";
            String strTable = "[BGOI_Inventory].[dbo].[tk_StockOutDetail] ";
            String strField = " BatchID,ProductID,ProName,Spec,Units,StockOutCount,StockOutCountActual,UnitPrice,Manufacturer,Remark,OrderID ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }

            return instData;
        }
        public static bool UpOutDateState(string ListOutID)
        {
            string HouseID = "";
            int StockOutCount = 0;
            int resultCount = 0;
            try
            {
                string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "'";
                DataTable dts = SQLBase.FillTable(sqlstr);
                string sqlstrIn = "   select HouseID FROM [BGOI_Inventory].[dbo].[tk_StockOut] where ListOutID='" + ListOutID + "' ";
                DataTable dtIn = SQLBase.FillTable(sqlstrIn);
                if (dtIn != null && dtIn.Rows.Count > 0)
                {
                    HouseID = dtIn.Rows[0][0].ToString();
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dts.Rows)
                        {
                            for (int i = 0; i < dts.Columns.Count; i++)
                            {
                                string ProductID = dr[i].ToString();
                                //string strCount = "select SUM(StockOutCount) FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "' and ProductID='" + ProductID + "'";//申请数量
                                string strCount = "select SUM(StockOutCountActual) FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "' and ProductID='" + ProductID + "'";//实际数量
                                DataTable dtcount = SQLBase.FillTable(strCount);
                                if (dtcount != null && dtcount.Rows.Count > 0)
                                {
                                    StockOutCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
                                }
                                //修改库存表中的剩余量
                                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(OnlineCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                                {
                                    return false;
                                }
                                else
                                {
                                    int newOnline = Convert.ToInt32(dt.Rows[0][1]) + Convert.ToInt32(StockOutCount);
                                    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(StockOutCount);
                                    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "',OnlineCount='" + newOnline + "'  where ProductID='" + ProductID + "'and HouseID='" + HouseID + "'");
                                }
                            }
                        }
                    }
                    else return false;
                }
                string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockOut] set State='1' where ListOutID='" + ListOutID + "'";
                if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion
        #region [项目销售出库]
        public static UIDataTable ProjectSalesOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "a.Remark,a.Purchase,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }
                }
            }
            return instData;
        }
        public static UIDataTable OrderProSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa01' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa01' and UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            string strOrderBy = " a.OrderID ";
            String strTable = " [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b ";
            String strField = " a.DID,a.OrderID,a.ProductID,OrderContent,SpecsModels,Manufacturer,a.OrderUnit,OrderNum,Price,Subtotal,a.DeliveryTime,b.SalesType  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static bool SaveOrderProSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{
            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''   and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            //int a = Convert.ToInt32(dr["Numupdate"]);
                            //int b = Convert.ToInt32(dr["StockInCount"]);
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion

                    string pid = SID.ProductID;
                    string ss = ProStockOutDetialNum(SID.ListOutID);
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "' )";
                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }
            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  BGOI_Sales.dbo.Orders_DetailInfo set IState='1' where IState='0' and DID='" + i + "'";
                if (UpProtot != "")
                {
                    SQLBase.ExecuteNonQuery(UpProtot);
                }
            }
            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
                if (count > 0)
                {
                    return true;
                }
                else return false;
                //}
                //else return false;
            }
            else
                return false;
        }
        #endregion
        #region [二级库出库]
        public static UIDataTable SecondOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }

                }
            }

            return instData;
        }
        public static bool SaveSecondOut(StockOut record, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;

            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{

            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }
            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!=''  and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            //int a = Convert.ToInt32(dr["Numupdate"]);
                            //int b = Convert.ToInt32(dr["StockInCount"]);
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    string pid = SID.ProductID;
                    string ss = ProStockOutDetialNum(SID.ListOutID);
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "' )";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }

                return true;
            }
            else
                return false;


        }
        #endregion
        #region [上样机出库]
        public static UIDataTable ProtoUpDetailList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Sales.dbo.PrototypeDetail where IState='0' and OperateType='0' and PAID in (select PAID from BGOI_Sales.dbo.PrototypeApply where UnitID='" + GAccount.GetAccountInfo().UnitID + "') ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " IState='0' and OperateType='0' and PAID in (select PAID from BGOI_Sales.dbo.PrototypeApply where UnitID='" + GAccount.GetAccountInfo().UnitID + "')  ";
            string strOrderBy = " DID ";
            String strTable = " BGOI_Sales.dbo.PrototypeDetail ";
            String strField = " DID,ProductID,FactoryNum,OrderContent,Ptype,Specifications,Supplier,Amount,UnitPrice,Total,BusinessType,OperateType ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetProtoUpDetail(string DID)
        {
            string sql = "select PAID,DID,ProductID,OrderContent,Specifications,Supplier,Amount,UnitPrice,Total,Remark from BGOI_Sales.dbo.PrototypeDetail  where IState='0' and DID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static bool SaveProtoUpOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;

            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(ProtoCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    //int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    //resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{
            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  BGOI_Sales.dbo.PrototypeDetail set IState='1' where IState='0' and OperateType='0' and DID='" + i + "'";
                if (UpProtot != "")
                {
                    SQLBase.ExecuteNonQuery(UpProtot);
                }
            }

            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }
            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {

                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            //int a = Convert.ToInt32(dr["Numupdate"]);
                            //int b = Convert.ToInt32(dr["StockInCount"]);
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    string pid = SID.ProductID;
                    string ss = ProStockOutDetialNum(SID.ListOutID);
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "' )";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);

                    }
                }
                return true;
            }

            else return false;
            //}
            //else
            //    return false;


        }
        public static UIDataTable ProtoUpOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }

                }
            }

            return instData;
        }
        #endregion
        #region [内购/赠送出库]
        public static UIDataTable BuyGiveOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "a.Purchase,a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }

                }
            }

            return instData;
        }
        public static UIDataTable InternalDetailList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Sales.dbo.Internal_Detail where IState='0' and IOID in (select IOID from BGOI_Sales.dbo.InternalOrder where UnitID='" + GAccount.GetAccountInfo().UnitID + "') ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " IState='0' and IOID in (select IOID from BGOI_Sales.dbo.InternalOrder where UnitID='" + GAccount.GetAccountInfo().UnitID + "')  ";
            string strOrderBy = " DID ";
            String strTable = " BGOI_Sales.dbo.Internal_Detail ";
            String strField = " DID,ProductID,OrderContent,GoodsType,Specifications,Supplier,Amount,UnitPrice,Total ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetBuyGiveDetail(string DID)
        {
            string sql = "select IOID,DID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,UnitPrice,Total,Remark from BGOI_Sales.dbo.Internal_Detail  where IState='0' and DID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static bool SaveBuyGiveOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;

            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{
            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  BGOI_Sales.dbo.Internal_Detail set IState='1' where IState='0' and DID='" + i + "'";
                if (UpProtot != "")
                {
                    SQLBase.ExecuteNonQuery(UpProtot);
                }
            }

            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }
            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            //int a = Convert.ToInt32(dr["Numupdate"]);
                            //int b = Convert.ToInt32(dr["StockInCount"]);
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    string pid = SID.ProductID;
                    string ss = ProStockOutDetialNum(SID.ListOutID);
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "' )";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }

                return true;
            }
            else
                return false;


        }
        #endregion
        #region [专柜出库]
        public static DataTable GetCounterSalesDetail(string DID)
        {
            string sql = "select SIID,DID,MaterialNum,OrderContent,Specifications,Amount,Remark,Price,Company,Total,CreateTime,CreateUser from [BGOI_Sales].[dbo].ShoppeInfoDetail where State='0' and DID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        public static UIDataTable CounterOutSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from [BGOI_Sales].[dbo].[ShoppeInfoDetail] a,[BGOI_Sales].[dbo].ShoppeInfo b where State='0' and a.SIID=b.SIID and b.MakeType='专柜制作' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.State='0' and a.SIID=b.SIID and b.MakeType='专柜制作'";
            string strOrderBy = " a.SIID ";
            String strTable = " [BGOI_Sales].[dbo].[ShoppeInfoDetail] a,[BGOI_Sales].[dbo].ShoppeInfo b ";
            String strField = " a.SIID,a.DID,a.OrderContent,a.Specifications,a.Amount,a.Remark,a.Price,a.Total,a.CreateTime,a.CreateUser,b.MakeType  ";
            //a.MaterialNum,a.Company,
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static bool SaveCounterSalesOut(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;

            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(ProtoCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {

                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "' )";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }

            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  BGOI_Sales.dbo.ShoppeInfoDetail set State='1' where State='0' and DID='" + i + "'";
                if (UpProtot != "")
                {
                    int ss = SQLBase.ExecuteNonQuery(UpProtot);

                }
            }

            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);


                return true;
            }
            else return false;



        }
        #endregion
        #region [基本出库]
        public static UIDataTable BasicOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "";
            if (unitid == "46" || unitid == "32")
            {
                strSelCount = "select COUNT(*) " +
                              " from BGOI_Inventory.dbo.tk_StockOut a" +
                              " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                              " left join BGOI_Inventory.dbo.tk_WareHouse where " + where;
            }
            else
            {
                strSelCount = "select COUNT(*) " +
                                       " from BGOI_Inventory.dbo.tk_StockOut a" +
                                         " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                         " left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "''))  e on a.HouseID=e.HouseID " +
                                         " where  a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " + where;
            }

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strFilter = where;
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                         "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
                strField = "a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            }
            else
            {
                strFilter = " a.HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and " + where;
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                         "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  left join (select HouseID,HouseName from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "'))  e on a.HouseID=e.HouseID ";
                strField = "a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            }
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }
                }
            }
            return instData;
        }
        public static bool SaveBacicOut(StockOut record, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            int oldcount = 0;
            int newcount = 0;
            foreach (StockOutDetail SID in delist)
            {

                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("select ISNULL(FinishCount,0) as fnum FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    newcount += dt.Rows.Count;
                    oldcount += Convert.ToInt32(dr["fnum"]);
                }
            }
            if (newcount == 0 || oldcount == 0)//Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                strErr = "该物料在仓库中没有剩余量";
                return false;
            }
            else
            {

                if (oldcount < Convert.ToInt32(Count))//Convert.ToInt32(dt.Rows[0][0])
                {
                    strErr = "库存不足，请重新修改出库数量";
                    return false;
                }
                //else
                //{
                //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                //}
            }

            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
            }

            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    string pid = SID.ProductID;
                    string ss = ProStockOutDetialNum(SID.ListOutID);
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual,BatchID) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "','" + SID.BatchID + "' )";

                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }

                return true;
            }
            else
                return false;


        }
        #endregion
        #region [生产领料单出库]
        public static bool SaveProductionMaterials(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(ProtoCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "' and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                    //else
                    //{
                    //    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID.StockOutCount);
                    //    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID.ProductID + "'");
                    //}
                }

            }
            //if (resultCount > 0)
            //{
            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string strInsertList = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''  and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            //int a = Convert.ToInt32(dr["Numupdate"]);
                            //int b = Convert.ToInt32(dr["StockInCount"]);
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "' )";
                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }
            string UpProtot = "";
            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                UpProtot = "update  [BGOI_Produce].[dbo].[tk_MaterialForm] set State='1' where State='0' and LLID IN (select LLID from [BGOI_Produce].[dbo].[tk_MaterialFDetail] where DID='" + i + "')";
                if (UpProtot != "")
                {
                    int ss = SQLBase.ExecuteNonQuery(UpProtot);
                }
            }
            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
                return true;
            }
            else return false;
        }
        public static UIDataTable ProductionMaterialsOutSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            //string strSelCount = " select COUNT(*) from [BGOI_Produce].[dbo].[tk_MaterialForm] a,[BGOI_Produce].[dbo].tk_MaterialFDetail b where a.State='0' and a.LLID=b.LLID ";

            string strSelCount = " select COUNT(*) from [BGOI_Produce].[dbo].[tk_MaterialForm]  where State='0' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " State='0' ";
            string strOrderBy = " LLID ";
            String strTable = " [BGOI_Produce].[dbo].[tk_MaterialForm] ";
            String strField = " LLID,Amount,CreateTime,CreateUser ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetProductionMaterialsSalesDetail(string DID)
        {
            string sql = "select a.LLID, a.DID, a.PID, a.OrderContent, a.SpecsModels, a.Specifications, a.Manufacturer, a.OrderUnit, a.OrderNum, a.Technology, a.BatchID, a.DeliveryTime,a.Remark,a.IdentitySharing from [BGOI_Produce].[dbo].tk_MaterialFDetail a,[BGOI_Produce].[dbo].tk_MaterialForm b  where a.LLID=b.LLID and b.State='0' and a.LLID='" + DID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #region [家用产品销售]
        public static UIDataTable ChangeHomeProductSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where  a.OrderID=b.OrderID and b.SalesType='Sa03'  and LibraryTubeState='0'  and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "  a.OrderID=b.OrderID and b.SalesType='Sa03' and b.LibraryTubeState='0'  and b.UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            string strOrderBy = " a.OrderID ";
            String strTable = " [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b ";
            String strField = " a.DID,a.OrderID,a.ProductID,OrderContent,SpecsModels,Manufacturer,b.OrderContactor,b.Remark,a.OrderUnit,OrderNum,Price,Subtotal,b.DeliveryTime,b.SalesType  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetHomeProductSales(string DID)
        {
            string sql = "select a.OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,b.OrderContactor," +
                         " a.OrderUnit,OrderNum,Price,Subtotal,a.DeliveryTime,b.Remark " +
                         " from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b " +
                         " where b.LibraryTubeState='0'  and b.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and a.OrderID=b.OrderID and DID='" + DID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetOrderID(string ListOutID)
        {
            string sql = " select *from [BGOI_Inventory].dbo.tk_StockOutDetail where ListOutID='" + ListOutID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static bool SaveHomeProductSales(StockOut record, string DID, string Count, List<StockOutDetail> delist, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            foreach (StockOutDetail SID in delist)
            {
                //修改库存表中的剩余量
                DataTable dt = SQLBase.FillTable(" select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.ProductID + "'and HouseID='" + record.HouseID + "'");
                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    strErr = "该物料在仓库中没有剩余量";
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0][0]) < Convert.ToInt32(Count))
                    {
                        strErr = "库存不足，请重新修改出库数量";
                        return false;
                    }
                }
            }
            string strInsertList = "";

            int fnum = 0;
            int infonum = 0;
            int Numnew = 0;
            string OrderID = "";
            if (delist.Count > 0)
            {
                foreach (StockOutDetail SID in delist)
                {
                    OrderID = SID.OrderID;
                    #region [先入先出]
                    //查看入库单入库数量（先入先出）
                    DataTable dtin = SQLBase.FillTable("select *from BGOI_Inventory.dbo.tk_StockInDetail  where UpState='0' and BatchNumber!='' and Numupdate!=''   and  ProductID='" + SID.ProductID + "' order by BatchNumber asc ");
                    for (int i = 0; i <= SID.StockOutCount; i++)
                    {
                        foreach (DataRow dr in dtin.Rows)
                        {
                            Numnew = Convert.ToInt32(dr["StockInCount"]) - Convert.ToInt32(dr["Numupdate"]);
                            if (Numnew > 0)//负数，说明有剩余
                            {
                                if (fnum == 0)//第一次循环
                                {
                                    //把剩余的先出库
                                    fnum = Convert.ToInt32(SID.StockOutCount) - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID = dr["BatchNumber"].ToString();
                                }
                                else
                                {
                                    //把剩余的先出库
                                    fnum = fnum - Numnew;// -Convert.ToInt32(dr["Numupdate"]);
                                    SID.BatchID += "," + dr["BatchNumber"].ToString();
                                }
                                if (fnum < 0)//出库完成
                                {
                                    infonum = -fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else if (fnum == 0)
                                {
                                    infonum = fnum;
                                    i = SID.StockOutCount;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (i == SID.StockOutCount)
                        {
                            break;
                        }
                    }
                    string updatestr = "";
                    string[] arrBatchID = SID.BatchID.Split(',');
                    for (int i = 0; i < arrBatchID.Length; i++)
                    {
                        if (i == arrBatchID.Length - 1)
                        {
                            if (infonum != 0)
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate='" + infonum + "' where ProductID='" + SID.ProductID + "' and BatchNumber='" + arrBatchID[arrBatchID.Length - 1].ToString() + "'";
                            }
                            else
                            {
                                updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                            }
                        }
                        else
                        {
                            updatestr += "  update  BGOI_Inventory.dbo.tk_StockInDetail set Numupdate=StockInCount,UpState='1' where  ProductID='" + SID.ProductID + "' and  BatchNumber='" + arrBatchID[i].ToString() + "'";
                        }
                    }
                    SQLBase.ExecuteNonQuery(updatestr);
                    #endregion
                    strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,Manufacturer,Remark,ListOutID,DOID,OrderID,StockOutCountActual,BatchID) " +
                        "values ('" + SID.ProductID + "','" + SID.ProName + "','" + SID.Spec + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "','" + SID.StockOutCountActual + "','" + SID.BatchID + "' )";
                    if (strInsertList != "")
                    {
                        SQLBase.ExecuteNonQuery(strInsertList);
                    }
                }
            }
            else return false;
            string UpProtot = "";

            string[] strDID = DID.Split(',');
            foreach (var i in strDID)
            {
                //出库完成修改库管状态为1；
                UpProtot = "update  BGOI_Sales.dbo.OrdersInfo set LibraryTubeState='1' where LibraryTubeState='0'  and UnitID='" + GAccount.GetAccountInfo().UnitID + "' and OrderID  in(select OrderID from  BGOI_Sales.dbo.Orders_DetailInfo where DID='" + i + "')";
                // UpProtot = "update  BGOI_Sales.dbo.Orders_DetailInfo set IState='1' where IState='0' and  DID='" + i + "'";
                if (UpProtot != "")
                {
                    int ss = SQLBase.ExecuteNonQuery(UpProtot);
                }
            }
            string strInsert = GSqlSentence.GetInsertInfoByD<StockOut>(record, "[BGOI_Inventory].[dbo].tk_StockOut");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
                string StockOutOrderid = "update  [BGOI_Inventory].[dbo].tk_StockOut set OrderID='" + OrderID + "' where ListOutID='" + record.ListOutID + "'";
                SQLBase.ExecuteNonQuery(StockOutOrderid);
                return true;
            }
            else return false;
        }
        public static UIDataTable HomeProductSalesList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a" +
                                    " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                                     " left join  BGOI_Inventory.dbo.InfooUT c on b.LibraryTubeState=c.id " +
                                     " left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                      " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                                      " left join  BGOI_Inventory.dbo.InfooUT c on b.LibraryTubeState=c.id " +
                                      "  left join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                      "  left join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID ";
            String strField = "    c.Text,b.OrderID,       a.Purchase,a.Remark,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }
                }
            }
            return instData;
        }

        //添加报警数据
        public static bool AddInAlarm(InAlarm inal)
        {
            int count = 0;
            string strInsert = GSqlSentence.GetInsertInfoByD(inal, " [BGOI_Inventory].[dbo].[InAlarm]");
            if (strInsert != "")
            {
                count = SQLBase.ExecuteNonQuery(strInsert);
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

        public static bool UpHomeProductSalesState(string ListOutID, string orderid)
        {
            string HouseID = "";
            int StockOutCount = 0;
            int resultCount = 0;
            try
            {
                string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "'";
                DataTable dts = SQLBase.FillTable(sqlstr);
                string sqlstrIn = "   select HouseID FROM [BGOI_Inventory].[dbo].[tk_StockOut] where ListOutID='" + ListOutID + "' ";
                DataTable dtIn = SQLBase.FillTable(sqlstrIn);
                if (dtIn != null && dtIn.Rows.Count > 0)
                {
                    HouseID = dtIn.Rows[0][0].ToString();
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dts.Rows)
                        {
                            for (int i = 0; i < dts.Columns.Count; i++)
                            {
                                string ProductID = dr[i].ToString();
                                //string strCount = "select SUM(StockOutCount) FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "' and ProductID='" + ProductID + "'";//申请数量
                                string strCount = "select SUM(StockOutCountActual) FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "' and ProductID='" + ProductID + "'";//实际数量
                                DataTable dtcount = SQLBase.FillTable(strCount);
                                if (dtcount != null && dtcount.Rows.Count > 0)
                                {
                                    StockOutCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
                                }
                                //修改库存表中的剩余量
                                DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0),ISNULL(OnlineCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
                                if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
                                {
                                    return false;
                                }
                                else
                                {
                                    int newOnline = Convert.ToInt32(dt.Rows[0][1]) + Convert.ToInt32(StockOutCount);
                                    int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(StockOutCount);
                                    resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "',OnlineCount='" + newOnline + "'  where ProductID='" + ProductID + "'and HouseID='" + HouseID + "'");
                                }
                            }
                        }
                    }
                    else return false;
                }
                string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockOut] set State='1' where ListOutID='" + ListOutID + "'";
                if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                {
                    //出库完成修改库管状态为2；
                    string UpProtot = "update  BGOI_Sales.dbo.OrdersInfo set LibraryTubeState='2' where LibraryTubeState='1'  and UnitID='" + GAccount.GetAccountInfo().UnitID + "' and OrderID  ='" + orderid + "'";
                    if (UpProtot != "")
                    {
                        SQLBase.ExecuteNonQuery(UpProtot);
                    }
                    return true;
                }
                else return false;
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        //提示报警
        public static DataTable GetOrderidNew()
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            if (unitid == "46" || unitid == "32")
            {
                sql = " select COUNT(*) as num  from [BGOI_Sales].dbo.Alarm a  " +
                        " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                        " where  b.LibraryTubeState=0 and b.AfterSaleState=0";
            }
            else
            {
                sql = " select COUNT(*) as num from [BGOI_Sales].dbo.Alarm a  " +
                      " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                      " where  b.LibraryTubeState=0 and b.AfterSaleState=0" +
                      " and b.UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #endregion
        #region [订单管理]
        #region [入库单管理]
        //入库单管理列表页
        public static UIDataTable StorageManagementtList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "";
            if (unitid == "46" || unitid == "32")
            {
                strSelCount = "select COUNT(*) " +
                                   " from BGOI_Inventory.dbo.tk_StockIn a " +
                                     " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                     " right join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  " +
                                     " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID  where " + where;
            }
            else
            {
                strSelCount = "select COUNT(*) " +
                                       " from BGOI_Inventory.dbo.tk_StockIn a " +
                                         " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                         " right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')and Validate='v') e on a.HouseID=e.HouseID  " +
                                         " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID  where " + where;
            }


            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strOrderBy = "a.StockInTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                                          "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  right join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                          "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "f.Text,a.BatchID AS BatchID,Amount,d.Text as SubjectName,e.HouseName as HouseName,a.ListInID as ListInID,Convert(varchar(12),a.StockInTime,111) as StockInTime,StockInUser,SubjectID,convert(varchar(20),State,120) State ";
            }
            else
            {
                strOrderBy = "a.StockInTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockIn a " +
                                          "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                          "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')and Validate='v') e on a.HouseID=e.HouseID " +
                                          "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "f.Text,a.BatchID AS BatchID,Amount,d.Text as SubjectName,e.HouseName as HouseName,a.ListInID as ListInID,Convert(varchar(12),a.StockInTime,111) as StockInTime,StockInUser,SubjectID,convert(varchar(20),State,120) State ";
            }
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未入库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已入库";
                    }

                }
            }

            return instData;
        }
        #endregion
        #region [出库单管理]
        public static UIDataTable StorageManagementOutList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "";
            if (unitid == "46" || unitid == "32")
            {
                strSelCount = "select COUNT(*) " +
                                  " from BGOI_Inventory.dbo.tk_StockOut a" +
                                    " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                    " right join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  " +
                                    " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID where  " + where;
            }
            else
            {
                strSelCount = "select COUNT(*) " +
                                  " from BGOI_Inventory.dbo.tk_StockOut a" +
                                    " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                    " right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "') and Validate='v') e on a.HouseID=e.HouseID  " +
                                    " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID where  " + where;
            }

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  right join  BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "f.Text,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            }
            else
            {
                strOrderBy = "a.CreateTime ";
                strTable = " BGOI_Inventory.dbo.tk_StockOut a " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "') and Validate='v') e on a.HouseID=e.HouseID " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "f.Text,a.ListOutID,d.Text as SubjectName,Convert(varchar(12),a.ProOutTime,111) as ProOutTime,a.ProOutUser,a.Amount,e.HouseName,convert(varchar(20),a.State,120) State ";
            }


            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未出库";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已出库";
                    }

                }
            }

            return instData;
        }
        //public static bool UpRefundDateState(string ListOutID)
        //{
        //    string HouseID = "";
        //    int StockOutCount = 0;
        //    int resultCount = 0;
        //    try
        //    {
        //        string sqlstr = "select distinct ProductID FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "'";
        //        DataTable dts = SQLBase.FillTable(sqlstr);

        //        string sqlstrIn = "   select HouseID FROM [BGOI_Inventory].[dbo].[tk_StockOut] where ListOutID='" + ListOutID + "' ";
        //        DataTable dtIn = SQLBase.FillTable(sqlstrIn);
        //        if (dtIn != null && dtIn.Rows.Count > 0)
        //        {
        //            HouseID = dtIn.Rows[0][0].ToString();


        //            if (dts != null && dts.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dts.Rows)
        //                {
        //                    for (int i = 0; i < dts.Columns.Count; i++)
        //                    {
        //                        string ProductID = dr[i].ToString();

        //                        string strCount = "select SUM(StockOutCount) FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "' and ProductID='" + ProductID + "'";
        //                        DataTable dtcount = SQLBase.FillTable(strCount);
        //                        if (dtcount != null && dtcount.Rows.Count > 0)
        //                        {
        //                            StockOutCount = Convert.ToInt32(dtcount.Rows[0][0].ToString());
        //                        }

        //                        //修改库存表中的剩余量
        //                        DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + ProductID + "' and HouseID='" + HouseID + "'");
        //                        if (dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
        //                        {
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            int newCount = Convert.ToInt32(dt.Rows[0][0]) + Convert.ToInt32(StockOutCount);
        //                            resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + ProductID + "'and HouseID='" + HouseID + "'");

        //                        }
        //                    }
        //                }
        //            }
        //            else return false;
        //        }
        //        string strSQL = "  update [BGOI_Inventory].[dbo].[tk_StockOut] set ValiDate='1' where ListOutID='" + ListOutID + "'";

        //        if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
        //        {
        //            string strCountnew = "select * FROM [BGOI_Inventory].[dbo].[tk_StockOutDetail] where ListOutID='" + ListOutID + "'";
        //             DataTable returnout = SQLBase.FillTable(strCountnew);
        //             foreach (DataRow dst in returnout.Rows)
        //            {
        //            string strInsertList = "Insert into [BGOI_Inventory].[dbo].tk_ReturnOutRecords (ListOutID, DOID, FactoryNum, ReturnType, Remark, CreateTime, Validate) " +
        //             "values ('" + ListOutID + "','" + dst["DOID"] + "','" + dst["FactoryNum"] + "','" + SID.Units + "'," + SID.StockOutCount + "," + SID.UnitPrice + "," + SID.TotalAmount + ",'" + SID.Manufacturer + "','" + SID.Remark + "','" + SID.ListOutID + "','" + ProStockOutDetialNum(SID.ListOutID) + "','" + SID.OrderID + "' )";

        //            }

        //            return true;
        //        }
        //        else return false;
        //    }
        //    catch
        //    {
        //        GLog.LogError("RM_CarRESInfo");
        //        return false;
        //    }
        //    finally
        //    {

        //    }
        //}
        #endregion
        #region [报废单管理]
        public static UIDataTable ScrapManagementList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string strSelCount = "";
            if (unitid == "46" || unitid == "32")
            {
                strSelCount = "select COUNT(*) " +
                                  " from BGOI_Inventory.dbo.tk_Scrap a" +
                                    " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                    " right join BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID  " +
                                    " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID where  " + where;
            }
            else
            {
                strSelCount = "select COUNT(*) " +
                                  " from BGOI_Inventory.dbo.tk_Scrap a" +
                                    " right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID" +
                                    " right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "') and Validate='v') e on a.HouseID=e.HouseID  " +
                                    " right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID where  " + where;
            }



            //string strSelCount = "select COUNT(*) " +
            //                       " from BGOI_Inventory.dbo.tk_Scrap  where" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "";
            String strTable = "";
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strOrderBy = "a.ScrapTime ";
                strTable = " BGOI_Inventory.dbo.tk_Scrap a " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  right join  BGOI_Inventory.dbo.tk_WareHouse e on a.HouseID=e.HouseID " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "  a.ReasonRemark, f.Text,ListScrapID,ScrapCount,AmountM,Convert(varchar(12),ScrapTime,111) as ScrapTime,PID,Handlers,e.HouseName,d.Text as SubjectName,convert(varchar(20),State,120) State";
            }
            else
            {
                strOrderBy = "a.ScrapTime ";
                strTable = " BGOI_Inventory.dbo.tk_Scrap a " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigSubject d on a.SubjectID=d.ID " +
                                         "  right join (select * from BGOI_Inventory.dbo.tk_WareHouse where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "') and Validate='v') e on a.HouseID=e.HouseID " +
                                         "  right join BGOI_Inventory.dbo.tk_ConfigProType f on e.TypeID=f.ID ";
                strField = "  a.ReasonRemark, f.Text,ListScrapID,ScrapCount,AmountM,Convert(varchar(12),ScrapTime,111) as ScrapTime,PID,Handlers,e.HouseName,d.Text as SubjectName,convert(varchar(20),State,120) State ";
            }
            //string strFilter = where;
            //string strOrderBy = "ScrapTime ";
            //String strTable = " BGOI_Inventory.dbo.tk_Scrap ";
            //String strField = "ListScrapID,ScrapCount,AmountM,Convert(varchar(12),ScrapTime,111) as ScrapTime,PID,Handlers,HouseID,SubjectID,convert(varchar(20),State,120) State ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未报废";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已报废";
                    }

                }
            }

            return instData;
        }
        public static string GetTopListScrapID()
        {
            string strID = "";
            string strD = "T" + DateTime.Now.ToString("yyyyMMdd");
            string strSqlID = "select max(ListScrapID) from BGOI_Inventory.dbo.tk_Scrap";
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
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(1, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "0" + (num + 1);

                        else
                            strD = strD + (num + 1);
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
        //制作报废单
        public static bool SaveScrapManagement(Scrap record, List<Scrap> delist, string Count, ref string strErr)
        {
            strErr = "";
            //int resultCount = 0;
            int count = 0;
            int oldcount = 0;
            int newcount = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                foreach (Scrap SID in delist)
                {
                    DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) as fnum  FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        newcount += dt.Rows.Count;
                        oldcount += Convert.ToInt32(dr["fnum"]);
                    }

                    if (newcount == 0 || oldcount == 0)//Convert.ToInt32(dt.Rows[0][0]) == 0)
                    {
                        strErr = "该物料在仓库中没有剩余量";
                        return false;
                    }
                    else
                    {
                        if (oldcount < Convert.ToInt32(Count))//Convert.ToInt32(dt.Rows[0][0])
                        {
                            strErr = "库存不足，请重新修改出库数量";
                            return false;
                        }
                    }
                    string strInsert = GSqlSentence.GetInsertInfoByD<Scrap>(record, "[BGOI_Inventory].[dbo].tk_Scrap");
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }

                    //if (Convert.ToInt32(SID.ScrapCount) < Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(dt.Rows[0][0]) != 0)
                    //{
                    //    string strInsert = GSqlSentence.GetInsertInfoByD<Scrap>(record, "[BGOI_Inventory].[dbo].tk_Scrap");
                    //    if (strInsert != "")
                    //    {
                    //        // strInsert = " set identity_insert [BGOI_Inventory].[dbo].tk_Scrap ON   " + strInsert;
                    //        count = SQLBase.ExecuteNonQuery(strInsert);
                    //    }
                    //    trans.Close(true);
                    //}
                    //else
                    //{
                    //    strErr = "库存不足，无法创建报废单";
                    //    return false;
                    //}
                }
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "添加失败";
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
        //报废完成
        public static bool UpScrapDateState(string ListScrapID, ref string strErr)
        {
            string strSQL = "  update [BGOI_Inventory].[dbo].[tk_Scrap] set State='1' where ListScrapID='" + ListScrapID + "'";
            try
            {
                if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                {
                    int resultCount = 0;
                    DataTable delist = SQLBase.FillTable("select * from [BGOI_Inventory].[dbo].[tk_Scrap] where ListScrapID='" + ListScrapID + "'");
                    foreach (DataRow SID in delist.Rows)
                    {
                        //修改库存表中的剩余量
                        DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID["PID"] + "'");

                        if (dt.Rows.Count != 0)
                        {
                            if (Convert.ToInt32(SID["ScrapCount"]) < Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(dt.Rows[0][0]) != 0)
                            {
                                int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID["ScrapCount"]);
                                resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID["PID"] + "'  and HouseID='" + SID["HouseID"] + "'");
                            }
                            else
                            {
                                strErr = "库存不足，无法创建报废单";
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        #endregion
        #region [销售发货单管理]
        public static UIDataTable SalesInvoiceManagementList(int a_intPageSize, int a_intPageIndex, string where, string whereone)
        {
            //where ContractID='HT-20150814-002'
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_Sales.dbo.Shipments a" +
                //" left join  (select * from BGOI_Sales.dbo.Shipments_DetailInfo  where ShipGoodsID in " +
                //" (select ShipGoodsID from BGOI_Sales.dbo.Shipments where OrderID in" +
                //" (select OrderID from  BGOI_Sales.dbo.OrdersInfo " + whereone + ")))" +
                //" b on a.ShipGoodsID=b.ShipGoodsID  
                                     " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime ";
            String strTable = " BGOI_Sales.dbo.Shipments a ";
            //" left join  (select * from BGOI_Sales.dbo.Shipments_DetailInfo  where ShipGoodsID in " +
            //" (select ShipGoodsID from BGOI_Sales.dbo.Shipments where OrderID in" +
            //" (select OrderID from  BGOI_Sales.dbo.OrdersInfo " + whereone + ")))" +
            //" b on a.ShipGoodsID=b.ShipGoodsID ";
            //"  left join BGOI_Sales.dbo.Shipments_DetailInfo b on a.ShipGoodsID=b.ShipGoodsID ";
            String strField = "a.OrderID as OrderID,Convert(varchar(12),a.CreateTime,111) as CreateTime,a.ShipGoodsID as ShipGoodsID,a.Shippers as Shippers ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }

            return instData;
        }
        //得到发货单编号和创建人
        public static string GetTopListSalesInvoiceID()
        {
            string strID = "";
            string strD = "F" + DateTime.Now.ToString("yyyyMMdd");
            string strSqlID = "select max(ShipGoodsID) from BGOI_Sales.dbo.Shipments";
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
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(1, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + "0" + (num + 1);

                        else
                            strD = strD + (num + 1);
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
        //制作销售发货单
        //public static bool SaveSalesInvoiceManagement(Shipments record, List<Shipments_DetailInfo> delist, ref string strErr)
        //{

        //    int count = 0;
        //    SQLTrans trans = new SQLTrans();
        //    trans.Open("MainInventory");
        //    try
        //    {
        //        foreach (Shipments_DetailInfo SID in delist)
        //        {
        //            DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID.PID + "' and HouseID='" + record.HouseID + "'");
        //            if (Convert.ToInt32(SID.Amount) < Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(dt.Rows[0][0]) != 0)
        //            {
        //                string strInsert = GSqlSentence.GetInsertInfoByD<Shipments>(record, "BGOI_Sales.dbo.Shipments");
        //                string strnewInsert = "Insert into BGOI_Sales.dbo.Shipments_DetailInfo(PID, ShipGoodsID, DID, OrderContent, Specifications, Supplier, Remark,Subtotal,HouseID, Amount) " +
        //                       "values ('" + SID.PID + "','" + SID.ShipGoodsID + "','" + SID.DID + "','" + SID.OrderContent + "','" + SID.Specifications + "','" + SID.Supplier + "','" + SID.Remark + "','" + SID.Subtotal + "','" + SID.HouseID + "','" + SID.Amount + "' )";
        //                string strsql = strInsert + ' ' + strnewInsert;
        //                if (strInsert != "")
        //                {
        //                    count = SQLBase.ExecuteNonQuery(strsql);
        //                }
        //                else
        //                {
        //                    strErr = "库存不足，请重新选择";
        //                    return false;
        //                }
        //                trans.Close(true);
        //            }
        //        }
        //        if (count > 0)
        //        {
        //            int PN = SQLBase.ExecuteNonQuery(" update [BGOI_Sales].[dbo].[Orders_DetailInfo] set State='1' where  ProductID='" + record.PID + "'");
        //            return true;
        //        }
        //        else
        //        {
        //            strErr = "添加失败";
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        strErr = ex.Message;
        //        trans.Close(true);
        //        return false;
        //    }
        //}
        //销售发货单完成
        public static bool UpSalesInvoDateState(string ShipGoodsID, ref string strErr)
        {
            string strSQL = "  update  BGOI_Sales.dbo.Shipments set State='1' where ShipGoodsID='" + ShipGoodsID + "'";
            try
            {
                if (SQLBase.ExecuteNonQuery(strSQL.ToString()) > 0)
                {

                    int resultCount = 0;
                    DataTable delist = SQLBase.FillTable("select * from BGOI_Sales.dbo.Shipments_DetailInfo where ShipGoodsID='" + ShipGoodsID + "'");
                    foreach (DataRow SID in delist.Rows)
                    {
                        //修改库存表中的剩余量
                        DataTable dt = SQLBase.FillTable("  select ISNULL(FinishCount,0) FROM [BGOI_Inventory].[dbo].[tk_StockRemain] where  ProductID='" + SID["PID"] + "'and HouseID='" + SID["HouseID"] + "'");

                        if (dt.Rows.Count != 0)
                        {
                            if (Convert.ToInt32(SID["Amount"]) < Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(dt.Rows[0][0]) != 0)
                            {
                                int newCount = Convert.ToInt32(dt.Rows[0][0]) - Convert.ToInt32(SID["Amount"]);
                                resultCount = SQLBase.ExecuteNonQuery("update [BGOI_Inventory].[dbo].[tk_StockRemain] set FinishCount='" + newCount + "' where ProductID='" + SID["PID"] + "'  and HouseID='" + SID["HouseID"] + "'");
                            }
                            else
                            {
                                strErr = "库存不足，无法创建销售发货单";
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        //订单
        public static UIDataTable OrderInfoInvoSalesList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.State='0' and a.OrderID=b.OrderID  ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.State='0' and a.OrderID=b.OrderID ";
            string strOrderBy = " a.OrderID ";
            String strTable = " [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b ";
            String strField = " a.DID,a.OrderID,a.ProductID,OrderContent,SpecsModels,Manufacturer,a.OrderUnit,OrderNum,Price,Subtotal,DeliveryTime,b.SalesType  ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        public static DataTable GetOrderSalesInvoDetail(string PID)
        {
            string sql = "select OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Price,Subtotal,DeliveryTime,Remark from [BGOI_Sales].[dbo].[Orders_DetailInfo]  where State='0' and ProductID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }

        public static UIDataTable SalesInvoiceManagementDetialList(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_Sales.dbo.Shipments_DetailInfo where ShipGoodsID='" + ShipGoodsID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " ShipGoodsID='" + ShipGoodsID + "' ";
            string strOrderBy = " ShipGoodsID ";
            String strTable = " BGOI_Sales.dbo.Shipments_DetailInfo  ";
            String strField = " ShipGoodsID, DID, ProductID, OrderContent, Specifications, Supplier, Unit, Remark, CreateTime, CreateUser, Validate, Price, Amount ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }

            return instData;
        }
        #endregion
        #endregion
        #region [统计总汇]
        #region [物料总汇]
        public static DataTable MaterialSummaryTableList(string HouseID, string start, string end, string where)
        {
            string sql = "";
            if (where != "")
            {
                sql = " select b.Text,a.PID,a.ProName,a.Spec,f.HouseNAME,c.HouseID,a.Units,a.UnitPrice,c.FinishCount,(a.UnitPrice*c.FinishCount) as total,InCount,OutCount,(c.FinishCount+OutCount-InCount) as totalCount,(c.FinishCount+OutCount-InCount)*a.UnitPrice as totalPrice,(c.FinishCount-c.FinishCount)as Cynum,(c.FinishCount-c.FinishCount)*a.UnitPrice as CynumPrice " +
               " from (select * from BGOI_Inventory.dbo.tk_ProductInfo where " + where + ")a  " +
               " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID " +
               " left join (select ProductID,HouseID,FinishCount from BGOI_Inventory.dbo.tk_StockRemain where HouseID='" + HouseID + "') c on a.PID=c.ProductID " +
               " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where HouseID='" + HouseID + "') f on c.HouseID=f.HouseID " +
               " left join (select ProductID,SUM(StockOutCountActual) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and HouseID='" + HouseID + "' and ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID) d on d.ProductID=a.PID " +
               " left join (select ValiDate,HouseID,ProOutTime from BGOI_Inventory.dbo.tk_StockOut where (CONVERT(varchar(100), ProOutTime, 23)='" + end + "' or CONVERT(varchar(100), ProOutTime, 23)='" + start + "' )and HouseID='" + HouseID + "' and ValiDate='v') g on c.HouseID=g.HouseID" +
               " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where HouseID='" + HouseID + "' and (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and  HouseID='" + HouseID + "' and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID ) e on e.ProductID=a.PID " +
               " left join (select ValiDate,HouseID,StockInTime from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23)='" + end + "' or CONVERT(varchar(100), StockInTime, 23)='" + start + "' )and HouseID='" + HouseID + "' and ValiDate='v') h on h.HouseID=c.HouseID ";
            }
            else
            {
                sql = " select b.Text,a.PID,a.ProName,a.Spec,f.HouseNAME,c.HouseID,a.Units,a.UnitPrice,c.FinishCount,(a.UnitPrice*c.FinishCount) as total,InCount,OutCount,(c.FinishCount+OutCount-InCount) as totalCount,(c.FinishCount+OutCount-InCount)*a.UnitPrice as totalPrice,(c.FinishCount-c.FinishCount)as Cynum,(c.FinishCount-c.FinishCount)*a.UnitPrice as CynumPrice " +
                   "  from (select * from BGOI_Inventory.dbo.tk_ProductInfo )a  " +
                   " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID " +
                   " left join (select ProductID,HouseID,FinishCount from BGOI_Inventory.dbo.tk_StockRemain where HouseID='" + HouseID + "') c on a.PID=c.ProductID " +
                   " left join (select * from BGOI_Inventory.dbo.tk_WareHouse where HouseID='" + HouseID + "') f on c.HouseID=f.HouseID " +
                   " left join (select ProductID,SUM(StockOutCountActual) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and HouseID='" + HouseID + "' and ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID) d on d.ProductID=a.PID " +
                   " left join (select ValiDate,HouseID,ProOutTime from BGOI_Inventory.dbo.tk_StockOut where (CONVERT(varchar(100), ProOutTime, 23)='" + end + "' or CONVERT(varchar(100), ProOutTime, 23)='" + start + "' )and HouseID='" + HouseID + "' and ValiDate='v') g on c.HouseID=g.HouseID" +
                   " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where HouseID='" + HouseID + "' and (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and  HouseID='" + HouseID + "' and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID ) e on e.ProductID=a.PID " +
                   " left join (select ValiDate,HouseID,StockInTime from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23)='" + end + "' or CONVERT(varchar(100), StockInTime, 23)='" + start + "' )and HouseID='" + HouseID + "' and ValiDate='v') h on h.HouseID=c.HouseID ";
            }

            sql += " where OutCount>0 or InCount>0 or c.FinishCount>0 ";
            sql += " group by a.ProName,a.Spec,a.Units,c.HouseID,a.PID,a.UnitPrice,c.FinishCount,f.HouseNAME,d.OutCount,e.InCount,b.Text order by PID desc";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #region [库存汇总表]
        public static DataTable InventorySummaryTableList(string start, string end, string where)
        {
            string sql = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                sql = " select b.Text,c.ProductID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,d.OutCount,totalcountnum,(e.InCount*a.UnitPrice) as inPrice,(d.OutCount*a.UnitPrice) as OutPrice,(totalcountnum-d.OutCount) as pronum,(totalcountnum-d.OutCount)*UnitPrice as pronumtatalprice from BGOI_Inventory.dbo.tk_ProductInfo a " +
                     " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                     " left join (select ProductID,sum(FinishCount) as totalcountnum from  BGOI_Inventory.dbo.tk_StockRemain  group by ProductID)c on a.PID=c.ProductID " +
                     " left join (select ProductID,SUM(StockOutCount) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + " group by ProductID) d on c.ProductID=d.ProductID and d.ProductID=a.PID " +
                     " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + "  group by ProductID ) e on e.ProductID=c.ProductID and e.ProductID=a.PID ";
                sql += " where d.OutCount>0 or e.InCount>0 or c.totalcountnum>0 " + where;
                sql += " group by a.ProName,a.Spec,a.Units,c.ProductID,a.UnitPrice,d.OutCount,e.InCount,b.Text,totalcountnum";
            }
            else
            {
                //if (where != "")
                //{
                sql = " select b.Text,c.ProductID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,d.OutCount,totalcountnum,(e.InCount*a.UnitPrice) as inPrice,(d.OutCount*a.UnitPrice) as OutPrice,(totalcountnum-d.OutCount) as pronum,(totalcountnum-d.OutCount)*UnitPrice as pronumtatalprice from BGOI_Inventory.dbo.tk_ProductInfo a " +
                     " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                     " left join (select ProductID,sum(FinishCount) as totalcountnum from  BGOI_Inventory.dbo.tk_StockRemain where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "') group by ProductID)c on a.PID=c.ProductID " +
                     " left join (select ProductID,SUM(StockOutCount) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + " group by ProductID) d on c.ProductID=d.ProductID and d.ProductID=a.PID " +
                     " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + "  group by ProductID ) e on e.ProductID=c.ProductID and e.ProductID=a.PID ";
                //}
                //else
                //{
                //    sql = " select b.Text,c.ProductID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,d.OutCount,totalcountnum,(e.InCount*a.UnitPrice) as inPrice,(d.OutCount*a.UnitPrice) as OutPrice,(totalcountnum-d.OutCount) as pronum,(totalcountnum-d.OutCount)*UnitPrice as pronumtatalprice from BGOI_Inventory.dbo.tk_ProductInfo a " +
                //          " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                //          " left join (select ProductID,sum(FinishCount) as totalcountnum from  BGOI_Inventory.dbo.tk_StockRemain where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "') group by ProductID)c on a.PID=c.ProductID " +
                //          " left join (select ProductID,SUM(StockOutCount) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo ) group by ProductID) d on c.ProductID=d.ProductID and d.ProductID=a.PID " +
                //          " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo ) group by ProductID ) e on e.ProductID=c.ProductID and e.ProductID=a.PID ";
                //}
                sql += " where d.OutCount>0 or e.InCount>0 or c.totalcountnum>0 " + where;
                sql += " group by a.ProName,a.Spec,a.Units,c.ProductID,a.UnitPrice,d.OutCount,e.InCount,b.Text,totalcountnum";
            }

            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable AdditionalList(string start, string end, string where)
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            if (unitid == "46" || unitid == "32")
            {
                sql = " select '合计',SUM(InCount) as totalInCount,SUM(OutCount) as totalOutCount,SUM(totalcountnum) as totalnum," +
                "SUM(inPrice) as totalPrice,SUM(OutPrice) as totalOutPrice,SUM(pronum) as totalProNum,SUM(UnitPrice) as totalUnitPrice," +
                "SUM(pronumtatalprice) as totalProNumTotalPrice " +
                "from (select top 10000 b.Text,c.ProductID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,d.OutCount,totalcountnum,(e.InCount*a.UnitPrice) as inPrice,(d.OutCount*a.UnitPrice) as OutPrice,(totalcountnum-d.OutCount) as pronum,(totalcountnum-d.OutCount)*UnitPrice as pronumtatalprice from BGOI_Inventory.dbo.tk_ProductInfo a " +
                " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                " left join (select ProductID,sum(FinishCount) as totalcountnum from  BGOI_Inventory.dbo.tk_StockRemain group by ProductID)c on a.PID=c.ProductID " +
                " left join (select ProductID,SUM(StockOutCount) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + "  group by ProductID) d on c.ProductID=d.ProductID and d.ProductID=a.PID" +
                " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo)  " + where + " group by ProductID ) e on e.ProductID=c.ProductID and e.ProductID=a.PID ";
                sql += " where d.OutCount>0 or e.InCount>0 or c.totalcountnum>0 " + where;
                sql += " group by a.ProName,a.Spec,a.Units,c.ProductID,a.UnitPrice,d.OutCount,e.InCount,b.Text,totalcountnum) A";
            }
            else
            {
                sql = " select '合计',SUM(InCount) as totalInCount,SUM(OutCount) as totalOutCount,SUM(totalcountnum) as totalnum," +
                    "SUM(inPrice) as totalPrice,SUM(OutPrice) as totalOutPrice,SUM(pronum) as totalProNum,SUM(UnitPrice) as totalUnitPrice," +
                    "SUM(pronumtatalprice) as totalProNumTotalPrice " +
                    "from (select top 10000 b.Text,c.ProductID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,d.OutCount,totalcountnum,(e.InCount*a.UnitPrice) as inPrice,(d.OutCount*a.UnitPrice) as OutPrice,(totalcountnum-d.OutCount) as pronum,(totalcountnum-d.OutCount)*UnitPrice as pronumtatalprice from BGOI_Inventory.dbo.tk_ProductInfo a " +
                    " left join  BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                    " left join (select ProductID,sum(FinishCount) as totalcountnum from  BGOI_Inventory.dbo.tk_StockRemain where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "') group by ProductID)c on a.PID=c.ProductID " +
                    " left join (select ProductID,SUM(StockOutCount) as OutCount from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut  where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + "  group by ProductID) d on c.ProductID=d.ProductID and d.ProductID=a.PID" +
                    " left join (select ProductID,SUM(StockInCount) as InCount from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v' ) and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo)  " + where + " group by ProductID ) e on e.ProductID=c.ProductID and e.ProductID=a.PID ";
                sql += " where d.OutCount>0 or e.InCount>0 or c.totalcountnum>0 " + where;
                sql += " group by a.ProName,a.Spec,a.Units,c.ProductID,a.UnitPrice,d.OutCount,e.InCount,b.Text,totalcountnum) A";
            }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable KuCunZongHui()
        {
            String strField = "select sum(b.FinishCount*a.UnitPrice) as zongjie from  BGOI_Inventory.dbo.tk_ProductInfo a " +
                                " left join(select FinishCount,ProductID from BGOI_Inventory.dbo.tk_StockRemain " +
                                " where  HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where TypeID=1 and UnitID='" + GAccount.GetAccountInfo().UnitID + "'))b " +
                                " on a.PID=b.ProductID " +
                                " left join(select ProductID from BGOI_Inventory.dbo.tk_StockRemain " +
                                " where  HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where TypeID=1 and UnitID='" + GAccount.GetAccountInfo().UnitID + "'))c " +
                                " on a.PID=c.ProductID";
            DataTable dt = SQLBase.FillTable(strField, "MainInventory");
            return dt;
        }

        public static DataTable GetJinE()
        {
            String strField = "select " +
                                " sum(a.Price2*b.StockInCount) as hsje,sum(b.UnitPrice*b.StockInCount) as zcbje " +
                                " from BGOI_Inventory.dbo.tk_ProductInfo a " +
                                " left join (select ProductID,StockInCount,UnitPrice from BGOI_Inventory.dbo.tk_StockInDetail " +
                                " group by ProductID,StockInCount,UnitPrice ) b on a.PID=b.ProductID";
            DataTable dt = SQLBase.FillTable(strField, "MainInventory");
            return dt;
        }
        #endregion
        #region [物料出入库明细表]
        public static DataTable MaterialOutOfTheWarehouseList(string start, string end, string where)
        {
            string sql = " select b.Text,a.PID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,(e.InCount*a.UnitPrice) as inPrice,e.RKzts,d.OutCount,(d.OutCount*a.UnitPrice) as OutPrice,d.CKzts from BGOI_Inventory.dbo.tk_ProductInfo a " +
                  " left join BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID " +
                  " left join (select ProductID,SUM(StockOutCount) as OutCount ,COUNT(*) as CKzts from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID  in (select ListOutID from BGOI_Inventory.dbo.tk_StockOut where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + " " +
                  " group by ProductID ) d on d.ProductID=a.PID  " +
                  " left join ( select ProductID,SUM(StockInCount)as InCount,COUNT(*) as RKzts from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo ) " + where + " " +
                  " group by ProductID) e on e.ProductID=a.PID ";
            sql += " where d.OutCount>0 or e.InCount>0  ";
            sql += " group by a.ProName,a.Spec,a.Units,a.PID,a.UnitPrice,d.OutCount,e.InCount,b.Text,d.OutCount,e.RKzts,d.CKzts";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable AddWarehouseList(string start, string end, string where)
        {
            string sql = " select '合计',SUM(InCount) as totalInCount,SUM(RKzts) as totalRKzts,SUM(inPrice) as totalPrice,SUM(OutCount) as totalOutCount,SUM(CKzts) as totalRKzts,SUM(OutPrice) as totalOutPrice,SUM(UnitPrice) as totalUnitPrice " +
                  " from (select top 10000 b.Text,a.PID,a.ProName,a.Spec,a.Units,a.UnitPrice,e.InCount,(e.InCount*a.UnitPrice) as inPrice,e.RKzts,d.OutCount,(d.OutCount*a.UnitPrice) as OutPrice,d.CKzts from BGOI_Inventory.dbo.tk_ProductInfo a " +
                  " left join BGOI_Inventory.dbo.tk_ConfigPType b on a.Ptype=b.ID  " +
                  " left join (select ProductID,SUM(StockOutCount) as OutCount ,COUNT(*) as CKzts from BGOI_Inventory.dbo.tk_StockOutDetail where ListOutID  in (select ListOutID  from BGOI_Inventory.dbo.tk_StockOut where (CONVERT(varchar(100), ProOutTime, 23) BETWEEN '" + start + "' and '" + end + "') and  ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo) " + where + " " +
                  " group by ProductID ) d on d.ProductID=a.PID " +
                  " left join (select ProductID,SUM(StockInCount)as InCount,COUNT(*) as RKzts from BGOI_Inventory.dbo.tk_StockInDetail where ListInID in (select ListInID from BGOI_Inventory.dbo.tk_StockIn where (CONVERT(varchar(100), StockInTime, 23) BETWEEN '" + start + "' and '" + end + "')and ValiDate='v') and ProductID in (select PID from BGOI_Inventory.dbo.tk_ProductInfo ) " + where + "  " +
                  " group by ProductID) e on e.ProductID=a.PID" + where;
            sql += " where d.OutCount>0 or e.InCount>0  ";
            sql += " group by a.ProName,a.Spec,a.Units,a.PID,a.UnitPrice,d.OutCount,e.InCount,b.Text,d.OutCount,RKzts,CKzts ) A";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #region [入库汇总]
        public static DataTable InventoryStatisticsList()
        {
            string sql = "";
            string unitid = GAccount.GetAccountInfo().UnitID;
            if (unitid == "46" || unitid == "32")
            {
                sql = " select b.ProductID,a.ProName,a.Spec,c.HouseName,sum(g.StockInCount) as StockInCount,a.Units,b.FinishCount as FinCount,b.OnlineCount,a.Spec,a.UnitPrice  from BGOI_Inventory.dbo.tk_ProductInfo a " +//,d.Number,(b.FinishCount/d.Number) as CPnum
                       " left join (select ProductID,HouseID,OnlineCount,FinishCount from BGOI_Inventory.dbo.tk_StockRemain where " +
                       " HouseID in(select HouseID from  BGOI_Inventory.dbo.tk_WareHouse) and ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID,HouseID,OnlineCount,FinishCount) b on a.PID=b.ProductID  " +
                    //" left join (select FinishCount,ProductID                     from BGOI_Inventory.dbo.tk_StockRemain where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo'))e on a.PID=e.ProductID " +
                       " left join  (select HouseID,HouseName from  BGOI_Inventory.dbo.tk_WareHouse)  c on c.HouseID=b.HouseID " +
                    // " left join  BGOI_Inventory.dbo.tk_ProFinishDefine d on b.ProductID=d.ProductID " +
                       " left join (select Number,PartPID,ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine where PartPID in((select PID from BGOI_Inventory.dbo.tk_ProductInfo))) f on b.ProductID=f.PartPID " +
                       " left join  BGOI_Inventory.dbo.tk_StockInDetail g on b.ProductID=g.ProductID ";
                sql += " where b.OnlineCount>0 or b.FinishCount>0 or f.Number>0 or g.StockInCount>0 ";// or d.Number>0 
                sql += " group by a.ProName,a.Spec,a.Units,a.PID,a.UnitPrice,c.HouseName,f.Number,b.OnlineCount,b.FinishCount,b.ProductID order by b.ProductID desc";//f.Number,
            }
            else
            {
                sql = " select b.ProductID,a.ProName,a.Spec,c.HouseName,sum(g.StockInCount) as StockInCount,a.Units,b.FinishCount as FinCount,b.OnlineCount,a.Spec,a.UnitPrice  from BGOI_Inventory.dbo.tk_ProductInfo a " +//,d.Number,(b.FinishCount/d.Number) as CPnum
                     " left join (select ProductID,HouseID,OnlineCount,FinishCount from BGOI_Inventory.dbo.tk_StockRemain where " +
                     " HouseID in(select HouseID from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "') and ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo) group by ProductID,HouseID,OnlineCount,FinishCount) b on a.PID=b.ProductID  " +
                    //" left join (select FinishCount,ProductID                     from BGOI_Inventory.dbo.tk_StockRemain where ProductID in(select PID from BGOI_Inventory.dbo.tk_ProductInfo'))e on a.PID=e.ProductID " +
                     " left join  (select HouseID,HouseName from  BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "')  c on c.HouseID=b.HouseID " +
                    // " left join  BGOI_Inventory.dbo.tk_ProFinishDefine d on b.ProductID=d.ProductID " +
                     " left join (select Number,PartPID,ProductID from BGOI_Inventory.dbo.tk_ProFinishDefine where PartPID in((select PID from BGOI_Inventory.dbo.tk_ProductInfo))) f on b.ProductID=f.PartPID " +
                     " left join  BGOI_Inventory.dbo.tk_StockInDetail g on b.ProductID=g.ProductID ";
                sql += " where b.OnlineCount>0 or b.FinishCount>0 or f.Number>0 or g.StockInCount>0 ";// or d.Number>0 
                sql += " group by a.ProName,a.Spec,a.Units,a.PID,a.UnitPrice,c.HouseName,f.Number,b.OnlineCount,b.FinishCount,b.ProductID order by b.ProductID desc";//f.Number,
            }

            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
        #endregion
        #region [库存报警]
        //最低库存报警--物料信息列表
        public static UIDataTable MaterialBasicData(int a_intPageSize, int a_intPageIndex, string where)
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;
            UIDataTable instData = new UIDataTable();
            string strSelCount = "";
            if (UnitID == "46" || UnitID == "32")
            {
                strSelCount = " select COUNT(*) from  BGOI_Inventory.dbo.tk_StockRemain a, " +
                            " BGOI_Inventory.dbo.tk_ProductInfo b, " +
                            " BGOI_Inventory.dbo.tk_WareHouse c," +
                            " BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d,  " +
                            " BGOI_Inventory.dbo.tk_ConfigProType f " +
                            " where a.ProductID=b.PID and a.HouseID=c.HouseID " +
                            " and d.PID=b.PID  and b.ProTypeID=f.OID " +
                            " and a.FinishCount<d.Num and  a.ProductID=d.PID  " + where;

            }
            else
            {
                strSelCount = " select COUNT(*) from  BGOI_Inventory.dbo.tk_StockRemain a, " +
                                " BGOI_Inventory.dbo.tk_ProductInfo b, " +
                                " BGOI_Inventory.dbo.tk_WareHouse c," +
                                " BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d,  " +
                    //" BGOI_Inventory.dbo.tk_HouseEarlyWarningNum e, " +
                                " BGOI_Inventory.dbo.tk_ConfigProType f " +
                                " where a.ProductID=b.PID and a.HouseID=c.HouseID " +
                                " and d.PID=b.PID  and b.ProTypeID=f.OID " +
                                " and a.FinishCount<d.Num and a.ProductID=d.PID and " +
                                " c.UnitID in (select distinct DeptId from BJOI_UM.dbo.UM_UnitNew " +
                                " where DeptId='" + UnitID + "' or SuperId='" + UnitID + "') " + where;
                // + " group by b.PID,b.Detail,c.HouseName,a.FinishCount,e.Num,b.Spec,f.Text  ";
            }
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //BGOI_Inventory.dbo.tk_HouseEarlyWarningNum e,

            string strFilter = "";
            string strOrderBy = "";
            String strTable = "";
            String strField = "";

            if (UnitID == "46" || UnitID == "32")
            {
                strFilter = " a.ProductID=b.PID and a.HouseID=c.HouseID " +
                                " and d.PID=b.PID  and b.ProTypeID=f.OID " +
                                " and a.FinishCount<d.Num and a.ProductID=d.PID " + where +
                                " group by b.PID,b.ProName,c.HouseName,a.FinishCount,d.Num,b.Spec,f.Text  ";
            }
            else
            {
                strFilter = " a.ProductID=b.PID and a.HouseID=c.HouseID " +
                                    " and d.PID=b.PID  and b.ProTypeID=f.OID " +
                                    " and a.FinishCount<d.Num and a.ProductID=d.PID  and " +
                                    " c.UnitID in (select distinct DeptId from BJOI_UM.dbo.UM_UnitNew " +
                                    " where DeptId='" + UnitID + "' or SuperId='" + UnitID + "') " + where +
                                    " group by b.PID,b.ProName,c.HouseName,a.FinishCount,d.Num,b.Spec,f.Text,d.Remarks  ";
            }

            strOrderBy = " b.PID ";
            strTable = " BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b, BGOI_Inventory.dbo.tk_WareHouse c, BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d,BGOI_Inventory.dbo.tk_ConfigProType f  ";
            strField = " b.PID,b.ProName,c.HouseName,sum(FinishCount) as Count,d.Num,b.Spec,f.Text,d.Remarks ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        //最低库存报警--上限数量
        public static DataTable MaterialBasicNum()
        {
            string UnitID = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            if (UnitID == "46" || UnitID == "32")
            {
                sql = " Select ROW_NUMBER() OVER (ORDER BY  b.PID ) AS RowNumber, " +
                      " b.PID,b.Detail,c.HouseName,sum(FinishCount) as Count " +
                      " From  BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b, " +
                      " BGOI_Inventory.dbo.tk_WareHouse c, BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d " +
                      " Where a.ProductID=b.PID and a.HouseID=c.HouseID and a.FinishCount<d.Num " +
                      " and a.ProductID=d.PID group by b.PID,b.Detail,c.HouseName,a.FinishCount ";
            }
            else
            {
                sql = " Select ROW_NUMBER() OVER (ORDER BY  b.PID ) AS RowNumber, " +
                      "b.PID,b.Detail,c.HouseName,sum(FinishCount) as Count " +
                      "From  BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b, " +
                      "BGOI_Inventory.dbo.tk_WareHouse c, BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d " +
                      "Where  a.ProductID=b.PID and a.HouseID=c.HouseID and a.FinishCount<d.Num " +
                      "and a.ProductID=d.PID  and  c.UnitID in (select distinct DeptId from BJOI_UM.dbo.UM_UnitNew " +
                      "where DeptId='" + UnitID + "' or SuperId='" + UnitID + "') group by b.PID,b.Detail,c.HouseName,a.FinishCount ";
            }

            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static string getAlarm(string strFirsTypeText, string strCount)
        {
            string str = "";
            string str1 = "";
            string strSql = " select Low,Remark from tk_StockAlarm where FirstType='" + strFirsTypeText + "'";
            DataTable dt = SQLBase.FillTable(strSql);
            if (dt == null)
            {
                str = "";
                str1 = "";
            }
            else if (dt.Rows.Count == 0)
            {
                str = "";
                str1 = "";
            }
            else
            {
                str = dt.Rows[0][0].ToString();
                str1 = dt.Rows[0][1].ToString();
            }

            return str + "," + str1;
        }
        // 获取报警库存列表
        public static DataTable getWarnLow(ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            string UnitID = acc.UnitID;
            string strSql = "";
            if (UnitID == "46" || UnitID == "32")
            {
                strSql = " select b.PID,b.Detail,c.HouseName,sum(FinishCount) as Count  ";
                strSql += " from BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b, BGOI_Inventory.dbo.tk_WareHouse c,BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d  ";
                strSql += " where a.ProductID=b.PID and a.HouseID=c.HouseID and a.FinishCount<d.Num and a.ProductID=d.PID " +
                          " group by b.PID,b.Detail,c.HouseName,a.FinishCount ";
                strSql += " order by c.HouseName ";
            }
            else
            {
                strSql = " select b.PID,b.Detail,c.HouseName,sum(FinishCount) as Count  ";
                strSql += " from BGOI_Inventory.dbo.tk_StockRemain a,BGOI_Inventory.dbo.tk_ProductInfo b, BGOI_Inventory.dbo.tk_WareHouse c,BGOI_Inventory.dbo.tk_HouseEarlyWarningNum d  ";
                strSql += " where a.ProductID=b.PID and a.HouseID=c.HouseID and a.FinishCount<d.Num and a.ProductID=d.PID and " +
                                    " c.UnitID in (select distinct DeptId from BJOI_UM.dbo.UM_UnitNew " +
                                    " where DeptId='" + UnitID + "' or SuperId='" + UnitID + "')" +
                                    " group by b.PID,b.Detail,c.HouseName,a.FinishCount ";
                strSql += " order by c.HouseName ";
            }

            DataTable dtWarn = SQLBase.FillTable(strSql);

            return dtWarn;

        }
        //查询物料编号
        public static DataTable GetNum()
        {
            string str = " select PID,ProName from BGOI_Inventory.dbo.tk_ProductInfo";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //根据pid得到报警数量上限
        public static DataTable UpLowAlarm(string PID)
        {
            string sql = " select * from BGOI_Inventory.dbo.tk_HouseEarlyWarningNum where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        //根据pid得到产品信息
        public static DataTable GetPidXiang(string PID)
        {
            string sql = "  select * from BGOI_Inventory.dbo.tk_ProductInfo  where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        //保存设置的报警数量上限
        public static bool SaveLowAlarm(tk_HouseEarlyWarningNum record, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = "";
                string sql = "  select * from [BGOI_Inventory].[dbo].tk_HouseEarlyWarningNum where PID='" + record.PID + "'";
                DataTable dt = SQLBase.FillTable(sql, "MainInventory");
                if (dt.Rows.Count > 0)
                {
                    strInsert = " update [BGOI_Inventory].[dbo].tk_HouseEarlyWarningNum set Num='" + record.Num + "' where  PID='" + record.PID + "' ";
                }
                else
                {
                    strInsert = GSqlSentence.GetInsertInfoByD<tk_HouseEarlyWarningNum>(record, "[BGOI_Inventory].[dbo].tk_HouseEarlyWarningNum");
                }
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                    strErr = "保存失败！";
                return false;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        //
        public static bool LowAlarmZSC(string PID, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = " update [BGOI_Inventory].[dbo].tk_HouseEarlyWarningNum set Remarks='在生产' where  PID='" + PID + "' ";
                count = SQLBase.ExecuteNonQuery(strInsert);
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                    strErr = "记录失败！";
                return false;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        //
        public static bool LowAlarmZT(string PID, ref string strErr)
        {
            //int resultCount = 0;
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = " update [BGOI_Inventory].[dbo].tk_HouseEarlyWarningNum set Remarks='在途中' where  PID='" + PID + "' ";
                count = SQLBase.ExecuteNonQuery(strInsert);
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                    strErr = "记录失败！";
                return false;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        #endregion
        #region [配置信息]
        public static UIDataTable ConfigurationInformationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_ProductSelect_Config  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = where;
            string strOrderBy = " ID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductSelect_Config ";
            String strField = " ID,Text,Type ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        //添加
        public static int InsertNewContentnew(string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            string ID = "";
            string Stype = "";
            string strInsertOrder = "";
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainInventory");
            if (type == "1")
            {
                Stype = "SpecsModels";
                ID = "Spec0" + PreGetTaskNonew(Stype, type);
            }
            if (type == "2")
            {
                Stype = "OutUses";
                ID = "U0000" + PreGetTaskNonew(Stype, type);
            }
            string strSql = "select ID,Type from BGOI_Inventory.dbo.tk_ProductSelect_Config where Validate = 'v' and Text='" + text + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
            int a = dt.Rows.Count;
            if (dt.Rows.Count < 1)
            {
                strInsertOrder = "insert into BGOI_Inventory.dbo.tk_ProductSelect_Config (ID, Text, Type,Validate) values ('" + ID + "','" + text + "','" + Stype + "','v')";
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
            }
            return intInsert;
        }
        public static string PreGetTaskNonew(string sel, string type)
        {
            string strID = "";
            string xid = "";
            // string strSqlID = "select max(ID) from BGOI_Inventory.dbo.tk_ProductSelect_Config where Type='" + sel + "'";
            string strSqlID = " select * from (select MAX(ID) IDs from BGOI_Inventory.dbo.tk_ProductSelect_Config where Type='" + sel + "' ) m where m.IDs is not null and m.IDs!=''";
            DataTable dtID = SQLBase.FillTable(strSqlID, "MainInventory");
            if (dtID != null && dtID.Rows.Count > 0)//
            {
                strID = dtID.Rows[0][0].ToString();
                int num = 0;
                if (type == "1")
                {
                    num = Convert.ToInt32(strID.Substring(5));
                }
                if (type == "2")
                {
                    num = Convert.ToInt32(strID.Substring(5));
                }
                num = num + 1;
                xid = num.ToString();
            }
            else
            {
                xid = "0";
            }
            return xid;
        }
        //删除
        public static int DeleteContentnew(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainInventory");
            string strInsertOrder = "update BGOI_Inventory.dbo.tk_ProductSelect_Config set Validate = 'i' where ID = '" + xid + "' and Type = '" + type + "'";
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
        //修改
        public static int UpdateContentnew(string xid, string type, string text, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainInventory");

            string strInsertOrder = "update BGOI_Inventory.dbo.tk_ProductSelect_Config set Text = '" + text + "' where ID = '" + xid + "' and Type = '" + type + "'";
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
        #region 【发展】
        public static DataTable GetHouseFZ()
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string str = "";
            if (unitid == "46" || unitid == "32")
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where TypeID='1'";
            }
            else
            {
                str = "select HouseID,HouseName From BGOI_Inventory.dbo.tk_WareHouse where TypeID='1' and  UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where SuperId='" + GAccount.GetAccountInfo().UnitID + "' or DeptId='" + GAccount.GetAccountInfo().UnitID + "')";
            }
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        //查询物料编号
        public static DataTable GetLJNum()
        {
            string str = " select * from BGOI_Inventory.dbo.tk_ProductInfo where Ptype='PT05' and ProTypeID='1' ";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        #region 【基本入库】
        //导出
        public static DataTable GetBasicStockInFZToExcel(string strWhere, string ListInID, ref string strErr)
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strField = "select ListInID, BatchID,c.Text, StockInTime, StockInUser, Amount,b.HouseName,Remark," +
                              " (case when State=0 then '未入库' else '已入库' end) as State " +
                              " from BGOI_Inventory.dbo.tk_StockIn a,BGOI_Inventory.dbo.tk_WareHouse b, " +
                              " BGOI_Inventory.dbo.tk_ConfigSubject c  where " +
                              " a.ListInID='" + ListInID + "' and a.HouseID in ( " +
                              " select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and b.HouseID=a.HouseID and a.SubjectID=c.ID";
            }
            else
            {
                strField = "select ListInID, BatchID,c.Text, StockInTime, StockInUser, Amount,b.HouseName,Remark," +
                                   " (case when State=0 then '未入库' else '已入库' end) as State " +
                                   " from BGOI_Inventory.dbo.tk_StockIn a,BGOI_Inventory.dbo.tk_WareHouse b, " +
                                   " BGOI_Inventory.dbo.tk_ConfigSubject c  where " +
                                   " a.ListInID='" + ListInID + "' and a.HouseID in ( " +
                                   " select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew  " +
                                   " where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and b.HouseID=a.HouseID and a.SubjectID=c.ID";
            }


            DataTable dt = SQLBase.FillTable(strField, "MainInventory");
            return dt;

        }
        #endregion
        #region 【基本出库】
        //导出
        public static DataTable BasicStockOutToExcelFZ(string strWhere, string ListOutID, ref string strErr)
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            String strField = "";
            if (unitid == "46" || unitid == "32")
            {
                strField = "select ListOutID,c.Text, ProOutTime, ProOutUser, Amount, [Use],[Type], Purchase, b.HouseName, Remark,(case when State=0 then '未出库' else '已出库' end) as State   " +
                               " from BGOI_Inventory.dbo.tk_StockOut a,BGOI_Inventory.dbo.tk_WareHouse b, " +
                               " BGOI_Inventory.dbo.tk_ConfigSubject c  where " +
                               " a.ListOutID='" + ListOutID + "' and a.HouseID in ( " +
                               " select HouseID from BGOI_Inventory.dbo.tk_WareHouse) and b.HouseID=a.HouseID and a.SubjectID=c.ID";
            }
            else
            {
                strField = "select ListOutID,c.Text, ProOutTime, ProOutUser, Amount, [Use],[Type], Purchase, b.HouseName, Remark," +
                                   " (case when State=0 then '未出库' else '已出库' end) as State   " +
                                   " from BGOI_Inventory.dbo.tk_StockOut a,BGOI_Inventory.dbo.tk_WareHouse b, " +
                                   " BGOI_Inventory.dbo.tk_ConfigSubject c  where " +
                                   " a.ListOutID='" + ListOutID + "' and a.HouseID in ( " +
                                   " select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID in(select DeptId from BJOI_UM.dbo.UM_UnitNew  " +
                                   " where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) and b.HouseID=a.HouseID and a.SubjectID=c.ID";
            }
            DataTable dt = SQLBase.FillTable(strField, "MainInventory");
            return dt;

        }
        #endregion
        #endregion

        #region [提醒]
        //入库
        public static DataTable GetNumTiXinRu()
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            if (unitid == "46" || unitid == "32")
            {
                sql = " select * from ( select * from (select sum(case [Type] when '基本' then lie else 0 end) as jb, " +
                                           "sum(case [Type] when '采购' then lie else 0 end) as cg, " +
                                           "sum(case [Type] when '退货检验' then lie else 0 end) as th, " +
                                           "sum(case [Type] when '撤样机' then lie else 0 end) as cj, " +
                                           "sum(case [Type] when '生产组装入库' then lie else 0 end) as sc " +
                                           "from (select [Type],COUNT(*) as lie from BGOI_Inventory.dbo.tk_StockIn where ValiDate='v' and State='0' " +
                                           "and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse ) " +
                                           "group by [Type]) a) A, " +
                                           "(select sum(case Ctype when '基本' then lie else 0 end) as jbc,  " +
                                           "sum(case Ctype when '零售销售' then lie else 0 end) as ls,  " +
                                           "sum(case Ctype when '项目销售' then lie else 0 end) as xm, " +
                                           "sum(case Ctype when '二级库' then lie else 0 end) as rj,  " +
                                           "sum(case Ctype when '上样机' then lie else 0 end) as sx,  " +
                                           "sum(case Ctype when '内购/赠送' then lie else 0 end) as ng  " +
                                           "from (select [Type] as Ctype,COUNT(*) as lie from BGOI_Inventory.dbo.tk_StockOut where ValiDate='v' and State='0' " +
                                           "and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse) " +
                                           "group by [Type]) a)B )c " +
                                           " where c.jb is not null and c.jb !='' " +
                                           "or  c.cg is not null and c.cg !='' or  c.th is not null and c.th !=''  " +
                                           "or  c.cj is not null and c.cj !=''  " +
                                           "or  c.sc is not null and c.sc !=''  " +
                                           "or  c.jbc is not null and c.jbc !=''  " +
                                           "or  c.ls is not null and c.ls !=''  " +
                                           "or  c.xm is not null and c.xm !=''  " +
                                           "or  c.rj is not null and c.rj !=''  " +
                                           "or  c.sx is not null and c.sx !=''  " +
                                           "or  c.ng is not null and c.ng !='' ";
            }
            else
            {
                sql = " select * from ( select * from (select sum(case [Type] when '基本' then lie else 0 end) as jb, " +
                               "sum(case [Type] when '采购' then lie else 0 end) as cg, " +
                               "sum(case [Type] when '退货检验' then lie else 0 end) as th, " +
                               "sum(case [Type] when '撤样机' then lie else 0 end) as cj, " +
                               "sum(case [Type] when '生产组装入库' then lie else 0 end) as sc " +
                               "from (select [Type],COUNT(*) as lie from BGOI_Inventory.dbo.tk_StockIn where ValiDate='v' and State='0' " +
                               "and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse  " +
                               "where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) " +
                               "group by [Type]) a) A, " +
                               "(select sum(case Ctype when '基本' then lie else 0 end) as jbc,  " +
                               "sum(case Ctype when '零售销售' then lie else 0 end) as ls,  " +
                               "sum(case Ctype when '项目销售' then lie else 0 end) as xm, " +
                               "sum(case Ctype when '二级库' then lie else 0 end) as rj,  " +
                               "sum(case Ctype when '上样机' then lie else 0 end) as sx,  " +
                               "sum(case Ctype when '内购/赠送' then lie else 0 end) as ng  " +
                               "from (select [Type] as Ctype,COUNT(*) as lie from BGOI_Inventory.dbo.tk_StockOut where ValiDate='v' and State='0' " +
                               "and HouseID in (select HouseID from  BGOI_Inventory.dbo.tk_WareHouse  " +
                               "where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + GAccount.GetAccountInfo().UnitID + "' or SuperId='" + GAccount.GetAccountInfo().UnitID + "')) " +
                    //"where UnitID in (select DeptId from BJOI_UM.dbo.UM_UnitNew  " +
                    //"where SuperId in (select SuperId from BJOI_UM.dbo.UM_UnitNew where  DeptId='" + GAccount.GetAccountInfo().UnitID + "') or DeptId='" + GAccount.GetAccountInfo().UnitID + "')) " +
                               "group by [Type]) a)B )c " +
                               " where c.jb is not null and c.jb !='' " +
                               "or  c.cg is not null and c.cg !='' or  c.th is not null and c.th !=''  " +
                               "or  c.cj is not null and c.cj !=''  " +
                               "or  c.sc is not null and c.sc !=''  " +
                               "or  c.jbc is not null and c.jbc !=''  " +
                               "or  c.ls is not null and c.ls !=''  " +
                               "or  c.xm is not null and c.xm !=''  " +
                               "or  c.rj is not null and c.rj !=''  " +
                               "or  c.sx is not null and c.sx !=''  " +
                               "or  c.ng is not null and c.ng !='' ";
            }


            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetNumTiXinRuNew()
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string sql = "";
            if (unitid == "46" || unitid == "32")
            {
                sql = " (select COUNT(*)as cgr FROM [BGOI_PP].[dbo].[PurchaseInventorys] a left join BGOI_Inventory.dbo.tk_WareHouse b on a.CKID=b.HouseID where a.Validate='1' and a.State='0') " +
                        " union all " +
                        " (select count(*) as thr from BGOI_Sales.dbo.Exchange_Check where IState='0')  " +
                        " union all " +
                        " (select COUNT(*) as cyr from BGOI_Sales.dbo.PrototypeDetail a,BGOI_Sales.dbo.PrototypeApply b,BGOI_Sales.dbo.tk_ConfigFiveMalls c where b.Malls=c.ID and a.OperateType='1' and a.IState='0' and a.PAID in(select PAID from BGOI_Sales.dbo.PrototypeApply)) " +
                        " union all " +
                        " (select COUNT(*)as scr from BGOI_Produce.dbo.tk_PStocking where Type='生产组装入库' and State='0') " +
                        " union all " +
                        " (select COUNT(*)as xsc from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa03') " +
                        " union all " +
                        " (select COUNT(*)as xmc from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa01') " +
                        " union all " +
                        " (select COUNT(*) as sxc from BGOI_Sales.dbo.PrototypeDetail where IState='0' and OperateType='0' and PAID in (select PAID from BGOI_Sales.dbo.PrototypeApply)) " +
                        " union all " +
                        " (select COUNT(*)as ngc from BGOI_Sales.dbo.Internal_Detail where IState='0' and IOID in (select IOID from BGOI_Sales.dbo.InternalOrder)) " +
                        " union all " +
                        " (select COUNT(*)as scc from [BGOI_Produce].[dbo].[tk_MaterialForm]  where State='0') "+
                        " union all "+
                        " (select COUNT(*) as jyc from [BGOI_Sales].dbo.Alarm a left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID where  b.LibraryTubeState=0 and b.AfterSaleState=0 ) ";

            }
            else
            {
                sql = " (select COUNT(*)as cgr FROM [BGOI_PP].[dbo].[PurchaseInventorys] a left join BGOI_Inventory.dbo.tk_WareHouse b on a.CKID=b.HouseID where a.Validate='1' and a.State='0' and a.UnitID='" + unitid + "') " +
                       " union all " +
                       " (select count(*) as thr from BGOI_Sales.dbo.Exchange_Check where IState='0')  " +
                       " union all " +
                       " (select COUNT(*) as cyr from BGOI_Sales.dbo.PrototypeDetail a,BGOI_Sales.dbo.PrototypeApply b,BGOI_Sales.dbo.tk_ConfigFiveMalls c where b.Malls=c.ID and a.OperateType='1' and a.IState='0' and a.PAID in(select PAID from BGOI_Sales.dbo.PrototypeApply where UnitID='" + unitid + "')) " +
                       " union all " +
                       " (select COUNT(*)as scr from BGOI_Produce.dbo.tk_PStocking where Type='生产组装入库' and State='0') " +
                       " union all " +
                       " (select COUNT(*)as xsc from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa03'  and UnitID='" + unitid + "') " +
                       " union all " +
                       " (select COUNT(*)as xmc from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where a.IState='0' and a.OrderID=b.OrderID and b.SalesType='Sa01' and UnitID='" + unitid + "') " +
                       " union all " +
                       " (select COUNT(*) as sxc from BGOI_Sales.dbo.PrototypeDetail where IState='0' and OperateType='0' and PAID in (select PAID from BGOI_Sales.dbo.PrototypeApply where UnitID='" + unitid + "')) " +
                       " union all " +
                       " (select COUNT(*)as ngc from BGOI_Sales.dbo.Internal_Detail where IState='0' and IOID in (select IOID from BGOI_Sales.dbo.InternalOrder where UnitID='" + unitid + "')) " +
                       " union all " +
                       " (select COUNT(*)as scc from [BGOI_Produce].[dbo].[tk_MaterialForm]  where State='0') "+
                       " union all " +
                       " (select COUNT(*) as jyc from [BGOI_Sales].dbo.Alarm a left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID where  b.LibraryTubeState=0 and b.AfterSaleState=0 and b.UnitID='" + unitid + "') ";
            }
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion

        #region [库存账单]
        public static DataTable InventoryBillList(string start, string end, string where, string PID, string Spec, string ProName, string SingleLibrary, string ListID)
        {
            string where2 = "";
            string where3 = "";
            if (ListID != "")
            {
                where2 += "  and b.ListInID  like '%" + ListID + "%' ";
                where3 += "  and d.ListOutID  like '%" + ListID + "%'  ";
            }
            if (Spec != "")
            {
                where2 += "  and b.Spec  like '%" + Spec + "%' ";
                where3 += "  and d.Spec  like '%" + Spec + "%'  ";
            }
            if (PID != "")
            {
                where2 += "  and b.ProductID  like '%" + PID + "%' ";
                where3 += "  and d.ProductID  like '%" + PID + "%'  ";
            }
            if (ProName != "")
            {
                where2 += "  and b.ProName  like '%" + ProName + "%' ";
                where3 += "  and d.ProName  like '%" + ProName + "%'  ";
            }
            #region [合并代码]
            string sql = "select * " +
                        "  from(select " +
                        "  c.ListInID as 'rkd', year(c.StockInTime) as 'n' , MONTH(c.StockInTime)as 'y',day(c.StockInTime) as 'r'," +
                        "  c.StockInTime,  a.ProductID,g.ProName,g.Spec,f.HouseName,b.StockInCount,a.FinishCount,  " +
                        "  g.Price2,(g.Price2*b.StockInCount) as intotalpricec ," +
                        "  (g.Price2*a.FinishCount) as fintotalprice   " +
                        "  from (select * from BGOI_Inventory.dbo.tk_StockRemain " +
                        "  where HouseID IN (select HouseID from BGOI_Inventory.dbo.tk_WareHouse   " +
                        "  where UnitID='47')) a   " +
                        "  left join  BGOI_Inventory.dbo.tk_StockInDetail b  on a.ProductID=b.ProductID  " +
                        "  left join  BGOI_Inventory.dbo.tk_StockIn c  on b.ListInID=c.ListInID  " +
                        "  left join  BGOI_Inventory.dbo.tk_WareHouse f on a.HouseID=f.HouseID and f.HouseID=c.HouseID  " +
                        "  left join  BGOI_Inventory.dbo.tk_ProductInfo g on a.ProductID=g.PID     " +
                        "  where f.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and convert(varchar(10),c.StockInTime,120) between '" + start + "' and '" + end + "'  " +
                        "  and c.ValiDate='v' " + where2 + " " + where + "" +
                        "  union all " +
                        "  select  " +
                        "  e.ListOutID,year(e.ProOutTime) as 'n' ,MONTH(e.ProOutTime)as 'y',day(e.ProOutTime) as 'r', " +
                        "  e.ProOutTime, a.ProductID,g.ProName,g.Spec,f.HouseName, d.StockOutCount,a.FinishCount, " +
                        "  g.Price2,(g.Price2*d.StockOutCount) as outtotalprice, " +
                        "  (g.Price2*a.FinishCount) as fintotalprice   " +
                        "  from (select * from BGOI_Inventory.dbo.tk_StockRemain " +
                        "  where HouseID IN (select HouseID from BGOI_Inventory.dbo.tk_WareHouse   where UnitID='47')) a   " +
                        "  left join  BGOI_Inventory.dbo.tk_StockOutDetail d on a.ProductID=d.ProductID  " +
                        "  left join  BGOI_Inventory.dbo.tk_StockOut e on d.ListOutID=e.ListOutID    " +
                        "  left join  BGOI_Inventory.dbo.tk_WareHouse f on a.HouseID=f.HouseID and f.HouseID=e.HouseID  " +
                        "  left join  BGOI_Inventory.dbo.tk_ProductInfo g on a.ProductID=g.PID     " +
                        "  where f.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and convert(varchar(10),e.ProOutTime,120) between '" + start + "' and '" + end + "'   " +
                        "  and e.ValiDate='v' " + where3 + " " + where + ") k order by k.StockInTime asc ";
            #endregion
            #region [分开代码]
            //string sql = "";
            //  if (SingleLibrary == "0")
            //  {
            //      sql = "select c.ListInID as 'rkd', year(c.StockInTime) as 'n' , MONTH(c.StockInTime)as 'y',day(c.StockInTime) as 'r',c.StockInTime, " +
            //          // " e.ListOutID,year(e.ProOutTime) as 'n' ,MONTH(e.ProOutTime)as 'y',day(e.ProOutTime) as 'r',e.ProOutTime,d.StockOutCount, (g.UnitPrice*d.StockOutCount) as outtotalprice," +
            //           " a.ProductID,g.ProName,g.Spec,f.HouseName,b.StockInCount,a.FinishCount, " +
            //           " g.UnitPrice,(g.UnitPrice*b.StockInCount) as intotalprice, " +
            //           " (g.UnitPrice*a.FinishCount) as fintotalprice  " +
            //           " from (select * from BGOI_Inventory.dbo.tk_StockRemain where HouseID IN (select HouseID from BGOI_Inventory.dbo.tk_WareHouse  " +
            //           " where UnitID='" + GAccount.GetAccountInfo().UnitID + "')) a  " +
            //           " left join  BGOI_Inventory.dbo.tk_StockInDetail b  on a.ProductID=b.ProductID " +
            //           " left join  BGOI_Inventory.dbo.tk_StockIn c  on b.ListInID=c.ListInID " +
            //          //" left join  BGOI_Inventory.dbo.tk_StockOutDetail d on a.ProductID=d.ProductID " +
            //          //" left join  BGOI_Inventory.dbo.tk_StockOut e on d.ListOutID=e.ListOutID   " +and f.HouseID=e.HouseID
            //           " left join  BGOI_Inventory.dbo.tk_WareHouse f on a.HouseID=f.HouseID and f.HouseID=c.HouseID " +
            //           " left join  BGOI_Inventory.dbo.tk_ProductInfo g on a.ProductID=g.PID    " +
            //          " where f.UnitID='" + GAccount.GetAccountInfo().UnitID + "' and convert(varchar(10),c.StockInTime,120) between '" + start + "' and '" + end + "'  and c.ValiDate='v'  " + where;
            //      //" and convert(varchar(10),e.ProOutTime,120) between '" + start + "' and '" + end + "' " +where;
            //  }
            //  else
            //  {
            //      sql = "select " +
            //           " e.ListOutID,year(e.ProOutTime) as 'n' ,MONTH(e.ProOutTime)as 'y',day(e.ProOutTime) as 'r',e.ProOutTime,d.StockOutCount, (g.UnitPrice*d.StockOutCount) as outtotalprice," +
            //           " a.ProductID,g.ProName,g.Spec,f.HouseName,a.FinishCount, " +
            //           " g.UnitPrice, " +
            //           " (g.UnitPrice*a.FinishCount) as fintotalprice  " +
            //           " from (select * from BGOI_Inventory.dbo.tk_StockRemain where HouseID IN (select HouseID from BGOI_Inventory.dbo.tk_WareHouse  " +
            //           " where UnitID='" + GAccount.GetAccountInfo().UnitID + "')) a  " +
            //          //" left join  BGOI_Inventory.dbo.tk_StockInDetail b  on a.ProductID=b.ProductID " +
            //          //" left join  BGOI_Inventory.dbo.tk_StockIn c  on b.ListInID=c.ListInID " +
            //          " left join  BGOI_Inventory.dbo.tk_StockOutDetail d on a.ProductID=d.ProductID " +
            //          " left join  BGOI_Inventory.dbo.tk_StockOut e on d.ListOutID=e.ListOutID   " +
            //          " left join  BGOI_Inventory.dbo.tk_WareHouse f on a.HouseID=f.HouseID and f.HouseID=e.HouseID " +
            //          " left join  BGOI_Inventory.dbo.tk_ProductInfo g on a.ProductID=g.PID    " +
            //          " where f.UnitID='" + GAccount.GetAccountInfo().UnitID + "'" +
            //          " and convert(varchar(10),e.ProOutTime,120) between '" + start + "' and '" + end + "'  and e.ValiDate='v'  " + where;
            //  }
            #endregion
            #region [旧代码]
            //string PIDnew = "";
            //string Specnew = "";
            //string ProNamenew = "";
            //if (PID != "")
            //{
            //    PIDnew += " and  a.ProductID='" + PID + "'";
            //}
            //if (Spec != "请选择")
            //{
            //    Specnew += " and  g.Spec='" + Spec + "'";
            //}
            //if (ProName != "")
            //{
            //    ProNamenew += " and  g.ProName='" + ProName + "'";
            //}
            //string sql = " select a.PID,f.HouseName, a.UnitPrice, b.StockInCount,c.StockOutCount,d.FinishCount, " +
            //            "(a.UnitPrice*b.StockInCount) as intotalprice,(a.UnitPrice*c.StockOutCount) as outtotalprice, " +
            //            "(a.UnitPrice*d.FinishCount) as fintotalprice from BGOI_Inventory.dbo.tk_ProductInfo a  " +
            //            "left join (select ProductID,sum(StockInCount) as StockInCount from BGOI_Inventory.dbo.tk_StockInDetail  " +
            //            "where ListInID in(select ListInID from BGOI_Inventory.dbo.tk_StockIn where HouseID in ( " +
            //            "select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' " +
            //            ")and convert(varchar(10),StockInTime,120) between '" + start + "' and '" + end + "' " + PIDnew + " "+Specnew+" "+ProNamenew+") " +
            //            "group by ProductID)b on a.PID=b.ProductID " +
            //            "left join (select ProductID,sum(StockOutCountActual) as StockOutCount from BGOI_Inventory.dbo.tk_StockOutDetail  " +
            //            "where ListOutID in(select ListOutID from BGOI_Inventory.dbo.tk_StockOut where HouseID in ( " +
            //            "select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' " +
            //            ")and convert(varchar(10),ProOutTime,120) between '" + start + "' and '" + end + "'  " +
            //            "    " + PIDnew + " " + Specnew + " " + ProNamenew + " " +
            //             ")group by ProductID,UnitPrice)c on a.PID=c.ProductID " +
            //            "left join (select ProductID,FinishCount,HouseID from BGOI_Inventory.dbo.tk_StockRemain  " +
            //            "where HouseID in(select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + GAccount.GetAccountInfo().UnitID + "' " +
            //            ")    " + PIDnew + "  )d on a.PID=d.ProductID " +
            //            "left join (select ID from  BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "' ) e on a.Ptype=e.ID " +
            //            "left join BGOI_Inventory.dbo.tk_WareHouse f on d.HouseID= f.HouseID " +
            //            "  where StockInCount!='' or StockOutCount!='' or FinishCount!=''  ";
            //if (where != "")
            //{
            //    sql +=" and "+where;
            //}
            #endregion
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;

        }
        #endregion
        #region [产品类型设置]
        public static DataTable GetConfigPType()
        {
            string str = "select ID,Text from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainInventory");
            return dt;
        }
        public static UIDataTable ProductTypeSettingList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from  BGOI_Inventory.dbo.tk_ConfigPType " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " OID ";
            String strTable = " BGOI_Inventory.dbo.tk_ConfigPType  ";
            String strField = " ID, OID, Text, UnitID, Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {

            }

            return instData;
        }
        //加载仓库ID
        public static int OID()
        {
            string strID = "";
            string strD = "";
            string strSqlID = "select max(OID) from BGOI_Inventory.dbo.tk_ConfigPType  where UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "0";
                }
                else
                {
                    int num = Convert.ToInt32(strID);
                    if (num < 9)
                        strD = strD + "" + (num + 1);
                    else if (num < 99 && num >= 9)
                        strD = strD + "" + (num + 1);
                    else
                        strD = strD + (num + 1);
                }
            }
            else
            {
                strD = strD + "0";
            }
            return Convert.ToInt32(strD);
        }
        //加载仓库ID
        public static string GetID()
        {
            string strID = "";
            string strD = "PT0";
            string strSqlID = "select max(OID) from BGOI_Inventory.dbo.tk_ConfigPType where  Validate='v' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' ";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
            }
            string strMaxID = "select ID from BGOI_Inventory.dbo.tk_ConfigPType where  Validate='v' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' and OID='" + strID + "'";
            DataTable dtMaxID = SQLBase.FillTable(strMaxID);
            if (dtMaxID != null && dtMaxID.Rows.Count > 0)
            {
                strID = dtMaxID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + "0";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));
                    if (num < 9)
                        strD = strD + "" + (num + 1);
                    else if (num < 99 && num >= 9)
                        strD = strD + "" + (num + 1);
                    else
                        strD = strD + (num + 1);
                }
            }
            else
            {
                strD = strD + "0";
            }
            return strD;
        }

        public static bool SaveAddProductTypeSetting(tk_ConfigPType rem, ref string strErr, string type)
        {
            SQLTrans trans = new SQLTrans();
            if (type == "1")
            {
                string sql = " select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and ID='" + rem.ID + "' and Validate='v'";
                DataTable dt = SQLBase.FillTable(sql, "MainInventory");
                if (dt == null || dt.Rows.Count < 1)
                {
                    strErr = "";
                    int count = 0;
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_ConfigPType>(rem, "[BGOI_Inventory].[dbo].tk_ConfigPType");
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "添加失败";
                        return false;
                    }
                }
                else
                {
                    strErr = "该类型已存在";
                    return false;
                }
            }
            else
            {
                #region [插入历史库]
                string oldstr = "select * from BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and ID='" + rem.ID + "' and Validate='v'";
                DataTable dt = SQLBase.FillTable(oldstr, "MainInventory");
                tk_ConfigPType_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_ConfigPType_HIS();
                    vishis.ID = dr["ID"].ToString();
                    vishis.OID = Convert.ToInt32(dr["OID"].ToString());
                    vishis.Text = dr["Text"].ToString();
                    vishis.UnitID = dr["UnitID"].ToString();
                    vishis.Validate = dr["Validate"].ToString();
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                }
                string strhis = GSqlSentence.GetInsertInfoByD<tk_ConfigPType_HIS>(vishis, "BGOI_Inventory.dbo.tk_ConfigPType_HIS");
                if (SQLBase.ExecuteNonQuery(strhis) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存产品类型','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ConfigPType_HIS','" + rem.ID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存产品类型','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ConfigPType_HIS','" + rem.ID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion

                #region [修改]
                string strUpdateList = "update BGOI_Inventory.dbo.tk_ConfigPType set ID='" + rem.ID + "', OID='" + rem.OID + "', Text='" + rem.Text + "', UnitID='" + rem.UnitID + "', Validate='" + rem.Validate + "'" +
                                      " where ID='" + rem.ID + "' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' and Validate='v'";
                if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改产品类型','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ConfigPType','" + rem.ID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                    return true;
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改产品类型','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_ConfigPType','" + rem.ID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                    strErr = "修改失败";
                    return false;
                }
                #endregion
            }
        }

        //撤销
        public static bool DeProductTypeSetting(string ID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = "update BGOI_Inventory.dbo.tk_ConfigPType set Validate='i' where UnitID='" + GAccount.GetAccountInfo().UnitID + "' and ID='" + ID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }
        //修改加载信息
        public static DataTable UpProductTypeSetting(string ID)
        {
            string sql = "select * from BGOI_Inventory.dbo.tk_ConfigPType  where Validate='v' and UnitID='" + GAccount.GetAccountInfo().UnitID + "' and ID='" + ID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion

        #region [上传]
        public static bool InsertAwardInOut(tk_AwardInOut bas, HttpFileCollection file, ref string a_strErr)
        {
            int intLog = 0;
            string strLog = "";
            string FileName = "";
            string savePath = "";
            string strInsertFile = "";
            int intInsertFile = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            for (int i = 0; i < file.Count; i++)
            {
                if (file[i] == null || file[i].ContentLength <= 0)
                {
                    a_strErr = "文件不能为空";
                }
                else
                {
                    string filename = Path.GetFileName(file[i].FileName);
                    int filesize = file[i].ContentLength;//获取上传文件的大小单位为字节byte
                    string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                    string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                    int Maxsize = 10000 * 1024;//定义上传文件的最大空间大小为10M
                    string FileType = ".xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt,.JPG,.PNG";//定义上传文件的类型字符串
                    // + DateTime.Now.ToString("yyyyMMddhhmmss")
                    FileName = NoFileName + fileEx;
                    if (!FileType.Contains(fileEx))
                    {
                        a_strErr = "文件类型不对，只能上传 .xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt 格式的文件";
                    }
                    if (filesize >= Maxsize)
                    {
                        a_strErr = "上传文件超过10M，不能上传";
                    }
                    string path = System.Configuration.ConfigurationSettings.AppSettings["UPStoin"];
                    if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
                    {
                        Directory.CreateDirectory(path);
                    }
                    savePath = Path.Combine(path, FileName);
                    file[i].SaveAs(savePath);
                    strInsertFile = "insert into tk_AwardInOut (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
                    strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + savePath + "','" + DateTime.Now + "','" + acc.UserName + "','v')";
                    intInsertFile = SQLBase.ExecuteNonQuery(strInsertFile, "MainInventory");
                    if (intInsertFile == 1)
                    {

                        strLog = "Insert into [BGOI_Inventory].[dbo].[tk_Inventorylog](LogTitle, LogContent, Time, Person, Type, Typeid) " +
                            "values ('上传文件信息','上传成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_AwardInOut','" + bas.SID + "')";
                    }
                }
            }
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "MainInventory");
                }
            }
            catch (SqlException e)
            {
                a_strErr = e.Message;
                return false;
            }

            return (intInsertFile + intLog) >= 2;
        }
        public static string GetTopSID()
        {
            string strID = "";
            string strD = "SC-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(SID) from BGOI_Inventory.dbo.tk_AwardInOut";
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            DataTable dtID = SQLBase.FillTable(strSqlID);
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + UnitID + "001";
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(3, 8);
                    if (DateTime.Now.ToString("yyyyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + UnitID + "00" + (num + 1);
                        else if (num < 99 && num >= 9)
                            strD = strD + UnitID + "0" + (num + 1);

                        else
                            strD = strD + UnitID + (num + 1);
                    }
                    else
                    {
                        strD = strD + UnitID + "001";
                    }
                }
            }
            else
            {
                strD = strD + UnitID + "001";
            }
            return strD;

        }
        public static UIDataTable InOutUploadList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_Inventory.dbo.tk_AwardInOut  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " " + where;
            string strOrderBy = " AwardTime ";
            String strTable = " BGOI_Inventory.dbo.tk_AwardInOut ";
            String strField = "  SID, ID, Award, AwardInfo, AwardTime, CreatUser, Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
            }
            return instData;
        }
        //撤销
        public static bool DeInOutUpload(string SID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainInventory");
            try
            {
                string strInsert = "update BGOI_Inventory.dbo.tk_AwardInOut set Validate='i' where SID='" + SID + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "撤销失败！";
                    return false;
                }
            }
            catch
            {
                GLog.LogError("RM_CarRESInfo");
                return false;
            }
            finally
            {

            }
        }

        public static DataTable GetDownloadAward(string id)
        {
            string strSql = "select Award,AwardInfo from BGOI_Inventory.dbo.tk_AwardInOut where id = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
            return dt;
        }

        public static DataTable GetFilesNew(string OId)
        {
            string sql = "select AwardInfo from BGOI_Inventory.dbo.tk_AwardInOut  where Validate='V' and SID='" + OId + "' order by AwardTime";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        #endregion
    }
}
