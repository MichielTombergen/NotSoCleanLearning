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
    public class ModuleRequirementsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: ModuleRequirements
        public ActionResult Index()
        {
            var moduleRequirements = db.ModuleRequirements.Include(m => m.Module).Include(m => m.Skill);
            return View(moduleRequirements.ToList());
        }

        // GET: ModuleRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleRequirements moduleRequirements = db.ModuleRequirements.Find(id);
            if (moduleRequirements == null)
            {
                return HttpNotFound();
            }
            return View(moduleRequirements);
        }

        // GET: ModuleRequirements/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode");
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName");
            return View();
        }

        // POST: ModuleRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ModuleID,SkillID")] ModuleRequirements moduleRequirements)
        {
            if (ModelState.IsValid)
            {
                db.ModuleRequirements.Add(moduleRequirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", moduleRequirements.ModuleID);
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", moduleRequirements.SkillID);
            return View(moduleRequirements);
        }

        // GET: ModuleRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleRequirements moduleRequirements = db.ModuleRequirements.Find(id);
            if (moduleRequirements == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", moduleRequirements.ModuleID);
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", moduleRequirements.SkillID);
            return View(moduleRequirements);
        }

        // POST: ModuleRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ModuleID,SkillID")] ModuleRequirements moduleRequirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleRequirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Module, "ID", "ModuleCode", moduleRequirements.ModuleID);
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", moduleRequirements.SkillID);
            return View(moduleRequirements);
        }

        // GET: ModuleRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleRequirements moduleRequirements = db.ModuleRequirements.Find(id);
            if (moduleRequirements == null)
            {
                return HttpNotFound();
            }
            return View(moduleRequirements);
        }

        // POST: ModuleRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModuleRequirements moduleRequirements = db.ModuleRequirements.Find(id);
            db.ModuleRequirements.Remove(moduleRequirements);
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
