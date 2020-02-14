﻿using Known.Web.Mvc;

namespace Known.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        [AllowAnonymous, Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Part()
        {
            return Partial("Partials/Page");
        }

        public ActionResult Download()
        {
            var content = System.Text.Encoding.Default.GetBytes("test");
            return File(content, "新建文本文档.txt");
        }
    }
}