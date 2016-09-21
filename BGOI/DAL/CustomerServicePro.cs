using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class CustomerServicePro
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
            DataTable dt = SQLBase.FillTable(strField, "MainCustomer");
            return dt;
        }
        //添加日志
        public static bool AddCustomerServiceLog(tk_CustomerServicelog logobj)
        {
            int count = 0;
            string strInsert = GSqlSentence.GetInsertInfoByD(logobj, " [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]");
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
        //根据NAME加载产品规格
        public static DataTable GetProSpec(string KID)
        {
            string sql = "select * from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + KID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //加载保修卡
        public static DataTable GetWXGCard()
        {
            string str = "select BXKID,BXKNum From BGOI_CustomerService.dbo.tk_WXGuaranteeCard";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到产品名称
        public static DataTable GetOrderContent()
        {
            string str = "select PID,ProName From BGOI_Inventory.dbo.tk_ProductInfo";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到产品规格
        public static DataTable GetSpecsModels()
        {
            string str = "select PID,Spec From BGOI_Inventory.dbo.tk_ProductInfo";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到一级库
        public static DataTable GetOneWareHouse()
        {
            string str = "select HouseID,HouseName From  BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='1'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到二级库
        public static DataTable GetTwoWareHouse()
        {
            string str = "select HouseID,HouseName From  BGOI_Inventory.dbo.tk_WareHouse where IsHouseID='2'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
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
        public static DataTable NewGetWarrantyCardToExcel(string strWhere, string tableName, string FieldName, string OrderBy, ref string strErr)
        {
            String strField = "select  " + FieldName + ""
                + "from " + tableName + " where  " + strWhere + " order by " + OrderBy + "";
            DataTable dt = SQLBase.FillTable(strField, "SalesDBCnn");
            return dt;

        }
        //根据用户id加载客户
        public static DataTable GetKClientBasID()
        {
            string DeptId = GAccount.GetAccountInfo().UnitID;
            string str = "select UserId,UserName from BJOI_UM.dbo.UM_UserNew WHERE DeptId='" + DeptId + "'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //根据用户id加载部门
        public static DataTable GetDepName()
        {
            string SuperId = "";
            string DeptId = GAccount.GetAccountInfo().UnitID;
            string strdrt = "select SuperId from BJOI_UM.dbo.UM_UnitNew where DeptId='" + DeptId + "'";
            DataTable drt = SQLBase.FillTable(strdrt, "MainCustomer");
            foreach (DataRow dr in drt.Rows)
            {
                SuperId = dr["SuperId"].ToString();
            }

            string str = "select DeptId,DeptName from BJOI_UM.dbo.UM_UnitNew where SuperId='" + SuperId + "'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到客户
        public static DataTable GetKClientBas()
        {
            string str = "select KID,DeclareUser From BGOI_BasMan.dbo.tk_KClientBas";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //得到分公司
        public static DataTable GetCompany()
        {
            string str = "select ID,Text From BGOI_CustomerService.dbo.tk_Customerser_Config where Type='gongshi' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        //加载客户dt
        public static DataTable GetKClientBas(string KID)
        {
            string sql = "select Phone,ComAddress,CName From BGOI_BasMan.dbo.tk_KClientBas where KID='" + KID + "' or DeclareUser='" + KID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        //根据选择id加载客户
        public static DataTable GetUserName(string DeptId)
        {
            string sql = "";
            //if (DeptId != "")
            //{
            sql = "select UserId,UserName from BJOI_UM.dbo.UM_UserNew where DeptId='" + DeptId + "'";
            //}
            //else
            //{
            //    sql = "select UserId,UserName from BJOI_UM.dbo.UM_UserNew";
            //}

            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        #region [加载订单]
        public static UIDataTable ChangeOrderList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            string OrderContactor = "";
            string sqloderuser = "select * from BGOI_BasMan.dbo.tk_KClientBas where  KID='" + ptype + "'";
            DataTable dt = SQLBase.FillTable(sqloderuser, "MainCustomer");
            foreach (DataRow dr in dt.Rows)
            {
                OrderContactor = dr["CName"].ToString();
            }
            string where = "";
            if (OrderContactor != "")
            {
                where = " and OrderContactor='" + OrderContactor + "'";
            }
            else
            {
                where = "";
            }
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) From BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b where a.OrderID=b.OrderID " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.OrderID=b.OrderID " + where;
            string strOrderBy = " a.PID ";
            String strTable = " BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b  ";
            String strField = " b.ProductID as ProductID,a.OrderID,ContractDate,OrderContent,SpecsModels,b.OrderUnit,OrderNum ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }
        #endregion
        #region [加载产品]
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            string strSelCount = "";
            UIDataTable instData = new UIDataTable();

            strSelCount = "select count(*)  From BGOI_Inventory.dbo.tk_ConfigPType ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "";
            string strOrderBy = " ID  ";
            String strTable = "BGOI_Inventory.dbo.tk_ConfigPType";
            String strField = "ID,Text";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }

        public static DataTable GetBasicDetail(string PID)
        {
            string sql = "select PID as ProductID,ProName,Spec,UnitPrice,Units,Remark From BGOI_Inventory.dbo.tk_ProductInfo where   PID in (" + PID + ")";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        //根据类型加载产品信息
        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            string where = "";
            if (ptype != "")
            {
                where = " and Ptype='" + ptype + "'";
            }
            else
            {
                where = "";
            }
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b where a.Ptype=b.ID " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.Ptype=b.ID " + where;
            string strOrderBy = " PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,BGOI_Inventory.dbo.tk_ConfigPType b  ";
            String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }
        #endregion
        #region [用户服务]

        #region [用户回访]
        public static string GetTopHFID()
        {
            string strID = "";
            string strD = "HF-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(HFID) from BGOI_CustomerService.dbo.tk_SHReturnVisit";
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
        //DID
        public static string GetTopHFIDDID()
        {
            string strID = "";
            string strD = "HFID-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHRV_Product";
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
        //添加修改
        public static bool SaveUserVisit(tk_SHReturnVisit project, List<tk_SHRV_Product> list, string type, string HFIDold, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            if (type == "1")
            {
                #region [插入历史表]
                string oldshrv = "select * from BGOI_CustomerService.dbo.tk_SHReturnVisit where  HFID='" + HFIDold + "'";
                DataTable dt = SQLBase.FillTable(oldshrv, "MainCustomer");
                tk_SHReturnVisit_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_SHReturnVisit_HIS();
                    #region [旧]
                    //vishis.HFID = project.HFID;
                    //vishis.RecordID = project.RecordID;
                    //vishis.ContactPerson = project.ContactPerson;
                    //vishis.UserInformation = project.UserInformation;
                    //vishis.SatisfiedDegree = project.SatisfiedDegree;
                    //vishis.Tel = project.Tel;
                    //vishis.ReturnVisit = project.ReturnVisit;
                    //vishis.CreateTime = Convert.ToDateTime(project.CreateTime);
                    //vishis.RVDate = Convert.ToDateTime(project.CreateTime);
                    //vishis.Remark = project.Remark;
                    //vishis.CreateUser = project.CreateUser;
                    //vishis.ReturnVisit = project.ReturnVisit;
                    //vishis.Validate = project.Validate;
                    #endregion
                    #region [新]
                    vishis.HFID = dr["HFID"].ToString();
                    vishis.RecordID = dr["RecordID"].ToString(); ;
                    vishis.ContactPerson = dr["ContactPerson"].ToString();
                    vishis.UserInformation = dr["UserInformation"].ToString();
                    vishis.SatisfiedDegree = dr["SatisfiedDegree"].ToString();
                    vishis.Tel = dr["Tel"].ToString();
                    vishis.ReturnVisit = dr["ReturnVisit"].ToString();
                    vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                    vishis.RVDate = Convert.ToDateTime(dr["CreateTime"].ToString());
                    vishis.Remark = dr["Remark"].ToString();
                    vishis.CreateUser = dr["CreateUser"].ToString();
                    vishis.ReturnVisit = dr["ReturnVisit"].ToString();
                    vishis.Validate = dr["Validate"].ToString();
                    #endregion
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                }
                string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_SHReturnVisit_HIS>(vishis, "BGOI_CustomerService.dbo.tk_SHReturnVisit_HIS");
                if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户回访','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHReturnVisit_HIS','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户回访','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHReturnVisit_HIS','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }

                string oldshrvPro = "select * from BGOI_CustomerService.dbo.tk_SHRV_Product where  HFID='" + HFIDold + "'";
                DataTable dthis = SQLBase.FillTable(oldshrvPro, "MainCustomer");
                tk_SHRV_Product_HIS visprohis = null;
                List<tk_SHRV_Product_HIS> listvisprohis = new List<tk_SHRV_Product_HIS>();
                for (int i = 0; i < dthis.Rows.Count; i++)
                {
                    visprohis = new tk_SHRV_Product_HIS();
                    visprohis.HFID = dthis.Rows[i]["HFID"].ToString();
                    visprohis.DID = dthis.Rows[i]["DID"].ToString();
                    visprohis.PID = dthis.Rows[i]["PID"].ToString();
                    visprohis.OrderContent = dthis.Rows[i]["OrderContent"].ToString();
                    visprohis.Unit = dthis.Rows[i]["Unit"].ToString(); ;
                    visprohis.SpecsModels = dthis.Rows[i]["SpecsModels"].ToString();
                    visprohis.Total = Convert.ToDecimal(dthis.Rows[i]["Total"].ToString());
                    visprohis.UnitPrice = Convert.ToDecimal(dthis.Rows[i]["UnitPrice"].ToString());
                    if (string.IsNullOrEmpty(dthis.Rows[i]["Num"].ToString()))
                    {
                        visprohis.Num = 0;
                    }
                    else
                    {
                        visprohis.Num = Convert.ToInt32(dthis.Rows[i]["Num"].ToString());
                    }
                    visprohis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    visprohis.NCreateTime = DateTime.Now;
                    listvisprohis.Add(visprohis);
                }
                string strInsertListhis = GSqlSentence.GetInsertByList(listvisprohis, "BGOI_CustomerService.dbo.tk_SHRV_Product_HIS");
                if (trans.ExecuteNonQuery(strInsertListhis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户回访','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product_HIS','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户回访','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product_HIS','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHReturnVisit]
                string strUpdateList = "update BGOI_CustomerService.dbo.tk_SHReturnVisit set HFID='" + HFIDold + "', UntiID='" + project.UntiID + "', " +
                  "RecordID='" + project.RecordID + "', RVDate='" + project.RVDate + "', ProductID='" + project.ProductID + "', UserInformation='" + project.UserInformation + "'," +
                  " ContactPerson='" + project.ContactPerson + "', Tel='" + project.Tel + "', SatisfiedDegree='" + project.SatisfiedDegree + "'," +
                  " Remark='" + project.Remark + "', CreateTime='" + project.CreateTime + "', CreateUser='" + project.CreateUser + "', Validate='" + project.Validate + "', ReturnVisit='" + project.ReturnVisit + "'" +
                  " where HFID='" + HFIDold + " ' ";
                if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改数据','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHReturnVisit','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改数据','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHReturnVisit','" + project.HFID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHRV_Product]
                string strUpdateListDel = "";
                int Amount = 0;
                decimal ResultTotal = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    string CheckPro = "select * from BGOI_CustomerService.dbo.tk_SHRV_Product where HFID='" + HFIDold + "' and  PID='" + list[i].PID + "'";
                    DataTable dtCheckPro = SQLBase.FillTable(CheckPro, "MainCustomer");
                    if (dtCheckPro.Rows.Count > 0)
                    {
                        Amount += list[i].Num;
                        ResultTotal += list[i].Total;
                        strUpdateListDel = "update BGOI_CustomerService.dbo.tk_SHRV_Product set HFID='" + HFIDold + "', DID='" + list[i].DID + "', PID='" + list[i].PID + "'," +
                        " OrderContent='" + list[i].OrderContent + "', SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + Amount + "', Total='" + ResultTotal + "', UnitPrice='" + list[i].UnitPrice + "'" +
                        " where HFID='" + HFIDold + "' and PID='" + list[i].PID + "'";
                        if (SQLBase.ExecuteNonQuery(strUpdateListDel) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改数据','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改数据','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    else
                    {
                        string CheckPronew = "Insert into BGOI_CustomerService.dbo.tk_SHRV_Product( HFID, DID, PID, OrderContent, SpecsModels, Unit, Num, Total, UnitPrice) " +
                          "values ('" + HFIDold + "','" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].Total + "','" + list[i].UnitPrice + "')";
                        if (SQLBase.ExecuteNonQuery(CheckPronew) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加数据','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加数据','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                }
                #endregion
                trans.Close(true);
                return true;
            }
            else
            {
                try
                {
                    string strQueryOld = "select * from BGOI_CustomerService.dbo.tk_SHReturnVisit where  RecordID='" + project.RecordID + "'";
                    DataTable dtquold = SQLBase.FillTable(strQueryOld, "MainCustomer");
                    if (dtquold.Rows.Count > 0 && dtquold != null)
                    {
                        strErr = "编号已存在！请重新输入";
                        //trans.Close(true);
                        return false;
                    }
                    else
                    {
                        string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHReturnVisit>(project, "BGOI_CustomerService.dbo.tk_SHReturnVisit");
                        string strInsertList = "";
                        if (list.Count > 0)
                        {
                            strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHRV_Product");
                        }
                        if (strInsert != "")
                        {
                            trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                        }
                        if (strInsertList != "")
                        {
                            if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                            {
                                #region [日志]
                                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('添加用户回访','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                                SQLBase.ExecuteNonQuery(strlog);
                                #endregion
                            }
                            else
                            {
                                #region [日志]
                                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('添加用户回访','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                                SQLBase.ExecuteNonQuery(strlog);
                                #endregion
                            }
                        }

                        trans.Close(true);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    trans.Close(true);
                    return false;
                }
            }
        }
        //用户回访列表 
        public static UIDataTable UserVisitList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from  BGOI_CustomerService.dbo.tk_SHReturnVisit where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHReturnVisit ";
            String strField = " HFID,UserInformation,ContactPerson,Tel,SatisfiedDegree,CreateUser,ReturnVisit,RVDate,RecordID ";
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
                    if (instData.DtData.Rows[r]["SatisfiedDegree"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["SatisfiedDegree"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["SatisfiedDegree"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["SatisfiedDegree"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["SatisfiedDegree"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["SatisfiedDegree"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["SatisfiedDegree"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["SatisfiedDegree"] = "不满意";
                    }
                }
            }

            return instData;
        }
        public static UIDataTable UserVisitDetialList(int a_intPageSize, int a_intPageIndex, string HFID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_SHRV_Product where Validate='v' and HFID='" + HFID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " Validate='v' and HFID='" + HFID + "'";
            string strOrderBy = " HFID ";
            String strTable = "BGOI_CustomerService.dbo.tk_SHRV_Product ";
            String strField = "PID,OrderContent,SpecsModels,Unit,Num,Total,UnitPrice ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            return instData;
        }
        //撤销
        public static bool DeUserVisit(string HFID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHReturnVisit set Validate='i' where HFID='" + HFID + "'";
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
        public static DataTable UpUserVisitPIDList(string HFID)
        {
            string sql = "select  a.HFID as HFID, UntiID, RecordID, RVDate, ProductID, UserInformation, ContactPerson, Tel, SatisfiedDegree, Remark, CreateTime, CreateUser, a.Validate, ReturnVisit, b.DID as DID, PID, OrderContent, SpecsModels, Unit, Num, Total, UnitPrice  from BGOI_CustomerService.dbo.tk_SHReturnVisit a,BGOI_CustomerService.dbo.tk_SHRV_Product b where  b.HFID='" + HFID + "' and a.HFID='" + HFID + "' and a.HFID=b.HFID and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static DataTable GetSupCode()
        {
            string sql = "select RecordID from dbo.tk_SHReturnVisit ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }

        //顾客满意度调查
        public static DataTable GetCustomerSatisfactionSurveyUserVisit(string HFID)
        {
            string sql = "select " +
                " b.PID,b.OrderContent,b.SpecsModels" +
                " from  BGOI_CustomerService.dbo.tk_SHReturnVisit a,BGOI_CustomerService.dbo.tk_SHRV_Product b  where a.HFID=b.HFID and a.HFID='" + HFID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //判断有无满意度调查
        public static DataTable IfGetDiaoCha(string HFID)
        {
            string sql = "select *  from  BGOI_CustomerService.dbo.tk_SHSurvey where  Validate='v'  and HFIDID = '" + HFID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }

        public static bool DeUserPro(string HFID, string PID)
        {
            string strErr = "";
            string sql = "update BGOI_CustomerService.dbo.tk_SHRV_Product set Validate='i' where PID='" + PID + "' and HFID='" + HFID + "'";
            if (SQLBase.ExecuteNonQuery(sql, "MainCustomer") > 0)
            {
                return true;
            }
            else
            {
                strErr = "删除失败";
                //trans.Close(true);
                return false;
            }
        }
        #endregion
        #region [顾客满意度调查]
        public static string GetTopDCID()
        {
            string strID = "";
            string strD = "DC-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(DCID) from BGOI_CustomerService.dbo.tk_SHSurvey";
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
        //DID
        public static string GetTopDCIDDID()
        {
            string strID = "";
            string strD = "DCID-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHSurvey_Product";
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
        //顾客满意度调查列表 
        public static UIDataTable CustomerSatisfactionSurveyList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from  BGOI_CustomerService.dbo.tk_SHSurvey where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "CreateTime ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHSurvey ";
            String strField = " DCID, UntiID, SurveyDate, CustomerID, Customer, ProductQuality, ProductQrice, ProductDelivery, ProductSurvey, CustomerServiceSurvey, SupplySurvey, LeakSurvey, ServiceSurvey, AgencySales, AgencyConsultation, AgencySpareParts, AgencySurvey, Remark, UserSign, CreateTime, CreateUser, Validate,HFIDID";
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
                    #region [产品质量调查结果]
                    if (instData.DtData.Rows[r]["ProductQuality"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["ProductQuality"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["ProductQuality"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["ProductQuality"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["ProductQuality"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["ProductQuality"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["ProductQuality"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["ProductQuality"] = "不满意";
                    }
                    #endregion
                    #region [产品价格调查结果]
                    if (instData.DtData.Rows[r]["ProductQrice"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["ProductQrice"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["ProductQrice"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["ProductQrice"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["ProductQrice"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["ProductQrice"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["ProductQrice"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["ProductQrice"] = "不满意";
                    }
                    #endregion
                    #region [产品交货期调查结果]
                    if (instData.DtData.Rows[r]["ProductDelivery"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["ProductDelivery"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["ProductDelivery"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["ProductDelivery"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["ProductDelivery"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["ProductDelivery"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["ProductDelivery"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["ProductDelivery"] = "不满意";
                    }
                    #endregion
                    #region [服务售后维修，保养服务调查结果]
                    if (instData.DtData.Rows[r]["CustomerServiceSurvey"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["CustomerServiceSurvey"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["CustomerServiceSurvey"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["CustomerServiceSurvey"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["CustomerServiceSurvey"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["CustomerServiceSurvey"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["CustomerServiceSurvey"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["CustomerServiceSurvey"] = "不满意";
                    }
                    #endregion
                    #region [服务备品，备件供应调查结果]
                    if (instData.DtData.Rows[r]["SupplySurvey"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["SupplySurvey"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["SupplySurvey"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["SupplySurvey"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["SupplySurvey"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["SupplySurvey"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["SupplySurvey"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["SupplySurvey"] = "不满意";
                    }
                    #endregion
                    #region [有无漏气现象调查结果]
                    if (instData.DtData.Rows[r]["LeakSurvey"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["LeakSurvey"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["LeakSurvey"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["LeakSurvey"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["LeakSurvey"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["LeakSurvey"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["LeakSurvey"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["LeakSurvey"] = "不满意";
                    }
                    #endregion
                    #region [代理售后维修，保养服务调查结果]
                    if (instData.DtData.Rows[r]["AgencySales"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["AgencySales"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["AgencySales"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["AgencySales"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["AgencySales"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["AgencySales"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["AgencySales"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["AgencySales"] = "不满意";
                    }
                    #endregion
                    #region [代理咨询，维护培训调查结果]
                    if (instData.DtData.Rows[r]["AgencyConsultation"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["AgencyConsultation"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["AgencyConsultation"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["AgencyConsultation"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["AgencyConsultation"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["AgencyConsultation"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["AgencyConsultation"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["AgencyConsultation"] = "不满意";
                    }
                    #endregion
                    #region [代理备品，备件供应调查结果]
                    if (instData.DtData.Rows[r]["AgencySpareParts"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["AgencySpareParts"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["AgencySpareParts"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["AgencySpareParts"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["AgencySpareParts"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["AgencySpareParts"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["AgencySpareParts"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["AgencySpareParts"] = "不满意";
                    }
                    #endregion
                }
            }

            return instData;
        }
        //加载订单信息
        public static DataTable GetCustomerSatisfactionSurvey(string PID)
        {
            string sql = "select b.ProductID, b.OrderID,b.OrderContent,b.SpecsModels,b.OrderUnit,b.OrderNum,a.ContractDate from BGOI_Sales.dbo.OrdersInfo a,BGOI_Sales.dbo.Orders_DetailInfo b where a.OrderID=b.OrderID and  a.OrderID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //添加修改
        public static bool SaveCustomerSatisfactionSurvey(tk_SHSurvey project, List<tk_SHSurvey_Product> list, string type, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            if (type == "1")
            {
                #region [插入历史表]
                string oldshrv = "select * from BGOI_CustomerService.dbo.tk_SHSurvey where  DCID='" + project.DCID + "'";
                DataTable dt = SQLBase.FillTable(oldshrv, "MainCustomer");
                tk_SHSurvey_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_SHSurvey_HIS();
                    #region [旧]
                    //vishis.DCID = project.DCID;
                    //vishis.UntiID = project.UntiID;
                    //vishis.CustomerID = project.CustomerID;
                    //vishis.Customer = project.Customer;
                    //vishis.ProductQuality = project.ProductQuality;
                    //vishis.ProductQrice = project.ProductQrice;
                    //vishis.CreateTime = Convert.ToDateTime(project.CreateTime);
                    //vishis.SurveyDate = Convert.ToDateTime(project.SurveyDate);
                    //vishis.ProductSurvey = project.ProductSurvey;
                    //vishis.CustomerServiceSurvey = project.CustomerServiceSurvey;
                    //vishis.SupplySurvey = project.SupplySurvey;
                    //vishis.LeakSurvey = project.LeakSurvey;
                    //vishis.ServiceSurvey = project.ServiceSurvey;
                    //vishis.AgencySales = project.AgencySales;
                    //vishis.AgencyConsultation = project.AgencyConsultation;
                    //vishis.AgencySpareParts = project.AgencySpareParts;
                    //vishis.AgencySurvey = project.AgencySurvey;
                    //vishis.UserSign = project.UserSign;
                    //vishis.CreateUser = project.CreateUser;
                    //vishis.Validate = project.Validate;
                    //vishis.Remark = project.Remark;
                    //vishis.CreateUser = project.CreateUser;
                    //vishis.ProductDelivery = project.ProductDelivery;
                    #endregion
                    #region [新]
                    vishis.DCID = dr["DCID"].ToString();
                    vishis.UntiID = dr["UntiID"].ToString();
                    vishis.CustomerID = dr["CustomerID"].ToString();
                    vishis.Customer = dr["Customer"].ToString();
                    vishis.ProductQuality = dr["ProductQuality"].ToString();
                    vishis.ProductQrice = dr["ProductQrice"].ToString();
                    vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                    vishis.SurveyDate = Convert.ToDateTime(dr["SurveyDate"].ToString());
                    vishis.ProductSurvey = dr["ProductSurvey"].ToString();
                    vishis.CustomerServiceSurvey = dr["CustomerServiceSurvey"].ToString();
                    vishis.SupplySurvey = dr["SupplySurvey"].ToString();
                    vishis.LeakSurvey = dr["LeakSurvey"].ToString();
                    vishis.ServiceSurvey = dr["ServiceSurvey"].ToString();
                    vishis.AgencySales = dr["AgencySales"].ToString();
                    vishis.AgencyConsultation = dr["AgencyConsultation"].ToString();
                    vishis.AgencySpareParts = dr["AgencySpareParts"].ToString();
                    vishis.AgencySurvey = dr["AgencySurvey"].ToString();
                    vishis.UserSign = dr["UserSign"].ToString();
                    vishis.CreateUser = dr["CreateUser"].ToString();
                    vishis.Validate = dr["Validate"].ToString();
                    vishis.Remark = dr["Remark"].ToString();
                    vishis.CreateUser = dr["CreateUser"].ToString();
                    vishis.ProductDelivery = dr["ProductDelivery"].ToString();
                    #endregion
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                }
                string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_SHSurvey_HIS>(vishis, "BGOI_CustomerService.dbo.tk_SHSurvey_HIS");
                if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存顾客满意度','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_HIS','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存顾客满意度','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_HIS','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }

                string oldshrvPro = "select * from BGOI_CustomerService.dbo.tk_SHSurvey_Product where  DCID='" + project.DCID + "'";
                DataTable dthis = SQLBase.FillTable(oldshrvPro, "MainCustomer");
                tk_SHSurvey_Product_HIS visprohis = null;
                List<tk_SHSurvey_Product_HIS> listvisprohis = new List<tk_SHSurvey_Product_HIS>();
                for (int i = 0; i < dthis.Rows.Count; i++)
                {
                    visprohis = new tk_SHSurvey_Product_HIS();
                    visprohis.DCID = dthis.Rows[i]["DCID"].ToString();
                    visprohis.DID = dthis.Rows[i]["DID"].ToString();
                    visprohis.PID = dthis.Rows[i]["PID"].ToString();
                    visprohis.OrderDate = Convert.ToDateTime(dthis.Rows[i]["OrderDate"].ToString());
                    visprohis.OrderContent = dthis.Rows[i]["OrderContent"].ToString();
                    visprohis.Unit = dthis.Rows[i]["Unit"].ToString(); ;
                    visprohis.SpecsModels = dthis.Rows[i]["SpecsModels"].ToString();
                    visprohis.Total = Convert.ToDecimal(dthis.Rows[i]["Total"].ToString());
                    visprohis.UnitPrice = Convert.ToDecimal(dthis.Rows[i]["UnitPrice"].ToString());
                    if (string.IsNullOrEmpty(dthis.Rows[i]["Num"].ToString()))
                    {
                        visprohis.Num = 0;
                    }
                    else
                    {
                        visprohis.Num = Convert.ToInt32(dthis.Rows[i]["Num"].ToString());
                    }
                    visprohis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    visprohis.NCreateTime = DateTime.Now;
                    listvisprohis.Add(visprohis);
                }
                string strInsertListhis = GSqlSentence.GetInsertByList(listvisprohis, "BGOI_CustomerService.dbo.tk_SHSurvey_Product_HIS");
                if (trans.ExecuteNonQuery(strInsertListhis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存顾客满意度','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product_HIS','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存顾客满意度','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product_HIS','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHSurvey]
                string strUpdateList = "update BGOI_CustomerService.dbo.tk_SHSurvey set DCID='" + project.DCID + "', UntiID='" + project.UntiID + "', " +
                    "SurveyDate='" + project.SurveyDate + "', CustomerID='" + project.CustomerID + "', Customer='" + project.Customer + "', " +
                    "ProductQuality='" + project.ProductQuality + "', ProductQrice='" + project.ProductQrice + "', ProductDelivery='" + project.ProductDelivery + "'," +
                    " ProductSurvey='" + project.ProductSurvey + "', CustomerServiceSurvey='" + project.CustomerServiceSurvey + "', SupplySurvey='" + project.SupplySurvey + "'," +
                    " LeakSurvey='" + project.LeakSurvey + "', ServiceSurvey='" + project.ServiceSurvey + "', AgencySales='" + project.AgencySales + "', AgencyConsultation='" + project.AgencyConsultation + "'," +
                    " AgencySpareParts='" + project.AgencySpareParts + "', AgencySurvey='" + project.AgencySurvey + "', Remark='" + project.Remark + "', UserSign='" + project.UserSign + "', " +
                    "CreateTime='" + project.CreateTime + "', CreateUser='" + project.CreateUser + "', Validate='" + project.Validate + "'" +
                    " where DCID='" + project.DCID + " ' ";
                if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改满意度调查','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改满意度调查','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey','" + project.DCID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHSurvey_Product]
                string strUpdateListDel = "";
                int Amount = 0;
                decimal ResultTotal = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    string CheckPro = "select * from BGOI_CustomerService.dbo.tk_SHSurvey_Product where  PID='" + list[i].PID + "'";
                    DataTable dtCheckPro = SQLBase.FillTable(CheckPro, "MainCustomer");
                    if (dtCheckPro != null)
                    {
                        Amount += list[i].Num;
                        ResultTotal += list[i].Total;
                        strUpdateListDel = "update BGOI_CustomerService.dbo.tk_SHSurvey_Product set DCID='" + list[i].DCID + "', DID='" + list[i].DID + "'," +
                            " PID='" + list[i].PID + "', OrderDate='" + list[i].OrderDate + "', OrderContent='" + list[i].OrderContent + "'," +
                            " SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + list[i].Num + "', UnitPrice='" + list[i].UnitPrice + "'," +
                            " Total='" + list[i].Total + "'" +
                            " where DCID='" + list[i].DCID + "' and PID='" + list[i].PID + "'";
                        if (SQLBase.ExecuteNonQuery(strUpdateListDel) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改满意度调查','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product','" + list[i].DCID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改满意度调查','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product','" + list[i].DCID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    #region [冗余代码]
                    // else
                    //{
                    //    string CheckPronew = "Insert into BGOI_CustomerService.dbo.tk_SHRV_Product( HFID, DID, PID, OrderContent, SpecsModels, Unit, Num, Total, UnitPrice) " +
                    //      "values ('" + HFIDold + ",'" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].Total + "','" + list[i].UnitPrice + "')";
                    //    if (SQLBase.ExecuteNonQuery(CheckPronew) > 0)
                    //    {
                    //        #region [日志]
                    //        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    //            "values ('添加数据','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                    //        SQLBase.ExecuteNonQuery(strlog);
                    //        #endregion
                    //    }
                    //    else
                    //    {
                    //        #region [日志]
                    //        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    //            "values ('添加数据','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + project.HFID + "')";
                    //        SQLBase.ExecuteNonQuery(strlog);
                    //        #endregion
                    //    }
                    // }
                    #endregion
                }
                #endregion
                trans.Close(true);
                return true;
            }
            else
            {
                try
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHSurvey>(project, "BGOI_CustomerService.dbo.tk_SHSurvey");
                    string strInsertList = "";
                    if (list.Count > 0)
                    {
                        strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHSurvey_Product");
                    }
                    if (strInsert != "")
                    {
                        trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                    }
                    if (strInsertList != "")
                    {
                        if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加满意度调查','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product','" + project.DCID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加满意度调查','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHSurvey_Product','" + project.DCID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }

                    trans.Close(true);
                    return true;
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    trans.Close(true);
                    return false;
                }
            }


        }
        //撤销
        public static bool DeCustomerSatisfactionSurvey(string DCID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHSurvey set Validate='i' where DCID='" + DCID + "'";
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
        //打印顾客满意度
        public static DataTable UpSurveyList(string DCID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_SHSurvey where  HFIDID='" + DCID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static UIDataTable UserCustomerSatisfactionSurveyList(int a_intPageSize, int a_intPageIndex, string DCID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from  BGOI_CustomerService.dbo.tk_SHSurvey a" +
                                     " left join BGOI_CustomerService.dbo.tk_KClientBas b on a.CustomerID=b.KID where HFID='" + DCID + "'";

            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "DCID='" + DCID + "'";
            string strOrderBy = "a.DCID ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHSurvey a " +
                                      "  left join BGOI_BasMan.dbo.tk_KClientBas b on a.CustomerID=b.KID ";
            String strField = "a.ProductSurvey,a.Remark,a.ServiceSurvey,a.AgencySurvey,a.Customer,a.CreateUser,b.ComAddress,a.SurveyDate";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        #endregion

        #region [用户投诉]
        public static string GetTopTSID()
        {
            string strID = "";
            string strD = "TS-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(TSID) from BGOI_CustomerService.dbo.tk_SHComplain";
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
        //用户投诉列表 
        public static UIDataTable UserComplaintsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            string strSelCount = "select COUNT(*) " +
                                " from  BGOI_CustomerService.dbo.tk_SHComplain a" +
                                " left join BGOI_CustomerService.dbo.tk_SHComplain_User c on a.TSID=c.TSID " +
                                " left join BGOI_CustomerService.dbo.tk_SHComplain_Process d on a.TSID=d.TSID " +
                                " left join BGOI_CustomerService.dbo.tk_Approval e on a.TSID=e.RelevanceID " +

                                "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHComplain a " +
                              " left join BGOI_CustomerService.dbo.tk_SHComplain_User c on a.TSID=c.TSID " +
                              " left join BGOI_CustomerService.dbo.tk_SHComplain_Process d on a.TSID=d.TSID " +
                              " left join BGOI_CustomerService.dbo.tk_Approval e on a.TSID=e.RelevanceID ";
            String strField = " e.PID,a.Tel,a.Adderss,d.HandleUser,a.TSID,a.ComplaintDate,a.Customer,a.ComplaintTheme,a.ComplaintCategory,a.ComplainContent,a.EmergencyDegree,a.FirstDealUser,a.Remark,a.State,a.CreateUser,c.UserName,Convert(varchar(100),a.RecordDate,23) as RecordDate ";
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
                    #region [处理状态]
                    if (instData.DtData.Rows[r]["State"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["State"] = "未处理";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已处理";
                    }
                    #endregion
                }
            }

            return instData;
        }
        //TSIDDID
        public static string GetTopTSIDDID()
        {
            string strID = "";
            string strD = "TSID-U" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHComplain_User";
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

                    string stryyyyMMdd = strID.Substring(6, 8);
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
        //CLIDDID
        public static string GetTopCLIDDID()
        {
            string strID = "";
            string strD = "TSID-P" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHComplain_User";
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

                    string stryyyyMMdd = strID.Substring(6, 8);
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
        //ProductDID
        public static string GetTopProductDID()
        {
            string strID = "";
            string strD = "TSID-C" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHComplain_User";
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

                    string stryyyyMMdd = strID.Substring(6, 8);
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
        //添加修改
        public static bool SaveUserComplaints(tk_SHComplain Project, tk_SHComplain_User allruser, tk_SHComplain_Process allpro, List<tk_SHComplain_Product> list, string type, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            if (type == "1")
            {
                #region [修改]
                #region [插入历史表]

                #region [用户投诉]
                string oldshrv = "select * from BGOI_CustomerService.dbo.tk_SHComplain where  TSID='" + Project.TSID + "'";
                DataTable dt = SQLBase.FillTable(oldshrv, "MainCustomer");
                tk_SHComplain_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_SHComplain_HIS();
                    vishis.TSID = dr["TSID"].ToString();// Project.TSID;
                    vishis.UntiID = dr["UntiID"].ToString();// Project.UntiID;
                    vishis.CustomerID = dr["CustomerID"].ToString();//Project.CustomerID;
                    vishis.Customer = dr["Customer"].ToString();// Project.Customer;
                    vishis.ComplaintDate = dr["ComplaintDate"].ToString();// Project.ComplaintDate;
                    vishis.EmergencyDegree = dr["EmergencyDegree"].ToString();// Project.EmergencyDegree;
                    vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"]);//Project.CreateTime);
                    vishis.RecordDate = Convert.ToDateTime(dr["RecordDate"]);//Project.RecordDate);
                    vishis.ComplaintTheme = dr["ComplaintTheme"].ToString();//Project.ComplaintTheme;
                    vishis.ComplainContent = dr["ComplainContent"].ToString();// Project.ComplainContent;
                    vishis.State = dr["State"].ToString();//Project.State;
                    vishis.Remark = dr["Remark"].ToString();//Project.Remark;
                    vishis.CreateUser = dr["CreateUser"].ToString();// Project.CreateUser;
                    vishis.Validate = dr["Validate"].ToString();// Project.Validate;
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                }
                string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_SHComplain_HIS>(vishis, "BGOI_CustomerService.dbo.tk_SHComplain_HIS");
                if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_HIS','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_HIS','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [用户投诉人员]
                string usertshc = "select * from BGOI_CustomerService.dbo.tk_SHComplain_User where  TSID='" + Project.TSID + "'";
                DataTable dtproshc = SQLBase.FillTable(usertshc, "MainCustomer");
                tk_SHComplain_User_HIS vishispro = null;
                foreach (DataRow dr in dtproshc.Rows)
                {
                    vishispro = new tk_SHComplain_User_HIS();
                    vishispro.TSID = dr["TSID"].ToString();// allruser.TSID;
                    vishispro.DID = dr["DID"].ToString();// allruser.DID;
                    vishispro.UserID = dr["UserID"].ToString();// allruser.UserID;
                    vishispro.UserUnitID = dr["UserUnitID"].ToString();//allruser.UserUnitID;
                    vishispro.UserName = dr["UserName"].ToString();// allruser.UserName;
                    vishispro.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishispro.NCreateTime = DateTime.Now;
                }
                string strtkSHComplainUserHIS = GSqlSentence.GetInsertInfoByD<tk_SHComplain_User_HIS>(vishispro, "BGOI_CustomerService.dbo.tk_SHComplain_User_HIS");
                if (trans.ExecuteNonQuery(strtkSHComplainUserHIS, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉人员','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User_HIS','" + allruser.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉人员','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User_HIS','" + allruser.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [用户投诉处理]
                string Processshc = "select * from BGOI_CustomerService.dbo.tk_SHComplain_Process where  TSID='" + Project.TSID + "'";
                DataTable dtProcessshc = SQLBase.FillTable(Processshc, "MainCustomer");
                tk_SHComplain_Process_HIS vishisProcess = null;
                foreach (DataRow dr in dtProcessshc.Rows)
                {
                    vishisProcess = new tk_SHComplain_Process_HIS();
                    vishisProcess.TSID = dr["TSID"].ToString();// allpro.TSID;
                    vishisProcess.CLID = dr["CLID"].ToString();//allpro.CLID;
                    vishisProcess.HandleProcess = dr["HandleProcess"].ToString();// allpro.HandleProcess;
                    vishisProcess.HandleState = dr["HandleState"].ToString();// allpro.HandleState;
                    vishisProcess.HandleDate = Convert.ToDateTime(dr["HandleDate"]);// allpro.HandleDate;
                    vishisProcess.CostDate = dr["CostDate"].ToString();//allpro.CostDate;
                    vishisProcess.CustomerFeedback = dr["CustomerFeedback"].ToString();//allpro.CustomerFeedback;
                    vishisProcess.HandleUser = dr["HandleUser"].ToString();//allpro.HandleUser;
                    vishisProcess.CreateTime = Convert.ToDateTime(dr["CreateTime"]);//allpro.CreateTime;
                    vishisProcess.CreateUser = dr["CreateUser"].ToString();//allpro.CreateUser;
                    vishisProcess.Validate = dr["Validate"].ToString();//allpro.Validate;
                    vishisProcess.State = dr["State"].ToString();//
                    vishisProcess.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishisProcess.NCreateTime = DateTime.Now;
                }
                string strtkSHComplainProcessHIS = GSqlSentence.GetInsertInfoByD<tk_SHComplain_Process_HIS>(vishisProcess, "BGOI_CustomerService.dbo.tk_SHComplain_Process_HIS");
                if (trans.ExecuteNonQuery(strtkSHComplainProcessHIS, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉处理','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process_HIS','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉处理','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process_HIS','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [用户投诉产品]
                string oldshrvPro = "select * from BGOI_CustomerService.dbo.tk_SHComplain_Product where  TSID='" + Project.TSID + "'";
                DataTable dthis = SQLBase.FillTable(oldshrvPro, "MainCustomer");
                tk_SHComplain_Product_HIS visprohis = null;
                List<tk_SHComplain_Product_HIS> listvisprohis = new List<tk_SHComplain_Product_HIS>();
                for (int i = 0; i < dthis.Rows.Count; i++)
                {
                    visprohis = new tk_SHComplain_Product_HIS();
                    visprohis.TSID = dthis.Rows[i]["TSID"].ToString();
                    visprohis.DID = dthis.Rows[i]["DID"].ToString();
                    visprohis.PID = dthis.Rows[i]["PID"].ToString();
                    visprohis.OrderContent = dthis.Rows[i]["OrderContent"].ToString();
                    visprohis.Unit = dthis.Rows[i]["Unit"].ToString(); ;
                    visprohis.SpecsModels = dthis.Rows[i]["SpecsModels"].ToString();
                    if (string.IsNullOrEmpty(dthis.Rows[i]["Num"].ToString()))
                    {
                        visprohis.Num = 0;
                    }
                    else
                    {
                        visprohis.Num = Convert.ToInt32(dthis.Rows[i]["Num"].ToString());
                    }
                    visprohis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    visprohis.NCreateTime = DateTime.Now;
                    visprohis.OrderID = dthis.Rows[i]["OrderID"].ToString();

                    listvisprohis.Add(visprohis);
                }
                string strInsertListhis = GSqlSentence.GetInsertByList(listvisprohis, "BGOI_CustomerService.dbo.tk_SHComplain_Product_HIS");
                if (trans.ExecuteNonQuery(strInsertListhis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉产品','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product_HIS','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存用户投诉产品','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product_HIS','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion

                #endregion
                #region [修改tk_SHComplain]
                string strUpdateList = "update BGOI_CustomerService.dbo.tk_SHComplain set TSID='" + Project.TSID + "', UntiID='" + Project.UntiID + "', CustomerID='" + Project.CustomerID + "'," +
                                       " Customer='" + Project.Customer + "', RecordDate='" + Project.RecordDate + "', ComplaintDate='" + Project.ComplaintDate + "', EmergencyDegree='" + Project.EmergencyDegree + "'," +
                                       " ComplaintTheme='" + Project.ComplaintTheme + "', ComplaintCategory='" + Project.ComplaintCategory + "', FirstDealUser='" + Project.FirstDealUser + "'," +
                                       " ComplainContent='" + Project.ComplainContent + "', State='" + Project.State + "', Remark='" + Project.Remark + "', CreateTime='" + Project.CreateTime + "', CreateUser='" + Project.CreateUser + "', Validate='" + Project.Validate + "'" +
                                       " where TSID='" + Project.TSID + "'";

                if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHComplain_Product]
                string strUpdateListDel = "";
                for (int i = 0; i < list.Count; i++)
                {
                    string CheckPro = " select * from BGOI_CustomerService.dbo.tk_SHComplain_Product where  PID='" + list[i].PID + "' and TSID='" + list[i].TSID + "' ";

                    //string CheckPro = "select * from (select MAX(TSID) IDs from BGOI_CustomerService.dbo.tk_SHComplain_Product where PID='" + list[i].PID + "' and TSID='" + list[i].TSID + "' ) m where m.IDs is not null and m.IDs!=''";
                    DataTable dtCheckPro = SQLBase.FillTable(CheckPro, "MainCustomer");

                    if (dtCheckPro.Rows.Count <= 0)
                    {
                        strUpdateListDel += "   Insert into  BGOI_CustomerService.dbo.tk_SHComplain_Product (TSID, DID, PID, OrderContent, SpecsModels, Unit, Num,Validate) values ('" + list[i].TSID + "','" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','v') ";
                    }
                    else
                    {
                        strUpdateListDel += "  update BGOI_CustomerService.dbo.tk_SHComplain_Product set TSID='" + list[i].TSID + "', DID='" + list[i].DID + "'," + " PID='" + list[i].PID + "', OrderContent='" + list[i].OrderContent + "', SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + list[i].Num + "' and Validate='" + list[i].Validate + "' " + " where PID='" + list[i].PID + "' and TSID='" + list[i].TSID + "' ";
                    }

                }
                if (SQLBase.ExecuteNonQuery(strUpdateListDel) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉产品','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉产品','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product','" + Project.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHComplain_User]
                string strUserList = "update BGOI_CustomerService.dbo.tk_SHComplain_User set TSID='" + allruser.TSID + "', DID='" + allruser.DID + "', UserID='" + allruser.UserID + "', UserUnitID='" + allruser.UserUnitID + "', UserName='" + allruser.UserName + "'" +
                                     " where TSID='" + allruser.TSID + "'";
                if (SQLBase.ExecuteNonQuery(strUserList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉人员','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User','" + allruser.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉人员','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User','" + allruser.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #region [修改tk_SHComplain_Process]
                string strProcessList = "update BGOI_CustomerService.dbo.tk_SHComplain_Process set TSID='" + allpro.TSID + "', CLID='" + allpro.CLID + "', HandleProcess='" + allpro.HandleProcess + "'," +
                                        " HandleState='" + allpro.HandleState + "', HandleDate='" + allpro.HandleDate + "', CostDate='" + allpro.CostDate + "', CustomerFeedback='" + allpro.CustomerFeedback + "', " +
                                        " HandleUser='" + allpro.HandleUser + "', CreateTime='" + allpro.CreateTime + "', CreateUser='" + allpro.CreateUser + "', Validate='" + allpro.Validate + "'" +
                                        " State='" + allpro.State + "' " +
                                        " where TSID='" + allpro.TSID + "'";
                if (SQLBase.ExecuteNonQuery(strProcessList) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉处理','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉处理','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #endregion
                trans.Close(true);
                return true;
            }
            else
            {
                try
                {
                    string strSHComplain = GSqlSentence.GetInsertInfoByD<tk_SHComplain>(Project, "BGOI_CustomerService.dbo.tk_SHComplain");
                    string strSHComplain_User = GSqlSentence.GetInsertInfoByD<tk_SHComplain_User>(allruser, "BGOI_CustomerService.dbo.tk_SHComplain_User");
                    string strSHComplain_Process = GSqlSentence.GetInsertInfoByD<tk_SHComplain_Process>(allpro, "BGOI_CustomerService.dbo.tk_SHComplain_Process");
                    string strInsertList = "";
                    if (strSHComplain_Process != "")
                    {
                        if (trans.ExecuteNonQuery(strSHComplain_Process, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉处理','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉处理','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    if (strSHComplain_User != "")
                    {
                        if (trans.ExecuteNonQuery(strSHComplain_User, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉人员','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉人员','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_User','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    if (strSHComplain != "")
                    {
                        trans.ExecuteNonQuery(strSHComplain, CommandType.Text, null);
                    }

                    if (list.Count > 0)
                    {
                        strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHComplain_Product");
                    }
                    if (strInsertList != "")
                    {
                        if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉产品','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加用户投诉产品','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Product','" + Project.TSID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }

                    trans.Close(true);
                    return true;
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    trans.Close(true);
                    return false;
                }
            }
        }
        public static UIDataTable UserComplaintsDetialList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_SHComplain_Process a " +
                // " left join BJOI_UM.dbo.UM_UserNew b on a.HandleUser=b.UserId " +
                                 "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.TSID ";
            String strTable = "BGOI_CustomerService.dbo.tk_SHComplain_Process a";
            // " left join BJOI_UM.dbo.UM_UserNew b on a.HandleUser=b.UserId ";
            String strField = " TSID, CLID, HandleProcess, HandleState,Convert(varchar(100),HandleDate,23) as  HandleDate, CostDate, CustomerFeedback, HandleUser, CreateTime, CreateUser, Validate,State ";
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
                        instData.DtData.Rows[r]["State"] = "未提交审批";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "待审批";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["State"] = "已审批";
                    }

                }
            }

            return instData;
        }
        public static UIDataTable UserProComDetialList(int a_intPageSize, int a_intPageIndex, string TSID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_SHComplain a " +
               " left join BGOI_CustomerService.dbo.tk_SHComplain_Product b on a.TSID=b.TSID where b.Validate='v' and  a.TSID='" + TSID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "  d.Validate='v' and  a.TSID='" + TSID + "'";
            string strOrderBy = " a.TSID ";
            String strTable = "BGOI_CustomerService.dbo.tk_SHComplain a " +
                 " left join BGOI_CustomerService.dbo.tk_SHComplain_Product d on a.TSID=d.TSID ";
            String strField = " Convert(varchar(100),a.RecordDate,23) as RecordDate, a.ComplainContent, d.OrderContent, d.SpecsModels,d.OrderID, d.Unit, d.Num, a.State";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //撤销
        public static bool DeUserComplaints(string TSID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHComplain set Validate='i' where TSID='" + TSID + "'";
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
        //添加
        public static bool SaveProcessingRecord(tk_SHComplain_Process allpro, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            try
            {
                string sql = " select * from BGOI_CustomerService.dbo.tk_SHComplain_Process where TSID='" + allpro.TSID + "'";
                DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
                foreach (DataRow dr in dt.Rows)
                {
                    tk_SHComplain_Process_HIS prohis = new tk_SHComplain_Process_HIS();
                    prohis.TSID = dr["TSID"].ToString();
                    prohis.CLID = dr["CLID"].ToString();
                    prohis.HandleProcess = dr["HandleProcess"].ToString();
                    prohis.HandleState = dr["HandleState"].ToString();
                    prohis.HandleDate = Convert.ToDateTime(dr["HandleDate"]);
                    prohis.CostDate = dr["CostDate"].ToString();
                    prohis.CustomerFeedback = dr["CustomerFeedback"].ToString();
                    prohis.HandleUser = dr["HandleUser"].ToString();
                    prohis.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    prohis.CreateUser = dr["CreateUser"].ToString();
                    prohis.Validate = dr["Validate"].ToString();
                    prohis.NCreateTime = DateTime.Now;
                    prohis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    string strSHComplain_Process_HIS = GSqlSentence.GetInsertInfoByD<tk_SHComplain_Process_HIS>(prohis, "BGOI_CustomerService.dbo.tk_SHComplain_Process_HIS");

                    if (trans.ExecuteNonQuery(strSHComplain_Process_HIS, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('保留用户投诉处理','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process_HIS','" + allpro.TSID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('保留用户投诉处理','保留失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process_HIS','" + allpro.TSID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }

                string strUpdateListDelPro = "update BGOI_CustomerService.dbo.tk_SHComplain_Process set TSID='" + allpro.TSID + "', CLID='" + allpro.CLID + "'," +
                                             " HandleProcess='" + allpro.HandleProcess + "', HandleState='" + allpro.HandleState + "', HandleDate='" + allpro.HandleDate + "', " +
                                             " CostDate='" + allpro.CostDate + "', CustomerFeedback='" + allpro.CustomerFeedback + "', HandleUser='" + allpro.HandleUser + "'," +
                                             " CreateTime='" + allpro.CreateTime + "', CreateUser='" + allpro.CreateUser + "', Validate='" + allpro.Validate + "'" +
                                             " where TSID='" + allpro.TSID + "'";
                if (SQLBase.ExecuteNonQuery(strUpdateListDelPro) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉处理','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('修改用户投诉处理','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHComplain_Process','" + allpro.TSID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }

                trans.Close(true);
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static DataTable UpAddProcessingRecord(string TSID)
        {
            string sql = "  select TSID, CLID, HandleProcess, HandleState,Convert(varchar(100),HandleDate,23) as  HandleDate, Convert(varchar(100),CostDate,23) as CostDate, CustomerFeedback, HandleUser,Convert(varchar(100),CreateTime,23) as  CreateTime, CreateUser, Validate, State from BGOI_CustomerService.dbo.tk_SHComplain_Process  where TSID='" + TSID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //加载修改用户投诉表
        public static DataTable UpModifyUserComplaintsList(string TSID)
        {
            string sql = "select a.TSID, a.UntiID,a.Customer,Convert(varchar(100),a.RecordDate,23) as RecordDate,Convert(varchar(100),a.ComplaintDate,23) as ComplaintDate, e.UserName,a.CreateUser,a.Tel,a.Adderss, " +
                         " Convert(varchar(100),a.ComplaintTheme,23) as ComplaintTheme, a.ComplaintCategory, a.FirstDealUser, a.ComplainContent,a.Remark,Convert(varchar(100),a.CreateTime,23) as CreateTime,a.EmergencyDegree, " +
                         " c.UserID, c.UserUnitID, c.UserName," +
                         " d.CLID, d.HandleProcess, d.HandleState,Convert(varchar(100),d.HandleDate,23) as HandleDate,Convert(varchar(100),d.CostDate,23) as CostDate, d.CustomerFeedback, d.HandleUser " +
                         " from BGOI_CustomerService.dbo.tk_SHComplain a " +
                         " left join BGOI_CustomerService.dbo.tk_SHComplain_User c on a.TSID=c.TSID" +
                         " left join BGOI_CustomerService.dbo.tk_SHComplain_Process d on a.TSID=d.TSID" +
                         " left join BJOI_UM.dbo.UM_UserNew e on a.FirstDealUser=e.UserId" +
                         " where a.TSID='" + TSID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //加载修改用户投诉表(产品)
        public static DataTable UpModifyUserComplaintsListPro(string TSID)
        {
            string sql = "select " +
                         "  b.OrderID, b.PID, b.OrderContent, b.SpecsModels, b.Unit, b.Num" +
                         " from BGOI_CustomerService.dbo.tk_SHComplain a " +
                         " left join BGOI_CustomerService.dbo.tk_SHComplain_Product b on a.TSID=b.TSID" +
                         " where a.TSID='" + TSID + "' and b.Validate='v' ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //根据客户加载报修
        public static DataTable GetBX(string Customer)
        {
            string sql = "select top 5 * from  BGOI_CustomerService.dbo.tk_WXRequisit where Customer='" + Customer + "' order by CreateTime DESC";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //根据客户加载报装
        public static DataTable GetBZ(string Customer)
        {
            string sql = "select top 5 * from  BGOI_CustomerService.dbo.tk_SHInstallR where CustomerName='" + Customer + "'  order by CreateTime DESC";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static bool DeUserCom(string TSID, string PID)
        {
            string strErr = "";
            string sql = "update BGOI_CustomerService.dbo.tk_SHComplain_Product set Validate='i' where PID='" + PID + "' and TSID='" + TSID + "'";
            if (SQLBase.ExecuteNonQuery(sql, "MainCustomer") > 0)
            {
                return true;
            }
            else
            {
                strErr = "删除失败";
                //trans.Close(true);
                return false;
            }
        }
        #endregion
        #region [电话记录]
        //电话记录ID
        public static string GetTopDHID()
        {
            string strID = "";
            string strD = "DH-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(DHID) from BGOI_CustomerService.dbo.tk_TelRecord";
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
        //加载电话记录
        public static UIDataTable TelephoneAnsweringList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_TelRecord where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "CreateTime desc";
            String strTable = " BGOI_CustomerService.dbo.tk_TelRecord ";
            String strField = " DHID, Convert(varchar(100),AnswerDate,23) as AnswerDate, Address, UserName, Tel, SchoolWork, ProcessingResults, Remark, CreateTime, CreateUser, Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //保存电话记录
        public static bool SaveTelephoneAnswering(tk_TelRecord record, string type, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_TelRecord>(record, "BGOI_CustomerService.dbo.tk_TelRecord");
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
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    String strField = "select * from BGOI_CustomerService.dbo.tk_TelRecord where DHID='" + record.DHID + "'";
                    DataTable dt = SQLBase.FillTable(strField, "MainCustomer");
                    tk_TelRecord_HIS telhis = new tk_TelRecord_HIS();
                    foreach (DataRow dr in dt.Rows)
                    {
                        telhis.DHID = dr["DHID"].ToString();
                        telhis.AnswerDate = Convert.ToDateTime(dr["AnswerDate"]);
                        telhis.Address = dr["Address"].ToString();
                        telhis.UserName = dr["UserName"].ToString();
                        telhis.Tel = dr["Tel"].ToString();
                        telhis.SchoolWork = dr["SchoolWork"].ToString();
                        telhis.ProcessingResults = dr["ProcessingResults"].ToString();
                        telhis.Remark = dr["Remark"].ToString();
                        telhis.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                        telhis.CreateUser = dr["CreateUser"].ToString();
                        telhis.Validate = dr["Validate"].ToString();
                        telhis.NCreateUser = GAccount.GetAccountInfo().UserName;
                        telhis.NCreateTime = DateTime.Now;
                    }
                    string telrecordhis = GSqlSentence.GetInsertInfoByD<tk_TelRecord_HIS>(telhis, "BGOI_CustomerService.dbo.tk_TelRecord_HIS");
                    if (telrecordhis != "")
                    {
                        count = SQLBase.ExecuteNonQuery(telrecordhis);
                        if (count > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('保留电话记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_TelRecord_HIS','" + record.DHID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                            string telUpsql = "update BGOI_CustomerService.dbo.tk_TelRecord set DHID='" + record.DHID + "', AnswerDate='" + record.AnswerDate + "'," +
                                              " Address='" + record.Address + "', UserName='" + record.UserName + "', Tel='" + record.Tel + "'," +
                                              " SchoolWork='" + record.SchoolWork + "', ProcessingResults='" + record.ProcessingResults + "'," +
                                              " Remark='" + record.Remark + "', CreateTime='" + record.CreateTime + "', CreateUser='" + record.CreateUser + "', Validate='" + record.Validate + "'" +
                                              " where DHID='" + record.DHID + "'";
                            if (SQLBase.ExecuteNonQuery(telUpsql) > 0)
                            {
                                trans.Close(true);
                                return true;
                            }
                            else
                            {
                                trans.Close(true);
                                strErr = "修改失败";
                                return false;
                            }


                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('保留电话记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_TelRecord_HIS','" + record.DHID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                            trans.Close(true);
                            strErr = "修改留存失败";
                            return false;
                        }

                    }
                    else
                    {
                        trans.Close(true);
                        strErr = "输入有误！请重新输入";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        //导出
        public static DataTable TelephoneAnsweringToExcelFZ(string strWhere, ref string strErr)
        {
            String strField = "select convert(varchar(11),AnswerDate,120) as AnswerDate, Address, UserName, Tel, SchoolWork, ProcessingResults, Remark " +
                                " from BGOI_CustomerService.dbo.tk_TelRecord " +
                                " where " + strWhere;
            DataTable dt = SQLBase.FillTable(strField, "MainCustomer");
            return dt;

        }
        //加载修改数据
        public static DataTable UpTelephoneAnswering(string DHID)
        {
            string sql = "select  DHID, convert(varchar(11),AnswerDate,120) as AnswerDate, Address, UserName, Tel, SchoolWork, ProcessingResults, Remark, CreateTime, CreateUser, Validate from BGOI_CustomerService.dbo.tk_TelRecord where DHID='" + DHID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        //撤销
        public static bool DeTelephoneAnswering(string DHID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_TelRecord set Validate='i' where DHID='" + DHID + "'";
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
        #endregion
        #endregion
        #region [产品安装]

        #region [产品报装]
        public static string GetTopBZID()
        {
            string strID = "";
            string strD = "BZ-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(BZID) from BGOI_CustomerService.dbo.tk_SHInstallR";
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
        //DID
        public static string GetTopDID()
        {
            string strID = "";
            string strD = "BZID-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHInstallR_Product";
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
        //安装详细表DID
        public static string GetTopAZIDDID()
        {
            string strID = "";
            string strD = "AZID-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_SHInstall_Product";
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
        //添加产品报装
        public static bool AddProductReport(tk_SHInstallR project, List<tk_SHInstallR_Product> list, string type, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            try
            {
                string ListOutID = InventoryPro.GetTopListOutID();
                trans.Open("SalesDBCnn");
                if (type == "1")
                {
                    #region [修改留存]

                    #region [留存tk_SHInstallR_HIS]
                    string old = "select * from BGOI_CustomerService.dbo.tk_SHInstallR where  BZID='" + project.BZID + "'";
                    DataTable dt = SQLBase.FillTable(old, "MainCustomer");
                    tk_SHInstallR_HIS vishis = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        vishis = new tk_SHInstallR_HIS();
                        vishis.BZID = dr["BZID"].ToString();
                        vishis.UntiID = dr["UntiID"].ToString();
                        vishis.CustomerName = dr["CustomerName"].ToString();
                        vishis.InstallTime = Convert.ToDateTime(dr["InstallTime"].ToString());
                        vishis.Address = dr["Address"].ToString();
                        vishis.Tel = dr["Tel"].ToString();

                        vishis.Remark = dr["Remark"].ToString();
                        vishis.Sate = dr["Sate"].ToString();
                        vishis.WarehouseTwo = dr["WarehouseTwo"].ToString();
                        vishis.IsWhether = dr["IsWhether"].ToString();
                        vishis.WarehouseOne = dr["WarehouseOne"].ToString();

                        vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                        vishis.CreateUser = dr["CreateUser"].ToString();
                        vishis.Validate = dr["Validate"].ToString();
                        vishis.BZCompany = dr["BZCompany"].ToString();

                        vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                        vishis.NCreateTime = DateTime.Now;
                        vishis.DiPer = dr["DiPer"].ToString();
                    }
                    string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_SHInstallR_HIS>(vishis, "BGOI_CustomerService.dbo.tk_SHInstallR_HIS");
                    if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog0 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存产品报装','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_HIS','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog0);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog01 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存产品报装','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_HIS','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog01);
                        #endregion
                    }
                    #endregion
                    #region [留存tk_SHInstallR_Product_HIS]
                    string oldpro = "select * from BGOI_CustomerService.dbo.tk_SHInstallR_Product where  BZID='" + project.BZID + "'";
                    DataTable dtpro = SQLBase.FillTable(oldpro, "MainCustomer");
                    tk_SHInstallR_Product_HIS vishispro = null;
                    List<tk_SHInstallR_Product_HIS> listhis = new List<tk_SHInstallR_Product_HIS>();
                    foreach (DataRow drp in dtpro.Rows)
                    {
                        vishispro = new tk_SHInstallR_Product_HIS();
                        vishispro.BZID = drp["BZID"].ToString();
                        vishispro.DID = drp["DID"].ToString();
                        vishispro.PID = drp["PID"].ToString();
                        vishispro.OrderContent = drp["OrderContent"].ToString();
                        vishispro.SpecsModels = drp["SpecsModels"].ToString();

                        vishispro.Unit = drp["Unit"].ToString();
                        vishispro.Num = Convert.ToInt32(drp["Num"]);
                        vishispro.Price = drp["Price"].ToString();
                        vishispro.State = drp["State"].ToString();
                        vishispro.UnitPrice = drp["UnitPrice"].ToString();

                        vishispro.NCreateUser = GAccount.GetAccountInfo().UserName;
                        vishispro.NCreateTime = DateTime.Now;
                        listhis.Add(vishispro);
                    }
                    //string strSHReturnVis = GSqlSentence.GetInsertInfoByD<tk_SHInstallR_Product_HIS>(vishispro, "BGOI_CustomerService.dbo.tk_SHInstallR_Product_HIS");

                    string strSHReturnVis = GSqlSentence.GetInsertByList(listhis, "BGOI_CustomerService.dbo.tk_SHInstallR_Product_HIS");
                    if (trans.ExecuteNonQuery(strSHReturnVis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存产品报装产品详细','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product_HIS','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog1);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog2 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存产品报装产品详细','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product_HIS','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog2);
                        #endregion
                    }


                    #endregion

                    #endregion
                    #region [修改tk_SHInstallR]
                    string strInsert = " update BGOI_CustomerService.dbo.tk_SHInstallR set BZID='" + project.BZID + "', UntiID='" + project.UntiID + "', CustomerName='" + project.CustomerName + "', " +
                                      " InstallTime='" + project.InstallTime + "', Address='" + project.Address + "', Tel='" + project.Tel + "', Remark='" + project.Remark + "', Sate='" + project.Sate + "', WarehouseTwo='" + project.WarehouseTwo + "', IsWhether='" + project.IsWhether + "', " +
                                      " WarehouseOne='" + project.WarehouseOne + "', CreateTime='" + project.CreateTime + "', CreateUser='" + project.CreateUser + "', Validate='" + project.Validate + "',BZCompany='" + project.BZCompany + "' ,DiPer='" + project.DiPer + "' " +
                                      " where BZID='" + project.BZID + "'";
                    if (SQLBase.ExecuteNonQuery(strInsert) > 0)
                    {
                        #region [修改二级库主表]
                        string strSecondOutListnew = " update [BGOI_Inventory].[dbo].tk_StockOut set  " +
                                                     "OrderID='" + project.BZID + "', ProOutUser='" + project.CreateUser + "', State='" + project.Sate + "', HouseID='" + project.WarehouseTwo + "'," +
                                                     " CreateTime='" + project.CreateTime + "', ValiDate='" + project.Validate + "',Type='二级库出库'" +
                                                     " where OrderID='" + project.BZID + "'";
                        if (SQLBase.ExecuteNonQuery(strSecondOutListnew) > 0)
                        {
                            #region [插入二级库出库日志]
                            string strlogout = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('修改二级库出库','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOut','" + project.BZID + "')";
                            SQLBase.ExecuteNonQuery(strlogout);
                            #endregion
                        }
                        else
                        {
                            #region [添加插入二级库出库日志]
                            string strlogout1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent,  Person,Time, Type, Typeid) " +
                                "values ('修改二级库出库','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOut','" + project.BZID + "')";
                            SQLBase.ExecuteNonQuery(strlogout1);
                            #endregion
                            strErr = "修改二级库失败";
                            return false;

                        }
                        #endregion
                        #region [日志]
                        string strlog3 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品报装','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog3);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog4 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品报装','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog4);
                        #endregion
                        strErr = "修改失败";
                        return false;
                    }
                    #endregion
                    #region [修改tk_SHInstallR_Product]
                    for (int i = 0; i < list.Count; i++)
                    {

                        #region [for循环]
                        string sqldt = "select * from BGOI_CustomerService.dbo.tk_SHInstallR_Product where BZID='" + list[i].BZID + "' and PID='" + list[i].PID + "' ";
                        DataTable dthis = SQLBase.FillTable(sqldt, "MainCustomer");
                        if (dthis.Rows.Count > 0)
                        {
                            #region [if]
                            string strInsertPro = " update BGOI_CustomerService.dbo.tk_SHInstallR_Product set BZID='" + list[i].BZID + "', DID='" + list[i].DID + "', PID='" + list[i].PID + "', OrderContent='" + list[i].OrderContent + "'," +
                                            " SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + list[i].Num + "', Price='" + list[i].Price + "', State='" + list[i].State + "', UnitPrice='" + list[i].UnitPrice + "',SalesChannel='" + list[i].SalesChannel + "',IsPendingCollection='" + list[i].IsPendingCollection + "',Validate='" + list[i].Validate + "'" +
                                            " where BZID='" + list[i].BZID + "' and PID='" + list[i].PID + "' ";
                            if (SQLBase.ExecuteNonQuery(strInsertPro) > 0)
                            {
                                #region [修改二级库副表]
                                string strInsertProoutnew = " update [BGOI_Inventory].[dbo].tk_StockOutDetail set ProductID='" + list[i].PID + "',ProName='" + list[i].OrderContent + "',Spec='" + list[i].SpecsModels + "',Units='" + list[i].Unit + "'," +
                                                         " StockOutCount='" + list[i].Num + "',UnitPrice='" + list[i].UnitPrice + "',TotalAmount='" + list[i].Price + "'" +//,ListOutID='" + list[i].BZID + "'
                                                         " where OrderID='" + list[i].BZID + "' and ProductID='" + list[i].PID + "'";
                                if (SQLBase.ExecuteNonQuery(strInsertProoutnew) > 0)
                                {
                                    #region [插入二级库出库日志]
                                    string strlognew = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                        "values ('修改二级库出库','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + list[i].BZID + "')";
                                    SQLBase.ExecuteNonQuery(strlognew);
                                    #endregion
                                }
                                else
                                {
                                    #region [插入二级库出库日志]
                                    string strlognew1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                        "values ('修改二级库出库','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + list[i].BZID + "')";
                                    SQLBase.ExecuteNonQuery(strlognew1);
                                    #endregion
                                    strErr = "修改二级库失败";
                                    return false;
                                }
                                #endregion
                                #region [日志]
                                string strlog11 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改产品报装详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + list[i].BZID + "')";
                                SQLBase.ExecuteNonQuery(strlog11);
                                #endregion
                            }
                            else
                            {
                                #region [日志]
                                string strlog22 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改产品报装详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + list[i].BZID + "')";
                                SQLBase.ExecuteNonQuery(strlog22);
                                #endregion
                                strErr = "修改失败";
                                return false;
                            }
                            #endregion
                        }
                        else
                        {
                            #region [else]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_SHInstallR_Product]( BZID, DID, PID,OrderContent,  SpecsModels, Unit,Num,Price,State,UnitPrice,SalesChannel,IsPendingCollection,Validate) " +
                                        "values ('" + list[i].BZID + "','" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].Price + "','" + list[i].State + "','" + list[i].UnitPrice + "','" + list[i].SalesChannel + "','" + list[i].IsPendingCollection + "','" + list[i].Validate + "')";
                            #region [二级库副表]
                            if (SQLBase.ExecuteNonQuery(strlog) > 0)
                            {
                                #region [日志]
                                string strlog11 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改产品报装详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + list[i].BZID + "')";
                                SQLBase.ExecuteNonQuery(strlog11);
                                #endregion
                                #region [if]
                                string strInsertListSecondOutnew = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,ListOutID,OrderID) " +
                          "values ('" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].UnitPrice + "','" + list[i].Price + "','" + InventoryPro.GetTopListOutID() + "','" + list[i].BZID + "')";
                                if (SQLBase.ExecuteNonQuery(strInsertListSecondOutnew) > 0)
                                {
                                    #region [插入二级库出库日志]
                                    string strlognew = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                        "values ('添加二级库出库','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + project.BZID + "')";
                                    SQLBase.ExecuteNonQuery(strlognew);
                                    #endregion
                                }
                                else
                                {
                                    #region [插入二级库出库日志]
                                    string strlognew = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                        "values ('添加二级库出库','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + project.BZID + "')";
                                    SQLBase.ExecuteNonQuery(strlognew);
                                    #endregion
                                    strErr = "插入二级库失败";
                                    return false;
                                }
                                #endregion
                            }
                            else
                            {
                                #region [日志]
                                string strlog11 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改产品报装详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + list[i].BZID + "')";
                                SQLBase.ExecuteNonQuery(strlog11);
                                #endregion
                                #region [插入二级库出库日志]
                                string strlognew1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改二级库出库','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + list[i].BZID + "')";
                                SQLBase.ExecuteNonQuery(strlognew1);
                                #endregion
                                strErr = "修改二级库失败";
                                return false;
                            }
                            #endregion

                            #endregion

                        }
                        #endregion
                    }
                    #endregion
                    trans.Close(true);
                    return true;
                }
                else
                {
                    #region [添加]
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHInstallR>(project, "BGOI_CustomerService.dbo.tk_SHInstallR");
                    string strInsertList = "";
                    #region [插入二级库]
                    string strSecondOutList = "Insert into [BGOI_Inventory].[dbo].tk_StockOut(ListOutID,OrderID,CreateTime, ProOutUser, State, HouseID,  ValiDate, Type) " +
                           "values ('" + ListOutID + "','" + project.BZID + "','" + project.CreateTime + "','" + project.CreateUser + "','" + project.Sate + "','" + project.WarehouseTwo + "','" + project.Validate + "','二级库出库')";

                    int countnew = SQLBase.ExecuteNonQuery(strSecondOutList);

                    if (countnew > 0)
                    {
                        #region [插入二级库出库日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加二级库出库','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOut','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        foreach (tk_SHInstallR_Product SID in list)
                        {
                            string strInsertListSecondOut = "Insert into [BGOI_Inventory].[dbo].tk_StockOutDetail(ProductID,ProName,Spec,Units,StockOutCount,UnitPrice,TotalAmount,ListOutID,OrderID) " +
                                "values ('" + SID.PID + "','" + SID.OrderContent + "','" + SID.SpecsModels + "','" + SID.Unit + "','" + SID.Num + "','" + SID.UnitPrice + "','" + SID.Price + "','" + ListOutID + "','" + SID.BZID + "')";
                            if (SQLBase.ExecuteNonQuery(strInsertListSecondOut) > 0)
                            {
                                #region [插入二级库出库日志]
                                string strlognew = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('添加二级库出库','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + project.BZID + "')";
                                SQLBase.ExecuteNonQuery(strlognew);
                                #endregion
                            }
                            else
                            {
                                #region [插入二级库出库日志]
                                string strlognew = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('添加二级库出库','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOutDetail','" + project.BZID + "')";
                                SQLBase.ExecuteNonQuery(strlognew);
                                #endregion
                                strErr = "插入二级库失败";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        #region [添加插入二级库出库日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent,  Person,Time, Type, Typeid) " +
                            "values ('添加二级库出库','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_StockOut','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        strErr = "插入二级库失败";
                        return false;
                    }
                    #endregion
                    if (list.Count > 0)
                    {
                        strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHInstallR_Product");
                    }
                    if (strInsertList != "")
                    {
                        if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加产品报装记录详细','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + project.BZID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('添加产品报装记录详细','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR_Product','" + project.BZID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    if (strInsert != "")
                    {
                        trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                    }


                    trans.Close(true);
                    return true;
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
        public static UIDataTable ProductReportList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                 " from  BGOI_CustomerService.dbo.tk_SHInstallR a" +
                // " left join (select distinct BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product) b on a.BZID=b.BZID   " +
                                  " left join BGOI_Inventory.dbo.tk_WareHouse c on a.WarehouseTwo=c.HouseID  " +
                               " left join BGOI_CustomerService.dbo.tk_Customerser_Config d on a.BZCompany=d.ID " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse e on  a.WarehouseOne=e.HouseID  " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "a.BZID desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstallR a " +
                //" left join (select distinct BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product) b on a.BZID=b.BZID   " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse c on a.WarehouseTwo=c.HouseID  " +
                               " left join BGOI_CustomerService.dbo.tk_Customerser_Config d on a.BZCompany=d.ID " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse e on  a.WarehouseOne=e.HouseID  ";
            String strField = " c.HouseName as 'ckejk',(case   when e.HouseName !='' then e.HouseName else'否' end) as 'yjdbk',    a.BZID,a.Remark,a.CreateUser,Convert(varchar(100),a.InstallTime,23)as   InstallTime,a.CustomerName as CustomerName,Convert(varchar(12),a.CreateTime,111) as CreateTime,a.Tel,d.Text,  a.Address,a.WarehouseTwo,convert(varchar(20),a.Sate,120) State ";
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
                        instData.DtData.Rows[r]["State"] = "未报装";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已报装";
                    }

                }
            }

            return instData;
        }
        //撤销
        public static bool DeProductReport(string BZID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHInstallR set Validate='i' where BZID='" + BZID + "'";
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
        public static DataTable UPProductReportList(string BZID)
        {
            string sql = "select  a.BZID as BZID,a.BZCompany as BZCompany, a.DiPer,  a.CustomerName as CustomerName, Convert(varchar(100),a.InstallTime,23) as InstallTime, a.Address as Address, a.Tel as Tel, a.Remark as Remark,  a.WarehouseTwo as WarehouseTwo, a.IsWhether as IsWhether, a.WarehouseOne as WarehouseOne, b.PID as PID, b.OrderContent as OrderContent, b.SpecsModels as SpecsModels, b.Unit as Unit, b.Num as Num, b.Price as Price, b.UnitPrice  as UnitPrice,b.SalesChannel,b.IsPendingCollection from BGOI_CustomerService.dbo.tk_SHInstallR a ,BGOI_CustomerService.dbo.tk_SHInstallR_Product b where a.BZID='" + BZID + "' and b.BZID='" + BZID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }

        public static DataTable UPProductReportListpro(string BZID)
        {
            string sql = "select   b.PID as PID, b.OrderContent as OrderContent, b.SpecsModels as SpecsModels, b.Unit as Unit, b.Num as Num, b.Price as Price, b.UnitPrice  as UnitPrice,b.SalesChannel,b.IsPendingCollection from BGOI_CustomerService.dbo.tk_SHInstallR a ,BGOI_CustomerService.dbo.tk_SHInstallR_Product b where a.BZID='" + BZID + "' and b.BZID='" + BZID + "' and b.Validate='v'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static string GetTopAZID()
        {
            string strID = "";
            string strD = "AZ-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(AZID) from BGOI_CustomerService.dbo.tk_SHInstall";
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
        //根据BZID得到DID
        public static string GetProductReportDID(string BZID)
        {
            string bzidDID = "";
            string strSqlID = " select DID from BGOI_CustomerService.dbo.tk_SHInstallR_Product where BZID='" + BZID + "'";
            DataTable dtID = SQLBase.FillTable(strSqlID);
            foreach (DataRow dt in dtID.Rows)
            {
                bzidDID = dt["DID"].ToString();
            }
            return bzidDID;
        }
        //产品报装详细
        public static UIDataTable ProductReportDetialList(int a_intPageSize, int a_intPageIndex, string BZID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_SHInstallR_Product where Validate='v' and  BZID='" + BZID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " Validate='v' and BZID='" + BZID + "'";
            string strOrderBy = " BZID ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstallR_Product  ";
            String strField = " BZID, DID, PID, OrderContent, SpecsModels, Unit, Num, Price, State, UnitPrice,SalesChannel,IsPendingCollection ";

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
                    if (instData.DtData.Rows[r]["IsPendingCollection"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsPendingCollection"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsPendingCollection"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsPendingCollection"] = "否";
                    }
                }
            }

            return instData;
        }
        //添加安装记录
        public static bool AddSHInstall(tk_SHInstall project, List<tk_SHInstall_Product> list, ref string strErr)
        {
            #region [修改产品报装状态]
            string strInsertnew = "update BGOI_CustomerService.dbo.tk_SHInstallR set Sate=1 where BZID='" + project.BZID + "'";
            if (SQLBase.ExecuteNonQuery(strInsertnew) > 0)
            {
                #region [日志]
                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('修改状态','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR','" + project.BZID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            else
            {
                #region [日志]
                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('修改状态','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstallR','" + project.BZID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            #endregion
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHInstall>(project, "BGOI_CustomerService.dbo.tk_SHInstall");
                string strInsertList = "";
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHInstall_Product");
                }
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (strInsertList != "")
                {
                    if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加产品安装记录详细','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加产品安装记录详细','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }

                trans.Close(true);
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        //根据产品加载保修卡
        public static DataTable GetPIDBasicDetail(string PID)
        {
            string sql = "select top 5* from BGOI_CustomerService.dbo.tk_WXGuaranteeCard where PID='" + PID + "' order by CreateTime DESC ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }

        public static bool DeProduct(string BZID, string PID)
        {
            string strErr = "";
            string sql = "update BGOI_CustomerService.dbo.tk_SHInstallR_Product set Validate='i' where PID='" + PID + "' and BZID='" + BZID + "'";
            if (SQLBase.ExecuteNonQuery(sql, "MainCustomer") > 0)
            {
                return true;
            }
            else
            {
                strErr = "删除失败";
                //trans.Close(true);
                return false;
            }
        }
        #endregion
        #region [产品安装记录列表]
        public static UIDataTable ProductInstallationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            //string strSelCount = "select COUNT(*) " +
            //                       " from  BGOI_CustomerService.dbo.tk_SHInstall a" +
            //                         " left join BGOI_CustomerService.dbo.tk_SHInstall_Product b on a.AZID=b.AZID " + where;

            string strSelCount = "select COUNT(*) " +
                                  " from  BGOI_CustomerService.dbo.tk_SHInstall  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            //string strOrderBy = "a.InstallTime ";
            //String strTable = " BGOI_CustomerService.dbo.tk_SHInstall a " +
            //                          "  left join BGOI_CustomerService.dbo.tk_SHInstall_Product b on a.AZID=b.AZID ";
            //String strField = "a.AZID,a.BZID,a.IsCharge,a.InstallTime,a.IsInvoice as IsInvoice,Convert(varchar(12),a.CreateTime,111) as CreateTime,a.ReceiptType,a.SureSatisfied,a.IsProContent,a.IsWearClothes,a.IsTeaching,a.IsGifts,a.IsClean,a.IsUserSign,a.Remark,a.CreateUser ";
            string strOrderBy = " CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstall ";
            String strField = "AZID,BZID,IsCharge, Convert(varchar(100),InstallTime,23) as InstallTime,InstallName,IsInvoice,Convert(varchar(12),CreateTime,111) as CreateTime,ReceiptType,SureSatisfied,IsProContent,IsWearClothes,IsTeaching,IsGifts,IsClean,IsUserSign,Remark,CreateUser ";
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
                    if (instData.DtData.Rows[r]["IsCharge"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsCharge"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsCharge"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsCharge"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsInvoice"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsInvoice"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsInvoice"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsInvoice"] = "否";
                    }

                    if (instData.DtData.Rows[r]["ReceiptType"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["ReceiptType"] = "发票";
                    }
                    if (instData.DtData.Rows[r]["ReceiptType"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["ReceiptType"] = "收据";
                    }

                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "不满意";
                    }

                    if (instData.DtData.Rows[r]["IsProContent"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsProContent"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsProContent"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsProContent"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsWearClothes"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsWearClothes"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsWearClothes"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsWearClothes"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsTeaching"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsTeaching"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsTeaching"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsTeaching"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsGifts"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsGifts"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsGifts"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsGifts"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsClean"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsClean"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsClean"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsClean"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsUserSign"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsUserSign"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsUserSign"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsUserSign"] = "否";
                    }
                }
            }

            return instData;
        }
        //撤销
        public static bool DeProductInstallation(string AZID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHInstall set Validate='i' where AZID='" + AZID + "'";
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
        //修改
        public static DataTable UpProductInstallation(string AZIDold)
        {
            string sql = "select a.AZID as AZID,a.AZCompany as AZCompany, Convert(varchar(100),a.InstallTime,23) as InstallTime,b.PID as PID, b.OrderContent as OrderContent,b.SpecsModels as SpecsModels, b.Unit as Unit,b.Num as Num,b.UnitPrice  as UnitPrice ,b.Total as Total,a.BZID as BZID, a.UntiID as UntiID, a.InstallTime as InstallTime, a.InstallName as InstallName, a.IsCharge as IsCharge, a.IsInvoice as IsInvoice, a.ReceiptType as ReceiptType, a.Remark as Remark, a.SureSatisfied as SureSatisfied, a.IsProContent as IsProContent, a.IsWearClothes as IsWearClothes, a.IsTeaching as IsTeaching,a.IsGifts as IsGifts, a.IsClean as IsClean, a.IsUserSign as IsUserSign, a.CreateTime as CreateTime, a.CreateUser as CreateUser, a.Validate as Validate  from BGOI_CustomerService.dbo.tk_SHInstall a ,BGOI_CustomerService.dbo.tk_SHInstall_Product  b where a.AZID='" + AZIDold + "' and b.AZID='" + AZIDold + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //修改
        public static DataTable UpProductInstallationpro(string AZIDold)
        {
            string sql = "select b.PID as PID, b.OrderContent as OrderContent,b.SpecsModels as SpecsModels, b.Unit as Unit,b.Num as Num,b.UnitPrice  as UnitPrice ,b.Total as Total from BGOI_CustomerService.dbo.tk_SHInstall a ,BGOI_CustomerService.dbo.tk_SHInstall_Product  b where b.Validate='v' and a.AZID='" + AZIDold + "' and b.AZID='" + AZIDold + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //修改安装记录
        public static bool SaveProductInstallation(tk_SHInstall project, List<tk_SHInstall_Product> list, string AZIDold, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            #region [保留安装记录]
            #region [保留tk_SHInstall到tk_SHInstall_HIS]
            string strInsertnew = "select * from  BGOI_CustomerService.dbo.tk_SHInstall  where AZID='" + AZIDold + "' and BZID='" + project.BZID + "'";
            DataTable dtnew = SQLBase.FillTable(strInsertnew);
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                foreach (DataRow dr in dtnew.Rows)
                {
                    string strallhis = "Insert into BGOI_CustomerService.dbo.tk_SHInstall_HIS " +
                        " (AZID, BZID, UntiID, InstallTime, InstallName, IsCharge, IsInvoice," +
                        " ReceiptType, Remark, SureSatisfied, IsProContent, IsWearClothes, " +
                        " IsTeaching, IsGifts, IsClean, IsUserSign, CreateTime, CreateUser, Validate,NCreateTime, NCreateUser,AZCompany) " +
                        " values ('" + dr["AZID"] + "','" + dr["BZID"] + "','" + dr["UntiID"] + "','" + dr["InstallTime"] + "','" + dr["InstallName"] + "','" + dr["IsCharge"] + "'" +
                        " ,'" + dr["IsInvoice"] + "','" + dr["ReceiptType"] + "','" + dr["Remark"] + "','" + dr["SureSatisfied"] + "'," +
                        " '" + dr["IsProContent"] + "','" + dr["IsWearClothes"] + "','" + dr["IsTeaching"] + "','" + dr["IsGifts"] + "'," +
                        " '" + dr["IsClean"] + "','" + dr["IsUserSign"] + "','" + dr["CreateTime"] + "','" + dr["CreateUser"] + "'," +
                        " '" + dr["Validate"] + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','" + dr["AZCompany"] + "')";
                    if (SQLBase.ExecuteNonQuery(strallhis) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装记录','保留失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
            }
            #endregion

            #region [保留tk_SHInstall_Product到tk_SHInstall_Product_HIS]
            string strInsertnewpro = "select * from  BGOI_CustomerService.dbo.tk_SHInstall_Product  where AZID='" + AZIDold + "'";
            DataTable dtnewpro = SQLBase.FillTable(strInsertnewpro);
            if (dtnewpro != null && dtnewpro.Rows.Count > 0)
            {
                foreach (DataRow dr in dtnewpro.Rows)
                {
                    string strallhispro = "Insert into BGOI_CustomerService.dbo.tk_SHInstall_Product_HIS " +
                        " (AZID, BZDID, DID, PID, OrderContent, " +
                        " SpecsModels, Unit, Num, UnitPrice, " +
                        " Total, NCreateTime, NCreateUser) " +
                        " values ('" + dr["AZID"] + "','" + dr["BZDID"] + "','" + dr["DID"] + "','" + dr["PID"] + "','" + dr["OrderContent"] + "','" + dr["SpecsModels"] + "'" +
                        " ,'" + dr["Unit"] + "','" + dr["Num"] + "','" + dr["UnitPrice"] + "','" + dr["Total"] + "'," +
                        " '" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "')";
                    if (SQLBase.ExecuteNonQuery(strallhispro) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装成品详细记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装成品详细记录','保留失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
            }
            #endregion
            #endregion

            trans.Open("SalesDBCnn");
            try
            {

                string strInsert = "update BGOI_CustomerService.dbo.tk_SHInstall set " +
                                " AZID='" + project.AZID + "', BZID='" + project.BZID + "', UntiID='" + project.UntiID + "', InstallTime='" + project.InstallTime + "', InstallName='" + project.InstallName + "', IsCharge='" + project.strIsCharge + "', " +
                                " IsInvoice='" + project.IsInvoice + "', ReceiptType='" + project.ReceiptType + "', Remark='" + project.Remark + "', SureSatisfied='" + project.SureSatisfied + "', IsProContent='" + project.IsProContent + "'," +
                                " IsWearClothes='" + project.IsWearClothes + "', IsTeaching='" + project.IsTeaching + "', IsGifts='" + project.IsGifts + "', IsClean='" + project.IsClean + "', IsUserSign='" + project.IsUserSign + "', CreateTime='" + project.CreateTime + "', CreateUser='" + project.CreateUser + "', Validate='" + project.Validate + "',AZCompany='" + project.AZCompany + "' " +
                                "  where BZID='" + project.BZID + "' and  AZID='" + AZIDold + "'";

                // string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHInstall>(project, "BGOI_CustomerService.dbo.tk_SHInstall");
                string strInsertList = "";
                if (strInsert != "")
                {
                    // trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                    if (SQLBase.ExecuteNonQuery(strInsert) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        if (list.Count > 0)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                string sqldt = "select * from BGOI_CustomerService.dbo.tk_SHInstall_Product where AZID='" + list[i].AZID + "' and PID='" + list[i].PID + "' ";
                                DataTable dthis = trans.FillTable(sqldt);// FillTable(sqldt, "MainCustomer");
                                if (dthis.Rows.Count > 0 && dthis != null)
                                {
                                    strInsertList = "update BGOI_CustomerService.dbo.tk_SHInstall_Product set " +
                                  " AZID='" + list[i].AZID + "', BZDID='" + list[i].BZDID + "', DID='" + list[i].DID + "', PID='" + list[i].PID + "', OrderContent='" + list[i].OrderContent + "', SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + list[i].Num + "', UnitPrice='" + list[i].UnitPrice + "'," +
                                  " Total='" + list[i].Total + "',Validate='" + list[i].Validate + "' where AZID='" + AZIDold + "' and PID='" + list[i].PID + "'";
                                    if (strInsertList != "")
                                    {
                                        if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                                        {
                                            #region [日志]
                                            string strlog1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog1);
                                            #endregion
                                        }
                                        else
                                        {
                                            #region [日志]
                                            string strlog2 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog2);
                                            #endregion
                                        }
                                    }
                                }
                                else
                                {
                                    string tjstr = " Insert into BGOI_CustomerService.dbo.tk_SHInstall_Product (AZID, BZDID, DID, PID, OrderContent, SpecsModels, Unit, Num, UnitPrice, Total,Validate) values('" + list[i].AZID + "','" + list[i].BZDID + "','" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].UnitPrice + "','" + list[i].Total + "','" + list[i].Validate + "')";
                                    if (tjstr != "")
                                    {
                                        if (SQLBase.ExecuteNonQuery(tjstr) > 0)
                                        {
                                            #region [日志]
                                            string strlog3 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog3);
                                            #endregion
                                        }
                                        else
                                        {
                                            #region [日志]
                                            string strlog4 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog4);
                                            #endregion
                                        }
                                    }
                                }
                            }
                            //strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHInstall_Product");
                        }
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
                trans.Close(true);
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }

        //产品安装详细
        public static UIDataTable ProductInstallationDetialList(int a_intPageSize, int a_intPageIndex, string AZID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_SHInstall_Product where Validate='v' and AZID='" + AZID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " Validate='v' and AZID='" + AZID + "'";
            string strOrderBy = " AZID ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstall_Product  ";
            String strField = " AZID, BZDID, DID, PID, OrderContent, SpecsModels, Unit, Num, UnitPrice, Total ";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);


            return instData;
        }

        public static bool DeUserInst(string AZID, string PID)
        {
            string strErr = "";
            string sql = "update BGOI_CustomerService.dbo.tk_SHInstall_Product set Validate='i' where PID='" + PID + "' and AZID='" + AZID + "'";
            if (SQLBase.ExecuteNonQuery(sql, "MainCustomer") > 0)
            {
                return true;
            }
            else
            {
                strErr = "删除失败";
                //trans.Close(true);
                return false;
            }
        }
        #endregion

        #region [家用产品售后]
        //添加报警数据
        public static bool AddCSAlarm(CSAlarm csal)
        {
            int count = 0;
            string strInsert = GSqlSentence.GetInsertInfoByD(csal, "  BGOI_CustomerService.dbo.CSAlarm");
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
        //添加安装记录
        public static bool SaveHomeSalesInstallation(tk_SHInstall project, List<tk_SHInstall_Product> list, ref string strErr)
        {
            #region [修改订单表售后状态]
            string strInsertnew = "update [BGOI_Sales].[dbo].OrdersInfo set AfterSaleState='1' where  AfterSaleState='0' and OrderID='" + project.OrderID + "'";
            if (SQLBase.ExecuteNonQuery(strInsertnew) > 0)
            {
                #region [日志]
                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('修改状态','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','OrdersInfo','" + project.OrderID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            else
            {
                #region [日志]
                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                    "values ('修改状态','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','OrdersInfo','" + project.OrderID + "')";
                SQLBase.ExecuteNonQuery(strlog);
                #endregion
            }
            #endregion
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_SHInstall>(project, "BGOI_CustomerService.dbo.tk_SHInstall");
                string strInsertList = "";
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_SHInstall_Product");
                }
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (strInsertList != "")
                {
                    if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加产品安装记录详细','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加产品安装记录详细','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.BZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }

                trans.Close(true);
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static UIDataTable ChangeHomeSalesInstallationList(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b where  a.OrderID=b.OrderID and b.SalesType='Sa03'  and AfterSaleState='0'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "  a.OrderID=b.OrderID and b.SalesType='Sa03' and b.AfterSaleState='0'";
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
        public static DataTable GetHomeSalesInstallation(string DID)
        {
            string sql = "select a.OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,b.OrderContactor," +
                         " a.OrderUnit,OrderNum,Price,Subtotal,a.DeliveryTime,b.Remark " +
                         " from [BGOI_Sales].[dbo].[Orders_DetailInfo] a,[BGOI_Sales].[dbo].OrdersInfo b " +
                         " where b.AfterSaleState='0'  and a.OrderID=b.OrderID and DID='" + DID + "' ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static UIDataTable HomeSalesInstallationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                  " from  BGOI_CustomerService.dbo.tk_SHInstall a,[BGOI_Sales].[dbo].OrdersInfo b ,BGOI_CustomerService.dbo.Infotall c  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstall a,[BGOI_Sales].[dbo].OrdersInfo b ,BGOI_CustomerService.dbo.Infotall c  ";
            String strField = " c.Text as AfterSaleState , a.OrderID,AZID,BZID,IsCharge, Convert(varchar(100),InstallTime,23) as InstallTime,InstallName,IsInvoice,Convert(varchar(12),a.CreateTime,111) as CreateTime,ReceiptType,SureSatisfied,IsProContent,IsWearClothes,IsTeaching,IsGifts,IsClean,IsUserSign,a.Remark,a.CreateUser ";
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
                    if (instData.DtData.Rows[r]["IsCharge"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsCharge"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsCharge"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsCharge"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsInvoice"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsInvoice"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsInvoice"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsInvoice"] = "否";
                    }

                    if (instData.DtData.Rows[r]["ReceiptType"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["ReceiptType"] = "发票";
                    }
                    if (instData.DtData.Rows[r]["ReceiptType"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["ReceiptType"] = "收据";
                    }

                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "非常满意";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "满意";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "一般";
                    }
                    if (instData.DtData.Rows[r]["SureSatisfied"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["SureSatisfied"] = "不满意";
                    }

                    if (instData.DtData.Rows[r]["IsProContent"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsProContent"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsProContent"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsProContent"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsWearClothes"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsWearClothes"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsWearClothes"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsWearClothes"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsTeaching"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsTeaching"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsTeaching"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsTeaching"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsGifts"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsGifts"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsGifts"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsGifts"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsClean"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsClean"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsClean"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsClean"] = "否";
                    }

                    if (instData.DtData.Rows[r]["IsUserSign"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["IsUserSign"] = "是";
                    }
                    if (instData.DtData.Rows[r]["IsUserSign"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["IsUserSign"] = "否";
                    }
                }
            }
            return instData;
        }
        //撤销
        public static bool ButDE(string OrderID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update [BGOI_Sales].[dbo].OrdersInfo set AfterSaleState='0' where AfterSaleState='2' and OrderID='" + OrderID + "'";
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
        //安排安装
        public static bool ButAPAZ(string OrderID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update [BGOI_Sales].[dbo].OrdersInfo set AfterSaleState='2' where AfterSaleState='1' and OrderID='" + OrderID + "'";
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
                    strErr = "安排失败！";
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
        //回款
        public static bool ButHK(string OrderID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update [BGOI_Sales].[dbo].OrdersInfo set AfterSaleState='3' where LibraryTubeState='2' and AfterSaleState='2' and OrderID='" + OrderID + "'";
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
                    strErr = "回款失败！";
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
        //完成
        public static bool ButWCAZ(string OrderID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update [BGOI_Sales].[dbo].OrdersInfo set AfterSaleState='4',LibraryTubeState='4'  where LibraryTubeState='2' and AfterSaleState='3' and OrderID='" + OrderID + "'";
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
                    strErr = "回款失败！";
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
        //提示报警
        public static DataTable GetOrderidNew()
        {
            string unitid = GAccount.GetAccountInfo().UnitID;
            string sql = " select COUNT(*) as num  from [BGOI_Sales].dbo.Alarm a  " +
                        " left join [BGOI_Sales].[dbo].OrdersInfo b on a.OrderID=b.OrderID " +
                        " where  b.LibraryTubeState=0 and b.AfterSaleState=0";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        //修改加载产品信息
        public static DataTable UpHomeSalesInstallation(string AZIDold)
        {
            string sql = "select a.OrderID, b.PID as PID, b.OrderContent as OrderContent,b.SpecsModels as SpecsModels, b.Unit as Unit,b.Num as Num,b.UnitPrice  as UnitPrice ,b.Total as Total from BGOI_CustomerService.dbo.tk_SHInstall a ,BGOI_CustomerService.dbo.tk_SHInstall_Product  b where b.Validate='v' and a.AZID='" + AZIDold + "' and b.AZID='" + AZIDold + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }

        //修改安装记录
        public static bool SaveUpHomeSalesInstallation(tk_SHInstall project, List<tk_SHInstall_Product> list, string AZIDold, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            #region [保留安装记录]
            #region [保留tk_SHInstall到tk_SHInstall_HIS]
            string strInsertnew = "select * from  BGOI_CustomerService.dbo.tk_SHInstall  where AZID='" + AZIDold + "' and BZID='" + project.BZID + "'";
            DataTable dtnew = SQLBase.FillTable(strInsertnew);
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                foreach (DataRow dr in dtnew.Rows)
                {
                    string strallhis = "Insert into BGOI_CustomerService.dbo.tk_SHInstall_HIS " +
                        " (AZID, BZID, UntiID, InstallTime, InstallName, IsCharge, IsInvoice," +
                        " ReceiptType, Remark, SureSatisfied, IsProContent, IsWearClothes, " +
                        " IsTeaching, IsGifts, IsClean, IsUserSign, CreateTime, CreateUser, Validate,NCreateTime, NCreateUser,AZCompany) " +
                        " values ('" + dr["AZID"] + "','" + dr["BZID"] + "','" + dr["UntiID"] + "','" + dr["InstallTime"] + "','" + dr["InstallName"] + "','" + dr["IsCharge"] + "'" +
                        " ,'" + dr["IsInvoice"] + "','" + dr["ReceiptType"] + "','" + dr["Remark"] + "','" + dr["SureSatisfied"] + "'," +
                        " '" + dr["IsProContent"] + "','" + dr["IsWearClothes"] + "','" + dr["IsTeaching"] + "','" + dr["IsGifts"] + "'," +
                        " '" + dr["IsClean"] + "','" + dr["IsUserSign"] + "','" + dr["CreateTime"] + "','" + dr["CreateUser"] + "'," +
                        " '" + dr["Validate"] + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','" + dr["AZCompany"] + "')";
                    if (SQLBase.ExecuteNonQuery(strallhis) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装记录','保留失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
            }
            #endregion

            #region [保留tk_SHInstall_Product到tk_SHInstall_Product_HIS]
            string strInsertnewpro = "select * from  BGOI_CustomerService.dbo.tk_SHInstall_Product  where AZID='" + AZIDold + "'";
            DataTable dtnewpro = SQLBase.FillTable(strInsertnewpro);
            if (dtnewpro != null && dtnewpro.Rows.Count > 0)
            {
                foreach (DataRow dr in dtnewpro.Rows)
                {
                    string strallhispro = "Insert into BGOI_CustomerService.dbo.tk_SHInstall_Product_HIS " +
                        " (AZID, BZDID, DID, PID, OrderContent, " +
                        " SpecsModels, Unit, Num, UnitPrice, " +
                        " Total, NCreateTime, NCreateUser) " +
                        " values ('" + dr["AZID"] + "','" + dr["BZDID"] + "','" + dr["DID"] + "','" + dr["PID"] + "','" + dr["OrderContent"] + "','" + dr["SpecsModels"] + "'" +
                        " ,'" + dr["Unit"] + "','" + dr["Num"] + "','" + dr["UnitPrice"] + "','" + dr["Total"] + "'," +
                        " '" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "')";
                    if (SQLBase.ExecuteNonQuery(strallhispro) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装成品详细记录','保留成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存安装成品详细记录','保留失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product_HIS','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
            }
            #endregion
            #endregion

            trans.Open("SalesDBCnn");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_SHInstall set " +
                                " AZID='" + project.AZID + "', BZID='" + project.BZID + "', UntiID='" + project.UntiID + "', InstallTime='" + project.InstallTime + "', InstallName='" + project.InstallName + "', IsCharge='" + project.strIsCharge + "', " +
                                " IsInvoice='" + project.IsInvoice + "', ReceiptType='" + project.ReceiptType + "', Remark='" + project.Remark + "', SureSatisfied='" + project.SureSatisfied + "', IsProContent='" + project.IsProContent + "'," +
                                " IsWearClothes='" + project.IsWearClothes + "', OrderID='" + project.OrderID + "',IsTeaching='" + project.IsTeaching + "', IsGifts='" + project.IsGifts + "', IsClean='" + project.IsClean + "', IsUserSign='" + project.IsUserSign + "', CreateTime='" + project.CreateTime + "', CreateUser='" + project.CreateUser + "', Validate='" + project.Validate + "',AZCompany='" + project.AZCompany + "' " +
                                "  where BZID='" + project.BZID + "' and  AZID='" + AZIDold + "'";
                string strInsertList = "";
                if (strInsert != "")
                {
                    if (SQLBase.ExecuteNonQuery(strInsert) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        if (list.Count > 0)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                string sqldt = "select * from BGOI_CustomerService.dbo.tk_SHInstall_Product where AZID='" + list[i].AZID + "' and PID='" + list[i].PID + "' ";
                                DataTable dthis = trans.FillTable(sqldt);// FillTable(sqldt, "MainCustomer");
                                if (dthis.Rows.Count > 0 && dthis != null)
                                {
                                    strInsertList = "update BGOI_CustomerService.dbo.tk_SHInstall_Product set " +
                                  " AZID='" + list[i].AZID + "', BZDID='" + list[i].BZDID + "', DID='" + list[i].DID + "', PID='" + list[i].PID + "', OrderContent='" + list[i].OrderContent + "', SpecsModels='" + list[i].SpecsModels + "', Unit='" + list[i].Unit + "', Num='" + list[i].Num + "', UnitPrice='" + list[i].UnitPrice + "'," +
                                  " Total='" + list[i].Total + "',Validate='" + list[i].Validate + "' where AZID='" + AZIDold + "' and PID='" + list[i].PID + "'";
                                    if (strInsertList != "")
                                    {
                                        if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                                        {
                                            #region [日志]
                                            string strlog1 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog1);
                                            #endregion
                                        }
                                        else
                                        {
                                            #region [日志]
                                            string strlog2 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog2);
                                            #endregion
                                        }
                                    }
                                }
                                else
                                {
                                    string tjstr = " Insert into BGOI_CustomerService.dbo.tk_SHInstall_Product (AZID, BZDID, DID, PID, OrderContent, SpecsModels, Unit, Num, UnitPrice, Total,Validate) values('" + list[i].AZID + "','" + list[i].BZDID + "','" + list[i].DID + "','" + list[i].PID + "','" + list[i].OrderContent + "','" + list[i].SpecsModels + "','" + list[i].Unit + "','" + list[i].Num + "','" + list[i].UnitPrice + "','" + list[i].Total + "','" + list[i].Validate + "')";
                                    if (tjstr != "")
                                    {
                                        if (SQLBase.ExecuteNonQuery(tjstr) > 0)
                                        {
                                            #region [日志]
                                            string strlog3 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog3);
                                            #endregion
                                        }
                                        else
                                        {
                                            #region [日志]
                                            string strlog4 = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                                "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall_Product','" + project.AZID + "')";
                                            SQLBase.ExecuteNonQuery(strlog4);
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('修改产品安装记录详细','修改失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHInstall','" + project.AZID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
                trans.Close(true);
                return true;
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
        #region [维修任务]
        #region [维修任务]
        //用户投诉列表 
        public static UIDataTable MaintenanceTaskList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                 " from  BGOI_CustomerService.dbo.tk_WXRequisit a " +
                //" left join BGOI_CustomerService.dbo.tk_WXRecord b on a.BXID=b.BXID " +
                //" left join BGOI_CustomerService.dbo.tk_WXRecord_Product c on b.WXID=c.WXID " +
                                 "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime desc";
            String strTable = " BGOI_CustomerService.dbo.tk_WXRequisit a ";
            //" left join BGOI_CustomerService.dbo.tk_WXRecord b on a.BXID=b.BXID " +
            //" left join BGOI_CustomerService.dbo.tk_WXRecord_Product c on b.WXID=c.WXID " +
            //" left join BGOI_Inventory.dbo.tk_ProductInfo d on d.PID=c.Lname  and d.ProName=a.DeviceName ";
            String strField = " a.BXID,a.UntiID,a.RepairID,a.Customer,a.ContactName,a.Address,a.Tel,a.DeviceName,a.DeviceType, " +
                              " a.EnableDate,a.GuaranteePeriod,a.BXKNum,a.RepairTheUser, Convert(varchar(100),a.RepairDate,23) as RepairDate,a.Sate,a.Remark,a.CreateTime, " +
                              " a.CreateUser,a.Validate, Convert(varchar(100),a.CollectionTime,23) as CollectionTime";
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
                    #region [维修状态]
                    if (instData.DtData.Rows[r]["Sate"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["Sate"] = "未维修";
                    }
                    if (instData.DtData.Rows[r]["Sate"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["Sate"] = "已维修";
                    }
                    #endregion
                }
            }

            return instData;
        }
        public static string GetTopBXID()
        {
            string strID = "";
            string strD = "BX-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(BXID) from BGOI_CustomerService.dbo.tk_WXRequisit";
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
        //添加修改
        public static bool SaveMaintenanceTask(tk_WXRequisit Project, string type, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            if (type == "1")
            {
                #region [修改]
                #region [插入历史表]
                #region [维修报修]
                string oldshrv = " select * from BGOI_CustomerService.dbo.tk_WXRequisit where BXID='" + Project.BXID + "'";
                DataTable dt = SQLBase.FillTable(oldshrv, "MainCustomer");
                tk_WXRequisit_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_WXRequisit_HIS();
                    vishis.BXID = dr["BXID"].ToString();
                    vishis.UntiID = dr["UntiID"].ToString(); ;
                    vishis.RepairID = dr["RepairID"].ToString();
                    vishis.Customer = dr["Customer"].ToString();
                    vishis.ContactName = dr["ContactName"].ToString();
                    vishis.Address = dr["Address"].ToString();
                    vishis.Tel = dr["Tel"].ToString();
                    vishis.DeviceName = dr["DeviceName"].ToString();
                    vishis.DeviceType = dr["DeviceType"].ToString();
                    vishis.EnableDate = Convert.ToDateTime(dr["EnableDate"].ToString());
                    vishis.GuaranteePeriod = dr["GuaranteePeriod"].ToString();
                    vishis.BXKNum = dr["BXKNum"].ToString();
                    vishis.RepairTheUser = dr["RepairTheUser"].ToString();
                    vishis.RepairDate = Convert.ToDateTime(dr["RepairDate"].ToString());
                    vishis.Sate = dr["Sate"].ToString();
                    vishis.Remark = dr["Remark"].ToString();
                    vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                    vishis.CreateUser = dr["CreateUser"].ToString();
                    vishis.Validate = dr["Validate"].ToString();
                    vishis.DeviceID = dr["DeviceID"].ToString();
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                    // vishis.CollectionTime = Convert.ToDateTime(dr["CollectionTime"].ToString());
                }
                string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_WXRequisit_HIS>(vishis, "BGOI_CustomerService.dbo.tk_WXRequisit_HIS");
                if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存维修报修','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXRequisit_HIS','" + Project.BXID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存维修报修','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXRequisit_HIS','" + Project.BXID + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion
                #endregion
                #region [修改tk_WXRequisit]
                string strUpdateList = "update BGOI_CustomerService.dbo.tk_WXRequisit set BXID='" + Project.BXID + "', UntiID='" + Project.UntiID + "'," +
                                       " RepairID='" + Project.RepairID + "', Customer='" + Project.Customer + "', ContactName='" + Project.ContactName + "', Address='" + Project.Address + "', " +
                                       " Tel='" + Project.Tel + "', DeviceName='" + Project.DeviceName + "', DeviceType='" + Project.DeviceType + "', " +
                                       " EnableDate='" + Project.EnableDate + "', GuaranteePeriod='" + Project.GuaranteePeriod + "', BXKNum='" + Project.BXKNum + "'," +
                                       " RepairTheUser='" + Project.RepairTheUser + "', RepairDate='" + Project.RepairDate + "', Sate='" + Project.Sate + "', Remark='" + Project.Remark + "', " +
                                       " CreateTime='" + Project.CreateTime + "', CreateUser='" + Project.CreateUser + "', Validate='" + Project.Validate + "', DeviceID='" + Project.DeviceID + "'" +
                                       " where BXID='" + Project.BXID + "'";
                if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
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
                #endregion
            }
            else
            {
                try
                {
                    string strWXRequisit = GSqlSentence.GetInsertInfoByD<tk_WXRequisit>(Project, "BGOI_CustomerService.dbo.tk_WXRequisit");
                    if (strWXRequisit != "")
                    {
                        trans.ExecuteNonQuery(strWXRequisit, CommandType.Text, null);
                    }
                    trans.Close(true);
                    return true;
                }
                catch (Exception ex)
                {
                    strErr = ex.Message;
                    trans.Close(true);
                    return false;
                }
            }
        }
        public static DataTable GetSpec()
        {
            string str = "select ID,Text  FROM [BGOI_Inventory].[dbo].[tk_ProductSelect_Config]  where [Type]='SpecsModels'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            return dt;
        }
        public static UIDataTable UserMaintenanceTaskList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_WXRequisit " +
                                 "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " BXID ";
            String strTable = " BGOI_CustomerService.dbo.tk_WXRequisit ";
            String strField = " BXID, UntiID, RepairID, Customer, ContactName, Address, Tel, DeviceName, DeviceType,Convert(varchar(100),EnableDate,23) as  EnableDate, GuaranteePeriod, BXKNum, RepairTheUser,Convert(varchar(100),RepairDate,23) as RepairDate, Sate, Remark, CreateTime, CreateUser, Validate,DeviceID ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //撤销
        public static bool DeMaintenanceTask(string BXID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_WXRequisit set Validate='i' where BXID='" + BXID + "'";
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
        public static bool WXMaintenanceTask(string BXID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "select * from  BGOI_CustomerService.dbo.tk_WXRecord where BXID='" + BXID + "'";
                DataTable dt = SQLBase.FillTable(strInsert, "MainCustomer");
                trans.Close(true);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    strErr = "空";
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
        //完成维修
        public static bool CompleteMaintenanceTask(string BXID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_WXRequisit set Sate='1' where BXID='" + BXID + "'";
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
                    strErr = "完成维修失败！";
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
        public static string GetTopWXID()
        {
            string strID = "";
            string strD = "WX-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(WXID) from BGOI_CustomerService.dbo.tk_WXRecord";
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
        public static DataTable UpWXRecordList(string BXID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_WXRequisit where BXID='" + BXID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //添加维修记录
        public static bool SaveUpMainten(tk_WXRecord Project, List<tk_WXRecord_Product> list, ref string strErr)
        {
            string strInsertList = "";
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            try
            {
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.tk_WXRecord_Product");
                }
                if (strInsertList != "")
                {
                    if (trans.ExecuteNonQuery(strInsertList, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加退货详细货品','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXRecord_Product','" + Project.WXID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加退货详细货品','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXRecord_Product','" + Project.WXID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                }
                string strWXRequisit = GSqlSentence.GetInsertInfoByD<tk_WXRecord>(Project, "BGOI_CustomerService.dbo.tk_WXRecord");
                if (strWXRequisit != "")
                {
                    trans.ExecuteNonQuery(strWXRequisit, CommandType.Text, null);
                }
                trans.Close(true);
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }

        }
        //DID
        public static string GetTopWXIDDID()
        {
            string strID = "";
            string strD = "WXID-C" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = " select max(DID) from BGOI_CustomerService.dbo.tk_WXRecord_Product";
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

                    string stryyyyMMdd = strID.Substring(6, 8);
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
        //修改
        public static DataTable UpDateModifyTaskComplaintsa(string BXID)
        {
            string sql = " select * from BGOI_CustomerService.dbo.tk_WXRequisit where BXID='" + BXID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static UIDataTable UserMaintenanceTaskTwoList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_WXRecord " +
                                 "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " WXID ";
            String strTable = " BGOI_CustomerService.dbo.tk_WXRecord ";
            String strField = " WXID,Convert(varchar(100),CollectionTime,23) as CollectionTime, BXID,Convert(varchar(100),MaintenanceTime,23) as  MaintenanceTime, MaintenanceVehicle, MaintenanceName, MaintenanceRecord, FinalResults, ArtificialCost, MaterialCost, OtherCost, Total, PaymentMethod, Payee, Sate, SignatureName, Remark, CreateTime, CreateUser, Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        public static UIDataTable UserMaintenanceTaskThreeList(int a_intPageSize, int a_intPageIndex, string where, string BXID)
        {
            string WXID = "";
            string strBXID = " select * from BGOI_CustomerService.dbo.tk_WXRecord where BXID='" + BXID + "'";
            DataTable dt = SQLBase.FillTable(strBXID, "MainCustomer");
            foreach (DataRow dr in dt.Rows)
            {
                WXID = dr["WXID"].ToString();
            }
            where = " WXID='" + WXID + "'";
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.tk_WXRecord_Product " +
                                 "where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " WXID ";
            String strTable = " BGOI_CustomerService.dbo.tk_WXRecord_Product ";
            String strField = " WXID, DID, Lname, UnitPrice, Amount, Total ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //根据产品id加载产品信息
        public static DataTable GetPronewSpec(string PID)
        {
            string sql = "select * from BGOI_Inventory.dbo.tk_ProductInfo where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //根据用户加载报装编号
        public static DataTable GetUserBAO(string Tel)
        {
            string sql = "select BZID From BGOI_CustomerService.dbo.tk_SHInstallR where Tel='" + Tel + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        #endregion
        #region  [保修卡]

        public static UIDataTable WarrantyCardList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_WXGuaranteeCard  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "CreateTime  desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_WXGuaranteeCard ";
            String strField = "Convert(varchar(12),BXDate,111) as BXDate,Convert(varchar(12),BuyDate,111) as BuyDate,OrderContent,Contact,BXKNum,BXKID,Tel,Convert(varchar(12),CreateTime,111) as CreateTime,PID,SpecsModels,Remark,CreateUser,EndUnit,convert(varchar(20),Validate,120) Validate ";

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
                //        instData.DtData.Rows[r]["State"] = "未报废";
                //    }
                //    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                //    {
                //        instData.DtData.Rows[r]["State"] = "已报废";
                //    }

                //}
            }

            return instData;
        }
        public static string GetTopBXKID()
        {
            string strID = "";
            string strD = "BXK-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(BXKID) from BGOI_CustomerService.dbo.tk_WXGuaranteeCard";
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

                    string stryyyyMMdd = strID.Substring(4, 8);
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
        //保存保修卡
        public static bool SaveWarrantyCard(tk_WXGuaranteeCard record, ref string strErr)
        {

            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<tk_WXGuaranteeCard>(record, "BGOI_CustomerService.dbo.tk_WXGuaranteeCard");
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
        //撤销
        public static bool DeWarrantyCard(string BXKID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_WXGuaranteeCard set Validate='i' where BXKID='" + BXKID + "'";
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
        //修改
        public static bool UPdateWarrantyCard(tk_WXGuaranteeCard card, string BXKIDold, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                #region [插入历史表]
                string oldshrvcard = "select * from BGOI_CustomerService.dbo.tk_WXGuaranteeCard where  BXKID='" + BXKIDold + "'";
                DataTable dt = SQLBase.FillTable(oldshrvcard, "MainCustomer");
                tk_WXGuaranteeCard_HIS vishis = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vishis = new tk_WXGuaranteeCard_HIS();
                    vishis.BXKID = dr["BXKID"].ToString();
                    vishis.BUnitID = dr["BUnitID"].ToString();
                    vishis.ContractID = dr["ContractID"].ToString();
                    vishis.BXKNum = dr["BXKNum"].ToString();
                    vishis.PID = dr["PID"].ToString();
                    vishis.Contact = dr["Contact"].ToString();
                    vishis.Tel = dr["Tel"].ToString();
                    vishis.OrderContent = dr["OrderContent"].ToString();
                    vishis.SpecsModels = dr["SpecsModels"].ToString();
                    vishis.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                    vishis.BuyDate = Convert.ToDateTime(dr["BuyDate"].ToString());
                    vishis.BXDate = Convert.ToDateTime(dr["BXDate"].ToString());
                    vishis.Remark = dr["Remark"].ToString();
                    vishis.CreateUser = dr["CreateUser"].ToString();
                    vishis.EndUnit = dr["EndUnit"].ToString();
                    vishis.Validate = dr["Validate"].ToString();
                    vishis.NCreateUser = GAccount.GetAccountInfo().UserName;
                    vishis.NCreateTime = DateTime.Now;
                }
                string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_WXGuaranteeCard_HIS>(vishis, "BGOI_CustomerService.dbo.tk_WXGuaranteeCard_HIS");
                if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存回访登记','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXGuaranteeCard_HIS','" + BXKIDold + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                else
                {
                    #region [日志]
                    string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                        "values ('留存回访登记','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_WXGuaranteeCard_HIS','" + BXKIDold + "')";
                    SQLBase.ExecuteNonQuery(strlog);
                    #endregion
                }
                #endregion

                string strInsert = " update BGOI_CustomerService.dbo.tk_WXGuaranteeCard set " +
                                   " BXKID='" + card.BXKID + "', BUnitID='" + card.BUnitID + "', ContractID='" + card.ContractID + "', BXKNum='" + card.BXKNum + "', OrderContent='" + card.OrderContent + "', PID='" + card.PID + "', " +
                                   " SpecsModels='" + card.SpecsModels + "', BuyDate='" + card.BuyDate + "', BXDate='" + card.BXDate + "', EndUnit='" + card.EndUnit + "', Contact='" + card.Contact + "', Tel='" + card.Tel + "', Remark='" + card.Remark + "'," +
                                   " CreateTime='" + card.CreateTime + "', CreateUser='" + card.CreateUser + "', Validate='" + card.Validate + "'" +
                                   " where BXKID='" + BXKIDold + "'";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    trans.Close(true);
                    return true;
                }
                else
                {
                    strErr = "修改失败";
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
        public static DataTable UPdateWarrantyCardList(string BXKID)
        {
            string sql = "select BXKID,BUnitID,ContractID,BXKNum,OrderContent,PID,SpecsModels,Convert(varchar(100),BuyDate,23) as BuyDate,Convert(varchar(100),BXDate,23) as BXDate,EndUnit,Contact,Tel,Remark,Convert(varchar(100),CreateTime,23) as CreateTime,CreateUser,Validate,UserAdd from BGOI_CustomerService.dbo.tk_WXGuaranteeCard where BXKID='" + BXKID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        //导出
        public static DataTable GetWarrantyCardToExcel(string strWhere, ref string strErr)
        {

            String strField = "select BXKID, ContractID, BXKNum, OrderContent, PID, SpecsModels,Convert(varchar(100),BuyDate,23) as  BuyDate,Convert(varchar(100),BXDate,23) as  BXDate, EndUnit, Contact, Tel, Remark, Convert(varchar(100),CreateTime,23) as  CreateTime, CreateUser "
                + "from BGOI_CustomerService.dbo.tk_WXGuaranteeCard  where " + strWhere + "order by CreateTime";
            DataTable dt = SQLBase.FillTable(strField, "SalesDBCnn");
            return dt;

        }


        // 保存计划表单数据 
        //维修记录编号	保修卡所属单位	合同编号	保修卡编号	产品名称	产品编号	产品规格型号	购买日期  （2015-10-19 00:00:00.000）	
        //保修时间	最终用户单位	联系人	联系方式	备注	创建时间   （2015-10-19 00:00:00.000）	
        //登记人	初始状态0	客户地址  
        public static bool SaveWarrantyCardData(string strData, ref string strErr)
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
                        string strsql = " select * from BGOI_CustomerService.dbo.tk_WXGuaranteeCard where ltrim(BXKID)=" + strList1[0].ToString().Trim() + "";
                        DataTable dt = SQLBase.FillTable(strsql, "MainCustomer");
                        if (dt.Rows.Count == 0)
                        {
                            strSql += " insert into BGOI_CustomerService.dbo.tk_WXGuaranteeCard" +
                           "(BXKID, BUnitID, ContractID, BXKNum, OrderContent, PID, SpecsModels, BuyDate, BXDate, EndUnit, Contact, Tel, Remark, CreateTime, CreateUser, Validate, UserAdd)" +
                           " values(" + strList1[0] + "," + strList1[1] + "," + strList1[2] + "," + strList1[3] + "," + strList1[4] + "," + strList1[5] + "" +
                           " ," + strList1[6] + "," + strList1[7] + "," + strList1[8] + "," + strList1[9] + "," + strList1[10] + "," + strList1[11] + "," + strList1[12] + ",'" + DateTime.Now + "'," + strList1[13] + ",'v'," + strList1[14] + ")   ";

                        }
                        else
                        {
                            //continue;//过滤重复的
                            strSql += " update BGOI_CustomerService.dbo.tk_WXGuaranteeCard set BXKID=" + strList1[0] + ", BUnitID=" + strList1[1] + ", ContractID=" + strList1[2] + ", BXKNum=" + strList1[3] + ", OrderContent=" + strList1[4] + ", PID=" + strList1[5] + ", SpecsModels=" + strList1[6] + ", BuyDate=" + strList1[7] + ", BXDate=" + strList1[8] + ", EndUnit=" + strList1[9] + ", Contact=" + strList1[10] + ", Tel=" + strList1[11] + ", Remark=" + strList1[12] + ", CreateTime='" + DateTime.Now + "', CreateUser=" + strList1[13] + ", Validate='v', UserAdd=" + strList1[14] + " where BXKID=" + strList1[0] + "  ";
                        }
                    }
                    if (strSql != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strSql);
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
                        strErr = "上传已保存";
                        return false;
                    }

                }
                else
                {
                    strErr = "数据上传失败，请重新上传";
                    return false;
                }

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                throw;
            }
        }

        #endregion
        #region [调压巡检]
        //保存调压巡检
        public static bool SavePressureAdjustingInspection(tk_PressureAdjustingInspection record, List<tk_PressureAdjustingInspectionDetail> detailList, string type, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strdetailist = GSqlSentence.GetInsertByList(detailList, "BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail");
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_PressureAdjustingInspection>(record, "BGOI_CustomerService.dbo.tk_PressureAdjustingInspection");
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }
                    trans.Close(true);
                    if (count > 0)
                    {
                        if (strdetailist != "")
                        {
                            if (SQLBase.ExecuteNonQuery(strdetailist) > 0)
                            {
                                return true;
                            }
                            else
                            {
                                strErr = "添加失败";
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    #region [插入历史表]
                    #region [tk_PressureAdjustingInspection_HIS]
                    string oldshrvcard = "select * from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection where  TYID='" + record.TYID + "'";
                    DataTable dt = SQLBase.FillTable(oldshrvcard, "MainCustomer");
                    tk_PressureAdjustingInspection_HIS card = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        card = new tk_PressureAdjustingInspection_HIS();
                        card.TYID = dr["TYID"].ToString();
                        card.UserName = dr["UserName"].ToString();
                        card.UserAdd = dr["UserAdd"].ToString();
                        card.Users = dr["Users"].ToString();
                        card.OperationTime = Convert.ToDateTime(dr["OperationTime"].ToString());
                        card.Tel = dr["Tel"].ToString();
                        card.KeyStorageUnitJia = dr["KeyStorageUnitJia"].ToString();
                        card.KeyStorageUnitYi = dr["KeyStorageUnitYi"].ToString();
                        card.Uses = dr["Uses"].ToString();
                        card.Boiler = dr["Boiler"].ToString();
                        card.CreateUser = dr["CreateUser"].ToString();
                        card.KungFu = dr["KungFu"].ToString();
                        card.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                        card.Validate = "v";
                        card.Civil = dr["Civil"].ToString();
                        card.UserSignature = dr["UserSignature"].ToString();
                        card.UsePressureShang = dr["UsePressureShang"].ToString();
                        card.UsePressureXia = dr["UsePressureXia"].ToString();
                        card.InspectionPersonnel = dr["InspectionPersonnel"].ToString();
                        card.Remarks = dr["Remarks"].ToString();
                        // card.AfternoonTime = Convert.ToDateTime(dr["AfternoonTime"]);
                        card.NCreateUser = GAccount.GetAccountInfo().UserName;
                        card.NCreateTime = DateTime.Now;
                    }
                    string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_PressureAdjustingInspection_HIS>(card, "BGOI_CustomerService.dbo.tk_PressureAdjustingInspection_HIS");
                    if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存调压巡检记录表','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspection_HIS','" + record.TYID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存调压巡检记录表','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspection_HIS','" + record.TYID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    #endregion
                    #region [tk_PressureAdjustingInspectionDetail]
                    string oldstr = "select * from BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail where UsePressureShangP2!='' and  TYID='" + record.TYID + "'";
                    DataTable dtold = SQLBase.FillTable(oldstr, "MainCustomer");
                    tk_PressureAdjustingInspectionDetail_HIS hisold = null;
                    foreach (DataRow dr in dtold.Rows)
                    {
                        hisold = new tk_PressureAdjustingInspectionDetail_HIS();
                        hisold.TYID = dr["TYID"].ToString();
                        hisold.TXID = dr["TXID"].ToString();
                        hisold.UsePressureShangP1 = dr["UsePressureShangP1"].ToString();
                        hisold.UsePressureShangP2 = dr["UsePressureShangP2"].ToString();
                        hisold.UsePressureShangPb = dr["UsePressureShangPb"].ToString();
                        hisold.UsePressureShangPf = dr["UsePressureShangPf"].ToString();
                        hisold.UsePressureXiaP1 = dr["UsePressureXiaP1"].ToString();
                        hisold.UsePressureXiaP2 = dr["UsePressureXiaP2"].ToString();
                        hisold.UsePressureXiaPb = dr["UsePressureXiaPb"].ToString();
                        hisold.Validate = dr["Validate"].ToString();
                        hisold.NCreateUser = GAccount.GetAccountInfo().UserName;
                        hisold.NCreateTime = DateTime.Now;
                    }
                    string strDetailhis = GSqlSentence.GetInsertInfoByD<tk_PressureAdjustingInspectionDetail_HIS>(hisold, "BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail_HIS");
                    if (trans.ExecuteNonQuery(strDetailhis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存调压巡检记录表详细','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspectionDetail_HIS','" + record.TYID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存调压巡检记录表详细','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspectionDetail_HIS','" + record.TYID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    #endregion
                    #endregion
                    string strInsert = " update BGOI_CustomerService.dbo.tk_PressureAdjustingInspection set " +
                                       " TYID='" + record.TYID + "', UserAdd='" + record.UserAdd + "', Users='" + record.Users + "', Tel='" + record.Tel + "', KeyStorageUnitJia='" + record.KeyStorageUnitJia + "', Uses='" + record.Uses + "', Boiler=''," +
                                       " KungFu='', Civil='', Other='', UsePressureShang='" + record.UsePressureShang + "', OperationTime='" + record.OperationTime + "'," +
                                       " InspectionPersonnel='" + record.InspectionPersonnel + "', UserSignature='" + record.UserSignature + "', Remarks='" + record.Remarks + "', KeyStorageUnitYi='" + record.KeyStorageUnitYi + "'," +
                                       " UsePressureXia='" + record.UsePressureXia + "', CreateTime='" + record.CreateTime + "', CreateUser='" + record.CreateUser + "', Validate='" + record.Validate + "', UserName='" + record.UserName + "'" +
                                       " where TYID='" + record.TYID + "'";
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }
                    trans.Close(true);
                    if (count > 0)
                    {
                        string deletestr = "delete from BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail where TYID='" + record.TYID + "'";
                        if (SQLBase.ExecuteNonQuery(deletestr) > 0)
                        {
                            string strdetailist = GSqlSentence.GetInsertByList(detailList, "BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail");
                            if (SQLBase.ExecuteNonQuery(strdetailist) > 0)
                            {
                                #region [日志]
                                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改调压巡检记录表详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspectionDetail','" + record.TYID + "')";
                                SQLBase.ExecuteNonQuery(strlog);
                                #endregion
                                trans.Close(true);
                                return true;
                            }
                            else
                            {
                                #region [日志]
                                string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                    "values ('修改调压巡检记录表详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_PressureAdjustingInspectionDetail','" + record.TYID + "')";
                                SQLBase.ExecuteNonQuery(strlog);
                                #endregion
                                strErr = "修改失败";
                                return false;
                            }
                        }
                        else
                        {
                            strErr = "修改失败";
                            return false;
                        }
                        #region [冗余]

                        //for (int i = 0; i < detailList.Count; i++)
                        //{
                        //   stroldhis = " update BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail set TXID='"+detailList[i].TYID+"', TYID='"+detailList[i].TXID+"',"+
                        //       " UsePressureShangP1='" + detailList[i].UsePressureShangP1 + "', UsePressureShangP2='" + detailList[i].UsePressureShangP2 + "',"+
                        //       " UsePressureShangPb='" + detailList[i].UsePressureShangPb + "', UsePressureShangPf='" + detailList[i].UsePressureShangPf + "',"+
                        //       " UsePressureXiaP1='" + detailList[i].UsePressureXiaP1 + "', UsePressureXiaP2='" + detailList[i].UsePressureXiaP2 + "',"+
                        //       " UsePressureXiaPb='" + detailList[i].UsePressureXiaPb + "', Validate='" + detailList[i].Validate + "' "+
                        //       " where TXID='" + record.TYID + "'";
                        //   if (SQLBase.ExecuteNonQuery(stroldhis) > 0)
                        //   {
                        //       trans.Close(true);
                        //       return true;
                        //   }
                        //   else
                        //   {
                        //       strErr = "修改失败";
                        //       return false;
                        //   }
                        //}
                        #endregion
                        trans.Close(true);
                        return true;
                    }
                    else
                    {
                        strErr = "修改失败";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static UIDataTable PressureAdjustingInspectionList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " CreateTime  desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_PressureAdjustingInspection ";
            String strField = " TYID, UserAdd,Users, Tel, KeyStorageUnitJia, (case when Uses=0 then '锅炉'when Uses=1 then '公福' when Uses=2 then '民用' when Uses=3 then '其它' end) as Uses, Boiler, KungFu, Civil, Other, UsePressureShang, Convert(varchar(100),OperationTime,23) as OperationTime, InspectionPersonnel, UserSignature, Remarks, KeyStorageUnitYi, UsePressureXia, CreateTime, CreateUser, Validate, UserName ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //撤销
        public static bool DePressureAdjustingInspection(string TYID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_PressureAdjustingInspection set Validate='i' where TYID='" + TYID + "'";
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
        public static string GetTopTYID()
        {
            string strID = "";
            string strD = "TYX-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(TYID) from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection";
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

                    string stryyyyMMdd = strID.Substring(4, 8);
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
        public static DataTable UpPressureAdjustingInspection(string TYID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection where TYID='" + TYID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static string GetTopTXID()
        {
            string strID = "";
            string strD = "TXX-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(TXID) from BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail";
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

                    string stryyyyMMdd = strID.Substring(4, 8);
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
        public static UIDataTable PressureAdjustingInspectionDetailList(int a_intPageSize, int a_intPageIndex, string TYID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail where UsePressureShangP1!='' and TYID='" + TYID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " UsePressureShangP1!='' and TYID='" + TYID + "'";
            string strOrderBy = "TYID ";
            String strTable = " BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail ";
            String strField = " UsePressureShangP1, UsePressureShangP2, UsePressureShangPb, UsePressureShangPf, UsePressureXiaP1, UsePressureXiaP2, UsePressureXiaPb ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }

        public static DataTable UpPrintPressureAdjustingInspection(string TYID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_PressureAdjustingInspectionDetail where UsePressureShangP1!='' and  TYID='" + TYID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        public static DataTable UpTime(string TYID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection where  TYID='" + TYID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        #endregion
        #region [设备调试任务单]
        public static string GetTopTRID()
        {
            string strID = "";
            string strD = "TR-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(TRID) from BGOI_CustomerService.dbo.tk_EquipmentDebugging";
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
        //保存设备调试任务单
        public static bool SaveEquipmentCommissioning(tk_EquipmentDebugging record, List<tk_DebuggingSituation> delist, string type, ref string strErr)
        {
            int count = 0;
            int countlist = 0;
            string strUpdesi = "";
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_EquipmentDebugging>(record, "BGOI_CustomerService.dbo.tk_EquipmentDebugging");
                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert);
                    }
                    trans.Close(true);
                    //string PID = "";
                    string strInsertList = "";
                    //for (int i = 0; i < delist.Count; i++)
                    //{
                    //    PID = delist[i].PID;
                    //}
                    //if (PID != "")
                    //{
                        strInsertList = GSqlSentence.GetInsertByList(delist, "[BGOI_CustomerService].[dbo].tk_DebuggingSituation");
                    //}
                    if (strInsertList != "")
                    {
                        countlist = SQLBase.ExecuteNonQuery(strInsertList);
                    }

                    if (count > 0 && countlist > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单详细','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        return true;
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单详细','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    #region [插入历史表]
                    #region [tk_EquipmentDebugging_HIS]
                    string oldeqde = "select * from BGOI_CustomerService.dbo.tk_EquipmentDebugging where  TRID='" + record.TRID + "'";
                    DataTable dt = SQLBase.FillTable(oldeqde, "MainCustomer");
                    tk_EquipmentDebugging_HIS card = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        card = new tk_EquipmentDebugging_HIS();
                        card.TRID = dr["TRID"].ToString();
                        card.UserName = dr["UserName"].ToString();
                        card.Address = dr["Address"].ToString();
                        card.ContactPerson = dr["ContactPerson"].ToString();
                        card.Tel = dr["Tel"].ToString();
                        card.ConstructionUnit = dr["ConstructionUnit"].ToString();
                        card.CUnitPer = dr["CUnitPer"].ToString();
                        card.CUnitTel = dr["CUnitTel"].ToString();
                        card.EquManType = dr["EquManType"].ToString();
                        card.UnitName = dr["UnitName"].ToString();
                        card.UnitTel = dr["UnitTel"].ToString();
                        card.UnitPer = dr["UnitPer"].ToString();
                        card.DebPerson = dr["DebPerson"].ToString();
                        card.DebTime = Convert.ToDateTime(dr["DebTime"].ToString());
                        card.UserType = dr["UserType"].ToString();
                        card.Gas = dr["Gas"].ToString();
                        card.FieldFailure = dr["FieldFailure"].ToString();
                        card.Remark = dr["Remark"].ToString();
                        card.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
                        card.CreateUser = dr["CreateUser"].ToString();
                        card.Validate = "v";
                        card.NCreateUser = GAccount.GetAccountInfo().UserName;
                        card.NCreateTime = DateTime.Now;
                    }
                    string strEquipmentDebugginghis = GSqlSentence.GetInsertInfoByD<tk_EquipmentDebugging_HIS>(card, "BGOI_CustomerService.dbo.tk_EquipmentDebugging_HIS");
                    if (trans.ExecuteNonQuery(strEquipmentDebugginghis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_EquipmentDebugging_HIS','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion

                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_EquipmentDebugging_HIS','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    #endregion
                    #region [tk_DebuggingSituation_HIS]
                    string olddesi = "select * from BGOI_CustomerService.dbo.tk_DebuggingSituation where  TRID='" + record.TRID + "'";
                    DataTable dthis = SQLBase.FillTable(olddesi, "MainCustomer");
                    tk_DebuggingSituation_HIS desi = null;
                    foreach (DataRow drt in dthis.Rows)
                    {
                        desi = new tk_DebuggingSituation_HIS();
                        desi.QKID = drt["QKID"].ToString();
                        desi.TRID = drt["TRID"].ToString();
                        desi.PID = drt["PID"].ToString();
                        desi.Spec = drt["Spec"].ToString();
                        desi.ProductForm = drt["ProductForm"].ToString();
                        desi.PowerNumber = drt["PowerNumber"].ToString();
                        desi.PowerTime = Convert.ToDateTime(drt["PowerTime"].ToString());
                        desi.PowerInitialP = drt["PowerInitialP"].ToString();
                        desi.StepDownNumber = drt["StepDownNumber"].ToString();
                        desi.StepDownTime = Convert.ToDateTime(drt["StepDownTime"].ToString());
                        desi.StepDownInitialP = drt["StepDownInitialP"].ToString();
                        desi.InletPreP1 = drt["InletPreP1"].ToString();
                        desi.BleedingpreP1 = drt["BleedingpreP1"].ToString();
                        desi.PowerExportPreP2 = drt["PowerExportPreP2"].ToString();
                        desi.PowerOffPrePb = drt["PowerOffPrePb"].ToString();
                        desi.PowerCutOffPrePq = drt["PowerCutOffPrePq"].ToString();
                        desi.SDExportPreP2 = drt["SDExportPreP2"].ToString();
                        desi.SDPowerOffPrePb = drt["SDPowerOffPrePb"].ToString();
                        desi.SDCutOffPrePq = drt["SDCutOffPrePq"].ToString();
                        desi.NCreateUser = GAccount.GetAccountInfo().UserName;
                        desi.NCreateTime = DateTime.Now;
                    }
                    string strDebuggingSituationhis = GSqlSentence.GetInsertInfoByD<tk_DebuggingSituation_HIS>(desi, "BGOI_CustomerService.dbo.tk_DebuggingSituation_HIS");
                    if (trans.ExecuteNonQuery(strDebuggingSituationhis, CommandType.Text, null) > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单详细','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation_HIS','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('留存设备调试任务单详细','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation_HIS','" + record.TRID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    #endregion
                    #endregion

                    string strUpeqde = " update BGOI_CustomerService.dbo.tk_EquipmentDebugging set " +
                                       " TRID='" + record.TRID + "', UserName='" + record.UserName + "', Address='" + record.Address + "'," +
                                       " ContactPerson='" + record.ContactPerson + "', Tel='" + record.Tel + "', ConstructionUnit='" + record.ConstructionUnit + "'," +
                                       " CUnitPer='" + record.CUnitPer + "', CUnitTel='" + record.CUnitTel + "', EquManType='" + record.EquManType + "', UnitName='" + record.UnitName + "', " +
                                       " UnitTel='" + record.UnitTel + "', UnitPer='" + record.UnitPer + "', DebPerson='" + record.DebPerson + "', DebTime='" + record.DebTime + "'," +
                                       " UserType='" + record.UserType + "', Gas='" + record.Gas + "', FieldFailure='" + record.FieldFailure + "', Remark='" + record.Remark + "'," +
                                       " CreateTime='" + record.CreateTime + "', CreateUser='" + record.CreateUser + "', Validate='" + record.Validate + "'" +
                                       " where TRID='" + record.TRID + "'";
                    count = SQLBase.ExecuteNonQuery(strUpeqde);
                    trans.Close(true);
                    if (count > 0)
                    {
                        if (delist.Count > 0)
                        {
                            for (int j = 0; j < delist.Count; j++)
                            {
                                string tistr = " select * from BGOI_CustomerService.dbo.tk_DebuggingSituation where TRID='" + delist[j].TRID + "' and PID='" + delist[j].PID + "' ";
                                DataTable dtr = SQLBase.FillTable(tistr);
                                #region [if]
                                if (dtr.Rows.Count <= 0)
                                {
                                    strUpdesi = " Insert into BGOI_CustomerService.dbo.tk_DebuggingSituation (QKID, TRID, PID, Spec, ProductForm, PowerNumber, PowerTime, PowerInitialP, StepDownNumber, StepDownTime, StepDownInitialP, InletPreP1, BleedingpreP1, PowerExportPreP2, PowerOffPrePb, PowerCutOffPrePq, SDExportPreP2, SDPowerOffPrePb, SDCutOffPrePq,ProName) values('" + delist[j].QKID + "','" + delist[j].TRID + "','" + delist[j].PID + "','" + delist[j].Spec + "','" + delist[j].ProductForm + "','" + delist[j].PowerNumber + "','" + delist[j].PowerTime + "','" + delist[j].PowerInitialP + "','" + delist[j].StepDownNumber + "','" + delist[j].StepDownTime + "','" + delist[j].strStepDownInitialP + "','" + delist[j].InletPreP1 + "','" + delist[j].BleedingpreP1 + "','" + delist[j].PowerExportPreP2 + "','" + delist[j].PowerOffPrePb + "','" + delist[j].PowerCutOffPrePq + "','" + delist[j].SDExportPreP2 + "','" + delist[j].SDPowerOffPrePb + "','" + delist[j].SDCutOffPrePq + "','" + delist[j].ProName + "')";
                                    countlist = SQLBase.ExecuteNonQuery(strUpdesi);
                                    if (countlist > 0)
                                    {
                                        #region [日志]
                                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                            "values ('修改设备调试任务单详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + delist[j].TRID + "')";
                                        SQLBase.ExecuteNonQuery(strlog);
                                        #endregion
                                    }
                                    else
                                    {
                                        #region [日志]
                                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                            "values ('修改设备调试任务单详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + delist[j].TRID + "')";
                                        SQLBase.ExecuteNonQuery(strlog);
                                        #endregion
                                        strErr = "修改失败";
                                        return false;
                                    }
                                }
                                else
                                {
                                    strUpdesi = " update BGOI_CustomerService.dbo.tk_DebuggingSituation set " +
                                              " QKID='" + delist[j].QKID + "', TRID='" + delist[j].TRID + "', PID='" + delist[j].PID + "', Spec='" + delist[j].Spec + "'," +
                                              " ProductForm='" + delist[j].ProductForm + "', PowerNumber='" + delist[j].PowerNumber + "', PowerTime='" + delist[j].PowerTime + "'," +
                                              " PowerInitialP='" + delist[j].PowerInitialP + "', StepDownNumber='" + delist[j].StepDownNumber + "', StepDownTime='" + delist[j].StepDownTime + "'," +
                                              " StepDownInitialP='" + delist[j].strStepDownInitialP + "', InletPreP1='" + delist[j].InletPreP1 + "', BleedingpreP1='" + delist[j].BleedingpreP1 + "'," +
                                              " PowerExportPreP2='" + delist[j].PowerExportPreP2 + "', PowerOffPrePb='" + delist[j].PowerOffPrePb + "', PowerCutOffPrePq='" + delist[j].PowerCutOffPrePq + "'," +
                                              " SDExportPreP2='" + delist[j].SDExportPreP2 + "', SDPowerOffPrePb='" + delist[j].SDPowerOffPrePb + "', SDCutOffPrePq='" + delist[j].SDCutOffPrePq + "',ProName='" + delist[j].ProName + "' " +
                                              " where TRID='" + delist[j].TRID + "' and PID='" + delist[j].PID + "' ";
                                    countlist = SQLBase.ExecuteNonQuery(strUpdesi);
                                    if (countlist > 0)
                                    {
                                        #region [日志]
                                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                            "values ('修改设备调试任务单详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + delist[j].TRID + "')";
                                        SQLBase.ExecuteNonQuery(strlog);
                                        #endregion

                                    }
                                    else
                                    {
                                        #region [日志]
                                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                            "values ('修改设备调试任务单详细','修改成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_DebuggingSituation','" + delist[j].TRID + "')";
                                        SQLBase.ExecuteNonQuery(strlog);
                                        #endregion
                                        strErr = "修改失败";
                                        return false;
                                    }
                                }
                                #endregion
                            }
                        }
                        trans.Close(true);
                        return true;
                    }
                    else
                    {
                        strErr = "修改失败";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static string GetTopQKID()
        {
            string strID = "";
            string strD = "QK-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(QKID) from BGOI_CustomerService.dbo.tk_DebuggingSituation";
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
                    string stryyyyMMdd = strID.Substring(4, 8);
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
        public static UIDataTable EquipmentCommissioningList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            //string strSelCount = "select COUNT(*)  from BGOI_CustomerService.dbo.tk_EquipmentDebugging a" +
            //                     " left join BGOI_CustomerService.dbo.tk_DebuggingSituation b on a.TRID=b.TRID " +
            //                     " where " + where;
            string strSelCount = "select COUNT(*)  from BGOI_CustomerService.dbo.tk_EquipmentDebugging " +
                                " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            //string strOrderBy = " a.CreateTime ";
            //String strTable = " BGOI_CustomerService.dbo.tk_EquipmentDebugging a " +
            //                  " left join BGOI_CustomerService.dbo.tk_DebuggingSituation b on a.TRID=b.TRID ";
            //String strField = " a.TRID, UserName, Address, ContactPerson, Tel, ConstructionUnit, CUnitPer, CUnitTel, EquManType, UnitName, UnitTel, UnitPer, DebPerson, DebTime, UserType, Gas, FieldFailure, Remark, CreateTime, CreateUser,b.ProductForm ";
            string strOrderBy = " CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_EquipmentDebugging ";
            String strField = " TRID, UserName, Address, ContactPerson, Tel, ConstructionUnit, CUnitPer, CUnitTel, EquManType, UnitName, UnitTel, UnitPer, DebPerson, Convert(varchar(100),DebTime,23) as DebTime, UserType, Gas, FieldFailure, Remark, CreateTime, CreateUser";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            else
            {
                #region [设备管理方式]
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["EquManType"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["EquManType"] = "自管";
                    }
                    if (instData.DtData.Rows[r]["EquManType"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["EquManType"] = "厂家代管";
                    }
                    if (instData.DtData.Rows[r]["EquManType"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["EquManType"] = "输配公司代管";
                    }
                    if (instData.DtData.Rows[r]["EquManType"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["EquManType"] = "燃气施工方式代管";
                    }
                    if (instData.DtData.Rows[r]["EquManType"].ToString() == "4")
                    {
                        instData.DtData.Rows[r]["EquManType"] = "其它公司代管";
                    }
                }
                #endregion
                #region [设备形式]
                //for (int r = 0; r < instData.DtData.Rows.Count; r++)
                //{
                //    string Pro = instData.DtData.Rows[r]["ProductForm"].ToString();
                //    string[] str = Pro.Split(',');
                //    for (int i = 0; i < str.Length; i++)
                //    {
                //        if (str[i] == "0")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] = "调压器";
                //        }
                //        if (str[i] == "1")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",箱";
                //        }
                //        if (str[i] == "2")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",装置";
                //        }
                //        if (str[i] == "3")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",单";
                //        }
                //        if (str[i] == "4")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",双";
                //        }
                //        if (str[i] == "5")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",切换";
                //        }
                //        if (str[i] == "6")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",球";
                //        }
                //        if (str[i] == "7")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",油";
                //        }
                //        if (str[i] == "8")
                //        {
                //            instData.DtData.Rows[r]["ProductForm"] += ",蝶";
                //        }
                //    }
                //}
                #endregion
                #region [气种]
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["Gas"] = "天然气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["Gas"] = "人工煤气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["Gas"] = "液化石油气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["Gas"] = "混气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "4")
                    {
                        instData.DtData.Rows[r]["Gas"] = "沼气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "5")
                    {
                        instData.DtData.Rows[r]["Gas"] = "压缩天然气";
                    }
                    if (instData.DtData.Rows[r]["Gas"].ToString() == "6")
                    {
                        instData.DtData.Rows[r]["Gas"] = "其它";
                    }
                }
                #endregion
                #region [用户类别]
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["UserType"].ToString() == "0")
                    {
                        instData.DtData.Rows[r]["UserType"] = "锅炉";
                    }
                    if (instData.DtData.Rows[r]["UserType"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["UserType"] = "直燃机";
                    }
                    if (instData.DtData.Rows[r]["UserType"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["UserType"] = "公福";
                    }
                    if (instData.DtData.Rows[r]["UserType"].ToString() == "3")
                    {
                        instData.DtData.Rows[r]["UserType"] = "居民户";
                    }
                    if (instData.DtData.Rows[r]["UserType"].ToString() == "4")
                    {
                        instData.DtData.Rows[r]["UserType"] = "其它";
                    }
                }
                #endregion
            }

            return instData;
        }
        //撤销
        public static bool DeEquipmentCommissioning(string TRID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_EquipmentDebugging set Validate='i' where TRID='" + TRID + "'";
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
        //根据编号加载主表信息
        public static DataTable UpEquipmentCommissioning(string TRID)
        {
            string sql = "select TRID, UserName, Address, ContactPerson, Tel, ConstructionUnit, CUnitPer, CUnitTel, EquManType, UnitName, UnitTel, UnitPer, DebPerson, Convert(varchar(100),DebTime,23) as  DebTime, UserType, Gas, FieldFailure, Remark,Convert(varchar(100),CreateTime,23) as CreateTime, CreateUser, Validate from BGOI_CustomerService.dbo.tk_EquipmentDebugging where TRID='" + TRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //根据编号加载副表信息
        public static DataTable UpDebuggingSituation(string TRID)
        {
            string sql = "select a.QKID, a.TRID, a.PID, a.Spec, a.ProductForm, a.PowerNumber,Convert(varchar(100),a.PowerTime,23) as  PowerTime, a.PowerInitialP, a.StepDownNumber,Convert(varchar(100),a.StepDownTime,23) as StepDownTime, a.StepDownInitialP, a.InletPreP1, a.BleedingpreP1," +
                         " a.PowerExportPreP2, a.PowerOffPrePb, a.PowerCutOffPrePq, a.SDExportPreP2, a.SDPowerOffPrePb, a.SDCutOffPrePq,a.ProName from BGOI_CustomerService.dbo.tk_DebuggingSituation a " +
                         " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.PID=b.PID " +
                         " where a.TRID='" + TRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //加载详细
        public static UIDataTable EquipmentCommissioningDetialList(int a_intPageSize, int a_intPageIndex, string TRID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*)  from BGOI_CustomerService.dbo.tk_DebuggingSituation a" +
                                 " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.PID=b.PID " +
                                 " where  a.TRID='" + TRID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.TRID='" + TRID + "'";
            string strOrderBy = " a.TRID ";
            String strTable = " BGOI_CustomerService.dbo.tk_DebuggingSituation a " +
                              " left join BGOI_Inventory.dbo.tk_ProductInfo b on a.PID=b.PID ";
            String strField = " QKID, TRID, ProductForm, PowerNumber,Convert(varchar(100),PowerTime,23) as  PowerTime, PowerInitialP, StepDownNumber," +
                              " Convert(varchar(100),StepDownTime,23) as StepDownTime, StepDownInitialP, InletPreP1, BleedingpreP1, PowerExportPreP2, PowerOffPrePb," +
                              " PowerCutOffPrePq, SDExportPreP2, SDPowerOffPrePb, SDCutOffPrePq,a.ProName, a.PID, a.Spec  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        //根据产品加载保修卡
        public static DataTable GetPIDBaoDetail(string PID)
        {
            string sql = "select top 5 * from  BGOI_CustomerService.dbo.tk_WXRequisit a ,BGOI_Inventory.dbo.tk_ProductInfo b  where b.ProName=a.DeviceName  and b.PID='" + PID + "' order by CreateTime DESC ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        public static UIDataTable EqProductReportList(int a_intPageSize, int a_intPageIndex, string RelationID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                 " from  BGOI_CustomerService.dbo.tk_SHInstallR a" +
                                 " left join (select distinct BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product) b on a.BZID=b.BZID   " +
                                 " left join BGOI_Inventory.dbo.tk_WareHouse c on a.WarehouseTwo=c.HouseID or a.WarehouseOne=c.HouseID " +
                                 " where RelationID='" + RelationID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " RelationID='" + RelationID + "' ";
            string strOrderBy = "a.InstallTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_SHInstallR a " +
                             " left join (select distinct BZID from BGOI_CustomerService.dbo.tk_SHInstallR_Product) b on a.BZID=b.BZID   " +
                               " left join BGOI_Inventory.dbo.tk_WareHouse c on a.WarehouseTwo=c.HouseID or a.WarehouseOne=c.HouseID ";
            String strField = " a.DiPer,c.HouseName,a.BZID,a.Remark,a.CreateUser,Convert(varchar(100),a.InstallTime,23)  as InstallTime,a.CustomerName as CustomerName,Convert(varchar(12),a.CreateTime,111) as CreateTime,a.Tel,a.Address,a.WarehouseTwo,convert(varchar(20),a.Sate,120) State ";
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
                        instData.DtData.Rows[r]["State"] = "未报装";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "已报装";
                    }

                }
            }

            return instData;
        }
        //判断是否报装
        public static DataTable PanDuanIfPro(string TRID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_SHInstallR  where RelationID='" + TRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        #endregion
        #endregion
        #region [销售记录]
        #region [收款记录]
        public static string GetTopCRID()
        {
            string strID = "";
            string strD = "SKJ-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(CRID) from BGOI_CustomerService.dbo.tk_CollectionRecord";
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

                    string stryyyyMMdd = strID.Substring(4, 8);
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
        //保存/修改收款记录
        public static bool SaveCollectionRecord(tk_CollectionRecord record, string type, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_CollectionRecord>(record, "BGOI_CustomerService.dbo.tk_CollectionRecord");
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
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    #region [修改]
                    int i = 0, m = 0, n = 0;
                    string InserNewOrdersHIS = "insert into BGOI_CustomerService.dbo.tk_CollectionRecord_HIS (CRID, CRTime, PaymentUnit, CollectionAmount, PaymentContent, PaymentMethod, PaymentPeo, CorporateFinance, Remark, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
          "select CRID, CRTime, PaymentUnit, CollectionAmount, PaymentContent, PaymentMethod, PaymentPeo, CorporateFinance, Remark, CreateTime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID ='" + record.CRID + "'";
                    m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainCustomer");

                    string strDelete = "delete from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID='" + record.CRID + "'";
                    i = SQLBase.ExecuteNonQuery(strDelete, "MainCustomer");

                    string strInsertnew = GSqlSentence.GetInsertInfoByD<tk_CollectionRecord>(record, "BGOI_CustomerService.dbo.tk_CollectionRecord");

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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static UIDataTable CollectionRecordList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_CollectionRecord a" +
                                   " left join BGOI_CustomerService.dbo.tk_Approval b on a.CRID=b.RelevanceID " +
                                   " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_CollectionRecord a " +
                              " left join BGOI_CustomerService.dbo.tk_Approval b on a.CRID=b.RelevanceID ";
            String strField = "Convert(varchar(12),a.CRTime,111) as CRTime,a.BRDID,a.CRID,a.PaymentUnit, a.CollectionAmount, a.PaymentContent, (case when a.PaymentMethod=0 then '汇款'when a.PaymentMethod=1 then '支票' when a.PaymentMethod=2 then '现金' end) as PaymentMethod, a.PaymentPeo, a.CorporateFinance, a.Remark,a.CreateTime, a.CreateUser, a.Validate,(case when a.StateNew='0' then '未收款'when a.StateNew='1' then '已收款' end) as State,b.PID   ";
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
        public static bool DeCollectionRecord(string CRID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_CollectionRecord set Validate='i' where CRID='" + CRID + "'";
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
        public static bool GetState(string CRID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_CollectionRecord set StateNew='1' where CRID='" + CRID + "'";
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
                    strErr = "收款完成！";
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
        public static DataTable UpCollectionRecord(string CRID)
        {
            string sql = "select  * from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID='" + CRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        public static DataTable UpUpCollectionRecordList(string CRID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID='" + CRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        //审批
        public static DataTable GetBasCusCollectionRecord(string CRID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID='" + CRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }

        public static UIDataTable UserBillingRecordsProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count (*) from (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                                 " left join BGOI_CustomerService.dbo.tk_CollectionRecord a on a.CRID = b.RelevanceID " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                              " left join BGOI_CustomerService.dbo.tk_CollectionRecord a on a.CRID = b.RelevanceID ";
            String strField = " a.State,b.PID as SPID,a.CRID,Convert(varchar(100),CRTime,23) as CRTime, PaymentUnit, CollectionAmount, PaymentContent, PaymentMethod, PaymentPeo, CorporateFinance, " +
                              " Remark, CreateTime, CreateUser,BRDID, Validate ";
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
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "待审批";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["State"] = "已审批";
                    }

                }
            }
            return instData;
        }

        public static DataTable GetSKSP(string CRID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_CollectionRecord where CRID='" + CRID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [订货单]
        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from BGOI_CustomerService.dbo.OrdersInfo " + strWhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainCustomer"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " SalesType='Sa01' and Validate ='v' " + strWhere;
            string strOrderBy = " CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.OrdersInfo";
            String strField = "PID, UnitID, OrderID, ContractID, CustomerID, SalesType, ContractDate, ProjectName, SupplyTime, OrderUnit, OrderContactor, OrderTel, OrderAddress, UseUnit, UseContactor, UseTel, UseAddress, Total, PayWay, Guarantee, Provider, ProvidManager, Demand, DemandManager, Remark, BusinessPerson, TotalTax, ReturnNoTax, ReturnTax, IsBranch, IsPriceRules, IsHK, HKRemark, IsManager, CreateUser, CreateTime, Validate, State, OState, ExpectedReturnDate, ChannelsFrom, ISF, ISHT, EXState ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainCustomer");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            if (instData.DtData != null)
            {
                //for (int r = 0; r < instData.DtData.Rows.Count; r++)
                //{
                //    instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
                //}
            }
            return instData;
        }
        public static string GetNewOrderID()
        {
            string strID = "";
            string strD = "DH" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(OrderID) from BGOI_CustomerService.dbo.OrdersInfo";
            DataTable dtID = SQLBase.FillTable(strSqlID, "MainCustomer");
            string UnitID = string.Format("{0:D4}", Convert.ToInt32(GAccount.GetAccountInfo().UnitID));
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + UnitID + string.Format("{0:D3}", 1);
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(2, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));
                        else if (num < 99 && num >= 9)
                            strD = strD + UnitID + string.Format("{0:D2}", (num + 1));

                        else
                            strD = strD + UnitID + (num + 1);
                    }
                    else
                    {
                        strD = strD + UnitID + string.Format("{0:D3}", 1);
                    }
                }
            }
            else
            {
                strD = strD + UnitID + string.Format("{0:D3}", 1);
            }
            return strD;
        }
        public static string GetMaxContractID()
        {
            string s = "";
            string strID = "";
            string str = "select *  from BGOI_CustomerService.dbo.OrdersInfo order by  right(right(ContractID,3),3) desc";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            if (dt == null) return null;

            string Str = GetNamePY(GAccount.GetAccountInfo().UserName);
            string Dime = DateTime.Now.Year.ToString();// ("YYYY");
            Dime = Dime.Substring(2, 2);
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0][3].ToString();
                if (strID == "" || strID == null)
                {
                    s = Str + "-" + Dime + "-" + "001";
                }
                else
                {
                    strID = strID.Substring(strID.Length - 3);
                    strID = string.Format("{0:D3}", Convert.ToInt32(strID) + 1);
                    s = Str + "-" + Dime + "-" + strID;
                }
            }
            else
            {
                s = Str + "-" + Dime + "-" + "001";
            }
            return s;
        }
        public static string GetNamePY(string LoginName)
        {
            string str = "select dbo.fGetPy('" + LoginName + "')";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            if (dt == null) return null;
            return dt.Rows[0][0].ToString();
        }
        public static bool SaveOrderInfo(OrdersInfoNew ordersinfo, List<Orders_DetailInfoNew> list, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<OrdersInfoNew>(ordersinfo, "BGOI_CustomerService.dbo.OrdersInfo");
                string strInsertList = "";
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, " BGOI_CustomerService.dbo.Orders_DetailInfo");
                }
                if (strInsert != "")
                {
                    SQLBase.ExecuteNonQuery(strInsert, "MainCustomer");

                }
                if (strInsertList != "")
                {
                    if (SQLBase.ExecuteNonQuery(strInsertList, "MainCustomer") > 0)
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加订单','添加成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + ordersinfo.OrderID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
                    else
                    {
                        #region [日志]
                        string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                            "values ('添加订单','添加失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_SHRV_Product','" + ordersinfo.OrderID + "')";
                        SQLBase.ExecuteNonQuery(strlog);
                        #endregion
                    }
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
        public static UIDataTable LoadOrderDetailnew(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderID ='" + OrderID + "' and Validate ='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainCustomer"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "OrderID='" + OrderID + "' and Validate ='v'";
            string strOrderBy = "DeliveryTime ";
            String strTable = " BGOI_CustomerService.dbo.Orders_DetailInfo";
            String strField = " PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer ," +
"OrderUnit ,OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,Convert(varchar(100),DeliveryTime,23) as DeliveryTime ,State ,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    if (instData.DtData.Rows[r]["ProductID"].ToString() == "undefined")
                    {
                        instData.DtData.Rows[r]["ProductID"] = "";
                    }
                    if (instData.DtData.Rows[r]["SpecsModels"].ToString() == "undefined")
                    {
                        instData.DtData.Rows[r]["SpecsModels"] = "";
                    }

                    if (instData.DtData.Rows[r]["OrderContent"].ToString() == "undefined")
                    {
                        instData.DtData.Rows[r]["OrderContent"] = "";
                    }
                    if (instData.DtData.Rows[r]["OrderUnit"].ToString() == "undefined")
                    {
                        instData.DtData.Rows[r]["OrderUnit"] = "";
                    }
                    //instData.DtData.Rows[r]["offerState"] = GetStatePro(instData.DtData.Rows[r]["offerState"].ToString(), "OfferState").StateDesc;
                    //instData.DtData.Rows[r]["BelongArea"] = GetSelectPro(instData.DtData.Rows[r]["BelongArea"].ToString()).Text;
                }
            }



            return instData;
        }
        public static bool CanCelOrdersInfo(string OrderID, ref string strErr)
        {
            string strCancel = " update  BGOI_CustomerService.dbo.OrdersInfo set Validate ='i' where OrderID ='" + OrderID + "'";
            try
            {
                int i = SQLBase.ExecuteNonQuery(strCancel, "MainCustomer");
                if (i <= 0) return false;
                return true;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return false;
                throw;
            }


            //return true;
        }
        public static OrdersInfo GetOrdersByOrderIDnew(string OrderID)
        {
            string str = "select PID, UnitID, OrderID, ContractID, CustomerID, SalesType,Convert(varchar(100),ContractDate,23) as ContractDate, ProjectName, SupplyTime, OrderUnit, OrderContactor, OrderTel, OrderAddress, UseUnit, UseContactor, UseTel, UseAddress, Total, PayWay, Guarantee, Provider, ProvidManager, Demand, DemandManager, Remark, BusinessPerson, TotalTax, ReturnNoTax, ReturnTax, IsBranch, IsPriceRules, IsHK, HKRemark, IsManager, CreateUser, CreateTime, Validate, State, OState, ExpectedReturnDate, ChannelsFrom, ISF, ISHT, EXState from BGOI_CustomerService.dbo.OrdersInfo where OrderID='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            if (dt == null) return null;
            OrdersInfo OrderInfo = new OrdersInfo();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToOrderInfonew(item, OrderInfo);
            }
            return OrderInfo;
        }
        public static void DataRowToOrderInfonew(DataRow item, OrdersInfo orderinfo)
        {
            orderinfo.OrderID = item["OrderID"].ToString();
            orderinfo.PID = item["PID"].ToString();
            orderinfo.UnitID = item["UnitID"].ToString();
            orderinfo.ContractID = item["ContractID"].ToString();
            orderinfo.SalesType = item["SalesType"].ToString();
            orderinfo.ContractDate = Convert.ToDateTime(item["ContractDate"]);
            orderinfo.OrderUnit = item["OrderUnit"].ToString();
            orderinfo.OrderContactor = item["OrderContactor"].ToString();
            orderinfo.OrderTel = item["OrderTel"].ToString();
            orderinfo.OrderAddress = item["OrderAddress"].ToString();
            orderinfo.UseUnit = item["UseUnit"].ToString();
            orderinfo.UseContactor = item["UseContactor"].ToString();
            orderinfo.UseTel = item["UseTel"].ToString();
            orderinfo.UseAddress = item["UseAddress"].ToString();
            if (!string.IsNullOrEmpty(item["Total"].ToString())) { orderinfo.Total = Convert.ToDecimal(item["Total"]); }
            orderinfo.PayWay = GetSelectPro(item["PayWay"].ToString()).Text;
            orderinfo.Guarantee = item["Guarantee"].ToString();
            orderinfo.Provider = item["Provider"].ToString();
            orderinfo.ProvidManager = item["ProvidManager"].ToString();
            orderinfo.Demand = item["Demand"].ToString();
            orderinfo.DemandManager = item["DemandManager"].ToString();
            orderinfo.Remark = item["Remark"].ToString();
            orderinfo.IsBranch = item["IsBranch"].ToString();
            orderinfo.IsPriceRules = item["IsPriceRules"].ToString();
            orderinfo.IsManager = item["IsManager"].ToString();
            orderinfo.State = item["State"].ToString();
            orderinfo.CreateTime = item["CreateTime"].ToString();
            orderinfo.CreateUser = item["CreateUser"].ToString();
            orderinfo.Validate = item["Validate"].ToString();
            orderinfo.ChannelsFrom = item["ChannelsFrom"].ToString();
            orderinfo.ExpectedReturnDate = item["ExpectedReturnDate"].ToString();
        }
        public static ProjectSelect_Config GetSelectPro(string ID)
        {
            string str = "select * from ProjectSelect_Config where ID='" + ID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            ProjectSelect_Config proselect = new ProjectSelect_Config();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToProSele(proselect, item);
            }
            return proselect;
        }
        private static void DatarowToProSele(ProjectSelect_Config pros, DataRow item)
        {
            pros.ID = item["ID"].ToString();
            pros.Text = item["Text"].ToString();
            pros.Type = item["Type"].ToString();
        }
        public static DataTable GetOrdersDetailnew(string OrderID)
        {
            string Str = "select DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
                + "OrderNum,Price,Subtotal,Technology,Convert(varchar(100),DeliveryTime,23) as DeliveryTime,TaxRate from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderID='" + OrderID + "' and Validate ='v' ";
            DataTable dt = SQLBase.FillTable(Str, "MainCustomer");
            if (dt == null) return null;
            return dt;
        }
        public static bool SaveUpdateOrderInfonew(OrdersInfoNew ordersinfo, List<Orders_DetailInfoNew> list, ref string strErr)
        {
            try
            {
                #region [插入历史表]
                int i = 0, j = 0, m = 0, n = 0;
                string InserNewOrdersHIS = "insert into BGOI_CustomerService.dbo.OrdersInfo_HIS (PID,UnitID,OrderID,ContractID,SalesType,ContractDate,OrderUnit,OrderContactor,OrderTel,OrderAddress,UseUnit,UseContactor,UseTel," +
           "UseAddress,Total,PayWay,Guarantee,Provider,ProvidManager,Demand,DemandManager,Remark,IsBranch,IsPriceRules,IsManager,State," +
           "CreateTime,CreateUser,Validate,OState,NCreateTime ,NCreateUser)" +
           "select PID,UnitID,OrderID,ContractID,SalesType,ContractDate,OrderUnit,OrderContactor,OrderTel,OrderAddress,UseUnit,UseContactor,UseTel," +
           "UseAddress,Total,PayWay,Guarantee,Provider,ProvidManager,Demand,DemandManager,Remark,IsBranch,IsPriceRules,IsManager,State," +
           "CreateTime,CreateUser,Validate,OState,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_CustomerService.dbo.OrdersInfo where OrderID ='" + ordersinfo.OrderID + "'";
                m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainCustomer");
                // string strUpdateList = "";
                string strInsertDetailHIS = "";

                string detailHis = "select * from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderID='" + ordersinfo.OrderID + "'";
                DataTable dtHIS = SQLBase.FillTable(detailHis, "MainCustomer");
                if (dtHIS.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHIS.Rows)
                    {
                        strInsertDetailHIS = "insert into BGOI_CustomerService.dbo.Orders_DetailInfo_HIS(PID,OrderID,DID,ProductID,OrderContent,SpecsModels," +
                             " Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,NCreateUser,NCreateTime)" +
                             "select PID,OrderID,DID,ProductID,OrderContent,SpecsModels," + "Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                             " from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderID='" + item["OrderID"] + "'";
                        SQLBase.ExecuteNonQuery(strInsertDetailHIS, "MainCustomer");
                    }
                }
                #endregion
                string strUpdate = "update BGOI_CustomerService.dbo.OrdersInfo set OrderUnit=@OrderUnit,OrderContactor=@OrderContactor,OrderTel=@OrderTel," +
                    "OrderAddress=@OrderAddress,UseUnit=@UseUnit,UseContactor=@UseContactor,UseTel=@UseTel,UseAddress=@UseAddress," +
                    "Total=@Total,PayWay=@PayWay,Guarantee=@Guarantee,Provider=@Provider,ProvidManager=@ProvidManager," +
                    "Demand=@Demand,DemandManager=@DemandManager,Remark=@Remark where OrderID=@OrderID";
                SqlParameter[] param ={new SqlParameter ("@OrderUnit",SqlDbType .NVarChar),
                                       new SqlParameter ("@OrderContactor",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@OrderTel",SqlDbType .VarChar ),
                                       new SqlParameter ("@OrderAddress",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseUnit",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseContactor",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseTel",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseAddress",SqlDbType .VarChar ),
                                       new SqlParameter ("@Total",SqlDbType .VarChar ),
                                       new SqlParameter ("@PayWay",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Guarantee",SqlDbType .VarChar ),
                                       new SqlParameter ("@Provider",SqlDbType .VarChar ),
                                       new SqlParameter ("@ProvidManager",SqlDbType .VarChar ),
                                       new SqlParameter ("@Demand",SqlDbType .VarChar ),
                                       new SqlParameter ("@DemandManager",SqlDbType .VarChar ),
                                       new SqlParameter ("@Remark",SqlDbType .VarChar ),
                                       new SqlParameter ("@OrderID",SqlDbType .VarChar )
                                     };
                param[0].Value = ordersinfo.OrderUnit;
                if (ordersinfo.OrderContactor == null) { param[1].Value = ""; }
                else { param[1].Value = ordersinfo.OrderContactor; }
                if (ordersinfo.OrderTel == null) { param[2].Value = ""; }
                else { param[2].Value = ordersinfo.OrderTel; }
                if (ordersinfo.OrderAddress == null) { param[3].Value = ""; }
                else { param[3].Value = ordersinfo.OrderAddress; }
                if (ordersinfo.UseUnit == null) { param[4].Value = ""; }
                else { param[4].Value = ordersinfo.UseUnit; }
                if (ordersinfo.UseContactor == null) { param[5].Value = ""; }
                else { param[5].Value = ordersinfo.UseContactor; }
                if (ordersinfo.UseTel == null) { param[6].Value = ""; }
                else { param[6].Value = ordersinfo.UseTel; }
                if (ordersinfo.UseAddress == null) { param[7].Value = ""; }
                else { param[7].Value = ordersinfo.UseAddress; }
                if (ordersinfo.Total == null) { param[8].Value = ""; }
                else { param[8].Value = ordersinfo.Total; }
                if (ordersinfo.PayWay == null) { param[9].Value = ""; }
                else { param[9].Value = ordersinfo.PayWay; }
                if (ordersinfo.Guarantee == null) { param[10].Value = ""; }
                else { param[10].Value = ordersinfo.Guarantee; }
                if (ordersinfo.Provider == null) { param[11].Value = ""; }
                else { param[11].Value = ordersinfo.Provider; }
                if (ordersinfo.ProvidManager == null) { param[12].Value = ""; }
                else { param[12].Value = ordersinfo.ProvidManager; }
                if (ordersinfo.Demand == null) { param[13].Value = ""; }
                else { param[13].Value = ordersinfo.Demand; }
                if (ordersinfo.DemandManager == null) { param[14].Value = ""; }
                else { param[14].Value = ordersinfo.DemandManager; }
                if (ordersinfo.Remark == null) { param[15].Value = ""; }
                else { param[15].Value = ordersinfo.Remark; }
                if (ordersinfo.OrderID == null) { param[16].Value = ""; }
                else { param[16].Value = ordersinfo.OrderID; }
                string strDelete = "delete from BGOI_CustomerService.dbo.Orders_DetailInfo where OrderID='" + ordersinfo.OrderID + "'";
                i = SQLBase.ExecuteNonQuery(strDelete, "MainCustomer");
                if (list.Count > 0)
                {
                    string strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_CustomerService.dbo.Orders_DetailInfo");
                    j = SQLBase.ExecuteNonQuery(strInsertList, "MainCustomer");

                }
                if (strUpdate != "")
                {
                    n = SQLBase.ExecuteNonQuery(strUpdate, param, "MainCustomer");
                }
                if (i + m + n >= 3)
                {
                    return true;
                }
                else
                {
                    strErr = "修改失败";
                    return false;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }
        public static UIDataTable GetOrderInfonew(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from(select  a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.OrderTel,a.OrderAddress,a.UseUnit,a.Total,DebtAmount=(a.Total-ISnull((select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),0)),HKAmount=(select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),a.UseContactor,a.UseTel,a.UseAddress,a.IsHK,a.State,a.Ostate,a.Ostate as OOstate,a.ISF,a.ISHT,a.EXState from  BGOI_CustomerService.dbo.OrdersInfo " + "a  Where a.SalesType='Sa01' and Validate ='v' " + strWhere + ")as s";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " SalesType='Sa01' and Validate ='v' " + strWhere;
            string strOrderBy = " a.CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.OrdersInfo a  ";
            String strField = "   a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.OrderTel,a.OrderAddress,a.UseUnit,a.Total,DebtAmount=(a.Total-ISnull((select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),0)),HKAmount=(select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),a.UseContactor,a.UseTel,a.UseAddress,a.IsHK,a.State,a.Ostate,a.Ostate as OOstate,a.ISF,a.ISHT,a.EXState";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
                }
            }
            return instData;
        }
        public static DataTable GetOrderInfoToExcelnew(string where, ref string strErr)
        {
            string str = "select a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.UseUnit,a.UseContactor,a.UseTel,a.UseAddress,b.IsPay ,a.State,a.Guarantee,a.Provider,a.ProvidManager,a.Demand,a.DemandManager,a.Remark" +
            " from BGOI_CustomerService.dbo.OrdersInfo a left join ProjectBasInfo b on a.PID=b.PID " + where + "order by ContractDate";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            return dt;
        }
        #region [供应商]
        public static UIDataTable GetCheckSupListOld(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            string where = "";
            if (ptype != "")
            {
                where = " and a.SupplierType='" + ptype + "'";
            }
            else
            {
                where = "";
            }
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) From BGOI_BasMan .dbo.tk_supplierbas a,BGOI_BasMan .dbo.tk_ConfigSupType b where a.SupplierType=b.Suid " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainCustomer"));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " a.SupplierType=b.Suid " + where;
            string strOrderBy = " SID ";
            String strTable = " BGOI_BasMan .dbo.tk_supplierbas a,BGOI_BasMan .dbo.tk_ConfigSupType b  ";
            String strField = " SID,COMNameC";

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainCustomer");

            return instData;
        }
        public static DataTable GetSupplierCot(string SID)
        {
            string str = "select a.SID,SupplierType,COMNameC ,SupplierCode ,b.Price from BGOI_BasMan .dbo.tk_SupplierBas a inner join BGOI_BasMan .dbo.tk_SProducts b on a.SID =b.SID where a.SID='" + SID + "'";
            DataTable dt = SQLBase.FillTable(str, "MainCustomer");
            if (dt == null) return null;
            return dt;
        }
        //获取供应商的类别
        public static UIDataTable GetSupTypeOld(int a_intPageSize, int a_intPageIndex)
        {
            try
            {
                string strSelCount = "";
                UIDataTable instData = new UIDataTable();

                strSelCount = "select count(*)  From BGOI_BasMan.dbo.tk_ConfigSupType ";
                instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "MainCustomer"));

                if (instData.IntRecords > 0)
                {
                    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
                }
                else
                    instData.IntTotalPages = 0;
                string strFilter = "";
                string strOrderBy = " Suid  ";
                String strTable = "BGOI_BasMan.dbo.tk_ConfigSupType ";
                String strField = "Suid,SupplierType";
                instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "MainCustomer");
                return instData;
            }
            catch (Exception)
            {
                //   strErr = ex.Message;
                throw;
            }

        }
        #endregion
        #region [报价的状态]
        public static ProjectState_Config GetStatePro(string ID, string Type)
        {
            string str = "select * from ProjectState_Config where StateId='" + ID + "' and StateType='" + Type + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            ProjectState_Config proselect = new ProjectState_Config();
            foreach (DataRow item in dt.Rows)
            {
                DatarowToPState(proselect, item);
            }
            return proselect;
        }
        public static void DatarowToPState(ProjectState_Config Pstate, DataRow item)
        {
            Pstate.StateId = item["StateId"].ToString();
            Pstate.StateDesc = item["StateDesc"].ToString();
            Pstate.StateType = item["StateType"].ToString();
        }
        #endregion
        #region MyRegion
        public static string GetNamePYnew(string LoginName)
        {
            string str = "select dbo.fGetPy('" + LoginName + "')";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            if (dt == null) return null;
            return dt.Rows[0][0].ToString();
        }
        #endregion

        public static DataTable GetUM_USERNEW(string DeptID)
        {
            string sql = "select * from um_userNew where DeptId ='" + DeptID + "'";
            //' and roleNames ='销售人员' ";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");

            return dt;
        }
        #endregion
        #region [开票记录]
        public static string GetTopBRDID()
        {
            string strID = "";
            string strD = "BRD-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(BRDID) from BGOI_CustomerService.dbo.tk_BillingRecords";
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

                    string stryyyyMMdd = strID.Substring(4, 8);
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
        //保存/修改收款记录
        public static bool SaveBillingRecords(tk_BillingRecords record, string type, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_BillingRecords>(record, "BGOI_CustomerService.dbo.tk_BillingRecords");
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
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    #region [修改]
                    int i = 0, m = 0, n = 0;
                    string InserNewOrdersHIS = "insert into BGOI_CustomerService.dbo.tk_BillingRecords_HIS (BRDID, BRDTime, DwName, Project, Amount, PersonCharge, ReceivablesTime, PaymentMethod, Remark, CreateTime, CreateUser, Validate, NCreateTime, NCreateUser)" +
          "select BRDID, BRDTime, DwName, Project, Amount, PersonCharge, ReceivablesTime, PaymentMethod, Remark, CreateTime, CreateUser, Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID ='" + record.BRDID + "'";
                    m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "MainCustomer");

                    string strDelete = "delete from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID='" + record.BRDID + "'";
                    i = SQLBase.ExecuteNonQuery(strDelete, "MainCustomer");

                    string strInsertnew = GSqlSentence.GetInsertInfoByD<tk_BillingRecords>(record, "BGOI_CustomerService.dbo.tk_BillingRecords");

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
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static UIDataTable BillingRecordsList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_BillingRecords  a " +
                                    " left join BGOI_CustomerService.dbo.tk_Approval b on a.BRDID=b.RelevanceID " +
                                    " left join BGOI_BasMan.dbo.tk_ConfigContent  c on a.PaymentMethod=c.SID " +
                                   " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_BillingRecords a" +
                              " left join BGOI_CustomerService.dbo.tk_Approval b on a.BRDID=b.RelevanceID " +
                               " left join BGOI_BasMan.dbo.tk_ConfigContent  c on a.PaymentMethod=c.SID ";
            String strField = "Convert(varchar(12),a.ReceivablesTime,111) as ReceivablesTime,a.BRDID, Convert(varchar(12),a.BRDTime,111) as BRDTime, a.DwName, a.Project, a.Amount, a.PersonCharge,c.Text as PaymentMethod, a.Remark, a.CreateTime, a.CreateUser, a.Validate,b.PID ";
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
        public static bool DeBillingRecords(string BRDID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "update BGOI_CustomerService.dbo.tk_BillingRecords set Validate='i' where BRDID='" + BRDID + "'";
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
        public static DataTable UpBillingRecords(string BRDID)
        {
            string sql = "select  * from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID='" + BRDID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        public static DataTable UpUpUpBillingRecords(string BRDID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID='" + BRDID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");

            return dt;
        }
        public static DataTable GetKPJL(string BRDID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_CollectionRecord where BRDID='" + BRDID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //判断审批
        public static DataTable GetPDSP(string BRDID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID='" + BRDID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //审批
        public static DataTable GetBasBillingRecords(string BRDID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_BillingRecords where BRDID='" + BRDID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        public static UIDataTable UserKPProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count (*) from (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                                 " left join BGOI_CustomerService.dbo.tk_BillingRecords a on a.BRDID = b.RelevanceID " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                              " left join BGOI_CustomerService.dbo.tk_BillingRecords a on a.BRDID = b.RelevanceID ";
            String strField = " Convert(varchar(12),ReceivablesTime,111) as ReceivablesTime,a.BRDID," +
                                " Convert(varchar(12),BRDTime,111) as BRDTime, DwName, Project, Amount, PersonCharge," +
                                " a.PaymentMethod, a.Remark, a.CreateTime, a.CreateUser, a.Validate,a.State,b.PID as SPID ";
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
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "待审批";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["State"] = "已审批";
                    }

                }
            }
            return instData;
        }
        #endregion
        #endregion
        #region [统计]

        #region [报装统计表]
        public static DataTable InstallStatisticsList(string where)
        {
            string sql = " select month(a.InstallTime) as y,d.Text as lb,a.CustomerName as khxm, c.Spec as xh, " +
                            "sum(b.Num) as sl,b.Price as dj,Convert(varchar(12),a.InstallTime,111) as bzsj, " +
                            "Convert(varchar(12), e.InstallTime ,111) as azsj,e.InstallName as azry,a.Tel as khdh,a.Address as khdz, " +
                            "(case when e.ReceiptType='0' then '发票' else '收据' end) as fpsj, " +
                            " b.SalesChannel as xxsqd,f.Text as fgs,  " +//后加销售渠道和分公司
                            "a.Remark as bz,(case when e.SureSatisfied='0' then '非常满意' when e.SureSatisfied='1'  " +
                            "then '满意' when e.SureSatisfied='2' then '一般' else '不满意' end) as qrkhmyd, " +
                            "(case when e.IsProContent='0' then '是' else '否' end) as sfxyhsmbnshwp, " +
                            "(case when e.IsWearClothes='0' then '是' else '否' end) as sfcgzf, " +
                            "(case when e.IsTeaching='0' then '是' else '否' end) as sfzdyhsyjzdsx, " +
                            "(case when e.IsGifts='0' then '是' else '否' end) as sfjsyhzydwp, " +
                            "(case when e.IsCharge='0' then '是' else '否' end) as sfsf, " +
                            "(case when e.IsClean='0' then '是' else '否' end) as gzwchsfzhqjgz, " +
                            "(case when e.IsUserSign='0' then '是' else '否' end) as khsfydazdbqz, " +
                            "year(a.InstallTime) as 年,b.PID from BGOI_CustomerService.dbo.tk_SHInstallR a, " +
                            "BGOI_CustomerService.dbo.tk_SHInstallR_Product b,BGOI_Inventory.dbo.tk_ProductInfo c, " +
                            "BGOI_Inventory.dbo.tk_ConfigPType d,BGOI_CustomerService.dbo.tk_SHInstall e ,BGOI_CustomerService.dbo.tk_Customerser_Config f ";
            sql += " where Sate='1' and a.BZID=b.BZID and c.PID=b.PID and c.Ptype=d.ID and a.BZID=e.BZID and a.BZCompany=f.ID " + where;
            sql += "  group by a.InstallTime,a.InstallTime,b.Num,b.PID,d.Text,c.Spec,b.Price,e.InstallTime,e.InstallName " +
                            ",a.Tel,a.Address,a.CustomerName,e.ReceiptType,a.Remark,e.SureSatisfied,e.IsProContent,e.IsWearClothes " +
                            ",e.IsTeaching,e.IsGifts,e.IsCharge,e.IsClean,e.IsUserSign ,b.SalesChannel,f.Text ";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [客户满意度统计]
        public static DataTable SatisfactionStatisticsList(string where)
        {
            string sql = " select '产品质量' as mc, " +
                            "COUNT(case when ProductQuality='0' then '非常满意' end) as cpzlfcmy, " +
                            "COUNT(case when ProductQuality='1' then '满意' end) as cpzlmy, " +
                            "COUNT(case when ProductQuality='2' then '一般' end) as cpzlyb, " +
                            "COUNT(case when ProductQuality='3' then '不满意' end) as cpzlbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            "   union all    select '产品价格' as mc, " +
                            "COUNT(case when ProductQrice='0' then '非常满意' end) as cpjzfcmy, " +
                            "COUNT(case when ProductQrice='1' then '满意' end) as cpjgmy, " +
                            "COUNT(case when ProductQrice='2' then '一般' end) as cpjgyb, " +
                            "COUNT(case when ProductQrice='3' then '不满意' end) as cpzlbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey   " + where +
                            "   union all  select '产品交货期' as mc, " +
                            "COUNT(case when ProductDelivery='0' then '非常满意' end) as cpjhqfcmy, " +
                            "COUNT(case when ProductDelivery='1' then '满意' end) as cpjhqmy, " +
                            "COUNT(case when ProductDelivery='2' then '一般' end) as cpjhqyb, " +
                            "COUNT(case when ProductDelivery='3' then '不满意' end) as cpjhqbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            " union all select '售后维修,保养服务' as mc, " +
                            "COUNT(case when CustomerServiceSurvey='0' then '非常满意' end) as shwxbyfwfcmy, " +
                            "COUNT(case when CustomerServiceSurvey='1' then '满意' end) as shwxbyfwmy, " +
                            "COUNT(case when CustomerServiceSurvey='2' then '一般' end) as shwxbyfwyb, " +
                            "COUNT(case when CustomerServiceSurvey='3' then '不满意' end) as shwxbyfwbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            "  union all   select '备品,备件供应' as mc, " +
                            "COUNT(case when SupplySurvey='0' then '非常满意' end) as bpbjgyfcmy, " +
                            "COUNT(case when SupplySurvey='1' then '满意' end) as bpbjgymy, " +
                            "COUNT(case when SupplySurvey='2' then '一般' end) as bpbjgyyb, " +
                            "COUNT(case when SupplySurvey='3' then '不满意' end) as bpbjgybmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            "  union all  select '有无漏气现象' as mc, " +
                            "COUNT(case when LeakSurvey='0' then '非常满意' end) as ywlqxxfcmy, " +
                            "COUNT(case when LeakSurvey='1' then '满意' end) as ywlqxxmy, " +
                            "COUNT(case when LeakSurvey='2' then '一般' end) as ywlqxxyb, " +
                            "COUNT(case when LeakSurvey='3' then '不满意' end) as ywlqxxbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            "   union all select '售后维修,保养服务' as mc, " +
                            "COUNT(case when AgencySales='0' then '非常满意' end) as dlshwxbyfwfcmy, " +
                            "COUNT(case when AgencySales='1' then '满意' end) as dlshwxbyfwmy, " +
                            "COUNT(case when AgencySales='2' then '一般' end) as dlshwxbyfwyb, " +
                            "COUNT(case when AgencySales='3' then '不满意' end) as dlshwxbyfwbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            " union all select '咨询，维护培训' as mc, " +
                            "COUNT(case when AgencyConsultation='0' then '非常满意' end) as zxwhpxfcmy, " +
                            "COUNT(case when AgencyConsultation='1' then '满意' end) as zxwhpxmy, " +
                            "COUNT(case when AgencyConsultation='2' then '一般' end) as zxwhpxyb, " +
                            "COUNT(case when AgencyConsultation='3' then '不满意' end) as zxwhpxbmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey  " + where +
                            " union all select '备品，备件供应' as mc, " +
                            "COUNT(case when AgencySpareParts='0' then '非常满意' end) as bpbjgyfcmy, " +
                            "COUNT(case when AgencySpareParts='1' then '满意' end) as bpbjgymy, " +
                            "COUNT(case when AgencySpareParts='2' then '一般' end) as bpbjgyyb, " +
                            "COUNT(case when AgencySpareParts='3' then '不满意' end) as bpbjgybmy " +
                            "from BGOI_CustomerService.dbo.tk_SHSurvey " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [维修任务统计]
        public static DataTable MaintenanceTaskStatisticsList(string where)
        {
            string sql = "select a.CreateTime,a.Customer,a.ContactName,a.Address,a.Tel,a.DeviceType,a.DeviceID  " +
                            ",a.EnableDate,a.GuaranteePeriod,a.RepairDate " +
                            ",b.MaintenanceTime,a.BXKNum,b.MaintenanceName,a.RepairTheUser,b.MaintenanceRecord " +
                            ",d.ProName,b.Total,a.Remark " +
                            "from BGOI_CustomerService..tk_WXRequisit a " +
                            "left join  BGOI_CustomerService..tk_WXRecord b on a.BXID=b.BXID " +
                            "left join BGOI_CustomerService..tk_WXRecord_Product c  on b.WXID=c.WXID " +
                            "left join BGOI_Inventory..tk_ProductInfo d on c.Lname=d.PID " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [设备调试统计表]
        public static DataTable TestingOfEquipmentStatisticsList(string where)
        {
            string sql = "select a.UserName,a.Tel,a.Address,(case  when a.EquManType='0' then '自管' " +
                         "when a.EquManType='1' then '厂家代管'  when a.EquManType='2' then '输配公司代管'  " +
                         "when a.EquManType='3' then '燃气施工方式代管'  else '其它公司代管' end )as  EquManType, " +
                         "c.ProName,b.Spec, (case  when a.UserType='0' then '锅炉'  " +
                         "when a.UserType='1' then '直燃机'  when a.UserType='2' then '公福'  " +
                         "when a.UserType='3' then '居民户'  else '其它' end ) as  UserType,a.DebPerson " +
                         ",b.InletPreP1,b.BleedingpreP1,b.PowerExportPreP2,b.PowerOffPrePb,b.PowerCutOffPrePq " +
                         ",b.SDExportPreP2,b.SDPowerOffPrePb,b.SDCutOffPrePq " +
                         " from  BGOI_CustomerService.dbo.tk_EquipmentDebugging a " +
                         " left join BGOI_CustomerService.dbo.tk_DebuggingSituation b on a.TRID=b.TRID " +
                         " left join BGOI_Inventory.dbo.tk_ProductInfo c on b.PID=c.PID " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        //计算总数
        public static DataTable GetTestingOfEquipmentStatistics(string where)
        {
            string sql = "select COUNT(*) as TotalNum " +
                        " from  BGOI_CustomerService.dbo.tk_EquipmentDebugging a " +
                        " left join BGOI_CustomerService.dbo.tk_DebuggingSituation b on a.TRID=b.TRID " +
                        " left join BGOI_Inventory.dbo.tk_ProductInfo c on b.PID=c.PID  " + where;
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion

        #endregion
        #region [客户服务]
        #region [客户服务]
        public static string GetTopKHID()
        {
            string strID = "";
            string strD = "KH-" + DateTime.Now.ToString("yyyyMMdd") + "-";
            string strSqlID = "select max(KHID) from BGOI_CustomerService.dbo.tk_CustomerInformation";
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
        //保存客户服务
        public static bool SaveCusService(tk_CustomerInformation record, string type, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            trans.Open("MainCustomer");
            try
            {
                if (type == "1")//添加
                {
                    string strInsert = GSqlSentence.GetInsertInfoByD<tk_CustomerInformation>(record, "BGOI_CustomerService.dbo.tk_CustomerInformation");
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
                        strErr = "添加失败";
                        return false;
                    }
                }
                else//修改
                {
                    #region [留存]
                    DataTable dt = UpSaveCusService(record.KHID);
                    tk_CustomerInformation_HIS cushis = new tk_CustomerInformation_HIS();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            cushis = new tk_CustomerInformation_HIS();
                            cushis.KHID = dr["KHID"].ToString();
                            cushis.CusName = dr["CusName"].ToString();
                            cushis.CusTel = dr["CusTel"].ToString();
                            cushis.CusAdd = dr["CusAdd"].ToString();
                            cushis.CusUnit = dr["CusUnit"].ToString();
                            cushis.CusEmail = dr["CusEmail"].ToString();
                            cushis.CreateUser = dr["CreateUser"].ToString();
                            cushis.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                            cushis.Remark = dr["Remark"].ToString();
                            cushis.Validate = dr["Validate"].ToString();
                            cushis.NCreateUser = GAccount.GetAccountInfo().UserName;
                            cushis.NCreateTime = DateTime.Now;
                        }
                        string strSHReturnVisithis = GSqlSentence.GetInsertInfoByD<tk_CustomerInformation_HIS>(cushis, "BGOI_CustomerService.dbo.tk_CustomerInformation_HIS");
                        if (trans.ExecuteNonQuery(strSHReturnVisithis, CommandType.Text, null) > 0)
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('留存客户信息','留存成功','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_CustomerInformation_HIS','" + record.KHID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                        else
                        {
                            #region [日志]
                            string strlog = "Insert into [BGOI_CustomerService].[dbo].[tk_CustomerServicelog]( LogTitle, LogContent, Person,Time,  Type, Typeid) " +
                                "values ('留存客户信息','留存失败','" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "','tk_CustomerInformation_HIS','" + record.KHID + "')";
                            SQLBase.ExecuteNonQuery(strlog);
                            #endregion
                        }
                    }
                    #endregion
                    string strUpdateList = "update BGOI_CustomerService.dbo.tk_CustomerInformation set KHID='" + record.KHID + "', CusName='" + record.CusName + "', CusTel='" + record.CusTel + "'," +
                                            " CusAdd='" + record.CusAdd + "', CusUnit='" + record.CusUnit + "', CusEmail='" + record.CusEmail + "', CreateUser='" + record.CreateUser + "'," +
                                            " CreateTime='" + record.CreateTime + "', Remark='" + record.Remark + "6', Validate='" + record.Validate + "'" +
                                            " where KHID='" + record.KHID + "'";
                    if (SQLBase.ExecuteNonQuery(strUpdateList) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "修改失败";
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                trans.Close(true);
                return false;
            }
        }
        public static UIDataTable CusServiceList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_CustomerInformation  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " CreateTime desc ";
            String strTable = " BGOI_CustomerService.dbo.tk_CustomerInformation ";
            String strField = " KHID, CusName, CusTel, CusAdd, CusUnit, CusEmail, CreateUser, CreateTime, Remark, Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }
        public static DataTable UpSaveCusService(string KHID)
        {
            string sql = "select  * from BGOI_CustomerService.dbo.tk_CustomerInformation where KHID='" + KHID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [合同]
        public static string GetNewShowCIDnew()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select CID, CidNo from tk_CIDno where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_CIDno (CID,CidNo,DateRecord) values('C',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SupplyCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["CidNo"]);
            }

            intNewID++;
            string str = "select CID, CidNo,DateRecord from tk_CIDno where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "SupplyCnn");
            strPID = dtPMaxID.Rows[0]["CID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 4);
            return strPID;
        }
        public static int InsertNewContractBasnew(ContractBas Bas, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intInsertLog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsert = "insert into tk_ContractBas (CID,ContractID,PID,Unit,BusinessType,Cname,ContractContent,CStartTime,TimeScale,CPlanEndTime,CBeginAmount,Margin,CEndAmount,CEndTime,Ctime,AmountNum,CurAmountNum,Principal,PartyA,PartyB,PayOrIncome,PageCount,Rmark,State,CreateTime,CreateUser,Validate) values ("
                + "'" + Bas.StrCID + "','" + Bas.StrContractID + "','" + Bas.StrPID + "','" + Bas.StrUnit + "','" + Bas.StrBusinessType + "','" + Bas.StrCname + "','" + Bas.StrContractContent + "','" + Bas.StrCStartTime + "','" + Bas.StrTimeScale + "','" + Bas.StrCPlanEndTime + "','" + Bas.StrCBeginAmount + "','" + Bas.StrMargin + "',"
                + "'" + Bas.StrCEndAmount + "','" + Bas.StrCEndTime + "','" + Bas.StrCtime + "','" + Bas.StrAmountNum + "','" + Bas.StrCurAmountNum + "','" + Bas.StrPrincipal + "','" + Bas.StrPartyA + "','" + Bas.StrPartyB + "','" + Bas.StrPayOrIncome + "','" + Bas.StrPageCount + "','" + Bas.StrRmark + "','" + Bas.StrState + "',"
                + "'" + Bas.StrCreateTime + "','" + Bas.StrCreateUser + "','" + Bas.StrValidate + "')";
            string strInsertLog = "insert into tk_UserLog values ('" + Bas.StrCID + "','添加合同操作','添加成功','" + DateTime.Now + "','" + account.UserName + "','合同')";

            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strInsertLog != "")
                    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
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
        public static UIDataTable getNewContractGridnew(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCHContract", CommandType.StoredProcedure, sqlPar, "MainCustomer");
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
        public static DataTable GetConfigContNew(string Type)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string strSql = "select ID,Text from BGOI_CustomerService.dbo.tk_Customerser_Config where Type = '" + Type + "' and Validate = 'v' ";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        //巡检次数
        public static DataTable ViewInspection(string CId)
        {
            string sql = "select COUNT(*) from BGOI_CustomerService.dbo.tk_PressureAdjustingInspection where Month(OperationTime) = Month(getDate()) and UserName= " +
" (select PartyA from BGOI_BasMan.dbo.tk_ContractBas where CID='" + CId + "')";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }


        //合同
        public static bool TJContract(string PID, string RelevanceID, string webkey, string createUser, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();

            Acc_Account account = GAccount.GetAccountInfo();
            string exjob = account.Exjob.ToString();
            trans.Open("MainCustomer");
            try
            {
                string strInsert = "insert into BGOI_CustomerService.dbo.tk_Approval " +
                       " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel)" +
                       "  values( '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0','经理','332','0' )";
                if (strInsert != "")
                {
                    count = SQLBase.ExecuteNonQuery(strInsert);
                }
                trans.Close(true);
                if (count > 0)
                {
                    string upstr = " update BGOI_BasMan.dbo.tk_ContractBas set [State]='1' where CID='" + RelevanceID + "' and State='0' and Validate='v' ";
                    SQLBase.ExecuteNonQuery(upstr);
                    return true;
                }
                else
                {
                    strErr = "提交失败！";
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
        //合同
        public static bool UpdateTJContract(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();

            Acc_Account account = GAccount.GetAccountInfo();
            string exjob = account.Exjob.ToString();
            string upstrnew = "";

            trans.Open("MainCustomer");
            try
            {
                if (IsPass == "是")
                {
                    #region [修改审批状态]
                    if (exjob == "总经理")
                    {
                        upstrnew = " update BGOI_BasMan.dbo.tk_ContractBas set [State]='2' where CID='" + RelevanceID + "' and State='1' and Validate='v' " +
                                   " update BGOI_CustomerService.dbo.tk_Approval set [State]='2' where PID='" + PID + "' and Validate='v'  " +
                                   " update BGOI_CustomerService.dbo.tk_Approval set [State]='2',ApprovalTime='" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' ";
                    }
                    else if (exjob == "副总经理")
                    {
                        upstrnew = " update BGOI_CustomerService.dbo.tk_Approval set [State]='1',ApprovalTime='" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' ";
                    }
                    else
                    {
                        upstrnew = " update BGOI_CustomerService.dbo.tk_Approval set [State]='1',ApprovalTime='" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' ";
                    }
                    #endregion
                    #region [提交审批]
                    string strInsert = "";
                    if (exjob == "副总经理")
                    {
                        strInsert = "insert into BGOI_CustomerService.dbo.tk_Approval " +
                                   " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel)" +
                                   "  values( '" + PID + "','" + RelevanceID + "','" + webkey + "','" + GAccount.GetAccountInfo().UserID + "','" + DateTime.Now + "','v','0','总经理','125','0' )";

                        //+ " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
                    }
                    else if (exjob == "经理")
                    {
                        strInsert = "insert into BGOI_CustomerService.dbo.tk_Approval " +
                          " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel)" +
                          "  values( '" + PID + "','" + RelevanceID + "','" + webkey + "','" + GAccount.GetAccountInfo().UserID + "','" + DateTime.Now + "','v','0','副总经理','129','0' )";
                    }
                    else
                    {
                        //总经理最大
                    }

                    if (strInsert != "")
                    {
                        count = SQLBase.ExecuteNonQuery(strInsert + upstrnew);
                    }
                    else
                    {
                        count = SQLBase.ExecuteNonQuery(upstrnew);
                    }
                    trans.Close(true);
                    #endregion
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "保存失败！";
                        return false;
                    }
                }
                else//审批未通过
                {
                    upstrnew = " update BGOI_CustomerService.dbo.tk_Approval set [State]='-1',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' " +
                               " update BGOI_CustomerService.dbo.tk_Approval set [State]='-1' where PID='" + PID + "' and Validate='v'  " +
                               " update BGOI_BasMan.dbo.tk_ContractBas set [State]='-1' where CID='" + RelevanceID + "' and State='1' and Validate='v' ";
                    count = SQLBase.ExecuteNonQuery(upstrnew);
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "保存失败！";
                        return false;
                    }
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
        //10W以下及10W审批
        public static bool UpdateTJContractSW(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string strErr)
        {
            int count = 0;
            SQLTrans trans = new SQLTrans();
            Acc_Account account = GAccount.GetAccountInfo();
            string exjob = account.Exjob.ToString();
            string upstrnew = "";
            trans.Open("MainCustomer");
            try
            {
                if (IsPass == "是")
                {
                    upstrnew = " update BGOI_BasMan.dbo.tk_ContractBas set [State]='2' where CID='" + RelevanceID + "' and State='1' and Validate='v' " +
                               " update BGOI_CustomerService.dbo.tk_Approval set [State]='2' where PID='" + PID + "' and Validate='v'  " +
                               " update BGOI_CustomerService.dbo.tk_Approval set [State]='2',ApprovalTime='" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' ";
                    count = SQLBase.ExecuteNonQuery(upstrnew);
                    trans.Close(true);

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "保存失败！";
                        return false;
                    }
                }
                else//审批未通过
                {
                    upstrnew = " update BGOI_CustomerService.dbo.tk_Approval set [State]='-1',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',Remark = '" + Remark + "' where PID='" + PID + "' and Validate='v' and Job='" + exjob + "' " +
                               " update BGOI_CustomerService.dbo.tk_Approval set [State]='-1' where PID='" + PID + "' and Validate='v'  " +
                               " update BGOI_BasMan.dbo.tk_ContractBas set [State]='-1' where CID='" + RelevanceID + "' and State='1' and Validate='v' ";
                    count = SQLBase.ExecuteNonQuery(upstrnew);
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "保存失败！";
                        return false;
                    }
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
        public static UIDataTable ConditionGridNew(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    //new SqlParameter("@Data",arr[0]),
                    //new SqlParameter("@Table",arr[1]),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getConditionNW", CommandType.StoredProcedure, sqlPar, "MainCustomer");
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
        #endregion
        #endregion
        #region [审批]
        public static UIDataTable UserAppProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count (*) from (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                                 " left join BGOI_CustomerService.dbo.tk_SHComplain_Process a on a.TSID = b.RelevanceID " +
                                 " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " (select distinct(PID),RelevanceID from BGOI_CustomerService.dbo.tk_Approval) b " +
                              " left join BGOI_CustomerService.dbo.tk_SHComplain_Process a on a.TSID = b.RelevanceID ";
            String strField = " a.State,b.PID as SPID,a.TSID,a.TSID as TSIDShow,CLID, HandleProcess, HandleState, HandleDate, CostDate, CustomerFeedback, HandleUser ";
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
                    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                    {
                        instData.DtData.Rows[r]["State"] = "待审批";
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() == "2")
                    {
                        instData.DtData.Rows[r]["State"] = "已审批";
                    }

                }
            }
            return instData;
        }

        public static UIDataTable UserContractProcessing(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count (*) from (select distinct(PID),RelevanceID, ApprovalPersons from BGOI_CustomerService.dbo.tk_Approval) b " +
                                 " left join BGOI_BasMan.dbo.tk_ContractBas a on a.CID = b.RelevanceID " +
                                  " left join BGOI_BasMan.dbo.tk_ConfigBussinessType c on a.BusinessType = c.SID and c.Type = ',55,' " +
                                  " left join BGOI_BasMan.dbo.tk_ConfigState d on a.State = d.StateId and d.Type = 'Contract' " +
                                  " left join [BJOI_UM]..UM_UnitNew z on a.Unit = z.DeptId  " +
                                  " where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = " a.CreateTime  desc ";
            String strTable = " (select distinct(PID),RelevanceID,ApprovalPersons,State from BGOI_CustomerService.dbo.tk_Approval) b " +
                              " left join BGOI_BasMan.dbo.tk_ContractBas a on a.CID = b.RelevanceID " +
                              " left join BGOI_BasMan.dbo.tk_ConfigBussinessType c on a.BusinessType = c.SID and c.Type = ',55,' " +
                              " left join BGOI_BasMan.dbo.tk_ConfigState d on a.State = d.StateId and d.Type = 'Contract' " +
                              " left join [BJOI_UM]..UM_UnitNew z on a.Unit = z.DeptId  ";
            String strField = " b.PID as SPID,c.Text as BusinessTypeDesc,d.name as StateDesc,z.DeptName, " +
                                " a.CID, a.ContractID,a.PID,a.BusinessType,a.Cname,a.ContractContent,a.PContractAmount, " +
                                " a.PBudget,a.PCost,a.PProfit, " +
                                " CONVERT(varchar(10), a.CStartTime, 23) as CStartTime,a.TimeScale,  " +
                                " CONVERT(varchar(10), a.CPlanEndTime, 23) as CPlanEndTime,a.CBeginAmount,  " +
                                " a.Margin,a.CEndAmount,CONVERT(varchar(10), a.CEndTime, 23) as CEndTime,  " +
                                " CONVERT(varchar(10), a.Ctime, 23) as Ctime,a.AmountNum,  " +
                                " a.CurAmountNum,a.Principal,a.PageCount,a.Rmark,b.State,a.PartyA,a.PartyB ";
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
                //    if (instData.DtData.Rows[r]["State"].ToString() == "1")
                //    {
                //        instData.DtData.Rows[r]["State"] = "待审批";
                //    }
                //    if (instData.DtData.Rows[r]["State"].ToString() == "2")
                //    {
                //        instData.DtData.Rows[r]["State"] = "已审批";
                //    }

                //}
            }
            return instData;
        }
        public static DataTable GetBasCus(string TSID)
        {
            string sql = "select * from BGOI_CustomerService.dbo.tk_SHComplain_Process where TSID='" + TSID + "'";
            DataTable dt = SQLBase.FillTable(sql, "MainCustomer");
            return dt;
        }
        #endregion
        #region [系统设置]
        public static UIDataTable getBasicGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from BGOI_CustomerService.dbo.tk_ConfigCustomerContent a " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = where;
            string strOrderBy = "XID ";
            String strTable = " BGOI_CustomerService.dbo.tk_ConfigCustomerContent a";
            String strField = " a.XID, a.SID, a.Text, a.Type, a.TypeDesc ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            //SqlParameter[] sqlPar = new SqlParameter[]
            //    {
            //        new SqlParameter("@PageSize",a_intPageSize.ToString()),
            //        new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
            //        new SqlParameter("@Where",where)
            //    };

            //DataSet DO_Order = SQLBase.FillDataSet("getBasic", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }

            //DataTable dtOrder = instData.Tables[0];
            //instData.IntRecords = GFun.SafeToInt32(instData.Tables[1].Rows[0][0]);
            //if (instData.IntRecords > 0)
            //{
            //    if (instData.IntRecords % a_intPageSize == 0)
            //        instData.IntTotalPages = instData.IntRecords / a_intPageSize;
            //    else
            //        instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;

            //instData.DtData = dtOrder;
            return instData;
        }

        public static System.Data.DataTable GetBasicContent()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string strSql = "select distinct Type as SID,TypeDesc as Text from BGOI_CustomerService.dbo.tk_ConfigCustomerContent where validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "MainCustomer");
            return dt;
        }
        #endregion

        #region [公共信息]
        public static UIDataTable ConfigurationInformationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select COUNT(*) from BGOI_CustomerService.dbo.tk_Customerser_Config  where " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = where;
            string strOrderBy = " ID ";
            String strTable = " BGOI_CustomerService.dbo.tk_Customerser_Config ";
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
            sqlTrans.Open("MainCustomer");
            if (type == "1")
            {
                Stype = "gongshi";
                ID = "goshi" + PreGetTaskNonew(Stype, type);
            }
            if (type == "2")
            {
                Stype = "CBMethod";
                ID = "CBMethod" + PreGetTaskNonew(Stype, type);
            }
            string strSql = "select ID,Type from BGOI_CustomerService.dbo.tk_Customerser_Config where Validate = 'v' and Text='" + text + "'";
            DataTable dt = SQLBase.FillTable(strSql, "MainCustomer");
            int a = dt.Rows.Count;
            if (dt.Rows.Count < 1)
            {
                strInsertOrder = "insert into BGOI_CustomerService.dbo.tk_Customerser_Config (ID, Text, Type,Validate) values ('" + ID + "','" + text + "','" + Stype + "','v')";
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
            string strSqlID = " select * from (select MAX(ID) IDs from BGOI_CustomerService.dbo.tk_Customerser_Config where Type='" + sel + "' ) m where m.IDs is not null and m.IDs!=''";
            DataTable dtID = SQLBase.FillTable(strSqlID, "MainCustomer");
            if (dtID != null && dtID.Rows.Count > 0)//
            {
                strID = dtID.Rows[0][0].ToString();
                int num = 0;
                if (type == "1")
                {
                    num = Convert.ToInt32(strID.Substring(5));
                }
                num = num + 1;
                xid = num.ToString();
            }
            else
            {
                //xid = "1";
                xid = "0";
            }
            return xid;
        }
        //删除
        public static int DeleteContentnew(string xid, string type, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainCustomer");
            string strInsertOrder = "update BGOI_CustomerService.dbo.tk_Customerser_Config set Validate = 'i' where ID = '" + xid + "' and Type = '" + type + "'";
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
            sqlTrans.Open("MainCustomer");

            string strInsertOrder = "update BGOI_CustomerService.dbo.tk_Customerser_Config set Text = '" + text + "' where ID = '" + xid + "' and Type = '" + type + "'";
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

        #region [待审批提示]
        public static DataTable ConSP()
        {
            int UserID = GAccount.GetAccountInfo().UserID;
            string sql = " select count(*) as num,ApprovalContent from BGOI_CustomerService.dbo.tk_Approval where ApprovalPersons='"+UserID+"' and Validate='v' and State='0' group by  ApprovalContent ";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }

        public static string GetSPid(string folderBack)
        {
            string[] arr = folderBack.Split('/');
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelPID = "select SPID, SPidNo from [" + arr[0] + "].." + arr[6] + " where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID);
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into [" + arr[0] + "].." + arr[6] + " (SPID,SPidNo,DateRecord) values('P',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID);
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["SPidNo"]);
            }

            intNewID++;
            string strUpdateID = "update [" + arr[0] + "].." + arr[6] + " set SPidNo='" + intNewID + "' where DateRecord ='" + strYMD + "'";
            SQLBase.ExecuteNonQuery(strUpdateID);

            string strSelPIDnew = "select SPID, SPidNo from [" + arr[0] + "].." + arr[6] + " where DateRecord='" + strYMD + "'";
            DataTable dtPMaxIDnew = SQLBase.FillTable(strSelPIDnew);

            strPID = dtPMaxIDnew.Rows[0]["SPID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strPID;
        }
        #endregion
    }
}

