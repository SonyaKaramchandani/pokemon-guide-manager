﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;

namespace Biod.Zebra.Library.Models.Email
{
    public class EventEmailInfoViewModel : EmailViewModel
    {
        public EventEmailInfoViewModel()
        {
            EventArticles = new List<usp_ZebraEventGetArticlesByEventId_Result>();
            EventMapInfo = new EventMapInfoModel(EventId ?? -1);
            EventAirports = new List<usp_ZebraEmailGetEventAirportInfo_Result>();
            OutbreakPotentialCategory = new EventLocationsOutbreakPotentialModel();
        }

        public static List<EventEmailInfoViewModel> GetEventEmailInfoModel(BiodZebraEntities zebraDbContext, int eventId, bool isLocal)
        {
            var eventArticles = zebraDbContext.usp_ZebraEventGetArticlesByEventId(eventId)
                                .GroupBy(n => n.SeqId).Select(g => g.FirstOrDefault())
                                .OrderBy(s => s.SeqId).ToList();

            var outbreakPotentialCategories = zebraDbContext.usp_ZebraDashboardGetOutbreakPotentialCategories().ToList();
            var zebraEventsInfos = zebraDbContext.usp_ZebraEmailGetEventByEventId(eventId, Convert.ToInt32(ConfigurationManager.AppSettings.Get("EventDistanceBuffer")))
                .Where(r => r.IsLocal != null && r.IsLocal.Value == isLocal)
                .ToList();

            List<EventEmailInfoViewModel> eventsEmailInfo = GetEventEmailInfoViewModel(zebraDbContext, eventId, eventArticles, outbreakPotentialCategories, zebraEventsInfos);
            return eventsEmailInfo;
        }

        private static List<EventEmailInfoViewModel> GetEventEmailInfoViewModel(
            BiodZebraEntities zebraDbContext,
            int eventId,
            List<usp_ZebraEventGetArticlesByEventId_Result> eventArticles,
            List<usp_ZebraDashboardGetOutbreakPotentialCategories_Result> outbreakPotentialCategories,
            List<usp_ZebraEmailGetEventByEventId_Result> zebraEventsInfos)
        {
            List<EventEmailInfoViewModel> eventsEmailInfoModel = new List<EventEmailInfoViewModel>();

            foreach (var zebraEventInfo in zebraEventsInfos)
            {
                //3-send email to the user if it's GeonameId (long, lat) matches the Event Hexagon (GridId)
                if (zebraEventInfo != null)
                {
                    //This is to avoid sending same email twice to the user.
                    var aoiEventInfo = string.IsNullOrEmpty(zebraEventInfo.AoiGeonameIds) ? "" : zebraEventInfo.AoiGeonameIds;
                    String[] userAois = aoiEventInfo.Split(',');
                    Array.Sort(userAois);
                    var userSortedAois = string.Join(",", userAois);

                    var wasEmailSent = zebraDbContext.usp_ZebraEmailGetIsEmailSent(zebraEventInfo.EventId, Constants.EmailTypes.EVENT_EMAIL, zebraEventInfo.Email, userSortedAois).FirstOrDefault();

                    if (wasEmailSent == false)
                    {
                        //get all airports related to the event for each user
                        List<usp_ZebraEmailGetEventAirportInfo_Result> eventAirportsTotal = zebraDbContext.usp_ZebraEmailGetEventAirportInfo(eventId, zebraEventInfo.UserId).ToList();
                        List<usp_ZebraEmailGetEventAirportInfo_Result> eventAirports = eventAirportsTotal.Take(10).ToList();


                        //Get potential outbreaks for user's AOIs
                        var outbreakPotentialCategoryList = OutbreakPotentialCategoryModel.GetOutbreakPotentialCategory(
                                        zebraDbContext,
                                        zebraEventInfo.EventId.Value, 
                                        zebraEventInfo.DiseaseId,
                                        //Temp fix. OutbreakPotentialAttributeId should not be null. 
                                        //DS are not providing AttributeId on their dev and staging internal APIs sometimes
                                        zebraEventInfo.OutbreakPotentialAttributeId != null ? zebraEventInfo.OutbreakPotentialAttributeId.Value : 5, 
                                        outbreakPotentialCategories, 
                                        zebraEventInfo.AoiGeonameIds);

                        //Get the outbreak potential that matches with the highest priority of EffectiveMessage
                        List<int> alertStateByPriority = new List<int>();

                        foreach (var outbreakPotential in outbreakPotentialCategoryList)
                        {
                            if ((outbreakPotential.AttributeId == 3 && outbreakPotential.MapThreshold == ">0") || outbreakPotential.AttributeId == 1)
                            {
                                alertStateByPriority.Add(Convert.ToInt32(outbreakPotential.Rule));
                            }
                            else if (outbreakPotential.AttributeId == 2)
                            {
                                alertStateByPriority.Add(Convert.ToInt32(outbreakPotential.Rule));
                            }
                            else if ((outbreakPotential.AttributeId == 3 && outbreakPotential.MapThreshold == "=0") || outbreakPotential.AttributeId == 4)
                            {
                                alertStateByPriority.Add(Convert.ToInt32(outbreakPotential.Rule));
                            }
                            else
                            {
                                alertStateByPriority.Add(Convert.ToInt32(outbreakPotential.Rule));
                            }
                        }
                        var alertStateByHighestPriority = alertStateByPriority.Max();

                        EventLocationsOutbreakPotentialModel eventLocationOutbreakModel = outbreakPotentialCategoryList.Where(x => x.Rule == alertStateByHighestPriority.ToString()).FirstOrDefault();
                        eventLocationOutbreakModel.AoiCount = outbreakPotentialCategoryList.Count;

                        //Get the outbreak importation risk value                 
                        var eventImportationRisk = zebraDbContext.usp_ZebraEventGetImportationRisk(zebraEventInfo.EventId, zebraEventInfo.AoiGeonameIds).FirstOrDefault();

                        eventsEmailInfoModel.Add(new EventEmailInfoViewModel()
                        {
                            EventId = zebraEventInfo.EventId.Value,
                            DiseaseName = zebraEventInfo.DiseaseName,
                            MicrobeType = zebraEventInfo.MicrobeType,
                            IncubationPeriod = zebraEventInfo.IncubationPeriod,
                            Vaccination = zebraEventInfo.Vaccination,
                            UserAoiLocationNames = zebraEventInfo.UserAoiLocationNames,
                            AoiGeonameIds = zebraEventInfo.AoiGeonameIds,
                            Title = zebraEventInfo.EventTitle,
                            Brief = zebraEventInfo.Brief,
                            PriorityTitle = zebraEventInfo.ExportationPriorityTitle,
                            ProbabilityName = zebraEventInfo.ExportationProbabilityName,
                            Reasons = zebraEventInfo.Reasons,
                            TransmittedBy = zebraEventInfo.TransmittedBy,
                            Email = zebraEventInfo.Email,
                            UserId = zebraEventInfo.UserId,
                            DoNotTrackEnabled = zebraEventInfo.DoNotTrackEnabled.Value,
                            LastUpdatedDate = zebraEventInfo.LastUpdatedDate.Value,
                            StartDate = zebraEventInfo.StartDate.Value.ToString("MMM d, yyyy"),
                            EndDate = (zebraEventInfo.EndDate.ToString("MMM d, yyyy") == "Jan 1, 1900" || zebraEventInfo.EndDate.ToString("MMM d, yyyy").IndexOf("0001") > 0 ? "Ongoing" : zebraEventInfo.EndDate.ToString("MMM d, yyyy")),
                            IsPaidUser = zebraEventInfo.IsPaidUser == null ? false : zebraEventInfo.IsPaidUser.Value,
                            OutbreakPotentialCategory = eventLocationOutbreakModel,
                            EventArticles = eventArticles,
                            EventAirports = eventAirports,
                            AdditionalAirports = eventAirportsTotal.Count - eventAirports.Count,
                            EventMapInfo = new EventMapInfoModel(zebraEventInfo.EventId.Value),
                            EventImportationRisk = eventImportationRisk
                        });
                    }
                }
            }

            return eventsEmailInfoModel;
        }

        public override string GetViewFilePath()
        {
            return "~/Views/Home/EventEmail.cshtml";
        }

        public override int GetEmailType()
        {
            return Constants.EmailTypes.EVENT_EMAIL;
        }

        public int EventGeonameId { get; set; }
        public string DiseaseName { get; set; }
        public string UserAoiLocationNames { get; set; }
        public string MicrobeType { get; set; }
        public string TransmittedBy { get; set; }
        public string IncubationPeriod { get; set; }
        public string Vaccination { get; set; }
        public string Brief { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reasons { get; set; }
        public string PriorityTitle { get; set; }
        public string ProbabilityName { get; set; }
        public string DestinationDisplayName { get; set; }
        public bool IsPaidUser { get; set; }
        public EventLocationsOutbreakPotentialModel OutbreakPotentialCategory { get; set; }
        public List<usp_ZebraEventGetArticlesByEventId_Result> EventArticles { get; set; }
        public EventMapInfoModel EventMapInfo { get; set; }
        public List<usp_ZebraEmailGetEventAirportInfo_Result> EventAirports { get; set; }
        public int AdditionalAirports { get; set; }
        public usp_ZebraEventGetImportationRisk_Result EventImportationRisk { get; set; }
    }
}
