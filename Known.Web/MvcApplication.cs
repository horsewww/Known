﻿using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using Known.Core;
using Known.Extensions;
using Known.Log;

namespace Known.Web
{
    /// <summary>
    /// Mvc应用程序全局类。
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 日志组件。
        /// </summary>
        protected readonly ILogger logger = new ConsoleLogger();

        /// <summary>
        /// 应用程序启动事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Container.Register<IJson, JsonProvider>();
            InitializeApp();
        }

        /// <summary>
        /// 应用程序请求处理。
        /// </summary>
        protected virtual void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        /// <summary>
        /// 应用程序认证请求事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 应用程序开始请求事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            var checker = new XSSChecker(Request);
            if (Request.Cookies != null)
            {
                if (checker.CheckCookieData())
                    ErrorResult("您提交的数据有恶意字符！");
            }
            if (Request.UrlReferrer != null)
            {
                if (checker.CheckUrlReferer())
                    ErrorResult("您提交的数据有恶意字符！");
            }
            if (Request.RequestType.ToUpper() == "POST")
            {
                if (checker.CheckPostData())
                    ErrorResult("您提交的数据有恶意字符！");
            }
            if (Request.RequestType.ToUpper() == "GET")
            {
                if (checker.CheckGetData())
                    ErrorResult("您提交的数据有恶意字符！");
            }
        }

        private void ErrorResult(string message)
        {
            Response.Write(new { ok = false, message }.ToJson());
            Response.End();
        }

        /// <summary>
        /// 应用程序发生错误事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {
                var message = $"App发生异常，操作人：{User.Identity.Name}，地址：{HttpContext.Current.Request.Url}";
                logger.Error(message, ex);
                ErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 应用程序停止事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 应用程序建立会话事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Session_Start(object sender, EventArgs e)
        {
            var sessionId = Session.SessionID;
        }

        /// <summary>
        /// 应用程序会话结束事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Session_End(object sender, EventArgs e)
        {
        }

        private void InitializeApp()
        {
            var app = AppInfo.Instance;
            var context = Known.Context.Create();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes();
                if (types == null || types.Length == 0)
                    continue;

                foreach (var type in types)
                {
                    InitializeModule(app, context, type);
                }
            }
        }

        private static void InitializeModule(AppInfo app, Context context, Type type)
        {
            if (!type.IsSubclassOf(typeof(ModuleBase)))
                return;

            var module = Activator.CreateInstance(type) as ModuleBase;
            module.Init(app, context);

            var mi = ModuleInfo.Create(module);
            var methods = type.GetMethods();
            if (methods != null && methods.Length > 0)
            {
                foreach (var method in methods)
                {
                    if (method.Name.EndsWith("View"))
                    {
                        var view = method.Invoke(module, null) as PageView;
                        var pi = ModuleInfo.Create(view);
                        pi.Url = $"/Home/Page/{pi.Id}";
                        mi.AddChild(pi);
                        AppInfo.Instance.Pages.Add(pi);
                    }
                }
            }

            AppInfo.Instance.AddModule(mi);
        }
    }
}
