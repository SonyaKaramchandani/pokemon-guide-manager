using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Http;
using BdDataApi.Models;
using BdDataApi.EntityModels;
using BdDataApi.Infrastructures;
using System.Net.Http;
using System.Net;
using System.Configuration;

namespace BdDataApi.Api
{
    [AllowAnonymous]
    public class ZebraEventUpdateController : ApiController
    {

        public HttpResponseMessage Get()
        {
            Logging.Log("ZebraEventUpdate GET: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucess message");
            return response;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] EventUpdateModel input)
        {
            IHttpActionResult toReturnAction = Ok();
            Logging.Log("ZebraEventUpdate: Step 1");
            try
            {
                using (var db = new ZebraEntities())
                {
                    Logging.Log("ZebraEventUpdate: Step 2");
                    if (!String.IsNullOrEmpty(input.eventID))
                    {
                        var curId = Convert.ToInt32(input.eventID);
                        var curEvent = db.Events.Where(s => s.EventId == curId).SingleOrDefault();

                        if (curEvent == null)//for a new event
                        {
                            //insert event
                            var r = new Event();

                            r = AssignEvent(r, input, db, true);

                            db.Events.Add(r);
                            db.SaveChanges();

                            var resp = db.usp_SetZebraSourceDestinations(r.EventId, ConfigurationManager.AppSettings.Get("ZebraVersion"));

                            toReturnAction = Ok("Success! Event " + r.EventId + " has been inserted"); 
                        }
                        else // for an existing event
                        {
                            //Clear Event items
                            curEvent.EventCreationReasons.Clear();
                            curEvent.Xtbl_Event_Location.Clear();
                            curEvent.ProcessedArticles.Clear();

                            Logging.Log("ZebraEventUpdate: Step 3");
                            curEvent = AssignEvent(curEvent, input, db, false);

                            db.SaveChanges();

                            var resp = db.usp_SetZebraSourceDestinations(curEvent.EventId, ConfigurationManager.AppSettings.Get("ZebraVersion"));

                            toReturnAction = Ok("Success! Event " + curEvent.EventId + " has been updated");
                        }
                    }
                    else
                    {
                        toReturnAction = BadRequest("Error: eventID cannot be null");
                    }
                }
            }
            catch(Exception ex) {
                toReturnAction = BadRequest("Error: " + ex.Message);
            }

            return toReturnAction;
        }

        private Event AssignEvent(Event evtObj, EventUpdateModel evm, ZebraEntities db, Boolean isInsert) {
            Logging.Log("ZebraEventUpdate: Step 4");
            //insert or udpate event
            evtObj.EventId = Convert.ToInt32(evm.eventID);
            evtObj.EventTitle = !String.IsNullOrEmpty(evm.eventTitle) ? evm.eventTitle : null;
            evtObj.StartDate = !String.IsNullOrEmpty(evm.startDate) ? Convert.ToDateTime(evm.startDate) : (DateTime?)null;
            evtObj.EndDate = !String.IsNullOrEmpty(evm.endDate) ? Convert.ToDateTime(evm.endDate) : (DateTime?)null;
            evtObj.LastUpdatedDate = DateTime.Now;
            evtObj.PriorityId = !String.IsNullOrEmpty(evm.priorityID) ? Convert.ToInt32(evm.priorityID) : (Int32?)null;
            //evtObj.IsPublished = !String.IsNullOrEmpty(evm.isPublished) ? bool.Parse(evm.isPublished) : (Boolean?)null;
            evtObj.IsPublished = true;
            evtObj.Summary = !String.IsNullOrEmpty(evm.summary) ? evm.summary : null;
            evtObj.Notes = !String.IsNullOrEmpty(evm.notes) ? evm.notes : null;
            evtObj.DiseaseId = !String.IsNullOrEmpty(evm.diseaseID) ? Convert.ToInt32(evm.diseaseID) : (Int32?)null;
            evtObj.EventMongoId = !String.IsNullOrEmpty(evm.eventMongoId) ? evm.eventMongoId : null;
            evtObj.LastUpdatedByUserName = !String.IsNullOrEmpty(evm.LastUpdatedByUserName) ? evm.LastUpdatedByUserName : null;
            if (isInsert) {
                evtObj.CreatedDate = DateTime.Now;
            }

            //insert or update reasons
            String[] selectedReasonIDs = (evm.reasonIDs[0] != "") ? evm.reasonIDs : null;
            if (selectedReasonIDs != null)
            {
                foreach (var reasonId in selectedReasonIDs)
                {
                    var reasonItem = db.EventCreationReasons.Find(int.Parse(reasonId));
                    evtObj.EventCreationReasons.Add(reasonItem);
                }
            };

            //insert or update event locations
            if (!String.IsNullOrEmpty(evm.locationObject))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var locObjArr = js.Deserialize<EventLocation[]>(evm.locationObject);

                foreach (var item in locObjArr)
                {
                    Xtbl_Event_Location evtLoc = new Xtbl_Event_Location();
                    evtLoc.EventId = evtObj.EventId;
                    evtLoc.GeonameId = item.GeonameId;
                    evtLoc.EventDate = item.EventDate;
                    evtLoc.SuspCases = item.SuspCases;
                    evtLoc.RepCases = item.RepCases;
                    evtLoc.ConfCases = item.ConfCases;
                    evtLoc.Deaths = item.Deaths;

                    evtObj.Xtbl_Event_Location.Add(evtLoc);
                }
            };

            //insert or update article
            if (!String.IsNullOrEmpty(evm.associatedArticles))
            {
                evm.associatedArticles = evm.associatedArticles.Replace("\\\\\\", "\\\\");
                JavaScriptSerializer js2 = new JavaScriptSerializer();
                var artArr = js2.Deserialize<ArticleUpdateForZebra[]>(evm.associatedArticles);

                foreach (var curArticle in artArr)
                {
                    var existingArticle = db.ProcessedArticles.Where(s => s.ArticleId == curArticle.ArticleId).SingleOrDefault();

                    if (existingArticle == null)//for a new event
                    {
                        var a = new ProcessedArticle();
                        a.ArticleId = curArticle.ArticleId;
                        a.ArticleTitle = curArticle.ArticleTitle;
                        a.SystemLastModifiedDate = curArticle.SystemLastModifiedDate;
                        a.CertaintyScore = curArticle.CertaintyScore;
                        a.ArticleFeedId = curArticle.ArticleFeedId;
                        a.FeedURL = curArticle.FeedURL;
                        a.FeedSourceId = curArticle.FeedSourceId;
                        a.FeedPublishedDate = curArticle.FeedPublishedDate;
                        a.HamTypeId = curArticle.HamTypeId;
                        a.OriginalSourceURL = curArticle.OriginalSourceURL;
                        a.IsCompleted = curArticle.IsCompleted;
                        a.SimilarClusterId = curArticle.SimilarClusterId;
                        a.OriginalLanguage = curArticle.OriginalLanguage;
                        a.UserLastModifiedDate = curArticle.UserLastModifiedDate;
                        a.LastUpdatedByUserName = curArticle.LastUpdatedByUserName;
                        a.Notes = curArticle.Notes;
                        a.ArticleBody = curArticle.ArticleBody;
                        a.IsRead = curArticle.IsRead;

                        evtObj.ProcessedArticles.Add(a);
                    }
                    else {
                        evtObj.ProcessedArticles.Add(existingArticle);
                    }

                }
            }

            Logging.Log("ZebraEventUpdate: Step 5");
            return evtObj;
        }
    }

    [AllowAnonymous]
    public class ZebraArticleUpdateController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] ArticleUpdateForZebra input)
        {
            IHttpActionResult toReturnAction = Ok();

            try
            {
                using (var db = new ZebraEntities())
                {
                    if (!String.IsNullOrEmpty(input.ArticleId))
                    {
                        var curId = input.ArticleId;
                        var curArticle = db.ProcessedArticles.Where(s => s.ArticleId == curId).SingleOrDefault();

                        if (curArticle == null)//for a new article
                        {
                            //insert article
                            var r = new ProcessedArticle();

                            r = AssignArticle(r, input, db, true);

                            db.ProcessedArticles.Add(r);
                            db.SaveChanges();
                            toReturnAction = Ok("Success! Article " + r.ArticleId + " has been inserted");
                        }
                        else // for an existing article
                        {
                            //Clear article items
                            curArticle.Xtbl_Article_Location_Disease.Clear();
                            curArticle.Events.Clear();

                            curArticle = AssignArticle(curArticle, input, db, false);

                            db.SaveChanges();
                            toReturnAction = Ok("Success! Article " + curArticle.ArticleId + " has been updated");
                        }
                    }
                    else
                    {
                        toReturnAction = BadRequest("Error: ArticleId cannot be null");
                    }
                }
            }
            catch (Exception ex)
            {
                toReturnAction = BadRequest("Error: " + ex.Message);
            }

            return toReturnAction;
        }

        private ProcessedArticle AssignArticle(ProcessedArticle artObj, ArticleUpdateForZebra artm, ZebraEntities db, Boolean isInsert)
        {
            artObj.ArticleId = artm.ArticleId;
            artObj.ArticleTitle = artm.ArticleTitle;
            artObj.ArticleFeedId = artm.ArticleFeedId;
            artObj.FeedURL = artm.FeedURL;
            artObj.FeedSourceId = artm.FeedSourceId;
            artObj.FeedPublishedDate = artm.FeedPublishedDate;
            artObj.HamTypeId = artm.HamTypeId;
            artObj.OriginalSourceURL = artm.OriginalSourceURL;
            artObj.IsCompleted = artm.IsCompleted;
            artObj.SimilarClusterId = artm.SimilarClusterId;
            artObj.OriginalLanguage = artm.OriginalLanguage;
            artObj.UserLastModifiedDate = artm.UserLastModifiedDate;
            artObj.LastUpdatedByUserName = artm.LastUpdatedByUserName;
            artObj.Notes = artm.Notes;
            artObj.ArticleBody = artm.ArticleBody;
            artObj.IsRead = artm.IsRead;
            artObj.SystemLastModifiedDate = artm.SystemLastModifiedDate;

            //insert or update the association with event
            if (artm.SelectedPublishedEventIds != null)
            {
                foreach (var evtId in artm.SelectedPublishedEventIds)
                {
                    var evtItem = db.Events.Find(evtId);
                    if (evtItem != null) { 
                        artObj.Events.Add(evtItem);
                    }
                }
            };

            if (!String.IsNullOrEmpty(artm.DiseaseObject)) {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var disArr = js.Deserialize<ArticleLocationDisease[]>(artm.DiseaseObject);

                foreach (var disItem in disArr)
                {
                    var ald = new Xtbl_Article_Location_Disease();
                    ald.ArticleId = artm.ArticleId;
                    ald.DiseaseId = disItem.DiseaseId;
                    ald.LocationGeoNameId = disItem.LocationId;
                    ald.NewSuspectedCount = disItem.NewSuspectedCount;
                    ald.NewConfirmedCount = disItem.NewConfirmedCount;
                    ald.NewReportedCount = disItem.NewReportedCount;
                    ald.NewDeathCount = disItem.NewDeathCount;
                    ald.TotalSuspectedCount = disItem.TotalSuspectedCount;
                    ald.TotalConfirmedCount = disItem.TotalConfirmedCount;
                    ald.TotalReportedCount = disItem.TotalReportedCount;
                    ald.TotalDeathCount = disItem.TotalDeathCount;

                    artObj.Xtbl_Article_Location_Disease.Add(ald);
                }
            }

            return artObj;
        }
    }
}