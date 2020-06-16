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
    public class QuestionsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();
        

        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Question.ToList());
        }


        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        public void QuestionController()
        {
            ViewBag.testList = new SelectList(db.Test.ToList(), "ID", "Name");
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            QuestionController();
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TestID,QuestionText,Answer,Scoring")] Question question)
        {
            var max = 1;
                       
            if (ModelState.IsValid)
            {
                
                
                string text = Request.Form["AnswerText"];
                string[] answers = text.Split(',');
                string correct = Request.Form["CorrectAnswer"];
                string[] bools = correct.Split(',');

                int count = 0;
                if (db.Question.Count() != 0)
                {
                    max = db.Question.Max(q => q.ID);
                }
                string vraagText = "";
                while (count <= answers.Length - 1)
                {

                    AnswerOption answerOption = new AnswerOption();

                    answerOption.AnswerText = answers[count];
                    answerOption.QuestionID = max + 1;


                    if (bools[count] == "1")
                    {
                        answerOption.CorrectAnswer = 1;
                        vraagText = answerOption.AnswerText;
                    }
                    else if (bools[count] == "0")
                    {
                        answerOption.CorrectAnswer = 0;
                    }

                    //Here used a query 
                    string queryText = "INSERT INTO AnswerOption(Question_ID, CorrectAnswer, AnswerText) VALUES ('" + answerOption.QuestionID + "', '" + answerOption.CorrectAnswer + "','" + answerOption.AnswerText + "')";
                    db.Database.ExecuteSqlCommand(queryText);
                    count++;
                }
                question.Answer = vraagText;
                db.Question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            QuestionController();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TestID,QuestionText,Answer,Scoring")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Question.Find(id);
            db.Question.Remove(question);
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

        // GET: Questions/feedback
        public ActionResult Feedback()
        {
            return View(db.QuestionFeedback.ToList());
        }

        // GET: Questions/feedbackPerTest
        public ActionResult FeedbackPerTest(int? testId)
        {
            Test test = db.Test.Find(testId);
            ViewData["Name"] = test.Name;
            var result = from tr in db.QuestionFeedback 
                         join q in db.Question 
                         on tr.QuestionID 
                         equals q.ID 
                         where q.TestID == testId 
                         select tr;
            return View(result.ToList());
        }

        public ActionResult AddAnswerOption()
        {
            return PartialView("_QuestionPartial");
        }

    }
}

