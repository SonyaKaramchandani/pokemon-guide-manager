using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserTransLogsController : Controller
    {
        private BiodZebraEntities db = new BiodZebraEntities();

        // GET: DashboardPage/UserTransLogs
        public ActionResult Index()
        {
            return View(db.UserTransLogs.ToList());
        }

        // GET: DashboardPage/UserTransLogs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransLog userTransLog = db.UserTransLogs.Find(id);
            if (userTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userTransLog);
        }

        // GET: DashboardPage/UserTransLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/UserTransLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ModifiedUTCDatetime,ModificationDescription")] UserTransLog userTransLog)
        {
            if (ModelState.IsValid)
            {
                db.UserTransLogs.Add(userTransLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTransLog);
        }

        // GET: DashboardPage/UserTransLogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransLog userTransLog = db.UserTransLogs.Find(id);
            if (userTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userTransLog);
        }

        // POST: DashboardPage/UserTransLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,ModifiedUTCDatetime,ModificationDescription")] UserTransLog userTransLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTransLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTransLog);
        }

        // GET: DashboardPage/UserTransLogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTransLog userTransLog = db.UserTransLogs.Find(id);
            if (userTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userTransLog);
        }

        // POST: DashboardPage/UserTransLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserTransLog userTransLog = db.UserTransLogs.Find(id);
            db.UserTransLogs.Remove(userTransLog);
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
