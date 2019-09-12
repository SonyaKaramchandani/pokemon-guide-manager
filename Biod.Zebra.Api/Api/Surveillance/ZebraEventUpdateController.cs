﻿using System;
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
using Biod.Zebra.Api.Api;

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
        public async Task<HttpResponseMessage> PostAsync([FromBody] EventUpdateModel input)
        {
            if (!ModelState.IsValid)
            {
                Logger.Warning($"Validation for EventUpdateModel failed for event ID {input?.eventID}: {GetModelStateErrors(ModelState)}");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                DbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

                var curId = Convert.ToInt32(input.eventID);
                var curEvent = DbContext.Events.Where(s => s.EventId == curId).SingleOrDefault();

                if (curEvent == null)//for a new event
                {
                    //insert event
                    var r = new Event();

                    r = AssignEvent(r, input, true);

                    DbContext.Events.Add(r);
                    DbContext.SaveChanges();

                    //var zebraVersion = ConfigurationManager.AppSettings.Get("ZebraVersion");
                    //var resp = db.usp_SetZebraSourceDestinations(r.EventId, "V3");
                    return await ZebraModelPrerendering(r);
                }
                else // for an existing event
                {
                    //Clear Event items
                    curEvent.EventCreationReasons.Clear();
                    curEvent.Xtbl_Event_Location.Clear();
                    curEvent.ProcessedArticles.Clear();

                    //Logging.Log("ZebraEventUpdate: Step 3");
                    curEvent = AssignEvent(curEvent, input, false);

                    DbContext.SaveChanges();

                    //var zebraVersion = ConfigurationManager.AppSettings.Get("ZebraVersion");
                    //var resp = db.usp_SetZebraSourceDestinations(curEvent.EventId, "V3");
                    return await ZebraModelPrerendering(curEvent);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to update event with ID {input?.eventID}", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task<HttpResponseMessage> ZebraModelPrerendering(Event r)
        {
            //Calculate risk of min and max exportation and render the results in tables
            List<MinMaxCasesClass> minMaxCasesClasses = new List<MinMaxCasesClass>();
            List<usp_ZebraDataRenderSetSourceDestinationsPart1_Result> grids = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart1(r.EventId).Where(x => x.GridId != "-1").ToList();
            if (grids.Any())
            {
                foreach (var grid in grids)
                {
                    var minMaxCasesService = await RequestResponseService.GetMinMaxCasesService(grid.GridId, grid.Cases.Value.ToString());
                    var minMaxCasesServiceResult = minMaxCasesService.Split(',');
                    minMaxCasesClasses.Add(
                        new MinMaxCasesClass()
                        {
                            GridId = minMaxCasesServiceResult[0],
                            Cases = minMaxCasesServiceResult[1],
                            MinCases = minMaxCasesServiceResult[2],
                            MaxCases = minMaxCasesServiceResult[3]
                        });
                }
                string jsonEventGridCases = new JavaScriptSerializer().Serialize(minMaxCasesClasses);

                usp_ZebraDataRenderSetSourceDestinationsPart2_Result eventCasesInfo = DbContext.usp_ZebraDataRenderSetSourceDestinationsPart2(r.EventId, jsonEventGridCases).FirstOrDefault();

                //requestResponseService.GetMinMaxPrevalenceService("0.5", "0.9", "5", "2", "2018-12-12", "2018-12-12").Wait();
                bool isMinCaseOverPopulationSizeEqualZero = false;
                if (eventCasesInfo.MinCaseOverPopulationSize == 0.0)
                {
                    eventCasesInfo.MinCaseOverPopulationSize = 0.000001;
                    isMinCaseOverPopulationSizeEqualZero = true;
                }
                bool isMaxCaseOverPopulationSizeEqualZero = false;
                if (eventCasesInfo.MaxCaseOverPopulationSize == 0.0)
                {
                    eventCasesInfo.MinCaseOverPopulationSize = 0.000001;
                    isMaxCaseOverPopulationSizeEqualZero = true;
                }

                if (!isMaxCaseOverPopulationSizeEqualZero)
                {
                    var minMaxPrevalenceService = await RequestResponseService.GetMinMaxPrevalenceService(
                        Convert.ToDouble(eventCasesInfo.MinCaseOverPopulationSize).ToString("F20"), Convert.ToDouble(eventCasesInfo.MaxCaseOverPopulationSize).ToString("F20"),
                        eventCasesInfo.DiseaseIncubation.ToString(), eventCasesInfo.DiseaseSymptomatic.ToString(),
                        eventCasesInfo.EventStart.Value.ToString("yyyy-MM-dd"), eventCasesInfo.EventEnd?.ToString("yyyy-MM-dd") ?? "");

                    var minMaxPrevalenceResult = minMaxPrevalenceService.Split(',');

                    DbContext.usp_ZebraDataRenderSetSourceDestinationsPart3(r.EventId,
                       isMinCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[0]), isMaxCaseOverPopulationSizeEqualZero ? 0 : Convert.ToDouble(minMaxPrevalenceResult[1])).FirstOrDefault();
                }

            }
            //Calculate risk of min and max importation and render the results in tables
            AccountHelper.PrecalculateRiskByEvent(DbContext, r.EventId);

            Logger.Info($"Successfully updated event with ID {r.EventId}");
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully processed the event " + r.EventId);
        }

        private Event AssignEvent(Event evtObj, EventUpdateModel evm, Boolean isInsert)
        {
            //Logging.Log("ZebraEventUpdate: Step 4");
            //insert or udpate event
            evtObj.EventId = Convert.ToInt32(evm.eventID);
            evtObj.EventTitle = !String.IsNullOrEmpty(evm.eventTitle) ? evm.eventTitle : null;
            evtObj.StartDate = !String.IsNullOrEmpty(evm.startDate) ? Convert.ToDateTime(evm.startDate) : (DateTime?)null;
            evtObj.EndDate = !String.IsNullOrEmpty(evm.endDate) ? Convert.ToDateTime(evm.endDate) : (DateTime?)null;
            evtObj.LastUpdatedDate = DateTime.Now;
            evtObj.IsLocalOnly = bool.Parse(evm.alertRadius);
            evtObj.PriorityId = !String.IsNullOrEmpty(evm.priorityID) ? Convert.ToInt32(evm.priorityID) : (Int32?)null;
            //evtObj.IsPublished = !String.IsNullOrEmpty(evm.isPublished) ? bool.Parse(evm.isPublished) : (Boolean?)null;
            evtObj.IsPublished = true;
            evtObj.Summary = !String.IsNullOrEmpty(evm.summary) ? evm.summary : null;
            evtObj.Notes = !String.IsNullOrEmpty(evm.notes) ? evm.notes : null;
            evtObj.DiseaseId = !String.IsNullOrEmpty(evm.diseaseID) ? Convert.ToInt32(evm.diseaseID) : (Int32?)null;
            evtObj.EventMongoId = !String.IsNullOrEmpty(evm.eventMongoId) ? evm.eventMongoId : null;
            evtObj.LastUpdatedByUserName = !String.IsNullOrEmpty(evm.LastUpdatedByUserName) ? evm.LastUpdatedByUserName : null;

            if (isInsert)
            {
                evtObj.CreatedDate = DateTime.Now;
            }
            evtObj.HasOutlookReport = (!isInsert) ? ((evtObj.HasOutlookReport != null) ? evtObj.HasOutlookReport.Value : false) : false;

            //insert or update reasons
            String[] selectedReasonIDs = (evm.reasonIDs.Any() && evm.reasonIDs[0] != "") ? evm.reasonIDs : null;
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
            if (!String.IsNullOrEmpty(evm.locationObject))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var locObjArr = js.Deserialize<EventLocation[]>(evm.locationObject);
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
                        if (currEvent != null)
                        {
                            if (currEvent.RepCases != eventLocation.RepCases)
                            {
                                //if the new repCase is different than the current value in the original table 
                                //Update history table
                                var updated = DbContext.usp_ZebraEventSetEventCase(evtObj.EventId)?.FirstOrDefault().Result;
                            }
                        }
                        else
                        {
                            //Update history table when there is a the NEW record with different Date or publishing the event 
                            var updated = DbContext.usp_ZebraEventSetEventCase(evtObj.EventId)?.FirstOrDefault().Result;
                        }

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