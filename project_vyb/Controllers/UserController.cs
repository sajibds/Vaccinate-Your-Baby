using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_vyb.Models;
using project_vyb.ViewModel;
namespace project_vyb.Controllers
{
    public class UserController : Controller
    {
        public VYBEntities dbcontext = new VYBEntities();
        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            user us = new user();
            return View(us);
        }
        [HttpPost]
        public ActionResult Registration(user usermodel)
        {
            dbcontext.users.Add(usermodel);
            dbcontext.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "Successfully Added";
            return View("Registration", new user());
        }

        [HttpGet]
        public ActionResult Login(project_vyb.Models.user ur)
        {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(login login)
        {
            //login ln = new login();
            var user = dbcontext.users.Where(u => u.username == login.username).FirstOrDefault();
            if (user != null)
            {
                if (user.password == login.password)
                {
                    Session["user"] = user.username;
                    return RedirectToAction("welcome");
                }
                else
                {
                    return View("wrong");
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult welcome()
        {
            string user = Session["user"].ToString();
            var upCom = CalculateUpcomming(user);
            var Due = GetDue(upCom);
            var given_vac = dbcontext.given_vaccine.Where(i => i.username == user).ToList();
            var ViewModel = new HomeView
            {
                given_Vaccines = given_vac,
                upcommings = upCom,
                due = Due,
            };
            return View(ViewModel);
        }


        public ActionResult wrong()
        {
            return View();
        }


        [NonAction]
        private IList<Upcomming> CalculateUpcomming(string user)
        {
            List<Upcomming> upcommings = new List<Upcomming>();
            var users = dbcontext.users.SingleOrDefault(u => u.username == user);
            var given = dbcontext.given_vaccine.Where(u => u.username == user).ToList();
            var total = dbcontext.vaccines.ToList();
            List<vaccine> Upcom = new List<vaccine>();
            foreach(var g in given)
            {
                foreach(var v in total)
                {
                    if (v.vac_id != g.vac_id)
                    {
                        Upcom.Add(v);
                    }
                }
            }
            foreach(var v in Upcom)
            {
                int days = v.due_months * 30;
                users.dob.AddDays(days);
                upcommings.Add(new Upcomming { VacineName=v.vac_name,Due_Time= users.dob.AddDays(days)
            });
            }
            var up= upcommings.OrderBy(x => x.Due_Time.TimeOfDay).ThenBy(x => x.Due_Time.Date).ThenBy(x => x.Due_Time.Month).ThenBy(x => x.Due_Time.Year);
            return up.ToList();
        }
        [NonAction]
        private IList<Upcomming> GetDue(IList<Upcomming> up)
        {
            List<Upcomming> du = new List<Upcomming>();
            DateTime Today = DateTime.Now;
            foreach(var v in up)
            {
                double dateDiff = (v.Due_Time.Date - Today.Date).TotalDays;
                if (dateDiff < 1)
                {
                    du.Add(v);
                }
            }
            return du;
        }
        public ActionResult logout()
        {
            Session["user"] = null;
            return View("Login");
        }

    }
}