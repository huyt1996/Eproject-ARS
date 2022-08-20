using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARS_Main.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ARS_Main.Controllers
{
    public class AeroPlaneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AeroPlane
        public ActionResult Index()
        {
            if (isAdminUser())
            {
                return View(db.Planes.ToList());               
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        
        }

        // GET: AeroPlane/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane PlaneInfo = db.Planes.Find(id);
            if (PlaneInfo == null)
            {
                return HttpNotFound();
            }
            return View(PlaneInfo);
        }

        // GET: AeroPlane/Create
        public ActionResult Create()
        {
            if (isAdminUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: AeroPlane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Planeid,APlaneName,SeatingCapacity,Price")] Plane PlaneInfo)
        {
            if (ModelState.IsValid)
            {
                db.Planes.Add(PlaneInfo);
                db.SaveChanges();
                ViewBag.m = "Record Saved";
                return View("");
            }

            return View(PlaneInfo);
        }

        // GET: AeroPlane/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane PlaneInfo = db.Planes.Find(id);
            if (PlaneInfo == null)
            {
                return HttpNotFound();
            }
            return View(PlaneInfo);
        }

        // POST: AeroPlane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Planeid,APlaneName,SeatingCapacity,Price")] Plane PlaneInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(PlaneInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(PlaneInfo);
        }

        // GET: AeroPlane/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane PlaneInfo = db.Planes.Find(id);
            if (PlaneInfo == null)
            {
                return HttpNotFound();
            }
            return View(PlaneInfo);
        }

        // POST: AeroPlane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plane aeroPlaneInfo = db.Planes.Find(id);
            db.Planes.Remove(aeroPlaneInfo);
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
    }
}
