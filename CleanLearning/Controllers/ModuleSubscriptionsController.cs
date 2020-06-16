using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleanLearning.Models;

namespace CleanLearning.Models
{
    public class ModuleSubscriptionsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: ModuleSubscriptions
        public ActionResult Index()
        {
            return View(db.ModuleSubscription.ToList());
        }

        // GET: ModuleSubscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleSubscription moduleSubscription = db.ModuleSubscription.Find(id);
            if (moduleSubscription == null)
            {
                return HttpNotFound();
            }
            return View(moduleSubscription);
        }

        public void ModuleSubscriptionDropdown()
        {
            ViewBag.studentList = new SelectList(db.Student, "ID", "FullName");
            ViewBag.moduleList = new SelectList(db.Module, "ID", "Name");
        }

        // GET: ModuleSubscriptions/Create
        public ActionResult Create()
        {
            ModuleSubscriptionDropdown();
            return View();
        }

        // POST: ModuleSubscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentID,ModuleID")] ModuleSubscription moduleSubscription)
        {
            if (ModelState.IsValid)
            {
                db.ModuleSubscription.Add(moduleSubscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moduleSubscription);
        }

        // GET: ModuleSubscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            ModuleSubscriptionDropdown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleSubscription moduleSubscription = db.ModuleSubscription.Find(id);
            if (moduleSubscription == null)
            {
                return HttpNotFound();
            }
            return View(moduleSubscription);
        }

        // POST: ModuleSubscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,ModuleID")] ModuleSubscription moduleSubscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleSubscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moduleSubscription);
        }

        // GET: ModuleSubscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleSubscription moduleSubscription = db.ModuleSubscription.Find(id);
            if (moduleSubscription == null)
            {
                return HttpNotFound();
            }
            return View(moduleSubscription);
        }

        // POST: ModuleSubscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModuleSubscription moduleSubscription = db.ModuleSubscription.Find(id);
            db.ModuleSubscription.Remove(moduleSubscription);
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
