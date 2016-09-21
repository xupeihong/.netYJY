using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHInstall
    {
       public string strAZID = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("AZID", "varchar")]
       //安装编号
       public string AZID
       {
           get { return strAZID; }
           set { strAZID = value; }
       }
        public string strBZID = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("BZID", "varchar")]
       //报装编号
       public string BZID
       {
           get { return strBZID; }
           set { strBZID = value; }
       }
       public string strUntiID = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("UntiID", "varchar")]
       //安装建立单位
       public string UntiID
       {
           get { return strUntiID; }
           set { strUntiID = value; }
       }
       public DateTime strInstallTime;
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("InstallTime", "datetime")]
       //安装时间
       public DateTime InstallTime
       {
           get { return strInstallTime; }
           set { strInstallTime = value; }
       }
       public string strInstallName = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("InstallName", "varchar")]
       //安装人员
       public string InstallName
       {
           get { return strInstallName; }
           set { strInstallName = value; }
       }
       public string strIsCharge = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsCharge", "varchar")]
       //是否收费
       public string IsCharge
       {
           get { return strIsCharge; }
           set { strIsCharge = value; }
       }
       public string strIsInvoice = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsInvoice", "varchar")]
       //是否开发票/收据
       public string IsInvoice
       {
           get { return strIsInvoice; }
           set { strIsInvoice = value; }
       }
       public string strReceiptType = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("ReceiptType", "varchar")]
       //收据类型，发票/收据
       public string ReceiptType
       {
           get { return strReceiptType; }
           set { strReceiptType = value; }
       }
       public string strRemark = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Remark", "varchar")]
       //备注
       public string Remark
       {
           get { return strRemark; }
           set { strRemark = value; }
       }
      
       public string strSureSatisfied = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("SureSatisfied", "varchar")]
       //确认客户满意度
       public string SureSatisfied
       {
           get { return strSureSatisfied; }
           set { strSureSatisfied = value; }
       }
       public string strIsProContent = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsProContent", "varchar")]
       //是否向用户说明包装内所含物品
       public string IsProContent
       {
           get { return strIsProContent; }
           set { strIsProContent = value; }
       }
       public string strIsWearClothes = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsWearClothes", "varchar")]
       //是否穿工作服
       public string IsWearClothes
       {
           get { return strIsWearClothes; }
           set { strIsWearClothes = value; }
       }
       public string strIsTeaching = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsTeaching", "varchar")]
       //是否指导用户使用及指导事项
       public string IsTeaching
       {
           get { return strIsTeaching; }
           set { strIsTeaching = value; }
       }
       public string strIsGifts = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsGifts", "varchar")]
       //是否接收用户赠与的物品
       public string IsGifts
       {
           get { return strIsGifts; }
           set { strIsGifts = value; }
       }
       public string strIsClean = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsClean", "varchar")]
       //工作完成后是否做好清洁工作
       public string IsClean
       {
           get { return strIsClean; }
           set { strIsClean = value; }
       }
       public string strIsUserSign = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsUserSign", "varchar")]
       //客户是否阅读安装单并签字
       public string IsUserSign
       {
           get { return strIsUserSign; }
           set { strIsUserSign = value; }
       }
       public string strCreateUser = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("CreateUser", "varchar")]
       //登记人
       public string CreateUser
       {
           get { return strCreateUser; }
           set { strCreateUser = value; }
       }
       public DateTime strCreateTime;
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("CreateTime", "datetime")]
       //创建时间
       public DateTime CreateTime
       {
           get { return strCreateTime; }
           set { strCreateTime = value; }
       }
       public string strValidate = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Validate", "varchar")]
       public string Validate
       {
           get { return strValidate; }
           set { strValidate = value; }
       }


       public string strAZCompany = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("AZCompany", "varchar")]
       public string AZCompany
       {
           get { return strAZCompany; }
           set { strAZCompany = value; }
       }

       public string strOrderID = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("OrderID", "varchar")]
       public string OrderID
       {
           get { return strOrderID; }
           set { strOrderID = value; }
       }
    }
}
