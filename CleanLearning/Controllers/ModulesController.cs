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
    public class ModulesController : Controller
    {
        private michiyf248_clEntities db = new michiyf248_clEntities();

        // GET: Modules
        public ActionResult Index()
        {
            return View(db.Module.ToList());
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }
        public void ModulesDropdown()
        {
            ViewBag.teacherList = new SelectList(db.Teacher, "ID", "FirstName");
        }
        // GET: Modules/Create
        public ActionResult Create()
        {
            ModulesDropdown();
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ModuleCode,Teacher,Name")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Module.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            ModulesDropdown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ModuleCode,Teacher,Name")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Module module = db.Module.Find(id);
            db.Module.Remove(module);
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
        public ActionResult ShowSupportingTeacherPerModule(int moduleID)
        {
            ViewData["title"] = moduleID;
            var innerQuery = from st in db.SupportingTeacher where st.ModuleID == moduleID select st.TeacherID;
            var innerQuery2 = from module in db.Module where module.ID == moduleID select module.Teacher;
            var results = from tr in db.Teacher
                          join t in db.TeacherSkill
                          on tr.ID equals t.TeacherID
                          join x in db.ModuleRequirements
                          on t.SkillID equals x.SkillID
                          where x.Module.ID == moduleID && !innerQuery.Contains(tr.ID) && !innerQuery2.Contains(tr.ID)
                          select tr;
            return View(results.ToList());
        }

        public ActionResult AddSupportingTeacher(int moduleID, int teacherID)
        {
            SupportingTeacher teacher = new SupportingTeacher();
            teacher.ModuleID = moduleID;
            teacher.TeacherID = teacherID;
            if (ModelState.IsValid)
            {
                db.SupportingTeacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
