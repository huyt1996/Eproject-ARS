using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARS_Main.Models;

namespace ARS_Main.Controllers
{
    public class FlightBookController : Controller
    {
        // GET: FlightBook
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.dcity = db.Flights.Select(l => l.From).Distinct().ToList();
            ViewBag.acity = db.Flights.Select(l => l.To).Distinct().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Search( string cityto, string cityfrom, string date1, string date2)
        {
            var c = db.Flights.Where(l => l.To.Equals(cityto) && l.From.Equals(cityfrom) && l.DepDate.Equals(date1) && l.ArDate.Equals(date2));
            ViewBag.ss = c;
            return View();
        }

    }
}