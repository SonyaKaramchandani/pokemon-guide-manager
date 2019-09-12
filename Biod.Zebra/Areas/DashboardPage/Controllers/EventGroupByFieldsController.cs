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
    public class EventGroupByFieldsController : BaseController
    {
        // GET: DashboardPage/EventGroupByFields
        public ActionResult Index()
        {
            Logger.Info($"Loaded Admin page for list of Group By Fields");
            return View(DbContext.EventGroupByFields.ToList());
        }

        // GET: DashboardPage/EventGroupByFields/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventGroupByField eventGroupByField = DbContext.EventGroupByFields.Find(id);
            if (eventGroupByField == null)
            {
                return HttpNotFound();
            }

            Logger.Info($"{UserName} viewed Group By Field with ID {id}");
            return View(eventGroupByField);
        }

        // GET: DashboardPage/EventGroupByFields/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/EventGroupByFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName,ColumnName,DisplayOrder,IsDefault,IsHidden")] EventGroupByField eventGroupByField)
        {
            if (ModelState.IsValid)
            {
                DbContext.EventGroupByFields.Add(eventGroupByField);
                DbContext.SaveChanges();

                Logger.Info($"{UserName} created Group By Field with ID {eventGroupByField.Id}");
                return RedirectToAction("Index");
            }

            return View(eventGroupByField);
        }

        // GET: DashboardPage/EventGroupByFields/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventGroupByField eventGroupByField = DbContext.EventGroupByFields.Find(id);
            if (eventGroupByField == null)
            {
                return HttpNotFound();
            }
            return View(eventGroupByField);
        }

        // POST: DashboardPage/EventGroupByFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayName,ColumnName,DisplayOrder,IsDefault,IsHidden")] EventGroupByField eventGroupByField)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(eventGroupByField).State = EntityState.Modified;
                DbContext.SaveChanges();

                Logger.Info($"{UserName} edited Group By Field with ID {eventGroupByField.Id}");
                return RedirectToAction("Index");
            }
            return View(eventGroupByField);
        }

        // GET: DashboardPage/EventGroupByFields/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventGroupByField eventGroupByField = DbContext.EventGroupByFields.Find(id);
            if (eventGroupByField == null)
            {
                return HttpNotFound();
            }
            return View(eventGroupByField);
        }

        // POST: DashboardPage/EventGroupByFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventGroupByField eventGroupByField = DbContext.EventGroupByFields.Find(id);
            DbContext.EventGroupByFields.Remove(eventGroupByField);
            DbContext.SaveChanges();

            Logger.Info($"{UserName} deleted Group By Field with ID {id}");
            return RedirectToAction("Index");
        }
    }
}
