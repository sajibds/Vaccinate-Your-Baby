using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_vyb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return RedirectToAction("Login","User");
        }

        public ActionResult Contact()
        {


            return RedirectToAction("Registration", "User");
        }
    }
}