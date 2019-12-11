using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Controllers;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventOrderByFieldsController : BaseController
    {
        // GET: DashboardPage/EventOrderByFields
        public ActionResult Index()
        {
            Logger.Info($"Loaded Admin page for list of Order By Fields");
            return View(DbContext.EventOrderByFields.ToList());
        }

        // GET: DashboardPage/EventOrderByFields/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventOrderByField eventOrderByField = DbContext.EventOrderByFields.Find(id);
            if (eventOrderByField == null)
            {
                return HttpNotFound();
            }

            Logger.Info($"{UserName} viewed Order By Field with ID {id}");
            return View(eventOrderByField);
        }

        // GET: DashboardPage/EventOrderByFields/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardPage/EventOrderByFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName,ColumnName,DisplayOrder,IsDefault,IsHidden")] EventOrderByField eventOrderByField)
        {
            if (ModelState.IsValid)
            {
                DbContext.EventOrderByFields.Add(eventOrderByField);
                DbContext.SaveChanges();

                Logger.Info($"{UserName} created Order By Field with ID {eventOrderByField.Id}");
                return RedirectToAction("Index");
            }

            return View(eventOrderByField);
        }

        // GET: DashboardPage/EventOrderByFields/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventOrderByField eventOrderByField = DbContext.EventOrderByFields.Find(id);
            if (eventOrderByField == null)
            {
                return HttpNotFound();
            }
            return View(eventOrderByField);
        }

        // POST: DashboardPage/EventOrderByFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayName,ColumnName,DisplayOrder,IsDefault,IsHidden")] EventOrderByField eventOrderByField)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(eventOrderByField).State = EntityState.Modified;
                DbContext.SaveChanges();

                Logger.Info($"{UserName} edited Order By Field with ID {eventOrderByField.Id}");
                return RedirectToAction("Index");
            }
            return View(eventOrderByField);
        }

        // GET: DashboardPage/EventOrderByFields/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventOrderByField eventOrderByField = DbContext.EventOrderByFields.Find(id);
            if (eventOrderByField == null)
            {
                return HttpNotFound();
            }
            return View(eventOrderByField);
        }

        // POST: DashboardPage/EventOrderByFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventOrderByField eventOrderByField = DbContext.EventOrderByFields.Find(id);
            DbContext.EventOrderByFields.Remove(eventOrderByField);
            DbContext.SaveChanges();

            Logger.Info($"{UserName} deleted Order By Field with ID {id}");
            return RedirectToAction("Index");
        }
    }
}
