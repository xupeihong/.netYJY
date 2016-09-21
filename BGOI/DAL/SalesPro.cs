using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;

namespace TECOCITY_BGOI
{
    public class SalesPro
    {
        #region [备案管理]

        public static string GetTopPID()
        {
            string strID = "";
            string strD = "BA" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(PID) from ProjectBasInfo";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));

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

        public static string GetNewPid()
        {
            string strPID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");

            string strSelPID = "select PID, PidNo from PIDNo where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "SalesDBCnn");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strPID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into PIDNo (PID,PidNo,DateRecord) values('BA',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SalesDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["PidNo"]);
            }

            intNewID++;
            string str = "select PID, PidNo,DateRecord from PIDNo where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "SalesDBCnn");
            strPID = dtPMaxID.Rows[0]["PID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 3);
            return strPID;
        }

        public static int CheckPlanIDandPlanName(ProjectBasInfo Project)
        {
            string str = "";
            //if (Project.PlanID != ""&&Project.PlanName=="") 
            //{
            //    str = " select PlanID  from ProjectBasInfo where validate='v' and PlanID='" + Project.PlanID + "'";
            //}
            //if(Project .PlanName!=""&&Project.PlanID==""){
            //    str = "select PlanName  from ProjectBasInfo where validate='v' and PlanName='" + Project.PlanName + "'";
            //}
            if (Project.PlanName != "" && Project.PlanID != "" && Project.BelongArea != "")
            {
                str = "select count(*)  from ProjectBasInfo where validate='v' and PlanName='" + Project.PlanName + "' and PlanID='" + Project.PlanID + "' and BelongArea='" + Project.BelongArea + "'";
            }
            //DataTable dt = SQLBase.FillTable(str,"SalesDBCnn");

            int i = GFun.SafeToInt32(SQLBase.ExecuteScalar(str, "SalesDBCnn"));
            //if (dt == null) return null;

            return i;
        }
        public static bool AddRecordInfo(string PID, Sales_RecordInfo record, List<Sales_RecordDetail> delist, string CreateUser, ref string strErr)
        {
            SQLTrans trans = new SQLTrans();
            trans.Open("SalesDBCnn");
            try
            {
                string strDel = "delete from RecordInfo where PID='" + PID + "'";
                string strInsert = GSqlSentence.GetInsertInfoByD<Sales_RecordInfo>(record, "RecordInfo");
                string strDelDetail = "delete from Record_Detail where PID='" + PID + "'";
                string strInsertList = "";
                string strDelBas = "delete from ProjectBasInfo where PID='" + PID + "'";
                string strInsertBas = "insert into ProjectBasInfo(PID,Manager,IsFilings,State,CreateTime,CreateUser,Validate) ";
                strInsertBas += "values('" + PID + "','" + CreateUser + "','y',0,'" + DateTime.Now + "','" + CreateUser + "','v')";
                if (delist.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(delist, "Record_Detail");
                }
                if (strInsert != "")
                {
                    trans.ExecuteNonQuery(strDel, CommandType.Text, null);
                    trans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                    trans.ExecuteNonQuery(strDelBas, CommandType.Text, null);
                    trans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                }
                if (strInsertList != "")
                {
                    trans.ExecuteNonQuery(strDelDetail, CommandType.Text, null);
                    trans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
                }

                UpdatePID();
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

        public static void UpdatePID()
        {
            string strNo = "select PidNo from PIDNo where DateRecord='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            string PidNo = Convert.ToString(SQLBase.ExecuteScalar(strNo, "SalesDBCnn"));
            int NewNo = int.Parse(PidNo) + 1;

            string alter = "update PIDNo set PidNo=" + NewNo + " where DateRecord='" + DateTime.Now.ToString("yyyyMMdd") + "'";
            int count = SQLBase.ExecuteNonQuery(alter, "SalesDBCnn");
        }

        public static DataTable ProjectBasInfoRelBill(string PID, ref string strErr)
        {
            try
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",PID)
                };
                DataTable dt = SQLBase.FillTable("ProjectBasInfoRelBill", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                throw;
            }
        }

        public static UIDataTable GetSalesGridInfo(int a_intPageSize, int a_intPageIndex, string strWhere, string ordercontent, string specification)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere),
                      new SqlParameter("@UnitID",unitid),
                      new SqlParameter("@LoginName",username),
                       new SqlParameter("@RoleNames",rolename),
                    new SqlParameter("@OrderContent",ordercontent),
                     new SqlParameter("@Specifications",specification)
                    
                   
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetProjectBasInfoGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
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
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["Pstate"] = GetSelectPro(instData.DtData.Rows[r]["Pstate"].ToString()).Text;

                    // instData.DtData.Rows[r]["Specifications"] = GetSelectPro(instData.DtData.Rows[r]["Specifications"].ToString()).Text;
                    //获取所在区域
                    //instData.DtData.Rows[r]["BelongArea"] = GetSelectPro(instData.DtData.Rows[r]["BelongArea"].ToString()).Text;

                    //instData.DtData.Rows[r]["ChannelsFrom"] = GetSelectPro(instData.DtData.Rows[r]["ChannelsFrom"].ToString()).Text;
                    //instData.DtData.Rows[r]["WorkChief"] = GetUnitNamePro(instData.DtData.Rows[r]["WorkChief"].ToString()).UserName;
                }
            }
            return instData;
        }
        // public static um_userNew

        public static UIDataTable GetRecordOfferInfo(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strwhere = "";
            if (!string.IsNullOrEmpty(BJID))
            {
                strwhere = "where BJID='" + BJID + "'";
            }

            string strSelCount = "select COUNT(*) " +
                                  " from OfferInfo " + strwhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "BJID='" + BJID + "'";
            string strOrderBy = "CreateTime desc";
            String strTable = " OfferInfo ";
            String strField = "BJID,XID,OrderContent,Specifications,Supplier,Unit,Amount,Total ,CreateTime,CreateUser,Validate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }

            return instData;

        }

        public static UIDataTable GetRecordOffer(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strwhere = "";
            if (!string.IsNullOrEmpty(BJID))
            {
                strwhere = "where BJID='" + BJID + "'";
            }

            string strSelCount = "select COUNT(*) " +
                                  " from Offer " + strwhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "BJID='" + BJID + "'";
            string strOrderBy = "OfferTime desc";
            String strTable = " Offer ";
            String strField = "BJID,PID,OfferUnit,OfferContacts,Approvaler1,Approvaler2,Description ,OfferTime,People,Tel,Customer,CustomerTel,CustomerPeople,Delivery,Total,State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }

            return instData;
        }

        public static UIDataTable GetOrderInfoDetailGrid(int a_intPageSize, int a_intPageIndex, string DHID)
        {
            UIDataTable instData = new UIDataTable();

            string strwhere = "";
            if (!string.IsNullOrEmpty(DHID))
            {
                strwhere = "where OrderID='" + DHID + "'";
            }
            string strSelCount = "select COUNT(*) " +
                                  " from Orders_DetailInfo " + strwhere;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "OrderID='" + DHID + "'";
            string strOrderBy = "DeliveryTime desc";
            String strTable = " Orders_DetailInfo ";
            String strField = "OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer ,OrderUnit ,OrderNum ,Price ,Subtotal ,DeliveryTime ,State  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");

            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }

            return instData;

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
        public static UIDataTable GetCheckDetail(int a_intPageSize, int a_intPageIndex)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from Record_Detail";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                if (instData.IntRecords % a_intPageSize == 0)
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize;
                else
                    instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "";
            string strOrderBy = "ID";
            String strTable = "Record_Detail";
            String strField = "PID,DeviceName , maincontent,SpecsModels ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");

            return instData;
        }
        public static UIDataTable GetOrderInfoGrid(int a_intPageSize, int a_intPageIndex, string PID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from OrdersInfo where OrderID='" + PID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "OrderID='" + PID + "'";
            string strOrderBy = "ContractDate desc";
            String strTable = "OrdersInfo";
            String strField = "PID,CustomerID,OrderID,ContractID,SalesType,ContractDate ,OrderUnit ,OrderContactor ,OrderTel ,Total,PayWay,Guarantee,State ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            return instData;
        }

        public static UIDataTable GetOrdersDetails(int a_intPageSize, int a_intPageIndex, string OID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from   ( select orderid as ID from OrdersInfo where PID='" + OID + "' union all select bjid as ID from offer where PID='" + OID + "') as dt";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "";
            string strOrderBy = "ID";
            String strTable = "( select orderid as ID from OrdersInfo where PID='" + OID + "' union all select bjid as ID from offer where PID='" + OID + "')as dt";
            String strField = "ID";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            return instData;

        }
        public static DataTable GetConfigInfo(string taskType)
        {
            string sql = "select * from ProjectSelect_Config where [Type]='" + taskType + "' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }
        public static UIDataTable GetProjectDetail(int a_intPageSize, int a_intPageIndex, string PID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from Project_Detail where PID='" + PID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "PID='" + PID + "'";
            string strOrderBy = " PurchaseDate ";
            String strTable = "Project_Detail ";
            String strField = "PID,XID,ProductID,OrderContent ,Specifications ,Unit ,Amount ,PurchaseDate ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            return instData;
        }

        public static DataTable GetListItem()
        {
            string Sql = "select ID,TEXT from ProjectSelect_Config  where Type='SpecsModels'";
            DataTable dt = SQLBase.FillTable(Sql, "SalesDBCnn");
            return dt;
        }

        public static DataTable GetBelongArea()
        {
            string sql = "select ID,TEXT from ProjectSelect_Config  where Type='Areas'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;

        }
        public static DataTable GetChannelsFrom()
        {
            string sql = "select ID,TEXT from ProjectSelect_Config  where Type='ChannelsFrom'";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            return dt;
        }

        public static bool AddProjectBaseInfo(ProjectBasInfo project, List<ProjectDetail> list, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<ProjectBasInfo>(project, "ProjectBasInfo");
                string strInsertList = "";
                string strDelBas = "delete from ProjectBasInfo where PID='" + project.PID + "'";
                string strInsertBasHIS = "insert ProjectBasInfo_HIS( PID, RecordID, UnitID, RecordDate, PlanID, PlanName, MainContent, SpecsModels, WorkChief, Constructor, Tel, BelongArea, ChannelsFrom, Remark, Manager, IsFilings, IsOffer, IsOrder, State, ShipmentsState, IsQDCompact, BackGoods, ChangeGoods, IsPay, CreateTime, CreateUser, Validate, Pstate,NCreateTime,NCreateUser)";
                strInsertBasHIS += " select PID, RecordID, UnitID, RecordDate, PlanID, PlanName, MainContent, SpecsModels, WorkChief, Constructor, Tel, BelongArea, ChannelsFrom, Remark, Manager, IsFilings, IsOffer, IsOrder, State, ShipmentsState, IsQDCompact, BackGoods, ChangeGoods, IsPay, CreateTime, CreateUser, Validate, Pstate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'  from  ProjectBasInfo";
                strInsertBasHIS += " where PID='" + project.PID + "'";
                int i = 0, j = 0;
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "project_detail");
                }
                if (strInsert != "")
                {

                    i = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strInsertBasHIS, "SalesDBCnn");
                }
                if (strInsertList != "")
                {
                    j = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                }
                if (i + j >= 2)
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
                //trans.Close(true);
                return false;
            }
        }


        public static DataTable GetRecordToExcel(string strWhere, string ordercontent, string specification)
        {
            //
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",strWhere),
                      new SqlParameter("@UnitID",unitid),
                      new SqlParameter("@LoginName",username),
                       new SqlParameter("@RoleNames",rolename),
                    new SqlParameter("@OrderContent",ordercontent),
                     new SqlParameter("@Specifications",specification)
                  };

            DataSet DO_Order = SQLBase.FillDataSet("ProjectBASInfoToExcel", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            DataTable dt = DO_Order.Tables[0];
            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    dt.Rows[r]["PState"] = GetSelectPro(dt.Rows[r]["PState"].ToString()).Text;

                    // instData.DtData.Rows[r]["Specifications"] = GetSelectPro(instData.DtData.Rows[r]["Specifications"].ToString()).Text;
                    //获取所在区域
                    //dt.Rows[r]["BelongArea"] = GetSelectPro(dt.Rows[r]["BelongArea"].ToString()).Text;

                    //dt.Rows[r]["ChannelsFrom"] = GetSelectPro(dt.Rows[r]["ChannelsFrom"].ToString()).Text;
                    //instData.DtData.Rows[r]["WorkChief"] = GetUnitNamePro(instData.DtData.Rows[r]["WorkChief"].ToString()).UserName;
                }
            }

            //
            //String strField = "select PID,recorddate,planname,planid,MainContent,SpecsModels,workchief,BelongArea,ChannelsFrom,State "
            //    + "from ProjectBasInfo " + strWhere + "order by recorddate";

            //DataTable dt = SQLBase.FillTable(strField, "SalesDBCnn");

            //if (dt != null)
            //{
            //    for (int r = 0; r < dt.Rows.Count; r++)
            //    {
            //        dt.Rows[r]["state"] = GetSelectPro(dt.Rows[r]["state"].ToString()).Text;

            //        // instData.DtData.Rows[r]["Specifications"] = GetSelectPro(instData.DtData.Rows[r]["Specifications"].ToString()).Text;
            //        //获取所在区域
            //        dt.Rows[r]["BelongArea"] = GetSelectPro(dt.Rows[r]["BelongArea"].ToString()).Text;

            //        dt.Rows[r]["ChannelsFrom"] = GetSelectPro(dt.Rows[r]["ChannelsFrom"].ToString()).Text;
            //        //instData.DtData.Rows[r]["WorkChief"] = GetUnitNamePro(instData.DtData.Rows[r]["WorkChief"].ToString()).UserName;
            //    }
            //}
            return dt;

        }

        public static ProjectBasInfo getProjectBaseInfo(string PID)
        {
            string str = "select * from ProjectBasInfo where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) { return null; }
            else
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    dt.Rows[r]["SpecsModels"] = GetSelectPro(dt.Rows[r]["SpecsModels"].ToString()).Text;
                    //dt.Rows[r]["BelongArea"] = GetSelectPro(dt.Rows[r]["BelongArea"].ToString()).Text;
                    //dt.Rows[r]["ChannelsFrom"] = GetSelectPro(dt.Rows[r]["ChannelsFrom"].ToString()).Text;
                }
            }

            ProjectBasInfo projectbaseinfo = new ProjectBasInfo();
            foreach (DataRow item in dt.Rows)
            {
                DataRowToProjectBaseInfo(item, projectbaseinfo);
            }
            return projectbaseinfo;
        }
        public static void DataRowToProjectBaseInfo(DataRow item, ProjectBasInfo projectbaseinfo)
        {
            projectbaseinfo.PID = item["PID"].ToString();
            projectbaseinfo.PlanID = item["PlanID"].ToString();
            projectbaseinfo.PlanName = item["PlanName"].ToString();
            if (item["RecordDate"] != null)
                projectbaseinfo.RecordDate = Convert.ToDateTime(item["RecordDate"]);
            projectbaseinfo.RecordID = item["RecordID"].ToString();
            projectbaseinfo.SpecsModels = item["SpecsModels"].ToString();
            projectbaseinfo.MainContent = item["MainContent"].ToString();
            projectbaseinfo.Manager = item["Manager"].ToString();
            projectbaseinfo.Pstate = item["Pstate"].ToString();
            projectbaseinfo.ShipmentsState = Convert.ToInt32(item["ShipmentsState"]);
            projectbaseinfo.UnitID = item["UnitID"].ToString();
            projectbaseinfo.WorkChief = item["WorkChief"].ToString();
            projectbaseinfo.Tel = item["Tel"].ToString();
            if (item["CreateTime"] != null)
                projectbaseinfo.CreateTime = Convert.ToDateTime(item["CreateTime"].ToString());
            projectbaseinfo.CreateUser = item["CreateUser"].ToString();
            projectbaseinfo.Validate = item["Validate"].ToString();
            projectbaseinfo.State = item["State"].ToString();
            projectbaseinfo.IsQDCompact = item["IsQDCompact"].ToString();
            projectbaseinfo.BackGoods = Convert.ToInt32(item["BackGoods"]);
            projectbaseinfo.ChangeGoods = Convert.ToInt32(item["ChangeGoods"]);
            projectbaseinfo.IsPay = item["IsPay"].ToString();
            projectbaseinfo.BelongArea = item["BelongArea"].ToString();
            projectbaseinfo.ChannelsFrom = item["ChannelsFrom"].ToString();
            projectbaseinfo.IsFilings = item["IsFilings"].ToString();
            projectbaseinfo.IsOffer = item["IsOffer"].ToString();
            projectbaseinfo.IsOrder = item["IsOrder"].ToString();
            projectbaseinfo.Remark = item["Remark"].ToString();

        }

        public static DataTable GetPrintProjectDetail(string PID)
        {
            string Str = "select * from Project_Detail where PID ='" + PID + "'";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) { return null; }
            return dt;
        }

        public static bool UPdateProjectState(string PID)
        {
            string UpdateProjectState = "update ProjectBasInfo set Pstate ='3' where PID='" + PID + "'";
            int i = 0;
            i = SQLBase.ExecuteNonQuery(UpdateProjectState, "SalesDBCnn");
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static UIDataTable GetLogGrid(string ID, int a_intPageSize, int a_intPageIndex)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT (*) from SalesLog where PID='" + ID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "  PID='" + ID + "'";
            string strOrderBy = " PID desc";
            String strTable = " saleslog";
            String strField = " PID,LogContent ,ProductType ,Actor ,Unit ,LogTime ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            //if (instData.DtData != null)
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OfferState").StateDesc;
            //        instData.DtData.Rows[r]["BelongArea"] = GetSelectPro(instData.DtData.Rows[r]["BelongArea"].ToString()).Text;
            //    }
            //}
            return instData;
        }

        #endregion

        #region [物品管理]
        public static UIDataTable GetPtype(int a_intPageSize, int a_intPageIndex)
        {
            string strSelCount = "";
            UIDataTable instData = new UIDataTable();

            strSelCount = "select count(*)  From BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " UnitID='" + GAccount.GetAccountInfo().UnitID + "'";
            string strOrderBy = " OID  ";
            String strTable = "BGOI_Inventory.dbo.tk_ConfigPType";
            String strField = "ID,Text";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);
            return instData;
        }

        public static UIDataTable ChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype, string UnitID, string PName, string PID, string Spec)
        {
            string where = "";
            if (ptype != "")
            {
                if (ptype == "PT00") { where = ""; }
                else
                {
                    where = " and Ptype='" + ptype + "'";
                }
                if (PName != "")
                {
                    where += " and a.ProName like '%" + PName + "%'";

                }
                if (PID != "")
                {
                    where += " and a.PID like '%" + PID + "%'";
                }
                if (Spec != "")
                {
                    where += " and a.Spec like '%" + Spec + "%'";
                }
            }
            else
            {

                where = "";
                if (PName != "")
                {
                    where += " and a.ProName like '%" + PName + "%'";

                }
                if (PID != "")
                {
                    where += " and a.PID like '%" + PID + "%'";
                }
                if (Spec != "")
                {
                    where += " and a.Spec like '%" + Spec + "%'";
                }

                if (ptype == "")
                {

                }
            }
            UIDataTable instData = new UIDataTable();
            //string strSelCount = " select COUNT (*) from BGOI_Inventory.dbo.tk_ProductInfo c left join  BGOI_Inventory.dbo.tk_ConfigPType pt on c.Ptype =pt.ID   where PID in (select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "')) and ProTypeID='2'" + where;
            string strSelCount = " select COUNT(*) From BGOI_Inventory.dbo.tk_ProductInfo a," +
           "(select ID,Text from  BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + UnitID + "') b where a.Ptype=b.ID and ProTypeID='2'" + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount));

            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //string strFilter = "  PID in (select ProductID from BGOI_Inventory.dbo.tk_StockRemain where HouseID in (select HouseID from BGOI_Inventory.dbo.tk_WareHouse where UnitID='" + UnitID + "'))  and ProTypeID='2' " + where;
            //string strOrderBy = " PID ";
            //String strTable = " BGOI_Inventory.dbo.tk_ProductInfo c left join  BGOI_Inventory.dbo.tk_ConfigPType pt on c.Ptype =pt.ID  ";
            //String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,Text ";
            string strFilter = " a.Ptype=b.ID and ProTypeID='2'" + where;
            string strOrderBy = " PID ";
            String strTable = " BGOI_Inventory.dbo.tk_ProductInfo a,(select ID,Text from  BGOI_Inventory.dbo.tk_ConfigPType where UnitID='" + UnitID + "') b  ";
            String strField = " PID,ProName,MaterialNum,Spec,UnitPrice,Manufacturer,b.Text ";


            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex);

            return instData;
        }

        public static DataTable GetChangePtypeList(int a_intPageSize, int a_intPageIndex, string ptype)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        public static DataTable GetBasicDetail(string PID)
        {
            string sql = "select PID as ProductID,ProName,Spec,UnitPrice,Units,Remark From BGOI_Inventory.dbo.tk_ProductInfo where PID in(" + PID + ")";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");

            return dt;
        }
        #endregion

        #region [报价管理]
        public static string GetNewBJID()
        {
            string strID = "";
            string strD = "BJ" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(BJID) from Offer";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));

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
        public static UIDataTable GetOfferGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            //string strSelCount = "select COUNT(*) " +
            //                      " from (select distinct b.BJID ,a.PID,a.PlanID  ,a.PlanName ,a.BelongArea ,a.Manager,a.IsPay,b.PID as offerPID,b.OfferTitle,b.Description,b.OfferContacts ,b.OfferUnit ,b.Total ,b.OfferTime ,b.State ,b.Ostate,b.ISF from ProjectBasInfo a right  join Offer  b on a.PID =b.PID where b.validate='v' " + strWhere + ")as s ";
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            ////if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            ////{
            ////    strWhere = strWhere.Substring(6, strWhere.Length - 6);
            ////}
            //string strFilter = " validate='v' " + strWhere;
            //string strOrderBy = " BJID desc";
            //String strTable = " (select distinct b.BJID , a.PID,a.PlanID  ,a.PlanName ,a.BelongArea ,a.Manager,a.IsPay,b.PID as offerPID,b.OfferTitle,b.Description,b.OfferContacts ,b.OfferUnit ,b.Total ,b.OfferTime ,b.State ,b.Ostate ,b.Validate,b.ISF from ProjectBasInfo a right  join Offer  b on a.PID =b.PID)as s ";
            //String strField = " BJID ,PlanID,PlanName,offerPID,OfferTitle,Description,OfferContacts ,OfferUnit ,Total,BelongArea,OfferTime ,State,OState,ISF";
            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            //// 
            //if (instData == null)
            //{
            //    instData.DtData = null;
            //    instData.IntRecords = 0;
            //    instData.IntTotalPages = 0;
            //}
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere),
                    new SqlParameter("@UnitID",unitid),
                      new SqlParameter("@LoginName",username),
                       new SqlParameter("@RoleNames",rolename)                    
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOfferGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

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


            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["OState"] = GetStatePro(instData.DtData.Rows[r]["OState"].ToString(), "OfferState").StateDesc;
                    instData.DtData.Rows[r]["BelongArea"] = GetSelectPro(instData.DtData.Rows[r]["BelongArea"].ToString()).Text;
                }
            }
            return instData;
        }

        public static UIDataTable GetSearchOfferGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            //string strSelCount = "select COUNT(*) " +
            //                      " from  (select a.PID,a.PlanID  ,a.PlanName ,a.BelongArea ,a.State ,b.OState,a.Manager,a.IsPay,b.BJID ,b.PID as offerPID,b.OfferTitle,b.Description,b.OfferContacts ,b.OfferUnit ,b.Total ,b.OfferTime ,b.State as OfferState from ProjectBasInfo a right  join Offer  b on a.PID =b.PID  where b.validate='v' " + strWhere + ")as s  ";
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            //if (!string.IsNullOrEmpty(strWhere) && strWhere != " ")
            //{
            //    //  strWhere = strWhere.Substring(6, strWhere.Length - 6);
            //}
            //string strFilter = " validate='v' " + strWhere;
            //string strOrderBy = " BJID desc ";
            //String strTable = "  (select a.PID,a.PlanID  ,a.PlanName ,a.BelongArea ,a.Manager,a.IsPay,b.BJID ,b.PID as offerPID,b.OfferTitle,b.Description,b.OfferContacts ,b.OfferUnit ,b.Total ,b.OfferTime ,b.State,b.Validate,b.OState from ProjectBasInfo a right  join Offer  b on a.PID =b.PID)as s ";
            //String strField = " BJID ,PlanID,PlanName,offerPID,OfferTitle,Description,OfferContacts ,OfferUnit ,Total ,BelongArea,OfferTime ,State,OState ";
            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            //// 
            //if (instData == null)
            //{
            //    instData.DtData = null;
            //    instData.IntRecords = 0;
            //    instData.IntTotalPages = 0;
            //}
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere),
                    new SqlParameter("@UnitID",unitid),
                      new SqlParameter("@LoginName",username),
                       new SqlParameter("@RoleNames",rolename)                    
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOfferGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

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

            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["OState"] = GetStatePro(instData.DtData.Rows[r]["OState"].ToString(), "OfferState").StateDesc;
                    instData.DtData.Rows[r]["BelongArea"] = GetSelectPro(instData.DtData.Rows[r]["BelongArea"].ToString()).Text;
                }
            }
            return instData;
        }

        public static Offer getOfferByBJID(string BJID)
        {
            string str = "select * from offer where bjid='" + BJID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            Offer offer = new Offer();
            foreach (DataRow item in dt.Rows)
            {

                DataRowToOffer(offer, item);
            }
            return offer;
        }
        public static void DataRowToOffer(Offer offer, DataRow item)
        {
            offer.BJID = item["BJID"].ToString();
            offer.PID = item["PID"].ToString();
            offer.OfferTitle = item["OfferTitle"].ToString();
            offer.OfferUnit = item["OfferUnit"].ToString();
            offer.OfferContacts = item["OfferContacts"].ToString();
            offer.Approvaler1 = item["Approvaler1"].ToString();
            offer.Approvaler2 = item["Approvaler2"].ToString();
            offer.Description = item["Description"].ToString();
            if (item["OfferTime"].ToString() != "")
                //offer.OfferTime = Convert.ToDateTime(item["OfferTime"]);
                offer.OfferTime = item["OfferTime"].ToString();
            offer.People = item["People"].ToString();
            offer.Tel = item["Tel"].ToString();
            offer.Customer = item["Customer"].ToString();
            offer.CustomerTel = item["CustomerTel"].ToString();
            offer.CustomerPeople = item["CustomerPeople"].ToString();
            offer.Delivery = item["Delivery"].ToString();
            offer.IsInvoice = item["IsInvoice"].ToString();
            offer.Validate = item["Validate"].ToString();
            offer.State = item["State"].ToString();
            offer.Payment = item["Payment"].ToString();
            offer.FKYD = item["FKYD"].ToString();
            offer.ISF = item["ISF"].ToString();
            if (!string.IsNullOrEmpty(item["Total"].ToString())) { offer.Total = Convert.ToDecimal(item["Total"]); }


            offer.CreateTime = Convert.ToDateTime(item["CreateTime"].ToString());
            offer.CreateUser = item["CreateUser"].ToString();

        }

        public static DataTable getProjectDetailGrid(string PID)
        {
            string str = "select XID ,ProductID ,OrderContent ,Specifications ,Unit ,Amount,PurchaseDate ,Remark " +
                "from Project_Detail where PID='" + PID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetOfferInfoGrid(string BJID)
        {
            string str = "select BJID,XID ,ProductID ,OrderContent ,Specifications ,Supplier,Unit ,Amount ,UintPrice ,Total,Remark " +
                "from Offerinfo where BJID='" + BJID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }
        //报价单下的物品数据
        public static UIDataTable GetOfferDetailGrid(int a_intPageSize, int a_intPageIndex, string BJID)
        {
            UIDataTable instData = new UIDataTable();
            //

            string strSelCount = "select COUNT(*) " +
                                  " from offerinfo  where BJID='" + BJID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "BJID='" + BJID + "'";
            string strOrderBy = "CreateTime ";
            String strTable = " offerinfo";
            String strField = "  BJID,XID ,ProductID ,OrderContent ,Specifications ,Supplier,Unit ,Amount ,UintPrice ,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }
        public static bool SaveOffer(Offer offer, List<OfferInfo> list, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<Offer>(offer, "BGOI_Sales.dbo.Offer");
                string strInsertList = "";
                //  string strDelBas = "delete from ProjectBasInfo where PID='" + offer.PID + "'";
                //string strInsertBas = "insert into ProjectBasInfo(PID,Manager,IsFilings,State,CreateTime,CreateUser,Validate) ";
                //strInsertBas += "values('" + PID + "','" + CreateUser + "','y',0,'" + DateTime.Now + "','" + CreateUser + "','v')";
                string UpdateProBas = "update ProjectBasInfo set Pstate ='2' where PID ='" + offer.PID + "'";
                string InsertProBasHIS = "insert into ProjectBasInfo_HIS (PID, RecordID, UnitID, RecordDate, PlanID, PlanName, MainContent, SpecsModels, WorkChief, Constructor, Tel, BelongArea, ChannelsFrom, Remark, Manager, IsFilings, IsOffer, IsOrder, State, ShipmentsState, IsQDCompact, BackGoods, ChangeGoods, IsPay, CreateTime, CreateUser, Validate, Pstate, NCreateTime, NCreateUser)" +
                "select PID, RecordID, UnitID, RecordDate, PlanID, PlanName, MainContent, SpecsModels, WorkChief, Constructor, Tel, BelongArea, ChannelsFrom, Remark, Manager, IsFilings, IsOffer, IsOrder, State, ShipmentsState, IsQDCompact, BackGoods, ChangeGoods, IsPay, CreateTime, CreateUser, Validate, Pstate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ProjectBasInfo where PID='" + offer.PID + "'";
                int i = 0, P = 0, H = 0;
                int j = 0;
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_Sales.dbo.offerinfo");
                }
                if (strInsert != "")
                {
                    i = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
                    P = SQLBase.ExecuteNonQuery(UpdateProBas, "SalesDBCnn");
                    H = SQLBase.ExecuteNonQuery(InsertProBasHIS, "SalesDBCnn");
                }
                if (strInsertList != "")
                {
                    j = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                }
                if (i + j >= 2)
                {

                    return true;
                }
                else { return false; }

                //trans.Close(true);
                //return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                //trans.Close(true);
                return false;
            }
        }

        public static bool SaveUpdateOffer(Offer offer, List<OfferInfo> list, ref string strErr)
        {
            try
            {
                int i = 0, j = 0, m = 0, n = 0;
                if (offer != null)
                {
                    string str = "update offer set OfferTitle='" + offer.OfferTitle + "',OfferTime='" + offer.OfferTime + "',Description='" + offer.Description + "',FKYD='" + offer.FKYD + "',CustomerTel='" + offer.CustomerTel + "',Customer='" + offer.Customer + "',OfferContacts='" + offer.OfferContacts + "'"
                        + " where BJID='" + offer.BJID + "'";
                    string InserOfferHIS = "insert into Offer_HIS( BJID,PID,OfferTitle,OfferUnit,OfferContacts ,State ,CreateTime ,CreateUser,Validate ,Approvaler1,"
                    + "Approvaler2,Description ,OfferTime ,People ,Tel ,CuStomer,CustomerTel ,CustomerPeople,Delivery ,IsInvoice ,Payment ,FKYD ,Total ,OState,NCreateTime,NCreateUser)"
                    + "select BJID,PID,OfferTitle,OfferUnit,OfferContacts ,State ,CreateTime ,CreateUser,Validate ,Approvaler1,"
                    + "Approvaler2,Description ,OfferTime ,People ,Tel ,CuStomer,CustomerTel ,CustomerPeople,Delivery ,IsInvoice ,Payment ,FKYD ,Total,OState,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from Offer  where BJID='" + offer.BJID + "'";

                    n = SQLBase.ExecuteNonQuery(InserOfferHIS, "SalesDBCnn");
                    i = SQLBase.ExecuteNonQuery(str, "SalesDBCnn");
                }
                if (list != null)
                {
                    string strDelete = "delete from OfferInfo where BJID ='" + offer.BJID + "'";
                    string strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_Sales.dbo.offerinfo");
                    SQLBase.ExecuteNonQuery(strDelete, "SalesDBCnn");
                    j = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                    foreach (OfferInfo item in list)
                    {
                        string strInsertInfoHIS = "insert into OfferInfo_HIS(BJID,XID,ProductID ,OrderContent ,SpeCifications ,Supplier ,Unit ,Amount ,UintPrice ,Total ,CreateTime ,CreateUser ,Validate ,NCreateTime,NCreateUser)"
                    + "select  BJID,XID,ProductID ,OrderContent ,SpeCifications ,Supplier ,Unit ,Amount ,UintPrice ,Total ,CreateTime ,CreateUser ,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from OfferInfo where XID='" + item.XID + "'";
                        SQLBase.ExecuteNonQuery(strInsertInfoHIS, "SalesDBCnn");
                    }

                    if (i + j + n >= 3)
                    {
                        return true;
                    }
                    else
                    {
                        strErr = "修改失败";
                        return false;
                    }
                    #region OlD
                    //foreach (OfferInfo item in list)
                    //{
                    //    string strOfferinfo = "";
                    //    #region Old
                    //    //if (item.XID == "add")
                    //    //{
                    //    //    string strBigXID = "select XID from OfferInfo where BJID='" + item.BJID + "' order by XID desc";
                    //    //    DataTable dt = SQLBase.FillTable(strBigXID, "SalesDBCnn");
                    //    //    if (dt.Rows.Count == 0) { item.XID = item.BJID + string.Format("{0:D3}", 1); }
                    //    //    else
                    //    //    {
                    //    //        string str3 = dt.Rows[0]["XID"].ToString();
                    //    //        str3 = str3.Substring(str3.Length - 3, 3);
                    //    //        int s = Convert.ToInt32(str3);
                    //    //        item.XID = item.BJID + string.Format("{0:D3}", s + 1);
                    //    //    }
                    //    //    strOfferinfo = "insert OfferInfo(BJID ,XID,ProductID ,OrderContent,SpeCifications ,Supplier,Unit,Amount,UintPrice ,Total,CreateTime,CreateUser,Validate,Remark)" +
                    //    //        " values('" + item.BJID + "','" + item.XID + "','" + item.ProductID + "','" + item.OrderContent + "','" + item.Specifications + "','" + item.Supplier + "','" + item.Unit + "','" + item.Amount + "','" + item.UintPrice + "','" + item.Total + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','v','" + item.Remark + "') ";
                    //    //}
                    //    //else
                    //    //{
                    //    //    strOfferinfo = "update offerinfo set Supplier='" + item.Supplier + "',Amount=" + item.Amount + ",UintPrice='" + item.UintPrice + "',Total='" + item.Total + "'"
                    //    // + "where XID='" + item.XID + "'";
                    //    //} 
                    //    #endregion

                    //    string strInsertInfoHIS = "insert into OfferInfo_HIS(BJID,XID,ProductID ,OrderContent ,SpeCifications ,Supplier ,Unit ,Amount ,UintPrice ,Total ,CreateTime ,CreateUser ,Validate ,NCreateTime,NCreateUser)"
                    //    + "select  BJID,XID,ProductID ,OrderContent ,SpeCifications ,Supplier ,Unit ,Amount ,UintPrice ,Total ,CreateTime ,CreateUser ,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from OfferInfo where XID='" + item.XID + "'";
                    //    SQLBase.ExecuteNonQuery(strInsertInfoHIS, "SalesDBCnn");
                    //    int j = SQLBase.ExecuteNonQuery(strOfferinfo, "SalesDBCnn");
                    //} 
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {

                strErr = ex.Message;
                return false;
            }

        }

        public static UIDataTable GetBJOrders(int a_intPageSize, int a_intPageIndex, string PID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from  OrdersInfo where PID='" + PID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = "PID='" + PID + "'";
            string strOrderBy = "OrderID";
            String strTable = "OrdersInfo";
            String strField = "OrderID";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            return instData;
        }
        public static bool CancelOffer(string ID, ref string strErr)
        {
            string str = "update offer set Validate='i' where BJID='" + ID + "'";
            try
            {
                int i = SQLBase.ExecuteNonQuery(str, "SalesDBCnn");
                if (i <= 0) { strErr = "撤销失败"; return false; }
            }
            catch (Exception e)
            {
                strErr = e.Message;
                throw;
            }


            return true;
        }


        public static DataTable GetOfferToExcel(string where, ref string strErr)
        {
            //if (!string.IsNullOrEmpty(where))
            //{
            //    where += "where " + where;
            //}
            string str = "select  BJID ,offerPID,OfferTitle,Description,OfferContacts ,OfferUnit ,Total ,OfferTime ,offerState" +
                " from (select a.PID,a.PlanID  ,a.PlanName ,a.BelongArea ,a.State ,a.Manager,a.IsPay,b.BJID ,b.PID as offerPID,b.OfferTitle,b.Description," +
                "b.OfferContacts ,b.OfferUnit ,b.Total ,b.OfferTime ,b.State as OfferState from ProjectBasInfo a right  join Offer  b on a.PID =b.PID  where b.validate='v')as s " + where
                + " order by OfferTime desc";

            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    dt.Rows[r]["offerState"] = GetStatePro(dt.Rows[r]["offerState"].ToString(), "OfferState").StateDesc;
                    // instData.DtData.Rows[r]["Specifications"] = GetSelectPro(instData.DtData.Rows[r]["Specifications"].ToString()).Text;
                    //获取所在区域

                }
            }
            return dt;
        }


        // 上传附件 修改后 
        public static int InsertBiddingNew(tk_CFile fileUp, HttpFileCollection Filedata, ref string a_strErr)
        {
            a_strErr = "";
            string savePaths = "";
            int intlog = 0;
            int intInsertFile = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SalesDBCnn");
            //
            string FileName = "";
            string savePath = "";
            string filename = Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;
            //
            string path = System.Configuration.ConfigurationSettings.AppSettings["SalesOfferupload"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos + "\\" + fileUp.CID;
            path = path + savePaths;
            if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
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

                    strInsertFile = "insert into tk_CFile (CID,FileType,FileName,FileInfo,CreateUser,CreateTime,Validate) ";
                    strInsertFile += " values ('" + fileUp.CID + "','" + fileUp.FileType + "','" + FileName + "','" + savePaths + "','"
                     + fileUp.CreateUser + "','" + fileUp.CreateTime + "','" + fileUp.Validate + "')";
                    intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                }
                else// 有同名文件 
                {
                    string strUpdate = "";
                    string strSel = " select count(*) from tk_CFile where CID='" + fileUp.CID + "' and FileName='" + FileName
                    + "' and FileType='" + fileUp.FileType + "' and FileInfo='" + savePaths + "' and CreateUser='" + account.UserName + "'   and Validate='v' ";
                    int count = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
                    if (count > 0)// 存在同一阶段同名的文件 则覆盖
                    {
                        savePath = Path.Combine(path, FileName);
                        Filedata[0].SaveAs(savePath);

                        strUpdate = " update tk_CFile set Validate='i' where CID='" + fileUp.CID + "' and FileName='" + FileName
                            + "' and FileType='" + fileUp.FileType + "' and Validate='v' ";
                        sqlTrans.ExecuteNonQuery(strUpdate);
                        //Comments,
                        strInsertFile = "insert into tk_CFile (CID,FileType,FileName,FileInfo,CreateUser,CreateTime,Validate) ";
                        strInsertFile += " values ('" + fileUp.CID + "','" + fileUp.FileType + "','" + FileName + "','" + savePaths + "','"
                            + fileUp.CreateUser + "','" + fileUp.CreateTime + "','" + fileUp.Validate + "')";
                        intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                    }
                }
            }

            Sales_SalesLog salesLog = new Sales_SalesLog();
            salesLog.LogTime = DateTime.Now.ToString();
            salesLog.PID = fileUp.CID;
            salesLog.LogContent = "上传附件";
            salesLog.ProductType = "上传成功";
            salesLog.Actor = GAccount.GetAccountInfo().UserName;
            salesLog.Unit = GAccount.GetAccountInfo().UnitName;
            salesLog.SignTime = DateTime.Now.ToString();
            salesLog.SalesType = "Project";
            //SaveLog(salesLog);
            //log.PID = fileUp.StrRID;
            //log. = "上传附件";
            //log.strLogContent = "上传附件成功";
            //log.strLogPerson = account.UserName;
            //log.strLogTime = DateTime.Now;
            //log.strType = "上传附件";

            try
            {
                SalesRetailPro.SaveLog(salesLog);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                return -1;
            }

            return intInsertFile;
        }

        //获取报价的文件
        public static UIDataTable GetUploadFileGrid(int a_intPageSize, int a_intPageIndex, string CID)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from  tk_CFile where validate='v' and CID='" + CID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            string strFilter = " validate='v' and CID='" + CID + "'";
            string strOrderBy = "ID";
            String strTable = "tk_CFile";
            String strField = " ID,CID,FileInfo ,FileName ,CreateTime ,CreateUser,Validate";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            return instData;
        }

        #region [获取供应商]
        public static DataTable GetSupplier(string SID)
        {
            string str = "select a.SID,SupplierType,COMNameC ,SupplierCode ,b.Price from BGOI_BasMan .dbo.tk_SupplierBas a inner join BGOI_BasMan .dbo.tk_SProducts b on a.SID =b.SID where a.SID='" + SID + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt == null) return null;
            return dt;
        }
        //获取供应商的类别
        public static UIDataTable GetSupType(int a_intPageSize, int a_intPageIndex)
        {
            try
            {
                string strSelCount = "";
                UIDataTable instData = new UIDataTable();

                strSelCount = "select count(*)  From tk_ConfigSupType ";
                instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SupplyCnn"));

                if (instData.IntRecords > 0)
                {
                    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
                }
                else
                    instData.IntTotalPages = 0;
                string strFilter = "";
                string strOrderBy = " Suid  ";
                String strTable = "BGOI_BasMan .dbo.tk_ConfigSupType ";
                String strField = "Suid,SupplierType";
                instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SupplyCnn");
                return instData;
            }
            catch (Exception)
            {
                //   strErr = ex.Message;
                throw;
            }

        }
        public static UIDataTable GetCheckSupList(int a_intPageSize, int a_intPageIndex, string ptype)
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
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SupplyCnn"));

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

            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SupplyCnn");

            return instData;
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

        #endregion

        #region [订单管理]
        public static string GetNewOrderID()
        {
            string strID = "";
            string strD = "DH" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(OrderID) from OrdersInfo";
            ///
            //string strSqlID = "select OrderID from OrdersInfo order by  right(right(OrderID,3),3) desc";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));

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

        public static DataTable GetUM_USER(string DeptID)
        {
            // string sql = "select * from um_userNew where DeptId ='" + DeptID + "' and roleNames ='销售人员' ";
            // string sql = "select * from um_userNew where DeptId ='" + DeptID + "'";//160412
            string sql = "select * from um_userNew where UserId in('181','323','325','326','331','333','334','350','352','322')";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");

            return dt;
        }

        public static DataTable GetProject(string ID)
        {
            string str = "select PID ,PlanID ,PlanName from ProjectBasInfo where PID='" + ID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static bool SaveOrderInfo(OrdersInfo ordersinfo, List<Orders_DetailInfo> list, ref string strErr)
        {
            try
            {
                //   SQLTrans sqlTrans = new SQLTrans();
                // int intInsert = 0;
                //int j = 0;
                // sqlTrans.Open();
                string strInsert = GSqlSentence.GetInsertInfoByD<OrdersInfo>(ordersinfo, "OrdersInfo");
                string strInsertList = "";
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "Orders_DetailInfo");
                }
                if (strInsert != "")
                {
                    SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
                    // intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);


                }

                if (strInsertList != "")
                {
                    SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                    //j = sqlTrans.ExecuteNonQuery(strInsertList, CommandType.Text, null);
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

        public static UIDataTable GetOrderInfo(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //string strSelCount = "select COUNT(*) from(select  a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.OrderTel,a.OrderAddress,a.UseUnit,a.Total,DebtAmount=(a.Total-ISnull((select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),0)),HKAmount=(select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),a.UseContactor,a.UseTel,a.UseAddress,a.IsHK,a.State,a.Ostate,a.Ostate as OOstate,a.ISF,a.ISHT,a.EXState from  OrdersInfo " + "a  Where a.SalesType='Sa01' and Validate ='v' " + strWhere + ")as s";
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            ////if (!string.IsNullOrEmpty(strWhere))
            ////{
            ////    strWhere = strWhere.Substring(6, strWhere.Length - 6);
            ////}
            //string strFilter = " SalesType='Sa01' and Validate ='v' " + strWhere;
            //string strOrderBy = " a.CreateTime desc ";
            //String strTable = " OrdersInfo a  ";
            //String strField = "   a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.OrderTel,a.OrderAddress,a.UseUnit,a.Total,DebtAmount=(a.Total-ISnull((select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),0)),HKAmount=(select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),a.UseContactor,a.UseTel,a.UseAddress,a.IsHK,a.State,a.Ostate,a.Ostate as OOstate,a.ISF,a.ISHT,a.EXState";
            //instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            ////
            //  UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere),
                    new SqlParameter("@UnitID",unitid),
                      new SqlParameter("@LoginName",username),
                       new SqlParameter("@RoleNames",rolename)                    
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOrdersInfoGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

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
            //if (instData.DtData != null)
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
            //    }
            //}
            // 
            //if (instData == null)
            //{
            //    instData.DtData = null;
            //    instData.IntRecords = 0;
            //    instData.IntTotalPages = 0;
            //}
            if (instData.DtData != null)
            {
                for (int r = 0; r < instData.DtData.Rows.Count; r++)
                {
                    instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
                }
            }
            return instData;
        }


        public static DataTable GetOrdersDetail(string OrderID)
        {
            string Str = "select DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
                + "OrderNum,Price,Subtotal,Technology,UnitCost ,TotalCost ,SaleNo ,ProjectNO ,JNAME  from Orders_DetailInfo where OrderID='" + OrderID + "' and Validate ='v' ";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetOredersShipmentDetail(string OrderID)
        {
            string Str = "select DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,(ordernum-shipmentnum )OrderNum,Price,Subtotal,Technology,DeliveryTime ,shipmentnum from Orders_DetailInfo where  (ordernum-shipmentnum)>0 and orderid='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetOrdersDetailBYDID(string DID)
        {
            string Str = "select  DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,ActualAmount,Price,ActualSubTotal,Technology,DeliveryTime from Orders_DetailInfo where DID in (" + DID + ")";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static OrdersInfo GetOrdersByOrderID(string OrderID)
        {
            string str = "select * from OrdersInfo where OrderID='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            OrdersInfo OrderInfo = new OrdersInfo();

            foreach (DataRow item in dt.Rows)
            {
                DataRowToOrderInfo(item, OrderInfo);
            }
            return OrderInfo;
        }


        public static string GetOrdersDetailDID(string OrderID)
        {
            string str = "select DID from Orders_DetailInfo where OrderID ='" + OrderID + "' order by DID desc";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            string s = "";
            if (dt.Rows.Count > 0) s = dt.Rows[0]["DID"].ToString();
            return s;
        }

        public static void DataRowToOrderInfo(DataRow item, OrdersInfo orderinfo)
        {
            orderinfo.OrderID = item["OrderID"].ToString();
            orderinfo.PID = item["PID"].ToString();
            orderinfo.UnitID = item["UnitID"].ToString();
            orderinfo.ContractID = item["ContractID"].ToString();
            orderinfo.SalesType = item["SalesType"].ToString();
            if (item["ContractDate"] != "")
            {
                orderinfo.ContractDate = Convert.ToDateTime(item["ContractDate"]);
            }
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
            orderinfo.ISF = item["ISF"].ToString();
            if (item["ISFinish"].ToString() != "") orderinfo.ISFinish = Convert.ToInt32(item["ISFinish"]);
            orderinfo.ISHT = item["ISHT"].ToString();
            orderinfo.ExpectedReturnDate = item["ExpectedReturnDate"].ToString();
            orderinfo.ChannelsFrom = item["ChannelsFrom"].ToString();
            orderinfo.DeliveryTime = item["DeliveryTime"].ToString();
            orderinfo.ISHT = item["ISHT"].ToString();
            if (item["OrderActualAmount"] != "")
            {
                orderinfo.OrderActualAmount = Convert.ToInt32(item["OrderActualAmount"]);
            }
            if (item["OrderActualTotal"] != "")
            {
                orderinfo.OrderActualTotal = Convert.ToDecimal(item["OrderActualTotal"]);
            }
            if (item["OrderAmount"] != "")
            {
                orderinfo.OrderAmount = Convert.ToInt32(item["OrderAmount"]);
            }
            if (item["OrderTotal"] != "")
            {
                orderinfo.OrderTotal = Convert.ToDecimal(item["OrderTotal"]);
            } if (item["Pstate"] != "")
            {
                orderinfo.Pstate = Convert.ToInt32(item["Pstate"]);
            }
            orderinfo.EXState = item["EXState"].ToString();
        }

        public static bool UpdateOrderInfo(OrdersInfo ordersinfo, List<Orders_DetailInfo> list, ref string strErr)
        {
            try
            {
                //string strUpdate = "update OrdersInfo set OrderUnit=@OrderUnit,OrderContactor=@OrderContactor,OrderTel=@OrderTel," +
                //    "OrderAddress=@OrderAddress,UseUnit=@UseUnit,UseContactor=@UseContactor,UseTel=@UseTel,UseAddress=@UseAddress," +
                //    "OrderActualTotal=@OrderActualTotal,PayWay=@PayWay,Guarantee=@Guarantee,Provider=@Provider,ProvidManager=@ProvidManager," +
                //    "Demand=@Demand,DemandManager=@DemandManager,Remark=@Remark,ChannelsFrom=@ChannelsFrom,DeliveryTime=@DeliveryTime,ExpectedReturnDate=@ExpectedReturnDate where OrderID=@OrderID";

                string strUpdate = "update OrdersInfo set OrderUnit='" + ordersinfo.OrderUnit + "',OrderContactor='" + ordersinfo.OrderContactor + "',OrderTel='" + ordersinfo.OrderTel + "'," + "OrderAddress='" + ordersinfo.OrderAddress + "',UseUnit='" + ordersinfo.UseUnit + "',UseContactor='" + ordersinfo.UseContactor + "',UseTel='" + ordersinfo.UseTel + "',UseAddress='" + ordersinfo.UseAddress + "'," + "OrderActualTotal='" + ordersinfo.OrderActualTotal + "',PayWay='" + ordersinfo.PayWay + "',Guarantee='" + ordersinfo.Guarantee + "',Provider='" + ordersinfo.Provider + "',ProvidManager='" + ordersinfo.ProvidManager + "'," + "Demand='" + ordersinfo.Demand + "',DemandManager='" + ordersinfo.DemandManager + "',Remark=" + ordersinfo.Remark + ",ChannelsFrom='" + ordersinfo.ChannelsFrom + "',DeliveryTime='" + ordersinfo.DeliveryTime + "',ExpectedReturnDate='" + ordersinfo.ExpectedReturnDate + "' where OrderID='" + ordersinfo.OrderID + "'";


                SqlParameter[] param ={new SqlParameter ("@OrderUnit",SqlDbType .NVarChar),
                                       new SqlParameter ("@OrderContactor",SqlDbType .NVarChar  ),
                                       new SqlParameter ("@OrderTel",SqlDbType .VarChar ),
                                       new SqlParameter ("@OrderAddress",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseUnit",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseContactor",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseTel",SqlDbType .VarChar ),
                                       new SqlParameter ("@UseAddress",SqlDbType .VarChar ),
                                       new SqlParameter ("@OrderActualTotal",SqlDbType .VarChar ),
                                       new SqlParameter ("@PayWay",SqlDbType .NVarChar ),
                                       new SqlParameter ("@Guarantee",SqlDbType .VarChar ),
                                       new SqlParameter ("@Provider",SqlDbType .VarChar ),
                                       new SqlParameter ("@ProvidManager",SqlDbType .VarChar ),
                                       new SqlParameter ("@Demand",SqlDbType .VarChar ),
                                       new SqlParameter ("@DemandManager",SqlDbType .VarChar ),
                                       new SqlParameter ("@Remark",SqlDbType .VarChar ),
                                       new SqlParameter ("@OrderID",SqlDbType .VarChar ),
                                       new SqlParameter("@ChannelsFrom",SqlDbType.VarChar),
                                       new SqlParameter ("@DeliveryTime",SqlDbType .DateTime ),
                                       new SqlParameter("@ExpectedReturnDate",SqlDbType .DateTime),
                                       
                                       
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
                if (ordersinfo.OrderActualTotal == null) { param[8].Value = ""; }
                else { param[8].Value = ordersinfo.OrderActualTotal; }
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
                else
                {
                    param[16].Value = ordersinfo.OrderID;
                }
                if (ordersinfo.ChannelsFrom == null) { param[17].Value = ""; }
                else { param[17].Value = ordersinfo.ChannelsFrom; }

                if (ordersinfo.DeliveryTime == "") { param[18].Value = ""; }
                else { param[18].Value = ordersinfo.DeliveryTime; }

                if (ordersinfo.ExpectedReturnDate == "") { param[19].Value = ""; }
                else { param[19].Value = ordersinfo.ExpectedReturnDate; }
                string InserNewOrdersHIS = "insert into OrdersInfo_HIS (PID,UnitID,OrderID,ContractID,SalesType,ContractDate,OrderUnit,OrderContactor,OrderTel,OrderAddress,UseUnit,UseContactor,UseTel," +
                "UseAddress,Total,PayWay,Guarantee,Provider,ProvidManager,Demand,DemandManager,Remark,IsBranch,IsPriceRules,IsManager,State," +
                "CreateTime,CreateUser,Validate,OState,NCreateTime ,NCreateUser)" +
                "select PID,UnitID,OrderID,ContractID,SalesType,ContractDate,OrderUnit,OrderContactor,OrderTel,OrderAddress,UseUnit,UseContactor,UseTel," +
                "UseAddress,Total,PayWay,Guarantee,Provider,ProvidManager,Demand,DemandManager,Remark,IsBranch,IsPriceRules,IsManager,State," +
                "CreateTime,CreateUser,Validate,OState,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from OrdersInfo where OrderID ='" + ordersinfo.OrderID + "'";
                // string strUpdateList = "";
                string strInsertDetailHIS = "";
                int i = 0, j = 0, m = 0, n = 0;
                string detailHis = "select * from Orders_DetailInfo where OrderID='" + ordersinfo.OrderID + "'";
                DataTable dtHIS = SQLBase.FillTable(detailHis, "SalesDBCnn");
                if (dtHIS.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHIS.Rows)
                    {
                        strInsertDetailHIS = "insert into Orders_DetailInfo_HIS(PID,OrderID,DID,ProductID,OrderContent,SpecsModels," +
                             " Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,NCreateUser,NCreateTime)" +
                             "select PID,OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                             " from Orders_DetailInfo where DID='" + item["OrderID"] + "'";
                        SQLBase.ExecuteNonQuery(strInsertDetailHIS, "SalesDBCnn");
                    }
                }
                string strDelete = "delete from Orders_DetailInfo where OrderID='" + ordersinfo.OrderID + "'";
                i = SQLBase.ExecuteNonQuery(strDelete, "SalesDBCnn");
                if (list.Count > 0)
                {
                    string strInsertList = GSqlSentence.GetInsertByList(list, "Orders_DetailInfo");
                    j = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");



                    #region OLD
                    //foreach (Orders_DetailInfo item in list)
                    //{
                    //    string strUpdateList = "";
                    //    if (item.DID == "add")
                    //    {
                    //        string strBigDID = "select * from Orders_DetailInfo where OrderID='" + item.OrderID + "' order by DID desc";
                    //        DataTable dt = SQLBase.FillTable(strBigDID, "SalesDBCnn");
                    //        if (dt.Rows.Count == 0) { item.DID = item.OrderID + string.Format("{0:D3}", 1); }
                    //        else
                    //        {
                    //            string str3 = dt.Rows[0]["DID"].ToString();
                    //            str3 = str3.Substring(str3.Length - 3, 3);
                    //            int s = Convert.ToInt32(str3);
                    //            item.DID = item.OrderID + string.Format("{0:D3}", s + 1);
                    //        }
                    //        strUpdateList = "insert Orders_DetailInfo(PID,OrderID ,DID,ProductID ,OrderContent,SpecsModels ,Manufacturer,OrderUnit,OrderNum,UnitPrice ,Subtotal,CreateTime,CreateUser,Validate,Remark)" +
                    //            " values('" + item.PID + "','" + item.OrderID + "','" + item.DID + "','" + item.ProductID + "','" + item.OrderContent + "','" + item.SpecsModels + "','" + item.Manufacturer + "','" + item.OrderUnit + "','" + item.OrderNum + "','" + item.UnitPrice + "','" + item.Subtotal + "','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','v','" + item.Remark + "') ";
                    //    }
                    //    else
                    //    {
                    //        strUpdateList = "update Orders_DetailInfo set ProductID='" + item.ProductID + "', DID='" + item.DID + "'," +
                    //           "OrderContent='" + item.OrderContent + "',SpecsModels='" + item.SpecsModels + "',Manufacturer='" + item.Manufacturer + "'," +
                    //           "OrderUnit='" + item.OrderUnit + "',OrderNum=" + item.OrderNum + ",Price=" + item.Price + ",Subtotal=" + item.Subtotal + "," +
                    //               "Technology='" + item.Technology + "',DeliveryTime='" + item.DeliveryTime + "' where DID='" + item.DID + "'";
                    //    }

                    //    strInsertDetailHIS = "insert into Orders_DetailInfo_HIS(PID,OrderID,DID,ProductID,OrderContent,SpecsModels," +
                    //          " Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,NCreateUser,NCreateTime)" +
                    //          "select PID,OrderID,DID,ProductID,OrderContent,SpecsModels,Manufacturer,OrderUnit,OrderNum,Price,Subtotal,Technology,DeliveryTime,State,Remark,Validate,CreateTime,CreateUser,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'" +
                    //          " from Orders_DetailInfo where DID='" + item.DID + "'";
                    //    if (strUpdateList != "")
                    //    {
                    //        SQLBase.ExecuteNonQuery(strInsertDetailHIS, "SalesDBCnn");
                    //        SQLBase.ExecuteNonQuery(strUpdateList, "SalesDBCnn");

                    //    }
                    //} 
                    #endregion
                }

                //string strDelete1 = "delete from OrdersInfo where OrderID='" + ordersinfo.OrderID + "'";
                //i = SQLBase.ExecuteNonQuery(strDelete1, "SalesDBCnn");
                //string strInsertList1 = GSqlSentence.GetUpdateInfo(ordersinfo);
                //if (strInsertList1 != "")
                //{
                //    m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "SalesDBCnn");
                //    n = SQLBase.ExecuteNonQuery(strInsertList1, "SalesDBCnn");
                //}
                if (strUpdate != "")
                {
                    m = SQLBase.ExecuteNonQuery(InserNewOrdersHIS, "SalesDBCnn");
                    // n = SQLBase.ExecuteNonQuery(strUpdate, param, "SalesDBCnn");
                    n = SQLBase.ExecuteNonQuery(strUpdate, "SalesDBCnn");
                }

                if (m + n >= 2)
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


        public static bool CanCelOrdersInfo(string OrderID, ref string strErr)
        {
            string strCancel = " update OrdersInfo set Validate ='i' where OrderID ='" + OrderID + "'";
            try
            {
                int i = SQLBase.ExecuteNonQuery(strCancel, "SalesDBCnn");
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

        public static UIDataTable LoadOrderDetail(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from Orders_DetailInfo where OrderID ='" + OrderID + "' and Validate ='v'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "OrderID='" + OrderID + "' and Validate ='v'";
            string strOrderBy = "DeliveryTime ";
            String strTable = " Orders_DetailInfo";
            String strField = " PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer ," +
"OrderUnit ,OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,ActualAmount,ActualSubTotal,Technology ,DeliveryTime ,State ,Remark ";
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

        public static UIDataTable LoadOrderBill(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from Shipments where OrderID ='" + OrderID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "OrderID='" + OrderID + "'";
            string strOrderBy = "ShipmentDate ";
            String strTable = " Shipments";
            String strField = " OrderID ,ShipGoodsID ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }


        public static DataTable GetOrderInfoToExcel(string where, ref string strErr)
        {

            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            string rolename = GAccount.GetAccountInfo().RoleNames;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where),
                    new SqlParameter("@UnitID",unitid),
                    new SqlParameter("@LoginName",username),
                    new SqlParameter("@RoleNames",rolename)                    
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOrdersInfoToExcel", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            //string str = "select a.PID ,a.OrderID ,a.ContractID,a.OrderUnit,a.OrderContactor,a.UseUnit,a.UseContactor,a.UseTel,a.UseAddress,b.IsPay ,a.State,a.Guarantee,a.Provider,a.ProvidManager,a.Demand,a.DemandManager,a.Remark" +
            //" from OrdersInfo a left join ProjectBasInfo b on a.PID=b.PID " + where + "order by ContractDate";
            //DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            DataTable dt = DO_Order.Tables[0];
            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    dt.Rows[r]["Ostate"] = GetStatePro(dt.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
                }
            }
            return dt;
        }

        public static string getOrderContractID(string OrderID)
        {
            string s = "";
            string str = "select ContractID from OrdersInfo where OrderID ='" + OrderID + "' ";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            if (dt.Rows.Count > 0) { s = dt.Rows[0]["ContractID"].ToString(); }
            return s;
        }

        public static string GetMaxContractID()
        {
            string s = "";
            string strID = "";
            string str = "select ContractID  from OrdersInfo order by  right(right(ContractID,3),3) desc";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;

            string Str = GetNamePY(GAccount.GetAccountInfo().UserName);
            string Dime = DateTime.Now.Year.ToString();// ("YYYY");
            Dime = Dime.Substring(2, 2);
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    s = "SP" + "-" + Dime + "-" + "X" + "-" + Str + "001";
                }
                else
                {
                    strID = strID.Substring(strID.Length - 3);
                    strID = string.Format("{0:D3}", Convert.ToInt32(strID) + 1);
                    s = "SP" + "-" + Dime + "-" + "X" + "-" + Str + "-" + strID;
                }
            }
            else
            {
                s = "SP" + "-" + Dime + "-" + "X" + "-" + Str + "-" + "001";
            }
            return s;
        }

        public static UIDataTable GetOrderHTXXGrid(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from tk_CFile where CID ='" + OrderID + "' and validate='v' ";// and  (filetype='doc' or filetype='docx')
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "CID='" + OrderID + "' and validate='v'";
            string strOrderBy = "CreateTime desc ";
            String strTable = " tk_CFile";
            String strField = "ID ,CID,FileName ,CreateTime ,CreateUser";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }

            //   if (instData.DtData != null)
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        instData.DtData.Rows[r]["CreateUser"]= GAccount.GetAccountInfo().UserName.ToString ();// ((instData.DtData.Rows[r]["CreateUser"].ToString()));


            //    }
            //}
            return instData;
        }

        public static UIDataTable GetOrderFJXXGrid(int a_intPageSize, int a_intPageIndex, string OrderID)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from tk_CFile where CID ='" + OrderID + "' and  (filetype!='doc' and filetype!='docx') ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "CID='" + OrderID + "' and (filetype!='doc' and filetype!='docx')";
            string strOrderBy = "CreateTime desc ";
            String strTable = " tk_CFile";
            String strField = " FileName ,CreateTime ,CreateUser";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }
        #endregion

        #region [回款管理]
        public static string GetHKNO(string UnitID)
        {
            //string strDHNo = "";
            //int MaxNum = 0;
            //string sqlMaxNum = "select MaxNum from SIDRecord where SID='HK' and DataRecord='" + DateTime.Now.ToString("yyMMdd") + "' and TaskType='Retail' ";
            //DataTable dtMaxNum = SQLBase.FillTable(sqlMaxNum, "SalesDBCnn");
            //if (dtMaxNum != null && dtMaxNum.Rows.Count > 0)
            //{
            //    MaxNum = int.Parse(dtMaxNum.Rows[0]["MaxNum"].ToString()) + 1; ;
            //}
            //else
            //    MaxNum = 1;

            //strDHNo = "HK" + DateTime.Now.ToString("yyMMdd") + "00" + UnitID + GFun.GetNum(MaxNum, 3);
            //string Rstr = "select RID from ReceivePayment where RID ='" + strDHNo + "' order by RID desc";
            //DataTable Rdt = SQLBase.FillTable(Rstr, "SalesDBCnn");
            //ReceivePayment rpayment = new ReceivePayment();
            //if (Rdt == null)
            //{
            //    rpayment.RID = rpayment.OrderID + string.Format("{0:D3}", 1);
            //}
            //if (Rdt.Rows.Count > 0)
            //{
            //    string s = Rdt.Rows[0]["RID"].ToString();
            //    string w = s.Substring(0, s.Length - 3);
            //    string c = s.Substring(s.Length - 3, 3);

            //    rpayment.RID = w + string.Format("{0:D3}", Convert.ToInt32(c) + 1);
            //    strDHNo = rpayment.RID;
            //}
            //return strDHNo;

            string strID = "";
            string strD = "HK" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(RID) from ReceivePayment";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
            UnitID = string.Format("{0:D4}", Convert.ToInt32(UnitID));
            if (dtID != null && dtID.Rows.Count > 0)
            {
                strID = dtID.Rows[0][0].ToString();
                if (strID == "" || strID == null)
                {
                    strD = strD + string.Format("{0:D4}", UnitID) + string.Format("{0:D3}", 1);
                }
                else
                {
                    int num = Convert.ToInt32(strID.Substring(strID.Length - 3, 3));

                    string stryyyyMMdd = strID.Substring(2, 6);
                    if (DateTime.Now.ToString("yyMMdd") == stryyyyMMdd)
                    {
                        if (num < 9)
                            strD = strD + string.Format("{0:D4}", UnitID) + string.Format("{0:D3}", (num + 1));
                        else if (num < 99 && num >= 9)
                            strD = strD + string.Format("{0:D4}", UnitID) + string.Format("{0:D3}", (num + 1));

                        else
                            strD = strD + string.Format("{0:D4}", UnitID) + (num + 1);
                    }
                    else
                    {
                        strD = strD + string.Format("{0:D4}", UnitID) + string.Format("{0:D3}", 1);
                    }
                }
            }
            else
            {
                strD = strD + string.Format("{0:D4}", UnitID) + string.Format("{0:D3}", 1);
            }
            return strD;
        }

        public static DataTable Methods()
        {
            string sql = "select * from ProjectSelect_Config where [Type]='CBMethod' ";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");

            return dt;
        }


        public static DataTable GetOrderID()
        {
            string sql = "select * from OrdersInfo where OState <5 order by OrderID desc";
            DataTable dt = SQLBase.FillTable(sql, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }
        public static bool SaveReceivePayment(ReceivePayment rpayment, ref string strErr)
        {
            try
            {//1.如果有合同则修改合同的回款金额和次数
                string rStr = "select CID from [BGOI_BasMan]..tk_ContractBas where PID in(select PID from OrdersInfo where OrderID ='" + rpayment.OrderID + "') ";
                DataTable dt = SQLBase.FillTable(rStr, "SalesDBCnn");
                if (dt.Rows.Count > 0)
                {
                    // string UpdateContractBas = "";
                    CCashBack Cash = new CCashBack();
                    Cash.StrCID = dt.Rows[0]["CID"].ToString();
                    string str = "select CBID from  [BGOI_BasMan]..tk_CCashBack where CID='" + Cash.StrCID + "' order by CBID desc";
                    DataTable dt1 = SQLBase.FillTable(str, "SalesDBCnn");
                    if (dt1 == null)
                    {
                        Cash.StrCBID = Cash.StrCID + string.Format("{0:D3}", 1);
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        string s = dt1.Rows[0]["CBID"].ToString();
                        string[] c = s.Split('-');
                        if (c.Length > 1)
                        { //   s=s.Substring (s.Length -1,2);
                            Cash.StrCBID = Cash.StrCID + string.Format("{0:D2}", Convert.ToInt32(c[1]) + 1);
                            Cash.StrCurAmountNum = Convert.ToInt32(c[1]);
                        }
                        else { Cash.StrCurAmountNum = 1; }
                    }
                    else
                    {
                        Cash.StrCBID = Cash.StrCID + "-" + string.Format("{0:D2}", 1);
                        Cash.StrCurAmountNum = 1;
                    }
                    Cash.StrCBMethod = rpayment.Mothods;
                    Cash.StrCBMoney = rpayment.Amount;
                    Cash.StrCBBillNo = rpayment.ChequeID;
                    Cash.StrReceiptNo = rpayment.InvoiceNum;
                    Cash.StrPayCompany = rpayment.PaymentUnit;
                    Cash.StrRemark = rpayment.Remark;
                    Cash.StrCBDate = rpayment.PayTime;
                    Cash.StrValidate = "v";
                    Cash.StrCreateTime = DateTime.Now;
                    Cash.StrCreateUser = GAccount.GetAccountInfo().UserName;
                    string strInsert = GSqlSentence.GetInsertInfoByD<CCashBack>(Cash, "[BGOI_BasMan]..tk_CCashBack");
                    string strUpdate = "update tk_ContractBas set CurAmountNum = '" + Cash.StrCurAmountNum + "',State = '1' where CID = '" + Cash.StrCID + "'";
                    string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','合同回款操作','回款成功','" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "','合同')";

                    SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                    SQLBase.ExecuteNonQuery(strUpdate, "SupplyCnn");
                    SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");
                }
                //string Rstr = "select RID from ReceivePayment where OrderID ='" + rpayment.OrderID + "' order by RID desc";
                //DataTable Rdt = SQLBase.FillTable(Rstr, "SalesDBCnn");
                //if (Rdt == null)
                //{
                //    rpayment.RID = rpayment.OrderID + string.Format("{0:D3}", 1);
                //}
                //if (Rdt.Rows.Count > 0)
                //{
                //    string s = Rdt.Rows[0]["RID"].ToString();
                //    string w = s.Substring(0, s.Length - 3);
                //    string c = s.Substring(s.Length - 3, 3);

                //    rpayment.RID = w + string.Format("{0:D3}", Convert.ToInt32(c) + 1);



                //}
                string InsertRP = GSqlSentence.GetInsertInfoByD<ReceivePayment>(rpayment, "ReceivePayment");
                //修改订单的状态为已回款1120K
                string UPdateOrdersInfo = "update OrdersInfo set OState ='4' where OrderID ='" + rpayment.OrderID + "'";
                int i = 0, j = 0;
                if (InsertRP != "")
                {
                    i = SQLBase.ExecuteNonQuery(InsertRP, "SalesDBCnn");
                    j = SQLBase.ExecuteNonQuery(UPdateOrdersInfo, "SalesDBCnn");
                }
                if (i >= 1)
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
                return false;
            }
        }

        public static UIDataTable LoadReceivePayment(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from ( select a.RID,a.ContractID,a.OrderID,b.OrderContent,a.Amount,a.Mothods,a.Manager,a.PayTime,a.IsInvoice,a.InvoiceNum,a.Remark,c.UseUnit,c.OrderUnit" +
  " from ReceivePayment  a left join Orders_DetailInfo b on a.OrderID =b.OrderID  left join OrdersInfo c on c.OrderID=a.OrderID " + strWhere + ")as s ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = strWhere.Substring(6, strWhere.Length - 6);
            }
            string strFilter = strWhere;
            string strOrderBy = "PayTime desc";
            //  String strTable = " ReceivePayment  a left join Orders_DetailInfo b on a.OrderID =b.OrderID  left join OrdersInfo c on c.OrderID=a.OrderID ";
            String strTable = " ReceivePayment  a left join OrdersInfo c on c.OrderID=a.OrderID left join Orders_DetailInfo b on a.OrderID =b.OrderID ";
            String strField = " a.RID ,a.ContractID,a.OrderID,OrderContent=(stuff((select ','+OrderContent from Orders_DetailInfo b where b.orderid =c.orderid for xml path('')),1,1,'')),a.Amount,a.Mothods,a.Manager,a.PayTime,a.IsInvoice,a.InvoiceNum,a.Remark,c.UseUnit,c.OrderUnit";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
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
                    if (instData.DtData.Rows[r]["Mothods"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["Mothods"] = GetSelectPro(instData.DtData.Rows[r]["Mothods"].ToString()).Text;
                    }
                }

            }
            return instData;
        }


        public static DataTable LoadReceiveBill(string OrderID, ref string strErr)
        {
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",OrderID)
                };
            DataTable dt = SQLBase.FillTable("getReceivePaymentRelatedData", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }
        public static UIDataTable LoadReceivePaymentBill(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",OrderID)
                };
            SqlParameter[] sqlcount = new SqlParameter[]
                {
                    new SqlParameter("@Where",OrderID)
                };
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar("GetReceiveBiiRelCount", CommandType.StoredProcedure, sqlcount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            instData.DtData = SQLBase.FillTable("getReceivePaymentRelatedData", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;

        }

        public static DataTable ReceivePaymentToExcel(string where, ref string strErr)
        {
            try
            {
                string str = "select a.RID ,a.OrderID,b.OrderContent,a.Amount,a.Mothods,a.PayTime" +
" from ReceivePayment  a left join Orders_DetailInfo b on a.OrderID =b.OrderID  left join OrdersInfo c on c.OrderID=a.OrderID " + where;
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                else
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        if (dt.Rows[r]["Mothods"].ToString() != "")
                        {
                            dt.Rows[r]["Mothods"] = GetSelectPro(dt.Rows[r]["Mothods"].ToString()).Text;
                        }
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }

        }

        public static ReceivePayment getReceivePayment(string RID)
        {
            string str = "select * from ReceivePayment where RID='" + RID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            ReceivePayment receivepayment = new ReceivePayment();
            foreach (DataRow item in dt.Rows)
            {
                DataRowToReceivePayment(item, receivepayment);
            }
            return receivepayment;
        }
        public static void DataRowToReceivePayment(DataRow item, ReceivePayment receivepayment)
        {
            receivepayment.RID = item["RID"].ToString();
            receivepayment.OrderID = item["OrderID"].ToString();
            if (item["Amount"] != "")
                receivepayment.Amount = Convert.ToDecimal(item["Amount"]);
            receivepayment.Mothods = item["Mothods"].ToString();
            receivepayment.PayTime = item["PayTime"].ToString();
            receivepayment.Mothods = item["Mothods"].ToString();
            receivepayment.IsInvoice = item["IsInvoice"].ToString();
            receivepayment.InvoiceNum = item["InvoiceNum"].ToString();
            receivepayment.Remark = item["Remark"].ToString();
            receivepayment.Manager = item["Manager"].ToString();
            receivepayment.PaymentUnit = item["PaymentUnit"].ToString();

        }

        public static bool SaveUpdateReceivePayment(ReceivePayment receivepayment, ref string strErr)
        {
            try
            {
                string strUpdateReceive = "update ReceivePayment set Amount='" + receivepayment.Amount + "',Mothods='" + receivepayment.Mothods + "',";
                strUpdateReceive += "ChequeID='" + receivepayment.ChequeID + "',Remark='" + receivepayment.Remark + "',";
                strUpdateReceive += "PaymentUnit='" + receivepayment.PaymentUnit + "' where RID='" + receivepayment.RID + "'";
                string strInReceiveHIS = "insert into  ReceivePayment_HIS (RID,OrderID,Amount,Mothods ,PayTime ,IsInvoice ,InvoiceNum , ChequeID ,Manager ," +
                "CreateUser,CreateTime ,Validate ,SalesType ,Remark ,PaymentUnit,  NCreateUser,NCreateTime) select RID,OrderID,Amount,Mothods ,PayTime ,IsInvoice ,InvoiceNum,ChequeID ,Manager ," +
                "CreateUser,CreateTime ,Validate ,SalesType ,Remark ,PaymentUnit,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'  from ReceivePayment where RID='" + receivepayment.RID + "'";

                SQLBase.ExecuteNonQuery(strUpdateReceive, "SalesDBCnn");
                SQLBase.ExecuteNonQuery(strInReceiveHIS, "SalesDBCnn");
                return true;

            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                throw;
            }
        }

        public static bool CancelReceivePayment(string RID, ref string strErr)
        {
            try
            {
                string strCancel = "update ReceivePayment set Validate='i' where RID='" + RID + "'";
                string strInReceiveHIS = "insert into  ReceivePayment_HIS (RID,OrderID,Amount,Mothods ,PayTime ,IsInvoice ,InvoiceNum , ChequeID ,Manager ," +
                "CreateUser,CreateTime ,Validate ,SalesType ,Remark ,PaymentUnit , NCreateUser,NCreateTime) select RID,OrderID,Amount,Mothods ,PayTime ,IsInvoice ,InvoiceNum,ChequeID ,Manager ," +
                "CreateUser,CreateTime ,Validate ,SalesType ,Remark ,PaymentUnit,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'  from ReceivePayment where RID='" + RID + "'";
                SQLBase.ExecuteNonQuery(strCancel, "SalesDBCnn");
                SQLBase.ExecuteNonQuery(strInReceiveHIS, "SalesDBCnn");
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                throw;
            }
        }


        public static DataTable GetReceivePaymentByOrderID(string OrderID)
        {
            string str = "select * from  ReceivePayment  where OrderID ='" + OrderID + "' ";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            if (dt != null)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    if (dt.Rows[r]["Mothods"].ToString() != "")
                    {
                        dt.Rows[r]["Mothods"] = GetSelectPro(dt.Rows[r]["Mothods"].ToString()).Text;
                    }
                }

            }
            return dt;

        }


        //获取订单的合同额和已回款
        public static DataTable getOrdersInfoTotal(string OrdersID)
        {
            string str = "select  a.Total,DebtAmount=(a.Total-ISnull((select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID),0)),HKAmount=(select sum(Amount) from ReceivePayment b where b.OrderID =a.OrderID)" + "from  OrdersInfo a where a.OrderID ='" + OrdersID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            //OrdersInfo order = new OrdersInfo();
            //foreach (DataRow item in dt.Rows)
            //{
            //    DataRowToOrderInfo(item, order);
            //}
            //return order;
            return dt;

        }
        //回款提示
        public static UIDataTable GetShowReceivePayment(int a_intPageSize, int a_intPageIndex, string strWhere) 
        {
            //string sql = "select top 3 * from ReceivePayment  order by PayTime desc ";
            //UIDataTable  uita = SQLBase.FillTable(sql,"SalesDBCnn");
            //if (dt == null) return null;
            //return dt;

            UIDataTable instData = new UIDataTable();
            //
            string strSelCount = "select COUNT(*) " +
                                  " from ( select a.RID,a.ContractID,a.OrderID,a.Amount,a.Mothods,a.Manager,a.PayTime,a.IsInvoice,a.InvoiceNum,a.Remark" +
  " from ReceivePayment a where validate='v' and  datediff(day,GETDATE(),a.PayTime)<7 " + strWhere + ")as s ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    strWhere = strWhere.Substring(6, strWhere.Length - 6);
            //}
            string strFilter = " validate='v' and  datediff(day,GETDATE(),a.PayTime)<7 " + strWhere;
            string strOrderBy = "PayTime desc";
            //  String strTable = " ReceivePayment  a left join Orders_DetailInfo b on a.OrderID =b.OrderID  left join OrdersInfo c on c.OrderID=a.OrderID ";
            String strTable = " ReceivePayment a";
            String strField = " a.RID ,a.ContractID,a.OrderID,a.Amount,a.Mothods,a.Manager,a.PayTime,a.IsInvoice,a.InvoiceNum,a.Remark";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
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
                    if (instData.DtData.Rows[r]["Mothods"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["Mothods"] = GetSelectPro(instData.DtData.Rows[r]["Mothods"].ToString()).Text;
                    }
                }

            }
            return instData;
               
        }

        public static DataTable GetTopShowReceivePayment() 
        {
            string sql = "select top 3 * from ReceivePayment  order by PayTime desc ";
            DataTable dt = SQLBase.FillTable(sql,"SalesDBCnn");
            if (dt == null) return null;
            return dt;
        
        }
        #endregion

        #region [发货管理]
        public static string GetNewShipGoodsID()
        {
            string strID = "";
            string strD = "FH" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(ShipGoodsID) from Shipments";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));

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

        public static bool SaveOrderShip(Shipments shipments, List<Shipments_DetailInfo> list, List<Orders_DetailInfo> listDetail, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<Shipments>(shipments, "Shipments");
                string strInsertList = "";
                int i = 0, j = 0, l = 0;
                if (list.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(list, "Shipments_DetailInfo");
                }
                if (strInsert != "")
                {
                    i = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");

                }

                if (listDetail != null)
                {
                    foreach (Orders_DetailInfo item in listDetail)
                    {
                        string updateOrderDetail = "update Orders_DetailInfo set ShipmentNum =ShipmentNum+" + item.ShipmentNum + " where DID='" + item.DID + "'";
                        j = SQLBase.ExecuteNonQuery(updateOrderDetail, "SalesDBCnn");

                    }


                }
                if (strInsertList != "")
                {
                    l = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                }
                //修改订单的发货状态
                string st = "select distinct OrderID from Orders_DetailInfo where OrderNum =ShipmentNum";
                DataTable dt2 = SQLBase.FillTable(st, "SalesDBCnn");
                if (dt2 != null)
                {
                    for (int m = 0; m < dt2.Rows.Count; m++)
                    {
                        string updateOrdersInfoState = "UPdate OrdersInfo set Ostate='3' where OrderID='" + dt2.Rows[m]["OrderID"] + "'";
                        SQLBase.ExecuteNonQuery(updateOrdersInfoState, "SalesDBCnn");
                    }
                }

                if (i + j + l >= 3) { return true; }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;

            }
        }

        public static UIDataTable LoadOrderShipment(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from Shipments where ShipGoodsID ='" + ShipGoodsID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "ShipGoodsID='" + ShipGoodsID + "'";
            string strOrderBy = "ShipmentDate ";
            String strTable = " Shipments";
            String strField = "ShipGoodsID ,OrderID,ShipmentDate,Orderer ,Shippers ,CreateTime,Remark";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }

        public static UIDataTable LoadOrderShipmentDetail(int a_intPageSize, int a_intPageIndex, string ShipGoodsID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) " +
                                   " from Shipments_DetailInfo where ShipGoodsID ='" + ShipGoodsID + "'";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            string strFilter = "ShipGoodsID='" + ShipGoodsID + "'";
            string strOrderBy = "CreateTime ";
            String strTable = " Shipments_DetailInfo";
            String strField = "ShipGoodsID ,DID,ProductID ,OrderContent ,Specifications ,Price ,Amount, Supplier ,Unit,Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }

        public static UIDataTable LoadShipmentsGrid(int a_intPageSize, int a_intPageIndex, string strWhere)
        {
            UIDataTable instData = new UIDataTable();
            //
            //string strSelCount = "select COUNT(*) from  (select  distinct a.ShipGoodsID , a.OrderID,a.ContractID,a.ShipmentDate,a.Shippers,a.Orderer,a.Remark from Shipments a  left join Shipments_DetailInfo b on a.ShipGoodsID =b.ShipGoodsID where  a.Validate ='v' " + strWhere + ")as t ";
            //instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            //if (instData.IntRecords > 0)
            //{
            //    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            //}
            //else
            //    instData.IntTotalPages = 0;
            ////if (!string.IsNullOrEmpty(strWhere))
            ////{
            ////    strWhere = strWhere.Substring(6, strWhere.Length - 6);
            ////}
            //string strFilter = "  a.Validate ='v' " + strWhere;
            //string strOrderBy = " ShipmentDate desc";
            //String strTable = "  Shipments a  left join Shipments_DetailInfo b on a.ShipGoodsID =b.ShipGoodsID  ";
            //String strField = " distinct a.ShipGoodsID,a.OrderID,a.ContractID,a.ShipmentDate,a.Shippers,a.Orderer,a.Remark";


            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere),
                   
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetShipmentsGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            // instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            //if (instData == null)
            //{
            //    instData.DtData = null;
            //    instData.IntRecords = 0;
            //    instData.IntTotalPages = 0;
            //}

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

        public static DataTable GetShipmentDetail(string ShipGoodsID)
        {
            string str = "select * from Shipments_DetailInfo where ShipGoodsID='" + ShipGoodsID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }
        //
        public static DataTable GetShipments(string ShipGoodsID, string Orderid)
        {
            string str = "select a.ShipGoodsID,a.Orderer ,a.Shippers,a.ShipmentDate,a.Remark ,b.ContractDate" +
            " from Shipments a left join OrdersInfo b on a.OrderID=b.OrderID where a.ShipGoodsID ='" + ShipGoodsID + "'and a.OrderID='" + Orderid + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static Shipments getShipmentsBySID(string ShipGoodsID)
        {
            string str = " select * from Shipments where ShipGoodsID='" + ShipGoodsID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            Shipments shipment = new Shipments();
            foreach (DataRow item in dt.Rows)
            {
                DtataRowToShipments(item, shipment);
            }
            return shipment;
        }
        public static void DtataRowToShipments(DataRow item, Shipments shipment)
        {
            shipment.ShipGoodsID = item["ShipGoodsID"].ToString();
            shipment.ShipmentDate = Convert.ToDateTime(item["ShipmentDate"]);
            shipment.Shippers = item["Shippers"].ToString();
            shipment.Orderer = item["Orderer"].ToString();
            shipment.OrderID = item["OrderID"].ToString();
            shipment.Remark = item["Remark"].ToString();
            shipment.CreateTime = Convert.ToDateTime(item["CreateTime"]);
            shipment.CreateUser = item["CreateUser"].ToString();
            shipment.Validate = item["Validate"].ToString();
        }

        public static bool SaveUpdateShipment(Shipments shipments, List<Shipments_DetailInfo> list, ref string strErr)
        {

            try
            {

                string upstr = "update  Shipments set Remark='" + shipments.Remark + "',Orderer='" + shipments.Orderer + "',Shippers='" + shipments.Shippers + "'" +
                    " where shipgoodsid='" + shipments.ShipGoodsID + "'";
                string hisstr = "insert into Shipments_HIS (ShipGoodsID ,OrderID,ShipmentDate,Remark ,Orderer ,Shippers ,CreateTime ,CreateUser,Validate,NCreateUser,NCreateTime)" +
                 " select ShipGoodsID ,OrderID,ShipmentDate,Remark ,Orderer ,Shippers ,CreateTime ,CreateUser,Validate,'" + GAccount.GetAccountInfo().UserName + "','" + DateTime.Now + "'";
                hisstr += "  from Shipments where ShipGoodsID='" + shipments.ShipGoodsID + "'";
                if (shipments != null)
                {
                    //写入到HIS表中
                    SQLBase.ExecuteNonQuery(hisstr, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(upstr, "SalesDBCnn");
                }
                string strUpdateList = "";
                string strInsertDetailHIS = "";
                if (list.Count > 0)
                {
                    foreach (Shipments_DetailInfo item in list)
                    {
                        strUpdateList = "update Shipments_DetailInfo set Amount=" + item.Amount + ",Supplier='" + item.Supplier + "' where DID ='" + item.DID + "'";
                        strInsertDetailHIS = "insert into Shipments_DetailInfo_HIS(ShipGoodsID ,DID ,ProductID ,OrderContent ,Specifications ,Supplier ,Unit,Price,Amount ,Remark,CreateTime ,CreateUser," +
                        "Validate ,NCreateTime,NCreateUser) select ShipGoodsID ,DID ,ProductID ,OrderContent ,Specifications ,Supplier ,Unit,Price,Amount ,Remark,CreateTime ,CreateUser," +
                        "Validate ,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from Shipments_DetailInfo where DID='" + item.DID + "'";
                        if (strUpdateList != "")
                        {
                            SQLBase.ExecuteNonQuery(strInsertDetailHIS, "SalesDBCnn");
                            SQLBase.ExecuteNonQuery(strUpdateList, "SalesDBCnn");

                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return false;
            }
        }

        public static bool CanCelShipments(string ShipGoodsID, ref string strErr)
        {
            string str = " update Shipments set Validate ='i' where ShipGoodsID='" + ShipGoodsID + "'";
            try
            {
                int i = SQLBase.ExecuteNonQuery(str, "SalesDBCnn");
                if (i <= 0) return false;
                return true;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return false;
                throw;
            }
        }

        public static DataTable ShipmentsToExcel(string where, ref string strErr)
        {
            try
            {
                string str = " select a.OrderID,a.ShipGoodsID ,a.ShipmentDate,a.Shippers,a.Orderer,a.Remark";
                str += " from  Shipments a left join OrdersInfo b on a.OrderID =b.OrderID" + where + "  order by ShipmentDate ";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }
        #endregion

        #region [退换货管理]
        public static string getNewGoodsID()
        {
            string strID = "";
            string strD = "TH" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(EID) from ExchangeGoods";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
                            strD = strD + UnitID + string.Format("{0:D3}", (num + 1));

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

        public static string getNewExCheckID()
        {
            string strID = "";
            string strD = "TC" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(TID) from Exchange_Check";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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

        //退换货类型
        public static DataTable GetEXCType()
        {
            string Sql = "select ID,TEXT from ProjectSelect_Config  where Type='ExcType'";
            DataTable dt = SQLBase.FillTable(Sql, "SalesDBCnn");
            return dt;
        }
        //退换货方式
        public static DataTable GetTypeSelect(string type)
        {
            string Sql = "select ID,TEXT from ProjectSelect_Config  where Type='" + type + "'";
            DataTable dt = SQLBase.FillTable(Sql, "SalesDBCnn");
            return dt;
        }


        public static DataTable GetShipmentsDetail(string OrderID)
        {
            string Str = "select ShipGoodsID,PID ,OrderContent,OrderContent,Supplier,Unit,Amount"
              + " from Shipments_DetailInfo where ShipGoodsID in" +
             " (select ShipGoodsID from Shipments where OrderID ='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetShipmentOrdersDetail(string OrderID)
        {
            //string Str = "select DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
            //       + "OrderNum,Price,Subtotal,Technology,DeliveryTime from Orders_DetailInfo where OrderNum >0 and  OrderID='" + OrderID + "'";
            // string Str = "select DID,OrderID,ProductID ,OrderContent,SpecsModels,Manufacturer,OrderUnit,"
            //+ "ActualAmount,Price,ActualSubTotal,Technology,DeliveryTime from Orders_DetailInfo where ActualAmount >0 and  OrderID='" + OrderID + "'";
            string Str = "select a.DID,OrderID,a.ProductID ,a.OrderContent,SpecsModels,Manufacturer,OrderUnit,(a.ActualAmount-ISNULL(b.Amount,0)-ISNULL(c.Amount ,0))as ActualAmount,a.Price,ActualSubTotal,a.Technology,DeliveryTime from Orders_DetailInfo a left join(select SUM(amount) as Amount ,EDID from Exchange_Detail group by EDID )as b on a.DID =b.EDID left join (select SUM(amount) as Amount ,EDID from  ExReturn_Detail group by EDID) as c on a.DID =c.EDID where ActualAmount >0 and  OrderID='" + OrderID + "' and (a.ActualAmount-ISNULL(b.Amount,0)-ISNULL(c.Amount ,0))>0";
            DataTable dt = SQLBase.FillTable(Str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static bool SaveExchangeGoods(ExchangeGoods excgoods, List<ExReturn_Detail> RList, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<ExchangeGoods>(excgoods, "ExchangeGoods");
                string strInsertList = "";
                string strInsertRList = "";
                string strInsertRorderList = "";
                #region MyRegion
                //if (ExcAmount == null)
                //{
                //    ExcAmount = "0";
                //}
                //if (RExcTotal == "" || RExcTotal == null)
                //{
                //    RExcTotal = "0";
                //}
                //string strUpdateOrder = "update OrdersInfo set OrderActualAmount =OrderActualAmount-" + Convert.ToInt32(ExcAmount) + "+" + Convert.ToInt32(RExcAmount) + " ,OrderActualTotal=OrderActualTotal -" + Convert.ToDecimal(ExcTotal) + "+" + Convert.ToDecimal(RExcTotal) + " where OrderID ='" + excgoods.OrderID + "'";


                //if (list.Count > 0)
                //{
                //    strInsertList = GSqlSentence.GetInsertByList(list, "Exchange_Detail");
                //} 
                #endregion
                if (RList.Count > 0)
                {
                    strInsertRList = GSqlSentence.GetInsertByList(RList, "ExReturn_Detail");
                }

                //if (RorderList.Count > 0)
                //{
                //    strInsertRorderList = GSqlSentence.GetInsertByList(RorderList, "Orders_DetailInfo");
                //}
                if (strInsert != "")
                {
                    SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");

                }
                if (strInsertRList != "")
                {
                    SQLBase.ExecuteNonQuery(strInsertRList, "SalesDBCnn");
                }
                #region MyRegion
                //if (strInsertRorderList != "")
                //{
                //    int j = SQLBase.ExecuteNonQuery(strInsertRorderList, "SalesDBCnn");
                //}
                //if (excgoods.OrderID != "")
                //{
                //    int i = SQLBase.ExecuteNonQuery(strUpdateOrder, "SalesDBCnn");
                //}

                //if (strInsertList != "")
                //{

                //    //1.先把原始数据插入到HIS表中
                //    //2.修改原始表中的数据
                //    decimal ETotal = 0;
                //    foreach (Orders_DetailInfo item in OrderList)
                //    {
                //        //获取原数据
                //        //string strUpdateOrderDetail = "update Orders_DetailInfo set OrderNum =OrderNum -" + item.OrderNum + "," +
                //        // "Price =Price -" + item.Price + "," + " Subtotal =Subtotal -" + item.Subtotal + " where DID='" + item.DID + "'";
                //        string strUpdateOrderDetail = "update Orders_DetailInfo set ActualAmount =ActualAmount -" + item.OrderNum + "," + " ActualSubTotal =ActualSubTotal -" + item.Subtotal + " where DID='" + item.DID + "'";
                //        //修改订单退货的差价1127k
                //        string str = "select OrderNum from Orders_DetailInfo where DID='" + item.DID + "'";
                //        string str1 = "select Price from Orders_DetailInfo where DID='" + item.DID + "'";
                //        DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                //        DataTable dt2 = SQLBase.FillTable(str1, "SalesDBCnn");
                //        decimal oldprice = 0;
                //        int oldnum = 0;
                //        if (dt != null)
                //            oldnum = Convert.ToInt32(dt.Rows[0]["OrderNum"]);
                //        if (dt2 != null)
                //            oldprice = Convert.ToDecimal(dt2.Rows[0]["Price"]);
                //        ETotal += (oldprice - item.Price) * (item.OrderNum);
                //        string strInsertOrderHIS = "insert into Orders_DetailInfo_HIS ( PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit," + "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," + "State ,Remark ,CreateTime,createuser,validate,NCreateTime ,NCreateUser  ) " +
                //              "select PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit ," + "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," +
                //              "State ,Remark ,CreateTime,createuser,validate," + "'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                //              "from Orders_DetailInfo " +
                //              "where  DID='" + item.DID + "'";
                //        SQLBase.ExecuteNonQuery(strInsertOrderHIS, "SalesDBCnn");

                //        SQLBase.ExecuteNonQuery(strUpdateOrderDetail, "SalesDBCnn");
                //    }
                //    string updateOrder = "update OrdersInfo set ReturnNoTax=" + ETotal + " where OrderID ='" + excgoods.OrderID + "'";
                //    SQLBase.ExecuteNonQuery(updateOrder, "SalesDBCnn");
                //    SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");





                //} 
                #endregion

                #region MyRegion
                ////huan货里面的物品则把valide 置为i
                //foreach (ExReturn_Detail  item in RList)
                //{
                //    string str = "select count(*) from Orders_DetailInfo where SpecsModels ='"+item.Specifications+"'";
                //    int i = GFun.SafeToInt32(SQLBase.ExecuteScalar(str, "SalesDBCnn"));
                //    string s = "select DID from Orders_DetailInfo where OrderID ='" + excgoods.OrderID + "' order by DID desc";
                //    DataTable dt = SQLBase.FillTable(s,"SalesDBCnn");
                //    if (dt != null) {
                //        string sno = dt.Rows[0]["DID"].ToString();
                //        sno = sno.Substring(sno.Length-3,3);
                //    }
                //    if (i == 0) 
                //    {
                //        string strInsert1 = "insert into(OrderID,DID)values()";
                //    }
                //       // string strUpdateOrderDetail = "update Orders_DetailInfo set OrderNum =OrderNum -" + item.OrderNum + "," + " Subtotal =Subtotal -" + item.Subtotal + " where DID='" + item.DID + "'";
                //} 
                #endregion


                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;

            }
        }

        public static bool SaveExchangeGoods(ExchangeGoods excgoods, List<Exchange_Detail> EList, ref string strErr)
        {
            try
            {
                int i = 0, j = 0;
                string strInsert = GSqlSentence.GetInsertInfoByD<ExchangeGoods>(excgoods, "ExchangeGoods");
                string strInsertList = "";
                if (EList.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(EList, "Exchange_Detail");
                }

                if (strInsert != "")
                {
                    i = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
                }
                if (strInsertList != "")
                {
                    j = SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                }
                if (i + j >= 2)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;

            }
        }



        public static bool SaveUpdateExcGoods(ExchangeGoods excgoods, List<ExReturn_Detail> RList, ref string strErr)
        {
            try
            {
                string strUpdatExc = "update ExchangeGoods set ReturnType='" + excgoods.ReturnType + "',ReturnWay='" + excgoods.ReturnWay + "',ReturnContract='" + excgoods.ReturnContract + "', "
                + "ReturnReason='" + excgoods.ReturnReason + "',ReturnInstructions='" + excgoods.ReturnInstructions + "' where EID='" + excgoods.EID + "'";
                string strInserExcHIS = "insert into  ExchangeGoods_HIS (EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,Methods,ReturnInstructions,"
                + " ExFinishDate,ExFinishDescription,ExFinishDealPeo,NCreateTime,NCreateUser) select EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,"
                + "Methods,ReturnInstructions,ExFinishDate,ExFinishDescription,ExFinishDealPeo,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ExchangeGoods where EID='" + excgoods.EID + "'";


                ///

                string UpdateReturnDetail = GSqlSentence.GetInsertByList<ExReturn_Detail>(RList, "ExReturn_Detail");
                //if (list.Count > 0)
                //{
                //    foreach (Exchange_Detail item in list)
                //    {
                //        string strUpdatDetail = "update Exchange_Detail set Amount=" + item.Amount + ",ExUnit='" + item.ExUnit + "',ExTotal='" + item.ExTotal + "' where DID='" + item.DID + "' ";
                //        string strInserDetailHIS = "insert into Exchange_Detail_HIS(EID,DID,PID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)"
                //        + "select EID,DID,PID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from Exchange_Detail where DID='" + item.DID + "'";
                //        SQLBase.ExecuteNonQuery(strInserDetailHIS, "SalesDBCnn");
                //        SQLBase.ExecuteNonQuery(strUpdatDetail, "SalesDBCnn");
                //    }
                //}
                if (RList.Count > 0)
                {
                    foreach (ExReturn_Detail item in RList)
                    {
                        //    string strUpdatReturnDetail = "update Exchange_Detail set Amount=" + item.Amount + ",ExUnit='" + item.ExUnit + "',ExTotal='" + item.ExTotal + "' where DID='" + item.DID + "' ";
                        string strInserReutrnDetailHIS = "insert into ExReturn_Detail_HIS(EID,DID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,Price,Subtotal ,UnitCost,TotalCost ,SaleNo,ProjectNO,JNAME,Technology,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)"
                           + "select EID,DID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,Price,Subtotal ,UnitCost,TotalCost ,SaleNo,ProjectNO,JNAME,Technology,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ExReturn_Detail where DID='" + item.DID + "'";

                        SQLBase.ExecuteNonQuery(strInserReutrnDetailHIS, "SalesDBCnn");
                    }
                    string strDelete = "delete from ExReturn_Detail where EID='" + excgoods.EID + "'";
                    //string strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_Sales.dbo.offerinfo");
                    SQLBase.ExecuteNonQuery(strDelete, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(UpdateReturnDetail, "SalesDBCnn");

                }
                if (strUpdatExc != "")
                {
                    SQLBase.ExecuteNonQuery(strInserExcHIS, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strUpdatExc, "SalesDBCnn");

                }
                //if (OrderList.Count > 0)
                //{

                //    //1.先把原始数据插入到HIS表中
                //    //2.修改原始表中的数据
                //    foreach (Orders_DetailInfo item in OrderList)
                //    {
                //        //获取原数据

                //        string strUpdateOrderDetail = "update Orders_DetailInfo set OrderNum =OrderNum -" + item.OrderNum + "," +
                //         "Price =Price -" + item.Price + "," + " Subtotal =Subtotal -" + item.Subtotal + " where DID='" + item.DID + "'";
                //        string strInsertOrderHIS = "insert into Orders_DetailInfo_HIS ( PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit," +
                //              "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," +
                //              "State ,Remark ,CreateTime,createuser,validate,NCreateTime ,NCreateUser  ) " +
                //              "select PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit ," +
                //              "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," +
                //              "State ,Remark ,CreateTime,createuser,validate," + "'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                //              "from Orders_DetailInfo " +
                //              "where  DID='" + item.DID + "'";
                //        SQLBase.ExecuteNonQuery(strInsertOrderHIS, "SalesDBCnn");

                //        SQLBase.ExecuteNonQuery(strUpdateOrderDetail, "SalesDBCnn");
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;

            }
        }




        public static bool SaveUpdateReturnGoods(ExchangeGoods excgoods, List<Exchange_Detail> RList, ref string strErr)
        {
            try
            {
                string strUpdatExc = "update ExchangeGoods set ReturnType='" + excgoods.ReturnType + "',ReturnWay='" + excgoods.ReturnWay + "',ReturnContract='" + excgoods.ReturnContract + "', "
                + "ReturnReason='" + excgoods.ReturnReason + "',ReturnInstructions='" + excgoods.ReturnInstructions + "' where EID='" + excgoods.EID + "'";
                string strInserExcHIS = "insert into  ExchangeGoods_HIS (EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,Methods,ReturnInstructions,"
                + " ExFinishDate,ExFinishDescription,ExFinishDealPeo,NCreateTime,NCreateUser) select EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,"
                + "Methods,ReturnInstructions,ExFinishDate,ExFinishDescription,ExFinishDealPeo,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ExchangeGoods where EID='" + excgoods.EID + "'";


                ///

                string UpdateReturnDetail = GSqlSentence.GetInsertByList<Exchange_Detail>(RList, "Exchange_Detail");
                //if (list.Count > 0)
                //{
                //    foreach (Exchange_Detail item in list)
                //    {
                //        string strUpdatDetail = "update Exchange_Detail set Amount=" + item.Amount + ",ExUnit='" + item.ExUnit + "',ExTotal='" + item.ExTotal + "' where DID='" + item.DID + "' ";
                //        string strInserDetailHIS = "insert into Exchange_Detail_HIS(EID,DID,PID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)"
                //        + "select EID,DID,PID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from Exchange_Detail where DID='" + item.DID + "'";
                //        SQLBase.ExecuteNonQuery(strInserDetailHIS, "SalesDBCnn");
                //        SQLBase.ExecuteNonQuery(strUpdatDetail, "SalesDBCnn");
                //    }
                //}
                if (RList.Count > 0)
                {
                    foreach (Exchange_Detail item in RList)
                    {
                        //    string strUpdatReturnDetail = "update Exchange_Detail set Amount=" + item.Amount + ",ExUnit='" + item.ExUnit + "',ExTotal='" + item.ExTotal + "' where DID='" + item.DID + "' ";
                        string strInserReutrnDetailHIS = "insert into Exchange_Detail_HIS(EID,DID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,NCreateTime,NCreateUser)"
                           + "select EID,DID,ProductID,OrderContent,Specifications,Supplier,Unit,Amount,ExUnitNo,ExTotalNo,ExUnit,ExTotal,Remark,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from Exchange_Detail where DID='" + item.DID + "'";

                        SQLBase.ExecuteNonQuery(strInserReutrnDetailHIS, "SalesDBCnn");
                    }
                    string strDelete = "delete from Exchange_Detail where EID='" + excgoods.EID + "'";
                    //string strInsertList = GSqlSentence.GetInsertByList(list, "BGOI_Sales.dbo.offerinfo");
                    SQLBase.ExecuteNonQuery(strDelete, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(UpdateReturnDetail, "SalesDBCnn");

                }
                if (strUpdatExc != "")
                {
                    SQLBase.ExecuteNonQuery(strInserExcHIS, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strUpdatExc, "SalesDBCnn");

                }
                //if (OrderList.Count > 0)
                //{

                //    //1.先把原始数据插入到HIS表中
                //    //2.修改原始表中的数据
                //    foreach (Orders_DetailInfo item in OrderList)
                //    {
                //        //获取原数据

                //        string strUpdateOrderDetail = "update Orders_DetailInfo set OrderNum =OrderNum -" + item.OrderNum + "," +
                //         "Price =Price -" + item.Price + "," + " Subtotal =Subtotal -" + item.Subtotal + " where DID='" + item.DID + "'";
                //        string strInsertOrderHIS = "insert into Orders_DetailInfo_HIS ( PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit," +
                //              "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," +
                //              "State ,Remark ,CreateTime,createuser,validate,NCreateTime ,NCreateUser  ) " +
                //              "select PID,OrderID ,DID ,ProductID ,OrderContent ,SpecsModels ,Manufacturer,OrderUnit ," +
                //              "OrderNum ,TaxUnitPrice ,UnitPrice ,DUnitPrice ,DTotalPrice ,Price ,Subtotal ,Technology ,DeliveryTime ," +
                //              "State ,Remark ,CreateTime,createuser,validate," + "'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "'" +
                //              "from Orders_DetailInfo " +
                //              "where  DID='" + item.DID + "'";
                //        SQLBase.ExecuteNonQuery(strInsertOrderHIS, "SalesDBCnn");

                //        SQLBase.ExecuteNonQuery(strUpdateOrderDetail, "SalesDBCnn");
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;

            }
        }


        public static UIDataTable LoadExchangeGoodsGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select COUNT(*) from( select a.EID,a.OrderID,a.ChangeDate,a.ReturnWay,a.State,a.ISF,a.ISEXR,b.ProductionState  " +
            " from ExchangeGoods a a left join  Exchange_Check b on a.EID =b.EID where a.validate='v' " + where + " ) as s ";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //if (!string.IsNullOrEmpty(where))
            //{
            //    where = where.Substring(6, where.Length - 6);
            //}

            string strFilter = " a.validate='v' " + where;
            string strOrderBy = " a.CreateTime desc";
            String strTable = " ExchangeGoods a left join  Exchange_Check b on a.EID =b.EID";
            String strField = " a.EID,a.ContractID,a.OrderID,a.ChangeDate,a.ReturnWay,a.State ,a.ISF,a.ISEXR,b.ProductionState ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
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
                    if (instData.DtData.Rows[r]["ReturnWay"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["ReturnWay"] = GetSelectPro(instData.DtData.Rows[r]["ReturnWay"].ToString()).Text;
                    }
                    if (instData.DtData.Rows[r]["State"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["State"] = GetStatePro(instData.DtData.Rows[r]["State"].ToString(), "ExChangeState").StateDesc;
                    }
                    if (instData.DtData.Rows[r]["ProductionState"].ToString() != "")
                    {
                        instData.DtData.Rows[r]["ProductionState"] = GetStatePro(instData.DtData.Rows[r]["ProductionState"].ToString(), "ProductionState").StateDesc;
                    }
                }

            }
            return instData;

        }

        public static UIDataTable LoadExchangeDetail(int a_intPageSize, int a_intPageIndex, string where)
        {

            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count(*) from Exchange_Detail  " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(6, where.Length - 6);
            }

            string strFilter = where;
            string strOrderBy = "CreateTime ";
            String strTable = " Exchange_Detail ";
            String strField = " EID ,DID,PID,ProductID,OrderContent ,Specifications ,Supplier ,Unit ,Amount ,ExUnit,ExTotal,ExUnitNo,ExTotalNo,Remark  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }

        public static UIDataTable LoadReturnDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = " select count(*) from ExReturn_Detail " + where;
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(6, where.Length - 6);
            }

            string strFilter = where;
            string strOrderBy = "CreateTime ";
            String strTable = " ExReturn_Detail ";
            String strField = " EID ,DID,ProductID,OrderContent ,Specifications ,Supplier ,Unit ,Amount ,ExUnit,ExTotal,ExUnitNo,ExTotalNo,Remark  ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            return instData;
        }

        public static DataTable LoadExchangeBill(string OrderID, ref string strErr)
        {
            try
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",OrderID)
                };
                DataTable dt = SQLBase.FillTable("getReceivePaymentRelatedData", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;
                //throw;
            }

        }

        public static DataTable GetExchangeGoodsDetail(string EID, ref string strErr)
        {
            try
            {
                string str = "select EID,DID ,EDID,ProductID ,OrderContent ,Specifications ,Supplier ,Unit ,Price,Subtotal,Amount ,ExUnitNo ,ExTotalNo,ExUnit ,ExTotal ,Remark,UnitCost,TotalCost,SaleNo,ProjectNO ,JNAME ,Technology  " +
               " from ExReturn_Detail where EID= '" + EID + "'";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                else
                {

                }
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }

        public static DataTable GetExcAndReturnDetailByEID(string EID, ref string strErr)
        {
            try
            {
                string str = "select * from Exchange_Detail where EID='" + EID + "'  ";//union all select * from Exchange_Detail 
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }


        public static DataTable GetExchangeGoodsDetailByDID(string DID, ref string strErr)
        {
            try
            {
                string str = "select EID,DID ,ProductID ,OrderContent ,Specifications ,Supplier ,Unit ,Amount ,ExUnitNo ,ExTotalNo,ExUnit ,ExTotal ,Remark " +
               " from Exchange_Detail where DID= '" + DID + "'";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }

        public static DataTable GetReturnDetailByDID(string DID, ref string strErr)
        {
            try
            {
                string str = "select EID,DID ,ProductID ,OrderContent ,Specifications ,Supplier ,Unit ,Amount ,ExUnitNo ,ExTotalNo,ExUnit ,ExTotal ,Remark " +
               " from ExReturn_Detail where DID= '" + DID + "'";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }

        public static DataTable GetReturnDetailByEID(string EID, ref string strErr)
        {
            try
            {
                string str = "select EID,DID ,EDID,ProductID ,OrderContent ,Specifications ,Supplier ,Unit ,Amount ,ExUnitNo ,ExTotalNo,ExUnit ,ExTotal ,Remark " +
               " from Exchange_Detail where EID= '" + EID + "'";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }

        public static DataTable GetExchangeGoodsCheckDetail(string EID, ref string strErr)
        {
            try
            {
                string str = "select * from ExchangeGoods_DetailInfo where DID like'%" + EID + "%' and Validate='v'";
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                return dt;
            }
            catch (Exception e)
            {
                strErr = e.Message;
                return null;

            }
        }
        public static ExchangeGoods GetExchangeGoodsByEID(string EID)
        {
            string str = " select * from ExchangeGoods where EID='" + EID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;

            //for (int r = 0; r < dt.Rows.Count; r++)
            //{
            //    if (dt.Rows[r]["ReturnType"].ToString() != "")
            //    {
            //        dt.Rows[r]["ReturnType"] = GetSelectPro(dt.Rows[r]["ReturnType"].ToString()).Text;
            //    }
            //    if (dt.Rows[r]["ReturnWay"].ToString() != "")
            //    {
            //        dt.Rows[r]["ReturnWay"] = GetSelectPro(dt.Rows[r]["ReturnWay"].ToString()).Text;
            //    }
            //    if (dt.Rows[r]["ReturnReason"].ToString() != "") { dt.Rows[r]["ReturnReason"] = GetSelectPro(dt.Rows[r]["ReturnReason"].ToString()).Text; }
            //}
            ExchangeGoods exchangegoods = new ExchangeGoods();

            foreach (DataRow item in dt.Rows)
            {
                //if (item["ReturnType"].ToString() != "")
                //{
                //    item["ReturnType"] = GetSelectPro(item["ReturnType"].ToString()).Text;
                //}
                //if (item["ReturnWay"].ToString() != "")
                //{
                //    item["ReturnWay"] = GetSelectPro(item["ReturnWay"].ToString()).Text;
                //}
                DataRowToExchangeGoods(item, exchangegoods);
            }
            return exchangegoods;
        }

        //打印用到的对象下拉框状态转换后的对象
        public static ExchangeGoods GetExchangeGoods(string EID)
        {
            string str = " select * from ExchangeGoods where EID='" + EID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                if (dt.Rows[r]["ReturnType"].ToString() != "")
                {
                    dt.Rows[r]["ReturnType"] = GetSelectPro(dt.Rows[r]["ReturnType"].ToString()).Text;
                }
                if (dt.Rows[r]["ReturnWay"].ToString() != "")
                {
                    dt.Rows[r]["ReturnWay"] = GetSelectPro(dt.Rows[r]["ReturnWay"].ToString()).Text;
                }
                if (dt.Rows[r]["ReturnReason"].ToString() != "") { dt.Rows[r]["ReturnReason"] = GetSelectPro(dt.Rows[r]["ReturnReason"].ToString()).Text; }
                if (dt.Rows[r]["ISEXR"].ToString() != "") { dt.Rows[r]["ISEXR"] = GetStatePro(dt.Rows[r]["ISEXR"].ToString(), "ISEXR").StateDesc; }

            }
            ExchangeGoods exchangegoods = new ExchangeGoods();

            foreach (DataRow item in dt.Rows)
            {
                //if (item["ReturnType"].ToString() != "")
                //{
                //    item["ReturnType"] = GetSelectPro(item["ReturnType"].ToString()).Text;
                //}
                //if (item["ReturnWay"].ToString() != "")
                //{
                //    item["ReturnWay"] = GetSelectPro(item["ReturnWay"].ToString()).Text;
                //}
                DataRowToExchangeGoods(item, exchangegoods);
            }
            return exchangegoods;
        }

        //根据订单编号获取退换货对象
        public static ExchangeGoods GetExchangeGoodsBYOrderID(string ID)
        {
            string str = " select * from  ExchangeGoods where EID =(select MAX(EID)EID from ExchangeGoods where OrderID ='" + ID + "')";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                if (dt.Rows[r]["ReturnType"].ToString() != "")
                {
                    dt.Rows[r]["ReturnType"] = GetSelectPro(dt.Rows[r]["ReturnType"].ToString()).Text;
                }
                if (dt.Rows[r]["ReturnWay"].ToString() != "")
                {
                    dt.Rows[r]["ReturnWay"] = GetSelectPro(dt.Rows[r]["ReturnWay"].ToString()).Text;
                }
            }
            ExchangeGoods exchangegoods = new ExchangeGoods();

            foreach (DataRow item in dt.Rows)
            {
                //if (item["ReturnType"].ToString() != "")
                //{
                //    item["ReturnType"] = GetSelectPro(item["ReturnType"].ToString()).Text;
                //}
                //if (item["ReturnWay"].ToString() != "")
                //{
                //    item["ReturnWay"] = GetSelectPro(item["ReturnWay"].ToString()).Text;
                //}
                DataRowToExchangeGoods(item, exchangegoods);
            }
            return exchangegoods;
        }
        public static void DataRowToExchangeGoods(DataRow item, ExchangeGoods exGoods)
        {
            exGoods.EID = item["EID"].ToString();
            exGoods.OrderID = item["OrderID"].ToString();
            exGoods.PayWay = item["PayWay"].ToString();
            exGoods.ReturnReason = item["ReturnReason"].ToString();
            exGoods.ReturnType = item["ReturnType"].ToString();
            exGoods.ReturnWay = item["ReturnWay"].ToString();
            exGoods.ReturnContract = item["ReturnContract"].ToString();
            exGoods.ReturnTotal = item["ReturnTotal"].ToString();
            exGoods.Methods = item["Methods"].ToString();
            exGoods.Brokerage = item["Brokerage"].ToString();
            if (item["ChangeDate"].ToString() != "")
                exGoods.ChangeDate = Convert.ToDateTime(item["ChangeDate"]).ToString("yyyy-MM-dd");
            exGoods.Reason = item["Reason"].ToString();
            exGoods.BrokerageRequire = item["BrokerageRequire"].ToString();
            exGoods.IsApproval1 = item["IsApproval1"].ToString();
            exGoods.Handle = item["Handle"].ToString();
            exGoods.State = item["State"].ToString();
            exGoods.ISEXR = item["ISEXR"].ToString();
            exGoods.ExFinishDate = item["ExFinishDate"].ToString();
            exGoods.ExFinishDescription = item["ExFinishDescription"].ToString();
            exGoods.ExFinishDealPeo = item["ExFinishDealPeo"].ToString();
            if (item["CreateTime"].ToString() != "")
                exGoods.CreateTime = Convert.ToDateTime(item["CreateTime"]);
            exGoods.CreateUser = item["CreateUser"].ToString();
            exGoods.ReturnInstructions = item["ReturnInstructions"].ToString();
            exGoods.Validate = item["Validate"].ToString();


        }


        public static bool SaveExchangeCheck(Exchange_Check check, List<ExchangeGoods_DetailInfo> listDetailInfo, ref string strErr)
        {
            try
            {
                string strInsertCheck = GSqlSentence.GetInsertInfoByD<Exchange_Check>(check, "Exchange_Check");
                string strInsertList = "";
                string strupdateChange = "update ExchangeGoods set State =3 where EID ='" + check.EID + "' ";
                if (listDetailInfo.Count > 0)
                {
                    strInsertList = GSqlSentence.GetInsertByList(listDetailInfo, "ExchangeGoods_DetailInfo");
                }
                if (strInsertCheck != "")
                {
                    SQLBase.ExecuteNonQuery(strupdateChange, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strInsertCheck, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strInsertList, "SalesDBCnn");
                }
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                // throw;
            }
        }


        public static bool CanCelExchangGoods(string EID, ref string strErr)
        {
            try
            {
                string strCancel = "update ExchangeGoods set Validate='i' where EID='" + EID + "'";
                string strInserExcHIS = "insert into  ExchangeGoods_HIS (EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,Methods,ReturnInstructions,"
                + " ExFinishDate,ExFinishDescription,ExFinishDealPeo,NCreateTime,NCreateUser) select EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,"
                + "Methods,ReturnInstructions,ExFinishDate,ExFinishDescription,ExFinishDealPeo,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ExchangeGoods where EID='" + EID + "'";
                if (strCancel != "")
                {
                    SQLBase.ExecuteNonQuery(strCancel, "SalesDBCnn");
                    SQLBase.ExecuteNonQuery(strInserExcHIS, "SalesDBCnn");
                }
                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                throw;
            }
        }

        public static bool SaveExchangeGoodsFinish(ExchangeGoods exGoods, ref string strErr)
        {
            try
            {
                string strUpdateFinish = " update ExchangeGoods set State ='4',ExFinishDate='" + exGoods.ExFinishDate + "',ExFinishDescription='" + exGoods.ExFinishDescription + "',ExFinishDealPeo='" + exGoods.ExFinishDealPeo + "' where EID='" + exGoods.EID + "'";
                string strInserExcHIS = "insert into  ExchangeGoods_HIS (EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,Methods,ReturnInstructions,"
               + " ExFinishDate,ExFinishDescription,ExFinishDealPeo,NCreateTime,NCreateUser) select EID,OrderID,Unit,Brokerage,ChangeDate,Reason,BrokerageRequire,IsApproval1,Handle,CreateUser,CreateTime,Validate,State,ReturnReason,ReturnTotal,ReturnType,ReturnWay,ReturnContract,PayWay,"
               + "Methods,ReturnInstructions,ExFinishDate,ExFinishDescription,ExFinishDealPeo,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ExchangeGoods where EID='" + exGoods.EID + "'";
                SQLBase.ExecuteNonQuery(strUpdateFinish, "SalesDBCnn");
                SQLBase.ExecuteNonQuery(strInserExcHIS, "SalesDBCnn");
                //修改订单的详细数据、退货、换货的数量
                string strEx = "update Orders_DetailInfo set ActualAmount= ActualAmount-c.Amount,ActualSubTotal=ActualSubTotal-c.ExTotal from Orders_DetailInfo  as a,Exchange_Detail as c where a.DID=c.EDID and a.DID in (select d.EDID from Exchange_Detail d where d.EID ='" + exGoods.EID + "')";//订单的数量减去退货的数量
                string strReturn = "update Orders_DetailInfo set ActualAmount= ActualAmount-b.Amount,ActualSubTotal=ActualSubTotal-b.Subtotal from Orders_DetailInfo  as a,ExReturn_Detail as b where a.DID=b.EDID  and  a.DID in (select d.EDID from ExReturn_Detail d where d.EID ='" + exGoods.EID + "') ";//订单数量减去换货货的数量
                string strGetExc = "select * from  ExReturn_Detail where EID ='" + exGoods.EID + "'";//获取换货数据
                //strjson = strjson.Substring(0, strjson.Length - 1);
                string OrderID = exGoods.OrderID;//获取订单ID
                DataTable dt2 = SQLBase.FillTable("select Max(DID)as DID from Orders_DetailInfo where OrderID='" + exGoods.OrderID + "'", "SalesDBCnn");
                string newDID = "";
                if (dt2 != null)
                {
                    newDID = dt2.Rows[0]["DID"].ToString();
                }
                string r = newDID.Substring(newDID.Length - 3, 3);

                DataTable dt = SQLBase.FillTable(strGetExc, "SalesDBCnn");
                int j = Convert.ToInt32(r);
                List<Orders_DetailInfo> list = new List<Orders_DetailInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    j++;
                    Orders_DetailInfo ordetail = new Orders_DetailInfo();
                    ordetail.OrderID = OrderID;
                    ordetail.DID = OrderID + string.Format("{0:D3}", j);
                    ordetail.ProductID = dt.Rows[i]["ProductID"].ToString();
                    ordetail.OrderContent = dt.Rows[i]["OrderContent"].ToString();
                    ordetail.SpecsModels = dt.Rows[i]["Specifications"].ToString();
                    ordetail.OrderUnit = dt.Rows[i]["Unit"].ToString();
                    ordetail.Manufacturer = dt.Rows[i]["Supplier"].ToString();
                    if (!String.IsNullOrEmpty(dt.Rows[i]["Amount"].ToString()))
                    {
                        ordetail.OrderNum = Convert.ToInt32(dt.Rows[i]["Amount"]);
                        ordetail.ActualAmount = Convert.ToInt32(dt.Rows[i]["Amount"]);
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[i]["Price"].ToString()))
                    {
                        ordetail.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[i]["Price"].ToString()))
                    {
                        ordetail.Subtotal = Convert.ToDecimal(dt.Rows[i]["Subtotal"]);
                        ordetail.ActualSubTotal = Convert.ToDecimal(dt.Rows[i]["Subtotal"]);
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[i]["UnitCost"].ToString()))
                    {
                        ordetail.UnitCost = Convert.ToDecimal(dt.Rows[i]["UnitCost"]);
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[i]["TotalCost"].ToString()))
                    {
                        ordetail.TotalCost = Convert.ToDecimal(dt.Rows[i]["TotalCost"]);
                    }
                    ordetail.SaleNo = dt.Rows[i]["SaleNo"].ToString();
                    ordetail.ProjectNo = dt.Rows[i]["ProjectNO"].ToString();
                    ordetail.JNAME = dt.Rows[i]["JNAME"].ToString();
                    ordetail.Technology = dt.Rows[i]["Technology"].ToString();
                    ordetail.State = "0";
                    ordetail.Remark = dt.Rows[i]["Remark"].ToString();
                    ordetail.Validate = "v";
                    ordetail.IState = "0";
                    ordetail.CreateTime = DateTime.Now.ToString();
                    ordetail.CreateUser = dt.Rows[i]["CreateUser"].ToString();
                    ordetail.DeliveryTime = DateTime.Now;
                    list.Add(ordetail);
                }
                string strInsertOrderDetail = "";
                if (list.Count > 0)
                {
                    strInsertOrderDetail = GSqlSentence.GetInsertByList(list, "Orders_DetailInfo");
                }

                //    string strUpdateOrder = "update OrdersInfo set OrderActualAmount =sum(b.ActualAmount),OrderActualTotal=SUM(b.ActualSubTotal) from  OrdersInfo a, Orders_DetailInfo b where a.OrderID ='" + OrderID + "' and  a.OrderID =b.OrderID ";

                string strUpdateOrder = "update OrdersInfo set OrderActualAmount =(select sum(b.ActualAmount) from Orders_DetailInfo b where b.OrderID ='" + OrderID + "'),OrderActualTotal=(select SUM(b.ActualSubTotal) from Orders_DetailInfo b where b.OrderID ='" + OrderID + "') where OrderID ='" + OrderID + "' ";
                if (strInsertOrderDetail != "")
                {

                    SQLBase.ExecuteNonQuery(strInsertOrderDetail, "SalesDBCnn");

                }
                SQLBase.ExecuteNonQuery(strEx, "SalesDBCnn");
                SQLBase.ExecuteNonQuery(strReturn, "SalesDBCnn");
                SQLBase.ExecuteNonQuery(strUpdateOrder, "SalesDBCnn");


                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                throw;
            }
        }

        public static DataTable ExchangeGoodsManageToExcel(string where, ref string strErr)
        {
            try
            {
                string str = "select a.EID,b.OrderID,a.ChangeDate,a.ReturnWay,a.State" +
            " from ExchangeGoods a left join OrdersInfo b on a.orderid=b.orderid where a.Validate='v' " + where;
                DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
                if (dt == null) return null;
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    if (dt.Rows[r]["ReturnWay"].ToString() != "")
                    {
                        dt.Rows[r]["ReturnWay"] = GetSelectPro(dt.Rows[r]["ReturnWay"].ToString()).Text;
                    }

                    if (dt.Rows[r]["State"].ToString() != "")
                    {
                        dt.Rows[r]["State"] = GetStatePro(dt.Rows[r]["State"].ToString(), "ExChangeState").StateDesc;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
                //  throw;
            }
        }

        public static UIDataTable GetExChangeGoodsByOID(int a_intPageSize, int a_intPageIndex, string OrderID)
        {
            try
            {
                UIDataTable instData = new UIDataTable();
                string strSelCount = "select count(*) from(select * from ExchangeGoods where OrderID ='" + OrderID + "' )as s";
                instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
                if (instData.IntRecords > 0)
                {
                    instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
                }
                else
                    instData.IntTotalPages = 0;


                string strFilter = " OrderID='" + OrderID + "'";
                string strOrderBy = " ChangeDate ";
                String strTable = " ExchangeGoods ";
                String strField = "EID,OrderID,Unit,ChangeDate,ReturnWay,State ,ReturnType,ReturnContract,ExFinishDate ,ExFinishDescription ,ExFinishDealPeo   ";
                instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
                // 
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
                        if (instData.DtData.Rows[r]["ReturnWay"].ToString() != "")
                        {
                            instData.DtData.Rows[r]["ReturnWay"] = GetSelectPro(instData.DtData.Rows[r]["ReturnWay"].ToString()).Text;
                        }
                        if (instData.DtData.Rows[r]["State"].ToString() != "")
                        {
                            instData.DtData.Rows[r]["State"] = GetStatePro(instData.DtData.Rows[r]["State"].ToString(), "ExChangeState").StateDesc;
                        }
                    }

                }
                return instData;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region 审批
        public static UIDataTable GetProjectBasApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetProjectBaseInfoApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                for (int r = 0; r < DO_Order.Tables[0].Rows.Count; r++)
                {
                    if (DO_Order.Tables[0].Rows[r]["Pstate"].ToString() != "")
                    {
                        DO_Order.Tables[0].Rows[r]["Pstate"] = GetStatePro(DO_Order.Tables[0].Rows[r]["Pstate"].ToString(), "ProjectState").StateDesc;
                    }
                    //if (DO_Order.Tables[0].Rows[r]["BelongArea"].ToString() != "")
                    //{
                    //    DO_Order.Tables[0].Rows[r]["BelongArea"] = GetSelectPro(DO_Order.Tables[0].Rows[r]["BelongArea"].ToString()).Text;
                    //}
                    if (DO_Order.Tables[0].Rows[r]["SpecsModels"].ToString() != "")
                    {
                        DO_Order.Tables[0].Rows[r]["SpecsModels"] = GetSelectPro(DO_Order.Tables[0].Rows[r]["SpecsModels"].ToString()).Text;
                    }
                }
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

        public static UIDataTable GetOfferApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            int userid = GAccount.GetAccountInfo().UserID;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                        new SqlParameter ("@UserID",userid)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOfferApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                //for (int r = 0; r < DO_Order.Tables[0].Rows.Count; r++)
                //{
                //    if (DO_Order.Tables[0].Rows[r]["State"].ToString() != "")
                //    {
                //        DO_Order.Tables[0].Rows[r]["State"] = GetStatePro(DO_Order.Tables[0].Rows[r]["State"].ToString()).StateDesc;
                //    }
                //}
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

        public static UIDataTable GetOrderApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            int userid = GAccount.GetAccountInfo().UserID;
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter ("@UserID",userid)
                };


            DataSet DO_Order = SQLBase.FillDataSet("GetOrderApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
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

        public static int InsertApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');

            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            string strUpdateBas = "";
            if (arr[2].IndexOf("..") > 0)
                strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            //string strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";

            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','提交" + webkey + "审批操作','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "')";
            string strUpdateOrder = "";
            if (arr[2].IndexOf("..") > 0)
            {
                strUpdateOrder = "update " + arr[2] + " set State = '" + (Convert.ToInt32(arr[5]) + 1) + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else if (arr[4] == "21")
            {
                strUpdateOrder = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + (Convert.ToInt32(arr[5])) + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else
            {
                strUpdateOrder = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + (Convert.ToInt32(arr[5]) + 1) + "' where " + arr[3] + " = '" + RelevanceID + "'";


            }
            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                if (strUpdateBas != "")
                    intUpdateBas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                if (strUpdateOrder != "")
                    sqlTrans.ExecuteNonQuery(strUpdateOrder, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intUpdateBas;
        }

        public static DataTable GetContractSPID(string CID)
        {
            string str = "select PID,RelevanceID ,State  from tk_Approval where RelevanceID ='" + CID + "' and Validate ='v'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static int UpdateApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            //获取PID
            //  string PID = GetApprovalPID(RelevanceID,account.UserID.ToString ());
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');
            int state = 0;
            if (IsPass == "是")
                state = 1;
            else
                state = -1;
            string strInsertBas = "update [" + arr[0] + "].." + arr[1] + " set ApprovalMan = '" + UserId + "',ApprovalTime = '" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',"
                    + "Remark = '" + Remark + "',State = '" + state + "' where PID = '" + PID + "' and ApprovalPersons = '" + UserId + "'";
            string strUpdateBas = "";
            if (state == -1)
            {
                if (arr[2].IndexOf("..") > 0)
                    strUpdateBas = "update " + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            if (strUpdateBas != "")
                intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, CommandType.Text, null);

            string str = "select ApprovalLevel,SUBSTRING(AppType,1,1) as Type,SUBSTRING(AppType,3,1) as num  from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '0'";
            DataTable dt = SQLBase.FillTable(str);
            string Type = "";
            int num = 0;
            int bol = 0;
            int Hcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Type = dt.Rows[i]["Type"].ToString();
                if (Type == "1")
                {
                    bol = 1;
                    break;
                }
                if (Type == "2")
                {
                    bol = 1;
                    break;
                }
                if (Type == "3")
                {
                    num = Convert.ToInt16(dt.Rows[i]["num"]);
                    string level = dt.Rows[i]["ApprovalLevel"].ToString();
                    // string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '1'";
                    string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '1' and AppType = '3-" + num + "'";
                    DataTable dt2 = SQLBase.FillTable(str);
                    if (dt2.Rows.Count > 0)
                        Hcount = Convert.ToInt16(dt2.Rows[0][0]);
                    if (Hcount < num)
                    {
                        bol = 1;
                        break;
                    }
                }
            }
            string strAllBas = "";
            string strAllOrderOstate = "";
            if (bol == 0)
            {
                #region 0427
                //if (arr[2].IndexOf("..") > 0)
                //    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                //else
                //    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                //修改订单的状态为审批中还是审批通过OState
                //  strAllOrderOstate = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'"; 
                #endregion
                if (state == -1)
                {
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                    strAllOrderOstate = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else
                {
                    strAllOrderOstate = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                }
            }
            else
            {
                strAllBas = "";
            }
            if (strAllBas != "")
            {
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, CommandType.Text, null);
                SQLBase.ExecuteNonQuery(strAllOrderOstate, CommandType.Text, null);
            }

            return intInsertBas;
        }

        public static string GetApprovalPID(string RelevanceID, string UserID)
        {
            string strsql = "select top 1 PID from  tk_approval where RelevanceID ='" + RelevanceID + "' and ApprovalPersons =" + UserID + " order by PID desc";
            DataTable dt = SQLBase.FillTable(strsql, "SalesDBCnn");
            string s = "";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    s = dt.Rows[0]["PID"].ToString();
                }
            }
            return s;
        }
        public static UIDataTable getConditionGrid11(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Data",arr[0]),
                    new SqlParameter("@Table",arr[1]),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCondition", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

            if (dtOrder != null)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    if (dtOrder.Rows[i]["Opinion"] != "")
                    {
                        dtOrder.Rows[i]["Opinion"] = dtOrder.Rows[i]["Opinion"].ToString().Replace("\n", "").Replace("\r", "");
                    }
                    if (dtOrder.Rows[i]["Remark"] != "")
                    {
                        dtOrder.Rows[i]["Remark"] = dtOrder.Rows[i]["Remark"].ToString().Replace("\n", "").Replace("\r", "");
                    }
                }
            }


            instData.DtData = dtOrder;
            return instData;
        }


        public static UIDataTable getConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Data",arr[0]),
                    new SqlParameter("@Table",arr[1]),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCondition", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static UIDataTable GetUserLogGrid(int a_intPageSize, int a_intPageIndex, string ReleVanceID)
        {
            UIDataTable instData = new UIDataTable();
            string strSelCount = "select count(*) from(select * from tk_UserLog where RelevanceID ='" + ReleVanceID + "' )as s";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;


            string strFilter = " RelevanceID='" + ReleVanceID + "'";
            string strOrderBy = " LogTime desc ";
            String strTable = " tk_UserLog ";
            String strField = " Relevanceid ,LogTitle ,LogContent,LogTime ,LogPerson ,Type";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }

            return instData;
        }

        #endregion

        #region [退货审批]

        public static UIDataTable EXGetProjectBasApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetProjectBaseInfoApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                for (int r = 0; r < DO_Order.Tables[0].Rows.Count; r++)
                {
                    if (DO_Order.Tables[0].Rows[r]["Pstate"].ToString() != "")
                    {
                        DO_Order.Tables[0].Rows[r]["Pstate"] = GetStatePro(DO_Order.Tables[0].Rows[r]["Pstate"].ToString(), "ProjectState").StateDesc;
                    }
                    //if (DO_Order.Tables[0].Rows[r]["BelongArea"].ToString() != "")
                    //{
                    //    DO_Order.Tables[0].Rows[r]["BelongArea"] = GetSelectPro(DO_Order.Tables[0].Rows[r]["BelongArea"].ToString()).Text;
                    //}
                    if (DO_Order.Tables[0].Rows[r]["SpecsModels"].ToString() != "")
                    {
                        DO_Order.Tables[0].Rows[r]["SpecsModels"] = GetSelectPro(DO_Order.Tables[0].Rows[r]["SpecsModels"].ToString()).Text;
                    }
                }
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

        public static UIDataTable EXGetOfferApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetOfferApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                //for (int r = 0; r < DO_Order.Tables[0].Rows.Count; r++)
                //{
                //    if (DO_Order.Tables[0].Rows[r]["State"].ToString() != "")
                //    {
                //        DO_Order.Tables[0].Rows[r]["State"] = GetStatePro(DO_Order.Tables[0].Rows[r]["State"].ToString()).StateDesc;
                //    }
                //}
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

        public static UIDataTable GetEXOrderApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetEXOrderApproval", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
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

        public static int EXInsertApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');

            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,EXState,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            string strInsertBas1 = "update  [" + arr[0] + "].." + arr[1] + " set EXState=0 where RelevanceID='" + RelevanceID + "' ";
            string strUpdateBas = "";
            if (arr[2].IndexOf("..") > 0)
                strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set EXState = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            //string strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";

            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','提交" + webkey + "审批操作','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "')";
            string strUpdateOrder = "";
            if (arr[2].IndexOf("..") > 0)
            {
                strUpdateOrder = "update " + arr[2] + " set EXState = '" + (Convert.ToInt32(arr[5]) + 1) + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else
            {
                //    strUpdateOrder = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + (Convert.ToInt32(arr[5]) + 1) + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            try
            {
                if (strInsertBas != "")
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                if (strUpdateBas != "")
                    intUpdateBas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                //if (strUpdateOrder != "")
                //    sqlTrans.ExecuteNonQuery(strUpdateOrder, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intUpdateBas;
        }

        public static DataTable EXGetContractSPID(string CID)
        {
            string str = "select PID,RelevanceID ,State  from tk_Approval where RelevanceID ='" + CID + "' and Validate ='v'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static int EXUpdateApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');
            int state = 0;
            if (IsPass == "是")
                state = 1;
            else
                state = -1;
            string strInsertBas = "update [" + arr[0] + "].." + arr[1] + " set ApprovalMan = '" + UserId + "',ApprovalTime = '" + DateTime.Now + "',IsPass = '" + IsPass + "',Opinion = '" + Opinion + "',"
                    + "Remark = '" + Remark + "',EXState = '" + state + "' where PID = '" + PID + "' and ApprovalPersons = '" + UserId + "'";
            string strUpdateBas = "";
            if (state == -1)
            {
                if (arr[2].IndexOf("..") > 0)
                    strUpdateBas = "update " + arr[2] + " set State = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set EXState = '" + arr[7] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            if (strUpdateBas != "")
                intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, CommandType.Text, null);

            string str = "select ApprovalLevel,SUBSTRING(AppType,1,1) as Type,SUBSTRING(AppType,3,1) as num  from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '0'";
            DataTable dt = SQLBase.FillTable(str);
            string Type = "";
            int num = 0;
            int bol = 0;
            int Hcount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Type = dt.Rows[i]["Type"].ToString();
                if (Type == "1")
                {
                    bol = 1;
                    break;
                }
                if (Type == "2")
                {
                    bol = 1;
                    break;
                }
                if (Type == "3")
                {
                    num = Convert.ToInt16(dt.Rows[i]["num"]);
                    string level = dt.Rows[i]["ApprovalLevel"].ToString();
                    string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + PID + "' and state = '1'";
                    DataTable dt2 = SQLBase.FillTable(str);
                    if (dt2.Rows.Count > 0)
                        Hcount = Convert.ToInt16(dt2.Rows[0][0]);
                    if (Hcount < num)
                    {
                        bol = 1;
                        break;
                    }
                }
            }
            string strAllBas = "";
            string strAllOrderOstate = "";
            if (bol == 0)
            {
                if (arr[2].IndexOf("..") > 0)
                    strAllBas = "update " + arr[2] + " set State = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strAllBas = "update [" + arr[0] + "].." + arr[2] + " set EXState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                //修改订单的状态为审批中还是审批通过OState
                //strAllOrderOstate = "update [" + arr[0] + "].." + arr[2] + " set OState = '" + arr[4] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else
            {
                strAllBas = "";
            }
            if (strAllBas != "")
            {
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, CommandType.Text, null);
                // SQLBase.ExecuteNonQuery(strAllOrderOstate, CommandType.Text, null);
            }

            return intInsertBas;
        }
        public static UIDataTable EXgetConditionGrid(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Data",arr[0]),
                    new SqlParameter("@Table",arr[1]),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCondition", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        public static int judgeLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            // 返回bol区别  -2审批未通过  -1- 不是审批人员 0-可以审批 1-已经审批 2-还没有到该人员审批
            int bol = 0;
            int Level = 0;
            int State = 0;
            string AppType = "";
            int haveCount = 0;
            int count = 0;
            int countl = 0;
            string[] arr = folderBack.Split('/');
            string strSql = "select ApprovalLevel,State,SUBSTRING(AppType,1,1) as AppType,SUBSTRING(AppType,3,1) as num from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalPersons = '" + userid + "' order by PID desc";
            DataTable dt = SQLBase.FillTable(strSql);
            if (dt.Rows.Count == 0 || dt == null)
            {
                bol = -1;
                return bol;
            }
            Level = Convert.ToInt16(dt.Rows[0]["ApprovalLevel"]);
            State = Convert.ToInt16(dt.Rows[0]["State"]);
            int lastLevel = Level - 1;
            if (Level == 0)
            {
                if (State == 0)
                {
                    bol = 0;
                    return bol;
                }
                else
                {
                    bol = 1;
                    return bol;
                }
            }
            else
            {
                if (State == 1)
                {
                    bol = 1;
                    return bol;
                }
                string strl = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and AppType = '2'  and ApprovalLevel < '" + Level + "' and state = '0'";
                DataTable dtl = SQLBase.FillTable(strl);
                if (dtl.Rows.Count > 0)
                    countl = Convert.ToInt16(dtl.Rows[0][0]);
                if (countl > 0)
                {
                    bol = 2;
                    return bol;
                }

                string str = "select SUBSTRING(AppType,1,1) as AppType from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt1 = SQLBase.FillTable(str);
                string str2 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt3 = SQLBase.FillTable(str2);
                string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "' and state = '1'";
                DataTable dt2 = SQLBase.FillTable(str1);
                if (dt1.Rows.Count > 0)
                    AppType = dt1.Rows[0]["AppType"].ToString();
                if (dt3.Rows.Count > 0)
                    haveCount = Convert.ToInt16(dt3.Rows[0][0]);
                if (dt2.Rows.Count > 0)
                    count = Convert.ToInt16(dt2.Rows[0][0]);
                if (AppType == "1")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                if (AppType == "2")
                {
                    if (count != haveCount)
                    {
                        bol = 2;
                    }
                    else
                    {
                        bol = 0;
                    }
                }
                if (AppType == "3")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                return bol;
            }
        }


        public static int ExjudgeLoginUser(string userid, string webkey, string folderBack, string SPID)
        {
            // 返回bol区别  -2审批未通过  -1- 不是审批人员 0-可以审批 1-已经审批 2-还没有到该人员审批
            int bol = 0;
            int Level = 0;
            int State = 0;
            string AppType = "";
            int haveCount = 0;
            int count = 0;
            int countl = 0;
            string[] arr = folderBack.Split('/');
            string strSql = "select ApprovalLevel,ExState,SUBSTRING(AppType,1,1) as AppType,SUBSTRING(AppType,3,1) as num from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalPersons = '" + userid + "' order by PID desc";
            DataTable dt = SQLBase.FillTable(strSql);
            if (dt.Rows.Count == 0 || dt == null)
            {
                bol = -1;
                return bol;
            }
            Level = Convert.ToInt16(dt.Rows[0]["ApprovalLevel"]);
            State = Convert.ToInt16(dt.Rows[0]["ExState"]);
            int lastLevel = Level - 1;
            if (Level == 0)
            {
                if (State == 0)
                {
                    bol = 0;
                    return bol;
                }
                else
                {
                    bol = 1;
                    return bol;
                }
            }
            else
            {
                if (State == 1)
                {
                    bol = 1;
                    return bol;
                }
                string strl = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and AppType = '2'  and ApprovalLevel < '" + Level + "' and state = '0'";
                DataTable dtl = SQLBase.FillTable(strl);
                if (dtl.Rows.Count > 0)
                    countl = Convert.ToInt16(dtl.Rows[0][0]);
                if (countl > 0)
                {
                    bol = 2;
                    return bol;
                }

                string str = "select SUBSTRING(AppType,1,1) as AppType from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt1 = SQLBase.FillTable(str);
                string str2 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "'";
                DataTable dt3 = SQLBase.FillTable(str2);
                string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "' and ApprovalLevel = '" + lastLevel + "' and state = '1'";
                DataTable dt2 = SQLBase.FillTable(str1);
                if (dt1.Rows.Count > 0)
                    AppType = dt1.Rows[0]["AppType"].ToString();
                if (dt3.Rows.Count > 0)
                    haveCount = Convert.ToInt16(dt3.Rows[0][0]);
                if (dt2.Rows.Count > 0)
                    count = Convert.ToInt16(dt2.Rows[0][0]);
                if (AppType == "1")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                if (AppType == "2")
                {
                    if (count != haveCount)
                    {
                        bol = 2;
                    }
                    else
                    {
                        bol = 0;
                    }
                }
                if (AppType == "3")
                {
                    //if (count >= 1)
                    //{
                    bol = 0;
                    //}
                    //else
                    //{
                    //    bol = 2;
                    //}
                }
                return bol;
            }
        }
        #endregion
        #region [结算]

        public static string getNewFinishID()
        {
            string strID = "";
            string strD = "JS" + DateTime.Now.ToString("yyMMdd");
            string strSqlID = "select max(PID) from ProFinish";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SalesDBCnn");
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
        public static bool InsertProFinish(ProFinish profinish, ref string strErr)
        {
            try
            {
                string strInsert = GSqlSentence.GetInsertInfoByD<ProFinish>(profinish, "ProFinish");

                string strInsertHIS = "insert into ProFinish_HIS (PID,OrderID,Amount,IsDebt,DebtAmount,DebtReason,HasExchange,AlterAmount,CreateUser,CreateTime,Validate,NCreateTime,NCreateUser)" +
            "select PID,OrderID ,Amount,IsDebt,DebtAmount,DebtReason,HasExchange,AlterAmount,CreateUser,CreateTime,Validate,'" + DateTime.Now + "','" + GAccount.GetAccountInfo().UserName + "' from ProFinish";
                int i = 0, j = 0;
                string updateOrderOstate = "update OrdersInfo set Ostate='5' where OrderID='" + profinish.OrderID + "'";
                if (strInsert != "")
                {
                    i = SQLBase.ExecuteNonQuery(strInsert, "SalesDBCnn");
                    j = SQLBase.ExecuteNonQuery(strInsertHIS, "SalesDBCnn");
                    //修改订单的状态为已结算
                    SQLBase.ExecuteNonQuery(updateOrderOstate, "SalesDBCnn");
                    //1210k修改订单的欠款额为0

                }
                if (i + j >= 2)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
                throw;
            }
        }

        public static decimal getDebtAmount(string OrderID)
        {
            decimal Debt = 0;
            string strSql = "select Total  from OrdersInfo where OrderID ='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SalesDBCnn");
            decimal TotalAmout = 0;
            decimal CBeginAmount = 0;
            decimal CEndAmount = 0;
            if (dt.Rows.Count > 0)
            {
                CBeginAmount = Convert.ToDecimal(dt.Rows[0]["Total"]);
                // CEndAmount = Convert.ToDecimal(dt.Rows[0]["CEndAmount"]);
            }
            if (CEndAmount != 0)
                TotalAmout = CEndAmount;
            else
                TotalAmout = CBeginAmount;
            decimal ReturnAmout = 0;
            string str = "select Amount  from ReceivePayment  where OrderID='" + OrderID + "'";
            DataTable dt2 = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    ReturnAmout += Convert.ToDecimal(dt2.Rows[i]["Amount"]);
                }
            }
            Debt = TotalAmout - ReturnAmout;
            return Debt;
        }

        public static string GetContractID(string OrderID)
        {
            string str = "select ContractID from OrdersInfo where OrderID='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            string ContractID = "";
            if (dt == null) return null;
            foreach (DataRow item in dt.Rows)
            {
                ContractID = item["ContractID"].ToString();
            }
            return ContractID;
        }
        #endregion


        #region [统计分析]
        public static DataTable GetStatisticsManageTable(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   //new SqlParameter("@PageSize",a_intPageSize.ToString()),
                   // new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("GetStatisticsManageTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }
        public static string getCountDebt(string where)
        {
            string countStr = "";
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("getCountDebtStatistics", CommandType.StoredProcedure, sqlPar, "MainProject");

            if (instData.Rows.Count > 0)
                countStr = instData.Rows[0][0].ToString();
            return countStr;
        }

        public static UIDataTable GetOrdersInfoStatisticalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    //new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    //new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };


            string strSelCount = "select COUNT(*) from(select c.SpecsModels,a.OrderID,a.ContractID ,a.OrderUnit,c.OrderContent,c.OrderUnit as OrderDUnit , c.OrderNum,a.ContractDate,c.DeliveryTime ,d.ShipmentDate, c.ShipmentNum ,d.ShipmentDate as SJFHDate,c.OrderNum-c.ShipmentNum as WFSL,c.ProductID,a.OrderAddress ,a.OrderTel ,a.Remark  from  OrdersInfo a left join Shipments d on a.OrderID =d.OrderID left join Orders_DetailInfo c on a.OrderID =c.OrderID  Where a.SalesType='Sa01'" + where + ")as s";
            instData.IntRecords = GFun.SafeToInt32(SQLBase.ExecuteScalar(strSelCount, "SalesDBCnn"));
            if (instData.IntRecords > 0)
            {
                instData.IntTotalPages = instData.IntRecords % a_intPageSize == 0 ? instData.IntRecords / a_intPageSize : instData.IntRecords / a_intPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    strWhere = strWhere.Substring(6, strWhere.Length - 6);
            //}
            string strFilter = " SalesType='Sa01' " + where;
            string strOrderBy = " a.CreateTime desc ";
            String strTable = "OrdersInfo a left join Shipments d on a.OrderID =d.OrderID left join Orders_DetailInfo c on a.OrderID =c.OrderID  ";
            String strField = "c.SpecsModels,a.OrderID,a.ContractID ,a.OrderUnit,c.OrderContent,c.OrderUnit as OrderDUnit , c.OrderNum,a.ContractDate,c.DeliveryTime ,d.ShipmentDate, c.ShipmentNum ,d.ShipmentDate as SJFHDate,c.OrderNum-c.ShipmentNum as WFSL,c.ProductID,a.OrderAddress ,a.OrderTel ,a.Remark ";
            instData.DtData = SQLBase.FillTable(strField, strTable, strOrderBy, strFilter, a_intPageSize, a_intPageIndex, "SalesDBCnn");
            // 
            if (instData == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
            }
            //if (instData.DtData != null)
            //{
            //    for (int r = 0; r < instData.DtData.Rows.Count; r++)
            //    {
            //        instData.DtData.Rows[r]["Ostate"] = GetStatePro(instData.DtData.Rows[r]["Ostate"].ToString(), "OrderState").StateDesc;
            //    }
            //}
            return instData;
        }

        public static string GetOrderStaticstical(string where)
        {
            string countStr = "";
            string str = "select COUNT(*) from OrdersInfo a left join Orders_DetailInfo b on a.OrderID =b.OrderID where a.Validate ='v' and 1=1 " + where + "";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            string Count = "";
            if (dt.Rows.Count > 0)
                Count = dt.Rows[0][0].ToString();
            string str1 = "select SUM(b.Subtotal ) from OrdersInfo a left join Orders_DetailInfo b  on a.OrderID =b.OrderID where 1=1 " + where + "";
            DataTable dt1 = SQLBase.FillTable(str1, "SalesDBCnn");
            string Total = "";
            if (dt1.Rows.Count > 0)
                Total = dt1.Rows[0][0].ToString();
            //string str2 = "select count(*) from tk_ProjectBas b where 1=1 and b.ValiDate = 'v' and State != '8' " + where + "";
            //DataTable dt2 = SQLBase.FillTable(str2, "MainProject");
            //string NoFinish = "";
            //if (dt2.Rows.Count > 0)
            //    NoFinish = dt2.Rows[0][0].ToString();
            countStr = Count + "@" + Total;
            return countStr;

        }

        public static DataTable GetOrderStaticalGrid(string where)
        {
            string str = "select SpecsModels=(stuff((select '',''+SpecsModels from Orders_DetailInfo b where b.OrderID = a.OrderID for xml path('')),1,1,'')),a.ContractID,a.OrderID ,a.OrderUnit,a.OrderContactor,c.OrderUnit,c.OrderNum,c.ShipmentNum ,c.OrderNum-c.ShipmentNum as WFSL,a.ContractDate,a.OrderTel,a.OrderAddress,a.UseUnit,a.Total,a.UseContactor,a.UseTel,a.UseAddress,a.IsHK,a.State,a.Ostate,a.Ostate as OOstate from  OrdersInfo a left join [BGOI_BasMan]..tk_ContractBas b on a.ContractID=b.ContractID left join Orders_DetailInfo c on a.OrderID =c.OrderID  Where a.SalesType='Sa01' " + where + "";

            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;

        }
        //年度累计汇总统计分析
        public static DataTable SalesSummaryTable(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   //new SqlParameter("@PageSize",a_intPageSize.ToString()),
                   // new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("GetSalesSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;
            else
            {
                for (int i = 0; i < instData.Rows.Count; i++)
                {
                    if (instData.Rows[i]["SubTotal"].ToString() != "" && instData.Rows[i]["HK"].ToString() != "")
                    {
                        if (Convert.ToInt64(instData.Rows[i]["SubTotal"]) != 0)
                        {
                            instData.Rows[i]["HKL"] = Convert.ToDouble(Convert.ToDouble(instData.Rows[i]["HK"]) / Convert.ToDouble(instData.Rows[i]["SubTotal"])).ToString("P");
                            // Convert.ToDouble(dValue).ToString("P");//
                        }
                    }
                }

            }
            return instData;
        }

        public static string GetSalesSummary(string where)
        {
            string countStr = "";
            string str = "select COUNT(*) from ( select SUM(Total) as SubTotal,CreateUser ,(select SUM(Amount) from ReceivePayment b where a.CreateUser=b.CreateUser group by CreateUser)as HK,(isnull(SUM(Total),0)-IsNULL((select SUM(Amount) from ReceivePayment b where a.CreateUser=b.CreateUser group by CreateUser),0))as Qk from OrdersInfo a " + where + " group by a.CreateUser )as t";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            string Count = "";
            if (dt.Rows.Count > 0)
                Count = dt.Rows[0][0].ToString();
            string str1 = "Select SUM(SubTotal) from (select CreateUser ,SUM(Total) as SubTotal,(select SUM(Amount) from ReceivePayment b where a.CreateUser=b.CreateUser group by CreateUser)as HK,(isnull(SUM(Total),0)-IsNULL((select SUM(Amount) from ReceivePayment b where a.CreateUser=b.CreateUser group by CreateUser),0))as Qk from OrdersInfo a " + where + " group by a.CreateUser  ) AS TEMPTABLE ";
            DataTable dt1 = SQLBase.FillTable(str1, "SalesDBCnn");
            string Total = "";
            if (dt1.Rows.Count > 0)
                Total = dt1.Rows[0][0].ToString();
            countStr = Count + "@" + Total;
            return countStr;

        }

        //本月销售汇总
        public static DataTable GetMonthsSalesSummaryTable(string where)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   //new SqlParameter("@PageSize",a_intPageSize.ToString()),
                   // new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            instData = SQLBase.FillTable("GetMonthsSalesSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }

        public static DataTable GetEquipmentSalesSummaryTable(string StartDate, string EndDate)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetEquipmentSalesSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }


        public static DataTable GetPressureRegulatingBoxTable(string StartDate, string EndDate)
        {

            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetPressureRegulatingBoxTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;
            else
            {
                for (int i = 0; i < instData.Rows.Count; i++)
                {
                    if (instData.Rows[i]["ISF"].ToString() != "")
                    {

                        instData.Rows[i]["ISF"] = GetStatePro(instData.Rows[i]["ISF"].ToString(), "ISF").StateDesc;

                    }
                }

            }
            return instData;
        }

        public static DataTable GetHighVoltageCompartmentTable(string StartDate, string EndDate)
        {

            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetHighVoltageCompartmentTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }


        public static DataTable GetCutOffSalesSummaryTable(string StartDate, string EndDate)
        {

            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetCutOffSalesSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }
        public static DataTable GetRegulatorSummaryTable(string StartDate, string EndDate)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetRegulatorSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }

        public static DataTable GetOtherEquipmentTable(string StartDate, string EndDate)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

            instData = SQLBase.FillTable("GetRegulatorSummaryTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");
            if (instData == null) return null;

            return instData;
        }
        #endregion

        #region [经营分析]

        public static DataTable GetContractStatisticalAnalysisTable(string DateTime, string LastMonthTime, string LastYearTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   //new SqlParameter("@PageSize",a_intPageSize.ToString()),
                   // new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@DateTime",DateTime),
                     new SqlParameter("@LastMonthTime",LastMonthTime),
                     new SqlParameter("@LastYearTime",LastYearTime)
                };

            instData = SQLBase.FillTable("GetContractStatisticalAnalysisTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;

        }

        public static DataTable GetContractNowStatisticalAnalysisTable(string StartTime, string EndTime, string LastStartTime, string LastEndTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@StartDateTime",StartTime ),
                     new SqlParameter("@EndDateTime",EndTime),
                     new SqlParameter("@LastStartTime",LastStartTime),
                     new SqlParameter ("@LastEndTime",LastEndTime)
                };

            instData = SQLBase.FillTable("GetContractNowStatisticalAnalysisTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }


        public static DataTable GetReceivableAccountTable(string StartTime, string EndTime, string LastStartTime, string LastEndTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@StartDateTime",StartTime ),
                     new SqlParameter("@EndDateTime",EndTime),
                     new SqlParameter("@LastStartTime",LastStartTime),
                     new SqlParameter ("@LastEndTime",LastEndTime)
                };

            instData = SQLBase.FillTable("GetContractNowStatisticalAnalysisTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }

        public static DataTable GetReceivableAccountTable(string DateTime, string LastDateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@DateTime",DateTime  ),
                     new SqlParameter("@LastDateTime",LastDateTime)
                   
                };

            instData = SQLBase.FillTable("GetReceivableAccountTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }


        public static DataTable GetDeceiveReceivableAccountTable(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@DateTime",DateTime  )
                  //   new SqlParameter("@LastDateTime",LastDateTime)
                   
                };

            instData = SQLBase.FillTable("GetDeveiceReceivableAccountTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }

        /// <summary>
        /// 应收款统计累计应收款情况
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        ///
        public static DataTable getLJYSKReceivableTable(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@DateTime",DateTime  )
                  //   new SqlParameter("@LastDateTime",LastDateTime)
                   
                };

            instData = SQLBase.FillTable("GetLJYSKReceivableTable", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");


            if (instData != null)
            {

                for (int i = 0; i < instData.Rows.Count; i++)
                {

                    if (instData.Rows[i]["Less3"].ToString() != "" && instData.Rows[i]["Less3"].ToString() == "1")
                    {
                        instData.Rows[i]["Less3"] = "√";
                    }
                    if (instData.Rows[i]["Greater3Less6"].ToString() != "" && instData.Rows[i]["Greater3Less6"].ToString() == "1")
                    {

                        instData.Rows[i]["Greater3Less6"] = "√";
                    }
                    if (instData.Rows[i]["Greater6Less1"].ToString() != "" && instData.Rows[i]["Greater6Less1"].ToString() == "1")
                    {

                        instData.Rows[i]["Greater6Less1"] = "√";
                    }
                    if (instData.Rows[i]["Greater1Less2"].ToString() != "" && instData.Rows[i]["Greater1Less2"].ToString() == "1")
                    {

                        instData.Rows[i]["Greater1Less2"] = "√";
                    }
                    if (instData.Rows[i]["Greater2"].ToString() != "" && instData.Rows[i]["Greater2"].ToString() == "1")
                    {

                        instData.Rows[i]["Greater2"] = "√";
                    }
                }
            }
            return instData;
        }


        /// <summary>
        ///应收款统计分析二
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 

        public static DataTable GetAccountsPayableStatisticalAnalysis2Table(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@DateTime",DateTime )
                };

            instData = SQLBase.FillTable("GetAccountsPayableStatisticalAnalysis2", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");


            if (instData != null)
            {

                for (int i = 0; i < instData.Rows.Count; i++)
                {

                    //if (instData.Rows[i]["Less3"].ToString() != "" && instData.Rows[i]["Less3"].ToString() == "1")
                    //{
                    //    instData.Rows[i]["Less3"] = "√";
                    //}
                    //if (instData.Rows[i]["Greater3Less6"].ToString() != "" && instData.Rows[i]["Greater3Less6"].ToString() == "1")
                    //{

                    //    instData.Rows[i]["Greater3Less6"] = "√";
                    //}
                    //if (instData.Rows[i]["Greater6Less1"].ToString() != "" && instData.Rows[i]["Greater6Less1"].ToString() == "1")
                    //{

                    //    instData.Rows[i]["Greater6Less1"] = "√";
                    //}
                    //if (instData.Rows[i]["Greater1Less2"].ToString() != "" && instData.Rows[i]["Greater1Less2"].ToString() == "1")
                    //{

                    //    instData.Rows[i]["Greater1Less2"] = "√";
                    //}
                    //if (instData.Rows[i]["Greater2"].ToString() != "" && instData.Rows[i]["Greater2"].ToString() == "1")
                    //{

                    //    instData.Rows[i]["Greater2"] = "√";
                    //}
                }
            }
            return instData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        public static DataTable GetAccountsPayableNotMonthTable(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@DateTime",DateTime )
                };

            instData = SQLBase.FillTable("AccountsPayableNotMonth", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");



            return instData;
        }


        public static DataTable getAccountsPayableYearsTable()
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    
                };

            instData = SQLBase.FillTable("AccountsPayableYears", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");



            return instData;
        }


        /// <summary>
        /// 自有产品的销售量
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        public static DataTable GetMonthOwnProductSalesAmount(string DateTime, string LastMonthDatetime, string LastYearDatetime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@DateTime",DateTime ),
                    new SqlParameter  ("@LastMonthDateTime",LastMonthDatetime),
                    new SqlParameter ("@LastYearDateTime",LastYearDatetime)
                };

            instData = SQLBase.FillTable("GetMonthOwnProductSales", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");



            return instData;
        }

        /// <summary>
        /// 自有产品销售额，合同额
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetMonthOwnProductSalesTotal(string DateTime, string LastMonthDateTime, string LastYearDateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@DateTime",DateTime ),
                    new SqlParameter  ("@LastMonthDateTime",LastMonthDateTime),
                    new SqlParameter ("@LastYearDateTime",LastYearDateTime)
                };

            instData = SQLBase.FillTable("GetMonthOwnProductSalesTotal", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");



            return instData;
        }

        /// <summary>
        /// 自有产品累计销售量
        /// </summary>
        /// <param name="ProID"></param>
        /// <param name="SupID"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetMonthlyAountOwnProducts(string StartDateTime, string EndDateTime, string LastStartDateTime, string LastEndDateTime)
        {

            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@StartDateTime",StartDateTime  ),
                    new SqlParameter  ("@EndDateTime",EndDateTime ),
                    new SqlParameter ("@LastStartDateTime",LastStartDateTime),
                    new SqlParameter ("@LastEndDateTime",LastEndDateTime )
                };

            instData = SQLBase.FillTable("GetMonthlyAountOwnProducts", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");



            return instData;
        }

        /// <summary>
        /// 自有产品累计销售额
        /// </summary>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <param name="LastStartDateTime"></param>
        /// <param name="LastEndDateTime"></param>
        /// <returns></returns>
        public static DataTable GetMonthlyTotalOwnProducts(string StartDateTime, string EndDateTime, string LastStartDateTime, string LastEndDateTime)
        {

            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@StartDateTime",StartDateTime  ),
                    new SqlParameter  ("@EndDateTime",EndDateTime ),
                    new SqlParameter ("@LastStartDateTime",LastStartDateTime),
                    new SqlParameter ("@LastEndDateTime",LastEndDateTime )
                };

            instData = SQLBase.FillTable("GetMonthlyTotalOwnProducts", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;


        }

        public static DataTable GetMonthOwnProductChannelsFrom(string Datetime, string StartDateTime, string EndDateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@StartDateTime",StartDateTime),
                    new SqlParameter  ("@EndDateTime",EndDateTime ),
                    new SqlParameter ("@DateTime",Datetime)
                  
                };

            instData = SQLBase.FillTable("GetMonthOwnProductChannelsFrom", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }


        public static DataTable GetMonthOwnProductModelAountTop10(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@DateTime",DateTime)
                 //   new SqlParameter  ("@EndDateTime",EndDateTime ),
                   // new SqlParameter ("@DateTime",Datetime)
                  
                };

            instData = SQLBase.FillTable("GetMonthOwnProductModelAountTop10", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }

        public static DataTable GetMonthOwnProductModelTotalTop10(string DateTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@DateTime",DateTime)
                 //   new SqlParameter  ("@EndDateTime",EndDateTime ),
                   // new SqlParameter ("@DateTime",Datetime)
                  
                };

            instData = SQLBase.FillTable("GetMonthOwnProductModelTotalTop10", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }


        public static DataTable GetMonthOwnProductModelFromToAountTop10(string DateTime, string EndTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter ("@DateTime",DateTime),
                   new SqlParameter  ("@EndTime",EndTime )
                   // new SqlParameter ("@DateTime",Datetime)
                  
                };

            instData = SQLBase.FillTable("GetMonthOwnProductFromToAountTop10", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }

        public static DataTable GetMonthOwnProductModelFromToTotalTop10(string DateTime, string EndTime)
        {
            DataTable instData = new DataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                   new SqlParameter ("@DateTime",DateTime),
                   new SqlParameter  ("@EndTime",EndTime )
                };

            instData = SQLBase.FillTable("GetMonthOwnProductFromToTotalTop10", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

            return instData;
        }

        #endregion


        #region [库存查询]

        public static UIDataTable GetInventoryGrid(int IntPageSize, int IntPageIndex, string Ptype, string Where)
        {
            UIDataTable instData = new UIDataTable();
            string unitid = GAccount.GetAccountInfo().UnitID;
            string username = GAccount.GetAccountInfo().UserName;
            //  string rolename = GAccount.GetAccountInfo().RoleNames;

            string ptype = "";
            if (Ptype != "")
            {
                if (Ptype == "PT00") { }
                else
                {
                    ptype = " and Ptype='" + Ptype + "'";
                }
            }

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",IntPageSize.ToString()),
                    new SqlParameter("@StartIndex",(IntPageSize * IntPageIndex).ToString()),
                    new SqlParameter("@Where",Where),
                    new SqlParameter("@UnitID",unitid),
                    new SqlParameter ("@Ptype",ptype)
                };

            DataSet DO_Order = SQLBase.FillDataSet("GetInventoryGrid", CommandType.StoredProcedure, sqlPar, "SalesDBCnn");

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
                if (instData.IntRecords % IntPageSize == 0)
                    instData.IntTotalPages = instData.IntRecords / IntPageSize;
                else
                    instData.IntTotalPages = instData.IntRecords / IntPageSize + 1;
            }
            else
                instData.IntTotalPages = 0;

            instData.DtData = dtOrder;
            return instData;
        }


        #endregion

        #region [获取物品单价]
        public static DataTable GetProductPrice(string ProID, string SupID)
        {
            string Str = "select b.price from tk_SupplierBas a inner join tk_SProducts b on a.SID =b.SID where b.ProductID ='" + ProID + "' and a.COMNameC ='" + SupID + "'";
            DataTable dt = SQLBase.FillTable(Str, "SupplyCnn");
            if (dt == null) return null;
            return dt;
        }

        #endregion

        #region MyRegion
        public static string GetNamePY(string LoginName)
        {
            string str = "select dbo.fGetPy('" + LoginName + "')";
            DataTable dt = SQLBase.FillTable(str, "AccountCnn");
            if (dt == null) return null;
            return dt.Rows[0][0].ToString();
        }
        #endregion

        #region [上传合同文件]
        public static int InsertFile(string id, byte[] fileByte, string FileName, ref string a_strErr)
        {
            int intInsert = 0;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };

            Acc_Account account = GAccount.GetAccountInfo();
            string[] arr = FileName.Split('.');
            string FileType = "";
            if (arr.Length > 0)
            {
                FileType = arr[1];
            }
            SQLTrans sqlTrans = new SQLTrans();
            string strInsertOrder = "insert into tk_CFile (CID,FileInfo,FileName,CreateTime,CreateUser,FileType,Validate) values ('" + id + "', @fileByte,'" + FileName + "','" + DateTime.Now + "','" + account.UserName.ToString() + "','" + FileType + "','v')";
            //160118K
            string UpdateOrderISHT = "";
            if (id != "")
            {
                UpdateOrderISHT = "update OrdersInfo set ISHT=1 where OrderID='" + id + "'";
            }
            //160118K
            try
            {
                sqlTrans.Open("SalesDBCnn");
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, para);
                if (UpdateOrderISHT != "")
                    intInsert += sqlTrans.ExecuteNonQuery(UpdateOrderISHT);
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
        //合同
        public static DataTable GetDownload(string id)
        {
            string strSql = "select ID,CID,[FileInfo],FileName from tk_CFile where CID = '" + id + "' and Validate = 'v' ";//GetFile
            DataTable dt = SQLBase.FillTable(strSql, "SalesDBCnn");
            return dt;
        }
        //附件

        public static DataTable GetUploadFile(string id)
        {
            string strSql = "select ID,CID,[FileInfo],FileName from tk_CFile where CID = '" + id + "' and Validate = 'v' and (FileType!='docx' and FileType!='doc')";
            DataTable dt = SQLBase.FillTable(strSql, "SalesDBCnn");
            return dt;
        }
        public static int DeleteFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SalesDBCnn");

            string strInsertOrder = "delete from tk_CFile where ID = '" + ID + "'";
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

        public static DataTable GetDownloadFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_CFile where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SalesDBCnn");
            return dt;
        }

        public static int UpdateCCashBack(CCashBack Cash, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SalesDBCnn");
            string strInsert = "update tk_CCashBack set CurAmountNum = '" + Cash.StrCurAmountNum + "',CBMethod = '" + Cash.StrCBMethod + "',CBMoney = '" + Cash.StrCBMoney + "',CBBillNo = '" + Cash.StrCBBillNo + "',ReceiptNo = '" + Cash.StrReceiptNo + "',IsReturn = '" + Cash.StrIsReturn + "',"
                + "NoReturnReason = '" + Cash.StrNoReturnReason + "',PayCompany = '" + Cash.StrPayCompany + "',Remark = '" + Cash.StrRemark + "',CBDate = '" + Cash.StrCBDate + "' where CBID = '" + Cash.StrCBID + "'";
            string strInsertLog = "insert into tk_UserLog values ('" + Cash.StrCID + "','修改合同回款操作','修改回款成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
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

        public static int dellCCashBack(string id, string cid, ref string a_strErr)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int intInsertLog = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SalesDBCnn");
            string strInsert = "update tk_CFile set Validate = 'i' where ID = '" + id + "'";
            //string strInsertLog = "insert into tk_UserLog values ('" + cid + "','撤销合同回款操作','撤销成功','" + DateTime.Now + "','" + account.UserName + "','合同')";
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                //if (strInsertLog != "")
                //    intInsertLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
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

        #region MyRegion
        public static DataTable GetOrderPstate(string OrderID)
        {
            string str = "select Pstate from OrdersInfo where OrderID ='" + OrderID + "'";
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        public static DataTable GetOrderPstateConfig(string Pstate)
        {
            string s = "";
            if (Pstate != "")
            {
                s += "(";
                for (int i = 0; i <= Convert.ToInt32(Pstate); i++)
                {
                    s += i + ",";
                }
                s = s.Substring(0, s.Length - 1);
                s += ")";
            }
            string str = "select StateDesc from  ProjectState_Config  where StateType ='Pstate' and StateId in" + s;
            DataTable dt = SQLBase.FillTable(str, "SalesDBCnn");
            if (dt == null) return null;
            return dt;
        }

        #endregion

    }
}
