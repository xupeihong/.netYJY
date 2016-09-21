using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_DebuggingSituation_HIS
    {
        public string strQKID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("QKID", "varchar")]
        //调试情况编号
        public string QKID
        {
            get { return strQKID; }
            set { strQKID = value; }
        }
        public string strTRID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("TRID", "varchar")]
        //调试任务编号
        public string TRID
        {
            get { return strTRID; }
            set { strTRID = value; }
        }
        public string strPID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PID", "varchar")]
        //产品编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strSpec = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("Spec", "varchar")]
        //规格型号
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
        public string strProductForm = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ProductForm", "varchar")]
        //产品形式   调压器（切，锅，衡 ）     箱    装置   单  双   切换   球   油  蝶
        public string ProductForm
        {
            get { return strProductForm; }
            set { strProductForm = value; }
        }
        public string strPowerNumber = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerNumber", "varchar")]
        //上台编号
        public string PowerNumber
        {
            get { return strPowerNumber; }
            set { strPowerNumber = value; }
        }
        public DateTime strPowerTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerTime", "datetime")]
        //上台生成日期
        public DateTime PowerTime
        {
            get { return strPowerTime; }
            set { strPowerTime = value; }
        }
        public string strPowerInitialP = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerInitialP", "varchar")]
        //上台初始P      kpa
        public string PowerInitialP
        {
            get { return strPowerInitialP; }
            set { strPowerInitialP = value; }
        }
        public string strStepDownNumber = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StepDownNumber", "varchar")]
        //下台编号
        public string StepDownNumber
        {
            get { return strStepDownNumber; }
            set { strStepDownNumber = value; }
        }
        public DateTime strStepDownTime;
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StepDownTime", "datetime")]
        //下台生成日期
        public DateTime StepDownTime
        {
            get { return strStepDownTime; }
            set { strStepDownTime = value; }
        }
        public string strStepDownInitialP = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("StepDownInitialP", "varchar")]
        //下台初始P      kpa
        public string StepDownInitialP
        {
            get { return strStepDownInitialP; }
            set { strStepDownInitialP = value; }
        }
        public string strInletPreP1 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("InletPreP1", "varchar")]
        //进口压力P1     Mpa
        public string InletPreP1
        {
            get { return strInletPreP1; }
            set { strInletPreP1 = value; }
        }
        public string strBleedingpreP1 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("BleedingpreP1", "varchar")]
        //放散压力P1     kpa
        public string BleedingpreP1
        {
            get { return strBleedingpreP1; }
            set { strBleedingpreP1 = value; }
        }
        public string strPowerExportPreP2 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerExportPreP2", "varchar")]
        //上台（高台）出口压力P2    kpa
        public string PowerExportPreP2
        {
            get { return strPowerExportPreP2; }
            set { strPowerExportPreP2 = value; }
        }
        public string strPowerOffPrePb = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerOffPrePb", "varchar")]
        //上台（高台）关闭压力Pb    kpa
        public string PowerOffPrePb
        {
            get { return strPowerOffPrePb; }
            set { strPowerOffPrePb = value; }
        }
        public string strPowerCutOffPrePq = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("PowerCutOffPrePq", "varchar")]
        //上台（高台）切断压力Pq    kpa
        public string PowerCutOffPrePq
        {
            get { return strPowerCutOffPrePq; }
            set { strPowerCutOffPrePq = value; }
        }
        public string strSDExportPreP2 = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SDExportPreP2", "varchar")]
        //下台（低台）出口压力P2    kpa
        public string SDExportPreP2
        {
            get { return strSDExportPreP2; }
            set { strSDExportPreP2 = value; }
        }
        public string strSDPowerOffPrePb = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SDPowerOffPrePb", "varchar")]
        //下台（低台）关闭压力Pb    kpa
        public string SDPowerOffPrePb
        {
            get { return strSDPowerOffPrePb; }
            set { strSDPowerOffPrePb = value; }
        }
        public string strSDCutOffPrePq = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("SDCutOffPrePq", "varchar")]
        //下台（低台）切断压力Pq    kpa
        public string SDCutOffPrePq
        {
            get { return strSDCutOffPrePq; }
            set { strSDCutOffPrePq = value; }
        }
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }
    }
}
