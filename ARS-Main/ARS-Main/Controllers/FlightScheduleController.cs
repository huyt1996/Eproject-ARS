using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARS_Main.Models;

namespace ARS_Main.Controllers
{
    public class FlightScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FlightSchedule
        public ActionResult Index()
        {
            return View(db.TicketReserves.ToList());
        }

        // GET: FlightSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserves.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // GET: FlightSchedule/Create
        public ActionResult Create()
        {
            ViewBag.PlaneId = new SelectList(db.PlaneInfos, "PlaneID", "APlaneName");
            return View();
        }

        // POST: FlightSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResID,ResFrom,ResTo,ResDepDate,ResTime,PlaneId,PlaneSeat,ResTicketPrice,ResPlaneType")] TicketReserve_tbl ticketReserve_tbl)
        {
            ViewBag.PlaneId = new SelectList(db.PlaneInfos, "PlaneID", "APlaneName");
            if (ModelState.IsValid)
            {
                db.TicketReserves.Add(ticketReserve_tbl);
                db.SaveChanges();
                ViewBag.m = "Record Saved";
                return RedirectToAction("Index");
            }

            return View(ticketReserve_tbl);
        }

        // GET: FlightSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserves.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // POST: FlightSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResID,ResFrom,ResTo,ResDepDate,ResTime,PlaneId,PlaneSeat,ResTicketPrice,ResPlaneType")] TicketReserve_tbl ticketReserve_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketReserve_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketReserve_tbl);
        }

        // GET: FlightSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserves.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // POST: FlightSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserves.Find(id);
            db.TicketReserves.Remove(ticketReserve_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
