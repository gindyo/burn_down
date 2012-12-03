using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PNUnit;

namespace BurnDown.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Dimitar's Project Management Application";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
