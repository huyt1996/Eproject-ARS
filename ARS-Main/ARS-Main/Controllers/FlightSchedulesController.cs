using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebARS.Models;

namespace ARS_Main.Controllers
{
    public class FlightSchedulesController : Controller
    {
        private MyDbConect db = new MyDbConect();

        // GET: FlightSchedules
        public ActionResult Index()
        {
            var flightSchedules = db.FlightSchedules.Include(f => f.Flight);
            return View(flightSchedules.ToList());
        }

        // GET: FlightSchedules/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightSchedule flightSchedule = db.FlightSchedules.Find(id);
            if (flightSchedule == null)
            {
                return HttpNotFound();
            }
            return View(flightSchedule);
        }

        // GET: FlightSchedules/Create
        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo");
            return View();
        }

        // POST: FlightSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightNo,FlightId,From,To,Arrival,Departure")] FlightSchedule flightSchedule)
        {
            if (ModelState.IsValid)
            {
                db.FlightSchedules.Add(flightSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightSchedule.FlightId);
            return View(flightSchedule);
        }

        // GET: FlightSchedules/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightSchedule flightSchedule = db.FlightSchedules.Find(id);
            if (flightSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightSchedule.FlightId);
            return View(flightSchedule);
        }

        // POST: FlightSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightNo,FlightId,From,To,Arrival,Departure")] FlightSchedule flightSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightSchedule.FlightId);
            return View(flightSchedule);
        }

        // GET: FlightSchedules/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightSchedule flightSchedule = db.FlightSchedules.Find(id);
            if (flightSchedule == null)
            {
                return HttpNotFound();
            }
            return View(flightSchedule);
        }

        // POST: FlightSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FlightSchedule flightSchedule = db.FlightSchedules.Find(id);
            db.FlightSchedules.Remove(flightSchedule);
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
