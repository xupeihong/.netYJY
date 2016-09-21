using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class FlowMeterPro
    {
        // 主页-查询加载登记卡列表 
        public static UIDataTable LoadCardList(int a_intPageSize, int a_intPageIndex, string where, string order, string strType)
        {
            UIDataTable instData = new UIDataTable();
            DataSet DO_Order = new DataSet();
            if (strType != "CardType2")// 不是超声波
            {
                if (strType != "" && strType != null)
                    where += " and a.ModelType='" + strType + "' ";
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",order)
                };
                DO_Order = SQLBase.FillDataSet("GetCardList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            }
            else
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",order)
                };
                DO_Order = SQLBase.FillDataSet("GetCardListUT", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            }
            //
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

            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    if (dtOrder.Rows[i]["IsOut"] != null)
                    {
                        if (dtOrder.Rows[i]["IsOut"].ToString() == "0")
                            dtOrder.Rows[i]["IsOut"] = "是";
                        else if (dtOrder.Rows[i]["IsOut"].ToString() == "1")
                            dtOrder.Rows[i]["IsOut"] = "否";
                    }
                }
            }

            instData.DtData = dtOrder;
            return instData;

        }

        // 加载状态为收货确认之前的登记卡列表 
        public static UIDataTable LoadCardListAll(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            DataSet DO_Order = new DataSet();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@Order",order)
                };
            DO_Order = SQLBase.FillDataSet("GetCardListAll", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            //
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

            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    if (dtOrder.Rows[i]["IsOut"] != null)
                    {
                        if (dtOrder.Rows[i]["IsOut"].ToString() == "0")
                            dtOrder.Rows[i]["IsOut"] = "否";
                        else if (dtOrder.Rows[i]["IsOut"].ToString() == "1")
                            dtOrder.Rows[i]["IsOut"] = "是";
                    }
                }
            }

            instData.DtData = dtOrder;
            return instData;

        }

        // 新增-加载需要检查的仪表项目
        public static DataTable GetCheckItems(ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select CheckItem,CheckContent from tk_ConfigFace ";
                strSql += " where Validate='v' order by OrderID ASC ";
                DataTable dtItems = SQLBase.FillTable(strSql, "FlowMeterDBCnn");

                return dtItems;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                return null;
            }

        }

        // 新增-加载超声波需要检查的仪表项目
        public static DataTable GetCheckItemsUT(ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select CheckItem,CheckContent from tk_ConfigFaceUT ";
                strSql += " where Validate='v' order by OrderID ASC ";
                DataTable dtItems = SQLBase.FillTable(strSql, "FlowMeterDBCnn");

                return dtItems;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                return null;
            }

        }

        // 新增-获取新的维修标识码 RID 
        public static string GetNewRID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strRID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='DJ'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strRID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('DJ',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string str = "select RID, RidNo,DateRecord from tk_RIDno where DateRecord='" + strYMD + "' and RID='DJ'";
            dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            strRID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strRID;
        }

        // 新增-插入新的维修标识码 RID 
        public static string GetRID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strRID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='DJ' ";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strRID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('DJ',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='DJ' ";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strRID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strRID;
        }

        // 新增-确认新增登记卡
        public static int AddNewCard(RepairCard repairCard, string Title, string Checked, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int insertlog = 0;
            string[] arrTitle = Title.Split(',');
            string[] arrChecked = Checked.Split('?');

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<RepairCard>(repairCard, "tk_RepairCard");
            string strInsertCheck = "";
            for (int i = 0; i < arrTitle.Length; i++)
            {
                string strTitle = arrTitle[i].ToString();
                string strCheck = arrChecked[i].ToString();
                string strsql = "";
                if (strTitle != "" && strCheck != "")
                    strsql = " insert into tk_FaceCheck values('" + repairCard.strRID + "'," + i + ",'" + strTitle + "','" + strCheck + "','合格','v') ";
                strInsertCheck += strsql;
            }

            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = repairCard.strRID;
            log.strLogTitle = "新增维修登记卡";
            log.strLogContent = "新增登记卡成功,同时生成随工单和流转卡";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修登记卡";

            try
            {
                if (strInsert != "")
                {
                    strInsert += strInsertCheck;
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (intInsert > 0)
                {
                    // 生成随工单和流转卡 【关联ID即可】
                    string strWRID = GetWRID("WR");// 随工单 
                    string strTRID = GetWRID("TR");// 流转卡
                    string strInsertWR = " insert into tk_WorkCard(WID,RID,Validate) values('" + strWRID + "','" + repairCard.strRID + "','v') ";
                    strInsertWR += " insert into tk_TransCard(TID,RID,Validate) values('" + strTRID + "','" + repairCard.strRID + "','v')";
                    intInsert += sqlTrans.ExecuteNonQuery(strInsertWR, CommandType.Text, null);

                }
                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + insertlog;

        }

        // 新增-确认新增登记卡
        public static int AddNewCard2(RepairCardNew repairCard, string Title, string Checked, string TitleUT, string CheckedUT, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int insertlog = 0;
            string[] arrTitle;
            string[] arrChecked;
            string strInsert = "";
            string strInsertCheck = "";
            a_strErr = "";

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 超声波登记卡
            if ((repairCard.strCustomerName == "" || repairCard.strCustomerName == null) &&
                (repairCard.strCustomerAddr == "" || repairCard.strCustomerName == null))
            {
                arrTitle = TitleUT.Split(',');
                arrChecked = CheckedUT.Split('?');
                //
                for (int i = 0; i < arrTitle.Length; i++)
                {
                    string strTitle = arrTitle[i].ToString();
                    string strCheck = arrChecked[i].ToString();
                    string strsql = "";
                    if (strTitle != "" && strCheck != "")
                        strsql = " insert into tk_UTFaceCheck values('" + repairCard.strRID + "'," + i + ",'" + strTitle + "','" + strCheck + "','合格','v') ";
                    strInsertCheck += strsql;
                }
                //
                strInsert = " insert into tk_UTRepairCard(RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,";
                strInsert += " MeterID,CertifID,MeterName,Manufacturer,Model,ModelType,CirNum,CirVersion,FactoryDate,FlowRange,";
                strInsert += " Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,";
                strInsert += " TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,FirstDate,SecondCheck,";
                strInsert += " SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strInsert += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser) values(";
                strInsert += "'" + repairCard.strRID + "','" + repairCard.strRepairIDUT + "','" + repairCard.strCustomerNameUT + "','" + repairCard.strCustomerAddrUT
                    + "','" + repairCard.strS_NameUT + "','" + repairCard.strS_TelUT + "','" + repairCard.strS_DateUT + "','" + repairCard.strSubUnitUT
                    + "','" + repairCard.strMeterIDUT + "','" + repairCard.strCertifIDUT + "','" + repairCard.strMeterNameUT + "','" + repairCard.strManufacturerUT
                    + "','" + repairCard.strModelUT + "','" + repairCard.strModelTypeUT + "','" + repairCard.strCirNumUT + "','" + repairCard.strCirVersionUT
                    + "','" + repairCard.strFactoryDateUT + "','" + repairCard.strFlowRangeUT + "','" + repairCard.strPressureUT + "','" + repairCard.strCaliberUT
                    + "','" + repairCard.strTrackA1UT + "','" + repairCard.strTrackA2UT + "','" + repairCard.strTrackA3UT + "','" + repairCard.strTrackA4UT
                    + "','" + repairCard.strTrackA5UT + "','" + repairCard.strTrackA6UT + "','" + repairCard.strTrackB1UT + "','" + repairCard.strTrackB2UT
                    + "','" + repairCard.strTrackB3UT + "','" + repairCard.strTrackB4UT + "','" + repairCard.strTrackB5UT + "','" + repairCard.strTrackB6UT
                    + "','" + repairCard.strComments1UT + "','" + repairCard.strFaceOtherUT + "','" + repairCard.strRepairContentUT + "','" + repairCard.strCheckUserUT
                    + "','" + repairCard.strFirstCheckUT + "','" + repairCard.strFirstDateUT + "','" + repairCard.strSecondCheckUT + "','" + repairCard.strSecondDateUT
                    + "','" + repairCard.strThirdCheckUT + "','" + repairCard.strThirdDateUT + "','" + repairCard.strTextUT + "','" + repairCard.strGetTypeModelUT
                    + "','" + repairCard.strG_NameUT + "','" + repairCard.strG_TelUT + "','" + repairCard.strG_DateUT + "','0','" + repairCard.strFirstDateUT
                    + "','" + repairCard.strIsOutUT + "','" + repairCard.strOutUnitUT + "','" + repairCard.strTakeIDUT + "','" + repairCard.strDeliverIDUT
                    + "','" + repairCard.strRecieveIDUT + "','" + repairCard.strReceiveUserUT + "','" + repairCard.strLogisticUT + "','" + repairCard.strModelProperty
                    + "','v','" + DateTime.Now + "','" + repairCard.strCreateUserUT + "')";
            }
            else
            {
                arrTitle = Title.Split(',');
                arrChecked = Checked.Split('?');
                //
                for (int i = 0; i < arrTitle.Length; i++)
                {
                    string strTitle = arrTitle[i].ToString();
                    string strCheck = arrChecked[i].ToString();
                    string strsql = "";
                    if (strTitle != "" && strCheck != "")
                        strsql = " insert into tk_FaceCheck values('" + repairCard.strRID + "'," + i + ",'" + strTitle + "','" + strCheck + "','合格','v') ";
                    strInsertCheck += strsql;
                }
                //
                strInsert += " insert into tk_RepairCard(RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,";
                strInsert += " MeterID,CertifID,MeterName,Manufacturer,Precision,Model,ModelType,FactoryDate,RecordNum,FlowRange,Pressure,";
                strInsert += " Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,X_Operating,X_FactoryDate,";
                strInsert += " X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,";
                strInsert += " ConfirmUser,ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strInsert += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,";
                strInsert += " CreateTime,CreateUser) values(";
                strInsert += "'" + repairCard.strRID + "','" + repairCard.strRepairID + "','" + repairCard.strCustomerName + "','" + repairCard.strCustomerAddr
                    + "','" + repairCard.strS_Name + "','" + repairCard.strS_Tel + "','" + repairCard.strS_Date + "','" + repairCard.strSubUnit
                    + "','" + repairCard.strMeterID + "','" + repairCard.strCertifID + "','" + repairCard.strMeterName + "','" + repairCard.strManufacturer
                    + "','" + repairCard.strPrecision + "','" + repairCard.strModel + "','" + repairCard.strModelType + "','" + repairCard.strFactoryDate
                    + "','" + repairCard.strRecordNum + "','" + repairCard.strFlowRange + "','" + repairCard.strPressure + "','" + repairCard.strCaliber
                    + "','" + repairCard.strPreUnit + "','" + repairCard.strNewUnit + "','" + repairCard.strX_ID + "','" + repairCard.strX_CertifID
                    + "','" + repairCard.strX_Model + "','" + repairCard.strX_Manufacturer + "','" + repairCard.strX_Standard + "','" + repairCard.strX_Operating
                    + "','" + repairCard.strX_FactoryDate + "','" + repairCard.strX_Pressure + "','" + repairCard.strX_Temperature + "','" + repairCard.strX_Data
                    + "','" + repairCard.strX_PreUnit + "','" + repairCard.strX_NewUnit + "','" + repairCard.strFaceOther + "','" + repairCard.strRepairContent
                    + "','" + repairCard.strCheckUser + "','" + repairCard.strIsRepair + "','" + repairCard.strConfirmUser + "','" + repairCard.strConfirmDate
                    + "','" + repairCard.strText + "','" + repairCard.strGetTypeModel + "','" + repairCard.strG_Name + "','" + repairCard.strG_Tel
                    + "','" + repairCard.strG_Date + "','0','" + repairCard.strFinishDate + "','" + repairCard.strIsOut + "','" + repairCard.strOutUnit
                    + "','" + repairCard.strTakeID + "','" + repairCard.strDeliverID + "','" + repairCard.strRecieveID + "','" + repairCard.strReceiveUser
                    + "','" + repairCard.strLogistic + "','" + repairCard.strModelProperty + "','v','" + DateTime.Now + "','" + repairCard.strCreateUser + "')";
            }
            // 日志记录 
            OperateLog log = new OperateLog();
            log.strMarkID = repairCard.strRID;
            log.strLogTitle = "新增维修登记卡";
            log.strLogContent = "新增登记卡成功,同时生成随工单和流转卡";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修登记卡";

            try
            {
                if (strInsert != "")
                {
                    strInsert += strInsertCheck;
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (intInsert > 0)
                {
                    // 生成随工单和流转卡 【关联ID即可】
                    string strWRID = GetWRID("WR");// 随工单 
                    string strTRID = GetWRID("TR");// 流转卡
                    string strInsertWR = "";
                    if ((repairCard.strCustomerName == "" || repairCard.strCustomerName == null) &&
                        (repairCard.strCustomerAddr == "" || repairCard.strCustomerName == null))
                    {
                        strInsertWR = " insert into tk_WorkCardUT(WID,RID,Validate) values('" + strWRID + "','" + repairCard.strRID + "','v') ";
                    }
                    else
                    {
                        strInsertWR = " insert into tk_WorkCard(WID,RID,Validate) values('" + strWRID + "','" + repairCard.strRID + "','v') ";
                        strInsertWR += " insert into tk_TransCard(TID,RID,Validate) values('" + strTRID + "','" + repairCard.strRID + "','v')";
                    }
                    intInsert += sqlTrans.ExecuteNonQuery(strInsertWR, CommandType.Text, null);
                }
                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + insertlog;

        }

        // 151016 ly 获取随工单ID 
        public static string GetWRID(string Rtype)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strWRID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='" + Rtype + "'";

            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strWRID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('" + Rtype + "',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }
            intNewID++;

            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='" + Rtype + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strWRID = Rtype + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strWRID;

        }

        // 新增-根据仪表编号判断是否存在当前信息
        public static DataTable CheckMeterInfo(string MeterID, string strModelType)
        {
            string sql = "";
            if (strModelType == "CardType2")// 超声波
                sql += " select * from tk_UTRepairCard where MeterID='" + MeterID + "' and ModelType='" + strModelType + "' and Validate='v' ";
            else
                sql += "select * From tk_RepairCard where MeterID='" + MeterID + "' and ModelType='" + strModelType + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
            return dt;
        }

        // 新增-根据仪表类型获取仪表名称 一般是一对一的关系
        public static string GetMeterName(string ModelType, ref string strErr)
        {
            strErr = "";
            string strSql = "";
            // 超声波
            if (ModelType == "CardType2")
                strSql = " select MeterName from tk_UTRepairCard where validate='v' and ModelType='" + ModelType + "'";
            else
                strSql = " select MeterName from tk_RepairCard where Validate='v' and ModelType='" + ModelType + "'";

            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";

        }

        // 新增-加载流量范围下拉框
        public static string GetFlowRange(string strModel)
        {
            string strSel = "";
            if (strModel == "")
            {
                strSel = " select distinct isnull(FlowRange,'') as Text, isnull(FlowRange,'') as ID ";
                strSel += " from tk_RepairCard ORDER BY Text ASC ";
            }
            else
            {
                strSel = " select distinct isnull(FlowRange,'') as Text, isnull(FlowRange,'') as ID ";
                strSel += " from tk_RepairCard where Model='" + strModel + "' ORDER BY Text ASC ";
            }

            DataTable dt = SQLBase.FillTable(strSel, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count <= 0)
                return "";
            else
            {
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i][0].ToString() + "?";
                }
                str = str.Substring(0, str.Length - 1);
                return str;
            }
        }

        // 新增-加载承压等级下拉框
        public static string GetPressure(string strModel)
        {
            string strSel = "";
            if (strModel == "")
            {
                strSel = " select distinct isnull(Pressure,'') as Text, isnull(Pressure,'') as ID ";
                strSel += " from tk_RepairCard ORDER BY Text ASC ";
            }
            else
            {
                strSel = " select distinct isnull(Pressure,'') as Text, isnull(Pressure,'') as ID ";
                strSel += " from tk_RepairCard where Model='" + strModel + "' ORDER BY Text ASC ";
            }

            DataTable dt = SQLBase.FillTable(strSel, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count <= 0)
                return "";
            else
            {
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i][0].ToString() + "?";
                }
                str = str.Substring(0, str.Length - 1);
                return str;
            }
        }

        // 新增-超声波加载流量范围下拉框
        public static string GetFlowRangeUT(string strModel)
        {
            string strSel = "";
            if (strModel == "")
            {
                strSel = " select distinct isnull(FlowRange,'') as Text, isnull(FlowRange,'') as ID ";
                strSel += " from tk_UTRepairCard ORDER BY Text ASC ";
            }
            else
            {
                strSel = " select distinct isnull(FlowRange,'') as Text, isnull(FlowRange,'') as ID ";
                strSel += " from tk_UTRepairCard where Model='" + strModel + "' ORDER BY Text ASC ";
            }

            DataTable dt = SQLBase.FillTable(strSel, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count <= 0)
                return "";
            else
            {
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i][0].ToString() + "?";
                }
                str = str.Substring(0, str.Length - 1);
                return str;
            }
        }

        // 新增-超声波加载承压等级下拉框
        public static string GetPressureUT(string strModel)
        {
            string strSel = "";
            if (strModel == "")
            {
                strSel = " select distinct isnull(Pressure,'') as Text, isnull(Pressure,'') as ID ";
                strSel += " from tk_UTRepairCard ORDER BY Text ASC ";
            }
            else
            {
                strSel = " select distinct isnull(Pressure,'') as Text, isnull(Pressure,'') as ID ";
                strSel += " from tk_UTRepairCard where Model='" + strModel + "' ORDER BY Text ASC ";
            }

            DataTable dt = SQLBase.FillTable(strSel, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count <= 0)
                return "";
            else
            {
                string str = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i][0].ToString() + "?";
                }
                str = str.Substring(0, str.Length - 1);
                return str;
            }
        }





        // 修改-获取指定id对应的登记卡详细信息
        public static RepairCard getNewRepairCard(string strID)
        {
            RepairCard repairCard = new RepairCard();
            string strSql = "select * from tk_RepairCard where RID = '" + strID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                repairCard.strRID = strID;
                repairCard.strRepairID = dt.Rows[0]["RepairID"].ToString();
                repairCard.strCustomerName = dt.Rows[0]["CustomerName"].ToString();
                repairCard.strCustomerAddr = dt.Rows[0]["CustomerAddr"].ToString();
                repairCard.strS_Name = dt.Rows[0]["S_Name"].ToString();
                repairCard.strS_Tel = dt.Rows[0]["S_Tel"].ToString();
                repairCard.strS_Date = dt.Rows[0]["S_Date"].ToString();
                repairCard.strMeterID = dt.Rows[0]["MeterID"].ToString();
                repairCard.strCertifID = dt.Rows[0]["CertifID"].ToString();
                repairCard.strMeterName = dt.Rows[0]["MeterName"].ToString();
                repairCard.strManufacturer = dt.Rows[0]["Manufacturer"].ToString();
                repairCard.strPrecision = dt.Rows[0]["Precision"].ToString();
                repairCard.strModel = dt.Rows[0]["Model"].ToString();
                repairCard.strModelType = dt.Rows[0]["ModelType"].ToString();
                repairCard.strFactoryDate = dt.Rows[0]["FactoryDate"].ToString();
                if (repairCard.strRecordNum != null)
                    repairCard.strRecordNum = Convert.ToDecimal(dt.Rows[0]["RecordNum"].ToString());
                else
                    repairCard.strRecordNum = null;

                repairCard.strFlowRange = dt.Rows[0]["FlowRange"].ToString();
                repairCard.strPressure = dt.Rows[0]["Pressure"].ToString();
                repairCard.strCaliber = dt.Rows[0]["Caliber"].ToString();
                repairCard.strPreUnit = dt.Rows[0]["PreUnit"].ToString();
                repairCard.strNewUnit = dt.Rows[0]["NewUnit"].ToString();
                repairCard.strX_ID = dt.Rows[0]["X_ID"].ToString();
                repairCard.strX_CertifID = dt.Rows[0]["X_CertifID"].ToString();
                repairCard.strX_Model = dt.Rows[0]["X_Model"].ToString();
                repairCard.strX_Manufacturer = dt.Rows[0]["X_Manufacturer"].ToString();
                repairCard.strX_Standard = dt.Rows[0]["X_Standard"].ToString();
                repairCard.strX_Operating = dt.Rows[0]["X_Operating"].ToString();
                repairCard.strX_FactoryDate = dt.Rows[0]["X_FactoryDate"].ToString();
                repairCard.strX_Pressure = dt.Rows[0]["X_Pressure"].ToString();
                repairCard.strX_Temperature = dt.Rows[0]["X_Temperature"].ToString();
                repairCard.strX_Data = dt.Rows[0]["X_Data"].ToString();
                repairCard.strX_PreUnit = dt.Rows[0]["X_PreUnit"].ToString();
                repairCard.strX_NewUnit = dt.Rows[0]["X_NewUnit"].ToString();
                repairCard.strFaceOther = dt.Rows[0]["FaceOther"].ToString();
                repairCard.strRepairContent = dt.Rows[0]["RepairContent"].ToString();
                repairCard.strCheckUser = dt.Rows[0]["CheckUser"].ToString();
                repairCard.strIsRepair = dt.Rows[0]["IsRepair"].ToString();
                repairCard.strConfirmUser = dt.Rows[0]["ConfirmUser"].ToString();
                repairCard.strConfirmDate = dt.Rows[0]["ConfirmDate"].ToString();
                repairCard.strText = dt.Rows[0]["Text"].ToString();

                repairCard.strGetTypeModel = dt.Rows[0]["GetTypeModel"].ToString();
                repairCard.strG_Name = dt.Rows[0]["G_Name"].ToString();
                repairCard.strG_Tel = dt.Rows[0]["G_Tel"].ToString();
                repairCard.strG_Date = dt.Rows[0]["G_Date"].ToString();
                repairCard.strState = Convert.ToInt32(dt.Rows[0]["State"].ToString());
                repairCard.strFinishDate = dt.Rows[0]["FinishDate"].ToString();
                repairCard.strIsOut = dt.Rows[0]["IsOut"].ToString();
                repairCard.strOutUnit = dt.Rows[0]["OutUnit"].ToString();
                repairCard.strSubUnit = dt.Rows[0]["SubUnit"].ToString();
                repairCard.strTakeID = dt.Rows[0]["TakeID"].ToString();
                repairCard.strDeliverID = dt.Rows[0]["DeliverID"].ToString();
                repairCard.strReceiveUser = dt.Rows[0]["ReceiveUser"].ToString();
                repairCard.strRecieveID = dt.Rows[0]["RecieveID"].ToString();
                repairCard.strLogistic = dt.Rows[0]["Logistic"].ToString();
                repairCard.strModelProperty = dt.Rows[0]["ModelProperty"].ToString();

                repairCard.strvalidate = dt.Rows[0]["Validate"].ToString();
                repairCard.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                repairCard.strCreateUser = dt.Rows[0]["CreateUser"].ToString();

            }

            return repairCard;

        }

        // 修改-获取选中的选项内容
        public static DataTable GetCheckeds(string strRID)
        {
            string strSql = " Select CheckItem,CheckContent from tk_FaceCheck where RID='" + strRID + "' and validate='v' order by RID ";
            DataTable dtTabMg = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dtTabMg == null || dtTabMg.Rows.Count < 1)// 不存在数据
                return null;
            else
                return dtTabMg;

        }

        // 修改-确认修改登记卡 
        // [1.将原信息存入His表中 2.在现表中存入修改后的信息 3.将修改操作记录在log表中]
        public static int UpdateCard(RepairCard repairCard, string Title, string Checked, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_RepairCardHis ";
            strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
            strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
            strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,";
            strPerSql += " CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_RepairCard where RID='" + repairCard.strRID + "' and validate='v' ";

            string strPerSql2 = " insert into tk_FaceCheckHis ";
            strPerSql2 += " select RID,OrderID,CheckItem,CheckContent,IsQualified,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql2 += " from tk_FaceCheck where RID='" + repairCard.strRID + "' and validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = GSqlSentence.GetUpdateInfoByD<RepairCard>(repairCard, "RID", "tk_RepairCard");
            string strDelCheck = " delete from tk_FaceCheck where RID='" + repairCard.strRID + "' and validate='v' ";
            string strInsertCheck = "";
            string[] arrTitle = Title.Split(',');
            string[] arrChecked = Checked.Split('?');

            for (int i = 0; i < arrTitle.Length; i++)
            {
                string strTitle = arrTitle[i].ToString();
                string strCheck = arrChecked[i].ToString();
                string strsql = "";
                if (strTitle != "" && strCheck != "")
                    strsql = " insert into tk_FaceCheck values('" + repairCard.strRID + "'," + i + ",'" + strTitle + "','" + strCheck + "','合格','v') ";
                strInsertCheck += strsql;
            }

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = repairCard.strRID;
            log.strLogTitle = "修改登记卡";
            log.strLogContent = "修改登记卡成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修登记卡";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql + strPerSql2, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    if (intUpdate > 0)
                        sqlTrans.ExecuteNonQuery(strDelCheck + strInsertCheck, CommandType.Text, null);
                }

                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }

        // 修改-获取指定id对应的超声波登记卡详细信息
        public static RepairCardUT getNewRepairCardUT(string strIDUT)
        {
            RepairCardUT repairCard = new RepairCardUT();
            string strSql = "select * from tk_UTRepairCard where RID = '" + strIDUT + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                repairCard.strRIDUT = strIDUT;
                repairCard.strRepairIDUT = dt.Rows[0]["RepairID"].ToString();
                repairCard.strCustomerNameUT = dt.Rows[0]["CustomerName"].ToString();
                repairCard.strCustomerAddrUT = dt.Rows[0]["CustomerAddr"].ToString();
                repairCard.strS_NameUT = dt.Rows[0]["S_Name"].ToString();
                repairCard.strS_TelUT = dt.Rows[0]["S_Tel"].ToString();
                repairCard.strS_DateUT = dt.Rows[0]["S_Date"].ToString();
                repairCard.strMeterIDUT = dt.Rows[0]["MeterID"].ToString();
                repairCard.strCertifIDUT = dt.Rows[0]["CertifID"].ToString();
                repairCard.strMeterNameUT = dt.Rows[0]["MeterName"].ToString();
                repairCard.strManufacturerUT = dt.Rows[0]["Manufacturer"].ToString();
                repairCard.strModelTypeUT = dt.Rows[0]["ModelType"].ToString();
                repairCard.strModelUT = dt.Rows[0]["Model"].ToString();
                repairCard.strCirNumUT = dt.Rows[0]["CirNum"].ToString();
                repairCard.strCirVersionUT = dt.Rows[0]["CirVersion"].ToString();
                repairCard.strFactoryDateUT = dt.Rows[0]["FactoryDate"].ToString();
                repairCard.strFlowRangeUT = dt.Rows[0]["FlowRange"].ToString();
                repairCard.strPressureUT = dt.Rows[0]["Pressure"].ToString();
                repairCard.strCaliberUT = dt.Rows[0]["Caliber"].ToString();

                repairCard.strTrackA1UT = dt.Rows[0]["TrackA1"].ToString();
                repairCard.strTrackA2UT = dt.Rows[0]["TrackA2"].ToString();
                repairCard.strTrackA3UT = dt.Rows[0]["TrackA3"].ToString();
                repairCard.strTrackA4UT = dt.Rows[0]["TrackA4"].ToString();
                repairCard.strTrackA5UT = dt.Rows[0]["TrackA5"].ToString();
                repairCard.strTrackA6UT = dt.Rows[0]["TrackA6"].ToString();
                repairCard.strTrackB1UT = dt.Rows[0]["TrackB1"].ToString();
                repairCard.strTrackB2UT = dt.Rows[0]["TrackB2"].ToString();
                repairCard.strTrackB3UT = dt.Rows[0]["TrackB3"].ToString();
                repairCard.strTrackB4UT = dt.Rows[0]["TrackB4"].ToString();
                repairCard.strTrackB5UT = dt.Rows[0]["TrackB5"].ToString();
                repairCard.strTrackB6UT = dt.Rows[0]["TrackB6"].ToString();

                repairCard.strComments1UT = dt.Rows[0]["Comments1"].ToString();
                repairCard.strFaceOtherUT = dt.Rows[0]["FaceOther"].ToString();
                repairCard.strRepairContentUT = dt.Rows[0]["RepairContent"].ToString();
                repairCard.strCheckUserUT = dt.Rows[0]["CheckUser"].ToString();
                repairCard.strFirstCheckUT = dt.Rows[0]["FirstCheck"].ToString();
                repairCard.strFirstDateUT = dt.Rows[0]["FirstDate"].ToString();
                repairCard.strSecondCheckUT = dt.Rows[0]["SecondCheck"].ToString();
                repairCard.strSecondDateUT = dt.Rows[0]["SecondDate"].ToString();
                repairCard.strThirdCheckUT = dt.Rows[0]["ThirdCheck"].ToString();
                repairCard.strThirdDateUT = dt.Rows[0]["ThirdDate"].ToString();

                repairCard.strTextUT = dt.Rows[0]["Text"].ToString();
                repairCard.strGetTypeModelUT = dt.Rows[0]["GetTypeModel"].ToString();
                repairCard.strG_NameUT = dt.Rows[0]["G_Name"].ToString();
                repairCard.strG_TelUT = dt.Rows[0]["G_Tel"].ToString();
                repairCard.strG_DateUT = dt.Rows[0]["G_Date"].ToString();
                repairCard.strStateUT = Convert.ToInt32(dt.Rows[0]["State"].ToString());
                repairCard.strFinishDateUT = dt.Rows[0]["FinishDate"].ToString();
                repairCard.strIsOutUT = dt.Rows[0]["IsOut"].ToString();
                repairCard.strOutUnitUT = dt.Rows[0]["OutUnit"].ToString();
                repairCard.strSubUnitUT = dt.Rows[0]["SubUnit"].ToString();

                repairCard.strTakeIDUT = dt.Rows[0]["TakeID"].ToString();
                repairCard.strDeliverIDUT = dt.Rows[0]["DeliverID"].ToString();
                repairCard.strReceiveUserUT = dt.Rows[0]["ReceiveUser"].ToString();
                repairCard.strRecieveIDUT = dt.Rows[0]["RecieveID"].ToString();
                repairCard.strLogisticUT = dt.Rows[0]["Logistic"].ToString();
                repairCard.strModelPropertyUT = dt.Rows[0]["ModelProperty"].ToString();

                repairCard.strvalidateUT = dt.Rows[0]["Validate"].ToString();
                repairCard.strCreateTimeUT = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                repairCard.strCreateUserUT = dt.Rows[0]["CreateUser"].ToString();

            }

            return repairCard;

        }

        // 修改-获取超声波选中的选项内容 
        public static DataTable GetCheckedsUT(string strRID)
        {
            string strSql = " Select CheckItem,CheckContent from tk_UTFaceCheck where RID='" + strRID + "' and validate='v' order by RID ";
            DataTable dtTabMg = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dtTabMg == null || dtTabMg.Rows.Count < 1)// 不存在数据
                return null;
            else
                return dtTabMg;

        }

        // 修改-确认修改超声波登记卡
        public static int UpdateCardUT(RepairCardUT repairCard, string Title, string Checked, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_UTRepairCardHis ";
            strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
            strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
            strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
            strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_UTRepairCard where RID='" + repairCard.strRIDUT + "' and validate='v' ";

            string strPerSql2 = " insert into tk_UTFaceCheckHis ";
            strPerSql2 += " select RID,OrderID,CheckItem,CheckContent,IsQualified,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql2 += " from tk_UTFaceCheck where RID='" + repairCard.strRIDUT + "' and validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = "";
            strUpdate += " update tk_UTRepairCard set RID='" + repairCard.strRIDUT + "',RepairID='" + repairCard.strRepairIDUT + "',CustomerName='" + repairCard.strCustomerNameUT
                + "',CustomerAddr='" + repairCard.strCustomerAddrUT + "',S_Name='" + repairCard.strS_NameUT + "',S_Tel='" + repairCard.strS_TelUT + "',S_Date='" + repairCard.strS_DateUT
                + "',SubUnit='" + repairCard.strSubUnitUT + "',MeterID='" + repairCard.strMeterIDUT + "',CertifID='" + repairCard.strCertifIDUT + "',MeterName='" + repairCard.strMeterNameUT
                + "',Manufacturer='" + repairCard.strManufacturerUT + "',ModelType='" + repairCard.strModelTypeUT + "',Model='" + repairCard.strModelUT + "',CirNum='" + repairCard.strCirNumUT
                + "',CirVersion='" + repairCard.strCirVersionUT + "',FactoryDate='" + repairCard.strFactoryDateUT + "',FlowRange='" + repairCard.strFlowRangeUT + "',Pressure='" + repairCard.strPressureUT
                + "',Caliber='" + repairCard.strCaliberUT + "',TrackA1='" + repairCard.strTrackA1UT + "',TrackA2='" + repairCard.strTrackA2UT + "',TrackA3='" + repairCard.strTrackA3UT
                + "',TrackA4='" + repairCard.strTrackA4UT + "',TrackA5='" + repairCard.strTrackA5UT + "',TrackA6='" + repairCard.strTrackA6UT + "',TrackB1='" + repairCard.strTrackB1UT
                + "',TrackB2='" + repairCard.strTrackB2UT + "',TrackB3='" + repairCard.strTrackB3UT + "',TrackB4='" + repairCard.strTrackB4UT + "',TrackB5='" + repairCard.strTrackB5UT
                + "',TrackB6='" + repairCard.strTrackB6UT + "',Comments1='" + repairCard.strComments1UT + "',FaceOther='" + repairCard.strFaceOtherUT + "',RepairContent='" + repairCard.strRepairContentUT
                + "',CheckUser='" + repairCard.strCheckUserUT + "',FirstCheck='" + repairCard.strFirstCheckUT + "',FirstDate='" + repairCard.strFirstDateUT + "',SecondCheck='" + repairCard.strSecondCheckUT
                + "',SecondDate='" + repairCard.strSecondDateUT + "',ThirdCheck='" + repairCard.strThirdCheckUT + "',ThirdDate='" + repairCard.strThirdDateUT + "',Text='" + repairCard.strTextUT
                + "',GetTypeModel='" + repairCard.strGetTypeModelUT + "',G_Name='" + repairCard.strG_NameUT + "',G_Tel='" + repairCard.strG_TelUT + "',G_Date='" + repairCard.strG_DateUT + "',State='" + repairCard.strStateUT
                + "',FinishDate='" + repairCard.strFirstDateUT + "',IsOut='" + repairCard.strIsOutUT + "',OutUnit='" + repairCard.strOutUnitUT + "',TakeID='" + repairCard.strTakeIDUT + "',DeliverID='" + repairCard.strDeliverIDUT
                + "',RecieveID='" + repairCard.strRecieveIDUT + "',ReceiveUser='" + repairCard.strReceiveUserUT + "',Logistic='" + repairCard.strLogisticUT + "',ModelProperty='" + repairCard.strModelPropertyUT
                + "', Validate='" + repairCard.strvalidateUT + "',CreateTime='" + repairCard.strCreateTimeUT + "',CreateUser='" + repairCard.strCreateUserUT + "' ";
            strUpdate += " where RID='" + repairCard.strRIDUT + "' and Validate='v' ";
            //
            string strDelCheck = " delete from tk_UTFaceCheck where RID='" + repairCard.strRIDUT + "' and validate='v' ";
            string strInsertCheck = "";
            string[] arrTitle = Title.Split(',');
            string[] arrChecked = Checked.Split('?');

            for (int i = 0; i < arrTitle.Length; i++)
            {
                string strTitle = arrTitle[i].ToString();
                string strCheck = arrChecked[i].ToString();
                string strsql = "";
                if (strTitle != "" && strCheck != "")
                    strsql = " insert into tk_UTFaceCheck values('" + repairCard.strRIDUT + "'," + i + ",'" + strTitle + "','" + strCheck + "','合格','v') ";
                strInsertCheck += strsql;
            }

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = repairCard.strRIDUT;
            log.strLogTitle = "修改超声波登记卡";
            log.strLogContent = "修改超声波登记卡成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "超声波维修登记卡";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql + strPerSql2, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    if (intUpdate > 0)
                        sqlTrans.ExecuteNonQuery(strDelCheck + strInsertCheck, CommandType.Text, null);
                }

                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }




        // 查看-获取操作日志记录列表 
        public static DataTable GetoperateLog(string strRID, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select ID,LogTitle,LogContent,LogTime,LogPerson from tk_OperateLog ";
                strSql += " where 1=1 and MarkID='" + strRID + "' ";
                strSql += " order by ID asc ";

                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["ID"] = i + 1;
                    }

                    return dt;

                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }

        // 打印登记卡-获取型号
        public static string getModels(string strRID)
        {
            string strModels = " select b.Text as Model,c.Text as X_Model,d.Text as ModelProperty from tk_RepairCard a ";
            strModels += " left join (select * from tk_ConfigContent where Type='YBModel' and validate='v') b on a.Model=b.SID ";
            strModels += " left join (select * from tk_ConfigContent where Type='XZYModel' and validate='v') c on a.X_Model=c.SID ";
            strModels += " left join (select * from tk_ConfigContent where Type='ModelProperty' and validate='v') d on a.ModelProperty=d.SID ";
            strModels += " where a.Validate='v' and a.RID='" + strRID + "'";

            DataTable dt = SQLBase.FillTable(strModels, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
                return dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString();

        }

        // 获取型号
        public static string getModelsUT(string strRID)
        {
            string strModels = " select b.Text as Model,c.Text as ModelProperty from tk_UTRepairCard a ";
            strModels += " left join (select * from tk_ConfigContent where Type='YBModel' and validate='v') b on a.Model=b.SID ";
            strModels += " left join (select * from tk_ConfigContent where Type='ModelProperty' and validate='v') c on a.ModelProperty=c.SID ";
            strModels += " where a.Validate='v' and a.RID='" + strRID + "'";

            DataTable dt = SQLBase.FillTable(strModels, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
                return dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString();
        }

        // 打印随工单-获取详细信息
        public static tk_WorkCard getNewWorkCard(string strID)
        {

            tk_WorkCard workCard = new tk_WorkCard();
            string strSql = "select * from tk_WorkCard where RID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                workCard.strRID = strID;
                workCard.strWID = dt.Rows[0]["WID"].ToString();
                workCard.strRepairUser = dt.Rows[0]["RepairUser"].ToString();
                workCard.strCheckUser = dt.Rows[0]["CheckUser"].ToString();
                workCard.strRepairDate = dt.Rows[0]["RepairDate"].ToString();
                workCard.strI_Qmin = dt.Rows[0]["I_Qmin"].ToString();
                workCard.strI_2Qmax = dt.Rows[0]["I_2Qmax"].ToString();
                workCard.strI_4Qmax = dt.Rows[0]["I_4Qmax"].ToString();
                workCard.strI_Qmax = dt.Rows[0]["I_Qmax"].ToString();
                workCard.strI_Repeat1 = dt.Rows[0]["I_Repeat1"].ToString();
                workCard.strI_Repeat2 = dt.Rows[0]["I_Repeat2"].ToString();
                workCard.strI_Repeat3 = dt.Rows[0]["I_Repeat3"].ToString();
                workCard.strI_Repeat4 = dt.Rows[0]["I_Repeat4"].ToString();
                workCard.strDescripComments = dt.Rows[0]["DescripComments"].ToString();

                workCard.strIsRepairY = dt.Rows[0]["IsRepairY"].ToString();
                workCard.strYChangeBak = dt.Rows[0]["YChangeBak"].ToString();
                workCard.strYUnChangeBak = dt.Rows[0]["YUnChangeBak"].ToString();
                workCard.strTotalCheck = dt.Rows[0]["TotalCheck"].ToString();
                workCard.strRepairGroup = dt.Rows[0]["RepairGroup"].ToString();
                workCard.strIsRepairN = dt.Rows[0]["IsRepairN"].ToString();
                workCard.strNChangeBak = dt.Rows[0]["NChangeBak"].ToString();
                workCard.strNUnChangeBak = dt.Rows[0]["NUnChangeBak"].ToString();
                workCard.strTotalCheck2 = dt.Rows[0]["TotalCheck2"].ToString();
                workCard.strRepairGroup2 = dt.Rows[0]["RepairGroup2"].ToString();

                workCard.strO_Qmin = dt.Rows[0]["O_Qmin"].ToString();
                workCard.strO_2Qmax = dt.Rows[0]["O_2Qmax"].ToString();
                workCard.strO_4Qmax = dt.Rows[0]["O_4Qmax"].ToString();
                workCard.strO_Qmax = dt.Rows[0]["O_Qmax"].ToString();
                workCard.strO_Repeat1 = dt.Rows[0]["O_Repeat1"].ToString();
                workCard.strO_Repeat2 = dt.Rows[0]["O_Repeat2"].ToString();
                workCard.strO_Repeat3 = dt.Rows[0]["O_Repeat3"].ToString();
                workCard.strO_Repeat4 = dt.Rows[0]["O_Repeat4"].ToString();
                workCard.strRepairPerson = dt.Rows[0]["RepairPerson"].ToString();
                workCard.strCheckUser = dt.Rows[0]["CheckUser"].ToString();
                workCard.strStockOut = dt.Rows[0]["StockOut"].ToString();
                workCard.strOutCheck = dt.Rows[0]["OutCheck"].ToString();
                workCard.strPressHege = dt.Rows[0]["PressHege"].ToString();
                workCard.strMainCheck = dt.Rows[0]["MainCheck"].ToString();
                workCard.strO_Date = dt.Rows[0]["O_Date"].ToString();

            }

            return workCard;

        }

        // 打印超声波随工单-获取详细信息
        public static tk_WorkCardUT getNewWorkCardUT(string strID)
        {

            tk_WorkCardUT workCard = new tk_WorkCardUT();
            string strSql = "select * from tk_WorkCardUT where RID = '" + strID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                workCard.strRID = strID;
                workCard.strWID = dt.Rows[0]["WID"].ToString();
                workCard.strRepairUser = dt.Rows[0]["RepairUser"].ToString();
                workCard.strIsCheck = dt.Rows[0]["IsCheck"].ToString();
                workCard.strCheckResult = dt.Rows[0]["CheckResult"].ToString();
                workCard.strCirNum = dt.Rows[0]["CirNum"].ToString();
                workCard.strNewCirNum = dt.Rows[0]["NewCirNum"].ToString();
                workCard.strChangePlace = dt.Rows[0]["ChangePlace"].ToString();
                workCard.strCirVersion = dt.Rows[0]["CirVersion"].ToString();
                workCard.strNewCirVersion = dt.Rows[0]["NewCirVersion"].ToString();
                workCard.strPreVersion = dt.Rows[0]["PreVersion"].ToString();
                workCard.strNewVersion = dt.Rows[0]["NewVersion"].ToString();
                workCard.strPreData = dt.Rows[0]["PreData"].ToString();
                workCard.strNewData = dt.Rows[0]["NewData"].ToString();
                workCard.strProbePlace1 = dt.Rows[0]["ProbePlace1"].ToString();

                workCard.strProbeID1 = dt.Rows[0]["ProbeID1"].ToString();
                workCard.strNewProbePlace1 = dt.Rows[0]["NewProbePlace1"].ToString();
                workCard.strNewProbeID1 = dt.Rows[0]["NewProbeID1"].ToString();
                workCard.strCheck1 = dt.Rows[0]["Check1"].ToString();
                workCard.strProbePlace2 = dt.Rows[0]["ProbePlace2"].ToString();
                workCard.strProbeID2 = dt.Rows[0]["ProbeID2"].ToString();
                workCard.strNewProbePlace2 = dt.Rows[0]["NewProbePlace2"].ToString();
                workCard.strNewProbeID2 = dt.Rows[0]["NewProbeID2"].ToString();
                workCard.strCheck2 = dt.Rows[0]["Check2"].ToString();
                workCard.strProbePlace3 = dt.Rows[0]["ProbePlace3"].ToString();
                workCard.strProbeID3 = dt.Rows[0]["ProbeID3"].ToString();
                workCard.strNewProbePlace3 = dt.Rows[0]["NewProbePlace3"].ToString();
                workCard.strNewProbeID3 = dt.Rows[0]["NewProbeID3"].ToString();
                workCard.strCheck3 = dt.Rows[0]["Check3"].ToString();
                workCard.strProbePlace4 = dt.Rows[0]["ProbePlace4"].ToString();
                workCard.strProbeID4 = dt.Rows[0]["ProbeID4"].ToString();
                workCard.strNewProbePlace4 = dt.Rows[0]["NewProbePlace4"].ToString();
                workCard.strNewProbeID4 = dt.Rows[0]["NewProbeID4"].ToString();
                workCard.strCheck4 = dt.Rows[0]["Check4"].ToString();
                workCard.strProbePlace5 = dt.Rows[0]["ProbePlace5"].ToString();
                workCard.strProbeID5 = dt.Rows[0]["ProbeID5"].ToString();
                workCard.strNewProbePlace5 = dt.Rows[0]["NewProbePlace5"].ToString();
                workCard.strNewProbeID5 = dt.Rows[0]["NewProbeID5"].ToString();
                workCard.strCheck5 = dt.Rows[0]["Check5"].ToString();
                workCard.strProbePlace6 = dt.Rows[0]["ProbePlace6"].ToString();
                workCard.strProbeID6 = dt.Rows[0]["ProbeID6"].ToString();
                workCard.strNewProbePlace6 = dt.Rows[0]["NewProbePlace6"].ToString();
                workCard.strNewProbeID6 = dt.Rows[0]["NewProbeID6"].ToString();
                workCard.strCheck6 = dt.Rows[0]["Check6"].ToString();
                workCard.strProbePlace7 = dt.Rows[0]["ProbePlace7"].ToString();
                workCard.strProbeID7 = dt.Rows[0]["ProbeID7"].ToString();
                workCard.strNewProbePlace7 = dt.Rows[0]["NewProbePlace7"].ToString();
                workCard.strNewProbeID7 = dt.Rows[0]["NewProbeID7"].ToString();
                workCard.strCheck7 = dt.Rows[0]["Check7"].ToString();
                workCard.strProbePlace8 = dt.Rows[0]["ProbePlace8"].ToString();
                workCard.strProbeID8 = dt.Rows[0]["ProbeID8"].ToString();
                workCard.strNewProbePlace8 = dt.Rows[0]["NewProbePlace8"].ToString();
                workCard.strNewProbeID8 = dt.Rows[0]["NewProbeID8"].ToString();
                workCard.strCheck8 = dt.Rows[0]["Check8"].ToString();

                workCard.strProbeOut = dt.Rows[0]["ProbeOut"].ToString();
                workCard.strRepairContent1 = dt.Rows[0]["RepairContent1"].ToString();
                workCard.strProbeUnder = dt.Rows[0]["ProbeUnder"].ToString();
                workCard.strRepairContent2 = dt.Rows[0]["RepairContent2"].ToString();
                workCard.strProbeUnit = dt.Rows[0]["ProbeUnit"].ToString();
                workCard.strRepairContent3 = dt.Rows[0]["RepairContent3"].ToString();
                workCard.strSuppress = dt.Rows[0]["Suppress"].ToString();
                workCard.strSpecialTool = dt.Rows[0]["SpecialTool"].ToString();
                workCard.strCoordinate = dt.Rows[0]["Coordinate"].ToString();
                workCard.strAcceptUser = dt.Rows[0]["AcceptUser"].ToString();
                workCard.strAcceptDate = dt.Rows[0]["AcceptDate"].ToString();
                workCard.strValidate = dt.Rows[0]["Validate"].ToString();

            }

            return workCard;

        }

        // 维修编号 流量计编号 型号 
        public static string getMeterInfo(string strRID)
        {
            string strSql = " select a.RepairID,a.MeterID,b.Text as Model from tk_UTRepairCard a ";
            strSql += " left join (select * from tk_ConfigContent where validate='v' and Type='YBModel') b ";
            strSql += " on a.Model=b.SID ";
            strSql += " where a.Validate='v' and a.RID='" + strRID + "' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
            {
                string str = dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString();
                return str;
            }
        }

        // 打印随工单-获取维修编号
        public static string getRepairID(string strRID)
        {
            string strModels = " select RepairID from tk_RepairCard where RID='" + strRID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strModels, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
                return dt.Rows[0][0].ToString();

        }

        // 打印随工单-加载需要检查的仪表项目
        public static DataTable GetOutCheck(ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select CheckItem,CheckContent from tk_ConfigOutCheck ";
                strSql += " where Validate='v' order by OrderID ASC ";
                DataTable dtItems = SQLBase.FillTable(strSql, "FlowMeterDBCnn");

                return dtItems;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                return null;
            }

        }

        // 打印随工单-加载选中的检测项目
        public static DataTable GetOutCheckeds(string strRID)
        {
            string strSql = " Select CheckItem,CheckContent from tk_OutCheck where RID='" + strRID + "' and validate='v' order by RID ";
            DataTable dtTabMg = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dtTabMg == null || dtTabMg.Rows.Count < 1)// 不存在数据
                return null;
            else
                return dtTabMg;

        }

        // 打印随工单-加载更换部件列表 
        public static DataTable GetChangeBakList(string strRID, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select BakName,BakType,BakNum,Comments from tk_ChangeBak ";
                strSql += " where 1=1 and RID='" + strRID + "' and Validate='v' ";
                strSql += " order by BakName asc ";

                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                    return dt;
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }

        // 打印流转单-获取详细信息
        public static tk_TransCard getNewTransCard(string strID)
        {
            tk_TransCard transCard = new tk_TransCard();
            string strSql = "select * from tk_TransCard where RID = '" + strID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                transCard.strRID = strID;
                transCard.strTID = dt.Rows[0]["TID"].ToString();
                transCard.strFirstCheck = dt.Rows[0]["FirstCheck"].ToString();
                transCard.strSendRepair = dt.Rows[0]["SendRepair"].ToString();
                transCard.strLastCheck = dt.Rows[0]["LastCheck"].ToString();
                transCard.strOneRepair = dt.Rows[0]["OneRepair"].ToString();
                transCard.strTwoCheck = dt.Rows[0]["TwoCheck"].ToString();
                transCard.strTwoRepair = dt.Rows[0]["TwoRepair"].ToString();
                transCard.strThreeCheck = dt.Rows[0]["ThreeCheck"].ToString();
                transCard.strThreeRepair = dt.Rows[0]["ThreeRepair"].ToString();
                transCard.strComments = dt.Rows[0]["Comments"].ToString();
            }
            else
            {
                transCard.strRID = strID;
                transCard.strTID = "";
                transCard.strFirstCheck = "";
                transCard.strSendRepair = "";
                transCard.strLastCheck = "";
                transCard.strOneRepair = "";
                transCard.strTwoCheck = "";
                transCard.strTwoRepair = "";
                transCard.strThreeCheck = "";
                transCard.strThreeRepair = "";
                transCard.strComments = "";
            }
            return transCard;
        }




        // 随工单管理-查询加载登记卡列表 
        public static UIDataTable LoadWorkCardList(int a_intPageSize, int a_intPageIndex, string where, string strType)
        {
            UIDataTable instData = new UIDataTable();
            DataSet DO_Order = new DataSet();
            if (strType != "CardType2")// 不是超声波
            {
                if (strType != "" && strType != null)
                    where += " and b.ModelType='" + strType + "' ";
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
                DO_Order = SQLBase.FillDataSet("GetWorkCardList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            }
            else
            {
                SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
                DO_Order = SQLBase.FillDataSet("GetWorkCardListUT", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            }
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

        // 随工单管理-查询超声波随工单列表
        public static UIDataTable LoadWorkCardListUT(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetWorkCardListUT", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 随工单管理-确认修改随工单 
        // [1.将原信息存入His表中 2.在现表中存入修改后的信息 3.将修改操作记录在log表中]
        public static int UpdateWorkCard(tk_WorkCard workCard, string Title, string Checked, string RID, string BakName,
            string BakType, string BakNum, string Comments, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intUpdate = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string[] arrRID = RID.Split(',');
            string[] arrBakName = BakName.Split(',');
            string[] arrBakType = BakType.Split(',');
            string[] arrBakNum = BakNum.Split(',');
            string[] arrComments = Comments.Split('@');

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_WorkCardHis ";
            strPerSql += " select WID,RID,RepairUser,CheckUser,RepairDate,I_Qmin,I_2Qmax,I_4Qmax,I_Qmax,I_Repeat1,I_Repeat2,I_Repeat3,I_Repeat4,DescripComments,";
            strPerSql += " IsRepairY,YChangeBak,YUnChangeBak,TotalCheck,RepairGroup,IsRepairN,NChangeBak,NUnChangeBak,TotalCheck2,RepairGroup2,O_Qmin,O_2Qmax,O_4Qmax,";
            strPerSql += " O_Qmax,O_Repeat1,O_Repeat2,O_Repeat3,O_Repeat4,RepairPerson,StockOut,OutCheck,PressHege,MainCheck,O_Date,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_WorkCard where RID='" + workCard.strRID + "' and validate='v' ";

            // 复选框选项 
            string strPerSql2 = " insert into tk_OutCheckHis ";
            strPerSql2 += " select RID,OrderID,CheckItem,CheckContent,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql2 += " from tk_OutCheck where RID='" + workCard.strRID + "' and validate='v' ";

            // 更换部件信息
            string strPerSql3 = "";
            if (BakName == "" && BakType == "" && BakNum == "")
                strPerSql3 = "";
            else
            {
                strPerSql3 = " insert into tk_ChangeBakHis ";
                strPerSql3 += " select RID,BakName,BakType,BakNum,Comments,CreateTime,CreateUser,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql3 += " from tk_ChangeBak where RID='" + workCard.strRID + "' and validate='v' ";
            }

            // 2.在现表中存入修改后的信息 
            string strUpdate = GSqlSentence.GetUpdateInfoByD<tk_WorkCard>(workCard, "RID", "tk_WorkCard");
            string strDelCheck = " delete from tk_OutCheck where RID='" + workCard.strRID + "' and validate='v' ";
            string strDelChanBak = " delete from tk_ChangeBak where RID='" + workCard.strRID + "' and validate='v' ";
            string strInsertCheck = "";
            string strInsertChanBak = "";
            string[] arrTitle = Title.Split(',');
            string[] arrChecked = Checked.Split('?');

            for (int i = 0; i < arrTitle.Length; i++)
            {
                string strTitle = arrTitle[i].ToString();
                string strCheck = arrChecked[i].ToString();
                string strsql = "";
                if (strTitle != "" && strCheck != "")
                    strsql = " insert into tk_OutCheck values('" + workCard.strRID + "'," + i + ",'" + strTitle + "','" + strCheck + "','v') ";
                strInsertCheck += strsql;
            }

            for (int i = 0; i < arrRID.Length; i++)
            {
                strInsertChanBak += "insert into tk_ChangeBak values ('" + arrRID[i] + "','" + arrBakName[i] + "','" + arrBakType[i] + "','" +
                    arrBakNum[i] + "','" + arrComments[i] + "','" + DateTime.Now + "','" + acc.UserName + "','v')";
            }

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = workCard.strRID;
            log.strLogTitle = "修改维修随工单";
            log.strLogContent = "修改维修随工单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修随工单";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql + strPerSql2 + strPerSql3, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    if (intUpdate > 0)
                        sqlTrans.ExecuteNonQuery(strDelCheck + strInsertCheck + strDelChanBak + strInsertChanBak, CommandType.Text, null);
                }
                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;
        }

        // 超声波随工单管理-确认修改随工单 
        public static int UpdateWorkCardUT(tk_WorkCardUT workCard, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intUpdate = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_WorkCardUTHis ";
            strPerSql += " select WID,RID,RepairUser,IsCheck,CheckResult,CirNum,NewCirNum,ChangePlace,CirVersion,NewCirVersion,PreVersion,NewVersion,PreData,";
            strPerSql += " NewData,ProbePlace1,ProbeID1,NewProbePlace1,NewProbeID1,Check1,ProbePlace2,ProbeID2,NewProbePlace2,NewProbeID2,Check2,ProbePlace3,ProbeID3,";
            strPerSql += " NewProbePlace3,NewProbeID3,Check3,ProbePlace4,ProbeID4,NewProbePlace4,NewProbeID4,Check4,ProbePlace5,ProbeID5,NewProbePlace5,NewProbeID5,";
            strPerSql += " Check5,ProbePlace6,ProbeID6,NewProbePlace6,NewProbeID6,Check6,ProbePlace7,ProbeID7,NewProbePlace7,NewProbeID7,Check7,ProbePlace8,ProbeID8,";
            strPerSql += " NewProbePlace8,NewProbeID8,Check8,ProbeOut,RepairContent1,ProbeUnder,RepairContent2,ProbeUnit,RepairContent3,Suppress,SpecialTool,Coordinate,";
            strPerSql += " AcceptUser,AcceptDate,Validate,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_WorkCardUT where RID='" + workCard.strRID + "' and validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = GSqlSentence.GetUpdateInfoByD<tk_WorkCardUT>(workCard, "RID", "tk_WorkCardUT");

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = workCard.strRID;
            log.strLogTitle = "修改超声波维修随工单";
            log.strLogContent = "修改超声波维修随工单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "超声波维修随工单";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;
        }





        // 流转卡管理-查询加载流转卡列表 
        public static UIDataTable LoadTransCardList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetTransCardList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 流转卡管理-查询加载超声波流转卡列表 
        public static UIDataTable LoadTransCardListUT(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetTransCardListUT", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 流转卡管理-确认修改流转卡
        // [1.将原信息存入His表中 2.在现表中存入修改后的信息 3.将修改操作记录在log表中]
        public static int UpdateTransCard(string TID, string RID, string FirstCheck, string SendRepair, string LastCheck, string OneRepair,
            string TwoCheck, string TwoRepair, string ThreeCheck, string ThreeRepair, string Comments, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_TransCardHis ";
            strPerSql += " select TID,RID,FirstCheck,SendRepair,LastCheck,OneRepair,TwoCheck,TwoRepair,ThreeCheck,ThreeRepair,Comments,Validate, ";
            strPerSql += " '" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_TransCard where TID='" + TID + "' and validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = " UPDATE tk_TransCard SET RID='" + RID + "',FirstCheck='" + FirstCheck + "',SendRepair='" + SendRepair + "',LastCheck='" + LastCheck + "',";
            strUpdate += " OneRepair='" + OneRepair + "',TwoCheck='" + TwoCheck + "',TwoRepair='" + TwoRepair + "',ThreeCheck='" + ThreeCheck + "',ThreeRepair='" + ThreeRepair + "',";
            strUpdate += " Comments='" + Comments + "',Validate='v' where TID='" + TID + "' and Validate='v' ";

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = RID;
            log.strLogTitle = "修改流转卡";
            log.strLogContent = "修改流转卡成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修流转卡";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);

                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }



        // 下发任务-获取接收任务的小组列表 
        public static UIDataTable getGroupList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getGroupList", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        // 下发任务-获取人员列表 
        public static UIDataTable getPersonList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getPersonList", CommandType.StoredProcedure, sqlPar, "AccountCnn");
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

        // 下发任务-确定下发任务
        public static int SendTask(string RID, string FinishDate, string Checked, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将登记卡原信息存入His表中
            // 2.在现表中存入修改后的信息，修改状态为2
            // 3.在下发任务表中添加小组和人员信息
            // 4.将操作记录在log表中【完成时间/是否送外部检测不为空】
            string strPerSql = " insert into tk_RepairCardHis ";
            strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
            strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
            strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_RepairCard where RID='" + RID + "' and Validate='v' ";
            // 超声波
            strPerSql += " insert into tk_UTRepairCardHis ";
            strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
            strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
            strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
            strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_UTRepairCard where RID='" + RID + "' and Validate='v' ";
            //
            string strUpdate = " UPDATE tk_RepairCard SET State='2',FinishDate='" + FinishDate + "' ";
            strUpdate += " where RID='" + RID + "' AND Validate='v' ";
            strUpdate += " UPDATE tk_UTRepairCard SET State='2',FinishDate='" + FinishDate + "' ";
            strUpdate += " where RID='" + RID + "' AND Validate='v' ";
            //
            OperateLog log = new OperateLog();
            log.strMarkID = RID;
            log.strLogTitle = "修改登记卡";
            log.strLogContent = "修改登记卡成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "维修登记卡";

            // 下发任务的操作记录
            OperateLog log1 = new OperateLog();
            log1.strMarkID = RID;
            log1.strLogTitle = "下发任务";
            log1.strLogContent = "下发任务成功，将信息记录到对应表中";
            log1.strLogPerson = acc.UserName;
            log1.strLogTime = DateTime.Now;
            log1.strType = "下发任务";
            //
            string[] str = Checked.Split('&');
            string strInsertSql = " insert into tk_RepairSend(RID,RepairUser,GroupID,Validate) values('"
                + RID + "','" + str[1] + "','" + str[0] + "','v') ";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate + strInsertSql, CommandType.Text, null);

                insertlog = OperateLog(log, ref a_strErr);
                insertlog += OperateLog(log1, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }



        // 确认收货 
        public static int CheckReceive(string RIDs, string TakeID, string UnitName, string DeliverName, string DeliverTel,
             string DeliverDate, string ReceiveName, string ReceiveTel, string ReceiveDate, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将登记卡原信息存入His表中
            // 2.在现表中存入修改后的信息，修改唯一收货单号，并将状态改为1
            // 3.将收货单信息填入数据库表中
            // 4.将操作记录在log表中 
            string[] strRID = RIDs.Split(',');// 获取RID值 
            string strPerSql = "";
            string strUpdate = "";
            string strInsert = "";

            for (int i = 0; i < strRID.Length; i++)
            {
                strPerSql += " insert into tk_RepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_RepairCard where RID='" + strRID[i] + "' and Validate='v' ";
                // 超声波
                strPerSql += " insert into tk_UTRepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_UTRepairCard where RID='" + strRID[i] + "' and Validate='v' ";
                //
                strUpdate += " UPDATE tk_RepairCard SET State='1',TakeID='" + TakeID + "' ";
                strUpdate += " where RID='" + strRID[i] + "' AND Validate='v' ";
                //超声波
                strUpdate += " UPDATE tk_UTRepairCard SET State='1',TakeID='" + TakeID + "' ";
                strUpdate += " where RID='" + strRID[i] + "' AND Validate='v' ";
                //
                strInsert += " insert into tk_TakeDelivery values('" + TakeID + "','" + strRID[i] + "','" + UnitName + "','" + DeliverName + "','" + DeliverTel + "','" + DeliverDate + "','";
                strInsert += ReceiveName + "','" + ReceiveTel + "','" + ReceiveDate + "','v','" + DateTime.Now + "','" + acc.UserName + "') ";
            }
            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate + strInsert, CommandType.Text, null);
                //
                for (int j = 0; j < strRID.Length; j++)
                {
                    OperateLog log = new OperateLog();
                    log.strMarkID = strRID[j];
                    log.strLogTitle = "修改登记卡";
                    log.strLogContent = "修改登记卡成功,同时将旧数据存入历史表中";
                    log.strLogPerson = acc.UserName;
                    log.strLogTime = DateTime.Now;
                    log.strType = "维修登记卡";

                    // 确认收货的操作记录
                    OperateLog log1 = new OperateLog();
                    log1.strMarkID = strRID[j];
                    log1.strLogTitle = "确认收货";
                    log1.strLogContent = "确认收货成功，将收货信息记录到对应表中";
                    log1.strLogPerson = acc.UserName;
                    log1.strLogTime = DateTime.Now;
                    log1.strType = "确认收货";

                    OperateLog(log, ref a_strErr);
                    OperateLog(log1, ref a_strErr);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }

        // 确认收货-判断选中的数据是否已经进行过入库操作
        public static int CheckStockInfo(string IDs, ref string a_strErr)
        {
            a_strErr = "";
            string strSQL = "";
            DataTable dt = new DataTable();
            for (int i = 0; i < IDs.Split(',').Length; i++)
            {
                strSQL = " select count(*) from tk_StockIn where RID='" + IDs.Split(',')[i] + "' and Validate='v' ";
                dt = SQLBase.FillTable(strSQL, "FlowMeterDBCnn");
                if (Convert.ToInt32(dt.Rows[0][0]) != 0)
                {
                    a_strErr += IDs.Split(',')[i] + ",";
                }
            }
            if (a_strErr != "")
            {
                a_strErr = a_strErr.Substring(0, a_strErr.Length - 1);
                return -1;
            }
            else
                return 0;
        }

        // 确认收货-入库-获取入库编码 
        public static string GetNewStockID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SIN'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SIN',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string str = "select RID, RidNo,DateRecord from tk_RIDno where DateRecord='" + strYMD + "' and RID='SIN'";
            dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 确认收货-入库-插入新的入库编码 
        public static string GetStockID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SIN'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SIN',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='SIN' ";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 确认收货-确定入库
        public static int AddStockInfo(tk_StockIn stockIn, string RIDs, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int count = 0;
            int intInsert = 0;
            int intUpdate = 0;
            int insertlog = 0;
            string[] arrRID = RIDs.Split(',');
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsert = "";
            string strUpdate = "";
            string strInsertHis = "";
            string strDel = "";
            for (int i = 0; i < arrRID.Length; i++)
            {
                string strRID = arrRID[i].ToString();
                stockIn.strRID = strRID;
                strInsert += GSqlSentence.GetInsertInfoByD<tk_StockIn>(stockIn, "tk_StockIn");

                strUpdate += " update tk_RepairCard set ReceiveUser='" + stockIn.strReceiveUser + "' ";
                strUpdate += " where RID='" + strRID + "' and Validate='v' ";
                strUpdate += " update tk_UTRepairCard set ReceiveUser='" + stockIn.strReceiveUser + "' ";
                strUpdate += " where RID='" + strRID + "' and Validate='v' ";

                strInsertHis += " insert into tk_RepairCardHis ";
                strInsertHis += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strInsertHis += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strInsertHis += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strInsertHis += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strInsertHis += " from tk_RepairCard where RID='" + strRID + "' and Validate='v' ";
                // 超声波
                strInsertHis += " insert into tk_UTRepairCardHis ";
                strInsertHis += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strInsertHis += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strInsertHis += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strInsertHis += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strInsertHis += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strInsertHis += " from tk_UTRepairCard where RID='" + strRID + "' and Validate='v' ";
                //
                strDel += " update tk_StockIn set Validate='i' where RID ='" + strRID + "' and Validate='v' ";
            }
            try
            {
                // 存入His表中
                if (strInsertHis != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (intInsert > 0)
                {
                    // 修改登记卡 承接人信息
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    // 将入库信息填入入库表单中
                    intInsert += sqlTrans.ExecuteNonQuery(strDel + strInsert, CommandType.Text, null);
                }
                count = intInsert + intUpdate;
                if (count > 0)
                {
                    for (int i = 0; i < arrRID.Length; i++)
                    {
                        string strRID = arrRID[i].ToString();
                        // 入库操作日志记录
                        OperateLog log = new OperateLog();
                        log.strMarkID = strRID;
                        log.strLogTitle = "提交入库信息";
                        log.strLogContent = "提交入库信息成功";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "入库表单";
                        insertlog += OperateLog(log, ref a_strErr);

                        // 修改登记卡 承接人信息日志记录
                        log = new OperateLog();
                        log.strMarkID = strRID;
                        log.strLogTitle = "修改维修登记卡";
                        log.strLogContent = "修改维修登记卡成功，将历史记录存入历史表中";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "维修登记卡";
                        insertlog += OperateLog(log, ref a_strErr);
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return count + insertlog;
        }

        // 收货确认单查看-加载列表
        public static DataTable GetDeliveryInfo(string strTakeID, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select distinct '' as T1,0 as ID,b.MeterName,b.Manufacturer,c.Text as Model,b.MeterID,b.FactoryDate,b.Caliber,'' as [File],b.FaceOther,a.RID ";
                strSql += " from tk_TakeDelivery a ";
                strSql += " left join (select * from tk_RepairCard where Validate='v') b on a.RID=b.RID ";
                strSql += " left join (select * from tk_ConfigContent where validate='v') c on b.Model=c.SID ";
                strSql += " where a.Validate='v' and b.RID!='' and b.TakeID='" + strTakeID + "' ";
                strSql += " union all (select distinct '' as T1,0 as ID,b.MeterName,b.Manufacturer,c.Text as Model,b.MeterID,b.FactoryDate,b.Caliber,'' as [File],b.FaceOther,a.RID ";
                strSql += " from tk_TakeDelivery a ";
                strSql += " left join (select * from tk_UTRepairCard where Validate='v') b on a.RID=b.RID ";
                strSql += " left join (select * from tk_ConfigContent where validate='v') c on b.Model=c.SID ";
                strSql += " where a.Validate='v' and b.RID!='' and b.TakeID='" + strTakeID + "') ";

                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["T1"] = "仪表基本信息";
                        dt.Rows[i]["ID"] = i + 1;

                        string strFace = dt.Rows[i]["FaceOther"].ToString();// 外观检查
                        string strsql = " select (CheckItem+''+CheckContent) as Text ";
                        strsql += " from tk_FaceCheck where Validate='v' and RID='" + dt.Rows[i]["RID"].ToString() + "' ";
                        strsql += " union all(select (CheckItem+''+CheckContent) as Text ";
                        strsql += " from tk_UTFaceCheck where Validate='v' and RID='" + dt.Rows[i]["RID"].ToString() + "') ";
                        // 
                        DataTable dtNew = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
                        string strText = "";
                        if (dtNew != null && dtNew.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtNew.Rows.Count; j++)
                            {
                                strText += dtNew.Rows[j][0].ToString() + ",";
                            }
                            dt.Rows[i]["FaceOther"] = strText.Substring(0, strText.Length - 1) + "," + strFace;
                        }
                        else
                            dt.Rows[i]["FaceOther"] = "";
                    }
                    dt.Columns.Remove("RID");
                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }



        // 收货管理-查询收货单列表
        public static UIDataTable LoadDeliveryList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetDeliveryList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 收货管理-加载收货单详细列表
        public static UIDataTable LoadDetailList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetDeliveryInfo", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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
            //表中有值 
            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                string strsql = "";
                for (int k = 0; k < dtOrder.Rows.Count; k++)
                {
                    string strFace = dtOrder.Rows[k]["FaceText"].ToString();// 外观检查
                    strsql = " select (CheckItem+''+CheckContent) as Text ";
                    strsql += " from tk_FaceCheck where Validate='v' and RID='" + dtOrder.Rows[k][1].ToString() + "' ";
                    strsql += " union all (select (CheckItem+''+CheckContent) as Text ";
                    strsql += " from tk_UTFaceCheck where Validate='v' and RID='" + dtOrder.Rows[k][1].ToString() + "') ";
                    // 
                    DataTable dtNew = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
                    string strText = "";
                    if (dtNew != null && dtNew.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtNew.Rows.Count; j++)
                        {
                            strText += dtNew.Rows[j][0].ToString() + ",";
                        }

                        dtOrder.Rows[k]["FaceText"] = strText.Substring(0, strText.Length - 1) + "," + strFace;
                    }
                    else
                        dtOrder.Rows[k]["FaceText"] = "";
                }
            }
            instData.DtData = dtOrder;
            return instData;
        }

        // 备用
        public static tk_TakeDelivery getNewDelivery(string strTakeID)
        {

            tk_TakeDelivery takeDelivery = new tk_TakeDelivery();
            string strSql = " select * from tk_TakeDelivery where TakeID = '" + strTakeID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                string strRIDs = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strRIDs += dt.Rows[i]["RID"].ToString() + ",";
                }
                takeDelivery.strRID = strRIDs.Substring(0, strRIDs.Length - 1);
                takeDelivery.strTakeID = strTakeID;
                takeDelivery.strUnitName = dt.Rows[0]["UnitName"].ToString();
                takeDelivery.strDeliverName = dt.Rows[0]["DeliverName"].ToString();
                takeDelivery.strDeliverTel = dt.Rows[0]["DeliverTel"].ToString();
                takeDelivery.strDeliverDate = dt.Rows[0]["DeliverDate"].ToString();
                takeDelivery.strReceiveName = dt.Rows[0]["ReceiveName"].ToString();
                takeDelivery.strReceiveTel = dt.Rows[0]["ReceiveTel"].ToString();
                takeDelivery.strReceiveDate = dt.Rows[0]["ReceiveDate"].ToString();
                takeDelivery.strValidate = dt.Rows[0]["Validate"].ToString();
                takeDelivery.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                takeDelivery.strCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return takeDelivery;
        }

        // 收货管理-修改界面获取收货单信息
        public static tk_TakeDelivery getNewDelivery2(string TakeID)
        {

            tk_TakeDelivery takeDelivery = new tk_TakeDelivery();
            string strSql = " select * from tk_TakeDelivery where TakeID = '" + TakeID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                takeDelivery.strTakeID = dt.Rows[0]["TakeID"].ToString();
                takeDelivery.strUnitName = dt.Rows[0]["UnitName"].ToString();
                takeDelivery.strDeliverName = dt.Rows[0]["DeliverName"].ToString();
                takeDelivery.strDeliverTel = dt.Rows[0]["DeliverTel"].ToString();
                takeDelivery.strDeliverDate = dt.Rows[0]["DeliverDate"].ToString();
                takeDelivery.strReceiveName = dt.Rows[0]["ReceiveName"].ToString();
                takeDelivery.strReceiveTel = dt.Rows[0]["ReceiveTel"].ToString();
                takeDelivery.strReceiveDate = dt.Rows[0]["ReceiveDate"].ToString();
                takeDelivery.strValidate = dt.Rows[0]["Validate"].ToString();
                takeDelivery.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                takeDelivery.strCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return takeDelivery;
        }

        // 收货管理-确认修改 
        public static int UpdateDelivery(tk_TakeDelivery takeDelivery, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_TakeDeliveryHis ";
            strPerSql += " select ID,TakeID,RID,UnitName,DeliverName,DeliverTel,DeliverDate,ReceiveName,ReceiveTel,ReceiveDate,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_TakeDelivery where TakeID='" + takeDelivery.strTakeID + "' and Validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = " update tk_TakeDelivery set UnitName='" + takeDelivery.strUnitName + "',DeliverName='" + takeDelivery.strDeliverName + "',DeliverTel='" + takeDelivery.strDeliverTel
                + "',DeliverDate='" + takeDelivery.strDeliverDate + "',ReceiveName='" + takeDelivery.strReceiveName + "',ReceiveTel='" + takeDelivery.strReceiveTel
                + "',ReceiveDate='" + takeDelivery.strReceiveDate + "' ";
            strUpdate += " where Validate='v' and TakeID ='" + takeDelivery.strTakeID + "' ";

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = takeDelivery.strTakeID;
            log.strLogTitle = "修改收货单";
            log.strLogContent = "修改收货单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "收货单";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    if (intUpdate > 0)
                        insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdate;
        }

        // 收货管理-撤销收货单
        public static int ReDelivery(string strRIDs, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string[] strList = strRIDs.Split(',');
            string strPerSql = "";
            string strUpdate = "";
            a_strErr = "";

            for (int i = 0; i < strList.Length; i++)
            {
                // 1.将原信息存入His表中 收货信息/登记卡
                strPerSql += " insert into tk_TakeDeliveryHis ";
                strPerSql += " select ID,TakeID,RID,UnitName,DeliverName,DeliverTel,DeliverDate,ReceiveName,ReceiveTel,ReceiveDate,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_TakeDelivery where RID ='" + strList[i] + "' and Validate='v' ";

                strPerSql += "  insert into tk_RepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_RepairCard where RID='" + strList[i] + "' and Validate='v' ";
                // 超声波
                strPerSql += " insert into tk_UTRepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_UTRepairCard where RID='" + strList[i] + "' and Validate='v' ";

                // 2.在现表中存入修改后的信息 收货信息/登记卡 
                strUpdate += " update tk_TakeDelivery set Validate='i' where RID='" + strList[i] + "' and Validate='v' ";
                strUpdate += " update tk_RepairCard set State='0',TakeID='' where RID='" + strList[i] + "' and Validate='v' ";
                strUpdate += " update tk_UTRepairCard set State='0',TakeID='' where RID='" + strList[i] + "' and Validate='v' ";
            }
            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);

                    // 3.将修改操作记录在log表中 
                    if (intUpdate > 0)
                    {
                        for (int j = 0; j < strList.Length; j++)
                        {
                            OperateLog log = new OperateLog();
                            log.strMarkID = strList[j];
                            log.strLogTitle = "撤销收货单";
                            log.strLogContent = "撤销收货单成功,同时将旧数据存入历史表中";
                            log.strLogPerson = acc.UserName;
                            log.strLogTime = DateTime.Now;
                            log.strType = "收货单";

                            OperateLog log1 = new OperateLog();
                            log1.strMarkID = strList[j];
                            log1.strLogTitle = "修改维修登记卡";
                            log1.strLogContent = "修改维修登记卡信息成功,同时将旧数据存入历史表中";
                            log1.strLogPerson = acc.UserName;
                            log1.strLogTime = DateTime.Now;
                            log1.strType = "维修登记卡";

                            insertlog = OperateLog(log, ref a_strErr);
                            insertlog += OperateLog(log1, ref a_strErr);
                        }
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdate + intInsert + insertlog;
        }




        // 送检单管理-加载送检表列表 
        public static UIDataTable LoadInspecList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetInspecList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 新建送检单-获取送检单编号
        public static string GetNewSID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SJ'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SJ',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string str = "select RID, RidNo,DateRecord from tk_RIDno where DateRecord='" + strYMD + "' and RID='SJ'";
            dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 新建送检单-插入送检单编号
        public static string GetSID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SJ'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SJ',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='SJ' ";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 新建送检单-加载仪表列表 [登记卡中是否送检IsOut=1&&状态为7-维修完成]
        public static UIDataTable LoadMeterList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetMeterList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 新建送检单-根据RID获取仪表信息 
        public static DataTable GetMeterDetail(string RID)
        {
            string sql = "select RID,MeterID,OutUnit From tk_RepairCard where RID='" + RID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
            return dt;
        }

        // 新建送检单-确定提交 将登记卡状态置为 8-外送检测中
        public static int InsertInspec(tk_InspecMain InspecMain, List<tk_InspecDetail> list, string[] OutUnit, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int insertlog = 0;
            int intUpdate = 0;

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<tk_InspecMain>(InspecMain, "tk_InspecMain");
            if (list.Count > 0)
                strInsert += GSqlSentence.GetInsertByList(list, "tk_InspecDetail");

            a_strErr = "";
            string strInsertHis = "";
            string strUpdate = "";
            for (int i = 0; i < list.Count; i++)
            {
                // 将历史记录存入His表中
                strInsertHis += "  insert into tk_RepairCardHis ";
                strInsertHis += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strInsertHis += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strInsertHis += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strInsertHis += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strInsertHis += " from tk_RepairCard where RID='" + list[i].RID + "' and Validate='v' ";
                // 超声波
                strInsertHis += " insert into tk_UTRepairCardHis ";
                strInsertHis += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strInsertHis += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strInsertHis += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strInsertHis += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strInsertHis += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strInsertHis += " from tk_UTRepairCard where RID='" + list[i].RID + "' and Validate='v' ";

                // 修改表中状态 
                strUpdate += " UPDATE tk_RepairCard SET State='8', OutUnit='" + OutUnit[i] + "' ";
                strUpdate += " where RID='" + list[i].RID + "' AND Validate='v' ";
                // 超声波
                strUpdate += " UPDATE tk_UTRepairCard SET State='8', OutUnit='" + OutUnit[i] + "' ";
                strUpdate += " where RID='" + list[i].RID + "' AND Validate='v' ";
            }
            try
            {
                if (strInsertHis != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertHis + strInsert, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);

                OperateLog log = new OperateLog();
                log.strMarkID = InspecMain.strSID;
                log.strLogTitle = "修改维修登记卡";
                log.strLogContent = "修改维修登记卡状态为8成功";
                log.strLogPerson = acc.UserName;
                log.strLogTime = DateTime.Now;
                log.strType = "维修登记卡";

                insertlog += OperateLog(log, ref a_strErr);

                log.strMarkID = InspecMain.strSID;
                log.strLogTitle = "新建送检单";
                log.strLogContent = "新建送检单成功";
                log.strLogPerson = acc.UserName;
                log.strLogTime = DateTime.Now;
                log.strType = "送检单";

                insertlog += OperateLog(log, ref a_strErr);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intInsert + intUpdate + insertlog;
        }

        // 修改送检单-通过SID获取送检单信息  
        public static tk_InspecMain getInspectBySID(string SID)
        {

            tk_InspecMain inspecMain = new tk_InspecMain();
            string strSql = " select * from tk_InspecMain where SID = '" + SID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                inspecMain.strSID = SID;
                inspecMain.strLinkPerson = dt.Rows[0]["LinkPerson"].ToString();
                inspecMain.strLinkTel = dt.Rows[0]["LinkTel"].ToString();
                inspecMain.strInspecDate = dt.Rows[0]["InspecDate"].ToString();
                inspecMain.strBathID = dt.Rows[0]["BathID"].ToString();
                inspecMain.strValidate = dt.Rows[0]["Validate"].ToString();
                inspecMain.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                inspecMain.strCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return inspecMain;
        }

        // 修改送检单-获取多条详细信息列表 
        public static DataTable GetInspecInfo(string SID)
        {
            string str = "select a.RID,a.MeterID,a.Accuracy,a.Mcount,b.OutUnit from tk_InspecDetail a, tk_RepairCard b ";
            str += " where a.SID='" + SID + "' AND a.RID=b.RID and a.Validate='v' and b.Validate='v' ";
            str += " union all (select a.RID,a.MeterID,a.Accuracy,a.Mcount,b.OutUnit from tk_InspecDetail a, tk_UTRepairCard b ";
            str += " where a.SID='" + SID + "' AND a.RID=b.RID and a.Validate='v' and b.Validate='v') ";
            DataTable dt = SQLBase.FillTable(str, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return null;
            return dt;
        }

        // 修改送检单-确认修改
        public static int UpdateInspec(tk_InspecMain inspecMain, List<tk_InspecDetail> list, string strRIDs, string[] OutUnit, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsert = "";
            string strInsertHis = "";
            string strUpdate = "";
            string strDel = "";

            // 修改送检单主表信息 
            strUpdate = GSqlSentence.GetUpdateInfoByD<tk_InspecMain>(inspecMain, "SID", "tk_InspecMain");
            // 插入新的送检详细信息
            if (list.Count > 0)
                strInsert = GSqlSentence.GetInsertByList(list, "tk_InspecDetail");

            // 1.1将原登记卡信息存入His表中 
            string strInsertHis1 = "  insert into tk_RepairCardHis ";
            strInsertHis1 += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strInsertHis1 += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
            strInsertHis1 += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
            strInsertHis1 += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strInsertHis1 += " from tk_RepairCard where RID in (" + strRIDs + ") and Validate='v' ";
            // 超声波
            strInsertHis1 += " insert into tk_UTRepairCardHis ";
            strInsertHis1 += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
            strInsertHis1 += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
            strInsertHis1 += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
            strInsertHis1 += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
            strInsertHis1 += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
            strInsertHis1 += " from tk_UTRepairCard where RID in (" + strRIDs + ") and Validate='v' ";

            // 1.2将原送检主表信息存入His表中
            string strInsertHis2 = " insert into tk_InspecMainHis ";
            strInsertHis2 += " select SID,LinkPerson,LinkTel,InspecDate,BathID,CreateTime,CreateUser,Validate,'"
                + DateTime.Now + "','" + acc.UserName + "' ";
            strInsertHis2 += " from tk_InspecMain where SID='" + inspecMain.strSID + "' and Validate='v' ";

            // 1.3将原送检表详细信息存入His表中
            string strInsertHis3 = " insert into tk_InspecDetailHis ";
            strInsertHis3 += " select ID,SID,RID,MeterID,Accuracy,Mcount,CreateTime,CreateUser,Validate,'"
                + DateTime.Now + "','" + acc.UserName + "' ";
            strInsertHis3 += " from tk_InspecDetail where SID='" + inspecMain.strSID + "' and Validate='v' ";

            strInsertHis = strInsertHis1 + strInsertHis2 + strInsertHis3;

            // 2.1将登记卡中相关RID对应的状态置为 8-送检中
            string strUpSql = "";
            string strIDs = "";
            for (int i = 0; i < list.Count; i++)
            {
                strUpSql += " update tk_RepairCard set State='8', OutUnit='" + OutUnit[i] + "' where RID='" + list[i].RID + "' and Validate='v' ";
                strIDs += "'" + list[i].RID + "',";
                // 超声波
                strUpSql += " update tk_UTRepairCard set State='8', OutUnit='" + OutUnit[i] + "' where RID='" + list[i].RID + "' and Validate='v' ";
                strIDs += "'" + list[i].RID + "',";

            }
            strIDs = strIDs.Substring(0, strIDs.Length - 1);

            string Drids = "";
            // 记录
            for (int k = 0; k < strRIDs.Split(',').Length; k++)
            {
                string[] strIdList = strIDs.Split(',');
                string rid1 = strRIDs.Split(',')[k];// RID 
                int Bo = Array.IndexOf(strIdList, rid1);
                if (Bo == -1)// 不存在
                    Drids += "," + rid1;
            }

            // 2.2将删掉的记录状态置为前一状态
            strUpSql += " update tk_RepairCard set State='7' where RID in (" + strIDs + ") and Validate='v' ";
            strUpSql += " update tk_UTRepairCard set State='7' where RID in (" + strIDs + ") and Validate='v' ";

            // 2.3修改送检主表信息
            string strUpSql2 = " update tk_InspecMain set LinkPerson='" + inspecMain.strLinkPerson + "',LinkTel='" + inspecMain.strLinkTel
                + "',InspecDate='" + inspecMain.strInspecDate + "',BathID='" + inspecMain.strBathID + "' ";
            strUpSql2 += " where SID='" + inspecMain.strSID + "' and Validate='v' ";

            strUpdate += strUpSql + strUpSql;

            // 3.删除原送检表详细信息 
            strDel = " delete from tk_InspecDetail where SID='" + inspecMain.strSID + "' and Validate='v' ";

            // 将送检单修改操作记录在log表中 
            OperateLog log = new OperateLog();
            log.strMarkID = inspecMain.strSID;
            log.strLogTitle = "修改送检单";
            log.strLogContent = "修改送检单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "送检单";

            try
            {
                if (strInsertHis != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    // 删除多条 再插入
                    intInsert += sqlTrans.ExecuteNonQuery(strDel + strInsert, CommandType.Text, null);
                }
                insertlog = OperateLog(log, ref a_strErr);
                for (int m = 0; m < list.Count; m++)
                {
                    // 将登记卡修改操作记录在log表中
                    log.strMarkID = list[m].RID;
                    log.strLogTitle = "修改维修登记卡";
                    log.strLogContent = "修改维修登记卡成功,同时将旧数据存入历史表中";
                    log.strLogPerson = acc.UserName;
                    log.strLogTime = DateTime.Now;
                    log.strType = "维修登记卡";

                    insertlog += OperateLog(log, ref a_strErr);
                }
                //
                for (int j = 0; j < Drids.Split(',').Length; j++)
                {
                    if (Drids.Split(',')[j] != "")
                    {
                        // 将登记卡修改操作记录在log表中
                        log.strMarkID = Drids.Split(',')[j];
                        log.strLogTitle = "修改维修登记卡";
                        log.strLogContent = "修改维修登记卡成功,同时将旧数据存入历史表中";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "维修登记卡";

                        insertlog += OperateLog(log, ref a_strErr);
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intInsert + intUpdate + insertlog;
        }

        // 已删除：查看详细-获取第三方检定单位名称
        public static string getUnitInspec(string strSID)
        {
            string strSql = " select b.UnitName from tk_InspecMain a ";
            strSql += " left join (select * from tk_ConfigUnit where Validate='v') b on a.UnitID=b.UnitID ";
            strSql += " where a.SID='" + strSID + "' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
                return dt.Rows[0][0].ToString();
        }

        // 导出-获取明细列表 
        public static DataTable GetDetailInfo(string strWhere)
        {
            string strSql = " select 0 as ID,c.MeterName,c.Manufacturer,d.Text as Model,a.MeterID,c.Caliber, ";
            strSql += " a.Accuracy,a.Mcount,c.CustomerAddr,b.BathID ";
            strSql += " from tk_InspecDetail a ";
            strSql += " left join (select * from tk_InspecMain where Validate='v') b on a.SID=b.SID ";
            strSql += " left join (select * from tk_RepairCard where Validate='v') c on a.RID=c.RID ";
            strSql += " left join (select * from tk_ConfigContent where validate='v' and Type='YBModel') d on c.Model=d.SID ";
            strSql += " where a.Validate='v' and c.RID!='' " + strWhere + " ";
            // 超声波
            strSql += " union all (select 0 as ID,c.MeterName,c.Manufacturer,d.Text as Model,a.MeterID,c.Caliber, ";
            strSql += " a.Accuracy,a.Mcount,c.CustomerAddr,b.BathID ";
            strSql += " from tk_InspecDetail a ";
            strSql += " left join (select * from tk_InspecMain where Validate='v') b on a.SID=b.SID ";
            strSql += " left join (select * from tk_UTRepairCard where Validate='v') c on a.RID=c.RID ";
            strSql += " left join (select * from tk_ConfigContent where validate='v' and Type='YBModel') d on c.Model=d.SID ";
            strSql += " where a.Validate='v' and c.RID!=''" + strWhere + " ) order by b.BathID ";

            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["ID"] = i + 1;
                }
                return dt;
            }
            else
                return null;
        }

        // 查看详细-获取详细列表
        public static UIDataTable LoadInspecDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetInspecDetail", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

            //
            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    dtOrder.Rows[i][1] = i + 1;
                }
            }

            instData.DtData = dtOrder;
            return instData;
        }





        // 发货管理-查询收货单列表
        public static UIDataTable LoadSendList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetSendList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 发货管理-加载发货单详细列表
        public static UIDataTable LoadSendDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetSendInfo", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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
            // 表中有值 
            //if (dtOrder != null && dtOrder.Rows.Count > 0)
            //{
            //    string strsql = "";
            //    for (int k = 0; k < dtOrder.Rows.Count; k++)
            //    {
            //        string strFace = dtOrder.Rows[k]["FaceText"].ToString();// 外观检查
            //        strsql = " select (CheckItem+''+CheckContent) as Text ";
            //        strsql += " from tk_FaceCheck where Validate='v' and RID='" + dtOrder.Rows[k][1].ToString() + "' ";
            //        strsql += " union all (select (CheckItem+''+CheckContent) as Text ";
            //        strsql += " from tk_UTFaceCheck where Validate='v' and RID='" + dtOrder.Rows[k][1].ToString() + "') ";
            //        // 
            //        DataTable dtNew = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
            //        string strText = "";
            //        if (dtNew != null && dtNew.Rows.Count > 0)
            //        {
            //            for (int j = 0; j < dtNew.Rows.Count; j++)
            //            {
            //                strText += dtNew.Rows[j][0].ToString() + ",";
            //            }
            //        }
            //        dtOrder.Rows[k]["FaceText"] = strText.Substring(0, strText.Length - 1) + "," + strFace;
            //    }
            //}
            instData.DtData = dtOrder;
            return instData;
        }

        // 发货管理-确认修改 
        public static int UpdateSendDelivery(tk_SendDelivery sendDelivery, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将原信息存入His表中 
            string strPerSql = " insert into tk_SendDeliveryHis ";
            strPerSql += " select ID,DeliverID,RID,UnitName,ReceiveName,ReceiveTel,ReceiveDate,ReceiveAddr,SendName,SendTel,SendDate,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strPerSql += " from tk_SendDelivery where DeliverID='" + sendDelivery.strDeliverID + "' and Validate='v' ";

            // 2.在现表中存入修改后的信息 
            string strUpdate = " update tk_SendDelivery set UnitName='" + sendDelivery.strUnitName + "',ReceiveName='" + sendDelivery.strReceiveName + "',ReceiveTel='" + sendDelivery.strReceiveTel
                + "',ReceiveDate='" + sendDelivery.strReceiveDate + "',ReceiveAddr='" + sendDelivery.strReceiveAddr + "',SendName='" + sendDelivery.strSendName + "',SendTel='" + sendDelivery.strSendTel
                + "', SendDate='" + sendDelivery.strSendDate + "' ";
            strUpdate += " where Validate='v' and DeliverID ='" + sendDelivery.strDeliverID + "' ";

            // 3.将修改操作记录在log表中 
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = sendDelivery.strDeliverID;
            log.strLogTitle = "修改发货单";
            log.strLogContent = "修改发货单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "发货单";

            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    if (intUpdate > 0)
                        insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdate;
        }

        // 发货管理-根据发货单号获取详细信息 
        public static tk_SendDelivery getNewSendDelivery(string DeliverID)
        {
            tk_SendDelivery sendDelivery = new tk_SendDelivery();
            string strSql = " select * from tk_SendDelivery where DeliverID = '" + DeliverID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                string strRIDs = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strRIDs += dt.Rows[i]["RID"].ToString() + ",";
                }
                sendDelivery.strRID = strRIDs.Substring(0, strRIDs.Length - 1);
                sendDelivery.strDeliverID = DeliverID;
                sendDelivery.strUnitName = dt.Rows[0]["UnitName"].ToString();
                sendDelivery.strReceiveName = dt.Rows[0]["ReceiveName"].ToString();
                sendDelivery.strReceiveTel = dt.Rows[0]["ReceiveTel"].ToString();
                sendDelivery.strReceiveDate = dt.Rows[0]["ReceiveDate"].ToString();
                sendDelivery.strReceiveAddr = dt.Rows[0]["ReceiveAddr"].ToString();
                sendDelivery.strSendName = dt.Rows[0]["SendName"].ToString();
                sendDelivery.strSendTel = dt.Rows[0]["SendTel"].ToString();
                sendDelivery.strSendDate = dt.Rows[0]["SendDate"].ToString();
                sendDelivery.strValidate = dt.Rows[0]["Validate"].ToString();
                sendDelivery.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                sendDelivery.strCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return sendDelivery;
        }

        // 发货管理-撤销发货单
        public static int ReSendDelivery(string strRIDs, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string[] strList = strRIDs.Split(',');
            string strPerSql = "";
            string strUpdate = "";
            a_strErr = "";

            for (int i = 0; i < strList.Length; i++)
            {
                // 1.将原信息存入His表中 发货信息/登记卡
                strPerSql += " insert into tk_SendDeliveryHis ";
                strPerSql += " select ID,DeliverID,RID,UnitName,ReceiveName,ReceiveTel,ReceiveDate,ReceiveAddr,SendName,SendTel,SendDate,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_SendDelivery where RID ='" + strList[i] + "' and Validate='v' ";

                strPerSql += "  insert into tk_RepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_RepairCard where RID='" + strList[i] + "' and Validate='v' ";
                // 超声波
                strPerSql += " insert into tk_UTRepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_UTRepairCard where RID='" + strList[i] + "' and Validate='v' ";

                // 2.在现表中存入修改后的信息 发货信息/登记卡 
                strUpdate += " update tk_SendDelivery set Validate='i' where RID='" + strList[i] + "' and Validate='v' ";
                strUpdate += " update tk_RepairCard set DeliverID='' where RID='" + strList[i] + "' and Validate='v' ";
                strUpdate += " update tk_UTRepairCard set DeliverID='' where RID='" + strList[i] + "' and Validate='v' ";
            }
            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);

                    // 3.将修改操作记录在log表中 
                    if (intUpdate > 0)
                    {
                        for (int j = 0; j < strList.Length; j++)
                        {
                            OperateLog log = new OperateLog();
                            log.strMarkID = strList[j];
                            log.strLogTitle = "撤销发货单";
                            log.strLogContent = "撤销发货单成功,同时将旧数据存入历史表中";
                            log.strLogPerson = acc.UserName;
                            log.strLogTime = DateTime.Now;
                            log.strType = "发货单";

                            OperateLog log1 = new OperateLog();
                            log1.strMarkID = strList[j];
                            log1.strLogTitle = "修改维修登记卡";
                            log1.strLogContent = "修改维修登记卡信息成功,同时将旧数据存入历史表中";
                            log1.strLogPerson = acc.UserName;
                            log1.strLogTime = DateTime.Now;
                            log1.strType = "维修登记卡";

                            insertlog = OperateLog(log, ref a_strErr);
                            insertlog += OperateLog(log1, ref a_strErr);
                        }
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdate + intInsert + insertlog;
        }

        // 发货管理-获取发货单信息
        public static tk_SendDelivery getNewSendDelivery2(string DeliverID)
        {
            tk_SendDelivery sendDelivery = new tk_SendDelivery();
            string strSql = " select * from tk_SendDelivery where DeliverID = '" + DeliverID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                sendDelivery.strDeliverID = dt.Rows[0]["DeliverID"].ToString();
                sendDelivery.strUnitName = dt.Rows[0]["UnitName"].ToString();
                sendDelivery.strReceiveName = dt.Rows[0]["ReceiveName"].ToString();
                sendDelivery.strReceiveTel = dt.Rows[0]["ReceiveTel"].ToString();
                sendDelivery.strReceiveDate = dt.Rows[0]["ReceiveDate"].ToString();
                sendDelivery.strReceiveAddr = dt.Rows[0]["ReceiveAddr"].ToString();
                sendDelivery.strSendName = dt.Rows[0]["SendName"].ToString();
                sendDelivery.strSendTel = dt.Rows[0]["SendTel"].ToString();
                sendDelivery.strSendDate = dt.Rows[0]["SendDate"].ToString();
                sendDelivery.strValidate = dt.Rows[0]["Validate"].ToString();
                sendDelivery.strCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                sendDelivery.strCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return sendDelivery;
        }

        // 发货确认单查看-加载列表 
        public static DataTable GetSendDeliveryInfo(string strDeliverID, ref string a_strErr)
        {
            a_strErr = "";
            try
            {
                string strSql = " select distinct '' as T1,0 as ID,b.MeterName,b.Manufacturer,c.Text as Model,b.MeterID,b.FactoryDate,b.Caliber,'' as [File],b.FaceOther,a.RID ";
                strSql += " from tk_SendDelivery a ";
                strSql += " left join (select * from tk_RepairCard where Validate='v') b on a.RID=b.RID ";
                strSql += " left join (select * from tk_ConfigContent where validate='v') c on b.Model=c.SID ";
                strSql += " where a.Validate='v' and b.RID!='' and b.DeliverID='" + strDeliverID + "' ";
                strSql += " union all (";
                strSql += " select distinct '' as T1,0 as ID,b.MeterName,b.Manufacturer,c.Text as Model,b.MeterID,b.FactoryDate,b.Caliber,'' as [File],b.FaceOther,a.RID ";
                strSql += " from tk_SendDelivery a ";
                strSql += " left join (select * from tk_UTRepairCard where Validate='v') b on a.RID=b.RID ";
                strSql += " left join (select * from tk_ConfigContent where validate='v') c on b.Model=c.SID ";
                strSql += " where a.Validate='v' and b.RID!='' and b.DeliverID='" + strDeliverID + "' )";

                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["T1"] = "仪表基本信息";
                        dt.Rows[i]["ID"] = i + 1;
                        //
                        //string strFace = dt.Rows[i]["FaceOther"].ToString();// 外观检查
                        //string strsql = " select (CheckItem+''+CheckContent) as Text ";
                        //strsql += " from tk_FaceCheck where Validate='v' and RID='" + dt.Rows[i]["RID"].ToString() + "' ";
                        //strsql += " union all (select (CheckItem+''+CheckContent) as Text ";
                        //strsql += " from tk_UTFaceCheck where Validate='v' and RID='" + dt.Rows[i]["RID"].ToString() + "') ";
                        //// 
                        //DataTable dtNew = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
                        //string strText = "";
                        //if (dtNew != null && dtNew.Rows.Count > 0)
                        //{
                        //    for (int j = 0; j < dtNew.Rows.Count; j++)
                        //    {
                        //        strText += dtNew.Rows[j][0].ToString() + ",";
                        //    }

                        //    dt.Rows[i]["FaceOther"] = strText.Substring(0, strText.Length - 1) + "," + strFace;
                        //}
                        //else
                        //    dt.Rows[i]["FaceOther"] = "";
                    }
                    dt.Columns.Remove("RID");
                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }

        // 确认发货-确定 
        public static int CheckSendInfo(string RIDs, string DeliverID, string UnitName, string ReceiveName, string ReceiveTel,
            string ReceiveDate, string ReceiveAddr, string SendName, string SendTel, string SendDate, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            // 1.将登记卡原信息存入His表中
            // 2.在现表中存入修改后的信息，修改发货单号
            // 3.将发货单信息填入数据库表中
            // 4.将操作记录在log表中 
            string[] strRID = RIDs.Split(',');// 获取RID值 
            string strPerSql = "";
            string strUpdate = "";
            string strInsert = "";

            for (int i = 0; i < strRID.Length; i++)
            {
                strPerSql += " insert into tk_RepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " Precision,ModelType,Model,FactoryDate,RecordNum,FlowRange,Pressure,Caliber,PreUnit,NewUnit,X_ID,X_CertifID,X_Model,X_Manufacturer,X_Standard,";
                strPerSql += " X_Operating,X_FactoryDate,X_Pressure,X_Temperature,X_Data,X_PreUnit,X_NewUnit,FaceOther,RepairContent,CheckUser,IsRepair,ConfirmUser,";
                strPerSql += " ConfirmDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_RepairCard where RID='" + strRID[i] + "' and Validate='v' ";
                // 超声波
                strPerSql += " insert into tk_UTRepairCardHis ";
                strPerSql += " select RID,RepairID,CustomerName,CustomerAddr,S_Name,S_Tel,S_Date,SubUnit,MeterID,CertifID,MeterName,Manufacturer,";
                strPerSql += " ModelType,Model,CirNum,CirVersion,FactoryDate,FlowRange,Pressure,Caliber,TrackA1,TrackA2,TrackA3,TrackA4,TrackA5,";
                strPerSql += " TrackA6,TrackB1,TrackB2,TrackB3,TrackB4,TrackB5,TrackB6,Comments1,FaceOther,RepairContent,CheckUser,FirstCheck,";
                strPerSql += " FirstDate,SecondCheck,SecondDate,ThirdCheck,ThirdDate,Text,GetTypeModel,G_Name,G_Tel,G_Date,State,FinishDate,IsOut,OutUnit,";
                strPerSql += " TakeID,DeliverID,RecieveID,ReceiveUser,Logistic,ModelProperty,Validate,CreateTime,CreateUser,'" + DateTime.Now + "','" + acc.UserName + "'";
                strPerSql += " from tk_UTRepairCard where RID='" + strRID[i] + "' and Validate='v' ";
                //
                strUpdate += " UPDATE tk_RepairCard SET DeliverID='" + DeliverID + "' ";
                strUpdate += " where RID='" + strRID[i] + "' AND Validate='v' ";
                // 超声波
                strUpdate += " UPDATE tk_UTRepairCard SET DeliverID='" + DeliverID + "' ";
                strUpdate += " where RID='" + strRID[i] + "' AND Validate='v' ";
                //
                strInsert += " insert into tk_SendDelivery values('" + DeliverID + "','" + strRID[i] + "','" + UnitName + "','" + ReceiveName + "','" + ReceiveTel + "','" + ReceiveDate + "','";
                strInsert += ReceiveAddr + "','" + SendName + "','" + SendTel + "','" + SendDate + "','v','" + DateTime.Now + "','" + acc.UserName + "') ";
            }
            try
            {
                if (strPerSql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strPerSql, CommandType.Text, null);
                if (intInsert > 0)
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate + strInsert, CommandType.Text, null);
                //
                for (int j = 0; j < strRID.Length; j++)
                {
                    OperateLog log = new OperateLog();
                    log.strMarkID = strRID[j];
                    log.strLogTitle = "修改登记卡";
                    log.strLogContent = "修改登记卡成功,同时将旧数据存入历史表中";
                    log.strLogPerson = acc.UserName;
                    log.strLogTime = DateTime.Now;
                    log.strType = "维修登记卡";

                    // 确认收货的操作记录
                    OperateLog log1 = new OperateLog();
                    log1.strMarkID = strRID[j];
                    log1.strLogTitle = "确认发货";
                    log1.strLogContent = "确认发货成功，将发货信息记录到对应表中";
                    log1.strLogPerson = acc.UserName;
                    log1.strLogTime = DateTime.Now;
                    log1.strType = "确认发货";

                    OperateLog(log, ref a_strErr);
                    OperateLog(log1, ref a_strErr);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }

        // 确认发货-判断选中的数据是否已经进行过出库记录操作
        public static int CheckStockOutInfo(string IDs, ref string a_strErr)
        {
            a_strErr = "";
            string strSQL = "";
            DataTable dt = new DataTable();
            for (int i = 0; i < IDs.Split(',').Length; i++)
            {
                strSQL = " select count(*) from tk_StockOut where RID='" + IDs.Split(',')[i] + "' and Validate='v' ";
                dt = SQLBase.FillTable(strSQL, "FlowMeterDBCnn");
                if (Convert.ToInt32(dt.Rows[0][0]) != 0)
                {
                    a_strErr += IDs.Split(',')[i] + ",";
                }
            }
            if (a_strErr != "")
            {
                a_strErr = a_strErr.Substring(0, a_strErr.Length - 1);
                return -1;
            }
            else
                return 0;
        }

        // 确认发货-出库-获取出库编号 
        public static string GetNewOutID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SO'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SO',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string str = "select RID, RidNo,DateRecord from tk_RIDno where DateRecord='" + strYMD + "' and RID='SO'";
            dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 确认发货-出库-插入新的出库编码 
        public static string GetOutID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='SO'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('SO',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='SO' ";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 确认发货-确定出库
        public static int AddStockOutInfo(tk_StockOut stockOut, string RIDs, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int count = 0;
            int intInsert = 0;
            int intUpdate = 0;
            int insertlog = 0;
            string[] arrRID = RIDs.Split(',');
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsert = "";
            string strDel = "";
            for (int i = 0; i < arrRID.Length; i++)
            {
                string strRID = arrRID[i].ToString();
                stockOut.strRID = strRID;
                strInsert += GSqlSentence.GetInsertInfoByD<tk_StockOut>(stockOut, "tk_StockOut");
                strDel += " update tk_StockOut set Validate='i' where RID ='" + strRID + "' and Validate='v' ";
            }
            try
            {
                // 将出库信息填入出库表单中
                intInsert = sqlTrans.ExecuteNonQuery(strDel + strInsert, CommandType.Text, null);
                count = intInsert + intUpdate;
                if (count > 0)
                {
                    for (int i = 0; i < arrRID.Length; i++)
                    {
                        string strRID = arrRID[i].ToString();
                        // 出库操作日志记录
                        OperateLog log = new OperateLog();
                        log.strMarkID = strRID;
                        log.strLogTitle = "提交出库信息";
                        log.strLogContent = "提交出库信息成功";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "出库表单";

                        insertlog += OperateLog(log, ref a_strErr);
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return count + insertlog;
        }




        // 上传附件 
        public static int InsertNewFile(tk_FileUpload fileUp, byte[] fileByte, ref string a_strErr)
        {
            int intlog = 0;
            int intInsert = 0;
            string strErr = "";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();

            string strInsertOrder = "";
            if (fileUp.StrFileName != "")
            {
                strInsertOrder = "insert into tk_FileUpload (RID,[Type],FileName,FileInfo,Comments,CreatePerson,CreateTime,Validate) ";
                strInsertOrder += " values ('" + fileUp.StrRID + "','" + fileUp.StrType + "','" + fileUp.StrFileName + "',@fileByte,'" + fileUp.StrComments + "','" + fileUp.StrCreatePerson + "','" + fileUp.StrCreateTime + "','" + fileUp.StrValidate + "')";
            }
            else
            {

                strInsertOrder = "insert into tk_FileUpload (RID,[Type],FileName,Comments,CreatePerson,CreateTime,Validate) ";
                strInsertOrder += " values ('" + fileUp.StrRID + "','" + fileUp.StrType + "','" + fileUp.StrFileName + "'"
                + "'" + fileUp.StrComments + "','" + fileUp.StrCreatePerson + "','" + fileUp.StrCreateTime + "','" + fileUp.StrValidate + "')";
            }
            OperateLog log = new OperateLog();
            log.strMarkID = fileUp.StrRID;
            log.strLogTitle = "上传附件";
            log.strLogContent = "上传附件成功";
            log.strLogPerson = account.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "上传附件";
            try
            {
                sqlTrans.Open("FlowMeterDBCnn");
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, para);
                intlog = OperateLog(log, ref strErr);
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

        // 主页-获取附件列表 
        public static UIDataTable LoadFileList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetFileList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 主页-下载附件 
        public static DataTable GetNewDownloadFile(string id)
        {
            string strSql = "select [FileInfo],FileName from tk_FileUpload where ID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }




        // 缴费单查询-界面加载缴费记录
        public static UIDataTable LoadPayList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetPayList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 缴费单查询-获取设备信息
        public static UIDataTable LoadModelList(int a_intPageSize, int a_intPageIndex, string where)
        {
            // 先获取报价 QID 有可能是多个,隔开
            string strsql = " select distinct QID from tk_Payment where Validate='v' " + where;
            DataTable dtQ = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
            string str = "";
            if (dtQ != null && dtQ.Rows.Count > 0)
            {
                string QIDs = dtQ.Rows[0]["QID"].ToString();
                string[] strs = QIDs.Split(',');
                for (int j = 0; j < strs.Length; j++)
                {
                    str += "'" + strs[j].ToString() + "',";
                }
                str = str.Substring(0, str.Length - 1);
            }
            string strWhere = "";
            if (str != "")
                strWhere = "(" + str + ")";
            else
                strWhere = "('')";
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetModelList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 缴费单查询-获取相关联的报价单
        public static UIDataTable LoadBJDList(int a_intPageSize, int a_intPageIndex, string where)
        {
            // 先获取报价 QID 有可能是多个,隔开
            string strsql = " select distinct QID from tk_Payment where Validate='v' " + where;
            DataTable dtQ = SQLBase.FillTable(strsql, "FlowMeterDBCnn");
            string str = "";
            if (dtQ != null && dtQ.Rows.Count > 0)
            {
                string QIDs = dtQ.Rows[0]["QID"].ToString();
                string[] strs = QIDs.Split(',');
                for (int j = 0; j < strs.Length; j++)
                {
                    str += "'" + strs[j].ToString() + "',";
                }
                str = str.Substring(0, str.Length - 1);
            }
            string strWhere = "";
            if (str != "")
                strWhere = "(" + str + ")";
            else
                strWhere = "('')";
            //
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetBJDList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 缴费单管理-获取新的缴费单号 
        public static string GetNewPayID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");

            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='PM'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('PM',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string str = "select RID, RidNo,DateRecord from tk_RIDno where DateRecord='" + strYMD + "' and RID='PM'";
            dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 缴费单管理-插入新的缴费单号 
        public static string GetPayID()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strStockID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where DateRecord='" + strYMD + "' and RID='PM'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strStockID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('PM',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where DateRecord ='" + strYMD + "' and RID='PM' ";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strStockID = dtRMaxID.Rows[0]["RID"].ToString() + DateTime.Now.ToString("yyMMdd") + GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strStockID;
        }

        // 新增缴费记录-加载为缴费完成状态的报价单列表 
        public static UIDataTable LoadBJDList1(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("GetBJDList1", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 新增缴费记录-确定新增缴费记录 
        public static int AddNewPay(tk_Payment payment, string Checks, decimal LowPrice, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsert = 0;
            int intUpdate = 0;
            int insertlog = 0;
            a_strErr = "";

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string strInsert = " insert into tk_Payment(PayID,QID,PayUnit,PayPerson,PayMount,PayDate,Comments,Validate,CreateTime,CreateUser)";
            strInsert += " values('" + payment.StrPayID + "','" + payment.StrQID + "','" + payment.StrPayUnit + "','" + payment.StrPayPerson + "','" +
            payment.StrPayMount + "','" + payment.StrPayDate + "','" + payment.StrComments + "','v','" + payment.StrCreateTime + "','" +
            payment.StrCreateUser + "' )";

            string strUpdate = "";
            string strInsertHis = "";

            // 修改报价单状态 
            string[] strIDs = Checks.Split(',');
            for (int i = 0; i < strIDs.Length; i++)
            {
                if (LowPrice != 0)// 有欠款  状态改为1
                {
                    strUpdate += " update tk_GenQtn set LowPrice='" + LowPrice + "', State='1' ";
                    strUpdate += " where QID='" + strIDs[i] + "' ";
                }
                else if (LowPrice == 0)// 无欠款  状态改为2
                {
                    strUpdate += " update tk_GenQtn set LowPrice='0.00', State='2' where QID='" + strIDs[i] + "' ";
                }
                // 插入历史表中
                strInsertHis += " insert into tk_GenQtnHis ";
                strInsertHis += " select RID,QID,TotalPriceU,TotalPriceC,State,LowPrice,Comments,'"
                + DateTime.Now + "','" + acc.UserName + "'";
                strInsertHis += " from tk_GenQtn where QID='" + strIDs[i] + "' ";
            }
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (intInsert > 0)
                {
                    // 将新增记录插入日志表中
                    OperateLog log = new OperateLog();
                    log.strMarkID = payment.StrPayID;
                    log.strLogTitle = "新增缴费记录";
                    log.strLogContent = "新增缴费记录成功,同时修改报价单状态和欠款金额";
                    log.strLogPerson = acc.UserName;
                    log.strLogTime = DateTime.Now;
                    log.strType = "缴费记录单";
                    insertlog = OperateLog(log, ref a_strErr);
                    //
                    if (strInsertHis != "")
                    {
                        intInsert += sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                        intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                        for (int j = 0; j < strIDs.Length; j++)
                        {
                            log.strMarkID = strIDs[j];
                            log.strLogTitle = "修改报价单";
                            log.strLogContent = "修改报价单状态和欠款金额成功";
                            log.strLogPerson = acc.UserName;
                            log.strLogTime = DateTime.Now;
                            log.strType = "报价单";
                            insertlog += OperateLog(log, ref a_strErr);
                        }
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsert + intUpdate + insertlog;

        }

        // 查看缴费单详细-获取详细信息 
        public static tk_Payment getNewPayMent(string strPayID)
        {
            tk_Payment payMent = new tk_Payment();
            string strSql = " select * from tk_Payment where PayID = '" + strPayID + "' and Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                payMent.StrPayID = strPayID;
                payMent.StrQID = dt.Rows[0]["QID"].ToString();
                payMent.StrPayUnit = dt.Rows[0]["PayUnit"].ToString();
                payMent.StrPayPerson = dt.Rows[0]["PayPerson"].ToString();
                payMent.StrPayMount = Convert.ToDecimal(dt.Rows[0]["PayMount"].ToString());
                payMent.StrPayDate = dt.Rows[0]["PayDate"].ToString();
                payMent.StrComments = dt.Rows[0]["Comments"].ToString();
                payMent.StrValidate = dt.Rows[0]["Validate"].ToString();
                payMent.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                payMent.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();
            }
            return payMent;
        }

        // 修改缴费单-加载相关报价单列表 
        public static UIDataTable LoadBJDList2(int a_intPageSize, int a_intPageIndex, string strQIDs)
        {
            string strWhere = "";
            for (int i = 0; i < strQIDs.Split(',').Length; i++)
            {
                strWhere += "'" + strQIDs.Split(',')[i].ToString() + "',";
            }
            strWhere = "(" + strWhere.Substring(0, strWhere.Length - 1) + ")";
            //
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",strWhere)
                };
            DataSet DO_Order = SQLBase.FillDataSet("GetBJDList2", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 修改缴费单-获取应缴金额和欠费金额
        public static string getBJInfo(string strPayID)
        {
            string strInfo = "";
            // 1.获取报价单号
            string strSel = " select QID,PayMount from tk_Payment where Validate='v' and PayID='" + strPayID + "' ";
            DataTable dtSel = SQLBase.FillTable(strSel, "FlowMeterDBCnn");
            string strQIDs = dtSel.Rows[0][0].ToString();
            string[] strList = strQIDs.Split(',');
            // 2.获取所需金额
            if (strList.Length > 1)// 批量缴费没有欠费情况 缴费金额#0（欠款金额）
            {
                strInfo = Convert.ToDecimal(dtSel.Rows[0]["PayMount"]) + "#";
                strInfo += "0.00";
            }
            else// 单个缴费 
            {
                string strSql = " select * from tk_GenQtn where QID='" + strQIDs + "' ";
                DataTable dtS = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                if (dtS != null && dtS.Rows.Count > 0)
                {
                    strInfo = Convert.ToDecimal(dtS.Rows[0]["TotalPriceC"]) + "#";
                    strInfo += Convert.ToDecimal(dtS.Rows[0]["LowPrice"]).ToString();
                }
            }
            return strInfo;
        }

        // 修改缴费单-确认修改
        public static int UpdatePayment(tk_Payment payment, decimal LowPrice, ref string a_strErr)
        {
            a_strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertHis = "";
            string strInsertHis2 = "";
            string strUpdate = "";

            // 1.1将原缴费单信息存入His表中 
            string strInsertHis1 = " insert into tk_PaymentHis ";
            strInsertHis1 += " select ID,PayID,QID,PayUnit,PayPerson,PayMount,PayDate,Comments,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strInsertHis1 += " from tk_Payment where PayID='" + payment.StrPayID + "' and Validate='v' ";

            // 1.2将原报价单详细信息存入His表中[限于单项报价单时]
            if (payment.StrQID.Split(',').Length == 1)
            {
                strInsertHis2 = " insert into tk_GenQtnHis ";
                strInsertHis2 += " select RID,QID,TotalPriceU,TotalPriceC,State,LowPrice,Comments,'"
                    + DateTime.Now + "','" + acc.UserName + "' ";
                strInsertHis2 += " from tk_GenQtn where QID='" + payment.StrQID + "' ";
            }
            strInsertHis = strInsertHis1 + strInsertHis2;

            // 修改缴费单信息 
            strUpdate = GSqlSentence.GetUpdateInfoByD<tk_Payment>(payment, "PayID", "tk_Payment");
            // 单项报价单
            if (payment.StrQID.Split(',').Length == 1)
            {
                if (LowPrice == 0)// 无欠款 
                {
                    strUpdate += " update tk_GenQtn set LowPrice='" + LowPrice + "',State='2'";
                    strUpdate += " where QID='" + payment.StrQID + "' ";
                }
                else
                {
                    strUpdate += " update tk_GenQtn set LowPrice='" + LowPrice + "'";
                    strUpdate += " where QID='" + payment.StrQID + "' ";
                }
            }
            // 将缴费单修改操作记录在log表中 
            OperateLog log = new OperateLog();
            log.strMarkID = payment.StrPayID;
            log.strLogTitle = "修改缴费单";
            log.strLogContent = "修改缴费单成功,同时将旧数据存入历史表中";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "缴费单";

            try
            {
                if (strInsertHis != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                    if (payment.StrQID.Split(',').Length == 1)
                    {
                        // 将报价单修改操作记录在log表中
                        log.strMarkID = payment.StrQID;
                        log.strLogTitle = "修改报价单";
                        log.strLogContent = "修改报价单成功,同时将旧数据存入历史表中";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "报价单";

                        insertlog += OperateLog(log, ref a_strErr);
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intInsert + intUpdate + insertlog;
        }

        // 缴费管理-确定撤销 
        public static int RePayment(string strPayID, string strQIDs, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            int intInsert = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string[] strList = strQIDs.Split(',');// 报价单编号 
            string strSqlHis = "";
            string strUpdate = "";
            a_strErr = "";

            // 1.1 将缴费单原信息存入His表中 
            strSqlHis += " insert into tk_PaymentHis ";
            strSqlHis += " select ID,PayID,QID,PayUnit,PayPerson,PayMount,PayDate,Comments,Validate,CreateTime,CreateUser,'"
                + DateTime.Now + "','" + acc.UserName + "'";
            strSqlHis += " from tk_Payment where PayID ='" + strPayID + "' and Validate='v' ";

            // 2.1 修改缴费单信息 将Validate置为i
            strUpdate += " update tk_Payment set Validate='i' where PayID='" + strPayID + "' and Validate='v' ";

            // 1.2 将报价单原信息存入His表中 
            for (int i = 0; i < strList.Length; i++)
            {
                strSqlHis += " insert into tk_GenQtnHis ";
                strSqlHis += " select RID,QID,TotalPriceU,TotalPriceC,State,LowPrice,Comments,'"
                    + DateTime.Now + "','" + acc.UserName + "'";
                strSqlHis += " from tk_GenQtn where QID='" + strList[i] + "' ";

                // 2.2在现表中 将状态修改为0-未缴费 LowPrice置为空
                strUpdate += " update tk_GenQtn set State='0',LowPrice='0.00' where QID='" + strList[i] + "' ";
            }
            try
            {
                if (strSqlHis != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strSqlHis, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);

                    // 3.将操作记录在log表中 
                    if (intUpdate > 0)
                    {
                        OperateLog log = new OperateLog();
                        log.strMarkID = strPayID;
                        log.strLogTitle = "撤销缴费记录单";
                        log.strLogContent = "撤销缴费记录单成功,同时将旧数据存入历史表中";
                        log.strLogPerson = acc.UserName;
                        log.strLogTime = DateTime.Now;
                        log.strType = "缴费单";
                        insertlog = OperateLog(log, ref a_strErr);
                        //
                        for (int j = 0; j < strList.Length; j++)
                        {
                            log.strMarkID = strList[j];
                            log.strLogTitle = "修改报价单";
                            log.strLogContent = "修改报价单成功,同时将旧数据存入历史表中";
                            log.strLogPerson = acc.UserName;
                            log.strLogTime = DateTime.Now;
                            log.strType = "报价单";

                            insertlog += OperateLog(log, ref a_strErr);
                        }
                    }
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdate + intInsert + insertlog;
        }






        // 插入日志记录 
        public static int OperateLog(OperateLog Log, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string strInsert = GSqlSentence.GetInsertInfoByD<OperateLog>(Log, "tk_OperateLog");
            try
            {
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
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

        // 配置-获取规格型号 
        public static DataTable getConfigContent(string strType)
        {
            string strSql = "select SID,Text from tk_ConfigContent where validate='v' and Type = '" + strType + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 配置
        public static DataTable getConfigContentNew(string strType)
        {
            string strSql = "select SID,Text from tk_ConfigContent where validate='v' and Type = '" + strType + "' and SID!='CardType2'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 配置
        public static DataTable getConfigContentNew2(string strType)
        {
            string strSql = "select SID,Text from tk_ConfigContent where validate='v' and Type = '" + strType + "' and SID='CardType2'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 配置-获取状态 
        public static DataTable getConfigState(string strType)
        {
            string strSql = "select StateId as SID,name as Text from tk_ConfigState where validate='v' and Type = '" + strType + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 配置-获取第三方检定单位
        public static DataTable getUnitList()
        {
            string strSql = "select UnitID,UnitName from tk_ConfigUnit where Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 配置-获取上传环节 
        public static DataTable getUpType()
        {
            string strSql = "select StateId as SID,name as Text from tk_ConfigState where validate='v' and Type='RepairState' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 获取客户名称
        public static DataTable GetCustomerName()
        {
            string strSql = "select distinct CustomerName as SID,CustomerName as Text from tk_RepairCard where Validate='v' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            return dt;
        }

        // 获取仪表类型
        public static string getType(string strType)
        {
            string strSql = "select distinct Text from tk_ConfigContent where Validate='v' and Type='CardType' and SID='" + strType + "' ";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }






        #region //检测
        #region 腰轮
        //添加检测
        public static int InsertCheckData(tk_CheckData data, ref string a_strErr)
        {
            string selectsql = "select Count(*) from tk_CheckData where RepairType='维修检测' and RID='" + data.StrRID + "'";
            DataTable dt = SQLBase.FillTable(selectsql, "FlowMeterDBCnn");
            var num = dt.Rows[0][0].ToString();

            int intInsertBas = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertBas = "insert into tk_CheckData (RID,RepairID,RepairType,RepairMethod,MeterID,CheckUser,CheckDate,Qmin,[0.1Qmax],[0.2Qmax],[0.25Qmax],[0.4Qmax],"
            + "[0.7Qmax],Qmax,Avg_Qmin,[Avg_0.1Qmax],[Avg_0.2Qmax],[Avg_0.25Qmax],[Avg_0.4Qmax],[Avg_0.7Qmax],[Avg_Qmax],Repeat_Qmin ,[Repeat_0.1Qmax],[Repeat_0.2Qmax],"
            + "[Repeat_0.25Qmax],[Repeat_0.4Qmax],[Repeat_0.7Qmax],Repeat_Qmax,Ratio,q1,q2,PDeviation,Oratio,Remark,CreateTime,CreateUser,validate,AvgRepeat  )"
            + " values ('" + data.StrRID + "','" + data.StrRepairID + "','" + data.StrRepairType + "','" + data.StrRepairMethod + "','" + data.StrMeterID
            + "','" + data.StrCheckUser + "','" + data.StrCheckDate + "','" + data.StrQmin + "','" + data.StrQmax1 + "','" + data.StrQmax2 + "','" + data.StrQmax25
            + "','" + data.StrQmax4 + "','" + data.StrQmax7 + "','" + data.StrQmax + "','" + data.StrAvg_Qmin + "','" + data.StrAvg_Qmax1 + "','" + data.StrAvg_Qmax2
            + "','" + data.StrAvg_Qmax25 + "','" + data.StrAvg_Qmax4 + "','" + data.StrAvg_Qmax7 + "','" + data.StrAvg_Qmax + "','" + data.StrRepeat_Qmin
            + "','" + data.StrRepeat_Qmax1 + "','" + data.StrRepeat_Qmax2 + "','" + data.StrRepeat_Qmax25 + "','" + data.StrRepeat_Qmax4 + "','" + data.StrRepeat_Qmax7
            + "','" + data.StrRepeat_Qmax + "','" + data.StrRatio + "','" + data.Strq1 + "','" + data.Strq2 + "','" + data.StrPDeviation + "','" + data.StrOratio
            + "','" + data.StrRemark + "','" + data.StrCreateTime + "','" + data.StrCreateUser + "','" + data.Strvalidate + "','" + data.StrAvgRepeat + "')";

            a_strErr = "";
            OperateLog Log = new OperateLog();
            Acc_Account account = GAccount.GetAccountInfo();
            Log.strMarkID = data.StrRepairID;
            Log.strLogTitle = "新建" + data.StrRepairType;
            Log.strLogContent = "新建" + data.StrRepairType + "成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = data.StrRepairType;

            tk_ProRecord pro = new tk_ProRecord();
            pro.StrRID = data.StrRID;
            pro.StrOpType = data.StrRepairType;
            pro.StrOpTime = data.StrCheckDate;
            pro.StrOpUser = data.StrCheckUser;
            pro.StrCreateTime = DateTime.Now;
            pro.StrCreateUser = account.UserName;


            try
            {
                if (strInsertBas != "")
                {
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                    if (data.StrRepairType == "进厂检测")
                    {
                        ChangeState(pro);

                    }
                    else
                    {
                        if (data.Strvalidate == "是")
                        {
                            string updatesql = "update tk_RepairCard set State=7 where RID='" + pro.StrRID + "'";
                            sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);

                            if (data.StrRepairType == "清洗检测")
                            {
                                string sql = "update tk_TransCard set FirstCheck='合格' where RID='" + data.StrRID + "'";
                                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                            }
                            if (data.StrRepairType == "维修检测")
                            {

                                string sql = "";
                                switch (num)
                                {
                                    case "0":
                                        sql = "update tk_TransCard set LastCheck='合格' where RID='" + data.StrRID + "'";

                                        break;
                                    case "1":
                                        sql = "update tk_TransCard set TwoCheck='合格' where RID='" + data.StrRID + "'";

                                        break;
                                    case "2":
                                        sql = "update tk_TransCard set ThreeCheck='合格' where RID='" + data.StrRID + "'";

                                        break;

                                }
                                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);

                            }
                        }
                        else
                        {
                            if (data.StrRepairType == "清洗检测")
                            {
                                string sql = "update tk_TransCard set FirstCheck='不合格' where RID='" + data.StrRID + "'";
                                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                            }
                            if (data.StrRepairType == "维修检测")
                            {

                                string sql = "";
                                switch (num)
                                {
                                    case "0":
                                        sql = "update tk_TransCard set LastCheck='不合格' where RID='" + data.StrRID + "'";

                                        break;
                                    case "1":
                                        sql = "update tk_TransCard set TwoCheck='不合格' where RID='" + data.StrRID + "'";

                                        break;
                                    case "2":
                                        sql = "update tk_TransCard set ThreeCheck='不合格' where RID='" + data.StrRID + "'";

                                        break;

                                }
                                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);

                            }
                        }

                    }

                    insertlog = OperateLog(Log, ref a_strErr);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        //查询加载检测列表 
        public static UIDataTable LoadCheckDataList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetCheckDataList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 获取指定id对应的检测表详细信息
        public static tk_CheckData getNewCheckData(string strID)
        {

            tk_CheckData data = new tk_CheckData();
            string strSql = "select * from tk_CheckData where RepairID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRID = dt.Rows[0]["RID"].ToString();
                data.StrRepairID = strID;
                data.StrRepairType = dt.Rows[0]["RepairType"].ToString();
                data.StrRepairMethod = dt.Rows[0]["RepairMethod"].ToString();
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrCheckUser = dt.Rows[0]["CheckUser"].ToString();
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrCheckDate = Convert.ToDateTime(dt.Rows[0]["CheckDate"]);
                data.StrQmin = dt.Rows[0]["Qmin"].ToString();
                data.StrQmax1 = dt.Rows[0]["0.1Qmax"].ToString();
                data.StrQmax2 = dt.Rows[0]["0.2Qmax"].ToString();
                data.StrQmax25 = dt.Rows[0]["0.25Qmax"].ToString();
                data.StrQmax4 = dt.Rows[0]["0.4Qmax"].ToString();
                data.StrQmax7 = dt.Rows[0]["0.7Qmax"].ToString();


                data.StrQmax = dt.Rows[0]["Qmax"].ToString();
                data.StrAvg_Qmin = dt.Rows[0]["Avg_Qmin"].ToString();
                data.StrAvg_Qmax1 = dt.Rows[0]["Avg_0.1Qmax"].ToString();
                data.StrAvg_Qmax2 = dt.Rows[0]["Avg_0.2Qmax"].ToString();
                data.StrAvg_Qmax25 = dt.Rows[0]["Avg_0.25Qmax"].ToString();
                data.StrAvg_Qmax4 = dt.Rows[0]["Avg_0.4Qmax"].ToString();
                data.StrAvg_Qmax7 = dt.Rows[0]["Avg_0.7Qmax"].ToString();
                data.StrAvg_Qmax = dt.Rows[0]["Avg_Qmax"].ToString();
                data.StrRepeat_Qmin = dt.Rows[0]["Repeat_Qmin"].ToString();
                data.StrRepeat_Qmax1 = dt.Rows[0]["Repeat_0.1Qmax"].ToString();
                data.StrRepeat_Qmax2 = dt.Rows[0]["Repeat_0.2Qmax"].ToString();
                data.StrRepeat_Qmax25 = dt.Rows[0]["Repeat_0.25Qmax"].ToString();
                data.StrRepeat_Qmax4 = dt.Rows[0]["Repeat_0.4Qmax"].ToString();
                data.StrRepeat_Qmax7 = dt.Rows[0]["Repeat_0.7Qmax"].ToString();
                data.StrRepeat_Qmax = dt.Rows[0]["Repeat_Qmax"].ToString();
                data.StrRatio = dt.Rows[0]["Ratio"].ToString();
                data.Strq1 = dt.Rows[0]["q1"].ToString();
                data.Strq2 = dt.Rows[0]["q2"].ToString();
                data.StrPDeviation = dt.Rows[0]["PDeviation"].ToString();
                data.StrOratio = dt.Rows[0]["Oratio"].ToString();
                data.StrRemark = dt.Rows[0]["Remark"].ToString();
                data.Strvalidate = dt.Rows[0]["validate"].ToString();
                data.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();

                data.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                data.StrAvgRepeat = dt.Rows[0]["AvgRepeat"].ToString();

            }

            return data;

        }

        public static bool CheckData(string RID)
        {

            tk_CheckData data = new tk_CheckData();
            string strSql = "select * from tk_CheckData where RID = '" + RID + "' and RepairType='清洗检测'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            string strSql2 = "select * from tk_CheckData2 where RID = '" + RID + "' and RepairType='清洗检测'";
            DataTable dt2 = SQLBase.FillTable(strSql2, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0 || dt2.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //修改检测表
        public static int UpdateCheckData(tk_CheckData data, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "update tk_CheckData set RID='" + data.StrRID + "',RepairID='" + data.StrRepairID + "',RepairType='" + data.StrRepairType
                + "',RepairMethod='" + data.StrRepairMethod + "',MeterID='" + data.StrMeterID + "',CheckUser='" + data.StrCheckUser + "',CheckDate='" + data.StrCheckDate
                + "',Qmin='" + data.StrQmin + "',[0.1Qmax]='" + data.StrQmax1 + "',[0.2Qmax]='" + data.StrQmax2 + "',[0.25Qmax]='" + data.StrQmax25
                + "',[0.4Qmax]='" + data.StrQmax4 + "',[0.7Qmax]='" + data.StrQmax7 + "',Qmax='" + data.StrQmax + "',Avg_Qmin='" + data.StrAvg_Qmin
                + "',[Avg_0.1Qmax]='" + data.StrAvg_Qmax1 + "',[Avg_0.2Qmax]='" + data.StrAvg_Qmax2 + "',[Avg_0.25Qmax]='" + data.StrAvg_Qmax25
                + "',[Avg_0.4Qmax]='" + data.StrAvg_Qmax4 + "',[Avg_0.7Qmax]='" + data.StrAvg_Qmax7 + "',Avg_Qmax='" + data.StrAvg_Qmax
                + "',Repeat_Qmin='" + data.StrRepeat_Qmin + "',[Repeat_0.1Qmax]='" + data.StrRepeat_Qmax1 + "',[Repeat_0.2Qmax]='" + data.StrRepeat_Qmax2
                + "',[Repeat_0.25Qmax]='" + data.StrRepeat_Qmax25 + "',[Repeat_0.4Qmax]='" + data.StrRepeat_Qmax4 + "',[Repeat_0.7Qmax]='" + data.StrRepeat_Qmax7
                + "',Repeat_Qmax='" + data.StrRepeat_Qmax + "',Ratio='" + data.StrRatio + "',q1='" + data.Strq1 + "',q2='" + data.Strq2
                + "',PDeviation ='" + data.StrPDeviation + "',Oratio='" + data.StrOratio + "',Remark='" + data.StrRemark
                + "',validate='" + data.Strvalidate + "',CreateTime='" + data.StrCreateTime + "',CreateUser='" + data.StrCreateUser + "',AvgRepeat='" + data.StrAvgRepeat
                + "' where RepairID = '" + data.StrRepairID + "'";



            string insertsql = "  insert into tk_CheckDataHis ";
            insertsql += "SELECT [RID],[RepairID],[RepairType],[RepairMethod],[MeterID],[CheckUser],[CheckDate],[Qmin] ,[0.1Qmax],[0.2Qmax],[0.25Qmax],[0.4Qmax]";
            insertsql += ",[0.7Qmax],[Qmax],[Avg_Qmin],[Avg_0.1Qmax],[Avg_0.2Qmax],[Avg_0.25Qmax],[Avg_0.4Qmax],[Avg_0.7Qmax],[Avg_Qmax],[Repeat_Qmin],[Repeat_0.1Qmax]";
            insertsql += ",[Repeat_0.2Qmax],[Repeat_0.25Qmax],[Repeat_0.4Qmax] ,[Repeat_0.7Qmax],[AvgRepeat],[Repeat_Qmax],[Ratio],[q1],[q2],[PDeviation],[Oratio],[Remark],";
            insertsql += "[CreateTime],[CreateUser] ,[validate],'" + DateTime.Now + "','" + acc.UserName + "'" + "FROM [BGOI_FlowMeter].[dbo].[tk_CheckData] ";
            insertsql += " where RepairID=" + "'" + data.StrRepairID + "'";

            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = data.StrRepairID;
            log.strLogTitle = "修改" + data.StrRepairType;
            log.strLogContent = "修改" + data.StrRepairType + "成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "修改" + data.StrRepairType;

            string updatepr = "update tk_ProRecord set OpTime='" + data.StrCheckDate + "' where RID='" + data.StrRID + "' and OpType='" + data.StrRepairType + "'";

            try
            {
                int num = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (num > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatepr, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }
        //撤销检测表
        public static int DeleteCheckData(string id, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intDelete = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            var data = getNewCheckData(id);
            string sql = "delete from tk_CheckData where RepairID='" + id + "'";

            string insertsql = "  insert into tk_CheckDataHis ";
            insertsql += "SELECT [RID],[RepairID],[RepairType],[RepairMethod],[MeterID],[CheckUser],[CheckDate],[Qmin] ,[0.1Qmax],[0.2Qmax],[0.25Qmax],[0.4Qmax]";
            insertsql += ",[0.7Qmax],[Qmax],[Avg_Qmin],[Avg_0.1Qmax],[Avg_0.2Qmax],[Avg_0.25Qmax],[Avg_0.4Qmax],[Avg_0.7Qmax],[Avg_Qmax],[Repeat_Qmin],[Repeat_0.1Qmax]";
            insertsql += ",[Repeat_0.2Qmax],[Repeat_0.25Qmax],[Repeat_0.4Qmax] ,[Repeat_0.7Qmax],[AvgRepeat],[Repeat_Qmax],[Ratio],[q1],[q2],[PDeviation],[Oratio],[Remark],";
            insertsql += "[CreateTime],[CreateUser] ,[validate],'" + DateTime.Now + "','" + acc.UserName + "'" + "FROM [BGOI_FlowMeter].[dbo].[tk_CheckData] ";
            insertsql += " where RepairID=" + "'" + data.StrRepairID + "'";

            a_strErr = "";

            OperateLog log = new OperateLog();
            log.strMarkID = id;
            log.strLogTitle = "撤销" + data.StrRepairType;
            log.strLogContent = "撤销" + data.StrRepairType + "成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "撤销" + data.StrRepairType;


            string updatesql = "update tk_RepairCard set [State]=[State]-1 where RID='" + data.StrRID + "'";
            string deletesql = "delete tk_ProRecord  where RID='" + data.StrRID + "' and OpType='" + data.StrRepairType + "'";
            try
            {
                int intInsert = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intDelete = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    if (data.StrRepairType == "进厂检测")
                        sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intDelete;

        }
        #endregion
        #region 超声
        //添加检测
        public static int InsertCheckData2(tk_CheckData2 data, ref string a_strErr)
        {
            int intInsertBas = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertBas = "insert into tk_CheckData2 (RID,RepairID,RepairType,RepairMethod,MeterID,CheckUser,CheckDate,[A1path] ,[A2path],[A3path],[A4path] ,"
            + "[A5path],[A6path] ,[ATemperature],[APressure],[AAverage],[AAheory],[B1path],[B2path],[B3path],[B4path],[B5path],[B6path],[BTemperature] ,[BPressure],"
            + "[BAverage],[BAheory],CreateTime,CreateUser,validate)"

            + " values ('" + data.StrRID + "','" + data.StrRepairID + "','" + data.StrRepairType + "','" + data.StrRepairMethod + "','" + data.StrMeterID
            + "','" + data.StrCheckUser + "','" + data.StrCheckDate + "','" + data.StrA1path + "','" + data.StrA2path + "','" + data.StrA3path + "','" + data.StrA4path
            + "','" + data.StrA5path + "','" + data.StrA6path + "','" + data.StrATemperature + "','" + data.StrAPressure + "','" + data.StrAAverage + "','" + data.StrAAheory
            + "','" + data.StrB1path + "','" + data.StrB2path + "','" + data.StrB3path + "','" + data.StrB4path + "','" + data.StrB5path
            + "','" + data.StrB6path + "','" + data.StrBTemperature + "','" + data.StrBPressure + "','" + data.StrBAverage + "','" + data.StrBAheory
            + "','" + data.StrCreateTime + "','" + data.StrCreateUser + "','" + data.Strvalidate + "')";


            a_strErr = "";
            OperateLog Log = new OperateLog();
            Acc_Account account = GAccount.GetAccountInfo();
            Log.strMarkID = data.StrRepairID;
            Log.strLogTitle = "新建" + data.StrRepairType;
            Log.strLogContent = "新建" + data.StrRepairType + "成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = data.StrRepairType;

            tk_ProRecord pro = new tk_ProRecord();
            pro.StrRID = data.StrRID;
            pro.StrOpType = data.StrRepairType;
            pro.StrOpTime = data.StrCheckDate;
            pro.StrOpUser = data.StrCheckUser;
            pro.StrCreateTime = DateTime.Now;
            pro.StrCreateUser = account.UserName;
            try
            {
                if (strInsertBas != "")
                {
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                    if (data.StrRepairType == "进厂检测")
                    {
                        ChangeState(pro);

                    }
                    else
                    {
                        if (data.Strvalidate == "是")
                        {
                            string updatesql = "update tk_UTRepairCard set State=7 where RID='" + pro.StrRID + "'";
                            sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                        }
                    }

                    insertlog = OperateLog(Log, ref a_strErr);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }
        //查询加载检测列表 
        public static UIDataTable LoadCheckDataList2(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetCheckDataList2", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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
        // 获取指定id对应的检测表详细信息
        public static tk_CheckData2 getNewCheckData2(string strID)
        {

            tk_CheckData2 data = new tk_CheckData2();
            string strSql = "select * from tk_CheckData2 where RepairID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRID = dt.Rows[0]["RID"].ToString();
                data.StrRepairID = strID;
                data.StrRepairType = dt.Rows[0]["RepairType"].ToString();
                data.StrRepairMethod = dt.Rows[0]["RepairMethod"].ToString();
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrCheckUser = dt.Rows[0]["CheckUser"].ToString();
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrCheckDate = Convert.ToDateTime(dt.Rows[0]["CheckDate"]);
                data.StrA1path = dt.Rows[0]["A1path"].ToString();
                data.StrA2path = dt.Rows[0]["A2path"].ToString();
                data.StrA3path = dt.Rows[0]["A3path"].ToString();
                data.StrA4path = dt.Rows[0]["A4path"].ToString();
                data.StrA5path = dt.Rows[0]["A5path"].ToString();
                data.StrA6path = dt.Rows[0]["A6path"].ToString();


                data.StrATemperature = dt.Rows[0]["ATemperature"].ToString();
                data.StrAPressure = dt.Rows[0]["APressure"].ToString();
                data.StrAAverage = dt.Rows[0]["AAverage"].ToString();
                data.StrAAheory = dt.Rows[0]["AAheory"].ToString();

                data.StrB1path = dt.Rows[0]["B1path"].ToString();
                data.StrB2path = dt.Rows[0]["B2path"].ToString();
                data.StrB3path = dt.Rows[0]["B3path"].ToString();
                data.StrB4path = dt.Rows[0]["B4path"].ToString();
                data.StrB5path = dt.Rows[0]["B5path"].ToString();
                data.StrB6path = dt.Rows[0]["B6path"].ToString();


                data.StrBTemperature = dt.Rows[0]["BTemperature"].ToString();
                data.StrBPressure = dt.Rows[0]["BPressure"].ToString();
                data.StrBAverage = dt.Rows[0]["BAverage"].ToString();
                data.StrBAheory = dt.Rows[0]["BAheory"].ToString();
                data.Strvalidate = dt.Rows[0]["validate"].ToString();
                data.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();

                data.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());


            }

            return data;

        }

        //修改检测表
        public static int UpdateCheckData2(tk_CheckData2 data, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "update tk_CheckData2 set RID='" + data.StrRID + "',RepairID='" + data.StrRepairID + "',RepairType='" + data.StrRepairType
                + "',RepairMethod='" + data.StrRepairMethod + "',MeterID='" + data.StrMeterID + "',CheckUser='" + data.StrCheckUser + "',CheckDate='" + data.StrCheckDate
                + "',A1path='" + data.StrA1path + "',A2path='" + data.StrA2path + "',A3path='" + data.StrA3path + "',A4path='" + data.StrA3path
                + "',A5path='" + data.StrA5path + "',A6path='" + data.StrA6path + "',ATemperature='" + data.StrATemperature + "',APressure='" + data.StrAPressure
                + "',AAverage='" + data.StrAAverage + "',AAheory='" + data.StrAAheory + "',B1path='" + data.StrB1path
                + "',B2path='" + data.StrB2path + "',B3path='" + data.StrB3path + "',B4path='" + data.StrB4path
                + "',B5path='" + data.StrB5path + "',B6path='" + data.StrB6path + "',BTemperature='" + data.StrBTemperature
                + "',BPressure='" + data.StrBPressure + "',BAverage='" + data.StrBAverage + "',BAheory='" + data.StrBAheory

                + "',validate='" + data.Strvalidate + "',CreateTime='" + data.StrCreateTime + "',CreateUser='" + data.StrCreateUser
                + "' where RepairID = '" + data.StrRepairID + "'";



            string insertsql = "  insert into tk_CheckData2His ";
            insertsql += "SELECT [RID],[RepairID],[RepairType],[RepairMethod],[MeterID],[CheckUser],[CheckDate],[A1path] ,[A2path],[A3path],[A4path],[A5path]";
            insertsql += ",[A6path],[ATemperature],[APressure],[AAverage],[AAheory],[B1path],[B2path],[B3path],[B4path],[B5path],[B6path]";
            insertsql += ",[BTemperature],[BPressure],[BAverage] ,[BAheory],";
            insertsql += "[CreateTime],[CreateUser] ,[validate],Remark,'" + DateTime.Now + "','" + acc.UserName + "'" + "FROM [BGOI_FlowMeter].[dbo].[tk_CheckData2] ";
            insertsql += " where RepairID=" + "'" + data.StrRepairID + "'";

            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = data.StrRepairID;
            log.strLogTitle = "修改" + data.StrRepairType;
            log.strLogContent = "修改" + data.StrRepairType + "成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "修改" + data.StrRepairType;

            string updatepr = "update tk_ProRecord set OpTime='" + data.StrCheckDate + "' where RID='" + data.StrRID + "' and OpType='" + data.StrRepairType + "'";

            try
            {
                int num = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (num > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatepr, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }

        //撤销检测表
        public static int DeleteCheckData2(string id, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intDelete = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            var data = getNewCheckData2(id);
            string sql = "delete from tk_CheckData2 where RepairID='" + id + "'";

            string insertsql = "  insert into tk_CheckData2His ";
            insertsql += "SELECT [RID],[RepairID],[RepairType],[RepairMethod],[MeterID],[CheckUser],[CheckDate],[A1path] ,[A2path],[A3path],[A4path],[A5path]";
            insertsql += ",[A6path],[ATemperature],[APressure],[AAverage],[AAheory],[B1path],[B2path],[B3path],[B4path],[B5path],[B6path]";
            insertsql += ",[BTemperature],[BPressure],[BAverage] ,[BAheory],";
            insertsql += "[CreateTime],[CreateUser] ,[validate],[Remark],'" + DateTime.Now + "','" + acc.UserName + "'" + "FROM [BGOI_FlowMeter].[dbo].[tk_CheckData2] ";
            insertsql += " where RepairID=" + "'" + data.StrRepairID + "'";

            a_strErr = "";

            OperateLog log = new OperateLog();
            log.strMarkID = id;
            log.strLogTitle = "撤销" + data.StrRepairType;
            log.strLogContent = "撤销" + data.StrRepairType + "成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "撤销" + data.StrRepairType;


            string updatesql = "update tk_UTRepairCard set [State]=[State]-1 where RID='" + data.StrRID + "'";
            string deletesql = "delete tk_ProRecord  where RID='" + data.StrRID + "' and OpType='" + data.StrRepairType + "'";
            try
            {
                int intInsert = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (intInsert > 0)
                {
                    intDelete = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    if (data.StrRepairType == "进厂检测")
                        sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intDelete;

        }
        #endregion
        #endregion

        #region //清洗记录
        //开始清洗
        public static bool StartCleanRepair(tk_ProRecord pro)
        {

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            //过程记录
            string prosql = "insert into tk_ProRecord (RID,OpType,OpTime,OpUser,CreateTime,CreateUser,Validate)";
            prosql += "values('" + pro.StrRID + "','" + pro.StrOpType + "','" + pro.StrOpTime + "','" + pro.StrOpUser + "','";
            prosql += pro.StrCreateTime + "','" + pro.StrCreateUser + "','v')";
            //登记卡状态
            string updatesql = "update tk_RepairCard set [State]=[State]+1 where RID='" + pro.StrRID + "'";
            //登记卡状态
            string updatesql2 = "update tk_UTRepairCard set [State]=4 where RID='" + pro.StrRID + "'";
            try
            {
                sqlTrans.ExecuteNonQuery(prosql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql2, CommandType.Text, null);
                sqlTrans.Close(true);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }

        //添加清洗记录
        public static int InsertCleanRepair(tk_CleanRepair cleanRepair, ref string a_strErr)
        {
            int intInsertBas = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertBas = "insert into tk_CleanRepair (RID,CleanID,CleanUser,CleanSDate,CleanEDate,MeterID,Breakdown ,Remark,validate,CreateTime,CreateUser,IsPhoto)"

            + " values ('" + cleanRepair.StrRID + "','" + cleanRepair.StrCleanID + "','" + cleanRepair.StrCleanUser + "','" + cleanRepair.StrCleanSDate + "','"
            + cleanRepair.StrCleanEDate + "','" + cleanRepair.StrMeterID + "','" + cleanRepair.StrBreakdown + "','" + cleanRepair.StrRemark + "','"
            + cleanRepair.Strvalidate + "','" + cleanRepair.StrCreateTime + "','" + cleanRepair.StrCreateUser + "','" + cleanRepair.StrIsPhoto + "')";

            a_strErr = "";
            OperateLog Log = new OperateLog();
            Acc_Account account = GAccount.GetAccountInfo();
            Log.strMarkID = cleanRepair.StrCleanID;
            Log.strLogTitle = "新建清洗记录";
            Log.strLogContent = "新建清洗记录成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "新建清洗记录";


            tk_ProRecord pro = new tk_ProRecord();
            pro.StrRID = cleanRepair.StrRID;
            pro.StrOpType = "清洗完成";
            pro.StrOpTime = cleanRepair.StrCleanEDate;
            pro.StrOpUser = cleanRepair.StrCleanUser;
            pro.StrCreateTime = DateTime.Now;
            pro.StrCreateUser = account.UserName;


            string updatepr = "update tk_ProRecord set OpTime='" + cleanRepair.StrCleanSDate + "' where RID='" + cleanRepair.StrRID + "' and OpType='开始清洗'";
            try
            {
                if (strInsertBas != "")
                {
                    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatepr, CommandType.Text, null);
                    ChangeState(pro);
                    insertlog = OperateLog(Log, ref a_strErr);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        //查询加载清洗记录列表 
        public static UIDataTable LoadCleanRepairList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetCleanRepairList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 获取指定id对应的洗记录详细信息
        public static tk_CleanRepair getCleanRepair(string strID)
        {

            tk_CleanRepair data = new tk_CleanRepair();
            string strSql = "select * from tk_CleanRepair where CleanID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRID = dt.Rows[0]["RID"].ToString();
                data.StrCleanID = strID;
                data.StrCleanUser = dt.Rows[0]["CleanUser"].ToString();
                data.StrCleanSDate = Convert.ToDateTime(dt.Rows[0]["CleanSDate"]);

                data.StrCleanEDate = Convert.ToDateTime(dt.Rows[0]["CleanEDate"]);
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrBreakdown = dt.Rows[0]["Breakdown"].ToString();
                data.StrRemark = dt.Rows[0]["Remark"].ToString();
                data.Strvalidate = dt.Rows[0]["validate"].ToString();
                data.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                data.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();


                data.StrIsPhoto = dt.Rows[0]["IsPhoto"].ToString();


            }

            return data;

        }
        //清洗更换备件
        public static DataTable RepairChange(string where)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string a_strErr = "";
            string sql = "select RID,DeviceName, DeviceType,Measure,Num,UnitPrice,ConcesioPrice from tk_RepairChange where " + where;
            try
            {
                DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                    return dt;
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }

        }
        //修改清洗记录
        public static int UpdateCleanRepair(tk_CleanRepair cleanRepair, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "update tk_CleanRepair set RID='" + cleanRepair.StrRID + "',CleanID='" + cleanRepair.StrCleanID + "',CleanUser='" + cleanRepair.StrCleanUser
                + "',CleanSDate='" + cleanRepair.StrCleanSDate + "',CleanEDate='" + cleanRepair.StrCleanEDate + "',MeterID='" + cleanRepair.StrMeterID
                + "',Breakdown='" + cleanRepair.StrBreakdown + "',Remark='" + cleanRepair.StrRemark + "',validate='" + cleanRepair.Strvalidate
                + "',CreateTime='" + cleanRepair.StrCreateTime + "',CreateUser='" + cleanRepair.StrCreateUser + "',IsPhoto='" + cleanRepair.StrIsPhoto
                + "' where CleanID = '" + cleanRepair.StrCleanID + "'";


            string insertsql = "insert into tk_CleanRepairHis";
            insertsql += " select [RID],[CleanID],[CleanUser],[CleanSDate],[CleanEDate],[MeterID] ,[Breakdown],[Remark],[CreateTime],[CreateUser],[IsPhoto],";
            insertsql += "[validate],'" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += " FROM [BGOI_FlowMeter].[dbo].[tk_CleanRepair] where CleanID = '" + cleanRepair.StrCleanID + "' and validate='v'";
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = cleanRepair.StrCleanID;
            log.strLogTitle = "修改清洗记录";
            log.strLogContent = "修改清洗记录成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "修改清洗记录";

            string updatepr = "update tk_ProRecord set OpTime='" + cleanRepair.StrCleanSDate + "' where RID='" + cleanRepair.StrRID + "' and OpType='开始清洗'";
            string updatepr2 = "update tk_ProRecord set OpTime='" + cleanRepair.StrCleanEDate + "' where RID='" + cleanRepair.StrRID + "' and OpType='清洗完成'";
            try
            {
                int num = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (num > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatepr, CommandType.Text, null);
                    sqlTrans.ExecuteNonQuery(updatepr2, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                    string deletesql = "delete tk_RepairChange where CleanID='" + cleanRepair.StrCleanID + "'";
                    sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }
        //删除清洗记录
        public static int DeleteCleanRepair(string id, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intDelete = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "delete from tk_CleanRepair where CleanID='" + id + "'";
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = id;
            log.strLogTitle = "删除清洗记录";
            log.strLogContent = "删除清洗记录成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "删除清洗记录";

            tk_CleanRepair cleanRepair = getCleanRepair(id);
            string insertsql = "insert into tk_CleanRepairHis";
            insertsql += " select [RID],[CleanID],[CleanUser],[CleanSDate],[CleanEDate],[MeterID] ,[Breakdown],[Remark],[CreateTime],[CreateUser],[IsPhoto],";
            insertsql += "[validate],'" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += " FROM [BGOI_FlowMeter].[dbo].[tk_CleanRepair] where CleanID = '" + cleanRepair.StrCleanID + "' and validate='v'";
            string updatesql = "update tk_RepairCard set [State]=[State]-1 where RID='" + cleanRepair.StrRID + "'";
            string updatesql2 = "update tk_UTRepairCard set [State]=[State]-1 where RID='" + cleanRepair.StrRID + "'";
            string deletesql = "delete tk_ProRecord  where RID='" + cleanRepair.StrRID + "' and OpType='清洗完成'";

            string deletesql2 = "delete tk_RepairChange  where RID='" + cleanRepair.StrRID + "'";
            try
            {
                sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                intDelete = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);

                sqlTrans.ExecuteNonQuery(updatesql2, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(deletesql2, CommandType.Text, null);
                insertlog = OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intDelete;

        }

        #endregion

        #region //维修管理

        //开始维修
        public static bool StartRepair(tk_ProRecord pro)
        {

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            //过程记录
            string prosql = "insert into tk_ProRecord (RID,OpType,OpTime,OpUser,CreateTime,CreateUser,Validate)";
            prosql += "values('" + pro.StrRID + "','" + pro.StrOpType + "','" + pro.StrOpTime + "','" + pro.StrOpUser + "','";
            prosql += pro.StrCreateTime + "','" + pro.StrCreateUser + "','v')";
            //登记卡状态
            string updatesql = "update tk_RepairCard set [State]=[State]+1 where RID='" + pro.StrRID + "'";

            //登记卡状态
            string updatesql2 = "update tk_UTRepairCard set [State]=[State]+1 where RID='" + pro.StrRID + "'";
            try
            {
                sqlTrans.ExecuteNonQuery(prosql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql2, CommandType.Text, null);
                sqlTrans.Close(true);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }

        //添加维修记录
        public static int InsertRepairInfo(tk_RepairInfo repairInfo, string BakName, string BakType, string Measure, string BakNum, ref string a_strErr)
        {
            int intInsertBas = 0;
            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string strInsertBas = "insert into tk_RepairInfo (RID,RepairID,RepairUser,RepairSDate,RepairEDate,MeterID,Breakdown ,RepairContent,RepairNum,AdjustPre,AdjustNow,RepairResult,Remark,validate,CreateTime,CreateUser,IsPhoto)"

            + " values ('" + repairInfo.StrRID + "','" + repairInfo.StrRepairID + "','" + repairInfo.StrRepairUser + "','" + repairInfo.StrRepairSDate + "','"
            + repairInfo.StrRepairEDate + "','" + repairInfo.StrMeterID + "','" + repairInfo.StrBreakdown + "','" + repairInfo.StrRepairContent + "','"
            + repairInfo.StrRepairNum + "','" + repairInfo.StrAdjustPre + "','" + repairInfo.StrAdjustNow + "','" + repairInfo.StrRepairResult + "','"
            + repairInfo.StrRemark + "','" + repairInfo.Strvalidate + "','" + repairInfo.StrCreateTime + "','" + repairInfo.StrCreateUser + "','"
            + repairInfo.StrIsPhoto + "')";

            a_strErr = "";
            OperateLog Log = new OperateLog();
            Acc_Account account = GAccount.GetAccountInfo();
            Log.strMarkID = repairInfo.StrRepairID;
            Log.strLogTitle = "新建维修记录";
            Log.strLogContent = "新建维修记录成功";
            Log.strLogTime = DateTime.Now;
            Log.strLogPerson = account.UserName;
            Log.strType = "新建维修记录";

            tk_ProRecord pro = new tk_ProRecord();
            pro.StrRID = repairInfo.StrRID;
            pro.StrOpType = "维修完成";
            pro.StrOpTime = repairInfo.StrRepairEDate;
            pro.StrOpUser = repairInfo.StrRepairUser;
            pro.StrCreateTime = DateTime.Now;
            pro.StrCreateUser = account.UserName;
            string prosql = "insert into tk_ProRecord (RID,OpType,OpTime,OpUser,CreateTime,CreateUser,Validate)";
            prosql += "values('" + pro.StrRID + "','" + pro.StrOpType + "','" + pro.StrOpTime + "','" + pro.StrOpUser + "','";
            prosql += pro.StrCreateTime + "','" + pro.StrCreateUser + "','v')";


            try
            {

                intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(prosql, CommandType.Text, null);
                insertlog = OperateLog(Log, ref a_strErr);

                if (BakName != "" && BakName != null)
                {

                    var BakNameList = BakName.Split(',');
                    var BakTypeList = BakType.Split(',');
                    var MeasureList = Measure.Split(',');
                    var BakNumList = BakNum.Split(',');
                    for (int i = 0; i < (BakNameList.Count() - 1); i++)
                    {
                        string sql = "insert into tk_RepairDevice (RID,RepairID, DeviceName,DeviceType ,Measure,Num) values (";
                        sql += "'" + repairInfo.StrRID + "','" + repairInfo.StrRepairID + "','" + BakNameList[i] + "','" + BakTypeList[i] + "','" + MeasureList[i] + "','" + BakNumList[i] + "')";
                        sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);

                        sql = "update tk_TransCard set SendRepair='" + repairInfo.StrRepairEDate + "' where RID='" + repairInfo.StrRID + "'";
                        sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                    }

                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }

        //查询加载维修记录列表 
        public static UIDataTable LoadRepairInfoList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetRepairInfoList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        // 获取指定id对应的维修记录详细信息
        public static tk_RepairInfo getRepairInfo(string strID)
        {

            tk_RepairInfo data = new tk_RepairInfo();
            string strSql = "select * from tk_RepairInfo where RepairID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRID = dt.Rows[0]["RID"].ToString();
                data.StrRepairID = strID;
                data.StrRepairUser = dt.Rows[0]["RepairUser"].ToString();
                data.StrRepairSDate = Convert.ToDateTime(dt.Rows[0]["RepairSDate"]);

                data.StrRepairEDate = Convert.ToDateTime(dt.Rows[0]["RepairEDate"]);
                data.StrMeterID = dt.Rows[0]["MeterID"].ToString();
                data.StrBreakdown = dt.Rows[0]["Breakdown"].ToString();


                data.StrRepairContent = dt.Rows[0]["RepairContent"].ToString();
                data.StrRepairNum = dt.Rows[0]["RepairNum"].ToString();
                data.StrAdjustPre = dt.Rows[0]["AdjustPre"].ToString();
                data.StrAdjustNow = dt.Rows[0]["AdjustNow"].ToString();
                data.StrRepairResult = dt.Rows[0]["RepairResult"].ToString();


                data.StrRemark = dt.Rows[0]["Remark"].ToString();
                data.Strvalidate = dt.Rows[0]["validate"].ToString();
                data.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                data.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();


                data.StrIsPhoto = dt.Rows[0]["IsPhoto"].ToString();


            }

            return data;

        }

        public static tk_RepairInfo getRepairInfo2(string strID)
        {

            tk_RepairInfo data = new tk_RepairInfo();
            string strSql = "select * from tk_RepairInfo where RID = '" + strID + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRepairID = dt.Rows[0]["RepairID"].ToString();

            }

            return data;

        }
        //加载维修更换备件
        public static DataTable RepairDevice(string where)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string a_strErr = "";
            string sql = "select * from tk_RepairDevice where " + where;
            try
            {
                DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                    return dt;
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }

        }

        //修改维修记录
        public static int UpdateRepairInfo(tk_RepairInfo repairInfo, string BakName, string BakType, string Measure, string BakNum, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "update tk_RepairInfo set RID='" + repairInfo.StrRID + "',RepairID='" + repairInfo.StrRepairID + "',RepairUser='" + repairInfo.StrRepairUser
                + "',RepairSDate='" + repairInfo.StrRepairSDate + "',RepairEDate='" + repairInfo.StrRepairEDate + "',MeterID='" + repairInfo.StrMeterID
                + "',Breakdown='" + repairInfo.StrBreakdown + "',RepairContent='" + repairInfo.StrRepairContent + "',RepairNum='" + repairInfo.StrRepairNum
                + "',AdjustPre='" + repairInfo.StrAdjustPre + "',AdjustNow='" + repairInfo.StrAdjustNow + "',RepairResult='" + repairInfo.StrRepairResult
                + "',Remark='" + repairInfo.StrRemark + "',validate='" + repairInfo.Strvalidate + "',CreateTime='" + repairInfo.StrCreateTime
                + "',CreateUser='" + repairInfo.StrCreateUser + "',IsPhoto='" + repairInfo.StrIsPhoto + "' where RepairID = '" + repairInfo.StrRepairID + "'";

            string insertsql = "insert into tk_RepairInfoHis";
            insertsql += "  select  [RID],[RepairID],[RepairUser],[RepairSDate],[RepairEDate],[MeterID],[Breakdown],[RepairContent],[RepairNum],[AdjustPre],";
            insertsql += "[AdjustNow],[RepairResult],[Remark],[CreateTime],[CreateUser],[IsPhoto] ,[validate],'" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += "FROM [BGOI_FlowMeter].[dbo].[tk_RepairInfo]  where RepairID = '" + repairInfo.StrRepairID + "' and validate='v'";
            a_strErr = "";
            OperateLog log = new OperateLog();

            log.strMarkID = repairInfo.StrRepairID;
            log.strLogTitle = "修改维修记录";
            log.strLogContent = "修改维修记录成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "修改维修记录";

            string updatepr2 = "update tk_ProRecord set OpTime='" + repairInfo.StrRepairSDate + "' where RID='" + repairInfo.StrRID + "' and OpType='维修完成'";
            try
            {
                int num = sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                if (num > 0)
                {
                    intUpdate = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);

                    sqlTrans.ExecuteNonQuery(updatepr2, CommandType.Text, null);
                    insertlog = OperateLog(log, ref a_strErr);
                }
                string deletesql = "delete tk_RepairDevice where RID='" + repairInfo.StrRID + "'";
                sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                if (BakName != "" && BakName != null)
                {

                    var BakNameList = BakName.Split(',');
                    var BakTypeList = BakType.Split(',');
                    var MeasureList = Measure.Split(',');
                    var BakNumList = BakNum.Split(',');
                    for (int i = 0; i < (BakNameList.Count() - 1); i++)
                    {
                        string sql2 = "insert into tk_RepairDevice (RID,RepairID, DeviceName,DeviceType ,Measure,Num) values (";
                        sql2 += "'" + repairInfo.StrRID + "','" + repairInfo.StrRepairID + "','" + BakNameList[i] + "','" + BakTypeList[i] + "','" + MeasureList[i] + "','" + BakNumList[i] + "')";
                        sqlTrans.ExecuteNonQuery(sql2, CommandType.Text, null);
                    }

                }
                num = Convert.ToInt32(repairInfo.StrRepairNum);
                switch (num)
                {
                    case 1:
                        sql = "update tk_TransCard set OneRepair='" + repairInfo.StrRepairEDate + "' where RID='" + repairInfo.StrRID + "'";


                        break;
                    case 2:
                        sql = "update tk_TransCard set TwoRepair='" + repairInfo.StrRepairEDate + "' where RID='" + repairInfo.StrRID + "'";
                        break;
                    case 3:
                        sql = "update tk_TransCard set ThreeRepair='" + repairInfo.StrRepairEDate + "' where RID='" + repairInfo.StrRID + "'";

                        break;

                }
                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;

        }
        //删除维修记录
        public static int DeleteRepairInfo(string id, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intDelete = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = id;
            log.strLogTitle = "删除维修记录";
            log.strLogContent = "删除维修记录成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "删除维修记录";

            tk_RepairInfo repairInfo = getRepairInfo(id);
            string insertsql = "insert into tk_RepairInfoHis";
            insertsql += "  select  [RID],[RepairID],[RepairUser],[RepairSDate],[RepairEDate],[MeterID],[Breakdown],[RepairContent],[RepairNum],[AdjustPre],";
            insertsql += "[AdjustNow],[RepairResult],[Remark],[CreateTime],[CreateUser],[IsPhoto] ,[validate],'" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += "FROM [BGOI_FlowMeter].[dbo].[tk_RepairInfo]  where RepairID = '" + repairInfo.StrRepairID + "' and validate='v'";

            string sql = "delete from tk_RepairInfo where RepairID='" + id + "'";//删除维修记录

            string updatesql = "update tk_RepairCard set [State]=[State]-1 where RID='" + repairInfo.StrRID + "'";//改变登记卡状态

            string updatesql2 = "update tk_UTRepairCard set [State]=[State]-1 where RID='" + repairInfo.StrRID + "'";//改变登记卡状态
            string deletesql = "delete tk_ProRecord  where RID='" + repairInfo.StrRID + "' and OpType='维修完成'";//删除过程记录（维修）

            string deletesql2 = "delete tk_RepairDevice  where RID='" + repairInfo.StrRID + "'";//删除维修更换零件
            try
            {
                sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(updatesql2, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(deletesql2, CommandType.Text, null);
                insertlog = OperateLog(log, ref a_strErr);
                intDelete = 1;
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intDelete;

        }

        #endregion

        #region 任务管理

        // 获取表主键ID  key表名字
        public static string GetKeyId(string key)
        {
            Acc_Account account = GAccount.GetAccountInfo();
            int unitId = Convert.ToInt16(account.UnitID);
            string strRID = "";
            string strYMD = DateTime.Now.ToString("yyMMdd");
            string strSelRID = "select RID, RidNo from tk_RIDno where  RID='" + key + "'";
            DataTable dtRMaxID = SQLBase.FillTable(strSelRID, "FlowMeterDBCnn");
            int intNewID = 0;
            if (dtRMaxID == null)
            {
                return strRID;
            }
            if (dtRMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into tk_RIDno (RID,RidNo,DateRecord) values('" + key + "',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "FlowMeterDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtRMaxID.Rows[0]["RidNo"]);
            }

            intNewID++;
            string strUpdateID = "update tk_RIDno set RidNo='" + intNewID + "' where RID='" + key + "'";
            SQLBase.ExecuteNonQuery(strUpdateID, "FlowMeterDBCnn");

            strRID = GFun.GetNum(unitId, 4) + GFun.GetNum(intNewID, 4);
            return strRID;
        }

        /// <summary>
        /// 根究状态判断是否可以进行当前操作 
        /// </summary>
        /// <param name="p">当前操作类型</param>
        /// <param name="rid">当前操作对象（登记卡）id</param>
        /// <returns></returns>

        public static bool Operate(int p, string rid, string type, ref string strErr)
        {

            SQLTrans sqlTrans = new SQLTrans();
            string sql = "select [State] from [BGOI_FlowMeter].[dbo].[tk_RepairCard] where RID='" + rid + "'";
            if (type == "超声波" || type == "CardType2")
            {
                sql = "select [State] from [BGOI_FlowMeter].[dbo].[tk_UTRepairCard] where RID='" + rid + "'";
                if (p == 4)
                {
                    return true;
                }
            }

            DataTable dtRMaxID = SQLBase.FillTable(sql, "FlowMeterDBCnn");
            int state = Convert.ToInt32(dtRMaxID.Rows[0]["State"]) + 1;
            if (p == state)
            {
                return true;
            }
            string str = strErr;


            switch (state - 1)
            {
                case 0:
                    strErr = "设备处于登记状态，不可以" + str;
                    break;
                case 1:
                    strErr = "设备处于收货状态，不可以" + str;
                    break;
                case 2:
                    strErr = "设备处于待初测状态，不可以" + str;
                    break;
                case 3:
                    strErr = "设备处于待清洗状态，不可以" + str;
                    break;
                case 4:
                    strErr = "设备处于清洗中，不可以" + str;
                    break;
                case 5:
                    strErr = "设备处于待维修状态，不可以" + str;
                    break;
                case 6:
                    strErr = "设备处于维修中，不可以" + str;
                    break;
                case 7:
                    strErr = "设备已维修完成，不可以" + str;
                    break;
                case 8:
                    strErr = "设备处于外送检测中，不可以" + str;
                    break;
                case 9:
                    strErr = "设备处于待打压状态，不可以" + str;
                    break;
                case 10:
                    strErr = "设备处于待领取状态，不可以" + str;
                    break;
                case 11:
                    strErr = "设备已领取，不可以" + str;
                    break;



            }

            return false;

        }

        public static bool ChangeState(tk_ProRecord pro)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string id = "select * from tk_RepairCard where RID='" + pro.StrRID + "'";
            DataTable dt = SQLBase.FillTable(id, "FlowMeterDBCnn");
            if (dt != null && dt.Rows.Count > 0)
            {
                //过程记录
                string prosql = "insert into tk_ProRecord (RID,OpType,OpTime,OpUser,CreateTime,CreateUser,Validate)";
                prosql += "values('" + pro.StrRID + "','" + pro.StrOpType + "','" + pro.StrOpTime + "','" + pro.StrOpUser + "','";
                prosql += pro.StrCreateTime + "','" + pro.StrCreateUser + "','v')";
                //登记卡状态
                string updatesql = "update tk_RepairCard set [State]=[State]+1 where RID='" + pro.StrRID + "'";
                try
                {
                    sqlTrans.ExecuteNonQuery(prosql, CommandType.Text, null);
                    if (pro.StrOpType != "维修完成")
                    {
                        sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                    }
                    sqlTrans.Close(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;



                }
            }
            else
            {
                //过程记录
                string prosql = "insert into tk_ProRecord (RID,OpType,OpTime,OpUser,CreateTime,CreateUser,Validate)";
                prosql += "values('" + pro.StrRID + "','" + pro.StrOpType + "','" + pro.StrOpTime + "','" + pro.StrOpUser + "','";
                prosql += pro.StrCreateTime + "','" + pro.StrCreateUser + "','v')";
                //登记卡状态
                string updatesql = "update tk_UTRepairCard set [State]=[State]+1 where RID='" + pro.StrRID + "'";
                try
                {
                    sqlTrans.ExecuteNonQuery(prosql, CommandType.Text, null);
                    if (pro.StrOpType != "维修检测")
                    {
                        sqlTrans.ExecuteNonQuery(updatesql, CommandType.Text, null);
                    }
                    sqlTrans.Close(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }

            }

        }

        //查看过程记录(对应步骤)
        public static tk_ProRecord getRepairInfo(string strID, string type)
        {

            tk_ProRecord data = new tk_ProRecord();
            string strSql = "select * from tk_ProRecord where RID = '" + strID + "' and OpType='" + type + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {
                data.StrRID = strID;

                data.StrOpType = type;
                data.StrOpTime = Convert.ToDateTime(dt.Rows[0]["OpTime"]);
                data.StrOpUser = dt.Rows[0]["OpUser"].ToString();
                data.StrCreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                data.StrCreateUser = dt.Rows[0]["CreateUser"].ToString();




            }

            return data;

        }
        //过程记录查询
        public static UIDataTable LoadProcedureList(string Rid)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            UIDataTable instData = new UIDataTable();

            string sql = "select distinct RID,";
            sql += "(select OpTime from tk_ProRecord b where a.RID = b.RID and OpType='进厂检测') 进厂检测, ";
            sql += "(select OpTime from tk_ProRecord b where a.RID = b.RID and OpType='开始清洗') 开始清洗, ";
            sql += "(select OpTime from tk_ProRecord b where  a.RID = b.RID and OpType='清洗完成') 清洗完成,";
            sql += "(select OpTime from tk_ProRecord b where  a.RID = b.RID and OpType='开始维修') 开始维修,";
            sql += "(select OpTime from tk_ProRecord b where  a.RID = b.RID and OpType='维修完成') 维修完成,";
            sql += "(select OpTime from tk_ProRecord b where  a.RID = b.RID and OpType='维修检测') 维修检测,";
            sql += "(select OpTime from tk_ProRecord b where  a.RID = b.RID and OpType='出厂检测') 出厂检测";
            sql += "  from tk_ProRecord a where RID='" + Rid + "'";

            DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            DataTable dtOrder = DO_Order;
            instData.IntRecords = GFun.SafeToInt32(DO_Order.Rows.Count);
            if (instData.IntRecords > 0)
            {
                if (instData.IntRecords % 15 == 0)
                    instData.IntTotalPages = instData.IntRecords / 15;
                else
                    instData.IntTotalPages = instData.IntRecords / 15 + 1;
            }
            else
                instData.IntTotalPages = 0;


            instData.DtData = dtOrder;
            return instData;

        }
        #endregion

        #region 报价单

        //获取零件
        public static DataTable GetComponent(string key)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("MainInventory");
            UIDataTable instData = new UIDataTable();


            string selectsql = "select * from [BGOI_Inventory].[dbo].[tk_ProductInfo]";
            if (key != "")
            {
                selectsql += " where ProName like '%" + key + "%'";
            }
            string a_strErr = "";
            try
            {
                DataTable dt = SQLBase.FillTable(selectsql, "FlowMeterDBCnn");
                if (dt != null && dt.Rows.Count > 0)
                    return dt;
                else
                    return null;
            }
            catch (Exception ex)
            {
                a_strErr = ex.Message;
                throw;
            }
        }








        //添加报价单
        public static int InsertQuotation(string RID, string type, string p, string DeviceName, string DeviceType, string Num, string UnitPrice, string TotalPrice, string Comments, string Measure, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intInsertBas = 0;

            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");



            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = RID;
            log.strLogTitle = "添加报价单";
            log.strLogContent = "添加报价单成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "添加报价单";


            decimal TotalPriceU = 0;
            decimal TotalPriceC = 0;
            try
            {

                var Qid = "BJ" + GetKeyId("Quotation");
                var TypeList = type.Split(',');
                var UnitPriceList = p.Split(',');

                string deletesql = "delete tk_Quotation where RID='" + RID + "'";
                sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                for (int i = 0; i < (TypeList.Count() - 1); i++)
                {
                    string sql = "insert into tk_Quotation (RID,QID,Type,UnitPrice,ConcesioPrice)";

                    sql += " values ('" + RID + "','" + Qid + "','" + TypeList[i] + "','" + UnitPriceList[i] + "','0')";
                    TotalPriceU += Convert.ToDecimal(UnitPriceList[i]);

                    sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                }

                var DeviceNameL = DeviceName.Split(',');
                var DeviceTypeL = DeviceType.Split(',');
                var NumL = Num.Split(',');
                var MeasureL = Measure.Split(',');
                var UnitPriceL = UnitPrice.Split(',');
                var TotalPriceL = TotalPrice.Split(',');
                var CommentsL = Comments.Split('@');

                string strSql = "select * from [tk_RepairDevice] where RID = '" + RID + "'";
                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                var pid = "";
                if (dt.Rows.Count > 0)
                {
                    pid = dt.Rows[0]["RepairID"].ToString();
                }

                string deletesql2 = "delete tk_RepairDevice where RID='" + RID + "'";
                sqlTrans.ExecuteNonQuery(deletesql2, CommandType.Text, null);
                for (int i = 0; i < (DeviceNameL.Count() - 1); i++)
                {







                    string sql2 = "insert into tk_RepairDevice (RID,RepairID, DeviceName,DeviceType ,Num,UnitPrice,TotalPrice,Comments,Measure) values (";
                    sql2 += "'" + RID + "','" + pid + "','" + DeviceNameL[i] + "','" + DeviceTypeL[i] + "','" + NumL[i];
                    sql2 += "','" + UnitPriceL[i] + "','" + TotalPriceL[i] + "','" + CommentsL[i] + "','" + MeasureL[i] + "')";


                    sqlTrans.ExecuteNonQuery(sql2, CommandType.Text, null);


                }
                string deleteqtn = "delete tk_GenQtn where RID='" + RID + "'"; sqlTrans.ExecuteNonQuery(deleteqtn, CommandType.Text, null);
                string genqtn = "insert into tk_GenQtn(RID,QID,TotalPriceU,TotalPriceC,State) values('" + RID + "','" + Qid + "','" + TotalPriceU + "','" + TotalPriceC + "','0')";
                sqlTrans.ExecuteNonQuery(genqtn, CommandType.Text, null);
                intInsertBas = 1;

                OperateLog(log, ref a_strErr);


                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas;
        }
        //修改报价单
        public static int UpdateQuotation(string RID, string type, string p, string DeviceName, string DeviceType, string Num, string UnitPrice, string TotalPrice, string Comments, string Measure, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intUpdate = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            a_strErr = "";

            OperateLog log = new OperateLog();
            log.strMarkID = RID;
            log.strLogTitle = "修改报价单";
            log.strLogContent = "修改报价单成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "修改报价单";


            string insertsql = "insert into tk_QuotationHis";
            insertsql += "  select  [RID],[QID],[Type],[RepairContent],[UnitPrice],[ConcesioPrice],'','" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += " FROM [BGOI_FlowMeter].[dbo].[tk_Quotation]  where RID = '" + RID + "' and Type='清洗'";
            string insertsql2 = "insert into tk_QuotationHis";
            insertsql2 += "  select  [RID],[QID],[Type],[RepairContent],[UnitPrice],[ConcesioPrice],'','" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql2 += " FROM [BGOI_FlowMeter].[dbo].[tk_Quotation]  where RID = '" + RID + "' and Type='检测'";

            sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
            sqlTrans.ExecuteNonQuery(insertsql2, CommandType.Text, null);
            decimal TotalPriceU = 0;

            try
            {

                var TypeList = type.Split(',');
                var UnitPriceList = p.Split(',');


                for (int i = 0; i < (TypeList.Count() - 1); i++)
                {
                    string sql = "update  tk_Quotation set UnitPrice='" + UnitPriceList[i] + "',ConcesioPrice='0' ";

                    sql += " where RID='" + RID + "' and Type='" + TypeList[i] + "'"; ;
                    TotalPriceU += Convert.ToDecimal(UnitPriceList[i]);

                    sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                }

                var DeviceNameL = DeviceName.Split(',');
                var DeviceTypeL = DeviceType.Split(',');
                var MeasureL = Measure.Split(',');
                var NumL = Num.Split(',');
                var UnitPriceL = UnitPrice.Split(',');
                var TotalPriceL = TotalPrice.Split(',');
                var CommentsL = Comments.Split('@');

                string strSql = "select * from tk_RepairDevice where RID = '" + RID + "'";
                DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
                var id = dt.Rows[0]["RepairID"].ToString();



                string deletesql = "delete tk_RepairDevice where RID='" + RID + "'";
                sqlTrans.ExecuteNonQuery(deletesql, CommandType.Text, null);
                for (int i = 0; i < (DeviceNameL.Count() - 1); i++)
                {




                    string sql2 = "insert into tk_RepairDevice (RID,RepairID, DeviceName,DeviceType ,Num,UnitPrice,TotalPrice,Comments,Measure) values (";
                    sql2 += "'" + RID + "','" + id + "','" + DeviceNameL[i] + "','" + DeviceTypeL[i] + "','" + NumL[i];
                    sql2 += "','" + UnitPriceL[i] + "','" + TotalPriceL[i] + "','" + CommentsL[i] + "','" + MeasureL[i] + "')";
                    //TotalPriceU += Convert.ToDecimal(TotalPriceL[i]);

                    sqlTrans.ExecuteNonQuery(sql2, CommandType.Text, null);

                }

                string genqtn = "update  tk_GenQtn set TotalPriceU='" + TotalPriceU + "',TotalPriceC='0',State='0' where RID='" + RID + "'";
                sqlTrans.ExecuteNonQuery(genqtn, CommandType.Text, null);
                intUpdate = 1;
                OperateLog(log, ref a_strErr);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdate;
        }
        // 获取指定id对应的维修记录详细信息
        public static tk_Quotation getQuotation(string id)
        {

            tk_Quotation data = new tk_Quotation();
            string strSql = "select * from tk_Quotation where QID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "FlowMeterDBCnn");
            if (dt.Rows.Count > 0)
            {

                data.StrRID = dt.Rows[0]["RID"].ToString();
                data.StrQID = Convert.ToInt32(dt.Rows[0]["QID"].ToString());
                data.StrType = dt.Rows[0]["Type"].ToString();
                data.StrRepairContent = dt.Rows[0]["RepairContent"].ToString();
                data.StrUnitPrice = Convert.ToDecimal(dt.Rows[0]["UnitPrice"].ToString());
                data.StrConcesioPrice = Convert.ToDecimal(dt.Rows[0]["ConcesioPrice"].ToString());

            }

            return data;

        }
        //查询加载报价单 详情
        public static UIDataTable LoadQuotationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetQuotationList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        //查询加载报价单总价 
        public static UIDataTable GetGenList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetGenList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                DataTable dtOrder = DO_Order.Tables[0];
                dtOrder.Columns.Add("零件", typeof(System.String));
                dtOrder.Columns.Add("DeviceName", typeof(System.String));
                dtOrder.Columns.Add("UnitPrice", typeof(System.String));
                dtOrder.Columns.Add("Num", typeof(System.String));
                dtOrder.Columns.Add("Measure", typeof(System.String));
                dtOrder.Columns.Add("TotalPrice2", typeof(System.String));

                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    string sql = "select *  from [tk_RepairDevice] where RID='" + dtOrder.Rows[i]["RID"].ToString() + "'";
                    DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                    var str = "";
                    var DeviceName = "";
                    var UnitPrice = "";
                    var Num = "";
                    var Measure = "";
                    var TotalPrice2 = "";
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (j == dt.Rows.Count - 1)
                            {
                                DeviceName += dt.Rows[j]["DeviceName"];
                                UnitPrice += dt.Rows[j]["UnitPrice"];
                                Num += dt.Rows[j]["Num"];
                                Measure += dt.Rows[j]["Measure"];
                                TotalPrice2 += dt.Rows[j]["TotalPrice"];
                                str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                                str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                            }
                            else
                            {

                                DeviceName += dt.Rows[j]["DeviceName"] + ",";
                                UnitPrice += dt.Rows[j]["UnitPrice"] + ",";
                                Num += dt.Rows[j]["Num"] + ",";
                                Measure += dt.Rows[j]["Measure"] + ",";
                                TotalPrice2 += dt.Rows[j]["TotalPrice"] + ",";
                                str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                                str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                            }
                        }

                    }
                    else
                    {
                        str += "<tr></tr>";
                    }
                    dtOrder.Rows[i]["零件"] = str;
                    dtOrder.Rows[i]["DeviceName"] = DeviceName;
                    dtOrder.Rows[i]["UnitPrice"] = UnitPrice;
                    dtOrder.Rows[i]["Num"] = Num;

                    dtOrder.Rows[i]["Measure"] = Measure;
                    dtOrder.Rows[i]["TotalPrice2"] = TotalPrice2;
                }
                instData.DtData = dtOrder;
            }

            return instData;

        }
        //查询加载报价单总价 
        public static UIDataTable GetGenList2(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetGenList2", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            else
            {
                DataTable dtOrder = DO_Order.Tables[0];
                dtOrder.Columns.Add("零件", typeof(System.String));
                dtOrder.Columns.Add("DeviceName", typeof(System.String));
                dtOrder.Columns.Add("UnitPrice", typeof(System.String));
                dtOrder.Columns.Add("Num", typeof(System.String));
                dtOrder.Columns.Add("Measure", typeof(System.String));
                dtOrder.Columns.Add("TotalPrice2", typeof(System.String));

                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    string sql = "select *  from [tk_RepairDevice] where RID='" + dtOrder.Rows[i]["RID"].ToString() + "'";
                    DataTable dt = SQLBase.FillTable(sql, "FlowMeterDBCnn");
                    var str = "";
                    var DeviceName = "";
                    var UnitPrice = "";
                    var Num = "";
                    var Measure = "";
                    var TotalPrice2 = "";
                    if (dt.Rows.Count > 0)
                    {
                        //for (int j = 0; j < dt.Rows.Count; j++)
                        //{
                        //    if (j == dt.Rows.Count - 1)
                        //    {
                        //        DeviceName += "<div >" + dt.Rows[j]["DeviceName"] + "</div><br/>";
                        //        UnitPrice += "<div>" + dt.Rows[j]["UnitPrice"] + "</div><br/>";
                        //        Num += "<div>" + dt.Rows[j]["Num"] + "</div><br/>";
                        //        Measure += "<div>" + dt.Rows[j]["Measure"] + "</div><br/>";
                        //        TotalPrice2 += "<div>" + dt.Rows[j]["TotalPrice"] + "</div><br/>";
                        //        str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                        //        str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                        //    }
                        //    else
                        //    {

                        //        DeviceName += "<div  style='border-bottom:1px solid #000000;' >" + dt.Rows[j]["DeviceName"] + "</div><br/>";
                        //        UnitPrice += "<div  style='border-bottom:1px solid #000000;' >" + dt.Rows[j]["UnitPrice"] + "</div><br/>";
                        //        Num += "<div  style='border-bottom:1px solid #000000;' >" + dt.Rows[j]["Num"] + "</div><br/>";
                        //        Measure += "<div  style='border-bottom:1px solid #000000;' >" + dt.Rows[j]["Measure"] + "</div><br/>";
                        //        TotalPrice2 += "<div  style='border-bottom:1px solid #000000;' >" + dt.Rows[j]["TotalPrice"] + "</div><br/>";
                        //        str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                        //        str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                        //    }

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (j == dt.Rows.Count - 1)
                            {
                                DeviceName += dt.Rows[j]["DeviceName"];
                                UnitPrice += dt.Rows[j]["UnitPrice"];
                                Num += dt.Rows[j]["Num"];
                                Measure += dt.Rows[j]["Measure"];
                                TotalPrice2 += dt.Rows[j]["TotalPrice"];
                                str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                                str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                            }
                            else
                            {

                                DeviceName += dt.Rows[j]["DeviceName"] + ",";
                                UnitPrice += dt.Rows[j]["UnitPrice"] + ",";
                                Num += dt.Rows[j]["Num"] + ",";
                                Measure += dt.Rows[j]["Measure"] + ",";
                                TotalPrice2 += dt.Rows[j]["TotalPrice"] + ",";
                                str += "<tr ><td width='70px' style='border-left:none'>" + dt.Rows[j]["DeviceName"] + "</td><td width='50px'>" + dt.Rows[j]["UnitPrice"] + "</td><td width='50px'>" + dt.Rows[j]["Num"];
                                str += "</td><td width='50px'>" + dt.Rows[j]["Measure"] + "</td><td width='50px'>" + dt.Rows[j]["TotalPrice"] + "</td></tr>";
                            }
                        }

                    }
                    else
                    {
                        str += "<tr></tr>";
                    }
                    dtOrder.Rows[i]["零件"] = str;
                    dtOrder.Rows[i]["DeviceName"] = DeviceName;
                    dtOrder.Rows[i]["UnitPrice"] = UnitPrice;
                    dtOrder.Rows[i]["Num"] = Num;

                    dtOrder.Rows[i]["Measure"] = Measure;
                    dtOrder.Rows[i]["TotalPrice2"] = TotalPrice2;
                }
                instData.DtData = dtOrder;
            }

            return instData;

        }

        //查询加载报价单总价 
        public static UIDataTable GetGenQtnList(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)

                };
            DataSet DO_Order = SQLBase.FillDataSet("GetGenQtnList", CommandType.StoredProcedure, sqlPar, "FlowMeterDBCnn");
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

        //删除报价单
        public static int DeleteQuotation(string id, ref string a_strErr)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            int intDelete = 0;

            int insertlog = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "delete  tk_Quotation where RID='" + id + "'";
            a_strErr = "";
            OperateLog log = new OperateLog();
            log.strMarkID = id;
            log.strLogTitle = "撤销报价单";
            log.strLogContent = "撤销报价单成功";
            log.strLogPerson = acc.UserName;
            log.strLogTime = DateTime.Now;
            log.strType = "撤销报价单";


            string insertsql = "insert into tk_QuotationHis";
            insertsql += "  select  [RID],[QID],[Type],[RepairContent],[UnitPrice],[ConcesioPrice],'','" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql += " FROM [BGOI_FlowMeter].[dbo].[tk_Quotation]  where RID = '" + id + "' and Type='清洗'";
            string insertsql2 = "insert into tk_QuotationHis";
            insertsql2 += "  select  [RID],[QID],[Type],[RepairContent],[UnitPrice],[ConcesioPrice],'','" + DateTime.Now + "','" + acc.UserName + "'";
            insertsql2 += " FROM [BGOI_FlowMeter].[dbo].[tk_Quotation]  where RID = '" + id + "' and Type='检测'";

            try
            {
                sqlTrans.ExecuteNonQuery(insertsql, CommandType.Text, null);
                sqlTrans.ExecuteNonQuery(insertsql2, CommandType.Text, null);
                intDelete = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);

                insertlog = OperateLog(log, ref a_strErr);

                string genqtn = "delete  tk_GenQtn where RID='" + id + "'";
                sqlTrans.ExecuteNonQuery(genqtn, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intDelete;

        }
        #endregion
        #region 统计分析

        public static string getModels2(string strRID, string type)
        {
            string strModels = " select b.Text as Model,c.Text as X_Model, d.Text as ModelType from tk_RepairCard a ";
            strModels += " left join (select * from tk_ConfigContent where Type='YBModel' and validate='v') b on a.Model=b.SID ";
            strModels += " left join (select * from tk_ConfigContent where Type='XZYModel' and validate='v') c on a.X_Model=c.SID ";
            strModels += " left join (select * from tk_ConfigContent where Type='CardType' and validate='v') d on a.ModelType=d.SID ";
            strModels += " where a.Validate='v' and a.RID='" + strRID + "'";
            if (type == "CardType2")
            {
                strModels = "";
                strModels = "select b.Text as Model,'' as X_Model, d.Text as ModelType from tk_UTRepairCard a ";
                strModels += " left join (select * from tk_ConfigContent where Type='YBModel' and validate='v') b on a.Model=b.SID ";

                strModels += " left join (select * from tk_ConfigContent where Type='CardType' and validate='v') d on a.ModelType=d.SID ";
                strModels += " where a.RID='" + strRID + "'";
            }


            DataTable dt = SQLBase.FillTable(strModels, "FlowMeterDBCnn");
            if (dt == null || dt.Rows.Count < 1)
                return "";
            else
                return dt.Rows[0][0].ToString() + "," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString();

        }
        public static DataTable ScheduleDetail(string where, string type)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");




            DataTable dt = new DataTable();
            dt.Columns.Add("口径", typeof(System.String));
            dt.Columns.Add("清洗完成", typeof(System.String));
            dt.Columns.Add("维修完成", typeof(System.String));
            dt.Columns.Add("完成总数", typeof(System.String));
            dt.Columns.Add("待初测", typeof(System.String));
            dt.Columns.Add("待清洗", typeof(System.String));
            dt.Columns.Add("清洗中", typeof(System.String));
            dt.Columns.Add("待维修", typeof(System.String));

            dt.Columns.Add("维修中", typeof(System.String));
            dt.Columns.Add("待检测", typeof(System.String));
            dt.Columns.Add("待打压", typeof(System.String));
            dt.Columns.Add("正在进行总数", typeof(System.String));

            dt.Columns.Add("周转表", typeof(System.String));

            dt.Columns.Add("无法维修", typeof(System.String));
            dt.Columns.Add("总数", typeof(System.String));
            dt.Columns.Add("待出厂", typeof(System.String));
            dt.Columns.Add("已出厂", typeof(System.String));

            for (int i = 1; i <= 8; i++)
            {
                var id = 50;
                switch (i)
                {
                    case 1:
                        id = 50;
                        break;
                    case 2:
                        id = 80;
                        break;
                    case 3:
                        id = 100;
                        break;
                    case 4:
                        id = 150;
                        break;
                    case 5:
                        id = 200;
                        break;
                    case 6:
                        id = 250;
                        break;
                    case 7:
                        id = 300;
                        break;
                    case 8:
                        id = 400;
                        break;
                }
                string sql = " select  top 1 ";
                sql += " (select COUNT(*) from [tk_CheckData]  where  RepairType='清洗检测' and [validate]='是' and RID in (select RID from tk_RepairCard  where  Caliber=" + id + where + ") ) 清洗完成,";
                sql += " (select COUNT(*) from [tk_CheckData]  where  RepairType='维修检测' and [validate]='是' and RID in (select RID from tk_RepairCard  where  Caliber=" + id + where + ") ) 维修完成,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='2' and Caliber='" + id + "'" + where + ") 待初测,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='3' and Caliber='" + id + "'" + where + ") 待清洗,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='4' and Caliber='" + id + "'" + where + ") 清洗中,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='5' and Caliber='" + id + "'" + where + ") 待维修,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='6' and Caliber='" + id + "'" + where + ") 维修中,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='7' and Caliber='" + id + "'" + where + ")待检测,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='9' and Caliber='" + id + "'" + where + ") 待打压,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='10' and Caliber='" + id + "'" + where + ") 待出厂,";
                sql += " (select COUNT(*) from tk_RepairCard  where  State='11' and Caliber='" + id + "'" + where + ") 已出厂,";
                sql += " (select COUNT(*) from tk_RepairCard  where  ModelProperty='周转表' and Caliber='" + id + "'" + where + ") 周转表,";
                sql += " (select COUNT(*) from tk_RepairCard  where   ModelProperty='无法维修' and Caliber='" + id + "'" + where + ") 无法维修";


                sql += "  from tk_RepairCard";
                DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");

                string sql2 = " select  top 1 ";
                sql2 += " (select COUNT(*) from [tk_CheckData2]  where  RepairType='清洗检测' and [validate]='是' and RID in (select RID from tk_UTRepairCard  where  Caliber=" + id + where + ")) 清洗完成,";
                sql2 += " (select COUNT(*) from [tk_CheckData2]  where  RepairType='维修检测' and [validate]='是' and RID in (select RID from tk_UTRepairCard  where  Caliber=" + id + where + ")) 维修完成,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='2' and Caliber='" + id + "'" + where + ") 待初测,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='3' and Caliber='" + id + "'" + where + ") 待清洗,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='4' and Caliber='" + id + "'" + where + ") 清洗中,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='5' and Caliber='" + id + "'" + where + where + ") 待维修,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='6' and Caliber='" + id + "'" + where + ") 维修中,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='7' and Caliber='" + id + "'" + where + ")待检测,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='9' and Caliber='" + id + "'" + where + ") 待打压,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='10' and Caliber='" + id + "'" + where + ") 待出厂,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  State='11' and Caliber='" + id + "'" + where + ") 已出厂,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where  ModelProperty='周转表' and Caliber='" + id + "'" + where + ") 周转表,";
                sql2 += " (select COUNT(*) from tk_UTRepairCard  where   ModelProperty='无法维修' and Caliber='" + id + "'" + where + ") 无法维修";
                sql2 += "  from tk_UTRepairCard";
                DataTable DO_Order2 = SQLBase.FillTable(sql2, "FlowMeterDBCnn");
                #region
                switch (type)
                {
                    case "CardType2"://超声
                        if (DO_Order2.Rows.Count > 0)
                        {

                            DataRow dr = dt.NewRow();
                            dr["口径"] = "DN" + id;
                            dr["清洗完成"] = DO_Order2.Rows[0]["清洗完成"].ToString();
                            dr["维修完成"] = DO_Order2.Rows[0]["维修完成"].ToString();
                            var num = Convert.ToInt32(DO_Order2.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["维修完成"].ToString());
                            dr["完成总数"] = num;
                            var num1 = 0;
                            dr["待初测"] = DO_Order2.Rows[0]["待初测"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["待初测"].ToString());
                            dr["待清洗"] = DO_Order2.Rows[0]["待清洗"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["待清洗"].ToString());
                            dr["清洗中"] = DO_Order2.Rows[0]["清洗中"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["清洗中"].ToString());
                            dr["待维修"] = DO_Order2.Rows[0]["待维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["待维修"].ToString());
                            dr["维修中"] = DO_Order2.Rows[0]["维修中"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["维修中"].ToString());
                            dr["待检测"] = DO_Order2.Rows[0]["待检测"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["待检测"].ToString());
                            dr["待打压"] = DO_Order2.Rows[0]["待打压"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["待打压"].ToString());
                            dr["正在进行总数"] = num1;


                            dr["周转表"] = DO_Order2.Rows[0]["周转表"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["周转表"].ToString());
                            dr["无法维修"] = DO_Order2.Rows[0]["无法维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order2.Rows[0]["无法维修"].ToString());
                            dr["总数"] = num + num1;


                            dr["待出厂"] = DO_Order2.Rows[0]["待出厂"].ToString();
                            dr["已出厂"] = DO_Order2.Rows[0]["已出厂"].ToString();
                            dt.Rows.Add(dr);


                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "DN" + id;
                            dr["清洗完成"] = 0;
                            dr["维修完成"] = 0;
                            dr["完成总数"] = 0;
                            dr["待初测"] = 0;
                            dr["待清洗"] = 0;
                            dr["清洗中"] = 0;

                            dr["待维修"] = 0;
                            dr["维修中"] = 0;
                            dr["待检测"] = 0;
                            dr["待打压"] = 0;
                            dr["正在进行总数"] = 0;

                            dr["周转表"] = 0;
                            dr["无法维修"] = 0;
                            dr["总数"] = 0;
                            dr["待出厂"] = 0;
                            dr["已出厂"] = 0;
                            dt.Rows.Add(dr);

                        }

                        break;
                    case "CardType1"://涡轮
                        if (DO_Order.Rows.Count > 0)
                        {

                            DataRow dr = dt.NewRow();
                            dr["口径"] = "DN" + id;
                            dr["清洗完成"] = DO_Order.Rows[0]["清洗完成"].ToString();
                            dr["维修完成"] = DO_Order.Rows[0]["维修完成"].ToString();
                            var num = Convert.ToInt32(DO_Order.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order.Rows[0]["维修完成"].ToString());
                            dr["完成总数"] = num;
                            var num1 = 0;
                            dr["待初测"] = DO_Order.Rows[0]["待初测"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待初测"].ToString());
                            dr["待清洗"] = DO_Order.Rows[0]["待清洗"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待清洗"].ToString());
                            dr["清洗中"] = DO_Order.Rows[0]["清洗中"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["清洗中"].ToString());
                            dr["待维修"] = DO_Order.Rows[0]["待维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待维修"].ToString());
                            dr["维修中"] = DO_Order.Rows[0]["维修中"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["维修中"].ToString());
                            dr["待检测"] = DO_Order.Rows[0]["待检测"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待检测"].ToString());
                            dr["待打压"] = DO_Order.Rows[0]["待打压"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待打压"].ToString());
                            dr["正在进行总数"] = num1;


                            dr["周转表"] = DO_Order.Rows[0]["周转表"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["周转表"].ToString());
                            dr["无法维修"] = DO_Order.Rows[0]["无法维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["无法维修"].ToString());
                            dr["总数"] = num + num1;


                            dr["待出厂"] = DO_Order.Rows[0]["待出厂"].ToString();
                            dr["已出厂"] = DO_Order.Rows[0]["已出厂"].ToString();
                            dt.Rows.Add(dr);


                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "DN" + id;
                            dr["清洗完成"] = 0;
                            dr["维修完成"] = 0;
                            dr["完成总数"] = 0;
                            dr["待初测"] = 0;
                            dr["待清洗"] = 0;
                            dr["清洗中"] = 0;

                            dr["待维修"] = 0;
                            dr["维修中"] = 0;
                            dr["待检测"] = 0;
                            dr["待打压"] = 0;
                            dr["正在进行总数"] = 0;

                            dr["周转表"] = 0;
                            dr["无法维修"] = 0;
                            dr["总数"] = 0;
                            dr["待出厂"] = 0;
                            dr["已出厂"] = 0;
                            dt.Rows.Add(dr);
                        }


                        break;
                    case "CardType3"://腰轮
                        if (DO_Order.Rows.Count > 0)
                        {

                            DataRow dr = dt.NewRow();
                            dr["口径"] = "DN" + id;
                            dr["清洗完成"] = DO_Order.Rows[0]["清洗完成"].ToString();
                            dr["维修完成"] = DO_Order.Rows[0]["维修完成"].ToString();
                            var num = Convert.ToInt32(DO_Order.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order.Rows[0]["维修完成"].ToString());
                            dr["完成总数"] = num;
                            var num1 = 0;
                            dr["待初测"] = DO_Order.Rows[0]["待初测"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待初测"].ToString());
                            dr["待清洗"] = DO_Order.Rows[0]["待清洗"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待清洗"].ToString());
                            dr["清洗中"] = DO_Order.Rows[0]["清洗中"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["清洗中"].ToString());
                            dr["待维修"] = DO_Order.Rows[0]["待维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待维修"].ToString());
                            dr["维修中"] = DO_Order.Rows[0]["维修中"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["维修中"].ToString());
                            dr["待检测"] = DO_Order.Rows[0]["待检测"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待检测"].ToString());
                            dr["待打压"] = DO_Order.Rows[0]["待打压"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["待打压"].ToString());
                            dr["正在进行总数"] = num1;


                            dr["周转表"] = DO_Order.Rows[0]["周转表"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["周转表"].ToString());
                            dr["无法维修"] = DO_Order.Rows[0]["无法维修"].ToString();
                            num1 += Convert.ToInt32(DO_Order.Rows[0]["无法维修"].ToString());
                            dr["总数"] = num + num1;


                            dr["待出厂"] = DO_Order.Rows[0]["待出厂"].ToString();
                            dr["已出厂"] = DO_Order.Rows[0]["已出厂"].ToString();
                            dt.Rows.Add(dr);


                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "DN" + id;
                            dr["清洗完成"] = 0;
                            dr["维修完成"] = 0;
                            dr["完成总数"] = 0;
                            dr["待初测"] = 0;
                            dr["待清洗"] = 0;
                            dr["清洗中"] = 0;

                            dr["待维修"] = 0;
                            dr["维修中"] = 0;
                            dr["待检测"] = 0;
                            dr["待打压"] = 0;
                            dr["正在进行总数"] = 0;

                            dr["周转表"] = 0;
                            dr["无法维修"] = 0;
                            dr["总数"] = 0;
                            dr["待出厂"] = 0;
                            dr["已出厂"] = 0;
                            dt.Rows.Add(dr);
                        }


                        break;
                    default:
                        if (DO_Order.Rows.Count > 0)
                        {

                            if (DO_Order2.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr["口径"] = "DN" + id;
                                dr["清洗完成"] = Convert.ToInt32(DO_Order.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["清洗完成"].ToString());
                                dr["维修完成"] = Convert.ToInt32(DO_Order.Rows[0]["维修完成"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["维修完成"].ToString());
                                var num = Convert.ToInt32(dr["清洗完成"].ToString()) + Convert.ToInt32(dr["维修完成"].ToString());
                                dr["完成总数"] = num;
                                var num1 = 0;
                                dr["待初测"] = Convert.ToInt32(DO_Order.Rows[0]["待初测"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待初测"].ToString());

                                num1 += Convert.ToInt32(dr["待初测"].ToString());
                                dr["待清洗"] = Convert.ToInt32(DO_Order.Rows[0]["待清洗"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待清洗"].ToString());

                                num1 += Convert.ToInt32(dr["待清洗"].ToString());
                                dr["清洗中"] = Convert.ToInt32(DO_Order.Rows[0]["清洗中"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["清洗中"].ToString());
                                num1 += Convert.ToInt32(dr["清洗中"].ToString());
                                dr["待维修"] = Convert.ToInt32(DO_Order.Rows[0]["待维修"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待维修"].ToString());
                                num1 += Convert.ToInt32(dr["待维修"].ToString());
                                dr["维修中"] = Convert.ToInt32(DO_Order.Rows[0]["维修中"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["维修中"].ToString());

                                num1 += Convert.ToInt32(dr["维修中"].ToString());
                                dr["待检测"] = Convert.ToInt32(DO_Order.Rows[0]["待检测"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待检测"].ToString());
                                num1 += Convert.ToInt32(dr["待检测"].ToString());
                                dr["待打压"] = Convert.ToInt32(DO_Order.Rows[0]["待打压"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待打压"].ToString());
                                num1 += Convert.ToInt32(dr["待打压"].ToString());
                                dr["正在进行总数"] = num1;
                                dr["周转表"] = Convert.ToInt32(DO_Order.Rows[0]["周转表"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["周转表"].ToString());
                                num1 += Convert.ToInt32(dr["周转表"].ToString());
                                dr["无法维修"] = Convert.ToInt32(DO_Order.Rows[0]["无法维修"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["无法维修"].ToString());
                                num1 += Convert.ToInt32(dr["无法维修"].ToString());
                                dr["总数"] = num+num1;
                                dr["待出厂"] = Convert.ToInt32(DO_Order.Rows[0]["待出厂"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["待出厂"].ToString());
                                dr["已出厂"] = Convert.ToInt32(DO_Order.Rows[0]["已出厂"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["已出厂"].ToString());


                                dt.Rows.Add(dr);

                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr["口径"] = "DN" + id;
                                dr["清洗完成"] = DO_Order.Rows[0]["清洗完成"].ToString();
                                dr["维修完成"] = DO_Order.Rows[0]["维修完成"].ToString();
                                var num = Convert.ToInt32(DO_Order.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order.Rows[0]["维修完成"].ToString());
                                dr["完成总数"] = num;
                                var num1 = 0;
                                dr["待初测"] = DO_Order.Rows[0]["待初测"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["待初测"].ToString());
                                dr["待清洗"] = DO_Order.Rows[0]["待清洗"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["待清洗"].ToString());
                                dr["清洗中"] = DO_Order.Rows[0]["清洗中"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["清洗中"].ToString());
                                dr["待维修"] = DO_Order.Rows[0]["待维修"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["待维修"].ToString());
                                dr["维修中"] = DO_Order.Rows[0]["维修中"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["维修中"].ToString());
                                dr["待检测"] = DO_Order.Rows[0]["待检测"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["待检测"].ToString());
                                dr["待打压"] = DO_Order.Rows[0]["待打压"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["待打压"].ToString());
                                dr["正在进行总数"] = num1;


                                dr["周转表"] = DO_Order.Rows[0]["周转表"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["周转表"].ToString());
                                dr["无法维修"] = DO_Order.Rows[0]["无法维修"].ToString();
                                num1 += Convert.ToInt32(DO_Order.Rows[0]["无法维修"].ToString());
                                dr["总数"] = num + num1;


                                dr["待出厂"] = DO_Order.Rows[0]["待出厂"].ToString();
                                dr["已出厂"] = DO_Order.Rows[0]["已出厂"].ToString();
                                dt.Rows.Add(dr);
                            }

                        }
                        else
                        {

                            if (DO_Order2.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr["口径"] = "DN" + id;
                                dr["清洗完成"] = DO_Order2.Rows[0]["清洗完成"].ToString();
                                dr["维修完成"] = DO_Order2.Rows[0]["维修完成"].ToString();
                                var num = Convert.ToInt32(DO_Order2.Rows[0]["清洗完成"].ToString()) + Convert.ToInt32(DO_Order2.Rows[0]["维修完成"].ToString());
                                dr["完成总数"] = num;
                                var num1 = 0;
                                dr["待初测"] = DO_Order2.Rows[0]["待初测"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["待初测"].ToString());
                                dr["待清洗"] = DO_Order2.Rows[0]["待清洗"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["待清洗"].ToString());
                                dr["清洗中"] = DO_Order2.Rows[0]["清洗中"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["清洗中"].ToString());
                                dr["待维修"] = DO_Order2.Rows[0]["待维修"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["待维修"].ToString());
                                dr["维修中"] = DO_Order2.Rows[0]["维修中"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["维修中"].ToString());
                                dr["待检测"] = DO_Order2.Rows[0]["待检测"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["待检测"].ToString());
                                dr["待打压"] = DO_Order2.Rows[0]["待打压"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["待打压"].ToString());
                                dr["正在进行总数"] = num1;


                                dr["周转表"] = DO_Order2.Rows[0]["周转表"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["周转表"].ToString());
                                dr["无法维修"] = DO_Order2.Rows[0]["无法维修"].ToString();
                                num1 += Convert.ToInt32(DO_Order2.Rows[0]["无法维修"].ToString());
                                dr["总数"] = num + num1;


                                dr["待出厂"] = DO_Order2.Rows[0]["待出厂"].ToString();
                                dr["已出厂"] = DO_Order2.Rows[0]["已出厂"].ToString();
                                dt.Rows.Add(dr);


                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = "DN" + id;
                                dr["清洗完成"] = 0;
                                dr["维修完成"] = 0;
                                dr["完成总数"] = 0;
                                dr["待初测"] = 0;
                                dr["待清洗"] = 0;
                                dr["清洗中"] = 0;

                                dr["待维修"] = 0;
                                dr["维修中"] = 0;
                                dr["待检测"] = 0;
                                dr["待打压"] = 0;
                                dr["正在进行总数"] = 0;

                                dr["周转表"] = 0;
                                dr["无法维修"] = 0;
                                dr["总数"] = 0;
                                dr["待出厂"] = 0;
                                dr["已出厂"] = 0;
                                dt.Rows.Add(dr);
                            }
                        }


                        break;
                }
                #endregion
            }
            return dt;

        }
        //总表
        public static DataTable LoadScheduleSummary(string where, string type)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "select ROW_NUMBER() OVER (ORDER BY a.RID asc) AS RowNumber ,* from [tk_RepairCard] a ";
            if (type == "CardType2")
            {
                sql = "select ROW_NUMBER() OVER (ORDER BY a.RID asc) AS RowNumber ,* from [tk_UTRepairCard] a ";
            }

            if (where != "")
                sql += " where 1=1 " + where;
            DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");

            if (DO_Order.Rows.Count == 0)
            {


                return null;
            }
            else
            {
                DO_Order.Columns.Add("y", typeof(System.String));
                DO_Order.Columns.Add("m", typeof(System.String));
                DO_Order.Columns.Add("d", typeof(System.String));

                DO_Order.Columns.Add("归属", typeof(System.String));
                DO_Order.Columns.Add("附件", typeof(System.String));
                DO_Order.Columns.Add("检测结果", typeof(System.String));
                DO_Order.Columns.Add("初测状态", typeof(System.String));
                DO_Order.Columns.Add("次数", typeof(System.String));
                DO_Order.Columns.Add("维修记录", typeof(System.String));
                DO_Order.Columns.Add("清洗维修状态", typeof(System.String));
                DO_Order.Columns.Add("出厂前检测状态", typeof(System.String));
                DO_Order.Columns.Add("打压状态", typeof(System.String));
                DO_Order.Columns.Add("铅封号", typeof(System.String));
                DO_Order.Columns.Add("完成情况", typeof(System.String));
                DO_Order.Columns.Add("零件", typeof(System.String));
                DO_Order.Columns.Add("说明", typeof(System.String));
                DO_Order.Columns.Add("地点", typeof(System.String));
                DO_Order.Columns.Add("情况", typeof(System.String));
                for (int i = 0; i < DO_Order.Rows.Count; i++)
                {
                    if (DO_Order.Rows[i]["S_Date"].ToString() != "" && DO_Order.Rows[i]["S_Date"] != null)
                    {
                        DO_Order.Rows[i]["y"] = Convert.ToDateTime(DO_Order.Rows[i]["S_Date"]).ToString("yyyy");
                        DO_Order.Rows[i]["m"] = Convert.ToDateTime(DO_Order.Rows[i]["S_Date"]).ToString("MM");
                        DO_Order.Rows[i]["d"] = Convert.ToDateTime(DO_Order.Rows[i]["S_Date"]).ToString("dd");
                    }
                    if (Convert.ToInt32(DO_Order.Rows[i]["State"].ToString()) >= 3)
                    {
                        DO_Order.Rows[i]["初测状态"] = "已完成";
                    }
                    else
                    {
                        DO_Order.Rows[i]["初测状态"] = "未完成";
                    }
                    string sqlsum = "select count(*) from tk_CheckData where  RID='" + DO_Order.Rows[i]["RID"].ToString() + "'";
                    var sum = SQLBase.FillTable(sqlsum, "FlowMeterDBCnn");
                    if (sum.Rows.Count > 0)
                    {
                        DO_Order.Rows[i]["次数"] = sum.Rows[0][0];
                    }
                    else
                    {
                        DO_Order.Rows[i]["次数"] = "0";
                    }

                    if (Convert.ToInt32(DO_Order.Rows[i]["State"].ToString()) >= 7)
                    {
                        DO_Order.Rows[i]["清洗维修状态"] = "已完成";
                    }
                    else
                    {
                        DO_Order.Rows[i]["清洗维修状态"] = "未完成";
                    }

                    if (Convert.ToInt32(DO_Order.Rows[i]["State"].ToString()) >= 9)
                    {
                        DO_Order.Rows[i]["出厂前检测状态"] = "已完成";
                    }
                    else
                    {
                        DO_Order.Rows[i]["出厂前检测状态"] = "未完成";
                    }
                    if (Convert.ToInt32(DO_Order.Rows[i]["State"].ToString()) >= 10)
                    {
                        DO_Order.Rows[i]["打压状态"] = "已完成";
                    }
                    else
                    {
                        DO_Order.Rows[i]["打压状态"] = "未完成";
                    }

                    string sql2 = "select name from [tk_ConfigState] where Type='RepairState' and StateId='" + DO_Order.Rows[i]["State"] + "'";
                    DataTable state = SQLBase.FillTable(sql2, "FlowMeterDBCnn");

                    DO_Order.Rows[i]["完成情况"] = state.Rows[0][0];


                    string sql3 = "select *  from [tk_RepairDevice] where RID='" + DO_Order.Rows[i]["RID"].ToString() + "'";
                    DataTable dt = SQLBase.FillTable(sql3, "FlowMeterDBCnn");

                    if (dt.Rows.Count > 0)
                    {
                        var DeviceName = "";

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (j == dt.Rows.Count - 1)
                                DeviceName += dt.Rows[j]["DeviceName"];
                            else
                                DeviceName += dt.Rows[j]["DeviceName"] + ",";


                        }
                        DO_Order.Rows[i]["零件"] = DeviceName;

                    }
                    else
                    {
                        DO_Order.Rows[i]["零件"] = "无";
                    }

                }


            }
            return DO_Order;

        }
        //查询加载报价单总价 
        public static DataTable LoadCheckDataSummary(string where)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string sql = "select * from [tk_CheckData] a left join (select RID,MeterID as M ,CertifID from [tk_RepairCard]) b on a.RID=b.RID";
            if (where != "")
                sql += " where " + where;
            DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");

            if (DO_Order.Rows.Count == 0)

                return null;




            return DO_Order;

        }
        public static DataTable LoadCheckData2Summary(string where)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");

            string sql = "select * from [tk_CheckData2] a left join (select RID,MeterID as M,CertifID from [tk_UTRepairCard]) b on a.RID=b.RID";
            if (where != "")
                sql += " where " + where;
            DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");

            if (DO_Order.Rows.Count == 0)
            {


                return null;
            }
            else
            {
                DO_Order.Columns.Add("测试条件A", typeof(System.String));
                DO_Order.Columns.Add("误差A", typeof(System.String));
                DO_Order.Columns.Add("A1ER", typeof(System.String));
                DO_Order.Columns.Add("A2ER", typeof(System.String));
                DO_Order.Columns.Add("A3ER", typeof(System.String));
                DO_Order.Columns.Add("A4ER", typeof(System.String));
                DO_Order.Columns.Add("A5ER", typeof(System.String));
                DO_Order.Columns.Add("A6ER", typeof(System.String));
                DO_Order.Columns.Add("AVER", typeof(System.String));
                DO_Order.Columns.Add("AHVER", typeof(System.String));
                DO_Order.Columns.Add("测试条件B", typeof(System.String));
                DO_Order.Columns.Add("误差B", typeof(System.String));
                DO_Order.Columns.Add("B1ER", typeof(System.String));
                DO_Order.Columns.Add("B2ER", typeof(System.String));
                DO_Order.Columns.Add("B3ER", typeof(System.String));
                DO_Order.Columns.Add("B4ER", typeof(System.String));
                DO_Order.Columns.Add("B5ER", typeof(System.String));
                DO_Order.Columns.Add("B6ER", typeof(System.String));
                DO_Order.Columns.Add("BVER", typeof(System.String));
                DO_Order.Columns.Add("BHVER", typeof(System.String));
            }



            return DO_Order;

        }

        public static DataTable LoadRepairInfoSummary(string where, string type)
        {
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("FlowMeterDBCnn");
            string sql = "select * from [tk_RepairInfo] a left join (select RID,MeterID as M,Manufacturer,ModelType,Model from [tk_RepairCard]) b on a.RID=b.RID";
            if (type == "CardType2" || type == "超声波")
            {
                sql = "select * from [tk_RepairInfo] a left join (select RID,MeterID as M,Manufacturer,ModelType,Model from [tk_UTRepairCard]) b on a.RID=b.RID";
            }

            if (where != "")
                sql += " where " + where;
            DataTable DO_Order = SQLBase.FillTable(sql, "FlowMeterDBCnn");

            if (DO_Order.Rows.Count == 0)
            {


                return null;
            }
            else
            {

                DO_Order.Columns.Add("DeviceName", typeof(System.String));
                DO_Order.Columns.Add("DeviceType", typeof(System.String));
                DO_Order.Columns.Add("Measure", typeof(System.String));
                DO_Order.Columns.Add("Num", typeof(System.String));
                for (int i = 0; i < DO_Order.Rows.Count; i++)
                {
                    string sql2 = "select *  from [tk_RepairDevice] where RID='" + DO_Order.Rows[i]["RID"].ToString() + "'";
                    DataTable dt = SQLBase.FillTable(sql2, "FlowMeterDBCnn");

                    if (dt.Rows.Count > 0)
                    {
                        var DeviceName = ""; var DeviceType = ""; var Measure = ""; var Num = "";

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DeviceName += dt.Rows[j]["DeviceName"] + ",";
                            DeviceType += dt.Rows[j]["DeviceType"] + ",";
                            Measure += dt.Rows[j]["Measure"] + ",";
                            Num += dt.Rows[j]["Num"] + ",";

                        }
                        DO_Order.Rows[i]["DeviceName"] = DeviceName;
                        DO_Order.Rows[i]["DeviceType"] = DeviceType;
                        DO_Order.Rows[i]["Measure"] = Measure;
                        DO_Order.Rows[i]["Num"] = Num;
                    }



                }

            }


            return DO_Order;

        }
        #endregion







    }
}
