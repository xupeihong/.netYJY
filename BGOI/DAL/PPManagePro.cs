using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PPManagePro
    {
        public static string GetNewPid()
        {
            string strCID = "";
            string strYMD = DateTime.Now.ToString("yyyyMMdd");
            string strSelPID = "select * from PurchaseApplication where DateRecord='" + strYMD + "'";
            DataTable dtPMaxID = SQLBase.FillTable(strSelPID, "MainPP");
            int intNewID = 0;
            if (dtPMaxID == null)
            {
                return strCID;
            }
            if (dtPMaxID.Rows.Count == 0)
            {
                string strInsertID = "insert into PIDNo (PID,PidNo,DateRecord) values('S',0,'" + strYMD + "')";
                SQLBase.ExecuteNonQuery(strInsertID, "SalesDBCnn");
                intNewID = 0;
            }
            else
            {
                intNewID = Convert.ToInt32(dtPMaxID.Rows[0]["PidNo"]);
            }

            intNewID++;
            string str = "select PID, PidNo,DateRecord from PIDNo where DateRecord='" + strYMD + "'";
            dtPMaxID = SQLBase.FillTable(strSelPID, "SalesDBCnn");
            strCID = dtPMaxID.Rows[0]["CID"].ToString() + DateTime.Now.ToString("yyyyMMdd") + GFun.GetNum(intNewID, 3);
            return strCID;
        }
    }
}
