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
    public class JobPostersController : Controller
    {
        private DevProjectDbEntities db = new DevProjectDbEntities();

        // GET: JobPosters
        public ActionResult Index()
        {
            return View(db.JobPosters.ToList());
        }

        // GET: JobPosters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPoster jobPoster = db.JobPosters.Find(id);
            if (jobPoster == null)
            {
                return HttpNotFound();
            }
            return View(jobPoster);
        }

        // GET: JobPosters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobPosters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobPostID,UserName,UserPass,ConfirmPass,JobName,JobDesc,WorkDays,Salary,Skills,Age,Gender,Email,ConNumber")] JobPoster jobPoster)
        {
            if (ModelState.IsValid)
            {
                db.JobPosters.Add(jobPoster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobPoster);
        }

        //public ActionResult ShowApplied()
        //{
            
        //    List<JobPoster> posternames = db.JobPosters.ToList();
        //    List<Apply> appliernames = db.Applies.ToList();

        //    var multipletable = from jpn in posternames
        //                        join an in appliernames on jpn.JobPostID equals an.JobPostID into table1
        //                        from an in table1.DefaultIfEmpty()
        //                        select new PosterApplyJoin { JobPosterName = jpn, AppliedName = an };
        //          return View(multipletable);
        //}


        // GET: JobPosters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPoster jobPoster = db.JobPosters.Find(id);
            if (jobPoster == null)
            {
                return HttpNotFound();
            }
            return View(jobPoster);
        }

        // POST: JobPosters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobPostID,UserName,UserPass,JobName,JobDesc,WorkDays,Salary,Skills,Age,Gender,Email,ConNumber")] JobPoster jobPoster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPoster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobPoster);
        }

        //GET: Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TempUser2 tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.JobPosters.Where(u => u.UserName.Equals(tempUser.UserName)
                && u.UserPass.Equals(tempUser.UserPass) && u.Email.Equals(tempUser.Email)).FirstOrDefault();

                if (user != null)
                {
                    Session["user_name"] = user.UserName;
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ViewBag.LoginFailed = "User Not Found or Password Mismatched";
                    return View();
                }
            }
            return View();
        }

        public ActionResult DashBoard()
        {
            string name = Convert.ToString(Session["user_name"]);
            var user = db.JobPosters.Where(u => u.UserName.Equals(name)).FirstOrDefault();

            return View(user);
        }

        public ActionResult JobCircuit(string search)
        {
           
             
            return View(db.JobPosters.Where(x => x.JobName.Contains(search) || x.JobDesc.Contains(search)||search == null).ToList());
        }

        public ActionResult PosterSee()
        {
            var applies = db.Applies.Include(a => a.JobFinder).Include(a => a.JobPoster);
            return View(applies.ToList());
        }


        // GET: JobPosters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPoster jobPoster = db.JobPosters.Find(id);
            if (jobPoster == null)
            {
                return HttpNotFound();
            }
            return View(jobPoster);
        }

        // POST: JobPosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPoster jobPoster = db.JobPosters.Find(id);
            db.JobPosters.Remove(jobPoster);
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
