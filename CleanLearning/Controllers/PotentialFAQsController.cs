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
    public class PotentialFAQsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: PotentialFAQs
        public ActionResult Index()
        {
            return View(db.PotentialFAQ.ToList());
        }

        // GET: PotentialFAQs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialFAQ potentialFAQ = db.PotentialFAQ.Find(id);
            if (potentialFAQ == null)
            {
                return HttpNotFound();
            }
            return View(potentialFAQ);
        }

        // GET: PotentialFAQs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PotentialFAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WordCombination")] PotentialFAQ potentialFAQ)
        {
            if (ModelState.IsValid)
            {
                db.PotentialFAQ.Add(potentialFAQ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(potentialFAQ);
        }

        // GET: PotentialFAQs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialFAQ potentialFAQ = db.PotentialFAQ.Find(id);
            if (potentialFAQ == null)
            {
                return HttpNotFound();
            }
            return View(potentialFAQ);
        }

        // POST: PotentialFAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WordCombination")] PotentialFAQ potentialFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potentialFAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(potentialFAQ);
        }

        // GET: PotentialFAQs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotentialFAQ potentialFAQ = db.PotentialFAQ.Find(id);
            if (potentialFAQ == null)
            {
                return HttpNotFound();
            }
            return View(potentialFAQ);
        }

        // POST: PotentialFAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PotentialFAQ potentialFAQ = db.PotentialFAQ.Find(id);
            db.PotentialFAQ.Remove(potentialFAQ);
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
