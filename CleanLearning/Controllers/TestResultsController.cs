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
    public class TestResultsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: TestResults
        public ActionResult Index()
        {
            var testResult = db.TestResult.Include(t => t.Student).Include(t => t.Test);
            return View(testResult.ToList());
        }

        // GET: TestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        public void TestResultsDropdown()
        {
            ViewBag.StudentID = new SelectList(db.Student, "ID", "FullName");
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name");
        }
        // GET: TestResults/Create
        public ActionResult Create()
        {
            TestResultsDropdown();
            return View();
        }

        // POST: TestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TestID,StudentID,Score")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.TestResult.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testResult.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testResult.TestID);
            return View(testResult);
        }

        // GET: TestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            TestResultsDropdown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testResult.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testResult.TestID);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TestID,StudentID,Score")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Student, "ID", "FirstName", testResult.StudentID);
            ViewBag.TestID = new SelectList(db.Test, "ID", "Name", testResult.TestID);
            return View(testResult);
        }

        // GET: TestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResult.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResult testResult = db.TestResult.Find(id);
            db.TestResult.Remove(testResult);
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

        public ActionResult ShowResultsPerModule(string moduleCode)
        {
            ViewData["title"] = moduleCode;
            var results = from tr in db.TestResult
                          join t in db.Test
                          on tr.TestID equals t.ID
                          where t.Module.ModuleCode == moduleCode
                          select tr;
            return View(results.ToList());
        }
    }
}
