﻿using System.Web.Mvc;

namespace Ceriyo.Master.Auth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}