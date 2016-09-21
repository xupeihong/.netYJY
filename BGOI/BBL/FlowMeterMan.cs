using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TECOCITY_BGOI
{
    public class FlowMeterMan
    {
        // 主页-查询加载登记卡列表 
        public static UIDataTable LoadCardList(int a_intPageSize, int a_intPageIndex, string where, string Order, string strType)
        {
            return FlowMeterPro.LoadCardList(a_intPageSize, a_intPageIndex, where, Order, strType);
        }

        // 加载状态为收货确认之前的登记卡列表 
        public static UIDataTable LoadCardListAll(int a_intPageSize, int a_intPageIndex, string where, string Order)
        {
            return FlowMeterPro.LoadCardListAll(a_intPageSize, a_intPageIndex, where, Order);
        }

        // 新增-加载需要检查的仪表项目
        public static string GetCheckItems(ref string a_strErr)
        {
            DataTable dtItems = new DataTable();
            dtItems = FlowMeterPro.GetCheckItems(ref a_strErr);

            if (dtItems == null)
                return "";
            if (dtItems.Rows.Count == 0)
                return "";

            string strCheckItems = GFun.Dt2Json("CheckItems", dtItems);
            return strCheckItems;
        }

        // 新增-加载超声波需要检查的仪表项目
        public static string GetCheckItemsUT(ref string a_strErr)
        {
            DataTable dtItems = new DataTable();
            dtItems = FlowMeterPro.GetCheckItemsUT(ref a_strErr);

            if (dtItems == null)
                return "";
            if (dtItems.Rows.Count == 0)
                return "";

            string strCheckItems = GFun.Dt2Json("CheckItems", dtItems);
            return strCheckItems;
        }

        // 新增-获取新的维修标识码 RID 
        public static string GetNewRID()
        {
            return FlowMeterPro.GetNewRID();
        }

        // 新增-插入新的维修标识码 RID 
        public static string GetRID()
        {
            return FlowMeterPro.GetRID();
        }

        // 新增-确认新增登记卡
        public static bool AddNewCard(RepairCard repairCard, string Title, string Checked, ref string a_strErr)
        {
            if (FlowMeterPro.AddNewCard(repairCard, Title, Checked, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        // 新增-确认新增登记卡
        public static bool AddNewCard2(RepairCardNew repairCard, string Title, string Checked, string TitleUT, string CheckedUT, ref string a_strErr)
        {
            if (FlowMeterPro.AddNewCard2(repairCard, Title, Checked, TitleUT, CheckedUT, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        // 新增-根据仪表编号判断是否存在当前信息
        public static DataTable CheckMeterInfo(string MeterID, string strModelType)
        {
            return FlowMeterPro.CheckMeterInfo(MeterID, strModelType);
        }

        // 新增-根据仪表类型获取仪表名称 一般是一对一的关系
        public static string GetMeterName(string ModelType, ref string strErr)
        {
            return FlowMeterPro.GetMeterName(ModelType, ref strErr);
        }

        // 新增-加载流量范围下拉框
        public static string GetFlowRange(string strModel)
        {
            string strFlowRange = FlowMeterPro.GetFlowRange(strModel);
            if (strFlowRange != "")
                return strFlowRange;
            else
                return "";
        }

        // 新增-加载承压等级下拉框
        public static string GetPressure(string strModel)
        {
            string strPressure = FlowMeterPro.GetPressure(strModel);
            if (strPressure != "")
                return strPressure;
            else
                return "";
        }

        // 新增-超声波加载流量范围下拉框
        public static string GetFlowRangeUT(string strModel)
        {
            string strFlowRangeUT = FlowMeterPro.GetFlowRangeUT(strModel);
            if (strFlowRangeUT != "")
                return strFlowRangeUT;
            else
                return "";
        }

        // 新增-超声波加载承压等级下拉框
        public static string GetPressureUT(string strModel)
        {
            string strPressureUT = FlowMeterPro.GetPressureUT(strModel);
            if (strPressureUT != "")
                return strPressureUT;
            else
                return "";
        }





        // 修改-获取指定id对应的登记卡详细信息
        public static RepairCard getNewRepairCard(string strID)
        {
            return FlowMeterPro.getNewRepairCard(strID);
        }

        // 修改-获取选中的选项内容
        public static DataTable GetCheckeds(string strRID)
        {
            return FlowMeterPro.GetCheckeds(strRID);
        }

        // 修改-确认修改登记卡 
        public static bool UpdateCard(RepairCard repairCard, string Title, string Checked, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateCard(repairCard, Title, Checked, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 修改-获取指定id对应的超声波登记卡详细信息
        public static RepairCardUT getNewRepairCardUT(string strIDUT)
        {
            return FlowMeterPro.getNewRepairCardUT(strIDUT);
        }

        // 修改-获取超声波选中的选项内容 
        public static DataTable GetCheckedsUT(string strRID)
        {
            return FlowMeterPro.GetCheckedsUT(strRID);
        }

        // 修改-确认修改超声波登记卡
        public static bool UpdateCardUT(RepairCardUT repairCard, string Title, string Checked, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateCardUT(repairCard, Title, Checked, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }




        // 查看-获取操作日志记录列表 
        public static string GetoperateLog(string strRID, ref string a_strErr)
        {
            DataTable dtList = new DataTable();
            dtList = FlowMeterPro.GetoperateLog(strRID, ref a_strErr);

            if (dtList == null) return "";
            if (dtList.Rows.Count == 0) return "";

            string strList = GFun.Dt2Json("OperateList", dtList);
            return strList;
        }

        // 打印登记卡-获取型号
        public static string getModels(string strRID)
        {
            return FlowMeterPro.getModels(strRID);
        }

        // 获取型号
        public static string getModelsUT(string strRID)
        {
            return FlowMeterPro.getModelsUT(strRID);
        }

        // 打印随工单-获取详细信息
        public static tk_WorkCard getNewWorkCard(string strID)
        {
            return FlowMeterPro.getNewWorkCard(strID);
        }

        // 打印超声波随工单-获取详细信息
        public static tk_WorkCardUT getNewWorkCardUT(string strID)
        {
            return FlowMeterPro.getNewWorkCardUT(strID);
        }


        // 打印随工单-获取维修编号
        public static string getRepairID(string strRID)
        {
            return FlowMeterPro.getRepairID(strRID);
        }

        // 打印随工单-加载需要检查的仪表项目
        public static string GetOutCheck(ref string a_strErr)
        {
            DataTable dtItems = new DataTable();
            dtItems = FlowMeterPro.GetOutCheck(ref a_strErr);

            if (dtItems == null)
                return "";
            if (dtItems.Rows.Count == 0)
                return "";

            string strCheckItems = GFun.Dt2Json("CheckItems", dtItems);
            return strCheckItems;
        }

        // 打印随工单-加载选中的检测项目
        public static DataTable GetOutCheckeds(string strRID)
        {
            return FlowMeterPro.GetOutCheckeds(strRID);
        }

        // 打印随工单-加载更换部件列表 
        public static string GetChangeBakList(string strRID, ref string a_strErr)
        {
            DataTable dtList = new DataTable();
            dtList = FlowMeterPro.GetChangeBakList(strRID, ref a_strErr);

            if (dtList == null) return "";
            if (dtList.Rows.Count == 0) return "";

            string strList = GFun.Dt2Json("ChangeBakList", dtList);
            return strList;
        }

        // 打印流转单-获取详细信息
        public static tk_TransCard getNewTransCard(string strID)
        {
            return FlowMeterPro.getNewTransCard(strID);
        }



        // 随工单管理-查询加载登记卡列表 
        public static UIDataTable LoadWorkCardList(int a_intPageSize, int a_intPageIndex, string where, string ModelType)
        {
            return FlowMeterPro.LoadWorkCardList(a_intPageSize, a_intPageIndex, where, ModelType);
        }

        // 随工单管理-查询超声波随工单列表
        public static UIDataTable LoadWorkCardListUT(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadWorkCardListUT(a_intPageSize, a_intPageIndex, where);
        }

        // 维修编号 流量计编号 型号 
        public static string getMeterInfo(string strRID)
        {
            return FlowMeterPro.getMeterInfo(strRID);
        }

        // 随工单管理-确认修改随工单 
        public static bool UpdateWorkCard(tk_WorkCard workCard, string Title, string Checked, string RID, string BakName,
            string BakType, string BakNum, string Comments, ref string strErr)
        {
            if (FlowMeterPro.UpdateWorkCard(workCard, Title, Checked, RID, BakName, BakType, BakNum, Comments, ref strErr) >= 1)
                return true;
            else
                return false;
        }

        // 超声波随工单管理-确认修改随工单 
        public static bool UpdateWorkCardUT(tk_WorkCardUT workCard, ref string strErr)
        {
            if (FlowMeterPro.UpdateWorkCardUT(workCard, ref strErr) >= 1)
                return true;
            else
                return false;
        }

        // 随工单管理-加载更换部件记录列表
        public static DataTable getChangeBakList2(string strRID, ref string a_strErr)
        {
            return FlowMeterPro.GetChangeBakList(strRID, ref a_strErr);
        }



        // 流转卡管理-查询加载流转卡列表 
        public static UIDataTable LoadTransCardList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadTransCardList(a_intPageSize, a_intPageIndex, where);
        }

        // 流转卡管理-查询加载超声波流转卡列表 
        public static UIDataTable LoadTransCardListUT(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadTransCardListUT(a_intPageSize, a_intPageIndex, where);
        }

        // 流转卡管理-确认修改流转卡
        public static bool UpdateTransCard(string TID, string RID, string FirstCheck, string SendRepair, string LastCheck, string OneRepair,
            string TwoCheck, string TwoRepair, string ThreeCheck, string ThreeRepair, string Comments, ref string strErr)
        {
            if (FlowMeterPro.UpdateTransCard(TID, RID, FirstCheck, SendRepair, LastCheck, OneRepair, TwoCheck, TwoRepair, ThreeCheck,
                    ThreeRepair, Comments, ref strErr) >= 1)
                return true;
            else
                return false;
        }



        // 下发任务-获取接收任务的小组列表 
        public static UIDataTable getGroupList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.getGroupList(a_intPageSize, a_intPageIndex, where);
        }

        // 下发任务-获取人员列表 
        public static UIDataTable getPersonList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.getPersonList(a_intPageSize, a_intPageIndex, where);
        }

        // 下发任务-确定下发任务 1.在下发任务表中添加小组和人员信息 2.在维修登记卡中填入完成时间，是否送外部检测
        public static bool SendTask(string RID, string FinishDate, string Checked, ref string a_strErr)
        {
            if (FlowMeterPro.SendTask(RID, FinishDate, Checked, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }



        // 确认收货-确定 
        public static bool CheckReceive(string RIDs, string TakeID, string UnitName, string DeliverName, string DeliverTel,
             string DeliverDate, string ReceiveName, string ReceiveTel, string ReceiveDate, ref string a_strErr)
        {
            if (FlowMeterPro.CheckReceive(RIDs, TakeID, UnitName, DeliverName, DeliverTel, DeliverDate, ReceiveName, ReceiveTel, ReceiveDate, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认收货-判断选中的数据是否已经进行过入库操作
        public static bool CheckStockInfo(string IDs, ref string a_strErr)
        {
            if (FlowMeterPro.CheckStockInfo(IDs, ref a_strErr) == 0)
                return true;
            else
                return false;
        }

        // 确认收货-入库-获取入库编码 
        public static string GetNewStockID()
        {
            return FlowMeterPro.GetNewStockID();
        }

        // 确认收货-入库-插入新的入库编码 
        public static string GetStockID()
        {
            return FlowMeterPro.GetStockID();
        }

        // 确认收货-确定入库
        public static bool AddStockInfo(tk_StockIn stockIn, string RIDs, ref string a_strErr)
        {
            if (FlowMeterPro.AddStockInfo(stockIn, RIDs, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }




        // 收货管理-查询
        public static UIDataTable LoadDeliveryList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadDeliveryList(a_intPageSize, a_intPageIndex, where);
        }

        // 收货管理-加载收货单详细列表
        public static UIDataTable LoadDetailList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadDetailList(a_intPageSize, a_intPageIndex, where);
        }

        // 备用
        public static tk_TakeDelivery getNewDelivery(string strTakeID)
        {
            return FlowMeterPro.getNewDelivery(strTakeID);
        }

        // 收货管理-确认修改 
        public static bool UpdateDelivery(tk_TakeDelivery takeDelivery, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateDelivery(takeDelivery, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 收货管理-撤销收货单
        public static bool ReDelivery(string strRIDs, ref string a_strErr)
        {
            if (FlowMeterPro.ReDelivery(strRIDs, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 收货确认单查看-加载列表
        public static string GetDeliveryInfo(string strTakeID, ref string a_strErr)
        {
            DataTable dtList = new DataTable();
            dtList = FlowMeterPro.GetDeliveryInfo(strTakeID, ref a_strErr);

            if (dtList == null) return "";
            if (dtList.Rows.Count == 0) return "";

            string strList = GFun.Dt2Json("DeliveryInfo", dtList);
            return strList;
        }

        // 收货管理-修改界面获取收货单信息
        public static tk_TakeDelivery getNewDelivery2(string TakeID)
        {
            return FlowMeterPro.getNewDelivery2(TakeID);
        }



        // 送检单管理-加载送检表列表 
        public static UIDataTable LoadInspecList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadInspecList(a_intPageSize, a_intPageIndex, where);
        }

        // 新建送检单-获取送检单编号
        public static string GetNewSID()
        {
            return FlowMeterPro.GetNewSID();
        }

        // 新建送检单-插入送检单编号
        public static string GetSID()
        {
            return FlowMeterPro.GetSID();
        }

        // 新建送检单-加载仪表列表 [登记卡中是否送检IsOut=1&&状态为7-维修完成]
        public static UIDataTable LoadMeterList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadMeterList(a_intPageSize, a_intPageIndex, where);
        }

        // 新建送检单-根据RID获取仪表信息 
        public static DataTable GetMeterDetail(string RID)
        {
            return FlowMeterPro.GetMeterDetail(RID);
        }

        // 新建送检单-确定提交
        public static bool InsertInspec(tk_InspecMain InspecMain, List<tk_InspecDetail> list, string[] OutUnit, ref string a_strErr)
        {
            if (FlowMeterPro.InsertInspec(InspecMain, list, OutUnit, ref a_strErr) > 0)
                return true;
            else
                return false;
        }

        // 修改送检单-通过SID获取送检单信息  
        public static tk_InspecMain getInspectBySID(string SID)
        {
            return FlowMeterPro.getInspectBySID(SID);
        }

        // 修改送检单-获取多条详细信息列表 
        public static DataTable GetInspecInfo(string SID)
        {
            return FlowMeterPro.GetInspecInfo(SID);
        }

        // 修改送检单-确认修改
        public static bool UpdateInspec(tk_InspecMain inspecMain, List<tk_InspecDetail> list, string strRIDs, string[] OutUnit, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateInspec(inspecMain, list, strRIDs, OutUnit, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 查看详细-获取第三方检定单位名称
        public static string getUnitInspec(string strSID)
        {
            return FlowMeterPro.getUnitInspec(strSID);
        }

        // 导出-获取明细列表 
        public static DataTable GetDetailInfo(string strWhere)
        {
            DataTable dtList = new DataTable();
            dtList = FlowMeterPro.GetDetailInfo(strWhere);

            if (dtList == null)
                return null;
            else
                return dtList;
        }

        // 查看详细-获取详细列表
        public static UIDataTable LoadInspecDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadInspecDetail(a_intPageSize, a_intPageIndex, where);
        }




        // 发货管理-查询
        public static UIDataTable LoadSendList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadSendList(a_intPageSize, a_intPageIndex, where);
        }

        // 发货管理-加载发货单详细列表
        public static UIDataTable LoadSendDetail(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadSendDetail(a_intPageSize, a_intPageIndex, where);
        }

        // 发货管理-确认修改 
        public static bool UpdateSendDelivery(tk_SendDelivery sendDelivery, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateSendDelivery(sendDelivery, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 发货管理-根据发货单号获取详细信息 
        public static tk_SendDelivery getNewSendDelivery(string DeliverID)
        {
            return FlowMeterPro.getNewSendDelivery(DeliverID);
        }

        // 发货管理-撤销发货单
        public static bool ReSendDelivery(string strRIDs, ref string a_strErr)
        {
            if (FlowMeterPro.ReSendDelivery(strRIDs, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 发货管理-获取发货单信息
        public static tk_SendDelivery getNewSendDelivery2(string DeliverID)
        {
            return FlowMeterPro.getNewSendDelivery2(DeliverID);
        }

        // 发货确认单查看-加载列表 
        public static string GetSendDeliveryInfo(string strDeliverID, ref string a_strErr)
        {
            DataTable dtList = new DataTable();
            dtList = FlowMeterPro.GetSendDeliveryInfo(strDeliverID, ref a_strErr);

            if (dtList == null) return "";
            if (dtList.Rows.Count == 0) return "";

            string strList = GFun.Dt2Json("SendDeliveryInfo", dtList);
            return strList;
        }

        // 确认发货-确定 
        public static bool CheckSendInfo(string RIDs, string DeliverID, string UnitName, string ReceiveName, string ReceiveTel,
            string ReceiveDate, string ReceiveAddr, string SendName, string SendTel, string SendDate, ref string a_strErr)
        {
            if (FlowMeterPro.CheckSendInfo(RIDs, DeliverID, UnitName, ReceiveName, ReceiveTel, ReceiveDate, ReceiveAddr, SendName, SendTel, SendDate, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 确认发货-判断选中的数据是否已经进行过出库记录操作
        public static bool CheckStockOutInfo(string IDs, ref string a_strErr)
        {
            if (FlowMeterPro.CheckStockOutInfo(IDs, ref a_strErr) == 0)
                return true;
            else
                return false;
        }

        // 确认发货-出库-获取出库编号 
        public static string GetNewOutID()
        {
            return FlowMeterPro.GetNewOutID();
        }

        // 确认发货-出库-插入新的出库编码 
        public static string GetOutID()
        {
            return FlowMeterPro.GetOutID();
        }

        // 确认发货-确定出库
        public static bool AddStockOutInfo(tk_StockOut stockOut, string RIDs, ref string a_strErr)
        {
            if (FlowMeterPro.AddStockOutInfo(stockOut, RIDs, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }




        // 上传附件 
        public static bool InsertNewFile(tk_FileUpload fileUp, byte[] fileByte, ref string a_strErr)
        {
            if (FlowMeterPro.InsertNewFile(fileUp, fileByte, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 主页-获取附件列表 
        public static UIDataTable LoadFileList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadFileList(a_intPageSize, a_intPageIndex, where);
        }

        // 主页-下载附件 
        public static DataTable GetNewDownloadFile(string id)
        {
            return FlowMeterPro.GetNewDownloadFile(id);
        }




        // 缴费单查询-界面加载缴费记录
        public static UIDataTable LoadPayList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadPayList(a_intPageSize, a_intPageIndex, where);
        }

        // 缴费单查询-获取设备信息
        public static UIDataTable LoadModelList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadModelList(a_intPageSize, a_intPageIndex, where);
        }

        // 缴费单查询-获取相关联的报价单
        public static UIDataTable LoadBJDList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadBJDList(a_intPageSize, a_intPageIndex, where);
        }

        // 缴费单管理-获取新的缴费单号 
        public static string GetNewPayID()
        {
            return FlowMeterPro.GetNewPayID();
        }

        // 缴费单管理-插入新的缴费单号 
        public static string GetPayID()
        {
            return FlowMeterPro.GetPayID();
        }

        // 新增缴费记录-加载为缴费完成状态的报价单列表 
        public static UIDataTable LoadBJDList1(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadBJDList1(a_intPageSize, a_intPageIndex, where);
        }

        // 新增缴费记录-确定新增缴费记录 
        public static bool AddNewPay(tk_Payment payment, string Checks, decimal LowPrice, ref string a_strErr)
        {
            if (FlowMeterPro.AddNewPay(payment, Checks, LowPrice, ref a_strErr) >= 2)
                return true;
            else
                return false;
        }

        // 查看缴费单详细-获取详细信息 
        public static tk_Payment getNewPayMent(string strPayID)
        {
            return FlowMeterPro.getNewPayMent(strPayID);
        }

        // 修改缴费单-加载相关报价单列表 
        public static UIDataTable LoadBJDList2(int a_intPageSize, int a_intPageIndex, string strQIDs)
        {
            return FlowMeterPro.LoadBJDList2(a_intPageSize, a_intPageIndex, strQIDs);
        }

        // 修改缴费单-获取应缴金额和欠费金额
        public static string getBJInfo(string strPayID)
        {
            return FlowMeterPro.getBJInfo(strPayID);
        }

        // 修改缴费单-确认修改
        public static bool UpdatePayment(tk_Payment payment, decimal LowPrice, ref string a_strErr)
        {
            if (FlowMeterPro.UpdatePayment(payment, LowPrice, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }

        // 缴费管理-确定撤销 
        public static bool RePayment(string strPayID, string strQIDs, ref string a_strErr)
        {
            if (FlowMeterPro.RePayment(strPayID, strQIDs, ref a_strErr) >= 1)
                return true;
            else
                return false;
        }






        // 配置-获取规格型号 
        public static List<SelectListItem> getConfigContent(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigContent(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            if (strType != "CardType")
            {
                SelListItem.Value = "";
                SelListItem.Text = "请选择";
                ListItem.Add(SelListItem);
            }
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 配置-获取规格型号 
        public static List<SelectListItem> getConfigContentNew(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigContentNew(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> getConfigContentNew2(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigContentNew2(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 配置-获取状态 
        public static List<SelectListItem> getConfigState(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigState(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 配置-获取第三方检定单位
        public static List<SelectListItem> getUnitList()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getUnitList();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["UnitID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["UnitName"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 配置-获取上传环节 
        public static List<SelectListItem> getUpType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getUpType();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 获取客户名称
        public static List<SelectListItem> GetCustomerName()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.GetCustomerName();
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();
                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        // 获取仪表类型
        public static string getType(string strType)
        {
            return FlowMeterPro.getType(strType);
        }




        #region //检测
        public static List<SelectListItem> getConfigContent2(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigContent(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["Text"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();

                ListItem.Add(SelListItem);
            }
            return ListItem;
        }

        public static List<SelectListItem> getConfigContent3(string strType)
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();
            DataTable dtDesc = FlowMeterPro.getConfigContent(strType);
            if (dtDesc == null)
            {
                return ListItem;
            }
            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            for (int i = 0; i < dtDesc.Rows.Count; i++)
            {
                SelListItem = new SelectListItem();
                SelListItem.Value = dtDesc.Rows[i]["SID"].ToString();
                SelListItem.Text = dtDesc.Rows[i]["Text"].ToString();

                ListItem.Add(SelListItem);
            }
            return ListItem;
        }
        //新增检测
        public static bool InsertCheckData(tk_CheckData data, ref string a_strErr)
        {

            if (FlowMeterPro.InsertCheckData(data, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //查询加载检测列表
        public static UIDataTable LoadCheckDataList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadCheckDataList(a_intPageSize, a_intPageIndex, where);
        }
        //检测表详情
        public static tk_CheckData getNewCheckData(string strID)
        {
            return FlowMeterPro.getNewCheckData(strID);
        }

        //检测表详情
        public static bool CheckData(string strID)
        {
            return FlowMeterPro.CheckData(strID);
        }
        //修改检测表
        public static bool UpdateCheckData(tk_CheckData data, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateCheckData(data, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //删除检测表
        public static bool DeleteCheckData(string id, ref string a_strErr)
        {
            if (FlowMeterPro.DeleteCheckData(id, ref a_strErr) > 0)
                return true;
            else
                return false;
        }


        //新增检测
        public static bool InsertCheckData2(tk_CheckData2 data, ref string a_strErr)
        {

            if (FlowMeterPro.InsertCheckData2(data, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //修改检测表
        public static bool UpdateCheckData2(tk_CheckData2 data, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateCheckData2(data, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //检测表详情
        public static tk_CheckData2 getNewCheckData2(string strID)
        {
            return FlowMeterPro.getNewCheckData2(strID);
        }
        //删除检测表
        public static bool DeleteCheckData2(string id, ref string a_strErr)
        {
            if (FlowMeterPro.DeleteCheckData2(id, ref a_strErr) > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region //清洗记录
        //开始清洗
        public static bool StartCleanRepair(tk_ProRecord pro)
        {
            return FlowMeterPro.StartCleanRepair(pro);

        }
        public static UIDataTable LoadCheckDataList2(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadCheckDataList2(a_intPageSize, a_intPageIndex, where);
        }
        //新增清洗记录
        public static bool InsertCleanRepair(tk_CleanRepair cleanRepair, ref string a_strErr)
        {

            if (FlowMeterPro.InsertCleanRepair(cleanRepair, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //查询加载清洗记录
        public static UIDataTable LoadCleanRepairList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadCleanRepairList(a_intPageSize, a_intPageIndex, where);
        }
        //清洗记录详情
        public static tk_CleanRepair getCleanRepair(string strID)
        {
            return FlowMeterPro.getCleanRepair(strID);
        }
        //清洗更换备件
        public static DataTable RepairChange(string where)
        {
            return FlowMeterPro.RepairChange(where);
        }
        //修改清洗记录
        public static bool UpdateCleanRepair(tk_CleanRepair cleanRepair, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateCleanRepair(cleanRepair, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //删除清洗记录
        public static bool DeleteCleanRepair(string id, ref string a_strErr)
        {
            if (FlowMeterPro.DeleteCleanRepair(id, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region //维修记录
        //开始维修
        public static bool StartRepair(tk_ProRecord pro)
        {
            return FlowMeterPro.StartRepair(pro);
        }
        //新增维修记录
        public static bool InsertRepairInfo(tk_RepairInfo cleanRepair, string BakName, string BakType, string Measure, string BakNum, ref string a_strErr)
        {

            if (FlowMeterPro.InsertRepairInfo(cleanRepair, BakName, BakType, Measure, BakNum, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //查询加载维修记录
        public static UIDataTable LoadRepairInfoList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadRepairInfoList(a_intPageSize, a_intPageIndex, where);
        }
        public static DataTable RepairDevice(string where)
        {
            return FlowMeterPro.RepairDevice(where);
        }
        //维修记录详情
        public static tk_RepairInfo getRepairInfo(string strID)
        {
            return FlowMeterPro.getRepairInfo(strID);
        }
        public static tk_RepairInfo getRepairInfo2(string strID)
        {
            return FlowMeterPro.getRepairInfo2(strID);
        }
        //修改维修记录
        public static bool UpdateRepairInfo(tk_RepairInfo repairInfo, string BakName, string BakType, string Measure, string BakNum, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateRepairInfo(repairInfo, BakName, BakType, Measure, BakNum, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //删除维修记录
        public static bool DeleteRepairInfo(string id, ref string a_strErr)
        {
            if (FlowMeterPro.DeleteRepairInfo(id, ref a_strErr) > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region //任务管理
        public static string GetKeyId(string key)
        {
            return FlowMeterPro.GetKeyId(key);
        }
        // 根究状态判断是否可以进行当前操作 
        public static bool Operate(int p, string rid, string type, ref string strErr)
        {
            return FlowMeterPro.Operate(p, rid, type, ref strErr);
        }
        //查看对应步骤记录过程
        public static tk_ProRecord getRepairInfo(string strID, string type)
        {
            return FlowMeterPro.getRepairInfo(strID, type);
        }
        //查看总记录过程
        public static UIDataTable LoadProcedureList(string Rid)
        {
            return FlowMeterPro.LoadProcedureList(Rid);
        }
        #endregion
        #region //报价
        public static DataTable GetComponent(string key)
        {
            return FlowMeterPro.GetComponent(key);
        }

        //报价类型
        public static List<SelectListItem> QuotationType()
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            SelectListItem SelListItem = new SelectListItem();
            SelListItem.Value = "";
            SelListItem.Text = "请选择";
            ListItem.Add(SelListItem);
            SelectListItem SelListItem2 = new SelectListItem();

            SelListItem2.Value = "清洗";
            SelListItem2.Text = "清洗";
            ListItem.Add(SelListItem2);

            SelectListItem SelListItem3 = new SelectListItem();
            SelListItem3.Value = "维修";
            SelListItem3.Text = "维修";
            ListItem.Add(SelListItem3);

            SelectListItem SelListItem4 = new SelectListItem();
            SelListItem4.Value = "检测";
            SelListItem4.Text = "检测";
            ListItem.Add(SelListItem4);

            SelectListItem SelListItem5 = new SelectListItem();
            SelListItem5.Value = "打压";
            SelListItem5.Text = "打压";
            ListItem.Add(SelListItem5);

            return ListItem;
        }
        //添加报价
        public static bool InsertQuotation(string RID, string type, string p, string DeviceName, string DeviceType, string Num, string UnitPrice, string TotalPrice, string Comments, string Measure, ref string a_strErr)
        {
            if (FlowMeterPro.InsertQuotation(RID, type, p, DeviceName, DeviceType, Num, UnitPrice, TotalPrice, Comments, Measure, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //修改
        public static bool UpdateQuotation(string RID, string type, string p, string DeviceName, string DeviceType, string Num, string UnitPrice, string TotalPrice, string Comments, string Measure, ref string a_strErr)
        {
            if (FlowMeterPro.UpdateQuotation(RID, type, p, DeviceName, DeviceType, Num, UnitPrice, TotalPrice, Comments, Measure, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //撤销
        public static bool DeleteQuotation(string id, ref string a_strErr)
        {
            if (FlowMeterPro.DeleteQuotation(id, ref a_strErr) > 0)
                return true;
            else
                return false;
        }
        //对应id对象详情
        public static tk_Quotation getQuotation(string id)
        {
            return FlowMeterPro.getQuotation(id);
        }
        public static UIDataTable LoadQuotationList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.LoadQuotationList(a_intPageSize, a_intPageIndex, where);
        }
        public static UIDataTable GetGenList(int a_intPageSize, int a_intPageIndex, string rid)
        {
            return FlowMeterPro.GetGenList(a_intPageSize, a_intPageIndex, rid);

        }
        public static UIDataTable GetGenList2(int a_intPageSize, int a_intPageIndex, string rid)
        {
            return FlowMeterPro.GetGenList2(a_intPageSize, a_intPageIndex, rid);

        }
        public static UIDataTable GetGenQtnList(int a_intPageSize, int a_intPageIndex, string where)
        {
            return FlowMeterPro.GetGenQtnList(a_intPageSize, a_intPageIndex, where);
        }
        #endregion

        #region 统计分析
        public static DataTable ScheduleDetail(string where, string type)
        {
            return FlowMeterPro.ScheduleDetail(where, type);

        }
        public static string getModels2(string strRID, string type)
        {
            return FlowMeterPro.getModels2(strRID, type);
        }
        public static DataTable LoadScheduleSummary(string where, string type)
        {
            return FlowMeterPro.LoadScheduleSummary(where, type);

        }
        public static DataTable LoadCheckDataSummary(string where)
        {
            return FlowMeterPro.LoadCheckDataSummary(where);
        }
        public static DataTable LoadCheckData2Summary(string where)
        {
            return FlowMeterPro.LoadCheckData2Summary(where);
        }
        public static DataTable LoadRepairInfoSummary(string where, string type)
        {
            return FlowMeterPro.LoadRepairInfoSummary(where, type);
        }
        #endregion




    }
}
