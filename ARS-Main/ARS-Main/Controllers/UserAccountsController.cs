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
    public class UserAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(db.UserLogins.ToList());
        }

        // GET: UserAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.UserLogins.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserRoles,UserName,FirstName,LastName,Address,Email,PhoneNumber,Password,ConfirmPassword,Gender,Age,CreditCardNumber,SkyMile")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                db.UserLogins.Add(registerViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerViewModel);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.UserLogins.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserRoles,UserName,FirstName,LastName,Address,Email,PhoneNumber,Password,ConfirmPassword,Gender,Age,CreditCardNumber,SkyMile")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerViewModel);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.UserLogins.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterViewModel registerViewModel = db.UserLogins.Find(id);
            db.UserLogins.Remove(registerViewModel);
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
