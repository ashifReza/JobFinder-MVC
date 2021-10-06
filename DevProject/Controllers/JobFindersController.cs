using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevProject.Models;

namespace DevProject.Controllers
{
    public class JobFindersController : Controller
    {
        private DevProjectDbEntities db = new DevProjectDbEntities();

        // GET: JobFinders
        public ActionResult Index()
        {
            return View(db.JobFinders.ToList());
        }

        // GET: JobFinders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobFinder jobFinder = db.JobFinders.Find(id);
            if (jobFinder == null)
            {
                return HttpNotFound();
            }
            return View(jobFinder);
        }

        // GET: JobFinders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobFinders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobFindID,UserName,UserPass,ConfirmPass,Address,UserEmail,ConNumber,College,University,Experience,Resume")] JobFinder jobFinder )
        {
            //string filepath = Server.MapPath("~/Content/Hi.docx");
            //string filename = Path.GetFileName(filepath);
            //FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //byte[] bytes = br.ReadBytes((Int32)fs.Length);
            if (ModelState.IsValid)
            {
                db.JobFinders.Add(jobFinder);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(jobFinder);
        }

        // GET: JobFinders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobFinder jobFinder = db.JobFinders.Find(id);
            if (jobFinder == null)
            {
                return HttpNotFound();
            }
            return View(jobFinder);
        }

        // POST: JobFinders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobFindID,UserName,UserPass,Address,UserEmail,ConNumber,College,University,Experience,Resume")] JobFinder jobFinder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobFinder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobFinder);
        }


        //GET: Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TempUser tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.JobFinders.Where(u => u.UserName.Equals(tempUser.UserName)
                && u.UserPass.Equals(tempUser.UserPass) && u.UserEmail.Equals(tempUser.UserEmail)).FirstOrDefault();

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
            var user = db.JobFinders.Where(u => u.UserName.Equals(name)).FirstOrDefault();

            return View(user);
        }

        public ActionResult JobCircuit2(string search)
        {
            string name = Convert.ToString(Session["user_name"]);
            var user = db.JobFinders.Where(u => u.UserName.Equals(name)).FirstOrDefault();
            return View(db.JobPosters.Where(x => x.JobName.Contains(search) || x.JobDesc.Contains(search) || search == null).ToList());

        }

        public ActionResult DashBoard2()
        {
            return View(db.JobFinders.ToList());
        }



        // GET: JobFinders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobFinder jobFinder = db.JobFinders.Find(id);
            if (jobFinder == null)
            {
                return HttpNotFound();
            }
            return View(jobFinder);
        }

        // POST: JobFinders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobFinder jobFinder = db.JobFinders.Find(id);
            db.JobFinders.Remove(jobFinder);
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
