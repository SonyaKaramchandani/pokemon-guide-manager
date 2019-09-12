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
    public class OutbreakPotentialCategoriesController : Controller
    {
        private BiodZebraEntities db = new BiodZebraEntities();

        // GET: OutbreakPotentialCategories
        public ActionResult Index()
        {
            return View(db.OutbreakPotentialCategories.ToList());
        }

        // GET: OutbreakPotentialCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutbreakPotentialCategory outbreakPotentialCategory = db.OutbreakPotentialCategories.Find(id);
            if (outbreakPotentialCategory == null)
            {
                return HttpNotFound();
            }
            return View(outbreakPotentialCategory);
        }

        // GET: OutbreakPotentialCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutbreakPotentialCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AttributeId,Rule,NeedsMap,MapThreshold,EffectiveMessageDescription,EffectiveMessage,IsLocalTransmissionPossible")] OutbreakPotentialCategory outbreakPotentialCategory)
        {
            if (ModelState.IsValid)
            {
                db.OutbreakPotentialCategories.Add(outbreakPotentialCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outbreakPotentialCategory);
        }

        // GET: OutbreakPotentialCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutbreakPotentialCategory outbreakPotentialCategory = db.OutbreakPotentialCategories.Find(id);
            if (outbreakPotentialCategory == null)
            {
                return HttpNotFound();
            }
            return View(outbreakPotentialCategory);
        }

        // POST: OutbreakPotentialCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AttributeId,Rule,NeedsMap,MapThreshold,EffectiveMessageDescription,EffectiveMessage,IsLocalTransmissionPossible")] OutbreakPotentialCategory outbreakPotentialCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outbreakPotentialCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outbreakPotentialCategory);
        }

        // GET: OutbreakPotentialCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutbreakPotentialCategory outbreakPotentialCategory = db.OutbreakPotentialCategories.Find(id);
            if (outbreakPotentialCategory == null)
            {
                return HttpNotFound();
            }
            return View(outbreakPotentialCategory);
        }

        // POST: OutbreakPotentialCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OutbreakPotentialCategory outbreakPotentialCategory = db.OutbreakPotentialCategories.Find(id);
            db.OutbreakPotentialCategories.Remove(outbreakPotentialCategory);
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
