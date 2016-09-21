using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class CSAlarm
   {
       private string strOrderID;
       [DataField("OrderID", "varchar")]
       public string OrderID
       {
           get { return strOrderID; }
           set { strOrderID = value; }
       }
       private string strOperator;
       [DataField("Operator", "varchar")]
       public string Operator
       {
           get { return strOperator; }
           set { strOperator = value; }
       }
       private string strOperationContent;

       [DataField("OperationContent", "varchar")]
       public string OperationContent
       {
           get { return strOperationContent; }
           set { strOperationContent = value; }
       }
       private string strOperationTime;
       [DataField("OperationTime", "datetime")]
       public string OperationTime
       {
           get { return strOperationTime; }
           set { strOperationTime = value; }
       }
   }
}
