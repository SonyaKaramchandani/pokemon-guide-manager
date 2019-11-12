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
    public class UserLoginTransController : Controller
    {
        private BiodZebraEntities db = new BiodZebraEntities();

        // GET: DashboardPage/UserLoginTrans
        public ActionResult Index()
        {
            return View(db.UserLoginTrans.ToList());
        }

        // GET: DashboardPage/UserLoginTrans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginTran userLoginTran = db.UserLoginTrans.Find(id);
            if (userLoginTran == null)
            {
                return HttpNotFound();
            }
            return View(userLoginTran);
        }

        // GET: DashboardPage/UserLoginTrans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/UserLoginTrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserLoginTransID,UserId,LoginDateTime")] UserLoginTran userLoginTran)
        {
            if (ModelState.IsValid)
            {
                db.UserLoginTrans.Add(userLoginTran);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userLoginTran);
        }

        // GET: DashboardPage/UserLoginTrans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginTran userLoginTran = db.UserLoginTrans.Find(id);
            if (userLoginTran == null)
            {
                return HttpNotFound();
            }
            return View(userLoginTran);
        }

        // POST: DashboardPage/UserLoginTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserLoginTransID,UserId,LoginDateTime")] UserLoginTran userLoginTran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLoginTran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userLoginTran);
        }

        // GET: DashboardPage/UserLoginTrans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginTran userLoginTran = db.UserLoginTrans.Find(id);
            if (userLoginTran == null)
            {
                return HttpNotFound();
            }
            return View(userLoginTran);
        }

        // POST: DashboardPage/UserLoginTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLoginTran userLoginTran = db.UserLoginTrans.Find(id);
            db.UserLoginTrans.Remove(userLoginTran);
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
