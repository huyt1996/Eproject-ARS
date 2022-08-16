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
            ViewBag.dcity = db.TicketReserves.Select(l => l.ResFrom).Distinct().ToList();
            ViewBag.acity = db.TicketReserves.Select(l => l.ResTo).Distinct().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Search( string cityto, string cityfrom, string date1)
        {
            var c = db.TicketReserves.Where(l => l.ResTo.Equals(cityto) && l.ResFrom.Equals(cityfrom) && l.ResDepDate.Equals(date1));
            ViewBag.ss = c;
            return View();
        }

        public ActionResult Booking( string fid)
        {
            int ids = int.Parse(fid);
            var a = db.TicketReserves.Where(l => l.ResID == ids).SingleOrDefault();
            ViewBag.name = a.ResID;
            ViewBag.id = fid;
            return View();
        }
    }
}