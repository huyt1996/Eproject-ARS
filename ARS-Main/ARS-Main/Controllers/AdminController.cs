using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARS_Main.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ARS_Main.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    return RedirectToAction("DashBoard"); ;
                }
                return View();
            }
            else
            {
                RedirectToAction("Index", "FlightBook");
            }
            return View();

        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
       
        
        public ActionResult DashBoard()
        {

            return View();
        }
    }
}