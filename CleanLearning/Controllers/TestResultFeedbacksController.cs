using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleanLearning.Models;

namespace CleanLearning.Controllers
{
    public class TestResultFeedbacksController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: TestResultFeedbacks
        public ActionResult Index()
        {
            var testResultFeedback = db.TestResultFeedback.Include(t => t.Teacher).Include(t => t.TestResult);
            return View(testResultFeedback.ToList());
        }

        // GET: TestResultFeedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultFeedback testResultFeedback = db.TestResultFeedback.Find(id);
            if (testResultFeedback == null)
            {
                return HttpNotFound();
            }
            return View(testResultFeedback);
        }

        // GET: TestResultFeedbacks/Create
        public ActionResult Create(int? testResId)
        {
            ViewData["testResId"] = testResId;
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: TestResultFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TestResultID,TeacherID,Text,ReadStatus")] TestResultFeedback testResultFeedback)
        {
            if (ModelState.IsValid)
            {
                db.TestResultFeedback.Add(testResultFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", testResultFeedback.TeacherID);
            ViewBag.TestResultID = new SelectList(db.TestResult, "ID", "ID", testResultFeedback.TestResultID);
            return View(testResultFeedback);
        }

        // GET: TestResultFeedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultFeedback testResultFeedback = db.TestResultFeedback.Find(id);
            if (testResultFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", testResultFeedback.TeacherID);
            ViewBag.TestResultID = new SelectList(db.TestResult, "ID", "ID", testResultFeedback.TestResultID);
            return View(testResultFeedback);
        }

        // POST: TestResultFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TestResultID,TeacherID,Text,ReadStatus")] TestResultFeedback testResultFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResultFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", testResultFeedback.TeacherID);
            ViewBag.TestResultID = new SelectList(db.TestResult, "ID", "ID", testResultFeedback.TestResultID);
            return View(testResultFeedback);
        }

        // GET: TestResultFeedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultFeedback testResultFeedback = db.TestResultFeedback.Find(id);
            if (testResultFeedback == null)
            {
                return HttpNotFound();
            }
            return View(testResultFeedback);
        }

        // POST: TestResultFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResultFeedback testResultFeedback = db.TestResultFeedback.Find(id);
            db.TestResultFeedback.Remove(testResultFeedback);
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
