using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
  public   class ExReturn_Detail
    {
        private string strEID;
      [DataField("EID", "varchar")]
        public string EID
        {
            get { return strEID; }
            set { strEID = value; }
        }
      private string strDID;
      [DataField("DID", "varchar")]
      public string DID
      {
          get { return strDID; }
          set { strDID = value; }
      }

      private string strEDID;
      [DataField("EDID", "varchar")]
      public string EDID
      {
          get { return strEDID; }
          set { strEDID = value; }
      }
      private string strProductID;
      [DataField("ProductID", "varchar")]
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
      [DataField("Unit", "nvarchar")]
      public string Unit
      {
          get { return strUnit; }
          set { strUnit = value; }
      }
      private int strAmount;
      [DataField ("Amount","int")]
      public int Amount
      {
          get { return strAmount; }
          set { strAmount = value; }
      }

      private decimal strPrice;
    
      [DataField("Price", "decimal")]
      public decimal Price
      {
          get { return strPrice; }
          set { strPrice = value; }
      }
      private decimal strSubtotal;
      [DataField("Subtotal", "decimal")]
      public decimal Subtotal
      {
          get { return strSubtotal; }
          set { strSubtotal = value; }
      }


      private decimal strUnitCost;
      [DataField("UnitCost", "decimal")]
      public decimal UnitCost
      {
          get { return strUnitCost; }
          set { strUnitCost = value; }
      }

      private decimal strTotalCost;
      [DataField("TotalCost", "decimal")]
      public decimal TotalCost
      {
          get { return strTotalCost; }
          set { strTotalCost = value; }
      }

      private string strSaleNo;
      [DataField("SaleNo", "varchar")]
      public string SaleNo
      {

          get { return strSaleNo; }
          set { strSaleNo = value; }
      }

      private string strProjectNo;
      [DataField("ProjectNo", "varchar")]
      public string ProjectNo
      {

          get { return strProjectNo; }
          set { strProjectNo = value; }

      }

      private string strJNAME;
      [DataField("JNAME", "varchar")]
      public string JNAME
      {
          get { return strJNAME; }
          set { strJNAME = value; }

      }




      private string strTechnology;
      
      [DataField("Technology", "nvarchar")]
      public string Technology
      {
          get { return strTechnology; }
          set { strTechnology = value; }
      }






      private decimal strExUnitNo;
      [DataField("ExUnitNo", "decimal")]
      public decimal ExUnitNo
      {
          get { return strExUnitNo; }
          set { strExUnitNo = value; }
      }
      private decimal strExTotalNo;
      [DataField("ExTotalNo", "decimal")]
      public decimal ExTotalNo
      {
          get { return strExTotalNo; }
          set { strExTotalNo = value; }
      }
      private decimal strExUnit;
      [DataField("ExUnit", "decimal")]
      public decimal ExUnit
      {
          get { return strExUnit; }
          set { strExUnit = value; }
      }
      private decimal strExTotal;
      [DataField("ExTotal", "decimal")]
      public decimal ExTotal
      {
          get { return strExTotal; }
          set { strExTotal = value; }
      }
      private string strReamrk;
      [DataField("Remark","varchar")]
      public string Reamrk
      {
          get { return strReamrk; }
          set { strReamrk = value; }
      }
      private string strCreateUser;
      [DataField("CreateUser","varchar")]
      public string CreateUser
      {
          get { return strCreateUser; }
          set { strCreateUser = value; }
      }
      private DateTime strCreateTime;
      [DataField("CreateTime","datetime")]
      public DateTime CreateTime
      {
          get { return strCreateTime; }
          set { strCreateTime = value; }
      }
      private string strValidate;
      [DataField("Validate","varchar")]
      public string Validate
      {
          get { return strValidate; }
          set { strValidate = value; }
      }
    }
}
