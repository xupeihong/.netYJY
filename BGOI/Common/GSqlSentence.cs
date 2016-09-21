using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;

namespace TECOCITY_BGOI
{
    public class GSqlSentence
    {
        public static T SetTValue<T>(T a_instT, HttpRequest a_Request) where T : class, new()
        {
            //TaskBasicInfo TaskBasic = GetTaskBasicInfo();
            try
            {
                Type types = typeof(T);
                PropertyInfo[] propertys = types.GetProperties();

                object[] objDataFieldAttribute = null;

                foreach (PropertyInfo property in propertys)
                {
                    objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                    if (objDataFieldAttribute != null)
                    {
                        string strFiledName = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
                        if (a_Request[strFiledName] != null)
                        {
                            object objContent = a_Request[strFiledName].ToString();
                            string s = property.PropertyType.ToString();
                            if (property.PropertyType.ToString() == "System.DateTime" || property.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                            {
                                if (objContent.ToString() != "")
                                    property.SetValue(a_instT, Convert.ChangeType(objContent, (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)), null);

                            }
                            else
                            {
                                if (objContent.ToString() != "")
                                    property.SetValue(a_instT, Convert.ChangeType(objContent, property.PropertyType), null);

                            }
                        }
                    }
                    //object strContent = a_TaskBasicInfo[property.Name];
                    //property.SetValue(a_instTask, strContent, null);
                }

                return a_instT;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return default(T);
            }
        }

        public static T SetTValueD<T>(T a_instT, System.Data.DataRow a_dtInfo) where T : class, new()
        {
            //TaskBasicInfo TaskBasic = GetTaskBasicInfo();
            try
            {
                Type types = typeof(T);
                PropertyInfo[] propertys = types.GetProperties();

                object[] objDataFieldAttribute = null;
                System.Data.DataRow drInfo = a_dtInfo;
                foreach (PropertyInfo property in propertys)
                {
                    objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                    if (objDataFieldAttribute != null)
                    {
                        string strFiledName = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
                        object d = drInfo[strFiledName];
                        if (drInfo[strFiledName] != null && drInfo[strFiledName] != System.DBNull.Value)
                        {
                            object objContent = drInfo[strFiledName].ToString();
                            if (property.PropertyType.ToString() == "System.DateTime" || property.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                            {
                                if (objContent.ToString() != "")
                                    property.SetValue(a_instT, Convert.ChangeType(objContent, (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)), null);

                            }
                            else
                            {
                                if (objContent.ToString() != "")
                                    property.SetValue(a_instT, Convert.ChangeType(objContent, property.PropertyType), null);
                            }
                        }
                    }
                    //object strContent = a_TaskBasicInfo[property.Name];
                    //property.SetValue(a_instTask, strContent, null);
                }

                return a_instT;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return null;
            }
        }

        //public static T SetTValue<T>(T a_instT, FormCollection a_fc) where T : class, new()
        //{
        //    //TaskBasicInfo TaskBasic = GetTaskBasicInfo();
        //    try
        //    {
        //        Type types = typeof(T);
        //        PropertyInfo[] propertys = types.GetProperties();

        //        object[] objDataFieldAttribute = null;

        //        foreach (PropertyInfo property in propertys)
        //        {
        //            objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
        //            if (objDataFieldAttribute != null)
        //            {
        //                string strFiledName = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
        //                if (a_fc[strFiledName] != null)
        //                {
        //                    object objContent = a_fc[strFiledName].ToString();
        //                    if (property.PropertyType.ToString() == "System.DateTime")
        //                    {
        //                        if (objContent.ToString() != "")
        //                            property.SetValue(a_instT, Convert.ChangeType(objContent, (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)), null);

        //                    }
        //                    else
        //                        property.SetValue(a_instT, Convert.ChangeType(objContent, property.PropertyType), null);
        //                }
        //            }
        //            //object strContent = a_TaskBasicInfo[property.Name];
        //            //property.SetValue(a_instTask, strContent, null);
        //        }

        //        return a_instT;
        //    }
        //    catch (Exception ex)
        //    {
        //        string ss = ex.Message;
        //        return default(T);
        //    }
        //}

        public static List<T> SetTValueList<T>(HttpRequest a_Request, int a_intlistCount) where T : class, new()
        {
            List<T> TaskUnits = new List<T>();
            try
            {
                Type types = typeof(T);
                PropertyInfo[] propertys = types.GetProperties();

                object[] objDataFieldAttribute = null;
                for (int i = 0; i < a_intlistCount; i++)
                {
                    T instT = new T();
                    foreach (PropertyInfo property in propertys)
                    {
                        objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                        string strFiledName = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
                        object strContent;
                        if (a_Request[strFiledName] != null)
                        {
                            if (a_Request[strFiledName].IndexOf(",") >= 0)
                            {
                                strContent = a_Request[strFiledName].Split(',')[i];
                                property.SetValue(instT, Convert.ChangeType(strContent, property.PropertyType), null);
                            }
                            else
                            {
                                strContent = a_Request[strFiledName];
                                if (property.PropertyType.ToString() == "System.DateTime" || property.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                                {
                                    if (strContent.ToString() != "")
                                        property.SetValue(instT, Convert.ChangeType(strContent, (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)), null);
                                    else
                                        property.SetValue(instT, Convert.ChangeType(DateTime.Now, (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType)), null);
                                }
                                else
                                {
                                    property.SetValue(instT, Convert.ChangeType(strContent, property.PropertyType), null);
                                }
                            }
                        }
                    }
                    TaskUnits.Add(instT);
                }
                return TaskUnits;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return null;
            }
        }

        #region - 获得插入类型的InsertSQL语句
        /// <summary>
        /// 获得插入类型的插入SQL语句（insert,values）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        public static string GetInsertInfo<T>(T Model) where T : class, new()
        {
            string str_InCol = "", str_InValues = "";
            string str_InsertModel = "";
            Type types = typeof(T);
            PropertyInfo[] propertys = types.GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                if (string.IsNullOrEmpty(str_InCol))
                {
                    str_InCol += property.Name;
                }
                else
                {
                    str_InCol += "," + property.Name;
                }
                if (!string.IsNullOrEmpty(str_InValues))
                {
                    str_InValues += ",";
                }

                if (property.PropertyType == typeof(System.String) || property.PropertyType == typeof(System.DateTime?) || property.PropertyType == typeof(System.DateTime))
                {
                    if (property.GetValue(Model, null) == null)
                    {
                        str_InValues += "null";
                    }
                    else
                    {
                        str_InValues += "'" + property.GetValue(Model, null) + "'";
                    }
                }
                else//如果是数字类型
                {
                    if (property.GetValue(Model, null) == null)
                    {
                        str_InValues += "null";
                    }
                    else
                    {
                        str_InValues += property.GetValue(Model, null).ToString();
                    }
                }
            }
            return str_InsertModel = "Insert " + Model.GetType().Name + "(" + str_InCol + ")" + "Values" + "(" + str_InValues + ")";
        }

        /// <summary>
        /// 获得插入类型的插入SQL语句（insert,values）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        public static string GetInsertInfoByD<T>(T Model, string a_strTableName) where T : class, new()
        {
            string str_InCol = "", str_InValues = "";
            string str_InsertModel = "";
            Type types = typeof(T);
            PropertyInfo[] propertys = types.GetProperties();
            object[] objDataFieldAttribute = null;
            foreach (PropertyInfo property in propertys)
            {
                objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);

                if (objDataFieldAttribute.Length > 0)
                {
                    if (string.IsNullOrEmpty(str_InCol))
                    {
                        str_InCol += ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
                    }
                    else
                    {
                        str_InCol += "," + ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;
                    }
                    if (!string.IsNullOrEmpty(str_InValues))
                    {
                        str_InValues += ",";
                    }

                    if (property.PropertyType == typeof(System.String) || property.PropertyType == typeof(System.DateTime?) || property.PropertyType == typeof(System.DateTime))
                    {
                        if (property.GetValue(Model, null) == null)
                        {
                            str_InValues += "null";
                        }
                        else
                        {
                            str_InValues += "'" + property.GetValue(Model, null) + "'";
                        }
                    }
                    else//如果是数字类型
                    {
                        if (property.GetValue(Model, null) == null)
                        {
                            str_InValues += "null";
                        }
                        else
                        {
                            str_InValues += property.GetValue(Model, null).ToString();
                        }
                    }
                }
            }
            return str_InsertModel = "Insert into " + a_strTableName + "(" + str_InCol + ")" + "Values" + "(" + str_InValues + ")";
        }
        #endregion - 获得插入类型的插入SQL语句

        #region - 获得保存类型的UpdateSQL语句
        /// <summary>
        /// 获得保存类型的更新SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        public static string GetUpdateInfo<T>(T Model) where T : class, new()
        {
            string str_UpdateCol = "", str_UpdateValues = "";
            string str_UpdateModel = "";
            int i_pk = 0;//
            Type types = typeof(T);
            PropertyInfo[] propertys = types.GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                //获得主键值
                if (property.Name.ToUpper() == "ID")
                {
                    i_pk = int.Parse(property.GetValue(Model, null).ToString());
                }

                str_UpdateCol = property.Name + "=";

                if (property.PropertyType == typeof(System.String) || property.PropertyType == typeof(System.DateTime?) || property.PropertyType == typeof(System.DateTime))
                {
                    //DateTime类型如果为空
                    if (property.GetValue(Model, null) == null)
                    {
                        str_UpdateValues = "null";
                    }
                    else
                    {
                        str_UpdateValues = "'" + property.GetValue(Model, null) + "'";
                    }
                }
                else
                {
                    if (property.GetValue(Model, null) == null)
                    {
                        str_UpdateValues = "null";
                    }
                    else
                    {
                        str_UpdateValues = property.GetValue(Model, null).ToString();
                    }
                }
                if (string.IsNullOrEmpty(str_UpdateModel))
                {
                    str_UpdateModel += str_UpdateCol + str_UpdateValues;
                }
                else
                {
                    str_UpdateModel += "," + str_UpdateCol + str_UpdateValues;
                }

            }

            return str_UpdateModel = "Update " + Model.GetType().Name + " set " + str_UpdateModel + " where id=" + i_pk.ToString();
        }

        /// <summary>
        /// 获得保存类型的更新SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        public static string GetUpdateInfoByD<T>(T Model, string a_strKeyID, string a_strTableName) where T : class, new()
        {
            string str_UpdateCol = "", str_UpdateValues = "";
            string str_UpdateModel = "";
            string strKeyValue = "";// i_pk = 0;//
            Type types = typeof(T);
            PropertyInfo[] propertys = types.GetProperties();
            object[] objDataFieldAttribute = null;
            foreach (PropertyInfo property in propertys)
            {
                objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                //获得主键值
                //if (property.Name.ToUpper() == "ID")
                if (((DataFieldAttribute)objDataFieldAttribute[0]).FieldName == a_strKeyID)
                {
                    strKeyValue = property.GetValue(Model, null).ToString();
                }

                //str_UpdateCol = property.Name + "=";
                str_UpdateCol = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName + "=";

                if (property.PropertyType == typeof(System.String) || property.PropertyType == typeof(System.DateTime?) || property.PropertyType == typeof(System.DateTime))
                {
                    //DateTime类型如果为空
                    if (property.GetValue(Model, null) == null)
                    {
                        str_UpdateValues = "null";
                    }
                    else
                    {
                        str_UpdateValues = "'" + property.GetValue(Model, null) + "'";
                    }
                }
                else
                {
                    if (property.GetValue(Model, null) == null)
                    {
                        str_UpdateValues = "null";
                    }
                    else
                    {
                        str_UpdateValues = property.GetValue(Model, null).ToString();
                    }
                }
                if (string.IsNullOrEmpty(str_UpdateModel))
                {
                    str_UpdateModel += str_UpdateCol + str_UpdateValues;
                }
                else
                {
                    str_UpdateModel += "," + str_UpdateCol + str_UpdateValues;
                }

            }

            return str_UpdateModel = "Update " + a_strTableName + " set " + str_UpdateModel + " where " + a_strKeyID + "='" + strKeyValue + "'";
        }
        #endregion - 获得插入类型的插入SQL语句

        public static string GetInsertByList<T>(List<T> a_Model, string a_strTableName) where T : class,new()
        {
            string strInsertModel = "";
            string strInsert = "";
            string strValues = "";
            Type types = typeof(T);
            PropertyInfo[] propertys = types.GetProperties();

            object[] objDataFieldAttribute = null;
            foreach (PropertyInfo property in propertys)
            {
                objDataFieldAttribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                if (objDataFieldAttribute.Length > 0)
                {
                    string strFiledName = ((DataFieldAttribute)objDataFieldAttribute[0]).FieldName;

                    if (strInsert != "") strInsert += ",";
                    strInsert += strFiledName;
                }
            }

            for (int i = 0; i < a_Model.Count; i++)
            {
                T ModelInfo = a_Model[i];
                string strV = "";
                if (strValues != "") strValues += " union select ";
                foreach (PropertyInfo property in propertys)
                {
                    if (!string.IsNullOrEmpty(strV))
                    {
                        strV += ",";
                    }
                    if (property.PropertyType == typeof(System.String) || property.PropertyType == typeof(System.DateTime?) || property.PropertyType == typeof(System.DateTime))
                    {
                        if (property.GetValue(ModelInfo, null) == null)
                        {
                            strV += "null";
                        }
                        else
                        {
                            strV += "'" + property.GetValue(ModelInfo, null) + "'";
                        }
                    }
                    else//如果是数字类型
                    {
                        if (property.GetValue(ModelInfo, null) == null)
                        {
                            strV += "null";
                        }
                        else
                        {
                            strV += property.GetValue(ModelInfo, null).ToString();
                        }
                    }
                }

                strValues += strV;
            }

            return strInsertModel = "Insert into " + a_strTableName + "(" + strInsert + ") select " + strValues;
        }
    }
}
