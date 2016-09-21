using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace TECOCITY_BGOI
{
    public class MandateInfoPro
    {
        public static DataTable GetTestType()
        {
            string sql = "select TID,ChildTestType FROM tk_ConfigTestType";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }
        public static DataTable GetTestType(string FatherTestType)
        {
            string sql = "select TID,ChildTestType FROM tk_ConfigTestType where FatherTestType='" + FatherTestType + "'";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetFatherTestType()
        {
            string sql = "select distinct FatherTestType from tk_ConfigTestType";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetTestItems(string TID)
        {
            string sql = "select ItemID,ItemContent from tk_ConfigTestingItem where TID=" + TID;
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetConfigInfo(string condition, string TaskType)
        {
            string sql = "";
            if (TaskType == "SampleName")
                sql = "select SampleID,SampleName from tk_ConfigSampleName where SampleName like '%" + condition + "%'";
            if (TaskType == "Specification")
                sql = "select ID,Standard as Specification FROM tk_ConfigStandard where Standard like '%" + condition + "%'";
            if (TaskType == "SampleState")
                sql = "select ID,text as SampleState FROM tk_ConfigState where type = 'SampleState'";
            if (TaskType == "ClienName")
                sql = "select distinct UserName as ClienName from ClientInfo where UserName like '%" + condition + "%'";
            if (TaskType == "testType")
                sql = "select ID,text  FROM tk_ConfigState where type = 'testType'";
            if (TaskType == "Provinces")
                sql = "select ID,text as Provinces FROM tk_ConfigState where type = 'Provinces' and text like '%" + condition + "%'";
            if (TaskType == "Manufacturer")
                sql = "select Manufacturer FROM tk_ConfigManufacturer where Manufacturer like '%" + condition + "%'";
            if (TaskType == "TestingBasi")
                sql = "select TestingBasis TestingBasi  from tk_ConfigBasis where TestingBasis like '%" + condition + "%' order by TestingBasis";
            if (TaskType == "TestingItem")
                sql = "select ItemContent TestingItem from tk_ConfigTestingItem where ItemContent like '%" + condition + "%' order by ItemContent";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetState(string type)
        {
            string sql = "select Id,Text from tk_ConfigState where type='" + type + "'";
            return SQLBase.FillTable(sql);
        }

        public static DataTable GetPayId(string PayCompany)
        {
            string sql = "select PayId from PayInfo b where PayType='2' and (select isnull(sum(Amount),0) from ConsumptionInfo a where  a.PayId=b.PayId and type!='2')<PayMoney and PayCompany='" + PayCompany + "'";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static double GetPayMoney(string ClienName)
        {
            string sql = "select Money from ClientPayInfo where ClienName='" + ClienName + "'";
            double Amount = Convert.ToDouble(SQLBase.ExecuteScalar(sql));
            return Amount;
        }

        public static double GetMandateCharge(string YYCode)
        {
            string sql = "select Charge from MandateInfo where YYCode='" + YYCode + "'";
            double Charge = Convert.ToDouble(SQLBase.ExecuteScalar(sql));
            sql = "select isnull(sum(Amount),0) from ConsumptionInfo where  YYCode='" + YYCode + "'";
            double Amount = Convert.ToDouble(SQLBase.ExecuteScalar(sql));
            return Charge - Amount;
        }

        public static DataTable GetMandateChargeInfo(string where)
        {
            string sql = "select a.YYCode,MCode,charge-isnull(b.Amount,0) charge,ClienName from mandateInfo a left join (select YYCode,isnull(sum(Amount),0)Amount from ConsumptionInfo group by yycode)b on a.YYCode = b.YYCode where charge-isnull(b.Amount,0)>0 and MCode<>'' and state<>-1" + where;
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetPayCompany()
        {
            string sql = "select distinct clienName from (select  clienName from mandateInfo union all select distinct PayCompany from tk_ConfigPayCompany)A";
            return SQLBase.FillTable(sql);
        }

        public static string GetPayId()
        {
            string NewTime = DateTime.Now.ToString("yyyyMMdd");
            string YYCode = NewTime + "JF000";
            string sql = "select count(*) from TK_ConfingIDRecord where type='Pay' and OneWord='JF' and YMD ='" + NewTime + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            string MaXValue = "1";
            if (count > 0)
            {
                sql = "select MaxValue from TK_ConfingIDRecord where type='Pay' and OneWord='JF' and YMD ='" + NewTime + "'";
                DataTable dt = SQLBase.FillTable(sql);
                MaXValue = (int.Parse(dt.Rows[0]["MaxValue"].ToString()) + 1).ToString();
            }

            return YYCode.Substring(0, 13 - MaXValue.Length) + MaXValue;
        }
        public static string GetYYCode()
        {
            string NewTime = DateTime.Now.ToString("yyyyMMdd");
            string YYCode = "L" + NewTime + "000";
            string sql = "select count(*) from TK_ConfingIDRecord where type='mandate' and OneWord='L' and YMD ='" + NewTime + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            string MaXValue = "1";
            if (count > 0)
            {
                sql = "select MaxValue from TK_ConfingIDRecord where type='mandate' and OneWord='L' and YMD ='" + NewTime + "'";
                DataTable dt = SQLBase.FillTable(sql);
                MaXValue = (int.Parse(dt.Rows[0]["MaxValue"].ToString()) + 1).ToString();
            }

            return YYCode.Substring(0, 12 - MaXValue.Length) + MaXValue;


        }

        public static string GetMCode(string type)
        {
            string NowTime = DateTime.Now.ToString("yyyy");
            string m;
            if (type == "F" || type == "B")
            {
                switch (DateTime.Now.ToString("MM"))
                {
                    case "10":
                        m = "O";
                        break;
                    case "11":
                        m = "N";
                        break;
                    case "12":
                        m = "D";
                        break;
                    default:
                        m = DateTime.Now.ToString("MM").Substring(1);
                        break;
                }
            }
            else
            {
                m = "Q";
            }
            string sql = "select count(*) from tk_ConfigMIDRecord where Year='" + NowTime + "' and Type='" + type + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            string MaxValue = "1";
            if (count > 0)
            {
                sql = "select MaxValue from tk_ConfigMIDRecord where Year='" + NowTime + "' and Type='" + type + "'";
                MaxValue = ((int)SQLBase.ExecuteScalar(sql) + 1).ToString();
            }
            if (MaxValue.Length < 4)
            {
                MaxValue = "000".Substring(0, 3 - MaxValue.Length) + MaxValue;
            }
            string MCode = DateTime.Now.ToString("yyyy") + type + m + MaxValue;
            return MCode;

        }

        public static PayInfo GetPayInfo(string PayId)
        {
            string sql = "select PayId, PaymentMethod, PayMoney, hkhorzph, PayCompany, PayType, SFKP, RendingReason, Remark, PayTime, CreateTime, CreateUser, Validate from PayInfo where PayId='" + PayId + "'";
            DataTable dt = SQLBase.FillTable(sql);
            PayInfo payInfo = new PayInfo();
            return GSqlSentence.SetTValueD<PayInfo>(payInfo, dt.Rows[0]);
        }


        public static bool UpdateYYCode(string YYCode)
        {
            string YYMMDD = YYCode.Substring(1, 8);
            string sql = "select count(*) from TK_ConfingIDRecord where type='mandate' and OneWord='L' ";
            int count = (int)SQLBase.ExecuteScalar(sql);
            if (count > 0)
            {
                sql = "update TK_ConfingIDRecord set MaxValue =" + YYCode.Substring(9, 3) + ", YMD ='" + YYMMDD + "'  where type='mandate' and OneWord='L'";
            }
            else
            {
                sql = "insert into TK_ConfingIDRecord(YMD, OneWord, MaxValue, Type) values ('" + YYMMDD + "','L'," + YYCode.Substring(9, 3) + ",'mandate')";
            }
            return SQLBase.ExecuteNonQuery(sql) > 0;
        }


        public static bool UpdateMcode(string type, string MCode)
        {
            string year = DateTime.Now.ToString("yyyy");
            string MaxValue;
            if (type == "F" || type == "B")
            {
                MaxValue = MCode.Substring(6);
            }
            else
            {
                MaxValue = MCode.Substring(7);
            }
            string sql = "select count(*) from tk_ConfigMIDRecord where Year='" + year + "' and type ='" + type + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            if (count > 0)
            {
                sql = "update tk_ConfigMIDRecord set MaxValue =" + MaxValue + "  where Year='" + year + "' and type='" + type + "'";
            }
            else
            {
                sql = "insert into tk_ConfigMIDRecord(year,type,MaxValue) values ('" + year + "','" + type + "'," + MaxValue + ")";
            }

            if (SQLBase.ExecuteNonQuery(sql) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool UpdatePayId(string PayId)
        {
            try
            {
                string YYMMDD = PayId.Substring(0, 8);
                string sql = "select count(*) from TK_ConfingIDRecord where type='Pay' and OneWord='JF'";
                int count = (int)SQLBase.ExecuteScalar(sql);
                if (count > 0)
                {
                    sql = "update TK_ConfingIDRecord set MaxValue =" + PayId.Substring(10, 3) + ", YMD ='" + YYMMDD + "'   where type='Pay' and OneWord='JF'";
                }
                else
                    sql = "insert into TK_ConfingIDRecord(YMD, OneWord, MaxValue, Type) values ('" + YYMMDD + "','JF'," + PayId.Substring(10, 3) + ",'Pay')";
                return SQLBase.ExecuteNonQuery(sql) > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public static DataTable GetBasisAndItem(string SampleName)
        {
            string sql = " select c.TestingBasisId,c.TestingBasis,b.ItemId,b.ItemContent from  tk_ConfigSampleName a left join tk_ConfigTestingItem b on a.ItemID LIKE '%,'+convert(varchar,b.ItemId)+',%' left join tk_ConfigBasis c on a.BasisID like '%,'+convert(varchar,c.TestingBasisId)+',%' where a.SampleName='" + SampleName + "'";

            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static List<TestItem> GetItemIDs(string ItemContent, string YYCode)
        {
            string sql = "select ItemId from tk_ConfigTestingItem where '," + ItemContent + ",' like '%,'+ItemContent+',%'";
            DataTable dt = SQLBase.FillTable(sql);
            TestItem testItem = null;
            List<TestItem> listItem = new List<TestItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                testItem = new TestItem();
                testItem.YYCode = YYCode;
                testItem.ItemID = Convert.ToInt32(dt.Rows[i][0]);
                listItem.Add(testItem);
            }
            return listItem;
        }

        public static DataTable GetClienInfo(string ClienName)
        {
            string sql = "select distinct UserName,Tel,Address,PostalCode from ClientInfo where UserName = '" + ClienName + "'";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }


        public static bool InsertMandate(MandateInfo mandate, List<SampleInfo> sampleList, ref string err)
        {
            string MandateSql = GSqlSentence.GetInsertInfoByD<MandateInfo>(mandate, "MandateInfo");
            string sampleSql = GSqlSentence.GetInsertByList<SampleInfo>(sampleList, "SampleInfo");
            List<TestItem> testItemList = GetItemIDs(mandate.TestingItems, mandate.YYCode);
            string testItemSql = GSqlSentence.GetInsertByList<TestItem>(testItemList, "TestItem");
            string sql = "select count(*) from MandateInfo where YYCode='" + mandate.YYCode + "'";
            try
            {
                if ((int)SQLBase.ExecuteScalar(sql) > 0)
                {
                    err = "该委托单已添加";
                    return false;
                }


                if (SQLBase.ExecuteNonQuery(MandateSql) > 0)
                {
                    if (!UpdateYYCode(mandate.YYCode))
                    {
                        err = "预约号更新失败";
                        return false;
                    }

                    if (sampleList.Count > 0)
                    {
                        if (SQLBase.ExecuteNonQuery(sampleSql) <= 0)
                        {
                            err = "样品信息添加失败";
                            return false;
                        }
                    }
                    if (testItemList.Count > 0)
                    {
                        if (SQLBase.ExecuteNonQuery(testItemSql) <= 0)
                        {
                            err = "检测项目添加失败";
                            return false;
                        }
                    }
                    string content = "添加委托单信息";
                    if (!insertLog(mandate.YYCode, content))
                    {
                        err = "更新日志失败";
                        return false;
                    }

                    return true;
                }
                err = "委托信息添加失败";
            }
            catch (Exception e)
            {
                err = e.Message;
            }


            return false;
        }

        public static bool UpdateMandate(MandateInfo mandate, List<SampleInfo> sampleList, ref string err)
        {
            string MandateSql = GSqlSentence.GetUpdateInfoByD(mandate, "YYCode", "MandateInfo");
            if (mandate.MCode != "" && mandate.MCode != null)
            {
                string MCode = mandate.MCode;
                int number = 0;
                for (int i = 0; i < sampleList.Count; i++)
                {
                    string sampleCode = "";
                    if (sampleList[i].Number > 1)
                    {
                        string strartStr = (number + 1).ToString();
                        string endStr = (number + sampleList[i].Number).ToString();
                        if (strartStr.Length < 2)
                            strartStr = "0" + strartStr;
                        if (endStr.Length < 2)
                            endStr = "0" + endStr;
                        sampleCode = MCode + "-" + strartStr + "-" + endStr;
                        number = Convert.ToInt32(endStr);
                    }
                    else
                    {
                        if ((number + 1).ToString().Length > 2)
                        {
                            sampleCode = MCode + "-" + (number + 1);
                        }
                        else
                        {
                            string result = "00" + (number + 1);
                            sampleCode = MCode + "-" + result.Substring(result.Length - 2, 2);
                        }
                        number += 1;
                    }
                    sampleList[i].SampleCode = sampleCode;
                }
            }

            string sampleSql = GSqlSentence.GetInsertByList<SampleInfo>(sampleList, "SampleInfo");

            List<TestItem> testItemList = GetItemIDs(mandate.TestingItems, mandate.YYCode);
            string testItemSql = GSqlSentence.GetInsertByList<TestItem>(testItemList, "TestItem");
            string sql = "";
            if (SQLBase.ExecuteNonQuery(MandateSql) > 0)
            {
                sql = "delete SampleInfo where YYCode='" + mandate.YYCode + "'";
                if (SQLBase.ExecuteNonQuery(sql) >= 0)
                {
                    if (sampleList.Count > 0)
                    {
                        if (SQLBase.ExecuteNonQuery(sampleSql) <= 0)
                        {
                            err = "样品信息添加失败";
                            return false;
                        }
                    }

                }
                sql = "delect TestItem where YYCode='" + mandate.YYCode + "'";
                if (SQLBase.ExecuteNonQuery(sql) >= 0)
                {
                    if (testItemList.Count > 0)
                    {
                        if (SQLBase.ExecuteNonQuery(testItemSql) <= 0)
                        {
                            err = "检测项目添加失败";
                            return false;
                        }
                    }
                }
                string content = "修改委托单信息";
                if (!insertLog(mandate.YYCode, content))
                {
                    err = "更新日志失败";
                    return false;
                }

                return true;
            }

            err = "委托信息更新失败";


            return false;
        }



        public static UIDataTable getMandateGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@PageSize",a_intPageSize.ToString()),
                new SqlParameter("@StartIndex",(a_intPageIndex * a_intPageSize).ToString()),
                new SqlParameter("@Where",where)
            };
            DataSet DO_Order = SQLBase.FillDataSet("GetMandateGrid", CommandType.StoredProcedure, para);
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

        public static MandateInfo GetMandateInfo(string YYCode)
        {
            string sql = "select YYCode, MCode, ClienName, ClienTel, ClienAddress, PostalCode, ProName, SourceWay, SamplePeople, SampleTime, Manufacturer, Document, Secrecy, TestingBasis, TestingItems, DemandFinishDate, PickupMethod, MailingAddress, Remark, SampleDisposition, MandatePeople, AcceptTime, AcceptPeople, Charge, SReceiveState, State, CreateTime, CreateUser, Validate,RepealReason, TestType, Provinces from MandateInfo where YYCode='" + YYCode + "'";
            DataTable dt = SQLBase.FillTable(sql);
            MandateInfo mandateInfo = new MandateInfo();
            if (dt.Rows.Count > 0)
            {
                mandateInfo.YYCode = dt.Rows[0]["YYCode"].ToString();
                mandateInfo.MCode = dt.Rows[0]["MCode"].ToString();
                mandateInfo.ClienName = dt.Rows[0]["ClienName"].ToString();
                mandateInfo.ClienTel = dt.Rows[0]["ClienTel"].ToString();
                mandateInfo.ClienAddress = dt.Rows[0]["ClienAddress"].ToString();
                mandateInfo.PostalCode = dt.Rows[0]["PostalCode"].ToString();
                mandateInfo.ProName = dt.Rows[0]["ProName"].ToString();
                mandateInfo.SourceWay = dt.Rows[0]["SourceWay"].ToString();
                mandateInfo.SamplePeople = dt.Rows[0]["SamplePeople"].ToString();
                mandateInfo.SampleTime = Convert.ToDateTime(dt.Rows[0]["SampleTime"]).ToString("yyyy-MM-dd");
                mandateInfo.Manufacturer = dt.Rows[0]["Manufacturer"].ToString();
                mandateInfo.Document = dt.Rows[0]["Document"].ToString();
                mandateInfo.Secrecy = dt.Rows[0]["Secrecy"].ToString();
                mandateInfo.TestingBasis = dt.Rows[0]["TestingBasis"].ToString();
                mandateInfo.TestingItems = dt.Rows[0]["TestingItems"].ToString();
                mandateInfo.DemandFinishDate = Convert.ToDateTime(dt.Rows[0]["DemandFinishDate"]).ToString("yyyy-MM-dd");
                mandateInfo.PickupMethod = dt.Rows[0]["PickupMethod"].ToString();
                mandateInfo.MailingAddress = dt.Rows[0]["MailingAddress"].ToString();
                mandateInfo.Remark = dt.Rows[0]["Remark"].ToString();
                mandateInfo.SampleDisposition = dt.Rows[0]["SampleDisposition"].ToString();
                mandateInfo.MandatePeople = dt.Rows[0]["MandatePeople"].ToString();
                mandateInfo.Charge = Convert.ToDouble(dt.Rows[0]["Charge"]);
                mandateInfo.AcceptTime = dt.Rows[0]["AcceptTime"].ToString();
                mandateInfo.AcceptPeople = dt.Rows[0]["AcceptPeople"].ToString();
                mandateInfo.SReceiveState = Convert.ToInt32(dt.Rows[0]["SReceiveState"]);
                mandateInfo.State = Convert.ToInt32(dt.Rows[0]["State"]);
                mandateInfo.CreateTime = dt.Rows[0]["CreateTime"].ToString();
                mandateInfo.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                mandateInfo.Validate = dt.Rows[0]["Validate"].ToString();
                mandateInfo.RepealReason = dt.Rows[0]["RepealReason"].ToString();
                mandateInfo.TestType = dt.Rows[0]["TestType"].ToString();
                mandateInfo.Provinces = dt.Rows[0]["Provinces"].ToString();
            }
            return mandateInfo;
        }

        public static DataTable GetSampleInfo(string YYCode)
        {
            string sql = "select SampleName,Specification,Number,Brand,SampleCode,SampleState,TestingBasis,TestingItem from SampleInfo where YYCode ='" + YYCode + "' order by serialNumber";

            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static string GetTestItemIDs(string YYCode)
        {
            string sql = "select ItemID from TestItem where YYCode ='" + YYCode + "'";
            DataTable dt = SQLBase.FillTable(sql);
            string TestItemIDs = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    TestItemIDs = dt.Rows[i][0].ToString();
                }
                else
                    TestItemIDs += "," + dt.Rows[i][0].ToString();
            }

            return TestItemIDs;
        }


        public static bool SaveAccept(string YYCode, string MCode, string AcceptPeople, string type, ref string err)
        {
            string sql = "update  MandateInfo set MCode='" + MCode + "',AcceptPeople='" + AcceptPeople + "',AcceptTime='" + DateTime.Now.ToString() + "',state=1 where YYCode='" + YYCode + "'";
            if (SQLBase.ExecuteNonQuery(sql) > 0)
            {
                if (!UpdateMcode(type, MCode))
                {
                    err = "委托单号更新失败";
                    return false;
                }
                //sql = "update sampleInfo set sampleCode='" + MCode + ".'+right('00',2-len(convert(varchar(2),serialNumber)))+convert(varchar(2),serialNumber) where YYCode='" + YYCode + "'";
                sql = "select YYCode,Number,serialNumber from SampleInfo where YYCode ='" + YYCode + "' order by serialNumber";
                DataTable dt = SQLBase.FillTable(sql);
                //未完
                int number = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sampleCode = "";
                    if ((int)dt.Rows[i]["Number"] > 1)
                    {
                        string strartStr = (number + 1).ToString();
                        string endStr = (number + (int)dt.Rows[i]["Number"]).ToString();
                        if (strartStr.Length < 2)
                            strartStr = "0" + strartStr;
                        if (endStr.Length < 2)
                            endStr = "0" + endStr;
                        sampleCode = MCode + "-" + strartStr + "-" + endStr;
                        number = Convert.ToInt32(endStr);
                    }
                    else
                    {
                        if ((number + 1).ToString().Length > 2)
                        {
                            sampleCode = MCode + "-" + (number + 1);
                        }
                        else
                        {
                            string result = "00" + (number + 1);
                            sampleCode = MCode + "-" + result.Substring(result.Length - 2, 2);
                        }
                        number += 1;
                    }
                    sql = "update sampleInfo set sampleCode='" + sampleCode + "' where YYCode='" + YYCode + "' and serialNumber=" + dt.Rows[i]["serialNumber"];
                    if (SQLBase.ExecuteNonQuery(sql) <= 0)
                    {
                        err = "样品编号更新失败";
                        return false;
                    }
                }

                sql = "insert into TaskOrder(MId,CreateTime,CreateUser,state) values('" + MCode + "','" + DateTime.Now.ToString() + "','" + GAccount.GetAccountInfo().UserName + "',9)";
                if (SQLBase.ExecuteNonQuery(sql) > 0)
                {
                    string content = "受理委托单并创建任务单";
                    if (!insertLog(YYCode, content))
                    {
                        err = "日志更新失败";
                        return false;
                    }
                    return true;
                }
                else
                {
                    err = "任务单创建失败";
                    return false;
                }

            }
            err = "受理失败";
            return false;
        }

        public static DataTable GetMandateInfoByYYCode(string YYCode)
        {
            string sql = "select  MCode, ClienName, ClienTel, ClienAddress, PostalCode, ProName, SourceWay, SamplePeople, SampleTime, Manufacturer, Document, Secrecy, TestingBasis, TestingItems, DemandFinishDate,Charge, PickupMethod, MailingAddress, Remark, SampleDisposition from MandateInfo where yycode='" + YYCode + "'";
            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static DataTable GetSampleInfoByYYCode(string YYCode)
        {
            string sql = "select SampleName, Specification, Number, Brand, SampleCode, SampleState from SampleInfo where yycode='" + YYCode + "' order by serialNumber";
            return SQLBase.FillTable(sql);
        }

        public static bool IsPayState(string YYCode)
        {
            string sql = "select count(*) from ConsumptionInfo where type=0 and  YYCode='" + YYCode + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            if (count > 0)
                return false;
            return true;
        }
        public static bool SavePayInfo(PayInfo payInfo, List<ConsumptionInfo> list, double yk, ref string err)
        {
            if (payInfo != null)
            {
                string paySql = GSqlSentence.GetInsertInfoByD<PayInfo>(payInfo, "PayInfo");
                if (SQLBase.ExecuteNonQuery(paySql) <= 0)
                {
                    err = "缴费信息添加失败";
                    return false;
                }
                if (!UpdatePayId(payInfo.PayId))
                {
                    err = "缴费单号更新失败";
                    return false;
                }

            }
            string conSql = GSqlSentence.GetInsertByList<ConsumptionInfo>(list, "ConsumptionInfo");
            if (SQLBase.ExecuteNonQuery(conSql) <= 0)
            {
                err = "消费信息添加失败";
                return false;
            }
            if (yk > 0)
            {
                string sql = "update ClientPayInfo set Money = Money-" + yk + " where ClienName ='" + payInfo.PayCompany + "'";
                if (SQLBase.ExecuteNonQuery(sql) <= 0)
                {
                    err = "更新余额失败";
                    return false;
                }
            }
            string YYCode = "";
            string Content = "";
            for (int i = 0; i < list.Count; i++)
            {
                YYCode += list[i].YYCode + ",";
                Content += "缴费，缴费状态为" + list[i].Type + ",";
            }
            if (!insertLog(YYCode.Substring(0, YYCode.Length - 1), Content.Substring(0, Content.Length - 1)))
            {
                err = "更新日志失败";
                return false;
            }
            return true;
        }

        public static bool UpdatePayInfo(PayInfo payInfo, string YYCode, ref string err)
        {
            string sql = "select PayType from PayInfo where payId='" + payInfo.PayId + "'";
            string pType = SQLBase.ExecuteScalar(sql).ToString();

            string paySql = GSqlSentence.GetUpdateInfoByD<PayInfo>(payInfo, "PayId", "PayInfo");
            if (SQLBase.ExecuteNonQuery(paySql) <= 0)
            {
                err = "缴费信息更新失败";
                return false;
            }
            sql = "update ConsumptionInfo set type=" + payInfo.PayType + " where PayId='" + payInfo.PayId + "'";
            if (SQLBase.ExecuteNonQuery(sql) <= 0)
            {
                err = "消费信息更新失败";
                return false;
            }

            if (pType != payInfo.PayType && payInfo.PayType == "2")
            {
                sql = "select ClienName from ClientPayInfo where ClienName = '" + payInfo.PayCompany + "'";
                DataTable dt = SQLBase.FillTable(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "update ClientPayInfo set Money = Money+" + payInfo.PayMoney + " where ClienName = '" + payInfo.PayCompany + "'";
                    if (SQLBase.ExecuteNonQuery(sql) <= 0)
                        return false;
                }
                else
                {
                    sql = "insert into ClientPayInfo(ClienName,Money) values('" + payInfo.PayCompany + "'," + payInfo.PayMoney + ")";
                    if (SQLBase.ExecuteNonQuery(sql) <= 0)
                        return false;

                }
            }

            return true;
        }

        public static UIDataTable GetPayGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@PageSize",a_intPageSize),
                new SqlParameter("@StartIndex",(a_intPageIndex * a_intPageSize).ToString()),
                new SqlParameter("@Where",where)
            };
            DataSet DO_Order = SQLBase.FillDataSet("GetPayGrid", CommandType.StoredProcedure, para);
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

        public static DataTable GetLedger(string where)
        {
            if (where == "")
                where = " and year(SampleTime)=year(getdate())";

            string sql = "select b.MCode,ClienName,Manufacturer,ProName,SamplePeople,ClienTel,CONVERT(varchar(100),SampleTime, 23)SampleTime,SampleName,Specification,(select sum(number) from SampleInfo where yycode=a.yycode )znumber,number,charge,isnull(Amount,0)Amount, charge-isnull(Amount,0)ws  from mandateInfo b left join SampleInfo a on a.YYCode=b.YYCode left join (select YYCode,MCode,Sum(Amount)Amount from ConsumptionInfo where type=0 group by YYCode,MCode)c on a.YYCode = c.YYCode where 1=1" + where;

            DataTable dt = SQLBase.FillTable(sql);
            return dt;
        }

        public static string GetLedgerMoneyAnaNumber(string where)
        {
            if (where == "")
                where = " and year(SampleTime)=year(getdate())";
            string sql = "select sum(number)number from mandateInfo b left join SampleInfo a on a.YYCode=b.YYCode where 1=1" + where;

            string number = SQLBase.ExecuteScalar(sql).ToString();

            sql = "select sum(charge)charge from mandateInfo b where 1=1 " + where;

            string charge = SQLBase.ExecuteScalar(sql).ToString();
            return number + "," + charge;

        }


        public static bool insertLog(string YYCode, string content)
        {
            tk_Log log = null;
            List<tk_Log> list = new List<tk_Log>();
            string[] YYCodes = YYCode.Split(',');
            string[] Contents = content.Split(',');
            for (int i = 0; i < YYCodes.Length; i++)
            {
                log = new tk_Log()
                {
                    ID = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss")),
                    LogTime = DateTime.Now,
                    YYCode = YYCodes[i],
                    Content = Contents[i],
                    Actor = GAccount.GetAccountInfo().UserName,
                    Unit = GAccount.GetAccountInfo().UnitName
                };
                list.Add(log);
            }
            string logSql = GSqlSentence.GetInsertByList<tk_Log>(list, "tk_Log");

            return SQLBase.ExecuteNonQuery(logSql) > 0;
        }


        public static bool SaveRepeal(string YYCode, string RepealReason, string MCode)
        {
            string sql = "update MandateInfo set state=-1,RepealReason='" + RepealReason + "' where YYCode='" + YYCode + "'";
            if (SQLBase.ExecuteNonQuery(sql) > 0)
            {
                sql = "select count(*) from TaskOrder where Mid='" + MCode + "'";
                if ((int)SQLBase.ExecuteScalar(sql) > 0)
                {
                    sql = "update TaskOrder set state=-1 where Mid='" + MCode + "'";
                    if (SQLBase.ExecuteNonQuery(sql) <= 0)
                        return false;
                }
                sql = "select count(*) from ConsumptionInfo where YYCode ='" + YYCode + "'";
                if ((int)SQLBase.ExecuteScalar(sql) > 0)
                {
                    sql = "select sum(Amount) from ConsumptionInfo where type=0 and YYCode ='" + YYCode + "'";
                    double Amount = Convert.ToDouble(SQLBase.ExecuteScalar(sql));
                    sql = "select a.ClienName from ClientPayInfo a left join MandateInfo b on a.ClienName= b.ClienName where YYCode ='" + YYCode + "'";
                    DataTable dt = SQLBase.FillTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "update ClientPayInfo set Money = Money+" + Amount + " where ClienName = '" + dt.Rows[0]["ClienName"].ToString() + "'";
                        if (SQLBase.ExecuteNonQuery(sql) <= 0)
                            return false;
                    }
                    else
                    {
                        sql = "select ClienName from MandateInfo where yycode='" + YYCode + "'";
                        DataTable dt2 = SQLBase.FillTable(sql);
                        sql = "insert into ClientPayInfo(ClienName,Money) values('" + dt2.Rows[0]["ClienName"] + "'," + Amount + ")";
                        if (SQLBase.ExecuteNonQuery(sql) <= 0)
                            return false;

                    }
                    sql = "update ConsumptionInfo set type=2 where type=0 and YYCode ='" + YYCode + "'";
                    if (SQLBase.ExecuteNonQuery(sql) <= 0)
                        return false;
                }

                if (!insertLog(YYCode, "撤销委托单"))
                    return false;
                return true;
            }

            return false;
        }
        /// <summary>
        /// 获取经验分析数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataSet GetJYFX(int month)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@MONTH",month)
            };
            DataSet DO_Order = SQLBase.FillDataSet("JYFX", CommandType.StoredProcedure, para);
            return DO_Order;
        }
        /// <summary>
        /// 获取经营任务
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataTable GetOperationTask(int month, ref string istask)
        {
            DataTable dt = null;
            string sql = "select count(*) from OperationAnalysis where year=" + DateTime.Now.ToString("yyyy") + " and Month = " + month + " and unit='" + GAccount.GetAccountInfo().UnitName + "'";

            if ((int)SQLBase.ExecuteScalar(sql) > 0)
            {
                sql = "select a.OId,YearTarget, MonthTarget, MonthComplete, Problem, NextMonthTarget, Type, Remark from OperationAnalysis a left join OperationTask b on a.OId=b.OId where year=" + DateTime.Now.ToString("yyyy") + " and Month = " + month + " and unit='" + GAccount.GetAccountInfo().UnitName + "'";
                dt = SQLBase.FillTable(sql);
                istask = "y";
            }
            else
            {
                sql = "select YearTarget, MonthTarget,NextMonthTarget, Type from OperationAnalysis a left join OperationTask b on a.OId=b.OId where year=" + DateTime.Now.ToString("yyyy") + " and Month = " + (month - 1) + " and unit='" + GAccount.GetAccountInfo().UnitName + "'";
                dt = SQLBase.FillTable(sql);
                istask = "n";
            }

            return dt;
        }


        public static OperationAnalysis getOperationAnalysis(int month)
        {
            string sql = "select top(1) OId, Year, Month, Overview, BusinesAnalysi, BusinesContent, PayableAnalysi, PayableContent, Experience, Other, Unit, CreateUser, CreateTime from OperationAnalysis where year=" + DateTime.Now.ToString("yyyy") + " and Month = " + month + " and unit='" + GAccount.GetAccountInfo().UnitName + "' order by CreateTime";
            OperationAnalysis oa = new OperationAnalysis();
            GSqlSentence.SetTValueD<OperationAnalysis>(oa, SQLBase.FillTable(sql).Rows[0]);
            return oa;

        }

        public static string GetOId()
        {
            string NewTime = DateTime.Now.ToString("yyyyMMdd");
            string YYCode = "FX" + NewTime + "00";
            string sql = "select count(*) from TK_ConfingIDRecord where type='Operation' and OneWord='FX' and YMD ='" + NewTime + "'";
            int count = (int)SQLBase.ExecuteScalar(sql);
            string MaXValue = "1";
            if (count > 0)
            {
                sql = "select MaxValue from TK_ConfingIDRecord where type='Operation' and OneWord='FX' and YMD ='" + NewTime + "'";
                DataTable dt = SQLBase.FillTable(sql);
                MaXValue = (int.Parse(dt.Rows[0]["MaxValue"].ToString()) + 1).ToString();
            }

            return YYCode.Substring(0, 12 - MaXValue.Length) + MaXValue;
        }

        public static bool UpdateOId(string OId)
        {
            string YYMMDD = OId.Substring(2, 8);
            string sql = "select count(*) from TK_ConfingIDRecord where type='Operation' and OneWord='FX' ";
            int count = (int)SQLBase.ExecuteScalar(sql);
            if (count > 0)
            {
                sql = "update TK_ConfingIDRecord set MaxValue =" + OId.Substring(10, 2) + ", YMD ='" + YYMMDD + "'  where type='Operation' and OneWord='FX'";
            }
            else
            {
                sql = "insert into TK_ConfingIDRecord(YMD, OneWord, MaxValue, Type) values ('" + YYMMDD + "','FX'," + OId.Substring(10, 2) + ",'Operation')";
            }
            return SQLBase.ExecuteNonQuery(sql) > 0;
        }

        public static bool SaveOperationAnalysis(OperationAnalysis oa, List<OperationTask> list, ref string err)
        {
            string sql = GSqlSentence.GetInsertInfo<OperationAnalysis>(oa);

            if (SQLBase.ExecuteNonQuery(sql) > 0)
            {
                sql = GSqlSentence.GetInsertByList<OperationTask>(list, "OperationTask");
                if (SQLBase.ExecuteNonQuery(sql) > 0)
                {
                    if (UpdateOId(oa.OId))
                    {
                        return true;
                    }
                    err = "编号更新失败";
                    return false;
                }
                err = "任务信息保存失败";
                return false;
            }


            err = "保存失败";
            return false;
        }

        public static UIDataTable GetOperationAnalysisGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@PageSize",a_intPageSize),
                new SqlParameter("@StartIndex",(a_intPageIndex * a_intPageSize).ToString()),
                new SqlParameter("@Where",where)
            };
            DataSet DO_Order = SQLBase.FillDataSet("GetOperationAnalysisGrid", CommandType.StoredProcedure, para);
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntTotalPages = 0;
                instData.IntRecords = 0;
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

        public static string SKTZ(string MCode)
        {
            string sql = "select a.mcode,a.ClienName,charge,sum(number)number from MandateInfo a left join sampleInfo b on a.yycode = b.yycode  where a.MCode ='" + MCode + "' group by a.mcode,a.ClienName,charge";

            DataTable dt = SQLBase.FillTable(sql);
            string result = dt.Rows[0]["mcode"] + "," + dt.Rows[0]["ClienName"] + "," + dt.Rows[0]["charge"] + "," + dt.Rows[0]["number"] + "," + DateTime.Now.ToString("yyyy年MM月dd日");
            return result;
        }
    }
}
