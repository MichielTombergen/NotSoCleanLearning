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
    public class TestFeedbacksController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: TestFeedbacks
        public ActionResult Index()
        {
            var testFeedback = db.TestFeedback.Include(t => t.Student).Include(t => t.Test);
            return View(testFeedback.ToList());
        }

        // GET: TestFeedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFeedback testFeedback = db.TestFeedback.Find(id);
            if (testFeedback == null)
            {
                return HttpNotFound();
            }
            return View(testFeedback);
        }

        // GET: TestFeedbacks/Create
        public ActionResult Create(int testresultID)
        {
            TestResult testResult = db.TestResult.Find(testresultID);
            ViewData["studID"] = testResult.StudentID;
            ViewData["testID"] = testResult.TestID;
            return View();
        }

        // POST: TestFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TestID,StudentID,Feedback")] TestFeedback testFeedback)
        {
            if (ModelState.IsValid)
            {
                db.TestFeedback.Add(testFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testFeedback.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testFeedback.TestID);
            return View(testFeedback);
        }

        // GET: TestFeedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFeedback testFeedback = db.TestFeedback.Find(id);
            if (testFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testFeedback.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testFeedback.TestID);
            return View(testFeedback);
        }

        // POST: TestFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TestID,StudentID,Feedback")] TestFeedback testFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testFeedback.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testFeedback.TestID);
            return View(testFeedback);
        }

        // GET: TestFeedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFeedback testFeedback = db.TestFeedback.Find(id);
            if (testFeedback == null)
            {
                return HttpNotFound();
            }
            return View(testFeedback);
        }

        // POST: TestFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestFeedback testFeedback = db.TestFeedback.Find(id);
            db.TestFeedback.Remove(testFeedback);
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
