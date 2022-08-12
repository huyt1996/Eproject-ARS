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
    public class FlightLogsController : Controller
    {
        private MyDbConect db = new MyDbConect();

        // GET: FlightLogs
        public ActionResult Index()
        {
            var flightLogs = db.FlightLogs.Include(f => f.Flight).Include(f => f.FlightSchedule);
            return View(flightLogs.ToList());
        }

        // GET: FlightLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightLog flightLog = db.FlightLogs.Find(id);
            if (flightLog == null)
            {
                return HttpNotFound();
            }
            return View(flightLog);
        }

        // GET: FlightLogs/Create
        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo");
            ViewBag.FlightNo = new SelectList(db.FlightSchedules, "FlightNo", "From");
            return View();
        }

        // POST: FlightLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LogDate,FlightNo,FlightId")] FlightLog flightLog)
        {
            if (ModelState.IsValid)
            {
                db.FlightLogs.Add(flightLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightLog.FlightId);
            ViewBag.FlightNo = new SelectList(db.FlightSchedules, "FlightNo", "From", flightLog.FlightNo);
            return View(flightLog);
        }

        // GET: FlightLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightLog flightLog = db.FlightLogs.Find(id);
            if (flightLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightLog.FlightId);
            ViewBag.FlightNo = new SelectList(db.FlightSchedules, "FlightNo", "From", flightLog.FlightNo);
            return View(flightLog);
        }

        // POST: FlightLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LogDate,FlightNo,FlightId")] FlightLog flightLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightId = new SelectList(db.Flights, "Id", "FlightNo", flightLog.FlightId);
            ViewBag.FlightNo = new SelectList(db.FlightSchedules, "FlightNo", "From", flightLog.FlightNo);
            return View(flightLog);
        }

        // GET: FlightLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightLog flightLog = db.FlightLogs.Find(id);
            if (flightLog == null)
            {
                return HttpNotFound();
            }
            return View(flightLog);
        }

        // POST: FlightLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightLog flightLog = db.FlightLogs.Find(id);
            db.FlightLogs.Remove(flightLog);
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
