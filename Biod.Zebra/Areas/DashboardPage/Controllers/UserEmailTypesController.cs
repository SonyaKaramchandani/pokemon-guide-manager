using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Controllers;
using Biod.Zebra.Library.EntityModels;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserEmailTypesController : BaseController
    {
        // GET: DashboardPage/UserEmailTypes
        public ActionResult Index()
        {
            Logger.Info($"Loaded Admin page for list of Email Types");
            return View(DbContext.UserEmailTypes.ToList());
        }

        // GET: DashboardPage/UserEmailTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailType userEmailType = DbContext.UserEmailTypes.Find(id);
            if (userEmailType == null)
            {
                return HttpNotFound();
            }

            Logger.Info($"{UserName} viewed Email Type with ID {id}");
            return View(userEmailType);
        }

        // GET: DashboardPage/UserEmailTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/UserEmailTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] UserEmailType userEmailType)
        {
            if (ModelState.IsValid)
            {
                DbContext.UserEmailTypes.Add(userEmailType);
                DbContext.SaveChanges();

                Logger.Info($"{UserName} created Email Type with ID {userEmailType.Id}");
                return RedirectToAction("Index");
            }

            return View(userEmailType);
        }

        // GET: DashboardPage/UserEmailTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailType userEmailType = DbContext.UserEmailTypes.Find(id);
            if (userEmailType == null)
            {
                return HttpNotFound();
            }
            return View(userEmailType);
        }

        // POST: DashboardPage/UserEmailTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] UserEmailType userEmailType)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(userEmailType).State = EntityState.Modified;
                DbContext.SaveChanges();

                Logger.Info($"{UserName} edited Email Type with ID {userEmailType.Id}");
                return RedirectToAction("Index");
            }
            return View(userEmailType);
        }

        // GET: DashboardPage/UserEmailTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmailType userEmailType = DbContext.UserEmailTypes.Find(id);
            if (userEmailType == null)
            {
                return HttpNotFound();
            }
            return View(userEmailType);
        }

        // POST: DashboardPage/UserEmailTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserEmailType userEmailType = DbContext.UserEmailTypes.Find(id);
            DbContext.UserEmailTypes.Remove(userEmailType);
            DbContext.SaveChanges();

            Logger.Info($"{UserName} deleted Email Type with ID {id}");
            return RedirectToAction("Index");
        }
    }
}
