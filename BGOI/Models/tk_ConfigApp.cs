using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public  class tk_ConfigApp
    {
       private string strText;
        [DataFieldAttribute("Text", "nvarchar")]
       public string Text
        {
            get { return strText; }
            set { strText = value; }
        }
    }
}
