using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TECOCITY_BGOI.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Acc_Login model, string returnUrl, string customSelectForm)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != null)
                {
                    if (model.Password.Length >= 0)
                    {
                        if (Account.ValidateUserPWD(model.UserName, model.Password))
                        {
                            Acc_Account iAccount = Account.ValidateUser(model.UserName, model.Password);

                            if (iAccount != null)
                            {
                                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                                //GAccountInfo uGa = GAccount.GetAccountInfo(model.UserName, customSelectForm);
                                HttpContextBase httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
                                //设置Session
                                httpContext.Session[GAccount.KEY_CACHEUSER] = iAccount;
                                //DALogWarnMan.AddLog("用户登录", strUserName, strUnitName);
                                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                {

                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    //string Path = iAccount.Path;
                                    //if (Path.IndexOf(".31.") >= 0)
                                    //{
                                        return RedirectToAction("SelectModuleYS", "Main");
                                    //}
                                    //if (Path.IndexOf(".49.") >= 0)
                                    //{
                                    //    return RedirectToAction("SelectModuleFZ", "Main");
                                    //}
                                    //return RedirectToAction("SelectModule", "Main");
                                }
                            }
                            return null;
                        }
                        else
                        {
                            ModelState.AddModelError("err", "用户名或密码输入错误");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "登录失败，密码应至少8位");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "登录异常...");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "登录异常...");
                return View(model);
            }
        }
    }
}
