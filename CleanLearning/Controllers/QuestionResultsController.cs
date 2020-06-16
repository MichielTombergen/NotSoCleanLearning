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
    public class QuestionResultsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: QuestionResults
        public ActionResult Index()
        {
            return View(db.QuestionResult.ToList());
        }

        // GET: QuestionResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResult questionResult = db.QuestionResult.Find(id);
            if (questionResult == null)
            {
                return HttpNotFound();
            }
            return View(questionResult);
        }

        public void QuestionResultsDropdown()
        {
            ViewBag.questionList = new SelectList(db.Question, "ID", "QuestionText");
            ViewBag.studentList = new SelectList(db.Student, "ID", "FullName");
        }

        // GET: QuestionResults/Create
        public ActionResult Create()
        {
            QuestionResultsDropdown();
            return View();
        }

        // POST: QuestionResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionID,StudentID,AnswerText")] QuestionResult questionResult)
        {
            if (ModelState.IsValid)
            {
                db.QuestionResult.Add(questionResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionResult);
        }

        // GET: QuestionResults/Edit/5
        public ActionResult Edit(int? id)
        {
            QuestionResultsDropdown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResult questionResult = db.QuestionResult.Find(id);
            if (questionResult == null)
            {
                return HttpNotFound();
            }
            return View(questionResult);
        }

        // POST: QuestionResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuestionID,StudentID,AnswerText")] QuestionResult questionResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionResult);
        }

        // GET: QuestionResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResult questionResult = db.QuestionResult.Find(id);
            if (questionResult == null)
            {
                return HttpNotFound();
            }
            return View(questionResult);
        }

        // POST: QuestionResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionResult questionResult = db.QuestionResult.Find(id);
            db.QuestionResult.Remove(questionResult);
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
