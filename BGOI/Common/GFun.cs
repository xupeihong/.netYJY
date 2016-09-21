using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;

namespace TECOCITY_BGOI
{
    public class GFun
    {

        //根据长度转换整数，不足前面补0
        public static string GetNum(int a_intNum, int a_intCount)
        {
            string strTemp = "";
            //补足0
            for (int i = 0; i < a_intCount - a_intNum.ToString().Length; i++)
            {
                strTemp += "0";
            }

            //连接text
            strTemp += a_intNum.ToString();

            //返回补足0的字符串
            return strTemp;
        }

        public static string Dt2Json(string jsonName, DataTable dt)
        {
            try
            {
                StringBuilder Json = new StringBuilder();
                if (jsonName != "")
                {
                    Json.Append("{\"" + jsonName + "\":[");
                }
                else
                    Json.Append("{[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]}");
                return Json.ToString();
            }
            catch (Exception ex)
            {
                GLog.LogError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  方法名：GFun：Dt2Json  异常：" + ex.Message);
                return "";
            }
        }

        //默认以，分割
        public static string str2SQLIN(string a_strInfo)
        {
            string strInfo = "";
            if (a_strInfo != "")
            {
                string[] strInfos = a_strInfo.Split(',');

                for (int i = 0; i < strInfos.Length; i++)
                {
                    if (strInfos[i] != "")
                    {
                        if (strInfo != "") strInfo += ",";
                        strInfo += "'" + strInfos[i] + "'";
                    }
                }
            }
            return strInfo;
        }
        public static List<dynamic> ToDynamicList(DataTable dt)
        {
            var result = new List<dynamic>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();
                    foreach (DataColumn col in dt.Columns)
                    {
                        obj.Add(col.ColumnName, row[col.ColumnName]);
                    }
                    result.Add(obj);
                }
            }
            return result;
        }

        public static String[] SplitString(String str, String strSeparator, int nSeparator)
        {
            if (nSeparator < 1) return null;

            String[] arStr = new String[nSeparator];

            int nCount = 0;

            int nLen = strSeparator.Length;
            int nStart = 0, nIndex;

            nIndex = str.IndexOf(strSeparator);
            while (nIndex >= 0)
            {
                if (nCount == nSeparator) return arStr;

                arStr[nCount] = str.Substring(nStart, nIndex - nStart);

                nStart = nIndex + nLen;
                nIndex = str.IndexOf(strSeparator, nStart);
                nCount++;
            }
            if (nCount < nSeparator)
            {
                arStr[nCount] = str.Substring(nStart);
            }

            return arStr;
        }

        public static String SplitFirst(String str, String strSeparator)
        {
            String strReturn = "";

            int nIndex = str.IndexOf(strSeparator);
            if (nIndex >= 0)
                strReturn = str.Substring(0, nIndex);
            else
                strReturn = str;

            return strReturn;
        }

        public static List<string> SplitString(String str, String strSeparator)
        {
            int nLen = strSeparator.Length;
            int nStart = 0, nIndex;

            List<string> oList = new List<string>();

            nIndex = str.IndexOf(strSeparator);
            while (nIndex >= 0)
            {
                oList.Add(str.Substring(nStart, nIndex - nStart));

                nStart = nIndex + nLen;
                nIndex = str.IndexOf(strSeparator, nStart);
            }
            oList.Add(str.Substring(nStart));

            return oList;
        }

        public static String MakeDataTimeString(DateTime oDateTime)
        {
            return oDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public static String MakeDataTimeString(Object oDateTime)
        {
            if (oDateTime == null) return "";

            String strTemp = "";
            try
            {
                DateTime dt = Convert.ToDateTime(oDateTime);
                strTemp = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
            }

            return strTemp;
        }

        public static String MakeDataString(DateTime oDateTime)
        {
            return oDateTime.ToString("yyyy-MM-dd");
        }


        public static String MakeDataString(Object oDateTime)
        {
            if (oDateTime == null) return "";

            String strTemp = "";
            try
            {
                DateTime dt = Convert.ToDateTime(oDateTime);
                strTemp = dt.ToString("yyyy-MM-dd");
            }
            catch
            {
            }

            return strTemp;
        }

        public static double SafeToDouble(String strValue)
        {
            if (strValue == "") return 0;

            double nReturn = 0;
            try
            {
                nReturn = Convert.ToDouble(strValue);
            }
            catch
            {
            }
            return nReturn;
        }

        public static double SafeToDouble(Object value)
        {
            double nReturn = 0;
            try
            {
                nReturn = Convert.ToDouble(value);
            }
            catch
            {
            }
            return nReturn;
        }

        public static float SafeToFloat(String strValue)
        {
            if (strValue == "") return 0;

            float nReturn = 0;
            try
            {
                nReturn = Convert.ToSingle(strValue);
            }
            catch
            {
            }
            return nReturn;
        }

        public static float SafeToFloat(Object value)
        {
            float nReturn = 0;
            try
            {
                nReturn = Convert.ToSingle(value);
            }
            catch
            {
            }
            return nReturn;
        }


        public static int SafeToInt32(String strValue)
        {
            if (strValue == "") return 0;

            int nReturn = 0;
            try
            {
                nReturn = Convert.ToInt32(strValue);
            }
            catch
            {
            }
            return nReturn;
        }

        public static int SafeToInt32(Object value)
        {
            int nReturn = 0;
            try
            {
                nReturn = Convert.ToInt32(value);
            }
            catch
            {
            }
            return nReturn;
        }
        public static bool IsInt32(Object value)
        {
            int nReturn = 0;
            try
            {
                nReturn = Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsDateTime(object value)
        {
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static String SafeToString(Object value)
        {
            String strReturn = "";
            try
            {
                strReturn = Convert.ToString(value);
                strReturn = strReturn.Trim();
            }
            catch
            {
            }
            return strReturn;
        }

        public static DateTime SafeToDateTime(String strValue)
        {
            DateTime dt = DateTime.MaxValue;
            try
            {
                dt = Convert.ToDateTime(strValue);
            }
            catch
            {
            }
            return dt;
        }

        public static DateTime SafeToDateTime(Object value)
        {
            DateTime dt = DateTime.MaxValue;
            try
            {
                dt = Convert.ToDateTime(value);
            }
            catch
            {
            }
            return dt;
        }
        public static Guid SafeToGuid(String strValue)
        {
            Guid dt = Guid.Empty;
            try
            {
                dt = Guid.Parse(strValue);
            }
            catch
            {
            }
            return dt;
        }

        public static String FromBase64String(String str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        public static String ToBase64String(String str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static String HTMLEncodeForString(String str)
        {
            //str = str.Replace("&", "&amp;");

            str = str.Replace("> ", "&gt;");
            str = str.Replace(" < ", "&lt;");

            str = str.Replace(" ", "&nbsp;&nbsp;");
            str = str.Replace(" ", "&nbsp;&nbsp;");

            str = str.Replace("\"", "&quot;");
            str = str.Replace("'", "&apos;");

            str = str.Replace("\r\n", "<BR>");
            str = str.Replace("\n", "<BR>");
            str = str.Replace("\r", "<BR>");

            return str;
        }


        /// <summary>
        /// 保存成功
        /// </summary>
        public const String SaveSuccess = "保存成功!";
        /// <summary>
        /// 保存失败
        /// </summary>
        public const String SaveFailure = "保存失败!";
        /// <summary>
        /// 删除失败
        /// </summary>
        public const String DeleteFailure = "删除失败!";
        /// <summary>
        /// 删除成功
        /// </summary>
        public const String DeleteSuccess = "删除成功!";
        /// <summary>
        /// 查询权限
        /// </summary>
        public const String RightQuery = "Query";
        /// <summary>
        /// 统计权限
        /// </summary>
        public const String RightReport = "Report";
        /// <summary>
        /// 编辑权限
        /// </summary>
        public const String RightEdit = "Edit";
        /// <summary>
        /// 处理任务权限
        /// </summary>
        public const String RightProcess = "Process";
        /// <summary>
        /// 客户端按钮

        /// </summary>
        public const String BtnButton = "button";
        /// <summary>
        /// 服务端按钮

        /// </summary>
        public const String BtnSubmit = "submit";
        /// <summary>
        /// 点击文本框弹出DIV框,需要的data字符串

        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static String GetIdNames(DataTable dt)
        {
            //返回字符串格式

            //    { id: "10", name: "请选择中国苹果" },
            //    { id: "20", name: "请选择中国香蕉" },
            //    { id: "30", name: "请选择中国西瓜" },
            //    { id: "40", name: "请选择中国桃子" },
            //    { id: "50", name: "请选择中国葡萄" }
            StringBuilder strNames = new StringBuilder();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != dt.Rows.Count - 1)
                        strNames.AppendFormat("{{ id:\"{0}\",name:\"{1}\"}},", dt.Rows[i]["ID"], dt.Rows[i]["CommonName"]);
                    else
                        strNames.AppendFormat("{{ id:\"{0}\",name:\"{1}\"}}", dt.Rows[i]["ID"], dt.Rows[i]["CommonName"]);
                }
            }
            return strNames.ToString();
        }

        /// <summary>
        /// 返回时间的随机数
        /// </summary>
        /// <returns></returns>
        public static String GetDateTimeStr()
        {
            DateTime now = DateTime.Now;
            String dateStr = now.Year.ToString() +
                now.Month.ToString() + now.Day.ToString() +
                now.Hour.ToString() + now.Minute.ToString() +
                now.Second.ToString() + now.Millisecond.ToString();
            return dateStr;
        }
    }
}
