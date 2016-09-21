using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace TECOCITY_BGOI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //
            //String strHost = this.Request.Headers["host"];
            //if (!String.IsNullOrEmpty(strHost))
            //{
            //    bool bOk = false;
            //    if (strHost.IndexOf("localhost") >= 0)
            //    {
            //        bOk = true;
            //    }

            //    string strVHost = System.Configuration.ConfigurationManager.AppSettings["ValidateHost"];
            //    string[] strVHosts = strVHost.Split('-');
            //    for (int i = 0; i < strVHosts.Length; i++)
            //    {
            //        if (strHost.IndexOf(strVHosts[i]) >= 0)
            //        {
            //            bOk = true;
            //            break;
            //        }
            //    }

            //    if (!bOk)
            //    {
            //        Response.Redirect("~/Main/ErrorPage?p=出现错误,访问host非法");
            //        return;
            //    }
            //}

            //StartProcessRequest();
        }

        /// <summary> 
        /// 处理用户提交的请求 
        /// </summary> 
        private void StartProcessRequest()
        {
            try
            {
                string getkeys = "";

                if (System.Web.HttpContext.Current.Request.QueryString != null)
                {

                    for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                        if (GValidator.HasInjectionData(System.Web.HttpContext.Current.Request.QueryString[getkeys]))
                        {
                            System.Web.HttpContext.Current.Response.Redirect("~/Main/ErrorPage?p=出现错误,输入参数包含非法字符串");
                        }
                    }
                }
                if (System.Web.HttpContext.Current.Request.Form != null)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
                        if (getkeys == "__VIEWSTATE") continue;
                        if (GValidator.HasInjectionData(System.Web.HttpContext.Current.Request.Form[getkeys]))
                        {
                            System.Web.HttpContext.Current.Response.Redirect("~/Main/ErrorPage?p=出现错误,输入参数包含非法字符串");
                        }
                    }
                }
                if (System.Web.HttpContext.Current.Request.Cookies != null)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Cookies.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.Cookies.Keys[i];
                        if (getkeys == "__VIEWSTATE") continue;
                        if (GValidator.HasInjectionData(System.Web.HttpContext.Current.Request.Cookies[getkeys].Value))
                        {
                            System.Web.HttpContext.Current.Response.Redirect("~/Main/ErrorPage?p=出现错误,包含非法字符串");
                        }
                    }
                }

            }
            catch
            {
                // 错误处理: 处理用户提交信息! 
            }
        }
    }
}