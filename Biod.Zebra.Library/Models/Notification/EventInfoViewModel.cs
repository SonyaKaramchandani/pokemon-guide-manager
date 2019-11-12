using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.Notification.Push;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static Biod.Zebra.Library.Infrastructures.Constants;

namespace Biod.Zebra.Library.Models.Notification
{
    public class EventInfoViewModel : EmailViewModel, IPushViewModel
    {
        public override string ViewFilePath => "~/Views/Home/EventEmail.cshtml";

        public override int NotificationType => EmailTypes.EVENT_EMAIL;

        public List<string> DeviceTokenList { get; set; } = new List<string>();

        public string Summary { get; set; }

        public int NotificationId { get; set; }

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

        public int AdditionalAirports { get; set; }

        public bool AlwaysNotify { get; set; }

        public EventLocationsOutbreakPotentialModel OutbreakPotentialCategory { get; set; }

        public List<usp_ZebraEventGetArticlesByEventId_Result> EventArticles { get; set; }

        public List<usp_ZebraEmailGetEventAirportInfo_Result> EventAirports { get; set; }

        public usp_ZebraEventGetImportationRisk_Result EventImportationRisk { get; set; }

        public EventInfoViewModel()
        {
            EventArticles = new List<usp_ZebraEventGetArticlesByEventId_Result>();
            EventAirports = new List<usp_ZebraEmailGetEventAirportInfo_Result>();
            OutbreakPotentialCategory = new EventLocationsOutbreakPotentialModel();
        }

        public static List<NotificationViewModel> GetNotificationViewModelList(BiodZebraEntities zebraDbContext, UserManager<ApplicationUser> userManager, int eventId, bool isLocal)
        {
            var eventArticles = zebraDbContext.usp_ZebraEventGetArticlesByEventId(eventId)
                                .GroupBy(n => n.SeqId).Select(g => g.FirstOrDefault())
                                .OrderBy(s => s.SeqId).ToList();

            var outbreakPotentialCategories = zebraDbContext.usp_ZebraDashboardGetOutbreakPotentialCategories().ToList();
            var zebraEventsInfos = zebraDbContext.usp_ZebraEmailGetEventByEventId(eventId, Convert.ToInt32(ConfigurationManager.AppSettings.Get("EventDistanceBuffer")))
                // 0 = destination, 1 = local, 2 = always notify
                .Where(e => new HashSet<int> { 2, isLocal ? 1 : 0 }.Contains(e.IsLocal))
                .ToList();

            return GetEventEmailInfoViewModel(zebraDbContext, userManager, eventId, eventArticles, outbreakPotentialCategories, zebraEventsInfos);
        }

        private static List<NotificationViewModel> GetEventEmailInfoViewModel(
            BiodZebraEntities zebraDbContext,
            UserManager<ApplicationUser> userManager,
            int eventId,
            List<usp_ZebraEventGetArticlesByEventId_Result> eventArticles,
            List<usp_ZebraDashboardGetOutbreakPotentialCategories_Result> outbreakPotentialCategories,
            List<usp_ZebraEmailGetEventByEventId_Result> zebraEventsInfos)
        {
            List<NotificationViewModel> result = new List<NotificationViewModel>();
            var users = userManager.Users.ToList();

            foreach (var zebraEventInfo in zebraEventsInfos)
            {
                if (zebraEventInfo == null)
                {
                    continue;
                }

                var user = users.FirstOrDefault(u => u.Id == zebraEventInfo.UserId);
                if (user == null || !user.NewOutbreakNotificationEnabled || !NotificationHelper.ShouldSendNotification(userManager, user))
                {
                    continue;
                }

                //3-send email to the user if it's GeonameId (long, lat) matches the Event Hexagon (GridId)

                //This is to avoid sending same email twice to the user.
                var aoiEventInfo = string.IsNullOrEmpty(zebraEventInfo.AoiGeonameIds) ? "" : zebraEventInfo.AoiGeonameIds;
                string[] userAois = aoiEventInfo.Split(',');
                Array.Sort(userAois);
                var userSortedAois = string.Join(",", userAois);

                var wasEmailSent = zebraDbContext.usp_ZebraEmailGetIsEmailSent(zebraEventInfo.EventId, EmailTypes.EVENT_EMAIL, zebraEventInfo.Email, userSortedAois).FirstOrDefault();
                if (wasEmailSent ?? false)
                {
                    continue;
                }

                //get all airports related to the event for each user
                List<usp_ZebraEmailGetEventAirportInfo_Result> eventAirportsTotal = zebraDbContext.usp_ZebraEmailGetEventAirportInfo(eventId, zebraEventInfo.UserId).ToList();
                List<usp_ZebraEmailGetEventAirportInfo_Result> eventAirports = eventAirportsTotal.Take(10).ToList();


                //Get potential outbreaks for user's AOIs
                var outbreakPotentialCategoryList = OutbreakPotentialCategoryModel.GetOutbreakPotentialCategory(
                                zebraDbContext,
                                zebraEventInfo.EventId.Value,
                                zebraEventInfo.DiseaseId,
                                //Temp fix. OutbreakPotentialAttributeId should not be null. 
                                //DS are not providing AttributeId on their dev and staging intrenal APIs sometimes
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

                EventLocationsOutbreakPotentialModel eventLocationOutbreakModel = outbreakPotentialCategoryList.FirstOrDefault(x => x.Rule == alertStateByHighestPriority.ToString());
                eventLocationOutbreakModel.AoiCount = outbreakPotentialCategoryList.Count;

                //Get the outbreak importation risk value                 
                var eventImportationRisk = zebraDbContext.usp_ZebraEventGetImportationRisk(zebraEventInfo.EventId, zebraEventInfo.AoiGeonameIds).FirstOrDefault();

                var viewModel = new EventInfoViewModel()
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
                    EmailConfirmed = zebraEventInfo.EmailConfirmed.Value,
                    LastUpdatedDate = zebraEventInfo.LastUpdatedDate.Value,
                    StartDate = zebraEventInfo.StartDate.Value.ToString("MMM d, yyyy"),
                    EndDate = (zebraEventInfo.EndDate.ToString("MMM d, yyyy") == "Jan 1, 1900" || zebraEventInfo.EndDate.ToString("MMM d, yyyy").IndexOf("0001") > 0 ? "Ongoing" : zebraEventInfo.EndDate.ToString("MMM d, yyyy")),
                    IsPaidUser = zebraEventInfo.IsPaidUser == null ? false : zebraEventInfo.IsPaidUser.Value,
                    OutbreakPotentialCategory = eventLocationOutbreakModel,
                    EventArticles = eventArticles,
                    EventAirports = eventAirports,
                    AdditionalAirports = eventAirportsTotal.Count - eventAirports.Count,
                    EventImportationRisk = eventImportationRisk,
                    AlwaysNotify = zebraEventInfo.RelevanceId == 1
                };

                viewModel.Summary = viewModel.Brief;
                viewModel.DeviceTokenList = ExternalIdentifierHelper.GetExternalIdentifier(zebraDbContext, ExternalIdentifiers.FIREBASE_FCM, zebraEventInfo.UserId);
                result.Add(viewModel);
            }

            return result;
        }
    }
}
