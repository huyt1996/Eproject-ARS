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
        //Get: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            if(Session["u"]!=null)
            {
                return RedirectToAction("DeshBoard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin l)
        {
            var x = c.AdminLogins.Where(a => a.AdminName == l.AdminName && a.Password == l.Password).FirstOrDefault();
            if(x!=null)
            {
                Session["u"] = l.AdminName;
                return RedirectToAction("DashBoard");
            }
            else
            {
                ViewBag.m = "Wrong User id or Password";
            }
            return View();
        }
        public ActionResult DashBoard()
        {

            return View();
        }
    }
}