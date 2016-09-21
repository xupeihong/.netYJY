using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TECOCITY_BGOI
{
    public class GValidator
    {
        /// <summary>
        /// 验证是否存在注入代码(条件语句）,如果存在，返回true
        /// </summary>
        /// <param name="inputData"></param>
        public static bool HasInjectionData(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
                return false;

            //里面定义恶意字符集合
            //验证inputData是否包含恶意集合，如果包含，返回false
            return !Regex.IsMatch(inputData.ToLower(), GetInjectionRegexString());
         
        }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        /// <returns></returns>
        public static string GetInjectionRegexString()
        {
            string str_Regex = "^((?!and|select|insert|delete|update|drop|exec|count|declare|=).)*$";
            return str_Regex;
        }
    }
}
