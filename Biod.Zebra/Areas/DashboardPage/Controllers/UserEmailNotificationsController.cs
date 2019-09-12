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
    public class UserEmailNotificationsController : Controller
    {
        private BiodZebraEntities db = new BiodZebraEntities();

        // GET: DashboardPage/UserEmailNotifications
        public ActionResult Index()
        {
            var userEmailNotifications = db.UserEmailNotifications.Include(u => u.Event);
            return View(userEmailNotifications.ToList());
        }

        // GET: DashboardPage/UserEmailNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailNotification userEmailNotification = db.UserEmailNotifications.Find(id);
            if (userEmailNotification == null)
            {
                return HttpNotFound();
            }
            return View(userEmailNotification);
        }

        // GET: DashboardPage/UserEmailNotifications/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle");
            return View();
        }

        // POST: DashboardPage/UserEmailNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserEmail,EmailType,EventId,Content,SentDate")] UserEmailNotification userEmailNotification)
        {
            if (ModelState.IsValid)
            {
                db.UserEmailNotifications.Add(userEmailNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", userEmailNotification.EventId);
            return View(userEmailNotification);
        }

        // GET: DashboardPage/UserEmailNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailNotification userEmailNotification = db.UserEmailNotifications.Find(id);
            if (userEmailNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", userEmailNotification.EventId);
            return View(userEmailNotification);
        }

        // POST: DashboardPage/UserEmailNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserEmail,EmailType,EventId,Content,SentDate")] UserEmailNotification userEmailNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userEmailNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EventTitle", userEmailNotification.EventId);
            return View(userEmailNotification);
        }

        // GET: DashboardPage/UserEmailNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailNotification userEmailNotification = db.UserEmailNotifications.Find(id);
            if (userEmailNotification == null)
            {
                return HttpNotFound();
            }
            return View(userEmailNotification);
        }

        // POST: DashboardPage/UserEmailNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserEmailNotification userEmailNotification = db.UserEmailNotifications.Find(id);
            db.UserEmailNotifications.Remove(userEmailNotification);
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
