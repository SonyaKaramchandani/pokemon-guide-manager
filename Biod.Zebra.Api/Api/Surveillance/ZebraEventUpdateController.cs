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
using Biod.Zebra.Library.Infrastructures.Geoname;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models.Surveillance;

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
                    var curEvent = DbContext.Events.Where(s => s.EventId == curId).SingleOrDefault();
                    var renderModel = false;
                    if (curEvent == null)//for a new event
                    {
                        //insert event
                        curEvent = AssignEvent(new Event(), input, true);
                        DbContext.Events.Add(curEvent);
                        renderModel = true;
                    }
                    else // for an existing event
                    {
                        var currentHashCode = GetEventHashCode(curEvent);

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
//                        DbContext.usp_UpdateEventNestedLocations(curEvent.EventId);
//                        dbContextTransaction.Commit();

                        if (forceUpdate || renderModel)
                        {
                            //var zebraVersion = ConfigurationManager.AppSettings.Get("ZebraVersion");
                            //var resp = db.usp_SetZebraSourceDestinations(curEvent.EventId, "V3");
                            return await ZebraModelPrerendering(curEvent);
                            //return await ZebraSpreadModelPrerendering(curEvent);
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
                Logger.Debug($"Calculating min and max exportation risk for event {r.EventId}");
                var grids = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd(r.EventId).Where(x => x.GridId != "-1").ToList();
                if (grids.Any())
                {
                    foreach (var grid in grids)
                    {
                        var minMaxCasesService = await RequestResponseService.GetMinMaxCasesService(grid.GridId, grid.Cases.Value.ToString());
                        var minMaxCasesServiceResult = minMaxCasesService.Split(',');
                        //save case count in zebra.EventSourceGridSpreadMd
                        var eventSourceGridSpreadMd = new EventSourceGridSpreadMd
                        {
                            EventId = r.EventId,
                            GridId = minMaxCasesServiceResult[0],
                            Cases = int.Parse(minMaxCasesServiceResult[1]),
                            MinCases = int.Parse(minMaxCasesServiceResult[2]),
                            MaxCases = int.Parse(minMaxCasesServiceResult[3])
                        };
                        DbContext.EventSourceGridSpreadMds.Add(eventSourceGridSpreadMd);
                    }
                    DbContext.SaveChanges();

                    //from part2 sp all except caseOverPop
                    var eventCasesInfo = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd(r.EventId).FirstOrDefault();
                    //from EventSourceAirportSpreadMd, EventId and caseOverPop
                    var eventSourceAirportSpreadMds = DbContext.EventSourceAirportSpreadMds.Where(e => e.EventId == r.EventId && e.MaxCaseOverPop > 0);
                    // Update prevalence in EventSourceAirportSpreadMd using results from R
                    if (eventCasesInfo != null)
                    {
                        var isMinCaseOverPopulationSizeEqualZero = false;
                        
                        foreach (var eventSourceAirportSpreadMd in eventSourceAirportSpreadMds)
                        {
                            if (eventSourceAirportSpreadMd.MinCaseOverPop <= 0.0)
                            {
                                eventSourceAirportSpreadMd.MinCaseOverPop = 0.000001;
                                isMinCaseOverPopulationSizeEqualZero = true;
                            }

                            var minMaxPrevalenceService = await RequestResponseService.GetInsightsMinMaxPrevalenceService(
                                Convert.ToDouble(eventSourceAirportSpreadMd.MinCaseOverPop).ToString("F20"), Convert.ToDouble(eventSourceAirportSpreadMd.MaxCaseOverPop).ToString("F20"),
                                eventCasesInfo.DiseaseIncubation.ToString(), eventCasesInfo.DiseaseSymptomatic.ToString(),
                                eventCasesInfo.EventStart.Value.ToString("yyyy-MM-dd"), eventCasesInfo.EventEnd?.ToString("yyyy-MM-dd") ?? "");

                            var minMaxPrevalenceResult = minMaxPrevalenceService.Split(',');

                            eventSourceAirportSpreadMd.MinPrevalence = isMinCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[0]);
                            eventSourceAirportSpreadMd.MaxPrevalence = Convert.ToDouble(minMaxPrevalenceResult[1]);

                            isMinCaseOverPopulationSizeEqualZero = false;
                        }

                        DbContext.SaveChanges();

                        //calling part3
                        DbContext.usp_ZebraDataRenderSetSourceDestinationsPart3SpreadMd(r.EventId).FirstOrDefault();
                        //what shall we do if above returns -1?
                    }
                }
            }

            Logger.Debug($"Calculating min and max importation risk for event {r.EventId}");
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