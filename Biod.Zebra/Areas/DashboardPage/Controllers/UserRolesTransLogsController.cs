using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesTransLogsController : Controller
    {
        private BiodZebraEntities db = new BiodZebraEntities();

        // GET: DashboardPage/UserRolesTransLogs
        public ActionResult Index()
        {
            return View(db.UserRolesTransLogs.ToList());
        }

        // GET: DashboardPage/UserRolesTransLogs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesTransLog userRolesTransLog = db.UserRolesTransLogs.Find(id);
            if (userRolesTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userRolesTransLog);
        }

        // GET: DashboardPage/UserRolesTransLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/UserRolesTransLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,ModifiedUTCDatetime,Description")] UserRolesTransLog userRolesTransLog)
        {
            if (ModelState.IsValid)
            {
                db.UserRolesTransLogs.Add(userRolesTransLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRolesTransLog);
        }

        // GET: DashboardPage/UserRolesTransLogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesTransLog userRolesTransLog = db.UserRolesTransLogs.Find(id);
            if (userRolesTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userRolesTransLog);
        }

        // POST: DashboardPage/UserRolesTransLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,ModifiedUTCDatetime,Description")] UserRolesTransLog userRolesTransLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRolesTransLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRolesTransLog);
        }

        // GET: DashboardPage/UserRolesTransLogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRolesTransLog userRolesTransLog = db.UserRolesTransLogs.Find(id);
            if (userRolesTransLog == null)
            {
                return HttpNotFound();
            }
            return View(userRolesTransLog);
        }

        // POST: DashboardPage/UserRolesTransLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserRolesTransLog userRolesTransLog = db.UserRolesTransLogs.Find(id);
            db.UserRolesTransLogs.Remove(userRolesTransLog);
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
