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
    public class QuestionResultFeedbacksController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: QuestionResultFeedbacks
        public ActionResult Index()
        {
            var questionResultFeedback = db.QuestionResultFeedback.Include(q => q.QuestionResult).Include(q => q.Teacher);
            return View(questionResultFeedback.ToList());
        }

        // GET: QuestionResultFeedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResultFeedback questionResultFeedback = db.QuestionResultFeedback.Find(id);
            if (questionResultFeedback == null)
            {
                return HttpNotFound();
            }
            return View(questionResultFeedback);
        }

        // GET: QuestionResultFeedbacks/Create
        public ActionResult Create(int? quesId)
        {
            ViewData["quesId"] = quesId;
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: QuestionResultFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionResultID,TeacherID,Text")] QuestionResultFeedback questionResultFeedback)
        {
            if (ModelState.IsValid)
            {
                db.QuestionResultFeedback.Add(questionResultFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionResultID = new SelectList(db.QuestionResult, "ID", "AnswerText", questionResultFeedback.QuestionResultID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", questionResultFeedback.TeacherID);
            return View(questionResultFeedback);
        }

        // GET: QuestionResultFeedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResultFeedback questionResultFeedback = db.QuestionResultFeedback.Find(id);
            if (questionResultFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionResultID = new SelectList(db.QuestionResult, "ID", "AnswerText", questionResultFeedback.QuestionResultID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", questionResultFeedback.TeacherID);
            return View(questionResultFeedback);
        }

        // POST: QuestionResultFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuestionResultID,TeacherID,Text")] QuestionResultFeedback questionResultFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionResultFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionResultID = new SelectList(db.QuestionResult, "ID", "AnswerText", questionResultFeedback.QuestionResultID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", questionResultFeedback.TeacherID);
            return View(questionResultFeedback);
        }

        // GET: QuestionResultFeedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResultFeedback questionResultFeedback = db.QuestionResultFeedback.Find(id);
            if (questionResultFeedback == null)
            {
                return HttpNotFound();
            }
            return View(questionResultFeedback);
        }

        // POST: QuestionResultFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionResultFeedback questionResultFeedback = db.QuestionResultFeedback.Find(id);
            db.QuestionResultFeedback.Remove(questionResultFeedback);
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
