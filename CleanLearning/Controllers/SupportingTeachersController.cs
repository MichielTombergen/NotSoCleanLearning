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
    public class SupportingTeachersController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: SupportingTeachers
        public ActionResult Index()
        {
            var supportingTeacher = db.SupportingTeacher.Include(s => s.Module).Include(s => s.Teacher);
            return View(supportingTeacher.ToList());
        }

        // GET: SupportingTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportingTeacher supportingTeacher = db.SupportingTeacher.Find(id);
            if (supportingTeacher == null)
            {
                return HttpNotFound();
            }
            return View(supportingTeacher);
        }

        // GET: SupportingTeachers/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode");
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: SupportingTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ModuleID,TeacherID")] SupportingTeacher supportingTeacher)
        {
            if (ModelState.IsValid)
            {
                db.SupportingTeacher.Add(supportingTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", supportingTeacher.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", supportingTeacher.TeacherID);
            return View(supportingTeacher);
        }

        // GET: SupportingTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportingTeacher supportingTeacher = db.SupportingTeacher.Find(id);
            if (supportingTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", supportingTeacher.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", supportingTeacher.TeacherID);
            return View(supportingTeacher);
        }

        // POST: SupportingTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ModuleID,TeacherID")] SupportingTeacher supportingTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportingTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", supportingTeacher.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", supportingTeacher.TeacherID);
            return View(supportingTeacher);
        }

        // GET: SupportingTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportingTeacher supportingTeacher = db.SupportingTeacher.Find(id);
            if (supportingTeacher == null)
            {
                return HttpNotFound();
            }
            return View(supportingTeacher);
        }

        // POST: SupportingTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportingTeacher supportingTeacher = db.SupportingTeacher.Find(id);
            db.SupportingTeacher.Remove(supportingTeacher);
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
