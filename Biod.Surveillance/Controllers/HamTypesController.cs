using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels.Surveillance;

namespace Biod.Surveillance.Controllers
{
    public class HamTypesController : Controller
    {
        BiodSurveillanceDataEntities db = new BiodSurveillanceDataEntities();

        // GET: HamTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.SurveillanceHamTypes.ToListAsync());
        }

        // GET: HamTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveillanceHamType hamType = await db.SurveillanceHamTypes.FindAsync(id);
            if (hamType == null)
            {
                return HttpNotFound();
            }
            return View(hamType);
        }

        // GET: HamTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HamTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HamTypeId,HamTypeName")] SurveillanceHamType hamType)
        {
            if (ModelState.IsValid)
            {
                db.SurveillanceHamTypes.Add(hamType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hamType);
        }

        // GET: HamTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveillanceHamType hamType = await db.SurveillanceHamTypes.FindAsync(id);
            if (hamType == null)
            {
                return HttpNotFound();
            }
            return View(hamType);
        }

        // POST: HamTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HamTypeId,HamType1")] SurveillanceHamType hamType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hamType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hamType);
        }

        // GET: HamTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveillanceHamType hamType = await db.SurveillanceHamTypes.FindAsync(id);
            if (hamType == null)
            {
                return HttpNotFound();
            }
            return View(hamType);
        }

        // POST: HamTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveillanceHamType hamType = await db.SurveillanceHamTypes.FindAsync(id);
            db.SurveillanceHamTypes.Remove(hamType);
            await db.SaveChangesAsync();
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
