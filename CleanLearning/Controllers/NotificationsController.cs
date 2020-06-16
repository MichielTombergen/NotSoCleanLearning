using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CleanLearning.Models
{
    public class NotificationsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: Notifications
        public ActionResult Index()
        {
            var notification = db.Notification.Include(n => n.Module).Include(n => n.Teacher);
            return View(notification.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notification.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode");
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ModuleID,TeacherID,Text")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notification.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", notification.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", notification.TeacherID);
            return View(notification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notification.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", notification.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", notification.TeacherID);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ModuleID,TeacherID,Text")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", notification.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", notification.TeacherID);
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notification.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notification.Find(id);
            db.Notification.Remove(notification);
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
