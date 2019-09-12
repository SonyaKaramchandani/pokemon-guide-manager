using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;
using System.IO;
using Biod.Zebra.Controllers;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin,EditorUsers")]
    public class EventsController : BaseController
    {
        // GET: DashboardPage/Events
        public async Task<ActionResult> Index()
        {
            var events = DbContext.Events;

            Logger.Info($"Loaded Admin page for list of Events");
            return View(await events.ToListAsync());
        }

        // GET: DashboardPage/Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await DbContext.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            Logger.Info($"{UserName} viewed Event with ID {id}");
            return View(@event);
        }

        // GET: DashboardPage/Events/Create
        public ActionResult Create()
        {
            ViewBag.PriorityId = new SelectList(DbContext.EventPriorities, "PriorityId", "PriorityTitle");
            return View();
        }

        // POST: DashboardPage/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EventId,EventTitle,HasOutlookReport,StartDate,EndDate,LastUpdatedDate,PriorityId,IsPublished,Summary,Notes,DiseaseId,CreatedDate,EventMongoId,LastUpdatedByUserName")] Event @event)
        {
            if (ModelState.IsValid)
            {
                DbContext.Events.Add(@event);
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} created Event with ID {@event.EventId}");
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(DbContext.EventPriorities, "PriorityId", "PriorityTitle", @event.PriorityId);
            return View(@event);
        }

        // GET: DashboardPage/Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await DbContext.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityId = new SelectList(DbContext.EventPriorities, "PriorityId", "PriorityTitle", @event.PriorityId);
            return View(@event);
        }

        // POST: DashboardPage/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EventId,EventTitle,HasOutlookReport,OutlookReportFile,StartDate,EndDate,LastUpdatedDate,PriorityId,IsPublished,Summary,Notes,DiseaseId,CreatedDate,EventMongoId,LastUpdatedByUserName")] Event @event)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        //save new event outlook report file
                        //var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/"), @event.EventId + Path.GetExtension(file.FileName));
                        file.SaveAs(path);
                    }

                    DbContext.Entry(@event).State = EntityState.Modified;
                    await DbContext.SaveChangesAsync();

                    Logger.Info($"{UserName} edited Event with ID {@event.EventId}");
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PriorityId = new SelectList(DbContext.EventPriorities, "PriorityId", "PriorityTitle", @event.PriorityId);
            return View(@event);
        }

        // GET: DashboardPage/Events/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await DbContext.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);

        }

        // POST: DashboardPage/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await DbContext.Events.FindAsync(id);
            DbContext.Events.Remove(@event);
            await DbContext.SaveChangesAsync();

            Logger.Info($"{UserName} deleted Event with ID {id}");
            return RedirectToAction("Index");
        }
    }
}
