﻿using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Known.Extensions;

namespace Known.Web.Controllers
{
    /// <summary>
    /// 用户控制器。
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// 登录页面。
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 注册页面。
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 忘记密码页面。
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        class menu
        {
            public string id { get; set; }
            public string text { get; set; }
            public string url { get; set; }
            public string iconCls { get; set; }
            public List<menu> children { get; set; }
        }
        /// <summary>
        /// 获取当前用户菜单数据。
        /// </summary>
        /// <returns>菜单数据。</returns>
        public ActionResult GetUserMenus()
        {
            var menus = System.IO.File.ReadAllText(Server.MapPath("~/menu.json")).FromJson<List<menu>>();
            return JsonResult(menus);
        }

        /// <summary>
        /// 登录验证。
        /// </summary>
        /// <param name="account">登录账号。</param>
        /// <param name="password">登录密码。</param>
        /// <param name="rememberMe">是否记住我。</param>
        /// <param name="returnUrl">登录成功后跳转的地址。</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn(string account, string password, bool rememberMe, string returnUrl)
        {
            account = account.ToLower();
            var result = Api.Post<dynamic>("/api/user/signin", new { account, password });
            if (result.status == 1)
                return ErrorResult(result.message);

            FormsAuthentication.SetAuthCookie(account, rememberMe);
            CurrentUser = result;

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = FormsAuthentication.DefaultUrl;

            return SuccessResult("登录成功，正在跳转页面......", returnUrl);
        }

        /// <summary>
        /// 退出系统。
        /// </summary>
        [HttpPost]
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
        }
    }
}