using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Http;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Web.ModelBinding;
using Biod.Zebra.Api.Api;
using Biod.Zebra.Api.Api.Surveillance;
using Biod.Zebra.Library.Infrastructures.Geoname;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.Surveillance;
using Newtonsoft.Json;

namespace Biod.Zebra.Api.Surveillance
{
    [AllowAnonymous]
    public class ZebraEventUpdateController : BaseApiController
    {
        public IZebraUpdateService RequestResponseService { get; set; }

        public ZebraEventUpdateController()
        {
            RequestResponseService = new CustomRequestResponseService();
        }

        public HttpResponseMessage Get()
        {
            //Logging.Log("ZebraEventUpdate GET: Step 1");
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Sucess message");
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody] EventUpdateModel input, [QueryString] bool forceUpdate = false)
        {
            if (!ModelState.IsValid)
            {
                Logger.Warning($"Validation for EventUpdateModel failed for event ID {input?.eventID}: {GetModelStateErrors(ModelState)}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

//            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

                    var curId = Convert.ToInt32(input.eventID);
                    Logger.Debug($"Received Event Update request for event with ID {curId}");
                    var curEvent = DbContext.Events.Where(s => s.EventId == curId).SingleOrDefault();
                    var renderModel = false;
                    var newEvent = curEvent == null;
                    if (newEvent)//for a new event
                    {
                        //insert event
                        curEvent = AssignEvent(new Event(), input, true);
                        DbContext.Events.Add(curEvent);
                        renderModel = true;
                    }
                    else // for an existing event
                    {
                        var currentHashCode = GetEventHashCode(curEvent);

                        if (curEvent.IsBeingCalculated)
                        {
                            Logger.Warning($"Current event with ID {input?.eventID} being processed, rejecting second update");
                            return Request.CreateErrorResponse(HttpStatusCode.Conflict, $"Current event with ID {input?.eventID} being processed, rejecting second update");

                        }

                        //Clear Event items
                        curEvent.EventCreationReasons.Clear();
                        curEvent.Xtbl_Event_Location.Clear();
                        curEvent.ProcessedArticles.Clear();
                        curEvent.EventNestedLocations.Clear();

                        //Logging.Log("ZebraEventUpdate: Step 3");
                        curEvent = AssignEvent(curEvent, input, false);
                        if (curEvent != null)
                        {
                            renderModel = GetEventHashCode(curEvent) != currentHashCode;
                        }
                    }

                    if (curEvent == null)
                    {
                        Logger.Error($"Failed to update event with ID {input?.eventID}");
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Failed to update event with ID {input?.eventID}");
                    }
                    else
                    {
                        GeonameInsertHelper.InsertEventActiveGeonames(DbContext, curEvent);
                        DbContext.SaveChanges();
                        Logger.Debug($"Saved event details for event with ID {curId}");
//                        DbContext.usp_UpdateEventNestedLocations(curEvent.EventId);
//                        dbContextTransaction.Commit();

                        if (forceUpdate || renderModel)
                        {
                            var response = await ZebraModelPrerendering(curEvent);
                            
                            // Send Email notification
                            if (renderModel)
                            {
                                // Only send email if there was an actual difference that caused the model to re-calculate
                                Task.Run(async () =>
                                {
                                    using (var client = new HttpClient())
                                    {
                                        Logger.Debug($"Sending notification API call for event with ID {curId}");
                                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("NotificationApi"));
                                        await client.PostAsync($"{(newEvent ? "event" : "proximal")}?eventId={curId}", null);
                                        Logger.Debug($"Finished sending notifications for event with ID {curId}");
                                    }
                                });
                            }
                            
                            return response;
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, "Successfully processed the event " + curEvent.EventId);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to update event with ID {input?.eventID}", ex);
//                    dbContextTransaction.Rollback();

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        private async Task<HttpResponseMessage> ZebraModelPrerendering(Event r)
        {
            Logger.Debug($"Pre-rendering model for event {r.EventId}");

            if (r.IsLocalOnly)
            {
                Logger.Debug($"Event {r.EventId} is local. min and max exportation risk will not be calculated.");
            } 
            else if (r.EndDate.HasValue)
            {
                Logger.Debug($"Event {r.EventId} is inactive. min and max exportation risk will not be calculated.");
            }
            else
            {
                Logger.Debug($"Calculating min and max exportation risk (Part 1) for event {r.EventId}");
                var grids = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd(r.EventId).Where(x => x.GridId != "-1").ToList();
                if (grids.Any())
                {
                    Logger.Debug($"Found {grids.Count} grids for event {r.EventId}, calculating min/max cases");
                    
                    DbContext.EventSourceGridSpreadMds.AddRange(grids.Select(grid =>
                    {
                        var minMaxCasesServiceResult = RServiceLogic.GetMinMaxCasesCount(grid.Cases ?? 0);
                        return new EventSourceGridSpreadMd
                        {
                            EventId = r.EventId,
                            GridId = grid.GridId,
                            Cases = grid.Cases ?? 0,
                            MinCases = minMaxCasesServiceResult[0],
                            MaxCases = minMaxCasesServiceResult[1]
                        };
                    }));
                    
                    Logger.Debug($"Min/Max cases calculation for all {grids.Count} grids completed for event {r.EventId}");
                    DbContext.SaveChanges();

                    //from part2 sp all except caseOverPop
                    Logger.Debug($"Calculating min and max exportation risk (Part 2) for event {r.EventId}");
                    var eventCasesInfo = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd(r.EventId).FirstOrDefault();
                    //from EventSourceAirportSpreadMd, EventId and caseOverPop
                    var eventSourceAirportSpreadMds = DbContext.EventSourceAirportSpreadMds.Where(e => e.EventId == r.EventId && e.MaxCaseOverPop > 0).ToList();
                    // Update prevalence in EventSourceAirportSpreadMd using results from R
                    if (eventCasesInfo != null)
                    {
                        var isMinCaseOverPopulationSizeEqualZero = false;
                        
                        Logger.Debug($"Found {eventSourceAirportSpreadMds.Count} airports for event {r.EventId}, calculating min/max prevalence");
                        foreach (var eventSourceAirportSpreadMd in eventSourceAirportSpreadMds)
                        {
                            if (eventSourceAirportSpreadMd.MinCaseOverPop <= 0.0)
                            {
                                eventSourceAirportSpreadMd.MinCaseOverPop = 0.000001;
                                isMinCaseOverPopulationSizeEqualZero = true;
                            }

                            var minMaxPrevalenceResult = RServiceLogic.GetMinMaxPrevalence(
                                eventSourceAirportSpreadMd.MinCaseOverPop ?? 0,
                                eventSourceAirportSpreadMd.MaxCaseOverPop ?? 0,
                                eventCasesInfo.DiseaseIncubation ?? 0,
                                eventCasesInfo.DiseaseSymptomatic ?? 0,
                                eventCasesInfo.EventStart.Value,
                                eventCasesInfo.EventEnd.Value);

                            eventSourceAirportSpreadMd.MinPrevalence = isMinCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[0]);
                            eventSourceAirportSpreadMd.MaxPrevalence = Convert.ToDouble(minMaxPrevalenceResult[1]);

                            isMinCaseOverPopulationSizeEqualZero = false;
                        }
                        Logger.Debug($"Min/Max prevalence calculation for all {eventSourceAirportSpreadMds.Count} airports completed for event {r.EventId}");
                        DbContext.SaveChanges();

                        //calling part3
                        Logger.Debug($"Calculating min and max exportation risk (Part 3) for event {r.EventId}");
                        DbContext.usp_ZebraDataRenderSetSourceDestinationsPart3SpreadMd(r.EventId).FirstOrDefault();
                        //what shall we do if above returns -1?
                    }
                }
            }

            Logger.Debug($"Pre-calculating min and max importation risk for event {r.EventId}");
            AccountHelper.PrecalculateRiskByEvent(DbContext, r.EventId);

            Logger.Info($"Successfully updated event with ID {r.EventId}");
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully processed the event " + r.EventId);
        }

        [NonAction]
        public int GetEventHashCode(Event e)
        {
            var hashCode = -1724906143;
            hashCode = hashCode * -1521134295 + e.StartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(e.EndDate);
            hashCode = hashCode * -1521134295 + e.DiseaseId.GetHashCode();
            hashCode = hashCode * -1521134295 + e.IsLocalOnly.GetHashCode();
            hashCode = hashCode * -1521134295 + e.SpeciesId.GetHashCode();
            hashCode = hashCode * -1521134295 + e.Xtbl_Event_Location
                           .Select(x => $"{x.GeonameId}|{x.EventDate}|{x.SuspCases}|{x.RepCases}|{x.ConfCases}".GetHashCode())
                           .Aggregate(0, (a, b) => a * -1521134295 + b);
            return hashCode;
        }
        
        [NonAction]
        public bool IsEventLocationChanged(List<Xtbl_Event_Location> updatedEvent, List<Xtbl_Event_Location> currentEvent)
        {
            if (updatedEvent.Count != currentEvent.Count)
            {
                return true;
            }

            var updatedLocations = new HashSet<string>(updatedEvent.Select(x => $"{x.GeonameId}|{x.EventDate}|{x.SuspCases}|{x.RepCases}|{x.ConfCases}"));
            var currentLocations = new HashSet<string>(currentEvent.Select(x => $"{x.GeonameId}|{x.EventDate}|{x.SuspCases}|{x.RepCases}|{x.ConfCases}"));

            return updatedLocations.Except(currentLocations).Any() || currentLocations.Except(updatedLocations).Any();
        }

        private Event AssignEvent(Event evtObj, EventUpdateModel evm, bool isInsert)
        {
            //insert or udpate event
            evtObj.EventId = Convert.ToInt32(evm.eventID);
            evtObj.EventTitle = string.IsNullOrWhiteSpace(evm.eventTitle) ? null : evm.eventTitle;
            evtObj.StartDate = Convert.ToDateTime(evm.startDate);
            evtObj.EndDate = string.IsNullOrWhiteSpace(evm.endDate) ? (DateTime?)null : Convert.ToDateTime(evm.endDate);
            evtObj.LastUpdatedDate = string.IsNullOrWhiteSpace(evm.lastUpdatedDate) ? DateTime.Now : Convert.ToDateTime(evm.lastUpdatedDate);
            evtObj.IsLocalOnly = bool.Parse(evm.alertRadius);
            evtObj.PriorityId = string.IsNullOrWhiteSpace(evm.priorityID) ? (int?)null : Convert.ToInt32(evm.priorityID);
            evtObj.IsPublished = true;
            evtObj.Summary = string.IsNullOrWhiteSpace(evm.summary) ? null : evm.summary;
            evtObj.Notes = string.IsNullOrWhiteSpace(evm.notes) ? null : evm.notes;
            evtObj.DiseaseId = Convert.ToInt32(evm.diseaseID);
            evtObj.EventMongoId = string.IsNullOrWhiteSpace(evm.eventMongoId) ? null : evm.eventMongoId;
            evtObj.LastUpdatedByUserName = string.IsNullOrWhiteSpace(evm.LastUpdatedByUserName) ? null : evm.LastUpdatedByUserName;
            evtObj.SpeciesId = evm.speciesID;

            if (isInsert)
            {
                evtObj.CreatedDate = DateTime.Now;
            }

            //insert or update reasons
            string[] selectedReasonIDs = (evm.reasonIDs.Any() && evm.reasonIDs[0] != "") ? evm.reasonIDs : null;
            if (selectedReasonIDs != null)
            {
                int[] selectedReasonIdArray = selectedReasonIDs.Select(id => int.Parse(id)).ToArray();
                var selectedReasons = DbContext.EventCreationReasons.Where(cr => selectedReasonIdArray.Contains(cr.ReasonId));
                foreach (var reasonItem in selectedReasons)
                {
                    evtObj.EventCreationReasons.Add(reasonItem);
                }
            };

            //insert or update event locations
            if (!string.IsNullOrEmpty(evm.locationObject))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var locObjArr = js.Deserialize<Library.EntityModels.Zebra.EventLocation[]>(evm.locationObject);
                var eventLocations = locObjArr.Select(x => x.GeonameId).Distinct();
                foreach (var geonameId in eventLocations)
                {
                    //store just the last event location with it's cases as we don't need to 
                    //keep track of the cases in Insights app
                    var eventLocation = locObjArr.Where(r => r.GeonameId == geonameId)
                        .OrderByDescending(x => x.EventDate)
                        .FirstOrDefault();

                    //....Current or the existing event in the original table; Can be null when user added a NEW record with a different date or publishing the event
                    Xtbl_Event_Location currEvent = DbContext.Xtbl_Event_Location
                        .FirstOrDefault(e =>
                            e.EventId == evtObj.EventId && e.GeonameId == eventLocation.GeonameId &&
                            e.EventDate == eventLocation.EventDate);

                    if (eventLocation != null)
                    {
                        Xtbl_Event_Location evtLoc = new Xtbl_Event_Location
                        {
                            EventId = evtObj.EventId,
                            GeonameId = eventLocation.GeonameId,
                            EventDate = eventLocation.EventDate,
                            SuspCases = eventLocation.SuspCases,
                            RepCases = eventLocation.RepCases,
                            ConfCases = eventLocation.ConfCases,
                            Deaths = eventLocation.Deaths
                        };
                        evtObj.Xtbl_Event_Location.Add(evtLoc);

                        var evnLocation = evtObj.EventLocations.SingleOrDefault(l => l.GeonameId == eventLocation.GeonameId && l.EventDate == eventLocation.EventDate);
                        if (evnLocation == null)
                        {
                            evnLocation = new Library.EntityModels.Zebra.EventLocation
                            {
                                EventId = evtObj.EventId,
                                GeonameId = eventLocation.GeonameId,
                                EventDate = eventLocation.EventDate,
                                SuspCases = eventLocation.SuspCases,
                                RepCases = eventLocation.RepCases,
                                ConfCases = eventLocation.ConfCases,
                                Deaths = eventLocation.Deaths
                            };
                            evtObj.EventLocations.Add(evnLocation);
                        }
                        else
                        {
                            evnLocation.EventId = evtObj.EventId;
                            evnLocation.GeonameId = eventLocation.GeonameId;
                            evnLocation.EventDate = eventLocation.EventDate;
                            evnLocation.SuspCases = eventLocation.SuspCases;
                            evnLocation.RepCases = eventLocation.RepCases;
                            evnLocation.ConfCases = eventLocation.ConfCases;
                            evnLocation.Deaths = eventLocation.Deaths;
                        }
                    }
                }
            };

            //insert or update article
            if (!String.IsNullOrEmpty(evm.associatedArticles))
            {
                evm.associatedArticles = evm.associatedArticles.Replace("\\\\\\", "\\\\");
                JavaScriptSerializer js2 = new JavaScriptSerializer();
                js2.MaxJsonLength = Int32.MaxValue;
                var artArr = js2.Deserialize<ArticleUpdateForZebra[]>(evm.associatedArticles);

                foreach (var curArticle in artArr)
                {
                    var existingArticle = DbContext.ProcessedArticles.Where(s => s.ArticleId == curArticle.ArticleId).SingleOrDefault();

                    if (existingArticle == null)//for a new event
                    {
                        var processedArticle = new ProcessedArticle
                        {
                            ArticleId = curArticle.ArticleId,
                            ArticleTitle = curArticle.ArticleTitle,
                            SystemLastModifiedDate = curArticle.SystemLastModifiedDate,
                            CertaintyScore = curArticle.CertaintyScore,
                            ArticleFeedId = curArticle.ArticleFeedId,
                            FeedURL = curArticle.FeedURL,
                            FeedSourceId = curArticle.FeedSourceId,
                            FeedPublishedDate = curArticle.FeedPublishedDate,
                            HamTypeId = curArticle.HamTypeId,
                            OriginalSourceURL = curArticle.OriginalSourceURL,
                            IsCompleted = curArticle.IsCompleted,
                            SimilarClusterId = curArticle.SimilarClusterId,
                            OriginalLanguage = curArticle.OriginalLanguage,
                            UserLastModifiedDate = curArticle.UserLastModifiedDate,
                            LastUpdatedByUserName = curArticle.LastUpdatedByUserName,
                            Notes = curArticle.Notes,
                            ArticleBody = curArticle.ArticleBody,
                            IsRead = curArticle.IsRead
                        };

                        evtObj.ProcessedArticles.Add(processedArticle);
                    }
                    else
                    {
                        evtObj.ProcessedArticles.Add(existingArticle);
                    }

                }
            }
            //Logging.Log("ZebraEventUpdate: Step 5");
            return evtObj;
        }

    }

}