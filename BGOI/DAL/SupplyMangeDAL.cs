using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
namespace TECOCITY_BGOI
{
    public class SupplyMangeDAL
    {
        #region sid规则

        /// <summary>
        /// 从pid表查数据插入数据
        /// </summary>
        /// <returns></returns>
        public static string GetPid()
        {
            //SP15082100320000
            string strPid = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            string danwei = acc.UnitID;
            string strYear = DateTime.Now.ToString("yyMMdd") + "00" + danwei;
            string strdepart = "select DeptId from UM_UnitNew where DeptId='" + danwei + "'";
            DataTable dtUnite = SQLBase.FillTable(strdepart, "AccountCnn");
            string steSelPid = "select PID,PidNo from tk_PID where DataRecord='" + strYear + "'";
            DataTable dt = SQLBase.FillTable(steSelPid, "SupplyCnn");
            int intnewPID = 0;
            if (dt == null)
            {
                return strPid;
            }
            if (dt.Rows.Count == 0)
            {
                string strInSert = "insert into tk_PID(PID,PidNo,DataRecord) values('SP',0,'" + strYear + "')";
                SQLBase.ExecuteNonQuery(strInSert, "SupplyCnn");
                intnewPID = 0;
            }
            else
            {
                intnewPID = Convert.ToInt32(dt.Rows[0]["PidNo"]);
            }
            intnewPID++;
            string str = "select PID,PidNo,DataRecord from tk_PID where DataRecord='" + strYear + "'";
            dt = SQLBase.FillTable(str, "SupplyCnn");
            strPid = dt.Rows[0]["PID"].ToString() + DateTime.Now.ToString("yyMMdd") + "00" + danwei + GFun.GetNum(intnewPID, 4);
            return strPid;
        }
        public static string GetKID()
        {
            //k000001
            string strkid = "";
            string strid = "select max(KID) KID from ( select substring(KID,2,7) KID from tk_KClientBas where KID is not null) k";
            DataTable dt = SQLBase.FillTable(strid, "SupplyCnn");
            string strnewid = "";
            string dtrow = dt.Rows[0][0].ToString();
            if (dtrow != "" && dtrow != null)
            {
                strnewid = dt.Rows[0][0].ToString();
                int intid = Convert.ToInt32(strnewid);
                if (intid > 0 && intid < 9)
                {
                    strnewid = "K00000" + (intid + 1);
                }
                else if (intid >= 9 && intid < 99)
                {
                    strnewid = "K0000" + (intid + 1);
                }
                else if (intid >= 99 && intid < 999)
                {
                    strnewid = "K000" + (intid + 1);
                }
                else if (intid >= 999 && intid < 9999)
                {
                    strnewid = "K00" + (intid + 1);
                }
                else
                {
                    strnewid = "K0" + (intid + 1);
                }
            }
            else
            {
                strnewid = "K000001";
            }
            strkid = strnewid;
            return strkid;
        }
        public static string GetYeaID()
        {
            string strYid = "";
            string strid = "select max(YRID) YRID from ( select substring(YRID,5,7) YRID from tk_SYRDetail where YRID is not null) k";
            DataTable dt = SQLBase.FillTable(strid, "SupplyCnn");
            string strnewid = "";
            string dtrow = dt.Rows[0][0].ToString();

            if (dtrow != "")
            {
                strnewid = dt.Rows[0][0].ToString();
                int intid = Convert.ToInt32(strnewid);
                if (intid > 0 && intid < 9)
                {
                    strnewid = DateTime.Now.Year + "00" + (intid + 1);
                }
                else if (intid >= 9 && intid < 99)
                {
                    strnewid = DateTime.Now.Year + "0" + (intid + 1);
                }
                else
                {
                    strnewid = (DateTime.Now.Year + (intid + 1)).ToString();
                }
            }
            else
            {
                strnewid = DateTime.Now.Year + "001";
            }
            strYid = strnewid;
            return strYid;
        }
        /// <summary>
        /// 更新pid表
        /// </summary>
        /// <returns></returns>
        public static string GetNewPid()
        {
            string strPid = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            string danwei = acc.UnitID;
            string strYear = DateTime.Now.ToString("yyMMdd") + "00" + danwei;
            string strdepart = "select DeptId from UM_UnitNew where DeptId='" + danwei + "'";
            DataTable dtUnite = SQLBase.FillTable(strdepart, "AccountCnn");
            string steSelPid = "select PID,PidNo from tk_PID where DataRecord='" + strYear + "'";
            DataTable dt = SQLBase.FillTable(steSelPid, "SupplyCnn");
            int intnewPID = 0;
            if (dt == null)
            {
                return strPid;
            }
            if (dt.Rows.Count == 0)
            {
                string strInSert = "insert into tk_PID(PID,PidNo,DataRecord) values('SP',0,'" + strYear + "')";
                SQLBase.ExecuteNonQuery(strInSert, "SupplyCnn");
                intnewPID = 0;
            }
            else
            {
                intnewPID = Convert.ToInt32(dt.Rows[0]["PidNo"]);
            }
            intnewPID++;
            string strUpdate = "update tk_PID set PidNo='" + intnewPID + "' where DataRecord='" + strYear + "'";
            SQLBase.FillTable(strUpdate, "SupplyCnn");
            strPid = dt.Rows[0]["PID"].ToString() + DateTime.Now.ToString("yyMMdd") + "00" + danwei + GFun.GetNum(intnewPID, 4);
            return strPid;
        }

        /// <summary>
        /// 流水号新编写
        /// </summary>
        /// <returns></returns>
        public static string UpdateSID()
        {
            //YS0001
            //S000001
            string stri = "";
            string strid = "select max(sid) sid  from ";
            strid += "(select SUBSTRING(SID,3,4) SID  from tk_SupplierBas where SID  is not null) k  ";
            DataTable dt = SQLBase.FillTable(strid, "SupplyCnn");
            string ResID = "";
            string NewDate = dt.Rows[0][0].ToString();
            if (NewDate != "" && NewDate != null)
            {
                ResID = dt.Rows[0][0].ToString();
                int Intid = Convert.ToInt32(ResID);
                if (Intid > 0 && Intid < 9)
                {
                    ResID = "YS000" + (Intid + 1);
                }
                else if (Intid >= 9 && Intid < 99)
                {
                    ResID = "YS00" + (Intid + 1);
                }
                else if (Intid >= 99 && Intid < 999)
                {
                    ResID = "YS0" + (Intid + 1);
                }
                else
                {
                    ResID = "YS" + (Intid + 1);
                }
            }
            else
            {
                ResID = "YS0001";
            }
            stri = ResID;
            return stri;
        }
        public static string UpdatenewnotSID()
        {
            string stri = "";
            string strid = "select max(sid) sid  from ";
            strid += "(select SUBSTRING(SID,3,4) SID  from tk_IsNotSupplierBas where SID  is not null) k  ";
            DataTable dt = SQLBase.FillTable(strid, "SupplyCnn");
            string ResID = "";
            string NewDate = dt.Rows[0][0].ToString();
            if (NewDate != "" && NewDate != null)
            {
                ResID = dt.Rows[0][0].ToString();
                int Intid = Convert.ToInt32(ResID);
                if (Intid > 0 && Intid < 9)
                {
                    ResID = "YS000" + (Intid + 1);
                }
                else if (Intid >= 9 && Intid < 99)
                {
                    ResID = "YS00" + (Intid + 1);
                }
                else if (Intid >= 99 && Intid < 999)
                {
                    ResID = "YS0" + (Intid + 1);
                }
                else
                {
                    ResID = "YS" + (Intid + 1);
                }
            }
            else
            {
                ResID = "YS0001";
            }
            stri = ResID;
            return stri;
        }
        public static DataTable NewKey(string data, string table)
        {
            string str = "select SID,State from [" + data + "].." + table + "";
            DataTable dt = SQLBase.FillTable(str);
            return dt;
            //string sql = " select b.text as state from	dbo.tk_SupplierBas   a";
            //sql += "  inner join	tk_ConfigContent b on a.state=b.sid and b.type='zcstate'   where a.sid='" + sid + "'";
            //return sql;
        }
        public static DataTable GetSupCode()
        {
            string sql = "select suppliercode from dbo.tk_SupplierBas ";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetSupNmae()
        {
            string sql = "select comnamec from dbo.tk_SupplierBas ";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetnotSupNmae()
        {
            string sql = "select comnamec from dbo.tk_IsNotSupplierBas ";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetZHENSHUCode()
        {
            string sql = "select CCode from dbo.tk_SCertificate ";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetCusName()
        {
            string sql = "select cname from dbo.tk_KClientBas ";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetUnit()
        {
            string sql = "SELECT DeptId,DeptName  FROM BJOI_UM..UM_UnitNew where DeptName='综合管理部' OR DeptName='输配产品部' OR  DeptName='火艺工程部' OR DeptName='应用产品部' OR  DeptName='售后服务部' OR DeptName='技术工程部'or deptname='公司领导'";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");
            return dt;
        }
        #endregion
        #region 从表中获取数据
        public static DataTable GetTypes(string type)
        {
            string sql = "select SID,XID,Text from tk_ConfigContent where  Type='" + type + "' and Validate='v' order by SID ASC";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetStateType()
        {
            string sql = "select SID, XID,Text from tk_ConfigContent where (type='zcstate' or type='rcstate')and sid>='21' AND sid IN('29') or SID >='50' AND SID<='56' order by SID ASC";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        /// <summary>
        /// 获得库存中获得产品分类
        /// </summary>
        /// <returns></returns>
        public static DataTable GetproType()
        {

            string sql = " select distinct a.ptype,b.text from BGOI_Inventory..tk_ProductInfo a";
            sql += "    inner join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetFType(string lever)
        {
            string sql = "select SID,Text from  dbo.tk_ConfigContent  where typedesc='" + lever + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetCode()
        {
            string sql = "select distinct b.pid ";
            sql += " from dbo.tk_ConfigPType a left join tk_ProductInfo b on a.id=b.Ptype";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetName()
        {
            string sql = "select distinct b.ProName ";
            sql += " from dbo.tk_ConfigPType a left join tk_ProductInfo b on a.id=b.Ptype";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetProUnite()
        {
            string sql = "select  distinct units from [BGOI_Inventory]..tk_ProductInfo ";
            // sql += " from dbo.tk_ConfigPType a left join tk_ProductInfo b on a.id=b.Ptype";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetProStand()
        {
            string sql = "select distinct b.Spec ";
            sql += " from dbo.tk_ConfigPType a left join tk_ProductInfo b on a.id=b.Ptype";
            DataTable dt = SQLBase.FillTable(sql, "MainInventory");
            return dt;
        }
        public static DataTable GetType2()
        {
            string sql = "select SID,TEXT,XID from tk_ConfigContent where type='zrstate' AND SID NOT IN(2,3,4) order by SID  asc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetTypeOK()
        {
            string sql = "select SID,TEXT,XID from tk_ConfigContent where (type='zcstate' OR Type='nstate' or type='hstate') AND SID NOT IN('29','62','64','30','20','60','63','61') order by SID  asc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetType3()
        {
            string sql = "select SID,TEXT,XID from tk_ConfigContent where type='zrstate' and text='待会签审批'  order by SID  asc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        /// <summary>
        /// 动态构建html
        /// </summary>
        /// <param name="type"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable GetSupConfig(string type, string table)
        {
            string sql = "select * from " + table + " where [Type]='" + type + "' and validate='v' order by SID";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDept(string table)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "select DeptID, DeptName from BJOI_UM.." + table + " where Superid=";
            sql += " (select Superid from BJOI_UM.." + table + " where deptid=" + acc.UnitID + ")";
            DataTable dt = SQLBase.FillTable(sql, "AccountCnn");
            return dt;
        }

        /// <summary>
        /// 点击按钮添加新行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetNewCon(string type)
        {
            string sql = "select SID,Text from tk_ConfigContent where Type='" + type + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }

        public static DataTable GetNewDetailID(string sid)
        {
            string where = " and a.SID = '" + sid + "'";
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@where",where)
                };
            DataTable dt = SQLBase.FillTable("GetSuppBas", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            return dt;
        }

        public static DataTable GetNewDetailSugestion(string sid)
        {
            string sql = "SELECT  * FROM tk_SUPSugestion where SID='" + sid + "' ORDER BY SCreate DESC";
            DataTable dt = SQLBase.FillTable(sql, CommandType.Text, null, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewDetailApproval(string sid)
        {
            string sql = "SELECT  * FROM tk_SApproval	where RelevanceID='" + sid + "' ORDER BY	   ApprovalTime desc";
            DataTable dt = SQLBase.FillTable(sql, CommandType.Text, null, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewClass(string sid)
        {
            string sql = " select AgentClass=stuff((select ','+Text from tk_ConfigAgentClass b";
            sql += " where charindex(''+convert(varchar,b.SID),''+a.AgentClass)>0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            DataTable dt2 = SQLBase.FillTable(sql, "SupplyCnn");
            return dt2;
        }
        /// <summary>
        /// 供应商供需关系
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetNewQuality(string sid)
        {
            string sql = " select Relation=stuff((select ','+Text from tk_ConfigReation b";
            sql += " where charindex(''+convert(varchar,b.SID),''+a.Relation)>0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            DataTable dt2 = SQLBase.FillTable(sql, "SupplyCnn");
            return dt2;
        }
        /// <summary>
        /// 供应商经营产品分类
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetNewScaleType(string sid)
        {
            string sql = " select ScaleType=stuff((select ','+Text from tk_ConfigScalType b";
            sql += " where charindex(''+convert(varchar,b.SID),''+a.ScaleType)>0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            DataTable dt2 = SQLBase.FillTable(sql, "SupplyCnn");
            return dt2;
        }
        /// <summary>
        /// 产品质量执行标准
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetNewQualityStandard(string sid)
        {
            string sql = " select QualityStandard=stuff((select ','+Text from tk_ConfigQualityStandard b";
            sql += " where charindex(''+convert(varchar,b.SID),''+a.QualityStandard)>0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            DataTable dt2 = SQLBase.FillTable(sql, "SupplyCnn");
            return dt2;
        }
        public static DataTable GetnewgetBusinessDistribute(string sid)
        {
            string sql = " select BusinessDistribute=stuff((select ','+Text from tk_BusinessDistribute b";
            sql += " where charindex(''+convert(varchar,b.XID),''+a.BusinessDistribute)>=0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            // string sql = "SELECT SID ,Text,Type  FROM   tk_ConfigContent where Type='BusinessDistribute'";
            DataTable dtb = SQLBase.FillTable(sql, "SupplyCnn");
            return dtb;
        }
        public static DataTable GetNewBillWay(string sid)
        {
            string sql = " select BillingWay=stuff((select ','+Text from tk_ConfigBillWay b";
            sql += " where charindex(''+convert(varchar,b.XID),''+a.BillingWay)>=0 for xml path('')),1,1,'')";
            sql += "from  tk_SupplierBas a where a.sid='" + sid + "'";
            DataTable dtbl = SQLBase.FillTable(sql, "SupplyCnn");
            return dtbl;
        }
        public static DataTable getNewBillHan()
        {
            string sql = " SELECT SID ,Text,Type  FROM   tk_ConfigContent where Type='BillingWay'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getNewBuinessHan()
        {
            string sql = " SELECT SID ,Text,Type  FROM   tk_ConfigContent where Type='BusinessDistribute'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getmfile(string sid)
        {
            string sql = "SELECT a.SID,b.Text as FType,c.text as TypeO,d.Text as Item,a.ItemO,a.FFileName,a.MFFileName,a.FID  FROM tk_SFileInfo a ";
            sql += "  LEFT JOIN tk_ConfigContent b ON a.FType=b.SID AND b.Type='ftype'";
            sql += " LEFT JOIN tk_ConfigContent c ON a.TypeO=c.SID AND c.type='TypeO'";
            sql += " LEFT JOIN tk_ConfigContent d ON a.Item=d.SID AND d.Type='Item' where a.sid='" + sid + "' order BY a.SID desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getCertifyname(string sid)
        {
            string sql = "SELECT b.Text as CType,a.CName,a.CCode,a.COrganization,a.CFileName,a.FID  FROM tk_SCertificate  a";
            sql += "  LEFT JOIN tk_ConfigContent b ON a.CType=b.sid and type='ctype' where a.sid='" + sid + "'	 order BY a.SID  desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getAward(string sid)
        {
            string sql = "SELECT * FROM tk_Award  where sid='" + sid + "' order BY SID desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getpricenam(string sid)
        {
            string sql = "SELECT * FROM tk_PriceUp where sid='" + sid + "'  order BY SID desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewUnit(string kid)
        {
            string sql = " select ShareUnits=stuff((select ','+DeptName from BJOI_UM..UM_UnitNew b";
            sql += " where charindex(','+convert(varchar,b.Deptid),','+a.ShareUnits)>0 for xml path('')),1,1,'') from ";
            sql += "  tk_KClientBas a where kid='" + kid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable getNewUnites(string sid)
        {
            string sql = " select UnReviewUnit=stuff((select ','+Text from tk_ConfigSupUnit b";
            sql += " where charindex(','+convert(varchar,b.SID),','+a.UnReviewUnit)>0 for xml path('')),1,1,'') from ";
            sql += "  tk_SupplierBas a where sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        /// <summary>
        /// 准入审批详细
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetNewDetailMsg(string sid)
        {
            string where = "  and a.sid='" + sid + "'";
            SqlParameter[] pas = new SqlParameter[]
            {
            new SqlParameter("@where",where)
            };
            DataTable dt = SQLBase.FillTable("GetDetail", CommandType.StoredProcedure, pas, "SupplyCnn");
            return dt;
        }
        /// <summary>
        /// 暂停或淘汰供应商详情
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable getDetail(string where)
        {
            string sql = "select distinct d.SID,d.COMNameC,d.ComAddress, b.text as SupplierType,d.Turnover,";
            sql += " d.COMShortName,d.COMNameE,d.COMWebsite,d.COMRAddress,e.text as COMCountry, f.text as  ";
            sql += " COMArea,d.COMFactoryAddress,d.COMFactoryArea,d.COMCreateDate,d.COMLegalPerson,d.TaxRegistrationNo,d.IsCooperate,d.COMGroup,";
            sql += "  d.BusinessLicenseNo,d.StaffNum,d.RegisteredCapital,g.text as CapitalUnit,d.OrganizationCode,i.text as BankName,d.BankAccount,h.text as EnterpriseType,d.BusinessDistribute,d.ThreeCertity,";
            sql += "	 d.BillingWay,d.DevelopStaffs,d.QAStaffs,d.ProduceStaffs,d.HasRegulation,d.ProductLineNum,d.BusinessScope,d.FAX,d.Relation";
            sql += "   from     tk_SupplierBas d ";
            sql += "  LEFT JOIN	tk_ConfigContent i ON d.BankName=i.sid AND i.Type='BankName' left join tk_ConfigContent  b on d.SupplierType=b.sid and b.type='SupplierType'";
            sql += "  left join tk_ConfigContent  e on d.COMCountry=e.sid and e.type='COMCtry' left join tk_ConfigContent  f on d.COMArea=f.sid and f.type='COMArea' left join tk_ConfigContent  g on d.CapitalUnit=g.sid and g.type='CapitalUnit'";
            sql += "  left join tk_ConfigContent  h on d.EnterpriseType=h.sid and h.type='EnterpriseType' where 1=1 and d.ValiDate = 'v'   ";
            sql += where;
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");//"getDetailApp"
            return dt;
        }
        public static DataTable GetNewDetailCustomerID(string kid)
        {
            string where = "  and a.KID='" + kid + "'";
            SqlParameter[] sqlpar = new SqlParameter[]
            {
            new  SqlParameter("@where",where)
            };
            DataTable dt = SQLBase.FillTable("GetCustomerInfo", CommandType.StoredProcedure, sqlpar, "SupplyCnn");
            return dt;
        }
        public static DataTable getDetailPerson(string sid)
        {
            string str = "select  b.Text as FDepartment,a.PName,c.Text as Department,d.Text as Job,a.Phone,a.Mobile,a.Email";
            str += "	from dbo.tk_SContactPerson a ";
            str += "	left join tk_ConfigContent b on a.FDepartment=b.SID and b.Type='FDepartment'";
            str += "	left join (select * from tk_ConfigContent) c on a.Department=c.SID and c.Type='Department'";
            str += "	left join (select * from tk_ConfigContent) d on a.Job=d.SID and d.Type='Job'  where a.SID='" + sid + "'";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            return dt;
        }
        public static DataTable getProduct(string sid)
        {
            string sqk = "SELECT * FROM tk_SProducts  where SID='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sqk, "SupplyCnn");
            return dt;
        }
        public static DataTable getnewServer(string sid)
        {
            string sqk = "SELECT * FROM tk_SService  where SID='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sqk, "SupplyCnn");
            return dt;
        }
        public static DataTable GetLogNum(string sid)
        {
            string sql = "	select * from   dbo.tk_UserLog	    where userid='" + sid + "' order by logtime desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        #endregion
        #region 展示数据
        public static UIDataTable getSupplyGride(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getSupply", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getCertificateGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getCertificate", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getProGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getPro", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getServerGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getServer", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getAwardGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getAward", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getAwardPrice(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getPrice", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getManageGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                     new SqlParameter("@Order",order)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getManage", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getManageokGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where.ToString()),
                     new SqlParameter("@Order",order)
                                   };
            DataSet DO_Order = SQLBase.FillDataSet("getManageOK", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getYearGride(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                     new SqlParameter("@Order",order)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getYearData", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getScoreGride(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getScore", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getnewScoreGride(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getYScore", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getAwardssGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getAward", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getPricessGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getPrice", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getCustomerGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getCustomer", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getisnotsuplyGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                                  };
            DataSet DO_Order = SQLBase.FillDataSet("getisnotsuply", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getContractPersoGrid(int a_intPageSize, int a_intPageIndex, string where, string kid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (kid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getperson", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getShareGrid(int a_intPageSize, int a_intPageIndex, string where, string kid, string isshare)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (kid != "" && isshare == "是")
            {
                string sql = "select ShareUnits=stuff((select ','+DeptName from BJOI_UM..UM_UnitNew b";
                sql += " where charindex(','+convert(varchar,b.Deptid),','+a.ShareUnits)>0 for xml path('')),1,1,'') from ";
                sql += "  tk_KClientBas a where kid='" + kid + "'";
                DataSet DO_Order = SQLBase.FillDataSet(sql, CommandType.Text, sqlPar, "SupplyCnn");

                if (DO_Order == null)
                {
                    instData.DtData = null;
                    instData.IntRecords = 0;
                    instData.IntTotalPages = 0;
                    return instData;
                }
                DataTable dtOrder = DO_Order.Tables[0];
                instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[0].Rows[0][0]);
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
            }
            return instData;
        }
        public static UIDataTable getWeiguiGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                     new SqlParameter("@Order",order)
              };
            DataSet DO_Order = SQLBase.FillDataSet("getWeigui", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getManageGridSP(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                     new SqlParameter("@Order",order)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getManageSp", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable getApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            string sql = "select a.userName,b.Duty,b.BuType from  UM_UserNew a ,UM_Examine b where a.UserId=b.UserId ";
            DataSet DO_Order = SQLBase.FillDataSet(sql, CommandType.Text, sqlPar, "AccountCnn");
            if (DO_Order == null)
            {
                instData.DtData = null;
                instData.IntRecords = 0;
                instData.IntTotalPages = 0;
                return instData;
            }
            DataTable dtOrder = DO_Order.Tables[0];
            instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[0].Rows[0][0]);
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
        public static UIDataTable getManageProGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            //string sql = "";
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getManagePro", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;

        }
        /// <summary>
        /// 综合管理中的可供应服务
        /// </summary>
        /// <param name="a_intPageSize"></param>
        /// <param name="a_intPageIndex"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static UIDataTable getManageSerGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getServer", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getSPR(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getSPRecord", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getSR(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getSRecord", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getPlanProSerGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getApproval", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getPlanSerGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {


                DataSet DO_Order = SQLBase.FillDataSet("getCertificate", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable GetLog(int a_intPageSize, int a_intPageIndex, string where, string sid, string kid)
        {
            //string sql = "";
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                //sql = "select * from tk_UserLog where USERID='" + sid + "' order by logtime desc";
                DataSet DO_Order = SQLBase.FillDataSet("getUserlog", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            else if (kid != "")
            {
                // sql = "select * from tk_UserLog where USERID='" + kid + "'";
                DataSet DO_Order = SQLBase.FillDataSet("getUserlog", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getConditonGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getConditionGrid", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            //else if (kid != "")
            //{
            //    // sql = "select * from tk_UserLog where USERID='" + kid + "'";
            //    DataSet DO_Order = SQLBase.FillDataSet("getUserlog", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
            //    if (DO_Order == null)
            //    {
            //        instData.DtData = null;
            //        instData.IntRecords = 0;
            //        instData.IntTotalPages = 0;
            //        return instData;
            //    }
            //    DataTable dtOrder = DO_Order.Tables[0];
            //    instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[1].Rows[0][0]);
            //    if (instData.IntRecords > 0)
            //    {
            //        if (instData.IntRecords % a_intPageSize == 0)
            //            instData.IntTotalPages = instData.IntRecords / a_intPageSize;
            //        else
            //            instData.IntTotalPages = instData.IntRecords / a_intPageSize + 1;
            //    }
            //    else
            //        instData.IntTotalPages = 0;
            //    instData.DtData = dtOrder;
            //}
            return instData;
        }
        public static UIDataTable getSP(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getRecord", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable GetBR(int a_intPageSize, int a_intPageIndex, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                  
                };

            string sql = " Select top  " + a_intPageSize + "  * from ( Select distinct  ROW_NUMBER() OVER (ORDER BY a.sid desc) AS RowNumber, a.SCreate,b.FContent,b.FReason,b.FName,a.Sperson,a.SContent,b.FTime from tk_SUPSugestion  a  ";
            sql += "   left join	tk_FeedBack b on a.sid=b.sid where b.sid='" + sid + "')	AS TEMPTABLE Where RowNumber> " + a_intPageIndex + "";
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet(sql, CommandType.Text, sqlPar, "SupplyCnn");
                if (DO_Order == null)
                {
                    instData.DtData = null;
                    instData.IntRecords = 0;
                    instData.IntTotalPages = 0;
                    return instData;
                }
                DataTable dtOrder = DO_Order.Tables[0];
                instData.IntRecords = GFun.SafeToInt32(DO_Order.Tables[0].Rows[0][0]);
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
            }

            return instData;
        }
        public static UIDataTable GetDealRecord(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getDealRecord", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        public static UIDataTable getMainCertificateGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };
            if (sid != "")
            {
                DataSet DO_Order = SQLBase.FillDataSet("getManaCertificate", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
            }
            return instData;
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加资质信息
        /// </summary>
        /// <param name="filems"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static int CreateFiles(tk_SFileInfo filems, ref string strErr)
        {
            int intInsertCon = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsertCon = GSqlSentence.GetInsertInfoByD<tk_SFileInfo>(filems, "tk_SFileInfo");
            try
            {
                if (strInsertCon != "")
                    intInsertCon = sqlTrans.ExecuteNonQuery(strInsertCon, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                strErr = e.Message;
                return -1;
            }
            return intInsertCon;
        }
        /// <summary>
        /// 添加证书信息
        /// </summary>
        /// <param name="certifi"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static int CreateCertifi(tk_SCertificate certifi, ref string Err)
        {
            int intInsertCon = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertCon = GSqlSentence.GetInsertInfoByD<tk_SCertificate>(certifi, "tk_SCertificate");
            try
            {
                if (strInsertCon != "")
                    intInsertCon = sqlTrans.ExecuteNonQuery(strInsertCon, CommandType.Text, null);

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }
            return intInsertCon;
        }
        /// <summary>
        /// 添加产品信息
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static int CreatePro(tk_SProducts pro, ref string Err)
        {
            int intInsertCon = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertCon = GSqlSentence.GetInsertInfoByD<tk_SProducts>(pro, "tk_SProducts");
            try
            {
                if (strInsertCon != "")
                    intInsertCon = sqlTrans.ExecuteNonQuery(strInsertCon, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }
            return intInsertCon;
        }
        public static int CreateServer(tk_SService server, ref string Err)
        {
            int intInsertCon = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertCon = GSqlSentence.GetInsertInfoByD<tk_SService>(server, "tk_SService");
            try
            {
                if (strInsertCon != "")
                    intInsertCon = sqlTrans.ExecuteNonQuery(strInsertCon, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }
            return intInsertCon;
        }

        /// <summary>
        /// 供应商基本信息保存
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static bool InsertSupplyBas(Tk_SupplierBas bas, List<Tk_SContactPerson> listPer, ref string strErr)
        {
            int intInsert = 0;
            int intInsertCon = 0;
            int intInsertConCount = 0;
            int intlog = 0;
            string strInsert = "";
            string strIsertCON = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            //插入基本信息，  
            if (bas.IsrankingIn5 == "0") //&& bas.HasAuthorization == "0"// bas.AgentClass!=""||bas.AgentClass!=null代理产品所属级别不选
            {
                strInsert += GSqlSentence.GetInsertInfoByD<Tk_SupplierBas>(bas, "tk_SupplierBas");

            }
            //else if (bas.IsrankingIn5 == "0" && bas.HasAuthorization == "1")
            //{
            //    strInsert = InsertNewBas(bas, strInsert, acc);
            //}
            else if (bas.IsrankingIn5 == "1") //&& bas.HasAuthorization == "0"
            {
                strInsert += InsertNewBasTwo(bas, strInsert, acc);
            }
            else
            {
                strInsert += InsertNewBasThree(bas, strInsert, acc);
            }
            if (strInsert != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
            }
            string strlog = "";
            //插入联系人
            strIsertCON = GSqlSentence.GetInsertByList<Tk_SContactPerson>(listPer, "Tk_SContactPerson");

            string strInertConCount = "update Tk_SContactPerson set SID='" + listPer[0].Sid + "' where  CreateTime=(select max('" + listPer[0].CreateTime + "') from Tk_SupplierBas) ";
            if (intInsert == 1)
            {
                //strInsertCont = "update tk_SupplierBas set  SID = '" + bas.Sid + "' where CreateTime=(select max('" + bas.CreateTime + "') from Tk_SupplierBas)";//State = '0' where
                strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','新增供应商基本信息','新增基本信息成功','" + DateTime.Now + "','" + acc.UserName + "','新增供应商基本信息')";
            }
            try
            {

                if (strIsertCON != "")
                {
                    intInsertCon = SQLBase.ExecuteNonQuery(strIsertCON, "SupplyCnn");
                }
                //if (strInsertCont != "")
                //{

                //    intInsertCont = SQLBase.ExecuteNonQuery(strInsertCont, "SupplyCnn");
                //}
                if (strInertConCount != "")
                {
                    intInsertConCount = SQLBase.ExecuteNonQuery(strInertConCount, "SupplyCnn");
                }
                if (strlog != "")
                {
                    intlog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                }
                //更新bas表中sid
                // GetSid(bas.Sid);
            }
            catch (SqlException e)
            {
                strErr = e.Message;
                return false;
            }
            return (intInsert + intInsertCon + intlog + intInsertConCount) >= 4;//+ update;
        }
        /// <summary>
        /// 现在不用
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="strInsert"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        private static string InsertNewBas(Tk_SupplierBas bas, string strInsert, Acc_Account acc)
        {
            strInsert += "insert into tk_SupplierBas(Sid,DeclareUnitID,DeclareDate,SupplierType,OtherType,COMNameC,COMShortName,";//
            strInsert += "COMWebsite,COMArea,COMNameE,COMCountry,COMRAddress,COMCreateDate,TaxRegistrationNo,BusinessLicenseNo,";
            strInsert += "ComAddress,COMLegalPerson,COMFactoryAddress,COMFactoryArea,OrganizationCode,COMGroup,RegisteredCapital,CapitalUnit,IsCooperate,BankName,BankAccount,StaffNum,EnterpriseType,BusinessDistribute,BillingWay,Turnover,DevelopStaffs,QAStaffs,ProduceStaffs,Relation,HasRegulation,ProductLineNum,WorkTime_Start,WorkTime_End,WorkDay_Start,WorkDay_End,FAX,BusinessScope,IsrankingIn5,RankingType,Ranking,ScaleType,QualityStandard,AnnualOutput,AnnualOutputValue,MainClient,Achievement,HasAuthorization,HasDrawing,AgentClass,HasImportMaterial,Award,CreateUser,CreateTime,Validate)values";
            strInsert += " ('" + bas.Sid + "','" + bas.DeclareUnitID + "','" + bas.DeclareDate + "','" + bas.SupplierType + "','" + bas.OtherType + "','" + bas.COMNameC + "','" + bas.COMShortName + "','" + bas.COMWebsite + "','" + bas.COMArea + "','" + bas.COMNameE + "','" + bas.COMCountry + "','" + bas.COMRAddress + "','" + bas.COMCreateDate + "','" + bas.TaxRegistrationNo + "','" + bas.BusinessLicenseNo + "','" + bas.ComAddress + "','" + bas.COMLegalPerson + "','" + bas.COMFactoryAddress + "','" + bas.COMFactoryArea + "','" + bas.OrganizationCode + "','" + bas.COMGroup + "','" + bas.RegisteredCapital + "','" + bas.CapitalUnit + "','" + bas.IsCooperate + "','" + bas.BankName + "','" + bas.BankAccount + "','" + bas.StaffNum + "','" + bas.EnterpriseType + "','" + bas.BusinessDistribute + "','" + bas.BillingWay + "','" + bas.Turnover + "','" + bas.DevelopStaffs + "','" + bas.QAStaffs + "','" + bas.ProduceStaffs + "','" + bas.Relation + "','" + bas.HasRegulation + "','" + bas.ProductLineNum + "','" + bas.WorkTime_Start + "','" + bas.WorkTime_End + "','" + bas.WorkDay_Start + "','" + bas.WorkDay_End + "','" + bas.FAX + "','" + bas.BusinessScope + "','" + bas.IsrankingIn5 + "','" + bas.RankingType + "','" + bas.Ranking + "','" + bas.ScaleType + "','" + bas.QualityStandard + "','" + bas.AnnualOutput + "','" + bas.AnnualOutputValue + "','" + bas.MainClient + "','" + bas.Achievement + "','" + bas.HasAuthorization + "','','','','" + bas.Award + "','" + acc.UserName + "','" + DateTime.Now + "','v')";
            return strInsert;
        }
        private static string InsertNewBasTwo(Tk_SupplierBas bas, string strInsert, Acc_Account acc)
        {
            strInsert += "insert into tk_SupplierBas(Sid,DeclareUnitID,DeclareDate,SupplierType,OtherType,COMNameC,COMShortName,";//
            strInsert += "COMWebsite,COMArea,COMNameE,COMCountry,COMRAddress,COMCreateDate,TaxRegistrationNo,BusinessLicenseNo,";
            strInsert += "ComAddress,COMLegalPerson,COMFactoryAddress,COMFactoryArea,OrganizationCode,COMGroup,RegisteredCapital,CapitalUnit,IsCooperate,BankName,BankAccount,StaffNum,EnterpriseType,BusinessDistribute,BillingWay,Turnover,DevelopStaffs,QAStaffs,ProduceStaffs,Relation,HasRegulation,ProductLineNum,WorkTime_Start,WorkTime_End,WorkDay_Start,WorkDay_End,FAX,BusinessScope,IsrankingIn5,RankingType,Ranking,ScaleType,QualityStandard,AnnualOutput,AnnualOutputValue,MainClient,Achievement,HasAuthorization,HasDrawing,AgentClass,HasImportMaterial,Award,CreateUser,CreateTime,Validate,ThreeCertity,WState,NState)values";
            strInsert += " ('" + bas.Sid + "','" + bas.DeclareUnitID + "','" + bas.DeclareDate + "','" + bas.SupplierType + "','" + bas.OtherType + "','" + bas.COMNameC + "','" + bas.COMShortName + "','" + bas.COMWebsite + "','" + bas.COMArea + "','" + bas.COMNameE + "','" + bas.COMCountry + "','" + bas.COMRAddress + "','" + bas.COMCreateDate + "','" + bas.TaxRegistrationNo + "','" + bas.BusinessLicenseNo + "','" + bas.ComAddress + "','" + bas.COMLegalPerson + "','" + bas.COMFactoryAddress + "','" + bas.COMFactoryArea + "','" + bas.OrganizationCode + "','" + bas.COMGroup + "','" + bas.RegisteredCapital + "','" + bas.CapitalUnit + "','" + bas.IsCooperate + "','" + bas.BankName + "','" + bas.BankAccount + "','" + bas.StaffNum + "','" + bas.EnterpriseType + "','" + bas.BusinessDistribute + "','" + bas.BillingWay + "','" + bas.Turnover + "','" + bas.DevelopStaffs + "','" + bas.QAStaffs + "','" + bas.ProduceStaffs + "','" + bas.Relation + "','" + bas.HasRegulation + "','" + bas.ProductLineNum + "','" + bas.WorkTime_Start + "','" + bas.WorkTime_End + "','" + bas.WorkDay_Start + "','" + bas.WorkDay_End + "','" + bas.FAX + "','" + bas.BusinessScope + "','" + bas.IsrankingIn5 + "','','','" + bas.ScaleType + "','" + bas.QualityStandard + "','" + bas.AnnualOutput + "','" + bas.AnnualOutputValue + "','" + bas.MainClient + "','" + bas.Achievement + "','" + bas.HasAuthorization + "','" + bas.HasDrawing + "','" + bas.AgentClass + "','" + bas.HasImportMaterial + "','" + bas.Award + "','" + acc.UserName + "','" + DateTime.Now + "','v','" + bas.ThreeCertity + "','0','" + bas.NState + "')";
            return strInsert;
        }
        private static string InsertNewBasThree(Tk_SupplierBas bas, string strInsert, Acc_Account acc)
        {
            strInsert += "insert into tk_SupplierBas(Sid,DeclareUnitID,DeclareDate,SupplierType,OtherType,COMNameC,COMShortName,";//
            strInsert += "COMWebsite,COMArea,COMNameE,COMCountry,COMRAddress,COMCreateDate,TaxRegistrationNo,BusinessLicenseNo,";
            strInsert += "ComAddress,COMLegalPerson,COMFactoryAddress,COMFactoryArea,OrganizationCode,COMGroup,RegisteredCapital,CapitalUnit,IsCooperate,BankName,BankAccount,StaffNum,EnterpriseType,BusinessDistribute,BillingWay,Turnover,DevelopStaffs,QAStaffs,ProduceStaffs,Relation,HasRegulation,ProductLineNum,WorkTime_Start,WorkTime_End,WorkDay_Start,WorkDay_End,FAX,BusinessScope,IsrankingIn5,RankingType,Ranking,ScaleType,QualityStandard,AnnualOutput,AnnualOutputValue,MainClient,Achievement,HasAuthorization,HasDrawing,AgentClass,HasImportMaterial,Award,CreateUser,CreateTime,Validate,ThreeCertity,WState,NState)values";
            strInsert += " ('" + bas.Sid + "','" + bas.DeclareUnitID + "','" + bas.DeclareDate + "','" + bas.SupplierType + "','" + bas.OtherType + "','" + bas.COMNameC + "','" + bas.COMShortName + "','" + bas.COMWebsite + "','" + bas.COMArea + "','" + bas.COMNameE + "','" + bas.COMCountry + "','" + bas.COMRAddress + "','" + bas.COMCreateDate + "','" + bas.TaxRegistrationNo + "','" + bas.BusinessLicenseNo + "','" + bas.ComAddress + "','" + bas.COMLegalPerson + "','" + bas.COMFactoryAddress + "','" + bas.COMFactoryArea + "','" + bas.OrganizationCode + "','" + bas.COMGroup + "','" + bas.RegisteredCapital + "','" + bas.CapitalUnit + "','" + bas.IsCooperate + "','" + bas.BankName + "','" + bas.BankAccount + "','" + bas.StaffNum + "','" + bas.EnterpriseType + "','" + bas.BusinessDistribute + "','" + bas.BillingWay + "','" + bas.Turnover + "','" + bas.DevelopStaffs + "','" + bas.QAStaffs + "','" + bas.ProduceStaffs + "','" + bas.Relation + "','" + bas.HasRegulation + "','" + bas.ProductLineNum + "','" + bas.WorkTime_Start + "','" + bas.WorkTime_End + "','" + bas.WorkDay_Start + "','" + bas.WorkDay_End + "','" + bas.FAX + "','" + bas.BusinessScope + "','" + bas.IsrankingIn5 + "','','','" + bas.ScaleType + "','" + bas.QualityStandard + "','" + bas.AnnualOutput + "','" + bas.AnnualOutputValue + "','" + bas.MainClient + "','" + bas.Achievement + "','" + bas.HasAuthorization + "','','','','" + bas.Award + "','" + acc.UserName + "','" + DateTime.Now + "','v','" + bas.ThreeCertity + "','0','" + bas.NState + "')";
            return strInsert;
        }
        public static bool InsertDetail(tk_SProcessInfo process, ref string Err)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intLog = 0;
            string upState = "";
            string strLog = "";
            //string strBas = "";
            //int intupBas = 0;
            string suglog = "";
            int intsuglog = 0;

            string strInsert = "insert into tk_SProcessInfo(SID,Reason,DeclareUser,DeclareUnit,ReviewDate,CreateUser,CreateTime,Validate,Time1,ApprovalState,Opinions) values('" + process.SID + "','" + process.Reason + "','" + process.DeclareUser + "','" + process.DeclareUnit + "','" + process.ReviewDate + "','" + process.CreateUser + "','" + process.CreateTime + "','" + process.Validate + "','" + DateTime.Now + "','" + 0 + "','" + process.Opinions + "')";
            if (strInsert != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
            }
            //根据处理意见改变状态
            if (intInsert == 1 && process.Opinions == "0")//停止供货
            {
                upState += "update tk_SupplierBas set state='" + 21 + "',WState='1' where SID='" + process.SID + "'";
            }
            else if (intInsert == 1 && process.Opinions == "1")
            {
                upState += "update tk_SupplierBas set state='" + 24 + "',WState='1' where SID='" + process.SID + "'";
            }
            else
            {
                upState += "update tk_SupplierBas set state='" + 27 + "',WState='1' where SID='" + process.SID + "'";
            }
            if (upState != "")
            {
                intUpdate = SQLBase.ExecuteNonQuery(upState, "SupplyCnn");
            }
            if (intUpdate >= 1)
            {

                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','准出供应商申请','准出供应商申请成功','" + DateTime.Now + "','" + process.CreateUser + "','准出供应商申请结果')";
            }
            if (process.Opinions == "0")
            {
                process.Opinions = "停止供货";
            }
            else if (process.Opinions == "1")
            {
                process.Opinions = "暂停供货";
            }
            else
            {
                process.Opinions = "淘汰供应商";
            }
            if (process.Opinions == "停止供货")//停止供货
            {
                suglog += "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.Opinions + "','" + process.Reason + "','" + DateTime.Now + "','" + process.CreateUser + "','准出供应商申请')";
                // strBas = "update tk_SupplierBas set State='" + 21 + "' where sid='" + process.SID + "'";
            }
            if (process.Opinions == "暂停供货")//暂停供货
            {
                suglog += "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.Opinions + "','" + process.Reason + "','" + DateTime.Now + "','" + process.CreateUser + "','准出供应商申请')";
                // strBas = "update tk_SupplierBas set State='" + 24 + "' where sid='" + process.SID + "'";
            }
            if (process.Opinions == "淘汰供应商")//淘汰供应商
            {
                suglog += "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.Opinions + "','" + process.Reason + "','" + DateTime.Now + "','" + process.CreateUser + "','准出供应商申请')";
                // strBas = "update tk_SupplierBas set State='" + 27 + "' where sid='" + process.SID + "'";
            }

            #region MyRegion
            try
            {
                //if (strBas != "")
                //    intupBas = SQLBase.ExecuteNonQuery(strBas, "SupplyCnn");
                if (suglog != "") intsuglog = SQLBase.ExecuteNonQuery(suglog, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            #endregion

            return (intInsert + intUpdate + intLog + intsuglog) >= 4;
        }
        public static bool InsertFZR(tk_SProcessInfo process, ref string Err)
        {
            int intupdate = 0;
            int intLog = 0;
            string strLog = "";
            string strBas = "";
            string sqlUPbas = "";
            int UPbas = 0;
            int intbas = 0;

            //string OpinionsD = Regex.Replace(process.OpinionsD, "\n", ""); //(process.OpinionsD).Replace();
            //负责人审批完后需将主表的state=暂停供货
            string sql = "update tk_SProcessInfo set OpinionsD='" + process.OpinionsD + "',Approval1User='" + process.Approval1User + "',Approval1='" + 1 + "',SPState='" + 1 + "',ApprovalTime1='" + process.ApprovalTime1 + "',ISAgree='" + process.ISAgree + "' where sid='" + process.SID + "'";
            //sqlUPbas = " UPDATE tk_SupplierBas SET State='30' where SID='" + process.SID + "'";
            if (sql != "")
            {
                intupdate = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
            }
            if (process.ISAgree == "0")
            {
                process.ISAgree = "是";
            }
            else
            {
                process.ISAgree = "否";
            }
            if (intupdate >= 1)
            {
                //UPbas = SQLBase.ExecuteNonQuery(sqlUPbas, "SupplyCnn");
                sqlUPbas = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.ISAgree + "','" + process.OpinionsD + "','" + DateTime.Now + "','" + process.CreateUser + "','部门级审批')";
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','部门级审批','部门级审批成功','" + DateTime.Now + "','" + process.CreateUser + "','部门级审批结果')";
            }

            #region MyRegion
            if (process.Opinions == "0" && process.ISAgree == "是")//停止供货
            {
                strBas = "update tk_SupplierBas set State='22',WState='1' where sid='" + process.SID + "'";
            }
            else if (process.Opinions == "0" && process.ISAgree == "否")
            {
                strBas = "update tk_SupplierBas set State='31',WState='1' where sid='" + process.SID + "'";
            }
            else if (process.Opinions == "1" && process.ISAgree == "是")//暂停供货
            {
                strBas = "update tk_SupplierBas set State='25',WState='1' where sid='" + process.SID + "'";
            }
            else if (process.Opinions == "1" && process.ISAgree == "否")
            {
                strBas = "update tk_SupplierBas set State='31',WState='1' where sid='" + process.SID + "'";
            }
            else if (process.Opinions == "2" && process.ISAgree == "是")//淘汰供应商
            {
                strBas = "update tk_SupplierBas set State='28',WState='1' where sid='" + process.SID + "'";
            }
            else
            {
                strBas = "update tk_SupplierBas set State='31',WState='1' where sid='" + process.SID + "'";
            }
            #endregion
            try
            {
                if (sqlUPbas != "")
                    UPbas = SQLBase.ExecuteNonQuery(sqlUPbas, "SupplyCnn");
                if (strBas != "")
                    intbas = SQLBase.ExecuteNonQuery(strBas, "SupplyCnn");
                if (strLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intupdate + intLog + UPbas + intbas) >= 1;
        }
        public static bool InsertBM(tk_SProcessInfo process, ref string Err)
        {
            int intupdate = 0;
            int intLog = 0;
            string strLog = "";
            string sqlUPbas = "";
            int UPbas = 0;
            string OpinionsD = Regex.Replace(process.OpinionsD, "\n", "");
            //负责人审批完后需将主表的state=暂停供货
            string sql = "	INSERT INTO tk_SProcessInfo (OpinionsD,Approval1User,Approval1,SPState,ApprovalTime1,ISAgree)VALUES('" + OpinionsD + "','" + process.Approval1User + "','1','1','" + process.ApprovalTime1 + "','" + process.ISAgree + "') where sid='" + process.SID + "'";
            if (sql != "")
            {
                intupdate = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
            }
            if (process.ISAgree == "0" && intupdate == 1)
            {
                strLog = "update tk_SupplierBas set State='" + 61 + "' where sid='" + process.SID + "'";
            }
            else
            {
                strLog = "update tk_SupplierBas set State='" + 63 + "' where sid='" + process.SID + "'";
            }
            if (intupdate >= 1)
            {
                sqlUPbas = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.ISAgree + "','" + process.OpinionsD + "','" + DateTime.Now + "','" + process.CreateUser + "','部门级恢复审批')";
            }
            try
            {
                if (sqlUPbas != "")
                    UPbas = SQLBase.ExecuteNonQuery(sqlUPbas, "SupplyCnn");
                if (strLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intupdate + UPbas + intLog) >= 3;
        }
        public static bool Insertbumen(tk_SProcessInfo process, ref string Err)
        {
            int intupdate = 0;
            int intLog = 0;
            string strLog = "";
            string sqlUPbas = "";
            int UPbas = 0;
            string OpinionsD = Regex.Replace(process.OpinionsD, "\n", "");
            //负责人审批完后需将主表的state=暂停供货
            string sql = "	INSERT INTO tk_SProcessInfo (OpinionsD,Approval1User,Approval1,SPState,ApprovalTime1,ISAgree)VALUES('" + OpinionsD + "','" + process.Approval1User + "','1','1','" + process.ApprovalTime1 + "','" + process.ISAgree + "') where sid='" + process.SID + "'";
            if (sql != "")
            {
                intupdate = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
            }
            if (process.ISAgree == "0" && intupdate == 1)
            {
                strLog = "update tk_SupplierBas set State='" + 51 + "' where sid='" + process.SID + "'";
            }
            else
            {
                strLog = "update tk_SupplierBas set State='" + 55 + "' where sid='" + process.SID + "'";
            }
            if (intupdate >= 1)
            {
                sqlUPbas = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','" + process.ISAgree + "','" + process.OpinionsD + "','" + DateTime.Now + "','" + process.CreateUser + "','部门级恢复审批')";
                //strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','部门级审批','部门级审批成功','" + DateTime.Now + "','" + process.CreateUser + "','部门级审批结果')";
            }
            try
            {
                if (sqlUPbas != "")
                    UPbas = SQLBase.ExecuteNonQuery(sqlUPbas, "SupplyCnn");
                if (strLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intupdate + UPbas + intLog) >= 3;
        }

        public static bool UPrecover(tk_SProcessInfo process, ref string Err)
        {
            int intupdate = 0;
            int intLog = 0;
            string strLog = "";
            string strbas = "";
            int intbas = 0;
            //负责人审批完后需将主表的state=暂停供货
            string sql = "update tk_SProcessInfo set RecoverReson='" + process.RecoverReson + "' where sid='" + process.SID + "'";
            if (sql != "")
            {
                intupdate = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
            }
            if (intupdate >= 1)
            {
                strbas = "update tk_SupplierBas set state='72' where sid='" + process.SID + "'";
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','恢复供货','恢复供货成功','" + DateTime.Now + "','" + process.CreateUser + "','恢复供货结果')";
            }

            try
            {
                if (strLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                if (strbas != "")
                    intbas = SQLBase.ExecuteNonQuery(strbas, "SupplyCnn");
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intupdate + intLog + intbas) >= 3;
        }
        public static bool InsertYearRes(tk_SProcessInfo process, ref string Err)
        {
            int intupdate = 0;
            int intupBas = 0;
            string strBas = "";
            int intLog = 0;
            //负责人审批完后需将主表的state=暂停供货
            string sql = "update tk_SProcessInfo set Opinions='" + process.Opinions + "',OpinionsD='" + process.OpinionsD + "',Approval1User='" + process.Approval1User + "',Approval1='" + 1 + "',SPState='" + 1 + "',ApprovalTime1='" + process.ApprovalTime1 + "' where sid='" + process.SID + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + process.SID + "','准出处理建议','准出处理建议成功','" + DateTime.Now + "','" + process.CreateUser + "','准出处理建议结果')";
            if (process.Opinions == "0")//停止供货
            {
                strBas = "update tk_SupplierBas set State='" + 23 + "' where sid='" + process.SID + "'";
            }
            if (process.Opinions == "1")//暂停供货
            {
                strBas = "update tk_SupplierBas set State='" + 26 + "' where sid='" + process.SID + "'";
            }
            if (process.Opinions == "2")//淘汰供应商
            {
                strBas = "update tk_SupplierBas set State='" + 29 + "' where sid='" + process.SID + "'";
            }
            try
            {
                if (sql != "")
                    intupdate = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                if (strBas != "")
                    intupBas = SQLBase.ExecuteNonQuery(strBas, "SupplyCnn");
                if (strLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");


            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intupdate + intupBas + intLog) >= 3;
        }
        public static int InsertContract(Tk_SContactPerson conPer, string Press, string CSize, string length, ref string a_Err)
        {
            int intInsert = 0;
            int intInsertCont = 0;
            SQLTrans sqltrans = new SQLTrans();
            sqltrans.Open("SupplyCnn");
            string[] arrPress = Press.Split(',');
            string[] arrSize = CSize.Split(',');
            string[] arrLenth = length.Split(',');
            string strInsertCont = "";
            string strInsert = GSqlSentence.GetInsertInfoByD<Tk_SContactPerson>(conPer, "Tk_SContactPerson");
            try
            {
                if (strInsert != "")
                {
                    intInsert = sqltrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                }
                if (strInsertCont != "")
                {
                    intInsertCont = sqltrans.ExecuteNonQuery(strInsertCont, CommandType.Text, null);
                }
                sqltrans.Close(true);
            }
            catch (SqlException e)
            {
                sqltrans.Close(false);
                a_Err = e.Message;
                return -1;
            }
            return intInsert;

        }

        public static bool InsertFile(tk_SFileInfo fileInfo, ref string Err, HttpFileCollection filc)
        {
            int intInsert = 0;
            int intLog = 0;
            string strInsert = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            for (int i = 0; i < filc.Count; i++)
            {
                string FileName = "";
                byte[] fileByte = new byte[0];
                FileName = filc[i].FileName.Substring(filc[i].FileName.LastIndexOf('\\') + 1);
                if (FileName.Length > 0)
                {
                    fileInfo.Ffilename = FileName;
                    int fileLength = filc[i].ContentLength;
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        filc[i].InputStream.Read(fileByte, 0, fileLength);
                    }
                    SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@fileByte",fileByte)
                };
                    strInsert = "insert into tk_SFileInfo (SID,FType,TypeO,Item,ItemO,FFileName,FileType,"
        + "FileInfo,CreateUser,CreateTime,FTimeOut,Validate) values ("
        + "'" + fileInfo.Sid + "','" + fileInfo.Ftype + "','" + fileInfo.Typeo + "','" + fileInfo.Item + "','" + fileInfo.Itemo + "','" + fileInfo.Ffilename + "',"
        + "'" + fileInfo.Filetype + "',@fileByte,'" + fileInfo.Createuser + "','" + fileInfo.Createtime + "','" + fileInfo.FTimeOut + "','" + fileInfo.Validate + "')";
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, CommandType.Text, para, "SupplyCnn");
                }
                else
                {
                    strInsert = "insert into tk_SFileInfo (SID,FType,TypeO,Item,ItemO,"
       + "CreateUser,CreateTime,FTimeOut,Validate) values ("
       + "'" + fileInfo.Sid + "','" + fileInfo.Ftype + "','" + fileInfo.Typeo + "','" + fileInfo.Item + "','" + fileInfo.Itemo + "',"
       + "'" + fileInfo.Createuser + "','" + fileInfo.Createtime + "','" + fileInfo.FTimeOut + "','" + fileInfo.Validate + "')";
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }
            }
            string strLog = "";
            if (intInsert == 1)
            {
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + fileInfo.Sid + "','新增资质信息','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增资质文件')";
            }
            try
            {
                //if (strInsert != "")
                //    intInsert = SQLBase.ExecuteNonQuery(strInsert, CommandType.Text, para, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog) >= 2;
        }
        public static bool UpdateFile(tk_SFileInfo sfi, byte[] fileByte, ref string Err)
        {
            int inupdate = 0;
            int intInsert = 0;
            int intLog = 0;
            string strupdate = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            string strInsert = "insert into tk_SFileInfoHis(SID,FID,FType,TypeO,Item,ItemO,FFileName,FileType,FileInfo,CreateUser,CreateTime,Validate,NCreateTime,NCreateUser)select SID,FID,FType,TypeO,Item,ItemO,FFileName,FileType,FileInfo,CreateUser,CreateTime,Validate,'" + DateTime.Now + "','" + acc.UserName + "'  from tk_SFileInfo where sid='" + sfi.Sid + "' and fid='" + sfi.Fid + "'";
            if (sfi.Ffilename != "")
            {
                strupdate += "update tk_SFileInfo set TypeO='" + sfi.Typeo + "',Item='" + sfi.Item + "',ItemO='" + sfi.Itemo + "',FFileName='" + sfi.Ffilename + "',FileType='" + sfi.Filetype + "',FileInfo= @fileByte,FTimeOut='" + sfi.FTimeOut + "',CreateTime='" + DateTime.Now + "'  where sid='" + sfi.Sid + "' and FID='" + sfi.Fid + "'";
            }
            else
            {
                strupdate += "update tk_SFileInfo set TypeO='" + sfi.Typeo + "',Item='" + sfi.Item + "',ItemO='" + sfi.Itemo + "',FTimeOut='" + sfi.FTimeOut + "',CreateTime='" + DateTime.Now + "'  where sid='" + sfi.Sid + "' and FID='" + sfi.Fid + "'";
            }

            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sfi.Sid + "','更新资质信息','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新资质操作')";
            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }
                if (strupdate != "")
                {
                    inupdate = SQLBase.ExecuteNonQuery(strupdate, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (inupdate + intInsert + intLog) >= 3;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfi"></param>
        /// <param name="fileByte"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool RemarkFile(tk_SFileInfo sfi, byte[] fileByte, ref string Err)
        {
            return false;
        }
        public static bool UpdateCertityMsg(tk_SCertificate scfi, byte[] filebyte, ref string Err)
        {
            int intUP = 0;
            int intInsert = 0;
            int intLog = 0;
            string strLog = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",filebyte)
            };
            string strInsert = "insert into tk_SCertificateHis(SID,FID,IsPlan,CType,CName,CCode,COrganization,CDate,CFileName,FileType,FileInfo,CreateUser,CreateTime,Validate,NCreateUser,NCreateTime)select SID,FID,IsPlan,CType,CName,CCode,COrganization,CDate,CFileName,FileType,FileInfo,CreateUser,CreateTime,Validate,'" + acc.UserName + "','" + DateTime.Now + "'  from tk_SCertificate where sid='" + scfi.Sid + "' and CreateTime='" + scfi.Createtime + "'";
            string strUp = "update tk_SCertificate set IsPlan='" + scfi.Isplan + "',CType='" + scfi.Ctype + "',CName='" + scfi.Cname + "',CCode='" + scfi.Ccode + "',COrganization='" + scfi.Corganization + "',CDate='" + scfi.Cdate + "',TimeOut='" + scfi.TimeOut + "',CFileName='" + scfi.Cfilename + "',FileType='" + scfi.FileType + "',FileInfo=@fileByte  where sid='" + scfi.Sid + "' and CreateTime='" + scfi.Createtime + "'";
            if (strUp != "")
            {
                intUP = SQLBase.ExecuteNonQuery(strUp, CommandType.Text, para, "SupplyCnn");
            }
            if (intUP >= 1)
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + scfi.Sid + "','更新证书','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新证书操作')";

            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }


                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUP + intInsert + intLog) >= 3;
        }
        public static bool UpdateNewApproval(Tk_SupplierBas bas, HttpFileCollection filc, string sid, ref string Err)
        {
            int intUpbas = 0;
            int intUpSf = 0;
            int intLog = 0;
            string strMFile = "";
            string strUpdate = "";
            string savePath = "";
            string FileName1 = "";
            string strInsertFile = "";
            string strLog1 = "";
            int intInsertFile = 0;
            int intLog1 = 0;
            tk_SFileInfo file = new tk_SFileInfo();
            tk_PriceUp price = new tk_PriceUp();
            Acc_Account acc = GAccount.GetAccountInfo();

            #region 基本信息录入
            //是否是免评供应商和供应商为
            if (bas.IsUnReview == "0" && bas.UnReviewUnit != "" || bas.UnReviewUnit != null)
            {
                strUpdate += "update Tk_SupplierBas set Evaluation1='" + bas.Evaluation1 + "', Evaluation2='" + bas.Evaluation2 + "',Evaluation3='" + bas.Evaluation3 + "',Evaluation4='" + bas.Evaluation4 + "',"
               + "Evaluation5='" + bas.Evaluation5 + "',Evaluation6='" + bas.Evaluation6 + "',Evaluation7='" + bas.Evaluation7 + "',State='" + 1 + "'";
                strUpdate += "  ,IsUnReview='" + bas.IsUnReview + "',IsURInnerUnit='" + bas.IsURInnerUnit + "',UnReviewUnit='" + bas.UnReviewUnit + "',WState='" + 0 + "'";
                strUpdate += "  ,UnReviewDesc='" + bas.UnReviewDesc + "',IsUnreviewUser='" + bas.IsUnreviewUser + "',URConfirmUser='" + bas.URConfirmUser + "'";
                strUpdate += " ,DeclareUser='" + bas.DeclareUser + "',BussinessUser='" + bas.BussinessUser + "',TecolUser='" + bas.TecolUser + "',BuyUser='" + bas.BuyUser + "',SaleUser='" + bas.SaleUser + "',ChargeUser='" + bas.ChargeUser + "',CreateTime='" + DateTime.Now + "' where  Sid='" + sid + "'";
            }
            else if (bas.IsUnReview == "0" && bas.UnReviewUnit == "" || bas.UnReviewUnit == null)
            {
                strUpdate += "update Tk_SupplierBas set Evaluation1='" + bas.Evaluation1 + "', Evaluation2='" + bas.Evaluation2 + "',Evaluation3='" + bas.Evaluation3 + "',Evaluation4='" + bas.Evaluation4 + "',"
                   + "Evaluation5='" + bas.Evaluation5 + "',Evaluation6='" + bas.Evaluation6 + "',Evaluation7='" + bas.Evaluation7 + "',State='" + 1 + "'";
                strUpdate += "  ,IsUnReview='" + bas.IsUnReview + "',IsURInnerUnit='" + bas.IsURInnerUnit + "',UnReviewUnit='" + null + "'";
                strUpdate += "  ,UnReviewDesc='" + bas.UnReviewDesc + "',IsUnreviewUser='" + bas.IsUnreviewUser + "',URConfirmUser='" + bas.URConfirmUser + "',WState='" + 0 + "'";
                strUpdate += " ,DeclareUser='" + bas.DeclareUser + "',BussinessUser='" + bas.BussinessUser + "',TecolUser='" + bas.TecolUser + "',BuyUser='" + bas.BuyUser + "',SaleUser='" + bas.SaleUser + "',ChargeUser='" + bas.ChargeUser + "',CreateTime='" + DateTime.Now + "' where  Sid='" + sid + "'";
            }
            else
            {
                strUpdate += "update Tk_SupplierBas set Evaluation1='" + bas.Evaluation1 + "', Evaluation2='" + bas.Evaluation2 + "',Evaluation3='" + bas.Evaluation3 + "',Evaluation4='" + bas.Evaluation4 + "',"
               + "Evaluation5='" + bas.Evaluation5 + "',Evaluation6='" + bas.Evaluation6 + "',Evaluation7='" + bas.Evaluation7 + "',State='" + 1 + "',IsUnReview='" + bas.IsUnReview + "',DeclareUser='" + bas.DeclareUser + "',WState='" + 0 + "'";
                strUpdate += ",BussinessUser='" + bas.BussinessUser + "',TecolUser='" + bas.TecolUser + "',BuyUser='" + bas.BuyUser + "',SaleUser='" + bas.SaleUser + "',ChargeUser='" + bas.ChargeUser + "',UnReviewUnit='" + null + "',CreateTime='" + DateTime.Now + "' ";
                strUpdate += "   where  Sid='" + sid + "'";
            }
            if (strUpdate != "")
            {
                intUpbas = SQLBase.ExecuteNonQuery(strUpdate, CommandType.Text, null, "SupplyCnn");
            }
            #endregion
            #region 多文档上传
            //文档上传为空时判断count=0是空的，count>=1是存在文档的
            //判断可以多选
            if (bas.IsUnReview == "0")
            {
                for (int i = 0; i < filc.Count; i++)
                {
                    string FileName = "";
                    byte[] fileByte = new byte[0];
                    FileName = filc[i].FileName.Substring(filc[i].FileName.LastIndexOf('\\') + 1);
                    if (FileName.Length > 0)
                    {
                        file.Ffilename = FileName;//filc[i].FileName.Substring(filc[i].FileName.LastIndexOf('\\') + 1);
                        int fileLength = filc[i].ContentLength;
                        if (fileLength != 0)
                        {
                            fileByte = new byte[fileLength];
                            filc[i].InputStream.Read(fileByte, 0, fileLength);
                        }
                        SqlParameter[] parms = new SqlParameter[]
                    {
                        new SqlParameter("@FileByte",fileByte)
                    };

                        strMFile = "insert into tk_SFileInfo(sid,FFileName,FileInfo,createuser,createtime,validate,FTimeOut) values(";
                        strMFile += " '" + bas.Sid + "','" + file.Ffilename + "',@FileByte,'" + acc.UserName + "','" + DateTime.Now + "','v','" + DateTime.Now + "' )";
                        intUpSf = SQLBase.ExecuteNonQuery(strMFile, CommandType.Text, parms, "SupplyCnn");//返回行数

                    }
                    //else
                    //{
                    //    strMFile = "insert into tk_SFileInfo(sid,createuser,createtime,validate) values(";
                    //    strMFile += " '" + bas.Sid + "','" + acc.UserName + "','" + DateTime.Now + "','v' )";
                    //    intUpSf = SQLBase.ExecuteNonQuery(strMFile, "SupplyCnn");//返回行数
                    //}
                }
            }
            else
            {
                for (int i = 0; i < filc.Count; i++)
                {
                    if (filc[i] == null || filc[i].ContentLength <= 0)
                    {
                        Err = "文件不能为空";
                    }
                    else
                    {
                        string filename = Path.GetFileName(filc[i].FileName);
                        int filesize = filc[i].ContentLength;//获取上传文件的大小单位为字节byte
                        string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                        string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                        int Maxsize = 10000 * 1024;//定义上传文件的最大空间大小为10M
                        string FileType = ".xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt,.JPG,.PNG";//定义上传文件的类型字符串

                        FileName1 = NoFileName + fileEx;//+ DateTime.Now.ToString("yyyyMMddhhmmss")

                        if (!FileType.Contains(fileEx))
                        {
                            Err = "文件类型不对，只能上传 .xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt 格式的文件";
                        }
                        if (filesize >= Maxsize)
                        {
                            Err = "上传文件超过10M，不能上传";
                        }
                        string path = System.Configuration.ConfigurationSettings.AppSettings["upload"];
                        if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
                        {
                            Directory.CreateDirectory(path);
                        }
                        savePath = Path.Combine(path, FileName1);
                        filc[i].SaveAs(savePath);
                        strInsertFile = "insert into tk_PriceUp (SID,PriceName ,PriceInfo ,PriceTime,Createuser,Validate) ";
                        strInsertFile += " values ('" + bas.Sid + "','" + FileName1 + "','" + savePath + "','" + DateTime.Now + "','" + acc.UserName + "','v')";
                        intInsertFile = SQLBase.ExecuteNonQuery(strInsertFile, "SupplyCnn");
                        if (intInsertFile == 1)
                        {
                            strLog1 = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','报价/比价单信息','上传成功','" + DateTime.Now + "','" + acc.UserName + "','上传报价/比价单')";
                        }
                    }
                }
            }
            #endregion

            string strLog = "";
            if (intUpbas == 1)
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','内部评审','新增内部评审成功','" + DateTime.Now + "','" + acc.UserName + "','内部评审')";
            else
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','内部评审','新增内部评审失败','" + DateTime.Now + "','" + acc.UserName + "','内部评审')";
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, CommandType.Text, null, "SupplyCnn");
                }
                if (strLog1 != "")
                    intLog1 = SQLBase.ExecuteNonQuery(strLog1, "SupplyCnn");

            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }

            return (intUpbas + intUpSf + intLog) >= 2;
        }
        public static bool AddFZSugestion(tk_SUPSugestion sgs, string sid, ref string Err)
        {
            int AddSGS = 0;
            int AddLog = 0;
            int intUpBas = 0;
            string strSGS = "";
            string strUpBas = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            strSGS = "insert into tk_SUPSugestion(SID,SState,Sperson,SCreate,SContent)values('" + sgs.Sid + "','" + sgs.SState + "','" + sgs.Sperson + "','" + sgs.SCreate + "','" + sgs.SContent + "')";
            if (strSGS != "")
            {
                AddSGS = SQLBase.ExecuteNonQuery(strSGS, CommandType.Text, null, "SupplyCnn");
            }
            //选是
            if (sgs.SState == "0" && AddSGS == 1)
            {
                strUpBas += "Update Tk_SupplierBas set state='" + 2 + "' where sid='" + sgs.Sid + "'";
            }
            else
            {
                strUpBas += "Update Tk_SupplierBas set state='" + -4 + "' where sid='" + sgs.Sid + "'";
            }
            string strLog = "";
            if (AddSGS == 1)
            {
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sgs.Sid + "','负责人内部评审意见','新增负责人意见成功','" + DateTime.Now + "','" + acc.UserName + "','负责人意见')";
            }
            else
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sgs.Sid + "','负责人内部评审意见','新增负责人意见失败','" + DateTime.Now + "','" + acc.UserName + "','负责人意见')";
            try
            {
                if (strUpBas != "")
                {
                    intUpBas = SQLBase.ExecuteNonQuery(strUpBas, CommandType.Text, null, "SupplyCnn");
                }
                if (strLog != "")
                {
                    AddLog = SQLBase.ExecuteNonQuery(strLog, CommandType.Text, null, "SupplyCnn");
                }

            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (AddLog + AddSGS + intUpBas) >= 3;


        }
        public static bool AddBSSugestion(tk_SUPSugestion sgs, string sid, ref string Err)
        {
            int AddSGS = 0;
            int AddLog = 0;
            int intUpBas = 0;
            string strSGS = "";
            string strUpBas = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            strSGS = "insert into tk_SUPSugestion(SID,SState,Sperson,SCreate,SContent)values('" + sgs.Sid + "','" + sgs.SState + "','" + sgs.Sperson + "','" + sgs.SCreate + "','" + sgs.SContent + "')";
            if (strSGS != "")
            {
                AddSGS = SQLBase.ExecuteNonQuery(strSGS, CommandType.Text, null, "SupplyCnn");
            }

            if (sgs.SState == "0" && AddSGS == 1)
            {
                strUpBas = "Update Tk_SupplierBas set state='" + 51 + "' where sid='" + sgs.Sid + "'";
            }
            else
            {
                strUpBas += "Update Tk_SupplierBas set state='" + 53 + "' where sid='" + sgs.Sid + "'";
            }

            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sgs.Sid + "','恢复供应商建议','新增恢复供应商成功','" + DateTime.Now + "','" + acc.UserName + "','恢复供应商建议')";
            try
            {
                if (strUpBas != "")
                {
                    intUpBas = SQLBase.ExecuteNonQuery(strUpBas, CommandType.Text, null, "SupplyCnn");
                }
                if (strLog != "")
                {
                    AddLog = SQLBase.ExecuteNonQuery(strLog, CommandType.Text, null, "SupplyCnn");
                }

            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (AddLog + AddSGS + intUpBas) >= 3;
        }
        public static bool YearDeal(tk_SYRDetail yearDetal, string sid, Tk_SupplierBas bas, ref string Err)
        {
            int intInsert = 0;
            int intLog = 0;
            int intUpBas = 0;
            int intdel = 0;
            string strupBas = "";
            string delyear = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            string strinsert = GSqlSentence.GetInsertInfoByD<tk_SYRDetail>(yearDetal, "tk_SYRDetail");
            if (strinsert != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strinsert, "SupplyCnn");
            }

            //先将数据写入数据库中，然后再将写入数据中的年份获取到
            #region 获取年份
            string year = (DateTime.Now.Year).ToString();//当前年
            string comYear = "select  Year from  tk_SYRDetail where sid='" + sid + "'";
            DataTable dtyear = SQLBase.FillTable(comYear, "SupplyCnn");
            //需要判断一下，当前年份和表中的年度进行比较，不相等则显示【返回合格供应商】
            if (year == dtyear.Rows[0][0].ToString())
            {
                strupBas = "update Tk_SupplierBas set nState='60' where sid='" + sid + "'";
            }
            else
            {
                strupBas = "update Tk_SupplierBas set State='10' where sid='" + sid + "'";
                delyear = "delete from tk_SYRDetail where sid='" + sid + "'";
            }
            #endregion
            string strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + yearDetal.SID + "','新增年度评审信息','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增年度评审')";
            try
            {
                if (strupBas != "")
                    intUpBas = SQLBase.ExecuteNonQuery(strupBas, "SupplyCnn");
                if (delyear != "")
                    intdel = SQLBase.ExecuteNonQuery(delyear, "SupplyCnn");
                if (strlog != "")
                    intLog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");

            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog + intUpBas + intdel) >= 3;
        }
        public static bool InsertCertifi(tk_SCertificate certifi, byte[] fileByte, ref string Err)
        {
            int intInsert = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            string strInsert = "insert into tk_SCertificate (SID,IsPlan,UserId,Unite,CType,CName,CCode,COrganization,CDate,TimeOut,"
+ "CFileName,FileType,FileInfo,CreateUser,CreateTime,Validate) values ("
+ "'" + certifi.Sid + "','" + certifi.Isplan + "','" + acc.UserID + "','" + acc.UnitID + "','" + certifi.Ctype + "','" + certifi.Cname + "','" + certifi.Ccode + "','" + certifi.Corganization + "',"
+ "'" + certifi.Cdate + "','" + certifi.TimeOut + "','" + certifi.Cfilename + "','" + certifi.FileType + "',@fileByte,'" + certifi.Createuser + "','" + certifi.Createtime + "','" + certifi.Validate + "')";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + certifi.Sid + "','新增证书信息','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增证书')";
            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog) >= 2;
        }
        public static bool InsertPro(tk_SProducts product, byte[] fileByte, ref string Err)
        {
            int intInsert = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            string strInsert = "insert into tk_SProducts (SID,Ptype,ProductID,ProductName,Standard,MeasureUnit,DetailDesc,"
+ "Price,OriginPlace,FFileName,FileInfo,CreateUser,CreateTime,Validate,BYTime) values ("
+ "'" + product.Sid + "','" + product.Ptype + "','" + product.Productid + "','" + product.Productname + "','" + product.Standard + "','" + product.Measureunit + "',"
+ "'" + product.Detaildesc + "'," + product.Price + ",'" + product.Originplace + "','" + product.FFileName + "',@fileByte,'" + product.Createuser + "','" + product.Createtime + "','" + product.Validate + "','" + product.BYTtime + "')";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + product.Sid + "','新增产品信息','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增产品')";
            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog) >= 2;
        }
        /// <summary>
        /// 淘汰供应商恢复方法
        /// </summary>
        /// <param name="fb"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool SaveFeedBack(tk_FeedBack fb, ref string Err, Tk_SupplierBas bas)
        {
            int intInsert = 0;
            int intLog = 0;
            int upState = 0;
            string strUpState = "";
            string strLog = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            string strInsertBack = "insert into tk_FeedBack values('" + fb.SID + "','" + fb.FName + "','" + fb.FContent + "','" + fb.FReason + "','" + fb.FTime + "')";
            if (strInsertBack != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strInsertBack, "SupplyCnn");
            }
            if (intInsert == 1 && bas.NState != 64)
            {
                strUpState = "update tk_SupplierBas set state='50' where sid='" + fb.SID + "'";
            }
            else if (intInsert == 1 && bas.NState == 64)
            {
                strUpState = "update tk_SupplierBas set nstate='60' where sid='" + fb.SID + "'";
            }
            if (strUpState != "")
            {
                upState = SQLBase.ExecuteNonQuery(strUpState, "SupplyCnn");
            }
            if (upState >= 1)
            {
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + fb.SID + "','申请恢复成合格供应商','" + fb.FReason + "','" + DateTime.Now + "','" + acc.UserName + "','恢复供应商')";
            }
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + upState + intLog) >= 3;
        }
        public static bool ResRe(Tk_SupplierBas bas, string sid, ref string Err)
        {
            int intUp = 0;
            int intlog = 0;
            int state = 0;
            string strlog = "";
            string upstate = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            if (bas.ResState == 1)//如果给的意见是淘汰则状态置成最终评审未通过
            {
                upstate = "update Tk_SupplierBas set State=5 ,WState='0' where sid='" + sid + "'"; //
            }
            //判断审批过程表中的多条数据

            string strUp = "update Tk_SupplierBas set ApprovalRes='" + bas.ApprovalRes + "',Approval4User='" + bas.Approval4User + "',AppTime='" + bas.AppTime + "',ResState='" + bas.ResState + "',State=10 ,WState='1' where sid='" + sid + "'";
            if (strUp != "")
            {
                intUp = SQLBase.ExecuteNonQuery(strUp, "SupplyCnn");
            }
            if (intUp >= 1)
            {
                strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','最终评审结果','最终评审更新成功','" + DateTime.Now + "','" + acc.UserName + "','最终评审结果')";
            }
            else
                strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','最终评审结果','最终评审更新失败','" + DateTime.Now + "','" + acc.UserName + "','最终评审结果')";
            try
            {

                if (upstate != "")
                {
                    state = SQLBase.ExecuteNonQuery(upstate, "SupplyCnn");
                }
                if (strlog != "")
                {
                    intlog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUp + intlog + state) >= 2;
        }
        /// <summary>
        /// 有问题
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static int InsertApprval(tk_SApproval approval, string sid, ref string Err)
        {
            int intInsert = 0;
            int intuP = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string sql = "insert into tk_SApproval (PID,SID,ApprovalType,ApprovalLever,ApprovalContent,ApprovalPersons,"
            + "ApprovalTime,CreateUser,CreateTime,Validate,State) values("
            + "'" + approval.PID + "','" + approval.SID + "','" + approval.ApprovalType + "','" + approval.ApprovalLever + "','" + approval.ApprovalContent + "',"
            + "'" + approval.ApprovalPersons + "','" + approval.ApprovalTime + "',"
            + "'" + approval.CreateUser + "','" + approval.CreateTime + "','" + approval.Validate + "','" + approval.State + "')";
            string strUp = "update tk_SupplierBas set State='" + 2 + "' where SID='" + sid + "'";
            try
            {
                if (sql != "")
                    intInsert = sqlTrans.ExecuteNonQuery(sql, CommandType.Text, null);
                if (strUp != "")
                {
                    intuP = sqlTrans.ExecuteNonQuery(strUp, CommandType.Text, null);
                }
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }
            return intInsert + intuP;
        }
        public static bool UpNewApp(tk_SApproval approval, string pid, ref string Err)
        {
            int upApp = 0;
            int upBas = 0;
            // int res = 0;
            string sqlBas = "";
            // string sqlJudge = "";
            //SQLTrans sqlTrans = new SQLTrans();
            //sqlTrans.Open("SupplyCnn");
            string sql = "update tk_SApproval set IsPass='" + approval.IsPass + "',ApprovalMan='" + approval.ApprovalMan + "',Job='" + approval.Job + "',NoPassReason='" + approval.NoPassReason + "',Remark='" + approval.Remark + "',state='" + 1 + "' where  PID=(select max('" + pid + "') from tk_SApproval)";
            //string strsid = "select sid from tk_SApproval where PID='" + pid + "'";
            sqlBas = "update tk_SupplierBas set ApprovalState='" + 1 + "'where SID=(select sid from tk_SApproval where PID='" + pid + "')";
            //循环判断一下审批过程表中的state都为1的情况下
            // sqlJudge = "select * from tk_SApproval where PID='" + pid + "'  and state='" + 1 + "'";
            #region MyRegion
            // int res = SQLBase.ExecuteNonQuery(sqlJudge, CommandType.Text, null);
            //if (res > 0)
            //{
            //    sqlBas = "update tk_SupplierBas set ApprovalState='" + 1 + "'where PID='" + pid + "'";
            //}
            //else
            //{
            //    sqlBas = "update tk_SupplierBas set ApprovalState='" + 0 + "'where PID='" + pid + "'";
            //} 
            #endregion
            try
            {
                if (sql != "")
                    upApp = SQLBase.ExecuteNonQuery(sql, CommandType.Text, null);
                #region MyRegion
                //if (sqlJudge != "")
                //{
                // res = SQLBase.ExecuteNonQuery(sqlJudge, CommandType.Text, null);
                //if (res > 0)
                //{

                //}
                //else
                //{
                //    sqlBas = "update tk_SupplierBas set ApprovalState='" + 0 + "'where PID='" + pid + "'";
                //}
                //} 
                #endregion
                if (sqlBas != "")
                {
                    upBas = SQLBase.ExecuteNonQuery(sqlBas, CommandType.Text, null);
                }
                // sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                //sqlTrans.Close(false);
                Err = e.Message;
                return false;
            }
            return (upApp + upBas) > 0;
        }
        public static bool UpNewWeiguiApproval(tk_SProcessInfo processinfo, string sid, ref string Err)
        {
            int upBas = 0;
            string upSql = "update tk_SProcessInfo set Approval1='" + processinfo.Approval1 + "',Approval1User='" + processinfo.Approval1User + "',Approval2='" + processinfo.Approval2 + "',Approval2User='" + processinfo.Approval2User + "',Approval3='" + processinfo.Approval3 + "',Approval3User='" + processinfo.Approval3User + "',Approval4='" + processinfo.Approval4 + "',Approval4User='" + processinfo.Approval4User + "',Approval5='" + processinfo.Approval5 + "',Approval5User='" + processinfo.Approval5User + "' where sid='" + sid + "'";
            try
            {
                if (upSql != "")
                {
                    upBas = SQLBase.ExecuteNonQuery(upSql, CommandType.Text, null);
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return upBas > 0;
        }
        public static bool InsertServer(tk_SService server, byte[] fileByte, ref string Err)
        {
            int intInsert = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            string strInsert = "insert into tk_SService (SID,ServiceName,ServiceDesc,Purpose,FFileName,FileInfo,CreateUser,CreateTime,Validate) values ("
+ "'" + server.Sid + "','" + server.ServiceName + "','" + server.ServiceDesc + "','" + server.Purpose + "','" + server.FFileName + "',@fileByte,'" + server.CreateUser + "','" + server.CreateTime + "','" + server.Validate + "')";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + server.Sid + "','新增服务信息','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增服务')";
            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog) >= 2;
        }
        public static bool InsertAward(tk_Award bas, HttpFileCollection file, ref string a_strErr)
        {
            #region MyRegion


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

                    FileName = NoFileName + fileEx;//+ DateTime.Now.ToString("yyyyMMddhhmmss")

                    if (!FileType.Contains(fileEx))
                    {
                        a_strErr = "文件类型不对，只能上传 .xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt 格式的文件";
                    }
                    if (filesize >= Maxsize)
                    {
                        a_strErr = "上传文件超过10M，不能上传";
                    }
                    string path = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\";
                    string infos = DateTime.Now.ToString("yyyyMMdd");
                    //savePaths = infos;
                    path = path + infos;
                    if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
                    {
                        Directory.CreateDirectory(path);
                    }
                    savePath = Path.Combine(path, FileName);
                    file[i].SaveAs(savePath);
                    strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
                    strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + savePath + "','" + DateTime.Now + "','" + acc.UserName + "','v')";
                    intInsertFile = SQLBase.ExecuteNonQuery(strInsertFile, "SupplyCnn");
                    if (intInsertFile == 1)
                    {
                        strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.SID + "','曾获奖项信息','上传成功','" + DateTime.Now + "','" + acc.UserName + "','上传奖项')";
                    }
                }
            }
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                a_strErr = e.Message;
                return false;
            }
            #region MyRegion

            ////string strupdate = " INSERT into tk_Award (SID,Award,AwardInfo,AwardTime) VALUES('" + bas.SID + "','" + bas.Award + "',@fileByte,'" + DateTime.Now + "') where SID='" + bas.SID + "'";

            ////if (strupdate != "")
            ////{
            ////    intInsert = SQLBase.ExecuteNonQuery(strupdate, CommandType.Text, para, "SupplyCnn");
            ////}
            ////if (intInsert == 1)
            ////{
            ////    strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.SID + "','曾获奖项信息','上传成功','" + DateTime.Now + "','" + acc.UserName + "','上传奖项')";
            ////}
            ////try
            ////{
            ////    if (strLog != "")
            ////    {
            ////        intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
            ////    }
            ////}
            ////catch (SqlException e)
            ////{
            ////    Err = e.Message;
            ////    return false;
            ////}

            #endregion
            return (intInsertFile + intLog) >= 2;
            #endregion

            #region MyRegion
            //a_strErr = "";
            //string savePaths = "";
            //int intInsertFile = 0;
            //Acc_Account account = GAccount.GetAccountInfo();
            //SQLTrans sqlTrans = new SQLTrans();
            //sqlTrans.Open("SupplyCnn");

            ////获取上传文件的文件名
            //string FileName = "";
            //string savePath = "";
            //string filename = Path.GetFileName(file[0].FileName);
            //string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            //string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            //FileName = filename;

            //// 文件保存路径
            //string path = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\";
            //string infos = DateTime.Now.ToString("yyyyMM");
            //savePaths = infos + "\\" + bas.SID;
            //path = path + savePaths;

            ////如果不存在就创建file文件夹 
            //if (Directory.Exists(path) == false)
            //{
            //    Directory.CreateDirectory(path);
            //}
            //savePath = Path.Combine(path, FileName);

            ////
            //string strInsertFile = "";

            //if (FileName != "")
            //{
            //    if (File.Exists(savePath) == false)// 没有同名文件 
            //    {
            //        file[0].SaveAs(savePath);

            //        strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
            //        strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + bas.AwardInfo + "','" + bas.CreatUser + "','"
            //            + bas.CreatUser + "','" + bas.Validate + "')";
            //        intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
            //    }
            //    else// 有同名文件 
            //    {
            //        string strUpdate = "";
            //        string strSel = " select count(*) from tk_Award where SID='" + bas.SID + "' and Award='" + FileName
            //        + "' and AwardInfo='" + bas.AwardInfo + "' and AwardTime='" + DateTime.Now + "' and CreatUser='" + account.UserName + "'   and Validate='v' ";
            //        int count = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
            //        if (count > 0)// 存在同一阶段同名的文件 则覆盖
            //        {
            //            savePath = Path.Combine(path, FileName);
            //            file[0].SaveAs(savePath);

            //            strUpdate = " update tk_Award set Validate='i' where SID='" + bas.SID + "' and Award='" + FileName
            //                + "' and AwardInfo='" + bas.AwardInfo + "' and Validate='v' ";
            //            sqlTrans.ExecuteNonQuery(strUpdate);
            //            //
            //            strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
            //            strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + bas.AwardInfo + "','" + bas.AwardTime + "','"
            //                + bas.CreatUser + "','" + bas.Validate + "')";
            //            intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
            //        }
            //        else // 存在同名文件 但是不同阶段 则更名上传
            //        {
            //            FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
            //            savePath = Path.Combine(path, FileName);
            //            file[0].SaveAs(savePath);
            //            //
            //            strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
            //            strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + bas.AwardInfo + "','"
            //                + bas.AwardTime + "','" + bas.CreatUser + "','" + bas.Validate + "')";
            //            intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
            //        }
            //    }
            //}

            //try
            //{
            //    sqlTrans.Close(true);
            //}
            //catch (SqlException e)
            //{
            //    sqlTrans.Close(false);
            //    return -1;
            //}

            //return intInsertFile; 
            #endregion
        }
        public static bool InsertPrice(tk_PriceUp bas, HttpFileCollection file, ref string a_strErr)
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

                    FileName = NoFileName + fileEx;
                    if (!FileType.Contains(fileEx))
                    {
                        a_strErr = "文件类型不对，只能上传 .xls,.xlsx.jpg,.png,.doc,.docx,.pdf,.txt 格式的文件";
                    }
                    if (filesize >= Maxsize)
                    {
                        a_strErr = "上传文件超过10M，不能上传";
                    }
                    string path = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\";
                    string infos = DateTime.Now.ToString("yyyyMMdd");
                    path = path + infos;
                    if (Directory.Exists(path) == false)   //如果不存在就创建file文件夹 
                    {
                        Directory.CreateDirectory(path);
                    }
                    savePath = Path.Combine(path, FileName);
                    file[i].SaveAs(savePath);
                    strInsertFile = "insert into tk_PriceUp (SID,PriceName,PriceInfo,PriceTime,Createuser,Validate) ";
                    strInsertFile += " values ('" + bas.SID + "','" + FileName + "','" + savePath + "','" + DateTime.Now + "','" + acc.UserName + "','v')";
                    intInsertFile = SQLBase.ExecuteNonQuery(strInsertFile, "SupplyCnn");
                    if (intInsertFile == 1)
                    {
                        strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.SID + "','报价/比价信息','上传成功','" + DateTime.Now + "','" + acc.UserName + "','上传报价/比价')";
                    }
                }
            }
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                a_strErr = e.Message;
                return false;
            }
            #region MyRegion
            //Acc_Account acc = GAccount.GetAccountInfo();
            //SqlParameter[] para = new SqlParameter[]
            //{
            //    new SqlParameter("@fileByte",fileByte)
            //};

            //string strupdate = " INSERT into tk_PriceUp (SID,PriceName,PriceInfo,PriceTime,Createuser) VALUES('" + bas.SID + "','" + bas.PriceName + "',@fileByte,'" + DateTime.Now + "','" + bas.Createuser + "') where SID='" + bas.SID + "'";
            //if (strupdate != "")
            //{
            //    intInsert = SQLBase.ExecuteNonQuery(strupdate, CommandType.Text, para, "SupplyCnn");
            //}
            //if (intInsert == 1)
            //{
            //    strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.SID + "','报价/比价单信息','上传成功','" + DateTime.Now + "','" + acc.UserName + "','上传报价/比价单')";
            //}
            //try
            //{

            //    if (strLog != "")
            //    {
            //        intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
            //    }
            //}
            //catch (SqlException e)
            //{
            //    Err = e.Message;
            //    return false;
            //} 
            #endregion
            return (intInsertFile + intLog) >= 2;
        }
        public static bool AddCustome(tk_KClientBas cbs, ref string Err)
        {
            int intInsert = 0;
            int intinsert2 = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strAdd = "insert into tk_KClientBas(KID,GainDate,DeclareUnit,DeclareUser,ChargeUser,IsShare,ShareUnits,CName,CShortName,Industry,StaffSize,Products,Phone,FAX,ZipCode,COMWebsite,ComAddress,Province,City,ClientDesc,Remark,CType,CClass,CSource,CRelation,Maturity,State,CreateUser,CreateTime,Validate) ";
            strAdd += " values('" + cbs.KID + "','" + cbs.GainDate + "','" + acc.UnitName + "','" + cbs.DeclareUser + "','" + cbs.ChargeUser + "','" + cbs.IsShare + "','" + cbs.ShareUnits + "','" + cbs.CName + "','" + cbs.CShortName + "','" + cbs.Industry + "','" + cbs.StaffSize + "','" + cbs.Products + "','" + cbs.Phone + "','" + cbs.FAX + "','" + cbs.ZipCode + "','" + cbs.COMWebsite + "','" + cbs.ComAddress + "','" + cbs.Province + "','" + cbs.City + "','" + cbs.ClientDesc + "','" + cbs.Remark + "','" + cbs.CType + "','" + cbs.CClass + "','" + cbs.CSource + "','" + cbs.CRelation + "','" + cbs.Maturity + "','" + cbs.State + "','" + cbs.CreateUser + "','" + cbs.CreateTime + "','" + cbs.Validate + "') ";
            string strInsertlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type)values('" + cbs.KID + "','新增客户信息','添加客户成功','" + DateTime.Now + "','" + acc.UserName + "','新增客户操作')";
            try
            {
                if (strAdd != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strAdd, "SupplyCnn");
                }
                if (strInsertlog != "")
                {
                    intinsert2 = SQLBase.ExecuteNonQuery(strInsertlog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intinsert2) >= 2;
        }
        public static bool Addisno(tk_IsNotSupplierBas cbs, ref string Err)
        {
            int intInsert = 0;
            int intinsert2 = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strAdd = "insert into tk_IsNotSupplierBas(SID,COMNameC,SupplyContent,Contacts,TelFax,Phone,Mailbox,Remarks,UnitID,Validate,CreateTime,CreateUser,State) ";
            strAdd += " values('" + cbs.SID + "','" + cbs.COMNameC + "','" + cbs.SupplyContent + "','" + cbs.Contacts + "','" + cbs.TelFax + "','" + cbs.Phone + "','" + cbs.Mailbox + "','" + cbs.Remarks + "','" + cbs.UnitID + "','" + cbs.Validate + "','" + cbs.CreateTime + "','" + cbs.CreateUser + "','" + cbs.State + "') ";
            //string strInsertlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type)values('" + cbs.SID + "','新增非合格供应商信息','添加非合格供应商成功','" + DateTime.Now + "','" + acc.UserName + "','新增非合格供应商操作')";
            try
            {
                if (strAdd != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strAdd, "SupplyCnn");
                }
                //if (strInsertlog != "")
                //{
                //    intinsert2 = SQLBase.ExecuteNonQuery(strInsertlog, "SupplyCnn");
                //}
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return intInsert >= 1;
        }
        /// <summary>
        /// 增加联系人
        /// </summary>
        /// <param name="kcp"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool AddPersons(tk_KContactPerson kcp, ref string Err)
        {
            int intinert = 0;
            int intinsert2 = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "insert into tk_KContactPerson values('" + kcp.KID + "','" + kcp.CName + "','" + kcp.Sex + "','" + kcp.Job + "','" + kcp.Birthday + "','" + kcp.Age + "','" + kcp.Mobile + "','" + kcp.FAX + "','" + kcp.Email + "','" + kcp.QQ + "','" + kcp.WeiXin + "','" + kcp.Remark + "','" + kcp.CreateUser + "','" + kcp.CreateTime + "','" + kcp.Validate + "')";
            string strinsert = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kcp.KID + "','新增联系人','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增联系人操作')";
            try
            {
                if (sql != "")
                {
                    intinert = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strinsert != "")
                {
                    intinsert2 = SQLBase.ExecuteNonQuery(strinsert, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intinert + intinsert2) >= 2;
        }
        /// <summary>
        /// 增加共享部门
        /// </summary>
        /// <param name="kuwc"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool AddUnite(tk_KClientBas kuwc, ref string Err)
        {
            int intLog = 0;
            int intinsert = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "insert into tk_KClientBas values('" + kuwc.KID + "','" + kuwc.ShareUnits + "','" + kuwc.DeclareUser + "','" + kuwc.CreateUser + "','" + kuwc.CreateTime + "','" + kuwc.Validate + "')";
            string strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kuwc.KID + "','新增共享部门','新增成功','" + DateTime.Now + "','" + acc.UserName + "','新增共享部门操作')";
            try
            {
                if (sql != "")
                {
                    intinsert = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strlog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intLog + intinsert) >= 2;
        }
        #endregion

        #region 修改并显示信息
        /// <summary>
        /// 获取供应商联系人
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Tk_SContactPerson GetPS(string sid)
        {
            Tk_SContactPerson scp = new Tk_SContactPerson();
            string strSelect = "select b.text as fdepartment ,c.text as Department,d.text as Job,a.PName,a.Phone,a.Mobile,a.Email,a.CreateUser,a.CreateTime,a.Validate,a.sid from Tk_SContactPerson  a";
            strSelect += "  left join	   tk_ConfigContent	  b	   on a.fdepartment=b.sid and b.type='Fdepartment'";
            strSelect += "  left join	   tk_ConfigContent	  c	   on a.Department=c.sid and c.type='Department'";
            strSelect += " left join	   tk_ConfigContent	  d	   on a.Job=d.sid and d.type='Job'";
            strSelect += "  where a.SID='" + sid + "' ";
            DataTable dt2 = SQLBase.FillTable(strSelect, "SupplyCnn");
            #region MyRegion
            if (dt2.Rows.Count > 0)
            {
                scp.Sid = sid;
                scp.Fdepartment = dt2.Rows[0]["FDepartment"].ToString();
                scp.Pname = dt2.Rows[0]["PName"].ToString();
                scp.Department = dt2.Rows[0]["Department"].ToString();
                scp.Job = dt2.Rows[0]["Job"].ToString();
                scp.Phone = dt2.Rows[0]["Phone"].ToString();
                scp.Mobile = dt2.Rows[0]["Mobile"].ToString();
                scp.CreateUser = dt2.Rows[0]["CreateUser"].ToString();
                scp.CreateTime = Convert.ToDateTime(dt2.Rows[0]["CreateTime"]);
                scp.Validate = dt2.Rows[0]["Validate"].ToString();
                scp.Email = dt2.Rows[0]["Email"].ToString();
            }
            #endregion
            return scp;

        }
        /// <summary>
        /// 显示供应商处理
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Tk_SupplierBas ShowInfo(string sid)
        {
            Tk_SupplierBas info = new Tk_SupplierBas();
            string sql = "select * from Tk_SupplierBas where sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                info.Sid = dt.Rows[0]["SID"].ToString();
                info.DeclareDate = Convert.ToDateTime(dt.Rows[0]["DeclareDate"]).ToString("yyyy-MM-dd");
                info.SupplierType = dt.Rows[0]["SupplierType"].ToString();
                info.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                info.COMShortName = dt.Rows[0]["COMShortName"].ToString();
                info.COMArea = dt.Rows[0]["COMArea"].ToString();
                info.COMCountry = dt.Rows[0]["COMCountry"].ToString();
                info.ComAddress = dt.Rows[0]["ComAddress"].ToString();
                info.COMCreateDate = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
                info.TaxRegistrationNo = dt.Rows[0]["TaxRegistrationNo"].ToString();
                info.BusinessLicenseNo = dt.Rows[0]["BusinessLicenseNo"].ToString();
                info.COMRAddress = dt.Rows[0]["COMRAddress"].ToString();
                info.COMLegalPerson = dt.Rows[0]["COMLegalPerson"].ToString();
                info.COMFactoryAddress = dt.Rows[0]["COMFactoryAddress"].ToString();
                info.OrganizationCode = dt.Rows[0]["OrganizationCode"].ToString();
                info.CapitalUnit = dt.Rows[0]["CapitalUnit"].ToString();
                info.BankName = dt.Rows[0]["BankName"].ToString();
                info.BankAccount = dt.Rows[0]["BankAccount"].ToString();
                info.StaffNum = dt.Rows[0]["StaffNum"].ToString();
                info.EnterpriseType = dt.Rows[0]["EnterpriseType"].ToString();
                info.BusinessDistribute = dt.Rows[0]["BusinessDistribute"].ToString();
                info.BillingWay = dt.Rows[0]["BillingWay"].ToString();
                info.Turnover = dt.Rows[0]["Turnover"].ToString();
                info.DevelopStaffs = dt.Rows[0]["DevelopStaffs"].ToString();
                info.QAStaffs = dt.Rows[0]["QAStaffs"].ToString();
                info.ProduceStaffs = dt.Rows[0]["ProduceStaffs"].ToString();
                info.Relation = dt.Rows[0]["Relation"].ToString();
                info.HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
                info.WorkTime_Start = dt.Rows[0]["WorkTime_Start"].ToString();
                info.WorkTime_End = dt.Rows[0]["WorkTime_End"].ToString();
                info.WorkDay_Start = dt.Rows[0]["WorkDay_Start"].ToString();
                info.WorkDay_End = dt.Rows[0]["WorkDay_End"].ToString();

                #region MyRegion
                //info.Opinions = dt.Rows[0]["Opinions"].ToString();
                //info.OpinionsD = dt.Rows[0]["OpinionsD"].ToString();
                //info.DeclareUnit = dt.Rows[0]["DeclareUnit"].ToString();
                //info.ReviewDate = Convert.ToDateTime(dt.Rows[0]["ReviewDate"]);
                //info.SPState = dt.Rows[0]["SPState"].ToString();
                //info.Time1 = Convert.ToDateTime(dt.Rows[0]["Time1"]);
                //info.ApprovalState = dt.Rows[0]["ApprovalState"].ToString(); 
                #endregion
            }
            return info;
        }
        public static Tk_SupplierBas ShowResInfo(string name, string type, string area, string state, string sid)
        {
            string sql = "";
            Tk_SupplierBas info = new Tk_SupplierBas();
            if (name != "" || type != "" || area != "" || state != "")
            {
                sql = "select * from Tk_SupplierBas where 1=1 and COMNameC='" + name + "' or SupplierType='" + type + "' or COMArea='" + area + "' or ApprovalState='" + state + "'";
            }
            else
            {
                sql = "select * from Tk_SupplierBas where  SID='" + sid + "'";
            }

            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                info.Sid = dt.Rows[0]["SID"].ToString();
                info.SupplierType = dt.Rows[0]["SupplierType"].ToString();
                info.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                info.COMShortName = dt.Rows[0]["COMShortName"].ToString();
                info.COMCountry = dt.Rows[0]["COMCountry"].ToString();
                info.COMArea = dt.Rows[0]["COMArea"].ToString();
                info.ComAddress = dt.Rows[0]["ComAddress"].ToString();
                info.COMCreateDate = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
                info.TaxRegistrationNo = dt.Rows[0]["TaxRegistrationNo"].ToString();
                info.BusinessLicenseNo = dt.Rows[0]["BusinessLicenseNo"].ToString();
                info.COMRAddress = dt.Rows[0]["COMRAddress"].ToString();
                info.COMLegalPerson = dt.Rows[0]["COMLegalPerson"].ToString();
                info.COMFactoryAddress = dt.Rows[0]["COMFactoryAddress"].ToString();
                info.OrganizationCode = dt.Rows[0]["OrganizationCode"].ToString();
                info.CapitalUnit = dt.Rows[0]["CapitalUnit"].ToString();
                info.BankName = dt.Rows[0]["BankName"].ToString();
                info.BankAccount = dt.Rows[0]["BankAccount"].ToString();
                info.StaffNum = dt.Rows[0]["StaffNum"].ToString();
                info.EnterpriseType = dt.Rows[0]["EnterpriseType"].ToString();
                info.BusinessDistribute = dt.Rows[0]["BusinessDistribute"].ToString();
                info.BillingWay = dt.Rows[0]["BillingWay"].ToString();
                info.Turnover = dt.Rows[0]["Turnover"].ToString();
                info.DevelopStaffs = dt.Rows[0]["DevelopStaffs"].ToString();
                info.QAStaffs = dt.Rows[0]["QAStaffs"].ToString();
                info.ProduceStaffs = dt.Rows[0]["ProduceStaffs"].ToString();
                info.Relation = dt.Rows[0]["Relation"].ToString();
                info.HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
                info.WorkTime_Start = dt.Rows[0]["WorkTime_Start"].ToString();
                info.WorkTime_End = dt.Rows[0]["WorkTime_End"].ToString();
                info.WorkDay_Start = dt.Rows[0]["WorkDay_Start"].ToString();
                info.WorkDay_End = dt.Rows[0]["WorkDay_End"].ToString();

            }
            return info;
        }
        public static Tk_SupplierBas ShowOKInfo(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string sql = "select * from Tk_SupplierBas where sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                bas.Sid = dt.Rows[0]["SID"].ToString();
                bas.DeclareDate = Convert.ToDateTime(dt.Rows[0]["DeclareDate"]).ToString("yyyy-MM-dd");
                bas.SupplierType = dt.Rows[0]["SupplierType"].ToString();
                bas.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                bas.COMShortName = dt.Rows[0]["COMShortName"].ToString();
                bas.COMArea = dt.Rows[0]["COMArea"].ToString();
                bas.COMCountry = dt.Rows[0]["COMCountry"].ToString();
                bas.ComAddress = dt.Rows[0]["ComAddress"].ToString();
                bas.COMCreateDate = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
                bas.TaxRegistrationNo = dt.Rows[0]["TaxRegistrationNo"].ToString();
                bas.BusinessLicenseNo = dt.Rows[0]["BusinessLicenseNo"].ToString();
                bas.COMRAddress = dt.Rows[0]["COMRAddress"].ToString();
                bas.COMLegalPerson = dt.Rows[0]["COMLegalPerson"].ToString();
                bas.COMFactoryAddress = dt.Rows[0]["COMFactoryAddress"].ToString();
                bas.OrganizationCode = dt.Rows[0]["OrganizationCode"].ToString();
                bas.CapitalUnit = dt.Rows[0]["CapitalUnit"].ToString();
                bas.BankName = dt.Rows[0]["BankName"].ToString();
                bas.BankAccount = dt.Rows[0]["BankAccount"].ToString();
                bas.StaffNum = dt.Rows[0]["StaffNum"].ToString();
                bas.EnterpriseType = dt.Rows[0]["EnterpriseType"].ToString();
                bas.BusinessDistribute = dt.Rows[0]["BusinessDistribute"].ToString();
                bas.BillingWay = dt.Rows[0]["BillingWay"].ToString();
                bas.Turnover = dt.Rows[0]["Turnover"].ToString();
                bas.DevelopStaffs = dt.Rows[0]["DevelopStaffs"].ToString();
                bas.QAStaffs = dt.Rows[0]["QAStaffs"].ToString();
                bas.ProduceStaffs = dt.Rows[0]["ProduceStaffs"].ToString();
                bas.Relation = dt.Rows[0]["Relation"].ToString();
                bas.HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
                bas.WorkTime_Start = dt.Rows[0]["WorkTime_Start"].ToString();
                bas.WorkTime_End = dt.Rows[0]["WorkTime_End"].ToString();
                bas.WorkDay_Start = dt.Rows[0]["WorkDay_Start"].ToString();
                bas.WorkDay_End = dt.Rows[0]["WorkDay_End"].ToString();
                bas.ThreeCertity = dt.Rows[0]["ThreeCertity"].ToString();
            }
            return bas;
        }
        public static Tk_SupplierBas ShowYear(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string sql = "select *,j.DeptName from Tk_SupplierBas a";
            sql += "  left join BJOI_UM..UM_UnitNew	j  ON a.DeclareUnitID=j.DeptId   where a.sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                bas.Sid = dt.Rows[0]["SID"].ToString();
                bas.SupplierType = dt.Rows[0]["SupplierType"].ToString();
                bas.COMNameC = dt.Rows[0]["COMNameC"].ToString();
            }
            return bas;
        }
        public static tk_SYRDetail getNewDetail(string sid)
        {
            tk_SYRDetail del = new tk_SYRDetail();
            string str = "SELECT TOP 1 * FROM tk_SYRDetail where SID='" + sid + "'  ORDER BY CreateTime desc ";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                del.Score1 = dt.Rows[0]["Score1"].ToString();
                del.Score2 = dt.Rows[0]["Score2"].ToString();
                del.Score3 = dt.Rows[0]["Score3"].ToString();
                del.Score4 = dt.Rows[0]["Score4"].ToString();
                del.Score5 = dt.Rows[0]["Score5"].ToString();
                del.Result = dt.Rows[0]["Result"].ToString();
                del.ReviewDate = Convert.ToDateTime(dt.Rows[0]["ReviewDate"]).ToString("yyyy-MM-dd");
                del.ResultDesc = dt.Rows[0]["ResultDesc"].ToString();
                del.DeclareUser = dt.Rows[0]["DeclareUser"].ToString();
                del.DeclareUnit = dt.Rows[0]["DeclareUnit"].ToString();
            }
            return del;
        }
        public static Tk_SupplierBas getNewBAS(string sid)
        {
            Tk_SupplierBas del = new Tk_SupplierBas();
            string str = "select UnReviewUnit=stuff((select ','+b.Text  from [tk_ConfigSupUnit] b";
            str += " where charindex(','+convert(varchar,b.sid),','+a.UnReviewUnit)>0 for xml path('')),1,1,'')";
            str += " ,* from  tk_SupplierBas a where sid='" + sid + "'";
            //"SELECT  * FROM Tk_SupplierBas where SID='" + sid + "'  ORDER BY CreateTime desc ";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                del.IsUnReview = dt.Rows[0]["IsUnReview"].ToString();
                del.URConfirmUser = dt.Rows[0]["URConfirmUser"].ToString();
                del.IsUnreviewUser = dt.Rows[0]["IsUnreviewUser"].ToString();
                del.IsURInnerUnit = dt.Rows[0]["IsURInnerUnit"].ToString();
                del.UnReviewUnit = dt.Rows[0]["UnReviewUnit"].ToString();
                del.Evaluation1 = dt.Rows[0]["Evaluation1"].ToString();
                del.Evaluation2 = dt.Rows[0]["Evaluation2"].ToString();
                del.Evaluation3 = dt.Rows[0]["Evaluation3"].ToString();
                del.Evaluation4 = dt.Rows[0]["Evaluation4"].ToString();
                del.Evaluation5 = dt.Rows[0]["Evaluation5"].ToString();
                del.Evaluation6 = dt.Rows[0]["Evaluation6"].ToString();
                del.NState = Convert.ToInt32(dt.Rows[0]["NState"]);
                del.State = Convert.ToInt32(dt.Rows[0]["State"].ToString());
            }
            return del;
        }
        public static tk_SYRDetail getNewsocre(string sid)
        {
            tk_SYRDetail del = new tk_SYRDetail();
            string str = "SELECT TOP 1 * FROM tk_SYRDetail where SID='" + sid + "'  ORDER BY CreateTime desc ";
            DataTable dt = SQLBase.FillTable(str, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                del.Score1 = dt.Rows[0]["Score1"].ToString();
                del.Score2 = dt.Rows[0]["Score2"].ToString();
                del.Score3 = dt.Rows[0]["Score3"].ToString();
                del.Score4 = dt.Rows[0]["Score4"].ToString();
                del.Score5 = dt.Rows[0]["Score5"].ToString();
                del.Result = dt.Rows[0]["Result"].ToString();
                del.ReviewDate = Convert.ToDateTime(dt.Rows[0]["ReviewDate"]).ToString("yyyy-MM-dd");
                del.ResultDesc = dt.Rows[0]["ResultDesc"].ToString();
                del.DeclareUser = dt.Rows[0]["DeclareUser"].ToString();
                del.DeclareUnit = dt.Rows[0]["DeclareUnit"].ToString();
            }
            return del;
        }
        public static tk_SYRDetail ShowScore(string sid)
        {
            tk_SYRDetail bas1 = new tk_SYRDetail();
            string sql = "select a.SID, b.text as Result,a.ReviewDate ,a.Score1,a.Score2,a.Score3,a.Score4,a.Score5 from	 tk_SYRDetail a ";
            sql += " inner join		tk_ConfigContent b on a.result=b.sid  and b.type='result'";
            sql += " where a.sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                bas1.SID = dt.Rows[0]["SID"].ToString();
                bas1.Result = dt.Rows[0]["Result"].ToString();
                bas1.ReviewDate = Convert.ToDateTime(dt.Rows[0]["ReviewDate"]).ToString("yyyy-MM-dd");
            }
            return bas1;
        }
        /// <summary>
        /// 填充客户信息
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public static tk_KClientBas UpdateCus(string kid)
        {
            tk_KClientBas cbs = new tk_KClientBas();
            string sql = "select * from tk_KClientBas where KID = '" + kid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                cbs.KID = kid;
                cbs.GainDate = Convert.ToDateTime(dt.Rows[0]["GainDate"]).ToString("yyyy-MM-dd");
                cbs.DeclareUnit = dt.Rows[0]["DeclareUnit"].ToString();
                cbs.DeclareUser = dt.Rows[0]["DeclareUser"].ToString();
                cbs.ChargeUser = dt.Rows[0]["ChargeUser"].ToString();
                cbs.IsShare = dt.Rows[0]["IsShare"].ToString();
                cbs.ShareUnits = dt.Rows[0]["ShareUnits"].ToString();
                cbs.CName = dt.Rows[0]["CName"].ToString();
                cbs.CShortName = dt.Rows[0]["CShortName"].ToString();
                cbs.Industry = dt.Rows[0]["Industry"].ToString();
                cbs.StaffSize = dt.Rows[0]["StaffSize"].ToString();
                cbs.Products = dt.Rows[0]["Products"].ToString();
                cbs.Phone = dt.Rows[0]["Phone"].ToString();
                cbs.FAX = dt.Rows[0]["FAX"].ToString();
                cbs.ZipCode = Convert.ToInt32(dt.Rows[0]["ZipCode"]);
                cbs.COMWebsite = dt.Rows[0]["COMWebsite"].ToString();
                cbs.ComAddress = dt.Rows[0]["ComAddress"].ToString();
                cbs.Province = dt.Rows[0]["Province"].ToString();
                cbs.City = dt.Rows[0]["City"].ToString();
                cbs.ClientDesc = dt.Rows[0]["ClientDesc"].ToString();
                cbs.Remark = dt.Rows[0]["Remark"].ToString();
                cbs.CType = dt.Rows[0]["CType"].ToString();
                cbs.CClass = dt.Rows[0]["CClass"].ToString();
                cbs.CSource = dt.Rows[0]["CSource"].ToString();
                cbs.CRelation = dt.Rows[0]["CRelation"].ToString();
                cbs.Maturity = dt.Rows[0]["Maturity"].ToString();
                cbs.State = dt.Rows[0]["State"].ToString();
            }
            return cbs;
        }
        public static tk_IsNotSupplierBas updateIsnot(string sid)
        {
            tk_IsNotSupplierBas cbs = new tk_IsNotSupplierBas();
            string sql = "select * from tk_IsNotSupplierBas where sid = '" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                cbs.SID = sid;
                cbs.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]).ToString("yyyy-MM-dd");
                cbs.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                cbs.SupplyContent = dt.Rows[0]["SupplyContent"].ToString();
                cbs.Contacts = dt.Rows[0]["Contacts"].ToString();
                cbs.TelFax = dt.Rows[0]["TelFax"].ToString();
                cbs.Phone = dt.Rows[0]["Phone"].ToString();
                cbs.Mailbox = dt.Rows[0]["Mailbox"].ToString();
                cbs.Remarks = dt.Rows[0]["Remarks"].ToString();
                cbs.State = dt.Rows[0]["State"].ToString();
            }
            return cbs;
        }
        /// <summary>
        /// 填充客户联系人
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public static tk_KContactPerson Persons(string kid, DateTime time)
        {
            tk_KContactPerson kcp = new tk_KContactPerson();
            string sql = "select * from tk_KContactPerson where KID = '" + kid + "' and createtime='" + time + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                kcp.KID = kid;
                kcp.CName = dt.Rows[0]["CName"].ToString();
                kcp.Sex = dt.Rows[0]["Sex"].ToString();
                kcp.Job = dt.Rows[0]["Job"].ToString();
                kcp.Birthday = Convert.ToDateTime(dt.Rows[0]["Birthday"]);
                kcp.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                kcp.Mobile = dt.Rows[0]["Mobile"].ToString();
                kcp.FAX = dt.Rows[0]["FAX"].ToString();
                kcp.Email = dt.Rows[0]["Email"].ToString();
                kcp.QQ = dt.Rows[0]["QQ"].ToString();
                kcp.WeiXin = dt.Rows[0]["WeiXin"].ToString();
                kcp.Remark = dt.Rows[0]["Remark"].ToString();
                kcp.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                kcp.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                kcp.Validate = dt.Rows[0]["Validate"].ToString();
            }
            return kcp;
        }
        /// <summary>
        /// 填充产品
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="proid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static tk_SProducts Product(string sid, string id, DateTime time, string filename)
        {
            tk_SProducts sp = new tk_SProducts();
            if (filename != "")
            {
                string sql = "select * from tk_SProducts where sid='" + sid + "'and id='" + id + "'and CreateTime='" + time + "'";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sp.Sid = sid;
                    sp.Ptype = dt.Rows[0]["Ptype"].ToString();
                    sp.Productid = dt.Rows[0]["ProductID"].ToString();
                    sp.Productname = dt.Rows[0]["ProductName"].ToString();
                    sp.Standard = dt.Rows[0]["Standard"].ToString();
                    sp.Measureunit = dt.Rows[0]["MeasureUnit"].ToString();
                    sp.Detaildesc = dt.Rows[0]["DetailDesc"].ToString();
                    sp.Price = Convert.ToDecimal(dt.Rows[0]["Price"]).ToString();
                    sp.Originplace = dt.Rows[0]["OriginPlace"].ToString();
                    sp.Createuser = dt.Rows[0]["Createuser"].ToString();
                    sp.Createtime = Convert.ToDateTime(dt.Rows[0]["Createtime"]);
                    sp.FFileName = dt.Rows[0]["FFileName"].ToString();
                    sp.BYTtime = Convert.ToDateTime(dt.Rows[0]["BYTime"]).ToString("yyyy-MM-dd");
                    sp.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            else
            {
                string sql = "select * from tk_SProducts where sid='" + sid + "'and id='" + id + "'and CreateTime='" + time + "'";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sp.Sid = sid;
                    sp.Ptype = dt.Rows[0]["Ptype"].ToString();
                    sp.Productid = dt.Rows[0]["ProductID"].ToString();
                    sp.Productname = dt.Rows[0]["ProductName"].ToString();
                    sp.Standard = dt.Rows[0]["Standard"].ToString();
                    sp.Measureunit = dt.Rows[0]["MeasureUnit"].ToString();
                    sp.Detaildesc = dt.Rows[0]["DetailDesc"].ToString();
                    sp.Price = Convert.ToDecimal(dt.Rows[0]["Price"]).ToString();
                    sp.Originplace = dt.Rows[0]["OriginPlace"].ToString();
                    sp.Createuser = dt.Rows[0]["Createuser"].ToString();
                    sp.Createtime = Convert.ToDateTime(dt.Rows[0]["Createtime"]);
                    //sp.FFileName = dt.Rows[0]["FFileName"].ToString();
                    sp.BYTtime = Convert.ToDateTime(dt.Rows[0]["BYTime"]).ToString("yyyy-MM-dd");
                    sp.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            return sp;
        }
        /// <summary>
        /// 填充服务
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static tk_SService Server(string sid, string id, string filename)
        {
            tk_SService sser = new tk_SService();
            if (filename != "")
            {
                string sql = "select * from tk_SService where sid='" + sid + "' and ServiceID='" + id + "'";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sser.Sid = sid;
                    sser.ServiceName = dt.Rows[0]["ServiceName"].ToString();
                    sser.ServiceID = dt.Rows[0]["ServiceID"].ToString();
                    sser.ServiceDesc = dt.Rows[0]["ServiceDesc"].ToString();
                    sser.Purpose = dt.Rows[0]["Purpose"].ToString();
                    sser.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                    sser.FFileName = dt.Rows[0]["FFileName"].ToString();
                    sser.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                    sser.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            else
            {
                string sql = "select * from tk_SService where sid='" + sid + "' and ServiceID='" + id + "'";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sser.Sid = sid;
                    sser.ServiceName = dt.Rows[0]["ServiceName"].ToString();
                    sser.ServiceID = dt.Rows[0]["ServiceID"].ToString();
                    sser.ServiceDesc = dt.Rows[0]["ServiceDesc"].ToString();
                    sser.Purpose = dt.Rows[0]["Purpose"].ToString();
                    sser.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                    sser.FFileName = dt.Rows[0]["FFileName"].ToString();
                    sser.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                    sser.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            return sser;
        }
        /// <summary>
        /// 填充资质
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static tk_SFileInfo FileInfo(string sid, string fid, DateTime time, string filename)
        {
            tk_SFileInfo sfi = new tk_SFileInfo();
            if (filename != "")
            {
                string sql = "select * from tk_SFileInfo where sid='" + sid + "' and FID='" + fid + "' and CreateTime='" + time + "' ";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sfi.Sid = sid;
                    sfi.Ftype = dt.Rows[0]["FType"].ToString();
                    sfi.Typeo = dt.Rows[0]["TypeO"].ToString();
                    sfi.Item = dt.Rows[0]["Item"].ToString();
                    sfi.Itemo = dt.Rows[0]["ItemO"].ToString();
                    sfi.Ffilename = dt.Rows[0]["FFileName"].ToString();
                    sfi.Filetype = dt.Rows[0]["FileType"].ToString();
                    sfi.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                    sfi.Createuser = dt.Rows[0]["Createuser"].ToString();
                    sfi.FTimeOut = Convert.ToDateTime(dt.Rows[0]["FTimeOut"]).ToString("yyyy-MM-dd");
                    sfi.Createtime = Convert.ToDateTime(dt.Rows[0]["Createtime"]);
                    sfi.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            else
            {
                string sql = "select * from tk_SFileInfo where sid='" + sid + "' and FID='" + fid + "' and CreateTime='" + time + "' ";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    sfi.Sid = sid;
                    sfi.Ftype = dt.Rows[0]["FType"].ToString();
                    sfi.Typeo = dt.Rows[0]["TypeO"].ToString();
                    sfi.Item = dt.Rows[0]["Item"].ToString();
                    sfi.Itemo = dt.Rows[0]["ItemO"].ToString();
                    sfi.Ffilename = dt.Rows[0]["FFileName"].ToString();
                    sfi.Filetype = dt.Rows[0]["FileType"].ToString();
                    sfi.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                    sfi.Createuser = dt.Rows[0]["Createuser"].ToString();
                    // sfi.FTimeOut = Convert.ToDateTime(dt.Rows[0]["FTimeOut"]).ToString("yyyy-MM-dd");
                    sfi.Createtime = Convert.ToDateTime(dt.Rows[0]["Createtime"]);
                    sfi.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            return sfi;
        }
        public static tk_SFileInfo GetReark(string sid, string fid)
        {
            tk_SFileInfo sf = new tk_SFileInfo();
            string sql = "select * from tk_SFileInfo where sid='" + sid + "' and FID='" + fid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                sf.Sid = sid;
                sf.Ftype = dt.Rows[0]["FType"].ToString();
                sf.Typeo = dt.Rows[0]["TypeO"].ToString();
                sf.Item = dt.Rows[0]["Item"].ToString();
                sf.Itemo = dt.Rows[0]["ItemO"].ToString();
                sf.Ffilename = dt.Rows[0]["FFileName"].ToString();
                sf.Filetype = dt.Rows[0]["FileType"].ToString();
                sf.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                sf.Createuser = dt.Rows[0]["Createuser"].ToString();
                sf.Createtime = Convert.ToDateTime(dt.Rows[0]["Createtime"]);
                sf.Validate = dt.Rows[0]["Validate"].ToString();
            }
            return sf;

        }
        /// <summary>
        /// 填充证书
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static tk_SCertificate Certify(string sid, string fid, DateTime time, string filename)
        {
            tk_SCertificate scf = new tk_SCertificate();
            if (filename != "")
            {
                string sql = "select * from tk_SCertificate where sid='" + sid + "' and fid='" + fid + "' and CreateTime='" + time + "'  ";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    scf.Sid = sid;
                    scf.Isplan = dt.Rows[0]["Isplan"].ToString();
                    scf.Ctype = dt.Rows[0]["CType"].ToString();
                    scf.Cname = dt.Rows[0]["CName"].ToString();
                    scf.Ccode = dt.Rows[0]["CCode"].ToString();
                    scf.Corganization = dt.Rows[0]["COrganization"].ToString();
                    scf.Cdate = Convert.ToDateTime(dt.Rows[0]["CDate"]).ToString("yyyy-MM-dd");
                    scf.TimeOut = Convert.ToDateTime(dt.Rows[0]["TimeOut"]).ToString("yyyy-MM-dd");
                    scf.Cfilename = dt.Rows[0]["CFileName"].ToString();
                    scf.FileType = dt.Rows[0]["FileType"].ToString();
                    scf.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                    scf.Createuser = dt.Rows[0]["CreateUser"].ToString();
                    scf.Createtime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                    scf.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            else
            {
                string sql = "select * from tk_SCertificate where sid='" + sid + "'and fid='" + fid + "' and CreateTime='" + time + "'  ";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                if (dt.Rows.Count > 0)
                {
                    scf.Sid = sid;
                    scf.Isplan = dt.Rows[0]["Isplan"].ToString();
                    scf.Ctype = dt.Rows[0]["CType"].ToString();
                    scf.Cname = dt.Rows[0]["CName"].ToString();
                    scf.Ccode = dt.Rows[0]["CCode"].ToString();
                    scf.Corganization = dt.Rows[0]["COrganization"].ToString();
                    scf.Cdate = Convert.ToDateTime(dt.Rows[0]["CDate"]).ToString("yyyy-MM-dd");
                    //scf.TimeOut = Convert.ToDateTime(dt.Rows[0]["TimeOut"]).ToString("yyyy-MM-dd");
                    scf.Cfilename = dt.Rows[0]["CFileName"].ToString();
                    scf.FileType = dt.Rows[0]["FileType"].ToString();
                    scf.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                    scf.Createuser = dt.Rows[0]["CreateUser"].ToString();
                    scf.Createtime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                    scf.Validate = dt.Rows[0]["Validate"].ToString();
                }
            }
            return scf;

        }
        public static tk_SCertificate RemarkCertify(string sid)
        {
            tk_SCertificate sc = new tk_SCertificate();
            string sql = "select * from tk_SCertificate where sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                sc.Sid = sid;
                sc.Isplan = dt.Rows[0]["Isplan"].ToString();
                sc.Ctype = dt.Rows[0]["CType"].ToString();
                sc.Cname = dt.Rows[0]["CName"].ToString();
                sc.Ccode = dt.Rows[0]["CCode"].ToString();
                sc.Corganization = dt.Rows[0]["COrganization"].ToString();
                sc.Cdate = Convert.ToDateTime(dt.Rows[0]["CDate"]).ToString("yyyy-MM-dd");
                sc.Cfilename = dt.Rows[0]["CFileName"].ToString();
                sc.FileType = dt.Rows[0]["FileType"].ToString();
                sc.Fileinfo = dt.Rows[0]["FileInfo"].ToString();
                sc.Createuser = dt.Rows[0]["CreateUser"].ToString();
                sc.Createtime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                sc.Validate = dt.Rows[0]["Validate"].ToString();
            }
            return sc;
        }
        /// <summary>
        /// 填充客户
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public static tk_KClientBas UpUnite(string kid)
        {
            tk_KClientBas cbs = new tk_KClientBas();
            string sql = "select * from tk_KClientBas where KID='" + kid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                cbs.KID = kid;
                cbs.ShareUnits = dt.Rows[0]["ShareUnits"].ToString();
                cbs.DeclareUser = dt.Rows[0]["DeclareUser"].ToString();
                cbs.CreateTime = dt.Rows[0]["CreateTime"].ToString();
                cbs.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                cbs.Validate = dt.Rows[0]["Validate"].ToString();
            }
            return cbs;
        }
        /// <summary>
        /// 负责人审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Tk_SupplierBas ApproverInfo(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string sql = "select DeptName ,b.COMNameC,b.SupplierCode,b.DeclareUnitID,b.CreateTime";
            sql += " from BJOI_UM..UM_UnitNew a inner join  tk_SupplierBas b on a.Deptid= b.DeclareUnitID   where sid='" + sid + "'";//"select * from Tk_SupplierBas where SID = '" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                bas.Sid = sid;
                bas.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                bas.SupplierCode = dt.Rows[0]["SupplierCode"].ToString();
                bas.DeclareUnitID = dt.Rows[0]["DeptName"].ToString();
                bas.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
            }
            return bas;
        }
        public static tk_SProcessInfo getNewInfo(string sid)
        {
            tk_SProcessInfo info = new tk_SProcessInfo();
            string sql = "SELECT  TOP 1 * FROM tk_SProcessInfo where SID='" + sid + "'  ORDER BY CreateTime desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                info.OpinionsD = dt.Rows[0]["OpinionsD"].ToString();
                info.Opinions = dt.Rows[0]["Opinions"].ToString();
                info.Reason = dt.Rows[0]["Reason"].ToString();
                info.ISAgree = dt.Rows[0]["ISAgree"].ToString();
            }
            return info;
        }
        public static tk_SUPSugestion getNewsuges(string sid)
        {
            tk_SUPSugestion info = new tk_SUPSugestion();
            string sql = "SELECT  TOP 1 * FROM tk_SUPSugestion where SID='" + sid + "'  ORDER BY SCreate desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                info.SState = dt.Rows[0]["SState"].ToString();
                info.SContent = dt.Rows[0]["SContent"].ToString();
            }
            return info;
        }
        public static tk_SFileInfo getNewfileInfo(string sid)
        {
            tk_SFileInfo info = new tk_SFileInfo();
            string sql = "SELECT  TOP 1 * FROM tk_SFileInfo where SID='" + sid + "'  ORDER BY CreateTime desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                info.MfFilename = dt.Rows[0]["MFFileName"].ToString();
                info.Ffilename = dt.Rows[0]["FFileName"].ToString();
            }
            return info;
        }
        public static tk_PriceUp getNewprice(string sid)
        {
            tk_PriceUp price = new tk_PriceUp();
            string sql = "SELECT  TOP 1 * FROM tk_PriceUp where SID='" + sid + "' and Validate='v'  ORDER BY PriceTime desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                price.PriceName = dt.Rows[0]["PriceName"].ToString();
                // price.Ffilename = dt.Rows[0]["FFileName"].ToString();
            }
            return price;
        }
        public static tk_SApproval getApprol(string sid)
        {
            tk_SApproval cbs = new tk_SApproval();
            string sql = "SELECT  TOP 1 * FROM tk_SApproval where RelevanceID='" + sid + "'  ORDER BY CreateTime desc";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt.Rows.Count > 0)
            {
                cbs.IsPass = dt.Rows[0]["IsPass"].ToString();
                cbs.Opinion = dt.Rows[0]["Opinion"].ToString();
            }
            return cbs;
        }
        #endregion
        #region 删除操作
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool deletePro(string sid, string id, ref string Err)
        {
            int intDelete = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_SProducts set Validate='i' where SID='" + sid + "' and ID='" + id + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除产品信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除产品')";
            try
            {
                if (strDele != "")
                    intDelete = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intDelete + intLog) >= 2;
        }
        public static bool deleteprice(string sid, string id, ref string Err)
        {
            int intDelete = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_PriceUp set Validate='i' where SID='" + sid + "' and ID='" + id + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除报价/比价单','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除报价/比价单')";
            try
            {
                if (strDele != "")
                    intDelete = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intDelete + intLog) >= 2;
        }
        public static bool deleteaward(string sid, string id, ref string Err)
        {
            int intDelete = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_Award set Validate='i' where SID='" + sid + "' and ID='" + id + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除曾获奖项','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除曾获奖项')";
            try
            {
                if (strDele != "")
                    intDelete = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intDelete + intLog) >= 2;
        }
        public static bool CancelSp(string sid, ref string Err)
        {
            int intDelete = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_SupplierBas set Validate='i',WState='0' where SID='" + sid + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + acc.UserID + "','删除供应商信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除供应商')";
            try
            {
                if (strDele != "")
                    intDelete = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intDelete + intLog) >= 2;
        }
        public static bool RESTSUP(string sid, ref string Err)
        {
            int intDelete = 0;
            int intLog = 0;
            string strLog = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_SupplierBas set state='0',WState='0' where SID='" + sid + "'";
            if (strDele != "")
                intDelete = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
            if (intDelete == 1)
            {
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + acc.UserID + "','重新提交供应商信息','重新提交成功','" + DateTime.Now + "','" + acc.UserName + "','重新提交供应商')";
            }
            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intDelete + intLog) >= 2;
        }
        public static bool DeleteServer(string sid, DateTime time, ref string Err)
        {
            int intInsert = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = "update tk_SService set Validate='i' where SID='" + sid + "' and CreateTime='" + time + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除服务信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除服务操作')";
            try
            {
                if (strDele != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intInsert + intLog) >= 2;
        }
        /// <summary>
        /// 删除资质文件
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool DeleteFile(string sid, DateTime time, ref string Err)
        {
            int intdelte = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "update tk_SFileInfo  set Validate='i' where sid='" + sid + "' and CreateTime='" + time + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除资质信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除资质操作')";
            try
            {
                if (sql != "")
                {
                    intdelte = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intdelte + intLog) >= 2;
        }
        /// <summary>
        /// 删除证书
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool DeleteCerticify(string sid, DateTime time, ref string Err)
        {
            int intdelte = 0;
            int intLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "update tk_SCertificate  set Validate='i' where sid='" + sid + "' and CreateTime='" + time + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sid + "','删除证书信息','删除证书成功','" + DateTime.Now + "','" + acc.UserName + "','删除证书操作')";
            try
            {
                if (sql != "")
                {
                    intdelte = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intdelte + intLog) >= 2;
        }
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="kid"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool deletePersons(string kid, ref string Err)
        {
            int str1 = 0;
            int str2 = 0;
            int intInsert = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDelete1 = "update tk_KContactPerson set Validate='i' where KID='" + kid + "'";
            string strUp = "update tk_KClientBas  set State='" + 1 + "' where KID='" + kid + "'";
            string strInsert = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kid + "','删除联系人','删除联系人成功','" + DateTime.Now + "','" + acc.UserName + "','删除联系人操作')";
            try
            {
                if (strDelete1 != "")
                {
                    str1 = SQLBase.ExecuteNonQuery(strDelete1, "SupplyCnn");
                }
                if (strUp != "")
                {
                    str2 = SQLBase.ExecuteNonQuery(strUp, "SupplyCnn");
                }
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (str1 + str2 + intInsert) >= 3;
        }
        public static bool deleteUnit(string kid, ref string Err)
        {
            int str1 = 0;
            int str2 = 0;
            int intlog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDelete1 = "update tk_KUnitWithClient  set Validate='i'  where KID='" + kid + "'";
            string strUp = "update tk_KClientBas set State='" + 1 + "' where KID='" + kid + "'";
            string strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kid + "','删除共享部门','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除共享部门操作')";
            try
            {
                if (strDelete1 != "")
                {
                    str1 = SQLBase.ExecuteNonQuery(strDelete1, "SupplyCnn");
                }
                if (strUp != "")
                {
                    str2 = SQLBase.ExecuteNonQuery(strUp, "SupplyCnn");
                }
                if (strlog != "")
                {
                    intlog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (str1 + str2 + intlog) >= 3;
        }
        public static bool deleteCus(string kid, ref string Err)
        {
            int sql = 0;
            int intInsert = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = " update tk_KClientBas set  Validate='i',state='1' where KID='" + kid + "'";
            string strInsert = "insert into tk_UserLog(UserId,LogTitle,LogContent,LogTime,LogPerson,Type)values('" + kid + "','删除客户信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除客户操作')";
            try
            {
                if (strDele != "")
                {
                    sql = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                }
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (sql + intInsert) >= 2;
        }
        public static bool deleteisnotsuply(string sid, ref string Err)
        {
            int sql = 0;
            // int intInsert = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strDele = " update tk_IsNotSupplierBas set  Validate='i' where sid='" + sid + "'";
            //string strInsert = "insert into tk_UserLog(UserId,LogTitle,LogContent,LogTime,LogPerson,Type)values('" + sid + "','删除供应商信息','删除成功','" + DateTime.Now + "','" + acc.UserName + "','删除供应商操作')";
            try
            {
                if (strDele != "")
                {
                    sql = SQLBase.ExecuteNonQuery(strDele, "SupplyCnn");
                }
                //if (strInsert != "")
                //{
                //    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                //}
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return sql >= 1;
        }
        #endregion
        #region 更新操作

        /// <summary>
        /// 修改供应商基本信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Tk_SupplierBas UpdateBas(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();

            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "select * from tk_SupplierBas where SID= '" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            #region 判断

            if (dt.Rows.Count > 0)
            {
                bas.Sid = sid;
                bas.DeclareUnitID = dt.Rows[0]["DeclareUnitID"].ToString();
                bas.DeclareDate = Convert.ToDateTime(dt.Rows[0]["DeclareDate"]).ToString("yyyy-MM-dd");
                bas.SupplierCode = dt.Rows[0]["SupplierCode"].ToString();
                bas.SupplierType = dt.Rows[0]["SupplierType"].ToString();
                bas.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                bas.COMShortName = dt.Rows[0]["COMShortName"].ToString();
                bas.COMNameE = dt.Rows[0]["COMNameE"].ToString();
                bas.COMWebsite = dt.Rows[0]["COMWebsite"].ToString();
                bas.COMRAddress = dt.Rows[0]["COMRAddress"].ToString();
                bas.COMCountry = dt.Rows[0]["COMCountry"].ToString();
                bas.ComAddress = dt.Rows[0]["ComAddress"].ToString();
                bas.COMArea = dt.Rows[0]["COMArea"].ToString();
                bas.COMFactoryAddress = dt.Rows[0]["COMFactoryAddress"].ToString();
                bas.COMFactoryArea = dt.Rows[0]["COMFactoryArea"].ToString();
                bas.COMCreateDate = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
                bas.COMLegalPerson = dt.Rows[0]["COMLegalPerson"].ToString();
                bas.TaxRegistrationNo = dt.Rows[0]["TaxRegistrationNo"].ToString();
                bas.IsCooperate = dt.Rows[0]["IsCooperate"].ToString();
                bas.COMGroup = dt.Rows[0]["COMGroup"].ToString();
                bas.BusinessLicenseNo = dt.Rows[0]["BusinessLicenseNo"].ToString();
                bas.StaffNum = dt.Rows[0]["StaffNum"].ToString();
                bas.RegisteredCapital = dt.Rows[0]["RegisteredCapital"].ToString();
                bas.CapitalUnit = dt.Rows[0]["CapitalUnit"].ToString();
                bas.OrganizationCode = dt.Rows[0]["OrganizationCode"].ToString();
                bas.BankName = dt.Rows[0]["BankName"].ToString();
                bas.BankAccount = dt.Rows[0]["BankAccount"].ToString().ToString();
                bas.EnterpriseType = dt.Rows[0]["EnterpriseType"].ToString();
                bas.FAX = dt.Rows[0]["FAX"].ToString();
                //业务分布
                bas.BusinessDistribute = dt.Rows[0]["BusinessDistribute"].ToString();
                //开票方式
                bas.BillingWay = dt.Rows[0]["BillingWay"].ToString();
                bas.DevelopStaffs = dt.Rows[0]["DevelopStaffs"].ToString();
                bas.QAStaffs = dt.Rows[0]["QAStaffs"].ToString();
                bas.ProduceStaffs = dt.Rows[0]["ProduceStaffs"].ToString();
                //健全的组织机构代码 HasRegulation
                bas.HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
                bas.ProductLineNum = dt.Rows[0]["ProductLineNum"].ToString();
                bas.WorkTime_Start = dt.Rows[0]["WorkTime_Start"].ToString();
                bas.WorkTime_End = dt.Rows[0]["WorkTime_End"].ToString();
                bas.WorkDay_Start = dt.Rows[0]["WorkDay_Start"].ToString();
                bas.WorkDay_End = dt.Rows[0]["WorkDay_End"].ToString();
                bas.BusinessScope = dt.Rows[0]["BusinessScope"].ToString();
                //是否排名前五
                bas.IsrankingIn5 = dt.Rows[0]["IsrankingIn5"].ToString();
                //排民类型
                bas.RankingType = dt.Rows[0]["RankingType"].ToString();
                bas.Ranking = dt.Rows[0]["Ranking"].ToString();
                //供应商种类和规模
                bas.ScaleType = dt.Rows[0]["ScaleType"].ToString();
                //产品执行标准
                bas.QualityStandard = dt.Rows[0]["QualityStandard"].ToString();
                bas.AnnualOutput = dt.Rows[0]["AnnualOutput"].ToString();
                bas.AnnualOutputValue = dt.Rows[0]["AnnualOutputValue"].ToString();
                bas.MainClient = dt.Rows[0]["MainClient"].ToString();
                bas.Achievement = dt.Rows[0]["Achievement"].ToString();
                //代理授权证明
                bas.HasAuthorization = dt.Rows[0]["HasAuthorization"].ToString();
                //图纸
                bas.HasDrawing = dt.Rows[0]["HasDrawing"].ToString();
                //级别
                bas.AgentClass = dt.Rows[0]["AgentClass"].ToString();
                //进口材料
                bas.HasImportMaterial = dt.Rows[0]["HasImportMaterial"].ToString();
                bas.Award = dt.Rows[0]["Award"].ToString();
                //供需关系
                bas.Relation = dt.Rows[0]["Relation"].ToString();
                bas.Turnover = dt.Rows[0]["Turnover"].ToString();
                bas.OtherType = dt.Rows[0]["OtherType"].ToString();
                bas.CreateUser = dt.Rows[0]["CreateUser"].ToString();
                bas.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                bas.Validate = dt.Rows[0]["Validate"].ToString();
                bas.ThreeCertity = dt.Rows[0]["ThreeCertity"].ToString();
            }
            #endregion
            return bas;
        }
        public static tk_SupplierBasHis getBAS(string sid)
        {
            tk_SupplierBasHis his = new tk_SupplierBasHis();
            Acc_Account acc = GAccount.GetAccountInfo();
            string sql = "select * from tk_SupplierBasHis where SID= '" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");

            if (dt.Rows.Count > 0)
            {
                his.Sid = sid;
                his.COMNameC = dt.Rows[0]["COMNameC"].ToString();
                his.NCreateTime = dt.Rows[0]["NCreateTime"].ToString();
                his.NCreateUser = dt.Rows[0]["NCreateUser"].ToString();

            }
            return his;
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="bas"></param>
        /// <param name="a_strErr"></param>
        /// <returns></returns>
        public static int UpdatePro(string sid, tk_SProducts bas, ref string a_strErr)
        {
            int intInsert = 0;
            int intUpdate = 0;
            int intInsertHis = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertHis = "insert into tk_SProductsHis (SID,Ptype,ProductID,ProductName,Standard,MeasureUnit,DetailDesc,Price,OriginPlace,"
            + "CreateTime,CreateUser,Validate , ProcessUser='" + bas.Createuser + "' , ProcessTime = '" + bas.Createtime + "'"
            + ") select SID,Ptype,ProductID,ProductName,Standard,MeasureUnit,DetailDesc,Price,OriginPlace,"
            + "CreateTime,CreateUser,Validate from tk_SProducts where SID = '" + sid + "'";

            string strInsert = "update tk_SProducts set Standard = '" + bas.Standard + "',Measureunit = '" + bas.Measureunit + "',Detaildesc = '" + bas.Detaildesc + "',Price='" + bas.Price + "',OriginPlace='" + bas.Originplace + "'"
               + " where SID = '" + sid + "'";

            string strUpdate = "";
            if (bas.Productname != null)
                strUpdate = "update tk_SProducts set Productname = '" + bas.Productname + "'";

            try
            {
                if (strInsertHis != "")
                    intInsertHis = sqlTrans.ExecuteNonQuery(strInsertHis, CommandType.Text, null);
                if (strInsert != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsert, CommandType.Text, null);
                if (strUpdate != "")
                    intUpdate = sqlTrans.ExecuteNonQuery(strUpdate, CommandType.Text, null);
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

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="FDepartment"></param>
        /// <param name="PName"></param>
        /// <param name="Department"></param>
        /// <param name="Job"></param>
        /// <param name="Phone"></param>
        /// <param name="Mobile"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateBasinfo(Tk_SupplierBas bas, List<Tk_SContactPerson> listper, ref string Err)
        {
            int intHis = 0;
            int intInsert = 0;
            int intinsertHis = 0;
            int intlog = 0;
            int intlogPerson = 0;
            int intUP = 0;
            int intDel = 0;
            string strup = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            Tk_SContactPerson pa = new Tk_SContactPerson();
            #region 信息历史表

            //基础信息表内容插入历史表中
            string strInsertHis = "insert into tk_SupplierBasHis(SID,DeclareUnitID,DeclareDate,SupplierCode,SupplierType,COMNameC,COMShortName,COMNameE,COMWebsite,COMRAddress,COMCountry,ComAddress,COMArea,COMFactoryAddress,COMFactoryArea,COMCreateDate,COMLegalPerson,TaxRegistrationNo,IsCooperate,COMGroup,BusinessLicenseNo,StaffNum,RegisteredCapital,CapitalUnit,OrganizationCode,BankName,BankAccount,EnterpriseType,BusinessDistribute,BillingWay,DevelopStaffs,QAStaffs,ProduceStaffs,HasRegulation,ProductLineNum,WorkTime_Start,WorkTime_End,WorkDay_Start,WorkDay_End,BusinessScope,IsrankingIn5,RankingType,Ranking,ScaleType,QualityStandard,AnnualOutput,AnnualOutputValue,MainClient,Achievement,HasAuthorization,HasDrawing,AgentClass,HasImportMaterial,Award,FAX,State,CreateUser,CreateTime,Validate,Turnover,OtherType,NCreateUser,NCreateTime)";
            strInsertHis += "  select SID,DeclareUnitID,DeclareDate,SupplierCode,SupplierType,COMNameC,COMShortName,COMNameE,COMWebsite,COMRAddress,COMCountry,ComAddress,COMArea,COMFactoryAddress,COMFactoryArea,COMCreateDate,COMLegalPerson,TaxRegistrationNo,IsCooperate,COMGroup,BusinessLicenseNo,StaffNum,RegisteredCapital,CapitalUnit,OrganizationCode,BankName,BankAccount,EnterpriseType,BusinessDistribute,BillingWay,DevelopStaffs,QAStaffs,ProduceStaffs,HasRegulation,ProductLineNum,WorkTime_Start,WorkTime_End,WorkDay_Start,WorkDay_End,BusinessScope,IsrankingIn5,RankingType,Ranking,ScaleType,QualityStandard,AnnualOutput,AnnualOutputValue,MainClient,Achievement,HasAuthorization,HasDrawing,AgentClass,HasImportMaterial,Award,FAX,State,CreateUser,CreateTime,Validate,Turnover,OtherType,'" + acc.UserName + "','" + DateTime.Now + "'   from Tk_SupplierBas where sid='" + bas.Sid + "'";
            if (strInsertHis != "")
            {
                intinsertHis = SQLBase.ExecuteNonQuery(strInsertHis, "SupplyCnn");
            }
            #endregion

            #region 更新基础表
            string strInsert = "update Tk_SupplierBas set DeclareDate = '" + bas.DeclareDate + "',SupplierType = '" + bas.SupplierType + "',OtherType = '" + bas.OtherType + "',"
+ "COMNameC = '" + bas.COMNameC + "',COMShortName = '" + bas.COMShortName + "',COMWebsite = '" + bas.COMWebsite + "',COMArea = '" + bas.COMArea + "',COMNameE = '" + bas.COMNameE + "',COMCountry = '" + bas.COMCountry + "',"
+ "COMRAddress = '" + bas.COMRAddress + "',COMCreateDate = '" + bas.COMCreateDate + "',TaxRegistrationNo = '" + bas.TaxRegistrationNo + "',BusinessLicenseNo = '" + bas.BusinessLicenseNo + "',ComAddress = '" + bas.ComAddress + "',COMLegalPerson = '" + bas.COMLegalPerson + "',"
+ "COMFactoryAddress = '" + bas.COMFactoryAddress + "',COMFactoryArea = '" + bas.COMFactoryArea + "',OrganizationCode = '" + bas.OrganizationCode + "',COMGroup = '" + bas.COMGroup + "',RegisteredCapital = '" + bas.RegisteredCapital + "',IsCooperate = '" + bas.IsCooperate + "',"
+ "BankName = '" + bas.BankName + "',BankAccount = '" + bas.BankAccount + "',StaffNum = '" + bas.StaffNum + "',EnterpriseType = '" + bas.EnterpriseType + "',BusinessDistribute = '" + bas.BusinessDistribute + "',BillingWay = '" + bas.BillingWay + "',"
+ "Turnover = '" + bas.Turnover + "',DevelopStaffs = '" + bas.DevelopStaffs + "',QAStaffs = '" + bas.QAStaffs + "',ProduceStaffs = '" + bas.ProduceStaffs + "',Relation = '" + bas.Relation + "',HasRegulation = '" + bas.HasRegulation + "',"
+ "ProductLineNum = '" + bas.ProductLineNum + "',WorkTime_Start = '" + bas.WorkTime_Start + "',WorkTime_End = '" + bas.WorkTime_End + "',WorkDay_Start = '" + bas.WorkDay_Start + "',WorkDay_End='" + bas.WorkDay_End + "',"
+ "BusinessScope='" + bas.BusinessScope + "',IsrankingIn5='" + bas.IsrankingIn5 + "',RankingType='" + bas.RankingType + "',Ranking='" + bas.Ranking + "',ScaleType='" + bas.ScaleType + "',QualityStandard='" + bas.QualityStandard + "',AnnualOutput='" + bas.AnnualOutput + "',"
+ "AnnualOutputValue='" + bas.AnnualOutputValue + "',MainClient='" + bas.MainClient + "',Achievement='" + bas.Achievement + "',HasAuthorization='" + bas.HasAuthorization + "',HasDrawing='" + bas.HasDrawing + "',AgentClass='" + bas.AgentClass + "',HasImportMaterial='" + bas.HasImportMaterial + "', CreateTime='" + DateTime.Now + "',State='0',Award='" + bas.Award + "',FAX='" + bas.FAX + "' "
+ " where SID = '" + bas.Sid + "'";
            if (strInsert != "" && intinsertHis == 1)
                intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
            #endregion
            //联系人插入历史表中
            string strHis = "insert into tk_SContactPersonHis(SID,FDepartment,PName,Department,Job,Phone,Mobile,CreateUser,CreateTime,Validate,NCreateUser,NCreateTime) select  SID,FDepartment,PName,Department,Job,Phone,Mobile,CreateUser,CreateTime,Validate,'" + acc.UserName + "','" + DateTime.Now + "' from tk_SContactPerson where sid='" + bas.Sid + "'";
            if (strHis != "" && intInsert >= 1)
                intHis = SQLBase.ExecuteNonQuery(strHis, "SupplyCnn");

            #region 更新联系人


            strup = "";
            if (listper.Count > 0)//&& intHis >= 1
            {
                string delup = "delete tk_SContactPerson where sid='" + bas.Sid + "'";
                intDel = SQLBase.ExecuteNonQuery(delup, "SupplyCnn");
                strup = GSqlSentence.GetInsertByList<Tk_SContactPerson>(listper, "tk_SContactPerson");

            }
            if (strup != "")//&& intDel >= 1
                intUP = SQLBase.ExecuteNonQuery(strup, "SupplyCnn");
            #endregion

            string strlog = "";
            string strperLog = "";
            if (intInsert > 0)
                strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','更新供应商基本信息','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新供应商基本信息')";
            if (intUP > 0)
                strperLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + bas.Sid + "','更新供应商供应商联系人信息','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新供应商联系人')";
            try
            {
                if (strlog != "" && intUP >= 1)
                    intlog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                if (strperLog != "" && intlog >= 1)
                    intlogPerson = SQLBase.ExecuteNonQuery(strperLog, "SupplyCnn");
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intHis + intinsertHis + intInsert + intUP + intlog + intlogPerson + intDel) >= 2;
        }
        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="cbas"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateCus(tk_KClientBas cbas, ref string Err)
        {
            int intUp = 0;
            int intinsert = 0;
            int intInsertHis = 0;
            int intUPHis = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strHis = "insert into tk_KClientBasHis(KID,GainDate,DeclareUnit,DeclareUser,ChargeUser,IsShare,ShareUnits,CName,CShortName,Industry,StaffSize,Products,Phone,FAX,ZipCode,COMWebsite,ComAddress,Province,City,ClientDesc,Remark,CType,CClass,CSource,CRelation,Maturity,State,CreateUser,CreateTime,Validate)select   KID,GainDate,DeclareUnit,DeclareUser,ChargeUser,IsShare,ShareUnits,CName,CShortName,Industry,StaffSize,Products,Phone,FAX,ZipCode,COMWebsite,ComAddress,Province,City,ClientDesc,Remark,CType,CClass,CSource,CRelation,Maturity,State,CreateUser,CreateTime,Validate  from tk_KClientBas where kid='" + cbas.KID + "'";
            string strUpHis = "update tk_KClientBasHis set NCreateUser='" + acc.UserName + "',NCreateTime='" + DateTime.Now + "' where kid='" + cbas.KID + "'";
            string strUpdate = "update tk_KClientBas set GainDate='" + cbas.GainDate + "',DeclareUnit='" + cbas.DeclareUnit + "',DeclareUser='" + cbas.DeclareUser + "',ChargeUser='" + cbas.ChargeUser + "',IsShare='" + cbas.IsShare + "',ShareUnits='" + cbas.ShareUnits + "',CName='" + cbas.CName + "',"
+ "CShortName='" + cbas.CShortName + "',Industry='" + cbas.Industry + "',StaffSize='" + cbas.StaffSize + "',Products='" + cbas.Products + "',Phone='" + cbas.Phone + "',FAX='" + cbas.FAX + "',"
+ "ZipCode='" + cbas.ZipCode + "',COMWebsite='" + cbas.COMWebsite + "',ComAddress='" + cbas.ComAddress + "',Province='" + cbas.Province + "',City='" + cbas.City + "',"
+ "ClientDesc='" + cbas.ClientDesc + "',Remark='" + cbas.Remark + "',CType='" + cbas.CType + "',CClass='" + cbas.CClass + "',CSource='" + cbas.CSource + "',CRelation='" + cbas.CRelation + "',"
+ "Maturity='" + cbas.Maturity + "',State='" + cbas.State + "'"
+ " where KID='" + cbas.KID + "'";
            string strinsert = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + cbas.KID + "','更新客户信息','更新客户信息成功','" + DateTime.Now + "','" + acc.UserName + "','更新客户操作')";
            try
            {
                if (strHis != "")
                {
                    intInsertHis = SQLBase.ExecuteNonQuery(strHis, "SupplyCnn");
                }
                if (strUpHis != "")
                {
                    intUPHis = SQLBase.ExecuteNonQuery(strUpHis, "SupplyCnn");
                }
                if (strUpdate != "")
                {
                    intUp = SQLBase.ExecuteNonQuery(strUpdate, "SupplyCnn");
                }
                if (strinsert != "")
                {
                    intinsert = SQLBase.ExecuteNonQuery(strinsert, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUp + intinsert + intUPHis + intInsertHis) >= 4;
        }
        public static bool Updateisok(tk_IsNotSupplierBas cbas, ref string Err)
        {
            int intUp = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strUpdate = "update tk_IsNotSupplierBas set COMNameC='" + cbas.COMNameC + "',SupplyContent='" + cbas.SupplyContent + "',Contacts='" + cbas.Contacts + "',TelFax='" + cbas.TelFax + "',Phone='" + cbas.Phone + "',Mailbox='" + cbas.Mailbox + "',Remarks='" + cbas.Remarks + "',CreateTime='" + DateTime.Now + "' where sid='" + cbas.SID + "'";

            try
            {
                if (strUpdate != "")
                {
                    intUp = SQLBase.ExecuteNonQuery(strUpdate, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return intUp >= 1;
        }
        /// <summary>
        /// 更新客户联系人
        /// </summary>
        /// <param name="kcp"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateMans(tk_KContactPerson kcp, ref string Err)
        {
            int intUp = 0;
            int intInsert = 0;
            int intUpHis = 0;
            int intinsertLog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            string strInsert = "insert into tk_KContactPersonHis(KID,CName,Sex,Job,Birthday,Age,Mobile,FAX,Email,QQ,WeiXin,Remark,CreateUser,CreateTime,Validate) select KID,CName,Sex,Job,Birthday,Age,Mobile,FAX,Email,QQ,WeiXin,Remark,CreateUser,CreateTime,Validate from  tk_KContactPerson where KID='" + kcp.KID + "' and createtime='" + kcp.CreateTime + "'";
            if (strInsert != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
            }
            string strUpHis = "update tk_KContactPersonHis set NCreateTime='" + DateTime.Now + "',NCreateUser='" + acc.UserName + "' where kid='" + kcp.KID + "' and createtime='" + kcp.CreateTime + "'";
            string strUp = "update tk_KContactPerson set CName='" + kcp.CName + "',Sex='" + kcp.Sex + "',Job='" + kcp.Job + "',Birthday='" + kcp.Birthday + "',Age='" + kcp.Age + "',Mobile='" + kcp.Mobile + "',FAX='" + kcp.FAX + "',Email='" + kcp.Email + "',QQ='" + kcp.QQ + "',WeiXin='" + kcp.WeiXin + "',Remark='" + kcp.Remark + "' where KID='" + kcp.KID + "' and createtime='" + kcp.CreateTime + "'";
            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kcp.KID + "','更新联系人','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新联系人操作')";
            try
            {

                if (strUpHis != "")
                {
                    intUpHis = SQLBase.ExecuteNonQuery(strUpHis, "SupplyCnn");
                }
                if (strUp != "")
                {
                    intUp = SQLBase.ExecuteNonQuery(strUp, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intinsertLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUp + intInsert + intUpHis + intinsertLog) >= 4;
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="time"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateProDuct(tk_SProducts sp, byte[] fileByte, ref string Err)
        {
            int intUp = 0;
            int intInsert = 0;
            int intLog = 0;
            string strUp = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",fileByte)
            };
            string strInsert = "insert into tk_SProductsHis (SID,Ptype,ProductID,ProductName,Standard,MeasureUnit,DetailDesc,Price,OriginPlace,FFileName,FileInfo,CreateUser,CreateTime,Validate,ProcessUser,ProcessTime) ";
            strInsert += "   select  SID,Ptype,ProductID,ProductName,Standard,MeasureUnit,DetailDesc,Price,OriginPlace,FFileName,FileInfo,CreateUser,CreateTime,Validate,'" + acc.UserName + "','" + DateTime.Now + "' from tk_SProducts where sid='" + sp.Sid + "' and id='" + sp.ID + "' ";
            if (strInsert != "")
            {
                intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
            }

            if (sp.FFileName != "")//
            {
                strUp += "update tk_SProducts set Ptype='" + sp.Ptype + "',ProductID='" + sp.Productid + "',ProductName='" + sp.Productname + "',BYTime='" + sp.BYTtime + "',Standard='" + sp.Standard + "',MeasureUnit='" + sp.Measureunit + "',DetailDesc='" + sp.Detaildesc + "',Price='" + sp.Price + "',OriginPlace='" + sp.Originplace + "',FFileName='" + sp.FFileName + "',FileInfo=@fileByte  where sid='" + sp.Sid + "' and id='" + sp.ID + "' ";
            }
            else
            {
                strUp += "update tk_SProducts set Ptype='" + sp.Ptype + "',ProductID='" + sp.Productid + "',ProductName='" + sp.Productname + "',BYTime='" + sp.BYTtime + "',Standard='" + sp.Standard + "',MeasureUnit='" + sp.Measureunit + "',DetailDesc='" + sp.Detaildesc + "',Price='" + sp.Price + "',OriginPlace='" + sp.Originplace + "'  where sid='" + sp.Sid + "'and id='" + sp.ID + "' ";
            }

            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sp.Sid + "','更新产品信息','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新拟购产品操作')";
            try
            {

                if (strUp != "")
                {
                    intUp = SQLBase.ExecuteNonQuery(strUp, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUp + intInsert + intLog) >= 3;
        }
        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="sse"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateServer(tk_SService sse, byte[] filebyte, ref string Err)
        {
            int intUP = 0;
            int intInsert = 0;

            int intLoG = 0;
            string strUp = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fileByte",filebyte)
            };
            string strInsert = "insert into tk_SServiceHis(SID,ServiceID,ServiceName,ServiceDesc,Purpose,CreateUser,CreateTime,Validate,ProcessUser,ProcessTime)select  SID,ServiceID,ServiceName,ServiceDesc,Purpose,CreateUser,CreateTime,Validate,'" + acc.UserName + "','" + DateTime.Now + "' from tk_SService where sid='" + sse.Sid + "' and ServiceID='" + sse.ServiceID + "'";
            if (sse.FFileName != "" && sse.FFileName != null)
            {
                strUp += "update tk_SService set ServiceName='" + sse.ServiceName + "',ServiceDesc='" + sse.ServiceDesc + "',Purpose='" + sse.Purpose + "',FFileName='" + sse.FFileName + "',FileInfo=@fileByte  where sid='" + sse.Sid + "' and ServiceID='" + sse.ServiceID + "'";
            }
            else
            {
                strUp += "update tk_SService set ServiceName='" + sse.ServiceName + "',ServiceDesc='" + sse.ServiceDesc + "',Purpose='" + sse.Purpose + "'  where sid='" + sse.Sid + "' and ServiceID='" + sse.ServiceID + "'";
            }

            string strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sse.Sid + "','更新服务信息','更新服务成功','" + DateTime.Now + "','" + acc.UserName + "','更新服务操作')";
            try
            {
                if (strInsert != "")
                {
                    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                }

                if (strUp != "")
                {
                    intUP = SQLBase.ExecuteNonQuery(strUp, CommandType.Text, para, "SupplyCnn");
                }
                if (strLog != "")
                {
                    intLoG = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUP + intInsert + intLoG) >= 3;
        }
        public static bool UpdateUinte(tk_KClientBas kuwc, ref string Err)
        {
            int intUP = 0;
            //int intInsert = 0;
            // int intUpHis = 0;
            int intlog = 0;
            Acc_Account acc = GAccount.GetAccountInfo();
            // string strInsert = "insert into tk_KUnitWithClientHis(KID,UnitID,WType,DeclareUser,CreateUser,CreateTime,Validate) select  KID,UnitID,WType,DeclareUser,CreateUser,CreateTime,Validate from tk_KUnitWithClient  where kid='" + kuwc.KID + "'";
            //string strUpHis = "update tk_KUnitWithClientHis set NCreateTime='" + DateTime.Now + "',NCreateUser='" + acc.UserName + "' where kid='" + kuwc.KID + "'";
            string strUp = "update tk_KClientBas set ShareUnits='" + kuwc.ShareUnits + "' where KID='" + kuwc.KID + "'";
            string strlog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + kuwc.KID + "','更新共享部门','更新成功','" + DateTime.Now + "','" + acc.UserName + "','更新部门操作')";
            try
            {
                //if (strInsert != "")
                //{
                //    intInsert = SQLBase.ExecuteNonQuery(strInsert, "SupplyCnn");
                //}
                //if (strUpHis != "")
                //{
                //    intUpHis = SQLBase.ExecuteNonQuery(strUpHis, "SupplyCnn");
                //}
                if (strUp != "")
                {
                    intUP = SQLBase.ExecuteNonQuery(strUp, "SupplyCnn");
                }
                if (strlog != "")
                {
                    intlog = SQLBase.ExecuteNonQuery(strlog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                Err = e.Message;
                return false;
            }
            return (intUP + intlog) >= 2;
        }
        #endregion
        #region 文件导出Excel

        /// <summary>
        /// 导出客户信息到excel
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable CusToExcel(string where)
        {
            string strsql = "";
            strsql = "select a.KID,a.GainDate,a.DeclareUnit,a.DeclareUser,a.ChargeUser,a.IsShare,a.ShareUnits,a.CName";
            strsql += ",a.CShortName,a.Industry,a.StaffSize,a.Products,a.Phone,a.FAX,a.ZipCode,a.COMWebsite,a.ComAddress,a.Province";
            strsql += ",a.City,a.ClientDesc,a.Remark,a.CType,a.CClass,a.CSource,a.CRelation,a.Maturity,a.State";
            strsql += "  from tk_KClientBas a where 1=1 ";
            strsql += where;
            DataTable dt = SQLBase.FillTable(strsql, "SupplyCnn");
            if (dt == null)
                return null;
            else if (dt.Rows.Count == 0)
                return null;
            else return dt;
        }
        /// <summary>
        /// 合格供应商导出
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetOutExcel(string where)
        {
            string sql = "select  a.SID SIDs,CONVERT(varchar(12),a.AppTime,111) DeclareDate,b.text as SupplierType,a.COMNameC,a.COMShortName,d.text as COMCountry,c.text as ";
            sql += " COMArea,a.ComAddress,CONVERT(varchar(12),a.COMCreateDate,111) COMCreateDate,a.ThreeCertity,a.TaxRegistrationNo,a.BusinessLicenseNo";
            sql += "  ,a.OrganizationCode,a.COMRAddress,a.COMLegalPerson,a.COMFactoryAddress,a.RegisteredCapital,f.text as ";
            sql += "    CapitalUnit,cc.text as BankName,a.BankAccount,a.StaffNum,j.text as ";
            sql += " EnterpriseType,";
            sql += "  stuff((select ','+b.Text  from BGOI_BasMan..[tk_BusinessDistribute]  b where charindex(','+convert(varchar,b.sid),','+a.BusinessDistribute+'' )>0 for xml path('')),1,1,'')+ ':'+SUBSTRING(BusinessDistribute,CHARINDEX(':',a.BusinessDistribute)+1,17) BusinessDistribute,";
            sql += "  stuff((select ','+b.Text  from BGOI_BasMan..tk_ConfigBillWay  b where charindex(','+convert(varchar,b.sid),','+a.BillingWay+'' )>0 for xml path('')),1,1,'')+ ':'+SUBSTRING(BillingWay,CHARINDEX(':',a.BillingWay)+1,15) BillingWay,";
            sql += " a.Turnover,a.DevelopStaffs,a.QAStaffs,a.ProduceStaffs,";
            sql += "stuff((select ','+b.Text  from BGOI_BasMan..[tk_ConfigReation] b where charindex(','+convert(varchar,b.sid),','+a.Relation)>0 for xml path('')),1,1,'') Relation ";
            sql += " ,case  when a.HasRegulation='0' then '是'when a.HasRegulation='1' then '否' else '其他' end HasRegulation,a.WorkTime_Start,a.WorkTime_End,g.text as WorkDay_Start,h.text as WorkDay_End,a.BusinessScope,case  when a.IsrankingIn5='0' then '是'  when a.IsrankingIn5='1' then '否' else '其他' end    IsrankingIn5,case  when a.RankingType='0' then '国内'when a.RankingType='1' then '国外' else '' end RankingType,a.Ranking,";
            sql += " stuff((select ','+b.Text  from BGOI_BasMan..tk_ConfigScalType  b where charindex(','+convert(varchar,b.sid),','+a.ScaleType )>0 for xml path('')),1,1,'') ScaleType,";
            sql += " stuff((select ','+b.Text  from BGOI_BasMan..tk_ConfigQualityStandard  b  where charindex(','+convert(varchar,b.sid),','+a.QualityStandard )>0 for xml path('')),1,1,'') QualityStandard, ";
            sql += " case  when a.HasAuthorization='0' then '是'when a.HasAuthorization='1' then '否' else '其他' end HasAuthorization,case  when a.HasDrawing='0' then '有'when a.HasDrawing='1' then '无' else '其他' end HasDrawing,";
            sql += " stuff((select ','+b.Text  from BGOI_BasMan.. tk_ConfigAgentClass b where charindex(','+convert(varchar,b.sid),','+a.AgentClass)>0 for xml path('')),1,1,'') AgentClass, ";
            sql += " n.text as HasImportMaterial,m.text as FDepartment,y.PName,l.text as Department,p.text as Job,y.Phone,y.Mobile,y.Email ";
            sql += "  ,x.text as State  from dbo.tk_SupplierBas a";
            sql += "   left join dbo.tk_ConfigContent b on a.SupplierType=b.sid and b.type='SupplierType'";
            sql += "  left join tk_ConfigContent c on a.COMArea=c.sid and c.type='COMArea'";
            sql += " left join tk_ConfigContent d on a.COMCountry=d.sid and d.type='COMCtry'";
            sql += " left join tk_ConfigContent f on a.CapitalUnit=f.sid and f.type='CapitalUnit'";
            sql += " left join tk_ConfigContent g on a.WorkDay_Start =g.sid and g.type='week'";
            sql += " left join tk_ConfigContent j on a.EnterpriseType=j.sid and j.type='EnterpriseType'";
            sql += " left join tk_ConfigContent h on a.WorkDay_End=h.sid and h.type='Week' ";
            sql += "   LEFT JOIN tk_ConfigContent cc ON a.BankName=cc.SID AND cc.Type='BankName'";
            sql += " left join tk_ConfigContent n on a.HasImportMaterial=n.sid and n.type='HasImportMaterial' ";
            sql += " left join ( select * FROM ( SELECT  ROW_NUMBER() OVER(ORDER BY SID ASC) as rownum,* FROM  tk_SContactPerson) t  ";
            sql += "   Where NOT EXISTS( select * FROM ( SELECT  ROW_NUMBER() OVER(ORDER BY SID ASC) as rownum,* FROM  tk_SContactPerson)z ";
            sql += "  WHERE t.SID= z.SID  AND  t.rownum>z.rownum))y  ON  y.SID =a.SID ";
            sql += " left join tk_ConfigContent m on y.FDepartment=m.sid and m.type='FDepartment'";
            sql += " left join tk_ConfigContent  l on y.Department=l.sid and l.type='Department'";
            sql += " left join tk_ConfigContent  p on y.Job=p.sid and p.type='Job'";
            sql += " left join	tk_ConfigContent x on a.State=x.sid and x.type='zcstate'";
            sql += " where 1=1 and  (20<=a.state and a.state <32 or (a.state>56 and a.state<76) or a.state=10) and a.State not in(29) and a.Validate = 'v' ";//a.state>=10  and  a.state<29
            sql += where;
            sql += "  order by  a.SID  desc";

            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null) return null;
            else return dt;
        }
        /// <summary>
        /// 年度评审导出
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetPrintExcel(string where)
        {
            string sql = " SELECT distinct  b.SID,b.COMNameC,a.Score1,a.Score2,a.score3,a.score4,a.score5,a.resultdesc,a.declareunit,a.declareuser,  ";
            sql += " CONVERT(varchar(12),a.reviewdate,111) reviewdate FROM	 dbo.tk_SYRDetail  a  ";
            sql += " INNER JOIN	  dbo.tk_SupplierBas  b ON a.SID=b.sid ";
            sql += " where 1=1 and(b.nstate>59 and b.nstate<64)  ";
            sql += where;
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null) return null;
            else return dt;
        }
        #endregion
        public static tk_SProcessInfo getReport(string sid)
        {
            tk_SProcessInfo info = new tk_SProcessInfo();
            string sql = "select  a.PID,a.SID,a.Reason,a.DeclareUser,a. Opinions,a.OpinionsD,a.DeclareUnit,a.ReviewDate,a.SPState,a.Time1,a.ApprovalState from tk_SProcessInfo a";
            sql += "  where sid='" + sid + "'";
            // sql += " from tk_SProcessInfo a";
            //sql += " left join tk_ConfigContent b on a.Opinions=b.SID and b.type='Opinions'";
            // sql += "   where a.PID='" + pid + "'";
            //if (sid != "")
            //{
            //    sql += "  and a.SID='" + sid + "'";
            //}
            DataTable dts = SQLBase.FillTable(sql, "SupplyCnn");
            if (dts.Rows.Count > 0)
            {

                info.SID = dts.Rows[0]["SID"].ToString();
                info.Reason = dts.Rows[0]["Reason"].ToString();
                info.DeclareUser = dts.Rows[0]["DeclareUser"].ToString();
                info.Opinions = dts.Rows[0]["Opinions"].ToString();
                info.OpinionsD = dts.Rows[0]["OpinionsD"].ToString();
                info.DeclareUnit = dts.Rows[0]["DeclareUnit"].ToString();
                info.ReviewDate = Convert.ToDateTime(dts.Rows[0]["ReviewDate"]).ToString();
                info.SPState = dts.Rows[0]["SPState"].ToString();
                info.Time1 = Convert.ToDateTime(dts.Rows[0]["Time1"]);
                info.ApprovalState = dts.Rows[0]["ApprovalState"].ToString();
            }
            return info;
        }
        public static DataTable getRecordListUT(string commc, string pid, string sid, string supllycode, ref string Err)
        {
            Err = "";
            try
            {
                string sql = "select  a.PID,a.SID,a.Reason,a.DeclareUser,a.Opinions,a.OpinionsD,a.DeclareUnit,a.ReviewDate,a.SPState,a.Time1,a.ApprovalState";
                sql += " from tk_SProcessInfo a where a.pid='" + pid + "' and a.sid='" + sid + "' and a.COMNameC='" + commc + "'  and a.SupplierCode='" + supllycode + "'";
                DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
                return dt;
            }
            catch (SqlException e)
            {
                Err = e.Message;
                throw;
            }
        }
        public static DataTable getPerson(string sid)
        {
            //string sql = "select b.text as FDepartment,PName,c.text as Department,d.text as Job,Phone,Mobile,Email from tk_SContactPerson a ";
            //sql += " left join	   tk_ConfigContent	  b	   on a.fdepartment=b.sid and b.type='Fdepartment'";
            //sql += "left join	   tk_ConfigContent	  c	   on a.Department=c.sid and c.type='Department'";
            //sql += "left join	   tk_ConfigContent	  d	   on a.Job=d.sid and d.type='Job'";
            //sql += "  where a.sid='" + sid + "'";
            string sql = "SELECT FDepartment, Department,Job,Phone,Mobile,Email,PName FROM tk_SContactPerson where SID='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        public static DataTable getProcessINfo(string sid)
        {
            //string sql = "select b.COMNameC,a. SID,a.Reason,a.DeclareUser,c.text as Opinions,a.OpinionsD,a.DeclareUnit,a.ReviewDate,a.SPState,a.Time1,a.ApprovalState from tk_SProcessInfo a inner join tk_SupplierBas b on a.sid=b.sid ";
            //sql += " left join tk_ConfigContent c on  a.Opinions=c.SID and c.type='Opinions'";
            //sql += "   where b.sid='" + sid + "'";
            string sql = "select a.SID,a.DeclareDate,b.text as SupplierType,a.COMNameC,a.COMShortName,c.text as COMArea,d.text as ";
            sql += " COMCountry,a.ComAddress,a.COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress,a.COMLegalPerson ";
            sql += " ,a.COMFactoryAddress,a.OrganizationCode,a.RegisteredCapital,f.text as CapitalUnit,a.BankName,a.BankAccount,a.StaffNum,j.text as ";
            sql += "EnterpriseType,a.BusinessDistribute,a.BillingWay,a.Turnover,a.DevelopStaffs,a.QAStaffs,a.ProduceStaffs,a.";
            sql += "Relation,a.HasRegulation,a.WorkTime_Start,a.WorkTime_End,g.text as WorkDay_Start,h.text as WorkDay_End ";
            sql += " ,a.BusinessScope,a.IsrankingIn5,a.RankingType,a.Ranking,a.ScaleType,a.QualityStandard,a.HasAuthorization,a.HasDrawing,a.AgentClass,n.text as HasImportMaterial";
            sql += "  from dbo.tk_SupplierBas a ";
            sql += " left join dbo.tk_ConfigContent b on a.SupplierType=b.sid and b.type='SupplierType' ";
            sql += "  left join tk_ConfigContent c on a.COMArea=c.sid and c.type='COMArea'";
            sql += "left join tk_ConfigContent d on a.COMCountry=d.sid and d.type='COMCtry'";
            sql += " left join tk_ConfigContent f on a.CapitalUnit=f.sid and f.type='CapitalUnit'";
            sql += " left join tk_ConfigContent g on a.WorkDay_Start =g.sid and g.type='week'";
            sql += " left join tk_ConfigContent j on a.EnterpriseType=j.sid and j.type='EnterpriseType'";
            sql += "  left join tk_ConfigContent n on a.HasImportMaterial=n.sid and n.type='HasImportMaterial' ";
            sql += " left join tk_ConfigContent h on a.WorkDay_End=h.sid and h.type='Week'  where a.sid='" + sid + "'";

            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        public static DataTable getSPOK(string sid)
        {
            string sql = "select a.SID,a.DeclareDate,b.text as SupplierType,a.COMNameC,a.COMShortName,c.text as COMArea,d.text as ";
            sql += " COMCountry,a.ComAddress,CONVERT(varchar(12),a.COMCreateDate,111) COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress,a.COMLegalPerson ";
            sql += " ,a.COMFactoryAddress,a.OrganizationCode,a.RegisteredCapital,f.text as CapitalUnit,k.text as BankName,a.BankAccount,a.StaffNum,j.text as ";
            sql += " EnterpriseType,a.BusinessDistribute,a.BillingWay,a.Turnover,a.DevelopStaffs,a.QAStaffs,a.ProduceStaffs,a.";
            sql += "Relation,a.HasRegulation,a.WorkTime_Start,a.WorkTime_End,g.text as WorkDay_Start,h.text as WorkDay_End ";
            sql += " ,a.BusinessScope,a.IsrankingIn5,a.RankingType,a.Ranking,a.ScaleType,a.QualityStandard,a.HasAuthorization,a.HasDrawing,a.AgentClass,n.text as HasImportMaterial,a.ThreeCertity ";
            sql += "  from dbo.tk_SupplierBas a ";
            sql += " left join dbo.tk_ConfigContent b on a.SupplierType=b.sid and b.type='SupplierType' ";
            sql += "  left join tk_ConfigContent c on a.COMArea=c.sid and c.type='COMArea'";
            sql += "left join tk_ConfigContent d on a.COMCountry=d.sid and d.type='COMCtry'";
            sql += " left join tk_ConfigContent f on a.CapitalUnit=f.sid and f.type='CapitalUnit'";
            sql += " left join tk_ConfigContent g on a.WorkDay_Start =g.sid and g.type='week'";
            sql += " left join tk_ConfigContent j on a.EnterpriseType=j.sid and j.type='EnterpriseType'";
            sql += "  left join tk_ConfigContent n on a.HasImportMaterial=n.sid and n.type='HasImportMaterial' ";
            sql += " LEFT JOIN dbo.tk_ConfigContent k ON a.BankName =k.sid AND k.type='BankName'";
            sql += " left join tk_ConfigContent h on a.WorkDay_End=h.sid and h.type='Week'  where a.sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        public static DataTable getOutPrint(string sid, string type, string name, string area, string state)
        {
            string sql = "";
            if (type != "" || name != "" || area != "" || state != "")
            {
                #region MyRegion
                //sql = "SELECT a.SID,b.text as SupplierType,a.COMNameC,a.COMShortName,c.text as COMArea,d.text as COMCountry,a.ComAddress,a.COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress from tk_SupplierBas a";
                //sql += " left join tk_ConfigContent b on a.SupplierType=b.sid and b.type='suppliertype'";
                //sql += "  left join tk_ConfigContent c on a.COMArea=c.sid and c.type='COMArea'";
                //sql += "  left join tk_ConfigContent d on a.COMCountry=d.sid and d.type='COMCtry'";
                //sql += "  LEFT JOIN	(select RelevanceID,MAX(PID) PID from dbo.tk_SApproval GROUP BY RelevanceID) f on a.SID=f.RelevanceID ";
                //sql += "  LEFT JOIN   tk_SApproval e ON f.PID=e.PID";
                //sql += "  where 1=1 and  a.state>19 and a.state<54 and (e.approvalcontent='准出停止供货评审' or e.approvalcontent='准出暂停供货评审' or e.approvalcontent='准出淘汰供应商评审' or e.approvalcontent='恢复供应商') and  a.SupplierType='" + type + "' or a.COMNameC='" + name + "'  or a.COMArea='" + area + "' order by a.sid desc "; 
                #endregion
                sql = " Select * from (Select  distinct ROW_NUMBER() OVER (ORDER BY sid  desc) AS RowNumber,* from (select distinct SID,PID,SupplierType,COMArea,ComAddress,COMNameC,COMShortName,COMCountry,COMCreateDate,TaxRegistrationNo,BusinessLicenseNo,COMRAddress,deptname,Opinions,OpinionsD,state,ServiceName,ProductName from  (Select distinct ROW_NUMBER() OVER (ORDER BY a.CreateTime desc) AS RowNumber,a.SID,d.PID,g.text as SupplierType,f.text as COMArea,a.ComAddress,a.COMNameC,a.COMShortName,l.text as COMCountry,CONVERT(varchar(12),a.COMCreateDate,111) COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress,";
                sql += "   h.deptname,b.ReviewDate,b.Reason,b.DeclareUser,c.text as Opinions,b.OpinionsD,b.SPState,b.ApprovalState,m.Fcontent,k.text as state";
                sql += " ,ServiceName=[dbo].[GetService](o.sid) , ProductName=[dbo].[GetProductName](j.sid)";
                sql += "  from    tk_SupplierBas a    left join tk_SProcessInfo  b on a.sid=b.sid";
                sql += "  left join  tk_ConfigContent c on b.Opinions=c.SID and c.Type='Opinions'  left join tk_ConfigContent  g on a.SupplierType=g.SID and g.Type='SupplierType'  left join tk_ConfigContent k on a.State=k.SID and (k.Type='zcState' or k.type='rcstate')  left join tk_SService o on a.sid=o.sid";
                sql += "   LEFT JOIN tk_ConfigContent f ON a.COMArea=f.SID AND f.Type='COMArea'";
                sql += "  left join tk_ConfigContent l on a.COMCountry=l.sid and l.type='COMCtry'";
                sql += "  left join tk_SProducts	j on a.sid=j.sid   ";
                sql += "  LEFT JOIN	(select RelevanceID,MAX(PID) PID from dbo.tk_SApproval GROUP BY RelevanceID) d on a.SID=d.RelevanceID ";
                sql += "  LEFT JOIN   tk_SApproval e ON a.SID=e.RelevanceID   left join BJOI_UM..UM_UnitNew h on a.declareunitid=h.deptid  left join tk_FeedBack m on a.sid=m.sid";
                sql += "  where 1=1 and  (e.approvalcontent='准出停止供货评审' or e.approvalcontent='准出暂停供货评审' or e.approvalcontent='准出淘汰供应商评审' or e.approvalcontent='恢复供应商') and a.state>19 and a.state<54 and a.Validate = 'v' and a.SupplierType='" + type + "' or a.COMArea='" + area + "' or a.COMNameC='" + name + "' or a.State='" + state + "' ) AS TEMPTABLE";
                sql += "   ) x )#T	    order by sid desc";
            }
            else
            {
                #region MyRegion
                //sql = "SELECT a.SID,b.text as SupplierType,a.COMNameC,a.COMShortName,c.text as COMArea,d.text as COMCountry,a.ComAddress,a.COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress from tk_SupplierBas a";
                //sql += " left join tk_ConfigContent b on a.SupplierType=b.sid and b.type='suppliertype'";
                //sql += "  left join tk_ConfigContent c on a.COMArea=c.sid and c.type='COMArea'";
                //sql += "  left join tk_ConfigContent d on a.COMCountry=d.sid and d.type='COMCtry'";
                //sql += "  LEFT JOIN	(select RelevanceID,MAX(PID) PID from dbo.tk_SApproval GROUP BY RelevanceID) f on a.SID=f.RelevanceID ";
                //sql += "  LEFT JOIN   tk_SApproval e ON f.PID=e.PID";
                //sql += "  where 1=1 and  a.state>19 and a.state<54 and (e.approvalcontent='准出停止供货评审' or e.approvalcontent='准出暂停供货评审' or e.approvalcontent='准出淘汰供应商评审' or e.approvalcontent='恢复供应商') and a.SID='" + sid + "'   order by a.sid desc "; 
                #endregion

                sql = " Select * from (Select  distinct ROW_NUMBER() OVER (ORDER BY sid  desc) AS RowNumber,* from (select distinct SID,PID,SupplierType,COMArea,ComAddress,COMNameC,COMShortName,COMCountry,COMCreateDate,TaxRegistrationNo,BusinessLicenseNo,COMRAddress,deptname,Opinions,OpinionsD,state,ServiceName,ProductName from  (Select distinct ROW_NUMBER() OVER (ORDER BY a.CreateTime desc) AS RowNumber,a.SID,d.PID,g.text as SupplierType,f.text as COMArea,a.ComAddress,a.COMNameC,a.COMShortName,l.text as COMCountry,CONVERT(varchar(12),a.COMCreateDate,111) COMCreateDate,a.TaxRegistrationNo,a.BusinessLicenseNo,a.COMRAddress,";
                sql += "   h.deptname,b.ReviewDate,b.Reason,b.DeclareUser,c.text as Opinions,b.OpinionsD,b.SPState,b.ApprovalState,m.Fcontent,k.text as state";
                sql += " ,ServiceName=[dbo].[GetService](o.sid) , ProductName=[dbo].[GetProductName](j.sid)";
                sql += "  from    tk_SupplierBas a    left join tk_SProcessInfo  b on a.sid=b.sid";
                sql += "  left join  tk_ConfigContent c on b.Opinions=c.SID and c.Type='Opinions'  left join tk_ConfigContent  g on a.SupplierType=g.SID and g.Type='SupplierType'  left join tk_ConfigContent k on a.State=k.SID and (k.Type='zcState' or k.type='rcstate')  left join tk_SService o on a.sid=o.sid";
                sql += "   LEFT JOIN tk_ConfigContent f ON a.COMArea=f.SID AND f.Type='COMArea'";
                sql += "  left join tk_ConfigContent l on a.COMCountry=l.sid and l.type='COMCtry'";
                sql += "  left join tk_SProducts	j on a.sid=j.sid   ";
                sql += "  LEFT JOIN	(select RelevanceID,MAX(PID) PID from dbo.tk_SApproval GROUP BY RelevanceID) d on a.SID=d.RelevanceID ";
                sql += "  LEFT JOIN   tk_SApproval e ON a.SID=e.RelevanceID   left join BJOI_UM..UM_UnitNew h on a.declareunitid=h.deptid  left join tk_FeedBack m on a.sid=m.sid";
                sql += "  where 1=1 and  (e.approvalcontent='准出停止供货评审' or e.approvalcontent='准出暂停供货评审' or e.approvalcontent='准出淘汰供应商评审' or e.approvalcontent='恢复供应商') and a.state>19 and a.state<54 and a.Validate = 'v' and a.SID='" + sid + "' ) AS TEMPTABLE";
                sql += "   ) x )#T	    order by sid desc";
            }


            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        public static DataTable getYearRes(string sid)
        {
            string sql = "select DISTINCT a.sid, b.text as Result,a.ReviewDate,c.COMNameC,d.text as SupplierType ,j.DeptName,";
            sql += "  a.Score1,a.Score2,a.Score3,a.Score4,a.Score5,a.DeclareUser from	 tk_SYRDetail a";
            sql += "   left join tk_ConfigContent b on a.result=b.sid  and b.type='result'";
            sql += "   left join	  tk_SupplierBas c on a.sid=c.sid";
            sql += "   left join	 tk_ConfigContent d on c.suppliertype=d.sid and d.type='suppliertype'";
            sql += "   left join BJOI_UM..UM_UnitNew	j  ON c.DeclareUnitID=j.DeptId";
            sql += "   where a.sid='" + sid + "'	";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;

        }
        public static DataTable getDetailProcess(string sid)
        {
            string sql = "select  distinct a.sid,d.text as Opinions,a.OpinionsD,c.ApprovalTime";
            sql += ",a.Approval1User,a.ApprovalTime1,a.Approval2User,a.ApprovalTime2,a.Approval3User,a.ApprovalTime3,a.Approval4User,a.ApprovalTime4,a.Approval5User,a.ApprovalTime5 ";
            sql += "  from tk_SProcessInfo a inner join tk_SApproval c on a.sid=c.RelevanceID";
            sql += " left join tk_ConfigContent d on a.Opinions=d.SID and d.type='Opinions'";
            sql += "  where a.sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        public static DataTable getYD(string sid, string yid)
        {
            string sql = "select distinct(sid),IsPass,Opinion,Remark from tk_SYRDetail a ";
            sql += " inner join tk_SApproval b on a.sid=b.relevanceid where a.sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            if (dt == null)
            {
                return null;
            }
            return dt;
        }
        /// <summary>
        /// 根据产品分类查找产品名称
        /// </summary>
        /// <param name="strSelThird"></param>
        /// <returns></returns>
        public static DataTable GetSelDesc(string strSelThird)
        {
            string strSql = "";
            if (strSelThird != "")
            {
                strSql = " select distinct a.ProName from BGOI_Inventory..tk_ProductInfo a";
                strSql += "  left join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id where b.text='" + strSelThird + "'";
                DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
                return dt;
            }
            else
                return null;
        }
        public static DataTable GetStand(string stand)
        {
            string strSql = "";
            if (stand != "")
            {
                strSql = " select distinct a.spec from BGOI_Inventory..tk_ProductInfo a";
                strSql += "  left join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id where b.text='" + stand + "'";
                DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
                return dt;
            }
            else
                return null;
        }
        public static DataTable GetStand3(string stand)
        {
            string strSql = "";
            if (stand != "")
            {
                strSql = " select distinct a.pid from BGOI_Inventory..tk_ProductInfo a where a.spec='" + stand + "' ";
                //  strSql += "  left join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id where b.text='" + stand + "'";
                DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
                return dt;
            }
            else
                return null;
        }
        public static DataTable GetPce(string proname)
        {
            string strSql = "";
            if (proname != "")
            {
                strSql = " select distinct a.UnitPrice from BGOI_Inventory..tk_ProductInfo a";
                strSql += "  left join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id where a.proname='" + proname + "'";
                DataTable dt = SQLBase.FillTable(strSql, "MainInventory");
                return dt;
            }
            else
                return null;
        }
        public static string getDescLink(string strDesc, string strThirdType, ref string a_strErr)
        {
            string strSel = "";
            string strWhere = "";
            if (strDesc == "" && strThirdType == "")
                return "";
            else
            {
                if (strDesc != "")
                    strWhere = " a.ProName like '%" + strDesc + "%' ";
                else
                    strWhere = "";
                //
                if (strThirdType != "")// 产品分类有值 
                {
                    strSel += " select distinct isnull(a.ProName,'') as Text ,isnull(a.ProName,'') as ID  from dbo.tk_ProductInfo a ";
                    strSel += " left join tk_ConfigPType b on a.ptype=b.id ";
                    strSel += " where b.text ='" + strThirdType + "' and " + strWhere + " ORDER BY Text ASC ";
                }
                else // 产品分类为空 
                {
                    strSel = "  select distinct isnull(a.ProName,'') as Text ,isnull(a.ProName,'') as ID ";
                    strSel += " from dbo.tk_ProductInfo a where 1=1 and " + strWhere + " ORDER BY Text ASC ";
                }

                DataTable dt = SQLBase.FillTable(strSel, "MainInventory");
                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                else
                {
                    string str = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str += dt.Rows[i][0].ToString() + "!";
                    }
                    str = str.Substring(0, str.Length - 1);
                    return str;
                }
            }
        }
        public static string getStand(string stand, string strThirdType, ref string a_strErr)
        {
            string strSel = "";
            string strWhere = "";
            if (stand == "" && strThirdType == "")
                return "";
            else
            {
                if (stand != "")
                    strWhere = " a.Spec = '%" + stand + "%' ";
                else
                    strWhere = "";
                if (strThirdType != "")// 产品分类有值 
                {
                    strSel += " select distinct isnull(a.Spec,'') as Text ,isnull(a.Spec,'') as ID  from BGOI_Inventory..tk_ProductInfo a ";
                    strSel += " left join BGOI_Inventory..tk_ConfigPType b on a.ptype=b.id ";
                    strSel += " where b.text ='" + strThirdType + "' and " + strWhere + " ORDER BY Text ASC ";
                }
                else // 产品分类为空 
                {
                    strSel = "  select distinct isnull(a.Spec,'') as Text ,isnull(a.Spec,'') as ID ";
                    strSel += " from BGOI_Inventory..tk_ProductInfo a where 1=1 and " + strWhere + " ORDER BY Text ASC ";
                }

                DataTable dt = SQLBase.FillTable(strSel, "MainInventory");
                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                else
                {
                    string str = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str += dt.Rows[i][0].ToString() + "!";
                    }
                    str = str.Substring(0, str.Length - 1);
                    return str;
                }
            }
        }
        public static string getPro(string proid, string Stand, ref string a_strErr)
        {
            string strSel = "";
            //string strWhere = "";
            if (proid == "" && Stand == "")
                return "";
            else
            {
                //if (proid != "")
                //    strWhere = " a.PID = '" + proid + "' ";
                //else
                //    strWhere = "";
                if (Stand != "")// 产品类型有值 
                {
                    strSel += " select distinct isnull(a.PID,'') as Text ,isnull(a.PID,'') as ID  from dbo.tk_ProductInfo a ";
                    strSel += " left join tk_ConfigPType b on a.ptype=b.id ";

                    strSel += " where  a.Spec ='" + Stand + "' ORDER BY Text ASC ";
                }
                else // 产品类型为空 
                {
                    strSel = "  select distinct isnull(a.PID,'') as Text ,isnull(a.PID,'') as ID ";
                    strSel += " from dbo.tk_ProductInfo a where 1=1 and a.spec='" + Stand + "' ORDER BY Text ASC ";
                }

                DataTable dt = SQLBase.FillTable(strSel, "MainInventory");
                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                else
                {
                    string str = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str += dt.Rows[i][0].ToString() + "!";
                    }
                    str = str.Substring(0, str.Length - 1);
                    return str;
                }
            }
        }
        public static string getPrice(string price, string strThirdType, ref string a_strErr)
        {
            string strSel = "";
            //string strWhere = "";
            if (price == "" && strThirdType == "")
                return "";
            else
            {
                //if (price != "")
                //    strWhere = " a.UnitPrice ='" + price + "' ";
                //else
                //    strWhere = "";
                if (strThirdType != "")// 产品单价有值 
                {
                    strSel += " select distinct isnull(a.UnitPrice,0) as Text ,isnull(a.UnitPrice,0) as ID  from dbo.tk_ProductInfo a ";
                    strSel += " left join tk_ConfigPType b on a.ptype=b.id ";
                    strSel += " where a.proname ='" + strThirdType + "' ORDER BY Text ASC ";
                }
                else // 产品单价为空 
                {
                    strSel = "  select distinct isnull(a.UnitPrice,0) as Text ,isnull(a.UnitPrice,0) as ID ";
                    strSel += " from dbo.tk_ProductInfo a where 1=1  ORDER BY Text ASC ";
                }

                DataTable dt = SQLBase.FillTable(strSel, "MainInventory");
                if (dt == null || dt.Rows.Count <= 0)
                    return null;
                else
                {
                    string str = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str += dt.Rows[i][0].ToString() + "!";
                    }
                    str = str.Substring(0, str.Length - 1);
                    return str;
                }
            }
        }
        public static DataTable GetSubType(string strFirst)
        {
            string strSel = "";
            if (strFirst == "--请选择--" || strFirst == "")// 大类为空
                strSel = " select  distinct SID,text from  dbo.tk_ConfigContent  where type='Ftype'";
            else
                strSel = "select  distinct SID,text from  dbo.tk_ConfigContent  where type='Ftype' and Typedesc='" + strFirst + "' ";
            DataTable dtInfo = SQLBase.FillTable(strSel, "SupplyCnn");
            return dtInfo;
        }
        public static DataTable GetItem(string strItem)
        {
            string strSel = "";
            if (strItem == "--请选择--" || strItem == "")// 大类为空
                strSel = " select  distinct SID, text from  dbo.tk_ConfigContent  where type='Item'";
            else
                strSel = "select  distinct SID, text from  dbo.tk_ConfigContent  where type='Item' and typedesc='" + strItem + "' ";
            DataTable dtInfo = SQLBase.FillTable(strSel, "SupplyCnn");
            return dtInfo;
        }
        public static DataTable GetCompare(string strFirst)
        {
            string strSel = "";
            if (strFirst == "--请选择--" || strFirst == "")// 大类为空
                strSel = " select  distinct SID, text from  dbo.tk_ConfigContent  where type='Item'";
            else
                strSel = "select  distinct SID, text from  dbo.tk_ConfigContent  where type='Item' and typedesc='" + strFirst + "' ";
            DataTable dtInfo = SQLBase.FillTable(strSel, "SupplyCnn");
            return dtInfo;
        }
        public static UIDataTable GetApprovalUser(int a_intPageSize, int a_intPageIndex, string where)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getApprovalUser", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable GetApproval(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            UIDataTable instData = new UIDataTable();
            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@UnitID",unitid)
                };
            DataSet DO_Order = SQLBase.FillDataSet("getZS", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static UIDataTable GetApprovalWarn(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where),
                    new SqlParameter("@UnitID",unitid)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getAptitude", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
        public static string GetNewTimeOut()
        {
            string CK1time = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CK' and TimeType = 'ZiZhi'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CK1time = dt.Rows[0]["num"].ToString();
            return CK1time;
        }
        public static string GetNewZSTimeOut()
        {
            string CKtime = "";
            string strSql = "select num from tk_ConfigWarnTime where Type = 'CK' and TimeType = 'ZhenShu'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            if (dt.Rows.Count > 0)
                CKtime = dt.Rows[0]["num"].ToString();
            return CKtime;
        }

        public static DataTable GetJob(string unitid, string userName, string Userid)
        {
            string sql = "";
            //sql = " SELECT  * FROM tk_SupplierBas a LEFT JOIN [BJOI_UM]..[UM_UserNew] b on a.DeclareUnitID=b.DeptId";
            //sql += "  where ( a.State IN('3','22','25','28','51','73') or a.NState IN('61'))  AND b.UserId='" + Userid + "'  ORDER BY a.CreateTime";
            sql = "SELECT * FROM tk_SupplierBas a ";
            sql += "  where  (a.State IN('3','22','25','28','51','73') or a.NState IN('61'))  AND a.Validate='v'  ORDER BY a.CreateTime";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetnewBas(string unitid, string userName, string Userid)
        {
            string sql = "";
            sql += "SELECT  * FROM tk_SupplierBasHis a LEFT JOIN [BJOI_UM]..[UM_UserNew] b on a.DeclareUnitID=b.DeptId ";
            sql += " where  b.UserId='" + Userid + "'  ORDER BY a.NCreateTime";
            DataTable dt = SQLBase.FillTable(sql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownload(string sid, string fid, string filename)
        {
            string strSql = "";
            if (filename != "")
            {
                strSql += "select fid,sid,fileinfo,ffilename,filetype from tk_SFileInfo where sid='" + sid + "'and fid='" + fid + "' and Validate = 'v' ";
            }
            else
            {
                strSql += ""; //"select fid,sid,fileinfo,ffilename,filetype from tk_SFileInfo where sid='" + id + "' and Validate = 'v' ";
            }
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetpriceDownload(string sid, string name)
        {
            string strSql = "";
            if (name != "")
            {
                strSql += "select * from tk_PriceUp where sid='" + sid + "' and Validate='v' ";
            }
            //else
            //{
            //    strSql += "";
            //}
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable getLoadUnit(string sid)
        {
            string strSql = "";
            //if (filename != "")
            //{
            strSql += "select fid,sid,FileInfo,FFileName,filetype from tk_SFileInfo where sid='" + sid + "' and Validate = 'v' ";
            //}
            //else
            //{
            //    strSql += ""; 
            //}
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownLoadProduct(string id, string timeout, string filename)
        {
            string strSql = "";
            if (filename != "")
            {
                strSql += "select id,sid,fileinfo,ffilename from tk_SProducts where sid='" + id + "'and id='" + timeout + "' and Validate = 'v' ";
            }
            //else
            //{
            //    strSql += "";
            //}
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownLoadServer(string sid, string id, string filename)
        {
            string strSql = "";
            if (filename != "")
            {
                strSql += "select serviceid,sid,fileinfo,ffilename from tk_SService where sid='" + sid + "'and ServiceID='" + id + "' and Validate = 'v' ";
            }

            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownload1(string id, string fid, string filename)
        {
            string strSql = "";
            if (filename != "")
            {
                strSql += "select fid,sid,fileinfo,cfilename,filetype from tk_SCertificate where sid='" + id + "'and fid='" + fid + "' and Validate = 'v'";
            }
            else
            {
                strSql += "";
            }
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadFile(string id)
        {
            string strSql = "select fileinfo,ffilename from tk_SFileInfo where fid = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownload(string fid)
        {
            string strSql = "select FileInfo,FFileName from tk_SFileInfo where fid = '" + fid + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadProduct(string id)
        {
            string strSql = "select fileinfo,ffilename from tk_SProducts where id = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadAward(string id)
        {
            string strSql = "select Award,AwardInfo from tk_Award where id = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadPriceNew(string id)
        {
            string strSql = "select PriceName,PriceInfo from tk_PriceUp where id = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadPro(string id)
        {
            string strSql = "select FileInfo,FFileName from tk_SProducts where id = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewDownloadServer(string id)
        {
            string strSql = "select fileinfo,ffilename from tk_SService where serviceid = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewDownloadFiles(string id)
        {
            string strSql = "select FileInfo,FFileName,MFFileName,MFileInfo from tk_SFileInfo where FID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetNewDownloadZhenshu(string id)
        {
            string strSql = "select FileInfo,CFileName from tk_SCertificate where FID = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static DataTable GetDownloadServer(string id)
        {
            string strSql = "select fileinfo,ffilename from tk_SService where serviceid = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }

        public static DataTable GetDownloadCerty(string id)
        {
            string strSql = "select fileinfo,cfilename from tk_SCertificate where fid = '" + id + "'";
            DataTable dt = SQLBase.FillTable(strSql, "SupplyCnn");
            return dt;
        }
        public static int DeleteFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsertOrder = "UPDATE tk_SFileInfo SET FFileName=null,FileInfo=null,FileType=null WHERE FID='" + ID + "'";
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
        public static int DeleteMFile(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsertOrder = "UPDATE tk_SFileInfo SET FFileName=null,FileInfo=null  WHERE FID='" + ID + "'";
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
        public static int DeleteProduct(string id, ref string Err)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertOrder = "UPDATE tk_SProducts SET FFileName=null,FileInfo=null WHERE ID='" + id + "'	";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }

            return intInsert;
        }
        public static int DeleteServer(string id, ref string Err)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            string strInsertOrder = "UPDATE tk_SService SET FFileName=null,FileInfo=null WHERE ServiceID='" + id + "'";
            try
            {
                if (strInsertOrder != "")
                    intInsert = sqlTrans.ExecuteNonQuery(strInsertOrder, CommandType.Text, null);
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                Err = e.Message;
                return -1;
            }

            return intInsert;
        }
        public static int DeleteCerty(string ID, ref string a_strErr)
        {
            int intInsert = 0;
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");

            string strInsertOrder = "UPDATE tk_SCertificate SET CFileName=null,FileInfo=null,FileType=null WHERE FID='" + ID + "'";
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
        public static DataTable getConfigType(string type)
        {
            string strSql = "select SID,Text from [BGOI_BasMan]..tk_ConfigContent where Type = '" + type + "' ";
            DataTable dt = SQLBase.FillTable(strSql);
            return dt;
        }
        /// <summary>
        /// 获得联系人文本框的值
        /// </summary>
        /// <param name="butype"></param>
        /// <returns></returns>
        public static string getHavetype(string sid)
        {
            string Content = "";
            string sql = "SELECT PName,Phone,Mobile,Email FROM  dbo.tk_SContactPerson WHERE sid='" + sid + "'";
            DataTable dt = SQLBase.FillTable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string pname = "";
                    string phone = "";
                    string mobile = "";
                    string email = "";
                    pname = dt.Rows[i]["PName"].ToString();
                    phone = dt.Rows[i]["Phone"].ToString();
                    mobile = dt.Rows[i]["Mobile"].ToString();
                    email = dt.Rows[i]["Email"].ToString();
                }
                DataTable dt2 = dt;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Content += dt.Rows[i]["PName"].ToString() + "/" + dt.Rows[i]["Phone"].ToString() + "/" + dt.Rows[i]["Mobile"].ToString() + "/" + dt.Rows[i]["Email"].ToString() + "@";
                }
                Content = Content.TrimEnd('@');
            }
            return Content;

        }

        public static string PreGetTaskNo(string sel)
        {
            string strID = "";
            string xid = "";
            string strSqlID = "select max(XID) from tk_ConfigContent where Type='" + sel + "'";
            DataTable dtID = SQLBase.FillTable(strSqlID, "SupplyCnn");
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

        public static int InsertApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SProcessInfo info)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intcomlog = 0;
            string sql = "";
            string intsercom = "";
            int intcom = 0;

            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');
            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/52/tk_PID/54/tk_UserLog
            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            intsercom = "update tk_SProcessInfo SET OpinOut='" + info.OpinOut + "' , isCompany='" + info.isCompany + "' WHERE SID='" + RelevanceID + "'";
            if (strInsertBas != "")
                intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            //更新评审状态的同时也更新当前状态
            if (intInsertBas == 1 && webkey == "准出停止供货评审")
            {
                sql = " UPDATE tk_SupplierBas SET State='22',WState='3' where SID='" + RelevanceID + "'";
            }
            else if (intInsertBas == 1 && webkey == "准出暂停供货评审")
            {
                sql = " UPDATE tk_SupplierBas SET State='25',WState='2'where SID='" + RelevanceID + "'";
            }
            else if (intInsertBas == 1 && webkey == "准出淘汰供应商评审")
            {
                sql = " UPDATE tk_SupplierBas SET State='28',WState='4' where SID='" + RelevanceID + "'";
            }
            string strUpdateBas = "";
            if (arr[2].IndexOf("..") > 0)
                strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            //string strcom = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + info.OpinOut + "','" + info.isCompany + "','" + DateTime.Now + "','" + account.UserName + "','公司级审批')";
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','提交" + webkey + "审批操作','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "')";
            try
            {

                if (strUpdateBas != "")
                    intUpdateBas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                if (intsercom != "") intcom = SQLBase.ExecuteNonQuery(intsercom, "SupplyCnn");
                //if (strcom != "")
                //    intcomlog = SQLBase.ExecuteNonQuery(strcom, "SupplyCnn");
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
        public static int InserttaotaiApproval(string RelevanceID, ref string a_strErr, tk_SProcessInfo info)
        {
            //int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intcomlog = 0;
            string sql = "";
            string intsercom = "";
            int intcom = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            //  string[] arr = folderBack.Split('/');
            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog
            //string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            // + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            intsercom = "update tk_SProcessInfo SET ISAgree='" + info.ISAgree + "' , OpinionsD='" + info.OpinionsD + "' WHERE SID='" + RelevanceID + "'";
            //if (strInsertBas != "")
            //    intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            //if (intInsertBas == 1 )
            //{
            // sql = " UPDATE tk_SupplierBas SET nState='61'where SID='" + RelevanceID + "'";
            //}

            string strUpdateBas = "";
            if (info.ISAgree == "0")
            {
                strUpdateBas = "update tk_SupplierBas set nState = '61' where SID = '" + RelevanceID + "'";
            }
            else
            {
                strUpdateBas = "update tk_SupplierBas set nState = '63' where SID = '" + RelevanceID + "'";
            }


            //string strcom = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + info.ISAgree + "','" + info.OpinionsD + "','" + DateTime.Now + "','" + account.UserName + "','公司级审批')";
            string strInsertLog = "insert into tk_UserLog  values ('" + RelevanceID + "','提交年度评审操作','提交年度评审成功','" + DateTime.Now + "','" + account.UserName + "','年度评审')";
            try
            {

                if (strUpdateBas != "")
                    intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, "SupplyCnn");
                if (strInsertLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");
                if (intsercom != "")
                    intcom = SQLBase.ExecuteNonQuery(intsercom, "SupplyCnn");
                //if (strcom != "")
                //    intcomlog = SQLBase.ExecuteNonQuery(strcom, "SupplyCnn");
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdateBas + intLog;
        }
        public static int InsertzhunchuApproval(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SProcessInfo info)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intcomlog = 0;
            string sql = "";
            string intsercom = "";
            int intcom = 0;
            int intsql = 0;
            string strUpdateBas = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog 停止
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog 暂停
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog 淘汰
            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            intsercom = "update tk_SProcessInfo SET ISAgree='" + info.ISAgree + "' , OpinionsD='" + info.OpinionsD + "' WHERE SID='" + RelevanceID + "'";
            if (strInsertBas != "")
                intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            //if (intInsertBas == 1 && webkey == "年度评审")
            //{
            //    sql = " UPDATE tk_SupplierBas SET nState='61'where SID='" + RelevanceID + "'";
            //}
            if (intInsertBas == 1 && info.ISAgree == "1")
            {
                sql = " UPDATE tk_SupplierBas SET State='31'where SID='" + RelevanceID + "'";
            }
            else
            {

                if (arr[2].IndexOf("..") > 0)
                    strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
                else
                    strUpdateBas = "update [" + arr[0] + "].." + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            string strcom = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + info.ISAgree + "','" + info.OpinionsD + "','" + DateTime.Now + "','" + account.UserName + "','部门级审批')";
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','提交" + webkey + "审批操作','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "')";
            try
            {
                if (sql != "")
                    intsql = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                if (strUpdateBas != "")
                    intUpdateBas = sqlTrans.ExecuteNonQuery(strUpdateBas, CommandType.Text, null);
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                if (intsercom != "")
                    intcom = SQLBase.ExecuteNonQuery(intsercom, "SupplyCnn");
                if (strcom != "")
                    intcomlog = SQLBase.ExecuteNonQuery(strcom, "SupplyCnn");

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intUpdateBas + intcomlog;
        }
        public static int InserthuifuApproval(string RelevanceID, ref string a_strErr, tk_SProcessInfo info)
        {
            int intInsertBas = 0;
            int intLog = 0;
            int intcomlog = 0;
            string sql = "";
            string intsercom = "";
            int intcom = 0;
            int intsql = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货
            intsercom = "update tk_SProcessInfo SET ISAgree='" + info.ISAgree + "' , OpinionsD='" + info.OpinionsD + "' WHERE SID='" + RelevanceID + "'";
            if (info.ISAgree == "1")
            {
                sql = " UPDATE tk_SupplierBas SET State='74' where SID='" + RelevanceID + "'";
            }
            else
            {
                sql = "update tk_SupplierBas set State = '73' where SID = '" + RelevanceID + "'";
            }
            if (info.ISAgree == "0")
            {
                info.ISAgree = "是";
            }
            else
            {
                info.ISAgree = "否";
            }
            string strcom = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + info.ISAgree + "','" + info.OpinionsD + "','" + DateTime.Now + "','" + account.UserName + "','部门级审批')";
            string strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','提交恢复供货审批操作','提交恢复供货审批成功','" + DateTime.Now + "','" + account.UserName + "','恢复供货')";
            try
            {
                if (sql != "")
                    intsql = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                if (strInsertLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");
                if (intsercom != "")
                    intcom = SQLBase.ExecuteNonQuery(intsercom, "SupplyCnn");
                if (strcom != "")
                    intcomlog = SQLBase.ExecuteNonQuery(strcom, "SupplyCnn");

                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intcomlog;
        }
        public static int Insertzhunrul(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SUPSugestion sgs)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            string sql = "";
            string strsug = "";
            int intsug = 0;
            int intsql = 0;
            int intfuzelog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            string[] arr = folderBack.Split('/');

            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/52/tk_PID/54/tk_UserLog
            string strInsertBas = "insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            if (strInsertBas != "")
                intInsertBas = sqlTrans.ExecuteNonQuery(strInsertBas, CommandType.Text, null);
            if (intInsertBas == 1 && webkey == "准入评审")
            {
                strsug = "INSERT  into tk_SUPSugestion (SID ,SState,Sperson,SCreate,SContent) VALUES('" + RelevanceID + "','" + sgs.SState + "','" + sgs.Sperson + "','" + sgs.SCreate + "','" + sgs.SContent + "')";
                sql = "update tk_SupplierBas set wstate='0' where sid='" + RelevanceID + "'";
            }
            string strUpdateBas = "";
            if (sgs.SState == "0")
                strUpdateBas = "update " + arr[2] + " set State = '" + arr[5] + "' where " + arr[3] + " = '" + RelevanceID + "'";
            else
                strUpdateBas = "update " + arr[2] + " set State = '-4' where " + arr[3] + " = '" + RelevanceID + "'";

            string strinsertfuzelog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + sgs.SState + "','" + sgs.SContent + "','" + DateTime.Now + "','" + account.UserName + "','部门负责人审批')";
            string strInsertLog = "insert into [" + arr[0] + "].." + arr[8] + " values ('" + RelevanceID + "','部门负责人评审','提交" + webkey + "审批成功','" + DateTime.Now + "','" + account.UserName + "','会签审批')";
            try
            {
                if (strsug != "")
                    intsug = SQLBase.ExecuteNonQuery(strsug, "SupplyCnn");
                if (strUpdateBas != "")
                    intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, "SupplyCnn");
                if (sql != "")
                {
                    intsql = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strInsertLog != "")
                    intLog = sqlTrans.ExecuteNonQuery(strInsertLog, CommandType.Text, null);
                if (strinsertfuzelog != "")
                    intfuzelog = SQLBase.ExecuteNonQuery(strinsertfuzelog, "SupplyCnn");
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intInsertBas + intUpdateBas + intsug;
        }
        public static int Insertzhunrul1(ref string a_strErr, tk_SUPSugestion sgs)
        {

            int intUpdateBas = 0;
            int intLog = 0;
            string sql = "";
            string strsug = "";
            int intsug = 0;
            int intsql = 0;
            int intfuzelog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/52/tk_PID/54/tk_UserLog

            strsug = "INSERT  into tk_SUPSugestion (SID ,SState,Sperson,SCreate,SContent) VALUES('" + sgs.Sid + "','" + sgs.SState + "','" + sgs.Sperson + "','" + sgs.SCreate + "','" + sgs.SContent + "')";
            sql = "update tk_SupplierBas set wstate='0' where sid='" + sgs.Sid + "'";//当前状态

            string strUpdateBas = "";
            if (sgs.SState == "0")
                strUpdateBas = "update tk_SupplierBas set State = '3' where SID = '" + sgs.Sid + "'";
            else
                strUpdateBas = "update tk_SupplierBas set State = '-4' where SID = '" + sgs.Sid + "'";

            //string strinsertfuzelog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + sgs.Sid + "','" + sgs.SState + "','" + sgs.SContent + "','" + DateTime.Now + "','" + account.UserName + "','部门负责人审批')";
            string strInsertLog = "insert into tk_UserLog  values ('" + sgs.Sid + "','部门负责人评审','提交会签审批成功','" + DateTime.Now + "','" + account.UserName + "','会签审批')";
            try
            {
                if (strsug != "")
                    intsug = SQLBase.ExecuteNonQuery(strsug, "SupplyCnn");
                if (strUpdateBas != "")
                    intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, "SupplyCnn");
                if (sql != "")
                {
                    intsql = SQLBase.ExecuteNonQuery(sql, "SupplyCnn");
                }
                if (strInsertLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");
                //if (strinsertfuzelog != "")
                //    intfuzelog = SQLBase.ExecuteNonQuery(strinsertfuzelog, "SupplyCnn");
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }

            return intUpdateBas + intsug;
        }

        public static int Insertrecover(string RelevanceID, ref string a_strErr, tk_SUPSugestion sgs, Tk_SupplierBas bas)
        {
            //int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            string strsug = "";
            int intsug = 0;
            //string str = "";
            // int intstr = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            string createUser = account.UserID.ToString();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open();
            #region MyRegion
            // string[] arr = folderBack.Split('/');

            // BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/51/tk_PID/56/tk_UserLog
            // string strInsertBas = "";
            //"insert into [" + arr[0] + "].." + arr[1] + " (PID,RelevanceID,ApprovalContent,CreateUser,CreateTime,Validate,State,Job,ApprovalPersons,ApprovalLevel,AppType)"
            // + " select '" + PID + "','" + RelevanceID + "','" + webkey + "','" + createUser + "','" + DateTime.Now + "','v','0',Duty,UserId,[Level],AppType from [BJOI_UM]..UM_Examine where BuType = '" + webkey + "'";
            //if (strInsertBas != "")
            //    intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, "SupplyCnn");
            //if (intInsertBas == 1 )
            //{ 
            #endregion
            strsug = "INSERT  into tk_SUPSugestion (SID ,SState,Sperson,SCreate,SContent) VALUES('" + RelevanceID + "','" + sgs.SState + "','" + sgs.Sperson + "','" + sgs.SCreate + "','" + sgs.SContent + "')";
            // }
            string strUpdateBas = "";
            //如果评审状态是待部门级恢复审批就走准出，如果是年审状态是待部门级审批走年审
            if (sgs.SState == "0" && bas.State == 50)
            {
                strUpdateBas = "update tk_SupplierBas set State = '51' where SID = '" + RelevanceID + "'";
            }
            if (sgs.SState == "1" && bas.State == 50)
            {
                strUpdateBas = "update tk_SupplierBas set State = '55' where SID = '" + RelevanceID + "'";
            }
            if (sgs.SState == "0" && bas.NState == 60)
            {
                strUpdateBas = "update tk_SupplierBas set nState = '61' where SID = '" + RelevanceID + "'";
            }
            if (sgs.SState == "1" && bas.NState == 60)
            {
                strUpdateBas = "update tk_SupplierBas set nState = '63' where SID = '" + RelevanceID + "'";
            }
            string strInsertLog = "";
            if (strUpdateBas != "")
                intUpdateBas = SQLBase.ExecuteNonQuery(strUpdateBas, "SupplyCnn");
            if (intUpdateBas == 1)
            {
                //str = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + RelevanceID + "','" + sgs.SState + "','" + sgs.SContent + "','" + DateTime.Now + "','" + account.UserName + "','部门级恢复供应商审批')";
                strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','提交恢复供应商审批操作','提交恢复供应商审批成功','" + DateTime.Now + "','" + account.UserName + "','恢复供应商')";
            }
            try
            {
                if (strsug != "")
                    intsug = SQLBase.ExecuteNonQuery(strsug, "SupplyCnn");

                //if (str != "")
                //    intstr = SQLBase.ExecuteNonQuery(str, "SupplyCnn");
                if (strInsertLog != "")
                    intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");
                sqlTrans.Close(true);
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                a_strErr = e.Message;
                return -1;
            }
            return intUpdateBas + intsug;
        }
        #region 自由审批
        public static int UpdateApproval(string IsPass, string Opinion, string PID, string RelevanceID, ref string a_strErr, string job)
        {
            int intInsertBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            string strInsertLog = "";
            string strInsertBas = "";
            string strAllBas = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();

            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/51/55/tk_PID/54/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货
            if (IsPass == "是" && job == "副总经理")
            {
                //插入审批记录
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','1')";
            }
            else if (IsPass == "否" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-1')";
                strAllBas = "update tk_SupplierBas set State = '6',wstate='0' where SID = '" + RelevanceID + "'";
            }
            if (IsPass == "是" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','2')";

            }
            else if (IsPass == "否" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-2')";
                strAllBas = "update tk_SupplierBas set State = '6',wstate='0' where SID = '" + RelevanceID + "'";
            }
            if (IsPass == "是" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','3')";
                strAllBas = "update tk_SupplierBas set State = '10',wstate='1' where SID = '" + RelevanceID + "'";
            }
            else if (IsPass == "否" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','准入评审','" + UserId + "','" + job + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-3')";
                strAllBas = "update tk_SupplierBas set State = '6',wstate='0' where SID = '" + RelevanceID + "'";
            }

            strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','会签审批操作','会签审批操作成功','" + DateTime.Now + "','" + account.UserName + "','会签审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, "SupplyCnn");
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");


            #region 后期
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog 准出停止
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog 准出暂停
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog 准出淘汰
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货

            //if (intInsertBas == 1 && IsPass == "是")
            //{
            //    strAllBas = "update tk_SupplierBas set State = '4',wstate='0' where SID = '" + RelevanceID + "'";
            //}
            //else
            //{
            //    strAllBas = "update tk_SupplierBas set State = '6',wstate='0' where SID = '" + RelevanceID + "'";
            //} 
            #endregion
            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, "SupplyCnn");
            return intInsertBas + intAllBas;
        }
        /// <summary>
        /// 准出公司审批
        /// </summary>
        /// <param name="IsPass"></param>
        /// <param name="Opinion"></param>
        /// <param name="Remark"></param>
        /// <param name="PID"></param>
        /// <param name="webkey"></param>
        /// <param name="folderBack"></param>
        /// <param name="RelevanceID"></param>
        /// <param name="a_strErr"></param>
        /// <returns></returns>
        public static int UpdatezhunchuApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr, string job)
        {
            int intInsertBas = 0;
            int intUpdateBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            string strInsertBas = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');

            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/51/55/tk_PID/54/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货
            #region 根据职位判断
            if (IsPass == "是" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','1')";
            }
            else if (IsPass == "否" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-1')";
            }
            if (IsPass == "是" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','2')";
            }
            else if (IsPass == "否" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-2')";
            }
            if (IsPass == "是" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','3')";
            }
            else if (IsPass == "否" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-3')";
            }
            #endregion


            string strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, "SupplyCnn");
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");

            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog 准出停止
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog 准出暂停
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog 准出淘汰
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog 恢复供货

            string strAllBas = "";
            if (intInsertBas == 1 && IsPass == "是")
            {
                if (arr[4] == "23" && job == "董事长")
                    strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='3' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "26" && job == "董事长")
                    strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='2' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "29" && job == "董事长")
                    strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='4' where " + arr[3] + " = '" + RelevanceID + "'";
                else if (arr[4] == "10" && job == "董事长")
                {
                    strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else if (arr[4] == "4" && job == "董事长")
                {
                    strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='0' where " + arr[3] + " = '" + RelevanceID + "'";
                }
                else if (arr[4] == "62" && job == "董事长")
                {
                    strAllBas = "update tk_SupplierBas set nState = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
                }
            }
            else if (IsPass == "否" && (arr[4] == "29" || arr[4] == "23" || arr[4] == "26") && (job == "副总经理" || job == "总经理" || job == "董事长"))//公司级恢复供货未通过
            {
                strAllBas = "update tk_SupplierBas set State = '" + arr[7] + "',wstate='1' where SID = '" + RelevanceID + "'";
            }
            else
            {
                strAllBas = "update tk_SupplierBas set State = '" + arr[7] + "',wstate='2' where SID = '" + RelevanceID + "'";
            }

            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, "SupplyCnn");
            return intInsertBas + intAllBas;
        }
        public static int UpdatendApproval(string IsPass, string Opinion, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr, string job)
        {
            int intInsertBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            string strInsertBas = "";
            string strAllBas = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');

            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/29/tk_UserLog 年度
            if (IsPass == "是" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','1')";
            }
            else if (IsPass == "否" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-1')";
                strAllBas = "update tk_SupplierBas set nState = '" + arr[7] + "',wstate='4' where SID = '" + RelevanceID + "'";
            }
            if (IsPass == "是" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','2')";
            }
            else if (IsPass == "否" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-2')";
                strAllBas = "update tk_SupplierBas set nState = '" + arr[7] + "',wstate='4' where SID = '" + RelevanceID + "'";
            }
            if (IsPass == "是" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','3')";
                strAllBas = "update tk_SupplierBas set nState = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            else if (IsPass == "否" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-3')";
                strAllBas = "update tk_SupplierBas set nState = '" + arr[7] + "',wstate='4' where SID = '" + RelevanceID + "'";
            }
            string strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, "SupplyCnn");
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");

            #region 后续
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/62/61/tk_PID/64/tk_UserLog 年度

            //if (intInsertBas == 1 && IsPass == "是")
            //{
            //    strAllBas = "update tk_SupplierBas set nState = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
            //}
            //else
            //{
            //    strAllBas = "update tk_SupplierBas set nState = '" + arr[7] + "',wstate='4' where SID = '" + RelevanceID + "'";
            //} 
            #endregion

            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, "SupplyCnn");
            return intInsertBas + intAllBas;
        }
        public static int UpdatehfgApproval(string IsPass, string Opinion, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr, Tk_SupplierBas bas, string job)
        {
            int intInsertBas = 0;
            int intLog = 0;
            int intAllBas = 0;
            string strInsertBas = "";
            Acc_Account account = GAccount.GetAccountInfo();
            string UserId = account.UserID.ToString();
            string[] arr = folderBack.Split('/');
            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/51/tk_PID/56/tk_UserLog  恢复供应商
            if (IsPass == "是" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','1')";
            }
            else if (IsPass == "否" && job == "副总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-1')";

            }
            if (IsPass == "是" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','2')";
            }
            else if (IsPass == "否" && job == "总经理")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-2')";
            }
            if (IsPass == "是" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','3')";
            }
            else if (IsPass == "否" && job == "董事长")
            {
                strInsertBas = "INSERT INTO tk_SApproval (PID,RelevanceID,ApprovalContent,ApprovalPersons,Job,ApprovalMan,ApprovalTime,IsPass,Opinion,CreateUser,CreateTime,Validate,State)VALUES('" + PID + "','" + RelevanceID + "','" + webkey + "','" + UserId + "','" + account.Exjob + "','" + UserId + "','" + DateTime.Now + "','" + IsPass + "','" + Opinion + "','" + account.UserID + "','" + DateTime.Now + "','v','-3')";
            }

            string strInsertLog = "insert into tk_UserLog values ('" + RelevanceID + "','" + webkey + "审批操作','" + webkey + "审批操作成功','" + DateTime.Now + "','" + account.UserName + "','" + webkey + "审批')";
            if (strInsertBas != "")
                intInsertBas = SQLBase.ExecuteNonQuery(strInsertBas, "SupplyCnn");
            if (strInsertLog != "")
                intLog = SQLBase.ExecuteNonQuery(strInsertLog, "SupplyCnn");


            //BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/51/tk_PID/56/tk_UserLog  恢复供应商
            string strAllBas = "";
            if (intInsertBas == 1 && IsPass == "是" && bas.NState != 61 && job == "董事长")
            {
                strAllBas = "update tk_SupplierBas set State = '" + arr[4] + "',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            if (intInsertBas == 1 && IsPass == "否" && bas.NState != 61 && (job == "副总经理" || job == "总经理" || job == "董事长"))
            {
                strAllBas = "update tk_SupplierBas set State = '" + arr[7] + "',wstate='2' where SID = '" + RelevanceID + "'";
            }
            if (intInsertBas == 1 && IsPass == "是" && bas.NState == 61 && job == "董事长")
            {
                strAllBas = "update tk_SupplierBas set nState = '62',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
            }
            if (intInsertBas == 1 && IsPass == "否" && bas.NState == 61 && job == "董事长")
            {
                strAllBas = "update tk_SupplierBas set nState = '64',wstate='1' where " + arr[3] + " = '" + RelevanceID + "'";
            }

            if (strAllBas != "")
                intAllBas = SQLBase.ExecuteNonQuery(strAllBas, "SupplyCnn");
            return intInsertBas + intAllBas;
        }
        #endregion

        public static int InsertBiddingNew(tk_Award fileUp, HttpFileCollection Filedata, ref string a_strErr)
        {
            a_strErr = "";
            string savePaths = "";
            int intInsertFile = 0;
            string strLog = "";
            int intLog = 0;
            Acc_Account account = GAccount.GetAccountInfo();
            SQLTrans sqlTrans = new SQLTrans();
            sqlTrans.Open("SupplyCnn");
            //
            string FileName = "";
            string savePath = "";
            string filename = Path.GetFileName(Filedata[0].FileName);
            string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
            string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
            FileName = filename;
            //
            string path = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\";
            string infos = DateTime.Now.ToString("yyyyMM");
            savePaths = infos;
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

                    strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
                    strInsertFile += " values ('" + fileUp.SID + "','" + FileName + "','" + savePaths + "','"
                     + fileUp.AwardTime + "','" + fileUp.CreatUser + "','" + fileUp.Validate + "')";
                    intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                }
                else// 有同名文件 
                {
                    string strUpdate = "";
                    string strSel = " select count(*) from tk_Award where SID='" + fileUp.SID + "' and Award='" + FileName
                    + "' and AwardInfo='" + savePaths + "' and CreatUser='" + account.UserName + "'   and Validate='v' ";
                    int count = Convert.ToInt32(sqlTrans.ExecuteScalar(strSel));
                    if (count > 0)// 存在同一阶段同名的文件 则覆盖
                    {
                        savePath = Path.Combine(path, FileName);
                        Filedata[0].SaveAs(savePath);

                        strUpdate = " update tk_Award set Validate='i' where SID='" + fileUp.SID + "' and Award='" + FileName
                            + "' and AwardInfo='" + fileUp.AwardInfo + "' and Validate='v' ";
                        sqlTrans.ExecuteNonQuery(strUpdate);
                        strInsertFile = "insert into tk_Award (SID,Award,AwardInfo,AwardTime,CreatUser,Validate) ";
                        strInsertFile += " values ('" + fileUp.SID + "','" + FileName + "','" + savePaths + "','"
                            + fileUp.AwardTime + "','" + fileUp.CreatUser + "','" + fileUp.Validate + "')";
                        intInsertFile = sqlTrans.ExecuteNonQuery(strInsertFile);
                    }
                }
            }
            if (intInsertFile == 1)
            {
                strLog = "insert into tk_UserLog (UserId,LogTitle,LogContent,LogTime,LogPerson,Type) values('" + fileUp.SID + "','曾获奖项信息','上传成功','" + DateTime.Now + "','" + account.UserName + "','上传奖项')";
            }

            try
            {
                if (strLog != "")
                {
                    intLog = SQLBase.ExecuteNonQuery(strLog, "SupplyCnn");
                }
            }
            catch (SqlException e)
            {
                sqlTrans.Close(false);
                return -1;
            }

            return intInsertFile;
        }
        public static int judgeLoginUser(string userid, string webkey, string folderBack, string SPID, string job)
        {
            // 返回bol区别  -2审批未通过  -1- 不是审批人员 0-可以审批 1-已经审批 2-还没有到该人员审批
            int bol = 0;
            int State = 0;
            //string update = "";
            //int intupdate = 0;
            string[] arr = folderBack.Split('/');

            string str = "SELECT * FROM [" + arr[0] + "].." + arr[1] + " where PID='" + SPID + "' order BY PID desc ";//and ApprovalPersons='" + userid + "'

            DataTable dt = SQLBase.FillTable(str);
            if (dt.Rows.Count == 0 || dt == null)//默认没pid只能副总经理审批
            {
                bol = -1;
                return bol;
            }

            State = Convert.ToInt16(dt.Rows[0]["State"]);//获取状态
            if (State == 0)//副总经理
            {
                bol = 0;
                return bol;
            }

            if (State == 1)// 总经理
            {
                bol = 1;
                return bol;
            }
            if (State == 2)//董事长
            {
                bol = 2;
                return bol;
            }
            if (State == -1)
            {
                bol = -1;
                return bol;
            }
            if (State == -2)
            {
                bol = -1;
                return bol;
            }
            if (State == -3)
            {
                bol = -1;
                return bol;
            }
            //string str1 = "select count(*) from [" + arr[0] + "].." + arr[1] + " where PID = '" + SPID + "'  and state = '1'";
            //DataTable dt2 = SQLBase.FillTable(str1);
            //if (dt2.Rows.Count > 0)
            //    count = Convert.ToInt16(dt2.Rows[0][0]);
            return bol;
        }
        public static UIDataTable getCondition(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            string[] arr = folderBack.Split('/');
            UIDataTable instData = new UIDataTable();

            SqlParameter[] sqlPar = new SqlParameter[]
                {
                    new SqlParameter("@PageSize",a_intPageSize.ToString()),
                    new SqlParameter("@StartIndex",(a_intPageSize * a_intPageIndex).ToString()),
                    new SqlParameter("@Where",where)
                };

            DataSet DO_Order = SQLBase.FillDataSet("getCondition", CommandType.StoredProcedure, sqlPar, "SupplyCnn");
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
    }
}
