﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Biod.SignalR.Postgres.EntityModels;
using Biod.SignalR.Postgres.Hubs;
using Biod.SignalR.Postgres.Models;

namespace Biod.SignalR.Postgres.Controllers
{
    [InvalidModelStateFilter]
    public class ToDoController : ApiControllerWithHub<ToDoHub>
    {
        //private static List<ToDoItem> db = new List<ToDoItem>
        //{
        //    new ToDoItem { ID = 0, Title = "Spread the word about ASP.NET Web API" },
        //    new ToDoItem { ID = 1, Title = "Wash the car" },
        //    new ToDoItem { ID = 2, Title = "Get a haircut", Finished = true }
        //};
        internal static postgresEntities context = new postgresEntities();
        private static List<ToDoItem> db = (from r in context.tblbasamtests.ToList()
                                            select new ToDoItem { ID = r.id, Title = r.name }).ToList();
        private static int lastId = db.Max(tdi => tdi.ID);

        public IEnumerable<ToDoItem> GetToDoItems()
        {
            //postgresEntities context = new postgresEntities();
            //List<ToDoItem> db = (from r in context.tblbasamtests.ToList()
            //                     select new ToDoItem { ID = r.id, Title = r.name }).ToList();
            lock (db)
                return db.ToArray();
        }

        public ToDoItem GetToDoItem(int id)
        {
            lock (db)
            {
                var item = db.SingleOrDefault(i => i.ID == id);
                if (item == null)
                    throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound)
                    );

                return item;
            }
        }

        public HttpResponseMessage PostNewToDoItem(ToDoItem item)
        {
            lock (db)
            {
                // Add item to the "database"
                item.ID = Interlocked.Increment(ref lastId);
                db.Add(item);

                // Notify the connected clients
                Hub.Clients.addItem(item);

                // Return the new item, inside a 201 response
                var response = Request.CreateResponse(HttpStatusCode.Created, item);
                string link = Url.Link("apiRoute", new { controller = "todo", id = item.ID });
                response.Headers.Location = new Uri(link);
                return response;
            }
        }

        public ToDoItem PutUpdatedToDoItem(int id, ToDoItem item)
        {
            lock (db)
            {
                // Find the existing item
                var toUpdate = db.SingleOrDefault(i => i.ID == id);
                if (toUpdate == null)
                    throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound)
                    );

                // Update the editable fields and save back to the "database"
                toUpdate.Title = item.Title;
                toUpdate.Finished = item.Finished;

                // Notify the connected clients
                Hub.Clients.updateItem(toUpdate);

                // Return the updated item
                return toUpdate;
            }
        }

        public HttpResponseMessage DeleteToDoItem(int id)
        {
            lock (db)
            {
                int removeCount = db.RemoveAll(i => i.ID == id);
                if (removeCount <= 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                // Notify the connected clients
                Hub.Clients.deleteItem(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
