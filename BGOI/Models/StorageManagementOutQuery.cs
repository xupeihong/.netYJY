﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class StorageManagementOutQuery
    {
        public string strListOutID = "";
        [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
        [DataFieldAttribute("ListOutID", "varchar")]
        //出库单号
        public string ListOutID
        {
            get { return strListOutID; }
            set { strListOutID = value; }
        }
    }
}
