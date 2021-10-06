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
    public class AdminnsController : Controller
    {
        private DevProjectDbEntities db = new DevProjectDbEntities();

        // GET: Adminns
        public ActionResult Index()
        {
            return View(db.Adminns.ToList());
        }

        // GET: Adminns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adminn adminn = db.Adminns.Find(id);
            if (adminn == null)
            {
                return HttpNotFound();
            }
            return View(adminn);
        }

        // GET: Adminns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adminns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,UserName,UserPass,UserEmail")] Adminn adminn)
        {
            if (ModelState.IsValid)
            {
                db.Adminns.Add(adminn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminn);
        }

        // GET: Adminns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adminn adminn = db.Adminns.Find(id);
            if (adminn == null)
            {
                return HttpNotFound();
            }
            return View(adminn);
        }

        // POST: Adminns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminID,UserName,UserPass,UserEmail")] Adminn adminn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminn);
        }

        public ActionResult Show()
        {
            return View();
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
                var user = db.Adminns.Where(u => u.UserName.Equals(tempUser.UserName)
                && u.UserPass.Equals(tempUser.UserPass) && u.UserEmail.Equals(tempUser.UserEmail)).FirstOrDefault();

                if (user != null)
                {
                   // Session["user_name"] = user.UserName;
                    return RedirectToAction("Show");
                }
                else
                {
                    ViewBag.LoginFailed = "User Not Found or Password Mismatched";
                    return View();
                }
            }
            return View();
        }


        // GET: Adminns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adminn adminn = db.Adminns.Find(id);
            if (adminn == null)
            {
                return HttpNotFound();
            }
            return View(adminn);
        }

        // POST: Adminns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adminn adminn = db.Adminns.Find(id);
            db.Adminns.Remove(adminn);
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
