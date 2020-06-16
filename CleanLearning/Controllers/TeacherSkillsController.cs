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
    public class TeacherSkillsController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: TeacherSkills
        public ActionResult Index()
        {
            var teacherSkill = db.TeacherSkill.Include(t => t.Skill).Include(t => t.Teacher);
            return View(teacherSkill.ToList());
        }

        // GET: TeacherSkills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSkill teacherSkill = db.TeacherSkill.Find(id);
            if (teacherSkill == null)
            {
                return HttpNotFound();
            }
            return View(teacherSkill);
        }

        // GET: TeacherSkills/Create
        public ActionResult Create()
        {
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName");
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: TeacherSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeacherID,SkillID,PersonID")] TeacherSkill teacherSkill)
        {
            if (ModelState.IsValid)
            {
                db.TeacherSkill.Add(teacherSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", teacherSkill.SkillID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", teacherSkill.TeacherID);
            return View(teacherSkill);
        }

        // GET: TeacherSkills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSkill teacherSkill = db.TeacherSkill.Find(id);
            if (teacherSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", teacherSkill.SkillID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", teacherSkill.TeacherID);
            return View(teacherSkill);
        }

        // POST: TeacherSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeacherID,SkillID,PersonID")] TeacherSkill teacherSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SkillID = new SelectList(db.Skill, "ID", "SkillName", teacherSkill.SkillID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", teacherSkill.TeacherID);
            return View(teacherSkill);
        }

        // GET: TeacherSkills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSkill teacherSkill = db.TeacherSkill.Find(id);
            if (teacherSkill == null)
            {
                return HttpNotFound();
            }
            return View(teacherSkill);
        }

        // POST: TeacherSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherSkill teacherSkill = db.TeacherSkill.Find(id);
            db.TeacherSkill.Remove(teacherSkill);
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
        public ActionResult ShowSkillsPerTeacher(int teacherID)
        {
            var results = from ts in db.TeacherSkill
                          where ts.TeacherID == teacherID
                          select ts;
            ViewData["title"] = results.First().Teacher.FirstName;
            return View(results.ToList());
        }
    }
}
