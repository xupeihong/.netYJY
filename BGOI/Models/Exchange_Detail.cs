using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class Exchange_Detail
    {
        private string strEID;
       [DataField("EID", "varchar")]
        public string EID
        {
            get { return strEID; }
            set { strEID = value; }
        }
       private string strDID;
       [DataField("DID","varchar")]
       public string DID
       {
           get { return strDID; }
           set { strDID = value; }
       }
       //订单详细产品编号
       private string strEDID;
       [DataField("EDID", "varchar")]
       public string EDID
       {
           get { return strEDID; }
           set { strEDID = value; }
       }
       private string strProductID;
       [DataField ("ProductID","varchar")]
       public string ProductID
       {
           get { return strProductID; }
           set { strProductID = value; }
       }
       private string strOrderContent;
       [DataField("OrderContent", "nvarchar")]
       public string OrderContent
       {
           get { return strOrderContent; }
           set { strOrderContent = value; }
       }
       private string strSpecifications;
       [DataField("Specifications", "nvarchar")]
       public string Specifications
       {
           get { return strSpecifications; }
           set { strSpecifications = value; }
       }
       private string strSupplier;
       [DataField("Supplier", "nvarchar")]
       public string Supplier
       {
           get { return strSupplier; }
           set { strSupplier = value; }
       }
       private string strUnit;
       [DataField("Unit","int")]
       public string Unit
       {
           get { return strUnit; }
           set { strUnit = value; }
       }

       private int strAmount;
       [DataField("Amount", "int")]
       public int Amount
       {
           get { return strAmount; }
           set { strAmount = value; }
       }
       //ExUnitNo,ExTotalNo,ExUnit,ExTotal ,Remark ,CreateTime ,CreateUser,Validate 
       private decimal strExUnitNo;
       [DataField("ExUnitNo", "varchar")]
       public decimal ExUnitNo
       {
           get { return strExUnitNo; }
           set { strExUnitNo = value; }
       }
       private decimal strExTotalNo;
       [DataField("ExTotalNo", "varchar")]
       public decimal ExTotalNo
       {
           get { return strExTotalNo; }
           set { strExTotalNo = value; }
       }
       private decimal strExUnit;
       [DataField("ExUnit", "varchar")]
       public decimal ExUnit
       {
           get { return strExUnit; }
           set { strExUnit = value; }
       }
       private decimal strExTotal;
       [DataField("ExTotal", "varchar")]
       public decimal ExTotal
       {
           get { return strExTotal; }
           set { strExTotal = value; }
       }
       private string strRemark;
       [DataField("Remark","varchar")]
       public string Remark
       {
           get { return strRemark; }
           set { strRemark = value; }
       }
       //CreateTime ,CreateUser,Validate 
       private string strCreateUser;
       [DataField("CreateUser", "varchar")]
       public string CreateUser
       {
           get { return strCreateUser; }
           set { strCreateUser = value; }
       }
       private DateTime strCreateTime;
       [DataField("CreateTime", "date")]
       public DateTime CreateTime
       {
           get { return strCreateTime; }
           set { strCreateTime = value; }
       }
       private string strValidate;
       [DataField("Validate", "varchar")]
       public string Validate
       {
           get { return strValidate; }
           set { strValidate = value; }
       }
    }
}
