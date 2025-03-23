using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ost_Assignment_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult About(string btnSubmit)
        {
            //ViewBag.Message = "Your application description page.";
            if (btnSubmit == "Save")
            {
                ViewBag.Message = "Save Triggered";
            }
            else if (btnSubmit == "Search")
            {
                ViewBag.Message = "Search Triggered";
            }
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}