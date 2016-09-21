using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TECOCITY_BGOI;
//using System.Runtime.InteropServices;

namespace TECOCITY_BGOI.Controllers
{
    [AuthorizationAttribute]
    public class SuppliesManageController : Controller
    {
        //
        // GET: /SuppliesManage/

        public ActionResult Index()
        {
            return View();
        }

        #region 主视图
        /// <summary>
        /// 供应商基本信息主视图
        /// </summary>
        /// <param name="oldsid"></param>
        /// <returns></returns>
        public ActionResult SuppliesPrepare()
        {
            Tk_SupplierBas Bas = new Tk_SupplierBas();
            Bas.Sid = SupplyManage.UpdateSID();
            string strUnit = GAccount.GetAccountInfo().UnitName;
            ViewBag.unit = strUnit;
            //ViewBag.Time = DateTime.Now.ToString("yyyy-MM-dd");
            #region 动态字段生成
            List<SelectListItem> items = new List<SelectListItem>();
            //业务分布
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("BusinessDistribute", "tk_BusinessDistribute", false);
            this.ViewBag.BusinessDistribute = items;
            ////开票方式
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("BillingWay", "tk_ConfigBillWay", false);
            this.ViewBag.BillingWay = items;
            ////供需关系
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("Relation", "tk_ConfigReation", false);
            this.ViewBag.Relation = items;
            ////按供应商规模和经营品种分类
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("ScaleType", "tk_ConfigScalType", false);
            this.ViewBag.ScaleType = items;
            ////产品质量执行标准
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("CPZLZXBZ", "tk_ConfigQualityStandard", false);
            this.ViewBag.QualityStandard = items;
            ////代理产品国内所属级别
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("AgentClass", "tk_ConfigAgentClass", false);
            this.ViewBag.AgentClass = items;
            #endregion
            return View(Bas);
        }
        /// <summary>
        /// 采购库存使用的非合格供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult IsnotSuppliesPrepare()
        {
            tk_IsNotSupplierBas nobas = new tk_IsNotSupplierBas();
            nobas.SID = SupplyManage.UpdateSID();
            return View(nobas);
        }
        /// <summary>
        /// 待处理供应商界面
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult SupplyMan()
        {
            Tk_SupplierBas Bas = new Tk_SupplierBas();
            Bas.Sid = SupplyManage.UpdateSID();
            Acc_Account account = GAccount.GetAccountInfo();

            ViewData["webkey"] = "准入评审";
            string fold = COM_ApprovalMan.getNewwebkey("准入评审");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            ViewData["Department"] = account.UnitName;
            ViewData["job"] = account.Exjob;
            ViewData["UserName"] = account.UserName;
            return View(Bas);
        }
        /// <summary>
        /// 合格供应商界面
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult SupplyOK()
        {
            //ViewData["webkey"] = "准入评审";
            //string fold = COM_ApprovalMan.getNewwebkey("准入评审");
            //ViewData["folderBack"] = fold;
            //string[] arr = fold.Split('/');
            //ViewData["Nostate"] = arr[7];  //审批不通过的状态
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas.Sid = SupplyManage.UpdateSID();
            Acc_Account acc = GAccount.GetAccountInfo();
            // ViewBag.exjob = acc.Exjob;
            ViewData["exjob"] = acc.Exjob;
            ViewData["Department"] = acc.UnitName;
            ViewData["UserName"] = acc.UserName;
            return View(bas);
        }
        /// <summary>
        /// 供应商违规处理主视图
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult SupplyWeigui()
        {
            #region MyRegion

            //string[] arr1 = sid.Split('/');
            //ViewData["data"] = arr1[0];//BGOI_BasMan
            //ViewData["table"] = arr1[1];//tk_SupplierBas

            //  ViewData["sid"] = sid;
            // List<SelectListItem> key = SupplyManage.getNewKey(arr1[0].ToString(), arr1[1].ToString());
            //string fold = "";
            //if (key == "建议停止供货待审批")
            //{ 

            //ViewData["webkey"] = "";
            //string fold = COM_ApprovalMan.getNewwebkey("");

            //}
            //else if (key == "建议暂停供货待审批")
            //{
            //    ViewData["webkey"] = "准出暂停供货评审";
            //    fold = COM_ApprovalMan.getNewwebkey("准出暂停供货评审");
            //}
            //else
            //{
            //    ViewData["webkey"] = "准出淘汰供应商评审";
            //    fold = COM_ApprovalMan.getNewwebkey("准出淘汰供应商评审");
            //} 


            //ViewData["folderBack"] = fold;
            //string[] arr = fold.Split('/');
            //ViewData["Nostate"] = arr[7];//审批不通过
            #endregion
            Acc_Account acc = GAccount.GetAccountInfo();
            // ViewBag.exjob = acc.Exjob;
            ViewData["exjob"] = acc.Exjob;
            return View();
        }
        /// <summary>
        /// 供应商准入评审主视图
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult Certificate()
        {
            ViewData["webkey"] = "准入评审";
            string fold = COM_ApprovalMan.getNewwebkey("准入评审");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewData["exjob"] = acc.Exjob;
            // ViewBag.exjob = acc.Exjob;
            return View();
        }
        public ActionResult ApprovalProcess(tk_SupplySearch supplySearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                string OrderDate = "";
                string sname = supplySearch.COMNameC;
                string type = Request["type"].ToString();
                string area = Request["area"].ToString();
                string state = Request["state"].ToString();
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Request["sName"].ToString() != "")
                {
                    where += "  and a.COMNameC like '%" + Request["sName"].ToString() + "%'";
                }
                if (type != "")
                {
                    where += "  and a.SupplierType = '" + type + "'";
                }
                if (area != "")
                {
                    where += "  and g.DeptId = '" + area + "'";
                }
                if (state != "")
                {
                    where += "  and a.State = '" + state + "'";
                }
                string Order = ""; //" order by a.State2";
                if (Request["order"] != null)
                    OrderDate = Request["order"].ToString();
                if (OrderDate != "")
                {
                    string[] arrLast = OrderDate.Split('@');
                    Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
                }
                else
                {
                    Order = " ORDER BY a.State asc ";
                }
                UIDataTable udtTask = SupplyManage.getNewManageGridSP(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        /// <summary>
        /// 供应商年度审批主视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplyApproval()
        {
            ViewData["webkey"] = "年度评审";
            string fold = COM_ApprovalMan.getNewwebkey("年度评审");
            ViewData["folderBack"] = fold;
            string[] arr = fold.Split('/');
            ViewData["Nostate"] = arr[7];  //审批不通过的状态
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewData["exjob"] = acc.Exjob;
            return View();
        }
        /// <summary>
        /// 客户管理主视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CustemerMang()
        {
            tk_KClientBas clbs = new tk_KClientBas();
            clbs.KID = SupplyManage.GetKID();
            return View(clbs);
        }
        #endregion
        #region 验证是否重名
        /// <summary>
        /// 验证供应商编号是否重名方法
        /// </summary>
        /// <param name="SupplierCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckUserAccountExists(string SupplierCode)
        {
            List<string> list = SupplyManage.GetCode();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == SupplierCode.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 供应商名称不能重复
        /// </summary>
        /// <param name="COMNameC"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckSupplyName(string COMNameC)
        {
            List<string> list = SupplyManage.GetSName();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == COMNameC.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckisnotSupplyName(string COMNameC)
        {
            List<string> list = SupplyManage.GetisnotName();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == COMNameC.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckCodeExists(string Ccode)
        {
            List<string> list = SupplyManage.GetZSCode();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == Ccode.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckCNameExists(string Cname)
        {
            List<string> list = SupplyManage.GetName();
            bool exist = string.IsNullOrEmpty(list.FirstOrDefault(u => u.ToLower() == Cname.ToLower())) == false;
            return Json(!exist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 次视图
        /// <summary>
        /// 提交准入审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult SubmitShenpi(string sid)
        {
            tk_SApproval approval = new tk_SApproval();
            approval.SID = sid;
            approval.PID = SupplyManage.GetnShowPid();
            ViewBag.pid = approval.PID;
            return View(approval);
        }
        /// <summary>
        /// 审批
        /// </summary>
        /// <returns></returns>
        public ActionResult Shenpi(string sid)
        {
            tk_SApproval sap = new tk_SApproval();
            ViewBag.pid = sid;
            return View(sap);
        }
        public ActionResult ResultApp(string sid)
        {
            Tk_SupplierBas sap = new Tk_SupplierBas();
            ViewBag.sid = sid;
            tk_SApproval app = new tk_SApproval();
            app = SupplyManage.getapp(sid);
            ViewData["IsPass"] = app.IsPass;
            ViewData["Opinion"] = app.Opinion;
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewBag.user = acc.UserName;
            return View(sap);
        }
        /// <summary>
        /// 违规审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult WeiguiShenpi(string sid)
        {
            tk_SProcessInfo processinfo = new tk_SProcessInfo();
            ViewBag.pid = sid;
            return View(processinfo);
        }
        /// <summary> 
        /// 内部审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>

        public ActionResult InnerApproval(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas.Sid = sid;
            ViewBag.sid = sid;
            List<SelectListItem> items = new List<SelectListItem>();
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("UnReviewUnit", "tk_ConfigSupUnit", false);
            this.ViewBag.UnReviewUnit = items;
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SFileInfo file = new tk_SFileInfo();
            tk_PriceUp price = new tk_PriceUp();
            file = SupplyManage.getmfilename(sid);
            price = SupplyManage.getbijiao(sid);
            ViewData["MFFileName"] = file.Ffilename;
            ViewData["PriceName"] = price.PriceName;
            return View(bas);
        }
        /// <summary>
        /// 负责人内部评审意见
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult FZInnerApproval(string sid)
        {
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            sgs.Sid = sid;
            ViewBag.sid = sid;
            return View(sgs);
        }
        /// <summary>
        /// 恢复供应商建议
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult BackSugestionApproval(string sid)
        {
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(sid);
            ViewData["Opinions"] = info.Opinions;
            sgs.Sid = sid;
            ViewBag.sid = sid;
            return View();
        }
        public ActionResult YearApproval(string sid)
        {
            //获得年度评审ID
            tk_SYRDetail bas = new tk_SYRDetail();
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewData["DeclareUser"] = acc.UserName;
            ViewData["DeclareUnit"] = acc.UnitName;
            bas.SID = sid;
            ViewBag.sid = sid;
            return View(bas);
        }
        #endregion
        #region 跳转添加界面
        /// <summary>
        /// 添加资质,获得bas最新sid
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddFileInfo(string sid)
        {
            tk_SFileInfo file = new tk_SFileInfo();
            ViewBag.sid = sid;
            file.Sid = sid;
            return View(file);
        }
        /// <summary>
        /// 添加证书
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddCertificate(string sid)
        {
            ViewBag.sid = sid;
            tk_SCertificate certificate = new tk_SCertificate();
            certificate.Sid = sid;
            return View(certificate);
        }
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddPro(string sid)
        {
            ViewBag.sid = sid;
            tk_SProducts product = new tk_SProducts();
            product.Sid = sid;
            return View(product);
        }
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddServer(string sid)
        {
            ViewBag.sid = sid;
            tk_SService service = new tk_SService();
            service.Sid = sid;
            return View(service);
        }
        /// <summary>
        /// 上传曾获奖项
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddAwardInfo(string sid)
        {
            ViewBag.sid = sid;
            tk_Award bas = new tk_Award();
            bas.SID = sid;
            return View(bas);
        }
        /// <summary>
        /// 上传询价/比价单
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult AddPriceInfo(string sid)
        {
            ViewBag.sid = sid;
            tk_PriceUp bas = new tk_PriceUp();
            bas.SID = sid;
            return View(bas);
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCustomer()
        {
            ViewBag.kid = SupplyManage.GetKID();
            tk_KClientBas cbs = new tk_KClientBas();
            cbs.KID = SupplyManage.GetKID();
            List<SelectListItem> items = new List<SelectListItem>();
            items = new List<SelectListItem>();
            items = SupplyManage.GetDept("UM_UnitNew", false);
            this.ViewBag.ShareUnits = items;
            return View(cbs);
        }
        /// <summary>
        /// 新增非合格供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult Addisnotsuply()
        {
            ViewBag.kid = SupplyManage.UpdatenotSID();
            tk_IsNotSupplierBas cbs = new tk_IsNotSupplierBas();
            cbs.SID = SupplyManage.UpdatenotSID();
            return View(cbs);
        }
        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult AddPerson(string kid)
        {
            ViewBag.kid = kid;
            tk_KContactPerson kcp = new tk_KContactPerson();
            kcp.KID = kid;
            return View(kcp);
        }
        /// <summary>
        /// 添加共享单位
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult AddUnite(string kid)
        {
            ViewBag.kid = kid;
            tk_KClientBas kuwc = new tk_KClientBas();
            kuwc.KID = kid;
            return View(kuwc);
        }
        #endregion
        #region 添加数据方法
        /// <summary>
        /// 添加客户信息方法
        /// </summary>
        /// <param name="cbs"></param>
        /// <returns></returns>
        public ActionResult AddCus(tk_KClientBas cbs)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                cbs.CreateUser = account.UserName;
                cbs.CreateTime = DateTime.Now.ToString();
                cbs.Validate = "v";
                string Err = "";
                if (SupplyManage.InsertCustomer(cbs, ref Err) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddCustomer", cbs);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddCustomer", cbs);
                }
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult Addisnotsuplyok(tk_IsNotSupplierBas nobas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                nobas.CreateUser = account.UserName;
                nobas.CreateTime = DateTime.Now.ToString();
                nobas.Validate = "v";
                nobas.State = "0";
                nobas.UnitID = account.UnitID;
                string Err = "";
                if (SupplyManage.Insertisnosuply(nobas, ref Err) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("Addisnotsuply", nobas);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("Addisnotsuply", nobas);
                }
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        /// <summary>
        /// 添加资质方法
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        public ActionResult InsertFileMsg(tk_SFileInfo fileinfo)
        {
            HttpFileCollection filc = System.Web.HttpContext.Current.Request.Files;
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                fileinfo.Createuser = account.UserName;
                fileinfo.Createtime = DateTime.Now;
                fileinfo.Validate = "v";
                #region 单个文件上传
                //HttpPostedFileBase file = Request.Files[0];
                //byte[] fileByte = new byte[0];
                //if (file.FileName != "")
                //{
                //    //这个获取文档名称
                //    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                //    fileinfo.Ffilename = a.Substring(0, a.IndexOf('.'));
                //    fileinfo.Filetype = a.Substring(a.LastIndexOf('.') + 1);
                //    int fileLength = file.ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        file.InputStream.Read(fileByte, 0, fileLength);
                //    }
                //} 
                #endregion
                string strErr = "";
                if (SupplyManage.InsertFileMsg(fileinfo, ref strErr, filc) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddFileInfo", fileinfo);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddFileInfo", fileinfo);
                }
            }
            else
            {
                ViewData["msg"] = "资质文件验证不通过";
                return View("AddFileInfo", fileinfo);
            }
        }
        /// <summary>
        /// 添加联系人方法
        /// </summary>
        /// <param name="kcp"></param>
        /// <returns></returns>
        public ActionResult AddPersons(tk_KContactPerson kcp)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            kcp.CreateUser = acc.UserName;
            kcp.CreateTime = DateTime.Now;
            kcp.Validate = "v";
            string Err = "";
            if (SupplyManage.InsertPerson(kcp, ref Err) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("AddPerson", kcp);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("AddPerson", kcp);
            }
        }
        /// <summary>
        /// 添加共享单位方法
        /// </summary>
        /// <param name="kuwc"></param>
        /// <returns></returns>
        public ActionResult AdduniteInfo(tk_KClientBas kuwc)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            kuwc.CreateUser = acc.UserName;
            kuwc.CreateTime = DateTime.Now.ToString();
            kuwc.Validate = "v";
            string Err = "";
            if (SupplyManage.InsertUnites(kuwc, ref Err) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("AddUnite", kuwc);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("AddUnite", kuwc);
            }
        }
        #endregion

        #region 跳转更新页面
        /// <summary>
        /// 修改供应商基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateBas(string sid)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            Tk_SContactPerson per = new Tk_SContactPerson();
            tk_SupplierBasHis bashis = new tk_SupplierBasHis();
            bashis = SupplyManage.getMsg(sid);
            bas = SupplyManage.GetUpdateBas(sid);//从数据库获取存在的数据并显示
            per = SupplyManage.GetPS(sid);//拿到联系人
            List<SelectListItem> items = new List<SelectListItem>();
            ViewData["DeclareUnitID"] = acc.UnitName;
            ViewData["FDepartment"] = per.Fdepartment;
            ViewData["PName"] = per.Pname;
            ViewData["Department"] = per.Department;
            ViewData["Job"] = per.Job;
            ViewData["Phone"] = per.Phone;
            ViewData["Mobile"] = per.Mobile;
            ViewData["Email"] = per.Email;
            ViewData["COMNameC"] = bashis.COMNameC;
            #region 动态生成
            //业务分布
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("BusinessDistribute", "tk_BusinessDistribute", false);
            this.ViewBag.BusinessDistribute = items;
            //开票方式
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("BillingWay", "tk_ConfigBillWay", false);
            this.ViewBag.BillingWay = items;
            //供需关系
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("Relation", "tk_ConfigReation", false);
            this.ViewBag.Relation = items;
            //按供应商规模和经营品种分类
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("ScaleType", "tk_ConfigScalType", false);
            this.ViewBag.ScaleType = items;
            //产品质量执行标准
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("CPZLZXBZ", "tk_ConfigQualityStandard", false);
            this.ViewBag.QualityStandard = items;
            //代理产品国内所属级别
            items = new List<SelectListItem>();
            items = SupplyManage.GetConfig("AgentClass", "tk_ConfigAgentClass", false);
            this.ViewBag.AgentClass = items;
            #endregion
            return View(bas);
        }
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult UpdateCustomer(string kid)
        {
            tk_KClientBas upcbs = new tk_KClientBas();
            upcbs = SupplyManage.getUPCbs(kid);
            List<SelectListItem> items = new List<SelectListItem>();
            items = new List<SelectListItem>();
            items = SupplyManage.GetDept("UM_UnitNew", false);
            this.ViewBag.ShareUnits = items;
            return View(upcbs);
        }
        /// <summary>
        /// 更新非合格供应商
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult Updateisnotsuply(string sid)
        {
            tk_IsNotSupplierBas upcbs = new tk_IsNotSupplierBas();
            upcbs = SupplyManage.getNewSuplycontent(sid);
            return View(upcbs);
        }
        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult UPPerson(string kid, DateTime time)
        {
            tk_KContactPerson kcp = new tk_KContactPerson();
            kcp = SupplyManage.getUpPeson(kid, time);
            return View(kcp);
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult UPProduct(string sid, string id, DateTime time, string filename)
        {
            tk_SProducts sp = new tk_SProducts();
            sp = SupplyManage.getProduct(sid, id, time, filename);
            ViewData["Productname"] = sp.Productname;
            ViewData["Standard"] = sp.Standard;
            return View(sp);
        }
        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult UPServer(string sid, string id, string filename)
        {
            tk_SService sser = new tk_SService();
            sser = SupplyManage.getServer(sid, id, filename);
            return View(sser);
        }
        /// <summary>
        /// 更新资质
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult UPFile(string sid, string fid, DateTime time, string filename)
        {
            tk_SFileInfo sfi = new tk_SFileInfo();
            sfi = SupplyManage.getFile(sid, fid, time, filename);
            return View(sfi);
        }
        /// <summary>
        /// 资质过期提醒
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public ActionResult RemarkFile(string sid, string fid)
        {
            tk_SFileInfo sf = new tk_SFileInfo();
            sf = SupplyManage.GetNewFile(sid, fid);
            return View(sf);
        }
        /// <summary>
        /// 更新证书
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult UPCertifify(string sid, string fid, DateTime time, string filename)
        {
            tk_SCertificate scf = new tk_SCertificate();
            scf = SupplyManage.getCertify(sid, fid, time, filename);
            return View(scf);
        }
        public ActionResult RemarkCertifity(string sid)
        {
            tk_SCertificate scf = new tk_SCertificate();
            scf = SupplyManage.getNewRemarkCertify(sid);
            return View(scf);
        }
        /// <summary>
        /// 更新共享单位
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult UPUnite(string kid)
        {
            tk_KClientBas cbs = new tk_KClientBas();
            cbs = SupplyManage.getUpUnit(kid);
            return View(cbs);
        }
        #endregion
        #region 删除操作
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepPro()
        {
            var Sid = Request["data1"];
            var ID = Request["id"];//唯一编号
            string strErr = "";
            if (SupplyManage.deleteNewPro(Sid, ID, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除报价/比价单
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepPrice()
        {
            var Sid = Request["data1"];
            var ID = Request["id"];//唯一编号
            string strErr = "";
            if (SupplyManage.deleteNewprice(Sid, ID, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }

        public ActionResult deletepAward()
        {
            var Sid = Request["data1"];
            var ID = Request["id"];//唯一编号
            string strErr = "";
            if (SupplyManage.deleteNewaward(Sid, ID, ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 撤销供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelSup()
        {
            var id = Request["data1"].ToString();
            string Err = "";
            if (SupplyManage.CancelSup(id, ref Err) == true)
            {
                return Json(new { succcess = "true", Msg = "撤销成功" });
            }
            else
            {
                return Json(new { succcess = "false", Msg = "撤销失败" + Err });
            }
        }
        public ActionResult RestSup()
        {
            var id = Request["data1"].ToString();
            string Err = "";
            if (SupplyManage.RestSup(id, ref Err) == true)
            {
                return Json(new { succcess = "true", Msg = "重新提交成功" });
            }
            else
            {
                return Json(new { succcess = "false", Msg = "重新提交失败" + Err });
            }
        }
        /// <summary>
        /// 删除服务
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepServer()
        {
            var id = Request["data1"];
            var time = Convert.ToDateTime(Request["time"]);
            string strErr = "";
            if (SupplyManage.deleteNewServer(id, time, ref  strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除资质文件
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepApproval()
        {
            var id = Request["data1"];
            var time = Convert.ToDateTime(Request["time"]);
            string strErr = "";
            if (SupplyManage.deleteNewFile(id, time, ref   strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除证书
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepZhenshu()
        {
            var id = Request["data1"];
            var time = Convert.ToDateTime(Request["time"]);
            string strErr = "";
            if (SupplyManage.deleteNewCertificate(id, time, ref   strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepPerson()
        {
            var id = Request["data1"].ToString();
            string strErr = "";
            if (SupplyManage.deleteNewPerson(id, ref   strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除共享单位
        /// </summary>
        /// <returns></returns>
        public ActionResult deletepShareUnit()
        {
            var id = Request["data1"].ToString();
            string strErr = "";
            if (SupplyManage.deleteNewUnit(id, ref   strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + strErr });
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleCus()
        {
            var kid = Request["data1"];
            string Err = "";
            if (SupplyManage.deleteNewCus(kid, ref   Err) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "--" + Err });
        }
        public ActionResult Deleisnotsuply()
        {
            var sid = Request["data1"].ToString();
            string Err = "";
            if (SupplyManage.deleteisnotsuply(sid, ref Err) == true)
            {
                return Json(new { success = "true", Msg = "删除成功" });
            }
            else
            {
                return Json(new { success = "false", Msg = "删除出错" + "--" + Err });
            }
        }
        #endregion
        #region 导出EXCEL
        /// <summary>
        /// 客户信息导出到excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileResult CustomerToExcel()
        {
            string strCurPage;
            string strRowNum;
            string where = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";
            string CName = Request["CName"].ToString();
            string ctype = Request["CType"].ToString();
            string crelation = Request["CRelation"].ToString();
            string gandate = Request["GainDate"].ToString();
            string state = Request["state"].ToString();
            if (CName != "")
            {
                where += "  and a.CName like '%" + CName + "%'";
            }

            if (ctype != "")
            {
                where += "  and a.CType = '" + ctype + "'";
            } if (crelation != "")
            {
                where += "  and a.CRelation = '" + crelation + "'";
            }
            if (gandate != "")
            {
                where += "  and a.GainDate like '%" + gandate + "%'";
            }

            if (state != "")
            {
                where += "  and a.State = '" + state + "'";
            }
            DataTable dt = SupplyManage.CustomerToExcel(where);
            if (dt != null)
            {
                string strCols = "客户ID-4000,客户时间-4000,填报单位-4000,填表人-4000,负责人-4000,是否共享-4000,共享部门-4000,客户名称-4000,客户简称-4000,所属行业-4000,人员规模-3000,意向产品-4000,客户座机-4000";
                strCols += ",传真-4000,邮编-4000,公司网址-4000,公司地址-4000,所属省份-4000,所属城市-4000,客户介绍-4000,备注-4000,客户类别-4000,客户等级-4000,客户来源-4000,客户关系-4000,成熟度-4000,状态-4000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "客户信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "客户信息.xls");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 合格供应商打印
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FileResult SupOkToExcel()
        {
            string strCurPage;
            string strRowNum;
            string where = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";
            string COMNameC = Request["COMNameC"].ToString();
            string SupplierType = Request["Type"].ToString();
            string COMArea = Request["Area"].ToString();
            string state = Request["state"].ToString();

            string ProductName = Request["ProductName"].ToString();
            #region 查询条件


            if (COMNameC != "")
            {
                where += "  and a.COMNameC like '%" + COMNameC + "%'";
            }
            if (SupplierType != "")
            {
                where += "  and a.SupplierType = '" + SupplierType + "'";
            }
            if (COMArea != "")
            {
                where += "  and a.COMArea = '" + COMArea + "'";
            }
            if (ProductName != "")
            {
                where += "  and a.ProductName like '%" + ProductName + "%'";
            }
            if (state != "")
            {
                where += "  and a.State='" + state + "'";
            }
            #endregion
            DataTable dt = SupplyManage.SPOKToExcel(where);
            if (dt != null)
            {
                string strCols = "流水号-4000,成为合格供应商日期-4000,供应商类别-4000,公司名称-5000,公司简称-4000,所属国家-4000,所属城市-4000,公司注册地址-5000,公司创建时间-4000,三证编号-4000,税务登记号-4000,营业执照号码-3000,组织机构代码-4000,公司办公地址-5000,法人代表-4000";
                strCols += ",公司出厂/出货地址-4000,注册资金-4000,单位-1000,开户名称-4000,银行账号-5000,公司总人数(人)-4000,企业类型-4000,业务分布(%)-7000,开票方式(%)-7000,去年营业额(万元)-4000,研发人数(人)-4000,质量人数(人)-4000,生产人数(人)-4000,供需关系-8000";
                strCols += ",是否有健全的组织机构-4000,工作开始时间-4000,工作结束时间-4000,工作日开始时间-4000";
                strCols += ",工作日结束时间-4000,经营范围-5000,拟购产品行业排名是否在前五名-4000,排名-3000,名次(名)-2000";
                strCols += ",供应商规模和经营品种分类-6000,产品质量执行标准-6000,是否有代理产品授权证明-4000";
                strCols += ",是否有产品生产厂家图纸-4000,代理产品国内所属级别-6000,代理进口产品进货证明材料-4000";
                strCols += ",职责部门-3000,姓名-4000,部门-4000,职位-4000,座机-4000,手机-4000,邮箱-5000,状态-5000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "合格供应商信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "合格供应商信息.xls");
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult YearOutExcel()
        {
            string strCurPage;
            string strRowNum;
            string where = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "15";
            string COMNameC = Request["COMNameC"].ToString();
            string SupplierType = Request["SupplierType"].ToString();
            string Date = Request["ReviewDate"].ToString();
            //string SID = Request["SID"].ToString();

            if (COMNameC != "")
            {
                where += "  and b.COMNameC like '%" + COMNameC + "%'";
            }
            if (SupplierType != "")
            {
                where += "  and b.SupplierType = '" + SupplierType + "'";
            }
            if (Date != "")
            {
                where += "  and b.ReviewDate = '" + Date + "'";
            }

            //if (SID != "")
            //{
            //    where += "  and b.SID='" + SID + "'";
            //}
            DataTable dt = SupplyManage.SPYearExcel(where);
            if (dt != null)
            {
                string strCols = "流水号-4000,供应商名称-6000,质量管理体系得分-4000,价格得分-4000,供货及时率得分-4000,服务得分-4000,得分总计-4000,待改进意见-4000,评分部门-4000,评分人-3000,评价日期-4000";
                System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "年度评审信息表", strCols.Split(','));
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "年度评审信息.xls");
            }
            else
            {
                return null;
            }
        }


        //[HttpPost]
        //public ActionResult PrintToExcel()
        //{
        //    string sid = Request["SID"].ToString();
        //    DataTable dt = SupplyManage.PrintExcel(sid);
        //    if (dt != null)
        //    {
        //        string strCols = "流水号-4000,创建日期-4000,供应商类别-4000,公司名称-4000,公司简称-4000,所属地区-4000,所属国家-4000,公司注册地址-4000,公司创建时间-4000,税务登记号-4000,营业执照号码-3000,公司办公地址-4000,法人代表-4000";
        //        strCols += ",公司出厂/出货地址-4000,组织机构代码-4000,注册资金-4000,开户名称-4000,银行账号-4000,公司总人数-4000,企业类型-4000,业务分布-4000,开票方式-4000,去年营业额-4000,研发人数-4000,质量人数-4000,生产人数-4000,供需关系-4000";
        //        strCols += ",是否有健全的组织机构-4000,工作开始时间-4000,工作结束时间-4000,工作日开始时间-4000";
        //        strCols += ",工作日结束时间-4000,经营范围-4000,拟购产品行业排名-4000,排名-4000,名次-4000";
        //        strCols += ",供应商规模和经营品种分类-4000,产品质量执行标准-4000,是否有代理产品授权证明-4000";
        //        strCols += ",是否有产品生产厂家图纸-4000,代理产品国内所属级别-4000,代理进口产品进货证明材料-4000";
        //        strCols += ",职责部门-4000,姓名-4000,部门-4000,职位-4000,座机-4000,手机-4000,邮箱-4000";
        //        System.IO.MemoryStream stream = ExcelHelper.ExportDataTableToExcel(dt, "恢复供应商信息表", strCols.Split(','));
        //        stream.Seek(0, System.IO.SeekOrigin.Begin);
        //        return File(stream, "application/vnd.ms-excel", "供应商相关信息.xls");
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        #endregion
        #region 审批操作
        /// <summary>
        /// 处理审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult ApproverSP(string sid)
        {
            ViewBag.sid = sid;
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewBag.DeclareUnitID = acc.UnitName;
            Tk_SupplierBas product = new Tk_SupplierBas();
            product = SupplyManage.Approver(sid);
            product.Sid = sid;
            return View(product);
        }
        /// <summary>
        /// 负责人审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult FZRSP(string sid)
        {
            ViewBag.sid = sid;
            Acc_Account acc = GAccount.GetAccountInfo();
            Tk_SupplierBas product = new Tk_SupplierBas();
            product = SupplyManage.Approver(sid);
            product.Sid = sid;
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(sid);
            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View(product);
        }
        /// <summary>
        /// 年度部门级审批
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult BumenSP(string sid)
        {
            ViewBag.sid = sid;
            Acc_Account acc = GAccount.GetAccountInfo();
            Tk_SupplierBas product = new Tk_SupplierBas();
            product = SupplyManage.Approver(sid);
            product.Sid = sid;
            tk_SYRDetail del = new tk_SYRDetail();
            del = SupplyManage.getDetails(sid);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View(product);
        }
        public ActionResult BackSupbumen(string sid)
        {
            ViewBag.sid = sid;
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(sid);
            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View();
        }
        public ActionResult RecoverSupply(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            ViewBag.sid = id;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            tk_SProcessInfo info = new tk_SProcessInfo();
            info.SID = id;
            return View(info);
        }
        /// <summary>
        /// 年度最终评审
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult YearResult(string sid)
        {
            ViewBag.sid = sid;
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.Approver(sid);
            bas.Sid = sid;
            return View(bas);
        }

        /// <summary>
        /// 供应商恢复
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult BackSup(string sid)
        {
            ViewBag.sid = sid;
            tk_FeedBack fb = new tk_FeedBack();
            tk_SYRDetail syscore = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            info = SupplyManage.getProceinfo(sid);
            bas = SupplyManage.getBAS(sid);
            syscore = SupplyManage.getScore(sid);
            ViewData["Opinions"] = info.Opinions;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["NState"] = bas.NState;

            ViewData["Score1"] = syscore.Score1;
            ViewData["Score2"] = syscore.Score2;
            ViewData["Score3"] = syscore.Score3;
            ViewData["Score4"] = syscore.Score4;
            ViewData["Score5"] = syscore.Score5;
            ViewData["Result"] = syscore.Result;
            ViewData["ResultDesc"] = syscore.ResultDesc;
            fb.SID = sid;
            return View(fb);
        }
        #endregion
        #region 展示数据
        /// <summary>
        /// 展示供应商基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplyGrid()
        {
            string where = "and";
            string strCurPage;
            string strRowNum;
            string Supply = Request["supply"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (Supply != "")
                where += " and a.SID like '%" + Supply + "%'";
            string FDepartment = Request["FDepartment"].ToString();
            string PName = Request["PName"].ToString();
            string Department = Request["Department"].ToString();
            string Job = Request["Job"].ToString();
            string Phone = Request["Phone"].ToString();
            int Mobile = Convert.ToInt32(Request["Mobile"]);
            UIDataTable udtTask = InventoryMan.StockInList(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 添加联系人下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContractSize()
        {
            string strType = Request["Type"].ToString();
            DataTable dt = SupplyManage.GetNewCon(strType);
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["SID"].ToString() + ",";
                name += dt.Rows[i]["Text"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "请选择" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }

        public ActionResult ProjectGrid()
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                string sid = Request["sid"].ToString();
                where += "   and a.SID ='" + sid + "'";
                UIDataTable udtTask = SupplyManage.getNewPSupplyGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult CertificateGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            where += "   and a.SID ='" + sid + "'";
            UIDataTable udtTask = SupplyManage.getNewCertificateGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            where += "   and a.SID ='" + sid + "'";
            UIDataTable udtTask = SupplyManage.getNewProGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ServerGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            where += "   and a.SID ='" + sid + "'";
            UIDataTable udtTask = SupplyManage.getNewServerGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AwardGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            where += "   and a.SID ='" + sid + "'";
            UIDataTable udtTask = SupplyManage.getNewAwardGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AwardNewGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //获取基础表中的sid
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewsAwardGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PriceNewGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //获取基础表中的sid
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewsPriceGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PriceGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            where += "   and a.SID ='" + sid + "'";
            UIDataTable udtTask = SupplyManage.getNewPriceGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageGrid(tk_SupplySearch supplysearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                string type = Request["type"].ToString();
                string area = Request["area"].ToString();
                string state = Request["state"].ToString();
                string sname = supplysearch.COMNameC;
                string OrderDate = "";
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Request["sName"].ToString() != "")
                {
                    where += "  and a.COMNameC like '%" + Request["sName"].ToString() + "%'";
                }
                if (type != "")
                {
                    where += "  and a.SupplierType = '" + type + "'";
                }
                if (area != "")
                {
                    where += "  and k.DeptId = '" + area + "'";
                }
                if (state != "")
                {
                    where += "  and a.State = '" + state + "'";
                }
                string Order = ""; //" order by a.State2";
                if (Request["order"] != null)
                    OrderDate = Request["order"].ToString();
                if (OrderDate != "")
                {
                    string[] arrLast = OrderDate.Split('@');
                    Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
                }
                else
                {
                    Order = "  ORDER BY a.CreateTime desc";
                }
                UIDataTable udtTask = SupplyManage.getNewManageGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult ManageokGrid(tk_SupplySearch supplysearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                string OrderDate = "";
                string type = Request["type"].ToString();
                string area = Request["area"].ToString();
                string state = Request["state"].ToString();
                string sname = supplysearch.COMNameC;
                string proName = Request["pName"].ToString();
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (Request["sName"].ToString() != "")
                {
                    where += "  and a.COMNameC like '%" + Request["sName"].ToString() + "%'";
                }
                if (type != "")
                {
                    where += "  and a.SupplierType = '" + type + "'";
                }
                if (area != "")
                {
                    where += "  and j.DeptId = '" + area + "'";
                }
                if (proName != "")
                {
                    where += "  and k.ProductName like'%" + proName + "%'";
                }
                if (state != "")
                {
                    where += "  and a.state='" + state + "'";
                }
                string Order = ""; //" order by a.State2";
                if (Request["order"] != null)
                    OrderDate = Request["order"].ToString();
                if (OrderDate != "")
                {
                    string[] arrLast = OrderDate.Split('@');
                    Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
                }
                else
                {
                    Order = "  order by  w.CreateTime desc";
                }
                UIDataTable udtTask = SupplyManage.getNewManageokGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult ReviewGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string OrderDate = "";
            string sname = Request["sName"].ToString();
            string type = Request["type"].ToString();
            string supyear = Request["appyear"].ToString();
            // string supcode = Request["appcode"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (sname != "")
            {
                where += "  and a.COMNameC like '%" + sname + "%'";
            }
            if (type != "")
            {
                where += "  and a.SupplierType = '" + type + "'";
            }
            if (supyear != "")
            {
                where += "  and c.Year = '" + supyear + "'";
            }
            //if (supcode != "")
            //{
            //    where += "  and a.SID = '" + supcode + "'";
            //}
            string Order = ""; //" order by a.State2";
            if (Request["order"] != null)
                OrderDate = Request["order"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }
            else
            {
                Order = "  ORDER BY  CreateTime desc";
            }
            UIDataTable udtTask = SupplyManage.getNewYearGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerGrid(tk_SupplySearch supplysearch)
        {
            if (ModelState.IsValid)
            {
                string where = "";
                string strCurPage;
                string strRowNum;
                string CName = Request["cname"].ToString();
                string ctype = Request["ctype"].ToString();
                string crelation = Request["crelation"].ToString();
                string gandate = Request["gandate"].ToString();
                string state = Request["state"].ToString();
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (CName != "")
                {
                    where += "  and a.CName like '%" + CName + "%'";
                }

                if (ctype != "")
                {
                    where += "  and a.CType like '%" + ctype + "%'";
                } if (crelation != "")
                {
                    where += "  and a.CRelation = '" + crelation + "'";
                }
                if (gandate != "")
                {
                    where += "  and a.GainDate like '%" + gandate + "%'";
                }

                if (state != "")
                {
                    where += "  and a.State = '" + state + "'";
                }

                UIDataTable udtTask = SupplyManage.getNewCustomerGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        /// <summary>
        /// 非合格供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult NoSupplyGrid(tk_SupplySearch tks)
        {
            if (ModelState.IsValid)
            {
                string strCurPage;
                string strRowNum;
                string name;
                string contatct;
                string where = "";
                name = Request["comname"].ToString();
                contatct = Request["contact"].ToString();
                if (Request["curpage"] != null)
                    strCurPage = Request["curpage"].ToString();
                if (Request["rownum"] != null)
                    strRowNum = Request["rownum"].ToString();
                else
                    strRowNum = "10";
                if (name != "")
                {
                    name = Request["comname"].ToString();
                }
                if (contatct!="")
                {
                    contatct = Request["contact"].ToString();
                }
                if (name != "")
                {
                    where += " and a.COMNameC like '%" + name + "%'";
                }
                if (contatct != "")
                {
                    where += "  and a.Contacts like '%" + contatct + "%'";
                }
                UIDataTable udtTask = SupplyManage.getNewisnotsuplyGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1,where);
                string strjson = GFun.Dt2Json("", udtTask.DtData);
                strjson = strjson.Substring(1);
                strjson = strjson.Substring(0, strjson.Length - 1);
                string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
                jsonData += strjson + "}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Msg = "查询条件验证不通过" });
            }
        }
        public ActionResult PersonGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string kid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["kid"] != null)
                kid = Request["kid"].ToString();
            if (kid != "")
            {
                where += " and a.KID = '" + kid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewContractPersonGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, kid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 共享单位
        /// </summary>
        /// <returns></returns>
        public ActionResult ShareGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string kid = "";
            string isshare = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["kid"] != null)
                kid = Request["kid"].ToString();
            if (kid != "")
            {
                where += " and a.KID = '" + kid + "'";
            }
            if (Request["share"] != null)
                isshare = Request["share"].ToString();
            if (isshare != "")
            {
                where += " and a.IsShare = '" + isshare + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewShareGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, kid, isshare);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WeiguiGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string OrderDate = "";
            string comname = Request["comName"].ToString();
            string supptype = Request["suptype"].ToString();
            string comarea = Request["comarea"].ToString();
            string state = Request["state"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (comname != "")
            {
                where += "  and a.COMNameC like '%" + comname + "%'";
            }
            if (supptype != "")
            {
                where += "  and a.SupplierType = '" + supptype + "'";
            }
            if (comarea != "")
            {
                where += "  and h.deptid = '" + comarea + "'";
            }

            if (state != "")
            {
                where += "  and k.SID='" + state + "'";
            }
            string Order = "";
            if (Request["order"] != null)
                OrderDate = Request["order"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }
            else
            {
                Order = " ORDER BY x.CreateTime  desc";
            }
            UIDataTable udtTask = SupplyManage.getNewWeiguirid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ZSGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = "";
            string strCurPage;
            string strRowNum;
            string UserID = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["userid"] != null)
                UserID = Request["userid"].ToString();
            if (UserID != "")
                where += " and a.UserID = '" + UserID + "'";
            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SupplyManage.getNewZSGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, account.UnitID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 展示资质信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ApprovalUserGrid()
        {
            Acc_Account account = GAccount.GetAccountInfo();
            string where = " and a.Unite = '" + account.UnitID + "'";
            string strCurPage;
            string strRowNum;
            string UserName = Request["userName"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (UserName != "")
                where += " and b.UserName like '%" + UserName + "%'";
            UIDataTable udtTask = SupplyManage.getNewApprovalUserGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 方法作废了！
        /// </summary>
        /// <returns></returns>
        public ActionResult WeiguiMsgGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string OrderDate = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string Order = ""; //" order by a.State2";
            if (Request["order"] != null)
                OrderDate = Request["order"].ToString();
            if (OrderDate != "")
            {
                string[] arrLast = OrderDate.Split('@');
                Order = " order by " + arrLast[0] + " " + arrLast[1] + "";
            }//getNewWeiguirid
            UIDataTable udtTask = SupplyManage.getNewWeiguirid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, Order);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovalGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            string sid = Request["sid"].ToString();
            string applever = Request["appLever"].ToString();
            string appcontent = Request["appcontent"].ToString();
            string apppersons = Request["apppersons"].ToString();
            string apptime = Request["apptime"].ToString();
            string apptype = Request["apptype"].ToString();
            if (sid != "")
            {
                where += " and a.SID='" + sid + "'";
            }
            if (applever != "")
            {
                where += "  and a.ApprovalLever='" + applever + "'";
            }
            if (appcontent != "")
            {
                where += "  and a.ApprovalContent='" + appcontent + "'";
            }
            if (apppersons != "")
            {
                where += "  and a.ApprovalPersons='" + apppersons + "'";
            }
            if (apptime != "")
            {
                where += " and a.ApprovalTime='" + apptime + "'";
            }
            if (apptype != "")
            {
                where += "  and a.ApprovalType='" + apptype + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewApprovalGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ManageProGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //获取基础表中的sid
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }

            UIDataTable udtTask = SupplyManage.getNewManageProGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ScoreGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //获取基础表中的sid

            string sname = Request["ShortName"].ToString();
            string type = Request["supplyType"].ToString();
            string supyear = Request["ReDate"].ToString();
            //string supcode = Request["SupCode"].ToString();
            if (sname != "")
            {
                where += "  and a.COMNameC like '%" + sname + "%'";
            }
            if (type != "")
            {
                where += "  and a.SupplierType = '" + type + "'";
            }
            if (supyear != "")
            {
                where += "  and a.ReviewDate = '" + supyear + "'";
            }
            //if (supcode != "")
            //{
            //    where += "  and a.SupplierCode = '" + supcode + "'";
            //}
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and c.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getScoreGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ScoreGrids()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and c.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getScoreGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YearShowGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            //获取基础表中的sid
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getYScoreGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageServerGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewManageSerGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审批记录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult SPRecordGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();

            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }


            UIDataTable udtTask = SupplyManage.getSPRecord(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 恢复供应商记录
        /// </summary>
        /// <returns></returns>
        public ActionResult SRcordGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getSRecord(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManagePlanProGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewPlanProGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManagePlanSerGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getNewPlanSerGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LogShowGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            string kid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
            {
                sid = Request["Sid"].ToString();
            }
            else if (Request["kid"] != null)
            {
                kid = Request["kid"].ToString();
            }
            if (sid != "")
            {
                where += " and a.UserId = '" + sid + "'";
            }
            else if (kid != "")
            {
                where += " and  a.UserId = '" + kid + "'";
            }
            UIDataTable udtTask = SupplyManage.getLogGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid, kid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 评审过程表
        /// </summary>
        /// <returns></returns>
        public ActionResult ConditionGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            string kid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
            {
                sid = Request["Sid"].ToString();
            }

            if (sid != "")
            {
                where += " and a.UserId = '" + sid + "'";
            }

            UIDataTable udtTask = SupplyManage.getcontentGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SPShowGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";

            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
            {
                sid = Request["Sid"].ToString();
            }
            if (sid != "")
            {
                where += " and a.sid = '" + sid + "'";
            }

            UIDataTable udtTask = SupplyManage.getSPGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 恢复供应商记录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadBR()
        {

            string strCurPage;
            string strRowNum;
            string sid = "";
            string content = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
            {
                sid = Request["Sid"].ToString();
            }
            //if (Request["content"] != null)
            //    content = Request["content"].ToString();


            UIDataTable udtTask = SupplyManage.getBRGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 违规处理记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DealRecordShow()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (Request["Sid"] != null)
                sid = Request["Sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getRecordShow(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MainCertificateGrid()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string sid = "";
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";

            if (Request["sid"] != null)
                sid = Request["sid"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            UIDataTable udtTask = SupplyManage.getMainCertificateGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, sid);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 进行更新操作
        /// <summary>
        /// 处理审批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertDetailInfo()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();

            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitID;
            if (SupplyManage.InsertNewDetail(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });

        }
        /// <summary>
        /// 负责人审批添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertFZRInfo()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Approval1User = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitName;
            if (SupplyManage.InsertNewfzr(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });
        }
        /// <summary>
        /// 年度部门级审批保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertBumenInfo()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitName;
            if (SupplyManage.InsertNewBumen(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });
        }
        [HttpPost]
        public ActionResult InsertBackSupbumenInfo()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitName;
            if (SupplyManage.InsertBackSupbumen(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });
        }
        [HttpPost]
        public ActionResult UPRecoverSupply()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitName;
            if (SupplyManage.UPrecover(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });
        }
        /// <summary>
        /// 年度最终评审
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertYearResult()
        {
            tk_SProcessInfo proinfo = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string Err = "";
            Acc_Account account = GAccount.GetAccountInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(proinfo, HttpContext.ApplicationInstance.Context.Request);
            proinfo.CreateTime = DateTime.Now;
            proinfo.CreateUser = account.UserName;
            proinfo.Validate = "v";
            proinfo.DeclareUnit = account.UnitName;
            if (SupplyManage.InsertNewRes(proinfo, ref  Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "---" + Err });
        }
        public ActionResult InsertServerMsg(tk_SService server)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                server.CreateUser = account.UserName;
                server.CreateTime = DateTime.Now;
                server.Validate = "v";
                string strErr = "";
                HttpPostedFileBase file = Request.Files[0];
                byte[] fileByte = new byte[0];
                if (file.FileName != "")
                {
                    //这个获取文档名称
                    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    server.FFileName = a;//.Substring(0, a.IndexOf('.'));
                    int fileLength = file.ContentLength;
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        file.InputStream.Read(fileByte, 0, fileLength);
                    }
                }
                if (SupplyManage.InsertServerMsg(server, fileByte, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddServer", server);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddServer", server);
                }
            }
            else
            {
                ViewData["msg"] = "拟购服务不通过";
                return View("AddServer", server);
            }
        }
        /// <summary>
        /// 上传曾获奖项
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertAward(tk_Award bas)
        {
            if (ModelState.IsValid)
            {
                #region MyRegion
                //string strErr = "";
                //Acc_Account acc = GAccount.GetAccountInfo();

                ////获取上传的文件
                //HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;
                //// 如果没有上传文件
                //if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
                //{
                //    return this.HttpNotFound();
                //}
                // 将信息存入 上传文件数据表中
                //tk_Award fileUp = new tk_Award();
                //fileUp.SID = Request["SID"].ToString();
                //fileUp.Award = Request["Award"].ToString();
                //fileUp.AwardInfo = Request["AwardInfo"].ToString();
                //fileUp.CreatUser = acc.UserName.ToString();
                //fileUp.AwardTime = DateTime.Now;
                //fileUp.Validate = "v";

                //SupplyManage.InsertBiddingNew(bas, Filedata, ref strErr);
                //return this.Json(new { });

                #endregion


                #region MyRegion
                Acc_Account account = GAccount.GetAccountInfo();
                string strErr = "";
                HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;//获取上传的文件
                #region MyRegion
                //HttpPostedFileBase file = Request.Files[0];
                //byte[] fileByte = new byte[0];
                //if (file.FileName != "")
                //{
                //    //这个获取文档名称
                //    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                //    bas.Award = a;
                //    int fileLength = file.ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        file.InputStream.Read(fileByte, 0, fileLength);
                //    }
                //} 
                #endregion
                if (SupplyManage.InsertAwardMsg(bas, file, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddAwardInfo", bas);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddAwardInfo", bas);
                }
            }
            else
            {
                ViewData["msg"] = "上传奖项不成功";
                return View("AddAwardInfo", bas);
            }
                #endregion
        }
        public ActionResult InsertPrice(tk_PriceUp bas)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                string strErr = "";
                HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;//获取上传的文件
                #region MyRegion
                //HttpPostedFileBase file = Request.Files[0];
                //byte[] fileByte = new byte[0];
                //if (file.FileName != "")
                //{
                //    //这个获取文档名称
                //    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                //    bas.PriceName = a;
                //    int fileLength = file.ContentLength;
                //    if (fileLength != 0)
                //    {
                //        fileByte = new byte[fileLength];
                //        file.InputStream.Read(fileByte, 0, fileLength);
                //    }
                //} 
                #endregion
                if (SupplyManage.InsertPriceMsg(bas, file, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddPriceInfo", bas);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddPriceInfo", bas);
                }
            }
            else
            {
                ViewData["msg"] = "询价/比价单上传不成功";
                return View("AddPriceInfo", bas);
            }
        }

        public ActionResult Addmpro(tk_SProducts bas)
        {
            var sid = Request["SID"];
            Acc_Account account = GAccount.GetAccountInfo();
            tk_SProductsHis prohis = new tk_SProductsHis();
            prohis.ProcessUser = account.UserName;
            prohis.ProcessTime = DateTime.Now;
            string strErr = "";
            if (SupplyManage.UpdateNewPro(sid, bas, ref strErr) == true)
            {
                ViewData["SID"] = sid;
                ViewData["msg"] = "保存成功";
                return View("AddmanagePro", bas);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("AddmanagePro", bas);
            }
        }
        /// <summary>
        /// 更新资质文件
        /// </summary>
        /// <param name="sfi"></param>
        /// <returns></returns>
        public ActionResult UPFileMsg(tk_SFileInfo sfi)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                sfi.Ffilename = a;
                sfi.Filetype = a.Substring(a.LastIndexOf('.') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (SupplyManage.UpdateFileMsg(sfi, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("UPFile", sfi);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UPFile", sfi);
            }
        }
        public ActionResult RemarkFileMsg(tk_SFileInfo sf)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                sf.Ffilename = a.Substring(0, a.IndexOf('.'));
                sf.Filetype = a.Substring(a.LastIndexOf('.') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";//UpdateRearkFileMsg
            if (SupplyManage.UpdateFileMsg(sf, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("RemarkFile", sf);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("RemarkFile", sf);
            }
        }
        /// <summary>
        /// 更新证书
        /// </summary>
        /// <param name="scfi"></param> 
        /// <returns></returns>
        public ActionResult UPCertityInfo(tk_SCertificate scfi)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                scfi.Cfilename = a;
                scfi.FileType = a.Substring(a.LastIndexOf('.') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (SupplyManage.UpdateCertityMsg(scfi, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("UPCertifify", scfi);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UPCertifify", scfi);
            }
        }
        /// <summary>
        /// 证书过期提醒
        /// </summary>
        /// <param name="cer"></param>
        /// <returns></returns>
        public ActionResult UPRemarkCertityInfo(tk_SCertificate cer)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                cer.Cfilename = a.Substring(0, a.IndexOf('.'));
                cer.FileType = a.Substring(a.LastIndexOf('.') + 1);
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string strErr = "";
            if (SupplyManage.UpdateCertityMsg(cer, fileByte, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("RemarkCertifity", cer);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("RemarkCertifity", cer);
            }
        }
        /// <summary>
        /// 新增基本表内部评审
        /// </summary>
        /// <param name="bas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpInnerApproval(string sid)
        {
            HttpFileCollection filc = System.Web.HttpContext.Current.Request.Files;
            string strErr = "";
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string srt = bas.UnReviewUnit;
            GSqlSentence.SetTValue<Tk_SupplierBas>(bas, HttpContext.ApplicationInstance.Context.Request);
            if (SupplyManage.UpdateNewApprovalBas(bas, filc, sid, ref strErr) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("InnerApproval", bas);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("InnerApproval", bas);
            }
        }
        /// <summary>
        /// 负责人内部评审意见
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult FZInnerApprovalSG(string sid)
        {
            string ERR = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            sgs.SCreate = (DateTime.Now).ToString();
            sgs.Sperson = acc.UserName;
            GSqlSentence.SetTValue<tk_SUPSugestion>(sgs, HttpContext.ApplicationInstance.Context.Request);
            if (SupplyManage.AddSugestion(sgs, sid, ref ERR) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("FZInnerApproval", sgs);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("FZInnerApproval", sgs);
            }

        }
        /// <summary>
        /// 恢复供应商建议
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult BackupSugestionApproval(string sid)
        {
            string ERR = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            GSqlSentence.SetTValue<tk_SUPSugestion>(sgs, HttpContext.ApplicationInstance.Context.Request);
            if (SupplyManage.AddBackSugestion(sgs, sid, ref ERR) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("BackSugestionApproval", sgs);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("BackSugestionApproval", sgs);
            }
        }
        public ActionResult YearDealApproval(tk_SYRDetail yearDetal)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            Acc_Account ACC = GAccount.GetAccountInfo();
            string sid = yearDetal.SID;
            yearDetal.YRID = SupplyManage.GetYid();//2015001
            yearDetal.Validate = "v";
            yearDetal.CreateUser = ACC.UserName;
            yearDetal.DeclareUnit = ACC.UnitName;
            yearDetal.CreateTime = DateTime.Now;
            yearDetal.DeclareUser = ACC.UserName;
            yearDetal.Year = yearDetal.YRID.Substring(0, 4).ToString();
            string Err = "";
            if (SupplyManage.AddYearDeal(yearDetal, sid, bas, ref Err) == true)
            {
                #region 时间提醒
                //string str1 = "";
                //if (yearDetal.Result == "1")
                //{
                //    str1 = "请在5天之内提交反馈";
                //}
                //else
                //{
                //    str1 = null;
                //}
                //ViewData["dialog"] = str1; 
                #endregion
                ViewData["msg"] = "保存成功";
                return View("YearApproval", yearDetal);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("YearApproval", yearDetal);
            }
        }
        public ActionResult InsertCerfiticateMsg(tk_SCertificate certificate)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                certificate.Createuser = account.UserName.ToString();
                certificate.Createtime = DateTime.Now;
                certificate.Validate = "v";
                certificate.Isplan = Request["Isplan"].ToString();
                HttpPostedFileBase file = Request.Files[0];
                int len = file.ContentLength;
                byte[] fileByte = new byte[0];
                if (file.FileName != "")
                {
                    //这个获取文档名称
                    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);

                    certificate.Cfilename = a;
                    certificate.FileType = a.Substring(a.LastIndexOf('.') + 1);
                    int fileLength = file.ContentLength;
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        file.InputStream.Read(fileByte, 0, fileLength);

                    }
                }
                string strErr = "";
                if (SupplyManage.InsertCertifiMsg(certificate, fileByte, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddCertificate", certificate);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddCertificate", certificate);
                }

            }
            else
            {
                ViewData["msg"] = "证书验证不通过";
                return View("AddCertificate", certificate);
            }
        }
        public ActionResult InsertProMsg(tk_SProducts product)
        {
            if (ModelState.IsValid)
            {
                Acc_Account account = GAccount.GetAccountInfo();
                product.Createuser = account.UserName;
                product.Createtime = DateTime.Now;
                product.Validate = "v";
                product.Productname = Request["Productname"].ToString();
                product.Standard = Request["Standard"].ToString();
                product.Productid = Request["Productid"].ToString();
                HttpPostedFileBase file = Request.Files[0];
                byte[] fileByte = new byte[0];
                if (file.FileName != "")
                {
                    //这个获取文档名称
                    string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    product.FFileName = a;//.Substring(0, a.IndexOf('.'));
                    int fileLength = file.ContentLength;//文件长度
                    if (fileLength != 0)
                    {
                        fileByte = new byte[fileLength];
                        file.InputStream.Read(fileByte, 0, fileLength);
                    }
                }
                string strErr = "";
                if (SupplyManage.InsertProMsg(product, fileByte, ref strErr) == true)
                {
                    ViewData["msg"] = "保存成功";
                    return View("AddPro", product);
                }
                else
                {
                    ViewData["msg"] = "保存失败";
                    return View("AddPro", product);
                }
            }
            else
            {
                ViewData["msg"] = "产品添加不通过";
                return View("AddPro", product);
            }

        }

        public ActionResult FeedBackMsg(tk_FeedBack fb)
        {
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            fb.FName = acc.UserName;
            fb.FTime = DateTime.Now;
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.getBAS(fb.SID);
            if (SupplyManage.FeedBack(fb, ref strErr, bas) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("BackSup", fb);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("BackSup", fb);
            }
        }

        public ActionResult ResReview(string sid, FormCollection fc)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas.ResState = Convert.ToInt32(Request["ResState"]);
            GSqlSentence.SetTValue<Tk_SupplierBas>(bas, HttpContext.ApplicationInstance.Context.Request);

            string strErr = "";
            if (SupplyManage.ResApp(bas, sid, ref strErr) == true)
            {
                ViewData["SID"] = sid;
                ViewData["msg"] = "保存成功";
                return View("ResultApp", bas);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("ResultApp", bas);
            }
        }
        #endregion

        #region 详细信息

        /// <summary>
        /// 基本信息详细信息
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult DetailBas(string sid)
        {

            StringBuilder sb = new StringBuilder();
            DataTable dt = SupplyManage.GetDetailID(sid);//基本信息表
            DataTable dt2 = SupplyManage.getAgenclass(sid);
            DataTable dt3 = SupplyManage.getReation(sid);
            DataTable dt4 = SupplyManage.getScaleType(sid);
            DataTable dt5 = SupplyManage.getQualityStandard(sid);
            //DataTable dt6 = SupplyManage.getBusinessDistribute(sid);
            //DataTable dt7 = SupplyManage.getbillWay(sid);
            DataTable dt8 = SupplyManage.getBillHanzi();//数字替换汉字
            DataTable dt9 = SupplyManage.getBuinessHanzi();//数字替换汉字
            DataTable dtfile = SupplyManage.getFilename(sid);  //资质文件
            DataTable dtcer = SupplyManage.getCertifyname(sid);//证书
            DataTable dtaward = SupplyManage.getAward(sid);    //曾获奖项
            DataTable dtprice = SupplyManage.getpricename(sid); //比价单
            DataTable dtConPerson = SupplyManage.GetDetailConPerson(sid);//联系人表 
            DataTable dtproduct = SupplyManage.getProduct(sid);//产品信息
            DataTable dtserver = SupplyManage.getnewServer(sid);//服务信息

            #region 业务分布和开票方式截取
            string BusinessDistributes = dt.Rows[0]["BusinessDistribute"].ToString();
            string[] bd = BusinessDistributes.Split(':');//国内，欧美，亚洲，其他，非洲：34,43
            string[] one = bd[0].ToString().Split(',');
            string[] two = bd[1].ToString().Split(',');//23,23,2
            string BusinessDistribute = "";
            string BusinessNum = "";
            for (int i = 0; i < one.Length; i++)
            {
                for (int k = 0; k < dt9.Rows.Count; k++)
                {
                    if (one[i] == dt9.Rows[k]["SID"].ToString())
                        one[i] = dt9.Rows[k]["Text"].ToString();

                }
                BusinessNum += one[i] + ",";
                BusinessDistribute += two[i] + "%" + ",";
            }
            string BillingWays = dt.Rows[0]["BillingWay"].ToString();
            string[] bl = BillingWays.Split(':');
            string[] bl1 = bl[0].ToString().Split(',');//0,2
            string[] bl2 = bl[1].ToString().Split(',');//23,32
            string BillingWay = "";
            string BillingNum = "";
            for (int i = 0; i < bl1.Length; i++)
            {
                for (int j = 0; j < dt8.Rows.Count; j++)
                {
                    if (bl1[i] == dt8.Rows[j]["SID"].ToString())
                        bl1[i] = dt8.Rows[j]["Text"].ToString();
                }
                BillingNum += bl1[i] + ",";
                BillingWay += bl2[i] + "%" + ",";

            }
            #endregion
            #region 汉字替换数字
            string time = Convert.ToDateTime(dt.Rows[0]["DeclareDate"]).ToString("yyyy-MM-dd");
            string time2 = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
            string HasDrawing = dt.Rows[0]["HasDrawing"].ToString();//0-1
            if (HasDrawing == "0")
            {
                HasDrawing = "有";
            }
            else
            {
                HasDrawing = "无";
            }
            string HasAuthorization = dt.Rows[0]["HasAuthorization"].ToString();
            if (HasAuthorization == "0")
            {
                HasAuthorization = "是";
            }
            else
            {
                HasAuthorization = "否";
            }
            string IsrankingIn5 = dt.Rows[0]["IsrankingIn5"].ToString();
            if (IsrankingIn5 == "0")
            {
                IsrankingIn5 = "是";
            }
            else
            {
                IsrankingIn5 = "否";
            }
            string IsCooperate = dt.Rows[0]["IsCooperate"].ToString();
            if (IsCooperate == "0")
            {
                IsCooperate = "是";
            }
            else
            {
                IsCooperate = "否";
            }
            string HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
            if (HasRegulation == "0")
            {
                HasRegulation = "是";
            }
            else
            {
                HasRegulation = "否";
            }
            string RankingType = dt.Rows[0]["RankingType"].ToString();
            if (RankingType == "0")
            {
                RankingType = "国内";
            }
            else if (RankingType == "1")
            {
                RankingType = "国际";
            }
            else
            {
                RankingType = "";
            }
            #endregion
            #region 拼接字符串
            #region 基本信息

            sb.Append("<div  id=\"tabTitile\"style=\"font-size:15px;\">流水号：" + sid + "--" + dt.Rows[0]["COMNameC"].ToString() + "</div><div style ='width:1000px;height:600px;overflow-x: scroll; overflow-y: scroll;'>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:1000px;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\" style=\"font-weight:bold;\">填报部门</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["DeptName"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">填报日期</td><td class=\"textRight\"  >" + time + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">供应商类别</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["SupplierType"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">其他</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["OtherType"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司名称(中)</td><td class=\"textRight\">" + dt.Rows[0]["COMNameC"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司简称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["COMShortName"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">三证编号</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ThreeCertity"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">税务登记号</td><td class=\"textRight\">" + dt.Rows[0]["TaxRegistrationNo"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">营业执照号码</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BusinessLicenseNo"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">组织机构与代码</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["OrganizationCode"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司网址</td><td class=\"textRight\">" + dt.Rows[0]["COMWebsite"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">所属城市</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["COMArea"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">公司名称(英)</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMNameE"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">所属国家</td><td class=\"textRight\">" + dt.Rows[0]["COMCountry"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司注册地址</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["COMRAddress"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">公司创建日期</td><td class=\"textRight\" colspan=\"2\">" + time2 + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">法人代表</td><td class=\"textRight\">" + dt.Rows[0]["COMLegalPerson"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司出厂/出货地址</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["COMFactoryAddress"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">工厂面积</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMFactoryArea"].ToString() + "㎡" + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司办公地址</td><td class=\"textRight\">" + dt.Rows[0]["ComAddress"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">供应商集团名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["COMGroup"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">注册资金</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["RegisteredCapital"].ToString() + dt.Rows[0]["CapitalUnit"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">是否与燕山公司合作过</td><td class=\"textRight\">" + IsCooperate + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">开户行名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["BankName"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">银行基本账号</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["BankAccount"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">公司总人数</td><td class=\"textRight\">" + dt.Rows[0]["StaffNum"].ToString() + "人" + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">企业类型</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["EnterpriseType"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">业务分布</td><td class=\"textRight\" colspan=\"2\">" + BusinessNum.Substring(0, BusinessNum.Length - 2) + ":" + BusinessDistribute.Substring(0, BusinessDistribute.Length - 3) + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">开票方式</td><td class=\"textRight\">" + BillingNum.Substring(0, BillingNum.Length - 2) + ":" + BillingWay.Substring(0, BillingWay.Length - 3) + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">去年营业额</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["Turnover"].ToString() + ("万元") + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">员工状况</td><td class=\"textRight\" colspan=\"2\"style=\"font-weight:bold;\">研发人员数量：" + dt.Rows[0]["DevelopStaffs"].ToString() + "人" + "</td><td class=\"textRight\" style=\"font-weight:bold;\">" + "质量人员数量:" + dt.Rows[0]["QAStaffs"].ToString() + "人" + "</td><td class=\"textRight\" colspan=\"5\"style=\"font-weight:bold;\">生产人员数量:" + dt.Rows[0]["ProduceStaffs"].ToString() + "人" + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">供需关系</td><td class=\"textRight\" colspan=\"2\">" + dt3.Rows[0]["Relation"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">是否有健全的组织机构和内部管理制度</td><td class=\"textRight\">" + HasRegulation + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">购买产品生产线数量</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["ProductLineNum"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">正常工作时间</td><td class=\"textRight\" colspan=\"2\">AM &nbsp;&nbsp;&nbsp;" + dt.Rows[0]["WorkTime_Start"].ToString() + "&nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["WorkTime_End"].ToString() + "&nbsp;&nbsp;PM " + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">正常工作日</td><td class=\"textRight\" >" + dt.Rows[0]["WorkDay_Start"].ToString() + "&nbsp;&nbsp;至&nbsp;&nbsp;" + dt.Rows[0]["WorkDay_End"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">经营范围</td><td class=\"textRight\" colspan=\"7\">" + dt.Rows[0]["BusinessScope"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">拟购产品行业排名是否前五</td><td class=\"textRight\"  colspan=\"2\">" + IsrankingIn5 + "</td><td class=\"textLeft\" colspan=\"2\"style=\"font-weight:bold;\">如是请填写以下：排名在</td><td class=\"textRight\">" + RankingType + "</td><td class=\"textRight\"  colspan=\"3\">" + "第&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["Ranking"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;名" + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">按供应商规模和经营品种分类</td><td class=\"textRight\" colspan=\"2\">" + dt4.Rows[0]["ScaleType"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">产品质量执行标准</td><td class=\"textRight\" >" + dt5.Rows[0]["QualityStandard"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">拟购产品的生产规模(年产量/年产值)</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["AnnualOutput"].ToString() + "&nbsp;&nbsp;/&nbsp;&nbsp;" + dt.Rows[0]["AnnualOutputValue"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">针对拟为燕山公司提供的产品或服务，近三年主要客户名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["MainClient"].ToString() + "</td><td class=\"textLeft\" colspan=\"2\"style=\"font-weight:bold;\">针对拟为燕山公司提供的产品或服务，近三年业绩</td><td class=\"textRight\"  colspan=\"4\">" + dt.Rows[0]["Achievement"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">代理产品授权证明</td><td class=\"textRight\" colspan=\"2\">" + HasAuthorization + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">产品生产厂家的全套图纸</td><td class=\"textRight\"  colspan=\"5\">" + HasDrawing + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">代理产品国内所属级别</td><td class=\"textRight\" colspan=\"2\">" + dt2.Rows[0]["AgentClass"].ToString() + "</td><td class=\"textLeft\"style=\"font-weight:bold;\">能够提供代理进口产品进货证明材料</td><td class=\"textRight\"  colspan=\"5\">" + dt.Rows[0]["HasImportMaterial"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\"style=\"font-weight:bold;\">传真</td><td class=\"textRight\" colspan=\"8\">" + dt.Rows[0]["FAX"].ToString() + "</td></tr>");
            #endregion
            #region 联系人
            int rowSpan = dtConPerson.Rows.Count + 1;
            int row1 = dtcer.Rows.Count + 1;
            int row2 = dtfile.Rows.Count + 1;
            int row3 = dtaward.Rows.Count + 1;
            int row4 = dtprice.Rows.Count + 1;
            int row5 = dtproduct.Rows.Count + 1;
            int row6 = dtserver.Rows.Count + 1;
            if (dtConPerson.Rows.Count == 0)
                rowSpan = 2;
            if (dtcer.Rows.Count == 0)
                row1 = 2;
            if (dtfile.Rows.Count == 0)
                row2 = 2;
            if (dtaward.Rows.Count == 0)
                row3 = 2;
            if (dtprice.Rows.Count == 0)
                row4 = 2;
            if (dtproduct.Rows.Count == 0)
                row5 = 2;
            if (dtserver.Rows.Count == 0)
                row6 = 2;

            sb.Append("<tr><td class=\"textLeft\" rowspan=" + rowSpan + " style=\"font-weight:bold;\">供应商联系人</td><td class=\"textLeft\" >职责部门</td><td class=\"textLeft\" >姓名</td><td class=\"textLeft\">部门</td><td class=\"textLeft\" >职位</td><td class=\"textLeft\">座机</td><td class=\"textLeft\">手机</td><td class=\"textLeft\" colspan=\"2\">邮箱</td></tr>");
            if (dtConPerson.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int i = 0; i < dtConPerson.Rows.Count; i++)
            {
                sb.Append("<tr><td class=\"textRight\" >" + dtConPerson.Rows[i]["FDepartment"].ToString() + "</td><td class=\"textRight\"  >" + dtConPerson.Rows[i]["PName"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Department"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Job"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Phone"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Mobile"].ToString() + "</td><td class=\"textRight\" colspan=\"2\" >" + dtConPerson.Rows[i]["Email"].ToString() + "</td></tr>");
            }
            #endregion
            #region 证书信息
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row1 + " style=\"font-weight:bold;\">供应商证书</td><td class=\"textLeft\" >证书类型</td><td class=\"textLeft\" >证书名称</td><td class=\"textLeft\">证书编号</td><td class=\"textLeft\">认证机构名称</td><td class=\"textLeft\" >证书文档名称</td><td class=\"textLeft\" colspan=\"3\">操作</td></tr>");
            if (dtcer.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int j = 0; j < dtcer.Rows.Count; j++)
            {
                sb.Append("<tr><td class=\"textRight\"  >" + dtcer.Rows[j]["CType"].ToString() + "</td><td class=\"textRight\">" + dtcer.Rows[j]["CName"].ToString() + "</td><td class=\"textRight\" >" + dtcer.Rows[j]["CCode"].ToString() + "</td><td class=\"textRight\" >" + dtcer.Rows[j]["COrganization"].ToString() + "</td><td class=\"textRight\" >" + dtcer.Rows[j]["CFileName"].ToString() + "</td><td class=\"textRight\" colspan=\"3\" ><a onclick='DownLoadCertify(\"" + dtcer.Rows[j]["FID"].ToString() + "\",\"" + dtcer.Rows[j]["CFileName"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            #region 资质信息
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row2 + " style=\"font-weight:bold;\">供应商资质</td><td class=\"textLeft\" >资质类型</td><td class=\"textLeft\" >资质具体项</td><td class=\"textLeft\">资质证书具体项</td><td class=\"textLeft\" >其他项说明</td><td class=\"textLeft\">资质文档名称</td><td class=\"textLeft\" colspan=\"3\" >操作</td></tr>");
            if (dtfile.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");

            for (int a = 0; a < dtfile.Rows.Count; a++)
            {

                sb.Append("<tr><td class=\"textRight\"  >" + dtfile.Rows[a]["FType"].ToString() + "</td><td class=\"textRight\">" + dtfile.Rows[a]["TypeO"].ToString() + "</td><td class=\"textRight\" >" + dtfile.Rows[a]["Item"].ToString() + "</td><td class=\"textRight\" >" + dtfile.Rows[a]["ItemO"].ToString() + "</td><td class=\"textRight\"  >" + dtfile.Rows[a]["FFileName"].ToString() + "</td><td class=\"textRight\" colspan=\"3\" ><a onclick='DownLoadFile(\"" + dtfile.Rows[a]["FID"].ToString() + "\",\"" + dtfile.Rows[a]["FFileName"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            #region 曾获奖项信息
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row3 + " style=\"font-weight:bold;\">曾获奖项信息</td><td class=\"textLeft\" colspan=\"5\" >奖项名称</td><td class=\"textLeft\" colspan=\"3\" >操作</td></tr>");
            if (dtaward.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int b = 0; b < dtaward.Rows.Count; b++)
            {
                sb.Append("<tr><td class=\"textRight\" colspan=\"5\" >" + dtaward.Rows[b]["Award"].ToString() + "</td><td class=\"textRight\" colspan=\"3\" ><a onclick='DownLoadAwards(\"" + dtaward.Rows[b]["ID"].ToString() + "\",\"" + dtaward.Rows[b]["Award"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            #region 报价/比价单
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row4 + " style=\"font-weight:bold;\">报价/比价单信息</td><td class=\"textLeft\" colspan=\"5\" >报价/比价单信息</td><td class=\"textLeft\" colspan=\"3\" >操作</td></tr>");
            if (dtprice.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int c = 0; c < dtprice.Rows.Count; c++)
            {
                sb.Append("<tr><td class=\"textRight\" colspan=\"5\">" + dtprice.Rows[c]["PriceName"].ToString() + "</td><td class=\"textRight\" colspan=\"3\" ><a onclick='DownLoadjiage(\"" + dtprice.Rows[c]["ID"].ToString() + "\",\"" + dtprice.Rows[c]["PriceName"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            #region 产品信息
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row5 + " style=\"font-weight:bold;\">产品信息</td><td class=\"textLeft\" >产品分类</td><td class=\"textLeft\" >产品编号</td><td class=\"textLeft\">产品名称</td><td class=\"textLeft\" >规格类型</td><td class=\"textLeft\">价格</td><td class=\"textLeft\"  >产地</td><td class=\"textLeft\"  >产品文档名称</td><td class=\"textLeft\" >操作</td></tr>");
            if (dtproduct.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int d = 0; d < dtproduct.Rows.Count; d++)
            {
                sb.Append("<tr><td class=\"textRight\" >" + dtproduct.Rows[d]["Ptype"].ToString() + "</td><td class=\"textRight\"  >" + dtproduct.Rows[d]["ProductID"].ToString() + "</td><td class=\"textRight\">" + dtproduct.Rows[d]["ProductName"].ToString() + "</td><td class=\"textRight\">" + dtproduct.Rows[d]["Standard"].ToString() + "</td><td class=\"textRight\">" + dtproduct.Rows[d]["Price"].ToString() + "</td><td class=\"textRight\">" + dtproduct.Rows[d]["OriginPlace"].ToString() + "</td><td class=\"textRight\">" + dtproduct.Rows[d]["FFileName"].ToString() + "</td><td class=\"textRight\" ><a onclick='DownLoadproduct(\"" + dtproduct.Rows[d]["ID"].ToString() + "\",\"" + dtproduct.Rows[d]["FFileName"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            #region 服务信息
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + row6 + " style=\"font-weight:bold;\">服务信息</td><td class=\"textLeft\"colspan=\"2\" >服务名称</td><td class=\"textLeft\"  >服务描述</td><td class=\"textLeft\" >用途</td><td class=\"textLeft\" >服务文档名称</td><td class=\"textLeft\" colspan=\"3\" >操作</td></tr>");
            if (dtserver.Rows.Count == 0)
                sb.Append("<tr><td class=\"textRight\" colspan=\"8\" style='text-align:center;' >无数据显示</td></tr>");
            for (int f = 0; f < dtserver.Rows.Count; f++)
            {
                sb.Append("<tr><td class=\"textRight\" colspan=\"2\" >" + dtserver.Rows[f]["ServiceName"].ToString() + "</td><td class=\"textRight\">" + dtserver.Rows[f]["ServiceDesc"].ToString() + "</td><td class=\"textRight\" >" + dtserver.Rows[f]["Purpose"].ToString() + "</td><td class=\"textRight\" >" + dtserver.Rows[f]["FFileName"].ToString() + "</td><td class=\"textRight\" colspan=\"3\" ><a onclick='DownLoadserver(\"" + dtserver.Rows[f]["ServiceID"].ToString() + "\",\"" + dtserver.Rows[f]["FFileName"].ToString() + "\")' style='color:blue;cursor:pointer;'>下载</a></td></tr>");
            }
            #endregion
            sb.Append("</table></div>");
            Response.Write(sb.ToString());
            #endregion
            return View();
        }
        /// <summary>
        /// 评审详细
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult DetailPS(string sid)
        {
            StringBuilder sb = new StringBuilder();
            //基础表
            DataTable dt = SupplyManage.GetDetailID(sid);//只有一条数据
            //负责人意见表
            DataTable dt2 = SupplyManage.GetSugestion(sid);
            //审批表
            DataTable dt3 = SupplyManage.GetApproval2(sid);
            //供应商为
            DataTable dt4 = SupplyManage.GetUnites(sid);

            #region 数字替换
            string IsUnReview = dt.Rows[0]["IsUnReview"].ToString();
            string UnReviewDesc = "";
            string UnReviewUnit = "";
            if (IsUnReview == "0")
            {
                IsUnReview = "是";
                UnReviewUnit = dt4.Rows[0]["UnReviewUnit"].ToString();
                UnReviewDesc = dt.Rows[0]["UnReviewDesc"].ToString();
            }
            else if (IsUnReview == null || IsUnReview == "")
            {
                IsUnReview = null;
                UnReviewUnit = null;
                UnReviewDesc = null;
            }
            else
            {
                IsUnReview = "否";
                UnReviewUnit = null;
                UnReviewDesc = null;
            }
            string URConfirmUser = dt.Rows[0]["URConfirmUser"].ToString();
            if (URConfirmUser == "0")
                URConfirmUser = "是";
            else if (URConfirmUser == null || URConfirmUser == "") URConfirmUser = null;
            else URConfirmUser = "否";
            string IsURInnerUnit = dt.Rows[0]["IsURInnerUnit"].ToString();
            if (IsURInnerUnit == "0")
                IsURInnerUnit = "是";
            else if (IsURInnerUnit == null || IsURInnerUnit == "") IsURInnerUnit = null;
            else IsURInnerUnit = "否";
            string Evaluation5 = dt.Rows[0]["Evaluation5"].ToString();
            if (Evaluation5 == "0") Evaluation5 = "是"; else if (Evaluation5 == null || Evaluation5 == "") Evaluation5 = null; else Evaluation5 = "否";
            string SState = "";
            string SContent = "";
            string stime = "";
            if (dt2.Rows.Count >= 1)
            {
                SState = dt2.Rows[0]["SState"].ToString();
                if (SState.Contains('0'))
                {
                    SState = "是";
                }
                else
                {
                    SState = "否";
                }
                SContent = dt2.Rows[0]["SContent"].ToString();
                stime = dt2.Rows[0]["SCreate"].ToString();
            }
            else
            {
                SState = "";
                SContent = "";
                stime = "";
            }
            string IsPass = "";
            string Opinions = "";
            string Remark = "";
            string rtime = "";
            if (dt3.Rows.Count >= 1)
            {
                IsPass = dt3.Rows[0]["IsPass"].ToString();//判断为空的条件下
                Opinions = dt3.Rows[0]["Opinion"].ToString();
                Remark = dt3.Rows[0]["Remark"].ToString();
                rtime = dt3.Rows[0]["ApprovalTime"].ToString();
            }
            else
            {
                IsPass = null;
                Opinions = null;
                Remark = null;
                rtime = "";
            }
            string ResState = dt.Rows[0]["ResState"].ToString();
            if (ResState == "0") ResState = "是"; else if (ResState == null || ResState == "") ResState = null; else ResState = "否";
            #endregion

            #region 内部评审内容

            sb.Append("<div  id=\"tabTitile\"style=\"font-size:14px;margin-left:10px;overflow-y:scroll;\">流水号：" + sid + "--" + dt.Rows[0]["COMNameC"].ToString() + "</div><div style ='width:1000px;height:600px;overflow-x: scroll; overflow-y: scroll;'>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:1000px;margin-top:10px;margin-left:10px;line-height: 30px;overflow-y: scroll;\">");
            sb.Append("<tr><td colspan=\"6\" style=\"font-size:15px;font-weight: bold;\">供应商评审流程</td></tr>");
            sb.Append("<tr><td  colspan=\"6\"><h4>内部评审" + dt.Rows[0]["CreateTime"].ToString() + "</h4></td></tr>");
            sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">是否是免评供应商</td><td class=\"textRight\" >" + IsUnReview + "</td><td class=\"textLeft\"style=\"width:20%;\">是否为集团合格供应商</td><td class=\"textRight\"  colspan=\"3\" >" + URConfirmUser + "</td></tr>");
            sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\" >项目经理名称</td><td class=\"textRight\" >" + dt.Rows[0]["IsUnreviewUser"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">免评审供应商是否为内部单位</td><td class=\"textRight\"  >" + IsURInnerUnit + "</td><td class=\"textLeft\" style=\"width:20%;\">供应商为</td><td class=\"textRight\" >" + UnReviewUnit + "</td></tr>");
            sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\">已存在资质证明</td><td colspan=\"5\" style=\"border: 1px solid #d1d1d1;\"><div id=\"unit\"></div></td></tr>");
            sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">对供方产品(工程)质量、技术能力的评价</td><td class=\"textRight\">" + dt.Rows[0]["Evaluation3"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">对供方合作意愿的评价</td><td class=\"textRight\"  >" + dt.Rows[0]["Evaluation4"] + "</td><td class=\"textLeft\" style=\"width:20%;\">对供应商信誉评价</td><td class=\"textRight\"  >" + dt.Rows[0]["Evaluation1"] + "</td></tr>");
            sb.Append("<tr style=\"border: 1px solid \"> <td class=\"textLeft\" style=\"width:20%;\">对供应商价格水平的评价</td><td class=\"textRight\" >" + dt.Rows[0]["Evaluation2"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">是否对供方进行实地考察</td><td class=\"textRight\" >" + Evaluation5 + "</td><td class=\"textLeft\" style=\"width:20%;\">考察评价</td><td class=\"textRight\">" + dt.Rows[0]["Evaluation6"].ToString() + "</td></tr>");

            #endregion
            #region 负责人意见内容
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["SState"].ToString().Contains('0'))
                {
                    dt2.Rows[i]["SState"] = '是';
                }
                else
                {
                    dt2.Rows[i]["SState"] = '否';
                }
                sb.Append("<tr><td  colspan=\"6\"><h4>部门负责人审批" + dt2.Rows[i]["SCreate"].ToString() + "</h4></td></tr>");
                sb.Append("<tr><td class=\"textLeft\" style=\"width:20%;\">是否同意通过内部评审</td><td class=\"textRight\">" + dt2.Rows[i]["SState"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">内部负责人意见</td><td class=\"textRight\" colspan=\"3\">" + dt2.Rows[i]["SContent"].ToString() + "</td></tr>");
            }
            #endregion
            #region MyRegion
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    if (dt2.Rows[i]["SState"].ToString() == "0")
            //    {
            //        #region MyRegion
            //        sb.Append("<tr><td  colspan=\"6\"><h4>内部评审" + dt.Rows[i]["CreateTime"].ToString() + "</h4></td></tr>");
            //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">是否是免评供应商</td><td class=\"textRight\" >" + IsUnReview + "</td><td class=\"textLeft\"style=\"width:20%;\">是否为集团合格供应商</td><td class=\"textRight\"  colspan=\"3\" >" + URConfirmUser + "</td></tr>");
            //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\" >项目经理名称</td><td class=\"textRight\" >" + dt.Rows[i]["IsUnreviewUser"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">免评审供应商是否为内部单位</td><td class=\"textRight\"  >" + IsURInnerUnit + "</td><td class=\"textLeft\" style=\"width:20%;\">供应商为</td><td class=\"textRight\" >" + UnReviewUnit + "</td></tr>");
            //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\">已存在资质证明</td><td colspan=\"5\" style=\"border: 1px solid #d1d1d1;\"><div id=\"unit\"></div></td></tr>");
            //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">对供方产品(工程)质量、技术能力的评价</td><td class=\"textRight\">" + dt.Rows[i]["Evaluation3"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">对供方合作意愿的评价</td><td class=\"textRight\"  >" + dt.Rows[i]["Evaluation4"] + "</td><td class=\"textLeft\" style=\"width:20%;\">对供应商信誉评价</td><td class=\"textRight\"  >" + dt.Rows[i]["Evaluation1"] + "</td></tr>");
            //        sb.Append("<tr style=\"border: 1px solid \"> <td class=\"textLeft\" style=\"width:20%;\">对供应商价格水平的评价</td><td class=\"textRight\" >" + dt.Rows[i]["Evaluation2"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">是否对供方进行实地考察</td><td class=\"textRight\" >" + Evaluation5 + "</td><td class=\"textLeft\" style=\"width:20%;\">考察评价</td><td class=\"textRight\">" + dt.Rows[i]["Evaluation6"].ToString() + "</td></tr>");
            //        #endregion
            //    }
            //} 
            #endregion
            #region 会签审批
            //判断如果内部评审状态是 未通过下不显示
            for (int j = 0; j < dt3.Rows.Count; j++)
            {
                if (SState != "否")
                {
                    sb.Append("<tr><td  colspan=\"6\"><h4>会签评审" + dt3.Rows[j]["ApprovalTime"].ToString() + "</h4></td></tr>");
                    sb.Append("<tr><td class=\"textLeft\" style=\"width:20%;\">是否通过审批</td><td class=\"textRight\">" + dt3.Rows[j]["IsPass"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">审批意见</td><td class=\"textRight\" >" + dt3.Rows[j]["Opinion"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">审批人</td><td class=\"textRight\" >" + dt3.Rows[j]["Remark"].ToString() + "</td></tr>");
                }
                #region MyRegion
                //else
                //{
                //    #region 内部评审
                //    for (int l = 0; l < dt.Rows.Count; l++)
                //    {
                //        sb.Append("<tr><td  colspan=\"6\"><h4>内部评审" + dt.Rows[0]["CreateTime"].ToString() + "</h4></td></tr>");
                //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">是否是免评供应商</td><td class=\"textRight\" >" + IsUnReview + "</td><td class=\"textLeft\"style=\"width:20%;\">是否为集团合格供应商</td><td class=\"textRight\"  colspan=\"3\" >" + URConfirmUser + "</td></tr>");
                //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\" >项目经理名称</td><td class=\"textRight\" >" + dt.Rows[0]["IsUnreviewUser"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">免评审供应商是否为内部单位</td><td class=\"textRight\"  >" + IsURInnerUnit + "</td><td class=\"textLeft\" style=\"width:20%;\">供应商为</td><td class=\"textRight\" >" + UnReviewUnit + "</td></tr>");
                //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\">已存在资质证明</td><td colspan=\"5\" style=\"border: 1px solid #d1d1d1;\"><div id=\"unit\"></div></td></tr>");
                //        sb.Append("<tr style=\"border: 1px solid \"><td class=\"textLeft\" style=\"width:20%;\">对供方产品(工程)质量、技术能力的评价</td><td class=\"textRight\">" + dt.Rows[0]["Evaluation3"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">对供方合作意愿的评价</td><td class=\"textRight\"  >" + dt.Rows[0]["Evaluation4"] + "</td><td class=\"textLeft\" style=\"width:20%;\">对供应商信誉评价</td><td class=\"textRight\"  >" + dt.Rows[0]["Evaluation1"] + "</td></tr>");

                //        sb.Append("<tr style=\"border: 1px solid \"> <td class=\"textLeft\" style=\"width:20%;\">对供应商价格水平的评价</td><td class=\"textRight\" >" + dt.Rows[0]["Evaluation2"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">是否对供方进行实地考察</td><td class=\"textRight\" >" + Evaluation5 + "</td><td class=\"textLeft\" style=\"width:20%;\">考察评价</td><td class=\"textRight\">" + dt.Rows[0]["Evaluation6"].ToString() + "</td></tr>");
                //    }
                //    #endregion
                //} 
                #endregion
            }
            #endregion
            #region 最终评审内容
            if (SState != "否" && IsPass != "否")
            {
                sb.Append("<tr><td  colspan=\"6\"><h4>最终审批" + dt.Rows[0]["AppTime"].ToString() + "</h4></td></tr>");
                sb.Append("<tr><td class=\"textLeft\" style=\"width:20%;\">是否通过最终评审</td><td class=\"textRight\">" + ResState + "</td><td class=\"textLeft\" style=\"width:20%;\">最终意见</td><td class=\"textRight\" >" + dt.Rows[0]["ApprovalRes"].ToString() + "</td><td class=\"textLeft\" style=\"width:20%;\">签字人</td><td class=\"textRight\" >" + dt.Rows[0]["Approval4User"].ToString() + "</td></tr>");
            }
            #endregion
            sb.Append("</table></div>");
            Response.Write(sb.ToString());
            return View();
        }
        /// <summary>
        /// 合格供应商打印显示品种分类
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult ScalType(string sid)
        {
            DataTable dt = SupplyManage.getScaleType(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        /// <summary>
        /// 合格供应商打印产品质量执行标准
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult getQualityStandard(string sid)
        {
            DataTable dt = SupplyManage.getQualityStandard(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        /// <summary>
        /// 合格供应商打印供需关系
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult getReation(string sid)
        {
            DataTable dt = SupplyManage.getReation(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        /// <summary>
        /// 合格供应商打印代理级别
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult getAgenclass(string sid)
        {
            DataTable dt = SupplyManage.getAgenclass(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult getBusinessDistribute(string sid)
        {
            DataTable dt = SupplyManage.getBusinessDistribute(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        public ActionResult getbillWay(string sid)
        {
            DataTable dt = SupplyManage.getbillWay(sid);
            if (dt == null)
                return Json(new { success = false });
            else
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
        }
        /// <summary>
        /// 客户信息详细展示
        /// </summary>
        /// <param name="kid"></param>
        /// <returns></returns>
        public ActionResult DetailCustomer(string kid)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = SupplyManage.GetDetailCustometerID(kid);//基本信息表
            DataTable unit = SupplyManage.GetUnite(kid);//共享部门
            string time = Convert.ToDateTime(dt.Rows[0]["GainDate"]).ToString("yyyy-MM-dd");
            sb.Append("<div  id=\"tabTitile\"style=\"font-size:15px;\">客户编号：" + kid + "</div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\">获得客户时间</td><td class=\"textRight\" colspan=\"2\">" + time + "</td><td class=\"textLeft\">填报单位</td><td class=\"textRight\">" + dt.Rows[0]["DeclareUnit"].ToString() + "</td><td class=\"textLeft\">填报人</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["DeclareUser"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">负责人</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ChargeUser"].ToString() + "</td><td class=\"textLeft\">是否共享</td><td class=\"textRight\">" + dt.Rows[0]["IsShare"].ToString() + "</td><td class=\"textLeft\">共享单位</td><td class=\"textRight\"  colspan=\"2\">" + unit.Rows[0]["ShareUnits"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">客户名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["CName"].ToString() + "</td><td class=\"textLeft\">客户简称</td><td class=\"textRight\">" + dt.Rows[0]["CShortName"].ToString() + "</td><td class=\"textLeft\">所属产业</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["Industry"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">人员规模</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["StaffSize"].ToString() + "</td><td class=\"textLeft\">意向产品</td><td class=\"textRight\">" + dt.Rows[0]["Products"].ToString() + "</td><td class=\"textLeft\">客户座机</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["Phone"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">传真</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["FAX"].ToString() + "</td><td class=\"textLeft\">邮编</td><td class=\"textRight\">" + dt.Rows[0]["ZipCode"].ToString() + "</td><td class=\"textLeft\">公司网址</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["COMWebsite"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">公司地址</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ComAddress"].ToString() + "</td><td class=\"textLeft\">所属省份</td><td class=\"textRight\">" + dt.Rows[0]["Province"].ToString() + "</td><td class=\"textLeft\">所属城市</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["City"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">客户介绍描述</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ClientDesc"].ToString() + "</td><td class=\"textLeft\">备注</td><td class=\"textRight\">" + dt.Rows[0]["Remark"].ToString() + "</td><td class=\"textLeft\">客户类型</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["CType"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">客户等级</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["CClass"].ToString() + "</td><td class=\"textLeft\">客户来源</td><td class=\"textRight\">" + dt.Rows[0]["CSource"].ToString() + "</td><td class=\"textLeft\">客户关系</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["CRelation"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">成熟度</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Maturity"].ToString() + "</td><td class=\"textLeft\">状态</td><td class=\"textRight\">" + dt.Rows[0]["State"].ToString() + "</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            return View();
        }
        /// <summary>
        /// 准入评审的详细
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult DetailInfomation(string sid)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = SupplyManage.GetDetailMsg(sid);
            DataTable dt2 = SupplyManage.getAgenclass(sid);
            DataTable dt3 = SupplyManage.getReation(sid);
            DataTable dt4 = SupplyManage.getScaleType(sid);
            DataTable dt5 = SupplyManage.getQualityStandard(sid);
            DataTable dt6 = SupplyManage.getBusinessDistribute(sid);
            DataTable dt7 = SupplyManage.getbillWay(sid);
            DataTable dtConPerson = SupplyManage.GetDetailConPerson(sid);
            #region 汉字替换数字
            string time = Convert.ToDateTime(dt.Rows[0]["DeclareDate"]).ToString("yyyy-MM-dd");
            string time2 = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"]).ToString("yyyy-MM-dd");
            string HasDrawing = dt.Rows[0]["HasDrawing"].ToString();//0-1

            if (HasDrawing == "0")
            {
                HasDrawing = "有";
            }
            else
            {
                HasDrawing = "无";
            }
            string HasAuthorization = dt.Rows[0]["HasAuthorization"].ToString();
            if (HasAuthorization == "0")
            {
                HasAuthorization = "是";
            }
            else
            {
                HasAuthorization = "否";
            }
            string IsrankingIn5 = dt.Rows[0]["IsrankingIn5"].ToString();
            if (IsrankingIn5 == "0")
            {
                IsrankingIn5 = "是";
            }
            else
            {
                IsrankingIn5 = "否";
            }
            string IsCooperate = dt.Rows[0]["IsCooperate"].ToString();
            if (IsCooperate == "0")
            {
                IsCooperate = "是";
            }
            else
            {
                IsCooperate = "否";
            }
            string HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
            if (HasRegulation == "0")
            {
                HasRegulation = "是";
            }
            else
            {
                HasRegulation = "否";
            }
            string RankingType = dt.Rows[0]["RankingType"].ToString();
            if (RankingType == "0")
            {
                RankingType = "国内";
            }
            else
            {
                RankingType = "国际";
            }
            #endregion
            #region 截取字符串
            string BusinessDistribute = dt.Rows[0]["BusinessDistribute"].ToString();
            string[] bd = BusinessDistribute.Split(':');
            string one = bd[0].ToString();
            string two = bd[1].ToString();
            for (int i = 0; i < two.Length; i++)
            {
                BusinessDistribute = two.ToString();
            }
            string BillingWay = dt.Rows[0]["BillingWay"].ToString();
            string[] bl = BillingWay.Split(':');
            string bl1 = bl[0].ToString();
            string bl2 = bl[1].ToString();
            for (int i = 0; i < bl2.Length; i++)
            {
                BillingWay = "," + bl2.ToString();
            }
            #endregion
            #region 拼接字符串

            sb.Append("<div  id=\"tabTitile\"style=\"font-size:15px;\">流水号：" + sid + "</div>");
            sb.Append("<table id=\"tab\" class = \"tabInfo\" style=\"width:97%;margin-top:10px;margin-left:10px;\">");
            sb.Append("<tr><td class=\"textLeft\">填报部门</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["DeclareUnitID"].ToString() + "</td><td class=\"textLeft\">填报日期</td><td class=\"textRight\"  colspan=\"4\">" + time + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">供应商类别</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["SupplierType"].ToString() + "</td><td class=\"textLeft\">其他</td><td class=\"textRight\">" + dt.Rows[0]["OtherType"].ToString() + "</td><td class=\"textLeft\">公司名称(中)</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMNameC"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">公司简称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMShortName"].ToString() + "</td><td class=\"textLeft\">公司网址</td><td class=\"textRight\">" + dt.Rows[0]["COMWebsite"].ToString() + "</td><td class=\"textLeft\">所属地区</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMArea"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">公司名称(英)</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMNameE"].ToString() + "</td><td class=\"textLeft\">所属国家</td><td class=\"textRight\">" + dt.Rows[0]["COMCountry"].ToString() + "</td><td class=\"textLeft\">公司注册地址</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ComAddress"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">公司创建日期</td><td class=\"textRight\" colspan=\"2\">" + time2 + "</td><td class=\"textLeft\">税务登记号</td><td class=\"textRight\">" + dt.Rows[0]["TaxRegistrationNo"].ToString() + "</td><td class=\"textLeft\">营业执照号码</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["BusinessLicenseNo"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">公司办公地址</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ComAddress"].ToString() + "</td><td class=\"textLeft\">法人代表</td><td class=\"textRight\">" + dt.Rows[0]["COMLegalPerson"].ToString() + "</td><td class=\"textLeft\">公司出货/出厂地址</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMFactoryAddress"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">工厂面积</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMFactoryArea"].ToString() + "</td><td class=\"textLeft\">机构组织代码</td><td class=\"textRight\">" + dt.Rows[0]["OrganizationCode"].ToString() + "</td><td class=\"textLeft\">供应商集团名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["COMGroup"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">注册资金</td><td class=\"textRight\"  colspan=\"2\">" + dt.Rows[0]["RegisteredCapital"].ToString() + dt.Rows[0]["CapitalUnit"].ToString() + "</td><td class=\"textLeft\">是否与燕山公司合作过</td><td class=\"textRight\">" + IsCooperate + "</td><td class=\"textLeft\">开户行名称</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["BankName"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">银行基本账号</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["BankAccount"].ToString() + "</td><td class=\"textLeft\">公司总人数</td><td class=\"textRight\">" + dt.Rows[0]["StaffNum"].ToString() + "</td><td class=\"textLeft\">企业类型</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["EnterpriseType"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">业务分布</td><td class=\"textRight\" colspan=\"2\">" + dt6.Rows[0]["BusinessDistribute"].ToString() + ':' + BusinessDistribute + "</td><td class=\"textLeft\">开票方式</td><td class=\"textRight\">" + dt7.Rows[0]["BillingWay"].ToString() + ':' + BillingWay + "</td><td class=\"textLeft\">去年营业额</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["Turnover"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">员工状况</td><td class=\"textRight\" colspan=\"2\">研发人员：" + dt.Rows[0]["DevelopStaffs"].ToString() + "</td><td class=\"textRight\" >" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;质量人员:" + "</td><td class=\"textRight\">" + dt.Rows[0]["QAStaffs"].ToString() + "</td><td class=\"textRight\">生产人员:</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ProduceStaffs"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">供需关系</td><td class=\"textRight\" colspan=\"2\">" + dt3.Rows[0]["Relation"].ToString() + "</td><td class=\"textLeft\">是否有健全的组织机构和内部管理制度</td><td class=\"textRight\">" + HasRegulation + "</td><td class=\"textLeft\">购买产品生产线数量</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["ProductLineNum"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">正常工作时间 </td><td class=\"textRight\" colspan=\"2\">" + "AM &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["WorkTime_Start"].ToString() + "&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;TO" + "</td><td class=\"textRight\">" + dt.Rows[0]["WorkTime_End"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;PM " + "</td><td class=\"textLeft\">正常工作日</td><td class=\"textRight\" colspan=\"3\" >" + dt.Rows[0]["WorkDay_Start"].ToString() + "&nbsp;&nbsp;至&nbsp;&nbsp;" + dt.Rows[0]["WorkDay_End"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">经营范围</td><td class=\"textRight\" colspan=\"7\">" + dt.Rows[0]["BusinessScope"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">拟购产品行业排名是否前五</td><td class=\"textRight\"  colspan=\"2\">" + IsrankingIn5 + "</td><td class=\"textLeft\" colspan=\"2\">如是请填写以下：排名在</td><td class=\"textRight\">" + RankingType + "</td><td class=\"textRight\" colspan=\"2\" >" + "第&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["Ranking"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;名" + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">按供应商规模和经营品种分类</td><td class=\"textRight\" colspan=\"2\">" + dt4.Rows[0]["ScaleType"].ToString() + "</td><td class=\"textLeft\">产品质量执行标准</td><td class=\"textRight\" >" + dt5.Rows[0]["QualityStandard"].ToString() + "</td><td class=\"textLeft\">拟购产品的生产规模(年产量/年产值)</td><td class=\"textRight\" colspan=\"2\">" + dt.Rows[0]["AnnualOutput"].ToString() + "&nbsp;&nbsp;/&nbsp;&nbsp;" + dt.Rows[0]["AnnualOutputValue"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">针对拟为燕山公司提供的产品或服务，近三年主要客户名称</td><td class=\"textRight\" colspan=\"3\">" + dt.Rows[0]["MainClient"].ToString() + "</td><td class=\"textLeft\">针对拟为燕山公司提供的产品或服务，近三年业绩</td><td class=\"textRight\"  colspan=\"3\">" + dt.Rows[0]["Achievement"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">代理产品授权证明</td><td class=\"textRight\" colspan=\"2\">" + HasAuthorization + "</td><td class=\"textLeft\">产品生产厂家的全套图纸</td><td class=\"textRight\"  colspan=\"4\">" + HasDrawing + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">代理产品国内所属级别</td><td class=\"textRight\" colspan=\"2\">" + dt2.Rows[0]["AgentClass"].ToString() + "</td><td class=\"textLeft\">能够提供代理进口产品进货证明材料</td><td class=\"textRight\"  colspan=\"4\">" + dt.Rows[0]["HasImportMaterial"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\">曾获奖项</td><td class=\"textRight\" colspan=\"7\">" + dt.Rows[0]["Award"].ToString() + "</td></tr>");
            sb.Append("<tr><td class=\"textLeft\" rowspan=" + (dtConPerson.Rows.Count + 1) + ">供应商联系人</td><td class=\"textLeft\" >职责部门</td><td class=\"textLeft\" >姓名</td><td class=\"textLeft\">部门</td><td class=\"textLeft\" >职位</td><td class=\"textLeft\">座机</td><td class=\"textLeft\">手机</td><td class=\"textLeft\">邮箱</td></tr>");
            for (int i = 0; i < dtConPerson.Rows.Count; i++)
            {

                if (i == 0)
                    sb.Append("<tr><td class=\"textRight\"  >" + dtConPerson.Rows[0]["FDepartment"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[0]["PName"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[0]["Department"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[0]["Job"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[0]["Phone"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[0]["Mobile"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[0]["Email"].ToString() + "</td></tr>");
                else
                    sb.Append("<tr><td class=\"textRight\" >" + dtConPerson.Rows[i]["FDepartment"].ToString() + "</td><td class=\"textRight\"  >" + dtConPerson.Rows[i]["PName"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Department"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Job"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Phone"].ToString() + "</td><td class=\"textRight\">" + dtConPerson.Rows[i]["Mobile"].ToString() + "</td><td class=\"textRight\" >" + dtConPerson.Rows[i]["Email"].ToString() + "</td></tr>");
            }

            sb.Append("</table>");
            Response.Write(sb.ToString());
            #endregion
            return View();
        }
        /// <summary>
        /// 暂停或淘汰供应商详情
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult DetailApp(string name, string sid)
        {
            string where = " and d.SID = '" + sid + "'";
            DataTable dt = SupplyManage.getNewDetailApp(where);//获得供应商基本信息
            DataTable dtLog = SupplyManage.getLog(sid);//获取日志信息
            DataTable dt3 = SupplyManage.getReation(sid);
            //DataTable dt6 = SupplyManage.getBusinessDistribute(sid);
            //DataTable dt7 = SupplyManage.getbillWay(sid);
            DataTable dt8 = SupplyManage.getBillHanzi();//数字替换汉字
            DataTable dt9 = SupplyManage.getBuinessHanzi();//数字替换汉字

            StringBuilder sb = new StringBuilder();
            string time = Convert.ToDateTime(dt.Rows[0]["COMCreateDate"].ToString()).ToString("yyyy-MM-dd");
            #region 数字变汉字
            string IsCooperate = dt.Rows[0]["IsCooperate"].ToString();
            if (IsCooperate == "0")
            {
                IsCooperate = "是";
            }
            else
            {
                IsCooperate = "否";
            }
            string HasRegulation = dt.Rows[0]["HasRegulation"].ToString();
            if (HasRegulation == "0")
            {
                HasRegulation = "是";
            }
            else
            {
                HasRegulation = "否";
            }
            #endregion
            #region 逗号分隔显示
            string BusinessDistributes = dt.Rows[0]["BusinessDistribute"].ToString();
            string[] bd = BusinessDistributes.Split(':');
            string[] one = bd[0].ToString().Split(',');
            string[] two = bd[1].ToString().Split(',');
            string BusinessDistribute = "";
            string BusinessNum = "";
            for (int x = 0; x < one.Length; x++)
            {
                for (int y = x; y < dt9.Rows.Count; y++)
                {
                    if (one[x] == dt9.Rows[y]["SID"].ToString())
                        one[x] = dt9.Rows[y]["Text"].ToString();

                }
                BusinessNum += one[x] + ",";
                BusinessDistribute += two[x] + "%" + ",";
            }
            string BillingWays = dt.Rows[0]["BillingWay"].ToString();
            string[] bl = BillingWays.Split(':');
            string[] bl1 = bl[0].ToString().Split(',');
            string[] bl2 = bl[1].ToString().Split(',');
            string BillingWay = "";
            string BillingNum = "";
            for (int i = 0; i < bl1.Length; i++)
            {
                for (int j = i; j < dt8.Rows.Count; j++)
                {
                    if (bl1[i] == dt8.Rows[j]["SID"].ToString())
                        bl1[i] = dt8.Rows[j]["Text"].ToString();

                }
                BillingNum += bl1[i] + ",";
                BillingWay += bl2[i] + "%" + ",";

            }
            #endregion
            sb.Append("<div  id=\"pageContent\"style=\"font-size:12px;height:650px;overflow-y: auto;;font-weight:bold;page-break-after: always;\">流水号：" + sid + dt.Rows[0]["COMNameC"].ToString() + "");
            sb.Append("<table id=\"tab\" class = \"tabInfo\"  style=\"width:97%;overflow-y: auto;;height:650px;margin-top:5px;font-size:12px;margin-left:10px;page-break-after: always;\">");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >供应商类型</td><td  style=\"width:20%\">" + dt.Rows[0]["SupplierType"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">公司简称</td><td   style=\"width:20%\">" + dt.Rows[0]["COMShortName"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >供应商英文简称</td><td  style=\"width:20%\">" + dt.Rows[0]["COMNameE"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\">公司网址</td><td   style=\"width:20%\">" + dt.Rows[0]["comwebsite"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >公司注册地址</td><td  style=\"width:20%\">" + dt.Rows[0]["COMRAddress"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">所属国家</td><td  style=\"width:20%\">" + dt.Rows[0]["COMCountry"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >公司办公地址</td><td  style=\"width:20%\">" + dt.Rows[0]["ComAddress"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">所属城市</td><td   style=\"width:20%\">" + dt.Rows[0]["COMArea"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >公司出厂/出货地址</td><td  style=\"width:20%\">" + dt.Rows[0]["COMFactoryAddress"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >公司创办日期</td><td  style=\"width:20%\">" + time + "</td><td  style=\"width:10%;font-weight:bold;\">公司法人代表</td><td   style=\"width:20%\">" + dt.Rows[0]["COMLegalPerson"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">公司工厂面积</td><td  style=\"width:20%\">" + dt.Rows[0]["COMFactoryArea"].ToString() + "㎡" + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >税务登记号</td><td  style=\"width:20%\">" + dt.Rows[0]["TaxRegistrationNo"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">营业执照号码</td><td   style=\"width:20%\">" + dt.Rows[0]["BusinessLicenseNo"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >组织机构与代码</td><td  style=\"width:20%\">" + dt.Rows[0]["OrganizationCode"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >注册资金</td><td  style=\"width:20%\">" + dt.Rows[0]["RegisteredCapital"].ToString() + dt.Rows[0]["CapitalUnit"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">是否与燕山公司合作过</td><td  style=\"width:20%\">" + IsCooperate + "</td><td  style=\"width:10%;font-weight:bold;\">供应商集团名称</td><td   style=\"width:20%\">" + dt.Rows[0]["COMGroup"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >开户行名称</td><td  style=\"width:20%\">" + dt.Rows[0]["BankName"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">银行基本账号</td><td  style=\"width:20%\">" + dt.Rows[0]["BankAccount"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >公司总人数</td><td  style=\"width:20%\">" + dt.Rows[0]["StaffNum"].ToString() + "人" + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >业务分布</td><td  style=\"width:20%\">" + BusinessNum.Substring(0, BusinessNum.Length - 2) + ":" + BusinessDistribute.Substring(0, BusinessDistribute.Length - 3) + "</td><td  style=\"width:10%;font-weight:bold;\">开票方式</td><td   style=\"width:20%\">" + BillingNum.Substring(0, BillingNum.Length - 2) + ":" + BillingWay.Substring(0, BillingWay.Length - 3) + "</td><td  style=\"width:10%;font-weight:bold;\">企业类型</td><td   style=\"width:20%\">" + dt.Rows[0]["EnterpriseType"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >研发人员数量</td><td  style=\"width:20%\" >" + dt.Rows[0]["DevelopStaffs"].ToString() + "人" + "</td><td  style=\"width:15%;font-weight:bold;\" >质量人员数量</td><td   style=\"width:20%\" >" + dt.Rows[0]["QAStaffs"].ToString() + "人" + "</td><td  style=\"width:15%;font-weight:bold;\" >生产人员数量</td><td  style=\"width:20%\" >" + dt.Rows[0]["ProduceStaffs"].ToString() + "人" + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >是否有健全的组织机构和内部管理的规章制度</td><td  style=\"width:20%\">" + HasRegulation + "</td><td  style=\"width:10%;font-weight:bold;\">拟购产品的生产线数量</td><td   style=\"width:20%\">" + dt.Rows[0]["ProductLineNum"].ToString() + "</td><td  style=\"width:15%;font-weight:bold;\" >去年营业额</td><td  style=\"width:20%\">" + dt.Rows[0]["Turnover"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:15%;font-weight:bold;\" >传真</td><td  style=\"width:20%\">" + dt.Rows[0]["FAX"].ToString() + "</td><td  style=\"width:10%;font-weight:bold;\">经营范围</td><td   style=\"width:20%\">" + dt.Rows[0]["BusinessScope"].ToString() + "</td><td style=\"width:10%;font-weight:bold;\">供需关系</td><td   style=\"width:20%\">" + dt3.Rows[0]["Relation"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  style=\"width:10%;font-weight:bold;\">三证编号</td><td   style=\"width:20%\" colspan=\"5\">" + dt.Rows[0]["ThreeCertity"].ToString() + "</td></tr>");
            sb.Append("<tr ><td  rowspan=" + (dtLog.Rows.Count + 1) + " style=\"font-weight:bold;\">日志记录</td><td  style=\"font-weight:bold;\">日志标题</td><td style=\"font-weight:bold;\" >日志内容</td><td style=\"font-weight:bold;\">记录时间</td><td style=\"font-weight:bold;\" >记录人</td><td style=\"font-weight:bold;\">日志类型</td></tr>");
            for (int i = 0; i < dtLog.Rows.Count; i++)
            {
                if (i == 0)
                    sb.Append("<tr ><td   >" + dtLog.Rows[0]["LogTitle"].ToString() + "</td><td >" + dtLog.Rows[0]["LogContent"].ToString() + "</td><td  >" + dtLog.Rows[0]["LogTime"].ToString() + "</td><td  >" + dtLog.Rows[0]["LogPerson"].ToString() + "</td><td  >" + dtLog.Rows[0]["Type"].ToString() + "</td></tr>");
                else
                    sb.Append("<tr ><td  >" + dtLog.Rows[i]["LogTitle"].ToString() + "</td><td   >" + dtLog.Rows[i]["LogContent"].ToString() + "</td><td >" + dtLog.Rows[i]["LogTime"].ToString() + "</td><td >" + dtLog.Rows[i]["LogPerson"].ToString() + "</td><td >" + dtLog.Rows[i]["Type"].ToString() + "</td></tr>");

            }
            sb.Append("</table>");

            sb.Append("</div>");
            Response.Write(sb.ToString());
            return View();
        }
        #endregion
        #region 修改后成功与否
        /// <summary>
        /// 基本信息更新
        /// </summary>
        /// <param name="bas"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult UpdateInfo(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                Tk_SupplierBas bas = new Tk_SupplierBas();
                Tk_SContactPerson pa = new Tk_SContactPerson();
                Acc_Account acc = GAccount.GetAccountInfo();
                string Err = "";
                GSqlSentence.SetTValue<Tk_SupplierBas>(bas, HttpContext.ApplicationInstance.Context.Request);
                GSqlSentence.SetTValue<Tk_SContactPerson>(pa, HttpContext.ApplicationInstance.Context.Request);
                //添加联系人
                #region 获取联系人信息
                int taleRow = int.Parse(Request["taleRow"]);//2行以上数据
                List<Tk_SContactPerson> colPa = new List<Tk_SContactPerson>();
                for (int j = 0; j < taleRow; j++)
                {
                    if (Request["FDepartment" + j].ToString() != "")
                    {
                        pa = new Tk_SContactPerson();
                        pa.Sid = bas.Sid;
                        pa.Fdepartment = Request["FDepartment" + j].ToString();
                        pa.Pname = Request["Pname" + j].ToString();
                        pa.Department = Request["Department" + j].ToString();
                        pa.Job = Request["Job" + j].ToString();
                        pa.Phone = Request["Phone" + j].ToString();
                        pa.Mobile = Request["Mobile" + j].ToString();
                        pa.Email = Request["Email" + j].ToString();
                        pa.CreateUser = acc.UserName;
                        pa.CreateTime = DateTime.Now;
                        pa.Validate = "v";
                        colPa.Add(pa);
                    }
                }
                #endregion
                if (SupplyManage.UpdateNewBas(bas, colPa, ref Err) == true)
                    return Json(new { success = "true", Msg = "保存成功" });
                else
                    return Json(new { success = "false", Msg = "保存出错" + "--" + Err });
            }
            else
            {
                return Json(new { success = false, Msg = "供应商修改不通过" });
            }
        }
        [HttpPost]
        public ActionResult InsertSuBas()
        {
            if (ModelState.IsValid)
            {
                Tk_SupplierBas subas = new Tk_SupplierBas();
                string strErr = "";
                Acc_Account account = GAccount.GetAccountInfo();
                //反射插入数值
                GSqlSentence.SetTValue<Tk_SupplierBas>(subas, HttpContext.ApplicationInstance.Context.Request);
                subas.CreateUser = account.UserName;
                subas.CreateTime = DateTime.Now;
                subas.DeclareUnitID = account.UnitID;
                // subas.DeclareDate = DateTime.Now.ToString("yyyy-MM-dd");
                subas.Validate = "v";
                subas.ApprovalState = "0";
                subas.State = 0;
                subas.WState = 0;
                subas.NState = 0;
                subas.ResState = 1;
                //添加联系人
                int taleRow = int.Parse(Request["taleRow"]);//2行以上数据
                Tk_SContactPerson pa = new Tk_SContactPerson();
                List<Tk_SContactPerson> colPa = new List<Tk_SContactPerson>();
                pa.Fdepartment = Request["FDepartment"].ToString();//FD1,FD2
                pa.Pname = Request["PName"].ToString();
                pa.Department = Request["Department"].ToString();
                pa.Job = Request["Job"].ToString();
                pa.Phone = Request["Phone"].ToString();
                pa.Mobile = Request["Mobile"].ToString();
                pa.Email = Request["Email"].ToString();

                #region MyRegion
                string[] Fdepartment = pa.Fdepartment.Split(',');
                string[] Pname = pa.Pname.Split(',');
                string[] Department = pa.Department.Split(',');
                string[] Job = pa.Job.Split(',');
                string[] Phone = pa.Phone.Split(',');
                string[] Mobile = pa.Mobile.Split(',');
                string[] Email = pa.Email.Split(',');
                for (int j = 0; j < Fdepartment.Length; j++)
                {
                    if (Fdepartment[j].ToString() != "")
                    {

                        pa = new Tk_SContactPerson();
                        pa.Sid = subas.Sid;
                        pa.Fdepartment = Fdepartment[j].ToString();
                        pa.Pname = Pname[j].ToString();
                        pa.Department = Department[j].ToString();
                        pa.Job = Job[j].ToString();
                        pa.Phone = Phone[j].ToString();
                        pa.Mobile = Mobile[j].ToString();
                        pa.Email = Email[j].ToString();
                        pa.CreateUser = account.UserName;
                        pa.CreateTime = DateTime.Now;
                        pa.Validate = "v";
                        colPa.Add(pa);
                    }
                }
                #endregion

                if (SupplyManage.InsertSupplyBas(subas, colPa, ref  strErr) == true)
                    return Json(new { success = "true", Msg = "保存成功，如果要继续添加请点击重置按钮" });

                else
                    return Json(new { success = "false", Msg = "保存出错" + "/" + strErr });
            }
            else
            {
                return Json(new { success = "false", Msg = "保存出现问题" });
            }
        }
        /// <summary>
        /// 客户信息的更新
        /// </summary>
        /// <param name="cbas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCusInfo(tk_KClientBas cbas)
        {
            string Err = "";
            if (SupplyManage.UpdateNewCus(cbas, ref Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "--" + Err });
        }
        /// <summary>
        /// 修改非合格供应商
        /// </summary>
        /// <param name="nobas"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateIsok(tk_IsNotSupplierBas nobas)
        {
            string Err = "";
            if (SupplyManage.UpdateNewok(nobas, ref Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "--" + Err });
        }
        /// <summary>
        /// 更新联系人
        /// </summary>
        /// <param name="kcp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateManInfo(tk_KContactPerson kcp)
        {
            string Err = "";
            if (SupplyManage.UpdateNewMan(kcp, ref Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "--" + Err });
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProInfo(tk_SProducts sp)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                sp.FFileName = a;
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string Err = "";

            if (SupplyManage.UpdateNewPro(sp, fileByte, ref Err) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("UPProduct", sp);
            }
            else
            {
                ViewData["msg"] = "保存失败";
                return View("UPProduct", sp);
            }
        }
        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="sse"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateSerInfo(tk_SService sse)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] fileByte = new byte[0];
            if (file.FileName != "")
            {
                //这个获取文档名称
                string a = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                sse.FFileName = a;
                int fileLength = file.ContentLength;
                if (fileLength != 0)
                {
                    fileByte = new byte[fileLength];
                    file.InputStream.Read(fileByte, 0, fileLength);
                }
            }
            string Err = "";

            if (SupplyManage.UpdateNewServer(sse, fileByte, ref Err) == true)
            {
                ViewData["msg"] = "保存成功";
                return View("UPServer", sse);
            }
            else
            {
                ViewData["msg"] = "保存成功";
                return View("UPServer", sse);
            }
        }
        [HttpPost]
        public ActionResult UpdateUniteInfo(tk_KClientBas cbs)
        {
            string Err = "";
            if (SupplyManage.UpdateNewUnite(cbs, ref Err) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存出错" + "--" + Err });
        }
        #endregion
        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportPrint(string sid)
        {
            //tk_SProcessInfo info = new tk_SProcessInfo();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.getInfo(sid);
            return View(bas);
        }
        /// <summary>
        /// 打准出供应商
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="date"></param>
        /// <param name="opinions"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult OutPrint(string name, string type, string area, string state, string sid)
        {
            //ViewBag.name = name;
            //ViewBag.type = type;
            //ViewBag.area = area;
            //ViewBag.opinions = opinions;
            //ViewBag.state = state;
            //ViewBag.SID = sid;

            ViewData["name"] = name;
            ViewData["type"] = type;
            ViewData["area"] = area;
            ViewData["state"] = state;
            ViewData["SID"] = sid;
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.getResInfo(name, type, area, state, sid);
            return View(bas);
        }
        /// <summary>
        /// 合格供应商打印
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult PrintOK(string sid)
        {
            // string where = " and d.sid = '" + sid + "'";
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.getOkSp(sid);
            return View(bas);
        }
        /// <summary>
        /// 年度评审打印
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult PrintYear(string sid)
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            tk_SYRDetail del = new tk_SYRDetail();
            bas = SupplyManage.getPrintYear(sid);
            del = SupplyManage.getPrintScore(sid);
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            return View(bas);
        }

        #endregion
        #region js中填充到表格中
        /// <summary>
        /// 存在问题需改进
        /// </summary>
        /// <returns></returns>
        public ActionResult getRecordList()
        {
            string strErr = "";
            string COMNameC = Request["COMNameC"].ToString();
            string PID = Request["PID"].ToString();
            string SID = Request["SID"].ToString();
            string SupplierCode = Request["SupplierCode"].ToString();
            string strDetail = SupplyManage.getRecordListUT(COMNameC, PID, SID, SupplierCode, ref strErr);
            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDetail == "")
                    return Json(new { success = "false", Msg = "列表加载失败" });
                else
                    return Json(new { success = "true", RecordListUT = strDetail });
            }
        }
        /// <summary>
        /// 展示供应商联系人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContractPerson()
        {
            string sid = Request["sid"].ToString();
            // string fdepartment = Request["Type"].ToString();
            DataTable dt = SupplyManage.getperson(sid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }

        }
        public ActionResult GetProcessInfo()
        {
            string sid = Request["sid"].ToString();
            DataTable dt = SupplyManage.getProcess(sid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        public ActionResult PrintOKSupply()
        {
            string sid = Request["sid"].ToString();
            DataTable dt = SupplyManage.getOK(sid);
            // DataTable dt6 = SupplyManage.getBusinessDistribute(sid);
            //DataTable dt7 = SupplyManage.getbillWay(sid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        /// <summary>
        /// 准出供应商打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintOutSupply()
        {
            string sid = Request["sid"].ToString();
            string type = Request["type"].ToString();
            string name = Request["name"].ToString();
            string area = Request["area"].ToString();
            string state = Request["state"].ToString();
            DataTable dt = SupplyManage.getOut(sid, type, name, area, state);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        /// <summary>
        /// 年度评审打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintYearSupply()
        {
            string sid = Request["sid"].ToString();
            DataTable dt = SupplyManage.getYear(sid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        public ActionResult GetDetailInfo()
        {
            string where = "";
            string sid = "";
            if (Request["id"] != null)
                sid = Request["id"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            DataTable dt = SupplyManage.getDetail(sid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        public ActionResult GetYearInfo()
        {
            string where = "";
            string sid = "";
            string yid = "";
            if (Request["id"] != null)
                sid = Request["id"].ToString();
            if (sid != "")
            {
                where += " and a.SID = '" + sid + "'";
            }
            if (Request["yid"] != null)
                yid = Request["yid"].ToString();
            if (yid != "")
            {
                where += " and c.YRID = '" + yid + "'";
            }
            DataTable dt = SupplyManage.getYReview(sid, yid);
            if (dt == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, datas = GFun.Dt2Json("", dt) });
            }
        }
        #endregion
        #region 可选可输入方法
        /// <summary>
        /// 根据产品分类查询产品名称
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSelDesc()
        {
            string strSelThird = "";//产品分类
            if (Request["ThirdType"] != null)
                strSelThird = Request["ThirdType"].ToString();
            string strJson = SupplyManage.GetSelDesc(strSelThird);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据产品分类获得规格类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStand()
        {
            string strSelThird = "";//产品分类
            if (Request["ThirdType"] != null)
                strSelThird = Request["ThirdType"].ToString();
            string strJson = SupplyManage.GetStand(strSelThird);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProIDS()
        {
            string strSelThird = "";//产品类型
            if (Request["Stand"] != null)
                strSelThird = Request["Stand"].ToString();
            string strJson = SupplyManage.GetStand2(strSelThird);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPrice()
        {
            string strSelThird = "";//产品名称
            if (Request["ThirdType"] != null)
                strSelThird = Request["ThirdType"].ToString();
            string strJson = SupplyManage.GetPrices(strSelThird);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据产品分类下拉框出现相关的产品名称
        /// </summary>
        /// <returns></returns>
        public ActionResult getDescLink()
        {
            string strErr = "";
            string strDesc = Request["SelDesc"].ToString();//文本输入的值
            string strThirdType = "";
            if (Request["ThirdType"] != null)
                strThirdType = Request["ThirdType"].ToString();//产品分类

            string strDescLink = SupplyManage.getDescLink(strDesc, strThirdType, ref strErr);

            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDescLink == "")
                    return Json(new { success = "false", Msg = "相关信息获取失败" });
                else
                    return Json(new { success = "true", strDescLink = strDescLink });
            }
        }
        public ActionResult getProStand()
        {
            string strErr = "";
            string strDesc = Request["Stand"].ToString();//文本输入的值
            string strThirdType = "";
            if (Request["ThirdType"] != null)
                strThirdType = Request["ThirdType"].ToString();//产品分类

            string strDescLink = SupplyManage.getPro(strDesc, strThirdType, ref strErr);

            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDescLink == "")
                    return Json(new { success = "false", Msg = "相关信息获取失败" });
                else
                    return Json(new { success = "true", strDescLink = strDescLink });
            }
        }
        public ActionResult getProID()
        {
            string strErr = "";
            string strDesc = Request["Proid"].ToString();//文本输入的值,产品编号
            string strThirdType = "";
            if (Request["Stand"] != null)
                strThirdType = Request["Stand"].ToString();//产品分类

            string strDescLink = SupplyManage.getProID(strDesc, strThirdType, ref strErr);

            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDescLink == "")
                    return Json(new { success = "false", Msg = "相关信息获取失败" });
                else
                    return Json(new { success = "true", strDescLink = strDescLink });
            }
        }
        public ActionResult getProPrice()
        {
            string strErr = "";
            string strDesc = Request["Price"].ToString();//文本输入的值
            string strThirdType = "";
            if (Request["ThirdType"] != null)
                strThirdType = Request["ThirdType"].ToString();//产品名称
            string strDescLink = SupplyManage.getprice(strDesc, strThirdType, ref strErr);

            if (strErr != "")
            {
                return Json(new { success = "false", Msg = strErr });
            }
            else
            {
                if (strDescLink == "")
                    return Json(new { success = "false", Msg = "相关信息获取失败" });
                else
                    return Json(new { success = "true", strDescLink = strDescLink });
            }
        }
        /// <summary>
        /// 获得二级项
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSecond()
        {
            string strSelFirst = "";
            if (Request["SelFirst"] != null)
                strSelFirst = Request["SelFirst"].ToString();//获取的是sid的值
            string strJson = SupplyManage.GetSeond(strSelFirst);
            return Json(strJson);
        }
        /// <summary>
        /// 获得三级项
        /// </summary>
        /// <returns></returns>
        public ActionResult GetThread()
        {
            string strSelFirst = "";
            if (Request["SelSecond"] != null)
                strSelFirst = Request["SelSecond"].ToString();//获取的是text的值
            string strJson = SupplyManage.GetThread(strSelFirst);
            return Json(strJson);
        }
        public ActionResult GetCom()
        {
            string strSelFirst = "";
            if (Request["SelComd"] != null)
                strSelFirst = Request["SelComd"].ToString();//获取的是text的值
            string strJson = SupplyManage.GetCompare(strSelFirst);
            return Json(strJson);
        }
        #endregion
        #region 证书相关设置
        /// <summary>
        /// 证书设置
        /// </summary> 
        /// <returns></returns>
        public ActionResult ApprovalMaintain()
        {
            return View();
        }
        /// <summary>
        /// 证书到期设置
        /// </summary>
        /// <returns></returns>
        public ActionResult ApprovalWarn()
        {
            return View();
        }

        public ActionResult LookPicture(string id)
        {
            ViewData["ID"] = id;
            return View();
        }
        /// <summary>
        /// 证书过期提醒
        /// </summary>
        /// <returns></returns>
        public ActionResult ApprovalWarnGrid()
        {
            string where = "";
            Acc_Account account = GAccount.GetAccountInfo();
            where = " and a.Unit = '" + account.UnitID + "'";
            string num = UserAptitudeMan.getNewAptitudeTime();
            if (num == "")
                where += " and DATEDIFF(MONTH,GETDATE(),a.CertificatDate) <= '2'";
            else
                where += "  and DATEDIFF(MONTH,GETDATE(),a.CertificatDate) <= '" + num + "'";
            string strCurPage;
            string strRowNum;
            string UserName = Request["userName"].ToString();
            string BusinessType = Request["businessType"].ToString();
            string TecoName = Request["tecoName"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (UserName != "")
                where += " and a.UserName like '%" + UserName + "%'";
            if (BusinessType != "")
                where += " and a.BusinessType = '" + BusinessType + "'";
            if (TecoName != "")
                where += " and a.TecoName like '%" + TecoName + "%'";
            UIDataTable udtTask = SupplyManage.getNewTimeOutGrid(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, account.UnitID);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 获取审批权限
        /// <summary>
        /// 获得领导职位,判断是否有审批信息
        /// </summary>
        /// <param name="ExJob"></param>
        /// <returns></returns> 
        public ActionResult GetApproval(string userid)
        {
            DataTable dt = SupplyManage.GetJob(GAccount.GetAccountInfo().UnitID, GAccount.GetAccountInfo().UserName, userid);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBas(string userid)
        {
            DataTable dt = SupplyManage.Getbas(GAccount.GetAccountInfo().UnitID, GAccount.GetAccountInfo().UserName, userid);
            string strJson = GFun.Dt2Json("", dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Substring(0, strJson.Length - 1);
            return Json(strJson, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审批信息汇总表
        /// </summary>
        /// <param name="exjob"></param>
        /// <returns></returns>
        public ActionResult SPRemind(string exjob)
        {
            ViewData["Exjob"] = exjob;
            return View();
        }
        #endregion
        #region 获取以上传的资料文档
        public ActionResult GetFile()
        {
            var id = Request["data1"];//sid
            var fid = Request["timeOut"];
            var filename = Request["filename"].ToString();
            string ID = "";
            string name = "";
            string file = "";
            string type = "";
            DataTable dtInfo = SupplyManage.GetNewDownLoad(id, fid, filename);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["fid"].ToString() + "@";
                name += dtInfo.Rows[i]["ffilename"].ToString() + "@";
                file += dtInfo.Rows[i]["fileinfo"].ToString() + "@";
                type += dtInfo.Rows[i]["filetype"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            type = type.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file, Type = type });
        }
        /// <summary>
        /// 显示比价单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPriceFile()
        {
            var id = Request["data1"];//sid
            var pricename = Request["pricename"];
            string ID = "";
            string price = "";
            DataTable dtInfo = SupplyManage.GetNewpriceDownLoad(id, pricename);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["ID"].ToString() + "@";
                price += dtInfo.Rows[i]["pricename"].ToString() + "@";
                //  file += dtInfo.Rows[i]["fileinfo"].ToString() + "@";
                //type += dtInfo.Rows[i]["filetype"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            price = price.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = price });
        }
        /// <summary>
        /// 获取资质证明材料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMFile()
        {
            var id = Request["data1"];//sid
            //var fid = Request["timeOut"];//fid
            //var mfilename = Request["filename"].ToString();//资质证明材料
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = SupplyManage.GetNewDownLoadUnit(id);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["fid"].ToString() + "@";
                name += dtInfo.Rows[i]["FFileName"].ToString() + "@";
                file += dtInfo.Rows[i]["FileInfo"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }
        public ActionResult GetProduct()
        {
            var id = Request["data1"];//sid
            var timeout = Request["timeOut"];//产品唯一编号
            var filename = Request["filename"].ToString();
            string ID = "";
            string name = "";
            string file = "";
            // string type = "";
            DataTable dtInfo = SupplyManage.GetNewDownLoadProduct(id, timeout, filename);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["id"].ToString() + "@";
                name += dtInfo.Rows[i]["ffilename"].ToString() + "@";
                file += dtInfo.Rows[i]["fileinfo"].ToString() + "@";
                //type += dtInfo.Rows[i]["filetype"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            //type = type.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }
        public ActionResult GetServer()
        {
            var sid = Request["data1"];//sid
            var id = Request["id"].ToString();//服务唯一编号
            var filename = Request["filename"].ToString();
            string ID = "";
            string name = "";
            string file = "";
            DataTable dtInfo = SupplyManage.GetNewDownLoadServer(sid, id, filename);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["serviceid"].ToString() + "@";
                name += dtInfo.Rows[i]["ffilename"].ToString() + "@";
                file += dtInfo.Rows[i]["fileinfo"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file });
        }
        public ActionResult GetCertifi()
        {
            var id = Request["data1"];
            var fid = Request["fid"];
            var filename = Request["CFilename"].ToString();
            string ID = "";
            string name = "";
            string file = "";
            string type = "";
            DataTable dtInfo = SupplyManage.GetNewDownLoad1(id, fid, filename);
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                ID += dtInfo.Rows[i]["fid"].ToString() + "@";
                name += dtInfo.Rows[i]["cfilename"].ToString() + "@";
                file += dtInfo.Rows[i]["fileinfo"].ToString() + "@";
                type += dtInfo.Rows[i]["filetype"].ToString() + "@";
            }
            ID = ID.TrimEnd('@');
            name = name.TrimEnd('@');
            file = file.TrimEnd('@');
            type = type.TrimEnd('@');
            return Json(new { success = "true", id = ID, Name = name, File = file, Type = type });
        }
        #endregion
        #region 下载以上传的文档
        /// <summary>
        /// 上传到数据库以二进制形式存储
        /// </summary>
        /// <param name="id"></param>
        public void DownLoad(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SupplyManage.GetNewDownloadFile(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["ffilename"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public void DownLoadUnit(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SupplyManage.GetNewDownload(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FFileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public void DownLoadProduct(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SupplyManage.GetNewDownloadProduct(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["ffilename"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        /// <summary>
        /// 上传到服务器的
        /// </summary>
        /// <param name="sid"></param>
        public void DownLoadAward(string sid)
        {

            DataTable dtInfo = SupplyManage.GetNewDownloadAward(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                if (dtInfo.Rows[0][0].ToString() != "")
                {
                    string fileName = dtInfo.Rows[0]["Award"].ToString();//客户端保存的文件名 
                    string filePath = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\"
                         + fileName;//路径
                    //以字符流的形式下载文件 
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 上传到服务器
        /// </summary>
        /// <param name="sid"></param>
        public void DownLoadPrice(string sid)
        {

            DataTable dtInfo = SupplyManage.GetNewDownloadPrice(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                if (dtInfo.Rows[0][0].ToString() != "")
                {
                    string fileName = dtInfo.Rows[0]["PriceName"].ToString();//客户端保存的文件名 
                    string filePath = System.Configuration.ConfigurationSettings.AppSettings["upload"] + "\\"
                         + fileName;//路径
                    //以字符流的形式下载文件 
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public void DownLoadSUProduct(string sid)
        {
            DataTable dtInfo = SupplyManage.GetNewDownloadAPro(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FFileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端
                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        /// <summary>
        /// 待处理供应商查看按钮
        /// </summary>
        /// <param name="id"></param>
        public void DownLoadNewServer(string sid)
        {
            DataTable dtInfo = SupplyManage.GetManDownloadServer(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["FFileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        /// <summary>
        /// 资质查看
        /// </summary>
        /// <param name="id"></param>
        public void DownLoadNewFile(string sid)
        {
            DataTable dtInfo = SupplyManage.GetManDownloadFile(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["ffilename"].ToString()));

                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public void DownLoadCertify(string sid)
        {
            DataTable dtInfo = SupplyManage.GetManDownloadCerty(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["CFileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        #region 待用代码
        //public void DownLoadServer(string sid)
        //{
        //    DataTable dtInfo = SupplyManage.GetNewDownloadProduct(sid);
        //    if (dtInfo.Rows[0][0].ToString() != "")
        //    {
        //        byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];
        //        Response.Clear();
        //        Response.Charset = "GB2312";
        //        Response.ContentEncoding = System.Text.Encoding.UTF8;
        //        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["ffilename"].ToString()));
        //        // 添加头信息，指定文件大小，让浏览器能够显示下载进度

        //        Response.AddHeader("Content-Length", bContent.Length.ToString());
        //        // 指定返回的是一个不能被客户端读取的流，必须被下载
        //        Response.ContentType = "application/msword";
        //        // 把文件流发送到客户端

        //        Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
        //        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //} 
        #endregion
        public void DownLoadZhenshu(string sid)
        {
            DataTable dtInfo = SupplyManage.GetManDownloadCerty(sid);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["FileInfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["CFileName"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public void DownLoadServer(string id)
        {
            string[] arrStr = id.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SupplyManage.GetNewDownloadServer(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["ffilename"].ToString()));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端
                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        public void DownLoadCerty(string fid)
        {
            string[] arrStr = fid.Split('/');
            string informNo = arrStr[0];
            DataTable dtInfo = SupplyManage.GetNewDownloadcerty(informNo);
            if (dtInfo.Rows[0][0].ToString() != "")
            {
                byte[] bContent = (byte[])dtInfo.Rows[0]["fileinfo"];
                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(dtInfo.Rows[0]["cfilename"].ToString()));
                //Response.BinaryWrite(bContent);
                //Response.Flush();
                //Response.End();
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                Response.AddHeader("Content-Length", bContent.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                Response.ContentType = "application/msword";
                // 把文件流发送到客户端

                Response.BinaryWrite((byte[])dtInfo.Rows[0][0]);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        #endregion
        #region 删除文档资料
        public ActionResult deleteFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SupplyManage.DellNewFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        public ActionResult deleteMFile()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SupplyManage.DellNewMFile(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        public ActionResult deleteProduct()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SupplyManage.DellNewProduct(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        public ActionResult deleteServer()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SupplyManage.DellNewServer(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        public ActionResult deleteCerty()
        {
            var id = Request["data1"];
            string[] arr = id.Split('/');
            string strErr = "";
            if (SupplyManage.DellNewCerty(arr[0], ref strErr) == true)
                return Json(new { success = "true", Msg = "删除成功" });
            else
                return Json(new { success = "false", Msg = "删除出错" + "/" + strErr });
        }
        #endregion
        #region 获取配置项
        public ActionResult GetConfigType()
        {
            DataTable dt = SupplyManage.getNewAppType("FDepartment");
            if (dt == null || dt.Rows.Count == 0) return Json(new { success = "false", Msg = "无数据" });
            string id = "";
            string name = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id += dt.Rows[i]["SID"].ToString() + ",";
                name += dt.Rows[i]["Text"].ToString() + ",";
            }
            id = "" + "," + id.TrimEnd(',');
            name = "--请选择--" + "," + name.TrimEnd(',');
            return Json(new { success = "true", Strid = id, Strname = name });
        }
        public ActionResult GetHaveContent()
        {
            var butype = Request["data1"];
            string content = SupplyManage.getNewHaveExaminContent(butype);
            if (content != "")
                return Json(new { success = "true", Msg = content });
            else
                return Json(new { success = "false", Msg = content });
        }
        #endregion

        #region 审批相关操作
        public ActionResult SubmitApproval(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(RelevanceID);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            ViewData["OpinionsD"] = info.OpinionsD;

            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            ViewData["ISAgree"] = info.ISAgree;


            tk_SYRDetail del = new tk_SYRDetail();
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;

            return View();
        }
        public ActionResult SubmittaotaiApproval(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            //string[] arr = id.Split('@');
            //string RelevanceID = arr[0];  //关联表单内ID
            //string webkey = arr[1];      // Web.config内关联的key
            //string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            //string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(id);
            //ViewData["webkey"] = webkey;
            //ViewData["folderBack"] = folderBack;
            //ViewData["PID"] = PID;
            ViewData["RelevanceID"] = id;

            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            tk_SYRDetail del = new tk_SYRDetail();
            del = SupplyManage.getDetails(id);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;

            return View();
        }
        public ActionResult Submitzhunchu(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(RelevanceID);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;

            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View();
        }
        /// <summary>
        /// 部门级恢复供货审批
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Submithuifu(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(id);
            ViewData["RelevanceID"] = id;

            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View();
        }
        /// <summary>
        /// 恢复供应商提交审批编号
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitRcover(string id)
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            //string[] arr = id.Split('@');
            //string RelevanceID = arr[0];  //关联表单内ID
            //string webkey = arr[1];      // Web.config内关联的key
            //string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            //string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            tk_SProcessInfo info = new tk_SProcessInfo();
            info = SupplyManage.getProceinfo(id);
            //ViewData["webkey"] = webkey;
            //ViewData["folderBack"] = folderBack;
            //ViewData["PID"] = PID;
            ViewData["RelevanceID"] = id;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["job"] = acc.Exjob;

            ViewData["Opinions"] = info.Opinions;
            ViewData["Reason"] = info.Reason;
            ViewData["UserName"] = acc.UserName;
            ViewData["time"] = DateTime.Now;
            return View();
        }
        public ActionResult ApprovalZhuRu(string id)
        {
            string[] arr = id.Split('@');
            string RelevanceID = arr[0];  //关联表单内ID
            string webkey = arr[1];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        /// <summary>
        /// 测试会签准入审批
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ApprovalZhuRu1(string id)
        {
            //string[] arr = id.Split('@');
            //string RelevanceID = arr[0];  //关联表单内ID
            //string webkey = arr[1];      // Web.config内关联的key
            //string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            //string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            //ViewData["webkey"] = webkey;
            //ViewData["folderBack"] = folderBack;
            //ViewData["PID"] = PID;
            //ViewData["RelevanceID"] = RelevanceID;
            ViewData["sid"] = id;
            return View();
        }

        #endregion
        #region 往审批表插入操作
        public ActionResult InsertApproval()
        {
            var webkey = Request["data1"];//准入还是准出
            var folderBack = Request["data2"];//审批关键路径
            var RelevanceID = Request["data3"];//sid
            tk_SProcessInfo info = new tk_SProcessInfo();
            GSqlSentence.SetTValue<tk_SProcessInfo>(info, HttpContext.ApplicationInstance.Context.Request);
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            if (SupplyManage.InsertNewApprovals(PID, RelevanceID, webkey, folderBack, ref strErr, info) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        public ActionResult InserttaotaiApproval()
        {
            //var webkey = Request["data1"];//准入还是准出
            //var folderBack = Request["data2"];//审批关键路径
            var RelevanceID = Request["data3"];//sid
            var isagree = Request["data4"].ToString();
            var scontetn = Request["data5"].ToString();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info.ISAgree = isagree;
            info.OpinionsD = scontetn;
            GSqlSentence.SetTValue<tk_SProcessInfo>(info, HttpContext.ApplicationInstance.Context.Request);
            //  string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            if (SupplyManage.InsertNewtaotaiApprovals(RelevanceID, ref strErr, info) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        /// <summary>
        /// 准出部门
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertzhunchuApproval()
        {
            var webkey = Request["data1"];//准入还是准出
            var folderBack = Request["data2"];//审批关键路径
            var RelevanceID = Request["data3"];//sid
            var isagree = Request["data4"].ToString();
            var scontetn = Request["data5"].ToString();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info.ISAgree = isagree;
            info.OpinionsD = scontetn;
            GSqlSentence.SetTValue<tk_SProcessInfo>(info, HttpContext.ApplicationInstance.Context.Request);
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            if (SupplyManage.InsertNewzhunchuApprovals(PID, RelevanceID, webkey, folderBack, ref strErr, info) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        public ActionResult InserthuifuApproval()
        {
            var RelevanceID = Request["data3"];//sid
            var isagree = Request["data4"].ToString();
            var scontetn = Request["data5"].ToString();
            tk_SProcessInfo info = new tk_SProcessInfo();
            info.ISAgree = isagree;
            info.OpinionsD = scontetn;
            GSqlSentence.SetTValue<tk_SProcessInfo>(info, HttpContext.ApplicationInstance.Context.Request);
            string strErr = "";
            if (SupplyManage.InsertNewhuifuApprovals(RelevanceID, ref strErr, info) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        /// <summary>
        /// 准入评审界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Insertzhunru()
        {
            var webkey = Request["data1"];//准入还是准出
            var folderBack = Request["data2"];//审批关键路径
            var RelevanceID = Request["data3"];//sid
            string state = Request["data4"].ToString();
            string content = Request["data5"].ToString();
            string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            sgs.SCreate = (DateTime.Now).ToString();
            sgs.Sperson = acc.UserName;
            sgs.SContent = content;
            sgs.SState = state;
            if (SupplyManage.InsertNewzhunru(PID, RelevanceID, webkey, folderBack, ref strErr, sgs) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        public ActionResult Insertzhunru1()
        {
            string state = Request["data4"].ToString();
            string content = Request["data5"].ToString();
            string sid = Request["data1"].ToString();
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            sgs.SCreate = (DateTime.Now).ToString();
            sgs.Sperson = acc.UserName;
            sgs.SContent = content;
            sgs.SState = state;
            sgs.Sid = sid;
            if (SupplyManage.InsertNewzhunru1(ref strErr, sgs) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        #endregion

        #region 不用审批设置自由审批操作
        /// <summary>
        /// 往库里插入的操作
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertRecover()
        {
            Acc_Account ac = GAccount.GetAccountInfo();
            //var webkey = Request["data1"];//准入还是准出
            //var folderBack = Request["data2"];//审批关键路径
            var RelevanceID = Request["data3"];//sid
            string state = Request["data4"].ToString();
            string content = Request["data5"].ToString();
            string person = ac.UserName;// Request["data6"].ToString();
            string time = DateTime.Now.ToString("yyyy-MM-dd");//Request["data7"].ToString();

            // string PID = COM_ApprovalMan.GetNewSPid(folderBack);
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            tk_SUPSugestion sgs = new tk_SUPSugestion();
            sgs.SCreate = time;
            sgs.Sperson = person;
            sgs.SContent = content;
            sgs.SState = state;
            Tk_SupplierBas bas = new Tk_SupplierBas();
            bas = SupplyManage.getBAS(RelevanceID);
            if (SupplyManage.InsertNewrecover(RelevanceID, ref strErr, sgs, bas) == true)
                return Json(new { success = "true", Msg = "提交成功" });
            else
                return Json(new { success = "false", Msg = "提交出错" + "/" + strErr });
        }
        /// <summary>
        /// 会签公司级审批操作
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult ApprovalSup(string id)
        {

            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            Tk_SupplierBas bas = new Tk_SupplierBas();
            string folderBack = COM_ApprovalMan.getNewwebkey("准入评审");
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);//产生pid但不往tkpid表插入
            // string pids = COM_ApprovalMan.GetNewSPid(folderBack);//产生新的pid往 tkpid表插入
            suges = SupplyManage.GetSuge(id);
            info = SupplyManage.getProceinfo(id);
            del = SupplyManage.getDetails(id);
            bas = SupplyManage.getBAS(id);

            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = id;

            ViewData["IsUnReview"] = bas.IsUnReview;
            ViewData["URConfirmUser"] = bas.URConfirmUser;
            ViewData["IsUnreviewUser"] = bas.IsUnreviewUser;
            ViewData["IsURInnerUnit"] = bas.IsURInnerUnit;
            ViewData["UnReviewUnit"] = bas.UnReviewUnit;//数字替换汉字
            ViewData["Evaluation1"] = bas.Evaluation1;
            ViewData["Evaluation2"] = bas.Evaluation2;
            ViewData["Evaluation3"] = bas.Evaluation3;
            ViewData["Evaluation4"] = bas.Evaluation4;
            ViewData["Evaluation5"] = bas.Evaluation5;
            ViewData["Evaluation6"] = bas.Evaluation6;


            return View();
        }
        /// <summary>
        /// 准出公司审批
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ApprovalzhunchuSup(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);//获取已经存在的pid
            string PIDS = COM_ApprovalMan.GetNewSPid(folderBack);//产生新的pid
            string RelevanceID = arr[1];
            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            suges = SupplyManage.GetSuge(RelevanceID);
            info = SupplyManage.getProceinfo(RelevanceID);
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;

            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();

        }
        /// <summary>
        /// 年度评审页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Approvalnd(string id)
        {
            string folderBack = COM_ApprovalMan.getNewwebkey("年度评审");
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            string PIDS = COM_ApprovalMan.GetNewSPid(folderBack);
            string RelevanceID = id;
            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            suges = SupplyManage.GetSuge(RelevanceID);
            info = SupplyManage.getProceinfo(RelevanceID);
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;
            ViewData["webkey"] = "年度评审";
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }

        /// <summary>
        /// 往审批表记录数据
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateApproval()
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            string folderBack = COM_ApprovalMan.getNewwebkey("准入评审");
            var PID = COM_ApprovalMan.GetNewSPid(folderBack);
            var RelevanceID = Request["RelevanceID"];
            var job = acc.Exjob;
            string strErr = "";
            if (SupplyManage.UpdateNewApproval(IsPass, Opinion, PID, RelevanceID, ref strErr, job) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        public ActionResult UpdateApprovalSup()
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var Remark = Request["Remark"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            var job = acc.Exjob;
            string strErr = "";
            if (SupplyManage.UpdateNewzhunchuApproval(IsPass, Opinion, Remark, PID, webkey, folderBack, RelevanceID, ref strErr, job) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        /// <summary>
        /// 年度评审插入到表
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateApprovalND()
        {
            Acc_Account acc = GAccount.GetAccountInfo();
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            var job = acc.Exjob;
            string strErr = "";
            if (SupplyManage.UpdateNewndApproval(IsPass, Opinion, PID, webkey, folderBack, RelevanceID, ref strErr, job) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }
        /// <summary>
        /// 恢复供应商页面方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Approvalhfg(string id)
        {
            string[] arr = id.Split('@');
            string webkey = arr[0];      // Web.config内关联的key
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);
            string PID = COM_ApprovalMan.GetNewShowSPid(folderBack);
            string PIDS = COM_ApprovalMan.GetNewSPid(folderBack);
            string RelevanceID = arr[1];
            tk_SYRDetail del = new tk_SYRDetail();
            tk_SProcessInfo info = new tk_SProcessInfo();
            tk_SUPSugestion suges = new tk_SUPSugestion();
            suges = SupplyManage.GetSuge(RelevanceID);
            info = SupplyManage.getProceinfo(RelevanceID);
            del = SupplyManage.getDetails(RelevanceID);
            ViewData["Score1"] = del.Score1;
            ViewData["Score2"] = del.Score2;
            ViewData["Score3"] = del.Score3;
            ViewData["Score4"] = del.Score4;
            ViewData["Score5"] = del.Score5;
            ViewData["Result"] = del.Result;
            ViewData["ReviewDate"] = del.ReviewDate;
            ViewData["ResultDesc"] = del.ResultDesc;
            ViewData["DeclareUser"] = del.DeclareUser;
            ViewData["DeclareUnit"] = del.DeclareUnit;
            ViewData["OpinionsD"] = info.OpinionsD;
            ViewData["ISAgree"] = info.ISAgree;
            ViewData["SState"] = suges.SState;
            ViewData["SContent"] = suges.SContent;

            ViewData["webkey"] = webkey;
            ViewData["folderBack"] = folderBack;
            ViewData["PID"] = PID;
            ViewData["RelevanceID"] = RelevanceID;
            return View();
        }
        /// <summary>
        /// 恢复供应商审批
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateApprovalhfg()
        {
            Tk_SupplierBas bas = new Tk_SupplierBas();
            Acc_Account acc = GAccount.GetAccountInfo();
            var IsPass = Request["IsPass"];
            var Opinion = Request["Opinion"];
            var webkey = Request["webkey"];
            var folderBack = Request["folderBack"];
            var PID = Request["PID"];
            var RelevanceID = Request["RelevanceID"];
            var job = acc.Exjob;
            bas = SupplyManage.getBAS(RelevanceID);

            string strErr = "";
            if (SupplyManage.UpdateNewhfgApproval(IsPass, Opinion, PID, webkey, folderBack, RelevanceID, ref strErr, bas, job) == true)
                return Json(new { success = "true", Msg = "保存成功" });
            else
                return Json(new { success = "false", Msg = "保存失败" + "/" + strErr });
        }

        public ActionResult InsertBiddingNew()
        {
            string strErr = "";
            Acc_Account acc = GAccount.GetAccountInfo();
            HttpFileCollection Filedata = System.Web.HttpContext.Current.Request.Files;//获取上传的文件
            // 如果没有上传文件
            if (Filedata == null || Filedata.Count == 0 || Filedata[0].ContentLength == 0)
            {
                return this.HttpNotFound();
            }

            tk_Award fileUp = new tk_Award();
            fileUp.SID = Request["SID"].ToString();
            //fileUp.FileType = Request["Types"].ToString();
            fileUp.CreatUser = acc.UserName.ToString();
            fileUp.AwardTime = DateTime.Now;
            fileUp.Validate = "v";
            SupplyManage.InsertBiddingNew(fileUp, Filedata, ref strErr);
            return this.Json(new { });
        }
        /// <summary>
        /// 公司审批判断
        /// </summary>
        /// <returns></returns>
        public ActionResult JudgeAppDisable()
        {
            var webkey = Request["data1"];
            var job = Request["Job"];
            var SPID = Request["SPID"];
            Acc_Account account = GAccount.GetAccountInfo();
            string logUser = account.UserID.ToString();
            string folderBack = COM_ApprovalMan.getNewwebkey(webkey);//准入评审
            //var SPID = COM_ApprovalMan.GetNewShowSPid(folderBack);

            int bol = SupplyManage.JudgeNewLoginUser(logUser, webkey, folderBack, SPID, job);//审批顺序
            return Json(new { success = "true", intblo = bol });
        }
        public ActionResult Condition()
        {
            string where = "";
            string strCurPage;
            string strRowNum;
            string webkey = Request["webkey"].ToString();
            string SID = "";
            string folderBack = Request["folderBack"].ToString();
            if (Request["SID"] != null)
                SID = Request["SID"].ToString();
            if (Request["curpage"] != null)
                strCurPage = Request["curpage"].ToString();
            if (Request["rownum"] != null)
                strRowNum = Request["rownum"].ToString();
            else
                strRowNum = "10";
            if (SID != "")
                where += " and a.RelevanceID = '" + SID + "'";

            UIDataTable udtTask = new UIDataTable();
            if (where != "")
                udtTask = SupplyManage.getNewCondition(GFun.SafeToInt32(strRowNum), GFun.SafeToInt32(Request["curpage"]) - 1, where, folderBack);
            string strjson = GFun.Dt2Json("", udtTask.DtData);
            strjson = strjson.Substring(1);
            strjson = strjson.Substring(0, strjson.Length - 1);
            string jsonData = "{ \"page\":" + GFun.SafeToInt32(Request["curpage"]) + ", \"total\": " + udtTask.IntTotalPages + ", \"records\": " + udtTask.IntRecords + ", \"rows\": ";
            jsonData += strjson + "}";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
