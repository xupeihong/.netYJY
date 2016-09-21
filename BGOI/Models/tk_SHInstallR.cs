using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_SHInstallR
    {
      
       public string strBZID = "";
     
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("BZID", "varchar")]
       //报装编号
       public string BZID
       {
           get { return strBZID; }
           set { strBZID = value; }
       }

       //报装建立单位
       public string strUntiID = "";
    
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("UntiID", "varchar")]
       public string UntiID
       {
           get { return strUntiID; }
           set { strUntiID = value; }
       }

       //客户姓名
       public string strCustomerName = "";
     
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("CustomerName", "varchar")]
       public string CustomerName
       {
           get { return strCustomerName; }
           set { strCustomerName = value; }
       }

       public DateTime strInstallTime;
    
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("InstallTime", "datetime")]
       //报装时间
       public DateTime InstallTime
       {
           get { return strInstallTime; }
           set { strInstallTime = value; }
       }
      
       //地址
       public string strAddress = "";
      
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Address", "varchar")]
       public string Address
       {
           get { return strAddress; }
           set { strAddress = value; }
       }
       //联系方式
       public string strTel = "";
     
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Tel", "varchar")]
       public string Tel
       {
           get { return strTel; }
           set { strTel = value; }
       }
       //备注
       public string strRemark = "";
    
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Remark", "varchar")]
       public string Remark
       {
           get { return strRemark; }
           set { strRemark = value; }
       }
     
       //出库二级库房
       public string strWarehouseTwo = "";
    
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("WarehouseTwo", "varchar")]
       public string WarehouseTwo
       {
           get { return strWarehouseTwo; }
           set { strWarehouseTwo = value; }
       }
       //是否调拨
       public string strWIsWhether = "";
      
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("IsWhether", "varchar")]
       public string IsWhether
       {
           get { return strWIsWhether; }
           set { strWIsWhether = value; }
       }
       //调拨一级库房
       public string strWarehouseOne = "";
      
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("WarehouseOne", "varchar")]
       public string WarehouseOne
       {
           get { return strWarehouseOne; }
           set { strWarehouseOne = value; }
       }
       //登记人
       public string strCreateUser = "";
       
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("CreateUser", "varchar")]
       public string CreateUser
       {
           get { return strCreateUser; }
           set { strCreateUser = value; }
       }
        //, , , , , ，,
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
       //状态
       public string strSate = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("Sate", "varchar")]
       public string Sate
       {
           get { return strSate; }
           set { strSate = value; }
       }

       //关联ID
       public string strRelationID = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("RelationID", "varchar")]
       public string RelationID
       {
           get { return strRelationID; }
           set { strRelationID = value; }
       }

       //分公司
       public string strBZCompany = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("BZCompany", "varchar")]
       public string BZCompany
       {
           get { return strBZCompany; }
           set { strBZCompany = value; }
       }

       //派工人员
       public string strDiPer = "";
       [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
       [DataFieldAttribute("DiPer", "varchar")]
       public string DiPer
       {
           get { return strDiPer; }
           set { strDiPer = value; }
       }
    }
}
