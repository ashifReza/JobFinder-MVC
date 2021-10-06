using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevProject.Models;

namespace DevProject.Controllers
{
    public class AppliesController : Controller
    {
        private DevProjectDbEntities db = new DevProjectDbEntities();

        // GET: Applies
        public ActionResult Index()
        {
            var applies = db.Applies.Include(a => a.JobFinder).Include(a => a.JobPoster);
            return View(applies.ToList());
        }

        // GET: Applies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // GET: Applies/Create
        public ActionResult Create()
        {
            ViewBag.JobFindID = new SelectList(db.JobFinders, "JobFindID", "UserName");
            ViewBag.JobPostID = new SelectList(db.JobPosters, "JobPostID", "UserName");
            return View();
        }

        // POST: Applies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplyID,ApplyName,JobFindID,JobPostID")] Apply apply)
        {
            if (ModelState.IsValid)
            {
                db.Applies.Add(apply);
                db.SaveChanges();
                return RedirectToAction("Humm");
                
            }

            ViewBag.JobFindID = new SelectList(db.JobFinders, "JobFindID", "UserEmail", apply.JobFindID);
            ViewBag.JobPostID = new SelectList(db.JobPosters, "JobPostID", "JobName", apply.JobPostID);
            return View(apply);
        }

        public ActionResult Humm()
        {
            return View();
        }

        // GET: Applies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobFindID = new SelectList(db.JobFinders, "JobFindID", "UserName", apply.JobFindID);
            ViewBag.JobPostID = new SelectList(db.JobPosters, "JobPostID", "UserName", apply.JobPostID);
            return View(apply);
        }

        // POST: Applies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplyID,ApplyName,JobFindID,JobPostID")] Apply apply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobFindID = new SelectList(db.JobFinders, "JobFindID", "UserName", apply.JobFindID);
            ViewBag.JobPostID = new SelectList(db.JobPosters, "JobPostID", "UserName", apply.JobPostID);
            return View(apply);
        }


        //public ActionResult stuff()
        //{
        //    List<JobFinder> list = db.JobFinders.ToList();
        //    ViewBag.finderlist = new SelectList(list, "JobFindId", "UserEmail");
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult stuff(Apply e)
        //{
        //    List<JobFinder> list = db.JobFinders.ToList();
        //    ViewBag.finderlist = new SelectList(list, "JobFindId", "UserEmail");

        //    Apply app = new Apply();
        //    app.ApplyName = e.ApplyName;
        //    app.JobFindID = e.JobFindID;

        //    return View();
        //}


        // GET: Applies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apply apply = db.Applies.Find(id);
            if (apply == null)
            {
                return HttpNotFound();
            }
            return View(apply);
        }

        // POST: Applies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apply apply = db.Applies.Find(id);
            db.Applies.Remove(apply);
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
