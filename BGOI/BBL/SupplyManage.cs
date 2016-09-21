using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
namespace TECOCITY_BGOI
{
    public class SupplyManage
    {
        private static SupplyMangeDAL sud;
        public SupplyManage()
        {
            sud = new SupplyMangeDAL();
        }

        public static string GetKID()
        {
            return SupplyMangeDAL.GetKID();
        }
        /// <summary>
        /// 页面加载是自动产生新的pid
        /// </summary>
        /// <returns></returns>
        public static string GetnShowPid()
        {
            return SupplyMangeDAL.GetPid();
        }
        public static string GetYid()
        {
            return SupplyMangeDAL.GetYeaID();
        }
        /// <summary>
        /// 往数据库插入数据的时候读取产生的新的pid
        /// </summary>
        /// <returns></returns>
        public static string GetShowPid()
        {
            return SupplyMangeDAL.GetNewPid();
        }

        public static string UpdateSID()
        {
            return SupplyMangeDAL.UpdateSID();
        }
        public static string UpdatenotSID()
        {
            return SupplyMangeDAL.UpdatenewnotSID();
        }
        public static List<SelectListItem> getNewKey(string data, string table)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DataTable dtDesc = SupplyMangeDAL.NewKey(data, table);
            if (dtDesc == null)
            {
                return list;
            }
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["State"].ToString();
                list.Add(SelListItem);
            }
            return list;
        }
        #region 从数据库中配置信息

        public static List<string> GetCode()
        {
            List<string> list = new List<string>();
            DataTable dt = SupplyMangeDAL.GetSupCode();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["suppliercode"].ToString());
            }
            return list;
        }
        public static List<string> GetSName()
        {
            List<string> list = new List<string>();
            DataTable dt = SupplyMangeDAL.GetSupNmae();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["comnamec"].ToString());
            }
            return list;
        }
        public static List<string> GetisnotName()
        {
            List<string> list = new List<string>();
            DataTable dt = SupplyMangeDAL.GetnotSupNmae();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["comnamec"].ToString());
            }
            return list;
        }
        public static List<string> GetZSCode()
        {
            List<string> list = new List<string>();
            DataTable dt = SupplyMangeDAL.GetZHENSHUCode();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["CCode"].ToString());
            }
            return list;
        }
        public static List<string> GetName()
        {
            List<string> list = new List<string>();
            DataTable dt = SupplyMangeDAL.GetCusName();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["cname"].ToString());
            }
            return list;
        }
        public static List<SelectListItem> GetConfigUnit()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetUnit();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["DeptId"].ToString();
                selectItem.Text = dt.Rows[i]["DeptName"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        /// <summary>
        /// 从配置表获得类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetConfigType(string type)
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetTypes(type);
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetConfigAppState()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetStateType();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> getFtype(string lever)
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetFType(lever);
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetProType()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetproType();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Text = dt.Rows[i]["text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetProCode()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetCode();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Text = dt.Rows[i]["PID"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetStand()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetProStand();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Text = dt.Rows[i]["Spec"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        /// <summary>
        /// 待处理供应商用此方法
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetState()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetType2();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetStateOK()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetTypeOK();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        /// <summary>
        /// 准入评审用到此方法
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetStateSP()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetType3();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Value = dt.Rows[i]["SID"].ToString();
                selectItem.Text = dt.Rows[i]["Text"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetProName()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetName();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                selectItem.Text = dt.Rows[i]["ProName"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;
        }
        public static List<SelectListItem> GetUnite()
        {
            List<SelectListItem> ListType = new List<SelectListItem>();
            DataTable dt = SupplyMangeDAL.GetProUnite();
            if (dt == null)
            {
                return ListType;
            }
            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "请选择";
            selectItem.Selected = true;
            ListType.Add(selectItem);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                selectItem = new SelectListItem();
                // selectItem.Value = dt.Rows[i]["ID"].ToString();
                selectItem.Text = dt.Rows[i]["units"].ToString();
                ListType.Add(selectItem);
            }
            return ListType;

        }
        public static List<SelectListItem> GetDept(string table, bool hasnull)
        {
            DataTable dt = SupplyMangeDAL.GetDept(table);
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            List<SelectListItem> items = new List<SelectListItem>();
            if (hasnull)
                items.Add(new SelectListItem { Text = "", Value = "" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new SelectListItem { Text = dt.Rows[i]["DeptName"].ToString(), Value = dt.Rows[i]["DeptID"].ToString() });
            }
            return items;
        }
        public static List<SelectListItem> GetConfig(string type, string table, bool hasnull)
        {
            DataTable dt = SupplyMangeDAL.GetSupConfig(type, table);
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            List<SelectListItem> items = new List<SelectListItem>();
            if (hasnull)
                items.Add(new SelectListItem { Text = "", Value = "" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new SelectListItem { Text = dt.Rows[i]["Text"].ToString(), Value = dt.Rows[i]["SID"].ToString() });
            }
            return items;
        }
        #endregion
        #region 展示数据
        public static UIDataTable getNewPSupplyGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getSupplyGride(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewCertificateGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getCertificateGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewProGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getProGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewServerGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getServerGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewAwardGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getAwardGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewPriceGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getAwardPrice(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewManageGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            return SupplyMangeDAL.getManageGrid(a_intPageSize, a_intPageIndex, where, order);
        }
        public static UIDataTable getNewManageokGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            return SupplyMangeDAL.getManageokGrid(a_intPageSize, a_intPageIndex, where, order);
        }
        public static UIDataTable getNewYearGrid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            return SupplyMangeDAL.getYearGride(a_intPageSize, a_intPageIndex, where, order);
        }
        public static UIDataTable getScoreGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getScoreGride(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getYScoreGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getnewScoreGride(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewsAwardGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getAwardssGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewsPriceGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getPricessGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewCustomerGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getCustomerGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewisnotsuplyGrid(int a_intPageSize, int a_intPageIndex,string where)
        {
            return SupplyMangeDAL.getisnotsuplyGrid(a_intPageSize, a_intPageIndex,where);
        }
        public static UIDataTable getNewContractPersonGrid(int a_intPageSize, int a_intPageIndex, string where, string kid)
        {
            return SupplyMangeDAL.getContractPersoGrid(a_intPageSize, a_intPageIndex, where, kid);
        }
        public static UIDataTable getNewShareGrid(int a_intPageSize, int a_intPageIndex, string where, string kid, string isshare)
        {
            return SupplyMangeDAL.getShareGrid(a_intPageSize, a_intPageIndex, where, kid, isshare);
        }
        public static UIDataTable getNewWeiguirid(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            return SupplyMangeDAL.getWeiguiGrid(a_intPageSize, a_intPageIndex, where, order);
        }
        public static UIDataTable getNewManageGridSP(int a_intPageSize, int a_intPageIndex, string where, string order)
        {
            return SupplyMangeDAL.getManageGridSP(a_intPageSize, a_intPageIndex, where, order);
        }
        public static UIDataTable getNewApprovalGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.getApprovalGrid(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewManageProGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getManageProGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewManageSerGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getManageSerGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getSPRecord(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getSPR(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getSRecord(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getSR(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewPlanProGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getPlanProSerGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getNewPlanSerGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getPlanSerGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getLogGrid(int a_intPageSize, int a_intPageIndex, string where, string sid, string kid)
        {
            return SupplyMangeDAL.GetLog(a_intPageSize, a_intPageIndex, where, sid, kid);
        }
        public static UIDataTable getcontentGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getConditonGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getSPGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getSP(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getBRGrid(int a_intPageSize, int a_intPageIndex, string sid)
        {
            return SupplyMangeDAL.GetBR(a_intPageSize, a_intPageIndex, sid);
        }
        public static UIDataTable getRecordShow(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.GetDealRecord(a_intPageSize, a_intPageIndex, where, sid);
        }
        public static UIDataTable getMainCertificateGrid(int a_intPageSize, int a_intPageIndex, string where, string sid)
        {
            return SupplyMangeDAL.getMainCertificateGrid(a_intPageSize, a_intPageIndex, where, sid);
        }
        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNewCon(string type)
        {
            return SupplyMangeDAL.GetNewCon(type);
        }

        public static DataTable GetDetailID(string sid)
        {
            return SupplyMangeDAL.GetNewDetailID(sid);
        }
        public static DataTable GetSugestion(string sid)
        {
            return SupplyMangeDAL.GetNewDetailSugestion(sid);
        }
        public static DataTable GetApproval2(string sid)
        {
            return SupplyMangeDAL.GetNewDetailApproval(sid);
        }
        /// <summary>
        /// 获取代理产品级别
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable getAgenclass(string sid)
        {
            return SupplyMangeDAL.GetNewClass(sid);
        }
        /// <summary>
        /// 获取质量执行标准
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable getReation(string sid)
        {
            return SupplyMangeDAL.GetNewQuality(sid);
        }
        /// <summary>
        /// 获取经营产品分类
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable getScaleType(string sid)
        {
            return SupplyMangeDAL.GetNewScaleType(sid);
        }
        public static DataTable getQualityStandard(string sid)
        {
            return SupplyMangeDAL.GetNewQualityStandard(sid);
        }
        public static DataTable getBusinessDistribute(string sid)
        {
            return SupplyMangeDAL.GetnewgetBusinessDistribute(sid);
        }
        public static DataTable getbillWay(string sid)
        {
            return SupplyMangeDAL.GetNewBillWay(sid);
        }
        public static DataTable getBillHanzi()
        {
            return SupplyMangeDAL.getNewBillHan();
        }
        public static DataTable getBuinessHanzi()
        {
            return SupplyMangeDAL.getNewBuinessHan();
        }
        public static DataTable getFilename(string sid)
        {
            return SupplyMangeDAL.getmfile(sid);
        }
        public static DataTable getCertifyname(string sid)
        {
            return SupplyMangeDAL.getCertifyname(sid);
        }
        public static DataTable getAward(string sid)
        {
            return SupplyMangeDAL.getAward(sid);
        }
        public static DataTable getpricename(string sid)
        {
            return SupplyMangeDAL.getpricenam(sid);
        }

        public static DataTable GetUnite(string kid)
        {
            return SupplyMangeDAL.GetNewUnit(kid);
        }
        public static DataTable GetUnites(string sid)
        {
            return SupplyMangeDAL.getNewUnites(sid);
        }
        public static DataTable GetDetailMsg(string sid)
        {
            return SupplyMangeDAL.GetNewDetailMsg(sid);
        }
        public static DataTable getNewDetailApp(string where)
        {
            return SupplyMangeDAL.getDetail(where);
        }
        public static DataTable GetDetailCustometerID(string kid)
        {
            return SupplyMangeDAL.GetNewDetailCustomerID(kid);
        }
        public static DataTable GetDetailConPerson(string sid)
        {
            return SupplyMangeDAL.getDetailPerson(sid);
        }
        public static DataTable getProduct(string sid)
        {
            return SupplyMangeDAL.getProduct(sid);
        }
        public static DataTable getnewServer(string sid)
        {
            return SupplyMangeDAL.getnewServer(sid);
        }
        public static DataTable getLog(string sid)
        {
            return SupplyMangeDAL.GetLogNum(sid);
        }
        #endregion
        #region 添加数据
        public static bool CreateFileInfo(tk_SFileInfo filems, ref string strErr)
        {
            if (SupplyMangeDAL.CreateFiles(filems, ref strErr) == 1)
                return true;
            else return false;
        }
        public static bool CreateCertifi(tk_SCertificate certifi, ref string Err)
        {
            if (SupplyMangeDAL.CreateCertifi(certifi, ref Err) == 1)
                return true;
            else return false;
        }
        public static bool CreatePro(tk_SProducts pro, ref string Err)
        {
            if (SupplyMangeDAL.CreatePro(pro, ref Err) == 1)
                return true;
            else return false;
        }
        public static bool CreateServer(tk_SService server, ref string Err)
        {
            if (SupplyMangeDAL.CreateServer(server, ref Err) == 1)
                return true;
            else return false;
        }
        public static bool InsertSupplyBas(Tk_SupplierBas supplyBas, List<Tk_SContactPerson> listPer, ref string strErr)
        {
            return SupplyMangeDAL.InsertSupplyBas(supplyBas, listPer, ref strErr);
        }
        public static bool InsertNewDetail(tk_SProcessInfo process, ref string Err)
        {
            return SupplyMangeDAL.InsertDetail(process, ref Err);
        }
        public static bool InsertNewfzr(tk_SProcessInfo process, ref string Err)
        {
            return SupplyMangeDAL.InsertFZR(process, ref Err);
        }
        public static bool InsertNewBumen(tk_SProcessInfo process, ref string Err)
        {
            return SupplyMangeDAL.InsertBM(process, ref Err);
        }
        public static bool InsertBackSupbumen(tk_SProcessInfo info, ref string Err)
        {
            return SupplyMangeDAL.Insertbumen(info, ref Err);
        }
        public static bool UPrecover(tk_SProcessInfo proinfo, ref string ERR)
        {
            return SupplyMangeDAL.UPrecover(proinfo, ref ERR);
        }
        public static bool InsertNewRes(tk_SProcessInfo process, ref string Err)
        {
            return SupplyMangeDAL.InsertYearRes(process, ref Err);
        }
        public static bool InsertNewContract(Tk_SContactPerson conPer, string Press, string CSize, string length, ref string a_Err)
        {
            if (SupplyMangeDAL.InsertContract(conPer, Press, CSize, length, ref a_Err) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertFileMsg(tk_SFileInfo fileInfo, ref string Err, HttpFileCollection filc)
        {
            return SupplyMangeDAL.InsertFile(fileInfo, ref Err, filc);
        }
        public static bool UpdateFileMsg(tk_SFileInfo sfi, byte[] fileByte, ref string Err)
        {
            return SupplyMangeDAL.UpdateFile(sfi, fileByte, ref Err);
        }
        public static bool UpdateRearkFileMsg(tk_SFileInfo sfi, byte[] fileByte, ref string Err)
        {
            return SupplyMangeDAL.RemarkFile(sfi, fileByte, ref Err);
        }
        public static bool UpdateCertityMsg(tk_SCertificate scfi, byte[] filebyte, ref string Err)
        {
            return SupplyMangeDAL.UpdateCertityMsg(scfi, filebyte, ref Err);
        }
        /// <summary>
        /// 更新部门内部审批
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="sf"></param>
        /// <param name="sid"></param>
        /// <param name="fileByte"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool UpdateNewApprovalBas(Tk_SupplierBas bas, HttpFileCollection filc, string sid, ref string Err)
        {
            if (SupplyMangeDAL.UpdateNewApproval(bas, filc, sid, ref Err))
                return true;
            else
                return false;

        }
        public static bool AddSugestion(tk_SUPSugestion sgs, string sid, ref string Err)
        {
            if (SupplyMangeDAL.AddFZSugestion(sgs, sid, ref Err))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool AddBackSugestion(tk_SUPSugestion sgs, string sid, ref string Err)
        {
            if (SupplyMangeDAL.AddBSSugestion(sgs, sid, ref Err))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool AddYearDeal(tk_SYRDetail yearDetal, string sid, Tk_SupplierBas bas, ref string Err)
        {
            return SupplyMangeDAL.YearDeal(yearDetal, sid, bas, ref Err);

        }
        public static bool InsertCertifiMsg(tk_SCertificate certifi, byte[] fileByte, ref string Err)
        {
            return SupplyMangeDAL.InsertCertifi(certifi, fileByte, ref Err);
        }
        public static bool InsertProMsg(tk_SProducts product, byte[] fileByte, ref string Err)
        {
            return SupplyMangeDAL.InsertPro(product, fileByte, ref Err);
        }
        public static bool FeedBack(tk_FeedBack fb, ref string Err, Tk_SupplierBas bas)
        {
            return SupplyMangeDAL.SaveFeedBack(fb, ref Err, bas);
        }
        public static bool ResApp(Tk_SupplierBas bas, string sid, ref string Err)
        {
            return SupplyMangeDAL.ResRe(bas, sid, ref Err);
        }
        /// <summary>
        /// 有问题
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        public static bool InsertNewApproval(tk_SApproval approval, string sid, ref string Err)
        {
            if (SupplyMangeDAL.InsertApprval(approval, sid, ref Err) >= 2)
                return true;
            else
                return false;
        }
        public static bool UpApproval(tk_SApproval approval, string pid, ref string Err)
        {
            //if (SupplyMangeDAL.UpNewApp(approval, pid, ref Err) >= 2)
            //    return true;
            //else
            //    return false;
            return SupplyMangeDAL.UpNewApp(approval, pid, ref Err);
        }
        public static bool UpWeiguiApproval(tk_SProcessInfo processinfo, string sid, ref string Err)
        {
            return SupplyMangeDAL.UpNewWeiguiApproval(processinfo, sid, ref Err);
        }
        public static bool InsertServerMsg(tk_SService server, byte[] fileByte, ref string Err)
        {
            return SupplyMangeDAL.InsertServer(server, fileByte, ref Err);
        }
        public static bool InsertAwardMsg(tk_Award bas, HttpFileCollection fileByte, ref string Err)
        {
            //if (SupplyMangeDAL.InsertAward(bas, fileByte, ref Err) > 0)
            //    return true;
            //else
            //    return false;
            return SupplyMangeDAL.InsertAward(bas, fileByte, ref Err);
        }
        public static bool InsertPriceMsg(tk_PriceUp bas, HttpFileCollection fileByte, ref string Err)
        {
            return SupplyMangeDAL.InsertPrice(bas, fileByte, ref Err);
        }
        public static bool InsertCustomer(tk_KClientBas cbs, ref string Err)
        {
            return SupplyMangeDAL.AddCustome(cbs, ref Err);
        }
        public static bool Insertisnosuply(tk_IsNotSupplierBas cbs, ref string Err)
        {
            return SupplyMangeDAL.Addisno(cbs, ref Err);
        }
        public static bool InsertPerson(tk_KContactPerson kcp, ref string Err)
        {
            return SupplyMangeDAL.AddPersons(kcp, ref Err);
        }
        public static bool InsertUnites(tk_KClientBas kuwc, ref string Err)
        {
            return SupplyMangeDAL.AddUnite(kuwc, ref Err);
        }
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="bas"></param>
        /// <param name="a_strErr"></param>
        /// <returns></returns>
        public static bool UpdateNewPro(string sid, tk_SProducts bas, ref string a_strErr)
        {
            if (SupplyMangeDAL.UpdatePro(sid, bas, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        #endregion

        public static Tk_SupplierBas GetUpdateBas(string sid)
        {
            return SupplyMangeDAL.UpdateBas(sid);
        }
        public static tk_SupplierBasHis getMsg(string sid)
        {
            return SupplyMangeDAL.getBAS(sid);
        }
        public static Tk_SContactPerson GetPS(string sid)
        {
            return SupplyMangeDAL.GetPS(sid);
        }
        public static Tk_SupplierBas getInfo(string sid)
        {
            return SupplyMangeDAL.ShowInfo(sid);
        }
        public static Tk_SupplierBas getResInfo(string name, string type, string area, string state, string sid)
        {
            return SupplyMangeDAL.ShowResInfo(name, type, area, state, sid);
        }
        public static Tk_SupplierBas getOkSp(string sid)
        {
            return SupplyMangeDAL.ShowOKInfo(sid);
        }
        public static Tk_SupplierBas getPrintYear(string sid)
        {
            return SupplyMangeDAL.ShowYear(sid);
        }
        public static tk_SYRDetail getDetails(string sid)
        {
            return SupplyMangeDAL.getNewDetail(sid);
        }
        public static Tk_SupplierBas getBAS(string sid)
        {
            return SupplyMangeDAL.getNewBAS(sid);
        }
        public static tk_SYRDetail getScore(string sid)
        {
            return SupplyMangeDAL.getNewsocre(sid);
        }
        public static tk_SYRDetail getPrintScore(string sid)
        {
            return SupplyMangeDAL.ShowScore(sid);
        }
        public static tk_KClientBas getUPCbs(string kid)
        {
            return SupplyMangeDAL.UpdateCus(kid);
        }
        public static tk_IsNotSupplierBas getNewSuplycontent(string sid)
        {
            return SupplyMangeDAL.updateIsnot(sid);
        }
        public static tk_KContactPerson getUpPeson(string kid, DateTime time)
        {
            return SupplyMangeDAL.Persons(kid, time);
        }
        public static tk_SProducts getProduct(string sid, string id, DateTime time, string filename)
        {
            return SupplyMangeDAL.Product(sid, id, time, filename);
        }
        public static tk_SService getServer(string sid, string id, string filename)
        {
            return SupplyMangeDAL.Server(sid, id, filename);
        }
        public static tk_SFileInfo getFile(string sid, string fid, DateTime time, string filename)
        {
            return SupplyMangeDAL.FileInfo(sid, fid, time, filename);
        }
        public static tk_SFileInfo GetNewFile(string sid, string fid)
        {
            return SupplyMangeDAL.GetReark(sid, fid);
        }
        public static tk_SCertificate getCertify(string sid, string fid, DateTime time, string filename)
        {
            return SupplyMangeDAL.Certify(sid, fid, time, filename);
        }
        public static tk_SCertificate getNewRemarkCertify(string sid)
        {
            return SupplyMangeDAL.RemarkCertify(sid);
        }
        public static tk_KClientBas getUpUnit(string kid)
        {
            return SupplyMangeDAL.UpUnite(kid);
        }
        public static Tk_SupplierBas Approver(string sid)
        {
            return SupplyMangeDAL.ApproverInfo(sid);
        }
        public static tk_SProcessInfo getProceinfo(string sid)
        {
            return SupplyMangeDAL.getNewInfo(sid);
        }
        public static tk_SUPSugestion GetSuge(string sid)
        {
            return SupplyMangeDAL.getNewsuges(sid);
        }
        public static tk_SFileInfo getmfilename(string sid)
        {
            return SupplyMangeDAL.getNewfileInfo(sid);
        }
        public static tk_PriceUp getbijiao(string sid)
        {
            return SupplyMangeDAL.getNewprice(sid);
        }
        public static tk_SApproval getapp(string sid)
        {
            return SupplyMangeDAL.getApprol(sid);
        }

        #region 删除操作

        public static bool deleteNewPro(string sid, string id, ref string Err)
        {
            return SupplyMangeDAL.deletePro(sid, id, ref Err);
        }
        public static bool deleteNewprice(string sid, string id, ref string Err)
        {
            return SupplyMangeDAL.deleteprice(sid, id, ref Err);
        }
        public static bool deleteNewaward(string sid, string id, ref string Err)
        {
            return SupplyMangeDAL.deleteaward(sid, id, ref Err);
        }
        public static bool CancelSup(string sid, ref string Err)
        {
            return SupplyMangeDAL.CancelSp(sid, ref Err);
        }
        public static bool RestSup(string sid, ref string Err)
        {
            return SupplyMangeDAL.RESTSUP(sid, ref Err);
        }
        public static bool deleteNewServer(string sid, DateTime time, ref string Err)
        {
            return SupplyMangeDAL.DeleteServer(sid, time, ref Err);
        }
        public static bool deleteNewFile(string sid, DateTime time, ref string Err)
        {
            return SupplyMangeDAL.DeleteFile(sid, time, ref Err);
        }
        public static bool deleteNewCertificate(string sid, DateTime time, ref string Err)
        {
            return SupplyMangeDAL.DeleteCerticify(sid, time, ref Err);
        }
        public static bool deleteNewPerson(string kid, ref string Err)
        {
            return SupplyMangeDAL.deletePersons(kid, ref Err);
        }
        public static bool deleteNewUnit(string kid, ref string Err)
        {
            return SupplyMangeDAL.deleteUnit(kid, ref Err);
        }
        public static bool deleteNewCus(string kid, ref string Err)
        {
            return SupplyMangeDAL.deleteCus(kid, ref Err);
        }
        public static bool deleteisnotsuply(string sid, ref string Err)
        {
            return SupplyMangeDAL.deleteisnotsuply(sid, ref Err);
        }
        #endregion

        #region 更新操作

        public static bool UpdateNewBas(Tk_SupplierBas bas, List<Tk_SContactPerson> listper, ref string Err)
        {
            return SupplyMangeDAL.UpdateBasinfo(bas, listper, ref Err);
        }
        public static bool UpdateNewCus(tk_KClientBas cbas, ref string Err)
        {
            return SupplyMangeDAL.UpdateCus(cbas, ref Err);
        }
        public static bool UpdateNewok(tk_IsNotSupplierBas cbas, ref string Err)
        {
            return SupplyMangeDAL.Updateisok(cbas, ref Err);
        }
        public static bool UpdateNewMan(tk_KContactPerson kcp, ref string Err)
        {
            return SupplyMangeDAL.UpdateMans(kcp, ref Err);
        }
        public static bool UpdateNewPro(tk_SProducts sp, byte[] Filebyte, ref string Err)
        {
            return SupplyMangeDAL.UpdateProDuct(sp, Filebyte, ref Err);
        }
        public static bool UpdateNewServer(tk_SService sse, byte[] filebyte, ref string Err)
        {
            return SupplyMangeDAL.UpdateServer(sse, filebyte, ref Err);
        }
        public static bool UpdateNewUnite(tk_KClientBas kuwc, ref string Err)
        {
            return SupplyMangeDAL.UpdateUinte(kuwc, ref Err);
        }
        #endregion
        public static DataTable CustomerToExcel(string where)
        {
            DataTable dt = SupplyMangeDAL.CusToExcel(where);
            if (dt == null)
                return null;
            else
                return dt;
        }
        public static DataTable SPOKToExcel(string where)
        {
            DataTable dt = SupplyMangeDAL.GetOutExcel(where);
            if (dt == null)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        public static DataTable SPYearExcel(string where)
        {
            DataTable dt = SupplyMangeDAL.GetPrintExcel(where);
            if (dt == null)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        public static tk_SProcessInfo getReport(string info)
        {
            return SupplyMangeDAL.getReport(info);
        }
        public static string getRecordListUT(string commc, string pid, string sid, string supplyCode, ref string Err)
        {
            DataTable dt = new DataTable();
            dt = SupplyMangeDAL.getRecordListUT(commc, pid, sid, supplyCode, ref Err);
            if (dt == null)
            {
                return "";
            }
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            string strreport = GFun.Dt2Json("RecordListUT", dt);
            return strreport;
        }
        public static DataTable getperson(string sid)
        {
            return SupplyMangeDAL.getPerson(sid);
        }

        public static DataTable getProcess(string sid)
        {
            return SupplyMangeDAL.getProcessINfo(sid);
        }
        public static DataTable getOK(string sid)
        {
            return SupplyMangeDAL.getSPOK(sid);
        }
        public static DataTable getOut(string sid, string type, string name, string area, string state)
        {
            return SupplyMangeDAL.getOutPrint(sid, type, name, area, state);
        }
        public static DataTable getYear(string sid)
        {
            return SupplyMangeDAL.getYearRes(sid);
        }
        public static DataTable getDetail(string sid)
        {
            return SupplyMangeDAL.getDetailProcess(sid);
        }
        public static DataTable getYReview(string sid, string yid)
        {
            return SupplyMangeDAL.getYD(sid, yid);
        }
        public static string GetSelDesc(string strSelThird)
        {
            DataTable dtInfo = SupplyMangeDAL.GetSelDesc(strSelThird);
            if (dtInfo == null)
                return "";
            if (dtInfo.Rows.Count <= 0)
                return "";

            string str = "";
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                str += dtInfo.Rows[i][0].ToString() + "?";
            }
            string strJson = str;
            return strJson;
        }
        public static string GetStand(string stand)
        {
            DataTable dtInfo = SupplyMangeDAL.GetStand(stand);
            if (dtInfo == null)
                return "";
            if (dtInfo.Rows.Count <= 0)
                return "";

            string str = "";
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                str += dtInfo.Rows[i][0].ToString() + "?";
            }
            string strJson = str;
            return strJson;
        }
        public static string GetStand2(string stand)
        {
            DataTable dtInfo = SupplyMangeDAL.GetStand3(stand);
            if (dtInfo == null)
                return "";
            if (dtInfo.Rows.Count <= 0)
                return "";

            string str = "";
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                str += dtInfo.Rows[i][0].ToString() + "?";
            }
            string strJson = str;
            return strJson;
        }
        public static string GetPrices(string propname)
        {
            DataTable dtInfo = SupplyMangeDAL.GetPce(propname);
            if (dtInfo == null)
                return "";
            if (dtInfo.Rows.Count <= 0)
                return "";

            string str = "";
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                str += dtInfo.Rows[i][0].ToString() + "?";
            }
            string strJson = str;
            return strJson;
        }
        public static string getDescLink(string strDesc, string strThirdType, ref string a_strErr)
        {
            string strDescLink = SupplyMangeDAL.getDescLink(strDesc, strThirdType, ref a_strErr);
            if (strDescLink == "")
                return "";
            else
                return strDescLink;
        }
        public static string getPro(string stand, string strThirdType, ref string a_strErr)
        {
            string strDescLink = SupplyMangeDAL.getStand(stand, strThirdType, ref a_strErr);
            if (strDescLink == "")
                return "";
            else
                return strDescLink;
        }
        public static string getProID(string proid, string Stand, ref string a_strErr)
        {
            string strDescLink = SupplyMangeDAL.getPro(proid, Stand, ref a_strErr);
            if (strDescLink == "")
                return "";
            else
                return strDescLink;
        }
        public static string getprice(string price, string strThirdType, ref string a_strErr)
        {
            string strDescLink = SupplyMangeDAL.getPrice(price, strThirdType, ref a_strErr);
            if (strDescLink == "")
                return "";
            else
                return strDescLink;
        }
        public static string GetSeond(string strFirst)
        {
            DataTable dtInfo = SupplyMangeDAL.GetSubType(strFirst);
            if (dtInfo == null) return "";
            if (dtInfo.Rows.Count == 0) return "";

            string strJson = GFun.Dt2Json("SubType", dtInfo);
            return strJson;
        }
        public static string GetThread(string strSecond)
        {
            DataTable dtInfo = SupplyMangeDAL.GetItem(strSecond);
            if (dtInfo == null) return "";
            if (dtInfo.Rows.Count == 0) return "";

            string strJson = GFun.Dt2Json("SubType", dtInfo);
            return strJson;
        }
        public static string GetCompare(string strfirst)
        {
            DataTable dtInfo = SupplyMangeDAL.GetCompare(strfirst);
            if (dtInfo == null) return "";
            if (dtInfo.Rows.Count == 0) return "";

            string strJson = GFun.Dt2Json("SubType", dtInfo);
            return strJson;
        }
        public static UIDataTable getNewApprovalUserGrid(int a_intPageSize, int a_intPageIndex, string where)
        {
            return SupplyMangeDAL.GetApprovalUser(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable getNewZSGrid(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            return SupplyMangeDAL.GetApproval(a_intPageSize, a_intPageIndex, where, unitid);
        }
        public static UIDataTable getNewTimeOutGrid(int a_intPageSize, int a_intPageIndex, string where, string unitid)
        {
            return SupplyMangeDAL.GetApprovalWarn(a_intPageSize, a_intPageIndex, where, unitid);
        }
        /// <summary>
        /// 资质过期
        /// </summary>
        /// <returns></returns>
        public static string getZZTimeOut()
        {
            return SupplyMangeDAL.GetNewTimeOut();
        }
        /// <summary>
        /// 证书到期
        /// </summary>
        /// <returns></returns>
        public static string getZSTimeOut()
        {
            return SupplyMangeDAL.GetNewZSTimeOut();
        }
        public static DataTable GetJob(string unitid, string userName, string userId)
        {
            return SupplyMangeDAL.GetJob(unitid, userName, userId);
        }
        public static DataTable Getbas(string unitid, string userName, string userId)
        {
            return SupplyMangeDAL.GetnewBas(unitid, userName, userId);
        }
        public static DataTable GetNewDownLoad(string sid, string fid, string filename)
        {
            return SupplyMangeDAL.GetDownload(sid, fid, filename);
        }
        public static DataTable GetNewpriceDownLoad(string sid, string name)
        {
            return SupplyMangeDAL.GetpriceDownload(sid, name);
        }
        public static DataTable GetNewDownLoadUnit(string sid)
        {
            return SupplyMangeDAL.getLoadUnit(sid);
        }
        public static DataTable GetNewDownLoadProduct(string id, string timeout, string filename)
        {
            return SupplyMangeDAL.GetDownLoadProduct(id, timeout, filename);
        }
        public static DataTable GetNewDownLoadServer(string sid, string id, string filename)
        {
            return SupplyMangeDAL.GetDownLoadServer(sid, id, filename);
        }
        public static DataTable GetNewDownLoad1(string sid, string fid, string filename)
        {
            return SupplyMangeDAL.GetDownload1(sid, fid, filename);
        }
        public static DataTable GetNewDownloadFile(string id)
        {
            return SupplyMangeDAL.GetDownloadFile(id);
        }
        public static DataTable GetNewDownload(string fid)
        {
            return SupplyMangeDAL.GetDownload(fid);
        }
        public static DataTable GetNewDownloadProduct(string id)
        {
            return SupplyMangeDAL.GetDownloadProduct(id);
        }
        public static DataTable GetNewDownloadAward(string id)
        {
            return SupplyMangeDAL.GetDownloadAward(id);
        }
        public static DataTable GetNewDownloadPrice(string id)
        {
            return SupplyMangeDAL.GetDownloadPriceNew(id);
        }
        public static DataTable GetNewDownloadAPro(string id)
        {
            return SupplyMangeDAL.GetDownloadPro(id);
        }
        public static DataTable GetManDownloadServer(string id)
        {
            return SupplyMangeDAL.GetNewDownloadServer(id);
        }
        public static DataTable GetManDownloadFile(string id)
        {
            return SupplyMangeDAL.GetNewDownloadFiles(id);
        }
        public static DataTable GetManDownloadCerty(string id)
        {
            return SupplyMangeDAL.GetNewDownloadZhenshu(id);
        }
        public static DataTable GetNewDownloadServer(string id)
        {
            return SupplyMangeDAL.GetDownloadServer(id);
        }
        public static DataTable GetNewDownloadcerty(string id)
        {
            return SupplyMangeDAL.GetDownloadCerty(id);
        }
        public static bool DellNewFile(string ID, ref string a_strErr)
        {
            if (SupplyMangeDAL.DeleteFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static bool DellNewMFile(string ID, ref string a_strErr)
        {
            if (SupplyMangeDAL.DeleteMFile(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static bool DellNewProduct(string id, ref string Err)
        {
            if (SupplyMangeDAL.DeleteProduct(id, ref Err) >= 1)
                return true;
            else
                return false;
        }
        public static bool DellNewServer(string id, ref string Err)
        {
            if (SupplyMangeDAL.DeleteServer(id, ref Err) >= 1)
                return true;
            else
                return false;
        }
        public static bool DellNewCerty(string ID, ref string a_strErr)
        {
            if (SupplyMangeDAL.DeleteCerty(ID, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }
        public static DataTable getNewAppType(string type)
        {
            return SupplyMangeDAL.getConfigType(type);
        }
        public static string getNewHaveExaminContent(string butype)
        {
            return SupplyMangeDAL.getHavetype(butype);
        }

        public static bool InsertNewApprovals(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SProcessInfo info)
        {
            if (SupplyMangeDAL.InsertApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr, info) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewtaotaiApprovals(string RelevanceID, ref string a_strErr, tk_SProcessInfo info)
        {
            if (SupplyMangeDAL.InserttaotaiApproval(RelevanceID, ref a_strErr, info) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewzhunchuApprovals(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SProcessInfo info)
        {
            if (SupplyMangeDAL.InsertzhunchuApproval(PID, RelevanceID, webkey, folderBack, ref a_strErr, info) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewhuifuApprovals(string RelevanceID, ref string a_strErr, tk_SProcessInfo info)
        {
            if (SupplyMangeDAL.InserthuifuApproval(RelevanceID, ref a_strErr, info) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewzhunru(string PID, string RelevanceID, string webkey, string folderBack, ref string a_strErr, tk_SUPSugestion sgs)
        {
            if (SupplyMangeDAL.Insertzhunrul(PID, RelevanceID, webkey, folderBack, ref a_strErr, sgs) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewzhunru1(ref string a_strErr, tk_SUPSugestion sgs)
        {
            if (SupplyMangeDAL.Insertzhunrul1(ref a_strErr, sgs) >= 1)
                return true;
            else
                return false;
        }
        public static bool InsertNewrecover(string RelevanceID, ref string a_strErr, tk_SUPSugestion sgs, Tk_SupplierBas bas)
        {
            if (SupplyMangeDAL.Insertrecover(RelevanceID, ref a_strErr, sgs, bas) >= 1)
                return true;
            else
                return false;
        }
        #region 自由审批
        public static bool UpdateNewApproval(string IsPass, string Opinion, string PID, string RelevanceID, ref string a_strErr, string job)
        {
            if (SupplyMangeDAL.UpdateApproval(IsPass, Opinion, PID, RelevanceID, ref a_strErr, job) >= 1)
                return true;
            else
                return false;
        }
        public static bool UpdateNewzhunchuApproval(string IsPass, string Opinion, string Remark, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr,string job)
        {
            if (SupplyMangeDAL.UpdatezhunchuApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref a_strErr,job) >= 1)
                return true;
            else
                return false;
        }
        public static bool UpdateNewndApproval(string IsPass, string Opinion, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr,string job)
        {
            if (SupplyMangeDAL.UpdatendApproval(IsPass, Opinion, PID, webkey, folderBack, RelevanceID, ref a_strErr,job) >= 1)
                return true;
            else
                return false;
        }
        public static bool UpdateNewhfgApproval(string IsPass, string Opinion, string PID, string webkey, string folderBack, string RelevanceID, ref string a_strErr, Tk_SupplierBas bas,string job)
        {
            if (SupplyMangeDAL.UpdatehfgApproval(IsPass, Opinion, PID, webkey, folderBack, RelevanceID, ref a_strErr, bas,job) >= 1)
                return true;
            else
                return false;
        }
        #endregion

        public static bool InsertBiddingNew(tk_Award fileUp, HttpFileCollection Filedata, ref string strErr)
        {
            if (SupplyMangeDAL.InsertBiddingNew(fileUp, Filedata, ref strErr) > 0)
                return true;
            else
                return false;
        }
        public static int JudgeNewLoginUser(string userid, string webkey, string folderBack, string SPID,string job)
        {
            return SupplyMangeDAL.judgeLoginUser(userid, webkey, folderBack, SPID,job);
        }
        public static UIDataTable getNewCondition(int a_intPageSize, int a_intPageIndex, string where, string folderBack)
        {
            return SupplyMangeDAL.getCondition(a_intPageSize, a_intPageIndex, where, folderBack);
        }
    }
}
