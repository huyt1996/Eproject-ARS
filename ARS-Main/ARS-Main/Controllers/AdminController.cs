using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARS_Main.Models;

namespace ARS_Main.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext c = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin l)
        {
            var x = c.AdminLogins.Where(a => a.AdminName == l.AdminName && a.Password == l.Password).ToList();
            if(x!=null)
            {
                return RedirectToAction("DeshBoard");
            }
            else
            {
                ViewBag.m = "Wrong User id or Password";
            }
            return View();
        }
        public ActionResult DeshBoard()
        {

            return View();
        }
    }
}