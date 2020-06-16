using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CleanLearning.Models;

namespace CleanLearning.Controllers
{
    public class NewFAQsController : Controller
    {
        private Regex reg = new Regex(@"\?|\.|\,|\!");
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: NewFAQs
        public ActionResult Index()
        {
            var newFAQ = db.NewFAQ.Include(n => n.FAQ);
            return View(newFAQ.ToList());
        }

        // GET: NewFAQs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewFAQ newFAQ = db.NewFAQ.Find(id);
            if (newFAQ == null)
            {
                return HttpNotFound();
            }
            return View(newFAQ);
        }

        // GET: NewFAQs/Create
        public ActionResult Create()
        {
            ViewBag.AnsweredByFAQ = new SelectList(db.FAQ, "ID", "Question");
            return View();
        }

        // POST: NewFAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Question,Answered,AnsweredByFAQ")] NewFAQ newFAQ)
        {
            if (ModelState.IsValid)
            {
                db.NewFAQ.Add(newFAQ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnsweredByFAQ = new SelectList(db.FAQ, "ID", "Question", newFAQ.AnsweredByFAQ);
            return View(newFAQ);
        }
        // Start of findwords series, Looks to find a word that is used more than once.
        public ActionResult FindWords()
        {
            var newFAQ = db.NewFAQ.Include(n => n.FAQ);
            var wordList = new List<String>();
            foreach(var item in newFAQ) {
                string[] words = item.Question.Split(' ');
                foreach(var word in words){
                    wordList.Add(reg.Replace(word, ""));
                }
            }
            ViewBag.dict = FindWordsDict(wordList);
            return View();
        }

        //
        public Dictionary<String, int> FindWordsDict(List<String> wordList)
        {
            var wordCounter = new Dictionary<String, int>();
            foreach (var word in wordList)
            {
                if (wordCounter.ContainsKey(word.ToString()))
                {
                    wordCounter[word.ToString()] += 1;
                    if (wordCounter[word.ToString()] > 10)
                    {
                        FindWordsLevelTwo(word.ToString());
                    }
                }
                else
                {
                    wordCounter.Add(word.ToString(), 1);
                }
            }
            return wordCounter;
        }
        private void FindWordsLevelTwo(string word)
        {
            var newFAQ = db.NewFAQ.Include(n => n.FAQ);
            var indexes = new List<string>();
            foreach(var item in newFAQ) {
                Boolean hasWord = false;
                string[] words = item.Question.Split(' ');
                foreach(var wordd in words) {
                    var regWord = reg.Replace(wordd, "");
                    if (regWord.Equals(word)) {
                        hasWord = true;
                    }
                }
                if(hasWord) {
                    indexes.Add(item.Question);
                }
            }
            FindTwoWords(indexes);
        }
        private void FindTwoWords(List<string> sentences)
        {
            var wordCounter = new Dictionary<string, int>();
            foreach(var item in sentences)
            {
                var words = item.Split(' ');
                foreach(var word in words)
                {
                    var regWord = reg.Replace(word, "");
                    if (wordCounter.ContainsKey(regWord)){
                        wordCounter[regWord]++;
                    }
                    else{
                        wordCounter.Add(regWord, 1);
                    }
                }
            }
            CountWords(wordCounter);
        }

        private void CountWords(Dictionary<string, int> wordCounter)
        {
            int amount = 0;
            var repeatedWords = new List<string>();
            foreach(var value in wordCounter)
            {
                if(value.Value > 3)
                {
                    amount++;
                    repeatedWords.Add(value.Key);
                }
            }
        }

        // GET: NewFAQs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewFAQ newFAQ = db.NewFAQ.Find(id);
            if (newFAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnsweredByFAQ = new SelectList(db.FAQ, "ID", "Question", newFAQ.AnsweredByFAQ);
            return View(newFAQ);
        }

        // POST: NewFAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Question,Answered,AnsweredByFAQ")] NewFAQ newFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newFAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnsweredByFAQ = new SelectList(db.FAQ, "ID", "Question", newFAQ.AnsweredByFAQ);
            return View(newFAQ);
        }

        // GET: NewFAQs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewFAQ newFAQ = db.NewFAQ.Find(id);
            if (newFAQ == null)
            {
                return HttpNotFound();
            }
            return View(newFAQ);
        }

        // POST: NewFAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewFAQ newFAQ = db.NewFAQ.Find(id);
            db.NewFAQ.Remove(newFAQ);
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
